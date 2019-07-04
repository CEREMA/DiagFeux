'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : dlgParam�trage.vb																						'
'						Saisie des param�tres du site 																		'
'																																							'
'						Classe																									'
'							dlgParam�trage																											'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe dlgParam�trage --------------------------
'Dialogue pour saisie du param�trage du poste : donn�es personnalis�es
'=====================================================================================================
Public Class dlgParam�trage
  Inherits DiagFeux.frmDlg

#Region " Code g�n�r� par le Concepteur Windows Form "

  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()

    'Ajoutez une initialisation quelconque apr�s l'appel InitializeComponent()

  End Sub

  'La m�thode substitu�e Dispose du formulaire pour nettoyer la liste des composants.
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

  'REMARQUE�: la proc�dure suivante est requise par le Concepteur Windows Form
  'Elle peut �tre modifi�e en utilisant le Concepteur Windows Form.  
  'Ne la modifiez pas en utilisant l'�diteur de code.
  Friend WithEvents grpUtilisateur As System.Windows.Forms.GroupBox
  Friend WithEvents grpTrafic As System.Windows.Forms.GroupBox
  Friend WithEvents grpVitesse As System.Windows.Forms.GroupBox
  Friend WithEvents txtVitessePi�ton As System.Windows.Forms.TextBox
  Friend WithEvents txtD�bitSaturation As System.Windows.Forms.TextBox
  Friend WithEvents txtVitesseV�lo As System.Windows.Forms.TextBox
  Friend WithEvents txtVitesseV�hicule As System.Windows.Forms.TextBox
  Friend WithEvents txtService As System.Windows.Forms.TextBox
  Friend WithEvents txtOrganisme As System.Windows.Forms.TextBox
  Friend WithEvents chkConserverD�faut As System.Windows.Forms.CheckBox
  Friend WithEvents btnLogo As System.Windows.Forms.Button
  Friend WithEvents lblUvpd As System.Windows.Forms.Label
  Friend WithEvents lblD�bitSaturation As System.Windows.Forms.Label
  Friend WithEvents lblService As System.Windows.Forms.Label
  Friend WithEvents lblOrganisme As System.Windows.Forms.Label
  Friend WithEvents lblMSV�los As System.Windows.Forms.Label
  Friend WithEvents lblV�los As System.Windows.Forms.Label
  Friend WithEvents lblMSV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblMSPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblDossierProjets As System.Windows.Forms.Label
  Friend WithEvents btnParcourir As System.Windows.Forms.Button
  Friend WithEvents lblNomDossier As System.Windows.Forms.Label
  Friend WithEvents grpTemps As System.Windows.Forms.GroupBox
  Friend WithEvents lblTempsPerdu As System.Windows.Forms.Label
  Friend WithEvents lblSecondesPerdu As System.Windows.Forms.Label
  Friend WithEvents chkPi�tonsSonore As System.Windows.Forms.CheckBox
  Friend WithEvents lblVertUtile As System.Windows.Forms.Label
  Friend WithEvents lblJauneInUtilis� As System.Windows.Forms.Label
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
    Me.txtD�bitSaturation = New System.Windows.Forms.TextBox
    Me.lblD�bitSaturation = New System.Windows.Forms.Label
    Me.grpUtilisateur = New System.Windows.Forms.GroupBox
    Me.txtService = New System.Windows.Forms.TextBox
    Me.lblService = New System.Windows.Forms.Label
    Me.txtOrganisme = New System.Windows.Forms.TextBox
    Me.lblOrganisme = New System.Windows.Forms.Label
    Me.lblMSV�los = New System.Windows.Forms.Label
    Me.txtVitesseV�lo = New System.Windows.Forms.TextBox
    Me.lblV�los = New System.Windows.Forms.Label
    Me.lblMSV�hicules = New System.Windows.Forms.Label
    Me.txtVitesseV�hicule = New System.Windows.Forms.TextBox
    Me.lblV�hicules = New System.Windows.Forms.Label
    Me.lblMSPi�tons = New System.Windows.Forms.Label
    Me.txtVitessePi�ton = New System.Windows.Forms.TextBox
    Me.lblPi�tons = New System.Windows.Forms.Label
    Me.grpVitesse = New System.Windows.Forms.GroupBox
    Me.chkConserverD�faut = New System.Windows.Forms.CheckBox
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
    Me.lblJauneInUtilis� = New System.Windows.Forms.Label
    Me.lblSecondesPerdu = New System.Windows.Forms.Label
    Me.txtVUAgglo = New System.Windows.Forms.TextBox
    Me.lblTempsPerdu = New System.Windows.Forms.Label
    Me.chkPi�tonsSonore = New System.Windows.Forms.CheckBox
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
    Me.grpTrafic.Controls.Add(Me.txtD�bitSaturation)
    Me.grpTrafic.Controls.Add(Me.lblD�bitSaturation)
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
    'txtD�bitSaturation
    '
    Me.txtD�bitSaturation.Location = New System.Drawing.Point(16, 56)
    Me.txtD�bitSaturation.Name = "txtD�bitSaturation"
    Me.txtD�bitSaturation.Size = New System.Drawing.Size(32, 20)
    Me.txtD�bitSaturation.TabIndex = 1
    Me.txtD�bitSaturation.Text = "1800"
    Me.txtD�bitSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblD�bitSaturation
    '
    Me.lblD�bitSaturation.Location = New System.Drawing.Point(8, 32)
    Me.lblD�bitSaturation.Name = "lblD�bitSaturation"
    Me.lblD�bitSaturation.Size = New System.Drawing.Size(112, 16)
    Me.lblD�bitSaturation.TabIndex = 0
    Me.lblD�bitSaturation.Text = "D�bit de saturation :"
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
    Me.txtService.Text = "Gestion du Trafic et T�l�matique"
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
    'lblMSV�los
    '
    Me.lblMSV�los.Location = New System.Drawing.Point(232, 56)
    Me.lblMSV�los.Name = "lblMSV�los"
    Me.lblMSV�los.Size = New System.Drawing.Size(24, 24)
    Me.lblMSV�los.TabIndex = 25
    Me.lblMSV�los.Text = "m/s"
    '
    'txtVitesseV�lo
    '
    Me.txtVitesseV�lo.Location = New System.Drawing.Point(200, 56)
    Me.txtVitesseV�lo.Name = "txtVitesseV�lo"
    Me.txtVitesseV�lo.Size = New System.Drawing.Size(24, 20)
    Me.txtVitesseV�lo.TabIndex = 24
    Me.txtVitesseV�lo.Text = "10"
    Me.txtVitesseV�lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblV�los
    '
    Me.lblV�los.Location = New System.Drawing.Point(200, 32)
    Me.lblV�los.Name = "lblV�los"
    Me.lblV�los.Size = New System.Drawing.Size(40, 16)
    Me.lblV�los.TabIndex = 23
    Me.lblV�los.Text = "V�los :"
    '
    'lblMSV�hicules
    '
    Me.lblMSV�hicules.Location = New System.Drawing.Point(144, 56)
    Me.lblMSV�hicules.Name = "lblMSV�hicules"
    Me.lblMSV�hicules.Size = New System.Drawing.Size(24, 24)
    Me.lblMSV�hicules.TabIndex = 22
    Me.lblMSV�hicules.Text = "m/s"
    '
    'txtVitesseV�hicule
    '
    Me.txtVitesseV�hicule.Location = New System.Drawing.Point(112, 56)
    Me.txtVitesseV�hicule.Name = "txtVitesseV�hicule"
    Me.txtVitesseV�hicule.Size = New System.Drawing.Size(24, 20)
    Me.txtVitesseV�hicule.TabIndex = 21
    Me.txtVitesseV�hicule.Text = "10"
    Me.txtVitesseV�hicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblV�hicules
    '
    Me.lblV�hicules.Location = New System.Drawing.Point(112, 32)
    Me.lblV�hicules.Name = "lblV�hicules"
    Me.lblV�hicules.Size = New System.Drawing.Size(64, 16)
    Me.lblV�hicules.TabIndex = 20
    Me.lblV�hicules.Text = "V�hicules :"
    '
    'lblMSPi�tons
    '
    Me.lblMSPi�tons.Location = New System.Drawing.Point(48, 56)
    Me.lblMSPi�tons.Name = "lblMSPi�tons"
    Me.lblMSPi�tons.Size = New System.Drawing.Size(24, 24)
    Me.lblMSPi�tons.TabIndex = 19
    Me.lblMSPi�tons.Text = "m/s"
    '
    'txtVitessePi�ton
    '
    Me.txtVitessePi�ton.Location = New System.Drawing.Point(16, 56)
    Me.txtVitessePi�ton.Name = "txtVitessePi�ton"
    Me.txtVitessePi�ton.Size = New System.Drawing.Size(24, 20)
    Me.txtVitessePi�ton.TabIndex = 18
    Me.txtVitessePi�ton.Text = "1"
    Me.txtVitessePi�ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblPi�tons
    '
    Me.lblPi�tons.Location = New System.Drawing.Point(16, 32)
    Me.lblPi�tons.Name = "lblPi�tons"
    Me.lblPi�tons.Size = New System.Drawing.Size(56, 16)
    Me.lblPi�tons.TabIndex = 17
    Me.lblPi�tons.Text = "Pi�tons :"
    '
    'grpVitesse
    '
    Me.grpVitesse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.grpVitesse.Controls.Add(Me.lblMSV�los)
    Me.grpVitesse.Controls.Add(Me.txtVitesseV�lo)
    Me.grpVitesse.Controls.Add(Me.lblV�los)
    Me.grpVitesse.Controls.Add(Me.lblMSV�hicules)
    Me.grpVitesse.Controls.Add(Me.txtVitesseV�hicule)
    Me.grpVitesse.Controls.Add(Me.lblV�hicules)
    Me.grpVitesse.Controls.Add(Me.lblMSPi�tons)
    Me.grpVitesse.Controls.Add(Me.txtVitessePi�ton)
    Me.grpVitesse.Controls.Add(Me.lblPi�tons)
    Me.grpVitesse.Location = New System.Drawing.Point(16, 360)
    Me.grpVitesse.Name = "grpVitesse"
    Me.grpVitesse.Size = New System.Drawing.Size(272, 96)
    Me.grpVitesse.TabIndex = 5
    Me.grpVitesse.TabStop = False
    Me.grpVitesse.Text = "Vitesses de d�gagement"
    '
    'chkConserverD�faut
    '
    Me.chkConserverD�faut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkConserverD�faut.Location = New System.Drawing.Point(16, 506)
    Me.chkConserverD�faut.Name = "chkConserverD�faut"
    Me.chkConserverD�faut.Size = New System.Drawing.Size(216, 24)
    Me.chkConserverD�faut.TabIndex = 10
    Me.chkConserverD�faut.Text = "Conserver comme valeurs par d�faut"
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
    Me.grpTemps.Controls.Add(Me.lblJauneInUtilis�)
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
    Me.lblHorsAgglo.Text = "Hors agglom�ration"
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
    Me.lblEnAgglo.Text = "En agglom�ration"
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
    Me.lblVertUtile.Text = "Vert utile = vert r�el"
    '
    'lblSecondesJauneAgglo
    '
    Me.lblSecondesJauneAgglo.Location = New System.Drawing.Point(328, 56)
    Me.lblSecondesJauneAgglo.Name = "lblSecondesJauneAgglo"
    Me.lblSecondesJauneAgglo.Size = New System.Drawing.Size(72, 24)
    Me.lblSecondesJauneAgglo.TabIndex = 24
    Me.lblSecondesJauneAgglo.Text = "secondes"
    '
    'lblJauneInUtilis�
    '
    Me.lblJauneInUtilis�.Location = New System.Drawing.Point(280, 16)
    Me.lblJauneInUtilis�.Name = "lblJauneInUtilis�"
    Me.lblJauneInUtilis�.Size = New System.Drawing.Size(80, 32)
    Me.lblJauneInUtilis�.TabIndex = 22
    Me.lblJauneInUtilis�.Text = "Temps de jaune inutilis�"
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
    Me.lblTempsPerdu.Text = "Temps perdu au d�marrage"
    '
    'chkPi�tonsSonore
    '
    Me.chkPi�tonsSonore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkPi�tonsSonore.Checked = True
    Me.chkPi�tonsSonore.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkPi�tonsSonore.Location = New System.Drawing.Point(16, 480)
    Me.chkPi�tonsSonore.Name = "chkPi�tonsSonore"
    Me.chkPi�tonsSonore.Size = New System.Drawing.Size(248, 16)
    Me.chkPi�tonsSonore.TabIndex = 28
    Me.chkPi�tonsSonore.Text = "Signal pi�tons sonore pour les malvoyants"
    Me.tipBulle.SetToolTip(Me.chkPi�tonsSonore, "Indique si les passages pi�tons sont �quip�s de signaux lumineux R12 avc disposit" & _
    "if sonore pour personnes aveugles et malvoyantes")
    '
    'tipBulle
    '
    Me.tipBulle.AutoPopDelay = 10000
    Me.tipBulle.InitialDelay = 500
    Me.tipBulle.ReshowDelay = 100
    '
    'dlgParam�trage
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(642, 543)
    Me.Controls.Add(Me.chkPi�tonsSonore)
    Me.Controls.Add(Me.lblNomDossier)
    Me.Controls.Add(Me.btnParcourir)
    Me.Controls.Add(Me.lblDossierProjets)
    Me.Controls.Add(Me.btnLogo)
    Me.Controls.Add(Me.chkConserverD�faut)
    Me.Controls.Add(Me.grpTrafic)
    Me.Controls.Add(Me.grpVitesse)
    Me.Controls.Add(Me.grpUtilisateur)
    Me.Controls.Add(Me.grpTemps)
    Me.Name = "dlgParam�trage"
    Me.Text = "Param�trage"
    Me.Controls.SetChildIndex(Me.grpTemps, 0)
    Me.Controls.SetChildIndex(Me.grpUtilisateur, 0)
    Me.Controls.SetChildIndex(Me.grpVitesse, 0)
    Me.Controls.SetChildIndex(Me.grpTrafic, 0)
    Me.Controls.SetChildIndex(Me.chkConserverD�faut, 0)
    Me.Controls.SetChildIndex(Me.btnLogo, 0)
    Me.Controls.SetChildIndex(Me.lblDossierProjets, 0)
    Me.Controls.SetChildIndex(Me.btnParcourir, 0)
    Me.Controls.SetChildIndex(Me.lblNomDossier, 0)
    Me.Controls.SetChildIndex(Me.chkPi�tonsSonore, 0)
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

  Public mParam�tres As Param�tres
  Public mObjetM�tier As M�tier
  Public mSignalPi�tonChang� As Boolean
  Private CheminLogo As String
  Private myFileInfo As New IO.FileInfo(NomExe)
  Private CheminStockage As String = myFileInfo.DirectoryName
  Private mBitmapLogo As Bitmap
  Private mTailleBoutonLogo As Size
  Private ChargementEnCours As Boolean

  '******************************************************************************
  ' Fermeture de la feuille
  '******************************************************************************
  Private Sub Param�trage_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Dim sParam�tres As Param�tres

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

        ElseIf VarianteTropAvanc�e() Then

          e.Cancel = True

        Else
          ' R�cup�rer les valeurs saisies

          With sParam�tres
            .VersionFichier = [Global].VersionFichier
            .Organisme = Me.txtOrganisme.Text
            .Service = Me.txtService.Text
            .CheminStockage = Me.lblNomDossier.Text
            cndCheminStockage = .CheminStockage
            .CheminLogo = Me.CheminLogo
            .D�bitSaturation = CType(Me.txtD�bitSaturation.Text, Short)
            .VitessePi�tons = CType(Me.txtVitessePi�ton.Text, Single)
            .VitesseV�hicules = CType(Me.txtVitesseV�hicule.Text, Single)
            .VitesseV�los = CType(Me.txtVitesseV�lo.Text, Single)
            'v12 et ant�rieures
            '.D�calageVertUtile = Me.updD�calageVertUtile.Value
            'v13
            .TempsPerduD�marrageAgglo = Me.updTempsPerduAgglo.Value
            .TempsPerduD�marrageCampagne = Me.updTempsPerduCampagne.Value
            .TempsJauneInutilis�Agglo = Me.updJauneAgglo.Value
            .TempsJauneInutilis�Campagne = Me.updJauneCampagne.Value
            If mParam�tres.SignalPi�tonsSonore <> Me.chkPi�tonsSonore.Checked Then
              mSignalPi�tonChang� = True
              .SignalPi�tonsSonore = Me.chkPi�tonsSonore.Checked
            End If
          End With

          'Affecter les nouvelles valeurs aux param�tres initiaux
          mParam�tres = sParam�tres

          If Me.chkConserverD�faut.Checked Then
            'D�finir les nouvelles valeurs par d�faut
            cndParam�tres = mParam�tres
          End If

          If Not IsNothing(cndVariante) Then
            'D�finir les nouvelles valeurs de param�trage de la variante
            cndVariante.Param = mParam�tres
          End If
        End If
    End Select

  End Sub

  Private Function VarianteTropAvanc�e() As Boolean
    Dim Alerte As Boolean
    Dim sParam�tres As Param�tres
    Dim Message As String

    ' R�cup�rer les valeurs saisies
    If Not IsNothing(cndVariante) AndAlso cndVariante.Verrou >= [Global].Verrouillage.LignesFeux Then

      With sParam�tres
        .D�bitSaturation = CType(Me.txtD�bitSaturation.Text, Short)
        .VitessePi�tons = CType(Me.txtVitessePi�ton.Text, Single)
        .VitesseV�hicules = CType(Me.txtVitesseV�hicule.Text, Single)
        .TempsPerduD�marrageAgglo = Me.updTempsPerduAgglo.Value
        .TempsPerduD�marrageCampagne = Me.updTempsPerduCampagne.Value
        .TempsJauneInutilis�Agglo = Me.updJauneAgglo.Value
        .TempsJauneInutilis�Campagne = Me.updJauneCampagne.Value

        If .VitessePi�tons <> mParam�tres.VitessePi�tons Or .VitesseV�hicules <> mParam�tres.VitesseV�hicules AndAlso _
        cndVariante.UnPhasageRetenu AndAlso cndVariante.ModeGraphique Then
          Message = "La modification des vitesses de d�gagement va r�initialiser les temps de rouge de d�gagement"
          Message &= vbCrLf & "Ceci conduit � red�finir l'organisation du phasage"
          Alerte = True
          mObjetM�tier = cndVariante.PremierPlanBaseRetenu

        ElseIf .D�bitSaturation <> mParam�tres.D�bitSaturation AndAlso cndVariante.DiagnosticCalcul� Then
          Message = "La modification du d�bit de saturation influe sur la capacit� du carrefour"
          If .D�bitSaturation < mParam�tres.D�bitSaturation Then
            Message &= vbCrLf & "Le carrefour est susceptible de ne plus fonctionner"
            Alerte = True
          Else
            Message &= vbCrLf & "Ceci conduit � recalculer les r�serves de capacit� et les temps d'attente"
          End If
          mObjetM�tier = cndVariante.PremierPlanFonctionnement

        ElseIf .TempsPerduD�marrageAgglo <> mParam�tres.TempsPerduD�marrageAgglo Or _
                .TempsPerduD�marrageCampagne <> mParam�tres.TempsPerduD�marrageCampagne Or _
                .TempsJauneInutilis�Agglo <> mParam�tres.TempsJauneInutilis�Agglo Or _
                .TempsJauneInutilis�Campagne <> mParam�tres.TempsJauneInutilis�Campagne Then
          If cndVariante.DiagnosticCalcul� Then
            Message = "La modification des param�tres de calcul du temps perdu influe sur la capacit� du carrefour"
            Message &= vbCrLf & "Ceci conduit � recalculer les r�serves de capacit� et les temps d'attente"
          End If
          mObjetM�tier = cndVariante.PremierPlanFonctionnement

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
  Private Sub Param�trage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MDIDiagfeux))

    ChargementEnCours = True

    'Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Icon = Nothing
    '	Me.Icon = MDIDiagfeux.Icon

    'M�moriser la taille initiale du bouton Logo
    mTailleBoutonLogo = Me.btnLogo.Size

    If IsNothing(cndVariante) Then
      ' Afficher les valeurs par d�faut
      mParam�tres = cndParam�tres

      If IsNothing(cndParam�tres.Service) Then
        '1�re utilisation de  DIAGFEUX
        'c'est d'ailleurs discutable : on pourrait consid�rer que la modification des param�tres g�n�raux est forc�ment � conserver
        Me.chkConserverD�faut.Visible = False
      End If
      Me.chkConserverD�faut.Checked = True

    Else
      ' Afficher les valeurs de la variante
      mParam�tres = cndVariante.Param
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

    With mParam�tres
      Me.txtOrganisme.Text = .Organisme
      Me.txtService.Text = .Service
      If Not IsNothing(.CheminStockage) Then
        CheminStockage = .CheminStockage
      End If
      Me.lblNomDossier.Text = CheminStockage
      CheminLogo = ImageRaster.FichierExistant(.CheminLogo)
      txtVitessePi�ton.Text = CType(.VitessePi�tons, String)
      Me.txtVitesseV�hicule.Text = CType(.VitesseV�hicules, String)
      Me.txtVitesseV�lo.Text = CType(.VitesseV�los, String)
      Me.txtD�bitSaturation.Text = CType(.D�bitSaturation, String)
      'v12 et ant�rieures
      'Me.updD�calageVertUtile.Value = .D�calageVertUtile
      'v13 
      Me.updTempsPerduAgglo.Value = .TempsPerduD�marrageAgglo
      Me.updTempsPerduCampagne.Value = .TempsPerduD�marrageCampagne
      Me.updJauneAgglo.Value = .TempsJauneInutilis�Agglo
      Me.updJauneCampagne.Value = .TempsJauneInutilis�Campagne
      Me.chkPi�tonsSonore.Checked = .SignalPi�tonsSonore
    End With

    Me.updTempsPerduAgglo.Maximum = [Global].PerteAuD�marrageMax
    Me.updTempsPerduCampagne.Maximum = [Global].PerteAuD�marrageMax
    Me.updJauneAgglo.Maximum = [Global].JauneAgglo
    Me.updJauneCampagne.Maximum = [Global].JauneCampagne

    ChargementEnCours = False
  End Sub

  '******************************************************************************
  ' Ev�nements de gestion de la saisie
  '******************************************************************************

  Private Sub txtVitessePi�ton_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVitessePi�ton.KeyPress, txtVitesseV�hicule.KeyPress, txtVitesseV�lo.KeyPress, txtD�bitSaturation.KeyPress, txtVUAgglo.KeyPress

    Dim txt As TextBox
    Dim Entier As Boolean

    txt = sender
    If txt Is Me.txtD�bitSaturation Then
      Entier = True
    End If

    e.Handled = ToucheNonNum�rique(e.KeyChar, Entier:=Entier)

  End Sub

  Private Sub txtVitessePi�ton_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtVitessePi�ton.Validating, txtVitesseV�hicule.Validating, txtVitesseV�lo.Validating, txtD�bitSaturation.Validating

    Dim txt As TextBox = sender
    Dim vMini, vMaxi, Donn�e As Double
    Dim chaine As String = txt.Text

    Try

      If txt Is txtVitessePi�ton Then
        vMini = 0.1
        vMaxi = 1
        Donn�e = mParam�tres.VitessePi�tons
      ElseIf txt Is txtVitesseV�hicule Then
        vMini = 1
        vMaxi = 10
        Donn�e = mParam�tres.VitesseV�hicules
      ElseIf txt Is txtVitesseV�lo Then
        vMini = 1
        vMaxi = 10
        Donn�e = mParam�tres.VitesseV�los
      End If

      If Not IsNumeric(txt.Text) Then
        MsgBox("Saisie incorrecte")
        e.Cancel = True
      ElseIf Not txt Is txtD�bitSaturation Then
        'Pas de controle particulier pour le d�bit de saturation (?)
        e.Cancel = ControlerBornes(Me, vMini, vMaxi, txt, Donn�e, unFormat:="0.0")
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
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt, InfoFichier:=New IO.FileInfo(mParam�tres.CheminLogo))
    End If

    If Not IsNothing(NomFichier) Then
      CheminLogo = NomFichier
      'Obliger � rafraichir l'image du bouton logo
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

        ' Associer une Image Bitmap tampon � un objet Graphics tampon
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
    Dim D�calageVertUtile As Short

    D�calageVertUtile = [Global].JauneAgglo - (TempsPerduAgglo + Me.updJauneAgglo.Value)
    Me.txtVUAgglo.Text = CType(Math.Abs(D�calageVertUtile), String)
    Select Case Math.Sign(D�calageVertUtile)
      Case 0
        Me.lblPlusAgglo.Text = ""
      Case 1
        Me.lblPlusAgglo.Text = "+"
      Case -1
        Me.lblPlusAgglo.Text = "-"
    End Select

    D�calageVertUtile = [Global].JauneCampagne - (TempsPerduCampagne + Me.updJauneCampagne.Value)
    Me.txtVuCampagne.Text = CType(Math.Abs(D�calageVertUtile), String)
    Select Case Math.Sign(D�calageVertUtile)
      Case 0
        Me.lblPlusCampagne.Text = ""
      Case 1
        Me.lblPlusCampagne.Text = "+"
      Case -1
        Me.lblPlusCampagne.Text = "-"
    End Select

  End Sub

  Private Sub chkPi�tonsSonore_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPi�tonsSonore.CheckedChanged
    Dim Message As String

    If Not chkPi�tonsSonore.Checked AndAlso Not ChargementEnCours Then
      Message = "Attention,les R12 avec dispositif sonore sont s�lectionn�s par d�faut,car ceux-ci sont obligatoires. (d�crets 99-756 et 99-757 du 13/07/1999)" & vbCrLf & vbCrLf
      Message &= "Vous ne pouvez d�s�lectionner ces signaux que si le carrefour est trop compliqu� ou si les passages pi�tons ne peuvent �tre que trop proches les uns des autres pour que les sont issus des diff�rents dispositifs puissent �tre distingu�s." & vbCrLf
      Message &= "Car dans ces cas le carrefour ne remplit pas les conditions de s�curit� n�cessaires pour les personnes aveugles et malvoyantes. On pr�f�rera alors les r�p�teurs tactiles."

      MessageBox.Show(Message, "Signal pi�tons sonore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End If

  End Sub

  Private Sub dlgParam�trage_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_PARAMETRAGE
  End Sub
End Class
