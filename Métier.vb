'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Métier.vb										  													'
'						Classes																														'
'							Métier														  														'
'							Courant								            															'
'							CourantCollection       																		    '
'             Antagonisme 			  																						'
'							AntagonismeCollection       																		'
'******************************************************************************
Imports System.Math

Public MustInherit Class Métier

  Public mGraphique As PolyArc

  Public Sub SupprimerGraphique(ByVal uneCollection As Graphiques)
    uneCollection.Remove(mGraphique)
  End Sub
  Public MustOverride Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc

End Class

'=====================================================================================================
'--------------------------- Classe Antagonisme  --------------------------
'=====================================================================================================
Public Class Antagonisme : Inherits Métier
  Private mTrajVéhicule1 As TrajectoireVéhicules
  'La 2ème trajectoire du conflit est véhicules ou piétons
  Private mTraject2 As Trajectoire
  Private mPoint As PointF
  Private mTypeConflit As Trajectoire.TypeConflitEnum
  Private dctFiliation As New Hashtable
  Private mMêmesCourants As Antagonisme
  Private mAntagoOrigine As Antagonisme

  Public Enum AntagonismeEnum
    TDTD    ' Systématique
    TDPiétons 'Systématique
    TDTAG   ' Systématique si phase spéciale TAG
    TDTAD
    TAGTAD
    TAGTAG
    TADTAD
    TAGPiétons ' Imposé si le TDTAG associé est non admis
    TADPiétons
    TAGPiétonsEtSensUnique
    TADPiétonsEtSensUnique
  End Enum
  Private mTypeCourantsAntagonistes As AntagonismeEnum

  Public Enum PositionEnum
    Premier
    Dernier
  End Enum

  Public ReadOnly Property EstPiéton() As Boolean
    Get
      Return TypeOf mTraject2 Is TraverséePiétonne
    End Get
  End Property

  Public ReadOnly Property EstVéhicule() As Boolean
    Get
      Return TypeOf mTraject2 Is TrajectoireVéhicules
    End Get
  End Property

  Public Property MêmesCourants() As Antagonisme
    Get
      Return mMêmesCourants
    End Get
    Set(ByVal Value As Antagonisme)
      mMêmesCourants = Value
    End Set
  End Property

  Private ReadOnly Property mVariante() As Variante
    Get
      Return mTrajVéhicule1.mVariante
    End Get
  End Property

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me
    Select Case mTypeConflit
      Case Trajectoire.TypeConflitEnum.Systématique
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitSystématique).Clone))
      Case Trajectoire.TypeConflitEnum.Admissible
        '        mGraphique.Add(New Cercle(PointDessin(mPoint), 3, unePlume:=cndPlumes.Plume(Plumes.PlumeEnum.ConflitAdmissible).Clone))
        'Curieusement le cast de Object vers Pen suite à la méthode clone est refusé par VS2005, alors qu'il passe pour un SolidBrush 
        mGraphique.Add(New Cercle(PointDessin(mPoint), 3, unePlume:=CType(cndPlumes.Plume(Plumes.PlumeEnum.ConflitAdmissible).Clone, Pen)))
      Case Trajectoire.TypeConflitEnum.Admis
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitAdmis).Clone))
      Case Trajectoire.TypeConflitEnum.NonAdmis
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitNonAdmis).Clone))
    End Select

    'Pour DIAGFEUX : les objets graphiques originaux (ceux construits par les trajectoires) ne sont pas dessinés : seuls les ont ceux correspondant à un scénario
    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  Public Sub New(ByVal unAntagonisme As Antagonisme, Optional ByVal DuplicationIncomplète As Boolean = True)

    With unAntagonisme
      Me.mTrajVéhicule1 = .mTrajectoire(PositionEnum.Premier)
      Me.mTraject2 = .mTrajectoire(PositionEnum.Dernier)
      mPoint = .Position
      mTypeConflit = .TypeConflit
      If mTypeConflit <> Trajectoire.TypeConflitEnum.Systématique And DuplicationIncomplète Then
        mTypeConflit = Trajectoire.TypeConflitEnum.Admissible
      End If
      mTypeCourantsAntagonistes = .TypeCourantsAntagonistes
    End With

    mAntagoOrigine = unAntagonisme

  End Sub

  Public ReadOnly Property AntagoOrigine() As Antagonisme
    Get
      If IsNothing(mAntagoOrigine) Then
        Return Me
      Else
        Return mAntagoOrigine
      End If
    End Get
  End Property

  Public Sub New(ByVal T1 As TrajectoireVéhicules, ByVal T2 As Trajectoire, ByVal p As PointF, ByVal TypeConflit As Trajectoire.TypeConflitEnum)

    Me.mTrajVéhicule1 = T1
    Me.mTraject2 = T2
    mPoint = p
    mTypeConflit = TypeConflit

    Try
      Dim msg As String = Nothing
      If EstPiéton Then
        Select Case mTrajVéhicule1.NatureCourant
          Case TrajectoireVéhicules.NatureCourantEnum.TD
            mTypeCourantsAntagonistes = AntagonismeEnum.TDPiétons
          Case TrajectoireVéhicules.NatureCourantEnum.TAG
            If Tp.mBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              mTypeCourantsAntagonistes = AntagonismeEnum.TAGPiétonsEtSensUnique
            Else
              mTypeCourantsAntagonistes = AntagonismeEnum.TAGPiétons
            End If
          Case TrajectoireVéhicules.NatureCourantEnum.TAD
            If Tp.mBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              mTypeCourantsAntagonistes = AntagonismeEnum.TADPiétonsEtSensUnique
            Else
              mTypeCourantsAntagonistes = AntagonismeEnum.TADPiétons
            End If
        End Select

      Else
        Dim NatureCourant2 As TrajectoireVéhicules.NatureCourantEnum = CType(mTraject2, TrajectoireVéhicules).NatureCourant

        Select Case mTrajVéhicule1.NatureCourant
          Case TrajectoireVéhicules.NatureCourantEnum.TD
            Select Case NatureCourant2
              Case TrajectoireVéhicules.NatureCourantEnum.TD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTD
              Case TrajectoireVéhicules.NatureCourantEnum.TAG
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAG
              Case TrajectoireVéhicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAD
            End Select

          Case TrajectoireVéhicules.NatureCourantEnum.TAG
            Select Case NatureCourant2
              Case TrajectoireVéhicules.NatureCourantEnum.TAG
                mTypeCourantsAntagonistes = AntagonismeEnum.TAGTAG
              Case TrajectoireVéhicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TAGTAD
              Case Else
                msg = "Anomalie Antagonisme TAG"
            End Select

          Case TrajectoireVéhicules.NatureCourantEnum.TAD
            Select Case NatureCourant2
              Case TrajectoireVéhicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAD
              Case Else
                msg = "Anomalie Antagonisme TAD"
            End Select

        End Select

      End If

      If Not IsNothing(msg) Then
        Throw New DiagFeux.MétierException(msg)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Création de l'antagonisme")
    End Try

  End Sub

  Private ReadOnly Property Tv2() As TrajectoireVéhicules
    Get
      Return CType(mTraject2, TrajectoireVéhicules)
    End Get
  End Property

  Public ReadOnly Property Tp() As TraverséePiétonne
    Get
      Return CType(mTraject2, TraverséePiétonne)
    End Get
  End Property

  Public ReadOnly Property Position() As PointF
    Get
      Return mPoint
    End Get
  End Property

  Public Property TypeConflit() As Trajectoire.TypeConflitEnum
    Get
      Return mTypeConflit
    End Get
    Set(ByVal Value As Trajectoire.TypeConflitEnum)
      mTypeConflit = Value
    End Set
  End Property

  Public ReadOnly Property TypeCourantsAntagonistes() As AntagonismeEnum
    Get
      Return mTypeCourantsAntagonistes
    End Get
  End Property

  Public ReadOnly Property Autorisé() As Boolean
    Get
      Select Case mTypeConflit
        Case Trajectoire.TypeConflitEnum.Admis, Trajectoire.TypeConflitEnum.Aucun
          Return True
      End Select

    End Get
  End Property

  Public ReadOnly Property Interdit() As Boolean
    Get
      Select Case mTypeConflit
        Case Trajectoire.TypeConflitEnum.Systématique, Trajectoire.TypeConflitEnum.NonAdmis, Trajectoire.TypeConflitEnum.Admissible
          Return True
      End Select
    End Get
  End Property

  Public ReadOnly Property Admissible() As Boolean
    Get
      Select Case mTypeConflit
        Case Trajectoire.TypeConflitEnum.Admissible, Trajectoire.TypeConflitEnum.Admis
          Return True
      End Select
    End Get
  End Property

  Public ReadOnly Property Résolu() As Boolean
    Get
      Select Case mTypeConflit
        Case Trajectoire.TypeConflitEnum.Admis, Trajectoire.TypeConflitEnum.NonAdmis
          Return True
      End Select
    End Get
  End Property

  '********************************************************************************************************************
  ' Obtient ou définit si un antagonisme est dépendant de celui-ci
  ' unAntagonisme : antagonisme pour lequel la dépendance est à définir ou à rechercher
  '********************************************************************************************************************

  Public Property EstPère(ByVal unAntagonisme As Antagonisme) As Boolean
    Get
      If dctFiliation.ContainsKey(unAntagonisme) Then
        EstPère = True
      End If
    End Get

    Set(ByVal Value As Boolean)
      If Value Then
        dctFiliation.Add(unAntagonisme, unAntagonisme)
      Else
        dctFiliation.Remove(unAntagonisme)
      End If

    End Set
  End Property

  Public ReadOnly Property FilsNonAdmis(ByVal colAntago As AntagonismeCollection) As Boolean
    Get
      Dim unAntagonisme As Antagonisme
      For Each unAntagonisme In colAntago
        If unAntagonisme.AntagoOrigine.EstPère(AntagoOrigine) AndAlso unAntagonisme.Interdit Then
          Return True
        End If
      Next
    End Get
  End Property

  Public Sub Verrouiller()

    If cndContexte = [Global].OngletEnum.Conflits Then
      If mVariante.Verrou > [Global].Verrouillage.LignesFeux Then
        'Voir tous les conflits dès que la matrice des conflits est verrouillée
        Masqué = False
      Else
        If mTypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
          Masqué = True
        ElseIf IsNothing(mVariante.BrancheEnCoursAntagonisme) Then
          'On souhaite afficher tous les antagonismes et non seulement ceux issus d'une branche particulière
          Masqué = False
        Else
          Masqué = Not BrancheCourant1 Is mVariante.BrancheEnCoursAntagonisme
        End If
      End If

    Else
      Masqué = True
    End If

    mGraphique.RendreSélectable(Not Masqué, Editable:=Not Masqué)

  End Sub

  Public ReadOnly Property mTrajectoire(ByVal Index As PositionEnum) As Trajectoire
    Get
      If Index = PositionEnum.Premier Then
        Return mTrajVéhicule1
      Else
        Return mTraject2
      End If
    End Get
  End Property

  Public ReadOnly Property LigneFeu(ByVal Index As PositionEnum) As LigneFeux
    Get
      Return mTrajectoire(Index).LigneFeu
    End Get
  End Property

  '*******************************************************************************************
  ' Recherche si un antagonisme est dépendant du couple de ligne de feux (LigneFeu1,LigneFeu2)
  '*******************************************************************************************
  Public Function AntagonismeLié(ByVal LigneFeu1 As LigneFeux, ByVal LigneFeu2 As LigneFeux) As Boolean
    Dim LV1 As LigneFeuVéhicules = LigneFeu(PositionEnum.Premier)
    Dim L2 As LigneFeux = LigneFeu(PositionEnum.Dernier)

    If (LigneFeu1 Is LV1 And LigneFeu2 Is L2) Or (LigneFeu1 Is L2 And LigneFeu2 Is LV1) Then
      Return True
    End If

  End Function

  Public ReadOnly Property Courant(ByVal Index As PositionEnum) As Courant
    Get
      If Index = PositionEnum.Premier Then
        With mTrajVéhicule1
          Return mVariante.mCourants(.mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine), _
                                       .mBranche(TrajectoireVéhicules.OrigineDestEnum.Destination))
        End With

      ElseIf EstVéhicule Then
        With CType(mTraject2, TrajectoireVéhicules)
          Return mVariante.mCourants(.mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine), _
                                       .mBranche(TrajectoireVéhicules.OrigineDestEnum.Destination))
        End With
      End If

    End Get
  End Property

  Public ReadOnly Property BrancheCourant1() As Branche
    Get
      Return Courant(Antagonisme.PositionEnum.Premier).Branche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    End Get
  End Property

  Public Function Libellé(ByVal Index As PositionEnum, ByVal Branches As BrancheCollection) As String
    Dim unCourant As Courant = Courant(Index)
    Dim uneTrajectoire As Trajectoire
    Dim ComplémentLibellé As String

    If IsNothing(unCourant) Then
      '2ème élément de l'antagonisme est une traversée piétonne
      uneTrajectoire = mTraject2
      ComplémentLibellé = " (" & uneTrajectoire.LigneFeu.ID & ")"
      Libellé = "Piétons : " & Branches.ID(Tp.mBranche)
      Libellé &= ComplémentLibellé

    Else

      With unCourant
        Libellé = Branches.ID(.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine))
        If Index = PositionEnum.Premier Then
          uneTrajectoire = Me.mTrajVéhicule1
        Else
          uneTrajectoire = Me.mTraject2
        End If
        ComplémentLibellé = " (" & uneTrajectoire.LigneFeu.ID & ")"
        Libellé &= ComplémentLibellé
        Libellé &= " --> " & Branches.ID(.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination))
      End With

    End If

  End Function

  Public Property Masqué() As Boolean
    Get
      Return mGraphique.Invisible
    End Get
    Set(ByVal Value As Boolean)
      mGraphique.Invisible = Value
    End Set
  End Property

  Public Sub Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow, ByVal desTrajectoires As TrajectoireCollection)
    Dim uneRowAntagonisme As DataSetDiagfeux.AntagonismeRow
    'Ajouter une enregistrement dans la table des antagonismes
    uneRowAntagonisme = ds.Antagonisme.AddAntagonismeRow(desTrajectoires.IndexOf(mTrajVéhicule1), desTrajectoires.IndexOf(mTraject2), TypeConflit, uneRowVariante)
    'Ajouter le point de conflit
    ds.pAntago.AddpAntagoRow(mPoint.X, mPoint.Y, uneRowAntagonisme)

  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe AntagonismeCollection--------------------------
'=====================================================================================================
Public Class AntagonismeCollection : Inherits CollectionBase
  Public NonTousSystématiques As Boolean

  Private mLignesFeux As LigneFeuxCollection
  Private mBrancheEnCoursAntagonisme As Branche

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal desLignesFeux As LigneFeuxCollection)
    mLignesFeux = desLignesFeux
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unAntagonisme As Antagonisme) As Short
    Return Me.List.Add(unAntagonisme)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Antagonisme)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unAntagonisme As Antagonisme)
    If Me.List.Contains(unAntagonisme) Then
      Me.List.Remove(unAntagonisme)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unAntagonisme As Antagonisme)
    Me.List.Insert(Index, unAntagonisme)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Antagonisme
    Get
      Return CType(Me.List.Item(Index), Antagonisme)
    End Get
  End Property

  Public Function IndexOf(ByVal unAntagonisme As Antagonisme) As Short
    Return Me.List.IndexOf(unAntagonisme)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal unAntagonisme As Antagonisme) As Boolean
    Return Me.List.Contains(unAntagonisme)
  End Function

  Public Function CréerGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim unAntagonisme As Antagonisme
    Dim mPoints As Point()
    Dim mPRef(-1) As Point
    Dim dctpRef(-1) As Hashtable
    ReDim mPoints(Count - 1)

    For Each unAntagonisme In Me
      mPoints(IndexOf(unAntagonisme)) = CType(unAntagonisme.CréerGraphique(uneCollection).Figures(0), Cercle).pO
    Next

    'For i = 0 To mPoints.Length - 2
    '  For j = 1 To mPoints.Length - 1
    '    If j > i Then
    '      If Distance(mPoints(i), mPoints(j)) < 2 Then
    '        pMil = Milieu(mPoints(i), mPoints(j))
    '        For k = 0 To mPRef.Length
    '          If Distance(pMil, mPRef(i)) < 4 Then

    '          End If

    '        Next
    '      End If

    '    End If
    '  Next
    'Next

  End Function

  Public Property BrancheEnCoursAntagonisme() As Branche
    Get
      Return mBrancheEnCoursAntagonisme
    End Get
    Set(ByVal Value As Branche)
      mBrancheEnCoursAntagonisme = Value
    End Set
  End Property

  Public Sub Verrouiller()
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      unAntagonisme.Verrouiller()
    Next
  End Sub

  '*************************************************************************************************
  ' Rechercher les possibles conflits TAG/Piétons liés à un autre antagonisme :
  ' Si l'Antagonisme père devenait non admis, le TAG/Piétons ne pourrait plus être admis non plus
  '*************************************************************************************************
  Public Sub AntagoFiliation()
    Dim unAntagonisme As Antagonisme
    Dim uneTrajectoire As TrajectoireVéhicules

    Try
      ' Un antagonisme TAG/Piétons est lié à tout autre antagonisme dont la trajectoire a même origine que le TAG
      For Each unAntagonisme In Me
        With unAntagonisme
          If .TypeConflit <> Trajectoire.TypeConflitEnum.Systématique Then
            ' Conflit non systématique pouvant être lié à un autre conflit non systématique
            uneTrajectoire = .mTrajectoire(Antagonisme.PositionEnum.Premier)
            If uneTrajectoire.NatureCourant = TrajectoireVéhicules.NatureCourantEnum.TAG Then
              RechercherTAGPiétons(unAntagonisme)
            ElseIf .EstVéhicule Then
              uneTrajectoire = .mTrajectoire(Antagonisme.PositionEnum.Dernier)
              If uneTrajectoire.NatureCourant = TrajectoireVéhicules.NatureCourantEnum.TAG Then
                RechercherTAGPiétons(unAntagonisme, InverserSens:=True)
              End If
            End If
          End If
        End With
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Antagonismes.AntagoFiliation")
    End Try

  End Sub

  '*************************************************************************************************
  ' Rechercher les possibles conflits TAG/Piétons liés à AntagonismePère :
  ' Si l'Antagonisme père devenait non admis, le TAG/Piétons ne pourrait plus être admis non plus
  '*************************************************************************************************
  Private Sub RechercherTAGPiétons(ByVal AntagonismePère As Antagonisme, Optional ByVal InverserSens As Boolean = False)

    Dim unAntagonisme As Antagonisme
    Dim uneLigneVéhicules As LigneFeuVéhicules
    Dim uneLignePiétons As LigneFeuPiétons
    Dim Trajectoire1, Trajectoire2 As Trajectoire

    Try
      If InverserSens Then
        Trajectoire1 = AntagonismePère.mTrajectoire(Antagonisme.PositionEnum.Dernier)
        Trajectoire2 = AntagonismePère.mTrajectoire(Antagonisme.PositionEnum.Premier)
      Else
        Trajectoire1 = AntagonismePère.mTrajectoire(Antagonisme.PositionEnum.Premier)
        Trajectoire2 = AntagonismePère.mTrajectoire(Antagonisme.PositionEnum.Dernier)
      End If

      If Trajectoire2.EstVéhicule Then
        ' Trajectoire2 est de courant TD  : il n'y a pas de conflit TAG/TAD, et si conflit TAG/TAG il est systématique
        For Each unAntagonisme In Me
          With unAntagonisme
            If Not AntagonismePère Is unAntagonisme AndAlso .TypeConflit <> Trajectoire.TypeConflitEnum.Systématique Then
              If .mTrajectoire(Antagonisme.PositionEnum.Premier) Is Trajectoire1 And .EstPiéton Then
                AntagonismePère.EstPère(unAntagonisme) = True
                uneLigneVéhicules = .LigneFeu(Antagonisme.PositionEnum.Premier)
                uneLignePiétons = .LigneFeu(Antagonisme.PositionEnum.Dernier)
                CType(Trajectoire1.LigneFeu, LigneFeuVéhicules).CréerFiliation(uneLigneVéhicules, uneLignePiétons)
              End If
            End If
          End With
        Next
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Antagonismes.RechercherTAGPiétons")
    End Try
  End Sub

  '************************************************************************************************
  ' Rechercher s'il reste encore un conflit non admis concerné pas les 2 lignes de feux
  '************************************************************************************************
  Public Function LignesFeuxIncompatibles(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Antagonisme  'Boolean '
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      With unAntagonisme
        If (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L1 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L2) Or _
        (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L2 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L1) Then
          If .Interdit Then
            'Il reste encore un conflit non admis pour ces 2 lignes de feux : elles restent incompatibles
            Return unAntagonisme 'True 
            Exit For
          End If
        End If
      End With
    Next

  End Function

  '************************************************************************************************
  ' Rechercher s'il reste un conflit admis ou non admis concerné pas les 2 lignes de feux
  '************************************************************************************************
  Public Function AntagonismeTypeconflitIncorrect(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Antagonisme
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      With unAntagonisme
        If (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L1 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L2) Or _
        (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L2 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L1) Then
          If .Interdit Xor mLignesFeux.EstIncompatible(L1, L2) Then
            'Ce conflit n'a pas le même statut que les autres pour ces 2 lignes de feux 
            Return unAntagonisme
            Exit For
          End If
        End If
      End With
    Next

  End Function

  Public Function ExisteConflit(ByVal l1 As LigneFeux, ByVal l2 As LigneFeux, Optional ByRef ListeNonAdmis As String = "") As Trajectoire.TypeConflitEnum
    Dim unAntagonisme As Antagonisme
    Dim TypeConflit As Trajectoire.TypeConflitEnum = Trajectoire.TypeConflitEnum.Aucun
    Dim desBranches As BrancheCollection = l1.mVariante.mBranches

    For Each unAntagonisme In Me
      With unAntagonisme
        If (.LigneFeu(Antagonisme.PositionEnum.Premier) Is l1 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is l2) Or _
        (.LigneFeu(Antagonisme.PositionEnum.Premier) Is l2 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is l1) Then
          If Not .Autorisé Then
            TypeConflit = Math.Max(TypeConflit, .TypeConflit)
            If .TypeConflit = Trajectoire.TypeConflitEnum.NonAdmis Then
              ListeNonAdmis &= .Libellé(Antagonisme.PositionEnum.Premier, desBranches) & " et " & .Libellé(Antagonisme.PositionEnum.Dernier, desBranches) & vbCrLf
            End If
          End If
        End If
      End With
    Next

    Return TypeConflit

  End Function

  Public Function NbConflitsARésoudre() As Short
    Dim nb As Short
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
        nb += 1
      End If
    Next

    Return nb
  End Function

  Public Function ConflitsPartiellementRésolus() As Boolean

    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      If unAntagonisme.Résolu Then
        Return True
      End If
    Next

  End Function

  Public Function Fils(ByVal unAntagonisme As Antagonisme) As AntagonismeCollection
    Dim colAntago As New AntagonismeCollection
    Dim unAntago As Antagonisme

    For Each unAntago In Me
      If unAntago.AntagoOrigine.EstPère(unAntagonisme.AntagoOrigine) Then
        colAntago.Add(unAntagonisme)
      End If
    Next
    Return colAntago

  End Function

  Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
    Dim unAntagonisme As Antagonisme
    Dim monAntago As Antagonisme = CType(value, Antagonisme)
    Dim AvecPiétons As Boolean

    'Rechercher si un antagonisme déjà créé ne concerne pas les mêmes courants 

    With monAntago
      ' Par défaut c'est l'antagonisme lui-même 
      .MêmesCourants = monAntago

      For Each unAntagonisme In Me
        AvecPiétons = .EstPiéton

        If (AvecPiétons Xor unAntagonisme.EstVéhicule) Then
          'Ne comparer les 2 antagonismes que s'ils comportent tous 2 soit 1 traversée piétonne soit aucune

          'Comparer les 2 premiers courants
          If unAntagonisme.Courant(Antagonisme.PositionEnum.Premier) Is .Courant(Antagonisme.PositionEnum.Premier) Then
            'Les antagonismes sont identiques si les 2 derniers courants sont identiques 
            ' soit les mêmes traversées piétonnes, soit les mêmes Courants de circulation
            If AvecPiétons Then
              ' les 2ème courants sont des traversées piétonnes
              If .Tp Is unAntagonisme.Tp Then
                .MêmesCourants = unAntagonisme
                Exit For
              End If
            Else
              ' les 2ème courants sont des trajectoires véhicules
              If .Courant(Antagonisme.PositionEnum.Dernier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Dernier) Then
                .MêmesCourants = unAntagonisme
                Exit For
              End If
            End If

          ElseIf Not AvecPiétons Then
            'Comparer le 1er courant avec le 2ème courant de l'autre antagonisme
            If .Courant(Antagonisme.PositionEnum.Premier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Dernier) AndAlso _
               .Courant(Antagonisme.PositionEnum.Dernier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Premier) Then
              'Les antagonismes sont identiques si 2ème courant est le même que le 1er courant de l'autre antagonisme
              .MêmesCourants = unAntagonisme
              Exit For
            End If
          End If
        End If  ' AvecPiétons Xor unAntagonisme.EstVéhicule

      Next  ' unAntagonisme

    End With

  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe Courant--------------------------
'=====================================================================================================
Public Class Courant

  ' Branches Origine et Destination du courant
  Private mBranche(TrajectoireVéhicules.OrigineDestEnum.Destination) As Branche

  'Le nature de courant peut être : TAD(tourne à droite),TAG(tourne à gauche) ou TD (tout droit)
  '##ModelId=4033135E0186
  Private mNatureCourant As TrajectoireVéhicules.NatureCourantEnum

  'Le coefficient de gêne s'applique à la difficulté d'écoulement du trafic pour le courant considéré
  'Par défaut :  1 pour TD, 1.3 pour TAD et 1.7 pour TAG
  '##ModelId=403313B30232
  Private mCoefGêne As Single

  'Ligne de feux véhicules qui commmande le courant
  Private mLigneFeux As LigneFeuVéhicules

  Public Sub New(ByVal BrancheOrigine As Branche, ByVal BrancheDestination As Branche)
    mBranche(TrajectoireVéhicules.OrigineDestEnum.Origine) = BrancheOrigine
    mBranche(TrajectoireVéhicules.OrigineDestEnum.Destination) = BrancheDestination
    mNatureCourant = TrajectoireVéhicules.NatureCourantEnum.TD
    mCoefGêne = 1.0
  End Sub

  Public Property LigneFeuxCommande() As LigneFeuVéhicules
    Get
      Return mLigneFeux
    End Get
    Set(ByVal Value As LigneFeuVéhicules)
      mLigneFeux = Value
    End Set
  End Property

  Public Property Branche(ByVal Index As TrajectoireVéhicules.OrigineDestEnum) As Branche
    Get
      Return mBranche(Index)
    End Get
    Set(ByVal Value As Branche)
      mBranche(Index) = Value
    End Set
  End Property

  Public Property NatureCourant() As TrajectoireVéhicules.NatureCourantEnum
    Get
      Return mNatureCourant
    End Get
    Set(ByVal Value As TrajectoireVéhicules.NatureCourantEnum)
      mNatureCourant = Value
    End Set
  End Property

  Public Property CoefGêne() As Single
    Get
      Return mCoefGêne
    End Get
    Set(ByVal Value As Single)
      mCoefGêne = Value
    End Set
  End Property

  Public Function valTrafic(ByVal unTrafic As Trafic) As Short
    Return unTrafic.QVéhicule(Branche(TrajectoireVéhicules.OrigineDestEnum.Origine), _
                              Branche(TrajectoireVéhicules.OrigineDestEnum.Destination))
  End Function

  Public Sub DessinerPhase(ByVal unGraphique As PolyArc, ByVal uneLigneVéhicules As LigneFeuVéhicules)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim mAxeDépart As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = uneLigneVéhicules.Dessin.MilieuF
    Dim strNuméroLigne As String = uneLigneVéhicules.strNuméro

    BrancheOrigine = Branche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de symétrie pour obtenir une parallèle à celle-ci passant au milieu de la ligne de feux
      With .LigneDeSymétrie
        P1 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 5mm (pour inscrire facilement le numéro de la ligne de feux
    P2 = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
    'L'axe départ : Axe de l'ensemble des voies entrantes commandées par la ligne de feux
    mAxeDépart = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeDépart, False)

    'Décrire le cercle entourant le numéro de ligne de feux
    Dim pCentre As Point = PointPosition(CvPoint(P2), 5, AngleBrancheRadians)
    Dim unCercle As New Cercle(pCentre, 2, unePlume)
    unGraphique.Add(unCercle)
    'Définir la boite d'encombrement du texte en fonction de la taille du cercle
    Dim uneBoite As Boite = Boite.NouvelleBoite(unCercle.Rayon)
    uneBoite = CType(uneBoite.Translation(pCentre), Boite)
    'Définir le texte contenant le numéro
    Dim unTexte As Texte = New Texte(strNuméroLigne, uneBoite, New SolidBrush(Color.Red), New Font("Arial", 8))
    unGraphique.Add(unTexte)

    Dim pO, pO2 As PointF
    Dim mFlèche, uneFlèche As Fleche
    ' Créer une flèche 
    'uneFlèche = New Fleche(0, HauteurFlèche:=3, Delta:=-2, unePlume:=New Pen(Color.Black))
    uneFlèche = New Fleche(0, HauteurFlèche:=2, SegmentCentral:=False, unePlume:=unePlume)

    Select Case NatureCourant
      Case TrajectoireVéhicules.NatureCourantEnum.TAD
        pO = PointPosition(mAxeDépart.pAF, 5, AngleBrancheRadians - Math.PI / 2)
        unAngle += 90
        unGraphique.Add(New Arc(pO, 5, unAngle Mod 360, 90, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)

      Case TrajectoireVéhicules.NatureCourantEnum.TAG
        'Rallonger la ligne axe origine de 5mm
        mAxeDépart.pAF = PointPosition(mAxeDépart.pAF, 5, AngleBrancheRadians + Math.PI)
        pO = PointPosition(mAxeDépart.pAF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne à gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de départ
        unAngle += 180
        unGraphique.Add(New Arc(pO, 5, unAngle Mod 360, 90, unePlume))

        ' Positionner la flèche à l'extrémité de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)

      Case TrajectoireVéhicules.NatureCourantEnum.TD
        'Dim BrancheDestination As Branche
        'Dim L1, L2 As Ligne
        'Dim mAxeFin As Ligne
        'BrancheDestination = Branche(TrajectoireVéhicules.OrigineDestEnum.Destination)
        '
        'L1 = BrancheDestination.BordChaussée(DiagFeux.Branche.Latéralité.Droite)
        'If BrancheDestination.SensUnique Then
        '  L2 = BrancheDestination.BordChaussée(DiagFeux.Branche.Latéralité.Gauche)
        'Else
        '  L2 = BrancheDestination.BordVoiesEntrantes(DiagFeux.Branche.Latéralité.Droite)
        'End If
        'mAxeFin = New Ligne(Milieu(L1.pAF, L2.pAF), Milieu(L1.pBF, L2.pBF), unePlume)

        'Dim LigneRaccord As New Ligne(mAxeDépart.pA, mAxeFin.pA, New Pen(Color.Black))

        'mGraphique.Add(CréerRaccord(mAxeDépart, LigneRaccord, unePlume:=unePlume))
        'LigneRaccord = LigneRaccord.Inversée
        'mGraphique.Add(CréerRaccord(LigneRaccord, mAxeFin, unePlume:=unePlume))
        'mGraphique.Add(LigneRaccord.Inversée, PoignéesACréer:=False)
        'mGraphique.Add(mAxeFin, poignéesacréer:=False)

        'Rallonger la ligne axe origine de 20mm
        mAxeDépart.pAF = PointPosition(mAxeDépart.pAF, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la flèche à l'extrémité de l'axe de la ligne de feux
        pO2 = mAxeDépart.pAF
        mFlèche = uneFlèche.RotTrans(pO2, AngleBrancheRadians)
    End Select
    'Ajouter la flèche 
    unGraphique.Add(mFlèche)

  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe CourantCollection--------------------------
'=====================================================================================================
Public Class CourantCollection : Inherits CollectionBase
  Private mTrafic As Trafic

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unCourant As Courant) As Short
    Return Me.List.Add(unCourant)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Courant)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unCourant As Courant)
    If Me.List.Contains(unCourant) Then
      Me.List.Remove(unCourant)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unCourant As Courant)
    Me.List.Insert(Index, unCourant)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Courant
    Get
      Return CType(Me.List.Item(Index), Courant)
    End Get
  End Property

  ' Creer une autre propriété par défaut Item pour cette collection.
  ' Permet la  recherche du courant à partir de ses composants.
  Default Public ReadOnly Property Item(ByVal BrancheOrigine As Branche, ByVal BrancheDestination As Branche) As Courant
    Get
      Dim unCourant As Courant
      For Each unCourant In Me.List
        If unCourant.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine) Is BrancheOrigine Then
          If unCourant.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination) Is BrancheDestination Then
            Return unCourant
          End If
        End If
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unCourant As Courant) As Short
    Return Me.List.IndexOf(unCourant)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal unCourant As Courant) As Boolean
    Return Me.List.Contains(unCourant)
  End Function

End Class


'=====================================================================================================
'--------------------------- Classe Plumes --------------------------
'=====================================================================================================
Public Class Plumes
  Public Enum BrosseEnum
    BrancheNomRue
    SignalFeuID
    SignalFeuIDImpression
    PhaseNuméroImpression
    ConflitSystématique
    ConflitAdmis
    ConflitNonAdmis
  End Enum

  Public Enum PlumeEnum
    BrancheAxe
    BrancheBordChaussée
    BrancheVoie
    BrancheSéparVoie
    BrancheId
    PassageContour
    PassageZebras
    Ilot
    IlotImpression
    Trajectoire
    TrajectoireImpression
    TrajectoireFlèches
    TraverséeContour
    TraverséeFlèche
    TraverséeFlècheImpression
    LigneFeuVéhicule
    LigneFeuVéhiculeImpression
    SignalFeu
    SignalFeuImpression
    SignalFeuLigneRappel
    SignalFeuTexte
    PhaseImpression
    PhaseNuméroImpression
    EllipseTraficImpression
    ConflitAdmissible
  End Enum

  Private mPlume(PlumeEnum.ConflitAdmissible) As Pen
  Private mBrosse(BrosseEnum.ConflitNonAdmis) As SolidBrush
  Private mCouleur(PlumeEnum.ConflitAdmissible) As Color

  Public Sub New()
    Dim unePlumeNoire As New Pen(Color.Black)
    Dim unePlumeBleue As New Pen(Color.Blue)
    Dim unePlumeRouge As New Pen(Color.Red)

    mPlume(PlumeEnum.BrancheAxe) = unePlumeNoire.Clone
    Dim EspacementTiretAxe() As Single = {10, 10, 1, 10}
    With mPlume(PlumeEnum.BrancheAxe)
      .Width = 0.3
      .DashStyle = Drawing2D.DashStyle.DashDot
      .DashPattern = EspacementTiretAxe
    End With

    mPlume(PlumeEnum.BrancheBordChaussée) = unePlumeNoire
    mPlume(PlumeEnum.BrancheVoie) = unePlumeBleue
    mPlume(PlumeEnum.BrancheSéparVoie) = unePlumeBleue.Clone
    Dim EspacementTiret() As Single = {10, 10}
    With Plume(PlumeEnum.BrancheSéparVoie)
      .DashStyle = Drawing2D.DashStyle.Dash
      .DashPattern = EspacementTiret
    End With

    mPlume(PlumeEnum.Ilot) = unePlumeNoire
    mPlume(PlumeEnum.IlotImpression) = unePlumeNoire

    mPlume(PlumeEnum.PassageContour) = unePlumeNoire

    mPlume(PlumeEnum.Trajectoire) = unePlumeRouge.Clone
    mPlume(PlumeEnum.TrajectoireImpression) = unePlumeNoire
    mPlume(PlumeEnum.TrajectoireFlèches) = unePlumeNoire

    mPlume(PlumeEnum.TraverséeContour) = New Pen(Color.Red, Width:=2)       '2 pixels de large
    With Plume(PlumeEnum.TraverséeContour)
      EspacementTiret(0) = 2
      EspacementTiret(1) = 2
      .DashPattern = EspacementTiret
      .DashStyle = Drawing2D.DashStyle.Dot
    End With

    mPlume(PlumeEnum.TraverséeFlèche) = New Pen(Color.Black, Width:=2)    '2 pixels de large
    mPlume(PlumeEnum.TraverséeFlècheImpression) = New Pen(Color.Black, Width:=0.4)  ' 4/10è mm

    mPlume(PlumeEnum.LigneFeuVéhicule) = New Pen(Color.Blue, Width:=2)     '2 pixels de large
    mPlume(PlumeEnum.LigneFeuVéhiculeImpression) = New Pen(Color.Blue, Width:=1)   ' 10/10è mm

    mPlume(PlumeEnum.SignalFeu) = unePlumeBleue
    mPlume(PlumeEnum.SignalFeuImpression) = unePlumeBleue
    mPlume(PlumeEnum.SignalFeuLigneRappel) = New Pen(Color.Gray, width:=0.3)
    mPlume(PlumeEnum.PhaseImpression) = New Pen(Color.Green, 0.3) ' 3/10 mm
    mPlume(PlumeEnum.PhaseNuméroImpression) = unePlumeNoire
    mPlume(PlumeEnum.EllipseTraficImpression) = New Pen(Color.Black, Width:=0.4)  '4/10 mm

    mPlume(PlumeEnum.ConflitAdmissible) = unePlumeRouge.Clone

    mBrosse(BrosseEnum.BrancheNomRue) = New SolidBrush(Color.Black)
    mBrosse(BrosseEnum.SignalFeuID) = New SolidBrush(Color.Blue)
    mBrosse(BrosseEnum.SignalFeuIDImpression) = New SolidBrush(Color.Blue)
    mBrosse(BrosseEnum.PhaseNuméroImpression) = New SolidBrush(Color.Red)

    mBrosse(BrosseEnum.ConflitSystématique) = New SolidBrush(Color.Red)
    mBrosse(BrosseEnum.ConflitAdmis) = New SolidBrush(Color.LightGreen)
    mBrosse(BrosseEnum.ConflitNonAdmis) = New SolidBrush(Color.LightSalmon)

    mCouleur(PlumeEnum.BrancheAxe) = Color.Black
    mCouleur(PlumeEnum.BrancheBordChaussée) = Color.Black
    mCouleur(PlumeEnum.BrancheVoie) = Color.Blue
    mCouleur(PlumeEnum.BrancheSéparVoie) = Color.Blue

  End Sub

  Public ReadOnly Property Plume(ByVal iPlume As PlumeEnum) As Pen
    Get
      Return mPlume(iPlume)
    End Get
  End Property

  Public ReadOnly Property Brosse(ByVal iBrosse As BrosseEnum) As SolidBrush
    Get
      Return mBrosse(iBrosse)
    End Get
  End Property


  Public ReadOnly Property Couleur(ByVal iPlume As PlumeEnum) As Color
    Get
      Return mCouleur(iPlume)
    End Get
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Nord --------------------------
'=====================================================================================================
Public Class Nord : Inherits Métier
  Private mFlèche As Fleche
  Private mpRef As Point
  Private mRotation As Single
  Private milieuFlèche As Point
  Const lgFlèche As Short = 40
  Const hFlèche As Short = 4
  Const DemiLargeurBoite As Short = 8
  Private mAffiché As Boolean

  Public Sub New()
    'Par défaut : en haut à gauche
    With mpRef
      .X = 30
      .Y = 30
    End With
    'Par défaut : vers le haut (les angles sont dans le sens des aiguilles d'une montre)
    mRotation = 3 * PI / 2
  End Sub

  Public Sub New(ByVal pRef As Point, ByVal Rotation As Single)
    mpRef = pRef
    mRotation = Rotation
  End Sub

  Public Sub New(ByVal uneRowNord As DataSetDiagfeux.NordRow)

    With uneRowNord
      mpRef = New Point(.GetpNordRows(0).X, .GetpNordRows(0).Y)
      mRotation = .Rotation
      mAffiché = .Visible
    End With

  End Sub

  Public Sub Enregistrer(ByVal uneRowAffichage As DataSetDiagfeux.AffichageRow)
    Dim uneRowNord As DataSetDiagfeux.NordRow = ds.Nord.AddNordRow(mRotation, mAffiché, uneRowAffichage)
    With mpRef
      ds.pNord.AddpNordRow(.X, .Y, uneRowNord)
    End With

  End Sub

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me

    Dim pEcriture As Point
    Dim uneFlèche As Fleche

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      uneFlèche = New Fleche(lgFlèche, hFlèche, unePlume:=New Pen(Color.Black))

      'Positionner la flèche
      mFlèche = uneFlèche.RotTrans(mpRef, mRotation + sngPI)
      mGraphique.Add(mFlèche)
      milieuFlèche = mFlèche.LigneRéférence.Milieu

      'Ecrire 'N' au bout de la flèche
      pEcriture = PointPosition(mpRef, hFlèche + DemiLargeurBoite * 2, mRotation)
      mGraphique.CréerBoiteTexte(pEcriture, DemiLargeurBoite, "N", New SolidBrush(Color.Black))

    Else
      Dim pRef As New Point(mpRef.X / 4, mpRef.Y / 4)
      pRef = Translation(pRef, cndZoneGraphique.Location)
      uneFlèche = New Fleche(lgFlèche / 4, hFlèche / 4, unePlume:=New Pen(Color.Black))

      'Positionner la flèche
      mFlèche = uneFlèche.RotTrans(pRef, mRotation + sngPI)
      mGraphique.Add(mFlèche)

      'Ecrire 'N' au bout de la flèche
      pEcriture = PointPosition(pRef, hFlèche / 4 + DemiLargeurBoite / 2, mRotation)
      mGraphique.CréerBoiteTexte(pEcriture, DemiLargeurBoite / 4, "N", New SolidBrush(Color.Black))

    End If


    mGraphique.Invisible = Not mAffiché

    'With pEcriture
    '  Select Case Rotation
    '    Case -PI / 4 To PI / 4
    '      .X += 8
    '    Case PI / 4 To 3 * PI / 4
    '      .Y -= 8
    '    Case 3 * PI / 4 To PI, -PI To -3 * PI / 4
    '      .X -= 8
    '    Case -3 * PI / 4 To -PI / 4
    '      .Y += 8
    '  End Select

    'End With

    uneCollection.Add(mGraphique)

  End Function

  Public Function Déplaçable(ByVal pEnCours As Point) As Boolean
    Return Distance(pEnCours, milieuFlèche) < lgFlèche / 4
  End Function

  Public Function Orientable(ByVal pEnCours As Point) As Boolean
    Return Distance(pEnCours, PointPosition(mpRef, hFlèche, mRotation)) < RaySélect
  End Function

  Public Property PtRéférence() As Point
    Get
      Return mpRef
    End Get
    Set(ByVal Value As Point)
      mpRef = Value
    End Set
  End Property

  Public ReadOnly Property LigneRéférence() As Ligne
    Get
      Return CType(mGraphique(0), Fleche).LigneRéférence
    End Get
  End Property

  Public Property Orientation() As Single
    Get
      Return mRotation
    End Get
    Set(ByVal Value As Single)
      mRotation = Value
      PtRéférence = PointPosition(LigneRéférence.pB, lgFlèche, mRotation)
    End Set
  End Property

  Public Property Affiché() As Boolean
    Get
      Return mAffiché
    End Get
    Set(ByVal Value As Boolean)
      mAffiché = Value
      If Not IsNothing(mGraphique) Then
        mGraphique.Invisible = Not Value
      End If
    End Set
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe SymEchelle --------------------------
'=====================================================================================================
Public Class SymEchelle : Inherits Métier
  Private mpRef As Point
  Private mLigneRéférence As Ligne
  Private mContour As PolyArc
  Private lBarre As Short = 50
  Private mAffiché As Boolean

  Public Sub New()
    'Par défaut : en haut à gauche
    With mpRef
      .X = 10
      .Y = 10
    End With
  End Sub

  Public Sub New(ByVal pRef As Point)
    mpRef = pRef
  End Sub

  Public Sub New(ByVal uneRowEchelle As DataSetDiagfeux.SymEchelleRow)

    With uneRowEchelle
      mpRef = New Point(.GetpSymEchelleRows(0).X, .GetpSymEchelleRows(0).Y)
      mAffiché = .Visible
    End With

  End Sub

  Public Sub Enregistrer(ByVal uneRowAffichage As DataSetDiagfeux.AffichageRow)
    Dim uneRowEchelle As DataSetDiagfeux.SymEchelleRow = ds.SymEchelle.AddSymEchelleRow(mAffiché, uneRowAffichage)
    With mpRef
      ds.pSymEchelle.AddpSymEchelleRow(.X, .Y, uneRowEchelle)
    End With
  End Sub

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetMétier = Me

    Dim i, nbSecteurs As Short
    Dim pOrigine As Point = mpRefRéel()
    Dim unRectangle As Rectangle
    Dim unPolyArc As PolyArc
    Dim pts(3) As Point
    Dim PtsContour(3) As Point
    Dim hBarre As Short
    Dim DemiLargeurBoite As Short


    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      hBarre = 6
      DemiLargeurBoite = 16

    Else
      hBarre = 2
      DemiLargeurBoite = 4
    End If

    'Déterminer si l'échelle en cours permet d'afficher le symbole d'échelle
    Select Case ToRéel(45)
      Case 0 To 1
        Return mGraphique

      Case 1 To 3
        nbSecteurs = 3
        lBarre = ToDessin(3)
      Case 3 To 7.5
        nbSecteurs = 5
        lBarre = ToDessin(5)
      Case 7.5 To 12.5
        nbSecteurs = 5
        lBarre = ToDessin(10)
      Case 12.5 To 17.5
        nbSecteurs = 3
        lBarre = ToDessin(15)
      Case 17.5 To 22.5
        nbSecteurs = 4
        lBarre = ToDessin(20)
      Case 22.5 To 27.5
        nbSecteurs = 5
        lBarre = ToDessin(25)
      Case Else
        Return mGraphique
    End Select

    mLigneRéférence = New Ligne(New Point(pOrigine.X, pOrigine.Y + hBarre / 2), New Point(pOrigine.X + lBarre, pOrigine.Y + hBarre / 2))

    For i = 0 To nbSecteurs - 1
      'Autant de petits rectangles que de secteurs prédéterminés
      unRectangle = New Rectangle(pOrigine, New Size(lBarre / nbSecteurs, hBarre))
      With unRectangle
        pts(0) = .Location
        pts(1) = New Point(.Right, .Top)
        pts(2) = New Point(.Right, .Bottom)
        pts(3) = New Point(.Left, .Bottom)
        If i = 0 Then
          PtsContour(0) = pts(0)
          PtsContour(3) = pts(3)
        ElseIf i = nbSecteurs - 1 Then
          PtsContour(1) = pts(1)
          PtsContour(2) = pts(2)
        End If
      End With
      unPolyArc = New PolyArc(pts, Clore:=True)
      If (i Mod 2 = 0) Then
        'Alterner les secteurs pleins et les vides
        unPolyArc.APeindre = True
        unPolyArc.Brosse = New SolidBrush(Color.Black)
      Else
        unPolyArc.Plume = New Pen(Color.Black)
      End If
      mGraphique.Add(unPolyArc)
      pOrigine.X += lBarre / nbSecteurs
    Next

    mContour = New PolyArc(PtsContour, clore:=True)

    Dim pEcriture As Point = PointPosition(mLigneRéférence.pB, DemiLargeurBoite, 0)
    mGraphique.CréerBoiteTexte(pEcriture, DemiLargeurBoite, CType(ToRéel(lBarre), Short) & "m", New SolidBrush(Color.Black), unefonte:=New Font("Arial", 8))

    mGraphique.Invisible = Not mAffiché

    uneCollection.Add(mGraphique)
  End Function

  Private Function mpRefRéel() As Point
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Return New Point(mpRef.X, yMaxPicture - mpRef.Y)
    Else
      Return New Point(cndZoneGraphique.X + mpRef.X / 4, cndZoneGraphique.Bottom - mpRef.Y / 4 - 5)
    End If
  End Function

  Public Property PtRéférence() As Point
    Get
      Return mpRef
    End Get
    Set(ByVal Value As Point)
      mpRef = Value
    End Set
  End Property

  Public ReadOnly Property LigneRéférence() As Ligne
    Get
      Return mLigneRéférence
    End Get
  End Property

  Public Function Déplaçable(ByVal pEnCours As Point) As Boolean
    Return mContour.Intérieur(pEnCours)
  End Function

  Public Property Affiché() As Boolean
    Get
      Return mAffiché
    End Get
    Set(ByVal Value As Boolean)
      mAffiché = Value
      If Not IsNothing(mGraphique) Then
        mGraphique.Invisible = Not Value
      End If
    End Set
  End Property
End Class