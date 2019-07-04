
'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : MDIDiagfeux.vb										  											'
'						Classes																														'
'							MDIDiagfeux : Feuille MDI de l'application												'
'																																							'
'******************************************************************************

Imports System.IO

'=====================================================================================================
'----------- Class MDIDiagfeux : Feuille MDI de l'application ---------------------
'=====================================================================================================
Public Class MDIDiagfeux
  Inherits System.Windows.Forms.Form

#Region " Code généré par le Concepteur Windows Form "

  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()

    'Ajoutez une initialisation quelconque après l'appel InitializeComponent()
    mdiApplication = Me

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
  Friend WithEvents mnuFichier As System.Windows.Forms.MenuItem
  Friend WithEvents mnuNouveau As System.Windows.Forms.MenuItem
  Friend WithEvents mnuFermer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuQuitter As System.Windows.Forms.MenuItem
  Friend WithEvents mnuOuvrir As System.Windows.Forms.MenuItem
  Friend WithEvents mnuParamétrage As System.Windows.Forms.MenuItem
  Friend WithEvents mnuFenêtre As System.Windows.Forms.MenuItem
  Friend WithEvents mnuCascade As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAide As System.Windows.Forms.MenuItem
  Friend WithEvents mnuMain As System.Windows.Forms.MainMenu
  Friend WithEvents mnuImprimer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuConfigImpr As System.Windows.Forms.MenuItem
  Friend WithEvents mnuEnregistrer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuEnregSous As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSepFic1 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSepFic2 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuHorizontal As System.Windows.Forms.MenuItem
  Friend WithEvents mnuRafraichir As System.Windows.Forms.MenuItem
  Friend WithEvents mnuToolBar As System.Windows.Forms.MenuItem
  Friend WithEvents mnuStatusBar As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSepFic3 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAffichage As System.Windows.Forms.MenuItem
  Friend WithEvents tbrbtnOuvrir As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnEnregistrer As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnNouveau As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnZoom As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnZoomMoins As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnZoomAvant As System.Windows.Forms.ToolBarButton
  Friend WithEvents mnuSepAffichage As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAfficherFDP As System.Windows.Forms.MenuItem
  Friend WithEvents tbrbtnRafraichir As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnZoomPAN As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnMesurer As System.Windows.Forms.ToolBarButton
  Friend WithEvents stapnlVerrou As System.Windows.Forms.StatusBarPanel
  Friend WithEvents stapnlCoord As System.Windows.Forms.StatusBarPanel
  Friend WithEvents tbrbtnImprimer As System.Windows.Forms.ToolBarButton
  Friend WithEvents ilsDiagfeux As System.Windows.Forms.ImageList
  Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuScénario As System.Windows.Forms.MenuItem
  Friend WithEvents mnuScénarioNouveau As System.Windows.Forms.MenuItem
  Friend WithEvents mnuScénarioDupliquer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuScénarioRenommer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuScénarioSupprimer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuEchelle As System.Windows.Forms.MenuItem
  Friend WithEvents mnuNord As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSepAffichage2 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAideSommaire As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAideSur As System.Windows.Forms.MenuItem
  Friend WithEvents mnuAideRecherche As System.Windows.Forms.MenuItem
  Friend WithEvents mnuApropos As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSepFic4 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSelect1 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSelect2 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSelect3 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSelect4 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSensCirculation As System.Windows.Forms.MenuItem
  Friend WithEvents tbrbtnNord As System.Windows.Forms.ToolBarButton
  Friend WithEvents tbrbtnEchelle As System.Windows.Forms.ToolBarButton
  Friend WithEvents pnlScénario As System.Windows.Forms.Panel
  Friend WithEvents lblScénario As System.Windows.Forms.Label
  Friend WithEvents cboScénario As System.Windows.Forms.ComboBox
  Friend WithEvents lblProjetDéfinitif As System.Windows.Forms.Label
  Friend WithEvents mnuSensTrajectoires As System.Windows.Forms.MenuItem
  Friend WithEvents tbrDiagfeux As System.Windows.Forms.ToolBar
  Friend WithEvents staDiagfeux As System.Windows.Forms.StatusBar
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MDIDiagfeux))
    Me.mnuMain = New System.Windows.Forms.MainMenu
    Me.mnuFichier = New System.Windows.Forms.MenuItem
    Me.mnuNouveau = New System.Windows.Forms.MenuItem
    Me.mnuOuvrir = New System.Windows.Forms.MenuItem
    Me.mnuFermer = New System.Windows.Forms.MenuItem
    Me.mnuEnregistrer = New System.Windows.Forms.MenuItem
    Me.mnuEnregSous = New System.Windows.Forms.MenuItem
    Me.mnuSepFic1 = New System.Windows.Forms.MenuItem
    Me.mnuParamétrage = New System.Windows.Forms.MenuItem
    Me.mnuSepFic3 = New System.Windows.Forms.MenuItem
    Me.mnuConfigImpr = New System.Windows.Forms.MenuItem
    Me.mnuImprimer = New System.Windows.Forms.MenuItem
    Me.mnuSepFic2 = New System.Windows.Forms.MenuItem
    Me.mnuSelect1 = New System.Windows.Forms.MenuItem
    Me.mnuSelect2 = New System.Windows.Forms.MenuItem
    Me.mnuSelect3 = New System.Windows.Forms.MenuItem
    Me.mnuSelect4 = New System.Windows.Forms.MenuItem
    Me.mnuSepFic4 = New System.Windows.Forms.MenuItem
    Me.mnuQuitter = New System.Windows.Forms.MenuItem
    Me.mnuScénario = New System.Windows.Forms.MenuItem
    Me.mnuScénarioNouveau = New System.Windows.Forms.MenuItem
    Me.mnuScénarioDupliquer = New System.Windows.Forms.MenuItem
    Me.mnuScénarioRenommer = New System.Windows.Forms.MenuItem
    Me.mnuScénarioSupprimer = New System.Windows.Forms.MenuItem
    Me.mnuAffichage = New System.Windows.Forms.MenuItem
    Me.mnuToolBar = New System.Windows.Forms.MenuItem
    Me.mnuStatusBar = New System.Windows.Forms.MenuItem
    Me.mnuSepAffichage = New System.Windows.Forms.MenuItem
    Me.mnuAfficherFDP = New System.Windows.Forms.MenuItem
    Me.mnuEchelle = New System.Windows.Forms.MenuItem
    Me.mnuNord = New System.Windows.Forms.MenuItem
    Me.mnuSensCirculation = New System.Windows.Forms.MenuItem
    Me.mnuSensTrajectoires = New System.Windows.Forms.MenuItem
    Me.mnuSepAffichage2 = New System.Windows.Forms.MenuItem
    Me.mnuRafraichir = New System.Windows.Forms.MenuItem
    Me.mnuFenêtre = New System.Windows.Forms.MenuItem
    Me.mnuCascade = New System.Windows.Forms.MenuItem
    Me.mnuHorizontal = New System.Windows.Forms.MenuItem
    Me.mnuAide = New System.Windows.Forms.MenuItem
    Me.mnuAideSommaire = New System.Windows.Forms.MenuItem
    Me.mnuAideSur = New System.Windows.Forms.MenuItem
    Me.mnuAideRecherche = New System.Windows.Forms.MenuItem
    Me.MenuItem13 = New System.Windows.Forms.MenuItem
    Me.mnuApropos = New System.Windows.Forms.MenuItem
    Me.tbrDiagfeux = New System.Windows.Forms.ToolBar
    Me.tbrbtnNouveau = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnOuvrir = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnEnregistrer = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnImprimer = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnZoom = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnZoomMoins = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnZoomPAN = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnZoomAvant = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnRafraichir = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnMesurer = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnNord = New System.Windows.Forms.ToolBarButton
    Me.tbrbtnEchelle = New System.Windows.Forms.ToolBarButton
    Me.ilsDiagfeux = New System.Windows.Forms.ImageList(Me.components)
    Me.staDiagfeux = New System.Windows.Forms.StatusBar
    Me.stapnlVerrou = New System.Windows.Forms.StatusBarPanel
    Me.stapnlCoord = New System.Windows.Forms.StatusBarPanel
    Me.pnlScénario = New System.Windows.Forms.Panel
    Me.lblProjetDéfinitif = New System.Windows.Forms.Label
    Me.lblScénario = New System.Windows.Forms.Label
    Me.cboScénario = New System.Windows.Forms.ComboBox
    CType(Me.stapnlVerrou, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.stapnlCoord, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlScénario.SuspendLayout()
    Me.SuspendLayout()
    '
    'mnuMain
    '
    Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFichier, Me.mnuScénario, Me.mnuAffichage, Me.mnuFenêtre, Me.mnuAide})
    '
    'mnuFichier
    '
    Me.mnuFichier.Index = 0
    Me.mnuFichier.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNouveau, Me.mnuOuvrir, Me.mnuFermer, Me.mnuEnregistrer, Me.mnuEnregSous, Me.mnuSepFic1, Me.mnuParamétrage, Me.mnuSepFic3, Me.mnuConfigImpr, Me.mnuImprimer, Me.mnuSepFic2, Me.mnuSelect1, Me.mnuSelect2, Me.mnuSelect3, Me.mnuSelect4, Me.mnuSepFic4, Me.mnuQuitter})
    Me.mnuFichier.Text = "&Fichier"
    '
    'mnuNouveau
    '
    Me.mnuNouveau.Index = 0
    Me.mnuNouveau.Shortcut = System.Windows.Forms.Shortcut.CtrlN
    Me.mnuNouveau.Text = "Nouveau..."
    '
    'mnuOuvrir
    '
    Me.mnuOuvrir.Index = 1
    Me.mnuOuvrir.Shortcut = System.Windows.Forms.Shortcut.CtrlO
    Me.mnuOuvrir.Text = "Ouvrir..."
    '
    'mnuFermer
    '
    Me.mnuFermer.Index = 2
    Me.mnuFermer.Shortcut = System.Windows.Forms.Shortcut.CtrlF4
    Me.mnuFermer.Text = "Fermer"
    '
    'mnuEnregistrer
    '
    Me.mnuEnregistrer.Enabled = False
    Me.mnuEnregistrer.Index = 3
    Me.mnuEnregistrer.Shortcut = System.Windows.Forms.Shortcut.CtrlS
    Me.mnuEnregistrer.Text = "Enregistrer"
    '
    'mnuEnregSous
    '
    Me.mnuEnregSous.Enabled = False
    Me.mnuEnregSous.Index = 4
    Me.mnuEnregSous.Text = "Enregistrer sous..."
    '
    'mnuSepFic1
    '
    Me.mnuSepFic1.Index = 5
    Me.mnuSepFic1.Text = "-"
    '
    'mnuParamétrage
    '
    Me.mnuParamétrage.Index = 6
    Me.mnuParamétrage.Text = "&Paramétrage"
    '
    'mnuSepFic3
    '
    Me.mnuSepFic3.Index = 7
    Me.mnuSepFic3.Text = "-"
    '
    'mnuConfigImpr
    '
    Me.mnuConfigImpr.Index = 8
    Me.mnuConfigImpr.Text = "Configurer l'imprimante..."
    '
    'mnuImprimer
    '
    Me.mnuImprimer.Enabled = False
    Me.mnuImprimer.Index = 9
    Me.mnuImprimer.Shortcut = System.Windows.Forms.Shortcut.CtrlP
    Me.mnuImprimer.Text = "Imprimer..."
    '
    'mnuSepFic2
    '
    Me.mnuSepFic2.Index = 10
    Me.mnuSepFic2.Text = "-"
    '
    'mnuSelect1
    '
    Me.mnuSelect1.Index = 11
    Me.mnuSelect1.Text = "&1...."
    Me.mnuSelect1.Visible = False
    '
    'mnuSelect2
    '
    Me.mnuSelect2.Index = 12
    Me.mnuSelect2.Text = "&2...."
    Me.mnuSelect2.Visible = False
    '
    'mnuSelect3
    '
    Me.mnuSelect3.Index = 13
    Me.mnuSelect3.Text = "&3...."
    Me.mnuSelect3.Visible = False
    '
    'mnuSelect4
    '
    Me.mnuSelect4.Index = 14
    Me.mnuSelect4.Text = "&4...."
    Me.mnuSelect4.Visible = False
    '
    'mnuSepFic4
    '
    Me.mnuSepFic4.Index = 15
    Me.mnuSepFic4.Text = "-"
    Me.mnuSepFic4.Visible = False
    '
    'mnuQuitter
    '
    Me.mnuQuitter.Index = 16
    Me.mnuQuitter.Text = "Quitter"
    '
    'mnuScénario
    '
    Me.mnuScénario.Index = 1
    Me.mnuScénario.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuScénarioNouveau, Me.mnuScénarioDupliquer, Me.mnuScénarioRenommer, Me.mnuScénarioSupprimer})
    Me.mnuScénario.Text = "&Scénario"
    Me.mnuScénario.Visible = False
    '
    'mnuScénarioNouveau
    '
    Me.mnuScénarioNouveau.Index = 0
    Me.mnuScénarioNouveau.Text = "Nouveau"
    '
    'mnuScénarioDupliquer
    '
    Me.mnuScénarioDupliquer.Index = 1
    Me.mnuScénarioDupliquer.Text = "Dupliquer"
    '
    'mnuScénarioRenommer
    '
    Me.mnuScénarioRenommer.Index = 2
    Me.mnuScénarioRenommer.Text = "Renommer"
    '
    'mnuScénarioSupprimer
    '
    Me.mnuScénarioSupprimer.Index = 3
    Me.mnuScénarioSupprimer.Text = "Suprimer"
    '
    'mnuAffichage
    '
    Me.mnuAffichage.Index = 2
    Me.mnuAffichage.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuToolBar, Me.mnuStatusBar, Me.mnuSepAffichage, Me.mnuAfficherFDP, Me.mnuEchelle, Me.mnuNord, Me.mnuSensCirculation, Me.mnuSensTrajectoires, Me.mnuSepAffichage2, Me.mnuRafraichir})
    Me.mnuAffichage.Text = "&Affichage"
    '
    'mnuToolBar
    '
    Me.mnuToolBar.Checked = True
    Me.mnuToolBar.Index = 0
    Me.mnuToolBar.Text = "Barre d'outils"
    '
    'mnuStatusBar
    '
    Me.mnuStatusBar.Checked = True
    Me.mnuStatusBar.Index = 1
    Me.mnuStatusBar.Text = "Barre d'état"
    '
    'mnuSepAffichage
    '
    Me.mnuSepAffichage.Index = 2
    Me.mnuSepAffichage.Text = "-"
    '
    'mnuAfficherFDP
    '
    Me.mnuAfficherFDP.Index = 3
    Me.mnuAfficherFDP.Text = "Masquer le fond de plan"
    Me.mnuAfficherFDP.Visible = False
    '
    'mnuEchelle
    '
    Me.mnuEchelle.Index = 4
    Me.mnuEchelle.Text = "Echelle"
    Me.mnuEchelle.Visible = False
    '
    'mnuNord
    '
    Me.mnuNord.Index = 5
    Me.mnuNord.Text = "Nord"
    Me.mnuNord.Visible = False
    '
    'mnuSensCirculation
    '
    Me.mnuSensCirculation.Index = 6
    Me.mnuSensCirculation.Text = "Sens de circulation"
    Me.mnuSensCirculation.Visible = False
    '
    'mnuSensTrajectoires
    '
    Me.mnuSensTrajectoires.Index = 7
    Me.mnuSensTrajectoires.Text = "Sens des trajectoires"
    Me.mnuSensTrajectoires.Visible = False
    '
    'mnuSepAffichage2
    '
    Me.mnuSepAffichage2.Index = 8
    Me.mnuSepAffichage2.Text = "-"
    Me.mnuSepAffichage2.Visible = False
    '
    'mnuRafraichir
    '
    Me.mnuRafraichir.Enabled = False
    Me.mnuRafraichir.Index = 9
    Me.mnuRafraichir.Shortcut = System.Windows.Forms.Shortcut.CtrlR
    Me.mnuRafraichir.Text = "&Rafraichir"
    '
    'mnuFenêtre
    '
    Me.mnuFenêtre.Index = 3
    Me.mnuFenêtre.MdiList = True
    Me.mnuFenêtre.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCascade, Me.mnuHorizontal})
    Me.mnuFenêtre.Text = "Fe&nêtre"
    '
    'mnuCascade
    '
    Me.mnuCascade.Index = 0
    Me.mnuCascade.Text = "Cascade"
    '
    'mnuHorizontal
    '
    Me.mnuHorizontal.Index = 1
    Me.mnuHorizontal.Text = "Mosaïque horizontale"
    '
    'mnuAide
    '
    Me.mnuAide.Index = 4
    Me.mnuAide.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAideSommaire, Me.mnuAideSur, Me.mnuAideRecherche, Me.MenuItem13, Me.mnuApropos})
    Me.mnuAide.Text = "&?"
    '
    'mnuAideSommaire
    '
    Me.mnuAideSommaire.Index = 0
    Me.mnuAideSommaire.Shortcut = System.Windows.Forms.Shortcut.F1
    Me.mnuAideSommaire.Text = "Sommaire"
    '
    'mnuAideSur
    '
    Me.mnuAideSur.Index = 1
    Me.mnuAideSur.Text = "Aide sur"
    '
    'mnuAideRecherche
    '
    Me.mnuAideRecherche.Index = 2
    Me.mnuAideRecherche.Text = "Rechercher"
    '
    'MenuItem13
    '
    Me.MenuItem13.Index = 3
    Me.MenuItem13.Text = "-"
    '
    'mnuApropos
    '
    Me.mnuApropos.Index = 4
    Me.mnuApropos.Text = "A propos de Diagfeux"
    '
    'tbrDiagfeux
    '
    Me.tbrDiagfeux.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbrbtnNouveau, Me.tbrbtnOuvrir, Me.tbrbtnEnregistrer, Me.tbrbtnImprimer, Me.tbrbtnZoom, Me.tbrbtnZoomMoins, Me.tbrbtnZoomPAN, Me.tbrbtnZoomAvant, Me.tbrbtnRafraichir, Me.tbrbtnMesurer, Me.tbrbtnNord, Me.tbrbtnEchelle})
    Me.tbrDiagfeux.DropDownArrows = True
    Me.tbrDiagfeux.ImageList = Me.ilsDiagfeux
    Me.tbrDiagfeux.Location = New System.Drawing.Point(0, 0)
    Me.tbrDiagfeux.Name = "tbrDiagfeux"
    Me.tbrDiagfeux.ShowToolTips = True
    Me.tbrDiagfeux.Size = New System.Drawing.Size(1028, 28)
    Me.tbrDiagfeux.TabIndex = 9
    '
    'tbrbtnNouveau
    '
    Me.tbrbtnNouveau.ImageIndex = 0
    Me.tbrbtnNouveau.ToolTipText = "Nouveau"
    '
    'tbrbtnOuvrir
    '
    Me.tbrbtnOuvrir.ImageIndex = 1
    Me.tbrbtnOuvrir.ToolTipText = "Ouvrir"
    '
    'tbrbtnEnregistrer
    '
    Me.tbrbtnEnregistrer.ImageIndex = 2
    Me.tbrbtnEnregistrer.ToolTipText = "Enregistrer"
    Me.tbrbtnEnregistrer.Visible = False
    '
    'tbrbtnImprimer
    '
    Me.tbrbtnImprimer.ImageIndex = 3
    Me.tbrbtnImprimer.ToolTipText = "Imprimer"
    Me.tbrbtnImprimer.Visible = False
    '
    'tbrbtnZoom
    '
    Me.tbrbtnZoom.ImageIndex = 4
    Me.tbrbtnZoom.ToolTipText = "Zoom avant"
    Me.tbrbtnZoom.Visible = False
    '
    'tbrbtnZoomMoins
    '
    Me.tbrbtnZoomMoins.ImageIndex = 5
    Me.tbrbtnZoomMoins.ToolTipText = "Zoom arrière"
    Me.tbrbtnZoomMoins.Visible = False
    '
    'tbrbtnZoomPAN
    '
    Me.tbrbtnZoomPAN.ImageIndex = 6
    Me.tbrbtnZoomPAN.ToolTipText = "Panoramique"
    Me.tbrbtnZoomPAN.Visible = False
    '
    'tbrbtnZoomAvant
    '
    Me.tbrbtnZoomAvant.ImageIndex = 7
    Me.tbrbtnZoomAvant.ToolTipText = "Zoom précédent"
    Me.tbrbtnZoomAvant.Visible = False
    '
    'tbrbtnRafraichir
    '
    Me.tbrbtnRafraichir.ImageIndex = 8
    Me.tbrbtnRafraichir.ToolTipText = "Rafaichir"
    Me.tbrbtnRafraichir.Visible = False
    '
    'tbrbtnMesurer
    '
    Me.tbrbtnMesurer.ImageIndex = 9
    Me.tbrbtnMesurer.ToolTipText = "Mesurer"
    Me.tbrbtnMesurer.Visible = False
    '
    'tbrbtnNord
    '
    Me.tbrbtnNord.ImageIndex = 10
    Me.tbrbtnNord.ToolTipText = "Nord"
    Me.tbrbtnNord.Visible = False
    '
    'tbrbtnEchelle
    '
    Me.tbrbtnEchelle.ImageIndex = 11
    Me.tbrbtnEchelle.ToolTipText = "Echelle"
    Me.tbrbtnEchelle.Visible = False
    '
    'ilsDiagfeux
    '
    Me.ilsDiagfeux.ImageSize = New System.Drawing.Size(16, 16)
    Me.ilsDiagfeux.ImageStream = CType(resources.GetObject("ilsDiagfeux.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ilsDiagfeux.TransparentColor = System.Drawing.Color.Transparent
    '
    'staDiagfeux
    '
    Me.staDiagfeux.Location = New System.Drawing.Point(0, 569)
    Me.staDiagfeux.Name = "staDiagfeux"
    Me.staDiagfeux.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.stapnlVerrou, Me.stapnlCoord})
    Me.staDiagfeux.ShowPanels = True
    Me.staDiagfeux.Size = New System.Drawing.Size(1028, 32)
    Me.staDiagfeux.TabIndex = 10
    '
    'stapnlVerrou
    '
    Me.stapnlVerrou.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
    Me.stapnlVerrou.Width = 150
    '
    'stapnlCoord
    '
    Me.stapnlCoord.Width = 200
    '
    'pnlScénario
    '
    Me.pnlScénario.Controls.Add(Me.lblProjetDéfinitif)
    Me.pnlScénario.Controls.Add(Me.lblScénario)
    Me.pnlScénario.Controls.Add(Me.cboScénario)
    Me.pnlScénario.Location = New System.Drawing.Point(370, 2)
    Me.pnlScénario.Name = "pnlScénario"
    Me.pnlScénario.Size = New System.Drawing.Size(646, 24)
    Me.pnlScénario.TabIndex = 16
    Me.pnlScénario.Visible = False
    '
    'lblProjetDéfinitif
    '
    Me.lblProjetDéfinitif.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblProjetDéfinitif.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblProjetDéfinitif.ForeColor = System.Drawing.Color.Green
    Me.lblProjetDéfinitif.Location = New System.Drawing.Point(344, 5)
    Me.lblProjetDéfinitif.Name = "lblProjetDéfinitif"
    Me.lblProjetDéfinitif.Size = New System.Drawing.Size(56, 20)
    Me.lblProjetDéfinitif.TabIndex = 18
    Me.lblProjetDéfinitif.Text = "Définitif"
    '
    'lblScénario
    '
    Me.lblScénario.Location = New System.Drawing.Point(0, 5)
    Me.lblScénario.Name = "lblScénario"
    Me.lblScénario.Size = New System.Drawing.Size(90, 20)
    Me.lblScénario.TabIndex = 17
    Me.lblScénario.Text = "Scénario courant"
    '
    'cboScénario
    '
    Me.cboScénario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboScénario.Location = New System.Drawing.Point(92, 3)
    Me.cboScénario.Name = "cboScénario"
    Me.cboScénario.Size = New System.Drawing.Size(232, 21)
    Me.cboScénario.TabIndex = 16
    '
    'MDIDiagfeux
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(1028, 601)
    Me.Controls.Add(Me.pnlScénario)
    Me.Controls.Add(Me.staDiagfeux)
    Me.Controls.Add(Me.tbrDiagfeux)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.IsMdiContainer = True
    Me.Menu = Me.mnuMain
    Me.Name = "MDIDiagfeux"
    Me.Text = "DIAGFEUX"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    CType(Me.stapnlVerrou, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.stapnlCoord, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnlScénario.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "Déclarations"
  Public Shared frmCourant As frmCarrefour
  Private CheminParamétrage As String = IO.Path.Combine(CheminDiagfeux, "Diagfeux.par")
  Private ChargementEnCours As Boolean = True

  Public Enum BarreOutilsEnum
    Nouveau
    Ouvrir
    Enregistrer
    Imprimer
    Zoom
    ZoomMoins
    PAN
    ZoomPrécédent
    Rafraichir
    Mesurer
    Nord
    Echelle
  End Enum

#End Region
#Region "Fonctions de la feuille"
  '********************************************************************************************************************
  '	Cet évènement est déclenché après l'évènement Activated de la feuille fille MDI
  '********************************************************************************************************************
  Private Sub MDIDiagfeux_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate

    frmCourant = ActiveMdiChild

    If IsNothing(frmCourant) Then
      cndVariante = Nothing 'Toutes les feuilles sont fermées
      ActiverAffichage(Activé:=False)

    Else
      ActiverAffichage(Activé:=cndVariante.ModeGraphique)

      'Activation des boutons de la barre d'outils

      With tbrDiagfeux.Buttons()
        Dim i As MDIDiagfeux.BarreOutilsEnum
        For i = BarreOutilsEnum.Enregistrer To BarreOutilsEnum.PAN
          .Item(i).Visible = True
        Next
        'Le menu intermédiaire Zoom précédent ne peut être rendu visible que contextuellement par la feuille active (cf frmCarrrefour.Activated)
        For i = BarreOutilsEnum.Rafraichir To BarreOutilsEnum.Echelle
          'RAfraichir, Mesurer, Nord et Echelle
          .Item(i).Visible = True
        Next

      End With

      mnuEnregistrer.Enabled = True
      mnuEnregSous.Enabled = True
      mnuImprimer.Enabled = True

      mnuRafraichir.Enabled = True

      'Scénarios
      AfficherScénarios()


    End If

    AfficherBarreEtat()

  End Sub

  Public Sub AfficherScénarios()

    Try

      mnuScénario.Visible = True
      cboScénario.Items.Clear()

      pnlScénario.Visible = cndVariante.mPlansFeuxBase.Count > 0
      Dim unPlanFeux As PlanFeux
      For Each unPlanFeux In cndVariante.mPlansFeuxBase
        cboScénario.Items.Add(unPlanFeux.Nom)
      Next
      If cndVariante.ScénarioEnCours() Then
        cboScénario.Text = cndVariante.ScénarioCourant.Nom
        mnuScénarioRenommer.Enabled = True
        mnuScénarioSupprimer.Enabled = True
        mnuScénarioDupliquer.Enabled = True
      Else
        mnuScénarioRenommer.Enabled = False
        mnuScénarioSupprimer.Enabled = False
        mnuScénarioDupliquer.Enabled = False
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherScénarios")

    End Try
  End Sub

  Private Sub MDIDiagfeux_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    If ProtectionOK() Then
      If IO.File.Exists(CheminParamétrage) Then
        Try
          ds = New DataSetDiagFeux
          ds.ReadXml(CheminParamétrage, XmlReadMode.ReadSchema)

          If ds.Paramétrage.Rows.Count = 0 Then
            ds.Dispose()
            mnuParamétrage.PerformClick()
            ds.ReadXml(CheminParamétrage, XmlReadMode.ReadSchema)
            If ds.Paramétrage.Rows.Count = 0 Then
              ds.Dispose()
              Me.Close()
            End If

          Else
            LireParamétrage()
            ds.Dispose()

            'Dim nb As Short = ds.TableCycleCapacité.Rows.Count
            'If nb = 0 Then
            '  LecModuleClassique()
            '  EcrireParamétrage()
            'Else
            '  LireParamétrage()
            'End If
          End If

          'Initialiser la correspondance des couleurs Autocad et RGB
          tableCouleur()

          If ChargementEnCours Then
            ChargementEnCours = False
            If Environment.GetCommandLineArgs.Length > 1 Then
              Ouvrir(Environment.GetCommandLineArgs(1))
            End If
          End If

        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
        End Try

      Else
        mnuParamétrage.PerformClick()
        If Not IO.File.Exists(CheminParamétrage) Then Me.Close()
      End If

      LireRegistry()

      initAide()

    End If

  End Sub

  Private Function ProtectionOK() As Boolean
    '********************************
    'test Protection
    '********************************
    'Type de protection
        TYPPROTECTION = CPM 'QLM 'CPM
    ' Vérification de l'enregistrement
    If ProtectCheck("its00+-k") = "its00+-k" Then
      ' Protection OK
      Return True
    Else 'la licence n'a pas été validée on ferme
      Me.Close()
      Return False
    End If
    '********************************

  End Function

  Private Sub MDIDiagfeux_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
    Dim i As Short
    Dim MySettings(,) As String

    For i = 0 To MRUFichiers.Length - 1
      SaveSetting(AppName:=NomProduit, Section:="Recent Files", _
      Key:="File" & CStr(i + 1), Setting:=MRUFichiers(i))
    Next
    ' Suppression dans la registry des fichiers effacés ( MRUmenu les a ignorés à l'ouverture de DIAGFEUX, mais ils sont tjs présents dans la registry)
    MySettings = GetAllSettings(AppName:=NomProduit, Section:="Recent Files")
    If Not IsNothing(MySettings) Then
      For i = UBound(MRUFichiers) + 1 To UBound(MySettings, 1)
        DeleteSetting(NomProduit, "Recent Files", MySettings(i, 0))
      Next
    End If

  End Sub
#End Region
#Region "Impressions"
  Private fntImp, fntEntete As Font
  Private nbPages As Integer
  Private WithEvents pDocment As New Printing.PrintDocument
  Private Sub pDocment_BeginPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) _
  Handles pDocment.BeginPrint

    fntImp = New Font("Courier New", 10)
    fntEntete = New Font("Arial", 10, FontStyle.Bold)

    nbPages = 0

  End Sub

  Private Sub pDocment_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
  Handles pDocment.PrintPage
    Dim rc As Rectangle = e.MarginBounds
    Dim h As Integer = fntImp.Height
    Dim w As Integer = rc.Width
    nbPages += 1

    'Remplir un espace grisé (sans contour) pour l'entete
    Dim rcEntete As New RectangleF(rc.Left, rc.Top, rc.Width, fntEntete.Height)
    e.Graphics.FillRectangle(Brushes.LightGray, rcEntete)

    Dim sf As New StringFormat(StringFormatFlags.NoWrap)
    e.Graphics.DrawString("SALUT les Gusses", fntEntete, Brushes.Black, rcEntete, sf)

    sf.Alignment = StringAlignment.Far
    e.Graphics.DrawString(nbPages.ToString, fntEntete, Brushes.Black, rcEntete, sf)

    rc.Y += CInt(rcEntete.Height)
    e.Graphics.DrawRectangle(Pens.Gray, rc)

    e.HasMorePages = False

  End Sub

  Private Sub pDocment_EndPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) _
  Handles pDocment.EndPrint
    fntImp.Dispose()
    fntEntete.Dispose()
  End Sub


#End Region
#Region "Carrefour"
  '********************************************************************************************************************
  ' Appeler la boite de dialogue de saisie des caractéristiques du carrefourr
  '********************************************************************************************************************
  Private Function CarGen(ByVal dlg As dlgCarGen, ByVal uneVariante As Variante) As Variante

    With dlg
      .mVariante = uneVariante
      If .ShowDialog(Me) = DialogResult.OK Then
        'Mettre à jour les données du carrefour avec les données saisies dans la boite
        .MettreAjour()
        Return .mVariante
      Else
        Return Nothing
      End If

    End With

  End Function

  '********************************************************************************************************************
  '	Instancier et initialiser la fenêtre Carrefour correspondant à la nouvelle variante
  '********************************************************************************************************************
  Private Sub FenetreCarrefour()
    Dim frm As New frmCarrefour

    Try

      With frm
        .MdiParent = Me
        frmCourant = frm
        If Not cndVariante.ModeGraphique Then
          'Désactiver les fonctions inutilisées dans le mode dégradé
          .btnLigneFeux.Visible = False
          .btnSignalMoins.Visible = False
        End If
        Try
          .Show()
        Catch ex As System.Exception
          .Close()
          AfficherMessageErreur(Me, ex)
          frmCourant = Nothing
        End Try
      End With

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
      frmCourant = Nothing
    End Try

  End Sub

  Public Sub SaisirInfoImprim()
    frmCourant.SaisirInfoImprim()
  End Sub

  Public Function Lire(ByVal NomFichier As String) As Variante
    Dim uneRowCarrefour As DataSetDiagfeux.CarrefourRow
    Dim unCarrefour As Carrefour
    Dim uneVariante As Variante

    Try

      Me.Cursor = Cursors.WaitCursor

      ds = New DataSetDiagfeux

      'Lire le projet
      ds.ReadXml(NomFichier, XmlReadMode.ReadSchema)

      'Lire le carrefour
      uneRowCarrefour = ds.Carrefour.Rows(0)
      unCarrefour = New Carrefour(uneRowCarrefour)
      'Lire la variante du carrefour
      uneVariante = New Variante(unCarrefour, uneRowCarrefour.GetVarianteRows(0))
      uneVariante.NomFichier = NomFichier
      cndVariantes.Add(uneVariante)
#If Not Debug Then
      If uneVariante.VersionFichier = 2 Then
        AfficherMessageErreur(Me, "Le fichier " & NomFichier & " est incompatible avec cette version de DIAGFEUX")
        cndVariantes.Remove(uneVariante)
        uneVariante = Nothing
      End If
#End If
      Lire = uneVariante

      Me.Cursor = Cursors.Default

    Catch ex As System.Exception
      Me.Cursor = Cursors.Default
      AfficherMessageErreur(Nothing, ex)

    End Try
  End Function

#End Region
#Region "Menus"
#Region "Menu Fichier"
  Private Sub mnuParamétrage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuParamétrage.Click
    Dim dlg As New dlgParamétrage

    With dlg
      .ShowDialog(Me)
      Select Case .DialogResult
        Case DialogResult.OK
          If .chkConserverDéfaut.Checked Then
            'la case ConserverDéfaut est sytématiquement cochée lors de la 1ère utilisation de DIAGFEUX - ensuite, selon le choix utilisateur
            Try
              EcrireParamétrage()

            Catch ex As System.Exception
              AfficherMessageErreur(Nothing, ex)
            End Try
          End If

          If TypeOf .mObjetMétier Is PlanFeuxBase Then
            frmCourant.RéinitialiserPhasages()

          ElseIf TypeOf .mObjetMétier Is PlanFeuxFonctionnement Then
            frmCourant.RecalculerCapacités()
          End If

          If .mSignalPiétonChangé AndAlso Not IsNothing(frmCourant) Then
            frmCourant.RecréerGraphique()
            frmCourant.Redessiner()
            frmCourant.Modif = True
          End If

        Case DialogResult.Cancel
          'MessageBox.Show("Annuler", "Paramétrage", MessageBoxButtons.OK)
      End Select
      .Dispose()
    End With
  End Sub

  'Public Sub LecModuleClassique()
  '  Dim fs As FileStream
  '  Dim Nomfich As String = "tablecapacitetheorique1.txt"
  '  Dim strRead As StreamReader
  '  Dim Ligne As String
  '  Dim i, j As Short
  '  Dim pos As Short
  '  Dim pos2 As Short

  '  ReDim TbCycleCapacité(15, 23)

  '  fs = New FileStream(Nomfich, FileMode.Open, FileAccess.Read, FileShare.Read, 32768)
  '  strRead = New StreamReader(fs)

  '  Try
  '    For i = 0 To TbCycleCapacité.GetUpperBound(0)
  '      j = 0
  '      Ligne = strRead.ReadLine
  '      pos = 0
  '      pos2 = Ligne.IndexOf(vbTab, pos)

  '      Do While pos2 <> -1
  '        TbCycleCapacité(i, j) = CType(Mid(Ligne, pos + 1, pos2 - pos), Short)
  '        pos = pos2 + 1
  '        pos2 = Ligne.IndexOf(vbTab, pos)
  '        j += 1
  '      Loop

  '      TbCycleCapacité(i, j) = CType(Mid(Ligne, pos + 1), Short)

  '    Next

  '  Catch ex As System.Exception
  '    AfficherMessageErreur(Nothing, ex)
  '  Finally
  '    fs.Close()
  '  End Try

  'End Sub

  Private Sub LireParamétrage()
    Try
      Dim uneRowParamétrage As DataSetDiagFeux.ParamétrageRow = ds.Paramétrage.Rows(0)
      With uneRowParamétrage
        Try
          cndParamètres.VersionFichier = VersionFichier
        Catch ex As StrongTypingException
          'Version non renseignée : < v13
          cndParamètres.TempsPerduDémarrageAgglo = 0
          cndParamètres.TempsPerduDémarrageCampagne = 0
          cndParamètres.TempsJauneInutiliséAgglo = [Global].JauneAgglo
          cndParamètres.TempsJauneInutiliséCampagne = [Global].JauneCampagne
          cndParamètres.SignalPiétonsSonore = True
        End Try
        cndParamètres.Organisme = .Organisme
        cndParamètres.Service = .Service
        If Not .IsStockageNull Then
          cndParamètres.CheminStockage = .Stockage
        End If
        If Not .IsLogoNull Then
          cndParamètres.CheminLogo = .Logo
        End If
        cndParamètres.VitessePiétons = .VitessePiétons
        cndParamètres.VitesseVéhicules = .VitesseVéhicules
        cndParamètres.VitesseVélos = .VitesseVélos
        cndParamètres.DébitSaturation = .DébitSaturation
        'v12 et antérieures
        'cndParamètres.DécalageVertUtile = .VertUtile
        If VersionFichier > 0 Then
          cndParamètres.TempsPerduDémarrageAgglo = .TempsPerduDémarrage
          cndParamètres.TempsPerduDémarrageCampagne = .TempsPerduDémarrageCampagne
          cndParamètres.TempsJauneInutiliséAgglo = .JauneInutiliséAgglo
          cndParamètres.TempsJauneInutiliséCampagne = .JauneInutiliséCampagne
          cndParamètres.SignalPiétonsSonore = .SignalPiétonsSonore
        End If
      End With
      cndParamètres.CheminFDP = IO.Directory.GetCurrentDirectory

      'If ds.TableCycleCapacité.Rows.Count = 0 Then
      'Else
      '  Dim uneRowCapacité As DataSetDiagfeux.TableCycleCapacitéRow = ds.TableCycleCapacité.Rows(0)
      '  With uneRowCapacité
      '    DuréeCycleMini = .DuréeCycleMini
      '    TempsPerduMini = .TempsPerduMini
      '    nbCycles = .GetDuréeCycleRows.Length
      '    For i = 0 To nbCycles - 1
      '      uneRowDuréeCycle = .GetDuréeCycleRows(i)
      '      If i = 0 Then
      '        nbTempsPerdus = uneRowDuréeCycle.GetDemandeCarrefourRows.Length
      '        ReDim TbCycleCapacité(nbCycles - 1, nbTempsPerdus - 1)
      '      End If
      '      For j = 0 To nbTempsPerdus - 1
      '        uneRowDemandeCarrefour = uneRowDuréeCycle.GetDemandeCarrefourRows(j)
      '        TbCycleCapacité(i, j) = uneRowDemandeCarrefour.DemandeCarrefour_Column
      '      Next
      '    Next
      '  End With
      'End If

    Catch ex As System.Exception

    End Try

  End Sub

  Private Sub EcrireParamétrage()

    ds = New DataSetDiagfeux
    Dim uneRowParamétrage As DataSetDiagfeux.ParamétrageRow = ds.Paramétrage.NewParamétrageRow
    With uneRowParamétrage
      .Version = cndParamètres.VersionFichier
      .Organisme = cndParamètres.Organisme
      .Service = cndParamètres.Service
      .Stockage = cndParamètres.CheminStockage
      .Logo = cndParamètres.CheminLogo
      .VitessePiétons = cndParamètres.VitessePiétons
      .VitesseVéhicules = cndParamètres.VitesseVéhicules
      .VitesseVélos = cndParamètres.VitesseVélos
      .DébitSaturation = cndParamètres.DébitSaturation
      'v12 et antérieures
      '.VertUtile = cndParamètres.DécalageVertUtile
      'v13
      .TempsPerduDémarrage = cndParamètres.TempsPerduDémarrageAgglo
      .TempsPerduDémarrageCampagne = cndParamètres.TempsPerduDémarrageCampagne
      .JauneInutiliséAgglo = cndParamètres.TempsJauneInutiliséAgglo
      .JauneInutiliséCampagne = cndParamètres.TempsJauneInutiliséCampagne
      .SignalPiétonsSonore = cndParamètres.SignalPiétonsSonore
    End With
    ds.Paramétrage.AddParamétrageRow(uneRowParamétrage)

    'Dim uneRowCapacité As DataSetDiagfeux.TableCycleCapacitéRow = ds.TableCycleCapacité.NewTableCycleCapacitéRow
    'Dim uneRowDuréeCycle As DataSetDiagfeux.DuréeCycleRow
    'Dim uneRowDemandeCarrefour As DataSetDiagfeux.DemandeCarrefourRow
    'Dim DemandeCarrefour As Short
    'Dim i, j As Short
    'With uneRowCapacité
    '  .DuréeCycleMini = 45
    '  .TempsPerduMini = 7
    '  ds.TableCycleCapacité.AddTableCycleCapacitéRow(uneRowCapacité)
    '  For i = 0 To TbCycleCapacité.GetUpperBound(0)
    '    uneRowDuréeCycle = ds.DuréeCycle.NewDuréeCycleRow
    '    uneRowDuréeCycle.SetParentRow(uneRowCapacité)
    '    ds.DuréeCycle.AddDuréeCycleRow(uneRowDuréeCycle)
    '    For j = 0 To TbCycleCapacité.GetUpperBound(1)
    '      uneRowDemandeCarrefour = ds.DemandeCarrefour.NewDemandeCarrefourRow
    '      uneRowDemandeCarrefour.DemandeCarrefour_Column = TbCycleCapacité(i, j)
    '      ds.DemandeCarrefour.AddDemandeCarrefourRow(TbCycleCapacité(i, j), uneRowDuréeCycle)
    '    Next
    '  Next
    'End With

    ds.WriteXml(CheminParamétrage, mode:=XmlWriteMode.WriteSchema)
    ds.Dispose()

  End Sub

  Private Sub mnuQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQuitter.Click
    Me.Close()
  End Sub

  Private Sub mnuImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImprimer.Click

    If Not IsNothing(frmCourant) Then
      If cndVariante.ScénarioEnCours Then
        Dim dlg As New dlgImpressions
        With dlg
          .ShowDialog(Me)
        End With

      Else
        MessageBox.Show("Il faut d'abord créer un scénario")
      End If
    End If
  End Sub

  Private Sub mnuConfigImpr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfigImpr.Click
    Dim dlg As New PrintDialog

    With dlg
      If IsNothing(cndPrintDocument) Then
        'Peut ête Nothing suite à fermeture de dlgImpressions (voir son évènement Closed)
        cndPrintDocument = New Printing.PrintDocument
        cndPrintDocument.PrinterSettings.PrinterName = NomImprimante
      End If
      .Document = cndPrintDocument
      .ShowDialog(Me)
      NomImprimante = .PrinterSettings.PrinterName
    End With

  End Sub

  Private Sub mnuNouveau_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNouveau.Click
    Dim dlg As New dlgCarGen
    Dim uneVariante As Variante

    uneVariante = CarGen(dlg, New Variante)

    If Not IsNothing(uneVariante) Then
      cndVariantes.Add(uneVariante)
      cndVariante = uneVariante
      cndVariante.Verrou = [Global].Verrouillage.Géométrie
      FenetreCarrefour()
    End If

    dlg.Dispose()

  End Sub

  Private Sub mnuOuvrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOuvrir.Click

    Dim NomFichier As String = DialogueFichier(Outils.TypeDialogueEnum.Ouvrir, Filtre:=ComposerFiltre(etuExtension), DefaultExt:=etuExtension)

    If Not IsNothing(NomFichier) Then
      If cndVariantes.VarianteOuverte(NomFichier) Then
        MessageBox.Show("Ce fichier est déjà ouvert")
      Else
        Ouvrir(NomFichier)
      End If
    End If

  End Sub

  Private Sub Ouvrir(ByVal NomFichier As String)
    Dim uneVariante As Variante

    cndVariante = New Variante
    uneVariante = Me.Lire(NomFichier)
    If Not IsNothing(uneVariante) Then
      FenetreCarrefour()
      If Not IsNothing(frmCourant) Then
        MRUmenu(NomFichier)
      End If
    End If

  End Sub
  Private Sub mnuFermer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFermer.Click
    If Not ActiveMdiChild Is Nothing Then
      ActiveMdiChild.Close()
    End If
  End Sub

  Private Sub mnuEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnregistrer.Click
    If Not IsNothing(frmCourant) Then frmCourant.Enregistrer()
  End Sub
  Private Sub mnuEnregSous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnregSous.Click
    If Not IsNothing(frmCourant) Then
      frmCourant.EnregistrerSous()
    End If
  End Sub

#End Region
#Region "Menu Fenêtre"
  'Menu Cascade
  Private Sub mnuCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCascade.Click
    LayoutMdi(MdiLayout.Cascade)
  End Sub
  Private Sub mnuHorizontal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHorizontal.Click
    LayoutMdi(MdiLayout.TileHorizontal)
  End Sub

  Private Sub mnuApropos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApropos.Click
    Dim dlg As New frmAPropos

    dlg.ShowDialog(Me)

    dlg.Dispose()
  End Sub

#End Region
#Region "Menu Affichage"

  Private Sub mnuRafraichir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRafraichir.Click
    frmCourant.CommandeZoom(BarreOutilsEnum.Rafraichir)
  End Sub

  Private Sub mnuAfficherFDP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAfficherFDP.Click
    frmCourant.BasculeAffichageFDP()
    frmCourant.RecréerMenuContextuel(Me.mnuAffichage)
  End Sub

  Private Sub mnuSensTrajectoires_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles mnuNord.Click, mnuEchelle.Click, mnuSensCirculation.Click, mnuSensTrajectoires.Click

    Dim mnuItem As MenuItem = sender

    mnuItem.Checked = Not mnuItem.Checked
    If mnuItem Is mnuSensTrajectoires Then
      frmCourant.chkSensTrajectoires.Checked = mnuItem.Checked
    ElseIf mnuItem Is mnuSensCirculation Then
      cndVariante.SensCirculation = mnuItem.Checked
      frmCourant.Redessiner()
    ElseIf mnuItem Is mnuNord Then
      cndVariante.NordAffiché = mnuItem.Checked
      frmCourant.Redessiner()
    ElseIf mnuItem Is mnuEchelle Then
      cndVariante.EchelleAffichée = mnuItem.Checked
      frmCourant.Redessiner()
    End If

  End Sub

  '**********************************************************************
  'Les items du Menu Affichage ne sont visibles que si au moins une
  'fenêtre carrefour est active, et que sa variante est en mode graphique
  '**********************************************************************
  Private Sub ActiverAffichage(ByVal Activé As Boolean)

    mnuSensTrajectoires.Visible = Activé
    mnuSensCirculation.Visible = Activé
    mnuNord.Visible = Activé
    mnuEchelle.Visible = Activé

    If Not IsNothing(cndVariante) AndAlso Not cndVariante.ModeGraphique Then
      'Toujours rendre accessible le sens de circulation en mode tableur : c'est le seul indicateur disponible
      mnuSensCirculation.Visible = True
    End If

    mnuSepAffichage2.Visible = Activé

  End Sub

#End Region
#Region "Menu Scénario"
  Private Sub mnuScénario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScénario.Click
    If Not cndVariante.VerrouGéom Then
      AfficherMessageErreur(Nothing, "Il faut d'abord verrouiller la géométrie")
    End If
  End Sub

  Private Sub mnuScénarioNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScénarioNouveau.Click
    Dim dlg As New dlgScénario

    If Not cndVariante.OngletInterdit([Global].OngletEnum.Trafics) Then
      With dlg
        .maVariante = cndVariante
        If .ShowDialog = DialogResult.OK Then
          frmCourant.NouveauScénario()
        End If
        .Dispose()
      End With
    End If

  End Sub

  Private Sub mnuScénarioDupliquer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScénarioDupliquer.Click
    frmCourant.DupliquerScénario()
  End Sub

  Private Sub mnuScénarioRenommer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScénarioRenommer.Click
    frmCourant.RenommerScénario()
  End Sub

  Private Sub mnuScénarioSupprimer_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScénarioSupprimer.Click
    frmCourant.SupprimerScénario()
  End Sub

  Private Sub cboScénario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboScénario.SelectedIndexChanged
    frmCourant.SélectionnerScénario(cboScénario.SelectedIndex)
  End Sub

#End Region
#Region "Menu Derniers fichiers"
  Private Sub mnuSelect1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelect1.Click, mnuSelect2.Click, mnuSelect3.Click, mnuSelect4.Click
    Dim mnu As MenuItem = sender
    Dim Index As Short = Val(mnu.Mnemonic) - 1
    Dim NomFichier As String = MRUFichiers(Index)

    If cndVariantes.VarianteOuverte(NomFichier) Then
      MessageBox.Show("Ce fichier est déjà ouvert")
    ElseIf Not IO.File.Exists(NomFichier) Then
    Else
      Ouvrir(NomFichier)
    End If

  End Sub

  Public Sub MRUmenu(ByVal NomFichier As String)
    ' Affichage dans le menu des derniers fichiers utilisés (MRU)
    Dim i, j As Short
    Const MAXFICH As Short = 4

    If IO.File.Exists(NomFichier) Then

      For i = 0 To nbfichMenu - 1
        If UCase(NomFichier) = UCase(MRUFichiers(i)) Then
          ' Le fichier est déjà dans la liste, on va le remettre en tête
          For j = i To nbfichMenu - 2
            ' Resserrement du tableau en supprimant le fichier
            MRUFichiers(j) = MRUFichiers(j + 1)
          Next
          nbfichMenu = nbfichMenu - 1
        End If
      Next

      If nbfichMenu < MAXFICH Then    ' Debug: cette valeur pourra être paramétrée (n'apparait qu'ici), mais sans dépasser la taille de mnuSelect
        mnuSelect(nbfichMenu).Visible = True
        If nbfichMenu = 0 Then mnuSepFic4.Visible = True 'Création de la ligne de séparation
        ReDim Preserve MRUFichiers(nbfichMenu)
        nbfichMenu = nbfichMenu + 1
      End If

      For i = nbfichMenu - 1 To 1 Step -1
        ' Décalage des fichiers pour insérer le nouveau en tête
        MRUFichiers(i) = MRUFichiers(i - 1)
        mnuSelect(i).Text = "&" & CStr(i + 1) & " " & MRUFichiers(i)
      Next
      MRUFichiers(0) = NomFichier
      mnuSelect(0).Text = "&1 " & NomFichier

    End If

  End Sub

  Private Sub LireRegistry()
    ' Object to hold 2-dimensional array returned by GetAllSettings.
    ' Integer to hold counter.
    Dim MySettings(,) As String
    Dim intSettings As Integer
    ' Retrieve the settings.
    MySettings = GetAllSettings(NomProduit, "Recent Files")

    If Not IsNothing(MySettings) Then
      For intSettings = UBound(MySettings, 1) To 0 Step -1     ' On les lit  à l'envers, car MRUmenu les ajoute par décalage
        MRUmenu(MySettings(intSettings, 1))
      Next intSettings

    End If

  End Sub

  Private Function mnuSelect(ByVal Index As Short) As MenuItem
    Select Case Index
      Case 0
        Return mnuSelect1
      Case 1
        Return mnuSelect2
      Case 2
        Return mnuSelect3
      Case 3
        Return mnuSelect4

    End Select
  End Function

#End Region
#End Region
#Region "Barres d'outils, d'état"
  Public Sub AfficherBarreEtat()
    If IsNothing(cndVariante) Then
      Me.staDiagfeux.Panels(0).Text = ""
    Else
      Me.staDiagfeux.Panels(0).Text = cndVariante.LibelléVerrouillage
    End If
  End Sub

  '**********************************************************************************************************************
  ' Barre d'outils-Barre d'état visible/invisible
  '**********************************************************************************************************************
  Private Sub mnuBarre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles mnuToolBar.Click, mnuStatusBar.Click
    Dim mnuItem As MenuItem = sender
    Dim ToolBar As Boolean = (mnuItem.Text = mnuToolBar.Text)

    mnuItem.Checked = Not mnuItem.Checked
    If ToolBar Then
      tbrDiagfeux.Visible = Not tbrDiagfeux.Visible
    Else
      staDiagfeux.Visible = Not staDiagfeux.Visible
    End If

    If TypeOf mnuItem.Parent Is ContextMenu Then
      If ToolBar Then
        mnuToolBar.Checked = mnuItem.Checked
      Else
        mnuStatusBar.Checked = mnuItem.Checked
      End If
    Else
      frmCourant.RecréerMenuContextuel(Me.mnuAffichage)
    End If

  End Sub

  '**********************************************************************************************************************
  ' Barre d'outils
  '**********************************************************************************************************************
  Private Sub tbrDiagfeux_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbrDiagfeux.ButtonClick
    Dim Index As BarreOutilsEnum = tbrDiagfeux.Buttons.IndexOf(e.Button())
    Select Case Index
      Case BarreOutilsEnum.Nouveau
        mnuNouveau.PerformClick()
      Case BarreOutilsEnum.Ouvrir
        mnuOuvrir.PerformClick()
      Case BarreOutilsEnum.Enregistrer
        mnuEnregistrer.PerformClick()
      Case BarreOutilsEnum.Imprimer
        mnuImprimer.PerformClick()
      Case BarreOutilsEnum.Zoom To BarreOutilsEnum.Mesurer
        If Not IsNothing(frmCourant) Then frmCourant.CommandeZoom(Index)
      Case BarreOutilsEnum.Nord
        mnuNord.PerformClick()
      Case BarreOutilsEnum.Echelle
        mnuEchelle.PerformClick()
    End Select
  End Sub
#End Region

  Private Sub mnuAideRecherche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAideRecherche.Click
    'La doc dit qu'il faut mettre Nothing à param mais çà ne marche pas
    Help.ShowHelp(Me, HelpFile, HelpNavigator.Find, "")

  End Sub

  Private Sub mnuAideSommaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAideSommaire.Click
    Help.ShowHelp(Me, HelpFile, HelpNavigator.TableOfContents, Nothing)

  End Sub

  Private Sub mnuAideSur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAideSur.Click
    If IsNothing(frmCourant) Then
      SendKeys.Send("{F1}")
    Else
      AppelAide(frmCourant)
    End If
  End Sub
End Class
