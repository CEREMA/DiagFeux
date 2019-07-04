'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgCarGen.vb																							'
'						Saisie des carctéristiques du carrefour 													'
'																																							'
'						Classes																														'
'							dlgCarGen																												'
'******************************************************************************

'--------------------------- Classe dlgCarGen --------------------------


Friend Class dlgCarGen
  Inherits DiagFeux.frmDlg

#Region " Code généré par le Concepteur Windows Form "

	Friend Sub New()
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
	Friend WithEvents btnParam As System.Windows.Forms.Button
	Friend WithEvents grpIdentification As System.Windows.Forms.GroupBox
	Friend WithEvents cboCarrefourType As System.Windows.Forms.ComboBox
	Friend WithEvents txtNbBranches As System.Windows.Forms.TextBox
	Friend WithEvents txtCommentaires As System.Windows.Forms.TextBox
	Friend WithEvents lblCommentaires As System.Windows.Forms.Label
	Friend WithEvents txtRégulation As System.Windows.Forms.TextBox
	Friend WithEvents lblRégulation As System.Windows.Forms.Label
	Friend WithEvents txtCommune As System.Windows.Forms.TextBox
	Friend WithEvents lblCommune As System.Windows.Forms.Label
	Friend WithEvents txtNom As System.Windows.Forms.TextBox
	Friend WithEvents lblNom As System.Windows.Forms.Label
	Friend WithEvents txtDateControleur As System.Windows.Forms.TextBox
	Friend WithEvents lblDateControleur As System.Windows.Forms.Label
	Friend WithEvents txtTypeControleur As System.Windows.Forms.TextBox
	Friend WithEvents lblTypeControleur As System.Windows.Forms.Label
	Friend WithEvents lblNbBranches As System.Windows.Forms.Label
	Friend WithEvents grpModalités As System.Windows.Forms.GroupBox
	Friend WithEvents radDégradé As System.Windows.Forms.RadioButton
	Friend WithEvents radGraphique As System.Windows.Forms.RadioButton
  Friend WithEvents pnlMode As System.Windows.Forms.Panel
	Friend WithEvents grpControleur As System.Windows.Forms.GroupBox
	Friend WithEvents btnFDP As System.Windows.Forms.Button
  Friend WithEvents lblSituation As System.Windows.Forms.Label
	Friend WithEvents radHorsAgglo As System.Windows.Forms.RadioButton
	Friend WithEvents radEnAgglo As System.Windows.Forms.RadioButton
	Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblTypeCarrefour As System.Windows.Forms.Label
  Friend WithEvents chkFDP As System.Windows.Forms.CheckBox
  Friend WithEvents lblFormatDate As System.Windows.Forms.Label
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.grpIdentification = New System.Windows.Forms.GroupBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.lblSituation = New System.Windows.Forms.Label
    Me.radEnAgglo = New System.Windows.Forms.RadioButton
    Me.txtCommentaires = New System.Windows.Forms.TextBox
    Me.lblCommentaires = New System.Windows.Forms.Label
    Me.txtRégulation = New System.Windows.Forms.TextBox
    Me.lblRégulation = New System.Windows.Forms.Label
    Me.txtCommune = New System.Windows.Forms.TextBox
    Me.txtNom = New System.Windows.Forms.TextBox
    Me.lblNom = New System.Windows.Forms.Label
    Me.radHorsAgglo = New System.Windows.Forms.RadioButton
    Me.grpControleur = New System.Windows.Forms.GroupBox
    Me.lblFormatDate = New System.Windows.Forms.Label
    Me.txtDateControleur = New System.Windows.Forms.TextBox
    Me.lblDateControleur = New System.Windows.Forms.Label
    Me.txtTypeControleur = New System.Windows.Forms.TextBox
    Me.btnParam = New System.Windows.Forms.Button
    Me.grpModalités = New System.Windows.Forms.GroupBox
    Me.chkFDP = New System.Windows.Forms.CheckBox
    Me.lblTypeCarrefour = New System.Windows.Forms.Label
    Me.pnlMode = New System.Windows.Forms.Panel
    Me.radDégradé = New System.Windows.Forms.RadioButton
    Me.radGraphique = New System.Windows.Forms.RadioButton
    Me.cboCarrefourType = New System.Windows.Forms.ComboBox
    Me.txtNbBranches = New System.Windows.Forms.TextBox
    Me.lblNbBranches = New System.Windows.Forms.Label
    Me.btnFDP = New System.Windows.Forms.Button
    Me.tipBulle = New System.Windows.Forms.ToolTip(Me.components)
    Me.grpIdentification.SuspendLayout()
    Me.grpControleur.SuspendLayout()
    Me.grpModalités.SuspendLayout()
    Me.pnlMode.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(664, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.Size = New System.Drawing.Size(88, 24)
    Me.btnAnnuler.TabIndex = 1
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(664, 176)
    Me.btnAide.Name = "btnAide"
    Me.btnAide.Size = New System.Drawing.Size(88, 24)
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(664, 16)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(88, 24)
    Me.btnOK.TabIndex = 0
    '
    'grpIdentification
    '
    Me.grpIdentification.Controls.Add(Me.Label1)
    Me.grpIdentification.Controls.Add(Me.lblSituation)
    Me.grpIdentification.Controls.Add(Me.radEnAgglo)
    Me.grpIdentification.Controls.Add(Me.txtCommentaires)
    Me.grpIdentification.Controls.Add(Me.lblCommentaires)
    Me.grpIdentification.Controls.Add(Me.txtRégulation)
    Me.grpIdentification.Controls.Add(Me.lblRégulation)
    Me.grpIdentification.Controls.Add(Me.txtCommune)
    Me.grpIdentification.Controls.Add(Me.txtNom)
    Me.grpIdentification.Controls.Add(Me.lblNom)
    Me.grpIdentification.Controls.Add(Me.radHorsAgglo)
    Me.grpIdentification.Location = New System.Drawing.Point(8, 8)
    Me.grpIdentification.Name = "grpIdentification"
    Me.grpIdentification.Size = New System.Drawing.Size(392, 208)
    Me.grpIdentification.TabIndex = 0
    Me.grpIdentification.TabStop = False
    Me.grpIdentification.Text = "Identification"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(8, 56)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(64, 16)
    Me.Label1.TabIndex = 30
    Me.Label1.Text = "Commune :"
    '
    'lblSituation
    '
    Me.lblSituation.Location = New System.Drawing.Point(8, 88)
    Me.lblSituation.Name = "lblSituation"
    Me.lblSituation.Size = New System.Drawing.Size(56, 24)
    Me.lblSituation.TabIndex = 29
    Me.lblSituation.Text = "Situation :"
    '
    'radEnAgglo
    '
    Me.radEnAgglo.Location = New System.Drawing.Point(112, 88)
    Me.radEnAgglo.Name = "radEnAgglo"
    Me.radEnAgglo.Size = New System.Drawing.Size(128, 24)
    Me.radEnAgglo.TabIndex = 2
    Me.radEnAgglo.Text = "En agglomération"
    '
    'txtCommentaires
    '
    Me.txtCommentaires.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.txtCommentaires.Location = New System.Drawing.Point(112, 152)
    Me.txtCommentaires.Multiline = True
    Me.txtCommentaires.Name = "txtCommentaires"
    Me.txtCommentaires.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtCommentaires.Size = New System.Drawing.Size(256, 48)
    Me.txtCommentaires.TabIndex = 5
    Me.txtCommentaires.Text = ""
    '
    'lblCommentaires
    '
    Me.lblCommentaires.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblCommentaires.Location = New System.Drawing.Point(8, 152)
    Me.lblCommentaires.Name = "lblCommentaires"
    Me.lblCommentaires.Size = New System.Drawing.Size(88, 16)
    Me.lblCommentaires.TabIndex = 25
    Me.lblCommentaires.Text = "Commentaires :"
    '
    'txtRégulation
    '
    Me.txtRégulation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.txtRégulation.Location = New System.Drawing.Point(112, 120)
    Me.txtRégulation.MaxLength = 20
    Me.txtRégulation.Name = "txtRégulation"
    Me.txtRégulation.TabIndex = 4
    Me.txtRégulation.Text = ""
    '
    'lblRégulation
    '
    Me.lblRégulation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRégulation.Location = New System.Drawing.Point(8, 120)
    Me.lblRégulation.Name = "lblRégulation"
    Me.lblRégulation.Size = New System.Drawing.Size(112, 19)
    Me.lblRégulation.TabIndex = 17
    Me.lblRégulation.Text = "Zone de régulation :"
    '
    'txtCommune
    '
    Me.txtCommune.Location = New System.Drawing.Point(112, 56)
    Me.txtCommune.Name = "txtCommune"
    Me.txtCommune.Size = New System.Drawing.Size(264, 20)
    Me.txtCommune.TabIndex = 1
    Me.txtCommune.Text = "LYON"
    '
    'txtNom
    '
    Me.txtNom.Location = New System.Drawing.Point(112, 24)
    Me.txtNom.Name = "txtNom"
    Me.txtNom.Size = New System.Drawing.Size(264, 20)
    Me.txtNom.TabIndex = 0
    Me.txtNom.Text = "La Croix Rousse"
    '
    'lblNom
    '
    Me.lblNom.Location = New System.Drawing.Point(8, 24)
    Me.lblNom.Name = "lblNom"
    Me.lblNom.Size = New System.Drawing.Size(100, 19)
    Me.lblNom.TabIndex = 13
    Me.lblNom.Text = "Nom du carrefour :"
    '
    'radHorsAgglo
    '
    Me.radHorsAgglo.Location = New System.Drawing.Point(248, 88)
    Me.radHorsAgglo.Name = "radHorsAgglo"
    Me.radHorsAgglo.Size = New System.Drawing.Size(128, 24)
    Me.radHorsAgglo.TabIndex = 3
    Me.radHorsAgglo.Text = "Hors agglomération"
    '
    'grpControleur
    '
    Me.grpControleur.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpControleur.Controls.Add(Me.lblFormatDate)
    Me.grpControleur.Controls.Add(Me.txtDateControleur)
    Me.grpControleur.Controls.Add(Me.lblDateControleur)
    Me.grpControleur.Controls.Add(Me.txtTypeControleur)
    Me.grpControleur.Location = New System.Drawing.Point(8, 224)
    Me.grpControleur.Name = "grpControleur"
    Me.grpControleur.Size = New System.Drawing.Size(640, 80)
    Me.grpControleur.TabIndex = 1
    Me.grpControleur.TabStop = False
    Me.grpControleur.Text = "Controleur"
    '
    'lblFormatDate
    '
    Me.lblFormatDate.Location = New System.Drawing.Point(384, 48)
    Me.lblFormatDate.Name = "lblFormatDate"
    Me.lblFormatDate.Size = New System.Drawing.Size(72, 16)
    Me.lblFormatDate.TabIndex = 24
    Me.lblFormatDate.Text = "JJ/MM/AAAA"
    '
    'txtDateControleur
    '
    Me.txtDateControleur.Location = New System.Drawing.Point(384, 24)
    Me.txtDateControleur.MaxLength = 10
    Me.txtDateControleur.Name = "txtDateControleur"
    Me.txtDateControleur.Size = New System.Drawing.Size(72, 20)
    Me.txtDateControleur.TabIndex = 1
    Me.txtDateControleur.Text = "14/07/1789"
    Me.tipBulle.SetToolTip(Me.txtDateControleur, "Format : JJ/MM/AAAA")
    '
    'lblDateControleur
    '
    Me.lblDateControleur.Location = New System.Drawing.Point(280, 24)
    Me.lblDateControleur.Name = "lblDateControleur"
    Me.lblDateControleur.Size = New System.Drawing.Size(72, 24)
    Me.lblDateControleur.TabIndex = 1
    Me.lblDateControleur.Text = "Date de mise en service :"
    '
    'txtTypeControleur
    '
    Me.txtTypeControleur.Location = New System.Drawing.Point(112, 24)
    Me.txtTypeControleur.MaxLength = 20
    Me.txtTypeControleur.Name = "txtTypeControleur"
    Me.txtTypeControleur.TabIndex = 0
    Me.txtTypeControleur.Text = ""
    Me.tipBulle.SetToolTip(Me.txtTypeControleur, "Nom et Qualité")
    '
    'btnParam
    '
    Me.btnParam.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnParam.Location = New System.Drawing.Point(664, 96)
    Me.btnParam.Name = "btnParam"
    Me.btnParam.Size = New System.Drawing.Size(88, 24)
    Me.btnParam.TabIndex = 2
    Me.btnParam.Text = "Paramètres..."
    '
    'grpModalités
    '
    Me.grpModalités.Controls.Add(Me.chkFDP)
    Me.grpModalités.Controls.Add(Me.lblTypeCarrefour)
    Me.grpModalités.Controls.Add(Me.pnlMode)
    Me.grpModalités.Controls.Add(Me.cboCarrefourType)
    Me.grpModalités.Controls.Add(Me.txtNbBranches)
    Me.grpModalités.Controls.Add(Me.lblNbBranches)
    Me.grpModalités.Location = New System.Drawing.Point(416, 8)
    Me.grpModalités.Name = "grpModalités"
    Me.grpModalités.Size = New System.Drawing.Size(232, 208)
    Me.grpModalités.TabIndex = 2
    Me.grpModalités.TabStop = False
    Me.grpModalités.Text = "Mode d'utilisation"
    '
    'chkFDP
    '
    Me.chkFDP.Location = New System.Drawing.Point(16, 168)
    Me.chkFDP.Name = "chkFDP"
    Me.chkFDP.Size = New System.Drawing.Size(176, 24)
    Me.chkFDP.TabIndex = 27
    Me.chkFDP.Text = "Carrefour avec fond de plan"
    '
    'lblTypeCarrefour
    '
    Me.lblTypeCarrefour.Location = New System.Drawing.Point(8, 128)
    Me.lblTypeCarrefour.Name = "lblTypeCarrefour"
    Me.lblTypeCarrefour.Size = New System.Drawing.Size(104, 16)
    Me.lblTypeCarrefour.TabIndex = 26
    Me.lblTypeCarrefour.Text = "Type de carrefour :"
    '
    'pnlMode
    '
    Me.pnlMode.Controls.Add(Me.radDégradé)
    Me.pnlMode.Controls.Add(Me.radGraphique)
    Me.pnlMode.Location = New System.Drawing.Point(8, 24)
    Me.pnlMode.Name = "pnlMode"
    Me.pnlMode.Size = New System.Drawing.Size(216, 32)
    Me.pnlMode.TabIndex = 0
    '
    'radDégradé
    '
    Me.radDégradé.Location = New System.Drawing.Point(117, 8)
    Me.radDégradé.Name = "radDégradé"
    Me.radDégradé.Size = New System.Drawing.Size(95, 16)
    Me.radDégradé.TabIndex = 1
    Me.radDégradé.Text = "Mode tableur"
    '
    'radGraphique
    '
    Me.radGraphique.Checked = True
    Me.radGraphique.Location = New System.Drawing.Point(5, 8)
    Me.radGraphique.Name = "radGraphique"
    Me.radGraphique.Size = New System.Drawing.Size(110, 16)
    Me.radGraphique.TabIndex = 0
    Me.radGraphique.TabStop = True
    Me.radGraphique.Text = "Mode graphique"
    '
    'cboCarrefourType
    '
    Me.cboCarrefourType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cboCarrefourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCarrefourType.Location = New System.Drawing.Point(120, 128)
    Me.cboCarrefourType.MaxDropDownItems = 4
    Me.cboCarrefourType.Name = "cboCarrefourType"
    Me.cboCarrefourType.Size = New System.Drawing.Size(104, 21)
    Me.cboCarrefourType.TabIndex = 3
    '
    'txtNbBranches
    '
    Me.txtNbBranches.Enabled = False
    Me.txtNbBranches.Location = New System.Drawing.Point(120, 80)
    Me.txtNbBranches.MaxLength = 1
    Me.txtNbBranches.Name = "txtNbBranches"
    Me.txtNbBranches.Size = New System.Drawing.Size(16, 20)
    Me.txtNbBranches.TabIndex = 1
    Me.txtNbBranches.Text = ""
    Me.txtNbBranches.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'lblNbBranches
    '
    Me.lblNbBranches.Location = New System.Drawing.Point(8, 80)
    Me.lblNbBranches.Name = "lblNbBranches"
    Me.lblNbBranches.Size = New System.Drawing.Size(120, 24)
    Me.lblNbBranches.TabIndex = 25
    Me.lblNbBranches.Text = "Nombre de branches :"
    '
    'btnFDP
    '
    Me.btnFDP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnFDP.Enabled = False
    Me.btnFDP.Location = New System.Drawing.Point(664, 136)
    Me.btnFDP.Name = "btnFDP"
    Me.btnFDP.Size = New System.Drawing.Size(88, 24)
    Me.btnFDP.TabIndex = 3
    Me.btnFDP.Text = "Fond de plan..."
    '
    'dlgCarGen
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(760, 311)
    Me.Controls.Add(Me.btnFDP)
    Me.Controls.Add(Me.grpModalités)
    Me.Controls.Add(Me.btnParam)
    Me.Controls.Add(Me.grpControleur)
    Me.Controls.Add(Me.grpIdentification)
    Me.Name = "dlgCarGen"
    Me.Text = "Caractéristiques générales du carrefour"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.grpIdentification, 0)
    Me.Controls.SetChildIndex(Me.grpControleur, 0)
    Me.Controls.SetChildIndex(Me.btnParam, 0)
    Me.Controls.SetChildIndex(Me.grpModalités, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.btnFDP, 0)
    Me.grpIdentification.ResumeLayout(False)
    Me.grpControleur.ResumeLayout(False)
    Me.grpModalités.ResumeLayout(False)
    Me.pnlMode.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Const VERSIONFINALE = 0

	Private mCarrefour As Carrefour
	Private mFondDePlan As FondDePlan
  Private sauvFondDePlan As FondDePlan
  Private maVariante As Variante
  Protected mCentre As PointF
  Public mParamDessin As ParamDessin

  'Drapeaux pour la frappe
  Private flagKeyPress As Boolean
  Private CaractèreDouble As Boolean

  Public ReadOnly Property Centre() As PointF
    Get
      Return mCentre
    End Get
  End Property

  '******************************************************************************
  '	Vérifier si les modifs ont été faites 
  '******************************************************************************
  Protected Overrides Function VeriModif() As Boolean
    '	If MessageBox.Show(Me, "Abandonner les modifs ?", NomProduit, MessageBoxButtons.YesNo) = DialogResult.No Then
    'VeriModif = True
    '	End If
  End Function

  '******************************************************************************
  ' Bouton Paramètres 
  '******************************************************************************
  Private Sub btnParam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParam.Click
    Dim sVariante As Variante = cndVariante
    cndVariante = mVariante
    mdiApplication.mnuParamétrage.PerformClick()
    cndVariante = sVariante
  End Sub


  '******************************************************************************
  ' Bouton radio : Fond de plan
  '******************************************************************************
  Private Sub chkFDP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkFDP.CheckedChanged

    Dim chk As CheckBox = sender

    btnFDP.Enabled = chk.Checked

  End Sub

  '******************************************************************************
  ' Choix d'un carrefour type
  '******************************************************************************
  Private Sub cboCarrefourType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCarrefourType.SelectedIndexChanged
    MajNbBranches()
  End Sub

  '******************************************************************************
  ' Mettre à jour le nombre de branches selon le type de carrefour
  '******************************************************************************
  Private Sub MajNbBranches()
    Dim i As Carrefour.CarrefourTypeEnum = cboCarrefourType.SelectedIndex

    Select Case i
      Case Carrefour.CarrefourTypeEnum.EnCroix
        txtNbBranches.Text = 4
      Case Carrefour.CarrefourTypeEnum.EnT, Carrefour.CarrefourTypeEnum.EnY
        txtNbBranches.Text = 3
      Case Carrefour.CarrefourTypeEnum.A5Branches
        txtNbBranches.Text = 5
      Case Carrefour.CarrefourTypeEnum.EnEtoile
        txtNbBranches.Text = 6
    End Select

  End Sub

  '******************************************************************************
  ' Bouton Fond de plan : choisir un fond de plan
  '******************************************************************************
  Private Sub btnFDP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFDP.Click
    Dim dlg As New dlgFDP

    With dlg
      .mFondDePlan = mFondDePlan
      If .ShowDialog(Me) = DialogResult.OK Then
        mFondDePlan = .mFondDePlan
        maVariante.mFondDePlan = mFondDePlan

        If mFondDePlan.EstDXF Then
          Dim p As New PointF(CSng(.txtX.Text), CSng(.txtY.Text))
          If Distance(p, mCentre) > 0 Then
            mCentre = p
            mParamDessin = .ParamDessin
            mFondDePlan.ADessiner = True
          End If

        Else
          mCentre = CType(mFondDePlan, ImageRaster).Centre
          mParamDessin = .ParamDessin
          mFondDePlan.ADessiner = True
        End If

      End If

      .Dispose()
    End With

  End Sub

  '******************************************************************************
  ' Bouton radio : Mode graphique ou dégradé
  '******************************************************************************
  Private Sub radGraphique_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radGraphique.CheckedChanged, radDégradé.CheckedChanged

    chkFDP.Enabled = radGraphique.Checked

    If radDégradé.Checked Then
      btnFDP.Enabled = False
    Else
      btnFDP.Enabled = chkFDP.Checked
    End If

  End Sub

  '******************************************************************************
  ' Chargement de la feuille
  '******************************************************************************
  Private Sub dlgCarGen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
   MyBase.Load


    If IsNothing(mCarrefour) Then
      'Test nécessaire :
      'Contrairement à l'aide en ligne, cet évènement survient à chaque ShowDialog et non seulement la 1ère fois

      With maVariante
        mCarrefour = .mCarrefour
        sauvFondDePlan = .mFondDePlan
        mFondDePlan = .mFondDePlan
      End With

      AjouterCarrefoursType()

      With mCarrefour
        txtCommentaires.Text = .Commentaires
        txtCommune.Text = .Commune
        If .EnAgglo Then
          radEnAgglo.Checked = True
        Else
          radHorsAgglo.Checked = True
        End If

#If DEBUG Then
        If IsNothing(.Nom) Then .Nom = "Place PIRMIL"
#End If
        txtNom.Text = .Nom
        txtRégulation.Text = .ZoneRégulation
        txtTypeControleur.Text = .TypeControleur
        If Not EstNulleDate(.DateControleur) Then
          txtDateControleur.Text = .DateControleur
        Else
          txtDateControleur.Text = ""
        End If
        cboCarrefourType.SelectedIndex = .CarrefourType
        txtNbBranches.Text = .NbBranches

        If mParamDessin.IsEmpty Then
          'Nouveau carrefour
          mParamDessin = New ParamDessin(DéfautEchelle, DéfautOrigine)
        End If
        If cndParamDessin.IsEmpty Then
          '1er  carrefour
          cndParamDessin = mParamDessin
        End If
        If .mCentre.IsEmpty Then
          'Le centre du carrefour sans fond de plan est au centre du picturebox  à 200x300 pixels
          Dim sParam As ParamDessin = cndParamDessin
          cndParamDessin = mParamDessin
          mCentre = PointRéel(New Point(200, 300))
          cndParamDessin = sParam
        Else
          mCentre = .mCentre
        End If
      End With

      With maVariante
        chkFDP.Checked = Not IsNothing(.mFondDePlan)
      End With

    End If

  End Sub

  Private Sub AjouterCarrefoursType()

    Me.cboCarrefourType.Items.AddRange(Carrefour.strCarrefourType)

  End Sub

  '******************************************************************************
  ' Vérifier les données avant la mise à jour
  '******************************************************************************
  Private Function DonnéesVérifiées() As Boolean

#If VERSIONFINALE = 0 Then
    DonnéesVérifiées = True
#Else
		Dim ChampObligatoire As TextBox

		If txtNom.Text.Length = 0 Then
			ChampObligatoire = txtNom
		ElseIf txtCommune.Text.Length = 0 Then
			ChampObligatoire = txtCommune
		ElseIf txtNbBranches.Text.Length = 0 Then
			ChampObligatoire = txtNbBranches
		End If

		If Not IsNothing(ChampObligatoire) Then
			AfficherMessageErreur me,"Donnée obligatoire"
			ChampObligatoire.Focus()
		Else
			If txtNbBranches.Text < Carrefour.MiniNbBranches Then
				MessageBox.Show(Me,"Minimum " & Carrefour.MiniNbBranches & " branches",nomproduit,MessageBoxButtons.OK,MessageBoxIcon.Exclamation )
				txtNbBranches.Focus()
			ElseIf txtNbBranches.Text > Carrefour.MaxiNbBranches Then
				MessageBox.Show(Me,"Maximum " & Carrefour.MaxiNbBranches & " branches",NomProduit,MessageBoxButtons.OK,MessageBoxIcon.Exclamation )
				txtNbBranches.Focus()
			Else
				DonnéesVérifiées = True
			End If
		End If
#End If

  End Function

  '******************************************************************************
  ' Mettre à jour les données du carrefour avec celles de la feuille
  '******************************************************************************
  Friend Sub MettreAjour()
    Dim chaine As String

    With mCarrefour
      .Commune = txtCommune.Text
      .Nom = txtNom.Text
      .EnAgglo = radEnAgglo.Checked

      .CarrefourType = cboCarrefourType.SelectedIndex
      .NbBranches = txtNbBranches.Text

      .Commentaires = txtCommentaires.Text
      .ZoneRégulation = txtRégulation.Text
      .TypeControleur = txtTypeControleur.Text
      chaine = txtDateControleur.Text
      If chaine.Length > 0 Then
        If IsDate(chaine) Then .DateControleur = CDate(chaine)
      Else
        .DateControleur = CDate("01/01/0001")
      End If
      .mCentre = mCentre
    End With

    With maVariante
      If Not IsNothing(.NomFichier) Then
        .AEnregistrer = True
      End If

      .ModeGraphique = radGraphique.Checked
      If .ModeGraphique Then
        If Me.chkFDP.Checked Then
          .mFondDePlan = mFondDePlan
        Else
          .mFondDePlan = Nothing
        End If

      Else
        .mFondDePlan = Nothing

      End If

    End With

  End Sub

  '******************************************************************************
  ' Fermeture de la feuille
  '******************************************************************************
  Private Sub dlgCarGen_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    If DialogResult = DialogResult.OK Then
      If Me.txtNom.Text.Length = 0 Then
        'DialogResult = DialogResult.None
        AfficherMessageErreur(Me, "Nom du carrefour obligatoire")
        e.Cancel = True
        Me.txtNom.Focus()

      ElseIf Not IsNothing(mCarrefour.Nom) Then
      End If

      If Not e.Cancel Then

        If radGraphique.Checked And chkFDP.Checked And IsNothing(mFondDePlan) Then
          'Si un fond de plan est demandé mais non renseigné, on oblige à le saisir
          btnFDP.PerformClick()
          'Si Echec de la saisie du fond de plan : la feuille reste ouverte
          e.Cancel = IsNothing(mFondDePlan)
        End If

        If Not e.Cancel Then
          e.Cancel = Not DonnéesVérifiées()
        End If
        If Not e.Cancel Then cndParamDessin = mParamDessin
      End If
    End If

  End Sub

  '******************************************************************************
  ' Vérifier la date du controleur
  '******************************************************************************
  Private Sub txtDateControleur_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDateControleur.Validating
    Dim chaine As String = txtDateControleur.Text

    If (chaine.Length > 0 And Not IsDate(chaine)) Then
      MessageBox.Show(Me, "Date incorrecte", NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      e.Cancel = True
    End If

  End Sub

  Private Sub txtDateControleur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtDateControleur.KeyPress

    If CaractèreDouble Then
      CaractèreDouble = False
      e.Handled = True
    ElseIf flagKeyPress Then
      'Touche refusée par l'évènement KeyDown
      e.Handled = True
      flagKeyPress = False
    End If
  End Sub

  Private Sub txtDateControleur_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateControleur.KeyDown
    If e.KeyValue = Keys.OemCloseBrackets And Not CaractèreDouble Then
      CaractèreDouble = True
    Else
      flagKeyPress = EstInCompatibleDate(e)
    End If
  End Sub

  Friend Property mVariante() As Variante
    Get
      Return maVariante
    End Get
    Set(ByVal Value As Variante)
      maVariante = Value
    End Set
  End Property

  Private Sub radEnAgglo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radEnAgglo.CheckedChanged, radHorsAgglo.CheckedChanged
    'Par dérogation aux principes de saisie/validation, on répercute immédiatement ce cochage pour l'appel du bouton paramètres (pour afficher le bon Jaune inutilisé)
    mVariante.mCarrefour.EnAgglo = radEnAgglo.Checked
  End Sub

  Private Sub dlgCarGen_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_NOUVEAU
  End Sub

End Class
