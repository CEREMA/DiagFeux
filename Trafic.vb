'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Trafic.vb																											'
'						Classes																														'
'							Trafic																																'
'******************************************************************************
Option Strict Off
Option Explicit On 

'=====================================================================================================
'--------------------------- Classe Trafic --------------------------
'=====================================================================================================

Public Class Trafic : Inherits Métier

  Public Const vMaxi As Short = 9999

  'Indique que les données trafic sont connues directement en UVP
  '##ModelId=4035E21403C8
  Private mUVP As Boolean = True

  'Tableau de trafics véhicules à 3 dimensions :
  '1ère dimension : type de trafic (VL - PL - 2R - UV)
  '2ème dimension : courant entrant (Branches A,B,....)
  '3ème dimension : courant sortantat (Branches A,B,....)
  '##ModelId=4035E250036B
  Private mQVéhicule(3, 0, 0) As Short

  'Tableau de trafics piétons dimensionné  au nombre de branches
  Private mQPiéton(0) As Short
  '
  '##ModelId=403CAED90399
  Private mVariante As Variante
  Private mNbBranches As Short
  Private mNom As String
  Public Commentaires As String
  Private mVerrouillé As Boolean

  Public Enum TraficEnum
    VL
    PL
    DEUXR
    UVP
  End Enum

  Private Sub Dimensionner(ByVal uneVariante As Variante)

    mVariante = uneVariante

    mNbBranches = mVariante.mBranches.Count

    ReDim mQVéhicule(3, mNbBranches, mNbBranches)
    ReDim mQPiéton(mNbBranches)

  End Sub

  Public Sub New(ByVal uneVariante As Variante)

    Dimensionner(uneVariante)

  End Sub

  '**************************************************************************
  ' Nouveau trafic par Duplication d'un trafic
  '**************************************************************************
  Public Sub New(ByVal unTrafic As Trafic)
    Dim numBranche, numBrancheSortante As Short
    Dim Index As TraficEnum

    Dimensionner(unTrafic.Variante)

    With unTrafic
      mUVP = .UVP
      mVerrouillé = .Verrouillé
      For numBranche = 0 To mNbBranches - 1
        QPiéton(numBranche) = .QPiéton(numBranche)
        For numBrancheSortante = 0 To mNbBranches - 1
          If UVP Then
            mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante) = .QVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante)
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              QVéhicule(Index, numBranche, numBrancheSortante) = .QVéhicule(Index, numBranche, numBrancheSortante)
            Next
          End If
        Next
      Next

    End With

  End Sub

  Public Sub Réinitialiser()

    Dim numBranche, numBrancheSortante As Short
    Dim Index As TraficEnum

    '   mUVP = False
    For numBranche = 0 To mNbBranches - 1
      QPiéton(numBranche) = 0
      For numBrancheSortante = 0 To mNbBranches - 1
        For Index = TraficEnum.VL To TraficEnum.DEUXR
          mQVéhicule(Index, numBranche, numBrancheSortante) = 0
        Next
        mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante) = 0
      Next
    Next

  End Sub

  Public Sub New(ByVal uneRowTrafic As DataSetDiagfeux.TraficRow)
    Dim Index As TraficEnum
    Dim j As Short
    Dim numBranche As Short
    Dim numBrancheSortante As Short

    Dimensionner(cndVariante)

    With uneRowTrafic
      mNom = .Nom
      UVP = .UVP
      If Not .IsVerrouilléNull Then
        Me.Verrouillé = .Verrouillé
      Else
        'Projet ACONDIA
        Me.Verrouillé = True
      End If

      For numBranche = 0 To mNbBranches - 1
        QPiéton(numBranche) = .GetQPiétonRows(numBranche).QPiéton_Column
        For numBrancheSortante = 0 To nbBranches - 1
          If UVP Then
            mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante) = .GetQVéhiculeRows(j).QVéhicule_Column
            j += 1
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              QVéhicule(Index, numBranche, numBrancheSortante) = .GetQVéhiculeRows(j).QVéhicule_Column
              j += 1
            Next
          End If
        Next
      Next

      If Not .IsTraficCommentNull Then
        Me.Commentaires = .TraficComment
      End If
    End With


  End Sub

  Public ReadOnly Property Variante() As Variante
    Get
      Return mVariante
    End Get
  End Property
  Private ReadOnly Property nbBranches() As Short
    Get
      Return mNbBranches
    End Get
  End Property

  Public Property Verrouillé() As Boolean
    Get
      Return mVerrouillé
    End Get
    Set(ByVal Value As Boolean)
      mVerrouillé = Value
    End Set
  End Property

  Public Property Nom() As String
    Get
      Return mNom
    End Get
    Set(ByVal Value As String)
      mNom = Value
    End Set
  End Property

  Public Function Libellé() As String
    If mNom.ToLower().IndexOf("période") = -1 Then
      Return "Période " & mNom
    Else
      Return mNom
    End If
  End Function

  '*********************************************************************************************************
  ' Bascule UVP <--> NonUVP : demander confirmation à l'utilisateur de réinitialisation des matrices de trafic
  '*********************************************************************************************************
  Public Function ChangeModeSaisieAccepté(ByVal UVPDemandé As Boolean) As Boolean
    Dim numBranche, numBrancheSortante As Short
    Dim Index As TraficEnum
    Dim Message As Boolean

    ChangeModeSaisieAccepté = True

    If mUVP <> UVPDemandé Then

      For numBranche = 0 To nbBranches - 1
        For numBrancheSortante = 0 To nbBranches - 1
          If mUVP Then
            If mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante) > 0 Then
              Message = True
              Exit For
            End If
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              If mQVéhicule(Index, numBranche, numBrancheSortante) > 0 Then
                Message = True
                Exit For
              End If
            Next
          End If

          If Message Then Exit For
        Next
        If Message Then Exit For
      Next

      If Message Then
        ChangeModeSaisieAccepté = Confirmation("Réinitialiser les données de trafic", Critique:=False)
      End If

      If ChangeModeSaisieAccepté Then
        mUVP = Not mUVP
        'Réinitialiser tous les trafics par nature de courant
        For numBranche = 0 To nbBranches - 1
          For numBrancheSortante = 0 To nbBranches - 1
            If mUVP Then
              For Index = TraficEnum.VL To TraficEnum.DEUXR
                mQVéhicule(Index, numBranche, numBrancheSortante) = 0
              Next
            Else
              mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante) = 0
            End If
          Next
        Next
      End If

    End If

  End Function

  Public Property UVP() As Boolean
    Get
      Return mUVP
    End Get
    Set(ByVal Value As Boolean)
      mUVP = Value
    End Set
  End Property

  '********************************************************************************************
  ' QE : Trafic total entrant par une branche
  ' Index : indique s'il s'agit dun trafic VL,PL,2 roues ou UVP
  '********************************************************************************************
  Public ReadOnly Property QE(ByVal Index As TraficEnum, ByVal numBrancheEntrante As Short) As Integer
    Get
      Dim numBranche As Short
      For numBranche = 0 To nbBranches - 1 ' 1 Trafic par branche du carrefour
        QE += mQVéhicule(Index, numBrancheEntrante, numBranche)
      Next
    End Get
  End Property

  '********************************************************************************************
  ' QS : Trafic total sortant sur une branche
  ' Index : indique s'il s'agit dun trafic VL,PL,2 roues ou UVP
  '********************************************************************************************
  Public ReadOnly Property QS(ByVal Index As TraficEnum, ByVal numBrancheSortante As Short) As Integer
    Get
      Dim numBranche As Short
      For numBranche = 0 To nbBranches - 1
        QS += mQVéhicule(Index, numBranche, numBrancheSortante)
      Next
    End Get
  End Property

  '********************************************************************************************
  ' QTotal : Trafic total du carrefour
  ' Index : indique s'il s'agit dun trafic VL,PL,2 roues ou UVP
  '********************************************************************************************
  Public ReadOnly Property QTotal(ByVal Index As TraficEnum) As Integer
    Get
      Dim numBranche As Short
      For numBranche = 0 To nbBranches - 1
        QTotal += QE(Index, numBranche)
      Next
    End Get
  End Property

  '********************************************************************************************
  ' QVéhicule : Trafic entrant sur une branche et sortant par une autre
  ' Index : indique s'il s'agit dun trafic VL,PL,2 roues ou UVP
  '********************************************************************************************
  Public Property QVéhicule(ByVal Index As TraficEnum, ByVal numBrancheEntrante As Short, ByVal numBrancheSortante As Short) As Short
    Get
      Return mQVéhicule(Index, numBrancheEntrante, numBrancheSortante)
    End Get
    Set(ByVal Value As Short)
      Dim Coefficient As Single
      Select Case Index
        Case TraficEnum.VL
          Coefficient = 1
        Case TraficEnum.PL      ' PL
          Coefficient = UvpPL
        Case TraficEnum.DEUXR      ' 2 roues
          Coefficient = Uvp2R
        Case TraficEnum.UVP      ' UVP
      End Select

      Dim Différence As Short = Value - mQVéhicule(Index, numBrancheEntrante, numBrancheSortante)
      mQVéhicule(Index, numBrancheEntrante, numBrancheSortante) = Value
      If Index < TraficEnum.UVP Then
        'Mettre à jour la valeur du trafic UVP
        mQVéhicule(TraficEnum.UVP, numBrancheEntrante, numBrancheSortante) += Différence * Coefficient
      End If
    End Set

  End Property

  '********************************************************************************************
  ' Trafic en UVP d'une branche vers une autre (= d'un mouvement)
  '********************************************************************************************
  Public ReadOnly Property QVéhicule(ByVal BrancheEntrante As Branche, ByVal BrancheSortante As Branche) As Short
    Get
      With mVariante.mBranches
        Return QVéhicule(TraficEnum.UVP, .IndexOf(BrancheEntrante), .IndexOf(BrancheSortante))
      End With
    End Get
  End Property

  '********************************************************************************************
  ' QPiéton : Trafic piéton sur une branche
  '********************************************************************************************
  Public Property QPiéton(ByVal numBranche As Short) As Short
    Get
      QPiéton = mQPiéton(numBranche)
    End Get
    Set(ByVal Value As Short)
      mQPiéton(numBranche) = Value
    End Set
  End Property

  '********************************************************************************************
  ' QPiéton : Trafic piéton sur une branche
  '********************************************************************************************
  Public ReadOnly Property QPiéton(ByVal uneBranche As Branche) As Short
    Get
      QPiéton = mQPiéton(mVariante.mBranches.IndexOf(uneBranche))
    End Get
  End Property

  Public ReadOnly Property QPiétonTotal() As Short
    Get
      Dim uneBranche As Branche

      For Each uneBranche In mVariante.mBranches
        QPiétonTotal += QPiéton(uneBranche)
      Next

      Return QPiétonTotal
    End Get
  End Property

  '********************************************************************************************************************
  ' Enregistrer le trafic dans le fichier
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim nbBranches As Short = mVariante.mBranches.Count
    Dim Index As TraficEnum

    Dim numBranche As Short
    Dim numBrancheSortante As Short

    Dim uneRowTrafic As DataSetDiagFeux.TraficRow

    Try

      uneRowTrafic = ds.Trafic.AddTraficRow(mVerrouillé, Nom, UVP, Me.Commentaires, uneRowVariante)
      For numBranche = 0 To nbBranches - 1
        ds.QPiéton.AddQPiétonRow(QPiéton(numBranche), uneRowTrafic)
        For numBrancheSortante = 0 To nbBranches - 1
          If UVP Then
            ds.QVéhicule.AddQVéhiculeRow(mQVéhicule(TraficEnum.UVP, numBranche, numBrancheSortante), uneRowTrafic)
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              ds.QVéhicule.AddQVéhiculeRow(mQVéhicule(Index, numBranche, numBrancheSortante), uneRowTrafic)
            Next
          End If
        Next
      Next

    Catch ex As System.Exception
      Throw (New DiagFeux.Exception(ex.Message & vbCrLf & "Enregistrement du trafic"))
    End Try
  End Sub


  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe TraficCollection--------------------------
'=====================================================================================================
Public Class TraficCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  Public Function Add(ByVal unTrafic As Trafic) As Short
    Return Me.List.Add(unTrafic)
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As Trafic)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unTrafic As Trafic)
    If Me.List.Contains(unTrafic) Then
      Me.List.Remove(unTrafic)
    End If

  End Sub

  Public Sub Renommer(ByVal unTrafic As Trafic, ByVal NouveauNom As String)
    Dim Index As Integer = IndexOf(unTrafic)
    Remove(unTrafic)
    unTrafic.Nom = NouveauNom
    Insert(Index, unTrafic)
  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unTrafic As Trafic)
    Me.List.Insert(Index, unTrafic)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Trafic
    Get
      Return CType(Me.List.Item(Index), Trafic)
    End Get
  End Property

  ' Creer une autre propriété par défaut Item pour cette collection.
  ' Permet la  recherche par nom.
  Default Public ReadOnly Property Item(ByVal Nom As String) As Trafic
    Get
      Dim unTrafic As Trafic
      For Each unTrafic In Me.List
        If unTrafic.Nom = Nom Then
          Return unTrafic
        End If
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unTrafic As Trafic) As Short
    Return Me.List.IndexOf(unTrafic)
  End Function

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Function Contains(ByVal unTrafic As Trafic) As Boolean
    Return Me.List.Contains(unTrafic)
  End Function

End Class