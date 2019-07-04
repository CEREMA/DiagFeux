'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
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

Public Class Trafic : Inherits M�tier

  Public Const vMaxi As Short = 9999

  'Indique que les donn�es trafic sont connues directement en UVP
  '##ModelId=4035E21403C8
  Private mUVP As Boolean = True

  'Tableau de trafics v�hicules � 3 dimensions :
  '1�re dimension : type de trafic (VL - PL - 2R - UV)
  '2�me dimension : courant entrant (Branches A,B,....)
  '3�me dimension : courant sortantat (Branches A,B,....)
  '##ModelId=4035E250036B
  Private mQV�hicule(3, 0, 0) As Short

  'Tableau de trafics pi�tons dimensionn�  au nombre de branches
  Private mQPi�ton(0) As Short
  '
  '##ModelId=403CAED90399
  Private mVariante As Variante
  Private mNbBranches As Short
  Private mNom As String
  Public Commentaires As String
  Private mVerrouill� As Boolean

  Public Enum TraficEnum
    VL
    PL
    DEUXR
    UVP
  End Enum

  Private Sub Dimensionner(ByVal uneVariante As Variante)

    mVariante = uneVariante

    mNbBranches = mVariante.mBranches.Count

    ReDim mQV�hicule(3, mNbBranches, mNbBranches)
    ReDim mQPi�ton(mNbBranches)

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
      mVerrouill� = .Verrouill�
      For numBranche = 0 To mNbBranches - 1
        QPi�ton(numBranche) = .QPi�ton(numBranche)
        For numBrancheSortante = 0 To mNbBranches - 1
          If UVP Then
            mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante) = .QV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante)
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              QV�hicule(Index, numBranche, numBrancheSortante) = .QV�hicule(Index, numBranche, numBrancheSortante)
            Next
          End If
        Next
      Next

    End With

  End Sub

  Public Sub R�initialiser()

    Dim numBranche, numBrancheSortante As Short
    Dim Index As TraficEnum

    '   mUVP = False
    For numBranche = 0 To mNbBranches - 1
      QPi�ton(numBranche) = 0
      For numBrancheSortante = 0 To mNbBranches - 1
        For Index = TraficEnum.VL To TraficEnum.DEUXR
          mQV�hicule(Index, numBranche, numBrancheSortante) = 0
        Next
        mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante) = 0
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
      If Not .IsVerrouill�Null Then
        Me.Verrouill� = .Verrouill�
      Else
        'Projet ACONDIA
        Me.Verrouill� = True
      End If

      For numBranche = 0 To mNbBranches - 1
        QPi�ton(numBranche) = .GetQPi�tonRows(numBranche).QPi�ton_Column
        For numBrancheSortante = 0 To nbBranches - 1
          If UVP Then
            mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante) = .GetQV�hiculeRows(j).QV�hicule_Column
            j += 1
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              QV�hicule(Index, numBranche, numBrancheSortante) = .GetQV�hiculeRows(j).QV�hicule_Column
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

  Public Property Verrouill�() As Boolean
    Get
      Return mVerrouill�
    End Get
    Set(ByVal Value As Boolean)
      mVerrouill� = Value
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

  Public Function Libell�() As String
    If mNom.ToLower().IndexOf("p�riode") = -1 Then
      Return "P�riode " & mNom
    Else
      Return mNom
    End If
  End Function

  '*********************************************************************************************************
  ' Bascule UVP <--> NonUVP : demander confirmation � l'utilisateur de r�initialisation des matrices de trafic
  '*********************************************************************************************************
  Public Function ChangeModeSaisieAccept�(ByVal UVPDemand� As Boolean) As Boolean
    Dim numBranche, numBrancheSortante As Short
    Dim Index As TraficEnum
    Dim Message As Boolean

    ChangeModeSaisieAccept� = True

    If mUVP <> UVPDemand� Then

      For numBranche = 0 To nbBranches - 1
        For numBrancheSortante = 0 To nbBranches - 1
          If mUVP Then
            If mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante) > 0 Then
              Message = True
              Exit For
            End If
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              If mQV�hicule(Index, numBranche, numBrancheSortante) > 0 Then
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
        ChangeModeSaisieAccept� = Confirmation("R�initialiser les donn�es de trafic", Critique:=False)
      End If

      If ChangeModeSaisieAccept� Then
        mUVP = Not mUVP
        'R�initialiser tous les trafics par nature de courant
        For numBranche = 0 To nbBranches - 1
          For numBrancheSortante = 0 To nbBranches - 1
            If mUVP Then
              For Index = TraficEnum.VL To TraficEnum.DEUXR
                mQV�hicule(Index, numBranche, numBrancheSortante) = 0
              Next
            Else
              mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante) = 0
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
        QE += mQV�hicule(Index, numBrancheEntrante, numBranche)
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
        QS += mQV�hicule(Index, numBranche, numBrancheSortante)
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
  ' QV�hicule : Trafic entrant sur une branche et sortant par une autre
  ' Index : indique s'il s'agit dun trafic VL,PL,2 roues ou UVP
  '********************************************************************************************
  Public Property QV�hicule(ByVal Index As TraficEnum, ByVal numBrancheEntrante As Short, ByVal numBrancheSortante As Short) As Short
    Get
      Return mQV�hicule(Index, numBrancheEntrante, numBrancheSortante)
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

      Dim Diff�rence As Short = Value - mQV�hicule(Index, numBrancheEntrante, numBrancheSortante)
      mQV�hicule(Index, numBrancheEntrante, numBrancheSortante) = Value
      If Index < TraficEnum.UVP Then
        'Mettre � jour la valeur du trafic UVP
        mQV�hicule(TraficEnum.UVP, numBrancheEntrante, numBrancheSortante) += Diff�rence * Coefficient
      End If
    End Set

  End Property

  '********************************************************************************************
  ' Trafic en UVP d'une branche vers une autre (= d'un mouvement)
  '********************************************************************************************
  Public ReadOnly Property QV�hicule(ByVal BrancheEntrante As Branche, ByVal BrancheSortante As Branche) As Short
    Get
      With mVariante.mBranches
        Return QV�hicule(TraficEnum.UVP, .IndexOf(BrancheEntrante), .IndexOf(BrancheSortante))
      End With
    End Get
  End Property

  '********************************************************************************************
  ' QPi�ton : Trafic pi�ton sur une branche
  '********************************************************************************************
  Public Property QPi�ton(ByVal numBranche As Short) As Short
    Get
      QPi�ton = mQPi�ton(numBranche)
    End Get
    Set(ByVal Value As Short)
      mQPi�ton(numBranche) = Value
    End Set
  End Property

  '********************************************************************************************
  ' QPi�ton : Trafic pi�ton sur une branche
  '********************************************************************************************
  Public ReadOnly Property QPi�ton(ByVal uneBranche As Branche) As Short
    Get
      QPi�ton = mQPi�ton(mVariante.mBranches.IndexOf(uneBranche))
    End Get
  End Property

  Public ReadOnly Property QPi�tonTotal() As Short
    Get
      Dim uneBranche As Branche

      For Each uneBranche In mVariante.mBranches
        QPi�tonTotal += QPi�ton(uneBranche)
      Next

      Return QPi�tonTotal
    End Get
  End Property

  '********************************************************************************************************************
  ' Enregistrer le trafic dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Sub Enregistrer(ByVal uneRowVariante As DataSetDiagfeux.VarianteRow)
    Dim nbBranches As Short = mVariante.mBranches.Count
    Dim Index As TraficEnum

    Dim numBranche As Short
    Dim numBrancheSortante As Short

    Dim uneRowTrafic As DataSetDiagFeux.TraficRow

    Try

      uneRowTrafic = ds.Trafic.AddTraficRow(mVerrouill�, Nom, UVP, Me.Commentaires, uneRowVariante)
      For numBranche = 0 To nbBranches - 1
        ds.QPi�ton.AddQPi�tonRow(QPi�ton(numBranche), uneRowTrafic)
        For numBrancheSortante = 0 To nbBranches - 1
          If UVP Then
            ds.QV�hicule.AddQV�hiculeRow(mQV�hicule(TraficEnum.UVP, numBranche, numBrancheSortante), uneRowTrafic)
          Else
            For Index = TraficEnum.VL To TraficEnum.DEUXR
              ds.QV�hicule.AddQV�hiculeRow(mQV�hicule(Index, numBranche, numBrancheSortante), uneRowTrafic)
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

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc

  End Function
End Class

'=====================================================================================================
'--------------------------- Classe TraficCollection--------------------------
'=====================================================================================================
Public Class TraficCollection : Inherits CollectionBase

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet � la collection.
  Public Function Add(ByVal unTrafic As Trafic) As Short
    Return Me.List.Add(unTrafic)
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As Trafic)
    Me.InnerList.AddRange(valeurs)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
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

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unTrafic As Trafic)
    Me.List.Insert(Index, unTrafic)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As Trafic
    Get
      Return CType(Me.List.Item(Index), Trafic)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
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

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Function Contains(ByVal unTrafic As Trafic) As Boolean
    Return Me.List.Contains(unTrafic)
  End Function

End Class