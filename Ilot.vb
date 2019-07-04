'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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
Public Class Ilot : Inherits M�tier

  Private Const d�fautLargeur As Single = 3.0
  Private Const d�fautRayon As Single = 3.0
  Private Const d�fautD�calage As Single = 2.0
  Private Const d�fautRetrait As Single = 4.0

  Public Const miniLargeur As Single = 1.0
  Public Const maxiLargeur As Single = 30.0
  Public Const miniRayon As Single = 1.0
  Public Const maxiRayon As Single = 99.0
  Public Const miniD�calAxe As Single = 0.0
  'Le maximum du d�calage est la largeur de la branche
  Public Const miniRetrait As Single = -10.0
  Public Const maxiRetrait As Single = 10.0

  'Largeur de l'�lot
  '##ModelId=4032308B0399
  Private mLargeur As Single

  'Rayon de l'�lot
  '##ModelId=403231050000
  Private mRayon As Single

  'D�calage de l'axe de sym�trie de l'�lot  avec le bord droit de la branche.
  'Toujours positif : l'origine de la branche est l'extr�mit� int�rieure de la branche(c�t� carrefour)
  'R�f : Cahier des charges du 16/04/03 �3
  '##ModelId=403231180109
  Private mD�calage As Single

  'Retrait de l'�lot par rapport � l'origine de la branche
  'R�f : Cahier des charges du 16/04/03 �3
  '##ModelId=403231E700BB
  Private mRetrait As Single

  '##ModelId=3C70D1A1004F
  Public mBranche As Branche

  Public Sub New(ByVal uneBranche As Branche)
    mBranche = uneBranche
    uneBranche.mIlot = Me

    mLargeur = d�fautLargeur
    mRayon = d�fautRayon
    mD�calage = d�fautD�calage
    mRetrait = d�fautRetrait

  End Sub

  Public Sub New(ByVal uneBranche As Branche, ByVal Largeur As Single, ByVal Rayon As Single, ByVal D�calage As Single, ByVal Retrait As Single)
    mBranche = uneBranche
    uneBranche.mIlot = Me

    mLargeur = Largeur
    mRayon = Rayon
    mD�calage = D�calage
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

  Public Property D�calage() As Single
    Get
      Return mD�calage
    End Get
    Set(ByVal Value As Single)
      mD�calage = Value
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

  Public Overridable Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal S�parateur As Char) As String
    Dim strLigne As String

    With Me
      Dim uneBranche As Branche = .mBranche

      strLigne = .Rayon & S�parateur
      strLigne &= .D�calage & S�parateur
      strLigne &= .Largeur & S�parateur
      strLigne &= .Retrait & S�parateur
    End With

    Return strLigne

  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc

    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetM�tier = Me

    Dim xP1, yP1, xP2, yP2, xP3, yP3, xP4, yP4 As Single
    Dim LgCarrefour As Single = mBranche.NbVoies * mBranche.LargeurVoies
    xP2 = mRetrait
    xP3 = xP2
    xP4 = xP2
    xP1 = mRetrait + mRayon
    yP1 = -LgCarrefour / 2 + mD�calage
    yP2 = yP1
    yP3 = yP2 + mLargeur / 2
    yP4 = yP2 - mLargeur / 2
    ' P1 : Pointe de l'ilot
    Dim P1 As New PointF(xP1, yP1)
    ' P3 et P4 : Segment repr�sentant la largeur de l'ilot
    Dim P3 As New PointF(xP3, yP3)
    Dim P4 As New PointF(xP4, yP4)
    'P2 : milieu de P3P4
    Dim P2 As New PointF(xP2, yP2)

    'Convertir les cordonn�es de l'ilot, d�finies dans le rep�re de la branche, dans le rep�re g�n�ral, puis en coordonn�es dessin
    Dim PtDessin(3) As Point
    With mBranche
      PtDessin(0) = PointDessin(.PtRep�reG�n�ral(P1))
      PtDessin(1) = PointDessin(.PtRep�reG�n�ral(P3))
      PtDessin(2) = PointDessin(.PtRep�reG�n�ral(P2))
      PtDessin(3) = PointDessin(.PtRep�reG�n�ral(P4))
    End With

    Dim uneLigne As Ligne
    Dim unePlume, unePlumeInvisible As Pen
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Ilot).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.IlotImpression).Clone
      unePlumeInvisible = New Pen(CouleurInvisible)
    End If

    ' 1er cot� de l'ilot
    uneLigne = New Ligne(PtDessin(0), PtDessin(1), unePlume)
    mGraphique.Add(uneLigne)
    ' 2�me cot� de l'ilot (sym�trique du premier par rapport � P1P2
    uneLigne = New Ligne(PtDessin(0), PtDessin(3), unePlume)
    mGraphique.Add(uneLigne)

    ' Arc sommet de l'ilot
    Dim V1, V2 As Vecteur
    Dim pCentre As Point = PtDessin(0)
    V1 = New Vecteur(pCentre, PtDessin(3))
    V2 = New Vecteur(pCentre, PtDessin(1))
    Dim AngleD�part As Single = CvAngleDegr�s(AngleForm�(V1))
    Dim AngleBalayage As Single = CvAngleDegr�s(AngleForm�(V1, V2))
    Dim Rayon As Single = Distance(pCentre, PtDessin(1))
    Dim unArc As New Arc(pCentre, Rayon, AngleD�part, AngleBalayage, unePlume)
    mGraphique.Add(unArc)

    'On ajoute le segment transversal en dernier pour que la poign�e P2 n'apparaisse pas
    uneLigne = New Ligne(PtDessin(0), PtDessin(2), unePlumeInvisible)
    mGraphique.Add(uneLigne)

    ' Cr�er un contour convexe non visible pour utile  � la commande d�placerilot
    Dim unPolyarc As New PolyArc(PtDessin, Clore:=True)
    mGraphique.Add(unPolyarc)

    uneCollection.Insert(mGraphique, 0)
    Return mGraphique

  End Function

  Public Sub Verrouiller(ByVal Verrouillage As Boolean)
    mGraphique.RendreS�lectable(Not Verrouillage)
  End Sub

  '********************************************************************************************************************
  ' Enregistrer l'ilot dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowBranche As DataSetDiagfeux.BrancheRow)
    Dim uneRowIlot As DataSetDiagfeux.IlotRow
    If uneRowBranche.GetIlotRows.Length = 0 Then
      uneRowIlot = ds.Ilot.AddIlotRow(mLargeur, mRayon, mD�calage, mRetrait, uneRowBranche)
    Else
      uneRowIlot = uneRowBranche.GetIlotRows(0)
      With uneRowIlot
        .Largeur = mLargeur
        .Rayon = mRayon
        .D�calage = mD�calage
        .Retrait = mRetrait
      End With
    End If
  End Sub

End Class