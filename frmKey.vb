Option Strict Off
Option Explicit On
Friend Class frmKey
	Inherits System.Windows.Forms.Form
	
	Private Sub frmKey_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		'intialisation
		Me.Text = Titre
		Me.LblTitre.Text = Msg
		Me.LblLicence.Text = LBLICENCE
		Me.LblSerial.Text = LBSERIAL
		Me.cmdOK.Text = BtnOK
		Me.cmdCancel.Text = btnCancel
		
		'Modification de l'apparence et du contenu de lblserial selon le type de protection
		If TYPPROTECTION = CPM Then
			Me.TxtSerial.Text = LireTxt(NomFichierSerial)
			Me.TxtSerial.Visible = False
			Me.LblSerial.Visible = False
		Else
			Me.TxtSerial.Visible = True
			Me.LblSerial.Visible = True
		End If
		
	End Sub
	
	'l'utilisateur clique sur annuler
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub
	
	'l'utilisateur clique sur OK
	Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
		
		'appel de la méthode
		If VerifLicence("rien", "rien", (TxtLicence.Text), (TxtSerial.Text)) Then
			MsgBox(MSGPWDVALID)
		Else
			MsgBox(MSGPWDINVALID) 's'affiche aussi si la licence a expiré
		End If
	End Sub
	
	'gestion de l'activation du bouton OK
	Function ActivercmdOK() As Boolean
		If TYPPROTECTION = CPM Then
			If Trim(Me.TxtLicence.Text) <> "" Then
				Me.cmdOK.Enabled = True
			Else
				Me.cmdOK.Enabled = False
			End If
		Else
			If Trim(Me.TxtLicence.Text) <> "" And Trim(Me.TxtSerial.Text) <> "" Then
				Me.cmdOK.Enabled = True
			Else
				Me.cmdOK.Enabled = False
			End If
		End If
	End Function
	
	'UPGRADE_WARNING: L'événement TxtLicence.TextChanged peut se déclencher lorsque le formulaire est initialisé. Cliquez ici : 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtLicence_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtLicence.TextChanged
		ActivercmdOK()
	End Sub
	
	'UPGRADE_WARNING: L'événement TxtSerial.TextChanged peut se déclencher lorsque le formulaire est initialisé. Cliquez ici : 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtSerial_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSerial.TextChanged
		ActivercmdOK()
	End Sub
End Class