'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgFDP.vb										  											'
'						Classes																														'
'							dlgFDP : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgFDP --------------------------
'Dialogue pour choisir un fond de plan 
'et préciser certaines caractéristiques (echelle, rotation, calques DXF à retenir...)
'=====================================================================================================
Public Class dlgFDP
  Inherits DiagFeux.frmDlg

#Region " Code généré par le Concepteur Windows Form "

  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()

    'Ajoutez une initialisation quelconque après l'appel InitializeComponent()

  End Sub

  'La méthode substituée Dispose du formulaire pour nettoyer la liste des composants.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Requis par le Concepteur Windows Form
  Private components As System.ComponentModel.IContainer

  'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
  'Elle peut être modifiée en utilisant le Concepteur Windows Form.  
  'Ne la modifiez pas en utilisant l'éditeur de code.
  Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
  Friend WithEvents radDXF As System.Windows.Forms.RadioButton
  Friend WithEvents btnParcourir As System.Windows.Forms.Button
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents grpFDP As System.Windows.Forms.GroupBox
  Friend WithEvents grpTaille As System.Windows.Forms.GroupBox
  Friend WithEvents txtRotation As System.Windows.Forms.TextBox
  Friend WithEvents txtPixel As System.Windows.Forms.TextBox
  Friend WithEvents lblDossier As System.Windows.Forms.Label
  Friend WithEvents grpCalques As System.Windows.Forms.GroupBox
  Friend WithEvents lvwCalques As System.Windows.Forms.ListView
  Friend WithEvents lblNomFichier As System.Windows.Forms.Label
  Friend WithEvents lblNom As System.Windows.Forms.Label
  Friend WithEvents lblNomDossier As System.Windows.Forms.Label
  Friend WithEvents lblDegrés As System.Windows.Forms.Label
  Friend WithEvents lblRotation As System.Windows.Forms.Label
  Friend WithEvents lblMètres As System.Windows.Forms.Label
  Friend WithEvents lbl1Pixel As System.Windows.Forms.Label
  Friend WithEvents lblValHauteur As System.Windows.Forms.Label
  Friend WithEvents lblHauteur As System.Windows.Forms.Label
  Friend WithEvents lblLargeur As System.Windows.Forms.Label
  Friend WithEvents lblTailleImage As System.Windows.Forms.Label
  Friend WithEvents lblValLargeur As System.Windows.Forms.Label
  Friend WithEvents lblX As System.Windows.Forms.Label
  Friend WithEvents txtX As System.Windows.Forms.TextBox
  Friend WithEvents txtY As System.Windows.Forms.TextBox
  Friend WithEvents lblY As System.Windows.Forms.Label
  Friend WithEvents lblCentre As System.Windows.Forms.Label
  Friend WithEvents radRaster As System.Windows.Forms.RadioButton
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.grpFDP = New System.Windows.Forms.GroupBox
    Me.radRaster = New System.Windows.Forms.RadioButton
    Me.lblCentre = New System.Windows.Forms.Label
    Me.txtY = New System.Windows.Forms.TextBox
    Me.lblY = New System.Windows.Forms.Label
    Me.txtX = New System.Windows.Forms.TextBox
    Me.lblX = New System.Windows.Forms.Label
    Me.lblNomFichier = New System.Windows.Forms.Label
    Me.lblNomDossier = New System.Windows.Forms.Label
    Me.lblDossier = New System.Windows.Forms.Label
    Me.lblNom = New System.Windows.Forms.Label
    Me.btnParcourir = New System.Windows.Forms.Button
    Me.radDXF = New System.Windows.Forms.RadioButton
    Me.grpCalques = New System.Windows.Forms.GroupBox
    Me.lvwCalques = New System.Windows.Forms.ListView
    Me.grpTaille = New System.Windows.Forms.GroupBox
    Me.lblValLargeur = New System.Windows.Forms.Label
    Me.lblDegrés = New System.Windows.Forms.Label
    Me.txtRotation = New System.Windows.Forms.TextBox
    Me.lblRotation = New System.Windows.Forms.Label
    Me.lblMètres = New System.Windows.Forms.Label
    Me.txtPixel = New System.Windows.Forms.TextBox
    Me.lbl1Pixel = New System.Windows.Forms.Label
    Me.lblValHauteur = New System.Windows.Forms.Label
    Me.lblHauteur = New System.Windows.Forms.Label
    Me.lblLargeur = New System.Windows.Forms.Label
    Me.lblTailleImage = New System.Windows.Forms.Label
    Me.grpFDP.SuspendLayout()
    Me.grpCalques.SuspendLayout()
    Me.grpTaille.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(352, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(352, 16)
    Me.btnOK.Name = "btnOK"
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(352, 96)
    Me.btnAide.Name = "btnAide"
    '
    'grpFDP
    '
    Me.grpFDP.Controls.Add(Me.radRaster)
    Me.grpFDP.Controls.Add(Me.lblCentre)
    Me.grpFDP.Controls.Add(Me.txtY)
    Me.grpFDP.Controls.Add(Me.lblY)
    Me.grpFDP.Controls.Add(Me.txtX)
    Me.grpFDP.Controls.Add(Me.lblX)
    Me.grpFDP.Controls.Add(Me.lblNomFichier)
    Me.grpFDP.Controls.Add(Me.lblNomDossier)
    Me.grpFDP.Controls.Add(Me.lblDossier)
    Me.grpFDP.Controls.Add(Me.lblNom)
    Me.grpFDP.Controls.Add(Me.btnParcourir)
    Me.grpFDP.Controls.Add(Me.radDXF)
    Me.grpFDP.Location = New System.Drawing.Point(8, 8)
    Me.grpFDP.Name = "grpFDP"
    Me.grpFDP.Size = New System.Drawing.Size(336, 136)
    Me.grpFDP.TabIndex = 11
    Me.grpFDP.TabStop = False
    Me.grpFDP.Text = "Fond de plan"
    '
    'radRaster
    '
    Me.radRaster.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.radRaster.Checked = True
    Me.radRaster.Location = New System.Drawing.Point(24, 80)
    Me.radRaster.Name = "radRaster"
    Me.radRaster.Size = New System.Drawing.Size(96, 24)
    Me.radRaster.TabIndex = 15
    Me.radRaster.TabStop = True
    Me.radRaster.Text = "Image Raster"
    '
    'lblCentre
    '
    Me.lblCentre.Location = New System.Drawing.Point(160, 80)
    Me.lblCentre.Name = "lblCentre"
    Me.lblCentre.Size = New System.Drawing.Size(144, 16)
    Me.lblCentre.TabIndex = 14
    Me.lblCentre.Text = "Centre du carrefour"
    '
    'txtY
    '
    Me.txtY.Enabled = False
    Me.txtY.Location = New System.Drawing.Point(248, 104)
    Me.txtY.Name = "txtY"
    Me.txtY.Size = New System.Drawing.Size(56, 20)
    Me.txtY.TabIndex = 13
    Me.txtY.Text = ""
    '
    'lblY
    '
    Me.lblY.Location = New System.Drawing.Point(224, 104)
    Me.lblY.Name = "lblY"
    Me.lblY.Size = New System.Drawing.Size(24, 16)
    Me.lblY.TabIndex = 12
    Me.lblY.Text = "Y ="
    '
    'txtX
    '
    Me.txtX.Enabled = False
    Me.txtX.Location = New System.Drawing.Point(152, 104)
    Me.txtX.Name = "txtX"
    Me.txtX.Size = New System.Drawing.Size(56, 20)
    Me.txtX.TabIndex = 11
    Me.txtX.Text = ""
    '
    'lblX
    '
    Me.lblX.Location = New System.Drawing.Point(128, 108)
    Me.lblX.Name = "lblX"
    Me.lblX.Size = New System.Drawing.Size(24, 16)
    Me.lblX.TabIndex = 10
    Me.lblX.Text = "X ="
    '
    'lblNomFichier
    '
    Me.lblNomFichier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblNomFichier.Location = New System.Drawing.Point(56, 24)
    Me.lblNomFichier.Name = "lblNomFichier"
    Me.lblNomFichier.Size = New System.Drawing.Size(144, 16)
    Me.lblNomFichier.TabIndex = 9
    '
    'lblNomDossier
    '
    Me.lblNomDossier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblNomDossier.Location = New System.Drawing.Point(56, 56)
    Me.lblNomDossier.Name = "lblNomDossier"
    Me.lblNomDossier.Size = New System.Drawing.Size(272, 16)
    Me.lblNomDossier.TabIndex = 8
    '
    'lblDossier
    '
    Me.lblDossier.Location = New System.Drawing.Point(8, 56)
    Me.lblDossier.Name = "lblDossier"
    Me.lblDossier.Size = New System.Drawing.Size(56, 16)
    Me.lblDossier.TabIndex = 7
    Me.lblDossier.Text = "Dossier :"
    '
    'lblNom
    '
    Me.lblNom.Location = New System.Drawing.Point(8, 24)
    Me.lblNom.Name = "lblNom"
    Me.lblNom.Size = New System.Drawing.Size(40, 16)
    Me.lblNom.TabIndex = 6
    Me.lblNom.Text = "Nom :"
    '
    'btnParcourir
    '
    Me.btnParcourir.Location = New System.Drawing.Point(224, 24)
    Me.btnParcourir.Name = "btnParcourir"
    Me.btnParcourir.Size = New System.Drawing.Size(96, 24)
    Me.btnParcourir.TabIndex = 4
    Me.btnParcourir.Text = "Parcourir..."
    '
    'radDXF
    '
    Me.radDXF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.radDXF.Location = New System.Drawing.Point(24, 104)
    Me.radDXF.Name = "radDXF"
    Me.radDXF.TabIndex = 1
    Me.radDXF.Text = "Fichier DXF"
    '
    'grpCalques
    '
    Me.grpCalques.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpCalques.Controls.Add(Me.lvwCalques)
    Me.grpCalques.Location = New System.Drawing.Point(8, 152)
    Me.grpCalques.Name = "grpCalques"
    Me.grpCalques.Size = New System.Drawing.Size(336, 104)
    Me.grpCalques.TabIndex = 12
    Me.grpCalques.TabStop = False
    Me.grpCalques.Text = "Calques visibles"
    '
    'lvwCalques
    '
    Me.lvwCalques.CheckBoxes = True
    Me.lvwCalques.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwCalques.Location = New System.Drawing.Point(8, 24)
    Me.lvwCalques.MultiSelect = False
    Me.lvwCalques.Name = "lvwCalques"
    Me.lvwCalques.Size = New System.Drawing.Size(312, 72)
    Me.lvwCalques.TabIndex = 0
    Me.lvwCalques.View = System.Windows.Forms.View.List
    '
    'grpTaille
    '
    Me.grpTaille.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpTaille.Controls.Add(Me.lblValLargeur)
    Me.grpTaille.Controls.Add(Me.lblDegrés)
    Me.grpTaille.Controls.Add(Me.txtRotation)
    Me.grpTaille.Controls.Add(Me.lblRotation)
    Me.grpTaille.Controls.Add(Me.lblMètres)
    Me.grpTaille.Controls.Add(Me.txtPixel)
    Me.grpTaille.Controls.Add(Me.lbl1Pixel)
    Me.grpTaille.Controls.Add(Me.lblValHauteur)
    Me.grpTaille.Controls.Add(Me.lblHauteur)
    Me.grpTaille.Controls.Add(Me.lblLargeur)
    Me.grpTaille.Controls.Add(Me.lblTailleImage)
    Me.grpTaille.Location = New System.Drawing.Point(8, 152)
    Me.grpTaille.Name = "grpTaille"
    Me.grpTaille.Size = New System.Drawing.Size(336, 104)
    Me.grpTaille.TabIndex = 14
    Me.grpTaille.TabStop = False
    Me.grpTaille.Text = "Taille de l'image"
    '
    'lblValLargeur
    '
    Me.lblValLargeur.Location = New System.Drawing.Point(72, 48)
    Me.lblValLargeur.Name = "lblValLargeur"
    Me.lblValLargeur.Size = New System.Drawing.Size(56, 16)
    Me.lblValLargeur.TabIndex = 12
    Me.lblValLargeur.Text = "734"
    '
    'lblDegrés
    '
    Me.lblDegrés.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblDegrés.Location = New System.Drawing.Point(272, 72)
    Me.lblDegrés.Name = "lblDegrés"
    Me.lblDegrés.Size = New System.Drawing.Size(32, 16)
    Me.lblDegrés.TabIndex = 11
    Me.lblDegrés.Text = "°"
    '
    'txtRotation
    '
    Me.txtRotation.Location = New System.Drawing.Point(224, 72)
    Me.txtRotation.Name = "txtRotation"
    Me.txtRotation.Size = New System.Drawing.Size(36, 20)
    Me.txtRotation.TabIndex = 10
    Me.txtRotation.Text = "0"
    Me.txtRotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblRotation
    '
    Me.lblRotation.Location = New System.Drawing.Point(168, 72)
    Me.lblRotation.Name = "lblRotation"
    Me.lblRotation.Size = New System.Drawing.Size(48, 16)
    Me.lblRotation.TabIndex = 9
    Me.lblRotation.Text = "Rotation ="
    '
    'lblMètres
    '
    Me.lblMètres.Location = New System.Drawing.Point(272, 48)
    Me.lblMètres.Name = "lblMètres"
    Me.lblMètres.Size = New System.Drawing.Size(32, 16)
    Me.lblMètres.TabIndex = 8
    Me.lblMètres.Text = "m"
    '
    'txtPixel
    '
    Me.txtPixel.Location = New System.Drawing.Point(224, 48)
    Me.txtPixel.MaxLength = 5
    Me.txtPixel.Name = "txtPixel"
    Me.txtPixel.Size = New System.Drawing.Size(36, 20)
    Me.txtPixel.TabIndex = 7
    Me.txtPixel.Text = ""
    Me.txtPixel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lbl1Pixel
    '
    Me.lbl1Pixel.Location = New System.Drawing.Point(168, 48)
    Me.lbl1Pixel.Name = "lbl1Pixel"
    Me.lbl1Pixel.Size = New System.Drawing.Size(48, 16)
    Me.lbl1Pixel.TabIndex = 6
    Me.lbl1Pixel.Text = "1 pixel ="
    '
    'lblValHauteur
    '
    Me.lblValHauteur.Location = New System.Drawing.Point(72, 64)
    Me.lblValHauteur.Name = "lblValHauteur"
    Me.lblValHauteur.Size = New System.Drawing.Size(56, 16)
    Me.lblValHauteur.TabIndex = 4
    Me.lblValHauteur.Text = "734"
    '
    'lblHauteur
    '
    Me.lblHauteur.Location = New System.Drawing.Point(24, 64)
    Me.lblHauteur.Name = "lblHauteur"
    Me.lblHauteur.Size = New System.Drawing.Size(48, 16)
    Me.lblHauteur.TabIndex = 2
    Me.lblHauteur.Text = "Hauteur:"
    '
    'lblLargeur
    '
    Me.lblLargeur.Location = New System.Drawing.Point(24, 48)
    Me.lblLargeur.Name = "lblLargeur"
    Me.lblLargeur.Size = New System.Drawing.Size(48, 16)
    Me.lblLargeur.TabIndex = 1
    Me.lblLargeur.Text = "Largeur:"
    '
    'lblTailleImage
    '
    Me.lblTailleImage.Location = New System.Drawing.Point(16, 24)
    Me.lblTailleImage.Name = "lblTailleImage"
    Me.lblTailleImage.Size = New System.Drawing.Size(136, 16)
    Me.lblTailleImage.TabIndex = 0
    Me.lblTailleImage.Text = "Taille de l'image en pixels"
    '
    'dlgFDP
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(440, 261)
    Me.Controls.Add(Me.grpFDP)
    Me.Controls.Add(Me.grpTaille)
    Me.Controls.Add(Me.grpCalques)
    Me.Name = "dlgFDP"
    Me.Text = "Fond de plan"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.grpCalques, 0)
    Me.Controls.SetChildIndex(Me.grpTaille, 0)
    Me.Controls.SetChildIndex(Me.grpFDP, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.grpFDP.ResumeLayout(False)
    Me.grpCalques.ResumeLayout(False)
    Me.grpTaille.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Friend PourRecalage As Boolean
  Public mFondDePlan As FondDePlan
  Private mParamDessin As ParamDessin

  Private Sub dlgFDP_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_NOUVEAU
  End Sub

  Private Sub dlgFDP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dlg As dlgCarGen = Me.Owner

    If PourRecalage Then
      Me.Height = Me.grpFDP.Top + Me.grpTaille.Height + 50
      Me.grpTaille.Visible = True
      Me.grpFDP.Visible = False
    End If

    If mFondDePlan Is Nothing Then
      Me.radRaster.Checked = True

    Else
      If mFondDePlan.EstDXF Then
        Me.radDXF.Checked = True
        AfficherCalques()
        AfficherCentreCarrefour(dlg.Centre)
      Else
        Me.radRaster.Checked = True
        AfficherQualitéImage()
      End If
    End If

  End Sub

  Public ReadOnly Property ParamDessin() As ParamDessin
    Get
      Return mParamDessin
    End Get
  End Property

  Private Sub rad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radDXF.CheckedChanged, radRaster.CheckedChanged

    If sender Is radDXF Then
      Me.grpCalques.Visible = True
      Me.grpTaille.Visible = False
      Me.txtX.Enabled = True
      Me.txtY.Enabled = True
      Me.lblCentre.Enabled = True
    ElseIf sender Is radRaster Then
      Me.grpCalques.Visible = False
      Me.grpTaille.Visible = True
      Me.txtX.Enabled = False
      Me.txtY.Enabled = False
      Me.lblCentre.Enabled = False
    End If

  End Sub

  '**********************************************************************************************
  ' Parcourir le réseau pour trouver le fichier fond de plan à charger
  '**********************************************************************************************
  Private Sub btnParcourir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParcourir.Click
    Dim Extension As String
    Dim Filtre As String
    Dim DefaultExt As String

    If Me.radDXF.Checked Then
      Extension = "dxf"
      DefaultExt = Extension
      Filtre = ComposerFiltre(Extension)
    Else
      DefaultExt = "jpg"
      Filtre = ImageRaster.Filtre '"Fichiers image (*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG)|*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG"
    End If

    Dim NomFichier As String
    If IsNothing(mFondDePlan) Then
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt)
    Else
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt, InfoFichier:=mFondDePlan.InfoFichier)
    End If

    If Not IsNothing(NomFichier) Then
      'Fichier trouvé
      If radDXF.Checked Then
        'Fichier DXF
        Me.Visible = False
        Dim unDXF As DXF = LecDXF.lecFDP(NomFichier, Me)
        Me.Visible = True
        If Not IsNothing(unDXF) Then
          'Lecture réussie
          If Not IsNothing(mFondDePlan) AndAlso TypeOf mFondDePlan Is DXF Then
            Dim Avertissement As String
            Avertissement = "Attention, assurez-vous que le nouveau dessin utilise le même système de coordonnées"
            Avertissement &= vbCrLf & "Faites le si nécessaire coïncider avec le schéma du carrefour"
            MessageBox.Show(Me, Avertissement, "Changement de fond de plan", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
          End If
          mFondDePlan = unDXF
          AfficherCalques()
          InitEchelleFDP(unDXF)
        End If

      Else
        'Fichier image
        Dim unRaster As New ImageRaster(NomFichier)
        If Not IsNothing(unRaster) Then
          mFondDePlan = unRaster
          AfficherQualitéImage()
        End If
      End If

    End If

  End Sub

  Private Sub InitEchelleFDP(ByVal unDXF As DXF)
    Dim uneEchelle As Single
    Dim uneOrigine As PointF

    With unDXF
      If Not .EchelleCalculée Then
        xMaxPicture = 404
        yMaxPicture = 586
        uneEchelle = xMaxPicture / .Largeur * 0.95
        uneEchelle = Math.Min(uneEchelle, yMaxPicture / .Hauteur * 0.95)
        uneOrigine.X = .Centre.X - (xMaxPicture / 2 / uneEchelle)
        uneOrigine.Y = .Centre.Y + (yMaxPicture / 2 / uneEchelle)
        AfficherCentreCarrefour(.Centre)

        mParamDessin = New ParamDessin(uneEchelle, uneOrigine)
        .EchelleCalculée = True
      End If
    End With

  End Sub

  Private Sub InitEchelleRaster(ByVal unRaster As ImageRaster)
    Dim uneEchelle As Single
    Dim uneOrigine As PointF

    With unRaster
      If Not .EchelleCalculée Then
        xMaxPicture = ImageRaster.LargeurImageBase ' base prise à l'initialisation de l'image raster pour qu"elle s'affiche dans la fenêtre
        yMaxPicture = xMaxPicture
        uneEchelle = cndParamDessin.Echelle
        uneOrigine.X = .Centre.X - (xMaxPicture / 2 / uneEchelle)
        uneOrigine.Y = .Centre.Y + (yMaxPicture / 2 / uneEchelle)

        mParamDessin = New ParamDessin(uneEchelle, uneOrigine)
        .EchelleCalculée = True
      End If
    End With

  End Sub

  Private Sub AfficherCentreCarrefour(ByVal Centre As PointF)
    With Centre
      Me.txtX.Text = Format(.X, "0.00")
      Me.txtY.Text = Format(.Y, "0.00")
    End With
  End Sub

  Private Sub AfficherCalques()
    Dim unCalque As Calque
    Dim unItem As ListViewItem

    AfficherNomFichier()

    With lvwCalques
      .BeginUpdate()
      .Items.Clear()
      For Each unCalque In CType(mFondDePlan, DXF).Calques
        unItem = .Items.Add(unCalque.Nom)
        unItem.Checked = unCalque.Visible
      Next
      .EndUpdate()
    End With
  End Sub

  Private Sub AfficherNomFichier()
    With mFondDePlan.InfoFichier
      lblNomFichier.Text = .Name
      lblNomDossier.Text = .DirectoryName
    End With
  End Sub

  Private Sub AfficherQualitéImage()

    AfficherNomFichier()

    With CType(mFondDePlan, ImageRaster)
      With .Taille()
        Me.lblValHauteur.Text = .Height
        Me.lblValLargeur.Text = .Width
      End With
      Me.txtPixel.Text = Format(.MètresParPixel, "0.###")
      Me.txtRotation.Text = Format(.Rotation, "0")
    End With

  End Sub

  Private Sub dlgFDP_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

    If Me.DialogResult = DialogResult.OK Then
      If IsNothing(mFondDePlan) Then
        MessageBox.Show("Définir le nom du fond de plan")
        e.Cancel = True

      ElseIf mFondDePlan.EstDXF Then
        Dim unCalque As Calque
        Dim unItem As ListViewItem
        Dim i As Short
        Dim unDXF As DXF = mFondDePlan
        With lvwCalques
          For Each unCalque In unDXF.Calques
            unItem = .Items(i)
            If unCalque.Visible <> unItem.Checked Then mFondDePlan.ADessiner = True
            unCalque.Visible = unItem.Checked
            i += 1
          Next
        End With

      Else
        InitEchelleRaster(CType(mFondDePlan, ImageRaster))

      End If

    End If

  End Sub

  Private Sub txtX_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtX.KeyPress, txtY.KeyPress
    Dim txt As TextBox

    txt = sender
    e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=False)

  End Sub

  Private Sub txtX_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtX.Validating, txtY.Validating
    Dim txt As TextBox = sender

    If Not IsNumeric(txt.Text) Then
      MsgBox("Saisie incorrecte")
      e.Cancel = True
    End If

  End Sub

  Private Sub txtPixel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPixel.KeyPress
    Dim txt As TextBox

    txt = sender
    e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=False)
    If Not e.Handled AndAlso e.KeyChar <> CType(vbBack, Char) AndAlso txt.SelectionLength = 0 Then
      e.Handled = DécimalesDépassées(txt.Text & "a", 3)

    End If

  End Sub

  Private Sub txtPixel_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPixel.Validating
    Dim unRaster As ImageRaster = CType(mFondDePlan, ImageRaster)

    If Not IsNothing(unRaster) AndAlso txtPixel.Text.Length > 0 Then
      e.Cancel = ControlerBornes(Me, 0.01, 1000, txtPixel, Donnée:=unRaster.MètresParPixel, unFormat:="0.00")

      If Not e.Cancel Then
        unRaster.MètresParPixel = CType(Me.txtPixel.Text, Single)
        unRaster.EchelleCalculée = False
      End If
    End If

  End Sub

  Private Sub txtRotation_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRotation.KeyPress
    e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=True, Négatif:=True)
  End Sub

  Private Sub txtRotation_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRotation.Validating
    Dim unRaster As ImageRaster = CType(mFondDePlan, ImageRaster)

    If Not IsNothing(unRaster) AndAlso txtRotation.Text.Length > 0 Then
      e.Cancel = ControlerBornes(Me, -360, 360, txtRotation, Donnée:=unRaster.Rotation, unFormat:="0")

      If Not e.Cancel Then
        unRaster.Rotation = CType(Me.txtRotation.Text, Short)
      End If
    End If
  End Sub

End Class
