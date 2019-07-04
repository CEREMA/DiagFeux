'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Branche.vb																								'
'						Classes																														'
'							Branche																													'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On 
Imports System.Math

'=====================================================================================================
'--------------------------- Classe Branche --------------------------
'=====================================================================================================
Public Class Branche : Inherits M�tier
  'Branche de carrefour

  Private Const d�fautLongueur As Single = 20.0
  Private Const d�fautLargeurVoies As Single = 3.0

  Public Const miniLongueur As Single = 10  ' 0 est stupide
  Public Const maxiLongueur = 99
  Public Const miniLargeurVoies As Single = 2  ' 0 est stupide
  Public Const maxiLargeurVoies = 9.9
  Public Const miniNbVoies = 0  ' 1 est stupide : sens unique
  Public Const maxiNbVoies = 9

  Private mVariante As Variante

  Private mGraphiqueBoite As New PolyArc
  Private mID As String
  Private mNbEllipses As Short
  Private mPr�f�renceTrafic As Point

  Public Property PR�f�renceTrafic() As Point
    Get
      Return mPr�f�renceTrafic
    End Get
    Set(ByVal Value As Point)
      mPr�f�renceTrafic = Value
    End Set
  End Property

  Public ReadOnly Property XouYR�f�renceTrafic() As Integer
    Get
      Select Case Portion
        Case Formules.Positionnement.Droite, Formules.Positionnement.Gauche
          Return mPr�f�renceTrafic.Y
        Case Else
          Return mPr�f�renceTrafic.X
      End Select
    End Get
  End Property

  Public ReadOnly Property EspacementTrafic() As Short
    Get
      Select Case Portion
        Case Formules.Positionnement.Droite, Formules.Positionnement.Gauche
          Return EspacementV
        Case Else
          Return EspacementH
      End Select
    End Get
  End Property

  Public Function XouYMinMaxTrafic(ByVal unTrafic As Trafic) As Point
    Dim hSur2 As Short
    Dim AvecPi�tons As Boolean = unTrafic.QPi�ton(Me) > 0

    If AvecPi�tons Then
      hSur2 = (mNbEllipses + 1) * EspacementTrafic / 2
    Else
      hSur2 = mNbEllipses * EspacementTrafic / 2
    End If

    Return New Point(XouYR�f�renceTrafic + hSur2, XouYR�f�renceTrafic - hSur2)

  End Function

  Public Property NbEllipses() As Short
    Get
      Return mNbEllipses
    End Get
    Set(ByVal Value As Short)
      mNbEllipses = Value
    End Set
  End Property

  Public Enum Lat�ralit�
    Aucune = -1
    Droite
    Gauche
  End Enum

  'Nom de la rue relative � la branche
  '##ModelId=40322C460157
  Private mNomRue As String

  'Angle de la branche en degr�s dans le sens trigonom�trique(ou direct)
  '##ModelId=40322C73036B
  Public Angle As Short

  'Longueur de la branche : c'est la longueur utile pour le dessin, la longueur �tant a priori illimit�e
  '##ModelId=40322CA1034B
  Public Longueur As Short = d�fautLongueur

  'Largeur commune des voies de la branche
  '##ModelId=40322CE9005D
  Private mLargeurVoies As Single = 3.0

  '##ModelId=3C70D1A1003E
  Public mIlot As Ilot

  '##ModelId=4035DF590290
  Public ptFlechesTrafic() As Drawing.Point

  '##ModelId=3C70D5D000CB
  'Origine de la branche en coordonn�es r�elles
  'Protected mOrigine As Drawing.PointF
  'Origine de la branche en coordonn�es r�elles relatives � l'origine du carrefour
  Protected mOrigineRelative As Drawing.PointF

  Private mExtr�mit�s(1) As PointF
  Private mBordChauss�e(1) As Ligne
  Private mLigneSym�trie As Ligne
  Private mEnveloppeVoiesEntrantes As PolyArc
  Private gSensCirculation As PolyArc

  '##ModelId=403C805D000F
  Private mVoies As New VoieCollection

  '##ModelId=403C81710271
  Public mPassages As New PassageCollection

  Public mCourants As New CourantCollection
  Private CourantTD As Boolean

  Public Function D�terminerCourants() As Branche
    Dim unCourant As Courant
    'Se positionner sur la voie entrante la + � droite : les courants en provenance de la branche sont ordonn�s ainsi
    Dim IndexVoie As Short = NbVoies - 1
    Dim i As Short
    Dim uneLigneFeux, exLigneFeux As LigneFeuV�hicules
    Dim uneVoie As Voie

    If Me.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
      'Analyse sans objet pour cette branche
      Return Nothing
    End If

    Try
      For Each unCourant In mCourants
        If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.Aucun Then
          'Au moins un courant n'a pu �tre affect� lors du traitement pr�alable(cf D�terminerCourants(uneLigneFeux))
          'Le verrouillage des lignes de feux sera refus�
          'Ne doit pas arriver en relecture d'un fichier (sauf fichiers ant�rieurs � DIAGFEUX 3)
          Return Me

        Else
          Select Case unCourant.NatureCourant
            Case TrajectoireV�hicules.NatureCourantEnum.TAG
              unCourant.CoefG�ne = CoefG�neTAG
            Case TrajectoireV�hicules.NatureCourantEnum.TAD
              unCourant.CoefG�ne = CoefG�neTAD
          End Select

          uneLigneFeux = unCourant.LigneFeuxCommande
          With uneLigneFeux
            If .nbVoies = 0 Then
              'N'ajouter les voies qu'au 1er courant command� par la ligne de feux
              'Par ailleurs en relecture, les voies ont d�j� �t� ajout�es
              For i = .NbVoiesTableur - 1 To 0 Step -1  ' les ins�rer en commen�ant par la + � gauche
                .Voies.Add(mVoies(IndexVoie - i))
              Next
              IndexVoie -= .NbVoiesTableur
            End If

            For Each uneVoie In .Voies
              uneVoie.mCourants.Add(unCourant)
            Next
          End With

        End If
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Banche.D�terminerCourants")
    End Try

  End Function

  '************************************************************************************
  'Mode tableur
  ' D�terminer la nature des courants de circulation command�s par une ligne de feux
  ' Cette fonction est appel�e pour chaque ligne de feux de la branche
  '************************************************************************************
  Public Function D�terminerCourants(ByVal uneLigneFeux As LigneFeuV�hicules) As Boolean
    Static TAGSeul, TDSeul, TADSeul, TAGCoupl�, TADCoupl� As Boolean
    Static nbCourantsCoch�s As Short
    Dim TAGSeulLF, TDSeulLF, TADSeulLF, TAGCoupl�LF, TADCoupl�LF As Boolean
    Dim unCourant, unCourantProche As Courant

    If IsNothing(uneLigneFeux) Then
      '1er appel  
      'initialisation des courants
      For Each unCourant In mCourants
        unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.Aucun
      Next

      'initialisation des variables statiques
      TAGSeul = False
      TDSeul = False
      TADSeul = False
      TAGCoupl� = False
      TADCoupl� = False
      CourantTD = False
      nbCourantsCoch�s = 0


    Else
      With uneLigneFeux
        TAGSeulLF = .TAG And Not .TD And Not .TAD
        TDSeulLF = Not .TAG And .TD And Not .TAD
        TADSeulLF = Not .TAG And Not .TD And .TAD
        TAGCoupl�LF = .TAG And (.TD Or .TAD)
        TADCoupl�LF = .TAD And (.TD Or .TAG)
        If .TAG Then nbCourantsCoch�s += 1
        If .TD Then nbCourantsCoch�s += 1
        If .TAD Then nbCourantsCoch�s += 1
      End With

      'Interdiction d'avoir 2 LF avec la m�me nature de courant seul
      If TAGSeul And TAGSeulLF Then Return True
      If TDSeul And TDSeulLF Then Return True
      If TADSeul And TADSeulLF Then Return True

      If TAGCoupl� And TAGCoupl�LF Then Return True
      If TADCoupl� And TADCoupl�LF Then Return True

      If nbCourantsCoch�s > mCourants.Count Then Return True

      TAGSeul = TAGSeul Or TAGSeulLF
      TDSeul = TDSeul Or TDSeulLF
      TADSeul = TADSeul Or TADSeulLF

      TAGCoupl� = TAGCoupl� Or TAGCoupl�LF
      TADCoupl� = TADCoupl� Or TADCoupl�LF

      If TAGSeulLF Then   ' 1)
        unCourant = mCourants(mCourants.Count - 1)
        If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
          'TAG d�j� attribu� pr�c�demment par un TAG coupl� : reporter ce dernier sur le courant pr�c�dent
          unCourantProche = mCourants(mCourants.Count - 2)
          unCourantProche.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG
          unCourantProche.LigneFeuxCommande = unCourant.LigneFeuxCommande
        Else
          unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG
        End If
        unCourant.LigneFeuxCommande = uneLigneFeux

      ElseIf TDSeulLF Then ' 2)
        'Mettre en tout droit tous les courants qui n'ont pas encore de nature
        For Each unCourant In mCourants
          If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.Aucun Then
            unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD
            unCourant.LigneFeuxCommande = uneLigneFeux
          End If
        Next

      ElseIf TADSeulLF Then  ' 3)
        unCourant = mCourants(0)
        If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD Then
          'TAD d�j� attribu� pr�c�demment par un TAD coupl� : reporter ce dernier sur le courant suivant
          unCourantProche = mCourants(1)
          unCourantProche.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD
          unCourantProche.LigneFeuxCommande = unCourant.LigneFeuxCommande
        Else
          unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD
        End If
        unCourant.LigneFeuxCommande = uneLigneFeux

      ElseIf TAGCoupl�LF Then ' 4)
        unCourant = mCourants(mCourants.Count - 1)
        If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
          'TAG d�j� attribu� pr�c�demment par un TAG seul : conserver ce dernier et attribuer le TAG au courant pr�c�dent
          unCourant = mCourants(mCourants.Count - 2)
        End If
        unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG
        unCourant.LigneFeuxCommande = uneLigneFeux

        If uneLigneFeux.TAD Then
          'TAG + TAD (et �ventuellement TD)
          unCourant = mCourants(0)
          If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD Then
            'TAD d�j� attribu� pr�c�demment par un TAD seul : attribuer le TAD au courant suivant
            unCourant = mCourants(1)
          End If
          unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD
          unCourant.LigneFeuxCommande = uneLigneFeux
        End If

        If uneLigneFeux.TD Then
          'TAG + TD (et �ventuellement TAD : trait� juste avant)
          'Mettre en tout droit tous les courants qui n'ont pas encore de nature
          For Each unCourant In mCourants
            If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.Aucun Then
              unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD
              unCourant.LigneFeuxCommande = uneLigneFeux
            End If
          Next
        End If

      ElseIf TADCoupl�LF Then ' 5)
        'TAD + TD (le TAG + TAD a �t� trait� au cas 4))

        'TAD
        unCourant = mCourants(0)
        If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD Then
          'TAD d�j� attribu� pr�c�demment par un TAD seul : attribuer le TAD au courant suivant
          unCourant = mCourants(1)
        End If
        unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD
        unCourant.LigneFeuxCommande = uneLigneFeux

        'TD
        'Mettre en tout droit tous les courants qui n'ont pas encore de nature
        For Each unCourant In mCourants
          If unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.Aucun Then
            unCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD
            unCourant.LigneFeuxCommande = uneLigneFeux
          End If
        Next

      End If

    End If

  End Function
  Public Property NomRue() As String
    Get
      Return mNomRue
    End Get
    Set(ByVal Value As String)
      mNomRue = Value
    End Set
  End Property

  Public ReadOnly Property Origine() As PointF
    Get
      Return Translation(CentreCarrefour, mOrigineRelative)
    End Get
  End Property

  Public Sub AttribuerOrigine(ByVal pO As PointF)
    'Origine Absolue
    mOrigineRelative.X = pO.X - CentreCarrefour.X
    mOrigineRelative.Y = pO.Y - CentreCarrefour.Y
  End Sub


  Public Sub AttribuerOrigineRelative(ByVal pORelatif As PointF)
    'Origine relative � celle du carrefour
    mOrigineRelative = pORelatif
  End Sub

  Public ReadOnly Property OrigineRelative() As PointF
    Get
      Return mOrigineRelative
    End Get
  End Property

  Public ReadOnly Property AvecIlot() As Boolean
    Get
      Return Not IsNothing(mIlot)
    End Get
  End Property

  Public ReadOnly Property SensUnique(ByVal TypeVoie As Voie.TypeVoieEnum) As Boolean
    Get
      'Sens unique entrant ou sortant : Si le nombre de voies de la branche=le nombre de voies entrantes ou le nombre de voies sortantes
      Return NbVoies(TypeVoie) = NbVoies
    End Get
  End Property

  Public Property ID() As String
    Get
      ID = mID
    End Get
    Set(ByVal Value As String)
      If IsNothing(mID) Then
        'N'attribuer l'ID que lors de l'initialisation  du carrefour (la branche peut + tard �tre affect�e � une autre collection qui ne doit pas affecter cet ID)
        mID = Value
      End If
    End Set
  End Property

  Public Sub New(ByVal uneVariante As Variante)
    mVariante = uneVariante
    'Par d�faut, on cr�e une voie entrante et une voie sortante
    ' La branche �tant orient�e depuis l'int�rieur du carrefour vers l'et�rieur, les voies sortantes viennet en premier
    mVoies.Add(Entrante:=False, uneBranche:=Me)
    '  If uneVariante.ModeGraphique Then
    'AV : 13/08/07 : Fianlement, mour le mode tableur, les voies entrantes seront d�termin�es par les lignes de feux
    mVoies.Add(Entrante:=True, uneBranche:=Me)
    ' End If
  End Sub

  Public ReadOnly Property Variante() As Variante
    Get
      Return mVariante
    End Get
  End Property

  Private ReadOnly Property CentreCarrefour() As PointF
    Get
      Return mVariante.mCarrefour.mCentre
    End Get
  End Property

  Public Sub New(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow, ByVal uneVariante As Variante)
    Dim i As Short
    Dim unPassage As PassagePi�ton

    mVariante = uneVariante

    With uneRowBranche
      'Propri�t�s de la branche
      Angle = .Angle
      LargeurVoies = .LargeurVoies
      Longueur = .Longueur
      mNomRue = .NomRue

      'Ilot �ventuel
      If .GetIlotRows.Length = 1 Then
        Dim unIlotRow As DataSetDiagfeux.IlotRow = .GetIlotRows(0)
        With unIlotRow
          mIlot = New Ilot(Me, .Largeur, .Rayon, .D�calage, .Retrait)
        End With
      End If

      'Cr�ation des voies
      For i = 0 To .GetVoieRows.Length - 1
        Dim uneRowVoie As DataSetDiagfeux.VoieRow = .GetVoieRows(i)
        'M�moriser l'ID ???
        mVoies.Add(uneRowVoie.Entrante, Me)
      Next

      'Cr�ation des passages pi�tons
      For i = 0 To .GetPassageRows.Length - 1
        unPassage = New PassagePi�ton(Me, .GetPassageRows(i))
        mPassages.Add(unPassage)
      Next

      'Point Origine de la branche
      With .GetOrigineRows(0)
        AttribuerOrigineRelative(New PointF(.X, .Y))
      End With

      'Positionnement des fleches de trafic sur le graphique
      For i = 0 To .GetptFlechesTraficRows.Length - 1
        ReDim ptFlechesTrafic(i)
        ptFlechesTrafic(i).X = .GetptFlechesTraficRows(i).X
        ptFlechesTrafic(i).Y = .GetptFlechesTraficRows(i).Y
      Next
    End With

  End Sub

  'Retourne le nombre de voies entrantes, sortantes ou totales de la branche
  '##ModelId=4033166A036B
  Public Property NbVoies(Optional ByVal TypeVoie As Voie.TypeVoieEnum = Voie.TypeVoieEnum.VoieQuelconque) As Short

    Get
      If TypeVoie = Voie.TypeVoieEnum.VoieQuelconque Then
        Return mVoies.Count
      Else
        Dim uneVoie As Voie
        For Each uneVoie In mVoies
          Select Case TypeVoie
            Case Voie.TypeVoieEnum.VoieEntrante
              If uneVoie.Entrante Then NbVoies += 1
            Case Voie.TypeVoieEnum.VoieSortante
              If Not uneVoie.Entrante Then NbVoies += 1
          End Select
        Next
      End If
    End Get

    Set(ByVal Value As Short)
      Dim nb As Short = Value - NbVoies(TypeVoie)
      Dim i As Short
      Select Case Sign(nb)
        'Ajouter les voies manquantes
      Case 1
          For i = 1 To nb
            mVoies.Add(Entrante:=(TypeVoie = Voie.TypeVoieEnum.VoieEntrante), uneBranche:=Me)
          Next
        Case -1
          'Supprimer les voies en trop
          Dim uneVoie As Voie
          Do Until i = nb
            For Each uneVoie In mVoies
              If uneVoie.Entrante Xor TypeVoie = Voie.TypeVoieEnum.VoieSortante Then
                mVoies.Remove(uneVoie)
                i -= 1
                Exit For
              End If
            Next
          Loop
      End Select
    End Set

  End Property

  Public ReadOnly Property Voies() As VoieCollection
    Get
      Return mVoies
    End Get
  End Property

  Public Property LargeurVoies() As Single
    Get
      Return mLargeurVoies
    End Get
    Set(ByVal Value As Single)
      mLargeurVoies = Value
    End Set
  End Property

  Public Function Largeur() As Single
    Return mVoies.Count * LargeurVoies
  End Function

  Public Sub RecalerPassagesPi�tons(ByVal Diff�rence As Single)
    Dim unPassage As PassagePi�ton

    'Si 2 passages pi�tons, l'algorithme est + complexe
    ' Pour bien faire, il faudrait m�moriser ou reconnaitre le cot� qui est proche du bord de chauss�e et ne modifier que celui-l�(dans Recaler)
    If mPassages.Count = 1 Then
      For Each unPassage In mPassages
        unPassage.Recaler(Diff�rence)
      Next
    End If
  End Sub

  Public Sub D�terminerVoiesPassages()
    Dim unPassage As PassagePi�ton
    If mPassages.Count = 1 Then
      For Each unPassage In mPassages
        unPassage.D�terminerVoies()
      Next
    End If
  End Sub

  '********************************************************************************************************************
  ' Retourne l'objet graphique Bord de chauss�e droite ou gauche (en coordonn�es dessin)
  ' La Bord de chauss�e droite est le le cot� voie sortante (branche orient�e depuis le centre du carrefour vers la sortie)
  '********************************************************************************************************************
  Public Function BordChauss�e(ByVal Cot� As Lat�ralit�) As Ligne

    Return mBordChauss�e(Cot�)
  End Function

  '********************************************************************************************************************
  ' Retourne l'extr�mit� du Bord de chauss�e droite ou gauche (en coordonn�es dessin) 
  '   avant qu'il ne soit tronqu� par le raccordement
  '********************************************************************************************************************
  Public ReadOnly Property Extr�mit�BordChauss�e(ByVal Cot� As Lat�ralit�) As Point
    Get
      With mExtr�mit�s(Cot�)
        Return New Point(.X, .Y)
      End With
    End Get
  End Property

  Public Function PtInt�rieur(ByVal p As Point) As Boolean
    Dim p1, p2, pOrigine1, pOrigine2 As Point

    pOrigine1 = BordChauss�e(Branche.Lat�ralit�.Droite).pA
    pOrigine2 = BordChauss�e(Branche.Lat�ralit�.Gauche).pA
    'V�rifier que tous les points sont � l'int�rieur de la branche (c'est � dire entre les 2 bords de chauss�e)
    'projeter le point sur le bord de chauss�e droite
    p1 = Projection(p, pOrigine1, AngleEnRadians)
    If Distance(p, p1) = 0.0 Then
      'Le point est sur le bord de chauss�e droite
      PtInt�rieur = True
    Else
      'projeter le point sur le bord de chauss�e gauche
      p2 = Projection(p, pOrigine2, AngleEnRadians)
      If Distance(p, p2) = 0.0 Then
        'Le point est sur le bord de chauss�e gauche
        PtInt�rieur = True
      Else
        PtInt�rieur = (Sign(AngleForm�(p1, p)) <> Sign(AngleForm�(p2, p)))
        'si le point est � l'int�rieur de la branche les angles sont de signe oppos�
      End If
    End If

  End Function

  Public Function BordChauss�eProche(ByRef p As Point) As Lat�ralit�
    Dim pProjet�, pOrigine As Point
    Dim DistanceUtile As Single
    Dim BordChauss� As Ligne
    Dim DistanceBordChauss�e As Single = 0.5 * Echelle ' D�part � 50 cm

    'V�rifier que le point est � l'int�rieur de la branche (c'est � dire entre les 2 bords de chauss�e)
    If PtInt�rieur(p) Then
      DistanceUtile = LargeurVoies / 2 * Echelle
    Else
      ' Rechercher si le point cliqu� � l'ext�rieur de la branche n'est pas malgr� tout tr�s proche
      DistanceUtile = 3
    End If

    'projeter le point sur le bord de chauss�e droite
    pOrigine = Me.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite)
    pProjet� = Projection(p, pOrigine, AngleEnRadians)
    If Distance(pProjet�, p) < DistanceUtile Then
      p = PointPosition(pProjet�, DistanceBordChauss�e, AngleEnRadians - sngPI / 2)
      Return Lat�ralit�.Droite
    Else
      'projeter le point sur le bord de chauss�e gauche
      pOrigine = Me.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche)
      pProjet� = Projection(p, pOrigine, AngleEnRadians)
      If Distance(pProjet�, p) < DistanceUtile Then
                p = PointPosition(pProjet�, DistanceBordChauss�e, AngleEnRadians + sngPI / 2)
        Return Lat�ralit�.Gauche
      End If
    End If

    Return Lat�ralit�.Aucune

  End Function

  Public Function VoieProche(ByVal p As Point) As Voie
    Dim uneVoie As Voie

    For Each uneVoie In mVoies
      If uneVoie.PtInt�rieur(p, Strict:=False) Then
        Return uneVoie
      End If
    Next

  End Function

  '********************************************************************************************************
  'AngleAnRadians : retourne l'angle en radians de la branche compris entre ]-pi et pi]
  '  dans le sens trigo 
  '********************************************************************************************************
  Public ReadOnly Property AngleEnRadians(Optional ByVal InverserSens As Boolean = True) As Single
    Get
      AngleEnRadians = EqvRadian(Angle, InverserSens:=InverserSens)
    End Get
  End Property

  '***********************************************************************************
  ' Retourne dans le rep�re g�n�ral le point p fourni dans le rep�re de la branche
  '***********************************************************************************
  Public Function PtRep�reG�n�ral(ByVal p As PointF) As PointF
    Return RotTrans(p, Origine, CvAngleRadians(Angle))
  End Function

  Public Function IndexOfVoie(ByVal uneVoie As Voie) As Short
    'Retourne le num�ro d'ordre de la voie dans l'ensemble des voies entrantes ou des voies sortantes
    IndexOfVoie = mVoies.IndexOf(uneVoie)
    If uneVoie.Entrante Then
      IndexOfVoie -= NbVoies(Voie.TypeVoieEnum.VoieSortante)
    End If
  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim uneLigne, uneLigne1 As Ligne
    Dim pOrigine As PointF = PointDessinF(Origine)
    Dim pExtr�mit� As PointF = pExtr�mit�Dessin()

    Dim unPassage As PassagePi�ton
    Dim numLigneVoie As Short
    Dim nb As Short = mVoies.Count
    Dim pDessin As PointF
    Dim VoiePointill�e(NbVoies) As Boolean
    Dim unePlume As Pen
    Dim unePlumeAxe As Pen
    Dim unePlumeBordChauss�e As Pen = cndPlumes.Plume(Plumes.PlumeEnum.BrancheBordChauss�e).Clone
    Dim unePlumeVoie As Pen = cndPlumes.Plume(Plumes.PlumeEnum.BrancheVoie).Clone
    Dim unePlumeS�parVoie As Pen = cndPlumes.Plume(Plumes.PlumeEnum.BrancheS�parVoie).Clone
    'Affichage du sens de circulation : 1 triangle plein pour l'ensemble des voies entrantes (et 1 pour les sortantes)
    'milEntrant,milSortant : pointes du triangle
    Dim milEntrant, milSortant As Point
    'hSensVoie : Hauteur du triangle
    Dim hSensVoie As Short

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      hSensVoie = 12
    Else
      hSensVoie = 3
    End If

    If (nb Mod 2) = 1 Then
      unePlumeAxe = cndPlumes.Plume(Plumes.PlumeEnum.BrancheAxe).Clone
    End If

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetM�tier = Me

    'Axe de sym�trie de la branche
    '-----------------------------
    'coordonn�es graphiques(pixels)dans le syst�me Windows
    mLigneSym�trie = New Ligne(pOrigine, pExtr�mit�, unePlumeAxe)

    If cndFlagImpression <> dlgImpressions.ImpressionEnum.Matrice Then
      mGraphique.Add(mLigneSym�trie, Poign�esACr�er:=True)
    End If

    'Dessin des voies
    '-----------------------------
    Dim nbSortant As Short = NbVoies(Voie.TypeVoieEnum.VoieSortante)
    Dim nbEntrant As Short = NbVoies(Voie.TypeVoieEnum.VoieEntrante)

    'D�terminer les lignes s�paratrices de voie de m�me sens
    For numLigneVoie = 1 To nbSortant - 1
      VoiePointill�e(numLigneVoie) = True
    Next
    For numLigneVoie = 1 To NbVoies(Voie.TypeVoieEnum.VoieEntrante) - 1
      VoiePointill�e(numLigneVoie + nbSortant) = True
    Next

    'Dessiner les lignes s�paratrices des voies
    ' Dessin des voies sortantes, puis des voies entrantes
    For numLigneVoie = 0 To nb
      pDessin = PointPosition(pOrigine, (numLigneVoie - nb / 2) * LargeurVoies * Echelle, Angle + 90, SensHoraire:=False)
      If VoiePointill�e(numLigneVoie) Then
        unePlume = unePlumeS�parVoie
      ElseIf numLigneVoie = 0 Or numLigneVoie = nb Then
        unePlume = unePlumeBordChauss�e
      Else
        'Trait normal, s�parant les voies de sens contraire
        unePlume = unePlumeVoie
      End If

      uneLigne = New Ligne(pDessin, PointPosition(pDessin, Longueur * Echelle, Angle, SensHoraire:=False), unePlume)

      mGraphique.Add(uneLigne, Poign�esACr�er:=False)

      'Rechercher le milieu de chaque groupe de voies (entrantes et sortantes)
      If numLigneVoie = 0 Then
        'Bord droit de la branche
        If Not SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
          'au moins une sortie : m�moriser le point extr�me de la voie la + � droite comme extr�me des voies sortantes 
          milSortant = uneLigne.pB
        Else
          'm�moriser le point extr�me de la voie la + � droite comme extr�me des voies entrantes
          milEntrant = uneLigne.pB
        End If

      ElseIf numLigneVoie = NbVoies(Voie.TypeVoieEnum.VoieSortante) Then
        'Bord gauche de la derni�re voie sortante
        If Not milSortant.IsEmpty Then
          milSortant = Milieu(milSortant, uneLigne.pB)
        End If
        If Not SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
          'au moins une entr�e :m�moriser le point extr�me de la voie la + � droite comme extr�me des voies entrantes
          milEntrant = uneLigne.pB
        End If

      ElseIf numLigneVoie = nb Then
        'Bord gauche de la branche
        If Not milEntrant.IsEmpty Then
          milEntrant = Milieu(milEntrant, uneLigne.pB)
        End If
      End If

      If unePlume Is unePlumeBordChauss�e Then
        'Il faut m�moriser � part les extr�mit�s logiques car les bords de chauss�e sont susceptibles d'�tre rogn�s lors du raccordement de branches
        If numLigneVoie = 0 Then
          mBordChauss�e(Lat�ralit�.Droite) = uneLigne
          mExtr�mit�s(Lat�ralit�.Droite) = uneLigne.pAF
        Else
          mBordChauss�e(Lat�ralit�.Gauche) = uneLigne
          mExtr�mit�s(Lat�ralit�.Gauche) = uneLigne.pAF
        End If

      ElseIf cndFlagImpression = dlgImpressions.ImpressionEnum.Matrice Then
        uneLigne.Invisible = True
      End If

      'M�moriser dans l'objet Voie les 2 lignes qui la d�limitent
      If numLigneVoie > 0 Then
        mVoies(numLigneVoie - CType(1, Short)).Cr�erGraphique(uneLigne1, uneLigne)
      End If
      'M�moriser ce 1er cot� de la voie courante comme �tant le 2�me cot� de la voie suivante
      uneLigne1 = uneLigne.Clone
    Next

    ' D�terminer l'enveloppe des voies entrantes
    '-------------------------------------------
    If Not SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
      Dim px(3) As Point
      px(0) = uneLigne1.pA
      px(1) = uneLigne1.pB
      uneLigne = mVoies(nbSortant).Bordure(Lat�ralit�.Droite)
      px(2) = uneLigne.pB
      px(3) = uneLigne.pA
      'mEnveloppeVoiesEntrantes = New PolyArc(px, Clore:=True)
      mEnveloppeVoiesEntrantes = ClearGraphique(Nothing, mEnveloppeVoiesEntrantes, px)
    Else
      mEnveloppeVoiesEntrantes = Nothing
    End If

    ' D�terminer les triangles indiquant les voies entrantes et sortantes
    '--------------------------------------------------------------------------
    Dim pxx(2) As Point
    pxx(0).X = 0
    pxx(0).Y = 0
    pxx(1).X = -hSensVoie / 3
    pxx(1).Y = hSensVoie
    pxx(2).X = hSensVoie / 3
    pxx(2).Y = hSensVoie

    Dim mg As New PolyArc(pxx, Clore:=True)
    gSensCirculation = ClearGraphique(Nothing, gSensCirculation)

    If Not milEntrant.IsEmpty Then
      'Triangle marquant les voies entrantes
            milEntrant = PointPosition(milEntrant, hSensVoie, Me.AngleEnRadians + sngPI)
            Dim mgEntrant As PolyArc = mg.RotTrans(milEntrant, Me.AngleEnRadians + 3 * sngPI / 2)
      gSensCirculation.Add(mgEntrant, Poign�esACr�er:=False)
      mgEntrant.APeindre = True
      ' mgEntrant.Brosse = New SolidBrush(Color.Cyan)
    End If

    If Not milSortant.IsEmpty Then
      'Triangle marquant les voies sortantes
            Dim mgSortant As PolyArc = mg.RotTrans(milSortant, Me.AngleEnRadians + sngPI / 2)
      gSensCirculation.Add(mgSortant, Poign�esACr�er:=False)
      mgSortant.APeindre = True
      'mgSortant.Brosse = New SolidBrush(Color.Cyan)
    End If
    mGraphique.Add(gSensCirculation)

    mg.Clear()
    mg = Nothing

    'Nom de la rue
    '-----------------------------
    If cndFlagImpression = dlgImpressions.ImpressionEnum.PlanCarrefour Then
      Dim D�calage As Short
      If AlignementTexte() = StringAlignment.Center Then
        D�calage = 8
      Else
        D�calage = 5
      End If
      pExtr�mit�.Y -= 2
      mGraphique.Add(TexteNomRue(CvPoint(pExtr�mit�), D�calage))
    End If

    uneCollection.Add(mGraphique)

    ' Indiquer l'ID de la branche (A, B,....) dans une boite
    '-------------------------------------------------------
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Cr�erBoiteID(uneCollection)
    End If

    If cndFlagImpression <> dlgImpressions.ImpressionEnum.Matrice And cndFlagImpression <> dlgImpressions.ImpressionEnum.DiagrammePhases Then
      ' Ilot
      '-----------------------------
      If Not IsNothing(mIlot) Then
        mIlot.Cr�erGraphique(uneCollection)
      End If

      'Passages pi�tons
      '-----------------------------
      For Each unPassage In mPassages
        unPassage.Cr�erGraphique(uneCollection)
      Next
    End If

    Return mGraphique

  End Function

  Public Function MouvementPossible(ByVal pEnCours As Point) As frmCarrefour.CommandeGraphique
    Dim uneLigne As Ligne = LigneDeSym�trie
    Dim PointProche As Point
    Dim uneCommande As frmCarrefour.CommandeGraphique

    If Distance(pEnCours, uneLigne.pA) < Distance(pEnCours, uneLigne.pB) Then
      PointProche = uneLigne.pA
      uneCommande = frmCarrefour.CommandeGraphique.OrigineBranche
    Else
      PointProche = uneLigne.pB
      uneCommande = frmCarrefour.CommandeGraphique.AngleBranche
    End If

    If Distance(PointProche, pEnCours) >= RayS�lect Then
      uneCommande = frmCarrefour.CommandeGraphique.AucuneCommande
    End If

    Return uneCommande
  End Function

  Public Function TexteNomRue(ByVal pExtr�mit� As Point, ByVal Distance As Short) As Texte

    'Nom de la rue

    Dim PositionTexte As Point = PointPosition(pExtr�mit�, Distance, Me.Angle, False)
    Dim unTexte As New Texte(Me.NomRue, New SolidBrush(Color.Black), New Font("Arial", 8), PositionTexte, unAlignement:=AlignementTexte)

    Return unTexte

  End Function

  Public Function pExtr�mit�Dessin() As PointF
    'pExtr�mit�Dessin = PointPosition(PointDessin(Origine), Longueur * Echelle, AngleEnDegr�s:=Angle, SensHoraire:=False)
    pExtr�mit�Dessin = PointDessinF(PointPosition(Origine, Longueur, AngleEnDegr�s:=Angle, SensHoraire:=True))

  End Function

  Public Sub AffecterPReferenceTrafic()
    Dim Cadre As Rectangle = cndParamDessin.ZoneGraphique
    Dim uneLigneBord, uneLigneAxe As Ligne
    Dim MargeHorizontale As Short = 15
    Dim MargeVerticale As Short = 30

    With Cadre
      Select Case Portion
        Case Formules.Positionnement.Bas
          uneLigneBord = New Ligne(New Point(0, .Bottom - MargeHorizontale), New Point(100, .Bottom - MargeHorizontale))
        Case Formules.Positionnement.Droite
          uneLigneBord = New Ligne(New Point(.Right - MargeVerticale, 0), New Point(.Right - MargeVerticale, 100))
        Case Formules.Positionnement.Gauche
          uneLigneBord = New Ligne(New Point(.Left + MargeVerticale, 0), New Point(.Left + MargeVerticale, 100))
        Case Formules.Positionnement.Haut
          uneLigneBord = New Ligne(New Point(0, .Top + MargeHorizontale), New Point(100, .Top + MargeHorizontale))
      End Select
    End With

    uneLigneAxe = New Ligne(PointDessinF(Origine), pExtr�mit�Dessin)
    PR�f�renceTrafic = CvPoint(intersect(uneLigneAxe, uneLigneBord, Formules.TypeInterSection.Indiff�rent))

  End Sub

  Public ReadOnly Property AlignementTexte() As StringAlignment
    Get
      Return Formules.AlignementTexte(AngleEnRadians)
    End Get
  End Property

  Friend ReadOnly Property Portion() As Formules.Positionnement
    Get
      Return Formules.Portion(AngleEnRadians)
    End Get
  End Property

  Public Sub DessinerTrafics(ByVal uneCollection As Graphiques)
    Dim unCourant As Courant
    Dim vTrafic(TrajectoireV�hicules.NatureCourantEnum.TAG) As Short
    Dim unPolyarc As New PolyArc

    Dim pR�f�rence As Point = Extr�mit�BordChauss�e(Lat�ralit�.Gauche)
    Dim uneLigne As Ligne
    Dim unePlume As New Pen(Color.Black, 2)
    Dim p1, p2 As Point
    p1 = pR�f�rence

    For Each unCourant In mCourants
      p1 = RotTrans(p1, New Point(0, 16), AngleEnRadians - sngPI / 2)
      uneLigne = New Ligne(p1, PointPosition(p1, 50, AngleEnRadians), unePlume)
      '    vTrafic(unCourant.NatureCourant) = unCourant.Trafic
      unPolyarc.Add(uneLigne, Poign�esACr�er:=False)
    Next

    uneCollection.Add(unPolyarc)

  End Sub

  Public ReadOnly Property EnveloppeVoiesEntrantes() As PolyArc
    Get
      Return mEnveloppeVoiesEntrantes
    End Get
  End Property

  Public ReadOnly Property BordVoiesEntrantes(ByVal Index As Lat�ralit�) As Ligne
    Get
      If Index = Lat�ralit�.Droite Then
        Return mVoies(NbVoies(Voie.TypeVoieEnum.VoieSortante)).Bordure(Lat�ralit�.Droite)
      Else
        Return mVoies(NbVoies - 1).Bordure(Lat�ralit�.Gauche)
      End If
    End Get
  End Property

  Public Sub AfficherSens(ByVal Value As Boolean)
    If Not IsNothing(gSensCirculation) Then
      gSensCirculation.Invisible = Not Value
    End If
  End Sub

  Private Sub Cr�erBoiteID(ByVal uneCollection As Graphiques)
    Dim unePlume As New Pen(Color.Black)
    Dim pExtr�mit� As Point = LigneDeSym�trie.pB

    mGraphiqueBoite = ClearGraphique(uneCollection, mGraphiqueBoite)

    Dim DemiLargeur As Short = 8 ' 16 pixels de cot� pour la boite
    'D�placer le centre de la boite � 16 pixels de l'extr�mit� de la branche
    Dim pCentre As Point = PointPosition(pExtr�mit�, 2 * DemiLargeur, Angle, SensHoraire:=False)
    'V12 : remplacement de la couleur rouge par la couleur bleue pour l'ID de la branche)
    mGraphiqueBoite.Cr�erBoiteTexte(pCentre, DemiLargeur, mID, New SolidBrush(Color.Blue), unePlume)
    uneCollection.Add(mGraphiqueBoite)

  End Sub

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    Dim unPassage As PassagePi�ton

    Try

      mGraphique.RendreS�lectable(Not Verrouillage, mLigneSym�trie)

      If IsNothing(mVariante.mFondDePlan) Then
        mLigneSym�trie.Invisible = Verrouillage
      Else
        'Masquer les �l�ments de la branche uniquement si le fond de plan est visible
        mGraphique.Invisible = Verrouillage And mVariante.mFondDePlan.Visible
      End If

      If Not IsNothing(mIlot) Then
        mIlot.Verrouiller(Verrouillage)
      End If

      For Each unPassage In mPassages
        unPassage.Verrouiller(Verrouillage)
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Branche.Verrouiller")
    End Try

  End Sub

  Public Sub Masquer()
    'Masquer la structure de la branche (lignes d�limitant les voies)
    mGraphique.Invisible = True

    'Masquer les passages pi�tons
    Dim unPassage As PassagePi�ton

    For Each unPassage In mPassages
      unPassage.mGraphique.Invisible = True
    Next

    'Masquer l'ilot
    If Not IsNothing(mIlot) Then
      mIlot.mGraphique.Invisible = True
    End If

  End Sub

  Public Function RecherPassage(ByVal p As Point) As PassagePi�ton
    Dim unPassage As PassagePi�ton
    For Each unPassage In mPassages
      If unPassage.PtInt�rieur(p) Then
        Return unPassage
      End If
    Next
  End Function

  Public Sub SupprimerIlot(ByVal uneCollection As Graphiques)

    'Supprimer l'ilot des objets graphiques
    ClearGraphique(uneCollection, mIlot.mGraphique)
    mIlot = Nothing

  End Sub

  '********************************************************************************************************************
  ' Retourne l'objet graphique Ligne de sym�trie (en coordonn�es dessin)
  '********************************************************************************************************************
  Public ReadOnly Property LigneDeSym�trie() As Ligne
    Get
      Return mLigneSym�trie
    End Get
  End Property

  '********************************************************************************************************************
  ' Enregistrer la Branche dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim p As Drawing.Point

    Try

      Dim uneRowBranche As DataSetDiagfeux.BrancheRow
      'Ajouter une enregistrement dans la table des Branches
      uneRowBranche = ds.Branche.AddBrancheRow(Angle, LargeurVoies, Longueur, NomRue, uneRowVariante)
      'Ajouter l'origine de la branche
      ds.Origine.AddOrigineRow(mOrigineRelative.X, mOrigineRelative.Y, uneRowBranche)

      'Ajouter �ventuellement le positionnement des fleches de trafic sur la branche
      With uneRowBranche
        If .GetptFlechesTraficRows.Length = 0 Then
          If Not IsNothing(ptFlechesTrafic) Then
            p = ptFlechesTrafic(0)
            ds.ptFlechesTrafic.AddptFlechesTraficRow(p.X, p.Y, uneRowBranche)
          End If
        Else
          If IsNothing(ptFlechesTrafic) Then
            ds.ptFlechesTrafic.RemoveptFlechesTraficRow(.GetptFlechesTraficRows(0))
          Else
            With .GetptFlechesTraficRows(0)
              .X = Origine.X
              .Y = Origine.Y
            End With
          End If
        End If
      End With

      'Ilot
      If IsNothing(mIlot) Then
        If uneRowBranche.GetIlotRows.Length = 1 Then ds.Ilot.RemoveIlotRow(uneRowBranche.GetIlotRows(0))
      Else
        mIlot.Enregistrer(uneRowBranche)
      End If

      mVoies.Enregistrer(uneRowBranche)

      mPassages.Enregistrer(uneRowBranche)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Enregistrement de la branche " & NomRue)
    End Try

  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe BrancheCollection --------------------------
'=====================================================================================================
Public Class BrancheCollection : Inherits CollectionBase

  Public mGraphique As PolyArc
  Public EnveloppeCarrefour As Surface
  Private mVariante As Variante

  ' Cr�er une instance la collection
  Public Sub New()

  End Sub
  Public Sub New(ByVal uneVariante As Variante)
    MyBase.New()
    mVariante = uneVariante
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal uneBranche As Branche) As Short
    Return Me.List.Add(uneBranche)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Branche)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal uneBranche As Branche)
    If Me.List.Contains(uneBranche) Then
      Me.List.Remove(uneBranche)
    End If
  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneBranche As Branche)
    Me.List.Insert(Index, uneBranche)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Branche
    Get
      Return CType(Me.List.Item(Index), Branche)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par le code de la branche : A,B,C....
  Default Public ReadOnly Property Item(ByVal Nom As Char) As Branche
    Get
      Item = Me.Item(Asc(Nom) - 65)
    End Get
  End Property

  Public Function IndexOf(ByVal uneBranche As Branche) As Short
    Return Me.List.IndexOf(uneBranche)
  End Function

  ' Method to check if a person object already exists in the collection.
  Public Function Contains(ByVal uneBranche As Branche) As Boolean
    Return Me.List.Contains(uneBranche)
  End Function

  Public ReadOnly Property ID(ByVal uneBranche As Branche) As Char
    'Pour IndexOf=0 Chr(65) = "A", et les lignes suivantes seront "B","C"...		
    Get
      ID = Chr(65 + IndexOf(uneBranche))
    End Get
  End Property

  Public Function IlotBranche(ByVal IndexCherch� As Short) As Ilot
    Dim uneBranche As Branche
    Dim unIlot As Ilot
    Dim Index As Short

    For Each uneBranche In Me
      unIlot = uneBranche.mIlot
      If Not IsNothing(unIlot) Then
        Index += 1
        If Index = IndexCherch� Then Return unIlot
      End If
    Next

  End Function

  Public Function IndexIlot(ByVal unIlot As Ilot) As Short
    Dim Index As Short
    Dim uneBranche As Branche

    For Each uneBranche In Me
      If uneBranche.AvecIlot Then
        Index += 1
        If uneBranche.mIlot Is unIlot Then Return Index
      End If
    Next

  End Function

  Public Function Suivante(ByVal uneBranche As Branche) As Branche
    Return Item((IndexOf(uneBranche) + 1) Mod Count)
  End Function

  Public Function Pr�c�dente(ByVal uneBranche As Branche) As Branche
    Return Item((IndexOf(uneBranche) + Count - 1) Mod Count)
  End Function

  Public Function NbLignesFeuxMini() As Short
    Dim uneBranche As Branche

    For Each uneBranche In Me
      If Not uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then NbLignesFeuxMini = NbLignesFeuxMini + 1
    Next
    Return NbLignesFeuxMini

  End Function

  Public Function D�terminerCourants() As Branche
    Dim uneBranche As Branche

    For Each uneBranche In Me
      uneBranche = uneBranche.D�terminerCourants()
        If Not IsNothing(uneBranche) Then Return uneBranche
    Next

  End Function

  Public Sub InitialiserCourants()
    Dim uneBranche As Branche

    For Each uneBranche In Me
      uneBranche.D�terminerCourants(Nothing)
    Next

  End Sub

  Private Sub essai(ByVal uneCollection As Graphiques)
    Dim RCarr� As Double
    Dim xF As Double
    Dim yF As Double
    Dim xH As Double
    Dim yH As Double
    Dim resultat As Double
    Dim unePlume As New Pen(Color.Green)

    RCarr� = 17.6548 ^ 2
    xF = -20
    yF = -30
    xH = 8.0129
    yH = -15.7317

    Dim l1 As New Ligne(New PointF(xF, yF), New PointF(xH, yH), unePlume)
    Dim l2 As New Ligne(New PointF(xF, yF), New PointF(0, 0), unePlume)
    Dim l3 As New Ligne(New PointF(0, 0), New PointF(xH, yH), unePlume)
    Dim t As New Vecteur(200, 200)

    RCarr� = l3.Longueur ^ 2

    l1 = l1.Rotation(PI / 4)
    l2 = l2.Rotation(PI / 4)
    l3 = l3.Rotation(PI / 4)

    xF = l1.pAF.X
    yF = l1.pAF.Y
    xH = l1.pBF.X
    yH = l1.pBF.Y


    resultat = (1 + (yF / xF) ^ 2) * yH ^ 2 - 2 * RCarr� * yF / xF ^ 2 * yH + RCarr� ^ 2 / xF ^ 2 - RCarr� '- xF ^ 2


    uneCollection.Add(Translation(l1, t))
    uneCollection.Add(Translation(l2, t))
    uneCollection.Add(Translation(l3, t))

  End Sub

  Public Function Cr�erGraphique(ByVal uneCollection As Graphiques) As Graphique

    'essai(uneCollection)

    Dim Index As Short
    Dim uneBranche As Branche

    'Cr�er les objets graphiques branches
    '------------------------------------
    For Each uneBranche In Me
      uneBranche.Cr�erGraphique(uneCollection)
    Next
    AfficherSens()

    'Raccordements de branches
    '-------------------------
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    For Each uneBranche In Me
      RaccorderBranche(uneBranche)
    Next

    'Ajouter les raccords de branche � la collection
    uneCollection.Add(mGraphique)

    'Les raccords de branche ne sont pas s�lectables
    mGraphique.RendreS�lectable(False)

    Dim uneSurface As Surface = Int�rieurCarrefour(uneCollection)
    If cndContexte = [Global].OngletEnum.G�om�trie Then uneSurface.RendreS�lectable(S�lectable:=True)

    uneCollection.Add(uneSurface)
    Return uneSurface

  End Function

  Public Function DessinerTrafic(ByVal uneCollection As Graphiques, ByVal unTrafic As Trafic) As PolyArc
    Dim uneBrancheEntrante, uneBrancheSortante As Branche
    Dim pR�f�rence, pEllipse, pEllipsePi�tons As Point
    Dim nbEllipses As Short
    Dim i As Short
    Dim AnglesFl�ches(), AngleLigne As Single
    Dim CoefAngle() As Short
    Dim Portion As Formules.Positionnement
    Dim Espacement As Short
    Dim CoefEntreEllipses, CoefEllipsePi�tons As Single
    Dim QPi�ton As Short
    Dim hPortions As New Hashtable
    Dim unePortion As PortionCRF
    Dim uneBranche As Branche

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)


    For Each uneBrancheEntrante In Me
      With uneBrancheEntrante
        .AffecterPReferenceTrafic()

        nbEllipses = 0
        If Not .SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
          uneBrancheSortante = Pr�c�dente(uneBrancheEntrante)
          Do
            If uneBrancheSortante.NbVoies(Voie.TypeVoieEnum.VoieSortante) > 0 Then
              nbEllipses += 1
            End If
            uneBrancheSortante = Pr�c�dente(uneBrancheSortante)
          Loop Until uneBrancheSortante Is uneBrancheEntrante
        End If
        .NbEllipses = nbEllipses
        If hPortions.Contains(.Portion) Then
          unePortion = hPortions(.Portion)
          unePortion.AjoutBranche(uneBrancheEntrante)
        Else
          unePortion = New PortionCRF(.Portion, uneBrancheEntrante, unTrafic)
          hPortions.Add(.Portion, unePortion)
        End If
      End With  ' uneBrancheEntrante
    Next

    Dim uneDE As DictionaryEntry

    For Each uneDE In hPortions
      unePortion = uneDE.Value
      unePortion.Recadrer(Me)
    Next

    For Each uneBrancheEntrante In Me
      With uneBrancheEntrante
        pR�f�rence = .PR�f�renceTrafic
        nbEllipses = .NbEllipses

        If Not .SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
          'Pas de trafic � partir d'une branche sans voie entrante
          ReDim AnglesFl�ches(-1)
          ReDim CoefAngle(-1)
          AngleLigne = .AngleEnRadians + PI ' La ligne de rappel part en sens oppos� � celui de la branche (de l'extr�mit� vers l'origine
          Select Case .Portion
            Case Formules.Positionnement.Bas
              AngleLigne = -PI / 2
            Case Formules.Positionnement.Droite
              AngleLigne = PI
            Case Formules.Positionnement.Gauche
              AngleLigne = 0
            Case Formules.Positionnement.Haut
              AngleLigne = PI / 2
          End Select
          uneBrancheSortante = Pr�c�dente(uneBrancheEntrante)
          Do
            If Not uneBrancheSortante.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
              ReDim Preserve CoefAngle(.NbEllipses)
              Select Case AngleForm�(New Vecteur(.LigneDeSym�trie), New Vecteur(uneBrancheSortante.LigneDeSym�trie))

                Case -PI To -7 * PI / 8, 7 * PI / 8 To PI
                  CoefAngle(nbEllipses) = 0
                Case -7 * PI / 8 To -5 * PI / 8
                  CoefAngle(nbEllipses) = -1
                Case -5 * PI / 8 To 0
                  CoefAngle(nbEllipses) = -2
                Case 5 * PI / 8 To 7 * PI / 8
                  CoefAngle(nbEllipses) = 1
                Case 0 * PI To 5 * PI
                  CoefAngle(nbEllipses) = 2

              End Select
            End If
            uneBrancheSortante = Pr�c�dente(uneBrancheSortante)
          Loop Until uneBrancheSortante Is uneBrancheEntrante

          ReDim AnglesFl�ches(nbEllipses - 1)
          For i = 0 To nbEllipses - 1
            AnglesFl�ches(i) = .AngleEnRadians + CoefAngle(i) * PI / 8
            If i > 0 AndAlso CoefAngle(i) = CoefAngle(i - 1) Then
              '2 fois le m�me angle de d�part : les faire glisser
              AnglesFl�ches(i - 1) -= PI / 16
              AnglesFl�ches(i) += PI / 16
            End If
          Next

          pEllipse = pR�f�rence
          pEllipsePi�tons = pR�f�rence
          Portion = .Portion
          CoefEntreEllipses = -nbEllipses / 2 + 0.5
          CoefEllipsePi�tons = nbEllipses / 2 + 0.5

          Espacement = .EspacementTrafic
          Select Case Portion
            Case Formules.Positionnement.Droite
              pEllipse.Y -= CoefEntreEllipses * Espacement
              pEllipsePi�tons.Y += CoefEllipsePi�tons * Espacement
            Case Formules.Positionnement.Gauche
              pEllipse.Y += CoefEntreEllipses * Espacement
              pEllipsePi�tons.Y = pR�f�rence.Y - CoefEllipsePi�tons * Espacement
            Case Formules.Positionnement.Haut
              pEllipse.X -= CoefEntreEllipses * Espacement
              pEllipsePi�tons.X = pR�f�rence.X + CoefEllipsePi�tons * Espacement
            Case Formules.Positionnement.Bas
              pEllipse.X += CoefEntreEllipses * Espacement
              pEllipsePi�tons.X = pR�f�rence.X - CoefEllipsePi�tons * Espacement
          End Select

          ' Trafic pi�tons
          QPi�ton = unTrafic.QPi�ton(IndexOf(uneBrancheEntrante))
          If QPi�ton > 0 Then
            pEllipsePi�tons = DessinerEllipseEtRappel(pEllipsePi�tons, AngleLigne, Portion, LongueurFl�chePi�tons:=Espacement * nbEllipses)
            EcrireTrafic(QPi�ton.ToString, pEllipsePi�tons)
          End If


          uneBrancheSortante = Pr�c�dente(uneBrancheEntrante)
          Do While uneBrancheSortante.NbVoies(Voie.TypeVoieEnum.VoieSortante) = 0
            uneBrancheSortante = Pr�c�dente(uneBrancheSortante)
          Loop

          '1er trafic v�hicules
          'DessinerEllipseEtRappel(pEllipse, AngleLigne, Portion, AnglesFl�ches(0))
          DessinerEllipseEtRappel(pEllipse, AngleLigne, Portion, uneBrancheSortante)
          EcrireTrafic(unTrafic.QV�hicule(uneBrancheEntrante, uneBrancheSortante).ToString, pEllipse)

          'Autres trafics v�hicules
          For i = 1 To nbEllipses - 1
            Select Case uneBrancheEntrante.Portion
              Case Formules.Positionnement.Droite
                pEllipse.Y -= Espacement
              Case Formules.Positionnement.Gauche
                pEllipse.Y += Espacement
              Case Formules.Positionnement.Haut
                pEllipse.X -= Espacement
              Case Formules.Positionnement.Bas
                pEllipse.X += Espacement
            End Select

            uneBrancheSortante = Pr�c�dente(uneBrancheSortante)
            Do While uneBrancheSortante.SensUnique(Voie.TypeVoieEnum.VoieEntrante)
              '  uneBrancheSortante.NbVoies(Voie.TypeVoieEnum.VoieSortante) = 0
              uneBrancheSortante = Pr�c�dente(uneBrancheSortante)
            Loop

            'Dessiner l'ellipse et la ligne de Rappel
            '  DessinerEllipseEtRappel(pEllipse, AngleLigne, Portion, AnglesFl�ches(i))
            DessinerEllipseEtRappel(pEllipse, AngleLigne, Portion, uneBrancheSortante)

            EcrireTrafic(unTrafic.QV�hicule(uneBrancheEntrante, uneBrancheSortante).ToString, pEllipse)
          Next

        End If  ' Branche effectivement entrante

        'Ecrire le nom de la rue
        pR�f�rence.Y -= 2   ' Moiti� de la hauteur de l'ellipse
        Dim D�calage As Short
        If .AlignementTexte = StringAlignment.Center Then
          D�calage = 8
        Else
          D�calage = 5
        End If
        mGraphique.Add(.TexteNomRue(pR�f�rence, D�calage))

      End With  ' uneBrancheEntrante
    Next

    uneCollection.Add(mGraphique)

    Return mGraphique
  End Function

  Private Sub EcrireTrafic(ByVal Valeur As String, ByVal pEllipse As Point)
    pEllipse.Y -= 2   ' Moiti� de la hauteur de l'ellipse
    Dim unTexte As New Texte(Valeur, New SolidBrush(Color.Green), New Font("Arial", 8), pEllipse, unAlignement:=StringAlignment.Center)
    mGraphique.Add(unTexte)
  End Sub

  Private Function DessinerEllipseEtRappel(ByVal pEllipse As Point, ByVal AngleBranche As Single, ByVal Portion As Formules.Positionnement, Optional ByVal uneBrancheSortante As Branche = Nothing, Optional ByVal LongueurFl�chePi�tons As Short = 0) As Point
    Dim uneEllipse As Ellipse
    Dim uneBoite As Boite
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.EllipseTraficImpression).Clone
    Dim uneLigne As Ligne
    Dim uneFl�che As Fleche
    Dim pRappel, pFl�che As Point
    Dim HauteurFl�che As Short = 2  ' 2 mm
    Dim LongueurFl�che As Short = 10
    Dim DemiLargeur As Short = 4
    Dim DemiHauteur As Short = 2
    Dim D�calHauteurEllipse As Short = DemiHauteur + HauteurFl�che
    Dim D�calLargeurEllipse As Short = DemiLargeur + HauteurFl�che
    Dim D�calEllipsePi�tons As Short = DemiLargeur + LongueurFl�che / 2 * Abs(Cos(AngleBranche)) ' 3mm pour la demilargeur de l'ellipse + 5mm pour la 1/2 longueur des lignes de rappel v�hicules
    Dim unAngle As Single

    If LongueurFl�chePi�tons > 0 Then
      uneFl�che = New Fleche(LongueurFl�chePi�tons, 2, unePlume:=unePlume, FlecheDouble:=True)
      pFl�che = pEllipse

      Select Case Portion
        Case Formules.Positionnement.Droite
          pFl�che.Y -= D�calHauteurEllipse
          pEllipse.X -= D�calEllipsePi�tons
          pFl�che.X = pEllipse.X
          unAngle = -PI / 2
        Case Formules.Positionnement.Gauche
          pFl�che.Y += D�calHauteurEllipse
          pEllipse.X += D�calEllipsePi�tons
          pFl�che.X = pEllipse.X
          unAngle = +PI / 2
        Case Formules.Positionnement.Haut
          pFl�che.X -= D�calLargeurEllipse
          pEllipse.Y += D�calEllipsePi�tons
          pFl�che.Y = pEllipse.Y
          unAngle = PI
        Case Formules.Positionnement.Bas
          pFl�che.X += D�calLargeurEllipse
          pEllipse.Y -= D�calEllipsePi�tons
          pFl�che.Y = pEllipse.Y
          unAngle = 0
      End Select
      uneFl�che = uneFl�che.RotTrans(pFl�che, unAngle)
      uneBoite = Boite.NouvelleBoite(DemiLargeur, DemiHauteur, unePlume)
      mGraphique.Add(uneBoite.Translation(pEllipse), Poign�esAcr�er:=False)

    Else
      unAngle = AngleForm�(pFl�che, uneBrancheSortante.PR�f�renceTrafic)
      If Abs(unAngle) <= PI / 2 Then LongueurFl�che /= 2

      'Point de d�part de la ligne de rappel � la bonne distance de pEllipse
      If Portion = Formules.Positionnement.Droite Or Portion = Formules.Positionnement.Gauche Then
        pRappel = PointPosition(pEllipse, DemiLargeur, AngleBranche)
      Else
        pRappel = PointPosition(pEllipse, DemiHauteur, AngleBranche)
      End If

      'Point de d�part de la fl�che (et d'arriv�e du 1er segment de la ligne de rappel)
      pFl�che = PointPosition(pRappel, LongueurFl�che, AngleBranche)
      uneFl�che = New Fleche(LongueurFl�che, 2, unePlume:=unePlume)
      unAngle = AngleForm�(pFl�che, uneBrancheSortante.PR�f�renceTrafic)
      uneFl�che = uneFl�che.RotTrans(PointPosition(pFl�che, LongueurFl�che, unAngle), unAngle + sngPI)
      CType(uneFl�che(2), Ligne).pB = pFl�che
      'Ligne partant de l'ellipse au d�but de la fl�che
      uneLigne = New Ligne(pRappel, pFl�che, unePlume)

      uneEllipse = New Ellipse(pEllipse, 2 * DemiLargeur, 2 * DemiHauteur, unePlume)
      mGraphique.Add(uneEllipse, Poign�esACr�er:=False)
    End If

    mGraphique.Add(uneFl�che)
    If Not IsNothing(uneLigne) Then mGraphique.Add(uneLigne)

    Return pEllipse

  End Function

  Private Function Int�rieurCarrefour(ByVal uneCollection As Graphiques) As Surface
    Dim tabPoint(Count * 2 - 1) As Point
    Dim tabPointEnveloppe(Count * 2 - 1) As Point
    Dim Index As Short
    Dim uneBranche As Branche
    Dim L1, L2, L3, L1D�but As Ligne
    Dim D�calage As Single = 15.0
    Dim unAngle As Single
    Dim unePlume As New Pen(Color.Green, 2)
    Dim p As Point
    Dim unPolyarc As New PolyArc

    For Each uneBranche In Me
      Index = IndexOf(uneBranche)
      With uneBranche
        L1 = New Ligne(.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite), .Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche), unePlume)
      End With
      L3 = L1.Translation(New Vecteur(D�calage / 3, CSng(AngleForm�(L1) + PI / 2)))
      tabPointEnveloppe(Index * 2) = L3.pA
      tabPointEnveloppe(Index * 2 + 1) = L3.pB

      L1 = L1.Translation(New Vecteur(D�calage, CSng(AngleForm�(L1) - PI / 2)))

      If Index = 0 Then
        L1D�but = L1
      Else
        p = CvPoint(intersect(L1, L2, Formules.TypeInterSection.Indiff�rent))
        L1.pA = p
        L2.pB = p
        tabPoint(Index * 2) = p
      End If

      L2 = New Ligne(uneBranche.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche), Me.Suivante(uneBranche).Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite), unePlume)
      L2 = L2.Translation(New Vecteur(D�calage, CSng(AngleForm�(L2) - PI / 2)))

      p = CvPoint(intersect(L1, L2, Formules.TypeInterSection.Indiff�rent))
      L1.pB = p
      L2.pA = p
      tabPoint(Index * 2 + 1) = p
    Next

    p = CvPoint(intersect(L2, L1D�but, Formules.TypeInterSection.Indiff�rent))
    L1D�but.pA = p
    L2.pB = p
    tabPoint(0) = p

    Me.EnveloppeCarrefour = New Surface(tabPointEnveloppe)
    '   Me.EnveloppeCarrefour.Plume = New Pen(Color.Green, 2)
    '  uneCollection.Add(Me.EnveloppeCarrefour)

    Return New Surface(tabPoint)

  End Function

  Private Sub RaccorderBranche(ByVal uneBranche As Branche)
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.BrancheBordChauss�e).Clone
    Dim BordCourantGauche As Ligne = uneBranche.BordChauss�e(Branche.Lat�ralit�.Gauche)
    Dim BordSuivantDroite As Ligne = Suivante(uneBranche).BordChauss�e(Branche.Lat�ralit�.Droite)
    Dim LigneRaccord As New Ligne(BordCourantGauche.pAF, BordSuivantDroite.pAF, unePlume)

    Try

      AjusterRaccord(uneBranche, BordCourantGauche, BordSuivantDroite, LigneRaccord)

      mGraphique.Add(Cr�erRaccord(BordCourantGauche, LigneRaccord, unePlume:=unePlume))
      LigneRaccord = LigneRaccord.Invers�e
      mGraphique.Add(Cr�erRaccord(LigneRaccord, BordSuivantDroite, unePlume:=unePlume))
      mGraphique.Add(LigneRaccord.Invers�e, Poign�esACr�er:=False)

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Branche.RaccorderBranche")

    End Try

  End Sub

  Private Sub AjusterRaccord(ByVal uneBranche As Branche, ByVal SegmentD�part As Ligne, ByVal SegmentArriv�e As Ligne, ByVal SegmentRaccord As Ligne)
    Dim pC1, pC2 As PointF

    pC1 = uneBranche.Voies(uneBranche.NbVoies(Voie.TypeVoieEnum.VoieQuelconque) - 1).AjusterRaccord(SegmentD�part, SegmentArriv�e, SegmentRaccord, Branche.Lat�ralit�.Droite)
    pC2 = Suivante(uneBranche).Voies(0).AjusterRaccord(SegmentArriv�e, SegmentD�part, SegmentRaccord, Branche.Lat�ralit�.Gauche)

    If Not pC1.IsEmpty AndAlso Not pC1.Equals(SegmentRaccord.pBF) Then
      SegmentD�part.pAF = pC1
      SegmentRaccord.pAF = pC1
    End If

    If Not pC2.IsEmpty AndAlso Not pC2.Equals(SegmentRaccord.pAF) Then
      SegmentArriv�e.pAF = pC2
      SegmentRaccord.pBF = pC2
    End If

  End Sub

  Public Sub AfficherSens()
    Dim uneBranche As Branche

    For Each uneBranche In Me
      uneBranche.AfficherSens(mVariante.SensCirculation)
    Next
  End Sub

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    Dim uneBranche As Branche

    For Each uneBranche In Me
      uneBranche.Verrouiller(Verrouillage)
    Next

    If Not IsNothing(mVariante.mFondDePlan) Then
      'Masquer les raccords de branche syst�matiquement si le fond de plan est visible
      mGraphique.Invisible = True
    End If
  End Sub

  '************************************************************************
  'Masquer les branches
  '************************************************************************
  Public Sub Masquer()
    Dim uneBranche As Branche

    'Masquer chaque branche
    For Each uneBranche In Me
      uneBranche.Masquer()
    Next

    'Masquer �galement les raccords de branche
    mGraphique.Invisible = True
  End Sub

  Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
    Dim uneBranche As Branche = Item(index)

    uneBranche.ID = ID(uneBranche)

  End Sub

  Public ReadOnly Property Libell�Rues() As String
    Get
      Dim Chaine(0) As String
      Dim i As Short
      Dim uneBranche As Branche
      Dim EnDouble As Boolean

      For Each uneBranche In Me
        If IndexOf(uneBranche) = 0 Then
          Chaine(0) = uneBranche.NomRue
        Else
          For i = 0 To Chaine.Length - 1
            If Chaine(i) = uneBranche.NomRue Then
              EnDouble = True
              Exit For
            End If
          Next
          If EnDouble Then
            EnDouble = False
          Else
            ReDim Preserve Chaine(Chaine.Length)
            Chaine(Chaine.Length - 1) = uneBranche.NomRue
          End If
        End If
      Next

      Return Join(Chaine, ", ")
    End Get
  End Property
End Class

'=====================================================================================================
'--------------------------- Classe PortionCRF --------------------------
'=====================================================================================================
Public Class PortionCRF
  Private mPortion As Positionnement
  Private mBranches As New BrancheCollection
  Private CoordMini As Integer
  Private CoordLimites As Point
  Private mTrafic As Trafic
  Private mEspacement As Short

  Friend Sub New(ByVal Portion As Positionnement, ByVal uneBranche As Branche, ByVal unTrafic As Trafic)

    mPortion = Portion
    mBranches.Add(uneBranche)
    mTrafic = unTrafic

    mEspacement = uneBranche.EspacementTrafic

  End Sub

  Public Sub AjoutBranche(ByVal uneBranche As Branche)
    Dim i, pos As Short
    pos = mBranches.Count

    For i = 0 To mBranches.Count - 1
      If uneBranche.XouYR�f�renceTrafic > mBranches(i).XouYR�f�renceTrafic Then
        pos = i
        Exit For
      End If
    Next

    mBranches.Insert(pos, uneBranche)

  End Sub

  Public Sub Recadrer(ByVal colBranches As BrancheCollection)
    Dim uneBranche As Branche
    Dim MinMax(mBranches.Count - 1) As Point
    Dim pR�f�rence(mBranches.Count - 1) As Point
    Dim intervalle, intervalle2, D�calage As Integer
    Dim PointD�part, PointArriv�e, MargeCommune, MargeR�elle, Encombrement As Short
    Dim TailleUtile, TopUtile, BottomUtile As Short
    Dim D�calage1, D�calage2 As Boolean

    With cndParamDessin.ZoneGraphique
      Select Case mPortion
        Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
          TailleUtile = .Width
          BottomUtile = .Right
          TopUtile = .Left
        Case Else
          TailleUtile = .Height
          BottomUtile = .Bottom
          TopUtile = .Top
      End Select
    End With

    For Each uneBranche In mBranches
      MinMax(mBranches.IndexOf(uneBranche)) = uneBranche.XouYMinMaxTrafic(mTrafic)
      pR�f�rence(mBranches.IndexOf(uneBranche)) = uneBranche.PR�f�renceTrafic
    Next

    If mBranches.Count > 1 Then
      Select Case mBranches.Count
        Case 2
          intervalle = MinMax(0).Y - MinMax(1).X - mEspacement
          D�calage1 = Sign(intervalle) < 0

          If D�calage1 Then
            'chevauchement des 2 : d�caler les 2 de l'intervalle
            Select Case mPortion
              Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
                intervalle = -intervalle + mEspacement
                pR�f�rence(0).X += intervalle / 2
                pR�f�rence(1).X -= intervalle / 2
              Case Else
                intervalle -= mEspacement
                pR�f�rence(0).Y -= intervalle / 2
                pR�f�rence(1).Y += intervalle / 2
            End Select
          End If

        Case 3
          intervalle = MinMax(0).Y - MinMax(1).X - mEspacement
          intervalle2 = MinMax(1).Y - MinMax(2).X - mEspacement
          D�calage1 = Sign(intervalle) < 0
          D�calage2 = Sign(intervalle2) < 0

          Select Case mPortion
            Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
              intervalle = -intervalle + mEspacement
              intervalle2 = -intervalle2 + mEspacement
            Case Else
              intervalle -= mEspacement
              intervalle2 -= mEspacement
          End Select

          If D�calage1 Then
            If D�calage2 Then
              'les 2  se chevauchent, conserver le central et d�caler le 1er et le 3�me en sens inverse
              Select Case mPortion
                Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
                  pR�f�rence(0).X += intervalle
                  pR�f�rence(2).X -= intervalle2
                Case Else
                  pR�f�rence(0).Y -= intervalle
                  pR�f�rence(2).Y += intervalle2
              End Select

            Else
              'seul le 1er chevauche le central : d�caler les 3 de la moiti� de l'intervalle (en sens inverse pour le 1er)
              Select Case mPortion
                Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
                  pR�f�rence(0).X += intervalle / 2
                  pR�f�rence(1).X -= intervalle / 2
                  pR�f�rence(2).X -= intervalle / 2
                Case Else
                  pR�f�rence(0).Y -= intervalle / 2
                  pR�f�rence(1).Y += intervalle / 2
                  pR�f�rence(2).Y += intervalle / 2
              End Select
            End If

          ElseIf D�calage2 Then
            'seul le 3�me chevauche le central : d�caler les 3 de la moiti� de l'intervalle (en sens inverse pour le 3�me)
            Select Case mPortion
              Case Formules.Positionnement.Bas, Formules.Positionnement.Haut
                pR�f�rence(0).X += intervalle2 / 2
                pR�f�rence(1).X += intervalle2 / 2
                pR�f�rence(2).X -= intervalle2 / 2
              Case Else
                pR�f�rence(0).Y -= intervalle2 / 2
                pR�f�rence(1).Y -= intervalle2 / 2
                pR�f�rence(2).Y += intervalle2 / 2
            End Select
          End If
      End Select

      For Each uneBranche In mBranches
        uneBranche.PR�f�renceTrafic = pR�f�rence(mBranches.IndexOf(uneBranche))
        MinMax(mBranches.IndexOf(uneBranche)) = uneBranche.XouYMinMaxTrafic(mTrafic)
      Next
    End If

    PointD�part = MinMax(0).X + mEspacement
    PointArriv�e = MinMax(mBranches.Count - 1).Y - mEspacement
    Encombrement = Abs(PointD�part - PointArriv�e)
    MargeCommune = (TailleUtile - Encombrement) / 2
    MargeR�elle = BottomUtile - PointD�part

    If MargeR�elle < MargeCommune Then
      'Dessin trop bas(portion droite et gauche), trop � droite (portion haute et basse)
      D�calage = -(MargeCommune - MargeR�elle)
    Else
      MargeR�elle = PointArriv�e - TopUtile
      If MargeR�elle < MargeCommune Then
        'Dessin trop haut(portion droite et gauche), trop � gauche(portion haute et basse)
        D�calage = MargeCommune - MargeR�elle
      End If
    End If

    If D�calage <> 0 Then
      For Each uneBranche In mBranches
        Select Case mPortion
          Case Formules.Positionnement.Droite, Formules.Positionnement.Gauche
            pR�f�rence(mBranches.IndexOf(uneBranche)).Y += D�calage
          Case Else
            pR�f�rence(mBranches.IndexOf(uneBranche)).X += D�calage
        End Select
        uneBranche.PR�f�renceTrafic = pR�f�rence(mBranches.IndexOf(uneBranche))
      Next
    End If

  End Sub

  Friend ReadOnly Property Portion() As Positionnement
    Get
      Return mPortion
    End Get
  End Property
  Public ReadOnly Property Espacement() As Short
    Get
      Return mEspacement
    End Get
  End Property
  Public Function NbEllipses() As Short
    Dim uneBranche As Branche
    For Each uneBranche In mBranches
      NbEllipses += uneBranche.NbEllipses
    Next
  End Function

  Public ReadOnly Property Branches() As BrancheCollection
    Get
      Return mBranches
    End Get
  End Property
End Class
