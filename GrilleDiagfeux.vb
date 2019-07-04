'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
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
  ' Retourne tous les champs (séparés par un Séparateur) d'une ligne de la grille 
  '	  sous la forme d'une chaine de caractères  
  '******************************************************************************
  Public Function strLigneEntière(ByVal Row As Integer) As String
    Dim rg As Grille.CellRange = GetCellRange(Row, 0, Row, Cols.Count - 1)

    'ClipSeparators = "|;"
    strLigneEntière = rg.Clip

  End Function

  '******************************************************************************
  ' Indique si la ligne courante de la grille est la dernière
  '******************************************************************************
  Public Function DernièreLigne() As Boolean

    DernièreLigne = (Row = Rows.Count - 1)

  End Function
  '******************************************************************************
  ' Désélectionner la sélection en cours
  '******************************************************************************
  Public Sub Désélectionner()
    Row = -1
  End Sub
  '******************************************************************************
  ' Ligne entière d'une grille (partie données sans entête éventuel)
  '******************************************************************************
  Public Function TouteLaLigne(Optional ByVal NumLigne As Integer = -1) As Grille.CellRange
    If NumLigne = -1 Then NumLigne = Row
    TouteLaLigne = GetCellRange(NumLigne, Cols.Fixed, NumLigne, Cols.Count - 1)
  End Function

  '******************************************************************************
  ' Colonne entière d'une grille (partie données sans entête éventuel)
  '******************************************************************************
  Public Function TouteLaColonne(Optional ByVal NumCol As Integer = -1) As Grille.CellRange
    If NumCol = -1 Then NumCol = Col
    TouteLaColonne = GetCellRange(Rows.Fixed, NumCol, Rows.Count - 1, NumCol)
  End Function

  '******************************************************************************
  ' Plage de cellules de la grille contenant des données(sans les entête)
  '******************************************************************************
  Public Function PlageDonnées() As Grille.CellRange
    PlageDonnées = GetCellRange(Rows.Fixed, Cols.Fixed, Rows.Count - 1, Cols.Count - 1)

  End Function

  '******************************************************************************
  ' Cellule sélectionnée par le click souris
  '==> Ne fonctionne que si toutes les cellules ont la même taille (DefaultSize)<==
  '******************************************************************************
  Public Function ExCelluleSélectionnée() As Grille.CellRange
    Dim ligne, colonne As Short

    Dim p As Point = Me.PointToClient(Me.MousePosition)

    'Eliminer les click en frontière de cellule
    If p.X Mod Cols.DefaultSize = 0 Then
    ElseIf p.Y Mod Rows.DefaultSize = 0 Then
    Else
      'Retrouver la cellule cliquée
      colonne = p.X \ Cols.DefaultSize
      ligne = p.Y \ Rows.DefaultSize
      If colonne >= Cols.Fixed And ligne >= Rows.Fixed Then
        If colonne < Cols.Count And ligne < Rows.Count Then
          ExCelluleSélectionnée = GetCellRange(ligne, colonne)
        End If
      End If
    End If

  End Function

  Public Function CelluleSélectionnée() As Grille.CellRange
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
  ' Définir la largeur de la grille en fonction du nombre de colonnes
  '******************************************************************************
  Public Sub DéfinirLargeurGrille(Optional ByVal AvecAscenseur As Boolean = True)
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

  Public Function CréerStyle(ByVal unStyle As Grille.CellStyle, ByVal NomStyle As String, ByVal Couleur As System.Drawing.Color) As Grille.CellStyle
    If IsNothing(unStyle) Then
      CréerStyle = Styles.Add(NomStyle)
      CréerStyle.BackColor = Couleur
    Else
      CréerStyle = unStyle
    End If
  End Function

End Class
