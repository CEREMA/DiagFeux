'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : M�tier.vb										  													'
'						Classes																														'
'							M�tier														  														'
'							Courant								            															'
'							CourantCollection       																		    '
'             Antagonisme 			  																						'
'							AntagonismeCollection       																		'
'******************************************************************************
Imports System.Math

Public MustInherit Class M�tier

  Public mGraphique As PolyArc

  Public Sub SupprimerGraphique(ByVal uneCollection As Graphiques)
    uneCollection.Remove(mGraphique)
  End Sub
  Public MustOverride Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc

End Class

'=====================================================================================================
'--------------------------- Classe Antagonisme  --------------------------
'=====================================================================================================
Public Class Antagonisme : Inherits M�tier
  Private mTrajV�hicule1 As TrajectoireV�hicules
  'La 2�me trajectoire du conflit est v�hicules ou pi�tons
  Private mTraject2 As Trajectoire
  Private mPoint As PointF
  Private mTypeConflit As Trajectoire.TypeConflitEnum
  Private dctFiliation As New Hashtable
  Private mM�mesCourants As Antagonisme
  Private mAntagoOrigine As Antagonisme

  Public Enum AntagonismeEnum
    TDTD    ' Syst�matique
    TDPi�tons 'Syst�matique
    TDTAG   ' Syst�matique si phase sp�ciale TAG
    TDTAD
    TAGTAD
    TAGTAG
    TADTAD
    TAGPi�tons ' Impos� si le TDTAG associ� est non admis
    TADPi�tons
    TAGPi�tonsEtSensUnique
    TADPi�tonsEtSensUnique
  End Enum
  Private mTypeCourantsAntagonistes As AntagonismeEnum

  Public Enum PositionEnum
    Premier
    Dernier
  End Enum

  Public ReadOnly Property EstPi�ton() As Boolean
    Get
      Return TypeOf mTraject2 Is Travers�ePi�tonne
    End Get
  End Property

  Public ReadOnly Property EstV�hicule() As Boolean
    Get
      Return TypeOf mTraject2 Is TrajectoireV�hicules
    End Get
  End Property

  Public Property M�mesCourants() As Antagonisme
    Get
      Return mM�mesCourants
    End Get
    Set(ByVal Value As Antagonisme)
      mM�mesCourants = Value
    End Set
  End Property

  Private ReadOnly Property mVariante() As Variante
    Get
      Return mTrajV�hicule1.mVariante
    End Get
  End Property

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me
    Select Case mTypeConflit
      Case Trajectoire.TypeConflitEnum.Syst�matique
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitSyst�matique).Clone))
      Case Trajectoire.TypeConflitEnum.Admissible
        '        mGraphique.Add(New Cercle(PointDessin(mPoint), 3, unePlume:=cndPlumes.Plume(Plumes.PlumeEnum.ConflitAdmissible).Clone))
        'Curieusement le cast de Object vers Pen suite � la m�thode clone est refus� par VS2005, alors qu'il passe pour un SolidBrush 
        mGraphique.Add(New Cercle(PointDessin(mPoint), 3, unePlume:=CType(cndPlumes.Plume(Plumes.PlumeEnum.ConflitAdmissible).Clone, Pen)))
      Case Trajectoire.TypeConflitEnum.Admis
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitAdmis).Clone))
      Case Trajectoire.TypeConflitEnum.NonAdmis
        mGraphique.Add(New Cercle(PointDessin(mPoint), 4, uneBrosse:=cndPlumes.Brosse(Plumes.BrosseEnum.ConflitNonAdmis).Clone))
    End Select

    'Pour DIAGFEUX : les objets graphiques originaux (ceux construits par les trajectoires) ne sont pas dessin�s : seuls les ont ceux correspondant � un sc�nario
    uneCollection.Add(mGraphique)

    Return mGraphique

  End Function

  Public Sub New(ByVal unAntagonisme As Antagonisme, Optional ByVal DuplicationIncompl�te As Boolean = True)

    With unAntagonisme
      Me.mTrajV�hicule1 = .mTrajectoire(PositionEnum.Premier)
      Me.mTraject2 = .mTrajectoire(PositionEnum.Dernier)
      mPoint = .Position
      mTypeConflit = .TypeConflit
      If mTypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique And DuplicationIncompl�te Then
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

  Public Sub New(ByVal T1 As TrajectoireV�hicules, ByVal T2 As Trajectoire, ByVal p As PointF, ByVal TypeConflit As Trajectoire.TypeConflitEnum)

    Me.mTrajV�hicule1 = T1
    Me.mTraject2 = T2
    mPoint = p
    mTypeConflit = TypeConflit

    Try
      Dim msg As String = Nothing
      If EstPi�ton Then
        Select Case mTrajV�hicule1.NatureCourant
          Case TrajectoireV�hicules.NatureCourantEnum.TD
            mTypeCourantsAntagonistes = AntagonismeEnum.TDPi�tons
          Case TrajectoireV�hicules.NatureCourantEnum.TAG
            If Tp.mBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              mTypeCourantsAntagonistes = AntagonismeEnum.TAGPi�tonsEtSensUnique
            Else
              mTypeCourantsAntagonistes = AntagonismeEnum.TAGPi�tons
            End If
          Case TrajectoireV�hicules.NatureCourantEnum.TAD
            If Tp.mBranche.SensUnique(Voie.TypeVoieEnum.VoieSortante) Then
              mTypeCourantsAntagonistes = AntagonismeEnum.TADPi�tonsEtSensUnique
            Else
              mTypeCourantsAntagonistes = AntagonismeEnum.TADPi�tons
            End If
        End Select

      Else
        Dim NatureCourant2 As TrajectoireV�hicules.NatureCourantEnum = CType(mTraject2, TrajectoireV�hicules).NatureCourant

        Select Case mTrajV�hicule1.NatureCourant
          Case TrajectoireV�hicules.NatureCourantEnum.TD
            Select Case NatureCourant2
              Case TrajectoireV�hicules.NatureCourantEnum.TD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTD
              Case TrajectoireV�hicules.NatureCourantEnum.TAG
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAG
              Case TrajectoireV�hicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAD
            End Select

          Case TrajectoireV�hicules.NatureCourantEnum.TAG
            Select Case NatureCourant2
              Case TrajectoireV�hicules.NatureCourantEnum.TAG
                mTypeCourantsAntagonistes = AntagonismeEnum.TAGTAG
              Case TrajectoireV�hicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TAGTAD
              Case Else
                msg = "Anomalie Antagonisme TAG"
            End Select

          Case TrajectoireV�hicules.NatureCourantEnum.TAD
            Select Case NatureCourant2
              Case TrajectoireV�hicules.NatureCourantEnum.TAD
                mTypeCourantsAntagonistes = AntagonismeEnum.TDTAD
              Case Else
                msg = "Anomalie Antagonisme TAD"
            End Select

        End Select

      End If

      If Not IsNothing(msg) Then
        Throw New DiagFeux.M�tierException(msg)
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Cr�ation de l'antagonisme")
    End Try

  End Sub

  Private ReadOnly Property Tv2() As TrajectoireV�hicules
    Get
      Return CType(mTraject2, TrajectoireV�hicules)
    End Get
  End Property

  Public ReadOnly Property Tp() As Travers�ePi�tonne
    Get
      Return CType(mTraject2, Travers�ePi�tonne)
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

  Public ReadOnly Property Autoris�() As Boolean
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
        Case Trajectoire.TypeConflitEnum.Syst�matique, Trajectoire.TypeConflitEnum.NonAdmis, Trajectoire.TypeConflitEnum.Admissible
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

  Public ReadOnly Property R�solu() As Boolean
    Get
      Select Case mTypeConflit
        Case Trajectoire.TypeConflitEnum.Admis, Trajectoire.TypeConflitEnum.NonAdmis
          Return True
      End Select
    End Get
  End Property

  '********************************************************************************************************************
  ' Obtient ou d�finit si un antagonisme est d�pendant de celui-ci
  ' unAntagonisme : antagonisme pour lequel la d�pendance est � d�finir ou � rechercher
  '********************************************************************************************************************

  Public Property EstP�re(ByVal unAntagonisme As Antagonisme) As Boolean
    Get
      If dctFiliation.ContainsKey(unAntagonisme) Then
        EstP�re = True
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
        If unAntagonisme.AntagoOrigine.EstP�re(AntagoOrigine) AndAlso unAntagonisme.Interdit Then
          Return True
        End If
      Next
    End Get
  End Property

  Public Sub Verrouiller()

    If cndContexte = [Global].OngletEnum.Conflits Then
      If mVariante.Verrou > [Global].Verrouillage.LignesFeux Then
        'Voir tous les conflits d�s que la matrice des conflits est verrouill�e
        Masqu� = False
      Else
        If mTypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
          Masqu� = True
        ElseIf IsNothing(mVariante.BrancheEnCoursAntagonisme) Then
          'On souhaite afficher tous les antagonismes et non seulement ceux issus d'une branche particuli�re
          Masqu� = False
        Else
          Masqu� = Not BrancheCourant1 Is mVariante.BrancheEnCoursAntagonisme
        End If
      End If

    Else
      Masqu� = True
    End If

    mGraphique.RendreS�lectable(Not Masqu�, Editable:=Not Masqu�)

  End Sub

  Public ReadOnly Property mTrajectoire(ByVal Index As PositionEnum) As Trajectoire
    Get
      If Index = PositionEnum.Premier Then
        Return mTrajV�hicule1
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
  ' Recherche si un antagonisme est d�pendant du couple de ligne de feux (LigneFeu1,LigneFeu2)
  '*******************************************************************************************
  Public Function AntagonismeLi�(ByVal LigneFeu1 As LigneFeux, ByVal LigneFeu2 As LigneFeux) As Boolean
    Dim LV1 As LigneFeuV�hicules = LigneFeu(PositionEnum.Premier)
    Dim L2 As LigneFeux = LigneFeu(PositionEnum.Dernier)

    If (LigneFeu1 Is LV1 And LigneFeu2 Is L2) Or (LigneFeu1 Is L2 And LigneFeu2 Is LV1) Then
      Return True
    End If

  End Function

  Public ReadOnly Property Courant(ByVal Index As PositionEnum) As Courant
    Get
      If Index = PositionEnum.Premier Then
        With mTrajV�hicule1
          Return mVariante.mCourants(.mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine), _
                                       .mBranche(TrajectoireV�hicules.OrigineDestEnum.Destination))
        End With

      ElseIf EstV�hicule Then
        With CType(mTraject2, TrajectoireV�hicules)
          Return mVariante.mCourants(.mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine), _
                                       .mBranche(TrajectoireV�hicules.OrigineDestEnum.Destination))
        End With
      End If

    End Get
  End Property

  Public ReadOnly Property BrancheCourant1() As Branche
    Get
      Return Courant(Antagonisme.PositionEnum.Premier).Branche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    End Get
  End Property

  Public Function Libell�(ByVal Index As PositionEnum, ByVal Branches As BrancheCollection) As String
    Dim unCourant As Courant = Courant(Index)
    Dim uneTrajectoire As Trajectoire
    Dim Compl�mentLibell� As String

    If IsNothing(unCourant) Then
      '2�me �l�ment de l'antagonisme est une travers�e pi�tonne
      uneTrajectoire = mTraject2
      Compl�mentLibell� = " (" & uneTrajectoire.LigneFeu.ID & ")"
      Libell� = "Pi�tons : " & Branches.ID(Tp.mBranche)
      Libell� &= Compl�mentLibell�

    Else

      With unCourant
        Libell� = Branches.ID(.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine))
        If Index = PositionEnum.Premier Then
          uneTrajectoire = Me.mTrajV�hicule1
        Else
          uneTrajectoire = Me.mTraject2
        End If
        Compl�mentLibell� = " (" & uneTrajectoire.LigneFeu.ID & ")"
        Libell� &= Compl�mentLibell�
        Libell� &= " --> " & Branches.ID(.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination))
      End With

    End If

  End Function

  Public Property Masqu�() As Boolean
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
    uneRowAntagonisme = ds.Antagonisme.AddAntagonismeRow(desTrajectoires.IndexOf(mTrajV�hicule1), desTrajectoires.IndexOf(mTraject2), TypeConflit, uneRowVariante)
    'Ajouter le point de conflit
    ds.pAntago.AddpAntagoRow(mPoint.X, mPoint.Y, uneRowAntagonisme)

  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe AntagonismeCollection--------------------------
'=====================================================================================================
Public Class AntagonismeCollection : Inherits CollectionBase
  Public NonTousSyst�matiques As Boolean

  Private mLignesFeux As LigneFeuxCollection
  Private mBrancheEnCoursAntagonisme As Branche

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal desLignesFeux As LigneFeuxCollection)
    mLignesFeux = desLignesFeux
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unAntagonisme As Antagonisme) As Short
    Return Me.List.Add(unAntagonisme)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Antagonisme)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal unAntagonisme As Antagonisme)
    If Me.List.Contains(unAntagonisme) Then
      Me.List.Remove(unAntagonisme)
    End If

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unAntagonisme As Antagonisme)
    Me.List.Insert(Index, unAntagonisme)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Antagonisme
    Get
      Return CType(Me.List.Item(Index), Antagonisme)
    End Get
  End Property

  Public Function IndexOf(ByVal unAntagonisme As Antagonisme) As Short
    Return Me.List.IndexOf(unAntagonisme)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Function Contains(ByVal unAntagonisme As Antagonisme) As Boolean
    Return Me.List.Contains(unAntagonisme)
  End Function

  Public Function Cr�erGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim unAntagonisme As Antagonisme
    Dim mPoints As Point()
    Dim mPRef(-1) As Point
    Dim dctpRef(-1) As Hashtable
    ReDim mPoints(Count - 1)

    For Each unAntagonisme In Me
      mPoints(IndexOf(unAntagonisme)) = CType(unAntagonisme.Cr�erGraphique(uneCollection).Figures(0), Cercle).pO
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
  ' Rechercher les possibles conflits TAG/Pi�tons li�s � un autre antagonisme :
  ' Si l'Antagonisme p�re devenait non admis, le TAG/Pi�tons ne pourrait plus �tre admis non plus
  '*************************************************************************************************
  Public Sub AntagoFiliation()
    Dim unAntagonisme As Antagonisme
    Dim uneTrajectoire As TrajectoireV�hicules

    Try
      ' Un antagonisme TAG/Pi�tons est li� � tout autre antagonisme dont la trajectoire a m�me origine que le TAG
      For Each unAntagonisme In Me
        With unAntagonisme
          If .TypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique Then
            ' Conflit non syst�matique pouvant �tre li� � un autre conflit non syst�matique
            uneTrajectoire = .mTrajectoire(Antagonisme.PositionEnum.Premier)
            If uneTrajectoire.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
              RechercherTAGPi�tons(unAntagonisme)
            ElseIf .EstV�hicule Then
              uneTrajectoire = .mTrajectoire(Antagonisme.PositionEnum.Dernier)
              If uneTrajectoire.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
                RechercherTAGPi�tons(unAntagonisme, InverserSens:=True)
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
  ' Rechercher les possibles conflits TAG/Pi�tons li�s � AntagonismeP�re :
  ' Si l'Antagonisme p�re devenait non admis, le TAG/Pi�tons ne pourrait plus �tre admis non plus
  '*************************************************************************************************
  Private Sub RechercherTAGPi�tons(ByVal AntagonismeP�re As Antagonisme, Optional ByVal InverserSens As Boolean = False)

    Dim unAntagonisme As Antagonisme
    Dim uneLigneV�hicules As LigneFeuV�hicules
    Dim uneLignePi�tons As LigneFeuPi�tons
    Dim Trajectoire1, Trajectoire2 As Trajectoire

    Try
      If InverserSens Then
        Trajectoire1 = AntagonismeP�re.mTrajectoire(Antagonisme.PositionEnum.Dernier)
        Trajectoire2 = AntagonismeP�re.mTrajectoire(Antagonisme.PositionEnum.Premier)
      Else
        Trajectoire1 = AntagonismeP�re.mTrajectoire(Antagonisme.PositionEnum.Premier)
        Trajectoire2 = AntagonismeP�re.mTrajectoire(Antagonisme.PositionEnum.Dernier)
      End If

      If Trajectoire2.EstV�hicule Then
        ' Trajectoire2 est de courant TD  : il n'y a pas de conflit TAG/TAD, et si conflit TAG/TAG il est syst�matique
        For Each unAntagonisme In Me
          With unAntagonisme
            If Not AntagonismeP�re Is unAntagonisme AndAlso .TypeConflit <> Trajectoire.TypeConflitEnum.Syst�matique Then
              If .mTrajectoire(Antagonisme.PositionEnum.Premier) Is Trajectoire1 And .EstPi�ton Then
                AntagonismeP�re.EstP�re(unAntagonisme) = True
                uneLigneV�hicules = .LigneFeu(Antagonisme.PositionEnum.Premier)
                uneLignePi�tons = .LigneFeu(Antagonisme.PositionEnum.Dernier)
                CType(Trajectoire1.LigneFeu, LigneFeuV�hicules).Cr�erFiliation(uneLigneV�hicules, uneLignePi�tons)
              End If
            End If
          End With
        Next
      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Antagonismes.RechercherTAGPi�tons")
    End Try
  End Sub

  '************************************************************************************************
  ' Rechercher s'il reste encore un conflit non admis concern� pas les 2 lignes de feux
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
  ' Rechercher s'il reste un conflit admis ou non admis concern� pas les 2 lignes de feux
  '************************************************************************************************
  Public Function AntagonismeTypeconflitIncorrect(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Antagonisme
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      With unAntagonisme
        If (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L1 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L2) Or _
        (.LigneFeu(Antagonisme.PositionEnum.Premier) Is L2 And .LigneFeu(Antagonisme.PositionEnum.Dernier) Is L1) Then
          If .Interdit Xor mLignesFeux.EstIncompatible(L1, L2) Then
            'Ce conflit n'a pas le m�me statut que les autres pour ces 2 lignes de feux 
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
          If Not .Autoris� Then
            TypeConflit = Math.Max(TypeConflit, .TypeConflit)
            If .TypeConflit = Trajectoire.TypeConflitEnum.NonAdmis Then
              ListeNonAdmis &= .Libell�(Antagonisme.PositionEnum.Premier, desBranches) & " et " & .Libell�(Antagonisme.PositionEnum.Dernier, desBranches) & vbCrLf
            End If
          End If
        End If
      End With
    Next

    Return TypeConflit

  End Function

  Public Function NbConflitsAR�soudre() As Short
    Dim nb As Short
    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      If unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Admissible Then
        nb += 1
      End If
    Next

    Return nb
  End Function

  Public Function ConflitsPartiellementR�solus() As Boolean

    Dim unAntagonisme As Antagonisme

    For Each unAntagonisme In Me
      If unAntagonisme.R�solu Then
        Return True
      End If
    Next

  End Function

  Public Function Fils(ByVal unAntagonisme As Antagonisme) As AntagonismeCollection
    Dim colAntago As New AntagonismeCollection
    Dim unAntago As Antagonisme

    For Each unAntago In Me
      If unAntago.AntagoOrigine.EstP�re(unAntagonisme.AntagoOrigine) Then
        colAntago.Add(unAntagonisme)
      End If
    Next
    Return colAntago

  End Function

  Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
    Dim unAntagonisme As Antagonisme
    Dim monAntago As Antagonisme = CType(value, Antagonisme)
    Dim AvecPi�tons As Boolean

    'Rechercher si un antagonisme d�j� cr�� ne concerne pas les m�mes courants 

    With monAntago
      ' Par d�faut c'est l'antagonisme lui-m�me 
      .M�mesCourants = monAntago

      For Each unAntagonisme In Me
        AvecPi�tons = .EstPi�ton

        If (AvecPi�tons Xor unAntagonisme.EstV�hicule) Then
          'Ne comparer les 2 antagonismes que s'ils comportent tous 2 soit 1 travers�e pi�tonne soit aucune

          'Comparer les 2 premiers courants
          If unAntagonisme.Courant(Antagonisme.PositionEnum.Premier) Is .Courant(Antagonisme.PositionEnum.Premier) Then
            'Les antagonismes sont identiques si les 2 derniers courants sont identiques 
            ' soit les m�mes travers�es pi�tonnes, soit les m�mes Courants de circulation
            If AvecPi�tons Then
              ' les 2�me courants sont des travers�es pi�tonnes
              If .Tp Is unAntagonisme.Tp Then
                .M�mesCourants = unAntagonisme
                Exit For
              End If
            Else
              ' les 2�me courants sont des trajectoires v�hicules
              If .Courant(Antagonisme.PositionEnum.Dernier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Dernier) Then
                .M�mesCourants = unAntagonisme
                Exit For
              End If
            End If

          ElseIf Not AvecPi�tons Then
            'Comparer le 1er courant avec le 2�me courant de l'autre antagonisme
            If .Courant(Antagonisme.PositionEnum.Premier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Dernier) AndAlso _
               .Courant(Antagonisme.PositionEnum.Dernier) Is unAntagonisme.Courant(Antagonisme.PositionEnum.Premier) Then
              'Les antagonismes sont identiques si 2�me courant est le m�me que le 1er courant de l'autre antagonisme
              .M�mesCourants = unAntagonisme
              Exit For
            End If
          End If
        End If  ' AvecPi�tons Xor unAntagonisme.EstV�hicule

      Next  ' unAntagonisme

    End With

  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe Courant--------------------------
'=====================================================================================================
Public Class Courant

  ' Branches Origine et Destination du courant
  Private mBranche(TrajectoireV�hicules.OrigineDestEnum.Destination) As Branche

  'Le nature de courant peut �tre : TAD(tourne � droite),TAG(tourne � gauche) ou TD (tout droit)
  '##ModelId=4033135E0186
  Private mNatureCourant As TrajectoireV�hicules.NatureCourantEnum

  'Le coefficient de g�ne s'applique � la difficult� d'�coulement du trafic pour le courant consid�r�
  'Par d�faut :  1 pour TD, 1.3 pour TAD et 1.7 pour TAG
  '##ModelId=403313B30232
  Private mCoefG�ne As Single

  'Ligne de feux v�hicules qui commmande le courant
  Private mLigneFeux As LigneFeuV�hicules

  Public Sub New(ByVal BrancheOrigine As Branche, ByVal BrancheDestination As Branche)
    mBranche(TrajectoireV�hicules.OrigineDestEnum.Origine) = BrancheOrigine
    mBranche(TrajectoireV�hicules.OrigineDestEnum.Destination) = BrancheDestination
    mNatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD
    mCoefG�ne = 1.0
  End Sub

  Public Property LigneFeuxCommande() As LigneFeuV�hicules
    Get
      Return mLigneFeux
    End Get
    Set(ByVal Value As LigneFeuV�hicules)
      mLigneFeux = Value
    End Set
  End Property

  Public Property Branche(ByVal Index As TrajectoireV�hicules.OrigineDestEnum) As Branche
    Get
      Return mBranche(Index)
    End Get
    Set(ByVal Value As Branche)
      mBranche(Index) = Value
    End Set
  End Property

  Public Property NatureCourant() As TrajectoireV�hicules.NatureCourantEnum
    Get
      Return mNatureCourant
    End Get
    Set(ByVal Value As TrajectoireV�hicules.NatureCourantEnum)
      mNatureCourant = Value
    End Set
  End Property

  Public Property CoefG�ne() As Single
    Get
      Return mCoefG�ne
    End Get
    Set(ByVal Value As Single)
      mCoefG�ne = Value
    End Set
  End Property

  Public Function valTrafic(ByVal unTrafic As Trafic) As Short
    Return unTrafic.QV�hicule(Branche(TrajectoireV�hicules.OrigineDestEnum.Origine), _
                              Branche(TrajectoireV�hicules.OrigineDestEnum.Destination))
  End Function

  Public Sub DessinerPhase(ByVal unGraphique As PolyArc, ByVal uneLigneV�hicules As LigneFeuV�hicules)
    Dim BrancheOrigine As Branche
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.PhaseImpression).Clone
    Dim mAxeD�part As Ligne
    Dim AngleBrancheRadians As Single
    Dim unAngle As Single
    Dim P1, P2 As PointF
    Dim pMilieuLigneFeux As PointF = uneLigneV�hicules.Dessin.MilieuF
    Dim strNum�roLigne As String = uneLigneV�hicules.strNum�ro

    BrancheOrigine = Branche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    With BrancheOrigine
      'Convertir l'angle en sens horaire
      unAngle = 360 - .Angle
      AngleBrancheRadians = .AngleEnRadians
      'Projeter la ligne de sym�trie pour obtenir une parall�le � celle-ci passant au milieu de la ligne de feux
      With .LigneDeSym�trie
        P1 = Projection(.pAF, pMilieuLigneFeux, AngleBrancheRadians)
        P2 = Projection(.pBF, pMilieuLigneFeux, AngleBrancheRadians)
      End With
    End With

    'Diminuer l'axe de 5mm (pour inscrire facilement le num�ro de la ligne de feux
    P2 = PointPosition(P2, 5, AngleBrancheRadians + Math.PI)
    'L'axe d�part : Axe de l'ensemble des voies entrantes command�es par la ligne de feux
    mAxeD�part = New Ligne(P1, P2, unePlume)
    unGraphique.Add(mAxeD�part, False)

    'D�crire le cercle entourant le num�ro de ligne de feux
    Dim pCentre As Point = PointPosition(CvPoint(P2), 5, AngleBrancheRadians)
    Dim unCercle As New Cercle(pCentre, 2, unePlume)
    unGraphique.Add(unCercle)
    'D�finir la boite d'encombrement du texte en fonction de la taille du cercle
    Dim uneBoite As Boite = Boite.NouvelleBoite(unCercle.Rayon)
    uneBoite = CType(uneBoite.Translation(pCentre), Boite)
    'D�finir le texte contenant le num�ro
    Dim unTexte As Texte = New Texte(strNum�roLigne, uneBoite, New SolidBrush(Color.Red), New Font("Arial", 8))
    unGraphique.Add(unTexte)

    Dim pO, pO2 As PointF
    Dim mFl�che, uneFl�che As Fleche
    ' Cr�er une fl�che 
    'uneFl�che = New Fleche(0, HauteurFl�che:=3, Delta:=-2, unePlume:=New Pen(Color.Black))
    uneFl�che = New Fleche(0, HauteurFl�che:=2, SegmentCentral:=False, unePlume:=unePlume)

    Select Case NatureCourant
      Case TrajectoireV�hicules.NatureCourantEnum.TAD
        pO = PointPosition(mAxeD�part.pAF, 5, AngleBrancheRadians - Math.PI / 2)
        unAngle += 90
        unGraphique.Add(New Arc(pO, 5, unAngle Mod 360, 90, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians + Math.PI / 2)

      Case TrajectoireV�hicules.NatureCourantEnum.TAG
        'Rallonger la ligne axe origine de 5mm
        mAxeD�part.pAF = PointPosition(mAxeD�part.pAF, 5, AngleBrancheRadians + Math.PI)
        pO = PointPosition(mAxeD�part.pAF, 5, AngleBrancheRadians + Math.PI / 2)
        ' Tourne � gauche : rajouter 90 en +des 90 car l'angle final est en fait l'angle de d�part
        unAngle += 180
        unGraphique.Add(New Arc(pO, 5, unAngle Mod 360, 90, unePlume))

        ' Positionner la fl�che � l'extr�mit� de l'arc de cercle
        pO2 = PointPosition(pO, 5, AngleBrancheRadians + Math.PI)
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians - Math.PI / 2)

      Case TrajectoireV�hicules.NatureCourantEnum.TD
        'Dim BrancheDestination As Branche
        'Dim L1, L2 As Ligne
        'Dim mAxeFin As Ligne
        'BrancheDestination = Branche(TrajectoireV�hicules.OrigineDestEnum.Destination)
        '
        'L1 = BrancheDestination.BordChauss�e(DiagFeux.Branche.Lat�ralit�.Droite)
        'If BrancheDestination.SensUnique Then
        '  L2 = BrancheDestination.BordChauss�e(DiagFeux.Branche.Lat�ralit�.Gauche)
        'Else
        '  L2 = BrancheDestination.BordVoiesEntrantes(DiagFeux.Branche.Lat�ralit�.Droite)
        'End If
        'mAxeFin = New Ligne(Milieu(L1.pAF, L2.pAF), Milieu(L1.pBF, L2.pBF), unePlume)

        'Dim LigneRaccord As New Ligne(mAxeD�part.pA, mAxeFin.pA, New Pen(Color.Black))

        'mGraphique.Add(Cr�erRaccord(mAxeD�part, LigneRaccord, unePlume:=unePlume))
        'LigneRaccord = LigneRaccord.Invers�e
        'mGraphique.Add(Cr�erRaccord(LigneRaccord, mAxeFin, unePlume:=unePlume))
        'mGraphique.Add(LigneRaccord.Invers�e, Poign�esACr�er:=False)
        'mGraphique.Add(mAxeFin, poign�esacr�er:=False)

        'Rallonger la ligne axe origine de 20mm
        mAxeD�part.pAF = PointPosition(mAxeD�part.pAF, 20, AngleBrancheRadians + Math.PI)

        ' Positionner la fl�che � l'extr�mit� de l'axe de la ligne de feux
        pO2 = mAxeD�part.pAF
        mFl�che = uneFl�che.RotTrans(pO2, AngleBrancheRadians)
    End Select
    'Ajouter la fl�che 
    unGraphique.Add(mFl�che)

  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe CourantCollection--------------------------
'=====================================================================================================
Public Class CourantCollection : Inherits CollectionBase
  Private mTrafic As Trafic

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unCourant As Courant) As Short
    Return Me.List.Add(unCourant)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Courant)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal unCourant As Courant)
    If Me.List.Contains(unCourant) Then
      Me.List.Remove(unCourant)
    End If

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unCourant As Courant)
    Me.List.Insert(Index, unCourant)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Courant
    Get
      Return CType(Me.List.Item(Index), Courant)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche du courant � partir de ses composants.
  Default Public ReadOnly Property Item(ByVal BrancheOrigine As Branche, ByVal BrancheDestination As Branche) As Courant
    Get
      Dim unCourant As Courant
      For Each unCourant In Me.List
        If unCourant.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine) Is BrancheOrigine Then
          If unCourant.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination) Is BrancheDestination Then
            Return unCourant
          End If
        End If
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unCourant As Courant) As Short
    Return Me.List.IndexOf(unCourant)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
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
    PhaseNum�roImpression
    ConflitSyst�matique
    ConflitAdmis
    ConflitNonAdmis
  End Enum

  Public Enum PlumeEnum
    BrancheAxe
    BrancheBordChauss�e
    BrancheVoie
    BrancheS�parVoie
    BrancheId
    PassageContour
    PassageZebras
    Ilot
    IlotImpression
    Trajectoire
    TrajectoireImpression
    TrajectoireFl�ches
    Travers�eContour
    Travers�eFl�che
    Travers�eFl�cheImpression
    LigneFeuV�hicule
    LigneFeuV�hiculeImpression
    SignalFeu
    SignalFeuImpression
    SignalFeuLigneRappel
    SignalFeuTexte
    PhaseImpression
    PhaseNum�roImpression
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

    mPlume(PlumeEnum.BrancheBordChauss�e) = unePlumeNoire
    mPlume(PlumeEnum.BrancheVoie) = unePlumeBleue
    mPlume(PlumeEnum.BrancheS�parVoie) = unePlumeBleue.Clone
    Dim EspacementTiret() As Single = {10, 10}
    With Plume(PlumeEnum.BrancheS�parVoie)
      .DashStyle = Drawing2D.DashStyle.Dash
      .DashPattern = EspacementTiret
    End With

    mPlume(PlumeEnum.Ilot) = unePlumeNoire
    mPlume(PlumeEnum.IlotImpression) = unePlumeNoire

    mPlume(PlumeEnum.PassageContour) = unePlumeNoire

    mPlume(PlumeEnum.Trajectoire) = unePlumeRouge.Clone
    mPlume(PlumeEnum.TrajectoireImpression) = unePlumeNoire
    mPlume(PlumeEnum.TrajectoireFl�ches) = unePlumeNoire

    mPlume(PlumeEnum.Travers�eContour) = New Pen(Color.Red, Width:=2)       '2 pixels de large
    With Plume(PlumeEnum.Travers�eContour)
      EspacementTiret(0) = 2
      EspacementTiret(1) = 2
      .DashPattern = EspacementTiret
      .DashStyle = Drawing2D.DashStyle.Dot
    End With

    mPlume(PlumeEnum.Travers�eFl�che) = New Pen(Color.Black, Width:=2)    '2 pixels de large
    mPlume(PlumeEnum.Travers�eFl�cheImpression) = New Pen(Color.Black, Width:=0.4)  ' 4/10� mm

    mPlume(PlumeEnum.LigneFeuV�hicule) = New Pen(Color.Blue, Width:=2)     '2 pixels de large
    mPlume(PlumeEnum.LigneFeuV�hiculeImpression) = New Pen(Color.Blue, Width:=1)   ' 10/10� mm

    mPlume(PlumeEnum.SignalFeu) = unePlumeBleue
    mPlume(PlumeEnum.SignalFeuImpression) = unePlumeBleue
    mPlume(PlumeEnum.SignalFeuLigneRappel) = New Pen(Color.Gray, width:=0.3)
    mPlume(PlumeEnum.PhaseImpression) = New Pen(Color.Green, 0.3) ' 3/10 mm
    mPlume(PlumeEnum.PhaseNum�roImpression) = unePlumeNoire
    mPlume(PlumeEnum.EllipseTraficImpression) = New Pen(Color.Black, Width:=0.4)  '4/10 mm

    mPlume(PlumeEnum.ConflitAdmissible) = unePlumeRouge.Clone

    mBrosse(BrosseEnum.BrancheNomRue) = New SolidBrush(Color.Black)
    mBrosse(BrosseEnum.SignalFeuID) = New SolidBrush(Color.Blue)
    mBrosse(BrosseEnum.SignalFeuIDImpression) = New SolidBrush(Color.Blue)
    mBrosse(BrosseEnum.PhaseNum�roImpression) = New SolidBrush(Color.Red)

    mBrosse(BrosseEnum.ConflitSyst�matique) = New SolidBrush(Color.Red)
    mBrosse(BrosseEnum.ConflitAdmis) = New SolidBrush(Color.LightGreen)
    mBrosse(BrosseEnum.ConflitNonAdmis) = New SolidBrush(Color.LightSalmon)

    mCouleur(PlumeEnum.BrancheAxe) = Color.Black
    mCouleur(PlumeEnum.BrancheBordChauss�e) = Color.Black
    mCouleur(PlumeEnum.BrancheVoie) = Color.Blue
    mCouleur(PlumeEnum.BrancheS�parVoie) = Color.Blue

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
Public Class Nord : Inherits M�tier
  Private mFl�che As Fleche
  Private mpRef As Point
  Private mRotation As Single
  Private milieuFl�che As Point
  Const lgFl�che As Short = 40
  Const hFl�che As Short = 4
  Const DemiLargeurBoite As Short = 8
  Private mAffich� As Boolean

  Public Sub New()
    'Par d�faut : en haut � gauche
    With mpRef
      .X = 30
      .Y = 30
    End With
    'Par d�faut : vers le haut (les angles sont dans le sens des aiguilles d'une montre)
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
      mAffich� = .Visible
    End With

  End Sub

  Public Sub Enregistrer(ByVal uneRowAffichage As DataSetDiagfeux.AffichageRow)
    Dim uneRowNord As DataSetDiagfeux.NordRow = ds.Nord.AddNordRow(mRotation, mAffich�, uneRowAffichage)
    With mpRef
      ds.pNord.AddpNordRow(.X, .Y, uneRowNord)
    End With

  End Sub

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    Dim pEcriture As Point
    Dim uneFl�che As Fleche

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      uneFl�che = New Fleche(lgFl�che, hFl�che, unePlume:=New Pen(Color.Black))

      'Positionner la fl�che
      mFl�che = uneFl�che.RotTrans(mpRef, mRotation + sngPI)
      mGraphique.Add(mFl�che)
      milieuFl�che = mFl�che.LigneR�f�rence.Milieu

      'Ecrire 'N' au bout de la fl�che
      pEcriture = PointPosition(mpRef, hFl�che + DemiLargeurBoite * 2, mRotation)
      mGraphique.Cr�erBoiteTexte(pEcriture, DemiLargeurBoite, "N", New SolidBrush(Color.Black))

    Else
      Dim pRef As New Point(mpRef.X / 4, mpRef.Y / 4)
      pRef = Translation(pRef, cndZoneGraphique.Location)
      uneFl�che = New Fleche(lgFl�che / 4, hFl�che / 4, unePlume:=New Pen(Color.Black))

      'Positionner la fl�che
      mFl�che = uneFl�che.RotTrans(pRef, mRotation + sngPI)
      mGraphique.Add(mFl�che)

      'Ecrire 'N' au bout de la fl�che
      pEcriture = PointPosition(pRef, hFl�che / 4 + DemiLargeurBoite / 2, mRotation)
      mGraphique.Cr�erBoiteTexte(pEcriture, DemiLargeurBoite / 4, "N", New SolidBrush(Color.Black))

    End If


    mGraphique.Invisible = Not mAffich�

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

  Public Function D�pla�able(ByVal pEnCours As Point) As Boolean
    Return Distance(pEnCours, milieuFl�che) < lgFl�che / 4
  End Function

  Public Function Orientable(ByVal pEnCours As Point) As Boolean
    Return Distance(pEnCours, PointPosition(mpRef, hFl�che, mRotation)) < RayS�lect
  End Function

  Public Property PtR�f�rence() As Point
    Get
      Return mpRef
    End Get
    Set(ByVal Value As Point)
      mpRef = Value
    End Set
  End Property

  Public ReadOnly Property LigneR�f�rence() As Ligne
    Get
      Return CType(mGraphique(0), Fleche).LigneR�f�rence
    End Get
  End Property

  Public Property Orientation() As Single
    Get
      Return mRotation
    End Get
    Set(ByVal Value As Single)
      mRotation = Value
      PtR�f�rence = PointPosition(LigneR�f�rence.pB, lgFl�che, mRotation)
    End Set
  End Property

  Public Property Affich�() As Boolean
    Get
      Return mAffich�
    End Get
    Set(ByVal Value As Boolean)
      mAffich� = Value
      If Not IsNothing(mGraphique) Then
        mGraphique.Invisible = Not Value
      End If
    End Set
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe SymEchelle --------------------------
'=====================================================================================================
Public Class SymEchelle : Inherits M�tier
  Private mpRef As Point
  Private mLigneR�f�rence As Ligne
  Private mContour As PolyArc
  Private lBarre As Short = 50
  Private mAffich� As Boolean

  Public Sub New()
    'Par d�faut : en haut � gauche
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
      mAffich� = .Visible
    End With

  End Sub

  Public Sub Enregistrer(ByVal uneRowAffichage As DataSetDiagfeux.AffichageRow)
    Dim uneRowEchelle As DataSetDiagfeux.SymEchelleRow = ds.SymEchelle.AddSymEchelleRow(mAffich�, uneRowAffichage)
    With mpRef
      ds.pSymEchelle.AddpSymEchelleRow(.X, .Y, uneRowEchelle)
    End With
  End Sub

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)
    mGraphique.ObjetM�tier = Me

    Dim i, nbSecteurs As Short
    Dim pOrigine As Point = mpRefR�el()
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

    'D�terminer si l'�chelle en cours permet d'afficher le symbole d'�chelle
    Select Case ToR�el(45)
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

    mLigneR�f�rence = New Ligne(New Point(pOrigine.X, pOrigine.Y + hBarre / 2), New Point(pOrigine.X + lBarre, pOrigine.Y + hBarre / 2))

    For i = 0 To nbSecteurs - 1
      'Autant de petits rectangles que de secteurs pr�d�termin�s
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

    Dim pEcriture As Point = PointPosition(mLigneR�f�rence.pB, DemiLargeurBoite, 0)
    mGraphique.Cr�erBoiteTexte(pEcriture, DemiLargeurBoite, CType(ToR�el(lBarre), Short) & "m", New SolidBrush(Color.Black), unefonte:=New Font("Arial", 8))

    mGraphique.Invisible = Not mAffich�

    uneCollection.Add(mGraphique)
  End Function

  Private Function mpRefR�el() As Point
    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      Return New Point(mpRef.X, yMaxPicture - mpRef.Y)
    Else
      Return New Point(cndZoneGraphique.X + mpRef.X / 4, cndZoneGraphique.Bottom - mpRef.Y / 4 - 5)
    End If
  End Function

  Public Property PtR�f�rence() As Point
    Get
      Return mpRef
    End Get
    Set(ByVal Value As Point)
      mpRef = Value
    End Set
  End Property

  Public ReadOnly Property LigneR�f�rence() As Ligne
    Get
      Return mLigneR�f�rence
    End Get
  End Property

  Public Function D�pla�able(ByVal pEnCours As Point) As Boolean
    Return mContour.Int�rieur(pEnCours)
  End Function

  Public Property Affich�() As Boolean
    Get
      Return mAffich�
    End Get
    Set(ByVal Value As Boolean)
      mAffich� = Value
      If Not IsNothing(mGraphique) Then
        mGraphique.Invisible = Not Value
      End If
    End Set
  End Property
End Class