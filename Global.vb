'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Global.vb																								'
'						Module des constantes et variables globales		  									'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On

'--------------------------- Module Global --------------------------
Module [Global]

  Public HelpFile As String

  'PasPassage : nombre de segments de ligne d'un cot� du passage pi�ton (v12 et ant�rieures :un seul segment)
  Public Const PasPassage As Short = 2

  'Version fichier : 0 (absent jusqu'� Acondia v12 inclus)
  '                  1 Acondia V13
  '                  2 DiagFeux v1
  '                  3 DiagFeux v2
  Public VersionFichier As Short = 3

  Public cndD�boguage As Boolean
  Public CouleurInvisible As Color = System.Drawing.SystemColors.ActiveCaptionText

  Public mdiApplication As DiagFeux.MDIDiagfeux
	Public NomExe As String = System.Reflection.Assembly.GetExecutingAssembly.Location
  Public Const etuExtension As String = "dfe"

  ' Derniers fichiers DIAGFEUX (.DFE) utilis�s
  '-----------------------------------------------------------
  Public MRUFichiers(-1) As String, nbfichMenu As Integer

  Public cndPrintDocument As New Printing.PrintDocument
  Public NomImprimante As String = cndPrintDocument.PrinterSettings.PrinterName

  Private myFileInfo As New IO.FileInfo(NomExe)
  Public CheminDiagfeux As String = myFileInfo.DirectoryName

  Public myFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(NomExe)
	Public NomProduit As String = myFileVersionInfo.ProductName
  Public VersionMajeure As Integer = myFileVersionInfo.FileMajorPart
  Public VersionMineure As Integer = myFileVersionInfo.FileMinorPart
	Private VersionIndice As Integer
	Private VersionBuild As Integer	 ' Inutilis� dans VB6
	Private VersionRelease As Integer = myFileVersionInfo.FilePrivatePart

  Public cndCheminStockage As String

  Public cndVariantes As New VarianteCollection
  Public cndGraphique As Graphics

  Public cndBlocsSignaux As New BlocCollection

  Public xMaxPicture, yMaxPicture As Integer
  Public cndpicDessin As PictureBox
  Public D�fautEchelle As Single = 4.0    ' 1 pixel = 4m
  Public D�fautOrigine As New PointF(0.0, 100.0)
  Public cndParamDessin As ParamDessin
  Public Wpmin, Wpmax As PointF

  Public ds As DataSetDiagfeux

  'Public cndAbaque As New frmAbaque
  
  Private NumberFormat As Globalization.NumberFormatInfo

  Public Const MAXPHASES As Short = 3

  'Param�tres g�n�raux de DIAGFEUX
  '
  'Les param�tres sont de 2 types :
  'Modifiables par l'utilisateur
  'Non Modifiables par l'utilisateur
  '
  'R�f�rence : Document Relev� de d�cisions de la r�union du 2 Octobre 2003 (r�v 220/10) �2 Outils...Param�tres, compl�t� par les pr�cisions au DAF du 07/01/2004

  Public Const d�fautVitessePi�tons As Single = 1 ' m/s - maxi : 1

  '##ModelId=403CAC490265
  Public Const d�fautVitesseV�hicules As Short = 10 ' m/s - maxi : 10

  '##ModelId=403CAC490266
  Public Const d�fautVitesseV�los As Short = 10 ' m/s - maxi : 10

  '##ModelId=403CAC490267
  Public Const d�fautD�bitSaturation As Short = 1800 ' uvp/h

  'Vert mini pi�ton
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC490269
  Private Const d�fautVertMiniPi�tons As Short = 6

  'Vert mini v�hicule
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026A
  Private Const d�fautVertMiniV�hicules As Short = 6

  'Jaune en agglom�ration
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026B
  Private Const d�fautJauneAgglo As Short = 3

  'Jaune hors agglom�ration
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026C
  Private Const d�fautJauneCampagne As Short = 5

  Private Const d�fautPerteD�marrageMax As Short = 5

  'Jaune clignotant (avc R11J)
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026D
  Private Const d�fautJauneClignotant As Short = 5

  'Equivalent 2 roues en UVP
  '##ModelId=403CAC49026E
  Private Const d�fautUvp2R As Single = 0.3

  'Equivalent Poids lourd en UVP
  '##ModelId=403CAC49026F
  Private Const d�fautUvpPL As Single = 2.0

  'Temps maximum entre la fin du vert et le d�but du vert suivant
  '##ModelId=403CAC490270
  Private Const d�fautAttenteMax As Short = 120

  'Temps minimum de rouge de d�gagement
  '
  '##ModelId=403CAC490271
  Private Const d�fautMiniRougeD�gagement As Short = 2


  'Versions ant�rieures ou = v11
  'Temps perdu au d�marrage et sur le jaune : le cahier des charges initial pr�voyait 5s. Il a �t� r�duit successivement � 4s, puis � 3s (04/05/2006)
  '  Private Const d�fautPerteAuD�marrage As Short = 3
  'v13
  Private Const d�fautPerteAuD�marrageMax As Short = 5

  'Coefficients de g�ne pour les mouvements TAG et TAD
  Private Const d�fautCoefG�neTAG As Single = 1.7
  Private Const d�fautCoefG�neTAD As Single = 1.1

  'Compter 5 m de long en moyenne par v�hicule pour les files d'attente
  ' R�f�rence : 'Compl�ments de calcul' du CERTU (28/11/2005) : Longueur de file d'attente
  Public Const LgMoyenneV�hicule As Short = 5

  '##ModelId=403CAC490262
  'Public cndOrganisme As String

  ''##ModelId=403CAC490263
  'Public cndService As String

  'Public cndD�calageVertUtile As Short = 0 ' [-3,3]
  Public cndParam�tres As New Param�tres(d�fautVitessePi�tons, d�fautVitesseV�hicules, d�fautVitesseV�los, d�fautD�bitSaturation, d�fautJauneAgglo, d�fautJauneCampagne, SignalPi�tonsSonore:=True)

  'Vert mini pi�ton
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC490269
  Public VertMiniPi�tons As Short = d�fautVertMiniPi�tons
  'Vert mini v�hicule
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026A
  Public VertMiniV�hicules As Short = d�fautVertMiniV�hicules
  'Hors sp�cifications : Valeur maximale des 2 verts mini ci-dessus 
  Public VertMiniMaximum As Short = 12

  'Jaune en agglom�ration
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026B
  Public JauneAgglo As Short = d�fautJauneAgglo

  'Jaune hors agglom�ration
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026C
  Public JauneCampagne As Short = d�fautJauneCampagne

  'Jaune clignotant (avc R11J)
  'Non modifiable par l'utilisateur
  '##ModelId=403CAC49026D
  Public JauneClignotant As Short = d�fautJauneClignotant

  'Equivalent 2 roues en UVP
  '##ModelId=403CAC49026E
  Public Uvp2R As Single = d�fautUvp2R

  'Equivalent Poids lourd en UVP
  '##ModelId=403CAC49026F
  Public UvpPL As Single = d�fautUvpPL

  'Temps maximum entre la fin du vert et le d�but du vert suivant
  '##ModelId=403CAC490270
  Public AttenteMax As Short = d�fautAttenteMax

  'Temps minimum de rouge de d�gagement
  '
  '##ModelId=403CAC490271
  Public MiniRougeD�gagement As Short = d�fautMiniRougeD�gagement

  'Versions ant�rieures ou = v11
  'Temps perdu au d�marrage et sur le jaune
  '##ModelId=403CAC490272
  'Public PerteAuD�marrage As Short = d�fautPerteAuD�marrage

  'v13
  Public PerteAuD�marrageMax As Short = d�fautPerteAuD�marrageMax

  'Coefficients de g�ne pour les mouvements TAG et TAD
  Public CoefG�neTAG As Single = d�fautCoefG�neTAG
  Public CoefG�neTAD As Single = d�fautCoefG�neTAD
  Public MinCoefG�ne As Single = 1.0
  Public MaxCoefG�ne As Single = 1.7

  'Public TbCycleCapacit�(0, 0) As Short
  'Public Dur�eCycleMini As Short
  'Public Incr�mentCycle As Short = 5
  'Public TempsPerduMini As Short
  'Public Function Dur�eCycleMaxi() As Short
  '  Return Dur�eCycleMini + Incr�mentCycle * TbCycleCapacit�.GetUpperBound(0)
  'End Function
  'Public Function TempsPerduMaxi() As Short
  '  Return TempsPerduMini + TbCycleCapacit�.GetUpperBound(1)
  'End Function

  Public Const EspacementV As Short = 8    '  8 mm d'espacement vertical entre les ellipses de dessin du trafic
  Public Const EspacementH As Short = 12   ' 12 mm d'espacement horizontal entre les ellipses

  Public Enum TypeLigneEnum
    DashType
  End Enum

  Public Enum OngletEnum
    G�om�trie
    LignesDeFeux
    Trafics
    Conflits
    PlansDeFeux
  End Enum

  Friend Enum Verrouillage
    Aucun
    G�om�trie
    LignesFeux
    Matrices
    PlanFeuBase
  End Enum

  Public ReadOnly Property Libell�CourtVerrouillage(ByVal Verrou As Verrouillage) As String
    Get
      Select Case Verrou
        Case Verrouillage.G�om�trie
          Return "G�om�trie"
        Case Verrouillage.LignesFeux
          Return "Lignes de feux"
        Case Verrouillage.Matrices
          Return "Matrice des conflits"
        Case Verrouillage.PlanFeuBase
          Return "Plan de feux de base"
      End Select
      NomProduit = myFileVersionInfo.ProductName

    End Get
  End Property


  Public cndPtD�cimal As String = Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator

  'Variables globales de  DIAGFEUX
  'Variante en cours
  Public cndVariante As Variante

  'Variante en cours
  Public cndCarrefour As Carrefour

  Public cndPlumes As New Plumes

  'Collection des signaux de feu
  Public cndSignaux As New SignalCollection

  Public cndContexte As OngletEnum
  Public cndFlagImpression As dlgImpressions.ImpressionEnum = dlgImpressions.ImpressionEnum.Aucun
  Public PhaseActiveImpressionRougeD�gagement As Phase
  Public cndZoneGraphique As Rectangle

#Region "Aide en ligne"
  'R�f�rence : context.h du CERTU du mer ao�  1 14:42:12 CEST 2007
  ' Doc_DiagFeux2007-07-30
  Public dctAide As New Hashtable

  Public Enum AideEnum


    SUPPORT = 10   ' pr01
    LICENCE = 20   ' pr02
    INTRODUCTION = 30  ' pr03

    PRINCIPES = 100     'ch01

    PR_GEOMETRIE = 101  'ch01s01
    PR_COMPATIBILITE = 102  'ch01s02
    PR_PHASAGE = 103  'ch01s03
    PR_TEMPS_TRAVERSEE = 104  'ch01s04
    PR_USAGER = 105  'ch01s05
    PR_INFOS = 106  'ch01s06
    PR_INFOS_OBLIG = 107  'ch01s06s01
    PR_INFOS_FACULT = 108  'ch01s06s02

    MANUEL_UTILISATEUR = 200    ' ch02
    LANCEMENT = 201  'ch02s01
    INTERFACES = 202  'ch02s02
    MENUS = 203  'ch02s03

    MENU_FICHIER = 204  'ch02s04
    MENU_NOUVEAU = 205  'ch02s04s01
    MENU_OUVRIR = 206  'ch02s04s02
    MENU_FERMER = 207  'ch02s04s03
    MENU_ENREGISTRER = 208  'ch02s04s04
    MENU_ENREGISTRERSOUS = 209   'ch02s04s05
    MENU_PARAMETRAGE = 210   'ch02s04s06
    MENU_CONFIG_IMPRIM = 211  'ch02s04s07
    MENU_IMPRIMER = 212   'ch02s04s08
    MENU_QUITTER = 213  'ch02s04s09

    ZONE_GRAPHIQUE = 214  ' ch02s05
    ONGLETS = 215   ' ch02s06
    ONGLET_GEOMETRIE = 216  ' ch02s06s01
    ONGLET_CIRCULATION = 217  ' ch02s06s02
    ONGLET_TRAFICS = 218  ' ch02s06s3
    ONGLET_CONFLITS = 219  ' ch02s06s04
    ONGLET_PLANS_FEUX = 220  ' ch02s06s05

    MENU_AFFICHAGE = 221  ' ch02s07
    BARRE_OUTILS = 222  ' ch02s07s01
    BARRE_ETAT = 223  ' ch02s07s02
    OUTIL_ECHELLE = 224  ' ch02s07s03
    OUTIL_NORD = 225  ' ch02s07s04
    SENS_CIRCULATION = 226  ' ch02s07s05
    SENS_TRAJECTOIRES = 227  ' ch02s07s06
    RAFRAICHIR = 228  ' ch02s07s07

    MENU_FENETRE = 229  ' ch02s08
    CASCADE = 230  ' ch02s08s01
    MOSAIQUE = 231  ' ch02s08s02

    MENU_AIDE = 232  ' ch02s09
    SOMMAIRE = 233  ' ch02s09s01
    AIDE_SUR = 234  ' ch02s09s02
    RECHERCHER = 235  ' ch02s09s03
    APROPOS = 236  ' ch02s09s04

    FICHIERS_DIAGFEUX = 237  ' ch02s10

  End Enum

  Public Function VersionSignificative() As String
    Return VersionMajeure & "." & VersionMineure
  End Function

  Public TopicAideCourant As AideEnum

  Public Sub AppelAide(ByVal uneFeuille As Form)
    Help.ShowHelp(uneFeuille, HelpFile, HelpNavigator.Topic, PageHtml(TopicAideCourant))
  End Sub

  Public Sub initAide()

    HelpFile = IO.Path.Combine(CheminDiagfeux, "Diagfeux.chm")

    dctAide.Add(CType(AideEnum.SUPPORT, Short), "pr01")
    dctAide.Add(CType(AideEnum.LICENCE, Short), "pr02")
    dctAide.Add(CType(AideEnum.INTRODUCTION, Short), "pr03")

    dctAide.Add(CType(AideEnum.PR_GEOMETRIE, Short), "ch01s01")
    dctAide.Add(CType(AideEnum.PR_COMPATIBILITE, Short), "ch01s02")
    dctAide.Add(CType(AideEnum.PR_PHASAGE, Short), "ch01s03")
    dctAide.Add(CType(AideEnum.PR_TEMPS_TRAVERSEE, Short), "ch01s04")
    dctAide.Add(CType(AideEnum.PR_USAGER, Short), "ch01s05")
    dctAide.Add(CType(AideEnum.PR_INFOS, Short), "ch01s06")
    dctAide.Add(CType(AideEnum.PR_INFOS_OBLIG, Short), "ch01s06s01")
    dctAide.Add(CType(AideEnum.PR_INFOS_FACULT, Short), "ch01s06s02")

    dctAide.Add(CType(AideEnum.MANUEL_UTILISATEUR, Short), "ch02")
    dctAide.Add(CType(AideEnum.LANCEMENT, Short), "ch02s01")
    dctAide.Add(CType(AideEnum.INTERFACES, Short), "ch02s02")
    dctAide.Add(CType(AideEnum.MENUS, Short), "ch02s03")

    dctAide.Add(CType(AideEnum.MENU_FICHIER, Short), "ch02s04")
    dctAide.Add(CType(AideEnum.MENU_NOUVEAU, Short), "ch02s04s01")
    dctAide.Add(CType(AideEnum.MENU_OUVRIR, Short), "ch02s04s02")
    dctAide.Add(CType(AideEnum.MENU_FERMER, Short), "ch02s04s03")
    dctAide.Add(CType(AideEnum.MENU_ENREGISTRER, Short), "ch02s04s04")
    dctAide.Add(CType(AideEnum.MENU_ENREGISTRERSOUS, Short), "ch02s04s05")
    dctAide.Add(CType(AideEnum.MENU_PARAMETRAGE, Short), "ch02s04s06")
    dctAide.Add(CType(AideEnum.MENU_CONFIG_IMPRIM, Short), "ch02s04s07")
    dctAide.Add(CType(AideEnum.MENU_IMPRIMER, Short), "ch02s04s08")
    dctAide.Add(CType(AideEnum.MENU_QUITTER, Short), "ch02s04s09")

    dctAide.Add(CType(AideEnum.ZONE_GRAPHIQUE, Short), "ch02s05")
    dctAide.Add(CType(AideEnum.ONGLETS, Short), "ch02s06")
    dctAide.Add(CType(AideEnum.ONGLET_GEOMETRIE, Short), "ch02s06s01")
    dctAide.Add(CType(AideEnum.ONGLET_CIRCULATION, Short), "ch02s06s02")
    dctAide.Add(CType(AideEnum.ONGLET_TRAFICS, Short), "ch02s06s03")
    dctAide.Add(CType(AideEnum.ONGLET_CONFLITS, Short), "ch02s06s04")
    dctAide.Add(CType(AideEnum.ONGLET_PLANS_FEUX, Short), "ch02s06s05")

    dctAide.Add(CType(AideEnum.MENU_AFFICHAGE, Short), "ch02s07")
    dctAide.Add(CType(AideEnum.BARRE_OUTILS, Short), "ch02s07s01")
    dctAide.Add(CType(AideEnum.BARRE_ETAT, Short), "ch02s07s02")
    dctAide.Add(CType(AideEnum.OUTIL_ECHELLE, Short), "ch02s07s03")
    dctAide.Add(CType(AideEnum.OUTIL_NORD, Short), "ch02s07s04")
    dctAide.Add(CType(AideEnum.SENS_CIRCULATION, Short), "ch02s07s05")
    dctAide.Add(CType(AideEnum.SENS_TRAJECTOIRES, Short), "ch02s07s06")
    dctAide.Add(CType(AideEnum.RAFRAICHIR, Short), "ch02s07s07")

    dctAide.Add(CType(AideEnum.MENU_FENETRE, Short), "ch02s08")
    dctAide.Add(CType(AideEnum.CASCADE, Short), "ch02s08s01")
    dctAide.Add(CType(AideEnum.MOSAIQUE, Short), "ch02s08s03")

    dctAide.Add(CType(AideEnum.MENU_AIDE, Short), "ch02s09")
    dctAide.Add(CType(AideEnum.SOMMAIRE, Short), "ch02s09s01")
    dctAide.Add(CType(AideEnum.AIDE_SUR, Short), "ch02s09s02")
    dctAide.Add(CType(AideEnum.RECHERCHER, Short), "ch02s09s03")
    dctAide.Add(CType(AideEnum.APROPOS, Short), "ch02s09s04")

  End Sub

  Public Function PageHtml(ByVal Index As Short) As String
    Return dctAide(Index) & ".html"
  End Function

  Public Const pr01 As Short = 10
  Public Const pr02 As Short = 20
  Public Const pr03 As Short = 30

  Public Const ch01 As Short = 100

  Public Const ch01s01 As Short = 101
  Public Const ch01s02 As Short = 102
  Public Const ch01s03 As Short = 103
  Public Const ch01s04 As Short = 104
  Public Const ch01s05 As Short = 105
  Public Const ch01s06 As Short = 106
  Public Const ch01s06s01 As Short = 107
  Public Const ch01s06s02 As Short = 108

  Public Const ch02 As Short = 200
  Public Const ch02s01 As Short = 201
  Public Const ch02s02 As Short = 202
  Public Const ch02s03 As Short = 203
  Public Const ch02s04 As Short = 204
  Public Const ch02s04s01 As Short = 205
  Public Const ch02s04s02 As Short = 206
  Public Const ch02s04s03 As Short = 207
  Public Const ch02s04s04 As Short = 208
  Public Const ch02s04s05 As Short = 209
  Public Const ch02s04s06 As Short = 210
  Public Const ch02s04s07 As Short = 211
  Public Const ch02s04s08 As Short = 212
  Public Const ch02s04s09 As Short = 213
  Public Const ch02s05 As Short = 214
  Public Const ch02s06 As Short = 215
  Public Const ch02s06s01 As Short = 216
  Public Const ch02s06s02 As Short = 217
  Public Const ch02s06s03 As Short = 218
  Public Const ch02s06s04 As Short = 219
  Public Const ch02s06s05 As Short = 220
  Public Const ch02s07 As Short = 221
  Public Const ch02s07s01 As Short = 222
  Public Const ch02s07s02 As Short = 223
  Public Const ch02s07s03 As Short = 224
  Public Const ch02s07s04 As Short = 225
  Public Const ch02s07s05 As Short = 226
  Public Const ch02s07s06 As Short = 227
  Public Const ch02s07s07 As Short = 228
  Public Const ch02s08 As Short = 229
  Public Const ch02s08s01 As Short = 230
  Public Const ch02s08s02 As Short = 231
  Public Const ch02s09 As Short = 232
  Public Const ch02s09s01 As Short = 233
  Public Const ch02s09s02 As Short = 234
  Public Const ch02s09s03 As Short = 235
  Public Const ch02s09s04 As Short = 236
  Public Const ch02s10 As Short = 237

#End Region
End Module