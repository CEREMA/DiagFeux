'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Trajectoire.vb																						'
'						Classes																														'
'							Trajectoire																											'
'							TrajectoireCollection												          					'
'							TrajectoireVéhicules								        										'
'							TraverséePiétonne                   														'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe Trajectoire  --------------------------
'=====================================================================================================
Public MustInherit Class Trajectoire : Inherits Métier
  'Ensemble des trajectoires véhicules du carrefour
  Public MustOverride Sub Verrouiller()
  Public MustOverride Function Intersection(ByVal uneTrajectoire As TrajectoireVéhicules) As PointF
  Public MustOverride Sub Réinitialiser(ByVal ConserverManuel As Boolean)

  'Ligne de feux commandant la trajectoire : voie(s) entrante(s) si véhicules - Passage(s) piétons si Piétons
  Protected mLigneFeux As LigneFeux

  Private dctEnConflit As New Hashtable
  Private dctPtConflit As New Hashtable

  Public Enum TypeConflitEnum
    Aucun
    Admissible
    Admis
    NonAdmis
    Systématique
  End Enum

  Public ReadOnly Property EstPiéton() As Boolean
    Get
      Return TypeOf Me Is TraverséePiétonne
    End Get
  End Property

  Public ReadOnly Property EstVéhicule() As Boolean
    Get
      Return TypeOf Me Is TrajectoireVéhicules
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
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overridable Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow

    Try
      Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = ds.Trajectoire.NewTrajectoireRow
      uneRowTrajectoire.SetParentRow(uneRowVariante)
      ds.Trajectoire.AddTrajectoireRow(uneRowTrajectoire)

      If Not IsNothing(mLigneFeux) Then
        'Pour une trajectoire véhicules, la ligne de feux correspondante n'est peut-être pas encore créée
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

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe TrajectoireCollection --------------------------
'=====================================================================================================
Public Class TrajectoireCollection : Inherits CollectionBase

  Private mVariante As Variante
  Private mAntagonismes As AntagonismeCollection

  ' Créer une instance la collection
  '  utilisée pour la collection des trajectoires commandées par une ligne de feux
  Public Sub New()
    MyBase.New()
  End Sub

  ' Créer une instance la collection
  ' Utilisée pour la collection des trajectoires de la variante
  Public Sub New(ByVal uneVariante As Variante)

    MyBase.New()
    mVariante = uneVariante
    mAntagonismes = New AntagonismeCollection(uneVariante.mLignesFeux)

  End Sub

  Public Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As Boolean
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow
    Dim desLignesPiétons As DataSetDiagfeux.PiétonsRow()
    Dim desLignesVéhicules As DataSetDiagfeux.VéhiculesRow()

    Try
      'Effacer tous les enregistrements relatifs aux trajectoires de cette variante
      Do Until uneRowVariante.GetChildRows("Variante_Trajectoire").Length = 0
        uneRowTrajectoire = uneRowVariante.GetChildRows("Variante_Trajectoire")(0)
        With uneRowTrajectoire
          desLignesPiétons = .GetChildRows("Trajectoire_Piétons")
          desLignesVéhicules = .GetChildRows("Trajectoire_Véhicules")
          'Il s'agit soit d'une trajectoire véhicules soit d'une traversée piétonn
          If desLignesPiétons.Length = 1 Then
            'Traversée piétonne
            ds.Piétons.RemovePiétonsRow(desLignesPiétons(0))
          Else
            ds.Véhicules.RemoveVéhiculesRow(desLignesVéhicules(0))
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

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal uneTrajectoire As Trajectoire) As Short
    Return Me.List.Add(uneTrajectoire)
  End Function

  Public Function Add(ByVal uneTrajectoire As Trajectoire, ByVal uneCollection As Graphiques) As Short
    'Créer sa représentation graphique
    uneTrajectoire.CréerGraphique(uneCollection)
    Return Me.List.Add(uneTrajectoire)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Trajectoire)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal uneTrajectoire As Trajectoire)
    If Me.List.Contains(uneTrajectoire) Then
      Me.List.Remove(uneTrajectoire)
    End If

  End Sub

  Public Sub Remove(ByVal uneTrajectoire As Trajectoire, ByVal uneCollection As Graphiques)

    If TypeOf uneTrajectoire Is TraverséePiétonne Then
      Dim uneTraversée As TraverséePiétonne = uneTrajectoire
      uneCollection.Remove(uneTraversée.mGraphique)
    End If

    Remove(uneTrajectoire)

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal uneTrajectoire As Trajectoire)
    Me.List.Insert(Index, uneTrajectoire)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Trajectoire
    Get
      Return CType(Me.List.Item(Index), Trajectoire)
    End Get
  End Property

  Public Function IndexOf(ByVal uneTrajectoire As Trajectoire) As Short
    Return Me.List.IndexOf(uneTrajectoire)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal uneTrajectoire As Trajectoire) As Boolean
    Return Me.List.Contains(uneTrajectoire)
  End Function

  Public Function Existe(ByVal VoieOrigine As Voie, ByVal VoieDestination As Voie) As Boolean
    Dim uneTrajectoire As Trajectoire
    Dim uneTrajectoireVéhicules As TrajectoireVéhicules

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule Then
        uneTrajectoireVéhicules = uneTrajectoire
        If VoieOrigine Is uneTrajectoireVéhicules.Voie(TrajectoireVéhicules.OrigineDestEnum.Origine) And VoieDestination Is uneTrajectoireVéhicules.Voie(TrajectoireVéhicules.OrigineDestEnum.Destination) Then
          Return True
        End If
      End If
    Next

  End Function

  Public Sub Epurer()
    Dim uneTrajectoire As Trajectoire
    Dim Continuer As Boolean

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule Then
        Remove(uneTrajectoire)
        Continuer = True
        Exit For
      End If
    Next
    If Continuer Then Epurer()
  End Sub

  Public Sub Réinitialiser(ByVal ConserverManuel As Boolean)
    Dim uneTrajectoire As Trajectoire
    For Each uneTrajectoire In Me
      uneTrajectoire.Réinitialiser(ConserverManuel)
    Next
  End Sub

  Public Function ContientManuelles() As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule AndAlso CType(uneTrajectoire, TrajectoireVéhicules).Manuel Then
        Return True
      End If
    Next
  End Function

  Public Function ContientVéhicules() As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule Then
        Return True
      End If
    Next

  End Function

#Region "Courants"
  '************************************************************************************
  ' Déterminer les courants de circulation de chaque voie commandée par une ligne de feux
  ' desVoies : Voies entrantes commandée par une ligne de feux véhicules donnée
  '************************************************************************************
  Public Sub DéterminerCourants(ByVal desVoies As VoieCollection)
    Dim uneTrajectoire As Trajectoire
    Dim uneVoie As Voie

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule Then
        With CType(uneTrajectoire, TrajectoireVéhicules)
          uneVoie = .Voie(TrajectoireVéhicules.OrigineDestEnum.Origine)
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
  'Créer les antagonismes suite à la lecture d'un projet existant
  '*************************************************************************************
  Public Sub CréerAntagonismes(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
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
            If .TypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
              mVariante.mLignesFeux.EstIncompatible(Ti.LigneFeu, Tj.LigneFeu) = True
            End If
          End With
        Next
      End With

      mAntagonismes.AntagoFiliation()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Trajectoire.CréerAntagonismess")

    End Try
  End Sub

  '********************************************************************************************************************
  ' Déterminer les points de conflit entre tous les couples de trajectoires
  ' Cette fonction est appelée une seule fois : lors du verrouillage des lignes de feux
  '********************************************************************************************************************
  Public Sub DéterminerConflits()
    Dim Ti, Tj As Trajectoire
    Dim Tvi, Tvj As TrajectoireVéhicules
    Dim uneTraversée As TraverséePiétonne
    Dim TypeConflit As Trajectoire.TypeConflitEnum
    Dim unAntagonisme As Antagonisme
    Dim Courant1, Courant2 As Courant
    Dim desBranches As BrancheCollection = mVariante.mBranches
    Dim desLignesFeux As LigneFeuxCollection = mVariante.mLignesFeux

    Dim pa As PointF

    Try

      mAntagonismes.Clear()

        For Each Ti In Me
          If Ti.EstVéhicule Then
            Tvi = Ti
            Courant1 = Tvi.Courant

            For Each Tj In Me
              If Tj.EstVéhicule Then
                ' Etudier le Conflit entre 2 trajectoires véhicules
                If IndexOf(Tj) > IndexOf(Ti) Then
                  Tvj = Tj
                  Courant2 = Tvj.Courant
                  If Not Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine) Is Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine) Then
                    TypeConflit = Trajectoire.TypeConflitEnum.Aucun
                    Select Case Courant1.NatureCourant
                      Case TrajectoireVéhicules.NatureCourantEnum.TD
                        ' Incompatibles si les courants se coupent
                        TypeConflit = QuelConflit(Courant1, Courant2, desBranches)
                        If TypeConflit = Trajectoire.TypeConflitEnum.Aucun Then
                          Select Case Tvj.NatureCourant
                            'Conflit possible TD/TAD si même branche destination
                          Case TrajectoireVéhicules.NatureCourantEnum.TAD
                              If Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination) Is Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination) Then
                                'Incompatibilité non systématique
                                TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                              End If
                          End Select
                        End If
                      Case TrajectoireVéhicules.NatureCourantEnum.TAG
                        'Le TAG doit être le 2ème paramètre
                        TypeConflit = QuelConflit(Courant2, Courant1, desBranches)
                      Case TrajectoireVéhicules.NatureCourantEnum.TAD
                        If Courant2.NatureCourant <> TrajectoireVéhicules.NatureCourantEnum.TAD Then
                          If Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination) Is Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination) Then
                            'Incompatibilité non systématique
                            TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                          Else
                            TypeConflit = QuelConflit(Courant2, Courant1, desBranches)
                          End If
                        End If
                        'TAD TAG entre eux : jamais de problème
                    End Select

                    If TypeConflit <> Trajectoire.TypeConflitEnum.Aucun Then
                      Select Case Tvi.NatureCourant
                        Case TrajectoireVéhicules.NatureCourantEnum.TD
                          pa = CréerAntagonisme(Tvi, Tj, TypeConflit)
                        Case TrajectoireVéhicules.NatureCourantEnum.TAG
                          If Tvj.NatureCourant = TrajectoireVéhicules.NatureCourantEnum.TD Then
                            ' En cas de conflit TAG/TD on positionne le courant TD en 1er
                            pa = CréerAntagonisme(Tvj, Ti, TypeConflit)
                          Else
                            pa = CréerAntagonisme(Tvi, Tj, TypeConflit)
                          End If
                        Case TrajectoireVéhicules.NatureCourantEnum.TAD
                          ' On positionne le courant TAD en 2ème
                          pa = CréerAntagonisme(Tvj, Ti, TypeConflit)
                      End Select
                    End If

                  End If    ' TVi et TVj d'origines différentes
                End If      ' IndexOf(Tj) > IndexOf(Ti)

              Else
                ' Trajectoire piéton
                uneTraversée = Tj
                Dim unPassage As PassagePiéton
                Dim uneVoie As Voie
                TypeConflit = Trajectoire.TypeConflitEnum.Aucun
                For Each unPassage In uneTraversée.mPassages
                  For Each uneVoie In unPassage.Voies
                    If uneVoie Is Tvi.Voie(TrajectoireVéhicules.OrigineDestEnum.Origine) Then
                      TypeConflit = Trajectoire.TypeConflitEnum.Systématique
                    ElseIf uneVoie Is Tvi.Voie(TrajectoireVéhicules.OrigineDestEnum.Destination) Then
                      Select Case Tvi.NatureCourant
                        Case TrajectoireVéhicules.NatureCourantEnum.TD
                          TypeConflit = Trajectoire.TypeConflitEnum.Systématique
                        Case Else
                          TypeConflit = Trajectoire.TypeConflitEnum.Admissible
                      End Select
                    End If
                  Next
                Next

                If TypeConflit <> Trajectoire.TypeConflitEnum.Aucun Then
                  pa = CréerAntagonisme(Tvi, Tj, TypeConflit)
                End If

              End If ' Tj est véhicule

            Next  ' Tj
          End If  ' Ti est véhicule
        Next      ' Ti

        For Each unAntagonisme In mAntagonismes
          With unAntagonisme
          'Si les lignes de feux sont strictement incompatibles(déterminé par CréerAntagonisme), l'antagonisme est systématique
            If desLignesFeux.EstIncompatible(.LigneFeu(Antagonisme.PositionEnum.Premier), .LigneFeu(Antagonisme.PositionEnum.Dernier)) Then
            unAntagonisme.TypeConflit = Trajectoire.TypeConflitEnum.Systématique
            End If
            ' Pour DIAGFEUX, ces objets graphiques ne seront jamais directement dssinés ni utilisés, mais il est préférable de les conserver
            'unAntagonisme.CréerGraphique(uneCollection)
          End With
        Next

        mAntagonismes.AntagoFiliation()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Trajectoire.DéterminerConflits")
    End Try

  End Sub

  '********************************************************************************************************************
  ' Déterminer la position du point de conflit et créer l'antagonisme 
  ' Activer l'incompatibilité des lignes de feux correspondantes (si c'est un systématique)
  '********************************************************************************************************************
  Private Function CréerAntagonisme(ByVal Ti As TrajectoireVéhicules, ByVal Tj As Trajectoire, ByVal TypeConflit As Trajectoire.TypeConflitEnum) As PointF
    Dim p As PointF
    Dim unAntagonisme As Antagonisme

    Try
      p = Tj.Intersection(Ti)
      If Not p.IsEmpty Then
        p = PointRéel(p)
        Ti.PtConflit(Tj) = p
        unAntagonisme = New Antagonisme(Ti, Tj, p, TypeConflit)
        'Ajouter l'antagonisme à la collection
        'Cette instruction permet aussi de regrouper les antagonismes qui sont liés car correspondant aux mêmes courants de circulation
        mAntagonismes.Add(unAntagonisme)
        If TypeConflit = Trajectoire.TypeConflitEnum.Systématique Then
          mVariante.mLignesFeux.EstIncompatible(Ti.LigneFeu, Tj.LigneFeu) = True
        End If

      End If

      Return p

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "CréerAntagonisme")
    End Try

  End Function

  Private Function QuelConflit(ByVal Courant1 As Courant, ByVal Courant2 As Courant, ByVal mesBranches As BrancheCollection) As Trajectoire.TypeConflitEnum
    Dim BO1, BO2, BD1, BD2, boucleBranche As Branche
    Dim TrajConsécutives, TrajConcurrentes As Boolean
    Dim Intersection As Boolean

    ' Détermination des branches origines et Destination de chaque courant
    BO1 = Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    BD1 = Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination)
    BO2 = Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine)
    BD2 = Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination)

    boucleBranche = BO1
    ' Faire une boucle sur les branches, en partant de la branche origine du 1er courant jusq'à trouver sa branche destination
    Do Until boucleBranche Is BD1
      boucleBranche = mesBranches.Suivante(boucleBranche)
      If boucleBranche Is BO2 Then
        TrajConsécutives = True
      ElseIf boucleBranche Is BD2 Then
        TrajConcurrentes = True
        boucleBranche = BD1
      ElseIf TrajConsécutives Then
        'Arrêter la boucle puisu'on a rattrapé la branche origine du 2ème courant
        boucleBranche = BD1
      End If
    Loop

    If TrajConcurrentes Then
      Intersection = True

    ElseIf TrajConsécutives Then
      If Not boucleBranche Is BO2 Then Intersection = True
    End If

    If Intersection Then
      If Courant2.NatureCourant = TrajectoireVéhicules.NatureCourantEnum.TAG Then
        'Le conflit TAGTD n'est pas systématique
        If Courant1.NatureCourant = TrajectoireVéhicules.NatureCourantEnum.TAG Then
          ' Conflit TAG/TAG : non systématique
          QuelConflit = Trajectoire.TypeConflitEnum.Admissible
        Else
          QuelConflit = ConflitTagTd(Courant1, Courant2, mesBranches)
        End If
      Else
        QuelConflit = Trajectoire.TypeConflitEnum.Systématique
      End If
    End If

  End Function

  Private Function ConflitTagTd(ByVal Courant1 As Courant, ByVal Courant2 As Courant, ByVal mesBranches As BrancheCollection) As Trajectoire.TypeConflitEnum
    Dim Index(2) As Short

    With mesBranches
      Index(0) = .IndexOf(Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination))
      Index(1) = .IndexOf(Courant1.Branche(TrajectoireVéhicules.OrigineDestEnum.Destination))
      Index(2) = .IndexOf(Courant2.Branche(TrajectoireVéhicules.OrigineDestEnum.Origine))
      If Index(1) < Index(0) Then Index(1) += .Count
      If Index(2) < Index(0) Then Index(2) += .Count
      If Index(1) > Index(2) Then
        ' Ce n'est pas vraiment un confllit TAGTD : on revient au conflit TD/TD classique
        Return Trajectoire.TypeConflitEnum.Systématique
      Else
        Return Trajectoire.TypeConflitEnum.Admissible
      End If
    End With

  End Function

  '*************************************************************************
  'Créer les objets graphiques trajectoires
  'Créer aussi s'il y a lieu ceux des antagonismes(points de conflits)
  '*************************************************************************
  Public Function CréerGraphique(ByVal uneCollection As Graphiques) As Graphique
    Dim uneTrajectoire As Trajectoire

    'Trajectoires
    For Each uneTrajectoire In Me
      uneTrajectoire.CréerGraphique(uneCollection)
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
  ' Recherche si une voie est origine d'au moins une trajectoire véhicules
  '*************************************************************************************
  Public Function ContientOrigine(ByVal uneVoie As Voie) As Boolean
    Dim uneTrajectoire As Trajectoire

    For Each uneTrajectoire In Me
      If uneTrajectoire.EstVéhicule Then
        If CType(uneTrajectoire, TrajectoireVéhicules).Voie(TrajectoireVéhicules.OrigineDestEnum.Origine) Is uneVoie Then Return True
      End If
    Next

  End Function

  Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
    If TypeOf value Is TrajectoireVéhicules Then
      Dim uneLigneFeux As LigneFeuVéhicules = CType(value, TrajectoireVéhicules).LigneFeu
      If Not IsNothing(uneLigneFeux) Then
        uneLigneFeux.DéterminerNatureCourants(Me)
      End If
    End If
  End Sub
End Class

'=====================================================================================================
'--------------------------- Classe TrajectoireVéhicules --------------------------
'=====================================================================================================

Public Class TrajectoireVéhicules : Inherits Trajectoire
  'Trajectoire véhicule

  Public Enum TypeCourantEnum
    TypeCourantMixte
    TypeCourantTC   ' Transports en commun
    TypeCourant2R   ' 2 roues
  End Enum


  Public Enum NatureCourantEnum
    Aucun = -1
    TAD   ' Tourne à droite
    TD   ' Tout droit
    TAG   ' Tourne à gauche

  End Enum

  Public Enum OrigineDestEnum
    Origine
    Destination
  End Enum


  'Le type de courant peut être : TC(transports en commun),deux-roues ou mixte (tous véhicules)
  Private mTypeCourant As TrajectoireVéhicules.TypeCourantEnum

  '##ModelId=403312F900CB
  Private mCourant As Courant

  '##ModelId=403C7FD00222
  Private mVoie(1) As Voie
  ' Points représentant la trajectoire en coordonnées réelles
  Private mPoints(-1) As PointF
  Private mPointsManuel(-1) As PointF
  Private mDessinRéel As PolyArc
  Private mFlèches As PolyArc
  Private mPtsAccès(1) As PointF
  Public LigneAccès As Ligne
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

    With uneRowTrajectoire.GetVéhiculesRows(0)
      numBranche = .VoieOrigine \ MaxVoies
      IndexVoie = .VoieOrigine Mod MaxVoies
      uneBranche = cndVariante.mBranches(numBranche)
      mVoie(OrigineDestEnum.Origine) = uneBranche.Voies(IndexVoie)

      numBranche = .VoieDestination \ MaxVoies
      IndexVoie = .VoieDestination Mod MaxVoies
      uneBranche = cndVariante.mBranches(numBranche)
      mVoie(OrigineDestEnum.Destination) = uneBranche.Voies(IndexVoie)

      Courant = cndVariante.mCourants(mVoie(OrigineDestEnum.Origine).mBranche, mVoie(OrigineDestEnum.Destination).mBranche)
      'Les instructions qui suivent permettent en fait d'affecter les propriétés TypeCourant(de la trajectoire) e NatureCourant(du courant)
      LibelTypeCourant = .TypeCourant()
      LibelNatureCourant = .NatureCourant()
      'Affecte en fait le coefficient de gêne au courant de la trajectoire
      CoefGêne = .CoefGene
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

  Public Property CoefGêne() As Single
    Get
      Return mCourant.CoefGêne
    End Get
    Set(ByVal Value As Single)
      mCourant.CoefGêne = Value
    End Set
  End Property

  '********************************************************************************************************************
  ' Enregistrer la trajectoire véhicules dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow
    'Enregistrer d'abord la trajectoire
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = MyBase.Enregistrer(uneRowVariante)
    'Enregistrer les propriétés spécifiques aux véhicules
    Dim uneRowVéhicules As DataSetDiagfeux.VéhiculesRow = ds.Véhicules.NewVéhiculesRow

    With uneRowVéhicules
      .VoieOrigine = Voie(OrigineDestEnum.Origine).ID
      .VoieDestination = Voie(OrigineDestEnum.Destination).ID
      .TypeCourant = LibelTypeCourant()
      .NatureCourant = LibelNatureCourant()
      .CoefGene = CoefGêne
      .SetParentRow(uneRowTrajectoire)
    End With

    ds.Véhicules.AddVéhiculesRow(uneRowVéhicules)

    If Manuel Then
      Dim i As Short
      For i = 0 To mPointsManuel.Length - 1
        ds.PointManuel.AddPointManuelRow(mPointsManuel(i).X, mPointsManuel(i).Y, uneRowVéhicules)
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

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    Dim unePlume As Pen

    Select Case cndFlagImpression
      Case dlgImpressions.ImpressionEnum.Aucun
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.Trajectoire).Clone
      Case dlgImpressions.ImpressionEnum.DiagrammePhases, dlgImpressions.ImpressionEnum.Matrice
        unePlume = cndPlumes.Plume(Plumes.PlumeEnum.TrajectoireImpression).Clone
      Case Else
        Return Nothing
    End Select

    Dim PoignéesACréer As Boolean

    PoignéesACréer = True

    Try

      ' Effacer l'ancien objet graphique s'il existe et l'instancier
      mGraphique = ClearGraphique(uneCollection, mGraphique)
      mGraphique.ObjetMétier = Me

      If mPoints.Length = 0 Then

        If Manuel Then
          AffecterPointsV12()
        Else
          InitGraphique(uneCollection)
        End If
      End If

      If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
        CréerFlèches()
        uneCollection.Add(mFlèches)
        mFlèches.Invisible = Not mVariante.SensTrajectoires
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
          mGraphique.Add(l1, PoignéesACréer:=PoignéesACréer)
        Else
          l1 = l2.Inversée
          mGraphique.Add(l1, PoignéesACréer:=PoignéesACréer)
        End If
        l2 = New Ligne(pDessin(i), pDessin(i + 1), unePlume)
        unArc = CréerRaccord(l1, l2, unePlume:=unePlume)
        mGraphique.Add(unArc)
      Next
      mGraphique.Add(l2, PoignéesACréer:=PoignéesACréer)

      LigneAccès = New Ligne(PointDessin(PtsAccès(OrigineDestEnum.Origine)), PointDessin(PtsAccès(OrigineDestEnum.Destination)))

      If Manuel Then
        'PolyManuel comprend les points d'accès et les points manuels intermédiaires
        For i = 1 To pDessin.Length - 2
          pDessin(i - 1) = pDessin(i)
        Next
        ReDim Preserve pDessin(pDessin.Length - 3)
        PolyManuel = New PolyArc(pDessin, Clore:=False)
        mGraphique.Add(PolyManuel)

      Else
        ClearGraphique(Nothing, PolyManuel)
        ' On ne l'ajoute pas au graphique afin qu'il n'intervienne pas dans la recherche des conflits
        '      mGraphique.Add(LigneAccès)
      End If

      uneCollection.Add(mGraphique)

      Return mGraphique

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Variante.CréerGraphique")
    End Try

  End Function

  Public Function MouvementPossible(ByVal pEnCours As Point, ByRef numPoint As Short) As frmCarrefour.CommandeGraphique
    Dim uneLigne As Ligne = LigneAccès
    Dim PointProche As Point
    Dim uneCommande As frmCarrefour.CommandeGraphique
    Dim i As Short
    Dim distMin, distMinPréc As Single

    distMinPréc = 500
    distMin = 500

    If Manuel Then
      With PolyManuel
        For i = 0 To .Points.Length - 1
          distMin = Math.Min(Distance(pEnCours, CvPoint(.Points(i))), distMinPréc)
          If distMin < distMinPréc Then
            distMinPréc = distMin
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

    If Distance(PointProche, pEnCours) >= RaySélect Then
      uneCommande = frmCarrefour.CommandeGraphique.AucuneCommande
    End If

    Return uneCommande

  End Function

  Public Function AxeVoie(ByVal Coté As TrajectoireVéhicules.OrigineDestEnum) As Ligne

    Return mVoie(Coté).Axe

  End Function

  Public Function Extrémité(ByVal Coté As TrajectoireVéhicules.OrigineDestEnum) As PointF
    Return AxeVoie(Coté).pAF
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
    Dim p1 As Point = BrancheOrigine.LigneDeSymétrie.pA

    Dim LigneOrigine As Ligne = AxeVoie(OrigineDestEnum.Origine).Clone
    Dim LigneDestination As Ligne = AxeVoie(OrigineDestEnum.Destination).Clone
    LigneOrigine.pA = PointDessin(PtsAccès(OrigineDestEnum.Origine))
    LigneDestination.pA = PointDessin(PtsAccès(OrigineDestEnum.Destination))

    Dim LigneInfranchissableOrigine As Ligne = mVoie(OrigineDestEnum.Origine).Bordure(Branche.Latéralité.Gauche).Clone
    Dim LigneInfranchissableDestination As Ligne = mVoie(OrigineDestEnum.Destination).Bordure(Branche.Latéralité.Gauche).Clone
    Dim LigneInfranchissableOrigine2 As Ligne = mVoie(OrigineDestEnum.Origine).Bordure(Branche.Latéralité.Droite).Clone
    Dim LigneInfranchissableDestination2 As Ligne = mVoie(OrigineDestEnum.Destination).Bordure(Branche.Latéralité.Droite).Clone
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

    Dim p As PointF = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indifférent)
    Dim LigneRaccord As Ligne
    Dim AvecRaccord As Boolean
    Dim RaccordDestination, RaccordOrigine As Ligne
    Dim unAngle As Single

    'Définir un segment raccordant les extrémités adéquate de la branche origine et de la branche destination
    Dim RaccordExtrémités As Ligne
    ' Décalage à effectuer sur le raccord pour être sur d'avoir une distance suffisante au droit de la sortie
    Dim Décalage As Single

    RaccordDestination = New Ligne(mVariante.mBranches.Précédente(BrancheDestination).ExtrémitéBordChaussée(Branche.Latéralité.Gauche), BrancheDestination.ExtrémitéBordChaussée(Branche.Latéralité.Droite))
    Décalage = 1.0 * Echelle
    unAngle = AngleFormé(RaccordDestination) - Math.PI / 2
    RaccordDestination = RaccordDestination.Translation(New Vecteur(Décalage, unAngle))
    RaccordDestination.Plume = New Pen(Color.Blue)
    '    mGraphique.Add(RaccordDestination)
    RaccordOrigine = New Ligne(BrancheOrigine.ExtrémitéBordChaussée(Branche.Latéralité.Gauche), mVariante.mBranches.Suivante(BrancheOrigine).ExtrémitéBordChaussée(Branche.Latéralité.Droite))
    Décalage = 1.0 * Echelle
    unAngle = AngleFormé(RaccordOrigine) - Math.PI / 2
    RaccordOrigine = RaccordOrigine.Translation(New Vecteur(Décalage, unAngle))
    RaccordOrigine.Plume = New Pen(Color.Blue)
    '    mGraphique.Add(RaccordOrigine)

    unAngle = CvAngleDegrés(BrancheOrigine.AngleEnRadians - BrancheDestination.AngleEnRadians, InverserSens:=False)

    With BrancheDestination
      If unAngle < 180 Then
        RaccordExtrémités = New Ligne(BrancheOrigine.ExtrémitéBordChaussée(Branche.Latéralité.Gauche), .ExtrémitéBordChaussée(Branche.Latéralité.Droite))
        Décalage = (.Voies.IndexOf(Voie(OrigineDestEnum.Destination)) + 0.5) * .LargeurVoies * Echelle
        unAngle = AngleFormé(RaccordExtrémités) - Math.PI / 2
      Else
        RaccordExtrémités = New Ligne(BrancheOrigine.ExtrémitéBordChaussée(Branche.Latéralité.Droite), BrancheDestination.ExtrémitéBordChaussée(Branche.Latéralité.Gauche))
        Décalage = (.Voies.Count - .Voies.IndexOf(Voie(OrigineDestEnum.Destination)) - 0.5) * .LargeurVoies * Echelle
        unAngle = AngleFormé(RaccordExtrémités) + Math.PI / 2
      End If

      ' Décaler le raccord vers l'intérieur du carrefour d'une distance égale à la largeur de sortie
      RaccordExtrémités = RaccordExtrémités.Translation(New Vecteur(Décalage, unAngle))
      RaccordExtrémités.Plume = New Pen(Color.Blue, 2)
      '     mGraphique.Add(RaccordExtrémités)
    End With

    Dim Ligne11 As New Ligne(LigneOrigine.pBF, p)
    Dim Ligne22 As New Ligne(LigneDestination.pBF, p)
    If Not Ligne11.PtSurSegment(LigneOrigine.pAF) Or Not Ligne22.PtSurSegment(LigneDestination.pAF) Then
      ' L'intersection des lignes origine et destination est sur un des 2 segments
      ' ou bien elle se trouve loin à l'extérieur des 2 lignes (cas de 2 branches presque parallèles)
      ' ou encore p.IsEmpty car les 2 segments sont parallèles
      'Insérer un raccord entre les 2 lignes
      LigneRaccord = New Ligne(LigneOrigine.pAF, LigneDestination.pAF)
      AvecRaccord = True

    ElseIf BrancheDestination Is mVariante.mBranches.Suivante(BrancheOrigine) Or BrancheOrigine Is mVariante.mBranches.Suivante(BrancheDestination) Then

      ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
      ' Les prolonger toutes les 2 jusqu'à ce point
      LigneOrigine.pAF = p
      LigneDestination.pAF = p
    End If

    Dim pr1 As PointF = intersect(LigneOrigine, RaccordExtrémités, Formules.TypeInterSection.SurPremierSegment)
    If pr1.IsEmpty Then ' LigneOrigine et le raccord sont colinéaires ou l'intersection est audelà dela ligne support de la trajectoire origine
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
    Dim pr2 As PointF = intersect(LigneDestination, RaccordExtrémités, Formules.TypeInterSection.SurPremierSegment)
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
      'on n'a pas réussi à tronquer les 2 lignes
      CréerRaccord(LigneOrigine, LigneDestination)
      pr1 = LigneOrigine.pAF
      pr2 = LigneDestination.pAF
    End If
    LigneRaccord = New Ligne(pr1, pr2)

    'Tronquer les lignes si nécessaire si LigneRaccord franchit une ligne infranchissable
    pr1 = intersect(LigneInfranchissableOrigine, LigneRaccord)
    If Not pr1.IsEmpty Then
      LigneRaccord.pAF = LigneInfranchissableOrigine.pAF
      LigneOrigine.pAF = intersect(LigneOrigine, LigneRaccord, Formules.TypeInterSection.Indifférent)
      LigneRaccord.pAF = LigneOrigine.pAF
    End If
    pr2 = intersect(LigneInfranchissableDestination, LigneRaccord)
    If Not pr2.IsEmpty Then
      LigneRaccord.pBF = LigneInfranchissableDestination.pAF
      LigneDestination.pAF = intersect(LigneDestination, LigneRaccord, Formules.TypeInterSection.Indifférent)
      LigneRaccord.pBF = LigneDestination.pAF
    End If
    pr1 = intersect(LigneInfranchissableOrigine2, LigneRaccord)
    If Not pr1.IsEmpty Then
      LigneRaccord.pAF = LigneInfranchissableOrigine2.pAF
      LigneOrigine.pAF = intersect(LigneOrigine, LigneRaccord, Formules.TypeInterSection.Indifférent)
      LigneRaccord.pAF = LigneOrigine.pAF
    End If
    pr2 = intersect(LigneInfranchissableDestination2, LigneRaccord)
    If Not pr2.IsEmpty Then
      LigneRaccord.pBF = LigneInfranchissableDestination2.pAF
      LigneDestination.pAF = intersect(LigneDestination, LigneRaccord, Formules.TypeInterSection.Indifférent)
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

    mPoints(0) = PointRéel(LigneOrigine.pBF)

    If AvecRaccord Then
      mPoints(1) = PointRéel(LigneRaccord.pAF)
      mPoints(2) = PointRéel(LigneRaccord.pBF)
    Else
      mPoints(1) = PointRéel(LigneOrigine.pAF)
    End If
    mPoints(mPoints.Length - 1) = PointRéel(LigneDestination.pBF)

    '=== Traitement du raboutement des trajectoires à tronçon origine ou destination identique
    'Dim c As TrajectoireCollection = VérifierConflits()
    'If Not IsNothing(c) Then
    '  c.CréerGraphique(uneCollection, AntagonismesACréer:=False)
    'End If

    '== 1er essai de définition des objets 'réels'
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
    '    unPolyarc.Add(l1, PoignéesACréer:=False)
    '  Else
    '    l1 = l2.Inversée
    '    unPolyarc.Add(l1, PoignéesACréer:=False)
    '  End If
    '  l2 = New Ligne(pk(i), pk(i + 1))
    '  unArc = CréerRaccord(l1, l2)
    '  unPolyarc.Add(unArc)
    'Next
    'unPolyarc.Add(l2, PoignéesACréer:=False)

    'For Each uneFigure In unPolyarc.Figures
    '  If TypeOf uneFigure Is Ligne Then
    '    With CType(uneFigure, Ligne)
    '      mDessinRéel.Add(New Ligne(PointRéel(.pA), PointRéel(.pB)))
    '    End With

    '  Else    ' Forcément un arc
    '    With CType(uneFigure, Arc)
    '      mDessinRéel.Add(New Arc(PointRéel(.pO), .Rayon / Echelle, .AngleDépart, .AngleBalayage))
    '    End With
    '  End If
    'Next

    '== Définition des lignes et arcs définissant la trajectoire en coordonnées réelles (2ème essai)
    'mDessinRéel = New PolyArc
    'For i = 1 To mPoints.Length - 2
    '  If i = 1 Then
    '    l1 = New Ligne(mPoints(i), mPoints(i - 1))
    '    mDessinRéel.Add(l1, PoignéesACréer:=False)
    '  Else
    '    l1 = l2.Inversée
    '    mDessinRéel.Add(l1, PoignéesACréer:=False)
    '  End If
    '  l2 = New Ligne(mPoints(i), mPoints(i + 1))
    '  unArc = CréerRaccord(l1, l2, R:=3 / Echelle)
    '  mDessinRéel.Add(unArc, PoignéesACréer:=False)
    'Next
    'mDessinRéel.Add(l2, PoignéesACréer:=False)

  End Sub

  Private Sub AjusterRaccord(ByVal SegmentDépart As Ligne, ByVal SegmentArrivée As Ligne, ByVal SegmentRaccord As Ligne)
    Dim pC1, pC2 As PointF

    pC1 = mVoie(OrigineDestEnum.Origine).AjusterRaccord(SegmentDépart, SegmentArrivée, SegmentRaccord, Branche.Latéralité.Aucune, CoefLargeur:=0.5)
    pC2 = mVoie(OrigineDestEnum.Destination).AjusterRaccord(SegmentArrivée, SegmentDépart, SegmentRaccord, Branche.Latéralité.Aucune, CoefLargeur:=0.5)

    If Not pC1.IsEmpty Then
      SegmentDépart.pAF = pC1
      SegmentRaccord.pAF = pC1
    End If

    If Not pC2.IsEmpty Then
      SegmentArrivée.pAF = pC2
      SegmentRaccord.pBF = pC2
    End If

  End Sub

  Private Sub CréerFlèches()
    Dim LigneOrigine As Ligne = AxeVoie(OrigineDestEnum.Origine)
    Dim LigneDestination As Ligne = AxeVoie(OrigineDestEnum.Destination)
    Dim unePlume As Pen = cndPlumes.Plume(Plumes.PlumeEnum.TrajectoireFlèches).Clone

    If Not IsNothing(mFlèches) Then
      mFlèches.Clear()
      mFlèches = Nothing
    End If

    mFlèches = New PolyArc
    ' Créer une flèche au milieu du 1er segment de trajectoire
    ' soit 8 pixels pour la base de la flèche
    Dim uneFlèche As New Fleche(Longueur:=0, HauteurFlèche:=6, unePlume:=unePlume)
    Dim mFlèche As Fleche
    mFlèche = uneFlèche.RotTrans(LigneOrigine.MilieuF, AngleFormé(LigneOrigine))
    'Ajouter la flèche 
    mFlèches.Add(mFlèche)
    ' Créer une flèche au milieu du 2ème segment de trajectoire
    mFlèche = uneFlèche.RotTrans(LigneDestination.MilieuF, AngleFormé(LigneDestination) - Math.PI)
    'Ajouter la flèche 
    mFlèches.Add(mFlèche)
    ' Créer une flèche a l'extrémité de trajectoire
    uneFlèche = New Fleche(0, HauteurFlèche:=6, Delta:=-3, unePlume:=unePlume)
    mFlèche = uneFlèche.RotTrans(LigneDestination.pBF, AngleFormé(LigneDestination) - Math.PI)
    'Ajouter la flèche 
    mFlèches.Add(mFlèche)

  End Sub

  'Protected Function VérifierConflits() As TrajectoireCollection
  '  Dim uneTrajectoire As Trajectoire
  '  Dim uneTrajectoireVéhicules As TrajectoireVéhicules
  '  Dim mPts As PointF()
  '  Dim p As PointF
  '  Dim dctTraj As New Hashtable
  '  Dim i As Short
  '  Dim unélément As Object

  '  For Each uneTrajectoire In cndVariante.mTrajectoires
  '    If uneTrajectoire.EstVéhicule Then
  '      uneTrajectoireVéhicules = uneTrajectoire
  '      With uneTrajectoireVéhicules
  '        If Not uneTrajectoireVéhicules Is Me And uneTrajectoireVéhicules.Points.Length > 0 Then
  '          ' Graphique déjà défini (test à supprimer dans la version définitive ???)
  '          If Voie(OrigineDestEnum.Origine) Is .Voie(OrigineDestEnum.Origine) Then
  '            dctTraj.Add(uneTrajectoireVéhicules, Distance(Points(0), .Points(1)))
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
  '      uneTrajectoireVéhicules = tbl(i)
  '      d2 = dctTraj(uneTrajectoireVéhicules)
  '      If d2 > d Then
  '        d = d2
  '        p = uneTrajectoireVéhicules.Points(1)
  '      End If
  '    Next

  '    If d > dctTraj(Me) Then
  '      Points(1) = p
  '      Dim col As New TrajectoireCollection
  '      For i = 0 To tbl.Length - 1
  '        uneTrajectoireVéhicules = tbl(i)
  '        d2 = dctTraj(uneTrajectoireVéhicules)
  '        If d2 < d Then
  '          uneTrajectoireVéhicules.Points(1) = p
  '          col.Add(uneTrajectoireVéhicules)
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
  '  Dim p1 As Point = BrancheOrigine.LigneDeSymétrie.pA
  '  Dim p2, p3 As Point

  '  Dim LigneOrigine As Ligne = New Ligne(Milieu(Ligne1.pA, Ligne2.pA), Milieu(Ligne1.pB, Ligne2.pB))
  '  Dim LigneDestination As Ligne = New Ligne(Milieu(Ligne3.pA, Ligne4.pA), Milieu(Ligne3.pB, Ligne4.pB))
  '  Dim p As Point = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indifférent)
  '  Dim LigneRaccord As Ligne
  '  Dim unArc, unArc1, unArc2 As Arc
  '  Dim AvecRaccord As Boolean

  '  Ligne1 = New Ligne(LigneOrigine.pB, p)
  '  Ligne2 = New Ligne(LigneDestination.pB, p)
  '  If Not Ligne1.PtSurSegment(LigneOrigine.pA) Or Not Ligne2.PtSurSegment(LigneDestination.pA) Then
  '    ' L'intersection des lignes origine et destination est sur un des 2 segments
  '    ' ou bien elle se trouve loin à l'extérieur des 2 lignes (cas de 2 branches presque parallèles)
  '    ' ou encore p.IsEmpty car les 2 segments sont parallèles
  '    'Insérer un raccord entre les 2 lignes
  '    LigneRaccord = New Ligne(LigneOrigine.pA, LigneDestination.pA)
  '    AvecRaccord = True

  '  Else
  '    ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
  '    ' Les prolonger toutes les 2 jusqu'à ce point
  '    LigneOrigine.pA = p
  '    LigneDestination.pA = p

  '    Dim lo As Ligne = LigneOrigine.Clone
  '    Dim ld As Ligne = LigneDestination.Clone
  '    ' Par défaut : Raccorder les 2 lignes
  '    unArc = CréerRaccord(lo, ld)

  '    Dim uneLigne, uneLigneRaccord As Ligne
  '    Dim uneBranche As Branche
  '    ' Etudier si les 2 lignes ne traversent pas un axe de branche (auquel cas il y a un rebroussement dans le carrefour)
  '    For Each uneBranche In cndVariante.mBranches
  '      If Not uneBranche Is BrancheOrigine Then
  '        uneLigne = New Ligne(p1, uneBranche.LigneDeSymétrie.pA)
  '        p2 = intersect(lo, uneLigne)
  '        If Not p2.IsEmpty Then
  '          p3 = intersect(ld, uneLigne)
  '          If Not p3.IsEmpty Then
  '            ' Rebroussement : tronquer les lignes lors de leur rencontre avec l'axe de la branche et insérer un raccord
  '            LigneRaccord = New Ligne(p2, p3)
  '            uneLigneRaccord = LigneRaccord.Clone
  '            unArc1 = CréerRaccord(lo, uneLigneRaccord)
  '            uneLigneRaccord = uneLigneRaccord.Inversée
  '            unArc2 = CréerRaccord(uneLigneRaccord, ld)
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

  '  mPoints(0) = PointRéel(LigneOrigine.pB)
  '  If Not AvecRaccord Then
  '    mPoints(1) = PointRéel(LigneOrigine.pA)
  '  Else
  '    mPoints(1) = PointRéel(LigneRaccord.pA)
  '    mPoints(2) = PointRéel(LigneRaccord.pB)
  '  End If
  '  mPoints(mPoints.Length - 1) = PointRéel(LigneDestination.pB)

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
  '  Dim p1 As Point = BrancheOrigine.LigneDeSymétrie.pA
  '  Dim p2, p3 As Point

  '  Dim LigneOrigine As Ligne = New Ligne(Milieu(Ligne1.pA, Ligne2.pA), Milieu(Ligne1.pB, Ligne2.pB))
  '  Dim LigneDestination As Ligne = New Ligne(Milieu(Ligne3.pA, Ligne4.pA), Milieu(Ligne3.pB, Ligne4.pB))
  '  Dim p As Point = intersect(LigneOrigine, LigneDestination, Formules.TypeInterSection.Indifférent)
  '  Dim LigneRaccord(-1) As Ligne
  '  Dim unArc, unArc1, unArc2 As Arc
  '  Dim AvecRaccord As Boolean
  '  Dim nbRaccord As Short

  '  Ligne1 = New Ligne(LigneOrigine.pB, p)
  '  Ligne2 = New Ligne(LigneDestination.pB, p)
  '  If Not Ligne1.PtSurSegment(LigneOrigine.pA) Or Not Ligne2.PtSurSegment(LigneDestination.pA) Then
  '    ' L'intersection des lignes origine et destination est sur un des 2 segments
  '    ' ou bien elle se trouve loin à l'extérieur des 2 lignes (cas de 2 branches presque parallèles)
  '    ' ou encore p.IsEmpty car les 2 segments sont parallèles
  '    'Insérer un raccord entre les 2 lignes
  '    ReDim LigneRaccord(0)
  '    LigneRaccord(0) = New Ligne(LigneOrigine.pA, LigneDestination.pA)
  '    AvecRaccord = True
  '    nbRaccord = 1

  '  Else
  '    ' L'intersection des lignes origine et destination est dans le prolongement des 2 segments
  '    ' Les prolonger toutes les 2 jusqu'à ce point
  '    LigneOrigine.pA = p
  '    LigneDestination.pA = p

  '    Dim lo As Ligne = LigneOrigine.Clone
  '    Dim ld As Ligne = LigneDestination.Clone
  '    ' Par défaut : Raccorder les 2 lignes
  '    unArc = CréerRaccord(lo, ld)

  '    Dim uneLigne, uneLigneRaccord As Ligne
  '    Dim uneBranche As Branche
  '    ' Etudier si les 2 lignes ne traversent pas un axe de branche (auquel cas il y a un rebroussement dans le carrefour)
  '    For Each uneBranche In cndVariante.mBranches
  '      If Not uneBranche Is BrancheOrigine Then
  '        uneLigne = New Ligne(p1, uneBranche.LigneDeSymétrie.pA)
  '        p2 = intersect(lo, uneLigne)
  '        If Not p2.IsEmpty Then
  '          p3 = intersect(ld, uneLigne)
  '          If Not p3.IsEmpty Then
  '            ' Rebroussement : tronquer les lignes lors de leur rencontre avec l'axe de la branche et insérer un raccord
  '            'Les 2 lignes suivantes sont en commentaire car elles méritent une analyse + fine (plusieurs raccords possibles : à mettre au point avec le proto v3)
  '            'ReDim Preserve LigneRaccord(LigneRaccord.Length)
  '            'nbRaccord += 1
  '            'Ci-dessous : les 2 Lignes de remplacement
  '            ReDim LigneRaccord(0)
  '            nbRaccord = 1
  '            LigneRaccord(nbRaccord - 1) = New Ligne(p2, p3)
  '            uneLigneRaccord = LigneRaccord(nbRaccord - 1).Clone
  '            unArc1 = CréerRaccord(lo, uneLigneRaccord)
  '            uneLigneRaccord = uneLigneRaccord.Inversée
  '            unArc2 = CréerRaccord(uneLigneRaccord, ld)
  '            AvecRaccord = True
  '          End If
  '        End If
  '      End If
  '    Next

  '  End If

  '  ReDim mPoints(2 + nbRaccord)
  '  Dim i As Short
  '  mPoints(0) = PointRéel(LigneOrigine.pB)
  '  If nbRaccord = 0 Then
  '    mPoints(1) = PointRéel(LigneOrigine.pA)
  '  Else
  '    For i = 0 To nbRaccord - 1
  '      mPoints(i + 1) = PointRéel(LigneRaccord(i).pA)
  '    Next
  '    mPoints(nbRaccord + 1) = PointRéel(LigneRaccord(nbRaccord - 1).pB)
  '  End If
  '  mPoints(mPoints.Length - 1) = PointRéel(LigneDestination.pB)

  'End Sub

  Public Overrides Function Intersection(ByVal uneTrajectoire As TrajectoireVéhicules) As PointF
    Dim p As PointF
    Do
      p = mGraphique.Intersection(uneTrajectoire.mGraphique)
      acoTolerance += 1
    Loop Until Not p.IsEmpty Or acoTolerance = 10
    acoTolerance = 0

    If Distance(p, AxeVoie(OrigineDestEnum.Destination).pBF) < 10 Then
      '  'Point d'intersection trouvé à l'extrémité des segments de destination : prendre un des points d'accès à la branche destination
      '  Dim l1 As Ligne = mGraphique(mGraphique.Count - 1)
      '  Dim l2 As Ligne = uneTrajectoire.mGraphique(uneTrajectoire.mGraphique.Count - 1)
      '  If l1.Longueur > l2.Longueur Then
      '    p = l2.pAF
      '  Else
      '    p = l1.pAF
      '  End If

      If Distance(PointRéel(p), PtsAccès(OrigineDestEnum.Destination)) < Distance(PointRéel(p), uneTrajectoire.PtsAccès(OrigineDestEnum.Destination)) Then
        p = PointDessinF(PtsAccès(OrigineDestEnum.Destination))
      Else
        p = PointDessinF(uneTrajectoire.PtsAccès(OrigineDestEnum.Destination))
      End If
    End If
    If Not p.IsEmpty Then Return p
  End Function

  Public Overrides Sub Verrouiller()
    Try
      mGraphique.RendreSélectable(cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.Géométrie)
      mGraphique.Invisible = (cndContexte = [Global].OngletEnum.Géométrie)
      mFlèches.Invisible = (cndContexte = [Global].OngletEnum.Géométrie Or Not mVariante.SensTrajectoires)
      If mVariante.Verrou <> [Global].Verrouillage.Géométrie Then LigneAccès.RendreNonSélectable()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "TrajectoireVéhicules.Verrouiller")
    End Try

  End Sub

  Public Function ARedessiner() As Boolean
    Return mPoints.Length = 0
  End Function

  Public Property Manuel() As Boolean
    Get
      Return mPointsManuel.Length > 0
      'Modif v13 (11/01/07) : on permet également de modifier manuellement les points d'accès aux branches
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

  Public Overrides Sub Réinitialiser(ByVal ConserverManuel As Boolean)
    'L'instruction qui suit obligera le prochain 'dessiner' à recalculer les points constituant la trajectoire
    ReDim mPoints(-1)
    If Not ConserverManuel Then Manuel = False
  End Sub

  Public Sub AffecterPointsManuels(ByVal p As Point())
    'Les points manuels sont les points intermédiaires entre le segment origine et le segment destination de la trajectoire(segments imposés)
    ReDim mPointsManuel(p.Length - 1)
    Dim i As Short

    'Définir les points intermédiaires
    For i = 0 To mPointsManuel.Length - 1
      mPointsManuel(i) = PointRéel(p(i))
    Next

    AffecterPointsV12()

  End Sub

  Public Sub AffecterPointAccès(ByVal p As Point, ByVal Index As OrigineDestEnum)
    Dim i As Short

    PtsAccès(Index) = PointRéel(p)
    If Manuel Then
      If Index = OrigineDestEnum.Origine Then
        'Modif v13 (11/01/07) : on permet également de modifier manuellement les points d'accès aux branches
        'mPointsManuel(0) = PtsAccès(Index)
        'If Distance(PtsAccès(Index), mPointsManuel(1)) < 2.0 Then
        If Distance(PtsAccès(Index), mPointsManuel(0)) < 2.0 Then
          For i = 1 To mPointsManuel.Length - 1
            mPointsManuel(i - 1) = mPointsManuel(i)
          Next
          ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)
        End If

      Else
        'Modif v13 (11/01/07) : on permet également de modifier manuellement les points d'accès aux branches
        'mPointsManuel(mPointsManuel.Length - 1) = PtsAccès(Index)
        'If Distance(PtsAccès(Index), mPointsManuel(mPointsManuel.Length - 2)) < 2.0 Then
        If Distance(PtsAccès(Index), mPointsManuel(mPointsManuel.Length - 1)) < 2.0 Then
          ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)
        End If

      End If
    End If

    AffecterPointsV12()

  End Sub

  Public Sub AffecterPointIntermédiaire(ByVal p As Point, ByVal Index As Short)
    Dim pRéel As PointF = PointRéel(p)
    Dim i As Short

    If Index = 0 And Distance(pRéel, PtsAccès(OrigineDestEnum.Origine)) < 2.0 Then
      For i = 1 To mPointsManuel.Length - 1
        mPointsManuel(i - 1) = mPointsManuel(i)
      Next
      ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)

    ElseIf Index = mPointsManuel.Length - 1 And Distance(pRéel, PtsAccès(OrigineDestEnum.Destination)) < 2.0 Then
      ReDim Preserve mPointsManuel(mPointsManuel.Length - 2)

    Else
      mPointsManuel(Index - 1) = pRéel
      AffecterPointsV12()
    End If

  End Sub

  Public Sub AffecterPointsV12()
    'Les points manuels sont les points intermédiaires entre le segment origine et le segment destination de la trajectoire(segments imposés)
    ReDim mPoints(mPointsManuel.Length + 3)
    Dim i As Short

    'Définir les points correspondant au segment origine
    mPoints(0) = PointRéel(AxeVoie(OrigineDestEnum.Origine).pBF)
    mPoints(1) = PtsAccès(OrigineDestEnum.Origine)

    'Définir les points intermédiaires
    For i = 0 To mPointsManuel.Length - 1
      mPoints(i + 2) = mPointsManuel(i)
    Next

    'Définir les points correspondant au segment destination
    mPoints(mPoints.Length - 2) = PtsAccès(OrigineDestEnum.Destination)
    mPoints(mPoints.Length - 1) = PointRéel(AxeVoie(OrigineDestEnum.Destination).pBF)

  End Sub

  'Modif v13 (11/01/07) : on permet également de modifier manuellement les points d'accès aux branches
  Private Sub AffecterPoints()
    'Les points manuels sont les points intermédiaires entre le segment origine et le segment destination de la trajectoire(segments imposés)
    ReDim mPoints(mPointsManuel.Length + 1)
    Dim i As Short

    'Définir le point correspondant au segment origine(extrémité extérieure)
    mPoints(0) = PointRéel(AxeVoie(OrigineDestEnum.Origine).pBF)

    'Définir les points intermédiaires
    For i = 0 To mPointsManuel.Length - 1
      mPoints(i + 1) = mPointsManuel(i)
    Next

    'Définir le point correspondant au segment destination (extrémité extérieure)
    mPoints(mPoints.Length - 1) = PointRéel(AxeVoie(OrigineDestEnum.Destination).pBF)

    'Redéfinir(?) si nécessaire les points d'accès
    PtsAccès(OrigineDestEnum.Origine) = mPoints(1)
    PtsAccès(OrigineDestEnum.Destination) = mPoints(mPoints.Length - 2)

  End Sub


  Public Property PtsAccès(ByVal Index As OrigineDestEnum) As PointF
    Get
      If mPtsAccès(Index).IsEmpty Then
        Return PointRéel(AxeVoie(Index).pAF)
      Else
        Return mPtsAccès(Index)
      End If
    End Get
    Set(ByVal Value As PointF)
      mPtsAccès(Index) = Value
    End Set
  End Property

End Class

'=====================================================================================================
'--------------------------- Classe TraverséePiétonne --------------------------
'=====================================================================================================
Public Class TraverséePiétonne : Inherits Trajectoire
  'Traversée piétonne
  'Une traversée représente un passage piéton ou un ensemble de 2 passages piétons dont la traversée est commandée par la même ligne de feux.


  '##ModelId=403C8174037A
  Public mPassages As New PassageCollection

  'Points décrivant le contour de la traversée piétonne, en coordonnées réelles dans le repère général
  ' Le contour est décrit dans le sens trigo et les 2 premiers points sont alignés sur le bord de chaussée du 1er passage piéton
  Private mPoints() As PointF
  Private mContour As PolyArc
  Private mFlèche As Fleche ' Cet élément est susceptible d'appartenir plutot à la ligne de feux piétons
  Private mLgMaximum As Single
  Private mLgMédiane As Single

  Public Sub New(ByVal unPassage As PassagePiéton)
    MyBase.New()
    AjouterPassage(unPassage)
    CréerContour()
  End Sub

  Public Sub New(ByVal colPassages As PassageCollection)
    MyBase.New()

    Dim unPassage As PassagePiéton
    For Each unPassage In colPassages
      AjouterPassage(unPassage)
    Next
    CréerContour()
  End Sub

  Public Sub New(ByVal uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow)
    MyBase.New(uneRowTrajectoire)

    Dim IDP1, IDP2 As Short
    Dim numBranche, IndexPassage As Short
    Dim uneBranche As Branche
    Dim MaxPassages As Short = PassagePiéton.MaxPassages
    Dim unPassage As PassagePiéton

    Try
      With uneRowTrajectoire.GetPiétonsRows(0)
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

      CréerContour()

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, " : Lire TraverséePiétonne")
    End Try

  End Sub

  Public ReadOnly Property Flèche() As Fleche
    Get
      Return mFlèche
    End Get
  End Property

  Public ReadOnly Property Contour() As PolyArc
    Get
      Return mContour
    End Get
  End Property

  Private Sub AjouterPassage(ByVal unPassage As PassagePiéton)
    mPassages.Add(unPassage)
    unPassage.mTraversée = Me
  End Sub

  '******************************************************************************
  ' Créer les points du contour à partir des points décrivant le contour
  ' de chaque passage piéton composant la traversée
  '******************************************************************************
  Private Sub CréerContour()
    Dim unPassage As PassagePiéton
    Dim i As Short = 1

    If mPassages.Count = 1 Then
      'Le contour de la traversée est identique à celui du passage
      ReDim mPoints(3)

      unPassage = mPassages(CType(0, Short))
      Array.Copy(unPassage.Points, mPoints, mPoints.Length)

    Else
      ReDim mPoints(7)
      'Traiter le 1er passage
      unPassage = mPassages(CType(0, Short))
      unPassage.mTraversée = Me
      'Utiliser les 3 premiers coins du premier passage
      Array.Copy(unPassage.Points, mPoints, 3)

      'Mettre le 4ème coin en dernière position
      mPoints(7) = unPassage.Points(3)

      'Traiter le 2ème passage
      unPassage = mPassages(CType(1, Short))
      unPassage.mTraversée = Me

      'Insérer les points du 2ème passage
      Dim p() As PointF = unPassage.Points

      For i = 3 To 6
        mPoints(i) = p(i Mod 4)
      Next

    End If

    'Les points du contour du (des) passage(s) piétons son dans le repère de la branche
    'Convertir le contour dans le repère général
    ConvertirContour()

  End Sub

  Public ReadOnly Property Points(ByVal Index As Short) As PointF
    Get
      Return mPoints(Index)
    End Get
  End Property

  '******************************************************************************
  ' Convertir les points du contour dans le repère général
  '******************************************************************************
  Private Sub ConvertirContour()
    Dim i As Short

    For i = 0 To mPoints.Length - 1
      With mBranche
        mPoints(i) = .PtRepèreGénéral(mPoints(i))
      End With
    Next

    'Calculer la longueur de la traversée
    Dim p1, p2 As PointF
    If mDouble Then
      p1 = mPoints(4)
      p2 = mPoints(5)
    Else
      p1 = mPoints(2)
      p2 = mPoints(3)
    End If

    'Calcul de la distance maximale parcourue par le piéton
    Dim lg As Single
    '1ère diagonale
    lg = Distance(mPoints(0), p1)
    '1er bord
    lg = Math.Max(lg, Distance(mPoints(0), p2))
    '2ème bord
    lg = Math.Max(lg, Distance(mPoints(1), p1))
    '2ème diagonale
    lg = Math.Max(lg, Distance(mPoints(1), p2))
    mLgMaximum = lg

    'Calcul de la médiane
    mLgMédiane = Distance(Milieu(mPoints(0), mPoints(1)), Milieu(p1, p2))

  End Sub

  Public ReadOnly Property LgMaximum() As Single
    Get
      Return mLgMaximum
    End Get
  End Property

  Public ReadOnly Property LgMédiane() As Single
    Get
      Return mLgMédiane
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
  ' Enregistrer la traversée piétonne dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overrides Function Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.TrajectoireRow
    'Enregistrer d'abord la trajectoire
    Dim uneRowTrajectoire As DataSetDiagfeux.TrajectoireRow = MyBase.Enregistrer(uneRowVariante)
    'Enregistrer les propriétés spécifiques aux piétons
    Dim uneRowPiétons As DataSetDiagfeux.PiétonsRow = ds.Piétons.NewPiétonsRow

    With uneRowPiétons
      .IDP1 = mPassages(CType(0, Short)).ID
      If mPassages.Count = 2 Then
        .IDP2 = mPassages(CType(1, Short)).ID
      End If
      .SetParentRow(uneRowTrajectoire)
    End With

    ds.Piétons.AddPiétonsRow(uneRowPiétons)

  End Function

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
    ' Effacer l'ancien objet graphique s'il existe et l'instancier
    mGraphique = ClearGraphique(uneCollection, mGraphique)

    mGraphique.ObjetMétier = Me

    Dim pDessin(mPoints.Length - 1) As PointF
    Dim i As Short
    For i = 0 To mPoints.Length - 1
      pDessin(i) = PointDessinF(mPoints(i))
    Next

    mContour = New PolyArc(pDessin, Clore:=True)

    mContour.Plume = cndPlumes.Plume(Plumes.PlumeEnum.TraverséeContour).Clone

    Dim p0 As PointF = Milieu(pDessin(0), pDessin(1))
    Dim p1 As PointF
    If mDouble Then
      p1 = Milieu(pDessin(4), pDessin(5))
    Else
      p1 = Milieu(pDessin(2), pDessin(3))
    End If

    Dim HauteurFlèche As Short
    Dim Delta As Single = 0.5
    Dim unePlume As Pen

    If cndFlagImpression = dlgImpressions.ImpressionEnum.Aucun Then
      mGraphique.Add(mContour)
      HauteurFlèche = 8
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.TraverséeFlèche).Clone
    Else
      unePlume = cndPlumes.Plume(Plumes.PlumeEnum.TraverséeFlècheImpression).Clone
      HauteurFlèche = 2
    End If

    ' Créer une flèche de part et d'autre de la ligne en prolongeant celle-ci de 50cm
    ' soit 8 pixels pour la base de la flèche (2 mm pour les impressions)
    Dim uneFlèche As New Fleche(Distance(p0, p1), HauteurFlèche:=HauteurFlèche, Delta:=Delta * Echelle, unePlume:=unePlume, FlecheDouble:=True)

    mFlèche = uneFlèche.RotTrans(p0, AngleFormé(p0, p1))
    mFlèche.RendreSélectable(False)
    'Ajouter la flèche matérialisant la ligne de feux piétons
    mGraphique.Add(mFlèche, PoignéesACréer:=False)

    uneCollection.Add(mGraphique)

  End Function

  Public Overloads Overrides Sub Verrouiller()
    Try
      mContour.RendreSélectable(Sélectable:=cndContexte >= [Global].OngletEnum.LignesDeFeux, Editable:=mVariante.Verrou = [Global].Verrouillage.Géométrie)
      mContour.Invisible = (cndContexte = [Global].OngletEnum.Géométrie)
      mFlèche.Invisible = (cndContexte = [Global].OngletEnum.Géométrie)
      mLigneFeux.Verrouiller()
    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "TraverséePiétonne.Verrouiller")
    End Try
  End Sub

  Public ReadOnly Property Voies() As VoieCollection
    Get
      Dim dVoies As New VoieCollection
      Dim unPassage As PassagePiéton
      Dim uneVoie As Voie

      For Each unPassage In mPassages
        For Each uneVoie In unPassage.Voies
          If Not dVoies.Contains(uneVoie) Then dVoies.Add(uneVoie)
        Next
      Next

    End Get
  End Property

  Public Overrides Function Intersection(ByVal uneTrajectoire As TrajectoireVéhicules) As System.Drawing.PointF
    Dim pCentre As New PointF(0, 0)
    Dim p As PointF
    Dim pDessin(mPoints.Length - 1) As Point
    Dim i As Short

    For i = 0 To pDessin.Length - 1
      pDessin(i) = PointDessin(mPoints(i))
    Next
    'Origine de la branche
    Dim pOrigine As Point = PointDessin(mBranche.PtRepèreGénéral(pCentre))

    Dim l1 As New Ligne(pDessin(0), pDessin(pDessin.Length - 1))
    Dim l2 As New Ligne(pDessin(1), pDessin(2))
    Dim uneLigne As Ligne


    If Me.mBranche Is uneTrajectoire.Voie(TrajectoireVéhicules.OrigineDestEnum.Origine).mBranche Then
      'Rechercher le coté du 1er passage piéton le plus loin de l'origine de la branche(vers l'extérieur du carrefour)
      If Distance(pOrigine, l1) < Distance(pOrigine, l2) Then
        uneLigne = l2
      Else
        uneLigne = l1
      End If
    Else
      'Rechercher le coté du 1er passage piéton le plus proche de l'origine de la branche(vers l'intérieur du carrefour)
      If Distance(pOrigine, l1) < Distance(pOrigine, l2) Then
        uneLigne = l1
      Else
        uneLigne = l2
      End If
    End If

    If mDouble Then
      'Traversée double : continuer la recherche sur les cotés du 2è passage piéton
      l1 = New Ligne(pDessin(3), pDessin(4))
      If Distance(pOrigine, l1) < Distance(pOrigine, uneLigne) Then uneLigne = l1
      l1 = New Ligne(pDessin(5), pDessin(6))
      If Distance(pOrigine, l1) < Distance(pOrigine, uneLigne) Then uneLigne = l1
    End If

    'Décaler la ligne d'un pixel pour qu'elle se distingue du contour du passage piéton
    uneLigne = uneLigne.Translation(New Vecteur(1, mBranche.AngleEnRadians + CSng(Math.PI)))

    p = uneLigne.Intersection(uneTrajectoire.mGraphique)

    If Not p.IsEmpty Then Return p

  End Function

  Public Overrides Sub Réinitialiser(ByVal ConserverManuel As Boolean)
    CréerContour()
  End Sub

End Class