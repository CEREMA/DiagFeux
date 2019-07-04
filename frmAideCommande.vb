'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : frmAideCommande.vb										  											'
'						Classes																														'
'							frmAideCommande : Feuille                												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe frmAideCommande --------------------------
'Feuille volante fournissant une aide contextuelle dans les commandes graphiques
'=====================================================================================================

Public Class frmAideCommande
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
  Friend WithEvents lblMessageCommande As System.Windows.Forms.Label
  Friend WithEvents chkArr�terMessage As System.Windows.Forms.CheckBox
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblMessageCommande = New System.Windows.Forms.Label
    Me.chkArr�terMessage = New System.Windows.Forms.CheckBox
    Me.btnCancel = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'lblMessageCommande
    '
    Me.lblMessageCommande.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblMessageCommande.ForeColor = System.Drawing.Color.RoyalBlue
    Me.lblMessageCommande.Location = New System.Drawing.Point(8, 8)
    Me.lblMessageCommande.Name = "lblMessageCommande"
    Me.lblMessageCommande.Size = New System.Drawing.Size(232, 32)
    Me.lblMessageCommande.TabIndex = 0
    Me.lblMessageCommande.Text = "zrzerzer"
    '
    'chkArr�terMessage
    '
    Me.chkArr�terMessage.Location = New System.Drawing.Point(8, 32)
    Me.chkArr�terMessage.Name = "chkArr�terMessage"
    Me.chkArr�terMessage.Size = New System.Drawing.Size(168, 24)
    Me.chkArr�terMessage.TabIndex = 1
    Me.chkArr�terMessage.Text = "Ne plus afficher ce message"
    Me.chkArr�terMessage.Visible = False
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(208, 48)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(72, 24)
    Me.btnCancel.TabIndex = 2
    Me.btnCancel.Text = "Annuler"
    '
    'frmAideCommande
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.BackColor = System.Drawing.Color.Linen
    Me.ClientSize = New System.Drawing.Size(294, 78)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.chkArr�terMessage)
    Me.Controls.Add(Me.lblMessageCommande)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(300, 100)
    Me.MinimumSize = New System.Drawing.Size(300, 100)
    Me.Name = "frmAideCommande"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    Me.Text = "AideCommande"
    Me.ResumeLayout(False)

  End Sub

#End Region
  Private frmCourant As frmCarrefour

  Private Sub frmAideCommande_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Me.Hide()
    e.Cancel = True
  End Sub

  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    frmCourant.InterrompreCommande()
  End Sub

  Private Sub frmAideCommande_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    frmCourant = mdiApplication.frmCourant
  End Sub
End Class
