'**************************************************************************************
'     Projet DIAGFEUX

'   Réalisation : André VIGNAUD

'   Module de feuille : frmDéfileDXF    -   Fichier frmDéfileDXF.vb
'   Fait défiler le nom du fichier DXF en cours de lecture et affiche le pourcentage d'avancement
'   Interruption possible par l'utilisateur de la lecture d'un fichier DXF

'     Adaptation du module de GIRATION v3 - CERTU/CETE de l'Ouest
'         Septembre 97

'						Source : frmDéfileDXF.vb										  											'
'						Classes																														'
'							frmDéfileDXF : Feuille 

'**************************************************************************************
Option Strict Off
Option Explicit On
'=====================================================================================================
'--------------------------- Classe frmDéfileDXF  --------------------------
'Feuille volante faisant défiler le om du fichier DXF en cours de lecture
'=====================================================================================================
Friend Class frmDéfileDXF
  Inherits System.Windows.Forms.Form
#Region "Code généré par le Concepteur Windows Form "
  Public Sub New()
    MyBase.New()

    'Cet appel est requis par le Concepteur Windows Form.
    InitializeComponent()
  End Sub
  'La méthode substituée Dispose du formulaire pour nettoyer la liste des composants.
  Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    If Disposing Then
      If Not components Is Nothing Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(Disposing)
  End Sub
  'Requis par le Concepteur Windows Form
  Private components As System.ComponentModel.IContainer
  Public ToolTip1 As System.Windows.Forms.ToolTip
  Public WithEvents txtPanneau As System.Windows.Forms.TextBox
  Public WithEvents tmrDéfile As System.Windows.Forms.Timer
  Public WithEvents lblPourCent As System.Windows.Forms.Label
  'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
  'Il peut être modifié à l'aide du Concepteur Windows Form.
  'Ne pas le modifier à l'aide de l'éditeur de code.
  Public WithEvents btnAnnuler As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.txtPanneau = New System.Windows.Forms.TextBox
    Me.tmrDéfile = New System.Windows.Forms.Timer(Me.components)
    Me.btnAnnuler = New System.Windows.Forms.Button
    Me.lblPourCent = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'txtPanneau
    '
    Me.txtPanneau.AcceptsReturn = True
    Me.txtPanneau.AutoSize = False
    Me.txtPanneau.BackColor = System.Drawing.SystemColors.Window
    Me.txtPanneau.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtPanneau.ForeColor = System.Drawing.SystemColors.WindowText
    Me.txtPanneau.Location = New System.Drawing.Point(8, 24)
    Me.txtPanneau.MaxLength = 0
    Me.txtPanneau.Name = "txtPanneau"
    Me.txtPanneau.ReadOnly = True
    Me.txtPanneau.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.txtPanneau.Size = New System.Drawing.Size(209, 19)
    Me.txtPanneau.TabIndex = 2
    Me.txtPanneau.Text = ""
    '
    'tmrDéfile
    '
    Me.tmrDéfile.Interval = 500
    '
    'btnAnnuler
    '
    Me.btnAnnuler.BackColor = System.Drawing.SystemColors.Control
    Me.btnAnnuler.Cursor = System.Windows.Forms.Cursors.Default
    Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnAnnuler.ForeColor = System.Drawing.SystemColors.ControlText
    Me.btnAnnuler.Location = New System.Drawing.Point(86, 68)
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.btnAnnuler.Size = New System.Drawing.Size(105, 25)
    Me.btnAnnuler.TabIndex = 0
    Me.btnAnnuler.Text = "Annuler"
    '
    'lblPourCent
    '
    Me.lblPourCent.BackColor = System.Drawing.SystemColors.Window
    Me.lblPourCent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblPourCent.Cursor = System.Windows.Forms.Cursors.Default
    Me.lblPourCent.ForeColor = System.Drawing.SystemColors.ControlText
    Me.lblPourCent.Location = New System.Drawing.Point(232, 24)
    Me.lblPourCent.Name = "lblPourCent"
    Me.lblPourCent.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblPourCent.Size = New System.Drawing.Size(33, 17)
    Me.lblPourCent.TabIndex = 1
    '
    'frmDéfileDXF
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(276, 99)
    Me.ControlBox = False
    Me.Controls.Add(Me.txtPanneau)
    Me.Controls.Add(Me.btnAnnuler)
    Me.Controls.Add(Me.lblPourCent)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Location = New System.Drawing.Point(118, 393)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmDéfileDXF"
    Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Import de fichier DXF"
    Me.ResumeLayout(False)

  End Sub
#End Region

  Public Annul As Boolean

  Private Sub btnAnnuler_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnAnnuler.Click

    Annul = True

  End Sub


  Private Sub frmDéfileDXF_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

    Me.Cursor = System.Windows.Forms.Cursors.Default

  End Sub

  Private Sub frmDéfileDXF_Closing(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    Dim Cancel As Short = eventArgs.Cancel

    ' On interdit à l'utilisateur de fermer la fenêtre

    eventArgs.Cancel = Cancel
  End Sub

  Private Sub frmDéfileDXF_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed

    Annul = False

  End Sub

  Private Sub tmrDéfile_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrDéfile.Tick
    ' A chaque interruption du Timer :
    '   renvoi en dernière position du premier caractère de panneau,donnant l'impression d'un défilement
    Dim chaine As String = txtPanneau.Text

    txtPanneau.Text = chaine.Substring(1) & chaine.Substring(0, 1)

  End Sub
End Class