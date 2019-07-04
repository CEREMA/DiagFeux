'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Outils.vb																								'
'						Module d'outillage divers		  									'
'																																							'
'******************************************************************************
'=====================================================================================================
'--------------------------- Module Outils --------------------------
'=====================================================================================================
Module Outils
  Private dlgOuvrir As New OpenFileDialog
  Private WithEvents dlgSave As New SaveFileDialog
  Private dlgOuvrirFDP As New OpenFileDialog

  Public Enum TypeDialogueEnum
    Ouvrir
    Enregistrer
    OuvrirFDP
  End Enum

  Public Enum DataTypeEnum
    typeInt16
    typeSingle
    typeString
    typeChar
  End Enum

  'D�finition d'une marge (en pixels) entre les controles
  Public Const LGMARGE As Short = 5

  Public Function SuivantDansCollection(ByVal Index As Short, ByVal Count As Short, Optional ByVal D�calage As Short = +1) As Short
    Return CType((Index + D�calage) Mod Count, Short)
  End Function

  Public Function QuelType(ByVal dType As Type) As DataTypeEnum
    'L'autre syntaxe qui ne marche pas ici serait : If TypeOf dType Is Int16
    'Une syntaxe �quivalente : 
    'If dType Is Type.GetType("System.Int16") Then

    If dType Is GetType(Int16) Then
      QuelType = DataTypeEnum.typeInt16
    ElseIf dType Is GetType(Single) Then
      QuelType = DataTypeEnum.typeSingle
    ElseIf dType Is GetType(String) Then
      QuelType = DataTypeEnum.typeString
    End If

  End Function

  Public Function DialogueFichier(ByVal TypeDialogue As TypeDialogueEnum, ByVal Filtre As String, _
                  Optional ByVal InfoFichier As IO.FileInfo = Nothing, Optional ByVal DefaultExt As String = etuExtension) _
                  As String

    If TypeDialogue = TypeDialogueEnum.Enregistrer Then
      With dlgSave
        If IsNothing(InfoFichier) Then
          .InitialDirectory = cndParam�tres.CheminStockage ' IO.Directory.GetCurrentDirectory
        Else
          'Proposer par d�faut le m�me dossier que le dossier d'origine
          .InitialDirectory = InfoFichier.DirectoryName
          .FileName = InfoFichier.Name ' IO.Path.GetFileName(NomFichier)
        End If
        .DefaultExt = DefaultExt
        .Filter = Filtre
        ' Discutable
        '.RestoreDirectory = True
        If .ShowDialog() = DialogResult.OK Then DialogueFichier = .FileName
        .Dispose()
      End With
    Else

      Dim dlg As OpenFileDialog
      Select Case TypeDialogue
        Case TypeDialogueEnum.Ouvrir
          dlg = dlgOuvrir

        Case TypeDialogueEnum.OuvrirFDP
          dlg = dlgOuvrirFDP

      End Select

      With dlg
        .DefaultExt = DefaultExt
        If IsNothing(InfoFichier) Then
          If TypeDialogue = TypeDialogueEnum.Ouvrir Then
            If IsNothing(cndCheminStockage) Then
              .InitialDirectory = cndParam�tres.CheminStockage ' IO.Directory.GetCurrentDirectory
            Else
              .InitialDirectory = cndCheminStockage
            End If

          Else  'Ouvertude d'un fond de plan (raster ou DXF)
            .InitialDirectory = cndParam�tres.CheminFDP ' IO.Directory.GetCurrentDirectory
          End If
          .FileName = ""

        Else
          .InitialDirectory = InfoFichier.DirectoryName  ' IO.Path.GetDirectoryName(NomFichier)
          .FileName = InfoFichier.Name ' IO.Path.GetFileName(NomFichier)
        End If

        .Filter = Filtre
        ' Pr�f�rable pour les fonds de plan
        .RestoreDirectory = (TypeDialogue <> TypeDialogueEnum.Ouvrir)
        If .ShowDialog() = DialogResult.OK Then
          DialogueFichier = .FileName
          If TypeDialogue = TypeDialogueEnum.Ouvrir Then
            cndCheminStockage = IO.Path.GetDirectoryName(.FileName)
          Else
            cndParam�tres.CheminFDP = IO.Path.GetDirectoryName(.FileName)
          End If
        End If
        .Dispose()
      End With

    End If

  End Function

  Public Function DialogueDossier(ByVal NomDossier As String) As String
    Dim dlg As New FolderBrowserDialog

    With dlg
      .SelectedPath = NomDossier
      'RootFolder : par d�faut : le bureau
      '      .RootFolder = Environment.SpecialFolder.MyComputer
      If .ShowDialog() = DialogResult.OK Then
        Return .SelectedPath()
      Else
        Return NomDossier
      End If

    End With

  End Function

  Public Function ComposerFiltre(ByVal Extension As String) As String
    ComposerFiltre = "(*." & Extension & ")|*." & Extension
  End Function

  '************************************************************************************************
  ' Interdit la frappe d'une touche non num�rique
  '************************************************************************************************
  Public Function EstIncompatibleNum�rique(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean

    Select Case e.KeyValue
      Case Keys.Back, Keys.Delete, Keys.Home To Keys.Right
        'Pav� touches direction
        If e.KeyValue = Keys.Up Then EstIncompatibleNum�rique = True
      Case Keys.NumPad0 To Keys.NumPad9, Keys.Decimal
        'Pav� num�rique
      Case Keys.D0 To Keys.D9
        'Chiffre sur clavier standard
        If e.Modifiers <> Keys.Shift Then EstIncompatibleNum�rique = True
      Case Keys.Oemcomma
        'La touche OEM de virgule sur un clavier r�gional (Windows 2000 ou version ult�rieure).
      Case Keys.ShiftKey ' Touche MAJ
      Case Else
        EstIncompatibleNum�rique = True
    End Select

    'Keys.Back() '8
    'Keys.Delete	 '46
    'Keys.End	'35
    'Keys.Home	 '36
    'Keys.Left '37
    'Keys.Up '38
    'Keys.Right '39
    'Keys.Down '40

  End Function

  Public Function EstInCompatibleDate(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
    Select Case e.KeyValue
      Case Keys.Back, Keys.Delete, Keys.Home To Keys.Right
        'Pav� touches direction
        If e.KeyValue = Keys.Up Then EstInCompatibleDate = True
      Case Keys.NumPad0 To Keys.NumPad9, Keys.Divide
        'Pav� num�rique sauf le point d�cimal mais avec le '/'
      Case Keys.D0 To Keys.D9
        'Chiffre sur clavier standard
        If e.Modifiers <> Keys.Shift Then EstInCompatibleDate = True
      Case Keys.Oemcomma
        'La touche OEM de virgule sur un clavier r�gional (Windows 2000 ou version ult�rieure).
      Case Keys.ShiftKey ' Touche MAJ
      Case Else
        EstInCompatibleDate = True
    End Select

  End Function

  Public Function Confirmation(ByVal Message As String, ByVal Critique As Boolean, Optional ByVal Controle As Control = Nothing) As Boolean
    Dim Icone As MessageBoxIcon
    Dim D�faut As MessageBoxDefaultButton

    If Critique Then
      Icone = MessageBoxIcon.Exclamation
      D�faut = MessageBoxDefaultButton.Button2
    Else
      Icone = MessageBoxIcon.Question
      D�faut = MessageBoxDefaultButton.Button1
    End If

    If IsNothing(Controle) Then
      Confirmation = (MessageBox.Show(Message, NomProduit, MessageBoxButtons.YesNo, Icone, D�faut) = DialogResult.Yes)
    Else
      Confirmation = (MessageBox.Show(Controle, Message, NomProduit, MessageBoxButtons.YesNo, MessageBoxIcon.Question, D�faut) = DialogResult.Yes)
    End If
  End Function

  '**************************************************************************
  ' Indique si le caract�re frapp� est num�rique
  ' Entier : Indique si le nombre est entier (interdiction point d�cimal)
  '**************************************************************************
  Public Function ToucheNonNum�rique(ByVal c As Char, Optional ByVal Entier As Boolean = True, Optional ByVal N�gatif As Boolean = False) As Boolean

    If Not Char.IsNumber(c) Then
      Select Case c
        Case CType(vbBack, Char)
        Case cndPtD�cimal
          If Entier Then ToucheNonNum�rique = True
        Case "-"
          If Not N�gatif Then ToucheNonNum�rique = True
        Case Else
          ToucheNonNum�rique = True
          'Si on frappe le point d�cimal et que les param�tres r�gionaux comportent une autre valeur que le point d�cimal comme s�parateur, 
          ' celui-ci est refus� par la fonction pr�c�dente : on remplace le point d�cimal par le caract�re sp�cifique r�gional
          If c = "."c Then SendKeys.Send(cndPtD�cimal)
      End Select
    End If

  End Function

  Public Function D�cimalesD�pass�es(ByVal unTexte As String, ByVal nbD�cimales As Short) As Boolean
    Dim pos As Short = unTexte.IndexOf(cndPtD�cimal) + 1
    If pos <> -1 Then
      Return unTexte.Length - pos > nbD�cimales
    End If

  End Function

  '**************************************************************************
  ' Controler que la valeur saisie est entre les bornes voulues
  ' Feuille : Feuille appelante
  ' vMini : valeur minimum de la valeur
  ' vMaxi : valeur maximum de la valeur
  ' Controle : Objet Control dans lequel se fait la saisie
  ' Donn�e : Donn�e � mettre � jour si le controle est satisfaisant
  '**************************************************************************
  Public Function ControlerBornes(ByVal Feuille As Form, ByVal vMini As Double, ByVal vMaxi As Double, ByVal Controle As Control, ByVal Donn�e As Object, Optional ByVal unFormat As String = Nothing) As Boolean

    Try
      Dim v As Double = Double.Parse(Controle.Text)
      Dim strMini, strMaxi As String
      Dim Erreur As Boolean
      Dim Message As String

      If IsNothing(unFormat) Then
        strMini = CStr(vMini)
        strMaxi = CStr(vMaxi)
      Else
        strMini = Format(vMini, unFormat)
        strMaxi = Format(vMaxi, unFormat)
      End If

      If vMini <= vMaxi Then
        Erreur = v < vMini Or v > vMaxi
        If vMini = vMaxi Then
          Message = "La seule valeur possible est " & strMini
        Else
          Message = "Saisir une valeur comprise entre " & strMini & " et " & strMaxi
        End If

      Else
        Erreur = (v < vMini And v > vMaxi) Or (v > vMini And v > vMaxi) Or (v < vMini And v < vMaxi)
        Message = "Saisir une valeur sup�rieure � " & strMini & " ou inf�rieure � " & strMaxi
      End If

      If Erreur Then
        MessageBox.Show(Feuille, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ControlerBornes = True
        'On r�affiche l'ancienne valeur dans le controle
        Controle.Text = Donn�e
      Else
        'Mise � jour de la donn�e
        Donn�e = Controle.Text
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Feuille, "Valeur incorrecte")
      'On r�affiche l'ancienne valeur dans le controle
      Controle.Text = Donn�e
      ControlerBornes = True
    End Try

  End Function

  '**************************************************************************
  ' Controler que la valeur saisie est entre les bornes voulues
  ' Feuille : Feuille appelante
  ' vMini : valeur minimum de la valeur
  ' vMaxi : valeur maximum de la valeur
  ' Controle : Objet Control dans lequel se fait la saisie
  ' Donn�e : Donn�e � mettre � jour si le controle est satisfaisant
  '**************************************************************************
  Public Function ControlerBornesMiniMaxi(ByVal Feuille As Form, ByVal vMini As Double, ByVal vMaxi As Double, ByVal Controle As Control, ByVal Donn�e As Object, Optional ByVal unFormat As String = Nothing) As Boolean

    If vMini <= vMaxi Then
      ControlerBornesMiniMaxi = ControlerBornes(Feuille, vMini, vMaxi, Controle, Donn�e, unFormat)
    Else

      Try
        Dim v As Double = Double.Parse(Controle.Text)
        Dim strMini, strMaxi As String

        If IsNothing(unFormat) Then
          strMini = CStr(vMini)
          strMaxi = CStr(vMaxi)
        Else
          strMini = Format(vMini, unFormat)
          strMaxi = Format(vMaxi, unFormat)
        End If

        If v < vMini And v > vMaxi Then
          MessageBox.Show(Feuille, "Saisir une valeur comprise sup�rieure � " & strMini & " ou inf�rieure � " & strMaxi, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          ControlerBornesMiniMaxi = True
          'On r�affiche l'ancienne valeur dans le controle
          Controle.Text = Donn�e
        Else
          'Mise � jour de la donn�e
          Donn�e = Controle.Text
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Feuille, "Valeur incorrecte")
        'On r�affiche l'ancienne valeur dans le controle
        Controle.Text = Donn�e
        ControlerBornesMiniMaxi = True
      End Try


    End If
  End Function


  '**************************************************************************
  'EstNulleDate : retourn si une date n'a pas encore �t� initialis�e(dans ce cas : 01/01/0001 � 00:00:00h)
  ' Rem AV : En attendant de trouver mieux
  '**************************************************************************
  Public Function EstNulleDate(ByVal uneDate As Date) As Boolean
    If uneDate.CompareTo(CDate("01/01/0001")) = 0 Then EstNulleDate = True
  End Function

  Public Sub AfficherMessageErreur(ByVal Feuille As Form, ByVal ex As System.Exception)
    Dim Message As String
    If cndD�boguage Then
      Message = ex.ToString
    Else
      Message = ex.Message
    End If

    Dim Icone As MessageBoxIcon
    If TypeOf ex Is DiagFeux.Exception Then
      Icone = MessageBoxIcon.Exclamation
    Else
      Icone = MessageBoxIcon.Error
    End If

    If IsNothing(Feuille) Then
      MessageBox.Show(Message, NomProduit, MessageBoxButtons.OK, Icone)
    Else
      MessageBox.Show(Feuille, Message, NomProduit, MessageBoxButtons.OK, Icone)
    End If

  End Sub

  Public Sub AfficherMessageErreur(ByVal Feuille As Form, ByVal Texte As String)
    MessageBox.Show(Feuille, Texte, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  End Sub

  Public Sub LancerDiagfeuxException(ByVal ex As System.Exception, ByVal NomFonction As String)
#If DEBUG Then
    Throw New DiagFeux.Exception(ex.ToString & vbCrLf & NomFonction)
#Else
    Throw New DiagFeux.Exception(ex.Message & vbCrLf & NomFonction)
#End If
  End Sub

End Module
