'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgModeTableur.vb										  											'
'						Classes																														'
'							dlgModeTableur : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgModeTableur --------------------------
'Dialogue pour saisie des informations complémentaires pour une étude en mode tableur
'=====================================================================================================
Public Class dlgModeTableur
  Inherits DiagFeux.frmDlg

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
  Friend WithEvents lblMessage As System.Windows.Forms.Label
  Friend WithEvents lstBrancheSensUnique As System.Windows.Forms.CheckedListBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblMessage = New System.Windows.Forms.Label
    Me.lstBrancheSensUnique = New System.Windows.Forms.CheckedListBox
    Me.SuspendLayout()
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(218, 96)
    Me.btnAide.Name = "btnAide"
    Me.btnAide.Visible = False
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(218, 16)
    Me.btnOK.Name = "btnOK"
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(218, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'lblMessage
    '
    Me.lblMessage.Location = New System.Drawing.Point(16, 24)
    Me.lblMessage.Name = "lblMessage"
    Me.lblMessage.Size = New System.Drawing.Size(168, 32)
    Me.lblMessage.TabIndex = 12
    Me.lblMessage.Text = "Cochez les branches à sens unique entrant"
    '
    'lstBrancheSensUnique
    '
    Me.lstBrancheSensUnique.Items.AddRange(New Object() {"Branche A", "Branche B", "Branche C", "Branche D", "Branche E", "Branche F"})
    Me.lstBrancheSensUnique.Location = New System.Drawing.Point(16, 64)
    Me.lstBrancheSensUnique.Name = "lstBrancheSensUnique"
    Me.lstBrancheSensUnique.Size = New System.Drawing.Size(136, 94)
    Me.lstBrancheSensUnique.TabIndex = 13
    '
    'dlgModeTableur
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(314, 175)
    Me.Controls.Add(Me.lstBrancheSensUnique)
    Me.Controls.Add(Me.lblMessage)
    Me.Name = "dlgModeTableur"
    Me.Text = "Nouveau carrefour"
    Me.Controls.SetChildIndex(Me.lblMessage, 0)
    Me.Controls.SetChildIndex(Me.lstBrancheSensUnique, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public maVariante As Variante

  Friend Sub MettreAjour()
    Dim uneBranche As Branche
    Dim itemChecked As Object
    Dim Index As Short
    Dim uneLigneFeux As LigneFeuVéhicules

    For Each itemChecked In Me.lstBrancheSensUnique.CheckedItems
      Index = Me.lstBrancheSensUnique.Items.IndexOf(itemChecked)
      maVariante.mBranches(Index).NbVoies(Voie.TypeVoieEnum.VoieSortante) = 0
    Next

    For Each uneBranche In maVariante.mBranches
      If Not uneBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Then
        uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) = 0
      Else
        uneLigneFeux = New LigneFeuVéhicules("F" & CStr(maVariante.mBranches.IndexOf(uneBranche) + 1), uneBranche, cndSignaux(SignalCollection.SignalEnum.R11))
        uneLigneFeux.NbVoiesTableur = 1
        maVariante.mLignesFeux.Add(uneLigneFeux)
      End If
    Next
  End Sub

  Private Sub dlgModeTableur_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Short

    For i = maVariante.mBranches.Count + 1 To 6
      Me.lstBrancheSensUnique.Items.RemoveAt(Me.lstBrancheSensUnique.Items.Count - 1)
    Next
  End Sub
End Class
