'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : dlgTrajPi�ton.vb										  											'
'						Classes																														'
'							dlgTrajPi�ton : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgTrajPi�ton --------------------------
'Dialogue pour saisie des caract�ristiques d'une travers�e pi�tonne
'=====================================================================================================
Public Class dlgTrajPi�ton
  Inherits DiagFeux.frmDlg
  Private EtatCheck As CheckState
  Public Modif As Boolean

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
  Friend WithEvents txtLgTravers�e As System.Windows.Forms.TextBox
  Friend WithEvents lblLgTravers�e As System.Windows.Forms.Label
  Friend WithEvents lblBranche As System.Windows.Forms.Label
  Friend WithEvents chkTravers�eDouble As System.Windows.Forms.CheckBox
  Friend WithEvents lblM�tres As System.Windows.Forms.Label
  Friend WithEvents lblM�tresM�diane As System.Windows.Forms.Label
  Friend WithEvents lblM�diane As System.Windows.Forms.Label
  Friend WithEvents txtM�diane As System.Windows.Forms.TextBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblLgTravers�e = New System.Windows.Forms.Label
    Me.txtLgTravers�e = New System.Windows.Forms.TextBox
    Me.lblBranche = New System.Windows.Forms.Label
    Me.chkTravers�eDouble = New System.Windows.Forms.CheckBox
    Me.lblM�tres = New System.Windows.Forms.Label
    Me.lblM�tresM�diane = New System.Windows.Forms.Label
    Me.lblM�diane = New System.Windows.Forms.Label
    Me.txtM�diane = New System.Windows.Forms.TextBox
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
    'lblLgTravers�e
    '
    Me.lblLgTravers�e.Location = New System.Drawing.Point(32, 96)
    Me.lblLgTravers�e.Name = "lblLgTravers�e"
    Me.lblLgTravers�e.Size = New System.Drawing.Size(192, 23)
    Me.lblLgTravers�e.TabIndex = 14
    Me.lblLgTravers�e.Text = "Longueur maximale de la travers�e :"
    '
    'txtLgTravers�e
    '
    Me.txtLgTravers�e.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtLgTravers�e.Location = New System.Drawing.Point(216, 96)
    Me.txtLgTravers�e.Name = "txtLgTravers�e"
    Me.txtLgTravers�e.ReadOnly = True
    Me.txtLgTravers�e.Size = New System.Drawing.Size(40, 13)
    Me.txtLgTravers�e.TabIndex = 11
    Me.txtLgTravers�e.Text = ""
    Me.txtLgTravers�e.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblBranche
    '
    Me.lblBranche.Location = New System.Drawing.Point(24, 16)
    Me.lblBranche.Name = "lblBranche"
    Me.lblBranche.Size = New System.Drawing.Size(248, 23)
    Me.lblBranche.TabIndex = 15
    '
    'chkTravers�eDouble
    '
    Me.chkTravers�eDouble.Location = New System.Drawing.Point(24, 56)
    Me.chkTravers�eDouble.Name = "chkTravers�eDouble"
    Me.chkTravers�eDouble.Size = New System.Drawing.Size(240, 24)
    Me.chkTravers�eDouble.TabIndex = 16
    Me.chkTravers�eDouble.Text = "Branche travers�e en 2 temps"
    '
    'lblM�tres
    '
    Me.lblM�tres.Location = New System.Drawing.Point(256, 96)
    Me.lblM�tres.Name = "lblM�tres"
    Me.lblM�tres.Size = New System.Drawing.Size(48, 16)
    Me.lblM�tres.TabIndex = 17
    Me.lblM�tres.Text = "m�tres"
    '
    'lblM�tresM�diane
    '
    Me.lblM�tresM�diane.Location = New System.Drawing.Point(256, 120)
    Me.lblM�tresM�diane.Name = "lblM�tresM�diane"
    Me.lblM�tresM�diane.Size = New System.Drawing.Size(48, 16)
    Me.lblM�tresM�diane.TabIndex = 20
    Me.lblM�tresM�diane.Text = "m�tres"
    '
    'lblM�diane
    '
    Me.lblM�diane.Location = New System.Drawing.Point(32, 120)
    Me.lblM�diane.Name = "lblM�diane"
    Me.lblM�diane.Size = New System.Drawing.Size(192, 23)
    Me.lblM�diane.TabIndex = 19
    Me.lblM�diane.Text = "Largeur m�diane de la travers�e :"
    '
    'txtM�diane
    '
    Me.txtM�diane.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtM�diane.Location = New System.Drawing.Point(216, 120)
    Me.txtM�diane.Name = "txtM�diane"
    Me.txtM�diane.ReadOnly = True
    Me.txtM�diane.Size = New System.Drawing.Size(40, 13)
    Me.txtM�diane.TabIndex = 18
    Me.txtM�diane.Text = ""
    Me.txtM�diane.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'dlgTrajPi�ton
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(402, 151)
    Me.Controls.Add(Me.lblM�tresM�diane)
    Me.Controls.Add(Me.lblM�diane)
    Me.Controls.Add(Me.txtM�diane)
    Me.Controls.Add(Me.lblM�tres)
    Me.Controls.Add(Me.chkTravers�eDouble)
    Me.Controls.Add(Me.lblBranche)
    Me.Controls.Add(Me.lblLgTravers�e)
    Me.Controls.Add(Me.txtLgTravers�e)
    Me.Name = "dlgTrajPi�ton"
    Me.Text = "Travers�e pi�tonne"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.txtLgTravers�e, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.lblLgTravers�e, 0)
    Me.Controls.SetChildIndex(Me.lblBranche, 0)
    Me.Controls.SetChildIndex(Me.chkTravers�eDouble, 0)
    Me.Controls.SetChildIndex(Me.lblM�tres, 0)
    Me.Controls.SetChildIndex(Me.txtM�diane, 0)
    Me.Controls.SetChildIndex(Me.lblM�diane, 0)
    Me.Controls.SetChildIndex(Me.lblM�tresM�diane, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region


  Private Sub dlgTrajPi�ton_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    EtatCheck = Me.chkTravers�eDouble.CheckState
  End Sub

  Private Sub chkTravers�eDouble_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTravers�eDouble.CheckedChanged
    Modif = (EtatCheck <> Me.chkTravers�eDouble.CheckState)
  End Sub

  Private Sub dlgTrajPi�ton_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.ONGLET_CIRCULATION
  End Sub
End Class
