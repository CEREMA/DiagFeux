'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : dlgImpressions.vb										  											'
'						Classes																														'
'							dlgImpressions : Dialogue               												'
'																																							'
'******************************************************************************
Imports System.Drawing.Printing

Public Class dlgImpressions
  Inherits DiagFeux.frmDlg

#Region " Code g�n�r� par le Concepteur Windows Form "

  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()

    'Ajoutez une initialisation quelconque apr�s l'appel InitializeComponent()
    ReDim TablePages(-1)
    nbPages = 0
    numPage = 0
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
  Friend WithEvents btnDonn�es As System.Windows.Forms.Button
  Friend WithEvents chkPlanCarrefour As System.Windows.Forms.CheckBox
  Friend WithEvents chkTrafics As System.Windows.Forms.CheckBox
  Friend WithEvents chkDiagramme As System.Windows.Forms.CheckBox
  Friend WithEvents chkPlanDeFeux As System.Windows.Forms.CheckBox
  Friend WithEvents chkMatrice As System.Windows.Forms.CheckBox
  Friend WithEvents chkListePlans As System.Windows.Forms.CheckBox
  Friend WithEvents chkEnsemble As System.Windows.Forms.CheckBox
  Friend WithEvents btnImprimante As System.Windows.Forms.Button
  Friend WithEvents tipBulle As System.Windows.Forms.ToolTip
  Friend WithEvents chkDiagnostic As System.Windows.Forms.CheckBox
  Friend WithEvents btnLogo As System.Windows.Forms.Button
  Friend WithEvents chkLogo As System.Windows.Forms.CheckBox
  Friend WithEvents radD�finitif As System.Windows.Forms.RadioButton
  Friend WithEvents radProjet As System.Windows.Forms.RadioButton
  Friend WithEvents lblSc�nario As System.Windows.Forms.Label
  Friend WithEvents pnlSc�nario As System.Windows.Forms.Panel
  Friend WithEvents cboSc�narios As System.Windows.Forms.ComboBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.chkPlanCarrefour = New System.Windows.Forms.CheckBox
    Me.chkTrafics = New System.Windows.Forms.CheckBox
    Me.chkDiagramme = New System.Windows.Forms.CheckBox
    Me.chkPlanDeFeux = New System.Windows.Forms.CheckBox
    Me.chkMatrice = New System.Windows.Forms.CheckBox
    Me.chkListePlans = New System.Windows.Forms.CheckBox
    Me.btnDonn�es = New System.Windows.Forms.Button
    Me.btnImprimante = New System.Windows.Forms.Button
    Me.chkEnsemble = New System.Windows.Forms.CheckBox
    Me.tipBulle = New System.Windows.Forms.ToolTip(Me.components)
    Me.btnLogo = New System.Windows.Forms.Button
    Me.chkDiagnostic = New System.Windows.Forms.CheckBox
    Me.chkLogo = New System.Windows.Forms.CheckBox
    Me.pnlSc�nario = New System.Windows.Forms.Panel
    Me.cboSc�narios = New System.Windows.Forms.ComboBox
    Me.lblSc�nario = New System.Windows.Forms.Label
    Me.radProjet = New System.Windows.Forms.RadioButton
    Me.radD�finitif = New System.Windows.Forms.RadioButton
    Me.pnlSc�nario.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(314, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.Size = New System.Drawing.Size(78, 24)
    Me.btnAnnuler.TabIndex = 12
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(312, 176)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(314, 16)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(78, 24)
    Me.btnOK.TabIndex = 11
    '
    'chkPlanCarrefour
    '
    Me.chkPlanCarrefour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkPlanCarrefour.Location = New System.Drawing.Point(16, 136)
    Me.chkPlanCarrefour.Name = "chkPlanCarrefour"
    Me.chkPlanCarrefour.Size = New System.Drawing.Size(112, 16)
    Me.chkPlanCarrefour.TabIndex = 2
    Me.chkPlanCarrefour.Text = "Plan du Carrefour"
    '
    'chkTrafics
    '
    Me.chkTrafics.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkTrafics.Location = New System.Drawing.Point(16, 192)
    Me.chkTrafics.Name = "chkTrafics"
    Me.chkTrafics.Size = New System.Drawing.Size(176, 16)
    Me.chkTrafics.TabIndex = 4
    Me.chkTrafics.Text = "Matrices de Trafics"
    '
    'chkDiagramme
    '
    Me.chkDiagramme.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkDiagramme.Enabled = False
    Me.chkDiagramme.Location = New System.Drawing.Point(16, 224)
    Me.chkDiagramme.Name = "chkDiagramme"
    Me.chkDiagramme.Size = New System.Drawing.Size(152, 16)
    Me.chkDiagramme.TabIndex = 5
    Me.chkDiagramme.Text = "Diagramme des phases"
    '
    'chkPlanDeFeux
    '
    Me.chkPlanDeFeux.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkPlanDeFeux.Enabled = False
    Me.chkPlanDeFeux.Location = New System.Drawing.Point(16, 320)
    Me.chkPlanDeFeux.Name = "chkPlanDeFeux"
    Me.chkPlanDeFeux.Size = New System.Drawing.Size(96, 16)
    Me.chkPlanDeFeux.TabIndex = 8
    Me.chkPlanDeFeux.Text = "Plans de feux"
    '
    'chkMatrice
    '
    Me.chkMatrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkMatrice.Enabled = False
    Me.chkMatrice.Location = New System.Drawing.Point(16, 256)
    Me.chkMatrice.Name = "chkMatrice"
    Me.chkMatrice.Size = New System.Drawing.Size(208, 16)
    Me.chkMatrice.TabIndex = 7
    Me.chkMatrice.Text = "Matrice des rouges de d�gagement"
    '
    'chkListePlans
    '
    Me.chkListePlans.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkListePlans.Enabled = False
    Me.chkListePlans.Location = New System.Drawing.Point(16, 288)
    Me.chkListePlans.Name = "chkListePlans"
    Me.chkListePlans.Size = New System.Drawing.Size(144, 16)
    Me.chkListePlans.TabIndex = 6
    Me.chkListePlans.Text = "Liste des Plans de feux"
    '
    'btnDonn�es
    '
    Me.btnDonn�es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnDonn�es.Location = New System.Drawing.Point(312, 96)
    Me.btnDonn�es.Name = "btnDonn�es"
    Me.btnDonn�es.Size = New System.Drawing.Size(80, 24)
    Me.btnDonn�es.TabIndex = 13
    Me.btnDonn�es.Text = "Information..."
    Me.tipBulle.SetToolTip(Me.btnDonn�es, "Saisie d'informations compl�mentaires pour l'impression")
    '
    'btnImprimante
    '
    Me.btnImprimante.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnImprimante.Location = New System.Drawing.Point(312, 136)
    Me.btnImprimante.Name = "btnImprimante"
    Me.btnImprimante.Size = New System.Drawing.Size(80, 24)
    Me.btnImprimante.TabIndex = 14
    Me.btnImprimante.Text = "Imprimante..."
    '
    'chkEnsemble
    '
    Me.chkEnsemble.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkEnsemble.Location = New System.Drawing.Point(16, 368)
    Me.chkEnsemble.Name = "chkEnsemble"
    Me.chkEnsemble.Size = New System.Drawing.Size(80, 16)
    Me.chkEnsemble.TabIndex = 10
    Me.chkEnsemble.Text = "Ensemble"
    '
    'btnLogo
    '
    Me.btnLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnLogo.Location = New System.Drawing.Point(16, 8)
    Me.btnLogo.Name = "btnLogo"
    Me.btnLogo.Size = New System.Drawing.Size(88, 80)
    Me.btnLogo.TabIndex = 0
    Me.btnLogo.Text = "Logo ..."
    Me.tipBulle.SetToolTip(Me.btnLogo, "Logo du service")
    '
    'chkDiagnostic
    '
    Me.chkDiagnostic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkDiagnostic.Enabled = False
    Me.chkDiagnostic.Location = New System.Drawing.Point(48, 336)
    Me.chkDiagnostic.Name = "chkDiagnostic"
    Me.chkDiagnostic.TabIndex = 9
    Me.chkDiagnostic.Text = "Diagnostic"
    '
    'chkLogo
    '
    Me.chkLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkLogo.Enabled = False
    Me.chkLogo.Location = New System.Drawing.Point(56, 160)
    Me.chkLogo.Name = "chkLogo"
    Me.chkLogo.TabIndex = 3
    Me.chkLogo.Text = "Logo"
    '
    'pnlSc�nario
    '
    Me.pnlSc�nario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlSc�nario.Controls.Add(Me.cboSc�narios)
    Me.pnlSc�nario.Controls.Add(Me.lblSc�nario)
    Me.pnlSc�nario.Controls.Add(Me.radProjet)
    Me.pnlSc�nario.Controls.Add(Me.radD�finitif)
    Me.pnlSc�nario.Location = New System.Drawing.Point(112, 8)
    Me.pnlSc�nario.Name = "pnlSc�nario"
    Me.pnlSc�nario.Size = New System.Drawing.Size(176, 112)
    Me.pnlSc�nario.TabIndex = 0
    '
    'cboSc�narios
    '
    Me.cboSc�narios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSc�narios.Location = New System.Drawing.Point(0, 80)
    Me.cboSc�narios.Name = "cboSc�narios"
    Me.cboSc�narios.Size = New System.Drawing.Size(160, 21)
    Me.cboSc�narios.TabIndex = 3
    '
    'lblSc�nario
    '
    Me.lblSc�nario.Location = New System.Drawing.Point(16, 8)
    Me.lblSc�nario.Name = "lblSc�nario"
    Me.lblSc�nario.Size = New System.Drawing.Size(64, 16)
    Me.lblSc�nario.TabIndex = 0
    Me.lblSc�nario.Text = "Sc�nario(s)"
    '
    'radProjet
    '
    Me.radProjet.Location = New System.Drawing.Point(16, 56)
    Me.radProjet.Name = "radProjet"
    Me.radProjet.Size = New System.Drawing.Size(80, 16)
    Me.radProjet.TabIndex = 2
    Me.radProjet.Text = "Projet(s)"
    '
    'radD�finitif
    '
    Me.radD�finitif.Location = New System.Drawing.Point(16, 32)
    Me.radD�finitif.Name = "radD�finitif"
    Me.radD�finitif.Size = New System.Drawing.Size(64, 16)
    Me.radD�finitif.TabIndex = 1
    Me.radD�finitif.Text = "D�finitif"
    '
    'dlgImpressions
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(402, 399)
    Me.Controls.Add(Me.pnlSc�nario)
    Me.Controls.Add(Me.chkLogo)
    Me.Controls.Add(Me.btnLogo)
    Me.Controls.Add(Me.chkDiagnostic)
    Me.Controls.Add(Me.chkEnsemble)
    Me.Controls.Add(Me.btnImprimante)
    Me.Controls.Add(Me.btnDonn�es)
    Me.Controls.Add(Me.chkListePlans)
    Me.Controls.Add(Me.chkDiagramme)
    Me.Controls.Add(Me.chkTrafics)
    Me.Controls.Add(Me.chkPlanCarrefour)
    Me.Controls.Add(Me.chkPlanDeFeux)
    Me.Controls.Add(Me.chkMatrice)
    Me.Name = "dlgImpressions"
    Me.Text = "Impressions"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.chkMatrice, 0)
    Me.Controls.SetChildIndex(Me.chkPlanDeFeux, 0)
    Me.Controls.SetChildIndex(Me.chkPlanCarrefour, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.chkTrafics, 0)
    Me.Controls.SetChildIndex(Me.chkDiagramme, 0)
    Me.Controls.SetChildIndex(Me.chkListePlans, 0)
    Me.Controls.SetChildIndex(Me.btnDonn�es, 0)
    Me.Controls.SetChildIndex(Me.btnImprimante, 0)
    Me.Controls.SetChildIndex(Me.chkEnsemble, 0)
    Me.Controls.SetChildIndex(Me.chkDiagnostic, 0)
    Me.Controls.SetChildIndex(Me.btnLogo, 0)
    Me.Controls.SetChildIndex(Me.chkLogo, 0)
    Me.Controls.SetChildIndex(Me.pnlSc�nario, 0)
    Me.pnlSc�nario.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region


#Region "D�clarations"
  Private WithEvents mPrintDocument As New Printing.PrintDocument

  Private PlansFonctionnement As PlanFeuxCollection
  Private Aper�uDialogue As New PrintPreviewDialog

  'Coefficient de conversion millim�tres et 1/100� de pouces ( 297mm = 11,69 pouces)
  Private Const FacteurPoucesMillim�tres As Single = 297 / 1169

  Private maFonte As New Font("Arial", 9)
  Private maBrosse = New SolidBrush(Color.Black)
  Private maPlumeCadre As New Pen(Color.Blue, Width:=0.2)

  Private monCarrefour As Carrefour
  Private pCourante As Point
  Private EventPage As Printing.PrintPageEventArgs
  Private maVariante As Variante
  Private Interligne As Short = 4

  'Sc�nario s�lectionn� pour l'impression
  Private PlanFeuxBaseActif As PlanFeuxBase
  Private TraficActif As Trafic
  Private PlanFeuxFctActif As PlanFeuxFonctionnement

  Private mParamDessin, sParamDessin As ParamDessin
  Private mRectangleUtile As Rectangle
  Private sContexte As [Global].OngletEnum
  Private sCheminLogo As String
  Private sFDPVisible As Boolean
  Private ImpressionAppel�e As Boolean

  Private Const LargeurBandeau As Short = 45
  Private Const HauteurPiedDePage As Short = 8
  Private Const HauteurEnteteDePage As Short = 25
  Private LargeurUtile, HauteurUtile, Cot�Vignette As Short

  Private PourVignette As Boolean
  Private ZoneGraphique As Rectangle
  Private ZoneGraphiqueOeil As Rectangle

  ' Collection des objets graphiques repr�sentant les objets m�tiers du projet : Objets � dessiner
  Private colObjetsGraphiques As New Graphiques

  Private CheminLogo As String
  Private mBitmapLogo As Bitmap
  Private mTailleBoutonLogo As Size

  Public Rechargement As Boolean
  Private numPage, nbPages As Short

  Private Enum MargeEnum
    Haut
    Gauche
    Bas
    Droite
  End Enum

  Private Enum RectangleEnum
    Hauteur
    Largeur
    Haut
    Gauche
    Bas
    Droite
  End Enum

  Public Enum ImpressionEnum
    Aucun
    ListeProjets
    PlanCarrefour
    Trafic
    DiagrammePhases
    Matrice
    ListePlans
    PlanDeFeux
    Diagnostic
  End Enum

  Private Mod�le As ImpressionEnum

  Private PremPage(ImpressionEnum.PlanDeFeux) As Short
  Private TablePages() As Short
  Private TablePlans As New Hashtable
#End Region

  '***************************************************************************************
  ' Chargement de la feuille
  '***************************************************************************************
  Private Sub dlgImpressions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim unSc�nario As PlanFeuxBase


    maVariante = cndVariante

    With maVariante
      PlansFonctionnement = .ListePlansFonctionnement

      Me.chkTrafics.Enabled = .mTrafics.Count > 0

      For Each unSc�nario In .mPlansFeuxBase
        If unSc�nario.Projet Then
          Me.cboSc�narios.Items.Add(unSc�nario.Nom)
        End If
      Next

      If Me.cboSc�narios.Items.Count > 1 Then
        Me.cboSc�narios.Items.Add("Tous les sc�narios projets")
      End If

      Me.radD�finitif.Enabled = Not IsNothing(.Sc�narioD�finitif)
      If .Sc�narioCourant.D�finitif Then
        Me.radD�finitif.Checked = True
      Else
        Me.radProjet.Checked = True
        Me.cboSc�narios.Text = .Sc�narioCourant.Nom
      End If

      CheminLogo = ImageRaster.FichierExistant(.CheminLogo, QuestionSiAbsent:=Not Rechargement)
      If Not IsNothing(CheminLogo) Then
        Me.chkLogo.Checked = True
        'Au cas o� la fonction CheminExistant � retourn� un chemin diff�rent du pr�c�dent
        .CheminLogo = CheminLogo
      End If
      'M�moriser la taille initiale du bouton Logo
      mTailleBoutonLogo = Me.btnLogo.Size

      monCarrefour = .mCarrefour
    End With

    btnOK.Text = "Imprimer"
    Me.btnOK.DialogResult = DialogResult.None

    'Sauvegarder les contextes pour les r�tablir en sortie
    sParamDessin = cndParamDessin
    sContexte = cndContexte
    cndContexte = [Global].OngletEnum.Conflits
    sCheminLogo = CheminLogo

    'Ajout en v12 : a priori on dessine toujours le fond de plan, m�me s'il n'est pas affich� (Menu Affichage)
    If Not IsNothing(maVariante.mFondDePlan) Then
      sFDPVisible = maVariante.mFondDePlan.Visible
      maVariante.mFondDePlan.Visible = True
    End If

    If IsNothing(cndPrintDocument) Then
      'Peut �te Nothing suite � fermeture de dlgImpressions (voir l'�v�nement Closed)
      mPrintDocument = New Printing.PrintDocument
      mPrintDocument.PrinterSettings.PrinterName = NomImprimante
    Else
      mPrintDocument = cndPrintDocument
    End If

  End Sub

  Private Sub btnDonn�es_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDonn�es.Click
    mdiApplication.SaisirInfoImprim()
  End Sub

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

    If TousLesProjets() Then
      'Imprimer le r�capitulatif des sc�narios projets sur une page
      ImprimerSc�nario()

      For Each PlanFeuxBaseActif In maVariante.mPlansFeuxBase
        'Imprimer tous les sc�narios projet
        If PlanFeuxBaseActif.Projet Then
          ImprimerSc�nario()
        End If
      Next
      'R�activer le drapeau
      PlanFeuxBaseActif = Nothing

    Else
      ImprimerSc�nario()
    End If
  End Sub

  Private Function TousLesProjets() As Boolean
    Return IsNothing(PlanFeuxBaseActif)
  End Function

  Private Sub ImprimerSc�nario()

    Try

      nbPages = 0
      numPage = 0
      ReDim TablePages(-1)

      If TousLesProjets() Then
        'Liste des sc�narios projet
        Cr�erBlocPages(1, ImpressionEnum.ListeProjets)

      Else
        With PlanFeuxBaseActif
          PlansFonctionnement = .mPlansFonctionnement

          If Me.chkPlanCarrefour.Checked Then
            Cr�erBlocPages(1, ImpressionEnum.PlanCarrefour)
          End If

          If chkActiv�(chkTrafics) Then
            Cr�erBlocPages(.TraficsImprimables.Count, ImpressionEnum.Trafic)
          End If

          If chkActiv�(chkDiagramme) Then
            Cr�erBlocPages(1, ImpressionEnum.DiagrammePhases)
          End If

          If chkActiv�(chkMatrice) Then
            Cr�erBlocPages(1, ImpressionEnum.Matrice)
          End If

          If chkActiv�(chkListePlans) Then
            Cr�erBlocPages(1, ImpressionEnum.ListePlans)
          End If

          If chkActiv�(chkPlanDeFeux) Then
            If chkActiv�(chkDiagnostic) Then
              Cr�erBlocPages(.mPlansFonctionnement.Count + .NbPfAvecTrafic, ImpressionEnum.PlanDeFeux)
            Else
              Cr�erBlocPages(.mPlansFonctionnement.Count, ImpressionEnum.PlanDeFeux)
            End If
          End If
        End With
      End If

      If nbPages > 0 Then
        With mPrintDocument
          Dim ps As New PageSettings(.PrinterSettings)
          ps.Landscape = True
          .DefaultPageSettings = ps
#If Not Debug Then
          'Version diffus�e : sortie imprimante
          .Print()
#End If
        End With

        With Aper�uDialogue
          .Document = mPrintDocument
          numPage = 0
          Application.DoEvents()
#If DEBUG Then
          'Version de travail : sortie aper�u
          .ShowDialog()
#End If
        End With

        ImpressionAppel�e = True
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try


  End Sub

  Private Sub Cr�erBlocPages(ByVal nbPagesBloc As Short, ByVal Mod�leImpression As ImpressionEnum)
    Dim i As Short
    Dim IndicePr�c�dent As Short = nbPages
    Dim unPlanFeux As PlanFeuxFonctionnement

    nbPages += nbPagesBloc
    ReDim Preserve TablePages(nbPages - 1)

    If Mod�leImpression = ImpressionEnum.PlanDeFeux And Me.chkDiagnostic.Checked Then
      i = IndicePr�c�dent
      For Each unPlanFeux In PlanFeuxBaseActif.mPlansFonctionnement
        TablePages(i) = Mod�leImpression
        TablePlans(i) = unPlanFeux
        i += 1
        If unPlanFeux.AvecTrafic Then
          TablePages(i) = ImpressionEnum.Diagnostic
          TablePlans(i) = unPlanFeux
          i += 1
        End If
      Next
      ReDim Preserve TablePages(nbPages - 1)

    Else
      For i = IndicePr�c�dent To nbPages - 1
        TablePages(i) = Mod�leImpression
        If Mod�leImpression = ImpressionEnum.PlanDeFeux Then
          TablePlans(i) = PlanFeuxBaseActif.mPlansFonctionnement(i - IndicePr�c�dent)
        End If
      Next
    End If

    PremPage(Mod�leImpression) = nbPages - nbPagesBloc

  End Sub

  '*****************************************************************************************
  ' Ev�nement PrintPage : appel� par Print ou par ShowDialog
  ' Imprime une nouvelle page
  ' Ev�nement rappel� tant que HasMorePages = True
  '*****************************************************************************************
  Private Sub pDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles mPrintDocument.PrintPage
    'Par d�faut PageUnit=Diplay (1/75�me de pouce !!! pouquoi pas le point ? 1/72�)

    EventPage = e
    e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.High

    Try
      With EventPage
        .Graphics.PageUnit = GraphicsUnit.Millimeter
        If numPage = 0 Then
          With Marges()
            .Left = 25
            .Top = 25
            .Bottom = 75
            .Right = 75
          End With
          RectangleUtile = .MarginBounds
          LargeurUtile = DimUtile(RectangleUtile, RectangleEnum.Largeur) - 5
          HauteurUtile = DimUtile(RectangleUtile, RectangleEnum.Hauteur) - 5
        End If
      End With

      DimensionnerGraphique()

      'pCourante d�signe le point d'insertion courant pour dessiner en coordonn�es utilisateur (rectangle utile de la page)
      pCourante = New Point(0, 0)

      Mod�le = TablePages(numPage)
      cndFlagImpression = Mod�le

      Select Case Mod�le
        Case ImpressionEnum.ListeProjets
          DessinerListeProjets()
        Case ImpressionEnum.PlanCarrefour
          DessinerPlanCarrefour()
        Case ImpressionEnum.Trafic
          TraficActif = PlanFeuxBaseActif.TraficsImprimables(numPage - PremPage(Mod�le))
          DessinerTrafic()
        Case ImpressionEnum.DiagrammePhases
          DessinerDiagrammePhases()
        Case ImpressionEnum.Matrice
          DessinerMatrice()
        Case ImpressionEnum.ListePlans
          DessinerListePlans()
        Case ImpressionEnum.PlanDeFeux
          PlanFeuxFctActif = TablePlans(numPage)
          DessinerPlanFeux()
        Case ImpressionEnum.Diagnostic
          PlanFeuxFctActif = TablePlans(numPage)
          DessinerDiagnostic()
      End Select

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    Finally
      numPage += 1
      EventPage.HasMorePages = (numPage < nbPages)

    End Try

  End Sub

  '******************************************************
  ' Dessiner le plan du carrefour
  '******************************************************
  Private Sub DessinerPlanCarrefour()
    EnteteCarrefour("PLAN DU CARREFOUR")
    BandeauGauche()
    PiedDePage()

    Pr�parerLeDessin(maVariante.mCarrefour, FDPADessiner:=True)
    EffectuerLeDessin()
  End Sub

  '******************************************************
  ' Dessiner une p�riode de trafic
  '******************************************************
  Private Sub DessinerTrafic()
    EnteteCarrefour("TRAFICS en uvp/h")
    BandeauGauche()
    PiedDePage()

    Pr�parerLeDessin(TraficActif)
    EffectuerLeDessin()

  End Sub

  '******************************************************
  ' Dessiner le diagramme des phases
  '******************************************************
  Private Sub DessinerDiagrammePhases()
    Dim HauteurBandeau As Short
    Dim pCentre(2) As Point
    Dim pFleche As Point
    Dim pCentreBoite As Point
    Dim unCercle As Cercle
    Dim f As Fleche()

    EnteteCarrefour("DIAGRAMME DES PHASES")
    PiedDePage()

    Cot�Vignette = 75  ' 7,5cm pour dessiner un miniplan du carrefour

    Dim unePhase As Phase
    Dim numPhase As Short
    Dim nbPhases As Short = PlanFeuxBaseActif.mPhases.Count

    'Dessiner chaque phase dans un cercle
    For Each unePhase In PlanFeuxBaseActif.mPhases

      'Cr�er les objets graphiques des mouvements de la phase
      DimensionnerGraphiquePourPhase(unePhase)
      Pr�parerLeDessin(unePhase)

      numPhase = PlanFeuxBaseActif.mPhases.IndexOf(unePhase)

      'Dessiner le cercle
      Dim Rayon As Short
      With ZoneGraphiqueOeil
        pCentre(numPhase).X = .X + .Width / 2
        pCentre(numPhase).Y = .Y + .Height / 2
        Rayon = 1.05 * .Width / 2
      End With
      unCercle = New Cercle(pCentre(numPhase), Rayon, New Pen(Color.Green, 0.5))
      unCercle.Dessiner(EventPage.Graphics)

      'Ecrire le nom de la phase
      Dim unPolyArc As New PolyArc
      With pCentreBoite
        .X = pCentre(numPhase).X
        .Y = pCentre(numPhase).Y + Rayon + 5
      End With
      unPolyArc.Cr�erBoiteTexte(pCentreBoite, Rayon, "Phase " & PlanFeuxBaseActif.mPhases.IndexOf(unePhase) + 1, New SolidBrush(Color.Black))
      unPolyArc.Dessiner(EventPage.Graphics)

      'Dessin complet de la phase
      EffectuerLeDessin()
    Next

    Dim uneFl�che As New Fleche(Longueur:=14, HauteurFl�che:=2, unePlume:=New Pen(Color.Green, 2))
    'Positionner la fl�che � droite et la retourner pour qu'elle s'oriente vers la droite
    Dim uneFleche1 As Fleche

    ReDim f(nbPhases - 1)
    If nbPhases = 2 Then
      With uneFl�che
        pFleche = Milieu(pCentre(0), pCentre(1))
        pFleche.Y -= 10
        pFleche.X += .Longueur / 2 - .HauteurFl�che
        uneFleche1 = .RotTrans(pFleche, Math.PI)
        uneFleche1.Dessiner(EventPage.Graphics)
        pFleche.Y += 20
        pFleche.X -= .Longueur - .HauteurFl�che * 2
        uneFleche1 = .RotTrans(pFleche, 0)
        uneFleche1.Dessiner(EventPage.Graphics)
      End With

    Else
      With uneFl�che
        pFleche = Milieu(pCentre(0), pCentre(1))
        pFleche.X += .Longueur / 2 - .HauteurFl�che
        uneFleche1 = .RotTrans(pFleche, Math.PI)
        uneFleche1.Dessiner(EventPage.Graphics)

        Dim AngleRot As Single
        pFleche = Milieu(pCentre(1), pCentre(2))
        pFleche.Y += .Longueur / 2 - .HauteurFl�che
        AngleRot = -Math.PI / 4
        AngleRot = AngleForm�(pCentre(2), pCentre(1))
        uneFleche1 = .RotTrans(pFleche, AngleRot)
        uneFleche1.Dessiner(EventPage.Graphics)

        pFleche = Milieu(pCentre(2), pCentre(0))
        pFleche.Y -= .Longueur / 2 - .HauteurFl�che
        AngleRot = Math.PI / 4
        AngleRot = AngleForm�(pCentre(0), pCentre(2))
        uneFleche1 = .RotTrans(pFleche, AngleRot)
        uneFleche1.Dessiner(EventPage.Graphics)

      End With
    End If

    DimensionnerGraphiquePourL�gende()
    HauteurBandeau = DessinerL�gendePhases()

    DimensionnerGraphiquePourMatrice(HauteurEnteteDePage + (ZoneGraphique.Height + HauteurBandeau - Cot�Vignette) / 2)
    PourVignette = True
    Pr�parerLeDessin(PlanFeuxBaseActif)
    PourVignette = False
    EffectuerLeDessin()

  End Sub

  Private Sub DessinerMatrice()

    EnteteCarrefour("MATRICE DES ROUGES DE DEGAGEMENT(en secondes)")
    PiedDePage()

    DessinerLaMatrice()

    'Cot�Vignette = 75  ' 7,5cm pour dessiner un miniplan du carrefour
    'DimensionnerGraphiquePourMatrice((ZoneGraphique.Height - Cot�Vignette) / 2)

    '' Dessiner un mini dessin du carrefour dans la marge
    ''PourVignette = True
    'Pr�parerLeDessin(maVariante)
    ''PourVignette = False
    'EffectuerLeDessin()

    Cot�Vignette = 59  ' 6cm pour dessiner un miniplan du carrefour
    Dim desPhases As PhaseCollection = PlanFeuxBaseActif.mPhases
    Dim nbPhases As Short = desPhases.Count
    Dim Intervalle As Short = (ZoneGraphique.Height - nbPhases * Cot�Vignette) / (nbPhases + 1)
    Dim unePhase As Phase

    Try

      For Each unePhase In desPhases
        'DimensionnerGraphiquePourMatrice((ZoneGraphique.Height - Cot�Vignette) / 2)
        If nbPhases = 2 Then
          DimensionnerGraphiquePourMatrice(23 + desPhases.IndexOf(unePhase) * (13 + Cot�Vignette))
        Else
          DimensionnerGraphiquePourMatrice(5 + desPhases.IndexOf(unePhase) * Cot�Vignette)
        End If

        ' Dessiner un mini dessin du carrefour dans la marge
        PhaseActiveImpressionRougeD�gagement = unePhase
        Pr�parerLeDessin(maVariante)
        EffectuerLeDessin()
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DessinerMatrice")
    End Try

  End Sub


  Private Sub DessinerPlanFeux()
    EnteteCarrefour("PLAN DE FEUX")
    PiedDePage()

    DessinerLePlan()

  End Sub

  Private Function Dur�eCycleMax() As Short
    'Suite � demande du CERTU : on conserve une �chelle fixe au lieu de la calculer en fonction de la r�alit�
    Return PlanFeux.maxiDur�eCycleAbsolue

    'Dim unPlanFeux As PlanFeuxFonctionnement
    'Dim Dur�e As Short = 0

    'For Each unPlanFeux In PlansFonctionnement
    '  Dur�e = Math.Max(Dur�e, unPlanFeux.Dur�eCycle)
    'Next
    'Return Dur�e

  End Function

  Private Sub DessinerLePlan()
    Dim ZoneDiagramme As Rectangle
    Dim lHorizontale As LigneFeux
    Dim mLignesFeux As LigneFeuxCollection = PlanFeuxFctActif.mLignesFeux
    Dim p1(1), p2(1) As Point
    Dim unePlume As New Pen(Color.Black, Width:=0.2)
    Dim nbLignesEntete As Short = 2
    Dim HauteurTableau As Short = Interligne * (mLignesFeux.Count + nbLignesEntete)

    With ZoneGraphique
      'Faire partir le tableau � gauche de la page
      .X = 0
      'Retirer les marges ajout�es dans DimensionnerGraphique pour retrouver les coordonn�es absolues
      .Y -= MargePage(MargeEnum.Haut)
      'Faire descendre le tout d'1cm 
      .Y += 10
    End With

    pCourante.X = 25
    pCourante.Y = ZoneGraphique.Y
    EventPage.Graphics.DrawLine(unePlume, PointImprim�(pCourante), PointImprim�(Point.op_Addition(pCourante, New Size(0, HauteurTableau))))
    pCourante.X += 5
    EventPage.Graphics.DrawLine(unePlume, PointImprim�(pCourante), PointImprim�(Point.op_Addition(pCourante, New Size(0, HauteurTableau))))

    'Entete du tableau 
    pCourante = ZoneGraphique.Location

    Dessiner("Ligne de feux")
#If DIAGFEUX Then
    pCourante.X += 25
#Else
    pCourante.X += 28
    Dessiner("N�", Alignement:=StringAlignment.Center)
    pCourante.X += 2
#End If

    With ZoneDiagramme
      .Location = PointImprim�(Point.op_Addition(pCourante, New Size(5, 0)))
      .Width = LargeurUtile - .X - 10 ' La largeur du diagramme est amput�e des 2 colonnes de lignes de feux,d'un espace et et d'1 cm pour �crire la longueur du cycle
      .Height = HauteurUtile
    End With

    'Dessiner la  1�re ligne horizontale en haut du tableau
    p1(0).X = pCourante.X
    p1(0).Y = ZoneGraphique.Y

    pCourante = Point.op_Addition(ZoneGraphique.Location, New Size(0, nbLignesEntete * Interligne))

    ' Une ligne du tableau par ligne de feux
    For Each lHorizontale In mLignesFeux
      'M�moriser les points de la ligne horizontale au-dessus de la ligne de feux
      p1(0).Y = pCourante.Y
      p1(1) = pCourante

      With lHorizontale
        'If .EstPi�ton Then
        '  Dessiner(.ID & " Pi�tons" & Libell�FeuPi�tons(.mBranche, mLignesFeux))
        '  pCourante.X += 30
        'Else
        pCourante.X += 6
        Dessiner(.ID)
#If DIAGFEUX Then
        'On retire les 5mm pr�vus pour le num�ro
        pCourante.X += 19
#Else
          pCourante.X += 24
        'End If
        Dessiner(lHorizontale.strNum�ro, Alignement:=StringAlignment.Far)
#End If
        pCourante.X += 5
      End With  ' lHorizontale

      'Dessiner une ligne horizontale au-dessus de la ligne de feux
      EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1(0)), PointImprim�(p1(1)))

      With pCourante
        pCourante.X = ZoneGraphique.X
        pCourante.Y += Interligne
      End With

    Next

    With PlanFeuxFctActif
      .Marges = ZoneDiagramme.Location
      '.MargeY = ZoneDiagramme.Location.Y   ' + rajouter l'ent�te pour les noms des phases
      .IntervalY = Interligne
      .IntervalX = ZoneDiagramme.Width / Dur�eCycleMax()
      ' Dessiner le diagramme
      .DessinerDiagramme(EventPage.Graphics, Nothing)

      ' Dessiner le cadre de l'ensemble : Tableau des libell�s + le diagramme
#If DIAGFEUX Then
      'On retire les 5mm pr�vus pour le num�ro
      DessinerCadre(New Point(0, ZoneGraphique.Location.Y), 30 + .IntervalX * (.Dur�eCycle + 2), HauteurTableau, unePlume)
#Else
      DessinerCadre(New Point(0, ZoneGraphique.Location.Y), 35 + .IntervalX * (.Dur�eCycle + 2), HauteurTableau, unePlume)
#End If
    End With

  End Sub

  Private Sub DessinerLaMatrice()
    Dim lHorizontale, lVerticale As LigneFeux
    Dim lgCol1, lgCol2, lgcol3, lgcol4, lgcol5, lgTableau, HauteurTableau As Single
    Dim pD�part As Point
    Dim desLignesFeux As LigneFeuxCollection = maVariante.mLignesFeux
    Dim mLignesFeux As LigneFeuxCollection = PlanFeuxBaseActif.mLignesFeux
    Dim nbLignesFeux As Short = mLignesFeux.Count
    Dim unePlume As New Pen(Color.Black, Width:=0.2)
    Dim p1(nbLignesFeux + 4), p2(nbLignesFeux + 4) As Point

    lgCol1 = 25
#If DIAGFEUX Then
    lgCol2 = 0
#Else
    lgCol2 = 5
#End If
    lgcol3 = 7
    lgcol4 = 11
    lgcol5 = 14

    lgTableau = lgCol1 + lgCol2 + nbLignesFeux * lgcol3 + lgcol4 + lgcol5

    pD�part = ZoneGraphique.Location
    pD�part.X = 60 + (ZoneGraphique.Width - lgTableau) / 2
    pD�part.Y += (ZoneGraphique.Height - (desLignesFeux.Count + 5) * Interligne) / 2 - MargePage(MargeEnum.Haut)
    'DessinerCadre(pD�part, ZoneGraphique.Size, unePlume:=unePlume, margesainclure:=False)

    'Entete du tableau de la matrice
    pCourante = pD�part
    pCourante.Y += Interligne / 2
    Dessiner("Ligne de feux")
    pCourante.X += lgCol1
#If Not DiagFeux Then
    Dessiner(" N�")
#End If
    pCourante.X += lgCol2 - lgcol3 / 2

    For Each lHorizontale In desLignesFeux
      pCourante.X += lgcol3
      Dessiner(lHorizontale.ID, Alignement:=StringAlignment.Center)
    Next

    pCourante.Y -= Interligne / 2
    pCourante.X += lgcol3 / 2
    Dessiner(" Type de feu", longueurmaxi:=lgcol4)
    pCourante.X += lgcol4
    pCourante.Y = pD�part.Y
    Dessiner(" Dur�e de jaune", longueurmaxi:=lgcol5)

    'Dessiner la  1�re ligne horizontale en haut du tableau
    p1(0) = pD�part
    p1(0).X += lgTableau
    EventPage.Graphics.DrawLine(unePlume, PointImprim�(pD�part), PointImprim�(p1(0)))

    ' 2 lignes pour l'entete du tableau
    pCourante = pD�part
    pCourante.Y += 2 * Interligne

    ' Une ligne du tableau par ligne de feux
    For Each lHorizontale In desLignesFeux
      'M�moriser les points de la ligne horizontale au-dessus de la ligne de feux
      p1(0).Y = pCourante.Y
      p1(1) = pCourante

      With lHorizontale
        'If .EstPi�ton Then
        '  Dessiner(.ID & " Pi�tons" & Libell�FeuPi�tons(.mBranche, desLignesFeux))
        '  pCourante.X += lgCol1 + lgCol2
        'Else
        pCourante.X += 6
        Dessiner(.ID)
        pCourante.X += lgCol1 + lgCol2 - 6
        'End If
#If Not DiagFeux Then
        Dessiner(lHorizontale.strNum�ro, Alignement:=StringAlignment.Far)
#End If
        pCourante.X += 5

        For Each lVerticale In desLignesFeux
          If lHorizontale.EstTrivialementCompatible(lVerticale) Then
            ' Dessiner une case hachur�e � la place du temps de d�gagement
            Dim uneHachure As New Drawing2D.HatchBrush(Drawing2D.HatchStyle.BackwardDiagonal, Color.LightGray, Color.White)
            p2(0).X = pCourante.X - 5
            p2(0).Y = pCourante.Y
            EventPage.Graphics.FillRectangle(uneHachure, New Rectangle(PointImprim�(p2(0)), New Size(lgcol3, Interligne)))


          ElseIf mLignesFeux.EstIncompatible(lHorizontale, lVerticale) Then
            Dessiner(mLignesFeux.TempsD�gagement(lHorizontale, lVerticale).ToString, Alignement:=StringAlignment.Far)
          End If
          pCourante.X += lgcol3
        Next
        pCourante.X += lgcol4 / 2 - lgcol3
        Dessiner(.mSignalFeu(0).mSignal.strCode, Alignement:=StringAlignment.Center)
        pCourante.X += (lgcol4 + lgcol5) / 2
        Dessiner(.Dur�eJaune.ToString, Alignement:=StringAlignment.Far)
      End With  ' Horizontale

      'Dessiner une ligne horizontale au-dessus de la ligne de feux
      EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1(0)), PointImprim�(p1(1)))

      With pCourante
        pCourante.X = pD�part.X
        pCourante.Y += Interligne
      End With
    Next

    HauteurTableau = pCourante.Y - pD�part.Y

    ' Lignes verticales
    p2(0) = p1(0)
    p1(0) = pD�part

    p1(1).X = p1(0).X + lgCol1
    p1(2).X = p1(1).X + lgCol1
    Dim i As Short

    p1(0).X = pD�part.X
    p1(1).X = p1(0).X + lgCol1
    p1(2).X = p1(1).X + lgCol2
    For i = 3 To 3 + nbLignesFeux - 1
      p1(i).X = p1(i - 1).X + lgcol3
    Next
    p1(nbLignesFeux + 3).X = p1(nbLignesFeux + 2).X + lgcol4
    p1(nbLignesFeux + 4).X = p1(nbLignesFeux + 3).X + lgcol5
    For i = 0 To p1.Length - 1
      p2(i).X = p1(i).X
      p1(i).Y = pD�part.Y
      p2(i).Y = pCourante.Y
    Next

    With EventPage.Graphics
      For i = 0 To p1.GetUpperBound(0)
        .DrawLine(unePlume, PointImprim�(p1(i)), PointImprim�(p2(i)))
      Next
    End With

    'Tracer un cadre �pais autour du tableau + un autre pour faire ressortir la matrice proprement dite
    Dim unePlumeEpaisse As New Pen(Color.Black, Width:=0.5)
    DessinerCadre(pD�part, New Size(lgTableau, HauteurTableau), unePlume:=unePlumeEpaisse)

    pCourante = Point.op_Addition(pD�part, New Size(lgCol1 + lgCol2, 0))
    DessinerCadre(pCourante, New Size(nbLignesFeux * lgcol3, HauteurTableau), unePlumeEpaisse)

    'pCourante.Y = pD�part.Y + HauteurTableau + 2 * Interligne
    'Dessiner("Temps des rouges de d�gagement en secondes", uneFonte:=New Font(maFonte, FontStyle.Underline))

  End Sub

  Private Sub DessinerListeProjets()
    Dim unPlanFeux As PlanFeuxBase
    Dim lgCol1, lgCol2, lgcol3, lgcol4, lgTableau As Single
    Dim pD�part As Point
    Dim Libell�Colonnes() As String = {" Nom du Sc�nario ", " Avec trafic ", " Verrouillage ", " Plans de feux "}
    Dim desPlansFeux As PlanFeuxCollection = maVariante.mPlansFeuxBase

    With EventPage.Graphics
      lgCol1 = .MeasureString(Libell�Colonnes(0), maFonte).Width
      For Each unPlanFeux In desPlansFeux
        lgCol1 = Math.Max(lgCol1, .MeasureString(" " & unPlanFeux.Nom & " ", maFonte).Width)
      Next
      lgCol2 = .MeasureString(Libell�Colonnes(1), maFonte).Width
      lgcol3 = .MeasureString(Libell�Colonnes(2), maFonte).Width
      For Each unPlanFeux In desPlansFeux
        lgcol3 = Math.Max(lgcol3, .MeasureString(" " & [Global].Libell�CourtVerrouillage(unPlanFeux.Verrou) & " ", maFonte).Width)
      Next
      lgcol4 = .MeasureString(Libell�Colonnes(3), maFonte).Width
    End With

    lgTableau = lgCol1 + lgCol2 + lgcol3 + lgcol4

    pD�part.X = (LargeurUtile - lgTableau) / 2
    pD�part.Y = ZoneGraphique.Y

    EnteteCarrefour("LISTE DES SCENARIOS PROJET")

    Dim maFonteItalic As New Font(maFonte, FontStyle.Italic)

    'Ecrire l'entete du tableau
    pCourante = pD�part
    Dessiner(Libell�Colonnes(0), uneFonte:=maFonteItalic)
    pCourante.X += lgCol1
    Dessiner(Libell�Colonnes(1), uneFonte:=maFonteItalic)
    pCourante.X += lgCol2
    Dessiner(Libell�Colonnes(2), uneFonte:=maFonteItalic)
    pCourante.X += lgcol3
    Dessiner(Libell�Colonnes(3), SautDeLignes:=1, uneFonte:=maFonteItalic)

    'Ecrire une ligne par sc�nario
    For Each unPlanFeux In desPlansFeux
      pCourante.X = pD�part.X
      With unPlanFeux
        Dessiner(.Nom)
        pCourante.X += lgCol1 + lgCol2 / 2
        If .AvecTrafic Then
          Dessiner("OUI", Alignement:=StringAlignment.Center)
        Else
          Dessiner("NON", Alignement:=StringAlignment.Center)
        End If
        pCourante.X += lgCol2 / 2
        Dessiner([Global].Libell�CourtVerrouillage(.Verrou))
        If .mPlansFonctionnement.Count > 0 Then
          'Affichage du nombre de plans de feux de fonctionnement
          pCourante.X += lgcol3 + lgcol4 / 2
          Dessiner(.mPlansFonctionnement.Count, Alignement:=StringAlignment.Center)
        End If
        pCourante.Y += Interligne
      End With
    Next

    Dim unePlume As New Pen(Color.Black, Width:=0.2)
    Dim unePlumeEpaisse As New Pen(Color.Black, Width:=0.5)
    Dim HauteurTableau As Single = Interligne * (desPlansFeux.Count + 1)

    'Dessiner l'encadrement
    With EventPage.Graphics
      'Cadre de l'en t�te
      Dim unRectangle As New Rectangle(PointImprim�(ZoneGraphique.Location), New Size(200, Interligne))
      unRectangle = RectangleImprim�(pD�part, New Size(lgTableau, HauteurTableau))
      .DrawRectangle(unePlumeEpaisse, unRectangle)
      'Ligne horizontale s�paratrice ent�te tableau
      .DrawLine(unePlumeEpaisse, PointImprim�(New Point(pD�part.X, pD�part.Y + Interligne)), PointImprim�(New Point(pD�part.X + lgTableau, pD�part.Y + Interligne)))

      ' Lignes verticales
      Dim p1(2), p2(2) As Point
      p1(0) = PointImprim�(New Point(pD�part.X + lgCol1, pD�part.Y))
      p2(0).X = p1(0).X
      p2(0).Y = PointImprim�(pCourante).Y

      p1(1) = Point.op_Addition(p1(0), New Size(lgCol2, 0))
      p2(1) = Point.op_Addition(p2(0), New Size(lgCol2, 0))

      p1(2) = Point.op_Addition(p1(1), New Size(lgcol3, 0))
      p2(2) = Point.op_Addition(p2(1), New Size(lgcol3, 0))

      Dim i As Short
      For i = 0 To p1.GetUpperBound(0)
        .DrawLine(unePlume, p1(i), p2(i))
      Next

    End With

  End Sub

  Private Sub DessinerListePlans()
    Dim unPlanFeux As PlanFeuxFonctionnement
    Dim lgCol1, lgCol2, lgcol3, lgTableau As Single
    Dim pD�part As Point
    Dim Libell�Colonnes() As String = {" Nom du Plan de Feux ", " P�riode d'utilisation ", " Longueur du cycle ", " Page  "}

    With EventPage.Graphics
      lgCol1 = .MeasureString(Libell�Colonnes(0), maFonte).Width
      lgCol2 = .MeasureString(Libell�Colonnes(1), maFonte).Width

      For Each unPlanFeux In PlansFonctionnement
        lgCol1 = Math.Max(lgCol1, .MeasureString(" " & unPlanFeux.Nom & " ", maFonte).Width)
        If unPlanFeux.AvecTrafic Then
          lgCol2 = Math.Max(lgCol2, .MeasureString(" " & unPlanFeux.Trafic.Libell� & " ", maFonte).Width)
        End If
      Next
      lgTableau = lgCol1 + lgCol2
      lgcol3 = .MeasureString(Libell�Colonnes(2), maFonte).Width
      lgTableau += lgcol3 + .MeasureString(Libell�Colonnes(3), maFonte).Width
    End With

    pD�part.X = (LargeurUtile - lgTableau) / 2
    pD�part.Y = ZoneGraphique.Y

    EnteteCarrefour("LISTE DES PLANS DE FEUX")

    Dim maFonteItalic As New Font(maFonte, FontStyle.Italic)

    'Ecrire l'entete du tableau
    pCourante = pD�part
    Dessiner(Libell�Colonnes(0), uneFonte:=maFonteItalic)
    pCourante.X += lgCol1
    Dessiner(Libell�Colonnes(1), uneFonte:=maFonteItalic)
    pCourante.X += lgCol2
    Dessiner(Libell�Colonnes(2), uneFonte:=maFonteItalic)
    pCourante.X += lgcol3
    Dessiner(Libell�Colonnes(3), SautDeLignes:=1, uneFonte:=maFonteItalic)

    'Ecrire une ligne par plans de feux
    For Each unPlanFeux In PlansFonctionnement
      pCourante.X = pD�part.X
      With unPlanFeux
        Dessiner(.Nom)
        pCourante.X += lgCol1
        If .AvecTrafic Then
          Dessiner(.Trafic.Libell�)
        Else
          Dessiner("<Aucune>")
        End If
        pCourante.X += lgCol2 + lgcol3 / 2
        Dessiner(.Dur�eCycle.ToString, Alignement:=StringAlignment.Far)
        If Me.chkPlanDeFeux.Checked Then
          'Affichage du num�ro de page des plans de feux si ceux-xi sont imprim�s
          pCourante.X += lgcol3 / 2 + 6
          Dessiner(numPagePlan(unPlanFeux), Alignement:=StringAlignment.Far)
        End If
        pCourante.Y += Interligne
      End With
    Next

    Dim unePlume As New Pen(Color.Black, Width:=0.2)
    Dim unePlumeEpaisse As New Pen(Color.Black, Width:=0.5)
    Dim HauteurTableau As Single = Interligne * (PlansFonctionnement.Count + 1)

    'Dessiner l'encadrement
    With EventPage.Graphics
      'Cadre de l'en t�te
      Dim unRectangle As New Rectangle(PointImprim�(ZoneGraphique.Location), New Size(200, Interligne))
      unRectangle = RectangleImprim�(pD�part, New Size(lgTableau, HauteurTableau))
      .DrawRectangle(unePlumeEpaisse, unRectangle)
      'Ligne horizontale s�paratrice ent�te tableau
      .DrawLine(unePlumeEpaisse, PointImprim�(New Point(pD�part.X, pD�part.Y + Interligne)), PointImprim�(New Point(pD�part.X + lgTableau, pD�part.Y + Interligne)))

      ' Lignes verticales
      Dim p1(2), p2(2) As Point
      p1(0) = PointImprim�(New Point(pD�part.X + lgCol1, pD�part.Y))
      p2(0).X = p1(0).X
      p2(0).Y = PointImprim�(pCourante).Y

      p1(1) = Point.op_Addition(p1(0), New Size(lgCol2, 0))
      p2(1) = Point.op_Addition(p2(0), New Size(lgCol2, 0))

      p1(2) = Point.op_Addition(p1(1), New Size(lgcol3, 0))
      p2(2) = Point.op_Addition(p2(1), New Size(lgcol3, 0))

      Dim i As Short
      For i = 0 To p1.GetUpperBound(0)
        .DrawLine(unePlume, p1(i), p2(i))
      Next

    End With

  End Sub

  Private Function numPagePlan(ByVal unPlanFeux As PlanFeuxFonctionnement) As Short
    Dim unEnum�rateur As IDictionaryEnumerator = TablePlans.GetEnumerator

    Do While unEnum�rateur.MoveNext
      If unEnum�rateur.Entry.Value Is unPlanFeux Then
        Return unEnum�rateur.Key + 1
      End If
    Loop

  End Function

  'Private Function Libell�FeuPi�tons(ByVal uneBranche As Branche, ByVal mLignesFeux As LigneFeuxCollection) As String
  '  Dim uneLigneFeux As LigneFeux
  '  Dim Chaine As New String("")
  '  Dim Num�ro As String

  '  For Each uneLigneFeux In mLignesFeux
  '    With uneLigneFeux
  '      If .EstV�hicule Then
  '        If .mBranche Is uneBranche Then
  '          Num�ro = uneLigneFeux.strNum�ro
  '          If Chaine.Length = 0 Then
  '            Chaine = " " & Num�ro
  '          Else
  '            Chaine &= "," & Num�ro
  '          End If
  '        End If
  '      End If
  '    End With
  '  Next

  '  If Chaine.Length > 0 Then
  '    Return " de" & Chaine
  '  End If


  'End Function

  Private Sub DessinerDiagnostic()
    EnteteCarrefour("DIAGNOSTIC")
    PiedDePage()

    DessinerParam�tres()
    DessinerLeDiagnostic()
    DessinerInfosAttente()
  End Sub

  Private Sub DessinerParam�tres()
    Dim feuille As New frmDiagnostic
    Dim pD�part As Point = ZoneGraphique.Location
    Dim posXRel(3), posX(3) As Short
    Dim i As Short
    Dim uneTaille As SizeF

    pD�part.Y -= Interligne
    pCourante = pD�part

    'Tabulations du cadre Param�trage
    posXRel(0) = 0
    posXRel(1) = 50
    posXRel(2) = 90
    posXRel(3) = 130
    For i = 0 To posX.Length - 1
      posX(i) = posXRel(i)
      posX(i) += pD�part.X
    Next

    ' cadre du param�trage
    DessinerCadre(pD�part, New Size(posXRel(3) + 28, 2 * Interligne))

    With feuille
      '1�re ligne
      Dessiner(.lblVitesse.Text)
      pCourante.X = posX(1)
      uneTaille = Dessiner(.lblPi�tons.Text)
      pCourante.X += uneTaille.Width
      Dessiner(maVariante.VitessePi�tons & " m/s")
      pCourante.X = posX(2)
      uneTaille = Dessiner(.lblV�hicules.Text)
      pCourante.X += uneTaille.Width
      Dessiner(maVariante.VitesseV�hicules & " m/s")
      pCourante.X = posX(3)
      uneTaille = Dessiner(.lblV�los.Text)
      pCourante.X += uneTaille.Width
      Dessiner(maVariante.VitesseV�los & " m/s", SautDeLignes:=1)

      '2�me ligne
      pCourante.X = pD�part.X  ' revenir en d�but de ligne
      pD�part.Y = pCourante.Y   ' red�finir pD�part selon le saut de lignes subi par pCourante
      uneTaille = Dessiner(.lblD�bitSaturation.Text)
      pCourante.X += uneTaille.Width
      Dessiner(maVariante.D�bitSaturation & .lblUvpd.Text)
      pCourante.X = posX(2)
      uneTaille = Dessiner(.lblVertUtile.Text)
      pCourante.X += uneTaille.Width
      Dessiner(maVariante.strVertUtile)
    End With

  End Sub

  Private Sub DessinerLeDiagnostic()
    Dim feuille As New frmDiagnostic
    Dim pD�part As Point = ZoneGraphique.Location
    Dim posXRel(5), posX(5) As Short
    Dim i As Short

    pD�part.Y += 2 * Interligne
    pCourante = pD�part

    'Tabulations du cadre diagnostic
    posXRel(0) = 0
    posXRel(1) = 43
    posXRel(2) = 54
    posXRel(3) = 95
    posXRel(4) = 102
    posXRel(5) = 158
    For i = 0 To posX.Length - 1
      posX(i) = posXRel(i)
      posX(i) += pD�part.X
    Next

    ' cadre du diagnostic
    DessinerCadre(pD�part, New Size(posXRel(5), 4 * Interligne))

    'Titre du cadre Diagnostic
    Dessiner("Diagnostic", sautdelignes:=1, uneFonte:=New Font(maFonte, newStyle:=FontStyle.Underline))
    pD�part = pCourante

    'Ecrire les infos de diagnostic
    With feuille
      '1�re ligne

      Dessiner(.lblCycle.Text)
      pCourante.X = posX(1)
      Dessiner(PlanFeuxFctActif.Dur�eCycle, Alignement:=StringAlignment.Far)
      pCourante.X = posX(2)
      Dessiner(.lblR�serveCapacit�.Text, LongueurMaxi:=posXRel(3) - posXRel(2) - 8)
      pCourante.X = posX(3)
      Dessiner(CType(Math.Round(PlanFeuxFctActif.R�serveCapacit�), String), Alignement:=StringAlignment.Far)
      pCourante.X = posX(4)
      Dessiner(.lblTempsPerduCycle.Text, LongueurMaxi:=posXRel(5) - posXRel(4) - 8)
      pCourante = New Point(posX(5), pD�part.Y)
      Dessiner(CType(Math.Round(PlanFeuxFctActif.TempsPerdu), String), SautDeLignes:=1, Alignement:=StringAlignment.Far)

      '2�me ligne
      pCourante.X = pD�part.X  ' revenir en d�but de ligne
      pD�part.Y = pCourante.Y   ' red�finir pD�part selon le saut de lignes subi par pCourante
      Dessiner(.lblDemande.Text, LongueurMaxi:=posXRel(1) - posXRel(0) - 8)
      pCourante = New Point(posX(1), pD�part.Y)
      Dessiner(PlanFeuxFctActif.Demande, Alignement:=StringAlignment.Far)
      pCourante.X = posX(2)
      Dessiner(.lblUVPDR�serveCapacit�.Text)
      pCourante.X = posX(3)
      Dessiner(PlanFeuxFctActif.strR�serveCapacit�PourCent, Alignement:=StringAlignment.Far)
      pCourante.X = posX(4)
      Dessiner(.lblCapacit�Plan.Text, LongueurMaxi:=posXRel(5) - posXRel(4) - 8)
      pCourante.X = posX(5)
      Dessiner(CType(Math.Round(PlanFeuxFctActif.Capacit�Th�orique), String), SautDeLignes:=1, Alignement:=StringAlignment.Far)

      'Derni�re ligne
      pCourante.X = pD�part.X
      Dessiner(.lblUvpdHeure.Text)
      pCourante.X = posX(4)
      Dessiner(.lblUVPDCapacit�Plan.Text)
    End With
  End Sub

  Private Sub DessinerInfosAttente()
    Dim Feuille As New frmDiagnostic
    Dim nbLignesEnt�te As Short

    Dim lgTableau, HauteurTableau As Single
    Dim pD�part As Point
    Dim uneLigneFeux As LigneFeux
    Dim mLignesFeux As LigneFeuxCollection = maVariante.mLignesFeux
    Dim nbLignesFeux As Short = mLignesFeux.nbLignesV�hicules
    Dim unePlume As New Pen(Color.Black, Width:=0.2)
    Dim p1, p2 As Point

    Dim lgCol(5), posX(6), posXc(5) As Short

    pD�part = ZoneGraphique.Location
    pD�part.Y += 7 * Interligne

    lgCol(0) = 15
    lgCol(1) = 20
    lgCol(2) = 20
    lgCol(3) = 25
    lgCol(4) = 25
    lgCol(5) = 20

    Dim i As Short
    For i = 0 To lgCol.Length - 1
      lgTableau += lgCol(i)
      If i = 0 Then
        posX(i) = pD�part.X
        posXc(i) = pD�part.X + lgCol(0) / 2
      Else
        posX(i) = posX(i - 1) + lgCol(i - 1)
        posXc(i) = posXc(i - 1) + (lgCol(i - 1) + lgCol(i)) / 2
      End If
    Next
    'Position de la derni�re ligne verticale fermant le tableau
    posX(6) = pD�part.X + lgTableau

    'Le  2 fonctionne ici parce qu'on est s�r que les en-t�te ne font pas + de 2 lignes
    'Sinon , il faudrait refaire un calcul dynamique lors de l'�criture des entetes de colonne
    nbLignesEnt�te = 2
    HauteurTableau = (nbLignesFeux + nbLignesEnt�te) * Interligne

    pCourante = pD�part

    'Libell�s des entetes de colonne v�hicules
    With Feuille
      Dessiner(.lblDiagV�hicules.Text, LongueurMaxi:=lgCol(0))
      pCourante.X += lgCol(0)
      Dessiner(.lblDemandeUVP.Text, longueurmaxi:=lgCol(1))
      pCourante.X += lgCol(1)
      pCourante.Y = pD�part.Y
      Dessiner(.lblVertV�hicules.Text, longueurmaxi:=lgCol(2))
      pCourante.X += lgCol(2)
      pCourante.Y = pD�part.Y
      Dessiner(.lblTempsAttenteFile.Text, LongueurMaxi:=lgCol(3))
      pCourante.X += lgCol(3)
      pCourante.Y = pD�part.Y
      Dessiner(.lblNbV�hicules.Text, LongueurMaxi:=lgCol(4))
      pCourante.X += lgCol(4)
      pCourante.Y = pD�part.Y
      Dessiner(.lblLgFileAttente.Text, SautDeLignes:=1, LongueurMaxi:=lgCol(5))
    End With

    'Remplir les lignes du tableau v�hicules
    pCourante.X = pD�part.X
    pCourante.Y = pD�part.Y + nbLignesEnt�te * Interligne

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstV�hicule Then
        pCourante.X = posXc(0)
        Dessiner(uneLigneFeux.ID, Alignement:=StringAlignment.Center)
        With PlanFeuxFctActif
          pCourante.X = posXc(1)
          Dessiner(.DemandeUVP(uneLigneFeux), Alignement:=StringAlignment.Center)
          pCourante.X = posXc(2)
          Dessiner(.VertUtile(uneLigneFeux), Alignement:=StringAlignment.Center)
          pCourante.X = posXc(3)
          Dessiner(Format(.RetardMoyen(uneLigneFeux), "###"), Alignement:=StringAlignment.Center)
          pCourante.X = posXc(4)
          Dessiner(.NbV�hiculesEnAttente(uneLigneFeux), Alignement:=StringAlignment.Center)
          pCourante.X = posXc(5)
          Dessiner(.LgFileAttente(uneLigneFeux), Alignement:=StringAlignment.Center)
        End With
        'Ligne suivante
        pCourante.Y += Interligne
      End If
    Next

    'Dessiner les lignes horizontales du tableau v�hicules
    p1 = pD�part
    p2 = p1
    p2.X += lgTableau

    'Dessiner une ligne horizontale au-dessus de la ligne de feux
    EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))

    p1.Y += nbLignesEnt�te * Interligne
    p2.Y += nbLignesEnt�te * Interligne

    'Dessiner une ligne horizontale par ligne de feux
    For i = 0 To nbLignesFeux
      EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))
      p1.Y += Interligne
      p2.Y += Interligne
    Next

    'Dessiner les lignes verticales du tableau v�hicules
    p1.Y = pD�part.Y
    p2.Y = pD�part.Y + HauteurTableau

    For i = 0 To lgCol.Length
      p1.X = posX(i)
      p2.X = p1.X
      EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))
    Next

    'Temps moyen d'attente
    pCourante.X = pD�part.X + lgTableau + 5
    pCourante.Y = p2.Y - Interligne
    Dessiner(Feuille.lblTempsMoyenV�hicules.Text & PlanFeuxFctActif.TMAV�hicules, Encadr�:=True)

    'Nouveau d�part pour le tableau pi�tons
    pD�part.Y = p2.Y + Interligne
    '=============== Lignes Pi�tons =================
    nbLignesFeux = mLignesFeux.nbLignesPi�tons

    If nbLignesFeux > 0 And PlanFeuxFctActif.Trafic.QPi�tonTotal > 0 Then
      'Pas de tableau pi�tons s'il n'y a pas de lignes de feux pi�tons ou si le trafic pi�tons n'est pas renseign�
      ReDim Preserve lgCol(3)
      lgTableau = 0
      For i = 0 To lgCol.Length - 1
        lgTableau += lgCol(i)
      Next
      'Position de la derni�re ligne verticale fermant le tableau
      posX(4) = pD�part.X + lgTableau

      'Le  2 fonctionne ici parce qu'on est s�r que les en-t�te ne font pas + de 2 lignes
      'Sinon , il faudrait refaire un calcul dynamique lors de l'�criture des entetes de colonne
      nbLignesEnt�te = 2
      HauteurTableau = (nbLignesFeux + nbLignesEnt�te) * Interligne

      pCourante = pD�part

      'Libell�s des entetes de colonne pi�tons
      With Feuille
        Dessiner(.lblDiagPi�tons.Text, LongueurMaxi:=lgCol(0))
        pCourante.X += lgCol(0)
        Dessiner(.lblFluxPi�tons.Text, longueurmaxi:=lgCol(1))
        pCourante.X += lgCol(1)
        pCourante.Y = pD�part.Y
        Dessiner(.lblVertPi�tons.Text, longueurmaxi:=lgCol(2))
        pCourante.X += lgCol(2)
        pCourante.Y = pD�part.Y
        Dessiner(.lblTempsAttentePi�tons.Text, LongueurMaxi:=lgCol(3))
      End With

      'Remplir les lignes du tableau pi�tons
      pCourante.X = pD�part.X
      pCourante.Y = pD�part.Y + nbLignesEnt�te * Interligne

      For Each uneLigneFeux In mLignesFeux
        If uneLigneFeux.EstPi�ton Then
          pCourante.X = posXc(0)
          Dessiner(uneLigneFeux.ID, Alignement:=StringAlignment.Center)
          With PlanFeuxFctActif
            pCourante.X = posXc(1)
            Dessiner(PlanFeuxFctActif.Trafic.QPi�ton(uneLigneFeux.mBranche), Alignement:=StringAlignment.Center)
            pCourante.X = posXc(2)
            Dessiner(.VertUtile(uneLigneFeux), Alignement:=StringAlignment.Center)
            pCourante.X = posXc(3)
            Dessiner(Format(.RetardMoyen(uneLigneFeux), "###"), Alignement:=StringAlignment.Center)
          End With
          'Ligne suivante
          pCourante.Y += Interligne
        End If
      Next

      'Dessiner les lignes horizontales du tableau pi�tons
      p1 = pD�part
      p2 = p1
      p2.X += lgTableau

      'Dessiner une ligne horizontale au-dessus de la ligne de feux
      EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))

      p1.Y += nbLignesEnt�te * Interligne
      p2.Y += nbLignesEnt�te * Interligne

      'Dessiner une ligne horizontale par ligne de feux
      For i = 0 To nbLignesFeux
        EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))
        p1.Y += Interligne
        p2.Y += Interligne
      Next

      'Dessiner les lignes verticales du tableau pi�tons
      p1.Y = pD�part.Y
      p2.Y = pD�part.Y + HauteurTableau

      For i = 0 To lgCol.Length
        p1.X = posX(i)
        p2.X = p1.X
        EventPage.Graphics.DrawLine(unePlume, PointImprim�(p1), PointImprim�(p2))
      Next

      'Temps moyen d'attente
      pCourante.X = pD�part.X + lgTableau + 5
      pCourante.Y = p2.Y - Interligne
      Dessiner(Feuille.lblTempsMoyenPi�tons.Text & PlanFeuxFctActif.TMAPi�tons, Encadr�:=True)

    End If  ' Trafic pi�ton 


  End Sub

  Private Sub EnteteDiagnostic()
    Dim feuille As New frmDiagnostic
    Dim LgCadre As Single
    Dim TexteVitesse, TextePi�tons, TexteV�hicules, TexteV�los, TexteD�bit, TexteVertUtile As String
    Dim XPi�tons As Single

    With feuille
      TexteVitesse = .lblVitesse.Text
      TextePi�tons = .lblPi�tons.Text & maVariante.VitessePi�tons & .lblMSPi�tons.Text
      TexteV�hicules = .lblV�hicules.Text & maVariante.VitesseV�hicules & .lblMSV�hicules.Text
      TexteV�los = .lblV�los.Text & maVariante.VitesseV�los & .lblMSV�los.Text
      TexteD�bit = .lblD�bitSaturation.Text & maVariante.D�bitSaturation & .lblUvpd.Text
      TexteVertUtile = .lblVertUtile.Text & maVariante.strVertUtile
    End With

    Dim Format As New StringFormat
    Format.Alignment = StringAlignment.Near
    With EventPage.Graphics
      XPi�tons = .MeasureString(TexteD�bit, maFonte, 1000, Format).Width + 10
      LgCadre = .MeasureString(TextePi�tons & TexteV�hicules & TexteV�los, maFonte, 1000, Format).Width + 10
      LgCadre = Math.Max(LgCadre, .MeasureString(TexteVertUtile, maFonte, 1000, Format).Width)
    End With
    LgCadre += XPi�tons + 1 'rajout d'1 mm pour des pb possibles d'arrondis

    Dim pD�part As New Point((LargeurUtile - LgCadre) / 2, 25)
    pCourante = pD�part

    Dessiner(TexteVitesse)
    pCourante.X += XPi�tons
    pCourante.X += Dessiner(TextePi�tons).Width + 5
    pCourante.X += Dessiner(TexteV�hicules).Width + 5
    Dessiner(TexteV�los, sautdelignes:=1)
    pCourante.X = pD�part.X
    Dessiner(TexteD�bit)
    pCourante.X = pD�part.X + XPi�tons
    Dessiner(TexteVertUtile)
    DessinerCadre(pD�part, New Size(LgCadre, 2 * Interligne))
  End Sub

  Private Function DessinerCadre(ByVal pLocation As Point, ByVal uneTaille As Size, Optional ByVal unePlume As Pen = Nothing, Optional ByVal MargesAInclure As Boolean = True) As Rectangle
    Dim unRectangle As Rectangle
    If MargesAInclure Then
      unRectangle = New Rectangle(PointImprim�(pLocation), uneTaille)
    Else
      unRectangle = New Rectangle(pLocation, uneTaille)
    End If
    If IsNothing(unePlume) Then unePlume = maPlumeCadre
    DessinerRectangle(unRectangle, unePlume)

    Return unRectangle
  End Function

  Private Function DessinerCadre(ByVal pLocation As Point, ByVal uneTaille As Size, ByVal Centr� As Boolean, Optional ByVal unePlume As Pen = Nothing, Optional ByVal MargesAInclure As Boolean = True) As Rectangle
    If Centr� Then
      pLocation = Point.op_Subtraction(pLocation, New Size(uneTaille.Width / 2, uneTaille.Height / 2))
    End If
    Return DessinerCadre(pLocation, uneTaille, unePlume)
  End Function
  Private Function DessinerCadre(ByVal pLocation As Point, ByVal Largeur As Short, ByVal Hauteur As Short, Optional ByVal unePlume As Pen = Nothing) As Rectangle
    Return DessinerCadre(pLocation, New Size(Largeur, Hauteur), unePlume)
  End Function

  Private Sub DessinerRectangle(ByVal unRectangle As Rectangle, ByVal unePlume As Pen)
    EventPage.Graphics.DrawRectangle(unePlume, unRectangle)
  End Sub

  Private Sub EnteteCarrefour(ByVal Titre As String)
    Const LargeurDateService As Short = 100
    Const LargeurEntete As Short = 180
    Dim HauteurEntete As Short
    Dim Texte As String
    Dim CentrePage As Short = LargeurUtile / 2
    Dim h As SizeF

    If Not TousLesProjets() AndAlso PlanFeuxBaseActif.Projet Then
      EcrireProjet()
    End If

    With monCarrefour
      If Mod�le = ImpressionEnum.PlanCarrefour Then
        pCourante.X = 1
        'Modif AV(21/06/07) : les coordonn�es du service sont encadr�es : le cadre doit �tre align� avec celui du bandeau de gauche
        pCourante.X = 0
        Dessiner(.Coordonn�esService, LongueurMaxi:=40, Encadr�:=True)
        pCourante.X = CentrePage
        pCourante.Y = 0
        Dessiner("CARREFOUR " & .Nom, SautDeLignes:=1.3, uneFonte:=New Font("Arial", 12), Alignement:=StringAlignment.Center)
        pCourante.X = 60 ' 5cm pour les coordonn�es + espacement d'1cm
        Dessiner("Rues @", maVariante.mBranches.Libell�Rues, SautDeLignes:=1, LongueurMaxi:=LargeurEntete)
        Dessiner("N� @", .Num�ro, 1, LargeurDateService)
        Dessiner("1�re mise en service @", .DatePremierService, 1, LargeurDateService)
        HauteurEntete = pCourante.Y
        pCourante.Y = 0
        DessinerCadre(pCourante, LargeurEntete, HauteurEntete)
        DessinerCadre(ZoneGraphique.Location, ZoneGraphique.Size, MargesAInclure:=False)
        pCourante.Y = 18       '20

        If Me.chkLogo.Checked Then
          DimensionnerGraphiquePourLogo()
        End If

      Else
        If Not TousLesProjets() Then
          Dessiner("Page " & numPage + 1 & "/" & nbPages)
        End If

        pCourante.X = CentrePage
        Dessiner("CARREFOUR " & .Nom, SautDeLignes:=1, uneFonte:=New Font("Arial", 12), Alignement:=StringAlignment.Center, Encadr�:=True)

        pCourante.Y = 15

        Select Case Mod�le
          Case ImpressionEnum.Trafic
            Texte = TraficActif.Libell�
            Dessiner(Texte, LongueurMaxi:=100, Alignement:=StringAlignment.Center, Encadr�:=True)
            DessinerCadre(ZoneGraphique.Location, ZoneGraphique.Size, MargesAInclure:=False)

          Case ImpressionEnum.DiagrammePhases, ImpressionEnum.Matrice, ImpressionEnum.Diagnostic
            Texte = PlanFeuxBaseActif.Nom
            Dessiner(Texte, LongueurMaxi:=100, Alignement:=StringAlignment.Center, Encadr�:=True)

          Case ImpressionEnum.PlanDeFeux
            With PlanFeuxFctActif
              Dim P�riodeTrafic As String
              If .AvecTrafic AndAlso String.Compare(.Nom, .Trafic.Nom, IgnoreCase:=True) <> 0 Then
                'Rajouter le nom de la p�riode si ce n'est pas le m�me que le nom du sc�nario
                P�riodeTrafic = " - " & .Trafic.Libell�
              End If
              Dessiner(.Nom & P�riodeTrafic, LongueurMaxi:=100, Alignement:=StringAlignment.Center, Encadr�:=True)
              Dim pRef As Point = New Point(40, 20)
              pCourante = pRef
              h = Dessiner("Cycle de" & vbCrLf & .Dur�eCycle & " s", uneFonte:=New Font(maFonte, FontStyle.Bold), Alignement:=StringAlignment.Center, Encadr�:=True)
              pCourante.Y = pRef.Y + h.Height / 2
              pCourante.X = CentrePage
            End With

        End Select

        pCourante.Y = 8   ' 10
      End If
    End With

    pCourante.X = LargeurUtile / 2
    Dessiner(Titre, uneFonte:=New Font("Arial", 12), Alignement:=StringAlignment.Center)


  End Sub

  Private Sub EcrireProjet()
    Dim uneChaine As String = "PROJET " & PlanFeuxBaseActif.Nom
    Dim uneFonte As New Font(maFonte.FontFamily, 16, FontStyle.Italic Or FontStyle.Underline)
    Dim p As Point = PointImprim�(LargeurUtile, 16)
    Dim uneBrosse As New SolidBrush(Color.Red)
    Dim Format As New StringFormat

    Format.Alignment = StringAlignment.Far

    With EventPage
      .Graphics.DrawString(uneChaine, uneFonte, uneBrosse, CvPointF(p), Format)
    End With
  End Sub

  Private Sub BandeauGauche()
    Dim LongueurMaxi As Short = LargeurBandeau
    Dim HauteurBandeau As Short

    pCourante = New Point(0, HauteurEnteteDePage)

    If Mod�le = ImpressionEnum.PlanCarrefour Then
      With monCarrefour
        Dessiner("Travaux suivis par @", vbCrLf & .SuperviseurTravaux, 2, LongueurMaxi)

        Dessiner("Mat�riel", SautDelignes:=1, uneFonte:=New Font(maFonte, FontStyle.Underline))

        Dessiner("Type controleur @", .TypeControleur, 1, LongueurMaxi)
        Dessiner("Fabricant @", .FabricantControleur, 1, LongueurMaxi)
        Dessiner(CStr(maVariante.mLignesFeux.Count & " Lignes de feux"), SautDeLignes:=2)

        If EtudePr�sente() Then
          Dessiner("Etude", SautDelignes:=1, uneFonte:=New Font(maFonte, FontStyle.Underline))
          Dessiner("Rendue le", .DateEtude, 1, LongueurMaxi)
          Dessiner("R�alis�e par", .R�alisateurEtude, 1, LongueurMaxi)
          Dessiner("Objectif de l'�tude @", .ObjectifEtude, 1, LongueurMaxi)
          pCourante.Y += Interligne
        End If

        Dessiner("Mise en service @", .DateMiseEnService, 1, LongueurMaxi)
        Dessiner("Modifications @", .DateModification, 1, LongueurMaxi)
        Dessiner("Modifications de la plage horaire @", .DateModifPlageHoraire, 1, LongueurMaxi)
      End With

      If pCourante.Y <> HauteurEnteteDePage Then
        HauteurBandeau = ZoneGraphique.Height
      End If

    Else
      'Trafic
      Dessiner("Commentaire sur la p�riode @", TraficActif.Commentaires, 1, LongueurMaxi)
      HauteurBandeau = pCourante.Y - HauteurEnteteDePage
    End If

    If HauteurBandeau > 0 Then
      DessinerCadre(New Point(0, HauteurEnteteDePage), New Size(LargeurBandeau, HauteurBandeau))
    End If

  End Sub

  Private Function DessinerL�gendePhases() As Short
    Dim p1, p2, p3, p4 As Point
    Dim uneLigne As Ligne
    Dim uneFl�che, mFl�che As Fleche
    Dim unePlume As Pen = New Pen(Color.Black, 0.3)
    Dim unePlumePointill�e As Pen = unePlume.Clone
    Dim unePlumeFl�che As Pen = unePlume.Clone
    'L'�paisseur de la plume �tant de 0.3, ceci fait un espacement de 0.6
    Dim EspacementTiret() As Single = {2, 2}
    Dim D�calageLigneTexte As New Size(3, -3)

    Dim HauteurBandeau As Short
    Dim LongueurMaxi As Short = LargeurBandeau

    unePlumePointill�e.DashStyle = Drawing2D.DashStyle.Dash
    unePlumePointill�e.DashPattern = EspacementTiret

    pCourante = New Point(0, HauteurEnteteDePage)
    Dessiner("L�gende", SautDelignes:=1, uneFonte:=New Font(maFonte, FontStyle.Underline))
    p1.X = 3
    p1.Y = pCourante.Y + Interligne
    p2.X = p1.X + 13
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("mouvement des v�hicules ayant le vert", SautDelignes:=CType(1, Short), LongueurMaxi:=LongueurMaxi, uneFonte:=maFonte)

    uneLigne = New Ligne(PointImprim�(p1), PointImprim�(p2), unePlume)
    uneLigne.Dessiner(EventPage.Graphics)
    uneFl�che = New Fleche(0, HauteurFl�che:=2, SegmentCentral:=False, unePlume:=unePlumeFl�che)
    mFl�che = uneFl�che.RotTrans(PointImprim�(p2), Math.PI)
    mFl�che.Dessiner(EventPage.Graphics)

    p1.Y = pCourante.Y + Interligne
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("mouvement des v�hicules ayant le jaune clignotant", 1, LongueurMaxi, maFonte)

    uneLigne = New Ligne(PointImprim�(p1), PointImprim�(p2), unePlumePointill�e)
    uneLigne.Dessiner(EventPage.Graphics)
    mFl�che = uneFl�che.RotTrans(PointImprim�(p2), Math.PI)
    mFl�che.Dessiner(EventPage.Graphics)

    p1.Y = pCourante.Y + Interligne
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("mouvement des pi�tons ayant le vert", 1, LongueurMaxi, maFonte)

    uneLigne = New Ligne(PointImprim�(p1), PointImprim�(p2), unePlume)
    uneLigne.Dessiner(EventPage.Graphics)
    mFl�che = uneFl�che.Translation(PointImprim�(p1))
    mFl�che.Dessiner(EventPage.Graphics)
    mFl�che = uneFl�che.RotTrans(PointImprim�(p2), Math.PI)
    mFl�che.Dessiner(EventPage.Graphics)

    p1.Y = pCourante.Y + Interligne
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("mouvement des v�hicules ayant le rouge", 1, LongueurMaxi, maFonte)

    uneLigne = New Ligne(PointImprim�(p1), PointImprim�(p2), unePlume)
    uneLigne.Dessiner(EventPage.Graphics)
    p3 = Point.op_Addition(p2, New Size(0, 1))
    p4 = Point.op_Addition(p2, New Size(0, -1))
    uneLigne = New Ligne(PointImprim�(p3), PointImprim�(p4), unePlume)
    uneLigne.Dessiner(EventPage.Graphics)

    p1.Y = pCourante.Y + Interligne
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("Enchainement possible des phases", 1, LongueurMaxi, maFonte)
    uneFl�che = New Fleche(Longueur:=15, HauteurFl�che:=2, unePlume:=New Pen(Color.Green, 2))
    uneFl�che = uneFl�che.RotTrans(PointImprim�(p2), Math.PI)
    uneFl�che.Dessiner(EventPage.Graphics)

    Dim unPolyArc As New PolyArc

    p1.Y = pCourante.Y + Interligne
    p2.Y = p1.Y
    pCourante = Point.op_Addition(p2, D�calageLigneTexte)
    Dessiner("num�ro de ligne de feux", 1, LongueurMaxi, maFonte)

    p1 = Point.op_Addition(p1, New Size(1, -1))
    unPolyArc.Cr�erCercleTexte(CvPointF(PointImprim�(p1)), Rayon:=2.2, unePlume:=New Pen(Color.Black), Chaine:="F1", uneBrosse:=New SolidBrush(Color.Red), uneFonte:=New Font("Arial", 7))
    unPolyArc.Dessiner(EventPage.Graphics)

    HauteurBandeau = pCourante.Y - HauteurEnteteDePage
    If HauteurBandeau > 0 Then
      'On rajoute  18mm  : 15mm r�serv� pour le ss symboles et un d�calage du texte de 3mm
      DessinerCadre(New Point(0, HauteurEnteteDePage), New Size(LargeurBandeau + 18, HauteurBandeau + 2))
    End If

    Return HauteurBandeau

  End Function

  Private Function EtudePr�sente() As Boolean
    With monCarrefour
      Return Not EstNulleDate(.DateEtude) Or Not IsNothing(.ObjectifEtude) Or Not IsNothing(.R�alisateurEtude)
    End With
  End Function

  Private Sub PiedDePage()
    Dim Position As Short
    Const LargeurVersion As Short = 30
    Dim OrigineVisa As String
    Dim SommetPiedDePage As Short = HauteurUtile - HauteurPiedDePage
    Dim HauteurFixe As Short = 2 * Interligne

    'Bandeau 1cm au-dessus de la marge du bas
    pCourante = New Point(0, SommetPiedDePage)

    With monCarrefour
      If Mod�le = ImpressionEnum.Trafic Then
        OrigineVisa = .VisaTrafics
        'Rajout� en v10 sans raison particuli�re
        'Supprim� en v13
        'pCourante.X += 5
      Else
        OrigineVisa = .OrigineVisa
      End If

      Position = Math.Ceiling(Dessiner("Visa de @", OrigineVisa, 1, 40, Encadr�:=True, HauteurFixe:=HauteurFixe).Width) + 5
      pCourante = New Point(pCourante.X + Position, SommetPiedDePage)
      Position = Math.Ceiling(Dessiner("Visa ", .Num�roVisa, 1, LargeurVersion, Encadr�:=True, HauteurFixe:=HauteurFixe).Width) + 5

      'Un cadre vide pour la signature
      pCourante = New Point(pCourante.X + Position, SommetPiedDePage)
      DessinerCadre(pCourante, New Size(30, HauteurFixe))

      If Mod�le = ImpressionEnum.PlanDeFeux AndAlso Not IsNothing(maVariante.mCarrefour.Syst�meR�gulation) Then
        pCourante = New Point(150, SommetPiedDePage)
        Dessiner("Syst�me de r�gulation @", maVariante.mCarrefour.Syst�meR�gulation, 0, LongueurMaxi:=100, Encadr�:=True, HauteurFixe:=HauteurFixe)
      End If

      If Mod�le = ImpressionEnum.DiagrammePhases AndAlso Not IsNothing(maVariante.mCarrefour.EnchainementPhases) Then
        pCourante.X = (ZoneGraphique.X + ZoneGraphique.Width) / 2
        'pCourante.Y = SommetPiedDePage - 20
        'Correction Diagfeux2 (16/05/2007) : un chevauchement est possible si long texte depuis que Cot�Vignette est pass� de 60 � 75
        pCourante.Y = SommetPiedDePage - 14
        Dessiner("Pr�cisions sur l'enchainement des phases@", vbCrLf & maVariante.mCarrefour.EnchainementPhases, SautDelignes:=0, longueurmaxi:=ZoneGraphique.Width - 40, Alignement:=StringAlignment.Center, Encadr�:=True)
      End If

      'Calculer la taille du texte pour le cadrer � droite de la page
      Dim uneTaille As SizeF
      If EstNulleDate(.DateVersion) Then
        uneTaille = Dessiner("Version @", .NumVersion, 1, LargeurVersion, HauteurFixe:=HauteurFixe, PourMesurer:=True)
      Else
        uneTaille = Dessiner("Version @" & .NumVersion & vbCrLf & "Date @" & .DateVersion, PourMesurer:=True)
      End If

      pCourante = New Point(LargeurUtile - uneTaille.Width, HauteurUtile - HauteurPiedDePage)
      If EstNulleDate(.DateVersion) Then
        uneTaille = Dessiner("Version @", .NumVersion, 1, LargeurVersion, HauteurFixe:=HauteurFixe, Encadr�:=True)
      Else
        uneTaille = Dessiner("Version @" & .NumVersion & vbCrLf & "Date @" & .DateVersion, Encadr�:=True)
      End If

    End With ' monCarrefour

  End Sub

  Private Function CVPoucesMillim�tres(ByVal uneDimension As Single) As Short
    Return uneDimension * FacteurPoucesMillim�tres
  End Function

  '**********************************************************************************************
  ' Dessiner une chaine, suivie d'une info � transformer en chaine, sans d�passer LongueurMaxi
  ' Si �� d�passe, dessin du d�but de la chaine et appel r�cursif pour dessiner la fin
  ' Une des 2 chaines peut �te vide, auquel cas on n'en dessine qu'une
  ' Si la 2�me est NOthing, on ne dessine rien
  '**********************************************************************************************
  Private Function Dessiner(ByVal Chaine1 As String, ByVal uneInfo As Object, ByVal SautDeLignes As Single, _
        ByVal LongueurMaxi As Short, Optional ByVal uneFonte As Font = Nothing, Optional ByVal Alignement As StringAlignment = StringAlignment.Near, _
          Optional ByVal Encadr� As Boolean = False, Optional ByVal HauteurFixe As Short = 0, Optional ByVal PourMesurer As Boolean = False) As SizeF
    Dim Chaine2 As String
    Dim pD�part As Point = pCourante
    Dim uneTaille As SizeF

    If IsNothing(uneFonte) Then uneFonte = maFonte

    If IsDate(uneInfo) Then
      If Not EstNulleDate(CDate(uneInfo)) Then Chaine2 = CDate(uneInfo).ToShortDateString
    Else
      If Not IsNothing(uneInfo) AndAlso CStr(uneInfo).Length > 0 Then Chaine2 = CStr(uneInfo)
    End If

    If Not IsNothing(Chaine2) Then
      If Chaine1.Length = 0 Then
        uneTaille = Dessiner(Chaine2, SautDeLignes, LongueurMaxi, uneFonte, Alignement:=Alignement, Encadr�:=Encadr�, HauteurFixe:=HauteurFixe, PourMesurer:=PourMesurer)
      ElseIf Chaine2.Length = 0 Then
        uneTaille = Dessiner(Chaine1, SautDeLignes, LongueurMaxi, uneFonte, Alignement:=Alignement, Encadr�:=Encadr�, HauteurFixe:=HauteurFixe, PourMesurer:=PourMesurer)
      Else
        Dim ChaineR�sultante As String
        If Chaine2.Length > 1 AndAlso Chaine2.Substring(0, 2) = vbCrLf Then
          ChaineR�sultante = String.Concat(Chaine1, Chaine2)
        Else
          ChaineR�sultante = String.Concat(Chaine1, " ", Chaine2)
        End If
        uneTaille = Dessiner(ChaineR�sultante, SautDeLignes, LongueurMaxi, uneFonte, Alignement:=Alignement, Encadr�:=Encadr�, HauteurFixe:=HauteurFixe, PourMesurer:=PourMesurer)
      End If

      Return uneTaille
    End If

  End Function

  '**********************************************************************************************
  ' Dessin effectif d'une chaine de caract�res avec un �ventuel saut de lignes
  '**********************************************************************************************
  Private Function Dessiner(ByVal uneChaine As String, Optional ByVal SautDeLignes As Single = 0, _
      Optional ByVal LongueurMaxi As Short = 0, Optional ByVal uneFonte As Font = Nothing, Optional ByVal Alignement As StringAlignment = StringAlignment.Near, _
   Optional ByVal Encadr� As Boolean = False, Optional ByVal HauteurFixe As Short = 0, Optional ByVal PourMesurer As Boolean = False) As SizeF
    Dim Format As New StringFormat
    Dim uneTaille As SizeF
    Dim pD�part As Point = pCourante

    Format.Alignment = Alignement
    If IsNothing(uneFonte) Then uneFonte = maFonte
    uneChaine = d�coup(uneChaine, uneFonte, LongueurMaxi, Format)
    uneTaille = EventPage.Graphics.MeasureString(uneChaine, uneFonte, 1000, Format)
    If HauteurFixe <> 0 Then uneTaille.Height = HauteurFixe

    Dim pos, pos2, pos3 As Short
    'Rechercher la pr�sence de l'occcurence "@"
    'Celle-ci sera remplac�e par ":" et le texte pr�c�dent ce signe sera mis en italique
    pos = uneChaine.IndexOf("@")

    Do While pos <> -1
      Dim fontItalic As New Font(uneFonte, FontStyle.Italic)
      'Remplacer "@" par ":"
      Dim SousChaine As String = uneChaine.Substring(0, pos) & ":"
      If Not PourMesurer Then
        'Ecrire le d�but du texte en italique
        EventPage.Graphics.DrawString(SousChaine, fontItalic, maBrosse, CvPointF(PointImprim�(pCourante)), Format)
      End If
      uneChaine = d�coup(uneChaine.Substring(pos + 1), uneFonte, LongueurMaxi, Format)
      pos2 = uneChaine.IndexOf(vbCrLf)
      Select Case pos2
        Case -1
          uneChaine = uneChaine.TrimStart
          If nbLignes(SousChaine) = 0 Then
            pCourante.X += EventPage.Graphics.MeasureString(SousChaine.Substring(0, pos + 1), fontItalic).Width
          Else
            pos3 = SousChaine.LastIndexOf(vbCrLf)
            pCourante.X += EventPage.Graphics.MeasureString(SousChaine.Substring(pos3 + 2), fontItalic).Width
            pCourante.Y += nbLignes(SousChaine) * Interligne
          End If
        Case 0
          'Eliminer le retour-chariot en t�te
          uneChaine = uneChaine.Substring(pos2 + 2)
          pCourante.Y += (nbLignes(SousChaine) + 1) * Interligne
        Case Else
          uneChaine = uneChaine.TrimStart
          pos2 = uneChaine.IndexOf(vbCrLf)
          If nbLignes(SousChaine) = 0 Then
            pCourante.X += EventPage.Graphics.MeasureString(SousChaine, fontItalic).Width - 1
          Else
            pos3 = SousChaine.LastIndexOf(vbCrLf)
            pCourante.X += EventPage.Graphics.MeasureString(SousChaine.Substring(pos3 + 2), fontItalic).Width
            pCourante.Y += nbLignes(SousChaine) * Interligne
            SousChaine = ""
          End If

          If Not PourMesurer Then
            EventPage.Graphics.DrawString(uneChaine.Substring(0, pos2), uneFonte, maBrosse, CvPointF(PointImprim�(pCourante)), Format)
          End If
          uneChaine = uneChaine.Substring(pos2 + 2)
          pCourante.X = pD�part.X
          pCourante.Y += (nbLignes(SousChaine) + 1) * Interligne
      End Select
      pos = uneChaine.IndexOf("@")
    Loop

    With EventPage
      If Not PourMesurer Then
        .Graphics.DrawString(uneChaine, uneFonte, maBrosse, CvPointF(PointImprim�(pCourante)), Format)
      End If
      SautDeLignes += nbLignes(uneChaine)
      pCourante.Y += SautDeLignes * Interligne
      pCourante.X = pD�part.X

      If Encadr� Then
        Dim pLocation As Point
        Select Case Alignement
          Case StringAlignment.Near
            pLocation = Point.op_Subtraction(pD�part, New Size(1, 0))
            pLocation = pD�part
          Case StringAlignment.Center
            pLocation = Point.op_Subtraction(pD�part, New Size(uneTaille.Width / 2, 0))
          Case StringAlignment.Far
            pLocation = Point.op_Subtraction(pD�part, New Size(uneTaille.Width, 0))
        End Select
        'uneTaille.Width -= 1
        DessinerCadre(pLocation, CvTaille(uneTaille))
      End If
      Return uneTaille
    End With

  End Function

  Private Function nbLignes(ByVal uneChaine As String) As Short
    Dim pos As Short

    pos = uneChaine.IndexOf(vbCrLf)
    Do While pos <> -1
      nbLignes += 1
      uneChaine = uneChaine.Substring(pos + 2)
      pos = uneChaine.IndexOf(vbCrLf)
    Loop

  End Function

  Private Function d�coup(ByVal uneChaine As String, ByVal uneFonte As Font, ByVal LongueurMaxi As Short, ByVal Format As StringFormat) As String
    Dim SousChaine, ChaineCompl�te As String
    Dim pos As Short

    If IsNothing(uneChaine) Then
      Return ""

    Else
      pos = uneChaine.IndexOf(vbCrLf)
      Do While pos <> -1
        SousChaine = uneChaine.Substring(0, pos)
        ChaineCompl�te = String.Concat(ChaineCompl�te, d�coupfin(SousChaine, uneFonte, LongueurMaxi, Format), vbCrLf)
        uneChaine = uneChaine.Substring(pos + 2)
        pos = uneChaine.IndexOf(vbCrLf)
      Loop

      Return String.Concat(ChaineCompl�te, d�coupfin(uneChaine, uneFonte, LongueurMaxi, Format))
    End If

  End Function

  Private Function d�coupfin(ByVal uneChaine As String, ByVal uneFonte As Font, ByVal LongueurMaxi As Short, ByVal Format As StringFormat) As String
    Dim pos As Short
    Dim Longueur As Short
    Dim tmp, Chaine1, Chaine2 As String

    Longueur = EventPage.Graphics.MeasureString(uneChaine, uneFonte).Width
    If Longueur > LongueurMaxi And LongueurMaxi <> 0 Then
      Chaine2 = uneChaine
      pos = Chaine2.IndexOf(" ")
      Do Until pos = -1
        tmp = String.Concat(Chaine1, Chaine2.Substring(0, pos))
        If EventPage.Graphics.MeasureString(tmp, uneFonte, 1000, Format).Width > LongueurMaxi Then
          pos = -1
          If IsNothing(Chaine1) Then
            'premier mot trop long : on le prend quand m�me
            Chaine1 = tmp
            Chaine2 = Chaine2.Substring(pos + 1)
          End If
        Else
          Chaine1 = tmp & " "
          Chaine2 = Chaine2.Substring(pos + 1)
          pos = Chaine2.IndexOf(" ")
        End If
      Loop

      If IsNothing(Chaine1) Then
        'D�coupage impossible : on retourne toute la chaine(m�me trop longue)
        Return Chaine2
      Else
        Return Chaine1.TrimEnd(" "c) & vbCrLf & d�coupfin(Chaine2, uneFonte, LongueurMaxi, Format)
      End If

    Else
      Return uneChaine
    End If


  End Function

  Private Sub pDocument_QueryPageSettings(ByVal sender As Object, ByVal e As System.Drawing.Printing.QueryPageSettingsEventArgs) Handles mPrintDocument.QueryPageSettings

  End Sub

  Private Function PointImprim�(ByVal xRelatif As Short, ByVal yRelatif As Short) As Point
    Return PointImprim�(New Point(xRelatif, yRelatif))
  End Function

  Private Function PointImprim�(ByVal pRelatif As Point) As Point

    Return New Point(MargePage(MargeEnum.Gauche) + pRelatif.X, MargePage(MargeEnum.Haut) + pRelatif.Y)

  End Function

  Private Function RectangleImprim�(ByVal pRelatif As Point, ByVal uneTaille As Size) As Rectangle
    Dim unRectangle As New Rectangle(PointImprim�(pRelatif), uneTaille)
    Return unRectangle
  End Function

  '*************************************************************************
  ' Retourne en mm la marge(gauche, haute...) de la page
  'Marges : Marges en 1/100� d pouce
  '*************************************************************************
  Private Function MargePage(ByVal Cot� As MargeEnum) As Short
    Dim Mesure As Short

    With Marges()
      Select Case Cot�
        Case MargeEnum.Haut
          Mesure = .Top
        Case MargeEnum.Gauche
          Mesure = .Left
        Case MargeEnum.Bas
          Mesure = .Bottom
        Case MargeEnum.Droite
          Mesure = .Right
      End Select
    End With

    Return CVPoucesMillim�tres(Mesure)

  End Function

  Private Property RectangleUtile() As Rectangle
    Get
      Return mRectangleUtile
    End Get
    Set(ByVal Value As Rectangle)
      With Value
        'Les valeurs ar d�faut sont de 100 : on les ram�ne � 25 en haut et � gauche, et � 75 en bas et � droite
        .X = 25
        .Y = 25
        .Width += 150
        .Height += 150
      End With
      mRectangleUtile = Value
    End Set
  End Property

  Private Sub DimensionnerGraphique()
    'Laisser 2mm entre le bandeau et la zone utile au dessin
    Dim MargeGauche As Short = LargeurBandeau + 2
    Dim MargeBas As Short = HauteurPiedDePage + 2

    With ZoneGraphique
      .Location = PointImprim�(MargeGauche, HauteurEnteteDePage)
      .Width = LargeurUtile - MargeGauche
      .Height = HauteurUtile - (HauteurEnteteDePage + MargeBas)
    End With
  End Sub

  Private Sub DimensionnerGraphiquePourLogo()
    Dim Cot� As Short = 15

    With ZoneGraphiqueOeil
      .Location = New Point(ZoneGraphique.Right - Cot�, MargePage(MargeEnum.Haut))
      .Width = Cot� ' 6cm pour dessiner un miniplan du carrefour
      .Height = Cot�
    End With

    Dim uneImage As New ImageRaster(CheminLogo, ZoneGraphiqueOeil.Size, ZoneGraphiqueOeil.Location)
    DessinerRectangle(ZoneGraphiqueOeil, maPlumeCadre)
    uneImage.Dessiner(EventPage.Graphics)


  End Sub

  Private Sub DimensionnerGraphiquePourMatrice(ByVal Sommet As Short)

    With ZoneGraphiqueOeil
      .Location = PointImprim�(0, Sommet)
      .Width = Cot�Vignette ' 6cm pour dessiner un miniplan du carrefour
      .Height = Cot�Vignette
    End With

    Dim unePlumeEpaisse As New Pen(Color.Black, Width:=0.5)
    DessinerRectangle(ZoneGraphiqueOeil, unePlumeEpaisse)

  End Sub

  Private Sub DimensionnerGraphiquePourL�gende()
    With ZoneGraphiqueOeil
      .Location = PointImprim�(0, 2 * HauteurEnteteDePage)
      .Width = 60 ' 6cm pour dessiner un miniplan du carrefour
      .Height = 50
    End With
  End Sub

  Private Sub DimensionnerGraphiquePourPhase(ByVal unePhase As Phase)
    Dim numPhase As Short = PlanFeuxBaseActif.mPhases.IndexOf(unePhase)
    Dim nbPhases As Short = PlanFeuxBaseActif.mPhases.Count
    'Un carr� de 6,5 cm permet laisser 2,5cm � gauche du premier cercle et un espacement de 6cm entre les 2 cercles
    Dim Cot�Carr� As Short = 65
    Dim EspacementCarr�s As Short = 60
    Dim Bordures As Short = 25

    With ZoneGraphiqueOeil
      .Width = Cot�Carr�
      .Height = Cot�Carr�

      Select Case numPhase
        Case 0
          ' Espace libre � gauche du cercle
          .X = Bordures
        Case 1
          'Position droite du 1er cercle + Espacement
          .X = Bordures + Cot�Carr� + EspacementCarr�s
        Case 2
          'Milieu des 2 cercles au-dessus
          .X = Bordures + (EspacementCarr�s + Cot�Carr�) / 2
      End Select

      .X += Cot�Vignette    ' 7,5cm pour le dessin r�duit du carrefour dans le Bandeau

      If nbPhases = 2 Then
        'Centrer en hauteur les 2 cercles
        .Y = (ZoneGraphique.Height - Cot�Carr�) / 2
      Else
        'Forc�ment 3, selon la cahier des charges (MAXPHASES)
        If numPhase = 2 Then
          'POsitionner la 3�me phase tout en bas
          .Y = Cot�Carr�
        Else
          'Positionner les 2 1�res phases tout en haut de la zone
          .Y = 0
        End If
      End If

      .Y += ZoneGraphique.Y + 3
    End With

  End Sub

  Private Function Marges() As Printing.Margins
    Return EventPage.PageSettings.Margins
  End Function

  Private Function DimUtile(ByVal unRectangle As Rectangle, ByVal Cot� As RectangleEnum) As Short
    Dim Mesure As Short

    With unRectangle
      Select Case Cot�
        Case RectangleEnum.Hauteur
          Mesure = .Height
        Case RectangleEnum.Largeur
          Mesure = .Width
        Case RectangleEnum.Haut
          Mesure = .Top
        Case RectangleEnum.Gauche
          Mesure = .Left
        Case RectangleEnum.Bas
          Mesure = .Bottom
        Case RectangleEnum.Droite
          Mesure = .Right
      End Select

    End With

    Return CVPoucesMillim�tres(Mesure)

  End Function

  '*****************************************************************************************
  'Pr�parerLeDessin : Calculer l'�chelle adapt�e et Cr�er les objets graphiques � dessiner
  ' unObjetM�tier : - Carrefour (dessin de l'ensemble du  carrefour)
  '                 - Trafic 
  '                 - Phase 
  '                 - PlanFeux (vignette du pour le diagramme de phases)
  '                 - Variante (vignette pour le dessin de la matrice des rouges de d�gagement)
  '********************************************************************************************
  Private Sub Pr�parerLeDessin(ByVal unObjetM�tier As M�tier, Optional ByVal FDPADessiner As Boolean = False)
    Dim unDXF As DXF
    Dim uneImageRaster As ImageRaster
    Dim unFDP As FondDePlan = maVariante.mFondDePlan
    FDPADessiner = FDPADessiner And Not IsNothing(unFDP)

    Try
      If Not IsNothing(unFDP) Then
        If unFDP.EstDXF Then
          unDXF = CType(unFDP, DXF)
        Else
          uneImageRaster = CType(unFDP, ImageRaster)
        End If
      End If

      ' D�finir l'�chelle en fonction de la zone r�serv�e au graphique sur la page
      InitEchelle(unObjetM�tier, unFDP)

      cndParamDessin = mParamDessin
      If FDPADessiner Then
        If unFDP.EstDXF Then
          CType(unFDP, DXF).Insert.Pr�parerDessin(Nothing).Dessiner(EventPage.Graphics)
        Else
          CType(unFDP, ImageRaster).Dessiner(EventPage.Graphics)
        End If
      End If

      ' Cr�er la collection d'objets � dessiner selon l'objet m�tier associ� au mod�le d'impression
      cndZoneGraphique = ZoneGraphique
      maVariante.Cr�erGraphique(colObjetsGraphiques, unObjetM�tier)

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Pr�parerLeDessin")
    End Try
  End Sub

  Private Sub InitEchelle(ByVal unObjetM�tier As M�tier, ByVal unFDP As FondDePlan)
    Dim uneEchelle As Single
    Dim uneOrigine As PointF
    Dim xMax, yMax As Integer
    Dim CoefMarges As Single = 0.95
    Dim Centre As PointF
    Dim uneZone As Rectangle
    Dim NomRuesACompter As Boolean

    Try
      Select Case Mod�le
        Case ImpressionEnum.Matrice, ImpressionEnum.DiagrammePhases
          uneZone = ZoneGraphiqueOeil
          If Mod�le = ImpressionEnum.DiagrammePhases AndAlso maVariante.mCarrefour.CarrefourType = Carrefour.CarrefourTypeEnum.EnT Then
            CoefMarges = 0.8
          End If
        Case Else
          uneZone = ZoneGraphique
      End Select

      If PourVignette Then CoefMarges = 1.0

      If TypeOf unObjetM�tier Is Carrefour Then
        NomRuesACompter = True
      ElseIf TypeOf unObjetM�tier Is Trafic Then
        'NomRuesACompter = True
      End If
      maVariante.D�finirEncombrement(NomRuesACompter)
      Centre = maVariante.Centre
      If IsNothing(maVariante.mFondDePlan) Then
        '     Centre = Milieu(Centre, maVariante.mCarrefour.mCentre)
      End If

      With uneZone
        xMax = .Width
        yMax = .Height
        uneEchelle = xMax / maVariante.Largeur * CoefMarges
        uneEchelle = Math.Min(uneEchelle, yMax / maVariante.Hauteur * CoefMarges)
        uneOrigine.X = Centre.X - (xMax / 2 + .X) / uneEchelle
        uneOrigine.Y = Centre.Y + (yMax / 2 + .Y) / uneEchelle
      End With

      mParamDessin = New ParamDessin(uneEchelle, uneOrigine, uneZone)

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "InitEchelle")
    End Try

  End Sub

  Private Sub EffectuerLeDessin()

    colObjetsGraphiques.Dessiner(EventPage.Graphics)

  End Sub

  Private Sub btnConfigurer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimante.Click

    mdiApplication.mnuConfigImpr.PerformClick()

    'Rajout de l'instruction suivante pour voir si �� r�gle le pb du CERTU(Impressions 
    mPrintDocument.PrinterSettings.PrinterName = NomImprimante

  End Sub

  Private Sub dlgImpressions_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

    If ImpressionAppel�e Then
      'Restituer le contexte sauvegard� au chargement
      cndFlagImpression = ImpressionEnum.Aucun
      cndParamDessin = sParamDessin
      cndContexte = sContexte
      If Not IsNothing(maVariante.mFondDePlan) Then maVariante.mFondDePlan.Visible = sFDPVisible

      mdiApplication.frmCourant.Recr�erGraphique()

      ImpressionAppel�e = False
    End If

    If sCheminLogo <> CheminLogo Then
      maVariante.AEnregistrer = True
      maVariante.CheminLogo = CheminLogo
    End If

    'il faut le mettre � Nothing, sinon � la prochaine ouverture, la variable numPage, pourtant Private, peut se voir affecter des valeurs bizarres qui d�clent une erreur dans l'�v�nement PrintPage (d�bordement de tableau sur TablePages)
    cndPrintDocument = Nothing

  End Sub

  Private Sub chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles chkPlanCarrefour.CheckedChanged, chkListePlans.CheckedChanged, chkTrafics.CheckedChanged, _
  chkMatrice.CheckedChanged, chkPlanDeFeux.CheckedChanged, chkDiagramme.CheckedChanged

    Dim chk As CheckBox = sender

    If Not chk.Checked Then Me.chkEnsemble.Checked = False

    If chk Is Me.chkPlanDeFeux Then
      'Remarque : pour bien faire, in ne faudrait cocher diagnostic 
      ' que si au moins un plan de feux de fonctionnement est bas� sur un sc�nario avec trafic
      Me.chkDiagnostic.Enabled = chk.Checked And maVariante.mTrafics.Count

    ElseIf chk Is Me.chkPlanCarrefour Then
      Me.chkLogo.Enabled = chk.Checked And Not IsNothing(CheminLogo)
    End If

  End Sub

  Private Sub chkLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLogo.CheckedChanged
    Me.btnLogo.Enabled = chkLogo.Checked
  End Sub

  Private Sub chkEnsemble_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnsemble.CheckedChanged

    If chkEnsemble.Checked Then
      With Me.chkPlanCarrefour
        If .Enabled Then .Checked = True
      End With

      With Me.chkListePlans
        If .Enabled Then .Checked = True
      End With

      With Me.chkTrafics
        If .Enabled Then .Checked = True
      End With

      With Me.chkMatrice
        If .Enabled Then .Checked = True
      End With
      With Me.chkPlanDeFeux
        If .Enabled Then .Checked = True
      End With

      With Me.chkDiagramme
        If .Enabled Then .Checked = True
      End With

    End If

  End Sub

  '********************************************
  ' Choisir un logo
  '********************************************
  Private Sub btnLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogo.Click
    Dim NomFichier As String = CheminLogo
    Dim Filtre As String = ImageRaster.Filtre '"Fichiers image (*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG)|*.JPG;*.GIF;*.TIF;*.TIFF;*.PNG"
    Dim DefaultExt As String = "jpg"

    If IsNothing(NomFichier) Then
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt)
    Else
      NomFichier = DialogueFichier(TypeDialogue:=Outils.TypeDialogueEnum.OuvrirFDP, Filtre:=Filtre, DefaultExt:=DefaultExt, InfoFichier:=New IO.FileInfo(CheminLogo))
    End If

    If Not IsNothing(NomFichier) Then
      CheminLogo = NomFichier
      'Obliger � rafraichir l'image du bouton logo
      mBitmapLogo = Nothing
      If Me.chkPlanCarrefour.Checked Then
        Me.chkLogo.Enabled = True
      End If
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

  Private Sub radD�finitif_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radD�finitif.CheckedChanged

    If radD�finitif.Checked Then
      Me.cboSc�narios.Enabled = False
    Else
      Me.cboSc�narios.Enabled = True
    End If

    S�lectionnerSc�nario()
  End Sub

  Private Sub S�lectionnerSc�nario()

    If radD�finitif.Checked Then
      PlanFeuxBaseActif = maVariante.Sc�narioD�finitif
    ElseIf Me.cboSc�narios.Text = "tous" Then
      PlanFeuxBaseActif = Nothing
    Else
      PlanFeuxBaseActif = maVariante.mPlansFeuxBase(Me.cboSc�narios.Text)
    End If

    If TousLesProjets() Then
      ' Tous les sc�narios projet
      Me.chkDiagramme.Enabled = False
      Me.chkMatrice.Enabled = False
      Me.chkListePlans.Enabled = False
      Me.chkPlanDeFeux.Enabled = False
      Me.chkDiagnostic.Enabled = False

      For Each PlanFeuxBaseActif In maVariante.mPlansFeuxBase
        'Activer les cases � cocher d�s qu'au moins un sc�nario le permet(le filtrage sera fait � l'ex�cution)
        If PlanFeuxBaseActif.Projet Then
          ActiverChk(chkTrafics, PourEnsemble:=True)

          ActiverChk(chkDiagramme, PourEnsemble:=True)
          ActiverChk(chkMatrice, PourEnsemble:=True)
          ActiverChk(chkListePlans, PourEnsemble:=True)
          ActiverChk(chkPlanDeFeux, PourEnsemble:=True)
          ActiverChk(chkDiagnostic, PourEnsemble:=True)
        End If
      Next

      PlanFeuxBaseActif = Nothing

    Else
      'Pour les trafics, plusieurs solutions sont possibles
      '1 : Afficher tous les trafics
      '2 : N'afficher que les trafics concern�s par au moins 1 plan de fonctionnement
      '3 : Ne faire cette restriction que si le plan de feux de base est verrouill� et au moins un PFF
      '4 : Ne faire cette restriction que pour le sc�nario d�finitif
      '5 : Combinaison des restrictions 3 et 4

      '19/06/07 : Solution 1
      ActiverChk(chkTrafics)

      ActiverChk(chkDiagramme)
      ActiverChk(chkMatrice)
      ActiverChk(chkListePlans)
      ActiverChk(chkPlanDeFeux)
      ActiverChk(chkDiagnostic)

    End If

  End Sub

  Private Sub ActiverChk(ByVal chk As CheckBox, Optional ByVal PourEnsemble As Boolean = False)

    If PourEnsemble Then
      'Pour l'ensemble, on laisse activ� la case � cocher 
      'd�s que la condition est satisfaite pour un sc�nario
      If Not chk.Enabled Then
        ActiverChk(chk)
      End If

    Else
      chk.Enabled = chkActivable(chk)
    End If

  End Sub

  Private Function chkActivable(ByVal chk As CheckBox) As Boolean

    With PlanFeuxBaseActif
      Select Case chk.Name
        Case "chkTrafics"
          Return .TraficsImprimables.Count > 0
        Case "chkDiagramme", "chkMatrice"
          Return .PhasageRetenu
        Case "chkListePlans", "chkPlanDeFeux"
          Return .mPlansFonctionnement.Count > 0
        Case "chkDiagnostic"
          If chkPlanDeFeux.Checked And chkPlanDeFeux.Enabled Then
            Return .Trafics.Count > 0
          End If
      End Select
    End With

  End Function

  Private Function chkActiv�(ByVal chk As CheckBox) As Boolean
    Return chk.Checked AndAlso chkActivable(chk)
  End Function

  Private Sub cboSc�narios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSc�narios.SelectedIndexChanged
    S�lectionnerSc�nario()
  End Sub

  Private Sub dlgImpressions_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_IMPRIMER
  End Sub
End Class
