'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Carrefour.vb																							'
'						Classes																														'
'							Carrefour																												'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe Carrefour --------------------------
'=====================================================================================================
Public Class Carrefour : Inherits Métier

  'Le carrefour peut être un carrefour type de type suivant
  'En croix
  'En T
  'En Y
  'En étoile à 6 branches
  Public Enum CarrefourTypeEnum
    Aucun = -1
    EnCroix
    EnT
    EnY
    A5Branches
    EnEtoile  ' à 6 branches
  End Enum

  Public Shared strCarrefourType() As String = {"en croix", "en T", "en Y", "carrefour à 5 branches", "en étoile à 6 branches"}

  Public Const MiniNbBranches As Short = 3
  Public Const MaxiNbBranches As Short = 6
  'Nombre de branches
  '
  '##ModelId=3C6FB146006D
  Public NbBranches As Short = 4


  '
  'Si ce n'est pas un carrefour-type : TypeCarrefour=Aucun
  '##ModelId=4032153C00AB
  Public CarrefourType As CarrefourTypeEnum = CarrefourTypeEnum.EnCroix

  'Nom du carrefour
  '##ModelId=3C6FB1850271
  Private mNom As String

  'Nom de la commune
  '##ModelId=3C6FB1990213
  Public Commune As String

  'Indique si le carrefour est situé en ou hors agglomération
  '##ModelId=3C6FB1C302BF
  Public EnAgglo As Boolean = True

  'Zone de régulation (texte libre)
  '20 caractères (Réf : Réponses CERTU du 25/07/2003 sur Cahier des Charges)
  '##ModelId=3C6FB1F000DA
  Public ZoneRégulation As String

  '##ModelId=3C6FB3E400EA
  Public Commentaires As String

  '##ModelId=3C6FB49E0290
  Public TypeControleur As String

  '##ModelId=3C6FB4AE0222
  Public DateControleur As Date

  'Infos complémentaires pour les impressions
  Public FabricantControleur, Numéro, CoordonnéesService, SuperviseurTravaux, RéalisateurEtude, ObjectifEtude, OrigineVisa, NuméroVisa, VisaTrafics, SystèmeRégulation, NumVersion, EnchainementPhases As String
  Public DatePremierService, DateEtude, DateMiseEnService, DateModification, DateModifPlageHoraire, DateVersion As Date

  Public mCentre As PointF

  '##ModelId=3C70D29602E0
  Public mVariantes As New VarianteCollection

  Public Property Nom() As String
    Get
      Return mNom
    End Get
    Set(ByVal Value As String)
      mNom = Value
    End Set
  End Property

  '**************************************************************************************
  ' Cloner : duplique les propriétés du Carrefour dans un autre
  '  mCarrefour : Carrefour recevant les propriétés
  '**************************************************************************************
  Public Sub Cloner(ByVal unCarrefour As Carrefour)
    With unCarrefour
      .NbBranches = Me.NbBranches
      .Nom = Nom
      .Commune = Commune
      .EnAgglo = EnAgglo
      .ZoneRégulation = ZoneRégulation
      .Commentaires = Commentaires
      .TypeControleur = TypeControleur
      .DateControleur = DateControleur
    End With
  End Sub

  Public Sub New()
    'Par défaut : carrefour en croix à 4 branches
    '	CarrefourType = Global.CarrefourTypeEnum.EnCroix
  End Sub

  Public Sub New(ByVal uneRowCarrefour As DataSetDiagfeux.CarrefourRow)
    Dim uneRowPropriétés As DataSetDiagfeux.PropriétésRow
    Dim i As CarrefourTypeEnum
    Dim dlg As New dlgInfoImpressions 'dans les versions inférieures à la v12, on acceptait toute longueur : on met une MaxLength aux TextBox et on tronque les valeurs précédemment enregistrées à cette longueusr

    'Propriétés du carrefour
    uneRowPropriétés = uneRowCarrefour.GetPropriétésRows(0)
    With uneRowPropriétés
      NbBranches = .NbBranches
      Nom = .Nom
      Commune = .Commune
      EnAgglo = .EnAgglo
      ZoneRégulation = .ZoneRégulation
      Commentaires = .Commentaires
      If Not .IsTypeControleurNull Then TypeControleur = TronquerSelonDlg(.TypeControleur, dlg.txtTypeControleur)
      If Not .IsDateControleurNull Then DateControleur = .DateControleur
      For i = 0 To strCarrefourType.Length - 1
        If .CarrefourType = strCarrefourType(i) Then
          CarrefourType = i
          Exit For
        End If
      Next

      If Not .IsFabricantNull Then Me.FabricantControleur = TronquerSelonDlg(.Fabricant, dlg.txtFabricant)
      If Not .IsNuméroNull Then Me.Numéro = .Numéro
      If Not .IsPremierServiceNull Then Me.DatePremierService = .PremierService
      If Not .IsCoordonnéesServiceNull Then Me.CoordonnéesService = .CoordonnéesService
      If Not .IsSuperviseurTravauxNull Then Me.SuperviseurTravaux = TronquerSelonDlg(.SuperviseurTravaux, dlg.txtSuiviPar)
      If Not .IsDateEtudeNull Then Me.DateEtude = .DateEtude
      If Not .IsRéalisateurEtudeNull Then Me.RéalisateurEtude = TronquerSelonDlg(.RéalisateurEtude, dlg.txtRéalisateurEtude)
      If Not .IsObjectifEtudeNull Then Me.ObjectifEtude = .ObjectifEtude
      If Not .IsOrigineVisaNull Then Me.OrigineVisa = TronquerSelonDlg(.OrigineVisa, dlg.txtVisaDe)
      If Not .IsNuméroVisaNull Then Me.NuméroVisa = TronquerSelonDlg(.NuméroVisa, dlg.txtVisa)
      If Not .IsVisaTraficsNull Then Me.VisaTrafics = TronquerSelonDlg(.VisaTrafics, dlg.txtVisasTrafics)
      If Not .IsDateServiceNull Then Me.DateMiseEnService = .DateService
      If Not .IsDateModificationNull Then Me.DateModification = .DateModification
      If Not .IsDateModifPlageHoraireNull Then Me.DateModifPlageHoraire = .DateModifPlageHoraire
      If Not .IsNumVersionNull Then Me.NumVersion = TronquerSelonDlg(.NumVersion, dlg.txtNumVersion)
      If Not .IsDateVersionNull Then Me.DateVersion = .DateVersion
      If Not .IsSystèmeRégulationNull Then Me.SystèmeRégulation = .SystèmeRégulation
      If Not .IsEnchainementPhasesNull Then Me.EnchainementPhases = .EnchainementPhases

      '    'Point Origine de la branche
      If .GetCentreRows.Length = 1 Then
        'Dans les versions antérieures au proto4, le centre du carrefour n'était pas défini : il faut donc faire le test
        With .GetCentreRows(0)
          mCentre = New PointF(.X, .Y)
        End With
      End If

    End With

  End Sub

  Private Function TronquerSelonDlg(ByVal Chaine As String, ByVal txt As TextBox) As String

    Return Chaine.Substring(0, Math.Min(Chaine.Length, txt.MaxLength))

  End Function


  '********************************************************************************************************************
  ' Enregistrer le carrefour dans le fichier
  ' Etape 1 : Créer les enregistrements nécessaires dans le DataSet DIAGFEUX
  ' uneVariante : si c'est renseigné, l'appel provient de Variante.Enregistrer - N'enregistrer que le carrefour
  '********************************************************************************************************************
  Public Function Enregistrer(ByVal uneVariante As Variante) As DataSetDiagfeux.CarrefourRow
    Dim Création As Boolean

    Try

      Dim uneRowCarrefour As DataSetDiagfeux.CarrefourRow
      Dim uneRowPropriétés As DataSetDiagfeux.PropriétésRow

      'Ajouter une enregistrement dans la table des carrefours
      uneRowCarrefour = ds.Carrefour.NewCarrefourRow()
      ds.Carrefour.AddCarrefourRow(uneRowCarrefour)
      'Ajouter une enregistrement dans la table des Propriétés du carrefour
      uneRowPropriétés = ds.Propriétés.NewPropriétésRow()


      With uneRowPropriétés
        .NbBranches = NbBranches
        .Nom = Nom
        .Commune = Commune
        .EnAgglo = EnAgglo
        .ZoneRégulation = ZoneRégulation
        .Commentaires = Commentaires
        .TypeControleur = TypeControleur
        If Not EstNulleDate(DateControleur) Then .DateControleur = DateControleur
        .CarrefourType = strCarrefourType(CarrefourType)
        .Fabricant = Me.FabricantControleur
        .Numéro = Me.Numéro
        If Not EstNulleDate(DatePremierService) Then .PremierService = Me.DatePremierService
        .CoordonnéesService = Me.CoordonnéesService
        .SuperviseurTravaux = Me.SuperviseurTravaux
        If Not EstNulleDate(DateEtude) Then .DateEtude = Me.DateEtude
        .RéalisateurEtude = Me.RéalisateurEtude
        .ObjectifEtude = Me.ObjectifEtude
        .OrigineVisa = Me.OrigineVisa
        .NuméroVisa = Me.NuméroVisa
        .VisaTrafics = Me.VisaTrafics
        If Not EstNulleDate(DateMiseEnService) Then .DateService = Me.DateMiseEnService
        If Not EstNulleDate(DateModification) Then .DateModification = Me.DateModification
        If Not EstNulleDate(DateModifPlageHoraire) Then .DateModifPlageHoraire = Me.DateModifPlageHoraire
        .NumVersion = Me.NumVersion
        If Not EstNulleDate(DateVersion) Then .DateVersion = Me.DateVersion
        .SystèmeRégulation = Me.SystèmeRégulation
        .EnchainementPhases = Me.EnchainementPhases

        .SetParentRow(uneRowCarrefour)
        'Ajout effectif
        ds.Propriétés.AddPropriétésRow(uneRowPropriétés)

        ''Ajouter le centre du carrefour
        ds.Centre.AddCentreRow(mCentre.X, mCentre.Y, uneRowPropriétés)

        Return uneRowCarrefour

      End With
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Enregistrement du carrefour")
    End Try

  End Function

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me

    Dim uneBoite As Boite = Boite.NouvelleBoite(DemiLargeur:=4)
    mGraphique.Add(uneBoite)
    uneCollection.Add(mGraphique)

  End Function
End Class