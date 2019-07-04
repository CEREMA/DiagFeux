'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : PassagePiéton.vb																					'
'						Classes																														'
'							PassagePiéton																										'
'******************************************************************************

Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe PassagePiéton --------------------------
'=====================================================================================================
Public Class PassagePiéton : Inherits Métier
  'Le passage piéton est assimilé à un trapèze
  'P1-P2 et P3-P4 représentent les 2 petits cotés non parallèles
  'P2-P3 et P1-P4 les 2 grands cotés parallèles.
  'Réf. DAF §9.4

  'Points décrivant le contour trapézique du passage piéton, en coordonnées réelles dans le repère de la branche
  ' Le contour est décrit dans le sens trigo et les 2 premiers points sont alignés sur le bord de chaussée
  Private mPoints() As Drawing.PointF

  '##ModelId=403C81710280
  Public mBranche As Branche
  Public mTraversée As TraverséePiétonne

  Public Const MaxPassages As Short = 2
  Public Const miniLargeur As Single = 2.0
  ' Les bandes des passages piétons font une largeur de 50cm et sont espacées de 50 cm
  Private Const LargeurBandeRélle As Single = 0.5
  Private Const EspacementBandeRélle As Single = 0.5

  Private mContour As PolyArc
  Private mZebras As PolyArc
  Private mVoies As New VoieCollection
  Private mVersExtérieurCarrefour As Boolean

  '********************************************************************************************************************
  ' Enregistrer le passage piéton dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim uneRowPassage As DataSetDiagfeux.PassageRow
    Dim i As Short
    Dim uneVoie As Voie

    uneRowPassage = ds.Passage.AddPassageRow(uneRowBranche)

    With uneRowPassage
      For i = 0 To mPoints.Length - 1
        ds.Point.AddPointRow(mPoints(i).X, mPoints(i).Y, uneRowPassage)
      Next
      For Each uneVoie In mVoies
        ds.VoieIntersectee.AddVoieIntersecteeRow(mBranche.Voies.IndexOf(uneVoie), uneRowPassage)
      Next
    End With

  End Sub

  '********************************************************************************************************************
  'Identifiant unique pour la variante en vue de la conservation en données persistantes
  'Doit être calculé par sommation sur l'ensemble des branches
  '********************************************************************************************************************
  Public Function ID() As Short
    Dim numBranche As Short = Me.Variante.mBranches.IndexOf(mBranche)
    Dim IndexPassage As Short = mBranche.mPassages.IndexOf(Me)

    ID = IndexPassage + numBranche * MaxPassages

  End Function

  Public Sub New(ByVal uneBranche As Branche, ByVal pDessin() As Point)
    Dim Index As Short

    mBranche = uneBranche
    ReDim mPoints(pDessin.Length - 1)

    For Index = 0 To pDessin.Length - 1
      AffecterPoint(pDessin(Index), Index)
    Next

    DéterminerVoies()

    DéterminerSens()
  End Sub

  Public Sub New(ByVal uneBranche As Branche)
    Dim DemiLargeur As Single = uneBranche.Largeur / 2

    mBranche = uneBranche
    ReDim mPoints(3)
    mPoints(0).X = 5
    mPoints(0).Y = DemiLargeur
    mPoints(1).X = 0
    mPoints(1).Y = DemiLargeur
    mPoints(2).X = 0
    mPoints(2).Y = -DemiLargeur
    mPoints(3).X = 5
    mPoints(3).Y = -DemiLargeur

    DéterminerVoies()

    DéterminerSens()

  End Sub

  Public Sub New(ByVal uneBranche As Branche, ByVal uneRowPassage As DataSetDiagfeux.PassageRow)
    Dim i As Short
    Dim uneVoie As Voie

    mBranche = uneBranche

    With uneRowPassage
      ReDim mPoints(.GetPointRows.Length - 1) 'en principe toujours 4 points
      For i = 0 To mPoints.Length - 1
        mPoints(i).X = .GetPointRows(i).X
        mPoints(i).Y = .GetPointRows(i).Y
      Next
      For i = 0 To .GetVoieIntersecteeRows.Length - 1
        uneVoie = mBranche.Voies(.GetVoieIntersecteeRows(i).VoieIntersectee_Column)
        mVoies.Add(uneVoie)
      Next

    End With

    DéterminerSens()
  End Sub

  Private ReadOnly Property Variante() As Variante
    Get
      Return mBranche.Variante
    End Get
  End Property

  Public Sub Recaler(ByVal Différence As Single)
    Dim Index As Short

    For Index = 0 To mPoints.Length - 1
      If mPoints(Index).Y < 0 Then
        mPoints(Index).Y -= Différence
      Else
        mPoints(Index).Y += Différence
      End If
    Next

  End Sub

  Private Sub DéterminerSens()
    'Le passage piéton est tjs dessiné dans le sens trigo
    'On détermine ici si le 1er segment, proche du bord de chaussée, va vers l'extérieur du carrefour ou vers l'intérieur
    With mBranche
      If Math.Abs(AngleFormé(PointDessin(.PtRepèreGénéral(mPoints(0))), PointDessin(.PtRepèreGénéral(mPoints(1)))) - mBranche.AngleEnRadians) < 0.1 Then
        mVersExtérieurCarrefour = True
      Else
        'Le segment et la branche sont de sens opposé (différence =PI)
        mVersExtérieurCarrefour = False
      End If
    End With
  End Sub
  Public ReadOnly Property VersExtérieurCarrefour() As Boolean
    Get
      Return mVersExtérieurCarrefour
    End Get
  End Property
  ' Convertir les points cliqués sur le dessin en points réels et dans le repère de la branche
  Public Sub AffecterPoint(ByVal pDessin As Point, ByVal Index As Short, Optional ByVal RedéfinirVoies As Boolean = False)

    Try
      With mBranche
        mPoints(Index) = ChangementRepère(.Origine, .Angle, PointRéel(pDessin))
      End With

      ' Rechercher les voies interceptées par la passage piéton
      If RedéfinirVoies Then DéterminerVoies()

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PassagePiéton.AffecterPoint")
    End Try
  End Sub

  '**************************************************************************
  ' Déterminer les voies qui sont traversées par le passage piéton
  '**************************************************************************
  Public Sub DéterminerVoies()
    Dim uneVoie As Voie
    With mBranche
      'Convertir les cordonnées du passage, définies dans le repère de la branche,dans le repère général, puis en coordonnées dessin
      Dim P1Dessin, P2Dessin As Point
      P1Dessin = PointDessin(.PtRepèreGénéral(mPoints(1)))
      P2Dessin = PointDessin(.PtRepèreGénéral(mPoints(2)))
      Dim l1 As New Ligne(P1Dessin, P2Dessin)
      mVoies.Clear()
      For Each uneVoie In mBranche.Voies
        If Not intersect(l1, uneVoie.Axe, Formules.TypeInterSection.SurSegment).IsEmpty Then
          mVoies.Add(uneVoie)
        End If
      Next
    End With

  End Sub

  Public ReadOnly Property Points() As PointF()
    Get
      Return (mPoints)
    End Get
  End Property

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim Index As Short

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetMétier = Me
    Dim unePlume As Pen = Nothing
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.PassageContour).Clone
    End If
    Dim pDessin(4 * PasPassage - 1) As Point

    With mBranche
      For Index = 0 To 3
        'Les points décrivant le contour du passage sont dans le repère de la branche : 
        'il faut les changer de repère avant de les convertir en coordonnées dessin
        pDessin(Index * PasPassage) = PointDessin(.PtRepèreGénéral(mPoints(Index)))
      Next
      If PasPassage = 2 Then
        For Index = 0 To 3
          pDessin(Index * 2 + 1) = Milieu(pDessin(Index * 2), pDessin(((Index + 1) * 2) Mod 8))
        Next
      End If
    End With

    mContour = New PolyArc(pDessin, Clore:=True)
    mContour.Plume = unePlume
    mGraphique.Add(mContour)

    If IsNothing(Me.Variante.mFondDePlan) OrElse Not Me.Variante.mFondDePlan.Visible Then
      mGraphique.Add(CréerZebras(), PoignéesACréer:=False)
    End If

    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  Private Function CréerZebras() As PolyArc
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    Dim pDessin() As PointF = mContour.Points
    Dim EspacementBande As Single = EspacementBandeRélle * Echelle
    Dim LargeurBande As Single = LargeurBandeRélle * Echelle
    Dim Coté1 As Ligne = New Ligne(pDessin(Index(1)), pDessin(Index(2)))
    Dim Coté2 As Ligne = New Ligne(pDessin(Index(0)), pDessin(Index(3)))
    Dim BordOpposé As Ligne = New Ligne(pDessin(Index(2)), pDessin(Index(3)))
    Dim AnglePassage As Single = AngleFormé(Coté2)
    Dim pOrigine, pProjeté As PointF
    Dim AB, CD As Ligne

    'Démarrer le dessin des bandes à l'angle du 1er point
    pOrigine = pDessin(Index(0))
    pProjeté = Projection(pOrigine, pDessin(Index(1)), AnglePassage)

    Dim EpaisseurPassage As Single = Distance(pOrigine, pProjeté)

    If Distance(pProjeté, Coté1.pBF) > Coté1.Longueur Then
      'la 1ère bande sort du passage : on la décale de la longueur nécessaire
      pOrigine = Translation(pOrigine, New Vecteur(pProjeté, pDessin(Index(1))))
      pProjeté = Projection(pOrigine, pDessin(Index(1)), AnglePassage)
    End If

    Dim LgPassage As Single = Math.Max(Distance(pOrigine, pDessin(Index(3))), Distance(pProjeté, pDessin(Index(2))))
    Dim LgParcourue As Single '= LargeurBande

    'Définir un rectangle ABCD contour d'une bande de passage piéton
    ' AB : Segment proche du bord de chaussée
    Dim pa As New PointF(0, 0)
    Dim pb As New PointF(0, EpaisseurPassage)
    Dim pc As New PointF(LargeurBande, EpaisseurPassage)
    Dim pd As New PointF(LargeurBande, 0)

    Dim unPolyArc As PolyArc
    'Définition du vecteur de translation pour le dessin de la bande suivante
    Dim VTranslation As Vecteur = New Vecteur(2 * LargeurBande, AnglePassage)

    'Faire tourner le rectangle autour de son sommet (0,0) et le translater en pOrigine
    pa = Translation(pa, pOrigine)  ' pa est déjà en 0,0 : rotation inutile
    pb = RotTrans(pb, pOrigine, AnglePassage)
    pc = RotTrans(pc, pOrigine, AnglePassage)
    pd = RotTrans(pd, pOrigine, AnglePassage)
    AB = New Ligne(pa, pb)
    CD = New Ligne(pc, pd)

    Dim pts(3) As PointF

    mZebras = Nothing
    mZebras = New PolyArc

    Do
      ' Dessiner les bandes par décalage depuis le départ jusqu'à rencontrer le bord opposé du contour

      'Tronquer si nécessaire la bande au niveau du contour du passage
      CD.pAF = PointZebra(CD, Coté1, BordOpposé, PremierPoint:=True)
      CD.pBF = PointZebra(CD, Coté2, BordOpposé, PremierPoint:=False)
      AB.pAF = PointZebra(AB, Coté2, BordOpposé, PremierPoint:=True)
      AB.pBF = PointZebra(AB, Coté1, BordOpposé, PremierPoint:=False)

      'Créer le rectangle graphiquement
      pts(0) = AB.pAF
      pts(1) = AB.pBF
      pts(2) = CD.pAF
      pts(3) = CD.pBF
      unPolyArc = New PolyArc(pts, Clore:=False)
      mZebras.Add(unPolyArc, PoignéesACréer:=False)
      unPolyArc.APeindre = True

      'Définir la bande suivante
      AB = AB.Translation(VTranslation)
      CD = CD.Translation(VTranslation)
      LgParcourue += 2 * LargeurBande
    Loop While LgParcourue < LgPassage

    'Repartir du départ du tracé des bandes, pour dessiner le triangle manquant précédent la 1ère bande
    LgPassage = Math.Max(Distance(pOrigine, pDessin(Index(0))), Distance(pProjeté, pDessin(Index(1))))
    LgParcourue = 0
    AB.pAF = pa
    AB.pBF = pb
    CD.pAF = pc
    CD.pBF = pd

    VTranslation = New Vecteur(2 * LargeurBande, AnglePassage + CType(Math.PI, Single))
    BordOpposé = New Ligne(pDessin(Index(0)), pDessin(Index(1)))

    Do While LgParcourue < LgPassage
      AB = AB.Translation(VTranslation)
      CD = CD.Translation(VTranslation)

      CD.pAF = PointZebra(CD, Coté1.Inversée, BordOpposé, PremierPoint:=True)
      CD.pBF = PointZebra(CD, Coté2.Inversée, BordOpposé, PremierPoint:=False)
      AB.pAF = PointZebra(AB, Coté2.Inversée, BordOpposé, PremierPoint:=True)
      AB.pBF = PointZebra(AB, Coté1.Inversée, BordOpposé, PremierPoint:=False)

      'Créer le rectangle graphiquement

      pts(0) = AB.pAF
      pts(1) = AB.pBF
      pts(2) = CD.pAF
      pts(3) = CD.pBF
      unPolyArc = New PolyArc(pts, Clore:=False)
      mZebras.Add(unPolyArc, PoignéesACréer:=False)
      unPolyArc.APeindre = True

      LgParcourue += 2 * LargeurBande
    Loop

    Return mZebras

  End Function

  Private Function PointZebra(ByVal Segment As Ligne, ByVal uneLigne As Ligne, ByVal BordOpposé As Ligne, ByVal PremierPoint As Boolean) As PointF
    Dim p As PointF = IIf(PremierPoint, Segment.pAF, Segment.pBF)

    If Distance(p, uneLigne) > 0 Then
      p = Projection(p, uneLigne)
      If PremierPoint Then
        Segment.pAF = p
      Else
        Segment.pBF = p
      End If
    End If

    If Distance(p, uneLigne.pAF) > uneLigne.Longueur Then
      p = intersect(Segment, BordOpposé)
      If p.IsEmpty Then
        p = uneLigne.pBF
      Else
        If PremierPoint Then
          Segment.pAF = p
        Else
          Segment.pBF = p
        End If
        If Segment.Longueur = 0 Then
          p = uneLigne.pBF
        End If
      End If
    End If

    Return p

  End Function

  Public ReadOnly Property Contour() As PolyArc
    Get
      Return mContour
    End Get
  End Property

  Public ReadOnly Property Zebras() As PolyArc
    Get
      Return mZebras
    End Get
  End Property

  Public Function MouvementPossible(ByVal pEnCours As Point, ByRef numPoignée As Short) As frmCarrefour.CommandeGraphique
    Dim unPolyarc As PolyArc = CType(mGraphique(0), PolyArc)
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    ' Faire une première recherche sur les points extrémités du passage
    For numPoignée = 0 To unPolyarc.NbPoignées - 1
      If Distance(pEnCours, unPolyarc.Poignée(numPoignée)) < RaySélect Then
        If PasPassage = 1 Then
          Return frmCarrefour.CommandeGraphique.EditPointPassage
        Else
          If numPoignée Mod 2 = 0 Then
            Return frmCarrefour.CommandeGraphique.EditPointPassage
          ElseIf (numPoignée - 1) Mod 4 = 0 Then
            Return frmCarrefour.CommandeGraphique.EditLongueurPassage
          Else
            Return frmCarrefour.CommandeGraphique.EditLargeurPassage
          End If
        End If
      End If
    Next

    'Faire une recherche sur les bords
    Dim Ligne1 As Ligne = New Ligne(mContour.Points(Index(1)), mContour.Points(Index(2))) ' Coté parallèle (intérieur au carrefour ???)
    Dim Ligne2 As Ligne = New Ligne(mContour.Points(Index(0)), mContour.Points(Index(3)))  ' Autre Coté parallèle

    Dim d1, d2 As Single

    d1 = Distance(pEnCours, Ligne1)
    d2 = Distance(pEnCours, Ligne2)
    If d1 < RaySélect Then
      If Distance(pEnCours, Ligne1.pA) < Distance(pEnCours, Ligne1.pB) Then
        numPoignée = 1 * PasPassage
      Else
        numPoignée = 2 * PasPassage
      End If
      Return frmCarrefour.CommandeGraphique.EditAnglePassage

    ElseIf d2 < RaySélect Then
      If Distance(pEnCours, Ligne2.pA) < Distance(pEnCours, Ligne2.pB) Then
        numPoignée = 0
      Else
        numPoignée = 3 * PasPassage
      End If
      Return frmCarrefour.CommandeGraphique.EditAnglePassage
    End If

    If mContour.Intérieur(pEnCours) Then
      Return frmCarrefour.CommandeGraphique.DéplacerPassage
    End If
  End Function

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    mGraphique.RendreSélectable(Not Verrouillage)
    mContour.Invisible = Verrouillage
  End Sub

  Public Function PtIntérieur(ByVal pSouris As Point) As Boolean
    Dim uneFigure As PolyArc = mGraphique(0)
    Return uneFigure.Intérieur(pSouris)
  End Function

  Public ReadOnly Property Voies() As VoieCollection
    Get
      Return mVoies
    End Get
  End Property
End Class

'=====================================================================================================
'--------------------------- Classe PassageCollection --------------------------
'=====================================================================================================
Public Class PassageCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim unPassage As PassagePiéton
    With uneRowBranche
      Do Until .GetPassageRows.Length = 0
        ds.Passage.RemovePassageRow(.GetPassageRows(0))
      Loop
      For Each unPassage In Me
        unPassage.Enregistrer(uneRowBranche)
      Next

    End With
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unPassage As PassagePiéton) As Short
    Return Me.List.Add(unPassage)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal colPassages As PassageCollection)
    Me.InnerList.AddRange(colPassages)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unPassage As PassagePiéton)
    If Me.List.Contains(unPassage) Then
      Me.List.Remove(unPassage)
    End If
  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unPassage As PassagePiéton)
    Me.List.Insert(Index, unPassage)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As PassagePiéton
    Get
      Return CType(Me.List.Item(Index), PassagePiéton)
    End Get
  End Property

  Public Function IndexOf(ByVal unPassage As PassagePiéton) As Short
    Return Me.List.IndexOf(unPassage)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal unPassage As PassagePiéton) As Boolean
    Return Me.List.Contains(unPassage)
  End Function

End Class

