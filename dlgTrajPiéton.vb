'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgTrajPiéton.vb										  											'
'						Classes																														'
'							dlgTrajPiéton : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgTrajPiéton --------------------------
'Dialogue pour saisie des caractéristiques d'une traversée piétonne
'=====================================================================================================
Public Class dlgTrajPiéton
  Inherits DiagFeux.frmDlg
  Private EtatCheck As CheckState
  Public Modif As Boolean

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
  Friend WithEvents txtLgTraversée As System.Windows.Forms.TextBox
  Friend WithEvents lblLgTraversée As System.Windows.Forms.Label
  Friend WithEvents lblBranche As System.Windows.Forms.Label
  Friend WithEvents chkTraverséeDouble As System.Windows.Forms.CheckBox
  Friend WithEvents lblMètres As System.Windows.Forms.Label
  Friend WithEvents lblMètresMédiane As System.Windows.Forms.Label
  Friend WithEvents lblMédiane As System.Windows.Forms.Label
  Friend WithEvents txtMédiane As System.Windows.Forms.TextBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblLgTraversée = New System.Windows.Forms.Label
    Me.txtLgTraversée = New System.Windows.Forms.TextBox
    Me.lblBranche = New System.Windows.Forms.Label
    Me.chkTraverséeDouble = New System.Windows.Forms.CheckBox
    Me.lblMètres = New System.Windows.Forms.Label
    Me.lblMètresMédiane = New System.Windows.Forms.Label
    Me.lblMédiane = New System.Windows.Forms.Label
    Me.txtMédiane = New System.Windows.Forms.TextBox
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(314, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(312, 96)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(314, 16)
    Me.btnOK.Name = "btnOK"
    '
    'lblLgTraversée
    '
    Me.lblLgTraversée.Location = New System.Drawing.Point(32, 96)
    Me.lblLgTraversée.Name = "lblLgTraversée"
    Me.lblLgTraversée.Size = New System.Drawing.Size(192, 23)
    Me.lblLgTraversée.TabIndex = 14
    Me.lblLgTraversée.Text = "Longueur maximale de la traversée :"
    '
    'txtLgTraversée
    '
    Me.txtLgTraversée.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtLgTraversée.Location = New System.Drawing.Point(216, 96)
    Me.txtLgTraversée.Name = "txtLgTraversée"
    Me.txtLgTraversée.ReadOnly = True
    Me.txtLgTraversée.Size = New System.Drawing.Size(40, 13)
    Me.txtLgTraversée.TabIndex = 11
    Me.txtLgTraversée.Text = ""
    Me.txtLgTraversée.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblBranche
    '
    Me.lblBranche.Location = New System.Drawing.Point(24, 16)
    Me.lblBranche.Name = "lblBranche"
    Me.lblBranche.Size = New System.Drawing.Size(248, 23)
    Me.lblBranche.TabIndex = 15
    '
    'chkTraverséeDouble
    '
    Me.chkTraverséeDouble.Location = New System.Drawing.Point(24, 56)
    Me.chkTraverséeDouble.Name = "chkTraverséeDouble"
    Me.chkTraverséeDouble.Size = New System.Drawing.Size(240, 24)
    Me.chkTraverséeDouble.TabIndex = 16
    Me.chkTraverséeDouble.Text = "Branche traversée en 2 temps"
    '
    'lblMètres
    '
    Me.lblMètres.Location = New System.Drawing.Point(256, 96)
    Me.lblMètres.Name = "lblMètres"
    Me.lblMètres.Size = New System.Drawing.Size(48, 16)
    Me.lblMètres.TabIndex = 17
    Me.lblMètres.Text = "mètres"
    '
    'lblMètresMédiane
    '
    Me.lblMètresMédiane.Location = New System.Drawing.Point(256, 120)
    Me.lblMètresMédiane.Name = "lblMètresMédiane"
    Me.lblMètresMédiane.Size = New System.Drawing.Size(48, 16)
    Me.lblMètresMédiane.TabIndex = 20
    Me.lblMètresMédiane.Text = "mètres"
    '
    'lblMédiane
    '
    Me.lblMédiane.Location = New System.Drawing.Point(32, 120)
    Me.lblMédiane.Name = "lblMédiane"
    Me.lblMédiane.Size = New System.Drawing.Size(192, 23)
    Me.lblMédiane.TabIndex = 19
    Me.lblMédiane.Text = "Largeur médiane de la traversée :"
    '
    'txtMédiane
    '
    Me.txtMédiane.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtMédiane.Location = New System.Drawing.Point(216, 120)
    Me.txtMédiane.Name = "txtMédiane"
    Me.txtMédiane.ReadOnly = True
    Me.txtMédiane.Size = New System.Drawing.Size(40, 13)
    Me.txtMédiane.TabIndex = 18
    Me.txtMédiane.Text = ""
    Me.txtMédiane.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'dlgTrajPiéton
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(402, 151)
    Me.Controls.Add(Me.lblMètresMédiane)
    Me.Controls.Add(Me.lblMédiane)
    Me.Controls.Add(Me.txtMédiane)
    Me.Controls.Add(Me.lblMètres)
    Me.Controls.Add(Me.chkTraverséeDouble)
    Me.Controls.Add(Me.lblBranche)
    Me.Controls.Add(Me.lblLgTraversée)
    Me.Controls.Add(Me.txtLgTraversée)
    Me.Name = "dlgTrajPiéton"
    Me.Text = "Traversée piétonne"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.txtLgTraversée, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.lblLgTraversée, 0)
    Me.Controls.SetChildIndex(Me.lblBranche, 0)
    Me.Controls.SetChildIndex(Me.chkTraverséeDouble, 0)
    Me.Controls.SetChildIndex(Me.lblMètres, 0)
    Me.Controls.SetChildIndex(Me.txtMédiane, 0)
    Me.Controls.SetChildIndex(Me.lblMédiane, 0)
    Me.Controls.SetChildIndex(Me.lblMètresMédiane, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region


  Private Sub dlgTrajPiéton_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    EtatCheck = Me.chkTraverséeDouble.CheckState
  End Sub

  Private Sub chkTraverséeDouble_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTraverséeDouble.CheckedChanged
    Modif = (EtatCheck <> Me.chkTraverséeDouble.CheckState)
  End Sub

  Private Sub dlgTrajPiéton_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.ONGLET_CIRCULATION
  End Sub
End Class
