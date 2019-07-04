'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgNouveauPlanFeux.vb										  											'
'						Classes																														'
'							dlgNouveauPlanFeux : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgNouveauPlanFeux --------------------------
'Dialogue pour saisie d'un nouveau plan de feux de fonctionnement
'=====================================================================================================
Public Class dlgNouveauPlanFeux
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
  Friend WithEvents cboTrafic As System.Windows.Forms.ComboBox
  Friend WithEvents lblTrafic As System.Windows.Forms.Label
  Friend WithEvents cboPlansDeFeux As System.Windows.Forms.ComboBox
  Friend WithEvents lblPlanFeuxOrigine As System.Windows.Forms.Label
  Friend WithEvents lblNomPLan As System.Windows.Forms.Label
  Friend WithEvents txtNomPlan As System.Windows.Forms.TextBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.cboTrafic = New System.Windows.Forms.ComboBox
    Me.lblTrafic = New System.Windows.Forms.Label
    Me.lblPlanFeuxOrigine = New System.Windows.Forms.Label
    Me.cboPlansDeFeux = New System.Windows.Forms.ComboBox
    Me.lblNomPLan = New System.Windows.Forms.Label
    Me.txtNomPlan = New System.Windows.Forms.TextBox
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(426, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(424, 96)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(426, 16)
    Me.btnOK.Name = "btnOK"
    '
    'cboTrafic
    '
    Me.cboTrafic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboTrafic.Items.AddRange(New Object() {"<Aucune>"})
    Me.cboTrafic.Location = New System.Drawing.Point(168, 96)
    Me.cboTrafic.Name = "cboTrafic"
    Me.cboTrafic.Size = New System.Drawing.Size(224, 21)
    Me.cboTrafic.TabIndex = 1
    '
    'lblTrafic
    '
    Me.lblTrafic.Location = New System.Drawing.Point(24, 96)
    Me.lblTrafic.Name = "lblTrafic"
    Me.lblTrafic.Size = New System.Drawing.Size(120, 16)
    Me.lblTrafic.TabIndex = 12
    Me.lblTrafic.Text = "Période de trafic :"
    Me.lblTrafic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblPlanFeuxOrigine
    '
    Me.lblPlanFeuxOrigine.Location = New System.Drawing.Point(24, 56)
    Me.lblPlanFeuxOrigine.Name = "lblPlanFeuxOrigine"
    Me.lblPlanFeuxOrigine.Size = New System.Drawing.Size(128, 16)
    Me.lblPlanFeuxOrigine.TabIndex = 14
    Me.lblPlanFeuxOrigine.Text = "A partir du plan de feux :"
    Me.lblPlanFeuxOrigine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cboPlansDeFeux
    '
    Me.cboPlansDeFeux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboPlansDeFeux.Items.AddRange(New Object() {"<Plan de feu de base>"})
    Me.cboPlansDeFeux.Location = New System.Drawing.Point(168, 56)
    Me.cboPlansDeFeux.Name = "cboPlansDeFeux"
    Me.cboPlansDeFeux.Size = New System.Drawing.Size(200, 21)
    Me.cboPlansDeFeux.TabIndex = 2
    '
    'lblNomPLan
    '
    Me.lblNomPLan.Location = New System.Drawing.Point(24, 16)
    Me.lblNomPLan.Name = "lblNomPLan"
    Me.lblNomPLan.Size = New System.Drawing.Size(120, 16)
    Me.lblNomPLan.TabIndex = 15
    Me.lblNomPLan.Text = "Nom du plan de feux :"
    '
    'txtNomPlan
    '
    Me.txtNomPlan.Location = New System.Drawing.Point(168, 16)
    Me.txtNomPlan.MaxLength = 25
    Me.txtNomPlan.Name = "txtNomPlan"
    Me.txtNomPlan.Size = New System.Drawing.Size(184, 20)
    Me.txtNomPlan.TabIndex = 0
    Me.txtNomPlan.Text = ""
    '
    'dlgNouveauPlanFeux
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(514, 135)
    Me.Controls.Add(Me.txtNomPlan)
    Me.Controls.Add(Me.lblNomPLan)
    Me.Controls.Add(Me.lblPlanFeuxOrigine)
    Me.Controls.Add(Me.cboPlansDeFeux)
    Me.Controls.Add(Me.lblTrafic)
    Me.Controls.Add(Me.cboTrafic)
    Me.Name = "dlgNouveauPlanFeux"
    Me.Text = "Nouveau plan de feux"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.cboTrafic, 0)
    Me.Controls.SetChildIndex(Me.lblTrafic, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.cboPlansDeFeux, 0)
    Me.Controls.SetChildIndex(Me.lblPlanFeuxOrigine, 0)
    Me.Controls.SetChildIndex(Me.lblNomPLan, 0)
    Me.Controls.SetChildIndex(Me.txtNomPlan, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region

  '******************************************************************************
  ' Fermeture de la feuille
  '******************************************************************************
  Private Sub dlgNouveauPlanFeux_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

    If DialogResult = DialogResult.OK Then
      If Not e.Cancel Then
        e.Cancel = Not DonnéesVérifiées()
      End If
    End If

  End Sub

  '******************************************************************************
  ' Vérifier les données avant la mise à jour
  '******************************************************************************
  Private Function DonnéesVérifiées() As Boolean
    If Me.txtNomPlan.Text.Length = 0 Then
      AfficherMessageErreur(Me, "Donnée obligatoire")
      Me.txtNomPlan.Focus()
    Else
      DonnéesVérifiées = True
    End If

  End Function

  Private Sub dlgNouveauPlanFeux_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub dlgNouveauPlanFeux_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.ONGLET_PLANS_FEUX
  End Sub
End Class
