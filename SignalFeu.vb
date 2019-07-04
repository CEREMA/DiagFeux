'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : SignalFeu.vb																											'
'						Classes																														'
'							SignalFeu																																'
'******************************************************************************
Option Strict Off
Option Explicit On

'=====================================================================================================
'--------------------------- Classe SignalFeu --------------------------
'=====================================================================================================
Public Class SignalFeu : Inherits M�tier

  Public mSignal As Signal

  '##ModelId=4035D545002E
  Public mLigneFeux As LigneFeux

  ' Point sp�cifiant le d�calage entre le point de r�f�rence du signal et son point d'insertion (en coordonn�es dessin)
  Private mPositionR�elle As Drawing.PointF

  '********************************************************************************************************************
  'unSignalFeu : membre de la collection cndSignaux correspondant au Signal de feu principal de la ligne de feux
  '********************************************************************************************************************
  Public Sub New(ByVal unSignal As Signal, ByVal uneLigneFeux As LigneFeux)
    mSignal = unSignal
    mLigneFeux = uneLigneFeux
  End Sub

  Public Property Position() As Point
    Get
      With mPositionR�elle
        Return New Point(ToDessin(.X), ToDessin(.Y))
      End With
    End Get
    Set(ByVal Value As Point)
      With mPositionR�elle
        .X = ToR�el(Value.X)
        .Y = ToR�el(Value.Y)
      End With
    End Set
  End Property

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    Dim uneLigne As Ligne
    Dim pOriRappel As Point
    Dim AngleSymbole As Single
    Dim pRef As Point = PtR�f�rence
    'Point d'insertion du symbole (�galement centre de la boite pour le d�placement du symbole)
    Dim pCentre As Point = Translation(pRef, Position)
    Dim Echel As Single = 1.0

    Try

      If mSignal.EstPi�ton Then
        Dim uneLignePi�tons As LigneFeuPi�tons = mLigneFeux
        Dim uneFleche As Fleche = uneLignePi�tons.mTravers�e.Fl�che
        uneLigne = uneFleche.LigneR�f�rence '(uneFleche.Count - 1)
        AngleSymbole = AngleForm�(uneLigne)
        If mLigneFeux.mSignalFeu(1) Is Me Then AngleSymbole += Math.PI
        pOriRappel = pRef

      Else
        Dim uneLigneV�hicules As LigneFeuV�hicules = mLigneFeux
        'Dessiner la ligne de raccord � partir de l'extr�mit� de la ligne de feux en la prolongeant vers l'ext�rieur
        uneLigne = CType(mLigneFeux, LigneFeuV�hicules).Dessin
        pOriRappel = uneLigne.Milieu
        'Faire pivoter le symbole en sorte que la fl�che soit tourn� vers l'extr�mit� de la branche
        AngleSymbole = AngleForm�(uneLigne) - Math.PI / 2
      End If

      Dim DemiLargeur As Short
      Dim unePlume As Pen
      If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
        DemiLargeur = 8  ' pixels
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.SignalFeu).Clone
      Else
        DemiLargeur = 2 ' mm
        Echel = 0.25
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.SignalFeuImpression).Clone
      End If

      ' 16 pixels de cot� pour la boite
      'On cr�e une boite qui ne se dessine pas mais servira � la s�lection
      Dim uneBoite As Boite = Boite.NouvelleBoite(DemiLargeur:=DemiLargeur)
      'D�placer le centre de la boite au point d'insertion souhait�
      uneBoite = CType(uneBoite.Translation(pCentre), Boite)
      mGraphique.Add(uneBoite)

      Dim unInsert As New Insert(mSignal.BlocSignal)
      Dim InsertSignal As Insert
      With unInsert
        .pInsertion = CvPointF(pCentre)
        .rot = CvAngleDegr�s(AngleSymbole)
        .Echx = Echel
        InsertSignal = .TrInsertion(Nothing)
        InsertSignal.AttribuerPlume(unePlume)
        mGraphique.Add(InsertSignal)
      End With

      'Dessiner une ligne de rappel partant de la ligne de feux au point le plus proche de la boite
      Dim LigneRappel As New Ligne(pOriRappel, pCentre, CType(cndPlumes.Plume(Plumes.PlumeEnum.SignalFeuLigneRappel).Clone, Pen))
      If mSignal.EstPi�ton Then
        LigneRappel.pB = CvPoint(CType(InsertSignal.Bloc.PolyArcs(0).Figures(1), Fleche).ptR�f�rence(0))
      End If

      'Ajouter la ligne de rappel
      mGraphique.Add(LigneRappel, Poign�esACr�er:=False)
      LigneRappel.RendreNonS�lectable()

      Dim PiSur4 As Single = Math.PI / 4
      Dim pCoin As Point
      ' On se limite � �crire en dessous ou � droite du symbole
      Select Case AngleForm�(LigneRappel)
        Case -3 * PiSur4 To -PiSur4, PiSur4 To 3 * PiSur4
          pCoin = New Point(pCentre.X + DemiLargeur, pCentre.Y - DemiLargeur)
        Case Else
          pCoin = New Point(pCentre.X - DemiLargeur, pCentre.Y + DemiLargeur)
      End Select

      Dim uneBrosse As SolidBrush
      If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
        uneBrosse = cndPlumes.Brosse(Plumes.BrosseEnum.SignalFeuID).Clone
      Else
        uneBrosse = cndPlumes.Brosse(Plumes.BrosseEnum.SignalFeuIDImpression).Clone
      End If

      Dim unTexte As New Texte(mLigneFeux.ID, uneBrosse, New Font("Arial", 7), pCoin)
      mGraphique.Add(unTexte)

      uneCollection.Add(mGraphique)
      Return mGraphique

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.Cr�erGraphique")
    End Try

  End Function

  Public ReadOnly Property PtR�f�rence() As Point
    Get
      Dim uneLigne As Ligne
      Dim Cot� As Branche.Lat�ralit�

      If mLigneFeux.EstV�hicule Then
        uneLigne = CType(mLigneFeux, LigneFeuV�hicules).Dessin
        If CType(mLigneFeux, LigneFeuV�hicules).AGauche Then
          Cot� = Branche.Lat�ralit�.Droite
        Else
          Cot� = Branche.Lat�ralit�.Gauche
        End If
        Return CvPoint(Formules.intersect(uneLigne, mLigneFeux.mBranche.BordChauss�e(Cot�), Formules.TypeInterSection.Indiff�rent))

      Else
        Dim uneLignePi�tons As LigneFeuPi�tons = mLigneFeux
        Dim uneFl�che As Fleche = uneLignePi�tons.mTravers�e.Fl�che
        Dim numPoint As Short = 0
        Dim SensVoies As Voie.TypeVoieEnum = Voie.TypeVoieEnum.VoieQuelconque
        Dim PremierSignal As Boolean = uneLignePi�tons.mSignalFeu(0) Is Me

        With uneLignePi�tons
          If .mBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
            SensVoies = Voie.TypeVoieEnum.VoieEntrante
          ElseIf .mBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
            SensVoies = Voie.TypeVoieEnum.VoieSortante
          End If

          Select Case SensVoies
            Case Voie.TypeVoieEnum.VoieEntrante
              'Positionner le signal cot� int�rieur de la branche
              If .mBranche.mPassages(0).VersExt�rieurCarrefour Then
                If PremierSignal Then
                  numPoint = 1
                Else
                  numPoint = 2
                End If
              Else
                If PremierSignal Then
                  numPoint = 0
                Else
                  numPoint = 3
                End If
              End If

            Case Voie.TypeVoieEnum.VoieSortante
              'Positionner le signal cot� centre du carrefour carrefour
              If .mBranche.mPassages(0).VersExt�rieurCarrefour Then
                If PremierSignal Then
                  numPoint = 0
                Else
                  numPoint = 3
                End If
              Else
                If PremierSignal Then
                  numPoint = 1
                Else
                  numPoint = 2
                End If
              End If

            Case Else
              'Positionner le signal � gauche
              If Not PremierSignal Then
                'Signal sur le 2� trottoir du passage
                If .mTravers�e.mDouble Then
                  'Le contour est constitu� de 7 points : les 2  1ers sont sur le 1er trottoir,les 2 suivants sont les 2 points interm�diaires
                  numPoint = 4
                Else
                  numPoint = 2
                End If
              End If
          End Select


          'If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
          Return CvPoint(.mTravers�e.Contour.Points(numPoint))
          'Else
          '  If PremierSignal Then
          '    Return CvPoint(uneFl�che.ptR�f�rence(0))
          '  Else
          '    Return CvPoint(uneFl�che.ptR�f�rence(1))
          '  End If
          'End If

        End With

      End If  'LigneV�hicules
    End Get

  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Signal --------------------------
'=====================================================================================================
Public Class Signal
  Public Code As SignalCollection.SignalEnum
  Private mBlocSignal, mBlocSignalSonore As Bloc

  Public Sub New(ByVal CodeFeu As SignalCollection.SignalEnum)

    Code = CodeFeu

    If Code = SignalCollection.SignalEnum.R12 Then
      mBlocSignal = cndBlocsSignaux("R12")
      mBlocSignalSonore = cndBlocsSignaux("R12s")
    Else
      mBlocSignal = cndBlocsSignaux("R11")
    End If

  End Sub

  Public ReadOnly Property BlocSignal() As Bloc
    Get
      If EstPi�ton AndAlso cndVariante.Param.SignalPi�tonsSonore Then
        Return mBlocSignalSonore
      Else
        Return mBlocSignal
      End If
    End Get
  End Property

  Public ReadOnly Property EstPi�ton() As Boolean
    Get
      EstPi�ton = cndSignaux.D�fautPi�ton Is Me
    End Get
  End Property

  Public ReadOnly Property Anticipation() As Boolean
    Get
      Select Case Code
        Case SignalCollection.SignalEnum.R15b To SignalCollection.SignalEnum.R16tg
          Anticipation = True
      End Select

    End Get
  End Property

  Public ReadOnly Property JauneClignotant() As Boolean
    Get
      JauneClignotant = (Code = SignalCollection.SignalEnum.R11j)
    End Get
  End Property

  Public ReadOnly Property strCode() As String
    Get
      Return cndSignaux.strCode(Me)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe SignalCollection --------------------------
'=====================================================================================================
Public Class SignalCollection : Inherits CollectionBase
  Public Enum SignalEnum
    'L'ordre de cette �num�ration ne doit pas �tre chang� : 
    'cf M�thode New de la classe SignalCollection et Anticipation de la classe SignalFeu
    R11   'Tricolore normal
    R12   'Pi�tons
    'Tricolores modaux et directionnels
    R13b  'Bus
    R13c  'Cycles
    R13t  'Tram
    R14d   'TD
    R14dtd  'TD et TAD
    R14dtg  'TD et TAG
    R14td   'TAD
    R14tg   'TAG
    'Signaux d'anticipation
    R15b   'Bus
    R15c   'Cycles
    R15t   'Tram
    R16d   'TD
    R16dtd  'TD et TAD
    R16dtg  'TD et TAG
    R16td   'TAD
    R16tg   'TAG
    'Signaux sp�ciaux
    R11j   'Jaune clignotant � la place du rouge (jaune associ� = mini 5s)
    R24   'Rouge clignotant

  End Enum

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
    Dim unSignal As Signal
    Dim i As SignalEnum

    Cr�erBlocSignal()

    For i = SignalCollection.SignalEnum.R11 To SignalCollection.SignalEnum.R24
      unSignal = New Signal(i)
      Me.Add(unSignal)
    Next

  End Sub

  Private Sub Cr�erBlocSignal()
    Dim unBloc As Bloc

    'Les symboles sont contenus dans une boite 16x16, dont le (0,0) est au centre de la boite
    Dim unePlume As Pen '= cndPlumes.Plume(Plumes.PlumeEnum.SignalFeu)

    'Dessiner un cercle et une fl�che pour symboliser le signal de feu
    ' Cercle : inscrit dans la moiti� gauche de la boite
    Dim unCercle As New Cercle(New Point(-4, 0), 3, unePlume)
    Dim uneFleche As New Fleche(Longueur:=6, HauteurFl�che:=2, unePlume:=unePlume)
    'Positionner la fl�che � droite et la retourner pour qu'elle s'oriente vers la droite
    Dim uneFleche1 As Fleche = uneFleche.RotTrans(New Point(4, 0), Math.PI)
    Dim unPolyarc As New PolyArc
    unPolyarc.Add(uneFleche1)
    unPolyarc.Add(unCercle)

    unBloc = New Bloc("R11")
    unBloc.PolyArcs.Add(unPolyarc)
    cndBlocsSignaux.Add(unBloc)

    'Symbole passage pi�ton
    unPolyarc = New PolyArc
    Dim uneBoite As Boite = Boite.NouvelleBoite(DemiLargeur:=4, unePlume:=unePlume).Translation(New Point(-2, 0))
    unPolyarc.Add(uneBoite)
    Dim uneFleche2 As Fleche = uneFleche.RotTrans(New Point(8, 0), Math.PI)
    unPolyarc.Add(uneFleche2)

    unBloc = New Bloc("R12")
    unBloc.PolyArcs.Add(unPolyarc)
    cndBlocsSignaux.Add(unBloc)

    'Symbole passage pi�ton
    unPolyarc = New PolyArc
    unPolyarc.Add(uneBoite)
    unPolyarc.Add(uneFleche2)
    Dim pTrap�ze(3) As Point
    pTrap�ze(0) = uneBoite.Poign�e(0)
    pTrap�ze(1) = TranslationBase(pTrap�ze(0), New Size(-2, -2))
    pTrap�ze(3) = uneBoite.Poign�e(3)
    pTrap�ze(2) = TranslationBase(pTrap�ze(3), New Size(-2, 2))
    unPolyarc.Add(New PolyArc(pTrap�ze, Clore:=True))

    unBloc = New Bloc("R12s")
    unBloc.PolyArcs.Add(unPolyarc)
    cndBlocsSignaux.Add(unBloc)

  End Sub

  '******************************************************************************
  ' Retourne sous forme de chaine le code du signal de feu
  ' Entr�e : unSignalFeu - Signal de feu dont on recherche le code
  '******************************************************************************
  Public Function strCode(ByVal unSignal As Signal) As String

    If Not IsNothing(unSignal) Then

      Select Case unSignal.Code
        Case SignalEnum.R11
          strCode = "R11"
        Case SignalEnum.R11j
          strCode = "R11j"
        Case SignalEnum.R12
          strCode = "R12"
        Case SignalEnum.R13b
          strCode = "R13b"
        Case SignalEnum.R13c
          strCode = "R13c"
        Case SignalEnum.R13t
          strCode = "R13t"
        Case SignalEnum.R14d
          strCode = "R14d"
        Case SignalEnum.R14dtd
          strCode = "R14dtd"
        Case SignalEnum.R14dtg
          strCode = "R14dtg"
        Case SignalEnum.R14td
          strCode = "R14td"
        Case SignalEnum.R14tg
          strCode = "R14tg"
        Case SignalEnum.R15b
          strCode = "R15b"
        Case SignalEnum.R15c
          strCode = "R15c"
        Case SignalEnum.R15t
          strCode = "R15t"
        Case SignalEnum.R16d
          strCode = "R16d"
        Case SignalEnum.R16dtd
          strCode = "R16dtd"
        Case SignalEnum.R16dtg
          strCode = "R16dtg"
        Case SignalEnum.R16td
          strCode = "R16td"
        Case SignalEnum.R16tg
          strCode = "R16tg"
        Case SignalEnum.R24
          strCode = "R24"
      End Select
    End If

  End Function

  Public ReadOnly Property D�fautPi�ton() As Signal
    Get
      D�fautPi�ton = Item(SignalEnum.R12)
    End Get
  End Property

  Public ReadOnly Property D�fautV�hicule() As Signal
    Get
      D�fautV�hicule = Item(SignalEnum.R11)
    End Get
  End Property

  Public Function strListe(ByVal Anticipation As Boolean, Optional ByVal SansPi�tons As Boolean = False) As String
    Dim unSignal As Signal
    If Anticipation Then strListe = "|" & "<Aucun>"
    For Each unSignal In Me
      If SansPi�tons And unSignal.EstPi�ton Then
      ElseIf unSignal.Anticipation = Anticipation Then
        strListe &= "|" & strCode(unSignal)
      End If
    Next

  End Function

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unSignal As Signal) As Short
    Return Me.List.Add(unSignal)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal Signaux() As Signal)
    Me.InnerList.AddRange(Signaux)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal unSignal As Signal)
    If Me.List.Contains(unSignal) Then
      Me.List.Remove(unSignal)
    End If

  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As SignalEnum) As Signal
    Get
      Return CType(Me.List.Item(Index), Signal)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par le code du feu.
  Default Public ReadOnly Property Item(ByVal unCode As String) As Signal
    Get
      Dim unSignal As Signal
      For Each unSignal In Me.List
        If unSignal.strCode = unCode Then Return unSignal
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unSignal As Signal) As Short
    Return Me.List.IndexOf(unSignal)
  End Function

  ' Methode pour v�rifier si un objet existe d�j� dans la collection.
  Public Function Contains(ByVal unSignal As Signal) As Boolean
    Return Me.List.Contains(unSignal)
  End Function

End Class
