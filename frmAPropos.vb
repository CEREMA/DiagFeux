Imports Microsoft.Win32

'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : frmAPropos.vb										  											'
'						Classes																														'
'							frmAPropos : Feuille                												'
'																																							'
'******************************************************************************
Public Class frmAPropos
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
  Friend WithEvents picIcone As System.Windows.Forms.PictureBox
  Friend WithEvents lblTitre As System.Windows.Forms.Label
  Friend WithEvents lblVersion As System.Windows.Forms.Label
  Friend WithEvents lblDescription As System.Windows.Forms.Label
  Friend WithEvents btnInfoSys As System.Windows.Forms.Button
  Friend WithEvents btnOK As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPropos))
        Me.picIcone = New System.Windows.Forms.PictureBox
        Me.lblTitre = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.btnInfoSys = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        CType(Me.picIcone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picIcone
        '
        Me.picIcone.BackgroundImage = CType(resources.GetObject("picIcone.BackgroundImage"), System.Drawing.Image)
        Me.picIcone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picIcone.Location = New System.Drawing.Point(8, 8)
        Me.picIcone.Name = "picIcone"
        Me.picIcone.Size = New System.Drawing.Size(48, 40)
        Me.picIcone.TabIndex = 0
        Me.picIcone.TabStop = False
        '
        'lblTitre
        '
        Me.lblTitre.Location = New System.Drawing.Point(72, 16)
        Me.lblTitre.Name = "lblTitre"
        Me.lblTitre.Size = New System.Drawing.Size(96, 24)
        Me.lblTitre.TabIndex = 1
        Me.lblTitre.Text = "Titre"
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(72, 56)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(128, 24)
        Me.lblVersion.TabIndex = 2
        Me.lblVersion.Text = "Version"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(72, 96)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(208, 40)
        Me.lblDescription.TabIndex = 3
        Me.lblDescription.Text = "Description"
        '
        'btnInfoSys
        '
        Me.btnInfoSys.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInfoSys.Location = New System.Drawing.Point(144, 152)
        Me.btnInfoSys.Name = "btnInfoSys"
        Me.btnInfoSys.Size = New System.Drawing.Size(136, 24)
        Me.btnInfoSys.TabIndex = 4
        Me.btnInfoSys.Text = "Informations système..."
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(144, 192)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(136, 24)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        '
        'frmAPropos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 229)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnInfoSys)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblTitre)
        Me.Controls.Add(Me.picIcone)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAPropos"
        Me.Text = "APropos"
        CType(Me.picIcone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

  Private Sub APropos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.lblTitre.Text = NomProduit
        Me.lblVersion.Text = "Version " & VersionSignificative() & vbCrLf & LBLICENCE & NumeroLicence ' myFileVersionInfo.ProductVersion
        Me.lblDescription.Text = myFileVersionInfo.Comments

  End Sub

  Private Sub StartSysInfo()

    Dim reg As Registry

    Dim regkey As RegistryKey = reg.LocalMachine
    Const gREGKEYSYSINFO As String = "SOFTWARE\Microsoft\Shared Tools\MSINFO"
    Const gREGVALSYSINFO As String = "PATH"
    Const gREGKEYSYSINFOLOC As String = "SOFTWARE\Microsoft\Shared Tools Location"
    Const gREGVALSYSINFOLOC As String = "MSINFO"

    ' Essaie d'obtenir le chemin et le nom du programme Infos système dans la base de registre...
    Dim SysInfoPath As String = regkey.CreateSubKey(gREGKEYSYSINFO).GetValue(gREGVALSYSINFO)

    If IsNothing(SysInfoPath) OrElse SysInfoPath.Length = 0 Then
      ' Essaie d'obtenir uniquement le chemin du programme Infos système dans la base de registre...
      SysInfoPath = regkey.CreateSubKey(gREGKEYSYSINFOLOC).GetValue(gREGVALSYSINFOLOC)
      If IsNothing(SysInfoPath) Then
        SysInfoPath = ""
      Else
        SysInfoPath = IO.Path.Combine(SysInfoPath, "MSINFO.EXE")
      End If
    End If

    If IO.File.Exists(SysInfoPath) Then
      Shell(SysInfoPath)
    Else
      MessageBox.Show(Me, "Les informations système ne sont pas disponibles actuellement", MessageBoxButtons.OK)
    End If

  End Sub

  Private Sub btnInfoSys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfoSys.Click
    StartSysInfo()
  End Sub

End Class
