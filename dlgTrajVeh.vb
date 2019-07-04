'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : dlgTrajVeh.vb										  											'
'						Classes																														'
'							dlgTrajVeh : Dialogue               												'
'																																							'
'******************************************************************************

'=====================================================================================================
'--------------------------- Classe dlgTrajVeh --------------------------
'Dialogue pour saisie des caract�ristiques d'une trajectoire v�hicules
'=====================================================================================================
Public Class dlgTrajVeh
  Inherits DiagFeux.frmDlg

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
  Friend WithEvents lblListeAcc�s As System.Windows.Forms.Label
  Friend WithEvents btnDessiner As System.Windows.Forms.Button
  Friend WithEvents chkManuel As System.Windows.Forms.CheckBox
  Friend WithEvents pnlManuel As System.Windows.Forms.Panel
  Friend WithEvents pnlPropTrajectoire As System.Windows.Forms.Panel
  Friend WithEvents radTAG As System.Windows.Forms.RadioButton
  Friend WithEvents radTD As System.Windows.Forms.RadioButton
  Friend WithEvents radTAD As System.Windows.Forms.RadioButton
  Friend WithEvents txtCoefGene As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cboTypeCourant As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.lblListeAcc�s = New System.Windows.Forms.Label
    Me.pnlManuel = New System.Windows.Forms.Panel
    Me.btnDessiner = New System.Windows.Forms.Button
    Me.chkManuel = New System.Windows.Forms.CheckBox
    Me.pnlPropTrajectoire = New System.Windows.Forms.Panel
    Me.radTAG = New System.Windows.Forms.RadioButton
    Me.radTD = New System.Windows.Forms.RadioButton
    Me.radTAD = New System.Windows.Forms.RadioButton
    Me.txtCoefGene = New System.Windows.Forms.TextBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.cboTypeCourant = New System.Windows.Forms.ComboBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.pnlManuel.SuspendLayout()
    Me.pnlPropTrajectoire.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Location = New System.Drawing.Point(306, 56)
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnAide
    '
    Me.btnAide.Location = New System.Drawing.Point(304, 96)
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(306, 16)
    Me.btnOK.Name = "btnOK"
    '
    'lblListeAcc�s
    '
    Me.lblListeAcc�s.Location = New System.Drawing.Point(8, 16)
    Me.lblListeAcc�s.Name = "lblListeAcc�s"
    Me.lblListeAcc�s.Size = New System.Drawing.Size(280, 24)
    Me.lblListeAcc�s.TabIndex = 11
    Me.lblListeAcc�s.Text = "Trajectoire depuis l'acc�s A vers l'acc�s B"
    '
    'pnlManuel
    '
    Me.pnlManuel.Controls.Add(Me.btnDessiner)
    Me.pnlManuel.Controls.Add(Me.chkManuel)
    Me.pnlManuel.Location = New System.Drawing.Point(0, 168)
    Me.pnlManuel.Name = "pnlManuel"
    Me.pnlManuel.Size = New System.Drawing.Size(232, 32)
    Me.pnlManuel.TabIndex = 21
    '
    'btnDessiner
    '
    Me.btnDessiner.Enabled = False
    Me.btnDessiner.Location = New System.Drawing.Point(128, 4)
    Me.btnDessiner.Name = "btnDessiner"
    Me.btnDessiner.Size = New System.Drawing.Size(88, 24)
    Me.btnDessiner.TabIndex = 24
    Me.btnDessiner.Text = "Redessiner..."
    '
    'chkManuel
    '
    Me.chkManuel.Location = New System.Drawing.Point(16, 4)
    Me.chkManuel.Name = "chkManuel"
    Me.chkManuel.Size = New System.Drawing.Size(104, 16)
    Me.chkManuel.TabIndex = 23
    Me.chkManuel.Text = "Dessin manuel"
    '
    'pnlPropTrajectoire
    '
    Me.pnlPropTrajectoire.Controls.Add(Me.radTAG)
    Me.pnlPropTrajectoire.Controls.Add(Me.radTD)
    Me.pnlPropTrajectoire.Controls.Add(Me.radTAD)
    Me.pnlPropTrajectoire.Controls.Add(Me.txtCoefGene)
    Me.pnlPropTrajectoire.Controls.Add(Me.Label4)
    Me.pnlPropTrajectoire.Controls.Add(Me.cboTypeCourant)
    Me.pnlPropTrajectoire.Controls.Add(Me.Label3)
    Me.pnlPropTrajectoire.Controls.Add(Me.Label2)
    Me.pnlPropTrajectoire.Location = New System.Drawing.Point(0, 40)
    Me.pnlPropTrajectoire.Name = "pnlPropTrajectoire"
    Me.pnlPropTrajectoire.Size = New System.Drawing.Size(272, 112)
    Me.pnlPropTrajectoire.TabIndex = 22
    '
    'radTAG
    '
    Me.radTAG.Location = New System.Drawing.Point(124, 37)
    Me.radTAG.Name = "radTAG"
    Me.radTAG.Size = New System.Drawing.Size(48, 24)
    Me.radTAG.TabIndex = 28
    Me.radTAG.Text = "TAG"
    '
    'radTD
    '
    Me.radTD.Location = New System.Drawing.Point(172, 37)
    Me.radTD.Name = "radTD"
    Me.radTD.Size = New System.Drawing.Size(40, 24)
    Me.radTD.TabIndex = 27
    Me.radTD.Text = "TD"
    '
    'radTAD
    '
    Me.radTAD.Location = New System.Drawing.Point(212, 37)
    Me.radTAD.Name = "radTAD"
    Me.radTAD.Size = New System.Drawing.Size(48, 24)
    Me.radTAD.TabIndex = 26
    Me.radTAD.Text = "TAD"
    '
    'txtCoefGene
    '
    Me.txtCoefGene.Location = New System.Drawing.Point(128, 80)
    Me.txtCoefGene.Name = "txtCoefGene"
    Me.txtCoefGene.Size = New System.Drawing.Size(48, 20)
    Me.txtCoefGene.TabIndex = 25
    Me.txtCoefGene.Text = "1,0"
    Me.txtCoefGene.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label4
    '
    Me.Label4.Location = New System.Drawing.Point(16, 80)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(112, 23)
    Me.Label4.TabIndex = 24
    Me.Label4.Text = "Coefficient de g�ne :"
    '
    'cboTypeCourant
    '
    Me.cboTypeCourant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboTypeCourant.Items.AddRange(New Object() {"Mixte", "TC", "V�los"})
    Me.cboTypeCourant.Location = New System.Drawing.Point(124, 5)
    Me.cboTypeCourant.Name = "cboTypeCourant"
    Me.cboTypeCourant.Size = New System.Drawing.Size(121, 21)
    Me.cboTypeCourant.TabIndex = 23
    '
    'Label3
    '
    Me.Label3.Location = New System.Drawing.Point(12, 45)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(104, 23)
    Me.Label3.TabIndex = 22
    Me.Label3.Text = "Nature de courant :"
    '
    'Label2
    '
    Me.Label2.Location = New System.Drawing.Point(12, 5)
    Me.Label2.Name = "Label2"
    Me.Label2.TabIndex = 21
    Me.Label2.Text = "Type de courant :"
    '
    'dlgTrajVeh
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(394, 215)
    Me.Controls.Add(Me.pnlPropTrajectoire)
    Me.Controls.Add(Me.pnlManuel)
    Me.Controls.Add(Me.lblListeAcc�s)
    Me.Name = "dlgTrajVeh"
    Me.Text = "Fiche caract�ristique de la trajectoire v�hicules"
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    Me.Controls.SetChildIndex(Me.lblListeAcc�s, 0)
    Me.Controls.SetChildIndex(Me.pnlManuel, 0)
    Me.Controls.SetChildIndex(Me.pnlPropTrajectoire, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.pnlManuel.ResumeLayout(False)
    Me.pnlPropTrajectoire.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public mCourant As Courant
  Public mTypeCourant As TrajectoireV�hicules.TypeCourantEnum
  Public Cr�ation As Boolean
  Public ToutDroitPossible As Boolean

  Private ChargementEnCours As Boolean
  Private MessageToutDroit As String = "L'angle est >= 110� et <=250� : vous avez la possiblilit� de le d�clarer en TD." _
  & vbCrLf & "Si la rue est en pente, il peut �tre pr�f�rable de la d�clarer en TD."

  Private Sub radTAD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles radTAD.CheckedChanged, radTD.CheckedChanged, radTAG.CheckedChanged

    If Not ChargementEnCours Then
      Dim rad As RadioButton = sender
      ' Changement de nature de courannt
      If radTAD.Checked Then
        mCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAD
        mCourant.CoefG�ne = CoefG�neTAD
      ElseIf radTD.Checked Then
        mCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD
        mCourant.CoefG�ne = 1.0
      ElseIf radTAG.Checked Then
        mCourant.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG
        mCourant.CoefG�ne = CoefG�neTAG
      End If

      If Not radTD.Checked And ToutDroitPossible Then
        MessageBox.Show(MessageToutDroit, "Angle des branches", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'Ne pas r�afficher le message x fois
        ToutDroitPossible = False
      End If
      Me.txtCoefGene.Text = CStr(mCourant.CoefG�ne)
    End If

  End Sub

  Private Sub dlgTrajVeh_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    If DialogResult = DialogResult.OK Then
      If (Not Me.radTAD.Checked) And (Not Me.radTD.Checked) And (Not Me.radTAG.Checked) Then
        AfficherMessageErreur(Me, "S�lectionner un courant")
        e.Cancel = True
      End If
    End If
  End Sub

  Private Sub chkManuel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManuel.CheckedChanged
    Me.btnDessiner.Enabled = (chkManuel.Checked)
  End Sub

  Private Sub btnDessiner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDessiner.Click
    Me.Hide()
    Me.DialogResult = DialogResult.Retry
  End Sub

  '******************************************************************************
  ' Ev�nements de gestion de la saisie
  '******************************************************************************
  Private Sub txtCoefGene_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
  Handles txtCoefGene.KeyPress
    Dim txt As TextBox

    txt = sender
    e.Handled = ToucheNonNum�rique(e.KeyChar, Entier:=False)

  End Sub

  Private Sub txtCoefGene_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
    Handles txtCoefGene.Validating
    Dim txt As TextBox = sender
    Dim Donn�e As Double
    Dim chaine As String = txt.Text

    Try
      Donn�e = mCourant.CoefG�ne
      e.Cancel = ControlerBornes(Me, MinCoefG�ne, MaxCoefG�ne, txt, Donn�e, unFormat:="0.0")
      If Not e.Cancel Then mCourant.CoefG�ne = CSng(txt.Text)
    Catch ex As System.Exception
      AfficherMessageErreur(Me, ex)

    End Try

  End Sub

  Private Sub dlgTrajVeh_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    cboTypeCourant.SelectedIndex = mTypeCourant
    ChargementEnCours = True
    Select Case mCourant.NatureCourant
      Case TrajectoireV�hicules.NatureCourantEnum.TAD
        radTAD.Checked = True
      Case TrajectoireV�hicules.NatureCourantEnum.TD
        If Not Cr�ation Then
          'En cr�ation, on ne propose pas de valeur par d�faut pour la nature du courant
          radTD.Checked = True
        End If
      Case TrajectoireV�hicules.NatureCourantEnum.TAG
        radTAG.Checked = True
    End Select
    ChargementEnCours = False

    txtCoefGene.Text = CType(mCourant.CoefG�ne, String)

  End Sub

  Private Sub cboTypeCourant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTypeCourant.SelectedIndexChanged
    mTypeCourant = cboTypeCourant.SelectedIndex
  End Sub

  Private Sub dlgTrajVeh_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.ONGLET_CIRCULATION
  End Sub
End Class
