Option Strict Off
Option Explicit On
Imports System.Math
Imports System.IO

Module LecDXF
  '******************************************************************************
  '																																							'
  '						Projet DIAGFEUX : programmation des carrefours à feux								'
  '						Maitrise d'ouvrage : CERTU																				'	
  '						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
  '						Auteur : André VIGNAUD																						'
  '																																							'
  '						Source : LecDXF.vb																								'
  '						Module de lecture d'un fichier DXF
  '																																							'
  '******************************************************************************

  Private CODE As Short
  Private Binaire As Boolean
  Private chaine As String
  Private entier As Short
  Private reel As Double
  Private CodeGroupe3, CodeGroupe2, CodeGroupe, NomFich, ValeurGroupe, ValeurGroupe2, ValeurGroupe3 As String
  Private num_ligne As Integer
  Private strRead As StreamReader
  Private binRead As BinaryReader
  Private nomCourtVéhicule As String
  Private IndiceVéhicule() As Short

  Private UnPourCent As Single
  Private PourCentEnCours As Short

  Private DéfileDXF As frmDéfileDXF
  Private Graph As SuperBloc
  Private mCalques As CalqueCollection
  Private mBlocs As BlocCollection
  Private echDXF As Double
  Private pMinFDP As PointF
  Private pMaxFDP As PointF

  Public Enum TypeDonnBinaire
    typeChaine
    typeCoord
    typeDouble
    typeEtendueBinaire
    typeEntier
    typeLong
    typeBooléen

  End Enum

  Private dwgVersion As Short

  Public tCouleur(255) As Integer
  Public Const ID_Fichier As String = "fichier"
  Public Const ID_Lecture As String = "Lecture"
  Public Const ID_LectureFichier As String = ID_Lecture & " - " & ID_Fichier & " "
  Public Const ID_RechercheLimites As String = "Recherche des limites..."
  Public Const ID_LIGNE As String = "ligne"
  Public Const IDm_Absent As String = " non trouvé"                   ' blanc initial important
  Public Const IDm_Erreur As String = "Erreur"
  Public Const IDm_ErrFatale As String = "Erreur fatale n° "
  Public Const IDm_Anomalie As String = "Anomalie"
  Public Const IDm_Err101 As String = "Aucun élément interprétable par GIRATION n'a été trouvé dans "
  Public Const IDm_Err103 As String = ID_Fichier & IDm_Absent
  Public Const ID_FDP As String = "Fond de plan"                       ' Imprime (as string= frmTrajPar.fraFDP)
  Public Const IDm_FDPRefusé As String = ID_FDP & " non chargé"
  Public Const IDm_FinPrematuree As String = "Fin prématurée atteinte"
  Public Const ID_DXFVersion As String = "Version DXF"
  Public Const ID_NonGeree As String = " non gérée par " ' blancs encadrant essentiels
  Public Const ID_Plan As String = "Calque"
  Public Const IDm_AbsentTablePlan As String = " absent de la table des couches"
  Public Const IDm_EntiteSansPlan As String = "Pas de couche définie pour cette entité"
  Public Const IDm_UnSeulPointPline As String = "Une polyligne doit comporter au moins 2 points"
  Public Const ID_Code As String = "Code"
  Public Const ID_Attendu As String = "attendu"
  Public Const ID_NombreEntier As String = "nombre entier"
  Public Const IDm_Incorrect As String = "incorrect"
  Public Const IDm_DXFIncorrect As String = ID_Code & " DXF " & IDm_Incorrect

  '**************************************************************************************
  ' Lecture dun fichier Fond de plan au format DXF
  ' FDP : nom du Fichier
  ' Cancel : Retourne Vrai en cas d'erreur
  '**************************************************************************************
  Public Function lecFDP(ByVal FDP As String, Optional ByVal FeuilleFDP As dlgFDP = Nothing, Optional ByVal AfficherFenêtres As Boolean = True) As DXF
    'Dim p As PointF ' Valeur fictive (0,) pour appel le lire_entite
    Dim PremièreLigne As String
    Dim unDXF As DXF
    Dim fs As FileStream

    Binaire = False
    NomFich = FDP
    echDXF = 1
    dwgVersion = 12

    Try

      If AfficherFenêtres Then
        DéfileDXF = New frmDéfileDXF
        '============= Panneau de défilement  =================
        With DéfileDXF
          .Owner = FeuilleFDP
          .Show()
          .tmrDéfile.Enabled = True
          .txtPanneau.Text = ID_LectureFichier & " " & Trim(NomFich) & "..." & ID_LectureFichier & Trim(NomFich) & "..."
          UnPourCent = FileLen(NomFich) / 100
          PourCentEnCours = 0
          .lblPourCent.Text = "0%"
          .Activate()
        End With
        mdiApplication.Cursor = Cursors.WaitCursor 'Sablier
      End If

      '============= Détermination du type ASCII/Binaire =================
      num_ligne = -1
      fs = New FileStream(NomFich, FileMode.Open, FileAccess.Read, FileShare.Read, 32768)
      strRead = New StreamReader(fs)
      'strRead = New StreamReader(NomFich, System.Text.Encoding.UTF8, False, 32767)
      PremièreLigne = strRead.ReadLine

      Binaire = (PremièreLigne = "AutoCAD Binary DXF")

      fs.Close()
      fs = New FileStream(NomFich, FileMode.Open, FileAccess.Read, FileShare.Read, 32768)
      If Binaire Then
        Dim octet As Integer
        binRead = New BinaryReader(fs)
        'Détecter si le code groupe est sur 1 ou 2 octets : si sur 2 octets, les octets 22 et 23 valent tous deux 0
        fs.Seek(23, SeekOrigin.Begin)
        octet = binRead.PeekChar
        If octet = 0 Then dwgVersion = 14 ' code groupe sur 2 octets
        fs.Seek(22, SeekOrigin.Begin) ' Se positionner au début du 1er code groupe
      Else
        strRead = New StreamReader(fs)
      End If

      lire_code(0)
      lire_chaine("SECTION")
      lire_code(2)
      '        lire_chaine "HEADER", "TABLES", "ENTITIES"  Modif : v14
      lire_chaine("HEADER", "CLASSES", "TABLES", "ENTITIES")

      '============= Section HEADER =================
      If chaine = "HEADER" Then ' Certains fichiers DXF n'ont pas de section HEADER - c'est bizarre, mais AutoCAD l'accepte
        lire_code(9)
        While (CODE = 9)
          lire_header()
        End While
        lire_chaine("ENDSEC")
        lire_code(0)
        lire_chaine("SECTION")
        lire_code(2)
        '          lire_chaine "TABLES"    Modif : v14
        lire_chaine("CLASSES", "TABLES", "ENTITIES")
      End If ' chaine = "HEADER"

      '============= Section CLASSES =================
      If chaine = "CLASSES" Then ' Fichier DXF  v14 : section CLASSES
        lire_code(0)
        lire_chaine("CLASS", "ENDSEC")
        While chaine = "CLASS"
          lire_classe()
          lire_chaine("CLASS", "ENDSEC")
        End While
        lire_code(0)
        lire_chaine("SECTION")
        lire_code(2)
        lire_chaine("TABLES", "ENTITIES")
      End If

      '=== Initialiser le DXF
      unDXF = New DXF(FDP, Echelle:=1)
      Graph = unDXF.GraphFDP
      mCalques = unDXF.Calques
      mBlocs = unDXF.Blocs
      ' Cette réinitialisation n'aua pas lieu d'être si on souhaite la superposition de FDP
      reinFDP()

      ' Au cas, peu probable, où le plan 0 serait absent de la section TABLES (ou pas de section TABLES)
      AjouterCalque(New Calque("0"))
      unDXF.Insert.DéfinirAttributs(mCalques("0"), 256, "BYBLOCK")

      '============= Section TABLES =================
      If chaine = "TABLES" Then ' Sinon ce fichier DXF n'a qu'1 section ENTITIES
        lire_code(0)
        lire_chaine("TABLE", "ENDSEC")
        While chaine = "TABLE"
          lire_table()
          lire_chaine("TABLE", "ENDSEC")
        End While

        If AfficherFenêtres Then
          ' Choix des couches à conserver
          With DéfileDXF
            .Hide()
            .tmrDéfile.Enabled = False
            If InitWindowImport(FeuilleFDP) Then
              Throw New DiagFeux.ErreurSansMessage
            End If
            .tmrDéfile.Enabled = True
            .Show()
            .Activate()
          End With
        End If

        pMinFDP.X *= echDXF
        pMinFDP.Y *= echDXF
        pMaxFDP.X *= echDXF
        pMaxFDP.Y *= echDXF

        lire_code(0)
        lire_chaine("SECTION")
        lire_code(2)
        lire_chaine("BLOCKS", "ENTITIES")

        '============= Section BLOCKS =================
        If chaine = "BLOCKS" Then ' Certains fichiers DXF (PISTE,TPL) n'ont pas de section BLOCKS - c'est bizarre, mais AutoCAD l'accepte
          lire_code(0)
          lire_chaine("BLOCK", "ENDSEC")
          While chaine = "BLOCK"
            lire_bloc()
            lire_chaine("BLOCK", "ENDSEC")
          End While

          lire_code(0)
          lire_chaine("SECTION")
          lire_code(2)
          lire_chaine("ENTITIES")
        End If ' chaine = "BLOCKS"

      Else ' Le fichier ne contient qu'1 section ENTITIES
      End If ' chaine =  "TABLES"

      '============= Section ENTITIES =================
      lire_code(0)
      lire_chaine("", "ENDSEC")
      Do While chaine <> "ENDSEC"
        lire_entite(chaine, Nothing)
        lire_chaine("", "ENDSEC")
      Loop

      lire_code(0)
      '        lire_chaine "EOF"         ' modif V14
      lire_chaine("SECTION", "EOF")

      '============= Section OBJECTS =================
      If chaine = "SECTION" Then
        lire_code(2)
        lire_chaine("OBJECTS")
        lire_code(0)
        lire_chaine("", "ENDSEC")

        'Mise en commentaire (AV : 25/06/03) des lignes qui suivent
        'GIRATION n'utilise pas la section OBJECTS : inutile de les lire
        'While chaine <> "ENDSEC"
        '  lire_objet()
        '  lire_chaine("", "ENDSEC")
        'End While

        ''============= Section EOF =================
        'lire_code(0)
        'lire_chaine("EOF")

      End If

      If AfficherFenêtres Then
        With DéfileDXF
          .tmrDéfile.Enabled = False
          .btnAnnuler.Enabled = False
          .txtPanneau.Text = ID_RechercheLimites
        End With
      End If

      '=============   ' traitement final pour le calcul de Zoom =================

      If pMinFDP.X = 0 And pMinFDP.Y = 0 And pMaxFDP.X = 0 And pMaxFDP.Y = 0 Then
        ' Pas de section HEADER
        Wpmin = New PointF(1000000000.0#, 1000000000.0#)
        Wpmax = New PointF(-1000000000.0#, -1000000000.0#)
        calminmax(pInsert:=New PointF(0, 0))
        pMinFDP.X = Wpmin.X
        pMinFDP.Y = Wpmin.Y
        pMaxFDP.X = Wpmax.X
        pMaxFDP.Y = Wpmax.Y
      Else
        Wpmin.X = pMinFDP.X
        Wpmin.Y = pMinFDP.Y
        Wpmax.X = pMaxFDP.X
        Wpmax.Y = pMaxFDP.Y
      End If

      unDXF.AffecterPminPmax()

      ' Epurer les calques inutiles
      Dim unCalque As Calque
      Dim i As Short
      For i = mCalques.Count - 1 To 0 Step -1
        unCalque = mCalques(i)
        If unCalque.Gele Then mCalques.Remove(unCalque)
      Next

      'Retourner l'objet DXF créé
      lecFDP = unDXF

    Catch ex As DiagFeux.ErreurSansMessage
      ' déclenché par le programme dans FinRapide (Une erreur + explicite a été fournie à l'utilisateur)
      ' ou si l'utilisateur a fait Escape dans frmDéfileDXF ou Annuler dans la dlgImportDXF
      TraitementErreur()
    Catch ex As DiagFeux.Exception
      TraitementErreur(ex)
    Catch ex As System.Exception
      TraitementErreur(ex)

    Finally
      fs.Close()

      If AfficherFenêtres Then
        With DéfileDXF
          .Close()
          .Dispose()
        End With
      End If
      mdiApplication.Cursor = System.Windows.Forms.Cursors.Default
    End Try


  End Function

  Private Sub TraitementErreur(Optional ByVal ex As System.Exception = Nothing)

    If Not IsNothing(ex) Then
      AfficherMessageErreur(Nothing, ex)
    End If

    pminFDP.X = 0
    pminFDP.Y = 0
    pmaxFDP.X = 0
    pmaxFDP.Y = 0

    If Not IsNothing(Graph) Then reinFDP() '  idem

  End Sub

  Private Function ComplémentMessage(ByVal Procédure As String) As String
    If Binaire Then
      ComplémentMessage = vbCrLf & Procédure & vbCrLf & ID_Fichier & " " & NomFich
    Else
      ComplémentMessage = vbCrLf & Procédure & vbCrLf & ID_LIGNE & " " & CStr(num_ligne) & " - " & ID_Fichier & " " & NomFich
    End If

  End Function

  Private Function InitWindowImport(ByVal FeuilleFDP As dlgFDP) As Boolean
    Dim dlg As New dlgImportDXF


    With dlg
      .Calques = mCalques
      InitWindowImport = (.ShowDialog(FeuilleFDP) = DialogResult.Cancel)
      echDXF = CDbl(.txtEchelle.Text)
      .Dispose()
    End With

  End Function

  '*******************************************
  '* lire_objet : lecture d'un objet si DXF 14
  '*******************************************
  ' Ne fait rien

  Private Sub lire_objet()

    lire_code(-1)

    While CODE <> 0
      lire_chaine("")
      lire_code(-1)
    End While

  End Sub

  '*******************************************
  '* lire_clsse : lecture d'une classe si DXF 14
  '*******************************************
  ' Ne fait rien

  Private Sub lire_classe()

    lire_code(1)
    While CODE <> 0
      lire_chaine("")
      lire_code(-1)
    End While

  End Sub


  Private Sub ErreurFDP(ByVal mess As String, Optional ByVal NonBloquant As Boolean = False)

    Dim mess_err As String

    If Err.Number = 101 Then
      mess_err = IDm_Err101 & " " & NomFich
    ElseIf Err.Number = 103 Then
      mess_err = IDm_Err103
    Else
      If Binaire Then
        mess_err = mess & vbCrLf & ID_Fichier & " " & NomFich
      Else
        mess_err = mess & vbCrLf & ID_LIGNE & " " & CStr(num_ligne) & " - " & ID_Fichier & " " & NomFich
      End If

      If NonBloquant Then
        mess_err = IDm_Anomalie & " : " & mess_err
      Else
        mess_err = IDm_Erreur & " : " & mess_err
      End If
    End If

    If NonBloquant Then
      mess_err = mess_err & vbCrLf & IDm_FDPRefusé
      MessageBox.Show(mess_err, ID_LectureFichier & "DXF", MessageBoxButtons.OK, MessageBoxIcon.Stop)
      Throw New DiagFeux.ErreurSansMessage
    Else
      MessageBox.Show(mess_err, ID_LectureFichier & "DXF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Throw New DiagFeux.ErreurSansMessage
    End If

  End Sub

  Private Sub lire_header()
    Dim flag(1) As Boolean

    Try
      lire_chaine("")  '               // nom de la variable AutoCad
      lire_code(-1)  '               // lecture du 1er groupe pour la variable

      Select Case chaine
        Case "$ACADVER"
          Select Case ValeurGroupe
            Case "AC1009"
              dwgVersion = 12     ' v11 aussi
            Case "AC1012"
              dwgVersion = 13
            Case "AC1014"
              dwgVersion = 14     ' bien que la doc de la v14 dise "AC1013" - mais c'est a priori "AC1014" (cf Aide Autocad 2000)
            Case "AC1015"
              dwgVersion = 15 ' AutoCAD 2000, 2000i(16), 2002(17)
              'Versions suivantes ajoutée à partir du Proto 13
            Case "AC1018"
              dwgVersion = 18  ' Autocad 2004,5(19),6(20)
            Case "AC1021"     ' AutoCAD 2007
              dwgVersion = 21
            Case "AC1006"
              ErreurFDP(ID_DXFVersion & CStr(10) & ID_NonGeree & System.Reflection.Assembly.GetExecutingAssembly.GetName.Name)
            Case Else
              ErreurFDP(ID_DXFVersion & ID_NonGeree & System.Reflection.Assembly.GetExecutingAssembly.GetName.Name)
          End Select
          lire_code(-1)

          ' Recherche des limites utiles du dessin
        Case "$EXTMIN"
          While Not (flag(0) And flag(1))
            If CODE = 10 Then
              If Val(ValeurGroupe) <> 1.0E+20 Then     ' version Site pilote
                pminFDP.X = Val(ValeurGroupe)
              End If
              flag(0) = True
            ElseIf CODE = 20 Then
              If Val(ValeurGroupe) <> 1.0E+20 Then     ' version Site pilote
                pminFDP.Y = Val(ValeurGroupe)
              End If
              flag(1) = True
            End If
            lire_code(-1)    '                 lecture du groupe suivant
          End While

        Case "$EXTMAX"
          While Not (flag(0) And flag(1))
            If CODE = 10 Then
              If Val(ValeurGroupe) <> -1.0E+20 Then     ' version Site pilote
                pMaxFDP.X = Val(ValeurGroupe)
              End If
              flag(0) = True
            ElseIf CODE = 20 Then
              If Val(ValeurGroupe) <> -1.0E+20 Then     ' version Site pilote
                pMaxFDP.Y = Val(ValeurGroupe)
              End If
              flag(1) = True
            End If
            lire_code(-1)    '                 lecture du groupe suivant
          End While

      End Select

      While (CODE <> 9 And CODE <> 0)
        lire_code(-1)
      End While

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "DXF.lire_header")
    End Try

  End Sub

  Private Sub lire_table()
    Dim GroupeTable As String, nomTable As String
    Dim uneCouleur As Integer
    Dim Typelign As String
    Dim Gelé As Boolean

    lire_code(2)
    lire_chaine("") ' // nom du groupe de tables
    GroupeTable = chaine
    '<===AV : 14/01/2004
    'If dwgVersion <> 12 Then ..........
    'A partir de la v3.2.0032(01/2004), la gestion des codes spécifiques v14,2000... est déportée dans lire_code
    'Le dernier source est disponible dans LexDXFv32.Bas (sous vSpline)
    '===>AV
    lire_code(70, 0)
    If CODE = 70 Then
      If dwgVersion >= 15 And GroupeTable = "DIMSTYLE" Then
        lire_code(71)
      End If
      lire_code(0)
    End If

    Do While True
      lire_chaine(GroupeTable, "ENDTAB")
      If chaine = "ENDTAB" Then   ' moins de tables que nbTables
        lire_code(0)
        Exit Do
      Else
        lire_code(2)
        lire_chaine("")   '       // nom utilisateur de la table
        nomTable = chaine
        If GroupeTable = "LAYER" Then
          uneCouleur = 7
          Typelign = "CONTINUOUS"
          Gelé = False
        End If
        lire_code(-1)
        While (CODE <> 0)
          If GroupeTable = "LAYER" Then
            If CODE = 70 Then
              Gelé = Gelé Or BitActif(Val(ValeurGroupe), 1)
            ElseIf CODE = 62 Then
              uneCouleur = Val(ValeurGroupe)
              Gelé = Gelé Or (Val(ValeurGroupe) < 0) ' plan inactif dans le DXF
            ElseIf CODE = 6 Then
              Typelign = ValeurGroupe
            End If
          End If
          lire_code(-1)
        End While
        If GroupeTable = "LAYER" Then
          AjouterCalque(New Calque(nomTable, uneCouleur, Typelign), Gelé)
        End If
      End If
    Loop

    If chaine <> "ENDTAB" Then    ' Il n'y a pas eu de sortie anticipée dans la boucle de lecture
      lire_chaine("ENDTAB")
      lire_code(0)
    End If

  End Sub

  Private Sub AjouterCalque(ByVal unCalque As Calque, Optional ByVal Gelé As Boolean = False)

    If Not mCalques.Contains(unCalque.Nom) Then
      mCalques.Add(unCalque)
    End If
    unCalque.Visible = Not Gelé
  End Sub

  Private Sub lire_bloc()
    Dim nomBlocUser As String
    Dim p As New PointF
    Dim BlocOK As Boolean
    Dim Valeur As Short
    Dim objetBloc As Bloc

    Try
      lire_code(-1)

      While (CODE <> 0)  '            // en-tete du bloc jusqu'à la première entité
        Select Case CODE
          Case 3, 8, 62, 30
          Case 2
            nomBlocUser = ValeurGroupe
          Case 10
            p.X = Val(ValeurGroupe)
          Case 20
            p.Y = Val(ValeurGroupe)

          Case 70
            Valeur = Val(ValeurGroupe)
            If Valeur = 64 Or Valeur = 66 Then
              BlocOK = True
            ElseIf Valeur = 0 Or Valeur = 2 Then
              ' Ajout AV : 07/01/2002 - ces blocs sont des blocs réservés AutoCAD pour l'espace papier et l'espace objet
              If InStr(1, nomBlocUser, "*Paper_Space", vbTextCompare) = 1 Or _
                    UCase(nomBlocUser) = "*MODEL_SPACE" Then
                BlocOK = False
              Else  ' curieusement, les fichiers MOSS mettent un indicateur 0 au lieu de 64 pour les blocs référencés et AutoCAD les lit quand même
                BlocOK = True
              End If
            Else  '         // bloc non classique (xref, ou dépendant d'xref, ou anonyme) ou non référencé
              BlocOK = False
            End If

            If Not BlocOK Then
              Do
                lire_code(-1)
                lire_chaine("")
              Loop While chaine <> "ENDBLK"
            End If
        End Select

        lire_code(-1)
      End While

      If BlocOK Then '         // bloc non ignoré
        objetBloc = mBlocs.Add(New Bloc(nomBlocUser))

        'Ajouter toutes les entités composant le bloc
        lire_chaine("", "ENDBLK")
        While chaine <> "ENDBLK"
          lire_entite(chaine, p, objetBloc)
          lire_chaine("", "ENDSEC")
        End While
        lire_code(-1)

        'Bloc vide
        If objetBloc.Count = 0 Then
          mBlocs.Remove(objetBloc)
        End If

      End If

      While (CODE <> 0)  '     // contrairt à la doc d'AutoDESK, ENDBLK peut avoir des codes de groupe (plan, maintien....autres???)
        lire_chaine("")
        lire_code(-1)
      End While

    Catch ex As DiagFeux.ErreurSansMessage
      Throw New DiagFeux.ErreurSansMessage
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LecDXF.lire_bloc"))
    End Try
  End Sub

  '******************************************************************************
  ' Lecture d'une entité élémentaire ou incluse dans un bloc
  '******************************************************************************
  Private Sub lire_entite(ByVal nomEntite As String, ByVal pBloc As PointF, Optional ByVal objetBloc As Bloc = Nothing)
    Dim interessant As Boolean
    Dim nomCalque As String
    Dim uneCouleur As Integer
    Dim CodeBinaire As Short
    Dim p0, p1, p2, p3 As PointF
    Dim nomBloc, Typelign As String
    Dim echx, echy, rot, distcol, distrow As Double
    Dim nbcol, nbrow As Short
    Dim Rayon, Alpha(1) As Double
    Dim arrondi() As Double
    Dim Drapeau As Short
    Dim Sommets As Pts
    Dim pLoop As Pt
    Dim pACréer As Boolean
    Dim unCalque As Calque
    Dim numVertex, nbVertex As Short
    ' SPLINE
    Dim nbNoeuds As Integer, numNoeud As Integer
    Dim mSpLine As Spline
    Dim mPolylines As PolyArcCollection

    Dim unGraphique As Graphique

    Static pLine As PolyArc

    Static flagArrondi As Boolean

    Try
      Select Case nomEntite
        Case "POINT", "LINE", "ARC", "CIRCLE", "INSERT"
          interessant = True
          If nomEntite = "INSERT" Then
            echx = 1 : echy = 1 : nbcol = 1 : nbrow = 1
          End If
        Case "POLYLINE", "LWPOLYLINE", "SOLID"
          interessant = True
          pLine = New PolyArc(Autocadien:=True)
        Case "VERTEX", "SEQEND"
          interessant = Not pLine Is Nothing
          ReDim arrondi(0)
        Case "SPLINE"
          interessant = True
          mSpLine = New Spline
      End Select

      uneCouleur = 256  'par défaut: uneCouleur DUPLAN
      Typelign = "BYLAYER"  'par défaut: type de ligne DUPLAN

      lire_code(-1)

      Do While (CODE <> 0)
        If interessant Then

          Select Case CODE
            Case 30, 31, 66 ' infos ignorées même pour les entités intéressantes
            Case 43
              If nomEntite = "SPLINE" Then mSpLine.tolPControle = Val(ValeurGroupe)
            Case Is > 997    ' Données étendues ignorées
            Case 8
              nomCalque = ValeurGroupe
              If Not mCalques.Contains(nomCalque) Then
                If nomCalque = "DEFPOINTS" Then
                  'Ajout AV (19/06/03 :suite à pb sous ACAD2000   (ECRDXF) : Points de définition des cotes de toute façon ignoré de GIRATION
                  interessant = False
                Else
                  ErreurFDP(ID_Plan & " " & nomCalque & IDm_AbsentTablePlan, NonBloquant:=True)
                  ' AutoCAD crée le plan s'il est absent : GIRATION aussi
                  AjouterCalque(New Calque(nomCalque))
                End If
              Else
                If mCalques(nomCalque).Gele Then interessant = False
              End If
              unCalque = mCalques(nomCalque)
            Case 62
              uneCouleur = Val(ValeurGroupe)
            Case 6
              Typelign = ValeurGroupe
            Case 70    'insert,polyline,vertex
              Select Case nomEntite
                Case "INSERT"
                  nbcol = Val(ValeurGroupe)
                Case "LWPOLYLINE"
                  CodeBinaire = Val(ValeurGroupe)
                  pLine.Fermé = BitActif(CodeBinaire, 1)
                Case "POLYLINE"
                  CodeBinaire = Val(ValeurGroupe)
                  If CodeExcluPoly(CodeBinaire) Then
                    interessant = False
                  Else
                    CodeBinaire = Val(ValeurGroupe)
                    pLine.Fermé = BitActif(CodeBinaire, 1)
                  End If
                Case "VERTEX"
                  CodeBinaire = Val(ValeurGroupe)
                  If CodeExcluSommet(CodeBinaire) Then interessant = False
                  'Oter le Drapeau P3D pour ne pas le réécrire lors de l'export (GIRATION lit mais ignore le Z du 3D)
                  If BitActif(CodeBinaire, 32) = 32 Then
                    Drapeau = CodeBinaire - 32
                  Else
                    Drapeau = CodeBinaire
                  End If
                Case "SPLINE"
                  CodeBinaire = Val(ValeurGroupe)
                  'Vérifier si la spline est rationnelle
                  mSpLine.Rational = BitActif(CodeBinaire, 4)

                  'Exclure les splines périodiques
                  If BitActif(CodeBinaire, 2) Then
                    interessant = False
                    mSpLine = Nothing
                  Else
                    'Information inutilisée par  DIAGFEUX
                    mSpLine.Fermé = BitActif(CodeBinaire, 1)
                  End If
              End Select
            Case 10    ' point,circle,arc,insert,polyline,line,vertex,solid
              If nomEntite = "LWPOLYLINE" Or nomEntite = "SPLINE" Then
                If pACréer Then Sommets.Add(pLoop)
                pLoop = New Pt(New PointF(Val(ValeurGroupe) * echDXF - pBloc.X, 0.0#))
              ElseIf nomEntite <> "POLYLINE" Then
                p0.X = Val(ValeurGroupe) * echDXF - pBloc.X
              End If
            Case 20    ' point,circle,arc,insert,polyline,line,vertex,solid
              If nomEntite = "LWPOLYLINE" Or nomEntite = "SPLINE" Then
                pLoop.p.Y = Val(ValeurGroupe) * echDXF - pBloc.Y
                Dim NumSommet As Short = Sommets.Count - 1
                If NumSommet >= 0 AndAlso pLoop.p.Equals(Sommets(NumSommet).p) Then
                  '  sommets confondus : refuser de créer le segment (ou pire l'arc)
                  nbVertex -= 1
                  pLoop = Sommets(NumSommet)
                  Sommets.RemoveAt(NumSommet)
                  pLoop.Arrondi = 0.0
                  Sommets.Add(pLoop)
                  pACréer = False
                Else
                  pACréer = True
                End If
              ElseIf nomEntite <> "POLYLINE" Then
                p0.Y = Val(ValeurGroupe) * echDXF - pBloc.Y
              End If
            Case 11    ' line solid
              p1.X = Val(ValeurGroupe) * echDXF - pBloc.X
            Case 21    ' line solid
              p1.Y = Val(ValeurGroupe) * echDXF - pBloc.Y
            Case 12    ' solid
              p2.X = Val(ValeurGroupe) * echDXF - pBloc.X
            Case 22    ' solid
              p2.Y = Val(ValeurGroupe) * echDXF - pBloc.Y
            Case 13    ' solid
              p3.X = Val(ValeurGroupe) * echDXF - pBloc.X
            Case 23    ' solid
              p3.Y = Val(ValeurGroupe) * echDXF - pBloc.Y

            Case 40    'circle,arc,polyline,vertex
              If nomEntite = "ARC" Or nomEntite = "CIRCLE" Then Rayon = Val(ValeurGroupe) * echDXF
              If nomEntite = "SPLINE" Then
                mSpLine.Noeuds(numNoeud) = Val(ValeurGroupe)
                numNoeud += 1
              End If
            Case 41    ' insert,polyline,vertex,spline
              If nomEntite = "INSERT" Then echx = Val(ValeurGroupe)
              If nomEntite = "SPLINE" Then pLoop.Arrondi = Val(ValeurGroupe)
            Case 42    ' insert,lwpolyline,vertex,spline
              If nomEntite = "INSERT" Then
                echy = Val(ValeurGroupe)
                If echy <> echx Then interessant = False
              ElseIf nomEntite = "VERTEX" Then
                arrondi(0) = Val(ValeurGroupe)
              ElseIf nomEntite = "LWPOLYLINE" Then
                pLoop.Arrondi = Val(ValeurGroupe)
              ElseIf nomEntite = "SPLINE" Then
                mSpLine.tolNoeuds = Val(ValeurGroupe)
              End If
            Case 44    'insert
              distcol = Val(ValeurGroupe) * echDXF
            Case 45    'insert
              distrow = Val(ValeurGroupe) * echDXF
            Case 50    'arc,insert (+ vertex : inutilisé dans la présente version de  DIAGFEUX)
              If nomEntite = "INSERT" Then
                rot = borne360(ValeurGroupe)
              Else
                Alpha(0) = borne360(ValeurGroupe)
                'Alpha(0) = Val(ValeurGroupe)
              End If
            Case 51    ' ARC
              Alpha(1) = borne360(ValeurGroupe)
              'Alpha(1) = Val(ValeurGroupe)
            Case 2    'INSERT
              nomBloc = ValeurGroupe
              If nomEntite = "INSERT" Then
                If Not mBlocs.Contains(nomBloc) Then interessant = False
              End If
            Case 67
              If Val(ValeurGroupe) = 1 Then interessant = False ' espace papier
            Case 71    'insert,polyline
              If nomEntite = "INSERT" Then nbrow = Val(ValeurGroupe)
              If nomEntite = "SPLINE" Then mSpLine.Ordre = Val(ValeurGroupe) + 1
            Case 72 ' SPLINE
              nbNoeuds = Val(ValeurGroupe)
            Case 73 ' SPLINE
              nbVertex = Val(ValeurGroupe)
              Sommets = New Pts
            Case 75    'polyline lissée
              If nomEntite = "POLYLINE" Then
                pLine.TypeLissage = Val(ValeurGroupe)
              End If
            Case 90    ' lwpolyline
              If nomEntite = "LWPOLYLINE" Then
                nbVertex = Val(ValeurGroupe)
                Sommets = New Pts
              End If
          End Select

        End If   ' interessant

        'Même si ce n'est pas intéressant, on termine la boucle de lecture de l'entité
        lire_code(-1)
      Loop

      If nomEntite = "LINE" AndAlso p0.Equals(p1) Then interessant = False

      If echx <> echy Then interessant = False

      If interessant Then

        If nomCalque = "" And nomEntite <> "SEQEND" Then
          ErreurFDP(IDm_EntiteSansPlan)
        End If

        If objetBloc Is Nothing Then
          objetBloc = Graph
        End If

        With objetBloc
          Select Case nomEntite
            Case "POINT"
              unGraphique = .Points.Add(New ACADPoint(p0))
            Case "LINE"
              unGraphique = .Lignes.Add(New Ligne(p0, p1))
            Case "LWPOLYLINE", "POLYLINE"
              unGraphique = .PolyArcs.Add(pLine)
            Case "SPLINE"
              unGraphique = .Splines.Add(mSpLine)
            Case "CIRCLE"
              unGraphique = .Cercles.Add(New Cercle(p0, Rayon))
            Case "ARC"
              'Drawing.Ellipse dessine dans le sens horaire à la différence d'Autocad(inverser les angles)
              unGraphique = .Arcs.Add(New Arc(p0, Rayon, Alpha(1), AngleBalayageArc(Alpha(1), Alpha(0))))
            Case "INSERT"
              Dim unInsert As New Insert(mBlocs(nomBloc))
              With unInsert
                .pInsertion = p0
                .nbcol = nbcol
                .nbrow = nbrow
                .Echx = echx
                .rot = rot
                .distcol = distcol
                .distrow = distrow
              End With
              unGraphique = .Inserts.Add(unInsert)
            Case "SOLID"
              With pLine
                .PtsPoly.Add(New Pt(p0))
                .PtsPoly.Add(New Pt(p1))
                .PtsPoly.Add(New Pt(p3))
                If Not p2.Equals(p3) Then .PtsPoly.Add(New Pt(p2))
                .Fermé = True
              End With
              unGraphique = .PolyArcs.Add(pLine)
              pLine = Nothing
          End Select

          Select Case nomEntite
            Case "SEQEND"
              mPolylines = .PolyArcs
            Case "VERTEX"
            Case Else
              unGraphique.DéfinirAttributs(unCalque, uneCouleur, Typelign)
          End Select

        End With  ' objet Graph (DXF.GraphFDP)

        ' Traitement complémentaire pour les Polylignes (POLYLINE, VERTEX, SEQEND)
        If Not pLine Is Nothing Then
          Select Case nomEntite
            Case "POLYLINE", "LWPOLYLINE"
              With pLine
                If nomEntite = "LWPOLYLINE" Then
                  ' Ajouter le dernier sommet lu dans la boucle
                  If pACréer Then Sommets.Add(pLoop)
                  For numVertex = 0 To nbVertex - 1
                    If numVertex <> 0 Then
                      If Sommets(numVertex - 1).Arrondi <> 0 Then CalArc(pLine, Sommets(numVertex).p, unCalque, uneCouleur, Typelign)
                    End If
                    .PtsPoly.Add(Sommets(numVertex))
                  Next

                  If .Fermé Then
                    If Sommets(nbVertex - 1).Arrondi <> 0 Then
                      CalArc(pLine, Sommets(0).p, unCalque, uneCouleur, Typelign)
                    End If
                  End If
                  'Ajout AV (14/01/04 : v3.2.33) : pouvait faire planter ultérieurement un insert de bloc avec attribut(entité SEQEND à ignorer alors )
                  pLine = Nothing
                End If
              End With

            Case "VERTEX"
              If pLine.PtsPoly.Count > 0 AndAlso p0.Equals(pLine.PtsPoly(pLine.PtsPoly.Count - 1).p) Then
                '          flagArrondi = False
              Else
                If flagArrondi Then   ' création de l'Arc avec le pt précédent
                  CalArc(pLine, p0, unCalque, uneCouleur, Typelign)
                End If
                pLine.PtsPoly.Add(p0.X, p0.Y, arrondi(0), Drapeau)
                flagArrondi = arrondi(0) <> 0
              End If

              'Si on veut retracer la polyligne splinée, il faut également (re)mettre en commentaire : 'Coul = QBColor(tCouleur(Abs(Coul)))' dans CERCLE.Dessiner
              '          If (Drapeau And 16) <> 16 Then .Cercles.Add nomPlan, QBColor(12), Typelign, p0, 1
            Case "SEQEND"
              With mPolylines
                If pLine.PtsPoly.Count < 2 Then
                  ' on fait comme AutoCAD : on arrête le chargement (suite ignorée)
                  ' on ne fait plus comme AutoCAD : on ignore simplement la polyligne
                  'ErreurFDP(IDm_UnSeulPointPline)
                  .Remove(pLine)
                End If

                If pLine.Fermé And flagArrondi Then CalArc(pLine, pLine.PtsPoly(0).p, unCalque, uneCouleur, Typelign)
              End With  ' mPolylines

              flagArrondi = False
              pLine = Nothing
          End Select
        End If

        ' Traitement complémentaire pour les splines
        If Not mSpLine Is Nothing Then
          With mSpLine
            Sommets.Add(pLoop)
            For Each pLoop In Sommets
              .PtsControle.Add(pLoop)
            Next
            .Etablir(nbPoints:=50)
          End With
          mSpLine = Nothing
        End If

      ElseIf nomEntite = "POLYLINE" Or nomEntite = "LWPOLYLINE" Then
        pLine = Nothing
      End If   ' interessant

    Catch ex As DiagFeux.ErreurSansMessage
      Throw New DiagFeux.ErreurSansMessage
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LECDXF.Lire_entite"))
    End Try

  End Sub

  '******************************************************
  ' Recherche si la polyligne est exclue par GIRATION
  '******************************************************
  Private Function CodeExcluPoly(ByVal CodeBinaire As Short) As Boolean

    '  If BitActif(CodeBinaire, 2) Then
    ' Bit 1 : Courbe lissée
    '    CodeExcluPoly = True
    '
    '  ElseIf BitActif(CodeBinaire, 4) Then
    '' Bit 2 : Spline
    '    CodeExcluPoly = True

    If BitActif(CodeBinaire, 16) Then
      ' Bit 4 : Maillage 3D
      CodeExcluPoly = True

    ElseIf BitActif(CodeBinaire, 32) Then
      ' Bit 5 : Maille polygone fermée
      CodeExcluPoly = True

      ' Bit 6 : Maille polyface
    ElseIf BitActif(CodeBinaire, 64) Then
      CodeExcluPoly = True
    End If

  End Function

  '******************************************************
  ' Recherche si le sommet est exclu par GIRATION
  '******************************************************
  Private Function CodeExcluSommet(ByVal CodeBinaire As Short) As Boolean

    ' Bit 0 - Drapeau=1  - Sommet créé par lissage (polylige lissée, ou polyline splinée avec SPLINESEGS <0)
    ' Bit 2 - Drapeau=4  - Inutilisé
    ' Bit 3 - Drapeau=8  - Sommet spline créé par lissage (polyligne splinée uniquement)
    ' Bit 4 - Drapeau=16 - Point de controle de spline  (polyligne splinée uniquement)
    ' Bit 5 - Drapeau=32 - Sommet de polyligne 3D

    'If BitActif(CodeBinaire, 2) Then
    ' Bit 1 : Tangence à Courbe lissée !! Giration ne sait pas interpréter le code 50
    ' CodeExcluSommet = True

    'If BitActif(CodeBinaire, 16) Then
    ' Bit 4 : Point de controle - le sommet ne doit pas être dessiné,
    ' mais être conservé en mémoire pour réécriture : Voir Calarc et POLYLINE.PtsUtiles
    '  CodeExcluSommet = True

    If BitActif(CodeBinaire, 64) Then   ' ne doit jamais arriver vu que les polylignes correspondantes ont été exclues
      ' Bit 6 : Maillage 3D
      CodeExcluSommet = True

      ' Bit 7 : Maille polyface
    ElseIf BitActif(CodeBinaire, 128) Then   ' ne doit jamais arriver vu que les polylignes correspondantes ont été exclues
      CodeExcluSommet = True
    End If

  End Function


  '******************************************************
  ' Détermine si le bit vaut 0 ou 1 dans CodeBinaire
  '******************************************************
  Private Function BitActif(ByVal CodeBinaire As Short, ByVal NumBit As Short) As Boolean
    Dim ValeurBit As Short

    'ValeurBit = 2 ^ PositionBit  : utile si la position du bit est fournie plutôt que la valeur recherchée
    ValeurBit = NumBit

    BitActif = ((CodeBinaire And ValeurBit) = ValeurBit)

  End Function

  '******************************************************************************
  ' Décryptage du type de données selon son code (fichier DXF binaire)
  '******************************************************************************
  Private Sub DecrypSelonCode()
    Dim typdonn As TypeDonnBinaire
    Dim Msg As String
    Dim i As Short

    If Binaire Then

      Select Case CODE
        Case 0 To 9, 998 To 1009
          typdonn = TypeDonnBinaire.typeChaine
          If CODE = 1004 And Binaire Then typdonn = TypeDonnBinaire.typeEtendueBinaire 'données d'entités étendues binaires
        Case 10 To 59
          If CODE = 41 Or CODE = 42 Or CODE > 49 Then
            typdonn = TypeDonnBinaire.typeDouble
          Else
            typdonn = TypeDonnBinaire.typeCoord
          End If
        Case 60 To 79, 170 To 178, 1060 To 1079   ' 175-->Repoussé à 178 pour les textes (bug doc DXF) - AV - 14/01/99
          typdonn = TypeDonnBinaire.typeEntier
          If CODE = 1071 Then typdonn = TypeDonnBinaire.typeLong 'données d'entités étendues de type entier long
        Case 90 To 99   ' AutoCAD 14
          typdonn = TypeDonnBinaire.typeLong
        Case 100, 102, 105, 300 To 369      ' AutoCAD 14
          typdonn = TypeDonnBinaire.typeChaine
          If Binaire And CODE > 309 And CODE < 320 Then
            typdonn = TypeDonnBinaire.typeEtendueBinaire
          End If
        Case 110, 111, 112, 120, 121, 122, 130, 131, 132  ' non documenté dans ref DXF 2000 mais trouvé dans un fichier du CERTU (26/09/03)
          typdonn = TypeDonnBinaire.typeCoord
        Case 390 To 399, 410 To 419          ' AutoCAD 2000
          typdonn = TypeDonnBinaire.typeChaine
        Case 270 To 275, 280 To 289     ' AutoCAD 14 - 270 à 275 non documenté mais présent dans DIMSYTLE
          typdonn = TypeDonnBinaire.typeEntier
        Case 290 To 299       ' AutoCAD 2000 (codes 290-299 rajoutés par AV le 26/09/03 : v 3.2.32 - Doc DXF incomplète et en + on n'indique pas dans l'annexe sur les fichiers binaires qu'un booléen est stocké sur un octet")
          typdonn = TypeDonnBinaire.typeBooléen
        Case 370 To 389, 400 To 409        ' AutoCAD 2000
          typdonn = TypeDonnBinaire.typeEntier
        Case 80 To 139, 148 To 169, 179 To 209, 240 To 997
          typdonn = False
        Case 140 To 147, 210 To 239, 1010 To 1059
          typdonn = TypeDonnBinaire.typeDouble
      End Select


      ''''If Not Binaire Then lire_ligne
      ''''''''''''''''''''''lire_ligne    ' ligne à effacer si précédente réactivée

      Select Case typdonn
        Case TypeDonnBinaire.typeBooléen
          lire_ligne("Booléen")
        Case TypeDonnBinaire.typeChaine
          lire_ligne("chaine")
        Case TypeDonnBinaire.typeEntier
          lire_ligne("entier")
        Case TypeDonnBinaire.typeLong
          lire_ligne("long")
        Case TypeDonnBinaire.typeDouble
          lire_ligne("double")
        Case TypeDonnBinaire.typeCoord   ' coordonnées de pt auxquelles on applique un facteur d'échelle(/ unités AutoCAD)
          lire_ligne("double")
        Case TypeDonnBinaire.typeEtendueBinaire    ' Données binaires dans un fichier DXF binaire(code 1004)
          lire_ligne("octet") ' le 1er octet contient le nombre d'octets à lire
          For i = 1 To Val(CodeGroupe) : lire_ligne("octet") : Next
        Case Else
          lire_ligne("chaine")
          Msg = ID_Code & " " & CStr(CODE) & " " & IDm_Incorrect
          ErreurFDP(Msg, NonBloquant:=True)
      End Select

    End If
  End Sub

  '******************************************************************************
  ' Lecture du code suivant
  '******************************************************************************
  Private Sub lire_code(ByVal controle As Short, Optional ByVal CodeFacultatif As Short = -2)
    ' controle représente la valeur du code attendu, -1 indique que le code peut être quelconque
    Static CoordXTraité As Boolean

    Try
      If Not Binaire And (CODE = 10 Or CODE = 11 Or CODE = 12 Or CODE = 13) And Not CoordXTraité Then
        CodeGroupe = CodeGroupe2
        ValeurGroupe = ValeurGroupe2
        CoordXTraité = True
      ElseIf CoordXTraité And (CODE = 20 Or CODE = 21 Or CODE = 22) Then
        CodeGroupe = CodeGroupe3  ' CodeGroupe3 contient le Z ou le code groupe qui suit si le point est un point 2D
        ValeurGroupe = ValeurGroupe3
      Else
        CoordXTraité = False
        If dwgVersion = 12 Then
          lire_ligne("octet")
        Else
          lire_ligne("CodeGroupe") ' Code sur 2 octets à partir de v13 (non documenté)
        End If
      End If

      CODE = Val(CodeGroupe)
      DecrypSelonCode() '(==Ajout)

      Select Case CODE
        'Ajout AV : 06/11/2000 - Code 102
      Case 102
          '  Données attachées à une entité : incluses entre 2 accolades {}
          '          If ValeurGroupe <> "}" Then
          ' Correction AV ! 22/11/2001 : L'objet XRECORD peut utiliser le code 102 pour une donnée applicative simple (donc pas d'accolades)
          '(==Suppression)  DecrypSelonCode
          If Left(ValeurGroupe, 1) = "{" Then 'Ex. "{ACAD_XDICTIONARY"  "{ACAD_REACTORS"
            'lire toutes les données jusqu'à retrouver l'accolade fermante
            Do
              lire_code(-1)
              '(==Suppression)      If CODE <> 102 Then DecrypSelonCode
            Loop Until CODE = 102
            ' Rappel récursif de lire_code pour traiter le cas qui nous intéressait au départ
            lire_code(controle, CodeFacultatif)
            ' Arrêt : La suite du traitement a été effectuée lors de l'appel récursif
          ElseIf Left(ValeurGroupe, 1) = "}" Then 'Ex. "{ACAD_XDICTIONARY"  "{ACAD_REACTORS"
            controle = -1
          End If

        Case 5, 105, 330, 100, 340
          ' code 340 ajouté (AV : 26/09/03) : présence inattendue d'un code 340 dans 1 fichier particulier
          lire_code(controle, CodeFacultatif)

      End Select

      If (controle <> -1) Then
        If CodeFacultatif = -2 Then
          If CODE <> controle Then
            ErreurFDP(IDm_DXFIncorrect & " (" & ID_Attendu & " : " & CStr(controle) & ")")
          End If
        ElseIf CODE <> controle And CODE <> CodeFacultatif Then
          ErreurFDP(IDm_DXFIncorrect & " (" & ID_Attendu & " : " & CStr(controle) & ")")
        End If
      End If

    Catch ex As DiagFeux.ErreurSansMessage
      Throw New DiagFeux.ErreurSansMessage
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LecDXF.lire_code"))
    End Try

  End Sub

  '******************************************************************************
  ' Lecture dune chaine
  '******************************************************************************
  Private Sub lire_chaine(ByVal controle As String, ByVal ParamArray arg() As String)

    Dim chaineOk As Boolean
    Dim i As Short
    Dim mess As String

    mess = controle
    chaine = ValeurGroupe

    Try
      If controle = chaine Then
        chaineOk = True
      ElseIf controle = "" Then
        chaineOk = True
      Else
        For i = 0 To arg.Length - 1
          mess = mess & " ou " & arg(i)
          If chaine = arg(i) Then
            chaineOk = True
            Exit For
          End If
        Next i
      End If

      If Not chaineOk Then
        mess = remplaceOuParVirgule(mess)
        ErreurFDP("'" & chaine & "' " & IDm_Incorrect & " (" & ID_Attendu & " : " & mess & ")")
      End If

    Catch ex As DiagFeux.ErreurSansMessage
      Throw New DiagFeux.ErreurSansMessage
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LecDXF.lire_chaine"))
    End Try

  End Sub

  '******************************************************************************
  ' Remplacement de tous les "ou" de l'énumération par une virgule, à l'exception du dernier
  '******************************************************************************
  Private Function remplaceOuParVirgule(ByRef s As String) As String
    Dim pos As Short

    remplaceOuParVirgule = s

    pos = InStr(s, " ou ")
    If pos <> 0 Then
      If InStr(pos + 4, s, " ou ") <> 0 Then   ' il y a encore un "ou" plus loin dans la chaine
        remplaceOuParVirgule = Left(s, pos - 1) & ", " & remplaceOuParVirgule(Mid(s, pos + 4))
      End If
    End If

  End Function

  '******************************************************************************
  ' Lire une ligne du fichier
  '******************************************************************************
  Private Sub lire_ligne(ByVal param As String)
    Dim entier As Short
    Dim l As Integer
    Dim dble As Double
    'En .NET un Char occupe 2 octets : il ne faut en lire qu'un seul ici
    'dim carac as Char 
    Dim carac As Byte
    Dim s As String
    Dim octet As Byte
    Dim PosEnCours As Integer

    '    If PourCentEnCours = 0 Then DéfileDXF.lblpourcent = "0%"

    Try
      If Binaire Then
        Select Case param
          Case "Booléen"
            'En .NET un booléen occupe 2 octets : il ne faut en lire qu'un seul ici
            octet = binRead.ReadByte()
          Case "CodeGroupe"    ' v13 et suivantes (non documenté) : code groupe sur 2 octets
            entier = binRead.ReadInt16
            CodeGroupe = CStr(entier)
          Case "octet"
            octet = binRead.ReadByte
            If dwgVersion = 12 And octet = 255 Then     ' données d'entités étendues  ( v12 uniquement, vu commentaire ci-dessus)
              lire_ligne("entier")
              CodeGroupe = ValeurGroupe
            Else
              CodeGroupe = CStr(octet)
            End If
          Case "entier"
            entier = binRead.ReadInt16
            ValeurGroupe = CStr(entier)
          Case "long"
            l = binRead.ReadInt32
            ValeurGroupe = CStr(l)
          Case "double"
            dble = binRead.ReadDouble
            ValeurGroupe = substPtDecimalRegional(CStr(dble), Regional:=False)
          Case "chaine"
            'En .NET un Char occupe 2 octets : il ne faut en lire qu'un seul ici
            carac = binRead.ReadByte
            While carac <> 0
              s &= Chr(carac)
              carac = binRead.ReadByte
            End While
            ValeurGroupe = s
        End Select   ' Case param
        PosEnCours = binRead.BaseStream.Position

      Else  ' ASCII
        CodeGroupe = Trim(strRead.ReadLine())
        num_ligne += 1
        If Not IsEntier(CodeGroupe) Then
          ErreurFDP(IDm_DXFIncorrect & " (" & ID_NombreEntier & " " & ID_Attendu & ")")
        End If
        If CodeGroupe = "10" Or CodeGroupe = "11" Or CodeGroupe = "12" Or CodeGroupe = "13" Then
          ' coordonnée X lue : on lit le Y et le Z qui en principe suivent juste après(si c'est un point 2D la lecture du 3ème Groupe sera gérée par lire_code)
          ValeurGroupe = strRead.ReadLine
          CodeGroupe2 = strRead.ReadLine
          ValeurGroupe2 = strRead.ReadLine
          CodeGroupe3 = strRead.ReadLine
          ValeurGroupe3 = strRead.ReadLine
          num_ligne = num_ligne + 5
        Else
          ValeurGroupe = strRead.ReadLine
          num_ligne += 1
        End If
        PosEnCours = strRead.BaseStream.Position()
      End If


      If Not IsNothing(DéfileDXF) Then
        ' Affichage du pourcentage suivant
        While PosEnCours >= (PourCentEnCours + 1) * UnPourCent  ' Then
          DéfileDXF.lblPourCent.Text = PourCentEnCours + 1 & "%"
          PourCentEnCours = PourCentEnCours + 1
          System.Windows.Forms.Application.DoEvents()
        End While

        If DéfileDXF.Annul Then
          Throw New DiagFeux.ErreurSansMessage
        End If
      End If

    Catch ex As DiagFeux.ErreurSansMessage
      Throw New DiagFeux.ErreurSansMessage
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LecDXF.lire_ligne"))
    End Try

  End Sub

  Public Sub minimax(ByVal ParamArray coord() As Double)
    Dim i, nbcoord As Short

    nbcoord = coord.Length
    For i = 1 To nbcoord Step 2
      With Wpmin
        If coord(i - 1) < .X Then .X = coord(i - 1)
        If coord(i) < .Y Then .Y = coord(i)
      End With
      With Wpmax
        If coord(i - 1) > .X Then .X = coord(i - 1)
        If coord(i) > .Y Then .Y = coord(i)
      End With
    Next

  End Sub

  Public Sub calminmax(ByVal pInsert As PointF, Optional ByVal objBloc As Bloc = Nothing)
    Dim Cpt As Short
    Dim xIns, yIns As Double
    Dim Angle As Double
    Dim PiEnDegrés As Double = 180

    Dim NewPInsert As PointF

    Dim colPoints As ACADPointCollection
    Dim colLines As LigneCollection
    Dim colPLines As PolyArcCollection
    Dim colArcs As ArcCollection
    Dim colCercles As CercleCollection
    Dim colInserts As InsertCollection

    Dim objPoint As ACADPoint
    Dim objLine As Ligne
    Dim objPLine As PolyArc
    Dim objArc As Arc
    '		Dim objCercle As CERCLE
    Dim objCercle As Cercle
    Dim objInsert As Insert

    If objBloc Is Nothing Then
      objBloc = Graph
    Else
      'Point d'insertion
      xIns = pInsert.X
      yIns = pInsert.Y
    End If

    With objBloc
      colPoints = .Points
      colLines = .Lignes
      colPLines = .PolyArcs
      colArcs = .Arcs
      colCercles = .Cercles
      colInserts = .Inserts
    End With


    For Each objPoint In colPoints
      With objPoint
        minimax(.p.X + xIns, .p.Y + yIns)
      End With
    Next objPoint

    For Each objLine In colLines
      With objLine
        minimax(.pAF.X + xIns, .pAF.Y + yIns, .pBF.X + xIns, .pBF.Y + yIns)
      End With
    Next objLine

    For Each objPLine In colPLines
      With objPLine
        For Cpt = 1 To .Points.Length
          minimax(.Points(Cpt).X + xIns, .Points(Cpt).Y + yIns)
        Next
      End With
    Next objPLine

    For Each objCercle In colCercles
      With objCercle
        minimax(.pOF.X - .Rayon + xIns, .pOF.Y - .Rayon + yIns, .pOF.X + .Rayon + xIns, .pOF.Y + .Rayon + yIns)
      End With
    Next objCercle

    Dim AngDep, AngFin As Single
    For Each objArc In colArcs
      With objArc
        'Autocad et Drawing.DrawEllipse tournent en sens inverse : inverser angdeb et angfin
        AngFin = borne360(.AngleDépart)
        AngDep = borne360(.AngleDépart + .AngleBalayage)
        If AngDep < AngFin Then
          Angle = AngDep
        Else
          Angle = AngDep - 2 * PiEnDegrés
        End If
        While Angle < AngFin
          minimax(.pOF.X + xIns + .Rayon * System.Math.Cos(CvAngleRadians(Angle)), .pOF.Y + yIns + .Rayon * System.Math.Sin(CvAngleRadians(Angle)))
          Angle += PiEnDegrés / 6
        End While
        minimax(.pOF.X + xIns + .Rayon * System.Math.Cos(CvAngleRadians(AngFin)), .pOF.Y + yIns + .Rayon * System.Math.Sin(CvAngleRadians(AngFin)))
      End With
    Next objArc

    For Each objInsert In colInserts
      With objInsert
        NewPInsert = New PointF(xIns + .pInsertion.X, yIns + .pInsertion.Y)
        calminmax(pInsert:=NewPInsert, objBloc:=.Bloc)
      End With
    Next objInsert

  End Sub

  Private Sub reinFDP()

    Graph.Clear()
  End Sub

  Public Sub CalArc(ByVal pLine As PolyArc, ByVal p2 As PointF, ByVal unCalque As Calque, ByVal couleur As Integer, ByVal typelign As String)
    Dim pPrécédent As Pt
    Dim centre As PointF
    Dim angdeb, Rayon, angfin As Double
    'Calcul du centre d'un arc de polyligne défini par ses extrémités et par son arrondi
    ' L'arrondi est la tangente du quart de l'arc décrit.
    ' Le signe de l'arrondi est positif si l'arc est décrit dans le sens trigonométrique
    ' négatif dans le cas contraire
    ' Calcule également le rayon de l'arc ainsi que l'angle de début et de fin au sens de la méthode graphique Graphics.DrawArc

    Dim Beta, Alpha, x0, d, y0, alphap, Gamma As Double
    Dim arrondi As Double
    Dim mPts As Pts

    mPts = pLine.PtsPoly

    Try
      ' p1 : pt précédent
      pPrécédent = mPts(mPts.Count - 1)
      arrondi = pPrécédent.Arrondi
      Dim p1 As PointF = pPrécédent.p
      'calcul de la distance entre les 2 sommets consécutifs
      d = Distance(p1, p2)
      ' rayon
      Rayon = System.Math.Abs((d / 2) * (1 + arrondi ^ 2) / (2 * arrondi))
      ' arc alpha
      Alpha = 4 * System.Math.Atan(System.Math.Abs(arrondi))
      alphap = Alpha
      If alphap > PI Then alphap = 2 * PI - alphap ' => 0 < alphap < pi
      If arrondi < 0 Then Alpha = -Alpha ' => -2pi < alpha < 2pi
      Beta = PI / 2 - alphap / 2
      If Alpha > PI Or (Alpha > -PI And Alpha < 0) Then Beta = -Beta
      ' centre du cercle
      x0 = d / 2
      y0 = Rayon * System.Math.Sin(Beta)
      Gamma = AngleFormé(p1, p2)
      centre.X = x0 * System.Math.Cos(Gamma) - y0 * System.Math.Sin(Gamma) + p1.X
      centre.Y = x0 * System.Math.Sin(Gamma) + y0 * System.Math.Cos(Gamma) + p1.Y
      ' Drawing.DrawEllipse dessine tjs dans le sens horaire
      If arrondi < 0 Then  ' sens horaire
        angdeb = AngleFormé(centre, p1)
        angfin = AngleFormé(centre, p2)
      Else  ' sens trigo
        angdeb = AngleFormé(centre, p2)
        angfin = AngleFormé(centre, p1)
      End If

      angdeb = CvAngleDegrés(angdeb)
      angfin = CvAngleDegrés(angfin)
      Dim AngleBalayage As Single = AngleBalayageArc(angdeb, angfin)
      If AngleBalayage = 0.0 Then
        pPrécédent.Arrondi = 0
      Else
        Dim unArc As New Arc(centre, Rayon, angdeb, AngleBalayage)
        With unArc
          .DéfinirAttributs(unCalque, couleur, typelign)
        End With
        pLine.Add(unArc)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message & ComplémentMessage("LECDXF.CalArc"))
    End Try

  End Sub

  Private Function borne360(ByRef v As String) As Double
    ' Pour VB, les angles vont dans le sens horaire (à l'inverse d'autocad)
    ' le signe - ayant une signification particulière pour la méthode graphique CIRCLE

    borne360 = 360 - Val(v)

  End Function

  Private Function IsEntier(ByRef s As String) As Boolean

    On Error GoTo GestErr

    IsEntier = (CStr(CShort(s)) = s)
    Exit Function

GestErr:
    IsEntier = False
    Exit Function

  End Function

  Public Sub tableCouleur()
    Static Passage As Boolean

    If Passage Then Exit Sub


    Dim i As Integer
    ' transformation des couleurs AutoCAD en couleur VB (pour la fonction QBColor)
    ' utilisée par les différentes fonctions 'dessin'

    tCouleur(0) = 0    ' noir
    tCouleur(1) = 4    ' rouge
    tCouleur(2) = 6    ' jaune
    tCouleur(3) = 2    ' vert
    tCouleur(4) = 3    ' cyan
    tCouleur(5) = 1    ' bleu
    tCouleur(6) = 5    ' magenta
    tCouleur(7) = 0    ' blanc transformé en noir
    tCouleur(8) = 8    ' gris
    For i = 1 To 7
      tCouleur(i + 8) = tCouleur(i) + 8
      tCouleur(i) = tCouleur(i + 8)         ' couleurs + vives systématiqut
      ' Ajout AV 28.03.2000 pour la v3.0.2 : on essaye de restituer qq couleurs spéciales pour le retour d'export
      If i = 3 Then
        tCouleur(i + 8) = 13
      ElseIf i = 5 Then
        tCouleur(i + 8) = 5
      ElseIf i = 7 Then
        tCouleur(i + 8) = 4
      End If
    Next

    For i = 16 To 255
      tCouleur(i) = 0  ' noir
    Next i

    Passage = True

  End Sub

  Public Function substPtDecimalRegional(ByVal s As String, Optional ByVal Regional As Boolean = False) As String
    ' fonction appelée pour remplacer le point décimal par une virgule ou réciproquement
    ' ceci permet aux fontions Cdbl et IsNumeric (en particulier) de fonctionner correctement
    ' Enfin, le drapeau DXFBinaire provient du fait que la lecture par Get,suivi de la fonction CStr peut venir remettre une virgule dans le nombre

    Dim pos%
    Dim vraipoint, fauxpoint As Char


    If Regional Then
      vraipoint = cndPtDécimal
      fauxpoint = "."
    Else
      vraipoint = "."
      fauxpoint = cndPtDécimal
    End If

    If vraipoint <> fauxpoint Then
      pos = InStr(s, fauxpoint)
      While pos <> 0
        Mid(s, pos) = vraipoint
        pos = InStr(s, fauxpoint)
      End While
    End If

    substPtDecimalRegional = s

  End Function

End Module