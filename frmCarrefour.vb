Imports Grille = C1.Win.C1FlexGrid
Imports System.Math

'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : frmCarrefour.vb										  											'
'						Classes																														'
'							frmCarrefour : Feuille MDIChild                												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe frmCarrefour --------------------------
'Feuille principale de l'application : feuille fille MDI(une feuille par �tude ouverte)
'=====================================================================================================
  Public Class frmCarrefour
    Inherits System.Windows.Forms.Form

#Region " Code g�n�r� par le Concepteur Windows Form "

    Public Sub New()
      MyBase.New()

      'Cet appel est requis par le Concepteur Windows Form.
      InitializeComponent()

      'Ajoutez une initialisation quelconque apr�s l'appel InitializeComponent()
      FonteGras = New Font(Me.tabOnglet.Font, FontStyle.Bold)
    Me.chkTraficR�f�rence.Visible = False
    Me.lblTraficVerrouill�.Visible = False
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
  Friend WithEvents tabPlansDeFeux As System.Windows.Forms.TabPage
  Friend WithEvents tabOnglet As System.Windows.Forms.TabControl
  Friend WithEvents tabTrafics As System.Windows.Forms.TabPage
  Friend WithEvents tabLignesDeFeux As System.Windows.Forms.TabPage
  Friend WithEvents pnlG�om�trie As System.Windows.Forms.Panel
  Friend WithEvents pnlPlansDeFeux As System.Windows.Forms.Panel
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  Friend WithEvents pnlLignesDeFeux As System.Windows.Forms.Panel
  Friend WithEvents lblP�riode As System.Windows.Forms.Label
  Friend WithEvents chkModeTrafic As System.Windows.Forms.CheckBox
  Friend WithEvents radUVP As System.Windows.Forms.RadioButton
  Friend WithEvents rad2Roues As System.Windows.Forms.RadioButton
  Friend WithEvents radPL As System.Windows.Forms.RadioButton
  Friend WithEvents radVL As System.Windows.Forms.RadioButton
  Friend WithEvents cboTrafic As System.Windows.Forms.ComboBox
  Friend WithEvents pnlTrafics As System.Windows.Forms.Panel
  Friend WithEvents pnlTrafic As System.Windows.Forms.Panel
  Friend WithEvents tabConflits As System.Windows.Forms.TabPage
  Friend WithEvents grpPi�ton As System.Windows.Forms.GroupBox
  Friend WithEvents grpV�hicule As System.Windows.Forms.GroupBox
  Friend WithEvents picDessin As System.Windows.Forms.PictureBox
  Friend WithEvents tabG�om�trie As System.Windows.Forms.TabPage
  Friend WithEvents AC1GrilleFeux As GrilleDiagfeux
  Friend WithEvents AC1GrilleBranches As GrilleDiagfeux
  Friend WithEvents btnLigneFeuMonter As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeuDescendre As System.Windows.Forms.Button
  Friend WithEvents splitOngletsPrincipal As System.Windows.Forms.Splitter
  Friend WithEvents splitGraphiqueDonn�es As System.Windows.Forms.Splitter
  Friend WithEvents pnlPhasage As System.Windows.Forms.Panel
  Friend WithEvents btnActionPhase As System.Windows.Forms.Button
  Friend WithEvents lblD�coupagePhases As System.Windows.Forms.Label
  Friend WithEvents pnlFeuBase As System.Windows.Forms.Panel
  Friend WithEvents grpSynchroBase As System.Windows.Forms.GroupBox
  Friend WithEvents lvwDur�eVert As System.Windows.Forms.ListView
  Friend WithEvents grpPhasesBase As System.Windows.Forms.GroupBox
  Friend WithEvents txtDur�eCycleBase As System.Windows.Forms.TextBox
  Friend WithEvents lblPhase3Base As System.Windows.Forms.Label
  Friend WithEvents lblPhase2Base As System.Windows.Forms.Label
  Friend WithEvents lblPhase1Base As System.Windows.Forms.Label
  Friend WithEvents updPhase3Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase2Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase1Base As System.Windows.Forms.NumericUpDown
  Friend WithEvents lvwcolLF As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDur�e As System.Windows.Forms.ColumnHeader
  Friend WithEvents pnlFeuFonctionnement As System.Windows.Forms.Panel
  Friend WithEvents grpPhasesFct As System.Windows.Forms.GroupBox
  Friend WithEvents txtDur�eCycleFct As System.Windows.Forms.TextBox
  Friend WithEvents lblPhase3Fct As System.Windows.Forms.Label
  Friend WithEvents lblPhase2Fct As System.Windows.Forms.Label
  Friend WithEvents lblPhase1Fct As System.Windows.Forms.Label
  Friend WithEvents updPhase3Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase2Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updPhase1Fct As System.Windows.Forms.NumericUpDown
  Friend WithEvents chkVerrouFeuBase As System.Windows.Forms.CheckBox
  Friend WithEvents lblCarrefourCompos� As System.Windows.Forms.Label
  Friend WithEvents cboCarrefourCompos� As System.Windows.Forms.ComboBox
  Friend WithEvents lvwcolD�calOuverture As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolD�calFermeture As System.Windows.Forms.ColumnHeader
  Friend WithEvents splitCarrefourCompos� As System.Windows.Forms.Splitter
  Friend WithEvents pnlCarrefourCompos� As System.Windows.Forms.Panel
  Friend WithEvents lvwDur�eVertFct As System.Windows.Forms.ListView
  Friend WithEvents lvwcolLFFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolDur�eFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolD�calOuvertureFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolD�calFermetureFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents updD�calageFermetureFct As System.Windows.Forms.NumericUpDown
  Friend WithEvents updD�calageOuvertureFct As System.Windows.Forms.NumericUpDown
  Friend WithEvents cboPlansDeFeux As System.Windows.Forms.ComboBox
  Friend WithEvents lblTrafic As System.Windows.Forms.Label
  Friend WithEvents lblPlansDeFeux As System.Windows.Forms.Label
  Friend WithEvents btnDupliquerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents btnSupprimerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents grpSynchroFct As System.Windows.Forms.GroupBox
  Friend WithEvents updD�calageFermetureBase As System.Windows.Forms.NumericUpDown
  Friend WithEvents updD�calageOuvertureBase As System.Windows.Forms.NumericUpDown
  Friend WithEvents btnRenommerPlanFeux As System.Windows.Forms.Button
  Friend WithEvents cboD�coupagePhases As System.Windows.Forms.ComboBox
  Friend WithEvents radPhase1Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase2Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase3Fct As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase3Base As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase2Base As System.Windows.Forms.RadioButton
  Friend WithEvents radPhase1Base As System.Windows.Forms.RadioButton
  Friend WithEvents btnSupprimerTrafic As System.Windows.Forms.Button
  Friend WithEvents btnNouveauTrafic As System.Windows.Forms.Button
  Friend WithEvents btnRenommerTrafic As System.Windows.Forms.Button
  Friend WithEvents Ac1GrilleTraficPi�tons As DiagFeux.GrilleDiagfeux
  Friend WithEvents chkTraficR�f�rence As System.Windows.Forms.CheckBox
  Friend WithEvents pnlBoutonsLignesFeux As System.Windows.Forms.Panel
  Friend WithEvents lblD�calages As System.Windows.Forms.Label
  Friend WithEvents lblCycle As System.Windows.Forms.Label
  Friend WithEvents lblD�calagesFct As System.Windows.Forms.Label
  Friend WithEvents lblCycleFct As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleTraficV�hicules As DiagFeux.GrilleDiagfeux
  Friend WithEvents pnlConflits As System.Windows.Forms.Panel
  Friend WithEvents Ac1GrilleS�curit� As DiagFeux.GrilleDiagfeux
  Friend WithEvents pnlVerrouMatrice As System.Windows.Forms.Panel
  Friend WithEvents lbImgSansConflit As System.Windows.Forms.Label
  Friend WithEvents lblImgConflit As System.Windows.Forms.Label
  Friend WithEvents pnlImgSansConflit As System.Windows.Forms.Panel
  Friend WithEvents pnlImgConflit As System.Windows.Forms.Panel
  Friend WithEvents chkVerrouMatrice As System.Windows.Forms.CheckBox
  'Friend WithEvents lblR�serveCapacit�Uvpd As System.Windows.Forms.Label
  'Friend WithEvents lblR�serveCapacit�Th�orique As System.Windows.Forms.Label
  'Friend WithEvents txtR�serveCapacit�Th�orique As System.Windows.Forms.TextBox
  Friend WithEvents btnPi�tonPlus As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents btnCarrefour As System.Windows.Forms.Button
  Friend WithEvents btnPi�tonMoins As System.Windows.Forms.Button
  Friend WithEvents lblPassage As System.Windows.Forms.Label
  Friend WithEvents pnlBtnG�om�trie As System.Windows.Forms.Panel
  Friend WithEvents pnlIlots As System.Windows.Forms.Panel
  Friend WithEvents lblIlot As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleIlot As DiagFeux.GrilleDiagfeux
  Friend WithEvents tipPicDessin As System.Windows.Forms.ToolTip
  Friend WithEvents lblLigneFeu As System.Windows.Forms.Label
  Friend WithEvents btnSignalMoins As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents AC1GrilleAntagonismes As DiagFeux.GrilleDiagfeux
  Friend WithEvents btnTravProp As System.Windows.Forms.Button
  Friend WithEvents lblTravers�e As System.Windows.Forms.Label
  Friend WithEvents btnTrajProp As System.Windows.Forms.Button
  Friend WithEvents lblTrajVehicule As System.Windows.Forms.Label
  Friend WithEvents pnlTrajectoires As System.Windows.Forms.Panel
  Friend WithEvents radMatriceInterverts As System.Windows.Forms.RadioButton
  Friend WithEvents radMatriceRougesD�gagement As System.Windows.Forms.RadioButton
  Friend WithEvents radMatriceConflits As System.Windows.Forms.RadioButton
  Friend WithEvents pnlMatricesS�curit� As System.Windows.Forms.Panel
  Friend WithEvents pnlAntagonismes As System.Windows.Forms.Panel
  Friend WithEvents cboBrancheCourant1 As System.Windows.Forms.ComboBox
  Friend WithEvents lblCourantOrigine As System.Windows.Forms.Label
  Friend WithEvents lblTriLignesFeux As System.Windows.Forms.Label
  Friend WithEvents cboTriLignesFeux As System.Windows.Forms.ComboBox
  Friend WithEvents lbFigerDur�eBase As System.Windows.Forms.Label
  Friend WithEvents cboM�thodeCalculCycle As System.Windows.Forms.ComboBox
  Friend WithEvents lblM�thodeCalculCycle As System.Windows.Forms.Label
  Friend WithEvents lbFigerDur�eFct As System.Windows.Forms.Label
  Friend WithEvents btnCalculerCycle As System.Windows.Forms.Button
  Friend WithEvents lblR�servCapacit�Choisie As System.Windows.Forms.Label
  Friend WithEvents cboR�serveCapacit�Choisie As System.Windows.Forms.ComboBox
  Friend WithEvents btnTrajToutes As System.Windows.Forms.Button
  Friend WithEvents lblUVP As System.Windows.Forms.Label
  'Friend WithEvents pnlCapacit�Th�orique As System.Windows.Forms.Panel
  Friend WithEvents radPhasage As System.Windows.Forms.RadioButton
  Friend WithEvents radFeuBase As System.Windows.Forms.RadioButton
  Friend WithEvents radFeuFonctionnement As System.Windows.Forms.RadioButton
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lblTraficVerrouill� As System.Windows.Forms.Label
  Friend WithEvents lblCommentaireP�riode As System.Windows.Forms.Label
  Friend WithEvents txtCommentaireP�riode As System.Windows.Forms.TextBox
  Friend WithEvents btnDiagnostic As System.Windows.Forms.Button
  Friend WithEvents chkVerrouG�om�trie As System.Windows.Forms.CheckBox
  Friend WithEvents chkVerrouLignesFeux As System.Windows.Forms.CheckBox
  'Friend WithEvents lblSecondesDur�eCycle As System.Windows.Forms.Label
  'Friend WithEvents txtDur�eCycle As System.Windows.Forms.TextBox
  'Friend WithEvents lblDur�eCycle As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblV�hiculeBase As System.Windows.Forms.Label
  Friend WithEvents lblPi�tonBase As System.Windows.Forms.Label
  Friend WithEvents txtVertMiniPi�ton As System.Windows.Forms.TextBox
  Friend WithEvents txtVertMiniV�hicule As System.Windows.Forms.TextBox
  Friend WithEvents lblVertMini As System.Windows.Forms.Label
  Friend WithEvents btnDupliquerTrafic As System.Windows.Forms.Button
  Friend WithEvents chkVerrouP�riode As System.Windows.Forms.CheckBox
  Friend WithEvents pnlFiltrePhasage As System.Windows.Forms.Panel
  Friend WithEvents lblR�serveCapacit� As System.Windows.Forms.Label
  Friend WithEvents chk3Phases As System.Windows.Forms.CheckBox
  Friend WithEvents pnlBoutonsLignesFeuxPlans As System.Windows.Forms.Panel
  Friend WithEvents cboTriLignesFeuxPlans As System.Windows.Forms.ComboBox
  Friend WithEvents lblTriLignesFeuxPlans As System.Windows.Forms.Label
  Friend WithEvents btnLigneFeuDescendrePlans As System.Windows.Forms.Button
  Friend WithEvents btnLigneFeuMonterPlans As System.Windows.Forms.Button
  Friend WithEvents chkSensTrajectoires As System.Windows.Forms.CheckBox
  Friend WithEvents btnR�initAntago As System.Windows.Forms.Button
  Friend WithEvents pnlBoutonsRouges As System.Windows.Forms.Panel
  Friend WithEvents btnRougesD�faut As System.Windows.Forms.Button
  Friend WithEvents btnRougeD�faut As System.Windows.Forms.Button
  Friend WithEvents lblBoutonsRouges As System.Windows.Forms.Label
  Friend WithEvents lvwcolPhase As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvwcolPhaseFct As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnPi�tonPlusRapide As System.Windows.Forms.Button
  Friend WithEvents lblLFMultiPhases As System.Windows.Forms.Label
  Friend WithEvents cbolLFMultiPhases As System.Windows.Forms.ComboBox
  Friend WithEvents lblPhasesSp�ciales As System.Windows.Forms.Label
  Friend WithEvents cboPhasesSp�ciales As System.Windows.Forms.ComboBox
  Friend WithEvents cboR�serveCapacit� As System.Windows.Forms.ComboBox
  'Friend WithEvents txtRPourCent As System.Windows.Forms.TextBox
  Friend WithEvents txtR�serveCapacit�PourCent As System.Windows.Forms.TextBox
  Friend WithEvents lblTaficSatur� As System.Windows.Forms.Label
  Friend WithEvents pnlTableauPhasage As System.Windows.Forms.Panel
  Friend WithEvents AC1GrillePhases As DiagFeux.GrilleDiagfeux
  Friend WithEvents lblConflitPotentiel As System.Windows.Forms.Label
  Friend WithEvents pnlConflitPotentiel As System.Windows.Forms.Panel
  Friend WithEvents chkD�coupagePhases As System.Windows.Forms.CheckBox
  Friend WithEvents chkSc�narioD�finitif As System.Windows.Forms.CheckBox
  Friend WithEvents lblTraficFct As System.Windows.Forms.Label
  Friend WithEvents cboTraficFct As System.Windows.Forms.ComboBox
  Friend WithEvents btnTrajMoinsTout As System.Windows.Forms.Button
  Friend WithEvents btnTravers�eMoins As System.Windows.Forms.Button
  Friend WithEvents btnTravers�e As System.Windows.Forms.Button
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
        Me.tabG�om�trie = New System.Windows.Forms.TabPage
        Me.tabLignesDeFeux = New System.Windows.Forms.TabPage
        Me.tabTrafics = New System.Windows.Forms.TabPage
        Me.tabConflits = New System.Windows.Forms.TabPage
        Me.splitOngletsPrincipal = New System.Windows.Forms.Splitter
        Me.splitGraphiqueDonn�es = New System.Windows.Forms.Splitter
        Me.pnlG�om�trie = New System.Windows.Forms.Panel
        Me.pnlIlots = New System.Windows.Forms.Panel
        Me.AC1GrilleIlot = New DiagFeux.GrilleDiagfeux
        Me.lblIlot = New System.Windows.Forms.Label
        Me.pnlBtnG�om�trie = New System.Windows.Forms.Panel
        Me.btnPi�tonPlusRapide = New System.Windows.Forms.Button
        Me.chkVerrouG�om�trie = New System.Windows.Forms.CheckBox
        Me.btnPi�tonPlus = New System.Windows.Forms.Button
        Me.btnCarrefour = New System.Windows.Forms.Button
        Me.btnPi�tonMoins = New System.Windows.Forms.Button
        Me.lblPassage = New System.Windows.Forms.Label
        Me.AC1GrilleBranches = New DiagFeux.GrilleDiagfeux
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlLignesDeFeux = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlTrajectoires = New System.Windows.Forms.Panel
        Me.chkSensTrajectoires = New System.Windows.Forms.CheckBox
        Me.btnTrajMoinsTout = New System.Windows.Forms.Button
        Me.btnTrajToutes = New System.Windows.Forms.Button
        Me.btnTravers�eMoins = New System.Windows.Forms.Button
        Me.btnTravProp = New System.Windows.Forms.Button
        Me.btnTravers�e = New System.Windows.Forms.Button
        Me.lblTravers�e = New System.Windows.Forms.Label
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
        Me.lblTaficSatur� = New System.Windows.Forms.Label
        Me.chkVerrouP�riode = New System.Windows.Forms.CheckBox
        Me.txtCommentaireP�riode = New System.Windows.Forms.TextBox
        Me.lblCommentaireP�riode = New System.Windows.Forms.Label
        Me.lblTraficVerrouill� = New System.Windows.Forms.Label
        Me.btnSupprimerTrafic = New System.Windows.Forms.Button
        Me.btnNouveauTrafic = New System.Windows.Forms.Button
        Me.btnRenommerTrafic = New System.Windows.Forms.Button
        Me.btnDupliquerTrafic = New System.Windows.Forms.Button
        Me.chkTraficR�f�rence = New System.Windows.Forms.CheckBox
        Me.lblP�riode = New System.Windows.Forms.Label
        Me.grpPi�ton = New System.Windows.Forms.GroupBox
        Me.Ac1GrilleTraficPi�tons = New DiagFeux.GrilleDiagfeux
        Me.chkModeTrafic = New System.Windows.Forms.CheckBox
        Me.grpV�hicule = New System.Windows.Forms.GroupBox
        Me.AC1GrilleTraficV�hicules = New DiagFeux.GrilleDiagfeux
        Me.pnlTrafic = New System.Windows.Forms.Panel
        Me.radUVP = New System.Windows.Forms.RadioButton
        Me.rad2Roues = New System.Windows.Forms.RadioButton
        Me.radPL = New System.Windows.Forms.RadioButton
        Me.radVL = New System.Windows.Forms.RadioButton
        Me.lblUVP = New System.Windows.Forms.Label
        Me.cboTrafic = New System.Windows.Forms.ComboBox
        Me.pnlPlansDeFeux = New System.Windows.Forms.Panel
        Me.pnlCarrefourCompos� = New System.Windows.Forms.Panel
        Me.chkSc�narioD�finitif = New System.Windows.Forms.CheckBox
        Me.radFeuFonctionnement = New System.Windows.Forms.RadioButton
        Me.radFeuBase = New System.Windows.Forms.RadioButton
        Me.radPhasage = New System.Windows.Forms.RadioButton
        Me.cboCarrefourCompos� = New System.Windows.Forms.ComboBox
        Me.lblCarrefourCompos� = New System.Windows.Forms.Label
        Me.splitCarrefourCompos� = New System.Windows.Forms.Splitter
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
        Me.cboR�serveCapacit�Choisie = New System.Windows.Forms.ComboBox
        Me.lblR�servCapacit�Choisie = New System.Windows.Forms.Label
        Me.btnCalculerCycle = New System.Windows.Forms.Button
        Me.cboM�thodeCalculCycle = New System.Windows.Forms.ComboBox
        Me.lblM�thodeCalculCycle = New System.Windows.Forms.Label
        Me.radPhase3Fct = New System.Windows.Forms.RadioButton
        Me.radPhase2Fct = New System.Windows.Forms.RadioButton
        Me.radPhase1Fct = New System.Windows.Forms.RadioButton
        Me.lbFigerDur�eFct = New System.Windows.Forms.Label
        Me.txtDur�eCycleFct = New System.Windows.Forms.TextBox
        Me.lblCycleFct = New System.Windows.Forms.Label
        Me.lblPhase3Fct = New System.Windows.Forms.Label
        Me.lblPhase2Fct = New System.Windows.Forms.Label
        Me.lblPhase1Fct = New System.Windows.Forms.Label
        Me.updPhase3Fct = New System.Windows.Forms.NumericUpDown
        Me.updPhase2Fct = New System.Windows.Forms.NumericUpDown
        Me.updPhase1Fct = New System.Windows.Forms.NumericUpDown
        Me.grpSynchroFct = New System.Windows.Forms.GroupBox
        Me.lblD�calagesFct = New System.Windows.Forms.Label
        Me.updD�calageFermetureFct = New System.Windows.Forms.NumericUpDown
        Me.updD�calageOuvertureFct = New System.Windows.Forms.NumericUpDown
        Me.lvwDur�eVertFct = New System.Windows.Forms.ListView
        Me.lvwcolLFFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolPhaseFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDur�eFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolD�calOuvertureFct = New System.Windows.Forms.ColumnHeader
        Me.lvwcolD�calFermetureFct = New System.Windows.Forms.ColumnHeader
        Me.pnlPhasage = New System.Windows.Forms.Panel
        Me.pnlTableauPhasage = New System.Windows.Forms.Panel
        Me.lblConflitPotentiel = New System.Windows.Forms.Label
        Me.pnlConflitPotentiel = New System.Windows.Forms.Panel
        Me.chkD�coupagePhases = New System.Windows.Forms.CheckBox
        Me.AC1GrillePhases = New DiagFeux.GrilleDiagfeux
        Me.pnlFiltrePhasage = New System.Windows.Forms.Panel
        Me.cboD�coupagePhases = New System.Windows.Forms.ComboBox
        Me.txtR�serveCapacit�PourCent = New System.Windows.Forms.TextBox
        Me.cboPhasesSp�ciales = New System.Windows.Forms.ComboBox
        Me.cbolLFMultiPhases = New System.Windows.Forms.ComboBox
        Me.lblPhasesSp�ciales = New System.Windows.Forms.Label
        Me.lblLFMultiPhases = New System.Windows.Forms.Label
        Me.lblR�serveCapacit� = New System.Windows.Forms.Label
        Me.cboR�serveCapacit� = New System.Windows.Forms.ComboBox
        Me.chk3Phases = New System.Windows.Forms.CheckBox
        Me.btnActionPhase = New System.Windows.Forms.Button
        Me.lblD�coupagePhases = New System.Windows.Forms.Label
        Me.pnlFeuBase = New System.Windows.Forms.Panel
        Me.lblV�hiculeBase = New System.Windows.Forms.Label
        Me.lblPi�tonBase = New System.Windows.Forms.Label
        Me.txtVertMiniPi�ton = New System.Windows.Forms.TextBox
        Me.txtVertMiniV�hicule = New System.Windows.Forms.TextBox
        Me.lblVertMini = New System.Windows.Forms.Label
        Me.grpSynchroBase = New System.Windows.Forms.GroupBox
        Me.lblD�calages = New System.Windows.Forms.Label
        Me.updD�calageFermetureBase = New System.Windows.Forms.NumericUpDown
        Me.updD�calageOuvertureBase = New System.Windows.Forms.NumericUpDown
        Me.lvwDur�eVert = New System.Windows.Forms.ListView
        Me.lvwcolLF = New System.Windows.Forms.ColumnHeader
        Me.lvwcolPhase = New System.Windows.Forms.ColumnHeader
        Me.lvwcolDur�e = New System.Windows.Forms.ColumnHeader
        Me.lvwcolD�calOuverture = New System.Windows.Forms.ColumnHeader
        Me.lvwcolD�calFermeture = New System.Windows.Forms.ColumnHeader
        Me.grpPhasesBase = New System.Windows.Forms.GroupBox
        Me.radPhase3Base = New System.Windows.Forms.RadioButton
        Me.radPhase2Base = New System.Windows.Forms.RadioButton
        Me.radPhase1Base = New System.Windows.Forms.RadioButton
        Me.lbFigerDur�eBase = New System.Windows.Forms.Label
        Me.txtDur�eCycleBase = New System.Windows.Forms.TextBox
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
        Me.btnRougeD�faut = New System.Windows.Forms.Button
        Me.btnRougesD�faut = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlAntagonismes = New System.Windows.Forms.Panel
        Me.btnR�initAntago = New System.Windows.Forms.Button
        Me.cboBrancheCourant1 = New System.Windows.Forms.ComboBox
        Me.lblCourantOrigine = New System.Windows.Forms.Label
        Me.AC1GrilleAntagonismes = New DiagFeux.GrilleDiagfeux
        Me.pnlMatricesS�curit� = New System.Windows.Forms.Panel
        Me.radMatriceInterverts = New System.Windows.Forms.RadioButton
        Me.radMatriceRougesD�gagement = New System.Windows.Forms.RadioButton
        Me.radMatriceConflits = New System.Windows.Forms.RadioButton
        Me.pnlVerrouMatrice = New System.Windows.Forms.Panel
        Me.lbImgSansConflit = New System.Windows.Forms.Label
        Me.lblImgConflit = New System.Windows.Forms.Label
        Me.pnlImgSansConflit = New System.Windows.Forms.Panel
        Me.pnlImgConflit = New System.Windows.Forms.Panel
        Me.chkVerrouMatrice = New System.Windows.Forms.CheckBox
        Me.Ac1GrilleS�curit� = New DiagFeux.GrilleDiagfeux
        Me.tipPicDessin = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabOnglet.SuspendLayout()
        Me.pnlG�om�trie.SuspendLayout()
        Me.pnlIlots.SuspendLayout()
        CType(Me.AC1GrilleIlot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBtnG�om�trie.SuspendLayout()
        CType(Me.AC1GrilleBranches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLignesDeFeux.SuspendLayout()
        Me.pnlTrajectoires.SuspendLayout()
        CType(Me.AC1GrilleFeux, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBoutonsLignesFeux.SuspendLayout()
        Me.pnlTrafics.SuspendLayout()
        Me.grpPi�ton.SuspendLayout()
        CType(Me.Ac1GrilleTraficPi�tons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpV�hicule.SuspendLayout()
        CType(Me.AC1GrilleTraficV�hicules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTrafic.SuspendLayout()
        Me.pnlPlansDeFeux.SuspendLayout()
        Me.pnlCarrefourCompos�.SuspendLayout()
        Me.pnlFeuFonctionnement.SuspendLayout()
        Me.grpPhasesFct.SuspendLayout()
        CType(Me.updPhase3Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase2Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updPhase1Fct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSynchroFct.SuspendLayout()
        CType(Me.updD�calageFermetureFct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updD�calageOuvertureFct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPhasage.SuspendLayout()
        Me.pnlTableauPhasage.SuspendLayout()
        CType(Me.AC1GrillePhases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiltrePhasage.SuspendLayout()
        Me.pnlFeuBase.SuspendLayout()
        Me.grpSynchroBase.SuspendLayout()
        CType(Me.updD�calageFermetureBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updD�calageOuvertureBase, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlMatricesS�curit�.SuspendLayout()
        Me.pnlVerrouMatrice.SuspendLayout()
        CType(Me.Ac1GrilleS�curit�, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tabOnglet.Controls.Add(Me.tabG�om�trie)
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
        'tabG�om�trie
        '
        Me.tabG�om�trie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tabG�om�trie.Location = New System.Drawing.Point(4, 22)
        Me.tabG�om�trie.Name = "tabG�om�trie"
        Me.tabG�om�trie.Size = New System.Drawing.Size(904, 0)
        Me.tabG�om�trie.TabIndex = 0
        Me.tabG�om�trie.Text = "G�om�trie"
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
        'splitGraphiqueDonn�es
        '
        Me.splitGraphiqueDonn�es.Location = New System.Drawing.Point(288, 27)
        Me.splitGraphiqueDonn�es.MinExtra = 100
        Me.splitGraphiqueDonn�es.MinSize = 200
        Me.splitGraphiqueDonn�es.Name = "splitGraphiqueDonn�es"
        Me.splitGraphiqueDonn�es.Size = New System.Drawing.Size(8, 570)
        Me.splitGraphiqueDonn�es.TabIndex = 6
        Me.splitGraphiqueDonn�es.TabStop = False
        '
        'pnlG�om�trie
        '
        Me.pnlG�om�trie.AutoScroll = True
        Me.pnlG�om�trie.AutoScrollMinSize = New System.Drawing.Size(440, 150)
        Me.pnlG�om�trie.Controls.Add(Me.pnlIlots)
        Me.pnlG�om�trie.Controls.Add(Me.pnlBtnG�om�trie)
        Me.pnlG�om�trie.Controls.Add(Me.AC1GrilleBranches)
        Me.pnlG�om�trie.Controls.Add(Me.Label1)
        Me.pnlG�om�trie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlG�om�trie.Location = New System.Drawing.Point(0, 0)
        Me.pnlG�om�trie.Name = "pnlG�om�trie"
        Me.pnlG�om�trie.Size = New System.Drawing.Size(912, 597)
        Me.pnlG�om�trie.TabIndex = 7
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
        'pnlBtnG�om�trie
        '
        Me.pnlBtnG�om�trie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBtnG�om�trie.Controls.Add(Me.btnPi�tonPlusRapide)
        Me.pnlBtnG�om�trie.Controls.Add(Me.chkVerrouG�om�trie)
        Me.pnlBtnG�om�trie.Controls.Add(Me.btnPi�tonPlus)
        Me.pnlBtnG�om�trie.Controls.Add(Me.btnCarrefour)
        Me.pnlBtnG�om�trie.Controls.Add(Me.btnPi�tonMoins)
        Me.pnlBtnG�om�trie.Controls.Add(Me.lblPassage)
        Me.pnlBtnG�om�trie.Location = New System.Drawing.Point(744, 120)
        Me.pnlBtnG�om�trie.Name = "pnlBtnG�om�trie"
        Me.pnlBtnG�om�trie.Size = New System.Drawing.Size(160, 192)
        Me.pnlBtnG�om�trie.TabIndex = 24
        '
        'btnPi�tonPlusRapide
        '
        Me.btnPi�tonPlusRapide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPi�tonPlusRapide.Image = CType(resources.GetObject("btnPi�tonPlusRapide.Image"), System.Drawing.Image)
        Me.btnPi�tonPlusRapide.Location = New System.Drawing.Point(48, 16)
        Me.btnPi�tonPlusRapide.Name = "btnPi�tonPlusRapide"
        Me.btnPi�tonPlusRapide.Size = New System.Drawing.Size(24, 24)
        Me.btnPi�tonPlusRapide.TabIndex = 34
        Me.tipBulle.SetToolTip(Me.btnPi�tonPlusRapide, "Cr�er un passage rapidement")
        '
        'chkVerrouG�om�trie
        '
        Me.chkVerrouG�om�trie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouG�om�trie.Location = New System.Drawing.Point(48, 112)
        Me.chkVerrouG�om�trie.Name = "chkVerrouG�om�trie"
        Me.chkVerrouG�om�trie.Size = New System.Drawing.Size(88, 32)
        Me.chkVerrouG�om�trie.TabIndex = 27
        Me.chkVerrouG�om�trie.Tag = "1"
        Me.chkVerrouG�om�trie.Text = "Verrouiller la g�om�trie"
        Me.tipBulle.SetToolTip(Me.chkVerrouG�om�trie, "Verrouiller la g�om�trie")
        '
        'btnPi�tonPlus
        '
        Me.btnPi�tonPlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPi�tonPlus.Image = CType(resources.GetObject("btnPi�tonPlus.Image"), System.Drawing.Image)
        Me.btnPi�tonPlus.Location = New System.Drawing.Point(88, 16)
        Me.btnPi�tonPlus.Name = "btnPi�tonPlus"
        Me.btnPi�tonPlus.Size = New System.Drawing.Size(24, 24)
        Me.btnPi�tonPlus.TabIndex = 33
        Me.tipBulle.SetToolTip(Me.btnPi�tonPlus, "Cr�er un passage point par point")
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
        'btnPi�tonMoins
        '
        Me.btnPi�tonMoins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPi�tonMoins.Image = CType(resources.GetObject("btnPi�tonMoins.Image"), System.Drawing.Image)
        Me.btnPi�tonMoins.Location = New System.Drawing.Point(128, 16)
        Me.btnPi�tonMoins.Name = "btnPi�tonMoins"
        Me.btnPi�tonMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnPi�tonMoins.TabIndex = 30
        Me.tipBulle.SetToolTip(Me.btnPi�tonMoins, "Supprimer un passage")
        '
        'lblPassage
        '
        Me.lblPassage.Location = New System.Drawing.Point(0, 8)
        Me.lblPassage.Name = "lblPassage"
        Me.lblPassage.Size = New System.Drawing.Size(56, 32)
        Me.lblPassage.TabIndex = 28
        Me.lblPassage.Text = "Passage pi�tons"
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
        Me.pnlTrajectoires.Controls.Add(Me.btnTravers�eMoins)
        Me.pnlTrajectoires.Controls.Add(Me.btnTravProp)
        Me.pnlTrajectoires.Controls.Add(Me.btnTravers�e)
        Me.pnlTrajectoires.Controls.Add(Me.lblTravers�e)
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
        Me.tipBulle.SetToolTip(Me.btnTrajToutes, "G�n�rer toutes les trajectoires")
        '
        'btnTravers�eMoins
        '
        Me.btnTravers�eMoins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTravers�eMoins.Image = CType(resources.GetObject("btnTravers�eMoins.Image"), System.Drawing.Image)
        Me.btnTravers�eMoins.Location = New System.Drawing.Point(136, 56)
        Me.btnTravers�eMoins.Name = "btnTravers�eMoins"
        Me.btnTravers�eMoins.Size = New System.Drawing.Size(24, 24)
        Me.btnTravers�eMoins.TabIndex = 26
        Me.tipBulle.SetToolTip(Me.btnTravers�eMoins, "Supprimer une travers�e")
        '
        'btnTravProp
        '
        Me.btnTravProp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTravProp.Image = CType(resources.GetObject("btnTravProp.Image"), System.Drawing.Image)
        Me.btnTravProp.Location = New System.Drawing.Point(176, 56)
        Me.btnTravProp.Name = "btnTravProp"
        Me.btnTravProp.Size = New System.Drawing.Size(24, 24)
        Me.btnTravProp.TabIndex = 25
        Me.tipBulle.SetToolTip(Me.btnTravProp, "Caract�ristiques de la travers�e")
        '
        'btnTravers�e
        '
        Me.btnTravers�e.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTravers�e.Image = CType(resources.GetObject("btnTravers�e.Image"), System.Drawing.Image)
        Me.btnTravers�e.Location = New System.Drawing.Point(96, 56)
        Me.btnTravers�e.Name = "btnTravers�e"
        Me.btnTravers�e.Size = New System.Drawing.Size(24, 24)
        Me.btnTravers�e.TabIndex = 24
        Me.tipBulle.SetToolTip(Me.btnTravers�e, "Cr�er une travers�e")
        '
        'lblTravers�e
        '
        Me.lblTravers�e.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTravers�e.Location = New System.Drawing.Point(16, 48)
        Me.lblTravers�e.Name = "lblTravers�e"
        Me.lblTravers�e.Size = New System.Drawing.Size(64, 32)
        Me.lblTravers�e.TabIndex = 23
        Me.lblTravers�e.Text = "Travers�e pi�tonne"
        '
        'btnTrajProp
        '
        Me.btnTrajProp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrajProp.Image = CType(resources.GetObject("btnTrajProp.Image"), System.Drawing.Image)
        Me.btnTrajProp.Location = New System.Drawing.Point(176, 16)
        Me.btnTrajProp.Name = "btnTrajProp"
        Me.btnTrajProp.Size = New System.Drawing.Size(24, 24)
        Me.btnTrajProp.TabIndex = 22
        Me.tipBulle.SetToolTip(Me.btnTrajProp, "Caract�ristiques de la trajectoire")
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
        Me.lblTrajVehicule.Text = "Trajectoire v�hicules"
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
        Me.tipBulle.SetToolTip(Me.btnLigneFeux, "Cr�er une ligne de feux")
        '
        'cboTriLignesFeux
        '
        Me.cboTriLignesFeux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriLignesFeux.Items.AddRange(New Object() {"Manuel", "Feux V�hicules en t�te", "Par Branche", "Par nom de feux"})
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
        Me.pnlTrafics.Controls.Add(Me.lblTaficSatur�)
        Me.pnlTrafics.Controls.Add(Me.chkVerrouP�riode)
        Me.pnlTrafics.Controls.Add(Me.txtCommentaireP�riode)
        Me.pnlTrafics.Controls.Add(Me.lblCommentaireP�riode)
        Me.pnlTrafics.Controls.Add(Me.lblTraficVerrouill�)
        Me.pnlTrafics.Controls.Add(Me.btnSupprimerTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnNouveauTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnRenommerTrafic)
        Me.pnlTrafics.Controls.Add(Me.btnDupliquerTrafic)
        Me.pnlTrafics.Controls.Add(Me.chkTraficR�f�rence)
        Me.pnlTrafics.Controls.Add(Me.lblP�riode)
        Me.pnlTrafics.Controls.Add(Me.grpPi�ton)
        Me.pnlTrafics.Controls.Add(Me.chkModeTrafic)
        Me.pnlTrafics.Controls.Add(Me.grpV�hicule)
        Me.pnlTrafics.Controls.Add(Me.cboTrafic)
        Me.pnlTrafics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTrafics.Location = New System.Drawing.Point(0, 0)
        Me.pnlTrafics.Name = "pnlTrafics"
        Me.pnlTrafics.Size = New System.Drawing.Size(912, 597)
        Me.pnlTrafics.TabIndex = 10
        '
        'lblTaficSatur�
        '
        Me.lblTaficSatur�.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTaficSatur�.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaficSatur�.ForeColor = System.Drawing.Color.Red
        Me.lblTaficSatur�.Location = New System.Drawing.Point(600, 520)
        Me.lblTaficSatur�.Name = "lblTaficSatur�"
        Me.lblTaficSatur�.Size = New System.Drawing.Size(168, 32)
        Me.lblTaficSatur�.TabIndex = 65
        Me.lblTaficSatur�.Text = "Les trafics sont sup�rieurs au d�bit de saturation"
        Me.lblTaficSatur�.Visible = False
        '
        'chkVerrouP�riode
        '
        Me.chkVerrouP�riode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVerrouP�riode.Location = New System.Drawing.Point(608, 448)
        Me.chkVerrouP�riode.Name = "chkVerrouP�riode"
        Me.chkVerrouP�riode.Size = New System.Drawing.Size(136, 24)
        Me.chkVerrouP�riode.TabIndex = 64
        Me.chkVerrouP�riode.Text = "Verrouiller la p�riode"
        '
        'txtCommentaireP�riode
        '
        Me.txtCommentaireP�riode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommentaireP�riode.Location = New System.Drawing.Point(696, 400)
        Me.txtCommentaireP�riode.Multiline = True
        Me.txtCommentaireP�riode.Name = "txtCommentaireP�riode"
        Me.txtCommentaireP�riode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCommentaireP�riode.Size = New System.Drawing.Size(200, 32)
        Me.txtCommentaireP�riode.TabIndex = 63
        '
        'lblCommentaireP�riode
        '
        Me.lblCommentaireP�riode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommentaireP�riode.Location = New System.Drawing.Point(608, 400)
        Me.lblCommentaireP�riode.Name = "lblCommentaireP�riode"
        Me.lblCommentaireP�riode.Size = New System.Drawing.Size(80, 28)
        Me.lblCommentaireP�riode.TabIndex = 62
        Me.lblCommentaireP�riode.Text = "Commentaires sur la p�riode"
        '
        'lblTraficVerrouill�
        '
        Me.lblTraficVerrouill�.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTraficVerrouill�.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTraficVerrouill�.ForeColor = System.Drawing.Color.Red
        Me.lblTraficVerrouill�.Location = New System.Drawing.Point(600, 520)
        Me.lblTraficVerrouill�.Name = "lblTraficVerrouill�"
        Me.lblTraficVerrouill�.Size = New System.Drawing.Size(272, 32)
        Me.lblTraficVerrouill�.TabIndex = 61
        Me.lblTraficVerrouill�.Text = "Les conflits sont verrouill�s                                  La p�riode de r�f�" & _
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
        Me.tipBulle.SetToolTip(Me.btnDupliquerTrafic, "Positionner les indications de trafic sur le sch�ma")
        '
        'chkTraficR�f�rence
        '
        Me.chkTraficR�f�rence.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTraficR�f�rence.Location = New System.Drawing.Point(768, 56)
        Me.chkTraficR�f�rence.Name = "chkTraficR�f�rence"
        Me.chkTraficR�f�rence.Size = New System.Drawing.Size(130, 16)
        Me.chkTraficR�f�rence.TabIndex = 29
        Me.chkTraficR�f�rence.Text = "Matrice de r�f�rence"
        '
        'lblP�riode
        '
        Me.lblP�riode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblP�riode.Location = New System.Drawing.Point(616, 24)
        Me.lblP�riode.Name = "lblP�riode"
        Me.lblP�riode.Size = New System.Drawing.Size(101, 23)
        Me.lblP�riode.TabIndex = 24
        Me.lblP�riode.Text = "P�riode de trafic :"
        '
        'grpPi�ton
        '
        Me.grpPi�ton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPi�ton.Controls.Add(Me.Ac1GrilleTraficPi�tons)
        Me.grpPi�ton.Location = New System.Drawing.Point(600, 312)
        Me.grpPi�ton.Name = "grpPi�ton"
        Me.grpPi�ton.Size = New System.Drawing.Size(296, 72)
        Me.grpPi�ton.TabIndex = 27
        Me.grpPi�ton.TabStop = False
        Me.grpPi�ton.Text = "Trafic pi�tons"
        '
        'Ac1GrilleTraficPi�tons
        '
        Me.Ac1GrilleTraficPi�tons.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Ac1GrilleTraficPi�tons.BackColor = System.Drawing.SystemColors.Window
        Me.Ac1GrilleTraficPi�tons.ColumnInfo = resources.GetString("Ac1GrilleTraficPi�tons.ColumnInfo")
        Me.Ac1GrilleTraficPi�tons.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.Ac1GrilleTraficPi�tons.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.Ac1GrilleTraficPi�tons.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.Ac1GrilleTraficPi�tons.Location = New System.Drawing.Point(24, 24)
        Me.Ac1GrilleTraficPi�tons.Name = "Ac1GrilleTraficPi�tons"
        Me.Ac1GrilleTraficPi�tons.Rows.Count = 2
        Me.Ac1GrilleTraficPi�tons.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.Ac1GrilleTraficPi�tons.Size = New System.Drawing.Size(256, 40)
        Me.Ac1GrilleTraficPi�tons.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Ac1GrilleTraficPi�tons.Styles"))
        Me.Ac1GrilleTraficPi�tons.TabIndex = 22
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
        'grpV�hicule
        '
        Me.grpV�hicule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpV�hicule.Controls.Add(Me.AC1GrilleTraficV�hicules)
        Me.grpV�hicule.Controls.Add(Me.pnlTrafic)
        Me.grpV�hicule.Controls.Add(Me.lblUVP)
        Me.grpV�hicule.Location = New System.Drawing.Point(600, 88)
        Me.grpV�hicule.Name = "grpV�hicule"
        Me.grpV�hicule.Size = New System.Drawing.Size(296, 216)
        Me.grpV�hicule.TabIndex = 26
        Me.grpV�hicule.TabStop = False
        Me.grpV�hicule.Text = "Trafic V�hicules"
        '
        'AC1GrilleTraficV�hicules
        '
        Me.AC1GrilleTraficV�hicules.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.AC1GrilleTraficV�hicules.BackColor = System.Drawing.SystemColors.Window
        Me.AC1GrilleTraficV�hicules.ColumnInfo = resources.GetString("AC1GrilleTraficV�hicules.ColumnInfo")
        Me.AC1GrilleTraficV�hicules.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.AC1GrilleTraficV�hicules.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.AC1GrilleTraficV�hicules.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.AC1GrilleTraficV�hicules.Location = New System.Drawing.Point(8, 56)
        Me.AC1GrilleTraficV�hicules.Name = "AC1GrilleTraficV�hicules"
        Me.AC1GrilleTraficV�hicules.Rows.Count = 2
        Me.AC1GrilleTraficV�hicules.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.AC1GrilleTraficV�hicules.Size = New System.Drawing.Size(280, 152)
        Me.AC1GrilleTraficV�hicules.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("AC1GrilleTraficV�hicules.Styles"))
        Me.AC1GrilleTraficV�hicules.TabIndex = 21
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
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlCarrefourCompos�)
        Me.pnlPlansDeFeux.Controls.Add(Me.splitCarrefourCompos�)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlFeuFonctionnement)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlPhasage)
        Me.pnlPlansDeFeux.Controls.Add(Me.pnlFeuBase)
        Me.pnlPlansDeFeux.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPlansDeFeux.Location = New System.Drawing.Point(0, 0)
        Me.pnlPlansDeFeux.Name = "pnlPlansDeFeux"
        Me.pnlPlansDeFeux.Size = New System.Drawing.Size(912, 597)
        Me.pnlPlansDeFeux.TabIndex = 10
        '
        'pnlCarrefourCompos�
        '
        Me.pnlCarrefourCompos�.Controls.Add(Me.chkSc�narioD�finitif)
        Me.pnlCarrefourCompos�.Controls.Add(Me.radFeuFonctionnement)
        Me.pnlCarrefourCompos�.Controls.Add(Me.radFeuBase)
        Me.pnlCarrefourCompos�.Controls.Add(Me.radPhasage)
        Me.pnlCarrefourCompos�.Controls.Add(Me.cboCarrefourCompos�)
        Me.pnlCarrefourCompos�.Controls.Add(Me.lblCarrefourCompos�)
        Me.pnlCarrefourCompos�.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCarrefourCompos�.Location = New System.Drawing.Point(0, 3)
        Me.pnlCarrefourCompos�.Name = "pnlCarrefourCompos�"
        Me.pnlCarrefourCompos�.Size = New System.Drawing.Size(912, 80)
        Me.pnlCarrefourCompos�.TabIndex = 0
        '
        'chkSc�narioD�finitif
        '
        Me.chkSc�narioD�finitif.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSc�narioD�finitif.Location = New System.Drawing.Point(654, 8)
        Me.chkSc�narioD�finitif.Name = "chkSc�narioD�finitif"
        Me.chkSc�narioD�finitif.Size = New System.Drawing.Size(72, 40)
        Me.chkSc�narioD�finitif.TabIndex = 54
        Me.chkSc�narioD�finitif.Text = "Sc�nario d�finitif"
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
        'cboCarrefourCompos�
        '
        Me.cboCarrefourCompos�.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCarrefourCompos�.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cboCarrefourCompos�.Location = New System.Drawing.Point(24, 32)
        Me.cboCarrefourCompos�.Name = "cboCarrefourCompos�"
        Me.cboCarrefourCompos�.Size = New System.Drawing.Size(40, 21)
        Me.cboCarrefourCompos�.TabIndex = 2
        Me.cboCarrefourCompos�.Visible = False
        '
        'lblCarrefourCompos�
        '
        Me.lblCarrefourCompos�.Location = New System.Drawing.Point(8, 8)
        Me.lblCarrefourCompos�.Name = "lblCarrefourCompos�"
        Me.lblCarrefourCompos�.Size = New System.Drawing.Size(104, 16)
        Me.lblCarrefourCompos�.TabIndex = 1
        Me.lblCarrefourCompos�.Text = "Carrefour compos�"
        Me.lblCarrefourCompos�.Visible = False
        '
        'splitCarrefourCompos�
        '
        Me.splitCarrefourCompos�.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitCarrefourCompos�.Enabled = False
        Me.splitCarrefourCompos�.Location = New System.Drawing.Point(0, 0)
        Me.splitCarrefourCompos�.Name = "splitCarrefourCompos�"
        Me.splitCarrefourCompos�.Size = New System.Drawing.Size(912, 3)
        Me.splitCarrefourCompos�.TabIndex = 1
        Me.splitCarrefourCompos�.TabStop = False
        Me.splitCarrefourCompos�.Visible = False
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
        Me.lblTraficFct.Text = "P�riode de trafic"
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
        Me.grpPhasesFct.Controls.Add(Me.cboR�serveCapacit�Choisie)
        Me.grpPhasesFct.Controls.Add(Me.lblR�servCapacit�Choisie)
        Me.grpPhasesFct.Controls.Add(Me.btnCalculerCycle)
        Me.grpPhasesFct.Controls.Add(Me.cboM�thodeCalculCycle)
        Me.grpPhasesFct.Controls.Add(Me.lblM�thodeCalculCycle)
        Me.grpPhasesFct.Controls.Add(Me.radPhase3Fct)
        Me.grpPhasesFct.Controls.Add(Me.radPhase2Fct)
        Me.grpPhasesFct.Controls.Add(Me.radPhase1Fct)
        Me.grpPhasesFct.Controls.Add(Me.lbFigerDur�eFct)
        Me.grpPhasesFct.Controls.Add(Me.txtDur�eCycleFct)
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
        Me.grpPhasesFct.Text = "Dur�es des phases"
        '
        'cboR�serveCapacit�Choisie
        '
        Me.cboR�serveCapacit�Choisie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboR�serveCapacit�Choisie.Items.AddRange(New Object() {"  0%", "10%", "15%", "20%"})
        Me.cboR�serveCapacit�Choisie.Location = New System.Drawing.Point(96, 136)
        Me.cboR�serveCapacit�Choisie.Name = "cboR�serveCapacit�Choisie"
        Me.cboR�serveCapacit�Choisie.Size = New System.Drawing.Size(48, 21)
        Me.cboR�serveCapacit�Choisie.TabIndex = 41
        '
        'lblR�servCapacit�Choisie
        '
        Me.lblR�servCapacit�Choisie.Location = New System.Drawing.Point(16, 128)
        Me.lblR�servCapacit�Choisie.Name = "lblR�servCapacit�Choisie"
        Me.lblR�servCapacit�Choisie.Size = New System.Drawing.Size(64, 32)
        Me.lblR�servCapacit�Choisie.TabIndex = 40
        Me.lblR�servCapacit�Choisie.Text = "R�serve de capacit� :"
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
        'cboM�thodeCalculCycle
        '
        Me.cboM�thodeCalculCycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboM�thodeCalculCycle.Items.AddRange(New Object() {"Manuellement", "M�thode de Webster", "M�thode classique"})
        Me.cboM�thodeCalculCycle.Location = New System.Drawing.Point(160, 104)
        Me.cboM�thodeCalculCycle.Name = "cboM�thodeCalculCycle"
        Me.cboM�thodeCalculCycle.Size = New System.Drawing.Size(128, 21)
        Me.cboM�thodeCalculCycle.TabIndex = 38
        '
        'lblM�thodeCalculCycle
        '
        Me.lblM�thodeCalculCycle.Location = New System.Drawing.Point(16, 104)
        Me.lblM�thodeCalculCycle.Name = "lblM�thodeCalculCycle"
        Me.lblM�thodeCalculCycle.Size = New System.Drawing.Size(144, 16)
        Me.lblM�thodeCalculCycle.TabIndex = 37
        Me.lblM�thodeCalculCycle.Text = "Dur�e du cycle d�termin�e"
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
        'lbFigerDur�eFct
        '
        Me.lbFigerDur�eFct.Location = New System.Drawing.Point(16, 68)
        Me.lbFigerDur�eFct.Name = "lbFigerDur�eFct"
        Me.lbFigerDur�eFct.Size = New System.Drawing.Size(88, 26)
        Me.lbFigerDur�eFct.TabIndex = 33
        Me.lbFigerDur�eFct.Text = "Figer la dur�e de la phase"
        '
        'txtDur�eCycleFct
        '
        Me.txtDur�eCycleFct.Location = New System.Drawing.Point(32, 40)
        Me.txtDur�eCycleFct.Name = "txtDur�eCycleFct"
        Me.txtDur�eCycleFct.Size = New System.Drawing.Size(24, 20)
        Me.txtDur�eCycleFct.TabIndex = 29
        Me.txtDur�eCycleFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.grpSynchroFct.Controls.Add(Me.lblD�calagesFct)
        Me.grpSynchroFct.Controls.Add(Me.updD�calageFermetureFct)
        Me.grpSynchroFct.Controls.Add(Me.updD�calageOuvertureFct)
        Me.grpSynchroFct.Controls.Add(Me.lvwDur�eVertFct)
        Me.grpSynchroFct.Location = New System.Drawing.Point(568, 236)
        Me.grpSynchroFct.Name = "grpSynchroFct"
        Me.grpSynchroFct.Size = New System.Drawing.Size(320, 216)
        Me.grpSynchroFct.TabIndex = 50
        Me.grpSynchroFct.TabStop = False
        Me.grpSynchroFct.Text = "Synchronisations"
        '
        'lblD�calagesFct
        '
        Me.lblD�calagesFct.Location = New System.Drawing.Point(208, 8)
        Me.lblD�calagesFct.Name = "lblD�calagesFct"
        Me.lblD�calagesFct.Size = New System.Drawing.Size(64, 16)
        Me.lblD�calagesFct.TabIndex = 28
        Me.lblD�calagesFct.Text = "D�calages"
        '
        'updD�calageFermetureFct
        '
        Me.updD�calageFermetureFct.Location = New System.Drawing.Point(256, 24)
        Me.updD�calageFermetureFct.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updD�calageFermetureFct.Name = "updD�calageFermetureFct"
        Me.updD�calageFermetureFct.Size = New System.Drawing.Size(32, 20)
        Me.updD�calageFermetureFct.TabIndex = 11
        Me.updD�calageFermetureFct.Visible = False
        '
        'updD�calageOuvertureFct
        '
        Me.updD�calageOuvertureFct.Location = New System.Drawing.Point(192, 24)
        Me.updD�calageOuvertureFct.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updD�calageOuvertureFct.Name = "updD�calageOuvertureFct"
        Me.updD�calageOuvertureFct.Size = New System.Drawing.Size(32, 20)
        Me.updD�calageOuvertureFct.TabIndex = 4
        Me.updD�calageOuvertureFct.Visible = False
        '
        'lvwDur�eVertFct
        '
        Me.lvwDur�eVertFct.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLFFct, Me.lvwcolPhaseFct, Me.lvwcolDur�eFct, Me.lvwcolD�calOuvertureFct, Me.lvwcolD�calFermetureFct})
        Me.lvwDur�eVertFct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDur�eVertFct.FullRowSelect = True
        Me.lvwDur�eVertFct.Location = New System.Drawing.Point(4, 48)
        Me.lvwDur�eVertFct.MultiSelect = False
        Me.lvwDur�eVertFct.Name = "lvwDur�eVertFct"
        Me.lvwDur�eVertFct.Size = New System.Drawing.Size(312, 164)
        Me.lvwDur�eVertFct.TabIndex = 0
        Me.lvwDur�eVertFct.UseCompatibleStateImageBehavior = False
        Me.lvwDur�eVertFct.View = System.Windows.Forms.View.Details
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
        'lvwcolDur�eFct
        '
        Me.lvwcolDur�eFct.Text = "Dur�e vert"
        Me.lvwcolDur�eFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDur�eFct.Width = 71
        '
        'lvwcolD�calOuvertureFct
        '
        Me.lvwcolD�calOuvertureFct.Text = "Ouverture"
        Me.lvwcolD�calOuvertureFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvwcolD�calFermetureFct
        '
        Me.lvwcolD�calFermetureFct.Text = "Fermeture"
        Me.lvwcolD�calFermetureFct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolD�calFermetureFct.Width = 72
        '
        'pnlPhasage
        '
        Me.pnlPhasage.AutoScroll = True
        Me.pnlPhasage.AutoScrollMinSize = New System.Drawing.Size(280, 150)
        Me.pnlPhasage.Controls.Add(Me.pnlTableauPhasage)
        Me.pnlPhasage.Controls.Add(Me.pnlFiltrePhasage)
        Me.pnlPhasage.Controls.Add(Me.btnActionPhase)
        Me.pnlPhasage.Controls.Add(Me.lblD�coupagePhases)
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
        Me.pnlTableauPhasage.Controls.Add(Me.chkD�coupagePhases)
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
        'chkD�coupagePhases
        '
        Me.chkD�coupagePhases.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkD�coupagePhases.Location = New System.Drawing.Point(5, 256)
        Me.chkD�coupagePhases.Name = "chkD�coupagePhases"
        Me.chkD�coupagePhases.Size = New System.Drawing.Size(128, 16)
        Me.chkD�coupagePhases.TabIndex = 48
        Me.chkD�coupagePhases.Text = "Retenir ce phasage"
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
        Me.pnlFiltrePhasage.Controls.Add(Me.cboD�coupagePhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.txtR�serveCapacit�PourCent)
        Me.pnlFiltrePhasage.Controls.Add(Me.cboPhasesSp�ciales)
        Me.pnlFiltrePhasage.Controls.Add(Me.cbolLFMultiPhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblPhasesSp�ciales)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblLFMultiPhases)
        Me.pnlFiltrePhasage.Controls.Add(Me.lblR�serveCapacit�)
        Me.pnlFiltrePhasage.Controls.Add(Me.cboR�serveCapacit�)
        Me.pnlFiltrePhasage.Controls.Add(Me.chk3Phases)
        Me.pnlFiltrePhasage.Location = New System.Drawing.Point(640, 32)
        Me.pnlFiltrePhasage.Name = "pnlFiltrePhasage"
        Me.pnlFiltrePhasage.Size = New System.Drawing.Size(266, 128)
        Me.pnlFiltrePhasage.TabIndex = 62
        '
        'cboD�coupagePhases
        '
        Me.cboD�coupagePhases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboD�coupagePhases.Location = New System.Drawing.Point(8, 0)
        Me.cboD�coupagePhases.Name = "cboD�coupagePhases"
        Me.cboD�coupagePhases.Size = New System.Drawing.Size(97, 21)
        Me.cboD�coupagePhases.TabIndex = 36
        '
        'txtR�serveCapacit�PourCent
        '
        Me.txtR�serveCapacit�PourCent.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtR�serveCapacit�PourCent.Location = New System.Drawing.Point(224, 24)
        Me.txtR�serveCapacit�PourCent.Name = "txtR�serveCapacit�PourCent"
        Me.txtR�serveCapacit�PourCent.ReadOnly = True
        Me.txtR�serveCapacit�PourCent.Size = New System.Drawing.Size(33, 20)
        Me.txtR�serveCapacit�PourCent.TabIndex = 70
        Me.txtR�serveCapacit�PourCent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboPhasesSp�ciales
        '
        Me.cboPhasesSp�ciales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPhasesSp�ciales.Enabled = False
        Me.cboPhasesSp�ciales.Items.AddRange(New Object() {"Inclure ces phasages", "Exclure ces phasages", "Ne proposer que ceux-l�"})
        Me.cboPhasesSp�ciales.Location = New System.Drawing.Point(144, 96)
        Me.cboPhasesSp�ciales.Name = "cboPhasesSp�ciales"
        Me.cboPhasesSp�ciales.Size = New System.Drawing.Size(124, 21)
        Me.cboPhasesSp�ciales.TabIndex = 69
        '
        'cbolLFMultiPhases
        '
        Me.cbolLFMultiPhases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbolLFMultiPhases.Enabled = False
        Me.cbolLFMultiPhases.Items.AddRange(New Object() {"Inclure ces phasages", "Exclure ces phasages", "Ne proposer que ceux-l�"})
        Me.cbolLFMultiPhases.Location = New System.Drawing.Point(8, 96)
        Me.cbolLFMultiPhases.Name = "cbolLFMultiPhases"
        Me.cbolLFMultiPhases.Size = New System.Drawing.Size(124, 21)
        Me.cbolLFMultiPhases.TabIndex = 68
        '
        'lblPhasesSp�ciales
        '
        Me.lblPhasesSp�ciales.Location = New System.Drawing.Point(144, 56)
        Me.lblPhasesSp�ciales.Name = "lblPhasesSp�ciales"
        Me.lblPhasesSp�ciales.Size = New System.Drawing.Size(88, 32)
        Me.lblPhasesSp�ciales.TabIndex = 67
        Me.lblPhasesSp�ciales.Text = "Phasages avec phase sp�ciale"
        '
        'lblLFMultiPhases
        '
        Me.lblLFMultiPhases.Location = New System.Drawing.Point(8, 56)
        Me.lblLFMultiPhases.Name = "lblLFMultiPhases"
        Me.lblLFMultiPhases.Size = New System.Drawing.Size(80, 32)
        Me.lblLFMultiPhases.TabIndex = 66
        Me.lblLFMultiPhases.Text = "Lignes de feux sur 2 phases"
        '
        'lblR�serveCapacit�
        '
        Me.lblR�serveCapacit�.Location = New System.Drawing.Point(144, 0)
        Me.lblR�serveCapacit�.Name = "lblR�serveCapacit�"
        Me.lblR�serveCapacit�.Size = New System.Drawing.Size(112, 24)
        Me.lblR�serveCapacit�.TabIndex = 65
        Me.lblR�serveCapacit�.Text = "R�serve de capacit�"
        '
        'cboR�serveCapacit�
        '
        Me.cboR�serveCapacit�.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboR�serveCapacit�.Enabled = False
        Me.cboR�serveCapacit�.Items.AddRange(New Object() {"<Indiff�rent>", "< 10%", "10 � 20%", ">=20%"})
        Me.cboR�serveCapacit�.Location = New System.Drawing.Point(144, 24)
        Me.cboR�serveCapacit�.Name = "cboR�serveCapacit�"
        Me.cboR�serveCapacit�.Size = New System.Drawing.Size(72, 21)
        Me.cboR�serveCapacit�.TabIndex = 64
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
        'lblD�coupagePhases
        '
        Me.lblD�coupagePhases.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblD�coupagePhases.Location = New System.Drawing.Point(640, 8)
        Me.lblD�coupagePhases.Name = "lblD�coupagePhases"
        Me.lblD�coupagePhases.Size = New System.Drawing.Size(152, 16)
        Me.lblD�coupagePhases.TabIndex = 37
        Me.lblD�coupagePhases.Text = "2 Phasages possibles"
        '
        'pnlFeuBase
        '
        Me.pnlFeuBase.AutoScroll = True
        Me.pnlFeuBase.AutoScrollMinSize = New System.Drawing.Size(320, 150)
        Me.pnlFeuBase.Controls.Add(Me.lblV�hiculeBase)
        Me.pnlFeuBase.Controls.Add(Me.lblPi�tonBase)
        Me.pnlFeuBase.Controls.Add(Me.txtVertMiniPi�ton)
        Me.pnlFeuBase.Controls.Add(Me.txtVertMiniV�hicule)
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
        'lblV�hiculeBase
        '
        Me.lblV�hiculeBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblV�hiculeBase.Location = New System.Drawing.Point(632, 0)
        Me.lblV�hiculeBase.Name = "lblV�hiculeBase"
        Me.lblV�hiculeBase.Size = New System.Drawing.Size(56, 12)
        Me.lblV�hiculeBase.TabIndex = 31
        Me.lblV�hiculeBase.Text = "V�hicules"
        '
        'lblPi�tonBase
        '
        Me.lblPi�tonBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPi�tonBase.Location = New System.Drawing.Point(688, 0)
        Me.lblPi�tonBase.Name = "lblPi�tonBase"
        Me.lblPi�tonBase.Size = New System.Drawing.Size(56, 12)
        Me.lblPi�tonBase.TabIndex = 30
        Me.lblPi�tonBase.Text = "Pi�tons"
        '
        'txtVertMiniPi�ton
        '
        Me.txtVertMiniPi�ton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVertMiniPi�ton.Location = New System.Drawing.Point(696, 16)
        Me.txtVertMiniPi�ton.Name = "txtVertMiniPi�ton"
        Me.txtVertMiniPi�ton.Size = New System.Drawing.Size(20, 20)
        Me.txtVertMiniPi�ton.TabIndex = 29
        Me.txtVertMiniPi�ton.Text = "10"
        Me.txtVertMiniPi�ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtVertMiniV�hicule
        '
        Me.txtVertMiniV�hicule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVertMiniV�hicule.Location = New System.Drawing.Point(648, 16)
        Me.txtVertMiniV�hicule.Name = "txtVertMiniV�hicule"
        Me.txtVertMiniV�hicule.Size = New System.Drawing.Size(20, 20)
        Me.txtVertMiniV�hicule.TabIndex = 28
        Me.txtVertMiniV�hicule.Text = "6"
        Me.txtVertMiniV�hicule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.grpSynchroBase.Controls.Add(Me.lblD�calages)
        Me.grpSynchroBase.Controls.Add(Me.updD�calageFermetureBase)
        Me.grpSynchroBase.Controls.Add(Me.updD�calageOuvertureBase)
        Me.grpSynchroBase.Controls.Add(Me.lvwDur�eVert)
        Me.grpSynchroBase.Location = New System.Drawing.Point(576, 176)
        Me.grpSynchroBase.Name = "grpSynchroBase"
        Me.grpSynchroBase.Size = New System.Drawing.Size(320, 240)
        Me.grpSynchroBase.TabIndex = 25
        Me.grpSynchroBase.TabStop = False
        Me.grpSynchroBase.Text = "Synchronisations"
        '
        'lblD�calages
        '
        Me.lblD�calages.Location = New System.Drawing.Point(208, 16)
        Me.lblD�calages.Name = "lblD�calages"
        Me.lblD�calages.Size = New System.Drawing.Size(64, 16)
        Me.lblD�calages.TabIndex = 28
        Me.lblD�calages.Text = "D�calages"
        '
        'updD�calageFermetureBase
        '
        Me.updD�calageFermetureBase.Location = New System.Drawing.Point(256, 32)
        Me.updD�calageFermetureBase.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updD�calageFermetureBase.Name = "updD�calageFermetureBase"
        Me.updD�calageFermetureBase.Size = New System.Drawing.Size(32, 20)
        Me.updD�calageFermetureBase.TabIndex = 11
        Me.updD�calageFermetureBase.Visible = False
        '
        'updD�calageOuvertureBase
        '
        Me.updD�calageOuvertureBase.Location = New System.Drawing.Point(192, 32)
        Me.updD�calageOuvertureBase.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updD�calageOuvertureBase.Name = "updD�calageOuvertureBase"
        Me.updD�calageOuvertureBase.Size = New System.Drawing.Size(32, 20)
        Me.updD�calageOuvertureBase.TabIndex = 4
        Me.updD�calageOuvertureBase.Visible = False
        '
        'lvwDur�eVert
        '
        Me.lvwDur�eVert.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolLF, Me.lvwcolPhase, Me.lvwcolDur�e, Me.lvwcolD�calOuverture, Me.lvwcolD�calFermeture})
        Me.lvwDur�eVert.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDur�eVert.FullRowSelect = True
        Me.lvwDur�eVert.HideSelection = False
        Me.lvwDur�eVert.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7})
        Me.lvwDur�eVert.Location = New System.Drawing.Point(4, 64)
        Me.lvwDur�eVert.MultiSelect = False
        Me.lvwDur�eVert.Name = "lvwDur�eVert"
        Me.lvwDur�eVert.Size = New System.Drawing.Size(312, 163)
        Me.lvwDur�eVert.TabIndex = 0
        Me.lvwDur�eVert.UseCompatibleStateImageBehavior = False
        Me.lvwDur�eVert.View = System.Windows.Forms.View.Details
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
        'lvwcolDur�e
        '
        Me.lvwcolDur�e.Text = "Dur�e vert"
        Me.lvwcolDur�e.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolDur�e.Width = 71
        '
        'lvwcolD�calOuverture
        '
        Me.lvwcolD�calOuverture.Text = "Ouverture"
        Me.lvwcolD�calOuverture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lvwcolD�calFermeture
        '
        Me.lvwcolD�calFermeture.Text = "Fermeture"
        Me.lvwcolD�calFermeture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvwcolD�calFermeture.Width = 72
        '
        'grpPhasesBase
        '
        Me.grpPhasesBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPhasesBase.Controls.Add(Me.radPhase3Base)
        Me.grpPhasesBase.Controls.Add(Me.radPhase2Base)
        Me.grpPhasesBase.Controls.Add(Me.radPhase1Base)
        Me.grpPhasesBase.Controls.Add(Me.lbFigerDur�eBase)
        Me.grpPhasesBase.Controls.Add(Me.txtDur�eCycleBase)
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
        Me.grpPhasesBase.Text = "Dur�es des phases"
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
        'lbFigerDur�eBase
        '
        Me.lbFigerDur�eBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbFigerDur�eBase.Location = New System.Drawing.Point(32, 86)
        Me.lbFigerDur�eBase.Name = "lbFigerDur�eBase"
        Me.lbFigerDur�eBase.Size = New System.Drawing.Size(80, 28)
        Me.lbFigerDur�eBase.TabIndex = 33
        Me.lbFigerDur�eBase.Text = "Figer la dur�e de la phase"
        '
        'txtDur�eCycleBase
        '
        Me.txtDur�eCycleBase.BackColor = System.Drawing.SystemColors.Window
        Me.txtDur�eCycleBase.Location = New System.Drawing.Point(32, 56)
        Me.txtDur�eCycleBase.Name = "txtDur�eCycleBase"
        Me.txtDur�eCycleBase.Size = New System.Drawing.Size(24, 20)
        Me.txtDur�eCycleBase.TabIndex = 29
        Me.txtDur�eCycleBase.Text = "60"
        Me.txtDur�eCycleBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cboTriLignesFeuxPlans.Items.AddRange(New Object() {"Manuel", "Feux V�hicules en t�te", "Par Branche", "Par nom de feux", "Par phase"})
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
        Me.pnlConflits.Controls.Add(Me.pnlMatricesS�curit�)
        Me.pnlConflits.Controls.Add(Me.pnlVerrouMatrice)
        Me.pnlConflits.Controls.Add(Me.Ac1GrilleS�curit�)
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
        Me.pnlBoutonsRouges.Controls.Add(Me.btnRougeD�faut)
        Me.pnlBoutonsRouges.Controls.Add(Me.btnRougesD�faut)
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
        Me.lblBoutonsRouges.Text = "Reprendre les valeurs par d�faut"
        '
        'btnRougeD�faut
        '
        Me.btnRougeD�faut.Location = New System.Drawing.Point(100, 32)
        Me.btnRougeD�faut.Name = "btnRougeD�faut"
        Me.btnRougeD�faut.Size = New System.Drawing.Size(72, 32)
        Me.btnRougeD�faut.TabIndex = 1
        Me.btnRougeD�faut.Text = "Le rouge s�lectionn�"
        '
        'btnRougesD�faut
        '
        Me.btnRougesD�faut.Enabled = False
        Me.btnRougesD�faut.Location = New System.Drawing.Point(16, 32)
        Me.btnRougesD�faut.Name = "btnRougesD�faut"
        Me.btnRougesD�faut.Size = New System.Drawing.Size(72, 32)
        Me.btnRougesD�faut.TabIndex = 0
        Me.btnRougesD�faut.Text = "Toute la Matrice"
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
        Me.pnlAntagonismes.Controls.Add(Me.btnR�initAntago)
        Me.pnlAntagonismes.Controls.Add(Me.cboBrancheCourant1)
        Me.pnlAntagonismes.Controls.Add(Me.lblCourantOrigine)
        Me.pnlAntagonismes.Controls.Add(Me.AC1GrilleAntagonismes)
        Me.pnlAntagonismes.Location = New System.Drawing.Point(664, 328)
        Me.pnlAntagonismes.Name = "pnlAntagonismes"
        Me.pnlAntagonismes.Size = New System.Drawing.Size(242, 232)
        Me.pnlAntagonismes.TabIndex = 36
        '
        'btnR�initAntago
        '
        Me.btnR�initAntago.Location = New System.Drawing.Point(144, 4)
        Me.btnR�initAntago.Name = "btnR�initAntago"
        Me.btnR�initAntago.Size = New System.Drawing.Size(72, 24)
        Me.btnR�initAntago.TabIndex = 38
        Me.btnR�initAntago.Text = "R�initialiser"
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
        'pnlMatricesS�curit�
        '
        Me.pnlMatricesS�curit�.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMatricesS�curit�.Controls.Add(Me.radMatriceInterverts)
        Me.pnlMatricesS�curit�.Controls.Add(Me.radMatriceRougesD�gagement)
        Me.pnlMatricesS�curit�.Controls.Add(Me.radMatriceConflits)
        Me.pnlMatricesS�curit�.Location = New System.Drawing.Point(752, 0)
        Me.pnlMatricesS�curit�.Name = "pnlMatricesS�curit�"
        Me.pnlMatricesS�curit�.Size = New System.Drawing.Size(160, 64)
        Me.pnlMatricesS�curit�.TabIndex = 33
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
        'radMatriceRougesD�gagement
        '
        Me.radMatriceRougesD�gagement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radMatriceRougesD�gagement.Location = New System.Drawing.Point(8, 24)
        Me.radMatriceRougesD�gagement.Name = "radMatriceRougesD�gagement"
        Me.radMatriceRougesD�gagement.Size = New System.Drawing.Size(144, 16)
        Me.radMatriceRougesD�gagement.TabIndex = 37
        Me.radMatriceRougesD�gagement.Text = "Rouges de d�gagement"
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
        'Ac1GrilleS�curit�
        '
        Me.Ac1GrilleS�curit�.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.Ac1GrilleS�curit�.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Ac1GrilleS�curit�.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Ac1GrilleS�curit�.BackColor = System.Drawing.SystemColors.Window
        Me.Ac1GrilleS�curit�.ColumnInfo = "4,1,0,0,0,20,Columns:0{Width:20;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:20;DataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Width:20;D" & _
            "ataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Width:20;DataType:System.Int16;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Ac1GrilleS�curit�.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.Ac1GrilleS�curit�.Location = New System.Drawing.Point(661, 72)
        Me.Ac1GrilleS�curit�.Name = "Ac1GrilleS�curit�"
        Me.Ac1GrilleS�curit�.Rows.Count = 2
        Me.Ac1GrilleS�curit�.Size = New System.Drawing.Size(344, 176)
        Me.Ac1GrilleS�curit�.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Ac1GrilleS�curit�.Styles"))
        Me.Ac1GrilleS�curit�.TabIndex = 24
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
        Me.Controls.Add(Me.splitGraphiqueDonn�es)
        Me.Controls.Add(Me.picDessin)
        Me.Controls.Add(Me.splitOngletsPrincipal)
        Me.Controls.Add(Me.tabOnglet)
        Me.Controls.Add(Me.pnlLignesDeFeux)
        Me.Controls.Add(Me.pnlPlansDeFeux)
        Me.Controls.Add(Me.pnlTrafics)
        Me.Controls.Add(Me.pnlConflits)
        Me.Controls.Add(Me.pnlG�om�trie)
        Me.Name = "frmCarrefour"
        Me.tabOnglet.ResumeLayout(False)
        Me.pnlG�om�trie.ResumeLayout(False)
        Me.pnlIlots.ResumeLayout(False)
        CType(Me.AC1GrilleIlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBtnG�om�trie.ResumeLayout(False)
        CType(Me.AC1GrilleBranches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLignesDeFeux.ResumeLayout(False)
        Me.pnlTrajectoires.ResumeLayout(False)
        CType(Me.AC1GrilleFeux, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBoutonsLignesFeux.ResumeLayout(False)
        Me.pnlTrafics.ResumeLayout(False)
        Me.pnlTrafics.PerformLayout()
        Me.grpPi�ton.ResumeLayout(False)
        CType(Me.Ac1GrilleTraficPi�tons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpV�hicule.ResumeLayout(False)
        CType(Me.AC1GrilleTraficV�hicules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTrafic.ResumeLayout(False)
        Me.pnlPlansDeFeux.ResumeLayout(False)
        Me.pnlCarrefourCompos�.ResumeLayout(False)
        Me.pnlFeuFonctionnement.ResumeLayout(False)
        Me.grpPhasesFct.ResumeLayout(False)
        Me.grpPhasesFct.PerformLayout()
        CType(Me.updPhase3Fct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase2Fct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updPhase1Fct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSynchroFct.ResumeLayout(False)
        CType(Me.updD�calageFermetureFct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updD�calageOuvertureFct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPhasage.ResumeLayout(False)
        Me.pnlTableauPhasage.ResumeLayout(False)
        CType(Me.AC1GrillePhases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiltrePhasage.ResumeLayout(False)
        Me.pnlFiltrePhasage.PerformLayout()
        Me.pnlFeuBase.ResumeLayout(False)
        Me.pnlFeuBase.PerformLayout()
        Me.grpSynchroBase.ResumeLayout(False)
        CType(Me.updD�calageFermetureBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updD�calageOuvertureBase, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pnlMatricesS�curit�.ResumeLayout(False)
        Me.pnlVerrouMatrice.ResumeLayout(False)
        CType(Me.Ac1GrilleS�curit�, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
  '  Me.pnlPlansDeFeux.Controls.Add(Me.pnlCarrefourCompos�)

#Region " D�clarations"
  Private BufPicDessin As System.Windows.Forms.PictureBox

  Public Enum CommandeGraphique
    EnCours = -1
    AucuneCommande
    D�placerCarrefour
    OrigineBranche
    AngleBranche
    EtirerIlot
    D�placerIlot
    ElargirIlot
    PassagePi�ton
    PassagePi�tonRapide
    D�placerPassage
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
    Travers�e
    PropTravers�e
    D�composerTravers�e
    PositionTrafic
    Antagonisme
    LigneFeux
    D�placerLigneFeu
    AllongerFeu
    SupprimerLigneFeu
    D�placerSignal
    Zoom
    ZoomMoins
    ZoomPr�c�dent
    ZoomPAN
    Mesure
    D�placerNord
    OrienterNord
    D�placerEchelle
  End Enum
  Private Enum M�thodeCalculCycle
    Manuel
    Webster
    Classique
  End Enum

  Private ChargementEnCours As Boolean = True
  Private FermetureEnCours As Boolean
  Private DessinEnCours As Boolean
  Private ChangementDeSc�nario As Boolean = True

  Private D�calageFeuxEnCours As Boolean
  Private AffichagePhasesEnCours As Boolean
  Private PhasageAffich� As Boolean

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

  Private monTraficPr�c�dent As Trafic

  Private IndexPhasages() As Short

  'Cr�ation d'une collection de plans de feux de base utiles � l'organisation du phasage
  'Private mesPlansPourPhasage As PlanFeuxCollection
  Private monPlanPourPhasage As PlanFeuxPhasage

  Private ModeGraphique As Boolean
  'Fond de plan 
  Private monFDP As FondDePlan
  Public colCalques As CalqueCollection
  Public GraphFDP As SuperBloc

  ' Vrai d�s qu'on commence le 'glisser'
  Private mDragging As Boolean

  Private NePasEffacer As Boolean
  Private EnAttenteMouseUp As Boolean

  'A remplacer par un vrai objet (evt Nothing)
  Private objS�lect As Graphique
  Private savObjS�lect As Graphique

  Private SelectObject As Boolean 'Indique qu'un objet est en cours de s�lection (inhiber les s�lections d'objet par les grilles)

  ' Collection des objets graphiques repr�sentant les objets m�tiers du projet : Objets � dessiner
  Private colObjetsGraphiques As New Graphiques
  Private PointCliqu� As Point

  Private PourFrame As Boolean = False
  Private UnCarr� As Boolean = False

  ' position de la souris.	(en Coordonn�es du picturebox)
  Private mPoint() As Point
  Private mPoint1 As Point

  'Ou		(en coordonn�es �cran)
  Private mScreen1 As Point
  Private mScreen2 As Point
  Private mScreen() As Point

  Private mEchelles As New Hashtable

  'Infos pour v�rifier que le point est dans un contour
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

  'Infos pour le passage pi�ton
  Private BrancheLi�e As Branche
  Private BordChauss�ePassage As Branche.Lat�ralit�
  Private AngleParall�le As Single    'Angle des 2 cot�s parall�les
  Private Poign�eCliqu�e As Short  ' Indique  quel cot� est en cours de modif
  Private SigneConserv� As Short

  'Infos pour le passage pi�ton et les lignes de feux
  Private VoieTraj As Voie
  Private VoieOrigine As Voie

  'Infos pour les lignes de feux
  Private LigneFeuEnCours As LigneFeuV�hicules
  Private SignalFeuEnCours As SignalFeu

  'Infos pour les travers�es pi�tonnes
  Private Travers�e As Travers�ePi�tonne

  Private G�n�rationTrajectoires As Boolean

  ' Buffer graphique associ� au PictureBox (pour Paint)
  Private mBufferGraphics As Graphics
  ' BitMap associ�e � ce Buffer
  Private mBitmap As Bitmap

  Private mBufferGraphicsA As Graphics
  Private mBitmapA As Bitmap

  'Base : position initiale de splitGraphiqueDonn�es pour chaque panel (on part d'1 ClientSize de 900)
  Private lgPanel(6) As Short
  Private pnlPalette As Panel
  Private pnlPlanFeu As Panel
  Private FonteGras As Font

  Private mAideTopic As [Global].AideEnum

  Private StyleGris�, StyleD�gris�, StyleGris�Gras, StyleD�gris�Gras, StyleGris�Bool�en, StyleGris�Rouge, StyleRouge, StyleVert, StyleOrang�, StyleSaisie, StyleSaisieItalique As Grille.CellStyle
  Private strSauveGrille As String
#End Region
#Region " Affichage des panels"
  Private Sub D�finirSplitPosition()
        'splitGraphiqueDonn�es.SplitPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
        If Not ChargementEnCours Then
            splitGraphiqueDonn�es.SplitPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
        End If
  End Sub

  Private Sub D�finirD�fautLargeurPanels()
    Dim lgD�fautPanel() As Short = {450, 500, 400, 280, 280, 340, 380}
    Dim i As Short

    For i = 0 To lgPanel.Length - 1
      D�finirD�fautLargeurPanel(i, lgD�fautPanel(i))
    Next

  End Sub

  Private Function D�finirD�fautLargeurPanel(ByVal num As OngletEnum, Optional ByVal ValeurD�faut As Short = 0) As Short

    If ValeurD�faut = 0 Then
      ValeurD�faut = lgPanel(num)
    Else
      lgPanel(num) = ValeurD�faut
    End If

    'If num = Global.OngletEnum.Conflits Then
    '  If Me.Ac1GrilleS�curit�.Rows.Count > 4 Then
    '    'La grille des conflits a �t� initialis�e
    '    lgPanel(OngletEnum.Conflits) = Math.Max(Me.Ac1GrilleS�curit�.Width + 2 * LGMARGE, ValeurD�faut)
    '  End If
    'End If

  End Function

  Private Function numPanel() As OngletEnum
    If pnlPalette Is Me.pnlG�om�trie Then
      numPanel = OngletEnum.G�om�trie
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

  Private Sub pnlG�om�trie_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlG�om�trie.Resize, pnlLignesDeFeux.Resize, pnlConflits.Resize

    If Not IsNothing(pnlPalette) Then
      If pnlPalette Is Me.pnlConflits Then
        Me.FenetreAntagonisme.Location = Me.pnlPalette.PointToScreen(New Point(-50, 100))
      Else
        Me.FenetreAideCommande.Location = Me.pnlPalette.PointToScreen(New Point(-50, 300))
      End If
    End If

  End Sub

  '******************************************************************************
  ' D�placement du Splitter vertical entre le graphique et pnlPalette
  '******************************************************************************
  Private Sub splitGraphiqueDonn�es_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) _
    Handles splitGraphiqueDonn�es.SplitterMoved
    'M�moriser la nouvelle largeur de la palette, afin qu"elle soit conserv�e par Form_Resize
    lgPanel(numPanel) = Me.ClientSize.Width - 8 - Me.splitGraphiqueDonn�es.SplitPosition
    'Le plan de feux a pu �tre tronqu�
    If numPanel() > [Global].OngletEnum.PlansDeFeux Then RedessinerDiagrammePlanFeux()
  End Sub
#End Region
#Region " Fonctions de la Feuille"
  '******************************************************************************
  ' Chargement de la feuille
  '******************************************************************************
  Private Sub frmCarrefour_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim NomFichierImage As String = "G�omf.jpg"

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
      ElseIf Not mParamDessin.TailleFen�tre.IsEmpty Then
        Me.Size = mParamDessin.TailleFen�tre
      End If

      Text = .Libell�
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
          Recr�erDessinAntagonismes()

        Else
          Me.pnlLignesDeFeux.Controls.Remove(Me.pnlTrajectoires)
          Me.chkVerrouG�om�trie.Top = 1
          Me.chkVerrouLignesFeux.Top -= 100
          Me.pnlBtnG�om�trie.Controls.Remove(Me.chkVerrouG�om�trie)
        End If

        .Cr�erGraphique(colObjetsGraphiques)
        If IsNothing(.NomFichier) Then
          'Par d�faut : pas de Nord ni d'�chelle pour un nouveau fichier
          'Les instructions qui suivent ne peuvent �tre appel�es qu'apr�s Cr�erGraphique
          .NordAffich� = False
          .EchelleAffich�e = False
          .SensCirculation = Not ModeGraphique
        End If
      End With


      cndContexte = [Global].OngletEnum.G�om�trie
      InitG�om�trie()
      If mesTrafics.Count > 0 Then
        InitTrafics()
      End If
      InitLignesFeux()

      InitVerrouillages()
      ChoisirOngletInitial()

      'Fen�tres outils
      Me.FenetreAideCommande.Owner = Me
      Me.FenetreAntagonisme.Owner = Me
      Me.FenetreDiagnostic.Owner = Me
      Me.FenetreDiagnostic.Text = "Diagnostic " & Me.Text

            ChargementEnCours = False
            'D�finirSplitPosition()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Chargement de la feuille carrefour")
    End Try

    End Sub
    '*******************************************************************************************************
    ' Instancier un tampon de la taille maximum, o� sera m�moris� le dessin por r�affichage lors du Paint
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
        cndD�boguage = False

    'variable globale red�finie sur ce carrefour
    cndVariante = maVariante

    cndpicDessin = picDessin
    cndGraphique = picDessin.CreateGraphics

    'cndAbaque.Owner = Me

    R�affecterEchelle()

    'Activation des boutons de la barre d'outils
    With mdiApplication
      With .tbrDiagfeux.Buttons()
        'Zoom pr�c�dent : actif si il y a une vue  pr�c�dente m�moris�e
        .Item(MDIDiagfeux.BarreOutilsEnum.ZoomPr�c�dent).Visible = mEchelles.Count > 1
        'Rafraichir
        .Item(MDIDiagfeux.BarreOutilsEnum.Rafraichir).Visible = True
      End With

      .mnuSensTrajectoires.Checked = maVariante.SensTrajectoires
      .mnuSensCirculation.Checked = maVariante.SensCirculation
      .mnuNord.Checked = maVariante.NordAffich�
      .mnuEchelle.Checked = maVariante.EchelleAffich�e

      AfficherContexteFDP()

      Recr�erMenuContextuel(.mnuAffichage)
    End With

    AfficherCacherDiagnostic()

    TopicAideCourant = mAideTopic

  End Sub

  Private Sub AfficherContexteFDP()

    'Barre d'�tat 
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

  Private Sub AfficherSc�narios()
    mdiApplication.AfficherSc�narios()
  End Sub

  '============= BLoc de fonctions sp�cifiques aux sc�narios ===========================

  Private Sub Recr�erDessinAntagonismes()

    If ModeGraphique AndAlso Sc�narioEnCours() Then
      With monPlanFeuxBase()
        PurgerAntagonismes()
        If .Verrou >= [Global].Verrouillage.LignesFeux Then
          .Antagonismes.Cr�erGraphique(colObjetsGraphiques)
          .Antagonismes.Verrouiller()
        End If
      End With
    End If

  End Sub

  Public Sub NouveauSc�nario()

    Try

      With monPlanFeuxBase()
        Recr�erDessinAntagonismes()
        If .AvecTrafic Then
          AjouterComboTrafic(.Nom)
          Me.tabOnglet.SelectedTab = Me.tabTrafics
        End If
      End With

      AfficherSc�narios()

      Modif = True

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "NouveauSc�nario")

    End Try
  End Sub

  Private Sub PurgerAntagonismes()
    Dim unObjetGraphique As Graphique
    Dim Garbage As New Graphiques

    For Each unObjetGraphique In colObjetsGraphiques
      If TypeOf unObjetGraphique.ObjetM�tier Is Antagonisme Then
        Garbage.Add(unObjetGraphique)
      End If
    Next

    For Each unObjetGraphique In Garbage
      colObjetsGraphiques.Remove(unObjetGraphique)
    Next
  End Sub

  Public Sub DupliquerSc�nario()
    Dim nomSc�nario As String = InputBox("Nom du sc�nario dupliqu�").Trim
    Dim unSc�nario As PlanFeuxBase = monPlanFeuxBase()
    Dim unTrafic As Trafic

    Try

      With maVariante
        If nomSc�nario.Length = 0 Then
        ElseIf Not IsNothing(unSc�nario) AndAlso String.Compare(nomSc�nario, unSc�nario.Nom, ignoreCase:=True) = 0 Then
        ElseIf maVariante.mPlansFeuxBase.Contains(nomSc�nario) Then
          MessageBox.Show("Un sc�nario de m�me nom existe d�j�")
        Else

          With .mPlansFeuxBase
            monPlanFeuxBase = .Item(.Add(New PlanFeuxBase(unSc�nario)))
          End With
          If unSc�nario.AvecTrafic Then
            unTrafic = New Trafic(unSc�nario.Trafic)
            unTrafic.Nom = nomSc�nario
            .mTrafics.Add(unTrafic)
            AjouterComboTrafic(unTrafic.Nom)
            monPlanFeuxBase.Trafic = unTrafic
          Else
            monPlanFeuxBase.Nom = nomSc�nario
          End If

          Dim cpt As Short = monPlanFeuxBase.mPlansFonctionnement.Count
          If monPlanFeuxBase.mPlansFonctionnement.Contains("") Then
            'Plan de feux de fonctionnement dont le nom est rest� identique au sc�nario initial
            'Lui donner le nom du nouveau sc�nario
            monPlanFeuxBase.mPlansFonctionnement("").Nom = nomSc�nario
            cpt -= 1
          End If
          Select Case cpt
            Case 1
              MessageBox.Show("Un plan de feux de fonctionnement a �t� �galement dupliqu�" & vbCrLf & "Il vous appartient de le renommer")
            Case Is > 1
              MessageBox.Show("Des plans de feux de fonctionnement ont �t� �galement dupliqu�s" & vbCrLf & "Il vous appartient de les renommer")
          End Select

          AfficherSc�narios()
          Modif = True
        End If
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try


  End Sub

  Public Sub RenommerSc�nario()
    Dim nomSc�nario As String = InputBox("Renommer le sc�nario " & monPlanFeuxBase.Nom).Trim

    Try
      If nomSc�nario.Length = 0 Then
      ElseIf String.Compare(nomSc�nario, monPlanFeuxBase.Nom, ignoreCase:=True) = 0 Then
      ElseIf maVariante.mPlansFeuxBase.Contains(nomSc�nario) Then
        MessageBox.Show("Un sc�nario de m�me nom existe d�j�")
      Else
        monPlanFeuxBase.Nom = nomSc�nario
        If monPlanFeuxBase.AvecTrafic Then
          RenommerComboTrafic(nomSc�nario, mesTrafics.IndexOf(monPlanFeuxBase.Trafic))
        End If

        AfficherSc�narios()
        Modif = True
      End If


    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try
  End Sub

  Public Sub SupprimerSc�nario()
    Try

      If Not Sc�narioNonSupprimable() AndAlso Confirmation("Supprimer le sc�nario " & monPlanFeuxBase.Nom, Critique:=True) Then
        maVariante.mPlansFeuxBase.Remove(monPlanFeuxBase)
        If monPlanFeuxBase.AvecTrafic Then
          Dim unTrafic As Trafic = monTraficActif()
          'Ajout 27/03/07 : si c'est le dernier trafic, il reste affich� avec ses valeurs si nouveau trafic ensuite
          unTrafic.R�initialiser()
          AfficherTrafic(AvecLesPi�tons:=True)
          SupprimerComboTrafic(mesTrafics.IndexOf(unTrafic))
          mesTrafics.Remove(unTrafic)
        End If
        If maVariante.mPlansFeuxBase.Count = 0 Then
          monPlanFeuxBase = Nothing
        Else
          maVariante.Sc�narioCourant = maVariante.mPlansFeuxBase(CType(0, Short))
        End If

        AfficherSc�narios()
        Me.tabOnglet.SelectedTab = Me.tabLignesDeFeux
        Modif = True
      End If


    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try
  End Sub

  Public Function Sc�narioNonSupprimable() As Boolean
    Dim unPlanFeuxBase As PlanFeuxBase
    Dim unPlanFeuxFct As PlanFeuxFonctionnement
    Dim unTrafic As Trafic = monPlanFeuxBase.Trafic
    Dim Message As String

    If Not IsNothing(unTrafic) Then
      For Each unPlanFeuxBase In maVariante.mPlansFeuxBase
        If Not unPlanFeuxBase Is monPlanFeuxBase AndAlso unPlanFeuxBase.Trafics.Contains(unTrafic) Then
          Message = "La p�riode de trafic de ce sc�nario est utilis�e"
          Message &= vbCrLf & "par un plan de feux"
          Message &= " du sc�nario " & unPlanFeuxBase.Nom
          MessageBox.Show(Message, "Suppression de sc�nario impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          Sc�narioNonSupprimable = True
          Exit For
        End If
      Next
    End If

  End Function

  Public Sub S�lectionnerSc�nario(ByVal Index As Short)

    Try

      If Sc�narioEnCours() AndAlso monPlanFeuxBase.AvecTrafic Then
        monTraficPr�c�dent = monPlanFeuxBase.Trafic
      End If

      If Index = -1 Then
        monPlanFeuxBase = Nothing
      Else
        monPlanFeuxBase = maVariante.mPlansFeuxBase(Index)
      End If
      'Ceci obligera a recr�er la liste des Plans de fonctionnement quand on y acc�dera
      monPlanFeuxFonctionnement = Nothing

      If ConflitsInitialis�s Then
        If Me.radMatriceConflits.Checked Then
          AfficherMatriceS�curit�(0)

        ElseIf maVariante.Verrou < [Global].Verrouillage.Matrices Then
          Me.radMatriceConflits.Checked = True
        End If

        If ModeGraphique Then
          R�afficherAntagonismes()
        End If
      End If

      Recr�erDessinAntagonismes()

      Me.chkSc�narioD�finitif.Checked = monPlanFeuxBase Is maVariante.Sc�narioD�finitif
      AfficherProjetD�finitif()
      ChoisirOngletInitial(OuvertureProjet:=False)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try

  End Sub

  Private Sub AfficherProjetD�finitif()
    mdiApplication.lblProjetD�finitif.Text = IIf(Me.chkSc�narioD�finitif.Checked, "D�finitif", "Projet")
    mdiApplication.lblProjetD�finitif.ForeColor = IIf(Me.chkSc�narioD�finitif.Checked, Color.Blue, Color.Red)
  End Sub

  Private Function monTraficActif() As Trafic
    If Sc�narioEnCours() Then
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
      Return maVariante.Sc�narioCourant
    End Get
    Set(ByVal Value As PlanFeuxBase)
      maVariante.Sc�narioCourant = Value
    End Set
  End Property

  Private Function Sc�narioEnCours() As Boolean
    Return Not IsNothing(monPlanFeuxBase)
  End Function

  Private Function mesPlansPourPhasage() As PlanFeuxCollection
    Return maVariante.PlansPourPhasage
  End Function

  Private Sub frmCarrefour_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
    'Eviter d'avoir 2 fen�tres diagnostic d'affich�es
    'Mise en commentaire suite � demande du CERTU
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

  Private Sub R�affecterEchelle()
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

    'WARNING (AV : 02/09/03) : Veiller � ce que la fen�tre soit assez grande si on importe une image raster, sinon le PictureBox ne sera pas assez grand non +
    If Not pnlPalette Is Nothing Then
      D�finirD�fautLargeurPanels()
      maVariante.mParamDessin.TailleFen�tre = Me.Size
      newPosition = Me.ClientSize.Width - 8 - lgPanel(numPanel)
      Dim pMouseDeb As Point = CvPoint(picDessin.Size)
      Me.splitGraphiqueDonn�es.SplitPosition = newPosition

      If Not DiagrammeActif() Then
        pMouseDeb = Point.op_Subtraction(pMouseDeb, picDessin.Size)
        pMouseDeb.X /= 2
        pMouseDeb.Y /= 2

        'pMouseDeb = Point.op_Addition(pMouseDeb, Milieu(pMouseDeb, CvPoint(picDessin.Size)))

        cndParamDessin = mParamDessin
        mParamDessin = D�terminerNewOrigineR�ellePAN(pMouseDeb)
        cndParamDessin = mParamDessin
        Recr�erGraphique()
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
      Select Case MessageBox.Show(mdiApplication, "Voulez-vous enregistrer les modifications apport�es au projet " & maVariante.Libell�(AjoutEtoile:=False) & " ?", NomProduit, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
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
      Me.Text = maVariante.Libell�
    End Set
  End Property

  '******************************************************************************
  ' La feuille est ferm�e
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
      .mnuSc�nario.Visible = False
      .pnlSc�nario.Visible = False
      .mnuEnregistrer.Enabled = False
      .mnuEnregSous.Enabled = False
      .mnuImprimer.Enabled = False
      .mnuRafraichir.Enabled = False
    End With

  End Sub

  Private Sub frmCarrefour_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    If e.KeyCode = Keys.Delete Then
      If Not IsNothing(objS�lect) Then
        Dim unObjetM�tier As M�tier = objS�lect.ObjetM�tier
        If TypeOf unObjetM�tier Is PassagePi�ton Then
          Me.btnPi�tonMoins.PerformClick()
        ElseIf TypeOf unObjetM�tier Is LigneFeuV�hicules Then
          Me.btnLigneFeuxMoins.PerformClick()
        ElseIf TypeOf unObjetM�tier Is TrajectoireV�hicules Then
          Me.btnTrajectoireMoins.PerformClick()
        ElseIf TypeOf unObjetM�tier Is Travers�ePi�tonne Then
          Me.btnTravers�eMoins.PerformClick()
        End If
      End If
    End If
  End Sub

  Private Sub frmCarrefour_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) _
   Handles MyBase.Paint

    If ObjetEffa�ableParPaint() Then savObjS�lect = objS�lect

  End Sub
  Private Function ObjetEffa�ableParPaint() As Boolean
    If IsNothing(objS�lect) Then
      Dim unObjetM�tier As M�tier

      If TypeOf unObjetM�tier Is TrajectoireV�hicules Then
      ElseIf TypeOf unObjetM�tier Is Antagonisme Then
      Else
        Return True
      End If
    End If

  End Function
#End Region
#Region " Initialisations des panels"

  '******************************************************************************
  ' Initialiser le panel G�om�trie
  '******************************************************************************
  Private Sub InitG�om�trie()
    Dim row As Short
    Dim uneBranche As Branche
    Dim fg As GrilleDiagfeux = Me.AC1GrilleBranches

    InitStyles()

    fg.SelectionMode = Grille.SelectionModeEnum.Cell

    Me.pnlG�om�trie.BringToFront()
    fg.Rows.Count = mesBranches.Count + 1
    fg.Height = (fg.Rows.Count - 1) * 17 + 21

    'Positionner correctement le tableau d'ilots en fonction de la taille du tableau de branches et le r�duire � son en t�te
    Me.pnlIlots.Top = fg.Top + fg.Height
    Me.pnlBtnG�om�trie.Top = Me.pnlIlots.Top

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

    'Cette propri�t�, non document�e dans l'aide en ligne, mais pr�sente dans la page de propri�t�s Design, 
    'permet de visualiser ou non  le triangle indicateur du tri (glyph) dans l'ent�te de colonne
    fg.ShowSort = False

    'La grille comporte 16 styles(stock styles) : classe CellStyleCollection
    'Le style Normal et Les 15 autres bas�s sur Normal (tout changement sur Normal se r�percute sur les autres sauf modif explicite)

  End Sub
#Region " InitLignesFeux"
  Private Sub InitStyles()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleBranches

    With fg
      'D�finir les styles personnalis�s
      StyleGris� = .Styles.Add("Gris�")
      StyleGris�.BackColor = Color.LightGray     'Color.LightSlateGray : un peu de bleut� dans le gris
      StyleGris�Bool�en = .Styles.Add("Gris�Bool�en")
      StyleGris�Bool�en.BackColor = Color.LightGray     'Color.LightSlateGray : un peu de bleut� dans le gris
      StyleD�gris� = .Styles.Add("D�gris�")
      StyleGris�Gras = .Styles.Add("Gris�Gras", StyleGris�)
      Dim fntGras As New Font(StyleGris�.Font, FontStyle.Bold)
      StyleGris�Gras.Font = fntGras
      StyleD�gris�Gras = .Styles.Add("D�Gris�Gras", StyleD�gris�)
      StyleD�gris�Gras.Font = fntGras
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

        .Cols("Signal").ComboList = cndSignaux.strListe(Anticipation:=False, SansPi�tons:=ModeGraphique)
        .Cols("SignalAnticipation").ComboList = cndSignaux.strListe(Anticipation:=True)

        If ModeGraphique Then
          'Interdire la modification de certaines colonnes
          .Cols("IdVoie").AllowEditing = False
          .Cols("NbVoies").AllowEditing = False
          .Cols("TAG").AllowEditing = False
          .Cols("TD").AllowEditing = False
          .Cols("TAD").AllowEditing = False
          .Cols("IdVoie").Style = StyleGris�
          .Cols("NbVoies").Style = StyleGris�
          .Cols("TAG").Style = StyleGris�Bool�en
          .Cols("TD").Style = StyleGris�Bool�en
          .Cols("TAD").Style = StyleGris�Bool�en
          'L'affectation des styles ci-dessus a fait disparaitre la propri�t� bool�en des colonnes cases � cocher
          '   .Cols("TAD").DataType = GetType(System.Boolean)

        Else
          'Interdire la saisie des voies entrantes dans la g�om�trie) : saisie faite indirectement par les lignes de feux
          Me.AC1GrilleBranches.Cols("NbVoiesE").Style = StyleGris�
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
        'd�s le verrouillage des lignes de feux(ind�pendant du plan de feux de base)
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

    Dim fg As GrilleDiagfeux = Me.Ac1GrilleS�curit�
    Dim rg As Grille.CellRange
    Static Passage As Boolean

    If Not Passage Then

      Try
        With fg
          'Instancier les styles sp�cifiques
          StyleRouge = .Cr�erStyle(StyleRouge, "Rouge", Color.Red)
          StyleVert = .Cr�erStyle(StyleVert, "Vert", Color.LightGreen)
          StyleOrang� = .Cr�erStyle(StyleOrang�, "Orang�", Color.LightSalmon)

          With .Styles
            If IsNothing(StyleGris�) Then
              StyleGris� = .Add("Gris�")
              StyleGris�.BackColor = Color.LightGray
            End If
          End With

          If IsNothing(StyleOrang�) Then
            StyleOrang� = fg.Styles.Add("Orang�")
            StyleOrang�.BackColor = Color.LightSalmon
          End If

          'Adapter la taille de la grille au nombre de lignes de feux
          .Rows.Count = mesLignesFeux.Count + 1
          .Cols.Count = .Rows.Count
          'Rallonger la 1�re colonne pour un en-t�te un peu + long
          .Cols(0).Width = .Cols(1).Width + 10

          Dim nbCellules As Single = .Cols.Count + 0.3

          'Rajouter 10 pixels pour tenir copte de la 1�re colonne
          .Width = nbCellules * .Cols.DefaultSize + 10
          Dim LargeurGrille As Single = Math.Max(Me.pnlAntagonismes.Width, .Width)
          D�finirD�fautLargeurPanel([Global].OngletEnum.Conflits, LargeurGrille + 3 * LGMARGE)

          .Height = nbCellules * .Rows.DefaultSize
          pnlConflits.AutoScrollMinSize = New Size(lgPanel(3), 150)
          D�finirSplitPosition()

          .Left = LGMARGE
          ' d�caler si n�cessaire le panel Verrou (qui contient aussi les symboles Vert et Rouge)
          Me.pnlVerrouMatrice.Top = Math.Max(Me.pnlVerrouMatrice.Top, .Top + .Height)
          Me.pnlBoutonsRouges.Top = Me.pnlVerrouMatrice.Top

          'D�caler en cons�quence le panel Antagonismes
          With Me.pnlVerrouMatrice
            Me.pnlAntagonismes.Top = .Top + .Height
          End With
        End With

        ' Ecrire les Entete de ligne et de colonne de la matrice avec l'ID des lignes de feux
        AfficherEnteteMatriceS�curit�()

        ' Par d�faut tous les feux sont compatibles (vert)
        rg = fg.GetCellRange(1, 1, fg.Rows.Count - 1, fg.Cols.Count - 1)
        rg.Style = StyleVert

        'Le phasage peut aussi �tre dimensionn� d�s le verrouillage des lignes de feux
        AfficherOrganisationPhasage()

        Passage = True

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Initialisation des conflits")
      End Try

    End If

    If Sc�narioEnCours() AndAlso Not ConflitsInitialis�s Then
      If ModeGraphique Then
        AntagonismesEnCours = True
        InitAntagonismes()
        AntagonismesEnCours = False
      Else
        Me.pnlAntagonismes.Visible = False
      End If

      'S�lectionner l'option Matrice des conflits
      If Me.radMatriceConflits.Checked Then
        AfficherMatriceS�curit�(0)
      Else
        Me.radMatriceConflits.Checked = True
      End If
      ConflitsInitialis�s = True
    End If

  End Sub

  '*********************************************************************************************
  'Afficher les ID de lignes de feux en t�te de ligne et de colonne de la matrice de s�curit�
  '*********************************************************************************************
  Private Sub AfficherEnteteMatriceS�curit�()
    Dim uneLigneFeux As LigneFeux
    Dim row, col As Short
    Dim ID As String
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleS�curit�

    Try
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        col = row
        ID = uneLigneFeux.ID

        'Mettre l'identignfiant dans la 1�re colonne (fixe)
        fg(row, 0) = mesBranches.ID(uneLigneFeux.mBranche) & "-" & ID
        'Mettre l'identifiant en t�te de colonne
        fg(0, col) = ID

      Next

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherEnteteMatriceS�curit�")
    End Try

  End Sub

  '*********************************************************************************************
  'Afficher les ID de lignes de feux en t�te de ligne et de colonne de la matrice de s�curit�
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
        If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
          'Pas de choix propos� pour les conflits syst�matiques
          fg.Rows(row).Visible = False
        Else
          If unAntagonisme Is unAntagonisme.M�mesCourants Then
            mAntagonismes.NonTousSyst�matiques = True
          Else
            'On n'affiche qu'une ligne dans la grille pour tous les antagonismes de m�me courant
            fg.Rows(row).Visible = False
          End If
          fg(row, 0) = unAntagonisme.Libell�(Antagonisme.PositionEnum.Premier, mesBranches)
          fg(row, 1) = unAntagonisme.Libell�(Antagonisme.PositionEnum.Dernier, mesBranches)
          rg = fg.GetCellRange(row, 2)
          D�finirStyle(unAntagonisme, rg)
        End If
      End With
    Next

    ' on n'affiche pas la grille des antagonismes s'il n'y a que des conflits syst�matiques
    Me.pnlAntagonismes.Visible = mAntagonismes.NonTousSyst�matiques

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
      'S�lectionner la 1�re branche
      .SelectedIndex = 0
    End With

    Me.btnR�initAntago.Enabled = mAntagonismes.ConflitsPartiellementR�solus

  End Sub

  Private Sub D�finirStyle(ByVal unAntagonisme As Antagonisme, ByVal rg As Grille.CellRange)

    If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
      rg.Style = StyleOrang�
    Else
      rg.Style = StyleD�gris�
    End If

    If unAntagonisme.Autoris� Then
      rg.Checkbox = Grille.CheckEnum.Checked
    Else
      rg.Checkbox = Grille.CheckEnum.Unchecked
    End If

  End Sub

  Private Sub R�afficherLibell�sAntagonismes()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim row As Short

    For Each unAntagonisme In mAntagonismes()
      row += 1
      With unAntagonisme
        If unAntagonisme.TypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique Then
          fg(row, 0) = unAntagonisme.Libell�(Antagonisme.PositionEnum.Premier, mesBranches)
          fg(row, 1) = unAntagonisme.Libell�(Antagonisme.PositionEnum.Dernier, mesBranches)
        End If
      End With
    Next

  End Sub

  Private Sub btnR�initAntago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnR�initAntago.Click
    If Confirmation("R�initialiser les antagonismes", Critique:=False) Then
      R�initialiserAntagonismes()
    End If
  End Sub

  Private Sub R�initialiserAntagonismes()
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes
    Dim unAntagonisme As Antagonisme
    Dim row As Short
    Dim rg As Grille.CellRange

    For Each unAntagonisme In mAntagonismes()
      row += 1
      With unAntagonisme
        If unAntagonisme.TypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique Then
          rg = fg.GetCellRange(row, 2)
          rg.Data = False
          rg.Style = StyleOrang�
          unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible
        End If
      End With
    Next

    mLignesFeux.R�initialiserAntagos(mesLignesFeux)

    AfficherAntagosDansMatrice(Me.Ac1GrilleS�curit�)

    Me.btnR�initAntago.Enabled = False

  End Sub

#End Region
#Region " InitTrafics"
  '******************************************************************************
  ' Initialiser le panel Matrices de trafic
  '******************************************************************************
  Private Function InitTrafics() As Boolean
    Dim uneBranche As Branche
    Dim fg As GrilleDiagfeux = Me.AC1GrilleTraficV�hicules
    Dim fgP As GrilleDiagfeux = Me.Ac1GrilleTraficPi�tons
    Dim rg As Grille.CellRange
    Dim row As Short
    Dim unStyle As Grille.CellStyle
    Static Passage As Boolean = False

    If Not Passage Then

      Try

        With fg

          With .Styles
            'Instancier les styles sp�cifiques
            If IsNothing(StyleGris�) Then
              StyleGris� = .Add("Gris�")
              StyleGris�.BackColor = Color.LightGray
            End If
            If IsNothing(StyleGris�Rouge) Then
              StyleGris�Rouge = .Add("Gris�Rouge", StyleGris�)
              StyleGris�Rouge.ForeColor = Color.Red
            End If
          End With

          'Cr�er une ligne de trafic par branche du carrefour
          For Each uneBranche In mesBranches
            row = mesBranches.IndexOf(uneBranche) + 1
            fg(row, 0) = mesBranches.ID(uneBranche)
            fg.Rows.Add()
          Next
          fg(mesBranches.Count + 1, 0) = "TS"     ' Intitul� trafic sortant

          'Supprimer les colonnes au-dela du nombre de branches(L'initialisation est faite sur 6 branches)
          fg.Cols.RemoveRange(mesBranches.Count + 1, count:=6 - mesBranches.Count)
          'Idem pour les pi�tons
          fgP.Cols.RemoveRange(mesBranches.Count, count:=6 - mesBranches.Count)

          'Mettre en gris� la derni�re ligne (totaux  sortant)
          rg = fg.GetCellRange(mesBranches.Count + 1, 1, mesBranches.Count + 1, mesBranches.Count + 1)
          rg.Style = StyleGris�
          '
          'Mettre en gris� la derni�re colonne(totaux  entrant)
          rg = fg.GetCellRange(1, mesBranches.Count + 1, mesBranches.Count, mesBranches.Count + 1)
          rg.Style = StyleGris�

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
            rg.Style = StyleGris�
            If uneBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
              rg = fg.GetCellRange(1, numBranche, .Count, numBranche)
              rg.Style = StyleGris�
              fg.Cols(numBranche).AllowEditing = False
            ElseIf uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              rg = fg.GetCellRange(numBranche, 1, numBranche, .Count)
              rg.Style = StyleGris�
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
      If Not ChargementEnCours AndAlso Sc�narioEnCours() AndAlso Not monPlanFeuxBase.AvecTrafic Then
        AfficherMessageErreur(Me, "Le sc�nario " & monPlanFeuxBase.Nom & " ne comporte pas de trafic")
        Return True

      ElseIf maVariante.mTrafics.Count = 0 Then
        'Premier appel de l'onglet trafic pour ce projet : cr�er une premi�re p�riode de trafic
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
  Private Function InitPhasage(Optional ByVal R�initialisation As Boolean = False) As Boolean

    If monPlanFeuxBase.PhasageInitialis� Then
      Pr�parerPhasage()

    Else
      Try
        Dim unPlanFeux As PlanFeuxPhasage

        'Rechercher les plans pour phasage trop longs (> 130s) : � ne pas proposer
        monPlanFeuxBase.Compl�mentOrganiserPhasage(False)

        Dim nbSc�narios As Short = mesPlansPourPhasage.Count

        If nbSc�narios = 0 Then
          AfficherMessageErreur(Me, "Tous les phasages possibles conduisent � un temps d'attente sup�rieur � " & AttenteMax & vbCrLf & "Revoir les trafics ou changer le plan de circulation")
          Return True
        End If

        Pr�parerPhasage()

        monPlanFeuxBase.PhasageInitialis� = True

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "InitPhasage")
      End Try

    End If
  End Function

  Private Sub Pr�parerPhasage()
    Dim nbSc�narios As Short = mesPlansPourPhasage.Count
    Dim unPlanFeux As PlanFeuxPhasage

    Me.lblD�coupagePhases.Text = IIf(nbSc�narios = 1, "Phasage unique propos�", CStr(nbSc�narios) & " phasages possibles")
    AffichagePhasesEnCours = True

    Me.cboD�coupagePhases.Visible = mesPlansPourPhasage.Count > 1

    Me.chk3Phases.Enabled = True

    With mFiltrePhasage()
      'Modif AV 27/03/07 : par d�faut ne proposer que les 2 phases
      'Me.chk3Phases.Checked = .TroisPhases
      Me.cbolLFMultiPhases.SelectedIndex = .LigneFeuxMultiPhases
      Me.cboPhasesSp�ciales.SelectedIndex = .AvecPhaseSp�ciale

      If IsNothing(monTraficActif) Then
        Me.cboR�serveCapacit�.Enabled = False
        Me.cboR�serveCapacit�.SelectedIndex = -1
        Me.txtR�serveCapacit�PourCent.Visible = False
      Else
        Me.cboR�serveCapacit�.Enabled = True
        Me.cboR�serveCapacit�.SelectedIndex = .Crit�reCapacit�
        Me.txtR�serveCapacit�PourCent.Visible = True
      End If
    End With

    For Each unPlanFeux In mesPlansPourPhasage()
      With unPlanFeux
        If .mPhases.Count > 2 Then
          'Modif AV 27/03/07 : inutile suite � la modif ci-dessus
          '   Me.chk3Phases.Enabled = True
          If unPlanFeux.PlanBaseAssoci� Is monPlanFeuxBase Then
            Me.chk3Phases.Checked = True
          End If
        End If
      End With
    Next

    AffichagePhasesEnCours = False
    AfficherComboPhasage()

    If cboD�coupagePhases.Items.Count = 0 And Not Me.chk3Phases.Checked Then
      'Aucun feu � 2 phases : recommencer avec les 3 phases
      Me.chk3Phases.Checked = True
    End If

  End Sub

#End Region
#Region "InitPlanFeux"
  '***************************************************************************************************
  ' Initialiser le panel Plan de feux de base (sous-panel de Plan de feux)
  ' RecalculerMini : Recalcule les valeurs mini des plans de feux suite � la modif des verts mini
  '****************************************************************************************************
  Private Sub InitPlanFeuxBase(Optional ByVal RecalculerMini As Boolean = False)

    Try

      If RecalculerMini Then
        '//DIAGFEUX//
        'Dans le cas des sc�narios, il ne faut plus que calculer les plans de phasage du sc�nario
        monPlanFeuxBase.CalculerDur�esMiniPlansFeux()

      Else
        monPlanFeuxActif = monPlanFeuxBase
      End If

      Me.txtVertMiniV�hicule.Text = maVariante.VertMiniV�hicules
      Me.txtVertMiniPi�ton.Text = maVariante.VertMiniPi�tons

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
#Region " Fonctions partag�es"
  Private Sub ChoisirOngletInitial(Optional ByVal OuvertureProjet As Boolean = True)
    Dim unOnglet As TabPage
    Dim OngletActif As TabPage = Me.tabOnglet.SelectedTab
    Dim TraficInterdit As Boolean

    ChangementDeSc�nario = True
    Me.chkVerrouMatrice.Checked = maVariante.Verrou >= [Global].Verrouillage.Matrices

    If Sc�narioEnCours() AndAlso monPlanFeuxBase.AvecTrafic Then
      Me.cboTrafic.Text = monPlanFeuxBase.Nom
      Me.tabTrafics.Enabled = True
    Else
      Me.cboTrafic.SelectedIndex = -1
      Me.tabTrafics.Enabled = False
    End If

    Select Case maVariante.Verrou
      Case [Global].Verrouillage.Aucun
        G�rerChangementOnglet()
        unOnglet = Me.tabG�om�trie

      Case [Global].Verrouillage.G�om�trie
        Me.chkVerrouG�om�trie.Checked = True

        If Sc�narioEnCours() Then
          'Forc�ment un sc�nario avec trafic � ce stade d'avancement du projet
          unOnglet = Me.tabTrafics
        Else
          unOnglet = Me.tabLignesDeFeux
        End If

      Case [Global].Verrouillage.LignesFeux
        Me.chkVerrouG�om�trie.Checked = True
        Me.chkVerrouLignesFeux.Checked = True

        If Sc�narioEnCours() Then
          If monPlanFeuxBase.AvecTrafic Then
            If monTraficActif.Verrouill� Then
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
        Me.chkVerrouG�om�trie.Checked = True
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
      ElseIf OngletAssoci�(OngletActif) > OngletAssoci�(unOnglet) Then
        'L'onglet en cours n'est pas acceptable pour ce sc�nario
        Me.tabOnglet.SelectedTab = unOnglet
      ElseIf TraficInterdit Then
        Me.tabOnglet.SelectedTab = unOnglet
      ElseIf Not ChoisirPanel() Then
        'Conserver l'onglet courant mais redessiner en fonction du sc�nario
        maVariante.Verrouiller()
        Redessiner()
      End If

    End If

    ChangementDeSc�nario = False

  End Sub

  '****************************************************************************************
  'ChoisirPanel : le changement de sc�nario peut conduire � conserver l'onglet en cours
  'Il faut analyser si le panel en cours peut �tre conserv�
  'Retourne True si on a chang� de panel
  '****************************************************************************************
  Private Function ChoisirPanel() As Boolean

    If Me.tabOnglet.SelectedTab Is Me.tabConflits Then
      InitConflits()
      If pnlConflitsIndex <> 0 AndAlso Not monPlanFeuxBase.Verrou = [Global].Verrouillage.Matrices Then
        'La matrice des conflits n'est encore pas verrouill�e pour ce sc�nario 
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
      'En mode manuel, on cr�e une ligne de feux vide suppl�mentaire pour permettre la saisie d'une nouvelle ligne de feux
      If fg.Rows.Count < mesLignesFeux.Count + 2 And Not maVariante.VerrouLigneFeu Then fg.Rows.Add()
    End If


  End Sub

  '******************************************************************************
  ' Afficher les diff�rents champs d'une ligne de feux dans la ligne de la grille
  '******************************************************************************
  Private Sub AfficherLigneDeFeux(ByVal uneLigneFeux As LigneFeux, Optional ByVal fg As GrilleDiagfeux = Nothing)
    If IsNothing(fg) Then fg = Me.AC1GrilleFeux
    Dim row As Short = mesLignesFeux.IndexOf(uneLigneFeux) + 1

    'Si besoin, cr�er la ligne dans la grille
    If row >= fg.Rows.Count Then
      fg.Rows.Add()
    Else
      fg.Rows(1).Visible = True
    End If

    'Rechercher la ligne de la grille adapt�e
    Dim rg As Grille.CellRange = fg.TouteLaLigne(row)
    'Afficher les donn�es dans la ligne
    rg.Clip = uneLigneFeux.strLigneGrille(mesBranches, S�parateur:=Chr(9))

    GriserLignePi�tons(fg, row, uneLigneFeux.EstPi�ton)

  End Sub

  '******************************************************************************
  ' Ins�rer la ligne de feux dans le tableau des lignes de feux
  '******************************************************************************
  Private Sub Ins�rerLigneDeFeux(ByVal Position As Short, ByVal uneLigneFeux As LigneFeuV�hicules)
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    SelectObject = True
    'Ne pas ins�rer de ligne si c'est la premi�re ligne de feux(toutes lignes de feux confondues), car on garde toujours au moins une ligne 'vide'
    If fg.Rows.Count > 2 Or mesLignesFeux.Count > 1 Then
      fg.Rows.Insert(Position + 1)
    End If
    AfficherLigneDeFeux(uneLigneFeux, fg)

    Position += 1
    If Position < fg.Rows.Count - 1 Then
      GriserLignePi�tons(fg, Position + 1, mesLignesFeux(CType(Position, Short)).EstPi�ton)
    End If

    SelectObject = False
  End Sub

  '******************************************************************************
  ' D�terminer s'il faut griser les cases non concern�es si ligne de feux pi�tonne
  '******************************************************************************
  Private Sub GriserLignePi�tons(ByVal fg As Grille.C1FlexGrid, ByVal numLigne As Short, ByVal EstPi�ton As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle
    Static StyleMasqu� As Grille.CellStyle

    If ModeGraphique Then
      'Seules les colonnes Signal et Signal associ� peuvent basculer selon que la ligne est pi�tons ou v�hicule
      'les autres sont soient autoris�es soient interdites d�s le d�part
      rg = fg.GetCellRange(numLigne, 3, numLigne, 4)

    Else
      If maVariante.Verrou = [Global].Verrouillage.G�om�trie Then
        'Colonne  : Signal associ� - Nombres de voies et TAD,TAG,TD
        rg = fg.GetCellRange(numLigne, 4, numLigne, 8)

      Else
        'Colonne : Signal (les suivantes ainsi que la 1�re sont d�j� inhib�es lors du verrouillage
        'Le signal peut par contre �tre modifi� pour un v�hicule m�me apr�s verrouillage
        rg = fg.GetCellRange(numLigne, 3)
      End If
    End If

    'D�terminer le style selon que la ligne de feux est v�hicules ou pi�tons
    If EstPi�ton Then
      unStyle = StyleGris�
    Else
      unStyle = StyleD�gris�
    End If

    If IsNothing(rg.Style) Then
      'Faire une initialisation du style(avec n'importe quoi)
      rg.Style = StyleD�gris�
    End If

    'Appliquer le style
      rg.Style = unStyle

    'If ModeGraphique And Not EstPi�ton Then
    '  rg = fg.GetCellRange(numLigne, 7)
    '  unStyle = StyleD�gris�
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

        'Instancier les styles sp�cifiques
        If IsNothing(StyleRouge) Then
          If IsNothing(StyleGris�) Then
            StyleGris� = .Add("Gris�")
            StyleGris�.BackColor = Color.LightGray
          End If
          StyleRouge = .Add("Rouge")
          StyleRouge.BackColor = Color.Red
          StyleVert = .Add("Vert")
          StyleVert.BackColor = Color.LightGreen
        End If

        If IsNothing(StyleOrang�) Then
          StyleOrang� = .Add("Orang�")
          StyleOrang�.BackColor = Color.LightSalmon

        End If
      End With      ' fg.Styles

      'Adapter la taille de la grille au nombre de lignes de feux
      .Rows.Count = mesLignesFeux.Count + 1

      .Cols.Count = MAXPHASES + 1
      .D�finirLargeurGrille()
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

      'Mettre l'identifiant dans la 1�re colonne (fixe)
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
    Dim lbFigerDur�e, lblPhase3 As Label
    Dim updD�calageOuverture, updD�calageFermeture As NumericUpDown
    Dim txtDur�eCycle As TextBox
    Dim PlanBase As Boolean = unPlanFeux Is monPlanFeuxBase


    Try

      If PlanBase Then
        updPhase1 = Me.updPhase1Base
        updPhase2 = Me.updPhase2Base
        updPhase3 = Me.updPhase3Base
        lbFigerDur�e = Me.lbFigerDur�eBase
        radVerrou1 = Me.radPhase1Base
        radVerrou2 = Me.radPhase2Base
        radVerrou3 = Me.radPhase3Base
        lblPhase3 = Me.lblPhase3Base
        updD�calageOuverture = Me.updD�calageOuvertureBase
        updD�calageFermeture = Me.updD�calageFermetureBase
        txtDur�eCycle = Me.txtDur�eCycleBase

      Else
        updPhase1 = Me.updPhase1Fct
        updPhase2 = Me.updPhase2Fct
        updPhase3 = Me.updPhase3Fct
        lbFigerDur�e = Me.lbFigerDur�eFct
        radVerrou1 = Me.radPhase1Fct
        radVerrou2 = Me.radPhase2Fct
        radVerrou3 = Me.radPhase3Fct
        lblPhase3 = Me.lblPhase3Fct
        updD�calageOuverture = Me.updD�calageOuvertureFct
        updD�calageFermeture = Me.updD�calageFermetureFct
        txtDur�eCycle = Me.txtDur�eCycleFct
        If unPlanFeux.Capacit�ACalculer Then
          unPlanFeux.CalculerR�serveCapacit�()
        End If
      End If

      AfficherTableauPlanFeux(unPlanFeux)

      'Verrouillage de phase inutile si 2 phases seulement
      Dim Visibilit�Verrou As Boolean = (desPhases.Count = 3)
      lbFigerDur�e.Visible = Visibilit�Verrou
      radVerrou1.Visible = Visibilit�Verrou
      radVerrou2.Visible = Visibilit�Verrou
      radVerrou3.Visible = Visibilit�Verrou
      updPhase3.Visible = Visibilit�Verrou
      lblPhase3.Visible = Visibilit�Verrou

      For Each unePhase In desPhases
        Select Case desPhases.IndexOf(unePhase)
          Case 0
            upd = updPhase1
          Case 1
            upd = updPhase2
          Case 2
            upd = updPhase3
            radAssoci�Phase(unePhase).Checked = True
        End Select

        upd.Tag = Nothing
        upd.Minimum = unePhase.Dur�eIncompressible
        upd.Value = unePhase.Dur�e
        'Cette instruction doit �tre mise en dernier pour que l'�v�nement updPhase_ValueChanged ne fasse rien
        upd.Tag = unePhase
      Next

      updD�calageOuverture.Tag = unPlanFeux
      updD�calageFermeture.Tag = unPlanFeux
      updD�calageOuverture.Value = 0
      updD�calageFermeture.Value = 0
      txtDur�eCycle.Text = CStr(unPlanFeux.Dur�eCycle)

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
        lstItems = Me.lvwDur�eVert.Items
      Else
        lstItems = Me.lvwDur�eVertFct.Items
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
        lstItems = Me.lvwDur�eVert.Items
      Else
        lstItems = Me.lvwDur�eVertFct.Items
      End If

      lstItems.Clear()
      For Each uneLigneFeux In unPlanFeux.mLignesFeux
        For Each unePhase In desPhases
          If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
            If unPlanFeux.PositionDansPhase(uneLigneFeux, unePhase) <> PlanFeux.Position.Derni�re Then
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
              'il faut recalculer le vert de la ligne de feux(fonction des dur�es de phases et des d�calages)

              itmX.SubItems(2).Text = unPlanFeux.Dur�eVert(uneLigneFeux)

              If uneLigneFeux.EstV�hicule Then
                itmX.SubItems(3).Text = "X"
              Else
                itmX.SubItems(3).Text = unPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Ouverture)
              End If
              itmX.SubItems(4).Text = unPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Fermeture)
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
  ' G�rer l'activation des boutons du panel lignes de Feux
  '**********************************************************************************************************************
  Private Sub ActiverBoutonsLignesFeux()

    Me.chkVerrouLignesFeux.Enabled = mesLignesFeux.nbLignesV�hicules >= IIf(ModeGraphique, mesBranches.NbLignesFeuxMini, 2)

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
  '============================== D�but des fonctions graphiques =====================================================
  '******************************************************************************
  ' Repeindre le graphique
  '******************************************************************************

  Private Sub TenterSuppressionObjet()

    If Not IsNothing(objS�lect) Then
      S�lD�s�lectionner(PourS�lection:=True)
      Select Case UneCommandeGraphique
        Case CommandeGraphique.SupprimerPassage
          Me.btnPi�tonMoins.PerformClick()
        Case CommandeGraphique.SupprimerTrajectoire
          Me.btnTrajectoireMoins.PerformClick()
        Case CommandeGraphique.SupprimerLigneFeu
          Me.btnLigneFeuxMoins.PerformClick()
      End Select
    End If

  End Sub

  Private Function RechercherPassage(ByVal p As Point) As PassagePi�ton
    Dim uneBranche As Branche
    Dim unPassage As PassagePi�ton

    For Each uneBranche In mesBranches
      unPassage = uneBranche.RecherPassage(p)
      If Not IsNothing(unPassage) Then Exit For
    Next

    If IsNothing(unPassage) Then
      AfficherMessageErreur(Me, "D�signer un passage pi�ton")
    Else
      Select Case UneCommandeGraphique
        Case CommandeGraphique.Travers�e
          BrancheLi�e = unPassage.mBranche
        Case CommandeGraphique.D�composerTravers�e, CommandeGraphique.PropTravers�e
          Travers�e = unPassage.mTravers�e
      End Select
    End If

    Return unPassage

  End Function

  Private Function RechercherObject(ByVal p As Point) As Graphique
    Dim uneS�lection As Graphique
    Dim unObjetM�tier As M�tier
    Dim fg As GrilleDiagfeux
    Dim numColonne As Short = 1

    'Traitement pr�alable sur l'objet pr�alablement s�lectionn�
    If Not IsNothing(objS�lect) Then
      unObjetM�tier = objS�lect.ObjetM�tier
      If TypeOf unObjetM�tier Is PassagePi�ton Then
        Dim unPassage As PassagePi�ton = unObjetM�tier
        If Not IsNothing(unPassage.Zebras) Then DessinerObjet(unPassage.Zebras)
      ElseIf TypeOf unObjetM�tier Is Antagonisme Then
        fg = Me.AC1GrilleAntagonismes
        fg.Row = -1
      End If
    End If

    'Rechercher si un objet est s�lectionn�
    uneS�lection = colObjetsGraphiques.RechercherObject(p, PointCliqu�)

    If Not IsNothing(uneS�lection) Then
      'Mettre en surbrillance dans la grille ad�quate la ligne correspondant � l'objet s�lectionn�
      SelectObject = True
      unObjetM�tier = uneS�lection.ObjetM�tier
      Dim Index As Short

      If TypeOf unObjetM�tier Is Branche Then
        Dim uneBranche As Branche = unObjetM�tier
        Index = mesBranches.IndexOf(uneBranche) + 1
        fg = Me.AC1GrilleBranches

      ElseIf TypeOf unObjetM�tier Is Ilot Then
        Dim unIlot As Ilot = unObjetM�tier
        Index = mesBranches.IndexIlot(unIlot)
        fg = Me.AC1GrilleIlot

      ElseIf TypeOf unObjetM�tier Is LigneFeuV�hicules Then
        Dim uneLigneFeux As LigneFeuV�hicules = unObjetM�tier
        Index = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        fg = Me.AC1GrilleFeux
      ElseIf TypeOf unObjetM�tier Is Travers�ePi�tonne Then
        Dim uneLigneFeux As LigneFeux = CType(unObjetM�tier, Travers�ePi�tonne).LigneFeu
        Index = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        fg = Me.AC1GrilleFeux

      ElseIf TypeOf unObjetM�tier Is PassagePi�ton Then
        Dim unPassage As PassagePi�ton = unObjetM�tier
        If Not IsNothing(unPassage.Zebras) Then EffacerObjet(unPassage.Zebras)
      ElseIf TypeOf unObjetM�tier Is Antagonisme Then
        Dim unAntagonisme As Antagonisme = unObjetM�tier
        'Pour les antagonismes comportant les m�mes courants, un seul est affich� dans la grille : c'est celui-ci qu'il faut rechercher
        Index = mAntagonismes.IndexOf(unAntagonisme.M�mesCourants) + 1
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

    Return uneS�lection

  End Function

  '******************************************************************************
  ' Montre ou cache les poign�es de s�lection
  '******************************************************************************
  Private Sub S�lD�s�lectionner(Optional ByVal PourS�lection As Boolean = False)
    Dim Index As Short

    'DessinerPoign�e(PointCliqu�, ptCliqu�:=True)

    Dim unObjetM�tier As M�tier = objS�lect.ObjetM�tier

    If Not IsNothing(unObjetM�tier) Then
      Debug.WriteLine(unObjetM�tier.GetType.FullName)
    End If
    Debug.WriteLine(PourS�lection.ToString)

    If TypeOf unObjetM�tier Is TrajectoireV�hicules Or TypeOf unObjetM�tier Is Antagonisme Then
      EffacerObjet(objS�lect)
      objS�lect.Pointillable = PourS�lection
      DessinerObjet(objS�lect)
      If TypeOf unObjetM�tier Is TrajectoireV�hicules Then
        objS�lect = CType(unObjetM�tier, TrajectoireV�hicules).PolyManuel
        If IsNothing(objS�lect) Then objS�lect = CType(unObjetM�tier, TrajectoireV�hicules).LigneAcc�s
        S�lD�s�lectionner(PourS�lection)
        objS�lect = unObjetM�tier.mGraphique
      End If

    ElseIf TypeOf unObjetM�tier Is Variante Then
      'Pas de poign�es de s�lection pour l'ensemble du carrefour
    Else
      For Index = 0 To objS�lect.NbPoign�es - 1
        DessinerPoign�e(objS�lect.Poign�e(Index))
      Next
    End If

  End Sub

  '******************************************************************************
  ' Dessiner le curseur �lastique sur le graphique
  '******************************************************************************
  Private Sub DessinerElastique(Optional ByVal Texte As String = Nothing)
    Dim i, nbPoints As Short

    If NePasEffacer Then
      NePasEffacer = False

    Else
      nbPoints = mScreen.Length
      Select Case UneCommandeGraphique
        Case CommandeGraphique.D�placerCarrefour, CommandeGraphique.ZoomPAN
          Dim numBranche As Short
          For numBranche = 0 To maVariante.mBranches.Count - 1
            DessinerReversible(mScreen(4 * numBranche), mScreen(4 * numBranche + 1))
            DessinerReversible(mScreen(4 * numBranche + 2), mScreen(4 * numBranche + 3))
          Next

        Case CommandeGraphique.OrigineBranche, CommandeGraphique.AngleBranche, _
        CommandeGraphique.PositionTrafic, _
        CommandeGraphique.D�placerLigneFeu, CommandeGraphique.AllongerFeu, _
        CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure, _
        CommandeGraphique.D�placerNord, CommandeGraphique.OrienterNord, CommandeGraphique.D�placerEchelle
          DessinerReversible(mScreen(0), mScreen(1))

        Case CommandeGraphique.EtirerIlot, CommandeGraphique.D�placerIlot, CommandeGraphique.ElargirIlot, _
              CommandeGraphique.D�placerPassage, _
              CommandeGraphique.D�placerSignal
          Dim IndiceMax As Short = nbPoints - 1
          For i = 0 To IndiceMax
            DessinerReversible(mScreen(i), mScreen((i + 1) Mod nbPoints))
          Next
          If UneCommandeGraphique = CommandeGraphique.D�placerSignal Then
            DessinerReversible(mScreen1, mScreen2)
          End If

        Case CommandeGraphique.EditerTrajectoire
          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            If Distance(mScreen1, mScreen2) <= RayS�lect Then
              'Souris proche du point destination :allumer la poign�e pour inciter � cliquer dessus pour terminer la commande
              DessinerPoign�e(mPoint1)
            Else
              'Eteindre la poign�e
              DessinerPoign�e(mPoint1, True)
            End If
          End If
          Dim Indice As Short = mScreen.Length - 1
          If Indice = 2 Then
            'Pas encore de point manuel cr�� : segment entre le point origine et la souris
            DessinerReversible(mScreen(0), mScreen(1))
          Else
            'Dessiner les 2 segments reliant le point pr�c�dent avec la souris, et le point destination avec la souris
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
        Case CommandeGraphique.PassagePi�ton, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          'If Not Texte = "paint" Then
          DessinerReversible(mScreen1, mScreen2)
          If mScreen.Length >= 3 Then ' Fin du passage pi�ton
            DessinerReversible(mScreen(0), mScreen2)
          End If

        Case Else
          If PourFrame Then DessinerFrame()
          If UnCarr� Then

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
  ' Dessiner une frame �lastique sur le graphique
  '******************************************************************************
  Private Sub DessinerFrame()
    Dim rc As New Rectangle
    Dim pScreen1, pScreen2 As Point

    'D�finir le coin haut gauche du rectangle � partir des points M1 et M2
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
    'D�finir la taille du rectangle � partir des points M1 et M2
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

    R�activerS�lect()
    Dim pSouris As New Point(e.X, e.Y)

    If UneCommandeGraphique = CommandeGraphique.Antagonisme Then D�marrerCommande(CommandeGraphique.AucuneCommande)

    Select Case UneCommandeGraphique

      Case CommandeGraphique.AucuneCommande

        If IsNothing(objS�lect) Then
          objS�lect = RechercherObject(pSouris)
        Else
          S�lD�s�lectionner(PourS�lection:=False)  ' Montre ou cache les poign�es de s�lection
          Dim NewObject As Graphique = RechercherObject(pSouris)
          If Not NewObject Is objS�lect Then
            objS�lect = NewObject
          End If
        End If

        If Not IsNothing(objS�lect) Then
          S�lD�s�lectionner(PourS�lection:=True)

          If mPoint.Length = 0 Then
            If TypeOf objS�lect.ObjetM�tier Is Antagonisme Then
              D�marrerCommande(CommandeGraphique.Antagonisme)
            End If
          Else
            'La souris a d�j� survol� un point permettant l'ex�cution d'une commande
            InitialiserCommande(pSouris)
            '          D�marrerDrag
            mDragging = True
            Select Case UneCommandeGraphique
              Case CommandeGraphique.AllongerFeu, CommandeGraphique.D�placerLigneFeu, CommandeGraphique.D�placerSignal
                D�s�lectionner()
                If UneCommandeGraphique = CommandeGraphique.D�placerSignal Then
                  EffacerObjet(SignalFeuEnCours.mGraphique)
                Else
                  EffacerObjet(LigneFeuEnCours.Dessin)
                  EffacerObjet(LigneFeuEnCours.mSignalFeu(0).mGraphique)
                End If
            End Select
            DessinerElastique()
          End If
        End If

      Case CommandeGraphique.PassagePi�ton, CommandeGraphique.Mesure
        If mPoint.Length = 0 Then
          ' Cr�ation du passage : l'utilisateur a juste d�clench� la commande avec btnPassage
          ' Outil Mesure : l'outil attend le point de r�f�rence
          EnAttenteMouseUp = True
        End If


      Case CommandeGraphique.Trajectoire
        EnAttenteMouseUp = True
        If Not IsNothing(objS�lect) Then
          S�lD�s�lectionner(PourS�lection:=True)
          objS�lect = RechercherObject(pSouris)
        End If
      Case CommandeGraphique.LigneFeux, CommandeGraphique.PassagePi�tonRapide
        EnAttenteMouseUp = True

      Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu
        objS�lect = RechercherObject(pSouris)
        TenterSuppressionObjet()

      Case CommandeGraphique.Travers�e, CommandeGraphique.D�composerTravers�e, CommandeGraphique.PropTravers�e
        Dim unPassage As PassagePi�ton = RechercherPassage(pSouris)

        If Not IsNothing(unPassage) Then TerminerCommande(pSouris)
        D�marrerCommande(CommandeGraphique.AucuneCommande)

      Case CommandeGraphique.PropTrajectoire
        objS�lect = RechercherObject(pSouris)
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

    R�activerS�lect()

    ' M�moriser le nouveau point
    Dim pSouris As Point = New Point(e.X, e.Y)
#If DEBUG Then
    Me.Label1.Text = PointR�el(pSouris).ToString
    Me.Label2.Text = pSouris.ToString
    Me.Label3.Text = pSouris.ToString
    Me.Label4.Text = pSouris.ToString
#End If
    Dim pF As PointF = PointR�el(pSouris)
    If Not IsNothing(monFDP) Then
      mdiApplication.staDiagfeux.Panels(1).Text = "X = " & Format(pF.X, "0.##") & ", Y = " & Format(pF.Y, "0.##")
    End If

    If mDragging Then
      'l 'op�ration de glissage a d�j� commenc�
      If Not PointDansPicture(pSouris) Then Exit Sub
      ' Effacer la ligne pr�c�dente
      DessinerElastique("Move")

      Try
        Select Case UneCommandeGraphique
          Case CommandeGraphique.D�placerNord, CommandeGraphique.D�placerEchelle
            TranslaterMscreen(pSouris)
          Case CommandeGraphique.OrienterNord
            mScreen(1) = RecalculermScreen(CentreRotation, pSouris, LongueurSegment)

          Case CommandeGraphique.OrigineBranche
            If OrigineBrancheOK(pSouris) Then
              TranslaterMscreen(pSouris)
            End If

          Case CommandeGraphique.D�placerPassage, CommandeGraphique.D�placerCarrefour, CommandeGraphique.ZoomPAN
            mScreen(0) = RecalculermScreen(pSouris)
          Case CommandeGraphique.AllongerFeu, CommandeGraphique.Mesure
            mScreen(1) = RecalculermScreen(pSouris)
            mdiApplication.staDiagfeux.Panels(1).Text = Format(DistanceR�elle(mScreen(0), mScreen(1)), "0.##") & " m"

          Case CommandeGraphique.D�placerSignal
            TranslaterMscreen(pSouris)
          Case CommandeGraphique.AngleBranche
            If AngleBrancheOK(pSouris) Then
              mScreen(1) = RecalculermScreen(CentreRotation, pSouris, LongueurSegment)
            End If

          Case CommandeGraphique.PassagePi�ton, CommandeGraphique.LigneFeux
            mScreen2 = RecalculermScreen(pSouris)
          Case CommandeGraphique.Trajectoire
            mScreen2 = RecalculermScreen(pSouris)
          Case CommandeGraphique.EtirerIlot, CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage
            mScreen(0) = RecalculermScreen(pSouris)
          Case CommandeGraphique.EditPointPassage
            mScreen(0) = RecalculermScreen(pSouris)

          Case CommandeGraphique.ElargirIlot
            mScreen(1) = RecalculermScreen(pSouris)
            mScreen(2) = Sym�trique(mScreen(1), mScreen(4))
          Case CommandeGraphique.D�placerIlot
            If P2IlotOK(pSouris) Then
              RecalculerMscreenIlot(pSouris)
            End If
          Case CommandeGraphique.EditerTrajectoire
            'M�moriser la nouvelle position de la souris (pour DessinerElastique)
            mScreen1 = RecalculermScreen(pSouris)
            mScreen(mScreen.Length - 2) = mScreen1
          Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire
            'M�moriser la nouvelle position de la souris (pour DessinerElastique)
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
        'Rechercher si un objet est s�lectionn�
        Dim unObjetSurvol�, objS�lectEncours As Graphique
        unObjetSurvol� = colObjetsGraphiques.RechercherObject(pSouris, PointCliqu�)
        If IsNothing(unObjetSurvol�) Then
          TraiterMessageGlisser()
          ReDim mPoint(-1)
        Else
          objS�lectEncours = objS�lect
          objS�lect = unObjetSurvol�
          If Not InitialiserCommande(pSouris) Then ReDim mPoint(-1)
          objS�lect = objS�lectEncours
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
        Case CommandeGraphique.OrigineBranche, CommandeGraphique.AngleBranche, CommandeGraphique.PassagePi�ton, _
            CommandeGraphique.EtirerIlot, CommandeGraphique.D�placerIlot, CommandeGraphique.ElargirIlot, _
            CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage, CommandeGraphique.D�placerPassage, _
            CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.D�placerLigneFeu, _
            CommandeGraphique.AllongerFeu, CommandeGraphique.D�placerSignal, CommandeGraphique.D�placerCarrefour, _
              CommandeGraphique.EditerTrajectoire, CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire, _
              CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure, _
              CommandeGraphique.D�placerNord, CommandeGraphique.OrienterNord, CommandeGraphique.D�placerEchelle

          FinCommande = TerminerCommande(pEncours)
      End Select

      If FinCommande Then
        Select Case UneCommandeGraphique
          Case CommandeGraphique.LigneFeux, CommandeGraphique.Trajectoire, CommandeGraphique.PassagePi�ton
            D�marrerCommande(UneCommandeGraphique, Continuation:=True)
          Case Else
            D�marrerCommande(CommandeGraphique.AucuneCommande)
        End Select
      End If

    Else
      'MouseUp aussitot MouseDown pour une s�lection : Il faut d�sactiver D�marrerDrag
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.ZoomPAN, CommandeGraphique.Mesure
          If EnAttenteMouseUp Then  'Validation du 1er point
            EnAttenteMouseUp = False
            If InitialiserCommande(pEncours) Then
              mDragging = True
              DessinerElastique()
              If UneCommandeGraphique = CommandeGraphique.Trajectoire Then
                mScreen1 = mScreen2
              ElseIf UneCommandeGraphique = CommandeGraphique.PassagePi�tonRapide Then
                TerminerCommande(pEncours)
                'Pour la commande passage pi�ton rapide, on pourrait analyser s'il reste une branche sans passage
                D�marrerCommande(UneCommandeGraphique, Continuation:=True)
              End If
            End If
          End If

        Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPAN
          TerminerCommande(pEncours)
          D�marrerCommande(CommandeGraphique.AucuneCommande)

        Case Else
          'MouseUp aussitot MouseDown pour une s�lection : Il faut d�sactiver D�marrerDrag
          If Not IsNothing(objS�lect) Then
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
      D�marrerCommande(CommandeGraphique.AucuneCommande)
    End If
    objS�lect = Nothing
  End Sub

  '******************************************************************************
  ' MouseDown ou MouseMove sur le graphique
  '******************************************************************************
  Private Sub R�activerS�lect()
    If Not IsNothing(savObjS�lect) Then
      objS�lect = savObjS�lect
      savObjS�lect = Nothing
      S�lD�s�lectionner()   ' Montre ou cache les poign�es de s�lection
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

    ' Les �v�nements d�clench�s sont : MouseDown, Click, MouseUp,DoubleClick et enfin MouseUp  � nouveau
    If TypeOf objS�lect Is PolyArc Then
      ' Commande d'�dition d'objet
      Dim unPolyArc As PolyArc = objS�lect
      Dim objetM�tier As M�tier = objS�lect.ObjetM�tier

      If TypeOf objetM�tier Is TrajectoireV�hicules Then
        EnAttenteMouseUp = True
        btnTrajProp.PerformClick()
      ElseIf TypeOf objetM�tier Is Travers�ePi�tonne Then
        Me.btnTravProp.PerformClick()
      End If

    ElseIf TypeOf objS�lect Is Cercle Then
      Dim unCercle As Cercle = objS�lect
      Dim objetm�tier As M�tier = objS�lect.ObjetM�tier

    End If
  End Sub

#End Region

#Region "RecalculsMscreen"
  '**************************************************************************************
  ' Translation de l'ensemble de l'ilot
  '**************************************************************************************
  Private Sub RecalculerMscreenIlot(ByVal pBase As Point)
    'pBase correspond � P5, sommet de l'arc
    Dim p1 As Point = Translation(pBase, DecalV(0))
    Dim p3 As Point = Translation(pBase, DecalV(1))
    Dim p4 As Point = Translation(pBase, DecalV(2))
    If BrancheLi�e.PtInt�rieur(p3) Then
      mScreen(0) = PtClipp�(p1, p3)
      mScreen(1) = PtClipp�(p3, pBase)
      mScreen(2) = PtClipp�(p4, pBase)
      mScreen(3) = PtClipp�(p1, p4)
    End If

  End Sub

  '**************************************************************************************
  ' Translation de l'ensemble du passage
  '**************************************************************************************
  Private Sub TranslaterMscreen(ByVal pBase As Point)
    'pBase correspond �
    ' Branche : point cliqu� au d�part du glissement
    ' Passage pi�ton (Carrefour) :  point int�rieur du passage(du carrefour) qui commande l'ensemble du d�placement (point cliqu� initial en train de glisser)
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
      mScreen(i) = PtClipp�(p(i), p(IIf((i Mod 2) = 0, i + 1, i - 1)))
    Next

    If UneCommandeGraphique = CommandeGraphique.D�placerSignal Then mScreen1 = picDessin.PointToScreen(pBase)
  End Sub

  Private Sub RecalculerMscreenLigneFeux(ByVal pBase As Point)
    mScreen(1) = Translat�Clipp�(pBase, DecalV(0), pBase)
  End Sub

  '**************************************************************************************
  ' Recalculer mScreenx avec le d�calage ad�quat par rapport au point cliqu� :Translation
  '**************************************************************************************
  Private Function Translat�Clipp�(ByVal pBase As Point, ByVal unVecteur As Vecteur, ByVal pBase2 As Point) As Point
    Dim p As Point = Translation(pBase, unVecteur)

    Return PtClipp�(p, pBase2)

  End Function

  '**************************************************************************************
  ' Recalculer mScreenx avec le d�calage ad�quat par rapport au point cliqu� :Rotation
  '**************************************************************************************
  Private Function RecalculermScreen(ByVal pCentre As Point, ByVal pBase As Point, ByVal Dist As Single) As Point
    Dim p As Point = PointPosition(pCentre, Dist, CType(AngleForm�(pCentre, pBase), Single))

    Return PtClipp�(p, pCentre)

  End Function

  '**************************************************************************************
  ' Recalculer mScreenx avec un calcul sp�cifique si le point en cours est le dernier
  '**************************************************************************************
  Private Function RecalculermScreen(ByVal pSouris As Point) As Point
    Dim Continuer As Boolean
    Dim p As Point
    Dim pScreen As Point = Control.MousePosition

    Select Case UneCommandeGraphique
      Case CommandeGraphique.D�placerCarrefour, CommandeGraphique.ZoomPAN
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

      Case CommandeGraphique.PassagePi�ton
        p = PtPassage(pSouris)
        If p.IsEmpty Then
          'Conserver l'ancien point car le trac� part en sens oppos�(ignorer le point)
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
          'Conserver l'ancien point car le trac� part en sens oppos�(ignorer le point)
          pScreen = mScreen2
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

      Case CommandeGraphique.D�placerLigneFeu, CommandeGraphique.AllongerFeu
        p = PtLigneFeuD�plac�(pSouris)
        If p.IsEmpty Then
          pScreen = mScreen(1)
        Else
          pScreen = picDessin.PointToScreen(p)
          If UneCommandeGraphique = CommandeGraphique.D�placerLigneFeu Then
            mScreen(0) = Translat�Clipp�(p, DecalV(0), p)
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

      Case CommandeGraphique.D�placerPassage
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
          'Conserver l'ancien point car l'ilot d�passe la limite (ignorer le point)
          pScreen = mScreen(0)
        Else
          pScreen = picDessin.PointToScreen(p)
        End If

    End Select

    G�rerCurseur(Not p.IsEmpty)

    Return pScreen

  End Function

  '**************************************************************************************
  ' Nouveau point sur la trajectoire
  ' ou Modification du point d'acc�s au carrefour de la trajectoire
  '**************************************************************************************
  Private Function PtEditTrajectoire(ByVal pSouris As Point) As Point

    Select Case UneCommandeGraphique
      Case CommandeGraphique.EditerTrajectoire, CommandeGraphique.EditerPointTrajectoire
        'Edition de la trajectoire point par point
        If mesBranches.EnveloppeCarrefour.Int�rieur(pSouris) Then
          Return pSouris
        End If

      Case Else
        'Modification de l'acc�s
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
    Dim numPoign�e As Short
    ' Traiter d'abord les cas o� la commande a �t� d�finie par le programme (Cr�ation d'objets - ZoomPAN - Edition manuelle de trajectoire)

    Try

      Select Case UneCommandeGraphique
        ' Traiter d'abord les cas o� la commande a �t� d�finie par le programme (Cr�ation d'objets - ZoomPAN )
      Case CommandeGraphique.ZoomPAN
          D�finirPointsCarrefour(pEnCours)
          InitialiserCommande = True

        Case CommandeGraphique.Mesure
          InitialiserCommande = True
          ReDim mPoint(0)
          ReDim mScreen(1)
          mPoint(0) = pEnCours
          mScreen(0) = picDessin.PointToScreen(mPoint(0))
          mScreen(1) = mScreen(0)

        Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          'Cr�ation d'objets
          Dim pInt�ressant As Point = pEnCours
          BrancheLi�e = BrancheProche(pInt�ressant)
          If UneCommandeGraphique = CommandeGraphique.PassagePi�ton Or UneCommandeGraphique = CommandeGraphique.PassagePi�tonRapide Then
            If IsNothing(BrancheLi�e) Then
              AfficherMessageErreur(Me, "Cliquer un point sur un bord de chauss�e")

            ElseIf BrancheLi�e.mPassages.Count = 2 Then
              AfficherMessageErreur(Me, "Cette branche comporte d�j� 2 passages pi�tons")

            ElseIf BrancheLi�e.mPassages.Count = 1 Then
              If UneCommandeGraphique = CommandeGraphique.PassagePi�tonRapide Then
                AfficherMessageErreur(Me, "Cette branche comporte d�j� 1 passage pi�ton")
              ElseIf (BrancheLi�e.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Or BrancheLi�e.SensUnique(Voie.TypeVoieEnum.VoieSortante)) Then
                'Si ce controle devait disparaitre, il faudrait revoir la fonction SignalFeu.PtR�f�rence
                AfficherMessageErreur(Me, "Cette branche � sens unique comporte d�j� 1 passage pi�ton")
              Else
                InitialiserCommande = True
              End If

            Else
              InitialiserCommande = True
            End If

            If InitialiserCommande Then
              ReDim mPoint(0)
              ReDim mScreen(0)
              mPoint(0) = pInt�ressant
            End If

          Else    ' Trajectoire v�hicules ou ligne de feux v�hicules
            If IsNothing(VoieTraj) Then
              AfficherMessageErreur(Me, "Cliquer entre les 2 bords d'une voie")
            ElseIf Not VoieTraj.Entrante Then
              AfficherMessageErreur(Me, "D�signer une voie entrante")
            Else
              InitialiserCommande = True
              ReDim mPoint(1)
              ReDim mScreen(1)
              If UneCommandeGraphique = CommandeGraphique.Trajectoire Then
                mPoint(0) = VoieTraj.MilieuExtr�mit�(Voie.Extr�mit�Enum.Ext�rieur)
                mPoint(1) = VoieTraj.MilieuExtr�mit�(Voie.Extr�mit�Enum.Int�rieur)
                mPoint(0) = PtClipp�(mPoint(0), mPoint(1), Coordonn�esEcran:=False)
              Else
                Dim p1, p2 As Point
                p1 = Projection(pEnCours, VoieTraj.Bordure(Branche.Lat�ralit�.Droite))
                p2 = Projection(pEnCours, VoieTraj.Bordure(Branche.Lat�ralit�.Gauche))
                If Distance(pEnCours, p1) < Distance(pEnCours, p2) Then
                  mPoint(0) = p1
                Else
                  mPoint(0) = p2
                End If
                mPoint(1) = mPoint(0)
                AngleProjection = BrancheLi�e.AngleEnRadians + PI / 2
                ContourPermis = BrancheLi�e.EnveloppeVoiesEntrantes
              End If
            End If
          End If

          If InitialiserCommande Then
            mScreen1 = picDessin.PointToScreen(mPoint(0))
            mScreen(0) = mScreen1
            If UneCommandeGraphique = CommandeGraphique.PassagePi�ton Then
              mScreen2 = mScreen1
            ElseIf UneCommandeGraphique = CommandeGraphique.PassagePi�tonRapide Then
              ReDim mPoint(3)

            Else
              mScreen2 = picDessin.PointToScreen(mPoint(1))
              mScreen(1) = mScreen2
            End If
          End If

        Case Else

          If TypeOf objS�lect Is PolyArc AndAlso CType(objS�lect, PolyArc).Editable Then
            ' Commande d'�dition d'objet
            Dim unPolyArc As PolyArc = objS�lect
            Dim objetM�tier As M�tier = objS�lect.ObjetM�tier

            If TypeOf objetM�tier Is Variante Then
              UneCommandeGraphique = CommandeGraphique.D�placerCarrefour
              D�finirPointsCarrefour(pEnCours)
              InitialiserCommande = True

            ElseIf TypeOf objetM�tier Is Nord Then
              InitialiserCommande = D�finirPointsNord(objetM�tier, pEnCours)

            ElseIf TypeOf objetM�tier Is SymEchelle Then
              InitialiserCommande = D�finirPointsEchelle(objetM�tier, pEnCours)

            ElseIf TypeOf objetM�tier Is Branche Then
              Dim uneBranche As Branche = objetM�tier
              UneCommandeGraphique = uneBranche.MouvementPossible(pEnCours)
              If UneCommandeGraphique <> CommandeGraphique.AucuneCommande Then
                D�finirPointsBranche(uneBranche)
                InitialiserCommande = True
              End If

            ElseIf TypeOf objetM�tier Is Ilot Then
              Dim unIlot As Ilot = objetM�tier
              unPolyArc = objS�lect
              For numPoign�e = 0 To unPolyArc.NbPoign�es - 1
                If Distance(PointCliqu�, unPolyArc.Poign�e(numPoign�e)) < RayS�lect Then
                  D�finirPointsIlots(unPolyArc, numPoign�e, Nothing)
                  InitialiserCommande = True
                  Exit For
                End If
              Next
              If Not InitialiserCommande Then
                Dim ContourUtile As PolyArc = CType(unPolyArc(4), PolyArc)
                If ContourUtile.Int�rieur(PointCliqu�) Then
                  D�finirPointsIlots(unPolyArc, numPoign�e:=3, pRef:=PointCliqu�)
                  UneCommandeGraphique = CommandeGraphique.D�placerIlot
                  InitialiserCommande = True
                End If
              End If

            ElseIf TypeOf objetM�tier Is PassagePi�ton Then
              unPolyArc = objS�lect
              Dim unPassage As PassagePi�ton = CType(objetM�tier, PassagePi�ton)

              UneCommandeGraphique = unPassage.MouvementPossible(PointCliqu�, numPoign�e)
              If PasPassage = 2 AndAlso numPoign�e Mod 2 = 1 Then
                'Clique sur une poign�e au milieu d'un segment
                Poign�eCliqu�e = ((numPoign�e + 1) / PasPassage) Mod 4
              Else
                'Clique sur un point proche d'un coin du passage
                Poign�eCliqu�e = numPoign�e / PasPassage
              End If

              Select Case UneCommandeGraphique
                Case CommandeGraphique.EditPointPassage
                  D�finirPointsEditPassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.EditAnglePassage
                  D�finirPointsEditAnglePassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.D�placerPassage
                  D�finirPointsPassage(unPassage, PointCliqu�)
                  InitialiserCommande = True
                Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage
                  D�finirPointsEditDimensionPassage(unPassage)
                  InitialiserCommande = True
                Case CommandeGraphique.EditLongueurPassage
                  InitialiserCommande = True
              End Select

              ''If Not InitialiserCommande Then
              ''  'Faire une recherche sur les cot�s parall�les du passage (les 'grands' cot�s)
              ''  InitialiserCommande = D�finirPointsPassageParall�le(unPassage, PointCliqu�)
              ''End If

            ElseIf TypeOf objetM�tier Is LigneFeuV�hicules Then
              Dim uneLigneFeux As LigneFeuV�hicules = objetM�tier
              Dim uneLigne As Ligne = uneLigneFeux.Dessin

              If Distance(PointCliqu�, uneLigne.pA) < Distance(PointCliqu�, uneLigne.pB) Then
                PointProche = uneLigne.pA
                UneCommandeGraphique = CommandeGraphique.D�placerLigneFeu
              Else
                PointProche = uneLigne.pB
                UneCommandeGraphique = CommandeGraphique.AllongerFeu
              End If

              If Distance(PointProche, PointCliqu�) < RayS�lect Then
                D�finirPointsLigneFeux(uneLigneFeux)
                InitialiserCommande = True
              Else
                UneCommandeGraphique = CommandeGraphique.AucuneCommande
              End If

            ElseIf TypeOf objetM�tier Is SignalFeu Then
              Dim unSignalFeu As SignalFeu = objetM�tier
              D�finirPointsSignalFeux(unSignalFeu, pEnCours)
              InitialiserCommande = True
              UneCommandeGraphique = CommandeGraphique.D�placerSignal

            ElseIf TypeOf objetM�tier Is TrajectoireV�hicules Then
              Dim uneTrajectoire As TrajectoireV�hicules = objetM�tier
              UneCommandeGraphique = uneTrajectoire.MouvementPossible(pEnCours, Poign�eCliqu�e)
              Select Case UneCommandeGraphique
                Case CommandeGraphique.EditerPointTrajectoire
                  D�finirPointInterm�diaire(uneTrajectoire)
                  InitialiserCommande = True
                Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire
                  D�finirPointsAcc�s(uneTrajectoire)
                  InitialiserCommande = True
              End Select
            End If

          End If

      End Select

      TraiterMessageGlisser()

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      D�marrerCommande(CommandeGraphique.AucuneCommande)
      InitialiserCommande = False
    End Try


  End Function
#Region "D�finirPoints"
  Private Sub D�finirPointsAcc�s(ByVal uneTrajectoire As TrajectoireV�hicules)
    ReDim mPoint(0)
    ReDim mScreen(1)

    With uneTrajectoire
      Select Case UneCommandeGraphique
        Case CommandeGraphique.EditerOrigineTrajectoire
          Segment1 = .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Origine)
        Case CommandeGraphique.EditerDestinationTrajectoire
          Segment1 = .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Destination)
      End Select
    End With

    mPoint(0) = Segment1.pA
    mScreen(0) = picDessin.PointToScreen(mPoint(0))
    'mScreen1 : point mobile
    mScreen1 = mScreen(0)
    'AU d�part, les 2 points de l'�lastique sont confondus - mScreen(1) restera fixe
    mScreen(1) = mScreen(0)

  End Sub

  Private Sub D�finirPointInterm�diaire(ByVal uneTrajectoire As TrajectoireV�hicules)
    ReDim mPoint(0)
    ReDim mScreen(2)

    With uneTrajectoire.PolyManuel
      mPoint(0) = CvPoint(.Points(Poign�eCliqu�e - 1))
      mScreen(0) = picDessin.PointToScreen(mPoint(0))
      mScreen(1) = picDessin.PointToScreen(CvPoint(.Points(Poign�eCliqu�e)))
      mScreen(2) = picDessin.PointToScreen(CvPoint(.Points(Poign�eCliqu�e + 1)))
      'mScreen1 : point mobile
      mScreen1 = mScreen(1)
    End With

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la gestion d'une commande graphique relative aux lignes de feux
  '**************************************************************************************
  Private Sub D�finirPointsSignalFeux(ByVal unSignalFeu As SignalFeu, ByVal pEnCours As Point)
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

    Dim pR�f�rence As Point
    If unSignalFeu.mLigneFeux.EstV�hicule Then
      pR�f�rence = CType(unSignalFeu.mLigneFeux.mGraphique(0), Ligne).Milieu
    Else
      pR�f�rence = unSignalFeu.PtR�f�rence
    End If

    mScreen1 = picDessin.PointToScreen(pEnCours)
    mScreen2 = picDessin.PointToScreen(pR�f�rence)

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la gestion d'une commande graphique relative aux lignes de feux
  '**************************************************************************************
  Private Sub D�finirPointsLigneFeux(ByVal uneLigneFeux As LigneFeuV�hicules)
    ReDim mPoint(1)
    ReDim mScreen(1)
    ReDim DecalV(0)
    Dim i As Short

    LigneFeuEnCours = uneLigneFeux
    Dim uneLigne As Ligne = LigneFeuEnCours.Dessin

    BrancheLi�e = LigneFeuEnCours.mBranche
    mPoint(0) = uneLigne.pA
    mPoint(1) = uneLigne.pB
    For i = 0 To 1
      mScreen(i) = picDessin.PointToScreen(mPoint(i))
    Next

    If UneCommandeGraphique = CommandeGraphique.D�placerLigneFeu Then
      'Bord de la voie la plus � gauche
      Segment1 = LigneFeuEnCours.BordVoie(1)
      DecalV(0) = New Vecteur(uneLigne)
      AngleProjection = BrancheLi�e.AngleEnRadians
    Else
      mScreen(0) = picDessin.PointToScreen(uneLigne.pA)
      mScreen(1) = picDessin.PointToScreen(uneLigne.pB)
      Dim p(1) As Point
      With BrancheLi�e
        p(0) = Projection(mPoint(0), .BordVoiesEntrantes(Branche.Lat�ralit�.Droite))
        p(1) = Projection(mPoint(1), .BordVoiesEntrantes(Branche.Lat�ralit�.Gauche))
      End With
      Segment1 = New Ligne(p(0), p(1))
      AngleProjection = BrancheLi�e.AngleEnRadians + PI / 2
      If AngleProjection > PI Then AngleProjection -= 2 * PI
    End If

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la gestion d'une commande graphique relative aux branches
  '**************************************************************************************
  Private Function D�finirPointsBranche(ByVal uneBranche As Branche) As Boolean
    ReDim mPoint(1)
    ReDim mScreen(1)
    ReDim DecalV(1)

    Dim BranchePr�c�dente As Branche = mesBranches.Pr�c�dente(uneBranche)
    Dim BrancheSuivante As Branche = mesBranches.Suivante(uneBranche)
    Dim p(3) As Point

    Dim unePlumeRouge As Pen = New Pen(Color.Red, 2)
    Dim unePlumeVerte As Pen = New Pen(Color.Green, 2)
    Dim unePlumeBleue As Pen = New Pen(Color.Blue, 2)

    Dim l1 As Ligne
    Dim l2 As Ligne

    With uneBranche.LigneDeSym�trie
      mPoint(0) = .pA
      mPoint(1) = .pB
      If UneCommandeGraphique = CommandeGraphique.OrigineBranche Then
        DecalV(0) = New Vecteur(0, 0)
        DecalV(1) = New Vecteur(uneBranche.LigneDeSym�trie)

        Segment1 = BranchePr�c�dente.BordChauss�e(Branche.Lat�ralit�.Gauche)
        Segment2 = BrancheSuivante.BordChauss�e(Branche.Lat�ralit�.Droite)

        'Lignes suivantes ajout�es suite aux raccords de branche
        Segment1.pA = BranchePr�c�dente.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche)
        Segment2.pA = BrancheSuivante.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite)

        'SegmentLimite = New Ligne(Segment1.pA, Segment2.pA)
        'AngleBranche = eqvRadian(uneBranche.Angle)
        'LargeurBranche = uneBranche.Largeur * Echelle
        '======================================================================================================
        p(0) = PointPosition(Segment2.pA, uneBranche.Largeur / 2 * Echelle, uneBranche.AngleEnRadians + sngPI / 2)
        'DecalV2 = New Vecteur(Segment2.pA, p(0))
        p(1) = PointPosition(Segment1.pA, uneBranche.Largeur / 2 * Echelle, uneBranche.AngleEnRadians - sngPI / 2)
        'DecalV3 = New Vecteur(Segment1.pA, p(1))

        ' Tol�rance de PI/12 par rapport aux r�gles +strictes
        l1 = New Ligne(p(1), PtClipp�(PointPosition(p(1), uneBranche.AngleEnRadians + sngPI / 12), p(1), Coordonn�esEcran:=False), unePlumeBleue)
        l2 = New Ligne(p(0), PtClipp�(PointPosition(p(0), uneBranche.AngleEnRadians - sngPI / 12), p(0), Coordonn�esEcran:=False), unePlumeVerte)
        'DessinerObjet(l1)
        'DessinerObjet(l2)

        ' Construire l'enveloppe autoris�e en s'appuyant sur l1,l2 et les bords du controle picDessin

        'D�finition des bords
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

        'Si l2 intersecte le m�me bord : ajouter ce point � l'enveloppe et terminer
        p(3) = Point.Ceiling(intersect(l2, uneLigneBord))
        If p(3).IsEmpty Then
          ' Ajouter le coin du controle � l'enveloppe
          p(3) = uneLigneBord.pB
          'Parcourir les bords suivants jusqu'� ce que l2 en intersecte un
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
        Dim pDonn�, pOrigine, pCherch� As Point
        CentreRotation = .pA
        LongueurSegment = .Longueur
        AngleProjection = EqvRadian(uneBranche.Angle) + Math.PI / 2
        If AngleProjection > PI Then AngleProjection -= 2 * PI
        mScreen(0) = picDessin.PointToScreen(CentreRotation)
        mScreen(1) = picDessin.PointToScreen(.pB)

        Dim Longueur As Single = uneBranche.Longueur * Echelle
        Dim DemiLargeur As Single = uneBranche.Largeur / 2 * Echelle
        ' bord de chauss�e droit
        'pDonn� = uneBranche.BordChauss�e(Branche.Lat�ralit�.Droite).pA
        pDonn� = uneBranche.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite)

        ' bord de chauss�e gauche de la branche pr�c�dente
        'pOrigine = BranchePr�c�dente.BordChauss�e(Branche.Lat�ralit�.Gauche).pA
        pOrigine = BranchePr�c�dente.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche)

        pCherch� = PointSurDroiteADistancePointDonn�(pDonn�, Longueur, pOrigine, EqvRadian(BranchePr�c�dente.Angle))
        If pCherch�.IsEmpty Then
          pCherch� = pOrigine
        ElseIf AngleForm�(pOrigine, pCherch�, pDonn�) < 0 Then
          pCherch� = pOrigine
        End If
        pMini = PointPosition(pCherch�, DemiLargeur, CType(AngleForm�(pDonn�, pCherch�) - PI / 2, Single))

        ' bord de chauss�e gauche
        'pDonn� = uneBranche.BordChauss�e(Branche.Lat�ralit�.Gauche).pA
        pDonn� = uneBranche.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche)

        ' bord de chauss�e droite de la branche suivante
        'pOrigine = BrancheSuivante.BordChauss�e(Branche.Lat�ralit�.Droite).pA
        pOrigine = BrancheSuivante.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite)

        pCherch� = PointSurDroiteADistancePointDonn�(pDonn�, Longueur, pOrigine, EqvRadian(BrancheSuivante.Angle))
        If pCherch�.IsEmpty Then
          pCherch� = pOrigine
        ElseIf AngleForm�(pOrigine, pCherch�, pDonn�) > 0 Then
          pCherch� = pOrigine
        End If
        pMaxi = PointPosition(pCherch�, DemiLargeur, CType(AngleForm�(pDonn�, pCherch�) + PI / 2, Single))

        AngleMini = CvAngleDegr�s(AngleForm�(mPoint(0), pMini), SurDeuxPi:=False)
        Dim AngleMaxi As Single = CvAngleDegr�s(AngleForm�(mPoint(0), pMaxi), SurDeuxPi:=False)
        If AngleMaxi < AngleMini Then AngleMaxi += 360
        BalayageMaxi = AngleMaxi - AngleMini
      End If
    End With

  End Function

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsPassage(ByVal unPassage As PassagePi�ton, ByVal pSouris As Point)
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
          'D�calage entre la position de la souris et le point du contour
          DecalV(Index) = New Vecteur(pSouris, p)
        Next

      End With

      ' Segment infini passant par le pointeur de souris et parall�le � l'axe de la branche
      ' on projettera le pointeur de souris sur ce segment pour d�terminer la nouvelle position du passage
      Segment1 = New Ligne(pSouris, PointPosition(pSouris, 100, .mBranche.AngleEnRadians))

    End With

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsEditPassage(ByVal unPassage As PassagePi�ton)
    Dim unContour As PolyArc = unPassage.Contour
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    BrancheLi�e = unPassage.mBranche
    Dim Ligne1 As Ligne = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))  ' Cot� proche et parall�le au bord de chauss�e
    Dim Ligne2 As Ligne = New Ligne(ptsContour(Index(2)), ptsContour(Index(3))) ' Cot� oppos�

    ReDim mScreen(3)
    ReDim mPoint(0)

    'UneCommandeGraphique = CommandeGraphique.EditPointPassage

    'M�morisation du point restant fixe dans mPoint(0)
    Select Case Poign�eCliqu�e
      Case 0
        Segment1 = Ligne1
        Segment2 = Ligne2
      Case 1
        Segment1 = Ligne1.Invers�e
        Segment2 = Ligne2.Invers�e
      Case 2
        Segment1 = Ligne2
        Segment2 = Ligne1
      Case 3
        Segment1 = Ligne2.Invers�e
        Segment2 = Ligne1.Invers�e
    End Select

    SegmentLimite = New Ligne(Segment1.pB, Segment2.pA)
    mPoint(0) = SegmentLimite.pA    ' Point invariant du cot� en cours de modif
    mScreen(0) = picDessin.PointToScreen(Segment1.pA)  'Point en cours de modif
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le cot� oppos�
    mScreen(1) = picDessin.PointToScreen(SegmentLimite.pA) 'Point invariant du petit cot� en cours de modif
    mScreen(3) = picDessin.PointToScreen(SegmentLimite.pB) 'Point invariant du cot� oppos� au cot� en cours de modif

    SigneConserv� = Math.Sign(AngleForm�(Segment1.pA, SegmentLimite.pA, SegmentLimite.pB))
    'Angle des 2 grands cot�s parall�les
    AngleParall�le = AngleForm�(Ligne1.pA, Ligne2.pB)

    'Bissectrice d l'angle form� par les 2 segments qui se croisent au point � modifer
    AngleProjection = (AngleForm�(Segment1) + AngleForm�(SegmentLimite)) / 2

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsEditAnglePassage(ByVal unPassage As PassagePi�ton)
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next

    BrancheLi�e = unPassage.mBranche

    ReDim mScreen(3)
    ReDim mPoint(0)

    Select Case Poign�eCliqu�e
      'Segment1 : Segment invariant
      'Segment2 :pA repr�sente le point en cours de modif, et pB son sym�trique qui sera recalcul� en fonction du parcours de pA)
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
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le cot� en cours de modif
    mScreen(3) = picDessin.PointToScreen(Segment1.pB) 'Point invariant du petit cot� oppos�
    mScreen(1) = picDessin.PointToScreen(Segment1.pA) 'Point invariant du petit cot� oppos�

    'M�morisation du point de r�f�rence dans mPoint(0) :point invariant sur le m�me grand cot� que le point en cours de modif
    mPoint(0) = Segment1.pA

    'Angle des 2 cot�s parall�les du trap�ze
    AngleParall�le = AngleForm�(ptsContour(Index(1)), ptsContour(Index(2)))
    'Angle � conserver : angle entre le point en cours de modif et le segment invariant(Segment1)
    SigneConserv� = Math.Sign(AngleForm�(Segment2.pA, Segment1.pA, Segment1.pB))

  End Sub

  ''**************************************************************************************
  '' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsEditDimensionPassage(ByVal unPassage As PassagePi�ton)
    Dim ptsContour() As PointF = unPassage.Contour.Points
    Dim i, Index(3) As Short
    For i = 0 To 3
      Index(i) = i * PasPassage
    Next
    BrancheLi�e = unPassage.mBranche
    Dim Ligne1 As Ligne = New Ligne(ptsContour(Index(0)), ptsContour(Index(1)))  ' Cot� proche et parall�le au bord de chauss�e
    Dim Ligne2 As Ligne = New Ligne(ptsContour(Index(2)), ptsContour(Index(3)))  ' Cot� oppos�

    ReDim mScreen(3)
    ReDim mPoint(0)

    ' Modification d'un petit cot� en restant parall�le au bord de chauss�e

    'M�morisation du point restant fixe dans mPoint(0)
    If UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Then
      'Angle des 2 grands cot�s parall�les
      AngleParall�le = AngleForm�(Ligne1.pA, Ligne2.pB)
      If Poign�eCliqu�e = 0 Then
        Segment1 = Ligne1
        Segment2 = Ligne2
      Else  ' Poign�eCliqu�e = 2
        Segment1 = Ligne2
        Segment2 = Ligne1
      End If

    Else  'UneCommandeGraphique = CommandeGraphique.EditLongueurPassage
      If Poign�eCliqu�e = 1 Then
        Segment1 = New Ligne(Ligne1.pB, Ligne2.pA)
        Segment2 = New Ligne(Ligne2.pB, Ligne1.pA)
        'Angle du cot� proche du bord de chauss�e
        AngleParall�le = AngleForm�(Ligne1)
      Else  ' Poign�eCliqu�e = 3
        Segment1 = New Ligne(Ligne2.pB, Ligne1.pA)
        Segment2 = New Ligne(Ligne1.pB, Ligne2.pA)
        'Angle du cot� proche du bord de chauss�e oppos� (ou cot� ilot)
        AngleParall�le = AngleForm�(Ligne2)
      End If
    End If

    SegmentLimite = New Ligne(Segment1.pB, Segment2.pA)
    mPoint(0) = SegmentLimite.Milieu     ' Point invariant du cot� en cours de modif
    mScreen(0) = picDessin.PointToScreen(Segment1.pA)  'Point en cours de modif
    mScreen(2) = picDessin.PointToScreen(Segment2.pB) 'Point variant sur le cot� oppos�
    mScreen(1) = picDessin.PointToScreen(SegmentLimite.pA) 'Point invariant du petit cot� en cours de modif
    mScreen(3) = picDessin.PointToScreen(SegmentLimite.pB) 'Point invariant du cot� oppos� au cot� en cours de modif

    SigneConserv� = Math.Sign(AngleForm�(Segment1.pA, SegmentLimite.pA, SegmentLimite.pB))
    AngleProjection = AngleParall�le + Math.PI / 2
  End Sub

  ''**************************************************************************************
  '' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  ''**************************************************************************************
  'Private Sub D�finirPointsPassage(ByVal unPassage As PassagePi�ton, ByVal numPoign�e As Short)
  '  Dim unContour As PolyArc = unPassage.Contour
  '  BrancheLi�e = unPassage.mBranche
  '  Dim Ligne1 As Ligne = New Ligne(unContour.Points(0), unContour.Points(1))  ' Cot� proche et parall�le au bord de chauss�e
  '  Dim Ligne2 As Ligne = New Ligne(unContour.Points(2), unContour.Points(3))  ' Cot� oppos�
  '  Dim Parall�lismeComplet As Boolean = Abs(AngleForm�(Ligne2) - BrancheLi�e.AngleEnRadians) < 0.1
  '  Dim Parall�lismeEnvisageable As Boolean = numPoign�e = 0 Or numPoign�e = 1 Or Parall�lismeComplet

  '  ReDim mScreen(3)
  '  ReDim mPoint(0)

  '  Poign�eCliqu�e = numPoign�e
  '  Dim pSouris As Point = picDessin.PointToClient(picDessin.MousePosition)
  '  If BrancheLi�e.BordChauss�eProche(pSouris) <> Branche.Lat�ralit�.Aucune And Parall�lismeEnvisageable Then
  '    ' Modification d'un petit cot� en restant parall�le au bord de chauss�e
  '    UneCommandeGraphique = CommandeGraphique.EditLargeurPassage

  '    'M�morisation du point restant fixe dans mPoint(0)
  '    Select Case Poign�eCliqu�e
  '      Case 0
  '        Segment1 = Ligne1
  '        Segment2 = Ligne2
  '      Case 1
  '        Segment1 = Ligne1.Invers�e
  '        Segment2 = Ligne2.Invers�e
  '      Case 2
  '        Segment1 = Ligne2
  '        Segment2 = Ligne1
  '      Case 3
  '        Segment1 = Ligne2.Invers�e
  '        Segment2 = Ligne1.Invers�e
  '    End Select

  '    'Angle de la branche
  '    AngleProjection = AngleForm�(Segment1)
  '    'Angle des 2 grands cot�s parall�les
  '    AngleParall�le = AngleForm�(Segment1.pA, Segment2.pB)
  '    mPoint(0) = Segment1.pB   ' Point invariant du cot� en cours de modif
  '    mScreen(1) = picDessin.PointToScreen(Segment1.pA) 'Point variant
  '    mScreen(2) = picDessin.PointToScreen(Segment2.pB)
  '    mScreen(3) = picDessin.PointToScreen(Segment2.pA) 'Point projet� du pr�c�dent sur l'autre cot�

  '  Else
  '    ' Modification d'un grand cot�
  '    ' On va soit �tirer le passage soit changer l'angle du passage (angle des 2 cot�s parall�les)

  '    'M�moriser le point cliqu� (et qui va ensuite se d�placer)
  '    If numPoign�e = 2 Then
  '      mPoint(0) = Ligne2.pA
  '    Else
  '      mPoint(0) = Ligne2.pB
  '    End If

  '    If Distance(pSouris, mPoint(0)) < 3 Then
  '      'Diff�rer la prise en compte du MouseMove jusqu'� ce que le Glisser soit significatif (pour �valuer l'angle)
  '      ReDim mPoint(-1)

  '    Else
  '      Dim uneLigne As Ligne = New Ligne(pSouris, mPoint(0))
  '      Dim uneLigne1 As Ligne = New Ligne(unContour.Points(1), unContour.Points(2)) ' Grand cot�
  '      Dim uneLigne2 As Ligne = New Ligne(unContour.Points(3), unContour.Points(0)) ' 2�me Grand cot�
  '      Dim AngleD�part As Single = Abs(AngleForm�(uneLigne, uneLigne1))
  '      If AngleD�part > PI / 2 Then AngleD�part = PI - AngleD�part
  '      If AngleD�part < PI / 6 Then
  '        'Etirer ou raccourcir le grand cot� cliqu�
  '        UneCommandeGraphique = CommandeGraphique.EditLongueurPassage
  '      Else
  '        'Rotation du passage
  '        UneCommandeGraphique = CommandeGraphique.EditAnglePassage
  '      End If

  '      If numPoign�e = 2 Then
  '        'Le point correspondant � Poign�e3 est invariant
  '        ' 1er grand cot�
  '        Segment1 = uneLigne1.Invers�e
  '        ' 2�me grand cot� 
  '        Segment2 = uneLigne2.Invers�e
  '      Else
  '        'Le point correspondant � Poign�e2 est invariant
  '        Segment1 = uneLigne2 ' 2�me grand cot� 
  '        Segment2 = uneLigne1 ' 1er grand cot� 
  '      End If

  '      'Angle du grand cot�
  '      AngleProjection = AngleForm�(Segment1)

  '      mScreen(1) = picDessin.PointToScreen(Segment1.pB)
  '      mScreen(2) = picDessin.PointToScreen(Segment2.pB)
  '      mScreen(3) = picDessin.PointToScreen(Segment2.pA)
  '    End If
  '  End If

  '  If mPoint.Length > 0 Then mScreen(0) = picDessin.PointToScreen(mPoint(0))

  'End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du passage pi�ton pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsCarrefour(ByVal pSouris As Point)
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
      p = uneBranche.BordChauss�e(Branche.Lat�ralit�.Gauche).pA
      mScreen(4 * numBranche) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche) = New Vecteur(pSouris, p)

      p = uneBranche.BordChauss�e(Branche.Lat�ralit�.Gauche).pB
      mScreen(4 * numBranche + 1) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 1) = New Vecteur(pSouris, p)

      uneBranche = mesBranches.Suivante(uneBranche)
      p = uneBranche.BordChauss�e(Branche.Lat�ralit�.Droite).pA
      mScreen(4 * numBranche + 2) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 2) = New Vecteur(pSouris, p)

      p = uneBranche.BordChauss�e(Branche.Lat�ralit�.Droite).pB
      mScreen(4 * numBranche + 3) = picDessin.PointToScreen(p)
      DecalV(4 * numBranche + 3) = New Vecteur(pSouris, p)

    Next

  End Sub

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du Nord pour MouseMove
  '**************************************************************************************
  Private Function D�finirPointsNord(ByVal objetM�tier As M�tier, ByVal pEnCours As Point) As Boolean
    Dim unNord As Nord = CType(objetM�tier, Nord)
    Dim uneLigne As Ligne = unNord.LigneR�f�rence

    If unNord.D�pla�able(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.D�placerNord
      D�finirPointsNord = True
    ElseIf unNord.Orientable(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.OrienterNord
      D�finirPointsNord = True
    End If

    If D�finirPointsNord Then
      ReDim mPoint(0)
      'Points utiles � l'�lastique : 1 segment pour repr�senter le Nord  (+ fl�che ?)
      ReDim mScreen(1)
      'Extr�mit� de la ligne la + loin de la fl�che
      mScreen(0) = picDessin.PointToScreen(uneLigne.pB)
      'Extr�mit� de la ligne la + proche de la fl�che
      mScreen(1) = picDessin.PointToScreen(uneLigne.pA)

      Select Case UneCommandeGraphique
        Case CommandeGraphique.D�placerNord

          ReDim DecalV(1)
          DecalV(0) = New Vecteur(uneLigne.pA.X - pEnCours.X, uneLigne.pA.Y - pEnCours.Y)
          DecalV(1) = New Vecteur(uneLigne.pB.X - pEnCours.X, uneLigne.pB.Y - pEnCours.Y)

        Case CommandeGraphique.OrienterNord
          'Extr�mit� de la ligne la + loin de la fl�che
          CentreRotation = uneLigne.pB
          LongueurSegment = uneLigne.Longueur
          'AngleProjection est l'angle du Nord + pi/2 pour un curseur perpendiculaire � la direction de la fl�che
          AngleProjection = unNord.Orientation + Math.PI / 2
      End Select
    End If

  End Function

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification du Nord pour MouseMove
  '**************************************************************************************
  Private Function D�finirPointsEchelle(ByVal uneEchelle As SymEchelle, ByVal pEnCours As Point) As Boolean
    Dim uneLigne As Ligne = uneEchelle.LigneR�f�rence

    If uneEchelle.D�pla�able(pEnCours) Then
      UneCommandeGraphique = CommandeGraphique.D�placerEchelle
      D�finirPointsEchelle = True

      ReDim mPoint(0)
      'Points utiles � l'�lastique : 1 segment pour repr�senter le rectangle
      ReDim mScreen(1)
      'Extr�mit� gauche 
      mScreen(0) = picDessin.PointToScreen(uneLigne.pB)
      'Extr�mit� droite
      mScreen(1) = picDessin.PointToScreen(uneLigne.pA)

      Select Case UneCommandeGraphique
        Case CommandeGraphique.D�placerEchelle

          ReDim DecalV(1)
          DecalV(0) = New Vecteur(uneLigne.pA.X - pEnCours.X, uneLigne.pA.Y - pEnCours.Y)
          DecalV(1) = New Vecteur(uneLigne.pB.X - pEnCours.X, uneLigne.pB.Y - pEnCours.Y)

      End Select
    End If

  End Function

  '**************************************************************************************
  ' D�finir les �l�ments utiles � la modification de l'ilot pour MouseMove
  '**************************************************************************************
  Private Sub D�finirPointsIlots(ByVal unPolyArc As PolyArc, ByVal numPoign�e As Short, ByVal pRef As Point)
    Dim uneLigne As Ligne
    Dim unArc As Arc
    Dim unIlot As Ilot = unPolyArc.ObjetM�tier
    BrancheLi�e = unIlot.mBranche

    ReDim mPoint(0)
    'Points utiles � l'�lastique : 3 segments pour repr�senter l'ilot  (triangle P1P3P4)
    ReDim mScreen(3)    ' le 4�me point est en principe �gal au 1er sauf clipping

    uneLigne = unPolyArc(0) ' 1er Grand cot� de l'ilot
    mScreen(0) = picDessin.PointToScreen(uneLigne.pA)   ' P1 : pointe de l'ilot
    mScreen(3) = mScreen(0)
    mScreen(1) = picDessin.PointToScreen(uneLigne.pB)   ' P3 : extr�mit� gauche de l'ilot

    uneLigne = unPolyArc(1) ' 2� Grand cot� de l'ilot
    mScreen(2) = picDessin.PointToScreen(uneLigne.pB)    ' P4 (sym�trique de P3)

    ' Points caract�ristiques de l'ilot
    uneLigne = unPolyArc(0)
    Dim P1 As Point = uneLigne.pA   ' P1 : pointe de l'ilot
    Dim P3 As Point = uneLigne.pB    ' P3 : extr�mit� gauche de l'ilot
    uneLigne = unPolyArc(1)
    Dim P4 As Point = uneLigne.pB    ' P4 (sym�trique de P3)
    Dim P2 As Point = Milieu(P3, P4)        ' P2 : P1P2 est l'axe de sym�trie longitudinal
    unArc = unPolyArc(2)

    Dim P5 As Point                   ' Milieu de l'arc (point le plus extr�me vers l'origine de la branche
    With unArc
      P5 = PointPosition(.pO, .Rayon, .AngleD�part + .AngleBalayage / 2, SensHoraire:=True)
    End With



    Select Case numPoign�e
      Case 0
        'Agrandissement ou r�tr�cissement de l'ilot - Affecte le rayon
        UneCommandeGraphique = CommandeGraphique.EtirerIlot

        mPoint(0) = P2       '  P1P2 est l'axe de sym�trie longitudinal
        AngleProjection = AngleForm�(P1, P2)
      Case 1, 2
        'Agrandissement ou r�tr�cissement de l'ilot - Affecte la largeur
        UneCommandeGraphique = CommandeGraphique.ElargirIlot
        mPoint(0) = P2    ' P3P4 est la corde de l'ilot et P2 est milieu de P3P4
        AngleProjection = AngleForm�(P3, P4)
        ReDim Preserve mScreen(4)  ' le 5�me point est juste pour m�moriser P2 (P3 et P4 sont sym�triques / P2)
        mScreen(4) = picDessin.PointToScreen(P2)

      Case 3
        'Translation de l'ilot : D�placement de P5 (Sommet de l'arc) ou d'un point � l'int�rieur de l'ilot - Affecte le retrait et le d�calage
        UneCommandeGraphique = CommandeGraphique.D�placerIlot
        If pRef.IsEmpty Then pRef = P5

        ' Point Origine du bord droit de la chauss�e (l'ilot ne doit pas s'approcher de ce bord � moins de 2 m)
        mPoint(0) = BrancheLi�e.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite)
        ' Projection du mileu d l'arc sur le bord de la chauss�e
        'AngleProjection est l'angle de direction mPoint(5)-mPoint(6)
        AngleProjection = EqvRadian(BrancheLi�e.Angle)

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
          D�marrerCommande(Me.CommandeGraphique.Zoom)
        Case MDIDiagfeux.BarreOutilsEnum.ZoomMoins
          D�marrerCommande(Me.CommandeGraphique.ZoomMoins)
        Case MDIDiagfeux.BarreOutilsEnum.PAN
          D�marrerCommande(Me.CommandeGraphique.ZoomPAN)
        Case MDIDiagfeux.BarreOutilsEnum.ZoomPr�c�dent
          D�marrerCommande(Me.CommandeGraphique.ZoomPr�c�dent)
        Case MDIDiagfeux.BarreOutilsEnum.Mesurer
          D�marrerCommande(Me.CommandeGraphique.Mesure)
        Case MDIDiagfeux.BarreOutilsEnum.Rafraichir
          Redessiner()
          D�marrerCommande(CommandeGraphique.AucuneCommande)
      End Select
    End If

  End Sub

  '**************************************************************************************
  ' D�marrer une nouvelle commande graphique
  '**************************************************************************************
  Private Sub D�marrerCommande(ByVal uneCommande As CommandeGraphique, Optional ByVal Continuation As Boolean = False)
    UneCommandeGraphique = uneCommande

    'Initialisations des drapeaux servant � g�rer le d�roulement de la commande
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
        Case CommandeGraphique.ZoomPr�c�dent
          TerminerCommande(picDessin.MousePosition)
          UneCommandeGraphique = CommandeGraphique.AucuneCommande
        Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu, _
         CommandeGraphique.PropTrajectoire, CommandeGraphique.PropTravers�e
          If IsNothing(objS�lect) Then
            TraiterMessageGlisser()
          Else
            TerminerCommande(picDessin.MousePosition)
            D�marrerCommande(CommandeGraphique.AucuneCommande)
          End If
        Case CommandeGraphique.AucuneCommande
          picDessin.Cursor = Cursors.Arrow
          TraiterMessageGlisser()
        Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux
          If Not Continuation Then
            D�s�lectionner(uneCommande)
          End If
          If UneCommandeGraphique = CommandeGraphique.LigneFeux Then
            Me.AC1GrilleFeux.Row = -1
          End If
          TraiterMessageGlisser(Continuation)
        Case CommandeGraphique.Antagonisme
          TraiterMessageGlisser()
      End Select

      If Not CommandeConservantS�lection() Then D�s�lectionner(uneCommande)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Public Sub InterrompreCommande()
    If UneCommandeGraphique = CommandeGraphique.Antagonisme Then
      If Not IsNothing(objS�lect) Then
        Try
          Dim unAntagonisme As Antagonisme = CType(objS�lect.ObjetM�tier, Antagonisme)

          If Not unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
            'La fen�tre des antagonismes ne concerne que les admissibles (pouvant basculer d'admis � non admis)

            ' La fen�tre des antagonismes ne visualise qu'un seul antagonisme pour tous ceux de m�me courant
            'Il faut donc Pointer sur l'antagonisme visible dans la fen�tre Antagonisme(antagonisme de m�me courant)
            unAntagonisme = unAntagonisme.M�mesCourants

            'Les valeurs globales de l'�chelle ont �t� modifi�es par l'activation de la feuille abaque
            'R�affecterEchelle()

            With Me.FenetreAntagonisme
              If .radOui.Checked Then
                If AntagonismeLi�Refus�(unAntagonisme, Admis:=True) Then
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
      ' L'instruction qui suit va �teindre les poign�es de l'objet s�lectionn�es : on le fait avant pour que celle-ci les rallument
      If Not IsNothing(objS�lect) Then S�lD�s�lectionner()
      D�marrerCommande(CommandeGraphique.AucuneCommande)
    End If
  End Sub

  Private Function CurseurCommande() As Cursor
    Select Case UneCommandeGraphique
      Case CommandeGraphique.OrigineBranche, CommandeGraphique.D�placerPassage, CommandeGraphique.D�placerSignal, CommandeGraphique.D�placerCarrefour, CommandeGraphique.D�placerNord, CommandeGraphique.D�placerEchelle
        Return Cursors.SizeAll
      Case CommandeGraphique.OrienterNord
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.AngleBranche
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EtirerIlot
        Return CurseurSelonAngle(AngleProjection)
        Return Cursors.SizeNS
      Case CommandeGraphique.D�placerIlot
        Return Cursors.SizeAll
      Case CommandeGraphique.ElargirIlot
        Return CurseurSelonAngle(AngleProjection)
        Return Cursors.SizeWE
      Case CommandeGraphique.D�placerLigneFeu, CommandeGraphique.AllongerFeu
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditPointPassage
        Return CurseurSelonAngle(AngleProjection)
      Case CommandeGraphique.EditAnglePassage
        Return CurseurSelonAngle(AngleParall�le + Math.PI / 2)
      Case CommandeGraphique.EditerTrajectoire
        Return Cursors.Cross
      Case CommandeGraphique.ZoomPAN
        Return Cursors.Hand
      Case CommandeGraphique.Mesure
        Return Cursors.Cross
      Case CommandeGraphique.LigneFeux, CommandeGraphique.PassagePi�ton
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
    ' NWSE et NESW sont invers�s en raison de l'inversion des Y dans le syst�me de coordonn�es Windows

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
  ' Masquer la s�lection de l'objet s�lectionn� (s'il y en a un)
  '****************************************************************************************
  Private Sub D�s�lectionner(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.AucuneCommande)
    If Not IsNothing(objS�lect) Then
      S�lD�s�lectionner(PourS�lection:=False)    ' Montre ou cache les poign�es de s�lection
      If Not uneCommande = CommandeGraphique.AucuneCommande Then
        'Traitement suppl�mentaire pour rallumer certains objets masqu�s pendant le temps de la commande
        D�S�lObjet()
        savObjS�lect = Nothing
      End If
    End If

  End Sub

  '****************************************************************************************
  ' Lors de la d�s�lection d'un objet : Redessiner la partie effac�e lors de la s�lection
  '****************************************************************************************
  Private Sub D�S�lObjet()
    Dim unObjetM�tier As M�tier = objS�lect.ObjetM�tier
    If TypeOf unObjetM�tier Is PassagePi�ton Then
      Dim unPassage As PassagePi�ton = unObjetM�tier
      If Not IsNothing(unPassage.Zebras) Then DessinerObjet(unPassage.Zebras)
    End If
    objS�lect = Nothing
  End Sub

  '******************************************************************************
  ' Traiter le message de l'op�ration Glisser lors du MouseMove ou du MouseUp
  '******************************************************************************
  Private Sub TraiterMessageGlisser(Optional ByVal Continuation As Boolean = False)
    Dim msg, Contexte As String

    Select Case UneCommandeGraphique
      Case CommandeGraphique.AucuneCommande
      Case CommandeGraphique.D�placerNord
        msg = "Positionner le Nord"
      Case CommandeGraphique.OrienterNord
        msg = "Orienter le Nord"
      Case CommandeGraphique.D�placerEchelle
        msg = "Positionner l'�chelle"
      Case CommandeGraphique.OrigineBranche
        msg = "Faire Glisser la branche"
        Contexte = "Origine de la branche"
      Case CommandeGraphique.AngleBranche
        msg = "Faire tourner la branche"
        Contexte = "Angle de la branche"
      Case CommandeGraphique.EtirerIlot
        msg = "Etirer l'ilot"
        Contexte = "Ilot"
      Case CommandeGraphique.D�placerIlot
        msg = "D�placer l'ilot"
        Contexte = "Ilot"
      Case CommandeGraphique.ElargirIlot
        msg = "Elargir l'ilot"
        Contexte = "Ilot"

      Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide
        Contexte = "Passage pi�ton"
        If UneCommandeGraphique = CommandeGraphique.PassagePi�tonRapide Then
          msg = "D�signer la branche"
          If Continuation Then msg &= " du passage suivant"
        Else
          Select Case mPoint.Length
            Case 0
              msg = "D�signer un point sur le bord de la chauss�e"
              If Continuation Then msg = "Premier point du passage suivant"
            Case 1
              msg = "D�signer l'extr�mit� du petit cot�"
            Case 2
              msg = "D�signer le 3�me point"
            Case 3
              msg = "D�signer le dernier point"
          End Select
        End If

      Case CommandeGraphique.EditLargeurPassage
        Contexte = "Passage pi�ton"
        msg = "Largeur du passage"
      Case CommandeGraphique.EditLongueurPassage
        Contexte = "Passage pi�ton"
        msg = "Longueur du passage"
      Case CommandeGraphique.EditAnglePassage
        Contexte = "Passage pi�ton"
        msg = "Angle du passage"
      Case CommandeGraphique.EditPointPassage
        Contexte = "Passage pi�ton"
        msg = "Modifier le passage"

      Case CommandeGraphique.D�placerPassage
        Contexte = "Passage pi�ton"
        msg = "D�placer le passage"
      Case CommandeGraphique.SupprimerPassage
        Contexte = "Passage pi�ton"
        msg = "D�signer un passage pi�ton"

      Case CommandeGraphique.Trajectoire
        Contexte = "Trajectoire"
        If mPoint.Length = 0 Then
          msg = "D�signer la voie entrante de la trajectoire" & IIf(Continuation, " suivante", "")
        Else
          msg = "D�signer la voie sortante"
        End If

      Case CommandeGraphique.EditerTrajectoire
        Contexte = "Trajectoire"
        If mPoint.Length = 0 Then
          msg = "D�signer un point"
        Else
          msg = "D�signer le point suivant, ou cliquer sur le d�but de la voie sortante pour terminer"
        End If

      Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire
        Contexte = "Trajectoire"
        msg = "Positionner le point d'acc�s"
      Case CommandeGraphique.EditerPointTrajectoire
        Contexte = "Trajectoire"
        msg = "Positionner le point interm�diaire"

      Case CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.PropTrajectoire
        Contexte = "Trajectoire"
        msg = "D�signer une trajectoire"

      Case CommandeGraphique.Travers�e, CommandeGraphique.D�composerTravers�e, CommandeGraphique.PropTravers�e
        msg = "D�signer un passage pi�ton"
        Contexte = "Travers�e pi�tonne"

      Case CommandeGraphique.LigneFeux
        Contexte = "Ligne de feux"
        If mPoint.Length = 0 Then
          msg = "D�signer le point de d�part de la ligne de feux" & IIf(Continuation, " suivante", "")
        Else
          msg = "D�signer l'extr�mit� de la ligne de feux"
        End If
      Case CommandeGraphique.D�placerLigneFeu
        Contexte = "Ligne de feux"
        msg = "D�placer la ligne de feux"
      Case CommandeGraphique.AllongerFeu
        Contexte = "Ligne de feux"
        msg = "Etirer la ligne de feux"
      Case CommandeGraphique.SupprimerLigneFeu
        Contexte = "Ligne de feux"
        msg = "D�signer une ligne de feux"

      Case CommandeGraphique.D�placerSignal
        Contexte = "Signal de feux"
        msg = "Positionner le signal"

      Case CommandeGraphique.D�placerCarrefour
        Contexte = "Carrefour"
        msg = "D�placer le carrefour"

      Case CommandeGraphique.PositionTrafic
        Contexte = "Trafic"
        msg = "Positionner les �critures du trafic"

      Case CommandeGraphique.Antagonisme
        Contexte = "Antagonismes"
        msg = "Valider le conflit"

      Case CommandeGraphique.ZoomPAN
        Contexte = "Panoramique"
        Select Case mPoint.Length
          Case 0
            msg = "1er point du d�placement"
          Case 1
            msg = "Extr�mit� du d�placement"
        End Select

      Case CommandeGraphique.Mesure
        Select Case mPoint.Length
          Case 0
            msg = "D�signer le point de r�f�rence"
          Case 1
            ' D�signer le point cherch�
            msg = ""
        End Select
    End Select

    TraiterMessageGraphique(msg, Contexte)
    picDessin.Cursor = CurseurCommande()

  End Sub

  '******************************************************************************
  ' Traiter le message graphique pour l'afficher dans le Label ad�quat
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
      Dim unAntagonisme As Antagonisme = objS�lect.ObjetM�tier
      Dim unTrafic As Trafic = monPlanFeuxBase.Trafic
      Dim fgAntago As GrilleDiagfeux = Me.AC1GrilleAntagonismes

      Try
        With FenetreAntagonisme
          'Libell� des courants antagonistes
          If unAntagonisme.EstPi�ton Then
            .lblLibell�Conflit.Text = "Conflit entre le courant " & unAntagonisme.Libell�(Antagonisme.PositionEnum.Premier, mesBranches) & " et  " & unAntagonisme.Libell�(Antagonisme.PositionEnum.Dernier, mesBranches)
          Else
            .lblLibell�Conflit.Text = "Conflit entre les courants " & unAntagonisme.Libell�(Antagonisme.PositionEnum.Premier, mesBranches) & " et  " & unAntagonisme.Libell�(Antagonisme.PositionEnum.Dernier, mesBranches)
          End If
          .pnlConflit.Enabled = (maVariante.Verrou = [Global].Verrouillage.LignesFeux)

          'Traitement des boutons radios
          .mAntagonisme = Nothing   ' Pou inhiber les actions cons�cutives au (d�)cochage des boutons radios
          Select Case unAntagonisme.TypeConflit
            Case Trajectoire.TypeConflitEnum.Syst�matique
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
            'Ajout V13 (oubli�  v12)
            .lblAlertePi�tons.Visible = False

          Else
            Dim valTrafic1, valTrafic2 As Short
            With unAntagonisme
              valTrafic1 = .Courant(Antagonisme.PositionEnum.Premier).valTrafic(unTrafic)
              If .EstV�hicule Then valTrafic2 = .Courant(Antagonisme.PositionEnum.Dernier).valTrafic(unTrafic)
            End With
            .lblMessageConflit.ForeColor = System.Drawing.SystemColors.ControlText
            .lblMessageConflit.Visible = True
            .lblAlerte.Visible = False
            .lblAlertePlus.Visible = False
            .lblAlertePi�tons.Visible = False
            Dim Alerte, AlertePlus, AlertePi�tons As Boolean
            Dim unTypeCourant As Antagonisme.AntagonismeEnum = unAntagonisme.TypeCourantsAntagonistes
            Select Case unTypeCourant
              Case Antagonisme.AntagonismeEnum.TDTAG ' ,Antagonisme.AntagonismeEnum.TAGTAG : celui-ci peut-il �tre assimil� au pr�c�dent ?
                .lblMessageConflit.Text = "Trafic : " & valTrafic1 & " - TAG : " & valTrafic2
                'cndAbaque.AjouterTrafics(valTrafic1, valTrafic2)
              Case Antagonisme.AntagonismeEnum.TDTAD, Antagonisme.AntagonismeEnum.TAGTAD ': celui-ci peut-il �tre assimil� au pr�c�dent ?
                .lblMessageConflit.Text = "Trafic : " & valTrafic1 & " - TAD : " & valTrafic2
                If valTrafic2 >= 500 Then Alerte = True
              Case Antagonisme.AntagonismeEnum.TADPi�tons, Antagonisme.AntagonismeEnum.TADPi�tonsEtSensUnique
                .lblMessageConflit.Text = "Trafic TAD : " & valTrafic1
                If valTrafic1 >= 500 Then
                  AlertePi�tons = True
                  If unTypeCourant = Antagonisme.AntagonismeEnum.TADPi�tons Then
                    Alerte = True
                  Else
                    'TADPi�tons et Sens unique
                    AlertePlus = True
                  End If
                End If
              Case Antagonisme.AntagonismeEnum.TAGPi�tons, Antagonisme.AntagonismeEnum.TAGPi�tonsEtSensUnique
                .lblMessageConflit.Text = "Trafic TAG : " & valTrafic1
                If valTrafic1 >= 500 Then
                  AlertePi�tons = True
                  If unTypeCourant = Antagonisme.AntagonismeEnum.TAGPi�tons Then
                    Alerte = True
                  Else
                    'TAGPi�tons et Sens unique
                    AlertePlus = True
                  End If
                End If
            End Select

            'If unTypeCourant = Antagonisme.AntagonismeEnum.TDTAG Then
            '  cndAbaque.Show()
            '  Application.DoEvents()
            '  R�affecterEchelle()
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
            If AlertePi�tons Then
              .lblAlertePi�tons.Visible = True
            End If
          End If

          If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
            .lblMessageConflit.Text = "Courants strictement incompatibles"
            .lblMessageConflit.Visible = True
          End If

          If maVariante.Verrou > [Global].Verrouillage.LignesFeux Then
            If Not IsNothing(maVariante.BrancheEnCoursAntagonisme) Then
              'S�lectionner la bonne branche dans la combo sauf si on voit tous les antagonismes (choix 'Tous' dans la combo)
              Me.cboBrancheCourant1.SelectedIndex = mesBranches.IndexOf(unAntagonisme.BrancheCourant1)
            End If
            'S�lectionner la ligne correspondante dans la grille
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
      CommandeGraphique.EtirerIlot, CommandeGraphique.D�placerIlot, CommandeGraphique.ElargirIlot, _
      CommandeGraphique.EditLargeurPassage, CommandeGraphique.EditLongueurPassage, CommandeGraphique.EditAnglePassage, CommandeGraphique.EditPointPassage, _
      CommandeGraphique.D�placerLigneFeu, CommandeGraphique.AllongerFeu
        Return False

      Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide, CommandeGraphique.SupprimerPassage, CommandeGraphique.Trajectoire, CommandeGraphique.SupprimerTrajectoire, _
       CommandeGraphique.Travers�e, CommandeGraphique.D�composerTravers�e, CommandeGraphique.LigneFeux, CommandeGraphique.SupprimerLigneFeu
        Return True

      Case CommandeGraphique.PropTrajectoire, CommandeGraphique.EditerTrajectoire
        Return True

      Case CommandeGraphique.PropTravers�e
        Return True

      Case CommandeGraphique.D�placerSignal, CommandeGraphique.D�placerCarrefour
        Return True
        'Case CommandeGraphique.Zoom
        'Case CommandeGraphique.ZoomMoins
        'Case CommandeGraphique.ZoomPr�c�dent

        'Case CommandeGraphique.PositionTrafic
        'Case CommandeGraphique.Antagonisme

    End Select

  End Function

  Private Function CommandeAvecAide() As Boolean
    If CommandeDeCr�ation() Then
      Return True
    ElseIf CommandeDeSuppression() Then
      Return True
    ElseIf CommandeInformation() Then
      Return True
    ElseIf UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
      Return True
    End If
  End Function

  Private Function CommandeDeCr�ation(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique

    Select Case uneCommande
      Case CommandeGraphique.PassagePi�ton, CommandeGraphique.PassagePi�tonRapide, CommandeGraphique.Trajectoire, CommandeGraphique.Travers�e, CommandeGraphique.LigneFeux
        Return True
    End Select
  End Function

  Private Function CommandeDeSuppression(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu, CommandeGraphique.D�composerTravers�e
        Return True
    End Select
  End Function

  Private Function CommandeInformation(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.PropTrajectoire, CommandeGraphique.PropTravers�e
        Return True
    End Select
  End Function

  Private Function CommandeConservantS�lection(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPr�c�dent, CommandeGraphique.ZoomPAN
        Return True
      Case CommandeGraphique.Antagonisme
        Return True
    End Select
  End Function

  Private Function CommandeN�cessitantS�lection(Optional ByVal uneCommande As CommandeGraphique = CommandeGraphique.EnCours) As Boolean
    If uneCommande = CommandeGraphique.EnCours Then uneCommande = UneCommandeGraphique
    Select Case uneCommande
      Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPr�c�dent, CommandeGraphique.ZoomPAN
        Return True
    End Select

  End Function

  Private Function PtTrajectoire(ByVal pSouris As Point)
    Dim uneBranche As Branche
    For Each uneBranche In mesBranches
      If uneBranche.PtInt�rieur(pSouris) Then Return pSouris
    Next
  End Function

  '******************************************************************************
  ' D�terminer le nouveau point de la ligne de feu sur la bordure de la voie
  '******************************************************************************
  Private Function PtLigneFeuD�plac�(ByVal pSouris As Point) As Point
    Dim pOrigine As Point = mPoint(0)   ' Position de l'origine de la ligne de feu pr�c�dente
    'Projeter le pointeur souris sur le bord de la voie
    Dim pProjet� As Point = Projection(pSouris, Segment1)
    If Segment1.PtSurSegment(pProjet�) Then Return pProjet�
  End Function

  '******************************************************************************
  ' D�terminer le point final de la ligne de feux v�hicules
  '******************************************************************************
  Private Function PtLigneFeux(ByVal pSouris As Point) As Point
    Dim p As Point
    If ContourPermis.Int�rieur(pSouris) Then
      'If BrancheLi�e.PtInt�rieur(pSouris) Then
      p = Projection(pSouris, mPoint(0), AngleProjection)
    End If
    Return p
  End Function

  '******************************************************************************
  ' D�terminer si la nouvelle origine de la branche est acceptable
  '******************************************************************************
  Private Function OrigineBrancheOK(ByVal pSouris As Point) As Boolean

    OrigineBrancheOK = EnveloppeBranche.Int�rieur(pSouris)
    OrigineBrancheOK = True
    G�rerCurseur(OrigineBrancheOK)
    Return True
    Return OrigineBrancheOK

    '======================================================================================================================
    'D�finition du segment interm�diaire joignant les 2 bords de chauss�e (si on accepte pSouris comme nouvelle origine de la branche)
    Dim p1, p2 As Point
    'Nouvelle extr�mit� du bord de chauss�e droite
    p1 = PointPosition(pSouris, LargeurBranche / 2, AngleBranche + sngPI / 2)
    Dim l1 As Ligne = New Ligne(p1, PointPosition(p1, AngleBranche))
    'Nouvelle extr�mit� du bord de chauss�e gauche
    p2 = PointPosition(pSouris, LargeurBranche / 2, AngleBranche - sngPI / 2)
    Dim l2 As Ligne = New Ligne(p2, PointPosition(p2, AngleBranche))

    Dim SegmentInterm�diaire As Ligne = New Ligne(p1, p2, New Pen(Color.Red))

    If intersect(Segment1, SegmentInterm�diaire).IsEmpty Then
      'Le segment n'intersecte pas la branche pr�c�dente
      If intersect(Segment2, SegmentInterm�diaire).IsEmpty Then
        'Le segment n'intersecte pas la branche suivante
        If intersect(SegmentLimite, SegmentInterm�diaire).IsEmpty Then
          'Le segment n'intersecte pas le segment joignant les origines de la branche pr�c�dente et de la branche suivante
          If intersect(p1, Segment1.pA, p2, Segment2.pA, TypeInterSect:=Formules.TypeInterSection.SurSegment).IsEmpty Then
            'Il n'y a pas de rebroussement :les segments de raccordement de branche ne s'intersectent pas
            If intersect(Segment1, l1, Formules.TypeInterSection.SurPremierSegmentStrict).IsEmpty Then
              'Le prolongement du nouveau bord de chauss�e droite n'intersecte pas le bord gauche pr�c�dent
              If intersect(Segment2, l2, Formules.TypeInterSection.SurPremierSegmentStrict).IsEmpty Then
                'Le prolongement du nouveau bord de chauss�e gauche n'intersecte pas le bord droit pr�c�dent
                If AngleForm�(SegmentLimite.pA, pSouris, SegmentLimite.pB) >= 0 Then
                  'Sinon L'origine est pass�e de l'autre cot� du carrefour
                  OrigineBrancheOK = True
                End If
              End If
            End If
          End If
        End If
      End If
    End If

    G�rerCurseur(OrigineBrancheOK)

  End Function

  Private Sub G�rerCurseur(ByVal OK As Boolean)

    If picDessin.Cursor Is Cursors.No Xor Not OK Then
      'Bacule OK<-->PASOK
      If OK Then
        'Bascule -->OK : d�terminer le curseur appropri� selon la commande graphique
        picDessin.Cursor = CurseurCommande()
      Else
        'Bascule -->PASOK : Sens interdit
        picDessin.Cursor = Cursors.No
      End If
    End If

  End Sub

  '******************************************************************************
  ' D�terminer si le nouvel angle est acceptable
  '******************************************************************************
  Private Function AngleBrancheOK(ByVal pSouris As Point) As Boolean

    Dim AngleFinal As Single = CvAngleDegr�s(AngleForm�(mPoint(0), pSouris), SurDeuxPi:=False)
    If AngleFinal < AngleMini Then AngleFinal += 360
    AngleBrancheOK = (AngleFinal - AngleMini) < BalayageMaxi

    AngleProjection = EqvRadian(AngleFinal) + Math.PI / 2
    If AngleProjection > PI Then AngleProjection -= 2 * PI

    G�rerCurseur(AngleBrancheOK)

  End Function

  '******************************************************************************
  ' D�terminer le nouveau point P1 ou P3 (ou son sym�trique P4): Point g�rant l'�largissement de l'ilot
  '******************************************************************************
  Private Function PtIlot(ByVal pSouris As Point) As Point
    Dim pOrigine As Point = mPoint(0)   ' P2
    Dim pProjet� As Point = Projection(pSouris, pOrigine, AngleProjection)

    If UneCommandeGraphique = CommandeGraphique.EtirerIlot Then
      'P1 � Au moins 1 m de P2 (Rayon >= 1m)
      If PointDansPicture(pProjet�) And DistanceR�elle(pProjet�, pOrigine) >= Ilot.miniRayon Then
        'P1 reste du m�me cot� de l'axe P3P4
        If Sign(AngleProjection) = Sign(AngleForm�(pProjet�, pOrigine)) Then Return pProjet�
      End If
    Else
      'CommandeGraphique.ElargirIlot
      Dim Dist As Single = DistanceR�elle(pProjet�, pOrigine)
      If Dist < Ilot.maxiLargeur / 2 And Dist >= Ilot.miniLargeur / 2 Then Return pProjet�
    End If

  End Function

  '******************************************************************************
  ' D�terminer si le nouveau point P2 est acceptable : : Point g�rant le d�placement de l'ilot
  '******************************************************************************
  Private Function P2IlotOK(ByVal pSouris As Point) As Boolean
    'Retrouver le point P2 futur en fonction de pSouris
    Dim NewP2 As Point = Translation(pSouris, DecalV(3))
    'Extr�mit� du bord droit de la chauss�e
    Dim pOrigine As Point = mPoint(0)

    'Projeter le point sur le bord droit de la chauss�e
    Dim pProjet� As Point = P6Ilot(NewP2)

    'Projeter le point sur l'axe de la branche
    Dim pSurAxe As Point = Projection(NewP2, BrancheLi�e.LigneDeSym�trie)
    Dim Retrait As Single
    If DistanceR�elle(NewP2, pSurAxe) < BrancheLi�e.Largeur / 2 Then
      Retrait = RetraitIlot(pOrigine, pProjet�)
      P2IlotOK = Retrait >= Ilot.miniRetrait And Retrait <= Ilot.maxiRetrait
    End If

    G�rerCurseur(P2IlotOK)

  End Function

  Private Function RetraitIlot(ByVal pOrigine As Point, ByVal pProjet� As Point)
    Dim Retrait As Single = DistanceR�elle(pOrigine, pProjet�)
    If Retrait <> 0.0 Then
      Dim unAngle As Single = AngleForm�(pOrigine, pProjet�)
      'La soustraction ci-dessous retourne 0 ou PI : si ce n'est pas 0, c'est que l'ilot est rentr� dans le carrefour
      If Abs(unAngle - BrancheLi�e.AngleEnRadians) > 0.1 Then Retrait = -Retrait
    End If

    Return Retrait

  End Function

  '******************************************************************************
  ' D�terminer le nouveau point P6 � parti de P2(Point g�rant le d�placement de l'ilot)
  '******************************************************************************
  Private Function P6Ilot(ByVal NewP2 As Point) As Point

    P6Ilot = Projection(NewP2, BrancheLi�e.BordChauss�e(Branche.Lat�ralit�.Droite))

  End Function

  '******************************************************************************
  ' D�terminer le point suivant du passage pi�ton � dessiner
  '******************************************************************************
  Private Function PtPassage(ByVal pSouris As Point) As Point
    Dim p As Point

    Select Case mPoint.Length
      Case 1
        ' D�finir le 2�me point issu du premier en restant parall�le � la branche
        p = Projection(pSouris, mPoint(0), BrancheLi�e.AngleEnRadians)
        'Largeur du passage �gale au moins � 2 m
        If DistanceR�elle(p, mPoint(0)) >= PassagePi�ton.miniLargeur Then
          Return p
        End If

      Case 2
        ' D�finir le 3�me point 
        p = pSouris
        'Projeter le point sur le bord de chasss�e la plus proche : p est pass� par r�f�rence
        Select Case BrancheLi�e.BordChauss�eProche(p)
          Case Branche.Lat�ralit�.Aucune
            'Il suffit que le point soit � l'int�rieur de la branche
            Return pSouris

          Case BordChauss�ePassage
            ' Refuser un point du m�me cot� de la branche
          Case Else
            ' Retourner le point sur le bord de chauss�e oppos�
            Return p
        End Select

      Case Else
        'Point final

        If Distance(pSouris, mPoint(2)) > 0 Then
          Dim unAngle As Single = AngleForm�(mPoint(1), mPoint(2))
          p = Point.Ceiling(intersect(New Ligne(mPoint(2), pSouris, Nothing), New Ligne(mPoint(0), PointPosition(mPoint(0), unAngle), Nothing), TypeInterSect:=Formules.TypeInterSection.Indiff�rent))
          If Not p.IsEmpty Then
            If Sign(unAngle) = Sign(AngleForm�(mPoint(0), p)) Then Return p
          End If
        Else
          Return pSouris
        End If
    End Select

  End Function

  '******************************************************************************
  ' D�terminer le point suivant de la trajectoire � dessiner
  '******************************************************************************
  Private Function PTrajVeh(ByVal pSouris As Point) As Point
    Dim p As Point
    mScreen2 = picDessin.PointToScreen(pSouris)
  End Function

  '******************************************************************************
  ' Valider si le point cliqu� est acceptable pour construire le passage
  '******************************************************************************
  Private Function EditPassageOK2(ByVal pSouris As Point) As Point
    Dim pProjet� As Point = Projection(pSouris, Segment1)

    Return pProjet�

  End Function

  '*************************************************************************************************
  ' Valider si le point cliqu� est acceptable pour construire le passage
  ' AnglePassage : Changement de l'orientation du passage (angle des 2 cot�s parall�les du trap�ze)
  '**************************************************************************************************
  Private Function EditPassageOK3(ByVal pSouris As Point) As Point
    Dim p0, p2 As Point
    Dim unAngle As Single

    p0 = CvPoint(intersect(New Ligne(pSouris, mPoint(0)), Segment2, Formules.TypeInterSection.Indiff�rent))

    If Math.Sign(AngleForm�(p0, Segment1.pA, Segment1.pB)) = SigneConserv� Then
      'Interdire de passer de l'autre cot� du segment invariant

      unAngle = AngleForm�(mPoint(0), p0)
      Dim l1 As New Ligne(Segment1.pB, PointPosition(Segment1.pB, 100.0, unAngle))
      p2 = CvPoint(intersect(l1, Segment2, Formules.TypeInterSection.Indiff�rent))
      If DistanceR�elle(p0, p2) >= PassagePi�ton.miniLargeur AndAlso DistanceR�elle(Segment1.pA, l1) >= PassagePi�ton.miniLargeur Then
        AngleParall�le = unAngle
        mScreen(2) = picDessin.PointToScreen(p2)
        picDessin.Cursor = CurseurCommande()
        Return p0
      End If
    End If

  End Function

  '*****************************************************************************************
  ' Valider si le point cliqu� est acceptable pour construire le passage
  ' PointPassage : d�placement d'un point
  ' EditLargeurPassage, EditLongueurPassage : �largissement ou allongement du passage
  '*****************************************************************************************
  Private Function EditPassageOK4(ByVal pSouris As Point) As Point
    Dim pASegment1, pBSegment2 As Point
    Dim L1, L2 As Ligne

    'Controle pr�alable sur la largeur du passage pi�ton
    If UneCommandeGraphique <> CommandeGraphique.EditLongueurPassage AndAlso DistanceR�elle(pSouris, SegmentLimite) < PassagePi�ton.miniLargeur Then
      Return pASegment1
    End If

    If UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
      pASegment1 = pSouris
      'Segment2 : segment sur lequel coulisse le point oppos� au point en cours de d�placement (pSouris)

    Else
      'EditLargeurPassage, EditLongueurPassage
      'Segment1 et Segment2 : segments sur lesquels coulissent les 2 points dont on d�place le milieu(pSouris)
      pASegment1 = CvPoint(intersect(New Ligne(pSouris, PointPosition(pSouris, 100.0, AngleParall�le)), Segment1, Formules.TypeInterSection.Indiff�rent))
    End If

    pBSegment2 = CvPoint(intersect(New Ligne(pSouris, PointPosition(pSouris, 100.0, AngleParall�le)), Segment2, Formules.TypeInterSection.Indiff�rent))

    L1 = New Ligne(pSouris, mPoint(0))
    L2 = New Ligne(pBSegment2, Segment2.pA)  ' pBSegment2 est sur Segment2 : L2 est le futur Segment2

    If CvPoint(intersect(L1, L2)).IsEmpty Then
      'Le point en cours de d�placement ne doit pas traverser le petit cot� oppos�
      If Math.Sign(AngleForm�(pSouris, SegmentLimite.pA, SegmentLimite.pB)) = SigneConserv� Then
        'Conserver le sens des points :ceci empeche le grand cot� en cours de modif (parall�le � SegmentLimite) � passer de l'autre cot� de SegmentLimite
        'SegmentLimite : Segment invariant du passage (segment oppos� au point en cours de modification)
        'Le futur Segment1 : (pSouris, mPoint(0)=Segment1.PB) - Recalculer la nouvelle bissectrice
        If UneCommandeGraphique = CommandeGraphique.EditPointPassage Then
          AngleProjection = (AngleForm�(pSouris, mPoint(0)) + AngleForm�(SegmentLimite)) / 2
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
    'FermerPassage : ne sert que pour la commande PassagePi�ton
    Static FermerPassage As Boolean

    Try
      Select Case UneCommandeGraphique
        'NORD
      Case CommandeGraphique.OrienterNord
          Dim unNord As Nord = objS�lect.ObjetM�tier
          colObjetsGraphiques.Remove(unNord.mGraphique)
          unNord.Orientation = AngleForm�(CentreRotation, pMouseUp)
          unNord.Cr�erGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

        Case CommandeGraphique.D�placerNord
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV(0))
          Dim unNord As Nord = objS�lect.ObjetM�tier
          colObjetsGraphiques.Remove(unNord.mGraphique)
          unNord.PtR�f�rence = p
          unNord.Cr�erGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

          'ECHELLE
        Case CommandeGraphique.D�placerEchelle
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV(0))
          'LEs coordonn�es du point de r�f�rence de l'�chelle sont relatives au point bas gauche : il faut inverser le sens des Y
          p.Y = picDessin.ClientSize.Height - p.y
          Dim uneEchelle As SymEchelle = objS�lect.ObjetM�tier
          colObjetsGraphiques.Remove(uneEchelle.mGraphique)
          uneEchelle.PtR�f�rence = p
          uneEchelle.Cr�erGraphique(colObjetsGraphiques)
          Redessiner()
          TerminerCommande = True

          'BRANCHE
        Case CommandeGraphique.OrigineBranche
          DessinerElastique()
          If OrigineBrancheOK(pMouseUp) Then
            Dim p As Point = Translation(pMouseUp, DecalV(0))
            If PointDansPicture(p) Then
              uneBranche = objS�lect.ObjetM�tier
              uneLigne = uneBranche.LigneDeSym�trie
              With uneLigne
                .pA = p
                .pB = Translation(pMouseUp, DecalV(1))
                uneBranche.AttribuerOrigine(PointR�el(.pA))
                RedessinerBranche(uneBranche)
              End With
            End If
            Modif = True
          Else
            D�s�lectionner()
          End If
          TerminerCommande = True

        Case CommandeGraphique.AngleBranche
          uneBranche = objS�lect.ObjetM�tier
          uneLigne = uneBranche.LigneDeSym�trie
          If AngleBrancheOK(pMouseUp) Then
            uneBranche.Angle = CvAngleDegr�s(AngleForm�(CentreRotation, pMouseUp))
            Dim uneGrille As GrilleDiagfeux = Me.AC1GrilleBranches
            Dim uneCellule As Grille.CellRange = uneGrille.GetCellRange(mesBranches.IndexOf(uneBranche) + 1, 2)
            uneCellule.Data = Math.Round(uneBranche.Angle)
            RedessinerBranche(uneBranche)
            Modif = True
          Else
            DessinerElastique()
            D�s�lectionner()
          End If
          TerminerCommande = True

          'EDITION ILOT
        Case CommandeGraphique.EtirerIlot, CommandeGraphique.D�placerIlot, CommandeGraphique.ElargirIlot
          ' M�moriser la position courante de la souris
          ' en Coordonn�es du PictureBox 
          ReDim Preserve mPoint(mPoint.Length)
          mPoint(mPoint.Length - 1) = pMouseUp
          unIlot = objS�lect.ObjetM�tier
          If Red�finirIlot(unIlot) Then
            Modif = True
          End If
          TerminerCommande = True

          'PASSAGE PIETON
        Case CommandeGraphique.PassagePi�tonRapide
          TerminerPassage(New PassagePi�ton(BrancheLi�e))
          TerminerCommande = True

        Case CommandeGraphique.PassagePi�ton
          Dim p As Point = PtPassage(pMouseUp)
          If Not p.IsEmpty Then
            ' M�moriser la position courante de la souris
            ' en Coordonn�es du PictureBox 
            ReDim Preserve mPoint(mPoint.Length)
            mPoint(mPoint.Length - 1) = p
            ReDim Preserve mScreen(mScreen.Length)
            mScreen1 = mScreen2 ' picDessin.PointToScreen(p)
            mScreen(mScreen.Length - 1) = mScreen1
            If mPoint.Length = DernierPoint() Then
              objS�lect = Cr�erPassage(FermerPassage)
              FermerPassage = False
              TerminerCommande = True
            Else
              If mScreen.Length = 3 And IsNothing(monFDP) Then
                If BrancheLi�e.BordChauss�eProche(p) <> Branche.Lat�ralit�.Aucune Then
                  FermerPassage = True
                  TerminerCommande = TerminerCommande(PointPosition(p, CType(AngleForm�(mPoint(1), mPoint(0)), Single)))
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

            Dim unPassage As PassagePi�ton = objS�lect.ObjetM�tier
            unPassage.AffecterPoint(p, Poign�eCliqu�e, Red�finirVoies:=True)
            'Poign�e sym�trique  � modifier en cons�quence
            p = picDessin.PointToClient(mScreen(2))
            If UneCommandeGraphique = CommandeGraphique.EditPointPassage Or UneCommandeGraphique = CommandeGraphique.EditLargeurPassage Then        'La poign�e sym�trique est sur un grand cot� (un des 2 cot�s parall�les du trap�ze)
              Poign�eCliqu�e = 3 - Poign�eCliqu�e

            Else
              'La poign�e cliqu�e est sur un petit cot� (proche du bord de chauss�e ou �ventuellement d'un ilot)
              Select Case Poign�eCliqu�e
                Case 0
                  Poign�eCliqu�e = 1
                Case 1
                  Poign�eCliqu�e = 0
                Case 2
                  Poign�eCliqu�e = 3
                Case 3
                  Poign�eCliqu�e = 2
              End Select
            End If
            unPassage.AffecterPoint(p, Poign�eCliqu�e, Red�finirVoies:=True)
            objS�lect = DessinerPassage(unPassage)
            Modif = True

            TerminerCommande = True
          End If

        Case CommandeGraphique.D�placerPassage
          DessinerElastique()
          Dim p As Point = EditPassageOK2(pMouseUp)
          If Not p.IsEmpty Then
            Dim unPassage As PassagePi�ton = objS�lect.ObjetM�tier
            Dim i As Short
            For i = 0 To 3
              unPassage.AffecterPoint(Translation(p, DecalV(i)), i, Red�finirVoies:=False)
            Next
            objS�lect = DessinerPassage(unPassage)
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
            AfficherMessageErreur(Me, "D�signer une voie sortante")
          ElseIf VoieTraj.mBranche Is VoieOrigine.mBranche Then
            AfficherMessageErreur(Me, "D�signer une voie sur une autre branche")
          ElseIf mesTrajectoires.Existe(VoieOrigine, VoieTraj) Then
            AfficherMessageErreur(Me, "Cette trajectoire est d�j� d�finie")
            EffacerElastiques()
            TerminerCommande = True
          Else
            objS�lect = Cr�erTrajectoire()
            TerminerCommande = True
          End If
          If IsNothing(objS�lect) Then VoieTraj = VoieOrigine

        Case CommandeGraphique.EditerTrajectoire
          If EnAttenteMouseUp Then
            EnAttenteMouseUp = False
          Else
            Dim p As Point = PtEditTrajectoire(pMouseUp)
            If Not p.IsEmpty Then
              If Distance(p, mPoint1) > RayS�lect Then
                'Commande non termin�e : m�morisation du point cliqu� et pr�parationde la saisie du suivant
                Dim pScreen As Point = picDessin.PointToScreen(pMouseUp)
                'Effacer le ou les segments pr�c�dents
                DessinerElastique()
                'Dessiner en couleur le segment cr��
                ControlPaint.DrawReversibleLine(mScreen(mScreen.Length - 3), pScreen, Color.Fuchsia)
                'Rajouter un point au tableau d'�lastiques
                ReDim Preserve mScreen(mScreen.Length)
                mScreen(mScreen.Length - 2) = pScreen
                mScreen(mScreen.Length - 1) = mScreen2
                DessinerElastique()

              Else
                'L'utilisateur a cliqu� sur le point d'acc�s destination pour terminer la commande
                DessinerElastique()
                ControlPaint.DrawReversibleLine(mScreen(mScreen.Length - 3), mScreen2, Color.Fuchsia)
                ReDim Preserve mScreen(mScreen.Length - 2)
                'Ajouter le point d'acc�s � la branche destination
                mScreen(mScreen.Length - 1) = mScreen2
                Cr�erTrajectoire()
                TerminerCommande = True
              End If
            End If
          End If

        Case CommandeGraphique.EditerOrigineTrajectoire, CommandeGraphique.EditerDestinationTrajectoire, CommandeGraphique.EditerPointTrajectoire
          Dim p As Point = PtEditTrajectoire(pMouseUp)
          If Not p.IsEmpty Then
            DessinerElastique()
            Dim uneTrajectoire As TrajectoireV�hicules = objS�lect.ObjetM�tier

            Select Case UneCommandeGraphique
              Case CommandeGraphique.EditerOrigineTrajectoire
                uneTrajectoire.AffecterPointAcc�s(picDessin.PointToClient(mScreen(0)), TrajectoireV�hicules.OrigineDestEnum.Origine)
              Case CommandeGraphique.EditerDestinationTrajectoire
                uneTrajectoire.AffecterPointAcc�s(picDessin.PointToClient(mScreen(0)), TrajectoireV�hicules.OrigineDestEnum.Destination)
              Case Else
                uneTrajectoire.AffecterPointInterm�diaire(picDessin.PointToClient(mScreen(1)), Poign�eCliqu�e)
            End Select

            DessinerTrajectoire(uneTrajectoire)
            TerminerCommande = True
          End If

          'TRAVERSEE PIETONNE
        Case CommandeGraphique.Travers�e
          If BrancheLi�e.mPassages.Count = 1 Then
            AfficherMessageErreur(Me, "Cette branche ne comporte qu'un seul passage pi�ton")
          Else
            maVariante.Cr�erTravers�e(BrancheLi�e, colObjetsGraphiques)
            TerminerCommandeTravers�e()
          End If
          TerminerCommande = True

        Case CommandeGraphique.D�composerTravers�e
          If Not Travers�e.mDouble Then
            AfficherMessageErreur(Me, "Cette travers�e ne comporte qu'un seul passage pi�ton")
          Else
            maVariante.D�composerTravers�e(Travers�e, colObjetsGraphiques)
            TerminerCommandeTravers�e()
          End If
          TerminerCommande = True

        Case CommandeGraphique.PropTravers�e
          dialogueTrajPi�tons(Travers�e)
          TerminerCommande = True

          ' LIGNE DE FEUX
        Case CommandeGraphique.LigneFeux
          Dim p As Point = PtLigneFeux(pMouseUp)
          If Not p.IsEmpty Then
            mPoint(1) = p
            Cr�erLigneDeFeux()
            TerminerCommande = True
          End If

        Case CommandeGraphique.D�placerLigneFeu
          Dim p As Point = PtLigneFeuD�plac�(pMouseUp)
          If p.IsEmpty Then
            DessinerElastique("Terminer")
            DessinerObjet(LigneFeuEnCours.mGraphique)
          Else
            Dim p2 As Point = Translation(p, DecalV(0))
            LigneFeuEnCours.D�calage = Distance(PointDessin(BrancheLi�e.Origine), New Ligne(p, p2)) / Echelle
            Modif = True
            DessinerLigneDeFeux(LigneFeuEnCours)
            objS�lect = LigneFeuEnCours.mGraphique
          End If
          TerminerCommande = True

        Case CommandeGraphique.AllongerFeu
          Dim p As Point = PtLigneFeuD�plac�(pMouseUp)
          If p.IsEmpty Then
            DessinerElastique()
            DessinerObjet(LigneFeuEnCours.mGraphique)
          Else
            mPoint(1) = p
            objS�lect = ModifierLigneDeFeux()
            TerminerCommande = True
          End If

        Case CommandeGraphique.D�placerSignal
          objS�lect = SignalD�plac�(pMouseUp)
          TerminerCommande = True


          'CARREFOUR
        Case CommandeGraphique.D�placerCarrefour
          DessinerElastique()
          Dim p As Point = Translation(pMouseUp, DecalV1)
          If PointDansPicture(p) Then
            PositionnerCarrefour(p)
          End If
          objS�lect = Nothing
          TerminerCommande = True

          'Suppression d'un objet m�tier
        Case CommandeGraphique.SupprimerPassage, CommandeGraphique.SupprimerTrajectoire, CommandeGraphique.SupprimerLigneFeu
          SupprimerObjetM�tier()

          'ZOOMS...
        Case CommandeGraphique.Zoom, CommandeGraphique.ZoomMoins, CommandeGraphique.ZoomPr�c�dent, CommandeGraphique.ZoomPAN
          If UneCommandeGraphique = CommandeGraphique.ZoomPr�c�dent Then
            mEchelles.Remove((mEchelles.Count - 1).ToString)
            mParamDessin = mEchelles((mEchelles.Count - 1).ToString)
          ElseIf UneCommandeGraphique = CommandeGraphique.ZoomPAN Then
            If Distance(pMouseUp, mPoint(0)) < RayS�lect Then Exit Function

            pMouseUp = Point.op_Subtraction(mPoint(0), Point.op_Explicit(pMouseUp))
            mParamDessin = D�terminerNewOrigineR�ellePAN(pMouseUp)
            mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
          Else
            mParamDessin = D�terminerNewOrigineR�elle(pMouseUp, ZoomPlus:=UneCommandeGraphique = CommandeGraphique.Zoom)
            mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
          End If
          cndParamDessin = mParamDessin
          Recr�erGraphique()

          If IsNothing(objS�lect) Then
            Redessiner()
          Else
            Redessiner(ObjetAS�lectionner:=objS�lect.ObjetM�tier.mGraphique)
          End If

          mdiApplication.tbrDiagfeux.Buttons(MDIDiagfeux.BarreOutilsEnum.ZoomPr�c�dent).Visible = mEchelles.Count > 1
          TerminerCommande = True

        Case CommandeGraphique.Mesure
          DessinerElastique()
          MessageBox.Show("Distance : " & mdiApplication.staDiagfeux.Panels(1).Text)
          TerminerCommande = True
      End Select

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

    If TerminerCommande And Not IsNothing(objS�lect) Then
      objS�lect.Dessiner(mBufferGraphics, picDessin.CreateGraphics)
    End If

  End Function

  Private Sub TerminerCommandeTravers�e()
    AfficherLignesDeFeux()
    Modif = True
    Redessiner()

  End Sub

  '******************************************************************************
  ' Positionner le carrefour en fonction de son nouveau centre
  '******************************************************************************
  Private Sub PositionnerCarrefour(ByVal pSouris As Point)
    If Distance(pSouris, PointDessin(maVariante.mCarrefour.mCentre)) > RayS�lect Then
      maVariante.mCarrefour.mCentre = PointR�el(pSouris)
      PositionnerCarrefour()
    End If

  End Sub

  Private Sub PositionnerCarrefour()
    Dim uneBranche As Branche
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In mesTrajectoires
      If uneTrajectoire.EstV�hicule Then
        CType(uneTrajectoire, TrajectoireV�hicules).R�initialiser(ConserverManuel:=False)
      Else
        CType(uneTrajectoire, Travers�ePi�tonne).R�initialiser(False)
      End If
    Next

    Recr�erGraphique()
    Redessiner()
  End Sub

  '******************************************************************************
  ' Positionner le signal de feu en fonction de son nouveau point d'insertion
  '******************************************************************************
  Private Function SignalD�plac�(ByVal pSouris As Point) As Graphique
    EffacerElastiques()

    With SignalFeuEnCours
      Dim p As Point = .PtR�f�rence
      .Position = New Point(pSouris.X - p.X, pSouris.Y - p.Y)
      .Cr�erGraphique(colObjetsGraphiques)
      DessinerObjet(.mGraphique)
      Return .mGraphique
    End With

  End Function

  '******************************************************************************
  ' Supprimer un objet m�tier (Passage pi�ton, ligne de feux.....)
  '       - Supprime �galement l(es) objets(s) graphique(s) associ�(s)
  '******************************************************************************
  Private Sub SupprimerObjetM�tier()
    Dim unPolyArc As PolyArc = objS�lect
    colObjetsGraphiques.Remove(unPolyArc)
    Dim ObjetM�tier As M�tier
    Dim ObjetSuppl�mentaire, ObjetSuppl�mentaire2 As PolyArc

    Select Case UneCommandeGraphique
      Case CommandeGraphique.SupprimerPassage
        ObjetM�tier = unPolyArc.ObjetM�tier
        Dim unPassage As PassagePi�ton = ObjetM�tier
        ObjetSuppl�mentaire = unPassage.Zebras
        unPassage.mBranche.mPassages.Remove(unPassage)
        If maVariante.Verrou = [Global].Verrouillage.G�om�trie Then
          'Supprimer �galement la travers�e pi�tonne et la ligne feux associ�e
          Travers�e = unPassage.mTravers�e
          'Supprimer la ligne de feux
          mesLignesFeux.Remove(Travers�e.LigneFeu, colObjetsGraphiques)
          'R�afficher les lignes de feux en cons�quence
          AfficherLignesDeFeux()
          If Travers�e.mDouble Then
            maVariante.D�composerTravers�e(Travers�e, colObjetsGraphiques)
          End If
          'Supprimer la travers�e
          mesTrajectoires.Remove(unPassage.mTravers�e, colObjetsGraphiques)
        End If
        maVariante.mPassagesEnAttente.Remove(unPassage)

      Case CommandeGraphique.SupprimerTrajectoire
        ObjetM�tier = unPolyArc.ObjetM�tier
        Dim uneTrajectoire As TrajectoireV�hicules = ObjetM�tier
        mesTrajectoires.Remove(uneTrajectoire)
        AfficherLignesDeFeux()

      Case CommandeGraphique.SupprimerLigneFeu
        ObjetM�tier = unPolyArc.ObjetM�tier
        Dim uneLigneFeux As LigneFeuV�hicules = ObjetM�tier
        ObjetSuppl�mentaire = uneLigneFeux.mSignalFeu(0).mGraphique
        If uneLigneFeux.EstPi�ton Then
          ObjetSuppl�mentaire2 = uneLigneFeux.mSignalFeu(1).mGraphique
        End If
        mesLignesFeux.Remove(uneLigneFeux)
        AfficherLignesDeFeux()
    End Select

    If Not IsNothing(ObjetM�tier) Then
      ' D�selectionner l'objet
      S�lD�s�lectionner()
      ' L'effacer de l'�cran
      EffacerObjet(unPolyArc)
      ' Retirer sa repr�sentation graphique des objets � dessiner
      colObjetsGraphiques.Remove(unPolyArc)
      ' Faire la m�me chose s'il y a un objet grahique suppl�mentaire associ� � l'objet m�tier
      If Not IsNothing(ObjetSuppl�mentaire) Then
        EffacerObjet(ObjetSuppl�mentaire)
        colObjetsGraphiques.Remove(ObjetSuppl�mentaire)
        If Not IsNothing(ObjetSuppl�mentaire2) Then
          EffacerObjet(ObjetSuppl�mentaire2)
          colObjetsGraphiques.Remove(ObjetSuppl�mentaire2)
        End If
      End If
      objS�lect = Nothing
      Modif = True
    End If

    savObjS�lect = Nothing

  End Sub

  '******************************************************************************
  ' Refaire le dessin suite � l'�dition de la branche 
  '******************************************************************************
  Private Sub RedessinerBranche(ByVal uneBranche As Branche)
    mesTrajectoires.R�initialiser(ConserverManuel:=True)
    maVariante.Cr�erGraphique(colObjetsGraphiques)
    Redessiner(ObjetAS�lectionner:=uneBranche.mGraphique)
  End Sub

  '******************************************************************************
  ' Red�finir l'ilot et le Redessiner
  ' Retourne True si la construction est autoris�e
  '******************************************************************************
  Private Function Red�finirIlot(ByVal unIlot As Ilot) As Boolean
    Dim OK As Boolean
    Dim P1, P3, p As Point
    Dim pSouris As Point = mPoint(1)
    'pOrigine : Point P2 sauf pour D�placerIlot( Extr�mit� du bord droit de la chauss�e)
    Dim pOrigine As Point = mPoint(0)

    DessinerElastique()
    If UneCommandeGraphique = CommandeGraphique.D�placerIlot Then
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
          .Rayon = DistanceR�elle(pOrigine, P1)
          fg(IndexIlot, 1) = Format(.Rayon, "#0.0")
        ElseIf UneCommandeGraphique = CommandeGraphique.D�placerIlot Then
          Dim NewP2 As Point = Translation(pSouris, DecalV(3))
          Dim pProjet� As Point = P6Ilot(NewP2)
          .D�calage = DistanceR�elle(NewP2, pProjet�)
          .Retrait = RetraitIlot(pOrigine, pProjet�)
          fg(IndexIlot, 2) = Format(.D�calage, "#0.0")
          fg(IndexIlot, 4) = Format(.Retrait, "#0.0")
        ElseIf UneCommandeGraphique = CommandeGraphique.ElargirIlot Then
          .Largeur = 2 * DistanceR�elle(pOrigine, P3)
          fg(IndexIlot, 3) = Format(.Largeur, "#0.0")
        End If
        .Cr�erGraphique(colObjetsGraphiques)
      End With
      Redessiner(ObjetAS�lectionner:=unIlot.mGraphique)
    Else
      D�s�lectionner()
    End If

    Return OK

  End Function

  '******************************************************************************
  ' Effacer les �lastiques dessin�s pendan la commande graphique
  '******************************************************************************
  Private Sub EffacerElastiques()
    Dim Index As Short

    If mScreen.Length > 0 Then
      'Effacer tous les segments de travail
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePi�ton, CommandeGraphique.Trajectoire
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
  ' Cr�er une ligne de feux
  ' Retourne l'objet graphique associ�
  '******************************************************************************
  Private Function Cr�erLigneDeFeux(Optional ByVal uneLigneV�hicules As LigneFeuV�hicules = Nothing) As PolyArc
    Dim uneVoie As Voie
    Dim Cr�ation As Boolean = IsNothing(uneLigneV�hicules)

    Try

      'Instancier la ligne de feux
      If Cr�ation Then
        'Le param�tre Nothing indique qu'il faudra g�n�rer l'ID
        uneLigneV�hicules = New LigneFeuV�hicules(Nothing, BrancheLi�e, cndSignaux.D�fautV�hicule)
      Else
        uneLigneV�hicules.Voies.Clear()
      End If

      Dim uneLigne As Ligne = New Ligne(mPoint(0), mPoint(1))
      Dim Message As String
      'Ins�rer les voies dans le m�me ordre que l'ordre  des voies dans la branche (ceci en parcourantt les voies de la branche
      For Each uneVoie In BrancheLi�e.Voies
        If uneVoie.Entrante Then
          'La voie est prise en compte si elle est entrante et que le dessin coupe son axe
          If Not intersect(uneLigne, uneVoie.Axe).IsEmpty Then
            uneLigneV�hicules.Voies.Add(uneVoie)
          End If
        End If
      Next

      If uneLigneV�hicules.Voies.Count = 0 Then
        Message = "La ligne de feux n'intersecte aucune voie"
      ElseIf Cr�ation Then
        If Not IsNothing(mesLignesFeux.VoiesCoup�es(uneLigneV�hicules.Voies)) Then
          Message = "Une voie ne peut pas �tre command�e par plusieurs lignes de feux"
        End If
      End If

      If Not IsNothing(Message) Then
        AfficherMessageErreur(Me, Message)
        DessinerElastique()
      Else

        uneLigneV�hicules.D�calage = Distance(PointDessin(BrancheLi�e.Origine), New Ligne(mPoint(1), mPoint(0))) / Echelle
        uneLigneV�hicules.D�terminerNatureCourants(mesTrajectoires)

        'Ajouter la ligne � la collection
        If Cr�ation Then
          Dim PositionInsertion As Short = mesLignesFeux.Premi�reLigneV�hiculeDispo
          mesLignesFeux.Insert(PositionInsertion, uneLigneV�hicules)
          Ins�rerLigneDeFeux(PositionInsertion, uneLigneV�hicules)
          ActiverBoutonsLignesFeux()
          uneLigneV�hicules.PositionnerSignal()
        Else
          AfficherLigneDeFeux(uneLigneV�hicules)
        End If

        DessinerLigneDeFeux(uneLigneV�hicules)

        Modif = True

        Return uneLigneV�hicules.mGraphique

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Cr�er ligne de feux")
    End Try
  End Function

  '******************************************************************************
  ' Modifier une ligne de feux
  ' Retourne l'objet graphique associ�
  '******************************************************************************
  Private Function ModifierLigneDeFeux() As PolyArc
    Dim sauvTAD, sauvTD, sauvTAG As Boolean

    With LigneFeuEnCours
      Dim mVoies As New VoieCollection
      Dim uneVoie As Voie
      'M�moriser les propri�t�s de la ligne de feux
      For Each uneVoie In .Voies
        mVoies.Add(uneVoie)
      Next
      sauvTAD = .TAD
      sauvTD = .TD
      sauvTAG = .TAG

      'Red�finir les nouvelles voies coup�es
      ModifierLigneDeFeux = Cr�erLigneDeFeux(LigneFeuEnCours)

      If IsNothing(ModifierLigneDeFeux) Then
        'La mise � jour a �chou� car la ligne de feux n'intersecte aucune voie
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
  ' Retourne l'objet graphique associ�
  '******************************************************************************
  Private Sub DessinerLigneDeFeux(ByVal uneLigneFeux As LigneFeux)
    Dim unPolyarc As PolyArc

    With uneLigneFeux
      'Cr�er le dessin de la  ligne de feux et son signal associ�
      unPolyarc = .Cr�erGraphique(colObjetsGraphiques)
      If uneLigneFeux.EstV�hicule Then
        'Dessiner les 2 objets
        DessinerObjet(.mGraphique)
        'If .SignalDessinable Then
        DessinerObjet(.mSignalFeu(0).mGraphique)
        'End If

      Else
        'Dessiner les 2 signaux
        'If .SignalDessinable Then
        DessinerObjet(.mSignalFeu(0).mGraphique)
        If CType(uneLigneFeux, LigneFeuPi�tons).SignalARepr�senter(1) Then
          DessinerObjet(.mSignalFeu(1).mGraphique)
        End If
        'End If
      End If
    End With

  End Sub

  '******************************************************************************
  ' Cr�er une trajectoire v�hicules
  ' Retourne l'objet graphique associ�
  '******************************************************************************
  Private Function Cr�erTrajectoire() As PolyArc
    Dim natCourant As TrajectoireV�hicules.NatureCourantEnum
    Dim typeCourant As TrajectoireV�hicules.TypeCourantEnum
    Dim coefG�ne As Single

    FenetreAideCommande.Hide()


    Dim uneTrajectoire As TrajectoireV�hicules
    If UneCommandeGraphique = CommandeGraphique.Trajectoire Or UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires Then
      uneTrajectoire = dialogueTrajV�hicules(Nothing)
    Else
      uneTrajectoire = dialogueTrajV�hicules(CType(objS�lect.ObjetM�tier, TrajectoireV�hicules))
    End If

    If Not IsNothing(uneTrajectoire) Then
      DessinerTrajectoire(uneTrajectoire)
      'La ligne qui suit peut poser des pb en s�lectionnant automatiquement la trajectoire
      'Cr�erTrajectoire = uneTrajectoire.mGraphique
    End If

  End Function

  '******************************************************************************
  ' Afficher la boite de dialogue travers�e pi�tonne 
  ' Permet de construire une travers�e � partir de 2 passages pi�tons ou inversement
  '******************************************************************************
  Private Sub dialogueTrajPi�tons(ByVal uneTravers�e As Travers�ePi�tonne)
    Dim dlg As New dlgTrajPi�ton
    Dim uneBranche As Branche = uneTravers�e.mBranche

    With dlg
      .chkTravers�eDouble.Enabled = uneBranche.mPassages.Count > 1
      .chkTravers�eDouble.Checked = uneTravers�e.EnDeuxTemps
      .txtLgTravers�e.Text = Format(uneTravers�e.LgMaximum, "#0.00")
      .txtM�diane.Text = Format(uneTravers�e.LgM�diane, "#0.00")
      .lblBranche.Text = uneBranche.NomRue & " Branche " & mesBranches.ID(uneBranche)

      If maVariante.Verrou <> [Global].Verrouillage.G�om�trie Then
        'les caract�ristiques de la trajectoire ne sont plus modifiables
        .chkTravers�eDouble.Enabled = False
        .btnOK.Enabled = False
      End If

      'Saisir les caract�ristiques de la travers�e
      If .ShowDialog(Me) = DialogResult.OK And .Modif Then
        If uneTravers�e.mDouble Then
          maVariante.D�composerTravers�e(uneTravers�e, colObjetsGraphiques)
        Else
          maVariante.Cr�erTravers�e(uneBranche, colObjetsGraphiques)
        End If
        TerminerCommandeTravers�e()
      End If

      .Dispose()
    End With

  End Sub

  '******************************************************************************
  ' Afficher la boite de dialogue trajectoire v�hicules
  '******************************************************************************
  Private Function dialogueTrajV�hicules(ByVal uneTrajectoire As TrajectoireV�hicules) As Trajectoire
    Dim VoieDestination As Voie
    Dim typeCourant As TrajectoireV�hicules.TypeCourantEnum
    Dim Cr�ation As Boolean = IsNothing(uneTrajectoire)
    Dim unCourant As Courant
    Dim OK As Boolean
    Dim ManuelD�Coch� As Boolean

    If Cr�ation Then
      typeCourant = TrajectoireV�hicules.TypeCourantEnum.TypeCourantMixte
      VoieDestination = VoieTraj
    Else
      With uneTrajectoire
        typeCourant = .TypeCourant
        VoieOrigine = .Voie(TrajectoireV�hicules.OrigineDestEnum.Origine)
        VoieDestination = .Voie(TrajectoireV�hicules.OrigineDestEnum.Destination)
      End With
    End If


    unCourant = maVariante.mCourants(VoieOrigine.mBranche, VoieDestination.mBranche)

    If UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires Then
      OK = True

    Else
      Dim dlg As New dlgTrajVeh

      With dlg
        .pnlManuel.Visible = Not Cr�ation

        'Intialiser les champs de la boite de dialogue
        .mTypeCourant = typeCourant
        .mCourant = unCourant
        If Cr�ation Then
          .Cr�ation = True
        Else
          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            .chkManuel.Checked = True
          Else
            .chkManuel.Checked = uneTrajectoire.Manuel
          End If
        End If
        .lblListeAcc�s.Text = "Trajectoire depuis " & VoieOrigine.Libell� & " vers " & VoieDestination.Libell�

        'Les 2 instructions qui suivent sont en attendant de comprendre pourquoi la boite de dialogue efface un bout du dessin en s'affichant
        '    .StartPosition = FormStartPosition.Manual
        '   .Location = New Point(Me.pnlLignesDeFeux.Location.X, 150)

        If maVariante.Verrou <> [Global].Verrouillage.G�om�trie Then
          'les caract�ristiques de la trajectoire ne sont plus modifiables
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

        'Saisir les caract�ristiques de la trajectoire
        Select Case .ShowDialog(Me)
          Case DialogResult.Retry
            'L'utilisateur a demand� � redessiner manuellement : drapeau pour ex�cuter la commande EditerTrajectoire en retour
            uneTrajectoire = Nothing

          Case DialogResult.Cancel

          Case DialogResult.OK
            OK = True
            typeCourant = .mTypeCourant
            unCourant.NatureCourant = .mCourant.NatureCourant
            unCourant.CoefG�ne = .mCourant.CoefG�ne
            ManuelD�Coch� = Not .chkManuel.Checked
        End Select

        .Dispose()
      End With  ' dlg

    End If

    If OK Then

      If Cr�ation Then
        uneTrajectoire = New TrajectoireV�hicules(VoieOrigine, VoieDestination)
        mesTrajectoires.Add(uneTrajectoire)
      End If

      Try
        With uneTrajectoire
          .TypeCourant = typeCourant
          If Cr�ation Then
            .Courant = unCourant
            .LigneFeu = mesLignesFeux.D�terminerLignesFeux(uneTrajectoire)
          End If
          If Not IsNothing(.LigneFeu) Then
            CType(.LigneFeu, LigneFeuV�hicules).D�terminerNatureCourants(mesTrajectoires)
            AfficherLigneDeFeux(.LigneFeu)
          End If

          If UneCommandeGraphique = CommandeGraphique.EditerTrajectoire Then
            Dim i As Short
            ReDim mPoint(mScreen.Length - 3)
            ''Exclure le 1er et le dernier point (extr�mit�s des branches origine et destination, syst�matiquement incorpor�s dans la trajectoire)
            'Modif v13 (11/01/07) : on permet �galement de modifier manuellement les points d'acc�s aux branches
            'ReDim mPoint(mScreen.Length - 1)
            For i = 0 To mPoint.Length - 1
              mPoint(i) = picDessin.PointToClient(mScreen(i + 1))
              'mPoint(i) = picDessin.PointToClient(mScreen(i))
            Next
            uneTrajectoire.AffecterPointsManuels(mPoint)

          ElseIf ManuelD�Coch� And .Manuel Then
            .R�initialiser(ConserverManuel:=False)
          End If

        End With  ' uneTrajectoire

        Modif = True

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
        If Cr�ation Then mesTrajectoires.Remove(uneTrajectoire)
        uneTrajectoire = Nothing
      End Try

    End If

    Return uneTrajectoire

  End Function

  '******************************************************************************
  ' Cr�er un passage pi�ton
  '******************************************************************************
  Private Function Cr�erPassage(ByVal FermerPassage As Boolean) As PolyArc
    Dim OK As Boolean

    'Effacer tous les segments de travail
    EffacerElastiques()

#If DEBUG Then
    Me.Label1.Text = AngleForm�(mPoint(0), mPoint(1), mPoint(2))
#End If

    'Cr�er le polyarc constitu� par les segments
    If AngleForm�(mPoint(0), mPoint(1), mPoint(2)) < 0 Then
      'Saisie faite en sens horaire : r�ordonner dans le sens trigo
      Dim pTemp As Point = mPoint(0)
      mPoint(0) = mPoint(1)
      mPoint(1) = pTemp
      pTemp = mPoint(2)
      mPoint(2) = mPoint(3)
      mPoint(3) = pTemp
    End If

    Dim p As Point = mPoint(0)
    With BrancheLi�e
      If .BordChauss�eProche(p) = Branche.Lat�ralit�.Droite Then
        mPoint(0) = Projection(mPoint(0), .BordChauss�e(Branche.Lat�ralit�.Droite))
        mPoint(1) = Projection(mPoint(1), .BordChauss�e(Branche.Lat�ralit�.Droite))
        If FermerPassage Then
          mPoint(2) = Projection(mPoint(2), .BordChauss�e(Branche.Lat�ralit�.Gauche))
          mPoint(3) = Projection(mPoint(3), .BordChauss�e(Branche.Lat�ralit�.Gauche))
        End If
      Else
        mPoint(0) = Projection(mPoint(0), .BordChauss�e(Branche.Lat�ralit�.Gauche))
        mPoint(1) = Projection(mPoint(1), .BordChauss�e(Branche.Lat�ralit�.Gauche))
        If FermerPassage Then
          mPoint(2) = Projection(mPoint(2), .BordChauss�e(Branche.Lat�ralit�.Droite))
          mPoint(3) = Projection(mPoint(3), .BordChauss�e(Branche.Lat�ralit�.Droite))
        End If
      End If
    End With

    TerminerPassage(New PassagePi�ton(BrancheLi�e, mPoint))

  End Function

  Private Sub TerminerPassage(ByVal unPassage As PassagePi�ton)
    BrancheLi�e.mPassages.Add(unPassage)
    maVariante.mPassagesEnAttente.Add(unPassage)
    Modif = True
    DessinerPassage(unPassage)
    If maVariante.Verrou = [Global].Verrouillage.G�om�trie Then AfficherLigneDeFeux(unPassage.mTravers�e.LigneFeu)

  End Sub

  '***************************************************************************************
  ' DessinerPassage :  dessine le passage pi�ton
  '                    cr�e �galement la travers�e associ�e si la g�om�trie est verrouill�e
  '***************************************************************************************
  Private Function DessinerPassage(ByVal unPassage As PassagePi�ton) As PolyArc
    DessinerPassage = unPassage.Cr�erGraphique(colObjetsGraphiques)
    'Test sur ModeGraphique rajout� en v13(10/01/07) : plantage en mode tableur
    If maVariante.Verrou = [Global].Verrouillage.G�om�trie AndAlso ModeGraphique Then
      Dim Travers�eDouble As Boolean = Not IsNothing(unPassage.mTravers�e) AndAlso unPassage.mTravers�e.mDouble
      If Travers�eDouble Then
        Travers�e = maVariante.Cr�erTravers�e(unPassage.mBranche, colObjetsGraphiques)
      Else
        Travers�e = maVariante.Cr�erTravers�e(unPassage, colObjetsGraphiques)
      End If

      Travers�e.Verrouiller()
    End If

    Redessiner()
  End Function

  Private Function BrancheProche(ByRef pSouris As Point) As Branche
    Dim uneBranche, maBranche As Branche
    Dim distMinPrec As Single = 500
    Dim distMin As Single = 500
    Dim pSourisF As PointF = CvPointF(pSouris)

    'D�terminer l'axe de branche le + proche du point cliqu�
    For Each uneBranche In mesBranches
      If Distance(pSouris, uneBranche.LigneDeSym�trie) = 0 Then
        distMin = 0
      Else
        Dim LigneProjection As Ligne = New Ligne(Projection(pSourisF, uneBranche.LigneDeSym�trie), pSourisF)
        If Not intersect(LigneProjection, uneBranche.LigneDeSym�trie).IsEmpty Then
          distMin = Min(distMin, LigneProjection.Longueur)
        End If
      End If
      If distMin < distMinPrec Then
        maBranche = uneBranche
        distMinPrec = distMin
      End If
    Next

    If distMin < 500 Then
      'Branche trouv�e
      Select Case UneCommandeGraphique
        Case CommandeGraphique.PassagePi�ton
          BordChauss�ePassage = maBranche.BordChauss�eProche(pSouris)
          If BordChauss�ePassage = Branche.Lat�ralit�.Aucune Then maBranche = Nothing
        Case CommandeGraphique.Trajectoire, CommandeGraphique.LigneFeux, CommandeGraphique.AllongerFeu
          VoieTraj = maBranche.VoieProche(pSouris)
      End Select
    Else
      VoieTraj = Nothing
    End If

    Return maBranche

  End Function

  '******************************************************************************
  'Num�ro Dernier point permettant de clore le trac� de la figure
  '******************************************************************************
  Private Function DernierPoint() As Short
    Select Case UneCommandeGraphique
      Case CommandeGraphique.PassagePi�ton
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
  ' Redessiner : Efface l'�cran et redessine tous les objets � dessiner
  '******************************************************************************
  Public Sub Redessiner(Optional ByVal ObjetAS�lectionner As Graphique = Nothing)

    If Not ChargementEnCours Then
      'Si le chargement en cours, il vaut mieux attendre la fin du Form_Load pour redessiner (�v�nement Form_Paint

      Try
        Rafraichir()
        'Remettre les bitmap � Nothing pour forcer la r�association Bitmap/Graphics
        mBitmap = Nothing
        mBitmapA = Nothing
        DrawPicture(picDessin.CreateGraphics)

        objS�lect = ObjetAS�lectionner
        If IsNothing(objS�lect) Then
          savObjS�lect = Nothing
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
            unDXF.Insert.Pr�parerDessin(Nothing).Dessiner(mBufferGraphics, gr)
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
      '  If Not unObjet Is objS�lect Then
      '    DessinerObjet(unObjet, gr)
      '  End If
      'Next

      colObjetsGraphiques.Dessiner(mBufferGraphics, gr)

      '    mdiApplication.Enabled = True
      DessinEnCours = False

    End If

  End Sub

  Public Sub Recr�erGraphique()
    maVariante.Cr�erGraphique(colObjetsGraphiques)
    Recr�erDessinAntagonismes()
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
  ' Redessiner le buffer m�moris� dans la propri�t� Tag du picturebox picture
  ' picture : PictureBox support du graphique
  ' gr : objet Graphics associ� � picture
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
        ' Associer une Image Bitmap tampon � un objet Graphics tampon
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
        G�rerChangementOnglet()
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If
  End Sub

  '******************************************************************************
  ' Gestion du Changement d'onglet principal
  '******************************************************************************
  Private Sub G�rerChangementOnglet()
    Dim nomOnglet As String
    Dim unOnglet As TabPage
    Dim pnlActif As Panel = pnlPalette
    Static OngletActif As TabPage
    Dim IndexOnglet As OngletEnum

    Try

      If IsNothing(OngletActif) Then
        D�finirD�fautLargeurPanels()
        Me.MinimumSize = New Size(lgPanel(0), 0)
      End If

      IndexOnglet = tabOnglet.SelectedIndex
      'If tabOnglet.SelectedIndex = -1 Then IndexOnglet = -1

      unOnglet = tabOnglet.SelectedTab

      If IsNothing(unOnglet) Then
        If maVariante.ModeGraphique Then
          pnlPalette = pnlG�om�trie
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
          Case OngletEnum.G�om�trie
            pnlPalette = pnlG�om�trie
            mAideTopic = [Global].AideEnum.ONGLET_GEOMETRIE

          Case OngletEnum.LignesDeFeux
            pnlPalette = pnlLignesDeFeux
            mAideTopic = [Global].AideEnum.ONGLET_CIRCULATION

          Case OngletEnum.Trafics
            pnlPalette = pnlTrafics
            pnlTrafics.BringToFront()
            If InitTrafics() Then
              'L'utilisateur a fait 'Annuler' sur le nom de la 1ere p�riode de Trafic
              'Ou le sc�nario en cours est sans trafic
              tabOnglet.SelectedTab = OngletActif
            End If
            mAideTopic = [Global].AideEnum.ONGLET_TRAFICS

          Case OngletEnum.Conflits
            pnlPalette = pnlConflits
            InitConflits()
            mAideTopic = [Global].AideEnum.ONGLET_CONFLITS

          Case OngletEnum.PlansDeFeux
            pnlPalette = pnlPlansDeFeux
            If pnlPlansFeuxIndex = -1 Or Me.cboD�coupagePhases.Items.Count = 0 Then
              '1er Appel de l'onglet Plans de feux (ou suite � r�initialisation de ceux-ci par d�verrouillage de la matrice des conflits)
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
              M�moriserCommentaireTrafic()
            End If
          End If

          TopicAideCourant = mAideTopic
        End If

        D�finirSplitPosition()
        pnlPalette.BringToFront()
        pnlPalette.Visible = True
        If pnlPalette Is Me.pnlConflits Then
          Me.Ac1GrilleS�curit�.Left = lgPanel(numPanel) - Me.Ac1GrilleS�curit�.Width - LGMARGE
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

  Private Overloads Function OngletAssoci�(ByVal pnl As Panel) As OngletEnum

    Select Case pnl.Name
      Case Me.pnlG�om�trie.Name
        OngletAssoci� = OngletEnum.G�om�trie
      Case Me.pnlTrafic.Name
        OngletAssoci� = OngletEnum.Trafics
      Case Me.pnlConflits.Name
        OngletAssoci� = OngletEnum.Conflits
      Case Me.pnlPlansDeFeux.Name
        OngletAssoci� = OngletEnum.PlansDeFeux
    End Select
  End Function

  Private Overloads Function OngletAssoci�(ByVal unOnglet As TabPage) As OngletEnum

    Select Case unOnglet.Name
      Case Me.tabG�om�trie.Name
        OngletAssoci� = OngletEnum.G�om�trie
      Case Me.tabTrafics.Name
        OngletAssoci� = OngletEnum.Trafics
      Case Me.tabConflits.Name
        OngletAssoci� = OngletEnum.Conflits
      Case Me.tabPlansDeFeux.Name
        OngletAssoci� = OngletEnum.PlansDeFeux
    End Select
  End Function

  '******************************************************************************
  ' Recr�er le Menu Contextuel � partir du MenuItem unMenu
  '******************************************************************************
  Public Sub Recr�erMenuContextuel(ByVal unMenu As MenuItem)

    picDessin.ContextMenu.MenuItems.Clear()
    picDessin.ContextMenu.MergeMenu(unMenu)

  End Sub
#End Region
#Region " Verrouillages"
  '********************************************************************************************************************
  ' Activation/D�sactivation d'une case de verrouillage de la variante
  '********************************************************************************************************************
  Private Sub chkVerrou_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkVerrouMatrice.CheckedChanged, chkVerrouFeuBase.CheckedChanged, chkVerrouG�om�trie.CheckedChanged, chkVerrouLignesFeux.CheckedChanged
    Dim chk As Windows.Forms.CheckBox = sender
    Dim Index As Verrouillage = chk.Tag
    Dim texte As String = "D�verrouiller "
    Dim TexteCompl�ment As String

    If ChargementEnCours Or ChangementDeSc�nario Then
      Select Case Index
        Case [Global].Verrouillage.G�om�trie
          VerrouillerBoutonsG�om�trie(Verrouillage:=True)
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
        D�marrerCommande(CommandeGraphique.AucuneCommande)
        Dim ContientManuelles = mesTrajectoires.ContientManuelles

        If Not chk.Checked Then
          Select Case Index
            Case [Global].Verrouillage.G�om�trie
              'Les trajectoires et lignes de feux pi�tons sont li�es dir�ectement aux passages pi�tons;
              ' Il ne faut donc d�tecter que la pr�sence de trajectoires et/ou de lignes de feux v�hicules
              If mesTrajectoires.ContientV�hicules Or mesLignesFeux.Count > mesTrajectoires.Count Then
                texte &= "la g�om�trie"
                TexteCompl�ment &= "Lignes de feux et Trajectoires seront r�initialis�es"
                If ModeGraphique And ConflitsPartiellementR�solus Then
                  TexteCompl�ment &= vbCrLf & "Les antagonismes seront r�initialis�s"
                End If

                If Sc�narioEnCours() AndAlso monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteCompl�ment &= vbCrLf & "Tous les plans de feux de fonctionnement vont �tre supprim�s"
                End If
              Else
                texte = ""
              End If

            Case [Global].Verrouillage.LignesFeux
              If Sc�narioEnCours() Then
                texte &= "les lignes de feux"
                If ModeGraphique AndAlso mAntagonismes.ConflitsPartiellementR�solus Then
                  TexteCompl�ment &= "Les antagonismes seront r�initialis�s"
                End If
                If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteCompl�ment &= vbCrLf & "Tous les plans de feux de fonctionnement vont �tre supprim�s"
                ElseIf PhasageRetenu Then
                  TexteCompl�ment &= "L'organisation du phasage sera � refaire"
                End If

              Else
                texte = ""
              End If

            Case [Global].Verrouillage.Matrices
              If PhasageRetenu Then
                texte &= "la matrice"
                If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                  TexteCompl�ment &= "Tous les plans de feux de fonctionnement vont �tre supprim�s"
                Else
                  TexteCompl�ment &= "L'organisation du phasage sera � refaire"
                End If
              Else
                texte = ""
              End If

            Case [Global].Verrouillage.PlanFeuBase
              If monPlanFeuxBase.mPlansFonctionnement.Count > 0 Then
                texte &= "le plan de feux de base"
                TexteCompl�ment &= "Tous les plans de feux de fonctionnement vont �tre supprim�s"
              Else
                texte = ""
              End If
          End Select

        End If

        Try
          Passage = True
          If texte.Length > 0 Then
            texte &= " ?" & vbCrLf & TexteCompl�ment
          End If
          If VerrouillageAccept�(chk, texte) Then
            mdiApplication.AfficherBarreEtat()

            Select Case Index
              Case [Global].Verrouillage.G�om�trie
                VerrouillerG�om�trie(chk.Checked)
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

  Private Overloads Function VerrouillageAccept�() As Boolean
    Dim ObjetM�tier As M�tier

    Try
      ObjetM�tier = maVariante.NonVerrouillable()
    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      ObjetM�tier = maVariante
    End Try

    Return IsNothing(ObjetM�tier)

  End Function

  '***********************************************************************
  ' V�rifier si le verrouillage peut �tre accept�
  '***********************************************************************
  Private Overloads Function VerrouillageAccept�(ByVal chk As CheckBox, ByVal Texte As String) As Boolean
    Dim ObjetM�tier As M�tier

    Try
      If chk.Checked Then
        'NonVerrouillable retourne Nothing s'il n'y a pas de pb pour verrouiller
        ObjetM�tier = maVariante.NonVerrouillable()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      ObjetM�tier = maVariante
    End Try

    If IsNothing(ObjetM�tier) Then
      If chk.Checked Or Texte.Length = 0 Then
        VerrouillageAccept� = True
      Else
        VerrouillageAccept� = Confirmation(Texte, Critique:=True)
      End If

      If VerrouillageAccept� Then
        'Version d�finitive
        maVariante.BasculerVerrou(chk)
      Else
        chk.Checked = Not chk.Checked
      End If

    Else
      chk.Checked = Not chk.Checked
      If ModeGraphique Then
        D�s�lectionner()
        If Not IsNothing(ObjetM�tier.mGraphique.ObjetM�tier) Then
          objS�lect = ObjetM�tier.mGraphique
          S�lD�s�lectionner(PourS�lection:=True)
        End If
      End If
    End If

  End Function

  '************************************************************************************
  ' (D�)Verrouiller les boutons suite au (d�)verrouillage de la g�om�trie
  '************************************************************************************
  Private Sub VerrouillerBoutonsG�om�trie(ByVal Verrouillage As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle

    If ModeGraphique Then
      'Griser/D�griser les colonnes Largeur de voies et nombre de colonnes
      ' On pourrait autoriser les largeurs de voies, mais l'�v�nement AC1GrillesBranches_ValidateEdit _
      ' red�finit l'ilot et supprime les passages pi�tons si celles-ci sont red�finies
      rg = Me.AC1GrilleBranches.GetCellRange(1, 4, mesBranches.Count, 6)

      If Verrouillage Then
        unStyle = StyleGris�
      Else
        unStyle = StyleD�gris�
      End If

    Else
      unStyle = StyleGris�
      rg = Me.AC1GrilleBranches.GetCellRange(1, 5, mesBranches.Count, 5)
    End If

    rg.Style = unStyle

  End Sub

  '*************************************************************************************
  ' Verrouiller la g�om�trie
  'Proc�dure appel� uniquement en mode graphique
  '*************************************************************************************
  Private Sub VerrouillerG�om�trie(ByVal Verrouillage As Boolean)

    Try

      VerrouillerBoutonsG�om�trie(Verrouillage)

      D�s�lectionner()

      If Verrouillage Then
        maVariante.InitialiserCourants()

        'Cr�er les travers�es pi�tonnes � partir des passages pi�tons et les lignes de feux associ�es
        maVariante.InitialiserTravers�es(colObjetsGraphiques)
        mesTrajectoires.Verrouiller()

        'Afficher le tableau des lignes de feux pi�tons
        Dim uneLignePi�tons As LigneFeuPi�tons
        For Each uneLignePi�tons In mesLignesFeux
          'A ce stade d'avancement, il n'y a pas de ligne v�hicules : le cast LigneFeux->LigneFeuPi�tons fonctionne toujours
          Me.AfficherLigneDeFeux(uneLignePi�tons)
        Next

      Else
        'Supprimer toutes les trajectoires et les lignes de feux
        maVariante.SupprimerTrajectoires(colObjetsGraphiques)
        maVariante.Cr�erGraphique(colObjetsGraphiques)

        'Supprimer toutes les lignes sauf les 2 premi�res qui sont incompressibles
        D�calageFeuxEnCours = True  'Drapeau pour d�sactiver l'�v�nement RowColChange
        Me.AC1GrilleFeux.Rows.RemoveRange(1, Me.AC1GrilleFeux.Rows.Count - 2)
        D�calageFeuxEnCours = False
        Me.AC1GrilleFeux.Rows(1).Clear(Grille.ClearFlags.Content)

        'D�verrouiller �galement le verrouillage aval
        Me.chkVerrouLignesFeux.Checked = False
        VerrouillerLignesFeux(Verrouillage)

        'R�initialiser le menu Sc�nario
        AfficherSc�narios()
        Me.cboTrafic.Items.Clear()

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "VerrouillerG�om�trie")

    End Try
  End Sub

  '************************************************************************************
  ' (D�)Verrouiller les boutons suite au (d�)verrouillage des lignes de feux
  '************************************************************************************
  Private Sub VerrouillerBoutonsLignesFeux(ByVal Verrouillage As Boolean)
    Dim rg As Grille.CellRange
    Dim unStyle As Grille.CellStyle

    'On ne peut pas ajouter ni supprimer une ligne de feux apr�s verrouillage
    Me.btnLigneFeux.Enabled = Not Verrouillage
    Me.btnLigneFeuxMoins.Enabled = Not Verrouillage

    'On ne peut pas ajouter ni supprimer une trajectoire apr�s verrouillage
    Me.btnTrajectoire.Enabled = Not Verrouillage
    Me.btnTrajectoireMoins.Enabled = Not Verrouillage

    'On ne peut pas ajouter ni supprimer une travers�e pi�tonne apr�s verrouillage
    Me.btnTravers�e.Enabled = Not Verrouillage
    Me.btnTravers�eMoins.Enabled = Not Verrouillage
    Me.btnTrajToutes.Enabled = Not Verrouillage
    Me.btnTrajMoinsTout.Enabled = Not Verrouillage

    If Verrouillage Then
      unStyle = StyleGris�
    Else
      unStyle = StyleD�gris�
    End If

    If ModeGraphique Then
      'Griser/D�griser la colonne Angle du tableau de branches :les trajectoires peuvent devenir indessinables et surtout en cascade , les positions des antagonismes)
      rg = Me.AC1GrilleBranches.GetCellRange(1, 2, mesBranches.Count, 2)
      rg.Style = unStyle

    Else
      'Griser/D�griser la colonne nb de voies sortantes (utile pour les trafics : sens uniques entrants �ventuels)
      rg = Me.AC1GrilleBranches.GetCellRange(1, 6, mesBranches.Count, 6)
      rg.Style = unStyle

      'Griser/D�griser les colonnes nombre de voies et TAG,TD,TAD  du tableau de LF
      'Ajout AV : 10/08/07 - Point Circulation 33 du document de suivi
      GriserLFTableur(Verrouillage)
      'rg = Me.AC1GrilleFeux.GetCellRange(1, 5, mesLignesFeux.Count, 8)
      'rg.Style = unStyle
    End If

    If Verrouillage Then
      'Dimensionner la largeur de la 1�re colonne selon l'apparition de l'ascenseur 
      '(10 lignes pour le plan de feux de base, 8 pour celui de fonctionnement)
      Me.lvwDur�eVert.Columns(0).Width = IIf(mesLignesFeux.Count > 10, 46, 63)
      Me.lvwDur�eVertFct.Columns(0).Width = IIf(mesLignesFeux.Count > 8, 46, 63)
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
      unStyle = StyleGris�
      rg.Style = unStyle

      unStyle = StyleD�gris�
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        If uneLigneFeux.EstV�hicule Then
          rg = fg.GetCellRange(row, 1, row, 3)
        Else
          rg = fg.GetCellRange(row, 1, row, 2)
        End If
        rg.Style = unStyle
      Next

    Else
      rg = fg.GetCellRange(1, 0, mesLignesFeux.Count, 8)
      unStyle = StyleD�gris�
      rg.Style = unStyle

      unStyle = StyleGris�
      For Each uneLigneFeux In mesLignesFeux
        row = mesLignesFeux.IndexOf(uneLigneFeux) + 1
        If uneLigneFeux.EstPi�ton Then
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
        D�s�lectionner()

      Else
        If Verrouillage Then
          'Supprimer la derni�re ligne : ajout interdit
          Me.AC1GrilleFeux.RemoveItem()
        Else
          'Rajouter une ligne pour cr�ation possible
          Me.AC1GrilleFeux.Rows.Add()
        End If

      End If

      If Verrouillage Then
        ConflitsInitialis�s = False
        InitConflits()

      Else
        'D�verrouiller �galement le verrouillage aval
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

      Me.Ac1GrilleS�curit�.Enabled = Not Verrouillage
      'Remettre � 0 le compteur pour que les Phasages soient reconstruits
      If Verrouillage Then

        If Not ChangementDeSc�nario Then
          'Les plans de phasage viennent d'�tre construits : calculer les capacit�s
          monPlanFeuxBase.CalculerCapacit�sPlansPhasage()
          'Ligne mise en commentaire le 21/03/07(AV) : On ne voit pas � quoi �� sert et c'est plutot pr�judiciable 
          '        ConflitsInitialis�s = False
        End If

      ElseIf Sc�narioEnCours() Then
        'D�verrouiller �galement le verrouillage aval
        If Me.chkVerrouFeuBase.Checked Then
          Me.chkVerrouFeuBase.Checked = False
          VerrouillerPlanFeuxBase(Verrouillage:=False)
          monPlanFeuxBase.mPlansFonctionnement.Clear()
        End If
        If PhasageRetenu Then
          'Phrase mise en commentaire le 06/02/07 en attendant : Nouvelle organisation du phasage(tous les phasages ne sont pas affich�s)
          '   Me.cboD�coupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)
          Me.chkD�coupagePhases.Checked = False
        End If
      End If

      'On ne peut plus modifier le trafic  d�s le verrouillage des conflits
      If Not IsNothing(monTraficActif) Then
        ActiverBoutonsTrafics()
      End If

      '    Me.AC1GrilleAntagonismes.Enabled = Not Verrouillage
      Me.AC1GrilleAntagonismes.Cols(2).AllowEditing = Not Verrouillage
      Me.btnR�initAntago.Enabled = Not Verrouillage

      If ModeGraphique AndAlso cndContexte = [Global].OngletEnum.Conflits Then
        '(D�)Verrouillage op�r� manuellement : les antagonismes sont visibles et cliquables uniquement dans cet onglet
        mAntagonismes.Verrouiller()
        Redessiner()
      End If

      'D�sactiver tous les boutons radios due l'onglet Plans de feux
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
    Me.chkD�coupagePhases.Enabled = Not Verrouillage

    'On ne peut plus modifier  le plan de feux de base
    Me.txtDur�eCycleBase.Enabled = Not Verrouillage
    Me.updPhase1Base.Enabled = Not Verrouillage
    Me.updPhase2Base.Enabled = Not Verrouillage
    Me.updPhase3Base.Enabled = Not Verrouillage
    Me.updD�calageOuvertureBase.ReadOnly = Verrouillage
    Me.updD�calageFermetureBase.ReadOnly = Verrouillage

    If Not Verrouillage Then
      monPlanFeuxBase.mPlansFonctionnement.Clear()
      Me.cboPlansDeFeux.Items.Clear()
    End If

  End Sub
#End Region
#Region " Boutons M�tier"
#Region " G�om�trie"
  Private Sub btnPi�tonPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnPi�tonPlus.Click, btnPi�tonPlusRapide.Click

    If sender Is btnPi�tonPlus Then
      D�marrerCommande(CommandeGraphique.PassagePi�ton)
    Else
      D�marrerCommande(CommandeGraphique.PassagePi�tonRapide)
    End If
  End Sub

  Private Sub btnPi�tonMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPi�tonMoins.Click
    If IsNothing(objS�lect) Then
      D�marrerCommande(CommandeGraphique.SupprimerPassage)
    ElseIf TypeOf objS�lect Is PolyArc Then
      Dim unPolyarc As PolyArc = objS�lect
      If TypeOf unPolyarc.ObjetM�tier Is PassagePi�ton Then
        If Confirmation("Supprimer le passage pi�ton", Critique:=False) Then
          D�marrerCommande(CommandeGraphique.SupprimerPassage)
        End If
      Else
        MessageBox.Show("S�lectionner un passage pi�ton")
        D�marrerCommande(CommandeGraphique.AucuneCommande)
        objS�lect = Nothing
      End If
    End If
  End Sub

#End Region
#Region " Trajectoires"

  '******************************************************************************
  ' Bouton  ouvrant la Boite de dialogue des propri�t�s de la trajectoire
  '******************************************************************************
  Private Sub btnTrajProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnTrajProp.Click

    If IsNothing(objS�lect) Then
      D�marrerCommande(CommandeGraphique.PropTrajectoire)
    ElseIf TypeOf objS�lect Is PolyArc Then
      Dim unPolyarc As PolyArc = objS�lect
      If TypeOf unPolyarc.ObjetM�tier Is TrajectoireV�hicules Then
        If UneCommandeGraphique = CommandeGraphique.PropTrajectoire Then D�marrerCommande(CommandeGraphique.AucuneCommande)
        Dim uneTrajectoire As TrajectoireV�hicules = objS�lect.ObjetM�tier
        If IsNothing(Me.dialogueTrajV�hicules(uneTrajectoire)) Then
          'L'utilisateur a demand� � redessiner manuellement : ex�cuter la commande EditerTrajectoire
          D�finirTrajectoireManuellement()
        Else
          If uneTrajectoire.ARedessiner Then DessinerTrajectoire(uneTrajectoire)
        End If

      Else
        MessageBox.Show("S�lectionner une trajectoire")
      End If
    End If

  End Sub

  '**************************************************************************************
  ' Case � cocher pour afficher/masquer les fl�ches indiquant le sens des trajectoires
  ' Coupl�e  avec l'item correspondant du menu Affichage
  '**************************************************************************************
  Private Sub chkSensTrajectoires_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSensTrajectoires.CheckedChanged
    maVariante.SensTrajectoires = Me.chkSensTrajectoires.Checked
    If Not DiagrammeActif() Then
      If Not ChargementEnCours Then Redessiner()
    End If
  End Sub

  '******************************************************************************
  ' D�finir les points de construction de la trajectoire manuellement
  '******************************************************************************
  Private Sub D�finirTrajectoireManuellement()
    Dim uneTrajectoire As TrajectoireV�hicules = objS�lect.ObjetM�tier
    Dim unePlume As New Pen(Color.Fuchsia)
    Dim LOrigine, LDestination As Ligne

    EffacerObjet(objS�lect)

    With uneTrajectoire
      Dim unPolyarc As PolyArc = CType(objS�lect, PolyArc)
      'Axe de la voie origine
      LOrigine = .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Origine).Clone
      'L'acc�s � la branche origine a pu �tre d�plac�
      LOrigine.pA = .LigneAcc�s.pA
      LOrigine.Plume = unePlume
      DessinerObjet(LOrigine)

      'Axe de la voie destination
      LDestination = .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Destination).Clone
      'L'acc�s � la branche destination a pu �tre d�plac�
      LDestination.pA = .LigneAcc�s.pB
      LDestination.Plume = unePlume
      DessinerObjet(LDestination)
    End With

    ReDim mPoint(0)
    ReDim mScreen(2)
    mPoint(0) = CvPoint(LOrigine.pAF)
    DessinerPoign�e(mPoint(0), True)
    mScreen(0) = picDessin.PointToScreen(mPoint(0))
    mScreen(1) = mScreen(0)
    'mScreen1 repr�sente le point 'mobile' de la souris (pour DessinerElastique)
    mScreen1 = mScreen(1)

    'Pour dessiner la poign�e de l'extr�mit� du segment destination :
    'mPoint1 Servira � rep�rer le point d'arriv�e � atteindre lors du dessin de la trajectoire
    mPoint1 = CvPoint(LDestination.pAF)
    mScreen(2) = picDessin.PointToScreen(mPoint1)
    mScreen2 = mScreen(2)

    UneCommandeGraphique = CommandeGraphique.EditerTrajectoire
    TraiterMessageGlisser()
    mDragging = True
    DessinerElastique()

  End Sub

  '******************************************************************************
  ' Refaire le dessin suite � l'�dition de la trajectoire
  '******************************************************************************
  Private Sub DessinerTrajectoire(ByVal uneTrajectoire As TrajectoireV�hicules)

    uneTrajectoire.Cr�erGraphique(colObjetsGraphiques)
    If Not G�n�rationTrajectoires Then Redessiner()

  End Sub

  '******************************************************************************
  ' Supprimer une trajectoire
  '******************************************************************************
  Private Sub btnTrajMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajectoireMoins.Click

    If IsNothing(objS�lect) Then
      If maVariante.mTrajectoires.Count > 0 Then D�marrerCommande(CommandeGraphique.SupprimerTrajectoire)

    ElseIf TypeOf objS�lect Is PolyArc Then
      Dim unPolyarc As PolyArc = objS�lect
      If TypeOf unPolyarc.ObjetM�tier Is TrajectoireV�hicules Then
        If Confirmation("Supprimer la trajectoire", Critique:=False) Then
          D�marrerCommande(CommandeGraphique.SupprimerTrajectoire)
        End If
      Else
        MessageBox.Show("S�lectionner une trajectoire")
        D�marrerCommande(CommandeGraphique.AucuneCommande)
        objS�lect = Nothing
      End If
    End If

  End Sub

  Private Sub btnTrajPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajectoire.Click

    D�marrerCommande(CommandeGraphique.Trajectoire)
  End Sub

  Private Sub btnTravPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTravers�e.Click

    UneCommandeGraphique = CommandeGraphique.Travers�e
    TraiterMessageGlisser()
  End Sub

  Private Sub btnTravMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTravers�eMoins.Click

    UneCommandeGraphique = CommandeGraphique.D�composerTravers�e
    TraiterMessageGlisser()

  End Sub

  Private Sub btnTravProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnTravProp.Click

    If IsNothing(objS�lect) Then
      D�marrerCommande(CommandeGraphique.PropTravers�e)
    ElseIf TypeOf objS�lect Is PolyArc Then
      Dim unPolyarc As PolyArc = objS�lect
      If TypeOf unPolyarc.ObjetM�tier Is Travers�ePi�tonne Then
        Dim uneTrajectoire As Travers�ePi�tonne = objS�lect.ObjetM�tier
        Me.dialogueTrajPi�tons(uneTrajectoire)
      Else
        MessageBox.Show("S�lectionner une passage pi�ton")
      End If
    End If

  End Sub

  '*****************************************************************************************
  'G�n�rer toutes les trajectoires v�hicules possibles
  ' A l'exception de celles d�j� d�finies
  '*****************************************************************************************
  Private Sub btnTrajToutes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrajToutes.Click
    Dim uneBrancheEntrante, uneBrancheSortante As Branche
    Dim uneTrajectoire As Trajectoire

    D�marrerCommande(CommandeGraphique.AucuneCommande)

    If Confirmation("G�n�rer toutes les trajectoires possibles", Critique:=False, Controle:=Me) Then
      UneCommandeGraphique = CommandeGraphique.ToutesTrajectoires

      G�n�rationTrajectoires = True

      For Each uneBrancheEntrante In mesBranches
        For Each VoieOrigine In uneBrancheEntrante.Voies
          If VoieOrigine.Entrante Then
            'Voie entrante : chercher toutes le voies sortantes possibles
            For Each uneBrancheSortante In mesBranches
              If Not uneBrancheEntrante Is uneBrancheSortante Then
                'Pas de trajectoire avc m�me branche d'entr�e et de sortie
                For Each VoieTraj In uneBrancheSortante.Voies
                  If Not VoieTraj.Entrante Then
                    'Voie sortante
                    If Not mesTrajectoires.Existe(VoieOrigine, VoieTraj) Then
                      'V�rifier que la trajectoire n'existe pas d�j�
                      Cr�erTrajectoire()
                    End If
                  End If
                Next
              End If
            Next
          End If
        Next
      Next

      G�n�rationTrajectoires = False
      Redessiner()
      D�marrerCommande(CommandeGraphique.AucuneCommande)
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
          .Cr�erGraphique(colObjetsGraphiques)
        End With
        AfficherLignesDeFeux()
        Redessiner()
      End If
    End If

  End Sub

#End Region
#Region " Lignes de feux"
  Private Sub btnLigneFeuPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLigneFeux.Click

    D�marrerCommande(CommandeGraphique.LigneFeux)

  End Sub

  '************************************************************************************
  ' Bouton Supprimer une ligne de feux
  '************************************************************************************
  Private Sub btnLigneFeuMoins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLigneFeuxMoins.Click
    Dim PoserQuestion As Boolean
    Dim msg As String

    If ModeGraphique Then
      If IsNothing(objS�lect) Then
        D�marrerCommande(CommandeGraphique.SupprimerLigneFeu)
      ElseIf TypeOf objS�lect Is PolyArc Then
        Dim unPolyarc As PolyArc = objS�lect
        If TypeOf unPolyarc.ObjetM�tier Is LigneFeuV�hicules Then
          PoserQuestion = True
        ElseIf TypeOf unPolyarc.ObjetM�tier Is LigneFeuPi�tons Then
          msg = "S�lectionner une ligne de feux v�hicules"
        Else
          msg = "S�lectionner une ligne de feux"
        End If
      End If

    Else
      Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
      Dim Index As Short = fg.Row - 1
      If Index = mesLignesFeux.Count Then
        'Effacer la ligne de feux encore en cours de cr�ation (mode tableur)
        fg.GetCellRange(Index + 1, 0, Index + 1, fg.Cols.Count - 1).Clear(Grille.ClearFlags.Content)
      Else
        Dim uneLigneFeux As LigneFeux = mesLignesFeux(Index)
        If uneLigneFeux.ToutesVoiesSurBranche Then
          msg = "Branche � sens unique : elle doit comporter au moins une ligne de feux"
        Else
          PoserQuestion = True
        End If
      End If
    End If

    If PoserQuestion Then
      If Confirmation("Supprimer la ligne de feux ?", Critique:=True, Controle:=Me) Then
        SupprimerLigneFeux()
      End If
      D�marrerCommande(CommandeGraphique.AucuneCommande)
    ElseIf Not IsNothing(msg) Then
      MessageBox.Show(msg)
      D�marrerCommande(CommandeGraphique.AucuneCommande)
      objS�lect = Nothing
    End If

  End Sub

  '************************************************************************************
  ' Supprimer la ligne de feux s�lectionn�e
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
        If fg.Row = 1 And fg.Rows.Count = 2 Then 'peut arriver en mode non graphique (voire en mode graphique sans passages pi�tons)
          fg.GetCellRange(1, 0, 1, fg.Cols.Count - 1).Clear(Grille.ClearFlags.Content)
        Else
          ' Dans certains cas (anormaux) RemoveItem d�clenche AfterRowColChange et �� peut planter 
          D�calageFeuxEnCours = True
          'D�s�lectionner la ligne de feux, car �� peut avoir des effets de bord
          fg.Row = -1
          fg.RemoveItem(Index + 1)
          D�calageFeuxEnCours = False
        End If
        ActiverBoutonsLignesFeux()
        If ModeGraphique Then
          UneCommandeGraphique = CommandeGraphique.SupprimerLigneFeu
          SupprimerObjetM�tier()
          UneCommandeGraphique = CommandeGraphique.AucuneCommande

        ElseIf uneLigneFeux.EstV�hicule Then
          With CType(uneLigneFeux, LigneFeuV�hicules)
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
  ' Faire Monter/Descendre la ligne de feux s�lectionn�e (ne touche qu'� l'ordre dans la collection)
  '************************************************************************************************
  Private Sub btnLigneFeuD�caler_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles btnLigneFeuMonter.Click, btnLigneFeuDescendre.Click
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim D�calage As Short, Position As Short

    Try

      'D�terminer le sens du d�calage
      If sender Is btnLigneFeuDescendre Then
        D�calerLigneFeux(+1, fg)
      Else
        D�calerLigneFeux(-1, fg)
      End If
      Me.cboTriLignesFeux.SelectedIndex = 0

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub D�calerLigneFeux(ByVal D�calage As Short, ByVal fg As GrilleDiagfeux)
    Dim Index As Short = fg.Row - 1
    Dim Position As Short
    Dim Donn�es As String = fg.TouteLaLigne.Clip

    Try

      'D�caler la ligne de feux
      Dim uneLigneFeux As LigneFeux = mesLignesFeux(Index)
      mesLignesFeux.D�caler(D�calage, mesLignesFeux(Index))

      AfficherCons�quencesModifLignesDeFeux()

      'R�percuter dans la grille
      With fg

        Position = .Row + D�calage

        D�calageFeuxEnCours = True
        .RemoveItem(.Row)
        .AddItem(Donn�es, Position)
        GriserLignePi�tons(fg, Position, uneLigneFeux.EstPi�ton)
        D�calageFeuxEnCours = False

        'D�selectionner la ligne
        .D�s�lectionner()
        'Res�lectionner la ligne qui vient d'�tre d�cal�e
        fg.Row = Position
        fg.Select(fg.GetCellRange(Position, 0, Position, fg.Cols.Count - 1))
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "D�calerLigneFeux")
    End Try

  End Sub

  '**********************************************************************************************
  ' R�percuter les modifications de lignes de feux dans les tableaux connexes :
  '   - Nom de la ligne
  '  - D�calage d'une ligne (SuiteAD�calage =true)
  '  - Changement de l'ordre de classement (SuiteAD�calage =true)
  '**********************************************************************************************
  Private Sub AfficherCons�quencesModifLignesDeFeux(Optional ByVal SuiteAD�calage As Boolean = True)

    If maVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
      'R�afficher en cons�quence l'ordonnancement des matrices de s�curit�
      If ConflitsInitialis�s Then
        AfficherEnteteMatriceS�curit�()
        If ModeGraphique Then R�afficherLibell�sAntagonismes()
        If SuiteAD�calage Then
          If Me.radMatriceConflits.Checked Then
            AfficherMatriceS�curit�(0)
          ElseIf Me.radMatriceRougesD�gagement.Checked Then
            AfficherMatriceS�curit�(1)
          ElseIf Me.radMatriceInterverts.Checked Then
            AfficherMatriceS�curit�(2)
          End If
        End If
      End If

      If Sc�narioEnCours() Then
        AfficherEntetePhasage()
        If monPlanFeuxBase.PhasageInitialis� Then
          If SuiteAD�calage Then AfficherPhasage(IndexPhasages(cboD�coupagePhases.SelectedIndex))
        End If
      End If

      maVariante.R�ordonnerPlansFeux()
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
      AfficherCons�quencesModifLignesDeFeux()
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
  ' Proc�dures issues de dlgTrafic
  '==============================================================================================
  Private Sub txtCommentaireP�riode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommentaireP�riode.Validated
    M�moriserCommentaireTrafic()
  End Sub

  Private Sub txtCommentaireP�riode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommentaireP�riode.TextChanged
    monTraficActif.Commentaires = Me.txtCommentaireP�riode.Text.Trim

  End Sub
  '*******************************************************************************************************
  'M�moriser le commentaire sur la p�riode de trafic avant de changer d'onglet ou de p�riode
  '********************************************************************************************************
  Private Sub M�moriserCommentaireTrafic()
    'Dim chaine As String = Me.txtCommentaireP�riode.Text.Trim
    'If Not IsNothing(monTraficActif) AndAlso Not ChargementEnCours Then
    '  monTraficActif.Commentaires = Me.txtCommentaireP�riode.Text.Trim
    'End If
  End Sub

  Private Sub chkModeTrafic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkModeTrafic.CheckedChanged
    Static QuestionPos�e As Boolean
    Dim unTrafic As Trafic = mesTrafics(cboTrafic.Text)

    If QuestionPos�e Then
      QuestionPos�e = False
    ElseIf unTrafic Is monTraficActif() Then
      If monTraficActif.ChangeModeSaisieAccept�(chkModeTrafic.Checked) Then
        Me.pnlTrafic.Visible = Not chkModeTrafic.Checked
      Else
        QuestionPos�e = True
        chkModeTrafic.Checked = Not chkModeTrafic.Checked
      End If
    Else
      Me.pnlTrafic.Visible = Not chkModeTrafic.Checked
    End If

  End Sub

  '*********************************************************************************************
  ' Choix du  type de trafic � afficher : VL, PL 2R ou UVP
  '*********************************************************************************************
  Private Sub radVehicule_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radPL.CheckedChanged, radVL.CheckedChanged, rad2Roues.CheckedChanged, radUVP.CheckedChanged

    If Not IsNothing(maVariante) Then
      Try
        AfficherTrafic(AvecLesPi�tons:=False)
      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If

  End Sub

  Private Sub btnNouveauTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNouveauTrafic.Click
    Dim nomSc�nario As String = InputBox("Nom de la p�riode de trafic � cr�er")
    Dim unSc�nario As PlanFeuxBase = monPlanFeuxBase()
    Dim unTrafic As Trafic

    With maVariante
      If nomSc�nario.Length = 0 Then
      ElseIf Not IsNothing(unSc�nario) AndAlso String.Compare(nomSc�nario, unSc�nario.Nom, ignoreCase:=True) = 0 Then
      ElseIf maVariante.mPlansFeuxBase.Contains(nomSc�nario) Then
        MessageBox.Show("Un sc�nario de m�me nom existe d�j�")
      Else

        .Cr�erSc�nario(nomSc�nario, AvecTrafic:=True)
        NouveauSc�nario()
      End If
    End With

  End Sub

  Private Sub btnRenommerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenommerTrafic.Click
    RenommerSc�nario()
  End Sub

  Private Sub btnDupliquerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDupliquerTrafic.Click
    DupliquerSc�nario()
  End Sub

  Private Sub btnSupprimerTrafic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSupprimerTrafic.Click
    SupprimerSc�nario()
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
  ' Choix d'une nouvelle p�riode de trafic dans la liste
  '******************************************************************************
  Private Sub cboTrafic_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles cboTrafic.SelectedIndexChanged
    Dim Index As Short = cboTrafic.SelectedIndex

    M�moriserCommentaireTrafic()

    Try
      If Index <> -1 Then

        mdiApplication.cboSc�nario.Text = Me.cboTrafic.Text
        Me.chkVerrouP�riode.Checked = monTraficActif.Verrouill�

        Me.chkModeTrafic.Checked = monTraficActif.UVP

        AfficherTrafic(AvecLesPi�tons:=True)

      Else
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  '******************************************************************************
  ' Afficher les donn�es du trafic s�lectionn�
  ' AvecLesPi�tons : si vrai, il faut aussi afficher les donn�es du trafic pi�ton
  '							sinon, c'est une simple bascule entre les cat�gories de v�hicules
  '******************************************************************************
  Private Sub AfficherTrafic(ByVal AvecLesPi�tons As Boolean)
    Dim Index As Trafic.TraficEnum
    Dim i, j As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrilleTraficV�hicules
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
                fg(i, j) = .QV�hicule(Index, i - 1, j - 1)
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

        If AvecLesPi�tons Then
          fg = Me.Ac1GrilleTraficPi�tons
          For i = 1 To mesBranches.Count
            fg(1, i - 1) = .QPi�ton(i - 1)
          Next
        End If

        Me.txtCommentaireP�riode.Text = .Commentaires
      End With

      AfficherTraficSatur�()

      ActiverBoutonsTrafics()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherTrafic")

    End Try

  End Sub

  Private Sub ActiverBoutonsTrafics()
    Me.btnSupprimerTrafic.Enabled = cboTrafic.Items.Count > 0
    Me.chkVerrouP�riode.Enabled = monPlanFeuxBase.Verrou < [Global].Verrouillage.Matrices

    Me.btnRenommerTrafic.Enabled = cboTrafic.Items.Count > 0

    ControlerAffichageTrafic()
  End Sub

  Private Sub ControlerAffichageTrafic()

    Dim Activ� As Boolean = Not monTraficActif.Verrouill�
    Me.Ac1GrilleTraficPi�tons.Enabled = Activ�
    Me.AC1GrilleTraficV�hicules.Enabled = Activ�
    chkModeTrafic.Enabled = Activ�

  End Sub

  '******************************************************************************
  '	IndexTrafic : retourne l'index de la cat�gorie de v�hicules(VL - PL- 2 ROues)
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

  Private Sub chkVerrouP�riode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVerrouP�riode.CheckedChanged

    If Not ChangementDeSc�nario Then
      Try
        If Me.chkVerrouP�riode.Checked And monPlanFeuxBase.Trafic.QTotal(Trafic.TraficEnum.UVP) = 0 Then
          MessageBox.Show("Saisir d'abord les trafics")
          Me.chkVerrouP�riode.Checked = False

        Else
          If V�rifierAntagonismesSaisis() Then
            monTraficActif.Verrouill� = chkVerrouP�riode.Checked
            ActiverBoutonsTrafics()

          Else
            'Pour que l'�v�nement d�clench� ensuite ne fasse rien :
            'ChangementDeSc�nario = True
            Me.chkVerrouP�riode.Checked = True
            'ChangementDeSc�nario = False
          End If
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If
  End Sub

  Private Function V�rifierAntagonismesSaisis() As Boolean

    If ModeGraphique Then
      Try

        'If maVariante.Verrou >= Global.Verrouillage.LignesFeux And Not monTraficActif.Verrouill� Then
        'Correction AV : 26/03/07
        If maVariante.Verrou >= [Global].Verrouillage.LignesFeux And monTraficActif.Verrouill� And Not Me.chkVerrouP�riode.Checked Then
          If monPlanFeuxBase.Antagonismes.ConflitsPartiellementR�solus AndAlso Not ChargementEnCours Then
            If Confirmation("R�initialiser les antagonismes", Critique:=False) Then
              R�initialiserAntagonismes()
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
        LancerDiagfeuxException(ex, "V�rifierAntagonismesSaisis")

      End Try

    Else
      'Sans objet pour le mode tableur
      Return True
    End If

  End Function

#End Region
#Region " Matrices de s�curit�"

  '**********************************************************************************************************************
  'Changement d'item dans le Panel Matrices de s�curit�
  '**********************************************************************************************************************
  Private Sub radMatriceConflits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radMatriceConflits.CheckedChanged, radMatriceRougesD�gagement.CheckedChanged, radMatriceInterverts.CheckedChanged

    Dim Index As Short
    If radMatriceConflits.Checked Then
      Index = 0
    ElseIf radMatriceRougesD�gagement.Checked Then
      Index = 1
    ElseIf Me.radMatriceInterverts.Checked Then
      Index = 2
    Else
      Index = -1
    End If

    If ModeGraphique Then Me.pnlAntagonismes.Visible = (Index = 0 And mAntagonismes.NonTousSyst�matiques)

    Dim MatriceNonVerrouill�e As Boolean = maVariante.Verrou < Verrouillage.Matrices

    Try

      With Me.Ac1GrilleS�curit�

        Select Case Index
          Case -1
            'Par d�faut : Matrice des conflits
            Me.radMatriceConflits.Checked = True

          Case 0
            'Matrice des conflits
            .Enabled = MatriceNonVerrouill�e
            chkVerrouMatrice.Enabled = True

          Case Else
            If MatriceNonVerrouill�e Then
              AfficherMessageErreur(Me, "Verrouiller d'abord la matrice des conflits")
              Me.radMatriceConflits.Checked = True
              Index = -1
            Else
              chkVerrouMatrice.Enabled = False
              If Index = 1 Then
                'Il est possible de modifier les rouges de d�gagement jusqu'� ce qu'on ait choisi un plan de feux de base
                .Enabled = Not PhasageRetenu
              Else
                .Enabled = False
              End If
            End If
        End Select
      End With

      If Index <> -1 Then
        Me.pnlVerrouMatrice.Visible = (Index = 0)
        Me.pnlBoutonsRouges.Visible = (Index = 1 And Ac1GrilleS�curit�.Enabled And ModeGraphique)
        AfficherMatriceS�curit�(Index)
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '***********************************************************************************
  ' Retourne l'index du bouton radio s�lectionn� dans l'onglet Conflits
  '***********************************************************************************
  Private Property pnlConflitsIndex() As Short
    Get
      If Me.radMatriceConflits.Checked Then
        Return 0
      ElseIf Me.radMatriceRougesD�gagement.Checked Then
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
          Me.radMatriceRougesD�gagement.Checked = True
        Case 2
          Me.radMatriceInterverts.Checked = True
        Case -1
          Me.radMatriceConflits.Checked = False
          Me.radMatriceRougesD�gagement.Checked = False
          Me.radMatriceInterverts.Checked = False
          monPlanPourPhasage = Nothing
      End Select
    End Set
  End Property

  '**********************************************************************************************************************
  'Changement d'item dans le Panel Matrices de s�curit�
  '**********************************************************************************************************************

  '**********************************************************************************************************************
  'Affichage de la matrice de s�curit� correspondant � l'index choisi
  '**********************************************************************************************************************
  Private Sub AfficherMatriceS�curit�(ByVal Index As Short)
    Dim lHorizontale, lVerticale As LigneFeux
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleS�curit�
    Dim rg As Grille.CellRange
    Dim row, col As Short

    Try

      'Remettre � blanc les donn�es de la grille(sans les entete)
      rg = fg.PlageDonn�es
      rg.Clear(Grille.ClearFlags.Content)

      For Each lHorizontale In mesLignesFeux
        row = mesLignesFeux.IndexOf(lHorizontale) + 1
        For Each lVerticale In mesLignesFeux
          col = mesLignesFeux.IndexOf(lVerticale) + 1
          rg = fg.GetCellRange(row, col)
          ' lHorizontale d�signe la ligne de feux horizontale
          ' lVerticale d�signe la ligne de feux verticale
          If lHorizontale.EstTrivialementCompatible(lVerticale) Then
            rg.Style = StyleGris�
          ElseIf mLignesFeux.EstIncompatible(lHorizontale, lVerticale) Then
            Select Case Index
              Case 0         ' Matrice des conflits
                rg.Style = StyleRouge
              Case 1         ' rouges de d�gagement
                AfficherRouge(lHorizontale, lVerticale, rg, fg)
              Case 2         ' interverts
                rg.Style = StyleGris�Gras          ' .Styles(Grille.CellStyleEnum.Normal)
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

      'Annuler la s�lection
      fg.D�s�lectionner()

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherMatriceS�curit�")
    End Try

  End Sub

  '**********************************************************************************************************************
  ' R�tablir l'ensemble des valeurs par d�faut (celles calcul�es par  DIAGFEUX) des rouges de d�gagement
  '**********************************************************************************************************************
  Private Sub btnRougesD�faut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRougesD�faut.Click
    Dim lh, lv As LigneFeux

    For Each lh In mLignesFeux()
      For Each lv In mLignesFeux()
        If Not lh Is lv Then
          If Not lh.EstTrivialementCompatible(lv) Then
            'La valeur par d�faut du rouge de d�gagement du plan de feux de base 
            'est celui calcul� comme rouge mini pour les lignes de feux de la variante (cf D�terminerTempsD�gagement)
            mLignesFeux.TempsD�gagement(lh, lv) = mesLignesFeux.RougeD�gagement(lh, lv)
          End If
        End If
      Next
    Next

    AfficherMatriceS�curit�(1)

  End Sub

  '**********************************************************************************************************************
  ' R�tablir la valeur par d�faut (celle calcul�e par  DIAGFEUX) du rouge de d�gagement
  '**********************************************************************************************************************
  Private Sub btnRougeD�faut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRougeD�faut.Click
    Dim fg As GrilleDiagfeux = Me.Ac1GrilleS�curit�
    Dim rg As Grille.CellRange
    Dim row, col As Short
    row = fg.Row
    col = fg.Col
    If row = -1 Then
      MessageBox.Show("S�lectionner d'abord la valeur � restaurer")

    Else
      rg = fg.GetCellRange(row, col)
      Dim lh, lv As LigneFeux
      ' lh d�signe la ligne de feux horizontale (au sens matriciel)
      ' lv d�signe la ligne de feux verticale
      lh = mLignesFeux(CType(row - 1, Short))
      lv = mLignesFeux(CType(col - 1, Short))
      If Not lh.EstTrivialementCompatible(lv) Then

        'La valeur par d�faut du rouge de d�gagement du plan de feux de base 
        'est celui calcul� comme rouge mini pour les lignes de feux de la variante (cf D�terminerTempsD�gagement)
        mLignesFeux.TempsD�gagement(lh, lv) = mesLignesFeux.RougeD�gagement(lh, lv)
        AfficherRouge(lh, lv, rg, fg)
        ActiverBoutonsRouges()
      End If

    End If
  End Sub

  '**********************************************************************************************
  ' Mettre en orang� dans la matrice les lignes de feux correspondant � un antagonisme non r�solu
  '**********************************************************************************************
  Private Sub AfficherAntagosDansMatrice(ByVal fg As GrilleDiagfeux)
    Dim rg As Grille.CellRange
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In mAntagonismes()
      With unAntagonisme
        If .M�mesCourants Is unAntagonisme Then
          ' Mettre en orang� les cases correspondant � des antagonismes encore sans d�cision
          If .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
            Dim l1 As LigneFeux = .LigneFeu(Antagonisme.PositionEnum.Premier)
            Dim l2 As LigneFeux = .LigneFeu(Antagonisme.PositionEnum.Dernier)
            With mLignesFeux()
              If Not .EstIncompatible(l1, l2) Then
                'Sinon la case est d�j� en rouge pour un conflit syst�matique : on la laisse en rouge
                rg = fg.GetCellRange(.IndexOf(l1) + 1, .IndexOf(l2) + 1)
                rg.Style = StyleOrang�
                rg = fg.GetCellRange(.IndexOf(l2) + 1, .IndexOf(l1) + 1)
                rg.Style = StyleOrang�
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
          If .TypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique AndAlso .M�mesCourants Is unAntagonisme Then
            'Masquer les objets graphiques repr�sentant les points de conflit  dont le 1er courant n'a pas pour origine la branche s�lectionn�e
            unAntagonisme.Verrouiller()
            '(D�)Masquer �galement la ligne d'antagonismes 
            Dim row As Short = mAntagonismes.IndexOf(unAntagonisme) + 1
            If IsNothing(uneBranche) Then
              fgAntago.Rows(row).Visible = True
            Else
              fgAntago.Rows(row).Visible = unAntagonisme.BrancheCourant1 Is uneBranche
            End If

            If AntagonismesEnCours Then
              rg = fgAntago.GetCellRange(row, 2)
              D�finirStyle(unAntagonisme, rg)
            End If
          End If
        End With
      Next

      If ConflitsInitialis�s Then
        Redessiner()
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherAntagonismes")
    End Try


  End Sub

  Private Sub R�afficherAntagonismes()

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

  Private Property ConflitsInitialis�s() As Boolean
    Get
      Return maVariante.ConflitsInitialis�s
    End Get
    Set(ByVal Value As Boolean)
      maVariante.ConflitsInitialis�s = Value
    End Set
  End Property

  Private ReadOnly Property ConflitsPartiellementR�solus()
    Get
      Dim unSc�nario As PlanFeuxBase
      For Each unSc�nario In maVariante.mPlansFeuxBase
        If unSc�nario.Antagonismes.ConflitsPartiellementR�solus Then
          Return True
        End If
      Next
    End Get
  End Property

  Private Function AntagonismeLi�Refus�(ByVal unAntagonisme As Antagonisme, ByVal Admis As Boolean, Optional ByVal AppelDepuisGrille As Boolean = False) As Boolean
    Dim fg As GrilleDiagfeux = Me.AC1GrilleAntagonismes

    Try

      With unAntagonisme
        If Admis AndAlso .FilsNonAdmis(mAntagonismes) Then
          If AppelDepuisGrille AndAlso .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
          Else
            MessageBox.Show("Ce conflit avec les pi�tons ne peut �tre admis car le conflit avec le courant v�hicule adverse ne l'est pas")
          End If
          AntagonismeLi�Refus� = True
        End If
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AntagonismeLi�Refus�")
    End Try

  End Function

  Private Sub MettreAJourConflit(ByVal unAntagonisme As Antagonisme, Optional ByVal TypeConflit As Trajectoire.TypeConflitEnum = Trajectoire.TypeConflitEnum.Syst�matique)

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

      Me.cboD�coupagePhases.Items.Clear()

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

              Select Case mFiltrePhasage.AvecPhaseSp�ciale
                Case FiltrePhasage.PhaseSp�cialeEnum.Inclure
                  Ajouter = Ajouter And True
                Case FiltrePhasage.PhaseSp�cialeEnum.Exclure
                  Ajouter = Ajouter And Not .mAvecPhaseSp�ciale
                Case FiltrePhasage.PhaseSp�cialeEnum.Uniquement
                  Ajouter = Ajouter And .mAvecPhaseSp�ciale
              End Select

            Else
              'Feu 3 phases rejet� car case � cocher 3 phases d�coch�e
              Ajouter = False
            End If

          Else
            ' Toujours accepter 2 phases
            Ajouter = True
            'Sauf si des restrictions suppl�mentaires ont �t� apport�es
            If mFiltrePhasage.LigneFeuxMultiPhases = FiltrePhasage.LFMultiphasesEnum.Uniquement Then
              Ajouter = False
            End If
            If mFiltrePhasage.AvecPhaseSp�ciale = FiltrePhasage.PhaseSp�cialeEnum.Uniquement Then
              Ajouter = False
            End If
          End If

          Select Case mFiltrePhasage.Crit�reCapacit�
            Case FiltrePhasage.Capacit�Enum.MoinsDix
              Ajouter = Ajouter And .R�serveCapacit�PourCent < 10
            Case FiltrePhasage.Capacit�Enum.DixVingt
              Ajouter = Ajouter And .R�serveCapacit�PourCent >= 10 And .R�serveCapacit�PourCent < 20
            Case FiltrePhasage.Capacit�Enum.PlusVingt
              Ajouter = Ajouter And .R�serveCapacit�PourCent >= 20
          End Select

        End With

        If Ajouter Then
          With Me.cboD�coupagePhases
            Index = mesPlansPourPhasage.IndexOf(unPlanFeux)
            IndexCombo = .Items.Count
            .Items.Add("Phasage " & CStr(Index + 1))
            IndexPhasages(IndexCombo) = Index
            'M�moriser l'index du plan en cours d'affichage
            If monPlanPourPhasage Is unPlanFeux Then
              IndexRetenu = IndexCombo
            End If
            'M�moriser l'index du plan de feux de base
            If unPlanFeux.PlanBaseAssoci� Is monPlanFeuxBase Then
              IndexRetenuSecondaire = IndexCombo
            End If

          End With
        End If
      Next

      If IndexRetenu <> -1 Then
        'R�afficher le phasage en cours
        Me.cboD�coupagePhases.SelectedIndex = IndexRetenu
      ElseIf IndexRetenuSecondaire <> -1 Then
        'Afficher le phasage du plan de feux de base
        Me.cboD�coupagePhases.SelectedIndex = IndexRetenuSecondaire
      ElseIf Me.cboD�coupagePhases.Items.Count > 0 Then
        'Afficher le 1er phasage
        Me.cboD�coupagePhases.SelectedIndex = 0
      Else
        'Aucun ne plan ne convient aux crit�res : Masquer la grille
        ActiverAspectPhases(Affich�:=False)
        monPlanPourPhasage = Nothing
      End If

      ActiverChoixD�coupage()
      Me.lblD�coupagePhases.Text = Me.cboD�coupagePhases.Items.Count & " phasages / " & mesPlansPourPhasage.Count & " possibles"

    Catch ex As DiagFeux.Exception
      AfficherMessageErreur(Me, ex)

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub ActiverChoixD�coupage()
    If IsNothing(monPlanPourPhasage) Then
      'La combinaison des choix a conduit � n'avoir aucune organisation de propos�e
      Me.chkD�coupagePhases.Enabled = Me.cboD�coupagePhases.Items.Count > 0

    Else
      If PhasageRetenu Then
        Me.chkD�coupagePhases.Enabled = Me.cboD�coupagePhases.Items.Count > 0 And monPlanPourPhasage Is monPlanFeuxBase.PlanPhasageAssoci� AndAlso maVariante.Verrou < [Global].Verrouillage.PlanFeuBase
      Else
        Me.chkD�coupagePhases.Enabled = Not monPlanPourPhasage.PhasageIncorrect
      End If
    End If

  End Sub

  Private Sub chk3Phases_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk3Phases.CheckedChanged

    If chk3Phases.Checked Then
      Me.cbolLFMultiPhases.Enabled = True
      Me.cboPhasesSp�ciales.Enabled = True
    Else
      Me.cbolLFMultiPhases.Enabled = False
      Me.cboPhasesSp�ciales.Enabled = False
    End If

    AfficherComboPhasage()
  End Sub

  Private Sub cbolLFMultiPhases_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolLFMultiPhases.SelectedIndexChanged
    mFiltrePhasage.LigneFeuxMultiPhases = Me.cbolLFMultiPhases.SelectedIndex
    AfficherComboPhasage()
  End Sub

  Private Sub cboPhasesSp�ciales_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPhasesSp�ciales.SelectedIndexChanged
    mFiltrePhasage.AvecPhaseSp�ciale = Me.cboPhasesSp�ciales.SelectedIndex
    AfficherComboPhasage()
  End Sub

  Private Sub cboCapacit�_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboR�serveCapacit�.SelectedIndexChanged
    mFiltrePhasage.Crit�reCapacit� = cboR�serveCapacit�.SelectedIndex
    AfficherComboPhasage()
  End Sub

  '**********************************************************************************************************************
  ' Choix d'une autre organisation de phasage dans la liste d�roulante
  '**********************************************************************************************************************
  Private Sub cboD�coupage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles cboD�coupagePhases.SelectedIndexChanged

    If PhasageModifi�(monPlanPourPhasage) Then
      ComposerPhasage()
    End If

    AfficherPhasage(IndexPhasages(cboD�coupagePhases.SelectedIndex))

  End Sub

  '**********************************************************************************************************************
  ' Afficher dans l'organisation du phasage celui correspondant � l'index choisi
  '**********************************************************************************************************************
  Private Sub AfficherPhasage(ByVal Index As Short)
    Dim unePhase As Phase
    Dim row, col As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim rg As Grille.CellRange
    Dim uneLigneFeux As LigneFeux

    Try
      'D�savtiver l' �v�nement AC1GrillePhases.CellChanged
      AffichagePhasesEnCours = True

      'D�terminer le plan de feu de base associ�
      monPlanPourPhasage = mesPlansPourPhasage(Index)

      With monPlanPourPhasage
        Me.txtR�serveCapacit�PourCent.Text = .strR�serveCapacit�PourCent

        'Affichage contextuel selon le nombre de phases
        ActiverAspectPhases(CType(.mPhases.Count, Short))
        'Remettre � blanc toutes les cellules
        For col = 1 To fg.Cols.Count - 1    ' .mPhases.Count
          For row = 1 To mesLignesFeux.Count
            rg = fg.GetCellRange(row, col)
            rg.Checkbox = Grille.CheckEnum.Unchecked
            rg.Style = StyleD�gris�
          Next
        Next

        'Cocher les cases ad��quates pour les lignes de feux de chaque phase
        col = 0
        For Each unePhase In .mPhases
          col += 1
          For Each uneLigneFeux In unePhase.mLignesFeux
            rg = fg.GetCellRange(mesLignesFeux.IndexOf(uneLigneFeux) + 1, col)
            rg.Checkbox = Grille.CheckEnum.Checked
          Next
        Next

        For col = 1 To .mPhases.Count
          TraiterOrang�(col)
        Next
        Me.D�terminerPhasageCorrect()
        Me.chkD�coupagePhases.Checked = monPlanPourPhasage.PlanBaseAssoci� Is monPlanFeuxBase
      End With

      'R�activer l'�v�nement AC1GrillePhases.CelllChanged
      AffichagePhasesEnCours = False

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherPhasage")
    End Try

  End Sub

  '**********************************************************************************************************************
  ' D�termine si l'organisation du phasage a �t� modifi�
  '**********************************************************************************************************************
  Private Function PhasageModifi�(ByVal unPlanFeu As PlanFeuxPhasage) As Boolean
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    If IsNothing(unPlanFeu) Then
      Return False
      'D�terminer si le nombre de phases est pass� de 2 � 3 ou inversement
    ElseIf (unPlanFeu.mPhases.Count = MAXPHASES) Xor (fg.Cols(MAXPHASES).Visible) Then
      Return True

    Else
      Dim row, col As Short
      Dim rg As Grille.CellRange
      Dim uneLigneFeux As LigneFeux
      Dim unePhase As Phase
      Dim MaxCol As Short = Math.Min(fg.Cols.Count - 1, unPlanFeu.mPhases.Count)
      'D�terminer si une ligne de feux a chang� de phase
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
  'D�terminer si le phasage affich� est correct
  'Il ne l'est pas si 2 lignes de feux incompatibles sont pr�sentes dans la m�me colonne(phase)
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
      'Cr�er une phase
      unePhase = New Phase
      For Each uneLigneFeux In mesLignesFeux
        'Ajouter les lignes de feux � la phase
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
  'Retourne le num�ro maxi de la colonne lors de l'organisation du phasage
  '		2 ou 3(MAXPHASAGES) selon que la derni�re colonne soit visible ou non
  '**********************************************************************************************************************
  Private Function MaxColPhasage() As Short
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    MaxColPhasage = IIf(fg.Cols(MAXPHASES).Visible, MAXPHASES, MAXPHASES - 1)
  End Function

  '**********************************************************************************************************************
  'D�terminer si le phasage affich� est correct
  'Il ne l'est pas si 2 lignes de feux incompatibles sont pr�sentes dans la m�me colonne(phase)
  'Il ne l'est pas s'il manque une ligne de feux dans l'ensemble des phases 
  '**********************************************************************************************************************
  Private Sub D�terminerPhasageCorrect()
    'Pr�sence des lignes de feux dans les colonnes(phases)
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
          If unStyle.Name = StyleOrang�.Name Then
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

      D�terminerAfficherCapacit�(monPlanPourPhasage)

      ActiverChoixD�coupage()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "D�terminerPhasageCorrect")
    End Try

  End Sub

  '**********************************************************************************************************************
  'D�terminer les cellules � mettre en orang� si sont activ�es des lignes de feux incompatibles
  '**********************************************************************************************************************
  Private Sub TraiterOrang�(ByVal col As Short)
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases

    Dim rg, rg1, rg2 As Grille.CellRange
    Dim row, row2 As Short
    Dim uneLigneFeux1, uneLigneFeux2 As LigneFeux
    Dim unStyle As Grille.CellStyle
    Dim Activ� As Boolean

    'D�griser toute la colonne
    rg = fg.TouteLaColonne(col)
    rg.Style = StyleD�gris�

    'Parcourir tous les couples de lignes de feux
    For row = 1 To fg.Rows.Count - 1
      rg1 = fg.GetCellRange(row, col)
      If rg1.Checkbox = Grille.CheckEnum.Checked Then
        For row2 = row + 1 To fg.Rows.Count - 1
          uneLigneFeux1 = mesLignesFeux(CType(row - 1, Short))
          uneLigneFeux2 = mesLignesFeux(CType(row2 - 1, Short))
          rg2 = fg.GetCellRange(row2, col)
          Activ� = (rg2.Checkbox = Grille.CheckEnum.Checked)
          If mLignesFeux.EstIncompatible(uneLigneFeux1, uneLigneFeux2) And Activ� Then
            rg1.Style = StyleOrang�
            rg2.Style = StyleOrang�
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
        'Supprimer la phase s�lectionn�e
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
            rg.Style = StyleD�gris�
            rg.Checkbox = Grille.CheckEnum.Unchecked
          Next
          TraiterOrang�(1)
          TraiterOrang�(2)

          D�terminerPhasageCorrect()
          AffichagePhasesEnCours = False
          ActiverAspectPhases(CType(MAXPHASES - 1, Short))
        Else
          MessageBox.Show(Me, "S�lectionner d'abord la phase � supprimer", NomProduit, MessageBoxButtons.OK)
        End If

      Else
        'Rajouter une 3�me phase
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

    ActiverAspectPhases(Affich�:=True)

    If nbPhases = MAXPHASES Then
      fg.Cols(MAXPHASES).Visible = True
      Me.btnActionPhase.Text = "Supprimer la phase"
    Else
      fg.Cols(MAXPHASES).Visible = False
      Me.btnActionPhase.Text = "Ajouter une phase"
    End If

  End Sub

  Private Overloads Sub ActiverAspectPhases(ByVal Affich� As Boolean)
    Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
    Dim i As Short

    For i = 1 To MAXPHASES
      fg.Cols(i).Visible = Affich�
    Next
    Me.btnActionPhase.Enabled = Affich�

  End Sub

  '**********************************************************************************************************************
  'Affecter l'organisation affich�e au plan de feux de base en cours de la variante
  '**********************************************************************************************************************
  Private Sub chkD�coupagePhases_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkD�coupagePhases.CheckedChanged

    If ChargementEnCours Then
      monPlanFeuxBase.D�terminerAutorisationsD�calage()
      Exit Sub
    End If

    Try

      'Eviter que la saisie des rouges de d�gagement,si active auparavant,  ne le soit plus automatiquement
      Me.radMatriceConflits.Checked = True

      If chkD�coupagePhases.Checked Then
        If Not PhasageRetenu Then ' sinon simple r�activation de la case suite � la s�lection du phasage retenu
          'S�lectionner comme plan de feux de base celui en cours d'affichage
          D�duirePlanbasePhasage()
          'Ajouter le plan la a 1�re fois que la case est coch�e (elle peutl'�tre ensuite par programme)
          'AV (21/02/2007) :Nouvelle d�finition du phasage retenu
          'maVariante.mPlansFeuxBase.Add(monPlanFeuxBase)

        Else
          monPlanPourPhasage = monPlanFeuxBase.PlanPhasageAssoci�
          AfficherComboPhasage()
          'Dim fg As GrilleDiagfeux = Me.AC1GrillePhases
          'fg.Cols(MAXPHASES).Visible = (monPlanPourPhasage.mPhases.Count = MAXPHASES)

        End If

        monPlanFeuxBase.D�terminerAutorisationsD�calage()

      Else
        'D�cochage du [phasage_retenu]
        If PhasageRetenu AndAlso monPlanPourPhasage Is monPlanFeuxBase.PlanPhasageAssoci� Then
          'D�cochage manuel du phasage retenu (et non par programme suite au choix d'un autre Phasage que celui retenu pour affichage)
          'AV (21/02/2007) :Nouvelle d�finition du phasage retenu
          'maVariante.mPlansFeuxBase.Remove(monPlanFeuxBase)
          monPlanFeuxBase.PlanPhasageAssoci� = Nothing

          'Lignes suivantes : pour �viter des controles intempestifs lors des prochaines initialisation du plan de feux de base
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

  Private Sub D�duirePlanbasePhasage()
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
      LancerDiagfeuxException(ex, "D�duirePlanbasePhasage")
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
          unPlanPourPhasage.mPhases.D�placer(unPlanPourPhasage.mPhases.PhaseEquivalente(unePhase), monPlanFeuxBase.mPhases.IndexOf(unePhase))
        Next
        'Associer le Sc�nario au plan de phasage retenu
        'Cette fonction recalcule en particulier les dur�es mini '
        'pour prendre en compte les vrais temps de rouge de d�gagement, qui ne peuvent plus d�sormais �tre modif�s)
        monPlanFeuxBase.PlanPhasageAssoci� = unPlanPourPhasage
        Me.cboD�coupagePhases.Text = "Phasage " & CStr(Index + 1)
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
  'R�initialisation du(des) phasage(s) suite � la modification des vitesses de d�gagement : classe Param�trage
  '***************************************************************************************************
  Public Sub R�initialiserPhasages()
    maVariante.R�initialiserPhasages()
    ChoisirOngletInitial()

  End Sub
#End Region
#Region " Plans de feux"
  Private flagKeyPress As Boolean
#Region "Ensemble Plans feux"
  '***********************************************************************************
  ' Retourne l'index du bouton radio s�lectionn� dans l'onglet plans de feux
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
          If ChangementDeSc�nario Then
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
            Me.cboD�coupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)
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
                'Donner par d�faut le nom du sc�nario
                .mPlansFonctionnement.Add(New PlanFeuxFonctionnement(monPlanFeuxBase, .Nom))
                .PlanFonctionnementCourant = .mPlansFonctionnement(.Nom)
                'Affecter par d�faut la p�riode de trafic ayant servi � construire le sc�nario
                .PlanFonctionnementCourant.Trafic = .Trafic
                'L'instruction qui suit est pour que le test juste apr�s �choue
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
            Me.cboM�thodeCalculCycle.Enabled = monPlanFeuxBase.AvecTrafic

            RedessinerDiagrammePlanFeux()

          ElseIf Not PhasageRetenu Then
            AfficherMessageErreur(Me, "Choisir d'abord l'organisation du phasage")
            radPhasage.Checked = True
          ElseIf monPlanFeuxBase.PhasageIncorrect Then
            AfficherMessageErreur(Me, "L'organisation du phasage est incorrecte")
            radPhasage.Checked = True
            Me.cboD�coupagePhases.SelectedIndex = mesPlansPourPhasage.IndexOf(monPlanFeuxBase)

          Else
            AfficherMessageErreur(Me, "Verrouiller d'abord le plan de feux de base")
            radFeuBase.Checked = True
          End If

      End Select

      If CType(sender, RadioButton).Checked Then
        D�finirSplitPosition()
        AfficherCacherDiagnostic()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub chkSc�narioD�finitif_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSc�narioD�finitif.CheckedChanged

    With maVariante

      If Me.chkSc�narioD�finitif.Checked Then
        Dim OK As Boolean = True
        If IsNothing(.Sc�narioD�finitif) Then
          '1�re fois que l'on choisit un sc�nario comme d�finitif
          OK = True
        ElseIf .Sc�narioD�finitif Is monPlanFeuxBase Then
          'Appel suite � S�lection du sc�nario d�finitif dans la liste des sc�narios(S�lectionnerSc�nario)
          OK = True
        Else
          'Demander confirmation du changement de sc�nario d�finitif
          OK = Confirmation("Vous avez d�j� retenu le sc�nario " & .Sc�narioD�finitif.Nom & vbCrLf & "Souhaitez-vous en changer", Critique:=False)
        End If
        If OK Then
          .Sc�narioD�finitif = monPlanFeuxBase
        Else
          Me.chkSc�narioD�finitif.Checked = False
        End If

      Else
        If .Sc�narioD�finitif Is monPlanFeuxBase Then
          'Sinon, c'est qu'on conserve le pr�c�dent sc�nario d�finitif
          .Sc�narioD�finitif = Nothing
        End If
      End If
    End With

    AfficherProjetD�finitif()

  End Sub
#End Region
#Region "Plans base et fonctionnement"
  '***********************************************************************************
  ' Mise � jour d'une phase suite � modif d'une autre en respectant le cycle total
  ' ==> Ces controles sont en ReadOnly, car les mini/maxi ne sont pas control�s
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

          ' D�terminer la phase suivante (en ignorant une �ventuelle phase verrouill�e)
          Dim updNext As NumericUpDown
          Dim PhaseSuivante As Phase = monPlanFeuxActif.mPhases.PhaseSuivante(unePhase)
          If PhaseSuivante.Verrouill�e Then PhaseSuivante = monPlanFeuxActif.mPhases.PhaseSuivante(PhaseSuivante)

          updNext = updAssoci�Phase(PhaseSuivante)

          'Par d�faut, on incr�mente ou d�cr�mente la phase qui suit la phase modifi�e
          'Si celle-ci est verrouill�e on agit sur la suivante
          'Calcul de la nouvelle valeur � afficher dans le controle r�sultant
          Dim Diff�rence As Short = updPhase.Value - unePhase.Dur�e
          Dim R�sultat As Short
          R�sultat = PhaseSuivante.Dur�e - Diff�rence

          If R�sultat < PhaseSuivante.Dur�eIncompressible Then
            'Refuser la modification
            updPhase.Value -= Diff�rence
          ElseIf R�sultat > updNext.Maximum Then
            'Refuser la modification
            updPhase.Value += Diff�rence
          Else
            'Mettre � jour les dur�es des phases
            unePhase.Dur�e = updPhase.Value
            PhaseSuivante.Dur�e = R�sultat
            Modif = True
          End If

          'Afficher les nouvelles dur�es des phases : la dur�e du cycle n'est pas chang�e, il n'y a pas lieu de recalculer la capacit�
          AfficherDur�esPhases(Capacit�ARecalculer:=False)

        Catch ex As System.Exception

          AfficherMessageErreur(Me, ex)
        End Try

      Else
        '  Initialisation de la feuille ou mise � jour par un autre m�canisme
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  ' IndexPhase : retourne l'index de la phase correspondant � l'UpDown cliqu�
  ' updPhase : UpDown cliqu�
  '*******************************************************************************************************
  Private Overloads Function IndexPhase(ByVal updPhase As NumericUpDown) As Short
    'les noms des updown de phase commencent tous par updPhase : le num�ro est en 1�re position
    'Retirer 1 pour obtenir l'index de la phase
    Return CType(Mid(updPhase.Name, 9, 1), Short) - 1
  End Function

  '*******************************************************************************************************
  ' IndexPhase : retourne l'index de la phase correspondant au bouton radio
  ' radPhase : bouton radio concern�
  '*******************************************************************************************************
  Private Overloads Function IndexPhase(ByVal radPhase As RadioButton) As Short
    'les noms des boutons radios de phase commencent tous par radPhase : le num�ro est en 1�re position
    'Retirer 1 pour obtenir l'index de la phase
    Return CType(Mid(radPhase.Name, 9, 1), Short) - 1
  End Function

  '*******************************************************************************************************
  ' updAssoci�Phase : retourne le UpDown correspondant � la phase
  ' unePhase : Phase pour laquelle on recherche le UpDown
  '*******************************************************************************************************
  Private Function updAssoci�Phase(ByVal unePhase As Phase) As NumericUpDown
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
  ' radAssoci�Phase : retourne le bouton radio correspondant � la phase
  ' unePhase : Phase pour laquelle on recherche le bouton radio
  '*******************************************************************************************************
  Private Function radAssoci�Phase(ByVal unePhase As Phase) As RadioButton
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

  Private Sub txtDur�eCycle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
  Handles txtDur�eCycleBase.KeyDown, txtDur�eCycleFct.KeyDown
    flagKeyPress = EstIncompatibleNum�rique(e)
  End Sub

  Private Sub txtDur�eCycle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtDur�eCycleBase.KeyPress, txtDur�eCycleFct.KeyPress

    Dim txt As TextBox = sender

    If flagKeyPress Then
      'Touche refus�e par l'�v�nement KeyDown
      e.Handled = True
      flagKeyPress = False
    Else
      e.Handled = ToucheNonNum�rique(e.KeyChar, Entier:=True)
    End If

  End Sub

  '************************************************************************************************
  ' Validation de la saisie de la dur�e du cycle
  '************************************************************************************************
  Private Sub txtDur�eCycle_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtDur�eCycleBase.Validating, txtDur�eCycleFct.Validating

    Dim txt As TextBox = sender
    Dim Dur�e As Short

    With monPlanFeuxActif
      e.Cancel = ControlerBornes(Me, .Dur�eCycle(Minimum:=True), PlanFeux.maxiDur�eCycleAbsolue, txt, .Dur�eCycle, unFormat:="#00")
    End With

    If Not e.Cancel Then
      Try
        Dur�e = CType(txt.Text, Short)
        If Dur�e > PlanFeux.maxiDur�eCycle Then
          MessageBox.Show("Une dur�e de cycle sup�rieure � " & PlanFeux.maxiDur�eCycle & "s est d�conseill�e")
        End If
        Red�finirDur�esPhases(Dur�eCycle:=Dur�e)

      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  '************************************************************************************************
  ' R�partir sur les phases l'allongement ou le raccourcissement de la dur�e du cycle
  '************************************************************************************************
  Private Sub Red�finirDur�esPhases(ByVal Dur�eCycle As Short, Optional ByVal VerrouillageConserv� As Boolean = False)
    Dim i As Short
    Dim unePhase As Phase

    With monPlanFeuxActif
      If IsNothing(.Trafic) Then
        'R�partition �gale entre les phases du d�calage demand�
        Dim D�calage As Short = Dur�eCycle - .Dur�eCycle

        'D�verrouiller l'�ventuelle phase verrouill�e (pour la fonction PhaseSuivante)
        For Each unePhase In .mPhases
          'M�moriser la phase pr�c�demment verrouill�e
          If unePhase.Verrouill�e Then i = .mPhases.IndexOf(unePhase) + 1
        Next
        .D�verrouillerPhases()

        If .mPhases.Count > 2 And TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
          'MODIF AV(13/02/06) : On ne d�verrouille plus la phase fig�e
          ' C'est en particulier obligatoire si cette phase est une phase uniquement pi�tonne
          For Each unePhase In .mPhases
            If unePhase.EstSeulementPi�ton Then
              VerrouillerPhase(.mPhases.IndexOf(unePhase))
              Exit For
            End If
          Next
        End If

        unePhase = .mPhases(CType(0, Short))

        If D�calage <> 0 Then
          Modif = True
        End If

        Do While D�calage <> 0
          If D�calage > 0 Then
            unePhase.Dur�e += 1
            D�calage -= 1
          Else
            If unePhase.Dur�e > unePhase.Dur�eIncompressible Then
              unePhase.Dur�e -= 1
              D�calage += 1
            End If
          End If
          unePhase = .mPhases.PhaseSuivante(unePhase)
        Loop

        If i > 0 Then VerrouillerPhase(i - 1) ' .mPhases(CType(i - 1, Short)).Verrouill�e = True

      Else
        'Avec trafic 
        'R�partition entre les phases du d�calage demand� en fonction du trafic support� par chaque phase
        .R�partirDur�eCycle(Dur�eCycle)

        If TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
          RecalculerCapacit�()
        End If
      End If


    End With

    'Mettre � jour les controles upDown en conformit� avec les nouvelles dur�es
    AfficherDur�esPhases(Capacit�ARecalculer:=True)

  End Sub

  '*******************************************************************************************************
  ' Changement de la phase verrouill�e
  ' radPhase : bouton radio d�terminant la phase � verrouiller
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
      'D�verrouille si n�cessaires la phase verrouill�e
      .D�verrouillerPhases()
      For Each unePhase In .mPhases
        If unePhase Is .mPhases(Index) Then
          'Verrouiller la phase
          radAssoci�Phase(unePhase).Checked = True
          updAssoci�Phase(unePhase).Enabled = False
          unePhase.Verrouill�e = True
        Else
          'D�Verrouiller la phase
          updAssoci�Phase(unePhase).Enabled = True
          unePhase.Verrouill�e = False
        End If
      Next

    End With

  End Sub

  '******************************************************************************
  ' S�lection d'une nouvelle ligne dans le tableau des dur�es de vert
  '******************************************************************************
  Private Sub lvwDur�eVert_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles lvwDur�eVert.SelectedIndexChanged, lvwDur�eVertFct.SelectedIndexChanged

    Try
      Dim lvw As ListView = sender
      Dim updOuverture As NumericUpDown = IIf(lvw Is Me.lvwDur�eVert, Me.updD�calageOuvertureBase, Me.updD�calageOuvertureFct)
      Dim updFermeture As NumericUpDown = IIf(lvw Is Me.lvwDur�eVert, Me.updD�calageFermetureBase, Me.updD�calageFermetureFct)
      Dim unPlanFeux As PlanFeux = updOuverture.Tag
      Dim desPhases As PhaseCollection = unPlanFeux.mPhases

      Dim itmX As ListViewItem = ItemDur�evertS�lectionn�(lvw)


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

          Dim VertMini As Short = IIf(uneLigneFeux.EstPi�ton, maVariante.VertMiniPi�tons, maVariante.VertMiniV�hicules)
          Dim Maximum As Short = unPlanFeux.Dur�eVertMaxi(uneLigneFeux) - VertMini
          Dim D�calOuvre As Short = unPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Ouverture)
          Dim D�calFerme As Short = unPlanFeux.D�calageOuvreFerme(uneLigneFeux, PlanFeux.D�calage.Fermeture)
          updOuverture.Maximum = Maximum - D�calFerme
          updFermeture.Maximum = Maximum - D�calOuvre

          updOuverture.Value = D�calOuvre
          updFermeture.Value = D�calFerme
          updOuverture.Visible = True
          updFermeture.Visible = True
          updOuverture.Enabled = uneLigneFeux.D�calageOuvertureAutoris�
        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        End Try
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  ' Diminution/augmentation des d�calages � l'ouverture ou � la fermeture de la ligne de feux s�lectionn�e
  '*******************************************************************************************************
  Private Sub updD�calageOuvertureFermeture_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles updD�calageFermetureFct.ValueChanged, updD�calageOuvertureFct.ValueChanged, updD�calageFermetureBase.ValueChanged, updD�calageOuvertureBase.ValueChanged

    Dim upd As NumericUpDown = sender

    If Not IsNothing(upd.Tag) Then
      Try
        Red�finirD�calage(upd)
      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try
    End If

  End Sub

  Private Sub Red�finirD�calage(ByVal upd As NumericUpDown)
    Dim updAutre As NumericUpDown

    Try

      'D�terminer la ligne de feux s�lectionn�e dans le tableau des dur�es de vert du plan de feux
      Dim unPlanFeux As PlanFeux = upd.Tag
      'Sinon l'�v�nement ValueChanged est appel� abusivement  (si l'appel vient de Validating)
      upd.Tag = Nothing

      Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDur�eVert, Me.lvwDur�eVertFct)
      Dim itmX As ListViewItem = ItemDur�evertS�lectionn�(lvw)

      If Not IsNothing(itmX) Then
        Dim uneLigneFeux As LigneFeux = itmX.Tag
        Dim Index As PlanFeux.D�calage = IIf(upd Is Me.updD�calageOuvertureBase Or upd Is Me.updD�calageOuvertureFct, PlanFeux.D�calage.Ouverture, PlanFeux.D�calage.Fermeture)
        Dim VertMini As Short = IIf(uneLigneFeux.EstPi�ton, maVariante.VertMiniPi�tons, maVariante.VertMiniV�hicules)
        Dim Diff�rence As Short

        'Rechercher la phase concern�e par la ligne de feux et m�moriser de combien le d�calage va varier
        Dim desPhases As PhaseCollection = unPlanFeux.mPhases

        'Mettre � jour la valeur du d�calage
        Diff�rence = upd.Value - unPlanFeux.D�calageOuvreFerme(uneLigneFeux, Index)
        If Diff�rence <> 0 Then Modif = True
        unPlanFeux.D�calageOuvreFerme(uneLigneFeux, Index) = upd.Value

        'Mettre � jour la ligne de feux dans le tableau
        itmX.SubItems(Index + 3).Text = upd.Value
        AfficherDur�eVert(unPlanFeux, uneLigneFeux)

        'Red�finir le maximum acceptable pour l'autre UpDown
        If TypeOf unPlanFeux Is PlanFeuxBase Then
          If upd Is Me.updD�calageOuvertureBase Then
            updAutre = Me.updD�calageFermetureBase
          Else
            updAutre = Me.updD�calageOuvertureBase
          End If
        Else
          If upd Is Me.updD�calageOuvertureFct Then
            updAutre = Me.updD�calageFermetureFct
          Else
            updAutre = Me.updD�calageOuvertureFct
          End If
          RecalculerCapacit�()
        End If
        updAutre.Maximum -= Diff�rence

        upd.Tag = unPlanFeux

        RedessinerDiagrammePlanFeux()
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Red�finirD�calage")
    End Try

  End Sub

  Private Sub updD�calageOuvertureFermeture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles updD�calageFermetureBase.Validating, updD�calageOuvertureBase.Validating, updD�calageFermetureFct.Validating, updD�calageOuvertureFct.Validating
    Dim Donn�e As Short


    Try
      Dim updPF As NumericUpDown = sender

      'D�terminer la ligne de feux s�lectionn�e dans le tableau des dur�es de vert du plan de feux
      Dim unPlanFeux As PlanFeux = updPF.Tag
      Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDur�eVert, Me.lvwDur�eVertFct)
      Dim itmX As ListViewItem = ItemDur�evertS�lectionn�(lvw)
      Dim uneLigneFeux As LigneFeux = itmX.Tag

      'Rechercher la phase concern�e par la ligne de feux et m�moriser de combien le d�calage va varier
      Dim desPhases As PhaseCollection = unPlanFeux.mPhases

      Dim Index As PlanFeux.D�calage = IIf(updPF Is Me.updD�calageOuvertureBase Or updPF Is Me.updD�calageOuvertureFct, PlanFeux.D�calage.Ouverture, PlanFeux.D�calage.Fermeture)

      Donn�e = unPlanFeux.D�calageOuvreFerme(uneLigneFeux, Index)

      If Donn�e <> updPF.Value Then
        e.Cancel = ControlerBornes(Me, updPF.Minimum, updPF.Maximum, updPF, CType(Donn�e, String))

        If Not e.Cancel Then
          Red�finirD�calage(updPF)
        End If
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '***********************************************************************************
  ' Afficher les dur�es de chaque phase et les dur�es de vert 
  '***********************************************************************************
  Private Sub AfficherDur�esPhases(ByVal Capacit�ARecalculer As Boolean)
    Dim unePhase As Phase
    Dim desPhases As PhaseCollection = monPlanFeuxActif.mPhases
    Dim uneLigneFeux As LigneFeux

    Dim upd As NumericUpDown

    Try

      'Mettre � jour les dur�es de phases
      For Each unePhase In desPhases
        upd = updAssoci�Phase(unePhase)
        'D�sactiver le tag pour que l'�v�nement updPhase_ValueChanged ne fasse rien
        upd.Tag = Nothing
        upd.Value = unePhase.Dur�e
        upd.Tag = unePhase
        'upd.Enabled = True remplac� par ceci (MODIF AV : 18/09/06  - Surveiller les r�gressions)
        upd.Enabled = Not unePhase.Verrouill�e

        'Afficher les dur�es de vert
        For Each uneLigneFeux In mesLignesFeux
          If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
            Try
              AfficherDur�eVert(monPlanFeuxActif, uneLigneFeux)

            Catch ex As DiagFeux.Exception
              Throw New DiagFeux.Exception(ex.Message)
            Catch ex As System.Exception
              LancerDiagfeuxException(ex, "Affichage des dur�es des phases")
            End Try
          End If
        Next
      Next

      RedessinerDiagrammePlanFeux()

      If TypeOf monPlanFeuxActif Is PlanFeuxFonctionnement Then
        If Capacit�ARecalculer Then
          D�terminerAfficherCapacit�()
        Else
          AfficherInfosAttente()
        End If
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherDur�esPhases")
    End Try

  End Sub


  '*************************************************************************************************
  ' AfficherDur�eVert : Affiche la dur�e de vert d'une ligne de feux
  ' uneLigneFeux : ligne de feux concern�e
  '*************************************************************************************************
  Private Sub AfficherDur�eVert(ByVal unPlanFeux As PlanFeux, ByVal uneLigneFeux As LigneFeux)
    Dim lvw As ListView = IIf(TypeOf unPlanFeux Is PlanFeuxBase, Me.lvwDur�eVert, Me.lvwDur�eVertFct)

    Try
      With lvw
        'il faut recalculer le vert de la ligne de feux(fonction des dur�es de phases et des d�calages)
        .Items(mLignesFeux.IndexOf(uneLigneFeux)).SubItems(2).Text = unPlanFeux.Dur�eVert(uneLigneFeux)
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Affichage de la dur�e de vert de la ligne " & uneLigneFeux.ID)
    End Try

  End Sub

  '*************************************************************************************************
  ' ItemDur�evertS�lectionn�:  ligne s�lectionn�e dans le tableau des dur�es de vert du plan de feux
  '*************************************************************************************************
  Private Function ItemDur�evertS�lectionn�(ByVal lvw As ListView) As ListViewItem
    Dim lstItems As ListView.ListViewItemCollection = lvw.Items

    If lvw.SelectedItems.Count > 0 Then Return lstItems(lvw.SelectedIndices(0))

  End Function
#End Region
#Region "Plans base"
  '************************************************************************************************
  ' Frappe d'une touche dans les textbox Vert Mini V�hicules ou pi�tons(plan de feux de base)
  ' Interdit la frappe d'une touche non num�rique
  '************************************************************************************************
  Private Sub txtVertMini_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    flagKeyPress = EstIncompatibleNum�rique(e)
  End Sub

  Private Sub txtVertMini_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    If flagKeyPress Then
      'Touche refus�e par l'�v�nement KeyDown
      e.Handled = True
      flagKeyPress = False
    End If
  End Sub

  '************************************************************************************************
  ' Validation des textbox Vert Mini V�hicules ou pi�tons(plan de feux de base)
  '************************************************************************************************
  Private Sub txtVertMini_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtVertMiniPi�ton.Validating, txtVertMiniV�hicule.Validating

    Dim chaine As String
    Dim txt As TextBox = sender
    Dim V�hicule As Boolean = txt Is txtVertMiniV�hicule
    'D�terminer le vert mini � controler
    Dim VertMiniAbsolu As Short = IIf(V�hicule, [Global].VertMiniV�hicules, [Global].VertMiniPi�tons)
    Dim VertMiniActuel As Short = IIf(V�hicule, monPlanFeuxBase.VertMiniV�hicules, monPlanFeuxBase.VertMiniPi�tons)

    Try
      chaine = txt.Text
      If chaine.Length = 0 Then
        e.Cancel = True
      Else
        e.Cancel = ControlerBornes(Me, VertMiniAbsolu, [Global].VertMiniMaximum, txt, VertMiniActuel, unFormat:="#0")
      End If

      If Not e.Cancel Then
        Dim R�initialiser As Boolean
        'Mettre � jour le nouveau mini de vert pour la variante consid�r�e
        If V�hicule Then
          'le test qui suit est en principe superflu, mais il s'av�re que 
          'l'�v�nement est d�clench� anormalement (d�calageouverture_lostfocus (?)) ce qui relance InitPlanFeuxBase
          If monPlanFeuxBase.VertMiniV�hicules <> CInt(chaine) Then
            R�initialiser = True
            monPlanFeuxBase.VertMiniV�hicules = CInt(chaine)
          End If
        Else
          If monPlanFeuxBase.VertMiniPi�tons <> CInt(chaine) Then
            R�initialiser = True
            monPlanFeuxBase.VertMiniPi�tons = CInt(chaine)
          End If
        End If

        If R�initialiser Then
          'Recalculer et afficher le nouveau plan de feux de s�curit�
          InitPlanFeuxBase(RecalculerMini:=True)
        End If


      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub btnLigneFeuDescendrePlans_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
Handles btnLigneFeuDescendrePlans.Click, btnLigneFeuMonterPlans.Click
    Dim D�calage As Short, Position As Short

    Try

      'D�terminer le sens du d�calage
      If sender Is btnLigneFeuDescendrePlans Then
        D�calerLigneFeux(+1, lvwDur�eVert)
      Else
        D�calerLigneFeux(-1, lvwDur�eVert)
      End If
      '      Me.cboTriLignesFeuxPlans.SelectedIndex = 0

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try


  End Sub

  Private Sub D�calerLigneFeux(ByVal D�calage As Short, ByVal lvw As ListView)
    Dim Index As Short

    Try

      Dim itmX As ListViewItem = ItemDur�evertS�lectionn�(lvw)

      If Not IsNothing(itmX) Then
        Dim uneLigneFeux As LigneFeux = itmX.Tag
        Index = mLignesFeux.IndexOf(uneLigneFeux)
        Dim Continuer As Boolean = (Index > 0 And D�calage = -1) Or (Index < mLignesFeux.Count - 1 And D�calage = 1)

        If Continuer Then
          'D�caler la ligne de feux
          mLignesFeux.D�caler(D�calage, uneLigneFeux)
          AfficherCons�quencesModifLignesDeFeuxPlans()
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
      LancerDiagfeuxException(ex, "D�calerLigneFeux")
    End Try

  End Sub

  Private Sub cboTriLignesFeuxPlans_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTriLignesFeuxPlans.SelectedIndexChanged
    Dim Ordre As LigneFeuxCollection.OrdreDeTriEnum

    If cboTriLignesFeux.SelectedIndex <> 0 Then
      monPlanFeuxBase.mLignesFeux.Trier(Ordre:=cboTriLignesFeuxPlans.SelectedIndex)
      AfficherCons�quencesModifLignesDeFeuxPlans()
    End If

  End Sub

  Private Sub AfficherCons�quencesModifLignesDeFeuxPlans()
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

    'Cr�er la liste des plans de feux de fonctionnement
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
        'S�lectionner le dernier PFF qui �tait s�lectionn� lorsque ce plan de base �tait actif
        Me.cboPlansDeFeux.Text = .PlanFonctionnementCourant.Nom
      End If
    End With

  End Sub
  '******************************************************************************
  ' Cr�er un nouveau plan de Feux de fonctionnement
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
      'Proposer le plan de feu de base par d�faut
      .cboPlansDeFeux.SelectedIndex = 0

      'Lister les trafics
      For Each unTrafic In mesTrafics
        If unTrafic.Verrouill� Then
          Index = .cboTrafic.Items.Add(unTrafic.Nom)
          'Proposer par d�faut le trafic de du sc�nario en cours
          If unTrafic Is monPlanFeuxBase.Trafic Then
            .cboTrafic.SelectedIndex = Index
          End If
        End If
      Next
      If Not monPlanFeuxBase.AvecTrafic Then
        'Par d�faut, proposer <Aucun trafic> , si le sc�nario en cours est sans trafic
        .cboTrafic.SelectedIndex = 0
      End If

      'Saisir les informations du nouveau plan de feux de fonctionnement
      .ShowDialog(Me)

      If .DialogResult = DialogResult.OK Then
        'Cr�er le plan de feux
        NomPlan = .txtNomPlan.Text
        If monPlanFeuxBase.mPlansFonctionnement.Contains(NomPlan) Then
          AfficherMessageErreur(Me, "Le plan " & NomPlan & " existe d�j�")

        Else
          Modif = True

          If .cboPlansDeFeux.SelectedIndex = 0 Then
            'Partir du plan de feux de base
            unPlan = New PlanFeuxFonctionnement(monPlanFeuxBase, NomPlan)
          Else
            'Partir d'un plan de feux de fonctionnement existant
            unPlan = New PlanFeuxFonctionnement(monPlanFeuxBase.mPlansFonctionnement(CType(.cboPlansDeFeux.SelectedIndex - 1, Short)), NomPlan)
          End If

          'Afficher le trafic (�ventuel) correspondant au plan de feux
          If .cboTrafic.SelectedIndex > 0 Then
            unPlan.Trafic = mesTrafics(.cboTrafic.Text)
          End If

          'Ajouter le plan � la combo
          Me.cboPlansDeFeux.Items.Add(unPlan.Nom)
          'S�lectionner ce plan comme plan � afficher
          Me.cboPlansDeFeux.Text = unPlan.Nom

        End If
      End If

      .Dispose()
    End With

  End Sub

  Private Sub btnRenommerPlanFeux_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenommerPlanFeux.Click
    Dim Index As Short = Me.cboPlansDeFeux.SelectedIndex
    Dim R�ponse As String = InputBox("Renommer le plan de feux en : ")

    If R�ponse.Length > 0 Then
      monPlanFeuxFonctionnement.Nom = R�ponse
      'Mettre � jour la combode la liste des plans de feux
      Me.cboPlansDeFeux.Items.RemoveAt(Index)
      Me.cboPlansDeFeux.Items.Insert(Index, R�ponse)
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
      'Se repositionner syst�matiquement sur le 1er de la liste (il existe toujours car on n'a pas le droit de supprimer tous les plans de feux)
      Me.cboPlansDeFeux.SelectedIndex = 0

      Modif = True
    End If
  End Sub

  '******************************************************************************
  ' G�rer l'activation des boutons du panel Feux de fonctionnement
  '******************************************************************************
  Private Sub ActiverBoutonsPlansDeFeux()

    Me.btnSupprimerPlanFeux.Enabled = Me.cboPlansDeFeux.Items.Count > 1
    Me.btnRenommerPlanFeux.Enabled = Me.cboPlansDeFeux.Items.Count > 0
    Me.btnDiagnostic.Enabled = monPlanFeuxActif.AvecTrafic

  End Sub

  Private Sub btnCalculerCycle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalculerCycle.Click
    'Calculer la dur�e du cycle
    Dim M�thode As M�thodeCalculCycle = Me.cboM�thodeCalculCycle.SelectedIndex
    Dim Dur�eCycle As Short
    Dim Message As String

    Try

      If M�thode = M�thodeCalculCycle.Webster Then
        Dur�eCycle = monPlanFeuxActif.CalculCycle(Message)

      Else
        'M�thode Classique
        Dim R�serveCapacit�Admise As Single
        Select Case Me.cboR�serveCapacit�Choisie.SelectedIndex
          Case 0
          Case 1
            R�serveCapacit�Admise = 0.1
          Case 2
            R�serveCapacit�Admise = 0.15
          Case 3
            R�serveCapacit�Admise = 0.2
        End Select
        Dur�eCycle = monPlanFeuxActif.CalculCycle(Message, CoefDemande:=R�serveCapacit�Admise)
      End If

      If Dur�eCycle = 0 Then
        AfficherMessageErreur(Me, Message)

      ElseIf Dur�eCycle <> monPlanFeuxActif.Dur�eCycle Then
        If Dur�eCycle > PlanFeux.maxiDur�eCycle Then
          AfficherMessageErreur(Me, "Dur�e de cycle importante : " & Dur�eCycle & " s")
        End If
        Red�finirDur�esPhases(Dur�eCycle:=CType(Dur�eCycle, Short))
        Me.txtDur�eCycleFct.Text = Dur�eCycle
      End If

      Me.btnCalculerCycle.Enabled = False
      Me.cboR�serveCapacit�Choisie.Enabled = False

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
      '  D�terminerAfficherCapacit�()
      'End If

      'Afficher le trafic (�ventuel) correspondant au plan de feux
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

      'Par d�faut, on propose une m�thode manuelle
      Me.cboM�thodeCalculCycle.SelectedIndex = 0
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
    'M�moriser le pr�c�dent trafic du plan de feux
    Dim unTrafic As Trafic = monPlanFeuxActif.Trafic
    Dim newTrafic As Trafic
    Static Passage As Boolean
    Dim Diagnostic�Afficher As Boolean

    If Passage Then
      Passage = False
    Else

      Try

        With cboTraficFct
          If .SelectedIndex = 0 Then
            monPlanFeuxActif.Trafic = Nothing
            Diagnostic�Afficher = True

          Else
            newTrafic = maVariante.mTrafics(.Text)
            'Ne pas accepter un trafic non verrouill� (il peut en particulier �tre vide)
            If Not newTrafic.Verrouill� Then
              MessageBox.Show("Cette p�riode de trafic n'est pas verrouill�e" & vbCrLf & "Elle ne peut pas �tre choisie pour un plan de feux")
              Passage = True
              If monPlanFeuxActif.AvecTrafic Then
                Me.cboTraficFct.Text = unTrafic.Nom
              Else
                Me.cboTraficFct.SelectedIndex = 0
              End If
              Exit Sub
            End If

            If Not newTrafic Is monPlanFeuxActif.Trafic Then
              Diagnostic�Afficher = True
              monPlanFeuxActif.Trafic = newTrafic
            End If
            'Bien que la demande ait pu �tre d�j� calcul�e pour un autre plan de feux avec le m�me trafic
            ' il est + simple de la recalculer
            'Mis en commentaire (AV :18/06/07) - Inutile dans Diagfeux (nouveaux drapeaux dans l'objet PlanFeux)
            'monPlanFeuxActif.CalculerDemande()
          End If

          FenetreDiagnostic.AffecterPlanFeux(monPlanFeuxActif)
          ' Afficher les donn�es de capacit� si une p�riode de trafic est associ�e
          D�terminerAfficherCapacit�()

          If Diagnostic�Afficher Then
            ' Test rajout� (DIAGFEUX 2  : 10/07/07) pour limiter les r�affichages du diagnostic
            ' On affiche automatiquement celle-ci :
            ' Bouton Calculer
            ' Changement de cycle
            ' ici : uniquement si l'appel de la fonction est du � un changement de trafic du plan de feux courant, 
            ' et non indirectement suite � la s�lection d'un autre plan de feux de fonctionnement
            AfficherDiagnostic(PourCacher:=IsNothing(monPlanFeuxActif.Trafic))
          End If

          Me.cboM�thodeCalculCycle.Enabled = Not IsNothing(monPlanFeuxActif.Trafic)
          'Par d�faut, on propose une m�thode manuelle
          Me.cboM�thodeCalculCycle.SelectedIndex = 0

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

  Private Sub cboM�thodeCalculCycle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboM�thodeCalculCycle.SelectedIndexChanged
    Dim M�thode As M�thodeCalculCycle = Me.cboM�thodeCalculCycle.SelectedIndex
    Select Case M�thode
      Case M�thodeCalculCycle.Manuel
        'Pas de calcul
        Me.btnCalculerCycle.Enabled = False
        Me.cboR�serveCapacit�Choisie.Enabled = False
      Case M�thodeCalculCycle.Webster
        'Calcul possible sans utiliser la r�serve de capacit�
        Me.btnCalculerCycle.Enabled = True
        Me.cboR�serveCapacit�Choisie.Enabled = False
      Case M�thodeCalculCycle.Classique
        'Calcul possible d�s qu'on aura choisi une r�serve de capacit�
        Me.btnCalculerCycle.Enabled = False
        Me.cboR�serveCapacit�Choisie.Enabled = True
    End Select

    'Remettre � blanc la r�serve de capacit�: soit elle ne sert � rien(2 1ers cas), soit on veut que l'utilisteur la choisisse volontairement
    Me.cboR�serveCapacit�Choisie.SelectedIndex = -1

  End Sub

  Private Sub cboR�serveCapacit�Choisie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles cboR�serveCapacit�Choisie.SelectedIndexChanged

    Dim M�thode As M�thodeCalculCycle = Me.cboM�thodeCalculCycle.SelectedIndex
    Me.btnCalculerCycle.Enabled = cboR�serveCapacit�Choisie.SelectedIndex <> -1 Or M�thode = M�thodeCalculCycle.Webster
  End Sub

  '**************************************************************************************************************
  'Recalcul du diagnostic suite � la modification du d�bit de saturation ou du temps perdu : classe Param�trage
  '***************************************************************************************************************
  Private Sub RecalculerCapacit�()
    monPlanFeuxActif.Capacit�ACalculer = True
    D�terminerAfficherCapacit�()
    AfficherDiagnostic()
  End Sub
  '**********************************************************************************************************************
  ' Afficher la capacit� du plan de feux et les infos qui en d�coulent
  '**********************************************************************************************************************
  Private Sub D�terminerAfficherCapacit�(Optional ByVal unPlanFeux As PlanFeux = Nothing)

    If IsNothing(unPlanFeux) Then unPlanFeux = monPlanFeuxActif

    With unPlanFeux

      Try
        If TypeOf unPlanFeux Is PlanFeuxBase Then
          If unPlanFeux.AvecTrafic Then
            ' Affichage dans le volet Organisation du phasage

            .CalculerR�serveCapacit�()
            Me.txtR�serveCapacit�PourCent.Text = .strR�serveCapacit�PourCent
          End If

        Else

          If .AvecTrafic Then
            If .Capacit�ACalculer Then
              .CalculerR�serveCapacit�()
            End If
            FenetreDiagnostic.AfficherCapacit�()
          Else
            AfficherDiagnostic(PourCacher:=True)
          End If
        End If

      Catch ex As DiagFeux.Exception
        Throw New DiagFeux.Exception(ex.Message)

      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "D�terminerAfficherCapacit�")
      End Try

    End With    'unPlanFeux

  End Sub

  Public Sub RecalculerCapacit�s()

    maVariante.R�initialiserCapacit�s()

    If Not IsNothing(monPlanFeuxFonctionnement) Then
      ' Afficher les donn�es de capacit� si une p�riode de trafic est associ�e
      D�terminerAfficherCapacit�(monPlanFeuxFonctionnement)
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
#Region " Boutons g�n�raux"
  Private Sub btnCarrefour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarrefour.Click
    Dim dlg As New dlgCarGen
    Dim ctrl As Control

    With dlg
      .cboCarrefourType.Enabled = False
      .radD�grad�.Enabled = False
      .radGraphique.Enabled = False
      .mParamDessin = Me.mParamDessin
      If maVariante.VerrouG�om Then
        For Each ctrl In .grpModalit�s.Controls
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
        'Mettre � jour les donn�es du carrefour avec les donn�es saisies dans la boite
        .MettreAjour()
        Text = .mVariante.Libell�
        RedessinerFondDePlan()
        AfficherContexteFDP()
      End If

      .Dispose()
    End With

    '    RedessinerFondDePlan()

  End Sub

  Private Sub RedessinerFondDePlan()
    Dim ParamDessinModifi� As Boolean

    If Not IsNothing(monFDP) AndAlso IsNothing(maVariante.mFondDePlan) Then
      monFDP = Nothing
      maVariante.Cr�erGraphique(colObjetsGraphiques)
      Redessiner()
    Else
      monFDP = maVariante.mFondDePlan
    End If

    If Not IsNothing(monFDP) AndAlso monFDP.ADessiner Then
      Dim p As Point = PointDessin(maVariante.mCarrefour.mCentre)
      If Not PointDansPicture(p) Then
        Dim pMouseUp As Point = New Point(picDessin.Width / 2, picDessin.Height / 2)
        pMouseUp = Point.op_Subtraction(p, Point.op_Explicit(pMouseUp))
        mParamDessin = D�terminerNewOrigineR�ellePAN(pMouseUp)
        cndParamDessin = mParamDessin
        ParamDessinModifi� = True
      ElseIf Not mParamDessin.Equals(cndParamDessin) Then
        mParamDessin = cndParamDessin
        ParamDessinModifi� = True
      End If

      If ParamDessinModifi� Then
        mEchelles.Clear()
        mEchelles.Add(mEchelles.Count.ToString, mParamDessin)
      End If

      PositionnerCarrefour()
    End If

  End Sub

#End Region
#Region " Grilles"
  '**********************************************************************************************************************
  ' Indique si le style est interdit � la saisie
  '**********************************************************************************************************************
  Private Function StyleInterdit(ByVal unStyle As Grille.CellStyle) As Boolean
    Select Case unStyle.Name
      Case "Gris�", "Gris�Gras", "Gris�Bool�en", "Rouge", "Vert"
        StyleInterdit = True
    End Select
  End Function

#Region " Grille Branches"
  '******************************************************************************
  ' Valider les donn�es d'une cellule de la grille Branches
  '******************************************************************************
  Private Sub AC1grilleBranche_ValidateEdit(ByVal sender As System.Object, ByVal e As Grille.ValidateEditEventArgs) _
  Handles AC1GrilleBranches.ValidateEdit

    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en g�n�ral) ou une ComboBox(propri�t� ComboList)ou Nothing si CheckBox
    Dim uneBranche As Branche = maVariante.mBranches(CType(e.Row - 1, Short))
    Dim ARedessiner As Boolean
    '    Dim PassagesEtIlots As Boolean

    'La feuille est en cours de fermeture ou bascule d'une fen�tre carrefour � une autre
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
              D�finirPointsBranche(uneBranche)
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
              D�marrerCommande(CommandeGraphique.AucuneCommande)
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
              .RecalerPassagesPi�tons((.Largeur - exLargeur) / 2)
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
              .RecalerPassagesPi�tons((.Largeur - exLargeur) / 2)
            End If

          Case "Ilot"
            Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
            ' A ce stade, la case � cocher n'est pas encore mis � jour (sera fait dans CellChange!!!) : il faut donc inverser le bool�en lors du cochage
            If rg.Checkbox = Grille.CheckEnum.Checked Then
              e.Cancel = Not Confirmation("Supprimer l'ilot", Critique:=False)
            End If
        End Select
      End With

      ' L'ilot est trait� ensuite par CellChanged
      If ARedessiner Then

        'PassagesEtIlot : caduque � partir de la v11 (Juillet 2006)
        'If PassagesEtIlots Then
        '  'Colonne  "Largeur des voies" ou "nombre de voies" : red�finir l'ilot et supprimer les passages pi�tons
        '  'Ceci ne survient en principe que si la g�om�trie n'est pas verrouill�e (cf VerrouillerBoutonsG�om�trie)

        '  '1) Red�finir l'ilot �ventuel avec les valeurs par d�faut
        '  If uneBranche.AvecIlot Then
        '    Dim Index As Short = mesBranches.IndexIlot(uneBranche.mIlot)
        '    Dim unIlot As New Ilot(uneBranche)
        '    Dim fgIlot As GrilleDiagfeux = Me.AC1GrilleIlot
        '    'Rechercher la ligne de la grille adapt�e
        '    Dim rg As Grille.CellRange = fgIlot.TouteLaLigne(Index)
        '    'Afficher les donn�es dans la ligne
        '    rg.Clip = unIlot.strLigneGrille(mesBranches, S�parateur:=Chr(9))
        '  End If

        '  '2)Supprimer les passages pi�tons
        '  uneBranche.mPassages.Clear()
        'End If

        RedessinerBranche(uneBranche)
        uneBranche.D�terminerVoiesPassages()
        If Not SelectObject Then S�lD�s�lectionner() ' Montre ou cache les poign�es de s�lection
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
  Handles AC1GrilleBranches.KeyPressEdit, Ac1GrilleS�curit�.KeyPressEdit, AC1GrilleIlot.KeyPressEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomChamp As String = fg.Cols(e.Col).Name

    With e

      Select Case QuelType(fg.Cols(e.Col).DataType)
        Case Outils.DataTypeEnum.typeSingle
          .Handled = ToucheNonNum�rique(e.KeyChar, Entier:=False)
          ''Si on frappe le point d�cimal et que les param�tres r�gionaux comportent une autre valeur que le point d�cimal comme s�parateur, 
          '' celui-ci est refus� par la fonction pr�c�dente : on remplace le point d�cimal par le caract�re sp�cifique r�gional
          'If .KeyChar = "."c And .Handled Then SendKeys.Send(cndPtD�cimal)
        Case Outils.DataTypeEnum.typeInt16
          .Handled = ToucheNonNum�rique(.KeyChar)
      End Select
    End With

  End Sub

  '******************************************************************************
  ' Grille Branches  : CellChanged
  ' Traitement de la case � cocher Ilot
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
            DessinerObjet(unIlot.Cr�erGraphique(colObjetsGraphiques))
          End If
          fgIlot.Rows.Insert(Index)
          fgIlot(Index, 0) = mesBranches.ID(maBranche)
          'Rechercher la ligne de la grille adapt�e
          rg = fgIlot.TouteLaLigne(Index)
          'Afficher les donn�es dans la ligne
          rg.Clip = unIlot.strLigneGrille(mesBranches, S�parateur:=Chr(9))

          If ChargementEnCours Then SelectObject = False

        Else
          unIlot = maBranche.mIlot
          'Supprimer la ligne du tableau d'ilots
          fgIlot.Rows.Remove(mesBranches.IndexIlot(unIlot))

          'Supprimer l'ilot des objets graphiques
          maBranche.SupprimerIlot(colObjetsGraphiques)

          'Redessiner la branche
          RedessinerBranche(maBranche)
          'Redessiner � effacer les poign�es de la branche si elle �tait s�lectionn�e
          objS�lect = Nothing

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
      'Si le style est gris�, on interdit l'acc�s � la cellule
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

    'La ligne s�lectionn�e peut �tre = -1 ==> IsValid=false
    If e.NewRange.IsValid Then
      'Passer en mode saisie sauf poul l'ilot (case � cocher que l'instruction ferait basculer)
      If rgNew.c1 < 7 Then fg.StartEditing()

      If rgOld.r1 <> rgNew.r1 And Not SelectObject Then
        D�s�lectionner()
        objS�lect = mesBranches(rgNew.r1 - 1).mGraphique
        S�lD�s�lectionner() ' Montre ou cache les poign�es de s�lection
      End If
    End If

  End Sub
#End Region
#Region " Grille Ilot"
  Private Sub AC1GrilleIlot_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles AC1GrilleIlot.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim ModifIlot As Boolean

    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en g�n�ral) ou une ComboBox(propri�t� ComboList)ou Nothing si CheckBox
    Dim Index As Short = e.Row
    Dim unIlot As Ilot = mesBranches.IlotBranche(Index)
    'La feuille est en cours de fermeture ou bascule d'une fen�tre carrefour � une autre
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

          Case "D�calage"
            If .D�calage = Valeur Then
              e.Cancel = ControlerBornes(Me, Ilot.miniLargeur, .mBranche.Largeur, Controle, .D�calage, unFormat:="0.0")
            End If
            If Not e.Cancel Then
              .D�calage = Valeur
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
          unIlot.Cr�erGraphique(colObjetsGraphiques)
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

    'La ligne s�lectionn�e peut �tre = -1 ==> IsValid=false
    If e.NewRange.IsValid Then
      'Passer en mode saisie 
      If Not IsNothing(rgNew.Data) Then fg.StartEditing()

      If rgOld.r1 <> rgNew.r1 And rgNew.r1 > 0 And Not SelectObject Then
        D�s�lectionner()
        objS�lect = mesBranches.IlotBranche(rgNew.r1).mGraphique
        S�lD�s�lectionner()   ' Montre ou cache les poign�es de s�lection
      End If
    End If

  End Sub
#End Region
#Region " Grille Trafic"
  '******************************************************************************
  ' Interdire l'acc�s � la saisie des trafics totaux (qui sont calcul�s par  DIAGFEUX)
  '******************************************************************************
  Private Sub GrilleTrafics_BeforeRowColChange(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _
  Ac1GrilleTraficPi�tons.BeforeRowColChange, AC1GrilleTraficV�hicules.BeforeRowColChange

    Dim unStyle As Grille.CellStyle
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange
    Dim col As Short

    If e.NewRange.IsValid Then unStyle = e.NewRange.Style

    If Not IsNothing(unStyle) Then
      'Si le style est gris�, on interdit l'acc�s � la cellule
      e.Cancel = StyleInterdit(unStyle)
    End If

    If Not fg.Cols(e.NewRange.c1).AllowEditing Then e.Cancel = True

    If Not e.Cancel And e.OldRange.IsValid Then
      'Lignes suivantes supprim�es (AV : 7/6/06 ) Ne sert � rien a priori. De + r�active la cellule 1,1 quand on clique dans la grille(il ne faut pas si branche A est sens unique)
      ' rg = e.OldRange
      'rg.Style = fg.Styles("Normal")
    End If

  End Sub

  Private Sub AC1GrilleTraficV�hicules_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleTraficV�hicules.AfterRowColChange, Ac1GrilleTraficPi�tons.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    fg.StartEditing()
  End Sub
  Private Sub AC1GrilleTrafics_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles Ac1GrilleTraficPi�tons.KeyPressEdit, AC1GrilleTraficV�hicules.KeyPressEdit
    '******************************************************************************
    ' Grilles Trafics : KeyPressEdit
    '******************************************************************************
    Dim fg As GrilleDiagfeux = sender
    Dim NomChamp As String = fg.Cols(e.Col).Name

    'Donn�e trafic
    e.Handled = ToucheNonNum�rique(e.KeyChar)

  End Sub

  '******************************************************************************
  ' Grilles Trafics : ValidateEdit
  '******************************************************************************
  Private Sub AC1GrilleTrafics_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) _
  Handles Ac1GrilleTraficPi�tons.ValidateEdit, AC1GrilleTraficV�hicules.ValidateEdit

    Dim fg As GrilleDiagfeux = sender
    Dim Controle As Control = fg.Editor
    Dim i, j As Short
    Dim ligneTotal As Short = mesBranches.Count + 1
    Dim colonneTotal As Short = ligneTotal
    Dim Index As Trafic.TraficEnum = IndexTrafic()

    'La feuille est en cours de fermeture ou bascule d'une fen�tre carrefour � une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If IsNothing(monTraficActif) Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub

    If Controle.Text = "" Then Controle.Text = "0"

    e.Cancel = ControlerBornes(Me, 0, Trafic.vMaxi, Controle, Nothing, unFormat:="0")

    Try

      If Not e.Cancel Then
        Dim rgCellule As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
        If fg Is Me.AC1GrilleTraficV�hicules Then
          'Affecter le nouveau trafic
          monTraficActif.QV�hicule(Index, e.Row - 1, e.Col - 1) = CType(Controle.Text, Short)
          'Afficher le nouveau trafic entrant sur cette branche
          fg(e.Row, colonneTotal) = monTraficActif.QE(Index, e.Row - 1)
          'Afficher le nouveautrafic sortant par cette branche
          fg(ligneTotal, e.Col) = monTraficActif.QS(Index, e.Col - 1)
          'Afficher le nouveautrafic total du carrefour
          fg(mesBranches.Count + 1, mesBranches.Count + 1) = monTraficActif.QTotal(Index)
        Else
          'Afficher le nouveau trafic pi�ton sur cette branche
          monTraficActif.QPi�ton(e.Col) = CType(Controle.Text, Short)
        End If

        Modif = True
        AfficherTraficSatur�()
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  Private Sub AfficherTraficSatur�()
    Dim uneBranche As Branche
    Dim rg As Grille.CellRange

    Me.lblTaficSatur�.Visible = False
    If maVariante.Verrou <= [Global].Verrouillage.LignesFeux Then
      For Each uneBranche In mesBranches
        rg = Me.AC1GrilleTraficV�hicules.GetCellRange(mesBranches.IndexOf(uneBranche) + 1, mesBranches.Count + 1)
        If Not uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) AndAlso _
        monTraficActif.QE(Trafic.TraficEnum.UVP, mesBranches.IndexOf(uneBranche)) / uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) > maVariante.D�bitSaturation Then
          Me.lblTaficSatur�.Visible = True
          rg.Style = StyleGris�Rouge
        Else
          rg.Style = StyleGris�
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
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en g�n�ral) ou une ComboBox(propri�t� ComboList) ou Nothing (checkbox)
    Dim Arr�t As Boolean
    Dim uneLigneFeux, uneLF As LigneFeux
    Dim ValeurModifi�e As String
    Dim Bool�en As Boolean
    Dim Derni�reLigne As Boolean = (e.Row = fg.Rows.Count - 1)
    Dim unStyle As Grille.CellStyle
    Dim unSignal As Signal
    Dim rg As Grille.CellRange
    Dim ValeurCourante As String
    Dim BasculePi�tonsV�hicules As Boolean
    Dim Message As String

    ' Jusqu'� la v11, on pouvait avoir le message suivant lors de l'appel de la fonction 'LigneDeFeux.MettreAJour'
    'Cast de la cha�ne "Voies" en type 'Short' non valide. ---> System.FormatException: Le format de la cha�ne d'entr�e est incorrect.

    'e.Row = 0 peut parfois arriver sur une suppression de ligne de feux
    If e.Row = 0 Then Exit Sub

    'La 1�re ligne de de feux existe dans la grille d�s le d�part, mais peut �tre vide(il n'y a pas encore de lignes de feux)
    'dans ce cas elle est invisible et il faut l'ignorer
    If e.Row = 1 And Not fg.Rows(1).Visible Then Exit Sub

    'La feuille est en cours de fermeture ou bascule d'une fen�tre carrefour � une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If fg.Cols(fg.Col).AllowEditing = False Then e.Cancel = True : Exit Sub
    If D�calageFeuxEnCours Then Exit Sub

    Try
      Dim rgCellule As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

      If IsNothing(Controle) Then
        'Case � cocher pour les TAG,TAD...
        unStyle = rgCellule.Style
        If IsNothing(unStyle) Then   'Cas g�n�ral
          Bool�en = (e.Checkbox = Grille.CheckEnum.Checked)
          Arr�t = (e.Checkbox = Grille.CheckEnum.None)
        Else
          'Le style est gris� si Mode graphique ou en cas de ligne de feux pi�tons
          Arr�t = StyleInterdit(unStyle)
        End If
        e.Cancel = Arr�t

      Else
        ValeurModifi�e = Controle.Text
        Arr�t = (ValeurModifi�e.Length = 0)
      End If

      If Not Arr�t Then
        'Controles de 1er niveau 
        Select Case NomColonne
          Case "IDVoie"
          Case "NomRue"

          Case "ID"
            'R�cup�rer la valeur courante de l'ID
            ValeurCourante = rgCellule.Data

            If ValeurModifi�e.Length > 2 Then
              Message = "Le nom de la ligne de feux ne doit pas d�passer 2 caract�res"

            Else
              'D�tecter si une ligne de feux de m�me ID n'existe pas d�j� pour une ligne de feux
              With mesLignesFeux
                If .Contains(ValeurModifi�e) Then
                  'Ce  n'est pas un probl�me s'il s'agit de la m�me ligne (i.e. : l'ID n'est en fait pas modifi�)
                  If e.Row <> .IndexOf(.Item(ValeurModifi�e)) + 1 Then
                    Message = "Nom de feu existant"
                  End If
                End If
              End With

              If Not ModeGraphique And Not Derni�reLigne And IsNothing(Message) Then
                'V�rifier que cet ID n'est pas pris non plus par la ligne de feux en cours de cr�ation (derni�re ligne de la grille)
                rg = fg.GetCellRange(fg.Rows.Count - 1, e.Col)
                If ValeurModifi�e = rg.Data Then
                  Message = "Nom de feu existant"
                End If
              End If
            End If

            If Not IsNothing(Message) And ValeurCourante <> ValeurModifi�e Then
              MessageBox.Show(Me, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
              rgCellule.Clear(Grille.ClearFlags.Content)
              'R�tablir la valeur m�moris�e lors de StartEdit(en fait ne fonctionne pas)
              rgCellule.Data = strSauveGrille
              e.Cancel = True
            End If

          Case "Signal"
            unSignal = cndSignaux(ValeurModifi�e)
            'Pointer sur la colone Signal Anticipation : celle-ci est gris�e si Ligne Pi�tons
            rg = fg.GetCellRange(e.Row, 4)
            BasculePi�tonsV�hicules = unSignal.EstPi�ton Xor (rg.Style.Name = StyleGris�.Name Or rg.Style.Name = StyleGris�Gras.Name)
            If BasculePi�tonsV�hicules Then
              If maVariante.VerrouLigneFeu Then
                MessageBox.Show(Me, "Le passage d'un feu pi�ton � un feu v�hicule ou inversement est interdit", NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
              ElseIf Not Derni�reLigne Then
                uneLigneFeux = mesLignesFeux(CType(e.Row - 1, Short))
                If uneLigneFeux.ToutesVoiesSurBranche Then
                  Message = "Branche � sens unique : elle doit comporter au moins une ligne de feux"
                  e.Cancel = True
                End If
              End If
              If e.Cancel Then
                MessageBox.Show(Me, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'R�tablir la valeur m�moriser lors de StartEdit
                rgCellule.Clip = strSauveGrille
              End If
            End If

          Case "NbVoies"
              e.Cancel = ControlerBornes(Me, 1, 4, Controle, Nothing, unFormat:="0")

          Case "SignalAnticipation"
          Case "TAD", "TD", "TAG"

        End Select

        If Not e.Cancel Then
          'Traitements compl�mentaires
          'Mise � jour de la ligne de feux
          uneLigneFeux = mesLignesFeux.MettreAjour(ValeurModifi�e, e.Checkbox = Grille.CheckEnum.Checked, fg.strLigneEnti�re(e.Row), e.Row - 1, e.Col)
          Dim MajBranche As Boolean = BasculePi�tonsV�hicules

          If Not IsNothing(uneLigneFeux) Then
            'En mode tableur : Rajout automatique d'une ligne vierge 
            'd�s que les infos minimales ont permis de cr�er la ligne de feu ou encore bascule ligne v�hicules/pi�tons
            If Not ModeGraphique Then
              If Derni�reLigne Then
                fg.Rows.Add()
                MajBranche = True
              ElseIf NomColonne = "NbVoies" Then
                MajBranche = True
              End If
            End If
          End If

          'Mise � jour des donn�es corr�l�es
          Select Case NomColonne
            Case "IDVoie"
              'Mettre � jour le nom de la voie en fonction du nom de la branche
              rg = fg.GetCellRange(e.Row, 1)
              rg.Data = mesBranches(CType(ValeurModifi�e, Char)).NomRue

            Case "NomRue"
              'Mettre � jour le nom de la voie pour toutes les lignes relatives � la m�me branche
              Dim IDVoie As Char
              Dim row As Short

              rg = fg.GetCellRange(e.Row, 0)
              IDVoie = rg.Data
              For row = 1 To fg.Rows.Count - 1
                rg = fg.GetCellRange(row, 0, row, 1)
                If rg.Data = IDVoie Then rg.Clip = IDVoie & fg.ClipSeparators.Chars(0) & ValeurModifi�e
              Next
              With Me.AC1GrilleBranches
                Dim uneBranche As Branche = mesBranches(IDVoie)
                rg = .GetCellRange(mesBranches.IndexOf(uneBranche) + 1, 1)
                rg.Data = ValeurModifi�e
              End With

            Case "ID"
              If ModeGraphique Then
                EffacerObjet(uneLigneFeux.mSignalFeu(0).mGraphique)
                If uneLigneFeux.EstPi�ton AndAlso CType(uneLigneFeux, LigneFeuPi�tons).SignalARepr�senter(1) Then
                  EffacerObjet(uneLigneFeux.mSignalFeu(1).mGraphique)
                End If
                uneLigneFeux.Cr�erGraphique(colObjetsGraphiques)
                DessinerLigneDeFeux(uneLigneFeux)
              End If
              If ValeurCourante <> ValeurModifi�e Then
                AfficherCons�quencesModifLignesDeFeux(SuiteAD�calage:=False)
                RenommerLignePlansFeux(uneLigneFeux, ValeurCourante)
              End If

            Case "Signal"
              'S�lectionner les cellules concern�es en cas de ligne de feux v�hicules seulement
              GriserLignePi�tons(fg, e.Row, unSignal.EstPi�ton)

            Case "SignalAnticipation"
            Case "NbVoies"
            Case "TAD", "TD", "TAG"
          End Select

          If MajBranche Then
            'V13 (AV : 10/01/07) : en cas de bascule v�hicules pi�tons : le nb de voies reste affich� et les TAD,TAG restent coch�s
            'Par ailleurs, pour une nouvelle ligne v�hicules, il est int�ressant d'initialiser � 1 le nombre de voies
            rg = fg.GetCellRange(e.Row, 0, e.Row, fg.Cols.Count - 1)
            rg.Clip = uneLigneFeux.strLigneGrille(mesBranches, S�parateur:=Chr(9))

            'DIAGFEUX 3 : les voies entrantes sont d�duites des voies des lignes de feux
            MettreAJourVoiesBranches()
          End If

          Modif = True
        End If  'not e.cancel

      End If  ' Not Arr�t

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

      Recr�erGraphique()
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
      'Si le style est gris�, on interdit l'acc�s � la cellule
      e.Cancel = StyleInterdit(unStyle)
    End If

    If Not fg.Cols(e.NewRange.c1).AllowEditing Then
      e.Cancel = True
      'La tentative de s�lectionner la ligne plante en d�bordement de pile (le try catch ne fonctione pas
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
        'Si on ouvre la combo IDVoie sans rien s�lectionner, rg.data n'est plus � Nothing
        e.Cancel = True
      End If
    End If

    If Not e.Cancel And e.OldRange.IsValid Then
      'R�tablir les styles non gras pour les anciens objets s�lectionn�s (cf �v�nement SelChange)
      For col = 0 To fg.Cols.Count - 1
        rg = fg.GetCellRange(e.OldRange.r1, col)
        If IsNothing(rg.Style) Then
        ElseIf rg.Style.Name = StyleGris�Gras.Name Then
          rg.Style = StyleGris�
        ElseIf rg.Style.Name = StyleD�gris�Gras.Name Then
          rg.Style = StyleD�gris�
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

    'La ligne s�lectionn�e peut �tre = -1 ==> IsValid=false
    If e.NewRange.IsValid And Not D�calageFeuxEnCours Then
      'Passer en mode saisie pour les champs :nom de la rue, nom du feu et nombre de voies (et surtout pas les cases � cocher que l'instruction ferait basculer)
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
        D�s�lectionner()
        Dim uneLigneFeux As LigneFeux = mesLignesFeux(CType(rgNew.r1 - 1, Short))
        If uneLigneFeux.EstV�hicule Then
          objS�lect = uneLigneFeux.mGraphique
        Else
          'Mettre en valeur la travers�e pi�tonne correspondant � la ligne de feux pi�tons
          objS�lect = CType(uneLigneFeux, LigneFeuPi�tons).mTravers�e.mGraphique
        End If

        S�lD�s�lectionner() ' Montre ou cache les poign�es de s�lection
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
        e.Handled = ToucheNonNum�rique(e.KeyChar)
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
  ' D�terrminer l'activabilit� des boutons selon la s�lection
  '******************************************************************************
  Private Sub AC1GrilleFeux_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles AC1GrilleFeux.SelChange
    Dim fg As GrilleDiagfeux = sender
    Dim col As Short
    Dim rg As Grille.CellRange

    If fg.Row = -1 Then
      'Aucune ligne s�lectionn�e
      Me.btnLigneFeuxMoins.Enabled = ModeGraphique And mesLignesFeux.nbLignesV�hicules > 0 And maVariante.Verrou = [Global].Verrouillage.G�om�trie
      Me.btnLigneFeuDescendre.Enabled = False
      Me.btnLigneFeuMonter.Enabled = False

    Else
      With fg
        Dim MaxRow As Short
        If ModeGraphique Or maVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
          MaxRow = .Rows.Count
        Else
          'En mode tableur, il y a toujours une ligne suppl�mentaire pour permettre la saisie 
          ' d'une nouvelle ligne de feux tant que celles ci ne sont pas verrouill�es
          MaxRow = .Rows.Count - 1
        End If
        'D�terminer si la ligne peut monter, descendre ou �tre supprim�e
        Me.btnLigneFeuxMoins.Enabled = .Row < MaxRow And maVariante.Verrou = [Global].Verrouillage.G�om�trie
        Me.btnLigneFeuMonter.Enabled = .Row < MaxRow And .Row > 1
        Me.btnLigneFeuDescendre.Enabled = .Row < MaxRow - 1
        'Mettre en gras les cellules de la ligne s�lectionn�e 
        For col = 0 To .Cols.Count - 1
          rg = .GetCellRange(.Row, col)
          If IsNothing(rg.Style) Then
            rg.Style = StyleD�gris�Gras
          ElseIf rg.Style.Name = StyleGris�.Name Then
            'conserver le gris� existant
            rg.Style = StyleGris�Gras
          Else
            rg.Style = StyleD�gris�Gras
          End If
        Next

      End With

    End If
  End Sub

  '**********************************************************************************************************************
  ' StartEdit : l'utilisateur commence � �diter la ligne de feux - m�moriser la valeur pr�c�dente
  '**********************************************************************************************************************
  Private Sub AC1GrilleFeux_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles AC1GrilleFeux.StartEdit
    Dim fg As GrilleDiagfeux = Me.AC1GrilleFeux
    Dim rg = fg.GetCellRange(e.Row, e.Col)
    Dim NomColonne As String = fg.Cols(e.Col).Name

    'On ne m�morise que les signaux de feux
    If NomColonne = "ID" Or NomColonne = "Signal" Then strSauveGrille = rg.data

    If NomColonne = "Signal" And ModeGraphique Then
      'Peut arriver par double click sur un signal pi�ton : il faut emp�cher le d�roulement de la liste des signaux
      Dim uneLigneFeux As LigneFeux = mesLignesFeux(CType(rg.r1 - 1, Short))
      If uneLigneFeux.EstPi�ton Then e.Cancel = True
    End If

  End Sub

#End Region
#Region " Grille s�curit�"
  '**********************************************************************************************************************
  ' Interdire l'�dition d'une celule si son style l'interdit
  '**********************************************************************************************************************
  Private Sub Ac1GrilleS�curit�_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles Ac1GrilleS�curit�.BeforeEdit
    Dim fg As GrilleDiagfeux = sender
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
    Dim unStyle As Grille.CellStyle = rg.Style

    If Not IsNothing(unStyle) Then
      'Si le style est gris�, on interdit l'acc�s � la cellule
      e.Cancel = StyleInterdit(unStyle)
      If Not e.Cancel Then btnRougeD�faut.Enabled = Me.radMatriceRougesD�gagement.Checked
    End If

  End Sub

  '**********************************************************************************************************************
  ' Click sur un point de conflit dans la matrice de s�curit�
  ' Basculer la case de rouge en vert ou inversement, ainsi que la case sym�trique
  '**********************************************************************************************************************
  Private Sub Ac1GrilleS�curit�_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ac1GrilleS�curit�.Click

    If Me.radMatriceConflits.Checked Then
      ' Onglet matrice des conflits

      Dim fg As GrilleDiagfeux = sender
      Dim rg As Grille.CellRange = fg.CelluleS�lectionn�e

      If rg.IsValid AndAlso rg.c1 > 0 Then
        Try

          Dim row As Integer = rg.r1
          Dim col As Integer = rg.c1
          Dim unAntagonisme As Antagonisme

          ' lHorizontale d�signe la row de feux horizontale
          ' lVerticale d�signe la col de feux verticale
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
                  Case Trajectoire.TypeConflitEnum.Syst�matique
                    msg = "Ces lignes de feux sont strictement incompatibles"
                  Case Trajectoire.TypeConflitEnum.NonAdmis
                    msg = "Ces lignes de feux sont incompatibles suite � une d�cision pr�c�dente sur les conflits" & vbCrLf & _
                           ListeNonAdmis & vbCrLf & "Rendre d'abord le(s) conflit admis"
                  Case Trajectoire.TypeConflitEnum.Admissible
                    msg = "Ces lignes de feux sont incompatibles tant que toutes les d�cisions n'ont pas �t� prises sur les conflits" & vbCrLf & _
                          "Rendre d'abord le conflit admis"
                  Case Else
                    Dim lVeh As LigneFeuV�hicules
                    Dim lPi�tons As LigneFeuPi�tons

                    If (lHorizontale.EstPi�ton Or lVerticale.EstPi�ton) Then
                      'V�rifier si les antagonismes li�s ont �t� r�solus individuellement
                      If lHorizontale.EstV�hicule Then
                        lVeh = lHorizontale
                        lPi�tons = lVerticale
                      Else
                        lVeh = lVerticale
                        lPi�tons = lHorizontale
                      End If
                      Dim lv2 As LigneFeuV�hicules = lVeh.LigneFeuxLi�e(lPi�tons)
                      If Not IsNothing(lv2) Then
                        msg = "Ces lignes ne peuvent pas �tre compatibles car " & lVeh.ID & " et " & lv2.ID & " ne le sont pas"
                      End If
                    End If

                End Select
              End If

              If IsNothing(msg) Then
                rg.Style = StyleVert
                'Basculer aussi la cellule sym�trique
                rg = fg.GetCellRange(col, row)
                rg.Style = StyleVert
                mLignesFeux.EstIncompatible(lHorizontale, lVerticale) = False

              Else
                AfficherMessageErreur(Me, msg)
              End If

            Case "Vert"
              rg.Style = StyleRouge
              'Basculer aussi la cellule sym�trique
              rg = fg.GetCellRange(col, row)
              rg.Style = StyleRouge
              mLignesFeux.EstIncompatible(lHorizontale, lVerticale) = True

              If ModeGraphique Then
                'Basculer �galement les conflits admissibles sans d�cision ou Admis dans un 1er temps (Admissible/Admis --> NonAdmis)
                For Each unAntagonisme In mAntagonismes()
                  With unAntagonisme
                    If .AntagonismeLi�(lHorizontale, lVerticale) And .Admissible Then
                      MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.NonAdmis)
                    End If
                  End With
                Next
              End If

            Case "Orang�"
              AfficherMessageErreur(Me, "Ce conflit doit �tre r�solu par la gestion des antagonismes")
              Dim IndexAntago As Short
              Dim IndexAD�finir As Short = -1
              Dim BrancheCherch�e As Branche
              Dim IndexBranche As Short

              For Each unAntagonisme In mAntagonismes()
                With unAntagonisme
                  If .AntagonismeLi�(lHorizontale, lVerticale) AndAlso .TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
                    BrancheCherch�e = .Courant(Antagonisme.PositionEnum.Premier).Branche(TrajectoireV�hicules.OrigineDestEnum.Origine)
                    IndexAntago = mAntagonismes.IndexOf(unAntagonisme)
                    If maVariante.BrancheEnCoursAntagonisme Is BrancheCherch�e Then
                      IndexAD�finir = -1
                      Exit For
                    Else
                      If IndexAD�finir = -1 Then
                        IndexAD�finir = IndexAntago
                        IndexBranche = mesBranches.IndexOf(BrancheCherch�e)
                      End If
                    End If
                  End If
                End With
              Next
              If IndexAD�finir <> -1 Then
                IndexAntago = IndexAD�finir
                If Me.cboBrancheCourant1.SelectedIndex <> mesBranches.Count Then Me.cboBrancheCourant1.SelectedIndex = IndexBranche
              End If
              Me.AC1GrilleAntagonismes.Select(IndexAntago + 1, 0)

          End Select

          'Pour �viter que + tard la cellule s�lectionn�e se rallume intempestivement
          Me.Ac1GrilleS�curit�.Row = -1
          Me.Ac1GrilleS�curit�.Col = -1

        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        End Try

      End If  ' rg.IsValid
    End If    ' Matrice de s�curit� = Matrice des conflits

  End Sub

  '**********************************************************************************************************************
  ' Emp�cher que le double click passe en mode saisie
  '**********************************************************************************************************************
  Private Sub Ac1GrilleS�curit�_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ac1GrilleS�curit�.DoubleClick
    Dim fg As GrilleDiagfeux = sender
    SendKeys.Send("{ESC}")
  End Sub

  '**********************************************************************************************************************
  ' Validation d'une cellule de la grille des matrices de s�curit� (rouges de d�gagement seulement)
  '**********************************************************************************************************************
  Private Sub Ac1GrilleS�curit�_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles Ac1GrilleS�curit�.ValidateEdit
    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name
    Dim Controle As Control = fg.Editor  'Controle est une TextBox(en g�n�ral) ou une ComboBox(propri�t� ComboList) ou Nothing (checkbox)
    Dim Arr�t As Boolean

    Dim uneBranche As Branche
    Dim uneLigneFeux As LigneFeux
    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)
    Dim ValeurModifi�e As String
    Dim Bool�en As Boolean
    Dim unStyle As Grille.CellStyle

    'La feuille est en cours de fermeture ou bascule d'une fen�tre carrefour � une autre
    If Not mdiApplication.ActiveMdiChild Is Me Then Exit Sub
    If Not fg.Cols(fg.Col).AllowEditing Then e.Cancel = True : Exit Sub

    Try
      Dim lh, lv As LigneFeux
      ' lh d�signe la ligne de feux horizontale (au sens matriciel)
      ' lv d�signe la ligne de feux verticale
      lh = mLignesFeux(CType(e.Row - 1, Short))
      lv = mLignesFeux(CType(e.Col - 1, Short))

      'La valeur par d�faut du rouge de d�gagement du plan de feux de base 
      'est celui calcul� comme rouge mini pour les lignes de feux de la variante (cf D�terminerTempsD�gagement)
      Dim TempsMini As Short = mesLignesFeux.RougeD�gagement(lh, lv)
      Dim TempsActuel As Short = mLignesFeux.TempsD�gagement(lh, lv)
      Dim TempsNouveau As Short = Short.Parse(Controle.Text)
      Dim unFormat As String = "0"

      e.Cancel = ControlerBornes(Me, 0, LigneFeux.MaxiRougeD�gagement, Controle, mLignesFeux.TempsD�gagement(lh, lv), unFormat:=unFormat)

      If Not e.Cancel AndAlso TempsNouveau < TempsMini Then
        Dim Message As String = "La dur�e du rouge de d�gagement ne devrait pas �tre inf�rieure � " & Format(TempsMini, unFormat)
        Message &= vbCrLf & "Confirmez-vous cette valeur ?"
        e.Cancel = Not Confirmation(Message, Critique:=True)
        If e.Cancel Then Controle.Text = TempsActuel
      End If

      If Not e.Cancel Then
        mLignesFeux.TempsD�gagement(lh, lv) = TempsNouveau
        'tant que le phasage n'est pas retenu, on peut modifier les rouges de d�gagement 
        'les dur�es mini sont �  recalculer(et donc les capacit�s)
        monPlanFeuxBase.CalculerCapacit�sPlansPhasage()

        AfficherRouge(lh, lv, rg, fg)
        ActiverBoutonsRouges()

        ' La modification des rouges de d�gagement est possible tant que le phasage n'est pas retenu
        ' Le nouveau calcul des capacit�s peut influer sur l'affichage de l'organisation du phasage
        If monPlanFeuxBase.AvecTrafic AndAlso Not IsNothing(monPlanPourPhasage) Then
          D�terminerAfficherCapacit�(monPlanPourPhasage)
        End If
        Modif = True
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try

  End Sub

  '*******************************************************************************************************
  'Activer la r�initialisation des rouges de d�gagement si au moins un rouge n'a pas la valeur par d�faut
  '*******************************************************************************************************
  Private Sub ActiverBoutonsRouges()
    Dim lh, lv As LigneFeux
    Dim Activ� As Boolean

    For Each lh In mLignesFeux()
      For Each lv In mLignesFeux()
        If Not lh.EstTrivialementCompatible(lv) Then

          If mLignesFeux.TempsD�gagement(lh, lv) <> mesLignesFeux.RougeD�gagement(lh, lv) Then
            Activ� = True
          End If
        End If
      Next
    Next

    Me.btnRougesD�faut.Enabled = Activ�

  End Sub

  Private Sub AfficherRouge(ByVal lh As LigneFeux, ByVal lv As LigneFeux, ByVal rg As Grille.CellRange, ByVal fg As GrilleDiagfeux)
    ' lh d�signe la ligne de feux horizontale (au sens matriciel)
    ' lv d�signe la ligne de feux verticale

    If mLignesFeux.TempsD�gagement(lh, lv) <> mesLignesFeux.RougeD�gagement(lh, lv) Then
      rg.Style = StyleSaisieItalique
    Else
      rg.Style = fg.Styles(Grille.CellStyleEnum.Normal)
    End If
    rg.Data = mLignesFeux.TempsD�gagement(lh, lv)

  End Sub
#End Region
#Region " Grille Antagonismes"
  Private Sub AC1GrilleAntagonismes_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles AC1GrilleAntagonismes.AfterRowColChange
    Dim fg As GrilleDiagfeux = sender
    Dim rgOld As Grille.CellRange = e.OldRange
    Dim rgNew As Grille.CellRange = e.NewRange

    Debug.WriteLine("rowcolchange")
    'La ligne s�lectionn�e peut �tre = -1 ==> IsValid=false
    If e.NewRange.IsValid Then

      If Not SelectObject Then
        If rgOld.r1 <> rgNew.r1 Then
          D�s�lectionner()
          objS�lect = mAntagonismes(rgNew.r1 - 1).mGraphique
          S�lD�s�lectionner() ' Montre ou cache les poign�es de s�lection
          D�marrerCommande(CommandeGraphique.Antagonisme)

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
    Visibilit�Fen�treAntagonisme()
  End Sub
  Private Sub Visibilit�Fen�treAntagonisme()
    If Not Me.FenetreAntagonisme.Visible AndAlso Not IsNothing(objS�lect) AndAlso TypeOf objS�lect.ObjetM�tier Is Antagonisme Then
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
      ' A ce stade, la case � cocher n'est pas encore mis � jour (sera fait dan CellChange!!!) : il faut donc inverser le bool�en lors du cochage
      Dim Admis As Boolean = (rg.Checkbox = Grille.CheckEnum.Unchecked)

      Try
        e.Cancel = AntagonismeLi�Refus�(unAntagonisme, Admis, AppelDepuisGrille:=True)

        If e.Cancel AndAlso unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
          Me.FenetreAntagonisme.radNon.Checked = True
        End If


      Catch ex As System.Exception
        AfficherMessageErreur(Me, ex)
      End Try

    End If

  End Sub

  '*******************************************************************************************************************
  ' Grille Antagonismes : Basculement case � cocher Admis , Non Admis
  '*******************************************************************************************************************
  Private Sub AC1GrilleAntagonismes_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles AC1GrilleAntagonismes.CellChanged
    ' Ignorer les changements lors de la construction de la grille
    If AntagonismesEnCours Then Exit Sub

    Dim fg As GrilleDiagfeux = sender
    Dim NomColonne As String = fg.Cols(e.Col).Name

    If NomColonne <> "ConflitAdmis" Then Exit Sub

    Dim rg As Grille.CellRange = fg.GetCellRange(e.Row, e.Col)

    Dim unStyle As Grille.CellStyle = rg.Style
    If unStyle.Name = "Orang�" Then rg.Style = StyleD�gris�
    Dim unAntagonisme As Antagonisme = mAntagonismes(e.Row - 1)
    Dim l1, l2 As LigneFeux
    Static BoucleEnCours As Boolean

    Try

      Dim Admis As Boolean = (rg.Checkbox = Grille.CheckEnum.Checked)
      Me.btnR�initAntago.Enabled = True

      With unAntagonisme
        'Mettre � jour l'antagonisme
        Me.FenetreAntagonisme.mAntagonisme = Nothing
        If Admis Then
          .TypeConflit = Trajectoire.TypeConflitEnum.Admis
          Me.FenetreAntagonisme.radOui.Checked = True
        Else
          .TypeConflit = Trajectoire.TypeConflitEnum.NonAdmis
          Me.FenetreAntagonisme.radNon.Checked = True
        End If
        Visibilit�Fen�treAntagonisme()
        Me.FenetreAntagonisme.mAntagonisme = unAntagonisme
        'Recr�er l'image graphique de l'antagonisme
        .Cr�erGraphique(colObjetsGraphiques)
        If IsNothing(maVariante.BrancheEnCoursAntagonisme) OrElse maVariante.BrancheEnCoursAntagonisme Is .BrancheCourant1 Then
          DessinerObjet(.mGraphique)
        End If

        'D�finir les lignes de feux qui correspondent aux courants en conflit dans cet antagonisme
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

      'Mettre � jour �galement les autres conflits portant sur les m�mes courants(qui ne sont pas visibles dans la grille)
      For Each bclAntago In mAntagonismes()
        If (Not (bclAntago Is unAntagonisme)) AndAlso bclAntago.M�mesCourants Is unAntagonisme Then
          MettreAJourConflit(bclAntago, unAntagonisme.TypeConflit)
        End If
      Next

      If mLignesFeux.EstIncompatible(l1, l2) Then
        unStyle = StyleRouge
        ' L'incompatiblilit� TD/TAG conduit � l'incompatiblit� TAG/Pi�tons
        For Each unAntagonisme In mAntagonismes.Fils(unAntagonisme)
          MettreAJourConflit(unAntagonisme, Trajectoire.TypeConflitEnum.NonAdmis)
        Next

      Else
        unStyle = StyleVert
      End If

      fg = Me.Ac1GrilleS�curit�
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
  'La cellule a chang� de valeur : D�terminer si ce ne g�n�re pas des conflits
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


        'Traiter la colonne modifi�e : recherche des incompatibilit�s
        TraiterOrang�(e.Col)

        'Traiter les autres colonnes
        If Not monPlanPourPhasage.mLigneFeuxMultiPhases Then
          For col = 1 To MaxColPhasage()
            If col <> e.Col Then
              'D�cocher la ligne de feux qui vient d'�tre coch�e ou d�coch�e(nb : si elle a �t� d�coch�e, les autres �taitent d�ja d�coch�es)
              rg = fg.GetCellRange(e.Row, col)
              rg.Checkbox = Grille.CheckEnum.Unchecked
              'Recherche des incompatibilit�s dans la colonne
              TraiterOrang�(col)
            End If
          Next
        End If

        D�terminerPhasageCorrect()

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
          'L'enregistrement a �chou�
          Return True
        Else
          'L'enregistrement a r�ussi
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
          AfficherMessageErreur(Me, "Ce fichier est d�j� ouvert")
          Return EnregistrerSous()
        Else
          maVariante.NomFichier = NomFichier
          If maVariante.Enregistrer() Then
            'L'enregistrement a �chou�
            maVariante.NomFichier = ExNomFichier
            Return True
          Else
            'L'enregistrement a r�ussi
            Modif = False
            Me.Text = maVariante.Libell�
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
