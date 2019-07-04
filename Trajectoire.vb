'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Trajectoire.vb																						'
'						Classes																														'
'							Trajectoire																											'
'							TrajectoireCollection												          					'
'							TrajectoireV�hicules								        										'
'							Travers�ePi�tonne                   														'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe Trajectoire  --------------------------
'=====================================================================================================
Public MustInherit Class Trajectoire : Inherits M�tier
  'Ensemble des trajectoires v�hicules du carrefour
  Public MustOverride Sub Verrouiller()
  Public MustOverride Function Intersection(ByVal uneTrajectoire As TrajectoireV�hicules) As PointF
  Public MustOverride Sub R�initialiser(ByVal ConserverManuel As Boolean)

  'Ligne de feux commandant la trajectoire : voie(s) entrante(s) si v�hicules - Passage(s) pi�tons si Pi�tons
  Protected mLigneFeux As LigneFeux

  Private dctEnConflit As New Hashtable
  Private dctPtConflit As New Hashtable

  Public Enum TypeConflitEnum
    Aucun
    Admissible
    Admis
    NonAdmis
    Syst�matique
  End Enum

  Public ReadOnly Property EstPi�ton() As Boolean
    Get
      Return TypeOf Me Is Travers�ePi�tonne
    End Get
  End Property

  Public ReadOnly Property EstV�hicule() As Boolean
    Get
      Return TypeOf Me Is TrajectoireV�hicules
    End Get
  End Property

  Public Property LigneFeu() As LigneFeux
    Get
      Return mLigneFeux
    End Get
    Set(ByVal Value As LigneFeux)
      mLigneFeux = Value
    End Set
  End Property

  Public ReadOnly Property mVariante() As Variante
    Get
      If IsNothing(mLigneFeux) Then
        'En attendant mieux ?
        Return cndVariante
      Else
        Return mLigneFeux.mVariante
      End If
    End Get
  End Property

  '********************************************************************************************************************
  ' Position du point de conflit entre uneTrajectoire et Me
  '********************************************************************************************************************
  Public Property PtConflit(ByVal uneTrajectoire As Trajectoire) As PointF
    Get
      Return dctPtConflit(uneTrajectoire)
    End Get
    Set(ByVal Value As PointF)
      dctPtConflit(uneTrajectoire) = Value
      uneTrajectoire.dctEnConflit(Me) = Value
    End Set
  End Property

  '********************************************************************************************************************
  ' Enregistrer la Trajectoire dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overridable Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow

    Try
      Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = ds.Trajectoire.NewTrajectoireRow
      uneRowTrajectoire.SetParentRow(uneRowVariante)
      ds.Trajectoire.AddTrajectoireRow(uneRowTrajectoire)

      If Not IsNothing(mLigneFeux) Then
        'Pour une trajectoire v�hicules, la ligne de feux correspondante n'est peut-�tre pas encore cr��e
        uneRowTrajectoire.IDLigneDeFeux = mLigneFeux.ID
      End If

      Return uneRowTrajectoire

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Enregistrement de la trajectoire")
    End Try
  End Function

  Public Sub New()

  End Sub

  Public Sub New(ByVal uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow)

  End Sub

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe TrajectoireCollection --------------------------
'=====================================================================================================
Public Class TrajectoireCollection : Inherits CollectionBase

  Private mVariante As Variante
  Private mAntagonismes As AntagonismeCollection

  ' Cr�er une instance la collection
  '  utilis�e pour la collection des trajectoires command�es par une ligne de feux
  Public Sub New()
    MyBase.New()
  End Sub

  ' Cr�er une instance la collection
  ' Utilis�e pour la collection des trajectoires de la variante
  Public Sub New(ByVal uneVariante As Variante)

    MyBase.New()
    mVariante = uneVariante
    mAntagonismes = New AntagonismeCollection(uneVariante.mLignesFeux)

  End Sub

  Public Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As Boolean
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow
    Dim desLignesPi�tons As DataSetDiagfeux.Pi�tonsRow()
    Dim desLignesV�hicules As DataSetDiagfeux.V�hiculesRow()

    Try
      'Effacer tous les enregistrements relatifs aux trajectoires de cette variante
      Do Until uneRowVariante.GetChildRows("Variante_Trajectoire").Length = 0
        uneRowTrajectoire = uneRowVariante.GetChildRows("Variante_Trajectoire")(0)
        With uneRowTrajectoire
          desLignesPi�tons = .GetChildRows("Trajectoire_Pi�tons")
          desLignesV�hicules = .GetChildRows("Trajectoire_V�hicules")
          'Il s'agit soit d'une trajectoire v�hicules soit d'une travers�e pi�tonn
          If desLignesPi�tons.Length = 1 Then
            'Travers�e pi�tonne
            ds.Pi�tons.RemovePi�tonsRow(desLignesPi�tons(0))
          Else
            ds.V�hicules.RemoveV�hiculesRow(desLignesV�hicules(0))
          End If
          ds.Trajectoire.RemoveTrajectoireRow(uneRowTrajectoire)
        End With
      Loop

      ' Trajectoires
      Dim uneTrajectoire As Trajectoire
      For Each uneTrajectoire In Me
        uneTrajectoire.Enregistrer(uneRowVariante)
      Next

      If mVariante.ModeGraphique Then
        'Antagonismes
        Dim unAntagonisme As Antagonisme
        For Each unAntagonisme In mAntagonismes
          unAntagonisme.Enregistrer(uneRowVariante, Me)
        Next

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
      Enregistrer = True
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Enregistrement des trajectoires")
      Enregistrer = True
    End Try

  End Function

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal uneTrajectoire As Trajectoire) As Short
    Return Me.List.Add(uneTrajectoire)
  End Function

  Public Function Add(ByVal uneTrajectoire As Trajectoire, ByVal uneCollection As Graphiques) As Short
    'Cr�er sa repr�sentation graphique
    uneTrajectoire.Cr�erGraphique(uneCollection)
    Return Me.List.Add(uneTrajectoire)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Trajectoire)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal uneTrajectoire As Trajectoire)
    If Me.List.Contains(uneTrajectoire) Then
      Me.List.Remove(uneTrajectoire)
    End If

  End Sub

  Public Sub Remove(ByVal uneTrajectoire As Trajectoire, ByVal uneCollection As Graphiques)

    If TypeOf uneTrajectoire Is Travers�ePi�tonne Then
      Dim uneTravers�e As Travers�ePi�tonne = uneTrajectoire
      uneCollection.Remove(uneTravers�e.mGraphique)
    End If

    Remove(uneTrajectoire)

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneTrajectoire As Trajectoire)
    Me.List.Insert(Index, uneTrajectoire)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Trajectoire
    Get
      Return CType(Me.List.Item(Index), Trajectoire)
    End Get
  End Property

  Public Function IndexOf(ByVal uneTrajectoire As Trajectoire) As Short
    Return Me.List.IndexOf(uneTrajectoire)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Function Contains(ByVal uneTrajectoire As Trajectoire) As Boolean
    Return Me.List.Contains(uneTrajectoire)
  End Function

  Public Function Existe(ByVal VoieOrigine As Voie, ByVal VoieDestination As Voie) As Boolean
    Dim uneTrajectoire As Trajectoire
    Dim uneTrajectoireV�hicules As TrajectoireV�hicules

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule Then
        uneTrajectoireV�hicules = uneTrajectoire
        If VoieOrigine Is uneTrajectoireV�hicules.Voie(TrajectoireV�hicules.OrigineDestEnum.Origine) And VoieDestination Is uneTrajectoireV�hicules.Voie(TrajectoireV�hicules.OrigineDestEnum.Destination) Then
          Return True
        End If
      End If
    Next

  End Function

  Public Sub Epurer()
    Dim uneTrajectoire As Trajectoire
    Dim Continuer As Boolean

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule Then
        Remove(uneTrajectoire)
        Continuer = True
        Exit For
      End If
    Next
    If Continuer Then Epurer()
  End Sub

  Public Sub R�initialiser(ByVal ConserverManuel As Boolean)
    Dim uneTrajectoire As Trajectoire
    For Each uneTrajectoire In Me
      uneTrajectoire.R�initialiser(ConserverManuel)
    Next
  End Sub

  Public Function ContientManuelles() As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule AndAlso CType(uneTrajectoire, TrajectoireV�hicules).Manuel Then
        Return True
      End If
    Next
  End Function

  Public Function ContientV�hicules() As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule Then
        Return True
      End If
    Next

  End Function

#Region "Courants"
  '************************************************************************************
  ' D�terminer les courants de circulation de chaque voie command�e par une ligne de feux
  ' desVoies : Voies entrantes command�e par une ligne de feux v�hicules donn�e
  '************************************************************************************
  Public Sub D�terminerCourants(ByVal desVoies As VoieCollection)
    Dim uneTrajectoire As Trajectoire
    Dim uneVoie As Voie

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule Then
        With CType(uneTrajectoire, TrajectoireV�hicules)
          uneVoie = .Voie(TrajectoireV�hicules.OrigineDestEnum.Origine)
          If desVoies.Contains(uneVoie) Then
            uneVoie.mCourants.Add(.Courant)
          End If
        End With
      End If
    Next

  End Sub


#End Region
#Region "Conflits-Antagonismes"
  Public ReadOnly Property Antagonismes() As AntagonismeCollection
    Get
      Return mAntagonismes
    End Get
  End Property

  '*************************************************************************************
  'Cr�er les antagonismes suite � la lecture d'un projet existant
  '*************************************************************************************
  Public Sub Cr�erAntagonismes(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim unAntagonisme As Antagonisme
    Dim i As Short
    Dim Ti, Tj As Trajectoire
    Dim p As PointF

    Try
      With uneRowVariante
        For i = 0 To .GetAntagonismeRows.Length - 1
          Dim uneRowAntagonisme As DataSetDiagfeux.AntagonismeRow = .GetAntagonismeRows(i)
          With uneRowAntagonisme
            Ti = Item(.Trajectoire1)
            Tj = Item(.Trajectoire2)
            With .GetpAntagoRows(0)
              p = New PointF(.X, .Y)
            End With
            unAntagonisme = New Antagonisme(Ti, Tj, p, .TypeConflit)
            mAntagonismes.Add(unAntagonisme)
            If .TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
              mVariante.mLignesFeux.EstIncompatible(Ti.LigneFeu, Tj.LigneFeu) = True
            End If
          End With
        Next
      End With

      mAntagonismes.AntagoFiliation()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Trajectoire.Cr�erAntagonismess")

    End Try
  End Sub

  '********************************************************************************************************************
  ' D�terminer les points de conflit entre tous les couples de trajectoires
  ' Cette fonction est appel�e une seule fois : lors du verrouillage des lignes de feux
  '********************************************************************************************************************
  Public Sub D�terminerConflits()
    Dim Ti, Tj As Trajectoire
    Dim Tvi, Tvj As TrajectoireV�hicules
    Dim uneTravers�e As Travers�ePi�tonne
    Dim TypeConflit As Trajectoire.TypeConflitEnum
    Dim unAntagonisme As Antagonisme
    Dim Courant1, Courant2 As Courant
    Dim desBranches As BrancheCollection = mVariante.mBranches
    Dim desLignesFeux As LigneFeuxCollection = mVariante.mLignesFeux

    Dim pa As PointF

    Try

      mAntagonismes.Clear()

        For Each Ti In Me
          If Ti.EstV�hicule Then
            Tvi = Ti
            Courant1 = Tvi.Courant

            For Each Tj In Me
              If Tj.EstV�hicule Then
                ' Etudier le Conflit entre 2 trajectoires v�hicules
                If IndexOf(Tj) > IndexOf(Ti) Then
                  Tvj = Tj
                  Courant2 = Tvj.Courant
                  If Not Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine) Is Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine) Then
                    TypeConflit = Trajectoire.TypeConflitEnum.Aucun
                    Select Case Courant1.NatureCourant
                      Case TrajectoireV�hicules.NatureCourantEnum.TD
                        ' Incompatibles si les courants se coupent
                        TypeConflit = QuelConflit(Courant1, Courant2, desBranches)
                        If TypeConflit = Trajectoire.TypeConflitEnum.Aucun Then
                          Select Case Tvj.NatureCourant
                            'Conflit possible TD/TAD si m�me branche destination
                          Case TrajectoireV�hicules.NatureCourantEnum.TAD
                              If Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination) Is Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination) Then
                                'Incompatibilit� non syst�matique
                                TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                              End If
                          End Select
                        End If
                      Case TrajectoireV�hicules.NatureCourantEnum.TAG
                        'Le TAG doit �tre le 2�me param�tre
                        TypeConflit = QuelConflit(Courant2, Courant1, desBranches)
                      Case TrajectoireV�hicules.NatureCourantEnum.TAD
                        If Courant2.NatureCourant <> TrajectoireV�hicules.NatureCourantEnum.TAD Then
                          If Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination) Is Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination) Then
                            'Incompatibilit� non syst�matique
                            TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                          Else
                            TypeConflit = QuelConflit(Courant2, Courant1, desBranches)
                          End If
                        End If
                        'TAD TAG entre eux : jamais de probl�me
                    End Select

                    If TypeConflit <> Trajectoire.TypeConflitEnum.Aucun Then
                      Select Case Tvi.NatureCourant
                        Case TrajectoireV�hicules.NatureCourantEnum.TD
                          pa = Cr�erAntagonisme(Tvi, Tj, TypeConflit)
                        Case TrajectoireV�hicules.NatureCourantEnum.TAG
                          If Tvj.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TD Then
                            ' En cas de conflit TAG/TD on positionne le courant TD en 1er
                            pa = Cr�erAntagonisme(Tvj, Ti, TypeConflit)
                          Else
                            pa = Cr�erAntagonisme(Tvi, Tj, TypeConflit)
                          End If
                        Case TrajectoireV�hicules.NatureCourantEnum.TAD
                          ' On positionne le courant TAD en 2�me
                          pa = Cr�erAntagonisme(Tvj, Ti, TypeConflit)
                      End Select
                    End If

                  End If    ' TVi et TVj d'origines diff�rentes
                End If      ' IndexOf(Tj) > IndexOf(Ti)

              Else
                ' Trajectoire pi�ton
                uneTravers�e = Tj
                Dim unPassage As PassagePi�ton
                Dim uneVoie As Voie
                TypeConflit = Trajectoire.TypeConflitEnum.Aucun
                For Each unPassage In uneTravers�e.mPassages
                  For Each uneVoie In unPassage.Voies
                    If uneVoie Is Tvi.Voie(TrajectoireV�hicules.OrigineDestEnum.Origine) Then
                      TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique
                    ElseIf uneVoie Is Tvi.Voie(TrajectoireV�hicules.OrigineDestEnum.Destination) Then
                      Select Case Tvi.NatureCourant
                        Case TrajectoireV�hicules.NatureCourantEnum.TD
                          TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique
                        Case Else
                          TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                      End Select
                    End If
                  Next
                Next

                If TypeConflit <> Trajectoire.TypeConflitEnum.Aucun Then
                  pa = Cr�erAntagonisme(Tvi, Tj, TypeConflit)
                End If

              End If ' Tj est v�hicule

            Next  ' Tj
          End If  ' Ti est v�hicule
        Next      ' Ti

        For Each unAntagonisme In mAntagonismes
          With unAntagonisme
          'Si les lignes de feux sont strictement incompatibles(d�termin� par Cr�erAntagonisme), l'antagonisme est syst�matique
            If desLignesFeux.EstIncompatible(.LigneFeu(Antagonisme.PositionEnum.Premier), .LigneFeu(Antagonisme.PositionEnum.Dernier)) Then
            unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique
            End If
            ' Pour DIAGFEUX, ces objets graphiques ne seront jamais directement dssin�s ni utilis�s, mais il est pr�f�rable de les conserver
            'unAntagonisme.Cr�erGraphique(uneCollection)
          End With
        Next

        mAntagonismes.AntagoFiliation()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Trajectoire.D�terminerConflits")
    End Try

  End Sub

  '********************************************************************************************************************
  ' D�terminer la position du point de conflit et cr�er l'antagonisme 
  ' Activer l'incompatibilit� des lignes de feux correspondantes (si c'est un syst�matique)
  '********************************************************************************************************************
  Private Function Cr�erAntagonisme(ByVal Ti As TrajectoireV�hicules, ByVal Tj As Trajectoire, ByVal TypeConflit As Trajectoire.TypeConflitEnum) As PointF
    Dim p As PointF
    Dim unAntagonisme As Antagonisme

    Try
      p = Tj.Intersection(Ti)
      If Not p.IsEmpty Then
        p = PointR�el(p)
        Ti.PtConflit(Tj) = p
        unAntagonisme = New Antagonisme(Ti, Tj, p, TypeConflit)
        'Ajouter l'antagonisme � la collection
        'Cette instruction permet aussi de regrouper les antagonismes qui sont li�s car correspondant aux m�mes courants de circulation
        mAntagonismes.Add(unAntagonisme)
        If TypeConflit = Trajectoire.TypeConflitEnum.Syst�matique Then
          mVariante.mLignesFeux.EstIncompatible(Ti.LigneFeu, Tj.LigneFeu) = True
        End If

      End If

      Return p

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Cr�erAntagonisme")
    End Try

  End Function

  Private Function QuelConflit(ByVal Courant1 As Courant, ByVal Courant2 As Courant, ByVal mesBranches As BrancheCollection) As Trajectoire.TypeConflitEnum
    Dim BO1, BO2, BD1, BD2, boucleBranche As Branche
    Dim TrajCons�cutives, TrajConcurrentes As Boolean
    Dim Intersection As Boolean

    ' D�termination des branches origines et Destination de chaque courant
    BO1 = Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    BD1 = Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination)
    BO2 = Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine)
    BD2 = Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination)

    boucleBranche = BO1
    ' Faire une boucle sur les branches, en partant de la branche origine du 1er courant jusq'� trouver sa branche destination
    Do Until boucleBranche Is BD1
      boucleBranche = mesBranches.Suivante(boucleBranche)
      If boucleBranche Is BO2 Then
        TrajCons�cutives = True
      ElseIf boucleBranche Is BD2 Then
        TrajConcurrentes = True
        boucleBranche = BD1
      ElseIf TrajCons�cutives Then
        'Arr�ter la boucle puisu'on a rattrap� la branche origine du 2�me courant
        boucleBranche = BD1
      End If
    Loop

    If TrajConcurrentes Then
      Intersection = True

    ElseIf TrajCons�cutives Then
      If Not boucleBranche Is BO2 Then Intersection = True
    End If

    If Intersection Then
      If Courant2.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
        'Le conflit TAGTD n'est pas syst�matique
        If Courant1.NatureCourant = TrajectoireV�hicules.NatureCourantEnum.TAG Then
          ' Conflit TAG/TAG : non syst�matique
          QuelConflit = Trajectoire.TypeConflitEnum.Admissible
        Else
          QuelConflit = ConflitTagTd(Courant1, Courant2, mesBranches)
        End If
      Else
        QuelConflit = Trajectoire.TypeConflitEnum.Syst�matique
      End If
    End If

  End Function

  Private Function ConflitTagTd(ByVal Courant1 As Courant, ByVal Courant2 As Courant, ByVal mesBranches As BrancheCollection) As Trajectoire.TypeConflitEnum
    Dim Index(2) As Short

    With mesBranches
      Index(0) = .IndexOf(Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination))
      Index(1) = .IndexOf(Courant1.Branche(TrajectoireV�hicules.OrigineDestEnum.Destination))
      Index(2) = .IndexOf(Courant2.Branche(TrajectoireV�hicules.OrigineDestEnum.Origine))
      If Index(1) < Index(0) Then Index(1) += .Count
      If Index(2) < Index(0) Then Index(2) += .Count
      If Index(1) > Index(2) Then
        ' Ce n'est pas vraiment un confllit TAGTD : on revient au conflit TD/TD classique
        Return Trajectoire.TypeConflitEnum.Syst�matique
      Else
        Return Trajectoire.TypeConflitEnum.Admissible
      End If
    End With

  End Function

  '*************************************************************************
  'Cr�er les objets graphiques trajectoires
  'Cr�er aussi s'il y a lieu ceux des antagonismes(points de conflits)
  '*************************************************************************
  Public Function Cr�erGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim uneTrajectoire As Trajectoire

    'Trajectoires
    For Each uneTrajectoire In Me
      uneTrajectoire.Cr�erGraphique(uneCollection)
    Next

  End Function

#End Region

#Region "Verrouillage"
  '*************************************************************************************************
  '*************************************************************************************************
  Public Sub Verrouiller()
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      uneTrajectoire.Verrouiller()
    Next

  End Sub

#End Region

  '*************************************************************************************
  ' Recherche si une voie est origine d'au moins une trajectoire v�hicules
  '*************************************************************************************
  Public Function ContientOrigine(ByVal uneVoie As Voie) As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstV�hicule Then
        If CType(uneTrajectoire, TrajectoireV�hicules).Voie(TrajectoireV�hicules.OrigineDestEnum.Origine) Is uneVoie Then Return True
      End If
    Next

  End Function

  Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
    If TypeOf value Is TrajectoireV�hicules Then
      Dim uneLigneFeux As LigneFeuV�hicules = CType(value, TrajectoireV�hicules).LigneFeu
      If Not IsNothing(uneLigneFeux) Then
        uneLigneFeux.D�terminerNatureCourants(Me)
      End If
    End If
  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe TrajectoireV�hicules --------------------------
'=====================================================================================================

Public Class TrajectoireV�hicules : Inherits Trajectoire
  'Trajectoire v�hicule

  Public Enum TypeCourantEnum
    TypeCourantMixte
    TypeCourantTC   ' Transports en commun
    TypeCourant2R   ' 2 roues
  End Enum


  Public Enum NatureCourantEnum
    Aucun = -1
    TAD   ' Tourne � droite
    TD   ' Tout droit
    TAG   ' Tourne � gauche

  End Enum

  Public Enum OrigineDestEnum
    Origine
    Destination
  End Enum


  'Le type de courant peut �tre : TC(transports en commun),deux-roues ou mixte (tous v�hicules)
  Private mTypeCourant As TrajectoireV�hicules.TypeCourantEnum

  '##ModelId=403312F900CB
  Private mCourant As Courant

  '##ModelId=403C7FD00222
  Private mVoie(1) As Voie
  ' Points repr�sentant la trajectoire en coordonn�es r�elles
  Private mPoints(-1) As PointF
  Private mPointsManuel(-1) As PointF
  Private mDessinR�el As PolyArc
  Private mFl�ches As PolyArc
  Private mPtsAcc�s(1) As PointF
  Public LigneAcc�s As Ligne
  Public PolyManuel As PolyArc

  Public Sub New(ByVal VoieOrigine As Voie, ByVal VoieDestination As Voie)
    MyBase.New()
    mVoie(OrigineDestEnum.Origine) = VoieOrigine
    mVoie(OrigineDestEnum.Destination) = VoieDestination
  End Sub

  Public Sub New(ByVal uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow)
    MyBase.New(uneRowTrajectoire)

    Dim numBranche, IndexVoie As Short
    Dim uneBranche As Branche
    Dim MaxVoies As Short = DiagFeux.Voie.MaxVoies

    With uneRowTrajectoire.GetV�hiculesRows(0)
      numBranche = .VoieOrigine \ MaxVoies
      IndexVoie = .VoieOrigine Mod MaxVoies
      uneBranche = cndVariante.mBranches(numBranche)
      mVoie(OrigineDestEnum.Origine) = uneBranche.Voies(IndexVoie)

      numBranche = .VoieDestination \ MaxVoies
      IndexVoie = .VoieDestination Mod MaxVoies
      uneBranche = cndVariante.mBranches(numBranche)
      mVoie(OrigineDestEnum.Destination) = uneBranche.Voies(IndexVoie)

      Courant = cndVariante.mCourants(mVoie(OrigineDestEnum.Origine).mBranche, mVoie(OrigineDestEnum.Destination).mBranche)
      'Les instructions qui suivent permettent en fait d'affecter les propri�t�s TypeCourant(de la trajectoire) e NatureCourant(du courant)
      LibelTypeCourant = .TypeCourant()
      LibelNatureCourant = .NatureCourant()
      'Affecte en fait le coefficient de g�ne au courant de la trajectoire
      CoefG�ne = .CoefGene
      Dim lg As Short = .GetPointManuelRows.Length
      If lg > 0 Then

        ReDim mPointsManuel(lg - 1)
        Dim i As Short
        For i = 0 To lg - 1
          mPointsManuel(i).X = .GetPointManuelRows(i).X
          mPointsManuel(i).Y = .GetPointManuelRows(i).Y
        Next
      End If
    End With

  End Sub

  Public Property Courant() As Courant
    Get
      Return mCourant
    End Get
    Set(ByVal Value As Courant)
      mCourant = Value
    End Set
  End Property

  Public Property TypeCourant() As TypeCourantEnum
    Get
      Return mTypeCourant
    End Get
    Set(ByVal Value As TypeCourantEnum)
      mTypeCourant = Value
    End Set
  End Property

  Public Property NatureCourant() As NatureCourantEnum
    Get
      Return mCourant.NatureCourant
    End Get
    Set(ByVal Value As NatureCourantEnum)
      mCourant.NatureCourant = Value
    End Set
  End Property

  Public Property CoefG�ne() As Single
    Get
      Return mCourant.CoefG�ne
    End Get
    Set(ByVal Value As Single)
      mCourant.CoefG�ne = Value
    End Set
  End Property

  '********************************************************************************************************************
  ' Enregistrer la trajectoire v�hicules dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow
    'Enregistrer d'abord la trajectoire
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = MyBase.Enregistrer(uneRowVariante)
    'Enregistrer les propri�t�s sp�cifiques aux v�hicules
    Dim uneRowV�hicules As DataSetDiagfeux.V�hiculesRow = ds.V�hicules.NewV�hiculesRow

    With uneRowV�hicules
      .VoieOrigine = Voie(OrigineDestEnum.Origine).ID
      .VoieDestination = Voie(OrigineDestEnum.Destination).ID
      .TypeCourant = LibelTypeCourant()
      .NatureCourant = LibelNatureCourant()
      .CoefGene = CoefG�ne
      .SetParentRow(uneRowTrajectoire)
    End With

    ds.V�hicules.AddV�hiculesRow(uneRowV�hicules)

    If Manuel Then
      Dim i As Short
      For i = 0 To mPointsManuel.Length - 1
        ds.PointManuel.AddPointManuelRow(mPointsManuel(i).X, mPointsManuel(i).Y, uneRowV�hicules)
      Next
    End If

  End Function

  Private Property LibelTypeCourant() As String
    Get
      Select Case mTypeCourant
        Case TypeCourantEnum.TypeCourantMixte
          Return "MIXTE"
        Case TypeCourantEnum.TypeCourant2R
          Return "2R"
        Case TypeCourantEnum.TypeCourantTC
          Return "TC"
      End Select

    End Get
    Set(ByVal Value As String)
      Select Case Value
        Case TypeCourantEnum.TypeCourantMixte
          mTypeCourant = "MIXTE"
        Case "2R"
          mTypeCourant = TypeCourantEnum.TypeCourant2R
        Case "TC"
          mTypeCourant = TypeCourantEnum.TypeCourantTC
      End Select

    End Set
  End Property

  Private Property LibelNatureCourant() As String
    Get
      Select Case mCourant.NatureCourant
        Case NatureCourantEnum.TAD
          Return "TAD"
        Case NatureCourantEnum.TD
          Return "TD"
        Case NatureCourantEnum.TAG
          Return "TAG"
      End Select

    End Get

    Set(ByVal Value As String)
      Select Case Value
        Case "TAD"
          mCourant.NatureCourant = NatureCourantEnum.TAD
        Case "TD"
          mCourant.NatureCourant = NatureCourantEnum.TD
        Case "TAG"
          mCourant.NatureCourant = NatureCourantEnum.TAG
      End Select

    End Set
  End Property

  Public ReadOnly Property mBranche(ByVal Index As OrigineDestEnum) As Branche
    Get
      Return mCourant.Branche(Index)
    End Get
  End Property

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim unePlume As Pen

    Select Case cndFlagImpression
      Case dlgImpressions.ImpressionEnum.Aucun
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Trajectoire).Clone
      Case dlgImpressions.ImpressionEnum.DiagrammePhases, dlgImpressions.ImpressionEnum.Matrice
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.TrajectoireImpression).Clone
      Case Else
        Return Nothing
    End Select

    Dim Poign�esACr�er As Boolean

    Poign�esACr�er = True

    Try

      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)
      mGraphique.ObjetM�tier = Me

      If mPoints.Length = 0 Then

        If Manuel Then
          AffecterPointsV12()
        Else
          InitGraphique(uneCollection)
        End If
      End If

      If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
        Cr�erFl�ches()
        uneCollection.Add(mFl�ches)
        mFl�ches.Invisible = Not mVariante.SensTrajectoires
      End If

      Dim l1, l2 As Ligne
      Dim unArc As Arc

      Dim pDessin(mPoints.Length - 1) As PointF
      Dim i As Short

      For i = 0 To mPoints.Length - 1
        pDessin(i) = PointDessinF(mPoints(i))
      Next

      For i = 1 To pDessin.Length - 2
        If i = 1 Then
          l1 = New Ligne(pDessin(i), pDessin(i - 1), unePlume)
          mGraphique.Add(l1, Poign�esACr�er:=Poign�esACr�er)
        Else
          l1 = l2.Invers�e
          mGraphique.Add(l1, Poign�esACr�er:=Poign�esACr�er)
        End If
        l2 = New Ligne(pDessin(i), pDessin(i + 1), unePlume)
        unArc = Cr�erRaccord(l1, l2, unePlume:=unePlume)
        mGraphique.Add(unArc)
      Next
      mGraphique.Add(l2, Poign�esACr�er:=Poign�esACr�er)

      LigneAcc�s = New Ligne(PointDessin(PtsAcc�s(OrigineDestEnum.Origine)), PointDessin(PtsAcc�s(OrigineDestEnum.Destination)))

      If Manuel Then
        'PolyManuel comprend les points d'acc�s et les points manuels interm�diaires
        For i = 1 To pDessin.Length - 2
          pDessin(i - 1) = pDessin(i)
        Next
        ReDim Preserve pDessin(pDessin.Length - 3)
        PolyManuel = New PolyArc(pDessin, Clore:=False)
        mGraphique.Add(PolyManuel)

      Else
        ClearGraphique(Nothing, PolyManuel)
        ' On ne l'ajoute pas au graphique afin qu'il n'intervienne pas dans la recherche des conflits
        '      mGraphique.Add(LigneAcc�s)
      End If

      uneCollection.Add(mGraphique)

      Return mGraphique

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.Cr�erGraphique")
    End Try

  End Function

  Public Function MouvementPossible(ByVal pEnCours As Point, ByRef numPoint As Short) As frmCarrefour.CommandeGraphique
    Dim uneLigne As Ligne = LigneAcc�s
    Dim PointProche As Point
    Dim uneCommande As frmCarrefour.CommandeGraphique
    Dim i As Short
    Dim distMin, distMinPr�c As Single

    distMinPr�c = 500
    distMin = 500

    If Manuel Then
      With PolyManuel
        For i = 0 To .Points.Length - 1
          distMin = Math.Min(Distance(pEnCours, CvPoint(.Points(i))), distMinPr�c)
          If distMin < distMinPr�c Then
            distMinPr�c = distMin
            numPoint = i
          End If
        Next

        Select Case numPoint
          Case 0
            uneCommande = frmCarrefour.CommandeGraphique.EditerOrigineTrajectoire
          Case .Points.Length - 1
            uneCommande = frmCarrefour.CommandeGraphique.EditerDestinationTrajectoire
          Case Else
            uneCommande = frmCarrefour.CommandeGraphique.EditerPointTrajectoire
        End Select
        PointProche = CvPoint(.Points(numPoint))
      End With

    Else
      If Distance(pEnCours, uneLigne.pA) < Distance(pEnCours, uneLigne.pB) Then
        PointProche = uneLigne.pA
        uneCommande = frmCarrefour.CommandeGraphique.EditerOrigineTrajectoire
      Else
        PointProche = uneLigne.pB
        uneCommande = frmCarrefour.CommandeGraphique.EditerDestinationTrajectoire
      End If
    End If

    If Distance(PointProche, pEnCours) >= RayS�lect Then
      uneCommande = frmCarrefour.CommandeGraphique.AucuneCommande
    End If

    Return uneCommande

  End Function

  Public Function AxeVoie(ByVal Cot� As TrajectoireV�hicules.OrigineDestEnum) As Ligne

    Return mVoie(Cot�).Axe

  End Function

  Public Function Extr�mit�(ByVal Cot� As TrajectoireV�hicules.OrigineDestEnum) As PointF
    Return AxeVoie(Cot�).pAF
  End Function

  Public ReadOnly Property Voie(ByVal Index As OrigineDestEnum) As Voie
    Get
      Return mVoie(Index)
    End Get
  End Property

  Private Sub InitGraphique(ByVal uneCollection As Graphiques)
    Dim BrancheOrigine, BrancheDestination As Branche
    BrancheOrigine = mBranche(OrigineDestEnum.Origine)
    BrancheDestination = mBranche(OrigineDestEnum.Destination)
    Dim p1 As Point = BrancheOrigine.LigneDeSym�trie.pA

    Dim LigneOrigine As Ligne = AxeVoie(OrigineDestEnum.Origine).Clone
    Dim LigneDestination As Ligne = AxeVoie(OrigineDestEnum.Destination).Clone
    LigneOrigine.pA = PointDessin(PtsAcc�s(OrigineDestEnum.Origine))
    LigneDestination.pA = PointDessin(PtsAcc�s(OrigineDestEnum.Destination))

    Dim LigneInfranchissableOrigine As Ligne = mVoie(OrigineDestEnum.Origine).Bordure(Branche.Lat�ralit�.Gauche).Clone
    Dim LigneInfranchissableDestination As Ligne = mVoie(OrigineDestEnum.Destination).Bordure(Branche.Lat�ralit�.Gauche).Clone
    Dim LigneInfranchissableOrigine2 As Ligne = mVoie(OrigineDestEnum.Origine).Bordure(Branche.Lat�ralit�.Droite).Clone
    Dim LigneInfranchissableDestination2 As Ligne = mVoie(OrigineDestEnum.Destination).Bordure(Branche.Lat�ralit�.Droite).Clone
    With LigneInfranchissableOrigine
      .pAF = PointPosition(.pAF, BrancheOrigine.LargeurVoies / 2 * Echelle, BrancheOrigine.AngleEnRadians + Math.PI)
      '      mGraphique.Add(LigneInfranchissableOrigine)
      LigneInfranchissableOrigine.Plume = New Pen(Color.Red)
    End With
    With LigneInfranchissableOrigine2
      .pAF = PointPosition(.pAF, BrancheOrigine.LargeurVoies / 2 * Echelle, BrancheOrigine.AngleEnRadians + Math.PI)
      '      mGraphique.Add(LigneInfranchissableOrigine)
      LigneInfranchissableOrigine.Plume = New Pen(Color.Red)
    End With
    With LigneInfranchissableDestination
      .pAF = PointPosition(.pAF, BrancheDestination.LargeurVoies / 2 * Echelle, BrancheDestination.AngleEnRadians + Math.PI)
      '      mGraphique.Add(LigneInfranchissableDestination)
      LigneInfranchissableDestination.Plume = New Pen(Color.Red)
    End With
    With LigneInfranchissableDestination2
      .pAF = PointPosition(.pAF, BrancheDestination.LargeurVoies / 2 * Echelle, BrancheDestination.AngleEnRadians + Math.PI)
      '      mGraphique.Add(LigneInfranchissableDestination)
      LigneInfranchissableDestination.Plume = New Pen(Color.Red)
    End With

    Dim p As PointF = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indiff�rent)
    Dim LigneRaccord As Ligne
    Dim AvecRaccord As Boolean
    Dim RaccordDestination, RaccordOrigine As Ligne
    Dim unAngle As Single

    'D�finir un segment raccordant les extr�mit�s ad�quate de la branche origine et de la branche destination
    Dim RaccordExtr�mit�s As Ligne
    ' D�calage � effectuer sur le raccord pour �tre sur d'avoir une distance suffisante au droit de la sortie
    Dim D�calage As Single

    RaccordDestination = New Ligne(mVariante.mBranches.Pr�c�dente(BrancheDestination).Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche), BrancheDestination.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite))
    D�calage = 1.0 * Echelle
    unAngle = AngleForm�(RaccordDestination) - Math.PI / 2
    RaccordDestination = RaccordDestination.Translation(New Vecteur(D�calage, unAngle))
    RaccordDestination.Plume = New Pen(Color.Blue)
    '    mGraphique.Add(RaccordDestination)
    RaccordOrigine = New Ligne(BrancheOrigine.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche), mVariante.mBranches.Suivante(BrancheOrigine).Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite))
    D�calage = 1.0 * Echelle
    unAngle = AngleForm�(RaccordOrigine) - Math.PI / 2
    RaccordOrigine = RaccordOrigine.Translation(New Vecteur(D�calage, unAngle))
    RaccordOrigine.Plume = New Pen(Color.Blue)
    '    mGraphique.Add(RaccordOrigine)

    unAngle = CvAngleDegr�s(BrancheOrigine.AngleEnRadians - BrancheDestination.AngleEnRadians, InverserSens:=False)

    With BrancheDestination
      If unAngle < 180 Then
        RaccordExtr�mit�s = New Ligne(BrancheOrigine.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche), .Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite))
        D�calage = (.Voies.IndexOf(Voie(OrigineDestEnum.Destination)) + 0.5) * .LargeurVoies * Echelle
        unAngle = AngleForm�(RaccordExtr�mit�s) - Math.PI / 2
      Else
        RaccordExtr�mit�s = New Ligne(BrancheOrigine.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Droite), BrancheDestination.Extr�mit�BordChauss�e(Branche.Lat�ralit�.Gauche))
        D�calage = (.Voies.Count - .Voies.IndexOf(Voie(OrigineDestEnum.Destination)) - 0.5) * .LargeurVoies * Echelle
        unAngle = AngleForm�(RaccordExtr�mit�s) + Math.PI / 2
      End If

      ' D�caler le raccord vers l'int�rieur du carrefour d'une distance �gale � la largeur de sortie
      RaccordExtr�mit�s = RaccordExtr�mit�s.Translation(New Vecteur(D�calage, unAngle))
      RaccordExtr�mit�s.Plume = New Pen(Color.Blue, 2)
      '     mGraphique.Add(RaccordExtr�mit�s)
    End With

    Dim Ligne11 As New Ligne(LigneOrigine.pBF, p)
    Dim Ligne22 As New Ligne(LigneDestination.pBF, p)
    If Not Ligne11.PtSurSegment(LigneOrigine.pAF) Or Not Ligne22.PtSurSegment(LigneDestination.pAF) Then
      ' L'intersection des lignes origine et destination est sur un des 2 segments
      ' ou bien elle se trouve loin � l'ext�rieur des 2 lignes (cas de 2 branches presque parall�les)
      ' ou encore p.IsEmpty car les 2 segments sont parall�les
      'Ins�rer un raccord entre les 2 lignes
      LigneRaccord = New Ligne(LigneOrigine.pAF, LigneDestination.pAF)
      AvecRaccord = True

    ElseIf BrancheDestination Is mVariante.mBranches.Suivante(BrancheOrigine) Or BrancheOrigine Is mVariante.mBranches.Suivante(BrancheDestination) Then

      ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
      ' Les prolonger toutes les 2 jusqu'� ce point
      LigneOrigine.pAF = p
      LigneDestination.pAF = p
    End If

    Dim pr1 As PointF = intersect(LigneOrigine, RaccordExtr�mit�s, Formules.TypeInterSection.SurPremierSegment)
    If pr1.IsEmpty Then ' LigneOrigine et le raccord sont colin�aires ou l'intersection est audel� dela ligne support de la trajectoire origine
      pr1 = LigneOrigine.pAF
    Else
      LigneOrigine.pAF = pr1
    End If
    pr1 = intersect(LigneOrigine, RaccordOrigine)
    If pr1.IsEmpty Then
      pr1 = LigneOrigine.pAF
    Else
      LigneOrigine.pAF = pr1
    End If
    Dim pr2 As PointF = intersect(LigneDestination, RaccordExtr�mit�s, Formules.TypeInterSection.SurPremierSegment)
    If pr2.IsEmpty Then
      pr2 = LigneDestination.pAF
    Else
      LigneDestination.pAF = pr2
    End If
    pr2 = intersect(LigneDestination, RaccordDestination)
    If pr2.IsEmpty Then
      pr2 = LigneDestination.pAF
    Else
      LigneDestination.pAF = pr2
    End If

    If Distance(pr1, pr2) / Echelle < 3 Then
      'on n'a pas r�ussi � tronquer les 2 lignes
      Cr�erRaccord(LigneOrigine, LigneDestination)
      pr1 = LigneOrigine.pAF
      pr2 = LigneDestination.pAF
    End If
    LigneRaccord = New Ligne(pr1, pr2)

    'Tronquer les lignes si n�cessaire si LigneRaccord franchit une ligne infranchissable
    pr1 = intersect(LigneInfranchissableOrigine, LigneRaccord)
    If Not pr1.IsEmpty Then
      LigneRaccord.pAF = LigneInfranchissableOrigine.pAF
      LigneOrigine.pAF = intersect(LigneOrigine, LigneRaccord, Formules.TypeInterSection.Indiff�rent)
      LigneRaccord.pAF = LigneOrigine.pAF
    End If
    pr2 = intersect(LigneInfranchissableDestination, LigneRaccord)
    If Not pr2.IsEmpty Then
      LigneRaccord.pBF = LigneInfranchissableDestination.pAF
      LigneDestination.pAF = intersect(LigneDestination, LigneRaccord, Formules.TypeInterSection.Indiff�rent)
      LigneRaccord.pBF = LigneDestination.pAF
    End If
    pr1 = intersect(LigneInfranchissableOrigine2, LigneRaccord)
    If Not pr1.IsEmpty Then
      LigneRaccord.pAF = LigneInfranchissableOrigine2.pAF
      LigneOrigine.pAF = intersect(LigneOrigine, LigneRaccord, Formules.TypeInterSection.Indiff�rent)
      LigneRaccord.pAF = LigneOrigine.pAF
    End If
    pr2 = intersect(LigneInfranchissableDestination2, LigneRaccord)
    If Not pr2.IsEmpty Then
      LigneRaccord.pBF = LigneInfranchissableDestination2.pAF
      LigneDestination.pAF = intersect(LigneDestination, LigneRaccord, Formules.TypeInterSection.Indiff�rent)
      LigneRaccord.pBF = LigneDestination.pAF
    End If

    AvecRaccord = True
    'End If

    If AvecRaccord Then
      ReDim mPoints(3)
      'AjusterRaccord(LigneOrigine, LigneDestination, LigneRaccord)

    Else
      ReDim mPoints(2)
    End If

    mPoints(0) = PointR�el(LigneOrigine.pBF)

    If AvecRaccord Then
      mPoints(1) = PointR�el(LigneRaccord.pAF)
      mPoints(2) = PointR�el(LigneRaccord.pBF)
    Else
      mPoints(1) = PointR�el(LigneOrigine.pAF)
    End If
    mPoints(mPoints.Length - 1) = PointR�el(LigneDestination.pBF)

    '=== Traitement du raboutement des trajectoires � tron�on origine ou destination identique
    'Dim c As TrajectoireCollection = V�rifierConflits()
    'If Not IsNothing(c) Then
    '  c.Cr�erGraphique(uneCollection, AntagonismesACr�er:=False)
    'End If

    '== 1er essai de d�finition des objets 'r�els'
    'Dim pk(mPoints.Length - 1) As Point
    'Dim i As Short
    'Dim l1, l2 As Ligne
    'Dim unPolyarc As New PolyArc
    'Dim uneFigure As Graphique

    'For i = 0 To mPoints.Length - 1
    '  pk(i) = PointDessin(mPoints(i))
    'Next

    'For i = 1 To mPoints.Length - 2
    '  If i = 1 Then
    '    l1 = New Ligne(pk(i), pk(i - 1))
    '    unPolyarc.Add(l1, Poign�esACr�er:=False)
    '  Else
    '    l1 = l2.Invers�e
    '    unPolyarc.Add(l1, Poign�esACr�er:=False)
    '  End If
    '  l2 = New Ligne(pk(i), pk(i + 1))
    '  unArc = Cr�erRaccord(l1, l2)
    '  unPolyarc.Add(unArc)
    'Next
    'unPolyarc.Add(l2, Poign�esACr�er:=False)

    'For Each uneFigure In unPolyarc.Figures
    '  If TypeOf uneFigure Is Ligne Then
    '    With CType(uneFigure, Ligne)
    '      mDessinR�el.Add(New Ligne(PointR�el(.pA), PointR�el(.pB)))
    '    End With

    '  Else    ' Forc�ment un arc
    '    With CType(uneFigure, Arc)
    '      mDessinR�el.Add(New Arc(PointR�el(.pO), .Rayon / Echelle, .AngleD�part, .AngleBalayage))
    '    End With
    '  End If
    'Next

    '== D�finition des lignes et arcs d�finissant la trajectoire en coordonn�es r�elles (2�me essai)
    'mDessinR�el = New PolyArc
    'For i = 1 To mPoints.Length - 2
    '  If i = 1 Then
    '    l1 = New Ligne(mPoints(i), mPoints(i - 1))
    '    mDessinR�el.Add(l1, Poign�esACr�er:=False)
    '  Else
    '    l1 = l2.Invers�e
    '    mDessinR�el.Add(l1, Poign�esACr�er:=False)
    '  End If
    '  l2 = New Ligne(mPoints(i), mPoints(i + 1))
    '  unArc = Cr�erRaccord(l1, l2, R:=3 / Echelle)
    '  mDessinR�el.Add(unArc, Poign�esACr�er:=False)
    'Next
    'mDessinR�el.Add(l2, Poign�esACr�er:=False)

  End Sub

  Private Sub AjusterRaccord(ByVal SegmentD�part As Ligne, ByVal SegmentArriv�e As Ligne, ByVal SegmentRaccord As Ligne)
    Dim pC1, pC2 As PointF

    pC1 = mVoie(OrigineDestEnum.Origine).AjusterRaccord(SegmentD�part, SegmentArriv�e, SegmentRaccord, Branche.Lat�ralit�.Aucune, CoefLargeur:=0.5)
    pC2 = mVoie(OrigineDestEnum.Destination).AjusterRaccord(SegmentArriv�e, SegmentD�part, SegmentRaccord, Branche.Lat�ralit�.Aucune, CoefLargeur:=0.5)

    If Not pC1.IsEmpty Then
      SegmentD�part.pAF = pC1
      SegmentRaccord.pAF = pC1
    End If

    If Not pC2.IsEmpty Then
      SegmentArriv�e.pAF = pC2
      SegmentRaccord.pBF = pC2
    End If

  End Sub

  Private Sub Cr�erFl�ches()
    Dim LigneOrigine As Ligne = AxeVoie(OrigineDestEnum.Origine)
    Dim LigneDestination As Ligne = AxeVoie(OrigineDestEnum.Destination)
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.TrajectoireFl�ches).Clone

    If Not IsNothing(mFl�ches) Then
      mFl�ches.Clear()
      mFl�ches = Nothing
    End If

    mFl�ches = New PolyArc
    ' Cr�er une fl�che au milieu du 1er segment de trajectoire
    ' soit 8 pixels pour la base de la fl�che
    Dim uneFl�che As New Fleche(Longueur:=0, HauteurFl�che:=6, unePlume:=unePlume)
    Dim mFl�che As Fleche
    mFl�che = uneFl�che.RotTrans(LigneOrigine.MilieuF, AngleForm�(LigneOrigine))
    'Ajouter la fl�che 
    mFl�ches.Add(mFl�che)
    ' Cr�er une fl�che au milieu du 2�me segment de trajectoire
    mFl�che = uneFl�che.RotTrans(LigneDestination.MilieuF, AngleForm�(LigneDestination) - Math.PI)
    'Ajouter la fl�che 
    mFl�ches.Add(mFl�che)
    ' Cr�er une fl�che a l'extr�mit� de trajectoire
    uneFl�che = New Fleche(0, HauteurFl�che:=6, Delta:=-3, unePlume:=unePlume)
    mFl�che = uneFl�che.RotTrans(LigneDestination.pBF, AngleForm�(LigneDestination) - Math.PI)
    'Ajouter la fl�che 
    mFl�ches.Add(mFl�che)

  End Sub

  'Protected Function V�rifierConflits() As TrajectoireCollection
  '  Dim uneTrajectoire As Trajectoire
  '  Dim uneTrajectoireV�hicules As TrajectoireV�hicules
  '  Dim mPts As PointF()
  '  Dim p As PointF
  '  Dim dctTraj As New Hashtable
  '  Dim i As Short
  '  Dim un�l�ment As Object

  '  For Each uneTrajectoire In cndVariante.mTrajectoires
  '    If uneTrajectoire.EstV�hicule Then
  '      uneTrajectoireV�hicules = uneTrajectoire
  '      With uneTrajectoireV�hicules
  '        If Not uneTrajectoireV�hicules Is Me And uneTrajectoireV�hicules.Points.Length > 0 Then
  '          ' Graphique d�j� d�fini (test � supprimer dans la version d�finitive ???)
  '          If Voie(OrigineDestEnum.Origine) Is .Voie(OrigineDestEnum.Origine) Then
  '            dctTraj.Add(uneTrajectoireV�hicules, Distance(Points(0), .Points(1)))
  '          End If
  '        End If
  '      End With
  '    End If
  '  Next

  '  If dctTraj.Count > 0 Then
  '    Dim d As Single = Distance(Points(0), Points(1))
  '    p = Points(1)
  '    Dim d2 As Single
  '    Dim tbl(dctTraj.Count - 1) As Object

  '    dctTraj.Keys.CopyTo(tbl, 0)

  '    For i = 0 To tbl.Length - 1
  '      uneTrajectoireV�hicules = tbl(i)
  '      d2 = dctTraj(uneTrajectoireV�hicules)
  '      If d2 > d Then
  '        d = d2
  '        p = uneTrajectoireV�hicules.Points(1)
  '      End If
  '    Next

  '    If d > dctTraj(Me) Then
  '      Points(1) = p
  '      Dim col As New TrajectoireCollection
  '      For i = 0 To tbl.Length - 1
  '        uneTrajectoireV�hicules = tbl(i)
  '        d2 = dctTraj(uneTrajectoireV�hicules)
  '        If d2 < d Then
  '          uneTrajectoireV�hicules.Points(1) = p
  '          col.Add(uneTrajectoireV�hicules)
  '        End If
  '      Next
  '      Return col
  '    End If

  '  End If

  'End Function

  Private Property Points() As PointF()
    Get
      Return mPoints
    End Get
    Set(ByVal Value As PointF())

    End Set
  End Property
  'Private Sub InitGraphiqueV4()
  '  Dim Ligne1, Ligne2, Ligne3, Ligne4 As Ligne
  '  Ligne1 = mVoieOrigine.Bordure(0)
  '  Ligne2 = mVoieOrigine.Bordure(1)
  '  Ligne3 = mVoieDestination.Bordure(0)
  '  Ligne4 = mVoieDestination.Bordure(1)
  '  Dim BrancheOrigine, BrancheDestination As Branche
  '  BrancheOrigine = mVoieOrigine.mBranche
  '  BrancheDestination = mVoieDestination.mBranche
  '  Dim p1 As Point = BrancheOrigine.LigneDeSym�trie.pA
  '  Dim p2, p3 As Point

  '  Dim LigneOrigine As Ligne = New Ligne(Milieu(Ligne1.pA, Ligne2.pA), Milieu(Ligne1.pB, Ligne2.pB))
  '  Dim LigneDestination As Ligne = New Ligne(Milieu(Ligne3.pA, Ligne4.pA), Milieu(Ligne3.pB, Ligne4.pB))
  '  Dim p As Point = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indiff�rent)
  '  Dim LigneRaccord As Ligne
  '  Dim unArc, unArc1, unArc2 As Arc
  '  Dim AvecRaccord As Boolean

  '  Ligne1 = New Ligne(LigneOrigine.pB, p)
  '  Ligne2 = New Ligne(LigneDestination.pB, p)
  '  If Not Ligne1.PtSurSegment(LigneOrigine.pA) Or Not Ligne2.PtSurSegment(LigneDestination.pA) Then
  '    ' L'intersection des lignes origine et destination est sur un des 2 segments
  '    ' ou bien elle se trouve loin � l'ext�rieur des 2 lignes (cas de 2 branches presque parall�les)
  '    ' ou encore p.IsEmpty car les 2 segments sont parall�les
  '    'Ins�rer un raccord entre les 2 lignes
  '    LigneRaccord = New Ligne(LigneOrigine.pA, LigneDestination.pA)
  '    AvecRaccord = True

  '  Else
  '    ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
  '    ' Les prolonger toutes les 2 jusqu'� ce point
  '    LigneOrigine.pA = p
  '    LigneDestination.pA = p

  '    Dim lo As Ligne = LigneOrigine.Clone
  '    Dim ld As Ligne = LigneDestination.Clone
  '    ' Par d�faut : Raccorder les 2 lignes
  '    unArc = Cr�erRaccord(lo, ld)

  '    Dim uneLigne, uneLigneRaccord As Ligne
  '    Dim uneBranche As Branche
  '    ' Etudier si les 2 lignes ne traversent pas un axe de branche (auquel cas il y a un rebroussement dans le carrefour)
  '    For Each uneBranche In cndVariante.mBranches
  '      If Not uneBranche Is BrancheOrigine Then
  '        uneLigne = New Ligne(p1, uneBranche.LigneDeSym�trie.pA)
  '        p2 = intersect(lo, uneLigne)
  '        If Not p2.IsEmpty Then
  '          p3 = intersect(ld, uneLigne)
  '          If Not p3.IsEmpty Then
  '            ' Rebroussement : tronquer les lignes lors de leur rencontre avec l'axe de la branche et ins�rer un raccord
  '            LigneRaccord = New Ligne(p2, p3)
  '            uneLigneRaccord = LigneRaccord.Clone
  '            unArc1 = Cr�erRaccord(lo, uneLigneRaccord)
  '            uneLigneRaccord = uneLigneRaccord.Invers�e
  '            unArc2 = Cr�erRaccord(uneLigneRaccord, ld)
  '            AvecRaccord = True
  '          End If
  '        End If
  '      End If
  '    Next

  '  End If

  '  If AvecRaccord Then
  '    ReDim mPoints(3)
  '  Else
  '    ReDim mPoints(2)
  '  End If

  '  mPoints(0) = PointR�el(LigneOrigine.pB)
  '  If Not AvecRaccord Then
  '    mPoints(1) = PointR�el(LigneOrigine.pA)
  '  Else
  '    mPoints(1) = PointR�el(LigneRaccord.pA)
  '    mPoints(2) = PointR�el(LigneRaccord.pB)
  '  End If
  '  mPoints(mPoints.Length - 1) = PointR�el(LigneDestination.pB)

  'End Sub

  'Private Sub InitGraphiqueV3()
  '  Dim Ligne1, Ligne2, Ligne3, Ligne4 As Ligne
  '  Ligne1 = mVoieOrigine.Bordure(0)
  '  Ligne2 = mVoieOrigine.Bordure(1)
  '  Ligne3 = mVoieDestination.Bordure(0)
  '  Ligne4 = mVoieDestination.Bordure(1)
  '  Dim BrancheOrigine, BrancheDestination As Branche
  '  BrancheOrigine = mVoieOrigine.mBranche
  '  BrancheDestination = mVoieDestination.mBranche
  '  Dim p1 As Point = BrancheOrigine.LigneDeSym�trie.pA
  '  Dim p2, p3 As Point

  '  Dim LigneOrigine As Ligne = New Ligne(Milieu(Ligne1.pA, Ligne2.pA), Milieu(Ligne1.pB, Ligne2.pB))
  '  Dim LigneDestination As Ligne = New Ligne(Milieu(Ligne3.pA, Ligne4.pA), Milieu(Ligne3.pB, Ligne4.pB))
  '  Dim p As Point = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indiff�rent)
  '  Dim LigneRaccord(-1) As Ligne
  '  Dim unArc, unArc1, unArc2 As Arc
  '  Dim AvecRaccord As Boolean
  '  Dim nbRaccord As Short

  '  Ligne1 = New Ligne(LigneOrigine.pB, p)
  '  Ligne2 = New Ligne(LigneDestination.pB, p)
  '  If Not Ligne1.PtSurSegment(LigneOrigine.pA) Or Not Ligne2.PtSurSegment(LigneDestination.pA) Then
  '    ' L'intersection des lignes origine et destination est sur un des 2 segments
  '    ' ou bien elle se trouve loin � l'ext�rieur des 2 lignes (cas de 2 branches presque parall�les)
  '    ' ou encore p.IsEmpty car les 2 segments sont parall�les
  '    'Ins�rer un raccord entre les 2 lignes
  '    ReDim LigneRaccord(0)
  '    LigneRaccord(0) = New Ligne(LigneOrigine.pA, LigneDestination.pA)
  '    AvecRaccord = True
  '    nbRaccord = 1

  '  Else
  '    ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
  '    ' Les prolonger toutes les 2 jusqu'� ce point
  '    LigneOrigine.pA = p
  '    LigneDestination.pA = p

  '    Dim lo As Ligne = LigneOrigine.Clone
  '    Dim ld As Ligne = LigneDestination.Clone
  '    ' Par d�faut : Raccorder les 2 lignes
  '    unArc = Cr�erRaccord(lo, ld)

  '    Dim uneLigne, uneLigneRaccord As Ligne
  '    Dim uneBranche As Branche
  '    ' Etudier si les 2 lignes ne traversent pas un axe de branche (auquel cas il y a un rebroussement dans le carrefour)
  '    For Each uneBranche In cndVariante.mBranches
  '      If Not uneBranche Is BrancheOrigine Then
  '        uneLigne = New Ligne(p1, uneBranche.LigneDeSym�trie.pA)
  '        p2 = intersect(lo, uneLigne)
  '        If Not p2.IsEmpty Then
  '          p3 = intersect(ld, uneLigne)
  '          If Not p3.IsEmpty Then
  '            ' Rebroussement : tronquer les lignes lors de leur rencontre avec l'axe de la branche et ins�rer un raccord
  '            'Les 2 lignes suivantes sont en commentaire car elles m�ritent une analyse + fine (plusieurs raccords possibles : � mettre au point avec le proto v3)
  '            'ReDim Preserve LigneRaccord(LigneRaccord.Length)
  '            'nbRaccord += 1
  '            'Ci-dessous : les 2 Lignes de remplacement
  '            ReDim LigneRaccord(0)
  '            nbRaccord = 1
  '            LigneRaccord(nbRaccord - 1) = New Ligne(p2, p3)
  '            uneLigneRaccord = LigneRaccord(nbRaccord - 1).Clone
  '            unArc1 = Cr�erRaccord(lo, uneLigneRaccord)
  '            uneLigneRaccord = uneLigneRaccord.Invers�e
  '            unArc2 = Cr�erRaccord(uneLigneRaccord, ld)
  '            AvecRaccord = True
  '          End If
  '        End If
  '      End If
  '    Next

  '  End If

  '  ReDim mPoints(2 + nbRaccord)
  '  Dim i As Short
  '  mPoints(0) = PointR�el(LigneOrigine.pB)
  '  If nbRaccord = 0 Then
  '    mPoints(1) = PointR�el(LigneOrigine.pA)
  '  Else
  '    For i = 0 To nbRaccord - 1
  '      mPoints(i + 1) = PointR�el(LigneRaccord(i).pA)
  '    Next
  '    mPoints(nbRaccord + 1) = PointR�el(LigneRaccord(nbRaccord - 1).pB)
  '  End If
  '  mPoints(mPoints.Length - 1) = PointR�el(LigneDestination.pB)

  'End Sub

  Public Overrides Function Intersection(ByVal uneTrajectoire As TrajectoireV�hicules) As PointF
    Dim p As PointF
    Do
      p = mGraphique.Intersection(uneTrajectoire.mGraphique)
      acoTolerance += 1
    Loop Until Not p.IsEmpty Or acoTolerance = 10
    acoTolerance = 0

    If Distance(p, AxeVoie(OrigineDestEnum.Destination).pBF) < 10 Then
      '  'Point d'intersection trouv� � l'extr�mit� des segments de destination : prendre un des points d'acc�s � la branche destination
      '  Dim l1 As Ligne = mGraphique(mGraphique.Count - 1)
      '  Dim l2 As Ligne = uneTrajectoire.mGraphique(uneTrajectoire.mGraphique.Count - 1)
      '  If l1.Longueur > l2.Longueur Then
      '    p = l2.pAF
      '  Else
      '    p = l1.pAF
      '  End If

      If Distance(PointR�el(p), PtsAcc�s(OrigineDestEnum.Destination)) < Distance(PointR�el(p), uneTrajectoire.PtsAcc�s(OrigineDestEnum.Destination)) Then
        p = PointDessinF(PtsAcc�s(OrigineDestEnum.Destination))
      Else
        p = PointDessinF(uneTrajectoire.PtsAcc�s(OrigineDestEnum.Destination))
      End If
    End If
    If Not p.IsEmpty Then Return p
  End Function

  Public Overrides Sub Verrouiller()
    Try
      mGraphique.RendreS�lectable(cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.G�om�trie)
      mGraphique.Invisible = (cndContexte = [Global].OngletEnum.G�om�trie)
      mFl�ches.Invisible = (cndContexte = [Global].OngletEnum.G�om�trie Or Not mVariante.SensTrajectoires)
      If mVariante.Verrou <> [Global].Verrouillage.G�om�trie Then LigneAcc�s.RendreNonS�lectable()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "TrajectoireV�hicules.Verrouiller")
    End Try

  End Sub

  Public Function ARedessiner() As Boolean
    Return mPoints.Length = 0
  End Function

  Public Property Manuel() As Boolean
    Get
      Return mPointsManuel.Length > 0
      'Modif v13 (11/01/07) : on permet �galement de modifier manuellement les points d'acc�s aux branches
      'Return mPointsManuel.Length > 2
    End Get
    Set(ByVal Value As Boolean)
      If Not Value Then
        ReDim mPointsManuel(-1)
      Else
        MessageBox.Show("Manuel interdit par programme")
      End If
    End Set
  End Property

  Public Overrides Sub R�initialiser(ByVal ConserverManuel As Boolean)
    'L'instruction qui suit obligera le prochain 'dessiner' � recalculer les points constituant la trajectoire
    ReDim mPoints(-1)
    If Not ConserverManuel Then Manuel = False
  End Sub

  Public Sub AffecterPointsManuels(ByVal p As Point())
    'Les points manuels sont les points interm�diaires entre le segment origine et le segment destination de la trajectoire(segments impos�s)
    ReDim mPointsManuel(p.Length - 1)
    Dim i As Short

    'D�finir les points interm�diaires
    For i = 0 To mPointsManuel.Length - 1
      mPointsManuel(i) = PointR�el(p(i))
    Next

    AffecterPointsV12()

  End Sub

  Public Sub AffecterPointAcc�s(ByVal p As Point, ByVal Index As OrigineDestEnum)
    Dim i As Short

    PtsAcc�s(Index) = PointR�el(p)
    If Manuel Then
      If Index = OrigineDestEnum.Origine Then
        'Modif v13 (11/01/07) : on permet �galement de modifier manuellement les points d'acc�s aux branches
        'mPointsManuel(0) = PtsAcc�s(Index)
        'If Distance(PtsAcc�s(Index), mPointsManuel(1)) < 2.0 Then
        If Distance(PtsAcc�s(Index), mPointsManuel(0)) < 2.0 Then
          For i = 1 To mPointsManuel.Length - 1
            mPointsManuel(i - 1) = mPointsManuel(i)
          Next
          ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)
        End If

      Else
        'Modif v13 (11/01/07) : on permet �galement de modifier manuellement les points d'acc�s aux branches
        'mPointsManuel(mPointsManuel.Length - 1) = PtsAcc�s(Index)
        'If Distance(PtsAcc�s(Index), mPointsManuel(mPointsManuel.Length - 2)) < 2.0 Then
        If Distance(PtsAcc�s(Index), mPointsManuel(mPointsManuel.Length - 1)) < 2.0 Then
          ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)
        End If

      End If
    End If

    AffecterPointsV12()

  End Sub

  Public Sub AffecterPointInterm�diaire(ByVal p As Point, ByVal Index As Short)
    Dim pR�el As PointF = PointR�el(p)
    Dim i As Short

    If Index = 0 And Distance(pR�el, PtsAcc�s(OrigineDestEnum.Origine)) < 2.0 Then
      For i = 1 To mPointsManuel.Length - 1
        mPointsManuel(i - 1) = mPointsManuel(i)
      Next
      ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)

    ElseIf Index = mPointsManuel.Length - 1 And Distance(pR�el, PtsAcc�s(OrigineDestEnum.Destination)) < 2.0 Then
      ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)

    Else
      mPointsManuel(Index - 1) = pR�el
      AffecterPointsV12()
    End If

  End Sub

  Public Sub AffecterPointsV12()
    'Les points manuels sont les points interm�diaires entre le segment origine et le segment destination de la trajectoire(segments impos�s)
    ReDim mPoints(mPointsManuel.Length + 3)
    Dim i As Short

    'D�finir les points correspondant au segment origine
    mPoints(0) = PointR�el(AxeVoie(OrigineDestEnum.Origine).pBF)
    mPoints(1) = PtsAcc�s(OrigineDestEnum.Origine)

    'D�finir les points interm�diaires
    For i = 0 To mPointsManuel.Length - 1
      mPoints(i + 2) = mPointsManuel(i)
    Next

    'D�finir les points correspondant au segment destination
    mPoints(mPoints.Length - 2) = PtsAcc�s(OrigineDestEnum.Destination)
    mPoints(mPoints.Length - 1) = PointR�el(AxeVoie(OrigineDestEnum.Destination).pBF)

  End Sub

  'Modif v13 (11/01/07) : on permet �galement de modifier manuellement les points d'acc�s aux branches
  Private Sub AffecterPoints()
    'Les points manuels sont les points interm�diaires entre le segment origine et le segment destination de la trajectoire(segments impos�s)
    ReDim mPoints(mPointsManuel.Length + 1)
    Dim i As Short

    'D�finir le point correspondant au segment origine(extr�mit� ext�rieure)
    mPoints(0) = PointR�el(AxeVoie(OrigineDestEnum.Origine).pBF)

    'D�finir les points interm�diaires
    For i = 0 To mPointsManuel.Length - 1
      mPoints(i + 1) = mPointsManuel(i)
    Next

    'D�finir le point correspondant au segment destination (extr�mit� ext�rieure)
    mPoints(mPoints.Length - 1) = PointR�el(AxeVoie(OrigineDestEnum.Destination).pBF)

    'Red�finir(?) si n�cessaire les points d'acc�s
    PtsAcc�s(OrigineDestEnum.Origine) = mPoints(1)
    PtsAcc�s(OrigineDestEnum.Destination) = mPoints(mPoints.Length - 2)

  End Sub


  Public Property PtsAcc�s(ByVal Index As OrigineDestEnum) As PointF
    Get
      If mPtsAcc�s(Index).IsEmpty Then
        Return PointR�el(AxeVoie(Index).pAF)
      Else
        Return mPtsAcc�s(Index)
      End If
    End Get
    Set(ByVal Value As PointF)
      mPtsAcc�s(Index) = Value
    End Set
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe Travers�ePi�tonne --------------------------
'=====================================================================================================
Public Class Travers�ePi�tonne : Inherits Trajectoire
  'Travers�e pi�tonne
  'Une travers�e repr�sente un passage pi�ton ou un ensemble de 2 passages pi�tons dont la travers�e est command�e par la m�me ligne de feux.


  '##ModelId=403C8174037A
  Public mPassages As New PassageCollection

  'Points d�crivant le contour de la travers�e pi�tonne, en coordonn�es r�elles dans le rep�re g�n�ral
  ' Le contour est d�crit dans le sens trigo et les 2 premiers points sont align�s sur le bord de chauss�e du 1er passage pi�ton
  Private mPoints() As PointF
  Private mContour As PolyArc
  Private mFl�che As Fleche ' Cet �l�ment est susceptible d'appartenir plutot � la ligne de feux pi�tons
  Private mLgMaximum As Single
  Private mLgM�diane As Single

  Public Sub New(ByVal unPassage As PassagePi�ton)
    MyBase.New()
    AjouterPassage(unPassage)
    Cr�erContour()
  End Sub

  Public Sub New(ByVal colPassages As PassageCollection)
    MyBase.New()

    Dim unPassage As PassagePi�ton
    For Each unPassage In colPassages
      AjouterPassage(unPassage)
    Next
    Cr�erContour()
  End Sub

  Public Sub New(ByVal uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow)
    MyBase.New(uneRowTrajectoire)

    Dim IDP1, IDP2 As Short
    Dim numBranche, IndexPassage As Short
    Dim uneBranche As Branche
    Dim MaxPassages As Short = PassagePi�ton.MaxPassages
    Dim unPassage As PassagePi�ton

    Try
      With uneRowTrajectoire.GetPi�tonsRows(0)
        IDP1 = .IDP1
        numBranche = IDP1 \ MaxPassages
        IndexPassage = IDP1 Mod MaxPassages
        uneBranche = cndVariante.mBranches(numBranche)
        unPassage = uneBranche.mPassages(IndexPassage)
        AjouterPassage(unPassage)

        If Not .IsIDP2Null Then
          IDP2 = .IDP2
          numBranche = IDP2 \ MaxPassages
          IndexPassage = IDP2 Mod MaxPassages
          uneBranche = cndVariante.mBranches(numBranche)
          unPassage = uneBranche.mPassages(IndexPassage)
          AjouterPassage(unPassage)
        End If
      End With

      Cr�erContour()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Lire Travers�ePi�tonne")
    End Try

  End Sub

  Public ReadOnly Property Fl�che() As Fleche
    Get
      Return mFl�che
    End Get
  End Property

  Public ReadOnly Property Contour() As PolyArc
    Get
      Return mContour
    End Get
  End Property

  Private Sub AjouterPassage(ByVal unPassage As PassagePi�ton)
    mPassages.Add(unPassage)
    unPassage.mTravers�e = Me
  End Sub

  '******************************************************************************
  ' Cr�er les points du contour � partir des points d�crivant le contour
  ' de chaque passage pi�ton composant la travers�e
  '******************************************************************************
  Private Sub Cr�erContour()
    Dim unPassage As PassagePi�ton
    Dim i As Short = 1

    If mPassages.Count = 1 Then
      'Le contour de la travers�e est identique � celui du passage
      ReDim mPoints(3)

      unPassage = mPassages(CType(0, Short))
      Array.Copy(unPassage.Points, mPoints, mPoints.Length)

    Else
      ReDim mPoints(7)
      'Traiter le 1er passage
      unPassage = mPassages(CType(0, Short))
      unPassage.mTravers�e = Me
      'Utiliser les 3 premiers coins du premier passage
      Array.Copy(unPassage.Points, mPoints, 3)

      'Mettre le 4�me coin en derni�re position
      mPoints(7) = unPassage.Points(3)

      'Traiter le 2�me passage
      unPassage = mPassages(CType(1, Short))
      unPassage.mTravers�e = Me

      'Ins�rer les points du 2�me passage
      Dim p() As PointF = unPassage.Points

      For i = 3 To 6
        mPoints(i) = p(i Mod 4)
      Next

    End If

    'Les points du contour du (des) passage(s) pi�tons son dans le rep�re de la branche
    'Convertir le contour dans le rep�re g�n�ral
    ConvertirContour()

  End Sub

  Public ReadOnly Property Points(ByVal Index As Short) As PointF
    Get
      Return mPoints(Index)
    End Get
  End Property

  '******************************************************************************
  ' Convertir les points du contour dans le rep�re g�n�ral
  '******************************************************************************
  Private Sub ConvertirContour()
    Dim i As Short

    For i = 0 To mPoints.Length - 1
      With mBranche
        mPoints(i) = .PtRep�reG�n�ral(mPoints(i))
      End With
    Next

    'Calculer la longueur de la travers�e
    Dim p1, p2 As PointF
    If mDouble Then
      p1 = mPoints(4)
      p2 = mPoints(5)
    Else
      p1 = mPoints(2)
      p2 = mPoints(3)
    End If

    'Calcul de la distance maximale parcourue par le pi�ton
    Dim lg As Single
    '1�re diagonale
    lg = Distance(mPoints(0), p1)
    '1er bord
    lg = Math.Max(lg, Distance(mPoints(0), p2))
    '2�me bord
    lg = Math.Max(lg, Distance(mPoints(1), p1))
    '2�me diagonale
    lg = Math.Max(lg, Distance(mPoints(1), p2))
    mLgMaximum = lg

    'Calcul de la m�diane
    mLgM�diane = Distance(Milieu(mPoints(0), mPoints(1)), Milieu(p1, p2))

  End Sub

  Public ReadOnly Property LgMaximum() As Single
    Get
      Return mLgMaximum
    End Get
  End Property

  Public ReadOnly Property LgM�diane() As Single
    Get
      Return mLgM�diane
    End Get
  End Property
  Public ReadOnly Property mBranche() As Branche
    Get
      Return mPassages(CType(0, Short)).mBranche
    End Get
  End Property

  Public ReadOnly Property EnDeuxTemps() As Boolean
    Get
      Return mBranche.mPassages.Count = 2 And Not mDouble
    End Get
  End Property

  Public ReadOnly Property mDouble() As Boolean
    Get
      Return mPassages.Count > 1
    End Get
  End Property

  '********************************************************************************************************************
  ' Enregistrer la travers�e pi�tonne dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow
    'Enregistrer d'abord la trajectoire
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = MyBase.Enregistrer(uneRowVariante)
    'Enregistrer les propri�t�s sp�cifiques aux pi�tons
    Dim uneRowPi�tons As DataSetDiagfeux.Pi�tonsRow = ds.Pi�tons.NewPi�tonsRow

    With uneRowPi�tons
      .IDP1 = mPassages(CType(0, Short)).ID
      If mPassages.Count = 2 Then
        .IDP2 = mPassages(CType(1, Short)).ID
      End If
      .SetParentRow(uneRowTrajectoire)
    End With

    ds.Pi�tons.AddPi�tonsRow(uneRowPi�tons)

  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetM�tier = Me

    Dim pDessin(mPoints.Length - 1) As PointF
    Dim i As Short
    For i = 0 To mPoints.Length - 1
      pDessin(i) = PointDessinF(mPoints(i))
    Next

    mContour = New PolyArc(pDessin, Clore:=True)

    mContour.Plume = cndPlumes.Plume(Plumes.PlumeEnum.Travers�eContour).Clone

    Dim p0 As PointF = Milieu(pDessin(0), pDessin(1))
    Dim p1 As PointF
    If mDouble Then
      p1 = Milieu(pDessin(4), pDessin(5))
    Else
      p1 = Milieu(pDessin(2), pDessin(3))
    End If

    Dim HauteurFl�che As Short
    Dim Delta As Single = 0.5
    Dim unePlume As Pen

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      mGraphique.Add(mContour)
      HauteurFl�che = 8
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Travers�eFl�che).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Travers�eFl�cheImpression).Clone
      HauteurFl�che = 2
    End If

    ' Cr�er une fl�che de part et d'autre de la ligne en prolongeant celle-ci de 50cm
    ' soit 8 pixels pour la base de la fl�che (2 mm pour les impressions)
    Dim uneFl�che As New Fleche(Distance(p0, p1), HauteurFl�che:=HauteurFl�che, Delta:=Delta * Echelle, unePlume:=unePlume, FlecheDouble:=True)

    mFl�che = uneFl�che.RotTrans(p0, AngleForm�(p0, p1))
    mFl�che.RendreS�lectable(False)
    'Ajouter la fl�che mat�rialisant la ligne de feux pi�tons
    mGraphique.Add(mFl�che, Poign�esACr�er:=False)

    uneCollection.Add(mGraphique)

  End Function

  Public Overloads Overrides Sub Verrouiller()
    Try
      mContour.RendreS�lectable(S�lectable:=cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.G�om�trie)
      mContour.Invisible = (cndContexte = [Global].OngletEnum.G�om�trie)
      mFl�che.Invisible = (cndContexte = [Global].OngletEnum.G�om�trie)
      mLigneFeux.Verrouiller()
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Travers�ePi�tonne.Verrouiller")
    End Try
  End Sub

  Public ReadOnly Property Voies() As VoieCollection
    Get
      Dim dVoies As New VoieCollection
      Dim unPassage As PassagePi�ton
      Dim uneVoie As Voie

      For Each unPassage In mPassages
        For Each uneVoie In unPassage.Voies
          If Not dVoies.Contains(uneVoie) Then dVoies.Add(uneVoie)
        Next
      Next

    End Get
  End Property

  Public Overrides Function Intersection(ByVal uneTrajectoire As TrajectoireV�hicules) As System.Drawing.PointF
    Dim pCentre As New PointF(0, 0)
    Dim p As PointF
    Dim pDessin(mPoints.Length - 1) As Point
    Dim i As Short

    For i = 0 To pDessin.Length - 1
      pDessin(i) = PointDessin(mPoints(i))
    Next
    'Origine de la branche
    Dim pOrigine As Point = PointDessin(mBranche.PtRep�reG�n�ral(pCentre))

    Dim l1 As New Ligne(pDessin(0), pDessin(pDessin.Length - 1))
    Dim l2 As New Ligne(pDessin(1), pDessin(2))
    Dim uneLigne As Ligne


    If Me.mBranche Is uneTrajectoire.Voie(TrajectoireV�hicules.OrigineDestEnum.Origine).mBranche Then
      'Rechercher le cot� du 1er passage pi�ton le plus loin de l'origine de la branche(vers l'ext�rieur du carrefour)
      If Distance(pOrigine, l1) < Distance(pOrigine, l2) Then
        uneLigne = l2
      Else
        uneLigne = l1
      End If
    Else
      'Rechercher le cot� du 1er passage pi�ton le plus proche de l'origine de la branche(vers l'int�rieur du carrefour)
      If Distance(pOrigine, l1) < Distance(pOrigine, l2) Then
        uneLigne = l1
      Else
        uneLigne = l2
      End If
    End If

    If mDouble Then
      'Travers�e double : continuer la recherche sur les cot�s du 2� passage pi�ton
      l1 = New Ligne(pDessin(3), pDessin(4))
      If Distance(pOrigine, l1) < Distance(pOrigine, uneLigne) Then uneLigne = l1
      l1 = New Ligne(pDessin(5), pDessin(6))
      If Distance(pOrigine, l1) < Distance(pOrigine, uneLigne) Then uneLigne = l1
    End If

    'D�caler la ligne d'un pixel pour qu'elle se distingue du contour du passage pi�ton
    uneLigne = uneLigne.Translation(New Vecteur(1, mBranche.AngleEnRadians + CSng(Math.PI)))

    p = uneLigne.Intersection(uneTrajectoire.mGraphique)

    If Not p.IsEmpty Then Return p

  End Function

  Public Overrides Sub R�initialiser(ByVal ConserverManuel As Boolean)
    Cr�erContour()
  End Sub

End Class