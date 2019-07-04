'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Formules.vb																							'
'						Module de formules mathématiques																	'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

Imports System.Math

'--------------------------- Module de formules --------------------------
Module Formules
#Region "Déclarations"
  Public Const RaySélect As Short = 5
  Public acoTolerance As Single

  Public Enum TypeInterSection
    Indifférent               ' le point d'intersection des 2 lignes peut être en dehors des 2 segments (dans leur prolongement)
    SurSegment                ' le point d'intersection des 2 lignes doit appartenir aux deux segments
    SurPremierSegment         ' le point d'intersection des 2 lignes doit appartenir au premier segment
    SurSegmentStrict          ' idem SurSegment mais en excluant les extrémités
    SurPremierSegmentStrict   ' idem SurSegmentStrict mais en excluant les extrémités
  End Enum

  Public Enum Positionnement
    Droite
    Haut
    Gauche
    Bas
  End Enum

    Public Const sngPI As Single = CType(PI, Single)

#End Region

#Region "Distance"
  '**************************************************************************************
  ' Distance entre 2 points
  '**************************************************************************************
  Public Function Distance(ByVal p1 As Drawing.PointF, ByVal p2 As Drawing.PointF) As Double

    Distance = Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)

  End Function

  '**************************************************************************************
  ' Distance entre 2 points
  '**************************************************************************************
  Public Function Distance(ByVal p1 As Drawing.Point, ByVal p2 As Drawing.Point) As Double

    Distance = Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)

  End Function

  '**************************************************************************************
  ' Distance d'un point à une droite
  '**************************************************************************************
  Public Function Distance(ByVal p As Drawing.Point, ByVal uneLigne As Ligne) As Double
    Return Distance(p, uneLigne.pA, uneLigne.pB)
  End Function
  Public Function Distance(ByVal p As Drawing.PointF, ByVal uneLigne As Ligne) As Double
    Return Distance(p, uneLigne.pAF, uneLigne.pBF)
  End Function

  '**************************************************************************************
  ' Distance d'un point à une droite déterminé par pA et pB
  '**************************************************************************************
  Public Function Distance(ByVal p As Drawing.Point, ByVal pA As Point, ByVal pB As Point) As Double
    Dim pProjeté As Point = Projection(p, pA, pB)
    Return Distance(p, pProjeté)
  End Function
  Public Function Distance(ByVal p As Drawing.PointF, ByVal pA As PointF, ByVal pB As PointF) As Double
    Dim pProjeté As PointF = Projection(p, pA, pB)
    Return Distance(p, pProjeté)
  End Function

  Public Function Distance(ByVal p As Drawing.Point, ByVal unPolyArc As PolyArc) As Double
    Dim i As Short
    Dim dist As Double = 500.0

    With unPolyArc
      For i = 1 To unPolyArc.Points.Length - 1
        dist = Min(dist, Distance(CvPointF(p), .Points(i), .Points(i - 1)))
      Next
    End With

    Return dist
  End Function
#End Region

#Region "Formules diverses"
  '**************************************************************************************
  ' Retourne le carré d'un nombre
  '**************************************************************************************
  Public Function Carré(ByVal v As Double) As Double
    Carré = v ^ 2
  End Function
  Public Function ProduitScalaire(ByVal V1 As Vecteur, ByVal V2 As Vecteur) As Double
    Return V1.X * V2.X + V1.Y * V2.Y
  End Function

  Public Function ProduitScalaire(ByVal L1 As Ligne, ByVal L2 As Ligne) As Double
    Return ProduitScalaire(New Vecteur(L1), New Vecteur(L2))
  End Function

  Public Function ProduitVectoriel(ByVal V1 As Vecteur, ByVal V2 As Vecteur) As Double
    Return V1.X * V2.Y - V2.X * V1.Y
  End Function
#End Region

#Region "AngleFormé"
  '=================================================================================================================
  ' Tous les formulent qui suivent sont en coordonnées dessin (axe des Y inversé)
  '=================================================================================================================

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur d'origine(0,0) et d'extrémité (X,Y)
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourné est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleFormé(ByVal X As Double, ByVal Y As Double, Optional ByVal SurDeuxPI As Boolean = False) As Double

    'Le système d'axe des Y est  inversé : d'où signe - pour Y 
    AngleFormé = Atan2(-Y, X)

    If AngleFormé < 0 And SurDeuxPI Then AngleFormé += 2 * PI

  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourné est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleFormé(ByVal unVecteur As Vecteur, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleFormé = AngleFormé(unVecteur.X, unVecteur.Y, SurDeuxPI)
  End Function

  '***************************************************************************************
  ' Calcul de l'angle de deux vecteurs
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourné est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleFormé(ByVal V1 As Vecteur, ByVal V2 As Vecteur, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleFormé = AngleFormé(V2) - AngleFormé(V1)
    If AngleFormé < 0 Then
      If SurDeuxPI Then
        AngleFormé += 2 * PI
      ElseIf AngleFormé <= -PI Then
        AngleFormé += 2 * PI
      End If
    Else
      If AngleFormé > PI AndAlso Not SurDeuxPI Then
        AngleFormé -= 2 * PI
      End If
    End If
  End Function

  '***************************************************************************************
  ' Calcul de l'angle orienté formé par 3 points - pSommet est le sommet de l'angle
  ' Retourne un angle compris entre ]-pi,pi]
  ' Retourne un angle compris entre [0,2pi[ si SurDeuxPi=True
  '**************************************************************************************
  Public Function AngleFormé(ByVal pSommet As PointF, ByVal p1 As PointF, ByVal p2 As PointF, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleFormé = AngleFormé(New Vecteur(pSommet, p1), New Vecteur(pSommet, p2), SurDeuxPI)
    If Not SurDeuxPI Then
      If AngleFormé > PI Then
        AngleFormé -= 2 * PI
      ElseIf AngleFormé <= -PI Then
        AngleFormé += 2 * PI
      End If
    End If
  End Function
  Public Function AngleFormé(ByVal pSommet As Point, ByVal p1 As Point, ByVal p2 As Point, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleFormé(CvPointF(pSommet), CvPointF(p1), CvPointF(p2), SurDeuxPI)
  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur 
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourné est sur [0,2pi[
  ' Si p1 et p2 sont en coordonnées dessin, l'angle retourné est dans le sens horaire
  '**************************************************************************************
  Public Function AngleFormé(ByVal p1 As Point, ByVal p2 As Point, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleFormé(New PointF(p1.X, p1.Y), New PointF(p2.X, p2.Y), SurDeuxPI)

  End Function

  '***************************************************************************************
  ' Calcul de l'angle formé par 2 droites
  ' Retourne un angle compris entre [0,pi]
  '***************************************************************************************
  Public Function AngleFormé(ByVal D1 As Ligne, ByVal D2 As Ligne) As Double
    AngleFormé = Abs(AngleFormé(D2) - AngleFormé(D1))
    If AngleFormé > PI Then AngleFormé -= PI
  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'une droite orientée
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourné est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleFormé(ByVal uneDroite As Ligne, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleFormé(uneDroite.pAF, uneDroite.pBF, SurDeuxPI)

  End Function

  '***************************************************************************************
  ' Calcul de l'angle formé par 2 points ordonnés p1 et p2
  ' Retourne un angle compris entre ]-pi,pi]
  ' Retourne un angle compris entre [0,2pi[ si SurDeuxPi=True
  '**************************************************************************************
  Public Function AngleFormé(ByVal p1 As PointF, ByVal p2 As PointF, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Dim Angle As Double

    If p1.X <> p2.X Then
      Angle = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X))
      If p2.X < p1.X Then
        ' L'angle déterminé ci-dessus appartient à ]-pi/2,pi/2[
        'Il appartient en fait  à ]pi/2,3pi/2] ou encore à un des 2 intervalles (]-pi,-pi/2[ et ]pi/2,pi])
        Select Case Sign(Angle)
          Case 1  ' 3ème quadrant
            Angle -= PI
          Case Else ' 2ème quadrant
            Angle += PI
        End Select
      End If

    Else
      ' Droite verticale
      If p2.Y > p1.Y Then
        Angle = PI / 2
      Else
        Angle = -PI / 2
      End If
    End If

    If Angle < 0 And SurDeuxPI Then Angle += 2 * PI

    Return Angle

  End Function
#End Region

#Region "Projection"
  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' p0,p1 : points déterminant la direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal p0 As Point, ByVal p1 As Point) As Point

    Projection = Projection(pAprojeter, pOrigine, CType(AngleFormé(p0, p1), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' p0,p1 : points déterminant la direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal p0 As PointF, ByVal p1 As PointF) As PointF

    Projection = Projection(pAprojeter, pOrigine, AngleFormé(p0, p1))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine et pFinal
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal pFinal As Point) As Point

    Projection = Projection(pAprojeter, pOrigine, CType(AngleFormé(pOrigine, pFinal), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine et pFinal
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal pFinal As PointF) As PointF

    Projection = Projection(pAprojeter, pOrigine, AngleFormé(pOrigine, pFinal))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' unVecteur : Vecteur de direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal unVecteur As Vecteur) As Point
    Projection = Projection(pAprojeter, pOrigine, CType(AngleFormé(unVecteur), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' unAngle : Angle de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal unAngle As Single) As Point
    Return Point.Ceiling(Projection(CvPointF(pAprojeter), CvPointF(pOrigine), unAngle))
  End Function

  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal unAngle As Single) As PointF

    'Angle entre la droite formée par les 2 points et la droite de projection
    Dim unAngleProjection As Single = AngleFormé(pOrigine, pAprojeter) - unAngle
    Dim Dist As Single = Distance(pOrigine, pAprojeter) * Cos(unAngleProjection)
    Return PointPosition(pOrigine, Dist, unAngle)

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite uneDroite
  ' unVecteur : Vecteur de direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal uneDroite As Ligne) As Point
    Return Projection(pAprojeter, uneDroite.pA, uneDroite.pB)
  End Function
  Public Function Projection(ByVal pAprojeter As PointF, ByVal uneDroite As Ligne) As PointF
    Return Projection(pAprojeter, uneDroite.pAF, uneDroite.pBF)
  End Function

  '******************************************************************************
  '******************************************************************************
  Public Function ProjectionMêmeSens(ByVal pSouris As Point, ByVal p2 As Point, ByVal AngleSegment As Single) As Point
    'Vecteur de norme quelconque de même sens que P1P0 et d'origine P2
    Dim V1 = New Vecteur(p2, PointPosition(p2, AngleSegment))

    'Projection de la souris sur V1
    Dim pProjeté As Point = Projection(pSouris, p2, AngleSegment)
    'Vecteur de même direction mais pas forcément de même sens que V1, correspondant au point projeté depuis la position de la souris sur V1
    Dim V2 = New Vecteur(p2, pProjeté)

    If ProduitScalaire(V1, V2) > 0 Then Return pProjeté

  End Function
#End Region

#Region "Intersections"
  Public Function intersect(ByVal xA As Single, ByVal yA As Single, ByVal xB As Single, ByVal yB As Single, _
      ByVal xC As Single, ByVal yC As Single, ByVal xD As Single, ByVal yD As Single, ByVal TypeIntersect As TypeInterSection) As PointF

    Dim X1, Y1, X2, Y2, X3, Y3 As Single
    Dim x As Single, y As Single
    Dim OK As Boolean

    X1 = xB - xA
    Y1 = yB - yA
    X2 = xC - xD
    Y2 = yC - yD
    X3 = xC - xA
    Y3 = yC - yA

    '   cette routine détermine les coordonnées éventuelles x et y 
    '   de l'intersection  de 2 segments AB et CD                    

    Dim det1, lambda, mu As Single
    det1 = determ(X1, X2, Y1, Y2)

    If det1 <> 0.0 Then ' les 2 segments ne sont pas  colinéaires 

      lambda = determ(X3, X2, Y3, Y2) / det1
      mu = determ(X1, X3, Y1, Y3) / det1


      Select Case TypeIntersect
        Case TypeInterSection.Indifférent
          OK = True
        Case TypeInterSection.SurSegment
          'OK = appart(lambda, 0.0, 1.0) And appart(mu, 0.0, 1.0)
          OK = appart(lambda, -0.02, 1.02) And appart(mu, -0.02, 1.02)
        Case TypeInterSection.SurPremierSegment
          OK = appart(lambda, 0.0, 1.0)
        Case TypeInterSection.SurSegmentStrict
          OK = appart_strict(lambda, 0.0, 1.0) And appart(mu, 0.0, 1.0)
        Case TypeInterSection.SurPremierSegmentStrict
          OK = appart_strict(lambda, 0.0, 1.0)
      End Select

      If OK Then
        x = xA + lambda * (xB - xA)
        y = yA + lambda * (yB - yA)
        Return New PointF(x, y)
      End If
    End If

    'sinon pas d'intersection 

  End Function

  Public Function intersect(ByVal pa As PointF, ByVal pb As PointF, ByVal pc As PointF, ByVal pd As PointF, ByVal TypeIntersect As TypeInterSection) As PointF
    Dim X, Y As Single
    Return intersect(pa.X, pa.Y, pb.X, pb.Y, pc.X, pc.Y, pd.X, pd.Y, TypeIntersect)

  End Function

  Public Function intersect(ByVal pa As Point, ByVal pb As Point, ByVal pc As Point, ByVal pd As Point, ByVal TypeIntersect As TypeInterSection) As Point
    Dim p As PointF = intersect(New PointF(pa.X, pa.Y), New PointF(pb.X, pb.Y), New PointF(pc.X, pc.Y), New PointF(pd.X, pd.Y), TypeIntersect)
    If Not p.IsEmpty Then Return CvPoint(p)

  End Function

  Public Function intersect(ByVal L1 As Ligne, ByVal L2 As Ligne, Optional ByVal TypeIntersect As TypeInterSection = TypeInterSection.SurSegment) As PointF

    Return intersect(L1.pAF, L1.pBF, L2.pAF, L2.pBF, TypeIntersect)

  End Function

  Private Function determ(ByVal a As Single, ByVal b As Single, ByVal c As Single, ByVal d As Single) As Single
    ' calcul d'un déterminant 
    Return a * d - b * c
  End Function

  Private Function appart(ByVal x As Single, ByVal y As Single, ByVal z As Single) As Boolean
    ' cette routine détermine si x€[y,z] 
    Return (y - x) * (x - z) >= 0.0

  End Function


  Private Function appart_strict(ByVal a As Single, ByVal b As Single, ByVal c As Single) As Boolean
    ' a € ]b,c[ 
    Return (b - a) * (a - c) > 0.0
  End Function
#End Region

#Region "Conversions"
  Public Function CvPointF(ByVal p As Point) As PointF
    Return Point.op_Implicit(p)
  End Function

  Public Function CvPoint(ByVal p As PointF) As Point
    Return Point.Round(p)
  End Function

  Public Function CvPoint(ByVal uneTaille As Size) As Point
    With uneTaille
      Return New Point(.Width, .Height)
    End With
  End Function

  Public Function CvTabPointF(ByVal tabPoint As Point()) As PointF()
    Dim i As Short
    Dim tabPointF(tabPoint.Length - 1) As PointF

    For i = 0 To tabPoint.Length - 1
      tabPointF(i) = CvPointF(tabPoint(i))
    Next

    Return tabPointF

  End Function

  Public Function CvTabPoint(ByVal tabPointF As PointF()) As Point()
    Dim i As Short
    Dim tabPoint(tabPointF.Length - 1) As Point

    For i = 0 To tabPointF.Length - 1
      tabPoint(i) = CvPoint(tabPointF(i))
    Next

    Return tabPoint

  End Function

  Public Function EqvRadian(ByVal unAngle As Single, Optional ByVal InverserSens As Boolean = True) As Single
    Return CvAngleRadians(unAngle, InverserSens:=InverserSens)
  End Function

  Public Function CvTaille(ByVal uneTaille As SizeF) As Size
    Return New Size(Math.Round(uneTaille.Width), Math.Round(uneTaille.Height))
  End Function

  Public Function CvTaillePlus(ByVal uneTaille As SizeF) As Size
    Return New Size(Math.Ceiling(uneTaille.Width), Math.Ceiling(uneTaille.Height))
  End Function
  Public Function CvTailleMoins(ByVal uneTaille As SizeF) As Size
    Return New Size(Math.Floor(uneTaille.Width), Math.Floor(uneTaille.Height))
  End Function


  '*******************************************************************************************************************
  'Conversion d'un angle de degrés en radians 
  ' Retourne une valeur comprise entre ]-pi et pi]
  ' InverserSens : Indique s'il faut inverser le sens de rotation du système angulaire
  '*******************************************************************************************************************
  Public Function CvAngleRadians(ByVal AngleEnDegrés As Single, Optional ByVal InverserSens As Boolean = False) As Single
    'If AngleEnDegrés > 360 Then AngleEnDegrés -= 360

    Dim unAngle As Single
    If InverserSens Then
      'Convertir du sens horaire au sens trigonométrique ou inversement
      unAngle = 360 - AngleEnDegrés
    Else
      unAngle = AngleEnDegrés
    End If

    If unAngle > 180 Then unAngle -= 360

    'Convertir en radians
    Return unAngle * Math.PI / 180
  End Function

  '*******************************************************************************************************************
  'Conversion d'un angle de radians en degrés
  ' Retourne une valeur comprise entre 0 et 360
  ' InverserSens : Indique s'il faut inverser le sens de rotation du système angulaire
  '*******************************************************************************************************************
  Public Function CvAngleDegrés(ByVal AngleEnRadians As Single, Optional ByVal InverserSens As Boolean = True, Optional ByVal SurDeuxPi As Boolean = True) As Single
    'On ne change pas le sens de rotation quand l'angle est déjà dans le sens horaire (angle obtenu à partir de points en coordonnées Windows)
    Dim unAngle As Single = AngleEnRadians Mod 2 * PI
    If unAngle < 0 Then unAngle += 2 * PI

    'Convertir du sens trigonométrique au sens horaire ou inversement
    If InverserSens Then unAngle = (2 * PI - unAngle) Mod 2 * PI

    If Not SurDeuxPi And unAngle > PI Then unAngle -= 2 * PI

    'Convertir en degrés
    Return unAngle * 180 / Math.PI
  End Function
#End Region

#Region "Position d'un point à une distance donnée"
  '*******************************************************************************************************************
  'Retourne un point à une distance donnée du point Origine dans la direction Angle
  ' Angle est fourni en degrés
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Dist As Single, ByVal AngleEnDegrés As Single, ByVal SensHoraire As Boolean) As Point
    Dim Alpha As Single

    Alpha = CvAngleRadians(AngleEnDegrés, SensHoraire)
    'Le système d'axe des Y est  inversé : d'où prendre l'angle opposé

    Return PointPosition(pOrigine, Dist, -Alpha)

  End Function

  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Dist As Single, ByVal AngleEnDegrés As Single, ByVal SensHoraire As Boolean) As PointF
    Dim Alpha As Single

    Alpha = CvAngleRadians(AngleEnDegrés, SensHoraire)
    'Le système d'axe des Y est  inversé : d'où prendre l'angle opposé

    Return PointPosition(pOrigine, Dist, -Alpha)

  End Function

  '*******************************************************************************************************************
  'Retourne un point à une distance donnée du point Origine dans la direction Alpha déterminée par les points p1 et p2
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Dist As Single, ByVal p1 As Point, ByVal p2 As Point) As Point
    Dim Alpha As Single = AngleFormé(p1, p2)
    Return PointPosition(pOrigine, Dist, Alpha)

  End Function

  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Dist As Single, ByVal p1 As PointF, ByVal p2 As PointF) As PointF
    Dim Alpha As Single = AngleFormé(p1, p2)
    Return PointPosition(pOrigine, Dist, Alpha)

  End Function

  '*******************************************************************************************************************
  'Retourne un point à une distance donnée du point Origine dans la direction Alpha
  ' Alpha est en radians
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Dist As Single, ByVal Alpha As Single) As Point
    'Dim xM, yM As Integer
    'xM = pOrigine.X + Dist * Cos(Alpha)
    'yM = pOrigine.Y + Dist * Sin(Alpha)
    'Return New Point(xM, yM)
    Return Translation(pOrigine, New Vecteur(Dist, Alpha))
  End Function

  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Dist As Single, ByVal Alpha As Single) As PointF
    'Dim xM, yM As Single
    'xM = pOrigine.X + Dist * Cos(Alpha)
    'yM = pOrigine.Y + Dist * Sin(Alpha)
    'Return New PointF(xM, yM)
    Return Translation(pOrigine, New Vecteur(Dist, Alpha))
  End Function

  '*******************************************************************************************************************
  'Retourne un point à une distance 'infinie' du point Origine dans la direction Alpha
  ' Alpha est en radians
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    ' On choisit une valeur fictive de  500 comme longueur du segment formé par pOrigine et le point cherché
    Return PointPosition(pOrigine, 500, Alpha)
  End Function
  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Alpha As Single) As PointF
    ' On choisit une valeureur fictive de  500000 comme longueur du segment formé par pOrigine et le point cherché
    Return PointPosition(pOrigine, 500000, Alpha)
  End Function

  '*******************************************************************************************************************
  'Retourne un point sur la droite passant par 2 points à un coefficient donné de pCentre
  ' le point M est tel que vectoriellement OM = Lambda * OI - O est pCentre et I pIntermédiaire
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pCentre As Point, ByVal pIntermédiaire As Point, ByVal Lambda As Single) As Point
    Dim xM, yM As Integer
    xM = Lambda * pIntermédiaire.X + (1 - Lambda) * pCentre.X
    yM = Lambda * pIntermédiaire.Y + (1 - Lambda) * pCentre.Y
    Return New Point(xM, yM)
  End Function

#End Region

#Region "Intersection cercle/cercle cercle/droite"
  '******************************************************************************************
  'Soient un point O et 2 droites CD et FE : rechercher un point H à une distance d de O
  'tel que l'intersection I de FH avec CD respecte 
  '   - les vecteurs CD et CI soient de même sens
  '   - le point I ne franchit pas FE
  ' H est tangent au cercle de centre O et de rayon d 
  '*******************************************************************************************
  Public Function PointTangence(ByVal pO As PointF, ByVal Rayon As Single, ByVal LigneRéFérence As Ligne, ByVal LigneAdverse As Ligne) As PointF
    Dim pF As PointF = LigneAdverse.pAF
    Dim pH As PointF

    Dim xF, yF As Single

    'Pour simplifier les calculs, on fait une translation 
    xF = pF.X - pO.X
    yF = pF.Y - pO.Y

    'Calculer un 1er point de tangence et Refaire la translation inverse
    pH = Translation(pO, PointTangence(xF, yF, Rayon))
    If Not pH.IsEmpty Then
      'Intersection du futur raccord avec la ligne de référence
      Dim pInter As PointF = intersect(LigneRéFérence, New Ligne(pF, pH), Formules.TypeInterSection.Indifférent)
      Dim pAF As PointF = LigneRéFérence.pAF
      Dim pbF As PointF = LigneRéFérence.pBF
      If Abs(AngleFormé(pAF, pbF, pInter)) < 0.1 Then
        'Le nouveau point ne doit pas changer le sens de la ligne de référence
        pH = Translation(pO, PointTangence(xF, yF, Rayon, PremierAppel:=False))

      ElseIf Distance(pAF, pInter) > Distance(pAF, LigneAdverse) Then
        ' Le nouveau point de la ligne de référence ne doit pas franchir la ligne adverse
        pH = Translation(pO, PointTangence(xF, yF, Rayon, PremierAppel:=False))
      End If

    End If

    Return pH

  End Function

  Public Function PointTangence(ByVal xF As Single, ByVal yF As Single, ByVal Rayon As Single, Optional ByVal PremierAppel As Boolean = True) As PointF
    Static xH, yH As Single

    If xF = 0 Then
      If PremierAppel Then
        yH = Rayon ^ 2 / yF
        xH = Rayon
      Else
        xH = -Rayon
      End If

    Else
      Dim a, bPrime, c As Single
      Dim RCArré As Double = Rayon ^ 2
      a = 1 + (yF / xF) ^ 2
      bPrime = -RCArré * yF / xF ^ 2
      c = (RCArré / xF) ^ 2 - RCArré
      yH = SolutionEquationDegré2(a, bPrime, c, PremierAppel)
      If Single.IsNaN(yH) Then Return New PointF
      xH = (RCArré - yF * yH) / xF
    End If

    Return New PointF(xH, yH)

  End Function

  '*******************************************************************************************************************
  ' Déterminer le point situé sur une droite à une distance Dist d'un point donné 
  ' pDonné : point donné
  ' Dist : Distance entre le point donné et le point à déterminer sur la droite
  ' pOrigine : Point origine de la droite
  ' Alpha : Angle de la droite
  'Il s'agit de résoudre le système d'équations
  ' (xM-xO)² + (yM-yO)² = Dist²
  ' xM = (xA - xO) * cos(Alpha)
  ' yM = (yA - yO) * cos(Alpha)
  ' O : pDonné
  ' A : pOrigineDroite
  ' M: Point recherché sur la droite d'origine A de direction Alpha
  ' En posant x1=xA-xO et y1=yA-yO On aboutit à l'équation : D² + 2(x1.cos alpha + y1.sin alpha)D + x1²+y1²-Dist²
  '*******************************************************************************************************************

  Public Function PointSurDroiteADistancePointDonné(ByVal pDonné As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    Dim pCherché As Point
    Dim d1 As Single
    Dim Beta As Single

    Select Case Alpha
      Case 0
        Beta = PI
      Case PI
        Beta = 0
      Case Else
        Beta = Alpha
    End Select

    d1 = SolutionEquationDegré2(pDonné, Dist, pOrigine, Alpha)

    If Not Single.IsNaN(d1) Then

      pCherché = PointPosition(pOrigine, d1, Alpha)
      If Sign(Beta) <> Sign(AngleFormé(pOrigine, pCherché)) Then
        d1 = SolutionEquationDegré2(pDonné, Dist, pOrigine, Alpha, PremierAppel:=False)
        pCherché = PointPosition(pOrigine, d1, Alpha)
      End If
      Return pCherché
    End If

  End Function

  Public Function PointSurCercle(ByVal pDonné As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    Dim pCherché As Point
    Dim d1 As Single

    d1 = SolutionEquationDegré2(pDonné, Dist, pOrigine, Alpha)

    If Not Single.IsNaN(d1) Then
      pCherché = PointPosition(pOrigine, d1, Alpha)
      If Sign(Alpha) <> Sign(AngleFormé(pOrigine, pCherché)) Then
        d1 = SolutionEquationDegré2(pDonné, Dist, pOrigine, Alpha, PremierAppel:=False)
        pCherché = PointPosition(pOrigine, d1, Alpha)
      End If
      If Distance(pCherché, pOrigine) > 0 Then Return pCherché
    End If

  End Function

  Public Function SolutionEquationDegré2(ByVal pDonné As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single, Optional ByVal PremierAppel As Boolean = True) As Single
    Static b As Single
    Static Déterminant As Single

    If PremierAppel Then
      Dim a As Single = 1.0
      'Définir x1 et y1
      Dim x1 As Integer = pOrigine.X - pDonné.X
      Dim y1 As Integer = pOrigine.Y - pDonné.Y
      'coefficient b' de l'équation
      b = x1 * Cos(Alpha) + y1 * Sin(Alpha)
      'coefficient c de l'équation
      Dim c As Single = x1 ^ 2 + y1 ^ 2 - Dist ^ 2

      'Déterminant simplifié car b est pair
      Déterminant = b ^ 2 - c

      If Déterminant < 0 Then
        Return Single.NaN
      Else
        Return -b + Sqrt(Déterminant)
      End If

    Else
      Return -b - Sqrt(Déterminant)
    End If

  End Function

  Public Function SolutionEquationDegré2(ByVal a As Single, ByVal bPrime As Single, ByVal c As Single, Optional ByVal PremierAppel As Boolean = True) As Single
    Static Déterminant As Single

    If PremierAppel Then
      'Déterminant simplifié car b est pair
      Déterminant = (bPrime ^ 2 - a * c)

      If Déterminant < 0 Then
        Return Single.NaN
      Else
        Return (-bPrime + Sqrt(Déterminant)) / a
      End If

    Else
      Return (-bPrime - Sqrt(Déterminant)) / a
    End If

  End Function

  '*********************************************************************************************
  ' Intersection d'un cercle et d'une droite
  '*********************************************************************************************
  Public Function IntersectionCercleDroite(ByVal pCentre As PointF, ByVal R As Single, ByVal uneLigne As Ligne, Optional ByVal PremierAppel As Boolean = True) As PointF
    Dim pProjeté As PointF = Projection(pCentre, uneLigne)
    Static pCherché As PointF

    Select Case Sign(Distance(pProjeté, pCentre) - R)
      Case 0
        'droite tangente en P
        Return pProjeté
      Case -1
        ' droite sécante
        If PremierAppel Then
          Dim AH As Single = Math.Sqrt(R ^ 2 - Carré(Distance(pCentre, pProjeté)))
          pCherché = PointPosition(pProjeté, AH, AngleFormé(uneLigne))
        Else
          pCherché = Symétrique(pCherché, pProjeté)
        End If
        Return pCherché
    End Select

  End Function

  Public Function IntersectionCercleDroite(ByVal pCentre As Point, ByVal R As Single, ByVal uneLigne As Ligne, Optional ByVal PremierAppel As Boolean = True) As Point
    Return CvPoint(IntersectionCercleDroite(CvPointF(pCentre), R, uneLigne, PremierAppel))
  End Function

  '*********************************************************************************************
  ' Intersection de 2 cercles de ce centres p1,p0 et de rayons R1,R0
  '*********************************************************************************************
  Public Function IntersectionCercles(ByVal p1 As Point, ByVal p0 As Point, ByVal R1 As Single, ByVal R0 As Single, Optional ByVal PremierAppel As Boolean = True) As Point
    Return Point.Ceiling(IntersectionCercles(CvPointF(p1), CvPointF(p0), R1, PremierAppel))
  End Function

  Public Function IntersectionCercles1(ByVal p1 As PointF, ByVal p0 As PointF, ByVal R1 As Single, ByVal R0 As Single, Optional ByVal PremierAppel As Boolean = True) As PointF
    Dim x1, x0, y1, y0 As Single
    Dim x As Single
    Dim y As Single
    Dim a, bPrime, c As Single

    Static pCherché As PointF
    If PremierAppel Then
      pCherché = Nothing
    ElseIf pCherché.IsEmpty Then
      Return pCherché
    End If

    x1 = p1.X
    y1 = p1.Y
    x0 = p0.X
    y0 = p0.Y

    ' R1² = x1² +x² - 2x1*x + y1² + y² - 2y1*y
    ' R0² = x0² +x² - 2x0*x + y0² + y² - 2y0*y
    ' La résolution du système aboutit par soustraction à
    ' R1² - R0² = x1² - x0² + 2x(x0-x1) + y1² - y0² +2y(y0-y1)

    If y0 = y1 Then
      ' on obtient : x =((R1² - R0²)- (x1² - x0²)) / 2(x0 - x1)
      If x0 <> x1 Then
        x = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2) / 2 / (x0 - x1)
        'Puis  y² - 2y1*y +  (x - x1)² + y1² - R1²  = 0
        a = 1
        bPrime = -y1
        c = (x - x1) ^ 2 + y1 ^ 2 - R1 ^ 2
        If PremierAppel Then
          y = SolutionEquationDegré2(a, bPrime, c)
          If Not Single.IsNaN(y) Then
            pCherché = New PointF(x, y)
            Return pCherché
          End If

        Else
          pCherché.Y = SolutionEquationDegré2(a, bPrime, c, PremierAppel:=False)
        End If

      Else
        ' Cercles concentriques
      End If

    Else
      ' Cas général
      x1 = p1.X - p0.X
      y1 = p1.Y - p0.Y

      Dim a1, b1, c1 As Single

      a1 = 2 * x1
      b1 = 2 * y1
      c1 = x1 ^ 2 + y1 ^ 2 - R1 ^ 2 + R0 ^ 2

      a = (a1 ^ 2 + b1 ^ 2)
      bPrime = -a1 * c1
      c = c1 ^ 2 - b1 ^ 2 * R0 ^ 2

      If PremierAppel Then
        x = SolutionEquationDegré2(a, bPrime, c)
        If Not Single.IsNaN(x) Then
          y = (c1 - a1 * x) / b1
          pCherché = New PointF(x, y)
        End If
      Else
        pCherché.X = SolutionEquationDegré2(a, bPrime, c, PremierAppel:=False)
        pCherché.Y = (c1 - a1 * pCherché.X) / b1
      End If

      Return Translation(pCherché, p0)

    End If

  End Function

  Public Function IntersectionCercles(ByVal p1 As PointF, ByVal p0 As PointF, ByVal R1 As Single, ByVal R0 As Single, Optional ByVal PremierAppel As Boolean = True) As PointF
    Dim x1, x0, y1, y0 As Single
    Dim x As Single
    Dim y As Single
    Dim a, b, c As Single

    Static pCherché As PointF
    If PremierAppel Then
      pCherché = Nothing
    ElseIf pCherché.IsEmpty Then
      Return pCherché
    End If

    x1 = p1.X
    y1 = p1.Y
    x0 = p0.X
    y0 = p0.Y

    ' R1² = x1² +x² - 2x1*x + y1² + y² - 2y1*y
    ' R0² = x0² +x² - 2x0*x + y0² + y² - 2y0*y
    ' La résolution du système aboutit par soustraction à
    ' R1² - R0² = x1² - x0² + 2x(x0-x1) + y1² - y0² +2y(y0-y1)

    If y0 = y1 Then
      ' on obtient : x =((R1² - R0²)- (x1² - x0²)) / 2(x0 - x1)
      If x0 <> x1 Then
        x = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2) / 2 / (x0 - x1)
        'Puis  y² - 2y1*y +  (x - x1)² + y1² - R1²  = 0
        a = 1
        b = -y1
        c = (x - x1) ^ 2 + y1 ^ 2 - R1 ^ 2
        If PremierAppel Then
          y = SolutionEquationDegré2(a, b, c)
          If Not Single.IsNaN(y) Then
            pCherché = New PointF(x, y)
          End If

        Else
          pCherché.Y = SolutionEquationDegré2(a, b, c, PremierAppel:=False)
        End If

      Else
        ' Cercles concentriques
      End If

    Else
      ' Cas général
      Dim N As Single = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2 + y0 ^ 2 - y1 ^ 2) / 2 / (y0 - y1)
      Dim K As Single = (x0 - x1) / (y0 - y1)
      a = K ^ 2 + 1
      b = (y0 - N) * K - x0
      c = x0 ^ 2 + y0 ^ 2 + N ^ 2 - R0 ^ 2 - 2 * y0 * N

      If PremierAppel Then
        x = SolutionEquationDegré2(a, b, c)
        If Not Single.IsNaN(x) Then
          y = N - K * x
          pCherché = New PointF(x, y)
        End If

      Else
        pCherché.X = SolutionEquationDegré2(a, b, c, PremierAppel:=False)
        pCherché.Y = N - K * pCherché.X
      End If

      Return pCherché

    End If

  End Function


#End Region

  '*******************************************************************************************************
  'Créer un arc raccordant 2 segments de ligne : Ligne1 et Ligne2
  'Ligne1 et Ligne2 sont ajustés par rapport à l'arc de raccordement calculé
  'R : Rayon de l'arc en unités réelles,  diminué récursivement si le raccord initial n'est pas posible
  '*******************************************************************************************************
  Public Function CréerRaccord(ByVal Ligne1 As Ligne, ByVal Ligne2 As Ligne, Optional ByVal R As Single = 3.0, Optional ByVal unePlume As Pen = Nothing) _
          As Arc

    Try

      'Sens =1 ou -1 selon sens trigo ou horaire
      Dim Sens As Short = Sign(AngleFormé(Ligne1.pBF, Ligne1.pAF, Ligne2.pBF))
      Dim Angle1 As Single = AngleFormé(Ligne1)
      Dim Angle2 As Single = AngleFormé(Ligne2)
      Dim CoordEntier As Boolean = False ' Not Ligne1.Réel

      'Point d'intersection des 2 segments
      Dim p As PointF = intersect(Ligne2, Ligne1)
      Dim Rayon As Single = Echelle * R ' 3 m, puis éventuellement en décroissant jusqu'à 0.5m

      'Rechercher le centre de l'arc 
      'Déterminer la droite P1P11 parallèle à Ligne1 à la distance 'Rayon' de celleci et de longueur 'infinie'
      Dim P1 As PointF = PointPosition(p, Rayon, Angle1 + PI / 2 * Sens)
      Dim p11 As PointF = PointPosition(P1, Angle1)
      'Déterminer la droite P2P12 parallèle à Ligne2 à la distance 'Rayon' de celleci et de longueur 'infinie'
      Dim P2 As PointF = PointPosition(p, Rayon, Angle2 - PI / 2 * Sens)
      Dim p22 As PointF = PointPosition(P2, Angle2)
      'Le centre de l'arc est l'intersection des 2 droites
      Dim pM As PointF = intersect(New Ligne(p11, P1, Nothing), New Ligne(p22, P2, Nothing), TypeInterSect:=Formules.TypeInterSection.Indifférent)

      If Not pM.IsEmpty Then

        'Déterminer les points de tangence avec l'arc
        Dim PT1 As PointF = Projection(pM, Ligne1)
        Dim PT2 As PointF = Projection(pM, Ligne2)
        If Not Ligne1.PtSurSegment(PT1) Then
          If R > 0.5 Then ' Sinon pas de raccord possible : les 2 segments initiaux restent inchangés
            Return CréerRaccord(Ligne1, Ligne2, R - 0.5, unePlume)
          End If
        ElseIf Not Ligne2.PtSurSegment(PT2) Then
          If R > 0.5 Then
            Return CréerRaccord(Ligne1, Ligne2, R - 0.5, unePlume)
          End If
        Else

          'Construire l'arc de raccordement
          Angle1 = CvAngleDegrés(AngleFormé(pM, PT1), InverserSens:=False)
          Angle2 = CvAngleDegrés(AngleFormé(pM, PT2), InverserSens:=False)
          Dim AngleDépart As Single
          Dim AngleFinal As Single
          If Sens = -1 Then ' sens horaire
            AngleDépart = Angle1
            AngleFinal = Angle2
          Else
            AngleDépart = Angle2
            AngleFinal = Angle1
          End If
          Dim AngleBalayage As Single = AngleBalayageArc(AngleDépart, AngleFinal)
          Dim plumeFuschia = New Pen(Color.Fuchsia)
          If IsNothing(unePlume) Then unePlume = plumeFuschia
          If AngleBalayage <> 0 Then
            'Couper les 2 lignes à leur points de tangence avec l'arc
            PT1 = PointPosition(pM, Rayon, CvAngleRadians(Angle1))
            PT2 = PointPosition(pM, Rayon, CvAngleRadians(Angle2))
            Ligne1.pAF = PT1
            Ligne2.pAF = PT2
            Return New Arc(pM, Rayon, AngleDépart, AngleBalayage, unePlume)

          End If
        End If

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Formules.CréerRaccord")

    End Try

  End Function

#Region "Clipping"
  '************************************************************************************************
  ' FONCTIONS DE CLIPPING
  '************************************************************************************************
  Public Function PointDansPicture(ByVal p As Point) As Boolean
    Return codeClip(p) = 0
    'Return Not (p.X < 0 Or p.Y < 0 Or p.X > picDessin.Width Or p.Y > picDessin.Height)
  End Function

  Public Sub AffecterLimites(ByVal picture As PictureBox)
    Dim uneTaille As Size = picture.ClientRectangle.Size
    xMaxPicture = uneTaille.Width
    yMaxPicture = uneTaille.Height
  End Sub

  '************************************************************************************************
  ' Retourne le point après éventuelt clipping en coordonnées écran
  '************************************************************************************************
  Public Function PtClippé(ByVal p As Point, ByVal pOrigine As Point, Optional ByVal CoordonnéesEcran As Boolean = True) As Point

    If Not PointDansPicture(p) Then
      clip(p, pOrigine)
    End If

    If CoordonnéesEcran Then
      Return cndpicDessin.PointToScreen(p)
    Else
      Return p
    End If

  End Function

  Private Function codeClip(ByVal p As Point) As Integer

    'En VB (v4 à v6 tout au moins) True=-1 et False=0
    ' On remultiplie par -1 chaque valeur pour faire 'ressortir' positivement les bits intéressants
    codeClip = (p.X < 0.0#) * -8 + (p.X > xMaxPicture) * -4 + (p.Y < 0.0#) * -2 + (p.Y > yMaxPicture) * -1

  End Function

  Private Function clip(ByRef p1 As Point, ByRef p2 As Point) As Boolean
    Dim c1 As Integer, c2 As Integer  ' codes binaires du clipping pour p1 et p2
    Dim dX As Double, dY As Double    ' deltaX et deltaY entre p1 et p2

    c1 = codeClip(p1)
    c2 = codeClip(p2)

    Do While c1 Or c2
      ' Un point au moins hors de la vue
      If c1 And c2 Then
        ' Sortir de la boucle : les 2 points sont hors de la Vue
        c1 = 0
        c2 = 0
        clip = True
      Else

        dX = p2.X - p1.X
        dY = p2.Y - p1.Y

        If c1 Then
          ' p1 est hors de la vue - redéfinir p1 à l'intersection de p1p2  et d'un bord
          c1 = NewCodeClip(p1, c1, dX, dY)
          ' p2 est hors de la vue - redéfinir p2 à l'intersection de p1p2  et d'un bord
        Else
          c2 = NewCodeClip(p2, c2, dX, dY)
        End If

      End If
    Loop

  End Function

  Private Function NewCodeClip(ByRef p As Point, ByVal c As Integer, ByVal dX As Double, ByVal dY As Double) As Integer

    If c And 8 Then
      'Tronquage du bord gauche
      p.Y = p.Y + dY * (-p.X) / dX
      p.X = 0.0#

    ElseIf c And 4 Then
      'Tronquage du bord droit
      p.Y = p.Y + dY * (xMaxPicture - p.X) / dX
      p.X = xMaxPicture

    ElseIf c And 2 Then
      'Tronquage du bord inférieur
      p.X = p.X + dX * (-p.Y) / dY
      p.Y = 0.0#

    ElseIf c And 1 Then
      'Tronquage du bord supérieur
      p.X = p.X + dX * (yMaxPicture - p.Y) / dY
      p.Y = yMaxPicture
    End If

    NewCodeClip = codeClip(p)

  End Function
#End Region

  Public Sub DessinerPoignée(ByVal p As Point, Optional ByVal ptCliqué As Boolean = False)
    Dim Width, Height As Integer
    Dim pScreen As Point
    Dim Taille As Short = 3
    Width = 2 * Taille
    Height = Width
    p = Point.op_Addition(p, New Size(-Taille, -Taille))

    Dim rc As New Rectangle

    Dim deltaX As Short = Taille - p.X
    If deltaX > 0 Then
      p.X += deltaX
      Width -= deltaX
    End If

    deltaX = Taille + p.X - xMaxPicture
    If deltaX > 0 Then
      Width -= deltaX
    End If

    Dim DeltaY As Short = Taille - p.Y
    If DeltaY > 0 Then
      p.Y += deltaX
      Height -= DeltaY
    End If

    DeltaY = Taille + p.Y - yMaxPicture
    If DeltaY > 0 Then
      Width -= DeltaY
    End If

    If Width > 0 And Height > 0 Then
      pScreen = cndpicDessin.PointToScreen(p)
      With rc
        .X = pScreen.X
        .Y = pScreen.Y
        .Width = Width
        .Height = Height
      End With

      If ptCliqué Then
        ControlPaint.DrawReversibleFrame(rc, Color.Gray, FrameStyle.Thick)
        '      ControlPaint.FillReversibleRectangle(rc, Color.Black)
      Else
        ControlPaint.FillReversibleRectangle(rc, Color.Cyan)
      End If

    End If

  End Sub

  '*****************************************************************************************
  'Retourne le point dans le repère de la droite d'origine pOrigine et d'orientation unAngle
  ' unAngle : angle de la branche en degrés
  '*****************************************************************************************
  Public Function ChangementRepère(ByVal pOrigine As PointF, ByVal unAngle As Single, ByVal p As PointF) As PointF
    Dim pTrans As PointF = New PointF(p.X - pOrigine.X, p.Y - pOrigine.Y)

    Return Rotation(pTrans, -CvAngleRadians(unAngle))

  End Function

  '**************************************************************************************
  'Déterminer l'angle de balayage de l'arc : les angles sont en degrés
  '**************************************************************************************
  Public Function AngleBalayageArc(ByVal AngleDépart As Single, ByVal AngleFinal As Single) As Single
    If AngleFinal < AngleDépart Then
      AngleBalayageArc = AngleFinal + (360 - AngleDépart)
    Else
      AngleBalayageArc = AngleFinal - AngleDépart
    End If

  End Function

  '***********************************************************************************************************
  'Détermine un positionnement d'écriture selon l'angle de la ligne à l'extrémité de laquelle on veut écrire
  '***********************************************************************************************************
  Public Function Portion(ByVal unAngle As Single) As Positionnement
    Select Case unAngle / PI * 4
      Case -1 To 1
        Return Positionnement.Droite
      Case -3 To -1
        Return Positionnement.Haut
      Case 3 To 4.1, -4.1 To -3
        Return Positionnement.Gauche
      Case 1 To 3
        Return Positionnement.Bas
    End Select

  End Function

  '***********************************************************************************************************
  'Détermine un l'alignement du texte selon l'angle de la ligne à l'extrémité de laquelle on veut écrire
  '***********************************************************************************************************
  Public Function AlignementTexte(ByVal unAngle As Single) As StringAlignment

    Select Case Portion(unAngle)
      Case Positionnement.Droite
        Return StringAlignment.Near
      Case Positionnement.Gauche
        Return StringAlignment.Far
      Case Else
        Return StringAlignment.Center
    End Select

  End Function

#Region "Conversions réelles - dessin"
  '**************************************************************************************
  ' Les coordonnées réelles croissent en sens inverse des coordonnées dessin (système de coord écran)
  ' Le 0,0 des coordonnées dessin coincide avec OrigineRéelle (en coordonnées réelles)
  '**************************************************************************************
  Public Function PointRéel(ByVal p As Point) As PointF
    Return PointRéel(CvPointF(p))

  End Function

  Public Function PointRéel(ByVal p As PointF) As PointF
    PointRéel = New PointF(ToRéel(p.X), ToRéel(-p.Y))

    PointRéel.X += OrigineRéelle.X
    PointRéel.Y += OrigineRéelle.Y

  End Function

  Public Function PointDessin(ByVal p As PointF) As Point
    p.X -= OrigineRéelle.X
    p.Y -= OrigineRéelle.Y

    PointDessin = New Point(ToDessin(p.X), ToDessin(-p.Y))

  End Function

  Public Function PointDessinF(ByVal p As PointF) As PointF
    p.X -= OrigineRéelle.X
    p.Y -= OrigineRéelle.Y

    Return New PointF(ToDessin(p.X), ToDessin(-p.Y))

  End Function

  Public Function DistanceRéelle(ByVal p1 As Point, ByVal p2 As Point) As Double
    Return ToRéel(Distance(p1, p2))
  End Function

  Public Function DistanceRéelle(ByVal p1 As Point, ByVal uneDroite As Ligne) As Double
    Return ToRéel(Distance(p1, uneDroite))
  End Function
  Public Function DistanceDessin(ByVal p1 As PointF, ByVal p2 As PointF) As Double
    Return ToDessin(Distance(p1, p2))
  End Function
  Public Function ToRéel(ByVal Value As Integer) As Single
    Return Value / Echelle
  End Function
  Public Function ToDessin(ByVal Value As Single) As Single
    Return Value * Echelle
  End Function

  Public ReadOnly Property Echelle() As Single
    Get
      Return cndParamDessin.Echelle
    End Get
  End Property

  Public ReadOnly Property OrigineRéelle() As PointF
    Get
      Return cndParamDessin.OrigineRéelle
    End Get
  End Property

  '**************************************************************************************
  'Déterminer la nouvelle origine réelle suite au zoom
  'L'origine réelle est le point correspondant au (0,0) du controle
  'pInvariant est le point cliqué sur le controle (centre du zoom)
  '**************************************************************************************
  Public Function DéterminerNewOrigineRéelle(ByVal pInvariant As Point, ByVal ZoomPlus As Boolean) As ParamDessin
    Dim pInvariantRéel As PointF = PointRéel(pInvariant)
    Dim uneEchelle As Single = cndParamDessin.Echelle
    Dim uneOrigine As PointF

    If ZoomPlus Then
      uneEchelle *= 2
    Else
      uneEchelle /= 2
    End If

    Dim unRectangle As Rectangle = cndpicDessin.ClientRectangle

    'La formule qui suit définit que le point invariant reste en même position dans le picturebox
    'OrigineRéelle.X = pInvariantRéel.X - pInvariant.X / uneEchelle
    'OrigineRéelle.Y = pInvariantRéel.Y + pInvariant.Y / uneEchelle

    'La formule qui suit définit que le point invariant passe au centre du picturebox
    Dim pCentre As New Point(cndpicDessin.Width / 2, cndpicDessin.Height / 2)
    uneOrigine.X = pInvariantRéel.X - pCentre.X / uneEchelle
    uneOrigine.Y = pInvariantRéel.Y + pCentre.Y / uneEchelle

    Return New ParamDessin(uneEchelle, uneOrigine)

  End Function

  '**************************************************************************************
  'Déterminer la nouvelle origine réelle suite au zoom
  'L'origine réelle est le point correspondant au (0,0) du controle
  'pInvariant est le point cliqué sur le controle (centre du zoom)
  '**************************************************************************************
  Public Function DéterminerNewOrigineRéellePAN(ByVal pTrans As Point) As ParamDessin
    Dim pTransRéel As PointF
    Dim uneEchelle As Single = cndParamDessin.Echelle
    Dim uneOrigine As PointF = cndParamDessin.OrigineRéelle

    With pTrans
      pTransRéel.X = .X / uneEchelle
      pTransRéel.Y = -.Y / uneEchelle
    End With

    uneOrigine = Translation(uneOrigine, pTransRéel)

    Return New ParamDessin(uneEchelle, uneOrigine)

  End Function

  Public Function NumQuadrant(ByVal Alpha As Double) As Short
    If Sign(Sin(Alpha)) >= 0 Then
      If Sign(Cos(Alpha)) >= 0 Then
        NumQuadrant = 0
      Else
        NumQuadrant = 1
      End If
    Else
      If Sign(Cos(Alpha)) >= 0 Then
        NumQuadrant = 3
      Else
        NumQuadrant = 2
      End If
    End If

    Return NumQuadrant

  End Function

  '**************************************************************************************
  'Créer un Arc en coordonnées dessin à partir des propriétés d'un arc en coordonnées réelles
  'AngleDépart et AngleFinal sont en degrés, mais dans le sens trigo
  '**************************************************************************************
  Public Function ArcDessin(ByVal pCentre As PointF, ByVal Rayon As Single, ByVal AngleDépart As Single, ByVal AngleFinal As Single) As Arc
    Dim AngleBalayage As Single

    'Conversion en sens horaire

    Dim Angle1 As Single = 360 - AngleFinal
    Dim Angle2 As Single = 360 - AngleDépart

    AngleBalayage = AngleBalayageArc(Angle1, Angle2)

    Return New Arc(PointDessin(pCentre), Rayon * Echelle, Angle1, AngleBalayage)

  End Function
#End Region


#Region "Géométrie"
  '**************************************************************************
  ' Retourne le point Milieu des points p1 et p2
  '**************************************************************************
  Public Function Milieu(ByVal p1 As PointF, ByVal p2 As PointF) As PointF
    Dim x1 As Single = (p1.X + p2.X) / 2
    Dim y1 As Single = (p1.Y + p2.Y) / 2
    Milieu = New PointF(x1, y1)
  End Function

  Public Function Milieu(ByVal p1 As Point, ByVal p2 As Point) As Point
    Dim x1 As Single = (p1.X + p2.X) / 2
    Dim y1 As Single = (p1.Y + p2.Y) / 2
    Milieu = New Point(x1, y1)
  End Function

  '********************************************************************************************************************
  ' Retourne le symétrique de p1 par rapport à p2
  '********************************************************************************************************************
  Public Function Symétrique(ByVal p1 As Point, ByVal p2 As Point) As Point
    Return New Point(2 * p2.X - p1.X, 2 * p2.Y - p1.Y)
  End Function
  Public Function Symétrique(ByVal p1 As PointF, ByVal p2 As PointF) As PointF
    Return New PointF(2 * p2.X - p1.X, 2 * p2.Y - p1.Y)
  End Function
#Region "Rotation"
  '********************************************************************************************************************
  ' Retourne le point transformé de p  dans la rotation de centre (0,0) et d'angle Alpha
  '********************************************************************************************************************
  Public Function Rotation(ByVal p As Point, ByVal Alpha As Single) As Point
    Return New Point(p.X * Cos(Alpha) - p.Y * Sin(Alpha), p.X * Sin(Alpha) + p.Y * Cos(Alpha))
  End Function

  '********************************************************************************************************************
  ' Retourne le point transformé de p  dans la rotation de centre (0,0) et d'angle Alpha
  '********************************************************************************************************************
  Public Function Rotation(ByVal p As PointF, ByVal Alpha As Single) As PointF
    Return New PointF(p.X * Cos(Alpha) - p.Y * Sin(Alpha), p.X * Sin(Alpha) + p.Y * Cos(Alpha))
  End Function
#End Region
#Region "Translation"
  '********************************************************************************************************************
  ' Retourne le point transformé de p  dans la translation de vecteur V(pTran.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal p As Point, ByVal pTrans As Point) As Point
    Return TranslationBase(p, New Size(pTrans.X, pTrans.Y))
  End Function

  '********************************************************************************************************************
  ' Retourne le point transformé de p  dans la translation de vecteur unVecteur
  '********************************************************************************************************************
  Public Function Translation(ByVal p As Point, ByVal unVecteur As Vecteur) As Point
    Return TranslationBase(p, New Size(unVecteur.X, unVecteur.Y))
  End Function

  Public Function Translation(ByVal p As PointF, ByVal unVecteur As Vecteur) As PointF
    Return TranslationBase(p, New SizeF(unVecteur.X, unVecteur.Y))
  End Function


  Public Function TranslationBase(ByVal p As Point, ByVal uneTaille As Size) As Point
    Return Point.op_Addition(p, uneTaille)
  End Function

  Public Function TranslationBase(ByVal p As PointF, ByVal uneTaille As Size) As PointF
    Return PointF.op_Addition(p, uneTaille)
  End Function

  Public Function TranslationBase(ByVal p As PointF, ByVal uneTaille As SizeF) As PointF
    p.X += uneTaille.Width
    p.Y += uneTaille.Height
    Return p
  End Function

  '********************************************************************************************************************
  ' Retourne la ligne transformé de uneLigne  dans la translation de vecteur unVecteur
  '********************************************************************************************************************
  Public Function Translation(ByVal uneLigne As Ligne, ByVal unVecteur As Vecteur) As Ligne
    uneLigne.pA = Translation(uneLigne.pA, unVecteur)
    uneLigne.pB = Translation(uneLigne.pB, unVecteur)
    Return uneLigne
  End Function

  '********************************************************************************************************************
  ' Retourne le point transformé de p  dans la translation de vecteur V(pTrans.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal p As PointF, ByVal pTrans As PointF) As PointF
    p.X += pTrans.X
    p.Y += pTrans.Y
    Return p
  End Function
#End Region
  '********************************************************************************************************************
  ' Transformé le point p dans la rotation de centre (0,0) et d'angle Alpha
  ' puis retourne la translation de ce dernier
  '********************************************************************************************************************
  Public Function RotTrans(ByVal p As PointF, ByVal pTrans As PointF, ByVal Alpha As Single) As PointF
    Return Translation(Rotation(p, Alpha), pTrans)
  End Function

  Public Function RotTrans(ByVal p As Point, ByVal pTrans As Point, ByVal Alpha As Single) As Point
    Return Translation(Rotation(p, Alpha), pTrans)
  End Function
#End Region

  Public Function ResolEq(ByVal alpha As Single, ByVal pB As PointF, ByVal beta As Single, ByVal pA As PointF) As PointF
    Dim a As Single = Math.Cos(alpha)
    Dim b As Single = Math.Sin(alpha)

    Dim a2 As Single = Math.Cos(beta)
    Dim b2 As Single = Math.Sin(beta)

    Dim déterminant As Single
    déterminant = a * b2 - a2 * b

    If déterminant = 0.0F Then
      ' les 2 éléments sont parallèles
    Else
      ' chercher le centre de l'arc de cercle tangent aux 2 éléments
      Dim c As Single = a * pA.X + b * pA.Y
      Dim c2 As Single = a2 * pB.X + b2 * pB.Y

      Dim xO, yO As Single
      xO = (b2 * c - b * c2) / déterminant
      yO = (a * c2 - a2 * c) / déterminant
      ResolEq = New PointF(xO, yO)
    End If

  End Function

  Public Function ClearGraphique(ByVal uneCollection As Graphiques, ByRef unPolyArc As PolyArc, Optional ByVal p() As Point = Nothing, Optional ByVal Clore As Boolean = True) As PolyArc
    If Not IsNothing(unPolyArc) Then
      If Not IsNothing(uneCollection) Then uneCollection.Remove(unPolyArc)
      unPolyArc.Clear()
      unPolyArc = Nothing
    End If

    If IsNothing(p) Then
      Return New PolyArc
    Else
      Return New PolyArc(p, Clore:=Clore)
    End If

  End Function

End Module
