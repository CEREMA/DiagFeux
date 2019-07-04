'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : dlgImportDXF.vb										  											'
'						Classes																														'
'							dlgImportDXF : Dialogue               												'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On
Imports Grille = C1.Win.C1FlexGrid

Friend Class dlgImportDXF
  Inherits DiagFeux.frmDlg
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
  Public WithEvents txtEchelle As System.Windows.Forms.TextBox
  Public WithEvents lblUnité As System.Windows.Forms.Label
  Public WithEvents lblEchelle As System.Windows.Forms.Label
  'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
  'Il peut être modifié à l'aide du Concepteur Windows Form.
  'Ne pas le modifier à l'aide de l'éditeur de code.
  Friend WithEvents AC1ListPlans As GrilleDiagfeux
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.txtEchelle = New System.Windows.Forms.TextBox
    Me.lblUnité = New System.Windows.Forms.Label
    Me.lblEchelle = New System.Windows.Forms.Label
    Me.AC1ListPlans = New DiagFeux.GrilleDiagfeux
    CType(Me.AC1ListPlans, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnAnnuler
    '
    Me.btnAnnuler.Name = "btnAnnuler"
    '
    'btnAide
    '
    Me.btnAide.Name = "btnAide"
    '
    'btnOK
    '
    Me.btnOK.Name = "btnOK"
    '
    'txtEchelle
    '
    Me.txtEchelle.AcceptsReturn = True
    Me.txtEchelle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEchelle.AutoSize = False
    Me.txtEchelle.BackColor = System.Drawing.SystemColors.Window
    Me.txtEchelle.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.txtEchelle.ForeColor = System.Drawing.SystemColors.WindowText
    Me.txtEchelle.Location = New System.Drawing.Point(120, 272)
    Me.txtEchelle.MaxLength = 0
    Me.txtEchelle.Name = "txtEchelle"
    Me.txtEchelle.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.txtEchelle.Size = New System.Drawing.Size(33, 20)
    Me.txtEchelle.TabIndex = 3
    Me.txtEchelle.Text = "1"
    Me.txtEchelle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'lblUnité
    '
    Me.lblUnité.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblUnité.BackColor = System.Drawing.SystemColors.Control
    Me.lblUnité.Cursor = System.Windows.Forms.Cursors.Default
    Me.lblUnité.ForeColor = System.Drawing.SystemColors.ControlText
    Me.lblUnité.Location = New System.Drawing.Point(160, 280)
    Me.lblUnité.Name = "lblUnité"
    Me.lblUnité.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblUnité.Size = New System.Drawing.Size(33, 17)
    Me.lblUnité.TabIndex = 4
    Me.lblUnité.Text = "m"
    '
    'lblEchelle
    '
    Me.lblEchelle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblEchelle.BackColor = System.Drawing.SystemColors.Control
    Me.lblEchelle.Cursor = System.Windows.Forms.Cursors.Default
    Me.lblEchelle.ForeColor = System.Drawing.SystemColors.ControlText
    Me.lblEchelle.Location = New System.Drawing.Point(24, 280)
    Me.lblEchelle.Name = "lblEchelle"
    Me.lblEchelle.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblEchelle.Size = New System.Drawing.Size(89, 17)
    Me.lblEchelle.TabIndex = 2
    Me.lblEchelle.Text = "Echelle : 1 unité ="
    '
    'AC1ListPlans
    '
    Me.AC1ListPlans.BackColor = System.Drawing.SystemColors.Window
    Me.AC1ListPlans.ColumnInfo = "2,0,0,0,0,85,Columns:0{Width:20;DataType:System.Boolean;ImageAlign:CenterCenter;}" & _
    "" & Microsoft.VisualBasic.ChrW(9) & "1{Width:300;AllowSorting:False;Caption:""Nom des calques"";AllowEditing:False;Dat" & _
    "aType:System.String;TextAlign:LeftCenter;}" & Microsoft.VisualBasic.ChrW(9)
    Me.AC1ListPlans.Location = New System.Drawing.Point(8, 16)
    Me.AC1ListPlans.Name = "AC1ListPlans"
    Me.AC1ListPlans.Rows.Count = 14
    Me.AC1ListPlans.Size = New System.Drawing.Size(344, 240)
    Me.AC1ListPlans.Styles = New C1.Win.C1FlexGrid.CellStyleCollection("Fixed{BackColor:Control;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}" & Microsoft.VisualBasic.ChrW(9) & "Hi" & _
    "ghlight{BackColor:Highlight;ForeColor:HighlightText;}" & Microsoft.VisualBasic.ChrW(9) & "Search{BackColor:Highlight" & _
    ";ForeColor:HighlightText;}" & Microsoft.VisualBasic.ChrW(9) & "Frozen{BackColor:Beige;}" & Microsoft.VisualBasic.ChrW(9) & "EmptyArea{BackColor:AppWorks" & _
    "pace;Border:Flat,1,ControlDarkDark,Both;}" & Microsoft.VisualBasic.ChrW(9) & "GrandTotal{BackColor:Black;ForeColor:W" & _
    "hite;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal1{BackColor" & _
    ":ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal2{BackColor:ControlDarkDark;ForeColor" & _
    ":White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal4{BackCol" & _
    "or:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal5{BackColor:ControlDarkDark;ForeCol" & _
    "or:White;}" & Microsoft.VisualBasic.ChrW(9))
    Me.AC1ListPlans.TabIndex = 7
    '
    'dlgImportDXF
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.btnAnnuler
    Me.ClientSize = New System.Drawing.Size(454, 319)
    Me.Controls.Add(Me.AC1ListPlans)
    Me.Controls.Add(Me.txtEchelle)
    Me.Controls.Add(Me.lblUnité)
    Me.Controls.Add(Me.lblEchelle)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Location = New System.Drawing.Point(75, 126)
    Me.Name = "dlgImportDXF"
    Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Options d'importation"
    Me.Controls.SetChildIndex(Me.lblEchelle, 0)
    Me.Controls.SetChildIndex(Me.lblUnité, 0)
    Me.Controls.SetChildIndex(Me.txtEchelle, 0)
    Me.Controls.SetChildIndex(Me.AC1ListPlans, 0)
    Me.Controls.SetChildIndex(Me.btnOK, 0)
    Me.Controls.SetChildIndex(Me.btnAnnuler, 0)
    Me.Controls.SetChildIndex(Me.btnAide, 0)
    CType(Me.AC1ListPlans, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

  Private flagKeyPress As Boolean
  Private mCalques As CalqueCollection

  '**************************************************************************************
  '     GIRATION v3 - CERTU/CETE de l'Ouest
  '         Septembre 97

  '   Réalisation : André VIGNAUD

  '   Module de feuille : dlgImportDXF   -   dlgImportDXF.vb
  '   Feuille permettant le choix des plans à retenir lors de l'imoort d'un fichier DXF

  '**************************************************************************************

  Private Const ID_Echelle As String = "Echelle"                        ' Import
  Private Const Idm_Obligatoire As String = "obligatoire"               ' Import

  Private Sub txtEchelle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
  Handles txtEchelle.KeyDown
    flagKeyPress = EstIncompatibleNumérique(e)

  End Sub

  Private Sub txtEchelle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEchelle.KeyPress
    Dim txt As TextBox = sender

    If flagKeyPress Then
      'Touche refusée par l'évènement KeyDown
      e.Handled = True
      flagKeyPress = False
    Else
      ' peut-être peut-on  accepter une echelle non entière ?
      e.Handled = ToucheNonNumérique(e.KeyChar, Entier:=False)
    End If

  End Sub

  Private Sub txtEchelle_Validating(ByVal Sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
  Handles txtEchelle.Validating
    Dim txt As TextBox = Sender

    If Not IsNumeric(txt.Text) Then
      MsgBox("Saisie incorrecte")
      e.Cancel = True
    End If

  End Sub

  '***************************************************************************************************
  ' Chargement de la feuille
  '***************************************************************************************************
  Private Sub frmImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    Dim unCalque As Calque
    Dim rg As Grille.CellRange
    Dim row As Short
    Dim OK As Boolean = True

    With AC1ListPlans
      .Rows.Count = mCalques.Count + 1
      For Each unCalque In mCalques
        row += 1
        rg = .GetCellRange(row, 0, row, 1)
        rg.Clip = unCalque.Visible.ToString & Chr(9) & unCalque.Nom
        '.Col = 3
        '.BackColor = System.Drawing.ColorTranslator.FromOle(QBColor(tCouleur(System.Math.Abs(unCalque.Couleur))))      ' la couleur peut être négative si plan inactif dans le DXF
        '.Col = 4
        '.TypeComboBoxCurSel = 0      'type de ligne continu
        'On conserve systématiquement le plan "0" (idem AutoCAD)
        If unCalque.Nom = "0" Then .Rows(row).Visible = False
      Next unCalque

      If mCalques.Count = 1 Then
        .Visible = False
        Me.ClientSize = New Size(Me.ClientSize.Width, 80)
      End If
    End With

  End Sub

  Public Property Calques() As CalqueCollection
    Get
      Return mCalques
    End Get
    Set(ByVal Value As CalqueCollection)
      mCalques = Value
    End Set
  End Property

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    If txtEchelle.Text = "" Then
      MsgBox(ID_Echelle & " " & Idm_Obligatoire)
      txtEchelle.Focus()

    Else
      Dim fg As GrilleDiagfeux = Me.AC1ListPlans
      With fg
        Dim unCalque As Calque
        Dim rg As Grille.CellRange
        For Each unCalque In mCalques
          rg = .GetCellRange(mCalques.IndexOf(unCalque) + 1, 0)
          unCalque.Visible = (rg.Checkbox = Grille.CheckEnum.Checked)
        Next unCalque

      End With

      DialogResult = DialogResult.OK
      Me.Close()
    End If

  End Sub

  Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
    '******************************************************************************
    ' Bouton Annuler
    '******************************************************************************
    Me.Close()
  End Sub

  Private Sub frmImport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    TopicAideCourant = [Global].AideEnum.MENU_NOUVEAU
  End Sub
End Class