'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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
Public Class Voie : Inherits M�tier

  'Voie de circulation sur une branche
  Public mBranche As Branche

  'Une voie peut �tre entrante ou sortante
  Public Entrante As Boolean
  Public Const MaxVoies As Short = 8

  Private mBordure(1) As Ligne
  Private mAxe As Ligne
  Private mExtr�mit�(1) As PointF

  'Courants parcourus par la voie si elle est entrante
  Public mCourants As CourantCollection

  Public Enum TypeVoieEnum
    VoieEntrante
    VoieSortante
    VoieQuelconque
  End Enum

  Public Enum Extr�mit�Enum
    Ext�rieur
    Int�rieur
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
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
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
  'Identifiant unique pour la variante en vue de la conservation en donn�es persistantes
  'Doit �tre calcul� par sommation sur l'ensemble des branches
  '********************************************************************************************************************
  Public Function ID() As Short
    Dim numBranche As Short = Me.Variante.mBranches.IndexOf(mBranche)
    Dim IndexVoie As Short = mBranche.Voies.IndexOf(Me)

    ID = IndexVoie + numBranche * MaxVoies

  End Function

  Public Overloads Sub Cr�erGraphique(ByVal L1 As Ligne, ByVal L2 As Ligne)
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(Nothing, mGraphique)

    'La 1�re ligne est la ligne est la ligne la plus � droite dans le rep�re de la branche (orient�e vers l'ext�rieur du carrefour)
    Dim i As Branche.Lat�ralit�

    mBordure(Branche.Lat�ralit�.Droite) = L1
    mBordure(Branche.Lat�ralit�.Gauche) = L2
    'On indique ici l'axe en coordonn�es Flottantes, car cet �l�ment sert au dessin des trajectoires (les recherches d'intesection peuvent �chouer � un epsilon pr�s : Fonction TrajectoireV�hicules.InitGraphique)
    mAxe = New Ligne(Milieu(L1.pAF, L2.pAF), Milieu(L1.pBF, L2.pBF))
    For i = Branche.Lat�ralit�.Droite To Branche.Lat�ralit�.Gauche
      mGraphique.Add(mBordure(i), Poign�esACr�er:=False)
      mExtr�mit�(i) = mBordure(i).pAF
    Next

  End Sub

  Public Function MilieuExtr�mit�(ByVal Cot� As Extr�mit�Enum) As Point

    If Cot� = Extr�mit�Enum.Int�rieur Then
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

  Public ReadOnly Property Bordure(ByVal Index As Branche.Lat�ralit�) As Ligne
    Get
      Return mBordure(Index)
    End Get
  End Property

  Public ReadOnly Property Extr�mit�(ByVal Index As Branche.Lat�ralit�) As PointF
    Get
      Return mExtr�mit�(Index)
    End Get
  End Property

  Public Function PtInt�rieur(ByVal p As Point, Optional ByVal Strict As Boolean = True) As Boolean
    Dim L1 As Ligne = Bordure(Branche.Lat�ralit�.Droite)
    Dim L2 As Ligne = Bordure(Branche.Lat�ralit�.Gauche)
    Dim P1, P2, pOrigine1, pOrigine2 As Point

    pOrigine1 = L1.pA
    pOrigine2 = L2.pA
    'V�rifier que tous les points sont � l'int�rieur de la branche (c'est � dire entre les 2 bords de chauss�e)
    'projeter le point sur le bord de chauss�e droite
    P1 = Projection(p, pOrigine1, mBranche.AngleEnRadians)
    If L1.PtSurSegment(P1) Then
      If Distance(p, P1) = 0.0 Then
        'Le point est sur le 1er cot� de la voie
        PtInt�rieur = Not Strict
      Else
        'projeter le point sur le bord de chauss�e gauche
        P2 = Projection(p, pOrigine2, mBranche.AngleEnRadians)
        If L2.PtSurSegment(P2) Then
          If Distance(p, P2) = 0.0 Then
            'Le point est sur le 2�me cot� de la voie
            PtInt�rieur = Not Strict
          Else
            PtInt�rieur = (Math.Sign(AngleForm�(P1, p)) <> Math.Sign(AngleForm�(P2, p)))
            'si le point est � l'int�rieur de la voie les angles sont de signe oppos�
          End If
        End If
      End If

    End If

  End Function

  Public Function Libell�() As String
    With mBranche
      Dim NomBranche As String = Me.Variante.mBranches.ID(mBranche) & " " & .NomRue
      If Entrante And .NbVoies(TypeVoieEnum.VoieEntrante) = 1 Then
        Libell� = "la branche " & NomBranche
      ElseIf Not Entrante And .NbVoies(TypeVoieEnum.VoieSortante) = 1 Then
        Libell� = "la branche " & NomBranche
      Else
        Libell� = " la voie " & .IndexOfVoie(Me).ToString & " de " & NomBranche
      End If
    End With
  End Function

  Public Overloads Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function

  Public Function AjusterRaccord(ByVal SegmentD�part As Ligne, ByVal SegmentArriv�e As Ligne, ByVal SegmentRaccord As Ligne, ByVal Lat�ralit� As Branche.Lat�ralit�, Optional ByVal CoefLargeur As Single = 1) As PointF

    Dim DistanceMini As Single = ToDessin(mBranche.LargeurVoies) * CoefLargeur
    Dim LigneR�f�rence As Ligne
    If Lat�ralit� = Branche.Lat�ralit�.Aucune Then
      LigneR�f�rence = Axe
    Else
      LigneR�f�rence = Bordure(Lat�ralit�)
    End If
    Dim pO As PointF = LigneR�f�rence.pAF
    Dim pH As PointF = Projection(pO, SegmentRaccord)
    Dim pCNew As PointF

    If SegmentRaccord.PtSurSegment(pH) AndAlso Distance(pO, SegmentRaccord) < DistanceMini + 0.1 Then

      pH = PointTangence(pO, DistanceMini, SegmentD�part, SegmentArriv�e)

      If Not pH.IsEmpty Then
        'Red�finir le nouveau point extr�mit� de  SegmentD�part et SegmentRaccord 
        Dim pF As PointF = SegmentArriv�e.pAF
        pCNew = intersect(New Ligne(pF, pH), SegmentD�part, TypeIntersect:=TypeInterSection.Indiff�rent)
      End If

    End If

    Return pCNew

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe VoieCollection --------------------------
'=====================================================================================================
Public Class VoieCollection : Inherits CollectionBase

  ' Cr�er une instance la collection
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

  ' Ajouter une voie � la collection.
  Public Function Add(ByVal uneVoie As Voie) As Short
    If uneVoie.Entrante Then
      'Les voies entrantes sont les derni�res de la collection
      Me.List.Add(uneVoie)
    Else
      'Les voies sortantes sont les premi�res de la collection
      Me.List.Insert(0, uneVoie)
    End If
  End Function

  Public Function Add(ByVal Entrante As Boolean, ByVal uneBranche As Branche) As Short
    Add(New Voie(EstEntrante:=Entrante, uneBranche:=uneBranche))
  End Function

  ' Ajouter une plage de voies � la collection.
  Public Sub AddRange(ByVal valeurs() As Voie)
    Me.InnerList.AddRange(valeurs)
  End Sub

  'Ins�rer une voie dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneVoie As Voie)
    Me.List.Insert(Index, uneVoie)
  End Sub

  ' Supprimer une voie sp�cifique de la collection.
  Public Sub Remove(ByVal uneVoie As Voie)
    If Me.List.Contains(uneVoie) Then
      Me.List.Remove(uneVoie)
    End If

  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
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
