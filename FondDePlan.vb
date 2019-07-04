'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : FondDePlan.vb																						'
'						Classes																														'
'							FondDePlan : Abstraite
'             ImageRaster
'							DXF																											'
'             Calque : Calque d'un fichier DXF                                '
'							CalqueCollection : Collection de calques DXF                		'
'           Structures                                                        '
'             AttributGraphique                                               '
'******************************************************************************

Option Strict Off
Option Explicit On 
Imports System.Math

'--------------------------- Classe FondDePlan --------------------------
Public MustInherit Class FondDePlan
  'Fond de plan du carrefour au format DXF ou raster

  'Nom du fichier contenant le fond de plan
  '##ModelId=40321E64034B
  Protected mNomFichier As String
  Private mInfoFichier As IO.FileInfo
  Protected mEchelle As Single
  Private mEchelleCalculée As Boolean
  Private mADessiner As Boolean = True
  Private mVisible As Boolean = True
  Protected pMinFDP, pMaxFDP As PointF
  Protected mTailleF As SizeF

  Public Sub New()

  End Sub

  Public Sub New(ByVal Nom As String, ByVal Echelle As Short)
    NomFichier = Nom
    mEchelle = Echelle
  End Sub

  Protected Property NomFichier() As String
    Get
      Return mNomFichier
    End Get
    Set(ByVal Value As String)
      mNomFichier = Value
      mInfoFichier = New IO.FileInfo(Value)
    End Set
  End Property
  Public ReadOnly Property InfoFichier() As IO.FileInfo
    Get
      Return mInfoFichier
    End Get
  End Property

  Public Property Visible() As Boolean
    Get
      Return mVisible
    End Get
    Set(ByVal Value As Boolean)
      mVisible = Value
    End Set
  End Property

  Protected Property Echelle() As Single
    Get
      Return mEchelle
    End Get
    Set(ByVal Value As Single)
      mEchelle = Value
    End Set
  End Property

  Public Property EchelleCalculée() As Boolean
    Get
      Return mEchelleCalculée
    End Get
    Set(ByVal Value As Boolean)
      mEchelleCalculée = Value
    End Set
  End Property

  Public ReadOnly Property Centre() As PointF
    Get
      Return Milieu(pMinFDP, pMaxFDP)
    End Get
  End Property

  Public ReadOnly Property Largeur() As Single
    Get
      Return mTailleF.Width
    End Get
  End Property

  Public ReadOnly Property Hauteur() As Single
    Get
      Return mTailleF.Height
    End Get
  End Property

  Public Sub AffecterPminPmax()
    pMinFDP = Wpmin
    pMaxFDP = Wpmax
    mTailleF = New SizeF(pMaxFDP.X - pMinFDP.X, Abs(pMaxFDP.Y - pMinFDP.Y))
  End Sub

  Public ReadOnly Property pMin() As PointF
    Get
      Return pMinFDP
    End Get
  End Property

  Public ReadOnly Property pMax() As PointF
    Get
      Return pMaxFDP
    End Get
  End Property

  Public Property ADessiner() As Boolean
    Get
      Return mADessiner
    End Get
    Set(ByVal Value As Boolean)
      mADessiner = Value
    End Set
  End Property

  Public ReadOnly Property EstDXF() As Boolean
    Get
      Return TypeOf Me Is DXF
    End Get
  End Property

  Public Overridable Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.FondPlanRow

    Return ds.FondPlan.AddFondPlanRow(mNomFichier, Echelle:=mEchelle, EstDXF:=EstDXF, Rotation:=0, parentVarianteRowByVariante_FondPlan:=uneRowVariante)

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe ImageRaster --------------------------
'=====================================================================================================
Public Class ImageRaster : Inherits FondDePlan
  'Image raster associée au carrefour

  'Rotation en degrés de l'image dans le sens trigonométrique (direct)
  '##ModelId=403CAB87036E
  Private mRotation As Short

  '##ModelId=403CAB870360
  Private mTaille As Drawing.Size
  Private mRaster As Bitmap
  Public Shared LargeurImageBase As Short = 452  ' on essaie d'afficher au départ l'image dans une picturebox de largeur 452pixels
  Private mSansEchelle As Boolean
  Private mOrigineDessin As Point
  Public Const Filtre As String = "Fichiers image (*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG;*.WMF)|*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG;*.WMF"

  Public Sub New(ByVal NomFichier As String, ByRef uneTaille As Drawing.Size, ByVal pOrigine As Point)
    Me.NomFichier = NomFichier

    Instancier()

    mSansEchelle = True
    mOrigineDessin = pOrigine
    Echelle = Math.Max(mTaille.Width / uneTaille.Width, mTaille.Height / uneTaille.Height)

    'Corriger La taille d'origine du cadre du logo pour qu'il corresponde à la taille 'utile'
    uneTaille = New Size(LargeurAffichage, HauteurAffichage)

  End Sub

  Public Sub New(ByVal NomFichier As String)

    Me.NomFichier = NomFichier

    Instancier()

    'm Echelle : nb de pixels pour représenter 100 m
    Echelle = 100 * (mTaille.Width / (LargeurImageBase / cndParamDessin.Echelle))
    Rotation = 0

  End Sub

  Public Sub New(ByVal uneRowFondPlan As DataSetDiagfeux.FondPlanRow)
    MyBase.New(uneRowFondPlan.Nom, uneRowFondPlan.Echelle)
    Instancier()
    'Rajouter la rotation - Définit également l'encombrement réel à partir de la taille
    Rotation = uneRowFondPlan.Rotation

  End Sub

  Public Shared Function FichierExistant(ByVal NomFichier As String, Optional ByVal QuestionSiAbsent As Boolean = True) As String
    Dim DefaultExt As String = "jpg"

    If Not IsNothing(NomFichier) AndAlso Not IO.File.Exists(NomFichier) Then
      If QuestionSiAbsent Then
        AfficherMessageErreur(mdiApplication, "Le fichier image" & NomFichier & " est introuvable")
        NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=ImageRaster.Filtre, DefaultExt:=DefaultExt, InfoFichier:=New IO.FileInfo(NomFichier))
      Else
        NomFichier = Nothing
      End If
    End If

    Return NomFichier

  End Function

  Private Sub Instancier()
    Dim stm As New IO.FileStream(mNomFichier, IO.FileMode.Open)
    mRaster = New Bitmap(stm)
    stm.Close()
    Taille = CvTaillePlus(mRaster.PhysicalDimension)

  End Sub

  Public ReadOnly Property imgRaster() As Bitmap
    Get
      Return mRaster
    End Get
  End Property

  Public Property Taille() As Size
    Get
      Return mTaille

    End Get
    Set(ByVal Value As Size)
      mTaille = Value
    End Set
  End Property

  Private Function LargeurAffichage() As Short
    If mSansEchelle Then
      Return mTaille.Width / Echelle
    Else
      Return mTaille.Width * (MètresParPixel * Formules.Echelle)
    End If
  End Function

  Private Function HauteurAffichage() As Short
    If mSansEchelle Then
      Return mTaille.Height / Echelle
    Else
      Return mTaille.Height * (MètresParPixel * Formules.Echelle)
    End If
  End Function

  Private Function ToRéel(ByVal dimension As Single) As Single
    Return dimension * (100 / Echelle)
  End Function

  Public Property MètresParPixel() As Single
    Get
      'L'echelle représente le nb de pixels pour représenter 100m
      Return 100 / Echelle
    End Get
    Set(ByVal Value As Single)
      Echelle = CType(100 / Value, Short)
      DéfinirEncombrementRéel()
    End Set
  End Property

  Public Property Rotation() As Short
    Get
      Return mRotation
    End Get
    Set(ByVal Value As Short)
      mRotation = Value
      DéfinirEncombrementRéel()
    End Set
  End Property

  Private ReadOnly Property OrigineDessin() As Point
    Get
      If mSansEchelle Then
        Return mOrigineDessin
      Else
        Return PointDessin(DéfautOrigine)
      End If

    End Get
  End Property

  Public Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)
    Dim pOrigine As Point = OrigineDessin
    Dim p1, p2, p3, p4 As Point
    Dim unAngle As Double = CvAngleRadians(mRotation)
    Dim Sinus, Cosinus As Double
    Dim lg As Short = LargeurAffichage()
    Dim hg As Short = HauteurAffichage()

    Dim mCadre As PolyArc
    Dim Quadrant As Short = NumQuadrant(unAngle)
    Sinus = Sin(unAngle - Quadrant * PI / 2)
    Cosinus = Cos(unAngle - Quadrant * PI / 2)

    Select Case Quadrant
      Case 0
        p1 = Translation(New Point(0, lg * Sinus), pOrigine)
        p2 = Translation(New Point(lg * Cosinus, 0), pOrigine)
        p3 = Translation(New Point(lg * Cosinus + hg * Sinus, hg * Cosinus), pOrigine)
        p4 = Translation(New Point(hg * Sinus, lg * Sinus + hg * Cosinus), pOrigine)
      Case 1
        Sinus = Sin(unAngle - PI / 2)
        Cosinus = Cos(unAngle - PI / 2)
        p1 = Translation(New Point(lg * Sinus, hg * Sinus + lg * Cosinus), pOrigine)
        p2 = Translation(New Point(0, hg * Sinus), pOrigine)
        p3 = Translation(New Point(hg * Cosinus, 0), pOrigine)
        p4 = Translation(New Point(lg * Sinus + hg * Cosinus, lg * Cosinus), pOrigine)
      Case 2
        Sinus = Sin(unAngle - PI)
        Cosinus = Cos(unAngle - PI)
        p1 = Translation(New Point(lg * Cosinus + hg * Sinus, hg * Cosinus), pOrigine)
        p2 = Translation(New Point(hg * Sinus, lg * Sinus + hg * Cosinus), pOrigine)
        p3 = Translation(New Point(0, lg * Sinus), pOrigine)
        p4 = Translation(New Point(lg * Cosinus, 0), pOrigine)
      Case 3
        p1 = Translation(New Point(hg * Cosinus, 0), pOrigine)
        p2 = Translation(New Point(hg * Cosinus + lg * Sinus, lg * Cosinus), pOrigine)
        p3 = Translation(New Point(lg * Sinus, hg * Sinus + lg * Cosinus), pOrigine)
        p4 = Translation(New Point(0, hg * Sinus), pOrigine)
    End Select

    Dim pt As Point() = {p1, p2, p4}

    ' Dessiner l'image 
    If Not IsNothing(g2) Then
      'g2.DrawImage(mRaster, pt)
    End If

    ' Dessiner l'image dans le tampon permanent ou sur l'imprimante
    g1.DrawImage(mRaster, pt)

    ReDim Preserve pt(3)
    pt(3) = p4
    pt(2) = p3

    mCadre = New PolyArc(pt, clore:=True)
    mCadre.Plume = New Pen(Color.Red)
    '  mCadre.Dessiner(g1, g2)

    Dim p(3) As Point
    p(0) = PointDessin(pMin)
    p(1) = PointDessin(New PointF(pMax.X, pMin.Y))
    p(2) = PointDessin(New PointF(pMax.X, pMax.Y))
    p(3) = PointDessin(New PointF(pMin.X, pMax.Y))
    mCadre = New PolyArc(p, clore:=True)
    mCadre.Plume = New Pen(Color.Red)
    ' mCadre.Dessiner(g1, g2)

  End Sub

  Private Sub DéfinirEncombrementRéel()
    Dim unAngle As Double = CvAngleRadians(mRotation)
    Dim Sinus, Cosinus As Double
    Dim Quadrant As Short = NumQuadrant(unAngle)
    Sinus = Sin(unAngle - Quadrant * PI / 2)
    Cosinus = Cos(unAngle - Quadrant * PI / 2)
    Dim lg, hg As Single
    Dim pBasGauche As PointF

    With mTaille
      lg = (.Width * Cosinus + .Height * Sinus) * MètresParPixel
      hg = (.Width * Sinus + .Height * Cosinus) * MètresParPixel
    End With

    Wpmin = Translation(New PointF(0, 0), DéfautOrigine)

    Select Case Quadrant
      Case 0, 2
        pBasGauche = New PointF(lg, -hg)
      Case 1, 3
        pBasGauche = New PointF(hg, -lg)
    End Select

    Wpmax = Translation(pBasGauche, DéfautOrigine)

    AffecterPminPmax()

  End Sub

  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.FondPlanRow
    Dim uneRowFondPlan As DataSetDiagfeux.FondPlanRow = MyBase.Enregistrer(uneRowVariante)

    uneRowFondPlan.Rotation = mRotation

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe DXF --------------------------
'=====================================================================================================
Public Class DXF : Inherits FondDePlan
  'Objet correspondant à un fichier DXF

  '##ModelId=403CAB87037B
  Private mCalques As New CalqueCollection
  Private mGraphFDP As New SuperBloc
  Private mInsert As Insert

  Public Sub New(ByVal Nom As String, ByVal Echelle As Short)
    MyBase.New(Nom, Echelle)
    mInsert = New Insert(mGraphFDP)
    mInsert.Echx = 1
  End Sub

  Public Sub New(ByVal uneRowFondPlan As DataSetDiagfeux.FondPlanRow)
    MyBase.New(uneRowFondPlan.Nom, uneRowFondPlan.Echelle)
    mInsert = New Insert(mGraphFDP)
    mInsert.Echx = 1
  End Sub

  Public Sub Construire(ByVal uneRowDXF As DataSetDiagfeux.DXFRow)
    Dim i As Short

    ' Lire les calques
    Dim unCalque As Calque
    For i = 0 To uneRowDXF.GetCalqueRows.Length - 1
      unCalque = New Calque(uneRowDXF.GetCalqueRows(i))
      mCalques.Add(unCalque)
    Next

    'Définir les attributs du calque "0" : id AutoCAD 
    mInsert.DéfinirAttributs(mCalques("0"), 256, "BYBLOCK")

    Dim unBloc As Bloc
    With uneRowDXF
      'Lire les blocs
      For i = 1 To .GetBlocRows.Length - 1
        unBloc = New Bloc(.GetBlocRows(i).Nom)
        Blocs.Add(unBloc)
      Next

      'Lire les entités
      mGraphFDP.Construire(.GetBlocRows(0), mCalques, Blocs)

      'Lire 
      For i = 1 To .GetBlocRows.Length - 1
        unBloc = Blocs(.GetBlocRows(i).Nom)
        unBloc.Construire(uneRowDXF.GetBlocRows(i), mCalques, Blocs)
      Next
    End With

    'Récupérer les limites du dessin
    With uneRowDXF
      If .GetLimitesRows.Length > 0 Then
        With .GetLimitesRows(0)
          Wpmin.X = uneRowDXF.GetLimitesRows(0).GetpMinRows(0).X
          Wpmin.Y = uneRowDXF.GetLimitesRows(0).GetpMinRows(0).Y
          Wpmax.X = uneRowDXF.GetLimitesRows(0).GetpMaxRows(0).X
          Wpmax.Y = uneRowDXF.GetLimitesRows(0).GetpMaxRows(0).Y
        End With

      Else
        calminmax(pInsert:=New PointF(0, 0), objBloc:=mGraphFDP)
      End If
    End With

    AffecterPminPmax()

  End Sub

  Public ReadOnly Property Calques() As CalqueCollection
    Get
      Return mCalques
    End Get
  End Property

  Public ReadOnly Property Blocs() As BlocCollection
    Get
      Return mGraphFDP.Blocs
    End Get
  End Property

  Public ReadOnly Property GraphFDP() As SuperBloc
    Get
      Return mGraphFDP
    End Get
  End Property

  Public ReadOnly Property Insert() As Insert
    Get
      Return mInsert
    End Get
  End Property

  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.FondPlanRow
    Dim uneRowFondPlan As DataSetDiagfeux.FondPlanRow = MyBase.Enregistrer(uneRowVariante)

    Dim uneRowDXF As DataSetDiagfeux.DXFRow = ds.DXF.NewDXFRow
    uneRowDXF.Nom = IO.Path.GetFileNameWithoutExtension(MyClass.mNomFichier)

    ds.DXF.AddDXFRow(uneRowDXF)

    Dim uneRowLimites As DataSetDiagfeux.LimitesRow
    uneRowLimites = ds.Limites.AddLimitesRow(uneRowDXF)
    ds.pMin.AddpMinRow(pMinFDP.X, pMinFDP.Y, uneRowLimites)
    ds.pMax.AddpMaxRow(pMaxFDP.X, pMaxFDP.Y, uneRowLimites)

    'Enregistrer les Calques
    Dim unCalque As Calque
    For Each unCalque In mCalques
      unCalque.Enregistrer(uneRowDXF)
    Next

    'Enregistrer le SuperBloc contenant tous les blocs et entités du DXF
    mGraphFDP.Enregistrer(uneRowDXF)

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe Calque--------------------------
'=====================================================================================================
Public Class Calque
  'Calque de dessin

  'Indique si les éléments graphiques de la couche sont dessinés
  '##ModelId=403CAB870380
  Private mVisible As Boolean

  Private mNom As String
  '##ModelId=403CAB870383
  Private mAttributs As AttributGraphique
  Private mPlume As Pen

  Public Sub New(ByVal Nom As String, Optional ByVal Couleur As Integer = 7, Optional ByVal typelign As String = "CONTINUOUS", Optional ByVal Alpha As Integer = 92)
    mNom = Nom
    Attributs = New AttributGraphique(Couleur, typelign, Alpha:=Alpha)
  End Sub

  Public Sub New(ByVal uneRowCalque As DataSetDiagfeux.CalqueRow)
    With uneRowCalque
      mNom = .Nom
      Attributs = New AttributGraphique(.Couleur, .TypeLign)
    End With
  End Sub
  Public Property Nom() As String
    Get
      Return mNom
    End Get
    Set(ByVal Value As String)
      mNom = Value
    End Set
  End Property

  Public Property Plume() As Pen
    Get
      Return mPlume
    End Get
    Set(ByVal Value As Pen)
      mPlume = Value
    End Set
  End Property

  Public Property Attributs() As AttributGraphique
    Get
      Return mAttributs
    End Get
    Set(ByVal Value As AttributGraphique)
      mAttributs = Value
      mPlume = mAttributs.Plume
    End Set
  End Property

  Public ReadOnly Property Couleur() As Integer
    Get
      Return mAttributs.Couleur
    End Get
  End Property

  Public ReadOnly Property Typelign() As String
    Get
      Return mAttributs.TypeLign
    End Get
  End Property

  Public Property Visible() As Boolean
    Get
      Return mVisible
    End Get
    Set(ByVal Value As Boolean)
      If Value Then
        mAttributs.Couleur = Math.Abs(Couleur)
      Else
        mAttributs.Couleur = -Math.Abs(Couleur)
      End If

      mVisible = Value
    End Set
  End Property

  Public ReadOnly Property Gele() As Boolean
    Get
      Return Not mVisible
    End Get
    'Set(ByVal Value As Boolean)
    '  mVisible = Not Value
    'End Set
  End Property

  Public Sub Enregistrer(ByVal uneRowDXF As DataSetDiagfeux.DXFRow)
    ds.Calque.AddCalqueRow(Nom, Couleur, Typelign, uneRowDXF)
  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe CalqueCollection--------------------------
'=====================================================================================================
Public Class CalqueCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal valeur As Calque) As Short
    Return Me.List.Add(valeur)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Calque)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal valeur As Calque)
    If Me.List.Contains(valeur) Then
      Me.List.Remove(valeur)
    End If

  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal index As Short) As Calque
    Get
      Return CType(Me.List.Item(index), Calque)
    End Get
  End Property

  ' Creer une autre propriété par défaut Item pour cette collection.
  ' Permet la  recherche par nom.
  Default Public ReadOnly Property Item(ByVal Nom As String) As Calque
    Get
      Dim unCalque As Calque
      For Each unCalque In Me.List
        If unCalque.Nom = Nom Then
          Return unCalque
        End If
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unCalque As Calque) As Short
    Return Me.List.IndexOf(unCalque)
  End Function

  ' Method to check if a person object already exists in the collection.
  Public Function Contains(ByVal valeur As Calque) As Boolean
    Return Me.List.Contains(valeur)
  End Function

  Public Function Contains(ByVal nomCalque As String) As Boolean
    Dim unCalque As Calque = Me(nomCalque)
    Return Not (IsNothing(unCalque))
  End Function

End Class

'=====================================================================================================
'--------------------------- Structure AttributGraphique --------------------------
'=====================================================================================================
Public Structure AttributGraphique
  Private mCouleur As Integer
  Private mTypeLign As String
  Private mPlume As Pen
  Private mIsRempli As Boolean
  Private mAlpha As Integer

  Public Sub New(ByVal uneCouleur As Integer, ByVal Typelign As String, Optional ByVal Alpha As Integer = 92)
    'La valeur de couleur peut être négative s'il s'agit d'un calque autocad inactif
    mCouleur = uneCouleur
    mTypeLign = Typelign
    mAlpha = Alpha

    If mCouleur < 0 Then
      Plume = Nothing
    Else
      Dim c As Color = System.Drawing.ColorTranslator.FromOle(QBColor((tCouleur(mCouleur))))
      c = Color.FromArgb(mAlpha, c)
      Plume = New Pen(c)
    End If
  End Sub

  Public Property Plume() As Pen
    Get
      Return mPlume
    End Get
    Set(ByVal Value As Pen)
      mPlume = Value
      IsEmpty = IsNothing(Value)
    End Set
  End Property

  Public Property Couleur() As Integer
    Get
      Return mCouleur
    End Get
    Set(ByVal Value As Integer)
      mCouleur = Value
    End Set
  End Property

  Public ReadOnly Property TypeLign() As String
    Get
      Return mTypeLign
    End Get
  End Property

  Public Property IsEmpty() As Boolean
    Get
      Return Not mIsRempli
    End Get
    Set(ByVal Value As Boolean)
      mIsRempli = Not Value
    End Set
  End Property
End Structure
