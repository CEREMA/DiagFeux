'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : dlgSc�nario.vb																						'
'						Sc�nario
'																																							'
'						Classe																									'
'							dlgSc�nario																											'
'******************************************************************************
'=====================================================================================================
'--------------------------- Classe dlgSc�nario --------------------------
'Dialogue pour cr�er un sc�nario dans l'�tude
'=====================================================================================================
Public Class dlgSc�nario
  Inherits DiagFeux.frmDlg
  Public maVariante As Variante

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
  Friend WithEvents lblSc�nario As System.Windows.Forms.Label
  Friend WithEvents txtSc�nario As System.Windows.Forms.TextBox
  Friend WithEvents radAvecTrafic As System.Windows.Forms.RadioButton
  Friend WithEvents radSansTrafic As System.Windows.Forms.RadioButton
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblSc�nario = New System.Windows.Forms.Label
    Me.txtSc�nario = New System.Windows.Forms.TextBox
    Me.radAvecTrafic = New System.Windows.Forms.RadioButton
    Me.radSansTrafic = New System.Windows.Forms.RadioButton
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Name = "btnAnnuler"
    Me.btnAnnuler.TabIndex = 4
    '
    'btnAide
    '
    Me.btnAide.Name = "btnAide"
    Me.btnAide.Visible = False
    '
    'btnOK
    '
    Me.btnOK.Name = "btnOK"
    Me.btnOK.TabIndex = 3
    '
    'lblSc�nario
    '
    Me.lblSc�nario.Location = New System.Drawing.Point(24, 16)
    Me.lblSc�nario.Name = "lblSc�nario"
    Me.lblSc�nario.Size = New System.Drawing.Size(56, 16)
    Me.lblSc�nario.TabIndex = 11
    Me.lblSc�nario.Text = "Sc�nario :"
    '
    'txtSc�nario
    '
    Me.txtSc�nario.Location = New System.Drawing.Point(88, 16)
    Me.txtSc�nario.MaxLength = 100
    Me.txtSc�nario.Name = "txtSc�nario"
    Me.txtSc�nario.Size = New System.Drawing.Size(264, 20)
    Me.txtSc�nario.TabIndex = 0
    Me.txtSc�nario.Text = ""
    '
    'radAvecTrafic
    '
    Me.radAvecTrafic.Checked = True
    Me.radAvecTrafic.Location = New System.Drawing.Point(24, 48)
    Me.radAvecTrafic.Name = "radAvecTrafic"
    Me.radAvecTrafic.Size = New System.Drawing.Size(144, 16)
    Me.radAvecTrafic.TabIndex = 1
    Me.radAvecTrafic.TabStop = True
    Me.radAvecTrafic.Text = "Avec trafic"
    '
    'radSansTrafic
    '
    Me.radSansTrafic.Location = New System.Drawing.Point(24, 72)
    Me.radSansTrafic.Name = "radSansTrafic"
    Me.radSansTrafic.Size = New System.Drawing.Size(144, 16)
    Me.radSansTrafic.TabIndex = 2
    Me.radSansTrafic.Text = "Sans trafic"
    '
    'dlgSc�nario
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(456, 133)
    Me.Controls.Add(Me.radSansTrafic)
    Me.Controls.Add(Me.radAvecTrafic)
    Me.Controls.Add(Me.txtSc�nario)
    Me.Controls.Add(Me.lblSc�nario)
    Me.Name = "dlgSc�nario"
    Me.Text = "Nouveau sc�nario"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.lblSc�nario, 0)
    Me.Controls.SetChildIndex(Me.txtSc�nario, 0)
    Me.Controls.SetChildIndex(Me.radAvecTrafic, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.radSansTrafic, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub dlgSc�nario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.radSansTrafic.Enabled = maVariante.Verrou > [Global].Verrouillage.G�om�trie
  End Sub

  Private Sub dlgSc�nario_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    If DialogResult = DialogResult.OK Then
      Dim nomSc�nario As String = txtSc�nario.Text.Trim
      Dim Message As String

      If nomSc�nario.Length = 0 Then
        Message = "Nom du sc�nario obligatoire"

      ElseIf maVariante.mPlansFeuxBase.Contains(nomSc�nario.Trim) Then
        Message = "Ce Sc�nario existe d�j�"
      End If

      If Not IsNothing(Message) Then
        AfficherMessageErreur(Me, Message)
        e.Cancel = True
        Me.txtSc�nario.Focus()

      Else
        Try
          'Cr�er le nouveau sc�nario
          With maVariante
            If Me.radAvecTrafic.Checked Then
              'Sc�nario avec trafic
              .Cr�erSc�nario(nomSc�nario, AvecTrafic:=True)

            Else
              'Sc�nario sans trafic
              .Cr�erSc�nario(nomSc�nario, AvecTrafic:=False)
            End If
          End With

        Catch ex As System.Exception
          AfficherMessageErreur(Me, ex)
          e.Cancel = True
        End Try

      End If

    End If

  End Sub

End Class
