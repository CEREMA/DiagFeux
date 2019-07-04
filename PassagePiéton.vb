'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : PassagePi�ton.vb																					'
'						Classes																														'
'							PassagePi�ton																										'
'******************************************************************************

Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe PassagePi�ton --------------------------
'=====================================================================================================
Public Class PassagePi�ton : Inherits M�tier
  'Le passage pi�ton est assimil� � un trap�ze
  'P1-P2 et P3-P4 repr�sentent les 2 petits cot�s non parall�les
  'P2-P3 et P1-P4 les 2 grands cot�s parall�les.
  'R�f. DAF �9.4

  'Points d�crivant le contour trap�zique du passage pi�ton, en coordonn�es r�elles dans le rep�re de la branche
  ' Le contour est d�crit dans le sens trigo et les 2 premiers points sont align�s sur le bord de chauss�e
  Private mPoints() As Drawing.PointF

  '##ModelId=403C81710280
  Public mBranche As Branche
  Public mTravers�e As Travers�ePi�tonne

  Public Const MaxPassages As Short = 2
  Public Const miniLargeur As Single = 2.0
  ' Les bandes des passages pi�tons font une largeur de 50cm et sont espac�es de 50 cm
  Private Const LargeurBandeR�lle As Single = 0.5
  Private Const EspacementBandeR�lle As Single = 0.5

  Private mContour As PolyArc
  Private mZebras As PolyArc
  Private mVoies As New VoieCollection
  Private mVersExt�rieurCarrefour As Boolean

  '********************************************************************************************************************
  ' Enregistrer le passage pi�ton dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
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
  'Identifiant unique pour la variante en vue de la conservation en donn�es persistantes
  'Doit �tre calcul� par sommation sur l'ensemble des branches
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

    D�terminerVoies()

    D�terminerSens()
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

    D�terminerVoies()

    D�terminerSens()

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

    D�terminerSens()
  End Sub

  Private ReadOnly Property Variante() As Variante
    Get
      Return mBranche.Variante
    End Get
  End Property

  Public Sub Recaler(ByVal Diff�rence As Single)
    Dim Index As Short

    For Index = 0 To mPoints.Length - 1
      If mPoints(Index).Y < 0 Then
        mPoints(Index).Y -= Diff�rence
      Else
        mPoints(Index).Y += Diff�rence
      End If
    Next

  End Sub

  Private Sub D�terminerSens()
    'Le passage pi�ton est tjs dessin� dans le sens trigo
    'On d�termine ici si le 1er segment, proche du bord de chauss�e, va vers l'ext�rieur du carrefour ou vers l'int�rieur
    With mBranche
      If Math.Abs(AngleForm�(PointDessin(.PtRep�reG�n�ral(mPoints(0))), PointDessin(.PtRep�reG�n�ral(mPoints(1)))) - mBranche.AngleEnRadians) < 0.1 Then
        mVersExt�rieurCarrefour = True
      Else
        'Le segment et la branche sont de sens oppos� (diff�rence =PI)
        mVersExt�rieurCarrefour = False
      End If
    End With
  End Sub
  Public ReadOnly Property VersExt�rieurCarrefour() As Boolean
    Get
      Return mVersExt�rieurCarrefour
    End Get
  End Property
  ' Convertir les points cliqu�s sur le dessin en points r�els et dans le rep�re de la branche
  Public Sub AffecterPoint(ByVal pDessin As Point, ByVal Index As Short, Optional ByVal Red�finirVoies As Boolean = False)

    Try
      With mBranche
        mPoints(Index) = ChangementRep�re(.Origine, .Angle, PointR�el(pDessin))
      End With

      ' Rechercher les voies intercept�es par la passage pi�ton
      If Red�finirVoies Then D�terminerVoies()

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PassagePi�ton.AffecterPoint")
    End Try
  End Sub

  '**************************************************************************
  ' D�terminer les voies qui sont travers�es par le passage pi�ton
  '**************************************************************************
  Public Sub D�terminerVoies()
    Dim uneVoie As Voie
    With mBranche
      'Convertir les cordonn�es du passage, d�finies dans le rep�re de la branche,dans le rep�re g�n�ral, puis en coordonn�es dessin
      Dim P1Dessin, P2Dessin As Point
      P1Dessin = PointDessin(.PtRep�reG�n�ral(mPoints(1)))
      P2Dessin = PointDessin(.PtRep�reG�n�ral(mPoints(2)))
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

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim Index As Short

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetM�tier = Me
    Dim unePlume As Pen = Nothing
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.PassageContour).Clone
    End If
    Dim pDessin(4 * PasPassage - 1) As Point

    With mBranche
      For Index = 0 To 3
        'Les points d�crivant le contour du passage sont dans le rep�re de la branche : 
        'il faut les changer de rep�re avant de les convertir en coordonn�es dessin
        pDessin(Index * PasPassage) = PointDessin(.PtRep�reG�n�ral(mPoints(Index)))
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
      mGraphique.Add(Cr�erZebras(), Poign�esACr�er:=False)
    End If

    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  Private Function Cr�erZebras() As PolyArc
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    Dim pDessin() As PointF = mContour.Points
    Dim EspacementBande As Single = EspacementBandeR�lle * Echelle
    Dim LargeurBande As Single = LargeurBandeR�lle * Echelle
    Dim Cot�1 As Ligne = New Ligne(pDessin(Index(1)), pDessin(Index(2)))
    Dim Cot�2 As Ligne = New Ligne(pDessin(Index(0)), pDessin(Index(3)))
    Dim BordOppos� As Ligne = New Ligne(pDessin(Index(2)), pDessin(Index(3)))
    Dim AnglePassage As Single = AngleForm�(Cot�2)
    Dim pOrigine, pProjet� As PointF
    Dim AB, CD As Ligne

    'D�marrer le dessin des bandes � l'angle du 1er point
    pOrigine = pDessin(Index(0))
    pProjet� = Projection(pOrigine, pDessin(Index(1)), AnglePassage)

    Dim EpaisseurPassage As Single = Distance(pOrigine, pProjet�)

    If Distance(pProjet�, Cot�1.pBF) > Cot�1.Longueur Then
      'la 1�re bande sort du passage : on la d�cale de la longueur n�cessaire
      pOrigine = Translation(pOrigine, New Vecteur(pProjet�, pDessin(Index(1))))
      pProjet� = Projection(pOrigine, pDessin(Index(1)), AnglePassage)
    End If

    Dim LgPassage As Single = Math.Max(Distance(pOrigine, pDessin(Index(3))), Distance(pProjet�, pDessin(Index(2))))
    Dim LgParcourue As Single '= LargeurBande

    'D�finir un rectangle ABCD contour d'une bande de passage pi�ton
    ' AB : Segment proche du bord de chauss�e
    Dim pa As New PointF(0, 0)
    Dim pb As New PointF(0, EpaisseurPassage)
    Dim pc As New PointF(LargeurBande, EpaisseurPassage)
    Dim pd As New PointF(LargeurBande, 0)

    Dim unPolyArc As PolyArc
    'D�finition du vecteur de translation pour le dessin de la bande suivante
    Dim VTranslation As Vecteur = New Vecteur(2 * LargeurBande, AnglePassage)

    'Faire tourner le rectangle autour de son sommet (0,0) et le translater en pOrigine
    pa = Translation(pa, pOrigine)  ' pa est d�j� en 0,0 : rotation inutile
    pb = RotTrans(pb, pOrigine, AnglePassage)
    pc = RotTrans(pc, pOrigine, AnglePassage)
    pd = RotTrans(pd, pOrigine, AnglePassage)
    AB = New Ligne(pa, pb)
    CD = New Ligne(pc, pd)

    Dim pts(3) As PointF

    mZebras = Nothing
    mZebras = New PolyArc

    Do
      ' Dessiner les bandes par d�calage depuis le d�part jusqu'� rencontrer le bord oppos� du contour

      'Tronquer si n�cessaire la bande au niveau du contour du passage
      CD.pAF = PointZebra(CD, Cot�1, BordOppos�, PremierPoint:=True)
      CD.pBF = PointZebra(CD, Cot�2, BordOppos�, PremierPoint:=False)
      AB.pAF = PointZebra(AB, Cot�2, BordOppos�, PremierPoint:=True)
      AB.pBF = PointZebra(AB, Cot�1, BordOppos�, PremierPoint:=False)

      'Cr�er le rectangle graphiquement
      pts(0) = AB.pAF
      pts(1) = AB.pBF
      pts(2) = CD.pAF
      pts(3) = CD.pBF
      unPolyArc = New PolyArc(pts, Clore:=False)
      mZebras.Add(unPolyArc, Poign�esACr�er:=False)
      unPolyArc.APeindre = True

      'D�finir la bande suivante
      AB = AB.Translation(VTranslation)
      CD = CD.Translation(VTranslation)
      LgParcourue += 2 * LargeurBande
    Loop While LgParcourue < LgPassage

    'Repartir du d�part du trac� des bandes, pour dessiner le triangle manquant pr�c�dent la 1�re bande
    LgPassage = Math.Max(Distance(pOrigine, pDessin(Index(0))), Distance(pProjet�, pDessin(Index(1))))
    LgParcourue = 0
    AB.pAF = pa
    AB.pBF = pb
    CD.pAF = pc
    CD.pBF = pd

    VTranslation = New Vecteur(2 * LargeurBande, AnglePassage + CType(Math.PI, Single))
    BordOppos� = New Ligne(pDessin(Index(0)), pDessin(Index(1)))

    Do While LgParcourue < LgPassage
      AB = AB.Translation(VTranslation)
      CD = CD.Translation(VTranslation)

      CD.pAF = PointZebra(CD, Cot�1.Invers�e, BordOppos�, PremierPoint:=True)
      CD.pBF = PointZebra(CD, Cot�2.Invers�e, BordOppos�, PremierPoint:=False)
      AB.pAF = PointZebra(AB, Cot�2.Invers�e, BordOppos�, PremierPoint:=True)
      AB.pBF = PointZebra(AB, Cot�1.Invers�e, BordOppos�, PremierPoint:=False)

      'Cr�er le rectangle graphiquement

      pts(0) = AB.pAF
      pts(1) = AB.pBF
      pts(2) = CD.pAF
      pts(3) = CD.pBF
      unPolyArc = New PolyArc(pts, Clore:=False)
      mZebras.Add(unPolyArc, Poign�esACr�er:=False)
      unPolyArc.APeindre = True

      LgParcourue += 2 * LargeurBande
    Loop

    Return mZebras

  End Function

  Private Function PointZebra(ByVal Segment As Ligne, ByVal uneLigne As Ligne, ByVal BordOppos� As Ligne, ByVal PremierPoint As Boolean) As PointF
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
      p = intersect(Segment, BordOppos�)
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

  Public Function MouvementPossible(ByVal pEnCours As Point, ByRef numPoign�e As Short) As frmCarrefour.CommandeGraphique
    Dim unPolyarc As PolyArc = CType(mGraphique(0), PolyArc)
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    ' Faire une premi�re recherche sur les points extr�mit�s du passage
    For numPoign�e = 0 To unPolyarc.NbPoign�es - 1
      If Distance(pEnCours, unPolyarc.Poign�e(numPoign�e)) < RayS�lect Then
        If PasPassage = 1 Then
          Return frmCarrefour.CommandeGraphique.EditPointPassage
        Else
          If numPoign�e Mod 2 = 0 Then
            Return frmCarrefour.CommandeGraphique.EditPointPassage
          ElseIf (numPoign�e - 1) Mod 4 = 0 Then
            Return frmCarrefour.CommandeGraphique.EditLongueurPassage
          Else
            Return frmCarrefour.CommandeGraphique.EditLargeurPassage
          End If
        End If
      End If
    Next

    'Faire une recherche sur les bords
    Dim Ligne1 As Ligne = New Ligne(mContour.Points(Index(1)), mContour.Points(Index(2))) ' Cot� parall�le (int�rieur au carrefour ???)
    Dim Ligne2 As Ligne = New Ligne(mContour.Points(Index(0)), mContour.Points(Index(3)))  ' Autre Cot� parall�le

    Dim d1, d2 As Single

    d1 = Distance(pEnCours, Ligne1)
    d2 = Distance(pEnCours, Ligne2)
    If d1 < RayS�lect Then
      If Distance(pEnCours, Ligne1.pA) < Distance(pEnCours, Ligne1.pB) Then
        numPoign�e = 1 * PasPassage
      Else
        numPoign�e = 2 * PasPassage
      End If
      Return frmCarrefour.CommandeGraphique.EditAnglePassage

    ElseIf d2 < RayS�lect Then
      If Distance(pEnCours, Ligne2.pA) < Distance(pEnCours, Ligne2.pB) Then
        numPoign�e = 0
      Else
        numPoign�e = 3 * PasPassage
      End If
      Return frmCarrefour.CommandeGraphique.EditAnglePassage
    End If

    If mContour.Int�rieur(pEnCours) Then
      Return frmCarrefour.CommandeGraphique.D�placerPassage
    End If
  End Function

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    mGraphique.RendreS�lectable(Not Verrouillage)
    mContour.Invisible = Verrouillage
  End Sub

  Public Function PtInt�rieur(ByVal pSouris As Point) As Boolean
    Dim uneFigure As PolyArc = mGraphique(0)
    Return uneFigure.Int�rieur(pSouris)
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

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim unPassage As PassagePi�ton
    With uneRowBranche
      Do Until .GetPassageRows.Length = 0
        ds.Passage.RemovePassageRow(.GetPassageRows(0))
      Loop
      For Each unPassage In Me
        unPassage.Enregistrer(uneRowBranche)
      Next

    End With
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unPassage As PassagePi�ton) As Short
    Return Me.List.Add(unPassage)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal colPassages As PassageCollection)
    Me.InnerList.AddRange(colPassages)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal unPassage As PassagePi�ton)
    If Me.List.Contains(unPassage) Then
      Me.List.Remove(unPassage)
    End If
  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unPassage As PassagePi�ton)
    Me.List.Insert(Index, unPassage)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As PassagePi�ton
    Get
      Return CType(Me.List.Item(Index), PassagePi�ton)
    End Get
  End Property

  Public Function IndexOf(ByVal unPassage As PassagePi�ton) As Short
    Return Me.List.IndexOf(unPassage)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Function Contains(ByVal unPassage As PassagePi�ton) As Boolean
    Return Me.List.Contains(unPassage)
  End Function

End Class

