'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Variante.vb																							'
'						Classes																														'
'							Variante																												'
'							VarianteCollection  : collection de variantes 									'
'           Structure
'             Paramètres
'******************************************************************************
Option Strict Off
Option Explicit On 

'
'=====================================================================================================
'--------------------------- Classe Variante --------------------------
'=====================================================================================================
Public Class Variante : Inherits Métier

  Public mParamDessin As ParamDessin
  'Taille du carrefour (représentant son encombrement en mètres pour pouvoir le dessiner)
  Private mTailleF As SizeF
  Private pMinFDP, pMaxFDP As PointF

  '##ModelId=403214C802FD
  Public ModeGraphique As Boolean = False

  '##ModelId=3C70D0E1037A
  Public mFondDePlan As FondDePlan

  '##ModelId=3C70D29602DE
  Public mCarrefour As Carrefour

  Public mLignesFeux As New LigneFeuxCollection(Me)
  Public mTrajectoires As New TrajectoireCollection(Me)

  '##ModelId=403C7E5C0261
  Public mBranches As New BrancheCollection(Me)
  Public mPassagesEnAttente As New PassageCollection
  Public mCourants As New CourantCollection

  '##ModelId=403C80C60232
  Public mTrafics As New TraficCollection

  '##ModelId=3C8B46930280
  Public mPlansFeuxBase As New PlanFeuxCollection
  Private mScénarioCourant As PlanFeuxBase
  Private mScénarioDéfinitif As PlanFeuxBase

  Public VertMiniVéhicules As Short = [Global].VertMiniVéhicules
  Public VertMiniPiétons As Short = [Global].VertMiniPiétons

  Private mVerrou As Verrouillage = Verrouillage.Aucun
  Private mSensTrajectoires As Boolean
  Private mSensCirculation As Boolean

  Public NomFichier As String
  Public mDataSet As DataSetDiagfeux

  Private mParamètres As New Paramètres(Initial:=True)

  Private mAEnregistrer As Boolean

  Private mNord As New Nord
  Private mSymEchelle As New SymEchelle

  Public Property ScénarioCourant() As PlanFeuxBase
    Get
      Return mScénarioCourant
    End Get
    Set(ByVal Value As PlanFeuxBase)
      mScénarioCourant = Value
    End Set
  End Property

  Public Function ScénarioEnCours() As Boolean
    Return Not IsNothing(mScénarioCourant)
  End Function

  Public Property ScénarioDéfinitif() As PlanFeuxBase
    Get
      Return mScénarioDéfinitif
    End Get
    Set(ByVal Value As PlanFeuxBase)
      mScénarioDéfinitif = Value
    End Set
  End Property

  Public Sub CréerScénario(ByVal nomScénario As String, ByVal AvecTrafic As Boolean)
    Dim unTrafic As Trafic
    Dim unPlanBase As PlanFeuxBase

    With mPlansFeuxBase
      If AvecTrafic Then
        'Scénario avec trafic
        unTrafic = New Trafic(Me)
        unTrafic.Nom = nomScénario
        mTrafics.Add(unTrafic)
        unPlanBase = .Item(.Add(New PlanFeuxBase(unTrafic)))

      Else
        'Scénario sans trafic
        unPlanBase = .Item(.Add(New PlanFeuxBase(nomScénario)))
      End If
    End With

    If Verrou >= [Global].Verrouillage.LignesFeux Then
      'Si le verrou est supérieur à LignesFeux, c'est qu'au moins un scénario est dans cet état
      ' donc les lignes de feux sont verrouillées 
      unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
      'Initialiser les rouges de dégagement avec les valeurs mini
      unPlanBase.mLignesFeux.InitialiserTempsDégagement(mLignesFeux)
    Else
      unPlanBase.Verrou = [Global].Verrouillage.Géométrie
    End If

    mScénarioCourant = unPlanBase


  End Sub

  Public Function EnAgglo() As Boolean
    Return mCarrefour.EnAgglo
  End Function

  Public Property AEnregistrer() As Boolean
    Get
      Return mAEnregistrer
    End Get
    Set(ByVal Value As Boolean)
      mAEnregistrer = Value
    End Set
  End Property

  Public Property Param() As Paramètres
    Get
      Return mParamètres
    End Get
    Set(ByVal Value As Paramètres)
      mParamètres = Value
    End Set
  End Property

  Public Property VitessePiétons() As Single
    Get
      Return mParamètres.VitessePiétons
    End Get
    Set(ByVal Value As Single)
      mParamètres.VitessePiétons = Value
    End Set
  End Property

  Public Property VitesseVéhicules() As Single
    Get
      Return mParamètres.VitesseVéhicules

    End Get
    Set(ByVal Value As Single)
      mParamètres.VitesseVéhicules = Value
    End Set
  End Property

  Property SensTrajectoires() As Boolean
    Get
      Return mSensTrajectoires
    End Get
    Set(ByVal Value As Boolean)
      mSensTrajectoires = Value
      mdiApplication.mnuSensTrajectoires.Checked = Value
      mTrajectoires.Verrouiller()
    End Set
  End Property

  Property SensCirculation() As Boolean
    Get
      Return mSensCirculation
    End Get
    Set(ByVal Value As Boolean)
      mSensCirculation = Value
      mBranches.AfficherSens()
      mdiApplication.mnuSensCirculation.Checked = Value
    End Set
  End Property

  Property NordAffiché() As Boolean
    Get
      Return mNord.Affiché
    End Get
    Set(ByVal Value As Boolean)
      mNord.Affiché = Value
      mdiApplication.mnuNord.Checked = Value
    End Set
  End Property

  Property EchelleAffichée() As Boolean
    Get
      Return mSymEchelle.Affiché
    End Get
    Set(ByVal Value As Boolean)
      mSymEchelle.Affiché = Value
      mdiApplication.mnuEchelle.Checked = Value
    End Set
  End Property

  Public ReadOnly Property SignalDessinable() As Boolean
    Get
      Select Case cndFlagImpression
        Case dlgImpressions.ImpressionEnum.Aucun
          SignalDessinable = Echelle >= 6
        Case dlgImpressions.ImpressionEnum.PlanCarrefour
          SignalDessinable = True
      End Select

    End Get
  End Property

  Public Property VitesseVélos() As Single
    Get
      Return mParamètres.VitesseVélos

    End Get
    Set(ByVal Value As Single)
      mParamètres.VitesseVélos = Value
    End Set
  End Property

  Public Property DébitSaturation() As Short
    Get
      Return mParamètres.DébitSaturation
    End Get
    Set(ByVal Value As Short)
      mParamètres.DébitSaturation = Value
    End Set
  End Property

  Public ReadOnly Property TempsPerduParPhase() As Short
    'Référence : Guide Carrefour du CERTU (§3.5 p31)
    Get
      Return TempsInutilisé + MiniRougeDégagement
    End Get
  End Property

  Private ReadOnly Property TempsInutilisé() As Short
    Get
      Return TempsPerduDémarrage + JauneInutilisé
    End Get
  End Property

  Public Property TempsPerduDémarrage() As Short
    Get
      If EnAgglo() Then
        Return mParamètres.TempsPerduDémarrageAgglo
      Else
        Return mParamètres.TempsPerduDémarrageCampagne
      End If
    End Get
    Set(ByVal Value As Short)
      If EnAgglo() Then
        mParamètres.TempsPerduDémarrageAgglo = Value
      Else
        mParamètres.TempsPerduDémarrageCampagne = Value
      End If
    End Set
  End Property

  Public Property JauneInutilisé() As Short
    Get
      If EnAgglo() Then
        Return mParamètres.TempsJauneInutiliséAgglo
      Else
        Return mParamètres.TempsJauneInutiliséCampagne
      End If
    End Get
    Set(ByVal Value As Short)
      If EnAgglo() Then
        mParamètres.TempsJauneInutiliséAgglo = Value
      Else
        mParamètres.TempsJauneInutiliséCampagne = Value
      End If
    End Set
  End Property

  Public ReadOnly Property JauneVéhicules() As Short
    Get
      If EnAgglo() Then
        Return [Global].JauneAgglo
      Else
        Return [Global].JauneCampagne
      End If
    End Get
  End Property

  Public ReadOnly Property DécalageVertUtile() As Short
    Get
      Return JauneVéhicules - TempsInutilisé
    End Get
  End Property

  Public ReadOnly Property strVertUtile() As String
    Get
      Dim chaine As String

      If DécalageVertUtile <> 0 Then

        If DécalageVertUtile > 0 Then
          chaine = "+"
        End If

        chaine &= DécalageVertUtile

        If Math.Abs(DécalageVertUtile) > 1 Then
          chaine &= " secondes"
        Else
          chaine &= " seconde"
        End If
      End If

      Return chaine
    End Get
  End Property

  Public Property SignalPiétonsSonore() As Boolean
    Get
      Return mParamètres.SignalPiétonsSonore
    End Get
    Set(ByVal Value As Boolean)
      mParamètres.SignalPiétonsSonore = Value
    End Set
  End Property

  Public Property VersionFichier() As Short
    Get
      Return mParamètres.VersionFichier
    End Get
    Set(ByVal Value As Short)
      mParamètres.VersionFichier = Value
    End Set
  End Property

  Public Property Organisme() As String
    Get
      Return mParamètres.Organisme
    End Get
    Set(ByVal Value As String)
      mParamètres.Organisme = Value
    End Set
  End Property

  Public Property Service() As String
    Get
      Return mParamètres.Service
    End Get
    Set(ByVal Value As String)
      mParamètres.Service = Value
    End Set
  End Property

  Public Property CheminLogo() As String
    Get
      Return mParamètres.CheminLogo
    End Get
    Set(ByVal Value As String)
      mParamètres.CheminLogo = Value
    End Set
  End Property

  Friend Function OngletInterdit(ByVal Index As OngletEnum) As Boolean
    Dim Message As String = ""

    Select Case Index
      Case OngletEnum.Géométrie
      Case OngletEnum.LignesDeFeux
        If Verrou < Verrouillage.Géométrie Then
          Message = "Il faut d'abord verrouiller la géométrie"
        End If

      Case OngletEnum.Trafics
        If Verrou < Verrouillage.Géométrie Then
          Message = "Il faut d'abord verrouiller la géométrie"
        ElseIf Verrou < [Global].Verrouillage.LignesFeux And Not ModeGraphique Then
          Message = "Il faut d'abord verrouiller les lignes de feux"
        End If

      Case OngletEnum.Conflits
        If Verrou < Verrouillage.Géométrie Then
          Message = "Il faut d'abord verrouiller la géométrie"
        ElseIf Verrou < Verrouillage.LignesFeux Then
          Message = "Il faut d'abord verrouiller les lignes de feux"
        ElseIf Not ScénarioEnCours() Then
          Message = "Il faut d'abord créer un scénario"
        ElseIf ScénarioCourant.AvecTrafic AndAlso Not ScénarioCourant.Trafic.Verrouillé Then
          Message = "Il faut d'abord verrouiller la période de trafic"
        End If

      Case OngletEnum.PlansDeFeux
        If Verrou < Verrouillage.Géométrie Then
          Message = "Il faut d'abord verrouiller la géométrie"
        ElseIf Verrou < Verrouillage.LignesFeux Then
          Message = "Il faut d'abord verrouiller les lignes de feux"
        ElseIf Verrou < Verrouillage.Matrices Then
          Message = "Il faut d'abord verrouiller la matrice des conflits"
        End If
    End Select

    If Message.Length > 0 Then
      AfficherMessageErreur(Nothing, Message)
      OngletInterdit = True
    End If

  End Function

  Public Property ConflitsInitialisés() As Boolean
    Get
      If Not IsNothing(ScénarioCourant) Then
        Return ScénarioCourant.ConflitsInitialisés
      End If
    End Get
    Set(ByVal Value As Boolean)
      Dim unScénario As PlanFeuxBase
      If Value Then
        ScénarioCourant.ConflitsInitialisés = True
      Else
        'Le verrouillage des lignes de feux doit réinitialiser les conflits de tous les scénarios
        For Each unScénario In mPlansFeuxBase
          unScénario.ConflitsInitialisés = False
        Next
      End If

    End Set
  End Property

  Friend Property Verrou() As Verrouillage
    Get
      If IsNothing(mScénarioCourant) Then
        Return mVerrou
      Else
        Return mScénarioCourant.Verrou
      End If
    End Get

    Set(ByVal Value As Verrouillage)
      Dim unScénario As PlanFeuxBase

      If IsNothing(ScénarioCourant) Then
        mVerrou = Value
      Else
        Select Case Value
          Case [Global].Verrouillage.Aucun  ' Déverrouillage de la géométrie
            mVerrou = Value

            'Supprimer tous les scénarios et les trafics
            mPlansFeuxBase.Clear()
            mTrafics.Clear()
            ScénarioCourant = Nothing

          Case [Global].Verrouillage.Géométrie
            mVerrou = Value
            'On vient peut-être de déverrouiller les lignes de feux : redescendre tous les scénarios au niveau Géométrie
            For Each unScénario In mPlansFeuxBase
              unScénario.Verrou = Value
            Next

          Case [Global].Verrouillage.LignesFeux
            mVerrou = Value
            For Each unScénario In mPlansFeuxBase
              If unScénario.Verrou < [Global].Verrouillage.LignesFeux Then
                'On vient de verrouiller les lignes de feux : faire monter tous les scénarios(en fait avec trafic) à ce niveau
                unScénario.Verrou = Value
              ElseIf unScénario Is ScénarioCourant Then
                'C'est ce plan de feux 
                unScénario.Verrou = Value
              End If
            Next

          Case Else
            'Le verrouillage ne concerne que le scénario en cours : la variante elle-même n'ira jamais au-delà du verrou Lignesde feux
            ScénarioCourant.Verrou = Value
        End Select
      End If
    End Set
  End Property

  Friend ReadOnly Property PlansPourPhasage() As PlanFeuxCollection
    Get
      Return mScénarioCourant.PlansPourPhasage
    End Get
  End Property

  Public Property TroisPhasesSeulement() As Boolean
    Get
      Return mScénarioCourant.mTroisPhasesSeulement
    End Get
    Set(ByVal Value As Boolean)
      mScénarioCourant.mTroisPhasesSeulement = Value
    End Set
  End Property
  'Indique si la géométrie est verrouillée
  Public ReadOnly Property VerrouGéom() As Boolean
    Get
      VerrouGéom = (Verrou >= Verrouillage.Géométrie)
    End Get
  End Property

  'Indique si le schéma de circulation et les lignes de feux sont verrouillées
  '##ModelId=4032294402CE
  Public ReadOnly Property VerrouLigneFeu() As Boolean
    Get
      VerrouLigneFeu = (Verrou >= Verrouillage.LignesFeux)
    End Get
  End Property

  'Indique si les matrices de sécurité sont verrouillées
  Public ReadOnly Property VerrouMatrices() As Boolean
    Get
      VerrouMatrices = (Verrou >= Verrouillage.Matrices)
    End Get
  End Property

  'Indique si le plan de feux de base est verrouillé
  Public ReadOnly Property VerrouFeuBase() As Boolean
    Get
      VerrouFeuBase = (Verrou = Verrouillage.PlanFeuBase)
    End Get
  End Property

  '********************************************************************************************************************
  ' Verrouiller/Déverrouiller une étape
  ' Incrément = +1 ou -1  selon que l'on avance ou que l'on recule dans une étape
  '********************************************************************************************************************
  Public Sub BasculerVerrou(ByVal chk As CheckBox)

    With chk
      Select Case .Name
        Case "chkVerrouGéométrie"
          If .Checked Then
            Verrou = [Global].Verrouillage.Géométrie
          Else
            Verrou = [Global].Verrouillage.Aucun
          End If
        Case "chkVerrouLignesFeux"
          If .Checked Then
            Verrou = [Global].Verrouillage.LignesFeux
          Else
            Verrou = [Global].Verrouillage.Géométrie
          End If
        Case "chkVerrouMatrice"
          If .Checked Then
            If Verrou = [Global].Verrouillage.LignesFeux Then
              'On bascule le verrou pour la première fois pour le scénario courant
              Verrou = [Global].Verrouillage.Matrices
            End If
          Else
            Verrou = [Global].Verrouillage.LignesFeux
          End If
        Case "chkVerrouFeuBase"
          If .Checked Then
            Verrou = [Global].Verrouillage.PlanFeuBase
          Else
            Verrou = [Global].Verrouillage.Matrices
          End If
      End Select
    End With

    If chk.Checked Then
      ' Remettre à 0 les objets éventuellement créés lors d'une variante précédente
      Select Case mVerrou
        Case Verrouillage.Géométrie
          Me.mLignesFeux.Clear()
          Me.mTrajectoires.Clear()
      End Select
    End If

  End Sub

  '************************************************************************************
  ' Détermine si on peut poser le verrou immédiatement supérieur à l'actuel
  '************************************************************************************
  Public Function NonVerrouillable() As Métier
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxVéhicules As LigneFeuVéhicules
    Dim uneBranche, uneBranche2 As Branche
    Dim uneVoie As Voie
    Dim uneTrajectoire As Trajectoire
    Dim msg As String
    Dim VerrouAPoser As Verrouillage = Verrou + 1

    Try
      Select Case VerrouAPoser
        Case [Global].Verrouillage.Géométrie
          For Each uneBranche In mBranches
            If uneBranche.mPassages.Count = 2 AndAlso _
            (uneBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Or uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante)) Then
              NonVerrouillable = uneBranche
              msg = "Branche " & mBranches.ID(uneBranche) & " : " & uneBranche.NomRue & vbCrLf & _
                    "Une branche à sens unique ne peut comporter qu'un seul passage piéton"
              Exit For
            End If

          Next

        Case [Global].Verrouillage.LignesFeux
          ' Verrouillage des lignes de feux

          If ModeGraphique Then

            'Vérifier que toutes les voies entrantes sont l'origine d'au moins une trajectoire
            For Each uneBranche In mBranches
              For Each uneVoie In uneBranche.Voies
                If uneVoie.Entrante Then
                  If Not mTrajectoires.ContientOrigine(uneVoie) Then
                    msg = "Branche " & mBranches.ID(uneBranche) & " : " & uneBranche.NomRue & vbCrLf & _
                          "Toutes les voies entrantes doivent comporter au moins une trajectoire"
                    NonVerrouillable = uneVoie
                    Exit For
                  End If
                End If
              Next
              If Not IsNothing(NonVerrouillable) Then Exit For
            Next

            If IsNothing(msg) Then
              'Vérifier que toutes les trajectoires sont commandées par une ligne de feux
              For Each uneTrajectoire In mTrajectoires
                If uneTrajectoire.EstVéhicule AndAlso IsNothing(uneTrajectoire.LigneFeu) Then
                  uneBranche = CType(uneTrajectoire, TrajectoireVéhicules).mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine)
                  uneBranche2 = CType(uneTrajectoire, TrajectoireVéhicules).mBranche(TrajectoireVéhicules.OrigineDestEnum.Destination)
                  msg = "Trajectoire depuis la branche " & mBranches.ID(uneBranche) & " : " & uneBranche.NomRue & _
                        " vers la branche " & mBranches.ID(uneBranche2) & " : " & uneBranche2.NomRue & vbCrLf & _
                        "Toutes les trajectoires doivent être commandées par une ligne de feux"
                  NonVerrouillable = uneTrajectoire
                  Exit For
                End If
              Next
            End If

          Else
            'Mode tableur : en mode graphique, l'initialisation des courants est faite au verrouillage de la géométrie
            InitialiserCourants()

            For Each uneLigneFeux In mLignesFeux
              If uneLigneFeux.EstVéhicule Then
                uneLigneFeuxVéhicules = uneLigneFeux
                With uneLigneFeuxVéhicules
                  If .NbVoiesTableur = 0 Then
                    'Possible en mode non graphique 
                    msg = "Indiquer au moins une voie par ligne de feux"
                  ElseIf Not (.TAD Or .TAG Or .TD) Then
                    msg = "Indiquer au moins un courant directionnel par ligne de feux"
                  End If
                  If Not IsNothing(msg) Then
                    NonVerrouillable = uneLigneFeuxVéhicules
                    Exit For
                  ElseIf .DéterminerCourants Then
                    NonVerrouillable = .mBranche
                    msg = "Impossible d'affecter les courants issus de la la branche " & mBranches.ID(.mBranche)
                  End If
                End With
              End If
            Next

            If IsNothing(NonVerrouillable) Then
              uneBranche = mBranches.DéterminerCourants()
              If Not IsNothing(uneBranche) Then
                msg = "Impossible d'affecter les courants sur la branche " & mBranches.ID(uneBranche)
                NonVerrouillable = uneBranche
              End If
            End If

          End If

        Case [Global].Verrouillage.Matrices
          ' Verrouillage de la matrice des conflits
          'Vérifier si les antagonismes sont résolus
          If ModeGraphique AndAlso AntagonismesCourants.NbConflitsARésoudre > 0 Then
            msg = "Tous les conflits n'ont pas été résolus"
            NonVerrouillable = AntagonismesCourants(0)

          Else
            'Vérifier si au moins un phasage est possible
            ScénarioCourant.ConstruirePlansDePhasage()
            If PlansPourPhasage.Count = 0 Then
              msg = "Aucune organisation de phasage n'est possible sans conflit"
              NonVerrouillable = Me
            End If
          End If
      End Select

      If Not IsNothing(msg) Then
        AfficherMessageErreur(Nothing, msg)
        msg = Nothing
      End If

    Catch ex As DiagFeux.Exception
      msg = ex.Message
      NonVerrouillable = Me
    Catch ex As System.Exception
      msg = ex.Message & vbCrLf & "Variante.NonVerrouillable"
      NonVerrouillable = Me

    Finally
      If Not IsNothing(msg) Then Throw New DiagFeux.Exception(msg)

    End Try

  End Function

  Public ReadOnly Property LibelléVerrouillage() As String
    Get
      Select Case Verrou
        Case Verrouillage.Aucun
          LibelléVerrouillage = ""
        Case Verrouillage.Géométrie
          LibelléVerrouillage = "Géométrie verrouillée"
        Case Verrouillage.LignesFeux
          LibelléVerrouillage = "Lignes de feux verrouillées"
        Case Verrouillage.Matrices
          LibelléVerrouillage = "Matrice des conflits verrouillée"
        Case Verrouillage.PlanFeuBase
          LibelléVerrouillage = "Plan de feux de base verrouillé"
      End Select

    End Get
  End Property

  Public ReadOnly Property PhasageRetenu() As Boolean
    '//DIAGFEUX//
    Get
      If ScénarioEnCours() Then
        Return mPlanFeuxBase.PhasageRetenu
      End If
    End Get
  End Property

  Public ReadOnly Property UnPhasageRetenu() As Boolean
    Get
      Dim unPlanBase As PlanFeuxBase
      For Each unPlanBase In mPlansFeuxBase
        If unPlanBase.PhasageRetenu Then
          UnPhasageRetenu = True
          Exit For
        End If
      Next
    End Get
  End Property
  Public Function PremierPlanBaseRetenu() As PlanFeuxBase
    Dim unPlanBase As PlanFeuxBase
    For Each unPlanBase In mPlansFeuxBase
      If unPlanBase.PhasageRetenu Then
        Return unPlanBase
      End If
    Next

  End Function
  Public Function PremierPlanFonctionnement() As PlanFeuxFonctionnement
    Dim unPlanBase As PlanFeuxBase
    For Each unPlanBase In mPlansFeuxBase
      If unPlanBase.mPlansFonctionnement.Count > 0 Then
        Return unPlanBase.mPlansFonctionnement(CType(0, Short))
      End If
    Next

  End Function
  Public ReadOnly Property DiagnosticCalculé() As Boolean
    Get
      Dim unPlanBase As PlanFeuxBase
      For Each unPlanBase In mPlansFeuxBase
        If unPlanBase.AvecTrafic AndAlso unPlanBase.mPlansFonctionnement.Count > 0 Then
          Return True
          Exit For
        End If
      Next

    End Get
  End Property

  Public Sub RéordonnerPlansFeux()
    Dim unScénario As PlanFeuxBase
    Dim uneLigneFeux As LigneFeux

    For Each unScénario In mPlansFeuxBase
      If Not unScénario.PhasageRetenu Then
        unScénario.mLignesFeux.Clear()
        For Each uneLigneFeux In mLignesFeux
          unScénario.mLignesFeux.Add(uneLigneFeux)
        Next
      End If
    Next

  End Sub

  Public ReadOnly Property mPlanFeuxBase() As PlanFeuxBase
    Get

      Return ScénarioCourant

    End Get
  End Property

  Public ReadOnly Property Antagonismes() As AntagonismeCollection
    Get
      Return mTrajectoires.Antagonismes
    End Get
  End Property

  Public ReadOnly Property AntagonismesCourants() As AntagonismeCollection
    Get
      Return ScénarioCourant.Antagonismes
    End Get
  End Property
  Public Property BrancheEnCoursAntagonisme() As Branche
    Get

      Return AntagonismesCourants.BrancheEnCoursAntagonisme
    End Get
    Set(ByVal Value As Branche)
      AntagonismesCourants.BrancheEnCoursAntagonisme = Value
    End Set
  End Property


  '********************************************************************************************************************
  ' Déterminer les points de conflit entre tous les couples de trajectoires
  ' Cette fonction est appelée une seule fois : lors du verrouillage des lignes de feux
  '********************************************************************************************************************
  Public Sub VerrouillerLignesFeux(ByVal Verrouillage As Boolean, ByVal uneCollection As Graphiques, Optional ByVal ChargementEnCours As Boolean = False)
    Dim unPlanFeuxBase As PlanFeuxBase

    If Not ChargementEnCours Then
      mLignesFeux.Dimensionner(RemiseAZéro:=Not Verrouillage)
    End If

    If ModeGraphique Then
      If Verrouillage And Not ChargementEnCours Then mTrajectoires.DéterminerConflits()
      mTrajectoires.Verrouiller()

      If Verrouillage Then
        InitialiserTempsDégagement()
      End If

      If Not ChargementEnCours Then
        DéterminerCourants(Verrouillage)

        For Each unPlanFeuxBase In mPlansFeuxBase
          'CréerAntagonismes crée également la collection des lignes de feux du plan de feux de base
          unPlanFeuxBase.CréerAntagonismes()
          unPlanFeuxBase.Antagonismes.CréerGraphique(uneCollection)
        Next
      End If

      mLignesFeux.Verrouiller()

    Else
      For Each unPlanFeuxBase In mPlansFeuxBase
        'L'instruction qui suit ne crée en fait que les lignes de feux
        unPlanFeuxBase.CréerAntagonismes()
      Next

    End If

    If Verrouillage Then
      For Each unPlanFeuxBase In mPlansFeuxBase
        If unPlanFeuxBase.Verrou >= [Global].Verrouillage.Matrices Then
          unPlanFeuxBase.ConstruirePlansDePhasage()
          unPlanFeuxBase.CalculerCapacitésPlansPhasage()
        End If
      Next

    Else
      Antagonismes.Clear()
      For Each unPlanFeuxBase In mPlansFeuxBase
        unPlanFeuxBase.ConflitsInitialisés = False
      Next
      If Not ModeGraphique Then
        mLignesFeux.RéinitialiserVoies()
      End If
    End If

  End Sub

  Public Sub InitialiserTempsDégagement()
    Dim unPlanFeuxBase As PlanFeuxBase

    If ModeGraphique Then
      mLignesFeux.DéterminerTempsDégagement()
    End If

  End Sub

  Public Sub RéinitialiserPhasages()
    Dim unPlanBase As PlanFeuxBase

    InitialiserTempsDégagement()
    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.RéinitialiserPhasage()
    Next
  End Sub

  Public Sub RéinitialiserCapacités()
    Dim unPlanBase As PlanFeuxBase

    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.CapacitéACalculer = True
    Next

  End Sub

  '*************************************************************************************************
  ' Déterminer les courants de la variante
  ' VerrouillageLignesFeux : Indique si on est en train de verrouiller ou non les lignes de feux
  '*************************************************************************************************
  Private Sub DéterminerCourants(ByVal VerrouillageLignesFeux As Boolean)
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneVéhicules As LigneFeuVéhicules
    Dim uneBranche As Branche
    Dim uneVoie As Voie
    Dim nbVoiesEntrantes(mBranches.Count - 1) As Short
    Dim i As Short

    If VerrouillageLignesFeux Then
      'Déterminer les courants commandés par chaque voie de chaque ligne de feux véhicules

      For Each uneLigneFeux In mLignesFeux
        If uneLigneFeux.EstVéhicule Then
          With CType(uneLigneFeux, LigneFeuVéhicules)
            If ModeGraphique Then
              mTrajectoires.DéterminerCourants(.Voies)
            Else
              .DéterminerCourants()
              i = mBranches.IndexOf(.mBranche)
              nbVoiesEntrantes(i) += .NbVoiesTableur
            End If
          End With
        End If
      Next

      If Not ModeGraphique Then
        '!!!!!!!!!!!!!!!!DIAFEUX 3!!!!!!!!!!!!!!!!!!!!!!!
        'Les lignes qui suivent sont  supprimées dans la version définitive
        'elles permettaient de récupérer d'anciens fichiers tableur avec un nombre de voies entrantes non en concordance avec les voies des LF
        'For i = 0 To nbVoiesEntrantes.Length - 1
        '  mBranches(CType(i, Short)).NbVoies(Voie.TypeVoieEnum.VoieEntrante) = nbVoiesEntrantes(i)
        'Next

        mBranches.DéterminerCourants()
      End If

    Else
      'Supprimer tous les courants de chaque voie
      For Each uneBranche In mBranches
        For Each uneVoie In uneBranche.Voies
          If uneVoie.Entrante Then
            uneVoie.mCourants.Clear()
          End If
        Next
      Next
    End If

  End Sub

  '********************************************************************************************************************
  'Réinitialiser les lignes de feux suite au déverrouillage de celles-ci
  '********************************************************************************************************************
  Private Sub RéinitialiserLignesFeux()
    Dim unPLanFeuxBase As PlanFeuxBase

    'Toutes les lignes de feux sont à nouveau compatibles
    mLignesFeux.RéinitialiserIncompatibilités()

    'Supprimer les lignes de feux des plans de feux de base
    For Each unPLanFeuxBase In mPlansFeuxBase
      unPLanFeuxBase.mLignesFeux.Clear()
    Next
  End Sub


  'Le carrefour est composé s'il y a plusieurs plans de feux de base (partition de l'ensemble des lignes de feux)
  '##ModelId=3C8B6B4A0251
  Public Function EstComposé() As Boolean

  End Function

  Public Sub New()
    mCarrefour = New Carrefour
    ModeGraphique = True
  End Sub

  Public Sub New(ByVal unCarrefour As Carrefour, ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim i As Short
    Dim uneBranche As Branche
    Dim uneTrajectoire As Trajectoire
    Dim uneLigneFeux As LigneFeux
    Dim unSignal As Signal
    Dim unPlanFeux As PlanFeux
    Dim unTrafic As Trafic

    cndVariante = Me
    mCarrefour = unCarrefour

    Dim uneRowParamétrage As DataSetDiagfeux.ParamétrageRow
    If ds.Paramétrage.Rows.Count > 0 Then
      uneRowParamétrage = ds.Paramétrage.Rows(0)
      With uneRowParamétrage
        Try
          VersionFichier = .Version
        Catch ex As StrongTypingException
          'Version non renseignée : < v13
          VersionFichier = 0
          TempsPerduDémarrage = 0
          'Par défaut : Jaune utilisé = 0
          JauneInutilisé = JauneVéhicules
          SignalPiétonsSonore = True
        End Try
        Organisme = .Organisme
        Service = .Service
        If Not .IsLogoNull Then
          CheminLogo = .Logo
        End If
        VitessePiétons = .VitessePiétons
        VitesseVéhicules = .VitesseVéhicules
        VitesseVélos = .VitesseVélos
        DébitSaturation = .DébitSaturation
        'v12 et antérieures
        'DécalageVertUtile = .VertUtile

        'v13
        If VersionFichier > 0 Then
          SignalPiétonsSonore = .SignalPiétonsSonore
        End If

        'v13 et DiagFeux 1
        'If VersionFichier > 0 Then
        '  TempsPerduDémarrage = .TempsPerduDémarrage
        '  mParamètres.TempsJauneInutiliséAgglo = .JauneInutiliséAgglo
        '  mParamètres.TempsJauneInutiliséCampagne = .JauneInutiliséCampagne
        'End If

        'DiagFeux2 (sauf si DiagFeux1 n'est pas conservé)

        Select Case VersionFichier
          Case 1
            TempsPerduDémarrage = .TempsPerduDémarrage
            If EnAgglo() Then
              JauneInutilisé = .JauneInutiliséAgglo
            Else
              JauneInutilisé = .JauneInutiliséCampagne
            End If
          Case Is > 1
            If EnAgglo() Then
              TempsPerduDémarrage = .TempsPerduDémarrage
              JauneInutilisé = .JauneInutiliséAgglo
            Else
              TempsPerduDémarrage = .TempsPerduDémarrageCampagne
              JauneInutilisé = .JauneInutiliséCampagne
            End If

        End Select

        'Alimenter la valeur de JauneInutilisé qui ne sert pas avec les valeurs par défaut du poste de travail(a priori sans intérêt)
        If EnAgglo() Then
          mParamètres.TempsJauneInutiliséCampagne = cndParamètres.TempsJauneInutiliséCampagne
        Else
          mParamètres.TempsJauneInutiliséAgglo = cndParamètres.TempsJauneInutiliséAgglo
        End If
      End With  ' uneRowParamétrage
    End If

    ' Affichages optionnels
    If ds.Affichage.Rows.Count > 0 Then
      Dim uneRowAffichage As DataSetDiagfeux.AffichageRow = ds.Affichage.Rows(0)
      With uneRowAffichage
        If .GetNordRows.Length > 0 Then
          mNord = New Nord(.GetNordRows(0))
        End If

        If .GetSymEchelleRows.Length > 0 Then
          mSymEchelle = New SymEchelle(.GetSymEchelleRows(0))
        End If

        mSensTrajectoires = .SensTrajectoires
        mSensCirculation = .SensCirculation

      End With  'uneRowAffichage
    End If

    With uneRowVariante
      ModeGraphique = .ModeGraphique
      mVerrou = .Verrou
      VertMiniPiétons = .VertMiniPiétons
      VertMiniVéhicules = .VertMiniVéhicules

      If .GetParamDessinRows.Length = 1 Then
        Dim uneRowParamDessin As DataSetDiagfeux.ParamDessinRow = .GetParamDessinRows(0)
        With uneRowParamDessin
          mParamDessin.Echelle = .Echelle
          mParamDessin.OrigineRéelle = New PointF(.GetOrigineReelleRows(0).X, .GetOrigineReelleRows(0).Y)
          If .GetTailleRows.Length = 1 Then
            Dim uneTaille As Size
            uneTaille = New Size(.GetTailleRows(0).X, .GetTailleRows(0).Y)
            'Récupérer la taille de la fenêtre lors de l'enregistrement sauf si celle-ci est + grande que l'écran
            If uneTaille.Width <= mdiApplication.ClientSize.Width And uneTaille.Height <= mdiApplication.ClientSize.Height Then
              mParamDessin.TailleFenêtre = uneTaille
            End If
          End If
        End With
        cndParamDessin = mParamDessin
      End If

      ' Lire le fond de plan
      If .GetFondPlanRows.Length > 0 Then
        Dim uneRowFondPlan As DataSetDiagfeux.FondPlanRow = .GetFondPlanRows(0)
        With uneRowFondPlan
          If .EstDXF Then
            mFondDePlan = New DXF(uneRowFondPlan)
            CType(mFondDePlan, DXF).Construire(ds.DXF.Rows(0))
          Else

            Dim NomFichier As String = ImageRaster.FichierExistant(uneRowFondPlan.Nom)
            If IsNothing(NomFichier) Then
              mFondDePlan = Nothing
              mAEnregistrer = True
            Else
              'Le fond de plan initial a été déplacé : il faudra enregistrer avec le nouveau chemin
              If uneRowFondPlan.Nom <> NomFichier Then mAEnregistrer = True
              uneRowFondPlan.Nom = NomFichier
              mFondDePlan = New ImageRaster(uneRowFondPlan)
            End If
          End If
        End With
      End If

      ' Lire les branches

      For i = 0 To mCarrefour.NbBranches - 1
        Dim pOrigine As PointF
        Dim unPassage As PassagePiéton
        uneBranche = New Branche(.GetBrancheRows(i), Me)
        mBranches.Add(uneBranche)
        For Each unPassage In uneBranche.mPassages
          mPassagesEnAttente.Add(unPassage)
        Next
      Next

      'Initialiser les courants de circulation (ils seront réinitialisés + tard si c'est prématuré)
      InitialiserCourants()

      'Lire les lignes de feux
      For i = 0 To .GetLigneDeFeuxRows.Length - 1
        Dim uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow = .GetLigneDeFeuxRows(i)
        unSignal = cndSignaux(uneRowLigneDeFeux.GetSignalRows(0).strCode)
        If unSignal.EstPiéton Then
          uneLigneFeux = New LigneFeuPiétons(uneRowLigneDeFeux)
        Else
          uneLigneFeux = New LigneFeuVéhicules(uneRowLigneDeFeux)
        End If
        mLignesFeux.Add(uneLigneFeux)
      Next


      'Créer les incompatibilités et les temps de rouge de dégagement entre les lignes de feux
      If Verrou >= [Global].Verrouillage.LignesFeux Then
        mLignesFeux.CréerIncompatibilités(.GetLigneDeFeuxRows)
      End If

      'Lire les trajectoires
      Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow
      For i = 0 To .GetTrajectoireRows.Length - 1
        uneRowTrajectoire = .GetTrajectoireRows(i)
        If uneRowTrajectoire.GetPiétonsRows.Length = 1 Then
          'Traversée piétonne
          uneTrajectoire = New TraverséePiétonne(uneRowTrajectoire)
        Else
          'Trajectoire véhicules
          uneTrajectoire = New TrajectoireVéhicules(uneRowTrajectoire)
        End If
        Try
          uneTrajectoire.LigneFeu = mLignesFeux(uneRowTrajectoire.IDLigneDeFeux)
          uneTrajectoire.LigneFeu.mTrajectoires.Add(uneTrajectoire)
          If uneTrajectoire.EstVéhicule Then
            CType(uneTrajectoire.LigneFeu, LigneFeuVéhicules).AjouterBrancheSortie(uneTrajectoire)
          End If
        Catch ex As StrongTypingException
          'IDLigneDeFeux est DBNull : aucune ligne de feux n'est encore associée à la trajectoire 
        Catch ex As System.NullReferenceException
          'IDLigneDeFeux ne correspond à aucune ligne de feux
          Throw New DiagFeux.Exception("La ligne de feux " & uneRowTrajectoire.IDLigneDeFeux & " associée à la trajectoire n'existe pas")
        Catch ex As System.Exception
          Throw New DiagFeux.Exception(ex.ToString)
        End Try
        mTrajectoires.Add(uneTrajectoire)
      Next

      If mVerrou >= [Global].Verrouillage.LignesFeux Then
        DéterminerCourants(VerrouillageLignesFeux:=True)
        If ModeGraphique Then
          ' Lire les antagonismes
          mTrajectoires.CréerAntagonismes(uneRowVariante)
        Else
          mBranches.DéterminerCourants()
        End If
      End If

      If ModeGraphique Then
        For Each uneLigneFeux In mLignesFeux
          If uneLigneFeux.EstVéhicule Then
            CType(uneLigneFeux, LigneFeuVéhicules).DéterminerNatureCourants(mTrajectoires)
          End If
        Next
      End If

      ' Lire les trafics
      For i = 0 To .GetTraficRows.Length - 1
        unTrafic = New Trafic(.GetTraficRows(i))
        mTrafics.Add(unTrafic)
      Next

      Dim DrapeauDiagFeux As Boolean

      ' Lire les plans de feux
      Dim unPlanFeuxBase As PlanFeuxBase
      For i = 0 To .GetPlanFeuxRows.Length - 1
        Dim uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow = .GetPlanFeuxRows(i)
        With uneRowPlanFeux
          If .IsNomFonctionnementNull Then
            'Plan de feux de base
            unPlanFeuxBase = New PlanFeuxBase(uneRowPlanFeux)
            mPlansFeuxBase.Add(unPlanFeuxBase)
            If unPlanFeuxBase.Nom.Length > 0 Then
              DrapeauDiagFeux = True
            End If
          Else
            If DrapeauDiagFeux Then
              unPlanFeuxBase = mPlansFeuxBase(.ID)
            Else
              unPlanFeuxBase = mPlansFeuxBase(CType(.ID, Short))
            End If
            unPlanFeux = New PlanFeuxFonctionnement(uneRowPlanFeux, unPlanFeuxBase)
          End If
        End With
      Next

      If VersionFichier > 1 Then
        'Projet DIAGFEUX
        If .IsPlanFeuxCourantNull Then
          If mPlansFeuxBase.Count > 0 Then
            ScénarioCourant = mPlansFeuxBase(CType(0, Short))
          End If
        Else
          ScénarioCourant = mPlansFeuxBase(.PlanFeuxCourant)
        End If

      Else
        'Projet ACONDIA
        ScénarioCourant = ScénarioAcondia(.GetLigneDeFeuxRows)
      End If

      For Each unPlanFeuxBase In mPlansFeuxBase
        unPlanFeuxBase.IndexerLignesFeux()
      Next

    End With 'uneRowVariante

    Dim coord(-1) As Double
    If mCarrefour.mCentre.IsEmpty Then
      Dim p As PointF
      For Each uneBranche In mBranches
        ReDim Preserve coord(coord.Length + 1)
        With uneBranche
          p = PointPosition(.OrigineRelative, .Longueur + 3, .Angle, True)
          coord(coord.Length - 2) = p.X
          coord(coord.Length - 1) = p.Y
        End With
      Next
      Wpmin = New PointF(1000, 1000)
      Wpmax = New PointF(-1000, -1000)
      minimax(coord)
      InitEchelle()
    End If

    If mParamDessin.IsEmpty Then
      mParamDessin = New ParamDessin(DéfautEchelle, DéfautOrigine)
    End If

    cndParamDessin = mParamDessin

  End Sub

  Private Function ScénarioAcondia(ByVal desRowLignesDeFeux As DataSetDiagfeux.LigneDeFeuxRow()) As PlanFeuxBase
    Dim unPlanBase As PlanFeuxBase
    Dim pfBase As PlanFeuxBase
    Dim Garbage As New PlanFeuxCollection
    Dim tfGarbage As New TraficCollection
    Dim pfFct As PlanFeuxFonctionnement
    Dim unTrafic As Trafic

    For Each unTrafic In mTrafics
      If unTrafic.QTotal(Trafic.TraficEnum.UVP) = 0 Then
        tfGarbage.Add(unTrafic)
      End If
    Next

    For Each unTrafic In tfGarbage
      mTrafics.Remove(unTrafic)
    Next

    'Projet ACONDIA
    If mPlansFeuxBase.Count > 0 Then

      pfBase = mPlansFeuxBase(CType(0, Short))
      pfBase.Verrou = mVerrou


      'Projet ACONDIA : un seul plan de feux de base avec ou sans trafic
      'Créer les incompatibilités et les temps de rouge de dégagement entre les lignes de feux
      'pfBase.mLignesFeux.CréerIncompatibilités(desRowLignesDeFeux)

      If mTrafics.Count = 0 Then
        pfBase.Nom = "Scénario sans trafic"

      Else
        Dim PFSansTrafic As PlanFeuxBase
        With mPlansFeuxBase

          'Affecter les plans de feux de fonctionnement au scénario correspondant à leur période de trafic
          For Each pfFct In pfBase.mPlansFonctionnement
            If pfFct.AvecTrafic Then
              'Affecter le trafic du 1er plan de fonctionnement avec trafic au plan de feux de base
              pfBase.Trafic = pfFct.Trafic
              Exit For
            End If
          Next

          If Not pfBase.AvecTrafic Then
            'Affecter le 1er trafic au plan de feux de base
            pfBase.Trafic = mTrafics(CType(0, Short))
          End If

          'Ajouter un Scénario par période de trafic
          For Each unTrafic In mTrafics
            If unTrafic Is pfBase.Trafic Then
              unTrafic.Verrouillé = True
            Else
              unPlanBase = .Item(.Add(New PlanFeuxBase))
              unPlanBase.Trafic = unTrafic
              unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
              unPlanBase.CréerAntagonismes()
            End If
          Next

        End With

      End If

    Else
      If mTrafics.Count > 0 Then
        'Projet ACONDIA : Trafics créés mais pas de phasage de retenu
        With mPlansFeuxBase
          For Each unTrafic In mTrafics
            .Add(New PlanFeuxBase(unTrafic))
            unPlanBase = .Item(CType(.Count - 1, Short))
            If mVerrou = [Global].Verrouillage.Matrices Then
              unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
            Else
              unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
            End If
          Next
        End With
        pfBase = mPlansFeuxBase(CType(0, Short))
        pfBase.Verrou = mVerrou

      ElseIf mVerrou = [Global].Verrouillage.Matrices Then
        pfBase = New PlanFeuxBase("Scénario sans trafic")
        pfBase.Verrou = mVerrou
        mPlansFeuxBase.Add(pfBase)
      End If
    End If

    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.VertMiniVéhicules = VertMiniVéhicules
      unPlanBase.VertMiniPiétons = VertMiniPiétons
    Next

    Dim unAntagonisme As Antagonisme
    If Not IsNothing(pfBase) AndAlso Not IsNothing(pfBase.Antagonismes) Then
      'La création des antagonismes n'a dupliqué que les systématiques dan le plan de feux 
      ' pour les projets ACONDIA, il faut une duplication complète
      For Each unAntagonisme In Antagonismes
        pfBase.Antagonismes(Antagonismes.IndexOf(unAntagonisme)).TypeConflit = unAntagonisme.TypeConflit
      Next
    End If
    Return pfBase

  End Function

  '******************************************************************************
  ' Définir l'encombrement du dessin
  '******************************************************************************
  Public Sub DéfinirEncombrement(ByVal NomRuesACompter As Boolean)
    Dim uneBranche As Branche
    Dim pMin, pMax As PointF
    Dim pExt, pExtrémité(mBranches.Count * 2 - 1) As PointF
    Dim DemiLargeur As Single

    If Not IsNothing(mFondDePlan) Then
      With mFondDePlan
        Wpmin = .pMin
        Wpmax = .pMax
      End With

    Else
      pMin = New PointF(10000, 10000)
      pMax = New PointF(-10000, -10000)

      Dim AjoutLongueur As Short
      Dim i As Short = 0
      '10 m : Augmenter cette valeur s'il s'avère que le nom des rues ne peut pas s'inscrire dans le cadre
      AjoutLongueur = 10

      For Each uneBranche In mBranches
        With uneBranche
          DemiLargeur = .Largeur / 2
          pExt = PointPosition(.Origine, .Longueur + AjoutLongueur, .Angle, True)
          pExtrémité(i) = PointPosition(pExt, DemiLargeur, .AngleEnRadians + Math.PI / 2)
          With pExtrémité(i)
            pMin.X = Math.Min(pMin.X, .X)
            pMin.Y = Math.Min(pMin.Y, .Y)
            pMax.X = Math.Max(pMax.X, .X)
            pMax.Y = Math.Max(pMax.Y, .Y)
          End With
          pExtrémité(i + 1) = PointPosition(pExtrémité(i), DemiLargeur, .AngleEnRadians - Math.PI / 2)
          With pExtrémité(i + 1)
            pMin.X = Math.Min(pMin.X, .X)
            pMin.Y = Math.Min(pMin.Y, .Y)
            pMax.X = Math.Max(pMax.X, .X)
            pMax.Y = Math.Max(pMax.Y, .Y)
          End With
          i += 2

        End With
        Wpmin = pMin
        Wpmax = pMax
      Next

      If NomRuesACompter Then
        For i = 0 To pExtrémité.Length - 1
          If Math.Abs(Wpmin.X - pExtrémité(i).X) < 2 Then
            Wpmin.X -= 2
          End If
          If Math.Abs(Wpmin.Y - pExtrémité(i).Y) < 2 Then
            Wpmin.Y -= 2
          End If
          If Math.Abs(Wpmax.X - pExtrémité(i).X) < 2 Then
            Wpmax.X += 2
          End If
          If Math.Abs(Wpmax.Y - pExtrémité(i).Y) < 2 Then
            Wpmax.Y += 2
          End If
        Next
      End If

    End If

    AffecterPminPmax()

  End Sub

  Public ReadOnly Property Centre() As PointF
    Get
      Return Milieu(pMinFDP, pMaxFDP)
    End Get
  End Property

  Public ReadOnly Property Largeur() As Single
    Get
      Return mTailleF.Width
    End Get
  End Property

  Public ReadOnly Property Hauteur() As Single
    Get
      Return mTailleF.Height
    End Get
  End Property

  Private Sub AffecterPminPmax()
    pMinFDP = Wpmin
    pMaxFDP = Wpmax
    mTailleF = New SizeF(pMaxFDP.X - pMinFDP.X, Math.Abs(pMaxFDP.Y - pMinFDP.Y))
    'mCarrefour.mCentre = Centre
  End Sub

  'Private Property Largeur() As Single
  '  Get
  '    Return mLargeur
  '  End Get
  '  Set(ByVal Value As Single)
  '    mLargeur = Value
  '  End Set
  'End Property

  Private Function InitEchelle() As PointF
    Dim uneEchelle As Single
    Dim uneOrigine As PointF
    Dim Centre As PointF = New PointF((Wpmin.X + Wpmax.X) / 2, (Wpmin.Y + Wpmax.Y) / 2)

    xMaxPicture = 404
    yMaxPicture = 586
    uneEchelle = xMaxPicture / (Wpmax.X - Wpmin.X) * 0.95
    uneEchelle = Math.Min(uneEchelle, yMaxPicture / (Wpmax.Y - Wpmin.Y) * 0.95)
    uneOrigine.X = Centre.X - (xMaxPicture / 2 / uneEchelle)
    uneOrigine.Y = Centre.Y + (yMaxPicture / 2 / uneEchelle)

    mParamDessin = New ParamDessin(uneEchelle, uneOrigine)

    Return Centre

  End Function

  Public Function Libellé(Optional ByVal AjoutEtoile As Boolean = True) As String
    Libellé = mCarrefour.Nom
    If Not IsNothing(NomFichier) Then
      Libellé = IO.Path.GetFileNameWithoutExtension(NomFichier) & " - " & Libellé
    End If
    If mAEnregistrer AndAlso AjoutEtoile Then
      Libellé &= "*"
    End If
  End Function

  '******************************************************************************
  '(Re)Dimensionner le carrefour en fonction du nombre de branches
  '******************************************************************************
  Public Sub Dimensionner()
    Dim i As Short
    Dim nb As Short = mCarrefour.NbBranches
    Dim uneBranche As Branche
    'Centre du carrefour en pixels
    Dim uneLigne As Ligne
    Dim pO As PointF

    Try
      mBranches.Clear()
      Do Until mBranches.Count = mCarrefour.NbBranches
        uneBranche = New Branche(Me)
        mBranches.Add(uneBranche)
      Loop

      If Not ModeGraphique Then
        Dim dlg As New dlgModeTableur

        With dlg
          .maVariante = Me
          If .ShowDialog = DialogResult.OK Then
            .MettreAjour()
          End If
          .Dispose()
        End With

      End If

      For Each uneBranche In mBranches
        With uneBranche
          If Me.mCarrefour.CarrefourType = Carrefour.CarrefourTypeEnum.EnT Then
            Select Case mBranches.IndexOf(uneBranche)
              Case 0
                .Angle = 270
              Case 1
                .Angle = 0
              Case 2
                .Angle = 180
            End Select
          Else
            .Angle = (mBranches.IndexOf(uneBranche)) * 360 / nb
            If Me.mCarrefour.CarrefourType = Carrefour.CarrefourTypeEnum.EnY Then .Angle += 30
          End If
          'définition de l'origine par défaut de la branche : il s'agit de l'origine en coordonnées réelles
          'Positionnement de l'origine à 12m du centre du carrefour (initialisation par défaut)
          pO = PointPosition(New PointF(0, 0), 12, .Angle, SensHoraire:=True)
          .AttribuerOrigineRelative(pO)
          i += 1

          .NomRue = "Rue" & CStr(i)
        End With
      Next

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Dimensionnement de la variante")
    End Try

    If ModeGraphique Then
      mVerrou = Verrouillage.Aucun
    Else
      mVerrou = Verrouillage.Géométrie
    End If

  End Sub

  '********************************************************************************************************************
  ' Déterminer la branche qui suit immédiatement la branche dans le carrefour
  '********************************************************************************************************************
  Public Function BrancheSuivante(ByVal uneBranche As Branche) As Branche

    With mBranches
      Return .Suivante(uneBranche)
    End With

  End Function

  '********************************************************************************************************************
  ' Déterminer la branche qui précède immédiatement la branche dans le carrefour
  '********************************************************************************************************************
  Public Function BranchePrécédente(ByVal uneBranche As Branche) As Branche

    With mBranches
      Return .Précédente(uneBranche)
    End With

  End Function

  '********************************************************************************************************************
  ' Lors du verrouillage de la géométrie, DIAGFEUX crée par défaut une traversée piétonne pour chaque passage piéton
  ' L'utilisateur pourra ensuite regrouper les passages piétons par 2 pour créer une nouvelle traversée
  '********************************************************************************************************************
  Public Sub InitialiserTraversées(ByVal uneCollection As Graphiques)
    Dim uneBranche As Branche
    Dim unPassage As PassagePiéton
    Dim uneTraversée As TraverséePiétonne

    For Each unPassage In mPassagesEnAttente
      'Créer la traversée piétonne
      uneTraversée = CréerTraversée(unPassage, uneCollection)
    Next
    'For Each uneBranche In mBranches
    '  For Each unPassage In uneBranche.mPassages
    '    'Créer la traversée piétonne
    '    uneTraversée = CréerTraversée(unPassage, uneCollection)
    '  Next
    'Next

  End Sub

  '********************************************************************************************************************
  ' Suppprimer toutes les trajectoires (suite au déverrouillage de la géométrie
  '********************************************************************************************************************
  Public Sub SupprimerTrajectoires(ByVal uneCollection As Graphiques)
    Dim uneTrajectoire As Trajectoire
    Dim uneLigneFeux As LigneFeux

    'Supprimer les trajectoires
    For Each uneTrajectoire In mTrajectoires
      uneCollection.Remove(uneTrajectoire.mGraphique)
    Next

    mTrajectoires.Clear()

    'Supprimer les lignes de feux
    For Each uneLigneFeux In mLignesFeux
      uneCollection.Remove(uneLigneFeux.mGraphique)
    Next

    mLignesFeux.Clear()

  End Sub

  '********************************************************************************************************************
  ' Suppprimer une traversée piétonne comportant plusieurs passages piétons de la même branche
  '********************************************************************************************************************
  Public Sub DécomposerTraversée(ByVal uneTraversée As TraverséePiétonne, ByVal uneCollection As Graphiques)
    Dim unPassage As PassagePiéton

    For Each unPassage In uneTraversée.mPassages
      CréerTraversée(unPassage, uneCollection)
    Next

  End Sub

  '********************************************************************************************************************
  ' Créer une traversée comportant un seul passage piéton
  '********************************************************************************************************************
  Public Function CréerTraversée(ByVal unPassage As PassagePiéton, ByVal uneCollection As Graphiques) As TraverséePiétonne
    Dim uneTraversée As TraverséePiétonne = unPassage.mTraversée

    If Not IsNothing(uneTraversée) Then
      'Supprimer l'ancienne traversée associée au passage
      mTrajectoires.Remove(uneTraversée, uneCollection)
      'Supprimer également la ligne de feux associée
      mLignesFeux.Remove(uneTraversée.LigneFeu, uneCollection)
    End If

    'Créer la nouvelle traversée
    uneTraversée = New TraverséePiétonne(unPassage)
    CréerLienTraverséeLigneFeux(unPassage.mBranche, uneTraversée, uneCollection)
    Return uneTraversée

  End Function

  '********************************************************************************************************************
  ' Créer une traversée formée par les 2 passages piétons de la branche
  '********************************************************************************************************************
  Public Function CréerTraversée(ByVal uneBranche As Branche, ByVal uneCollection As Graphiques) As TraverséePiétonne
    Dim uneTraversée As TraverséePiétonne
    Dim colPassages As PassageCollection = uneBranche.mPassages

    Dim i As Short
    For i = 0 To 1
      uneTraversée = colPassages(i).mTraversée
      mTrajectoires.Remove(uneTraversée, uneCollection)
      If Not IsNothing(uneTraversée) Then
        mLignesFeux.Remove(uneTraversée.LigneFeu, uneCollection)
      End If
    Next

    'Créer la nouvelle traversée
    uneTraversée = New TraverséePiétonne(colPassages)
    CréerLienTraverséeLigneFeux(uneBranche, uneTraversée, uneCollection)
    Return uneTraversée
  End Function

  Private Sub CréerLienTraverséeLigneFeux(ByVal uneBranche As Branche, ByVal uneTraversée As TraverséePiétonne, ByVal uneCollection As Graphiques)
    mTrajectoires.Add(uneTraversée, uneCollection)
    'Associer une ligne de feux à la traversée
    Dim uneLigneFeux As New LigneFeuPiétons(Nothing, uneBranche, cndSignaux.DéfautPiéton)
    uneTraversée.LigneFeu = uneLigneFeux
    uneLigneFeux.mTrajectoires.Add(uneTraversée)
    mLignesFeux.Add(uneLigneFeux, uneCollection)
  End Sub

  '*****************************************************************************************************
  ' Intialise la collection des courants de circulation en fonction des branches entrantes et sortantes
  'Pour le mode graphique, cette fonction est appelée au verrouillage de la géométrie
  'Pour le mode tableur, cette fonction est appelée au verrouillage des lignes de feux
  '*****************************************************************************************************
  Public Sub InitialiserCourants()
    Dim BrancheOrigine, BrancheDestination As Branche
    Dim unCourant As Courant

    mCourants.Clear()

    For Each BrancheOrigine In mBranches
      If BrancheOrigine.NbVoies(Voie.TypeVoieEnum.VoieEntrante) > 0 Then
        BrancheOrigine.mCourants.Clear()
        BrancheDestination = mBranches.Suivante(BrancheOrigine)
        Do
          If BrancheDestination.NbVoies(Voie.TypeVoieEnum.VoieSortante) > 0 Then
            unCourant = New Courant(BrancheOrigine, BrancheDestination)
            BrancheOrigine.mCourants.Add(unCourant)
            mCourants.Add(unCourant)
          End If

          BrancheDestination = mBranches.Suivante(BrancheDestination)
        Loop Until BrancheDestination Is BrancheOrigine

        'For Each BrancheDestination In mBranches
        '  If Not BrancheOrigine Is BrancheDestination AndAlso BrancheDestination.NbVoies(Voie.TypeVoieEnum.VoieSortante) > 0 Then
        '    unCourant = New Courant(BrancheOrigine, BrancheDestination)
        '    BrancheOrigine.mCourants.Add(unCourant)
        '    mCourants.Add(unCourant)
        '  End If
        'Next
      End If
    Next

    If Not ModeGraphique Then
      mBranches.InitialiserCourants()
    End If

  End Sub

  '********************************************************************************************************************
  ' Enregistrer la variante dans le fichier
  ' Etape 1 : Créer les enregistrements nécessaires dans le DataSet DIAGFEUX
  ' uneRowCarrefour : si Renseigné, l'appel provient de Carrefour.Enregistrer, qui créera le fichier(Ne pas créer le fichier ici)
  '********************************************************************************************************************
  Public Function Enregistrer(Optional ByVal uneRowCarrefour As DataSetDiagfeux.CarrefourRow = Nothing) As Boolean

    Dim uneRowVariante As DataSetDiagfeux.VarianteRow

    Try
      ds = New DataSetDiagfeux

      uneRowCarrefour = mCarrefour.Enregistrer(Me)

      Dim uneRowParamétrage As DataSetDiagfeux.ParamétrageRow = ds.Paramétrage.NewParamétrageRow
      With uneRowParamétrage
        'Le fichier n'a peut-être pas le même niveau de version
        'On a attendu le 1er réenregistrement du fichier pour  mettre à niveau cette variable
        VersionFichier = [Global].VersionFichier
        .Version = VersionFichier
        .Organisme = Organisme
        .Service = Service
        .Logo = CheminLogo
        .VitessePiétons = VitessePiétons
        .VitesseVéhicules = VitesseVéhicules
        .VitesseVélos = VitesseVélos
        .DébitSaturation = DébitSaturation

        'v12 et antérieures
        '.VertUtile = DécalageVertUtile

        'v13
        .SignalPiétonsSonore = SignalPiétonsSonore

        'v13 et DiagFeux1
        '.TempsPerduDémarrage = TempsPerduDémarrage
        'If EnAgglo() Then
        '  .JauneInutiliséAgglo = JauneInutilisé
        '  .JauneInutiliséCampagne = cndParamètres.TempsJauneInutiliséCampagne
        'Else
        '  .JauneInutiliséCampagne = JauneInutilisé
        '  .JauneInutiliséAgglo = cndParamètres.TempsJauneInutiliséAgglo
        'End If

        'DiagFeux2 (sauf si DiagFeux1 est remplacé)
        If EnAgglo() Then
          .TempsPerduDémarrage = TempsPerduDémarrage
          .JauneInutiliséAgglo = JauneInutilisé
        Else
          .TempsPerduDémarrageCampagne = TempsPerduDémarrage
          .JauneInutiliséCampagne = JauneInutilisé
        End If
      End With
      ds.Paramétrage.AddParamétrageRow(uneRowParamétrage)

      Dim uneRowAffichage As DataSetDiagfeux.AffichageRow = ds.Affichage.NewAffichageRow
      ds.Affichage.AddAffichageRow(uneRowAffichage)
      With uneRowAffichage
        mNord.Enregistrer(uneRowAffichage)
        mSymEchelle.Enregistrer(uneRowAffichage)
        .SensTrajectoires = mSensTrajectoires
        .SensCirculation = mSensCirculation
      End With

      With ds.Variante
        'Ajouter une enregistrement dans la table des Variantes du carrefour
        If IsNothing(ScénarioCourant) Then
          uneRowVariante = .AddVarianteRow(Nothing, ModeGraphique, mVerrou, VertMiniVéhicules, VertMiniPiétons, uneRowCarrefour)
        Else
          If IsNothing(ScénarioDéfinitif) Then
            uneRowVariante = .AddVarianteRow(ScénarioCourant.Nom, ModeGraphique, mVerrou, VertMiniVéhicules, VertMiniPiétons, uneRowCarrefour)
          Else
            uneRowVariante = .AddVarianteRow(ScénarioCourant.Nom, ModeGraphique, mVerrou, VertMiniVéhicules, VertMiniPiétons, uneRowCarrefour)
          End If
        End If
      End With

      With mParamDessin
        Dim uneRowParamDessin As DataSetDiagfeux.ParamDessinRow
        uneRowParamDessin = ds.ParamDessin.AddParamDessinRow(.Echelle, uneRowVariante)
        ds.OrigineReelle.AddOrigineReelleRow(.OrigineRéelle.X, .OrigineRéelle.Y, uneRowParamDessin)
        ds.Taille.AddTailleRow(.TailleFenêtre.Width, .TailleFenêtre.Height, uneRowParamDessin)
      End With

      If Not IsNothing(mFondDePlan) Then
        mFondDePlan.Enregistrer(uneRowVariante)
      End If

      'Créer les branches
      Dim uneBranche As Branche
      'Enregistrer les origines de branches en relatif par rapport au centre du carrefour
      For Each uneBranche In mBranches
        uneBranche.Enregistrer(uneRowVariante)
      Next

      'Créer les lignes de feux
      mLignesFeux.Enregistrer(uneRowVariante)

      'Créer les trajectoires
      If mTrajectoires.Enregistrer(uneRowVariante) Then
        'Anomalie lors de l'enregistrement des trajectoires
        Enregistrer = True
      End If

      'Créer les trafics
      Dim unTrafic As Trafic
      For Each unTrafic In mTrafics
        unTrafic.Enregistrer(uneRowVariante)
      Next

      Dim unPlanFeuxBase As PlanFeuxBase
      For Each unPlanFeuxBase In mPlansFeuxBase
        unPlanFeuxBase.Enregistrer(Me, uneRowVariante)
      Next


      ds.WriteXml(NomFichier, XmlWriteMode.WriteSchema)

      'If CréerFichier Then
      '  ds.WriteXml(NomFichier, XmlWriteMode.WriteSchema)
      'End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Enregistrement du carrefour")
    End Try

  End Function

  Public Overloads Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Try
      uneCollection.Clear()
      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)

      mNord.CréerGraphique(uneCollection)
      mSymEchelle.CréerGraphique(uneCollection)

      'Dessiner les branches et
      'Créer l'enveloppe intérieure du carrefour en vue de son déplacement
      mGraphique = mBranches.CréerGraphique(uneCollection)
      mGraphique.ObjetMétier = Me

      If ModeGraphique Then
        'Dessiner les trajectoires et les antagonismes
        mTrajectoires.CréerGraphique(uneCollection)

        'Dessiner les lignes de feux 
        mLignesFeux.CréerGraphique(uneCollection)
        Verrouiller()

        If cndContexte = [Global].OngletEnum.Trafics Then
          'DessinerTrafics(uneCollection)
        End If
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.CréerGraphique")
    End Try

  End Function

  '*********************************************************************
  'Créer les objets graphiques spécifiques à une impression particulière
  ' unObjetMétier : - Carrefour (dessin de l'ensemble du  carrefour)
  '                 - Trafic 
  '                 - Phase 
  '                 - PlanFeux (vignette du pour le diagramme de phases)
  '                 - Variante (vignette pour le dessin de la matrice des rouges de dégagement)
  '*********************************************************************
  Public Overloads Function CréerGraphique(ByVal uneCollection As Graphiques, ByVal unObjetMétier As Métier) As PolyArc

    Try

      uneCollection.Clear()
      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)

      If TypeOf unObjetMétier Is Carrefour Then
        CréerGraphique(uneCollection)

      ElseIf TypeOf unObjetMétier Is Trafic Then
        Dim unTrafic As Trafic = CType(unObjetMétier, Trafic)
        mBranches.DessinerTrafic(uneCollection, unTrafic)

      ElseIf unObjetMétier Is Me Then
        'mini dessin du carrefour pour les rouges de dégagement
        mBranches.CréerGraphique(uneCollection)
        mLignesFeux.CréerGraphique(uneCollection)

      ElseIf TypeOf unObjetMétier Is PlanFeuxBase Then
        'mini dessin du carrefour pour le diagramme des phases
        mBranches.CréerGraphique(uneCollection)
        mTrajectoires.CréerGraphique(uneCollection)

      ElseIf TypeOf unObjetMétier Is Phase Then
        'Diagramme d'une phase
        mBranches.CréerGraphique(uneCollection)
        ' les branches n'ont été créées que pour s'appuyer dessus pour dessiner le diagramme
        mBranches.Masquer()
        CType(unObjetMétier, Phase).CréerGraphique(uneCollection)
        '        mLignesFeux.DessinerPhase(CType(unObjetMétier, Phase), uneCollection)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.CréerGraphique")

    End Try
  End Function

  Private Sub DessinerTrafics(ByVal uneCollection As Graphiques)
    Dim uneBranche As Branche
    For Each uneBranche In mBranches
      uneBranche.DessinerTrafics(uneCollection)
    Next
  End Sub

  Public ReadOnly Property Nord() As Nord
    Get
      Return mNord
    End Get
  End Property

  Public Overloads Sub Verrouiller()
    Dim GéométrieDéplaçable As Boolean = (cndContexte = [Global].OngletEnum.Géométrie And Verrou < [Global].Verrouillage.LignesFeux)

    'Le carrefour est toujours déplaçable dans l'onglet géométrie
    mGraphique.RendreSélectable(GéométrieDéplaçable)
    mBranches.Verrouiller(Verrouillage:=Not GéométrieDéplaçable)

    If ModeGraphique Then
      mTrajectoires.Verrouiller()
      mLignesFeux.Verrouiller()
      If Not IsNothing(ScénarioCourant) AndAlso Not IsNothing(ScénarioCourant.Antagonismes) Then
        ScénarioCourant.Antagonismes.Verrouiller()
      End If
    End If

  End Sub

  Public Function ListePlansFonctionnement() As PlanFeuxCollection
    Dim PlansFeux As New PlanFeuxCollection
    Dim unPlanFeuBase As PlanFeuxBase
    Dim unPlanFeuFonctionnement As PlanFeuxFonctionnement

    For Each unPlanFeuBase In mPlansFeuxBase
      For Each unPlanFeuFonctionnement In unPlanFeuBase.mPlansFonctionnement
        PlansFeux.Add(unPlanFeuFonctionnement)
      Next
    Next

    Return PlansFeux

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe VarianteCollection--------------------------
'=====================================================================================================
Public Class VarianteCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal uneVariante As Variante) As Short
    Add = Me.List.Add(uneVariante)
    If Not uneVariante.mCarrefour.mVariantes.Contains(uneVariante) Then
      uneVariante.mCarrefour.mVariantes.Add(uneVariante)
    End If
    Return Add
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal desVariantes() As Variante)
    Me.InnerList.AddRange(desVariantes)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal uneVariante As Variante)
    If Me.List.Contains(uneVariante) Then
      Me.List.Remove(uneVariante)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneVariante As Variante)
    Me.List.Insert(Index, uneVariante)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Variante
    Get
      Return CType(Me.List.Item(Index), Variante)
    End Get
  End Property

  Public Function IndexOf(ByVal uneVariante As Variante) As Short
    Return Me.List.IndexOf(uneVariante)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal uneVariante As Variante) As Boolean
    Return Me.List.Contains(uneVariante)
  End Function

  Public Function VarianteOuverte(ByVal NomFichier As String, Optional ByVal VarianteEnCours As Variante = Nothing) As Boolean
    Dim uneVariante As Variante

    For Each uneVariante In Me
      If uneVariante.NomFichier = NomFichier And Not uneVariante Is VarianteEnCours Then
        Return True
      End If
    Next
  End Function
End Class

'=====================================================================================================
'--------------------------- Structure Paramètres --------------------------
'=====================================================================================================
Public Structure Paramètres
  Public VersionFichier As Short
  Public Organisme As String
  Public Service As String
  Public CheminStockage As String
  Public CheminFDP As String
  Public CheminLogo As String
  Public VitessePiétons As Single
  Public VitesseVéhicules As Short
  Public VitesseVélos As Short
  Public DébitSaturation As Short
  'V12 et antérieures
  'Public DécalageVertUtile As Short
  'V13
  Public TempsPerduDémarrageAgglo As Short
  Public TempsPerduDémarrageCampagne As Short
  Public TempsJauneInutiliséAgglo As Short
  Public TempsJauneInutiliséCampagne As Short
  Public SignalPiétonsSonore As Boolean

  Public Sub New(ByVal vitessePiétons As Single, ByVal vitesseVéhicules As Short, ByVal vitesseVélos As Short, ByVal débit As Short, ByVal JauneAgglo As Short, ByVal JauneCampagne As Short, ByVal SignalPiétonsSonore As Boolean)
    'par défaut 0s pour : TempsPerduDémarrage
    ' Temps de jaune pour TempsJauneInutilisé 

    Me.VitessePiétons = vitessePiétons
    Me.VitesseVéhicules = vitesseVéhicules
    Me.VitesseVélos = vitesseVélos
    Me.DébitSaturation = débit

    'v13
    Me.TempsJauneInutiliséAgglo = JauneAgglo
    Me.TempsJauneInutiliséCampagne = JauneCampagne
    Me.SignalPiétonsSonore = SignalPiétonsSonore

  End Sub

  Public Sub New(ByVal Initial As Boolean)
    With cndParamètres
      VersionFichier = .VersionFichier
      Organisme = .Organisme
      Service = .Service
      CheminStockage = .CheminStockage
      CheminLogo = .CheminLogo
      VitessePiétons = .VitessePiétons
      VitesseVéhicules = .VitesseVéhicules
      VitesseVélos = .VitesseVélos
      DébitSaturation = .DébitSaturation
      'V12 et antérieures
      '      DécalageVertUtile = .DécalageVertUtile
      'V13
      TempsPerduDémarrageAgglo = .TempsPerduDémarrageAgglo
      TempsPerduDémarrageCampagne = .TempsPerduDémarrageCampagne
      TempsJauneInutiliséAgglo = .TempsJauneInutiliséAgglo
      TempsJauneInutiliséCampagne = .TempsJauneInutiliséCampagne
      SignalPiétonsSonore = .SignalPiétonsSonore
    End With
  End Sub

  Public ReadOnly Property IsEmpty() As Boolean
    Get
      Return (VersionFichier = 0)
    End Get
  End Property
End Structure
