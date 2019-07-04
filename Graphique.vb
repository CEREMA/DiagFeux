'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'		
'						Module Graphique : Classes gérant les objets graphiques           '
'																																							'
'******************************************************************************
Imports System.Math

'=====================================================================================================
'--------------------------- Graphique : Classe Générique pour tous les objets graphiques
'=====================================================================================================
Public MustInherit Class Graphique
  Public ObjetMétier As Métier
  Public mPointillable As Boolean
  Protected mPoignée(-1) As Point

  Public MustOverride Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)
  Public MustOverride Function ProcheDuPoint(ByVal pC As Point, ByRef pIntéressant As Point, Optional ByVal R As Single = Nothing) As Boolean
  Public MustOverride ReadOnly Property NbPoignées() As Short
  Public MustOverride Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
  Public MustOverride Function TrInsertion(ByVal ObjInsert As Insert) As Graphique
  Public Overridable ReadOnly Property Longueur() As Single
    Get

    End Get
  End Property
  Public Overridable Function Clone() As Graphique

  End Function
  Public Overridable Function CvRéel() As Graphique

  End Function

  Public Overridable Function CvDessin() As Graphique

  End Function

  Public Overridable ReadOnly Property Poignée(ByVal Index As Short) As Point
    Get
      Return mPoignée(Index)
    End Get
  End Property

  Public Overridable Sub Effacer(ByVal g1 As Graphics, ByVal g2 As Graphics)

    Try
      If IsNothing(mPlume) Then
        If Not IsNothing(mBrosse) Then
          'mBrosse peut aussi être Nothing si ce n'est qu'un objet Graphique de construction
          Dim uneCouleur As Drawing.Color = mBrosse.Color
          mBrosse.Color = CouleurInvisible
          Dessiner(g1, g2)
          mBrosse.Color = uneCouleur
        End If

      Else
        Dim uneCouleur As Drawing.Color = mPlume.Color
        mPlume.Color = CouleurInvisible
        Dessiner(g1, g2)
        mPlume.Color = uneCouleur
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Graphique.Effacer")

    End Try
  End Sub

  Public Overridable Property Pointillable() As Boolean
    Get
      Return mPointillable
    End Get
    Set(ByVal Value As Boolean)
      mPointillable = Value
    End Set
  End Property

  Public Overridable Function Intersection(ByVal uneFigure As Graphique) As PointF

  End Function
  Protected mPlume As Pen
  Protected mBrosse As SolidBrush
  Protected mAttributs As AttributGraphique
  Protected mCalque As Calque
  Protected mCouleur As Integer
  Protected mTypelign As String
  Protected mAlpha As Integer

  Public Invisible As Boolean

  Public Overridable Sub DéfinirAttributs(ByVal unCalque As Calque, ByVal uneCouleur As Integer, ByVal typelign As String, Optional ByVal Alpha As Integer = 92)
    mCalque = unCalque
    mCouleur = uneCouleur
    mAlpha = Alpha
    If IsNothing(typelign) Then
      mTypelign = "BYLAYER"
    Else
      mTypelign = typelign
    End If
  End Sub

  Protected Function RechCouleur(ByVal uneCouleur As Integer, ByVal unCalque As Calque, Optional ByVal EntitéInsert As Insert = Nothing) As Long

    If IsNothing(EntitéInsert) Then
      If uneCouleur = 256 Then      'couleur DUPLAN (ou DUCALQUE)
        Return unCalque.Couleur
      Else
        Return uneCouleur
      End If

      ' Couleur des entités insérées dans un bloc:  cf Manuel utilisateur AutoCAD 12 ch.10 p 394 (Blocs et plans)

    Else  ' Insert
      If uneCouleur = 0 Then    'couleur DUBLOC
        Return RechCouleur(EntitéInsert.mCouleur, EntitéInsert.mCalque)
      Else
        If unCalque.Nom = "0" Then    ' plan caméléon: couleur du plan du bloc
          Return RechCouleur(uneCouleur, EntitéInsert.mCalque)
        Else
          Return RechCouleur(uneCouleur, unCalque)
        End If
      End If
    End If

  End Function

  Protected Function RechTypeLign(ByVal unTypeLign As String, ByVal unCalque As Calque, Optional ByVal EntitéInsert As Insert = Nothing) As String

    If IsNothing(EntitéInsert) Then
      If unTypeLign = "BYLAYER" Then    'typelign DUPLAN (ou DUCALQUE)
        Return unCalque.Typelign
      Else
        Return unTypeLign
      End If

      ' Type de ligne des entités insérées dans un bloc:  cf Manuel utilisateur AutoCAD 12 ch.10 p 394 (Blocs et plans)
    Else  ' Insert
      If unTypeLign = "BYBLOCK" Then  'typelign DUBLOC
        Return RechTypeLign(EntitéInsert.mTypelign, EntitéInsert.mCalque)
      Else
        If unCalque.Nom = "0" Then    ' plan caméléon: typeligne du plan du bloc
          Return RechTypeLign(unTypeLign, EntitéInsert.mCalque)
        Else
          Return RechTypeLign(unTypeLign, unCalque)
        End If
      End If
    End If

  End Function

  Public ReadOnly Property Sélectable() As Boolean
    Get
      Return NbPoignées > 0
    End Get
  End Property

  Public Overridable Property Plume() As Pen

    Get
      If Attributs.IsEmpty Then
        Return mPlume
      Else
        Return Attributs.Plume
      End If
    End Get
    Set(ByVal Value As Pen)
      If Attributs.IsEmpty Then
        mPlume = Value
      Else
        mAttributs.Plume = Value
      End If
    End Set

  End Property

  Public Overridable Property Brosse() As SolidBrush
    Get
      If Attributs.IsEmpty Then
        Return mBrosse
      Else
        Return New SolidBrush(Attributs.Plume.Color)
      End If
    End Get
    Set(ByVal Value As SolidBrush)
      If Attributs.IsEmpty Then
        mBrosse = Value
      Else
        mAttributs.Plume = New Pen(Value.Color)
      End If
    End Set
  End Property

  Public Overridable Property Attributs() As AttributGraphique
    Get
      If IsNothing(mCalque) Then
        Return mAttributs
      Else
        Return mCalque.Attributs
      End If

    End Get
    Set(ByVal Value As AttributGraphique)
      mAttributs = Value
      Plume = mAttributs.Plume
    End Set
  End Property

  Public ReadOnly Property Calque() As Calque
    Get
      Return mCalque
    End Get
  End Property
  Public ReadOnly Property Couleur() As Integer
    Get
      Return mCouleur
    End Get

  End Property
  Public ReadOnly Property TypeLign() As String
    Get
      Return mTypelign
    End Get
  End Property

  '********************************************************************************************************************
  ' Retourne le transformé de l'objet Graphique dans la rotation de centre (0,0) et d'angle Alpha
  '********************************************************************************************************************
  Public MustOverride Function Rotation(ByVal Alpha As Single) As Graphique

  '********************************************************************************************************************
  ' Retourne le point transformé de l'objet Graphique  dans la translation de vecteur V(pTrans.X,pTrans.Y)
  '********************************************************************************************************************
  Public MustOverride Function TranslationBase(ByVal pTrans As SizeF) As Graphique

  '********************************************************************************************************************
  ' Retourne le point transformé de l'objet Graphique  dans la translation de vecteur V(pTrans.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal pTrans As Point) As Graphique
    Return TranslationBase(New SizeF(pTrans.X, pTrans.Y))

  End Function

  '********************************************************************************************************************
  ' Retourne le point transformé de l'objet Graphique  dans la translation de vecteur unVecteur
  '********************************************************************************************************************
  Public Function Translation(ByVal unVecteur As Vecteur) As Graphique
    Return TranslationBase(New SizeF(unVecteur.X, unVecteur.Y))
  End Function

  Public Function Translation(ByVal uneFigure As Graphique, ByVal pTrans As Point) As Graphique
    Return uneFigure.Translation(pTrans)
  End Function
  Public Function Translation(ByVal uneFigure As Graphique, ByVal pTrans As PointF) As Graphique
    Return uneFigure.Translation(pTrans)
  End Function

  '********************************************************************************************************************
  ' Retourne le point transformé de l'objet Graphique  dans la translation de vecteur V(pTrans.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal pTrans As PointF) As Graphique
    Return TranslationBase(New SizeF(pTrans.X, pTrans.Y))
  End Function

  '********************************************************************************************************************
  ' Transformé de l'objet Graphique dans la rotation de centre (0,0) et d'angle Alpha
  ' puis retourne la translation de ce dernier
  '********************************************************************************************************************
  Public Function RotTrans(ByVal pTrans As Point, ByVal Alpha As Single) As Graphique
    Return Translation(Rotation(Alpha), pTrans)
  End Function

  Public Function RotTrans(ByVal pTrans As PointF, ByVal Alpha As Single) As Graphique
    Return Translation(Rotation(Alpha), pTrans)
  End Function

  'Mémoriser l'image à un instant 't'
  Public Shared Function AssocierBitmapGraphics(ByVal uneTaille As Size, ByVal gr As Graphics, ByRef unTampon As Graphics) As Bitmap

    ' Make a new bitmap that fits the control.
    With uneTaille
      AssocierBitmapGraphics = New Bitmap(.Width, .Height, gr)
    End With
    'FromImage établit un lien solide entre l'objet Bitmap et l'objet Graphics
    unTampon = Graphics.FromImage(AssocierBitmapGraphics)
    unTampon.InterpolationMode = Drawing2D.InterpolationMode.High
    'gr.InterpolationMode = Drawing2D.InterpolationMode.High
  End Function

End Class

'=====================================================================================================
'--------------------------- Collection d'objets graphiques
'=====================================================================================================
Public Class Graphiques : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal uneFigure As Graphique) As Graphique
    Me.List.Add(uneFigure)
    Return uneFigure
  End Function
  Public Function Insert(ByVal uneFigure As Graphique, ByVal Index As Integer) As Graphique
    Me.List.Insert(Index, uneFigure)
    Return uneFigure
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal Figures() As Graphique)
    Me.InnerList.AddRange(Figures)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal uneFigure As Graphique)
    If Me.List.Contains(uneFigure) Then
      Me.List.Remove(uneFigure)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneFigure As Graphique)
    Me.List.Insert(Index, uneFigure)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Graphique
    Get
      Return CType(Me.List.Item(Index), Graphique)
    End Get
  End Property

  Public Function IndexOf(ByVal uneFigure As Graphique) As Short
    Return Me.List.IndexOf(uneFigure)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal uneFigure As Graphique) As Boolean
    Return Me.List.Contains(uneFigure)
  End Function

  Public Function RechercherObject(ByVal p As Point, ByRef PointCliqué As Point) As Graphique
    Dim distMin, distMinPréc As Single
    Dim unObjet As Graphique
    Dim uneSélection As Object
    Dim pProjeté, pProche As Point
    Dim MétierSélection, MétierCourant As Métier

    distMinPréc = 500
    distMin = 500

    For Each unObjet In Me
      If unObjet.Sélectable Then
        If unObjet.ProcheDuPoint(p, pProjeté) Then
          MétierCourant = unObjet.ObjetMétier
          If Not IsNothing(uneSélection) Then
            If TypeOf MétierSélection Is Variante Then
              'Le déplacement de carrefour doit être proposé en dernier (si on ne trouve vraiment rien d'autre)
              distMinPréc = 500
              'ElseIf TypeOf MétierCourant Is Antagonisme AndAlso Not TypeOf MétierSélection Is Antagonisme Then
              '  'Favoriser la sélection d'antagonisme par rapport à un autre type d'objet métier
              '  distMinPréc = 500
            ElseIf TypeOf MétierCourant Is PassagePiéton AndAlso TypeOf MétierSélection Is Ilot Then
              'Favoriser la sélection de passage piéton par rapport à un ilot
              distMinPréc = 500
            ElseIf TypeOf MétierCourant Is Branche AndAlso (TypeOf MétierSélection Is PassagePiéton Or TypeOf MétierSélection Is Ilot) Then
              'Favoriser la sélection de la branche par rapport au passage piéton ou à un ilot
              distMinPréc = 500
            End If
          End If
          distMin = Min(Distance(p, pProjeté), distMinPréc)
        End If

        If distMin < distMinPréc Then
          distMinPréc = distMin
          PointCliqué = pProjeté
          uneSélection = unObjet
          MétierSélection = MétierCourant

          If TypeOf MétierSélection Is Branche Or TypeOf MétierSélection Is Antagonisme Then
            Exit For
          End If
        End If
      End If
    Next

    Return uneSélection

  End Function

  Public Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)
    Dim uneFigure As Graphique

    For Each uneFigure In Me
      uneFigure.Dessiner(g1, g2)
    Next
  End Sub

  Public Sub Effacer(ByVal g1 As Graphics, ByVal g2 As Graphics)
    Dim uneFigure As Graphique

    For Each uneFigure In Me
      uneFigure.Effacer(g1, g2)
    Next

  End Sub

  Public Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique()
    Dim uneFigure As Graphique
    Dim mFigures(Me.Count - 1) As Graphique

    For Each uneFigure In Me
      mFigures(IndexOf(uneFigure)) = uneFigure.PréparerDessin(ObjInsert)
    Next

    Return mFigures

  End Function

  Public Function TrInsertion(ByVal ObjInsert As Insert) As Graphique()
    Dim mFigures(Me.Count - 1) As Graphique
    Dim uneFigure As Graphique

    For Each uneFigure In Me
      mFigures(IndexOf(uneFigure)) = uneFigure.TrInsertion(ObjInsert)
    Next

    Return mFigures

  End Function

  Public Sub AttribuerPlume(ByVal unePlume As Pen)
    Dim uneFigure As Graphique

    For Each uneFigure In Me
      uneFigure.Plume = unePlume
    Next

  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe Bloc
'=====================================================================================================
Public Class Bloc
  Public mNom As String
  Private mPoints As New ACADPointCollection
  Private mLignes As New LigneCollection
  Private mArcs As New ArcCollection
  Private mCercles As New CercleCollection
  Private mPolyArcs As New PolyArcCollection
  Private mSplines As New SplineCollection
  Private mTextes As New TexteCollection
  Private mInserts As New InsertCollection


  Public Sub New()

  End Sub

  Public Sub Construire(ByVal uneRowBloc As DataSetDiagfeux.BlocRow, ByVal Calques As CalqueCollection, ByVal Blocs As BlocCollection)
    Dim i As Short
    Dim uneLigne As Ligne
    Dim unArc As Arc
    Dim unCercle As Cercle
    Dim unPolyArc As PolyArc
    Dim unInsert As Insert
    Dim uneSpline As Spline

    With uneRowBloc

      For i = 0 To .GetLIGNERows.Length - 1
        With .GetLIGNERows(i)
          uneLigne = New Ligne(New PointF(.GetPARows(0).X, .GetPARows(0).Y), New PointF(.GetPARows(1).X, .GetPARows(1).Y))
          uneLigne.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
          mLignes.Add(uneLigne)
        End With
      Next

      For i = 0 To .GetARCRows.Length - 1
        With .GetARCRows(i)
          unArc = New Arc(New PointF(.GetCentreArcRows(0).X, .GetCentreArcRows(0).Y), .Rayon, .AngleDépart, .AngleBalayage)
          unArc.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
          mArcs.Add(unArc)
        End With
      Next

      For i = 0 To .GetCERCLERows.Length - 1
        With .GetCERCLERows(i)
          unCercle = New Cercle(New PointF(.GetCentreCercleRows(0).X, .GetCentreCercleRows(0).Y), .Rayon)
          unCercle.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
        End With
        mCercles.Add(unCercle)
      Next

      Dim j As Short
      Dim unPt As Pt
      For i = 0 To .GetPOLYARCRows.Length - 1
        With .GetPOLYARCRows(i)
          unPolyArc = New PolyArc(Autocadien:=True)
          unPolyArc.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
          unPolyArc.Fermé = .Clore
          For j = 0 To .GetPIRows.Length - 1
            With .GetPIRows(j)
              unPt = New Pt(New PointF(.GetPRows(0).X, .GetPRows(0).Y))
              unPt.Arrondi = .Arrondi
              With unPolyArc
                If j > 0 AndAlso .PtsPoly(j - 1).Arrondi <> 0 Then
                  CalArc(unPolyArc, unPt.p, .Calque, .Couleur, .TypeLign)
                End If
                .PtsPoly.Add(unPt)
              End With
            End With
          Next
          With unPolyArc
            If .Fermé And .PtsPoly(.PtsPoly.Count - 1).Arrondi <> 0 Then
              CalArc(unPolyArc, .PtsPoly(0).p, .Calque, .Couleur, .TypeLign)
            End If
          End With
        End With
        mPolyArcs.Add(unPolyArc)
      Next

      For i = 0 To .GetINSERTRows.Length - 1
        With .GetINSERTRows(i)
          unInsert = New Insert(Blocs(.NomBloc))
          unInsert.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
          unInsert.Echx = .Echelle
          unInsert.rot = .Rotation
          unInsert.pInsertion = New PointF(.GetpInsertionRows(0).X, .GetpInsertionRows(0).Y)
        End With
        mInserts.Add(unInsert)
      Next

      For i = 0 To .GetSPLINERows.Length - 1
        With .GetSPLINERows(i)
          uneSpline = New Spline
          uneSpline.DéfinirAttributs(Calques(.NomCalque), .Couleur, .TypeLign)
          For j = 0 To .GetPtDessinéRows.Length - 1
            uneSpline.PtsDessinés.Add(New Pt(New PointF(.GetPtDessinéRows(j).X, .GetPtDessinéRows(j).Y)))
          Next
          mSplines.Add(uneSpline)
        End With
      Next
    End With
  End Sub

  Public Sub New(ByVal Nom As String)
    mNom = Nom
  End Sub

  Public ReadOnly Property Nom() As String
    Get
      Return mNom
    End Get
  End Property

  Public ReadOnly Property Points() As ACADPointCollection
    Get
      Return mPoints
    End Get
  End Property
  Public ReadOnly Property Lignes() As LigneCollection
    Get
      Return mLignes
    End Get
  End Property

  Public ReadOnly Property PolyArcs() As PolyArcCollection
    Get
      Return mPolyArcs
    End Get
  End Property

  Public ReadOnly Property Splines() As SplineCollection
    Get
      Return mSplines
    End Get
  End Property
  Public ReadOnly Property Cercles() As CercleCollection
    Get
      Return mCercles
    End Get
  End Property

  Public ReadOnly Property Arcs() As ArcCollection
    Get
      Return mArcs
    End Get
  End Property

  Public ReadOnly Property Textes() As TexteCollection
    Get
      Return mTextes
    End Get
  End Property

  Public ReadOnly Property Inserts() As InsertCollection
    Get
      Return mInserts
    End Get
  End Property
  Public Sub Clear()
    mPoints.Clear()
    mLignes.Clear()
    mArcs.Clear()
    mCercles.Clear()
    mPolyArcs.Clear()
    mSplines.Clear()
    mInserts.Clear()
  End Sub


  '*************************************************************
  ' Retourne le nombre d'éléments composants le bloc
  '*************************************************************
  Public Function Count() As Short

    Count = mPoints.Count + mLignes.Count + mPolyArcs.Count + mSplines.Count
    Count += mCercles.Count + mArcs.Count + mInserts.Count

  End Function

  Public Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Bloc
    Dim ImageDessin As New Bloc

    With ImageDessin
      .Lignes.AddRange(Lignes.PréparerDessin(ObjInsert))
      .Arcs.AddRange(Arcs.PréparerDessin(ObjInsert))
      .Cercles.AddRange(Cercles.PréparerDessin(ObjInsert))
      .PolyArcs.AddRange(PolyArcs.PréparerDessin(ObjInsert))
      .Splines.AddRange(Splines.PréparerDessin(ObjInsert))
      .Textes.AddRange(Textes.PréparerDessin(ObjInsert))
      .Inserts.AddRange(Inserts.PréparerDessin(ObjInsert))
    End With

    Return ImageDessin

  End Function

  Public Function TrInsertion(ByVal ObjInsert As Insert) As Bloc
    Dim ImageDessin As New Bloc

    With ImageDessin
      .Lignes.AddRange(Lignes.TrInsertion(ObjInsert))
      .Arcs.AddRange(Arcs.TrInsertion(ObjInsert))
      .Cercles.AddRange(Cercles.TrInsertion(ObjInsert))
      .PolyArcs.AddRange(PolyArcs.TrInsertion(ObjInsert))
      .Splines.AddRange(Splines.TrInsertion(ObjInsert))
      .Textes.AddRange(Textes.TrInsertion(ObjInsert))
      .Inserts.AddRange(Inserts.TrInsertion(ObjInsert))
    End With

    Return ImageDessin

  End Function

  Public Sub AttribuerPlume(ByVal unePlume As Pen)

    Lignes.AttribuerPlume(unePlume)
    Arcs.AttribuerPlume(unePlume)
    Cercles.AttribuerPlume(unePlume)
    PolyArcs.AttribuerPlume(unePlume)
    Splines.AttribuerPlume(unePlume)
    Inserts.AttribuerPlume(unePlume)

  End Sub

  Public Overridable Sub Enregistrer(ByVal uneRowDXF As DataSetDiagfeux.DXFRow)
    Dim uneLigne As Ligne
    Dim unArc As Arc
    Dim unCercle As Cercle
    Dim unPolyArc As PolyArc
    Dim unInsert As Insert
    Dim uneSpline As Spline

    Dim uneRowBloc As DataSetDiagfeux.BlocRow = ds.Bloc.AddBlocRow(Nom, uneRowDXF)
    Dim uneRowLigne As DataSetDiagfeux.LIGNERow
    For Each uneLigne In mLignes
      With uneLigne
        uneRowLigne = ds.LIGNE.AddLIGNERow(.Couleur, .TypeLign, .Calque.Nom, uneRowBloc)
        ds.PA.AddPARow(.pAF.X, .pAF.Y, uneRowLigne)
        ds.PA.AddPARow(.pBF.X, .pBF.Y, uneRowLigne)
      End With
    Next

    Dim uneRowArc As DataSetDiagfeux.ARCRow
    For Each unArc In mArcs
      With unArc
        uneRowArc = ds.ARC.AddARCRow(.Couleur, .TypeLign, .Calque.Nom, .Rayon, .AngleDépart, .AngleBalayage, uneRowBloc)
        ds.CentreArc.AddCentreArcRow(.pOF.X, .pOF.Y, uneRowArc)
      End With
    Next

    Dim uneRowCercle As DataSetDiagfeux.CERCLERow
    For Each unCercle In mCercles
      With unCercle
        uneRowCercle = ds.CERCLE.AddCERCLERow(.Couleur, .TypeLign, .Calque.Nom, .Rayon, uneRowBloc)
        ds.CentreCercle.AddCentreCercleRow(.pOF.X, .pOF.Y, uneRowCercle)
      End With
    Next

    Dim uneRowPolyArc As DataSetDiagfeux.POLYARCRow
    Dim unPt As Pt
    Dim uneRowPI As DataSetDiagfeux.PIRow
    For Each unPolyArc In mPolyArcs
      With unPolyArc
        uneRowPolyArc = ds.POLYARC.AddPOLYARCRow(.Fermé, .Couleur, .TypeLign, .Calque.Nom, uneRowBloc)
        For Each unPt In .PtsPoly
          With unPt
            uneRowPI = ds.PI.AddPIRow(.Arrondi, .Drapeau, uneRowPolyArc)
            ds.P.AddPRow(.p.X, .p.Y, uneRowPI)
          End With
        Next
      End With
    Next

    Dim uneRowInsert As DataSetDiagfeux.INSERTRow
    For Each unInsert In mInserts
      With unInsert
        uneRowInsert = ds.INSERT.AddINSERTRow(.Couleur, .TypeLign, .Calque.Nom, .Bloc.Nom, .Echx, .rot, uneRowBloc)
        ds.pInsertion.AddpInsertionRow(.pInsertion.X, .pInsertion.Y, uneRowInsert)
      End With
    Next

    Dim uneRowSpline As DataSetDiagfeux.SPLINERow
    For Each uneSpline In mSplines
      With uneSpline
        uneRowSpline = ds.SPLINE.AddSPLINERow(.Couleur, .TypeLign, .Calque.Nom, uneRowBloc)
        For Each unPt In .PtsDessinés
          ds.PtDessiné.AddPtDessinéRow(unPt.p.X, unPt.p.Y, uneRowSpline)
        Next
      End With
    Next
  End Sub
End Class


'=====================================================================================================
'--------------------------- Collection d'objets BLOC
'=====================================================================================================
Public Class BlocCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unBloc As Bloc) As Bloc
    Me.List.Add(unBloc)
    Return unBloc
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal Blocs() As Bloc)
    Me.InnerList.AddRange(Blocs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unBloc As Bloc)
    If Me.List.Contains(unBloc) Then
      Me.List.Remove(unBloc)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unBloc As Bloc)
    Me.List.Insert(Index, unBloc)
  End Sub


  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Bloc
    Get
      Return CType(Me.List.Item(Index), Bloc)
    End Get
  End Property

  Default Public ReadOnly Property Item(ByVal Nom As String) As Bloc
    Get
      Dim unBloc As Bloc
      For Each unBloc In Me
        If unBloc.Nom = Nom Then Return unBloc
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unBloc As Bloc) As Short
    Return Me.List.IndexOf(unBloc)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal unBloc As Bloc) As Boolean
    Return Me.List.Contains(unBloc)
  End Function
  Public Function Contains(ByVal Nom As String) As Boolean
    Dim unBloc As Bloc = Item(Nom)
    Return Not IsNothing(unBloc)
  End Function
End Class

'=====================================================================================================
'--------------------------- Classe SuperBloc : En général un dessin AutoCAD entier -----------------
'=====================================================================================================
Public Class SuperBloc : Inherits Bloc
  Private mBlocs As New BlocCollection

  Public Shadows Sub Clear()
    MyBase.Clear()
    mBlocs.Clear()
  End Sub

  Public ReadOnly Property Blocs() As BlocCollection
    Get
      Return mBlocs
    End Get
  End Property

  Public Overrides Sub Enregistrer(ByVal uneRowDXF As DataSetDiagfeux.DXFRow)
    MyBase.Enregistrer(uneRowDXF)
    Dim unBloc As Bloc
    For Each unBloc In mBlocs
      unBloc.Enregistrer(uneRowDXF)
    Next
  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe ACADPoint
'=====================================================================================================

Public Class ACADPoint : Inherits Graphique
  Public p As PointF

  Public Sub New(ByVal unPoint As PointF)
    With unPoint
      p.X = .X
      p.Y = .Y
    End With
  End Sub
  Public Overrides Sub Dessiner(ByVal g1 As System.Drawing.Graphics, Optional ByVal g2 As System.Drawing.Graphics = Nothing)

  End Sub

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get
      Return 1
    End Get
  End Property

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    Return New ACADPoint(Formules.TranslationBase(p, pTrans))
  End Function

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique

  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Return New ACADPoint(CvPointF(PointDessin(p)))
  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    End If
  End Function

  Public Overloads Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal R As Single = 0.0) As Boolean

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe ACADPointCollection--------------------------
'=====================================================================================================
Public Class ACADPointCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As ACADPoint
    Get
      Return CType(Me.List.Item(Index), Object)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Ligne
'=====================================================================================================

Public Class Ligne : Inherits Graphique

  Public pAF, pBF As PointF

  Public Sub New(ByVal p1 As PointF, ByVal p2 As PointF, Optional ByVal unePlume As Pen = Nothing)
    ReDim mPoignée(1)

    pAF = p1
    pBF = p2
    mPoignée(0) = Point.Ceiling(p1)
    mPoignée(1) = Point.Ceiling(p2)
    Plume = unePlume
  End Sub

  Public Sub New(ByVal p1 As Point, ByVal p2 As Point, Optional ByVal unePlume As Pen = Nothing)
    ReDim mPoignée(1)

    pA = p1
    pB = p2
    mPoignée(0) = pA
    mPoignée(1) = pB
    Plume = unePlume
  End Sub

  Public Property pA() As Point
    Get
      Return Point.Round(pAF)
    End Get
    Set(ByVal Value As Point)
      pAF = CvPointF(Value)
    End Set
  End Property

  Public Property pB() As Point
    Get
      Return Point.Round(pBF)
    End Get
    Set(ByVal Value As Point)
      pBF = CvPointF(Value)
    End Set
  End Property

  Public Overrides Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)

    If Invisible OrElse IsNothing(Plume) Then
      Exit Sub
    End If

    Dim unePlume As Pen = Plume.Clone
    If mPointillable Then
      Dim EspacementTiret() As Single = {1, 30}
      unePlume.DashPattern = EspacementTiret
      unePlume.DashStyle = Drawing2D.DashStyle.Dot

      'Else
      '  unePlume.DashStyle = Drawing2D.DashStyle.Solid
    End If

    Try

      ' Redessiner pour montrer la nouvelle ligne.
      If IsNothing(g2) Then
        unePlume.Width /= 5
      Else
        g2.DrawLine(unePlume, pAF, pBF)
      End If

      ' Dessiner la ligne dans le tampon permanent ou sur l'imprimante
      g1.DrawLine(unePlume, pAF, pBF)

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & vbCrLf & ex.StackTrace)
    End Try

  End Sub

  Public ReadOnly Property Milieu() As Point
    Get
      Return Formules.Milieu(pA, pB)
    End Get
  End Property

  Public ReadOnly Property MilieuF() As PointF
    Get
      Return Formules.Milieu(pAF, pBF)
    End Get
  End Property

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get
      Return mPoignée.Length
    End Get
  End Property

  Public Overrides ReadOnly Property Longueur() As Single
    Get
      Return Distance(pAF, pBF)
    End Get
  End Property

  Public Function Inversée() As Ligne
    Return New Ligne(pBF, pAF, Plume)

  End Function

  Public Overrides Function Clone() As Graphique
    If IsNothing(Plume) Then
      Clone = New Ligne(pAF, pBF)
    Else
      Clone = New Ligne(pAF, pBF, Plume.Clone)
      If mAttributs.Plume Is Nothing Then mAttributs.Plume = Clone.Plume
      Clone.Attributs = Me.Attributs
    End If
  End Function

  Public Overrides Function ProcheDuPoint(ByVal pC As Point, ByRef pIntéressant As Point, Optional ByVal unRayonSélection As Single = 0) As Boolean
    Dim p1, p2 As Point

    p1 = pA
    p2 = pB

    If Distance(p1, p2) = 0 Then Return False

    If unRayonSélection = 0 Then unRayonSélection = RaySélect

    Dim Lambda As Single

    Dim X1 As Single = (p2.X - p1.X)
    Dim Y1 As Single = (p2.Y - p1.Y)
    Dim X2 As Single = (pC.X - p1.X)
    Dim Y2 As Single = (pC.Y - p1.Y)

    'Le point D cherché, projection de C sur AB est tel que AB = Lambda * AD  (AB et AD sont des vecteurs)
    If Y1 = 0 Then
      'evidemment on suppose que X1 est non nul sinon A et B seraient confondus
      Lambda = X2 / X1
    Else
      Lambda = (X2 * X1 + Y2 * Y1) / (X1 ^ 2 + Y1 ^ 2)
    End If

    Select Case Lambda
      Case Is < 0
        'La projection est en-dehors du segment, et située avant le début :on retient le point début
        pIntéressant = p1
      Case Is > 1
        'La projection est en-dehors du segment, et située après la fin :on retient le point de fin
        pIntéressant = p2
      Case Else
        ' D est le point projection du point C sur le segment AB
        pIntéressant.X = Lambda * p2.X + (1 - Lambda) * p1.X
        pIntéressant.Y = Lambda * p2.Y + (1 - Lambda) * p1.Y

    End Select

    ProcheDuPoint = Distance(pC, pIntéressant) < unRayonSélection

  End Function

  Public Overloads Overrides Function Rotation(ByVal Alpha As Single) As Graphique
    Dim pi, pj As PointF
    pi = Formules.Rotation(pAF, Alpha)
    pj = Formules.Rotation(pBF, Alpha)
    Return New Ligne(pi, pj, Plume)
  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    Dim pi, pj As PointF
    pi = Formules.TranslationBase(pAF, pTrans)
    pj = Formules.TranslationBase(pBF, pTrans)
    Return New Ligne(pi, pj, Plume)
  End Function

  Public Overrides Function Intersection(ByVal uneFigure As Graphique) As System.Drawing.PointF
    Dim p As PointF

    If TypeOf uneFigure Is Ligne Then
      'Intersection de 2 segments de ligne
      Dim uneLigne As Ligne = uneFigure
      p = Formules.intersect(Me, uneLigne)
    ElseIf TypeOf uneFigure Is Arc Then
      'Intersection d'1 segment de ligne avec un arc
      Dim unArc As Arc = uneFigure
      p = Formules.IntersectionCercleDroite(unArc.pOF, unArc.Rayon + acoTolerance, Me)
      If Not PtSurSegment(p) Then
        p = Formules.IntersectionCercleDroite(unArc.pOF, unArc.Rayon + acoTolerance, Me, PremierAppel:=False)
        If Not PtSurSegment(p) Then p = Nothing
      End If
      If Not unArc.PtSurArc(p) Then p = Nothing
    ElseIf TypeOf uneFigure Is PolyArc Then
      Dim unPolyarc As PolyArc = uneFigure
      p = unPolyarc.Intersection(Me)
    End If

    If Not p.IsEmpty Then Return p

  End Function

  '*******************************************************************************************************************
  ' Indique si le point P1 est sur le segment AB (délimité par pA et pB)
  ' Condition : P1 est consdéré comme étant sur la droite AB (à implémenter avec le Booleen AVérifier
  '*******************************************************************************************************************
  Public Function PtSurSegment(ByVal P1 As PointF, Optional ByVal AVérifier As Boolean = False) As Boolean
    If Not AVérifier And Not P1.IsEmpty Then Return Distance(P1, MilieuF) - Longueur / 2 <= 0.001
  End Function

  Public Function PtSurSegment(ByVal P1 As Point, Optional ByVal AVérifier As Boolean = False) As Boolean
    If Not AVérifier And Not P1.IsEmpty Then Return Distance(P1, Milieu) - Longueur / 2 <= 0.001
  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim TrP1, TrP2 As Point
    Dim NewLigne, LigneInsérée As Ligne

    Try
      With CType(TrInsertion(ObjInsert), Ligne)
        TrP1 = PointDessin(.pAF)
        TrP2 = PointDessin(.pBF)
      End With

      NewLigne = New Ligne(TrP1, TrP2)
      NewLigne.Attributs = New AttributGraphique(RechCouleur(mCouleur, mCalque, ObjInsert), RechTypeLign(mTypelign, mCalque, ObjInsert), Alpha:=mAlpha)
      Return NewLigne

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Ligne.PréparerDessin")
    End Try

  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique
    If IsNothing(ObjInsert) Then
      Return Me
    Else
      With ObjInsert
        Return New Ligne(.TransRot(pAF), .TransRot(pBF), Plume)
      End With
    End If

  End Function

  Public Overrides Function CvDessin() As Graphique
    Return New Ligne(PointDessin(pAF), PointDessin(pBF))
  End Function

  Public Overrides Function CvRéel() As Graphique
    Return New Ligne(pAF, pBF)
  End Function

  Public Sub RendreNonSélectable()
    ReDim mPoignée(-1)
  End Sub

End Class

'=====================================================================================================
'--------------------------- Collection d'objets LIGNE
'=====================================================================================================
Public Class LigneCollection : Inherits Graphiques

  'Public Overloads Overrides Function Add(ByVal uneFigure As Graphique, ByVal unCalque As Calque, ByVal Couleur As Integer, ByVal typelign As String, ByVal p1 As PointF, ByVal p2 As PointF) As Short

  'End Function

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As Ligne
    Get
      Return CType(Me.List.Item(Index), Ligne)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Ellipse
'=====================================================================================================
Public Class Ellipse : Inherits Graphique
  Public mRectangle As RectangleF  ' Rectangle d'encombrement de l'ellipse
  Protected uneBrosse As SolidBrush
  Public pOF As PointF
  Public pO As Point

  Public Sub New(ByVal pCentre As Point, ByVal Largeur As Single, ByVal Hauteur As Single, Optional ByVal unePlume As Pen = Nothing)
    pO = pCentre
    Me.pOF = CvPointF(pO)

    With mRectangle
      .X = pCentre.X - Largeur / 2
      .Y = pCentre.Y - Hauteur / 2
      .Width = Largeur
      .Height = Hauteur
    End With

    Plume = unePlume

  End Sub

  Public Sub New(ByVal pCentre As PointF, ByVal Largeur As Single, ByVal Hauteur As Single, Optional ByVal unePlume As Pen = Nothing)
    pOF = pCentre
    Me.pO = CvPoint(pOF)

    With mRectangle
      .X = pCentre.X - Largeur / 2
      .Y = pCentre.Y - Hauteur / 2
      .Width = Largeur
      .Height = Hauteur
    End With

    Plume = unePlume

  End Sub

  Public Overrides Sub Dessiner(ByVal g1 As System.Drawing.Graphics, Optional ByVal g2 As System.Drawing.Graphics = Nothing)

    If Invisible OrElse (IsNothing(Plume) And IsNothing(uneBrosse)) Then
      Exit Sub
    End If

    Dim unePlume As Pen
    If IsNothing(uneBrosse) Then unePlume = Plume.Clone

    If mPointillable Then
      If Not IsNothing(uneBrosse) Then
        unePlume = New Pen(uneBrosse.Color)
      End If
      Dim EspacementTiret() As Single = {10, 5}
      unePlume.DashStyle = Drawing2D.DashStyle.Dot
    Else
      If IsNothing(uneBrosse) Then
        unePlume.DashStyle = Drawing2D.DashStyle.Solid
      End If
    End If

    Try
      If IsNothing(unePlume) Then
        g1.FillEllipse(uneBrosse, mRectangle)
        If Not IsNothing(g2) Then g2.FillEllipse(uneBrosse, mRectangle)

      Else
        If IsNothing(g2) Then
          unePlume.Width /= 5
        Else
          ' Redessiner pour montrer le nouveau cercle.
          g2.DrawEllipse(unePlume, mRectangle)
        End If

        ' Dessiner le cercle dans le tampon permanent ou sur l'imprimante
        g1.DrawEllipse(unePlume, mRectangle)

      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Cercle.Dessiner")

    Finally
      If mPointillable And Not IsNothing(uneBrosse) Then unePlume = Nothing
    End Try

  End Sub

  Public Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal R As Single = 0.0) As Boolean

  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique

  End Function
  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    End If
  End Function

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get

    End Get
  End Property

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique

  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    If pO.IsEmpty Then
      Dim pi As PointF = Formules.TranslationBase(pOF, pTrans)
      Return New Ellipse(pi, mRectangle.Width, mRectangle.Height, Plume)
    Else
      Dim pi As Point = Formules.TranslationBase(pO, New Size(pTrans.Width, pTrans.Height))
      Return New Ellipse(pi, mRectangle.Width, mRectangle.Height, Plume)

    End If
  End Function
End Class

'=====================================================================================================
'--------------------------- Classe Cercle
'=====================================================================================================
Public Class Cercle : Inherits Graphique

  Public pOF As PointF
  Public Rayon As Single
  Private mRectangle As RectangleF ' Rectangle d'encombrement du cercle (en fait un carré)
  Private uneBrosse As SolidBrush

  Public Sub New(ByVal pCentre As PointF, ByVal Rayon As Single, Optional ByVal unePlume As Pen = Nothing)
    ReDim mPoignée(3)

    pOF = pCentre
    Me.Rayon = Rayon
    With mRectangle
      .X = pO.X - Rayon
      .Y = pO.Y - Rayon
      .Width = 2 * Rayon
      .Height = 2 * Rayon
    End With

    Plume = unePlume

  End Sub

  Public Sub New(ByVal pCentre As Point, ByVal Rayon As Single, Optional ByVal unePlume As Pen = Nothing)
    ReDim mPoignée(3)

    pO = pCentre
    Me.Rayon = Rayon
    With mRectangle
      .X = pO.X - Rayon
      .Y = pO.Y - Rayon
      .Width = 2 * Rayon
      .Height = 2 * Rayon
    End With
    mPoignée(0).X = pO.X - Rayon
    mPoignée(0).Y = pO.Y
    mPoignée(1).X = pO.X
    mPoignée(1).Y = pO.Y + Rayon
    mPoignée(2).X = pO.X + Rayon
    mPoignée(2).Y = pO.Y
    mPoignée(3).X = pO.X
    mPoignée(3).Y = pO.Y - Rayon

    Plume = unePlume
  End Sub

  Public Sub New(ByVal pCentre As Point, ByVal Rayon As Single, ByVal uneBrosse As SolidBrush)
    ReDim mPoignée(3)

    pO = pCentre
    Me.Rayon = Rayon
    With mRectangle
      .X = pO.X - Rayon
      .Y = pO.Y - Rayon
      .Width = 2 * Rayon
      .Height = 2 * Rayon
    End With
    mPoignée(0).X = pO.X - Rayon
    mPoignée(0).Y = pO.Y
    mPoignée(1).X = pO.X
    mPoignée(1).Y = pO.Y + Rayon
    mPoignée(2).X = pO.X + Rayon
    mPoignée(2).Y = pO.Y
    mPoignée(3).X = pO.X
    mPoignée(3).Y = pO.Y - Rayon

    Me.uneBrosse = uneBrosse

  End Sub

  Public Property pO() As Point
    Get
      Return Point.Round(pOF)
    End Get
    Set(ByVal Value As Point)
      pOF = CvPointF(Value)
    End Set
  End Property

  Public Overrides Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)

    If Invisible OrElse (IsNothing(Plume) And IsNothing(uneBrosse)) Then
      Exit Sub
    End If

    Dim unePlume As Pen
    If IsNothing(uneBrosse) Then unePlume = Plume.Clone

    If mPointillable Then
      If Not IsNothing(uneBrosse) Then
        unePlume = New Pen(uneBrosse.Color)
      End If
      Dim EspacementTiret() As Single = {10, 5}
      unePlume.DashStyle = Drawing2D.DashStyle.Dot
    Else
      If IsNothing(uneBrosse) Then
        unePlume.DashStyle = Drawing2D.DashStyle.Solid
      End If
    End If

    Try
      If IsNothing(unePlume) Then
        g1.FillEllipse(uneBrosse, mRectangle)
        If Not IsNothing(g2) Then g2.FillEllipse(uneBrosse, mRectangle)

      Else
        If IsNothing(g2) Then
          unePlume.Width /= 5
        Else
          ' Redessiner pour montrer le nouveau cercle.
          g2.DrawEllipse(unePlume, mRectangle)
        End If

        ' Dessiner le cercle dans le tampon permanent ou sur l'imprimante
        g1.DrawEllipse(unePlume, mRectangle)

      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Cercle.Dessiner")

    Finally
      If mPointillable And Not IsNothing(uneBrosse) Then unePlume = Nothing
    End Try


  End Sub

  Public Overrides Property Brosse() As SolidBrush
    Get
      Return uneBrosse
    End Get
    Set(ByVal Value As SolidBrush)
      uneBrosse = Value
    End Set
  End Property

  Public ReadOnly Property Rectangle() As RectangleF
    Get
      Return mRectangle
    End Get
  End Property

  Public Overloads Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal unRayonSélection As Single = 0.0) As Boolean

    If unRayonSélection = 0 Then unRayonSélection = RaySélect

    Dim Dist As Single = Distance(pC, pO)
    If Dist = 0 Then
      pIntéressant = pO
    Else
      'Déterminer la projection du point sur le cercle
      Dim Lambda As Single = Rayon / Dist
      Dim xM, yM As Integer
      xM = Lambda * pC.X + (1 - Lambda) * pO.X
      yM = Lambda * pC.Y + (1 - Lambda) * pO.Y
      pIntéressant = New Point(xM, yM)
    End If

    ProcheDuPoint = Dist > Rayon - unRayonSélection And Dist < Rayon + unRayonSélection
  End Function

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get
      Return 4
    End Get
  End Property

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique
    If pO.IsEmpty Then
      Dim pi As PointF = Formules.Rotation(pOF, Alpha)
      Return New Cercle(pi, Rayon, Plume)
    Else
      Dim pi As Point = Formules.Rotation(pO, Alpha)
      Return New Cercle(pi, Rayon, Plume)
    End If
  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    If pO.IsEmpty Then
      Dim pi As PointF = Formules.TranslationBase(pOF, pTrans)
      Return New Cercle(pi, Rayon, Plume)
    Else
      Dim pi As Point = Formules.TranslationBase(pO, New Size(pTrans.Width, pTrans.Height))
      Return New Cercle(pi, Rayon, Plume)

    End If
  End Function

  Public Overrides Sub Effacer(ByVal g1 As System.Drawing.Graphics, ByVal g2 As System.Drawing.Graphics)
    If IsNothing(uneBrosse) Then
      MyBase.Effacer(g1, g2)
    Else
      Dim uneCouleur As Drawing.Color = uneBrosse.Color
      uneBrosse.Color = CouleurInvisible
      Dessiner(g1, g2)
      uneBrosse.Color = uneCouleur
    End If
  End Sub

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim pO As Point
    Dim R As Single

    With CType(TrInsertion(ObjInsert), Cercle)
      pO = PointDessin(.pOF)
      R = Rayon * Echelle
    End With

    Dim NewCercle As New Cercle(pO, R, Plume)
    NewCercle.Attributs = New AttributGraphique(RechCouleur(mCouleur, mCalque, ObjInsert), RechTypeLign(mTypelign, mCalque, ObjInsert), Alpha:=mAlpha)

    Return NewCercle

  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    Else
      With ObjInsert
        Dim pCentre As PointF = .TransRot(pOF)
        Dim R As Single = Rayon * Abs(.Echx)
        Return New Cercle(pCentre, R, Plume)
      End With
    End If

  End Function

  Public Overrides Function Clone() As Graphique
    Return New Cercle(pO, Rayon, CType(Plume.Clone, Pen))
  End Function
End Class

'=====================================================================================================
'--------------------------- Collection d'objets CERCLE
'=====================================================================================================
Public Class CercleCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As Cercle
    Get
      Return CType(Me.List.Item(Index), Cercle)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Arc
'=====================================================================================================
Public Class Arc : Inherits Graphique

  Public pOF As PointF
  Private mRayon As Single
  Private mRectangle As RectangleF ' Rectangle d'encombrement du cercle (en fait un carré)
  Public AngleDépart As Single    ' Angle de départ de l'arc (compris entre [0, 360] degrés)
  Public AngleBalayage As Single  ' Angle de balayage de l'arc entre ]0,360] dans le sens horaire

  Public Sub New(ByVal pCentre As PointF, ByVal Rayon As Single, ByVal AngleDépart As Single, ByVal AngleBalayage As Single, Optional ByVal unePlume As Pen = Nothing)
    Dim Alpha As Single
    Dim xM, yM As Integer

    ReDim mPoignée(1)

    pOF = pCentre
    mRayon = Rayon
    With mRectangle
      .X = pOF.X - Rayon
      .Y = pOF.Y - Rayon
      .Width = 2 * Rayon
      .Height = 2 * Rayon
    End With

    Me.AngleDépart = AngleDépart
    Me.AngleBalayage = AngleBalayage

    Plume = unePlume

  End Sub

  Public ReadOnly Property Rayon() As Single
    Get
      Return mRayon
    End Get
  End Property
  Public Sub New(ByVal pCentre As Point, ByVal Rayon As Single, ByVal AngleDépart As Single, ByVal AngleBalayage As Single, Optional ByVal unePlume As Pen = Nothing)

    Dim Alpha As Single
    Dim xM, yM As Integer

    ReDim mPoignée(1)

    pO = pCentre
    mRayon = Rayon
    With mRectangle
      .X = pO.X - Rayon
      .Y = pO.Y - Rayon
      .Width = 2 * Rayon
      .Height = 2 * Rayon
    End With

    Me.AngleDépart = AngleDépart
    Me.AngleBalayage = AngleBalayage

    mPoignée(0) = PointPosition(pO, Rayon, AngleDépart, SensHoraire:=True)
    mPoignée(1) = PointPosition(pO, Rayon, AngleDépart + AngleBalayage, SensHoraire:=True)

    If AngleBalayage > 10 Then
      ReDim Preserve mPoignée(2)
      mPoignée(2) = PointPosition(pO, Rayon, AngleDépart + AngleBalayage / 2, SensHoraire:=True)
    End If

    Plume = unePlume

  End Sub

  Public Property pO() As Point
    Get
      Return Point.Round(pOF)
    End Get
    Set(ByVal Value As Point)
      pOF = CvPointF(Value)
    End Set
  End Property

  Public Overrides Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)
    If Invisible OrElse IsNothing(Plume) Then
      Exit Sub
    End If

    Dim unePlume As Pen = Plume.Clone

    If mPointillable Then
      Dim EspacementTiret() As Single = {1, 30}
      unePlume.DashPattern = EspacementTiret
      unePlume.DashStyle = Drawing2D.DashStyle.Dot
    Else
      unePlume.DashStyle = Drawing2D.DashStyle.Solid
    End If

    Try
      If IsNothing(g2) Then
        unePlume.Width /= 5
      Else
        ' Redessiner pour montrer le nouvel arc.
        g2.DrawArc(unePlume, mRectangle, AngleDépart, AngleBalayage)
      End If

      ' Dessiner l'arc dans le tampon permanent ou sur l'imprimante
      g1.DrawArc(unePlume, mRectangle, AngleDépart, AngleBalayage)


    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Arc.Dessiner")
    End Try
  End Sub

  Public Overloads Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal unRayonSélection As Single = 0.0) As Boolean

    Dim Dist As Single = Distance(pC, pO)

    If unRayonSélection = 0 Then unRayonSélection = RaySélect
    If Dist > Rayon - unRayonSélection And Dist < Rayon + unRayonSélection Then

      If Dist = 0 Then
        pIntéressant = pO
      Else
        If Distance(Poignée(0), pC) < unRayonSélection Then
          'Clic Proche du début de l'arc
          pIntéressant = Poignée(0)
          ProcheDuPoint = True
        ElseIf Distance(Poignée(1), pC) < unRayonSélection Then
          'Clic Proche de la fin de l'arc
          pIntéressant = Poignée(1)
          ProcheDuPoint = True
        Else
          ' Déterminer la projection du point sur le cercle : OM = Lambda * OC (vectoriellemt)
          Dim Lambda As Single = Rayon / Dist   ' c'est à dire : OM/OC

          Dim pM As Point = PointPosition(pO, pC, Lambda)

          Dim Angle As Single = CvAngleDegrés(AngleFormé(New Vecteur(pO, pM)))
          pIntéressant = pM
          If Angle < AngleDépart Then Angle += 360
          ProcheDuPoint = (Angle <= AngleDépart + AngleBalayage)
        End If
      End If

    End If

  End Function

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get
      Return mPoignée.Length
    End Get
  End Property

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique
    If pO.IsEmpty Then
      Dim pi As PointF
      pi = Formules.Rotation(pOF, Alpha)
      Return New Arc(pi, Rayon, AngleDépart, AngleBalayage, Plume)
    Else
      Dim pi As Point
      pi = Formules.Rotation(pO, Alpha)
      Return New Arc(pi, Rayon, AngleDépart, AngleBalayage, Plume)
    End If

  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    If pO.IsEmpty Then
      Dim pi As PointF
      pi = Formules.TranslationBase(pOF, pTrans)
      Return New Arc(pi, Rayon, AngleDépart, AngleBalayage, Plume)
    Else
      Dim pi As Point
      pi = Formules.TranslationBase(pO, New Size(pTrans.Width, pTrans.Height))
      Return New Arc(pi, Rayon, AngleDépart, AngleBalayage, Plume)
    End If
  End Function

  Public Overrides Function Intersection(ByVal uneFigure As Graphique) As System.Drawing.PointF
    Dim p As PointF

    If TypeOf uneFigure Is Ligne Then
      'Intersection de l'arc avec une ligne
      Dim uneLigne As Ligne = uneFigure
      p = Formules.IntersectionCercleDroite(pOF, Rayon + acoTolerance, uneLigne)
      If Not uneLigne.PtSurSegment(p) Then
        p = Formules.IntersectionCercleDroite(pOF, Rayon + acoTolerance, uneLigne, PremierAppel:=False)
        If Not uneLigne.PtSurSegment(p) Then p = Nothing
      End If

    ElseIf TypeOf uneFigure Is Arc Then
      'Intersection de 2 arcs
      Dim unArc As Arc = uneFigure
      p = Formules.IntersectionCercles(Me.pOF, unArc.pOF, Me.Rayon + acoTolerance, unArc.Rayon + acoTolerance)
      If Not PtSurArc(p) Then p = Formules.IntersectionCercles(Me.pOF, unArc.pOF, Me.Rayon + acoTolerance, unArc.Rayon + acoTolerance, PremierAppel:=False)
      If Not unArc.PtSurArc(p) Then p = Nothing

    ElseIf TypeOf uneFigure Is PolyArc Then
      Dim unPolyarc As PolyArc = uneFigure
      p = unPolyarc.Intersection(Me)
    End If

    If PtSurArc(p) Then Return p

  End Function

  Public Function PtSurArc(ByVal p As Point) As Boolean

    Return PtSurArc(CvPointF(p))

  End Function

  Public Function PtSurArc(ByVal p As PointF) As Boolean

    If Not p.IsEmpty Then
      Dim unAngle As Single = AngleFormé(pOF, p)
      Dim AngleEnDegrés = CvAngleDegrés(unAngle, InverserSens:=False)

      Dim AngleFinal As Single = AngleDépart + AngleBalayage
      If AngleFinal < 360 Then
        Return (AngleEnDegrés >= AngleDépart - 3) And (AngleEnDegrés <= AngleFinal + 3)
      Else
        Return (AngleEnDegrés >= AngleDépart - 3) Or (AngleEnDegrés <= AngleFinal - 360 + 3)
      End If
    End If

  End Function

  Private Function AngleFinal() As Single
    AngleFinal = AngleDépart + AngleBalayage
    If AngleFinal >= 360 Then AngleFinal -= 360
  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim pO As Point
    Dim R As Single
    Dim unAngle As Single

    With CType(TrInsertion(ObjInsert), Arc)
      pO = PointDessin(.pOF)
      R = Rayon * Echelle
      unAngle = .AngleDépart
    End With

    Dim NewArc As New Arc(pO, R, unAngle, AngleBalayage)
    NewArc.Attributs = New AttributGraphique(RechCouleur(mCouleur, mCalque, ObjInsert), RechTypeLign(mTypelign, mCalque, ObjInsert), Alpha:=mAlpha)

    Return NewArc
  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    Else
      With ObjInsert
        Dim pCentre As PointF = .TransRot(pOF)
        Dim R As Single = Rayon * Abs(.Echx)
        Dim unAngle As Single
        If .Echx < 0 Then
          unAngle = 360 - AngleFinal()
        Else
          unAngle = AngleDépart
        End If
        Return New Arc(pCentre, R, unAngle + .rot, AngleBalayage, Plume)
      End With
    End If

  End Function

  Public Overrides Function CvRéel() As Graphique
    Return New Arc(CvPointF(pO), Rayon / Echelle, AngleDépart, AngleBalayage)

  End Function

  Public Overrides Function CvDessin() As Graphique
    Return New Arc(PointDessin(pOF), Rayon * Echelle, AngleDépart, AngleBalayage)
  End Function

  Public Overrides ReadOnly Property Longueur() As Single
    Get
      Return Rayon * Abs(CvAngleRadians(AngleBalayage))
    End Get
  End Property
End Class

'=====================================================================================================
'--------------------------- Collection d'objets ARC
'=====================================================================================================
Public Class ArcCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As Arc
    Get
      Return CType(Me.List.Item(Index), Arc)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Spline --------------------------
'=====================================================================================================
Public Class Spline : Inherits PolyArc
  Public Ordre As Short
  Public Rational As Boolean

  Public tolNoeuds As Single
  Public tolPControle As Single

  Public PtsControle As New Pts
  Public PtsDessinés As New Pts

  Private mNoeuds(-1) As Single

  Public Property Noeuds(ByVal Index As Short) As Single
    Get
      Return mNoeuds(Index)
    End Get
    Set(ByVal Value As Single)
      If Index > mNoeuds.Length - 1 Then ReDim Preserve mNoeuds(Index)
      mNoeuds(Index) = Value
    End Set
  End Property

  Public Sub Etablir(ByVal nbPoints As Short)
    Dim fin As Single, intervalle As Single
    Dim i As Short, j As Short
    Dim temp As Single
    Dim nbasis() As Double
    Dim p As Pt
    Dim pb As Pt
    Dim nPlusOrdre As Short
    Dim t As Single
    Dim npts As Short

    'PtsDessinés

    Try
      npts = PtsControle.Count

      intervalle = Noeuds(mNoeuds.Length - 1) / (nbPoints - 1)

      ReDim nbasis(npts - 1)
      'Il faut n fonctions d'ordre k(=Ordre) autant que de points de controle
      'Il faut n+k fonctions de base d'ordre 1 puisqu'on perd une fonction à chaque fois qu'on élève le degré et donc l'ordre de la courbe
      nPlusOrdre = npts + Ordre

      For i = 1 To nbPoints

        If Noeuds(nPlusOrdre - 1) - t < 0.000005 Then
          t = Noeuds(nPlusOrdre - 1)
        End If

        Basis(t, nbasis)   '      /* generate the basis function for this value of t */

        pb = New Pt(New PointF(0.0, 0.0))

        j = 0
        For Each p In PtsControle    ' /* Do local matrix multiplication */
          temp = nbasis(j) * p.p.X
          pb.p.X += temp
          temp = nbasis(j) * p.p.Y
          pb.p.Y += temp
          j += 1
        Next

        PtsDessinés.Add(pb)

        t += intervalle
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Spline.Etablir")
    End Try

  End Sub

  Private Sub Basis(ByVal t As Single, ByRef n() As Double)

    '/*  Subroutine to generate B-spline basis functions for open knot vectors
    '    t        = parameter value
    '    n[]      = array containing the basis functions
    '               n[1] contains the basis function associated with B1 etc.

    '    nPlusOrdre   = constant -- npts + ordre -- maximum number of knot values
    '    temp[]   = temporary array

    '    d        = first term of the basis function recursion relation
    '    e        = second term of the basis function recursion relation
    '*/

    Try
      Dim nPlusOrdre As Short
      Dim i, k As Short
      Dim d, e As Double
      Dim temp() As Double
      Dim Somme As Single

      nPlusOrdre = PtsControle.Count + Ordre

      ReDim temp(nPlusOrdre - 1)

      '/* calculate the first order basis functions n[i][1]  */

      For i = 1 To nPlusOrdre - 1
        If t >= Noeuds(i - 1) And t < Noeuds(i) Then
          temp(i - 1) = 1
        Else
          temp(i - 1) = 0
        End If
      Next

      '/* calculate the higher order basis functions */

      For k = 2 To Ordre
        For i = 1 To nPlusOrdre - k
          If temp(i - 1) = 0.0# Then '    /* if the lower order basis function is zero skip the calculation */
            d = 0
          Else
            d = ((t - Noeuds(i - 1)) * temp(i - 1)) / (Noeuds(i + k - 2) - Noeuds(i - 1))
          End If

          If temp(i) = 0.0# Then  '     /* if the lower order basis function is zero skip the calculation */
            e = 0
          Else
            e = ((Noeuds(i + k - 1) - t) * temp(i)) / (Noeuds(i + k - 1) - Noeuds(i))
          End If

          temp(i - 1) = d + e
        Next
      Next

      If (t = Noeuds(nPlusOrdre - 1)) Then '   /*    pick up last point  */
        temp(PtsControle.Count - 1) = 1
      End If

      '/* put in n array */

      If Rational Then
        Somme = 0.0#
        For i = 0 To PtsControle.Count - 1
          Somme = Somme + temp(i) * PtsControle(i).Arrondi
        Next
        For i = 0 To PtsControle.Count - 1
          If Somme <> 0.0# Then
            n(i) = temp(i) * PtsControle(i).Arrondi / Somme
          Else
            n(i) = 0.0#
          End If
        Next

      Else
        For i = 0 To PtsControle.Count - 1
          n(i) = temp(i)
        Next
      End If


    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Spline.Basis")
    End Try

  End Sub

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim i As Short
    Dim uneLigne As Ligne
    Dim pa, pb As Pt
    Dim uneSpline As New Spline

    For i = 1 To PtsDessinés.Count
      pb = PtsDessinés(i - 1)
      If i > 1 Then
        uneLigne = New Ligne(pa.p, pb.p)
        uneLigne.DéfinirAttributs(mCalque, mCouleur, mTypelign)
        uneSpline.Add(uneLigne.PréparerDessin(ObjInsert), PoignéesACréer:=False)
      End If
      pa = pb
    Next

    Return uneSpline

  End Function

End Class

'=====================================================================================================
'--------------------------- Collection d'objets SPLINE
'=====================================================================================================
Public Class SplineCollection : Inherits Graphiques

End Class


'=====================================================================================================
'--------------------------- Classe Surface --------------------------
'=====================================================================================================
Public Class Surface : Inherits PolyArc

  Public Sub New(ByVal tabPoint As Point())
    MyBase.New(tabPoint, Clore:=True)
  End Sub

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique

  End Function

  Protected Overrides Sub CréerPoignées(Optional ByVal uneFigure As Graphique = Nothing)
    ReDim mPoignée(1)
  End Sub

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get
      Return mPoignée.Length
    End Get
  End Property
End Class

'=====================================================================================================
'--------------------------- Classe PolyArc : suite de segments et d'arcs de cercle
'=====================================================================================================
Public Class PolyArc : Inherits Graphique
  Protected colObjets As New Graphiques
  'Protected nBrosse As SolidBrush
  Protected Enveloppe() As PointF
  Protected mPoint() As PointF
  Public PtsPoly As Pts   ' Points issus du DXF
  Private mClore As Boolean
  Public TypeLissage As Short
  Private mEditable As Boolean
  Public mpRef As PointF

  Public Sub New()
    ReDim mPoignée(-1)
    ReDim Enveloppe(-1)
    ReDim mPoint(-1)
  End Sub

  Public Sub New(ByVal tabPoint() As PointF, ByVal Clore As Boolean)
    ReDim mPoignée(-1)
    ReDim Enveloppe(-1)
    ReDim mPoint(tabPoint.Length - 1)

    Dim i As Short

    For i = 0 To mPoint.Length - 1
      mPoint(i) = tabPoint(i)
    Next

    mClore = Clore

  End Sub

  Public Sub New(ByVal tabPoint() As Point, ByVal Clore As Boolean)
    ReDim mPoignée(-1)
    ReDim Enveloppe(-1)
    ReDim mPoint(tabPoint.Length - 1)

    Dim i As Short

    For i = 0 To mPoint.Length - 1
      mPoint(i) = CvPointF(tabPoint(i))
    Next

    mClore = Clore
  End Sub

  Public Sub New(ByVal Autocadien As Boolean)
    ReDim mPoignée(-1)
    ReDim Enveloppe(-1)
    ReDim mPoint(-1)
    PtsPoly = New Pts
  End Sub

  Public Overrides Function Clone() As Graphique
    Dim unPolyArc As PolyArc

    If mPoint.Length = 0 Then
      Dim uneFigure As Graphique
      unPolyArc = New PolyArc
      For Each uneFigure In Me.Figures
        unPolyArc.Add(uneFigure.Clone)
      Next

    Else
      unPolyArc = New PolyArc(mPoint, Clore:=mClore)
      unPolyArc.Plume = Plume.Clone
    End If

    Return unPolyArc
  End Function
  Public Property Fermé() As Boolean
    Get
      Return (mClore)
    End Get
    Set(ByVal Value As Boolean)
      mClore = Value
    End Set
  End Property

  Public ReadOnly Property PtsUtiles() As Pts
    Get
      Dim unPt As Pt
      Dim mPts As Pts

      If TypeLissage = 0 Then
        PtsUtiles = PtsPoly
      Else
        mPts = New Pts
        For Each unPt In PtsPoly
          With unPt
            If (.Drapeau And 16) <> 16 Then   ' Si drapeau = 16 : point de controle --> n'est pas sur la courbe
              mPts.Add(.p.X, .p.Y, .Arrondi, .Drapeau)
            End If
          End With
        Next
        PtsUtiles = mPts
        mPts = Nothing
      End If
    End Get
  End Property

  Public Function Add(ByVal uneFigure As Graphique, Optional ByVal PoignéesACréer As Boolean = True) As Graphique

    If IsNothing(uneFigure) Then Exit Function

    If uneFigure.NbPoignées = 0 Then PoignéesACréer = False

    If PoignéesACréer Then
      CréerPoignées(uneFigure)
      mEditable = True
    End If

    Envelopper(uneFigure)

    Return colObjets.Add(uneFigure)

  End Function

  Public Overloads Function Intersection(ByVal uneLigne As Ligne) As PointF
    Dim uneFigure As Graphique
    Dim p As PointF
    Dim i As Short
    Dim Continuer As Boolean

    For Each uneFigure In Me.Figures
      p = uneLigne.Intersection(uneFigure)
      If Not p.IsEmpty Then Return p
    Next

  End Function

  '***************************************************************************************************
  ' Recherche l'intersection entre les différents éléments du Polyarc et ceux d'un autre Polyarc
  ' unPolyarc : aure polyarc susceptble de m'intercepter
  ' pts :  tableau de points déjà utilisés et à refuser
  '***************************************************************************************************
  Public Overloads Function Intersection(ByVal unPolyarc As PolyArc) As PointF

    Dim Figure1, Figure2 As Graphique
    Dim p As PointF
    Dim i As Short
    Dim Continuer As Boolean

    For Each Figure1 In Me.Figures

      For Each Figure2 In unPolyarc.Figures
        If TypeOf Figure2 Is PolyArc Then
          Dim unPoly As PolyArc = Figure2
          p = unPoly.Intersection(Figure1)
        ElseIf TypeOf Figure2 Is Ligne Then
          Dim uneLigne As Ligne = Figure2
          p = uneLigne.Intersection(Figure1)
        ElseIf TypeOf Figure2 Is Arc Then
          Dim unArc As Arc = Figure2
          p = unArc.Intersection(Figure1)
        End If

        If Not p.IsEmpty Then
          'For i = 0 To pts.Length - 1
          '  If Distance(pts(i), p) < 4 Then
          '    Continuer = True
          '    Exit For
          '  End If
          'Next
          If Continuer Then
            Continuer = False
          Else
            Return p
          End If
        End If

      Next
    Next

  End Function

  Public Sub ChangerPlume(ByVal unePlume As Pen)
    Dim uneFigure As Graphique
    Dim unPolyarc As PolyArc

    If mPoint.Length = 0 Then
      For Each uneFigure In Me.Figures
        If TypeOf uneFigure Is PolyArc Then
          unPolyarc = uneFigure
          unPolyarc.ChangerPlume(unePlume)
        Else
          uneFigure.Plume = unePlume
        End If
      Next

    Else
      Plume = unePlume
    End If

  End Sub

  Protected Overridable Sub CréerPoignées(Optional ByVal uneFigure As Graphique = Nothing)
    Dim Index, i As Short
    Dim distMin As Single

    If IsNothing(uneFigure) Then
      For Each uneFigure In Figures()
        CréerPoignées(uneFigure)
      Next

    Else

      If mPoignée.Length = 0 Then
        ReDim mPoignée(0)
        mPoignée(0) = uneFigure.Poignée(0)
      End If

      For Index = 0 To uneFigure.NbPoignées - 1
        distMin = 500
        For i = 0 To mPoignée.Length - 1
          distMin = Min(Distance(mPoignée(i), uneFigure.Poignée(Index)), distMin)
        Next
        If distMin > 1 Then
          ReDim Preserve mPoignée(mPoignée.Length)
          mPoignée(mPoignée.Length - 1) = uneFigure.Poignée(Index)
        End If
      Next
    End If

  End Sub

  Public Sub AjouterPoignéeMilieu(ByVal Index1 As Short, ByVal Index2 As Short)
    ReDim Preserve mPoignée(mPoignée.Length)

    mPoignée(mPoignée.Length - 1) = Milieu(mPoignée(Index1), mPoignée(Index2))

  End Sub

  Public Property Editable() As Boolean
    Get
      Return mEditable
    End Get
    Set(ByVal Value As Boolean)
      mEditable = Value
    End Set
  End Property

  Public Sub RendreSélectable(ByVal Sélectable As Boolean, Optional ByVal uneFigure As Graphique = Nothing, Optional ByVal Editable As Boolean = True)
    If Sélectable Then
      CréerPoignées(uneFigure)
      mEditable = Editable
    Else
      ReDim mPoignée(-1)
      'Quelle que soit la valeur de Editable, l'objet n'est pas Editable  s'il n'est pas sélectable
      mEditable = False
    End If
  End Sub

  Private Sub Envelopper(ByVal uneFigure As Graphique)

    If TypeOf uneFigure Is Ligne Then
      Dim uneLigne As Ligne = uneFigure
      If Enveloppe.Length = 0 Then
        ReDim Enveloppe(0)
        Enveloppe(0) = uneLigne.pAF
      End If
      ReDim Preserve Enveloppe(Enveloppe.Length)
      Enveloppe(Enveloppe.Length - 1) = uneLigne.pBF
    End If

  End Sub

  Default Public ReadOnly Property Item(ByVal Index As Short) As Graphique
    Get
      Return colObjets.Item(Index)
    End Get
  End Property

  Public ReadOnly Property Count() As Short
    Get
      Return colObjets.Count
    End Get
  End Property

  Public Sub Remove(ByVal uneFigure As Graphique)
    colObjets.Remove(uneFigure)
  End Sub

  Public Sub Clear()
    colObjets.Clear()
  End Sub

  Public Function IndexOf(ByVal uneFigure As Graphique) As Short
    Return colObjets.IndexOf(uneFigure)
  End Function

  '***************************************************************************
  ' Dessiner le Polyarc
  '***************************************************************************
  Public Overrides Sub Dessiner(ByVal g1 As Graphics, Optional ByVal g2 As Graphics = Nothing)
    If Invisible Then Exit Sub

    Dim i As Short

    If APeindre Then
      'Polygone à remplir le contour est défini par une séquence de points
      Dim unPolygone As PointF()
      ReDim unPolygone(mPoint.Length - 1)
      For i = 0 To mPoint.Length - 1
        unPolygone(i) = mPoint(i)
      Next
      g1.FillPolygon(mBrosse, unPolygone)
      If Not IsNothing(g2) Then g2.FillPolygon(mBrosse, unPolygone)

    ElseIf mPoint.Length = 0 Then
      'Polyarc constitué d'un ensemble d'objets graphiques (lignes, arcs, polyarcs...)
      If mpRef.IsEmpty Then
        Figures.Dessiner(g1, g2)
      Else
        TranslationBase(New SizeF(mpRef.X, mpRef.Y)).Dessiner(g1, g2)
      End If

    Else
      ' Polyligne décrite par une liste de points
      Dim unePlume As Pen = Plume
      If Not IsNothing(unePlume) Then
        If IsNothing(g2) Then unePlume.Width /= 5
        For i = 0 To mPoint.Length - 2
          g1.DrawLine(unePlume, mPoint(i), mPoint(i + 1))
          If Not IsNothing(g2) Then g2.DrawLine(unePlume, mPoint(i), mPoint(i + 1))
        Next
        If mClore Then
          g1.DrawLine(unePlume, mPoint(i), mPoint(0))
          If Not IsNothing(g2) Then g2.DrawLine(unePlume, mPoint(i), mPoint(0))
        End If
      End If

    End If

  End Sub

  Public Property APeindre() As Boolean
    Get
      Return Not IsNothing(mBrosse)
    End Get
    Set(ByVal Value As Boolean)
      If Value Then
        mBrosse = New SolidBrush(Color.Gray)
      Else
        mBrosse = Nothing
      End If
    End Set
  End Property

  Public Overrides ReadOnly Property NbPoignées() As Short

    Get
      If mPoint.Length = 0 Then
        Return mPoignée.Length
      Else
        Return mPoint.Length
      End If
    End Get

  End Property

  Public Overrides ReadOnly Property Poignée(ByVal Index As Short) As System.Drawing.Point
    Get
      If mPoint.Length = 0 Then
        Return MyBase.Poignée(Index)
      Else
        Return CvPoint(mPoint(Index))
      End If

    End Get
  End Property

  Public Function Points() As PointF()
    Return mPoint
  End Function

  Public Function Figures() As Graphiques
    Return colObjets
  End Function

  '*******************************************************************************************************************
  ' Indique si le point est à l'intérieur du PolyArc
  ' PolyArc est supposé convexe
  '*******************************************************************************************************************
  Public Function Intérieur(ByVal pSouris As Point) As Boolean

    Dim p As Point
    Dim i As Short
    Dim Contour() As PointF

    If mPoint.Length = 0 Then
      Contour = Enveloppe
    Else
      Contour = mPoint
    End If

    If Contour.Length > 2 Then

      For i = 0 To Contour.Length - 1
        p = CvPoint(Contour(i))
        If Distance(pSouris, p) < 0.1 Then Return True
      Next

      p = CvPoint(Contour(0))
      Dim p0 As Point = CvPoint(Contour(1))
      'Déterminer le sens(horaire ou trigo) de l'angle au centre pSouris avec les 2 premiers points
      Dim Sens As Short = Sign(AngleFormé(p, pSouris, p0))
      'Balayer tous les autres angles au centre pSouris avec 2 points consécutifs
      For i = 2 To Contour.Length
        p = p0
        p0 = CvPoint(Contour(i Mod Contour.Length))
        'Si le mouvement change de sens, le point est à l'extérieur du polygone
        If Sign(AngleFormé(p, pSouris, p0)) <> Sens Then Return False
      Next

      Return True

    End If

  End Function

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique
    Dim unPolyarc As PolyArc

    If TypeOf Me Is Fleche Then
      unPolyarc = New Fleche
    Else
      unPolyarc = New PolyArc
    End If

    If mPoint.Length = 0 Then
      Dim uneFigure As Graphique

      For Each uneFigure In Figures()
        unPolyarc.Add(uneFigure.Rotation(Alpha))
      Next

    Else
      Dim p(mPoint.Length - 1) As PointF
      Dim i As Short
      For i = 0 To mPoint.Length - 1
        p(i) = Formules.Rotation(mPoint(i), Alpha)
      Next
      unPolyarc = New PolyArc(p, Clore:=Me.mClore)
      unPolyarc.Plume = Plume
    End If

    Return unPolyarc

  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    Dim unPolyarc As PolyArc

    If mPoint.Length = 0 Then
      If TypeOf Me Is Fleche Then
        unPolyarc = New Fleche
      Else
        unPolyarc = New PolyArc
      End If

      Dim uneFigure As Graphique
      For Each uneFigure In Figures()
        unPolyarc.Add(uneFigure.TranslationBase(pTrans))
      Next
      Return unPolyarc

    Else
      Dim p(mPoint.Length - 1) As PointF
      Dim i As Short
      For i = 0 To mPoint.Length - 1
        p(i) = Formules.TranslationBase(mPoint(i), New Size(pTrans.Width, pTrans.Height))
      Next
      If TypeOf Me Is Boite Then
        Return New Boite(CvTabPoint(p), unePlume:=Plume)
      Else
        unPolyarc = New PolyArc(p, Clore:=Me.mClore)
        unPolyarc.Plume = Plume
        Return unPolyarc
      End If
    End If

  End Function

  Public Overrides Sub Effacer(ByVal g1 As System.Drawing.Graphics, ByVal g2 As System.Drawing.Graphics)

    Try

      If APeindre Then
        Dim sauvBrosse As SolidBrush = mBrosse.Clone
        mBrosse.Color = CouleurInvisible
        Dessiner(g1, g2)
        mBrosse = sauvBrosse
      ElseIf mPoint.Length > 0 Then
        If Not IsNothing(mPlume) Then
          'On n'efface pas l'objet s'il ne se dessine pas (mPlume Is Nothing)
          Dim sauvPlume As Pen = mPlume.Clone
          mPlume.Color = CouleurInvisible
          Dessiner(g1, g2)
          mPlume = sauvPlume
        End If

      Else
        'Polyarc constitué d'un ensemble d'objets graphiques (lignes, arcs, polyarcs...)
        Figures.Effacer(g1, g2)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PolyArc.Effacer")

    End Try

  End Sub

  Public Overrides Property Pointillable() As Boolean
    Get
      Return (mPointillable)

    End Get
    Set(ByVal Value As Boolean)
      Dim uneFigure As Graphique
      For Each uneFigure In Figures()
        uneFigure.Pointillable = Value
      Next
    End Set
  End Property

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim mPts As Pts = PtsUtiles
    Dim unPolyarc As New PolyArc

    If IsNothing(mPts) Then
      unPolyarc.Figures.AddRange(Figures.PréparerDessin(ObjInsert))
      'Dim uneFigure As Graphique
      'For Each uneFigure In Figures()
      '  unPolyarc.Add(uneFigure.PréparerDessin(ObjInsert), PoignéesACréer:=False)
      'Next

    Else
      Dim pa, pb As Pt
      Dim i, j As Short
      Dim nbPoints, nbSegments, iSuivant As Short
      Dim uneLigne As Ligne

      nbPoints = mPts.Count

      If mClore Then
        nbSegments = nbPoints
      Else
        nbSegments = nbPoints - 1
      End If
      For i = 1 To nbSegments
        iSuivant = i Mod nbPoints
        pa = mPts(i - 1)
        pb = mPts(iSuivant)
        If pa.Arrondi <> 0 Then
          Try
            unPolyarc.Add(CType(Figures(j), Arc).PréparerDessin(ObjInsert), PoignéesACréer:=False)
          Catch ex As System.Exception
            LancerDiagfeuxException(ex, "PolyArc.Préparerdessin")
          End Try
          j += 1
        Else
          uneLigne = New Ligne(pa.p, pb.p)
          uneLigne.DéfinirAttributs(mCalque, mCouleur, mTypelign)
          unPolyarc.Add(uneLigne.PréparerDessin(ObjInsert), PoignéesACréer:=False)
        End If
      Next
    End If

    Return unPolyarc

  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    Else

      Dim mPts As Pts = PtsUtiles
      Dim unPolyArc As New PolyArc
      Dim pa, pb As Pt
      Dim i, j As Short
      Dim nbPoints, nbSegments, iSuivant As Short
      Dim uneLigne As Ligne

      If IsNothing(mPts) Then
        If mPoint.Length = 0 Then
          Dim uneFigure As Graphique
          For Each uneFigure In Me.Figures
            unPolyArc.Add(uneFigure.TrInsertion(ObjInsert))
          Next

        Else
          Dim p1, p2 As Point
          For i = 0 To mPoint.Length - 2
            unPolyArc.Add(New Ligne(mPoint(i), mPoint(i + 1), Plume).TrInsertion(ObjInsert))
          Next
          If mClore Then
            unPolyArc.Add(New Ligne(mPoint(i), mPoint(0), Plume).TrInsertion(ObjInsert))
          End If

        End If

      Else
        nbPoints = mPts.Count

        If mClore Then
          nbSegments = nbPoints
        Else
          nbSegments = nbPoints - 1
        End If
        For i = 1 To nbSegments
          iSuivant = i Mod nbPoints
          pa = mPts(i - 1)
          pb = mPts(iSuivant)
          If pa.Arrondi <> 0 Then
            unPolyArc.Add(CType(Figures(j), Arc).TrInsertion(ObjInsert))
            j += 1
          Else
            uneLigne = New Ligne(pa.p, pb.p)
            unPolyArc.Add(uneLigne.TrInsertion(ObjInsert))
          End If
        Next

      End If

      Return unPolyArc
    End If

  End Function

  Public Overrides Sub DéfinirAttributs(ByVal unCalque As Calque, ByVal uneCouleur As Integer, ByVal typelign As String, Optional ByVal Alpha As Integer = 92)
    MyBase.DéfinirAttributs(unCalque, uneCouleur, typelign)
    Dim uneFigure As Graphique
    For Each uneFigure In Figures()
      uneFigure.DéfinirAttributs(unCalque, uneCouleur, typelign, Alpha:=Alpha)
    Next
  End Sub

  Public Overloads Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal unRayonSélection As Single = 0.0) As Boolean
    Dim uneFigure As Graphique

    If unRayonSélection = 0 Then unRayonSélection = RaySélect

    If mPoint.Length = 0 Then
      uneFigure = Figures.RechercherObject(pC, pIntéressant)
      If Not IsNothing(uneFigure) Then
        ProcheDuPoint = True
      End If

    Else  ' Polyarc décrit comme un contour convexe
      Dim i As Short
      Dim distMin, DistMinPréc As Single
      Dim pProjeté As Point
      DistMinPréc = 500

      If Intérieur(pC) Then
        ProcheDuPoint = True
        pIntéressant = pC
      Else
        Dim nbLignes = IIf(Me.Fermé, mPoint.Length, mPoint.Length - 1)
        For i = 0 To nbLignes - 1
          If New Ligne(mPoint(i), mPoint((i + 1) Mod mPoint.Length)).ProcheDuPoint(pC, pProjeté, unRayonSélection) Then
            distMin = Min(Distance(pC, pProjeté), DistMinPréc)

            If distMin < DistMinPréc Then
              DistMinPréc = distMin
              pIntéressant = pProjeté
            End If
          End If
        Next
        ProcheDuPoint = (DistMinPréc < unRayonSélection)
      End If
    End If

  End Function

  Public Overrides ReadOnly Property Longueur() As Single
    Get
      Dim uneFigure As Graphique
      For Each uneFigure In Figures()
        Longueur += uneFigure.Longueur
      Next
    End Get
  End Property

  Public Sub CréerBoiteTexte(ByVal pCentre As Point, ByVal DemiLargeur As Short, ByVal Chaine As String, ByVal uneBrosse As SolidBrush, Optional ByVal unePlume As Pen = Nothing, Optional ByVal uneFonte As Font = Nothing)

    Dim uneBoite As Boite = Boite.NouvelleBoite(DemiLargeur, unePlume:=unePlume)
    uneBoite = CType(uneBoite.Translation(pCentre), Boite)
    Add(uneBoite, PoignéesACréer:=False)
    Dim unTexte As Texte = New Texte(Chaine, uneBoite, uneBrosse, uneFonte)
    Add(unTexte)

  End Sub

  Public Function CréerCercleTexte(ByVal pCentre As PointF, ByVal Rayon As Single, ByVal unePlume As Pen, ByVal Chaine As String, ByVal uneBrosse As SolidBrush, Optional ByVal uneFonte As Font = Nothing) As PolyArc
    '  If Len(Chaine) > 1 Then Rayon = 3
    Dim unPolyArc As New PolyArc
    Dim pRef As Point

    With unPolyArc
      .mpRef = pCentre
      'Décrire le cercle entourant le texte
      Dim unCercle As New Cercle(pRef, Rayon, unePlume)
      .Add(unCercle)
      'Définir la boite d'encombrement du texte en fonction de la taille du cercle
      Dim uneBoite As Boite = Boite.NouvelleBoite(unCercle.Rayon)
      'uneBoite = CType(uneBoite.Translation(pCentre), Boite)
      'Définir le texte contenant le numéro
      Dim unTexte As Texte = New Texte(Chaine, uneBoite, uneBrosse, uneFonte)
      .Add(unTexte)
    End With

    Return Add(unPolyArc)

  End Function

  Public Overrides Property Plume() As System.Drawing.Pen
    Get
      Return MyBase.Plume
    End Get
    Set(ByVal Value As System.Drawing.Pen)
      If mPoint.Length = 0 Then
        Dim uneFigure As Graphique
        For Each uneFigure In Me.Figures
          uneFigure.Plume = Value
        Next
      Else
        MyBase.Plume = Value
      End If

    End Set
  End Property
End Class

'=====================================================================================================
'--------------------------- Collection d'objets ARC
'=====================================================================================================
Public Class PolyArcCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As PolyArc
    Get
      Return CType(Me.List.Item(Index), PolyArc)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Boite
'=====================================================================================================
Public Class Boite : Inherits PolyArc

  Public Sub New(ByVal Points As Point(), Optional ByVal unePlume As Pen = Nothing)
    MyBase.New(Points, Clore:=True)
    Plume = unePlume
  End Sub

  Public ReadOnly Property Location() As PointF
    Get
      Return mPoint(0)
    End Get
  End Property

  Public ReadOnly Property Taille() As SizeF
    Get
      Return New SizeF(mPoint(2).X - mpoint(0).X, mPoint(2).Y - mpoint(0).Y)
    End Get
  End Property

  Public Shared Function NouvelleBoite(ByVal DemiLargeur As Short, Optional ByVal unePlume As Pen = Nothing) As Boite

    Dim pBoite(3) As Point
    pBoite(0) = New Point(-DemiLargeur, -DemiLargeur)
    pBoite(1) = New Point(DemiLargeur, -DemiLargeur)
    pBoite(2) = New Point(DemiLargeur, DemiLargeur)
    pBoite(3) = New Point(-DemiLargeur, DemiLargeur)
    Return New Boite(pBoite, unePlume)
  End Function

  Public Shared Function NouvelleBoite(ByVal DemiLargeur As Short, ByVal DemiHauteur As Short, Optional ByVal unePlume As Pen = Nothing) As Boite

    Dim pBoite(3) As Point
    pBoite(0) = New Point(-DemiLargeur, -DemiHauteur)
    pBoite(1) = New Point(DemiLargeur, -DemiHauteur)
    pBoite(2) = New Point(DemiLargeur, DemiHauteur)
    pBoite(3) = New Point(-DemiLargeur, DemiHauteur)
    Return New Boite(pBoite, unePlume)
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe Texte
'=====================================================================================================

Public Class Texte : Inherits Graphique
  Private mChaine As String
  Private mFonte As Font
  Private mEmplacement As PointF 'Point haut gauche du texte
  Private mEncombrement As RectangleF
  Private mFormat As Drawing.StringFormat = New StringFormat

  Public Sub New(ByVal s As String, ByVal Emplacement As Point, _
      Optional ByVal uneBrosse As Brush = Nothing, Optional ByVal uneFonte As Font = Nothing)

    mChaine = s
    mBrosse = uneBrosse
    If IsNothing(mFonte) Then
      Me.mFonte = FonteDéfaut()
    Else
      Me.mFonte = uneFonte
    End If
    mEmplacement = New PointF(Emplacement.X, Emplacement.Y)
    mFormat.Alignment = StringAlignment.Center
  End Sub

  Public Sub New(ByVal s As String, ByVal Encombrement As Boite, ByVal uneBrosse As Brush, _
                Optional ByVal uneFonte As Font = Nothing)
    mChaine = s
    mBrosse = uneBrosse
    If IsNothing(uneFonte) Then
      Me.mFonte = FonteDéfaut()
    Else
      Me.mFonte = uneFonte
    End If
    mEncombrement = New RectangleF(Encombrement.Location, Encombrement.Taille)
    mFormat.Alignment = StringAlignment.Center
    mFormat.LineAlignment = StringAlignment.Center
  End Sub

  Public Sub New(ByVal s As String, ByVal uneBrosse As Brush, ByVal uneFonte As Font, ByVal Location As Point, _
                  Optional ByVal unAlignement As StringAlignment = StringAlignment.Center)
    Dim TailleTexte As SizeF

    mChaine = s
    mBrosse = uneBrosse
    mFonte = uneFonte
    TailleTexte = cndGraphique.MeasureString(s, mFonte)

    mFormat.Alignment = unAlignement
    Select Case unAlignement
      Case StringAlignment.Far
        Location.X -= TailleTexte.Width
      Case StringAlignment.Center
        Location.X -= TailleTexte.Width / 2
    End Select

    mEncombrement = New RectangleF(CvPointF(Location), TailleTexte)

  End Sub

  Private Function FonteDéfaut() As Font

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Return New Font("Arial", 10)
    Else
      Return New Font("Arial", 8)
    End If
  End Function

  Public Overrides Sub Dessiner(ByVal g1 As System.Drawing.Graphics, Optional ByVal g2 As System.Drawing.Graphics = Nothing)

    Dim uneBrosse As SolidBrush = Brosse
    If Invisible OrElse IsNothing(uneBrosse) Then
      Exit Sub
    End If

    If mEmplacement.IsEmpty Then
      g1.DrawString(mChaine, mFonte, uneBrosse, mEncombrement, Format:=mFormat)
      If Not IsNothing(g2) Then g2.DrawString(mChaine, mFonte, uneBrosse, mEncombrement, Format:=mFormat)
    Else
      g1.DrawString(mChaine, mFonte, uneBrosse, mEmplacement, Format:=mFormat)
      If Not IsNothing(g2) Then g2.DrawString(mChaine, mFonte, uneBrosse, mEmplacement, Format:=mFormat)
    End If

  End Sub

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get

    End Get
  End Property

  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique

  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique

    If mEmplacement.IsEmpty Then
      Dim p As PointF = mEncombrement.Location

      p.X += pTrans.Width
      p.Y += pTrans.Height
      mEncombrement.Location = p

    Else
      mEmplacement.X += pTrans.Width
      mEmplacement.Y += pTrans.Height
    End If

    Return Me

  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim TrEmplacement As Point

    If IsNothing(ObjInsert) Then
      TrEmplacement = PointDessin(mEmplacement)
    Else
      TrEmplacement = PointDessin(ObjInsert.TransRot(mEmplacement))
    End If

    Dim NewTexte As New Texte(mChaine, TrEmplacement, uneFonte:=New Font("Arial", 8))
    NewTexte.Attributs = New AttributGraphique(RechCouleur(mCouleur, mCalque, ObjInsert), RechTypeLign(mTypelign, mCalque, ObjInsert), Alpha:=mAlpha)

    Return NewTexte
  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    If IsNothing(ObjInsert) Then
      Return Me
    End If
  End Function

  Public Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal R As Single = 0.0) As Boolean

  End Function
End Class

'=====================================================================================================
'--------------------------- Collection d'objets TEXTE
'=====================================================================================================
Public Class TexteCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As Texte
    Get
      Return CType(Me.List.Item(Index), Texte)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Insert
'=====================================================================================================
Public Class Insert : Inherits Graphique
  Private mBloc As Bloc
  Public pInsertion As PointF
  Private mEchx As Single = 1.0
  Private mEchy As Single = mEchx
  Public rot, distcol, distrow As Single
  Public nbcol, nbrow As Short

  'Public pMinFDP,pMaxFDP As PointF
  Public pMaxFDP As PointF

  Public Sub New()

  End Sub

  Public Sub New(ByVal unBloc As Bloc)

    mBloc = unBloc
    pInsertion = New PointF(0, 0)

  End Sub

  Public Sub New(ByVal unBloc As Bloc, ByVal unePlume As Pen)
    mBloc = unBloc
    pInsertion = New PointF(0, 0)
    Plume = unePlume
  End Sub

  'Public ReadOnly Property Centre() As PointF
  '  Get
  '    Return Milieu(pMinFDP, pMaxFDP)
  '  End Get
  'End Property
  Public Property Echx() As Single
    Get
      Return mEchx
    End Get
    Set(ByVal Value As Single)
      mEchx = Value
      mEchy = Value
    End Set
  End Property

  Public ReadOnly Property Echy() As Single
    Get
      Return mEchy
    End Get
  End Property

  Public Property Bloc() As Bloc
    Get
      Return mBloc
    End Get
    Set(ByVal Value As Bloc)
      mBloc = Value
    End Set
  End Property

  Public Function TransRot(ByVal p As PointF) As PointF
    Dim p0 As New PointF

    ' Facteur d'échelle
    p0.X = p.X * Echx
    p0.Y = p.Y * Echx
    ' Rotation
    If rot <> 0 Then
      p0 = Formules.Rotation(p0, CvAngleRadians(rot, InverserSens:=True))
    End If
    ' translation
    Return Formules.Translation(p0, pInsertion)

  End Function

  Public Overrides Sub Dessiner(ByVal g1 As System.Drawing.Graphics, Optional ByVal g2 As System.Drawing.Graphics = Nothing)

    With mBloc
      .Lignes.Dessiner(g1, g2)
      .Arcs.Dessiner(g1, g2)
      .Cercles.Dessiner(g1, g2)
      .PolyArcs.Dessiner(g1, g2)
      .Splines.Dessiner(g1, g2)
      .Textes.Dessiner(g1, g2)
      .Inserts.Dessiner(g1, g2)
    End With
  End Sub

  Public Overrides ReadOnly Property NbPoignées() As Short
    Get

    End Get
  End Property


  Public Overrides Function Rotation(ByVal Alpha As Single) As Graphique
    Dim unInsert As New Insert

    'Dim uneFigure As Graphique
    'For Each uneFigure In colObjets
    '  unInsert.Add(uneFigure.Rotation(Alpha))
    'Next

    Return unInsert

  End Function

  Public Overrides Function TranslationBase(ByVal pTrans As System.Drawing.SizeF) As Graphique
    Dim unInsert As New Insert

    'Dim uneFigure As Graphique
    'For Each uneFigure In colObjets
    '  unInsert.Add(uneFigure.Rotation(Alpha))
    'Next

    Return unInsert

  End Function

  Public Overrides Function PréparerDessin(Optional ByVal ObjInsert As Insert = Nothing) As Graphique
    Dim ImageDessin As New Bloc
    Dim NewInsert As Insert

    With mBloc
      If IsNothing(ObjInsert) OrElse TypeOf ObjInsert.mBloc Is SuperBloc Then
        NewInsert = Me
      Else
        ' entité insérée dans 1 bloc
        NewInsert = New Insert        ' Blocs imbriqués: cf Manuel utilisateur AutoCAD 12 ch.10 p 394...
        Dim unCalque As Calque, uneCouleur As Long, unTypeLign As String
        With NewInsert
          .pInsertion = ObjInsert.TransRot(pInsertion)
          .Echx = Echx * ObjInsert.Echx
          .rot = rot + ObjInsert.rot
          If mCalque.Nom = "0" Then          'l'insert étant dans le plan "0", on crée l'objet dans le plan du 'père'
            unCalque = ObjInsert.mCalque
          Else
            unCalque = mcalque
          End If
          .DéfinirAttributs(unCalque, RechCouleur(mCouleur, mCalque, ObjInsert), RechTypeLign(mTypeLign, mCalque, ObjInsert))
        End With
      End If

      Try
        Return New Insert(.PréparerDessin(NewInsert))

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Insert.PréparerDessin")
      End Try

    End With

  End Function

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique

    Dim ImageDessin As New Bloc
    Dim NewInsert As Insert

    With mBloc
      If IsNothing(ObjInsert) OrElse TypeOf ObjInsert.mBloc Is SuperBloc Then
        NewInsert = Me
      Else
        ' entité insérée dans 1 bloc
        NewInsert = New Insert        ' Blocs imbriqués: cf Manuel utilisateur AutoCAD 12 ch.10 p 394...
        Dim unCalque As Calque, uneCouleur As Long, unTypeLign As String
        With NewInsert
          .pInsertion = ObjInsert.TransRot(pInsertion)
          .Echx = Echx * ObjInsert.Echx
          .rot = rot + ObjInsert.rot
        End With
      End If

      Try
        Return New Insert(.TrInsertion(NewInsert))

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Insert.TrInsertion")
      End Try

    End With

  End Function

  Public Sub AttribuerPlume(ByVal unePlume As Pen)

    mBloc.AttribuerPlume(unePlume)

  End Sub

  Public Overrides Function ProcheDuPoint(ByVal pC As System.Drawing.Point, ByRef pIntéressant As System.Drawing.Point, Optional ByVal R As Single = 0.0) As Boolean

  End Function

  Public Overrides ReadOnly Property Longueur() As Single
    Get

    End Get
  End Property

  Public Overrides Sub Effacer(ByVal g1 As System.Drawing.Graphics, ByVal g2 As System.Drawing.Graphics)

    With mBloc
      .Lignes.Effacer(g1, g2)
      .Arcs.Effacer(g1, g2)
      .Cercles.Effacer(g1, g2)
      .PolyArcs.Effacer(g1, g2)
      .Splines.Effacer(g1, g2)
      .Textes.Effacer(g1, g2)
      .Inserts.Effacer(g1, g2)
    End With
  End Sub
End Class


'=====================================================================================================
'--------------------------- Collection d'objets INSERT
'=====================================================================================================
Public Class InsertCollection : Inherits Graphiques

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public Shadows ReadOnly Property Item(ByVal Index As Short) As Insert
    Get
      Return CType(Me.List.Item(Index), Insert)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Pt : Point de polyligne ou de spline  Autocad ----------------
'=====================================================================================================
Public Class Pt
  Public p As PointF
  Private mArrondi As Single
  Public Drapeau As Short

  Public Sub New(ByVal unPoint As PointF)
    With unPoint
      p.X = .X
      p.Y = .Y
    End With
  End Sub

  Public Property Arrondi() As Single
    Get
      Return mArrondi
    End Get
    Set(ByVal Value As Single)
      mArrondi = Value
    End Set
  End Property

End Class


'=====================================================================================================
'--------------------------- Collection d'objets Pt --------------------------
'=====================================================================================================
Public Class Pts : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unPoint As Pt) As Short
    Return Me.List.Add(unPoint)
  End Function

  Public Function Add(ByVal X1 As Single, ByVal Y1 As Single, Optional ByVal arrondi As Single = 0.0, Optional ByVal Drapeau As Short = 0) As Pt
    Dim unPoint As New Pt(New PointF(X1, Y1))

    With unPoint
      .Arrondi = arrondi
      .Drapeau = Drapeau
    End With

    Me.List.Add(unPoint)
    Add = unPoint

  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal Points() As Pt)
    Me.InnerList.AddRange(Points)
  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unPoint As Pt)
    Me.List.Insert(Index, unPoint)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Pt
    Get
      Return CType(Me.List.Item(Index), Pt)
    End Get
  End Property

End Class
'=====================================================================================================
'--------------------------- Classe Vecteur
'=====================================================================================================
Public Class Vecteur
  Public X, Y As Double

  Public Sub New(ByVal pA As Point, ByVal pB As Point)
    X = pB.X - pA.X
    Y = pB.Y - pA.Y
  End Sub

  Public Sub New(ByVal pA As PointF, ByVal pB As PointF)
    X = pB.X - pA.X
    Y = pB.Y - pA.Y
  End Sub

  Public Sub New(ByVal Longueur As Single, ByVal Angle As Single)
    X = Longueur * Cos(Angle)
    Y = Longueur * Sin(Angle)
  End Sub
  Public Sub New(ByVal uneLigne As Ligne)
    X = uneLigne.pB.X - uneLigne.pA.X
    Y = uneLigne.pB.Y - uneLigne.pA.Y
  End Sub
  Public Sub New(ByVal X As Integer, ByVal Y As Integer)
    Me.X = X
    Me.Y = Y
  End Sub
End Class

Public Structure ParamDessin
  Public Echelle As Single
  Public OrigineRéelle As PointF
  Public TailleFenêtre As Size
  Public ZoneGraphique As Rectangle

  Public Sub New(ByVal Echelle As Single, ByVal pOrigine As PointF)
    Me.Echelle = Echelle
    OrigineRéelle = pOrigine
  End Sub
  Public Sub New(ByVal uneEchelle As Single, ByVal pOrigine As PointF, ByVal unRectangle As Rectangle)
    Echelle = uneEchelle
    OrigineRéelle = pOrigine
    ZoneGraphique = unRectangle
  End Sub

  Public ReadOnly Property IsEmpty() As Boolean
    Get
      Return OrigineRéelle.IsEmpty
    End Get
  End Property
End Structure

'=====================================================================================================
'--------------------------- Classe Flêche
'=====================================================================================================
Public Class Fleche : Inherits PolyArc
  Private mHauteurFlèche As Single

  Public Sub New()

  End Sub

  Public Sub New(ByVal Longueur As Single, ByVal HauteurFlèche As Single, Optional ByVal Delta As Single = 0.0, Optional ByVal unePlume As Pen = Nothing, Optional ByVal FlecheDouble As Boolean = False, Optional ByVal SegmentCentral As Boolean = True)
    MyBase.New()

    mHauteurFlèche = HauteurFlèche

    ' Définir un objet flèche, basé sur le segment P1P2 dans le repère de la demi-droite P1P2 d'origine P1
    ' P1P2 est prolongé à gauche et à droite d'une longueur Delta
    Dim p(2) As PointF

    ' Flèche de gauche
    Dim pA As PointF = New PointF(-Delta, 0)
    ' Sommet de la flèche
    p(0) = Formules.TranslationBase(pA, New Size(-HauteurFlèche, 0))
    ' Extrémités de la flèche
    ' Par défaut, l'angle au centre de la flèche est de 60 degrés, soit 30 de chaque coté et sin(pi/6)=1/2
    p(1) = Formules.TranslationBase(pA, New SizeF(0, HauteurFlèche / 2))
    p(2) = Formules.TranslationBase(pA, New SizeF(0, -HauteurFlèche / 2))
    Me.Add(New Ligne(p(0), p(1), unePlume))
    Me.Add(New Ligne(p(0), p(2), unePlume))

    Dim pB As PointF
    If FlecheDouble Then
      ' Flèche de droite
      pB = New PointF(Longueur + Delta, 0)
      ' Sommet de la flèche
      p(0) = Formules.TranslationBase(pB, New Size(HauteurFlèche, 0))
      ' Extrémités de la flèche
      p(1) = Formules.TranslationBase(pB, New Size(0, HauteurFlèche / 2))
      p(2) = Formules.TranslationBase(pB, New Size(0, -HauteurFlèche / 2))
      Me.Add(New Ligne(p(0), p(1), unePlume))
      Me.Add(New Ligne(p(0), p(2), unePlume))

    Else
      ' Dans le cas d'une demi-flèche, on fournit la longueur de la demi-flèche
      pB = New PointF(Longueur, 0)
    End If

    'Ajouter le segment central de la flèche
    Me.Add(New Ligne(pA, pB, unePlume:=IIf(SegmentCentral, unePlume, Nothing)))

  End Sub

  Public Sub New(ByVal desLignes As Graphique())
    Dim uneLigne As Ligne

    Figures.AddRange(desLignes)

  End Sub

  Public ReadOnly Property HauteurFlèche() As Single
    Get
      Return mHauteurFlèche
    End Get
  End Property
  Public ReadOnly Property ptRéférence(ByVal Index As Short) As PointF
    Get
      If Index = 0 Then
        Return LigneRéférence.pAF
      Else
        Return LigneRéférence.pBF
      End If
    End Get
  End Property
  Public ReadOnly Property Angle() As Single
    Get
      Return AngleFormé(LigneRéférence)
    End Get
  End Property
  Public ReadOnly Property LigneRéférence() As Ligne
    Get
      Return CType(Item(Count - 1), Ligne)
    End Get
  End Property

  Public Overrides Function TrInsertion(ByVal ObjInsert As Insert) As Graphique
    If IsNothing(ObjInsert) Then
      Return Me
    Else
      Return New Fleche(Me.Figures.TrInsertion(ObjInsert))
    End If
  End Function
End Class