'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Phase.vb																									'
'						Classes																														'
'							Phase																														'
'******************************************************************************

Option Strict Off
Option Explicit On

'=====================================================================================================
'--------------------------- Classe Phase --------------------------
'=====================================================================================================
Public Class Phase : Inherits M�tier


  '##ModelId=3C8B3BB00251
  Private mDur�e As Short
  Private mDur�eIncompressible As Short
  ' Private mRougeD�gagement As Short
  Private mTraficSupport� As Integer

  '##ModelId=3C8B3AA50177
  Public mLignesFeux As New LigneFeuxCollection
  Public mPlanFeux As PlanFeux
  Public Verrouill�e As Boolean
  Public mGraphiqueNum�rosFeux As PolyArc

  Public Const RayonCercleLF As Single = 2.5    ' 2mm en v10 - 3mm en v11 (alors en Short)

  Public Property TraficSupport�() As Integer
    Get
      Return mTraficSupport�
    End Get
    Set(ByVal Value As Integer)
      mTraficSupport� = Value
    End Set
  End Property

  Public Property Dur�e() As Short
    Get
      Dur�e = mDur�e
    End Get
    Set(ByVal Value As Short)
      mDur�e = Value
    End Set
  End Property

  '*************************************************************************************************
  ' Dur�e incompressible de la phase
  '*************************************************************************************************
  Public Property Dur�eIncompressible() As Short
    Get
      If TypeOf mPlanFeux Is PlanFeuxBase Then
        'Dur�e de la phase correspondante du plan de feux de s�curit�
        Return mDur�eIncompressible
      Else
        'Dur�e de la phase correspondante du plan de feux servant de base au plan de feux de fonctionnement
        Dim IndexPhase As Short = mPlanFeux.mPhases.IndexOf(Me)
        Return CType(mPlanFeux, PlanFeuxFonctionnement).mPlanBase.mPhases(IndexPhase).Dur�e
      End If
    End Get

    Set(ByVal Value As Short)
      mDur�eIncompressible = Value
    End Set
  End Property

  Public Sub CalculerDur�eMini(ByVal PhaseSuivante As Phase)
    Dim lHorizontale As LigneFeux
    Dim lVerticale As LigneFeux
    Dim RougeD�gagement As Short
    Dim Dur�eIncompressiblePhase As Short

    Dur�eIncompressiblePhase = 0

    For Each lHorizontale In mLignesFeux
      If Not PhaseSuivante.mLignesFeux.Contains(lHorizontale) Then
        'Sinon la ligne continue sur la phase suivante : pas de rouge de d�gagement pour cette phase
        'D�terminer le rouge de d�gagement le + d�favorable pour ce feu par rapport aux feux de la phase suivante
        RougeD�gagement = 0

        For Each lVerticale In PhaseSuivante.mLignesFeux
          If Not mLignesFeux.Contains(lVerticale) Then
            'Sinon, la ligne de feux a d�marr� dans la m^me phase que lHorizontale
            RougeD�gagement = Math.Max(RougeD�gagement, mPlanFeux.mLignesFeux.TempsD�gagement(lHorizontale, lVerticale))
          End If
        Next
        'En d�duire la dur�e incompressible de la phase
        Dur�eIncompressiblePhase = Math.Max(Dur�eIncompressiblePhase, Dur�eMini(RougeD�gagement, lHorizontale))
      End If

    Next  ' lHorizontale

    'Affecter les propri�t�s � la phase
    Dur�eIncompressible = Dur�eIncompressiblePhase

    'Initialiser la dur�e de la phase avec cette valeur minimale si
    ' - il n'y avait pas de trafic pour ce plan de feux (donc dur�e pr�c�dente =0, car non calcul�e par CalculerCapacit�sPlansPhasage)
    ' - le trafic �tait trop important (> d�bit de saturation) , donc idem ci-dessus : dur�e pr�c�dente = 0
    ' - les rouges de d�gagement ou les verts mini ont �t� augment�s et font que les dur�es pr�c�dentes peuvent ne plus satisfaire aux minima
    If Dur�e < Dur�eIncompressiblePhase Then
      Dur�e = Dur�eIncompressiblePhase
    End If

  End Sub

  '************************************************************************************
  ' Calculer la dur�e minimum de la phase pour un feu, connaissant le rouge de d�gagement
  '************************************************************************************
  Private Function Dur�eMini(ByVal RougeD�gagement As Short, ByVal uneLigneFeux As LigneFeux) As Short

    Dim Dur�ePhase As Short
    Dim Dur�eJaune As Short = uneLigneFeux.Dur�eJaune

    Dur�ePhase = RougeD�gagement
    'Ajouter le vert
    If uneLigneFeux.EstPi�ton Then
      Dur�ePhase += mVertMiniPi�tons()
    Else
      Dur�ePhase += mVertMiniV�hicules()
    End If

    'Ajouter le jaune pour les v�hicules
    Dur�ePhase += Dur�eJaune

    'M�moriser dans le plan de feux de base le rouge incompressible de la ligne de feux :
    'C'est le + grand rouge de d�gagement de la ligne /ensemble des lignes de la phase suivante
    CType(mPlanFeux, PlanFeuxBase).RougeIncompressible(uneLigneFeux) = RougeD�gagement

    Return Dur�ePhase

  End Function

  Private Function mVertMiniV�hicules() As Short
    Return CType(mPlanFeux, PlanFeuxBase).VertMiniV�hicules
  End Function

  Private Function mVertMiniPi�tons() As Short
    Return CType(mPlanFeux, PlanFeuxBase).VertMiniPi�tons
  End Function

  '*************************************************************************************************
  'Temps perdu sur la phase : temps pendant aucun v�hicule ne passe
  '*************************************************************************************************
  Public Function TempsPerdu(ByVal unPlanFeux As PlanFeux) As Short
    Dim uneLigneFeux As LigneFeux

    Dim TempsPerduD�marrage As Short = mPlanFeux.mVariante.TempsPerduD�marrage
    Dim JauneInutilis� As Short = mPlanFeux.mVariante.JauneInutilis�
    Dim D�calOuvre, D�calFerme, D�calOuvreLF, D�calFermeLF As Short
    D�calOuvre = 100
    D�calFerme = 100

    If EstSeulementPi�ton() Then
      ' Pas de ligne de feux v�hicules : toute la phase est du temps perdu pour leur �coulement
      'Pour un plan de phasage, ce sera la dur�e mini calcul�e par PhaseCollection.CalculerDur�esMini
      Return Dur�e

    Else
      For Each uneLigneFeux In mLignesFeux
        If uneLigneFeux.EstV�hicule Then
          With mPlanFeux
            'Les d�calages � l'ouverture �tant interdits aux v�hicules (???), D�calOuvreLF= toujours 0
            'La ligne suivante r�serve l'avenir
            D�calOuvreLF = .D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Ouverture)
            D�calFermeLF = .D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Fermeture)
            D�calFermeLF += .RougeIncompressible(uneLigneFeux)
          End With

          'Les d�calages � l'ouverture �tant interdits aux v�hicules (???), D�butVert= toujours 0
          'La ligne suivante r�serve l'avenir
          Select Case unPlanFeux.PositionDansPhase(uneLigneFeux, Me)
            Case PlanFeux.Position.Unique
              'Le vert d�marre et s'arr�te dans cette phase pour cette ligne de feux
              'prendre le + petit d�calage � l'ouverture
              D�calOuvre = Math.Min(D�calOuvre, D�calOuvreLF)
              'prendre le + petit d�calage � la fermeture augment� du rougeincompressible(pour temps de d�gagement)
              D�calFerme = Math.Min(D�calFerme, D�calFermeLF)

            Case PlanFeux.Position.Premi�re
              'Le vert d�marre dans cette phase pour cette ligne de feux 
              D�calOuvre = Math.Min(D�calOuvre, D�calOuvreLF)
              'La ligne continue sur la phase suivante : Le vert va  jusqu'� la fin de la phase
              D�calFerme = 0
              JauneInutilis� = 0

            Case PlanFeux.Position.Derni�re
              'Les v�hicules passaient d�j� dans la phase pr�c�dente
              'Pas de temps perdu au d�marrage
              D�calOuvre = 0
              TempsPerduD�marrage = 0
              'prendre le + petit d�calage � la fermeture augment� du rougeincompressible(pour temps de d�gagement)
              D�calFerme = Math.Min(D�calFerme, D�calFermeLF)

          End Select
        End If
      Next

      Return D�calOuvre + D�calFerme + TempsPerduD�marrage + JauneInutilis�

    End If

  End Function

  '*************************************************************************************************
  ' Cloner la phase du plan de feux de base(ou d'un autre plan de feux de fct) dans une nouvelle phase 
  ' pour initialiser  le plan de feux de fonctionnement
  '*************************************************************************************************
  Public Function Cloner(ByVal unPlanFeux As PlanFeux) As Phase
    Dim unePhase As New Phase
    Dim uneLigneFeux As LigneFeux
    Dim i As PlanFeux.D�calage

    With unePhase
      .mPlanFeux = unPlanFeux
      If TypeOf unPlanFeux Is PlanFeuxBase Then
        .Dur�eIncompressible = Dur�eIncompressible
      End If
      .Dur�e = mDur�e
      For Each uneLigneFeux In mLignesFeux
        .mLignesFeux.Add(uneLigneFeux)
        For i = PlanFeux.D�calage.Ouverture To PlanFeux.D�calage.Fermeture
          .mPlanFeux.D�calageOuvreFerme(uneLigneFeux, i) = mPlanFeux.D�calageOuvreFerme(uneLigneFeux, i)
        Next
      Next
    End With

    Return unePhase

  End Function

  Public Sub New()
  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeux, ByVal uneRowPhase As DataSetDiagfeux.PhaseRow)
    Dim i As Short
    Dim uneLigneFeux As LigneFeux
    Dim ID As String
    Dim uneVariante As Variante = unPlanFeux.mVariante

    mPlanFeux = unPlanFeux

    With uneRowPhase
      mDur�e = .Dur�e
      Verrouill�e = .Verrouill�e

      For i = 0 To .GetIDLigneFeuxRows.Length - 1
        With .GetIDLigneFeuxRows(i)
          ID = .IDLigneFeux_text
          uneLigneFeux = uneVariante.mLignesFeux(ID)
          If .D�calageOuvre > 0 Then
            mPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Ouverture) = .D�calageOuvre
          End If
          If .D�calageFerme > 0 Then
            mPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Fermeture) = .D�calageFerme
          End If
        End With
        mLignesFeux.Add(uneLigneFeux)
      Next

    End With

  End Sub

  '********************************************************************************************************************
  ' Enregistrer la Ligne de feux dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim uneRowPhase As DataSetDiagfeux.PhaseRow
    Dim uneLigneFeux As LigneFeux

    uneRowPhase = ds.Phase.AddPhaseRow(mDur�e, Verrouill�e, uneRowPlanFeux)
    For Each uneLigneFeux In mLignesFeux
      'Il y aura redondance si la ligne est sur plusieurs phases
      ds.IDLigneFeux.AddIDLigneFeuxRow(mPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Ouverture), mPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Fermeture), uneLigneFeux.ID, uneRowPhase)
    Next

  End Sub

  Public Function Equivalente(ByVal unePhase As Phase) As Boolean
    If mLignesFeux.Count = unePhase.mLignesFeux.Count Then
      Dim uneLigneFeux As LigneFeux

      For Each uneLigneFeux In mLignesFeux
        'Modif AV : 26/03/07 - + Pertinent (l'ordre n'a pas d'importance)
        'If Not uneLigneFeux Is unePhase.mLignesFeux(mLignesFeux.IndexOf(uneLigneFeux)) Then
        If Not unePhase.mLignesFeux.Contains(uneLigneFeux) Then
          Return False
        End If
      Next

      Return True

    End If
  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim uneLigneFeux As LigneFeux
    Dim p1, p2 As PointF
    Dim D�calagePossible As Single
    '2 cercles doivent �tre espac�s d'au moins 1mm
    Dim DistMin As Single = 2 * RayonCercleLF + 1
    Dim i, j As Short

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    'Stocker les num�ros de feux (ID entour� d'un cercle) dans 1 polyarc particulier
    mGraphiqueNum�rosFeux = Nothing
    mGraphiqueNum�rosFeux = New PolyArc

    'Cr�er l'objet graphique de chaque ligne de feux (pour les v�hicules : m�me si la ligne de feux n'est pas dans la phase)
    For Each uneLigneFeux In mPlanFeux.mVariante.mLignesFeux
      uneLigneFeux.Cr�erGraphiquePhase(Me, uneCollection)
    Next

    mGraphique.Add(mGraphiqueNum�rosFeux)

    'D�terter d'�ventuels chevauchement des cercles contenant les ID
    For i = 0 To mGraphiqueNum�rosFeux.Count - 1
      p1 = CType(mGraphiqueNum�rosFeux(i), PolyArc).mpRef

      For j = 0 To mGraphiqueNum�rosFeux.Count - 1
        If i <> j Then
          p2 = CType(mGraphiqueNum�rosFeux(j), PolyArc).mpRef
          'On divise par 2 car l'�cartement sera partag� par chacun des 2 cercles
          D�calagePossible = (DistMin - Distance(p1, p2)) / 2
          If D�calagePossible > 0 Then
            'Chevauchement ou moins d'1mm entre les 2 : les �carter de part et d'autre de mani�re � ce que l'�cart soit de 1mm
            p1 = PointPosition(p1, D�calagePossible, p2, p1)
            p2 = PointPosition(p2, D�calagePossible, p1, p2)
            CType(mGraphiqueNum�rosFeux(i), PolyArc).mpRef = p1
            CType(mGraphiqueNum�rosFeux(j), PolyArc).mpRef = p2
          End If
        End If
      Next
    Next

    uneCollection.Add(mGraphique)
    Return mGraphique

  End Function

  Public Function EstSeulementPi�ton() As Boolean
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstV�hicule Then Return False
    Next
    Return True
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe PhaseCollection--------------------------
'=====================================================================================================
Public Class PhaseCollection : Inherits CollectionBase
  Private mPlanFeux As PlanFeux

  ' Cr�er une instance la collection
  Public Sub New(ByVal unPlanFeux As PlanFeux)
    MyBase.New()
    mPlanFeux = unPlanFeux
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unePhase As Phase) As Short
    Return Me.List.Add(unePhase)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Phase)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal valeur As Phase)
    If Me.List.Contains(valeur) Then
      Me.List.Remove(valeur)
    End If

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unePhase As Phase)
    Me.List.Insert(Index, unePhase)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Phase
    Get
      Return CType(Me.List.Item(Index), Phase)
    End Get
  End Property

  Public Function IndexOf(ByVal valeur As Phase) As Short
    Return Me.List.IndexOf(valeur)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Function Contains(ByVal valeur As Phase) As Boolean
    Return Me.List.Contains(valeur)
  End Function

  Public Sub D�placer(ByVal unePhase As Phase, ByVal Index As Short)
    If IndexOf(unePhase) <> Index Then
      Remove(unePhase)
      Insert(Index, unePhase)
    End If
  End Sub

  Public Sub Cloner(ByVal unPlanFeux As PlanFeux)
    Dim unePhase As Phase

    For Each unePhase In Me
      unPlanFeux.mPhases.Add(unePhase.Cloner(unPlanFeux))
    Next

  End Sub

  '********************************************************************************************************************
  ' D�terminer la 1�re phase non verrouill�e qui suit la phase
  '********************************************************************************************************************
  Public Function PhaseSuivante(ByVal unePhase As Phase) As Phase

    With Me
      PhaseSuivante = .Item(SuivantDansCollection(.IndexOf(unePhase), .Count))
    End With

  End Function


  '*******************************************************************************
  ' Calculer les dur�es incompressibles de chaque phase : 
  '	C'est le plan de feux de s�curit�
  '*******************************************************************************
  Public Sub CalculerDur�esMini()
    Dim unePhase As Phase

    For Each unePhase In Me
      unePhase.CalculerDur�eMini(PhaseSuivante(unePhase))
    Next ' unePhase

  End Sub

  Public Function TempsPerdu() As Short
    Dim unePhase As Phase

    For Each unePhase In Me
      TempsPerdu += unePhase.TempsPerdu(mPlanFeux)
    Next

  End Function

  Public Function PhaseEquivalente(ByVal xPhase As Phase) As Phase
    Dim unePhase As Phase

    For Each unePhase In Me
      If unePhase.Equivalente(xPhase) Then
        Return unePhase
      End If
    Next
  End Function

  Public Function IndexOfEquivalent(ByVal xPhase As Phase) As Short
    Dim unePhase As Phase

    For Each unePhase In Me
      If unePhase.Equivalente(xPhase) Then
        Return IndexOf(unePhase)
      End If
    Next
  End Function

  '******************************************************************
  ' retourne si 2 phases dnas les 3 sont �quivalentes
  ' dans ce cas : ceci �quivaut � un 2 phases
  '******************************************************************
  Public Function EquivalentDeuxPhases() As Boolean

    If Item(0).Equivalente(Item(1)) Then
      Return True
    ElseIf Item(0).Equivalente(Item(2)) Then
      Return True
    ElseIf Item(1).Equivalente(Item(2)) Then
      Return True
    End If

  End Function

End Class
