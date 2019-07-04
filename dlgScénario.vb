'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgScénario.vb																						'
'						Scénario
'																																							'
'						Classe																									'
'							dlgScénario																											'
'******************************************************************************
'=====================================================================================================
'--------------------------- Classe dlgScénario --------------------------
'Dialogue pour créer un scénario dans l'étude
'=====================================================================================================
Public Class dlgScénario
  Inherits DiagFeux.frmDlg
  Public maVariante As Variante

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
  Friend WithEvents lblScénario As System.Windows.Forms.Label
  Friend WithEvents txtScénario As System.Windows.Forms.TextBox
  Friend WithEvents radAvecTrafic As System.Windows.Forms.RadioButton
  Friend WithEvents radSansTrafic As System.Windows.Forms.RadioButton
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblScénario = New System.Windows.Forms.Label
    Me.txtScénario = New System.Windows.Forms.TextBox
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
    'lblScénario
    '
    Me.lblScénario.Location = New System.Drawing.Point(24, 16)
    Me.lblScénario.Name = "lblScénario"
    Me.lblScénario.Size = New System.Drawing.Size(56, 16)
    Me.lblScénario.TabIndex = 11
    Me.lblScénario.Text = "Scénario :"
    '
    'txtScénario
    '
    Me.txtScénario.Location = New System.Drawing.Point(88, 16)
    Me.txtScénario.MaxLength = 100
    Me.txtScénario.Name = "txtScénario"
    Me.txtScénario.Size = New System.Drawing.Size(264, 20)
    Me.txtScénario.TabIndex = 0
    Me.txtScénario.Text = ""
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
    'dlgScénario
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(456, 133)
    Me.Controls.Add(Me.radSansTrafic)
    Me.Controls.Add(Me.radAvecTrafic)
    Me.Controls.Add(Me.txtScénario)
    Me.Controls.Add(Me.lblScénario)
    Me.Name = "dlgScénario"
    Me.Text = "Nouveau scénario"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.lblScénario, 0)
    Me.Controls.SetChildIndex(Me.txtScénario, 0)
    Me.Controls.SetChildIndex(Me.radAvecTrafic, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.radSansTrafic, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub dlgScénario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.radSansTrafic.Enabled = maVariante.Verrou > [Global].Verrouillage.Géométrie
  End Sub

  Private Sub dlgScénario_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    If DialogResult = DialogResult.OK Then
      Dim nomScénario As String = txtScénario.Text.Trim
      Dim Message As String

      If nomScénario.Length = 0 Then
        Message = "Nom du scénario obligatoire"

      ElseIf maVariante.mPlansFeuxBase.Contains(nomScénario.Trim) Then
        Message = "Ce Scénario existe déjà"
      End If

      If Not IsNothing(Message) Then
        AfficherMessageErreur(Me, Message)
        e.Cancel = True
        Me.txtScénario.Focus()

      Else
        Try
          'Créer le nouveau scénario
          With maVariante
            If Me.radAvecTrafic.Checked Then
              'Scénario avec trafic
              .CréerScénario(nomScénario, AvecTrafic:=True)

            Else
              'Scénario sans trafic
              .CréerScénario(nomScénario, AvecTrafic:=False)
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
