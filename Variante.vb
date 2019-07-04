'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Variante.vb																							'
'						Classes																														'
'							Variante																												'
'							VarianteCollection  : collection de variantes 									'
'           Structure
'             Param�tres
'******************************************************************************
Option Strict Off
Option Explicit On 

'
'=====================================================================================================
'--------------------------- Classe Variante --------------------------
'=====================================================================================================
Public Class Variante : Inherits M�tier

  Public mParamDessin As ParamDessin
  'Taille du carrefour (repr�sentant son encombrement en m�tres pour pouvoir le dessiner)
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
  Private mSc�narioCourant As PlanFeuxBase
  Private mSc�narioD�finitif As PlanFeuxBase

  Public VertMiniV�hicules As Short = [Global].VertMiniV�hicules
  Public VertMiniPi�tons As Short = [Global].VertMiniPi�tons

  Private mVerrou As Verrouillage = Verrouillage.Aucun
  Private mSensTrajectoires As Boolean
  Private mSensCirculation As Boolean

  Public NomFichier As String
  Public mDataSet As DataSetDiagfeux

  Private mParam�tres As New Param�tres(Initial:=True)

  Private mAEnregistrer As Boolean

  Private mNord As New Nord
  Private mSymEchelle As New SymEchelle

  Public Property Sc�narioCourant() As PlanFeuxBase
    Get
      Return mSc�narioCourant
    End Get
    Set(ByVal Value As PlanFeuxBase)
      mSc�narioCourant = Value
    End Set
  End Property

  Public Function Sc�narioEnCours() As Boolean
    Return Not IsNothing(mSc�narioCourant)
  End Function

  Public Property Sc�narioD�finitif() As PlanFeuxBase
    Get
      Return mSc�narioD�finitif
    End Get
    Set(ByVal Value As PlanFeuxBase)
      mSc�narioD�finitif = Value
    End Set
  End Property

  Public Sub Cr�erSc�nario(ByVal nomSc�nario As String, ByVal AvecTrafic As Boolean)
    Dim unTrafic As Trafic
    Dim unPlanBase As PlanFeuxBase

    With mPlansFeuxBase
      If AvecTrafic Then
        'Sc�nario avec trafic
        unTrafic = New Trafic(Me)
        unTrafic.Nom = nomSc�nario
        mTrafics.Add(unTrafic)
        unPlanBase = .Item(.Add(New PlanFeuxBase(unTrafic)))

      Else
        'Sc�nario sans trafic
        unPlanBase = .Item(.Add(New PlanFeuxBase(nomSc�nario)))
      End If
    End With

    If Verrou >= [Global].Verrouillage.LignesFeux Then
      'Si le verrou est sup�rieur � LignesFeux, c'est qu'au moins un sc�nario est dans cet �tat
      ' donc les lignes de feux sont verrouill�es 
      unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
      'Initialiser les rouges de d�gagement avec les valeurs mini
      unPlanBase.mLignesFeux.InitialiserTempsD�gagement(mLignesFeux)
    Else
      unPlanBase.Verrou = [Global].Verrouillage.G�om�trie
    End If

    mSc�narioCourant = unPlanBase


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

  Public Property Param() As Param�tres
    Get
      Return mParam�tres
    End Get
    Set(ByVal Value As Param�tres)
      mParam�tres = Value
    End Set
  End Property

  Public Property VitessePi�tons() As Single
    Get
      Return mParam�tres.VitessePi�tons
    End Get
    Set(ByVal Value As Single)
      mParam�tres.VitessePi�tons = Value
    End Set
  End Property

  Public Property VitesseV�hicules() As Single
    Get
      Return mParam�tres.VitesseV�hicules

    End Get
    Set(ByVal Value As Single)
      mParam�tres.VitesseV�hicules = Value
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

  Property NordAffich�() As Boolean
    Get
      Return mNord.Affich�
    End Get
    Set(ByVal Value As Boolean)
      mNord.Affich� = Value
      mdiApplication.mnuNord.Checked = Value
    End Set
  End Property

  Property EchelleAffich�e() As Boolean
    Get
      Return mSymEchelle.Affich�
    End Get
    Set(ByVal Value As Boolean)
      mSymEchelle.Affich� = Value
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

  Public Property VitesseV�los() As Single
    Get
      Return mParam�tres.VitesseV�los

    End Get
    Set(ByVal Value As Single)
      mParam�tres.VitesseV�los = Value
    End Set
  End Property

  Public Property D�bitSaturation() As Short
    Get
      Return mParam�tres.D�bitSaturation
    End Get
    Set(ByVal Value As Short)
      mParam�tres.D�bitSaturation = Value
    End Set
  End Property

  Public ReadOnly Property TempsPerduParPhase() As Short
    'R�f�rence : Guide Carrefour du CERTU (�3.5 p31)
    Get
      Return TempsInutilis� + MiniRougeD�gagement
    End Get
  End Property

  Private ReadOnly Property TempsInutilis�() As Short
    Get
      Return TempsPerduD�marrage + JauneInutilis�
    End Get
  End Property

  Public Property TempsPerduD�marrage() As Short
    Get
      If EnAgglo() Then
        Return mParam�tres.TempsPerduD�marrageAgglo
      Else
        Return mParam�tres.TempsPerduD�marrageCampagne
      End If
    End Get
    Set(ByVal Value As Short)
      If EnAgglo() Then
        mParam�tres.TempsPerduD�marrageAgglo = Value
      Else
        mParam�tres.TempsPerduD�marrageCampagne = Value
      End If
    End Set
  End Property

  Public Property JauneInutilis�() As Short
    Get
      If EnAgglo() Then
        Return mParam�tres.TempsJauneInutilis�Agglo
      Else
        Return mParam�tres.TempsJauneInutilis�Campagne
      End If
    End Get
    Set(ByVal Value As Short)
      If EnAgglo() Then
        mParam�tres.TempsJauneInutilis�Agglo = Value
      Else
        mParam�tres.TempsJauneInutilis�Campagne = Value
      End If
    End Set
  End Property

  Public ReadOnly Property JauneV�hicules() As Short
    Get
      If EnAgglo() Then
        Return [Global].JauneAgglo
      Else
        Return [Global].JauneCampagne
      End If
    End Get
  End Property

  Public ReadOnly Property D�calageVertUtile() As Short
    Get
      Return JauneV�hicules - TempsInutilis�
    End Get
  End Property

  Public ReadOnly Property strVertUtile() As String
    Get
      Dim chaine As String

      If D�calageVertUtile <> 0 Then

        If D�calageVertUtile > 0 Then
          chaine = "+"
        End If

        chaine &= D�calageVertUtile

        If Math.Abs(D�calageVertUtile) > 1 Then
          chaine &= " secondes"
        Else
          chaine &= " seconde"
        End If
      End If

      Return chaine
    End Get
  End Property

  Public Property SignalPi�tonsSonore() As Boolean
    Get
      Return mParam�tres.SignalPi�tonsSonore
    End Get
    Set(ByVal Value As Boolean)
      mParam�tres.SignalPi�tonsSonore = Value
    End Set
  End Property

  Public Property VersionFichier() As Short
    Get
      Return mParam�tres.VersionFichier
    End Get
    Set(ByVal Value As Short)
      mParam�tres.VersionFichier = Value
    End Set
  End Property

  Public Property Organisme() As String
    Get
      Return mParam�tres.Organisme
    End Get
    Set(ByVal Value As String)
      mParam�tres.Organisme = Value
    End Set
  End Property

  Public Property Service() As String
    Get
      Return mParam�tres.Service
    End Get
    Set(ByVal Value As String)
      mParam�tres.Service = Value
    End Set
  End Property

  Public Property CheminLogo() As String
    Get
      Return mParam�tres.CheminLogo
    End Get
    Set(ByVal Value As String)
      mParam�tres.CheminLogo = Value
    End Set
  End Property

  Friend Function OngletInterdit(ByVal Index As OngletEnum) As Boolean
    Dim Message As String = ""

    Select Case Index
      Case OngletEnum.G�om�trie
      Case OngletEnum.LignesDeFeux
        If Verrou < Verrouillage.G�om�trie Then
          Message = "Il faut d'abord verrouiller la g�om�trie"
        End If

      Case OngletEnum.Trafics
        If Verrou < Verrouillage.G�om�trie Then
          Message = "Il faut d'abord verrouiller la g�om�trie"
        ElseIf Verrou < [Global].Verrouillage.LignesFeux And Not ModeGraphique Then
          Message = "Il faut d'abord verrouiller les lignes de feux"
        End If

      Case OngletEnum.Conflits
        If Verrou < Verrouillage.G�om�trie Then
          Message = "Il faut d'abord verrouiller la g�om�trie"
        ElseIf Verrou < Verrouillage.LignesFeux Then
          Message = "Il faut d'abord verrouiller les lignes de feux"
        ElseIf Not Sc�narioEnCours() Then
          Message = "Il faut d'abord cr�er un sc�nario"
        ElseIf Sc�narioCourant.AvecTrafic AndAlso Not Sc�narioCourant.Trafic.Verrouill� Then
          Message = "Il faut d'abord verrouiller la p�riode de trafic"
        End If

      Case OngletEnum.PlansDeFeux
        If Verrou < Verrouillage.G�om�trie Then
          Message = "Il faut d'abord verrouiller la g�om�trie"
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

  Public Property ConflitsInitialis�s() As Boolean
    Get
      If Not IsNothing(Sc�narioCourant) Then
        Return Sc�narioCourant.ConflitsInitialis�s
      End If
    End Get
    Set(ByVal Value As Boolean)
      Dim unSc�nario As PlanFeuxBase
      If Value Then
        Sc�narioCourant.ConflitsInitialis�s = True
      Else
        'Le verrouillage des lignes de feux doit r�initialiser les conflits de tous les sc�narios
        For Each unSc�nario In mPlansFeuxBase
          unSc�nario.ConflitsInitialis�s = False
        Next
      End If

    End Set
  End Property

  Friend Property Verrou() As Verrouillage
    Get
      If IsNothing(mSc�narioCourant) Then
        Return mVerrou
      Else
        Return mSc�narioCourant.Verrou
      End If
    End Get

    Set(ByVal Value As Verrouillage)
      Dim unSc�nario As PlanFeuxBase

      If IsNothing(Sc�narioCourant) Then
        mVerrou = Value
      Else
        Select Case Value
          Case [Global].Verrouillage.Aucun  ' D�verrouillage de la g�om�trie
            mVerrou = Value

            'Supprimer tous les sc�narios et les trafics
            mPlansFeuxBase.Clear()
            mTrafics.Clear()
            Sc�narioCourant = Nothing

          Case [Global].Verrouillage.G�om�trie
            mVerrou = Value
            'On vient peut-�tre de d�verrouiller les lignes de feux : redescendre tous les sc�narios au niveau G�om�trie
            For Each unSc�nario In mPlansFeuxBase
              unSc�nario.Verrou = Value
            Next

          Case [Global].Verrouillage.LignesFeux
            mVerrou = Value
            For Each unSc�nario In mPlansFeuxBase
              If unSc�nario.Verrou < [Global].Verrouillage.LignesFeux Then
                'On vient de verrouiller les lignes de feux : faire monter tous les sc�narios(en fait avec trafic) � ce niveau
                unSc�nario.Verrou = Value
              ElseIf unSc�nario Is Sc�narioCourant Then
                'C'est ce plan de feux 
                unSc�nario.Verrou = Value
              End If
            Next

          Case Else
            'Le verrouillage ne concerne que le sc�nario en cours : la variante elle-m�me n'ira jamais au-del� du verrou Lignesde feux
            Sc�narioCourant.Verrou = Value
        End Select
      End If
    End Set
  End Property

  Friend ReadOnly Property PlansPourPhasage() As PlanFeuxCollection
    Get
      Return mSc�narioCourant.PlansPourPhasage
    End Get
  End Property

  Public Property TroisPhasesSeulement() As Boolean
    Get
      Return mSc�narioCourant.mTroisPhasesSeulement
    End Get
    Set(ByVal Value As Boolean)
      mSc�narioCourant.mTroisPhasesSeulement = Value
    End Set
  End Property
  'Indique si la g�om�trie est verrouill�e
  Public ReadOnly Property VerrouG�om() As Boolean
    Get
      VerrouG�om = (Verrou >= Verrouillage.G�om�trie)
    End Get
  End Property

  'Indique si le sch�ma de circulation et les lignes de feux sont verrouill�es
  '##ModelId=4032294402CE
  Public ReadOnly Property VerrouLigneFeu() As Boolean
    Get
      VerrouLigneFeu = (Verrou >= Verrouillage.LignesFeux)
    End Get
  End Property

  'Indique si les matrices de s�curit� sont verrouill�es
  Public ReadOnly Property VerrouMatrices() As Boolean
    Get
      VerrouMatrices = (Verrou >= Verrouillage.Matrices)
    End Get
  End Property

  'Indique si le plan de feux de base est verrouill�
  Public ReadOnly Property VerrouFeuBase() As Boolean
    Get
      VerrouFeuBase = (Verrou = Verrouillage.PlanFeuBase)
    End Get
  End Property

  '********************************************************************************************************************
  ' Verrouiller/D�verrouiller une �tape
  ' Incr�ment = +1 ou -1  selon que l'on avance ou que l'on recule dans une �tape
  '********************************************************************************************************************
  Public Sub BasculerVerrou(ByVal chk As CheckBox)

    With chk
      Select Case .Name
        Case "chkVerrouG�om�trie"
          If .Checked Then
            Verrou = [Global].Verrouillage.G�om�trie
          Else
            Verrou = [Global].Verrouillage.Aucun
          End If
        Case "chkVerrouLignesFeux"
          If .Checked Then
            Verrou = [Global].Verrouillage.LignesFeux
          Else
            Verrou = [Global].Verrouillage.G�om�trie
          End If
        Case "chkVerrouMatrice"
          If .Checked Then
            If Verrou = [Global].Verrouillage.LignesFeux Then
              'On bascule le verrou pour la premi�re fois pour le sc�nario courant
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
      ' Remettre � 0 les objets �ventuellement cr��s lors d'une variante pr�c�dente
      Select Case mVerrou
        Case Verrouillage.G�om�trie
          Me.mLignesFeux.Clear()
          Me.mTrajectoires.Clear()
      End Select
    End If

  End Sub

  '************************************************************************************
  ' D�termine si on peut poser le verrou imm�diatement sup�rieur � l'actuel
  '************************************************************************************
  Public Function NonVerrouillable() As M�tier
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxV�hicules As LigneFeuV�hicules
    Dim uneBranche, uneBranche2 As Branche
    Dim uneVoie As Voie
    Dim uneTrajectoire As Trajectoire
    Dim msg As String
    Dim VerrouAPoser As Verrouillage = Verrou + 1

    Try
      Select Case VerrouAPoser
        Case [Global].Verrouillage.G�om�trie
          For Each uneBranche In mBranches
            If uneBranche.mPassages.Count = 2 AndAlso _
            (uneBranche.SensUnique(Voie.TypeVoieEnum.VoieEntrante) Or uneBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante)) Then
              NonVerrouillable = uneBranche
              msg = "Branche " & mBranches.ID(uneBranche) & " : " & uneBranche.NomRue & vbCrLf & _
                    "Une branche � sens unique ne peut comporter qu'un seul passage pi�ton"
              Exit For
            End If

          Next

        Case [Global].Verrouillage.LignesFeux
          ' Verrouillage des lignes de feux

          If ModeGraphique Then

            'V�rifier que toutes les voies entrantes sont l'origine d'au moins une trajectoire
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
              'V�rifier que toutes les trajectoires sont command�es par une ligne de feux
              For Each uneTrajectoire In mTrajectoires
                If uneTrajectoire.EstV�hicule AndAlso IsNothing(uneTrajectoire.LigneFeu) Then
                  uneBranche = CType(uneTrajectoire, TrajectoireV�hicules).mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine)
                  uneBranche2 = CType(uneTrajectoire, TrajectoireV�hicules).mBranche(TrajectoireV�hicules.OrigineDestEnum.Destination)
                  msg = "Trajectoire depuis la branche " & mBranches.ID(uneBranche) & " : " & uneBranche.NomRue & _
                        " vers la branche " & mBranches.ID(uneBranche2) & " : " & uneBranche2.NomRue & vbCrLf & _
                        "Toutes les trajectoires doivent �tre command�es par une ligne de feux"
                  NonVerrouillable = uneTrajectoire
                  Exit For
                End If
              Next
            End If

          Else
            'Mode tableur : en mode graphique, l'initialisation des courants est faite au verrouillage de la g�om�trie
            InitialiserCourants()

            For Each uneLigneFeux In mLignesFeux
              If uneLigneFeux.EstV�hicule Then
                uneLigneFeuxV�hicules = uneLigneFeux
                With uneLigneFeuxV�hicules
                  If .NbVoiesTableur = 0 Then
                    'Possible en mode non graphique 
                    msg = "Indiquer au moins une voie par ligne de feux"
                  ElseIf Not (.TAD Or .TAG Or .TD) Then
                    msg = "Indiquer au moins un courant directionnel par ligne de feux"
                  End If
                  If Not IsNothing(msg) Then
                    NonVerrouillable = uneLigneFeuxV�hicules
                    Exit For
                  ElseIf .D�terminerCourants Then
                    NonVerrouillable = .mBranche
                    msg = "Impossible d'affecter les courants issus de la la branche " & mBranches.ID(.mBranche)
                  End If
                End With
              End If
            Next

            If IsNothing(NonVerrouillable) Then
              uneBranche = mBranches.D�terminerCourants()
              If Not IsNothing(uneBranche) Then
                msg = "Impossible d'affecter les courants sur la branche " & mBranches.ID(uneBranche)
                NonVerrouillable = uneBranche
              End If
            End If

          End If

        Case [Global].Verrouillage.Matrices
          ' Verrouillage de la matrice des conflits
          'V�rifier si les antagonismes sont r�solus
          If ModeGraphique AndAlso AntagonismesCourants.NbConflitsAR�soudre > 0 Then
            msg = "Tous les conflits n'ont pas �t� r�solus"
            NonVerrouillable = AntagonismesCourants(0)

          Else
            'V�rifier si au moins un phasage est possible
            Sc�narioCourant.ConstruirePlansDePhasage()
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

  Public ReadOnly Property Libell�Verrouillage() As String
    Get
      Select Case Verrou
        Case Verrouillage.Aucun
          Libell�Verrouillage = ""
        Case Verrouillage.G�om�trie
          Libell�Verrouillage = "G�om�trie verrouill�e"
        Case Verrouillage.LignesFeux
          Libell�Verrouillage = "Lignes de feux verrouill�es"
        Case Verrouillage.Matrices
          Libell�Verrouillage = "Matrice des conflits verrouill�e"
        Case Verrouillage.PlanFeuBase
          Libell�Verrouillage = "Plan de feux de base verrouill�"
      End Select

    End Get
  End Property

  Public ReadOnly Property PhasageRetenu() As Boolean
    '//DIAGFEUX//
    Get
      If Sc�narioEnCours() Then
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
  Public ReadOnly Property DiagnosticCalcul�() As Boolean
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

  Public Sub R�ordonnerPlansFeux()
    Dim unSc�nario As PlanFeuxBase
    Dim uneLigneFeux As LigneFeux

    For Each unSc�nario In mPlansFeuxBase
      If Not unSc�nario.PhasageRetenu Then
        unSc�nario.mLignesFeux.Clear()
        For Each uneLigneFeux In mLignesFeux
          unSc�nario.mLignesFeux.Add(uneLigneFeux)
        Next
      End If
    Next

  End Sub

  Public ReadOnly Property mPlanFeuxBase() As PlanFeuxBase
    Get

      Return Sc�narioCourant

    End Get
  End Property

  Public ReadOnly Property Antagonismes() As AntagonismeCollection
    Get
      Return mTrajectoires.Antagonismes
    End Get
  End Property

  Public ReadOnly Property AntagonismesCourants() As AntagonismeCollection
    Get
      Return Sc�narioCourant.Antagonismes
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
  ' D�terminer les points de conflit entre tous les couples de trajectoires
  ' Cette fonction est appel�e une seule fois : lors du verrouillage des lignes de feux
  '********************************************************************************************************************
  Public Sub VerrouillerLignesFeux(ByVal Verrouillage As Boolean, ByVal uneCollection As Graphiques, Optional ByVal ChargementEnCours As Boolean = False)
    Dim unPlanFeuxBase As PlanFeuxBase

    If Not ChargementEnCours Then
      mLignesFeux.Dimensionner(RemiseAZ�ro:=Not Verrouillage)
    End If

    If ModeGraphique Then
      If Verrouillage And Not ChargementEnCours Then mTrajectoires.D�terminerConflits()
      mTrajectoires.Verrouiller()

      If Verrouillage Then
        InitialiserTempsD�gagement()
      End If

      If Not ChargementEnCours Then
        D�terminerCourants(Verrouillage)

        For Each unPlanFeuxBase In mPlansFeuxBase
          'Cr�erAntagonismes cr�e �galement la collection des lignes de feux du plan de feux de base
          unPlanFeuxBase.Cr�erAntagonismes()
          unPlanFeuxBase.Antagonismes.Cr�erGraphique(uneCollection)
        Next
      End If

      mLignesFeux.Verrouiller()

    Else
      For Each unPlanFeuxBase In mPlansFeuxBase
        'L'instruction qui suit ne cr�e en fait que les lignes de feux
        unPlanFeuxBase.Cr�erAntagonismes()
      Next

    End If

    If Verrouillage Then
      For Each unPlanFeuxBase In mPlansFeuxBase
        If unPlanFeuxBase.Verrou >= [Global].Verrouillage.Matrices Then
          unPlanFeuxBase.ConstruirePlansDePhasage()
          unPlanFeuxBase.CalculerCapacit�sPlansPhasage()
        End If
      Next

    Else
      Antagonismes.Clear()
      For Each unPlanFeuxBase In mPlansFeuxBase
        unPlanFeuxBase.ConflitsInitialis�s = False
      Next
      If Not ModeGraphique Then
        mLignesFeux.R�initialiserVoies()
      End If
    End If

  End Sub

  Public Sub InitialiserTempsD�gagement()
    Dim unPlanFeuxBase As PlanFeuxBase

    If ModeGraphique Then
      mLignesFeux.D�terminerTempsD�gagement()
    End If

  End Sub

  Public Sub R�initialiserPhasages()
    Dim unPlanBase As PlanFeuxBase

    InitialiserTempsD�gagement()
    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.R�initialiserPhasage()
    Next
  End Sub

  Public Sub R�initialiserCapacit�s()
    Dim unPlanBase As PlanFeuxBase

    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.Capacit�ACalculer = True
    Next

  End Sub

  '*************************************************************************************************
  ' D�terminer les courants de la variante
  ' VerrouillageLignesFeux : Indique si on est en train de verrouiller ou non les lignes de feux
  '*************************************************************************************************
  Private Sub D�terminerCourants(ByVal VerrouillageLignesFeux As Boolean)
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneV�hicules As LigneFeuV�hicules
    Dim uneBranche As Branche
    Dim uneVoie As Voie
    Dim nbVoiesEntrantes(mBranches.Count - 1) As Short
    Dim i As Short

    If VerrouillageLignesFeux Then
      'D�terminer les courants command�s par chaque voie de chaque ligne de feux v�hicules

      For Each uneLigneFeux In mLignesFeux
        If uneLigneFeux.EstV�hicule Then
          With CType(uneLigneFeux, LigneFeuV�hicules)
            If ModeGraphique Then
              mTrajectoires.D�terminerCourants(.Voies)
            Else
              .D�terminerCourants()
              i = mBranches.IndexOf(.mBranche)
              nbVoiesEntrantes(i) += .NbVoiesTableur
            End If
          End With
        End If
      Next

      If Not ModeGraphique Then
        '!!!!!!!!!!!!!!!!DIAFEUX 3!!!!!!!!!!!!!!!!!!!!!!!
        'Les lignes qui suivent sont  supprim�es dans la version d�finitive
        'elles permettaient de r�cup�rer d'anciens fichiers tableur avec un nombre de voies entrantes non en concordance avec les voies des LF
        'For i = 0 To nbVoiesEntrantes.Length - 1
        '  mBranches(CType(i, Short)).NbVoies(Voie.TypeVoieEnum.VoieEntrante) = nbVoiesEntrantes(i)
        'Next

        mBranches.D�terminerCourants()
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
  'R�initialiser les lignes de feux suite au d�verrouillage de celles-ci
  '********************************************************************************************************************
  Private Sub R�initialiserLignesFeux()
    Dim unPLanFeuxBase As PlanFeuxBase

    'Toutes les lignes de feux sont � nouveau compatibles
    mLignesFeux.R�initialiserIncompatibilit�s()

    'Supprimer les lignes de feux des plans de feux de base
    For Each unPLanFeuxBase In mPlansFeuxBase
      unPLanFeuxBase.mLignesFeux.Clear()
    Next
  End Sub


  'Le carrefour est compos� s'il y a plusieurs plans de feux de base (partition de l'ensemble des lignes de feux)
  '##ModelId=3C8B6B4A0251
  Public Function EstCompos�() As Boolean

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

    Dim uneRowParam�trage As DataSetDiagfeux.Param�trageRow
    If ds.Param�trage.Rows.Count > 0 Then
      uneRowParam�trage = ds.Param�trage.Rows(0)
      With uneRowParam�trage
        Try
          VersionFichier = .Version
        Catch ex As StrongTypingException
          'Version non renseign�e : < v13
          VersionFichier = 0
          TempsPerduD�marrage = 0
          'Par d�faut : Jaune utilis� = 0
          JauneInutilis� = JauneV�hicules
          SignalPi�tonsSonore = True
        End Try
        Organisme = .Organisme
        Service = .Service
        If Not .IsLogoNull Then
          CheminLogo = .Logo
        End If
        VitessePi�tons = .VitessePi�tons
        VitesseV�hicules = .VitesseV�hicules
        VitesseV�los = .VitesseV�los
        D�bitSaturation = .D�bitSaturation
        'v12 et ant�rieures
        'D�calageVertUtile = .VertUtile

        'v13
        If VersionFichier > 0 Then
          SignalPi�tonsSonore = .SignalPi�tonsSonore
        End If

        'v13 et DiagFeux 1
        'If VersionFichier > 0 Then
        '  TempsPerduD�marrage = .TempsPerduD�marrage
        '  mParam�tres.TempsJauneInutilis�Agglo = .JauneInutilis�Agglo
        '  mParam�tres.TempsJauneInutilis�Campagne = .JauneInutilis�Campagne
        'End If

        'DiagFeux2 (sauf si DiagFeux1 n'est pas conserv�)

        Select Case VersionFichier
          Case 1
            TempsPerduD�marrage = .TempsPerduD�marrage
            If EnAgglo() Then
              JauneInutilis� = .JauneInutilis�Agglo
            Else
              JauneInutilis� = .JauneInutilis�Campagne
            End If
          Case Is > 1
            If EnAgglo() Then
              TempsPerduD�marrage = .TempsPerduD�marrage
              JauneInutilis� = .JauneInutilis�Agglo
            Else
              TempsPerduD�marrage = .TempsPerduD�marrageCampagne
              JauneInutilis� = .JauneInutilis�Campagne
            End If

        End Select

        'Alimenter la valeur de JauneInutilis� qui ne sert pas avec les valeurs par d�faut du poste de travail(a priori sans int�r�t)
        If EnAgglo() Then
          mParam�tres.TempsJauneInutilis�Campagne = cndParam�tres.TempsJauneInutilis�Campagne
        Else
          mParam�tres.TempsJauneInutilis�Agglo = cndParam�tres.TempsJauneInutilis�Agglo
        End If
      End With  ' uneRowParam�trage
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
      VertMiniPi�tons = .VertMiniPi�tons
      VertMiniV�hicules = .VertMiniV�hicules

      If .GetParamDessinRows.Length = 1 Then
        Dim uneRowParamDessin As DataSetDiagfeux.ParamDessinRow = .GetParamDessinRows(0)
        With uneRowParamDessin
          mParamDessin.Echelle = .Echelle
          mParamDessin.OrigineR�elle = New PointF(.GetOrigineReelleRows(0).X, .GetOrigineReelleRows(0).Y)
          If .GetTailleRows.Length = 1 Then
            Dim uneTaille As Size
            uneTaille = New Size(.GetTailleRows(0).X, .GetTailleRows(0).Y)
            'R�cup�rer la taille de la fen�tre lors de l'enregistrement sauf si celle-ci est + grande que l'�cran
            If uneTaille.Width <= mdiApplication.ClientSize.Width And uneTaille.Height <= mdiApplication.ClientSize.Height Then
              mParamDessin.TailleFen�tre = uneTaille
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
              'Le fond de plan initial a �t� d�plac� : il faudra enregistrer avec le nouveau chemin
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
        Dim unPassage As PassagePi�ton
        uneBranche = New Branche(.GetBrancheRows(i), Me)
        mBranches.Add(uneBranche)
        For Each unPassage In uneBranche.mPassages
          mPassagesEnAttente.Add(unPassage)
        Next
      Next

      'Initialiser les courants de circulation (ils seront r�initialis�s + tard si c'est pr�matur�)
      InitialiserCourants()

      'Lire les lignes de feux
      For i = 0 To .GetLigneDeFeuxRows.Length - 1
        Dim uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow = .GetLigneDeFeuxRows(i)
        unSignal = cndSignaux(uneRowLigneDeFeux.GetSignalRows(0).strCode)
        If unSignal.EstPi�ton Then
          uneLigneFeux = New LigneFeuPi�tons(uneRowLigneDeFeux)
        Else
          uneLigneFeux = New LigneFeuV�hicules(uneRowLigneDeFeux)
        End If
        mLignesFeux.Add(uneLigneFeux)
      Next


      'Cr�er les incompatibilit�s et les temps de rouge de d�gagement entre les lignes de feux
      If Verrou >= [Global].Verrouillage.LignesFeux Then
        mLignesFeux.Cr�erIncompatibilit�s(.GetLigneDeFeuxRows)
      End If

      'Lire les trajectoires
      Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow
      For i = 0 To .GetTrajectoireRows.Length - 1
        uneRowTrajectoire = .GetTrajectoireRows(i)
        If uneRowTrajectoire.GetPi�tonsRows.Length = 1 Then
          'Travers�e pi�tonne
          uneTrajectoire = New Travers�ePi�tonne(uneRowTrajectoire)
        Else
          'Trajectoire v�hicules
          uneTrajectoire = New TrajectoireV�hicules(uneRowTrajectoire)
        End If
        Try
          uneTrajectoire.LigneFeu = mLignesFeux(uneRowTrajectoire.IDLigneDeFeux)
          uneTrajectoire.LigneFeu.mTrajectoires.Add(uneTrajectoire)
          If uneTrajectoire.EstV�hicule Then
            CType(uneTrajectoire.LigneFeu, LigneFeuV�hicules).AjouterBrancheSortie(uneTrajectoire)
          End If
        Catch ex As StrongTypingException
          'IDLigneDeFeux est DBNull : aucune ligne de feux n'est encore associ�e � la trajectoire 
        Catch ex As System.NullReferenceException
          'IDLigneDeFeux ne correspond � aucune ligne de feux
          Throw New DiagFeux.Exception("La ligne de feux " & uneRowTrajectoire.IDLigneDeFeux & " associ�e � la trajectoire n'existe pas")
        Catch ex As System.Exception
          Throw New DiagFeux.Exception(ex.ToString)
        End Try
        mTrajectoires.Add(uneTrajectoire)
      Next

      If mVerrou >= [Global].Verrouillage.LignesFeux Then
        D�terminerCourants(VerrouillageLignesFeux:=True)
        If ModeGraphique Then
          ' Lire les antagonismes
          mTrajectoires.Cr�erAntagonismes(uneRowVariante)
        Else
          mBranches.D�terminerCourants()
        End If
      End If

      If ModeGraphique Then
        For Each uneLigneFeux In mLignesFeux
          If uneLigneFeux.EstV�hicule Then
            CType(uneLigneFeux, LigneFeuV�hicules).D�terminerNatureCourants(mTrajectoires)
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
            Sc�narioCourant = mPlansFeuxBase(CType(0, Short))
          End If
        Else
          Sc�narioCourant = mPlansFeuxBase(.PlanFeuxCourant)
        End If

      Else
        'Projet ACONDIA
        Sc�narioCourant = Sc�narioAcondia(.GetLigneDeFeuxRows)
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
      mParamDessin = New ParamDessin(D�fautEchelle, D�fautOrigine)
    End If

    cndParamDessin = mParamDessin

  End Sub

  Private Function Sc�narioAcondia(ByVal desRowLignesDeFeux As DataSetDiagfeux.LigneDeFeuxRow()) As PlanFeuxBase
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
      'Cr�er les incompatibilit�s et les temps de rouge de d�gagement entre les lignes de feux
      'pfBase.mLignesFeux.Cr�erIncompatibilit�s(desRowLignesDeFeux)

      If mTrafics.Count = 0 Then
        pfBase.Nom = "Sc�nario sans trafic"

      Else
        Dim PFSansTrafic As PlanFeuxBase
        With mPlansFeuxBase

          'Affecter les plans de feux de fonctionnement au sc�nario correspondant � leur p�riode de trafic
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

          'Ajouter un Sc�nario par p�riode de trafic
          For Each unTrafic In mTrafics
            If unTrafic Is pfBase.Trafic Then
              unTrafic.Verrouill� = True
            Else
              unPlanBase = .Item(.Add(New PlanFeuxBase))
              unPlanBase.Trafic = unTrafic
              unPlanBase.Verrou = [Global].Verrouillage.LignesFeux
              unPlanBase.Cr�erAntagonismes()
            End If
          Next

        End With

      End If

    Else
      If mTrafics.Count > 0 Then
        'Projet ACONDIA : Trafics cr��s mais pas de phasage de retenu
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
        pfBase = New PlanFeuxBase("Sc�nario sans trafic")
        pfBase.Verrou = mVerrou
        mPlansFeuxBase.Add(pfBase)
      End If
    End If

    For Each unPlanBase In mPlansFeuxBase
      unPlanBase.VertMiniV�hicules = VertMiniV�hicules
      unPlanBase.VertMiniPi�tons = VertMiniPi�tons
    Next

    Dim unAntagonisme As Antagonisme
    If Not IsNothing(pfBase) AndAlso Not IsNothing(pfBase.Antagonismes) Then
      'La cr�ation des antagonismes n'a dupliqu� que les syst�matiques dan le plan de feux 
      ' pour les projets ACONDIA, il faut une duplication compl�te
      For Each unAntagonisme In Antagonismes
        pfBase.Antagonismes(Antagonismes.IndexOf(unAntagonisme)).TypeConflit = unAntagonisme.TypeConflit
      Next
    End If
    Return pfBase

  End Function

  '******************************************************************************
  ' D�finir l'encombrement du dessin
  '******************************************************************************
  Public Sub D�finirEncombrement(ByVal NomRuesACompter As Boolean)
    Dim uneBranche As Branche
    Dim pMin, pMax As PointF
    Dim pExt, pExtr�mit�(mBranches.Count * 2 - 1) As PointF
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
      '10 m : Augmenter cette valeur s'il s'av�re que le nom des rues ne peut pas s'inscrire dans le cadre
      AjoutLongueur = 10

      For Each uneBranche In mBranches
        With uneBranche
          DemiLargeur = .Largeur / 2
          pExt = PointPosition(.Origine, .Longueur + AjoutLongueur, .Angle, True)
          pExtr�mit�(i) = PointPosition(pExt, DemiLargeur, .AngleEnRadians + Math.PI / 2)
          With pExtr�mit�(i)
            pMin.X = Math.Min(pMin.X, .X)
            pMin.Y = Math.Min(pMin.Y, .Y)
            pMax.X = Math.Max(pMax.X, .X)
            pMax.Y = Math.Max(pMax.Y, .Y)
          End With
          pExtr�mit�(i + 1) = PointPosition(pExtr�mit�(i), DemiLargeur, .AngleEnRadians - Math.PI / 2)
          With pExtr�mit�(i + 1)
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
        For i = 0 To pExtr�mit�.Length - 1
          If Math.Abs(Wpmin.X - pExtr�mit�(i).X) < 2 Then
            Wpmin.X -= 2
          End If
          If Math.Abs(Wpmin.Y - pExtr�mit�(i).Y) < 2 Then
            Wpmin.Y -= 2
          End If
          If Math.Abs(Wpmax.X - pExtr�mit�(i).X) < 2 Then
            Wpmax.X += 2
          End If
          If Math.Abs(Wpmax.Y - pExtr�mit�(i).Y) < 2 Then
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

  Public Function Libell�(Optional ByVal AjoutEtoile As Boolean = True) As String
    Libell� = mCarrefour.Nom
    If Not IsNothing(NomFichier) Then
      Libell� = IO.Path.GetFileNameWithoutExtension(NomFichier) & " - " & Libell�
    End If
    If mAEnregistrer AndAlso AjoutEtoile Then
      Libell� &= "*"
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
          'd�finition de l'origine par d�faut de la branche : il s'agit de l'origine en coordonn�es r�elles
          'Positionnement de l'origine � 12m du centre du carrefour (initialisation par d�faut)
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
      mVerrou = Verrouillage.G�om�trie
    End If

  End Sub

  '********************************************************************************************************************
  ' D�terminer la branche qui suit imm�diatement la branche dans le carrefour
  '********************************************************************************************************************
  Public Function BrancheSuivante(ByVal uneBranche As Branche) As Branche

    With mBranches
      Return .Suivante(uneBranche)
    End With

  End Function

  '********************************************************************************************************************
  ' D�terminer la branche qui pr�c�de imm�diatement la branche dans le carrefour
  '********************************************************************************************************************
  Public Function BranchePr�c�dente(ByVal uneBranche As Branche) As Branche

    With mBranches
      Return .Pr�c�dente(uneBranche)
    End With

  End Function

  '********************************************************************************************************************
  ' Lors du verrouillage de la g�om�trie, DIAGFEUX cr�e par d�faut une travers�e pi�tonne pour chaque passage pi�ton
  ' L'utilisateur pourra ensuite regrouper les passages pi�tons par 2 pour cr�er une nouvelle travers�e
  '********************************************************************************************************************
  Public Sub InitialiserTravers�es(ByVal uneCollection As Graphiques)
    Dim uneBranche As Branche
    Dim unPassage As PassagePi�ton
    Dim uneTravers�e As Travers�ePi�tonne

    For Each unPassage In mPassagesEnAttente
      'Cr�er la travers�e pi�tonne
      uneTravers�e = Cr�erTravers�e(unPassage, uneCollection)
    Next
    'For Each uneBranche In mBranches
    '  For Each unPassage In uneBranche.mPassages
    '    'Cr�er la travers�e pi�tonne
    '    uneTravers�e = Cr�erTravers�e(unPassage, uneCollection)
    '  Next
    'Next

  End Sub

  '********************************************************************************************************************
  ' Suppprimer toutes les trajectoires (suite au d�verrouillage de la g�om�trie
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
  ' Suppprimer une travers�e pi�tonne comportant plusieurs passages pi�tons de la m�me branche
  '********************************************************************************************************************
  Public Sub D�composerTravers�e(ByVal uneTravers�e As Travers�ePi�tonne, ByVal uneCollection As Graphiques)
    Dim unPassage As PassagePi�ton

    For Each unPassage In uneTravers�e.mPassages
      Cr�erTravers�e(unPassage, uneCollection)
    Next

  End Sub

  '********************************************************************************************************************
  ' Cr�er une travers�e comportant un seul passage pi�ton
  '********************************************************************************************************************
  Public Function Cr�erTravers�e(ByVal unPassage As PassagePi�ton, ByVal uneCollection As Graphiques) As Travers�ePi�tonne
    Dim uneTravers�e As Travers�ePi�tonne = unPassage.mTravers�e

    If Not IsNothing(uneTravers�e) Then
      'Supprimer l'ancienne travers�e associ�e au passage
      mTrajectoires.Remove(uneTravers�e, uneCollection)
      'Supprimer �galement la ligne de feux associ�e
      mLignesFeux.Remove(uneTravers�e.LigneFeu, uneCollection)
    End If

    'Cr�er la nouvelle travers�e
    uneTravers�e = New Travers�ePi�tonne(unPassage)
    Cr�erLienTravers�eLigneFeux(unPassage.mBranche, uneTravers�e, uneCollection)
    Return uneTravers�e

  End Function

  '********************************************************************************************************************
  ' Cr�er une travers�e form�e par les 2 passages pi�tons de la branche
  '********************************************************************************************************************
  Public Function Cr�erTravers�e(ByVal uneBranche As Branche, ByVal uneCollection As Graphiques) As Travers�ePi�tonne
    Dim uneTravers�e As Travers�ePi�tonne
    Dim colPassages As PassageCollection = uneBranche.mPassages

    Dim i As Short
    For i = 0 To 1
      uneTravers�e = colPassages(i).mTravers�e
      mTrajectoires.Remove(uneTravers�e, uneCollection)
      If Not IsNothing(uneTravers�e) Then
        mLignesFeux.Remove(uneTravers�e.LigneFeu, uneCollection)
      End If
    Next

    'Cr�er la nouvelle travers�e
    uneTravers�e = New Travers�ePi�tonne(colPassages)
    Cr�erLienTravers�eLigneFeux(uneBranche, uneTravers�e, uneCollection)
    Return uneTravers�e
  End Function

  Private Sub Cr�erLienTravers�eLigneFeux(ByVal uneBranche As Branche, ByVal uneTravers�e As Travers�ePi�tonne, ByVal uneCollection As Graphiques)
    mTrajectoires.Add(uneTravers�e, uneCollection)
    'Associer une ligne de feux � la travers�e
    Dim uneLigneFeux As New LigneFeuPi�tons(Nothing, uneBranche, cndSignaux.D�fautPi�ton)
    uneTravers�e.LigneFeu = uneLigneFeux
    uneLigneFeux.mTrajectoires.Add(uneTravers�e)
    mLignesFeux.Add(uneLigneFeux, uneCollection)
  End Sub

  '*****************************************************************************************************
  ' Intialise la collection des courants de circulation en fonction des branches entrantes et sortantes
  'Pour le mode graphique, cette fonction est appel�e au verrouillage de la g�om�trie
  'Pour le mode tableur, cette fonction est appel�e au verrouillage des lignes de feux
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
  ' Etape 1 : Cr�er les enregistrements n�cessaires dans le DataSet DIAGFEUX
  ' uneRowCarrefour : si Renseign�, l'appel provient de Carrefour.Enregistrer, qui cr�era le fichier(Ne pas cr�er le fichier ici)
  '********************************************************************************************************************
  Public Function Enregistrer(Optional ByVal uneRowCarrefour As DataSetDiagfeux.CarrefourRow = Nothing) As Boolean

    Dim uneRowVariante As DataSetDiagfeux.VarianteRow

    Try
      ds = New DataSetDiagfeux

      uneRowCarrefour = mCarrefour.Enregistrer(Me)

      Dim uneRowParam�trage As DataSetDiagfeux.Param�trageRow = ds.Param�trage.NewParam�trageRow
      With uneRowParam�trage
        'Le fichier n'a peut-�tre pas le m�me niveau de version
        'On a attendu le 1er r�enregistrement du fichier pour  mettre � niveau cette variable
        VersionFichier = [Global].VersionFichier
        .Version = VersionFichier
        .Organisme = Organisme
        .Service = Service
        .Logo = CheminLogo
        .VitessePi�tons = VitessePi�tons
        .VitesseV�hicules = VitesseV�hicules
        .VitesseV�los = VitesseV�los
        .D�bitSaturation = D�bitSaturation

        'v12 et ant�rieures
        '.VertUtile = D�calageVertUtile

        'v13
        .SignalPi�tonsSonore = SignalPi�tonsSonore

        'v13 et DiagFeux1
        '.TempsPerduD�marrage = TempsPerduD�marrage
        'If EnAgglo() Then
        '  .JauneInutilis�Agglo = JauneInutilis�
        '  .JauneInutilis�Campagne = cndParam�tres.TempsJauneInutilis�Campagne
        'Else
        '  .JauneInutilis�Campagne = JauneInutilis�
        '  .JauneInutilis�Agglo = cndParam�tres.TempsJauneInutilis�Agglo
        'End If

        'DiagFeux2 (sauf si DiagFeux1 est remplac�)
        If EnAgglo() Then
          .TempsPerduD�marrage = TempsPerduD�marrage
          .JauneInutilis�Agglo = JauneInutilis�
        Else
          .TempsPerduD�marrageCampagne = TempsPerduD�marrage
          .JauneInutilis�Campagne = JauneInutilis�
        End If
      End With
      ds.Param�trage.AddParam�trageRow(uneRowParam�trage)

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
        If IsNothing(Sc�narioCourant) Then
          uneRowVariante = .AddVarianteRow(Nothing, ModeGraphique, mVerrou, VertMiniV�hicules, VertMiniPi�tons, uneRowCarrefour)
        Else
          If IsNothing(Sc�narioD�finitif) Then
            uneRowVariante = .AddVarianteRow(Sc�narioCourant.Nom, ModeGraphique, mVerrou, VertMiniV�hicules, VertMiniPi�tons, uneRowCarrefour)
          Else
            uneRowVariante = .AddVarianteRow(Sc�narioCourant.Nom, ModeGraphique, mVerrou, VertMiniV�hicules, VertMiniPi�tons, uneRowCarrefour)
          End If
        End If
      End With

      With mParamDessin
        Dim uneRowParamDessin As DataSetDiagfeux.ParamDessinRow
        uneRowParamDessin = ds.ParamDessin.AddParamDessinRow(.Echelle, uneRowVariante)
        ds.OrigineReelle.AddOrigineReelleRow(.OrigineR�elle.X, .OrigineR�elle.Y, uneRowParamDessin)
        ds.Taille.AddTailleRow(.TailleFen�tre.Width, .TailleFen�tre.Height, uneRowParamDessin)
      End With

      If Not IsNothing(mFondDePlan) Then
        mFondDePlan.Enregistrer(uneRowVariante)
      End If

      'Cr�er les branches
      Dim uneBranche As Branche
      'Enregistrer les origines de branches en relatif par rapport au centre du carrefour
      For Each uneBranche In mBranches
        uneBranche.Enregistrer(uneRowVariante)
      Next

      'Cr�er les lignes de feux
      mLignesFeux.Enregistrer(uneRowVariante)

      'Cr�er les trajectoires
      If mTrajectoires.Enregistrer(uneRowVariante) Then
        'Anomalie lors de l'enregistrement des trajectoires
        Enregistrer = True
      End If

      'Cr�er les trafics
      Dim unTrafic As Trafic
      For Each unTrafic In mTrafics
        unTrafic.Enregistrer(uneRowVariante)
      Next

      Dim unPlanFeuxBase As PlanFeuxBase
      For Each unPlanFeuxBase In mPlansFeuxBase
        unPlanFeuxBase.Enregistrer(Me, uneRowVariante)
      Next


      ds.WriteXml(NomFichier, XmlWriteMode.WriteSchema)

      'If Cr�erFichier Then
      '  ds.WriteXml(NomFichier, XmlWriteMode.WriteSchema)
      'End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Enregistrement du carrefour")
    End Try

  End Function

  Public Overloads Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Try
      uneCollection.Clear()
      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)

      mNord.Cr�erGraphique(uneCollection)
      mSymEchelle.Cr�erGraphique(uneCollection)

      'Dessiner les branches et
      'Cr�er l'enveloppe int�rieure du carrefour en vue de son d�placement
      mGraphique = mBranches.Cr�erGraphique(uneCollection)
      mGraphique.ObjetM�tier = Me

      If ModeGraphique Then
        'Dessiner les trajectoires et les antagonismes
        mTrajectoires.Cr�erGraphique(uneCollection)

        'Dessiner les lignes de feux 
        mLignesFeux.Cr�erGraphique(uneCollection)
        Verrouiller()

        If cndContexte = [Global].OngletEnum.Trafics Then
          'DessinerTrafics(uneCollection)
        End If
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.Cr�erGraphique")
    End Try

  End Function

  '*********************************************************************
  'Cr�er les objets graphiques sp�cifiques � une impression particuli�re
  ' unObjetM�tier : - Carrefour (dessin de l'ensemble du  carrefour)
  '                 - Trafic 
  '                 - Phase 
  '                 - PlanFeux (vignette du pour le diagramme de phases)
  '                 - Variante (vignette pour le dessin de la matrice des rouges de d�gagement)
  '*********************************************************************
  Public Overloads Function Cr�erGraphique(ByVal uneCollection As Graphiques, ByVal unObjetM�tier As M�tier) As PolyArc

    Try

      uneCollection.Clear()
      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)

      If TypeOf unObjetM�tier Is Carrefour Then
        Cr�erGraphique(uneCollection)

      ElseIf TypeOf unObjetM�tier Is Trafic Then
        Dim unTrafic As Trafic = CType(unObjetM�tier, Trafic)
        mBranches.DessinerTrafic(uneCollection, unTrafic)

      ElseIf unObjetM�tier Is Me Then
        'mini dessin du carrefour pour les rouges de d�gagement
        mBranches.Cr�erGraphique(uneCollection)
        mLignesFeux.Cr�erGraphique(uneCollection)

      ElseIf TypeOf unObjetM�tier Is PlanFeuxBase Then
        'mini dessin du carrefour pour le diagramme des phases
        mBranches.Cr�erGraphique(uneCollection)
        mTrajectoires.Cr�erGraphique(uneCollection)

      ElseIf TypeOf unObjetM�tier Is Phase Then
        'Diagramme d'une phase
        mBranches.Cr�erGraphique(uneCollection)
        ' les branches n'ont �t� cr��es que pour s'appuyer dessus pour dessiner le diagramme
        mBranches.Masquer()
        CType(unObjetM�tier, Phase).Cr�erGraphique(uneCollection)
        '        mLignesFeux.DessinerPhase(CType(unObjetM�tier, Phase), uneCollection)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.Cr�erGraphique")

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
    Dim G�om�trieD�pla�able As Boolean = (cndContexte = [Global].OngletEnum.G�om�trie And Verrou < [Global].Verrouillage.LignesFeux)

    'Le carrefour est toujours d�pla�able dans l'onglet g�om�trie
    mGraphique.RendreS�lectable(G�om�trieD�pla�able)
    mBranches.Verrouiller(Verrouillage:=Not G�om�trieD�pla�able)

    If ModeGraphique Then
      mTrajectoires.Verrouiller()
      mLignesFeux.Verrouiller()
      If Not IsNothing(Sc�narioCourant) AndAlso Not IsNothing(Sc�narioCourant.Antagonismes) Then
        Sc�narioCourant.Antagonismes.Verrouiller()
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

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal uneVariante As Variante) As Short
    Add = Me.List.Add(uneVariante)
    If Not uneVariante.mCarrefour.mVariantes.Contains(uneVariante) Then
      uneVariante.mCarrefour.mVariantes.Add(uneVariante)
    End If
    Return Add
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal desVariantes() As Variante)
    Me.InnerList.AddRange(desVariantes)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal uneVariante As Variante)
    If Me.List.Contains(uneVariante) Then
      Me.List.Remove(uneVariante)
    End If

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneVariante As Variante)
    Me.List.Insert(Index, uneVariante)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Variante
    Get
      Return CType(Me.List.Item(Index), Variante)
    End Get
  End Property

  Public Function IndexOf(ByVal uneVariante As Variante) As Short
    Return Me.List.IndexOf(uneVariante)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
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
'--------------------------- Structure Param�tres --------------------------
'=====================================================================================================
Public Structure Param�tres
  Public VersionFichier As Short
  Public Organisme As String
  Public Service As String
  Public CheminStockage As String
  Public CheminFDP As String
  Public CheminLogo As String
  Public VitessePi�tons As Single
  Public VitesseV�hicules As Short
  Public VitesseV�los As Short
  Public D�bitSaturation As Short
  'V12 et ant�rieures
  'Public D�calageVertUtile As Short
  'V13
  Public TempsPerduD�marrageAgglo As Short
  Public TempsPerduD�marrageCampagne As Short
  Public TempsJauneInutilis�Agglo As Short
  Public TempsJauneInutilis�Campagne As Short
  Public SignalPi�tonsSonore As Boolean

  Public Sub New(ByVal vitessePi�tons As Single, ByVal vitesseV�hicules As Short, ByVal vitesseV�los As Short, ByVal d�bit As Short, ByVal JauneAgglo As Short, ByVal JauneCampagne As Short, ByVal SignalPi�tonsSonore As Boolean)
    'par d�faut 0s pour : TempsPerduD�marrage
    ' Temps de jaune pour TempsJauneInutilis� 

    Me.VitessePi�tons = vitessePi�tons
    Me.VitesseV�hicules = vitesseV�hicules
    Me.VitesseV�los = vitesseV�los
    Me.D�bitSaturation = d�bit

    'v13
    Me.TempsJauneInutilis�Agglo = JauneAgglo
    Me.TempsJauneInutilis�Campagne = JauneCampagne
    Me.SignalPi�tonsSonore = SignalPi�tonsSonore

  End Sub

  Public Sub New(ByVal Initial As Boolean)
    With cndParam�tres
      VersionFichier = .VersionFichier
      Organisme = .Organisme
      Service = .Service
      CheminStockage = .CheminStockage
      CheminLogo = .CheminLogo
      VitessePi�tons = .VitessePi�tons
      VitesseV�hicules = .VitesseV�hicules
      VitesseV�los = .VitesseV�los
      D�bitSaturation = .D�bitSaturation
      'V12 et ant�rieures
      '      D�calageVertUtile = .D�calageVertUtile
      'V13
      TempsPerduD�marrageAgglo = .TempsPerduD�marrageAgglo
      TempsPerduD�marrageCampagne = .TempsPerduD�marrageCampagne
      TempsJauneInutilis�Agglo = .TempsJauneInutilis�Agglo
      TempsJauneInutilis�Campagne = .TempsJauneInutilis�Campagne
      SignalPi�tonsSonore = .SignalPi�tonsSonore
    End With
  End Sub

  Public ReadOnly Property IsEmpty() As Boolean
    Get
      Return (VersionFichier = 0)
    End Get
  End Property
End Structure
