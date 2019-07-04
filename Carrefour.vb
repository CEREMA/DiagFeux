'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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
Public Class Carrefour : Inherits M�tier

  'Le carrefour peut �tre un carrefour type de type suivant
  'En croix
  'En T
  'En Y
  'En �toile � 6 branches
  Public Enum CarrefourTypeEnum
    Aucun = -1
    EnCroix
    EnT
    EnY
    A5Branches
    EnEtoile  ' � 6 branches
  End Enum

  Public Shared strCarrefourType() As String = {"en croix", "en T", "en Y", "carrefour � 5 branches", "en �toile � 6 branches"}

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

  'Indique si le carrefour est situ� en ou hors agglom�ration
  '##ModelId=3C6FB1C302BF
  Public EnAgglo As Boolean = True

  'Zone de r�gulation (texte libre)
  '20 caract�res (R�f : R�ponses CERTU du 25/07/2003 sur Cahier des Charges)
  '##ModelId=3C6FB1F000DA
  Public ZoneR�gulation As String

  '##ModelId=3C6FB3E400EA
  Public Commentaires As String

  '##ModelId=3C6FB49E0290
  Public TypeControleur As String

  '##ModelId=3C6FB4AE0222
  Public DateControleur As Date

  'Infos compl�mentaires pour les impressions
  Public FabricantControleur, Num�ro, Coordonn�esService, SuperviseurTravaux, R�alisateurEtude, ObjectifEtude, OrigineVisa, Num�roVisa, VisaTrafics, Syst�meR�gulation, NumVersion, EnchainementPhases As String
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
  ' Cloner : duplique les propri�t�s du Carrefour dans un autre
  '  mCarrefour : Carrefour recevant les propri�t�s
  '**************************************************************************************
  Public Sub Cloner(ByVal unCarrefour As Carrefour)
    With unCarrefour
      .NbBranches = Me.NbBranches
      .Nom = Nom
      .Commune = Commune
      .EnAgglo = EnAgglo
      .ZoneR�gulation = ZoneR�gulation
      .Commentaires = Commentaires
      .TypeControleur = TypeControleur
      .DateControleur = DateControleur
    End With
  End Sub

  Public Sub New()
    'Par d�faut : carrefour en croix � 4 branches
    '	CarrefourType = Global.CarrefourTypeEnum.EnCroix
  End Sub

  Public Sub New(ByVal uneRowCarrefour As DataSetDiagfeux.CarrefourRow)
    Dim uneRowPropri�t�s As DataSetDiagfeux.Propri�t�sRow
    Dim i As CarrefourTypeEnum
    Dim dlg As New dlgInfoImpressions 'dans les versions inf�rieures � la v12, on acceptait toute longueur : on met une MaxLength aux TextBox et on tronque les valeurs pr�c�demment enregistr�es � cette longueusr

    'Propri�t�s du carrefour
    uneRowPropri�t�s = uneRowCarrefour.GetPropri�t�sRows(0)
    With uneRowPropri�t�s
      NbBranches = .NbBranches
      Nom = .Nom
      Commune = .Commune
      EnAgglo = .EnAgglo
      ZoneR�gulation = .ZoneR�gulation
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
      If Not .IsNum�roNull Then Me.Num�ro = .Num�ro
      If Not .IsPremierServiceNull Then Me.DatePremierService = .PremierService
      If Not .IsCoordonn�esServiceNull Then Me.Coordonn�esService = .Coordonn�esService
      If Not .IsSuperviseurTravauxNull Then Me.SuperviseurTravaux = TronquerSelonDlg(.SuperviseurTravaux, dlg.txtSuiviPar)
      If Not .IsDateEtudeNull Then Me.DateEtude = .DateEtude
      If Not .IsR�alisateurEtudeNull Then Me.R�alisateurEtude = TronquerSelonDlg(.R�alisateurEtude, dlg.txtR�alisateurEtude)
      If Not .IsObjectifEtudeNull Then Me.ObjectifEtude = .ObjectifEtude
      If Not .IsOrigineVisaNull Then Me.OrigineVisa = TronquerSelonDlg(.OrigineVisa, dlg.txtVisaDe)
      If Not .IsNum�roVisaNull Then Me.Num�roVisa = TronquerSelonDlg(.Num�roVisa, dlg.txtVisa)
      If Not .IsVisaTraficsNull Then Me.VisaTrafics = TronquerSelonDlg(.VisaTrafics, dlg.txtVisasTrafics)
      If Not .IsDateServiceNull Then Me.DateMiseEnService = .DateService
      If Not .IsDateModificationNull Then Me.DateModification = .DateModification
      If Not .IsDateModifPlageHoraireNull Then Me.DateModifPlageHoraire = .DateModifPlageHoraire
      If Not .IsNumVersionNull Then Me.NumVersion = TronquerSelonDlg(.NumVersion, dlg.txtNumVersion)
      If Not .IsDateVersionNull Then Me.DateVersion = .DateVersion
      If Not .IsSyst�meR�gulationNull Then Me.Syst�meR�gulation = .Syst�meR�gulation
      If Not .IsEnchainementPhasesNull Then Me.EnchainementPhases = .EnchainementPhases

      '    'Point Origine de la branche
      If .GetCentreRows.Length = 1 Then
        'Dans les versions ant�rieures au proto4, le centre du carrefour n'�tait pas d�fini : il faut donc faire le test
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
  ' Etape 1 : Cr�er les enregistrements n�cessaires dans le DataSet DIAGFEUX
  ' uneVariante : si c'est renseign�, l'appel provient de Variante.Enregistrer - N'enregistrer que le carrefour
  '********************************************************************************************************************
  Public Function Enregistrer(ByVal uneVariante As Variante) As DataSetDiagfeux.CarrefourRow
    Dim Cr�ation As Boolean

    Try

      Dim uneRowCarrefour As DataSetDiagfeux.CarrefourRow
      Dim uneRowPropri�t�s As DataSetDiagfeux.Propri�t�sRow

      'Ajouter une enregistrement dans la table des carrefours
      uneRowCarrefour = ds.Carrefour.NewCarrefourRow()
      ds.Carrefour.AddCarrefourRow(uneRowCarrefour)
      'Ajouter une enregistrement dans la table des Propri�t�s du carrefour
      uneRowPropri�t�s = ds.Propri�t�s.NewPropri�t�sRow()


      With uneRowPropri�t�s
        .NbBranches = NbBranches
        .Nom = Nom
        .Commune = Commune
        .EnAgglo = EnAgglo
        .ZoneR�gulation = ZoneR�gulation
        .Commentaires = Commentaires
        .TypeControleur = TypeControleur
        If Not EstNulleDate(DateControleur) Then .DateControleur = DateControleur
        .CarrefourType = strCarrefourType(CarrefourType)
        .Fabricant = Me.FabricantControleur
        .Num�ro = Me.Num�ro
        If Not EstNulleDate(DatePremierService) Then .PremierService = Me.DatePremierService
        .Coordonn�esService = Me.Coordonn�esService
        .SuperviseurTravaux = Me.SuperviseurTravaux
        If Not EstNulleDate(DateEtude) Then .DateEtude = Me.DateEtude
        .R�alisateurEtude = Me.R�alisateurEtude
        .ObjectifEtude = Me.ObjectifEtude
        .OrigineVisa = Me.OrigineVisa
        .Num�roVisa = Me.Num�roVisa
        .VisaTrafics = Me.VisaTrafics
        If Not EstNulleDate(DateMiseEnService) Then .DateService = Me.DateMiseEnService
        If Not EstNulleDate(DateModification) Then .DateModification = Me.DateModification
        If Not EstNulleDate(DateModifPlageHoraire) Then .DateModifPlageHoraire = Me.DateModifPlageHoraire
        .NumVersion = Me.NumVersion
        If Not EstNulleDate(DateVersion) Then .DateVersion = Me.DateVersion
        .Syst�meR�gulation = Me.Syst�meR�gulation
        .EnchainementPhases = Me.EnchainementPhases

        .SetParentRow(uneRowCarrefour)
        'Ajout effectif
        ds.Propri�t�s.AddPropri�t�sRow(uneRowPropri�t�s)

        ''Ajouter le centre du carrefour
        ds.Centre.AddCentreRow(mCentre.X, mCentre.Y, uneRowPropri�t�s)

        Return uneRowCarrefour

      End With
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Enregistrement du carrefour")
    End Try

  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    Dim uneBoite As Boite = Boite.NouvelleBoite(DemiLargeur:=4)
    mGraphique.Add(uneBoite)
    uneCollection.Add(mGraphique)

  End Function
End Class