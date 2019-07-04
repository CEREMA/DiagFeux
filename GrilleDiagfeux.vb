'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : GrilleDiagfeux.vb										  										'
'						Classes																														'
'							GrilleDiagfeux                                               		'
'																																							'
'******************************************************************************

Imports System.IO

Imports Grille = C1.Win.C1FlexGrid

'=====================================================================================================
'--------------------------- Classe GrilleDiagfeux --------------------------
'=====================================================================================================
Public Class GrilleDiagfeux : Inherits Grille.C1FlexGrid

  '******************************************************************************
  ' Retourne tous les champs (s�par�s par un S�parateur) d'une ligne de la grille 
  '	  sous la forme d'une chaine de caract�res  
  '******************************************************************************
  Public Function strLigneEnti�re(ByVal Row As Integer) As String
    Dim rg As Grille.CellRange = GetCellRange(Row, 0, Row, Cols.Count - 1)

    'ClipSeparators = "|;"
    strLigneEnti�re = rg.Clip

  End Function

  '******************************************************************************
  ' Indique si la ligne courante de la grille est la derni�re
  '******************************************************************************
  Public Function Derni�reLigne() As Boolean

    Derni�reLigne = (Row = Rows.Count - 1)

  End Function
  '******************************************************************************
  ' D�s�lectionner la s�lection en cours
  '******************************************************************************
  Public Sub D�s�lectionner()
    Row = -1
  End Sub
  '******************************************************************************
  ' Ligne enti�re d'une grille (partie donn�es sans ent�te �ventuel)
  '******************************************************************************
  Public Function TouteLaLigne(Optional ByVal NumLigne As Integer = -1) As Grille.CellRange
    If NumLigne = -1 Then NumLigne = Row
    TouteLaLigne = GetCellRange(NumLigne, Cols.Fixed, NumLigne, Cols.Count - 1)
  End Function

  '******************************************************************************
  ' Colonne enti�re d'une grille (partie donn�es sans ent�te �ventuel)
  '******************************************************************************
  Public Function TouteLaColonne(Optional ByVal NumCol As Integer = -1) As Grille.CellRange
    If NumCol = -1 Then NumCol = Col
    TouteLaColonne = GetCellRange(Rows.Fixed, NumCol, Rows.Count - 1, NumCol)
  End Function

  '******************************************************************************
  ' Plage de cellules de la grille contenant des donn�es(sans les ent�te)
  '******************************************************************************
  Public Function PlageDonn�es() As Grille.CellRange
    PlageDonn�es = GetCellRange(Rows.Fixed, Cols.Fixed, Rows.Count - 1, Cols.Count - 1)

  End Function

  '******************************************************************************
  ' Cellule s�lectionn�e par le click souris
  '==> Ne fonctionne que si toutes les cellules ont la m�me taille (DefaultSize)<==
  '******************************************************************************
  Public Function ExCelluleS�lectionn�e() As Grille.CellRange
    Dim ligne, colonne As Short

    Dim p As Point = Me.PointToClient(Me.MousePosition)

    'Eliminer les click en fronti�re de cellule
    If p.X Mod Cols.DefaultSize = 0 Then
    ElseIf p.Y Mod Rows.DefaultSize = 0 Then
    Else
      'Retrouver la cellule cliqu�e
      colonne = p.X \ Cols.DefaultSize
      ligne = p.Y \ Rows.DefaultSize
      If colonne >= Cols.Fixed And ligne >= Rows.Fixed Then
        If colonne < Cols.Count And ligne < Rows.Count Then
          ExCelluleS�lectionn�e = GetCellRange(ligne, colonne)
        End If
      End If
    End If

  End Function

  Public Function CelluleS�lectionn�e() As Grille.CellRange
    Dim ligne, colonne As Short
    Dim i, j As Short
    Dim p As Point = Me.PointToClient(Me.MousePosition)

    If p.Y Mod Rows.DefaultSize > 0 And p.X > 0 Then
      Dim c As Grille.Column
      Dim l As Grille.Row
      Dim lg, hg As Single

      For Each c In Me.Cols
        If c.Width = -1 Then
          lg += Me.Cols.DefaultSize
        Else
          lg += c.Width
        End If

        If lg = p.X Then
          Return Nothing
        ElseIf lg > p.X Then
          colonne = i
          Exit For
        End If
        i += 1

      Next

      For Each l In Me.Rows
        If l.Height = -1 Then
          hg += Me.Rows.DefaultSize
        Else
          hg += c.Width
        End If

        If hg > p.Y Then
          ligne = j
          Exit For
        End If
        j += 1

      Next

      Return GetCellRange(ligne, colonne)

    End If
  End Function

  '******************************************************************************
  ' D�finir la largeur de la grille en fonction du nombre de colonnes
  '******************************************************************************
  Public Sub D�finirLargeurGrille(Optional ByVal AvecAscenseur As Boolean = True)
    Dim col As Short
    Dim w As Short

    For col = 0 To Cols.Count - 1
      w += Cols(col).Width
    Next
    w += Cols.Count * 2

    If AvecAscenseur Then
      Width = w + 12 ' +12 pour l'ascenseur
    Else
      Width = w
    End If

  End Sub

  Public Function Cr�erStyle(ByVal unStyle As Grille.CellStyle, ByVal NomStyle As String, ByVal Couleur As System.Drawing.Color) As Grille.CellStyle
    If IsNothing(unStyle) Then
      Cr�erStyle = Styles.Add(NomStyle)
      Cr�erStyle.BackColor = Couleur
    Else
      Cr�erStyle = unStyle
    End If
  End Function

End Class
