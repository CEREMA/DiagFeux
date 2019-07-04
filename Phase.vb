'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
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
Public Class Phase : Inherits Métier


  '##ModelId=3C8B3BB00251
  Private mDurée As Short
  Private mDuréeIncompressible As Short
  ' Private mRougeDégagement As Short
  Private mTraficSupporté As Integer

  '##ModelId=3C8B3AA50177
  Public mLignesFeux As New LigneFeuxCollection
  Public mPlanFeux As PlanFeux
  Public Verrouillée As Boolean
  Public mGraphiqueNumérosFeux As PolyArc

  Public Const RayonCercleLF As Single = 2.5    ' 2mm en v10 - 3mm en v11 (alors en Short)

  Public Property TraficSupporté() As Integer
    Get
      Return mTraficSupporté
    End Get
    Set(ByVal Value As Integer)
      mTraficSupporté = Value
    End Set
  End Property

  Public Property Durée() As Short
    Get
      Durée = mDurée
    End Get
    Set(ByVal Value As Short)
      mDurée = Value
    End Set
  End Property

  '*************************************************************************************************
  ' Durée incompressible de la phase
  '*************************************************************************************************
  Public Property DuréeIncompressible() As Short
    Get
      If TypeOf mPlanFeux Is PlanFeuxBase Then
        'Durée de la phase correspondante du plan de feux de sécurité
        Return mDuréeIncompressible
      Else
        'Durée de la phase correspondante du plan de feux servant de base au plan de feux de fonctionnement
        Dim IndexPhase As Short = mPlanFeux.mPhases.IndexOf(Me)
        Return CType(mPlanFeux, PlanFeuxFonctionnement).mPlanBase.mPhases(IndexPhase).Durée
      End If
    End Get

    Set(ByVal Value As Short)
      mDuréeIncompressible = Value
    End Set
  End Property

  Public Sub CalculerDuréeMini(ByVal PhaseSuivante As Phase)
    Dim lHorizontale As LigneFeux
    Dim lVerticale As LigneFeux
    Dim RougeDégagement As Short
    Dim DuréeIncompressiblePhase As Short

    DuréeIncompressiblePhase = 0

    For Each lHorizontale In mLignesFeux
      If Not PhaseSuivante.mLignesFeux.Contains(lHorizontale) Then
        'Sinon la ligne continue sur la phase suivante : pas de rouge de dégagement pour cette phase
        'Déterminer le rouge de dégagement le + défavorable pour ce feu par rapport aux feux de la phase suivante
        RougeDégagement = 0

        For Each lVerticale In PhaseSuivante.mLignesFeux
          If Not mLignesFeux.Contains(lVerticale) Then
            'Sinon, la ligne de feux a démarré dans la m^me phase que lHorizontale
            RougeDégagement = Math.Max(RougeDégagement, mPlanFeux.mLignesFeux.TempsDégagement(lHorizontale, lVerticale))
          End If
        Next
        'En déduire la durée incompressible de la phase
        DuréeIncompressiblePhase = Math.Max(DuréeIncompressiblePhase, DuréeMini(RougeDégagement, lHorizontale))
      End If

    Next  ' lHorizontale

    'Affecter les propriétés à la phase
    DuréeIncompressible = DuréeIncompressiblePhase

    'Initialiser la durée de la phase avec cette valeur minimale si
    ' - il n'y avait pas de trafic pour ce plan de feux (donc durée précédente =0, car non calculée par CalculerCapacitésPlansPhasage)
    ' - le trafic était trop important (> débit de saturation) , donc idem ci-dessus : durée précédente = 0
    ' - les rouges de dégagement ou les verts mini ont été augmentés et font que les durées précédentes peuvent ne plus satisfaire aux minima
    If Durée < DuréeIncompressiblePhase Then
      Durée = DuréeIncompressiblePhase
    End If

  End Sub

  '************************************************************************************
  ' Calculer la durée minimum de la phase pour un feu, connaissant le rouge de dégagement
  '************************************************************************************
  Private Function DuréeMini(ByVal RougeDégagement As Short, ByVal uneLigneFeux As LigneFeux) As Short

    Dim DuréePhase As Short
    Dim DuréeJaune As Short = uneLigneFeux.DuréeJaune

    DuréePhase = RougeDégagement
    'Ajouter le vert
    If uneLigneFeux.EstPiéton Then
      DuréePhase += mVertMiniPiétons()
    Else
      DuréePhase += mVertMiniVéhicules()
    End If

    'Ajouter le jaune pour les véhicules
    DuréePhase += DuréeJaune

    'Mémoriser dans le plan de feux de base le rouge incompressible de la ligne de feux :
    'C'est le + grand rouge de dégagement de la ligne /ensemble des lignes de la phase suivante
    CType(mPlanFeux, PlanFeuxBase).RougeIncompressible(uneLigneFeux) = RougeDégagement

    Return DuréePhase

  End Function

  Private Function mVertMiniVéhicules() As Short
    Return CType(mPlanFeux, PlanFeuxBase).VertMiniVéhicules
  End Function

  Private Function mVertMiniPiétons() As Short
    Return CType(mPlanFeux, PlanFeuxBase).VertMiniPiétons
  End Function

  '*************************************************************************************************
  'Temps perdu sur la phase : temps pendant aucun véhicule ne passe
  '*************************************************************************************************
  Public Function TempsPerdu(ByVal unPlanFeux As PlanFeux) As Short
    Dim uneLigneFeux As LigneFeux

    Dim TempsPerduDémarrage As Short = mPlanFeux.mVariante.TempsPerduDémarrage
    Dim JauneInutilisé As Short = mPlanFeux.mVariante.JauneInutilisé
    Dim DécalOuvre, DécalFerme, DécalOuvreLF, DécalFermeLF As Short
    DécalOuvre = 100
    DécalFerme = 100

    If EstSeulementPiéton() Then
      ' Pas de ligne de feux véhicules : toute la phase est du temps perdu pour leur écoulement
      'Pour un plan de phasage, ce sera la durée mini calculée par PhaseCollection.CalculerDuréesMini
      Return Durée

    Else
      For Each uneLigneFeux In mLignesFeux
        If uneLigneFeux.EstVéhicule Then
          With mPlanFeux
            'Les décalages à l'ouverture étant interdits aux véhicules (???), DécalOuvreLF= toujours 0
            'La ligne suivante réserve l'avenir
            DécalOuvreLF = .DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Ouverture)
            DécalFermeLF = .DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Fermeture)
            DécalFermeLF += .RougeIncompressible(uneLigneFeux)
          End With

          'Les décalages à l'ouverture étant interdits aux véhicules (???), DébutVert= toujours 0
          'La ligne suivante réserve l'avenir
          Select Case unPlanFeux.PositionDansPhase(uneLigneFeux, Me)
            Case PlanFeux.Position.Unique
              'Le vert démarre et s'arrête dans cette phase pour cette ligne de feux
              'prendre le + petit décalage à l'ouverture
              DécalOuvre = Math.Min(DécalOuvre, DécalOuvreLF)
              'prendre le + petit décalage à la fermeture augmenté du rougeincompressible(pour temps de dégagement)
              DécalFerme = Math.Min(DécalFerme, DécalFermeLF)

            Case PlanFeux.Position.Première
              'Le vert démarre dans cette phase pour cette ligne de feux 
              DécalOuvre = Math.Min(DécalOuvre, DécalOuvreLF)
              'La ligne continue sur la phase suivante : Le vert va  jusqu'à la fin de la phase
              DécalFerme = 0
              JauneInutilisé = 0

            Case PlanFeux.Position.Dernière
              'Les véhicules passaient déjà dans la phase précédente
              'Pas de temps perdu au démarrage
              DécalOuvre = 0
              TempsPerduDémarrage = 0
              'prendre le + petit décalage à la fermeture augmenté du rougeincompressible(pour temps de dégagement)
              DécalFerme = Math.Min(DécalFerme, DécalFermeLF)

          End Select
        End If
      Next

      Return DécalOuvre + DécalFerme + TempsPerduDémarrage + JauneInutilisé

    End If

  End Function

  '*************************************************************************************************
  ' Cloner la phase du plan de feux de base(ou d'un autre plan de feux de fct) dans une nouvelle phase 
  ' pour initialiser  le plan de feux de fonctionnement
  '*************************************************************************************************
  Public Function Cloner(ByVal unPlanFeux As PlanFeux) As Phase
    Dim unePhase As New Phase
    Dim uneLigneFeux As LigneFeux
    Dim i As PlanFeux.Décalage

    With unePhase
      .mPlanFeux = unPlanFeux
      If TypeOf unPlanFeux Is PlanFeuxBase Then
        .DuréeIncompressible = DuréeIncompressible
      End If
      .Durée = mDurée
      For Each uneLigneFeux In mLignesFeux
        .mLignesFeux.Add(uneLigneFeux)
        For i = PlanFeux.Décalage.Ouverture To PlanFeux.Décalage.Fermeture
          .mPlanFeux.DécalageOuvreFerme(uneLigneFeux, i) = mPlanFeux.DécalageOuvreFerme(uneLigneFeux, i)
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
      mDurée = .Durée
      Verrouillée = .Verrouillée

      For i = 0 To .GetIDLigneFeuxRows.Length - 1
        With .GetIDLigneFeuxRows(i)
          ID = .IDLigneFeux_text
          uneLigneFeux = uneVariante.mLignesFeux(ID)
          If .DécalageOuvre > 0 Then
            mPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Ouverture) = .DécalageOuvre
          End If
          If .DécalageFerme > 0 Then
            mPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Fermeture) = .DécalageFerme
          End If
        End With
        mLignesFeux.Add(uneLigneFeux)
      Next

    End With

  End Sub

  '********************************************************************************************************************
  ' Enregistrer la Ligne de feux dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim uneRowPhase As DataSetDiagfeux.PhaseRow
    Dim uneLigneFeux As LigneFeux

    uneRowPhase = ds.Phase.AddPhaseRow(mDurée, Verrouillée, uneRowPlanFeux)
    For Each uneLigneFeux In mLignesFeux
      'Il y aura redondance si la ligne est sur plusieurs phases
      ds.IDLigneFeux.AddIDLigneFeuxRow(mPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Ouverture), mPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Fermeture), uneLigneFeux.ID, uneRowPhase)
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

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim uneLigneFeux As LigneFeux
    Dim p1, p2 As PointF
    Dim DécalagePossible As Single
    '2 cercles doivent être espacés d'au moins 1mm
    Dim DistMin As Single = 2 * RayonCercleLF + 1
    Dim i, j As Short

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me

    'Stocker les numéros de feux (ID entouré d'un cercle) dans 1 polyarc particulier
    mGraphiqueNumérosFeux = Nothing
    mGraphiqueNumérosFeux = New PolyArc

    'Créer l'objet graphique de chaque ligne de feux (pour les véhicules : même si la ligne de feux n'est pas dans la phase)
    For Each uneLigneFeux In mPlanFeux.mVariante.mLignesFeux
      uneLigneFeux.CréerGraphiquePhase(Me, uneCollection)
    Next

    mGraphique.Add(mGraphiqueNumérosFeux)

    'Déterter d'éventuels chevauchement des cercles contenant les ID
    For i = 0 To mGraphiqueNumérosFeux.Count - 1
      p1 = CType(mGraphiqueNumérosFeux(i), PolyArc).mpRef

      For j = 0 To mGraphiqueNumérosFeux.Count - 1
        If i <> j Then
          p2 = CType(mGraphiqueNumérosFeux(j), PolyArc).mpRef
          'On divise par 2 car l'écartement sera partagé par chacun des 2 cercles
          DécalagePossible = (DistMin - Distance(p1, p2)) / 2
          If DécalagePossible > 0 Then
            'Chevauchement ou moins d'1mm entre les 2 : les écarter de part et d'autre de manière à ce que l'écart soit de 1mm
            p1 = PointPosition(p1, DécalagePossible, p2, p1)
            p2 = PointPosition(p2, DécalagePossible, p1, p2)
            CType(mGraphiqueNumérosFeux(i), PolyArc).mpRef = p1
            CType(mGraphiqueNumérosFeux(j), PolyArc).mpRef = p2
          End If
        End If
      Next
    Next

    uneCollection.Add(mGraphique)
    Return mGraphique

  End Function

  Public Function EstSeulementPiéton() As Boolean
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstVéhicule Then Return False
    Next
    Return True
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe PhaseCollection--------------------------
'=====================================================================================================
Public Class PhaseCollection : Inherits CollectionBase
  Private mPlanFeux As PlanFeux

  ' Créer une instance la collection
  Public Sub New(ByVal unPlanFeux As PlanFeux)
    MyBase.New()
    mPlanFeux = unPlanFeux
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unePhase As Phase) As Short
    Return Me.List.Add(unePhase)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Phase)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal valeur As Phase)
    If Me.List.Contains(valeur) Then
      Me.List.Remove(valeur)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unePhase As Phase)
    Me.List.Insert(Index, unePhase)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Phase
    Get
      Return CType(Me.List.Item(Index), Phase)
    End Get
  End Property

  Public Function IndexOf(ByVal valeur As Phase) As Short
    Return Me.List.IndexOf(valeur)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal valeur As Phase) As Boolean
    Return Me.List.Contains(valeur)
  End Function

  Public Sub Déplacer(ByVal unePhase As Phase, ByVal Index As Short)
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
  ' Déterminer la 1ère phase non verrouillée qui suit la phase
  '********************************************************************************************************************
  Public Function PhaseSuivante(ByVal unePhase As Phase) As Phase

    With Me
      PhaseSuivante = .Item(SuivantDansCollection(.IndexOf(unePhase), .Count))
    End With

  End Function


  '*******************************************************************************
  ' Calculer les durées incompressibles de chaque phase : 
  '	C'est le plan de feux de sécurité
  '*******************************************************************************
  Public Sub CalculerDuréesMini()
    Dim unePhase As Phase

    For Each unePhase In Me
      unePhase.CalculerDuréeMini(PhaseSuivante(unePhase))
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
  ' retourne si 2 phases dnas les 3 sont équivalentes
  ' dans ce cas : ceci équivaut à un 2 phases
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
