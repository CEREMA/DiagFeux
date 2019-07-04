'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : LigneFeux.vb																							'
'						Classes																														'
'							LigneFeux																												'
'							LigneFeuxCollection																							'
'							LigneFeuV�hicules																							'
'							LigneFeuPi�tons																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe LigneFeux --------------------------
'=====================================================================================================
Public MustInherit Class LigneFeux : Inherits M�tier
  'Une ligne de feux est soit une ligne de feux v�hicules, soit une ligne de feux pi�tons
  Public MustOverride Sub Verrouiller()
  Public MustOverride Sub D�terminerLargeurD�gagement()
  Public MustOverride Sub D�terminerAutorisationD�calage(ByVal unePhase As Phase)
  Public MustOverride Sub Cr�erGraphiqueD�gagement(ByVal uneCollection As Graphiques)
  Public MustOverride Function Dur�eJaune() As Short

  '***********************************************************************************
  ' Retourne la position par d�faut du signal li� � la ligne de feux
  ' Index : 0 ou 1 (il peut y avoir 1 ou 2 signaux pour repr�senter une ligne Pi�tons)
  '***********************************************************************************
  Public MustOverride Function PositionSignal(ByVal Index As Short) As Point
  Public MustOverride Sub Cr�erGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)

  Public Const MaxiRougeD�gagement As Short = 99

  Public mBranche As Branche
  Public mVariante As Variante

  'Signal associ� � la ligne de feux
  '##ModelId=3C72748F0148
  '	Private Signal As Global.SignalEnum

  'Code du feu (Identifiant unique)
  '##ModelId=3C72750400BB
  Public ID As String

  'Signaux physiques associ�s � la ligne de feux
  'Sera redimensionn� � 1 pour un feu pi�tons
  Public mSignalFeu(0) As SignalFeu

  '##ModelId=3C8B7FBE033C
  Public mTrajectoires As TrajectoireCollection

  Private mLgD�gagement As Single

  Protected mD�calageOuvertureAutoris� As Boolean

  Public ReadOnly Property D�calageOuvertureAutoris�() As Boolean
    Get
      Return mD�calageOuvertureAutoris�
    End Get
  End Property

  Public Property LgD�gagement() As Single
    Get
      Return mLgD�gagement
    End Get
    Set(ByVal Value As Single)
      mLgD�gagement = Value
    End Set
  End Property

  Public Property Signal() As Signal
    ' Tous les signaux physiques d'une ligne de feux sont associ�s au m�me signal
    Get
      Return mSignalFeu(0).mSignal
    End Get
    Set(ByVal Value As Signal)
      mSignalFeu(0).mSignal = Value
    End Set
  End Property

  'Temps de rouge de d�gagement entre cette ligne de feux et uneLigneFeux'ensemble des lignes de feux du carrefour
  '##ModelId=3C8B300F00CB
  Public Sub TempsD�gagementTotal()
    'No implementation (abstract class)
  End Sub

  Public Overridable Function ToutesVoiesSurBranche() As Boolean
  End Function

  '********************************************************************************************************************
  ' Construit la chaine � afficher dans la ligne de saisie des lignes de feux � partir des propri�t�s de la ligne de feux
  '********************************************************************************************************************
  Public Overridable Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal S�parateur As Char) As String
    Dim strLigne As String

    With Me
      Dim uneBranche As Branche = .mBranche

      strLigne = desBranches.ID(uneBranche) & S�parateur
      strLigne &= uneBranche.NomRue & S�parateur
      strLigne &= .ID & S�parateur
      strLigne &= cndSignaux.strCode(.Signal) & S�parateur
    End With

    Return strLigne

  End Function

  '********************************************************************************************************************
  ' Une ligne de feux est trivialement compatible avec elle-m�me
  ' 2 lignes de feux pi�tons le sont �galement
  '********************************************************************************************************************
  Public ReadOnly Property EstTrivialementCompatible(ByVal uneLigneFeux As LigneFeux) As Boolean

    Get
      Try
        If uneLigneFeux Is Me Then
          EstTrivialementCompatible = True
        ElseIf Me.EstPi�ton And uneLigneFeux.EstPi�ton Then
          EstTrivialementCompatible = True
        End If

      Catch ex As System.Exception
        Throw New DiagFeux.Exception(ex.Message)
      End Try
    End Get
  End Property

  Public ReadOnly Property EstPi�ton() As Boolean
    Get
      Return TypeOf Me Is LigneFeuPi�tons
    End Get
  End Property

  Public ReadOnly Property EstV�hicule() As Boolean
    Get
      Return TypeOf Me Is LigneFeuV�hicules
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
      If EstPi�ton Then
        With mSignalFeu(1)
          uneRowSignalFeu = ds.Signal.AddSignalRow(Signal.strCode, .Position.X, .Position.Y, uneRowLigneDeFeux)
        End With
      End If

      'If desLignesFeux.mRougesD�gagement.Length > 0 Then
      If desLignesFeux.colRougesD�gagement.Count > 0 Then
        'Lignes de feux verrouill�es : les incompatibilit�s sont initialis�es
        Dim uneLigneAdverse As LigneFeux

        For Each uneLigneAdverse In desLignesFeux
          'Incompatibilit�s

          If desLignesFeux.EstIncompatible(Me, uneLigneAdverse) Then
            If desLignesFeux.IndexOf(uneLigneAdverse) > desLignesFeux.IndexOf(Me) Then
              'On n'�crit qu'une fois l'incompatibilit� : si F1 incompatible avec F2 inutile d'�crire que F2 l'est avec F1
              ds.Incompatible.AddIncompatibleRow(uneLigneAdverse.ID, uneRowLigneDeFeux)
            End If
          End If

          'Rouges de d�gagement

          'Solution 1 : toujours �crire une valeur

          ds.RougeD�gagement.AddRougeD�gagementRow(uneLigneAdverse.ID, desLignesFeux.RougeD�gagement(Me, uneLigneAdverse), uneRowLigneDeFeux)
          'Solution 2 :  n'�crire que si la valeur est renseign�e
          'If mRougesD�gagement.Contains(uneLigneAdverse) Then
          '  ds.RougeD�gagement.AddRougeD�gagementRow(uneLigneAdverse.ID, RougeD�gagement(uneLigneAdverse), uneRowLigneDeFeux)
          'End If
          'Solution 3 : n'�crire que si la valeur est positive
          'If Me.RougeD�gagement(uneLigneAdverse) <> 0 Then
          '  ds.RougeD�gagement.AddRougeD�gagementRow(uneLigneAdverse.ID, RougeD�gagement(uneLigneAdverse), uneRowLigneDeFeux)
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
  ' Retourne sous forme d'une chaine le num�ro absolu de la ligne de feux dans la collection de la variante courante
  ' La num�rotation  commence � "1"
  '****************************************************************************************************************
  Public Function strNum�ro() As String
    strNum�ro = (mVariante.mLignesFeux.IndexOf(Me) + 1).ToString
  End Function

  Public Sub DessinerNum�roLigne(ByVal pRef As PointF, ByVal unGraphique As PolyArc)
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseNum�roImpression).Clone
    Dim uneBrosse As SolidBrush = cndPlumes.Brosse(Plumes.BrosseEnum.PhaseNum�roImpression).Clone

    'unGraphique.Cr�erCercleTexte(pRef, Rayon:=2, unePlume:=unePlume, Chaine:=strNum�ro, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    ' v11
    '    unGraphique.Cr�erCercleTexte(pRef, Rayon:=3, unePlume:=unePlume, Chaine:=ID, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    ' v12
    'pRefImprNum�ro = pRef
    Dim GraphiqueNum�ro As PolyArc = unGraphique.Cr�erCercleTexte(pRef, Rayon:=Phase.RayonCercleLF, unePlume:=unePlume, Chaine:=ID, uneBrosse:=uneBrosse, uneFonte:=New Font("Arial", 7))
    GraphiqueNum�ro.ObjetM�tier = Me

  End Sub

  Protected ReadOnly Property SignalDessinable() As Boolean
    Get
      Return mVariante.SignalDessinable
    End Get
  End Property

  Protected Function TexteLgD�gagement(ByVal p As Point, ByVal unAngle As Single) As Texte
    Dim D�calage As Short

    If EstPi�ton Then
      D�calage = 2
    Else
      If AlignementTexte(unAngle) = StringAlignment.Center Then
        D�calage = 5
      Else
        D�calage = 3
      End If
    End If

    p.Y -= 2
    Dim PositionTexte As Point = PointPosition(p, D�calage, unAngle)

    Dim unTexte As New Texte(Format(Me.LgD�gagement, "##"), New SolidBrush(Color.Green), New Font("Arial", 8), PositionTexte, Formules.AlignementTexte(unAngle))

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

  Public mRougesD�gagement(-1) As Hashtable
  Private colIncompatibles As New SortedList
  Public colRougesD�gagement As New SortedList

  Public Enum OrdreDeTriEnum
    V�hiculesEnT�te = 1
    OrdreBranche = 2
    OrdreCodeFeu = 3
    OrdrePhase = 4
  End Enum

  Private OrdreDeTri As OrdreDeTriEnum

  ' Cr�er une instance la collection
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

  Public Sub Dimensionner(Optional ByVal RemiseAZ�ro As Boolean = False)

    If RemiseAZ�ro Then
      colRougesD�gagement.Clear()
      colIncompatibles.Clear()
    Else

      Dim uneLigneFeux As LigneFeux
      For Each uneLigneFeux In Me
        colIncompatibles.Add(uneLigneFeux.ID, New Hashtable)
        colRougesD�gagement.Add(uneLigneFeux.ID, New Hashtable)
      Next

    End If
  End Sub

  Public Sub R�initialiserVoies()
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstV�hicule Then
        CType(uneLigneFeux, LigneFeuV�hicules).Voies.Clear()
      End If
    Next

  End Sub

  '*************************************************************************************************
  ' Cloner les incompatibilit�s de la collection de lignes de feux
  ' A leur initialisation, chaque sc�nario r�cup�re les conflits syst�matiques des lignes de feux de la variante
  ' desLignesFeux : collection des lignes de feux de la variante
  '*************************************************************************************************
  Public Sub ClonerIncompatibilit�s(ByVal desLignesFeux As LigneFeuxCollection)
    Dim uneLigneFeux As LigneFeux
    Dim unEnum�rateur As IDictionaryEnumerator
    Dim IDAdverse As String
    Dim uneHashtable As Hashtable

    Try

      Dimensionner()

      For Each uneLigneFeux In desLignesFeux
        uneHashtable = desLignesFeux.colIncompatibles(uneLigneFeux.ID)
        unEnum�rateur = uneHashtable.GetEnumerator

        Do While unEnum�rateur.MoveNext
          IDAdverse = unEnum�rateur.Key
          EstIncompatible(Item(uneLigneFeux.ID), Item(IDAdverse)) = True
        Loop

        InitialiserTempsD�gagement(desLignesFeux)

      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LignesFeux.ClonerIncompatibilit�s")

    End Try
  End Sub

  Public Property EstIncompatible(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Boolean
    Get
      If colIncompatibles.Count = 0 Then
        'Lignes de feux non verrouill�es
        Return False

      Else
        Dim uneHashtable As Hashtable = colIncompatibles(uneLigneFeux.ID)
        Return uneHashtable.ContainsValue(uneLigneAdverse)
      End If

    End Get
    Set(ByVal Value As Boolean)
      Dim uneHashtable As Hashtable = colIncompatibles(uneLigneFeux.ID)

      If uneHashtable.ContainsValue(uneLigneAdverse) Xor Value Then
        'Compatibilit� modifi�e
        If Value Then
          uneHashtable.Add(uneLigneAdverse.ID, uneLigneAdverse)
        Else
          uneHashtable.Remove(uneLigneAdverse.ID)
        End If

        'L'incompatibilit� est sym�trique
        EstIncompatible(uneLigneAdverse, uneLigneFeux) = Value
      End If
    End Set
  End Property

  Public Property RougeD�gagement(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Dim uneHashtable As Hashtable = colRougesD�gagement(uneLigneFeux.ID)
      If uneHashtable.Contains(uneLigneAdverse) Then Return uneHashtable.Item(uneLigneAdverse)
    End Get
    Set(ByVal Value As Short)
      Dim uneHashtable As Hashtable = colRougesD�gagement(uneLigneFeux.ID)
      uneHashtable.Item(uneLigneAdverse) = Value
    End Set
  End Property

  '*************************************************************************************************************
  ' Temps de d�gagement entre 2 lignes de feux
  '*************************************************************************************************************
  Public Property TempsD�gagement(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Return RougeD�gagement(uneLigneFeux, uneLigneAdverse)
    End Get
    Set(ByVal Value As Short)
      RougeD�gagement(uneLigneFeux, uneLigneAdverse) = Value
    End Set
  End Property

  '*************************************************************************************************************
  ' Intervert : temps entre la fin du vert et le d�but du vert de Ligne Adverse
  '*************************************************************************************************************
  Public ReadOnly Property InterVerts(ByVal uneLigneFeux As LigneFeux, ByVal uneLigneAdverse As LigneFeux) As Short
    Get
      Return TempsD�gagement(uneLigneFeux, uneLigneAdverse) + uneLigneFeux.Dur�eJaune()
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

  Public Function Cr�erGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim uneLigneFeux As LigneFeux

    For Each uneLigneFeux In Me
      If cndFlagImpression = dlgImpressions.ImpressionEnum.Matrice Then
        If PhaseActiveImpressionRougeD�gagement.mLignesFeux.Contains(uneLigneFeux) Then
          uneLigneFeux.Cr�erGraphiqueD�gagement(uneCollection)
        End If
      Else
        uneLigneFeux.Cr�erGraphique(uneCollection)
      End If
    Next

  End Function

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal uneLigneFeux As LigneFeux) As Short
    Add = Count
    Insert(Count, uneLigneFeux)

  End Function

  Public Function Add(ByVal uneLigneFeux As LigneFeux, ByVal uneCollection As Graphiques) As Short
    Add = Count
    Insert(Count, uneLigneFeux)
    uneLigneFeux.Cr�erGraphique(uneCollection)

  End Function

  '**********************************************************************************************************
  'ColonneModifi�e, ValeurModifi�e :Colonne et Valeur en cours de validation
  'Chaine : ensemble des champs de la ligne
  'NumLigneFeux : Index de la ligne de feux (commence � 0)
  '**********************************************************************************************************
  Public Function MettreAjour(ByVal ValeurModifi�e As String, ByVal Bool�en As Boolean, ByVal chaine As String, ByVal NumLigneFeux As Short, ByVal ColonneModifi�e As Short) As LigneFeux
    Dim CodeVoie As Char
    Dim NomRue As String
    Dim ID, exID As String
    Dim CodeFeu As String
    Dim CodeFeuAssoci� As String
    Dim NbVoies As Short
    Dim blnTAD As Boolean
    Dim blnTD As Boolean
    Dim blnTAG As Boolean

    Dim uneLigneFeux As LigneFeux = Nothing
    'Alimentation des donn�es avec les valeurs actuelles de la ligne du tableau - le code tabulation(chr(9)) sert de s�parateur
    Dim tValeurs() As String = Split(chaine, Chr(9))

    Try

      CodeVoie = tValeurs(0)
      NomRue = tValeurs(1)
      ID = tValeurs(2)
      CodeFeu = tValeurs(3)
      CodeFeuAssoci� = tValeurs(4)
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

      'La valeur qui vient d'�tre saisie n'est pas encore valid�e : il faut la substituer � celle aliment�e ci-dessus
      Select Case ColonneModifi�e
        Case 0
          CodeVoie = ValeurModifi�e
        Case 1
          NomRue = ValeurModifi�e
        Case 2
          ID = ValeurModifi�e
          If NumLigneFeux < Me.Count Then
            uneLigneFeux = Me.Item(NumLigneFeux)
            exID = uneLigneFeux.ID
            If exID <> ID Then
              'Supprimer et recr�er la  ligne de feux avec son nouvel ID
              uneLigneFeux.ID = ID
              Substituer(uneLigneFeux, exID)
            End If
          End If
        Case 3
          CodeFeu = ValeurModifi�e
        Case 4
          CodeFeuAssoci� = ValeurModifi�e
        Case 5
          NbVoies = CType(ValeurModifi�e, Short)
        Case 6
          blnTAG = Bool�en
        Case 7
          blnTD = Bool�en
        Case 8
          blnTAD = Bool�en
      End Select

      If CodeVoie <> "" And ID.Length > 0 And CodeFeu.Length > 0 Then
        'On ne cr�e pas l'instance LigneFeux tq ces 3 donn�es ne sont pas d�finies
        Dim uneBranche As Branche = mVariante.mBranches(CodeVoie)
        Dim unSignal As Signal = cndSignaux(CodeFeu)

        If dctLigneFeux.Contains(ID) Then
          uneLigneFeux = Me(ID)

          Select Case ColonneModifi�e
            Case 0
              'Mode tableur : changement de branche de la LF
              SubstituerBranche(uneLigneFeux, uneBranche)
            Case 1
              uneLigneFeux.mBranche.NomRue = ValeurModifi�e
            Case 2
              uneLigneFeux.ID = ID
            Case 3
              If uneLigneFeux.EstPi�ton Xor unSignal.EstPi�ton Then
                'Passage d'une ligne v�hicules � une ligne pi�tons ou inversement
                uneLigneFeux = Substituer(uneLigneFeux, unSignal)
              Else
                uneLigneFeux.mSignalFeu(0).mSignal = unSignal
              End If
          End Select

        Else
          If unSignal.EstPi�ton Then
            uneLigneFeux = New LigneFeuPi�tons(ID, uneBranche, unSignal)
          Else
            uneLigneFeux = New LigneFeuV�hicules(ID, uneBranche, unSignal)
          End If
          Me.Add(uneLigneFeux)
        End If

        If uneLigneFeux.EstV�hicule Then
          With CType(uneLigneFeux, LigneFeuV�hicules)
            If CodeFeuAssoci�.Length = 0 Then
              .mSignalAnticipation = Nothing
            Else
              .mSignalAnticipation = New SignalFeu(cndSignaux(CodeFeuAssoci�), uneLigneFeux)
            End If

            'En mode tableur, pour une nouvelle ligne v�hicules, il est int�ressant d'initialiser � 1 le nombre de voies
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

    If uneLigneFeux.EstV�hicule Then
      Dim uneLigneV�hicules As LigneFeuV�hicules = uneLigneFeux
      Dim nbVoies As Short = uneLigneV�hicules.NbVoiesTableur
      uneLigneFeux.mBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) -= nbVoies
      uneBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) += nbVoies
    End If

    uneLigneFeux.mBranche = uneBranche

  End Sub

  '**********************************************************************************************************
  'Substituer une ligne feux � une autre suite � son changement de type : v�hicules<-->pi�tons
  '**********************************************************************************************************
  Private Overloads Function Substituer(ByVal uneLigneFeux As LigneFeux, ByVal unSignal As Signal) As LigneFeux
    Dim newLigneFeux As LigneFeux
    Dim Index As Short = Me.IndexOf(uneLigneFeux)

    With uneLigneFeux
      If unSignal.EstPi�ton Then
        newLigneFeux = New LigneFeuPi�tons(.ID, .mBranche, unSignal)
        Dim nbVoies As Short = CType(uneLigneFeux, LigneFeuV�hicules).NbVoiesTableur
        .mBranche.NbVoies(Voie.TypeVoieEnum.VoieEntrante) -= nbVoies
      Else
        newLigneFeux = New LigneFeuV�hicules(.ID, .mBranche, unSignal)
      End If

    End With

    Me.Remove(uneLigneFeux)
    Me.Insert(Index, newLigneFeux)
    Return newLigneFeux

  End Function

  '**********************************************************************************************************
  'Substituer une ligne feux � une autre suite � son changement d'ID (donc de cl�) 
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

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As LigneFeux)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal uneLigneFeux As LigneFeux)
    If Me.List.Contains(uneLigneFeux) Then
      Me.List.Remove(uneLigneFeux)
    End If

  End Sub

  Public Sub Remove(ByVal uneLigneFeux As LigneFeux, ByVal uneCollection As Graphiques)
    If uneLigneFeux.EstPi�ton Then
      CType(uneLigneFeux, LigneFeuPi�tons).EffacerSignaux(uneCollection)
    End If

    Remove(uneLigneFeux)

  End Sub

  Protected Overrides Sub OnRemove(ByVal Index As Integer, ByVal uneLigne As Object)
    Me.dctLigneFeux.Remove(CType(uneLigne, LigneFeux).ID)
  End Sub 'OnRemove

  Protected Overrides Sub OnClear()
    Me.dctLigneFeux.Clear()
  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneLigne As LigneFeux)
    'Ajouter l'objet au dictionnaire associ�
    If IsNothing(uneLigne.ID) Then
      uneLigne.ID = RechercherIndicelibreID(TypeOf uneLigne Is LigneFeuPi�tons)
    End If
    dctLigneFeux.Add(uneLigne.ID, uneLigne)

    'Ajout � la collection
    Me.List.Insert(Index, uneLigne)

  End Sub

  Private Function RechercherIndicelibreID(ByVal EstPi�ton As Boolean) As String
    Dim i As Short
    Dim str As String
    Dim Pr�fixe As String = IIf(EstPi�ton, "P", "F")
    Do
      i += 1
      str = Pr�fixe & CStr(i)
    Loop Until Not Me.Contains(str)
    Return str

  End Function

  '************************************************************************************
  ' Cette fonction permet de trouver l'index de la derni�re ligne v�hicules
  ' afin d'ins�rer une ligne v�hicules entre celle-ci et la 1�re ligne pi�tons
  ' Si l'utilisateur a bouscul� cet ordre, la ligne est ajout�e en fin de collection
  '************************************************************************************
  Public ReadOnly Property Premi�reLigneV�hiculeDispo() As Short
    Get
      Dim Pi�tonTrouv� As Boolean
      Dim i As Short
      Dim uneLigneFeux As LigneFeux
      For Each uneLigneFeux In Me
        If uneLigneFeux.EstV�hicule Then
          If Pi�tonTrouv� Then
            'L'utilisateur a d�j� ins�r� des lignes pi�tons entre les lignes v�hicules : ajouter la ligne v�hicules � la fin
            Return Me.Count
          Else
            i += 1
          End If
        Else
          Pi�tonTrouv� = True
        End If
      Next
      Return i
    End Get
  End Property

  Public Function nbLignesV�hicules() As Short
    Dim uneLigne As LigneFeux

    For Each uneLigne In Me
      If uneLigne.EstV�hicule Then nbLignesV�hicules += 1
    Next

    Return nbLignesV�hicules
  End Function

  Public Function nbLignesPi�tons() As Short
    Return Count - nbLignesV�hicules()
  End Function

  '************************************************************************************
  ' D�placer une ligne de feux dans la collection
  ' D�calage : indique de combien (en + ou en -) il faut d�caler la ligne
  ' uneLigne : Ligne de feux � d�caler
  '************************************************************************************
  Public Sub D�caler(ByVal D�calage As Short, ByVal uneLigne As LigneFeux)
    Dim NewPosition As Short = Me.IndexOf(uneLigne) + D�calage

    Me.Remove(uneLigne)
    Me.Insert(NewPosition, uneLigne)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As LigneFeux
    Get
      Return CType(Me.List(Index), LigneFeux)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par nom.
  Default Public ReadOnly Property Item(ByVal ID As String) As LigneFeux
    Get
      If dctLigneFeux.Contains(ID) Then Return dctLigneFeux(ID)
    End Get
  End Property

  Public Function IndexOf(ByVal uneLigneFeux As LigneFeux) As Short
    Return Me.List.IndexOf(uneLigneFeux)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Overloads Function Contains(ByVal uneLigne As LigneFeux) As Boolean
    Return Me.List.Contains(uneLigne)
  End Function

  Public Overloads Function Contains(ByVal ID As String) As Boolean

    Contains = Me.dctLigneFeux.Contains(ID)

  End Function

  Public Sub R�initialiserAntagos(ByVal desAntagonismes As AntagonismeCollection)
    Dim L1, L2 As LigneFeux

    For Each L1 In Me
      For Each L2 In Me
        If EstIncompatible(L1, L2) Then
          Select Case desAntagonismes.ExisteConflit(L1, L2)
            Case Trajectoire.TypeConflitEnum.Syst�matique
            Case Else
              EstIncompatible(L1, L2) = False
          End Select
        End If
      Next
    Next
  End Sub

  '**********************************************************************************************
  'Suite � la r�initialisation des antagonismes du plan de feux de base
  'Toutes ses lignes de feux redeviennent incompatibles, sauf les incompatibilit�s syst�matiques
  '**********************************************************************************************
  Public Sub R�initialiserAntagos(ByVal desLignesFeux As LigneFeuxCollection)
    Dim L1, L2 As LigneFeux

    For Each L1 In Me
      For Each L2 In Me
        EstIncompatible(L1, L2) = desLignesFeux.EstIncompatible(L1, L2)
      Next
    Next
  End Sub

  '*************************************************************************************
  ' R�initialiser les compatiblilit�s suite au d�verrouillage des lignes de feux
  '*************************************************************************************
  Public Sub R�initialiserIncompatibilit�s()
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneAdverse As LigneFeux
    Dim uneLigneV�hicules As LigneFeuV�hicules
    Dim uneLignePi�tons As LigneFeuPi�tons

    For Each uneLigneFeux In Me
      For Each uneLigneAdverse In Me
        If IndexOf(uneLigneAdverse) > IndexOf(uneLigneFeux) Then
          EstIncompatible(uneLigneFeux, uneLigneAdverse) = False
        End If

        If uneLigneFeux.EstV�hicule And uneLigneAdverse.EstPi�ton Then
          uneLigneV�hicules = uneLigneFeux
          uneLignePi�tons = uneLigneAdverse
          uneLigneV�hicules.R�initialiserFiliations()
        End If
      Next
    Next

  End Sub

  '*************************************************************************************
  '*************************************************************************************
  Public Sub Cr�erIncompatibilit�s(ByVal desRowLignesDeFeux As DataSetDiagfeux.LigneDeFeuxRow())
    Dim uneLigneFeux As LigneFeux
    Dim uneRowRouge As DataSetDiagfeux.RougeD�gagementRow
    Dim i, j As Short

    Dimensionner()

    For i = 0 To desRowLignesDeFeux.Length - 1
      With desRowLignesDeFeux(i)
        uneLigneFeux = Item(.ID)

        'Incompatibilit�s
        For j = 0 To .GetIncompatibleRows.Length - 1
          EstIncompatible(uneLigneFeux, Item(.GetIncompatibleRows(j).IDAdverse)) = True
        Next

        'Temps de rouge de d�gagement
        For j = 0 To .GetRougeD�gagementRows.Length - 1
          uneRowRouge = .GetRougeD�gagementRows(j)
          RougeD�gagement(uneLigneFeux, Item(uneRowRouge.IDAdverse)) = uneRowRouge.RougeD�gagement_text
        Next
      End With
    Next

  End Sub

  '*************************************************************************************
  ' D�terminer la ligne de feux qui commande la trajectoire
  '*************************************************************************************
  Public Function D�terminerLignesFeux(ByVal uneTrajectoire As TrajectoireV�hicules) As LigneFeux
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxV�hicules As LigneFeuV�hicules
    Dim uneVoie As Voie

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstV�hicule Then
        uneLigneFeuxV�hicules = uneLigneFeux
        With uneTrajectoire
          For Each uneVoie In uneLigneFeuxV�hicules.Voies
            If uneVoie Is .Voie(TrajectoireV�hicules.OrigineDestEnum.Origine) Then
              .LigneFeu = uneLigneFeuxV�hicules
              .LigneFeu.mTrajectoires.Add(uneTrajectoire)
              uneLigneFeuxV�hicules.AjouterBrancheSortie(uneTrajectoire)
              Return uneLigneFeuxV�hicules
            End If
          Next
        End With

      End If  ' Ligne feux estv�hicule
    Next

  End Function

  '*************************************************************************************
  ' V�rifie si une ligne de feux ne coupe pas une des voies
  '*************************************************************************************
  Public Function VoiesCoup�es(ByVal desVoies As VoieCollection) As LigneFeuV�hicules

    Dim uneLigneFeux As LigneFeux
    Dim uneLigneFeuxV�hicules As LigneFeuV�hicules
    Dim uneVoie As Voie

    For Each uneLigneFeux In Me
      If uneLigneFeux.EstV�hicule Then
        uneLigneFeuxV�hicules = uneLigneFeux
        For Each uneVoie In uneLigneFeuxV�hicules.Voies
          If desVoies.Contains(uneVoie) Then Return uneLigneFeuxV�hicules
        Next
      End If
    Next
  End Function

  '*************************************************************************************
  ' D�terminer les largeurs de d�gagement entre les lignes de feux
  '*************************************************************************************
  Private Sub D�terminerLargeursD�gagement()
    Dim uneLigneFeux As LigneFeux

    'D�terminer d'abord les largeurs de d�gagement
    For Each uneLigneFeux In Me
      uneLigneFeux.D�terminerLargeurD�gagement()
    Next

  End Sub

  '*************************************************************************************
  ' D�terminer les temps de d�gagement mini entre les lignes de feux
  'La valeur par d�faut du rouge de d�gagement du plan de feux de base 
  'est celui calcul� comme rouge mini pour les lignes de feux de la variante 
  'les temps mini de d�gagement seront affect�es aux temps de d�gagement des plans de feux de base � leur initialisation
  '*************************************************************************************
  Public Sub D�terminerTempsD�gagement()
    Dim Li, Lj As LigneFeux

    D�terminerLargeursD�gagement()

    For Each Li In Me
      If Li.EstV�hicule Then
        'Il n'y a pas de temps de d�gagement entre 2 lignes pi�tons 
        'parcourir uniquement les lignes de v�hicules
        For Each Lj In Me
          If Lj.EstV�hicule Then

            If Not Li.EstTrivialementCompatible(Lj) Then
              'Sinon : temps de d�gagement = 0
              RougeD�gagement(Lj, Li) = Sp�cialArrondiD�gagement(Lj)
              RougeD�gagement(Li, Lj) = Sp�cialArrondiD�gagement(Li)
            End If

          Else
            'Lj est pi�ton

            If Not Li.EstTrivialementCompatible(Lj) Then
              If Li.mBranche Is Lj.mBranche Then
                'Pas de rouge de d�gagement pour la ligne v�hicules car au m�me niveau que le passage pi�tons
                'Ajout (AV : 15/09/06) : Rapport du CERTU sur la v11(09/06) : Points 12 et 19 de Conflits 
                RougeD�gagement(Li, Lj) = Sp�cialArrondiD�gagement(Li, Lj)
                'Rouge de d�gagement pi�tons = dur�e de la travers�e pi�tonne
                RougeD�gagement(Lj, Li) = Sp�cialArrondiD�gagement(Lj)
              Else
                RougeD�gagement(Li, Lj) = Sp�cialArrondiD�gagement(Li)
                'D�duire du temps de d�gagement des pi�tons le temps que vont mettre les v�hicules � traverser le carrefour
                'Sans que cette valeur ne devienne quand m�me n�gative
                RougeD�gagement(Lj, Li) = Math.Max(0, Sp�cialArrondiD�gagement(Lj) - RougeD�gagement(Li, Lj))
              End If
            End If
          End If

        Next
      End If
    Next

  End Sub

  Private Function Sp�cialArrondiD�gagement(ByVal uneLigneFeux As LigneFeux, Optional ByVal uneLignePi�tons As LigneFeuPi�tons = Nothing) As Short
    Dim RougeD�gagement As Single
    Dim Vitesse As Single

    With mVariante.Param
      If IsNothing(uneLignePi�tons) Then
        If uneLigneFeux.EstV�hicule Then
          Vitesse = .VitesseV�hicules
        Else
          Vitesse = .VitessePi�tons
        End If

        RougeD�gagement = uneLigneFeux.LgD�gagement / Vitesse

      Else
        'Cas particulier : Traitement d'une ligne de feux v�hicules situ�e (tr�s) en arri�re du passage pi�ton sur la m�me brnche(!!!!)
        Vitesse = .VitesseV�hicules
        Dim Largeur As Single
        Largeur = Distance(CType(uneLigneFeux, LigneFeuV�hicules).Dessin.Milieu, uneLignePi�tons.mTravers�e.Contour) / Echelle
        RougeD�gagement = Largeur / Vitesse
      End If
    End With

    If RougeD�gagement > 0.1 Then
      'Arrondir � la valeur sup�rieure au-del� de 0.1s
      'cf Rapport du CERTU sur la v11(09/06) : Points 12 et 19 de Conflits 
      Return Math.Ceiling(RougeD�gagement)
    Else
      Return 0
    End If

  End Function

  Public Sub InitialiserTempsD�gagement(ByVal desLignesFeux As LigneFeuxCollection)
    Dim Li, Lj As LigneFeux

    For Each Li In Me
      For Each Lj In Me
        RougeD�gagement(Li, Lj) = desLignesFeux.RougeD�gagement(Li, Lj)
        RougeD�gagement(Lj, Li) = desLignesFeux.RougeD�gagement(Lj, Li)
      Next
    Next

  End Sub

  Public Sub Trier(ByVal Ordre As OrdreDeTriEnum)
    Dim uneLigneFeux As LigneFeux
    Dim i As Short
    Dim Indice(Count - 1) As Short
    Dim D�calage As Short

    OrdreDeTri = Ordre

    'Consid�rer au d�part que le tableau est correctement ordonn�
    For i = 0 To Count - 1
      Indice(i) = i
    Next

    'Algorithme de tri insertion : � la fin de la boucle, indice(i) sera un tableau ordonn� des positions actuelles dans la collection des lignes de feux
    For i = 1 To Count - 1
      Insertion(i, Indice)
    Next

    'M�moriser les lignes de feux avec l'ordre actuel
    Dim M�moLignes As New LigneFeuxCollection
    For Each uneLigneFeux In Me
      M�moLignes.Add(uneLigneFeux)
    Next

    'Remise en ordre des lignes de feux
    For i = 0 To Count - 1
      'i correspond � la nouvelle position de la  ligne de feux dont la position actuelle est m�moris�e dans Indice(i)
      uneLigneFeux = M�moLignes(Indice(i))
      ' Calculer le d�calage entre la position actuelle et la position souhait�e
      D�calage = i - IndexOf(uneLigneFeux)
      ' D�caler si n�cessaire la ligne de feux
      If D�calage <> 0 Then D�caler(D�calage, uneLigneFeux)
    Next

    M�moLignes.Clear()

  End Sub

  Private Sub Insertion(ByVal droite_local As Short, ByRef Indice() As Short)
    Dim i As Short = droite_local - 1
    Dim sauv As Short = Indice(droite_local)

    Do While Sup�rieur(Item(Indice(i)), Item(sauv))
      Indice(i + 1) = Indice(i)
      i -= 1
      If i = -1 Then Exit Do
    Loop

    Indice(i + 1) = sauv
  End Sub

  Private Function Sup�rieur(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Boolean

    Select Case OrdreDeTri
      Case OrdreDeTriEnum.OrdreBranche
        Dim desBranches As BrancheCollection = mVariante.mBranches
        Select Case Math.Sign(desBranches.IndexOf(L1.mBranche) - desBranches.IndexOf(L2.mBranche))
          Case 1
            Sup�rieur = True
          Case 0
            If L1.EstPi�ton And L2.EstV�hicule Then Sup�rieur = True
        End Select
      Case OrdreDeTriEnum.V�hiculesEnT�te
        If L1.EstPi�ton And L2.EstV�hicule Then Sup�rieur = True

      Case OrdreDeTriEnum.OrdreCodeFeu
        Sup�rieur = String.Compare(L1.ID, L2.ID) > 0

      Case OrdreDeTriEnum.OrdrePhase
        Return mPlanFeux.Sup�rieur(L1, L2)
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
'--------------------------- Classe LigneFeuV�hicules--------------------------
'=====================================================================================================
Public Class LigneFeuV�hicules : Inherits LigneFeux
  'Ligne d'effets de feux : correspond � la ligne d'arr�t des v�hicules sur la chauss�e.


  'D�calage de la ligne de feu par rapport � l'origine de la branche.
  'Inutilis� dans le mode non graphique
  '##ModelId=3C72725C01F4
  Private mD�calage As Single

  'Nombre de voies de circulation command�es par la ligne de feux.
  'Saisi dans le mode non graphique
  '##ModelId=3C7272E3038A
  Public NbVoiesTableur As Short

  'Indice de la voie coup�e par la ligne de feux la plus � droite.
  'Utile en particulier s'il y a une phase sp�ciale pour le TAG.
  '##ModelId=3C72732F0138
  '  Private NumVoie1 As Short

  Private mVoies As New VoieCollection
  Private mTrajectoirePrincipale As TrajectoireV�hicules
  Private mBranchesSortie As New BrancheCollection

  'Signal d'anticipation �ventuel associ� � la ligne de feux
  '##ModelId=3C7274DB002E
  Public mSignalAnticipation As SignalFeu

  'Indique si la ligne coupe un courant Tourne � gauche
  'Saisi dans le mode non graphique
  '##ModelId=3C7275310280
  Public TAG As Boolean

  'Indique si la ligne coupe un courant Tourne � droite
  'Saisi dans le mode non graphique
  '##ModelId=3C727560037A
  Public TAD As Boolean
  Private mCoefG�ne(2) As Single

  'Indique si la ligne coupe un courant Tout droit
  'Saisi dans le mode non graphique
  '##ModelId=3C7275620109
  Public TD As Boolean

  'Demande (en uvp/s) de la file 
  'Private mDemandeUVP As Single
  '  Public nbV�hiculesEnAttente As Short

  Private dctFiliation As New Hashtable
  Private mDessin As Ligne

  Public ReadOnly Property TrajectoirePrincipale() As TrajectoireV�hicules
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
  ' Construit la chaine � afficher dans la ligne de saisie des lignes de feux � partir des propri�t�s de la ligne de feux
  '********************************************************************************************************************
  Public Overrides Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal S�parateur As Char) As String
    ' Construire le d�but de la ligne (commune V�hicules/Pi�tons)
    Dim s As String = MyBase.strLigneGrille(desBranches, S�parateur)

    With Me
      If IsNothing(.mSignalAnticipation) Then
        s &= S�parateur
      Else
        s &= cndSignaux.strCode(.mSignalAnticipation.mSignal) & S�parateur
      End If

      If mVariante.ModeGraphique Then
        s &= .nbVoies.ToString & S�parateur
        s &= IIf(.TAG, StrCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TAG), "") & S�parateur
        s &= IIf(.TD, StrCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TD), "") & S�parateur
        s &= IIf(.TAD, StrCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TAD), "") & S�parateur

        's &= IIf(.TAG, mCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TAG).ToString, "") & S�parateur
        's &= IIf(.TD, mCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TD).ToString, "") & S�parateur
        's &= IIf(.TAD, mCoefG�ne(TrajectoireV�hicules.NatureCourantEnum.TAD).ToString, "") & S�parateur

        's &= IIf(.TAG, "   X", "") & S�parateur
        's &= IIf(.TD, "   X", "") & S�parateur
        's &= IIf(.TAD, "   X", "") & S�parateur

      Else
        s &= .NbVoiesTableur.ToString & S�parateur
        s &= .TAG.ToString & S�parateur
        s &= .TD.ToString & S�parateur
        s &= .TAD.ToString & S�parateur
      End If
    End With

    Return s

  End Function

  Private Function StrCoefG�ne(ByVal NatureCourant As TrajectoireV�hicules.NatureCourantEnum) As String
    If mCoefG�ne(NatureCourant) = -1 Then ' ou 0 (?)
      Return " XX"
    Else
      Return mCoefG�ne(NatureCourant).ToString
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
        D�calage = .D�calage
        NumVoie1 = .NumVoie1
        'Ne sert que pour le mode tableur tq les LF ne sont pas verrouill�es
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
      LancerDiagfeuxException(ex, " : Lecture de la ligne de feux v�hicules")
    End Try

  End Sub

  Public ReadOnly Property nbVoies() As Short
    Get
      Return mVoies.Count
    End Get
  End Property

  Public Property D�calage() As Single
    Get
      Return mD�calage
    End Get
    Set(ByVal Value As Single)
      mD�calage = Value
    End Set
  End Property

  '********************************************************************************************************************
  ' Enregistrer la ligne de feux v�hicules dans le fichier
  ' Etape 1 : Cr�er les enregistrements n�cessaires dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow, ByVal desLignesFeux As LigneFeuxCollection) As DataSetDiagfeux.LigneDeFeuxRow
    'Enregistrer d'abord la ligne de deux

    Dim uneRowLigneDeFeux As DataSetDiagfeux.LigneDeFeuxRow = MyBase.Enregistrer(uneRowVariante, desLignesFeux)

    If Not IsNothing(uneRowLigneDeFeux) Then
      'Enregistrer les propri�t�s sp�cifiques aux v�hicules
      With uneRowLigneDeFeux
        .D�calage = D�calage
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

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    ' Le 1er point repr�sentant la ligne de feux est le point de la ligne de feux le plus � gauche dans la branche(rappel : la branche est orient�e � partir du centre du carrefour)
    Dim p1 As PointF = PointPosition(mVoies(mVoies.Count - 1).Extr�mit�(Branche.Lat�ralit�.Gauche), D�calage * Echelle, mBranche.AngleEnRadians)
    Dim p2 As PointF = PointPosition(mVoies(0).Extr�mit�(Branche.Lat�ralit�.Droite), D�calage * Echelle, mBranche.AngleEnRadians)

    Dim unePlume As Pen
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.LigneFeuV�hicule).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.LigneFeuV�hiculeImpression).Clone
    End If

    mDessin = New Ligne(p1, p2, unePlume)
    mGraphique.Add(mDessin)

    'If SignalDessinable Then
    With mSignalFeu(0)
      If .Position.Equals(New Point(0, 0)) Then
        'Intialiser avec la position par d�faut
        .Position = PositionSignal(0)
      End If
      .Cr�erGraphique(uneCollection)
      If Not SignalDessinable Then .mGraphique.Invisible = True
    End With
    'End If

    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  '********************************************************************************
  ' AGauche : indique si le signal doit �tre positionn� � gauche de la branche
  '********************************************************************************
  Public ReadOnly Property AGauche() As Boolean
    Get
      Dim nbVoies As Short = mVoies.Count
      With mBranche
        'AGauche, si 
        ' -la voie la +� gauche de la ligne de feux est la + � gauche de la branche 
        ' -la ligne de feux ne commande pas toutes les voies entrantes
        Return mVoies(nbVoies - 1) Is .Voies(.NbVoies(Voie.TypeVoieEnum.VoieSortante)) AndAlso _
        .NbVoies(Voie.TypeVoieEnum.VoieEntrante) > nbVoies
      End With
    End Get
  End Property

  Public Sub D�terminerNatureCourants(ByVal colTrajectoires As TrajectoireCollection)
    Dim uneTrajectoire As Trajectoire
    Dim uneTrajectoireV�hicules As TrajectoireV�hicules
    Dim uneVoie As Voie

    TAD = False
    TD = False
    TAG = False

    For Each uneTrajectoire In colTrajectoires
      If uneTrajectoire.EstV�hicule Then
        uneTrajectoireV�hicules = CType(uneTrajectoire, TrajectoireV�hicules)
        With uneTrajectoireV�hicules
          For Each uneVoie In mVoies
            If uneVoie Is .Voie(TrajectoireV�hicules.OrigineDestEnum.Origine) Then
              uneTrajectoire.LigneFeu = Me
              If Not mTrajectoires.Contains(uneTrajectoire) Then
                mTrajectoires.Add(uneTrajectoire)
                AjouterBrancheSortie(uneTrajectoireV�hicules)
              End If
              mCoefG�ne(.NatureCourant) = AffecterCoefG�ne(.NatureCourant, CType(uneTrajectoire, TrajectoireV�hicules).CoefG�ne)
            End If
          Next
        End With  ' uneTrajectoireV�hicules
      End If
    Next

  End Sub

  Public Sub AjouterBrancheSortie(ByVal uneTrajectoire As TrajectoireV�hicules)
    Dim uneBranche As Branche = uneTrajectoire.Voie(TrajectoireV�hicules.OrigineDestEnum.Destination).mBranche

    If Not mBranchesSortie.Contains(uneBranche) Then
      mBranchesSortie.Add(uneBranche)
    End If
  End Sub

  Private Function AffecterCoefG�ne(ByVal NatureCourant As TrajectoireV�hicules.NatureCourantEnum, ByVal Coefficient As Single) As Single

    Select Case NatureCourant
      Case TrajectoireV�hicules.NatureCourantEnum.TAG
        If TAG Then
          'Une trajectoire TAG est d�j� command�e par cette LF
          If Coefficient = mCoefG�ne(NatureCourant) Then
            Return Coefficient
          Else
            'Plusieurs coef g�ne TAG pour cette ligne de feux : -1 permettra d'afficher des croix dans le tableau pour l'indiquer
            Return -1
          End If
        Else
          TAG = True
          Return Coefficient
        End If

      Case TrajectoireV�hicules.NatureCourantEnum.TD
        If TD Then
          If Coefficient = mCoefG�ne(NatureCourant) Then
            Return Coefficient
          Else
            Return -1
          End If
        Else
          TD = True
          Return Coefficient
        End If
      Case TrajectoireV�hicules.NatureCourantEnum.TAD
        If TAD Then
          If Coefficient = mCoefG�ne(NatureCourant) Then
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

  Public Function D�terminerCourants() As Boolean
    Return mBranche.D�terminerCourants(Me)
  End Function

  Public ReadOnly Property VoiesAffect�esUnMouvement() As Boolean
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

  Public Function TraficPond�r�Riche(ByVal unTrafic As Trafic) As Integer
    Dim uneVoie As Voie
    Dim unCourant As Courant
    Dim desCourants As New CourantCollection
    Dim q, qPond�r� As Single
    Dim qCourant(-1), qCourantPond�r�(0) As Single
    Dim nbVoies(-1) As Short
    Dim i As Short

    'D�terminer tous les courants g�r�s par la ligne de feux
    For Each uneVoie In Voies
      For Each unCourant In uneVoie.mCourants
        If Not desCourants.Contains(unCourant) Then
          desCourants.Add(unCourant)
          ReDim Preserve qCourant(qCourant.Length)
          ReDim Preserve qCourantPond�r�(qCourantPond�r�.Length)
          With unCourant
            qCourant(desCourants.Count - 1) = unTrafic.QV�hicule(.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine), _
                                                            .Branche(TrajectoireV�hicules.OrigineDestEnum.Destination))
            qCourantPond�r�(desCourants.Count - 1) = qCourant(desCourants.Count - 1) * .CoefG�ne
          End With
        End If
      Next
    Next

    If VoiesAffect�esUnMouvement Then
      'Rechercher le courant le plus charg�, en divisant les trafics de chaque courant par le nb de voies utilisant ce courant
      ReDim nbVoies(desCourants.Count - 1)
      For Each uneVoie In Voies
        nbVoies(desCourants.IndexOf(uneVoie.mCourants(0))) += 1
      Next
      For i = 0 To desCourants.Count - 1
        ' v11
        q = Math.Max(q, qCourant(i) / nbVoies(i))
        qPond�r� = Math.Max(qPond�r�, qCourantPond�r�(i) / nbVoies(i))
      Next

    Else
      'Faire la sommation des trafics pond�r�s de chaque courant, et diviser le tout par le nombre de voies de la ligne de feux
      For i = 0 To desCourants.Count - 1
        q += qCourant(i)
        qPond�r� += qCourantPond�r�(i)
      Next
      q /= Voies.Count
      qPond�r� /= Voies.Count
    End If

    ' DemandeUVP = qPond�r�
    Return CType(qPond�r�, Integer)

  End Function

  Public Function TraficPond�r�D�grad�(ByVal unTrafic As Trafic) As Integer
    Dim qTAG As Integer
    Dim qTAD As Integer
    Dim qTD As Integer
    Dim QE As Integer = unTrafic.QE(Trafic.TraficEnum.UVP, mVariante.mBranches.IndexOf(mBranche))
    Dim Branche2 As Branche

    '	trafic du courant command� par la ligne de feux
    With mBranche
      '	trafics directionnels de la branche 
      If TAG Then
        Branche2 = mVariante.BranchePr�c�dente(mBranche)
        Do While Branche2.SensUnique(Voie.TypeVoieEnum.VoieSortante)
          Branche2 = mVariante.BranchePr�c�dente(Branche2)
          'Le test qui suit est superflu(il n'y aurait aucun trafic sortant !!)
          If Branche2 Is mBranche Then Exit Do
        Loop
        qTAG = unTrafic.QV�hicule(mBranche, Branche2)
      End If

      If TAD Then
        Branche2 = mVariante.BrancheSuivante(mBranche)
        Do While Branche2.SensUnique(Voie.TypeVoieEnum.VoieSortante)
          Branche2 = mVariante.BrancheSuivante(Branche2)
          'Le test qui suit est superflu(il n'y aurait aucun trafic sortant !!)
          If Branche2 Is mBranche Then Exit Do
        Loop
        qTAD = unTrafic.QV�hicule(mBranche, Branche2)
      End If
    End With

    'Dans la variante d�grad�e(mode tableur), si +  4 branches, tous les trafics sont tout droit sauf le 1er � gauche et le 1er � droite)
    qTD = QE - qTAD - qTAG

    'Dans la variante d�grad�e , on ne peut pas faire mieux (2� � p26 du guide carrefour � feux)
    Return (qTAD * CoefG�neTAD + qTD + qTAG * CoefG�neTAG) / nbVoies

  End Function

  ' *****************************************************************************
  ' Retourne le bord de la voie limitant la port�e de la ligne de feux
  ' Index =0 s'il s'agit du bord  le plus � droite
  ' Index = 1 s'il s'agit du bord le plus � gauche 
  ' *****************************************************************************

  Public ReadOnly Property BordVoie(ByVal Index As Branche.Lat�ralit�) As Ligne
    Get
      If Index = Branche.Lat�ralit�.Droite Then
        Return VoieDroite.Bordure(Branche.Lat�ralit�.Droite)
      Else
        Return VoieGauche.Bordure(Branche.Lat�ralit�.Gauche)
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
      mGraphique.RendreS�lectable(cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.G�om�trie)
      mGraphique.Invisible = (cndContexte = [Global].OngletEnum.G�om�trie)
      With mSignalFeu(0).mGraphique
        CType(.Item(0), Boite).RendreS�lectable(cndContexte >= [Global].OngletEnum.LignesDeFeux And SignalDessinable)
        .Invisible = (cndContexte = [Global].OngletEnum.G�om�trie Or Not SignalDessinable)
      End With

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuV�hicules.Verrouiller")
    End Try

  End Sub

  Public Function V�rifierVoieCoup�e(ByVal maVoie As Voie) As Boolean
    Dim uneVoie As Voie

    mVoies.Contains(maVoie)
    For Each uneVoie In mVoies
      If uneVoie Is maVoie Then Return True
    Next

  End Function

  Public Sub Cr�erFiliation(ByVal L1 As LigneFeuV�hicules, ByVal L2 As LigneFeuPi�tons)

    Try
      If Not dctFiliation.ContainsKey(L2) Then
        dctFiliation.Add(L2, L1)
      End If
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuV�hicules.Cr�erFiliation")

    End Try

  End Sub

  Public Sub R�initialiserFiliations()
    dctFiliation.Clear()
  End Sub

  Public Function LigneFeuxLi�e(ByVal uneLignePi�tons As LigneFeuPi�tons, Optional ByVal desLignesFeux As LigneFeuxCollection = Nothing) As LigneFeuV�hicules
    Dim uneLigneV�hicules As LigneFeuV�hicules
    If dctFiliation.ContainsKey(uneLignePi�tons) Then
      uneLigneV�hicules = dctFiliation(uneLignePi�tons)

      If desLignesFeux.EstIncompatible(Me, uneLigneV�hicules) Then
        Return uneLigneV�hicules
      End If
    End If
  End Function

  Public Sub PositionnerSignal()
    mSignalFeu(0).Position = PositionSignal(0)
  End Sub

  '***********************************************************************************
  ' Retourne la position par d�faut du signal li� � la ligne de feux
  ' Index=0 : inutilis� car il n'y a qu'1 signal pour une ligne v�hicules
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

  Public Overrides Sub D�terminerLargeurD�gagement()
    Dim uneTrajectoire As Trajectoire
    Dim LgMax As Single, Lg As Single

    For Each uneTrajectoire In mTrajectoires
      With CType(uneTrajectoire, TrajectoireV�hicules)
        Lg = .mGraphique.Longueur
        Lg -= .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Origine).Longueur
        Lg -= .AxeVoie(TrajectoireV�hicules.OrigineDestEnum.Destination).Longueur
        Lg /= Echelle
        Lg += mD�calage
      End With
      LgMax = Math.Max(LgMax, Lg)
      If LgMax = Lg Then mTrajectoirePrincipale = uneTrajectoire
    Next

    LgD�gagement = LgMax

  End Sub

  Private Sub DessinerPhaseA(ByVal unePhase As Phase)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim unePlumeFl�che As Pen = unePlume.Clone
    Dim uneTrajectoire As Trajectoire = mTrajectoires(0)

    Dim mAxeD�part As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle, unAngleFl�che As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = mDessin.MilieuF
    Dim unGraphique As PolyArc = unePhase.mGraphique

    BrancheOrigine = CType(uneTrajectoire, TrajectoireV�hicules).mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de sym�trie pour obtenir une parall�le � celle-ci passant au milieu de la ligne de feux
      With .LigneDeSym�trie
        P1 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 2mm (pour inscrire facilement le num�ro de la ligne de feux
    P1 = PointPosition(P1, 2, AngleBrancheRadians + Math.PI)
    'L'axe d�part : Axe de l'ensemble des voies entrantes command�es par la ligne de feux

    'L'�paisseur de la plume �tant de 0.3, ceci fait un espacement de 0.6
    Dim EspacementTiret() As Single = {2, 2}
    If Signal.JauneClignotant Then
      unePlume.DashStyle = Drawing2D.DashStyle.Dash
      unePlume.DashPattern = EspacementTiret
    End If
    mAxeD�part = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeD�part, False)

    'D�crire le cercle entourant le num�ro de ligne de feux
    Dim pCentre As PointF = PointPosition(P1, 5, AngleBrancheRadians)
    DessinerNum�roLigne(pCentre, unePhase.mGraphiqueNum�rosFeux)

    If unePhase.mLignesFeux.Contains(Me) Then
      Dim pO, pO2 As PointF
      Dim mFl�che, uneFl�che As Fleche
      ' Cr�er une fl�che 
      uneFl�che = New Fleche(0, HauteurFl�che:=2, SegmentCentral:=False, unePlume:=unePlumeFl�che)

      If TAD Then
        pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians - Math.PI / 2)
        'Cr�er un arc de cercle de 5mm de rayon tournant vers la droite
        unAngleFl�che = unAngle + 90
        unGraphique.Add(New Arc(pO, 5, unAngleFl�che Mod 360, 90, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

      If TAG Then
        'Rallonger la ligne axe origine de 5mm
        mAxeD�part.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
        'Cr�er un arc de cercle de 5mm de rayon tournant vers la gauche
        pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne � gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de d�part
        unAngleFl�che = unAngle + 180
        unGraphique.Add(New Arc(pO, 5, unAngleFl�che Mod 360, 90, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

      If TD Then
        'Rallonger la ligne axe origine de 20mm
        mAxeD�part.pBF = PointPosition(P2, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la fl�che � l'extr�mit� de l'axe de la ligne de feux
        pO2 = mAxeD�part.pBF
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

    Else
      'La ligne de feux appartient � une autre phase  : visualiser que les v�hicules sont arr�t�s au droit de cette ligne

      '1 ) on tronque l'axe central au droit de la ligne de feux
      mAxeD�part.pBF = pMilieuLigneFeux

      '2) On ajoute le dessin de la ligne elle-m�me (transverse � la pr�c�dente)
      Dim uneLigne As Ligne = mDessin.Clone
      uneLigne.Plume = unePlume
      mGraphique.Add(uneLigne)
    End If

  End Sub

  Private Sub DessinerPhaseB(ByVal unePhase As Phase)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim unePlumeFl�che As Pen = unePlume.Clone
    Dim uneTrajectoire As Trajectoire = mTrajectoires(0)

    Dim mAxeD�part As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle, unAngleFl�che As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = mDessin.MilieuF
    Dim unGraphique As PolyArc = unePhase.mGraphique

    BrancheOrigine = CType(uneTrajectoire, TrajectoireV�hicules).mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de sym�trie pour obtenir une parall�le � celle-ci passant au milieu de la ligne de feux
      With .LigneDeSym�trie
        P1 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 2mm (pour inscrire facilement le num�ro de la ligne de feux
    P1 = PointPosition(P1, 2, AngleBrancheRadians + Math.PI)
    'L'axe d�part : Axe de l'ensemble des voies entrantes command�es par la ligne de feux

    'L'�paisseur de la plume �tant de 0.3, ceci fait un espacement de 0.6
    Dim EspacementTiret() As Single = {2, 2}
    If Signal.JauneClignotant Then
      unePlume.DashStyle = Drawing2D.DashStyle.Dash
      unePlume.DashPattern = EspacementTiret
    End If
    mAxeD�part = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeD�part, False)

    'D�crire le cercle entourant le num�ro de ligne de feux
    Dim pCentre As PointF = PointPosition(P1, 5, AngleBrancheRadians)
    DessinerNum�roLigne(pCentre, unePhase.mGraphiqueNum�rosFeux)

    If unePhase.mLignesFeux.Contains(Me) Then
      Dim pO, pO2 As PointF
      Dim mFl�che, uneFl�che As Fleche
      ' Cr�er une fl�che 
      uneFl�che = New Fleche(0, HauteurFl�che:=2, SegmentCentral:=False, unePlume:=unePlumeFl�che)

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
            pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians - Math.PI / 2)
            'Cr�er un arc de cercle de 5mm de rayon tournant vers la droite
            unAngleFl�che = unAngle + 90

            ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
            pO2 = PointPosition(pO, 5, uneBranche.AngleEnRadians - Math.PI / 2)
            mFl�che = uneFl�che.RotTrans(pO2, uneBranche.AngleEnRadians + Math.PI)
          Case Else
            'TAG
            'Rallonger la ligne axe origine de 5mm
            mAxeD�part.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
            pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians + Math.PI / 2)
            'Cr�er un arc de cercle de 5mm de rayon tournant vers la gauche
            unAngleFl�che = uneBranche.Angle + 90

            ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
            pO2 = PointPosition(pO, 5, uneBranche.AngleEnRadians + Math.PI / 2)
            mFl�che = uneFl�che.RotTrans(pO2, uneBranche.AngleEnRadians + Math.PI)
        End Select
        unGraphique.Add(New Arc(pO, 5, unAngleFl�che Mod 360, Balayage, unePlume))
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)

      Next

      Return

      Balayage = 90
      If TAD Then
        pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians - Math.PI / 2)
        'Cr�er un arc de cercle de 5mm de rayon tournant vers la droite
        unAngleFl�che = unAngle + 90
        unGraphique.Add(New Arc(pO, 5, unAngleFl�che Mod 360, Balayage, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

      If TAG Then
        'Rallonger la ligne axe origine de 5mm
        mAxeD�part.pBF = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
        'Cr�er un arc de cercle de 5mm de rayon tournant vers la gauche
        pO = PointPosition(mAxeD�part.pBF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne � gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de d�part
        unAngleFl�che = unAngle + 180
        unGraphique.Add(New Arc(pO, 5, unAngleFl�che Mod 360, Balayage, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

      If TD Then
        'Rallonger la ligne axe origine de 20mm
        mAxeD�part.pBF = PointPosition(P2, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la fl�che � l'extr�mit� de l'axe de la ligne de feux
        pO2 = mAxeD�part.pBF
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians)
        'Ajouter la fl�che 
        unGraphique.Add(mFl�che)
      End If

    Else
      'La ligne de feux appartient � une autre phase  : visualiser que les v�hicules sont arr�t�s au droit de cette ligne

      '1 ) on tronque l'axe central au droit de la ligne de feux
      mAxeD�part.pBF = pMilieuLigneFeux

      '2) On ajoute le dessin de la ligne elle-m�me (transverse � la pr�c�dente)
      Dim uneLigne As Ligne = mDessin.Clone
      uneLigne.Plume = unePlume
      mGraphique.Add(uneLigne)
    End If

  End Sub

  Public Overrides Sub Cr�erGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)

    'Cr�er le dessin de la ligne de feux sans le rendre visible
    Cr�erGraphique(uneCollection)
    mDessin.Invisible = True
    DessinerPhaseA(unePhase)

  End Sub

  Public Overrides Sub D�terminerAutorisationD�calage(ByVal unePhase As Phase)

    mD�calageOuvertureAutoris� = False
    'mD�calageOuvertureAutoris� = True

    'If TAD Or TD Then
    '  'Interdire le d�calage � l'ouverture si un TAG arrive en face dans la m�me phase
    '  For Each uneLigneFeux In unePhase.mLignesFeux
    '    If uneLigneFeux.EstV�hicule AndAlso Not uneLigneFeux Is Me Then
    '      If CType(uneLigneFeux, LigneFeuV�hicules).TAG Then
    '        mD�calageOuvertureAutoris� = False
    '      End If
    '    End If
    '  Next
    'End If

  End Sub

  Public Overrides Sub Cr�erGraphiqueD�gagement(ByVal uneCollection As Graphiques)
    ' Dessiner la ligne de feux
    Cr�erGraphique(uneCollection)

    Dim unPolyArc As PolyArc = TrajectoirePrincipale.Cr�erGraphique(uneCollection)
    'Tronquer le d�but de la trajectoire au droit de la ligne de feux
    Dim uneLigne As Ligne = unPolyArc(0)
    uneLigne.pBF = intersect(uneLigne, mDessin)

    'Dernier segment de la trajectoire pour positionner l'�criture du rouge de d�gagement
    uneLigne = CType(unPolyArc(unPolyArc.Count - 1), Ligne)
    Dim unAngle As Single = AngleForm�(uneLigne)

    'Dessiner une fl�che � l'extr�mit� du segment final
    Dim uneFl�che As New Fleche(0, 2, SegmentCentral:=False, unePlume:=mDessin.Plume)
    uneFl�che = uneFl�che.RotTrans(uneLigne.pB, unAngle + sngPI)
    mGraphique.Add(uneFl�che)

    'Ecrire la distance de d�gagement de la travers�e v�hicule
    mGraphique.Add(TexteLgD�gagement(uneLigne.pB, unAngle))

  End Sub

  Public Overrides Function Dur�eJaune() As Short

    If Signal.JauneClignotant Then
      Dur�eJaune = JauneClignotant    ' R11J
    Else
      Dur�eJaune = mVariante.JauneV�hicules
    End If
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe LigneFeuPi�tons --------------------------
'=====================================================================================================
Public Class LigneFeuPi�tons : Inherits LigneFeux

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
        'Pr�caution pour relire les fichiers ant�rieurs � Proto6
        With .GetSignalRows(1)
          mSignalFeu(1).Position = New Point(.X, .Y)
        End With
      End If
    End With

  End Sub

  Public Overrides Function strLigneGrille(ByVal desBranches As BrancheCollection, ByVal S�parateur As Char) As String

    Dim s As String = MyBase.strLigneGrille(desBranches, S�parateur)
    s &= (S�parateur & S�parateur & S�parateur & S�parateur)
    Return s
  End Function

  '*****************************************************************************
  'La ligne de feux pi�tons n'a pas de repr�sentation graphique
  'Celle-ci es remplac�e par le dessin d'1 ou 2 signaux de feux
  '*****************************************************************************
  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim i As Short

    'If SignalDessinable Then
    For i = 0 To mSignalFeu.Length - 1
      If SignalARepr�senter(i) Then
        With mSignalFeu(i)
          If .Position.Equals(New Point(0, 0)) Then
            'Intialiser avec la position par d�faut
            .Position = PositionSignal(i)
          End If
          .Cr�erGraphique(uneCollection)
          If Not SignalDessinable Then .mGraphique.Invisible = True
        End With
      End If
    Next
    'End If

  End Function

  Public ReadOnly Property SignalARepr�senter(ByVal Index As Short) As Boolean
    Get
      If Index = 0 Then
        Return True
      Else
        Return mBranche.mPassages.Count = 1 Or mTravers�e.mDouble
      End If
    End Get
  End Property

  Public Overloads Overrides Sub Verrouiller()
    Dim i As Short

    Try
      For i = 0 To mSignalFeu.Length - 1
        If SignalARepr�senter(i) Then
          With mSignalFeu(i).mGraphique
            'CType(.Item(0), Boite).RendreS�lectable(cndContexte >= Global.OngletEnum.LignesDeFeux And SignalDessinable)
            .RendreS�lectable(cndContexte >= [Global].OngletEnum.LignesDeFeux And SignalDessinable, CType(.Item(0), Boite))
            .Invisible = (cndContexte = [Global].OngletEnum.G�om�trie Or Not SignalDessinable)
          End With
        End If
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "LigneFeuPi�tons.Verrouiller")
    End Try
    'End If

  End Sub

  '**********************************************************************
  ' Retourne la position par d�faut du signal li� � la ligne de feux pi�tons
  ' Index : 0 ou 1 (il peut y avoir 1 ou 2 signaux pour repr�senter la ligne)
  '**********************************************************************
  Public Overrides Function PositionSignal(ByVal Index As Short) As Point
    Dim AngleRot As Single
    Dim p1, p2 As Point
    Dim Distance As Single

    With mTravers�e
      p1 = PointDessin(Milieu(.Points(0), .Points(1)))
      If .mDouble Then
        p2 = PointDessin(Milieu(.Points(4), .Points(5)))
      Else
        p2 = PointDessin(Milieu(.Points(2), .Points(3)))
      End If
    End With

    If Index = 0 Then
      AngleRot = AngleForm�(p2, p1)
    Else
      AngleRot = AngleForm�(p1, p2)
    End If

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Distance = 32 '32 pixels
    Else
      Distance = 5  '5mm de l'extr�mit� de la fl�che repr�sentant la travers�e pi�tonne
    End If

    Return PointPosition(New Point(0, 0), Distance, AngleRot)

  End Function

  Public Sub EffacerSignaux(ByVal uneCollection As Graphiques)
    Dim i As Short

    For i = 0 To mSignalFeu.Length - 1
      If SignalARepr�senter(i) Then uneCollection.Remove(mSignalFeu(i).mGraphique)
    Next

  End Sub

  Public Overrides Sub D�terminerLargeurD�gagement()
    '    LgD�gagement = CType(mTrajectoires(0), Travers�ePi�tonne).LgMaximum
    'Modif Proto v12 suite � demande CERTU : Dossier de suivi Circulation Point 13 (D�cemebre 2006)
    LgD�gagement = CType(mTrajectoires(0), Travers�ePi�tonne).LgM�diane
  End Sub

  Public ReadOnly Property mTravers�e() As Travers�ePi�tonne
    Get
      Return CType(mTrajectoires(0), Travers�ePi�tonne)
    End Get
  End Property

  Public Overrides Sub Cr�erGraphiquePhase(ByVal unePhase As Phase, ByVal uneCollection As Graphiques)
    Dim pCentre As PointF

    If unePhase.mLignesFeux.Contains(Me) Then
      ' Dessiner la travers�e pi�tonne (Fl�che)
      mTravers�e.Cr�erGraphique(uneCollection)

      ' Ecrire le num�ro de ligne de feux dans un cercle au m�me endroit que le signal de feu sur le sch�ma du carrefour
      'Dim p1 As Point = mSignalFeu(0).PtR�f�rence
      Dim uneFl�che As Fleche = mTravers�e.Fl�che
      Dim p1 As PointF = uneFl�che.ptR�f�rence(0)
      p1 = Translation(p1, CvPointF(PositionSignal(0)))
      Dim p2 As PointF = uneFl�che.ptR�f�rence(1)
      p2 = Translation(p2, CvPointF(PositionSignal(1)))
      Dim Distance1, Distance2 As Single

      'If SignalARepr�senter(1) Then

      '  Distance1 = Formules.Distance(p1, mBranche.BordChauss�e(Branche.Lat�ralit�.Gauche))
      '  Distance2 = Formules.Distance(p2, mBranche.BordChauss�e(Branche.Lat�ralit�.Gauche))
      '  If Distance1 < Distance2 Then
      '    pCentre = p1
      '  Else
      '    pCentre = p2
      '  End If

      'Else
      '  pCentre = p1
      'End If

      'DessinerNum�roLigne(pCentre, unePhase.mGraphiqueNum�rosFeux)

      Distance1 = Formules.Distance(p1, PointDessinF(mVariante.Centre))
      Distance2 = Formules.Distance(p2, PointDessinF(mVariante.Centre))
      If Distance1 > Distance2 Then
        pCentre = p1
      Else
        pCentre = p2
      End If
      DessinerNum�roLigne(pCentre, unePhase.mGraphiqueNum�rosFeux)
    End If

  End Sub

  Public Overrides Sub D�terminerAutorisationD�calage(ByVal unePhase As Phase)
    mD�calageOuvertureAutoris� = True
  End Sub

  Public Overrides Sub Cr�erGraphiqueD�gagement(ByVal uneCollection As Graphiques)
    Dim unAngle As Single
    Dim p As Point

    With mTravers�e
      .Cr�erGraphique(uneCollection)
      With .Fl�che
        unAngle = .Angle
        p = PointPosition(mSignalFeu(0).PtR�f�rence, .Longueur / 2, unAngle)
      End With
    End With

    mTravers�e.mGraphique.Add(TexteLgD�gagement(p, unAngle))

  End Sub

  Public Overrides Function Dur�eJaune() As Short
    ' Pas de jaune pour les pi�tons
    Return 0
  End Function

End Class
