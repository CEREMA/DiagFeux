'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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
  Friend WithEvents grpDiagnostic As System.Windows.Forms.GroupBox
  Friend WithEvents txtR�servePourCent As System.Windows.Forms.TextBox
  Friend WithEvents lblUvpdHeure As System.Windows.Forms.Label
  Friend WithEvents lblR�serveCapacit� As System.Windows.Forms.Label
  Friend WithEvents pnlCommentaires As System.Windows.Forms.Panel
  Friend WithEvents lblCommentairesPlanFeux As System.Windows.Forms.Label
  Friend WithEvents txtCommentairesFct As System.Windows.Forms.TextBox
  Friend WithEvents pnlParam�trage As System.Windows.Forms.Panel
  Friend WithEvents lblVitesse As System.Windows.Forms.Label
  Friend WithEvents lblMSPi�tons As System.Windows.Forms.Label
  Friend WithEvents txtVitessePi�ton As System.Windows.Forms.TextBox
  Friend WithEvents lblPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblMSV�hicules As System.Windows.Forms.Label
  Friend WithEvents txtVitesseV�hicule As System.Windows.Forms.TextBox
  Friend WithEvents lblV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblMSV�los As System.Windows.Forms.Label
  Friend WithEvents txtVitesseV�lo As System.Windows.Forms.TextBox
  Friend WithEvents lblV�los As System.Windows.Forms.Label
  Friend WithEvents lblUvpd As System.Windows.Forms.Label
  Friend WithEvents txtD�bitSaturation As System.Windows.Forms.TextBox
  Friend WithEvents lblD�bitSaturation As System.Windows.Forms.Label
  Friend WithEvents lblVertUtile As System.Windows.Forms.Label
  Friend WithEvents lblSecondesVert As System.Windows.Forms.Label
  Friend WithEvents lblCycle As System.Windows.Forms.Label
  Friend WithEvents lblDemande As System.Windows.Forms.Label
  Friend WithEvents txtDur�eCycleBase As System.Windows.Forms.TextBox
  Friend WithEvents txtDemande As System.Windows.Forms.TextBox
  Friend WithEvents txtCapacit�Plan As System.Windows.Forms.TextBox
  Friend WithEvents grpTemps As System.Windows.Forms.GroupBox
  Friend WithEvents txtTempsAttentePi�tons As System.Windows.Forms.TextBox
  Friend WithEvents lblTempsAttenteFile As System.Windows.Forms.Label
  Friend WithEvents lblLgFileAttente As System.Windows.Forms.Label
  Friend WithEvents lblSeconde As System.Windows.Forms.Label
  Friend WithEvents txtTempsAttenteV�hicules As System.Windows.Forms.TextBox
  Friend WithEvents lvwcolLP As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTP As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolLF As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDemande As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolVert As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolNbV�hicules As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolLongueurFile As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTMA As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolVertPi�tons As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolTMAPi�ton As System.Windows.Forms.ColumnHeader
  Friend WithEvents lblVertPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblFluxPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblUVPDR�serveCapacit� As System.Windows.Forms.Label
  Friend WithEvents lblUVPDCapacit�Plan As System.Windows.Forms.Label
  Friend WithEvents lblCapacit�Plan As System.Windows.Forms.Label
  Friend WithEvents lblSecondesCapacit� As System.Windows.Forms.Label
  Friend WithEvents lblTempsAttentePi�tons As System.Windows.Forms.Label
  Friend WithEvents lblTempsMoyenPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblNbV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblVertV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblDemandeUVP As System.Windows.Forms.Label
  Friend WithEvents lblDiagV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblDiagPi�tons As System.Windows.Forms.Label
  Friend WithEvents lblTempsMoyenV�hicules As System.Windows.Forms.Label
  Friend WithEvents lblTempsPerduCycle As System.Windows.Forms.Label
  Friend WithEvents txtTempsPerduCycle As System.Windows.Forms.TextBox
  Friend WithEvents lvwPi�tons As System.Windows.Forms.ListView
  Friend WithEvents lvwV�hicules As System.Windows.Forms.ListView
  Friend WithEvents txtR�serveCapacit� As System.Windows.Forms.TextBox
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
    Me.lblUVPDR�serveCapacit� = New System.Windows.Forms.Label
    Me.lblUVPDCapacit�Plan = New System.Windows.Forms.Label
    Me.txtCapacit�Plan = New System.Windows.Forms.TextBox
    Me.lblCapacit�Plan = New System.Windows.Forms.Label
    Me.lblSecondesCapacit� = New System.Windows.Forms.Label
    Me.txtDemande = New System.Windows.Forms.TextBox
    Me.txtDur�eCycleBase = New System.Windows.Forms.TextBox
    Me.lblDemande = New System.Windows.Forms.Label
    Me.lblCycle = New System.Windows.Forms.Label
    Me.txtR�servePourCent = New System.Windows.Forms.TextBox
    Me.lblUvpdHeure = New System.Windows.Forms.Label
    Me.txtR�serveCapacit� = New System.Windows.Forms.TextBox
    Me.lblR�serveCapacit� = New System.Windows.Forms.Label
    Me.pnlCommentaires = New System.Windows.Forms.Panel
    Me.lblCommentairesPlanFeux = New System.Windows.Forms.Label
    Me.txtCommentairesFct = New System.Windows.Forms.TextBox
    Me.pnlParam�trage = New System.Windows.Forms.Panel
    Me.lblPi�tons = New System.Windows.Forms.Label
    Me.lblSecondesVert = New System.Windows.Forms.Label
    Me.lblVertUtile = New System.Windows.Forms.Label
    Me.lblUvpd = New System.Windows.Forms.Label
    Me.txtD�bitSaturation = New System.Windows.Forms.TextBox
    Me.lblD�bitSaturation = New System.Windows.Forms.Label
    Me.lblMSV�los = New System.Windows.Forms.Label
    Me.txtVitesseV�lo = New System.Windows.Forms.TextBox
    Me.lblV�los = New System.Windows.Forms.Label
    Me.lblMSV�hicules = New System.Windows.Forms.Label
    Me.txtVitesseV�hicule = New System.Windows.Forms.TextBox
    Me.lblV�hicules = New System.Windows.Forms.Label
    Me.lblMSPi�tons = New System.Windows.Forms.Label
    Me.txtVitessePi�ton = New System.Windows.Forms.TextBox
    Me.lblVitesse = New System.Windows.Forms.Label
    Me.grpTemps = New System.Windows.Forms.GroupBox
    Me.lblTempsAttentePi�tons = New System.Windows.Forms.Label
    Me.txtTempsAttentePi�tons = New System.Windows.Forms.TextBox
    Me.lblTempsMoyenPi�tons = New System.Windows.Forms.Label
    Me.lblVertPi�tons = New System.Windows.Forms.Label
    Me.lblFluxPi�tons = New System.Windows.Forms.Label
    Me.lblNbV�hicules = New System.Windows.Forms.Label
    Me.lblVertV�hicules = New System.Windows.Forms.Label
    Me.lblDemandeUVP = New System.Windows.Forms.Label
    Me.lblTempsAttenteFile = New System.Windows.Forms.Label
    Me.lblLgFileAttente = New System.Windows.Forms.Label
    Me.lblDiagV�hicules = New System.Windows.Forms.Label
    Me.lblDiagPi�tons = New System.Windows.Forms.Label
    Me.lblSeconde = New System.Windows.Forms.Label
    Me.txtTempsAttenteV�hicules = New System.Windows.Forms.TextBox
    Me.lblTempsMoyenV�hicules = New System.Windows.Forms.Label
    Me.lvwPi�tons = New System.Windows.Forms.ListView
    Me.lvwcolLP = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTP = New System.Windows.Forms.ColumnHeader
    Me.lvwcolVertPi�tons = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTMAPi�ton = New System.Windows.Forms.ColumnHeader
    Me.lvwV�hicules = New System.Windows.Forms.ListView
    Me.lvwcolLF = New System.Windows.Forms.ColumnHeader
    Me.lvwcolDemande = New System.Windows.Forms.ColumnHeader
    Me.lvwcolVert = New System.Windows.Forms.ColumnHeader
    Me.lvwcolTMA = New System.Windows.Forms.ColumnHeader
    Me.lvwcolNbV�hicules = New System.Windows.Forms.ColumnHeader
    Me.lvwcolLongueurFile = New System.Windows.Forms.ColumnHeader
    Me.grpDiagnostic.SuspendLayout()
    Me.pnlCommentaires.SuspendLayout()
    Me.pnlParam�trage.SuspendLayout()
    Me.grpTemps.SuspendLayout()
    Me.SuspendLayout()
    '
    'grpDiagnostic
    '
    Me.grpDiagnostic.Controls.Add(Me.txtTempsPerduCycle)
    Me.grpDiagnostic.Controls.Add(Me.lblTempsPerduCycle)
    Me.grpDiagnostic.Controls.Add(Me.lblUVPDR�serveCapacit�)
    Me.grpDiagnostic.Controls.Add(Me.lblUVPDCapacit�Plan)
    Me.grpDiagnostic.Controls.Add(Me.txtCapacit�Plan)
    Me.grpDiagnostic.Controls.Add(Me.lblCapacit�Plan)
    Me.grpDiagnostic.Controls.Add(Me.lblSecondesCapacit�)
    Me.grpDiagnostic.Controls.Add(Me.txtDemande)
    Me.grpDiagnostic.Controls.Add(Me.txtDur�eCycleBase)
    Me.grpDiagnostic.Controls.Add(Me.lblDemande)
    Me.grpDiagnostic.Controls.Add(Me.lblCycle)
    Me.grpDiagnostic.Controls.Add(Me.txtR�servePourCent)
    Me.grpDiagnostic.Controls.Add(Me.lblUvpdHeure)
    Me.grpDiagnostic.Controls.Add(Me.txtR�serveCapacit�)
    Me.grpDiagnostic.Controls.Add(Me.lblR�serveCapacit�)
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
    'lblUVPDR�serveCapacit�
    '
    Me.lblUVPDR�serveCapacit�.Location = New System.Drawing.Point(208, 40)
    Me.lblUVPDR�serveCapacit�.Name = "lblUVPDR�serveCapacit�"
    Me.lblUVPDR�serveCapacit�.Size = New System.Drawing.Size(88, 16)
    Me.lblUVPDR�serveCapacit�.TabIndex = 37
    Me.lblUVPDR�serveCapacit�.Text = "(uvpd/h et voie)"
    '
    'lblUVPDCapacit�Plan
    '
    Me.lblUVPDCapacit�Plan.Location = New System.Drawing.Point(360, 64)
    Me.lblUVPDCapacit�Plan.Name = "lblUVPDCapacit�Plan"
    Me.lblUVPDCapacit�Plan.Size = New System.Drawing.Size(88, 16)
    Me.lblUVPDCapacit�Plan.TabIndex = 36
    Me.lblUVPDCapacit�Plan.Text = "(uvpd/h et voie)"
    '
    'txtCapacit�Plan
    '
    Me.txtCapacit�Plan.BackColor = System.Drawing.SystemColors.Window
    Me.txtCapacit�Plan.Location = New System.Drawing.Point(496, 48)
    Me.txtCapacit�Plan.Name = "txtCapacit�Plan"
    Me.txtCapacit�Plan.ReadOnly = True
    Me.txtCapacit�Plan.Size = New System.Drawing.Size(48, 20)
    Me.txtCapacit�Plan.TabIndex = 35
    Me.txtCapacit�Plan.Text = "190"
    Me.txtCapacit�Plan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblCapacit�Plan
    '
    Me.lblCapacit�Plan.Location = New System.Drawing.Point(360, 48)
    Me.lblCapacit�Plan.Name = "lblCapacit�Plan"
    Me.lblCapacit�Plan.Size = New System.Drawing.Size(136, 23)
    Me.lblCapacit�Plan.TabIndex = 34
    Me.lblCapacit�Plan.Text = "Capacit� du plan de feux:"
    '
    'lblSecondesCapacit�
    '
    Me.lblSecondesCapacit�.Location = New System.Drawing.Point(336, 48)
    Me.lblSecondesCapacit�.Name = "lblSecondesCapacit�"
    Me.lblSecondesCapacit�.Size = New System.Drawing.Size(24, 16)
    Me.lblSecondesCapacit�.TabIndex = 33
    Me.lblSecondesCapacit�.Text = "%"
    Me.lblSecondesCapacit�.Visible = False
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
    'txtDur�eCycleBase
    '
    Me.txtDur�eCycleBase.BackColor = System.Drawing.SystemColors.Window
    Me.txtDur�eCycleBase.Location = New System.Drawing.Point(152, 24)
    Me.txtDur�eCycleBase.Name = "txtDur�eCycleBase"
    Me.txtDur�eCycleBase.ReadOnly = True
    Me.txtDur�eCycleBase.Size = New System.Drawing.Size(24, 20)
    Me.txtDur�eCycleBase.TabIndex = 31
    Me.txtDur�eCycleBase.Text = "60"
    Me.txtDur�eCycleBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
    Me.lblCycle.Text = "Dur�e du cycle (s) :"
    '
    'txtR�servePourCent
    '
    Me.txtR�servePourCent.BackColor = System.Drawing.SystemColors.Window
    Me.txtR�servePourCent.Location = New System.Drawing.Point(304, 48)
    Me.txtR�servePourCent.Name = "txtR�servePourCent"
    Me.txtR�servePourCent.ReadOnly = True
    Me.txtR�servePourCent.Size = New System.Drawing.Size(32, 20)
    Me.txtR�servePourCent.TabIndex = 17
    Me.txtR�servePourCent.Text = "17"
    Me.txtR�servePourCent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblUvpdHeure
    '
    Me.lblUvpdHeure.Location = New System.Drawing.Point(16, 64)
    Me.lblUvpdHeure.Name = "lblUvpdHeure"
    Me.lblUvpdHeure.Size = New System.Drawing.Size(88, 16)
    Me.lblUvpdHeure.TabIndex = 2
    Me.lblUvpdHeure.Text = "(uvpd/h et voie)"
    '
    'txtR�serveCapacit�
    '
    Me.txtR�serveCapacit�.BackColor = System.Drawing.SystemColors.Window
    Me.txtR�serveCapacit�.Location = New System.Drawing.Point(304, 24)
    Me.txtR�serveCapacit�.Name = "txtR�serveCapacit�"
    Me.txtR�serveCapacit�.ReadOnly = True
    Me.txtR�serveCapacit�.Size = New System.Drawing.Size(40, 20)
    Me.txtR�serveCapacit�.TabIndex = 1
    Me.txtR�serveCapacit�.Text = "190"
    Me.txtR�serveCapacit�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblR�serveCapacit�
    '
    Me.lblR�serveCapacit�.Location = New System.Drawing.Point(192, 24)
    Me.lblR�serveCapacit�.Name = "lblR�serveCapacit�"
    Me.lblR�serveCapacit�.Size = New System.Drawing.Size(120, 23)
    Me.lblR�serveCapacit�.TabIndex = 0
    Me.lblR�serveCapacit�.Text = "R�serve de capacit� :"
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
    'pnlParam�trage
    '
    Me.pnlParam�trage.Controls.Add(Me.lblPi�tons)
    Me.pnlParam�trage.Controls.Add(Me.lblSecondesVert)
    Me.pnlParam�trage.Controls.Add(Me.lblVertUtile)
    Me.pnlParam�trage.Controls.Add(Me.lblUvpd)
    Me.pnlParam�trage.Controls.Add(Me.txtD�bitSaturation)
    Me.pnlParam�trage.Controls.Add(Me.lblD�bitSaturation)
    Me.pnlParam�trage.Controls.Add(Me.lblMSV�los)
    Me.pnlParam�trage.Controls.Add(Me.txtVitesseV�lo)
    Me.pnlParam�trage.Controls.Add(Me.lblV�los)
    Me.pnlParam�trage.Controls.Add(Me.lblMSV�hicules)
    Me.pnlParam�trage.Controls.Add(Me.txtVitesseV�hicule)
    Me.pnlParam�trage.Controls.Add(Me.lblV�hicules)
    Me.pnlParam�trage.Controls.Add(Me.lblMSPi�tons)
    Me.pnlParam�trage.Controls.Add(Me.txtVitessePi�ton)
    Me.pnlParam�trage.Controls.Add(Me.lblVitesse)
    Me.pnlParam�trage.Location = New System.Drawing.Point(8, 8)
    Me.pnlParam�trage.Name = "pnlParam�trage"
    Me.pnlParam�trage.Size = New System.Drawing.Size(616, 64)
    Me.pnlParam�trage.TabIndex = 48
    '
    'lblPi�tons
    '
    Me.lblPi�tons.Location = New System.Drawing.Point(144, 24)
    Me.lblPi�tons.Name = "lblPi�tons"
    Me.lblPi�tons.Size = New System.Drawing.Size(48, 16)
    Me.lblPi�tons.TabIndex = 20
    Me.lblPi�tons.Text = "Pi�tons :"
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
    Me.lblVertUtile.Text = "Vert utile = vert r�el "
    '
    'lblUvpd
    '
    Me.lblUvpd.Location = New System.Drawing.Point(136, 40)
    Me.lblUvpd.Name = "lblUvpd"
    Me.lblUvpd.Size = New System.Drawing.Size(48, 16)
    Me.lblUvpd.TabIndex = 31
    Me.lblUvpd.Text = "uvpd/h"
    '
    'txtD�bitSaturation
    '
    Me.txtD�bitSaturation.BackColor = System.Drawing.SystemColors.Control
    Me.txtD�bitSaturation.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtD�bitSaturation.Location = New System.Drawing.Point(104, 40)
    Me.txtD�bitSaturation.Name = "txtD�bitSaturation"
    Me.txtD�bitSaturation.Size = New System.Drawing.Size(32, 13)
    Me.txtD�bitSaturation.TabIndex = 30
    Me.txtD�bitSaturation.Text = "1700"
    Me.txtD�bitSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblD�bitSaturation
    '
    Me.lblD�bitSaturation.Location = New System.Drawing.Point(8, 40)
    Me.lblD�bitSaturation.Name = "lblD�bitSaturation"
    Me.lblD�bitSaturation.Size = New System.Drawing.Size(112, 16)
    Me.lblD�bitSaturation.TabIndex = 29
    Me.lblD�bitSaturation.Text = "D�bit de saturation :"
    '
    'lblMSV�los
    '
    Me.lblMSV�los.Location = New System.Drawing.Point(448, 24)
    Me.lblMSV�los.Name = "lblMSV�los"
    Me.lblMSV�los.Size = New System.Drawing.Size(24, 24)
    Me.lblMSV�los.TabIndex = 28
    Me.lblMSV�los.Text = "m/s"
    '
    'txtVitesseV�lo
    '
    Me.txtVitesseV�lo.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitesseV�lo.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitesseV�lo.Location = New System.Drawing.Point(424, 24)
    Me.txtVitesseV�lo.Name = "txtVitesseV�lo"
    Me.txtVitesseV�lo.Size = New System.Drawing.Size(16, 13)
    Me.txtVitesseV�lo.TabIndex = 27
    Me.txtVitesseV�lo.Text = "7"
    Me.txtVitesseV�lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblV�los
    '
    Me.lblV�los.Location = New System.Drawing.Point(384, 24)
    Me.lblV�los.Name = "lblV�los"
    Me.lblV�los.Size = New System.Drawing.Size(40, 16)
    Me.lblV�los.TabIndex = 26
    Me.lblV�los.Text = "V�los :"
    '
    'lblMSV�hicules
    '
    Me.lblMSV�hicules.Location = New System.Drawing.Point(344, 24)
    Me.lblMSV�hicules.Name = "lblMSV�hicules"
    Me.lblMSV�hicules.Size = New System.Drawing.Size(24, 24)
    Me.lblMSV�hicules.TabIndex = 25
    Me.lblMSV�hicules.Text = "m/s"
    '
    'txtVitesseV�hicule
    '
    Me.txtVitesseV�hicule.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitesseV�hicule.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitesseV�hicule.Location = New System.Drawing.Point(328, 24)
    Me.txtVitesseV�hicule.Name = "txtVitesseV�hicule"
    Me.txtVitesseV�hicule.Size = New System.Drawing.Size(16, 13)
    Me.txtVitesseV�hicule.TabIndex = 24
    Me.txtVitesseV�hicule.Text = "8"
    Me.txtVitesseV�hicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblV�hicules
    '
    Me.lblV�hicules.Location = New System.Drawing.Point(264, 24)
    Me.lblV�hicules.Name = "lblV�hicules"
    Me.lblV�hicules.Size = New System.Drawing.Size(64, 16)
    Me.lblV�hicules.TabIndex = 23
    Me.lblV�hicules.Text = "V�hicules :"
    '
    'lblMSPi�tons
    '
    Me.lblMSPi�tons.Location = New System.Drawing.Point(216, 24)
    Me.lblMSPi�tons.Name = "lblMSPi�tons"
    Me.lblMSPi�tons.Size = New System.Drawing.Size(24, 24)
    Me.lblMSPi�tons.TabIndex = 22
    Me.lblMSPi�tons.Text = "m/s"
    '
    'txtVitessePi�ton
    '
    Me.txtVitessePi�ton.BackColor = System.Drawing.SystemColors.Control
    Me.txtVitessePi�ton.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtVitessePi�ton.Location = New System.Drawing.Point(184, 24)
    Me.txtVitessePi�ton.Name = "txtVitessePi�ton"
    Me.txtVitessePi�ton.Size = New System.Drawing.Size(24, 13)
    Me.txtVitessePi�ton.TabIndex = 21
    Me.txtVitessePi�ton.Text = "0.8"
    Me.txtVitessePi�ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblVitesse
    '
    Me.lblVitesse.Location = New System.Drawing.Point(8, 24)
    Me.lblVitesse.Name = "lblVitesse"
    Me.lblVitesse.Size = New System.Drawing.Size(136, 16)
    Me.lblVitesse.TabIndex = 0
    Me.lblVitesse.Text = "Vitesses de d�gagement"
    '
    'grpTemps
    '
    Me.grpTemps.Controls.Add(Me.lblTempsAttentePi�tons)
    Me.grpTemps.Controls.Add(Me.txtTempsAttentePi�tons)
    Me.grpTemps.Controls.Add(Me.lblTempsMoyenPi�tons)
    Me.grpTemps.Controls.Add(Me.lblVertPi�tons)
    Me.grpTemps.Controls.Add(Me.lblFluxPi�tons)
    Me.grpTemps.Controls.Add(Me.lblNbV�hicules)
    Me.grpTemps.Controls.Add(Me.lblVertV�hicules)
    Me.grpTemps.Controls.Add(Me.lblDemandeUVP)
    Me.grpTemps.Controls.Add(Me.lblTempsAttenteFile)
    Me.grpTemps.Controls.Add(Me.lblLgFileAttente)
    Me.grpTemps.Controls.Add(Me.lblDiagV�hicules)
    Me.grpTemps.Controls.Add(Me.lblDiagPi�tons)
    Me.grpTemps.Controls.Add(Me.lblSeconde)
    Me.grpTemps.Controls.Add(Me.txtTempsAttenteV�hicules)
    Me.grpTemps.Controls.Add(Me.lblTempsMoyenV�hicules)
    Me.grpTemps.Controls.Add(Me.lvwPi�tons)
    Me.grpTemps.Controls.Add(Me.lvwV�hicules)
    Me.grpTemps.Location = New System.Drawing.Point(8, 184)
    Me.grpTemps.Name = "grpTemps"
    Me.grpTemps.Size = New System.Drawing.Size(616, 376)
    Me.grpTemps.TabIndex = 50
    Me.grpTemps.TabStop = False
    '
    'lblTempsAttentePi�tons
    '
    Me.lblTempsAttentePi�tons.Location = New System.Drawing.Point(216, 248)
    Me.lblTempsAttentePi�tons.Name = "lblTempsAttentePi�tons"
    Me.lblTempsAttentePi�tons.Size = New System.Drawing.Size(104, 16)
    Me.lblTempsAttentePi�tons.TabIndex = 44
    Me.lblTempsAttentePi�tons.Text = "Temps d'attente (s)"
    '
    'txtTempsAttentePi�tons
    '
    Me.txtTempsAttentePi�tons.BackColor = System.Drawing.SystemColors.Window
    Me.txtTempsAttentePi�tons.Location = New System.Drawing.Point(400, 336)
    Me.txtTempsAttentePi�tons.Name = "txtTempsAttentePi�tons"
    Me.txtTempsAttentePi�tons.ReadOnly = True
    Me.txtTempsAttentePi�tons.Size = New System.Drawing.Size(40, 20)
    Me.txtTempsAttentePi�tons.TabIndex = 43
    Me.txtTempsAttentePi�tons.Text = ""
    Me.txtTempsAttentePi�tons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsMoyenPi�tons
    '
    Me.lblTempsMoyenPi�tons.Location = New System.Drawing.Point(320, 336)
    Me.lblTempsMoyenPi�tons.Name = "lblTempsMoyenPi�tons"
    Me.lblTempsMoyenPi�tons.Size = New System.Drawing.Size(88, 16)
    Me.lblTempsMoyenPi�tons.TabIndex = 42
    Me.lblTempsMoyenPi�tons.Text = "Temps moyen :"
    '
    'lblVertPi�tons
    '
    Me.lblVertPi�tons.Location = New System.Drawing.Point(160, 248)
    Me.lblVertPi�tons.Name = "lblVertPi�tons"
    Me.lblVertPi�tons.Size = New System.Drawing.Size(88, 16)
    Me.lblVertPi�tons.TabIndex = 40
    Me.lblVertPi�tons.Text = "Vert (s)"
    '
    'lblFluxPi�tons
    '
    Me.lblFluxPi�tons.Location = New System.Drawing.Point(104, 248)
    Me.lblFluxPi�tons.Name = "lblFluxPi�tons"
    Me.lblFluxPi�tons.Size = New System.Drawing.Size(32, 16)
    Me.lblFluxPi�tons.TabIndex = 39
    Me.lblFluxPi�tons.Text = "Flux"
    '
    'lblNbV�hicules
    '
    Me.lblNbV�hicules.Location = New System.Drawing.Point(304, 16)
    Me.lblNbV�hicules.Name = "lblNbV�hicules"
    Me.lblNbV�hicules.Size = New System.Drawing.Size(96, 32)
    Me.lblNbV�hicules.TabIndex = 37
    Me.lblNbV�hicules.Text = "Nombre de v�hicules par file"
    '
    'lblVertV�hicules
    '
    Me.lblVertV�hicules.Location = New System.Drawing.Point(152, 16)
    Me.lblVertV�hicules.Name = "lblVertV�hicules"
    Me.lblVertV�hicules.Size = New System.Drawing.Size(64, 32)
    Me.lblVertV�hicules.TabIndex = 35
    Me.lblVertV�hicules.Text = "Vert utile(s)"
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
    'lblDiagV�hicules
    '
    Me.lblDiagV�hicules.Location = New System.Drawing.Point(16, 16)
    Me.lblDiagV�hicules.Name = "lblDiagV�hicules"
    Me.lblDiagV�hicules.Size = New System.Drawing.Size(64, 16)
    Me.lblDiagV�hicules.TabIndex = 28
    Me.lblDiagV�hicules.Text = "V�hicules"
    '
    'lblDiagPi�tons
    '
    Me.lblDiagPi�tons.Location = New System.Drawing.Point(16, 248)
    Me.lblDiagPi�tons.Name = "lblDiagPi�tons"
    Me.lblDiagPi�tons.Size = New System.Drawing.Size(56, 16)
    Me.lblDiagPi�tons.TabIndex = 27
    Me.lblDiagPi�tons.Text = "Pi�tons :"
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
    'txtTempsAttenteV�hicules
    '
    Me.txtTempsAttenteV�hicules.BackColor = System.Drawing.SystemColors.Window
    Me.txtTempsAttenteV�hicules.Location = New System.Drawing.Point(568, 216)
    Me.txtTempsAttenteV�hicules.Name = "txtTempsAttenteV�hicules"
    Me.txtTempsAttenteV�hicules.ReadOnly = True
    Me.txtTempsAttenteV�hicules.Size = New System.Drawing.Size(40, 20)
    Me.txtTempsAttenteV�hicules.TabIndex = 25
    Me.txtTempsAttenteV�hicules.Text = ""
    Me.txtTempsAttenteV�hicules.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblTempsMoyenV�hicules
    '
    Me.lblTempsMoyenV�hicules.Location = New System.Drawing.Point(488, 216)
    Me.lblTempsMoyenV�hicules.Name = "lblTempsMoyenV�hicules"
    Me.lblTempsMoyenV�hicules.Size = New System.Drawing.Size(88, 16)
    Me.lblTempsMoyenV�hicules.TabIndex = 24
    Me.lblTempsMoyenV�hicules.Text = "Temps moyen :"
    '
    'lvwPi�tons
    '
    Me.lvwPi�tons.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLP, Me.lvwcolTP, Me.lvwcolVertPi�tons, Me.lvwcolTMAPi�ton})
    Me.lvwPi�tons.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lvwPi�tons.FullRowSelect = True
    Me.lvwPi�tons.GridLines = True
    Me.lvwPi�tons.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwPi�tons.HideSelection = False
    Me.lvwPi�tons.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4})
    Me.lvwPi�tons.Location = New System.Drawing.Point(16, 264)
    Me.lvwPi�tons.MultiSelect = False
    Me.lvwPi�tons.Name = "lvwPi�tons"
    Me.lvwPi�tons.Size = New System.Drawing.Size(289, 90)
    Me.lvwPi�tons.TabIndex = 38
    Me.lvwPi�tons.View = System.Windows.Forms.View.Details
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
    'lvwcolVertPi�tons
    '
    Me.lvwcolVertPi�tons.Text = "Vert r�el"
    Me.lvwcolVertPi�tons.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolVertPi�tons.Width = 72
    '
    'lvwcolTMAPi�ton
    '
    Me.lvwcolTMAPi�ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolTMAPi�ton.Width = 83
    '
    'lvwV�hicules
    '
    Me.lvwV�hicules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLF, Me.lvwcolDemande, Me.lvwcolVert, Me.lvwcolTMA, Me.lvwcolNbV�hicules, Me.lvwcolLongueurFile})
    Me.lvwV�hicules.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lvwV�hicules.FullRowSelect = True
    Me.lvwV�hicules.GridLines = True
    Me.lvwV�hicules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwV�hicules.HideSelection = False
    Me.lvwV�hicules.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10})
    Me.lvwV�hicules.Location = New System.Drawing.Point(16, 48)
    Me.lvwV�hicules.MultiSelect = False
    Me.lvwV�hicules.Name = "lvwV�hicules"
    Me.lvwV�hicules.Size = New System.Drawing.Size(472, 184)
    Me.lvwV�hicules.TabIndex = 33
    Me.lvwV�hicules.View = System.Windows.Forms.View.Details
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
    Me.lvwcolVert.Text = "Vert r�el"
    Me.lvwcolVert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolVert.Width = 72
    '
    'lvwcolTMA
    '
    Me.lvwcolTMA.Text = "Temps perdu"
    Me.lvwcolTMA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolTMA.Width = 83
    '
    'lvwcolNbV�hicules
    '
    Me.lvwcolNbV�hicules.Text = "Nombre"
    Me.lvwcolNbV�hicules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.lvwcolNbV�hicules.Width = 85
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
    Me.Controls.Add(Me.pnlParam�trage)
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
    Me.pnlParam�trage.ResumeLayout(False)
    Me.grpTemps.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "D�clarations"
  Private monPlanFeuxActif As PlanFeuxFonctionnement
  Private mesLignesFeux As LigneFeuxCollection
  Private mesBranches As BrancheCollection

  Private mEnVeille As Boolean
  Private D�fautLargeurPi�tons, D�fautLargeurV�hicules As Short

#End Region

#Region "Proc�dures"
  Public Sub AfficherCapacit�()

    With monPlanFeuxActif.mVariante
      Me.txtVitesseV�hicule.Text = .VitesseV�hicules
      Me.txtVitessePi�ton.Text = .VitessePi�tons
      Me.txtVitesseV�lo.Text = .VitesseV�los
      Me.txtD�bitSaturation.Text = .D�bitSaturation
      Me.lblSecondesVert.Text = .strVertUtile
    End With

    If IsNothing(monPlanFeuxActif) Then
      Me.txtDur�eCycleBase.Text = ""
      Me.txtDemande.Text = ""
      Me.txtCapacit�Plan.Text = ""
      Me.txtR�serveCapacit�.Text = ""
      Me.txtR�servePourCent.Text = ""

    Else
      With monPlanFeuxActif
        Me.txtDur�eCycleBase.Text = .Dur�eCycle
        Me.txtDemande.Text = .Demande
        Me.txtCapacit�Plan.Text = CType(Math.Round(.Capacit�Th�orique), String)
        Me.txtR�serveCapacit�.Text = CType(Math.Round(.R�serveCapacit�), String)
        Me.txtR�servePourCent.Text = .strR�serveCapacit�PourCent
        Me.txtTempsPerduCycle.Text = .TempsPerdu
      End With ' monPlanFeuxActif
    End If

    AfficherInfosAttente()
  End Sub

  Public Sub AfficherInfosAttente()
    Dim uneLigneFeux As LigneFeux
    Dim itmX As ListViewItem
    Dim IDLigneFeux As String

    Me.lvwV�hicules.Items.Clear()
    Me.lvwPi�tons.Items.Clear()

    If IsNothing(monPlanFeuxActif) Then
      Me.txtTempsAttenteV�hicules.Text = ""
      Me.txtTempsAttentePi�tons.Text = ""

    Else
      monPlanFeuxActif.AffecterInfosAttente()

      For Each uneLigneFeux In mesLignesFeux
        IDLigneFeux = uneLigneFeux.ID & " (" & mesBranches.ID(uneLigneFeux.mBranche) & ")"
        If uneLigneFeux.EstV�hicule Then
          With CType(monPlanFeuxActif, PlanFeuxFonctionnement)
            itmX = New ListViewItem(New String() {IDLigneFeux, "0", "0", "0", "0", "0", "0"})
            Me.lvwV�hicules.Items.Add(itmX)
            itmX.SubItems(1).Text = .DemandeUVP(uneLigneFeux)
            'Vert utile ( voir si r�ekl in t�ressant (?)
            itmX.SubItems(2).Text = .VertUtile(uneLigneFeux)
            'Temps moyen d'attente
            itmX.SubItems(3).Text = Format(.RetardMoyen(uneLigneFeux), "###")
            'Nombre de v�hicules par file
            itmX.SubItems(4).Text = .NbV�hiculesEnAttente(uneLigneFeux)
            'Longueur file
            itmX.SubItems(5).Text = Format(.LgFileAttente(uneLigneFeux), "###")
          End With

        Else
          With monPlanFeuxActif
            itmX = New ListViewItem(New String() {IDLigneFeux, "0", "0", "0"})
            Me.lvwPi�tons.Items.Add(itmX)
            'Trafic pi�tons sur la branche travers�e
            itmX.SubItems(1).Text = monPlanFeuxActif.Trafic.QPi�ton(uneLigneFeux.mBranche)
            'Vert r�el
            itmX.SubItems(2).Text = .VertUtile(uneLigneFeux)
            'Temps moyen d'attente
            If monPlanFeuxActif.Trafic.QPi�ton(uneLigneFeux.mBranche) > 0 Then
              itmX.SubItems(3).Text = Format(.RetardMoyen(uneLigneFeux), "###")
            Else
              itmX.SubItems(3).Text = ""
            End If
          End With

        End If

      Next

      Me.txtTempsAttenteV�hicules.Text = CType(monPlanFeuxActif, PlanFeuxFonctionnement).TMAV�hicules
      Me.txtTempsAttentePi�tons.Text = CType(monPlanFeuxActif, PlanFeuxFonctionnement).TMAPi�tons

    End If

  End Sub

  Public Sub RenommerColonnePlanFeux(ByVal uneLigneRenomm�e As LigneFeux, ByVal Position As Short)
    Dim lstItems As ListView.ListViewItemCollection
    Dim Index As Short
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In mesLignesFeux
      If uneLigneFeux.EstV�hicule Xor uneLigneRenomm�e.EstPi�ton Then
        If mesLignesFeux.IndexOf(uneLigneFeux) = Position Then
          Exit For
        End If
        Index += 1
      End If
    Next

    If uneLigneFeux.EstV�hicule Then
      lstItems = Me.lvwV�hicules.Items
    Else
      lstItems = Me.lvwPi�tons.Items
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
    D�fautLargeurV�hicules = Me.lvwV�hicules.Width
    D�fautLargeurPi�tons = Me.lvwPi�tons.Width

  End Sub

  Private Sub frmDiagnostic_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
    If Me.Visible Then
      With lvwV�hicules
        If .Items.Count > 13 Then
          .Width = D�fautLargeurV�hicules + 15
        Else
          .Width = D�fautLargeurV�hicules
        End If
      End With

      With lvwPi�tons
        If .Items.Count > 6 Then
          .Width = D�fautLargeurPi�tons + 15
        Else
          .Width = D�fautLargeurPi�tons
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
