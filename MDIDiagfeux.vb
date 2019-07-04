
'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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

#Region " Code g�n�r� par le Concepteur Windows Form "

  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()

    'Ajoutez une initialisation quelconque apr�s l'appel InitializeComponent()
    mdiApplication = Me

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
  Friend WithEvents mnuFichier As System.Windows.Forms.MenuItem
  Friend WithEvents mnuNouveau As System.Windows.Forms.MenuItem
  Friend WithEvents mnuFermer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuQuitter As System.Windows.Forms.MenuItem
  Friend WithEvents mnuOuvrir As System.Windows.Forms.MenuItem
  Friend WithEvents mnuParam�trage As System.Windows.Forms.MenuItem
  Friend WithEvents mnuFen�tre As System.Windows.Forms.MenuItem
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
  Friend WithEvents mnuSc�nario As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSc�narioNouveau As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSc�narioDupliquer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSc�narioRenommer As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSc�narioSupprimer As System.Windows.Forms.MenuItem
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
  Friend WithEvents pnlSc�nario As System.Windows.Forms.Panel
  Friend WithEvents lblSc�nario As System.Windows.Forms.Label
  Friend WithEvents cboSc�nario As System.Windows.Forms.ComboBox
  Friend WithEvents lblProjetD�finitif As System.Windows.Forms.Label
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
    Me.mnuParam�trage = New System.Windows.Forms.MenuItem
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
    Me.mnuSc�nario = New System.Windows.Forms.MenuItem
    Me.mnuSc�narioNouveau = New System.Windows.Forms.MenuItem
    Me.mnuSc�narioDupliquer = New System.Windows.Forms.MenuItem
    Me.mnuSc�narioRenommer = New System.Windows.Forms.MenuItem
    Me.mnuSc�narioSupprimer = New System.Windows.Forms.MenuItem
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
    Me.mnuFen�tre = New System.Windows.Forms.MenuItem
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
    Me.pnlSc�nario = New System.Windows.Forms.Panel
    Me.lblProjetD�finitif = New System.Windows.Forms.Label
    Me.lblSc�nario = New System.Windows.Forms.Label
    Me.cboSc�nario = New System.Windows.Forms.ComboBox
    CType(Me.stapnlVerrou, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.stapnlCoord, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlSc�nario.SuspendLayout()
    Me.SuspendLayout()
    '
    'mnuMain
    '
    Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFichier, Me.mnuSc�nario, Me.mnuAffichage, Me.mnuFen�tre, Me.mnuAide})
    '
    'mnuFichier
    '
    Me.mnuFichier.Index = 0
    Me.mnuFichier.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNouveau, Me.mnuOuvrir, Me.mnuFermer, Me.mnuEnregistrer, Me.mnuEnregSous, Me.mnuSepFic1, Me.mnuParam�trage, Me.mnuSepFic3, Me.mnuConfigImpr, Me.mnuImprimer, Me.mnuSepFic2, Me.mnuSelect1, Me.mnuSelect2, Me.mnuSelect3, Me.mnuSelect4, Me.mnuSepFic4, Me.mnuQuitter})
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
    'mnuParam�trage
    '
    Me.mnuParam�trage.Index = 6
    Me.mnuParam�trage.Text = "&Param�trage"
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
    'mnuSc�nario
    '
    Me.mnuSc�nario.Index = 1
    Me.mnuSc�nario.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSc�narioNouveau, Me.mnuSc�narioDupliquer, Me.mnuSc�narioRenommer, Me.mnuSc�narioSupprimer})
    Me.mnuSc�nario.Text = "&Sc�nario"
    Me.mnuSc�nario.Visible = False
    '
    'mnuSc�narioNouveau
    '
    Me.mnuSc�narioNouveau.Index = 0
    Me.mnuSc�narioNouveau.Text = "Nouveau"
    '
    'mnuSc�narioDupliquer
    '
    Me.mnuSc�narioDupliquer.Index = 1
    Me.mnuSc�narioDupliquer.Text = "Dupliquer"
    '
    'mnuSc�narioRenommer
    '
    Me.mnuSc�narioRenommer.Index = 2
    Me.mnuSc�narioRenommer.Text = "Renommer"
    '
    'mnuSc�narioSupprimer
    '
    Me.mnuSc�narioSupprimer.Index = 3
    Me.mnuSc�narioSupprimer.Text = "Suprimer"
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
    Me.mnuStatusBar.Text = "Barre d'�tat"
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
    'mnuFen�tre
    '
    Me.mnuFen�tre.Index = 3
    Me.mnuFen�tre.MdiList = True
    Me.mnuFen�tre.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCascade, Me.mnuHorizontal})
    Me.mnuFen�tre.Text = "Fe&n�tre"
    '
    'mnuCascade
    '
    Me.mnuCascade.Index = 0
    Me.mnuCascade.Text = "Cascade"
    '
    'mnuHorizontal
    '
    Me.mnuHorizontal.Index = 1
    Me.mnuHorizontal.Text = "Mosa�que horizontale"
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
    Me.tbrbtnZoomMoins.ToolTipText = "Zoom arri�re"
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
    Me.tbrbtnZoomAvant.ToolTipText = "Zoom pr�c�dent"
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
    'pnlSc�nario
    '
    Me.pnlSc�nario.Controls.Add(Me.lblProjetD�finitif)
    Me.pnlSc�nario.Controls.Add(Me.lblSc�nario)
    Me.pnlSc�nario.Controls.Add(Me.cboSc�nario)
    Me.pnlSc�nario.Location = New System.Drawing.Point(370, 2)
    Me.pnlSc�nario.Name = "pnlSc�nario"
    Me.pnlSc�nario.Size = New System.Drawing.Size(646, 24)
    Me.pnlSc�nario.TabIndex = 16
    Me.pnlSc�nario.Visible = False
    '
    'lblProjetD�finitif
    '
    Me.lblProjetD�finitif.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblProjetD�finitif.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblProjetD�finitif.ForeColor = System.Drawing.Color.Green
    Me.lblProjetD�finitif.Location = New System.Drawing.Point(344, 5)
    Me.lblProjetD�finitif.Name = "lblProjetD�finitif"
    Me.lblProjetD�finitif.Size = New System.Drawing.Size(56, 20)
    Me.lblProjetD�finitif.TabIndex = 18
    Me.lblProjetD�finitif.Text = "D�finitif"
    '
    'lblSc�nario
    '
    Me.lblSc�nario.Location = New System.Drawing.Point(0, 5)
    Me.lblSc�nario.Name = "lblSc�nario"
    Me.lblSc�nario.Size = New System.Drawing.Size(90, 20)
    Me.lblSc�nario.TabIndex = 17
    Me.lblSc�nario.Text = "Sc�nario courant"
    '
    'cboSc�nario
    '
    Me.cboSc�nario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSc�nario.Location = New System.Drawing.Point(92, 3)
    Me.cboSc�nario.Name = "cboSc�nario"
    Me.cboSc�nario.Size = New System.Drawing.Size(232, 21)
    Me.cboSc�nario.TabIndex = 16
    '
    'MDIDiagfeux
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(1028, 601)
    Me.Controls.Add(Me.pnlSc�nario)
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
    Me.pnlSc�nario.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "D�clarations"
  Public Shared frmCourant As frmCarrefour
  Private CheminParam�trage As String = IO.Path.Combine(CheminDiagfeux, "Diagfeux.par")
  Private ChargementEnCours As Boolean = True

  Public Enum BarreOutilsEnum
    Nouveau
    Ouvrir
    Enregistrer
    Imprimer
    Zoom
    ZoomMoins
    PAN
    ZoomPr�c�dent
    Rafraichir
    Mesurer
    Nord
    Echelle
  End Enum

#End Region
#Region "Fonctions de la feuille"
  '********************************************************************************************************************
  '	Cet �v�nement est d�clench� apr�s l'�v�nement Activated de la feuille fille MDI
  '********************************************************************************************************************
  Private Sub MDIDiagfeux_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate

    frmCourant = ActiveMdiChild

    If IsNothing(frmCourant) Then
      cndVariante = Nothing 'Toutes les feuilles sont ferm�es
      ActiverAffichage(Activ�:=False)

    Else
      ActiverAffichage(Activ�:=cndVariante.ModeGraphique)

      'Activation des boutons de la barre d'outils

      With tbrDiagfeux.Buttons()
        Dim i As MDIDiagfeux.BarreOutilsEnum
        For i = BarreOutilsEnum.Enregistrer To BarreOutilsEnum.PAN
          .Item(i).Visible = True
        Next
        'Le menu interm�diaire Zoom pr�c�dent ne peut �tre rendu visible que contextuellement par la feuille active (cf frmCarrrefour.Activated)
        For i = BarreOutilsEnum.Rafraichir To BarreOutilsEnum.Echelle
          'RAfraichir, Mesurer, Nord et Echelle
          .Item(i).Visible = True
        Next

      End With

      mnuEnregistrer.Enabled = True
      mnuEnregSous.Enabled = True
      mnuImprimer.Enabled = True

      mnuRafraichir.Enabled = True

      'Sc�narios
      AfficherSc�narios()


    End If

    AfficherBarreEtat()

  End Sub

  Public Sub AfficherSc�narios()

    Try

      mnuSc�nario.Visible = True
      cboSc�nario.Items.Clear()

      pnlSc�nario.Visible = cndVariante.mPlansFeuxBase.Count > 0
      Dim unPlanFeux As PlanFeux
      For Each unPlanFeux In cndVariante.mPlansFeuxBase
        cboSc�nario.Items.Add(unPlanFeux.Nom)
      Next
      If cndVariante.Sc�narioEnCours() Then
        cboSc�nario.Text = cndVariante.Sc�narioCourant.Nom
        mnuSc�narioRenommer.Enabled = True
        mnuSc�narioSupprimer.Enabled = True
        mnuSc�narioDupliquer.Enabled = True
      Else
        mnuSc�narioRenommer.Enabled = False
        mnuSc�narioSupprimer.Enabled = False
        mnuSc�narioDupliquer.Enabled = False
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "AfficherSc�narios")

    End Try
  End Sub

  Private Sub MDIDiagfeux_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    If ProtectionOK() Then
      If IO.File.Exists(CheminParam�trage) Then
        Try
          ds = New DataSetDiagFeux
          ds.ReadXml(CheminParam�trage, XmlReadMode.ReadSchema)

          If ds.Param�trage.Rows.Count = 0 Then
            ds.Dispose()
            mnuParam�trage.PerformClick()
            ds.ReadXml(CheminParam�trage, XmlReadMode.ReadSchema)
            If ds.Param�trage.Rows.Count = 0 Then
              ds.Dispose()
              Me.Close()
            End If

          Else
            LireParam�trage()
            ds.Dispose()

            'Dim nb As Short = ds.TableCycleCapacit�.Rows.Count
            'If nb = 0 Then
            '  LecModuleClassique()
            '  EcrireParam�trage()
            'Else
            '  LireParam�trage()
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
        mnuParam�trage.PerformClick()
        If Not IO.File.Exists(CheminParam�trage) Then Me.Close()
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
    ' V�rification de l'enregistrement
    If ProtectCheck("its00+-k") = "its00+-k" Then
      ' Protection OK
      Return True
    Else 'la licence n'a pas �t� valid�e on ferme
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
    ' Suppression dans la registry des fichiers effac�s ( MRUmenu les a ignor�s � l'ouverture de DIAGFEUX, mais ils sont tjs pr�sents dans la registry)
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

    'Remplir un espace gris� (sans contour) pour l'entete
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
  ' Appeler la boite de dialogue de saisie des caract�ristiques du carrefourr
  '********************************************************************************************************************
  Private Function CarGen(ByVal dlg As dlgCarGen, ByVal uneVariante As Variante) As Variante

    With dlg
      .mVariante = uneVariante
      If .ShowDialog(Me) = DialogResult.OK Then
        'Mettre � jour les donn�es du carrefour avec les donn�es saisies dans la boite
        .MettreAjour()
        Return .mVariante
      Else
        Return Nothing
      End If

    End With

  End Function

  '********************************************************************************************************************
  '	Instancier et initialiser la fen�tre Carrefour correspondant � la nouvelle variante
  '********************************************************************************************************************
  Private Sub FenetreCarrefour()
    Dim frm As New frmCarrefour

    Try

      With frm
        .MdiParent = Me
        frmCourant = frm
        If Not cndVariante.ModeGraphique Then
          'D�sactiver les fonctions inutilis�es dans le mode d�grad�
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
  Private Sub mnuParam�trage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuParam�trage.Click
    Dim dlg As New dlgParam�trage

    With dlg
      .ShowDialog(Me)
      Select Case .DialogResult
        Case DialogResult.OK
          If .chkConserverD�faut.Checked Then
            'la case ConserverD�faut est syt�matiquement coch�e lors de la 1�re utilisation de DIAGFEUX - ensuite, selon le choix utilisateur
            Try
              EcrireParam�trage()

            Catch ex As System.Exception
              AfficherMessageErreur(Nothing, ex)
            End Try
          End If

          If TypeOf .mObjetM�tier Is PlanFeuxBase Then
            frmCourant.R�initialiserPhasages()

          ElseIf TypeOf .mObjetM�tier Is PlanFeuxFonctionnement Then
            frmCourant.RecalculerCapacit�s()
          End If

          If .mSignalPi�tonChang� AndAlso Not IsNothing(frmCourant) Then
            frmCourant.Recr�erGraphique()
            frmCourant.Redessiner()
            frmCourant.Modif = True
          End If

        Case DialogResult.Cancel
          'MessageBox.Show("Annuler", "Param�trage", MessageBoxButtons.OK)
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

  '  ReDim TbCycleCapacit�(15, 23)

  '  fs = New FileStream(Nomfich, FileMode.Open, FileAccess.Read, FileShare.Read, 32768)
  '  strRead = New StreamReader(fs)

  '  Try
  '    For i = 0 To TbCycleCapacit�.GetUpperBound(0)
  '      j = 0
  '      Ligne = strRead.ReadLine
  '      pos = 0
  '      pos2 = Ligne.IndexOf(vbTab, pos)

  '      Do While pos2 <> -1
  '        TbCycleCapacit�(i, j) = CType(Mid(Ligne, pos + 1, pos2 - pos), Short)
  '        pos = pos2 + 1
  '        pos2 = Ligne.IndexOf(vbTab, pos)
  '        j += 1
  '      Loop

  '      TbCycleCapacit�(i, j) = CType(Mid(Ligne, pos + 1), Short)

  '    Next

  '  Catch ex As System.Exception
  '    AfficherMessageErreur(Nothing, ex)
  '  Finally
  '    fs.Close()
  '  End Try

  'End Sub

  Private Sub LireParam�trage()
    Try
      Dim uneRowParam�trage As DataSetDiagFeux.Param�trageRow = ds.Param�trage.Rows(0)
      With uneRowParam�trage
        Try
          cndParam�tres.VersionFichier = VersionFichier
        Catch ex As StrongTypingException
          'Version non renseign�e : < v13
          cndParam�tres.TempsPerduD�marrageAgglo = 0
          cndParam�tres.TempsPerduD�marrageCampagne = 0
          cndParam�tres.TempsJauneInutilis�Agglo = [Global].JauneAgglo
          cndParam�tres.TempsJauneInutilis�Campagne = [Global].JauneCampagne
          cndParam�tres.SignalPi�tonsSonore = True
        End Try
        cndParam�tres.Organisme = .Organisme
        cndParam�tres.Service = .Service
        If Not .IsStockageNull Then
          cndParam�tres.CheminStockage = .Stockage
        End If
        If Not .IsLogoNull Then
          cndParam�tres.CheminLogo = .Logo
        End If
        cndParam�tres.VitessePi�tons = .VitessePi�tons
        cndParam�tres.VitesseV�hicules = .VitesseV�hicules
        cndParam�tres.VitesseV�los = .VitesseV�los
        cndParam�tres.D�bitSaturation = .D�bitSaturation
        'v12 et ant�rieures
        'cndParam�tres.D�calageVertUtile = .VertUtile
        If VersionFichier > 0 Then
          cndParam�tres.TempsPerduD�marrageAgglo = .TempsPerduD�marrage
          cndParam�tres.TempsPerduD�marrageCampagne = .TempsPerduD�marrageCampagne
          cndParam�tres.TempsJauneInutilis�Agglo = .JauneInutilis�Agglo
          cndParam�tres.TempsJauneInutilis�Campagne = .JauneInutilis�Campagne
          cndParam�tres.SignalPi�tonsSonore = .SignalPi�tonsSonore
        End If
      End With
      cndParam�tres.CheminFDP = IO.Directory.GetCurrentDirectory

      'If ds.TableCycleCapacit�.Rows.Count = 0 Then
      'Else
      '  Dim uneRowCapacit� As DataSetDiagfeux.TableCycleCapacit�Row = ds.TableCycleCapacit�.Rows(0)
      '  With uneRowCapacit�
      '    Dur�eCycleMini = .Dur�eCycleMini
      '    TempsPerduMini = .TempsPerduMini
      '    nbCycles = .GetDur�eCycleRows.Length
      '    For i = 0 To nbCycles - 1
      '      uneRowDur�eCycle = .GetDur�eCycleRows(i)
      '      If i = 0 Then
      '        nbTempsPerdus = uneRowDur�eCycle.GetDemandeCarrefourRows.Length
      '        ReDim TbCycleCapacit�(nbCycles - 1, nbTempsPerdus - 1)
      '      End If
      '      For j = 0 To nbTempsPerdus - 1
      '        uneRowDemandeCarrefour = uneRowDur�eCycle.GetDemandeCarrefourRows(j)
      '        TbCycleCapacit�(i, j) = uneRowDemandeCarrefour.DemandeCarrefour_Column
      '      Next
      '    Next
      '  End With
      'End If

    Catch ex As System.Exception

    End Try

  End Sub

  Private Sub EcrireParam�trage()

    ds = New DataSetDiagfeux
    Dim uneRowParam�trage As DataSetDiagfeux.Param�trageRow = ds.Param�trage.NewParam�trageRow
    With uneRowParam�trage
      .Version = cndParam�tres.VersionFichier
      .Organisme = cndParam�tres.Organisme
      .Service = cndParam�tres.Service
      .Stockage = cndParam�tres.CheminStockage
      .Logo = cndParam�tres.CheminLogo
      .VitessePi�tons = cndParam�tres.VitessePi�tons
      .VitesseV�hicules = cndParam�tres.VitesseV�hicules
      .VitesseV�los = cndParam�tres.VitesseV�los
      .D�bitSaturation = cndParam�tres.D�bitSaturation
      'v12 et ant�rieures
      '.VertUtile = cndParam�tres.D�calageVertUtile
      'v13
      .TempsPerduD�marrage = cndParam�tres.TempsPerduD�marrageAgglo
      .TempsPerduD�marrageCampagne = cndParam�tres.TempsPerduD�marrageCampagne
      .JauneInutilis�Agglo = cndParam�tres.TempsJauneInutilis�Agglo
      .JauneInutilis�Campagne = cndParam�tres.TempsJauneInutilis�Campagne
      .SignalPi�tonsSonore = cndParam�tres.SignalPi�tonsSonore
    End With
    ds.Param�trage.AddParam�trageRow(uneRowParam�trage)

    'Dim uneRowCapacit� As DataSetDiagfeux.TableCycleCapacit�Row = ds.TableCycleCapacit�.NewTableCycleCapacit�Row
    'Dim uneRowDur�eCycle As DataSetDiagfeux.Dur�eCycleRow
    'Dim uneRowDemandeCarrefour As DataSetDiagfeux.DemandeCarrefourRow
    'Dim DemandeCarrefour As Short
    'Dim i, j As Short
    'With uneRowCapacit�
    '  .Dur�eCycleMini = 45
    '  .TempsPerduMini = 7
    '  ds.TableCycleCapacit�.AddTableCycleCapacit�Row(uneRowCapacit�)
    '  For i = 0 To TbCycleCapacit�.GetUpperBound(0)
    '    uneRowDur�eCycle = ds.Dur�eCycle.NewDur�eCycleRow
    '    uneRowDur�eCycle.SetParentRow(uneRowCapacit�)
    '    ds.Dur�eCycle.AddDur�eCycleRow(uneRowDur�eCycle)
    '    For j = 0 To TbCycleCapacit�.GetUpperBound(1)
    '      uneRowDemandeCarrefour = ds.DemandeCarrefour.NewDemandeCarrefourRow
    '      uneRowDemandeCarrefour.DemandeCarrefour_Column = TbCycleCapacit�(i, j)
    '      ds.DemandeCarrefour.AddDemandeCarrefourRow(TbCycleCapacit�(i, j), uneRowDur�eCycle)
    '    Next
    '  Next
    'End With

    ds.WriteXml(CheminParam�trage, mode:=XmlWriteMode.WriteSchema)
    ds.Dispose()

  End Sub

  Private Sub mnuQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQuitter.Click
    Me.Close()
  End Sub

  Private Sub mnuImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImprimer.Click

    If Not IsNothing(frmCourant) Then
      If cndVariante.Sc�narioEnCours Then
        Dim dlg As New dlgImpressions
        With dlg
          .ShowDialog(Me)
        End With

      Else
        MessageBox.Show("Il faut d'abord cr�er un sc�nario")
      End If
    End If
  End Sub

  Private Sub mnuConfigImpr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfigImpr.Click
    Dim dlg As New PrintDialog

    With dlg
      If IsNothing(cndPrintDocument) Then
        'Peut �te Nothing suite � fermeture de dlgImpressions (voir son �v�nement Closed)
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
      cndVariante.Verrou = [Global].Verrouillage.G�om�trie
      FenetreCarrefour()
    End If

    dlg.Dispose()

  End Sub

  Private Sub mnuOuvrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOuvrir.Click

    Dim NomFichier As String = DialogueFichier(Outils.TypeDialogueEnum.Ouvrir, Filtre:=ComposerFiltre(etuExtension), DefaultExt:=etuExtension)

    If Not IsNothing(NomFichier) Then
      If cndVariantes.VarianteOuverte(NomFichier) Then
        MessageBox.Show("Ce fichier est d�j� ouvert")
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
#Region "Menu Fen�tre"
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
    frmCourant.Recr�erMenuContextuel(Me.mnuAffichage)
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
      cndVariante.NordAffich� = mnuItem.Checked
      frmCourant.Redessiner()
    ElseIf mnuItem Is mnuEchelle Then
      cndVariante.EchelleAffich�e = mnuItem.Checked
      frmCourant.Redessiner()
    End If

  End Sub

  '**********************************************************************
  'Les items du Menu Affichage ne sont visibles que si au moins une
  'fen�tre carrefour est active, et que sa variante est en mode graphique
  '**********************************************************************
  Private Sub ActiverAffichage(ByVal Activ� As Boolean)

    mnuSensTrajectoires.Visible = Activ�
    mnuSensCirculation.Visible = Activ�
    mnuNord.Visible = Activ�
    mnuEchelle.Visible = Activ�

    If Not IsNothing(cndVariante) AndAlso Not cndVariante.ModeGraphique Then
      'Toujours rendre accessible le sens de circulation en mode tableur : c'est le seul indicateur disponible
      mnuSensCirculation.Visible = True
    End If

    mnuSepAffichage2.Visible = Activ�

  End Sub

#End Region
#Region "Menu Sc�nario"
  Private Sub mnuSc�nario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSc�nario.Click
    If Not cndVariante.VerrouG�om Then
      AfficherMessageErreur(Nothing, "Il faut d'abord verrouiller la g�om�trie")
    End If
  End Sub

  Private Sub mnuSc�narioNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSc�narioNouveau.Click
    Dim dlg As New dlgSc�nario

    If Not cndVariante.OngletInterdit([Global].OngletEnum.Trafics) Then
      With dlg
        .maVariante = cndVariante
        If .ShowDialog = DialogResult.OK Then
          frmCourant.NouveauSc�nario()
        End If
        .Dispose()
      End With
    End If

  End Sub

  Private Sub mnuSc�narioDupliquer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSc�narioDupliquer.Click
    frmCourant.DupliquerSc�nario()
  End Sub

  Private Sub mnuSc�narioRenommer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSc�narioRenommer.Click
    frmCourant.RenommerSc�nario()
  End Sub

  Private Sub mnuSc�narioSupprimer_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSc�narioSupprimer.Click
    frmCourant.SupprimerSc�nario()
  End Sub

  Private Sub cboSc�nario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSc�nario.SelectedIndexChanged
    frmCourant.S�lectionnerSc�nario(cboSc�nario.SelectedIndex)
  End Sub

#End Region
#Region "Menu Derniers fichiers"
  Private Sub mnuSelect1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelect1.Click, mnuSelect2.Click, mnuSelect3.Click, mnuSelect4.Click
    Dim mnu As MenuItem = sender
    Dim Index As Short = Val(mnu.Mnemonic) - 1
    Dim NomFichier As String = MRUFichiers(Index)

    If cndVariantes.VarianteOuverte(NomFichier) Then
      MessageBox.Show("Ce fichier est d�j� ouvert")
    ElseIf Not IO.File.Exists(NomFichier) Then
    Else
      Ouvrir(NomFichier)
    End If

  End Sub

  Public Sub MRUmenu(ByVal NomFichier As String)
    ' Affichage dans le menu des derniers fichiers utilis�s (MRU)
    Dim i, j As Short
    Const MAXFICH As Short = 4

    If IO.File.Exists(NomFichier) Then

      For i = 0 To nbfichMenu - 1
        If UCase(NomFichier) = UCase(MRUFichiers(i)) Then
          ' Le fichier est d�j� dans la liste, on va le remettre en t�te
          For j = i To nbfichMenu - 2
            ' Resserrement du tableau en supprimant le fichier
            MRUFichiers(j) = MRUFichiers(j + 1)
          Next
          nbfichMenu = nbfichMenu - 1
        End If
      Next

      If nbfichMenu < MAXFICH Then    ' Debug: cette valeur pourra �tre param�tr�e (n'apparait qu'ici), mais sans d�passer la taille de mnuSelect
        mnuSelect(nbfichMenu).Visible = True
        If nbfichMenu = 0 Then mnuSepFic4.Visible = True 'Cr�ation de la ligne de s�paration
        ReDim Preserve MRUFichiers(nbfichMenu)
        nbfichMenu = nbfichMenu + 1
      End If

      For i = nbfichMenu - 1 To 1 Step -1
        ' D�calage des fichiers pour ins�rer le nouveau en t�te
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
      For intSettings = UBound(MySettings, 1) To 0 Step -1     ' On les lit  � l'envers, car MRUmenu les ajoute par d�calage
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
#Region "Barres d'outils, d'�tat"
  Public Sub AfficherBarreEtat()
    If IsNothing(cndVariante) Then
      Me.staDiagfeux.Panels(0).Text = ""
    Else
      Me.staDiagfeux.Panels(0).Text = cndVariante.Libell�Verrouillage
    End If
  End Sub

  '**********************************************************************************************************************
  ' Barre d'outils-Barre d'�tat visible/invisible
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
      frmCourant.Recr�erMenuContextuel(Me.mnuAffichage)
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
    'La doc dit qu'il faut mettre Nothing � param mais �� ne marche pas
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
