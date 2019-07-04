'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Ilot.vb																									'
'						Classes																														'
'							Ilot																														'
'******************************************************************************
Option Strict Off
Option Explicit On

'=====================================================================================================
'--------------------------- Classe Ilot --------------------------
'=====================================================================================================
Public Class Ilot : Inherits Métier

  Private Const défautLargeur As Single = 3.0
  Private Const défautRayon As Single = 3.0
  Private Const défautDécalage As Single = 2.0
  Private Const défautRetrait As Single = 4.0

  Public Const miniLargeur As Single = 1.0
  Public Const maxiLargeur As Single = 30.0
  Public Const miniRayon As Single = 1.0
  Public Const maxiRayon As Single = 99.0
  Public Const miniDécalAxe As Single = 0.0
  'Le maximum du décalage est la largeur de la branche
  Public Const miniRetrait As Single = -10.0
  Public Const maxiRetrait As Single = 10.0

  'Largeur de l'îlot
  '##ModelId=4032308B0399
  Private mLargeur As Single

  'Rayon de l'îlot
  '##ModelId=403231050000
  Private mRayon As Single

  'Décalage de l'axe de symétrie de l'îlot  avec le bord droit de la branche.
  'Toujours positif : l'origine de la branche est l'extrémité intérieure de la branche(côté carrefour)
  'Réf : Cahier des charges du 16/04/03 §3
  '##ModelId=403231180109
  Private mDécalage As Single

  'Retrait de l'îlot par rapport à l'origine de la branche
  'Réf : Cahier des charges du 16/04/03 §3
  '##ModelId=403231E700BB
  Private mRetrait As Single

  '##ModelId=3C70D1A1004F
  Public mBranche As Branche

  Public Sub New(ByVal uneBranche As Branche)
    mBranche = uneBranche
    uneBranche.mIlot = Me

    mLargeur = défautLargeur
    mRayon = défautRayon
    mDécalage = défautDécalage
    mRetrait = défautRetrait

  End Sub

  Public Sub New(ByVal uneBranche As Branche, ByVal Largeur As Single, ByVal Rayon As Single, ByVal Décalage As Single, ByVal Retrait As Single)
    mBranche = uneBranche
    uneBranche.mIlot = Me

    mLargeur = Largeur
    mRayon = Rayon
    mDécalage = Décalage
    mRetrait = Retrait

  End Sub

  Public Property Largeur() As Single
    Get
      Return mLargeur
    End Get
    Set(ByVal Value As Single)
      mLargeur = Value
    End Set
  End Property

  Public Property Rayon() As Single
    Get
      Return mRayon
    End Get
    Set(ByVal Value As Single)
      mRayon = Value
    End Set
  End Property

  Public Property Décalage() As Single
    Get
      Return mDécalage
    End Get
    Set(ByVal Value As Single)
      mDécalage = Value
    End Set
  End Property

  Public Property Retrait() As Single
    Get
      Return mRetrait
    End Get
    Set(ByVal Value As Single)
      mRetrait = Value
    End Set
  End Property

  Public Overridable Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal Séparateur As Char) As String
    Dim strLigne As String

    With Me
      Dim uneBranche As Branche = .mBranche

      strLigne = .Rayon & Séparateur
      strLigne &= .Décalage & Séparateur
      strLigne &= .Largeur & Séparateur
      strLigne &= .Retrait & Séparateur
    End With

    Return strLigne

  End Function

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetMétier = Me

    Dim xP1, yP1, xP2, yP2, xP3, yP3, xP4, yP4 As Single
    Dim LgCarrefour As Single = mBranche.NbVoies * mBranche.LargeurVoies
    xP2 = mRetrait
    xP3 = xP2
    xP4 = xP2
    xP1 = mRetrait + mRayon
    yP1 = -LgCarrefour / 2 + mDécalage
    yP2 = yP1
    yP3 = yP2 + mLargeur / 2
    yP4 = yP2 - mLargeur / 2
    ' P1 : Pointe de l'ilot
    Dim P1 As New PointF(xP1, yP1)
    ' P3 et P4 : Segment représentant la largeur de l'ilot
    Dim P3 As New PointF(xP3, yP3)
    Dim P4 As New PointF(xP4, yP4)
    'P2 : milieu de P3P4
    Dim P2 As New PointF(xP2, yP2)

    'Convertir les cordonnées de l'ilot, définies dans le repère de la branche, dans le repère général, puis en coordonnées dessin
    Dim PtDessin(3) As Point
    With mBranche
      PtDessin(0) = PointDessin(.PtRepèreGénéral(P1))
      PtDessin(1) = PointDessin(.PtRepèreGénéral(P3))
      PtDessin(2) = PointDessin(.PtRepèreGénéral(P2))
      PtDessin(3) = PointDessin(.PtRepèreGénéral(P4))
    End With

    Dim uneLigne As Ligne
    Dim unePlume, unePlumeInvisible As Pen
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Ilot).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.IlotImpression).Clone
      unePlumeInvisible = New Pen(CouleurInvisible)
    End If

    ' 1er coté de l'ilot
    uneLigne = New Ligne(PtDessin(0), PtDessin(1), unePlume)
    mGraphique.Add(uneLigne)
    ' 2ème coté de l'ilot (symétrique du premier par rapport à P1P2
    uneLigne = New Ligne(PtDessin(0), PtDessin(3), unePlume)
    mGraphique.Add(uneLigne)

    ' Arc sommet de l'ilot
    Dim V1, V2 As Vecteur
    Dim pCentre As Point = PtDessin(0)
    V1 = New Vecteur(pCentre, PtDessin(3))
    V2 = New Vecteur(pCentre, PtDessin(1))
    Dim AngleDépart As Single = CvAngleDegrés(AngleFormé(V1))
    Dim AngleBalayage As Single = CvAngleDegrés(AngleFormé(V1, V2))
    Dim Rayon As Single = Distance(pCentre, PtDessin(1))
    Dim unArc As New Arc(pCentre, Rayon, AngleDépart, AngleBalayage, unePlume)
    mGraphique.Add(unArc)

    'On ajoute le segment transversal en dernier pour que la poignée P2 n'apparaisse pas
    uneLigne = New Ligne(PtDessin(0), PtDessin(2), unePlumeInvisible)
    mGraphique.Add(uneLigne)

    ' Créer un contour convexe non visible pour utile  à la commande déplacerilot
    Dim unPolyarc As New PolyArc(PtDessin, Clore:=True)
    mGraphique.Add(unPolyarc)

    uneCollection.Insert(mGraphique, 0)
    Return mGraphique

  End Function

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    mGraphique.RendreSélectable(Not Verrouillage)
  End Sub

  '********************************************************************************************************************
  ' Enregistrer l'ilot dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim uneRowIlot As DataSetDiagfeux.IlotRow
    If uneRowBranche.GetIlotRows.Length = 0 Then
      uneRowIlot = ds.Ilot.AddIlotRow(mLargeur, mRayon, mDécalage, mRetrait, uneRowBranche)
    Else
      uneRowIlot = uneRowBranche.GetIlotRows(0)
      With uneRowIlot
        .Largeur = mLargeur
        .Rayon = mRayon
        .Décalage = mDécalage
        .Retrait = mRetrait
      End With
    End If
  End Sub

End Class