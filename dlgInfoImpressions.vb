'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgInfoImpressions.vb										  											'
'						Classes																														'
'							dlgInfoImpressions : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgInfoImpressions --------------------------
'Dialogue pour saisie des informations complémentaires utiles dans les impressions
'=====================================================================================================
Public Class dlgInfoImpressions
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
  Friend WithEvents lblCoorDonnéesService As System.Windows.Forms.Label
  Friend WithEvents grpCarrefour As System.Windows.Forms.GroupBox
  Friend WithEvents lblNumCarrefour As System.Windows.Forms.Label
  Friend WithEvents txtNumCarrefour As System.Windows.Forms.TextBox
  Friend WithEvents lblSuiviPar As System.Windows.Forms.Label
  Friend WithEvents txtSuiviPar As System.Windows.Forms.TextBox
  Friend WithEvents grpMatériel As System.Windows.Forms.GroupBox
  Friend WithEvents txtCoorDonnéesService As System.Windows.Forms.TextBox
  Friend WithEvents txtTypeControleur As System.Windows.Forms.TextBox
  Friend WithEvents lblTypeControleur As System.Windows.Forms.Label
  Friend WithEvents lblFabricant As System.Windows.Forms.Label
  Friend WithEvents txtFabricant As System.Windows.Forms.TextBox
  Friend WithEvents grpVisa As System.Windows.Forms.GroupBox
  Friend WithEvents lblVisa As System.Windows.Forms.Label
  Friend WithEvents txtVisa As System.Windows.Forms.TextBox
  Friend WithEvents lblVisaDe As System.Windows.Forms.Label
  Friend WithEvents txtVisaDe As System.Windows.Forms.TextBox
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  Friend WithEvents grpEtude As System.Windows.Forms.GroupBox
  Friend WithEvents lblEtudeRendue As System.Windows.Forms.Label
  Friend WithEvents txtDateEtude As System.Windows.Forms.TextBox
  Friend WithEvents lblObjectifEtude As System.Windows.Forms.Label
  Friend WithEvents txtObjectifEtude As System.Windows.Forms.TextBox
  Friend WithEvents txtDate1erService As System.Windows.Forms.TextBox
  Friend WithEvents lblDate1erService As System.Windows.Forms.Label
  Friend WithEvents grpDates As System.Windows.Forms.GroupBox
  Friend WithEvents lblDateService As System.Windows.Forms.Label
  Friend WithEvents txtDateService As System.Windows.Forms.TextBox
  Friend WithEvents lblModifications As System.Windows.Forms.Label
  Friend WithEvents txtDateModifications As System.Windows.Forms.TextBox
  Friend WithEvents lblModificationsPlage As System.Windows.Forms.Label
  Friend WithEvents txtNomCarrefour As System.Windows.Forms.TextBox
  Friend WithEvents lblNomCarrefour As System.Windows.Forms.Label
  Friend WithEvents LblEtudeRéalisateur As System.Windows.Forms.Label
  Friend WithEvents grpVersion As System.Windows.Forms.GroupBox
  Friend WithEvents lblNumVersion As System.Windows.Forms.Label
  Friend WithEvents txtNumVersion As System.Windows.Forms.TextBox
  Friend WithEvents lblDateVersion As System.Windows.Forms.Label
  Friend WithEvents txtDateVersion As System.Windows.Forms.TextBox
  Friend WithEvents txtDateModifPlage As System.Windows.Forms.TextBox
  Friend WithEvents txtRéalisateurEtude As System.Windows.Forms.TextBox
  Friend WithEvents lblVisaTrafics As System.Windows.Forms.Label
  Friend WithEvents txtVisasTrafics As System.Windows.Forms.TextBox
  Friend WithEvents txtSystèmeRégulation As System.Windows.Forms.TextBox
  Friend WithEvents lblSystèmeRégulation As System.Windows.Forms.Label
  Friend WithEvents txtEnchainementDesPhases As System.Windows.Forms.TextBox
  Friend WithEvents lblEnchainementPhases As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.lblCoorDonnéesService = New System.Windows.Forms.Label
    Me.txtCoorDonnéesService = New System.Windows.Forms.TextBox
    Me.grpCarrefour = New System.Windows.Forms.GroupBox
    Me.txtNumCarrefour = New System.Windows.Forms.TextBox
    Me.lblNumCarrefour = New System.Windows.Forms.Label
    Me.txtNomCarrefour = New System.Windows.Forms.TextBox
    Me.lblNomCarrefour = New System.Windows.Forms.Label
    Me.txtDate1erService = New System.Windows.Forms.TextBox
    Me.lblDate1erService = New System.Windows.Forms.Label
    Me.lblSuiviPar = New System.Windows.Forms.Label
    Me.txtSuiviPar = New System.Windows.Forms.TextBox
    Me.grpMatériel = New System.Windows.Forms.GroupBox
    Me.lblFabricant = New System.Windows.Forms.Label
    Me.txtFabricant = New System.Windows.Forms.TextBox
    Me.lblTypeControleur = New System.Windows.Forms.Label
    Me.txtTypeControleur = New System.Windows.Forms.TextBox
    Me.grpVisa = New System.Windows.Forms.GroupBox
    Me.lblVisaTrafics = New System.Windows.Forms.Label
    Me.txtVisasTrafics = New System.Windows.Forms.TextBox
    Me.lblVisa = New System.Windows.Forms.Label
    Me.txtVisa = New System.Windows.Forms.TextBox
    Me.lblVisaDe = New System.Windows.Forms.Label
    Me.txtVisaDe = New System.Windows.Forms.TextBox
    Me.tipBulle = New System.Windows.Forms.ToolTip(Me.components)
    Me.grpEtude = New System.Windows.Forms.GroupBox
    Me.txtObjectifEtude = New System.Windows.Forms.TextBox
    Me.lblObjectifEtude = New System.Windows.Forms.Label
    Me.LblEtudeRéalisateur = New System.Windows.Forms.Label
    Me.txtRéalisateurEtude = New System.Windows.Forms.TextBox
    Me.lblEtudeRendue = New System.Windows.Forms.Label
    Me.txtDateEtude = New System.Windows.Forms.TextBox
    Me.grpDates = New System.Windows.Forms.GroupBox
    Me.lblModificationsPlage = New System.Windows.Forms.Label
    Me.txtDateModifPlage = New System.Windows.Forms.TextBox
    Me.lblModifications = New System.Windows.Forms.Label
    Me.txtDateModifications = New System.Windows.Forms.TextBox
    Me.lblDateService = New System.Windows.Forms.Label
    Me.txtDateService = New System.Windows.Forms.TextBox
    Me.grpVersion = New System.Windows.Forms.GroupBox
    Me.lblNumVersion = New System.Windows.Forms.Label
    Me.txtNumVersion = New System.Windows.Forms.TextBox
    Me.lblDateVersion = New System.Windows.Forms.Label
    Me.txtDateVersion = New System.Windows.Forms.TextBox
    Me.txtSystèmeRégulation = New System.Windows.Forms.TextBox
    Me.lblSystèmeRégulation = New System.Windows.Forms.Label
    Me.txtEnchainementDesPhases = New System.Windows.Forms.TextBox
    Me.lblEnchainementPhases = New System.Windows.Forms.Label
    Me.grpCarrefour.SuspendLayout()
    Me.grpMatériel.SuspendLayout()
    Me.grpVisa.SuspendLayout()
    Me.grpEtude.SuspendLayout()
    Me.grpDates.SuspendLayout()
    Me.grpVersion.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(474, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.TabIndex = 13
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(472, 96)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(474, 16)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.TabIndex = 11
    '
    'lblCoorDonnéesService
    '
    Me.lblCoorDonnéesService.Location = New System.Drawing.Point(16, 136)
    Me.lblCoorDonnéesService.Name = "lblCoorDonnéesService"
    Me.lblCoorDonnéesService.Size = New System.Drawing.Size(80, 24)
    Me.lblCoorDonnéesService.TabIndex = 1
    Me.lblCoorDonnéesService.Text = "Coordonnées du Service"
    '
    'txtCoorDonnéesService
    '
    Me.txtCoorDonnéesService.AutoSize = False
    Me.txtCoorDonnéesService.Location = New System.Drawing.Point(96, 136)
    Me.txtCoorDonnéesService.Multiline = True
    Me.txtCoorDonnéesService.Name = "txtCoorDonnéesService"
    Me.txtCoorDonnéesService.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtCoorDonnéesService.Size = New System.Drawing.Size(120, 32)
    Me.txtCoorDonnéesService.TabIndex = 2
    Me.txtCoorDonnéesService.Text = ""
    '
    'grpCarrefour
    '
    Me.grpCarrefour.Controls.Add(Me.txtNumCarrefour)
    Me.grpCarrefour.Controls.Add(Me.lblNumCarrefour)
    Me.grpCarrefour.Controls.Add(Me.txtNomCarrefour)
    Me.grpCarrefour.Controls.Add(Me.lblNomCarrefour)
    Me.grpCarrefour.Controls.Add(Me.txtDate1erService)
    Me.grpCarrefour.Controls.Add(Me.lblDate1erService)
    Me.grpCarrefour.Location = New System.Drawing.Point(16, 16)
    Me.grpCarrefour.Name = "grpCarrefour"
    Me.grpCarrefour.Size = New System.Drawing.Size(448, 96)
    Me.grpCarrefour.TabIndex = 0
    Me.grpCarrefour.TabStop = False
    Me.grpCarrefour.Text = "Carrefour"
    '
    'txtNumCarrefour
    '
    Me.txtNumCarrefour.Location = New System.Drawing.Point(56, 56)
    Me.txtNumCarrefour.Name = "txtNumCarrefour"
    Me.txtNumCarrefour.Size = New System.Drawing.Size(72, 20)
    Me.txtNumCarrefour.TabIndex = 1
    Me.txtNumCarrefour.Text = ""
    '
    'lblNumCarrefour
    '
    Me.lblNumCarrefour.Location = New System.Drawing.Point(8, 56)
    Me.lblNumCarrefour.Name = "lblNumCarrefour"
    Me.lblNumCarrefour.Size = New System.Drawing.Size(56, 16)
    Me.lblNumCarrefour.TabIndex = 30
    Me.lblNumCarrefour.Text = "Numéro :"
    '
    'txtNomCarrefour
    '
    Me.txtNomCarrefour.Location = New System.Drawing.Point(56, 24)
    Me.txtNomCarrefour.Name = "txtNomCarrefour"
    Me.txtNomCarrefour.Size = New System.Drawing.Size(264, 20)
    Me.txtNomCarrefour.TabIndex = 0
    Me.txtNomCarrefour.Text = ""
    '
    'lblNomCarrefour
    '
    Me.lblNomCarrefour.Location = New System.Drawing.Point(8, 24)
    Me.lblNomCarrefour.Name = "lblNomCarrefour"
    Me.lblNomCarrefour.Size = New System.Drawing.Size(40, 19)
    Me.lblNomCarrefour.TabIndex = 29
    Me.lblNomCarrefour.Text = "Nom  :"
    '
    'txtDate1erService
    '
    Me.txtDate1erService.Location = New System.Drawing.Point(248, 56)
    Me.txtDate1erService.MaxLength = 10
    Me.txtDate1erService.Name = "txtDate1erService"
    Me.txtDate1erService.Size = New System.Drawing.Size(72, 20)
    Me.txtDate1erService.TabIndex = 2
    Me.txtDate1erService.Text = ""
    '
    'lblDate1erService
    '
    Me.lblDate1erService.Location = New System.Drawing.Point(152, 56)
    Me.lblDate1erService.Name = "lblDate1erService"
    Me.lblDate1erService.Size = New System.Drawing.Size(80, 24)
    Me.lblDate1erService.TabIndex = 25
    Me.lblDate1erService.Text = "Première mise en service :"
    '
    'lblSuiviPar
    '
    Me.lblSuiviPar.Location = New System.Drawing.Point(16, 184)
    Me.lblSuiviPar.Name = "lblSuiviPar"
    Me.lblSuiviPar.Size = New System.Drawing.Size(72, 26)
    Me.lblSuiviPar.TabIndex = 14
    Me.lblSuiviPar.Text = "Travaux suivis par :"
    '
    'txtSuiviPar
    '
    Me.txtSuiviPar.Location = New System.Drawing.Point(96, 192)
    Me.txtSuiviPar.MaxLength = 20
    Me.txtSuiviPar.Name = "txtSuiviPar"
    Me.txtSuiviPar.Size = New System.Drawing.Size(120, 20)
    Me.txtSuiviPar.TabIndex = 3
    Me.txtSuiviPar.Text = ""
    '
    'grpMatériel
    '
    Me.grpMatériel.Controls.Add(Me.lblFabricant)
    Me.grpMatériel.Controls.Add(Me.txtFabricant)
    Me.grpMatériel.Controls.Add(Me.lblTypeControleur)
    Me.grpMatériel.Controls.Add(Me.txtTypeControleur)
    Me.grpMatériel.Location = New System.Drawing.Point(264, 128)
    Me.grpMatériel.Name = "grpMatériel"
    Me.grpMatériel.Size = New System.Drawing.Size(200, 96)
    Me.grpMatériel.TabIndex = 4
    Me.grpMatériel.TabStop = False
    Me.grpMatériel.Text = "Matériel"
    '
    'lblFabricant
    '
    Me.lblFabricant.Location = New System.Drawing.Point(8, 64)
    Me.lblFabricant.Name = "lblFabricant"
    Me.lblFabricant.Size = New System.Drawing.Size(64, 16)
    Me.lblFabricant.TabIndex = 17
    Me.lblFabricant.Text = "Fabricant :"
    '
    'txtFabricant
    '
    Me.txtFabricant.Location = New System.Drawing.Point(80, 64)
    Me.txtFabricant.MaxLength = 20
    Me.txtFabricant.Name = "txtFabricant"
    Me.txtFabricant.TabIndex = 1
    Me.txtFabricant.Text = ""
    '
    'lblTypeControleur
    '
    Me.lblTypeControleur.Location = New System.Drawing.Point(8, 24)
    Me.lblTypeControleur.Name = "lblTypeControleur"
    Me.lblTypeControleur.Size = New System.Drawing.Size(64, 26)
    Me.lblTypeControleur.TabIndex = 15
    Me.lblTypeControleur.Text = "Type de contrôleur :"
    '
    'txtTypeControleur
    '
    Me.txtTypeControleur.Location = New System.Drawing.Point(80, 24)
    Me.txtTypeControleur.MaxLength = 20
    Me.txtTypeControleur.Name = "txtTypeControleur"
    Me.txtTypeControleur.TabIndex = 0
    Me.txtTypeControleur.Text = ""
    '
    'grpVisa
    '
    Me.grpVisa.Controls.Add(Me.lblVisaTrafics)
    Me.grpVisa.Controls.Add(Me.txtVisasTrafics)
    Me.grpVisa.Controls.Add(Me.lblVisa)
    Me.grpVisa.Controls.Add(Me.txtVisa)
    Me.grpVisa.Controls.Add(Me.lblVisaDe)
    Me.grpVisa.Controls.Add(Me.txtVisaDe)
    Me.grpVisa.Location = New System.Drawing.Point(16, 232)
    Me.grpVisa.Name = "grpVisa"
    Me.grpVisa.Size = New System.Drawing.Size(200, 136)
    Me.grpVisa.TabIndex = 5
    Me.grpVisa.TabStop = False
    Me.grpVisa.Text = "Visas"
    '
    'lblVisaTrafics
    '
    Me.lblVisaTrafics.Location = New System.Drawing.Point(16, 96)
    Me.lblVisaTrafics.Name = "lblVisaTrafics"
    Me.lblVisaTrafics.Size = New System.Drawing.Size(48, 32)
    Me.lblVisaTrafics.TabIndex = 19
    Me.lblVisaTrafics.Text = "Visa des Trafics  :"
    '
    'txtVisasTrafics
    '
    Me.txtVisasTrafics.Location = New System.Drawing.Point(80, 104)
    Me.txtVisasTrafics.MaxLength = 20
    Me.txtVisasTrafics.Name = "txtVisasTrafics"
    Me.txtVisasTrafics.TabIndex = 18
    Me.txtVisasTrafics.Text = ""
    '
    'lblVisa
    '
    Me.lblVisa.Location = New System.Drawing.Point(16, 64)
    Me.lblVisa.Name = "lblVisa"
    Me.lblVisa.Size = New System.Drawing.Size(48, 16)
    Me.lblVisa.TabIndex = 17
    Me.lblVisa.Text = "Numéro  :"
    '
    'txtVisa
    '
    Me.txtVisa.Location = New System.Drawing.Point(80, 64)
    Me.txtVisa.MaxLength = 10
    Me.txtVisa.Name = "txtVisa"
    Me.txtVisa.TabIndex = 16
    Me.txtVisa.Text = ""
    '
    'lblVisaDe
    '
    Me.lblVisaDe.Location = New System.Drawing.Point(16, 24)
    Me.lblVisaDe.Name = "lblVisaDe"
    Me.lblVisaDe.Size = New System.Drawing.Size(48, 26)
    Me.lblVisaDe.TabIndex = 15
    Me.lblVisaDe.Text = "Visa de :"
    '
    'txtVisaDe
    '
    Me.txtVisaDe.Location = New System.Drawing.Point(80, 24)
    Me.txtVisaDe.MaxLength = 20
    Me.txtVisaDe.Name = "txtVisaDe"
    Me.txtVisaDe.TabIndex = 1
    Me.txtVisaDe.Text = ""
    '
    'grpEtude
    '
    Me.grpEtude.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpEtude.Controls.Add(Me.txtObjectifEtude)
    Me.grpEtude.Controls.Add(Me.lblObjectifEtude)
    Me.grpEtude.Controls.Add(Me.LblEtudeRéalisateur)
    Me.grpEtude.Controls.Add(Me.txtRéalisateurEtude)
    Me.grpEtude.Controls.Add(Me.lblEtudeRendue)
    Me.grpEtude.Controls.Add(Me.txtDateEtude)
    Me.grpEtude.Location = New System.Drawing.Point(16, 384)
    Me.grpEtude.Name = "grpEtude"
    Me.grpEtude.Size = New System.Drawing.Size(200, 128)
    Me.grpEtude.TabIndex = 7
    Me.grpEtude.TabStop = False
    Me.grpEtude.Text = "Etude"
    '
    'txtObjectifEtude
    '
    Me.txtObjectifEtude.AutoSize = False
    Me.txtObjectifEtude.Location = New System.Drawing.Point(72, 88)
    Me.txtObjectifEtude.Multiline = True
    Me.txtObjectifEtude.Name = "txtObjectifEtude"
    Me.txtObjectifEtude.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtObjectifEtude.Size = New System.Drawing.Size(120, 32)
    Me.txtObjectifEtude.TabIndex = 2
    Me.txtObjectifEtude.Text = ""
    '
    'lblObjectifEtude
    '
    Me.lblObjectifEtude.Location = New System.Drawing.Point(8, 88)
    Me.lblObjectifEtude.Name = "lblObjectifEtude"
    Me.lblObjectifEtude.Size = New System.Drawing.Size(56, 16)
    Me.lblObjectifEtude.TabIndex = 19
    Me.lblObjectifEtude.Text = "Objectif :"
    '
    'LblEtudeRéalisateur
    '
    Me.LblEtudeRéalisateur.Location = New System.Drawing.Point(8, 56)
    Me.LblEtudeRéalisateur.Name = "LblEtudeRéalisateur"
    Me.LblEtudeRéalisateur.Size = New System.Drawing.Size(80, 16)
    Me.LblEtudeRéalisateur.TabIndex = 17
    Me.LblEtudeRéalisateur.Text = "Réalisée par  :"
    '
    'txtRéalisateurEtude
    '
    Me.txtRéalisateurEtude.Location = New System.Drawing.Point(88, 56)
    Me.txtRéalisateurEtude.MaxLength = 20
    Me.txtRéalisateurEtude.Name = "txtRéalisateurEtude"
    Me.txtRéalisateurEtude.TabIndex = 1
    Me.txtRéalisateurEtude.Text = ""
    '
    'lblEtudeRendue
    '
    Me.lblEtudeRendue.Location = New System.Drawing.Point(8, 24)
    Me.lblEtudeRendue.Name = "lblEtudeRendue"
    Me.lblEtudeRendue.Size = New System.Drawing.Size(64, 16)
    Me.lblEtudeRendue.TabIndex = 15
    Me.lblEtudeRendue.Text = "Rendue le :"
    '
    'txtDateEtude
    '
    Me.txtDateEtude.Location = New System.Drawing.Point(88, 24)
    Me.txtDateEtude.MaxLength = 10
    Me.txtDateEtude.Name = "txtDateEtude"
    Me.txtDateEtude.TabIndex = 0
    Me.txtDateEtude.Text = ""
    '
    'grpDates
    '
    Me.grpDates.Controls.Add(Me.lblModificationsPlage)
    Me.grpDates.Controls.Add(Me.txtDateModifPlage)
    Me.grpDates.Controls.Add(Me.lblModifications)
    Me.grpDates.Controls.Add(Me.txtDateModifications)
    Me.grpDates.Controls.Add(Me.lblDateService)
    Me.grpDates.Controls.Add(Me.txtDateService)
    Me.grpDates.Location = New System.Drawing.Point(264, 240)
    Me.grpDates.Name = "grpDates"
    Me.grpDates.Size = New System.Drawing.Size(200, 128)
    Me.grpDates.TabIndex = 6
    Me.grpDates.TabStop = False
    Me.grpDates.Text = "Dates"
    '
    'lblModificationsPlage
    '
    Me.lblModificationsPlage.Location = New System.Drawing.Point(8, 88)
    Me.lblModificationsPlage.Name = "lblModificationsPlage"
    Me.lblModificationsPlage.Size = New System.Drawing.Size(80, 32)
    Me.lblModificationsPlage.TabIndex = 21
    Me.lblModificationsPlage.Text = "Modifications Plage horaire :"
    '
    'txtDateModifPlage
    '
    Me.txtDateModifPlage.Location = New System.Drawing.Point(88, 88)
    Me.txtDateModifPlage.MaxLength = 10
    Me.txtDateModifPlage.Name = "txtDateModifPlage"
    Me.txtDateModifPlage.TabIndex = 3
    Me.txtDateModifPlage.Text = ""
    '
    'lblModifications
    '
    Me.lblModifications.Location = New System.Drawing.Point(8, 56)
    Me.lblModifications.Name = "lblModifications"
    Me.lblModifications.Size = New System.Drawing.Size(72, 16)
    Me.lblModifications.TabIndex = 19
    Me.lblModifications.Text = "Modifications :"
    '
    'txtDateModifications
    '
    Me.txtDateModifications.Location = New System.Drawing.Point(88, 56)
    Me.txtDateModifications.MaxLength = 10
    Me.txtDateModifications.Name = "txtDateModifications"
    Me.txtDateModifications.TabIndex = 2
    Me.txtDateModifications.Text = ""
    '
    'lblDateService
    '
    Me.lblDateService.Location = New System.Drawing.Point(8, 24)
    Me.lblDateService.Name = "lblDateService"
    Me.lblDateService.Size = New System.Drawing.Size(48, 24)
    Me.lblDateService.TabIndex = 17
    Me.lblDateService.Text = "Mise en service :"
    '
    'txtDateService
    '
    Me.txtDateService.Location = New System.Drawing.Point(88, 24)
    Me.txtDateService.MaxLength = 10
    Me.txtDateService.Name = "txtDateService"
    Me.txtDateService.TabIndex = 1
    Me.txtDateService.Text = ""
    '
    'grpVersion
    '
    Me.grpVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpVersion.Controls.Add(Me.lblNumVersion)
    Me.grpVersion.Controls.Add(Me.txtNumVersion)
    Me.grpVersion.Controls.Add(Me.lblDateVersion)
    Me.grpVersion.Controls.Add(Me.txtDateVersion)
    Me.grpVersion.Location = New System.Drawing.Point(264, 392)
    Me.grpVersion.Name = "grpVersion"
    Me.grpVersion.Size = New System.Drawing.Size(200, 88)
    Me.grpVersion.TabIndex = 8
    Me.grpVersion.TabStop = False
    Me.grpVersion.Text = "Version"
    '
    'lblNumVersion
    '
    Me.lblNumVersion.Location = New System.Drawing.Point(8, 24)
    Me.lblNumVersion.Name = "lblNumVersion"
    Me.lblNumVersion.Size = New System.Drawing.Size(56, 16)
    Me.lblNumVersion.TabIndex = 21
    Me.lblNumVersion.Text = "Numéro  :"
    '
    'txtNumVersion
    '
    Me.txtNumVersion.Location = New System.Drawing.Point(64, 24)
    Me.txtNumVersion.MaxLength = 10
    Me.txtNumVersion.Name = "txtNumVersion"
    Me.txtNumVersion.TabIndex = 0
    Me.txtNumVersion.Text = ""
    '
    'lblDateVersion
    '
    Me.lblDateVersion.Location = New System.Drawing.Point(8, 56)
    Me.lblDateVersion.Name = "lblDateVersion"
    Me.lblDateVersion.Size = New System.Drawing.Size(40, 16)
    Me.lblDateVersion.TabIndex = 19
    Me.lblDateVersion.Text = "Date :"
    '
    'txtDateVersion
    '
    Me.txtDateVersion.Location = New System.Drawing.Point(64, 56)
    Me.txtDateVersion.MaxLength = 10
    Me.txtDateVersion.Name = "txtDateVersion"
    Me.txtDateVersion.TabIndex = 1
    Me.txtDateVersion.Text = ""
    '
    'txtSystèmeRégulation
    '
    Me.txtSystèmeRégulation.AutoSize = False
    Me.txtSystèmeRégulation.Location = New System.Drawing.Point(344, 488)
    Me.txtSystèmeRégulation.Multiline = True
    Me.txtSystèmeRégulation.Name = "txtSystèmeRégulation"
    Me.txtSystèmeRégulation.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtSystèmeRégulation.Size = New System.Drawing.Size(120, 32)
    Me.txtSystèmeRégulation.TabIndex = 9
    Me.txtSystèmeRégulation.Text = ""
    '
    'lblSystèmeRégulation
    '
    Me.lblSystèmeRégulation.Location = New System.Drawing.Point(264, 488)
    Me.lblSystèmeRégulation.Name = "lblSystèmeRégulation"
    Me.lblSystèmeRégulation.Size = New System.Drawing.Size(80, 32)
    Me.lblSystèmeRégulation.TabIndex = 21
    Me.lblSystèmeRégulation.Text = "Système de régulation :"
    '
    'txtEnchainementDesPhases
    '
    Me.txtEnchainementDesPhases.AutoSize = False
    Me.txtEnchainementDesPhases.Location = New System.Drawing.Point(112, 528)
    Me.txtEnchainementDesPhases.Multiline = True
    Me.txtEnchainementDesPhases.Name = "txtEnchainementDesPhases"
    Me.txtEnchainementDesPhases.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtEnchainementDesPhases.Size = New System.Drawing.Size(352, 32)
    Me.txtEnchainementDesPhases.TabIndex = 10
    Me.txtEnchainementDesPhases.Text = ""
    '
    'lblEnchainementPhases
    '
    Me.lblEnchainementPhases.Location = New System.Drawing.Point(16, 528)
    Me.lblEnchainementPhases.Name = "lblEnchainementPhases"
    Me.lblEnchainementPhases.Size = New System.Drawing.Size(96, 32)
    Me.lblEnchainementPhases.TabIndex = 23
    Me.lblEnchainementPhases.Text = "Enchainement des phases :"
    '
    'dlgInfoImpressions
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(562, 567)
    Me.Controls.Add(Me.txtEnchainementDesPhases)
    Me.Controls.Add(Me.lblEnchainementPhases)
    Me.Controls.Add(Me.txtSystèmeRégulation)
    Me.Controls.Add(Me.lblSystèmeRégulation)
    Me.Controls.Add(Me.grpVersion)
    Me.Controls.Add(Me.grpDates)
    Me.Controls.Add(Me.grpEtude)
    Me.Controls.Add(Me.grpVisa)
    Me.Controls.Add(Me.grpMatériel)
    Me.Controls.Add(Me.txtSuiviPar)
    Me.Controls.Add(Me.lblSuiviPar)
    Me.Controls.Add(Me.grpCarrefour)
    Me.Controls.Add(Me.txtCoorDonnéesService)
    Me.Controls.Add(Me.lblCoorDonnéesService)
    Me.Name = "dlgInfoImpressions"
    Me.Text = "Informations pour l'impression"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.lblCoorDonnéesService, 0)
    Me.Controls.SetChildIndex(Me.txtCoorDonnéesService, 0)
    Me.Controls.SetChildIndex(Me.grpCarrefour, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.lblSuiviPar, 0)
    Me.Controls.SetChildIndex(Me.txtSuiviPar, 0)
    Me.Controls.SetChildIndex(Me.grpMatériel, 0)
    Me.Controls.SetChildIndex(Me.grpVisa, 0)
    Me.Controls.SetChildIndex(Me.grpEtude, 0)
    Me.Controls.SetChildIndex(Me.grpDates, 0)
    Me.Controls.SetChildIndex(Me.grpVersion, 0)
    Me.Controls.SetChildIndex(Me.lblSystèmeRégulation, 0)
    Me.Controls.SetChildIndex(Me.txtSystèmeRégulation, 0)
    Me.Controls.SetChildIndex(Me.lblEnchainementPhases, 0)
    Me.Controls.SetChildIndex(Me.txtEnchainementDesPhases, 0)
    Me.grpCarrefour.ResumeLayout(False)
    Me.grpMatériel.ResumeLayout(False)
    Me.grpVisa.ResumeLayout(False)
    Me.grpEtude.ResumeLayout(False)
    Me.grpDates.ResumeLayout(False)
    Me.grpVersion.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "Déclarations"
  Private maVariante As Variante
  Private mCarrefour As Carrefour

  'Drapeaux pour la frappe
  Private flagKeyPress As Boolean
  Private CaractèreDouble As Boolean
#End Region

  Friend Property mVariante() As Variante
    Get
      Return maVariante
    End Get
    Set(ByVal Value As Variante)
      maVariante = Value
    End Set
  End Property


#Region "Fonctions de la feuille"
  '***************************************************************************************
  ' Chargement de la feuille
  '***************************************************************************************
  Private Sub dlgInfoImpressions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    mCarrefour = maVariante.mCarrefour

    'Bulles d'aide
    Me.tipBulle.SetToolTip(Me.txtTypeControleur, "Nom et Qualité")
    Me.tipBulle.SetToolTip(Me.txtDate1erService, "Format : JJ/MM/AAAA")
    Me.tipBulle.SetToolTip(Me.txtDateEtude, "Format : JJ/MM/AAAA")
    Me.tipBulle.SetToolTip(Me.txtDateService, "Format : JJ/MM/AAAA")
    Me.tipBulle.SetToolTip(Me.txtDateModifications, "Format : JJ/MM/AAAA")
    Me.tipBulle.SetToolTip(Me.txtDateModifPlage, "Format : JJ/MM/AAAA")
    Me.tipBulle.SetToolTip(Me.txtDateVersion, "Format : JJ/MM/AAAA")

    With mCarrefour
      Me.txtNomCarrefour.Text = .Nom
      Me.txtNumCarrefour.Text = .Numéro
      Me.txtTypeControleur.Text = .TypeControleur
      If Not EstNulleDate(.DatePremierService) <> 0 Then Me.txtDate1erService.Text = .DatePremierService
      Me.txtCoorDonnéesService.Text = .CoordonnéesService
      Me.txtSuiviPar.Text = .SuperviseurTravaux
      Me.txtVisaDe.Text = .OrigineVisa
      Me.txtVisa.Text = .NuméroVisa
      Me.txtVisasTrafics.Text = .VisaTrafics
      If Not EstNulleDate(.DateEtude) <> 0 Then Me.txtDateEtude.Text = .DateEtude
      Me.txtRéalisateurEtude.Text = .RéalisateurEtude
      Me.txtObjectifEtude.Text = .ObjectifEtude
      Me.txtFabricant.Text = .FabricantControleur
      If Not EstNulleDate(.DateMiseEnService) <> 0 Then Me.txtDateService.Text = .DateMiseEnService
      If Not EstNulleDate(.DateModification) <> 0 Then Me.txtDateModifications.Text = .DateModification
      If Not EstNulleDate(.DateModifPlageHoraire) <> 0 Then Me.txtDateModifPlage.Text = .DateModifPlageHoraire
      Me.txtNumVersion.Text = .NumVersion
      If Not EstNulleDate(.DateVersion) <> 0 Then Me.txtDateVersion.Text = .DateVersion
      Me.txtSystèmeRégulation.Text = .SystèmeRégulation
      Me.txtEnchainementDesPhases.Text = .EnchainementPhases
    End With

  End Sub
#End Region

#Region "Controles de saisie"

  Private Sub txtDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
  Handles txtDateEtude.KeyDown, txtDate1erService.KeyDown, txtDateModifications.KeyDown, txtDateModifPlage.KeyDown, txtDateService.KeyDown, txtDateVersion.KeyDown

    If e.KeyValue = Keys.OemCloseBrackets And Not CaractèreDouble Then
      CaractèreDouble = True
    Else
      flagKeyPress = EstInCompatibleDate(e)
    End If

  End Sub


  Private Sub txtDate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtDateEtude.KeyPress, txtDate1erService.KeyPress, txtDateModifications.KeyPress, txtDateModifPlage.KeyPress, txtDateService.KeyPress, txtDateVersion.KeyPress

    If CaractèreDouble Then
      CaractèreDouble = False
      e.Handled = True
    ElseIf flagKeyPress Then
      'Touche refusée par l'évènement KeyPress
      e.Handled = True
      flagKeyPress = False
    End If

  End Sub

  Private Sub txtDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtDateEtude.Validating, txtDate1erService.Validating, txtDateModifications.Validating, txtDateModifPlage.Validating, txtDateService.Validating, txtDateVersion.Validating

    Dim chaine As String = txtDateService.Text

    If chaine.Length > 0 And Not IsDate(chaine) Then
      MessageBox.Show(Me, "Date incorrecte", NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      e.Cancel = True
    End If

  End Sub
#End Region

#Region "LectureEcriture"
  Public Sub MettreAJour()

    With mCarrefour
      .Nom = Me.txtNomCarrefour.Text
      .Numéro = Me.txtNumCarrefour.Text
      .TypeControleur = Me.txtTypeControleur.Text
      .DatePremierService = ValeurDate(txtDate1erService)
      .CoordonnéesService = Me.txtCoorDonnéesService.Text
      .SuperviseurTravaux = Me.txtSuiviPar.Text
      .OrigineVisa = Me.txtVisaDe.Text
      .NuméroVisa = Me.txtVisa.Text
      .VisaTrafics = Me.txtVisasTrafics.Text
      .DateEtude = ValeurDate(Me.txtDateEtude)
      .RéalisateurEtude = Me.txtRéalisateurEtude.Text
      .ObjectifEtude = Me.txtObjectifEtude.Text
      .FabricantControleur = Me.txtFabricant.Text
      .DateMiseEnService = ValeurDate(txtDateService)
      .DateModification = ValeurDate(txtDateModifications)
      .DateModifPlageHoraire = ValeurDate(txtDateModifPlage)
      .NumVersion = txtNumVersion.Text
      .DateVersion = ValeurDate(txtDateVersion)
      .SystèmeRégulation = Me.txtSystèmeRégulation.Text
      .EnchainementPhases = Me.txtEnchainementDesPhases.Text
    End With

  End Sub

  Private Function ValeurDate(ByVal txt As TextBox) As Date
    Dim chaine As String

    chaine = txt.Text
    If chaine.Length > 0 And IsDate(chaine) Then Return CDate(chaine)
  End Function

#End Region


  Private Sub txtTypeControleur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTypeControleur.TextChanged

  End Sub

  Private Sub dlgInfoImpressions_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_IMPRIMER
  End Sub
End Class
