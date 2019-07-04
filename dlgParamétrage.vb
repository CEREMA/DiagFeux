'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgParamétrage.vb																						'
'						Saisie des paramètres du site 																		'
'																																							'
'						Classe																									'
'							dlgParamétrage																											'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe dlgParamétrage --------------------------
'Dialogue pour saisie du paramétrage du poste : données personnalisées
'=====================================================================================================
Public Class dlgParamétrage
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
  Friend WithEvents grpUtilisateur As System.Windows.Forms.GroupBox
  Friend WithEvents grpTrafic As System.Windows.Forms.GroupBox
  Friend WithEvents grpVitesse As System.Windows.Forms.GroupBox
  Friend WithEvents txtVitessePiéton As System.Windows.Forms.TextBox
  Friend WithEvents txtDébitSaturation As System.Windows.Forms.TextBox
  Friend WithEvents txtVitesseVélo As System.Windows.Forms.TextBox
  Friend WithEvents txtVitesseVéhicule As System.Windows.Forms.TextBox
  Friend WithEvents txtService As System.Windows.Forms.TextBox
  Friend WithEvents txtOrganisme As System.Windows.Forms.TextBox
  Friend WithEvents chkConserverDéfaut As System.Windows.Forms.CheckBox
  Friend WithEvents btnLogo As System.Windows.Forms.Button
  Friend WithEvents lblUvpd As System.Windows.Forms.Label
  Friend WithEvents lblDébitSaturation As System.Windows.Forms.Label
  Friend WithEvents lblService As System.Windows.Forms.Label
  Friend WithEvents lblOrganisme As System.Windows.Forms.Label
  Friend WithEvents lblMSVélos As System.Windows.Forms.Label
  Friend WithEvents lblVélos As System.Windows.Forms.Label
  Friend WithEvents lblMSVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblMSPiétons As System.Windows.Forms.Label
  Friend WithEvents lblPiétons As System.Windows.Forms.Label
  Friend WithEvents lblDossierProjets As System.Windows.Forms.Label
  Friend WithEvents btnParcourir As System.Windows.Forms.Button
  Friend WithEvents lblNomDossier As System.Windows.Forms.Label
  Friend WithEvents grpTemps As System.Windows.Forms.GroupBox
  Friend WithEvents lblTempsPerdu As System.Windows.Forms.Label
  Friend WithEvents lblSecondesPerdu As System.Windows.Forms.Label
  Friend WithEvents chkPiétonsSonore As System.Windows.Forms.CheckBox
  Friend WithEvents lblVertUtile As System.Windows.Forms.Label
  Friend WithEvents lblJauneInUtilisé As System.Windows.Forms.Label
  Friend WithEvents lblEnAgglo As System.Windows.Forms.Label
  Friend WithEvents txtVUAgglo As System.Windows.Forms.TextBox
  Friend WithEvents updJauneAgglo As System.Windows.Forms.NumericUpDown
  Friend WithEvents updJauneCampagne As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblHorsAgglo As System.Windows.Forms.Label
  Friend WithEvents lblSecondesJauneCampagne As System.Windows.Forms.Label
  Friend WithEvents txtVuCampagne As System.Windows.Forms.TextBox
  Friend WithEvents lblPlusAgglo As System.Windows.Forms.Label
  Friend WithEvents lblPlusCampagne As System.Windows.Forms.Label
  Friend WithEvents lblSecondesVertCampagne As System.Windows.Forms.Label
  Friend WithEvents lblSecondesVertAgglo As System.Windows.Forms.Label
  Friend WithEvents lblSecondesJauneAgglo As System.Windows.Forms.Label
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  Friend WithEvents updTempsPerduAgglo As System.Windows.Forms.NumericUpDown
  Friend WithEvents updTempsPerduCampagne As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label1 As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.grpTrafic = New System.Windows.Forms.GroupBox
    Me.lblUvpd = New System.Windows.Forms.Label
    Me.txtDébitSaturation = New System.Windows.Forms.TextBox
    Me.lblDébitSaturation = New System.Windows.Forms.Label
    Me.grpUtilisateur = New System.Windows.Forms.GroupBox
    Me.txtService = New System.Windows.Forms.TextBox
    Me.lblService = New System.Windows.Forms.Label
    Me.txtOrganisme = New System.Windows.Forms.TextBox
    Me.lblOrganisme = New System.Windows.Forms.Label
    Me.lblMSVélos = New System.Windows.Forms.Label
    Me.txtVitesseVélo = New System.Windows.Forms.TextBox
    Me.lblVélos = New System.Windows.Forms.Label
    Me.lblMSVéhicules = New System.Windows.Forms.Label
    Me.txtVitesseVéhicule = New System.Windows.Forms.TextBox
    Me.lblVéhicules = New System.Windows.Forms.Label
    Me.lblMSPiétons = New System.Windows.Forms.Label
    Me.txtVitessePiéton = New System.Windows.Forms.TextBox
    Me.lblPiétons = New System.Windows.Forms.Label
    Me.grpVitesse = New System.Windows.Forms.GroupBox
    Me.chkConserverDéfaut = New System.Windows.Forms.CheckBox
    Me.btnLogo = New System.Windows.Forms.Button
    Me.lblDossierProjets = New System.Windows.Forms.Label
    Me.lblNomDossier = New System.Windows.Forms.Label
    Me.btnParcourir = New System.Windows.Forms.Button
    Me.grpTemps = New System.Windows.Forms.GroupBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.updTempsPerduCampagne = New System.Windows.Forms.NumericUpDown
    Me.lblPlusCampagne = New System.Windows.Forms.Label
    Me.lblPlusAgglo = New System.Windows.Forms.Label
    Me.updJauneCampagne = New System.Windows.Forms.NumericUpDown
    Me.lblHorsAgglo = New System.Windows.Forms.Label
    Me.lblSecondesVertCampagne = New System.Windows.Forms.Label
    Me.lblSecondesJauneCampagne = New System.Windows.Forms.Label
    Me.txtVuCampagne = New System.Windows.Forms.TextBox
    Me.updTempsPerduAgglo = New System.Windows.Forms.NumericUpDown
    Me.updJauneAgglo = New System.Windows.Forms.NumericUpDown
    Me.lblEnAgglo = New System.Windows.Forms.Label
    Me.lblSecondesVertAgglo = New System.Windows.Forms.Label
    Me.lblVertUtile = New System.Windows.Forms.Label
    Me.lblSecondesJauneAgglo = New System.Windows.Forms.Label
    Me.lblJauneInUtilisé = New System.Windows.Forms.Label
    Me.lblSecondesPerdu = New System.Windows.Forms.Label
    Me.txtVUAgglo = New System.Windows.Forms.TextBox
    Me.lblTempsPerdu = New System.Windows.Forms.Label
    Me.chkPiétonsSonore = New System.Windows.Forms.CheckBox
    Me.tipBulle = New System.Windows.Forms.ToolTip(Me.components)
    Me.grpTrafic.SuspendLayout()
    Me.grpUtilisateur.SuspendLayout()
    Me.grpVitesse.SuspendLayout()
    Me.grpTemps.SuspendLayout()
    CType(Me.updTempsPerduCampagne, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.updJauneCampagne, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.updTempsPerduAgglo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.updJauneAgglo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(546, 64)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(546, 104)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(546, 24)
    Me.btnOK.Name = "btnOK"
    '
    'grpTrafic
    '
    Me.grpTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpTrafic.Controls.Add(Me.lblUvpd)
    Me.grpTrafic.Controls.Add(Me.txtDébitSaturation)
    Me.grpTrafic.Controls.Add(Me.lblDébitSaturation)
    Me.grpTrafic.Location = New System.Drawing.Point(368, 360)
    Me.grpTrafic.Name = "grpTrafic"
    Me.grpTrafic.Size = New System.Drawing.Size(168, 96)
    Me.grpTrafic.TabIndex = 6
    Me.grpTrafic.TabStop = False
    Me.grpTrafic.Text = "Trafics"
    '
    'lblUvpd
    '
    Me.lblUvpd.Location = New System.Drawing.Point(56, 56)
    Me.lblUvpd.Name = "lblUvpd"
    Me.lblUvpd.Size = New System.Drawing.Size(48, 16)
    Me.lblUvpd.TabIndex = 2
    Me.lblUvpd.Text = "uvpd/h"
    '
    'txtDébitSaturation
    '
    Me.txtDébitSaturation.Location = New System.Drawing.Point(16, 56)
    Me.txtDébitSaturation.Name = "txtDébitSaturation"
    Me.txtDébitSaturation.Size = New System.Drawing.Size(32, 20)
    Me.txtDébitSaturation.TabIndex = 1
    Me.txtDébitSaturation.Text = "1800"
    Me.txtDébitSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblDébitSaturation
    '
    Me.lblDébitSaturation.Location = New System.Drawing.Point(8, 32)
    Me.lblDébitSaturation.Name = "lblDébitSaturation"
    Me.lblDébitSaturation.Size = New System.Drawing.Size(112, 16)
    Me.lblDébitSaturation.TabIndex = 0
    Me.lblDébitSaturation.Text = "Débit de saturation :"
    '
    'grpUtilisateur
    '
    Me.grpUtilisateur.Controls.Add(Me.txtService)
    Me.grpUtilisateur.Controls.Add(Me.lblService)
    Me.grpUtilisateur.Controls.Add(Me.txtOrganisme)
    Me.grpUtilisateur.Controls.Add(Me.lblOrganisme)
    Me.grpUtilisateur.Location = New System.Drawing.Point(16, 112)
    Me.grpUtilisateur.Name = "grpUtilisateur"
    Me.grpUtilisateur.Size = New System.Drawing.Size(520, 104)
    Me.grpUtilisateur.TabIndex = 2
    Me.grpUtilisateur.TabStop = False
    Me.grpUtilisateur.Text = "Utilisateur"
    '
    'txtService
    '
    Me.txtService.Location = New System.Drawing.Point(120, 64)
    Me.txtService.Name = "txtService"
    Me.txtService.Size = New System.Drawing.Size(368, 20)
    Me.txtService.TabIndex = 3
    Me.txtService.Text = "Gestion du Trafic et Télématique"
    '
    'lblService
    '
    Me.lblService.Location = New System.Drawing.Point(24, 64)
    Me.lblService.Name = "lblService"
    Me.lblService.Size = New System.Drawing.Size(72, 24)
    Me.lblService.TabIndex = 2
    Me.lblService.Text = "Service :"
    '
    'txtOrganisme
    '
    Me.txtOrganisme.Location = New System.Drawing.Point(120, 24)
    Me.txtOrganisme.Name = "txtOrganisme"
    Me.txtOrganisme.Size = New System.Drawing.Size(368, 20)
    Me.txtOrganisme.TabIndex = 1
    Me.txtOrganisme.Text = "CERTU"
    '
    'lblOrganisme
    '
    Me.lblOrganisme.Location = New System.Drawing.Point(24, 24)
    Me.lblOrganisme.Name = "lblOrganisme"
    Me.lblOrganisme.Size = New System.Drawing.Size(72, 24)
    Me.lblOrganisme.TabIndex = 0
    Me.lblOrganisme.Text = "Organisme :"
    '
    'lblMSVélos
    '
    Me.lblMSVélos.Location = New System.Drawing.Point(232, 56)
    Me.lblMSVélos.Name = "lblMSVélos"
    Me.lblMSVélos.Size = New System.Drawing.Size(24, 24)
    Me.lblMSVélos.TabIndex = 25
    Me.lblMSVélos.Text = "m/s"
    '
    'txtVitesseVélo
    '
    Me.txtVitesseVélo.Location = New System.Drawing.Point(200, 56)
    Me.txtVitesseVélo.Name = "txtVitesseVélo"
    Me.txtVitesseVélo.Size = New System.Drawing.Size(24, 20)
    Me.txtVitesseVélo.TabIndex = 24
    Me.txtVitesseVélo.Text = "10"
    Me.txtVitesseVélo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVélos
    '
    Me.lblVélos.Location = New System.Drawing.Point(200, 32)
    Me.lblVélos.Name = "lblVélos"
    Me.lblVélos.Size = New System.Drawing.Size(40, 16)
    Me.lblVélos.TabIndex = 23
    Me.lblVélos.Text = "Vélos :"
    '
    'lblMSVéhicules
    '
    Me.lblMSVéhicules.Location = New System.Drawing.Point(144, 56)
    Me.lblMSVéhicules.Name = "lblMSVéhicules"
    Me.lblMSVéhicules.Size = New System.Drawing.Size(24, 24)
    Me.lblMSVéhicules.TabIndex = 22
    Me.lblMSVéhicules.Text = "m/s"
    '
    'txtVitesseVéhicule
    '
    Me.txtVitesseVéhicule.Location = New System.Drawing.Point(112, 56)
    Me.txtVitesseVéhicule.Name = "txtVitesseVéhicule"
    Me.txtVitesseVéhicule.Size = New System.Drawing.Size(24, 20)
    Me.txtVitesseVéhicule.TabIndex = 21
    Me.txtVitesseVéhicule.Text = "10"
    Me.txtVitesseVéhicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVéhicules
    '
    Me.lblVéhicules.Location = New System.Drawing.Point(112, 32)
    Me.lblVéhicules.Name = "lblVéhicules"
    Me.lblVéhicules.Size = New System.Drawing.Size(64, 16)
    Me.lblVéhicules.TabIndex = 20
    Me.lblVéhicules.Text = "Véhicules :"
    '
    'lblMSPiétons
    '
    Me.lblMSPiétons.Location = New System.Drawing.Point(48, 56)
    Me.lblMSPiétons.Name = "lblMSPiétons"
    Me.lblMSPiétons.Size = New System.Drawing.Size(24, 24)
    Me.lblMSPiétons.TabIndex = 19
    Me.lblMSPiétons.Text = "m/s"
    '
    'txtVitessePiéton
    '
    Me.txtVitessePiéton.Location = New System.Drawing.Point(16, 56)
    Me.txtVitessePiéton.Name = "txtVitessePiéton"
    Me.txtVitessePiéton.Size = New System.Drawing.Size(24, 20)
    Me.txtVitessePiéton.TabIndex = 18
    Me.txtVitessePiéton.Text = "1"
    Me.txtVitessePiéton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblPiétons
    '
    Me.lblPiétons.Location = New System.Drawing.Point(16, 32)
    Me.lblPiétons.Name = "lblPiétons"
    Me.lblPiétons.Size = New System.Drawing.Size(56, 16)
    Me.lblPiétons.TabIndex = 17
    Me.lblPiétons.Text = "Piétons :"
    '
    'grpVitesse
    '
    Me.grpVitesse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpVitesse.Controls.Add(Me.lblMSVélos)
    Me.grpVitesse.Controls.Add(Me.txtVitesseVélo)
    Me.grpVitesse.Controls.Add(Me.lblVélos)
    Me.grpVitesse.Controls.Add(Me.lblMSVéhicules)
    Me.grpVitesse.Controls.Add(Me.txtVitesseVéhicule)
    Me.grpVitesse.Controls.Add(Me.lblVéhicules)
    Me.grpVitesse.Controls.Add(Me.lblMSPiétons)
    Me.grpVitesse.Controls.Add(Me.txtVitessePiéton)
    Me.grpVitesse.Controls.Add(Me.lblPiétons)
    Me.grpVitesse.Location = New System.Drawing.Point(16, 360)
    Me.grpVitesse.Name = "grpVitesse"
    Me.grpVitesse.Size = New System.Drawing.Size(272, 96)
    Me.grpVitesse.TabIndex = 5
    Me.grpVitesse.TabStop = False
    Me.grpVitesse.Text = "Vitesses de dégagement"
    '
    'chkConserverDéfaut
    '
    Me.chkConserverDéfaut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkConserverDéfaut.Location = New System.Drawing.Point(16, 506)
    Me.chkConserverDéfaut.Name = "chkConserverDéfaut"
    Me.chkConserverDéfaut.Size = New System.Drawing.Size(216, 24)
    Me.chkConserverDéfaut.TabIndex = 10
    Me.chkConserverDéfaut.Text = "Conserver comme valeurs par défaut"
    '
    'btnLogo
    '
    Me.btnLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnLogo.Location = New System.Drawing.Point(24, 16)
    Me.btnLogo.Name = "btnLogo"
    Me.btnLogo.Size = New System.Drawing.Size(88, 80)
    Me.btnLogo.TabIndex = 23
    Me.btnLogo.Text = "Logo ..."
    '
    'lblDossierProjets
    '
    Me.lblDossierProjets.Location = New System.Drawing.Point(136, 40)
    Me.lblDossierProjets.Name = "lblDossierProjets"
    Me.lblDossierProjets.Size = New System.Drawing.Size(144, 24)
    Me.lblDossierProjets.TabIndex = 24
    Me.lblDossierProjets.Text = "Emplacement des projets :"
    '
    'lblNomDossier
    '
    Me.lblNomDossier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblNomDossier.Location = New System.Drawing.Point(136, 80)
    Me.lblNomDossier.Name = "lblNomDossier"
    Me.lblNomDossier.Size = New System.Drawing.Size(392, 16)
    Me.lblNomDossier.TabIndex = 26
    '
    'btnParcourir
    '
    Me.btnParcourir.Location = New System.Drawing.Point(272, 32)
    Me.btnParcourir.Name = "btnParcourir"
    Me.btnParcourir.Size = New System.Drawing.Size(96, 24)
    Me.btnParcourir.TabIndex = 25
    Me.btnParcourir.Text = "Parcourir..."
    '
    'grpTemps
    '
    Me.grpTemps.Controls.Add(Me.Label1)
    Me.grpTemps.Controls.Add(Me.updTempsPerduCampagne)
    Me.grpTemps.Controls.Add(Me.lblPlusCampagne)
    Me.grpTemps.Controls.Add(Me.lblPlusAgglo)
    Me.grpTemps.Controls.Add(Me.updJauneCampagne)
    Me.grpTemps.Controls.Add(Me.lblHorsAgglo)
    Me.grpTemps.Controls.Add(Me.lblSecondesVertCampagne)
    Me.grpTemps.Controls.Add(Me.lblSecondesJauneCampagne)
    Me.grpTemps.Controls.Add(Me.txtVuCampagne)
    Me.grpTemps.Controls.Add(Me.updTempsPerduAgglo)
    Me.grpTemps.Controls.Add(Me.updJauneAgglo)
    Me.grpTemps.Controls.Add(Me.lblEnAgglo)
    Me.grpTemps.Controls.Add(Me.lblSecondesVertAgglo)
    Me.grpTemps.Controls.Add(Me.lblVertUtile)
    Me.grpTemps.Controls.Add(Me.lblSecondesJauneAgglo)
    Me.grpTemps.Controls.Add(Me.lblJauneInUtilisé)
    Me.grpTemps.Controls.Add(Me.lblSecondesPerdu)
    Me.grpTemps.Controls.Add(Me.txtVUAgglo)
    Me.grpTemps.Controls.Add(Me.lblTempsPerdu)
    Me.grpTemps.Location = New System.Drawing.Point(16, 232)
    Me.grpTemps.Name = "grpTemps"
    Me.grpTemps.Size = New System.Drawing.Size(520, 112)
    Me.grpTemps.TabIndex = 27
    Me.grpTemps.TabStop = False
    Me.grpTemps.Text = "Calcul du temps perdu"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(192, 80)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(64, 24)
    Me.Label1.TabIndex = 52
    Me.Label1.Text = "secondes"
    '
    'updTempsPerduCampagne
    '
    Me.updTempsPerduCampagne.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.updTempsPerduCampagne.Location = New System.Drawing.Point(160, 80)
    Me.updTempsPerduCampagne.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
    Me.updTempsPerduCampagne.Name = "updTempsPerduCampagne"
    Me.updTempsPerduCampagne.ReadOnly = True
    Me.updTempsPerduCampagne.Size = New System.Drawing.Size(32, 20)
    Me.updTempsPerduCampagne.TabIndex = 51
    Me.updTempsPerduCampagne.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblPlusCampagne
    '
    Me.lblPlusCampagne.Location = New System.Drawing.Point(408, 80)
    Me.lblPlusCampagne.Name = "lblPlusCampagne"
    Me.lblPlusCampagne.Size = New System.Drawing.Size(16, 16)
    Me.lblPlusCampagne.TabIndex = 50
    Me.lblPlusCampagne.Text = "+"
    '
    'lblPlusAgglo
    '
    Me.lblPlusAgglo.Location = New System.Drawing.Point(408, 56)
    Me.lblPlusAgglo.Name = "lblPlusAgglo"
    Me.lblPlusAgglo.Size = New System.Drawing.Size(16, 16)
    Me.lblPlusAgglo.TabIndex = 49
    Me.lblPlusAgglo.Text = "+"
    '
    'updJauneCampagne
    '
    Me.updJauneCampagne.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.updJauneCampagne.Location = New System.Drawing.Point(288, 80)
    Me.updJauneCampagne.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
    Me.updJauneCampagne.Name = "updJauneCampagne"
    Me.updJauneCampagne.ReadOnly = True
    Me.updJauneCampagne.Size = New System.Drawing.Size(32, 20)
    Me.updJauneCampagne.TabIndex = 48
    Me.updJauneCampagne.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblHorsAgglo
    '
    Me.lblHorsAgglo.Location = New System.Drawing.Point(16, 88)
    Me.lblHorsAgglo.Name = "lblHorsAgglo"
    Me.lblHorsAgglo.Size = New System.Drawing.Size(112, 16)
    Me.lblHorsAgglo.TabIndex = 47
    Me.lblHorsAgglo.Text = "Hors agglomération"
    '
    'lblSecondesVertCampagne
    '
    Me.lblSecondesVertCampagne.Location = New System.Drawing.Point(456, 80)
    Me.lblSecondesVertCampagne.Name = "lblSecondesVertCampagne"
    Me.lblSecondesVertCampagne.Size = New System.Drawing.Size(56, 24)
    Me.lblSecondesVertCampagne.TabIndex = 46
    Me.lblSecondesVertCampagne.Text = "s"
    '
    'lblSecondesJauneCampagne
    '
    Me.lblSecondesJauneCampagne.Location = New System.Drawing.Point(328, 80)
    Me.lblSecondesJauneCampagne.Name = "lblSecondesJauneCampagne"
    Me.lblSecondesJauneCampagne.Size = New System.Drawing.Size(72, 24)
    Me.lblSecondesJauneCampagne.TabIndex = 45
    Me.lblSecondesJauneCampagne.Text = "secondes"
    '
    'txtVuCampagne
    '
    Me.txtVuCampagne.Location = New System.Drawing.Point(424, 80)
    Me.txtVuCampagne.MaxLength = 1
    Me.txtVuCampagne.Name = "txtVuCampagne"
    Me.txtVuCampagne.ReadOnly = True
    Me.txtVuCampagne.Size = New System.Drawing.Size(24, 20)
    Me.txtVuCampagne.TabIndex = 44
    Me.txtVuCampagne.Text = "0"
    Me.txtVuCampagne.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'updTempsPerduAgglo
    '
    Me.updTempsPerduAgglo.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.updTempsPerduAgglo.Location = New System.Drawing.Point(160, 56)
    Me.updTempsPerduAgglo.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
    Me.updTempsPerduAgglo.Name = "updTempsPerduAgglo"
    Me.updTempsPerduAgglo.ReadOnly = True
    Me.updTempsPerduAgglo.Size = New System.Drawing.Size(32, 20)
    Me.updTempsPerduAgglo.TabIndex = 43
    Me.updTempsPerduAgglo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'updJauneAgglo
    '
    Me.updJauneAgglo.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.updJauneAgglo.Location = New System.Drawing.Point(288, 56)
    Me.updJauneAgglo.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
    Me.updJauneAgglo.Name = "updJauneAgglo"
    Me.updJauneAgglo.ReadOnly = True
    Me.updJauneAgglo.Size = New System.Drawing.Size(32, 20)
    Me.updJauneAgglo.TabIndex = 42
    Me.updJauneAgglo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblEnAgglo
    '
    Me.lblEnAgglo.Location = New System.Drawing.Point(16, 56)
    Me.lblEnAgglo.Name = "lblEnAgglo"
    Me.lblEnAgglo.Size = New System.Drawing.Size(112, 16)
    Me.lblEnAgglo.TabIndex = 41
    Me.lblEnAgglo.Text = "En agglomération"
    '
    'lblSecondesVertAgglo
    '
    Me.lblSecondesVertAgglo.Location = New System.Drawing.Point(456, 56)
    Me.lblSecondesVertAgglo.Name = "lblSecondesVertAgglo"
    Me.lblSecondesVertAgglo.Size = New System.Drawing.Size(56, 24)
    Me.lblSecondesVertAgglo.TabIndex = 40
    Me.lblSecondesVertAgglo.Text = "secondes"
    '
    'lblVertUtile
    '
    Me.lblVertUtile.Location = New System.Drawing.Point(408, 16)
    Me.lblVertUtile.Name = "lblVertUtile"
    Me.lblVertUtile.Size = New System.Drawing.Size(72, 32)
    Me.lblVertUtile.TabIndex = 39
    Me.lblVertUtile.Text = "Vert utile = vert réel"
    '
    'lblSecondesJauneAgglo
    '
    Me.lblSecondesJauneAgglo.Location = New System.Drawing.Point(328, 56)
    Me.lblSecondesJauneAgglo.Name = "lblSecondesJauneAgglo"
    Me.lblSecondesJauneAgglo.Size = New System.Drawing.Size(72, 24)
    Me.lblSecondesJauneAgglo.TabIndex = 24
    Me.lblSecondesJauneAgglo.Text = "secondes"
    '
    'lblJauneInUtilisé
    '
    Me.lblJauneInUtilisé.Location = New System.Drawing.Point(280, 16)
    Me.lblJauneInUtilisé.Name = "lblJauneInUtilisé"
    Me.lblJauneInUtilisé.Size = New System.Drawing.Size(80, 32)
    Me.lblJauneInUtilisé.TabIndex = 22
    Me.lblJauneInUtilisé.Text = "Temps de jaune inutilisé"
    '
    'lblSecondesPerdu
    '
    Me.lblSecondesPerdu.Location = New System.Drawing.Point(192, 56)
    Me.lblSecondesPerdu.Name = "lblSecondesPerdu"
    Me.lblSecondesPerdu.Size = New System.Drawing.Size(64, 24)
    Me.lblSecondesPerdu.TabIndex = 21
    Me.lblSecondesPerdu.Text = "secondes"
    '
    'txtVUAgglo
    '
    Me.txtVUAgglo.Location = New System.Drawing.Point(424, 56)
    Me.txtVUAgglo.MaxLength = 1
    Me.txtVUAgglo.Name = "txtVUAgglo"
    Me.txtVUAgglo.ReadOnly = True
    Me.txtVUAgglo.Size = New System.Drawing.Size(24, 20)
    Me.txtVUAgglo.TabIndex = 20
    Me.txtVUAgglo.Text = "0"
    Me.txtVUAgglo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsPerdu
    '
    Me.lblTempsPerdu.Location = New System.Drawing.Point(160, 16)
    Me.lblTempsPerdu.Name = "lblTempsPerdu"
    Me.lblTempsPerdu.Size = New System.Drawing.Size(84, 36)
    Me.lblTempsPerdu.TabIndex = 0
    Me.lblTempsPerdu.Text = "Temps perdu au démarrage"
    '
    'chkPiétonsSonore
    '
    Me.chkPiétonsSonore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkPiétonsSonore.Checked = True
    Me.chkPiétonsSonore.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkPiétonsSonore.Location = New System.Drawing.Point(16, 480)
    Me.chkPiétonsSonore.Name = "chkPiétonsSonore"
    Me.chkPiétonsSonore.Size = New System.Drawing.Size(248, 16)
    Me.chkPiétonsSonore.TabIndex = 28
    Me.chkPiétonsSonore.Text = "Signal piétons sonore pour les malvoyants"
    Me.tipBulle.SetToolTip(Me.chkPiétonsSonore, "Indique si les passages piétons sont équipés de signaux lumineux R12 avc disposit" & _
    "if sonore pour personnes aveugles et malvoyantes")
    '
    'tipBulle
    '
    Me.tipBulle.AutoPopDelay = 10000
    Me.tipBulle.InitialDelay = 500
    Me.tipBulle.ReshowDelay = 100
    '
    'dlgParamétrage
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(642, 543)
    Me.Controls.Add(Me.chkPiétonsSonore)
    Me.Controls.Add(Me.lblNomDossier)
    Me.Controls.Add(Me.btnParcourir)
    Me.Controls.Add(Me.lblDossierProjets)
    Me.Controls.Add(Me.btnLogo)
    Me.Controls.Add(Me.chkConserverDéfaut)
    Me.Controls.Add(Me.grpTrafic)
    Me.Controls.Add(Me.grpVitesse)
    Me.Controls.Add(Me.grpUtilisateur)
    Me.Controls.Add(Me.grpTemps)
    Me.Name = "dlgParamétrage"
    Me.Text = "Paramétrage"
    Me.Controls.SetChildIndex(Me.grpTemps, 0)
    Me.Controls.SetChildIndex(Me.grpUtilisateur, 0)
    Me.Controls.SetChildIndex(Me.grpVitesse, 0)
    Me.Controls.SetChildIndex(Me.grpTrafic, 0)
    Me.Controls.SetChildIndex(Me.chkConserverDéfaut, 0)
    Me.Controls.SetChildIndex(Me.btnLogo, 0)
    Me.Controls.SetChildIndex(Me.lblDossierProjets, 0)
    Me.Controls.SetChildIndex(Me.btnParcourir, 0)
    Me.Controls.SetChildIndex(Me.lblNomDossier, 0)
    Me.Controls.SetChildIndex(Me.chkPiétonsSonore, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.grpTrafic.ResumeLayout(False)
    Me.grpUtilisateur.ResumeLayout(False)
    Me.grpVitesse.ResumeLayout(False)
    Me.grpTemps.ResumeLayout(False)
    CType(Me.updTempsPerduCampagne, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.updJauneCampagne, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.updTempsPerduAgglo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.updJauneAgglo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public mParamètres As Paramètres
  Public mObjetMétier As Métier
  Public mSignalPiétonChangé As Boolean
  Private CheminLogo As String
  Private myFileInfo As New IO.FileInfo(NomExe)
  Private CheminStockage As String = myFileInfo.DirectoryName
  Private mBitmapLogo As Bitmap
  Private mTailleBoutonLogo As Size
  Private ChargementEnCours As Boolean

  '******************************************************************************
  ' Fermeture de la feuille
  '******************************************************************************
  Private Sub Paramétrage_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Dim sParamètres As Paramètres

    Select Case DialogResult
      Case DialogResult.Cancel
        e.Cancel = VeriModif()

      Case DialogResult.OK

        If Me.txtOrganisme.Text.Length = 0 Then
          AfficherMessageErreur(Me, "Organisme obligatoire")
          e.Cancel = True
          txtOrganisme.Focus()

        ElseIf Me.txtService.Text.Length = 0 Then
          Me.DialogResult = DialogResult.None
          AfficherMessageErreur(Me, "Service obligatoire")
          txtService.Focus()

        ElseIf VarianteTropAvancée() Then

          e.Cancel = True

        Else
          ' Récupérer les valeurs saisies

          With sParamètres
            .VersionFichier = [Global].VersionFichier
            .Organisme = Me.txtOrganisme.Text
            .Service = Me.txtService.Text
            .CheminStockage = Me.lblNomDossier.Text
            cndCheminStockage = .CheminStockage
            .CheminLogo = Me.CheminLogo
            .DébitSaturation = CType(Me.txtDébitSaturation.Text, Short)
            .VitessePiétons = CType(Me.txtVitessePiéton.Text, Single)
            .VitesseVéhicules = CType(Me.txtVitesseVéhicule.Text, Single)
            .VitesseVélos = CType(Me.txtVitesseVélo.Text, Single)
            'v12 et antérieures
            '.DécalageVertUtile = Me.updDécalageVertUtile.Value
            'v13
            .TempsPerduDémarrageAgglo = Me.updTempsPerduAgglo.Value
            .TempsPerduDémarrageCampagne = Me.updTempsPerduCampagne.Value
            .TempsJauneInutiliséAgglo = Me.updJauneAgglo.Value
            .TempsJauneInutiliséCampagne = Me.updJauneCampagne.Value
            If mParamètres.SignalPiétonsSonore <> Me.chkPiétonsSonore.Checked Then
              mSignalPiétonChangé = True
              .SignalPiétonsSonore = Me.chkPiétonsSonore.Checked
            End If
          End With

          'Affecter les nouvelles valeurs aux paramètres initiaux
          mParamètres = sParamètres

          If Me.chkConserverDéfaut.Checked Then
            'Définir les nouvelles valeurs par défaut
            cndParamètres = mParamètres
          End If

          If Not IsNothing(cndVariante) Then
            'Définir les nouvelles valeurs de paramétrage de la variante
            cndVariante.Param = mParamètres
          End If
        End If
    End Select

  End Sub

  Private Function VarianteTropAvancée() As Boolean
    Dim Alerte As Boolean
    Dim sParamètres As Paramètres
    Dim Message As String

    ' Récupérer les valeurs saisies
    If Not IsNothing(cndVariante) AndAlso cndVariante.Verrou >= [Global].Verrouillage.LignesFeux Then

      With sParamètres
        .DébitSaturation = CType(Me.txtDébitSaturation.Text, Short)
        .VitessePiétons = CType(Me.txtVitessePiéton.Text, Single)
        .VitesseVéhicules = CType(Me.txtVitesseVéhicule.Text, Single)
        .TempsPerduDémarrageAgglo = Me.updTempsPerduAgglo.Value
        .TempsPerduDémarrageCampagne = Me.updTempsPerduCampagne.Value
        .TempsJauneInutiliséAgglo = Me.updJauneAgglo.Value
        .TempsJauneInutiliséCampagne = Me.updJauneCampagne.Value

        If .VitessePiétons <> mParamètres.VitessePiétons Or .VitesseVéhicules <> mParamètres.VitesseVéhicules AndAlso _
        cndVariante.UnPhasageRetenu AndAlso cndVariante.ModeGraphique Then
          Message = "La modification des vitesses de dégagement va réinitialiser les temps de rouge de dégagement"
          Message &= vbCrLf & "Ceci conduit à redéfinir l'organisation du phasage"
          Alerte = True
          mObjetMétier = cndVariante.PremierPlanBaseRetenu

        ElseIf .DébitSaturation <> mParamètres.DébitSaturation AndAlso cndVariante.DiagnosticCalculé Then
          Message = "La modification du débit de saturation influe sur la capacité du carrefour"
          If .DébitSaturation < mParamètres.DébitSaturation Then
            Message &= vbCrLf & "Le carrefour est susceptible de ne plus fonctionner"
            Alerte = True
          Else
            Message &= vbCrLf & "Ceci conduit à recalculer les réserves de capacité et les temps d'attente"
          End If
          mObjetMétier = cndVariante.PremierPlanFonctionnement

        ElseIf .TempsPerduDémarrageAgglo <> mParamètres.TempsPerduDémarrageAgglo Or _
                .TempsPerduDémarrageCampagne <> mParamètres.TempsPerduDémarrageCampagne Or _
                .TempsJauneInutiliséAgglo <> mParamètres.TempsJauneInutiliséAgglo Or _
                .TempsJauneInutiliséCampagne <> mParamètres.TempsJauneInutiliséCampagne Then
          If cndVariante.DiagnosticCalculé Then
            Message = "La modification des paramètres de calcul du temps perdu influe sur la capacité du carrefour"
            Message &= vbCrLf & "Ceci conduit à recalculer les réserves de capacité et les temps d'attente"
          End If
          mObjetMétier = cndVariante.PremierPlanFonctionnement

        End If

        If Not IsNothing(Message) Then
          Return Not Confirmation(Message, Critique:=Alerte)
        End If
      End With

    End If

  End Function

  '******************************************************************************
  ' Chargement de la feuille
  '******************************************************************************
  Private Sub Paramétrage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MDIDiagfeux))

    ChargementEnCours = True

    'Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Icon = Nothing
    '	Me.Icon = MDIDiagfeux.Icon

    'Mémoriser la taille initiale du bouton Logo
    mTailleBoutonLogo = Me.btnLogo.Size

    If IsNothing(cndVariante) Then
      ' Afficher les valeurs par défaut
      mParamètres = cndParamètres

      If IsNothing(cndParamètres.Service) Then
        '1ère utilisation de  DIAGFEUX
        'c'est d'ailleurs discutable : on pourrait considérer que la modification des paramètres généraux est forcément à conserver
        Me.chkConserverDéfaut.Visible = False
      End If
      Me.chkConserverDéfaut.Checked = True

    Else
      ' Afficher les valeurs de la variante
      mParamètres = cndVariante.Param
      If cndVariante.EnAgglo Then
        Me.lblHorsAgglo.Visible = False
        Me.updJauneCampagne.Visible = False
        Me.lblSecondesJauneCampagne.Visible = False
        Me.lblPlusCampagne.Visible = False
        Me.txtVuCampagne.Visible = False
        Me.lblSecondesVertCampagne.Visible = False
      Else
        Me.lblEnAgglo.Visible = False
        Me.updJauneAgglo.Visible = False
        Me.lblSecondesJauneAgglo.Visible = False
        Me.lblPlusAgglo.Visible = False
        Me.txtVUAgglo.Visible = False
        Me.lblSecondesVertAgglo.Visible = False
        Me.lblHorsAgglo.Top = Me.lblEnAgglo.Top
        Me.updJauneCampagne.Top = Me.updJauneAgglo.Top
        Me.lblSecondesJauneCampagne.Top = Me.lblSecondesJauneAgglo.Top
        Me.lblPlusCampagne.Top = Me.lblPlusAgglo.Top
        Me.txtVuCampagne.Top = Me.txtVUAgglo.Top
        Me.lblSecondesVertCampagne.Top = Me.lblSecondesVertAgglo.Top
      End If
    End If

    With mParamètres
      Me.txtOrganisme.Text = .Organisme
      Me.txtService.Text = .Service
      If Not IsNothing(.CheminStockage) Then
        CheminStockage = .CheminStockage
      End If
      Me.lblNomDossier.Text = CheminStockage
      CheminLogo = ImageRaster.FichierExistant(.CheminLogo)
      txtVitessePiéton.Text = CType(.VitessePiétons, String)
      Me.txtVitesseVéhicule.Text = CType(.VitesseVéhicules, String)
      Me.txtVitesseVélo.Text = CType(.VitesseVélos, String)
      Me.txtDébitSaturation.Text = CType(.DébitSaturation, String)
      'v12 et antérieures
      'Me.updDécalageVertUtile.Value = .DécalageVertUtile
      'v13 
      Me.updTempsPerduAgglo.Value = .TempsPerduDémarrageAgglo
      Me.updTempsPerduCampagne.Value = .TempsPerduDémarrageCampagne
      Me.updJauneAgglo.Value = .TempsJauneInutiliséAgglo
      Me.updJauneCampagne.Value = .TempsJauneInutiliséCampagne
      Me.chkPiétonsSonore.Checked = .SignalPiétonsSonore
    End With

    Me.updTempsPerduAgglo.Maximum = [Global].PerteAuDémarrageMax
    Me.updTempsPerduCampagne.Maximum = [Global].PerteAuDémarrageMax
    Me.updJauneAgglo.Maximum = [Global].JauneAgglo
    Me.updJauneCampagne.Maximum = [Global].JauneCampagne

    ChargementEnCours = False
  End Sub

  '******************************************************************************
  ' Evènements de gestion de la saisie
  '******************************************************************************

  Private Sub txtVitessePiéton_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVitessePiéton.KeyPress, txtVitesseVéhicule.KeyPress, txtVitesseVélo.KeyPress, txtDébitSaturation.KeyPress, txtVUAgglo.KeyPress

    Dim txt As TextBox
    Dim Entier As Boolean

    txt = sender
    If txt Is Me.txtDébitSaturation Then
      Entier = True
    End If

    e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=Entier)

  End Sub

  Private Sub txtVitessePiéton_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtVitessePiéton.Validating, txtVitesseVéhicule.Validating, txtVitesseVélo.Validating, txtDébitSaturation.Validating

    Dim txt As TextBox = sender
    Dim vMini, vMaxi, Donnée As Double
    Dim chaine As String = txt.Text

    Try

      If txt Is txtVitessePiéton Then
        vMini = 0.1
        vMaxi = 1
        Donnée = mParamètres.VitessePiétons
      ElseIf txt Is txtVitesseVéhicule Then
        vMini = 1
        vMaxi = 10
        Donnée = mParamètres.VitesseVéhicules
      ElseIf txt Is txtVitesseVélo Then
        vMini = 1
        vMaxi = 10
        Donnée = mParamètres.VitesseVélos
      End If

      If Not IsNumeric(txt.Text) Then
        MsgBox("Saisie incorrecte")
        e.Cancel = True
      ElseIf Not txt Is txtDébitSaturation Then
        'Pas de controle particulier pour le débit de saturation (?)
        e.Cancel = ControlerBornes(Me, vMini, vMaxi, txt, Donnée, unFormat:="0.0")
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try

  End Sub

  Private Sub btnLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogo.Click
    Dim NomFichier As String = CheminLogo
    Dim Filtre As String = ImageRaster.Filtre '"Fichiers image (*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG)|*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG"
    Dim DefaultExt As String = "jpg"

    If IsNothing(NomFichier) Then
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt)
    Else
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt, InfoFichier:=New IO.FileInfo(mParamètres.CheminLogo))
    End If

    If Not IsNothing(NomFichier) Then
      CheminLogo = NomFichier
      'Obliger à rafraichir l'image du bouton logo
      mBitmapLogo = Nothing
    End If

  End Sub

  Private Sub btnLogo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles btnLogo.Paint
    Dim gr As Graphics = e.Graphics

    Try

      If IsNothing(mBitmapLogo) Then   '1er appel de Paint pour cette bitmap
        Dim unTampon As Graphics
        Dim uneImage As ImageRaster


        If Not IsNothing(CheminLogo) Then
          Me.btnLogo.Size = mTailleBoutonLogo
          uneImage = New ImageRaster(CheminLogo, Me.btnLogo.Size, New Point(0, 0))
        End If

        ' Associer une Image Bitmap tampon à un objet Graphics tampon
        mBitmapLogo = Graphique.AssocierBitmapGraphics(Me.btnLogo.Size, gr, unTampon)

        If Not IsNothing(uneImage) Then
          uneImage.Dessiner(unTampon, gr)
        End If
      End If

      ' Dessiner l'image tampon
      gr.DrawImage(mBitmapLogo, 0, 0)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try


  End Sub

  Private Sub btnParcourir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParcourir.Click
    Me.lblNomDossier.Text = DialogueDossier(Me.lblNomDossier.Text)
  End Sub

  Private Sub updJauneAgglo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updJauneAgglo.ValueChanged, updJauneCampagne.ValueChanged, updTempsPerduAgglo.ValueChanged, updTempsPerduCampagne.ValueChanged
    Dim TempsPerduAgglo As Short = Me.updTempsPerduAgglo.Value
    Dim TempsPerduCampagne As Short = Me.updTempsPerduCampagne.Value
    Dim DécalageVertUtile As Short

    DécalageVertUtile = [Global].JauneAgglo - (TempsPerduAgglo + Me.updJauneAgglo.Value)
    Me.txtVUAgglo.Text = CType(Math.Abs(DécalageVertUtile), String)
    Select Case Math.Sign(DécalageVertUtile)
      Case 0
        Me.lblPlusAgglo.Text = ""
      Case 1
        Me.lblPlusAgglo.Text = "+"
      Case -1
        Me.lblPlusAgglo.Text = "-"
    End Select

    DécalageVertUtile = [Global].JauneCampagne - (TempsPerduCampagne + Me.updJauneCampagne.Value)
    Me.txtVuCampagne.Text = CType(Math.Abs(DécalageVertUtile), String)
    Select Case Math.Sign(DécalageVertUtile)
      Case 0
        Me.lblPlusCampagne.Text = ""
      Case 1
        Me.lblPlusCampagne.Text = "+"
      Case -1
        Me.lblPlusCampagne.Text = "-"
    End Select

  End Sub

  Private Sub chkPiétonsSonore_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPiétonsSonore.CheckedChanged
    Dim Message As String

    If Not chkPiétonsSonore.Checked AndAlso Not ChargementEnCours Then
      Message = "Attention,les R12 avec dispositif sonore sont sélectionnés par défaut,car ceux-ci sont obligatoires. (décrets 99-756 et 99-757 du 13/07/1999)" & vbCrLf & vbCrLf
      Message &= "Vous ne pouvez désélectionner ces signaux que si le carrefour est trop compliqué ou si les passages piétons ne peuvent être que trop proches les uns des autres pour que les sont issus des différents dispositifs puissent être distingués." & vbCrLf
      Message &= "Car dans ces cas le carrefour ne remplit pas les conditions de sécurité nécessaires pour les personnes aveugles et malvoyantes. On préférera alors les répéteurs tactiles."

      MessageBox.Show(Message, "Signal piétons sonore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End If

  End Sub

  Private Sub dlgParamétrage_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_PARAMETRAGE
  End Sub
End Class
