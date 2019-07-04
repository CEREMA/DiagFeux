<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmKey
#Region "Code généré par le Concepteur Windows Form "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'Cet appel est requis par le Concepteur Windows Form.
		InitializeComponent()
	End Sub
	'Form remplace la méthode Dispose pour nettoyer la liste des composants.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Requise par le Concepteur Windows Form
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents TxtSerial As System.Windows.Forms.TextBox
	Public WithEvents TxtLicence As System.Windows.Forms.TextBox
  Public WithEvents LblTitre As System.Windows.Forms.Label
	Public WithEvents LblSerial As System.Windows.Forms.Label
	Public WithEvents LblLicence As System.Windows.Forms.Label
  Public WithEvents imgLogo As System.Windows.Forms.PictureBox
	'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
	'Elle peut être modifiée à l'aide du Concepteur Windows Form.
	'Ne la modifiez pas à l'aide de l'éditeur de code.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKey))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdCancel = New System.Windows.Forms.Button
    Me.TxtSerial = New System.Windows.Forms.TextBox
    Me.TxtLicence = New System.Windows.Forms.TextBox
    Me.imgLogo = New System.Windows.Forms.PictureBox
    Me.LblTitre = New System.Windows.Forms.Label
    Me.LblSerial = New System.Windows.Forms.Label
    Me.LblLicence = New System.Windows.Forms.Label
    CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdOK
    '
    Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
    Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdOK.Enabled = False
    Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
    Me.cmdOK.Location = New System.Drawing.Point(88, 224)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.cmdOK.Size = New System.Drawing.Size(89, 25)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "OK"
    Me.cmdOK.UseVisualStyleBackColor = False
    '
    'cmdCancel
    '
    Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
    Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
    Me.cmdCancel.Location = New System.Drawing.Point(192, 224)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.cmdCancel.Size = New System.Drawing.Size(89, 25)
    Me.cmdCancel.TabIndex = 4
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = False
    '
    'TxtSerial
    '
    Me.TxtSerial.AcceptsReturn = True
    Me.TxtSerial.BackColor = System.Drawing.SystemColors.Window
    Me.TxtSerial.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.TxtSerial.ForeColor = System.Drawing.SystemColors.WindowText
    Me.TxtSerial.Location = New System.Drawing.Point(128, 176)
    Me.TxtSerial.MaxLength = 0
    Me.TxtSerial.Name = "TxtSerial"
    Me.TxtSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.TxtSerial.Size = New System.Drawing.Size(193, 19)
    Me.TxtSerial.TabIndex = 1
    '
    'TxtLicence
    '
    Me.TxtLicence.AcceptsReturn = True
    Me.TxtLicence.BackColor = System.Drawing.SystemColors.Window
    Me.TxtLicence.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.TxtLicence.ForeColor = System.Drawing.SystemColors.WindowText
    Me.TxtLicence.Location = New System.Drawing.Point(128, 136)
    Me.TxtLicence.MaxLength = 0
    Me.TxtLicence.Name = "TxtLicence"
    Me.TxtLicence.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.TxtLicence.Size = New System.Drawing.Size(105, 19)
    Me.TxtLicence.TabIndex = 0
    '
    'imgLogo
    '
    Me.imgLogo.BackgroundImage = CType(resources.GetObject("imgLogo.BackgroundImage"), System.Drawing.Image)
    Me.imgLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
    Me.imgLogo.Location = New System.Drawing.Point(304, 24)
    Me.imgLogo.Name = "imgLogo"
    Me.imgLogo.Size = New System.Drawing.Size(81, 81)
    Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.imgLogo.TabIndex = 5
    Me.imgLogo.TabStop = False
    '
    'LblTitre
    '
    Me.LblTitre.BackColor = System.Drawing.SystemColors.Control
    Me.LblTitre.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblTitre.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblTitre.Location = New System.Drawing.Point(24, 40)
    Me.LblTitre.Name = "LblTitre"
    Me.LblTitre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblTitre.Size = New System.Drawing.Size(233, 49)
    Me.LblTitre.TabIndex = 6
    Me.LblTitre.Text = "Please, register your licence"
    '
    'LblSerial
    '
    Me.LblSerial.BackColor = System.Drawing.SystemColors.Control
    Me.LblSerial.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblSerial.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblSerial.Location = New System.Drawing.Point(24, 176)
    Me.LblSerial.Name = "LblSerial"
    Me.LblSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblSerial.Size = New System.Drawing.Size(97, 19)
    Me.LblSerial.TabIndex = 5
    Me.LblSerial.Text = "Serial :"
    Me.LblSerial.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'LblLicence
    '
    Me.LblLicence.BackColor = System.Drawing.SystemColors.Control
    Me.LblLicence.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblLicence.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblLicence.Location = New System.Drawing.Point(24, 136)
    Me.LblLicence.Name = "LblLicence"
    Me.LblLicence.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblLicence.Size = New System.Drawing.Size(97, 19)
    Me.LblLicence.TabIndex = 2
    Me.LblLicence.Text = "License :"
    Me.LblLicence.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'frmKey
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(411, 276)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.TxtSerial)
    Me.Controls.Add(Me.TxtLicence)
    Me.Controls.Add(Me.imgLogo)
    Me.Controls.Add(Me.LblTitre)
    Me.Controls.Add(Me.LblSerial)
    Me.Controls.Add(Me.LblLicence)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(3, 29)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmKey"
    Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Text = "Licence register"
    CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region 
End Class