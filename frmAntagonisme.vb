'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : frmAntagonisme.vb										  											'
'						Classes																														'
'							frmAntagonisme : Feuille                												'
'																																							'
'******************************************************************************
'=====================================================================================================
'--------------------------- Classe frmAntagonisme  --------------------------
'Feuille volante fournissant une aide contextuelle dans la saisie des antagonismes
'=====================================================================================================

Public Class frmAntagonisme : Inherits System.Windows.Forms.Form

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
  Friend WithEvents lblLibell�Conflit As System.Windows.Forms.Label
  Friend WithEvents lblMessageConflit As System.Windows.Forms.Label
  Friend WithEvents lblConflitAdmis As System.Windows.Forms.Label
  Friend WithEvents radOui As System.Windows.Forms.RadioButton
  Friend WithEvents radNon As System.Windows.Forms.RadioButton
  Friend WithEvents pnlConflit As System.Windows.Forms.Panel
  Friend WithEvents lblAlerte As System.Windows.Forms.Label
  Friend WithEvents lblAlertePlus As System.Windows.Forms.Label
  Friend WithEvents lblAlertePi�tons As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblLibell�Conflit = New System.Windows.Forms.Label
    Me.lblMessageConflit = New System.Windows.Forms.Label
    Me.pnlConflit = New System.Windows.Forms.Panel
    Me.lblConflitAdmis = New System.Windows.Forms.Label
    Me.radOui = New System.Windows.Forms.RadioButton
    Me.radNon = New System.Windows.Forms.RadioButton
    Me.lblAlerte = New System.Windows.Forms.Label
    Me.lblAlertePlus = New System.Windows.Forms.Label
    Me.lblAlertePi�tons = New System.Windows.Forms.Label
    Me.pnlConflit.SuspendLayout()
    Me.SuspendLayout()
    '
    'lblLibell�Conflit
    '
    Me.lblLibell�Conflit.Location = New System.Drawing.Point(8, 8)
    Me.lblLibell�Conflit.Name = "lblLibell�Conflit"
    Me.lblLibell�Conflit.Size = New System.Drawing.Size(288, 32)
    Me.lblLibell�Conflit.TabIndex = 2
    '
    'lblMessageConflit
    '
    Me.lblMessageConflit.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblMessageConflit.Location = New System.Drawing.Point(8, 56)
    Me.lblMessageConflit.Name = "lblMessageConflit"
    Me.lblMessageConflit.Size = New System.Drawing.Size(120, 24)
    Me.lblMessageConflit.TabIndex = 1
    Me.lblMessageConflit.Text = "Message du conflit"
    Me.lblMessageConflit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'pnlConflit
    '
    Me.pnlConflit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.pnlConflit.Controls.Add(Me.lblConflitAdmis)
    Me.pnlConflit.Controls.Add(Me.radOui)
    Me.pnlConflit.Controls.Add(Me.radNon)
    Me.pnlConflit.Location = New System.Drawing.Point(8, 120)
    Me.pnlConflit.Name = "pnlConflit"
    Me.pnlConflit.Size = New System.Drawing.Size(272, 40)
    Me.pnlConflit.TabIndex = 3
    '
    'lblConflitAdmis
    '
    Me.lblConflitAdmis.Location = New System.Drawing.Point(24, 16)
    Me.lblConflitAdmis.Name = "lblConflitAdmis"
    Me.lblConflitAdmis.Size = New System.Drawing.Size(72, 16)
    Me.lblConflitAdmis.TabIndex = 8
    Me.lblConflitAdmis.Text = "Conflit admis"
    '
    'radOui
    '
    Me.radOui.Location = New System.Drawing.Point(112, 16)
    Me.radOui.Name = "radOui"
    Me.radOui.Size = New System.Drawing.Size(48, 16)
    Me.radOui.TabIndex = 7
    Me.radOui.Text = "Oui"
    '
    'radNon
    '
    Me.radNon.Location = New System.Drawing.Point(184, 16)
    Me.radNon.Name = "radNon"
    Me.radNon.Size = New System.Drawing.Size(56, 16)
    Me.radNon.TabIndex = 6
    Me.radNon.Text = "Non"
    '
    'lblAlerte
    '
    Me.lblAlerte.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblAlerte.ForeColor = System.Drawing.Color.OrangeRed
    Me.lblAlerte.Location = New System.Drawing.Point(136, 56)
    Me.lblAlerte.Name = "lblAlerte"
    Me.lblAlerte.Size = New System.Drawing.Size(136, 24)
    Me.lblAlerte.TabIndex = 4
    Me.lblAlerte.Text = "Trafic > 500 uvp"
    Me.lblAlerte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lblAlerte.Visible = False
    '
    'lblAlertePlus
    '
    Me.lblAlertePlus.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblAlertePlus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblAlertePlus.ForeColor = System.Drawing.Color.OrangeRed
    Me.lblAlertePlus.Location = New System.Drawing.Point(136, 56)
    Me.lblAlertePlus.Name = "lblAlertePlus"
    Me.lblAlertePlus.Size = New System.Drawing.Size(128, 24)
    Me.lblAlertePlus.TabIndex = 5
    Me.lblAlertePlus.Text = "Trafic > 500 uvp sur sens unique"
    Me.lblAlertePlus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lblAlertePlus.Visible = False
    '
    'lblAlertePi�tons
    '
    Me.lblAlertePi�tons.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.lblAlertePi�tons.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblAlertePi�tons.ForeColor = System.Drawing.Color.OrangeRed
    Me.lblAlertePi�tons.Location = New System.Drawing.Point(136, 80)
    Me.lblAlertePi�tons.Name = "lblAlertePi�tons"
    Me.lblAlertePi�tons.Size = New System.Drawing.Size(160, 40)
    Me.lblAlertePi�tons.TabIndex = 6
    Me.lblAlertePi�tons.Text = "Il est recommand� de d�clarer ce mouvement incompatible avec ceux des pi�tons"
    Me.lblAlertePi�tons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lblAlertePi�tons.Visible = False
    '
    'frmAntagonisme
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(306, 162)
    Me.Controls.Add(Me.pnlConflit)
    Me.Controls.Add(Me.lblMessageConflit)
    Me.Controls.Add(Me.lblLibell�Conflit)
    Me.Controls.Add(Me.lblAlertePi�tons)
    Me.Controls.Add(Me.lblAlertePlus)
    Me.Controls.Add(Me.lblAlerte)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmAntagonisme"
    Me.ShowInTaskbar = False
    Me.Text = "Antagonisme"
    Me.pnlConflit.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public mAntagonisme As Antagonisme

  Private Sub radOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radOui.CheckedChanged, radNon.CheckedChanged
    Try
      If Not IsNothing(mAntagonisme) Then mdiApplication.frmCourant.InterrompreCommande()

    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)
    End Try
  End Sub

  Private Sub frmAntagonisme_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Me.Hide()
    e.Cancel = True
  End Sub
End Class
