'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Voie.vb																									'
'						Classes																														'
'							Voie : Voie de circulation																			'
'							VoieCollection : Collection de voies														'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe Voie --------------------------
'=====================================================================================================
Public Class Voie : Inherits Métier

  'Voie de circulation sur une branche
  Public mBranche As Branche

  'Une voie peut être entrante ou sortante
  Public Entrante As Boolean
  Public Const MaxVoies As Short = 8

  Private mBordure(1) As Ligne
  Private mAxe As Ligne
  Private mExtrémité(1) As PointF

  'Courants parcourus par la voie si elle est entrante
  Public mCourants As CourantCollection

  Public Enum TypeVoieEnum
    VoieEntrante
    VoieSortante
    VoieQuelconque
  End Enum

  Public Enum ExtrémitéEnum
    Extérieur
    Intérieur
  End Enum

  Public Sub New(ByVal EstEntrante As Boolean, ByVal uneBranche As Branche)
    mBranche = uneBranche
    Entrante = EstEntrante
    If Entrante Then
      mCourants = New CourantCollection
    End If
  End Sub


  '********************************************************************************************************************
  ' Enregistrer la voie dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim uneRowVoie As DataSetDiagfeux.VoieRow

    uneRowVoie = ds.Voie.AddVoieRow(Entrante, ID, uneRowBranche)
  End Sub

  Private ReadOnly Property Variante() As Variante
    Get
      Return mBranche.Variante
    End Get
  End Property

  '********************************************************************************************************************
  'Identifiant unique pour la variante en vue de la conservation en données persistantes
  'Doit être calculé par sommation sur l'ensemble des branches
  '********************************************************************************************************************
  Public Function ID() As Short
    Dim numBranche As Short = Me.Variante.mBranches.IndexOf(mBranche)
    Dim IndexVoie As Short = mBranche.Voies.IndexOf(Me)

    ID = IndexVoie + numBranche * MaxVoies

  End Function

  Public Overloads Sub CréerGraphique(ByVal L1 As Ligne, ByVal L2 As Ligne)
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(Nothing, mGraphique)

    'La 1ère ligne est la ligne est la ligne la plus à droite dans le repère de la branche (orientée vers l'extérieur du carrefour)
    Dim i As Branche.Latéralité

    mBordure(Branche.Latéralité.Droite) = L1
    mBordure(Branche.Latéralité.Gauche) = L2
    'On indique ici l'axe en coordonnées Flottantes, car cet élément sert au dessin des trajectoires (les recherches d'intesection peuvent échouer à un epsilon près : Fonction TrajectoireVéhicules.InitGraphique)
    mAxe = New Ligne(Milieu(L1.pAF, L2.pAF), Milieu(L1.pBF, L2.pBF))
    For i = Branche.Latéralité.Droite To Branche.Latéralité.Gauche
      mGraphique.Add(mBordure(i), PoignéesACréer:=False)
      mExtrémité(i) = mBordure(i).pAF
    Next

  End Sub

  Public Function MilieuExtrémité(ByVal Coté As ExtrémitéEnum) As Point

    If Coté = ExtrémitéEnum.Intérieur Then
      Return mAxe.pA
    Else
      Return mAxe.pB
    End If

  End Function

  Public ReadOnly Property Axe() As Ligne
    Get
      Return mAxe
    End Get
  End Property

  Public ReadOnly Property Bordure(ByVal Index As Branche.Latéralité) As Ligne
    Get
      Return mBordure(Index)
    End Get
  End Property

  Public ReadOnly Property Extrémité(ByVal Index As Branche.Latéralité) As PointF
    Get
      Return mExtrémité(Index)
    End Get
  End Property

  Public Function PtIntérieur(ByVal p As Point, Optional ByVal Strict As Boolean = True) As Boolean
    Dim L1 As Ligne = Bordure(Branche.Latéralité.Droite)
    Dim L2 As Ligne = Bordure(Branche.Latéralité.Gauche)
    Dim P1, P2, pOrigine1, pOrigine2 As Point

    pOrigine1 = L1.pA
    pOrigine2 = L2.pA
    'Vérifier que tous les points sont à l'intérieur de la branche (c'est à dire entre les 2 bords de chaussée)
    'projeter le point sur le bord de chaussée droite
    P1 = Projection(p, pOrigine1, mBranche.AngleEnRadians)
    If L1.PtSurSegment(P1) Then
      If Distance(p, P1) = 0.0 Then
        'Le point est sur le 1er coté de la voie
        PtIntérieur = Not Strict
      Else
        'projeter le point sur le bord de chaussée gauche
        P2 = Projection(p, pOrigine2, mBranche.AngleEnRadians)
        If L2.PtSurSegment(P2) Then
          If Distance(p, P2) = 0.0 Then
            'Le point est sur le 2ème coté de la voie
            PtIntérieur = Not Strict
          Else
            PtIntérieur = (Math.Sign(AngleFormé(P1, p)) <> Math.Sign(AngleFormé(P2, p)))
            'si le point est à l'intérieur de la voie les angles sont de signe opposé
          End If
        End If
      End If

    End If

  End Function

  Public Function Libellé() As String
    With mBranche
      Dim NomBranche As String = Me.Variante.mBranches.ID(mBranche) & " " & .NomRue
      If Entrante And .NbVoies(TypeVoieEnum.VoieEntrante) = 1 Then
        Libellé = "la branche " & NomBranche
      ElseIf Not Entrante And .NbVoies(TypeVoieEnum.VoieSortante) = 1 Then
        Libellé = "la branche " & NomBranche
      Else
        Libellé = " la voie " & .IndexOfVoie(Me).ToString & " de " & NomBranche
      End If
    End With
  End Function

  Public Overloads Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function

  Public Function AjusterRaccord(ByVal SegmentDépart As Ligne, ByVal SegmentArrivée As Ligne, ByVal SegmentRaccord As Ligne, ByVal Latéralité As Branche.Latéralité, Optional ByVal CoefLargeur As Single = 1) As PointF

    Dim DistanceMini As Single = ToDessin(mBranche.LargeurVoies) * CoefLargeur
    Dim LigneRéférence As Ligne
    If Latéralité = Branche.Latéralité.Aucune Then
      LigneRéférence = Axe
    Else
      LigneRéférence = Bordure(Latéralité)
    End If
    Dim pO As PointF = LigneRéférence.pAF
    Dim pH As PointF = Projection(pO, SegmentRaccord)
    Dim pCNew As PointF

    If SegmentRaccord.PtSurSegment(pH) AndAlso Distance(pO, SegmentRaccord) < DistanceMini + 0.1 Then

      pH = PointTangence(pO, DistanceMini, SegmentDépart, SegmentArrivée)

      If Not pH.IsEmpty Then
        'Redéfinir le nouveau point extrémité de  SegmentDépart et SegmentRaccord 
        Dim pF As PointF = SegmentArrivée.pAF
        pCNew = intersect(New Ligne(pF, pH), SegmentDépart, TypeIntersect:=TypeInterSection.Indifférent)
      End If

    End If

    Return pCNew

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe VoieCollection --------------------------
'=====================================================================================================
Public Class VoieCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim uneVoie As Voie

    With uneRowBranche
      Do Until .GetVoieRows.Length = 0
        ds.Voie.RemoveVoieRow(.GetVoieRows(0))
      Loop
    End With

    For Each uneVoie In Me
      uneVoie.Enregistrer(uneRowBranche)
    Next

  End Sub

  ' Ajouter une voie à la collection.
  Public Function Add(ByVal uneVoie As Voie) As Short
    If uneVoie.Entrante Then
      'Les voies entrantes sont les dernières de la collection
      Me.List.Add(uneVoie)
    Else
      'Les voies sortantes sont les premières de la collection
      Me.List.Insert(0, uneVoie)
    End If
  End Function

  Public Function Add(ByVal Entrante As Boolean, ByVal uneBranche As Branche) As Short
    Add(New Voie(EstEntrante:=Entrante, uneBranche:=uneBranche))
  End Function

  ' Ajouter une plage de voies à la collection.
  Public Sub AddRange(ByVal valeurs() As Voie)
    Me.InnerList.AddRange(valeurs)
  End Sub

  'Insérer une voie dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneVoie As Voie)
    Me.List.Insert(Index, uneVoie)
  End Sub

  ' Supprimer une voie spécifique de la collection.
  Public Sub Remove(ByVal uneVoie As Voie)
    If Me.List.Contains(uneVoie) Then
      Me.List.Remove(uneVoie)
    End If

  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Voie
    Get
      Return CType(Me.List.Item(Index), Voie)
    End Get
  End Property

  Public Function IndexOf(ByVal uneVoie As Voie) As Short
    Return Me.List.IndexOf(uneVoie)
  End Function

  ' Method to check if a person object already exists in the collection.
  Public Function Contains(ByVal uneVoie As Voie) As Boolean
    Return Me.List.Contains(uneVoie)
  End Function

End Class
