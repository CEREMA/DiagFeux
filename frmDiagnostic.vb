'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : frmDiagnostic.vb										  											'
'						Classes																														'
'							frmDiagnostic : Feuille                 												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe frmDiagnostic --------------------------
'Affichage du diagnostic du plan de feux de fonxtionnement
'=====================================================================================================
Public Class frmDiagnostic
  Inherits System.Windows.Forms.Form

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
  Friend WithEvents grpDiagnostic As System.Windows.Forms.GroupBox
  Friend WithEvents txtRéservePourCent As System.Windows.Forms.TextBox
  Friend WithEvents lblUvpdHeure As System.Windows.Forms.Label
  Friend WithEvents lblRéserveCapacité As System.Windows.Forms.Label
  Friend WithEvents pnlCommentaires As System.Windows.Forms.Panel
  Friend WithEvents lblCommentairesPlanFeux As System.Windows.Forms.Label
  Friend WithEvents txtCommentairesFct As System.Windows.Forms.TextBox
  Friend WithEvents pnlParamétrage As System.Windows.Forms.Panel
  Friend WithEvents lblVitesse As System.Windows.Forms.Label
  Friend WithEvents lblMSPiétons As System.Windows.Forms.Label
  Friend WithEvents txtVitessePiéton As System.Windows.Forms.TextBox
  Friend WithEvents lblPiétons As System.Windows.Forms.Label
  Friend WithEvents lblMSVéhicules As System.Windows.Forms.Label
  Friend WithEvents txtVitesseVéhicule As System.Windows.Forms.TextBox
  Friend WithEvents lblVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblMSVélos As System.Windows.Forms.Label
  Friend WithEvents txtVitesseVélo As System.Windows.Forms.TextBox
  Friend WithEvents lblVélos As System.Windows.Forms.Label
  Friend WithEvents lblUvpd As System.Windows.Forms.Label
  Friend WithEvents txtDébitSaturation As System.Windows.Forms.TextBox
  Friend WithEvents lblDébitSaturation As System.Windows.Forms.Label
  Friend WithEvents lblVertUtile As System.Windows.Forms.Label
  Friend WithEvents lblSecondesVert As System.Windows.Forms.Label
  Friend WithEvents lblCycle As System.Windows.Forms.Label
  Friend WithEvents lblDemande As System.Windows.Forms.Label
  Friend WithEvents txtDuréeCycleBase As System.Windows.Forms.TextBox
  Friend WithEvents txtDemande As System.Windows.Forms.TextBox
  Friend WithEvents txtCapacitéPlan As System.Windows.Forms.TextBox
  Friend WithEvents grpTemps As System.Windows.Forms.GroupBox
  Friend WithEvents txtTempsAttentePiétons As System.Windows.Forms.TextBox
  Friend WithEvents lblTempsAttenteFile As System.Windows.Forms.Label
  Friend WithEvents lblLgFileAttente As System.Windows.Forms.Label
  Friend WithEvents lblSeconde As System.Windows.Forms.Label
  Friend WithEvents txtTempsAttenteVéhicules As System.Windows.Forms.TextBox
  Friend WithEvents lvwcolLP As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTP As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolLF As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDemande As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolVert As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolNbVéhicules As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolLongueurFile As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTMA As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolVertPiétons As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTMAPiéton As System.Windows.Forms.ColumnHeader
  Friend WithEvents lblVertPiétons As System.Windows.Forms.Label
  Friend WithEvents lblFluxPiétons As System.Windows.Forms.Label
  Friend WithEvents lblUVPDRéserveCapacité As System.Windows.Forms.Label
  Friend WithEvents lblUVPDCapacitéPlan As System.Windows.Forms.Label
  Friend WithEvents lblCapacitéPlan As System.Windows.Forms.Label
  Friend WithEvents lblSecondesCapacité As System.Windows.Forms.Label
  Friend WithEvents lblTempsAttentePiétons As System.Windows.Forms.Label
  Friend WithEvents lblTempsMoyenPiétons As System.Windows.Forms.Label
  Friend WithEvents lblNbVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblVertVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblDemandeUVP As System.Windows.Forms.Label
  Friend WithEvents lblDiagVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblDiagPiétons As System.Windows.Forms.Label
  Friend WithEvents lblTempsMoyenVéhicules As System.Windows.Forms.Label
  Friend WithEvents lblTempsPerduCycle As System.Windows.Forms.Label
  Friend WithEvents txtTempsPerduCycle As System.Windows.Forms.TextBox
  Friend WithEvents lvwPiétons As System.Windows.Forms.ListView
  Friend WithEvents lvwVéhicules As System.Windows.Forms.ListView
  Friend WithEvents txtRéserveCapacité As System.Windows.Forms.TextBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P1", "10", "23", "28"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P2", "300", "16", "26"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P5", "50", "27", "31"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P6", "200", "29", "30"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F1", "400", "23", "28", "6", "30", "28"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F2", "270", "23", "26", "4", "20", "26"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F3", "250", "16", "31", "4", "20", "31"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F4", "165", "16", "30", "3", "15", "30"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F5", "415", "28", "24", "6", "30", "24"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F6", "600", "28", "28", "9", "45", "28"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
    Me.grpDiagnostic = New System.Windows.Forms.GroupBox
    Me.txtTempsPerduCycle = New System.Windows.Forms.TextBox
    Me.lblTempsPerduCycle = New System.Windows.Forms.Label
    Me.lblUVPDRéserveCapacité = New System.Windows.Forms.Label
    Me.lblUVPDCapacitéPlan = New System.Windows.Forms.Label
    Me.txtCapacitéPlan = New System.Windows.Forms.TextBox
    Me.lblCapacitéPlan = New System.Windows.Forms.Label
    Me.lblSecondesCapacité = New System.Windows.Forms.Label
    Me.txtDemande = New System.Windows.Forms.TextBox
    Me.txtDuréeCycleBase = New System.Windows.Forms.TextBox
    Me.lblDemande = New System.Windows.Forms.Label
    Me.lblCycle = New System.Windows.Forms.Label
    Me.txtRéservePourCent = New System.Windows.Forms.TextBox
    Me.lblUvpdHeure = New System.Windows.Forms.Label
    Me.txtRéserveCapacité = New System.Windows.Forms.TextBox
    Me.lblRéserveCapacité = New System.Windows.Forms.Label
    Me.pnlCommentaires = New System.Windows.Forms.Panel
    Me.lblCommentairesPlanFeux = New System.Windows.Forms.Label
    Me.txtCommentairesFct = New System.Windows.Forms.TextBox
    Me.pnlParamétrage = New System.Windows.Forms.Panel
    Me.lblPiétons = New System.Windows.Forms.Label
    Me.lblSecondesVert = New System.Windows.Forms.Label
    Me.lblVertUtile = New System.Windows.Forms.Label
    Me.lblUvpd = New System.Windows.Forms.Label
    Me.txtDébitSaturation = New System.Windows.Forms.TextBox
    Me.lblDébitSaturation = New System.Windows.Forms.Label
    Me.lblMSVélos = New System.Windows.Forms.Label
    Me.txtVitesseVélo = New System.Windows.Forms.TextBox
    Me.lblVélos = New System.Windows.Forms.Label
    Me.lblMSVéhicules = New System.Windows.Forms.Label
    Me.txtVitesseVéhicule = New System.Windows.Forms.TextBox
    Me.lblVéhicules = New System.Windows.Forms.Label
    Me.lblMSPiétons = New System.Windows.Forms.Label
    Me.txtVitessePiéton = New System.Windows.Forms.TextBox
    Me.lblVitesse = New System.Windows.Forms.Label
    Me.grpTemps = New System.Windows.Forms.GroupBox
    Me.lblTempsAttentePiétons = New System.Windows.Forms.Label
    Me.txtTempsAttentePiétons = New System.Windows.Forms.TextBox
    Me.lblTempsMoyenPiétons = New System.Windows.Forms.Label
    Me.lblVertPiétons = New System.Windows.Forms.Label
    Me.lblFluxPiétons = New System.Windows.Forms.Label
    Me.lblNbVéhicules = New System.Windows.Forms.Label
    Me.lblVertVéhicules = New System.Windows.Forms.Label
    Me.lblDemandeUVP = New System.Windows.Forms.Label
    Me.lblTempsAttenteFile = New System.Windows.Forms.Label
    Me.lblLgFileAttente = New System.Windows.Forms.Label
    Me.lblDiagVéhicules = New System.Windows.Forms.Label
    Me.lblDiagPiétons = New System.Windows.Forms.Label
    Me.lblSeconde = New System.Windows.Forms.Label
    Me.txtTempsAttenteVéhicules = New System.Windows.Forms.TextBox
    Me.lblTempsMoyenVéhicules = New System.Windows.Forms.Label
    Me.lvwPiétons = New System.Windows.Forms.ListView
    Me.lvwcolLP = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTP = New System.Windows.Forms.ColumnHeader
    Me.lvwcolVertPiétons = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTMAPiéton = New System.Windows.Forms.ColumnHeader
    Me.lvwVéhicules = New System.Windows.Forms.ListView
    Me.lvwcolLF = New System.Windows.Forms.ColumnHeader
    Me.lvwcolDemande = New System.Windows.Forms.ColumnHeader
    Me.lvwcolVert = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTMA = New System.Windows.Forms.ColumnHeader
    Me.lvwcolNbVéhicules = New System.Windows.Forms.ColumnHeader
    Me.lvwcolLongueurFile = New System.Windows.Forms.ColumnHeader
    Me.grpDiagnostic.SuspendLayout()
    Me.pnlCommentaires.SuspendLayout()
    Me.pnlParamétrage.SuspendLayout()
    Me.grpTemps.SuspendLayout()
    Me.SuspendLayout()
    '
    'grpDiagnostic
    '
    Me.grpDiagnostic.Controls.Add(Me.txtTempsPerduCycle)
    Me.grpDiagnostic.Controls.Add(Me.lblTempsPerduCycle)
    Me.grpDiagnostic.Controls.Add(Me.lblUVPDRéserveCapacité)
    Me.grpDiagnostic.Controls.Add(Me.lblUVPDCapacitéPlan)
    Me.grpDiagnostic.Controls.Add(Me.txtCapacitéPlan)
    Me.grpDiagnostic.Controls.Add(Me.lblCapacitéPlan)
    Me.grpDiagnostic.Controls.Add(Me.lblSecondesCapacité)
    Me.grpDiagnostic.Controls.Add(Me.txtDemande)
    Me.grpDiagnostic.Controls.Add(Me.txtDuréeCycleBase)
    Me.grpDiagnostic.Controls.Add(Me.lblDemande)
    Me.grpDiagnostic.Controls.Add(Me.lblCycle)
    Me.grpDiagnostic.Controls.Add(Me.txtRéservePourCent)
    Me.grpDiagnostic.Controls.Add(Me.lblUvpdHeure)
    Me.grpDiagnostic.Controls.Add(Me.txtRéserveCapacité)
    Me.grpDiagnostic.Controls.Add(Me.lblRéserveCapacité)
    Me.grpDiagnostic.Location = New System.Drawing.Point(8, 80)
    Me.grpDiagnostic.Name = "grpDiagnostic"
    Me.grpDiagnostic.Size = New System.Drawing.Size(616, 96)
    Me.grpDiagnostic.TabIndex = 43
    Me.grpDiagnostic.TabStop = False
    Me.grpDiagnostic.Text = "Diagnostic"
    '
    'txtTempsPerduCycle
    '
    Me.txtTempsPerduCycle.BackColor = System.Drawing.SystemColors.Window
    Me.txtTempsPerduCycle.Location = New System.Drawing.Point(496, 24)
    Me.txtTempsPerduCycle.Name = "txtTempsPerduCycle"
    Me.txtTempsPerduCycle.ReadOnly = True
    Me.txtTempsPerduCycle.Size = New System.Drawing.Size(48, 20)
    Me.txtTempsPerduCycle.TabIndex = 39
    Me.txtTempsPerduCycle.Text = "9"
    Me.txtTempsPerduCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsPerduCycle
    '
    Me.lblTempsPerduCycle.Location = New System.Drawing.Point(360, 24)
    Me.lblTempsPerduCycle.Name = "lblTempsPerduCycle"
    Me.lblTempsPerduCycle.Size = New System.Drawing.Size(152, 16)
    Me.lblTempsPerduCycle.TabIndex = 38
    Me.lblTempsPerduCycle.Text = "Temps perdu sur le cycle (s)"
    '
    'lblUVPDRéserveCapacité
    '
    Me.lblUVPDRéserveCapacité.Location = New System.Drawing.Point(208, 40)
    Me.lblUVPDRéserveCapacité.Name = "lblUVPDRéserveCapacité"
    Me.lblUVPDRéserveCapacité.Size = New System.Drawing.Size(88, 16)
    Me.lblUVPDRéserveCapacité.TabIndex = 37
    Me.lblUVPDRéserveCapacité.Text = "(uvpd/h et voie)"
    '
    'lblUVPDCapacitéPlan
    '
    Me.lblUVPDCapacitéPlan.Location = New System.Drawing.Point(360, 64)
    Me.lblUVPDCapacitéPlan.Name = "lblUVPDCapacitéPlan"
    Me.lblUVPDCapacitéPlan.Size = New System.Drawing.Size(88, 16)
    Me.lblUVPDCapacitéPlan.TabIndex = 36
    Me.lblUVPDCapacitéPlan.Text = "(uvpd/h et voie)"
    '
    'txtCapacitéPlan
    '
    Me.txtCapacitéPlan.BackColor = System.Drawing.SystemColors.Window
    Me.txtCapacitéPlan.Location = New System.Drawing.Point(496, 48)
    Me.txtCapacitéPlan.Name = "txtCapacitéPlan"
    Me.txtCapacitéPlan.ReadOnly = True
    Me.txtCapacitéPlan.Size = New System.Drawing.Size(48, 20)
    Me.txtCapacitéPlan.TabIndex = 35
    Me.txtCapacitéPlan.Text = "190"
    Me.txtCapacitéPlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblCapacitéPlan
    '
    Me.lblCapacitéPlan.Location = New System.Drawing.Point(360, 48)
    Me.lblCapacitéPlan.Name = "lblCapacitéPlan"
    Me.lblCapacitéPlan.Size = New System.Drawing.Size(136, 23)
    Me.lblCapacitéPlan.TabIndex = 34
    Me.lblCapacitéPlan.Text = "Capacité du plan de feux:"
    '
    'lblSecondesCapacité
    '
    Me.lblSecondesCapacité.Location = New System.Drawing.Point(336, 48)
    Me.lblSecondesCapacité.Name = "lblSecondesCapacité"
    Me.lblSecondesCapacité.Size = New System.Drawing.Size(24, 16)
    Me.lblSecondesCapacité.TabIndex = 33
    Me.lblSecondesCapacité.Text = "%"
    Me.lblSecondesCapacité.Visible = False
    '
    'txtDemande
    '
    Me.txtDemande.BackColor = System.Drawing.SystemColors.Window
    Me.txtDemande.Location = New System.Drawing.Point(136, 48)
    Me.txtDemande.Name = "txtDemande"
    Me.txtDemande.ReadOnly = True
    Me.txtDemande.Size = New System.Drawing.Size(40, 20)
    Me.txtDemande.TabIndex = 32
    Me.txtDemande.Text = "1250"
    Me.txtDemande.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'txtDuréeCycleBase
    '
    Me.txtDuréeCycleBase.BackColor = System.Drawing.SystemColors.Window
    Me.txtDuréeCycleBase.Location = New System.Drawing.Point(152, 24)
    Me.txtDuréeCycleBase.Name = "txtDuréeCycleBase"
    Me.txtDuréeCycleBase.ReadOnly = True
    Me.txtDuréeCycleBase.Size = New System.Drawing.Size(24, 20)
    Me.txtDuréeCycleBase.TabIndex = 31
    Me.txtDuréeCycleBase.Text = "60"
    Me.txtDuréeCycleBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblDemande
    '
    Me.lblDemande.Location = New System.Drawing.Point(16, 48)
    Me.lblDemande.Name = "lblDemande"
    Me.lblDemande.Size = New System.Drawing.Size(128, 16)
    Me.lblDemande.TabIndex = 30
    Me.lblDemande.Text = "Demande du carrefour :"
    '
    'lblCycle
    '
    Me.lblCycle.Location = New System.Drawing.Point(16, 24)
    Me.lblCycle.Name = "lblCycle"
    Me.lblCycle.Size = New System.Drawing.Size(112, 16)
    Me.lblCycle.TabIndex = 29
    Me.lblCycle.Text = "Durée du cycle (s) :"
    '
    'txtRéservePourCent
    '
    Me.txtRéservePourCent.BackColor = System.Drawing.SystemColors.Window
    Me.txtRéservePourCent.Location = New System.Drawing.Point(304, 48)
    Me.txtRéservePourCent.Name = "txtRéservePourCent"
    Me.txtRéservePourCent.ReadOnly = True
    Me.txtRéservePourCent.Size = New System.Drawing.Size(32, 20)
    Me.txtRéservePourCent.TabIndex = 17
    Me.txtRéservePourCent.Text = "17"
    Me.txtRéservePourCent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblUvpdHeure
    '
    Me.lblUvpdHeure.Location = New System.Drawing.Point(16, 64)
    Me.lblUvpdHeure.Name = "lblUvpdHeure"
    Me.lblUvpdHeure.Size = New System.Drawing.Size(88, 16)
    Me.lblUvpdHeure.TabIndex = 2
    Me.lblUvpdHeure.Text = "(uvpd/h et voie)"
    '
    'txtRéserveCapacité
    '
    Me.txtRéserveCapacité.BackColor = System.Drawing.SystemColors.Window
    Me.txtRéserveCapacité.Location = New System.Drawing.Point(304, 24)
    Me.txtRéserveCapacité.Name = "txtRéserveCapacité"
    Me.txtRéserveCapacité.ReadOnly = True
    Me.txtRéserveCapacité.Size = New System.Drawing.Size(40, 20)
    Me.txtRéserveCapacité.TabIndex = 1
    Me.txtRéserveCapacité.Text = "190"
    Me.txtRéserveCapacité.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblRéserveCapacité
    '
    Me.lblRéserveCapacité.Location = New System.Drawing.Point(192, 24)
    Me.lblRéserveCapacité.Name = "lblRéserveCapacité"
    Me.lblRéserveCapacité.Size = New System.Drawing.Size(120, 23)
    Me.lblRéserveCapacité.TabIndex = 0
    Me.lblRéserveCapacité.Text = "Réserve de capacité :"
    '
    'pnlCommentaires
    '
    Me.pnlCommentaires.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlCommentaires.Controls.Add(Me.lblCommentairesPlanFeux)
    Me.pnlCommentaires.Controls.Add(Me.txtCommentairesFct)
    Me.pnlCommentaires.Location = New System.Drawing.Point(3714, 2214)
    Me.pnlCommentaires.Name = "pnlCommentaires"
    Me.pnlCommentaires.Size = New System.Drawing.Size(280, 128)
    Me.pnlCommentaires.TabIndex = 47
    '
    'lblCommentairesPlanFeux
    '
    Me.lblCommentairesPlanFeux.Location = New System.Drawing.Point(8, 8)
    Me.lblCommentairesPlanFeux.Name = "lblCommentairesPlanFeux"
    Me.lblCommentairesPlanFeux.TabIndex = 1
    Me.lblCommentairesPlanFeux.Text = "Commentaires"
    '
    'txtCommentairesFct
    '
    Me.txtCommentairesFct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtCommentairesFct.ForeColor = System.Drawing.Color.Red
    Me.txtCommentairesFct.Location = New System.Drawing.Point(8, 40)
    Me.txtCommentairesFct.Multiline = True
    Me.txtCommentairesFct.Name = "txtCommentairesFct"
    Me.txtCommentairesFct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtCommentairesFct.Size = New System.Drawing.Size(264, 64)
    Me.txtCommentairesFct.TabIndex = 0
    Me.txtCommentairesFct.Text = ""
    '
    'pnlParamétrage
    '
    Me.pnlParamétrage.Controls.Add(Me.lblPiétons)
    Me.pnlParamétrage.Controls.Add(Me.lblSecondesVert)
    Me.pnlParamétrage.Controls.Add(Me.lblVertUtile)
    Me.pnlParamétrage.Controls.Add(Me.lblUvpd)
    Me.pnlParamétrage.Controls.Add(Me.txtDébitSaturation)
    Me.pnlParamétrage.Controls.Add(Me.lblDébitSaturation)
    Me.pnlParamétrage.Controls.Add(Me.lblMSVélos)
    Me.pnlParamétrage.Controls.Add(Me.txtVitesseVélo)
    Me.pnlParamétrage.Controls.Add(Me.lblVélos)
    Me.pnlParamétrage.Controls.Add(Me.lblMSVéhicules)
    Me.pnlParamétrage.Controls.Add(Me.txtVitesseVéhicule)
    Me.pnlParamétrage.Controls.Add(Me.lblVéhicules)
    Me.pnlParamétrage.Controls.Add(Me.lblMSPiétons)
    Me.pnlParamétrage.Controls.Add(Me.txtVitessePiéton)
    Me.pnlParamétrage.Controls.Add(Me.lblVitesse)
    Me.pnlParamétrage.Location = New System.Drawing.Point(8, 8)
    Me.pnlParamétrage.Name = "pnlParamétrage"
    Me.pnlParamétrage.Size = New System.Drawing.Size(616, 64)
    Me.pnlParamétrage.TabIndex = 48
    '
    'lblPiétons
    '
    Me.lblPiétons.Location = New System.Drawing.Point(144, 24)
    Me.lblPiétons.Name = "lblPiétons"
    Me.lblPiétons.Size = New System.Drawing.Size(48, 16)
    Me.lblPiétons.TabIndex = 20
    Me.lblPiétons.Text = "Piétons :"
    '
    'lblSecondesVert
    '
    Me.lblSecondesVert.Location = New System.Drawing.Point(360, 40)
    Me.lblSecondesVert.Name = "lblSecondesVert"
    Me.lblSecondesVert.Size = New System.Drawing.Size(72, 16)
    Me.lblSecondesVert.TabIndex = 40
    Me.lblSecondesVert.Text = "+ 1 seconde(s)"
    '
    'lblVertUtile
    '
    Me.lblVertUtile.Location = New System.Drawing.Point(264, 40)
    Me.lblVertUtile.Name = "lblVertUtile"
    Me.lblVertUtile.Size = New System.Drawing.Size(104, 16)
    Me.lblVertUtile.TabIndex = 39
    Me.lblVertUtile.Text = "Vert utile = vert réel "
    '
    'lblUvpd
    '
    Me.lblUvpd.Location = New System.Drawing.Point(136, 40)
    Me.lblUvpd.Name = "lblUvpd"
    Me.lblUvpd.Size = New System.Drawing.Size(48, 16)
    Me.lblUvpd.TabIndex = 31
    Me.lblUvpd.Text = "uvpd/h"
    '
    'txtDébitSaturation
    '
    Me.txtDébitSaturation.BackColor = System.Drawing.SystemColors.Control
    Me.txtDébitSaturation.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtDébitSaturation.Location = New System.Drawing.Point(104, 40)
    Me.txtDébitSaturation.Name = "txtDébitSaturation"
    Me.txtDébitSaturation.Size = New System.Drawing.Size(32, 13)
    Me.txtDébitSaturation.TabIndex = 30
    Me.txtDébitSaturation.Text = "1700"
    Me.txtDébitSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblDébitSaturation
    '
    Me.lblDébitSaturation.Location = New System.Drawing.Point(8, 40)
    Me.lblDébitSaturation.Name = "lblDébitSaturation"
    Me.lblDébitSaturation.Size = New System.Drawing.Size(112, 16)
    Me.lblDébitSaturation.TabIndex = 29
    Me.lblDébitSaturation.Text = "Débit de saturation :"
    '
    'lblMSVélos
    '
    Me.lblMSVélos.Location = New System.Drawing.Point(448, 24)
    Me.lblMSVélos.Name = "lblMSVélos"
    Me.lblMSVélos.Size = New System.Drawing.Size(24, 24)
    Me.lblMSVélos.TabIndex = 28
    Me.lblMSVélos.Text = "m/s"
    '
    'txtVitesseVélo
    '
    Me.txtVitesseVélo.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitesseVélo.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitesseVélo.Location = New System.Drawing.Point(424, 24)
    Me.txtVitesseVélo.Name = "txtVitesseVélo"
    Me.txtVitesseVélo.Size = New System.Drawing.Size(16, 13)
    Me.txtVitesseVélo.TabIndex = 27
    Me.txtVitesseVélo.Text = "7"
    Me.txtVitesseVélo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVélos
    '
    Me.lblVélos.Location = New System.Drawing.Point(384, 24)
    Me.lblVélos.Name = "lblVélos"
    Me.lblVélos.Size = New System.Drawing.Size(40, 16)
    Me.lblVélos.TabIndex = 26
    Me.lblVélos.Text = "Vélos :"
    '
    'lblMSVéhicules
    '
    Me.lblMSVéhicules.Location = New System.Drawing.Point(344, 24)
    Me.lblMSVéhicules.Name = "lblMSVéhicules"
    Me.lblMSVéhicules.Size = New System.Drawing.Size(24, 24)
    Me.lblMSVéhicules.TabIndex = 25
    Me.lblMSVéhicules.Text = "m/s"
    '
    'txtVitesseVéhicule
    '
    Me.txtVitesseVéhicule.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitesseVéhicule.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitesseVéhicule.Location = New System.Drawing.Point(328, 24)
    Me.txtVitesseVéhicule.Name = "txtVitesseVéhicule"
    Me.txtVitesseVéhicule.Size = New System.Drawing.Size(16, 13)
    Me.txtVitesseVéhicule.TabIndex = 24
    Me.txtVitesseVéhicule.Text = "8"
    Me.txtVitesseVéhicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVéhicules
    '
    Me.lblVéhicules.Location = New System.Drawing.Point(264, 24)
    Me.lblVéhicules.Name = "lblVéhicules"
    Me.lblVéhicules.Size = New System.Drawing.Size(64, 16)
    Me.lblVéhicules.TabIndex = 23
    Me.lblVéhicules.Text = "Véhicules :"
    '
    'lblMSPiétons
    '
    Me.lblMSPiétons.Location = New System.Drawing.Point(216, 24)
    Me.lblMSPiétons.Name = "lblMSPiétons"
    Me.lblMSPiétons.Size = New System.Drawing.Size(24, 24)
    Me.lblMSPiétons.TabIndex = 22
    Me.lblMSPiétons.Text = "m/s"
    '
    'txtVitessePiéton
    '
    Me.txtVitessePiéton.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitessePiéton.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitessePiéton.Location = New System.Drawing.Point(184, 24)
    Me.txtVitessePiéton.Name = "txtVitessePiéton"
    Me.txtVitessePiéton.Size = New System.Drawing.Size(24, 13)
    Me.txtVitessePiéton.TabIndex = 21
    Me.txtVitessePiéton.Text = "0.8"
    Me.txtVitessePiéton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVitesse
    '
    Me.lblVitesse.Location = New System.Drawing.Point(8, 24)
    Me.lblVitesse.Name = "lblVitesse"
    Me.lblVitesse.Size = New System.Drawing.Size(136, 16)
    Me.lblVitesse.TabIndex = 0
    Me.lblVitesse.Text = "Vitesses de dégagement"
    '
    'grpTemps
    '
    Me.grpTemps.Controls.Add(Me.lblTempsAttentePiétons)
    Me.grpTemps.Controls.Add(Me.txtTempsAttentePiétons)
    Me.grpTemps.Controls.Add(Me.lblTempsMoyenPiétons)
    Me.grpTemps.Controls.Add(Me.lblVertPiétons)
    Me.grpTemps.Controls.Add(Me.lblFluxPiétons)
    Me.grpTemps.Controls.Add(Me.lblNbVéhicules)
    Me.grpTemps.Controls.Add(Me.lblVertVéhicules)
    Me.grpTemps.Controls.Add(Me.lblDemandeUVP)
    Me.grpTemps.Controls.Add(Me.lblTempsAttenteFile)
    Me.grpTemps.Controls.Add(Me.lblLgFileAttente)
    Me.grpTemps.Controls.Add(Me.lblDiagVéhicules)
    Me.grpTemps.Controls.Add(Me.lblDiagPiétons)
    Me.grpTemps.Controls.Add(Me.lblSeconde)
    Me.grpTemps.Controls.Add(Me.txtTempsAttenteVéhicules)
    Me.grpTemps.Controls.Add(Me.lblTempsMoyenVéhicules)
    Me.grpTemps.Controls.Add(Me.lvwPiétons)
    Me.grpTemps.Controls.Add(Me.lvwVéhicules)
    Me.grpTemps.Location = New System.Drawing.Point(8, 184)
    Me.grpTemps.Name = "grpTemps"
    Me.grpTemps.Size = New System.Drawing.Size(616, 376)
    Me.grpTemps.TabIndex = 50
    Me.grpTemps.TabStop = False
    '
    'lblTempsAttentePiétons
    '
    Me.lblTempsAttentePiétons.Location = New System.Drawing.Point(216, 248)
    Me.lblTempsAttentePiétons.Name = "lblTempsAttentePiétons"
    Me.lblTempsAttentePiétons.Size = New System.Drawing.Size(104, 16)
    Me.lblTempsAttentePiétons.TabIndex = 44
    Me.lblTempsAttentePiétons.Text = "Temps d'attente (s)"
    '
    'txtTempsAttentePiétons
    '
    Me.txtTempsAttentePiétons.BackColor = System.Drawing.SystemColors.Window
    Me.txtTempsAttentePiétons.Location = New System.Drawing.Point(400, 336)
    Me.txtTempsAttentePiétons.Name = "txtTempsAttentePiétons"
    Me.txtTempsAttentePiétons.ReadOnly = True
    Me.txtTempsAttentePiétons.Size = New System.Drawing.Size(40, 20)
    Me.txtTempsAttentePiétons.TabIndex = 43
    Me.txtTempsAttentePiétons.Text = ""
    Me.txtTempsAttentePiétons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsMoyenPiétons
    '
    Me.lblTempsMoyenPiétons.Location = New System.Drawing.Point(320, 336)
    Me.lblTempsMoyenPiétons.Name = "lblTempsMoyenPiétons"
    Me.lblTempsMoyenPiétons.Size = New System.Drawing.Size(88, 16)
    Me.lblTempsMoyenPiétons.TabIndex = 42
    Me.lblTempsMoyenPiétons.Text = "Temps moyen :"
    '
    'lblVertPiétons
    '
    Me.lblVertPiétons.Location = New System.Drawing.Point(160, 248)
    Me.lblVertPiétons.Name = "lblVertPiétons"
    Me.lblVertPiétons.Size = New System.Drawing.Size(88, 16)
    Me.lblVertPiétons.TabIndex = 40
    Me.lblVertPiétons.Text = "Vert (s)"
    '
    'lblFluxPiétons
    '
    Me.lblFluxPiétons.Location = New System.Drawing.Point(104, 248)
    Me.lblFluxPiétons.Name = "lblFluxPiétons"
    Me.lblFluxPiétons.Size = New System.Drawing.Size(32, 16)
    Me.lblFluxPiétons.TabIndex = 39
    Me.lblFluxPiétons.Text = "Flux"
    '
    'lblNbVéhicules
    '
    Me.lblNbVéhicules.Location = New System.Drawing.Point(304, 16)
    Me.lblNbVéhicules.Name = "lblNbVéhicules"
    Me.lblNbVéhicules.Size = New System.Drawing.Size(96, 32)
    Me.lblNbVéhicules.TabIndex = 37
    Me.lblNbVéhicules.Text = "Nombre de véhicules par file"
    '
    'lblVertVéhicules
    '
    Me.lblVertVéhicules.Location = New System.Drawing.Point(152, 16)
    Me.lblVertVéhicules.Name = "lblVertVéhicules"
    Me.lblVertVéhicules.Size = New System.Drawing.Size(64, 32)
    Me.lblVertVéhicules.TabIndex = 35
    Me.lblVertVéhicules.Text = "Vert utile(s)"
    '
    'lblDemandeUVP
    '
    Me.lblDemandeUVP.Location = New System.Drawing.Point(80, 16)
    Me.lblDemandeUVP.Name = "lblDemandeUVP"
    Me.lblDemandeUVP.Size = New System.Drawing.Size(72, 28)
    Me.lblDemandeUVP.TabIndex = 34
    Me.lblDemandeUVP.Text = "Demande uvp/h et voie"
    '
    'lblTempsAttenteFile
    '
    Me.lblTempsAttenteFile.Location = New System.Drawing.Point(224, 16)
    Me.lblTempsAttenteFile.Name = "lblTempsAttenteFile"
    Me.lblTempsAttenteFile.Size = New System.Drawing.Size(88, 32)
    Me.lblTempsAttenteFile.TabIndex = 30
    Me.lblTempsAttenteFile.Text = "Temps d'attente par file (s)"
    '
    'lblLgFileAttente
    '
    Me.lblLgFileAttente.Location = New System.Drawing.Point(400, 16)
    Me.lblLgFileAttente.Name = "lblLgFileAttente"
    Me.lblLgFileAttente.Size = New System.Drawing.Size(72, 28)
    Me.lblLgFileAttente.TabIndex = 29
    Me.lblLgFileAttente.Text = "Longueur file d'attente (m)"
    '
    'lblDiagVéhicules
    '
    Me.lblDiagVéhicules.Location = New System.Drawing.Point(16, 16)
    Me.lblDiagVéhicules.Name = "lblDiagVéhicules"
    Me.lblDiagVéhicules.Size = New System.Drawing.Size(64, 16)
    Me.lblDiagVéhicules.TabIndex = 28
    Me.lblDiagVéhicules.Text = "Véhicules"
    '
    'lblDiagPiétons
    '
    Me.lblDiagPiétons.Location = New System.Drawing.Point(16, 248)
    Me.lblDiagPiétons.Name = "lblDiagPiétons"
    Me.lblDiagPiétons.Size = New System.Drawing.Size(56, 16)
    Me.lblDiagPiétons.TabIndex = 27
    Me.lblDiagPiétons.Text = "Piétons :"
    '
    'lblSeconde
    '
    Me.lblSeconde.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblSeconde.Location = New System.Drawing.Point(680, 216)
    Me.lblSeconde.Name = "lblSeconde"
    Me.lblSeconde.Size = New System.Drawing.Size(24, 16)
    Me.lblSeconde.TabIndex = 26
    Me.lblSeconde.Text = "s"
    '
    'txtTempsAttenteVéhicules
    '
    Me.txtTempsAttenteVéhicules.BackColor = System.Drawing.SystemColors.Window
    Me.txtTempsAttenteVéhicules.Location = New System.Drawing.Point(568, 216)
    Me.txtTempsAttenteVéhicules.Name = "txtTempsAttenteVéhicules"
    Me.txtTempsAttenteVéhicules.ReadOnly = True
    Me.txtTempsAttenteVéhicules.Size = New System.Drawing.Size(40, 20)
    Me.txtTempsAttenteVéhicules.TabIndex = 25
    Me.txtTempsAttenteVéhicules.Text = ""
    Me.txtTempsAttenteVéhicules.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsMoyenVéhicules
    '
    Me.lblTempsMoyenVéhicules.Location = New System.Drawing.Point(488, 216)
    Me.lblTempsMoyenVéhicules.Name = "lblTempsMoyenVéhicules"
    Me.lblTempsMoyenVéhicules.Size = New System.Drawing.Size(88, 16)
    Me.lblTempsMoyenVéhicules.TabIndex = 24
    Me.lblTempsMoyenVéhicules.Text = "Temps moyen :"
    '
    'lvwPiétons
    '
    Me.lvwPiétons.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLP, Me.lvwcolTP, Me.lvwcolVertPiétons, Me.lvwcolTMAPiéton})
    Me.lvwPiétons.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lvwPiétons.FullRowSelect = True
    Me.lvwPiétons.GridLines = True
    Me.lvwPiétons.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwPiétons.HideSelection = False
    Me.lvwPiétons.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4})
    Me.lvwPiétons.Location = New System.Drawing.Point(16, 264)
    Me.lvwPiétons.MultiSelect = False
    Me.lvwPiétons.Name = "lvwPiétons"
    Me.lvwPiétons.Size = New System.Drawing.Size(289, 90)
    Me.lvwPiétons.TabIndex = 38
    Me.lvwPiétons.View = System.Windows.Forms.View.Details
    '
    'lvwcolLP
    '
    Me.lvwcolLP.Text = "Ligne de feux"
    Me.lvwcolLP.Width = 57
    '
    'lvwcolTP
    '
    Me.lvwcolTP.Text = "Flux"
    Me.lvwcolTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolTP.Width = 73
    '
    'lvwcolVertPiétons
    '
    Me.lvwcolVertPiétons.Text = "Vert réel"
    Me.lvwcolVertPiétons.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolVertPiétons.Width = 72
    '
    'lvwcolTMAPiéton
    '
    Me.lvwcolTMAPiéton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolTMAPiéton.Width = 83
    '
    'lvwVéhicules
    '
    Me.lvwVéhicules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLF, Me.lvwcolDemande, Me.lvwcolVert, Me.lvwcolTMA, Me.lvwcolNbVéhicules, Me.lvwcolLongueurFile})
    Me.lvwVéhicules.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lvwVéhicules.FullRowSelect = True
    Me.lvwVéhicules.GridLines = True
    Me.lvwVéhicules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwVéhicules.HideSelection = False
    Me.lvwVéhicules.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10})
    Me.lvwVéhicules.Location = New System.Drawing.Point(16, 48)
    Me.lvwVéhicules.MultiSelect = False
    Me.lvwVéhicules.Name = "lvwVéhicules"
    Me.lvwVéhicules.Size = New System.Drawing.Size(472, 184)
    Me.lvwVéhicules.TabIndex = 33
    Me.lvwVéhicules.View = System.Windows.Forms.View.Details
    '
    'lvwcolLF
    '
    Me.lvwcolLF.Text = "Ligne de feux"
    Me.lvwcolLF.Width = 57
    '
    'lvwcolDemande
    '
    Me.lvwcolDemande.Text = "Demande"
    Me.lvwcolDemande.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolDemande.Width = 73
    '
    'lvwcolVert
    '
    Me.lvwcolVert.Text = "Vert réel"
    Me.lvwcolVert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolVert.Width = 72
    '
    'lvwcolTMA
    '
    Me.lvwcolTMA.Text = "Temps perdu"
    Me.lvwcolTMA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolTMA.Width = 83
    '
    'lvwcolNbVéhicules
    '
    Me.lvwcolNbVéhicules.Text = "Nombre"
    Me.lvwcolNbVéhicules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolNbVéhicules.Width = 85
    '
    'lvwcolLongueurFile
    '
    Me.lvwcolLongueurFile.Text = "Longueur"
    Me.lvwcolLongueurFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolLongueurFile.Width = 88
    '
    'frmDiagnostic
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(632, 576)
    Me.Controls.Add(Me.pnlParamétrage)
    Me.Controls.Add(Me.pnlCommentaires)
    Me.Controls.Add(Me.grpDiagnostic)
    Me.Controls.Add(Me.grpTemps)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
    Me.Location = New System.Drawing.Point(400, 100)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(700, 600)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(300, 320)
    Me.Name = "frmDiagnostic"
    Me.ShowInTaskbar = False
    Me.Text = "Diagnostic"
    Me.grpDiagnostic.ResumeLayout(False)
    Me.pnlCommentaires.ResumeLayout(False)
    Me.pnlParamétrage.ResumeLayout(False)
    Me.grpTemps.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "Déclarations"
  Private monPlanFeuxActif As PlanFeuxFonctionnement
  Private mesLignesFeux As LigneFeuxCollection
  Private mesBranches As BrancheCollection

  Private mEnVeille As Boolean
  Private DéfautLargeurPiétons, DéfautLargeurVéhicules As Short

#End Region

#Region "Procédures"
  Public Sub AfficherCapacité()

    With monPlanFeuxActif.mVariante
      Me.txtVitesseVéhicule.Text = .VitesseVéhicules
      Me.txtVitessePiéton.Text = .VitessePiétons
      Me.txtVitesseVélo.Text = .VitesseVélos
      Me.txtDébitSaturation.Text = .DébitSaturation
      Me.lblSecondesVert.Text = .strVertUtile
    End With

    If IsNothing(monPlanFeuxActif) Then
      Me.txtDuréeCycleBase.Text = ""
      Me.txtDemande.Text = ""
      Me.txtCapacitéPlan.Text = ""
      Me.txtRéserveCapacité.Text = ""
      Me.txtRéservePourCent.Text = ""

    Else
      With monPlanFeuxActif
        Me.txtDuréeCycleBase.Text = .DuréeCycle
        Me.txtDemande.Text = .Demande
        Me.txtCapacitéPlan.Text = CType(Math.Round(.CapacitéThéorique), String)
        Me.txtRéserveCapacité.Text = CType(Math.Round(.RéserveCapacité), String)
        Me.txtRéservePourCent.Text = .strRéserveCapacitéPourCent
        Me.txtTempsPerduCycle.Text = .TempsPerdu
      End With ' monPlanFeuxActif
    End If

    AfficherInfosAttente()
  End Sub

  Public Sub AfficherInfosAttente()
    Dim uneLigneFeux As LigneFeux
    Dim itmX As ListViewItem
    Dim IDLigneFeux As String

    Me.lvwVéhicules.Items.Clear()
    Me.lvwPiétons.Items.Clear()

    If IsNothing(monPlanFeuxActif) Then
      Me.txtTempsAttenteVéhicules.Text = ""
      Me.txtTempsAttentePiétons.Text = ""

    Else
      monPlanFeuxActif.AffecterInfosAttente()

      For Each uneLigneFeux In mesLignesFeux
        IDLigneFeux = uneLigneFeux.ID & " (" & mesBranches.ID(uneLigneFeux.mBranche) & ")"
        If uneLigneFeux.EstVéhicule Then
          With CType(monPlanFeuxActif, PlanFeuxFonctionnement)
            itmX = New ListViewItem(New String() {IDLigneFeux, "0", "0", "0", "0", "0", "0"})
            Me.lvwVéhicules.Items.Add(itmX)
            itmX.SubItems(1).Text = .DemandeUVP(uneLigneFeux)
            'Vert utile ( voir si réekl in téressant (?)
            itmX.SubItems(2).Text = .VertUtile(uneLigneFeux)
            'Temps moyen d'attente
            itmX.SubItems(3).Text = Format(.RetardMoyen(uneLigneFeux), "###")
            'Nombre de véhicules par file
            itmX.SubItems(4).Text = .NbVéhiculesEnAttente(uneLigneFeux)
            'Longueur file
            itmX.SubItems(5).Text = Format(.LgFileAttente(uneLigneFeux), "###")
          End With

        Else
          With monPlanFeuxActif
            itmX = New ListViewItem(New String() {IDLigneFeux, "0", "0", "0"})
            Me.lvwPiétons.Items.Add(itmX)
            'Trafic piétons sur la branche traversée
            itmX.SubItems(1).Text = monPlanFeuxActif.Trafic.QPiéton(uneLigneFeux.mBranche)
            'Vert réel
            itmX.SubItems(2).Text = .VertUtile(uneLigneFeux)
            'Temps moyen d'attente
            If monPlanFeuxActif.Trafic.QPiéton(uneLigneFeux.mBranche) > 0 Then
              itmX.SubItems(3).Text = Format(.RetardMoyen(uneLigneFeux), "###")
            Else
              itmX.SubItems(3).Text = ""
            End If
          End With

        End If

      Next

      Me.txtTempsAttenteVéhicules.Text = CType(monPlanFeuxActif, PlanFeuxFonctionnement).TMAVéhicules
      Me.txtTempsAttentePiétons.Text = CType(monPlanFeuxActif, PlanFeuxFonctionnement).TMAPiétons

    End If

  End Sub

  Public Sub RenommerColonnePlanFeux(ByVal uneLigneRenommée As LigneFeux, ByVal Position As Short)
    Dim lstItems As ListView.ListViewItemCollection
    Dim Index As Short
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In mesLignesFeux
      If uneLigneFeux.EstVéhicule Xor uneLigneRenommée.EstPiéton Then
        If mesLignesFeux.IndexOf(uneLigneFeux) = Position Then
          Exit For
        End If
        Index += 1
      End If
    Next

    If uneLigneFeux.EstVéhicule Then
      lstItems = Me.lvwVéhicules.Items
    Else
      lstItems = Me.lvwPiétons.Items
    End If
    lstItems(Index).SubItems(0).Text = uneLigneFeux.ID & " (" & mesBranches.ID(uneLigneFeux.mBranche) & ")"

  End Sub

  Public Sub AffecterPlanFeux(ByVal unPlanFeux As PlanFeuxFonctionnement)

    If Not IsNothing(unPlanFeux) AndAlso Not IsNothing(unPlanFeux.Trafic) Then
      monPlanFeuxActif = unPlanFeux
      mesLignesFeux = unPlanFeux.mLignesFeux
      mesBranches = unPlanFeux.mVariante.mBranches
    Else
      monPlanFeuxActif = Nothing
    End If

  End Sub

  Public ReadOnly Property PlanFeu() As PlanFeuxFonctionnement
    Get
      Return monPlanFeuxActif
    End Get
  End Property
#End Region

#Region "Fonctions de la feuille"
  Private Sub frmDiagnostic_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    DéfautLargeurVéhicules = Me.lvwVéhicules.Width
    DéfautLargeurPiétons = Me.lvwPiétons.Width

  End Sub

  Private Sub frmDiagnostic_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
    If Me.Visible Then
      With lvwVéhicules
        If .Items.Count > 13 Then
          .Width = DéfautLargeurVéhicules + 15
        Else
          .Width = DéfautLargeurVéhicules
        End If
      End With

      With lvwPiétons
        If .Items.Count > 6 Then
          .Width = DéfautLargeurPiétons + 15
        Else
          .Width = DéfautLargeurPiétons
        End If
      End With

      Me.BringToFront()

    End If
  End Sub

  Private Sub frmDiagnostic_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Me.Hide()
    EnVeille = False
    e.Cancel = True
  End Sub
#End Region

  Public Property EnVeille() As Boolean
    Get
      Return mEnVeille
    End Get
    Set(ByVal Value As Boolean)
      mEnVeille = Value
    End Set
  End Property

End Class
