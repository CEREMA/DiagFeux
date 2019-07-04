'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
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

  'Définition d'une marge (en pixels) entre les controles
  Public Const LGMARGE As Short = 5

  Public Function SuivantDansCollection(ByVal Index As Short, ByVal Count As Short, Optional ByVal Décalage As Short = +1) As Short
    Return CType((Index + Décalage) Mod Count, Short)
  End Function

  Public Function QuelType(ByVal dType As Type) As DataTypeEnum
    'L'autre syntaxe qui ne marche pas ici serait : If TypeOf dType Is Int16
    'Une syntaxe équivalente : 
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
          .InitialDirectory = cndParamètres.CheminStockage ' IO.Directory.GetCurrentDirectory
        Else
          'Proposer par défaut le même dossier que le dossier d'origine
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
              .InitialDirectory = cndParamètres.CheminStockage ' IO.Directory.GetCurrentDirectory
            Else
              .InitialDirectory = cndCheminStockage
            End If

          Else  'Ouvertude d'un fond de plan (raster ou DXF)
            .InitialDirectory = cndParamètres.CheminFDP ' IO.Directory.GetCurrentDirectory
          End If
          .FileName = ""

        Else
          .InitialDirectory = InfoFichier.DirectoryName  ' IO.Path.GetDirectoryName(NomFichier)
          .FileName = InfoFichier.Name ' IO.Path.GetFileName(NomFichier)
        End If

        .Filter = Filtre
        ' Préférable pour les fonds de plan
        .RestoreDirectory = (TypeDialogue <> TypeDialogueEnum.Ouvrir)
        If .ShowDialog() = DialogResult.OK Then
          DialogueFichier = .FileName
          If TypeDialogue = TypeDialogueEnum.Ouvrir Then
            cndCheminStockage = IO.Path.GetDirectoryName(.FileName)
          Else
            cndParamètres.CheminFDP = IO.Path.GetDirectoryName(.FileName)
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
      'RootFolder : par défaut : le bureau
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
  ' Interdit la frappe d'une touche non numérique
  '************************************************************************************************
  Public Function EstIncompatibleNumérique(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean

    Select Case e.KeyValue
      Case Keys.Back, Keys.Delete, Keys.Home To Keys.Right
        'Pavé touches direction
        If e.KeyValue = Keys.Up Then EstIncompatibleNumérique = True
      Case Keys.NumPad0 To Keys.NumPad9, Keys.Decimal
        'Pavé numérique
      Case Keys.D0 To Keys.D9
        'Chiffre sur clavier standard
        If e.Modifiers <> Keys.Shift Then EstIncompatibleNumérique = True
      Case Keys.Oemcomma
        'La touche OEM de virgule sur un clavier régional (Windows 2000 ou version ultérieure).
      Case Keys.ShiftKey ' Touche MAJ
      Case Else
        EstIncompatibleNumérique = True
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
        'Pavé touches direction
        If e.KeyValue = Keys.Up Then EstInCompatibleDate = True
      Case Keys.NumPad0 To Keys.NumPad9, Keys.Divide
        'Pavé numérique sauf le point décimal mais avec le '/'
      Case Keys.D0 To Keys.D9
        'Chiffre sur clavier standard
        If e.Modifiers <> Keys.Shift Then EstInCompatibleDate = True
      Case Keys.Oemcomma
        'La touche OEM de virgule sur un clavier régional (Windows 2000 ou version ultérieure).
      Case Keys.ShiftKey ' Touche MAJ
      Case Else
        EstInCompatibleDate = True
    End Select

  End Function

  Public Function Confirmation(ByVal Message As String, ByVal Critique As Boolean, Optional ByVal Controle As Control = Nothing) As Boolean
    Dim Icone As MessageBoxIcon
    Dim Défaut As MessageBoxDefaultButton

    If Critique Then
      Icone = MessageBoxIcon.Exclamation
      Défaut = MessageBoxDefaultButton.Button2
    Else
      Icone = MessageBoxIcon.Question
      Défaut = MessageBoxDefaultButton.Button1
    End If

    If IsNothing(Controle) Then
      Confirmation = (MessageBox.Show(Message, NomProduit, MessageBoxButtons.YesNo, Icone, Défaut) = DialogResult.Yes)
    Else
      Confirmation = (MessageBox.Show(Controle, Message, NomProduit, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Défaut) = DialogResult.Yes)
    End If
  End Function

  '**************************************************************************
  ' Indique si le caractère frappé est numérique
  ' Entier : Indique si le nombre est entier (interdiction point décimal)
  '**************************************************************************
  Public Function ToucheNonNumérique(ByVal c As Char, Optional ByVal Entier As Boolean = True, Optional ByVal Négatif As Boolean = False) As Boolean

    If Not Char.IsNumber(c) Then
      Select Case c
        Case CType(vbBack, Char)
        Case cndPtDécimal
          If Entier Then ToucheNonNumérique = True
        Case "-"
          If Not Négatif Then ToucheNonNumérique = True
        Case Else
          ToucheNonNumérique = True
          'Si on frappe le point décimal et que les paramètres régionaux comportent une autre valeur que le point décimal comme séparateur, 
          ' celui-ci est refusé par la fonction précédente : on remplace le point décimal par le caractère spécifique régional
          If c = "."c Then SendKeys.Send(cndPtDécimal)
      End Select
    End If

  End Function

  Public Function DécimalesDépassées(ByVal unTexte As String, ByVal nbDécimales As Short) As Boolean
    Dim pos As Short = unTexte.IndexOf(cndPtDécimal) + 1
    If pos <> -1 Then
      Return unTexte.Length - pos > nbDécimales
    End If

  End Function

  '**************************************************************************
  ' Controler que la valeur saisie est entre les bornes voulues
  ' Feuille : Feuille appelante
  ' vMini : valeur minimum de la valeur
  ' vMaxi : valeur maximum de la valeur
  ' Controle : Objet Control dans lequel se fait la saisie
  ' Donnée : Donnée à mettre à jour si le controle est satisfaisant
  '**************************************************************************
  Public Function ControlerBornes(ByVal Feuille As Form, ByVal vMini As Double, ByVal vMaxi As Double, ByVal Controle As Control, ByVal Donnée As Object, Optional ByVal unFormat As String = Nothing) As Boolean

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
        Message = "Saisir une valeur supérieure à " & strMini & " ou inférieure à " & strMaxi
      End If

      If Erreur Then
        MessageBox.Show(Feuille, Message, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ControlerBornes = True
        'On réaffiche l'ancienne valeur dans le controle
        Controle.Text = Donnée
      Else
        'Mise à jour de la donnée
        Donnée = Controle.Text
      End If

    Catch ex As System.Exception
      AfficherMessageErreur(Feuille, "Valeur incorrecte")
      'On réaffiche l'ancienne valeur dans le controle
      Controle.Text = Donnée
      ControlerBornes = True
    End Try

  End Function

  '**************************************************************************
  ' Controler que la valeur saisie est entre les bornes voulues
  ' Feuille : Feuille appelante
  ' vMini : valeur minimum de la valeur
  ' vMaxi : valeur maximum de la valeur
  ' Controle : Objet Control dans lequel se fait la saisie
  ' Donnée : Donnée à mettre à jour si le controle est satisfaisant
  '**************************************************************************
  Public Function ControlerBornesMiniMaxi(ByVal Feuille As Form, ByVal vMini As Double, ByVal vMaxi As Double, ByVal Controle As Control, ByVal Donnée As Object, Optional ByVal unFormat As String = Nothing) As Boolean

    If vMini <= vMaxi Then
      ControlerBornesMiniMaxi = ControlerBornes(Feuille, vMini, vMaxi, Controle, Donnée, unFormat)
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
          MessageBox.Show(Feuille, "Saisir une valeur comprise supérieure à " & strMini & " ou inférieure à " & strMaxi, NomProduit, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          ControlerBornesMiniMaxi = True
          'On réaffiche l'ancienne valeur dans le controle
          Controle.Text = Donnée
        Else
          'Mise à jour de la donnée
          Donnée = Controle.Text
        End If

      Catch ex As System.Exception
        AfficherMessageErreur(Feuille, "Valeur incorrecte")
        'On réaffiche l'ancienne valeur dans le controle
        Controle.Text = Donnée
        ControlerBornesMiniMaxi = True
      End Try


    End If
  End Function


  '**************************************************************************
  'EstNulleDate : retourn si une date n'a pas encore été initialisée(dans ce cas : 01/01/0001 à 00:00:00h)
  ' Rem AV : En attendant de trouver mieux
  '**************************************************************************
  Public Function EstNulleDate(ByVal uneDate As Date) As Boolean
    If uneDate.CompareTo(CDate("01/01/0001")) = 0 Then EstNulleDate = True
  End Function

  Public Sub AfficherMessageErreur(ByVal Feuille As Form, ByVal ex As System.Exception)
    Dim Message As String
    If cndDéboguage Then
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
