'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : LigneFeux.vb																							'
'						Classes																														'
'							LigneFeux																												'
'							LigneFeuxCollection																							'
'							LigneFeuVéhicules																							'
'							LigneFeuPiétons																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe LigneFeux --------------------------
'=====================================================================================================
Public MustInherit Class LigneFeux : Inherits Métier
  'Une ligne de feux est soit une ligne de feux véhicules, soit une ligne de feux piétons
  Public MustOverride Sub Verrouiller()
  Public MustOverride Sub DéterminerLargeurDégagement()
  Public MustOverride Sub DéterminerAutorisationDécalage(ByVal unePhase As Phase)
  Public MustOverride Sub CréerGraphiqueDégagement(ByVal uneCollection As Graphiques)
  Public MustOverride Function DuréeJaune() As Short

  '***********************************************************************************
  ' Retourne la position par défaut du signal lié à la ligne de feux
  ' Index : 0 ou 1 (il peut y avoir 1 ou 2 signaux pour représenter une ligne Piétons)
  '***********************************************************************************
  Public MustOverride Function PositionSignal(ByVal Index As Short) As Point
  Public MustOverride Sub CréerGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)

  Public Const MaxiRougeDégagement As Short = 99

  Public mBranche As Branche
  Public mVariante As Variante

  'Signal associé à la ligne de feux
  '##ModelId=3C72748F0148
  '	Private Signal As Global.SignalEnum

  'Code du feu (Identifiant unique)
  '##ModelId=3C72750400BB
  Public ID As String

  'Signaux physiques associés à la ligne de feux
  'Sera redimensionné à 1 pour un feu piétons
  Public mSignalFeu(0) As SignalFeu

  '##ModelId=3C8B7FBE033C
  Public mTrajectoires As TrajectoireCollection

  Private mLgDégagement As Single

  Protected mDécalageOuvertureAutorisé As Boolean

  Public ReadOnly Property DécalageOuvertureAutorisé() As Boolean
    Get
      Return mDécalageOuvertureAutorisé
    End Get
  End Property

  Public Property LgDégagement() As Single
    Get
      Return mLgDégagement
    End Get
    Set(ByVal Value As Single)
      mLgDégagement = Value
    End Set
  End Property

  Public Property Signal() As Signal
    ' Tous les signaux physiques d'une ligne de feux sont associés au même signal
    Get
      Return mSignalFeu(0).mSignal
    End Get
    Set(ByVal Value As Signal)
      mSignalFeu(0).mSignal = Value
    End Set
  End Property

  'Temps de rouge de dégagement entre cette ligne de feux et uneLigneFeux'ensemble des lignes de feux du carrefour
  '##ModelId=3C8B300F00CB
  Public Sub TempsDégagementTotal()
    'No implementation (abstract class)
  End Sub

  Public Overridable Function ToutesVoiesSurBranche() As Boolean
  End Function

  '********************************************************************************************************************
  ' Construit la chaine à afficher dans la ligne de saisie des lignes de feux à partir des propriétés de la ligne de feux
  '********************************************************************************************************************
  Public Overridable Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal Séparateur As Char) As String
    Dim strLigne As String

    With Me
      Dim uneBranche As Branche = .mBranche

      strLigne = desBranches.ID(uneBranche) & Séparateur
      strLigne &= uneBranche.NomRue & Séparateur
      strLigne &= .ID & Séparateur
      strLigne &= cndSignaux.strCode(.Signal) & Séparateur
    End With

    Return strLigne

  End Function

  '********************************************************************************************************************
  ' Une ligne de feux est trivialement compatible avec elle-même
  ' 2 lignes de feux piétons le sont également
  '********************************************************************************************************************
  Public ReadOnly Property EstTrivialementCompatible(ByVal uneLigneFeux As LigneFeux) As Boolean

    Get
      Try
        If uneLigneFeux Is Me Then
          EstTrivialementCompatible = True
        ElseIf Me.EstPiéton And uneLigneFeux.EstPiéton Then
          EstTrivialementCompatible = True
        End If

      Catch ex As System.Exception
        Throw New DiagFeux.Exception(ex.Message)
      End Try
    End Get
  End Property

  Public ReadOnly Property EstPiéton() As Boolean
    Get
      Return TypeOf Me Is LigneFeuPiétons
    End Get
  End Property

  Public ReadOnly Property EstVéhicule() As Boolean
    Get
      Return TypeOf Me Is LigneFeuVéhicules
    End Get
  End Property

  Public Overridable Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow, ByVal desLignesFeux As LigneFeuxCollection) As DataSetDiagfeux.LigneDeFeuxRow
    Dim uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow = ds.LigneDeFeux.NewLigneDeFeuxRow
    Dim uneRowSignalFeu As DataSetDiagfeux.SignalRow

    Try
      'Ajouter une enregistrement dans la table des Variantes du carrefour
      uneRowLigneDeFeux = ds.LigneDeFeux.NewLigneDeFeuxRow
      With uneRowLigneDeFeux
        .ID = ID
        .NumBranche = mVariante.mBranches.IndexOf(mBranche)
        .SetParentRow(uneRowVariante)
      End With
      ds.LigneDeFeux.AddLigneDeFeuxRow(uneRowLigneDeFeux)
      With mSignalFeu(0)
        uneRowSignalFeu = ds.Signal.AddSignalRow(Signal.strCode, .Position.X, .Position.Y, uneRowLigneDeFeux)
      End With
      If EstPiéton Then
        With mSignalFeu(1)
          uneRowSignalFeu = ds.Signal.AddSignalRow(Signal.strCode, .Position.X, .Position.Y, uneRowLigneDeFeux)
        End With
      End If

      'If desLignesFeux.mRougesDégagement.Length > 0 Then
      If desLignesFeux.colRougesDégagement.Count > 0 Then
        'Lignes de feux verrouillées : les incompatibilités sont initialisées
        Dim uneLigneAdverse As LigneFeux

        For Each uneLigneAdverse In desLignesFeux
          'Incompatibilités

          If desLignesFeux.EstIncompatible(Me, uneLigneAdverse) Then
            If desLignesFeux.IndexOf(uneLigneAdverse) > desLignesFeux.IndexOf(Me) Then
              'On n'écrit qu'une fois l'incompatibilité : si F1 incompatible avec F2 inutile d'écrire que F2 l'est avec F1
              ds.Incompatible.AddIncompatibleRow(uneLigneAdverse.ID, uneRowLigneDeFeux)
            End If
          End If

          'Rouges de dégagement

          'Solution 1 : toujours écrire une valeur

          ds.RougeDégagement.AddRougeDégagementRow(uneLigneAdverse.ID, desLignesFeux.RougeDégagement(Me, uneLigneAdverse), uneRowLigneDeFeux)
          'Solution 2 :  n'écrire que si la valeur est renseignée
          'If mRougesDégagement.Contains(uneLigneAdverse) Then
          '  ds.RougeDégagement.AddRougeDégagementRow(uneLigneAdverse.ID, RougeDégagement(uneLigneAdverse), uneRowLigneDeFeux)
          'End If
          'Solution 3 : n'écrire que si la valeur est positive
          'If Me.RougeDégagement(uneLigneAdverse) <> 0 Then
          '  ds.RougeDégagement.AddRougeDégagementRow(uneLigneAdverse.ID, RougeDégagement(uneLigneAdverse), uneRowLigneDeFeux)
          'End If
        Next

      End If

      Return uneRowLigneDeFeux

    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message)
    End Try

  End Function

  Public Sub New(ByVal ID As String, ByVal uneBranche As Branche, ByVal unSignal As Signal)
    mVariante = cndVariante
    If mVariante.ModeGraphique Then mTrajectoires = New TrajectoireCollection
    Me.ID = ID
    mBranche = uneBranche
    mSignalFeu(0) = New SignalFeu(unSignal, Me)
  End Sub

  Public Sub New(ByVal uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow)

    mVariante = cndVariante
    If mVariante.ModeGraphique Then mTrajectoires = New TrajectoireCollection

    With uneRowLigneDeFeux
      ID = .ID
      mBranche = mVariante.mBranches(.NumBranche)
      'Signal feu
      With .GetSignalRows(0)
        mSignalFeu(0) = New SignalFeu(cndSignaux(.strCode), Me)
        If Echelle <> 0 Then mSignalFeu(0).Position = New Point(.X, .Y)
      End With
    End With

  End Sub

  '****************************************************************************************************************
  ' Retourne sous forme d'une chaine le numéro absolu de la ligne de feux dans la collection de la variante courante
  ' La numérotation  commence à "1"
  '****************************************************************************************************************
  Public Function strNuméro() As String
    strNuméro = (mVariante.mLignesFeux.IndexOf(Me) + 1).ToString
  End Function

  Public Sub DessinerNuméroLigne(ByVal pRef As PointF, ByVal unGraphique As PolyArc)
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseNuméroImpression).Clone
    Dim uneBrosse As SolidBrush = cndPlumes.Brosse(Plumes.BrosseEnum.PhaseNuméroImpression).Clone

    'unGraphique.CréerCercleTexte(pRef, Rayon:=2, unePlume:=unePlume, Chaine:=strNuméro, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    ' v11
    '    unGraphique.CréerCercleTexte(pRef, Rayon:=3, unePlume:=unePlume, Chaine:=ID, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    ' v12
    'pRefImprNuméro = pRef
    Dim GraphiqueNuméro As PolyArc = unGraphique.CréerCercleTexte(pRef, Rayon:=Phase.RayonCercleLF, unePlume:=unePlume, Chaine:=ID, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    GraphiqueNuméro.ObjetMétier = Me

  End Sub

  Protected ReadOnly Property SignalDessinable() As Boolean
    Get
      Return mVariante.SignalDessinable
    End Get
  End Property

  Protected Function TexteLgDégagement(ByVal p As Point, ByVal unAngle As Single) As Texte
    Dim Décalage As Short

    If EstPiéton Then
      Décalage = 2
    Else
      If AlignementTexte(unAngle) = StringAlignment.Center Then
        Décalage = 5
      Else
        Décalage = 3
      End If
    End If

    p.Y -= 2
    Dim PositionTexte As Point = PointPosition(p, Décalage, unAngle)

    Dim unTexte As New Texte(Format(Me.LgDégagement, "##"), New SolidBrush(Color.Green), New Font("Arial", 8), PositionTexte, Formules.AlignementTexte(unAngle))

    Return unTexte

  End Function

End Class

'=====================================================================================================
'--------------------------- Classe LigneFeuxCollection --------------------------
'=====================================================================================================
Public Class LigneFeuxCollection : Inherits CollectionBase
  Private mVariante As Variante
  Private dctLigneFeux As New Hashtable
  Private mPlanFeux As PlanFeuxBase

  Public mRougesDégagement(-1) As Hashtable
  Private colIncompatibles As New SortedList
  Public colRougesDégagement As New SortedList

  Public Enum OrdreDeTriEnum
    VéhiculesEnTête = 1
    OrdreBranche = 2
    OrdreCodeFeu = 3
    OrdrePhase = 4
  End Enum

  Private OrdreDeTri As OrdreDeTriEnum

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal uneVariante As Variante)

    MyBase.New()
    mVariante = uneVariante

  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeuxBase)
    MyBase.New()
    mPlanFeux = unPlanFeux
    mVariante = mPlanFeux.mVariante
  End Sub

  Public Sub Dimensionner(Optional ByVal RemiseAZéro As Boolean = False)

    If RemiseAZéro Then
      colRougesDégagement.Clear()
      colIncompatibles.Clear()
    Else

      Dim uneLigneFeux As LigneFeux
      For Each uneLigneFeux In Me
        colIncompatibles.Add(uneLigneFeux.ID, New Hashtable)
        colRougesDégagement.Add(uneLigneFeux.ID, New Hashtable)
      Next

    End If
  End Sub

  Public Sub RéinitialiserVoies()
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstVéhicule Then
        CType(uneLigneFeux, LigneFeuVéhicules).Voies.Clear()
      End If
    Next

  End Sub

  '*************************************************************************************************
  ' Cloner les incompatibilités de la collection de lignes de feux
  ' A leur initialisation, chaque scénario récupère les conflits systématiques des lignes de feux de la variante
  ' desLignesFeux : collection des lignes de feux de la variante
  '*************************************************************************************************
  Public Sub ClonerIncompatibilités(ByVal desLignesFeux As LigneFeuxCollection)
    Dim uneLigneFeux As LigneFeux
    Dim unEnumérateur As IDictionaryEnumerator
    Dim IDAdverse As String
    Dim uneHashtable As Hashtable

    Try

      Dimensionner()

      For Each uneLigneFeux In desLignesFeux
        uneHashtable = desLignesFeux.colIncompatibles(uneLigneFeux.ID)
        unEnumérateur = uneHashtable.GetEnumerator

        Do While unEnumérateur.MoveNext
          IDAdverse = unEnumérateur.Key
          EstIncompatible(Item(uneLigneFeux.ID), Item(IDAdverse)) = True
        Loop

        InitialiserTempsDégagement(desLignesFeux)

      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LignesFeux.ClonerIncompatibilités")

    End Try
  End Sub

  Public Property EstIncompatible(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Boolean
    Get
      If colIncompatibles.Count = 0 Then
        'Lignes de feux non verrouillées
        Return False

      Else
        Dim uneHashtable As Hashtable = colIncompatibles(uneLigneFeux.ID)
        Return uneHashtable.ContainsValue(uneLigneAdverse)
      End If

    End Get
    Set(ByVal Value As Boolean)
      Dim uneHashtable As Hashtable = colIncompatibles(uneLigneFeux.ID)

      If uneHashtable.ContainsValue(uneLigneAdverse) Xor Value Then
        'Compatibilité modifiée
        If Value Then
          uneHashtable.Add(uneLigneAdverse.ID, uneLigneAdverse)
        Else
          uneHashtable.Remove(uneLigneAdverse.ID)
        End If

        'L'incompatibilité est symétrique
        EstIncompatible(uneLigneAdverse, uneLigneFeux) = Value
      End If
    End Set
  End Property

  Public Property RougeDégagement(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Dim uneHashtable As Hashtable = colRougesDégagement(uneLigneFeux.ID)
      If uneHashtable.Contains(uneLigneAdverse) Then Return uneHashtable.Item(uneLigneAdverse)
    End Get
    Set(ByVal Value As Short)
      Dim uneHashtable As Hashtable = colRougesDégagement(uneLigneFeux.ID)
      uneHashtable.Item(uneLigneAdverse) = Value
    End Set
  End Property

  '*************************************************************************************************************
  ' Temps de dégagement entre 2 lignes de feux
  '*************************************************************************************************************
  Public Property TempsDégagement(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Return RougeDégagement(uneLigneFeux, uneLigneAdverse)
    End Get
    Set(ByVal Value As Short)
      RougeDégagement(uneLigneFeux, uneLigneAdverse) = Value
    End Set
  End Property

  '*************************************************************************************************************
  ' Intervert : temps entre la fin du vert et le début du vert de Ligne Adverse
  '*************************************************************************************************************
  Public ReadOnly Property InterVerts(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Return TempsDégagement(uneLigneFeux, uneLigneAdverse) + uneLigneFeux.DuréeJaune()
    End Get
  End Property

  '*************************************************************************************************************
  'Enregistrer la collection de lignes de feux
  '*************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim uneLigneFeux As LigneFeux

    Try

      'Enregistrer les lignes de feux
      For Each uneLigneFeux In Me
        uneLigneFeux.Enregistrer(uneRowVariante, Me)
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Enregistrement des lignes de feux")
    End Try

  End Sub

  Public Function CréerGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In Me
      If cndFlagImpression = dlgImpressions.ImpressionEnum.Matrice Then
        If PhaseActiveImpressionRougeDégagement.mLignesFeux.Contains(uneLigneFeux) Then
          uneLigneFeux.CréerGraphiqueDégagement(uneCollection)
        End If
      Else
        uneLigneFeux.CréerGraphique(uneCollection)
      End If
    Next

  End Function

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal uneLigneFeux As LigneFeux) As Short
    Add = Count
    Insert(Count, uneLigneFeux)

  End Function

  Public Function Add(ByVal uneLigneFeux As LigneFeux, ByVal uneCollection As Graphiques) As Short
    Add = Count
    Insert(Count, uneLigneFeux)
    uneLigneFeux.CréerGraphique(uneCollection)

  End Function

  '**********************************************************************************************************
  'ColonneModifiée, ValeurModifiée :Colonne et Valeur en cours de validation
  'Chaine : ensemble des champs de la ligne
  'NumLigneFeux : Index de la ligne de feux (commence à 0)
  '**********************************************************************************************************
  Public Function MettreAjour(ByVal ValeurModifiée As String, ByVal Booléen As Boolean, ByVal chaine As String, ByVal NumLigneFeux As Short, ByVal ColonneModifiée As Short) As LigneFeux
    Dim CodeVoie As Char
    Dim NomRue As String
    Dim ID, exID As String
    Dim CodeFeu As String
    Dim CodeFeuAssocié As String
    Dim NbVoies As Short
    Dim blnTAD As Boolean
    Dim blnTD As Boolean
    Dim blnTAG As Boolean

    Dim uneLigneFeux As LigneFeux = Nothing
    'Alimentation des données avec les valeurs actuelles de la ligne du tableau - le code tabulation(chr(9)) sert de séparateur
    Dim tValeurs() As String = Split(chaine, Chr(9))

    Try

      CodeVoie = tValeurs(0)
      NomRue = tValeurs(1)
      ID = tValeurs(2)
      CodeFeu = tValeurs(3)
      CodeFeuAssocié = tValeurs(4)
      If tValeurs(5).Length > 0 Then NbVoies = CType(tValeurs(5), Short)

      If StrComp(tValeurs(6), "True", CompareMethod.Text) = 0 Then
        blnTAG = True
      Else
        blnTAG = (tValeurs(6) = "   X")
      End If

      If StrComp(tValeurs(7), "True", CompareMethod.Text) = 0 Then
        blnTD = True
      Else
        blnTD = (tValeurs(7) = "   X")
      End If

      If StrComp(tValeurs(8), "True", CompareMethod.Text) = 0 Then
        blnTAD = True
      Else
        blnTAD = (tValeurs(8) = "   X")
      End If

      'La valeur qui vient d'être saisie n'est pas encore validée : il faut la substituer à celle alimentée ci-dessus
      Select Case ColonneModifiée
        Case 0
          CodeVoie = ValeurModifiée
        Case 1
          NomRue = ValeurModifiée
        Case 2
          ID = ValeurModifiée
          If NumLigneFeux < Me.Count Then
            uneLigneFeux = Me.Item(NumLigneFeux)
            exID = uneLigneFeux.ID
            If exID <> ID Then
              'Supprimer et recréer la  ligne de feux avec son nouvel ID
              uneLigneFeux.ID = ID
              Substituer(uneLigneFeux, exID)
            End If
          End If
        Case 3
          CodeFeu = ValeurModifiée
        Case 4
          CodeFeuAssocié = ValeurModifiée
        Case 5
          NbVoies = CType(ValeurModifiée, Short)
        Case 6
          blnTAG = Booléen
        Case 7
          blnTD = Booléen
        Case 8
          blnTAD = Booléen
      End Select

      If CodeVoie <> "" And ID.Length > 0 And CodeFeu.Length > 0 Then
        'On ne crée pas l'instance LigneFeux tq ces 3 données ne sont pas définies
        Dim uneBranche As Branche = mVariante.mBranches(CodeVoie)
        Dim unSignal As Signal = cndSignaux(CodeFeu)

        If dctLigneFeux.Contains(ID) Then
          uneLigneFeux = Me(ID)

          Select Case ColonneModifiée
            Case 0
              'Mode tableur : changement de branche de la LF
              SubstituerBranche(uneLigneFeux, uneBranche)
            Case 1
              uneLigneFeux.mBranche.NomRue = ValeurModifiée
            Case 2
              uneLigneFeux.ID = ID
            Case 3
              If uneLigneFeux.EstPiéton Xor unSignal.EstPiéton Then
                'Passage d'une ligne véhicules à une ligne piétons ou inversement
                uneLigneFeux = Substituer(uneLigneFeux, unSignal)
              Else
                uneLigneFeux.mSignalFeu(0).mSignal = unSignal
              End If
          End Select

        Else
          If unSignal.EstPiéton Then
            uneLigneFeux = New LigneFeuPiétons(ID, uneBranche, unSignal)
          Else
            uneLigneFeux = New LigneFeuVéhicules(ID, uneBranche, unSignal)
          End If
          Me.Add(uneLigneFeux)
        End If

        If uneLigneFeux.EstVéhicule Then
          With CType(uneLigneFeux, LigneFeuVéhicules)
            If CodeFeuAssocié.Length = 0 Then
              .mSignalAnticipation = Nothing
            Else
              .mSignalAnticipation = New SignalFeu(cndSignaux(CodeFeuAssocié), uneLigneFeux)
            End If

            'En mode tableur, pour une nouvelle ligne véhicules, il est intéressant d'initialiser à 1 le nombre de voies
            If Not mVariante.ModeGraphique Then
              If NbVoies = 0 Then NbVoies = 1
              If .NbVoiesTableur <> NbVoies Then
                'Cas d'une saisie manuelle dans le mode non graphique
                uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) += NbVoies - .NbVoiesTableur
                .NbVoiesTableur = NbVoies
                '.Voies.Clear()
                'For i = 1 To NbVoies
                '  .Voies.Add(New Voie(EstEntrante:=True, uneBranche:=.mBranche))
                'Next
              End If
            End If

            .TAD = blnTAD
            .TD = blnTD
            .TAG = blnTAG
          End With

        End If
      End If

    Catch ex As System.Exception
      Throw New DiagFeux.Exception(ex.Message)
    End Try

    Return uneLigneFeux

  End Function

  Private Sub SubstituerBranche(ByVal uneLigneFeux As LigneFeux, ByVal uneBranche As Branche)

    If uneLigneFeux.EstVéhicule Then
      Dim uneLigneVéhicules As LigneFeuVéhicules = uneLigneFeux
      Dim nbVoies As Short = uneLigneVéhicules.NbVoiesTableur
      uneLigneFeux.mBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) -= nbVoies
      uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) += nbVoies
    End If

    uneLigneFeux.mBranche = uneBranche

  End Sub

  '**********************************************************************************************************
  'Substituer une ligne feux à une autre suite à son changement de type : véhicules<-->piétons
  '**********************************************************************************************************
  Private Overloads Function Substituer(ByVal uneLigneFeux As LigneFeux, ByVal unSignal As Signal) As LigneFeux
    Dim newLigneFeux As LigneFeux
    Dim Index As Short = Me.IndexOf(uneLigneFeux)

    With uneLigneFeux
      If unSignal.EstPiéton Then
        newLigneFeux = New LigneFeuPiétons(.ID, .mBranche, unSignal)
        Dim nbVoies As Short = CType(uneLigneFeux, LigneFeuVéhicules).NbVoiesTableur
        .mBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) -= nbVoies
      Else
        newLigneFeux = New LigneFeuVéhicules(.ID, .mBranche, unSignal)
      End If

    End With

    Me.Remove(uneLigneFeux)
    Me.Insert(Index, newLigneFeux)
    Return newLigneFeux

  End Function

  '**********************************************************************************************************
  'Substituer une ligne feux à une autre suite à son changement d'ID (donc de clé) 
  '**********************************************************************************************************
  Public Overloads Function Substituer(ByVal uneLigneFeux As LigneFeux, ByVal exID As String) As LigneFeux

    dctLigneFeux.Remove(exID)
    dctLigneFeux(uneLigneFeux.ID) = uneLigneFeux

  End Function

  Public Sub Verrouiller()
    Dim uneLigneDeFeux As LigneFeux

    For Each uneLigneDeFeux In Me
      uneLigneDeFeux.Verrouiller()
    Next
  End Sub

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As LigneFeux)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal uneLigneFeux As LigneFeux)
    If Me.List.Contains(uneLigneFeux) Then
      Me.List.Remove(uneLigneFeux)
    End If

  End Sub

  Public Sub Remove(ByVal uneLigneFeux As LigneFeux, ByVal uneCollection As Graphiques)
    If uneLigneFeux.EstPiéton Then
      CType(uneLigneFeux, LigneFeuPiétons).EffacerSignaux(uneCollection)
    End If

    Remove(uneLigneFeux)

  End Sub

  Protected Overrides Sub OnRemove(ByVal Index As Integer, ByVal uneLigne As Object)
    Me.dctLigneFeux.Remove(CType(uneLigne, LigneFeux).ID)
  End Sub 'OnRemove

  Protected Overrides Sub OnClear()
    Me.dctLigneFeux.Clear()
  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneLigne As LigneFeux)
    'Ajouter l'objet au dictionnaire associé
    If IsNothing(uneLigne.ID) Then
      uneLigne.ID = RechercherIndicelibreID(TypeOf uneLigne Is LigneFeuPiétons)
    End If
    dctLigneFeux.Add(uneLigne.ID, uneLigne)

    'Ajout à la collection
    Me.List.Insert(Index, uneLigne)

  End Sub

  Private Function RechercherIndicelibreID(ByVal EstPiéton As Boolean) As String
    Dim i As Short
    Dim str As String
    Dim Préfixe As String = IIf(EstPiéton, "P", "F")
    Do
      i += 1
      str = Préfixe & CStr(i)
    Loop Until Not Me.Contains(str)
    Return str

  End Function

  '************************************************************************************
  ' Cette fonction permet de trouver l'index de la dernière ligne véhicules
  ' afin d'insérer une ligne véhicules entre celle-ci et la 1ère ligne piétons
  ' Si l'utilisateur a bousculé cet ordre, la ligne est ajoutée en fin de collection
  '************************************************************************************
  Public ReadOnly Property PremièreLigneVéhiculeDispo() As Short
    Get
      Dim PiétonTrouvé As Boolean
      Dim i As Short
      Dim uneLigneFeux As LigneFeux
      For Each uneLigneFeux In Me
        If uneLigneFeux.EstVéhicule Then
          If PiétonTrouvé Then
            'L'utilisateur a déjà inséré des lignes piétons entre les lignes véhicules : ajouter la ligne véhicules à la fin
            Return Me.Count
          Else
            i += 1
          End If
        Else
          PiétonTrouvé = True
        End If
      Next
      Return i
    End Get
  End Property

  Public Function nbLignesVéhicules() As Short
    Dim uneLigne As LigneFeux

    For Each uneLigne In Me
      If uneLigne.EstVéhicule Then nbLignesVéhicules += 1
    Next

    Return nbLignesVéhicules
  End Function

  Public Function nbLignesPiétons() As Short
    Return Count - nbLignesVéhicules()
  End Function

  '************************************************************************************
  ' Déplacer une ligne de feux dans la collection
  ' Décalage : indique de combien (en + ou en -) il faut décaler la ligne
  ' uneLigne : Ligne de feux à décaler
  '************************************************************************************
  Public Sub Décaler(ByVal Décalage As Short, ByVal uneLigne As LigneFeux)
    Dim NewPosition As Short = Me.IndexOf(uneLigne) + Décalage

    Me.Remove(uneLigne)
    Me.Insert(NewPosition, uneLigne)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As LigneFeux
    Get
      Return CType(Me.List(Index), LigneFeux)
    End Get
  End Property

  ' Creer une autre propriété par défaut Item pour cette collection.
  ' Permet la  recherche par nom.
  Default Public ReadOnly Property Item(ByVal ID As String) As LigneFeux
    Get
      If dctLigneFeux.Contains(ID) Then Return dctLigneFeux(ID)
    End Get
  End Property

  Public Function IndexOf(ByVal uneLigneFeux As LigneFeux) As Short
    Return Me.List.IndexOf(uneLigneFeux)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Overloads Function Contains(ByVal uneLigne As LigneFeux) As Boolean
    Return Me.List.Contains(uneLigne)
  End Function

  Public Overloads Function Contains(ByVal ID As String) As Boolean

    Contains = Me.dctLigneFeux.Contains(ID)

  End Function

  Public Sub RéinitialiserAntagos(ByVal desAntagonismes As AntagonismeCollection)
    Dim L1, L2 As LigneFeux

    For Each L1 In Me
      For Each L2 In Me
        If EstIncompatible(L1, L2) Then
          Select Case desAntagonismes.ExisteConflit(L1, L2)
            Case Trajectoire.TypeConflitEnum.Systématique
            Case Else
              EstIncompatible(L1, L2) = False
          End Select
        End If
      Next
    Next
  End Sub

  '**********************************************************************************************
  'Suite à la réinitialisation des antagonismes du plan de feux de base
  'Toutes ses lignes de feux redeviennent incompatibles, sauf les incompatibilités systématiques
  '**********************************************************************************************
  Public Sub RéinitialiserAntagos(ByVal desLignesFeux As LigneFeuxCollection)
    Dim L1, L2 As LigneFeux

    For Each L1 In Me
      For Each L2 In Me
        EstIncompatible(L1, L2) = desLignesFeux.EstIncompatible(L1, L2)
      Next
    Next
  End Sub

  '*************************************************************************************
  ' Réinitialiser les compatiblilités suite au déverrouillage des lignes de feux
  '*************************************************************************************
  Public Sub RéinitialiserIncompatibilités()
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneAdverse As LigneFeux
    Dim uneLigneVéhicules As LigneFeuVéhicules
    Dim uneLignePiétons As LigneFeuPiétons

    For Each uneLigneFeux In Me
      For Each uneLigneAdverse In Me
        If IndexOf(uneLigneAdverse) > IndexOf(uneLigneFeux) Then
          EstIncompatible(uneLigneFeux, uneLigneAdverse) = False
        End If

        If uneLigneFeux.EstVéhicule And uneLigneAdverse.EstPiéton Then
          uneLigneVéhicules = uneLigneFeux
          uneLignePiétons = uneLigneAdverse
          uneLigneVéhicules.RéinitialiserFiliations()
        End If
      Next
    Next

  End Sub

  '*************************************************************************************
  '*************************************************************************************
  Public Sub CréerIncompatibilités(ByVal desRowLignesDeFeux As DataSetDiagfeux.LigneDeFeuxRow())
    Dim uneLigneFeux As LigneFeux
    Dim uneRowRouge As DataSetDiagfeux.RougeDégagementRow
    Dim i, j As Short

    Dimensionner()

    For i = 0 To desRowLignesDeFeux.Length - 1
      With desRowLignesDeFeux(i)
        uneLigneFeux = Item(.ID)

        'Incompatibilités
        For j = 0 To .GetIncompatibleRows.Length - 1
          EstIncompatible(uneLigneFeux, Item(.GetIncompatibleRows(j).IDAdverse)) = True
        Next

        'Temps de rouge de dégagement
        For j = 0 To .GetRougeDégagementRows.Length - 1
          uneRowRouge = .GetRougeDégagementRows(j)
          RougeDégagement(uneLigneFeux, Item(uneRowRouge.IDAdverse)) = uneRowRouge.RougeDégagement_text
        Next
      End With
    Next

  End Sub

  '*************************************************************************************
  ' Déterminer la ligne de feux qui commande la trajectoire
  '*************************************************************************************
  Public Function DéterminerLignesFeux(ByVal uneTrajectoire As TrajectoireVéhicules) As LigneFeux
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxVéhicules As LigneFeuVéhicules
    Dim uneVoie As Voie

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstVéhicule Then
        uneLigneFeuxVéhicules = uneLigneFeux
        With uneTrajectoire
          For Each uneVoie In uneLigneFeuxVéhicules.Voies
            If uneVoie Is .Voie(TrajectoireVéhicules.OrigineDestEnum.Origine) Then
              .LigneFeu = uneLigneFeuxVéhicules
              .LigneFeu.mTrajectoires.Add(uneTrajectoire)
              uneLigneFeuxVéhicules.AjouterBrancheSortie(uneTrajectoire)
              Return uneLigneFeuxVéhicules
            End If
          Next
        End With

      End If  ' Ligne feux estvéhicule
    Next

  End Function

  '*************************************************************************************
  ' Vérifie si une ligne de feux ne coupe pas une des voies
  '*************************************************************************************
  Public Function VoiesCoupées(ByVal desVoies As VoieCollection) As LigneFeuVéhicules

    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxVéhicules As LigneFeuVéhicules
    Dim uneVoie As Voie

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstVéhicule Then
        uneLigneFeuxVéhicules = uneLigneFeux
        For Each uneVoie In uneLigneFeuxVéhicules.Voies
          If desVoies.Contains(uneVoie) Then Return uneLigneFeuxVéhicules
        Next
      End If
    Next
  End Function

  '*************************************************************************************
  ' Déterminer les largeurs de dégagement entre les lignes de feux
  '*************************************************************************************
  Private Sub DéterminerLargeursDégagement()
    Dim uneLigneFeux As LigneFeux

    'Déterminer d'abord les largeurs de dégagement
    For Each uneLigneFeux In Me
      uneLigneFeux.DéterminerLargeurDégagement()
    Next

  End Sub

  '*************************************************************************************
  ' Déterminer les temps de dégagement mini entre les lignes de feux
  'La valeur par défaut du rouge de dégagement du plan de feux de base 
  'est celui calculé comme rouge mini pour les lignes de feux de la variante 
  'les temps mini de dégagement seront affectées aux temps de dégagement des plans de feux de base à leur initialisation
  '*************************************************************************************
  Public Sub DéterminerTempsDégagement()
    Dim Li, Lj As LigneFeux

    DéterminerLargeursDégagement()

    For Each Li In Me
      If Li.EstVéhicule Then
        'Il n'y a pas de temps de dégagement entre 2 lignes piétons 
        'parcourir uniquement les lignes de véhicules
        For Each Lj In Me
          If Lj.EstVéhicule Then

            If Not Li.EstTrivialementCompatible(Lj) Then
              'Sinon : temps de dégagement = 0
              RougeDégagement(Lj, Li) = SpécialArrondiDégagement(Lj)
              RougeDégagement(Li, Lj) = SpécialArrondiDégagement(Li)
            End If

          Else
            'Lj est piéton

            If Not Li.EstTrivialementCompatible(Lj) Then
              If Li.mBranche Is Lj.mBranche Then
                'Pas de rouge de dégagement pour la ligne véhicules car au même niveau que le passage piétons
                'Ajout (AV : 15/09/06) : Rapport du CERTU sur la v11(09/06) : Points 12 et 19 de Conflits 
                RougeDégagement(Li, Lj) = SpécialArrondiDégagement(Li, Lj)
                'Rouge de dégagement piétons = durée de la traversée piétonne
                RougeDégagement(Lj, Li) = SpécialArrondiDégagement(Lj)
              Else
                RougeDégagement(Li, Lj) = SpécialArrondiDégagement(Li)
                'Déduire du temps de dégagement des piétons le temps que vont mettre les véhicules à traverser le carrefour
                'Sans que cette valeur ne devienne quand même négative
                RougeDégagement(Lj, Li) = Math.Max(0, SpécialArrondiDégagement(Lj) - RougeDégagement(Li, Lj))
              End If
            End If
          End If

        Next
      End If
    Next

  End Sub

  Private Function SpécialArrondiDégagement(ByVal uneLigneFeux As LigneFeux, Optional ByVal uneLignePiétons As LigneFeuPiétons = Nothing) As Short
    Dim RougeDégagement As Single
    Dim Vitesse As Single

    With mVariante.Param
      If IsNothing(uneLignePiétons) Then
        If uneLigneFeux.EstVéhicule Then
          Vitesse = .VitesseVéhicules
        Else
          Vitesse = .VitessePiétons
        End If

        RougeDégagement = uneLigneFeux.LgDégagement / Vitesse

      Else
        'Cas particulier : Traitement d'une ligne de feux véhicules située (très) en arrière du passage piéton sur la même brnche(!!!!)
        Vitesse = .VitesseVéhicules
        Dim Largeur As Single
        Largeur = Distance(CType(uneLigneFeux, LigneFeuVéhicules).Dessin.Milieu, uneLignePiétons.mTraversée.Contour) / Echelle
        RougeDégagement = Largeur / Vitesse
      End If
    End With

    If RougeDégagement > 0.1 Then
      'Arrondir à la valeur supérieure au-delà de 0.1s
      'cf Rapport du CERTU sur la v11(09/06) : Points 12 et 19 de Conflits 
      Return Math.Ceiling(RougeDégagement)
    Else
      Return 0
    End If

  End Function

  Public Sub InitialiserTempsDégagement(ByVal desLignesFeux As LigneFeuxCollection)
    Dim Li, Lj As LigneFeux

    For Each Li In Me
      For Each Lj In Me
        RougeDégagement(Li, Lj) = desLignesFeux.RougeDégagement(Li, Lj)
        RougeDégagement(Lj, Li) = desLignesFeux.RougeDégagement(Lj, Li)
      Next
    Next

  End Sub

  Public Sub Trier(ByVal Ordre As OrdreDeTriEnum)
    Dim uneLigneFeux As LigneFeux
    Dim i As Short
    Dim Indice(Count - 1) As Short
    Dim Décalage As Short

    OrdreDeTri = Ordre

    'Considérer au départ que le tableau est correctement ordonné
    For i = 0 To Count - 1
      Indice(i) = i
    Next

    'Algorithme de tri insertion : à la fin de la boucle, indice(i) sera un tableau ordonné des positions actuelles dans la collection des lignes de feux
    For i = 1 To Count - 1
      Insertion(i, Indice)
    Next

    'Mémoriser les lignes de feux avec l'ordre actuel
    Dim MémoLignes As New LigneFeuxCollection
    For Each uneLigneFeux In Me
      MémoLignes.Add(uneLigneFeux)
    Next

    'Remise en ordre des lignes de feux
    For i = 0 To Count - 1
      'i correspond à la nouvelle position de la  ligne de feux dont la position actuelle est mémorisée dans Indice(i)
      uneLigneFeux = MémoLignes(Indice(i))
      ' Calculer le décalage entre la position actuelle et la position souhaitée
      Décalage = i - IndexOf(uneLigneFeux)
      ' Décaler si nécessaire la ligne de feux
      If Décalage <> 0 Then Décaler(Décalage, uneLigneFeux)
    Next

    MémoLignes.Clear()

  End Sub

  Private Sub Insertion(ByVal droite_local As Short, ByRef Indice() As Short)
    Dim i As Short = droite_local - 1
    Dim sauv As Short = Indice(droite_local)

    Do While Supérieur(Item(Indice(i)), Item(sauv))
      Indice(i + 1) = Indice(i)
      i -= 1
      If i = -1 Then Exit Do
    Loop

    Indice(i + 1) = sauv
  End Sub

  Private Function Supérieur(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Boolean

    Select Case OrdreDeTri
      Case OrdreDeTriEnum.OrdreBranche
        Dim desBranches As BrancheCollection = mVariante.mBranches
        Select Case Math.Sign(desBranches.IndexOf(L1.mBranche) - desBranches.IndexOf(L2.mBranche))
          Case 1
            Supérieur = True
          Case 0
            If L1.EstPiéton And L2.EstVéhicule Then Supérieur = True
        End Select
      Case OrdreDeTriEnum.VéhiculesEnTête
        If L1.EstPiéton And L2.EstVéhicule Then Supérieur = True

      Case OrdreDeTriEnum.OrdreCodeFeu
        Supérieur = String.Compare(L1.ID, L2.ID) > 0

      Case OrdreDeTriEnum.OrdrePhase
        Return mPlanFeux.Supérieur(L1, L2)
    End Select

  End Function

  Public Sub PositionInsertion(ByVal uneLigneFeux As LigneFeux, ByVal desLignesFeux As LigneFeuxCollection)
    Dim lf As LigneFeux
    Dim t(Count - 1) As Short
    Dim Index, IndexLF As Short

    IndexLF = desLignesFeux.IndexOf(uneLigneFeux)

    For Each lf In desLignesFeux
      If Contains(lf) Then
        t(Index) = desLignesFeux.IndexOf(lf)
        If t(Index) > IndexLF Then
          Insert(Index, uneLigneFeux)
          Return
        End If
        Index += 1
      End If
    Next
    Insert(Index, uneLigneFeux)
  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe LigneFeuVéhicules--------------------------
'=====================================================================================================
Public Class LigneFeuVéhicules : Inherits LigneFeux
  'Ligne d'effets de feux : correspond à la ligne d'arrêt des véhicules sur la chaussée.


  'Décalage de la ligne de feu par rapport à l'origine de la branche.
  'Inutilisé dans le mode non graphique
  '##ModelId=3C72725C01F4
  Private mDécalage As Single

  'Nombre de voies de circulation commandées par la ligne de feux.
  'Saisi dans le mode non graphique
  '##ModelId=3C7272E3038A
  Public NbVoiesTableur As Short

  'Indice de la voie coupée par la ligne de feux la plus à droite.
  'Utile en particulier s'il y a une phase spéciale pour le TAG.
  '##ModelId=3C72732F0138
  '  Private NumVoie1 As Short

  Private mVoies As New VoieCollection
  Private mTrajectoirePrincipale As TrajectoireVéhicules
  Private mBranchesSortie As New BrancheCollection

  'Signal d'anticipation éventuel associé à la ligne de feux
  '##ModelId=3C7274DB002E
  Public mSignalAnticipation As SignalFeu

  'Indique si la ligne coupe un courant Tourne à gauche
  'Saisi dans le mode non graphique
  '##ModelId=3C7275310280
  Public TAG As Boolean

  'Indique si la ligne coupe un courant Tourne à droite
  'Saisi dans le mode non graphique
  '##ModelId=3C727560037A
  Public TAD As Boolean
  Private mCoefGêne(2) As Single

  'Indique si la ligne coupe un courant Tout droit
  'Saisi dans le mode non graphique
  '##ModelId=3C7275620109
  Public TD As Boolean

  'Demande (en uvp/s) de la file 
  'Private mDemandeUVP As Single
  '  Public nbVéhiculesEnAttente As Short

  Private dctFiliation As New Hashtable
  Private mDessin As Ligne

  Public ReadOnly Property TrajectoirePrincipale() As TrajectoireVéhicules
    Get
      Return mTrajectoirePrincipale
    End Get
  End Property

  Public ReadOnly Property Voies() As VoieCollection
    Get
      Return mVoies
    End Get
  End Property

  Public Overrides Function ToutesVoiesSurBranche() As Boolean
    With mBranche
      Return .NbVoies(Voie.TypeVoieEnum.VoieQuelconque) = NbVoiesTableur
    End With
  End Function

  '********************************************************************************************************************
  ' Construit la chaine à afficher dans la ligne de saisie des lignes de feux à partir des propriétés de la ligne de feux
  '********************************************************************************************************************
  Public Overrides Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal Séparateur As Char) As String
    ' Construire le début de la ligne (commune Véhicules/Piétons)
    Dim s As String = MyBase.strLigneGrille(desBranches, Séparateur)

    With Me
      If IsNothing(.mSignalAnticipation) Then
        s &= Séparateur
      Else
        s &= cndSignaux.strCode(.mSignalAnticipation.mSignal) & Séparateur
      End If

      If mVariante.ModeGraphique Then
        s &= .nbVoies.ToString & Séparateur
        s &= IIf(.TAG, StrCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TAG), "") & Séparateur
        s &= IIf(.TD, StrCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TD), "") & Séparateur
        s &= IIf(.TAD, StrCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TAD), "") & Séparateur

        's &= IIf(.TAG, mCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TAG).ToString, "") & Séparateur
        's &= IIf(.TD, mCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TD).ToString, "") & Séparateur
        's &= IIf(.TAD, mCoefGêne(TrajectoireVéhicules.NatureCourantEnum.TAD).ToString, "") & Séparateur

        's &= IIf(.TAG, "   X", "") & Séparateur
        's &= IIf(.TD, "   X", "") & Séparateur
        's &= IIf(.TAD, "   X", "") & Séparateur

      Else
        s &= .NbVoiesTableur.ToString & Séparateur
        s &= .TAG.ToString & Séparateur
        s &= .TD.ToString & Séparateur
        s &= .TAD.ToString & Séparateur
      End If
    End With

    Return s

  End Function

  Private Function StrCoefGêne(ByVal NatureCourant As TrajectoireVéhicules.NatureCourantEnum) As String
    If mCoefGêne(NatureCourant) = -1 Then ' ou 0 (?)
      Return " XX"
    Else
      Return mCoefGêne(NatureCourant).ToString
    End If
  End Function

  Public Sub New(ByVal ID As String, ByVal uneBranche As Branche, ByVal unSignal As Signal)
    MyBase.New(ID, uneBranche, unSignal)
  End Sub

  Public Sub New(ByVal uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow)
    MyBase.New(uneRowLigneDeFeux)

    Dim i As Short
    Dim NumVoie1 As Short

    Try
      With uneRowLigneDeFeux
        Décalage = .Décalage
        NumVoie1 = .NumVoie1
        'Ne sert que pour le mode tableur tq les LF ne sont pas verrouillées
        Me.NbVoiesTableur = .NbVoies

        If NumVoie1 <> -1 Then
          'pour la version tableur, il n'y a pas de lien entre les voies physiques des branches 
          ' et les voies de la ligne de feux 
          'Ce lien est fait lors du verrouillage des lignes de feux
          For i = 0 To .NbVoies - 1
            mVoies.Add(mBranche.Voies(NumVoie1 + i))
          Next
        End If

        TAD = .TAD
        TD = .TD
        TAG = .TAG

        If Not .IsSignalAnticipationNull Then
          If .SignalAnticipation.Length > 0 Then
            mSignalAnticipation = New SignalFeu(cndSignaux(.SignalAnticipation), Me)
          End If
        End If
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Lecture de la ligne de feux véhicules")
    End Try

  End Sub

  Public ReadOnly Property nbVoies() As Short
    Get
      Return mVoies.Count
    End Get
  End Property

  Public Property Décalage() As Single
    Get
      Return mDécalage
    End Get
    Set(ByVal Value As Single)
      mDécalage = Value
    End Set
  End Property

  '********************************************************************************************************************
  ' Enregistrer la ligne de feux véhicules dans le fichier
  ' Etape 1 : Créer les enregistrements nécessaires dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow, ByVal desLignesFeux As LigneFeuxCollection) As DataSetDiagfeux.LigneDeFeuxRow
    'Enregistrer d'abord la ligne de deux

    Dim uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow = MyBase.Enregistrer(uneRowVariante, desLignesFeux)

    If Not IsNothing(uneRowLigneDeFeux) Then
      'Enregistrer les propriétés spécifiques aux véhicules
      With uneRowLigneDeFeux
        .Décalage = Décalage
        If mVoies.Count = 0 Then
          'Dans le mode tableur, il n'y a pas de lien entre les voies des branches 
          'et celles des lignes de feux jusqu'au verrouillage des LF : NumVoie1 = -1
          .NbVoies = NbVoiesTableur
          .NumVoie1 = -1
        Else
          .NbVoies = nbVoies
          .NumVoie1 = mBranche.Voies.IndexOf(mVoies(0))
        End If

        .TAD = TAD
        .TD = TD
        .TAG = TAG
        If IsNothing(mSignalAnticipation) Then
          .SignalAnticipation = ""
        Else
          .SignalAnticipation = mSignalAnticipation.mSignal.strCode
        End If
      End With
    End If

    Return uneRowLigneDeFeux

  End Function

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me

    ' Le 1er point représentant la ligne de feux est le point de la ligne de feux le plus à gauche dans la branche(rappel : la branche est orientée à partir du centre du carrefour)
    Dim p1 As PointF = PointPosition(mVoies(mVoies.Count - 1).Extrémité(Branche.Latéralité.Gauche), Décalage * Echelle, mBranche.AngleEnRadians)
    Dim p2 As PointF = PointPosition(mVoies(0).Extrémité(Branche.Latéralité.Droite), Décalage * Echelle, mBranche.AngleEnRadians)

    Dim unePlume As Pen
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.LigneFeuVéhicule).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.LigneFeuVéhiculeImpression).Clone
    End If

    mDessin = New Ligne(p1, p2, unePlume)
    mGraphique.Add(mDessin)

    'If SignalDessinable Then
    With mSignalFeu(0)
      If .Position.Equals(New Point(0, 0)) Then
        'Intialiser avec la position par défaut
        .Position = PositionSignal(0)
      End If
      .CréerGraphique(uneCollection)
      If Not SignalDessinable Then .mGraphique.Invisible = True
    End With
    'End If

    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  '********************************************************************************
  ' AGauche : indique si le signal doit être positionné à gauche de la branche
  '********************************************************************************
  Public ReadOnly Property AGauche() As Boolean
    Get
      Dim nbVoies As Short = mVoies.Count
      With mBranche
        'AGauche, si 
        ' -la voie la +à gauche de la ligne de feux est la + à gauche de la branche 
        ' -la ligne de feux ne commande pas toutes les voies entrantes
        Return mVoies(nbVoies - 1) Is .Voies(.NbVoies(Voie.TypeVoieEnum.VoieSortante)) AndAlso _
        .NbVoies(Voie.TypeVoieEnum.VoieEntrante) > nbVoies
      End With
    End Get
  End Property

  Public Sub DéterminerNatureCourants(ByVal colTrajectoires As TrajectoireCollection)
    Dim uneTrajectoire As Trajectoire
    Dim uneTrajectoireVéhicules As TrajectoireVéhicules
    Dim uneVoie As Voie

    TAD = False
    TD = False
    TAG = False

    For Each uneTrajectoire In colTrajectoires
      If uneTrajectoire.EstVéhicule Then
        uneTrajectoireVéhicules = CType(uneTrajectoire, TrajectoireVéhicules)
        With uneTrajectoireVéhicules
          For Each uneVoie In mVoies
            If uneVoie Is .Voie(TrajectoireVéhicules.OrigineDestEnum.Origine) Then
              uneTrajectoire.LigneFeu = Me
              If Not mTrajectoires.Contains(uneTrajectoire) Then
                mTrajectoires.Add(uneTrajectoire)
                AjouterBrancheSortie(uneTrajectoireVéhicules)
              End If
              mCoefGêne(.NatureCourant) = AffecterCoefGêne(.NatureCourant, CType(uneTrajectoire, TrajectoireVéhicules).CoefGêne)
            End If
          Next
        End With  ' uneTrajectoireVéhicules
      End If
    Next

  End Sub

  Public Sub AjouterBrancheSortie(ByVal uneTrajectoire As TrajectoireVéhicules)
    Dim uneBranche As Branche = uneTrajectoire.Voie(TrajectoireVéhicules.OrigineDestEnum.Destination).mBranche

    If Not mBranchesSortie.Contains(uneBranche) Then
      mBranchesSortie.Add(uneBranche)
    End If
  End Sub

  Private Function AffecterCoefGêne(ByVal NatureCourant As TrajectoireVéhicules.NatureCourantEnum, ByVal Coefficient As Single) As Single

    Select Case NatureCourant
      Case TrajectoireVéhicules.NatureCourantEnum.TAG
        If TAG Then
          'Une trajectoire TAG est déjà commandée par cette LF
          If Coefficient = mCoefGêne(NatureCourant) Then
            Return Coefficient
          Else
            'Plusieurs coef gêne TAG pour cette ligne de feux : -1 permettra d'afficher des croix dans le tableau pour l'indiquer
            Return -1
          End If
        Else
          TAG = True
          Return Coefficient
        End If

      Case TrajectoireVéhicules.NatureCourantEnum.TD
        If TD Then
          If Coefficient = mCoefGêne(NatureCourant) Then
            Return Coefficient
          Else
            Return -1
          End If
        Else
          TD = True
          Return Coefficient
        End If
      Case TrajectoireVéhicules.NatureCourantEnum.TAD
        If TAD Then
          If Coefficient = mCoefGêne(NatureCourant) Then
            Return Coefficient
          Else
            Return -1
          End If
        Else
          TAD = True
          Return Coefficient
        End If
    End Select

  End Function

  Public Function DéterminerCourants() As Boolean
    Return mBranche.DéterminerCourants(Me)
  End Function

  Public ReadOnly Property VoiesAffectéesUnMouvement() As Boolean
    Get
      Dim uneVoie As Voie
      For Each uneVoie In Voies
        If uneVoie.mCourants.Count > 1 Then
          Return False
        End If
      Next

      Return True
    End Get
  End Property

  Public Function TraficPondéréRiche(ByVal unTrafic As Trafic) As Integer
    Dim uneVoie As Voie
    Dim unCourant As Courant
    Dim desCourants As New CourantCollection
    Dim q, qPondéré As Single
    Dim qCourant(-1), qCourantPondéré(0) As Single
    Dim nbVoies(-1) As Short
    Dim i As Short

    'Déterminer tous les courants gérés par la ligne de feux
    For Each uneVoie In Voies
      For Each unCourant In uneVoie.mCourants
        If Not desCourants.Contains(unCourant) Then
          desCourants.Add(unCourant)
          ReDim Preserve qCourant(qCourant.Length)
          ReDim Preserve qCourantPondéré(qCourantPondéré.Length)
          With unCourant
            qCourant(desCourants.Count - 1) = unTrafic.QVéhicule(.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine), _
                                                            .Branche(TrajectoireVéhicules.OrigineDestEnum.Destination))
            qCourantPondéré(desCourants.Count - 1) = qCourant(desCourants.Count - 1) * .CoefGêne
          End With
        End If
      Next
    Next

    If VoiesAffectéesUnMouvement Then
      'Rechercher le courant le plus chargé, en divisant les trafics de chaque courant par le nb de voies utilisant ce courant
      ReDim nbVoies(desCourants.Count - 1)
      For Each uneVoie In Voies
        nbVoies(desCourants.IndexOf(uneVoie.mCourants(0))) += 1
      Next
      For i = 0 To desCourants.Count - 1
        ' v11
        q = Math.Max(q, qCourant(i) / nbVoies(i))
        qPondéré = Math.Max(qPondéré, qCourantPondéré(i) / nbVoies(i))
      Next

    Else
      'Faire la sommation des trafics pondérés de chaque courant, et diviser le tout par le nombre de voies de la ligne de feux
      For i = 0 To desCourants.Count - 1
        q += qCourant(i)
        qPondéré += qCourantPondéré(i)
      Next
      q /= Voies.Count
      qPondéré /= Voies.Count
    End If

    ' DemandeUVP = qPondéré
    Return CType(qPondéré, Integer)

  End Function

  Public Function TraficPondéréDégradé(ByVal unTrafic As Trafic) As Integer
    Dim qTAG As Integer
    Dim qTAD As Integer
    Dim qTD As Integer
    Dim QE As Integer = unTrafic.QE(Trafic.TraficEnum.UVP, mVariante.mBranches.IndexOf(mBranche))
    Dim Branche2 As Branche

    '	trafic du courant commandé par la ligne de feux
    With mBranche
      '	trafics directionnels de la branche 
      If TAG Then
        Branche2 = mVariante.BranchePrécédente(mBranche)
        Do While Branche2.SensUnique(Voie.TypeVoieEnum.VoieSortante)
          Branche2 = mVariante.BranchePrécédente(Branche2)
          'Le test qui suit est superflu(il n'y aurait aucun trafic sortant !!)
          If Branche2 Is mBranche Then Exit Do
        Loop
        qTAG = unTrafic.QVéhicule(mBranche, Branche2)
      End If

      If TAD Then
        Branche2 = mVariante.BrancheSuivante(mBranche)
        Do While Branche2.SensUnique(Voie.TypeVoieEnum.VoieSortante)
          Branche2 = mVariante.BrancheSuivante(Branche2)
          'Le test qui suit est superflu(il n'y aurait aucun trafic sortant !!)
          If Branche2 Is mBranche Then Exit Do
        Loop
        qTAD = unTrafic.QVéhicule(mBranche, Branche2)
      End If
    End With

    'Dans la variante dégradée(mode tableur), si +  4 branches, tous les trafics sont tout droit sauf le 1er à gauche et le 1er à droite)
    qTD = QE - qTAD - qTAG

    'Dans la variante dégradée , on ne peut pas faire mieux (2è § p26 du guide carrefour à feux)
    Return (qTAD * CoefGêneTAD + qTD + qTAG * CoefGêneTAG) / nbVoies

  End Function

  ' *****************************************************************************
  ' Retourne le bord de la voie limitant la portée de la ligne de feux
  ' Index =0 s'il s'agit du bord  le plus à droite
  ' Index = 1 s'il s'agit du bord le plus à gauche 
  ' *****************************************************************************

  Public ReadOnly Property BordVoie(ByVal Index As Branche.Latéralité) As Ligne
    Get
      If Index = Branche.Latéralité.Droite Then
        Return VoieDroite.Bordure(Branche.Latéralité.Droite)
      Else
        Return VoieGauche.Bordure(Branche.Latéralité.Gauche)
      End If
    End Get
  End Property

  Public ReadOnly Property VoieGauche() As Voie
    Get
      Return mVoies(mVoies.Count - 1)
    End Get
  End Property

  Public ReadOnly Property VoieDroite() As Voie
    Get
      Return mVoies(0)
    End Get
  End Property

  Public ReadOnly Property Dessin() As Ligne
    Get
      Return mDessin
    End Get
  End Property

  Public Overrides Sub Verrouiller()

    Try
      mGraphique.RendreSélectable(cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.Géométrie)
      mGraphique.Invisible = (cndContexte = [Global].OngletEnum.Géométrie)
      With mSignalFeu(0).mGraphique
        CType(.Item(0), Boite).RendreSélectable(cndContexte >= [Global].OngletEnum.LignesDeFeux And SignalDessinable)
        .Invisible = (cndContexte = [Global].OngletEnum.Géométrie Or Not SignalDessinable)
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuVéhicules.Verrouiller")
    End Try

  End Sub

  Public Function VérifierVoieCoupée(ByVal maVoie As Voie) As Boolean
    Dim uneVoie As Voie

    mVoies.Contains(maVoie)
    For Each uneVoie In mVoies
      If uneVoie Is maVoie Then Return True
    Next

  End Function

  Public Sub CréerFiliation(ByVal L1 As LigneFeuVéhicules, ByVal L2 As LigneFeuPiétons)

    Try
      If Not dctFiliation.ContainsKey(L2) Then
        dctFiliation.Add(L2, L1)
      End If
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuVéhicules.CréerFiliation")

    End Try

  End Sub

  Public Sub RéinitialiserFiliations()
    dctFiliation.Clear()
  End Sub

  Public Function LigneFeuxLiée(ByVal uneLignePiétons As LigneFeuPiétons, Optional ByVal desLignesFeux As LigneFeuxCollection = Nothing) As LigneFeuVéhicules
    Dim uneLigneVéhicules As LigneFeuVéhicules
    If dctFiliation.ContainsKey(uneLignePiétons) Then
      uneLigneVéhicules = dctFiliation(uneLignePiétons)

      If desLignesFeux.EstIncompatible(Me, uneLigneVéhicules) Then
        Return uneLigneVéhicules
      End If
    End If
  End Function

  Public Sub PositionnerSignal()
    mSignalFeu(0).Position = PositionSignal(0)
  End Sub

  '***********************************************************************************
  ' Retourne la position par défaut du signal lié à la ligne de feux
  ' Index=0 : inutilisé car il n'y a qu'1 signal pour une ligne véhicules
  '***********************************************************************************
  Public Overrides Function PositionSignal(ByVal Index As Short) As Point
    Dim unAngle As Single = mBranche.AngleEnRadians
    If AGauche Then
      unAngle += Math.PI / 2
    Else
      unAngle -= Math.PI / 2
    End If

    'Return PointPosition(New Point(0, 0), 4, unAngle)
    Return PointPosition(New Point(0, 0), 16, unAngle)

  End Function

  Public Overrides Sub DéterminerLargeurDégagement()
    Dim uneTrajectoire As Trajectoire
    Dim LgMax As Single, Lg As Single

    For Each uneTrajectoire In mTrajectoires
      With CType(uneTrajectoire, TrajectoireVéhicules)
        Lg = .mGraphique.Longueur
        Lg -= .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Origine).Longueur
        Lg -= .AxeVoie(TrajectoireVéhicules.OrigineDestEnum.Destination).Longueur
        Lg /= Echelle
        Lg += mDécalage
      End With
      LgMax = Math.Max(LgMax, Lg)
      If LgMax = Lg Then mTrajectoirePrincipale = uneTrajectoire
    Next

    LgDégagement = LgMax

  End Sub

  Private Sub DessinerPhaseA(ByVal unePhase As Phase)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim unePlumeFlèche As Pen = unePlume.Clone
    Dim uneTrajectoire As Trajectoire = mTrajectoires(0)

    Dim mAxeDépart As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle, unAngleFlèche As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = mDessin.MilieuF
    Dim unGraphique As PolyArc = unePhase.mGraphique

    BrancheOrigine = CType(uneTrajectoire, TrajectoireVéhicules).mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de symétrie pour obtenir une parallèle à celle-ci passant au milieu de la ligne de feux
      With .LigneDeSymétrie
        P1 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 2mm (pour inscrire facilement le numéro de la ligne de feux
    P1 = PointPosition(P1, 2, AngleBrancheRadians + Math.PI)
    'L'axe départ : Axe de l'ensemble des voies entrantes commandées par la ligne de feux

    'L'épaisseur de la plume étant de 0.3, ceci fait un espacement de 0.6
    Dim EspacementTiret() As Single = {2, 2}
    If Signal.JauneClignotant Then
      unePlume.DashStyle = Drawing2D.DashStyle.Dash
      unePlume.DashPattern = EspacementTiret
    End If
    mAxeDépart = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeDépart, False)

    'Décrire le cercle entourant le numéro de ligne de feux
    Dim pCentre As PointF = PointPosition(P1, 5, AngleBrancheRadians)
    DessinerNuméroLigne(pCentre, unePhase.mGraphiqueNumérosFeux)

    If unePhase.mLignesFeux.Contains(Me) Then
      Dim pO, pO2 As PointF
      Dim mFlèche, uneFlèche As Fleche
      ' Créer une flèche 
      uneFlèche = New Fleche(0, HauteurFlèche:=2, SegmentCentral:=False, unePlume:=unePlumeFlèche)

      If TAD Then
        pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians - Math.PI / 2)
        'Créer un arc de cercle de 5mm de rayon tournant vers la droite
        unAngleFlèche = unAngle + 90
        unGraphique.Add(New Arc(pO, 5, unAngleFlèche Mod 360, 90, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

      If TAG Then
        'Rallonger la ligne axe origine de 5mm
        mAxeDépart.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
        'Créer un arc de cercle de 5mm de rayon tournant vers la gauche
        pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne à gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de départ
        unAngleFlèche = unAngle + 180
        unGraphique.Add(New Arc(pO, 5, unAngleFlèche Mod 360, 90, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

      If TD Then
        'Rallonger la ligne axe origine de 20mm
        mAxeDépart.pBF = PointPosition(P2, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la flèche à l'extrémité de l'axe de la ligne de feux
        pO2 = mAxeDépart.pBF
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

    Else
      'La ligne de feux appartient à une autre phase  : visualiser que les véhicules sont arrêtés au droit de cette ligne

      '1 ) on tronque l'axe central au droit de la ligne de feux
      mAxeDépart.pBF = pMilieuLigneFeux

      '2) On ajoute le dessin de la ligne elle-même (transverse à la précédente)
      Dim uneLigne As Ligne = mDessin.Clone
      uneLigne.Plume = unePlume
      mGraphique.Add(uneLigne)
    End If

  End Sub

  Private Sub DessinerPhaseB(ByVal unePhase As Phase)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim unePlumeFlèche As Pen = unePlume.Clone
    Dim uneTrajectoire As Trajectoire = mTrajectoires(0)

    Dim mAxeDépart As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle, unAngleFlèche As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = mDessin.MilieuF
    Dim unGraphique As PolyArc = unePhase.mGraphique

    BrancheOrigine = CType(uneTrajectoire, TrajectoireVéhicules).mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de symétrie pour obtenir une parallèle à celle-ci passant au milieu de la ligne de feux
      With .LigneDeSymétrie
        P1 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 2mm (pour inscrire facilement le numéro de la ligne de feux
    P1 = PointPosition(P1, 2, AngleBrancheRadians + Math.PI)
    'L'axe départ : Axe de l'ensemble des voies entrantes commandées par la ligne de feux

    'L'épaisseur de la plume étant de 0.3, ceci fait un espacement de 0.6
    Dim EspacementTiret() As Single = {2, 2}
    If Signal.JauneClignotant Then
      unePlume.DashStyle = Drawing2D.DashStyle.Dash
      unePlume.DashPattern = EspacementTiret
    End If
    mAxeDépart = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeDépart, False)

    'Décrire le cercle entourant le numéro de ligne de feux
    Dim pCentre As PointF = PointPosition(P1, 5, AngleBrancheRadians)
    DessinerNuméroLigne(pCentre, unePhase.mGraphiqueNumérosFeux)

    If unePhase.mLignesFeux.Contains(Me) Then
      Dim pO, pO2 As PointF
      Dim mFlèche, uneFlèche As Fleche
      ' Créer une flèche 
      uneFlèche = New Fleche(0, HauteurFlèche:=2, SegmentCentral:=False, unePlume:=unePlumeFlèche)

      Dim uneBranche As Branche
      Dim Ecart, EcartRadians, Balayage, BalayageRadians As Single
      For Each uneBranche In mBranchesSortie
        Ecart = unAngle - (360 - uneBranche.Angle)
        EcartRadians = CvAngleRadians(Ecart)
        Balayage = Math.Abs(180 - Math.Abs(Ecart))
        BalayageRadians = CvAngleRadians(Balayage)

        Select Case Ecart
          Case -360 To -180, 0 To 180
            'TAD
            pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians - Math.PI / 2)
            'Créer un arc de cercle de 5mm de rayon tournant vers la droite
            unAngleFlèche = unAngle + 90

            ' Positionner la flèche à l'extrémité de l'arc de cercle
            pO2 = PointPosition(pO, 5, uneBranche.AngleEnRadians - Math.PI / 2)
            mFlèche = uneFlèche.RotTrans(pO2, uneBranche.AngleEnRadians + Math.PI)
          Case Else
            'TAG
            'Rallonger la ligne axe origine de 5mm
            mAxeDépart.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
            pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians + Math.PI / 2)
            'Créer un arc de cercle de 5mm de rayon tournant vers la gauche
            unAngleFlèche = uneBranche.Angle + 90

            ' Positionner la flèche à l'extrémité de l'arc de cercle
            pO2 = PointPosition(pO, 5, uneBranche.AngleEnRadians + Math.PI / 2)
            mFlèche = uneFlèche.RotTrans(pO2, uneBranche.AngleEnRadians + Math.PI)
        End Select
        unGraphique.Add(New Arc(pO, 5, unAngleFlèche Mod 360, Balayage, unePlume))
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)

      Next

      Return

      Balayage = 90
      If TAD Then
        pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians - Math.PI / 2)
        'Créer un arc de cercle de 5mm de rayon tournant vers la droite
        unAngleFlèche = unAngle + 90
        unGraphique.Add(New Arc(pO, 5, unAngleFlèche Mod 360, Balayage, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

      If TAG Then
        'Rallonger la ligne axe origine de 5mm
        mAxeDépart.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
        'Créer un arc de cercle de 5mm de rayon tournant vers la gauche
        pO = PointPosition(mAxeDépart.pBF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne à gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de départ
        unAngleFlèche = unAngle + 180
        unGraphique.Add(New Arc(pO, 5, unAngleFlèche Mod 360, Balayage, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

      If TD Then
        'Rallonger la ligne axe origine de 20mm
        mAxeDépart.pBF = PointPosition(P2, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la flèche à l'extrémité de l'axe de la ligne de feux
        pO2 = mAxeDépart.pBF
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians)
        'Ajouter la flèche 
        unGraphique.Add(mFlèche)
      End If

    Else
      'La ligne de feux appartient à une autre phase  : visualiser que les véhicules sont arrêtés au droit de cette ligne

      '1 ) on tronque l'axe central au droit de la ligne de feux
      mAxeDépart.pBF = pMilieuLigneFeux

      '2) On ajoute le dessin de la ligne elle-même (transverse à la précédente)
      Dim uneLigne As Ligne = mDessin.Clone
      uneLigne.Plume = unePlume
      mGraphique.Add(uneLigne)
    End If

  End Sub

  Public Overrides Sub CréerGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)

    'Créer le dessin de la ligne de feux sans le rendre visible
    CréerGraphique(uneCollection)
    mDessin.Invisible = True
    DessinerPhaseA(unePhase)

  End Sub

  Public Overrides Sub DéterminerAutorisationDécalage(ByVal unePhase As Phase)

    mDécalageOuvertureAutorisé = False
    'mDécalageOuvertureAutorisé = True

    'If TAD Or TD Then
    '  'Interdire le décalage à l'ouverture si un TAG arrive en face dans la même phase
    '  For Each uneLigneFeux In unePhase.mLignesFeux
    '    If uneLigneFeux.EstVéhicule AndAlso Not uneLigneFeux Is Me Then
    '      If CType(uneLigneFeux, LigneFeuVéhicules).TAG Then
    '        mDécalageOuvertureAutorisé = False
    '      End If
    '    End If
    '  Next
    'End If

  End Sub

  Public Overrides Sub CréerGraphiqueDégagement(ByVal uneCollection As Graphiques)
    ' Dessiner la ligne de feux
    CréerGraphique(uneCollection)

    Dim unPolyArc As PolyArc = TrajectoirePrincipale.CréerGraphique(uneCollection)
    'Tronquer le début de la trajectoire au droit de la ligne de feux
    Dim uneLigne As Ligne = unPolyArc(0)
    uneLigne.pBF = intersect(uneLigne, mDessin)

    'Dernier segment de la trajectoire pour positionner l'écriture du rouge de dégagement
    uneLigne = CType(unPolyArc(unPolyArc.Count - 1), Ligne)
    Dim unAngle As Single = AngleFormé(uneLigne)

    'Dessiner une flèche à l'extrémité du segment final
    Dim uneFlèche As New Fleche(0, 2, SegmentCentral:=False, unePlume:=mDessin.Plume)
    uneFlèche = uneFlèche.RotTrans(uneLigne.pB, unAngle + sngPI)
    mGraphique.Add(uneFlèche)

    'Ecrire la distance de dégagement de la traversée véhicule
    mGraphique.Add(TexteLgDégagement(uneLigne.pB, unAngle))

  End Sub

  Public Overrides Function DuréeJaune() As Short

    If Signal.JauneClignotant Then
      DuréeJaune = JauneClignotant    ' R11J
    Else
      DuréeJaune = mVariante.JauneVéhicules
    End If
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe LigneFeuPiétons --------------------------
'=====================================================================================================
Public Class LigneFeuPiétons : Inherits LigneFeux

  Public Sub New(ByVal ID As String, ByVal uneBranche As Branche, ByVal unSignal As Signal)

    MyBase.New(ID, uneBranche, unSignal)
    ReDim Preserve mSignalFeu(1)
    mSignalFeu(1) = New SignalFeu(unSignal, Me)

  End Sub

  Public Sub New(ByVal uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow)

    MyBase.New(uneRowLigneDeFeux)
    ReDim Preserve mSignalFeu(1)

    With uneRowLigneDeFeux
      mSignalFeu(1) = New SignalFeu(cndSignaux(.GetSignalRows(0).strCode), Me)
      If .GetSignalRows.Length = 2 Then
        'Précaution pour relire les fichiers antérieurs à Proto6
        With .GetSignalRows(1)
          mSignalFeu(1).Position = New Point(.X, .Y)
        End With
      End If
    End With

  End Sub

  Public Overrides Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal Séparateur As Char) As String

    Dim s As String = MyBase.strLigneGrille(desBranches, Séparateur)
    s &= (Séparateur & Séparateur & Séparateur & Séparateur)
    Return s
  End Function

  '*****************************************************************************
  'La ligne de feux piétons n'a pas de représentation graphique
  'Celle-ci es remplacée par le dessin d'1 ou 2 signaux de feux
  '*****************************************************************************
  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim i As Short

    'If SignalDessinable Then
    For i = 0 To mSignalFeu.Length - 1
      If SignalAReprésenter(i) Then
        With mSignalFeu(i)
          If .Position.Equals(New Point(0, 0)) Then
            'Intialiser avec la position par défaut
            .Position = PositionSignal(i)
          End If
          .CréerGraphique(uneCollection)
          If Not SignalDessinable Then .mGraphique.Invisible = True
        End With
      End If
    Next
    'End If

  End Function

  Public ReadOnly Property SignalAReprésenter(ByVal Index As Short) As Boolean
    Get
      If Index = 0 Then
        Return True
      Else
        Return mBranche.mPassages.Count = 1 Or mTraversée.mDouble
      End If
    End Get
  End Property

  Public Overloads Overrides Sub Verrouiller()
    Dim i As Short

    Try
      For i = 0 To mSignalFeu.Length - 1
        If SignalAReprésenter(i) Then
          With mSignalFeu(i).mGraphique
            'CType(.Item(0), Boite).RendreSélectable(cndContexte >= Global.OngletEnum.LignesDeFeux And SignalDessinable)
            .RendreSélectable(cndContexte >= [Global].OngletEnum.LignesDeFeux And SignalDessinable, CType(.Item(0), Boite))
            .Invisible = (cndContexte = [Global].OngletEnum.Géométrie Or Not SignalDessinable)
          End With
        End If
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuPiétons.Verrouiller")
    End Try
    'End If

  End Sub

  '**********************************************************************
  ' Retourne la position par défaut du signal lié à la ligne de feux piétons
  ' Index : 0 ou 1 (il peut y avoir 1 ou 2 signaux pour représenter la ligne)
  '**********************************************************************
  Public Overrides Function PositionSignal(ByVal Index As Short) As Point
    Dim AngleRot As Single
    Dim p1, p2 As Point
    Dim Distance As Single

    With mTraversée
      p1 = PointDessin(Milieu(.Points(0), .Points(1)))
      If .mDouble Then
        p2 = PointDessin(Milieu(.Points(4), .Points(5)))
      Else
        p2 = PointDessin(Milieu(.Points(2), .Points(3)))
      End If
    End With

    If Index = 0 Then
      AngleRot = AngleFormé(p2, p1)
    Else
      AngleRot = AngleFormé(p1, p2)
    End If

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Distance = 32 '32 pixels
    Else
      Distance = 5  '5mm de l'extrémité de la flèche représentant la traversée piétonne
    End If

    Return PointPosition(New Point(0, 0), Distance, AngleRot)

  End Function

  Public Sub EffacerSignaux(ByVal uneCollection As Graphiques)
    Dim i As Short

    For i = 0 To mSignalFeu.Length - 1
      If SignalAReprésenter(i) Then uneCollection.Remove(mSignalFeu(i).mGraphique)
    Next

  End Sub

  Public Overrides Sub DéterminerLargeurDégagement()
    '    LgDégagement = CType(mTrajectoires(0), TraverséePiétonne).LgMaximum
    'Modif Proto v12 suite à demande CERTU : Dossier de suivi Circulation Point 13 (Décemebre 2006)
    LgDégagement = CType(mTrajectoires(0), TraverséePiétonne).LgMédiane
  End Sub

  Public ReadOnly Property mTraversée() As TraverséePiétonne
    Get
      Return CType(mTrajectoires(0), TraverséePiétonne)
    End Get
  End Property

  Public Overrides Sub CréerGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)
    Dim pCentre As PointF

    If unePhase.mLignesFeux.Contains(Me) Then
      ' Dessiner la traversée piétonne (Flèche)
      mTraversée.CréerGraphique(uneCollection)

      ' Ecrire le numéro de ligne de feux dans un cercle au même endroit que le signal de feu sur le schéma du carrefour
      'Dim p1 As Point = mSignalFeu(0).PtRéférence
      Dim uneFlèche As Fleche = mTraversée.Flèche
      Dim p1 As PointF = uneFlèche.ptRéférence(0)
      p1 = Translation(p1, CvPointF(PositionSignal(0)))
      Dim p2 As PointF = uneFlèche.ptRéférence(1)
      p2 = Translation(p2, CvPointF(PositionSignal(1)))
      Dim Distance1, Distance2 As Single

      'If SignalAReprésenter(1) Then

      '  Distance1 = Formules.Distance(p1, mBranche.BordChaussée(Branche.Latéralité.Gauche))
      '  Distance2 = Formules.Distance(p2, mBranche.BordChaussée(Branche.Latéralité.Gauche))
      '  If Distance1 < Distance2 Then
      '    pCentre = p1
      '  Else
      '    pCentre = p2
      '  End If

      'Else
      '  pCentre = p1
      'End If

      'DessinerNuméroLigne(pCentre, unePhase.mGraphiqueNumérosFeux)

      Distance1 = Formules.Distance(p1, PointDessinF(mVariante.Centre))
      Distance2 = Formules.Distance(p2, PointDessinF(mVariante.Centre))
      If Distance1 > Distance2 Then
        pCentre = p1
      Else
        pCentre = p2
      End If
      DessinerNuméroLigne(pCentre, unePhase.mGraphiqueNumérosFeux)
    End If

  End Sub

  Public Overrides Sub DéterminerAutorisationDécalage(ByVal unePhase As Phase)
    mDécalageOuvertureAutorisé = True
  End Sub

  Public Overrides Sub CréerGraphiqueDégagement(ByVal uneCollection As Graphiques)
    Dim unAngle As Single
    Dim p As Point

    With mTraversée
      .CréerGraphique(uneCollection)
      With .Flèche
        unAngle = .Angle
        p = PointPosition(mSignalFeu(0).PtRéférence, .Longueur / 2, unAngle)
      End With
    End With

    mTraversée.mGraphique.Add(TexteLgDégagement(p, unAngle))

  End Sub

  Public Overrides Function DuréeJaune() As Short
    ' Pas de jaune pour les piétons
    Return 0
  End Function

End Class
