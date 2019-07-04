'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : frmDlg.vb										  											'
'						Classes																														'
'							frmDlg : Modèle des dialogues de l'application, tous en héritent'
'																																							'
'******************************************************************************

Imports System.IO

'=====================================================================================================
'----------- Class frmDlg : Modèle des dialogues de l'application ---------------------
'=====================================================================================================
Public Class frmDlg
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
  Protected Friend WithEvents btnOK As System.Windows.Forms.Button
  Protected Friend WithEvents btnAnnuler As System.Windows.Forms.Button
  Protected Friend WithEvents btnAide As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.btnAnnuler = New System.Windows.Forms.Button
    Me.btnOK = New System.Windows.Forms.Button
    Me.btnAide = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnAnnuler.CausesValidation = False
    Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnAnnuler.Location = New System.Drawing.Point(360, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.Size = New System.Drawing.Size(80, 24)
    Me.btnAnnuler.TabIndex = 10
    Me.btnAnnuler.Text = "Annuler"
    '
    'btnOK
    '
    Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.btnOK.Location = New System.Drawing.Point(360, 16)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(80, 24)
    Me.btnOK.TabIndex = 9
    Me.btnOK.Text = "OK"
    '
    'btnAide
    '
    Me.btnAide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnAide.CausesValidation = False
    Me.btnAide.Location = New System.Drawing.Point(360, 96)
    Me.btnAide.Name = "btnAide"
    Me.btnAide.Size = New System.Drawing.Size(80, 24)
    Me.btnAide.TabIndex = 11
    Me.btnAide.Text = "Aide"
    '
    'frmDlg
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(456, 133)
    Me.Controls.Add(Me.btnAide)
    Me.Controls.Add(Me.btnAnnuler)
    Me.Controls.Add(Me.btnOK)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.KeyPreview = True
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmDlg"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Boite de dialogue type"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub frmDlg_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Select Case Me.DialogResult
      Case DialogResult.OK
      Case DialogResult.Cancel
        e.Cancel = VeriModif()

    End Select

  End Sub

  Protected Overridable Function VeriModif() As Boolean

  End Function

  Private Sub frmDlg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.CancelButton = Me.btnAnnuler
    Me.AcceptButton = Me.btnOK
  End Sub

  Private Sub btnAide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAide.Click
    [Global].AppelAide(Me)
  End Sub

  Private Sub frmDlg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    If e.KeyCode = Keys.F1 Then
      e.Handled = True
      btnAide.PerformClick()
    End If
  End Sub
End Class
