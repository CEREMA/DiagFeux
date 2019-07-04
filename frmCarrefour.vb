Imports Grille = C1.Win.C1FlexGrid
Imports System.Math

'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : frmCarrefour.vb										  											'
'						Classes																														'
'							frmCarrefour : Feuille MDIChild                												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe frmCarrefour --------------------------
'Feuille principale de l'application : feuille fille MDI(une feuille par étude ouverte)
'=====================================================================================================
  Public Class frmCarrefour
    Inherits System.Windows.Forms.Form

#Region " Code généré par le Concepteur Windows Form "

    Public Sub New()
      MyBase.New()

      'Cet appel est requis par le Concepteur Windows Form.
      InitializeComponent()

      'Ajoutez une initialisation quelconque après l'appel InitializeComponent()
      FonteGras = New Font(Me.tabOnglet.Font, FontStyle.Bold)
    Me.chkTraficRéférence.Visible = False
    Me.lblTraficVerrouillé.Visible = False
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
  Friend WithEvents tabPlansDeFeux As System.Windows.Forms.TabPage
  Friend WithEvents tabOnglet As System.Windows.Forms.TabControl
  Friend WithEvents tabTrafics As System.Windows.Forms.TabPage
  Friend WithEvents tabLignesDeFeux As System.Windows.Forms.TabPage
  Friend WithEvents pnlGéométrie As System.Windows.Forms.Panel
  Friend WithEvents pnlPlansDeFeux As System.Windows.Forms.Panel
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  Friend WithEvents pnlLignesDeFeux As System.Windows.Forms.Panel
  Friend WithEvents lblPériode As System.Windows.Forms.Label
  Friend WithEvents chkModeTrafic As System.Windows.Forms.CheckBox
  Friend WithEvents radUVP As System.Windows.Forms.RadioButton
  Friend WithEvents rad2Roues As System.Windows.Forms.RadioButton
  Friend WithEvents radPL As System.Windows.Forms.RadioButton
  Friend WithEvents radVL As System.Windows.Forms.RadioButton
  Friend WithEvents cboTrafic As System.Windows.Forms.ComboBox
  Friend WithEvents pnlTrafics As System.Windows.Forms.Panel
  Friend WithEvents pnlTrafic As System.Windows.Forms.Panel
  Friend WithEvents tabConflits As System.Windows.Forms.TabPage
  Friend WithEvents grpPiéton As System.Windows.Forms.GroupBox
  Friend WithEvents grpVéhicule As System.Windows.Forms.GroupBox
  Friend WithEvents picDessin As System.Windows.Forms.PictureBox
  Friend WithEvents tabGéométrie As System.Windows.Forms.TabPage
  Friend WithEvents AC1GrilleFeux As GrilleDiagfeux
  Friend WithEvents AC1GrilleBranches As GrilleDiagfeux
  Friend WithEvents btnLigneFeuMonter As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeuDescendre As System.Windows.Forms.Button
  Friend WithEvents splitOngletsPrincipal As System.Windows.Forms.Splitter
  Friend WithEvents splitGraphiqueDonnées As System.Windows.Forms.Splitter
  Friend WithEvents pnlPhasage As System.Windows.Forms.Panel
  Friend WithEvents btnActionPhase As System.Windows.Forms.Button
  Friend WithEvents lblDécoupagePhases As System.Windows.Forms.Label
  Friend WithEvents pnlFeuBase As System.Windows.Forms.Panel
  Friend WithEvents grpSynchroBase As System.Windows.Forms.GroupBox
  Friend WithEvents lvwDuréeVert As System.Windows.Forms.ListView
  Friend WithEvents grpPhasesBase As System.Windows.Forms.GroupBox
  Friend WithEvents txtDuréeCycleBase As System.Windows.Forms.TextBox
  Friend WithEvents lblPhase3Base As System.Windows.Forms.Label
  Friend WithEvents lblPhase2Base As System.Windows.Forms.Label
  Friend WithEvents lblPhase1Base As System.Windows.Forms.Label
  Friend WithEvents updPhase3Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase2Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase1Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents lvwcolLF As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDurée As System.Windows.Forms.ColumnHeader
  Friend WithEvents pnlFeuFonctionnement As System.Windows.Forms.Panel
  Friend WithEvents grpPhasesFct As System.Windows.Forms.GroupBox
  Friend WithEvents txtDuréeCycleFct As System.Windows.Forms.TextBox
  Friend WithEvents lblPhase3Fct As System.Windows.Forms.Label
  Friend WithEvents lblPhase2Fct As System.Windows.Forms.Label
  Friend WithEvents lblPhase1Fct As System.Windows.Forms.Label
  Friend WithEvents updPhase3Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase2Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase1Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents chkVerrouFeuBase As System.Windows.Forms.CheckBox
  Friend WithEvents lblCarrefourComposé As System.Windows.Forms.Label
  Friend WithEvents cboCarrefourComposé As System.Windows.Forms.ComboBox
  Friend WithEvents lvwcolDécalOuverture As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDécalFermeture As System.Windows.Forms.ColumnHeader
  Friend WithEvents splitCarrefourComposé As System.Windows.Forms.Splitter
  Friend WithEvents pnlCarrefourComposé As System.Windows.Forms.Panel
  Friend WithEvents lvwDuréeVertFct As System.Windows.Forms.ListView
  Friend WithEvents lvwcolLFFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDuréeFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDécalOuvertureFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDécalFermetureFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents updDécalageFermetureFct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updDécalageOuvertureFct As System.Windows.Forms.NumericUpDown
  Friend WithEvents cboPlansDeFeux As System.Windows.Forms.ComboBox
  Friend WithEvents lblTrafic As System.Windows.Forms.Label
  Friend WithEvents lblPlansDeFeux As System.Windows.Forms.Label
  Friend WithEvents btnDupliquerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents btnSupprimerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents grpSynchroFct As System.Windows.Forms.GroupBox
  Friend WithEvents updDécalageFermetureBase As System.Windows.Forms.NumericUpDown
  Friend WithEvents updDécalageOuvertureBase As System.Windows.Forms.NumericUpDown
  Friend WithEvents btnRenommerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents cboDécoupagePhases As System.Windows.Forms.ComboBox
  Friend WithEvents radPhase1Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase2Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase3Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase3Base As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase2Base As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase1Base As System.Windows.Forms.RadioButton
  Friend WithEvents btnSupprimerTrafic As System.Windows.Forms.Button
  Friend WithEvents btnNouveauTrafic As System.Windows.Forms.Button
  Friend WithEvents btnRenommerTrafic As System.Windows.Forms.Button
  Friend WithEvents Ac1GrilleTraficPiétons As DiagFeux.GrilleDiagfeux
  Friend WithEvents chkTraficRéférence As System.Windows.Forms.CheckBox
  Friend WithEvents pnlBoutonsLignesFeux As System.Windows.Forms.Panel
  Friend WithEvents lblDécalages As System.Windows.Forms.Label
  Friend WithEvents lblCycle As System.Windows.Forms.Label
  Friend WithEvents lblDécalagesFct As System.Windows.Forms.Label
  Friend WithEvents lblCycleFct As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleTraficVéhicules As DiagFeux.GrilleDiagfeux
  Friend WithEvents pnlConflits As System.Windows.Forms.Panel
  Friend WithEvents Ac1GrilleSécurité As DiagFeux.GrilleDiagfeux
  Friend WithEvents pnlVerrouMatrice As System.Windows.Forms.Panel
  Friend WithEvents lbImgSansConflit As System.Windows.Forms.Label
  Friend WithEvents lblImgConflit As System.Windows.Forms.Label
  Friend WithEvents pnlImgSansConflit As System.Windows.Forms.Panel
  Friend WithEvents pnlImgConflit As System.Windows.Forms.Panel
  Friend WithEvents chkVerrouMatrice As System.Windows.Forms.CheckBox
  'Friend WithEvents lblRéserveCapacitéUvpd As System.Windows.Forms.Label
  'Friend WithEvents lblRéserveCapacitéThéorique As System.Windows.Forms.Label
  'Friend WithEvents txtRéserveCapacitéThéorique As System.Windows.Forms.TextBox
  Friend WithEvents btnPiétonPlus As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents btnCarrefour As System.Windows.Forms.Button
  Friend WithEvents btnPiétonMoins As System.Windows.Forms.Button
  Friend WithEvents lblPassage As System.Windows.Forms.Label
  Friend WithEvents pnlBtnGéométrie As System.Windows.Forms.Panel
  Friend WithEvents pnlIlots As System.Windows.Forms.Panel
  Friend WithEvents lblIlot As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleIlot As DiagFeux.GrilleDiagfeux
  Friend WithEvents tipPicDessin As System.Windows.Forms.ToolTip
  Friend WithEvents lblLigneFeu As System.Windows.Forms.Label
  Friend WithEvents btnSignalMoins As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleAntagonismes As DiagFeux.GrilleDiagfeux
  Friend WithEvents btnTravProp As System.Windows.Forms.Button
  Friend WithEvents lblTraversée As System.Windows.Forms.Label
  Friend WithEvents btnTrajProp As System.Windows.Forms.Button
  Friend WithEvents lblTrajVehicule As System.Windows.Forms.Label
  Friend WithEvents pnlTrajectoires As System.Windows.Forms.Panel
  Friend WithEvents radMatriceInterverts As System.Windows.Forms.RadioButton
  Friend WithEvents radMatriceRougesDégagement As System.Windows.Forms.RadioButton
  Friend WithEvents radMatriceConflits As System.Windows.Forms.RadioButton
  Friend WithEvents pnlMatricesSécurité As System.Windows.Forms.Panel
  Friend WithEvents pnlAntagonismes As System.Windows.Forms.Panel
  Friend WithEvents cboBrancheCourant1 As System.Windows.Forms.ComboBox
  Friend WithEvents lblCourantOrigine As System.Windows.Forms.Label
  Friend WithEvents lblTriLignesFeux As System.Windows.Forms.Label
  Friend WithEvents cboTriLignesFeux As System.Windows.Forms.ComboBox
  Friend WithEvents lbFigerDuréeBase As System.Windows.Forms.Label
  Friend WithEvents cboMéthodeCalculCycle As System.Windows.Forms.ComboBox
  Friend WithEvents lblMéthodeCalculCycle As System.Windows.Forms.Label
  Friend WithEvents lbFigerDuréeFct As System.Windows.Forms.Label
  Friend WithEvents btnCalculerCycle As System.Windows.Forms.Button
  Friend WithEvents lblRéservCapacitéChoisie As System.Windows.Forms.Label
  Friend WithEvents cboRéserveCapacitéChoisie As System.Windows.Forms.ComboBox
  Friend WithEvents btnTrajToutes As System.Windows.Forms.Button
  Friend WithEvents lblUVP As System.Windows.Forms.Label
  'Friend WithEvents pnlCapacitéThéorique As System.Windows.Forms.Panel
  Friend WithEvents radPhasage As System.Windows.Forms.RadioButton
  Friend WithEvents radFeuBase As System.Windows.Forms.RadioButton
  Friend WithEvents radFeuFonctionnement As System.Windows.Forms.RadioButton
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lblTraficVerrouillé As System.Windows.Forms.Label
  Friend WithEvents lblCommentairePériode As System.Windows.Forms.Label
  Friend WithEvents txtCommentairePériode As System.Windows.Forms.TextBox
  Friend WithEvents btnDiagnostic As System.Windows.Forms.Button
  Friend WithEvents chkVerrouGéométrie As System.Windows.Forms.CheckBox
  Friend WithEvents chkVerrouLignesFeux As System.Windows.Forms.CheckBox
  'Friend WithEvents lblSecondesDuréeCycle As System.Windows.Forms.Label
  'Friend WithEvents txtDuréeCycle As System.Windows.Forms.TextBox
  'Friend WithEvents lblDuréeCycle As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblVéhiculeBase As System.Windows.Forms.Label
  Friend WithEvents lblPiétonBase As System.Windows.Forms.Label
  Friend WithEvents txtVertMiniPiéton As System.Windows.Forms.TextBox
  Friend WithEvents txtVertMiniVéhicule As System.Windows.Forms.TextBox
  Friend WithEvents lblVertMini As System.Windows.Forms.Label
  Friend WithEvents btnDupliquerTrafic As System.Windows.Forms.Button
  Friend WithEvents chkVerrouPériode As System.Windows.Forms.CheckBox
  Friend WithEvents pnlFiltrePhasage As System.Windows.Forms.Panel
  Friend WithEvents lblRéserveCapacité As System.Windows.Forms.Label
  Friend WithEvents chk3Phases As System.Windows.Forms.CheckBox
  Friend WithEvents pnlBoutonsLignesFeuxPlans As System.Windows.Forms.Panel
  Friend WithEvents cboTriLignesFeuxPlans As System.Windows.Forms.ComboBox
  Friend WithEvents lblTriLignesFeuxPlans As System.Windows.Forms.Label
  Friend WithEvents btnLigneFeuDescendrePlans As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeuMonterPlans As System.Windows.Forms.Button
  Friend WithEvents chkSensTrajectoires As System.Windows.Forms.CheckBox
  Friend WithEvents btnRéinitAntago As System.Windows.Forms.Button
  Friend WithEvents pnlBoutonsRouges As System.Windows.Forms.Panel
  Friend WithEvents btnRougesDéfaut As System.Windows.Forms.Button
  Friend WithEvents btnRougeDéfaut As System.Windows.Forms.Button
  Friend WithEvents lblBoutonsRouges As System.Windows.Forms.Label
  Friend WithEvents lvwcolPhase As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolPhaseFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnPiétonPlusRapide As System.Windows.Forms.Button
  Friend WithEvents lblLFMultiPhases As System.Windows.Forms.Label
  Friend WithEvents cbolLFMultiPhases As System.Windows.Forms.ComboBox
  Friend WithEvents lblPhasesSpéciales As System.Windows.Forms.Label
  Friend WithEvents cboPhasesSpéciales As System.Windows.Forms.ComboBox
  Friend WithEvents cboRéserveCapacité As System.Windows.Forms.ComboBox
  'Friend WithEvents txtRPourCent As System.Windows.Forms.TextBox
  Friend WithEvents txtRéserveCapacitéPourCent As System.Windows.Forms.TextBox
  Friend WithEvents lblTaficSaturé As System.Windows.Forms.Label
  Friend WithEvents pnlTableauPhasage As System.Windows.Forms.Panel
  Friend WithEvents AC1GrillePhases As DiagFeux.GrilleDiagfeux
  Friend WithEvents lblConflitPotentiel As System.Windows.Forms.Label
  Friend WithEvents pnlConflitPotentiel As System.Windows.Forms.Panel
  Friend WithEvents chkDécoupagePhases As System.Windows.Forms.CheckBox
  Friend WithEvents chkScénarioDéfinitif As System.Windows.Forms.CheckBox
  Friend WithEvents lblTraficFct As System.Windows.Forms.Label
  Friend WithEvents cboTraficFct As System.Windows.Forms.ComboBox
  Friend WithEvents btnTrajMoinsTout As System.Windows.Forms.Button
  Friend WithEvents btnTraverséeMoins As System.Windows.Forms.Button
  Friend WithEvents btnTraversée As System.Windows.Forms.Button
  Friend WithEvents btnTrajectoireMoins As System.Windows.Forms.Button
  Friend WithEvents btnTrajectoire As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeuxMoins As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeux As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCarrefour))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F1", "1"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F2", "2"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"F3", "3"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P1", "1"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P2", "2"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P3 ", "3"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"P4", "2"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Me.tabPlansDeFeux = New System.Windows.Forms.TabPage
        Me.tabOnglet = New System.Windows.Forms.TabControl
        Me.tabGéométrie = New System.Windows.Forms.TabPage
        Me.tabLignesDeFeux = New System.Windows.Forms.TabPage
        Me.tabTrafics = New System.Windows.Forms.TabPage
        Me.tabConflits = New System.Windows.Forms.TabPage
        Me.splitOngletsPrincipal = New System.Windows.Forms.Splitter
        Me.splitGraphiqueDonnées = New System.Windows.Forms.Splitter
        Me.pnlGéométrie = New System.Windows.Forms.Panel
        Me.pnlIlots = New System.Windows.Forms.Panel
        Me.AC1GrilleIlot = New DiagFeux.GrilleDiagfeux
        Me.lblIlot = New System.Windows.Forms.Label
        Me.pnlBtnGéométrie = New System.Windows.Forms.Panel
        Me.btnPiétonPlusRapide = New System.Windows.Forms.Button
        Me.chkVerrouGéométrie = New System.Windows.Forms.CheckBox
        Me.btnPiétonPlus = New System.Windows.Forms.Button
        Me.btnCarrefour = New System.Windows.Forms.Button
        Me.btnPiétonMoins = New System.Windows.Forms.Button
        Me.lblPassage = New System.Windows.Forms.Label
        Me.AC1GrilleBranches = New DiagFeux.GrilleDiagfeux
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlLignesDeFeux = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlTrajectoires = New System.Windows.Forms.Panel
        Me.chkSensTrajectoires = New System.Windows.Forms.CheckBox
        Me.btnTrajMoinsTout = New System.Windows.Forms.Button
        Me.btnTrajToutes = New System.Windows.Forms.Button
        Me.btnTraverséeMoins = New System.Windows.Forms.Button
        Me.btnTravProp = New System.Windows.Forms.Button
        Me.btnTraversée = New System.Windows.Forms.Button
        Me.lblTraversée = New System.Windows.Forms.Label
        Me.btnTrajProp = New System.Windows.Forms.Button
        Me.btnTrajectoireMoins = New System.Windows.Forms.Button
        Me.btnTrajectoire = New System.Windows.Forms.Button
        Me.lblTrajVehicule = New System.Windows.Forms.Label
        Me.chkVerrouLignesFeux = New System.Windows.Forms.CheckBox
        Me.AC1GrilleFeux = New DiagFeux.GrilleDiagfeux
        Me.pnlBoutonsLignesFeux = New System.Windows.Forms.Panel
        Me.btnLigneFeuxMoins = New System.Windows.Forms.Button
        Me.btnLigneFeux = New System.Windows.Forms.Button
        Me.cboTriLignesFeux = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSignalMoins = New System.Windows.Forms.Button
        Me.lblTriLignesFeux = New System.Windows.Forms.Label
        Me.lblLigneFeu = New System.Windows.Forms.Label
        Me.btnLigneFeuDescendre = New System.Windows.Forms.Button
        Me.btnLigneFeuMonter = New System.Windows.Forms.Button
        Me.pnlTrafics = New System.Windows.Forms.Panel
        Me.lblTaficSaturé = New System.Windows.Forms.Label
        Me.chkVerrouPériode = New System.Windows.Forms.CheckBox
        Me.txtCommentairePériode = New System.Windows.Forms.TextBox
        Me.lblCommentairePériode = New System.Windows.Forms.Label
        Me.lblTraficVerrouillé = New System.Windows.Forms.Label
        Me.btnSupprimerTrafic = New System.Windows.Forms.Button
        Me.btnNouveauTrafic = New System.Windows.Forms.Button
        Me.btnRenommerTrafic = New System.Windows.Forms.Button
        Me.btnDupliquerTrafic = New System.Windows.Forms.Button
        Me.chkTraficRéférence = New System.Windows.Forms.CheckBox
        Me.lblPériode = New System.Windows.Forms.Label
        Me.grpPiéton = New System.Windows.Forms.GroupBox
        Me.Ac1GrilleTraficPiétons = New DiagFeux.GrilleDiagfeux
        Me.chkModeTrafic = New System.Windows.Forms.CheckBox
        Me.grpVéhicule = New System.Windows.Forms.GroupBox
        Me.AC1GrilleTraficVéhicules = New DiagFeux.GrilleDiagfeux
        Me.pnlTrafic = New System.Windows.Forms.Panel
        Me.radUVP = New System.Windows.Forms.RadioButton
        Me.rad2Roues = New System.Windows.Forms.RadioButton
        Me.radPL = New System.Windows.Forms.RadioButton
        Me.radVL = New System.Windows.Forms.RadioButton
        Me.lblUVP = New System.Windows.Forms.Label
        Me.cboTrafic = New System.Windows.Forms.ComboBox
        Me.pnlPlansDeFeux = New System.Windows.Forms.Panel
        Me.pnlCarrefourComposé = New System.Windows.Forms.Panel
        Me.chkScénarioDéfinitif = New System.Windows.Forms.CheckBox
        Me.radFeuFonctionnement = New System.Windows.Forms.RadioButton
        Me.radFeuBase = New System.Windows.Forms.RadioButton
        Me.radPhasage = New System.Windows.Forms.RadioButton
        Me.cboCarrefourComposé = New System.Windows.Forms.ComboBox
        Me.lblCarrefourComposé = New System.Windows.Forms.Label
        Me.splitCarrefourComposé = New System.Windows.Forms.Splitter
        Me.pnlFeuFonctionnement = New System.Windows.Forms.Panel
        Me.lblTraficFct = New System.Windows.Forms.Label
        Me.cboTraficFct = New System.Windows.Forms.ComboBox
        Me.lblPlansDeFeux = New System.Windows.Forms.Label
        Me.cboPlansDeFeux = New System.Windows.Forms.ComboBox
        Me.btnDiagnostic = New System.Windows.Forms.Button
        Me.btnSupprimerPlanFeux = New System.Windows.Forms.Button
        Me.btnDupliquerPlanFeux = New System.Windows.Forms.Button
        Me.btnRenommerPlanFeux = New System.Windows.Forms.Button
        Me.grpPhasesFct = New System.Windows.Forms.GroupBox
        Me.cboRéserveCapacitéChoisie = New System.Windows.Forms.ComboBox
        Me.lblRéservCapacitéChoisie = New System.Windows.Forms.Label
        Me.btnCalculerCycle = New System.Windows.Forms.Button
        Me.cboMéthodeCalculCycle = New System.Windows.Forms.ComboBox
        Me.lblMéthodeCalculCycle = New System.Windows.Forms.Label
        Me.radPhase3Fct = New System.Windows.Forms.RadioButton
        Me.radPhase2Fct = New System.Windows.Forms.RadioButton
        Me.radPhase1Fct = New System.Windows.Forms.RadioButton
        Me.lbFigerDuréeFct = New System.Windows.Forms.Label
        Me.txtDuréeCycleFct = New System.Windows.Forms.TextBox
        Me.lblCycleFct = New System.Windows.Forms.Label
        Me.lblPhase3Fct = New System.Windows.Forms.Label
        Me.lblPhase2Fct = New System.Windows.Forms.Label
        Me.lblPhase1Fct = New System.Windows.Forms.Label
        Me.updPhase3Fct = New System.Windows.Forms.NumericUpDown
        Me.updPhase2Fct = New System.Windows.Forms.NumericUpDown
        Me.updPhase1Fct = New System.Windows.Forms.NumericUpDown
        Me.grpSynchroFct = New System.Windows.Forms.GroupBox
        Me.lblDécalagesFct = New System.Windows.Forms.Label
        Me.updDécalageFermetureFct = New System.Windows.Forms.NumericUpDown
        Me.updDécalageOuvertureFct = New System.Windows.Forms.NumericUpDown
        Me.lvwDuréeVertFct = New System.Windows.Forms.ListView
        Me.lvwcolLFFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolPhaseFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDuréeFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDécalOuvertureFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDécalFermetureFct = New System.Windows.Forms.ColumnHeader
        Me.pnlPhasage = New System.Windows.Forms.Panel
        Me.pnlTableauPhasage = New System.Windows.Forms.Panel
        Me.lblConflitPotentiel = New System.Windows.Forms.Label
        Me.pnlConflitPotentiel = New System.Windows.Forms.Panel
        Me.chkDécoupagePhases = New System.Windows.Forms.CheckBox
        Me.AC1GrillePhases = New DiagFeux.GrilleDiagfeux
        Me.pnlFiltrePhasage = New System.Windows.Forms.Panel
        Me.cboDécoupagePhases = New System.Windows.Forms.ComboBox
        Me.txtRéserveCapacitéPourCent = New System.Windows.Forms.TextBox
        Me.cboPhasesSpéciales = New System.Windows.Forms.ComboBox
        Me.cbolLFMultiPhases = New System.Windows.Forms.ComboBox
        Me.lblPhasesSpéciales = New System.Windows.Forms.Label
        Me.lblLFMultiPhases = New System.Windows.Forms.Label
        Me.lblRéserveCapacité = New System.Windows.Forms.Label
        Me.cboRéserveCapacité = New System.Windows.Forms.ComboBox
        Me.chk3Phases = New System.Windows.Forms.CheckBox
        Me.btnActionPhase = New System.Windows.Forms.Button
        Me.lblDécoupagePhases = New System.Windows.Forms.Label
        Me.pnlFeuBase = New System.Windows.Forms.Panel
        Me.lblVéhiculeBase = New System.Windows.Forms.Label
        Me.lblPiétonBase = New System.Windows.Forms.Label
        Me.txtVertMiniPiéton = New System.Windows.Forms.TextBox
        Me.txtVertMiniVéhicule = New System.Windows.Forms.TextBox
        Me.lblVertMini = New System.Windows.Forms.Label
        Me.grpSynchroBase = New System.Windows.Forms.GroupBox
        Me.lblDécalages = New System.Windows.Forms.Label
        Me.updDécalageFermetureBase = New System.Windows.Forms.NumericUpDown
        Me.updDécalageOuvertureBase = New System.Windows.Forms.NumericUpDown
        Me.lvwDuréeVert = New System.Windows.Forms.ListView
        Me.lvwcolLF = New System.Windows.Forms.ColumnHeader
        Me.lvwcolPhase = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDurée = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDécalOuverture = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDécalFermeture = New System.Windows.Forms.ColumnHeader
        Me.grpPhasesBase = New System.Windows.Forms.GroupBox
        Me.radPhase3Base = New System.Windows.Forms.RadioButton
        Me.radPhase2Base = New System.Windows.Forms.RadioButton
        Me.radPhase1Base = New System.Windows.Forms.RadioButton
        Me.lbFigerDuréeBase = New System.Windows.Forms.Label
        Me.txtDuréeCycleBase = New System.Windows.Forms.TextBox
        Me.lblCycle = New System.Windows.Forms.Label
        Me.lblPhase3Base = New System.Windows.Forms.Label
        Me.lblPhase2Base = New System.Windows.Forms.Label
        Me.lblPhase1Base = New System.Windows.Forms.Label
        Me.updPhase3Base = New System.Windows.Forms.NumericUpDown
        Me.updPhase2Base = New System.Windows.Forms.NumericUpDown
        Me.updPhase1Base = New System.Windows.Forms.NumericUpDown
        Me.chkVerrouFeuBase = New System.Windows.Forms.CheckBox
        Me.pnlBoutonsLignesFeuxPlans = New System.Windows.Forms.Panel
        Me.cboTriLignesFeuxPlans = New System.Windows.Forms.ComboBox
        Me.lblTriLignesFeuxPlans = New System.Windows.Forms.Label
        Me.btnLigneFeuDescendrePlans = New System.Windows.Forms.Button
        Me.btnLigneFeuMonterPlans = New System.Windows.Forms.Button
        Me.tipBulle = New System.Windows.Forms.ToolTip(Me.components)
        Me.picDessin = New System.Windows.Forms.PictureBox
        Me.pnlConflits = New System.Windows.Forms.Panel
        Me.pnlBoutonsRouges = New System.Windows.Forms.Panel
        Me.lblBoutonsRouges = New System.Windows.Forms.Label
        Me.btnRougeDéfaut = New System.Windows.Forms.Button
        Me.btnRougesDéfaut = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlAntagonismes = New System.Windows.Forms.Panel
        Me.btnRéinitAntago = New System.Windows.Forms.Button
        Me.cboBrancheCourant1 = New System.Windows.Forms.ComboBox
        Me.lblCourantOrigine = New System.Windows.Forms.Label
        Me.AC1GrilleAntagonismes = New DiagFeux.GrilleDiagfeux
        Me.pnlMatricesSécurité = New System.Windows.Forms.Panel
        Me.radMatriceInterverts = New System.Windows.Forms.RadioButton
        Me.radMatriceRougesDégagement = New System.Windows.Forms.RadioButton
        Me.radMatriceConflits = New System.Windows.Forms.RadioButton
        Me.pnlVerrouMatrice = New System.Windows.Forms.Panel
        Me.lbImgSansConflit = New System.Windows.Forms.Label
        Me.lblImgConflit = New System.Windows.Forms.Label
        Me.pnlImgSansConflit = New System.Windows.Forms.Panel
        Me.pnlImgConflit = New System.Windows.Forms.Panel
        Me.chkVerrouMatrice = New System.Windows.Forms.CheckBox
        Me.Ac1GrilleSécurité = New DiagFeux.GrilleDiagfeux
        Me.tipPicDessin = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabOnglet.SuspendLayout()
        Me.pnlGéométrie.SuspendLayout()
        Me.pnlIlots.SuspendLayout()
        CType(Me.AC1GrilleIlot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBtnGéométrie.SuspendLayout()
        CType(Me.AC1GrilleBranches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLignesDeFeux.SuspendLayout()
        Me.pnlTrajectoires.SuspendLayout()
        CType(Me.AC1GrilleFeux, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBoutonsLignesFeux.SuspendLayout()
        Me.pnlTrafics.SuspendLayout()
        Me.grpPiéton.SuspendLayout()
        CType(Me.Ac1GrilleTraficPiétons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVéhicule.SuspendLayout()
        CType(Me.AC1GrilleTraficVéhicules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTrafic.SuspendLayout()
        Me.pnlPlansDeFeux.SuspendLayout()
        Me.pnlCarrefourComposé.SuspendLayout()
        Me.pnlFeuFonctionnement.SuspendLayout()
        Me.grpPhasesFct.SuspendLayout()
        CType(Me.updPhase3Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase2Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase1Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSynchroFct.SuspendLayout()
        CType(Me.updDécalageFermetureFct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updDécalageOuvertureFct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPhasage.SuspendLayout()
        Me.pnlTableauPhasage.SuspendLayout()
        CType(Me.AC1GrillePhases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiltrePhasage.SuspendLayout()
        Me.pnlFeuBase.SuspendLayout()
        Me.grpSynchroBase.SuspendLayout()
        CType(Me.updDécalageFermetureBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updDécalageOuvertureBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPhasesBase.SuspendLayout()
        CType(Me.updPhase3Base, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase2Base, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase1Base, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBoutonsLignesFeuxPlans.SuspendLayout()
        CType(Me.picDessin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlConflits.SuspendLayout()
        Me.pnlBoutonsRouges.SuspendLayout()
        Me.pnlAntagonismes.SuspendLayout()
        CType(Me.AC1GrilleAntagonismes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMatricesSécurité.SuspendLayout()
        Me.pnlVerrouMatrice.SuspendLayout()
        CType(Me.Ac1GrilleSécurité, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabPlansDeFeux
        '
        Me.tabPlansDeFeux.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabPlansDeFeux.Location = New System.Drawing.Point(4, 22)
        Me.tabPlansDeFeux.Name = "tabPlansDeFeux"
        Me.tabPlansDeFeux.Size = New System.Drawing.Size(904, 0)
        Me.tabPlansDeFeux.TabIndex = 5
        Me.tabPlansDeFeux.Text = "Plans de feux"
        Me.tabPlansDeFeux.Visible = False
        '
        'tabOnglet
        '
        Me.tabOnglet.Controls.Add(Me.tabGéométrie)
        Me.tabOnglet.Controls.Add(Me.tabLignesDeFeux)
        Me.tabOnglet.Controls.Add(Me.tabTrafics)
        Me.tabOnglet.Controls.Add(Me.tabConflits)
        Me.tabOnglet.Controls.Add(Me.tabPlansDeFeux)
        Me.tabOnglet.Dock = System.Windows.Forms.DockStyle.Top
        Me.tabOnglet.ItemSize = New System.Drawing.Size(115, 18)
        Me.tabOnglet.Location = New System.Drawing.Point(0, 0)
        Me.tabOnglet.Name = "tabOnglet"
        Me.tabOnglet.SelectedIndex = 0
        Me.tabOnglet.Size = New System.Drawing.Size(912, 24)
        Me.tabOnglet.TabIndex = 3
        '
        'tabGéométrie
        '
        Me.tabGéométrie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabGéométrie.Location = New System.Drawing.Point(4, 22)
        Me.tabGéométrie.Name = "tabGéométrie"
        Me.tabGéométrie.Size = New System.Drawing.Size(904, 0)
        Me.tabGéométrie.TabIndex = 0
        Me.tabGéométrie.Text = "Géométrie"
        '
        'tabLignesDeFeux
        '
        Me.tabLignesDeFeux.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabLignesDeFeux.Location = New System.Drawing.Point(4, 22)
        Me.tabLignesDeFeux.Name = "tabLignesDeFeux"
        Me.tabLignesDeFeux.Size = New System.Drawing.Size(904, 0)
        Me.tabLignesDeFeux.TabIndex = 2
        Me.tabLignesDeFeux.Text = "Circulation & Signalisation"
        Me.tabLignesDeFeux.Visible = False
        '
        'tabTrafics
        '
        Me.tabTrafics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabTrafics.Location = New System.Drawing.Point(4, 22)
        Me.tabTrafics.Name = "tabTrafics"
        Me.tabTrafics.Size = New System.Drawing.Size(904, 0)
        Me.tabTrafics.TabIndex = 3
        Me.tabTrafics.Text = "Trafics"
        Me.tabTrafics.Visible = False
        '
        'tabConflits
        '
        Me.tabConflits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabConflits.Location = New System.Drawing.Point(4, 22)
        Me.tabConflits.Name = "tabConflits"
        Me.tabConflits.Size = New System.Drawing.Size(904, 0)
        Me.tabConflits.TabIndex = 4
        Me.tabConflits.Text = "Conflits"
        Me.tabConflits.Visible = False
        '
        'splitOngletsPrincipal
        '
        Me.splitOngletsPrincipal.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitOngletsPrincipal.Location = New System.Drawing.Point(0, 24)
        Me.splitOngletsPrincipal.Name = "splitOngletsPrincipal"
        Me.splitOngletsPrincipal.Size = New System.Drawing.Size(912, 3)
        Me.splitOngletsPrincipal.TabIndex = 4
        Me.splitOngletsPrincipal.TabStop = False
        Me.splitOngletsPrincipal.Visible = False
        '
        'splitGraphiqueDonnées
        '
        Me.splitGraphiqueDonnées.Location = New System.Drawing.Point(288, 27)
        Me.splitGraphiqueDonnées.MinExtra = 100
        Me.splitGraphiqueDonnées.MinSize = 200
        Me.splitGraphiqueDonnées.Name = "splitGraphiqueDonnées"
        Me.splitGraphiqueDonnées.Size = New System.Drawing.Size(8, 570)
        Me.splitGraphiqueDonnées.TabIndex = 6
        Me.splitGraphiqueDonnées.TabStop = False
        '
        'pnlGéométrie
        '
        Me.pnlGéométrie.AutoScroll = True
        Me.pnlGéométrie.AutoScrollMinSize = New System.Drawing.Size(440, 150)
        Me.pnlGéométrie.Controls.Add(Me.pnlIlots)
        Me.pnlGéométrie.Controls.Add(Me.pnlBtnGéométrie)
        Me.pnlGéométrie.Controls.Add(Me.AC1GrilleBranches)
        Me.pnlGéométrie.Controls.Add(Me.Label1)
        Me.pnlGéométrie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGéométrie.Location = New System.Drawing.Point(0, 0)
        Me.pnlGéométrie.Name = "pnlGéométrie"
        Me.pnlGéométrie.Size = New System.Drawing.Size(912, 597)
        Me.pnlGéométrie.TabIndex = 7
        '
        'pnlIlots
        '
        Me.pnlIlots.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlIlots.Controls.Add(Me.AC1GrilleIlot)
        Me.pnlIlots.Controls.Add(Me.lblIlot)
        Me.pnlIlots.Location = New System.Drawing.Point(480, 120)
        Me.pnlIlots.Name = "pnlIlots"
        Me.pnlIlots.Size = New System.Drawing.Size(264, 160)
        Me.pnlIlots.TabIndex = 27
        Me.pnlIlots.Visible = False
        '
        'AC1GrilleIlot
        '
        Me.AC1GrilleIlot.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.AC1GrilleIlot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AC1GrilleIlot.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleIlot.ColumnInfo = resources.GetString("AC1GrilleIlot.ColumnInfo")
        Me.AC1GrilleIlot.Location = New System.Drawing.Point(32, 16)
        Me.AC1GrilleIlot.Name = "AC1GrilleIlot"
        Me.AC1GrilleIlot.Rows.Count = 2
        Me.AC1GrilleIlot.Size = New System.Drawing.Size(224, 40)
        Me.AC1GrilleIlot.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleIlot.Styles"))
        Me.AC1GrilleIlot.TabIndex = 29
        '
        'lblIlot
        '
        Me.lblIlot.Location = New System.Drawing.Point(0, 20)
        Me.lblIlot.Name = "lblIlot"
        Me.lblIlot.Size = New System.Drawing.Size(25, 16)
        Me.lblIlot.TabIndex = 28
        Me.lblIlot.Text = "Ilots"
        '
        'pnlBtnGéométrie
        '
        Me.pnlBtnGéométrie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBtnGéométrie.Controls.Add(Me.btnPiétonPlusRapide)
        Me.pnlBtnGéométrie.Controls.Add(Me.chkVerrouGéométrie)
        Me.pnlBtnGéométrie.Controls.Add(Me.btnPiétonPlus)
        Me.pnlBtnGéométrie.Controls.Add(Me.btnCarrefour)
        Me.pnlBtnGéométrie.Controls.Add(Me.btnPiétonMoins)
        Me.pnlBtnGéométrie.Controls.Add(Me.lblPassage)
        Me.pnlBtnGéométrie.Location = New System.Drawing.Point(744, 120)
        Me.pnlBtnGéométrie.Name = "pnlBtnGéométrie"
        Me.pnlBtnGéométrie.Size = New System.Drawing.Size(160, 192)
        Me.pnlBtnGéométrie.TabIndex = 24
        '
        'btnPiétonPlusRapide
        '
        Me.btnPiétonPlusRapide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPiétonPlusRapide.Image = CType(resources.GetObject("btnPiétonPlusRapide.Image"), System.Drawing.Image)
        Me.btnPiétonPlusRapide.Location = New System.Drawing.Point(48, 16)
        Me.btnPiétonPlusRapide.Name = "btnPiétonPlusRapide"
        Me.btnPiétonPlusRapide.Size = New System.Drawing.Size(24, 24)
        Me.btnPiétonPlusRapide.TabIndex = 34
        Me.tipBulle.SetToolTip(Me.btnPiétonPlusRapide, "Créer un passage rapidement")
        '
        'chkVerrouGéométrie
        '
        Me.chkVerrouGéométrie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouGéométrie.Location = New System.Drawing.Point(48, 112)
        Me.chkVerrouGéométrie.Name = "chkVerrouGéométrie"
        Me.chkVerrouGéométrie.Size = New System.Drawing.Size(88, 32)
        Me.chkVerrouGéométrie.TabIndex = 27
        Me.chkVerrouGéométrie.Tag = "1"
        Me.chkVerrouGéométrie.Text = "Verrouiller la géométrie"
        Me.tipBulle.SetToolTip(Me.chkVerrouGéométrie, "Verrouiller la géométrie")
        '
        'btnPiétonPlus
        '
        Me.btnPiétonPlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPiétonPlus.Image = CType(resources.GetObject("btnPiétonPlus.Image"), System.Drawing.Image)
        Me.btnPiétonPlus.Location = New System.Drawing.Point(88, 16)
        Me.btnPiétonPlus.Name = "btnPiétonPlus"
        Me.btnPiétonPlus.Size = New System.Drawing.Size(24, 24)
        Me.btnPiétonPlus.TabIndex = 33
        Me.tipBulle.SetToolTip(Me.btnPiétonPlus, "Créer un passage point par point")
        '
        'btnCarrefour
        '
        Me.btnCarrefour.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCarrefour.Location = New System.Drawing.Point(48, 56)
        Me.btnCarrefour.Name = "btnCarrefour"
        Me.btnCarrefour.Size = New System.Drawing.Size(80, 24)
        Me.btnCarrefour.TabIndex = 31
        Me.btnCarrefour.Text = "Carrefour..."
        '
        'btnPiétonMoins
        '
        Me.btnPiétonMoins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPiétonMoins.Image = CType(resources.GetObject("btnPiétonMoins.Image"), System.Drawing.Image)
        Me.btnPiétonMoins.Location = New System.Drawing.Point(128, 16)
        Me.btnPiétonMoins.Name = "btnPiétonMoins"
        Me.btnPiétonMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnPiétonMoins.TabIndex = 30
        Me.tipBulle.SetToolTip(Me.btnPiétonMoins, "Supprimer un passage")
        '
        'lblPassage
        '
        Me.lblPassage.Location = New System.Drawing.Point(0, 8)
        Me.lblPassage.Name = "lblPassage"
        Me.lblPassage.Size = New System.Drawing.Size(56, 32)
        Me.lblPassage.TabIndex = 28
        Me.lblPassage.Text = "Passage piétons"
        '
        'AC1GrilleBranches
        '
        Me.AC1GrilleBranches.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.AC1GrilleBranches.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AC1GrilleBranches.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleBranches.ColumnInfo = resources.GetString("AC1GrilleBranches.ColumnInfo")
        Me.AC1GrilleBranches.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.AC1GrilleBranches.Location = New System.Drawing.Point(480, 16)
        Me.AC1GrilleBranches.Name = "AC1GrilleBranches"
        Me.AC1GrilleBranches.Rows.Count = 4
        Me.AC1GrilleBranches.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AC1GrilleBranches.Size = New System.Drawing.Size(416, 72)
        Me.AC1GrilleBranches.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleBranches.Styles"))
        Me.AC1GrilleBranches.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 30)
        Me.Label1.TabIndex = 28
        '
        'pnlLignesDeFeux
        '
        Me.pnlLignesDeFeux.AutoScroll = True
        Me.pnlLignesDeFeux.AutoScrollMinSize = New System.Drawing.Size(500, 150)
        Me.pnlLignesDeFeux.Controls.Add(Me.Label4)
        Me.pnlLignesDeFeux.Controls.Add(Me.pnlTrajectoires)
        Me.pnlLignesDeFeux.Controls.Add(Me.chkVerrouLignesFeux)
        Me.pnlLignesDeFeux.Controls.Add(Me.AC1GrilleFeux)
        Me.pnlLignesDeFeux.Controls.Add(Me.pnlBoutonsLignesFeux)
        Me.pnlLignesDeFeux.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLignesDeFeux.Location = New System.Drawing.Point(0, 0)
        Me.pnlLignesDeFeux.Name = "pnlLignesDeFeux"
        Me.pnlLignesDeFeux.Size = New System.Drawing.Size(912, 597)
        Me.pnlLignesDeFeux.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(432, 328)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 23)
        Me.Label4.TabIndex = 51
        '
        'pnlTrajectoires
        '
        Me.pnlTrajectoires.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTrajectoires.Controls.Add(Me.chkSensTrajectoires)
        Me.pnlTrajectoires.Controls.Add(Me.btnTrajMoinsTout)
        Me.pnlTrajectoires.Controls.Add(Me.btnTrajToutes)
        Me.pnlTrajectoires.Controls.Add(Me.btnTraverséeMoins)
        Me.pnlTrajectoires.Controls.Add(Me.btnTravProp)
        Me.pnlTrajectoires.Controls.Add(Me.btnTraversée)
        Me.pnlTrajectoires.Controls.Add(Me.lblTraversée)
        Me.pnlTrajectoires.Controls.Add(Me.btnTrajProp)
        Me.pnlTrajectoires.Controls.Add(Me.btnTrajectoireMoins)
        Me.pnlTrajectoires.Controls.Add(Me.btnTrajectoire)
        Me.pnlTrajectoires.Controls.Add(Me.lblTrajVehicule)
        Me.pnlTrajectoires.Location = New System.Drawing.Point(600, 416)
        Me.pnlTrajectoires.Name = "pnlTrajectoires"
        Me.pnlTrajectoires.Size = New System.Drawing.Size(288, 120)
        Me.pnlTrajectoires.TabIndex = 50
        '
        'chkSensTrajectoires
        '
        Me.chkSensTrajectoires.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSensTrajectoires.Location = New System.Drawing.Point(96, 96)
        Me.chkSensTrajectoires.Name = "chkSensTrajectoires"
        Me.chkSensTrajectoires.Size = New System.Drawing.Size(184, 16)
        Me.chkSensTrajectoires.TabIndex = 29
        Me.chkSensTrajectoires.Text = "Indiquer le sens des trajectoires"
        '
        'btnTrajMoinsTout
        '
        Me.btnTrajMoinsTout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrajMoinsTout.Image = CType(resources.GetObject("btnTrajMoinsTout.Image"), System.Drawing.Image)
        Me.btnTrajMoinsTout.Location = New System.Drawing.Point(256, 16)
        Me.btnTrajMoinsTout.Name = "btnTrajMoinsTout"
        Me.btnTrajMoinsTout.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajMoinsTout.TabIndex = 28
        Me.tipBulle.SetToolTip(Me.btnTrajMoinsTout, "Supprimer toutes les trajectoires")
        '
        'btnTrajToutes
        '
        Me.btnTrajToutes.Image = CType(resources.GetObject("btnTrajToutes.Image"), System.Drawing.Image)
        Me.btnTrajToutes.Location = New System.Drawing.Point(216, 16)
        Me.btnTrajToutes.Name = "btnTrajToutes"
        Me.btnTrajToutes.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajToutes.TabIndex = 27
        Me.tipBulle.SetToolTip(Me.btnTrajToutes, "Générer toutes les trajectoires")
        '
        'btnTraverséeMoins
        '
        Me.btnTraverséeMoins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTraverséeMoins.Image = CType(resources.GetObject("btnTraverséeMoins.Image"), System.Drawing.Image)
        Me.btnTraverséeMoins.Location = New System.Drawing.Point(136, 56)
        Me.btnTraverséeMoins.Name = "btnTraverséeMoins"
        Me.btnTraverséeMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnTraverséeMoins.TabIndex = 26
        Me.tipBulle.SetToolTip(Me.btnTraverséeMoins, "Supprimer une traversée")
        '
        'btnTravProp
        '
        Me.btnTravProp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTravProp.Image = CType(resources.GetObject("btnTravProp.Image"), System.Drawing.Image)
        Me.btnTravProp.Location = New System.Drawing.Point(176, 56)
        Me.btnTravProp.Name = "btnTravProp"
        Me.btnTravProp.Size = New System.Drawing.Size(24, 24)
        Me.btnTravProp.TabIndex = 25
        Me.tipBulle.SetToolTip(Me.btnTravProp, "Caractéristiques de la traversée")
        '
        'btnTraversée
        '
        Me.btnTraversée.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTraversée.Image = CType(resources.GetObject("btnTraversée.Image"), System.Drawing.Image)
        Me.btnTraversée.Location = New System.Drawing.Point(96, 56)
        Me.btnTraversée.Name = "btnTraversée"
        Me.btnTraversée.Size = New System.Drawing.Size(24, 24)
        Me.btnTraversée.TabIndex = 24
        Me.tipBulle.SetToolTip(Me.btnTraversée, "Créer une traversée")
        '
        'lblTraversée
        '
        Me.lblTraversée.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTraversée.Location = New System.Drawing.Point(16, 48)
        Me.lblTraversée.Name = "lblTraversée"
        Me.lblTraversée.Size = New System.Drawing.Size(64, 32)
        Me.lblTraversée.TabIndex = 23
        Me.lblTraversée.Text = "Traversée piétonne"
        '
        'btnTrajProp
        '
        Me.btnTrajProp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrajProp.Image = CType(resources.GetObject("btnTrajProp.Image"), System.Drawing.Image)
        Me.btnTrajProp.Location = New System.Drawing.Point(176, 16)
        Me.btnTrajProp.Name = "btnTrajProp"
        Me.btnTrajProp.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajProp.TabIndex = 22
        Me.tipBulle.SetToolTip(Me.btnTrajProp, "Caractéristiques de la trajectoire")
        '
        'btnTrajectoireMoins
        '
        Me.btnTrajectoireMoins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrajectoireMoins.Image = CType(resources.GetObject("btnTrajectoireMoins.Image"), System.Drawing.Image)
        Me.btnTrajectoireMoins.Location = New System.Drawing.Point(136, 16)
        Me.btnTrajectoireMoins.Name = "btnTrajectoireMoins"
        Me.btnTrajectoireMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajectoireMoins.TabIndex = 21
        Me.tipBulle.SetToolTip(Me.btnTrajectoireMoins, "Supprimer une trajectoire")
        '
        'btnTrajectoire
        '
        Me.btnTrajectoire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrajectoire.BackColor = System.Drawing.SystemColors.Control
        Me.btnTrajectoire.Image = CType(resources.GetObject("btnTrajectoire.Image"), System.Drawing.Image)
        Me.btnTrajectoire.Location = New System.Drawing.Point(96, 16)
        Me.btnTrajectoire.Name = "btnTrajectoire"
        Me.btnTrajectoire.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajectoire.TabIndex = 20
        Me.tipBulle.SetToolTip(Me.btnTrajectoire, "Construire une trajectoire")
        Me.btnTrajectoire.UseVisualStyleBackColor = False
        '
        'lblTrajVehicule
        '
        Me.lblTrajVehicule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTrajVehicule.Location = New System.Drawing.Point(16, 8)
        Me.lblTrajVehicule.Name = "lblTrajVehicule"
        Me.lblTrajVehicule.Size = New System.Drawing.Size(64, 32)
        Me.lblTrajVehicule.TabIndex = 19
        Me.lblTrajVehicule.Text = "Trajectoire véhicules"
        '
        'chkVerrouLignesFeux
        '
        Me.chkVerrouLignesFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouLignesFeux.Location = New System.Drawing.Point(696, 544)
        Me.chkVerrouLignesFeux.Name = "chkVerrouLignesFeux"
        Me.chkVerrouLignesFeux.Size = New System.Drawing.Size(168, 16)
        Me.chkVerrouLignesFeux.TabIndex = 49
        Me.chkVerrouLignesFeux.Tag = "2"
        Me.chkVerrouLignesFeux.Text = "Verrouiller les lignes de feux"
        Me.tipBulle.SetToolTip(Me.chkVerrouLignesFeux, "Verrouiller les lignes de feu")
        '
        'AC1GrilleFeux
        '
        Me.AC1GrilleFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AC1GrilleFeux.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleFeux.ColumnInfo = resources.GetString("AC1GrilleFeux.ColumnInfo")
        Me.AC1GrilleFeux.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Heavy
        Me.AC1GrilleFeux.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.AC1GrilleFeux.Location = New System.Drawing.Point(432, 24)
        Me.AC1GrilleFeux.Name = "AC1GrilleFeux"
        Me.AC1GrilleFeux.Rows.Count = 2
        Me.AC1GrilleFeux.Size = New System.Drawing.Size(464, 288)
        Me.AC1GrilleFeux.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleFeux.Styles"))
        Me.AC1GrilleFeux.TabIndex = 46
        '
        'pnlBoutonsLignesFeux
        '
        Me.pnlBoutonsLignesFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.btnLigneFeuxMoins)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.btnLigneFeux)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.cboTriLignesFeux)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.Label2)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.btnSignalMoins)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.lblTriLignesFeux)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.lblLigneFeu)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.btnLigneFeuDescendre)
        Me.pnlBoutonsLignesFeux.Controls.Add(Me.btnLigneFeuMonter)
        Me.pnlBoutonsLignesFeux.Location = New System.Drawing.Point(600, 328)
        Me.pnlBoutonsLignesFeux.Name = "pnlBoutonsLignesFeux"
        Me.pnlBoutonsLignesFeux.Size = New System.Drawing.Size(288, 88)
        Me.pnlBoutonsLignesFeux.TabIndex = 47
        '
        'btnLigneFeuxMoins
        '
        Me.btnLigneFeuxMoins.Image = CType(resources.GetObject("btnLigneFeuxMoins.Image"), System.Drawing.Image)
        Me.btnLigneFeuxMoins.Location = New System.Drawing.Point(136, 8)
        Me.btnLigneFeuxMoins.Name = "btnLigneFeuxMoins"
        Me.btnLigneFeuxMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeuxMoins.TabIndex = 57
        Me.tipBulle.SetToolTip(Me.btnLigneFeuxMoins, "Supprimer une ligne de feux")
        '
        'btnLigneFeux
        '
        Me.btnLigneFeux.Image = CType(resources.GetObject("btnLigneFeux.Image"), System.Drawing.Image)
        Me.btnLigneFeux.Location = New System.Drawing.Point(96, 8)
        Me.btnLigneFeux.Name = "btnLigneFeux"
        Me.btnLigneFeux.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeux.TabIndex = 56
        Me.tipBulle.SetToolTip(Me.btnLigneFeux, "Créer une ligne de feux")
        '
        'cboTriLignesFeux
        '
        Me.cboTriLignesFeux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriLignesFeux.Items.AddRange(New Object() {"Manuel", "Feux Véhicules en tête", "Par Branche", "Par nom de feux"})
        Me.cboTriLignesFeux.Location = New System.Drawing.Point(128, 40)
        Me.cboTriLignesFeux.Name = "cboTriLignesFeux"
        Me.cboTriLignesFeux.Size = New System.Drawing.Size(120, 21)
        Me.cboTriLignesFeux.TabIndex = 55
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(160, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 24)
        Me.Label2.TabIndex = 54
        '
        'btnSignalMoins
        '
        Me.btnSignalMoins.Image = CType(resources.GetObject("btnSignalMoins.Image"), System.Drawing.Image)
        Me.btnSignalMoins.Location = New System.Drawing.Point(168, 40)
        Me.btnSignalMoins.Name = "btnSignalMoins"
        Me.btnSignalMoins.Size = New System.Drawing.Size(21, 20)
        Me.btnSignalMoins.TabIndex = 53
        Me.tipBulle.SetToolTip(Me.btnSignalMoins, "Supprimer un signal")
        Me.btnSignalMoins.Visible = False
        '
        'lblTriLignesFeux
        '
        Me.lblTriLignesFeux.Location = New System.Drawing.Point(8, 40)
        Me.lblTriLignesFeux.Name = "lblTriLignesFeux"
        Me.lblTriLignesFeux.Size = New System.Drawing.Size(81, 16)
        Me.lblTriLignesFeux.TabIndex = 51
        Me.lblTriLignesFeux.Text = "Ordonner"
        '
        'lblLigneFeu
        '
        Me.lblLigneFeu.Location = New System.Drawing.Point(8, 16)
        Me.lblLigneFeu.Name = "lblLigneFeu"
        Me.lblLigneFeu.Size = New System.Drawing.Size(73, 16)
        Me.lblLigneFeu.TabIndex = 48
        Me.lblLigneFeu.Text = "Ligne de feux"
        '
        'btnLigneFeuDescendre
        '
        Me.btnLigneFeuDescendre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLigneFeuDescendre.Enabled = False
        Me.btnLigneFeuDescendre.Image = CType(resources.GetObject("btnLigneFeuDescendre.Image"), System.Drawing.Image)
        Me.btnLigneFeuDescendre.Location = New System.Drawing.Point(216, 8)
        Me.btnLigneFeuDescendre.Name = "btnLigneFeuDescendre"
        Me.btnLigneFeuDescendre.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeuDescendre.TabIndex = 47
        Me.tipBulle.SetToolTip(Me.btnLigneFeuDescendre, "Descendre la  ligne de feux")
        '
        'btnLigneFeuMonter
        '
        Me.btnLigneFeuMonter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLigneFeuMonter.Enabled = False
        Me.btnLigneFeuMonter.Image = CType(resources.GetObject("btnLigneFeuMonter.Image"), System.Drawing.Image)
        Me.btnLigneFeuMonter.Location = New System.Drawing.Point(176, 8)
        Me.btnLigneFeuMonter.Name = "btnLigneFeuMonter"
        Me.btnLigneFeuMonter.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeuMonter.TabIndex = 45
        Me.tipBulle.SetToolTip(Me.btnLigneFeuMonter, "Monter la ligne de feux")
        '
        'pnlTrafics
        '
        Me.pnlTrafics.AutoScroll = True
        Me.pnlTrafics.AutoScrollMinSize = New System.Drawing.Size(400, 150)
        Me.pnlTrafics.Controls.Add(Me.lblTaficSaturé)
        Me.pnlTrafics.Controls.Add(Me.chkVerrouPériode)
        Me.pnlTrafics.Controls.Add(Me.txtCommentairePériode)
        Me.pnlTrafics.Controls.Add(Me.lblCommentairePériode)
        Me.pnlTrafics.Controls.Add(Me.lblTraficVerrouillé)
        Me.pnlTrafics.Controls.Add(Me.btnSupprimerTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnNouveauTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnRenommerTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnDupliquerTrafic)
        Me.pnlTrafics.Controls.Add(Me.chkTraficRéférence)
        Me.pnlTrafics.Controls.Add(Me.lblPériode)
        Me.pnlTrafics.Controls.Add(Me.grpPiéton)
        Me.pnlTrafics.Controls.Add(Me.chkModeTrafic)
        Me.pnlTrafics.Controls.Add(Me.grpVéhicule)
        Me.pnlTrafics.Controls.Add(Me.cboTrafic)
        Me.pnlTrafics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTrafics.Location = New System.Drawing.Point(0, 0)
        Me.pnlTrafics.Name = "pnlTrafics"
        Me.pnlTrafics.Size = New System.Drawing.Size(912, 597)
        Me.pnlTrafics.TabIndex = 10
        '
        'lblTaficSaturé
        '
        Me.lblTaficSaturé.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTaficSaturé.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaficSaturé.ForeColor = System.Drawing.Color.Red
        Me.lblTaficSaturé.Location = New System.Drawing.Point(600, 520)
        Me.lblTaficSaturé.Name = "lblTaficSaturé"
        Me.lblTaficSaturé.Size = New System.Drawing.Size(168, 32)
        Me.lblTaficSaturé.TabIndex = 65
        Me.lblTaficSaturé.Text = "Les trafics sont supérieurs au débit de saturation"
        Me.lblTaficSaturé.Visible = False
        '
        'chkVerrouPériode
        '
        Me.chkVerrouPériode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouPériode.Location = New System.Drawing.Point(608, 448)
        Me.chkVerrouPériode.Name = "chkVerrouPériode"
        Me.chkVerrouPériode.Size = New System.Drawing.Size(136, 24)
        Me.chkVerrouPériode.TabIndex = 64
        Me.chkVerrouPériode.Text = "Verrouiller la période"
        '
        'txtCommentairePériode
        '
        Me.txtCommentairePériode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommentairePériode.Location = New System.Drawing.Point(696, 400)
        Me.txtCommentairePériode.Multiline = True
        Me.txtCommentairePériode.Name = "txtCommentairePériode"
        Me.txtCommentairePériode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCommentairePériode.Size = New System.Drawing.Size(200, 32)
        Me.txtCommentairePériode.TabIndex = 63
        '
        'lblCommentairePériode
        '
        Me.lblCommentairePériode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommentairePériode.Location = New System.Drawing.Point(608, 400)
        Me.lblCommentairePériode.Name = "lblCommentairePériode"
        Me.lblCommentairePériode.Size = New System.Drawing.Size(80, 28)
        Me.lblCommentairePériode.TabIndex = 62
        Me.lblCommentairePériode.Text = "Commentaires sur la période"
        '
        'lblTraficVerrouillé
        '
        Me.lblTraficVerrouillé.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTraficVerrouillé.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTraficVerrouillé.ForeColor = System.Drawing.Color.Red
        Me.lblTraficVerrouillé.Location = New System.Drawing.Point(600, 520)
        Me.lblTraficVerrouillé.Name = "lblTraficVerrouillé"
        Me.lblTraficVerrouillé.Size = New System.Drawing.Size(272, 32)
        Me.lblTraficVerrouillé.TabIndex = 61
        Me.lblTraficVerrouillé.Text = "Les conflits sont verrouillés                                  La période de réfé" & _
            "rence n'est plus modifiable"
        '
        'btnSupprimerTrafic
        '
        Me.btnSupprimerTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSupprimerTrafic.Enabled = False
        Me.btnSupprimerTrafic.Location = New System.Drawing.Point(752, 488)
        Me.btnSupprimerTrafic.Name = "btnSupprimerTrafic"
        Me.btnSupprimerTrafic.Size = New System.Drawing.Size(68, 24)
        Me.btnSupprimerTrafic.TabIndex = 59
        Me.btnSupprimerTrafic.Text = "Supprimer"
        '
        'btnNouveauTrafic
        '
        Me.btnNouveauTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNouveauTrafic.Location = New System.Drawing.Point(592, 488)
        Me.btnNouveauTrafic.Name = "btnNouveauTrafic"
        Me.btnNouveauTrafic.Size = New System.Drawing.Size(68, 24)
        Me.btnNouveauTrafic.TabIndex = 58
        Me.btnNouveauTrafic.Text = "Nouveau..."
        '
        'btnRenommerTrafic
        '
        Me.btnRenommerTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRenommerTrafic.Enabled = False
        Me.btnRenommerTrafic.Location = New System.Drawing.Point(672, 488)
        Me.btnRenommerTrafic.Name = "btnRenommerTrafic"
        Me.btnRenommerTrafic.Size = New System.Drawing.Size(68, 24)
        Me.btnRenommerTrafic.TabIndex = 60
        Me.btnRenommerTrafic.Text = "Renommer"
        '
        'btnDupliquerTrafic
        '
        Me.btnDupliquerTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDupliquerTrafic.Location = New System.Drawing.Point(832, 488)
        Me.btnDupliquerTrafic.Name = "btnDupliquerTrafic"
        Me.btnDupliquerTrafic.Size = New System.Drawing.Size(68, 24)
        Me.btnDupliquerTrafic.TabIndex = 30
        Me.btnDupliquerTrafic.Text = "Dupliquer"
        Me.tipBulle.SetToolTip(Me.btnDupliquerTrafic, "Positionner les indications de trafic sur le schéma")
        '
        'chkTraficRéférence
        '
        Me.chkTraficRéférence.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTraficRéférence.Location = New System.Drawing.Point(768, 56)
        Me.chkTraficRéférence.Name = "chkTraficRéférence"
        Me.chkTraficRéférence.Size = New System.Drawing.Size(130, 16)
        Me.chkTraficRéférence.TabIndex = 29
        Me.chkTraficRéférence.Text = "Matrice de référence"
        '
        'lblPériode
        '
        Me.lblPériode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPériode.Location = New System.Drawing.Point(616, 24)
        Me.lblPériode.Name = "lblPériode"
        Me.lblPériode.Size = New System.Drawing.Size(101, 23)
        Me.lblPériode.TabIndex = 24
        Me.lblPériode.Text = "Période de trafic :"
        '
        'grpPiéton
        '
        Me.grpPiéton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPiéton.Controls.Add(Me.Ac1GrilleTraficPiétons)
        Me.grpPiéton.Location = New System.Drawing.Point(600, 312)
        Me.grpPiéton.Name = "grpPiéton"
        Me.grpPiéton.Size = New System.Drawing.Size(296, 72)
        Me.grpPiéton.TabIndex = 27
        Me.grpPiéton.TabStop = False
        Me.grpPiéton.Text = "Trafic piétons"
        '
        'Ac1GrilleTraficPiétons
        '
        Me.Ac1GrilleTraficPiétons.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Ac1GrilleTraficPiétons.BackColor = System.Drawing.SystemColors.Window
        Me.Ac1GrilleTraficPiétons.ColumnInfo = resources.GetString("Ac1GrilleTraficPiétons.ColumnInfo")
        Me.Ac1GrilleTraficPiétons.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.Ac1GrilleTraficPiétons.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.Ac1GrilleTraficPiétons.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.Ac1GrilleTraficPiétons.Location = New System.Drawing.Point(24, 24)
        Me.Ac1GrilleTraficPiétons.Name = "Ac1GrilleTraficPiétons"
        Me.Ac1GrilleTraficPiétons.Rows.Count = 2
        Me.Ac1GrilleTraficPiétons.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.Ac1GrilleTraficPiétons.Size = New System.Drawing.Size(256, 40)
        Me.Ac1GrilleTraficPiétons.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Ac1GrilleTraficPiétons.Styles"))
        Me.Ac1GrilleTraficPiétons.TabIndex = 22
        '
        'chkModeTrafic
        '
        Me.chkModeTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkModeTrafic.Location = New System.Drawing.Point(608, 56)
        Me.chkModeTrafic.Name = "chkModeTrafic"
        Me.chkModeTrafic.Size = New System.Drawing.Size(161, 24)
        Me.chkModeTrafic.TabIndex = 28
        Me.chkModeTrafic.Text = "Saisir directement en UVP"
        '
        'grpVéhicule
        '
        Me.grpVéhicule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpVéhicule.Controls.Add(Me.AC1GrilleTraficVéhicules)
        Me.grpVéhicule.Controls.Add(Me.pnlTrafic)
        Me.grpVéhicule.Controls.Add(Me.lblUVP)
        Me.grpVéhicule.Location = New System.Drawing.Point(600, 88)
        Me.grpVéhicule.Name = "grpVéhicule"
        Me.grpVéhicule.Size = New System.Drawing.Size(296, 216)
        Me.grpVéhicule.TabIndex = 26
        Me.grpVéhicule.TabStop = False
        Me.grpVéhicule.Text = "Trafic Véhicules"
        '
        'AC1GrilleTraficVéhicules
        '
        Me.AC1GrilleTraficVéhicules.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.AC1GrilleTraficVéhicules.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleTraficVéhicules.ColumnInfo = resources.GetString("AC1GrilleTraficVéhicules.ColumnInfo")
        Me.AC1GrilleTraficVéhicules.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.AC1GrilleTraficVéhicules.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.AC1GrilleTraficVéhicules.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.AC1GrilleTraficVéhicules.Location = New System.Drawing.Point(8, 56)
        Me.AC1GrilleTraficVéhicules.Name = "AC1GrilleTraficVéhicules"
        Me.AC1GrilleTraficVéhicules.Rows.Count = 2
        Me.AC1GrilleTraficVéhicules.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.AC1GrilleTraficVéhicules.Size = New System.Drawing.Size(280, 152)
        Me.AC1GrilleTraficVéhicules.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleTraficVéhicules.Styles"))
        Me.AC1GrilleTraficVéhicules.TabIndex = 21
        '
        'pnlTrafic
        '
        Me.pnlTrafic.Controls.Add(Me.radUVP)
        Me.pnlTrafic.Controls.Add(Me.rad2Roues)
        Me.pnlTrafic.Controls.Add(Me.radPL)
        Me.pnlTrafic.Controls.Add(Me.radVL)
        Me.pnlTrafic.Location = New System.Drawing.Point(16, 24)
        Me.pnlTrafic.Name = "pnlTrafic"
        Me.pnlTrafic.Size = New System.Drawing.Size(248, 24)
        Me.pnlTrafic.TabIndex = 19
        '
        'radUVP
        '
        Me.radUVP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radUVP.Location = New System.Drawing.Point(176, 0)
        Me.radUVP.Name = "radUVP"
        Me.radUVP.Size = New System.Drawing.Size(72, 24)
        Me.radUVP.TabIndex = 26
        Me.radUVP.Text = "Voir UVP"
        '
        'rad2Roues
        '
        Me.rad2Roues.Location = New System.Drawing.Point(112, 0)
        Me.rad2Roues.Name = "rad2Roues"
        Me.rad2Roues.Size = New System.Drawing.Size(64, 24)
        Me.rad2Roues.TabIndex = 25
        Me.rad2Roues.Text = "2 roues"
        '
        'radPL
        '
        Me.radPL.Location = New System.Drawing.Point(56, 0)
        Me.radPL.Name = "radPL"
        Me.radPL.Size = New System.Drawing.Size(56, 24)
        Me.radPL.TabIndex = 24
        Me.radPL.Text = "PL"
        '
        'radVL
        '
        Me.radVL.Checked = True
        Me.radVL.Location = New System.Drawing.Point(0, 0)
        Me.radVL.Name = "radVL"
        Me.radVL.Size = New System.Drawing.Size(56, 24)
        Me.radVL.TabIndex = 23
        Me.radVL.TabStop = True
        Me.radVL.Text = "VL"
        '
        'lblUVP
        '
        Me.lblUVP.Location = New System.Drawing.Point(16, 24)
        Me.lblUVP.Name = "lblUVP"
        Me.lblUVP.Size = New System.Drawing.Size(100, 23)
        Me.lblUVP.TabIndex = 20
        Me.lblUVP.Text = "UVP"
        '
        'cboTrafic
        '
        Me.cboTrafic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTrafic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrafic.Location = New System.Drawing.Point(736, 24)
        Me.cboTrafic.Name = "cboTrafic"
        Me.cboTrafic.Size = New System.Drawing.Size(136, 21)
        Me.cboTrafic.TabIndex = 25
        '
        'pnlPlansDeFeux
        '
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlCarrefourComposé)
        Me.pnlPlansDeFeux.Controls.Add(Me.splitCarrefourComposé)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlFeuFonctionnement)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlPhasage)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlFeuBase)
        Me.pnlPlansDeFeux.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPlansDeFeux.Location = New System.Drawing.Point(0, 0)
        Me.pnlPlansDeFeux.Name = "pnlPlansDeFeux"
        Me.pnlPlansDeFeux.Size = New System.Drawing.Size(912, 597)
        Me.pnlPlansDeFeux.TabIndex = 10
        '
        'pnlCarrefourComposé
        '
        Me.pnlCarrefourComposé.Controls.Add(Me.chkScénarioDéfinitif)
        Me.pnlCarrefourComposé.Controls.Add(Me.radFeuFonctionnement)
        Me.pnlCarrefourComposé.Controls.Add(Me.radFeuBase)
        Me.pnlCarrefourComposé.Controls.Add(Me.radPhasage)
        Me.pnlCarrefourComposé.Controls.Add(Me.cboCarrefourComposé)
        Me.pnlCarrefourComposé.Controls.Add(Me.lblCarrefourComposé)
        Me.pnlCarrefourComposé.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCarrefourComposé.Location = New System.Drawing.Point(0, 3)
        Me.pnlCarrefourComposé.Name = "pnlCarrefourComposé"
        Me.pnlCarrefourComposé.Size = New System.Drawing.Size(912, 80)
        Me.pnlCarrefourComposé.TabIndex = 0
        '
        'chkScénarioDéfinitif
        '
        Me.chkScénarioDéfinitif.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkScénarioDéfinitif.Location = New System.Drawing.Point(654, 8)
        Me.chkScénarioDéfinitif.Name = "chkScénarioDéfinitif"
        Me.chkScénarioDéfinitif.Size = New System.Drawing.Size(72, 40)
        Me.chkScénarioDéfinitif.TabIndex = 54
        Me.chkScénarioDéfinitif.Text = "Scénario définitif"
        '
        'radFeuFonctionnement
        '
        Me.radFeuFonctionnement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radFeuFonctionnement.Location = New System.Drawing.Point(730, 56)
        Me.radFeuFonctionnement.Name = "radFeuFonctionnement"
        Me.radFeuFonctionnement.Size = New System.Drawing.Size(184, 16)
        Me.radFeuFonctionnement.TabIndex = 53
        Me.radFeuFonctionnement.Text = "Plan de feux de fonctionnement"
        '
        'radFeuBase
        '
        Me.radFeuBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radFeuBase.Location = New System.Drawing.Point(730, 32)
        Me.radFeuBase.Name = "radFeuBase"
        Me.radFeuBase.Size = New System.Drawing.Size(136, 16)
        Me.radFeuBase.TabIndex = 52
        Me.radFeuBase.Text = "Plan de feux de base"
        '
        'radPhasage
        '
        Me.radPhasage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radPhasage.Location = New System.Drawing.Point(730, 8)
        Me.radPhasage.Name = "radPhasage"
        Me.radPhasage.Size = New System.Drawing.Size(152, 16)
        Me.radPhasage.TabIndex = 51
        Me.radPhasage.Text = "Organisation du phasage"
        '
        'cboCarrefourComposé
        '
        Me.cboCarrefourComposé.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCarrefourComposé.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cboCarrefourComposé.Location = New System.Drawing.Point(24, 32)
        Me.cboCarrefourComposé.Name = "cboCarrefourComposé"
        Me.cboCarrefourComposé.Size = New System.Drawing.Size(40, 21)
        Me.cboCarrefourComposé.TabIndex = 2
        Me.cboCarrefourComposé.Visible = False
        '
        'lblCarrefourComposé
        '
        Me.lblCarrefourComposé.Location = New System.Drawing.Point(8, 8)
        Me.lblCarrefourComposé.Name = "lblCarrefourComposé"
        Me.lblCarrefourComposé.Size = New System.Drawing.Size(104, 16)
        Me.lblCarrefourComposé.TabIndex = 1
        Me.lblCarrefourComposé.Text = "Carrefour composé"
        Me.lblCarrefourComposé.Visible = False
        '
        'splitCarrefourComposé
        '
        Me.splitCarrefourComposé.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitCarrefourComposé.Enabled = False
        Me.splitCarrefourComposé.Location = New System.Drawing.Point(0, 0)
        Me.splitCarrefourComposé.Name = "splitCarrefourComposé"
        Me.splitCarrefourComposé.Size = New System.Drawing.Size(912, 3)
        Me.splitCarrefourComposé.TabIndex = 1
        Me.splitCarrefourComposé.TabStop = False
        Me.splitCarrefourComposé.Visible = False
        '
        'pnlFeuFonctionnement
        '
        Me.pnlFeuFonctionnement.AutoScroll = True
        Me.pnlFeuFonctionnement.AutoScrollMinSize = New System.Drawing.Size(380, 150)
        Me.pnlFeuFonctionnement.Controls.Add(Me.lblTraficFct)
        Me.pnlFeuFonctionnement.Controls.Add(Me.cboTraficFct)
        Me.pnlFeuFonctionnement.Controls.Add(Me.lblPlansDeFeux)
        Me.pnlFeuFonctionnement.Controls.Add(Me.cboPlansDeFeux)
        Me.pnlFeuFonctionnement.Controls.Add(Me.btnDiagnostic)
        Me.pnlFeuFonctionnement.Controls.Add(Me.btnSupprimerPlanFeux)
        Me.pnlFeuFonctionnement.Controls.Add(Me.btnDupliquerPlanFeux)
        Me.pnlFeuFonctionnement.Controls.Add(Me.btnRenommerPlanFeux)
        Me.pnlFeuFonctionnement.Controls.Add(Me.grpPhasesFct)
        Me.pnlFeuFonctionnement.Controls.Add(Me.grpSynchroFct)
        Me.pnlFeuFonctionnement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFeuFonctionnement.Location = New System.Drawing.Point(0, 0)
        Me.pnlFeuFonctionnement.Name = "pnlFeuFonctionnement"
        Me.pnlFeuFonctionnement.Size = New System.Drawing.Size(912, 597)
        Me.pnlFeuFonctionnement.TabIndex = 27
        '
        'lblTraficFct
        '
        Me.lblTraficFct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTraficFct.Location = New System.Drawing.Point(576, 36)
        Me.lblTraficFct.Name = "lblTraficFct"
        Me.lblTraficFct.Size = New System.Drawing.Size(88, 16)
        Me.lblTraficFct.TabIndex = 60
        Me.lblTraficFct.Text = "Période de trafic"
        '
        'cboTraficFct
        '
        Me.cboTraficFct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTraficFct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTraficFct.Items.AddRange(New Object() {"<Aucune>"})
        Me.cboTraficFct.Location = New System.Drawing.Point(664, 32)
        Me.cboTraficFct.Name = "cboTraficFct"
        Me.cboTraficFct.Size = New System.Drawing.Size(200, 21)
        Me.cboTraficFct.TabIndex = 59
        '
        'lblPlansDeFeux
        '
        Me.lblPlansDeFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPlansDeFeux.Location = New System.Drawing.Point(576, 0)
        Me.lblPlansDeFeux.Name = "lblPlansDeFeux"
        Me.lblPlansDeFeux.Size = New System.Drawing.Size(72, 16)
        Me.lblPlansDeFeux.TabIndex = 54
        Me.lblPlansDeFeux.Text = "Plan de feux"
        Me.lblPlansDeFeux.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboPlansDeFeux
        '
        Me.cboPlansDeFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPlansDeFeux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlansDeFeux.Location = New System.Drawing.Point(664, 0)
        Me.cboPlansDeFeux.Name = "cboPlansDeFeux"
        Me.cboPlansDeFeux.Size = New System.Drawing.Size(200, 21)
        Me.cboPlansDeFeux.TabIndex = 53
        '
        'btnDiagnostic
        '
        Me.btnDiagnostic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDiagnostic.Enabled = False
        Me.btnDiagnostic.Location = New System.Drawing.Point(536, 464)
        Me.btnDiagnostic.Name = "btnDiagnostic"
        Me.btnDiagnostic.Size = New System.Drawing.Size(83, 24)
        Me.btnDiagnostic.TabIndex = 58
        Me.btnDiagnostic.Text = "Diagnostic..."
        '
        'btnSupprimerPlanFeux
        '
        Me.btnSupprimerPlanFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSupprimerPlanFeux.Enabled = False
        Me.btnSupprimerPlanFeux.Location = New System.Drawing.Point(824, 464)
        Me.btnSupprimerPlanFeux.Name = "btnSupprimerPlanFeux"
        Me.btnSupprimerPlanFeux.Size = New System.Drawing.Size(83, 24)
        Me.btnSupprimerPlanFeux.TabIndex = 56
        Me.btnSupprimerPlanFeux.Text = "Supprimer"
        '
        'btnDupliquerPlanFeux
        '
        Me.btnDupliquerPlanFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDupliquerPlanFeux.Location = New System.Drawing.Point(632, 464)
        Me.btnDupliquerPlanFeux.Name = "btnDupliquerPlanFeux"
        Me.btnDupliquerPlanFeux.Size = New System.Drawing.Size(83, 24)
        Me.btnDupliquerPlanFeux.TabIndex = 55
        Me.btnDupliquerPlanFeux.Text = "Nouveau..."
        '
        'btnRenommerPlanFeux
        '
        Me.btnRenommerPlanFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRenommerPlanFeux.Enabled = False
        Me.btnRenommerPlanFeux.Location = New System.Drawing.Point(728, 464)
        Me.btnRenommerPlanFeux.Name = "btnRenommerPlanFeux"
        Me.btnRenommerPlanFeux.Size = New System.Drawing.Size(83, 24)
        Me.btnRenommerPlanFeux.TabIndex = 57
        Me.btnRenommerPlanFeux.Text = "Renommer"
        '
        'grpPhasesFct
        '
        Me.grpPhasesFct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPhasesFct.Controls.Add(Me.cboRéserveCapacitéChoisie)
        Me.grpPhasesFct.Controls.Add(Me.lblRéservCapacitéChoisie)
        Me.grpPhasesFct.Controls.Add(Me.btnCalculerCycle)
        Me.grpPhasesFct.Controls.Add(Me.cboMéthodeCalculCycle)
        Me.grpPhasesFct.Controls.Add(Me.lblMéthodeCalculCycle)
        Me.grpPhasesFct.Controls.Add(Me.radPhase3Fct)
        Me.grpPhasesFct.Controls.Add(Me.radPhase2Fct)
        Me.grpPhasesFct.Controls.Add(Me.radPhase1Fct)
        Me.grpPhasesFct.Controls.Add(Me.lbFigerDuréeFct)
        Me.grpPhasesFct.Controls.Add(Me.txtDuréeCycleFct)
        Me.grpPhasesFct.Controls.Add(Me.lblCycleFct)
        Me.grpPhasesFct.Controls.Add(Me.lblPhase3Fct)
        Me.grpPhasesFct.Controls.Add(Me.lblPhase2Fct)
        Me.grpPhasesFct.Controls.Add(Me.lblPhase1Fct)
        Me.grpPhasesFct.Controls.Add(Me.updPhase3Fct)
        Me.grpPhasesFct.Controls.Add(Me.updPhase2Fct)
        Me.grpPhasesFct.Controls.Add(Me.updPhase1Fct)
        Me.grpPhasesFct.Location = New System.Drawing.Point(568, 60)
        Me.grpPhasesFct.Name = "grpPhasesFct"
        Me.grpPhasesFct.Size = New System.Drawing.Size(320, 168)
        Me.grpPhasesFct.TabIndex = 49
        Me.grpPhasesFct.TabStop = False
        Me.grpPhasesFct.Text = "Durées des phases"
        '
        'cboRéserveCapacitéChoisie
        '
        Me.cboRéserveCapacitéChoisie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRéserveCapacitéChoisie.Items.AddRange(New Object() {"  0%", "10%", "15%", "20%"})
        Me.cboRéserveCapacitéChoisie.Location = New System.Drawing.Point(96, 136)
        Me.cboRéserveCapacitéChoisie.Name = "cboRéserveCapacitéChoisie"
        Me.cboRéserveCapacitéChoisie.Size = New System.Drawing.Size(48, 21)
        Me.cboRéserveCapacitéChoisie.TabIndex = 41
        '
        'lblRéservCapacitéChoisie
        '
        Me.lblRéservCapacitéChoisie.Location = New System.Drawing.Point(16, 128)
        Me.lblRéservCapacitéChoisie.Name = "lblRéservCapacitéChoisie"
        Me.lblRéservCapacitéChoisie.Size = New System.Drawing.Size(64, 32)
        Me.lblRéservCapacitéChoisie.TabIndex = 40
        Me.lblRéservCapacitéChoisie.Text = "Réserve de capacité :"
        '
        'btnCalculerCycle
        '
        Me.btnCalculerCycle.Enabled = False
        Me.btnCalculerCycle.Location = New System.Drawing.Point(160, 136)
        Me.btnCalculerCycle.Name = "btnCalculerCycle"
        Me.btnCalculerCycle.Size = New System.Drawing.Size(80, 24)
        Me.btnCalculerCycle.TabIndex = 39
        Me.btnCalculerCycle.Text = "Calculer"
        '
        'cboMéthodeCalculCycle
        '
        Me.cboMéthodeCalculCycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMéthodeCalculCycle.Items.AddRange(New Object() {"Manuellement", "Méthode de Webster", "Méthode classique"})
        Me.cboMéthodeCalculCycle.Location = New System.Drawing.Point(160, 104)
        Me.cboMéthodeCalculCycle.Name = "cboMéthodeCalculCycle"
        Me.cboMéthodeCalculCycle.Size = New System.Drawing.Size(128, 21)
        Me.cboMéthodeCalculCycle.TabIndex = 38
        '
        'lblMéthodeCalculCycle
        '
        Me.lblMéthodeCalculCycle.Location = New System.Drawing.Point(16, 104)
        Me.lblMéthodeCalculCycle.Name = "lblMéthodeCalculCycle"
        Me.lblMéthodeCalculCycle.Size = New System.Drawing.Size(144, 16)
        Me.lblMéthodeCalculCycle.TabIndex = 37
        Me.lblMéthodeCalculCycle.Text = "Durée du cycle déterminée"
        '
        'radPhase3Fct
        '
        Me.radPhase3Fct.Location = New System.Drawing.Point(256, 64)
        Me.radPhase3Fct.Name = "radPhase3Fct"
        Me.radPhase3Fct.Size = New System.Drawing.Size(16, 24)
        Me.radPhase3Fct.TabIndex = 36
        '
        'radPhase2Fct
        '
        Me.radPhase2Fct.Location = New System.Drawing.Point(192, 64)
        Me.radPhase2Fct.Name = "radPhase2Fct"
        Me.radPhase2Fct.Size = New System.Drawing.Size(16, 24)
        Me.radPhase2Fct.TabIndex = 35
        '
        'radPhase1Fct
        '
        Me.radPhase1Fct.Location = New System.Drawing.Point(128, 64)
        Me.radPhase1Fct.Name = "radPhase1Fct"
        Me.radPhase1Fct.Size = New System.Drawing.Size(16, 24)
        Me.radPhase1Fct.TabIndex = 34
        '
        'lbFigerDuréeFct
        '
        Me.lbFigerDuréeFct.Location = New System.Drawing.Point(16, 68)
        Me.lbFigerDuréeFct.Name = "lbFigerDuréeFct"
        Me.lbFigerDuréeFct.Size = New System.Drawing.Size(88, 26)
        Me.lbFigerDuréeFct.TabIndex = 33
        Me.lbFigerDuréeFct.Text = "Figer la durée de la phase"
        '
        'txtDuréeCycleFct
        '
        Me.txtDuréeCycleFct.Location = New System.Drawing.Point(32, 40)
        Me.txtDuréeCycleFct.Name = "txtDuréeCycleFct"
        Me.txtDuréeCycleFct.Size = New System.Drawing.Size(24, 20)
        Me.txtDuréeCycleFct.TabIndex = 29
        Me.txtDuréeCycleFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCycleFct
        '
        Me.lblCycleFct.Location = New System.Drawing.Point(32, 16)
        Me.lblCycleFct.Name = "lblCycleFct"
        Me.lblCycleFct.Size = New System.Drawing.Size(48, 24)
        Me.lblCycleFct.TabIndex = 28
        Me.lblCycleFct.Text = "Cycle"
        '
        'lblPhase3Fct
        '
        Me.lblPhase3Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase3Fct.Location = New System.Drawing.Point(248, 16)
        Me.lblPhase3Fct.Name = "lblPhase3Fct"
        Me.lblPhase3Fct.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase3Fct.TabIndex = 27
        Me.lblPhase3Fct.Text = "Phase 3"
        '
        'lblPhase2Fct
        '
        Me.lblPhase2Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase2Fct.Location = New System.Drawing.Point(184, 16)
        Me.lblPhase2Fct.Name = "lblPhase2Fct"
        Me.lblPhase2Fct.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase2Fct.TabIndex = 26
        Me.lblPhase2Fct.Text = "Phase 2"
        '
        'lblPhase1Fct
        '
        Me.lblPhase1Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase1Fct.Location = New System.Drawing.Point(120, 16)
        Me.lblPhase1Fct.Name = "lblPhase1Fct"
        Me.lblPhase1Fct.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase1Fct.TabIndex = 25
        Me.lblPhase1Fct.Text = "Phase 1"
        '
        'updPhase3Fct
        '
        Me.updPhase3Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase3Fct.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase3Fct.Location = New System.Drawing.Point(248, 40)
        Me.updPhase3Fct.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase3Fct.Name = "updPhase3Fct"
        Me.updPhase3Fct.ReadOnly = True
        Me.updPhase3Fct.Size = New System.Drawing.Size(40, 20)
        Me.updPhase3Fct.TabIndex = 23
        Me.updPhase3Fct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'updPhase2Fct
        '
        Me.updPhase2Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase2Fct.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase2Fct.Location = New System.Drawing.Point(184, 40)
        Me.updPhase2Fct.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase2Fct.Name = "updPhase2Fct"
        Me.updPhase2Fct.ReadOnly = True
        Me.updPhase2Fct.Size = New System.Drawing.Size(40, 20)
        Me.updPhase2Fct.TabIndex = 22
        Me.updPhase2Fct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'updPhase1Fct
        '
        Me.updPhase1Fct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase1Fct.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase1Fct.Location = New System.Drawing.Point(120, 40)
        Me.updPhase1Fct.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase1Fct.Name = "updPhase1Fct"
        Me.updPhase1Fct.ReadOnly = True
        Me.updPhase1Fct.Size = New System.Drawing.Size(40, 20)
        Me.updPhase1Fct.TabIndex = 21
        Me.updPhase1Fct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'grpSynchroFct
        '
        Me.grpSynchroFct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSynchroFct.Controls.Add(Me.lblDécalagesFct)
        Me.grpSynchroFct.Controls.Add(Me.updDécalageFermetureFct)
        Me.grpSynchroFct.Controls.Add(Me.updDécalageOuvertureFct)
        Me.grpSynchroFct.Controls.Add(Me.lvwDuréeVertFct)
        Me.grpSynchroFct.Location = New System.Drawing.Point(568, 236)
        Me.grpSynchroFct.Name = "grpSynchroFct"
        Me.grpSynchroFct.Size = New System.Drawing.Size(320, 216)
        Me.grpSynchroFct.TabIndex = 50
        Me.grpSynchroFct.TabStop = False
        Me.grpSynchroFct.Text = "Synchronisations"
        '
        'lblDécalagesFct
        '
        Me.lblDécalagesFct.Location = New System.Drawing.Point(208, 8)
        Me.lblDécalagesFct.Name = "lblDécalagesFct"
        Me.lblDécalagesFct.Size = New System.Drawing.Size(64, 16)
        Me.lblDécalagesFct.TabIndex = 28
        Me.lblDécalagesFct.Text = "Décalages"
        '
        'updDécalageFermetureFct
        '
        Me.updDécalageFermetureFct.Location = New System.Drawing.Point(256, 24)
        Me.updDécalageFermetureFct.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updDécalageFermetureFct.Name = "updDécalageFermetureFct"
        Me.updDécalageFermetureFct.Size = New System.Drawing.Size(32, 20)
        Me.updDécalageFermetureFct.TabIndex = 11
        Me.updDécalageFermetureFct.Visible = False
        '
        'updDécalageOuvertureFct
        '
        Me.updDécalageOuvertureFct.Location = New System.Drawing.Point(192, 24)
        Me.updDécalageOuvertureFct.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updDécalageOuvertureFct.Name = "updDécalageOuvertureFct"
        Me.updDécalageOuvertureFct.Size = New System.Drawing.Size(32, 20)
        Me.updDécalageOuvertureFct.TabIndex = 4
        Me.updDécalageOuvertureFct.Visible = False
        '
        'lvwDuréeVertFct
        '
        Me.lvwDuréeVertFct.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLFFct, Me.lvwcolPhaseFct, Me.lvwcolDuréeFct, Me.lvwcolDécalOuvertureFct, Me.lvwcolDécalFermetureFct})
        Me.lvwDuréeVertFct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDuréeVertFct.FullRowSelect = True
        Me.lvwDuréeVertFct.Location = New System.Drawing.Point(4, 48)
        Me.lvwDuréeVertFct.MultiSelect = False
        Me.lvwDuréeVertFct.Name = "lvwDuréeVertFct"
        Me.lvwDuréeVertFct.Size = New System.Drawing.Size(312, 164)
        Me.lvwDuréeVertFct.TabIndex = 0
        Me.lvwDuréeVertFct.UseCompatibleStateImageBehavior = False
        Me.lvwDuréeVertFct.View = System.Windows.Forms.View.Details
        '
        'lvwcolLFFct
        '
        Me.lvwcolLFFct.Text = "Ligne"
        Me.lvwcolLFFct.Width = 86
        '
        'lvwcolPhaseFct
        '
        Me.lvwcolPhaseFct.Text = "Phase"
        Me.lvwcolPhaseFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolPhaseFct.Width = 42
        '
        'lvwcolDuréeFct
        '
        Me.lvwcolDuréeFct.Text = "Durée vert"
        Me.lvwcolDuréeFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDuréeFct.Width = 71
        '
        'lvwcolDécalOuvertureFct
        '
        Me.lvwcolDécalOuvertureFct.Text = "Ouverture"
        Me.lvwcolDécalOuvertureFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvwcolDécalFermetureFct
        '
        Me.lvwcolDécalFermetureFct.Text = "Fermeture"
        Me.lvwcolDécalFermetureFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDécalFermetureFct.Width = 72
        '
        'pnlPhasage
        '
        Me.pnlPhasage.AutoScroll = True
        Me.pnlPhasage.AutoScrollMinSize = New System.Drawing.Size(280, 150)
        Me.pnlPhasage.Controls.Add(Me.pnlTableauPhasage)
        Me.pnlPhasage.Controls.Add(Me.pnlFiltrePhasage)
        Me.pnlPhasage.Controls.Add(Me.btnActionPhase)
        Me.pnlPhasage.Controls.Add(Me.lblDécoupagePhases)
        Me.pnlPhasage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPhasage.Location = New System.Drawing.Point(0, 0)
        Me.pnlPhasage.Name = "pnlPhasage"
        Me.pnlPhasage.Size = New System.Drawing.Size(912, 597)
        Me.pnlPhasage.TabIndex = 5
        '
        'pnlTableauPhasage
        '
        Me.pnlTableauPhasage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTableauPhasage.Controls.Add(Me.lblConflitPotentiel)
        Me.pnlTableauPhasage.Controls.Add(Me.pnlConflitPotentiel)
        Me.pnlTableauPhasage.Controls.Add(Me.chkDécoupagePhases)
        Me.pnlTableauPhasage.Controls.Add(Me.AC1GrillePhases)
        Me.pnlTableauPhasage.Location = New System.Drawing.Point(640, 168)
        Me.pnlTableauPhasage.Name = "pnlTableauPhasage"
        Me.pnlTableauPhasage.Size = New System.Drawing.Size(266, 280)
        Me.pnlTableauPhasage.TabIndex = 63
        '
        'lblConflitPotentiel
        '
        Me.lblConflitPotentiel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblConflitPotentiel.Location = New System.Drawing.Point(165, 256)
        Me.lblConflitPotentiel.Name = "lblConflitPotentiel"
        Me.lblConflitPotentiel.Size = New System.Drawing.Size(96, 16)
        Me.lblConflitPotentiel.TabIndex = 50
        Me.lblConflitPotentiel.Text = "Conflit potentiel"
        '
        'pnlConflitPotentiel
        '
        Me.pnlConflitPotentiel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlConflitPotentiel.BackColor = System.Drawing.Color.LightSalmon
        Me.pnlConflitPotentiel.Location = New System.Drawing.Point(141, 256)
        Me.pnlConflitPotentiel.Name = "pnlConflitPotentiel"
        Me.pnlConflitPotentiel.Size = New System.Drawing.Size(24, 16)
        Me.pnlConflitPotentiel.TabIndex = 49
        '
        'chkDécoupagePhases
        '
        Me.chkDécoupagePhases.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkDécoupagePhases.Location = New System.Drawing.Point(5, 256)
        Me.chkDécoupagePhases.Name = "chkDécoupagePhases"
        Me.chkDécoupagePhases.Size = New System.Drawing.Size(128, 16)
        Me.chkDécoupagePhases.TabIndex = 48
        Me.chkDécoupagePhases.Text = "Retenir ce phasage"
        '
        'AC1GrillePhases
        '
        Me.AC1GrillePhases.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.AC1GrillePhases.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AC1GrillePhases.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrillePhases.ColumnInfo = resources.GetString("AC1GrillePhases.ColumnInfo")
        Me.AC1GrillePhases.Location = New System.Drawing.Point(5, 0)
        Me.AC1GrillePhases.Name = "AC1GrillePhases"
        Me.AC1GrillePhases.Rows.Count = 4
        Me.AC1GrillePhases.ShowSort = False
        Me.AC1GrillePhases.Size = New System.Drawing.Size(256, 80)
        Me.AC1GrillePhases.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrillePhases.Styles"))
        Me.AC1GrillePhases.TabIndex = 43
        '
        'pnlFiltrePhasage
        '
        Me.pnlFiltrePhasage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFiltrePhasage.Controls.Add(Me.cboDécoupagePhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.txtRéserveCapacitéPourCent)
        Me.pnlFiltrePhasage.Controls.Add(Me.cboPhasesSpéciales)
        Me.pnlFiltrePhasage.Controls.Add(Me.cbolLFMultiPhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblPhasesSpéciales)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblLFMultiPhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblRéserveCapacité)
        Me.pnlFiltrePhasage.Controls.Add(Me.cboRéserveCapacité)
        Me.pnlFiltrePhasage.Controls.Add(Me.chk3Phases)
        Me.pnlFiltrePhasage.Location = New System.Drawing.Point(640, 32)
        Me.pnlFiltrePhasage.Name = "pnlFiltrePhasage"
        Me.pnlFiltrePhasage.Size = New System.Drawing.Size(266, 128)
        Me.pnlFiltrePhasage.TabIndex = 62
        '
        'cboDécoupagePhases
        '
        Me.cboDécoupagePhases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDécoupagePhases.Location = New System.Drawing.Point(8, 0)
        Me.cboDécoupagePhases.Name = "cboDécoupagePhases"
        Me.cboDécoupagePhases.Size = New System.Drawing.Size(97, 21)
        Me.cboDécoupagePhases.TabIndex = 36
        '
        'txtRéserveCapacitéPourCent
        '
        Me.txtRéserveCapacitéPourCent.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtRéserveCapacitéPourCent.Location = New System.Drawing.Point(224, 24)
        Me.txtRéserveCapacitéPourCent.Name = "txtRéserveCapacitéPourCent"
        Me.txtRéserveCapacitéPourCent.ReadOnly = True
        Me.txtRéserveCapacitéPourCent.Size = New System.Drawing.Size(33, 20)
        Me.txtRéserveCapacitéPourCent.TabIndex = 70
        Me.txtRéserveCapacitéPourCent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboPhasesSpéciales
        '
        Me.cboPhasesSpéciales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPhasesSpéciales.Enabled = False
        Me.cboPhasesSpéciales.Items.AddRange(New Object() {"Inclure ces phasages", "Exclure ces phasages", "Ne proposer que ceux-là"})
        Me.cboPhasesSpéciales.Location = New System.Drawing.Point(144, 96)
        Me.cboPhasesSpéciales.Name = "cboPhasesSpéciales"
        Me.cboPhasesSpéciales.Size = New System.Drawing.Size(124, 21)
        Me.cboPhasesSpéciales.TabIndex = 69
        '
        'cbolLFMultiPhases
        '
        Me.cbolLFMultiPhases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbolLFMultiPhases.Enabled = False
        Me.cbolLFMultiPhases.Items.AddRange(New Object() {"Inclure ces phasages", "Exclure ces phasages", "Ne proposer que ceux-là"})
        Me.cbolLFMultiPhases.Location = New System.Drawing.Point(8, 96)
        Me.cbolLFMultiPhases.Name = "cbolLFMultiPhases"
        Me.cbolLFMultiPhases.Size = New System.Drawing.Size(124, 21)
        Me.cbolLFMultiPhases.TabIndex = 68
        '
        'lblPhasesSpéciales
        '
        Me.lblPhasesSpéciales.Location = New System.Drawing.Point(144, 56)
        Me.lblPhasesSpéciales.Name = "lblPhasesSpéciales"
        Me.lblPhasesSpéciales.Size = New System.Drawing.Size(88, 32)
        Me.lblPhasesSpéciales.TabIndex = 67
        Me.lblPhasesSpéciales.Text = "Phasages avec phase spéciale"
        '
        'lblLFMultiPhases
        '
        Me.lblLFMultiPhases.Location = New System.Drawing.Point(8, 56)
        Me.lblLFMultiPhases.Name = "lblLFMultiPhases"
        Me.lblLFMultiPhases.Size = New System.Drawing.Size(80, 32)
        Me.lblLFMultiPhases.TabIndex = 66
        Me.lblLFMultiPhases.Text = "Lignes de feux sur 2 phases"
        '
        'lblRéserveCapacité
        '
        Me.lblRéserveCapacité.Location = New System.Drawing.Point(144, 0)
        Me.lblRéserveCapacité.Name = "lblRéserveCapacité"
        Me.lblRéserveCapacité.Size = New System.Drawing.Size(112, 24)
        Me.lblRéserveCapacité.TabIndex = 65
        Me.lblRéserveCapacité.Text = "Réserve de capacité"
        '
        'cboRéserveCapacité
        '
        Me.cboRéserveCapacité.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRéserveCapacité.Enabled = False
        Me.cboRéserveCapacité.Items.AddRange(New Object() {"<Indifférent>", "< 10%", "10 à 20%", ">=20%"})
        Me.cboRéserveCapacité.Location = New System.Drawing.Point(144, 24)
        Me.cboRéserveCapacité.Name = "cboRéserveCapacité"
        Me.cboRéserveCapacité.Size = New System.Drawing.Size(72, 21)
        Me.cboRéserveCapacité.TabIndex = 64
        '
        'chk3Phases
        '
        Me.chk3Phases.Enabled = False
        Me.chk3Phases.Location = New System.Drawing.Point(12, 20)
        Me.chk3Phases.Name = "chk3Phases"
        Me.chk3Phases.Size = New System.Drawing.Size(72, 32)
        Me.chk3Phases.TabIndex = 62
        Me.chk3Phases.Text = "Accepter 3 phases"
        '
        'btnActionPhase
        '
        Me.btnActionPhase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActionPhase.Location = New System.Drawing.Point(824, 32)
        Me.btnActionPhase.Name = "btnActionPhase"
        Me.btnActionPhase.Size = New System.Drawing.Size(72, 32)
        Me.btnActionPhase.TabIndex = 39
        Me.btnActionPhase.Tag = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActionPhase.Text = "Supprimer une phase"
        Me.btnActionPhase.Visible = False
        '
        'lblDécoupagePhases
        '
        Me.lblDécoupagePhases.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDécoupagePhases.Location = New System.Drawing.Point(640, 8)
        Me.lblDécoupagePhases.Name = "lblDécoupagePhases"
        Me.lblDécoupagePhases.Size = New System.Drawing.Size(152, 16)
        Me.lblDécoupagePhases.TabIndex = 37
        Me.lblDécoupagePhases.Text = "2 Phasages possibles"
        '
        'pnlFeuBase
        '
        Me.pnlFeuBase.AutoScroll = True
        Me.pnlFeuBase.AutoScrollMinSize = New System.Drawing.Size(320, 150)
        Me.pnlFeuBase.Controls.Add(Me.lblVéhiculeBase)
        Me.pnlFeuBase.Controls.Add(Me.lblPiétonBase)
        Me.pnlFeuBase.Controls.Add(Me.txtVertMiniPiéton)
        Me.pnlFeuBase.Controls.Add(Me.txtVertMiniVéhicule)
        Me.pnlFeuBase.Controls.Add(Me.lblVertMini)
        Me.pnlFeuBase.Controls.Add(Me.grpSynchroBase)
        Me.pnlFeuBase.Controls.Add(Me.grpPhasesBase)
        Me.pnlFeuBase.Controls.Add(Me.chkVerrouFeuBase)
        Me.pnlFeuBase.Controls.Add(Me.pnlBoutonsLignesFeuxPlans)
        Me.pnlFeuBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFeuBase.Location = New System.Drawing.Point(0, 0)
        Me.pnlFeuBase.Name = "pnlFeuBase"
        Me.pnlFeuBase.Size = New System.Drawing.Size(912, 597)
        Me.pnlFeuBase.TabIndex = 46
        '
        'lblVéhiculeBase
        '
        Me.lblVéhiculeBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVéhiculeBase.Location = New System.Drawing.Point(632, 0)
        Me.lblVéhiculeBase.Name = "lblVéhiculeBase"
        Me.lblVéhiculeBase.Size = New System.Drawing.Size(56, 12)
        Me.lblVéhiculeBase.TabIndex = 31
        Me.lblVéhiculeBase.Text = "Véhicules"
        '
        'lblPiétonBase
        '
        Me.lblPiétonBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPiétonBase.Location = New System.Drawing.Point(688, 0)
        Me.lblPiétonBase.Name = "lblPiétonBase"
        Me.lblPiétonBase.Size = New System.Drawing.Size(56, 12)
        Me.lblPiétonBase.TabIndex = 30
        Me.lblPiétonBase.Text = "Piétons"
        '
        'txtVertMiniPiéton
        '
        Me.txtVertMiniPiéton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVertMiniPiéton.Location = New System.Drawing.Point(696, 16)
        Me.txtVertMiniPiéton.Name = "txtVertMiniPiéton"
        Me.txtVertMiniPiéton.Size = New System.Drawing.Size(20, 20)
        Me.txtVertMiniPiéton.TabIndex = 29
        Me.txtVertMiniPiéton.Text = "10"
        Me.txtVertMiniPiéton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtVertMiniVéhicule
        '
        Me.txtVertMiniVéhicule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVertMiniVéhicule.Location = New System.Drawing.Point(648, 16)
        Me.txtVertMiniVéhicule.Name = "txtVertMiniVéhicule"
        Me.txtVertMiniVéhicule.Size = New System.Drawing.Size(20, 20)
        Me.txtVertMiniVéhicule.TabIndex = 28
        Me.txtVertMiniVéhicule.Text = "6"
        Me.txtVertMiniVéhicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblVertMini
        '
        Me.lblVertMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVertMini.Location = New System.Drawing.Point(576, 16)
        Me.lblVertMini.Name = "lblVertMini"
        Me.lblVertMini.Size = New System.Drawing.Size(80, 16)
        Me.lblVertMini.TabIndex = 27
        Me.lblVertMini.Text = "Vert minimum"
        '
        'grpSynchroBase
        '
        Me.grpSynchroBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSynchroBase.Controls.Add(Me.lblDécalages)
        Me.grpSynchroBase.Controls.Add(Me.updDécalageFermetureBase)
        Me.grpSynchroBase.Controls.Add(Me.updDécalageOuvertureBase)
        Me.grpSynchroBase.Controls.Add(Me.lvwDuréeVert)
        Me.grpSynchroBase.Location = New System.Drawing.Point(576, 176)
        Me.grpSynchroBase.Name = "grpSynchroBase"
        Me.grpSynchroBase.Size = New System.Drawing.Size(320, 240)
        Me.grpSynchroBase.TabIndex = 25
        Me.grpSynchroBase.TabStop = False
        Me.grpSynchroBase.Text = "Synchronisations"
        '
        'lblDécalages
        '
        Me.lblDécalages.Location = New System.Drawing.Point(208, 16)
        Me.lblDécalages.Name = "lblDécalages"
        Me.lblDécalages.Size = New System.Drawing.Size(64, 16)
        Me.lblDécalages.TabIndex = 28
        Me.lblDécalages.Text = "Décalages"
        '
        'updDécalageFermetureBase
        '
        Me.updDécalageFermetureBase.Location = New System.Drawing.Point(256, 32)
        Me.updDécalageFermetureBase.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updDécalageFermetureBase.Name = "updDécalageFermetureBase"
        Me.updDécalageFermetureBase.Size = New System.Drawing.Size(32, 20)
        Me.updDécalageFermetureBase.TabIndex = 11
        Me.updDécalageFermetureBase.Visible = False
        '
        'updDécalageOuvertureBase
        '
        Me.updDécalageOuvertureBase.Location = New System.Drawing.Point(192, 32)
        Me.updDécalageOuvertureBase.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updDécalageOuvertureBase.Name = "updDécalageOuvertureBase"
        Me.updDécalageOuvertureBase.Size = New System.Drawing.Size(32, 20)
        Me.updDécalageOuvertureBase.TabIndex = 4
        Me.updDécalageOuvertureBase.Visible = False
        '
        'lvwDuréeVert
        '
        Me.lvwDuréeVert.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLF, Me.lvwcolPhase, Me.lvwcolDurée, Me.lvwcolDécalOuverture, Me.lvwcolDécalFermeture})
        Me.lvwDuréeVert.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDuréeVert.FullRowSelect = True
        Me.lvwDuréeVert.HideSelection = False
        Me.lvwDuréeVert.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7})
        Me.lvwDuréeVert.Location = New System.Drawing.Point(4, 64)
        Me.lvwDuréeVert.MultiSelect = False
        Me.lvwDuréeVert.Name = "lvwDuréeVert"
        Me.lvwDuréeVert.Size = New System.Drawing.Size(312, 163)
        Me.lvwDuréeVert.TabIndex = 0
        Me.lvwDuréeVert.UseCompatibleStateImageBehavior = False
        Me.lvwDuréeVert.View = System.Windows.Forms.View.Details
        '
        'lvwcolLF
        '
        Me.lvwcolLF.Text = "Ligne"
        Me.lvwcolLF.Width = 45
        '
        'lvwcolPhase
        '
        Me.lvwcolPhase.Text = "Phase"
        Me.lvwcolPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolPhase.Width = 42
        '
        'lvwcolDurée
        '
        Me.lvwcolDurée.Text = "Durée vert"
        Me.lvwcolDurée.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDurée.Width = 71
        '
        'lvwcolDécalOuverture
        '
        Me.lvwcolDécalOuverture.Text = "Ouverture"
        Me.lvwcolDécalOuverture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvwcolDécalFermeture
        '
        Me.lvwcolDécalFermeture.Text = "Fermeture"
        Me.lvwcolDécalFermeture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDécalFermeture.Width = 72
        '
        'grpPhasesBase
        '
        Me.grpPhasesBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPhasesBase.Controls.Add(Me.radPhase3Base)
        Me.grpPhasesBase.Controls.Add(Me.radPhase2Base)
        Me.grpPhasesBase.Controls.Add(Me.radPhase1Base)
        Me.grpPhasesBase.Controls.Add(Me.lbFigerDuréeBase)
        Me.grpPhasesBase.Controls.Add(Me.txtDuréeCycleBase)
        Me.grpPhasesBase.Controls.Add(Me.lblCycle)
        Me.grpPhasesBase.Controls.Add(Me.lblPhase3Base)
        Me.grpPhasesBase.Controls.Add(Me.lblPhase2Base)
        Me.grpPhasesBase.Controls.Add(Me.lblPhase1Base)
        Me.grpPhasesBase.Controls.Add(Me.updPhase3Base)
        Me.grpPhasesBase.Controls.Add(Me.updPhase2Base)
        Me.grpPhasesBase.Controls.Add(Me.updPhase1Base)
        Me.grpPhasesBase.Location = New System.Drawing.Point(576, 48)
        Me.grpPhasesBase.Name = "grpPhasesBase"
        Me.grpPhasesBase.Size = New System.Drawing.Size(320, 120)
        Me.grpPhasesBase.TabIndex = 24
        Me.grpPhasesBase.TabStop = False
        Me.grpPhasesBase.Text = "Durées des phases"
        '
        'radPhase3Base
        '
        Me.radPhase3Base.Location = New System.Drawing.Point(256, 80)
        Me.radPhase3Base.Name = "radPhase3Base"
        Me.radPhase3Base.Size = New System.Drawing.Size(16, 24)
        Me.radPhase3Base.TabIndex = 39
        '
        'radPhase2Base
        '
        Me.radPhase2Base.Location = New System.Drawing.Point(192, 80)
        Me.radPhase2Base.Name = "radPhase2Base"
        Me.radPhase2Base.Size = New System.Drawing.Size(16, 24)
        Me.radPhase2Base.TabIndex = 38
        '
        'radPhase1Base
        '
        Me.radPhase1Base.Location = New System.Drawing.Point(128, 80)
        Me.radPhase1Base.Name = "radPhase1Base"
        Me.radPhase1Base.Size = New System.Drawing.Size(16, 24)
        Me.radPhase1Base.TabIndex = 37
        '
        'lbFigerDuréeBase
        '
        Me.lbFigerDuréeBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbFigerDuréeBase.Location = New System.Drawing.Point(32, 86)
        Me.lbFigerDuréeBase.Name = "lbFigerDuréeBase"
        Me.lbFigerDuréeBase.Size = New System.Drawing.Size(80, 28)
        Me.lbFigerDuréeBase.TabIndex = 33
        Me.lbFigerDuréeBase.Text = "Figer la durée de la phase"
        '
        'txtDuréeCycleBase
        '
        Me.txtDuréeCycleBase.BackColor = System.Drawing.SystemColors.Window
        Me.txtDuréeCycleBase.Location = New System.Drawing.Point(32, 56)
        Me.txtDuréeCycleBase.Name = "txtDuréeCycleBase"
        Me.txtDuréeCycleBase.Size = New System.Drawing.Size(24, 20)
        Me.txtDuréeCycleBase.TabIndex = 29
        Me.txtDuréeCycleBase.Text = "60"
        Me.txtDuréeCycleBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCycle
        '
        Me.lblCycle.Location = New System.Drawing.Point(32, 24)
        Me.lblCycle.Name = "lblCycle"
        Me.lblCycle.Size = New System.Drawing.Size(48, 24)
        Me.lblCycle.TabIndex = 28
        Me.lblCycle.Text = "Cycle"
        '
        'lblPhase3Base
        '
        Me.lblPhase3Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase3Base.Location = New System.Drawing.Point(248, 24)
        Me.lblPhase3Base.Name = "lblPhase3Base"
        Me.lblPhase3Base.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase3Base.TabIndex = 27
        Me.lblPhase3Base.Text = "Phase 3"
        '
        'lblPhase2Base
        '
        Me.lblPhase2Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase2Base.Location = New System.Drawing.Point(184, 24)
        Me.lblPhase2Base.Name = "lblPhase2Base"
        Me.lblPhase2Base.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase2Base.TabIndex = 26
        Me.lblPhase2Base.Text = "Phase 2"
        '
        'lblPhase1Base
        '
        Me.lblPhase1Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhase1Base.Location = New System.Drawing.Point(120, 24)
        Me.lblPhase1Base.Name = "lblPhase1Base"
        Me.lblPhase1Base.Size = New System.Drawing.Size(48, 24)
        Me.lblPhase1Base.TabIndex = 25
        Me.lblPhase1Base.Text = "Phase 1"
        '
        'updPhase3Base
        '
        Me.updPhase3Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase3Base.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase3Base.Location = New System.Drawing.Point(248, 56)
        Me.updPhase3Base.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase3Base.Name = "updPhase3Base"
        Me.updPhase3Base.ReadOnly = True
        Me.updPhase3Base.Size = New System.Drawing.Size(40, 20)
        Me.updPhase3Base.TabIndex = 23
        Me.updPhase3Base.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'updPhase2Base
        '
        Me.updPhase2Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase2Base.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase2Base.Location = New System.Drawing.Point(184, 56)
        Me.updPhase2Base.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase2Base.Name = "updPhase2Base"
        Me.updPhase2Base.ReadOnly = True
        Me.updPhase2Base.Size = New System.Drawing.Size(40, 20)
        Me.updPhase2Base.TabIndex = 22
        Me.updPhase2Base.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'updPhase1Base
        '
        Me.updPhase1Base.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updPhase1Base.BackColor = System.Drawing.SystemColors.Window
        Me.updPhase1Base.Location = New System.Drawing.Point(120, 56)
        Me.updPhase1Base.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.updPhase1Base.Name = "updPhase1Base"
        Me.updPhase1Base.ReadOnly = True
        Me.updPhase1Base.Size = New System.Drawing.Size(40, 20)
        Me.updPhase1Base.TabIndex = 21
        Me.updPhase1Base.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkVerrouFeuBase
        '
        Me.chkVerrouFeuBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouFeuBase.Location = New System.Drawing.Point(592, 468)
        Me.chkVerrouFeuBase.Name = "chkVerrouFeuBase"
        Me.chkVerrouFeuBase.Size = New System.Drawing.Size(152, 24)
        Me.chkVerrouFeuBase.TabIndex = 26
        Me.chkVerrouFeuBase.Tag = "4"
        Me.chkVerrouFeuBase.Text = "Verrouiller le plan de feux"
        '
        'pnlBoutonsLignesFeuxPlans
        '
        Me.pnlBoutonsLignesFeuxPlans.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBoutonsLignesFeuxPlans.Controls.Add(Me.cboTriLignesFeuxPlans)
        Me.pnlBoutonsLignesFeuxPlans.Controls.Add(Me.lblTriLignesFeuxPlans)
        Me.pnlBoutonsLignesFeuxPlans.Controls.Add(Me.btnLigneFeuDescendrePlans)
        Me.pnlBoutonsLignesFeuxPlans.Controls.Add(Me.btnLigneFeuMonterPlans)
        Me.pnlBoutonsLignesFeuxPlans.Location = New System.Drawing.Point(576, 416)
        Me.pnlBoutonsLignesFeuxPlans.Name = "pnlBoutonsLignesFeuxPlans"
        Me.pnlBoutonsLignesFeuxPlans.Size = New System.Drawing.Size(300, 40)
        Me.pnlBoutonsLignesFeuxPlans.TabIndex = 48
        '
        'cboTriLignesFeuxPlans
        '
        Me.cboTriLignesFeuxPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriLignesFeuxPlans.Items.AddRange(New Object() {"Manuel", "Feux Véhicules en tête", "Par Branche", "Par nom de feux", "Par phase"})
        Me.cboTriLignesFeuxPlans.Location = New System.Drawing.Point(100, 8)
        Me.cboTriLignesFeuxPlans.Name = "cboTriLignesFeuxPlans"
        Me.cboTriLignesFeuxPlans.Size = New System.Drawing.Size(120, 21)
        Me.cboTriLignesFeuxPlans.TabIndex = 55
        '
        'lblTriLignesFeuxPlans
        '
        Me.lblTriLignesFeuxPlans.Location = New System.Drawing.Point(8, 4)
        Me.lblTriLignesFeuxPlans.Name = "lblTriLignesFeuxPlans"
        Me.lblTriLignesFeuxPlans.Size = New System.Drawing.Size(81, 28)
        Me.lblTriLignesFeuxPlans.TabIndex = 48
        Me.lblTriLignesFeuxPlans.Text = "Ordonner les lignes de feux"
        '
        'btnLigneFeuDescendrePlans
        '
        Me.btnLigneFeuDescendrePlans.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLigneFeuDescendrePlans.Image = CType(resources.GetObject("btnLigneFeuDescendrePlans.Image"), System.Drawing.Image)
        Me.btnLigneFeuDescendrePlans.Location = New System.Drawing.Point(272, 8)
        Me.btnLigneFeuDescendrePlans.Name = "btnLigneFeuDescendrePlans"
        Me.btnLigneFeuDescendrePlans.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeuDescendrePlans.TabIndex = 47
        Me.tipBulle.SetToolTip(Me.btnLigneFeuDescendrePlans, "Descendre la  ligne de feux")
        '
        'btnLigneFeuMonterPlans
        '
        Me.btnLigneFeuMonterPlans.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLigneFeuMonterPlans.Image = CType(resources.GetObject("btnLigneFeuMonterPlans.Image"), System.Drawing.Image)
        Me.btnLigneFeuMonterPlans.Location = New System.Drawing.Point(232, 8)
        Me.btnLigneFeuMonterPlans.Name = "btnLigneFeuMonterPlans"
        Me.btnLigneFeuMonterPlans.Size = New System.Drawing.Size(24, 24)
        Me.btnLigneFeuMonterPlans.TabIndex = 45
        Me.tipBulle.SetToolTip(Me.btnLigneFeuMonterPlans, "Monter la ligne de feux")
        '
        'picDessin
        '
        Me.picDessin.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.picDessin.Dock = System.Windows.Forms.DockStyle.Left
        Me.picDessin.Location = New System.Drawing.Point(0, 27)
        Me.picDessin.Name = "picDessin"
        Me.picDessin.Size = New System.Drawing.Size(288, 570)
        Me.picDessin.TabIndex = 5
        Me.picDessin.TabStop = False
        '
        'pnlConflits
        '
        Me.pnlConflits.AutoScroll = True
        Me.pnlConflits.AutoScrollMinSize = New System.Drawing.Size(500, 150)
        Me.pnlConflits.Controls.Add(Me.pnlBoutonsRouges)
        Me.pnlConflits.Controls.Add(Me.Label3)
        Me.pnlConflits.Controls.Add(Me.pnlAntagonismes)
        Me.pnlConflits.Controls.Add(Me.pnlMatricesSécurité)
        Me.pnlConflits.Controls.Add(Me.pnlVerrouMatrice)
        Me.pnlConflits.Controls.Add(Me.Ac1GrilleSécurité)
        Me.pnlConflits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlConflits.Location = New System.Drawing.Point(0, 0)
        Me.pnlConflits.Name = "pnlConflits"
        Me.pnlConflits.Size = New System.Drawing.Size(912, 597)
        Me.pnlConflits.TabIndex = 10
        '
        'pnlBoutonsRouges
        '
        Me.pnlBoutonsRouges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBoutonsRouges.Controls.Add(Me.lblBoutonsRouges)
        Me.pnlBoutonsRouges.Controls.Add(Me.btnRougeDéfaut)
        Me.pnlBoutonsRouges.Controls.Add(Me.btnRougesDéfaut)
        Me.pnlBoutonsRouges.Location = New System.Drawing.Point(688, 256)
        Me.pnlBoutonsRouges.Name = "pnlBoutonsRouges"
        Me.pnlBoutonsRouges.Size = New System.Drawing.Size(192, 72)
        Me.pnlBoutonsRouges.TabIndex = 37
        '
        'lblBoutonsRouges
        '
        Me.lblBoutonsRouges.Location = New System.Drawing.Point(8, 8)
        Me.lblBoutonsRouges.Name = "lblBoutonsRouges"
        Me.lblBoutonsRouges.Size = New System.Drawing.Size(176, 16)
        Me.lblBoutonsRouges.TabIndex = 2
        Me.lblBoutonsRouges.Text = "Reprendre les valeurs par défaut"
        '
        'btnRougeDéfaut
        '
        Me.btnRougeDéfaut.Location = New System.Drawing.Point(100, 32)
        Me.btnRougeDéfaut.Name = "btnRougeDéfaut"
        Me.btnRougeDéfaut.Size = New System.Drawing.Size(72, 32)
        Me.btnRougeDéfaut.TabIndex = 1
        Me.btnRougeDéfaut.Text = "Le rouge sélectionné"
        '
        'btnRougesDéfaut
        '
        Me.btnRougesDéfaut.Enabled = False
        Me.btnRougesDéfaut.Location = New System.Drawing.Point(16, 32)
        Me.btnRougesDéfaut.Name = "btnRougesDéfaut"
        Me.btnRougesDéfaut.Size = New System.Drawing.Size(72, 32)
        Me.btnRougesDéfaut.TabIndex = 0
        Me.btnRougesDéfaut.Text = "Toute la Matrice"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(664, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 24)
        Me.Label3.TabIndex = 0
        '
        'pnlAntagonismes
        '
        Me.pnlAntagonismes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlAntagonismes.AutoScroll = True
        Me.pnlAntagonismes.Controls.Add(Me.btnRéinitAntago)
        Me.pnlAntagonismes.Controls.Add(Me.cboBrancheCourant1)
        Me.pnlAntagonismes.Controls.Add(Me.lblCourantOrigine)
        Me.pnlAntagonismes.Controls.Add(Me.AC1GrilleAntagonismes)
        Me.pnlAntagonismes.Location = New System.Drawing.Point(664, 328)
        Me.pnlAntagonismes.Name = "pnlAntagonismes"
        Me.pnlAntagonismes.Size = New System.Drawing.Size(242, 232)
        Me.pnlAntagonismes.TabIndex = 36
        '
        'btnRéinitAntago
        '
        Me.btnRéinitAntago.Location = New System.Drawing.Point(144, 4)
        Me.btnRéinitAntago.Name = "btnRéinitAntago"
        Me.btnRéinitAntago.Size = New System.Drawing.Size(72, 24)
        Me.btnRéinitAntago.TabIndex = 38
        Me.btnRéinitAntago.Text = "Réinitialiser"
        '
        'cboBrancheCourant1
        '
        Me.cboBrancheCourant1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBrancheCourant1.Location = New System.Drawing.Point(80, 8)
        Me.cboBrancheCourant1.Name = "cboBrancheCourant1"
        Me.cboBrancheCourant1.Size = New System.Drawing.Size(50, 21)
        Me.cboBrancheCourant1.TabIndex = 37
        '
        'lblCourantOrigine
        '
        Me.lblCourantOrigine.Location = New System.Drawing.Point(8, 0)
        Me.lblCourantOrigine.Name = "lblCourantOrigine"
        Me.lblCourantOrigine.Size = New System.Drawing.Size(76, 32)
        Me.lblCourantOrigine.TabIndex = 36
        Me.lblCourantOrigine.Text = "Courant1 issu de branche :"
        '
        'AC1GrilleAntagonismes
        '
        Me.AC1GrilleAntagonismes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AC1GrilleAntagonismes.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleAntagonismes.ColumnInfo = resources.GetString("AC1GrilleAntagonismes.ColumnInfo")
        Me.AC1GrilleAntagonismes.Location = New System.Drawing.Point(0, 32)
        Me.AC1GrilleAntagonismes.Name = "AC1GrilleAntagonismes"
        Me.AC1GrilleAntagonismes.Rows.Count = 2
        Me.AC1GrilleAntagonismes.Size = New System.Drawing.Size(244, 192)
        Me.AC1GrilleAntagonismes.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleAntagonismes.Styles"))
        Me.AC1GrilleAntagonismes.TabIndex = 32
        '
        'pnlMatricesSécurité
        '
        Me.pnlMatricesSécurité.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMatricesSécurité.Controls.Add(Me.radMatriceInterverts)
        Me.pnlMatricesSécurité.Controls.Add(Me.radMatriceRougesDégagement)
        Me.pnlMatricesSécurité.Controls.Add(Me.radMatriceConflits)
        Me.pnlMatricesSécurité.Location = New System.Drawing.Point(752, 0)
        Me.pnlMatricesSécurité.Name = "pnlMatricesSécurité"
        Me.pnlMatricesSécurité.Size = New System.Drawing.Size(160, 64)
        Me.pnlMatricesSécurité.TabIndex = 33
        '
        'radMatriceInterverts
        '
        Me.radMatriceInterverts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radMatriceInterverts.Location = New System.Drawing.Point(8, 40)
        Me.radMatriceInterverts.Name = "radMatriceInterverts"
        Me.radMatriceInterverts.Size = New System.Drawing.Size(72, 16)
        Me.radMatriceInterverts.TabIndex = 38
        Me.radMatriceInterverts.Text = "Interverts"
        '
        'radMatriceRougesDégagement
        '
        Me.radMatriceRougesDégagement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radMatriceRougesDégagement.Location = New System.Drawing.Point(8, 24)
        Me.radMatriceRougesDégagement.Name = "radMatriceRougesDégagement"
        Me.radMatriceRougesDégagement.Size = New System.Drawing.Size(144, 16)
        Me.radMatriceRougesDégagement.TabIndex = 37
        Me.radMatriceRougesDégagement.Text = "Rouges de dégagement"
        '
        'radMatriceConflits
        '
        Me.radMatriceConflits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radMatriceConflits.Location = New System.Drawing.Point(8, 8)
        Me.radMatriceConflits.Name = "radMatriceConflits"
        Me.radMatriceConflits.Size = New System.Drawing.Size(120, 16)
        Me.radMatriceConflits.TabIndex = 36
        Me.radMatriceConflits.Text = "Matrice des conflits"
        '
        'pnlVerrouMatrice
        '
        Me.pnlVerrouMatrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlVerrouMatrice.AutoScroll = True
        Me.pnlVerrouMatrice.Controls.Add(Me.lbImgSansConflit)
        Me.pnlVerrouMatrice.Controls.Add(Me.lblImgConflit)
        Me.pnlVerrouMatrice.Controls.Add(Me.pnlImgSansConflit)
        Me.pnlVerrouMatrice.Controls.Add(Me.pnlImgConflit)
        Me.pnlVerrouMatrice.Controls.Add(Me.chkVerrouMatrice)
        Me.pnlVerrouMatrice.Location = New System.Drawing.Point(688, 256)
        Me.pnlVerrouMatrice.Name = "pnlVerrouMatrice"
        Me.pnlVerrouMatrice.Size = New System.Drawing.Size(208, 72)
        Me.pnlVerrouMatrice.TabIndex = 31
        '
        'lbImgSansConflit
        '
        Me.lbImgSansConflit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbImgSansConflit.Location = New System.Drawing.Point(128, 12)
        Me.lbImgSansConflit.Name = "lbImgSansConflit"
        Me.lbImgSansConflit.Size = New System.Drawing.Size(72, 16)
        Me.lbImgSansConflit.TabIndex = 38
        Me.lbImgSansConflit.Text = "Pas de conflit"
        '
        'lblImgConflit
        '
        Me.lblImgConflit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblImgConflit.Location = New System.Drawing.Point(48, 12)
        Me.lblImgConflit.Name = "lblImgConflit"
        Me.lblImgConflit.Size = New System.Drawing.Size(40, 16)
        Me.lblImgConflit.TabIndex = 37
        Me.lblImgConflit.Text = "Conflit"
        '
        'pnlImgSansConflit
        '
        Me.pnlImgSansConflit.BackColor = System.Drawing.Color.SpringGreen
        Me.pnlImgSansConflit.Location = New System.Drawing.Point(104, 12)
        Me.pnlImgSansConflit.Name = "pnlImgSansConflit"
        Me.pnlImgSansConflit.Size = New System.Drawing.Size(24, 16)
        Me.pnlImgSansConflit.TabIndex = 36
        '
        'pnlImgConflit
        '
        Me.pnlImgConflit.BackColor = System.Drawing.Color.Red
        Me.pnlImgConflit.Location = New System.Drawing.Point(24, 12)
        Me.pnlImgConflit.Name = "pnlImgConflit"
        Me.pnlImgConflit.Size = New System.Drawing.Size(24, 16)
        Me.pnlImgConflit.TabIndex = 35
        '
        'chkVerrouMatrice
        '
        Me.chkVerrouMatrice.Location = New System.Drawing.Point(24, 40)
        Me.chkVerrouMatrice.Name = "chkVerrouMatrice"
        Me.chkVerrouMatrice.Size = New System.Drawing.Size(136, 24)
        Me.chkVerrouMatrice.TabIndex = 34
        Me.chkVerrouMatrice.Tag = "3"
        Me.chkVerrouMatrice.Text = "Verrouiller la matrice"
        '
        'Ac1GrilleSécurité
        '
        Me.Ac1GrilleSécurité.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.Ac1GrilleSécurité.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Ac1GrilleSécurité.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Ac1GrilleSécurité.BackColor = System.Drawing.SystemColors.Window
        Me.Ac1GrilleSécurité.ColumnInfo = "4,1,0,0,0,20,Columns:0{Width:20;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:20;DataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Width:20;D" & _
            "ataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Width:20;DataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Ac1GrilleSécurité.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.Ac1GrilleSécurité.Location = New System.Drawing.Point(661, 72)
        Me.Ac1GrilleSécurité.Name = "Ac1GrilleSécurité"
        Me.Ac1GrilleSécurité.Rows.Count = 2
        Me.Ac1GrilleSécurité.Size = New System.Drawing.Size(344, 176)
        Me.Ac1GrilleSécurité.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Ac1GrilleSécurité.Styles"))
        Me.Ac1GrilleSécurité.TabIndex = 24
        '
        'tipPicDessin
        '
        Me.tipPicDessin.AutoPopDelay = 50000
        Me.tipPicDessin.InitialDelay = 0
        Me.tipPicDessin.ReshowDelay = 100
        '
        'frmCarrefour
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(912, 597)
        Me.Controls.Add(Me.splitGraphiqueDonnées)
        Me.Controls.Add(Me.picDessin)
        Me.Controls.Add(Me.splitOngletsPrincipal)
        Me.Controls.Add(Me.tabOnglet)
        Me.Controls.Add(Me.pnlLignesDeFeux)
        Me.Controls.Add(Me.pnlPlansDeFeux)
        Me.Controls.Add(Me.pnlTrafics)
        Me.Controls.Add(Me.pnlConflits)
        Me.Controls.Add(Me.pnlGéométrie)
        Me.Name = "frmCarrefour"
        Me.tabOnglet.ResumeLayout(False)
        Me.pnlGéométrie.ResumeLayout(False)
        Me.pnlIlots.ResumeLayout(False)
        CType(Me.AC1GrilleIlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBtnGéométrie.ResumeLayout(False)
        CType(Me.AC1GrilleBranches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLignesDeFeux.ResumeLayout(False)
        Me.pnlTrajectoires.ResumeLayout(False)
        CType(Me.AC1GrilleFeux, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBoutonsLignesFeux.ResumeLayout(False)
        Me.pnlTrafics.ResumeLayout(False)
        Me.pnlTrafics.PerformLayout()
        Me.grpPiéton.ResumeLayout(False)
        CType(Me.Ac1GrilleTraficPiétons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVéhicule.ResumeLayout(False)
        CType(Me.AC1GrilleTraficVéhicules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTrafic.ResumeLayout(False)
        Me.pnlPlansDeFeux.ResumeLayout(False)
        Me.pnlCarrefourComposé.ResumeLayout(False)
        Me.pnlFeuFonctionnement.ResumeLayout(False)
        Me.grpPhasesFct.ResumeLayout(False)
        Me.grpPhasesFct.PerformLayout()
        CType(Me.updPhase3Fct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase2Fct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase1Fct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSynchroFct.ResumeLayout(False)
        CType(Me.updDécalageFermetureFct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updDécalageOuvertureFct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPhasage.ResumeLayout(False)
        Me.pnlTableauPhasage.ResumeLayout(False)
        CType(Me.AC1GrillePhases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiltrePhasage.ResumeLayout(False)
        Me.pnlFiltrePhasage.PerformLayout()
        Me.pnlFeuBase.ResumeLayout(False)
        Me.pnlFeuBase.PerformLayout()
        Me.grpSynchroBase.ResumeLayout(False)
        CType(Me.updDécalageFermetureBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updDécalageOuvertureBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPhasesBase.ResumeLayout(False)
        Me.grpPhasesBase.PerformLayout()
        CType(Me.updPhase3Base, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase2Base, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase1Base, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBoutonsLignesFeuxPlans.ResumeLayout(False)
        CType(Me.picDessin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlConflits.ResumeLayout(False)
        Me.pnlBoutonsRouges.ResumeLayout(False)
        Me.pnlAntagonismes.ResumeLayout(False)
        CType(Me.AC1GrilleAntagonismes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMatricesSécurité.ResumeLayout(False)
        Me.pnlVerrouMatrice.ResumeLayout(False)
        CType(Me.Ac1GrilleSécurité, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
  '  Me.pnlPlansDeFeux.Controls.Add(Me.pnlCarrefourComposé)

#Region " Déclarations"
  Private BufPicDessin As System.Windows.Forms.PictureBox

  Public Enum CommandeGraphique
    EnCours = -1
    AucuneCommande
    DéplacerCarrefour
    OrigineBranche
    AngleBranche
    EtirerIlot
    DéplacerIlot
    ElargirIlot
    PassagePiéton
    PassagePiétonRapide
    DéplacerPassage
    SupprimerPassage
    EditLargeurPassage
    EditLongueurPassage
    EditAnglePassage
    EditPointPassage
    Trajectoire
    ToutesTrajectoires
    PropTrajectoire
    SupprimerTrajectoire
    EditerTrajectoire
    EditerPointTrajectoire
    EditerOrigineTrajectoire
    EditerDestinationTrajectoire
    Traversée
    PropTraversée
    DécomposerTraversée
    PositionTrafic
    Antagonisme
    LigneFeux
    DéplacerLigneFeu
    AllongerFeu
    SupprimerLigneFeu
    DéplacerSignal
    Zoom
    ZoomMoins
    ZoomPrécédent
    ZoomPAN
    Mesure
    DéplacerNord
    OrienterNord
    DéplacerEchelle
  End Enum
  Private Enum MéthodeCalculCycle
    Manuel
    Webster
    Classique
  End Enum

  Private ChargementEnCours As Boolean = True
  Private FermetureEnCours As Boolean
  Private DessinEnCours As Boolean
  Private ChangementDeScénario As Boolean = True

  Private DécalageFeuxEnCours As Boolean
  Private AffichagePhasesEnCours As Boolean
  Private PhasageAffiché As Boolean

  Private AntagonismesEnCours As Boolean

  Private UneCommandeGraphique As CommandeGraphique
  Private FenetreAideCommande As New frmAideCommande
  Private FenetreAntagonisme As New frmAntagonisme
  Private FenetreDiagnostic As New frmDiagnostic

  Private maVariante As Variante
  Private mesBranches As BrancheCollection
  Private mesTrajectoires As TrajectoireCollection
  Private mesAntagonismes As AntagonismeCollection
  Private mesLignesFeux As LigneFeuxCollection
  Private mesPlansFeuxBase As PlanFeuxCollection
  Private mesTrafics As TraficCollection
  Private monPlanFeuxFonctionnement As PlanFeuxFonctionnement
  Private monPlanFeuxActif As PlanFeux

  Private monTraficPrécédent As Trafic

  Private IndexPhasages() As Short

  'Création d'une collection de plans de feux de base utiles à l'organisation du phasage
  'Private mesPlansPourPhasage As PlanFeuxCollection
  Private monPlanPourPhasage As PlanFeuxPhasage

  Private ModeGraphique As Boolean
  'Fond de plan 
  Private monFDP As FondDePlan
  Public colCalques As CalqueCollection
  Public GraphFDP As SuperBloc

  ' Vrai dès qu'on commence le 'glisser'
  Private mDragging As Boolean

  Private NePasEffacer As Boolean
  Private EnAttenteMouseUp As Boolean

  'A remplacer par un vrai objet (evt Nothing)
  Private objSélect As Graphique
  Private savObjSélect As Graphique

  Private SelectObject As Boolean 'Indique qu'un objet est en cours de sélection (inhiber les sélections d'objet par les grilles)

  ' Collection des objets graphiques représentant les objets métiers du projet : Objets à dessiner
  Private colObjetsGraphiques As New Graphiques
  Private PointCliqué As Point

  Private PourFrame As Boolean = False
  Private UnCarré As Boolean = False

  ' position de la souris.	(en Coordonnées du picturebox)
  Private mPoint() As Point
  Private mPoint1 As Point

  'Ou		(en coordonnées écran)
  Private mScreen1 As Point
  Private mScreen2 As Point
  Private mScreen() As Point

  Private mEchelles As New Hashtable

  'Infos pour vérifier que le point est dans un contour
  Private ContourPermis As PolyArc

  'Infos pour la translation
  Private DecalV(), DecalV1 As Vecteur
  Private Segment1, Segment2, Segment3, SegmentLimite As Ligne
  Private AngleBranche, LargeurBranche As Single
  Private EnveloppeBranche As PolyArc

  'Infos pour la rotation
  Private CentreRotation As Point
  Private LongueurSegment As Single
  Private AngleMini, BalayageMaxi As Single
  'Infos pour la projection
  Private AngleProjection As Single

  'Infos pour le passage piéton
  Private BrancheLiée As Branche
  Private BordChausséePassage As Branche.Latéralité
  Private AngleParallèle As Single    'Angle des 2 cotés parallèles
  Private PoignéeCliquée As Short  ' Indique  quel coté est en cours de modif
  Private SigneConservé As Short

  'Infos pour le passage piéton et les lignes de feux
  Private VoieTraj As Voie
  Private VoieOrigine As Voie

  'Infos pour les lignes de feux
  Private LigneFeuEnCours As LigneFeuVéhicules
  Private SignalFeuEnCours As SignalFeu

  'Infos pour les traversées piétonnes
  Private Traversée As TraverséePiétonne

  Private GénérationTrajectoires As Boolean

  ' Buffer graphique associé au PictureBox (pour Paint)
  Private mBufferGraphics As Graphics
  ' BitMap associée à ce Buffer
  Private mBitmap As Bitmap

  Private mBufferGraphicsA As Graphics
  Private mBitmapA As Bitmap

  'Base : position initiale de splitGraphiqueDonnées pour chaque panel (on part d'1 ClientSize de 900)
  Private lgPanel(6) As Short
  Private pnlPalette As Panel
  Private pnlPlanFeu As Panel
  Private FonteGras As Font

  Private mAideTopic As [Global].AideEnum

  Private StyleGrisé, StyleDégrisé, StyleGriséGras, StyleDégriséGras, StyleGriséBooléen, StyleGriséRouge, StyleRouge, StyleVert, StyleOrangé, StyleSaisie, StyleSaisieItalique As Grille.CellStyle
  Private strSauveGrille As String
#End Region
#Region " Affichage des panels"
  Private Sub DéfinirSplitPosition()
        'splitGraphiqueDonnées.SplitPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
        If Not ChargementEnCours Then
            splitGraphiqueDonnées.SplitPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
        End If
  End Sub

  Private Sub DéfinirDéfautLargeurPanels()
    Dim lgDéfautPanel() As Short = {450, 500, 400, 280, 280, 340, 380}
    Dim i As Short

    For i = 0 To lgPanel.Length - 1
      DéfinirDéfautLargeurPanel(i, lgDéfautPanel(i))
    Next

  End Sub

  Private Function DéfinirDéfautLargeurPanel(ByVal num As OngletEnum, Optional ByVal ValeurDéfaut As Short = 0) As Short

    If ValeurDéfaut = 0 Then
      ValeurDéfaut = lgPanel(num)
    Else
      lgPanel(num) = ValeurDéfaut
    End If

    'If num = Global.OngletEnum.Conflits Then
    '  If Me.Ac1GrilleSécurité.Rows.Count > 4 Then
    '    'La grille des conflits a été initialisée
    '    lgPanel(OngletEnum.Conflits) = Math.Max(Me.Ac1GrilleSécurité.Width + 2 * LGMARGE, ValeurDéfaut)
    '  End If
    'End If

  End Function

  Private Function numPanel() As OngletEnum
    If pnlPalette Is Me.pnlGéométrie Then
      numPanel = OngletEnum.Géométrie
    ElseIf pnlPalette Is Me.pnlLignesDeFeux Then
      numPanel = OngletEnum.LignesDeFeux
    ElseIf pnlPalette Is Me.pnlTrafics Then
      numPanel = OngletEnum.Trafics
    ElseIf pnlPalette Is Me.pnlConflits Then
      numPanel = OngletEnum.Conflits
    ElseIf pnlPalette Is Me.pnlPlansDeFeux Then
      numPanel = OngletEnum.PlansDeFeux + pnlPlansFeuxIndex
    End If
  End Function

  Private Sub pnlGéométrie_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlGéométrie.Resize, pnlLignesDeFeux.Resize, pnlConflits.Resize

    If Not IsNothing(pnlPalette) Then
      If pnlPalette Is Me.pnlConflits Then
        Me.FenetreAntagonisme.Location = Me.pnlPalette.PointToScreen(New Point(-50, 100))
      Else
        Me.FenetreAideCommande.Location = Me.pnlPalette.PointToScreen(New Point(-50, 300))
      End If
    End If

  End Sub

  '******************************************************************************
  ' Déplacement du Splitter vertical entre le graphique et pnlPalette
  '******************************************************************************
  Private Sub splitGraphiqueDonnées_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) _
    Handles splitGraphiqueDonnées.SplitterMoved
    'Mémoriser la nouvelle largeur de la palette, afin qu"elle soit conservée par Form_Resize
    lgPanel(numPanel) = Me.ClientSize.Width - 8 - Me.splitGraphiqueDonnées.SplitPosition
    'Le plan de feux a pu être tronqué
    If numPanel() > [Global].OngletEnum.PlansDeFeux Then RedessinerDiagrammePlanFeux()
  End Sub
#End Region
#Region " Fonctions de la Feuille"
  '******************************************************************************
  ' Chargement de la feuille
  '******************************************************************************
  Private Sub frmCarrefour_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim NomFichierImage As String = "Géomf.jpg"

    picDessin.ContextMenu = New ContextMenu

    ReDim mPoint(-1)
    ReDim mScreen(-1)

    Me.KeyPreview = True
    InstancierBufferDessin()

    maVariante = cndVariante

    With maVariante
      If Not IsNothing(.mFondDePlan) Then
        monFDP = .mFondDePlan
      End If
      mParamDessin = cndParamDessin
      mEchelles.Add(mEchelles.Count.ToString, mParamDessin)

      If IsNothing(.NomFichier) Then
        .Dimensionner()
      ElseIf Not mParamDessin.TailleFenêtre.IsEmpty Then
        Me.Size = mParamDessin.TailleFenêtre
      End If

      Text = .Libellé
      ModeGraphique = .ModeGraphique
      mesBranches = .mBranches
      mesTrajectoires = .mTrajectoires
      mesAntagonismes = .mTrajectoires.Antagonismes
      mesLignesFeux = .mLignesFeux
      mesPlansFeuxBase = .mPlansFeuxBase
      mesTrafics = .mTrafics

    End With

    Try

      cndGraphique = picDessin.CreateGraphics

      With maVariante
        If ModeGraphique Then
          RecréerDessinAntagonismes()

        Else
          Me.pnlLignesDeFeux.Controls.Remove(Me.pnlTrajectoires)
          Me.chkVerrouGéométrie.Top = 1
          Me.chkVerrouLignesFeux.Top -= 100
          Me.pnlBtnGéométrie.Controls.Remove(Me.chkVerrouGéométrie)
        End If

        .CréerGraphique(colObjetsGraphiques)
        If IsNothing(.NomFichier) Then
          'Par défaut : pas de Nord ni d'échelle pour un nouveau fichier
          'Les instructions qui suivent ne peuvent être appelées qu'après CréerGraphique
          .NordAffiché = False
          .EchelleAffichée = False
          .SensCirculation = Not ModeGraphique
        End If
      End With


      cndContexte = [Global].OngletEnum.Géométrie
      InitGéométrie()
      If mesTrafics.Count > 0 Then
        InitTrafics()
      End If
      InitLignesFeux()

      InitVerrouillages()
      ChoisirOngletInitial()

      'Fenêtres outils
      Me.FenetreAideCommande.Owner = Me
      Me.FenetreAntagonisme.Owner = Me
      Me.FenetreDiagnostic.Owner = Me
      Me.FenetreDiagnostic.Text = "Diagnostic " & Me.Text

            ChargementEnCours = False
            'DéfinirSplitPosition()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Chargement de la feuille carrefour")
    End Try

    End Sub
    '*******************************************************************************************************
    ' Instancier un tampon de la taille maximum, où sera mémorisé le dessin por réaffichage lors du Paint
    '*******************************************************************************************************
    Private Sub InstancierBufferDessin()
        Me.BufPicDessin = New System.Windows.Forms.PictureBox
        '
        'BufPicDessin
        '
        Me.BufPicDessin.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BufPicDessin.Name = "BufPicDessin"
        Me.BufPicDessin.Size = New System.Drawing.Size(404, 586)
        Me.BufPicDessin.Size = mdiApplication.ClientSize

    End Sub

  '******************************************************************************
  ' Cette feuille Carrefour devient la feuile active
  '******************************************************************************
  Private Sub frmCarrefour_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        cndDéboguage = False

    'variable globale redéfinie sur ce carrefour
    cndVariante = maVariante

    cndpicDessin = picDessin
    cndGraphique = picDessin.CreateGraphics

    'cndAbaque.Owner = Me

    RéaffecterEchelle()

    'Activation des boutons de la barre d'outils
    With mdiApplication
      With .tbrDiagfeux.Buttons()
        'Zoom précédent : actif si il y a une vue  précédente mémorisée
        .Item(MDIDiagfeux.BarreOutilsEnum.ZoomPrécédent).Visible = mEchelles.Count > 1
        'Rafraichir
        .Item(MDIDiagfeux.BarreOutilsEnum.Rafraichir).Visible = True
      End With

      .mnuSensTrajectoires.Checked = maVariante.SensTrajectoires
      .mnuSensCirculation.Checked = maVariante.SensCirculation
      .mnuNord.Checked = maVariante.NordAffiché
      .mnuEchelle.Checked = maVariante.EchelleAffichée

      AfficherContexteFDP()

      RecréerMenuContextuel(.mnuAffichage)
    End With

    AfficherCacherDiagnostic()

    TopicAideCourant = mAideTopic

  End Sub

  Private Sub AfficherContexteFDP()

    'Barre d'état 
    With mdiApplication
      If IsNothing(monFDP) Then
        .staDiagfeux.Panels(1).BorderStyle = StatusBarPanelBorderStyle.None
        .staDiagfeux.Panels(1).Text = ""
        .mnuAfficherFDP.Visible = False
      Else
        .staDiagfeux.Panels(1).BorderStyle = StatusBarPanelBorderStyle.Sunken
        MenuAfficherFDP()
      End If
    End With

  End Sub

  Private Sub AfficherScénarios()
    mdiApplication.AfficherScénarios()
  End Sub

  '============= BLoc de fonctions spécifiques aux scénarios ===========================

  Private Sub RecréerDessinAntagonismes()

    If ModeGraphique AndAlso ScénarioEnCours() Then
      With monPlanFeuxBase()
        PurgerAntagonismes()
        If .Verrou >= [Global].Verrouillage.LignesFeux Then
          .Antagonismes.CréerGraphique(colObjetsGraphiques)
          .Antagonismes.Verrouiller()
        End If
      End With
    End If

  End Sub

  Public Sub NouveauScénario()

    Try

      With monPlanFeuxBase()
        RecréerDessinAntagonismes()
        If .AvecTrafic Then
          AjouterComboTrafic(.Nom)
          Me.tabOnglet.SelectedTab = Me.tabTrafics
        End If
      End With

      AfficherScénarios()

      Modif = True

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "NouveauScénario")

    End Try
  End Sub

  Private Sub PurgerAntagonismes()
    Dim unObjetGraphique As Graphique
    Dim Garbage As New Graphiques

    For Each unObjetGraphique In colObjetsGraphiques
      If TypeOf unObjetGraphique.ObjetMétier Is Antagonisme Then
        Garbage.Add(unObjetGraphique)
      End If
    Next

    For Each unObjetGraphique In Garbage
      colObjetsGraphiques.Remove(unObjetGraphique)
    Next
  End Sub

  Public Sub DupliquerScénario()
    Dim nomScénario As String = InputBox("Nom du scénario dupliqué").Trim
    Dim unScénario As PlanFeuxBase = monPlanFeuxBase()
    Dim unTrafic As Trafic

    Try

      With maVariante
        If nomScénario.Length = 0 Then
        ElseIf Not IsNothing(unScénario) AndAlso String.Compare(nomScénario, unScénario.Nom, ignoreCase:=True) = 0 Then
        ElseIf maVariante.mPlansFeuxBase.Contains(nomScénario) Then
          MessageBox.Show("Un scénario de même nom existe déjà")
        Else

          With .mPlansFeuxBase
            monPlanFeuxBase = .Item(.Add(New PlanFeuxBase(unScénario)))
          End With
          If unScénario.AvecTrafic Then
            unTrafic = New Trafic(unScénario.Trafic)
            unTrafic.Nom = nomScénario
            .mTrafics.Add(unTrafic)
            AjouterComboTrafic(unTrafic.Nom)
            monPlanFeuxBase.Trafic = unTrafic
          Else
            monPlanFeuxBase.Nom = nomScénario
          End If

          Dim cpt As Short = monPlanFeuxBase.mPlansFonctionnement.Count
          If monPlanFeuxBase.mPlansFonctionnement.Contains("") Then
            'Plan de feux de fonctionnement dont le nom est resté identique au scénario initial
            'Lui donner le nom du nouveau scénario
            monPlanFeuxBase.mPlansFonctionnement("").Nom = nomScénario
            cpt -= 1
          End If
          Select Case cpt
            Case 1
              MessageBox.Show("Un plan de feux de fonctionnement a été également dupliqué" & vbCrLf & "Il vous appartient de le renommer")
            Case Is > 1
              MessageBox.Show("Des plans de feux de fonctionnement ont été également dupliqués" & vbCrLf & "Il vous appartient de les renommer")
          End Select

          AfficherScénarios()
          Modif = True
        End If
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try


  End Sub

  Public Sub RenommerScénario()
    Dim nomScénario As String = InputBox("Renommer le scénario " & monPlanFeuxBase.Nom).Trim

    Try
      If nomScénario.Length = 0 Then
      ElseIf String.Compare(nomScénario, monPlanFeuxBase.Nom, ignoreCase:=True) = 0 Then
      ElseIf maVariante.mPlansFeuxBase.Contains(nomScénario) Then
        MessageBox.Show("Un scénario de même nom existe déjà")
      Else
        monPlanFeuxBase.Nom = nomScénario
        If monPlanFeuxBase.AvecTrafic Then
          RenommerComboTrafic(nomScénario, mesTrafics.IndexOf(monPlanFeuxBase.Trafic))
        End If

        AfficherScénarios()
        Modif = True
      End If


    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try
  End Sub

  Public Sub SupprimerScénario()
    Try

      If Not ScénarioNonSupprimable() AndAlso Confirmation("Supprimer le scénario " & monPlanFeuxBase.Nom, Critique:=True) Then
        maVariante.mPlansFeuxBase.Remove(monPlanFeuxBase)
        If monPlanFeuxBase.AvecTrafic Then
          Dim unTrafic As Trafic = monTraficActif()
          'Ajout 27/03/07 : si c'est le dernier trafic, il reste affiché avec ses valeurs si nouveau trafic ensuite
          unTrafic.Réinitialiser()
          AfficherTrafic(AvecLesPiétons:=True)
          SupprimerComboTrafic(mesTrafics.IndexOf(unTrafic))
          mesTrafics.Remove(unTrafic)
        End If
        If maVariante.mPlansFeuxBase.Count = 0 Then
          monPlanFeuxBase = Nothing
        Else
          maVariante.ScénarioCourant = maVariante.mPlansFeuxBase(CType(0, Short))
        End If

        AfficherScénarios()
        Me.tabOnglet.SelectedTab = Me.tabLignesDeFeux
        Modif = True
      End If


    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try
  End Sub

  Public Function ScénarioNonSupprimable() As Boolean
    Dim unPlanFeuxBase As PlanFeuxBase
    Dim unPlanFeuxFct As PlanFeuxFonctionnement
    Dim unTrafic As Trafic = monPlanFeuxBase.Trafic
    Dim Message As String

    If Not IsNothing(unTrafic) Then
      For Each unPlanFeuxBase In maVariante.mPlansFeuxBase
        If Not unPlanFeuxBase Is monPlanFeuxBase AndAlso unPlanFeuxBase.Trafics.Contains(unTrafic) Then
          Message = "La période de trafic de ce scénario est utilisée"
          Message &= vbCrLf & "par un plan de feux"
          Message &= " du scénario " & unPlanFeuxBase.Nom
          MessageBox.Show(Message, "Suppression de scénario impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          ScénarioNonSupprimable = True
          Exit For
        End If
      Next
    End If

  End Function

  Public Sub SélectionnerScénario(ByVal Index As Short)

    Try

      If ScénarioEnCours() AndAlso monPlanFeuxBase.AvecTrafic Then
        monTraficPrécédent = monPlanFeuxBase.Trafic
      End If

      If Index = -1 Then
        monPlanFeuxBase = Nothing
      Else
        monPlanFeuxBase = maVariante.mPlansFeuxBase(Index)
      End If
      'Ceci obligera a recréer la liste des Plans de fonctionnement quand on y accèdera
      monPlanFeuxFonctionnement = Nothing

      If ConflitsInitialisés Then
        If Me.radMatriceConflits.Checked Then
          AfficherMatriceSécurité(0)

        ElseIf maVariante.Verrou < [Global].Verrouillage.Matrices Then
          Me.radMatriceConflits.Checked = True
        End If

        If ModeGraphique Then
          RéafficherAntagonismes()
        End If
      End If

      RecréerDessinAntagonismes()

      Me.chkScénarioDéfinitif.Checked = monPlanFeuxBase Is maVariante.ScénarioDéfinitif
      AfficherProjetDéfinitif()
      ChoisirOngletInitial(OuvertureProjet:=False)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try

  End Sub

  Private Sub AfficherProjetDéfinitif()
    mdiApplication.lblProjetDéfinitif.Text = IIf(Me.chkScénarioDéfinitif.Checked, "Définitif", "Projet")
    mdiApplication.lblProjetDéfinitif.ForeColor = IIf(Me.chkScénarioDéfinitif.Checked, Color.Blue, Color.Red)
  End Sub

  Private Function monTraficActif() As Trafic
    If ScénarioEnCours() Then
      Return monPlanFeuxBase.Trafic
    End If
  End Function

  Private Function mLignesFeux() As LigneFeuxCollection
    Return monPlanFeuxBase.mLignesFeux
  End Function

  Private Function mAntagonismes() As AntagonismeCollection
    Return monPlanFeuxBase.Antagonismes
  End Function


  Private Function mFiltrePhasage() As FiltrePhasage
    Return monPlanFeuxBase.mFiltrePhasage
  End Function

  Private Property monPlanFeuxBase() As PlanFeuxBase
    Get
      Return maVariante.ScénarioCourant
    End Get
    Set(ByVal Value As PlanFeuxBase)
      maVariante.ScénarioCourant = Value
    End Set
  End Property

  Private Function ScénarioEnCours() As Boolean
    Return Not IsNothing(monPlanFeuxBase)
  End Function

  Private Function mesPlansPourPhasage() As PlanFeuxCollection
    Return maVariante.PlansPourPhasage
  End Function

  Private Sub frmCarrefour_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
    'Eviter d'avoir 2 fenêtres diagnostic d'affichées
    'Mise en commentaire suite à demande du CERTU
    '   FenetreDiagnostic.Hide()
    Me.InterrompreCommande()

  End Sub

  Private Sub MenuAfficherFDP()

    With mdiApplication
      .mnuAfficherFDP.Visible = Not DiagrammeActif()
      If monFDP.Visible Then
        .mnuAfficherFDP.Text = "Masquer le fond de plan"
      Else
        .mnuAfficherFDP.Text = "Afficher le fond de plan"
      End If
    End With
  End Sub

  Public Sub BasculeAffichageFDP()
    monFDP.Visible = Not monFDP.Visible
    maVariante.Verrouiller()
    Redessiner()
    MenuAfficherFDP()
  End Sub

  Private Sub RéaffecterEchelle()
    AffecterLimites(cndpicDessin)
    cndParamDessin = mParamDessin
  End Sub

  Private Property mParamDessin() As ParamDessin
    Get
      Return maVariante.mParamDessin
    End Get
    Set(ByVal Value As ParamDessin)
      maVariante.mParamDessin = Value
    End Set
  End Property

  '******************************************************************************
  ' Redimensionnement de la feuille
  '******************************************************************************
  Private Sub frmCarrefour_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    Dim newPosition As Short

    'WARNING (AV : 02/09/03) : Veiller à ce que la fenêtre soit assez grande si on importe une image raster, sinon le PictureBox ne sera pas assez grand non +
    If Not pnlPalette Is Nothing Then
      DéfinirDéfautLargeurPanels()
      maVariante.mParamDessin.TailleFenêtre = Me.Size
      newPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
      Dim pMouseDeb As Point = CvPoint(picDessin.Size)
      Me.splitGraphiqueDonnées.SplitPosition = newPosition

      If Not DiagrammeActif() Then
        pMouseDeb = Point.op_Subtraction(pMouseDeb, picDessin.Size)
        pMouseDeb.X /= 2
        pMouseDeb.Y /= 2

        'pMouseDeb = Point.op_Addition(pMouseDeb, Milieu(pMouseDeb, CvPoint(picDessin.Size)))

        cndParamDessin = mParamDessin
        mParamDessin = DéterminerNewOrigineRéellePAN(pMouseDeb)
        cndParamDessin = mParamDessin
        RecréerGraphique()
        Redessiner()
      End If
    End If

  End Sub

  '******************************************************************************
  ' La feuille va se fermer
  '******************************************************************************
  Private Sub frmCarrefour_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles MyBase.Closing

    If Modif Then
      Select Case MessageBox.Show(mdiApplication, "Voulez-vous enregistrer les modifications apportées au projet " & maVariante.Libellé(AjoutEtoile:=False) & " ?", NomProduit, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Case DialogResult.Yes
          e.Cancel = Enregistrer()
        Case DialogResult.Cancel
          e.Cancel = True
      End Select
    End If

    FermetureEnCours = Not e.Cancel

  End Sub

  Public Property Modif() As Boolean
    Get
      Return maVariante.AEnregistrer
    End Get
    Set(ByVal Value As Boolean)
      maVariante.AEnregistrer = Value
      Me.Text = maVariante.Libellé
    End Set
  End Property

  '******************************************************************************
  ' La feuille est fermée
  '******************************************************************************
  Private Sub frmCarrefour_Closed(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles MyBase.Closed
    cndVariantes.Remove(maVariante)
    'cndAbaque.Owner = Nothing
    'cndAbaque.Hide()

    With mdiApplication
      With .tbrDiagfeux.Buttons()
        Dim i As MDIDiagfeux.BarreOutilsEnum
        'Ne laisser visibles que les boutons Nouveau et Ouvrir
        For i = MDIDiagfeux.BarreOutilsEnum.Enregistrer To MDIDiagfeux.BarreOutilsEnum.Echelle
          .Item(i).Visible = False
        Next
      End With
      .mnuScénario.Visible = False
      .pnlScénario.Visible = False
      .mnuEnregistrer.Enabled = False
      .mnuEnregSous.Enabled = False
      .mnuImprimer.Enabled = False
      .mnuRafraichir.Enabled = False
    End With

  End Sub

  Private Sub frmCarrefour_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    If e.KeyCode = Keys.Delete Then
      If Not IsNothing(objSélect) Then
        Dim unObjetMétier As Métier = objSélect.ObjetMétier
        If TypeOf unObjetMétier Is PassagePiéton Then
          Me.btnPiétonMoins.PerformClick()
        ElseIf TypeOf unObjetMétier Is LigneFeuVéhicules Then
          Me.btnLigneFeuxMoins.PerformClick()
        ElseIf TypeOf unObjetMétier Is TrajectoireVéhicules Then
          Me.btnTrajectoireMoins.PerformClick()
        ElseIf TypeOf unObjetMétier Is TraverséePiétonne Then
          Me.btnTraverséeMoins.PerformClick()
        End If
      End If
    End If
  End Sub

  Private Sub frmCarrefour_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) _
   Handles MyBase.Paint

    If ObjetEffaçableParPaint() Then savObjSélect = objSélect

  End Sub
  Private Function ObjetEffaçableParPaint() As Boolean
    If IsNothing(objSélect) Then
      Dim unObjetMétier As Métier

      If TypeOf unObjetMétier Is TrajectoireVéhicules Then
      ElseIf TypeOf unObjetMétier Is Antagonisme Then
      Else
        Return True
      End If
    End If

  End Function
#End Region
#Region " Initialisations des panels"

  '******************************************************************************
  ' Initialiser le panel Géométrie
  '******************************************************************************
  Private Sub InitGéométrie()
    Dim row As Short
    Dim uneBranche As Branche
    Dim fg As GrilleDiagfeux = Me.AC1GrilleBranches

    InitStyles()

    fg.SelectionMode = Grille.SelectionModeEnum.Cell

    Me.pnlGéométrie.BringToFront()
    fg.Rows.Count = mesBranches.Count + 1
    fg.Height = (fg.Rows.Count - 1) * 17 + 21

    'Positionner correctement le tableau d'ilots en fonction de la taille du tableau de branches et le réduire à son en tête
    Me.pnlIlots.Top = fg.Top + fg.Height
    Me.pnlBtnGéométrie.Top = Me.pnlIlots.Top

    Me.AC1GrilleIlot.Rows.Count = 1

    For Each uneBranche In mesBranches
      row = mesBranches.IndexOf(uneBranche) + 1
      fg(row, 0) = mesBranches.ID(uneBranche)
      With uneBranche
        fg(row, 1) = .NomRue
        fg(row, 2) = .Angle
        fg(row, 3) = .Longueur
        fg(row, 4) = .LargeurVoies
        fg(row, 5) = .NbVoies(Voie.TypeVoieEnum.VoieEntrante)
        fg(row, 6) = .NbVoies(Voie.TypeVoieEnum.VoieSortante)
        If Not IsNothing(.mIlot) Then
          fg(row, 7) = True.ToString
        End If
      End With
    Next

    'Cette propriété, non documentée dans l'aide en ligne, mais présente dans la page de propriétés Design, 
    'permet de visualiser ou non  le triangle indicateur du tri (glyph) dans l'entête de colonne
    fg.ShowSort = False

    'La grille comporte 16 styles(stock styles) : classe CellStyleCollection
    'Le style Normal et Les 15 autres basés sur Normal (tout changement sur Normal se répercute sur les autres sauf modif explicite)

  End Sub
#Region " InitLignesFeux"
  Private Sub InitStyles()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleBranches

    With fg
      'Définir les styles personnalisés
      StyleGrisé = .Styles.Add("Grisé")
      StyleGrisé.BackColor = Color.LightGray     'Color.LightSlateGray : un peu de bleuté dans le gris
      StyleGriséBooléen = .Styles.Add("GriséBooléen")
      StyleGriséBooléen.BackColor = Color.LightGray     'Color.LightSlateGray : un peu de bleuté dans le gris
      StyleDégrisé = .Styles.Add("Dégrisé")
      StyleGriséGras = .Styles.Add("GriséGras", StyleGrisé)
      Dim fntGras As New Font(StyleGrisé.Font, FontStyle.Bold)
      StyleGriséGras.Font = fntGras
      StyleDégriséGras = .Styles.Add("DéGriséGras", StyleDégrisé)
      StyleDégriséGras.Font = fntGras
      StyleSaisie = .Styles.Add("Saisie")
      StyleSaisie.BackColor = Color.LightPink ' l'autre couleur qui reste 'visible' est LightYellow
      StyleSaisieItalique = .Styles.Add("Italique", StyleSaisie)
      Dim fntItalique As New Font(StyleSaisie.Font, FontStyle.Italic)
      StyleSaisieItalique.Font = fntItalique
    End With

  End Sub

  '******************************************************************************
  ' Initialiser le panel Lignes de feux
  '******************************************************************************
  Private Sub InitLignesFeux()

    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim uneBranche As Branche

    Try

      With fg
        .SelectionMode = Grille.SelectionModeEnum.Cell

        For Each uneBranche In mesBranches
          .Cols("IDVoie").ComboList &= "|" & mesBranches.ID(uneBranche)
        Next

        .Cols("Signal").ComboList = cndSignaux.strListe(Anticipation:=False, SansPiétons:=ModeGraphique)
        .Cols("SignalAnticipation").ComboList = cndSignaux.strListe(Anticipation:=True)

        If ModeGraphique Then
          'Interdire la modification de certaines colonnes
          .Cols("IdVoie").AllowEditing = False
          .Cols("NbVoies").AllowEditing = False
          .Cols("TAG").AllowEditing = False
          .Cols("TD").AllowEditing = False
          .Cols("TAD").AllowEditing = False
          .Cols("IdVoie").Style = StyleGrisé
          .Cols("NbVoies").Style = StyleGrisé
          .Cols("TAG").Style = StyleGriséBooléen
          .Cols("TD").Style = StyleGriséBooléen
          .Cols("TAD").Style = StyleGriséBooléen
          'L'affectation des styles ci-dessus a fait disparaitre la propriété booléen des colonnes cases à cocher
          '   .Cols("TAD").DataType = GetType(System.Boolean)

        Else
          'Interdire la saisie des voies entrantes dans la géométrie) : saisie faite indirectement par les lignes de feux
          Me.AC1GrilleBranches.Cols("NbVoiesE").Style = StyleGrisé
        End If

      End With

      AfficherLignesDeFeux(fg)
      ActiverBoutonsLignesFeux()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Initialisation des lignes de feux")
    End Try

  End Sub

  Private Sub InitVerrouillages()

    Try
      'Traitement d'infos issues de la lecture
      Me.chkVerrouLignesFeux.Checked = maVariante.VerrouLigneFeu
      If Me.chkVerrouLignesFeux.Checked Then
        maVariante.VerrouillerLignesFeux(Verrouillage:=True, uneCollection:=colObjetsGraphiques, ChargementEnCours:=ChargementEnCours)
        'Pour DIAGFEUX : on peut initialiser les conflits et l'aspect du phasage 
        'dès le verrouillage des lignes de feux(indépendant du plan de feux de base)
        InitConflits()
      End If

      Me.chkSensTrajectoires.Checked = maVariante.SensTrajectoires

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Initialisation des verrouillages")

    End Try
  End Sub

#End Region
#Region " InitConflits"
  '******************************************************************************
  ' Initialiser le panel Conflits
  '******************************************************************************
  Private Sub InitConflits()

    Dim fg As GrilleDiagfeux = Me.Ac1GrilleSécurité
    Dim rg As Grille.CellRange
    Static Passage As Boolean

    If Not Passage Then

      Try
        With fg
          'Instancier les styles spécifiques
          StyleRouge = .CréerStyle(StyleRouge, "Rouge", Color.Red)
          StyleVert = .CréerStyle(StyleVert, "Vert", Color.LightGreen)
          StyleOrangé = .CréerStyle(StyleOrangé, "Orangé", Color.LightSalmon)

          With .Styles
            If IsNothing(StyleGrisé) Then
              StyleGrisé = .Add("Grisé")
              StyleGrisé.BackColor = Color.LightGray
            End If
          End With

          If IsNothing(StyleOrangé) Then
            StyleOrangé = fg.Styles.Add("Orangé")
            StyleOrangé.BackColor = Color.LightSalmon
          End If

          'Adapter la taille de la grille au nombre de lignes de feux
          .Rows.Count = mesLignesFeux.Count + 1
          .Cols.Count = .Rows.Count
          'Rallonger la 1ère colonne pour un en-tête un peu + long
          .Cols(0).Width = .Cols(1).Width + 10

          Dim nbCellules As Single = .Cols.Count + 0.3

          'Rajouter 10 pixels pour tenir copte de la 1ère colonne
          .Width = nbCellules * .Cols.DefaultSize + 10
          Dim LargeurGrille As Single = Math.Max(Me.pnlAntagonismes.Width, .Width)
          DéfinirDéfautLargeurPanel([Global].OngletEnum.Conflits, LargeurGrille + 3 * LGMARGE)

          .Height = nbCellules * .Rows.DefaultSize
          pnlConflits.AutoScrollMinSize = New Size(lgPanel(3), 150)
          DéfinirSplitPosition()

          .Left = LGMARGE
          ' décaler si nécessaire le panel Verrou (qui contient aussi les symboles Vert et Rouge)
          Me.pnlVerrouMatrice.Top = Math.Max(Me.pnlVerrouMatrice.Top, .Top + .Height)
          Me.pnlBoutonsRouges.Top = Me.pnlVerrouMatrice.Top

          'Décaler en conséquence le panel Antagonismes
          With Me.pnlVerrouMatrice
            Me.pnlAntagonismes.Top = .Top + .Height
          End With
        End With

        ' Ecrire les Entete de ligne et de colonne de la matrice avec l'ID des lignes de feux
        AfficherEnteteMatriceSécurité()

        ' Par défaut tous les feux sont compatibles (vert)
        rg = fg.GetCellRange(1, 1, fg.Rows.Count - 1, fg.Cols.Count - 1)
        rg.Style = StyleVert

        'Le phasage peut aussi être dimensionné dès le verrouillage des lignes de feux
        AfficherOrganisationPhasage()

        Passage = True

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Initialisation des conflits")
      End Try

    End If

    If ScénarioEnCours() AndAlso Not ConflitsInitialisés Then
      If ModeGraphique Then
        AntagonismesEnCours = True
        InitAntagonismes()
        AntagonismesEnCours = False
      Else
        Me.pnlAntagonismes.Visible = False
      End If

      'Sélectionner l'option Matrice des conflits
      If Me.radMatriceConflits.Checked Then
        AfficherMatriceSécurité(0)
      Else
        Me.radMatriceConflits.Checked = True
      End If
      ConflitsInitialisés = True
    End If

  End Sub

  '*********************************************************************************************
  'Afficher les ID de lignes de feux en tête de ligne et de colonne de la matrice de sécurité
  '*********************************************************************************************
  Private Sub AfficherEnteteMatriceSécurité()
    Dim uneLigneFeux As LigneFeux
    Dim row, col As Short
    Dim ID As String
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleSécurité

    Try
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        col = row
        ID = uneLigneFeux.ID

        'Mettre l'identignfiant dans la 1ère colonne (fixe)
        fg(row, 0) = mesBranches.ID(uneLigneFeux.mBranche) & "-" & ID
        'Mettre l'identifiant en tête de colonne
        fg(0, col) = ID

      Next

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherEnteteMatriceSécurité")
    End Try

  End Sub

  '*********************************************************************************************
  'Afficher les ID de lignes de feux en tête de ligne et de colonne de la matrice de sécurité
  '*********************************************************************************************
  Private Sub InitAntagonismes()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim row As Short
    Dim rg As Grille.CellRange

    fg.Rows.Count = mAntagonismes.Count + 1
    fg.FocusRect = Grille.FocusRectEnum.Heavy

    For Each unAntagonisme In mAntagonismes()
      row += 1
      With unAntagonisme
        If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
          'Pas de choix proposé pour les conflits systématiques
          fg.Rows(row).Visible = False
        Else
          If unAntagonisme Is unAntagonisme.MêmesCourants Then
            mAntagonismes.NonTousSystématiques = True
          Else
            'On n'affiche qu'une ligne dans la grille pour tous les antagonismes de même courant
            fg.Rows(row).Visible = False
          End If
          fg(row, 0) = unAntagonisme.Libellé(Antagonisme.PositionEnum.Premier, mesBranches)
          fg(row, 1) = unAntagonisme.Libellé(Antagonisme.PositionEnum.Dernier, mesBranches)
          rg = fg.GetCellRange(row, 2)
          DéfinirStyle(unAntagonisme, rg)
        End If
      End With
    Next

    ' on n'affiche pas la grille des antagonismes s'il n'y a que des conflits systématiques
    Me.pnlAntagonismes.Visible = mAntagonismes.NonTousSystématiques

    Dim uneBranche As Branche

    With Me.cboBrancheCourant1
      With .Items
        .Clear()
        For Each uneBranche In mesBranches
          .Add(mesBranches.ID(uneBranche))
        Next

        'Rajouter en v13
        .Add("Tous")
      End With
      'Sélectionner la 1ère branche
      .SelectedIndex = 0
    End With

    Me.btnRéinitAntago.Enabled = mAntagonismes.ConflitsPartiellementRésolus

  End Sub

  Private Sub DéfinirStyle(ByVal unAntagonisme As Antagonisme, ByVal rg As Grille.CellRange)

    If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
      rg.Style = StyleOrangé
    Else
      rg.Style = StyleDégrisé
    End If

    If unAntagonisme.Autorisé Then
      rg.Checkbox = Grille.CheckEnum.Checked
    Else
      rg.Checkbox = Grille.CheckEnum.Unchecked
    End If

  End Sub

  Private Sub RéafficherLibellésAntagonismes()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim row As Short

    For Each unAntagonisme In mAntagonismes()
      row += 1
      With unAntagonisme
        If unAntagonisme.TypeConflit <> Trajectoire.TypeConflitEnum.Systématique Then
          fg(row, 0) = unAntagonisme.Libellé(Antagonisme.PositionEnum.Premier, mesBranches)
          fg(row, 1) = unAntagonisme.Libellé(Antagonisme.PositionEnum.Dernier, mesBranches)
        End If
      End With
    Next

  End Sub

  Private Sub btnRéinitAntago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRéinitAntago.Click
    If Confirmation("Réinitialiser les antagonismes", Critique:=False) Then
      RéinitialiserAntagonismes()
    End If
  End Sub

  Private Sub RéinitialiserAntagonismes()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim row As Short
    Dim rg As Grille.CellRange

    For Each unAntagonisme In mAntagonismes()
      row += 1
      With unAntagonisme
        If unAntagonisme.TypeConflit <> Trajectoire.TypeConflitEnum.Systématique Then
          rg = fg.GetCellRange(row, 2)
          rg.Data = False
          rg.Style = StyleOrangé
          unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible
        End If
      End With
    Next

    mLignesFeux.RéinitialiserAntagos(mesLignesFeux)

    AfficherAntagosDansMatrice(Me.Ac1GrilleSécurité)

    Me.btnRéinitAntago.Enabled = False

  End Sub

#End Region
#Region " InitTrafics"
  '******************************************************************************
  ' Initialiser le panel Matrices de trafic
  '******************************************************************************
  Private Function InitTrafics() As Boolean
    Dim uneBranche As Branche
    Dim fg As GrilleDiagfeux = Me.AC1GrilleTraficVéhicules
    Dim fgP As GrilleDiagfeux = Me.Ac1GrilleTraficPiétons
    Dim rg As Grille.CellRange
    Dim row As Short
    Dim unStyle As Grille.CellStyle
    Static Passage As Boolean = False

    If Not Passage Then

      Try

        With fg

          With .Styles
            'Instancier les styles spécifiques
            If IsNothing(StyleGrisé) Then
              StyleGrisé = .Add("Grisé")
              StyleGrisé.BackColor = Color.LightGray
            End If
            If IsNothing(StyleGriséRouge) Then
              StyleGriséRouge = .Add("GriséRouge", StyleGrisé)
              StyleGriséRouge.ForeColor = Color.Red
            End If
          End With

          'Créer une ligne de trafic par branche du carrefour
          For Each uneBranche In mesBranches
            row = mesBranches.IndexOf(uneBranche) + 1
            fg(row, 0) = mesBranches.ID(uneBranche)
            fg.Rows.Add()
          Next
          fg(mesBranches.Count + 1, 0) = "TS"     ' Intitulé trafic sortant

          'Supprimer les colonnes au-dela du nombre de branches(L'initialisation est faite sur 6 branches)
          fg.Cols.RemoveRange(mesBranches.Count + 1, count:=6 - mesBranches.Count)
          'Idem pour les piétons
          fgP.Cols.RemoveRange(mesBranches.Count, count:=6 - mesBranches.Count)

          'Mettre en grisé la dernière ligne (totaux  sortant)
          rg = fg.GetCellRange(mesBranches.Count + 1, 1, mesBranches.Count + 1, mesBranches.Count + 1)
          rg.Style = StyleGrisé
          '
          'Mettre en grisé la dernière colonne(totaux  entrant)
          rg = fg.GetCellRange(1, mesBranches.Count + 1, mesBranches.Count, mesBranches.Count + 1)
          rg.Style = StyleGrisé

          'Interdire les saisies correspondantes
          fg.Cols(fg.Cols.Count - 1).AllowEditing = False
          fg.Rows(fg.Rows.Count - 1).AllowEditing = False
        End With    ' fg

        'griser les lignes et/ou les colonnes des branches sans voies entrantes et/ou sortantes (sens unique)
        Dim numBranche As Short
        For Each uneBranche In mesBranches
          With mesBranches
            numBranche = .IndexOf(uneBranche) + 1
            rg = fg.GetCellRange(numBranche, numBranche)
            rg.Style = StyleGrisé
            If uneBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
              rg = fg.GetCellRange(1, numBranche, .Count, numBranche)
              rg.Style = StyleGrisé
              fg.Cols(numBranche).AllowEditing = False
            ElseIf uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              rg = fg.GetCellRange(numBranche, 1, numBranche, .Count)
              rg.Style = StyleGrisé
              fg.Rows(numBranche).AllowEditing = False
            End If
          End With
        Next

        'Alimenter les combos avec les trafics existants
        Dim unTrafic As Trafic
        For Each unTrafic In mesTrafics
          AjouterComboTrafic(unTrafic.Nom)
        Next

        Passage = True

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Initialisation des trafics")
      End Try
    End If  ' Not Passage

    Try
      If Not ChargementEnCours AndAlso ScénarioEnCours() AndAlso Not monPlanFeuxBase.AvecTrafic Then
        AfficherMessageErreur(Me, "Le scénario " & monPlanFeuxBase.Nom & " ne comporte pas de trafic")
        Return True

      ElseIf maVariante.mTrafics.Count = 0 Then
        'Premier appel de l'onglet trafic pour ce projet : créer une première période de trafic
        Me.btnNouveauTrafic.PerformClick()
        Return maVariante.mTrafics.Count = 0
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Initialisation des trafics")
    End Try

  End Function

#End Region
#Region " InitPhasage"
  '******************************************************************************
  ' Initialiser le panel Organisation du phasage (sous-panel de Plan de feux)
  '******************************************************************************
  Private Function InitPhasage(Optional ByVal Réinitialisation As Boolean = False) As Boolean

    If monPlanFeuxBase.PhasageInitialisé Then
      PréparerPhasage()

    Else
      Try
        Dim unPlanFeux As PlanFeuxPhasage

        'Rechercher les plans pour phasage trop longs (> 130s) : à ne pas proposer
        monPlanFeuxBase.ComplémentOrganiserPhasage(False)

        Dim nbScénarios As Short = mesPlansPourPhasage.Count

        If nbScénarios = 0 Then
          AfficherMessageErreur(Me, "Tous les phasages possibles conduisent à un temps d'attente supérieur à " & AttenteMax & vbCrLf & "Revoir les trafics ou changer le plan de circulation")
          Return True
        End If

        PréparerPhasage()

        monPlanFeuxBase.PhasageInitialisé = True

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "InitPhasage")
      End Try

    End If
  End Function

  Private Sub PréparerPhasage()
    Dim nbScénarios As Short = mesPlansPourPhasage.Count
    Dim unPlanFeux As PlanFeuxPhasage

    Me.lblDécoupagePhases.Text = IIf(nbScénarios = 1, "Phasage unique proposé", CStr(nbScénarios) & " phasages possibles")
    AffichagePhasesEnCours = True

    Me.cboDécoupagePhases.Visible = mesPlansPourPhasage.Count > 1

    Me.chk3Phases.Enabled = True

    With mFiltrePhasage()
      'Modif AV 27/03/07 : par défaut ne proposer que les 2 phases
      'Me.chk3Phases.Checked = .TroisPhases
      Me.cbolLFMultiPhases.SelectedIndex = .LigneFeuxMultiPhases
      Me.cboPhasesSpéciales.SelectedIndex = .AvecPhaseSpéciale

      If IsNothing(monTraficActif) Then
        Me.cboRéserveCapacité.Enabled = False
        Me.cboRéserveCapacité.SelectedIndex = -1
        Me.txtRéserveCapacitéPourCent.Visible = False
      Else
        Me.cboRéserveCapacité.Enabled = True
        Me.cboRéserveCapacité.SelectedIndex = .CritèreCapacité
        Me.txtRéserveCapacitéPourCent.Visible = True
      End If
    End With

    For Each unPlanFeux In mesPlansPourPhasage()
      With unPlanFeux
        If .mPhases.Count > 2 Then
          'Modif AV 27/03/07 : inutile suite à la modif ci-dessus
          '   Me.chk3Phases.Enabled = True
          If unPlanFeux.PlanBaseAssocié Is monPlanFeuxBase Then
            Me.chk3Phases.Checked = True
          End If
        End If
      End With
    Next

    AffichagePhasesEnCours = False
    AfficherComboPhasage()

    If cboDécoupagePhases.Items.Count = 0 And Not Me.chk3Phases.Checked Then
      'Aucun feu à 2 phases : recommencer avec les 3 phases
      Me.chk3Phases.Checked = True
    End If

  End Sub

#End Region
#Region "InitPlanFeux"
  '***************************************************************************************************
  ' Initialiser le panel Plan de feux de base (sous-panel de Plan de feux)
  ' RecalculerMini : Recalcule les valeurs mini des plans de feux suite à la modif des verts mini
  '****************************************************************************************************
  Private Sub InitPlanFeuxBase(Optional ByVal RecalculerMini As Boolean = False)

    Try

      If RecalculerMini Then
        '//DIAGFEUX//
        'Dans le cas des scénarios, il ne faut plus que calculer les plans de phasage du scénario
        monPlanFeuxBase.CalculerDuréesMiniPlansFeux()

      Else
        monPlanFeuxActif = monPlanFeuxBase
      End If

      Me.txtVertMiniVéhicule.Text = maVariante.VertMiniVéhicules
      Me.txtVertMiniPiéton.Text = maVariante.VertMiniPiétons

      Me.chkVerrouFeuBase.Checked = (monPlanFeuxBase.Verrou = [Global].Verrouillage.PlanFeuBase)
      AfficherPlanFeux()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & "InitPlanFeuxBase")
    End Try

  End Sub

#End Region
#End Region
#Region " Fonctions partagées"
  Private Sub ChoisirOngletInitial(Optional ByVal OuvertureProjet As Boolean = True)
    Dim unOnglet As TabPage
    Dim OngletActif As TabPage = Me.tabOnglet.SelectedTab
    Dim TraficInterdit As Boolean

    ChangementDeScénario = True
    Me.chkVerrouMatrice.Checked = maVariante.Verrou >= [Global].Verrouillage.Matrices

    If ScénarioEnCours() AndAlso monPlanFeuxBase.AvecTrafic Then
      Me.cboTrafic.Text = monPlanFeuxBase.Nom
      Me.tabTrafics.Enabled = True
    Else
      Me.cboTrafic.SelectedIndex = -1
      Me.tabTrafics.Enabled = False
    End If

    Select Case maVariante.Verrou
      Case [Global].Verrouillage.Aucun
        GérerChangementOnglet()
        unOnglet = Me.tabGéométrie

      Case [Global].Verrouillage.Géométrie
        Me.chkVerrouGéométrie.Checked = True

        If ScénarioEnCours() Then
          'Forcément un scénario avec trafic à ce stade d'avancement du projet
          unOnglet = Me.tabTrafics
        Else
          unOnglet = Me.tabLignesDeFeux
        End If

      Case [Global].Verrouillage.LignesFeux
        Me.chkVerrouGéométrie.Checked = True
        Me.chkVerrouLignesFeux.Checked = True

        If ScénarioEnCours() Then
          If monPlanFeuxBase.AvecTrafic Then
            If monTraficActif.Verrouillé Then
              unOnglet = Me.tabConflits
            Else
              unOnglet = Me.tabTrafics
            End If
          Else
            unOnglet = Me.tabConflits
            TraficInterdit = OngletActif Is Me.tabTrafics
          End If

        Else
          unOnglet = Me.tabLignesDeFeux
        End If

      Case [Global].Verrouillage.Matrices, [Global].Verrouillage.PlanFeuBase
        Me.chkVerrouGéométrie.Checked = True
        Me.chkVerrouLignesFeux.Checked = True
        Me.chkVerrouMatrice.Checked = True
        Me.chkVerrouFeuBase.Checked = (monPlanFeuxBase.Verrou = [Global].Verrouillage.PlanFeuBase)
        unOnglet = Me.tabPlansDeFeux

        If Not monPlanFeuxBase.AvecTrafic Then
          TraficInterdit = OngletActif Is Me.tabTrafics
        End If


    End Select

    If OuvertureProjet Then
      Me.tabOnglet.SelectedTab = unOnglet
      If unOnglet Is Me.tabPlansDeFeux Then
        If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
          Me.radFeuFonctionnement.Checked = True
        ElseIf monPlanFeuxBase.PhasageRetenu Then
          Me.radFeuBase.Checked = True
        End If
      End If

    Else

      If IsNothing(OngletActif) Then
        Me.tabOnglet.SelectedTab = unOnglet
      ElseIf OngletAssocié(OngletActif) > OngletAssocié(unOnglet) Then
        'L'onglet en cours n'est pas acceptable pour ce scénario
        Me.tabOnglet.SelectedTab = unOnglet
      ElseIf TraficInterdit Then
        Me.tabOnglet.SelectedTab = unOnglet
      ElseIf Not ChoisirPanel() Then
        'Conserver l'onglet courant mais redessiner en fonction du scénario
        maVariante.Verrouiller()
        Redessiner()
      End If

    End If

    ChangementDeScénario = False

  End Sub

  '****************************************************************************************
  'ChoisirPanel : le changement de scénario peut conduire à conserver l'onglet en cours
  'Il faut analyser si le panel en cours peut être conservé
  'Retourne True si on a changé de panel
  '****************************************************************************************
  Private Function ChoisirPanel() As Boolean

    If Me.tabOnglet.SelectedTab Is Me.tabConflits Then
      InitConflits()
      If pnlConflitsIndex <> 0 AndAlso Not monPlanFeuxBase.Verrou = [Global].Verrouillage.Matrices Then
        'La matrice des conflits n'est encore pas verrouillée pour ce scénario 
        Me.radMatriceConflits.Checked = True
        ChoisirPanel = True
      End If
    End If

    If Me.tabOnglet.SelectedTab Is Me.tabPlansDeFeux Or monPlanFeuxBase.Verrou >= [Global].Verrouillage.Matrices Then

      Return ChoisirOngletPlanfeux()

    End If

  End Function

  Private Function ChoisirOngletPlanfeux() As Boolean

    Select Case pnlPlansFeuxIndex
      Case 0
        InitPhasage()

      Case 1
        If Not monPlanFeuxBase.PhasageRetenu Then
          Me.radPhasage.Checked = True
          Return True
        Else
          InitPlanFeuxBase()
        End If

      Case 2
        If Not monPlanFeuxBase.PhasageRetenu Then
          Me.radPhasage.Checked = True
          Return True
        ElseIf Not monPlanFeuxBase.Verrou = [Global].Verrouillage.PlanFeuBase Then
          Me.radFeuBase.Checked = True
          Return True
        Else
          InitPlansFeuxFonctionnement()
        End If
    End Select

  End Function

  Private Sub AfficherLignesDeFeux(Optional ByVal fg As GrilleDiagfeux = Nothing)
    If IsNothing(fg) Then fg = Me.AC1GrilleFeux

    'Afficher les lignes de feux dans le tableau
    Dim uneLigneFeux As LigneFeux
    For Each uneLigneFeux In mesLignesFeux
      AfficherLigneDeFeux(uneLigneFeux, fg)
    Next

    If ModeGraphique Then
      fg.Rows.Count = Max(2, mesLignesFeux.Count + 1)
      fg.Rows(1).Visible = (mesLignesFeux.Count > 0)
    Else
      'En mode manuel, on crée une ligne de feux vide supplémentaire pour permettre la saisie d'une nouvelle ligne de feux
      If fg.Rows.Count < mesLignesFeux.Count + 2 And Not maVariante.VerrouLigneFeu Then fg.Rows.Add()
    End If


  End Sub

  '******************************************************************************
  ' Afficher les différents champs d'une ligne de feux dans la ligne de la grille
  '******************************************************************************
  Private Sub AfficherLigneDeFeux(ByVal uneLigneFeux As LigneFeux, Optional ByVal fg As GrilleDiagfeux = Nothing)
    If IsNothing(fg) Then fg = Me.AC1GrilleFeux
    Dim row As Short = mesLignesFeux.IndexOf(uneLigneFeux) + 1

    'Si besoin, créer la ligne dans la grille
    If row >= fg.Rows.Count Then
      fg.Rows.Add()
    Else
      fg.Rows(1).Visible = True
    End If

    'Rechercher la ligne de la grille adaptée
    Dim rg As Grille.CellRange = fg.TouteLaLigne(row)
    'Afficher les données dans la ligne
    rg.Clip = uneLigneFeux.strLigneGrille(mesBranches, Séparateur:=Chr(9))

    GriserLignePiétons(fg, row, uneLigneFeux.EstPiéton)

  End Sub

  '******************************************************************************
  ' Insérer la ligne de feux dans le tableau des lignes de feux
  '******************************************************************************
  Private Sub InsérerLigneDeFeux(ByVal Position As Short, ByVal uneLigneFeux As LigneFeuVéhicules)
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    SelectObject = True
    'Ne pas insérer de ligne si c'est la première ligne de feux(toutes lignes de feux confondues), car on garde toujours au moins une ligne 'vide'
    If fg.Rows.Count > 2 Or mesLignesFeux.Count > 1 Then
      fg.Rows.Insert(Position + 1)
    End If
    AfficherLigneDeFeux(uneLigneFeux, fg)

    Position += 1
    If Position < fg.Rows.Count - 1 Then
      GriserLignePiétons(fg, Position + 1, mesLignesFeux(CType(Position, Short)).EstPiéton)
    End If

    SelectObject = False
  End Sub

  '******************************************************************************
  ' Déterminer s'il faut griser les cases non concernées si ligne de feux piétonne
  '******************************************************************************
  Private Sub GriserLignePiétons(ByVal fg As Grille.C1FlexGrid, ByVal numLigne As Short, ByVal EstPiéton As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle
    Static StyleMasqué As Grille.CellStyle

    If ModeGraphique Then
      'Seules les colonnes Signal et Signal associé peuvent basculer selon que la ligne est piétons ou véhicule
      'les autres sont soient autorisées soient interdites dès le départ
      rg = fg.GetCellRange(numLigne, 3, numLigne, 4)

    Else
      If maVariante.Verrou = [Global].Verrouillage.Géométrie Then
        'Colonne  : Signal associé - Nombres de voies et TAD,TAG,TD
        rg = fg.GetCellRange(numLigne, 4, numLigne, 8)

      Else
        'Colonne : Signal (les suivantes ainsi que la 1ère sont déjà inhibées lors du verrouillage
        'Le signal peut par contre être modifié pour un véhicule même après verrouillage
        rg = fg.GetCellRange(numLigne, 3)
      End If
    End If

    'Déterminer le style selon que la ligne de feux est véhicules ou piétons
    If EstPiéton Then
      unStyle = StyleGrisé
    Else
      unStyle = StyleDégrisé
    End If

    If IsNothing(rg.Style) Then
      'Faire une initialisation du style(avec n'importe quoi)
      rg.Style = StyleDégrisé
    End If

    'Appliquer le style
      rg.Style = unStyle

    'If ModeGraphique And Not EstPiéton Then
    '  rg = fg.GetCellRange(numLigne, 7)
    '  unStyle = StyleDégrisé
    '  rg.Style = unStyle
    'End If

  End Sub

  Private Sub AfficherOrganisationPhasage()
    Dim uneLigneFeux As LigneFeux
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    Dim rg As Grille.CellRange
    Dim row, j As Short
    Dim ID As String
    Dim nbLignes As Short

    With fg
      With .Styles

        'Instancier les styles spécifiques
        If IsNothing(StyleRouge) Then
          If IsNothing(StyleGrisé) Then
            StyleGrisé = .Add("Grisé")
            StyleGrisé.BackColor = Color.LightGray
          End If
          StyleRouge = .Add("Rouge")
          StyleRouge.BackColor = Color.Red
          StyleVert = .Add("Vert")
          StyleVert.BackColor = Color.LightGreen
        End If

        If IsNothing(StyleOrangé) Then
          StyleOrangé = .Add("Orangé")
          StyleOrangé.BackColor = Color.LightSalmon

        End If
      End With      ' fg.Styles

      'Adapter la taille de la grille au nombre de lignes de feux
      .Rows.Count = mesLignesFeux.Count + 1

      .Cols.Count = MAXPHASES + 1
      .DéfinirLargeurGrille()
      nbLignes = Math.Min(.Rows.Count, 15)
      .Height = (nbLignes + 0.3) * .Rows.DefaultSize
      Me.pnlTableauPhasage.Height = .Height + 40
    End With     ' fg

    AfficherEntetePhasage()

  End Sub

  Private Sub AfficherEntetePhasage()
    Dim uneLigneFeux As LigneFeux
    Dim row, col As Short
    Dim ID As String
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    For Each uneLigneFeux In mesLignesFeux
      row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
      ID = mesBranches.ID(uneLigneFeux.mBranche) & " - " & uneLigneFeux.ID

      'Mettre l'identifiant dans la 1ère colonne (fixe)
      fg(row, 0) = ID
    Next

  End Sub

  '*******************************************************************************
  ' Afficher le plan de feux actif
  '*******************************************************************************
  Private Sub AfficherPlanFeux(Optional ByVal unPlanFeux As PlanFeux = Nothing)
    If IsNothing(unPlanFeux) Then unPlanFeux = monPlanFeuxActif

    Dim desPhases As PhaseCollection = unPlanFeux.mPhases
    Dim unePhase As Phase
    Dim upd, updPhase1, updPhase2, updPhase3 As NumericUpDown
    Dim radVerrou1, radVerrou2, radVerrou3 As RadioButton
    Dim lbFigerDurée, lblPhase3 As Label
    Dim updDécalageOuverture, updDécalageFermeture As NumericUpDown
    Dim txtDuréeCycle As TextBox
    Dim PlanBase As Boolean = unPlanFeux Is monPlanFeuxBase


    Try

      If PlanBase Then
        updPhase1 = Me.updPhase1Base
        updPhase2 = Me.updPhase2Base
        updPhase3 = Me.updPhase3Base
        lbFigerDurée = Me.lbFigerDuréeBase
        radVerrou1 = Me.radPhase1Base
        radVerrou2 = Me.radPhase2Base
        radVerrou3 = Me.radPhase3Base
        lblPhase3 = Me.lblPhase3Base
        updDécalageOuverture = Me.updDécalageOuvertureBase
        updDécalageFermeture = Me.updDécalageFermetureBase
        txtDuréeCycle = Me.txtDuréeCycleBase

      Else
        updPhase1 = Me.updPhase1Fct
        updPhase2 = Me.updPhase2Fct
        updPhase3 = Me.updPhase3Fct
        lbFigerDurée = Me.lbFigerDuréeFct
        radVerrou1 = Me.radPhase1Fct
        radVerrou2 = Me.radPhase2Fct
        radVerrou3 = Me.radPhase3Fct
        lblPhase3 = Me.lblPhase3Fct
        updDécalageOuverture = Me.updDécalageOuvertureFct
        updDécalageFermeture = Me.updDécalageFermetureFct
        txtDuréeCycle = Me.txtDuréeCycleFct
        If unPlanFeux.CapacitéACalculer Then
          unPlanFeux.CalculerRéserveCapacité()
        End If
      End If

      AfficherTableauPlanFeux(unPlanFeux)

      'Verrouillage de phase inutile si 2 phases seulement
      Dim VisibilitéVerrou As Boolean = (desPhases.Count = 3)
      lbFigerDurée.Visible = VisibilitéVerrou
      radVerrou1.Visible = VisibilitéVerrou
      radVerrou2.Visible = VisibilitéVerrou
      radVerrou3.Visible = VisibilitéVerrou
      updPhase3.Visible = VisibilitéVerrou
      lblPhase3.Visible = VisibilitéVerrou

      For Each unePhase In desPhases
        Select Case desPhases.IndexOf(unePhase)
          Case 0
            upd = updPhase1
          Case 1
            upd = updPhase2
          Case 2
            upd = updPhase3
            radAssociéPhase(unePhase).Checked = True
        End Select

        upd.Tag = Nothing
        upd.Minimum = unePhase.DuréeIncompressible
        upd.Value = unePhase.Durée
        'Cette instruction doit être mise en dernier pour que l'évènement updPhase_ValueChanged ne fasse rien
        upd.Tag = unePhase
      Next

      updDécalageOuverture.Tag = unPlanFeux
      updDécalageFermeture.Tag = unPlanFeux
      updDécalageOuverture.Value = 0
      updDécalageFermeture.Value = 0
      txtDuréeCycle.Text = CStr(unPlanFeux.DuréeCycle)

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & "Affichage des plans de feux")
    End Try

  End Sub

  Private Sub RenommerColonnePlanFeux(ByVal uneLigneFeux As LigneFeux)
    Dim itmX As ListViewItem
    Dim lstItems As ListView.ListViewItemCollection

    If Not IsNothing(monPlanFeuxActif) Then
      Dim Index As Short = mLignesFeux.IndexOf(uneLigneFeux)
      If TypeOf monPlanFeuxActif Is PlanFeuxBase Then
        lstItems = Me.lvwDuréeVert.Items
      Else
        lstItems = Me.lvwDuréeVertFct.Items
      End If
      lstItems(Index).SubItems(0).Text = uneLigneFeux.ID & " (" & mesBranches.ID(uneLigneFeux.mBranche) & ")"

      If TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
        FenetreDiagnostic.RenommerColonnePlanFeux(uneLigneFeux, Index)
      End If
    End If

  End Sub

  Private Sub AfficherTableauPlanFeux(ByVal unPlanFeux As PlanFeux)
    Dim uneLigneFeux As LigneFeux
    Dim unePhase As Phase
    Dim lstItems As ListView.ListViewItemCollection
    Dim desPhases As PhaseCollection = unPlanFeux.mPhases
    Dim IDLigneFeux As String
    Dim itmX As ListViewItem

    Try

      If TypeOf unPlanFeux Is PlanFeuxBase Then
        lstItems = Me.lvwDuréeVert.Items
      Else
        lstItems = Me.lvwDuréeVertFct.Items
      End If

      lstItems.Clear()
      For Each uneLigneFeux In unPlanFeux.mLignesFeux
        For Each unePhase In desPhases
          If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
            If unPlanFeux.PositionDansPhase(uneLigneFeux, unePhase) <> PlanFeux.Position.Dernière Then
              IDLigneFeux = uneLigneFeux.ID & " (" & mesBranches.ID(uneLigneFeux.mBranche) & ")"
              itmX = New ListViewItem(New String() {IDLigneFeux, "1", "6", "0", "0"})

              With desPhases
                If unPlanFeux.PositionDansPhase(uneLigneFeux, unePhase) = PlanFeux.Position.Unique Then
                  itmX.SubItems(1).Text = CStr(.IndexOf(unePhase) + 1)
                Else
                  itmX.SubItems(1).Text = CStr(.IndexOf(unePhase) + 1) & " - " & CStr(.IndexOf(.PhaseSuivante(unePhase)) + 1)
                End If
              End With

              'A chaque affichage d'un nouveau plan de feux, _
              'il faut recalculer le vert de la ligne de feux(fonction des durées de phases et des décalages)

              itmX.SubItems(2).Text = unPlanFeux.DuréeVert(uneLigneFeux)

              If uneLigneFeux.EstVéhicule Then
                itmX.SubItems(3).Text = "X"
              Else
                itmX.SubItems(3).Text = unPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Ouverture)
              End If
              itmX.SubItems(4).Text = unPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Fermeture)
              itmX.Tag = uneLigneFeux
              Exit For
            End If
          End If
        Next
        lstItems.Add(itmX)
      Next


    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & "Affichage des plans de feux")
    End Try

  End Sub

  '**********************************************************************************************************************
  ' Gérer l'activation des boutons du panel lignes de Feux
  '**********************************************************************************************************************
  Private Sub ActiverBoutonsLignesFeux()

    Me.chkVerrouLignesFeux.Enabled = mesLignesFeux.nbLignesVéhicules >= IIf(ModeGraphique, mesBranches.NbLignesFeuxMini, 2)

  End Sub

  '******************************************************************************
  ' Redessiner le Diagramme du Plan de Feux
  '******************************************************************************
  Private Sub RedessinerDiagrammePlanFeux()

    If Not IsNothing(monPlanFeuxActif) And Not IsNothing(pnlPalette) Then
      If (Not IsNothing(mBitmapA)) Or (Not IsNothing(mBufferGraphics)) Then
        Me.picDessin.CreateGraphics.Clear(Me.picDessin.BackColor)
      End If

      If Not IsNothing(mBitmapA) Then
        mBitmapA.Dispose()
        mBitmapA = Nothing
      End If
      DrawPicture(Me.picDessin.CreateGraphics)
    End If

  End Sub

#End Region
#Region " Fonctions graphiques"
  '============================== Début des fonctions graphiques =====================================================
  '******************************************************************************
  ' Repeindre le graphique
  '******************************************************************************

  Private Sub TenterSuppressionObjet()

    If Not IsNothing(objSélect) Then
      SélDésélectionner(PourSélection:=True)
      Select Case UneCommandeGraphique
        Case CommandeGraphique.SupprimerPassage
          Me.btnPiétonMoins.PerformClick()
        Case CommandeGraphique.SupprimerTrajectoire
          Me.btnTrajectoireMoins.PerformClick()
        Case CommandeGraphique.SupprimerLigneFeu
          Me.btnLigneFeuxMoins.PerformClick()
      End Select
    End If

  End Sub

  Private Function RechercherPassage(ByVal p As Point) As PassagePiéton
    Dim uneBranche As Branche
    Dim unPassage As PassagePiéton

    For Each uneBranche In mesBranches
      unPassage = uneBranche.RecherPassage(p)
      If Not IsNothing(unPassage) Then Exit For
    Next

    If IsNothing(unPassage) Then
      AfficherMessageErreur(Me, "Désigner un passage piéton")
    Else
      Select Case UneCommandeGraphique
        Case CommandeGraphique.Traversée
          BrancheLiée = unPassage.mBranche
        Case CommandeGraphique.DécomposerTraversée, CommandeGraphique.PropTraversée
          Traversée = unPassage.mTraversée
      End Select
    End If

    Return unPassage

  End Function

  Private Function RechercherObject(ByVal p As Point) As Graphique
    Dim uneSélection As Graphique
    Dim unObjetMétier As Métier
    Dim fg As GrilleDiagfeux
    Dim numColonne As Short = 1

    'Traitement préalable sur l'objet préalablement sélectionné
    If Not IsNothing(objSélect) Then
      unObjetMétier = objSélect.ObjetMétier
      If TypeOf unObjetMétier Is PassagePiéton Then
        Dim unPassage As PassagePiéton = unObjetMétier
        If Not IsNothing(unPassage.Zebras) Then DessinerObjet(unPassage.Zebras)
      ElseIf TypeOf unObjetMétier Is Antagonisme Then
        fg = Me.AC1GrilleAntagonismes
        fg.Row = -1
      End If
    End If

    'Rechercher si un objet est sélectionné
    uneSélection = colObjetsGraphiques.RechercherObject(p, PointCliqué)

    If Not IsNothing(uneSélection) Then
      'Mettre en surbrillance dans la grille adéquate la ligne correspondant à l'objet sélectionné
      SelectObject = True
      unObjetMétier = uneSélection.ObjetMétier
      Dim Index As Short

      If TypeOf unObjetMétier Is Branche Then
        Dim uneBranche As Branche = unObjetMétier
        Index = mesBranches.IndexOf(uneBranche) + 1
        fg = Me.AC1GrilleBranches

      ElseIf TypeOf unObjetMétier Is Ilot Then
        Dim unIlot As Ilot = unObjetMétier
        Index = mesBranches.IndexIlot(unIlot)
        fg = Me.AC1GrilleIlot

      ElseIf TypeOf unObjetMétier Is LigneFeuVéhicules Then
        Dim uneLigneFeux As LigneFeuVéhicules = unObjetMétier
        Index = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        fg = Me.AC1GrilleFeux
      ElseIf TypeOf unObjetMétier Is TraverséePiétonne Then
        Dim uneLigneFeux As LigneFeux = CType(unObjetMétier, TraverséePiétonne).LigneFeu
        Index = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        fg = Me.AC1GrilleFeux

      ElseIf TypeOf unObjetMétier Is PassagePiéton Then
        Dim unPassage As PassagePiéton = unObjetMétier
        If Not IsNothing(unPassage.Zebras) Then EffacerObjet(unPassage.Zebras)
      ElseIf TypeOf unObjetMétier Is Antagonisme Then
        Dim unAntagonisme As Antagonisme = unObjetMétier
        'Pour les antagonismes comportant les mêmes courants, un seul est affiché dans la grille : c'est celui-ci qu'il faut rechercher
        Index = mAntagonismes.IndexOf(unAntagonisme.MêmesCourants) + 1
        numColonne = 2
        fg = Me.AC1GrilleAntagonismes

      End If

      If Not IsNothing(fg) Then
        Dim rg As Grille.CellRange = fg.GetCellRange(Index, numColonne)
        fg.Select(rg, True)
        fg.ShowCell(Index, numColonne)
        SelectObject = False
      End If

    End If

    Return uneSélection

  End Function

  '******************************************************************************
  ' Montre ou cache les poignées de sélection
  '******************************************************************************
  Private Sub SélDésélectionner(Optional ByVal PourSélection As Boolean = False)
    Dim Index As Short

    'DessinerPoignée(PointCliqué, ptCliqué:=True)

    Dim unObjetMétier As Métier = objSélect.ObjetMétier

    If Not IsNothing(unObjetMétier) Then
      Debug.WriteLine(unObjetMétier.GetType.FullName)
    End If
    Debug.WriteLine(PourSélection.ToString)

    If TypeOf unObjetMétier Is TrajectoireVéhicules Or TypeOf unObjetMétier Is Antagonisme Then
      EffacerObjet(objSélect)
      objSélect.Pointillable = PourSélection
      DessinerObjet(objSélect)
      If TypeOf unObjetMétier Is TrajectoireVéhicules Then
        objSélect = CType(unObjetMétier, TrajectoireVéhicules).PolyManuel
        If IsNothing(objSélect) Then objSélect = CType(unObjetMétier, TrajectoireVéhicules).LigneAccès
        SélDésélectionner(PourSélection)
        objSélect = unObjetMétier.mGraphique
      End If

    ElseIf TypeOf unObjetMétier Is Variante Then
      'Pas de poignées de sélection pour l'ensemble du carrefour
    Else
      For Index = 0 To objSélect.NbPoignées - 1
        DessinerPoignée(objSélect.Poignée(Index))
      Next
    End If

  End Sub

  '******************************************************************************
  ' Dessiner le curseur élastique sur le graphique
  '******************************************************************************
  Private Sub DessinerElastique(Optional ByVal Texte As String = Nothing)
    Dim i, nbPoints As Short

    If NePasEffacer Then
      NePasEffacer = False

    Else
      nbPoints = mScreen.Length
      Select Case UneCommandeGraphique
        Case CommandeGraphique.DéplacerCarrefour, CommandeGraphique.ZoomPAN
          Dim numBranche As Short
          For numBranche = 0 To maVariante.mBranches.Count - 1
            DessinerReversible(mScreen(4 * numBranche), mScreen(4 * numBranche + 1))
            DessinerReversible(mScreen(4 * numBranche + 2), mScreen(4 * numBranche + 3))
          Next

        Case CommandeGraphique.OrigineBranche, CommandeGraphique.AngleBranche, _
        CommandeGraphique.PositionTrafic, _
        CommandeGraphique.DéplacerLigneFeu, CommandeGraphique.AllongerFeu, _
        CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure, _
        CommandeGraphique.DéplacerNord, CommandeGraphique.OrienterNord, CommandeGraphique.DéplacerEchelle
          DessinerReversible(mScreen(0), mScreen(1))

        Case CommandeGraphique.EtirerIlot, CommandeGraphique.DéplacerIlot, CommandeGraphique.ElargirIlot, _
              CommandeGraphique.DéplacerPassage, _
              CommandeGraphique.DéplacerSignal
          Dim IndiceMax As Short = nbPoints - 1
          For i = 0 To IndiceMax
            DessinerReversible(mScreen(i), mScreen((i + 1) Mod nbPoints))
          Next
          If UneCommandeGraphique = CommandeGraphique.DéplacerSignal Then
            DessinerReversible(mScreen1, mScreen2)
          End If

        Case CommandeGraphique.EditerTrajectoire
          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            If Distance(mScreen1, mScreen2) <= RaySélect Then
              'Souris proche du point destination :allumer la poignée pour inciter à cliquer dessus pour terminer la commande
              DessinerPoignée(mPoint1)
            Else
              'Eteindre la poignée
              DessinerPoignée(mPoint1, True)
            End If
          End If
          Dim Indice As Short = mScreen.Length - 1
          If Indice = 2 Then
            'Pas encore de point manuel créé : segment entre le point origine et la souris
            DessinerReversible(mScreen(0), mScreen(1))
          Else
            'Dessiner les 2 segments reliant le point précédent avec la souris, et le point destination avec la souris
            For i = 0 To 1
              DessinerReversible(mScreen(Indice - i - 1), mScreen(Indice - i))
            Next
          End If

        Case CommandeGraphique.EditerPointTrajectoire
          For i = 0 To 1
            DessinerReversible(mScreen(i), mScreen(i + 1))
          Next

        Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire
          DessinerReversible(mScreen(0), mScreen(1))

        Case CommandeGraphique.EditAnglePassage
          DessinerReversible(mScreen(0), mScreen(1))
          DessinerReversible(mScreen(0), mScreen(2))
          DessinerReversible(mScreen(3), mScreen(2))
        Case CommandeGraphique.EditPointPassage, CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage
          DessinerReversible(mScreen(0), mScreen(1))
          DessinerReversible(mScreen(0), mScreen(2))
          DessinerReversible(mScreen(2), mScreen(3))
        Case CommandeGraphique.PassagePiéton, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          'If Not Texte = "paint" Then
          DessinerReversible(mScreen1, mScreen2)
          If mScreen.Length >= 3 Then ' Fin du passage piéton
            DessinerReversible(mScreen(0), mScreen2)
          End If

        Case Else
          If PourFrame Then DessinerFrame()
          If UnCarré Then

            'mScreen2.X = mScreen1.X + 10
            'mScreen2.Y = mScreen1.Y + 10
            DessinerFrame()
          End If
      End Select
    End If
  End Sub

  Private Sub DessinerReversible(ByVal p1 As Point, ByVal p2 As Point)
    ControlPaint.DrawReversibleLine(p1, p2, Color.Gray)
  End Sub

  '******************************************************************************
  ' Dessiner une frame élastique sur le graphique
  '******************************************************************************
  Private Sub DessinerFrame()
    Dim rc As New Rectangle
    Dim pScreen1, pScreen2 As Point

    'Définir le coin haut gauche du rectangle à partir des points M1 et M2
    If mScreen1.X < mScreen2.X Then
      rc.X = mScreen1.X
    Else
      rc.X = mScreen2.X
    End If
    If mScreen1.Y < mScreen2.Y Then
      rc.Y = mScreen1.Y
    Else
      rc.Y = mScreen2.Y
    End If
    'Définir la taille du rectangle à partir des points M1 et M2
    rc.Width = Math.Abs(mScreen2.X - mScreen1.X)
    rc.Height = Math.Abs(mScreen2.Y - mScreen1.Y)
    ' Dessin effectif
    ControlPaint.DrawReversibleFrame(rc, Color.Gray, FrameStyle.Dashed)

  End Sub

#Region " Actions Souris"
  '******************************************************************************
  ' MouseDown sur le graphique
  '******************************************************************************
  Private Sub picDessin_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDessin.MouseDown

    If DiagrammeActif() Then Exit Sub

    RéactiverSélect()
    Dim pSouris As New Point(e.X, e.Y)

    If UneCommandeGraphique = CommandeGraphique.Antagonisme Then DémarrerCommande(CommandeGraphique.AucuneCommande)

    Select Case UneCommandeGraphique

      Case CommandeGraphique.AucuneCommande

        If IsNothing(objSélect) Then
          objSélect = RechercherObject(pSouris)
        Else
          SélDésélectionner(PourSélection:=False)  ' Montre ou cache les poignées de sélection
          Dim NewObject As Graphique = RechercherObject(pSouris)
          If Not NewObject Is objSélect Then
            objSélect = NewObject
          End If
        End If

        If Not IsNothing(objSélect) Then
          SélDésélectionner(PourSélection:=True)

          If mPoint.Length = 0 Then
            If TypeOf objSélect.ObjetMétier Is Antagonisme Then
              DémarrerCommande(CommandeGraphique.Antagonisme)
            End If
          Else
            'La souris a déjà survolé un point permettant l'exécution d'une commande
            InitialiserCommande(pSouris)
            '          DémarrerDrag
            mDragging = True
            Select Case UneCommandeGraphique
              Case CommandeGraphique.AllongerFeu, CommandeGraphique.DéplacerLigneFeu, CommandeGraphique.DéplacerSignal
                Désélectionner()
                If UneCommandeGraphique = CommandeGraphique.DéplacerSignal Then
                  EffacerObjet(SignalFeuEnCours.mGraphique)
                Else
                  EffacerObjet(LigneFeuEnCours.Dessin)
                  EffacerObjet(LigneFeuEnCours.mSignalFeu(0).mGraphique)
                End If
            End Select
            DessinerElastique()
          End If
        End If

      Case CommandeGraphique.PassagePiéton, CommandeGraphique.Mesure
        If mPoint.Length = 0 Then
          ' Création du passage : l'utilisateur a juste déclenché la commande avec btnPassage
          ' Outil Mesure : l'outil attend le point de référence
          EnAttenteMouseUp = True
        End If


      Case CommandeGraphique.Trajectoire
        EnAttenteMouseUp = True
        If Not IsNothing(objSélect) Then
          SélDésélectionner(PourSélection:=True)
          objSélect = RechercherObject(pSouris)
        End If
      Case CommandeGraphique.LigneFeux, CommandeGraphique.PassagePiétonRapide
        EnAttenteMouseUp = True

      Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu
        objSélect = RechercherObject(pSouris)
        TenterSuppressionObjet()

      Case CommandeGraphique.Traversée, CommandeGraphique.DécomposerTraversée, CommandeGraphique.PropTraversée
        Dim unPassage As PassagePiéton = RechercherPassage(pSouris)

        If Not IsNothing(unPassage) Then TerminerCommande(pSouris)
        DémarrerCommande(CommandeGraphique.AucuneCommande)

      Case CommandeGraphique.PropTrajectoire
        objSélect = RechercherObject(pSouris)
        Me.btnTrajProp.PerformClick()

      Case CommandeGraphique.ZoomPAN
        If mPoint.Length = 0 Then
          InitialiserCommande(pSouris)
          mDragging = True
          DessinerElastique()
        End If
    End Select

  End Sub

  '******************************************************************************
  ' MouseMove sur le graphique
  '******************************************************************************
  Private Sub picDessin_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDessin.MouseMove

    If DiagrammeActif() Then Exit Sub

    RéactiverSélect()

    ' Mémoriser le nouveau point
    Dim pSouris As Point = New Point(e.X, e.Y)
#If DEBUG Then
    Me.Label1.Text = PointRéel(pSouris).ToString
    Me.Label2.Text = pSouris.ToString
    Me.Label3.Text = pSouris.ToString
    Me.Label4.Text = pSouris.ToString
#End If
    Dim pF As PointF = PointRéel(pSouris)
    If Not IsNothing(monFDP) Then
      mdiApplication.staDiagfeux.Panels(1).Text = "X = " & Format(pF.X, "0.##") & ", Y = " & Format(pF.Y, "0.##")
    End If

    If mDragging Then
      'l 'opération de glissage a déjà commencé
      If Not PointDansPicture(pSouris) Then Exit Sub
      ' Effacer la ligne précédente
      DessinerElastique("Move")

      Try
        Select Case UneCommandeGraphique
          Case CommandeGraphique.DéplacerNord, CommandeGraphique.DéplacerEchelle
            TranslaterMscreen(pSouris)
          Case CommandeGraphique.OrienterNord
            mScreen(1) = RecalculermScreen(CentreRotation, pSouris, LongueurSegment)

          Case CommandeGraphique.OrigineBranche
            If OrigineBrancheOK(pSouris) Then
              TranslaterMscreen(pSouris)
            End If

          Case CommandeGraphique.DéplacerPassage, CommandeGraphique.DéplacerCarrefour, CommandeGraphique.ZoomPAN
            mScreen(0) = RecalculermScreen(pSouris)
          Case CommandeGraphique.AllongerFeu, CommandeGraphique.Mesure
            mScreen(1) = RecalculermScreen(pSouris)
            mdiApplication.staDiagfeux.Panels(1).Text = Format(DistanceRéelle(mScreen(0), mScreen(1)), "0.##") & " m"

          Case CommandeGraphique.DéplacerSignal
            TranslaterMscreen(pSouris)
          Case CommandeGraphique.AngleBranche
            If AngleBrancheOK(pSouris) Then
              mScreen(1) = RecalculermScreen(CentreRotation, pSouris, LongueurSegment)
            End If

          Case CommandeGraphique.PassagePiéton, CommandeGraphique.LigneFeux
            mScreen2 = RecalculermScreen(pSouris)
          Case CommandeGraphique.Trajectoire
            mScreen2 = RecalculermScreen(pSouris)
          Case CommandeGraphique.EtirerIlot, CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage
            mScreen(0) = RecalculermScreen(pSouris)
          Case CommandeGraphique.EditPointPassage
            mScreen(0) = RecalculermScreen(pSouris)

          Case CommandeGraphique.ElargirIlot
            mScreen(1) = RecalculermScreen(pSouris)
            mScreen(2) = Symétrique(mScreen(1), mScreen(4))
          Case CommandeGraphique.DéplacerIlot
            If P2IlotOK(pSouris) Then
              RecalculerMscreenIlot(pSouris)
            End If
          Case CommandeGraphique.EditerTrajectoire
            'Mémoriser la nouvelle position de la souris (pour DessinerElastique)
            mScreen1 = RecalculermScreen(pSouris)
            mScreen(mScreen.Length - 2) = mScreen1
          Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire
            'Mémoriser la nouvelle position de la souris (pour DessinerElastique)
            mScreen1 = RecalculermScreen(pSouris)
            mScreen(mScreen.Length - 2) = mScreen1

        End Select

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

      'Dessiner la nouvelle ligne
      DessinerElastique()

    Else  ' not mDragging
      If UneCommandeGraphique = CommandeGraphique.AucuneCommande Then
        Dim CommandePossible As Boolean
        'Rechercher si un objet est sélectionné
        Dim unObjetSurvolé, objSélectEncours As Graphique
        unObjetSurvolé = colObjetsGraphiques.RechercherObject(pSouris, PointCliqué)
        If IsNothing(unObjetSurvolé) Then
          TraiterMessageGlisser()
          ReDim mPoint(-1)
        Else
          objSélectEncours = objSélect
          objSélect = unObjetSurvolé
          If Not InitialiserCommande(pSouris) Then ReDim mPoint(-1)
          objSélect = objSélectEncours
          'picDessin.Cursor = CurseurCommande()
          UneCommandeGraphique = CommandeGraphique.AucuneCommande
        End If
      End If

    End If  'mDragging

  End Sub

  '******************************************************************************
  ' MouseUp sur le graphique
  '******************************************************************************
  Private Sub picDessin_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDessin.MouseUp

    Dim pEncours As Point = New Point(e.X, e.Y)

    Dim FinCommande As Boolean

    If mPoint.Length > 0 Then

      Select Case UneCommandeGraphique
        Case CommandeGraphique.OrigineBranche, CommandeGraphique.AngleBranche, CommandeGraphique.PassagePiéton, _
            CommandeGraphique.EtirerIlot, CommandeGraphique.DéplacerIlot, CommandeGraphique.ElargirIlot, _
            CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage, CommandeGraphique.DéplacerPassage, _
            CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.DéplacerLigneFeu, _
            CommandeGraphique.AllongerFeu, CommandeGraphique.DéplacerSignal, CommandeGraphique.DéplacerCarrefour, _
              CommandeGraphique.EditerTrajectoire, CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire, _
              CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure, _
              CommandeGraphique.DéplacerNord, CommandeGraphique.OrienterNord, CommandeGraphique.DéplacerEchelle

          FinCommande = TerminerCommande(pEncours)
      End Select

      If FinCommande Then
        Select Case UneCommandeGraphique
          Case CommandeGraphique.LigneFeux, CommandeGraphique.Trajectoire, CommandeGraphique.PassagePiéton
            DémarrerCommande(UneCommandeGraphique, Continuation:=True)
          Case Else
            DémarrerCommande(CommandeGraphique.AucuneCommande)
        End Select
      End If

    Else
      'MouseUp aussitot MouseDown pour une sélection : Il faut désactiver DémarrerDrag
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure
          If EnAttenteMouseUp Then  'Validation du 1er point
            EnAttenteMouseUp = False
            If InitialiserCommande(pEncours) Then
              mDragging = True
              DessinerElastique()
              If UneCommandeGraphique = CommandeGraphique.Trajectoire Then
                mScreen1 = mScreen2
              ElseIf UneCommandeGraphique = CommandeGraphique.PassagePiétonRapide Then
                TerminerCommande(pEncours)
                'Pour la commande passage piéton rapide, on pourrait analyser s'il reste une branche sans passage
                DémarrerCommande(UneCommandeGraphique, Continuation:=True)
              End If
            End If
          End If

        Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPAN
          TerminerCommande(pEncours)
          DémarrerCommande(CommandeGraphique.AucuneCommande)

        Case Else
          'MouseUp aussitot MouseDown pour une sélection : Il faut désactiver DémarrerDrag
          If Not IsNothing(objSélect) Then
            mDragging = False
          Else
          End If

      End Select
    End If

  End Sub

  Private Sub picDessin_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picDessin.MouseLeave
    mdiApplication.staDiagfeux.Panels(1).Text = ""
  End Sub
  '******************************************************************************
  ' Resize du graphique
  '******************************************************************************
  Private Sub picDessin_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles picDessin.Resize
    AffecterLimites(picDessin)
    If UneCommandeGraphique <> CommandeGraphique.AucuneCommande Then
      DémarrerCommande(CommandeGraphique.AucuneCommande)
    End If
    objSélect = Nothing
  End Sub

  '******************************************************************************
  ' MouseDown ou MouseMove sur le graphique
  '******************************************************************************
  Private Sub RéactiverSélect()
    If Not IsNothing(savObjSélect) Then
      objSélect = savObjSélect
      savObjSélect = Nothing
      SélDésélectionner()   ' Montre ou cache les poignées de sélection
    End If

  End Sub

  '******************************************************************************
  ' Paint sur picDessin
  '******************************************************************************
  Private Sub picDessin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) _
  Handles picDessin.Paint

    If Not DessinEnCours Then
      DrawPicture(e.Graphics)
    End If

  End Sub

  Private Sub picDessin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles picDessin.DoubleClick
    If DiagrammeActif() Then Exit Sub

    ' Les évènements déclenchés sont : MouseDown, Click, MouseUp,DoubleClick et enfin MouseUp  à nouveau
    If TypeOf objSélect Is PolyArc Then
      ' Commande d'édition d'objet
      Dim unPolyArc As PolyArc = objSélect
      Dim objetMétier As Métier = objSélect.ObjetMétier

      If TypeOf objetMétier Is TrajectoireVéhicules Then
        EnAttenteMouseUp = True
        btnTrajProp.PerformClick()
      ElseIf TypeOf objetMétier Is TraverséePiétonne Then
        Me.btnTravProp.PerformClick()
      End If

    ElseIf TypeOf objSélect Is Cercle Then
      Dim unCercle As Cercle = objSélect
      Dim objetmétier As Métier = objSélect.ObjetMétier

    End If
  End Sub

#End Region

#Region "RecalculsMscreen"
  '**************************************************************************************
  ' Translation de l'ensemble de l'ilot
  '**************************************************************************************
  Private Sub RecalculerMscreenIlot(ByVal pBase As Point)
    'pBase correspond à P5, sommet de l'arc
    Dim p1 As Point = Translation(pBase, DecalV(0))
    Dim p3 As Point = Translation(pBase, DecalV(1))
    Dim p4 As Point = Translation(pBase, DecalV(2))
    If BrancheLiée.PtIntérieur(p3) Then
      mScreen(0) = PtClippé(p1, p3)
      mScreen(1) = PtClippé(p3, pBase)
      mScreen(2) = PtClippé(p4, pBase)
      mScreen(3) = PtClippé(p1, p4)
    End If

  End Sub

  '**************************************************************************************
  ' Translation de l'ensemble du passage
  '**************************************************************************************
  Private Sub TranslaterMscreen(ByVal pBase As Point)
    'pBase correspond à
    ' Branche : point cliqué au départ du glissement
    ' Passage piéton (Carrefour) :  point intérieur du passage(du carrefour) qui commande l'ensemble du déplacement (point cliqué initial en train de glisser)
    ' Signal de feu :   1er point de la boite

    Dim i As Short
    Dim p(mScreen.Length - 1) As Point

    For i = 0 To mScreen.Length - 1
      p(i) = Translation(pBase, DecalV(i))
    Next

    For i = 0 To mScreen.Length - 1 Step 2
      If Not PointDansPicture(p(i)) And Not PointDansPicture(p(i + 1)) Then p(i + 1) = p(i)
    Next
    For i = 0 To mScreen.Length - 1
      mScreen(i) = PtClippé(p(i), p(IIf((i Mod 2) = 0, i + 1, i - 1)))
    Next

    If UneCommandeGraphique = CommandeGraphique.DéplacerSignal Then mScreen1 = picDessin.PointToScreen(pBase)
  End Sub

  Private Sub RecalculerMscreenLigneFeux(ByVal pBase As Point)
    mScreen(1) = TranslatéClippé(pBase, DecalV(0), pBase)
  End Sub

  '**************************************************************************************
  ' Recalculer mScreenx avec le décalage adéquat par rapport au point cliqué :Translation
  '**************************************************************************************
  Private Function TranslatéClippé(ByVal pBase As Point, ByVal unVecteur As Vecteur, ByVal pBase2 As Point) As Point
    Dim p As Point = Translation(pBase, unVecteur)

    Return PtClippé(p, pBase2)

  End Function

  '**************************************************************************************
  ' Recalculer mScreenx avec le décalage adéquat par rapport au point cliqué :Rotation
  '**************************************************************************************
  Private Function RecalculermScreen(ByVal pCentre As Point, ByVal pBase As Point, ByVal Dist As Single) As Point
    Dim p As Point = PointPosition(pCentre, Dist, CType(AngleFormé(pCentre, pBase), Single))

    Return PtClippé(p, pCentre)

  End Function

  '**************************************************************************************
  ' Recalculer mScreenx avec un calcul spécifique si le point en cours est le dernier
  '**************************************************************************************
  Private Function RecalculermScreen(ByVal pSouris As Point) As Point
    Dim Continuer As Boolean
    Dim p As Point
    Dim pScreen As Point = Control.MousePosition

    Select Case UneCommandeGraphique
      Case CommandeGraphique.DéplacerCarrefour, CommandeGraphique.ZoomPAN
        p = pSouris
        TranslaterMscreen(p)
        pScreen = mScreen(0)

      Case CommandeGraphique.Mesure
        If PointDansPicture(pSouris) Then
          pScreen = picDessin.PointToScreen(pSouris)
          p = pScreen
        Else
          pScreen = mScreen(1)
        End If

      Case CommandeGraphique.PassagePiéton
        p = PtPassage(pSouris)
        If p.IsEmpty Then
          'Conserver l'ancien point car le tracé part en sens opposé(ignorer le point)
          pScreen = mScreen2
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

      Case CommandeGraphique.Trajectoire
        p = PtTrajectoire(pSouris)
        pScreen = picDessin.PointToScreen(pSouris)

      Case CommandeGraphique.LigneFeux
        p = PtLigneFeux(pSouris)
        If p.IsEmpty Then
          'Conserver l'ancien point car le tracé part en sens opposé(ignorer le point)
          pScreen = mScreen2
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

      Case CommandeGraphique.DéplacerLigneFeu, CommandeGraphique.AllongerFeu
        p = PtLigneFeuDéplacé(pSouris)
        If p.IsEmpty Then
          pScreen = mScreen(1)
        Else
          pScreen = picDessin.PointToScreen(p)
          If UneCommandeGraphique = CommandeGraphique.DéplacerLigneFeu Then
            mScreen(0) = TranslatéClippé(p, DecalV(0), p)
          End If
        End If

      Case CommandeGraphique.EditerTrajectoire, CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire
        p = PtEditTrajectoire(pSouris)
        If p.IsEmpty Then
          pScreen = mScreen1
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

      Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage
        If UneCommandeGraphique = CommandeGraphique.EditLongueurPassage Then
          p = EditPassageOK4(pSouris)
        ElseIf UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
          p = EditPassageOK4(pSouris)
        ElseIf UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Then
          p = EditPassageOK4(pSouris)
        Else
          p = EditPassageOK3(pSouris)
        End If

        If p.IsEmpty Then
          'Conserver l'ancien point car le passage sort de la branche (ignorer le point)
          pScreen = mScreen(0)
        ElseIf UneCommandeGraphique = CommandeGraphique.EditAnglePassage Then
          pScreen = picDessin.PointToScreen(p)
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

      Case CommandeGraphique.DéplacerPassage
        p = EditPassageOK2(pSouris)
        If p.IsEmpty Then
          'Conserver l'ancien point car le passage sort de la branche (ignorer le point)
          pScreen = mScreen(0)
        Else
          TranslaterMscreen(p)
          pScreen = mScreen(0)
        End If

      Case CommandeGraphique.EtirerIlot, CommandeGraphique.ElargirIlot
        p = PtIlot(pSouris)
        If p.IsEmpty Then
          'Conserver l'ancien point car l'ilot dépasse la limite (ignorer le point)
          pScreen = mScreen(0)
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

    End Select

    GérerCurseur(Not p.IsEmpty)

    Return pScreen

  End Function

  '**************************************************************************************
  ' Nouveau point sur la trajectoire
  ' ou Modification du point d'accès au carrefour de la trajectoire
  '**************************************************************************************
  Private Function PtEditTrajectoire(ByVal pSouris As Point) As Point

    Select Case UneCommandeGraphique
      Case CommandeGraphique.EditerTrajectoire, CommandeGraphique.EditerPointTrajectoire
        'Edition de la trajectoire point par point
        If mesBranches.EnveloppeCarrefour.Intérieur(pSouris) Then
          Return pSouris
        End If

      Case Else
        'Modification de l'accès
        Dim p As Point = Projection(pSouris, Segment1)
        If Not Segment1.PtSurSegment(p) Then
          Return p
        End If

    End Select

  End Function
#End Region
#Region "InitialiserCommande"
  '**************************************************************************************
  ' Initialiser une commande graphique
  '**************************************************************************************
  Private Function InitialiserCommande(ByVal pEnCours As Point) As Boolean
    Dim PointProche As Point
    Dim numPoignée As Short
    ' Traiter d'abord les cas où la commande a été définie par le programme (Création d'objets - ZoomPAN - Edition manuelle de trajectoire)

    Try

      Select Case UneCommandeGraphique
        ' Traiter d'abord les cas où la commande a été définie par le programme (Création d'objets - ZoomPAN )
      Case CommandeGraphique.ZoomPAN
          DéfinirPointsCarrefour(pEnCours)
          InitialiserCommande = True

        Case CommandeGraphique.Mesure
          InitialiserCommande = True
          ReDim mPoint(0)
          ReDim mScreen(1)
          mPoint(0) = pEnCours
          mScreen(0) = picDessin.PointToScreen(mPoint(0))
          mScreen(1) = mScreen(0)

        Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          'Création d'objets
          Dim pIntéressant As Point = pEnCours
          BrancheLiée = BrancheProche(pIntéressant)
          If UneCommandeGraphique = CommandeGraphique.PassagePiéton Or UneCommandeGraphique = CommandeGraphique.PassagePiétonRapide Then
            If IsNothing(BrancheLiée) Then
              AfficherMessageErreur(Me, "Cliquer un point sur un bord de chaussée")

            ElseIf BrancheLiée.mPassages.Count = 2 Then
              AfficherMessageErreur(Me, "Cette branche comporte déjà 2 passages piétons")

            ElseIf BrancheLiée.mPassages.Count = 1 Then
              If UneCommandeGraphique = CommandeGraphique.PassagePiétonRapide Then
                AfficherMessageErreur(Me, "Cette branche comporte déjà 1 passage piéton")
              ElseIf (BrancheLiée.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Or BrancheLiée.SensUnique(Voie.TypeVoieEnum.VoieSortante)) Then
                'Si ce controle devait disparaitre, il faudrait revoir la fonction SignalFeu.PtRéférence
                AfficherMessageErreur(Me, "Cette branche à sens unique comporte déjà 1 passage piéton")
              Else
                InitialiserCommande = True
              End If

            Else
              InitialiserCommande = True
            End If

            If InitialiserCommande Then
              ReDim mPoint(0)
              ReDim mScreen(0)
              mPoint(0) = pIntéressant
            End If

          Else    ' Trajectoire véhicules ou ligne de feux véhicules
            If IsNothing(VoieTraj) Then
              AfficherMessageErreur(Me, "Cliquer entre les 2 bords d'une voie")
            ElseIf Not VoieTraj.Entrante Then
              AfficherMessageErreur(Me, "Désigner une voie entrante")
            Else
              InitialiserCommande = True
              ReDim mPoint(1)
              ReDim mScreen(1)
              If UneCommandeGraphique = CommandeGraphique.Trajectoire Then
                mPoint(0) = VoieTraj.MilieuExtrémité(Voie.ExtrémitéEnum.Extérieur)
                mPoint(1) = VoieTraj.MilieuExtrémité(Voie.ExtrémitéEnum.Intérieur)
                mPoint(0) = PtClippé(mPoint(0), mPoint(1), CoordonnéesEcran:=False)
              Else
                Dim p1, p2 As Point
                p1 = Projection(pEnCours, VoieTraj.Bordure(Branche.Latéralité.Droite))
                p2 = Projection(pEnCours, VoieTraj.Bordure(Branche.Latéralité.Gauche))
                If Distance(pEnCours, p1) < Distance(pEnCours, p2) Then
                  mPoint(0) = p1
                Else
                  mPoint(0) = p2
                End If
                mPoint(1) = mPoint(0)
                AngleProjection = BrancheLiée.AngleEnRadians + PI / 2
                ContourPermis = BrancheLiée.EnveloppeVoiesEntrantes
              End If
            End If
          End If

          If InitialiserCommande Then
            mScreen1 = picDessin.PointToScreen(mPoint(0))
            mScreen(0) = mScreen1
            If UneCommandeGraphique = CommandeGraphique.PassagePiéton Then
              mScreen2 = mScreen1
            ElseIf UneCommandeGraphique = CommandeGraphique.PassagePiétonRapide Then
              ReDim mPoint(3)

            Else
              mScreen2 = picDessin.PointToScreen(mPoint(1))
              mScreen(1) = mScreen2
            End If
          End If

        Case Else

          If TypeOf objSélect Is PolyArc AndAlso CType(objSélect, PolyArc).Editable Then
            ' Commande d'édition d'objet
            Dim unPolyArc As PolyArc = objSélect
            Dim objetMétier As Métier = objSélect.ObjetMétier

            If TypeOf objetMétier Is Variante Then
              UneCommandeGraphique = CommandeGraphique.DéplacerCarrefour
              DéfinirPointsCarrefour(pEnCours)
              InitialiserCommande = True

            ElseIf TypeOf objetMétier Is Nord Then
              InitialiserCommande = DéfinirPointsNord(objetMétier, pEnCours)

            ElseIf TypeOf objetMétier Is SymEchelle Then
              InitialiserCommande = DéfinirPointsEchelle(objetMétier, pEnCours)

            ElseIf TypeOf objetMétier Is Branche Then
              Dim uneBranche As Branche = objetMétier
              UneCommandeGraphique = uneBranche.MouvementPossible(pEnCours)
              If UneCommandeGraphique <> CommandeGraphique.AucuneCommande Then
                DéfinirPointsBranche(uneBranche)
                InitialiserCommande = True
              End If

            ElseIf TypeOf objetMétier Is Ilot Then
              Dim unIlot As Ilot = objetMétier
              unPolyArc = objSélect
              For numPoignée = 0 To unPolyArc.NbPoignées - 1
                If Distance(PointCliqué, unPolyArc.Poignée(numPoignée)) < RaySélect Then
                  DéfinirPointsIlots(unPolyArc, numPoignée, Nothing)
                  InitialiserCommande = True
                  Exit For
                End If
              Next
              If Not InitialiserCommande Then
                Dim ContourUtile As PolyArc = CType(unPolyArc(4), PolyArc)
                If ContourUtile.Intérieur(PointCliqué) Then
                  DéfinirPointsIlots(unPolyArc, numPoignée:=3, pRef:=PointCliqué)
                  UneCommandeGraphique = CommandeGraphique.DéplacerIlot
                  InitialiserCommande = True
                End If
              End If

            ElseIf TypeOf objetMétier Is PassagePiéton Then
              unPolyArc = objSélect
              Dim unPassage As PassagePiéton = CType(objetMétier, PassagePiéton)

              UneCommandeGraphique = unPassage.MouvementPossible(PointCliqué, numPoignée)
              If PasPassage = 2 AndAlso numPoignée Mod 2 = 1 Then
                'Clique sur une poignée au milieu d'un segment
                PoignéeCliquée = ((numPoignée + 1) / PasPassage) Mod 4
              Else
                'Clique sur un point proche d'un coin du passage
                PoignéeCliquée = numPoignée / PasPassage
              End If

              Select Case UneCommandeGraphique
                Case CommandeGraphique.EditPointPassage
                  DéfinirPointsEditPassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.EditAnglePassage
                  DéfinirPointsEditAnglePassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.DéplacerPassage
                  DéfinirPointsPassage(unPassage, PointCliqué)
                  InitialiserCommande = True
                Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage
                  DéfinirPointsEditDimensionPassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.EditLongueurPassage
                  InitialiserCommande = True
              End Select

              ''If Not InitialiserCommande Then
              ''  'Faire une recherche sur les cotés parallèles du passage (les 'grands' cotés)
              ''  InitialiserCommande = DéfinirPointsPassageParallèle(unPassage, PointCliqué)
              ''End If

            ElseIf TypeOf objetMétier Is LigneFeuVéhicules Then
              Dim uneLigneFeux As LigneFeuVéhicules = objetMétier
              Dim uneLigne As Ligne = uneLigneFeux.Dessin

              If Distance(PointCliqué, uneLigne.pA) < Distance(PointCliqué, uneLigne.pB) Then
                PointProche = uneLigne.pA
                UneCommandeGraphique = CommandeGraphique.DéplacerLigneFeu
              Else
                PointProche = uneLigne.pB
                UneCommandeGraphique = CommandeGraphique.AllongerFeu
              End If

              If Distance(PointProche, PointCliqué) < RaySélect Then
                DéfinirPointsLigneFeux(uneLigneFeux)
                InitialiserCommande = True
              Else
                UneCommandeGraphique = CommandeGraphique.AucuneCommande
              End If

            ElseIf TypeOf objetMétier Is SignalFeu Then
              Dim unSignalFeu As SignalFeu = objetMétier
              DéfinirPointsSignalFeux(unSignalFeu, pEnCours)
              InitialiserCommande = True
              UneCommandeGraphique = CommandeGraphique.DéplacerSignal

            ElseIf TypeOf objetMétier Is TrajectoireVéhicules Then
              Dim uneTrajectoire As TrajectoireVéhicules = objetMétier
              UneCommandeGraphique = uneTrajectoire.MouvementPossible(pEnCours, PoignéeCliquée)
              Select Case UneCommandeGraphique
                Case CommandeGraphique.EditerPointTrajectoire
                  DéfinirPointIntermédiaire(uneTrajectoire)
                  InitialiserCommande = True
                Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire
                  DéfinirPointsAccès(uneTrajectoire)
                  InitialiserCommande = True
              End Select
            End If

          End If

      End Select

      TraiterMessageGlisser()

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      DémarrerCommande(CommandeGraphique.AucuneCommande)
      InitialiserCommande = False
    End Try


  End Function
#Region "DéfinirPoints"
  Private Sub DéfinirPointsAccès(ByVal uneTrajectoire As TrajectoireVéhicules)
    ReDim mPoint(0)
    ReDim mScreen(1)

    With uneTrajectoire
      Select Case UneCommandeGraphique
        Case CommandeGraphique.EditerOrigineTrajectoire
          Segment1 = .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Origine)
        Case CommandeGraphique.EditerDestinationTrajectoire
          Segment1 = .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Destination)
      End Select
    End With

    mPoint(0) = Segment1.pA
    mScreen(0) = picDessin.PointToScreen(mPoint(0))
    'mScreen1 : point mobile
    mScreen1 = mScreen(0)
    'AU départ, les 2 points de l'élastique sont confondus - mScreen(1) restera fixe
    mScreen(1) = mScreen(0)

  End Sub

  Private Sub DéfinirPointIntermédiaire(ByVal uneTrajectoire As TrajectoireVéhicules)
    ReDim mPoint(0)
    ReDim mScreen(2)

    With uneTrajectoire.PolyManuel
      mPoint(0) = CvPoint(.Points(PoignéeCliquée - 1))
      mScreen(0) = picDessin.PointToScreen(mPoint(0))
      mScreen(1) = picDessin.PointToScreen(CvPoint(.Points(PoignéeCliquée)))
      mScreen(2) = picDessin.PointToScreen(CvPoint(.Points(PoignéeCliquée + 1)))
      'mScreen1 : point mobile
      mScreen1 = mScreen(1)
    End With

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la gestion d'une commande graphique relative aux lignes de feux
  '**************************************************************************************
  Private Sub DéfinirPointsSignalFeux(ByVal unSignalFeu As SignalFeu, ByVal pEnCours As Point)
    ReDim mPoint(3)
    ReDim mScreen(3)
    ReDim DecalV(3)
    Dim i As Short

    SignalFeuEnCours = unSignalFeu
    Dim uneBoite As Boite = unSignalFeu.mGraphique(0)
    For i = 0 To 3
      mPoint(i) = CvPoint(uneBoite.Points(i))
      mScreen(i) = picDessin.PointToScreen(mPoint(i))
    Next

    'DecalV(0) = New Vecteur(mPoint(1).X - mPoint(0).X, mPoint(1).Y - mPoint(0).Y)
    'DecalV(1) = New Vecteur(mPoint(2).X - mPoint(0).X, mPoint(2).Y - mPoint(0).Y)
    'DecalV(2) = New Vecteur(mPoint(3).X - mPoint(0).X, mPoint(3).Y - mPoint(0).Y)

    DecalV(0) = New Vecteur(-8, -8)
    DecalV(1) = New Vecteur(-8, 8)
    DecalV(2) = New Vecteur(8, 8)
    DecalV(3) = New Vecteur(8, -8)

    Dim pRéférence As Point
    If unSignalFeu.mLigneFeux.EstVéhicule Then
      pRéférence = CType(unSignalFeu.mLigneFeux.mGraphique(0), Ligne).Milieu
    Else
      pRéférence = unSignalFeu.PtRéférence
    End If

    mScreen1 = picDessin.PointToScreen(pEnCours)
    mScreen2 = picDessin.PointToScreen(pRéférence)

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la gestion d'une commande graphique relative aux lignes de feux
  '**************************************************************************************
  Private Sub DéfinirPointsLigneFeux(ByVal uneLigneFeux As LigneFeuVéhicules)
    ReDim mPoint(1)
    ReDim mScreen(1)
    ReDim DecalV(0)
    Dim i As Short

    LigneFeuEnCours = uneLigneFeux
    Dim uneLigne As Ligne = LigneFeuEnCours.Dessin

    BrancheLiée = LigneFeuEnCours.mBranche
    mPoint(0) = uneLigne.pA
    mPoint(1) = uneLigne.pB
    For i = 0 To 1
      mScreen(i) = picDessin.PointToScreen(mPoint(i))
    Next

    If UneCommandeGraphique = CommandeGraphique.DéplacerLigneFeu Then
      'Bord de la voie la plus à gauche
      Segment1 = LigneFeuEnCours.BordVoie(1)
      DecalV(0) = New Vecteur(uneLigne)
      AngleProjection = BrancheLiée.AngleEnRadians
    Else
      mScreen(0) = picDessin.PointToScreen(uneLigne.pA)
      mScreen(1) = picDessin.PointToScreen(uneLigne.pB)
      Dim p(1) As Point
      With BrancheLiée
        p(0) = Projection(mPoint(0), .BordVoiesEntrantes(Branche.Latéralité.Droite))
        p(1) = Projection(mPoint(1), .BordVoiesEntrantes(Branche.Latéralité.Gauche))
      End With
      Segment1 = New Ligne(p(0), p(1))
      AngleProjection = BrancheLiée.AngleEnRadians + PI / 2
      If AngleProjection > PI Then AngleProjection -= 2 * PI
    End If

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la gestion d'une commande graphique relative aux branches
  '**************************************************************************************
  Private Function DéfinirPointsBranche(ByVal uneBranche As Branche) As Boolean
    ReDim mPoint(1)
    ReDim mScreen(1)
    ReDim DecalV(1)

    Dim BranchePrécédente As Branche = mesBranches.Précédente(uneBranche)
    Dim BrancheSuivante As Branche = mesBranches.Suivante(uneBranche)
    Dim p(3) As Point

    Dim unePlumeRouge As Pen = New Pen(Color.Red, 2)
    Dim unePlumeVerte As Pen = New Pen(Color.Green, 2)
    Dim unePlumeBleue As Pen = New Pen(Color.Blue, 2)

    Dim l1 As Ligne
    Dim l2 As Ligne

    With uneBranche.LigneDeSymétrie
      mPoint(0) = .pA
      mPoint(1) = .pB
      If UneCommandeGraphique = CommandeGraphique.OrigineBranche Then
        DecalV(0) = New Vecteur(0, 0)
        DecalV(1) = New Vecteur(uneBranche.LigneDeSymétrie)

        Segment1 = BranchePrécédente.BordChaussée(Branche.Latéralité.Gauche)
        Segment2 = BrancheSuivante.BordChaussée(Branche.Latéralité.Droite)

        'Lignes suivantes ajoutées suite aux raccords de branche
        Segment1.pA = BranchePrécédente.ExtrémitéBordChaussée(Branche.Latéralité.Gauche)
        Segment2.pA = BrancheSuivante.ExtrémitéBordChaussée(Branche.Latéralité.Droite)

        'SegmentLimite = New Ligne(Segment1.pA, Segment2.pA)
        'AngleBranche = eqvRadian(uneBranche.Angle)
        'LargeurBranche = uneBranche.Largeur * Echelle
        '======================================================================================================
        p(0) = PointPosition(Segment2.pA, uneBranche.Largeur / 2 * Echelle, uneBranche.AngleEnRadians + sngPI / 2)
        'DecalV2 = New Vecteur(Segment2.pA, p(0))
        p(1) = PointPosition(Segment1.pA, uneBranche.Largeur / 2 * Echelle, uneBranche.AngleEnRadians - sngPI / 2)
        'DecalV3 = New Vecteur(Segment1.pA, p(1))

        ' Tolérance de PI/12 par rapport aux règles +strictes
        l1 = New Ligne(p(1), PtClippé(PointPosition(p(1), uneBranche.AngleEnRadians + sngPI / 12), p(1), CoordonnéesEcran:=False), unePlumeBleue)
        l2 = New Ligne(p(0), PtClippé(PointPosition(p(0), uneBranche.AngleEnRadians - sngPI / 12), p(0), CoordonnéesEcran:=False), unePlumeVerte)
        'DessinerObjet(l1)
        'DessinerObjet(l2)

        ' Construire l'enveloppe autorisée en s'appuyant sur l1,l2 et les bords du controle picDessin

        'Définition des bords
        Dim uneTaille As Size = picDessin.ClientSize
        Dim lBord(3) As Ligne
        'Bord gauche
        lBord(0) = New Ligne(New Point(0, 0), New Point(0, uneTaille.Height))
        'Bord bas
        lBord(1) = New Ligne(lBord(0).pB, New Point(uneTaille.Width, uneTaille.Height))
        'Bord droit
        lBord(2) = New Ligne(lBord(1).pB, New Point(uneTaille.Width, 0))
        'Bord haut
        lBord(3) = New Ligne(lBord(2).pB, lBord(0).pA)

        ' Recherche de l'intersection de l1 avec un bord
        Dim i, j As Short
        Dim uneLigneBord As Ligne
        For i = 0 To 3
          uneLigneBord = lBord(i)
          p(2) = Point.Ceiling(intersect(l1, uneLigneBord))
          If Not p(2).IsEmpty Then
            j = i
            Exit For
          End If
        Next

        'Si l2 intersecte le même bord : ajouter ce point à l'enveloppe et terminer
        p(3) = Point.Ceiling(intersect(l2, uneLigneBord))
        If p(3).IsEmpty Then
          ' Ajouter le coin du controle à l'enveloppe
          p(3) = uneLigneBord.pB
          'Parcourir les bords suivants jusqu'à ce que l2 en intersecte un
          For i = (j + 1) Mod 4 To (j + 3) Mod 4
            uneLigneBord = lBord(i)
            ReDim Preserve p(p.Length)
            p(p.Length - 1) = Point.Ceiling(intersect(l2, uneLigneBord, Formules.TypeInterSection.SurPremierSegment))
            If p(p.Length - 1).IsEmpty Then
              p(p.Length - 1) = uneLigneBord.pB
            Else
              Exit For
            End If
          Next
        End If

        EnveloppeBranche = New PolyArc(p, Clore:=True)
        EnveloppeBranche.Plume = unePlumeRouge
        'DessinerObjet(EnveloppeBranche)

        '======================================================================================================

      Else
        ' Rotation de la branche
        Dim pMini, pMaxi As Point
        Dim pDonné, pOrigine, pCherché As Point
        CentreRotation = .pA
        LongueurSegment = .Longueur
        AngleProjection = EqvRadian(uneBranche.Angle) + Math.PI / 2
        If AngleProjection > PI Then AngleProjection -= 2 * PI
        mScreen(0) = picDessin.PointToScreen(CentreRotation)
        mScreen(1) = picDessin.PointToScreen(.pB)

        Dim Longueur As Single = uneBranche.Longueur * Echelle
        Dim DemiLargeur As Single = uneBranche.Largeur / 2 * Echelle
        ' bord de chaussée droit
        'pDonné = uneBranche.BordChaussée(Branche.Latéralité.Droite).pA
        pDonné = uneBranche.ExtrémitéBordChaussée(Branche.Latéralité.Droite)

        ' bord de chaussée gauche de la branche précédente
        'pOrigine = BranchePrécédente.BordChaussée(Branche.Latéralité.Gauche).pA
        pOrigine = BranchePrécédente.ExtrémitéBordChaussée(Branche.Latéralité.Gauche)

        pCherché = PointSurDroiteADistancePointDonné(pDonné, Longueur, pOrigine, EqvRadian(BranchePrécédente.Angle))
        If pCherché.IsEmpty Then
          pCherché = pOrigine
        ElseIf AngleFormé(pOrigine, pCherché, pDonné) < 0 Then
          pCherché = pOrigine
        End If
        pMini = PointPosition(pCherché, DemiLargeur, CType(AngleFormé(pDonné, pCherché) - PI / 2, Single))

        ' bord de chaussée gauche
        'pDonné = uneBranche.BordChaussée(Branche.Latéralité.Gauche).pA
        pDonné = uneBranche.ExtrémitéBordChaussée(Branche.Latéralité.Gauche)

        ' bord de chaussée droite de la branche suivante
        'pOrigine = BrancheSuivante.BordChaussée(Branche.Latéralité.Droite).pA
        pOrigine = BrancheSuivante.ExtrémitéBordChaussée(Branche.Latéralité.Droite)

        pCherché = PointSurDroiteADistancePointDonné(pDonné, Longueur, pOrigine, EqvRadian(BrancheSuivante.Angle))
        If pCherché.IsEmpty Then
          pCherché = pOrigine
        ElseIf AngleFormé(pOrigine, pCherché, pDonné) > 0 Then
          pCherché = pOrigine
        End If
        pMaxi = PointPosition(pCherché, DemiLargeur, CType(AngleFormé(pDonné, pCherché) + PI / 2, Single))

        AngleMini = CvAngleDegrés(AngleFormé(mPoint(0), pMini), SurDeuxPi:=False)
        Dim AngleMaxi As Single = CvAngleDegrés(AngleFormé(mPoint(0), pMaxi), SurDeuxPi:=False)
        If AngleMaxi < AngleMini Then AngleMaxi += 360
        BalayageMaxi = AngleMaxi - AngleMini
      End If
    End With

  End Function

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsPassage(ByVal unPassage As PassagePiéton, ByVal pSouris As Point)
    Dim p As Point
    ReDim mScreen(3)
    ReDim mPoint(0)
    ReDim DecalV(3)

    mPoint(0) = pSouris

    Dim Index As Short

    With unPassage
      With unPassage.Contour
        For Index = 0 To 3
          ' Point du contour du passage
          p = CvPoint(.Points(Index * PasPassage))
          mScreen(Index) = picDessin.PointToScreen(p)
          'Décalage entre la position de la souris et le point du contour
          DecalV(Index) = New Vecteur(pSouris, p)
        Next

      End With

      ' Segment infini passant par le pointeur de souris et parallèle à l'axe de la branche
      ' on projettera le pointeur de souris sur ce segment pour déterminer la nouvelle position du passage
      Segment1 = New Ligne(pSouris, PointPosition(pSouris, 100, .mBranche.AngleEnRadians))

    End With

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsEditPassage(ByVal unPassage As PassagePiéton)
    Dim unContour As PolyArc = unPassage.Contour
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    BrancheLiée = unPassage.mBranche
    Dim Ligne1 As Ligne = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))  ' Coté proche et parallèle au bord de chaussée
    Dim Ligne2 As Ligne = New Ligne(ptsContour(Index(2)), ptsContour(Index(3))) ' Coté opposé

    ReDim mScreen(3)
    ReDim mPoint(0)

    'UneCommandeGraphique = CommandeGraphique.EditPointPassage

    'Mémorisation du point restant fixe dans mPoint(0)
    Select Case PoignéeCliquée
      Case 0
        Segment1 = Ligne1
        Segment2 = Ligne2
      Case 1
        Segment1 = Ligne1.Inversée
        Segment2 = Ligne2.Inversée
      Case 2
        Segment1 = Ligne2
        Segment2 = Ligne1
      Case 3
        Segment1 = Ligne2.Inversée
        Segment2 = Ligne1.Inversée
    End Select

    SegmentLimite = New Ligne(Segment1.pB, Segment2.pA)
    mPoint(0) = SegmentLimite.pA    ' Point invariant du coté en cours de modif
    mScreen(0) = picDessin.PointToScreen(Segment1.pA)  'Point en cours de modif
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le coté opposé
    mScreen(1) = picDessin.PointToScreen(SegmentLimite.pA) 'Point invariant du petit coté en cours de modif
    mScreen(3) = picDessin.PointToScreen(SegmentLimite.pB) 'Point invariant du coté opposé au coté en cours de modif

    SigneConservé = Math.Sign(AngleFormé(Segment1.pA, SegmentLimite.pA, SegmentLimite.pB))
    'Angle des 2 grands cotés parallèles
    AngleParallèle = AngleFormé(Ligne1.pA, Ligne2.pB)

    'Bissectrice d l'angle formé par les 2 segments qui se croisent au point à modifer
    AngleProjection = (AngleFormé(Segment1) + AngleFormé(SegmentLimite)) / 2

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsEditAnglePassage(ByVal unPassage As PassagePiéton)
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    BrancheLiée = unPassage.mBranche

    ReDim mScreen(3)
    ReDim mPoint(0)

    Select Case PoignéeCliquée
      'Segment1 : Segment invariant
      'Segment2 :pA représente le point en cours de modif, et pB son symétrique qui sera recalculé en fonction du parcours de pA)
    Case 0
        Segment1 = New Ligne(ptsContour(Index(3)), ptsContour(Index(2)))    ' 3-2
        Segment2 = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))    ' 0-1
      Case 1
        Segment1 = New Ligne(ptsContour(Index(2)), ptsContour(Index(3)))    ' 2-3
        Segment2 = New Ligne(ptsContour(Index(1)), ptsContour(Index(0)))    ' 1-0
      Case 2
        Segment1 = New Ligne(ptsContour(Index(1)), ptsContour(Index(0)))    ' 1-0
        Segment2 = New Ligne(ptsContour(Index(2)), ptsContour(Index(3)))    ' 2-3
      Case 3
        Segment1 = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))    ' 0-1
        Segment2 = New Ligne(ptsContour(Index(3)), ptsContour(Index(2)))    ' 3-2
    End Select

    mScreen(0) = picDessin.PointToScreen(Segment2.pA)  'Point en cours de modif
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le coté en cours de modif
    mScreen(3) = picDessin.PointToScreen(Segment1.pB) 'Point invariant du petit coté opposé
    mScreen(1) = picDessin.PointToScreen(Segment1.pA) 'Point invariant du petit coté opposé

    'Mémorisation du point de référence dans mPoint(0) :point invariant sur le même grand coté que le point en cours de modif
    mPoint(0) = Segment1.pA

    'Angle des 2 cotés parallèles du trapèze
    AngleParallèle = AngleFormé(ptsContour(Index(1)), ptsContour(Index(2)))
    'Angle à conserver : angle entre le point en cours de modif et le segment invariant(Segment1)
    SigneConservé = Math.Sign(AngleFormé(Segment2.pA, Segment1.pA, Segment1.pB))

  End Sub

  ''**************************************************************************************
  '' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsEditDimensionPassage(ByVal unPassage As PassagePiéton)
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next
    BrancheLiée = unPassage.mBranche
    Dim Ligne1 As Ligne = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))  ' Coté proche et parallèle au bord de chaussée
    Dim Ligne2 As Ligne = New Ligne(ptsContour(Index(2)), ptsContour(Index(3)))  ' Coté opposé

    ReDim mScreen(3)
    ReDim mPoint(0)

    ' Modification d'un petit coté en restant parallèle au bord de chaussée

    'Mémorisation du point restant fixe dans mPoint(0)
    If UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Then
      'Angle des 2 grands cotés parallèles
      AngleParallèle = AngleFormé(Ligne1.pA, Ligne2.pB)
      If PoignéeCliquée = 0 Then
        Segment1 = Ligne1
        Segment2 = Ligne2
      Else  ' PoignéeCliquée = 2
        Segment1 = Ligne2
        Segment2 = Ligne1
      End If

    Else  'UneCommandeGraphique = CommandeGraphique.EditLongueurPassage
      If PoignéeCliquée = 1 Then
        Segment1 = New Ligne(Ligne1.pB, Ligne2.pA)
        Segment2 = New Ligne(Ligne2.pB, Ligne1.pA)
        'Angle du coté proche du bord de chaussée
        AngleParallèle = AngleFormé(Ligne1)
      Else  ' PoignéeCliquée = 3
        Segment1 = New Ligne(Ligne2.pB, Ligne1.pA)
        Segment2 = New Ligne(Ligne1.pB, Ligne2.pA)
        'Angle du coté proche du bord de chaussée opposé (ou coté ilot)
        AngleParallèle = AngleFormé(Ligne2)
      End If
    End If

    SegmentLimite = New Ligne(Segment1.pB, Segment2.pA)
    mPoint(0) = SegmentLimite.Milieu     ' Point invariant du coté en cours de modif
    mScreen(0) = picDessin.PointToScreen(Segment1.pA)  'Point en cours de modif
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le coté opposé
    mScreen(1) = picDessin.PointToScreen(SegmentLimite.pA) 'Point invariant du petit coté en cours de modif
    mScreen(3) = picDessin.PointToScreen(SegmentLimite.pB) 'Point invariant du coté opposé au coté en cours de modif

    SigneConservé = Math.Sign(AngleFormé(Segment1.pA, SegmentLimite.pA, SegmentLimite.pB))
    AngleProjection = AngleParallèle + Math.PI / 2
  End Sub

  ''**************************************************************************************
  '' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  ''**************************************************************************************
  'Private Sub DéfinirPointsPassage(ByVal unPassage As PassagePiéton, ByVal numPoignée As Short)
  '  Dim unContour As PolyArc = unPassage.Contour
  '  BrancheLiée = unPassage.mBranche
  '  Dim Ligne1 As Ligne = New Ligne(unContour.Points(0), unContour.Points(1))  ' Coté proche et parallèle au bord de chaussée
  '  Dim Ligne2 As Ligne = New Ligne(unContour.Points(2), unContour.Points(3))  ' Coté opposé
  '  Dim ParallélismeComplet As Boolean = Abs(AngleFormé(Ligne2) - BrancheLiée.AngleEnRadians) < 0.1
  '  Dim ParallélismeEnvisageable As Boolean = numPoignée = 0 Or numPoignée = 1 Or ParallélismeComplet

  '  ReDim mScreen(3)
  '  ReDim mPoint(0)

  '  PoignéeCliquée = numPoignée
  '  Dim pSouris As Point = picDessin.PointToClient(picDessin.MousePosition)
  '  If BrancheLiée.BordChausséeProche(pSouris) <> Branche.Latéralité.Aucune And ParallélismeEnvisageable Then
  '    ' Modification d'un petit coté en restant parallèle au bord de chaussée
  '    UneCommandeGraphique = CommandeGraphique.EditLargeurPassage

  '    'Mémorisation du point restant fixe dans mPoint(0)
  '    Select Case PoignéeCliquée
  '      Case 0
  '        Segment1 = Ligne1
  '        Segment2 = Ligne2
  '      Case 1
  '        Segment1 = Ligne1.Inversée
  '        Segment2 = Ligne2.Inversée
  '      Case 2
  '        Segment1 = Ligne2
  '        Segment2 = Ligne1
  '      Case 3
  '        Segment1 = Ligne2.Inversée
  '        Segment2 = Ligne1.Inversée
  '    End Select

  '    'Angle de la branche
  '    AngleProjection = AngleFormé(Segment1)
  '    'Angle des 2 grands cotés parallèles
  '    AngleParallèle = AngleFormé(Segment1.pA, Segment2.pB)
  '    mPoint(0) = Segment1.pB   ' Point invariant du coté en cours de modif
  '    mScreen(1) = picDessin.PointToScreen(Segment1.pA) 'Point variant
  '    mScreen(2) = picDessin.PointToScreen(Segment2.pB)
  '    mScreen(3) = picDessin.PointToScreen(Segment2.pA) 'Point projeté du précédent sur l'autre coté

  '  Else
  '    ' Modification d'un grand coté
  '    ' On va soit étirer le passage soit changer l'angle du passage (angle des 2 cotés parallèles)

  '    'Mémoriser le point cliqué (et qui va ensuite se déplacer)
  '    If numPoignée = 2 Then
  '      mPoint(0) = Ligne2.pA
  '    Else
  '      mPoint(0) = Ligne2.pB
  '    End If

  '    If Distance(pSouris, mPoint(0)) < 3 Then
  '      'Différer la prise en compte du MouseMove jusqu'à ce que le Glisser soit significatif (pour évaluer l'angle)
  '      ReDim mPoint(-1)

  '    Else
  '      Dim uneLigne As Ligne = New Ligne(pSouris, mPoint(0))
  '      Dim uneLigne1 As Ligne = New Ligne(unContour.Points(1), unContour.Points(2)) ' Grand coté
  '      Dim uneLigne2 As Ligne = New Ligne(unContour.Points(3), unContour.Points(0)) ' 2ème Grand coté
  '      Dim AngleDépart As Single = Abs(AngleFormé(uneLigne, uneLigne1))
  '      If AngleDépart > PI / 2 Then AngleDépart = PI - AngleDépart
  '      If AngleDépart < PI / 6 Then
  '        'Etirer ou raccourcir le grand coté cliqué
  '        UneCommandeGraphique = CommandeGraphique.EditLongueurPassage
  '      Else
  '        'Rotation du passage
  '        UneCommandeGraphique = CommandeGraphique.EditAnglePassage
  '      End If

  '      If numPoignée = 2 Then
  '        'Le point correspondant à Poignée3 est invariant
  '        ' 1er grand coté
  '        Segment1 = uneLigne1.Inversée
  '        ' 2ème grand coté 
  '        Segment2 = uneLigne2.Inversée
  '      Else
  '        'Le point correspondant à Poignée2 est invariant
  '        Segment1 = uneLigne2 ' 2ème grand coté 
  '        Segment2 = uneLigne1 ' 1er grand coté 
  '      End If

  '      'Angle du grand coté
  '      AngleProjection = AngleFormé(Segment1)

  '      mScreen(1) = picDessin.PointToScreen(Segment1.pB)
  '      mScreen(2) = picDessin.PointToScreen(Segment2.pB)
  '      mScreen(3) = picDessin.PointToScreen(Segment2.pA)
  '    End If
  '  End If

  '  If mPoint.Length > 0 Then mScreen(0) = picDessin.PointToScreen(mPoint(0))

  'End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du passage piéton pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsCarrefour(ByVal pSouris As Point)
    Dim nbBranches As Short = maVariante.mBranches.Count
    Dim uneBranche As Branche
    Dim i As Short
    Dim p As Point
    ReDim mPoint(0)
    ReDim mScreen(nbBranches * 4 - 1)
    ReDim DecalV(mScreen.Length - 1)

    mPoint(0) = pSouris
    DecalV1 = New Vecteur(pSouris, PointDessin(maVariante.mCarrefour.mCentre))

    Dim numBranche As Short
    For numBranche = 0 To nbBranches - 1
      uneBranche = mesBranches(numBranche)
      p = uneBranche.BordChaussée(Branche.Latéralité.Gauche).pA
      mScreen(4 * numBranche) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche) = New Vecteur(pSouris, p)

      p = uneBranche.BordChaussée(Branche.Latéralité.Gauche).pB
      mScreen(4 * numBranche + 1) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 1) = New Vecteur(pSouris, p)

      uneBranche = mesBranches.Suivante(uneBranche)
      p = uneBranche.BordChaussée(Branche.Latéralité.Droite).pA
      mScreen(4 * numBranche + 2) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 2) = New Vecteur(pSouris, p)

      p = uneBranche.BordChaussée(Branche.Latéralité.Droite).pB
      mScreen(4 * numBranche + 3) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 3) = New Vecteur(pSouris, p)

    Next

  End Sub

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du Nord pour MouseMove
  '**************************************************************************************
  Private Function DéfinirPointsNord(ByVal objetMétier As Métier, ByVal pEnCours As Point) As Boolean
    Dim unNord As Nord = CType(objetMétier, Nord)
    Dim uneLigne As Ligne = unNord.LigneRéférence

    If unNord.Déplaçable(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.DéplacerNord
      DéfinirPointsNord = True
    ElseIf unNord.Orientable(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.OrienterNord
      DéfinirPointsNord = True
    End If

    If DéfinirPointsNord Then
      ReDim mPoint(0)
      'Points utiles à l'élastique : 1 segment pour représenter le Nord  (+ flèche ?)
      ReDim mScreen(1)
      'Extrémité de la ligne la + loin de la flèche
      mScreen(0) = picDessin.PointToScreen(uneLigne.pB)
      'Extrémité de la ligne la + proche de la flèche
      mScreen(1) = picDessin.PointToScreen(uneLigne.pA)

      Select Case UneCommandeGraphique
        Case CommandeGraphique.DéplacerNord

          ReDim DecalV(1)
          DecalV(0) = New Vecteur(uneLigne.pA.X - pEnCours.X, uneLigne.pA.Y - pEnCours.Y)
          DecalV(1) = New Vecteur(uneLigne.pB.X - pEnCours.X, uneLigne.pB.Y - pEnCours.Y)

        Case CommandeGraphique.OrienterNord
          'Extrémité de la ligne la + loin de la flèche
          CentreRotation = uneLigne.pB
          LongueurSegment = uneLigne.Longueur
          'AngleProjection est l'angle du Nord + pi/2 pour un curseur perpendiculaire à la direction de la flèche
          AngleProjection = unNord.Orientation + Math.PI / 2
      End Select
    End If

  End Function

  '**************************************************************************************
  ' Définir les éléments utiles à la modification du Nord pour MouseMove
  '**************************************************************************************
  Private Function DéfinirPointsEchelle(ByVal uneEchelle As SymEchelle, ByVal pEnCours As Point) As Boolean
    Dim uneLigne As Ligne = uneEchelle.LigneRéférence

    If uneEchelle.Déplaçable(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.DéplacerEchelle
      DéfinirPointsEchelle = True

      ReDim mPoint(0)
      'Points utiles à l'élastique : 1 segment pour représenter le rectangle
      ReDim mScreen(1)
      'Extrémité gauche 
      mScreen(0) = picDessin.PointToScreen(uneLigne.pB)
      'Extrémité droite
      mScreen(1) = picDessin.PointToScreen(uneLigne.pA)

      Select Case UneCommandeGraphique
        Case CommandeGraphique.DéplacerEchelle

          ReDim DecalV(1)
          DecalV(0) = New Vecteur(uneLigne.pA.X - pEnCours.X, uneLigne.pA.Y - pEnCours.Y)
          DecalV(1) = New Vecteur(uneLigne.pB.X - pEnCours.X, uneLigne.pB.Y - pEnCours.Y)

      End Select
    End If

  End Function

  '**************************************************************************************
  ' Définir les éléments utiles à la modification de l'ilot pour MouseMove
  '**************************************************************************************
  Private Sub DéfinirPointsIlots(ByVal unPolyArc As PolyArc, ByVal numPoignée As Short, ByVal pRef As Point)
    Dim uneLigne As Ligne
    Dim unArc As Arc
    Dim unIlot As Ilot = unPolyArc.ObjetMétier
    BrancheLiée = unIlot.mBranche

    ReDim mPoint(0)
    'Points utiles à l'élastique : 3 segments pour représenter l'ilot  (triangle P1P3P4)
    ReDim mScreen(3)    ' le 4ème point est en principe égal au 1er sauf clipping

    uneLigne = unPolyArc(0) ' 1er Grand coté de l'ilot
    mScreen(0) = picDessin.PointToScreen(uneLigne.pA)   ' P1 : pointe de l'ilot
    mScreen(3) = mScreen(0)
    mScreen(1) = picDessin.PointToScreen(uneLigne.pB)   ' P3 : extrémité gauche de l'ilot

    uneLigne = unPolyArc(1) ' 2è Grand coté de l'ilot
    mScreen(2) = picDessin.PointToScreen(uneLigne.pB)    ' P4 (symétrique de P3)

    ' Points caractéristiques de l'ilot
    uneLigne = unPolyArc(0)
    Dim P1 As Point = uneLigne.pA   ' P1 : pointe de l'ilot
    Dim P3 As Point = uneLigne.pB    ' P3 : extrémité gauche de l'ilot
    uneLigne = unPolyArc(1)
    Dim P4 As Point = uneLigne.pB    ' P4 (symétrique de P3)
    Dim P2 As Point = Milieu(P3, P4)        ' P2 : P1P2 est l'axe de symétrie longitudinal
    unArc = unPolyArc(2)

    Dim P5 As Point                   ' Milieu de l'arc (point le plus extrême vers l'origine de la branche
    With unArc
      P5 = PointPosition(.pO, .Rayon, .AngleDépart + .AngleBalayage / 2, SensHoraire:=True)
    End With



    Select Case numPoignée
      Case 0
        'Agrandissement ou rétrécissement de l'ilot - Affecte le rayon
        UneCommandeGraphique = CommandeGraphique.EtirerIlot

        mPoint(0) = P2       '  P1P2 est l'axe de symétrie longitudinal
        AngleProjection = AngleFormé(P1, P2)
      Case 1, 2
        'Agrandissement ou rétrécissement de l'ilot - Affecte la largeur
        UneCommandeGraphique = CommandeGraphique.ElargirIlot
        mPoint(0) = P2    ' P3P4 est la corde de l'ilot et P2 est milieu de P3P4
        AngleProjection = AngleFormé(P3, P4)
        ReDim Preserve mScreen(4)  ' le 5ème point est juste pour mémoriser P2 (P3 et P4 sont symétriques / P2)
        mScreen(4) = picDessin.PointToScreen(P2)

      Case 3
        'Translation de l'ilot : Déplacement de P5 (Sommet de l'arc) ou d'un point à l'intérieur de l'ilot - Affecte le retrait et le décalage
        UneCommandeGraphique = CommandeGraphique.DéplacerIlot
        If pRef.IsEmpty Then pRef = P5

        ' Point Origine du bord droit de la chaussée (l'ilot ne doit pas s'approcher de ce bord à moins de 2 m)
        mPoint(0) = BrancheLiée.ExtrémitéBordChaussée(Branche.Latéralité.Droite)
        ' Projection du mileu d l'arc sur le bord de la chaussée
        'AngleProjection est l'angle de direction mPoint(5)-mPoint(6)
        AngleProjection = EqvRadian(BrancheLiée.Angle)

        ReDim DecalV(3)
        DecalV(0) = New Vecteur(P1.X - pRef.X, P1.Y - pRef.Y)
        DecalV(1) = New Vecteur(P3.X - pRef.X, P3.Y - pRef.Y)
        DecalV(2) = New Vecteur(P4.X - pRef.X, P4.Y - pRef.Y)
        DecalV(3) = New Vecteur(P2.X - pRef.X, P2.Y - pRef.Y)
    End Select

  End Sub

#End Region
#End Region
  Public Sub CommandeZoom(ByVal Index As MDIDiagfeux.BarreOutilsEnum)

    If Not DiagrammeActif() Then
      Select Case Index
        Case MDIDiagfeux.BarreOutilsEnum.Zoom
          DémarrerCommande(Me.CommandeGraphique.Zoom)
        Case MDIDiagfeux.BarreOutilsEnum.ZoomMoins
          DémarrerCommande(Me.CommandeGraphique.ZoomMoins)
        Case MDIDiagfeux.BarreOutilsEnum.PAN
          DémarrerCommande(Me.CommandeGraphique.ZoomPAN)
        Case MDIDiagfeux.BarreOutilsEnum.ZoomPrécédent
          DémarrerCommande(Me.CommandeGraphique.ZoomPrécédent)
        Case MDIDiagfeux.BarreOutilsEnum.Mesurer
          DémarrerCommande(Me.CommandeGraphique.Mesure)
        Case MDIDiagfeux.BarreOutilsEnum.Rafraichir
          Redessiner()
          DémarrerCommande(CommandeGraphique.AucuneCommande)
      End Select
    End If

  End Sub

  '**************************************************************************************
  ' Démarrer une nouvelle commande graphique
  '**************************************************************************************
  Private Sub DémarrerCommande(ByVal uneCommande As CommandeGraphique, Optional ByVal Continuation As Boolean = False)
    UneCommandeGraphique = uneCommande

    'Initialisations des drapeaux servant à gérer le déroulement de la commande
    mDragging = False ' Inhibe MouseMove
    ReDim mPoint(-1)
    ReDim mScreen(-1)
    ReDim DecalV(-1)

    Try

      Select Case uneCommande
        Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins
          picDessin.Cursor = Cursors.Cross
        Case CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure
          TraiterMessageGlisser()
        Case CommandeGraphique.ZoomPrécédent
          TerminerCommande(picDessin.MousePosition)
          UneCommandeGraphique = CommandeGraphique.AucuneCommande
        Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu, _
         CommandeGraphique.PropTrajectoire, CommandeGraphique.PropTraversée
          If IsNothing(objSélect) Then
            TraiterMessageGlisser()
          Else
            TerminerCommande(picDessin.MousePosition)
            DémarrerCommande(CommandeGraphique.AucuneCommande)
          End If
        Case CommandeGraphique.AucuneCommande
          picDessin.Cursor = Cursors.Arrow
          TraiterMessageGlisser()
        Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          If Not Continuation Then
            Désélectionner(uneCommande)
          End If
          If UneCommandeGraphique = CommandeGraphique.LigneFeux Then
            Me.AC1GrilleFeux.Row = -1
          End If
          TraiterMessageGlisser(Continuation)
        Case CommandeGraphique.Antagonisme
          TraiterMessageGlisser()
      End Select

      If Not CommandeConservantSélection() Then Désélectionner(uneCommande)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Public Sub InterrompreCommande()
    If UneCommandeGraphique = CommandeGraphique.Antagonisme Then
      If Not IsNothing(objSélect) Then
        Try
          Dim unAntagonisme As Antagonisme = CType(objSélect.ObjetMétier, Antagonisme)

          If Not unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
            'La fenêtre des antagonismes ne concerne que les admissibles (pouvant basculer d'admis à non admis)

            ' La fenêtre des antagonismes ne visualise qu'un seul antagonisme pour tous ceux de même courant
            'Il faut donc Pointer sur l'antagonisme visible dans la fenêtre Antagonisme(antagonisme de même courant)
            unAntagonisme = unAntagonisme.MêmesCourants

            'Les valeurs globales de l'échelle ont été modifiées par l'activation de la feuille abaque
            'RéaffecterEchelle()

            With Me.FenetreAntagonisme
              If .radOui.Checked Then
                If AntagonismeLiéRefusé(unAntagonisme, Admis:=True) Then
                  .mAntagonisme = Nothing
                  .radNon.Checked = True
                  .mAntagonisme = unAntagonisme
                Else
                  MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.Admis)
                End If
              Else
                MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.NonAdmis)
              End If
            End With

          End If

        Catch ex As DiagFeux.Exception
          Throw New DiagFeux.Exception(ex.Message)
        Catch ex As System.Exception
          LancerDiagfeuxException(ex, "InterrompreCommande")
        End Try
      End If

    Else
      EffacerElastiques()
      ' L'instruction qui suit va éteindre les poignées de l'objet sélectionnées : on le fait avant pour que celle-ci les rallument
      If Not IsNothing(objSélect) Then SélDésélectionner()
      DémarrerCommande(CommandeGraphique.AucuneCommande)
    End If
  End Sub

  Private Function CurseurCommande() As Cursor
    Select Case UneCommandeGraphique
      Case CommandeGraphique.OrigineBranche, CommandeGraphique.DéplacerPassage, CommandeGraphique.DéplacerSignal, CommandeGraphique.DéplacerCarrefour, CommandeGraphique.DéplacerNord, CommandeGraphique.DéplacerEchelle
        Return Cursors.SizeAll
      Case CommandeGraphique.OrienterNord
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.AngleBranche
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EtirerIlot
        Return CurseurSelonAngle(AngleProjection)
        Return Cursors.SizeNS
      Case CommandeGraphique.DéplacerIlot
        Return Cursors.SizeAll
      Case CommandeGraphique.ElargirIlot
        Return CurseurSelonAngle(AngleProjection)
        Return Cursors.SizeWE
      Case CommandeGraphique.DéplacerLigneFeu, CommandeGraphique.AllongerFeu
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditPointPassage
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EditAnglePassage
        Return CurseurSelonAngle(AngleParallèle + Math.PI / 2)
      Case CommandeGraphique.EditerTrajectoire
        Return Cursors.Cross
      Case CommandeGraphique.ZoomPAN
        Return Cursors.Hand
      Case CommandeGraphique.Mesure
        Return Cursors.Cross
      Case CommandeGraphique.LigneFeux, CommandeGraphique.PassagePiéton
        If mPoint.Length = 0 Then
          Return Cursors.Cross
        Else
          Return Cursors.Arrow
        End If
      Case Else
        Return Cursors.Arrow
    End Select
  End Function

  Private Function CurseurSelonAngle(ByVal unAngle As Single) As Cursor
    ' NWSE et NESW sont inversés en raison de l'inversion des Y dans le système de coordonnées Windows

    Select Case unAngle / PI * 8
      Case -1 To 1, Is > 7, Is < -7
        Return Cursors.SizeWE
      Case 1 To 3, -7 To -5
        Return Cursors.SizeNWSE
      Case 3 To 5, -5 To -3
        Return Cursors.SizeNS
      Case 5 To 7, -3 To -1
        Return Cursors.SizeNESW
    End Select
  End Function

  '****************************************************************************************
  ' Masquer la sélection de l'objet sélectionné (s'il y en a un)
  '****************************************************************************************
  Private Sub Désélectionner(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.AucuneCommande)
    If Not IsNothing(objSélect) Then
      SélDésélectionner(PourSélection:=False)    ' Montre ou cache les poignées de sélection
      If Not uneCommande = CommandeGraphique.AucuneCommande Then
        'Traitement supplémentaire pour rallumer certains objets masqués pendant le temps de la commande
        DéSélObjet()
        savObjSélect = Nothing
      End If
    End If

  End Sub

  '****************************************************************************************
  ' Lors de la désélection d'un objet : Redessiner la partie effacée lors de la sélection
  '****************************************************************************************
  Private Sub DéSélObjet()
    Dim unObjetMétier As Métier = objSélect.ObjetMétier
    If TypeOf unObjetMétier Is PassagePiéton Then
      Dim unPassage As PassagePiéton = unObjetMétier
      If Not IsNothing(unPassage.Zebras) Then DessinerObjet(unPassage.Zebras)
    End If
    objSélect = Nothing
  End Sub

  '******************************************************************************
  ' Traiter le message de l'opération Glisser lors du MouseMove ou du MouseUp
  '******************************************************************************
  Private Sub TraiterMessageGlisser(Optional ByVal Continuation As Boolean = False)
    Dim msg, Contexte As String

    Select Case UneCommandeGraphique
      Case CommandeGraphique.AucuneCommande
      Case CommandeGraphique.DéplacerNord
        msg = "Positionner le Nord"
      Case CommandeGraphique.OrienterNord
        msg = "Orienter le Nord"
      Case CommandeGraphique.DéplacerEchelle
        msg = "Positionner l'échelle"
      Case CommandeGraphique.OrigineBranche
        msg = "Faire Glisser la branche"
        Contexte = "Origine de la branche"
      Case CommandeGraphique.AngleBranche
        msg = "Faire tourner la branche"
        Contexte = "Angle de la branche"
      Case CommandeGraphique.EtirerIlot
        msg = "Etirer l'ilot"
        Contexte = "Ilot"
      Case CommandeGraphique.DéplacerIlot
        msg = "Déplacer l'ilot"
        Contexte = "Ilot"
      Case CommandeGraphique.ElargirIlot
        msg = "Elargir l'ilot"
        Contexte = "Ilot"

      Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide
        Contexte = "Passage piéton"
        If UneCommandeGraphique = CommandeGraphique.PassagePiétonRapide Then
          msg = "Désigner la branche"
          If Continuation Then msg &= " du passage suivant"
        Else
          Select Case mPoint.Length
            Case 0
              msg = "Désigner un point sur le bord de la chaussée"
              If Continuation Then msg = "Premier point du passage suivant"
            Case 1
              msg = "Désigner l'extrémité du petit coté"
            Case 2
              msg = "Désigner le 3ème point"
            Case 3
              msg = "Désigner le dernier point"
          End Select
        End If

      Case CommandeGraphique.EditLargeurPassage
        Contexte = "Passage piéton"
        msg = "Largeur du passage"
      Case CommandeGraphique.EditLongueurPassage
        Contexte = "Passage piéton"
        msg = "Longueur du passage"
      Case CommandeGraphique.EditAnglePassage
        Contexte = "Passage piéton"
        msg = "Angle du passage"
      Case CommandeGraphique.EditPointPassage
        Contexte = "Passage piéton"
        msg = "Modifier le passage"

      Case CommandeGraphique.DéplacerPassage
        Contexte = "Passage piéton"
        msg = "Déplacer le passage"
      Case CommandeGraphique.SupprimerPassage
        Contexte = "Passage piéton"
        msg = "Désigner un passage piéton"

      Case CommandeGraphique.Trajectoire
        Contexte = "Trajectoire"
        If mPoint.Length = 0 Then
          msg = "Désigner la voie entrante de la trajectoire" & IIf(Continuation, " suivante", "")
        Else
          msg = "Désigner la voie sortante"
        End If

      Case CommandeGraphique.EditerTrajectoire
        Contexte = "Trajectoire"
        If mPoint.Length = 0 Then
          msg = "Désigner un point"
        Else
          msg = "Désigner le point suivant, ou cliquer sur le début de la voie sortante pour terminer"
        End If

      Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire
        Contexte = "Trajectoire"
        msg = "Positionner le point d'accès"
      Case CommandeGraphique.EditerPointTrajectoire
        Contexte = "Trajectoire"
        msg = "Positionner le point intermédiaire"

      Case CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.PropTrajectoire
        Contexte = "Trajectoire"
        msg = "Désigner une trajectoire"

      Case CommandeGraphique.Traversée, CommandeGraphique.DécomposerTraversée, CommandeGraphique.PropTraversée
        msg = "Désigner un passage piéton"
        Contexte = "Traversée piétonne"

      Case CommandeGraphique.LigneFeux
        Contexte = "Ligne de feux"
        If mPoint.Length = 0 Then
          msg = "Désigner le point de départ de la ligne de feux" & IIf(Continuation, " suivante", "")
        Else
          msg = "Désigner l'extrémité de la ligne de feux"
        End If
      Case CommandeGraphique.DéplacerLigneFeu
        Contexte = "Ligne de feux"
        msg = "Déplacer la ligne de feux"
      Case CommandeGraphique.AllongerFeu
        Contexte = "Ligne de feux"
        msg = "Etirer la ligne de feux"
      Case CommandeGraphique.SupprimerLigneFeu
        Contexte = "Ligne de feux"
        msg = "Désigner une ligne de feux"

      Case CommandeGraphique.DéplacerSignal
        Contexte = "Signal de feux"
        msg = "Positionner le signal"

      Case CommandeGraphique.DéplacerCarrefour
        Contexte = "Carrefour"
        msg = "Déplacer le carrefour"

      Case CommandeGraphique.PositionTrafic
        Contexte = "Trafic"
        msg = "Positionner les écritures du trafic"

      Case CommandeGraphique.Antagonisme
        Contexte = "Antagonismes"
        msg = "Valider le conflit"

      Case CommandeGraphique.ZoomPAN
        Contexte = "Panoramique"
        Select Case mPoint.Length
          Case 0
            msg = "1er point du déplacement"
          Case 1
            msg = "Extrémité du déplacement"
        End Select

      Case CommandeGraphique.Mesure
        Select Case mPoint.Length
          Case 0
            msg = "Désigner le point de référence"
          Case 1
            ' Désigner le point cherché
            msg = ""
        End Select
    End Select

    TraiterMessageGraphique(msg, Contexte)
    picDessin.Cursor = CurseurCommande()

  End Sub

  '******************************************************************************
  ' Traiter le message graphique pour l'afficher dans le Label adéquat
  '  ou  le mettre dans l'infobulle
  '******************************************************************************
  Private Sub TraiterMessageGraphique(ByVal Message As String, ByVal Contexte As String)

    If mDragging Or CommandeAvecAide() Then
      With FenetreAideCommande
        .Text = Contexte
        .lblMessageCommande.Text = Message
        .Visible = True
        .btnCancel.Visible = CommandeAnnulable()
      End With

    ElseIf UneCommandeGraphique = CommandeGraphique.Antagonisme Then
      Dim unAntagonisme As Antagonisme = objSélect.ObjetMétier
      Dim unTrafic As Trafic = monPlanFeuxBase.Trafic
      Dim fgAntago As GrilleDiagfeux = Me.AC1GrilleAntagonismes

      Try
        With FenetreAntagonisme
          'Libellé des courants antagonistes
          If unAntagonisme.EstPiéton Then
            .lblLibelléConflit.Text = "Conflit entre le courant " & unAntagonisme.Libellé(Antagonisme.PositionEnum.Premier, mesBranches) & " et  " & unAntagonisme.Libellé(Antagonisme.PositionEnum.Dernier, mesBranches)
          Else
            .lblLibelléConflit.Text = "Conflit entre les courants " & unAntagonisme.Libellé(Antagonisme.PositionEnum.Premier, mesBranches) & " et  " & unAntagonisme.Libellé(Antagonisme.PositionEnum.Dernier, mesBranches)
          End If
          .pnlConflit.Enabled = (maVariante.Verrou = [Global].Verrouillage.LignesFeux)

          'Traitement des boutons radios
          .mAntagonisme = Nothing   ' Pou inhiber les actions consécutives au (dé)cochage des boutons radios
          Select Case unAntagonisme.TypeConflit
            Case Trajectoire.TypeConflitEnum.Systématique
              .radNon.Checked = True
              .pnlConflit.Enabled = False
            Case Trajectoire.TypeConflitEnum.Admissible
              .radOui.Checked = False
              .radNon.Checked = False
            Case Trajectoire.TypeConflitEnum.Admis
              .radOui.Checked = True
            Case Trajectoire.TypeConflitEnum.NonAdmis
              .radNon.Checked = True
          End Select
          .mAntagonisme = unAntagonisme

          ' Traitement des messages
          If IsNothing(unTrafic) Then
            .lblMessageConflit.Visible = False
            .lblAlerte.Visible = False
            .lblAlertePlus.Visible = False
            'Ajout V13 (oublié  v12)
            .lblAlertePiétons.Visible = False

          Else
            Dim valTrafic1, valTrafic2 As Short
            With unAntagonisme
              valTrafic1 = .Courant(Antagonisme.PositionEnum.Premier).valTrafic(unTrafic)
              If .EstVéhicule Then valTrafic2 = .Courant(Antagonisme.PositionEnum.Dernier).valTrafic(unTrafic)
            End With
            .lblMessageConflit.ForeColor = System.Drawing.SystemColors.ControlText
            .lblMessageConflit.Visible = True
            .lblAlerte.Visible = False
            .lblAlertePlus.Visible = False
            .lblAlertePiétons.Visible = False
            Dim Alerte, AlertePlus, AlertePiétons As Boolean
            Dim unTypeCourant As Antagonisme.AntagonismeEnum = unAntagonisme.TypeCourantsAntagonistes
            Select Case unTypeCourant
              Case Antagonisme.AntagonismeEnum.TDTAG ' ,Antagonisme.AntagonismeEnum.TAGTAG : celui-ci peut-il être assimilé au précédent ?
                .lblMessageConflit.Text = "Trafic : " & valTrafic1 & " - TAG : " & valTrafic2
                'cndAbaque.AjouterTrafics(valTrafic1, valTrafic2)
              Case Antagonisme.AntagonismeEnum.TDTAD, Antagonisme.AntagonismeEnum.TAGTAD ': celui-ci peut-il être assimilé au précédent ?
                .lblMessageConflit.Text = "Trafic : " & valTrafic1 & " - TAD : " & valTrafic2
                If valTrafic2 >= 500 Then Alerte = True
              Case Antagonisme.AntagonismeEnum.TADPiétons, Antagonisme.AntagonismeEnum.TADPiétonsEtSensUnique
                .lblMessageConflit.Text = "Trafic TAD : " & valTrafic1
                If valTrafic1 >= 500 Then
                  AlertePiétons = True
                  If unTypeCourant = Antagonisme.AntagonismeEnum.TADPiétons Then
                    Alerte = True
                  Else
                    'TADPiétons et Sens unique
                    AlertePlus = True
                  End If
                End If
              Case Antagonisme.AntagonismeEnum.TAGPiétons, Antagonisme.AntagonismeEnum.TAGPiétonsEtSensUnique
                .lblMessageConflit.Text = "Trafic TAG : " & valTrafic1
                If valTrafic1 >= 500 Then
                  AlertePiétons = True
                  If unTypeCourant = Antagonisme.AntagonismeEnum.TAGPiétons Then
                    Alerte = True
                  Else
                    'TAGPiétons et Sens unique
                    AlertePlus = True
                  End If
                End If
            End Select

            'If unTypeCourant = Antagonisme.AntagonismeEnum.TDTAG Then
            '  cndAbaque.Show()
            '  Application.DoEvents()
            '  RéaffecterEchelle()
            'Else
            '  cndAbaque.Hide()
            'End If

            If Alerte Then
              .lblMessageConflit.ForeColor = System.Drawing.Color.OrangeRed
              .lblAlerte.Visible = True
            ElseIf AlertePlus Then
              .lblMessageConflit.ForeColor = System.Drawing.Color.OrangeRed
              .lblAlertePlus.Visible = True
            End If
            If AlertePiétons Then
              .lblAlertePiétons.Visible = True
            End If
          End If

          If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
            .lblMessageConflit.Text = "Courants strictement incompatibles"
            .lblMessageConflit.Visible = True
          End If

          If maVariante.Verrou > [Global].Verrouillage.LignesFeux Then
            If Not IsNothing(maVariante.BrancheEnCoursAntagonisme) Then
              'Sélectionner la bonne branche dans la combo sauf si on voit tous les antagonismes (choix 'Tous' dans la combo)
              Me.cboBrancheCourant1.SelectedIndex = mesBranches.IndexOf(unAntagonisme.BrancheCourant1)
            End If
            'Sélectionner la ligne correspondante dans la grille
            Dim row As Short = mAntagonismes.IndexOf(unAntagonisme) + 1
            fgAntago.Select(row, 0, row, 2)
          End If

          .Visible = True
        End With

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "TraiterMessageGraphique (Antagonisme)")
      End Try

    Else
      Me.tipPicDessin.SetToolTip(picDessin, Message)
      If UneCommandeGraphique = CommandeGraphique.AucuneCommande Then
        FenetreAideCommande.Hide()
        FenetreAntagonisme.Hide()
        '        cndAbaque.Hide()
      End If
    End If

  End Sub

  Private Function CommandeAnnulable() As Boolean

    Select Case UneCommandeGraphique
      Case CommandeGraphique.OrigineBranche, CommandeGraphique.AngleBranche, _
      CommandeGraphique.EtirerIlot, CommandeGraphique.DéplacerIlot, CommandeGraphique.ElargirIlot, _
      CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage, _
      CommandeGraphique.DéplacerLigneFeu, CommandeGraphique.AllongerFeu
        Return False

      Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide, CommandeGraphique.SupprimerPassage, CommandeGraphique.Trajectoire, CommandeGraphique.SupprimerTrajectoire, _
       CommandeGraphique.Traversée, CommandeGraphique.DécomposerTraversée, CommandeGraphique.LigneFeux, CommandeGraphique.SupprimerLigneFeu
        Return True

      Case CommandeGraphique.PropTrajectoire, CommandeGraphique.EditerTrajectoire
        Return True

      Case CommandeGraphique.PropTraversée
        Return True

      Case CommandeGraphique.DéplacerSignal, CommandeGraphique.DéplacerCarrefour
        Return True
        'Case CommandeGraphique.Zoom
        'Case CommandeGraphique.ZoomMoins
        'Case CommandeGraphique.ZoomPrécédent

        'Case CommandeGraphique.PositionTrafic
        'Case CommandeGraphique.Antagonisme

    End Select

  End Function

  Private Function CommandeAvecAide() As Boolean
    If CommandeDeCréation() Then
      Return True
    ElseIf CommandeDeSuppression() Then
      Return True
    ElseIf CommandeInformation() Then
      Return True
    ElseIf UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
      Return True
    End If
  End Function

  Private Function CommandeDeCréation(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique

    Select Case uneCommande
      Case CommandeGraphique.PassagePiéton, CommandeGraphique.PassagePiétonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.Traversée, CommandeGraphique.LigneFeux
        Return True
    End Select
  End Function

  Private Function CommandeDeSuppression(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu, CommandeGraphique.DécomposerTraversée
        Return True
    End Select
  End Function

  Private Function CommandeInformation(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.PropTrajectoire, CommandeGraphique.PropTraversée
        Return True
    End Select
  End Function

  Private Function CommandeConservantSélection(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPrécédent, CommandeGraphique.ZoomPAN
        Return True
      Case CommandeGraphique.Antagonisme
        Return True
    End Select
  End Function

  Private Function CommandeNécessitantSélection(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPrécédent, CommandeGraphique.ZoomPAN
        Return True
    End Select

  End Function

  Private Function PtTrajectoire(ByVal pSouris As Point)
    Dim uneBranche As Branche
    For Each uneBranche In mesBranches
      If uneBranche.PtIntérieur(pSouris) Then Return pSouris
    Next
  End Function

  '******************************************************************************
  ' Déterminer le nouveau point de la ligne de feu sur la bordure de la voie
  '******************************************************************************
  Private Function PtLigneFeuDéplacé(ByVal pSouris As Point) As Point
    Dim pOrigine As Point = mPoint(0)   ' Position de l'origine de la ligne de feu précédente
    'Projeter le pointeur souris sur le bord de la voie
    Dim pProjeté As Point = Projection(pSouris, Segment1)
    If Segment1.PtSurSegment(pProjeté) Then Return pProjeté
  End Function

  '******************************************************************************
  ' Déterminer le point final de la ligne de feux véhicules
  '******************************************************************************
  Private Function PtLigneFeux(ByVal pSouris As Point) As Point
    Dim p As Point
    If ContourPermis.Intérieur(pSouris) Then
      'If BrancheLiée.PtIntérieur(pSouris) Then
      p = Projection(pSouris, mPoint(0), AngleProjection)
    End If
    Return p
  End Function

  '******************************************************************************
  ' Déterminer si la nouvelle origine de la branche est acceptable
  '******************************************************************************
  Private Function OrigineBrancheOK(ByVal pSouris As Point) As Boolean

    OrigineBrancheOK = EnveloppeBranche.Intérieur(pSouris)
    OrigineBrancheOK = True
    GérerCurseur(OrigineBrancheOK)
    Return True
    Return OrigineBrancheOK

    '======================================================================================================================
    'Définition du segment intermédiaire joignant les 2 bords de chaussée (si on accepte pSouris comme nouvelle origine de la branche)
    Dim p1, p2 As Point
    'Nouvelle extrémité du bord de chaussée droite
    p1 = PointPosition(pSouris, LargeurBranche / 2, AngleBranche + sngPI / 2)
    Dim l1 As Ligne = New Ligne(p1, PointPosition(p1, AngleBranche))
    'Nouvelle extrémité du bord de chaussée gauche
    p2 = PointPosition(pSouris, LargeurBranche / 2, AngleBranche - sngPI / 2)
    Dim l2 As Ligne = New Ligne(p2, PointPosition(p2, AngleBranche))

    Dim SegmentIntermédiaire As Ligne = New Ligne(p1, p2, New Pen(Color.Red))

    If intersect(Segment1, SegmentIntermédiaire).IsEmpty Then
      'Le segment n'intersecte pas la branche précédente
      If intersect(Segment2, SegmentIntermédiaire).IsEmpty Then
        'Le segment n'intersecte pas la branche suivante
        If intersect(SegmentLimite, SegmentIntermédiaire).IsEmpty Then
          'Le segment n'intersecte pas le segment joignant les origines de la branche précédente et de la branche suivante
          If intersect(p1, Segment1.pA, p2, Segment2.pA, TypeInterSect:=Formules.TypeInterSection.SurSegment).IsEmpty Then
            'Il n'y a pas de rebroussement :les segments de raccordement de branche ne s'intersectent pas
            If intersect(Segment1, l1, Formules.TypeInterSection.SurPremierSegmentStrict).IsEmpty Then
              'Le prolongement du nouveau bord de chaussée droite n'intersecte pas le bord gauche précédent
              If intersect(Segment2, l2, Formules.TypeInterSection.SurPremierSegmentStrict).IsEmpty Then
                'Le prolongement du nouveau bord de chaussée gauche n'intersecte pas le bord droit précédent
                If AngleFormé(SegmentLimite.pA, pSouris, SegmentLimite.pB) >= 0 Then
                  'Sinon L'origine est passée de l'autre coté du carrefour
                  OrigineBrancheOK = True
                End If
              End If
            End If
          End If
        End If
      End If
    End If

    GérerCurseur(OrigineBrancheOK)

  End Function

  Private Sub GérerCurseur(ByVal OK As Boolean)

    If picDessin.Cursor Is Cursors.No Xor Not OK Then
      'Bacule OK<-->PASOK
      If OK Then
        'Bascule -->OK : déterminer le curseur approprié selon la commande graphique
        picDessin.Cursor = CurseurCommande()
      Else
        'Bascule -->PASOK : Sens interdit
        picDessin.Cursor = Cursors.No
      End If
    End If

  End Sub

  '******************************************************************************
  ' Déterminer si le nouvel angle est acceptable
  '******************************************************************************
  Private Function AngleBrancheOK(ByVal pSouris As Point) As Boolean

    Dim AngleFinal As Single = CvAngleDegrés(AngleFormé(mPoint(0), pSouris), SurDeuxPi:=False)
    If AngleFinal < AngleMini Then AngleFinal += 360
    AngleBrancheOK = (AngleFinal - AngleMini) < BalayageMaxi

    AngleProjection = EqvRadian(AngleFinal) + Math.PI / 2
    If AngleProjection > PI Then AngleProjection -= 2 * PI

    GérerCurseur(AngleBrancheOK)

  End Function

  '******************************************************************************
  ' Déterminer le nouveau point P1 ou P3 (ou son symétrique P4): Point gérant l'élargissement de l'ilot
  '******************************************************************************
  Private Function PtIlot(ByVal pSouris As Point) As Point
    Dim pOrigine As Point = mPoint(0)   ' P2
    Dim pProjeté As Point = Projection(pSouris, pOrigine, AngleProjection)

    If UneCommandeGraphique = CommandeGraphique.EtirerIlot Then
      'P1 à Au moins 1 m de P2 (Rayon >= 1m)
      If PointDansPicture(pProjeté) And DistanceRéelle(pProjeté, pOrigine) >= Ilot.miniRayon Then
        'P1 reste du même coté de l'axe P3P4
        If Sign(AngleProjection) = Sign(AngleFormé(pProjeté, pOrigine)) Then Return pProjeté
      End If
    Else
      'CommandeGraphique.ElargirIlot
      Dim Dist As Single = DistanceRéelle(pProjeté, pOrigine)
      If Dist < Ilot.maxiLargeur / 2 And Dist >= Ilot.miniLargeur / 2 Then Return pProjeté
    End If

  End Function

  '******************************************************************************
  ' Déterminer si le nouveau point P2 est acceptable : : Point gérant le déplacement de l'ilot
  '******************************************************************************
  Private Function P2IlotOK(ByVal pSouris As Point) As Boolean
    'Retrouver le point P2 futur en fonction de pSouris
    Dim NewP2 As Point = Translation(pSouris, DecalV(3))
    'Extrémité du bord droit de la chaussée
    Dim pOrigine As Point = mPoint(0)

    'Projeter le point sur le bord droit de la chaussée
    Dim pProjeté As Point = P6Ilot(NewP2)

    'Projeter le point sur l'axe de la branche
    Dim pSurAxe As Point = Projection(NewP2, BrancheLiée.LigneDeSymétrie)
    Dim Retrait As Single
    If DistanceRéelle(NewP2, pSurAxe) < BrancheLiée.Largeur / 2 Then
      Retrait = RetraitIlot(pOrigine, pProjeté)
      P2IlotOK = Retrait >= Ilot.miniRetrait And Retrait <= Ilot.maxiRetrait
    End If

    GérerCurseur(P2IlotOK)

  End Function

  Private Function RetraitIlot(ByVal pOrigine As Point, ByVal pProjeté As Point)
    Dim Retrait As Single = DistanceRéelle(pOrigine, pProjeté)
    If Retrait <> 0.0 Then
      Dim unAngle As Single = AngleFormé(pOrigine, pProjeté)
      'La soustraction ci-dessous retourne 0 ou PI : si ce n'est pas 0, c'est que l'ilot est rentré dans le carrefour
      If Abs(unAngle - BrancheLiée.AngleEnRadians) > 0.1 Then Retrait = -Retrait
    End If

    Return Retrait

  End Function

  '******************************************************************************
  ' Déterminer le nouveau point P6 à parti de P2(Point gérant le déplacement de l'ilot)
  '******************************************************************************
  Private Function P6Ilot(ByVal NewP2 As Point) As Point

    P6Ilot = Projection(NewP2, BrancheLiée.BordChaussée(Branche.Latéralité.Droite))

  End Function

  '******************************************************************************
  ' Déterminer le point suivant du passage piéton à dessiner
  '******************************************************************************
  Private Function PtPassage(ByVal pSouris As Point) As Point
    Dim p As Point

    Select Case mPoint.Length
      Case 1
        ' Définir le 2ème point issu du premier en restant parallèle à la branche
        p = Projection(pSouris, mPoint(0), BrancheLiée.AngleEnRadians)
        'Largeur du passage égale au moins à 2 m
        If DistanceRéelle(p, mPoint(0)) >= PassagePiéton.miniLargeur Then
          Return p
        End If

      Case 2
        ' Définir le 3ème point 
        p = pSouris
        'Projeter le point sur le bord de chasssée la plus proche : p est passé par référence
        Select Case BrancheLiée.BordChausséeProche(p)
          Case Branche.Latéralité.Aucune
            'Il suffit que le point soit à l'intérieur de la branche
            Return pSouris

          Case BordChausséePassage
            ' Refuser un point du même coté de la branche
          Case Else
            ' Retourner le point sur le bord de chaussée opposé
            Return p
        End Select

      Case Else
        'Point final

        If Distance(pSouris, mPoint(2)) > 0 Then
          Dim unAngle As Single = AngleFormé(mPoint(1), mPoint(2))
          p = Point.Ceiling(intersect(New Ligne(mPoint(2), pSouris, Nothing), New Ligne(mPoint(0), PointPosition(mPoint(0), unAngle), Nothing), TypeInterSect:=Formules.TypeInterSection.Indifférent))
          If Not p.IsEmpty Then
            If Sign(unAngle) = Sign(AngleFormé(mPoint(0), p)) Then Return p
          End If
        Else
          Return pSouris
        End If
    End Select

  End Function

  '******************************************************************************
  ' Déterminer le point suivant de la trajectoire à dessiner
  '******************************************************************************
  Private Function PTrajVeh(ByVal pSouris As Point) As Point
    Dim p As Point
    mScreen2 = picDessin.PointToScreen(pSouris)
  End Function

  '******************************************************************************
  ' Valider si le point cliqué est acceptable pour construire le passage
  '******************************************************************************
  Private Function EditPassageOK2(ByVal pSouris As Point) As Point
    Dim pProjeté As Point = Projection(pSouris, Segment1)

    Return pProjeté

  End Function

  '*************************************************************************************************
  ' Valider si le point cliqué est acceptable pour construire le passage
  ' AnglePassage : Changement de l'orientation du passage (angle des 2 cotés parallèles du trapèze)
  '**************************************************************************************************
  Private Function EditPassageOK3(ByVal pSouris As Point) As Point
    Dim p0, p2 As Point
    Dim unAngle As Single

    p0 = CvPoint(intersect(New Ligne(pSouris, mPoint(0)), Segment2, Formules.TypeInterSection.Indifférent))

    If Math.Sign(AngleFormé(p0, Segment1.pA, Segment1.pB)) = SigneConservé Then
      'Interdire de passer de l'autre coté du segment invariant

      unAngle = AngleFormé(mPoint(0), p0)
      Dim l1 As New Ligne(Segment1.pB, PointPosition(Segment1.pB, 100.0, unAngle))
      p2 = CvPoint(intersect(l1, Segment2, Formules.TypeInterSection.Indifférent))
      If DistanceRéelle(p0, p2) >= PassagePiéton.miniLargeur AndAlso DistanceRéelle(Segment1.pA, l1) >= PassagePiéton.miniLargeur Then
        AngleParallèle = unAngle
        mScreen(2) = picDessin.PointToScreen(p2)
        picDessin.Cursor = CurseurCommande()
        Return p0
      End If
    End If

  End Function

  '*****************************************************************************************
  ' Valider si le point cliqué est acceptable pour construire le passage
  ' PointPassage : déplacement d'un point
  ' EditLargeurPassage, EditLongueurPassage : élargissement ou allongement du passage
  '*****************************************************************************************
  Private Function EditPassageOK4(ByVal pSouris As Point) As Point
    Dim pASegment1, pBSegment2 As Point
    Dim L1, L2 As Ligne

    'Controle préalable sur la largeur du passage piéton
    If UneCommandeGraphique <> CommandeGraphique.EditLongueurPassage AndAlso DistanceRéelle(pSouris, SegmentLimite) < PassagePiéton.miniLargeur Then
      Return pASegment1
    End If

    If UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
      pASegment1 = pSouris
      'Segment2 : segment sur lequel coulisse le point opposé au point en cours de déplacement (pSouris)

    Else
      'EditLargeurPassage, EditLongueurPassage
      'Segment1 et Segment2 : segments sur lesquels coulissent les 2 points dont on déplace le milieu(pSouris)
      pASegment1 = CvPoint(intersect(New Ligne(pSouris, PointPosition(pSouris, 100.0, AngleParallèle)), Segment1, Formules.TypeInterSection.Indifférent))
    End If

    pBSegment2 = CvPoint(intersect(New Ligne(pSouris, PointPosition(pSouris, 100.0, AngleParallèle)), Segment2, Formules.TypeInterSection.Indifférent))

    L1 = New Ligne(pSouris, mPoint(0))
    L2 = New Ligne(pBSegment2, Segment2.pA)  ' pBSegment2 est sur Segment2 : L2 est le futur Segment2

    If CvPoint(intersect(L1, L2)).IsEmpty Then
      'Le point en cours de déplacement ne doit pas traverser le petit coté opposé
      If Math.Sign(AngleFormé(pSouris, SegmentLimite.pA, SegmentLimite.pB)) = SigneConservé Then
        'Conserver le sens des points :ceci empeche le grand coté en cours de modif (parallèle à SegmentLimite) à passer de l'autre coté de SegmentLimite
        'SegmentLimite : Segment invariant du passage (segment opposé au point en cours de modification)
        'Le futur Segment1 : (pSouris, mPoint(0)=Segment1.PB) - Recalculer la nouvelle bissectrice
        If UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
          AngleProjection = (AngleFormé(pSouris, mPoint(0)) + AngleFormé(SegmentLimite)) / 2
          picDessin.Cursor = CurseurCommande()
        End If

        mScreen(2) = picDessin.PointToScreen(pBSegment2)
        Return pASegment1
      End If

    End If

  End Function

  '******************************************************************************
  ' Terminer la commande graphique
  '******************************************************************************
  Private Function TerminerCommande(ByVal pMouseUp As Point) As Boolean
    Dim uneLigne As Ligne
    Dim uneBranche As Branche
    Dim unIlot As Ilot
    'FermerPassage : ne sert que pour la commande PassagePiéton
    Static FermerPassage As Boolean

    Try
      Select Case UneCommandeGraphique
        'NORD
      Case CommandeGraphique.OrienterNord
          Dim unNord As Nord = objSélect.ObjetMétier
          colObjetsGraphiques.Remove(unNord.mGraphique)
          unNord.Orientation = AngleFormé(CentreRotation, pMouseUp)
          unNord.CréerGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

        Case CommandeGraphique.DéplacerNord
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV(0))
          Dim unNord As Nord = objSélect.ObjetMétier
          colObjetsGraphiques.Remove(unNord.mGraphique)
          unNord.PtRéférence = p
          unNord.CréerGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

          'ECHELLE
        Case CommandeGraphique.DéplacerEchelle
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV(0))
          'LEs coordonnées du point de référence de l'échelle sont relatives au point bas gauche : il faut inverser le sens des Y
          p.Y = picDessin.ClientSize.Height - p.y
          Dim uneEchelle As SymEchelle = objSélect.ObjetMétier
          colObjetsGraphiques.Remove(uneEchelle.mGraphique)
          uneEchelle.PtRéférence = p
          uneEchelle.CréerGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

          'BRANCHE
        Case CommandeGraphique.OrigineBranche
          DessinerElastique()
          If OrigineBrancheOK(pMouseUp) Then
            Dim p As Point = Translation(pMouseUp, DecalV(0))
            If PointDansPicture(p) Then
              uneBranche = objSélect.ObjetMétier
              uneLigne = uneBranche.LigneDeSymétrie
              With uneLigne
                .pA = p
                .pB = Translation(pMouseUp, DecalV(1))
                uneBranche.AttribuerOrigine(PointRéel(.pA))
                RedessinerBranche(uneBranche)
              End With
            End If
            Modif = True
          Else
            Désélectionner()
          End If
          TerminerCommande = True

        Case CommandeGraphique.AngleBranche
          uneBranche = objSélect.ObjetMétier
          uneLigne = uneBranche.LigneDeSymétrie
          If AngleBrancheOK(pMouseUp) Then
            uneBranche.Angle = CvAngleDegrés(AngleFormé(CentreRotation, pMouseUp))
            Dim uneGrille As GrilleDiagfeux = Me.AC1GrilleBranches
            Dim uneCellule As Grille.CellRange = uneGrille.GetCellRange(mesBranches.IndexOf(uneBranche) + 1, 2)
            uneCellule.Data = Math.Round(uneBranche.Angle)
            RedessinerBranche(uneBranche)
            Modif = True
          Else
            DessinerElastique()
            Désélectionner()
          End If
          TerminerCommande = True

          'EDITION ILOT
        Case CommandeGraphique.EtirerIlot, CommandeGraphique.DéplacerIlot, CommandeGraphique.ElargirIlot
          ' Mémoriser la position courante de la souris
          ' en Coordonnées du PictureBox 
          ReDim Preserve mPoint(mPoint.Length)
          mPoint(mPoint.Length - 1) = pMouseUp
          unIlot = objSélect.ObjetMétier
          If RedéfinirIlot(unIlot) Then
            Modif = True
          End If
          TerminerCommande = True

          'PASSAGE PIETON
        Case CommandeGraphique.PassagePiétonRapide
          TerminerPassage(New PassagePiéton(BrancheLiée))
          TerminerCommande = True

        Case CommandeGraphique.PassagePiéton
          Dim p As Point = PtPassage(pMouseUp)
          If Not p.IsEmpty Then
            ' Mémoriser la position courante de la souris
            ' en Coordonnées du PictureBox 
            ReDim Preserve mPoint(mPoint.Length)
            mPoint(mPoint.Length - 1) = p
            ReDim Preserve mScreen(mScreen.Length)
            mScreen1 = mScreen2 ' picDessin.PointToScreen(p)
            mScreen(mScreen.Length - 1) = mScreen1
            If mPoint.Length = DernierPoint() Then
              objSélect = CréerPassage(FermerPassage)
              FermerPassage = False
              TerminerCommande = True
            Else
              If mScreen.Length = 3 And IsNothing(monFDP) Then
                If BrancheLiée.BordChausséeProche(p) <> Branche.Latéralité.Aucune Then
                  FermerPassage = True
                  TerminerCommande = TerminerCommande(PointPosition(p, CType(AngleFormé(mPoint(1), mPoint(0)), Single)))
                Else
                  ControlPaint.DrawReversibleLine(mScreen1, mScreen(0), Color.Gray)
                End If
              End If
              TraiterMessageGlisser()
            End If
          End If

        Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage
          DessinerElastique()
          Dim p As Point
          If UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
            p = EditPassageOK4(pMouseUp)
          ElseIf UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Or UneCommandeGraphique = CommandeGraphique.EditLongueurPassage Then
            p = EditPassageOK4(pMouseUp)
          Else  'AnglePassage
            p = EditPassageOK3(pMouseUp)
          End If
          If Not p.IsEmpty Then

            Dim unPassage As PassagePiéton = objSélect.ObjetMétier
            unPassage.AffecterPoint(p, PoignéeCliquée, RedéfinirVoies:=True)
            'Poignée symétrique  à modifier en conséquence
            p = picDessin.PointToClient(mScreen(2))
            If UneCommandeGraphique = CommandeGraphique.EditPointPassage Or UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Then        'La poignée symétrique est sur un grand coté (un des 2 cotés parallèles du trapèze)
              PoignéeCliquée = 3 - PoignéeCliquée

            Else
              'La poignée cliquée est sur un petit coté (proche du bord de chaussée ou éventuellement d'un ilot)
              Select Case PoignéeCliquée
                Case 0
                  PoignéeCliquée = 1
                Case 1
                  PoignéeCliquée = 0
                Case 2
                  PoignéeCliquée = 3
                Case 3
                  PoignéeCliquée = 2
              End Select
            End If
            unPassage.AffecterPoint(p, PoignéeCliquée, RedéfinirVoies:=True)
            objSélect = DessinerPassage(unPassage)
            Modif = True

            TerminerCommande = True
          End If

        Case CommandeGraphique.DéplacerPassage
          DessinerElastique()
          Dim p As Point = EditPassageOK2(pMouseUp)
          If Not p.IsEmpty Then
            Dim unPassage As PassagePiéton = objSélect.ObjetMétier
            Dim i As Short
            For i = 0 To 3
              unPassage.AffecterPoint(Translation(p, DecalV(i)), i, RedéfinirVoies:=False)
            Next
            objSélect = DessinerPassage(unPassage)
            Modif = True
          Else
            MessageBox.Show("Le passage sort des limites de la branche")
          End If
          TerminerCommande = True

          'TRAJECTOIRE
        Case CommandeGraphique.Trajectoire
          VoieOrigine = VoieTraj
          Dim BrancheDestination As Branche = BrancheProche(pMouseUp)
          If IsNothing(VoieTraj) Then
            AfficherMessageErreur(Me, "Cliquer entre les 2 bords d'une voie")
          ElseIf VoieTraj.Entrante Then
            AfficherMessageErreur(Me, "Désigner une voie sortante")
          ElseIf VoieTraj.mBranche Is VoieOrigine.mBranche Then
            AfficherMessageErreur(Me, "Désigner une voie sur une autre branche")
          ElseIf mesTrajectoires.Existe(VoieOrigine, VoieTraj) Then
            AfficherMessageErreur(Me, "Cette trajectoire est déjà définie")
            EffacerElastiques()
            TerminerCommande = True
          Else
            objSélect = CréerTrajectoire()
            TerminerCommande = True
          End If
          If IsNothing(objSélect) Then VoieTraj = VoieOrigine

        Case CommandeGraphique.EditerTrajectoire
          If EnAttenteMouseUp Then
            EnAttenteMouseUp = False
          Else
            Dim p As Point = PtEditTrajectoire(pMouseUp)
            If Not p.IsEmpty Then
              If Distance(p, mPoint1) > RaySélect Then
                'Commande non terminée : mémorisation du point cliqué et préparationde la saisie du suivant
                Dim pScreen As Point = picDessin.PointToScreen(pMouseUp)
                'Effacer le ou les segments précédents
                DessinerElastique()
                'Dessiner en couleur le segment créé
                ControlPaint.DrawReversibleLine(mScreen(mScreen.Length - 3), pScreen, Color.Fuchsia)
                'Rajouter un point au tableau d'élastiques
                ReDim Preserve mScreen(mScreen.Length)
                mScreen(mScreen.Length - 2) = pScreen
                mScreen(mScreen.Length - 1) = mScreen2
                DessinerElastique()

              Else
                'L'utilisateur a cliqué sur le point d'accès destination pour terminer la commande
                DessinerElastique()
                ControlPaint.DrawReversibleLine(mScreen(mScreen.Length - 3), mScreen2, Color.Fuchsia)
                ReDim Preserve mScreen(mScreen.Length - 2)
                'Ajouter le point d'accès à la branche destination
                mScreen(mScreen.Length - 1) = mScreen2
                CréerTrajectoire()
                TerminerCommande = True
              End If
            End If
          End If

        Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire
          Dim p As Point = PtEditTrajectoire(pMouseUp)
          If Not p.IsEmpty Then
            DessinerElastique()
            Dim uneTrajectoire As TrajectoireVéhicules = objSélect.ObjetMétier

            Select Case UneCommandeGraphique
              Case CommandeGraphique.EditerOrigineTrajectoire
                uneTrajectoire.AffecterPointAccès(picDessin.PointToClient(mScreen(0)), TrajectoireVéhicules.OrigineDestEnum.Origine)
              Case CommandeGraphique.EditerDestinationTrajectoire
                uneTrajectoire.AffecterPointAccès(picDessin.PointToClient(mScreen(0)), TrajectoireVéhicules.OrigineDestEnum.Destination)
              Case Else
                uneTrajectoire.AffecterPointIntermédiaire(picDessin.PointToClient(mScreen(1)), PoignéeCliquée)
            End Select

            DessinerTrajectoire(uneTrajectoire)
            TerminerCommande = True
          End If

          'TRAVERSEE PIETONNE
        Case CommandeGraphique.Traversée
          If BrancheLiée.mPassages.Count = 1 Then
            AfficherMessageErreur(Me, "Cette branche ne comporte qu'un seul passage piéton")
          Else
            maVariante.CréerTraversée(BrancheLiée, colObjetsGraphiques)
            TerminerCommandeTraversée()
          End If
          TerminerCommande = True

        Case CommandeGraphique.DécomposerTraversée
          If Not Traversée.mDouble Then
            AfficherMessageErreur(Me, "Cette traversée ne comporte qu'un seul passage piéton")
          Else
            maVariante.DécomposerTraversée(Traversée, colObjetsGraphiques)
            TerminerCommandeTraversée()
          End If
          TerminerCommande = True

        Case CommandeGraphique.PropTraversée
          dialogueTrajPiétons(Traversée)
          TerminerCommande = True

          ' LIGNE DE FEUX
        Case CommandeGraphique.LigneFeux
          Dim p As Point = PtLigneFeux(pMouseUp)
          If Not p.IsEmpty Then
            mPoint(1) = p
            CréerLigneDeFeux()
            TerminerCommande = True
          End If

        Case CommandeGraphique.DéplacerLigneFeu
          Dim p As Point = PtLigneFeuDéplacé(pMouseUp)
          If p.IsEmpty Then
            DessinerElastique("Terminer")
            DessinerObjet(LigneFeuEnCours.mGraphique)
          Else
            Dim p2 As Point = Translation(p, DecalV(0))
            LigneFeuEnCours.Décalage = Distance(PointDessin(BrancheLiée.Origine), New Ligne(p, p2)) / Echelle
            Modif = True
            DessinerLigneDeFeux(LigneFeuEnCours)
            objSélect = LigneFeuEnCours.mGraphique
          End If
          TerminerCommande = True

        Case CommandeGraphique.AllongerFeu
          Dim p As Point = PtLigneFeuDéplacé(pMouseUp)
          If p.IsEmpty Then
            DessinerElastique()
            DessinerObjet(LigneFeuEnCours.mGraphique)
          Else
            mPoint(1) = p
            objSélect = ModifierLigneDeFeux()
            TerminerCommande = True
          End If

        Case CommandeGraphique.DéplacerSignal
          objSélect = SignalDéplacé(pMouseUp)
          TerminerCommande = True


          'CARREFOUR
        Case CommandeGraphique.DéplacerCarrefour
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV1)
          If PointDansPicture(p) Then
            PositionnerCarrefour(p)
          End If
          objSélect = Nothing
          TerminerCommande = True

          'Suppression d'un objet métier
        Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu
          SupprimerObjetMétier()

          'ZOOMS...
        Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPrécédent, CommandeGraphique.ZoomPAN
          If UneCommandeGraphique = CommandeGraphique.ZoomPrécédent Then
            mEchelles.Remove((mEchelles.Count - 1).ToString)
            mParamDessin = mEchelles((mEchelles.Count - 1).ToString)
          ElseIf UneCommandeGraphique = CommandeGraphique.ZoomPAN Then
            If Distance(pMouseUp, mPoint(0)) < RaySélect Then Exit Function

            pMouseUp = Point.op_Subtraction(mPoint(0), Point.op_Explicit(pMouseUp))
            mParamDessin = DéterminerNewOrigineRéellePAN(pMouseUp)
            mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
          Else
            mParamDessin = DéterminerNewOrigineRéelle(pMouseUp, ZoomPlus:=UneCommandeGraphique = CommandeGraphique.Zoom)
            mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
          End If
          cndParamDessin = mParamDessin
          RecréerGraphique()

          If IsNothing(objSélect) Then
            Redessiner()
          Else
            Redessiner(ObjetASélectionner:=objSélect.ObjetMétier.mGraphique)
          End If

          mdiApplication.tbrDiagfeux.Buttons(MDIDiagfeux.BarreOutilsEnum.ZoomPrécédent).Visible = mEchelles.Count > 1
          TerminerCommande = True

        Case CommandeGraphique.Mesure
          DessinerElastique()
          MessageBox.Show("Distance : " & mdiApplication.staDiagfeux.Panels(1).Text)
          TerminerCommande = True
      End Select

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

    If TerminerCommande And Not IsNothing(objSélect) Then
      objSélect.Dessiner(mBufferGraphics, picDessin.CreateGraphics)
    End If

  End Function

  Private Sub TerminerCommandeTraversée()
    AfficherLignesDeFeux()
    Modif = True
    Redessiner()

  End Sub

  '******************************************************************************
  ' Positionner le carrefour en fonction de son nouveau centre
  '******************************************************************************
  Private Sub PositionnerCarrefour(ByVal pSouris As Point)
    If Distance(pSouris, PointDessin(maVariante.mCarrefour.mCentre)) > RaySélect Then
      maVariante.mCarrefour.mCentre = PointRéel(pSouris)
      PositionnerCarrefour()
    End If

  End Sub

  Private Sub PositionnerCarrefour()
    Dim uneBranche As Branche
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In mesTrajectoires
      If uneTrajectoire.EstVéhicule Then
        CType(uneTrajectoire, TrajectoireVéhicules).Réinitialiser(ConserverManuel:=False)
      Else
        CType(uneTrajectoire, TraverséePiétonne).Réinitialiser(False)
      End If
    Next

    RecréerGraphique()
    Redessiner()
  End Sub

  '******************************************************************************
  ' Positionner le signal de feu en fonction de son nouveau point d'insertion
  '******************************************************************************
  Private Function SignalDéplacé(ByVal pSouris As Point) As Graphique
    EffacerElastiques()

    With SignalFeuEnCours
      Dim p As Point = .PtRéférence
      .Position = New Point(pSouris.X - p.X, pSouris.Y - p.Y)
      .CréerGraphique(colObjetsGraphiques)
      DessinerObjet(.mGraphique)
      Return .mGraphique
    End With

  End Function

  '******************************************************************************
  ' Supprimer un objet métier (Passage piéton, ligne de feux.....)
  '       - Supprime également l(es) objets(s) graphique(s) associé(s)
  '******************************************************************************
  Private Sub SupprimerObjetMétier()
    Dim unPolyArc As PolyArc = objSélect
    colObjetsGraphiques.Remove(unPolyArc)
    Dim ObjetMétier As Métier
    Dim ObjetSupplémentaire, ObjetSupplémentaire2 As PolyArc

    Select Case UneCommandeGraphique
      Case CommandeGraphique.SupprimerPassage
        ObjetMétier = unPolyArc.ObjetMétier
        Dim unPassage As PassagePiéton = ObjetMétier
        ObjetSupplémentaire = unPassage.Zebras
        unPassage.mBranche.mPassages.Remove(unPassage)
        If maVariante.Verrou = [Global].Verrouillage.Géométrie Then
          'Supprimer également la traversée piétonne et la ligne feux associée
          Traversée = unPassage.mTraversée
          'Supprimer la ligne de feux
          mesLignesFeux.Remove(Traversée.LigneFeu, colObjetsGraphiques)
          'Réafficher les lignes de feux en conséquence
          AfficherLignesDeFeux()
          If Traversée.mDouble Then
            maVariante.DécomposerTraversée(Traversée, colObjetsGraphiques)
          End If
          'Supprimer la traversée
          mesTrajectoires.Remove(unPassage.mTraversée, colObjetsGraphiques)
        End If
        maVariante.mPassagesEnAttente.Remove(unPassage)

      Case CommandeGraphique.SupprimerTrajectoire
        ObjetMétier = unPolyArc.ObjetMétier
        Dim uneTrajectoire As TrajectoireVéhicules = ObjetMétier
        mesTrajectoires.Remove(uneTrajectoire)
        AfficherLignesDeFeux()

      Case CommandeGraphique.SupprimerLigneFeu
        ObjetMétier = unPolyArc.ObjetMétier
        Dim uneLigneFeux As LigneFeuVéhicules = ObjetMétier
        ObjetSupplémentaire = uneLigneFeux.mSignalFeu(0).mGraphique
        If uneLigneFeux.EstPiéton Then
          ObjetSupplémentaire2 = uneLigneFeux.mSignalFeu(1).mGraphique
        End If
        mesLignesFeux.Remove(uneLigneFeux)
        AfficherLignesDeFeux()
    End Select

    If Not IsNothing(ObjetMétier) Then
      ' Déselectionner l'objet
      SélDésélectionner()
      ' L'effacer de l'écran
      EffacerObjet(unPolyArc)
      ' Retirer sa représentation graphique des objets à dessiner
      colObjetsGraphiques.Remove(unPolyArc)
      ' Faire la même chose s'il y a un objet grahique supplémentaire associé à l'objet métier
      If Not IsNothing(ObjetSupplémentaire) Then
        EffacerObjet(ObjetSupplémentaire)
        colObjetsGraphiques.Remove(ObjetSupplémentaire)
        If Not IsNothing(ObjetSupplémentaire2) Then
          EffacerObjet(ObjetSupplémentaire2)
          colObjetsGraphiques.Remove(ObjetSupplémentaire2)
        End If
      End If
      objSélect = Nothing
      Modif = True
    End If

    savObjSélect = Nothing

  End Sub

  '******************************************************************************
  ' Refaire le dessin suite à l'édition de la branche 
  '******************************************************************************
  Private Sub RedessinerBranche(ByVal uneBranche As Branche)
    mesTrajectoires.Réinitialiser(ConserverManuel:=True)
    maVariante.CréerGraphique(colObjetsGraphiques)
    Redessiner(ObjetASélectionner:=uneBranche.mGraphique)
  End Sub

  '******************************************************************************
  ' Redéfinir l'ilot et le Redessiner
  ' Retourne True si la construction est autorisée
  '******************************************************************************
  Private Function RedéfinirIlot(ByVal unIlot As Ilot) As Boolean
    Dim OK As Boolean
    Dim P1, P3, p As Point
    Dim pSouris As Point = mPoint(1)
    'pOrigine : Point P2 sauf pour DéplacerIlot( Extrémité du bord droit de la chaussée)
    Dim pOrigine As Point = mPoint(0)

    DessinerElastique()
    If UneCommandeGraphique = CommandeGraphique.DéplacerIlot Then
      If P2IlotOK(pSouris) Then
        OK = True
      Else
        pSouris = picDessin.PointToClient(Milieu(mScreen(1), mScreen(2)))
      End If

    Else
      p = PtIlot(pSouris)
      If UneCommandeGraphique = CommandeGraphique.EtirerIlot Then
        P1 = p
      Else
        P3 = p
      End If
      OK = Not p.IsEmpty
    End If

    If OK Then
      Dim fg As GrilleDiagfeux = Me.AC1GrilleIlot

      With unIlot
        Dim IndexIlot As Short = mesBranches.IndexIlot(unIlot)
        If UneCommandeGraphique = CommandeGraphique.EtirerIlot Then
          .Rayon = DistanceRéelle(pOrigine, P1)
          fg(IndexIlot, 1) = Format(.Rayon, "#0.0")
        ElseIf UneCommandeGraphique = CommandeGraphique.DéplacerIlot Then
          Dim NewP2 As Point = Translation(pSouris, DecalV(3))
          Dim pProjeté As Point = P6Ilot(NewP2)
          .Décalage = DistanceRéelle(NewP2, pProjeté)
          .Retrait = RetraitIlot(pOrigine, pProjeté)
          fg(IndexIlot, 2) = Format(.Décalage, "#0.0")
          fg(IndexIlot, 4) = Format(.Retrait, "#0.0")
        ElseIf UneCommandeGraphique = CommandeGraphique.ElargirIlot Then
          .Largeur = 2 * DistanceRéelle(pOrigine, P3)
          fg(IndexIlot, 3) = Format(.Largeur, "#0.0")
        End If
        .CréerGraphique(colObjetsGraphiques)
      End With
      Redessiner(ObjetASélectionner:=unIlot.mGraphique)
    Else
      Désélectionner()
    End If

    Return OK

  End Function

  '******************************************************************************
  ' Effacer les élastiques dessinés pendan la commande graphique
  '******************************************************************************
  Private Sub EffacerElastiques()
    Dim Index As Short

    If mScreen.Length > 0 Then
      'Effacer tous les segments de travail
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePiéton, CommandeGraphique.Trajectoire
          Select Case mScreen.Length
            Case 1
              DessinerElastique()
            Case 2
              ControlPaint.DrawReversibleLine(mScreen(0), mScreen(1), Color.Gray)
              DessinerElastique()
            Case 3
              ControlPaint.DrawReversibleLine(mScreen(0), mScreen(1), Color.Gray)
              ControlPaint.DrawReversibleLine(mScreen(1), mScreen(2), Color.Gray)
              ControlPaint.DrawReversibleLine(mScreen(2), mScreen2, Color.Gray)
              ControlPaint.DrawReversibleLine(mScreen(0), mScreen2, Color.Gray)
          End Select

        Case Else
          DessinerElastique()
      End Select

    End If

  End Sub

  '******************************************************************************
  ' Créer une ligne de feux
  ' Retourne l'objet graphique associé
  '******************************************************************************
  Private Function CréerLigneDeFeux(Optional ByVal uneLigneVéhicules As LigneFeuVéhicules = Nothing) As PolyArc
    Dim uneVoie As Voie
    Dim Création As Boolean = IsNothing(uneLigneVéhicules)

    Try

      'Instancier la ligne de feux
      If Création Then
        'Le paramètre Nothing indique qu'il faudra générer l'ID
        uneLigneVéhicules = New LigneFeuVéhicules(Nothing, BrancheLiée, cndSignaux.DéfautVéhicule)
      Else
        uneLigneVéhicules.Voies.Clear()
      End If

      Dim uneLigne As Ligne = New Ligne(mPoint(0), mPoint(1))
      Dim Message As String
      'Insérer les voies dans le même ordre que l'ordre  des voies dans la branche (ceci en parcourantt les voies de la branche
      For Each uneVoie In BrancheLiée.Voies
        If uneVoie.Entrante Then
          'La voie est prise en compte si elle est entrante et que le dessin coupe son axe
          If Not intersect(uneLigne, uneVoie.Axe).IsEmpty Then
            uneLigneVéhicules.Voies.Add(uneVoie)
          End If
        End If
      Next

      If uneLigneVéhicules.Voies.Count = 0 Then
        Message = "La ligne de feux n'intersecte aucune voie"
      ElseIf Création Then
        If Not IsNothing(mesLignesFeux.VoiesCoupées(uneLigneVéhicules.Voies)) Then
          Message = "Une voie ne peut pas être commandée par plusieurs lignes de feux"
        End If
      End If

      If Not IsNothing(Message) Then
        AfficherMessageErreur(Me, Message)
        DessinerElastique()
      Else

        uneLigneVéhicules.Décalage = Distance(PointDessin(BrancheLiée.Origine), New Ligne(mPoint(1), mPoint(0))) / Echelle
        uneLigneVéhicules.DéterminerNatureCourants(mesTrajectoires)

        'Ajouter la ligne à la collection
        If Création Then
          Dim PositionInsertion As Short = mesLignesFeux.PremièreLigneVéhiculeDispo
          mesLignesFeux.Insert(PositionInsertion, uneLigneVéhicules)
          InsérerLigneDeFeux(PositionInsertion, uneLigneVéhicules)
          ActiverBoutonsLignesFeux()
          uneLigneVéhicules.PositionnerSignal()
        Else
          AfficherLigneDeFeux(uneLigneVéhicules)
        End If

        DessinerLigneDeFeux(uneLigneVéhicules)

        Modif = True

        Return uneLigneVéhicules.mGraphique

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Créer ligne de feux")
    End Try
  End Function

  '******************************************************************************
  ' Modifier une ligne de feux
  ' Retourne l'objet graphique associé
  '******************************************************************************
  Private Function ModifierLigneDeFeux() As PolyArc
    Dim sauvTAD, sauvTD, sauvTAG As Boolean

    With LigneFeuEnCours
      Dim mVoies As New VoieCollection
      Dim uneVoie As Voie
      'Mémoriser les propriétés de la ligne de feux
      For Each uneVoie In .Voies
        mVoies.Add(uneVoie)
      Next
      sauvTAD = .TAD
      sauvTD = .TD
      sauvTAG = .TAG

      'Redéfinir les nouvelles voies coupées
      ModifierLigneDeFeux = CréerLigneDeFeux(LigneFeuEnCours)

      If IsNothing(ModifierLigneDeFeux) Then
        'La mise à jour a échoué car la ligne de feux n'intersecte aucune voie
        Dim Index As Short
        For Each uneVoie In mVoies
          .Voies.Add(uneVoie)
        Next

        .TAD = sauvTAD
        .TD = sauvTD
        .TAG = sauvTAG
        ModifierLigneDeFeux = LigneFeuEnCours.mGraphique
      End If

    End With

  End Function

  '******************************************************************************
  ' Dessiner une ligne de feux ainsi que son signal
  ' Retourne l'objet graphique associé
  '******************************************************************************
  Private Sub DessinerLigneDeFeux(ByVal uneLigneFeux As LigneFeux)
    Dim unPolyarc As PolyArc

    With uneLigneFeux
      'Créer le dessin de la  ligne de feux et son signal associé
      unPolyarc = .CréerGraphique(colObjetsGraphiques)
      If uneLigneFeux.EstVéhicule Then
        'Dessiner les 2 objets
        DessinerObjet(.mGraphique)
        'If .SignalDessinable Then
        DessinerObjet(.mSignalFeu(0).mGraphique)
        'End If

      Else
        'Dessiner les 2 signaux
        'If .SignalDessinable Then
        DessinerObjet(.mSignalFeu(0).mGraphique)
        If CType(uneLigneFeux, LigneFeuPiétons).SignalAReprésenter(1) Then
          DessinerObjet(.mSignalFeu(1).mGraphique)
        End If
        'End If
      End If
    End With

  End Sub

  '******************************************************************************
  ' Créer une trajectoire véhicules
  ' Retourne l'objet graphique associé
  '******************************************************************************
  Private Function CréerTrajectoire() As PolyArc
    Dim natCourant As TrajectoireVéhicules.NatureCourantEnum
    Dim typeCourant As TrajectoireVéhicules.TypeCourantEnum
    Dim coefGêne As Single

    FenetreAideCommande.Hide()


    Dim uneTrajectoire As TrajectoireVéhicules
    If UneCommandeGraphique = CommandeGraphique.Trajectoire Or UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires Then
      uneTrajectoire = dialogueTrajVéhicules(Nothing)
    Else
      uneTrajectoire = dialogueTrajVéhicules(CType(objSélect.ObjetMétier, TrajectoireVéhicules))
    End If

    If Not IsNothing(uneTrajectoire) Then
      DessinerTrajectoire(uneTrajectoire)
      'La ligne qui suit peut poser des pb en sélectionnant automatiquement la trajectoire
      'CréerTrajectoire = uneTrajectoire.mGraphique
    End If

  End Function

  '******************************************************************************
  ' Afficher la boite de dialogue traversée piétonne 
  ' Permet de construire une traversée à partir de 2 passages piétons ou inversement
  '******************************************************************************
  Private Sub dialogueTrajPiétons(ByVal uneTraversée As TraverséePiétonne)
    Dim dlg As New dlgTrajPiéton
    Dim uneBranche As Branche = uneTraversée.mBranche

    With dlg
      .chkTraverséeDouble.Enabled = uneBranche.mPassages.Count > 1
      .chkTraverséeDouble.Checked = uneTraversée.EnDeuxTemps
      .txtLgTraversée.Text = Format(uneTraversée.LgMaximum, "#0.00")
      .txtMédiane.Text = Format(uneTraversée.LgMédiane, "#0.00")
      .lblBranche.Text = uneBranche.NomRue & " Branche " & mesBranches.ID(uneBranche)

      If maVariante.Verrou <> [Global].Verrouillage.Géométrie Then
        'les caractéristiques de la trajectoire ne sont plus modifiables
        .chkTraverséeDouble.Enabled = False
        .btnOK.Enabled = False
      End If

      'Saisir les caractéristiques de la traversée
      If .ShowDialog(Me) = DialogResult.OK And .Modif Then
        If uneTraversée.mDouble Then
          maVariante.DécomposerTraversée(uneTraversée, colObjetsGraphiques)
        Else
          maVariante.CréerTraversée(uneBranche, colObjetsGraphiques)
        End If
        TerminerCommandeTraversée()
      End If

      .Dispose()
    End With

  End Sub

  '******************************************************************************
  ' Afficher la boite de dialogue trajectoire véhicules
  '******************************************************************************
  Private Function dialogueTrajVéhicules(ByVal uneTrajectoire As TrajectoireVéhicules) As Trajectoire
    Dim VoieDestination As Voie
    Dim typeCourant As TrajectoireVéhicules.TypeCourantEnum
    Dim Création As Boolean = IsNothing(uneTrajectoire)
    Dim unCourant As Courant
    Dim OK As Boolean
    Dim ManuelDéCoché As Boolean

    If Création Then
      typeCourant = TrajectoireVéhicules.TypeCourantEnum.TypeCourantMixte
      VoieDestination = VoieTraj
    Else
      With uneTrajectoire
        typeCourant = .TypeCourant
        VoieOrigine = .Voie(TrajectoireVéhicules.OrigineDestEnum.Origine)
        VoieDestination = .Voie(TrajectoireVéhicules.OrigineDestEnum.Destination)
      End With
    End If


    unCourant = maVariante.mCourants(VoieOrigine.mBranche, VoieDestination.mBranche)

    If UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires Then
      OK = True

    Else
      Dim dlg As New dlgTrajVeh

      With dlg
        .pnlManuel.Visible = Not Création

        'Intialiser les champs de la boite de dialogue
        .mTypeCourant = typeCourant
        .mCourant = unCourant
        If Création Then
          .Création = True
        Else
          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            .chkManuel.Checked = True
          Else
            .chkManuel.Checked = uneTrajectoire.Manuel
          End If
        End If
        .lblListeAccès.Text = "Trajectoire depuis " & VoieOrigine.Libellé & " vers " & VoieDestination.Libellé

        'Les 2 instructions qui suivent sont en attendant de comprendre pourquoi la boite de dialogue efface un bout du dessin en s'affichant
        '    .StartPosition = FormStartPosition.Manual
        '   .Location = New Point(Me.pnlLignesDeFeux.Location.X, 150)

        If maVariante.Verrou <> [Global].Verrouillage.Géométrie Then
          'les caractéristiques de la trajectoire ne sont plus modifiables
          .pnlManuel.Enabled = False
          .pnlPropTrajectoire.Enabled = False
          .btnOK.Enabled = False
        End If

        Dim unAngle As Single = VoieOrigine.mBranche.AngleEnRadians - VoieDestination.mBranche.AngleEnRadians
        If unAngle <= -PI Then
          unAngle += 2 * PI
        ElseIf unAngle > PI Then
          unAngle -= 2 * PI
        End If
        If CvAngleRadians(110) <= Abs(unAngle) Then
          .ToutDroitPossible = True
        End If

        'Saisir les caractéristiques de la trajectoire
        Select Case .ShowDialog(Me)
          Case DialogResult.Retry
            'L'utilisateur a demandé à redessiner manuellement : drapeau pour exécuter la commande EditerTrajectoire en retour
            uneTrajectoire = Nothing

          Case DialogResult.Cancel

          Case DialogResult.OK
            OK = True
            typeCourant = .mTypeCourant
            unCourant.NatureCourant = .mCourant.NatureCourant
            unCourant.CoefGêne = .mCourant.CoefGêne
            ManuelDéCoché = Not .chkManuel.Checked
        End Select

        .Dispose()
      End With  ' dlg

    End If

    If OK Then

      If Création Then
        uneTrajectoire = New TrajectoireVéhicules(VoieOrigine, VoieDestination)
        mesTrajectoires.Add(uneTrajectoire)
      End If

      Try
        With uneTrajectoire
          .TypeCourant = typeCourant
          If Création Then
            .Courant = unCourant
            .LigneFeu = mesLignesFeux.DéterminerLignesFeux(uneTrajectoire)
          End If
          If Not IsNothing(.LigneFeu) Then
            CType(.LigneFeu, LigneFeuVéhicules).DéterminerNatureCourants(mesTrajectoires)
            AfficherLigneDeFeux(.LigneFeu)
          End If

          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            Dim i As Short
            ReDim mPoint(mScreen.Length - 3)
            ''Exclure le 1er et le dernier point (extrémités des branches origine et destination, systématiquement incorporés dans la trajectoire)
            'Modif v13 (11/01/07) : on permet également de modifier manuellement les points d'accès aux branches
            'ReDim mPoint(mScreen.Length - 1)
            For i = 0 To mPoint.Length - 1
              mPoint(i) = picDessin.PointToClient(mScreen(i + 1))
              'mPoint(i) = picDessin.PointToClient(mScreen(i))
            Next
            uneTrajectoire.AffecterPointsManuels(mPoint)

          ElseIf ManuelDéCoché And .Manuel Then
            .Réinitialiser(ConserverManuel:=False)
          End If

        End With  ' uneTrajectoire

        Modif = True

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
        If Création Then mesTrajectoires.Remove(uneTrajectoire)
        uneTrajectoire = Nothing
      End Try

    End If

    Return uneTrajectoire

  End Function

  '******************************************************************************
  ' Créer un passage piéton
  '******************************************************************************
  Private Function CréerPassage(ByVal FermerPassage As Boolean) As PolyArc
    Dim OK As Boolean

    'Effacer tous les segments de travail
    EffacerElastiques()

#If DEBUG Then
    Me.Label1.Text = AngleFormé(mPoint(0), mPoint(1), mPoint(2))
#End If

    'Créer le polyarc constitué par les segments
    If AngleFormé(mPoint(0), mPoint(1), mPoint(2)) < 0 Then
      'Saisie faite en sens horaire : réordonner dans le sens trigo
      Dim pTemp As Point = mPoint(0)
      mPoint(0) = mPoint(1)
      mPoint(1) = pTemp
      pTemp = mPoint(2)
      mPoint(2) = mPoint(3)
      mPoint(3) = pTemp
    End If

    Dim p As Point = mPoint(0)
    With BrancheLiée
      If .BordChausséeProche(p) = Branche.Latéralité.Droite Then
        mPoint(0) = Projection(mPoint(0), .BordChaussée(Branche.Latéralité.Droite))
        mPoint(1) = Projection(mPoint(1), .BordChaussée(Branche.Latéralité.Droite))
        If FermerPassage Then
          mPoint(2) = Projection(mPoint(2), .BordChaussée(Branche.Latéralité.Gauche))
          mPoint(3) = Projection(mPoint(3), .BordChaussée(Branche.Latéralité.Gauche))
        End If
      Else
        mPoint(0) = Projection(mPoint(0), .BordChaussée(Branche.Latéralité.Gauche))
        mPoint(1) = Projection(mPoint(1), .BordChaussée(Branche.Latéralité.Gauche))
        If FermerPassage Then
          mPoint(2) = Projection(mPoint(2), .BordChaussée(Branche.Latéralité.Droite))
          mPoint(3) = Projection(mPoint(3), .BordChaussée(Branche.Latéralité.Droite))
        End If
      End If
    End With

    TerminerPassage(New PassagePiéton(BrancheLiée, mPoint))

  End Function

  Private Sub TerminerPassage(ByVal unPassage As PassagePiéton)
    BrancheLiée.mPassages.Add(unPassage)
    maVariante.mPassagesEnAttente.Add(unPassage)
    Modif = True
    DessinerPassage(unPassage)
    If maVariante.Verrou = [Global].Verrouillage.Géométrie Then AfficherLigneDeFeux(unPassage.mTraversée.LigneFeu)

  End Sub

  '***************************************************************************************
  ' DessinerPassage :  dessine le passage piéton
  '                    crée également la traversée associée si la géométrie est verrouillée
  '***************************************************************************************
  Private Function DessinerPassage(ByVal unPassage As PassagePiéton) As PolyArc
    DessinerPassage = unPassage.CréerGraphique(colObjetsGraphiques)
    'Test sur ModeGraphique rajouté en v13(10/01/07) : plantage en mode tableur
    If maVariante.Verrou = [Global].Verrouillage.Géométrie AndAlso ModeGraphique Then
      Dim TraverséeDouble As Boolean = Not IsNothing(unPassage.mTraversée) AndAlso unPassage.mTraversée.mDouble
      If TraverséeDouble Then
        Traversée = maVariante.CréerTraversée(unPassage.mBranche, colObjetsGraphiques)
      Else
        Traversée = maVariante.CréerTraversée(unPassage, colObjetsGraphiques)
      End If

      Traversée.Verrouiller()
    End If

    Redessiner()
  End Function

  Private Function BrancheProche(ByRef pSouris As Point) As Branche
    Dim uneBranche, maBranche As Branche
    Dim distMinPrec As Single = 500
    Dim distMin As Single = 500
    Dim pSourisF As PointF = CvPointF(pSouris)

    'Déterminer l'axe de branche le + proche du point cliqué
    For Each uneBranche In mesBranches
      If Distance(pSouris, uneBranche.LigneDeSymétrie) = 0 Then
        distMin = 0
      Else
        Dim LigneProjection As Ligne = New Ligne(Projection(pSourisF, uneBranche.LigneDeSymétrie), pSourisF)
        If Not intersect(LigneProjection, uneBranche.LigneDeSymétrie).IsEmpty Then
          distMin = Min(distMin, LigneProjection.Longueur)
        End If
      End If
      If distMin < distMinPrec Then
        maBranche = uneBranche
        distMinPrec = distMin
      End If
    Next

    If distMin < 500 Then
      'Branche trouvée
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePiéton
          BordChausséePassage = maBranche.BordChausséeProche(pSouris)
          If BordChausséePassage = Branche.Latéralité.Aucune Then maBranche = Nothing
        Case CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.AllongerFeu
          VoieTraj = maBranche.VoieProche(pSouris)
      End Select
    Else
      VoieTraj = Nothing
    End If

    Return maBranche

  End Function

  '******************************************************************************
  'Numéro Dernier point permettant de clore le tracé de la figure
  '******************************************************************************
  Private Function DernierPoint() As Short
    Select Case UneCommandeGraphique
      Case CommandeGraphique.PassagePiéton
        DernierPoint = 4
      Case CommandeGraphique.Trajectoire
        DernierPoint = 3
    End Select
  End Function

  '******************************************************************************
  ' Rafraichir
  '******************************************************************************
  Private Sub Rafraichir()
    Dim unBufferGraphics As Graphics

    If DiagrammeActif() Then
      unBufferGraphics = mBufferGraphicsA
    Else
      unBufferGraphics = mBufferGraphics
    End If

    With picDessin
      Dim gr As Graphics = .CreateGraphics
      gr.Clear(.BackColor)
      If Not IsNothing(unBufferGraphics) Then
        unBufferGraphics.Clear(.BackColor)
      End If
      gr.Dispose()
    End With

  End Sub

  '******************************************************************************
  ' Redessiner : Efface l'écran et redessine tous les objets à dessiner
  '******************************************************************************
  Public Sub Redessiner(Optional ByVal ObjetASélectionner As Graphique = Nothing)

    If Not ChargementEnCours Then
      'Si le chargement en cours, il vaut mieux attendre la fin du Form_Load pour redessiner (évènement Form_Paint

      Try
        Rafraichir()
        'Remettre les bitmap à Nothing pour forcer la réassociation Bitmap/Graphics
        mBitmap = Nothing
        mBitmapA = Nothing
        DrawPicture(picDessin.CreateGraphics)

        objSélect = ObjetASélectionner
        If IsNothing(objSélect) Then
          savObjSélect = Nothing
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If

  End Sub

  '******************************************************************************
  ' DessinerToutLeCarrefour : Dessine tous les objets de la collection colObjetsGraphiques
  '******************************************************************************
  Private Sub DessinerToutLeCarrefour(Optional ByVal gr As Graphics = Nothing)
    Dim unObjet As Graphique

    If IsNothing(gr) Then
      gr = picDessin.CreateGraphics
    End If

    If IsNothing(mBufferGraphics) Then
      DrawPicture(gr)
    Else

      DessinEnCours = True
      '   mdiApplication.Enabled = False

      If Not IsNothing(monFDP) Then
        'InitEchelleFDP(gr)
        If monFDP.Visible Then
          If monFDP.EstDXF Then
            Dim unDXF As DXF = monFDP
            unDXF.Insert.PréparerDessin(Nothing).Dessiner(mBufferGraphics, gr)
          Else
            Dim unRaster As ImageRaster = monFDP
            ' Dessiner l'image raster (avant tout dessin vectoriel)
            unRaster.Dessiner(mBufferGraphics, gr)
          End If
        End If
        monFDP.ADessiner = False
      End If

      'Dessiner les objets vectoriels

      'For Each unObjet In colObjetsGraphiques
      '  If Not unObjet Is objSélect Then
      '    DessinerObjet(unObjet, gr)
      '  End If
      'Next

      colObjetsGraphiques.Dessiner(mBufferGraphics, gr)

      '    mdiApplication.Enabled = True
      DessinEnCours = False

    End If

  End Sub

  Public Sub RecréerGraphique()
    maVariante.CréerGraphique(colObjetsGraphiques)
    RecréerDessinAntagonismes()
  End Sub

  '******************************************************************************
  ' DessinerObjet :  dessiner un objet graphique
  '******************************************************************************
  Private Sub DessinerObjet(ByVal unObjet As Graphique, Optional ByVal gr As Graphics = Nothing)

    Try
      If IsNothing(gr) Then
        unObjet.Dessiner(mBufferGraphics, picDessin.CreateGraphics)
      Else
        unObjet.Dessiner(mBufferGraphics, gr)
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '******************************************************************************
  ' EffacerObjet :  effacer un objet graphique
  '******************************************************************************
  Private Sub EffacerObjet(ByVal unObjet As Graphique, Optional ByVal gr As Graphics = Nothing)

    Try
      If IsNothing(gr) Then
        unObjet.Effacer(mBufferGraphics, picDessin.CreateGraphics)
      Else
        unObjet.Effacer(mBufferGraphics, gr)
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

 
  Private Function DiagrammeActif() As Boolean
    If pnlPalette Is pnlPlansDeFeux AndAlso Not radPhasage.Checked Then
      DiagrammeActif = True
    End If
  End Function

  '******************************************************************************
  ' Redessiner le buffer mémorisé dans la propriété Tag du picturebox picture
  ' picture : PictureBox support du graphique
  ' gr : objet Graphics associé à picture
  '******************************************************************************
  Private Sub DrawPicture(ByVal gr As Graphics)
    Dim uneBitMap As Bitmap
    Dim unTampon As Graphics
    Dim uneTaille As Size = BufPicDessin.Size()

    Try
      If DiagrammeActif() Then
        uneBitMap = mBitmapA
      Else
        uneBitMap = mBitmap
      End If

      If IsNothing(uneBitMap) Then   '1er appel de Paint pour cette bitmap
        ' Associer une Image Bitmap tampon à un objet Graphics tampon
        uneBitMap = Graphique.AssocierBitmapGraphics(uneTaille, gr, unTampon)

        DessinerSelonContexte(uneBitMap, unTampon, gr)

      Else
        ' Dessiner l'image tampon
        'gr.DrawImage(uneBitMap, 0, 0)
      End If

      ' Dessiner l'image tampon
      gr.DrawImage(uneBitMap, 0, 0)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub DessinerSelonContexte(ByVal uneBitMap As Bitmap, ByVal unTampon As Graphics, ByVal gr As Graphics)

    If DiagrammeActif() Then
      mBitmapA = uneBitMap
      mBufferGraphicsA = unTampon
      If Not IsNothing(monPlanFeuxActif) Then
        monPlanFeuxActif.DessinerDiagramme(gr, mBufferGraphicsA)
      End If
    Else
      mBitmap = uneBitMap
      mBufferGraphics = unTampon

      DessinerToutLeCarrefour(gr)

    End If

  End Sub

  '============================== Fin des fonctions graphiques =====================================================

#End Region
#Region " Gestion des onglets"
  '******************************************************************************
  ' Changement d'onglet principal
  '******************************************************************************
  Private Sub tabOnglet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
   Handles tabOnglet.SelectedIndexChanged

    If IsNothing(maVariante) Then
      tabOnglet.SelectedTab = Nothing
    Else
      Try
        GérerChangementOnglet()
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If
  End Sub

  '******************************************************************************
  ' Gestion du Changement d'onglet principal
  '******************************************************************************
  Private Sub GérerChangementOnglet()
    Dim nomOnglet As String
    Dim unOnglet As TabPage
    Dim pnlActif As Panel = pnlPalette
    Static OngletActif As TabPage
    Dim IndexOnglet As OngletEnum

    Try

      If IsNothing(OngletActif) Then
        DéfinirDéfautLargeurPanels()
        Me.MinimumSize = New Size(lgPanel(0), 0)
      End If

      IndexOnglet = tabOnglet.SelectedIndex
      'If tabOnglet.SelectedIndex = -1 Then IndexOnglet = -1

      unOnglet = tabOnglet.SelectedTab

      If IsNothing(unOnglet) Then
        If maVariante.ModeGraphique Then
          pnlPalette = pnlGéométrie
        Else
          pnlPalette = pnlLignesDeFeux
        End If

      Else

        If maVariante.OngletInterdit(IndexOnglet) Then
          unOnglet = OngletActif
          tabOnglet.SelectedTab = unOnglet
          IndexOnglet = tabOnglet.SelectedIndex
          'If tabOnglet.SelectedIndex = -1 Then IndexOnglet = -1
        ElseIf Not IsNothing(pnlPalette) Then
          pnlPalette.SendToBack()
        End If

        Select Case IndexOnglet
          Case OngletEnum.Géométrie
            pnlPalette = pnlGéométrie
            mAideTopic = [Global].AideEnum.ONGLET_GEOMETRIE

          Case OngletEnum.LignesDeFeux
            pnlPalette = pnlLignesDeFeux
            mAideTopic = [Global].AideEnum.ONGLET_CIRCULATION

          Case OngletEnum.Trafics
            pnlPalette = pnlTrafics
            pnlTrafics.BringToFront()
            If InitTrafics() Then
              'L'utilisateur a fait 'Annuler' sur le nom de la 1ere période de Trafic
              'Ou le scénario en cours est sans trafic
              tabOnglet.SelectedTab = OngletActif
            End If
            mAideTopic = [Global].AideEnum.ONGLET_TRAFICS

          Case OngletEnum.Conflits
            pnlPalette = pnlConflits
            InitConflits()
            mAideTopic = [Global].AideEnum.ONGLET_CONFLITS

          Case OngletEnum.PlansDeFeux
            pnlPalette = pnlPlansDeFeux
            If pnlPlansFeuxIndex = -1 Or Me.cboDécoupagePhases.Items.Count = 0 Then
              '1er Appel de l'onglet Plans de feux (ou suite à réinitialisation de ceux-ci par déverrouillage de la matrice des conflits)
              If InitPhasage() Then
                tabOnglet.SelectedTab = OngletActif
              Else
                Me.radPhasage.Checked = True
              End If
            End If
            mAideTopic = [Global].AideEnum.ONGLET_PLANS_FEUX

        End Select
      End If

      If Not IsNothing(pnlPalette) Then

        cndContexte = IndexOnglet

        If Not unOnglet Is OngletActif And ModeGraphique Then
          maVariante.Verrouiller()
          If Not IsNothing(OngletActif) Then
            Redessiner()
            If Not IsNothing(monFDP) Then MenuAfficherFDP()
            If tabOnglet.TabPages(OngletEnum.Trafics) Is OngletActif Then
              MémoriserCommentaireTrafic()
            End If
          End If

          TopicAideCourant = mAideTopic
        End If

        DéfinirSplitPosition()
        pnlPalette.BringToFront()
        pnlPalette.Visible = True
        If pnlPalette Is Me.pnlConflits Then
          Me.Ac1GrilleSécurité.Left = lgPanel(numPanel) - Me.Ac1GrilleSécurité.Width - LGMARGE
        End If

        AfficherCacherDiagnostic()

        OngletActif = tabOnglet.SelectedTab
        'OngletActif.Font = New Font(Me.tabOnglet.Font, FontStyle.Bold)  ' FonteGras
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      If IndexOnglet = -1 Then
        Throw New DiagFeux.Exception(ex.Message)
      Else
        LancerDiagfeuxException(ex, "Activation de l'onglet " & Me.tabOnglet.TabPages(IndexOnglet).Text)
      End If
    Finally
      mdiApplication.AfficherBarreEtat()
    End Try

  End Sub

  Private Sub ActiverAide()

  End Sub

  Private Overloads Function OngletAssocié(ByVal pnl As Panel) As OngletEnum

    Select Case pnl.Name
      Case Me.pnlGéométrie.Name
        OngletAssocié = OngletEnum.Géométrie
      Case Me.pnlTrafic.Name
        OngletAssocié = OngletEnum.Trafics
      Case Me.pnlConflits.Name
        OngletAssocié = OngletEnum.Conflits
      Case Me.pnlPlansDeFeux.Name
        OngletAssocié = OngletEnum.PlansDeFeux
    End Select
  End Function

  Private Overloads Function OngletAssocié(ByVal unOnglet As TabPage) As OngletEnum

    Select Case unOnglet.Name
      Case Me.tabGéométrie.Name
        OngletAssocié = OngletEnum.Géométrie
      Case Me.tabTrafics.Name
        OngletAssocié = OngletEnum.Trafics
      Case Me.tabConflits.Name
        OngletAssocié = OngletEnum.Conflits
      Case Me.tabPlansDeFeux.Name
        OngletAssocié = OngletEnum.PlansDeFeux
    End Select
  End Function

  '******************************************************************************
  ' Recréer le Menu Contextuel à partir du MenuItem unMenu
  '******************************************************************************
  Public Sub RecréerMenuContextuel(ByVal unMenu As MenuItem)

    picDessin.ContextMenu.MenuItems.Clear()
    picDessin.ContextMenu.MergeMenu(unMenu)

  End Sub
#End Region
#Region " Verrouillages"
  '********************************************************************************************************************
  ' Activation/Désactivation d'une case de verrouillage de la variante
  '********************************************************************************************************************
  Private Sub chkVerrou_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkVerrouMatrice.CheckedChanged, chkVerrouFeuBase.CheckedChanged, chkVerrouGéométrie.CheckedChanged, chkVerrouLignesFeux.CheckedChanged
    Dim chk As Windows.Forms.CheckBox = sender
    Dim Index As Verrouillage = chk.Tag
    Dim texte As String = "Déverrouiller "
    Dim TexteComplément As String

    If ChargementEnCours Or ChangementDeScénario Then
      Select Case Index
        Case [Global].Verrouillage.Géométrie
          VerrouillerBoutonsGéométrie(Verrouillage:=True)
        Case [Global].Verrouillage.LignesFeux
          VerrouillerBoutonsLignesFeux(Verrouillage:=True)
        Case [Global].Verrouillage.Matrices
          VerrouillerMatrice(chk.Checked)
        Case [Global].Verrouillage.PlanFeuBase
          VerrouillerPlanFeuxBase(chk.Checked)
      End Select

    Else

      Static Passage As Boolean

      If Not Passage Then
        DémarrerCommande(CommandeGraphique.AucuneCommande)
        Dim ContientManuelles = mesTrajectoires.ContientManuelles

        If Not chk.Checked Then
          Select Case Index
            Case [Global].Verrouillage.Géométrie
              'Les trajectoires et lignes de feux piétons sont liées diréectement aux passages piétons;
              ' Il ne faut donc détecter que la présence de trajectoires et/ou de lignes de feux véhicules
              If mesTrajectoires.ContientVéhicules Or mesLignesFeux.Count > mesTrajectoires.Count Then
                texte &= "la géométrie"
                TexteComplément &= "Lignes de feux et Trajectoires seront réinitialisées"
                If ModeGraphique And ConflitsPartiellementRésolus Then
                  TexteComplément &= vbCrLf & "Les antagonismes seront réinitialisés"
                End If

                If ScénarioEnCours() AndAlso monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteComplément &= vbCrLf & "Tous les plans de feux de fonctionnement vont être supprimés"
                End If
              Else
                texte = ""
              End If

            Case [Global].Verrouillage.LignesFeux
              If ScénarioEnCours() Then
                texte &= "les lignes de feux"
                If ModeGraphique AndAlso mAntagonismes.ConflitsPartiellementRésolus Then
                  TexteComplément &= "Les antagonismes seront réinitialisés"
                End If
                If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteComplément &= vbCrLf & "Tous les plans de feux de fonctionnement vont être supprimés"
                ElseIf PhasageRetenu Then
                  TexteComplément &= "L'organisation du phasage sera à refaire"
                End If

              Else
                texte = ""
              End If

            Case [Global].Verrouillage.Matrices
              If PhasageRetenu Then
                texte &= "la matrice"
                If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteComplément &= "Tous les plans de feux de fonctionnement vont être supprimés"
                Else
                  TexteComplément &= "L'organisation du phasage sera à refaire"
                End If
              Else
                texte = ""
              End If

            Case [Global].Verrouillage.PlanFeuBase
              If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                texte &= "le plan de feux de base"
                TexteComplément &= "Tous les plans de feux de fonctionnement vont être supprimés"
              Else
                texte = ""
              End If
          End Select

        End If

        Try
          Passage = True
          If texte.Length > 0 Then
            texte &= " ?" & vbCrLf & TexteComplément
          End If
          If VerrouillageAccepté(chk, texte) Then
            mdiApplication.AfficherBarreEtat()

            Select Case Index
              Case [Global].Verrouillage.Géométrie
                VerrouillerGéométrie(chk.Checked)
              Case [Global].Verrouillage.LignesFeux
                VerrouillerLignesFeux(chk.Checked)
              Case [Global].Verrouillage.Matrices
                VerrouillerMatrice(chk.Checked)
              Case [Global].Verrouillage.PlanFeuBase
                VerrouillerPlanFeuxBase(chk.Checked)
            End Select

            Modif = True
          End If

        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        Finally
          Passage = False
        End Try

      End If  ' Not Passage

    End If  'ChargementEnCours

  End Sub

  Private Overloads Function VerrouillageAccepté() As Boolean
    Dim ObjetMétier As Métier

    Try
      ObjetMétier = maVariante.NonVerrouillable()
    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      ObjetMétier = maVariante
    End Try

    Return IsNothing(ObjetMétier)

  End Function

  '***********************************************************************
  ' Vérifier si le verrouillage peut être accepté
  '***********************************************************************
  Private Overloads Function VerrouillageAccepté(ByVal chk As CheckBox, ByVal Texte As String) As Boolean
    Dim ObjetMétier As Métier

    Try
      If chk.Checked Then
        'NonVerrouillable retourne Nothing s'il n'y a pas de pb pour verrouiller
        ObjetMétier = maVariante.NonVerrouillable()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      ObjetMétier = maVariante
    End Try

    If IsNothing(ObjetMétier) Then
      If chk.Checked Or Texte.Length = 0 Then
        VerrouillageAccepté = True
      Else
        VerrouillageAccepté = Confirmation(Texte, Critique:=True)
      End If

      If VerrouillageAccepté Then
        'Version définitive
        maVariante.BasculerVerrou(chk)
      Else
        chk.Checked = Not chk.Checked
      End If

    Else
      chk.Checked = Not chk.Checked
      If ModeGraphique Then
        Désélectionner()
        If Not IsNothing(ObjetMétier.mGraphique.ObjetMétier) Then
          objSélect = ObjetMétier.mGraphique
          SélDésélectionner(PourSélection:=True)
        End If
      End If
    End If

  End Function

  '************************************************************************************
  ' (Dé)Verrouiller les boutons suite au (dé)verrouillage de la géométrie
  '************************************************************************************
  Private Sub VerrouillerBoutonsGéométrie(ByVal Verrouillage As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle

    If ModeGraphique Then
      'Griser/Dégriser les colonnes Largeur de voies et nombre de colonnes
      ' On pourrait autoriser les largeurs de voies, mais l'événement AC1GrillesBranches_ValidateEdit _
      ' redéfinit l'ilot et supprime les passages piétons si celles-ci sont redéfinies
      rg = Me.AC1GrilleBranches.GetCellRange(1, 4, mesBranches.Count, 6)

      If Verrouillage Then
        unStyle = StyleGrisé
      Else
        unStyle = StyleDégrisé
      End If

    Else
      unStyle = StyleGrisé
      rg = Me.AC1GrilleBranches.GetCellRange(1, 5, mesBranches.Count, 5)
    End If

    rg.Style = unStyle

  End Sub

  '*************************************************************************************
  ' Verrouiller la géométrie
  'Procédure appelé uniquement en mode graphique
  '*************************************************************************************
  Private Sub VerrouillerGéométrie(ByVal Verrouillage As Boolean)

    Try

      VerrouillerBoutonsGéométrie(Verrouillage)

      Désélectionner()

      If Verrouillage Then
        maVariante.InitialiserCourants()

        'Créer les traversées piétonnes à partir des passages piétons et les lignes de feux associées
        maVariante.InitialiserTraversées(colObjetsGraphiques)
        mesTrajectoires.Verrouiller()

        'Afficher le tableau des lignes de feux piétons
        Dim uneLignePiétons As LigneFeuPiétons
        For Each uneLignePiétons In mesLignesFeux
          'A ce stade d'avancement, il n'y a pas de ligne véhicules : le cast LigneFeux->LigneFeuPiétons fonctionne toujours
          Me.AfficherLigneDeFeux(uneLignePiétons)
        Next

      Else
        'Supprimer toutes les trajectoires et les lignes de feux
        maVariante.SupprimerTrajectoires(colObjetsGraphiques)
        maVariante.CréerGraphique(colObjetsGraphiques)

        'Supprimer toutes les lignes sauf les 2 premières qui sont incompressibles
        DécalageFeuxEnCours = True  'Drapeau pour désactiver l'évènement RowColChange
        Me.AC1GrilleFeux.Rows.RemoveRange(1, Me.AC1GrilleFeux.Rows.Count - 2)
        DécalageFeuxEnCours = False
        Me.AC1GrilleFeux.Rows(1).Clear(Grille.ClearFlags.Content)

        'Déverrouiller également le verrouillage aval
        Me.chkVerrouLignesFeux.Checked = False
        VerrouillerLignesFeux(Verrouillage)

        'Réinitialiser le menu Scénario
        AfficherScénarios()
        Me.cboTrafic.Items.Clear()

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "VerrouillerGéométrie")

    End Try
  End Sub

  '************************************************************************************
  ' (Dé)Verrouiller les boutons suite au (dé)verrouillage des lignes de feux
  '************************************************************************************
  Private Sub VerrouillerBoutonsLignesFeux(ByVal Verrouillage As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle

    'On ne peut pas ajouter ni supprimer une ligne de feux après verrouillage
    Me.btnLigneFeux.Enabled = Not Verrouillage
    Me.btnLigneFeuxMoins.Enabled = Not Verrouillage

    'On ne peut pas ajouter ni supprimer une trajectoire après verrouillage
    Me.btnTrajectoire.Enabled = Not Verrouillage
    Me.btnTrajectoireMoins.Enabled = Not Verrouillage

    'On ne peut pas ajouter ni supprimer une traversée piétonne après verrouillage
    Me.btnTraversée.Enabled = Not Verrouillage
    Me.btnTraverséeMoins.Enabled = Not Verrouillage
    Me.btnTrajToutes.Enabled = Not Verrouillage
    Me.btnTrajMoinsTout.Enabled = Not Verrouillage

    If Verrouillage Then
      unStyle = StyleGrisé
    Else
      unStyle = StyleDégrisé
    End If

    If ModeGraphique Then
      'Griser/Dégriser la colonne Angle du tableau de branches :les trajectoires peuvent devenir indessinables et surtout en cascade , les positions des antagonismes)
      rg = Me.AC1GrilleBranches.GetCellRange(1, 2, mesBranches.Count, 2)
      rg.Style = unStyle

    Else
      'Griser/Dégriser la colonne nb de voies sortantes (utile pour les trafics : sens uniques entrants éventuels)
      rg = Me.AC1GrilleBranches.GetCellRange(1, 6, mesBranches.Count, 6)
      rg.Style = unStyle

      'Griser/Dégriser les colonnes nombre de voies et TAG,TD,TAD  du tableau de LF
      'Ajout AV : 10/08/07 - Point Circulation 33 du document de suivi
      GriserLFTableur(Verrouillage)
      'rg = Me.AC1GrilleFeux.GetCellRange(1, 5, mesLignesFeux.Count, 8)
      'rg.Style = unStyle
    End If

    If Verrouillage Then
      'Dimensionner la largeur de la 1ère colonne selon l'apparition de l'ascenseur 
      '(10 lignes pour le plan de feux de base, 8 pour celui de fonctionnement)
      Me.lvwDuréeVert.Columns(0).Width = IIf(mesLignesFeux.Count > 10, 46, 63)
      Me.lvwDuréeVertFct.Columns(0).Width = IIf(mesLignesFeux.Count > 8, 46, 63)
    End If

  End Sub

  Private Sub GriserLFTableur(ByVal Verrouillage As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim uneLigneFeux As LigneFeux
    Dim row As Short

    If Verrouillage Then
      rg = fg.GetCellRange(1, 0, mesLignesFeux.Count, 8)
      unStyle = StyleGrisé
      rg.Style = unStyle

      unStyle = StyleDégrisé
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        If uneLigneFeux.EstVéhicule Then
          rg = fg.GetCellRange(row, 1, row, 3)
        Else
          rg = fg.GetCellRange(row, 1, row, 2)
        End If
        rg.Style = unStyle
      Next

    Else
      rg = fg.GetCellRange(1, 0, mesLignesFeux.Count, 8)
      unStyle = StyleDégrisé
      rg.Style = unStyle

      unStyle = StyleGrisé
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        If uneLigneFeux.EstPiéton Then
          rg = fg.GetCellRange(row, 4, row, 8)
          rg.Style = unStyle
        End If
      Next

    End If

  End Sub

  '************************************************************************************
  ' Verrouiller les lignes de feux
  '************************************************************************************
  Private Sub VerrouillerLignesFeux(ByVal Verrouillage As Boolean)

    Try

      VerrouillerBoutonsLignesFeux(Verrouillage)

      maVariante.VerrouillerLignesFeux(Verrouillage, colObjetsGraphiques)

      If ModeGraphique Then
        Désélectionner()

      Else
        If Verrouillage Then
          'Supprimer la dernière ligne : ajout interdit
          Me.AC1GrilleFeux.RemoveItem()
        Else
          'Rajouter une ligne pour création possible
          Me.AC1GrilleFeux.Rows.Add()
        End If

      End If

      If Verrouillage Then
        ConflitsInitialisés = False
        InitConflits()

      Else
        'Déverrouiller également le verrouillage aval
        Me.chkVerrouMatrice.Checked = False
        VerrouillerMatrice(Verrouillage)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "VerrouillerLignesFeux")
    End Try

  End Sub

  '*************************************************************************************
  ' Verrouiller la matrice des conflits
  '*************************************************************************************
  Private Sub VerrouillerMatrice(ByVal Verrouillage As Boolean)

    Try

      Me.Ac1GrilleSécurité.Enabled = Not Verrouillage
      'Remettre à 0 le compteur pour que les Phasages soient reconstruits
      If Verrouillage Then

        If Not ChangementDeScénario Then
          'Les plans de phasage viennent d'être construits : calculer les capacités
          monPlanFeuxBase.CalculerCapacitésPlansPhasage()
          'Ligne mise en commentaire le 21/03/07(AV) : On ne voit pas à quoi çà sert et c'est plutot préjudiciable 
          '        ConflitsInitialisés = False
        End If

      ElseIf ScénarioEnCours() Then
        'Déverrouiller également le verrouillage aval
        If Me.chkVerrouFeuBase.Checked Then
          Me.chkVerrouFeuBase.Checked = False
          VerrouillerPlanFeuxBase(Verrouillage:=False)
          monPlanFeuxBase.mPlansFonctionnement.Clear()
        End If
        If PhasageRetenu Then
          'Phrase mise en commentaire le 06/02/07 en attendant : Nouvelle organisation du phasage(tous les phasages ne sont pas affichés)
          '   Me.cboDécoupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)
          Me.chkDécoupagePhases.Checked = False
        End If
      End If

      'On ne peut plus modifier le trafic  dès le verrouillage des conflits
      If Not IsNothing(monTraficActif) Then
        ActiverBoutonsTrafics()
      End If

      '    Me.AC1GrilleAntagonismes.Enabled = Not Verrouillage
      Me.AC1GrilleAntagonismes.Cols(2).AllowEditing = Not Verrouillage
      Me.btnRéinitAntago.Enabled = Not Verrouillage

      If ModeGraphique AndAlso cndContexte = [Global].OngletEnum.Conflits Then
        '(Dé)Verrouillage opéré manuellement : les antagonismes sont visibles et cliquables uniquement dans cet onglet
        mAntagonismes.Verrouiller()
        Redessiner()
      End If

      'Désactiver tous les boutons radios due l'onglet Plans de feux
      pnlPlansFeuxIndex = -1

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*************************************************************************************
  ' Verrouiller le plan de feux de base
  '*************************************************************************************
  Private Sub VerrouillerPlanFeuxBase(ByVal Verrouillage As Boolean)

    'On ne peut plus modifier l'organisation du phasage, ni le plan de feux de base
    '    VerrouillerOrgaPhasage()
    Me.chkDécoupagePhases.Enabled = Not Verrouillage

    'On ne peut plus modifier  le plan de feux de base
    Me.txtDuréeCycleBase.Enabled = Not Verrouillage
    Me.updPhase1Base.Enabled = Not Verrouillage
    Me.updPhase2Base.Enabled = Not Verrouillage
    Me.updPhase3Base.Enabled = Not Verrouillage
    Me.updDécalageOuvertureBase.ReadOnly = Verrouillage
    Me.updDécalageFermetureBase.ReadOnly = Verrouillage

    If Not Verrouillage Then
      monPlanFeuxBase.mPlansFonctionnement.Clear()
      Me.cboPlansDeFeux.Items.Clear()
    End If

  End Sub
#End Region
#Region " Boutons Métier"
#Region " Géométrie"
  Private Sub btnPiétonPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnPiétonPlus.Click, btnPiétonPlusRapide.Click

    If sender Is btnPiétonPlus Then
      DémarrerCommande(CommandeGraphique.PassagePiéton)
    Else
      DémarrerCommande(CommandeGraphique.PassagePiétonRapide)
    End If
  End Sub

  Private Sub btnPiétonMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPiétonMoins.Click
    If IsNothing(objSélect) Then
      DémarrerCommande(CommandeGraphique.SupprimerPassage)
    ElseIf TypeOf objSélect Is PolyArc Then
      Dim unPolyarc As PolyArc = objSélect
      If TypeOf unPolyarc.ObjetMétier Is PassagePiéton Then
        If Confirmation("Supprimer le passage piéton", Critique:=False) Then
          DémarrerCommande(CommandeGraphique.SupprimerPassage)
        End If
      Else
        MessageBox.Show("Sélectionner un passage piéton")
        DémarrerCommande(CommandeGraphique.AucuneCommande)
        objSélect = Nothing
      End If
    End If
  End Sub

#End Region
#Region " Trajectoires"

  '******************************************************************************
  ' Bouton  ouvrant la Boite de dialogue des propriétés de la trajectoire
  '******************************************************************************
  Private Sub btnTrajProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnTrajProp.Click

    If IsNothing(objSélect) Then
      DémarrerCommande(CommandeGraphique.PropTrajectoire)
    ElseIf TypeOf objSélect Is PolyArc Then
      Dim unPolyarc As PolyArc = objSélect
      If TypeOf unPolyarc.ObjetMétier Is TrajectoireVéhicules Then
        If UneCommandeGraphique = CommandeGraphique.PropTrajectoire Then DémarrerCommande(CommandeGraphique.AucuneCommande)
        Dim uneTrajectoire As TrajectoireVéhicules = objSélect.ObjetMétier
        If IsNothing(Me.dialogueTrajVéhicules(uneTrajectoire)) Then
          'L'utilisateur a demandé à redessiner manuellement : exécuter la commande EditerTrajectoire
          DéfinirTrajectoireManuellement()
        Else
          If uneTrajectoire.ARedessiner Then DessinerTrajectoire(uneTrajectoire)
        End If

      Else
        MessageBox.Show("Sélectionner une trajectoire")
      End If
    End If

  End Sub

  '**************************************************************************************
  ' Case à cocher pour afficher/masquer les flèches indiquant le sens des trajectoires
  ' Couplée  avec l'item correspondant du menu Affichage
  '**************************************************************************************
  Private Sub chkSensTrajectoires_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSensTrajectoires.CheckedChanged
    maVariante.SensTrajectoires = Me.chkSensTrajectoires.Checked
    If Not DiagrammeActif() Then
      If Not ChargementEnCours Then Redessiner()
    End If
  End Sub

  '******************************************************************************
  ' Définir les points de construction de la trajectoire manuellement
  '******************************************************************************
  Private Sub DéfinirTrajectoireManuellement()
    Dim uneTrajectoire As TrajectoireVéhicules = objSélect.ObjetMétier
    Dim unePlume As New Pen(Color.Fuchsia)
    Dim LOrigine, LDestination As Ligne

    EffacerObjet(objSélect)

    With uneTrajectoire
      Dim unPolyarc As PolyArc = CType(objSélect, PolyArc)
      'Axe de la voie origine
      LOrigine = .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Origine).Clone
      'L'accès à la branche origine a pu être déplacé
      LOrigine.pA = .LigneAccès.pA
      LOrigine.Plume = unePlume
      DessinerObjet(LOrigine)

      'Axe de la voie destination
      LDestination = .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Destination).Clone
      'L'accès à la branche destination a pu être déplacé
      LDestination.pA = .LigneAccès.pB
      LDestination.Plume = unePlume
      DessinerObjet(LDestination)
    End With

    ReDim mPoint(0)
    ReDim mScreen(2)
    mPoint(0) = CvPoint(LOrigine.pAF)
    DessinerPoignée(mPoint(0), True)
    mScreen(0) = picDessin.PointToScreen(mPoint(0))
    mScreen(1) = mScreen(0)
    'mScreen1 représente le point 'mobile' de la souris (pour DessinerElastique)
    mScreen1 = mScreen(1)

    'Pour dessiner la poignée de l'extrémité du segment destination :
    'mPoint1 Servira à repérer le point d'arrivée à atteindre lors du dessin de la trajectoire
    mPoint1 = CvPoint(LDestination.pAF)
    mScreen(2) = picDessin.PointToScreen(mPoint1)
    mScreen2 = mScreen(2)

    UneCommandeGraphique = CommandeGraphique.EditerTrajectoire
    TraiterMessageGlisser()
    mDragging = True
    DessinerElastique()

  End Sub

  '******************************************************************************
  ' Refaire le dessin suite à l'édition de la trajectoire
  '******************************************************************************
  Private Sub DessinerTrajectoire(ByVal uneTrajectoire As TrajectoireVéhicules)

    uneTrajectoire.CréerGraphique(colObjetsGraphiques)
    If Not GénérationTrajectoires Then Redessiner()

  End Sub

  '******************************************************************************
  ' Supprimer une trajectoire
  '******************************************************************************
  Private Sub btnTrajMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajectoireMoins.Click

    If IsNothing(objSélect) Then
      If maVariante.mTrajectoires.Count > 0 Then DémarrerCommande(CommandeGraphique.SupprimerTrajectoire)

    ElseIf TypeOf objSélect Is PolyArc Then
      Dim unPolyarc As PolyArc = objSélect
      If TypeOf unPolyarc.ObjetMétier Is TrajectoireVéhicules Then
        If Confirmation("Supprimer la trajectoire", Critique:=False) Then
          DémarrerCommande(CommandeGraphique.SupprimerTrajectoire)
        End If
      Else
        MessageBox.Show("Sélectionner une trajectoire")
        DémarrerCommande(CommandeGraphique.AucuneCommande)
        objSélect = Nothing
      End If
    End If

  End Sub

  Private Sub btnTrajPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajectoire.Click

    DémarrerCommande(CommandeGraphique.Trajectoire)
  End Sub

  Private Sub btnTravPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraversée.Click

    UneCommandeGraphique = CommandeGraphique.Traversée
    TraiterMessageGlisser()
  End Sub

  Private Sub btnTravMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraverséeMoins.Click

    UneCommandeGraphique = CommandeGraphique.DécomposerTraversée
    TraiterMessageGlisser()

  End Sub

  Private Sub btnTravProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnTravProp.Click

    If IsNothing(objSélect) Then
      DémarrerCommande(CommandeGraphique.PropTraversée)
    ElseIf TypeOf objSélect Is PolyArc Then
      Dim unPolyarc As PolyArc = objSélect
      If TypeOf unPolyarc.ObjetMétier Is TraverséePiétonne Then
        Dim uneTrajectoire As TraverséePiétonne = objSélect.ObjetMétier
        Me.dialogueTrajPiétons(uneTrajectoire)
      Else
        MessageBox.Show("Sélectionner une passage piéton")
      End If
    End If

  End Sub

  '*****************************************************************************************
  'Générer toutes les trajectoires véhicules possibles
  ' A l'exception de celles déjà définies
  '*****************************************************************************************
  Private Sub btnTrajToutes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajToutes.Click
    Dim uneBrancheEntrante, uneBrancheSortante As Branche
    Dim uneTrajectoire As Trajectoire

    DémarrerCommande(CommandeGraphique.AucuneCommande)

    If Confirmation("Générer toutes les trajectoires possibles", Critique:=False, Controle:=Me) Then
      UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires

      GénérationTrajectoires = True

      For Each uneBrancheEntrante In mesBranches
        For Each VoieOrigine In uneBrancheEntrante.Voies
          If VoieOrigine.Entrante Then
            'Voie entrante : chercher toutes le voies sortantes possibles
            For Each uneBrancheSortante In mesBranches
              If Not uneBrancheEntrante Is uneBrancheSortante Then
                'Pas de trajectoire avc même branche d'entrée et de sortie
                For Each VoieTraj In uneBrancheSortante.Voies
                  If Not VoieTraj.Entrante Then
                    'Voie sortante
                    If Not mesTrajectoires.Existe(VoieOrigine, VoieTraj) Then
                      'Vérifier que la trajectoire n'existe pas déjà
                      CréerTrajectoire()
                    End If
                  End If
                Next
              End If
            Next
          End If
        Next
      Next

      GénérationTrajectoires = False
      Redessiner()
      DémarrerCommande(CommandeGraphique.AucuneCommande)
    End If

  End Sub

  '******************************************************************************
  ' Supprimer toutes les trajectoires
  '******************************************************************************
  Private Sub btntrajMoinsTout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajMoinsTout.Click

    If maVariante.mTrajectoires.Count > 0 Then
      If Confirmation("Supprimer toutes les trajectoires", Critique:=False, Controle:=Me) Then
        With maVariante
          .mTrajectoires.Epurer()
          .CréerGraphique(colObjetsGraphiques)
        End With
        AfficherLignesDeFeux()
        Redessiner()
      End If
    End If

  End Sub

#End Region
#Region " Lignes de feux"
  Private Sub btnLigneFeuPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLigneFeux.Click

    DémarrerCommande(CommandeGraphique.LigneFeux)

  End Sub

  '************************************************************************************
  ' Bouton Supprimer une ligne de feux
  '************************************************************************************
  Private Sub btnLigneFeuMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLigneFeuxMoins.Click
    Dim PoserQuestion As Boolean
    Dim msg As String

    If ModeGraphique Then
      If IsNothing(objSélect) Then
        DémarrerCommande(CommandeGraphique.SupprimerLigneFeu)
      ElseIf TypeOf objSélect Is PolyArc Then
        Dim unPolyarc As PolyArc = objSélect
        If TypeOf unPolyarc.ObjetMétier Is LigneFeuVéhicules Then
          PoserQuestion = True
        ElseIf TypeOf unPolyarc.ObjetMétier Is LigneFeuPiétons Then
          msg = "Sélectionner une ligne de feux véhicules"
        Else
          msg = "Sélectionner une ligne de feux"
        End If
      End If

    Else
      Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
      Dim Index As Short = fg.Row - 1
      If Index = mesLignesFeux.Count Then
        'Effacer la ligne de feux encore en cours de création (mode tableur)
        fg.GetCellRange(Index + 1, 0, Index + 1, fg.Cols.Count - 1).Clear(Grille.ClearFlags.Content)
      Else
        Dim uneLigneFeux As LigneFeux = mesLignesFeux(Index)
        If uneLigneFeux.ToutesVoiesSurBranche Then
          msg = "Branche à sens unique : elle doit comporter au moins une ligne de feux"
        Else
          PoserQuestion = True
        End If
      End If
    End If

    If PoserQuestion Then
      If Confirmation("Supprimer la ligne de feux ?", Critique:=True, Controle:=Me) Then
        SupprimerLigneFeux()
      End If
      DémarrerCommande(CommandeGraphique.AucuneCommande)
    ElseIf Not IsNothing(msg) Then
      MessageBox.Show(msg)
      DémarrerCommande(CommandeGraphique.AucuneCommande)
      objSélect = Nothing
    End If

  End Sub

  '************************************************************************************
  ' Supprimer la ligne de feux sélectionnée
  '************************************************************************************
  Private Sub SupprimerLigneFeux()
    Dim uneLigneFeux As LigneFeux
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim Index As Short = fg.Row - 1

    If Index >= 0 Then
      Try
        'Supprimer la ligne de feux
        uneLigneFeux = mesLignesFeux(Index)
        mesLignesFeux.Remove(uneLigneFeux)
        'Supprimer la ligne de la grille
        If fg.Row = 1 And fg.Rows.Count = 2 Then 'peut arriver en mode non graphique (voire en mode graphique sans passages piétons)
          fg.GetCellRange(1, 0, 1, fg.Cols.Count - 1).Clear(Grille.ClearFlags.Content)
        Else
          ' Dans certains cas (anormaux) RemoveItem déclenche AfterRowColChange et çà peut planter 
          DécalageFeuxEnCours = True
          'Désélectionner la ligne de feux, car çà peut avoir des effets de bord
          fg.Row = -1
          fg.RemoveItem(Index + 1)
          DécalageFeuxEnCours = False
        End If
        ActiverBoutonsLignesFeux()
        If ModeGraphique Then
          UneCommandeGraphique = CommandeGraphique.SupprimerLigneFeu
          SupprimerObjetMétier()
          UneCommandeGraphique = CommandeGraphique.AucuneCommande

        ElseIf uneLigneFeux.EstVéhicule Then
          With CType(uneLigneFeux, LigneFeuVéhicules)
            .mBranche.NbVoies -= .NbVoiesTableur
          End With
          MettreAJourVoiesBranches()
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  '************************************************************************************************
  ' Faire Monter/Descendre la ligne de feux sélectionnée (ne touche qu'à l'ordre dans la collection)
  '************************************************************************************************
  Private Sub btnLigneFeuDécaler_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles btnLigneFeuMonter.Click, btnLigneFeuDescendre.Click
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim Décalage As Short, Position As Short

    Try

      'Déterminer le sens du décalage
      If sender Is btnLigneFeuDescendre Then
        DécalerLigneFeux(+1, fg)
      Else
        DécalerLigneFeux(-1, fg)
      End If
      Me.cboTriLignesFeux.SelectedIndex = 0

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub DécalerLigneFeux(ByVal Décalage As Short, ByVal fg As GrilleDiagfeux)
    Dim Index As Short = fg.Row - 1
    Dim Position As Short
    Dim Données As String = fg.TouteLaLigne.Clip

    Try

      'Décaler la ligne de feux
      Dim uneLigneFeux As LigneFeux = mesLignesFeux(Index)
      mesLignesFeux.Décaler(Décalage, mesLignesFeux(Index))

      AfficherConséquencesModifLignesDeFeux()

      'Répercuter dans la grille
      With fg

        Position = .Row + Décalage

        DécalageFeuxEnCours = True
        .RemoveItem(.Row)
        .AddItem(Données, Position)
        GriserLignePiétons(fg, Position, uneLigneFeux.EstPiéton)
        DécalageFeuxEnCours = False

        'Déselectionner la ligne
        .Désélectionner()
        'Resélectionner la ligne qui vient d'être décalée
        fg.Row = Position
        fg.Select(fg.GetCellRange(Position, 0, Position, fg.Cols.Count - 1))
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DécalerLigneFeux")
    End Try

  End Sub

  '**********************************************************************************************
  ' Répercuter les modifications de lignes de feux dans les tableaux connexes :
  '   - Nom de la ligne
  '  - Décalage d'une ligne (SuiteADécalage =true)
  '  - Changement de l'ordre de classement (SuiteADécalage =true)
  '**********************************************************************************************
  Private Sub AfficherConséquencesModifLignesDeFeux(Optional ByVal SuiteADécalage As Boolean = True)

    If maVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
      'Réafficher en conséquence l'ordonnancement des matrices de sécurité
      If ConflitsInitialisés Then
        AfficherEnteteMatriceSécurité()
        If ModeGraphique Then RéafficherLibellésAntagonismes()
        If SuiteADécalage Then
          If Me.radMatriceConflits.Checked Then
            AfficherMatriceSécurité(0)
          ElseIf Me.radMatriceRougesDégagement.Checked Then
            AfficherMatriceSécurité(1)
          ElseIf Me.radMatriceInterverts.Checked Then
            AfficherMatriceSécurité(2)
          End If
        End If
      End If

      If ScénarioEnCours() Then
        AfficherEntetePhasage()
        If monPlanFeuxBase.PhasageInitialisé Then
          If SuiteADécalage Then AfficherPhasage(IndexPhasages(cboDécoupagePhases.SelectedIndex))
        End If
      End If

      maVariante.RéordonnerPlansFeux()
    End If

  End Sub

  Private Sub RenommerLignePlansFeux(ByVal uneLigneFeux As LigneFeux, ByVal exID As String)
    Dim unPlanBase As PlanFeuxBase

    For Each unPlanBase In mesPlansFeuxBase
      unPlanBase.RenommerLigneFeux(uneLigneFeux, exID)
    Next
    Me.RenommerColonnePlanFeux(uneLigneFeux)
  End Sub

  '**********************************************************************
  ' Changement de l'ordre de classement des lignes de feux
  '**********************************************************************
  Private Sub cboTriLignesFeux_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTriLignesFeux.SelectedIndexChanged
    Dim Ordre As LigneFeuxCollection.OrdreDeTriEnum

    If cboTriLignesFeux.SelectedIndex <> 0 Then
      mesLignesFeux.Trier(Ordre:=cboTriLignesFeux.SelectedIndex)
      AfficherLignesDeFeux()
      AfficherConséquencesModifLignesDeFeux()
    End If

  End Sub

  Private Sub btnSignalMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnSignalMoins.Click
    If Confirmation("Supprimer le signal", Critique:=False) Then

    End If
  End Sub
#End Region
#Region " Trafics"
  '==============================================================================================
  ' Procédures issues de dlgTrafic
  '==============================================================================================
  Private Sub txtCommentairePériode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommentairePériode.Validated
    MémoriserCommentaireTrafic()
  End Sub

  Private Sub txtCommentairePériode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommentairePériode.TextChanged
    monTraficActif.Commentaires = Me.txtCommentairePériode.Text.Trim

  End Sub
  '*******************************************************************************************************
  'Mémoriser le commentaire sur la période de trafic avant de changer d'onglet ou de période
  '********************************************************************************************************
  Private Sub MémoriserCommentaireTrafic()
    'Dim chaine As String = Me.txtCommentairePériode.Text.Trim
    'If Not IsNothing(monTraficActif) AndAlso Not ChargementEnCours Then
    '  monTraficActif.Commentaires = Me.txtCommentairePériode.Text.Trim
    'End If
  End Sub

  Private Sub chkModeTrafic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkModeTrafic.CheckedChanged
    Static QuestionPosée As Boolean
    Dim unTrafic As Trafic = mesTrafics(cboTrafic.Text)

    If QuestionPosée Then
      QuestionPosée = False
    ElseIf unTrafic Is monTraficActif() Then
      If monTraficActif.ChangeModeSaisieAccepté(chkModeTrafic.Checked) Then
        Me.pnlTrafic.Visible = Not chkModeTrafic.Checked
      Else
        QuestionPosée = True
        chkModeTrafic.Checked = Not chkModeTrafic.Checked
      End If
    Else
      Me.pnlTrafic.Visible = Not chkModeTrafic.Checked
    End If

  End Sub

  '*********************************************************************************************
  ' Choix du  type de trafic à afficher : VL, PL 2R ou UVP
  '*********************************************************************************************
  Private Sub radVehicule_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radPL.CheckedChanged, radVL.CheckedChanged, rad2Roues.CheckedChanged, radUVP.CheckedChanged

    If Not IsNothing(maVariante) Then
      Try
        AfficherTrafic(AvecLesPiétons:=False)
      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If

  End Sub

  Private Sub btnNouveauTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNouveauTrafic.Click
    Dim nomScénario As String = InputBox("Nom de la période de trafic à créer")
    Dim unScénario As PlanFeuxBase = monPlanFeuxBase()
    Dim unTrafic As Trafic

    With maVariante
      If nomScénario.Length = 0 Then
      ElseIf Not IsNothing(unScénario) AndAlso String.Compare(nomScénario, unScénario.Nom, ignoreCase:=True) = 0 Then
      ElseIf maVariante.mPlansFeuxBase.Contains(nomScénario) Then
        MessageBox.Show("Un scénario de même nom existe déjà")
      Else

        .CréerScénario(nomScénario, AvecTrafic:=True)
        NouveauScénario()
      End If
    End With

  End Sub

  Private Sub btnRenommerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenommerTrafic.Click
    RenommerScénario()
  End Sub

  Private Sub btnDupliquerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDupliquerTrafic.Click
    DupliquerScénario()
  End Sub

  Private Sub btnSupprimerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSupprimerTrafic.Click
    SupprimerScénario()
  End Sub


  Private Sub AjouterComboTrafic(ByVal nomTrafic As String)
    cboTrafic.Items.Add(nomTrafic)
    cboTraficFct.Items.Add(nomTrafic)
  End Sub

  Private Sub RenommerComboTrafic(ByVal nomTrafic As String, ByVal Index As Short)
    cboTrafic.Items(Index) = nomTrafic
    cboTraficFct.Items(Index + 1) = nomTrafic
  End Sub

  Private Sub SupprimerComboTrafic(ByVal Index As Short)
    cboTrafic.Items.RemoveAt(Index)
    cboTraficFct.Items.RemoveAt(Index + 1)
  End Sub

  '******************************************************************************
  ' Choix d'une nouvelle période de trafic dans la liste
  '******************************************************************************
  Private Sub cboTrafic_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles cboTrafic.SelectedIndexChanged
    Dim Index As Short = cboTrafic.SelectedIndex

    MémoriserCommentaireTrafic()

    Try
      If Index <> -1 Then

        mdiApplication.cboScénario.Text = Me.cboTrafic.Text
        Me.chkVerrouPériode.Checked = monTraficActif.Verrouillé

        Me.chkModeTrafic.Checked = monTraficActif.UVP

        AfficherTrafic(AvecLesPiétons:=True)

      Else
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  '******************************************************************************
  ' Afficher les données du trafic sélectionné
  ' AvecLesPiétons : si vrai, il faut aussi afficher les données du trafic piéton
  '							sinon, c'est une simple bascule entre les catégories de véhicules
  '******************************************************************************
  Private Sub AfficherTrafic(ByVal AvecLesPiétons As Boolean)
    Dim Index As Trafic.TraficEnum
    Dim i, j As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrilleTraficVéhicules
    Dim TotalTrafic As Integer
    Dim uneBrancheEntrante, uneBrancheSortante As Branche
    Dim SenUniqueEntrant, SenUniqueSortant As Boolean

    Index = IndexTrafic()

    Try

      With monTraficActif()

        For Each uneBrancheEntrante In mesBranches
          If Not uneBrancheEntrante.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
            'Trafics entrants de la branche i vers chaque branche
            i = mesBranches.IndexOf(uneBrancheEntrante) + 1

            For Each uneBrancheSortante In mesBranches
              If Not uneBrancheEntrante Is uneBrancheSortante AndAlso Not uneBrancheSortante.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
                j = mesBranches.IndexOf(uneBrancheSortante) + 1
                fg(i, j) = .QVéhicule(Index, i - 1, j - 1)
              End If
            Next
            'Total trafic entrant par la branche  i
            fg(i, mesBranches.Count + 1) = .QE(Index, i - 1)

          End If

        Next

        'Total trafics sortants vers chaque branche
        For Each uneBrancheSortante In mesBranches
          If Not uneBrancheSortante.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
            j = mesBranches.IndexOf(uneBrancheSortante) + 1
            fg(mesBranches.Count + 1, j) = .QS(Index, j - 1)
          End If
        Next

        'Total trafic du carrefour
        fg(mesBranches.Count + 1, mesBranches.Count + 1) = .QTotal(Index)

        If AvecLesPiétons Then
          fg = Me.Ac1GrilleTraficPiétons
          For i = 1 To mesBranches.Count
            fg(1, i - 1) = .QPiéton(i - 1)
          Next
        End If

        Me.txtCommentairePériode.Text = .Commentaires
      End With

      AfficherTraficSaturé()

      ActiverBoutonsTrafics()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherTrafic")

    End Try

  End Sub

  Private Sub ActiverBoutonsTrafics()
    Me.btnSupprimerTrafic.Enabled = cboTrafic.Items.Count > 0
    Me.chkVerrouPériode.Enabled = monPlanFeuxBase.Verrou < [Global].Verrouillage.Matrices

    Me.btnRenommerTrafic.Enabled = cboTrafic.Items.Count > 0

    ControlerAffichageTrafic()
  End Sub

  Private Sub ControlerAffichageTrafic()

    Dim Activé As Boolean = Not monTraficActif.Verrouillé
    Me.Ac1GrilleTraficPiétons.Enabled = Activé
    Me.AC1GrilleTraficVéhicules.Enabled = Activé
    chkModeTrafic.Enabled = Activé

  End Sub

  '******************************************************************************
  '	IndexTrafic : retourne l'index de la catégorie de véhicules(VL - PL- 2 ROues)
  '******************************************************************************
  Private Function IndexTrafic() As Trafic.TraficEnum

    If Me.chkModeTrafic.Checked Then
      IndexTrafic = Trafic.TraficEnum.UVP
    Else
      If Me.radVL.Checked Then  ' VL
        IndexTrafic = Trafic.TraficEnum.VL
      ElseIf Me.radPL.Checked Then  'PL
        IndexTrafic = Trafic.TraficEnum.PL
      ElseIf Me.rad2Roues.Checked Then  '2 roues
        IndexTrafic = Trafic.TraficEnum.DEUXR
      Else   'UVP
        IndexTrafic = Trafic.TraficEnum.UVP
      End If
    End If

  End Function

  Private Sub chkVerrouPériode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVerrouPériode.CheckedChanged

    If Not ChangementDeScénario Then
      Try
        If Me.chkVerrouPériode.Checked And monPlanFeuxBase.Trafic.QTotal(Trafic.TraficEnum.UVP) = 0 Then
          MessageBox.Show("Saisir d'abord les trafics")
          Me.chkVerrouPériode.Checked = False

        Else
          If VérifierAntagonismesSaisis() Then
            monTraficActif.Verrouillé = chkVerrouPériode.Checked
            ActiverBoutonsTrafics()

          Else
            'Pour que l'évènement déclenché ensuite ne fasse rien :
            'ChangementDeScénario = True
            Me.chkVerrouPériode.Checked = True
            'ChangementDeScénario = False
          End If
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If
  End Sub

  Private Function VérifierAntagonismesSaisis() As Boolean

    If ModeGraphique Then
      Try

        'If maVariante.Verrou >= Global.Verrouillage.LignesFeux And Not monTraficActif.Verrouillé Then
        'Correction AV : 26/03/07
        If maVariante.Verrou >= [Global].Verrouillage.LignesFeux And monTraficActif.Verrouillé And Not Me.chkVerrouPériode.Checked Then
          If monPlanFeuxBase.Antagonismes.ConflitsPartiellementRésolus AndAlso Not ChargementEnCours Then
            If Confirmation("Réinitialiser les antagonismes", Critique:=False) Then
              RéinitialiserAntagonismes()
              Return True
            Else
              Return False
            End If

          Else
            Return True
          End If

        Else
          Return True
        End If

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "VérifierAntagonismesSaisis")

      End Try

    Else
      'Sans objet pour le mode tableur
      Return True
    End If

  End Function

#End Region
#Region " Matrices de sécurité"

  '**********************************************************************************************************************
  'Changement d'item dans le Panel Matrices de sécurité
  '**********************************************************************************************************************
  Private Sub radMatriceConflits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radMatriceConflits.CheckedChanged, radMatriceRougesDégagement.CheckedChanged, radMatriceInterverts.CheckedChanged

    Dim Index As Short
    If radMatriceConflits.Checked Then
      Index = 0
    ElseIf radMatriceRougesDégagement.Checked Then
      Index = 1
    ElseIf Me.radMatriceInterverts.Checked Then
      Index = 2
    Else
      Index = -1
    End If

    If ModeGraphique Then Me.pnlAntagonismes.Visible = (Index = 0 And mAntagonismes.NonTousSystématiques)

    Dim MatriceNonVerrouillée As Boolean = maVariante.Verrou < Verrouillage.Matrices

    Try

      With Me.Ac1GrilleSécurité

        Select Case Index
          Case -1
            'Par défaut : Matrice des conflits
            Me.radMatriceConflits.Checked = True

          Case 0
            'Matrice des conflits
            .Enabled = MatriceNonVerrouillée
            chkVerrouMatrice.Enabled = True

          Case Else
            If MatriceNonVerrouillée Then
              AfficherMessageErreur(Me, "Verrouiller d'abord la matrice des conflits")
              Me.radMatriceConflits.Checked = True
              Index = -1
            Else
              chkVerrouMatrice.Enabled = False
              If Index = 1 Then
                'Il est possible de modifier les rouges de dégagement jusqu'à ce qu'on ait choisi un plan de feux de base
                .Enabled = Not PhasageRetenu
              Else
                .Enabled = False
              End If
            End If
        End Select
      End With

      If Index <> -1 Then
        Me.pnlVerrouMatrice.Visible = (Index = 0)
        Me.pnlBoutonsRouges.Visible = (Index = 1 And Ac1GrilleSécurité.Enabled And ModeGraphique)
        AfficherMatriceSécurité(Index)
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '***********************************************************************************
  ' Retourne l'index du bouton radio sélectionné dans l'onglet Conflits
  '***********************************************************************************
  Private Property pnlConflitsIndex() As Short
    Get
      If Me.radMatriceConflits.Checked Then
        Return 0
      ElseIf Me.radMatriceRougesDégagement.Checked Then
        Return 1
      ElseIf Me.radMatriceInterverts.Checked Then
        Return 2
      Else
        Return -1
      End If
    End Get
    Set(ByVal Value As Short)
      Select Case Value
        Case 0
          Me.radMatriceConflits.Checked = True
        Case 1
          Me.radMatriceRougesDégagement.Checked = True
        Case 2
          Me.radMatriceInterverts.Checked = True
        Case -1
          Me.radMatriceConflits.Checked = False
          Me.radMatriceRougesDégagement.Checked = False
          Me.radMatriceInterverts.Checked = False
          monPlanPourPhasage = Nothing
      End Select
    End Set
  End Property

  '**********************************************************************************************************************
  'Changement d'item dans le Panel Matrices de sécurité
  '**********************************************************************************************************************

  '**********************************************************************************************************************
  'Affichage de la matrice de sécurité correspondant à l'index choisi
  '**********************************************************************************************************************
  Private Sub AfficherMatriceSécurité(ByVal Index As Short)
    Dim lHorizontale, lVerticale As LigneFeux
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleSécurité
    Dim rg As Grille.CellRange
    Dim row, col As Short

    Try

      'Remettre à blanc les données de la grille(sans les entete)
      rg = fg.PlageDonnées
      rg.Clear(Grille.ClearFlags.Content)

      For Each lHorizontale In mesLignesFeux
        row = mesLignesFeux.IndexOf(lHorizontale) + 1
        For Each lVerticale In mesLignesFeux
          col = mesLignesFeux.IndexOf(lVerticale) + 1
          rg = fg.GetCellRange(row, col)
          ' lHorizontale désigne la ligne de feux horizontale
          ' lVerticale désigne la ligne de feux verticale
          If lHorizontale.EstTrivialementCompatible(lVerticale) Then
            rg.Style = StyleGrisé
          ElseIf mLignesFeux.EstIncompatible(lHorizontale, lVerticale) Then
            Select Case Index
              Case 0         ' Matrice des conflits
                rg.Style = StyleRouge
              Case 1         ' rouges de dégagement
                AfficherRouge(lHorizontale, lVerticale, rg, fg)
              Case 2         ' interverts
                rg.Style = StyleGriséGras          ' .Styles(Grille.CellStyleEnum.Normal)
                rg.Data = mLignesFeux.InterVerts(lHorizontale, lVerticale)
            End Select
          Else
            rg.Style = StyleVert
          End If
        Next
      Next

      If ModeGraphique Then
        AfficherAntagosDansMatrice(fg)
        If Index = 1 Then ActiverBoutonsRouges()
      End If

      'Annuler la sélection
      fg.Désélectionner()

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherMatriceSécurité")
    End Try

  End Sub

  '**********************************************************************************************************************
  ' Rétablir l'ensemble des valeurs par défaut (celles calculées par  DIAGFEUX) des rouges de dégagement
  '**********************************************************************************************************************
  Private Sub btnRougesDéfaut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRougesDéfaut.Click
    Dim lh, lv As LigneFeux

    For Each lh In mLignesFeux()
      For Each lv In mLignesFeux()
        If Not lh Is lv Then
          If Not lh.EstTrivialementCompatible(lv) Then
            'La valeur par défaut du rouge de dégagement du plan de feux de base 
            'est celui calculé comme rouge mini pour les lignes de feux de la variante (cf DéterminerTempsDégagement)
            mLignesFeux.TempsDégagement(lh, lv) = mesLignesFeux.RougeDégagement(lh, lv)
          End If
        End If
      Next
    Next

    AfficherMatriceSécurité(1)

  End Sub

  '**********************************************************************************************************************
  ' Rétablir la valeur par défaut (celle calculée par  DIAGFEUX) du rouge de dégagement
  '**********************************************************************************************************************
  Private Sub btnRougeDéfaut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRougeDéfaut.Click
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleSécurité
    Dim rg As Grille.CellRange
    Dim row, col As Short
    row = fg.Row
    col = fg.Col
    If row = -1 Then
      MessageBox.Show("Sélectionner d'abord la valeur à restaurer")

    Else
      rg = fg.GetCellRange(row, col)
      Dim lh, lv As LigneFeux
      ' lh désigne la ligne de feux horizontale (au sens matriciel)
      ' lv désigne la ligne de feux verticale
      lh = mLignesFeux(CType(row - 1, Short))
      lv = mLignesFeux(CType(col - 1, Short))
      If Not lh.EstTrivialementCompatible(lv) Then

        'La valeur par défaut du rouge de dégagement du plan de feux de base 
        'est celui calculé comme rouge mini pour les lignes de feux de la variante (cf DéterminerTempsDégagement)
        mLignesFeux.TempsDégagement(lh, lv) = mesLignesFeux.RougeDégagement(lh, lv)
        AfficherRouge(lh, lv, rg, fg)
        ActiverBoutonsRouges()
      End If

    End If
  End Sub

  '**********************************************************************************************
  ' Mettre en orangé dans la matrice les lignes de feux correspondant à un antagonisme non résolu
  '**********************************************************************************************
  Private Sub AfficherAntagosDansMatrice(ByVal fg As GrilleDiagfeux)
    Dim rg As Grille.CellRange
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In mAntagonismes()
      With unAntagonisme
        If .MêmesCourants Is unAntagonisme Then
          ' Mettre en orangé les cases correspondant à des antagonismes encore sans décision
          If .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
            Dim l1 As LigneFeux = .LigneFeu(Antagonisme.PositionEnum.Premier)
            Dim l2 As LigneFeux = .LigneFeu(Antagonisme.PositionEnum.Dernier)
            With mLignesFeux()
              If Not .EstIncompatible(l1, l2) Then
                'Sinon la case est déjà en rouge pour un conflit systématique : on la laisse en rouge
                rg = fg.GetCellRange(.IndexOf(l1) + 1, .IndexOf(l2) + 1)
                rg.Style = StyleOrangé
                rg = fg.GetCellRange(.IndexOf(l2) + 1, .IndexOf(l1) + 1)
                rg.Style = StyleOrangé
              End If
            End With
          End If
        End If
      End With
    Next

  End Sub

  Private Sub cboBrancheCourant1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBrancheCourant1.SelectedIndexChanged

    Dim uneBranche As Branche
    If Me.cboBrancheCourant1.SelectedIndex = mesBranches.Count Then
      maVariante.BrancheEnCoursAntagonisme = Nothing
    Else
      uneBranche = mesBranches(Me.cboBrancheCourant1.SelectedIndex)
      maVariante.BrancheEnCoursAntagonisme = uneBranche
    End If

    AfficherAntagonismes()
  End Sub

  Private Sub AfficherAntagonismes()
    Dim fgAntago As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim uneBranche As Branche = maVariante.BrancheEnCoursAntagonisme
    Dim rg As Grille.CellRange

    Try

      For Each unAntagonisme In mAntagonismes()
        With unAntagonisme
          If .TypeConflit <> Trajectoire.TypeConflitEnum.Systématique AndAlso .MêmesCourants Is unAntagonisme Then
            'Masquer les objets graphiques représentant les points de conflit  dont le 1er courant n'a pas pour origine la branche sélectionnée
            unAntagonisme.Verrouiller()
            '(Dé)Masquer également la ligne d'antagonismes 
            Dim row As Short = mAntagonismes.IndexOf(unAntagonisme) + 1
            If IsNothing(uneBranche) Then
              fgAntago.Rows(row).Visible = True
            Else
              fgAntago.Rows(row).Visible = unAntagonisme.BrancheCourant1 Is uneBranche
            End If

            If AntagonismesEnCours Then
              rg = fgAntago.GetCellRange(row, 2)
              DéfinirStyle(unAntagonisme, rg)
            End If
          End If
        End With
      Next

      If ConflitsInitialisés Then
        Redessiner()
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherAntagonismes")
    End Try


  End Sub

  Private Sub RéafficherAntagonismes()

    AntagonismesEnCours = True

    Dim Index As Short = Me.cboBrancheCourant1.SelectedIndex

    If IsNothing(maVariante.BrancheEnCoursAntagonisme) Then
      Index = mesBranches.Count
    Else
      Index = mesBranches.IndexOf(maVariante.BrancheEnCoursAntagonisme)
    End If

    If Index = Me.cboBrancheCourant1.SelectedIndex Then
      AfficherAntagonismes()
    Else
      Me.cboBrancheCourant1.SelectedIndex = Index
    End If

    AntagonismesEnCours = False

  End Sub

  Private Property ConflitsInitialisés() As Boolean
    Get
      Return maVariante.ConflitsInitialisés
    End Get
    Set(ByVal Value As Boolean)
      maVariante.ConflitsInitialisés = Value
    End Set
  End Property

  Private ReadOnly Property ConflitsPartiellementRésolus()
    Get
      Dim unScénario As PlanFeuxBase
      For Each unScénario In maVariante.mPlansFeuxBase
        If unScénario.Antagonismes.ConflitsPartiellementRésolus Then
          Return True
        End If
      Next
    End Get
  End Property

  Private Function AntagonismeLiéRefusé(ByVal unAntagonisme As Antagonisme, ByVal Admis As Boolean, Optional ByVal AppelDepuisGrille As Boolean = False) As Boolean
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes

    Try

      With unAntagonisme
        If Admis AndAlso .FilsNonAdmis(mAntagonismes) Then
          If AppelDepuisGrille AndAlso .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
          Else
            MessageBox.Show("Ce conflit avec les piétons ne peut être admis car le conflit avec le courant véhicule adverse ne l'est pas")
          End If
          AntagonismeLiéRefusé = True
        End If
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AntagonismeLiéRefusé")
    End Try

  End Function

  Private Sub MettreAJourConflit(ByVal unAntagonisme As Antagonisme, Optional ByVal TypeConflit As Trajectoire.TypeConflitEnum = Trajectoire.TypeConflitEnum.Systématique)

    Try
      If unAntagonisme.TypeConflit <> TypeConflit Then
        Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
        Dim rg As Grille.CellRange = fg.GetCellRange(mAntagonismes.IndexOf(unAntagonisme) + 1, 2)
        unAntagonisme.TypeConflit = TypeConflit
        rg.Data = (TypeConflit = Trajectoire.TypeConflitEnum.Admis)
      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "MettreAJourConflit")
    End Try

  End Sub
#End Region
#Region " Phasage"

  Private Sub AfficherComboPhasage()

    If AffichagePhasesEnCours Then Exit Sub

    Dim unPlanFeux As PlanFeuxPhasage
    Dim Ajouter As Boolean
    Dim Index, IndexCombo, IndexRetenu, IndexRetenuSecondaire As Short
    IndexRetenu = -1
    IndexRetenuSecondaire = -1

    Try

      ReDim IndexPhasages(mesPlansPourPhasage.Count - 1)

      Me.cboDécoupagePhases.Items.Clear()

      For Each unPlanFeux In mesPlansPourPhasage()
        With unPlanFeux
          If .mPhases.Count = MAXPHASES Then
            If Me.chk3Phases.Checked Then
              Select Case mFiltrePhasage.LigneFeuxMultiPhases
                Case FiltrePhasage.LFMultiphasesEnum.Inclure
                  Ajouter = True
                Case FiltrePhasage.LFMultiphasesEnum.Exclure
                  Ajouter = Not .mLigneFeuxMultiPhases
                Case FiltrePhasage.LFMultiphasesEnum.Uniquement
                  Ajouter = .mLigneFeuxMultiPhases
              End Select

              Select Case mFiltrePhasage.AvecPhaseSpéciale
                Case FiltrePhasage.PhaseSpécialeEnum.Inclure
                  Ajouter = Ajouter And True
                Case FiltrePhasage.PhaseSpécialeEnum.Exclure
                  Ajouter = Ajouter And Not .mAvecPhaseSpéciale
                Case FiltrePhasage.PhaseSpécialeEnum.Uniquement
                  Ajouter = Ajouter And .mAvecPhaseSpéciale
              End Select

            Else
              'Feu 3 phases rejeté car case à cocher 3 phases décochée
              Ajouter = False
            End If

          Else
            ' Toujours accepter 2 phases
            Ajouter = True
            'Sauf si des restrictions supplémentaires ont été apportées
            If mFiltrePhasage.LigneFeuxMultiPhases = FiltrePhasage.LFMultiphasesEnum.Uniquement Then
              Ajouter = False
            End If
            If mFiltrePhasage.AvecPhaseSpéciale = FiltrePhasage.PhaseSpécialeEnum.Uniquement Then
              Ajouter = False
            End If
          End If

          Select Case mFiltrePhasage.CritèreCapacité
            Case FiltrePhasage.CapacitéEnum.MoinsDix
              Ajouter = Ajouter And .RéserveCapacitéPourCent < 10
            Case FiltrePhasage.CapacitéEnum.DixVingt
              Ajouter = Ajouter And .RéserveCapacitéPourCent >= 10 And .RéserveCapacitéPourCent < 20
            Case FiltrePhasage.CapacitéEnum.PlusVingt
              Ajouter = Ajouter And .RéserveCapacitéPourCent >= 20
          End Select

        End With

        If Ajouter Then
          With Me.cboDécoupagePhases
            Index = mesPlansPourPhasage.IndexOf(unPlanFeux)
            IndexCombo = .Items.Count
            .Items.Add("Phasage " & CStr(Index + 1))
            IndexPhasages(IndexCombo) = Index
            'Mémoriser l'index du plan en cours d'affichage
            If monPlanPourPhasage Is unPlanFeux Then
              IndexRetenu = IndexCombo
            End If
            'Mémoriser l'index du plan de feux de base
            If unPlanFeux.PlanBaseAssocié Is monPlanFeuxBase Then
              IndexRetenuSecondaire = IndexCombo
            End If

          End With
        End If
      Next

      If IndexRetenu <> -1 Then
        'Réafficher le phasage en cours
        Me.cboDécoupagePhases.SelectedIndex = IndexRetenu
      ElseIf IndexRetenuSecondaire <> -1 Then
        'Afficher le phasage du plan de feux de base
        Me.cboDécoupagePhases.SelectedIndex = IndexRetenuSecondaire
      ElseIf Me.cboDécoupagePhases.Items.Count > 0 Then
        'Afficher le 1er phasage
        Me.cboDécoupagePhases.SelectedIndex = 0
      Else
        'Aucun ne plan ne convient aux critères : Masquer la grille
        ActiverAspectPhases(Affiché:=False)
        monPlanPourPhasage = Nothing
      End If

      ActiverChoixDécoupage()
      Me.lblDécoupagePhases.Text = Me.cboDécoupagePhases.Items.Count & " phasages / " & mesPlansPourPhasage.Count & " possibles"

    Catch ex As DiagFeux.Exception
      AfficherMessageErreur(Me, ex)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub ActiverChoixDécoupage()
    If IsNothing(monPlanPourPhasage) Then
      'La combinaison des choix a conduit à n'avoir aucune organisation de proposée
      Me.chkDécoupagePhases.Enabled = Me.cboDécoupagePhases.Items.Count > 0

    Else
      If PhasageRetenu Then
        Me.chkDécoupagePhases.Enabled = Me.cboDécoupagePhases.Items.Count > 0 And monPlanPourPhasage Is monPlanFeuxBase.PlanPhasageAssocié AndAlso maVariante.Verrou < [Global].Verrouillage.PlanFeuBase
      Else
        Me.chkDécoupagePhases.Enabled = Not monPlanPourPhasage.PhasageIncorrect
      End If
    End If

  End Sub

  Private Sub chk3Phases_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk3Phases.CheckedChanged

    If chk3Phases.Checked Then
      Me.cbolLFMultiPhases.Enabled = True
      Me.cboPhasesSpéciales.Enabled = True
    Else
      Me.cbolLFMultiPhases.Enabled = False
      Me.cboPhasesSpéciales.Enabled = False
    End If

    AfficherComboPhasage()
  End Sub

  Private Sub cbolLFMultiPhases_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolLFMultiPhases.SelectedIndexChanged
    mFiltrePhasage.LigneFeuxMultiPhases = Me.cbolLFMultiPhases.SelectedIndex
    AfficherComboPhasage()
  End Sub

  Private Sub cboPhasesSpéciales_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPhasesSpéciales.SelectedIndexChanged
    mFiltrePhasage.AvecPhaseSpéciale = Me.cboPhasesSpéciales.SelectedIndex
    AfficherComboPhasage()
  End Sub

  Private Sub cboCapacité_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRéserveCapacité.SelectedIndexChanged
    mFiltrePhasage.CritèreCapacité = cboRéserveCapacité.SelectedIndex
    AfficherComboPhasage()
  End Sub

  '**********************************************************************************************************************
  ' Choix d'une autre organisation de phasage dans la liste déroulante
  '**********************************************************************************************************************
  Private Sub cboDécoupage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles cboDécoupagePhases.SelectedIndexChanged

    If PhasageModifié(monPlanPourPhasage) Then
      ComposerPhasage()
    End If

    AfficherPhasage(IndexPhasages(cboDécoupagePhases.SelectedIndex))

  End Sub

  '**********************************************************************************************************************
  ' Afficher dans l'organisation du phasage celui correspondant à l'index choisi
  '**********************************************************************************************************************
  Private Sub AfficherPhasage(ByVal Index As Short)
    Dim unePhase As Phase
    Dim row, col As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim rg As Grille.CellRange
    Dim uneLigneFeux As LigneFeux

    Try
      'Désavtiver l' évènement AC1GrillePhases.CellChanged
      AffichagePhasesEnCours = True

      'Déterminer le plan de feu de base associé
      monPlanPourPhasage = mesPlansPourPhasage(Index)

      With monPlanPourPhasage
        Me.txtRéserveCapacitéPourCent.Text = .strRéserveCapacitéPourCent

        'Affichage contextuel selon le nombre de phases
        ActiverAspectPhases(CType(.mPhases.Count, Short))
        'Remettre à blanc toutes les cellules
        For col = 1 To fg.Cols.Count - 1    ' .mPhases.Count
          For row = 1 To mesLignesFeux.Count
            rg = fg.GetCellRange(row, col)
            rg.Checkbox = Grille.CheckEnum.Unchecked
            rg.Style = StyleDégrisé
          Next
        Next

        'Cocher les cases adééquates pour les lignes de feux de chaque phase
        col = 0
        For Each unePhase In .mPhases
          col += 1
          For Each uneLigneFeux In unePhase.mLignesFeux
            rg = fg.GetCellRange(mesLignesFeux.IndexOf(uneLigneFeux) + 1, col)
            rg.Checkbox = Grille.CheckEnum.Checked
          Next
        Next

        For col = 1 To .mPhases.Count
          TraiterOrangé(col)
        Next
        Me.DéterminerPhasageCorrect()
        Me.chkDécoupagePhases.Checked = monPlanPourPhasage.PlanBaseAssocié Is monPlanFeuxBase
      End With

      'Réactiver l'évènement AC1GrillePhases.CelllChanged
      AffichagePhasesEnCours = False

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherPhasage")
    End Try

  End Sub

  '**********************************************************************************************************************
  ' Détermine si l'organisation du phasage a été modifié
  '**********************************************************************************************************************
  Private Function PhasageModifié(ByVal unPlanFeu As PlanFeuxPhasage) As Boolean
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    If IsNothing(unPlanFeu) Then
      Return False
      'Déterminer si le nombre de phases est passé de 2 à 3 ou inversement
    ElseIf (unPlanFeu.mPhases.Count = MAXPHASES) Xor (fg.Cols(MAXPHASES).Visible) Then
      Return True

    Else
      Dim row, col As Short
      Dim rg As Grille.CellRange
      Dim uneLigneFeux As LigneFeux
      Dim unePhase As Phase
      Dim MaxCol As Short = Math.Min(fg.Cols.Count - 1, unPlanFeu.mPhases.Count)
      'Déterminer si une ligne de feux a changé de phase
      For row = 1 To fg.Rows.Count - 1
        uneLigneFeux = mesLignesFeux(CType(row - 1, Short))
        For col = 1 To MaxCol
          unePhase = unPlanFeu.mPhases(CType(col - 1, Short))
          rg = fg.GetCellRange(row, col)
          If (rg.Checkbox = Grille.CheckEnum.Checked) Xor (unePhase.mLignesFeux.Contains(uneLigneFeux)) Then
            Return True
          End If
        Next
      Next
    End If

  End Function

  '**********************************************************************************************************************
  'Déterminer si le phasage affiché est correct
  'Il ne l'est pas si 2 lignes de feux incompatibles sont présentes dans la même colonne(phase)
  'Il ne l'est pas s'il manque une ligne de feux dans l'ensemble des phases 
  '**********************************************************************************************************************
  Private Sub ComposerPhasage()
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim row, col As Short
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle
    Dim uneLigneFeux As LigneFeux
    Dim unePhase As Phase
    Dim MaxCol As Short = MaxColPhasage()

    monPlanPourPhasage.mPhases.Clear()
    For col = 1 To MaxCol
      'Créer une phase
      unePhase = New Phase
      For Each uneLigneFeux In mesLignesFeux
        'Ajouter les lignes de feux à la phase
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        rg = fg.GetCellRange(row, col)
        If rg.Checkbox = Grille.CheckEnum.Checked Then
          unePhase.mLignesFeux.Add(uneLigneFeux)
          unStyle = rg.Style
        End If
      Next
      'Ajouter la phase dans le plan
      monPlanPourPhasage.AddPhases(unePhase)
    Next

  End Sub
  '**********************************************************************************************************************
  'Retourne le numéro maxi de la colonne lors de l'organisation du phasage
  '		2 ou 3(MAXPHASAGES) selon que la dernière colonne soit visible ou non
  '**********************************************************************************************************************
  Private Function MaxColPhasage() As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    MaxColPhasage = IIf(fg.Cols(MAXPHASES).Visible, MAXPHASES, MAXPHASES - 1)
  End Function

  '**********************************************************************************************************************
  'Déterminer si le phasage affiché est correct
  'Il ne l'est pas si 2 lignes de feux incompatibles sont présentes dans la même colonne(phase)
  'Il ne l'est pas s'il manque une ligne de feux dans l'ensemble des phases 
  '**********************************************************************************************************************
  Private Sub DéterminerPhasageCorrect()
    'Présence des lignes de feux dans les colonnes(phases)
    Dim cpt(mesLignesFeux.Count - 1) As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    Dim unStyle As Grille.CellStyle
    Dim row, col As Short
    Dim rg As Grille.CellRange
    Dim uneLigneFeux As LigneFeux
    Dim MaxCol As Short = MaxColPhasage()
    Dim i As Short
    Dim Incorrect As Boolean

    Try
      For col = 1 To MaxCol
        For row = 1 To mesLignesFeux.Count
          uneLigneFeux = mesLignesFeux(CType(row - 1, Short))
          rg = fg.GetCellRange(row, col)
          If rg.Checkbox = Grille.CheckEnum.Checked Then
            cpt(mesLignesFeux.IndexOf(uneLigneFeux)) += 1
          End If
          unStyle = rg.Style
          If unStyle.Name = StyleOrangé.Name Then
            Incorrect = True
            Exit For
          End If
        Next
        If Incorrect Then
          Me.lblConflitPotentiel.Visible = True
          Me.pnlConflitPotentiel.Visible = True
          Exit For
        Else
          Me.lblConflitPotentiel.Visible = False
          Me.pnlConflitPotentiel.Visible = False
        End If
      Next

      If Not Incorrect Then
        For i = 0 To mesLignesFeux.Count - 1
          If cpt(i) = 0 Then
            Incorrect = True
          End If
        Next
      End If

      With monPlanPourPhasage
        .PhasageIncorrect = Incorrect
      End With

      DéterminerAfficherCapacité(monPlanPourPhasage)

      ActiverChoixDécoupage()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DéterminerPhasageCorrect")
    End Try

  End Sub

  '**********************************************************************************************************************
  'Déterminer les cellules à mettre en orangé si sont activées des lignes de feux incompatibles
  '**********************************************************************************************************************
  Private Sub TraiterOrangé(ByVal col As Short)
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    Dim rg, rg1, rg2 As Grille.CellRange
    Dim row, row2 As Short
    Dim uneLigneFeux1, uneLigneFeux2 As LigneFeux
    Dim unStyle As Grille.CellStyle
    Dim Activé As Boolean

    'Dégriser toute la colonne
    rg = fg.TouteLaColonne(col)
    rg.Style = StyleDégrisé

    'Parcourir tous les couples de lignes de feux
    For row = 1 To fg.Rows.Count - 1
      rg1 = fg.GetCellRange(row, col)
      If rg1.Checkbox = Grille.CheckEnum.Checked Then
        For row2 = row + 1 To fg.Rows.Count - 1
          uneLigneFeux1 = mesLignesFeux(CType(row - 1, Short))
          uneLigneFeux2 = mesLignesFeux(CType(row2 - 1, Short))
          rg2 = fg.GetCellRange(row2, col)
          Activé = (rg2.Checkbox = Grille.CheckEnum.Checked)
          If mLignesFeux.EstIncompatible(uneLigneFeux1, uneLigneFeux2) And Activé Then
            rg1.Style = StyleOrangé
            rg2.Style = StyleOrangé
          End If
        Next
      End If
    Next

  End Sub

  '**********************************************************************************************************************
  'Affichage contextuel selon le nombre de phases
  '**********************************************************************************************************************
  Private Sub btnActionPhase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnActionPhase.Click
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim rg As Grille.CellRange = fg.Selection
    Dim rg2 As Grille.CellRange
    Dim row, col, row2, col2 As Short


    Try

      If fg.Cols(MAXPHASES).Visible Then
        'Supprimer la phase sélectionnée
        If rg.c1 = rg.c2 And rg.r1 = 1 And rg.r2 = fg.Rows.Count - 1 Then

          AffichagePhasesEnCours = True
          For col = rg.c1 To 2
            For row = 1 To fg.Rows.Count - 1
              rg = fg.GetCellRange(row, col + 1)
              rg2 = fg.GetCellRange(row, col)
              rg2.Checkbox = rg.Checkbox
            Next
          Next
          For row = 1 To fg.Rows.Count - 1
            rg = fg.GetCellRange(row, MAXPHASES)
            rg.Style = StyleDégrisé
            rg.Checkbox = Grille.CheckEnum.Unchecked
          Next
          TraiterOrangé(1)
          TraiterOrangé(2)

          DéterminerPhasageCorrect()
          AffichagePhasesEnCours = False
          ActiverAspectPhases(CType(MAXPHASES - 1, Short))
        Else
          MessageBox.Show(Me, "Sélectionner d'abord la phase à supprimer", NomProduit, MessageBoxButtons.OK)
        End If

      Else
        'Rajouter une 3ème phase
        ActiverAspectPhases(MAXPHASES)
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '**********************************************************************************************************************
  'Affichage contextuel selon le nombre de phases
  '**********************************************************************************************************************
  Private Overloads Sub ActiverAspectPhases(ByVal nbPhases As Short)
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    ActiverAspectPhases(Affiché:=True)

    If nbPhases = MAXPHASES Then
      fg.Cols(MAXPHASES).Visible = True
      Me.btnActionPhase.Text = "Supprimer la phase"
    Else
      fg.Cols(MAXPHASES).Visible = False
      Me.btnActionPhase.Text = "Ajouter une phase"
    End If

  End Sub

  Private Overloads Sub ActiverAspectPhases(ByVal Affiché As Boolean)
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim i As Short

    For i = 1 To MAXPHASES
      fg.Cols(i).Visible = Affiché
    Next
    Me.btnActionPhase.Enabled = Affiché

  End Sub

  '**********************************************************************************************************************
  'Affecter l'organisation affichée au plan de feux de base en cours de la variante
  '**********************************************************************************************************************
  Private Sub chkDécoupagePhases_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkDécoupagePhases.CheckedChanged

    If ChargementEnCours Then
      monPlanFeuxBase.DéterminerAutorisationsDécalage()
      Exit Sub
    End If

    Try

      'Eviter que la saisie des rouges de dégagement,si active auparavant,  ne le soit plus automatiquement
      Me.radMatriceConflits.Checked = True

      If chkDécoupagePhases.Checked Then
        If Not PhasageRetenu Then ' sinon simple réactivation de la case suite à la sélection du phasage retenu
          'Sélectionner comme plan de feux de base celui en cours d'affichage
          DéduirePlanbasePhasage()
          'Ajouter le plan la a 1ère fois que la case est cochée (elle peutl'être ensuite par programme)
          'AV (21/02/2007) :Nouvelle définition du phasage retenu
          'maVariante.mPlansFeuxBase.Add(monPlanFeuxBase)

        Else
          monPlanPourPhasage = monPlanFeuxBase.PlanPhasageAssocié
          AfficherComboPhasage()
          'Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
          'fg.Cols(MAXPHASES).Visible = (monPlanPourPhasage.mPhases.Count = MAXPHASES)

        End If

        monPlanFeuxBase.DéterminerAutorisationsDécalage()

      Else
        'Décochage du [phasage_retenu]
        If PhasageRetenu AndAlso monPlanPourPhasage Is monPlanFeuxBase.PlanPhasageAssocié Then
          'Décochage manuel du phasage retenu (et non par programme suite au choix d'un autre Phasage que celui retenu pour affichage)
          'AV (21/02/2007) :Nouvelle définition du phasage retenu
          'maVariante.mPlansFeuxBase.Remove(monPlanFeuxBase)
          monPlanFeuxBase.PlanPhasageAssocié = Nothing

          'Lignes suivantes : pour éviter des controles intempestifs lors des prochaines initialisation du plan de feux de base
          updPhase1Base.Tag = Nothing
          updPhase2Base.Tag = Nothing
          updPhase3Base.Tag = Nothing

        End If

      End If

      VerrouillerOrgaPhasage()

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  Private Sub DéduirePlanbasePhasage()
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim rg As Grille.CellRange
    Dim row, col As Short
    Dim unePhase As Phase

    Try

      For col = 1 To MAXPHASES
        If fg.Cols(col).Visible Then monPlanFeuxBase.AddPhases(New Phase)
      Next

      col = 0
      For Each unePhase In monPlanFeuxBase.mPhases
        col += 1
        For row = 1 To fg.Rows.Count - 1
          rg = fg.GetCellRange(row, col)
          If rg.Checkbox = Grille.CheckEnum.Checked Then
            unePhase.mLignesFeux.Add(mesLignesFeux(CType(row - 1, Short)))
          End If
        Next
      Next

      AssocierPlanBasePhasage()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DéduirePlanbasePhasage")
    End Try

  End Sub

  Private Sub AssocierPlanBasePhasage()
    Dim unPlanPourPhasage As PlanFeuxBase
    Dim Index As Short
    Dim unePhase As Phase

    For Each unPlanPourPhasage In mesPlansPourPhasage()
      If unPlanPourPhasage.Equivalent(monPlanFeuxBase) Then
        Index = mesPlansPourPhasage.IndexOf(unPlanPourPhasage)
        For Each unePhase In monPlanFeuxBase.mPhases
          unPlanPourPhasage.mPhases.Déplacer(unPlanPourPhasage.mPhases.PhaseEquivalente(unePhase), monPlanFeuxBase.mPhases.IndexOf(unePhase))
        Next
        'Associer le Scénario au plan de phasage retenu
        'Cette fonction recalcule en particulier les durées mini '
        'pour prendre en compte les vrais temps de rouge de dégagement, qui ne peuvent plus désormais être modifés)
        monPlanFeuxBase.PlanPhasageAssocié = unPlanPourPhasage
        Me.cboDécoupagePhases.Text = "Phasage " & CStr(Index + 1)
        Exit For
      End If
    Next

  End Sub

  '*************************************************************************************
  'Verrouiller le panel organisation du phasage
  '*************************************************************************************
  Private Sub VerrouillerOrgaPhasage()

    If PhasageRetenu Then
      Me.btnActionPhase.Enabled = False
    Else
      Me.btnActionPhase.Enabled = True
    End If

  End Sub

  Private ReadOnly Property PhasageRetenu() As Boolean
    Get
      Return maVariante.PhasageRetenu
    End Get
  End Property

  '***************************************************************************************************
  'Réinitialisation du(des) phasage(s) suite à la modification des vitesses de dégagement : classe Paramétrage
  '***************************************************************************************************
  Public Sub RéinitialiserPhasages()
    maVariante.RéinitialiserPhasages()
    ChoisirOngletInitial()

  End Sub
#End Region
#Region " Plans de feux"
  Private flagKeyPress As Boolean
#Region "Ensemble Plans feux"
  '***********************************************************************************
  ' Retourne l'index du bouton radio sélectionné dans l'onglet plans de feux
  '***********************************************************************************
  Private Property pnlPlansFeuxIndex() As Short
    Get
      If Me.radPhasage.Checked Then
        Return 0
      ElseIf Me.radFeuBase.Checked Then
        Return 1
      ElseIf Me.radFeuFonctionnement.Checked Then
        Return 2
      Else
        Return -1
      End If
    End Get
    Set(ByVal Value As Short)
      Select Case Value
        Case 0
          Me.radPhasage.Checked = True
        Case 1
          Me.radFeuBase.Checked = True
        Case 2
          Me.radFeuFonctionnement.Checked = True
        Case -1
          Me.radPhasage.Checked = False
          Me.radFeuBase.Checked = False
          Me.radFeuFonctionnement.Checked = False
          monPlanPourPhasage = Nothing
      End Select
    End Set
  End Property

  '**********************************************************************************************************************
  'Changement d'item dans le Panel Plan de feux
  '**********************************************************************************************************************
  Private Sub radPlansDeFeux_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles radPhasage.CheckedChanged, radFeuBase.CheckedChanged, radFeuFonctionnement.CheckedChanged

    Try

      Select Case pnlPlansFeuxIndex
        Case 0
          'Organisation du phasage
          Me.pnlPhasage.BringToFront()
          Redessiner()
          If ChangementDeScénario Then
            InitPhasage()
          End If

        Case 1
          'Plans de feux de base
          If Not PhasageRetenu Then
            AfficherMessageErreur(Me, "Choisir d'abord l'organisation du phasage")
            radPhasage.Checked = True
          ElseIf monPlanFeuxBase.PhasageIncorrect Then
            AfficherMessageErreur(Me, "L'organisation du phasage est incorrecte")
            radPhasage.Checked = True
            Me.cboDécoupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)
          Else
            Me.pnlFeuBase.BringToFront()
            InitPlanFeuxBase()
          End If

          RedessinerDiagrammePlanFeux()

        Case 2

          'Plans de feux de fonctionnement
          If maVariante.VerrouFeuBase Then
            With monPlanFeuxBase
              If .mPlansFonctionnement.Count = 0 Then
                'Donner par défaut le nom du scénario
                .mPlansFonctionnement.Add(New PlanFeuxFonctionnement(monPlanFeuxBase, .Nom))
                .PlanFonctionnementCourant = .mPlansFonctionnement(.Nom)
                'Affecter par défaut la période de trafic ayant servi à construire le scénario
                .PlanFonctionnementCourant.Trafic = .Trafic
                'L'instruction qui suit est pour que le test juste après échoue
                monPlanFeuxFonctionnement = Nothing
              End If
              If .mPlansFonctionnement.Contains(monPlanFeuxFonctionnement) Then
                monPlanFeuxActif = monPlanFeuxFonctionnement
              Else
                InitPlansFeuxFonctionnement()
                Me.cboPlansDeFeux.Text = .PlanFonctionnementCourant.Nom
              End If
            End With

            Me.pnlFeuFonctionnement.BringToFront()
            Me.cboMéthodeCalculCycle.Enabled = monPlanFeuxBase.AvecTrafic

            RedessinerDiagrammePlanFeux()

          ElseIf Not PhasageRetenu Then
            AfficherMessageErreur(Me, "Choisir d'abord l'organisation du phasage")
            radPhasage.Checked = True
          ElseIf monPlanFeuxBase.PhasageIncorrect Then
            AfficherMessageErreur(Me, "L'organisation du phasage est incorrecte")
            radPhasage.Checked = True
            Me.cboDécoupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)

          Else
            AfficherMessageErreur(Me, "Verrouiller d'abord le plan de feux de base")
            radFeuBase.Checked = True
          End If

      End Select

      If CType(sender, RadioButton).Checked Then
        DéfinirSplitPosition()
        AfficherCacherDiagnostic()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub chkScénarioDéfinitif_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScénarioDéfinitif.CheckedChanged

    With maVariante

      If Me.chkScénarioDéfinitif.Checked Then
        Dim OK As Boolean = True
        If IsNothing(.ScénarioDéfinitif) Then
          '1ère fois que l'on choisit un scénario comme définitif
          OK = True
        ElseIf .ScénarioDéfinitif Is monPlanFeuxBase Then
          'Appel suite à Sélection du scénario définitif dans la liste des scénarios(SélectionnerScénario)
          OK = True
        Else
          'Demander confirmation du changement de scénario définitif
          OK = Confirmation("Vous avez déjà retenu le scénario " & .ScénarioDéfinitif.Nom & vbCrLf & "Souhaitez-vous en changer", Critique:=False)
        End If
        If OK Then
          .ScénarioDéfinitif = monPlanFeuxBase
        Else
          Me.chkScénarioDéfinitif.Checked = False
        End If

      Else
        If .ScénarioDéfinitif Is monPlanFeuxBase Then
          'Sinon, c'est qu'on conserve le précédent scénario définitif
          .ScénarioDéfinitif = Nothing
        End If
      End If
    End With

    AfficherProjetDéfinitif()

  End Sub
#End Region
#Region "Plans base et fonctionnement"
  '***********************************************************************************
  ' Mise à jour d'une phase suite à modif d'une autre en respectant le cycle total
  ' ==> Ces controles sont en ReadOnly, car les mini/maxi ne sont pas controlés
  '			dans la version 1.1 de VB.NET (AV : 27/01/04)
  '***********************************************************************************
  Private Sub updPhase_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
   Handles updPhase3Base.ValueChanged, updPhase2Base.ValueChanged, updPhase1Base.ValueChanged, _
            updPhase1Fct.ValueChanged, updPhase2Fct.ValueChanged, updPhase3Fct.ValueChanged

    Try
      Dim updPhase As NumericUpDown = sender
      Dim unePhase As Phase = updPhase.Tag

      If Not IsNothing(unePhase) Then
        Try

          ' Déterminer la phase suivante (en ignorant une éventuelle phase verrouillée)
          Dim updNext As NumericUpDown
          Dim PhaseSuivante As Phase = monPlanFeuxActif.mPhases.PhaseSuivante(unePhase)
          If PhaseSuivante.Verrouillée Then PhaseSuivante = monPlanFeuxActif.mPhases.PhaseSuivante(PhaseSuivante)

          updNext = updAssociéPhase(PhaseSuivante)

          'Par défaut, on incrémente ou décrémente la phase qui suit la phase modifiée
          'Si celle-ci est verrouillée on agit sur la suivante
          'Calcul de la nouvelle valeur à afficher dans le controle résultant
          Dim Différence As Short = updPhase.Value - unePhase.Durée
          Dim Résultat As Short
          Résultat = PhaseSuivante.Durée - Différence

          If Résultat < PhaseSuivante.DuréeIncompressible Then
            'Refuser la modification
            updPhase.Value -= Différence
          ElseIf Résultat > updNext.Maximum Then
            'Refuser la modification
            updPhase.Value += Différence
          Else
            'Mettre à jour les durées des phases
            unePhase.Durée = updPhase.Value
            PhaseSuivante.Durée = Résultat
            Modif = True
          End If

          'Afficher les nouvelles durées des phases : la durée du cycle n'est pas changée, il n'y a pas lieu de recalculer la capacité
          AfficherDuréesPhases(CapacitéARecalculer:=False)

        Catch ex As System.Exception

          AfficherMessageErreur(Me, ex)
        End Try

      Else
        '  Initialisation de la feuille ou mise à jour par un autre mécanisme
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  ' IndexPhase : retourne l'index de la phase correspondant à l'UpDown cliqué
  ' updPhase : UpDown cliqué
  '*******************************************************************************************************
  Private Overloads Function IndexPhase(ByVal updPhase As NumericUpDown) As Short
    'les noms des updown de phase commencent tous par updPhase : le numéro est en 1ère position
    'Retirer 1 pour obtenir l'index de la phase
    Return CType(Mid(updPhase.Name, 9, 1), Short) - 1
  End Function

  '*******************************************************************************************************
  ' IndexPhase : retourne l'index de la phase correspondant au bouton radio
  ' radPhase : bouton radio concerné
  '*******************************************************************************************************
  Private Overloads Function IndexPhase(ByVal radPhase As RadioButton) As Short
    'les noms des boutons radios de phase commencent tous par radPhase : le numéro est en 1ère position
    'Retirer 1 pour obtenir l'index de la phase
    Return CType(Mid(radPhase.Name, 9, 1), Short) - 1
  End Function

  '*******************************************************************************************************
  ' updAssociéPhase : retourne le UpDown correspondant à la phase
  ' unePhase : Phase pour laquelle on recherche le UpDown
  '*******************************************************************************************************
  Private Function updAssociéPhase(ByVal unePhase As Phase) As NumericUpDown
    Dim updPhase As NumericUpDown
    Dim unPlanFeux As PlanFeux = unePhase.mPlanFeux
    Dim PlanBase As Boolean = TypeOf unPlanFeux Is PlanFeuxBase

    Select Case unPlanFeux.mPhases.IndexOf(unePhase)
      Case 0
        If PlanBase Then
          updPhase = Me.updPhase1Base
        Else
          updPhase = Me.updPhase1Fct
        End If
      Case 1
        If PlanBase Then
          updPhase = Me.updPhase2Base
        Else
          updPhase = Me.updPhase2Fct
        End If
      Case 2
        If PlanBase Then
          updPhase = Me.updPhase3Base
        Else
          updPhase = Me.updPhase3Fct
        End If
    End Select

    Return updPhase

  End Function

  '*******************************************************************************************************
  ' radAssociéPhase : retourne le bouton radio correspondant à la phase
  ' unePhase : Phase pour laquelle on recherche le bouton radio
  '*******************************************************************************************************
  Private Function radAssociéPhase(ByVal unePhase As Phase) As RadioButton
    Dim radPhase As RadioButton
    Dim unPlanFeux As PlanFeux = unePhase.mPlanFeux
    Dim PlanBase As Boolean = TypeOf unPlanFeux Is PlanFeuxBase

    Select Case unPlanFeux.mPhases.IndexOf(unePhase)
      Case 0
        If PlanBase Then
          radPhase = Me.radPhase1Base
        Else
          radPhase = Me.radPhase1Fct
        End If
      Case 1
        If PlanBase Then
          radPhase = Me.radPhase2Base
        Else
          radPhase = Me.radPhase2Fct
        End If
      Case 2
        If PlanBase Then
          radPhase = Me.radPhase3Base
        Else
          radPhase = Me.radPhase3Fct
        End If
    End Select

    Return radPhase

  End Function

  Private Sub txtDuréeCycle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
  Handles txtDuréeCycleBase.KeyDown, txtDuréeCycleFct.KeyDown
    flagKeyPress = EstIncompatibleNumérique(e)
  End Sub

  Private Sub txtDuréeCycle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtDuréeCycleBase.KeyPress, txtDuréeCycleFct.KeyPress

    Dim txt As TextBox = sender

    If flagKeyPress Then
      'Touche refusée par l'évènement KeyDown
      e.Handled = True
      flagKeyPress = False
    Else
      e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=True)
    End If

  End Sub

  '************************************************************************************************
  ' Validation de la saisie de la durée du cycle
  '************************************************************************************************
  Private Sub txtDuréeCycle_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtDuréeCycleBase.Validating, txtDuréeCycleFct.Validating

    Dim txt As TextBox = sender
    Dim Durée As Short

    With monPlanFeuxActif
      e.Cancel = ControlerBornes(Me, .DuréeCycle(Minimum:=True), PlanFeux.maxiDuréeCycleAbsolue, txt, .DuréeCycle, unFormat:="#00")
    End With

    If Not e.Cancel Then
      Try
        Durée = CType(txt.Text, Short)
        If Durée > PlanFeux.maxiDuréeCycle Then
          MessageBox.Show("Une durée de cycle supérieure à " & PlanFeux.maxiDuréeCycle & "s est déconseillée")
        End If
        RedéfinirDuréesPhases(DuréeCycle:=Durée)

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  '************************************************************************************************
  ' Répartir sur les phases l'allongement ou le raccourcissement de la durée du cycle
  '************************************************************************************************
  Private Sub RedéfinirDuréesPhases(ByVal DuréeCycle As Short, Optional ByVal VerrouillageConservé As Boolean = False)
    Dim i As Short
    Dim unePhase As Phase

    With monPlanFeuxActif
      If IsNothing(.Trafic) Then
        'Répartition égale entre les phases du décalage demandé
        Dim Décalage As Short = DuréeCycle - .DuréeCycle

        'Déverrouiller l'éventuelle phase verrouillée (pour la fonction PhaseSuivante)
        For Each unePhase In .mPhases
          'Mémoriser la phase précédemment verrouillée
          If unePhase.Verrouillée Then i = .mPhases.IndexOf(unePhase) + 1
        Next
        .DéverrouillerPhases()

        If .mPhases.Count > 2 And TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
          'MODIF AV(13/02/06) : On ne déverrouille plus la phase figée
          ' C'est en particulier obligatoire si cette phase est une phase uniquement piétonne
          For Each unePhase In .mPhases
            If unePhase.EstSeulementPiéton Then
              VerrouillerPhase(.mPhases.IndexOf(unePhase))
              Exit For
            End If
          Next
        End If

        unePhase = .mPhases(CType(0, Short))

        If Décalage <> 0 Then
          Modif = True
        End If

        Do While Décalage <> 0
          If Décalage > 0 Then
            unePhase.Durée += 1
            Décalage -= 1
          Else
            If unePhase.Durée > unePhase.DuréeIncompressible Then
              unePhase.Durée -= 1
              Décalage += 1
            End If
          End If
          unePhase = .mPhases.PhaseSuivante(unePhase)
        Loop

        If i > 0 Then VerrouillerPhase(i - 1) ' .mPhases(CType(i - 1, Short)).Verrouillée = True

      Else
        'Avec trafic 
        'Répartition entre les phases du décalage demandé en fonction du trafic supporté par chaque phase
        .RépartirDuréeCycle(DuréeCycle)

        If TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
          RecalculerCapacité()
        End If
      End If


    End With

    'Mettre à jour les controles upDown en conformité avec les nouvelles durées
    AfficherDuréesPhases(CapacitéARecalculer:=True)

  End Sub

  '*******************************************************************************************************
  ' Changement de la phase verrouillée
  ' radPhase : bouton radio déterminant la phase à verrouiller
  '*******************************************************************************************************
  Private Sub radPhase_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles radPhase1Base.CheckedChanged, radPhase2Base.CheckedChanged, radPhase3Base.CheckedChanged, radPhase1Fct.CheckedChanged, radPhase2Fct.CheckedChanged, radPhase3Fct.CheckedChanged
    Dim radPhase As RadioButton = sender

    If Not IsNothing(monPlanFeuxActif) AndAlso radPhase.Checked Then

      VerrouillerPhase(IndexPhase(radPhase))
    End If

  End Sub

  '*******************************************************************************************************
  ' Verrrouiller une phase
  ' Index : Index de la phase dans la collection de phases du plan de feux
  '*******************************************************************************************************
  Private Sub VerrouillerPhase(ByVal Index As Short)

    Dim unePhase As Phase
    Dim upd As NumericUpDown

    With monPlanFeuxActif
      'Déverrouille si nécessaires la phase verrouillée
      .DéverrouillerPhases()
      For Each unePhase In .mPhases
        If unePhase Is .mPhases(Index) Then
          'Verrouiller la phase
          radAssociéPhase(unePhase).Checked = True
          updAssociéPhase(unePhase).Enabled = False
          unePhase.Verrouillée = True
        Else
          'DéVerrouiller la phase
          updAssociéPhase(unePhase).Enabled = True
          unePhase.Verrouillée = False
        End If
      Next

    End With

  End Sub

  '******************************************************************************
  ' Sélection d'une nouvelle ligne dans le tableau des durées de vert
  '******************************************************************************
  Private Sub lvwDuréeVert_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles lvwDuréeVert.SelectedIndexChanged, lvwDuréeVertFct.SelectedIndexChanged

    Try
      Dim lvw As ListView = sender
      Dim updOuverture As NumericUpDown = IIf(lvw Is Me.lvwDuréeVert, Me.updDécalageOuvertureBase, Me.updDécalageOuvertureFct)
      Dim updFermeture As NumericUpDown = IIf(lvw Is Me.lvwDuréeVert, Me.updDécalageFermetureBase, Me.updDécalageFermetureFct)
      Dim unPlanFeux As PlanFeux = updOuverture.Tag
      Dim desPhases As PhaseCollection = unPlanFeux.mPhases

      Dim itmX As ListViewItem = ItemDuréevertSélectionné(lvw)


      If IsNothing(itmX) Then
        updOuverture.Visible = False
        updFermeture.Visible = False
      Else
        Dim uneLigneFeux As LigneFeux = itmX.Tag
        Dim unePhase, maPhase As Phase
        Try

          For Each unePhase In unPlanFeux.mPhases
            If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
              maPhase = unePhase
            End If
          Next

          Dim VertMini As Short = IIf(uneLigneFeux.EstPiéton, maVariante.VertMiniPiétons, maVariante.VertMiniVéhicules)
          Dim Maximum As Short = unPlanFeux.DuréeVertMaxi(uneLigneFeux) - VertMini
          Dim DécalOuvre As Short = unPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Ouverture)
          Dim DécalFerme As Short = unPlanFeux.DécalageOuvreFerme(uneLigneFeux, PlanFeux.Décalage.Fermeture)
          updOuverture.Maximum = Maximum - DécalFerme
          updFermeture.Maximum = Maximum - DécalOuvre

          updOuverture.Value = DécalOuvre
          updFermeture.Value = DécalFerme
          updOuverture.Visible = True
          updFermeture.Visible = True
          updOuverture.Enabled = uneLigneFeux.DécalageOuvertureAutorisé
        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        End Try
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  ' Diminution/augmentation des décalages à l'ouverture ou à la fermeture de la ligne de feux sélectionnée
  '*******************************************************************************************************
  Private Sub updDécalageOuvertureFermeture_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles updDécalageFermetureFct.ValueChanged, updDécalageOuvertureFct.ValueChanged, updDécalageFermetureBase.ValueChanged, updDécalageOuvertureBase.ValueChanged

    Dim upd As NumericUpDown = sender

    If Not IsNothing(upd.Tag) Then
      Try
        RedéfinirDécalage(upd)
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If

  End Sub

  Private Sub RedéfinirDécalage(ByVal upd As NumericUpDown)
    Dim updAutre As NumericUpDown

    Try

      'Déterminer la ligne de feux sélectionnée dans le tableau des durées de vert du plan de feux
      Dim unPlanFeux As PlanFeux = upd.Tag
      'Sinon l'évènement ValueChanged est appelé abusivement  (si l'appel vient de Validating)
      upd.Tag = Nothing

      Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDuréeVert, Me.lvwDuréeVertFct)
      Dim itmX As ListViewItem = ItemDuréevertSélectionné(lvw)

      If Not IsNothing(itmX) Then
        Dim uneLigneFeux As LigneFeux = itmX.Tag
        Dim Index As PlanFeux.Décalage = IIf(upd Is Me.updDécalageOuvertureBase Or upd Is Me.updDécalageOuvertureFct, PlanFeux.Décalage.Ouverture, PlanFeux.Décalage.Fermeture)
        Dim VertMini As Short = IIf(uneLigneFeux.EstPiéton, maVariante.VertMiniPiétons, maVariante.VertMiniVéhicules)
        Dim Différence As Short

        'Rechercher la phase concernée par la ligne de feux et mémoriser de combien le décalage va varier
        Dim desPhases As PhaseCollection = unPlanFeux.mPhases

        'Mettre à jour la valeur du décalage
        Différence = upd.Value - unPlanFeux.DécalageOuvreFerme(uneLigneFeux, Index)
        If Différence <> 0 Then Modif = True
        unPlanFeux.DécalageOuvreFerme(uneLigneFeux, Index) = upd.Value

        'Mettre à jour la ligne de feux dans le tableau
        itmX.SubItems(Index + 3).Text = upd.Value
        AfficherDuréeVert(unPlanFeux, uneLigneFeux)

        'Redéfinir le maximum acceptable pour l'autre UpDown
        If TypeOf unPlanFeux Is PlanFeuxBase Then
          If upd Is Me.updDécalageOuvertureBase Then
            updAutre = Me.updDécalageFermetureBase
          Else
            updAutre = Me.updDécalageOuvertureBase
          End If
        Else
          If upd Is Me.updDécalageOuvertureFct Then
            updAutre = Me.updDécalageFermetureFct
          Else
            updAutre = Me.updDécalageOuvertureFct
          End If
          RecalculerCapacité()
        End If
        updAutre.Maximum -= Différence

        upd.Tag = unPlanFeux

        RedessinerDiagrammePlanFeux()
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "RedéfinirDécalage")
    End Try

  End Sub

  Private Sub updDécalageOuvertureFermeture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles updDécalageFermetureBase.Validating, updDécalageOuvertureBase.Validating, updDécalageFermetureFct.Validating, updDécalageOuvertureFct.Validating
    Dim Donnée As Short


    Try
      Dim updPF As NumericUpDown = sender

      'Déterminer la ligne de feux sélectionnée dans le tableau des durées de vert du plan de feux
      Dim unPlanFeux As PlanFeux = updPF.Tag
      Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDuréeVert, Me.lvwDuréeVertFct)
      Dim itmX As ListViewItem = ItemDuréevertSélectionné(lvw)
      Dim uneLigneFeux As LigneFeux = itmX.Tag

      'Rechercher la phase concernée par la ligne de feux et mémoriser de combien le décalage va varier
      Dim desPhases As PhaseCollection = unPlanFeux.mPhases

      Dim Index As PlanFeux.Décalage = IIf(updPF Is Me.updDécalageOuvertureBase Or updPF Is Me.updDécalageOuvertureFct, PlanFeux.Décalage.Ouverture, PlanFeux.Décalage.Fermeture)

      Donnée = unPlanFeux.DécalageOuvreFerme(uneLigneFeux, Index)

      If Donnée <> updPF.Value Then
        e.Cancel = ControlerBornes(Me, updPF.Minimum, updPF.Maximum, updPF, CType(Donnée, String))

        If Not e.Cancel Then
          RedéfinirDécalage(updPF)
        End If
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '***********************************************************************************
  ' Afficher les durées de chaque phase et les durées de vert 
  '***********************************************************************************
  Private Sub AfficherDuréesPhases(ByVal CapacitéARecalculer As Boolean)
    Dim unePhase As Phase
    Dim desPhases As PhaseCollection = monPlanFeuxActif.mPhases
    Dim uneLigneFeux As LigneFeux

    Dim upd As NumericUpDown

    Try

      'Mettre à jour les durées de phases
      For Each unePhase In desPhases
        upd = updAssociéPhase(unePhase)
        'Désactiver le tag pour que l'évènement updPhase_ValueChanged ne fasse rien
        upd.Tag = Nothing
        upd.Value = unePhase.Durée
        upd.Tag = unePhase
        'upd.Enabled = True remplacé par ceci (MODIF AV : 18/09/06  - Surveiller les régressions)
        upd.Enabled = Not unePhase.Verrouillée

        'Afficher les durées de vert
        For Each uneLigneFeux In mesLignesFeux
          If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
            Try
              AfficherDuréeVert(monPlanFeuxActif, uneLigneFeux)

            Catch ex As DiagFeux.Exception
              Throw New DiagFeux.Exception(ex.Message)
            Catch ex As System.Exception
              LancerDiagfeuxException(ex, "Affichage des durées des phases")
            End Try
          End If
        Next
      Next

      RedessinerDiagrammePlanFeux()

      If TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
        If CapacitéARecalculer Then
          DéterminerAfficherCapacité()
        Else
          AfficherInfosAttente()
        End If
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherDuréesPhases")
    End Try

  End Sub


  '*************************************************************************************************
  ' AfficherDuréeVert : Affiche la durée de vert d'une ligne de feux
  ' uneLigneFeux : ligne de feux concernée
  '*************************************************************************************************
  Private Sub AfficherDuréeVert(ByVal unPlanFeux As PlanFeux, ByVal uneLigneFeux As LigneFeux)
    Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDuréeVert, Me.lvwDuréeVertFct)

    Try
      With lvw
        'il faut recalculer le vert de la ligne de feux(fonction des durées de phases et des décalages)
        .Items(mLignesFeux.IndexOf(uneLigneFeux)).SubItems(2).Text = unPlanFeux.DuréeVert(uneLigneFeux)
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Affichage de la durée de vert de la ligne " & uneLigneFeux.ID)
    End Try

  End Sub

  '*************************************************************************************************
  ' ItemDuréevertSélectionné:  ligne sélectionnée dans le tableau des durées de vert du plan de feux
  '*************************************************************************************************
  Private Function ItemDuréevertSélectionné(ByVal lvw As ListView) As ListViewItem
    Dim lstItems As ListView.ListViewItemCollection = lvw.Items

    If lvw.SelectedItems.Count > 0 Then Return lstItems(lvw.SelectedIndices(0))

  End Function
#End Region
#Region "Plans base"
  '************************************************************************************************
  ' Frappe d'une touche dans les textbox Vert Mini Véhicules ou piétons(plan de feux de base)
  ' Interdit la frappe d'une touche non numérique
  '************************************************************************************************
  Private Sub txtVertMini_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    flagKeyPress = EstIncompatibleNumérique(e)
  End Sub

  Private Sub txtVertMini_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    If flagKeyPress Then
      'Touche refusée par l'évènement KeyDown
      e.Handled = True
      flagKeyPress = False
    End If
  End Sub

  '************************************************************************************************
  ' Validation des textbox Vert Mini Véhicules ou piétons(plan de feux de base)
  '************************************************************************************************
  Private Sub txtVertMini_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtVertMiniPiéton.Validating, txtVertMiniVéhicule.Validating

    Dim chaine As String
    Dim txt As TextBox = sender
    Dim Véhicule As Boolean = txt Is txtVertMiniVéhicule
    'Déterminer le vert mini à controler
    Dim VertMiniAbsolu As Short = IIf(Véhicule, [Global].VertMiniVéhicules, [Global].VertMiniPiétons)
    Dim VertMiniActuel As Short = IIf(Véhicule, monPlanFeuxBase.VertMiniVéhicules, monPlanFeuxBase.VertMiniPiétons)

    Try
      chaine = txt.Text
      If chaine.Length = 0 Then
        e.Cancel = True
      Else
        e.Cancel = ControlerBornes(Me, VertMiniAbsolu, [Global].VertMiniMaximum, txt, VertMiniActuel, unFormat:="#0")
      End If

      If Not e.Cancel Then
        Dim Réinitialiser As Boolean
        'Mettre à jour le nouveau mini de vert pour la variante considérée
        If Véhicule Then
          'le test qui suit est en principe superflu, mais il s'avère que 
          'l'évènement est déclenché anormalement (décalageouverture_lostfocus (?)) ce qui relance InitPlanFeuxBase
          If monPlanFeuxBase.VertMiniVéhicules <> CInt(chaine) Then
            Réinitialiser = True
            monPlanFeuxBase.VertMiniVéhicules = CInt(chaine)
          End If
        Else
          If monPlanFeuxBase.VertMiniPiétons <> CInt(chaine) Then
            Réinitialiser = True
            monPlanFeuxBase.VertMiniPiétons = CInt(chaine)
          End If
        End If

        If Réinitialiser Then
          'Recalculer et afficher le nouveau plan de feux de sécurité
          InitPlanFeuxBase(RecalculerMini:=True)
        End If


      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub btnLigneFeuDescendrePlans_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
Handles btnLigneFeuDescendrePlans.Click, btnLigneFeuMonterPlans.Click
    Dim Décalage As Short, Position As Short

    Try

      'Déterminer le sens du décalage
      If sender Is btnLigneFeuDescendrePlans Then
        DécalerLigneFeux(+1, lvwDuréeVert)
      Else
        DécalerLigneFeux(-1, lvwDuréeVert)
      End If
      '      Me.cboTriLignesFeuxPlans.SelectedIndex = 0

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try


  End Sub

  Private Sub DécalerLigneFeux(ByVal Décalage As Short, ByVal lvw As ListView)
    Dim Index As Short

    Try

      Dim itmX As ListViewItem = ItemDuréevertSélectionné(lvw)

      If Not IsNothing(itmX) Then
        Dim uneLigneFeux As LigneFeux = itmX.Tag
        Index = mLignesFeux.IndexOf(uneLigneFeux)
        Dim Continuer As Boolean = (Index > 0 And Décalage = -1) Or (Index < mLignesFeux.Count - 1 And Décalage = 1)

        If Continuer Then
          'Décaler la ligne de feux
          mLignesFeux.Décaler(Décalage, uneLigneFeux)
          AfficherConséquencesModifLignesDeFeuxPlans()
          For Each itmX In lvw.Items
            If itmX.Tag Is uneLigneFeux Then
              itmX.Selected = True
            End If
          Next
        End If
      End If


    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DécalerLigneFeux")
    End Try

  End Sub

  Private Sub cboTriLignesFeuxPlans_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTriLignesFeuxPlans.SelectedIndexChanged
    Dim Ordre As LigneFeuxCollection.OrdreDeTriEnum

    If cboTriLignesFeux.SelectedIndex <> 0 Then
      monPlanFeuxBase.mLignesFeux.Trier(Ordre:=cboTriLignesFeuxPlans.SelectedIndex)
      AfficherConséquencesModifLignesDeFeuxPlans()
    End If

  End Sub

  Private Sub AfficherConséquencesModifLignesDeFeuxPlans()
    AfficherPlanFeux()
    RedessinerDiagrammePlanFeux()
    If Me.cboPlansDeFeux.SelectedIndex <> -1 Then
      AfficherPlanFeux(monPlanFeuxBase.mPlansFonctionnement(Me.cboPlansDeFeux.Text))
    End If
  End Sub
#End Region
#Region "Plans fonctionnement"
  '***********************************************************************************
  ' Initialiser la liste des plans de feux de fonctionnement
  '***********************************************************************************
  Private Sub InitPlansFeuxFonctionnement()
    Dim unPlanFeux As PlanFeuxFonctionnement

    'Créer la liste des plans de feux de fonctionnement
    With cboPlansDeFeux
      .Items.Clear()
      For Each unPlanFeux In monPlanFeuxBase.mPlansFonctionnement
        .Items.Add(unPlanFeux.Nom)
      Next
    End With

    With monPlanFeuxBase
      If IsNothing(.PlanFonctionnementCourant) Then
        '1er appel de l'onglet PFF pour ce plan de base : prendre le 1er PFF
        Me.cboPlansDeFeux.SelectedIndex = 0
      Else
        'Sélectionner le dernier PFF qui était sélectionné lorsque ce plan de base était actif
        Me.cboPlansDeFeux.Text = .PlanFonctionnementCourant.Nom
      End If
    End With

  End Sub
  '******************************************************************************
  ' Créer un nouveau plan de Feux de fonctionnement
  '******************************************************************************
  Private Sub btnDupliquerPlanFeux_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDupliquerPlanFeux.Click
    Dim dlgNouveauPlan As New dlgNouveauPlanFeux
    Dim unPlanFct As PlanFeuxFonctionnement
    Dim unPlan As PlanFeuxFonctionnement
    Dim NomPlan As String
    Dim unTrafic As Trafic
    Dim Index As Short

    With dlgNouveauPlan
      'Lister les plans de feux de fonctionnement
      For Each unPlanFct In monPlanFeuxBase.mPlansFonctionnement
        .cboPlansDeFeux.Items.Add(unPlanFct.Nom)
      Next
      'Proposer le plan de feu de base par défaut
      .cboPlansDeFeux.SelectedIndex = 0

      'Lister les trafics
      For Each unTrafic In mesTrafics
        If unTrafic.Verrouillé Then
          Index = .cboTrafic.Items.Add(unTrafic.Nom)
          'Proposer par défaut le trafic de du scénario en cours
          If unTrafic Is monPlanFeuxBase.Trafic Then
            .cboTrafic.SelectedIndex = Index
          End If
        End If
      Next
      If Not monPlanFeuxBase.AvecTrafic Then
        'Par défaut, proposer <Aucun trafic> , si le scénario en cours est sans trafic
        .cboTrafic.SelectedIndex = 0
      End If

      'Saisir les informations du nouveau plan de feux de fonctionnement
      .ShowDialog(Me)

      If .DialogResult = DialogResult.OK Then
        'Créer le plan de feux
        NomPlan = .txtNomPlan.Text
        If monPlanFeuxBase.mPlansFonctionnement.Contains(NomPlan) Then
          AfficherMessageErreur(Me, "Le plan " & NomPlan & " existe déjà")

        Else
          Modif = True

          If .cboPlansDeFeux.SelectedIndex = 0 Then
            'Partir du plan de feux de base
            unPlan = New PlanFeuxFonctionnement(monPlanFeuxBase, NomPlan)
          Else
            'Partir d'un plan de feux de fonctionnement existant
            unPlan = New PlanFeuxFonctionnement(monPlanFeuxBase.mPlansFonctionnement(CType(.cboPlansDeFeux.SelectedIndex - 1, Short)), NomPlan)
          End If

          'Afficher le trafic (éventuel) correspondant au plan de feux
          If .cboTrafic.SelectedIndex > 0 Then
            unPlan.Trafic = mesTrafics(.cboTrafic.Text)
          End If

          'Ajouter le plan à la combo
          Me.cboPlansDeFeux.Items.Add(unPlan.Nom)
          'Sélectionner ce plan comme plan à afficher
          Me.cboPlansDeFeux.Text = unPlan.Nom

        End If
      End If

      .Dispose()
    End With

  End Sub

  Private Sub btnRenommerPlanFeux_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenommerPlanFeux.Click
    Dim Index As Short = Me.cboPlansDeFeux.SelectedIndex
    Dim Réponse As String = InputBox("Renommer le plan de feux en : ")

    If Réponse.Length > 0 Then
      monPlanFeuxFonctionnement.Nom = Réponse
      'Mettre à jour la combode la liste des plans de feux
      Me.cboPlansDeFeux.Items.RemoveAt(Index)
      Me.cboPlansDeFeux.Items.Insert(Index, Réponse)
      Me.cboPlansDeFeux.SelectedIndex = Index

      Modif = True
    End If

  End Sub

  '******************************************************************************
  ' Supprimer un plan de Feux de fonctionnement
  '******************************************************************************
  Private Sub btnSupprimerPlanFeux_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSupprimerPlanFeux.Click
    If Confirmation("Supprimer le plan de feux ?", Critique:=True, Controle:=Me) Then
      Dim Index As Short = Me.cboPlansDeFeux.SelectedIndex
      'Supprimer le plan de feux
      monPlanFeuxBase.mPlansFonctionnement.RemoveAt(Index)
      'le retirer de la liste
      Me.cboPlansDeFeux.Items.RemoveAt(Index)
      'Se repositionner systématiquement sur le 1er de la liste (il existe toujours car on n'a pas le droit de supprimer tous les plans de feux)
      Me.cboPlansDeFeux.SelectedIndex = 0

      Modif = True
    End If
  End Sub

  '******************************************************************************
  ' Gérer l'activation des boutons du panel Feux de fonctionnement
  '******************************************************************************
  Private Sub ActiverBoutonsPlansDeFeux()

    Me.btnSupprimerPlanFeux.Enabled = Me.cboPlansDeFeux.Items.Count > 1
    Me.btnRenommerPlanFeux.Enabled = Me.cboPlansDeFeux.Items.Count > 0
    Me.btnDiagnostic.Enabled = monPlanFeuxActif.AvecTrafic

  End Sub

  Private Sub btnCalculerCycle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalculerCycle.Click
    'Calculer la durée du cycle
    Dim Méthode As MéthodeCalculCycle = Me.cboMéthodeCalculCycle.SelectedIndex
    Dim DuréeCycle As Short
    Dim Message As String

    Try

      If Méthode = MéthodeCalculCycle.Webster Then
        DuréeCycle = monPlanFeuxActif.CalculCycle(Message)

      Else
        'Méthode Classique
        Dim RéserveCapacitéAdmise As Single
        Select Case Me.cboRéserveCapacitéChoisie.SelectedIndex
          Case 0
          Case 1
            RéserveCapacitéAdmise = 0.1
          Case 2
            RéserveCapacitéAdmise = 0.15
          Case 3
            RéserveCapacitéAdmise = 0.2
        End Select
        DuréeCycle = monPlanFeuxActif.CalculCycle(Message, CoefDemande:=RéserveCapacitéAdmise)
      End If

      If DuréeCycle = 0 Then
        AfficherMessageErreur(Me, Message)

      ElseIf DuréeCycle <> monPlanFeuxActif.DuréeCycle Then
        If DuréeCycle > PlanFeux.maxiDuréeCycle Then
          AfficherMessageErreur(Me, "Durée de cycle importante : " & DuréeCycle & " s")
        End If
        RedéfinirDuréesPhases(DuréeCycle:=CType(DuréeCycle, Short))
        Me.txtDuréeCycleFct.Text = DuréeCycle
      End If

      Me.btnCalculerCycle.Enabled = False
      Me.cboRéserveCapacitéChoisie.Enabled = False

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '***************************************************************************************
  ' Choix d'un nouveau plan de feux de fonctionnement
  '***************************************************************************************
  Private Sub cboPlansDeFeux_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles cboPlansDeFeux.SelectedIndexChanged

    Try
      monPlanFeuxFonctionnement = monPlanFeuxBase.mPlansFonctionnement(CType(cboPlansDeFeux.SelectedIndex, Short))
      monPlanFeuxBase.PlanFonctionnementCourant = monPlanFeuxFonctionnement
      monPlanFeuxActif = monPlanFeuxFonctionnement

      AfficherPlanFeux()

      'DIAGFEUX v1
      'If monPlanFeuxBase.AvecTrafic Then
      '  FenetreDiagnostic.AffecterPlanFeux(monPlanFeuxActif)
      '  DéterminerAfficherCapacité()
      'End If

      'Afficher le trafic (éventuel) correspondant au plan de feux
      If monPlanFeuxFonctionnement.AvecTrafic Then
        If Me.cboTraficFct.Text = monPlanFeuxFonctionnement.Trafic.Nom Then
          cboTraficFct_SelectedIndexChanged(cboTraficFct, New System.EventArgs)
        Else
          Me.cboTraficFct.Text = monPlanFeuxFonctionnement.Trafic.Nom
        End If

      Else
        'Trafic = <Aucun>
        Me.cboTraficFct.SelectedIndex = 0
      End If

      RedessinerDiagrammePlanFeux()

      'Par défaut, on propose une méthode manuelle
      Me.cboMéthodeCalculCycle.SelectedIndex = 0
      Me.btnCalculerCycle.Enabled = False

      ActiverBoutonsPlansDeFeux()

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  '***************************************************************************************
  ' Choix d'un trafic pour le plan de feux de fonctionnement
  '***************************************************************************************
  Private Sub cboTraficFct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTraficFct.SelectedIndexChanged
    'Mémoriser le précédent trafic du plan de feux
    Dim unTrafic As Trafic = monPlanFeuxActif.Trafic
    Dim newTrafic As Trafic
    Static Passage As Boolean
    Dim DiagnosticàAfficher As Boolean

    If Passage Then
      Passage = False
    Else

      Try

        With cboTraficFct
          If .SelectedIndex = 0 Then
            monPlanFeuxActif.Trafic = Nothing
            DiagnosticàAfficher = True

          Else
            newTrafic = maVariante.mTrafics(.Text)
            'Ne pas accepter un trafic non verrouillé (il peut en particulier être vide)
            If Not newTrafic.Verrouillé Then
              MessageBox.Show("Cette période de trafic n'est pas verrouillée" & vbCrLf & "Elle ne peut pas être choisie pour un plan de feux")
              Passage = True
              If monPlanFeuxActif.AvecTrafic Then
                Me.cboTraficFct.Text = unTrafic.Nom
              Else
                Me.cboTraficFct.SelectedIndex = 0
              End If
              Exit Sub
            End If

            If Not newTrafic Is monPlanFeuxActif.Trafic Then
              DiagnosticàAfficher = True
              monPlanFeuxActif.Trafic = newTrafic
            End If
            'Bien que la demande ait pu être déjà calculée pour un autre plan de feux avec le même trafic
            ' il est + simple de la recalculer
            'Mis en commentaire (AV :18/06/07) - Inutile dans Diagfeux (nouveaux drapeaux dans l'objet PlanFeux)
            'monPlanFeuxActif.CalculerDemande()
          End If

          FenetreDiagnostic.AffecterPlanFeux(monPlanFeuxActif)
          ' Afficher les données de capacité si une période de trafic est associée
          DéterminerAfficherCapacité()

          If DiagnosticàAfficher Then
            ' Test rajouté (DIAGFEUX 2  : 10/07/07) pour limiter les réaffichages du diagnostic
            ' On affiche automatiquement celle-ci :
            ' Bouton Calculer
            ' Changement de cycle
            ' ici : uniquement si l'appel de la fonction est du à un changement de trafic du plan de feux courant, 
            ' et non indirectement suite à la sélection d'un autre plan de feux de fonctionnement
            AfficherDiagnostic(PourCacher:=IsNothing(monPlanFeuxActif.Trafic))
          End If

          Me.cboMéthodeCalculCycle.Enabled = Not IsNothing(monPlanFeuxActif.Trafic)
          'Par défaut, on propose une méthode manuelle
          Me.cboMéthodeCalculCycle.SelectedIndex = 0

          ActiverBoutonsPlansDeFeux()
        End With

        If Not ChargementEnCours AndAlso Not unTrafic Is monPlanFeuxActif.Trafic Then
          Modif = True
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  Private Sub cboMéthodeCalculCycle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMéthodeCalculCycle.SelectedIndexChanged
    Dim Méthode As MéthodeCalculCycle = Me.cboMéthodeCalculCycle.SelectedIndex
    Select Case Méthode
      Case MéthodeCalculCycle.Manuel
        'Pas de calcul
        Me.btnCalculerCycle.Enabled = False
        Me.cboRéserveCapacitéChoisie.Enabled = False
      Case MéthodeCalculCycle.Webster
        'Calcul possible sans utiliser la réserve de capacité
        Me.btnCalculerCycle.Enabled = True
        Me.cboRéserveCapacitéChoisie.Enabled = False
      Case MéthodeCalculCycle.Classique
        'Calcul possible dès qu'on aura choisi une réserve de capacité
        Me.btnCalculerCycle.Enabled = False
        Me.cboRéserveCapacitéChoisie.Enabled = True
    End Select

    'Remettre à blanc la réserve de capacité: soit elle ne sert à rien(2 1ers cas), soit on veut que l'utilisteur la choisisse volontairement
    Me.cboRéserveCapacitéChoisie.SelectedIndex = -1

  End Sub

  Private Sub cboRéserveCapacitéChoisie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles cboRéserveCapacitéChoisie.SelectedIndexChanged

    Dim Méthode As MéthodeCalculCycle = Me.cboMéthodeCalculCycle.SelectedIndex
    Me.btnCalculerCycle.Enabled = cboRéserveCapacitéChoisie.SelectedIndex <> -1 Or Méthode = MéthodeCalculCycle.Webster
  End Sub

  '**************************************************************************************************************
  'Recalcul du diagnostic suite à la modification du débit de saturation ou du temps perdu : classe Paramétrage
  '***************************************************************************************************************
  Private Sub RecalculerCapacité()
    monPlanFeuxActif.CapacitéACalculer = True
    DéterminerAfficherCapacité()
    AfficherDiagnostic()
  End Sub
  '**********************************************************************************************************************
  ' Afficher la capacité du plan de feux et les infos qui en découlent
  '**********************************************************************************************************************
  Private Sub DéterminerAfficherCapacité(Optional ByVal unPlanFeux As PlanFeux = Nothing)

    If IsNothing(unPlanFeux) Then unPlanFeux = monPlanFeuxActif

    With unPlanFeux

      Try
        If TypeOf unPlanFeux Is PlanFeuxBase Then
          If unPlanFeux.AvecTrafic Then
            ' Affichage dans le volet Organisation du phasage

            .CalculerRéserveCapacité()
            Me.txtRéserveCapacitéPourCent.Text = .strRéserveCapacitéPourCent
          End If

        Else

          If .AvecTrafic Then
            If .CapacitéACalculer Then
              .CalculerRéserveCapacité()
            End If
            FenetreDiagnostic.AfficherCapacité()
          Else
            AfficherDiagnostic(PourCacher:=True)
          End If
        End If

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)

      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "DéterminerAfficherCapacité")
      End Try

    End With    'unPlanFeux

  End Sub

  Public Sub RecalculerCapacités()

    maVariante.RéinitialiserCapacités()

    If Not IsNothing(monPlanFeuxFonctionnement) Then
      ' Afficher les données de capacité si une période de trafic est associée
      DéterminerAfficherCapacité(monPlanFeuxFonctionnement)
    End If
  End Sub
#Region " Diagnostic"
  Private Sub btnDiagnostic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiagnostic.Click
    AfficherDiagnostic()
  End Sub

  Private Sub AfficherDiagnostic(Optional ByVal PourCacher As Boolean = False)
    If PourCacher Then
      FenetreDiagnostic.Hide()
    Else
      FenetreDiagnostic.Show()
    End If

    FenetreDiagnostic.EnVeille = Not PourCacher
  End Sub

  Private Sub AfficherCacherDiagnostic()
    If Not (Me.tabOnglet.SelectedTab Is Me.tabPlansDeFeux AndAlso Me.radFeuFonctionnement.Checked) Then
      Me.FenetreDiagnostic.Hide()
    ElseIf FenetreDiagnostic.EnVeille Then
      AfficherDiagnostic()
    End If
  End Sub

  Private Sub AfficherInfosAttente()
    FenetreDiagnostic.AfficherInfosAttente()
  End Sub
#End Region
#End Region
#End Region

#End Region
#Region " Boutons généraux"
  Private Sub btnCarrefour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarrefour.Click
    Dim dlg As New dlgCarGen
    Dim ctrl As Control

    With dlg
      .cboCarrefourType.Enabled = False
      .radDégradé.Enabled = False
      .radGraphique.Enabled = False
      .mParamDessin = Me.mParamDessin
      If maVariante.VerrouGéom Then
        For Each ctrl In .grpModalités.Controls
          ctrl.Enabled = False
        Next
        If maVariante.VerrouMatrices Then
          .radEnAgglo.Enabled = False
          .radHorsAgglo.Enabled = False
        End If
      ElseIf Not IsNothing(maVariante.NomFichier) Then
        For Each ctrl In .pnlMode.Controls
          ctrl.Enabled = False
        Next
      End If

      .mVariante = maVariante
      If .ShowDialog(Me) = DialogResult.OK Then
        'Mettre à jour les données du carrefour avec les données saisies dans la boite
        .MettreAjour()
        Text = .mVariante.Libellé
        RedessinerFondDePlan()
        AfficherContexteFDP()
      End If

      .Dispose()
    End With

    '    RedessinerFondDePlan()

  End Sub

  Private Sub RedessinerFondDePlan()
    Dim ParamDessinModifié As Boolean

    If Not IsNothing(monFDP) AndAlso IsNothing(maVariante.mFondDePlan) Then
      monFDP = Nothing
      maVariante.CréerGraphique(colObjetsGraphiques)
      Redessiner()
    Else
      monFDP = maVariante.mFondDePlan
    End If

    If Not IsNothing(monFDP) AndAlso monFDP.ADessiner Then
      Dim p As Point = PointDessin(maVariante.mCarrefour.mCentre)
      If Not PointDansPicture(p) Then
        Dim pMouseUp As Point = New Point(picDessin.Width / 2, picDessin.Height / 2)
        pMouseUp = Point.op_Subtraction(p, Point.op_Explicit(pMouseUp))
        mParamDessin = DéterminerNewOrigineRéellePAN(pMouseUp)
        cndParamDessin = mParamDessin
        ParamDessinModifié = True
      ElseIf Not mParamDessin.Equals(cndParamDessin) Then
        mParamDessin = cndParamDessin
        ParamDessinModifié = True
      End If

      If ParamDessinModifié Then
        mEchelles.Clear()
        mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
      End If

      PositionnerCarrefour()
    End If

  End Sub

#End Region
#Region " Grilles"
  '**********************************************************************************************************************
  ' Indique si le style est interdit à la saisie
  '**********************************************************************************************************************
  Private Function StyleInterdit(ByVal unStyle As Grille.CellStyle) As Boolean
    Select Case unStyle.Name
      Case "Grisé", "GriséGras", "GriséBooléen", "Rouge", "Vert"
        StyleInterdit = True
    End Select
  End Function

#Region " Grille Branches"
  '******************************************************************************
  ' Valider les données d'une cellule de la grille Branches
  '******************************************************************************
  Private Sub AC1grilleBranche_ValidateEdit(ByVal sender As System.Object, ByVal e As Grille.ValidateEditEventArgs) _
  Handles AC1GrilleBranches.ValidateEdit

    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en général) ou une ComboBox(propriété ComboList)ou Nothing si CheckBox
    Dim uneBranche As Branche = maVariante.mBranches(CType(e.Row - 1, Short))
    Dim ARedessiner As Boolean
    '    Dim PassagesEtIlots As Boolean

    'La feuille est en cours de fermeture ou bascule d'une fenêtre carrefour à une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub

    Try
      With uneBranche
        Dim exLargeur As Single = .Largeur

        Select Case NomColonne
          Case "NomRue"
            If .NomRue = Controle.Text Then Exit Sub
            .NomRue = Controle.Text
            MajRueLF(uneBranche)

          Case "Angle"
            If .Angle = Int16.Parse(Controle.Text) Then Exit Sub
            e.Cancel = ControlerBornes(Me, 0, 360, Controle, .Angle)
            If Not e.Cancel Then
              UneCommandeGraphique = CommandeGraphique.AngleBranche
              DéfinirPointsBranche(uneBranche)
              Dim vMini As Single = Math.Ceiling(AngleMini)
              If vMini < 0 Then vMini += 360
              Dim vMaxi As Single = Math.Floor(AngleMini + BalayageMaxi)
              Dim AngleFinal As Single = CSng(Controle.Text)
              'Test repris de AngleBrancheOK
              If AngleFinal > 180 Then AngleFinal -= 360
              If AngleFinal < AngleMini Then AngleFinal += 360
              If (AngleFinal - AngleMini) >= BalayageMaxi Then
                e.Cancel = ControlerBornes(Me, vMini, vMaxi, Controle, .Angle)
              End If
              DémarrerCommande(CommandeGraphique.AucuneCommande)
            End If

            If Not e.Cancel Then
              .Angle = Int16.Parse(Controle.Text)
              ARedessiner = True
            End If

          Case "Longueur"
            If .Longueur = Int16.Parse(Controle.Text) Then Exit Sub
            e.Cancel = ControlerBornes(Me, Branche.miniLongueur, Branche.maxiLongueur, Controle, .Longueur)
            If Not e.Cancel Then
              .Longueur = Int16.Parse(Controle.Text)
              ARedessiner = True
            End If

          Case "LargeurVoies"
            If .LargeurVoies = Single.Parse(Controle.Text) Then Exit Sub
            e.Cancel = ControlerBornes(Me, Branche.miniLargeurVoies, Branche.maxiLargeurVoies, Controle, .LargeurVoies)
            If Not e.Cancel Then
              Controle.Text = Format(Single.Parse(Controle.Text), "0.#")
              .LargeurVoies = Single.Parse(Controle.Text)
              .RecalerPassagesPiétons((.Largeur - exLargeur) / 2)
              ARedessiner = True
              '              PassagesEtIlots = True
            End If

          Case "NbVoiesE", "NbVoiesS"
            If NomColonne = "NbVoiesE" Then
              If .NbVoies(Voie.TypeVoieEnum.VoieEntrante) = Int16.Parse(Controle.Text) Then Exit Sub
            Else
              If .NbVoies(Voie.TypeVoieEnum.VoieSortante) = Int16.Parse(Controle.Text) Then Exit Sub
            End If

            e.Cancel = ControlerBornes(Me, 0, Branche.maxiNbVoies, Controle, _
            IIf(NomColonne = "NbVoiesE", .NbVoies(Voie.TypeVoieEnum.VoieEntrante), .NbVoies(Voie.TypeVoieEnum.VoieSortante)), unFormat:="0")
            Dim nbVoies As Short = Int16.Parse(Controle.Text)
            If nbVoies = 0 And _
                ((NomColonne = "NbVoiesE" And .NbVoies(Voie.TypeVoieEnum.VoieSortante) = 0) Or _
                (NomColonne = "NbVoiesS" And .NbVoies(Voie.TypeVoieEnum.VoieEntrante) = 0)) Then
              AfficherMessageErreur(Me, "La branche doit comporter au moins une voie")
              e.Cancel = True
            Else
              ARedessiner = True
              '              PassagesEtIlots = True
            End If

            If Not e.Cancel Then
              If NomColonne = "NbVoiesE" Then
                .NbVoies(Voie.TypeVoieEnum.VoieEntrante) = Int16.Parse(Controle.Text)
              Else
                .NbVoies(Voie.TypeVoieEnum.VoieSortante) = Int16.Parse(Controle.Text)
              End If
              .RecalerPassagesPiétons((.Largeur - exLargeur) / 2)
            End If

          Case "Ilot"
            Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
            ' A ce stade, la case à cocher n'est pas encore mis à jour (sera fait dans CellChange!!!) : il faut donc inverser le booléen lors du cochage
            If rg.Checkbox = Grille.CheckEnum.Checked Then
              e.Cancel = Not Confirmation("Supprimer l'ilot", Critique:=False)
            End If
        End Select
      End With

      ' L'ilot est traité ensuite par CellChanged
      If ARedessiner Then

        'PassagesEtIlot : caduque à partir de la v11 (Juillet 2006)
        'If PassagesEtIlots Then
        '  'Colonne  "Largeur des voies" ou "nombre de voies" : redéfinir l'ilot et supprimer les passages piétons
        '  'Ceci ne survient en principe que si la géométrie n'est pas verrouillée (cf VerrouillerBoutonsGéométrie)

        '  '1) Redéfinir l'ilot éventuel avec les valeurs par défaut
        '  If uneBranche.AvecIlot Then
        '    Dim Index As Short = mesBranches.IndexIlot(uneBranche.mIlot)
        '    Dim unIlot As New Ilot(uneBranche)
        '    Dim fgIlot As GrilleDiagfeux = Me.AC1GrilleIlot
        '    'Rechercher la ligne de la grille adaptée
        '    Dim rg As Grille.CellRange = fgIlot.TouteLaLigne(Index)
        '    'Afficher les données dans la ligne
        '    rg.Clip = unIlot.strLigneGrille(mesBranches, Séparateur:=Chr(9))
        '  End If

        '  '2)Supprimer les passages piétons
        '  uneBranche.mPassages.Clear()
        'End If

        RedessinerBranche(uneBranche)
        uneBranche.DéterminerVoiesPassages()
        If Not SelectObject Then SélDésélectionner() ' Montre ou cache les poignées de sélection
        Modif = True
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub MajRueLF(ByVal uneBranche As Branche)
    Dim uneLigneFeux As LigneFeux
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim rg As Grille.CellRange

    For Each uneLigneFeux In mesLignesFeux
      If uneLigneFeux.mBranche Is uneBranche Then
        rg = fg.GetCellRange(mesLignesFeux.IndexOf(uneLigneFeux) + 1, 1)
        rg.Data = uneBranche.NomRue
      End If
    Next
  End Sub

  '******************************************************************************
  ' Grilles Branches et Conflits : KeyPressEdit
  '******************************************************************************
  Private Sub AC1GrilleBranches_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) _
  Handles AC1GrilleBranches.KeyPressEdit, Ac1GrilleSécurité.KeyPressEdit, AC1GrilleIlot.KeyPressEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomChamp As String = fg.Cols(e.Col).Name

    With e

      Select Case QuelType(fg.Cols(e.Col).DataType)
        Case Outils.DataTypeEnum.typeSingle
          .Handled = ToucheNonNumérique(e.KeyChar, Entier:=False)
          ''Si on frappe le point décimal et que les paramètres régionaux comportent une autre valeur que le point décimal comme séparateur, 
          '' celui-ci est refusé par la fonction précédente : on remplace le point décimal par le caractère spécifique régional
          'If .KeyChar = "."c And .Handled Then SendKeys.Send(cndPtDécimal)
        Case Outils.DataTypeEnum.typeInt16
          .Handled = ToucheNonNumérique(.KeyChar)
      End Select
    End With

  End Sub

  '******************************************************************************
  ' Grille Branches  : CellChanged
  ' Traitement de la case à cocher Ilot
  '******************************************************************************
  Private Sub AC1GrilleBranches_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) _
   Handles AC1GrilleBranches.CellChanged
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim row, col As Short
    row = e.Row
    col = e.Col
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim uneBranche, maBranche As Branche
    Dim unIlot As Ilot
    Dim Index As Short = 1

    Dim fgIlot As GrilleDiagfeux = Me.AC1GrilleIlot

    Try

      If NomColonne = "Ilot" Then
        maBranche = mesBranches(row - 1)
        rg = fg.GetCellRange(row, col)
        If rg.Checkbox = Grille.CheckEnum.Checked Then
          Me.pnlIlots.Visible = True
          If ChargementEnCours Then
            unIlot = maBranche.mIlot
            Index = fgIlot.Rows.Count
            SelectObject = True
          Else
            For Each uneBranche In mesBranches
              If uneBranche.AvecIlot Then
                Index += 1
                If mesBranches.IndexOf(uneBranche) > mesBranches.IndexOf(maBranche) Then
                  Index -= 1
                  Exit For
                End If
              End If
            Next
            unIlot = New Ilot(maBranche)
            DessinerObjet(unIlot.CréerGraphique(colObjetsGraphiques))
          End If
          fgIlot.Rows.Insert(Index)
          fgIlot(Index, 0) = mesBranches.ID(maBranche)
          'Rechercher la ligne de la grille adaptée
          rg = fgIlot.TouteLaLigne(Index)
          'Afficher les données dans la ligne
          rg.Clip = unIlot.strLigneGrille(mesBranches, Séparateur:=Chr(9))

          If ChargementEnCours Then SelectObject = False

        Else
          unIlot = maBranche.mIlot
          'Supprimer la ligne du tableau d'ilots
          fgIlot.Rows.Remove(mesBranches.IndexIlot(unIlot))

          'Supprimer l'ilot des objets graphiques
          maBranche.SupprimerIlot(colObjetsGraphiques)

          'Redessiner la branche
          RedessinerBranche(maBranche)
          'Redessiner à effacer les poignées de la branche si elle était sélectionnée
          objSélect = Nothing

          Me.pnlIlots.Visible = fgIlot.Rows.Count > 1

        End If

        If Not ChargementEnCours AndAlso Not e.Cancel Then
          Modif = True
        End If

        fgIlot.Height = (fgIlot.Rows.Count - 1) * 17 + 21

      End If    ' NomColonne = "Ilot"

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '******************************************************************************
  ' Grille Branches : BeforeRowColChange
  '******************************************************************************
  Private Sub AC1GrilleBranches_BeforeRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleBranches.BeforeRowColChange
    Dim unStyle As Grille.CellStyle
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim col As Short

    If e.NewRange.IsValid Then unStyle = e.NewRange.Style

    If Not IsNothing(unStyle) Then
      'Si le style est grisé, on interdit l'accès à la cellule
      e.Cancel = StyleInterdit(unStyle)
    End If

  End Sub

  '******************************************************************************
  ' Grille Branches : AfterRowColChange
  '******************************************************************************
  Private Sub AC1GrilleBranches_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleBranches.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    Dim rgOld As Grille.CellRange = e.OldRange
    Dim rgNew As Grille.CellRange = e.NewRange

    'La ligne sélectionnée peut être = -1 ==> IsValid=false
    If e.NewRange.IsValid Then
      'Passer en mode saisie sauf poul l'ilot (case à cocher que l'instruction ferait basculer)
      If rgNew.c1 < 7 Then fg.StartEditing()

      If rgOld.r1 <> rgNew.r1 And Not SelectObject Then
        Désélectionner()
        objSélect = mesBranches(rgNew.r1 - 1).mGraphique
        SélDésélectionner() ' Montre ou cache les poignées de sélection
      End If
    End If

  End Sub
#End Region
#Region " Grille Ilot"
  Private Sub AC1GrilleIlot_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles AC1GrilleIlot.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim ModifIlot As Boolean

    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en général) ou une ComboBox(propriété ComboList)ou Nothing si CheckBox
    Dim Index As Short = e.Row
    Dim unIlot As Ilot = mesBranches.IlotBranche(Index)
    'La feuille est en cours de fermeture ou bascule d'une fenêtre carrefour à une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub

    Try
      Dim Valeur As Single = Single.Parse(Controle.Text)
      Controle.Text = Format(Valeur, "0.0")
      With unIlot
        Select Case NomColonne
          Case "Largeur"
            If .Largeur <> Valeur Then
              e.Cancel = ControlerBornes(Me, Ilot.miniLargeur, Ilot.maxiLargeur, Controle, .Largeur, unFormat:="0.0")
            End If
            If Not e.Cancel Then
              .Largeur = Valeur
              ModifIlot = True
            End If

          Case "Rayon"
            If .Rayon <> Valeur Then
              e.Cancel = ControlerBornes(Me, Ilot.miniRayon, Ilot.maxiRayon, Controle, .Rayon, unFormat:="0.0")
            End If
            If Not e.Cancel Then
              .Rayon = Valeur
              ModifIlot = True
            End If

          Case "Décalage"
            If .Décalage = Valeur Then
              e.Cancel = ControlerBornes(Me, Ilot.miniLargeur, .mBranche.Largeur, Controle, .Décalage, unFormat:="0.0")
            End If
            If Not e.Cancel Then
              .Décalage = Valeur
              ModifIlot = True
            End If

          Case "Retrait"
            If .Retrait = Valeur Then
              e.Cancel = ControlerBornes(Me, Ilot.miniRetrait, Ilot.maxiRetrait, Controle, .Retrait, unFormat:="0.0")
            End If
            If Not e.Cancel Then
              .Retrait = Valeur
              ModifIlot = True
            End If
        End Select

        If ModifIlot And Not e.Cancel Then
          unIlot.CréerGraphique(colObjetsGraphiques)
          Redessiner()
          Modif = True
        End If
      End With

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub
  Private Sub AC1GrilleIlot_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleIlot.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    Dim rgOld As Grille.CellRange = e.OldRange
    Dim rgNew As Grille.CellRange = e.NewRange

    'La ligne sélectionnée peut être = -1 ==> IsValid=false
    If e.NewRange.IsValid Then
      'Passer en mode saisie 
      If Not IsNothing(rgNew.Data) Then fg.StartEditing()

      If rgOld.r1 <> rgNew.r1 And rgNew.r1 > 0 And Not SelectObject Then
        Désélectionner()
        objSélect = mesBranches.IlotBranche(rgNew.r1).mGraphique
        SélDésélectionner()   ' Montre ou cache les poignées de sélection
      End If
    End If

  End Sub
#End Region
#Region " Grille Trafic"
  '******************************************************************************
  ' Interdire l'accès à la saisie des trafics totaux (qui sont calculés par  DIAGFEUX)
  '******************************************************************************
  Private Sub GrilleTrafics_BeforeRowColChange(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _
  Ac1GrilleTraficPiétons.BeforeRowColChange, AC1GrilleTraficVéhicules.BeforeRowColChange

    Dim unStyle As Grille.CellStyle
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim col As Short

    If e.NewRange.IsValid Then unStyle = e.NewRange.Style

    If Not IsNothing(unStyle) Then
      'Si le style est grisé, on interdit l'accès à la cellule
      e.Cancel = StyleInterdit(unStyle)
    End If

    If Not fg.Cols(e.NewRange.c1).AllowEditing Then e.Cancel = True

    If Not e.Cancel And e.OldRange.IsValid Then
      'Lignes suivantes supprimées (AV : 7/6/06 ) Ne sert à rien a priori. De + réactive la cellule 1,1 quand on clique dans la grille(il ne faut pas si branche A est sens unique)
      ' rg = e.OldRange
      'rg.Style = fg.Styles("Normal")
    End If

  End Sub

  Private Sub AC1GrilleTraficVéhicules_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleTraficVéhicules.AfterRowColChange, Ac1GrilleTraficPiétons.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    fg.StartEditing()
  End Sub
  Private Sub AC1GrilleTrafics_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles Ac1GrilleTraficPiétons.KeyPressEdit, AC1GrilleTraficVéhicules.KeyPressEdit
    '******************************************************************************
    ' Grilles Trafics : KeyPressEdit
    '******************************************************************************
    Dim fg As GrilleDiagfeux = sender
    Dim NomChamp As String = fg.Cols(e.Col).Name

    'Donnée trafic
    e.Handled = ToucheNonNumérique(e.KeyChar)

  End Sub

  '******************************************************************************
  ' Grilles Trafics : ValidateEdit
  '******************************************************************************
  Private Sub AC1GrilleTrafics_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) _
  Handles Ac1GrilleTraficPiétons.ValidateEdit, AC1GrilleTraficVéhicules.ValidateEdit

    Dim fg As GrilleDiagfeux = sender
    Dim Controle As Control = fg.Editor
    Dim i, j As Short
    Dim ligneTotal As Short = mesBranches.Count + 1
    Dim colonneTotal As Short = ligneTotal
    Dim Index As Trafic.TraficEnum = IndexTrafic()

    'La feuille est en cours de fermeture ou bascule d'une fenêtre carrefour à une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If IsNothing(monTraficActif) Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub

    If Controle.Text = "" Then Controle.Text = "0"

    e.Cancel = ControlerBornes(Me, 0, Trafic.vMaxi, Controle, Nothing, unFormat:="0")

    Try

      If Not e.Cancel Then
        Dim rgCellule As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
        If fg Is Me.AC1GrilleTraficVéhicules Then
          'Affecter le nouveau trafic
          monTraficActif.QVéhicule(Index, e.Row - 1, e.Col - 1) = CType(Controle.Text, Short)
          'Afficher le nouveau trafic entrant sur cette branche
          fg(e.Row, colonneTotal) = monTraficActif.QE(Index, e.Row - 1)
          'Afficher le nouveautrafic sortant par cette branche
          fg(ligneTotal, e.Col) = monTraficActif.QS(Index, e.Col - 1)
          'Afficher le nouveautrafic total du carrefour
          fg(mesBranches.Count + 1, mesBranches.Count + 1) = monTraficActif.QTotal(Index)
        Else
          'Afficher le nouveau trafic piéton sur cette branche
          monTraficActif.QPiéton(e.Col) = CType(Controle.Text, Short)
        End If

        Modif = True
        AfficherTraficSaturé()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub AfficherTraficSaturé()
    Dim uneBranche As Branche
    Dim rg As Grille.CellRange

    Me.lblTaficSaturé.Visible = False
    If maVariante.Verrou <= [Global].Verrouillage.LignesFeux Then
      For Each uneBranche In mesBranches
        rg = Me.AC1GrilleTraficVéhicules.GetCellRange(mesBranches.IndexOf(uneBranche) + 1, mesBranches.Count + 1)
        If Not uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) AndAlso _
        monTraficActif.QE(Trafic.TraficEnum.UVP, mesBranches.IndexOf(uneBranche)) / uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) > maVariante.DébitSaturation Then
          Me.lblTaficSaturé.Visible = True
          rg.Style = StyleGriséRouge
        Else
          rg.Style = StyleGrisé
        End If
      Next
    End If

  End Sub

#End Region
#Region " Grille Feux"
  '******************************************************************************
  ' Validation d'un champ de la grille Ligne de feux
  '******************************************************************************
  Private Sub AC1GrilleFeux_ValidateEdit(ByVal sender As System.Object, ByVal e As Grille.ValidateEditEventArgs) _
  Handles AC1GrilleFeux.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en général) ou une ComboBox(propriété ComboList) ou Nothing (checkbox)
    Dim Arrêt As Boolean
    Dim uneLigneFeux, uneLF As LigneFeux
    Dim ValeurModifiée As String
    Dim Booléen As Boolean
    Dim DernièreLigne As Boolean = (e.Row = fg.Rows.Count - 1)
    Dim unStyle As Grille.CellStyle
    Dim unSignal As Signal
    Dim rg As Grille.CellRange
    Dim ValeurCourante As String
    Dim BasculePiétonsVéhicules As Boolean
    Dim Message As String

    ' Jusqu'à la v11, on pouvait avoir le message suivant lors de l'appel de la fonction 'LigneDeFeux.MettreAJour'
    'Cast de la chaîne "Voies" en type 'Short' non valide. ---> System.FormatException: Le format de la chaîne d'entrée est incorrect.

    'e.Row = 0 peut parfois arriver sur une suppression de ligne de feux
    If e.Row = 0 Then Exit Sub

    'La 1ère ligne de de feux existe dans la grille dès le départ, mais peut être vide(il n'y a pas encore de lignes de feux)
    'dans ce cas elle est invisible et il faut l'ignorer
    If e.Row = 1 And Not fg.Rows(1).Visible Then Exit Sub

    'La feuille est en cours de fermeture ou bascule d'une fenêtre carrefour à une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub
    If DécalageFeuxEnCours Then Exit Sub

    Try
      Dim rgCellule As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

      If IsNothing(Controle) Then
        'Case à cocher pour les TAG,TAD...
        unStyle = rgCellule.Style
        If IsNothing(unStyle) Then   'Cas général
          Booléen = (e.Checkbox = Grille.CheckEnum.Checked)
          Arrêt = (e.Checkbox = Grille.CheckEnum.None)
        Else
          'Le style est grisé si Mode graphique ou en cas de ligne de feux piétons
          Arrêt = StyleInterdit(unStyle)
        End If
        e.Cancel = Arrêt

      Else
        ValeurModifiée = Controle.Text
        Arrêt = (ValeurModifiée.Length = 0)
      End If

      If Not Arrêt Then
        'Controles de 1er niveau 
        Select Case NomColonne
          Case "IDVoie"
          Case "NomRue"

          Case "ID"
            'Récupérer la valeur courante de l'ID
            ValeurCourante = rgCellule.Data

            If ValeurModifiée.Length > 2 Then
              Message = "Le nom de la ligne de feux ne doit pas dépasser 2 caractères"

            Else
              'Détecter si une ligne de feux de même ID n'existe pas déjà pour une ligne de feux
              With mesLignesFeux
                If .Contains(ValeurModifiée) Then
                  'Ce  n'est pas un problème s'il s'agit de la même ligne (i.e. : l'ID n'est en fait pas modifié)
                  If e.Row <> .IndexOf(.Item(ValeurModifiée)) + 1 Then
                    Message = "Nom de feu existant"
                  End If
                End If
              End With

              If Not ModeGraphique And Not DernièreLigne And IsNothing(Message) Then
                'Vérifier que cet ID n'est pas pris non plus par la ligne de feux en cours de création (dernière ligne de la grille)
                rg = fg.GetCellRange(fg.Rows.Count - 1, e.Col)
                If ValeurModifiée = rg.Data Then
                  Message = "Nom de feu existant"
                End If
              End If
            End If

            If Not IsNothing(Message) And ValeurCourante <> ValeurModifiée Then
              MessageBox.Show(Me, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
              rgCellule.Clear(Grille.ClearFlags.Content)
              'Rétablir la valeur mémorisée lors de StartEdit(en fait ne fonctionne pas)
              rgCellule.Data = strSauveGrille
              e.Cancel = True
            End If

          Case "Signal"
            unSignal = cndSignaux(ValeurModifiée)
            'Pointer sur la colone Signal Anticipation : celle-ci est grisée si Ligne Piétons
            rg = fg.GetCellRange(e.Row, 4)
            BasculePiétonsVéhicules = unSignal.EstPiéton Xor (rg.Style.Name = StyleGrisé.Name Or rg.Style.Name = StyleGriséGras.Name)
            If BasculePiétonsVéhicules Then
              If maVariante.VerrouLigneFeu Then
                MessageBox.Show(Me, "Le passage d'un feu piéton à un feu véhicule ou inversement est interdit", NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
              ElseIf Not DernièreLigne Then
                uneLigneFeux = mesLignesFeux(CType(e.Row - 1, Short))
                If uneLigneFeux.ToutesVoiesSurBranche Then
                  Message = "Branche à sens unique : elle doit comporter au moins une ligne de feux"
                  e.Cancel = True
                End If
              End If
              If e.Cancel Then
                MessageBox.Show(Me, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Rétablir la valeur mémoriser lors de StartEdit
                rgCellule.Clip = strSauveGrille
              End If
            End If

          Case "NbVoies"
              e.Cancel = ControlerBornes(Me, 1, 4, Controle, Nothing, unFormat:="0")

          Case "SignalAnticipation"
          Case "TAD", "TD", "TAG"

        End Select

        If Not e.Cancel Then
          'Traitements complémentaires
          'Mise à jour de la ligne de feux
          uneLigneFeux = mesLignesFeux.MettreAjour(ValeurModifiée, e.Checkbox = Grille.CheckEnum.Checked, fg.strLigneEntière(e.Row), e.Row - 1, e.Col)
          Dim MajBranche As Boolean = BasculePiétonsVéhicules

          If Not IsNothing(uneLigneFeux) Then
            'En mode tableur : Rajout automatique d'une ligne vierge 
            'dès que les infos minimales ont permis de créer la ligne de feu ou encore bascule ligne véhicules/piétons
            If Not ModeGraphique Then
              If DernièreLigne Then
                fg.Rows.Add()
                MajBranche = True
              ElseIf NomColonne = "NbVoies" Then
                MajBranche = True
              End If
            End If
          End If

          'Mise à jour des données corrélées
          Select Case NomColonne
            Case "IDVoie"
              'Mettre à jour le nom de la voie en fonction du nom de la branche
              rg = fg.GetCellRange(e.Row, 1)
              rg.Data = mesBranches(CType(ValeurModifiée, Char)).NomRue

            Case "NomRue"
              'Mettre à jour le nom de la voie pour toutes les lignes relatives à la même branche
              Dim IDVoie As Char
              Dim row As Short

              rg = fg.GetCellRange(e.Row, 0)
              IDVoie = rg.Data
              For row = 1 To fg.Rows.Count - 1
                rg = fg.GetCellRange(row, 0, row, 1)
                If rg.Data = IDVoie Then rg.Clip = IDVoie & fg.ClipSeparators.Chars(0) & ValeurModifiée
              Next
              With Me.AC1GrilleBranches
                Dim uneBranche As Branche = mesBranches(IDVoie)
                rg = .GetCellRange(mesBranches.IndexOf(uneBranche) + 1, 1)
                rg.Data = ValeurModifiée
              End With

            Case "ID"
              If ModeGraphique Then
                EffacerObjet(uneLigneFeux.mSignalFeu(0).mGraphique)
                If uneLigneFeux.EstPiéton AndAlso CType(uneLigneFeux, LigneFeuPiétons).SignalAReprésenter(1) Then
                  EffacerObjet(uneLigneFeux.mSignalFeu(1).mGraphique)
                End If
                uneLigneFeux.CréerGraphique(colObjetsGraphiques)
                DessinerLigneDeFeux(uneLigneFeux)
              End If
              If ValeurCourante <> ValeurModifiée Then
                AfficherConséquencesModifLignesDeFeux(SuiteADécalage:=False)
                RenommerLignePlansFeux(uneLigneFeux, ValeurCourante)
              End If

            Case "Signal"
              'Sélectionner les cellules concernées en cas de ligne de feux véhicules seulement
              GriserLignePiétons(fg, e.Row, unSignal.EstPiéton)

            Case "SignalAnticipation"
            Case "NbVoies"
            Case "TAD", "TD", "TAG"
          End Select

          If MajBranche Then
            'V13 (AV : 10/01/07) : en cas de bascule véhicules piétons : le nb de voies reste affiché et les TAD,TAG restent cochés
            'Par ailleurs, pour une nouvelle ligne véhicules, il est intéressant d'initialiser à 1 le nombre de voies
            rg = fg.GetCellRange(e.Row, 0, e.Row, fg.Cols.Count - 1)
            rg.Clip = uneLigneFeux.strLigneGrille(mesBranches, Séparateur:=Chr(9))

            'DIAGFEUX 3 : les voies entrantes sont déduites des voies des lignes de feux
            MettreAJourVoiesBranches()
          End If

          Modif = True
        End If  'not e.cancel

      End If  ' Not Arrêt

      ActiverBoutonsLignesFeux()


    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  Private Sub MettreAJourVoiesBranches()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleBranches
    Dim rg As Grille.CellRange
    Dim uneBranche As Branche
    Dim i As Short

    If Not ModeGraphique Then
      For Each uneBranche In mesBranches
        i = mesBranches.IndexOf(uneBranche)
        rg = fg.GetCellRange(i + 1, 5)
        rg.Data = uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante)
      Next

      RecréerGraphique()
      Redessiner()
    End If

  End Sub

  '******************************************************************************
  ' Grille : ligne de feux -  L'utilisateur change de cellule
  ' Interdire ce changement si la cellule est interdite
  '******************************************************************************
  Private Sub AC1GrilleFeux_BeforeRowColChange(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) _
  Handles AC1GrilleFeux.BeforeRowColChange
    Dim unStyle As Grille.CellStyle
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim col As Short

    If e.NewRange.IsValid Then unStyle = e.NewRange.Style

    If Not IsNothing(unStyle) Then
      'Si le style est grisé, on interdit l'accès à la cellule
      e.Cancel = StyleInterdit(unStyle)
    End If

    If Not fg.Cols(e.NewRange.c1).AllowEditing Then
      e.Cancel = True
      'La tentative de sélectionner la ligne plante en débordement de pile (le try catch ne fonctione pas
      'If e.NewRange.c1 = 0 And e.NewRange.r1 > 0 Then
      '  Try
      '    fg.Select(fg.GetCellRange(e.NewRange.r1, 0, e.NewRange.r1, fg.Cols.Count - 1))

      '  Catch ex As System.Exception
      '    Debug.Write("")
      '  End Try
      'End If

    ElseIf Not ModeGraphique AndAlso e.NewRange.IsValid AndAlso e.NewRange.c1 > 0 Then
      'En mode tableur : imposer le choix de la branche avant toute saisie
      rg = fg.GetCellRange(e.NewRange.r1, 0)
      If IsNothing(rg.Data) OrElse rg.Data = Nothing Then
        'Si on ouvre la combo IDVoie sans rien sélectionner, rg.data n'est plus à Nothing
        e.Cancel = True
      End If
    End If

    If Not e.Cancel And e.OldRange.IsValid Then
      'Rétablir les styles non gras pour les anciens objets sélectionnés (cf évènement SelChange)
      For col = 0 To fg.Cols.Count - 1
        rg = fg.GetCellRange(e.OldRange.r1, col)
        If IsNothing(rg.Style) Then
        ElseIf rg.Style.Name = StyleGriséGras.Name Then
          rg.Style = StyleGrisé
        ElseIf rg.Style.Name = StyleDégriséGras.Name Then
          rg.Style = StyleDégrisé
        End If
      Next
    End If

  End Sub

  '******************************************************************************
  ' Grille Ligne de feux : AfterRowColChange - Passer en mode Edit 
  '******************************************************************************
  Private Sub AC1GrilleFeux_AfterRowColChange(ByVal sender As Object, ByVal e As Grille.RangeEventArgs) _
  Handles AC1GrilleFeux.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    Dim rgOld As Grille.CellRange = e.OldRange
    Dim rgNew As Grille.CellRange = e.NewRange
    Dim UnStyle As Grille.CellStyle

    'La ligne sélectionnée peut être = -1 ==> IsValid=false
    If e.NewRange.IsValid And Not DécalageFeuxEnCours Then
      'Passer en mode saisie pour les champs :nom de la rue, nom du feu et nombre de voies (et surtout pas les cases à cocher que l'instruction ferait basculer)
      Select Case rgNew.c1
        Case 1, 2, 5
          Try
            If Not IsNothing(rgNew.Data) Then fg.StartEditing()
          Catch ex As System.Exception
            LancerDiagfeuxException(ex, "GrilleFeux.AfterRowColChange")
            Exit Sub
          End Try
      End Select

      If rgOld.r1 <> rgNew.r1 And Not SelectObject And ModeGraphique Then
        Désélectionner()
        Dim uneLigneFeux As LigneFeux = mesLignesFeux(CType(rgNew.r1 - 1, Short))
        If uneLigneFeux.EstVéhicule Then
          objSélect = uneLigneFeux.mGraphique
        Else
          'Mettre en valeur la traversée piétonne correspondant à la ligne de feux piétons
          objSélect = CType(uneLigneFeux, LigneFeuPiétons).mTraversée.mGraphique
        End If

        SélDésélectionner() ' Montre ou cache les poignées de sélection
      End If
    End If

  End Sub

  '******************************************************************************
  ' Grille Lignes de Feux : KeyPressEdit
  '******************************************************************************
  Private Sub AC1GrilleFeux_KeyPressEdit(ByVal sender As Object, ByVal e As Grille.KeyPressEditEventArgs) _
  Handles AC1GrilleFeux.KeyPressEdit

    Dim fg As GrilleDiagfeux = sender
    Dim NomChamp As String = fg.Cols(e.Col).Name
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

    Select Case e.Col
      Case 5   'Nombre de voies
        e.Handled = ToucheNonNumérique(e.KeyChar)
      Case 2 ' Code du feu
        Select Case e.KeyChar
          Case vbBack, "A"c To "Z"c, "a"c To "z"c, "0" To "9"
          Case Else
            e.Handled = True
        End Select
        If e.KeyChar = vbTab Then

        End If
    End Select

  End Sub

  '******************************************************************************
  ' Grille Ligne de feux : AfterEdit
  '******************************************************************************
  Private Sub AC1GrilleFeux_AfterEdit(ByVal sender As Object, ByVal e As Grille.RowColEventArgs) _
  Handles AC1GrilleFeux.AfterEdit
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

    If e.Col = 4 Then
      'Signal d'anticipation
      If rg.Data = "<Aucun>" Then rg.Data = ""
    End If

  End Sub

  '******************************************************************************
  ' Grille lignes de feux : SelChange
  ' Déterrminer l'activabilité des boutons selon la sélection
  '******************************************************************************
  Private Sub AC1GrilleFeux_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles AC1GrilleFeux.SelChange
    Dim fg As GrilleDiagfeux = sender
    Dim col As Short
    Dim rg As Grille.CellRange

    If fg.Row = -1 Then
      'Aucune ligne sélectionnée
      Me.btnLigneFeuxMoins.Enabled = ModeGraphique And mesLignesFeux.nbLignesVéhicules > 0 And maVariante.Verrou = [Global].Verrouillage.Géométrie
      Me.btnLigneFeuDescendre.Enabled = False
      Me.btnLigneFeuMonter.Enabled = False

    Else
      With fg
        Dim MaxRow As Short
        If ModeGraphique Or maVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
          MaxRow = .Rows.Count
        Else
          'En mode tableur, il y a toujours une ligne supplémentaire pour permettre la saisie 
          ' d'une nouvelle ligne de feux tant que celles ci ne sont pas verrouillées
          MaxRow = .Rows.Count - 1
        End If
        'Déterminer si la ligne peut monter, descendre ou être supprimée
        Me.btnLigneFeuxMoins.Enabled = .Row < MaxRow And maVariante.Verrou = [Global].Verrouillage.Géométrie
        Me.btnLigneFeuMonter.Enabled = .Row < MaxRow And .Row > 1
        Me.btnLigneFeuDescendre.Enabled = .Row < MaxRow - 1
        'Mettre en gras les cellules de la ligne sélectionnée 
        For col = 0 To .Cols.Count - 1
          rg = .GetCellRange(.Row, col)
          If IsNothing(rg.Style) Then
            rg.Style = StyleDégriséGras
          ElseIf rg.Style.Name = StyleGrisé.Name Then
            'conserver le grisé existant
            rg.Style = StyleGriséGras
          Else
            rg.Style = StyleDégriséGras
          End If
        Next

      End With

    End If
  End Sub

  '**********************************************************************************************************************
  ' StartEdit : l'utilisateur commence à éditer la ligne de feux - mémoriser la valeur précédente
  '**********************************************************************************************************************
  Private Sub AC1GrilleFeux_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles AC1GrilleFeux.StartEdit
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim rg = fg.GetCellRange(e.Row, e.Col)
    Dim NomColonne As String = fg.Cols(e.Col).Name

    'On ne mémorise que les signaux de feux
    If NomColonne = "ID" Or NomColonne = "Signal" Then strSauveGrille = rg.data

    If NomColonne = "Signal" And ModeGraphique Then
      'Peut arriver par double click sur un signal piéton : il faut empêcher le déroulement de la liste des signaux
      Dim uneLigneFeux As LigneFeux = mesLignesFeux(CType(rg.r1 - 1, Short))
      If uneLigneFeux.EstPiéton Then e.Cancel = True
    End If

  End Sub

#End Region
#Region " Grille sécurité"
  '**********************************************************************************************************************
  ' Interdire l'édition d'une celule si son style l'interdit
  '**********************************************************************************************************************
  Private Sub Ac1GrilleSécurité_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles Ac1GrilleSécurité.BeforeEdit
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
    Dim unStyle As Grille.CellStyle = rg.Style

    If Not IsNothing(unStyle) Then
      'Si le style est grisé, on interdit l'accès à la cellule
      e.Cancel = StyleInterdit(unStyle)
      If Not e.Cancel Then btnRougeDéfaut.Enabled = Me.radMatriceRougesDégagement.Checked
    End If

  End Sub

  '**********************************************************************************************************************
  ' Click sur un point de conflit dans la matrice de sécurité
  ' Basculer la case de rouge en vert ou inversement, ainsi que la case symétrique
  '**********************************************************************************************************************
  Private Sub Ac1GrilleSécurité_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ac1GrilleSécurité.Click

    If Me.radMatriceConflits.Checked Then
      ' Onglet matrice des conflits

      Dim fg As GrilleDiagfeux = sender
      Dim rg As Grille.CellRange = fg.CelluleSélectionnée

      If rg.IsValid AndAlso rg.c1 > 0 Then
        Try

          Dim row As Integer = rg.r1
          Dim col As Integer = rg.c1
          Dim unAntagonisme As Antagonisme

          ' lHorizontale désigne la row de feux horizontale
          ' lVerticale désigne la col de feux verticale
          Dim lHorizontale As LigneFeux = mLignesFeux(CType(row - 1, Short))
          Dim lVerticale As LigneFeux = mLignesFeux(CType(col - 1, Short))

          'Basculer les cellules rouges en vert et inversement
          Dim unstyle As Grille.CellStyle = rg.Style

          Select Case unstyle.Name
            Case "Rouge"
              Dim msg As String
              If ModeGraphique Then
                Dim ListeNonAdmis As String = ""
                Select Case mAntagonismes.ExisteConflit(lHorizontale, lVerticale, ListeNonAdmis)
                  Case Trajectoire.TypeConflitEnum.Systématique
                    msg = "Ces lignes de feux sont strictement incompatibles"
                  Case Trajectoire.TypeConflitEnum.NonAdmis
                    msg = "Ces lignes de feux sont incompatibles suite à une décision précédente sur les conflits" & vbCrLf & _
                           ListeNonAdmis & vbCrLf & "Rendre d'abord le(s) conflit admis"
                  Case Trajectoire.TypeConflitEnum.Admissible
                    msg = "Ces lignes de feux sont incompatibles tant que toutes les décisions n'ont pas été prises sur les conflits" & vbCrLf & _
                          "Rendre d'abord le conflit admis"
                  Case Else
                    Dim lVeh As LigneFeuVéhicules
                    Dim lPiétons As LigneFeuPiétons

                    If (lHorizontale.EstPiéton Or lVerticale.EstPiéton) Then
                      'Vérifier si les antagonismes liés ont été résolus individuellement
                      If lHorizontale.EstVéhicule Then
                        lVeh = lHorizontale
                        lPiétons = lVerticale
                      Else
                        lVeh = lVerticale
                        lPiétons = lHorizontale
                      End If
                      Dim lv2 As LigneFeuVéhicules = lVeh.LigneFeuxLiée(lPiétons)
                      If Not IsNothing(lv2) Then
                        msg = "Ces lignes ne peuvent pas être compatibles car " & lVeh.ID & " et " & lv2.ID & " ne le sont pas"
                      End If
                    End If

                End Select
              End If

              If IsNothing(msg) Then
                rg.Style = StyleVert
                'Basculer aussi la cellule symétrique
                rg = fg.GetCellRange(col, row)
                rg.Style = StyleVert
                mLignesFeux.EstIncompatible(lHorizontale, lVerticale) = False

              Else
                AfficherMessageErreur(Me, msg)
              End If

            Case "Vert"
              rg.Style = StyleRouge
              'Basculer aussi la cellule symétrique
              rg = fg.GetCellRange(col, row)
              rg.Style = StyleRouge
              mLignesFeux.EstIncompatible(lHorizontale, lVerticale) = True

              If ModeGraphique Then
                'Basculer également les conflits admissibles sans décision ou Admis dans un 1er temps (Admissible/Admis --> NonAdmis)
                For Each unAntagonisme In mAntagonismes()
                  With unAntagonisme
                    If .AntagonismeLié(lHorizontale, lVerticale) And .Admissible Then
                      MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.NonAdmis)
                    End If
                  End With
                Next
              End If

            Case "Orangé"
              AfficherMessageErreur(Me, "Ce conflit doit être résolu par la gestion des antagonismes")
              Dim IndexAntago As Short
              Dim IndexADéfinir As Short = -1
              Dim BrancheCherchée As Branche
              Dim IndexBranche As Short

              For Each unAntagonisme In mAntagonismes()
                With unAntagonisme
                  If .AntagonismeLié(lHorizontale, lVerticale) AndAlso .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
                    BrancheCherchée = .Courant(Antagonisme.PositionEnum.Premier).Branche(TrajectoireVéhicules.OrigineDestEnum.Origine)
                    IndexAntago = mAntagonismes.IndexOf(unAntagonisme)
                    If maVariante.BrancheEnCoursAntagonisme Is BrancheCherchée Then
                      IndexADéfinir = -1
                      Exit For
                    Else
                      If IndexADéfinir = -1 Then
                        IndexADéfinir = IndexAntago
                        IndexBranche = mesBranches.IndexOf(BrancheCherchée)
                      End If
                    End If
                  End If
                End With
              Next
              If IndexADéfinir <> -1 Then
                IndexAntago = IndexADéfinir
                If Me.cboBrancheCourant1.SelectedIndex <> mesBranches.Count Then Me.cboBrancheCourant1.SelectedIndex = IndexBranche
              End If
              Me.AC1GrilleAntagonismes.Select(IndexAntago + 1, 0)

          End Select

          'Pour éviter que + tard la cellule sélectionnée se rallume intempestivement
          Me.Ac1GrilleSécurité.Row = -1
          Me.Ac1GrilleSécurité.Col = -1

        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        End Try

      End If  ' rg.IsValid
    End If    ' Matrice de sécurité = Matrice des conflits

  End Sub

  '**********************************************************************************************************************
  ' Empêcher que le double click passe en mode saisie
  '**********************************************************************************************************************
  Private Sub Ac1GrilleSécurité_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ac1GrilleSécurité.DoubleClick
    Dim fg As GrilleDiagfeux = sender
    SendKeys.Send("{ESC}")
  End Sub

  '**********************************************************************************************************************
  ' Validation d'une cellule de la grille des matrices de sécurité (rouges de dégagement seulement)
  '**********************************************************************************************************************
  Private Sub Ac1GrilleSécurité_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles Ac1GrilleSécurité.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en général) ou une ComboBox(propriété ComboList) ou Nothing (checkbox)
    Dim Arrêt As Boolean

    Dim uneBranche As Branche
    Dim uneLigneFeux As LigneFeux
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
    Dim ValeurModifiée As String
    Dim Booléen As Boolean
    Dim unStyle As Grille.CellStyle

    'La feuille est en cours de fermeture ou bascule d'une fenêtre carrefour à une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If Not fg.Cols(fg.Col).AllowEditing Then e.Cancel = True : Exit Sub

    Try
      Dim lh, lv As LigneFeux
      ' lh désigne la ligne de feux horizontale (au sens matriciel)
      ' lv désigne la ligne de feux verticale
      lh = mLignesFeux(CType(e.Row - 1, Short))
      lv = mLignesFeux(CType(e.Col - 1, Short))

      'La valeur par défaut du rouge de dégagement du plan de feux de base 
      'est celui calculé comme rouge mini pour les lignes de feux de la variante (cf DéterminerTempsDégagement)
      Dim TempsMini As Short = mesLignesFeux.RougeDégagement(lh, lv)
      Dim TempsActuel As Short = mLignesFeux.TempsDégagement(lh, lv)
      Dim TempsNouveau As Short = Short.Parse(Controle.Text)
      Dim unFormat As String = "0"

      e.Cancel = ControlerBornes(Me, 0, LigneFeux.MaxiRougeDégagement, Controle, mLignesFeux.TempsDégagement(lh, lv), unFormat:=unFormat)

      If Not e.Cancel AndAlso TempsNouveau < TempsMini Then
        Dim Message As String = "La durée du rouge de dégagement ne devrait pas être inférieure à " & Format(TempsMini, unFormat)
        Message &= vbCrLf & "Confirmez-vous cette valeur ?"
        e.Cancel = Not Confirmation(Message, Critique:=True)
        If e.Cancel Then Controle.Text = TempsActuel
      End If

      If Not e.Cancel Then
        mLignesFeux.TempsDégagement(lh, lv) = TempsNouveau
        'tant que le phasage n'est pas retenu, on peut modifier les rouges de dégagement 
        'les durées mini sont à  recalculer(et donc les capacités)
        monPlanFeuxBase.CalculerCapacitésPlansPhasage()

        AfficherRouge(lh, lv, rg, fg)
        ActiverBoutonsRouges()

        ' La modification des rouges de dégagement est possible tant que le phasage n'est pas retenu
        ' Le nouveau calcul des capacités peut influer sur l'affichage de l'organisation du phasage
        If monPlanFeuxBase.AvecTrafic AndAlso Not IsNothing(monPlanPourPhasage) Then
          DéterminerAfficherCapacité(monPlanPourPhasage)
        End If
        Modif = True
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  'Activer la réinitialisation des rouges de dégagement si au moins un rouge n'a pas la valeur par défaut
  '*******************************************************************************************************
  Private Sub ActiverBoutonsRouges()
    Dim lh, lv As LigneFeux
    Dim Activé As Boolean

    For Each lh In mLignesFeux()
      For Each lv In mLignesFeux()
        If Not lh.EstTrivialementCompatible(lv) Then

          If mLignesFeux.TempsDégagement(lh, lv) <> mesLignesFeux.RougeDégagement(lh, lv) Then
            Activé = True
          End If
        End If
      Next
    Next

    Me.btnRougesDéfaut.Enabled = Activé

  End Sub

  Private Sub AfficherRouge(ByVal lh As LigneFeux, ByVal lv As LigneFeux, ByVal rg As Grille.CellRange, ByVal fg As GrilleDiagfeux)
    ' lh désigne la ligne de feux horizontale (au sens matriciel)
    ' lv désigne la ligne de feux verticale

    If mLignesFeux.TempsDégagement(lh, lv) <> mesLignesFeux.RougeDégagement(lh, lv) Then
      rg.Style = StyleSaisieItalique
    Else
      rg.Style = fg.Styles(Grille.CellStyleEnum.Normal)
    End If
    rg.Data = mLignesFeux.TempsDégagement(lh, lv)

  End Sub
#End Region
#Region " Grille Antagonismes"
  Private Sub AC1GrilleAntagonismes_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleAntagonismes.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    Dim rgOld As Grille.CellRange = e.OldRange
    Dim rgNew As Grille.CellRange = e.NewRange

    Debug.WriteLine("rowcolchange")
    'La ligne sélectionnée peut être = -1 ==> IsValid=false
    If e.NewRange.IsValid Then

      If Not SelectObject Then
        If rgOld.r1 <> rgNew.r1 Then
          Désélectionner()
          objSélect = mAntagonismes(rgNew.r1 - 1).mGraphique
          SélDésélectionner() ' Montre ou cache les poignées de sélection
          DémarrerCommande(CommandeGraphique.Antagonisme)

        Else
          FenetreAntagonisme.Visible = True
        End If
      End If
    End If

  End Sub

  Private Sub AC1GrilleAntagonismes_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles AC1GrilleAntagonismes.Leave
    Me.FenetreAntagonisme.Hide()
  End Sub

  Private Sub AC1GrilleAntagonismes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AC1GrilleAntagonismes.Click
    VisibilitéFenêtreAntagonisme()
  End Sub
  Private Sub VisibilitéFenêtreAntagonisme()
    If Not Me.FenetreAntagonisme.Visible AndAlso Not IsNothing(objSélect) AndAlso TypeOf objSélect.ObjetMétier Is Antagonisme Then
      Me.FenetreAntagonisme.Visible = True
    End If
  End Sub

  '******************************************************************************
  ' Validation d'un champ de la grille Antagonismes
  '******************************************************************************
  Private Sub AC1GrilleAntagonismes_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) _
  Handles AC1GrilleAntagonismes.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name

    If NomColonne = "ConflitAdmis" Then
      Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

      Dim unAntagonisme As Antagonisme = mAntagonismes(e.Row - 1)
      ' A ce stade, la case à cocher n'est pas encore mis à jour (sera fait dan CellChange!!!) : il faut donc inverser le booléen lors du cochage
      Dim Admis As Boolean = (rg.Checkbox = Grille.CheckEnum.Unchecked)

      Try
        e.Cancel = AntagonismeLiéRefusé(unAntagonisme, Admis, AppelDepuisGrille:=True)

        If e.Cancel AndAlso unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
          Me.FenetreAntagonisme.radNon.Checked = True
        End If


      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  '*******************************************************************************************************************
  ' Grille Antagonismes : Basculement case à cocher Admis , Non Admis
  '*******************************************************************************************************************
  Private Sub AC1GrilleAntagonismes_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles AC1GrilleAntagonismes.CellChanged
    ' Ignorer les changements lors de la construction de la grille
    If AntagonismesEnCours Then Exit Sub

    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name

    If NomColonne <> "ConflitAdmis" Then Exit Sub

    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

    Dim unStyle As Grille.CellStyle = rg.Style
    If unStyle.Name = "Orangé" Then rg.Style = StyleDégrisé
    Dim unAntagonisme As Antagonisme = mAntagonismes(e.Row - 1)
    Dim l1, l2 As LigneFeux
    Static BoucleEnCours As Boolean

    Try

      Dim Admis As Boolean = (rg.Checkbox = Grille.CheckEnum.Checked)
      Me.btnRéinitAntago.Enabled = True

      With unAntagonisme
        'Mettre à jour l'antagonisme
        Me.FenetreAntagonisme.mAntagonisme = Nothing
        If Admis Then
          .TypeConflit = Trajectoire.TypeConflitEnum.Admis
          Me.FenetreAntagonisme.radOui.Checked = True
        Else
          .TypeConflit = Trajectoire.TypeConflitEnum.NonAdmis
          Me.FenetreAntagonisme.radNon.Checked = True
        End If
        VisibilitéFenêtreAntagonisme()
        Me.FenetreAntagonisme.mAntagonisme = unAntagonisme
        'Recréer l'image graphique de l'antagonisme
        .CréerGraphique(colObjetsGraphiques)
        If IsNothing(maVariante.BrancheEnCoursAntagonisme) OrElse maVariante.BrancheEnCoursAntagonisme Is .BrancheCourant1 Then
          DessinerObjet(.mGraphique)
        End If

        'Définir les lignes de feux qui correspondent aux courants en conflit dans cet antagonisme
        l1 = .LigneFeu(Antagonisme.PositionEnum.Premier)
        l2 = .LigneFeu(Antagonisme.PositionEnum.Dernier)

      End With

      monPlanFeuxBase.mLignesFeux.EstIncompatible(l1, l2) = Not Admis

      Dim bclAntago As Antagonisme

      If Not BoucleEnCours Then
        BoucleEnCours = True
        bclAntago = mAntagonismes.AntagonismeTypeconflitIncorrect(l1, l2)
        Do While Not IsNothing(bclAntago)
          MettreAJourConflit(bclAntago, IIf(Admis, Trajectoire.TypeConflitEnum.Admis, Trajectoire.TypeConflitEnum.NonAdmis))
          bclAntago = mAntagonismes.AntagonismeTypeconflitIncorrect(l1, l2)
        Loop
        BoucleEnCours = False
      End If

      'Mettre à jour également les autres conflits portant sur les mêmes courants(qui ne sont pas visibles dans la grille)
      For Each bclAntago In mAntagonismes()
        If (Not (bclAntago Is unAntagonisme)) AndAlso bclAntago.MêmesCourants Is unAntagonisme Then
          MettreAJourConflit(bclAntago, unAntagonisme.TypeConflit)
        End If
      Next

      If mLignesFeux.EstIncompatible(l1, l2) Then
        unStyle = StyleRouge
        ' L'incompatiblilité TD/TAG conduit à l'incompatiblité TAG/Piétons
        For Each unAntagonisme In mAntagonismes.Fils(unAntagonisme)
          MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.NonAdmis)
        Next

      Else
        unStyle = StyleVert
      End If

      fg = Me.Ac1GrilleSécurité
      Dim row, col As Short
      row = mLignesFeux.IndexOf(l1) + 1
      col = mLignesFeux.IndexOf(l2) + 1
      rg = fg.GetCellRange(row, col)
      rg.Style = unStyle
      rg = fg.GetCellRange(col, row)
      rg.Style = unStyle

      Modif = True

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

#End Region
#Region " Grille Phases"
  '**********************************************************************************************************************
  'La cellule a changé de valeur : Déterminer si ce ne génère pas des conflits
  '**********************************************************************************************************************
  Private Sub AC1GrillePhases_CellChanged(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) _
  Handles AC1GrillePhases.CellChanged

    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim row, col As Short

    If AffichagePhasesEnCours Or e.Col = 0 Or e.Row = 0 Then Exit Sub
    AffichagePhasesEnCours = True

    Try
      If PhasageRetenu Then
        rg = fg.GetCellRange(e.Row, e.Col)
        If rg.Checkbox = Grille.CheckEnum.Checked Then
          rg.Checkbox = Grille.CheckEnum.Unchecked
        Else
          rg.Checkbox = Grille.CheckEnum.Checked
        End If

      Else


        'Traiter la colonne modifiée : recherche des incompatibilités
        TraiterOrangé(e.Col)

        'Traiter les autres colonnes
        If Not monPlanPourPhasage.mLigneFeuxMultiPhases Then
          For col = 1 To MaxColPhasage()
            If col <> e.Col Then
              'Décocher la ligne de feux qui vient d'être cochée ou décochée(nb : si elle a été décochée, les autres étaitent déja décochées)
              rg = fg.GetCellRange(e.Row, col)
              rg.Checkbox = Grille.CheckEnum.Unchecked
              'Recherche des incompatibilités dans la colonne
              TraiterOrangé(col)
            End If
          Next
        End If

        DéterminerPhasageCorrect()

      End If
      AffichagePhasesEnCours = False

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub AC1GrillePhases_AfterDragColumn(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.DragRowColEventArgs) _
  Handles AC1GrillePhases.AfterDragColumn
    Dim i As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    For i = 1 To 3
      fg(0, i) = "Phase " & CType(i, String)
    Next
  End Sub

  Private Sub AC1GrillePhases_BeforeDragColumn(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.DragRowColEventArgs) _
  Handles AC1GrillePhases.BeforeDragColumn
    If PhasageRetenu Then
      e.Cancel = True
    End If
  End Sub
#End Region
#End Region
#Region " Enregistrement"
  Public Function Enregistrer() As Boolean

    Try

      If IsNothing(maVariante.NomFichier) Then
        Return EnregistrerSous()
      Else
        If maVariante.Enregistrer() Then
          'L'enregistrement a échoué
          Return True
        Else
          'L'enregistrement a réussi
          Modif = False
        End If
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      Return True
    End Try
  End Function

  Public Function EnregistrerSous() As Boolean
    Dim ExNomFichier As String = maVariante.NomFichier
    Dim NomFichier As String

    If IsNothing(ExNomFichier) Then
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.Enregistrer, Filtre:=ComposerFiltre(etuExtension), DefaultExt:=etuExtension)
    Else
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.Enregistrer, Filtre:=ComposerFiltre(etuExtension), InfoFichier:=New IO.FileInfo(ExNomFichier), DefaultExt:=etuExtension)
    End If

    Try
      If IsNothing(NomFichier) Then
        Return True
      Else
        If cndVariantes.VarianteOuverte(NomFichier, maVariante) Then
          AfficherMessageErreur(Me, "Ce fichier est déjà ouvert")
          Return EnregistrerSous()
        Else
          maVariante.NomFichier = NomFichier
          If maVariante.Enregistrer() Then
            'L'enregistrement a échoué
            maVariante.NomFichier = ExNomFichier
            Return True
          Else
            'L'enregistrement a réussi
            Modif = False
            Me.Text = maVariante.Libellé
            Me.FenetreDiagnostic.Text = "Diagnostic " & Text
            mdiApplication.MRUmenu(NomFichier)
          End If
        End If
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Function

  Public Function SaisirInfoImprim()
    Dim dlg As New dlgInfoImpressions

    With dlg
      .mVariante = maVariante
      If .ShowDialog(Me) = DialogResult.OK Then
        .MettreAJour()
        Modif = True
      End If
    End With

  End Function
#End Region



End Class

'****************************************************************************************************
'Commentaires divers
' le bouton Supprimer provient de VisualStudio\Common7\Graphics\Bitmaps\Assorted\DELETE.BMP
' les boutons Nouveau et Ouvrir proviennent de C:\Program Files\Microsoft Visual Studio .NET 2003\Common7\Graphics\bitmaps\OffCtlBr\Small\Color (ou Large\Color) : .bmp
' Les images de Zoom PAN proviennent de Giration\French\Image2 (.gif)
