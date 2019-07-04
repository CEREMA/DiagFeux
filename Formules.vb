'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : Formules.vb																							'
'						Module de formules math�matiques																	'
'																																							'
'******************************************************************************
Option Strict Off
Option Explicit On 

Imports System.Math

'--------------------------- Module de formules --------------------------
Module Formules
#Region "D�clarations"
  Public Const RayS�lect As Short = 5
  Public acoTolerance As Single

  Public Enum TypeInterSection
    Indiff�rent               ' le point d'intersection des 2 lignes peut �tre en dehors des 2 segments (dans leur prolongement)
    SurSegment                ' le point d'intersection des 2 lignes doit appartenir aux deux segments
    SurPremierSegment         ' le point d'intersection des 2 lignes doit appartenir au premier segment
    SurSegmentStrict          ' idem SurSegment mais en excluant les extr�mit�s
    SurPremierSegmentStrict   ' idem SurSegmentStrict mais en excluant les extr�mit�s
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
  ' Distance d'un point � une droite
  '**************************************************************************************
  Public Function Distance(ByVal p As Drawing.Point, ByVal uneLigne As Ligne) As Double
    Return Distance(p, uneLigne.pA, uneLigne.pB)
  End Function
  Public Function Distance(ByVal p As Drawing.PointF, ByVal uneLigne As Ligne) As Double
    Return Distance(p, uneLigne.pAF, uneLigne.pBF)
  End Function

  '**************************************************************************************
  ' Distance d'un point � une droite d�termin� par pA et pB
  '**************************************************************************************
  Public Function Distance(ByVal p As Drawing.Point, ByVal pA As Point, ByVal pB As Point) As Double
    Dim pProjet� As Point = Projection(p, pA, pB)
    Return Distance(p, pProjet�)
  End Function
  Public Function Distance(ByVal p As Drawing.PointF, ByVal pA As PointF, ByVal pB As PointF) As Double
    Dim pProjet� As PointF = Projection(p, pA, pB)
    Return Distance(p, pProjet�)
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
  ' Retourne le carr� d'un nombre
  '**************************************************************************************
  Public Function Carr�(ByVal v As Double) As Double
    Carr� = v ^ 2
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

#Region "AngleForm�"
  '=================================================================================================================
  ' Tous les formulent qui suivent sont en coordonn�es dessin (axe des Y invers�)
  '=================================================================================================================

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur d'origine(0,0) et d'extr�mit� (X,Y)
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourn� est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleForm�(ByVal X As Double, ByVal Y As Double, Optional ByVal SurDeuxPI As Boolean = False) As Double

    'Le syst�me d'axe des Y est  invers� : d'o� signe - pour Y 
    AngleForm� = Atan2(-Y, X)

    If AngleForm� < 0 And SurDeuxPI Then AngleForm� += 2 * PI

  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourn� est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleForm�(ByVal unVecteur As Vecteur, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleForm� = AngleForm�(unVecteur.X, unVecteur.Y, SurDeuxPI)
  End Function

  '***************************************************************************************
  ' Calcul de l'angle de deux vecteurs
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourn� est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleForm�(ByVal V1 As Vecteur, ByVal V2 As Vecteur, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleForm� = AngleForm�(V2) - AngleForm�(V1)
    If AngleForm� < 0 Then
      If SurDeuxPI Then
        AngleForm� += 2 * PI
      ElseIf AngleForm� <= -PI Then
        AngleForm� += 2 * PI
      End If
    Else
      If AngleForm� > PI AndAlso Not SurDeuxPI Then
        AngleForm� -= 2 * PI
      End If
    End If
  End Function

  '***************************************************************************************
  ' Calcul de l'angle orient� form� par 3 points - pSommet est le sommet de l'angle
  ' Retourne un angle compris entre ]-pi,pi]
  ' Retourne un angle compris entre [0,2pi[ si SurDeuxPi=True
  '**************************************************************************************
  Public Function AngleForm�(ByVal pSommet As PointF, ByVal p1 As PointF, ByVal p2 As PointF, Optional ByVal SurDeuxPI As Boolean = False) As Double
    AngleForm� = AngleForm�(New Vecteur(pSommet, p1), New Vecteur(pSommet, p2), SurDeuxPI)
    If Not SurDeuxPI Then
      If AngleForm� > PI Then
        AngleForm� -= 2 * PI
      ElseIf AngleForm� <= -PI Then
        AngleForm� += 2 * PI
      End If
    End If
  End Function
  Public Function AngleForm�(ByVal pSommet As Point, ByVal p1 As Point, ByVal p2 As Point, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleForm�(CvPointF(pSommet), CvPointF(p1), CvPointF(p2), SurDeuxPI)
  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'un vecteur 
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourn� est sur [0,2pi[
  ' Si p1 et p2 sont en coordonn�es dessin, l'angle retourn� est dans le sens horaire
  '**************************************************************************************
  Public Function AngleForm�(ByVal p1 As Point, ByVal p2 As Point, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleForm�(New PointF(p1.X, p1.Y), New PointF(p2.X, p2.Y), SurDeuxPI)

  End Function

  '***************************************************************************************
  ' Calcul de l'angle form� par 2 droites
  ' Retourne un angle compris entre [0,pi]
  '***************************************************************************************
  Public Function AngleForm�(ByVal D1 As Ligne, ByVal D2 As Ligne) As Double
    AngleForm� = Abs(AngleForm�(D2) - AngleForm�(D1))
    If AngleForm� > PI Then AngleForm� -= PI
  End Function

  '***************************************************************************************
  ' Calcul de l'angle d'une droite orient�e
  ' Retourne un angle compris entre ]-pi,pi]
  ' SurDeuxPI : si vrai, l'angle retourn� est sur [0,2pi[
  '**************************************************************************************
  Public Function AngleForm�(ByVal uneDroite As Ligne, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Return AngleForm�(uneDroite.pAF, uneDroite.pBF, SurDeuxPI)

  End Function

  '***************************************************************************************
  ' Calcul de l'angle form� par 2 points ordonn�s p1 et p2
  ' Retourne un angle compris entre ]-pi,pi]
  ' Retourne un angle compris entre [0,2pi[ si SurDeuxPi=True
  '**************************************************************************************
  Public Function AngleForm�(ByVal p1 As PointF, ByVal p2 As PointF, Optional ByVal SurDeuxPI As Boolean = False) As Double
    Dim Angle As Double

    If p1.X <> p2.X Then
      Angle = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X))
      If p2.X < p1.X Then
        ' L'angle d�termin� ci-dessus appartient � ]-pi/2,pi/2[
        'Il appartient en fait  � ]pi/2,3pi/2] ou encore � un des 2 intervalles (]-pi,-pi/2[ et ]pi/2,pi])
        Select Case Sign(Angle)
          Case 1  ' 3�me quadrant
            Angle -= PI
          Case Else ' 2�me quadrant
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
  ' p0,p1 : points d�terminant la direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal p0 As Point, ByVal p1 As Point) As Point

    Projection = Projection(pAprojeter, pOrigine, CType(AngleForm�(p0, p1), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' p0,p1 : points d�terminant la direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal p0 As PointF, ByVal p1 As PointF) As PointF

    Projection = Projection(pAprojeter, pOrigine, AngleForm�(p0, p1))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine et pFinal
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal pFinal As Point) As Point

    Projection = Projection(pAprojeter, pOrigine, CType(AngleForm�(pOrigine, pFinal), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine et pFinal
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal pFinal As PointF) As PointF

    Projection = Projection(pAprojeter, pOrigine, AngleForm�(pOrigine, pFinal))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' unVecteur : Vecteur de direction de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal unVecteur As Vecteur) As Point
    Projection = Projection(pAprojeter, pOrigine, CType(AngleForm�(unVecteur), Single))

  End Function

  '******************************************************************************
  ' Projection du point pAprojeter sur la droite passant par pOrigine 
  ' unAngle : Angle de la droite
  '******************************************************************************
  Public Function Projection(ByVal pAprojeter As Point, ByVal pOrigine As Point, ByVal unAngle As Single) As Point
    Return Point.Ceiling(Projection(CvPointF(pAprojeter), CvPointF(pOrigine), unAngle))
  End Function

  Public Function Projection(ByVal pAprojeter As PointF, ByVal pOrigine As PointF, ByVal unAngle As Single) As PointF

    'Angle entre la droite form�e par les 2 points et la droite de projection
    Dim unAngleProjection As Single = AngleForm�(pOrigine, pAprojeter) - unAngle
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
  Public Function ProjectionM�meSens(ByVal pSouris As Point, ByVal p2 As Point, ByVal AngleSegment As Single) As Point
    'Vecteur de norme quelconque de m�me sens que P1P0 et d'origine P2
    Dim V1 = New Vecteur(p2, PointPosition(p2, AngleSegment))

    'Projection de la souris sur V1
    Dim pProjet� As Point = Projection(pSouris, p2, AngleSegment)
    'Vecteur de m�me direction mais pas forc�ment de m�me sens que V1, correspondant au point projet� depuis la position de la souris sur V1
    Dim V2 = New Vecteur(p2, pProjet�)

    If ProduitScalaire(V1, V2) > 0 Then Return pProjet�

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

    '   cette routine d�termine les coordonn�es �ventuelles x et y 
    '   de l'intersection  de 2 segments AB et CD                    

    Dim det1, lambda, mu As Single
    det1 = determ(X1, X2, Y1, Y2)

    If det1 <> 0.0 Then ' les 2 segments ne sont pas  colin�aires 

      lambda = determ(X3, X2, Y3, Y2) / det1
      mu = determ(X1, X3, Y1, Y3) / det1


      Select Case TypeIntersect
        Case TypeInterSection.Indiff�rent
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
    ' calcul d'un d�terminant 
    Return a * d - b * c
  End Function

  Private Function appart(ByVal x As Single, ByVal y As Single, ByVal z As Single) As Boolean
    ' cette routine d�termine si x�[y,z] 
    Return (y - x) * (x - z) >= 0.0

  End Function


  Private Function appart_strict(ByVal a As Single, ByVal b As Single, ByVal c As Single) As Boolean
    ' a � ]b,c[ 
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
  'Conversion d'un angle de degr�s en radians 
  ' Retourne une valeur comprise entre ]-pi et pi]
  ' InverserSens : Indique s'il faut inverser le sens de rotation du syst�me angulaire
  '*******************************************************************************************************************
  Public Function CvAngleRadians(ByVal AngleEnDegr�s As Single, Optional ByVal InverserSens As Boolean = False) As Single
    'If AngleEnDegr�s > 360 Then AngleEnDegr�s -= 360

    Dim unAngle As Single
    If InverserSens Then
      'Convertir du sens horaire au sens trigonom�trique ou inversement
      unAngle = 360 - AngleEnDegr�s
    Else
      unAngle = AngleEnDegr�s
    End If

    If unAngle > 180 Then unAngle -= 360

    'Convertir en radians
    Return unAngle * Math.PI / 180
  End Function

  '*******************************************************************************************************************
  'Conversion d'un angle de radians en degr�s
  ' Retourne une valeur comprise entre 0 et 360
  ' InverserSens : Indique s'il faut inverser le sens de rotation du syst�me angulaire
  '*******************************************************************************************************************
  Public Function CvAngleDegr�s(ByVal AngleEnRadians As Single, Optional ByVal InverserSens As Boolean = True, Optional ByVal SurDeuxPi As Boolean = True) As Single
    'On ne change pas le sens de rotation quand l'angle est d�j� dans le sens horaire (angle obtenu � partir de points en coordonn�es Windows)
    Dim unAngle As Single = AngleEnRadians Mod 2 * PI
    If unAngle < 0 Then unAngle += 2 * PI

    'Convertir du sens trigonom�trique au sens horaire ou inversement
    If InverserSens Then unAngle = (2 * PI - unAngle) Mod 2 * PI

    If Not SurDeuxPi And unAngle > PI Then unAngle -= 2 * PI

    'Convertir en degr�s
    Return unAngle * 180 / Math.PI
  End Function
#End Region

#Region "Position d'un point � une distance donn�e"
  '*******************************************************************************************************************
  'Retourne un point � une distance donn�e du point Origine dans la direction Angle
  ' Angle est fourni en degr�s
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Dist As Single, ByVal AngleEnDegr�s As Single, ByVal SensHoraire As Boolean) As Point
    Dim Alpha As Single

    Alpha = CvAngleRadians(AngleEnDegr�s, SensHoraire)
    'Le syst�me d'axe des Y est  invers� : d'o� prendre l'angle oppos�

    Return PointPosition(pOrigine, Dist, -Alpha)

  End Function

  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Dist As Single, ByVal AngleEnDegr�s As Single, ByVal SensHoraire As Boolean) As PointF
    Dim Alpha As Single

    Alpha = CvAngleRadians(AngleEnDegr�s, SensHoraire)
    'Le syst�me d'axe des Y est  invers� : d'o� prendre l'angle oppos�

    Return PointPosition(pOrigine, Dist, -Alpha)

  End Function

  '*******************************************************************************************************************
  'Retourne un point � une distance donn�e du point Origine dans la direction Alpha d�termin�e par les points p1 et p2
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Dist As Single, ByVal p1 As Point, ByVal p2 As Point) As Point
    Dim Alpha As Single = AngleForm�(p1, p2)
    Return PointPosition(pOrigine, Dist, Alpha)

  End Function

  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Dist As Single, ByVal p1 As PointF, ByVal p2 As PointF) As PointF
    Dim Alpha As Single = AngleForm�(p1, p2)
    Return PointPosition(pOrigine, Dist, Alpha)

  End Function

  '*******************************************************************************************************************
  'Retourne un point � une distance donn�e du point Origine dans la direction Alpha
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
  'Retourne un point � une distance 'infinie' du point Origine dans la direction Alpha
  ' Alpha est en radians
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    ' On choisit une valeur fictive de  500 comme longueur du segment form� par pOrigine et le point cherch�
    Return PointPosition(pOrigine, 500, Alpha)
  End Function
  Public Function PointPosition(ByVal pOrigine As PointF, ByVal Alpha As Single) As PointF
    ' On choisit une valeureur fictive de  500000 comme longueur du segment form� par pOrigine et le point cherch�
    Return PointPosition(pOrigine, 500000, Alpha)
  End Function

  '*******************************************************************************************************************
  'Retourne un point sur la droite passant par 2 points � un coefficient donn� de pCentre
  ' le point M est tel que vectoriellement OM = Lambda * OI - O est pCentre et I pInterm�diaire
  '*******************************************************************************************************************
  Public Function PointPosition(ByVal pCentre As Point, ByVal pInterm�diaire As Point, ByVal Lambda As Single) As Point
    Dim xM, yM As Integer
    xM = Lambda * pInterm�diaire.X + (1 - Lambda) * pCentre.X
    yM = Lambda * pInterm�diaire.Y + (1 - Lambda) * pCentre.Y
    Return New Point(xM, yM)
  End Function

#End Region

#Region "Intersection cercle/cercle cercle/droite"
  '******************************************************************************************
  'Soient un point O et 2 droites CD et FE : rechercher un point H � une distance d de O
  'tel que l'intersection I de FH avec CD respecte 
  '   - les vecteurs CD et CI soient de m�me sens
  '   - le point I ne franchit pas FE
  ' H est tangent au cercle de centre O et de rayon d 
  '*******************************************************************************************
  Public Function PointTangence(ByVal pO As PointF, ByVal Rayon As Single, ByVal LigneR�F�rence As Ligne, ByVal LigneAdverse As Ligne) As PointF
    Dim pF As PointF = LigneAdverse.pAF
    Dim pH As PointF

    Dim xF, yF As Single

    'Pour simplifier les calculs, on fait une translation 
    xF = pF.X - pO.X
    yF = pF.Y - pO.Y

    'Calculer un 1er point de tangence et Refaire la translation inverse
    pH = Translation(pO, PointTangence(xF, yF, Rayon))
    If Not pH.IsEmpty Then
      'Intersection du futur raccord avec la ligne de r�f�rence
      Dim pInter As PointF = intersect(LigneR�F�rence, New Ligne(pF, pH), Formules.TypeInterSection.Indiff�rent)
      Dim pAF As PointF = LigneR�F�rence.pAF
      Dim pbF As PointF = LigneR�F�rence.pBF
      If Abs(AngleForm�(pAF, pbF, pInter)) < 0.1 Then
        'Le nouveau point ne doit pas changer le sens de la ligne de r�f�rence
        pH = Translation(pO, PointTangence(xF, yF, Rayon, PremierAppel:=False))

      ElseIf Distance(pAF, pInter) > Distance(pAF, LigneAdverse) Then
        ' Le nouveau point de la ligne de r�f�rence ne doit pas franchir la ligne adverse
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
      Dim RCArr� As Double = Rayon ^ 2
      a = 1 + (yF / xF) ^ 2
      bPrime = -RCArr� * yF / xF ^ 2
      c = (RCArr� / xF) ^ 2 - RCArr�
      yH = SolutionEquationDegr�2(a, bPrime, c, PremierAppel)
      If Single.IsNaN(yH) Then Return New PointF
      xH = (RCArr� - yF * yH) / xF
    End If

    Return New PointF(xH, yH)

  End Function

  '*******************************************************************************************************************
  ' D�terminer le point situ� sur une droite � une distance Dist d'un point donn� 
  ' pDonn� : point donn�
  ' Dist : Distance entre le point donn� et le point � d�terminer sur la droite
  ' pOrigine : Point origine de la droite
  ' Alpha : Angle de la droite
  'Il s'agit de r�soudre le syst�me d'�quations
  ' (xM-xO)� + (yM-yO)� = Dist�
  ' xM = (xA - xO) * cos(Alpha)
  ' yM = (yA - yO) * cos(Alpha)
  ' O : pDonn�
  ' A : pOrigineDroite
  ' M: Point recherch� sur la droite d'origine A de direction Alpha
  ' En posant x1=xA-xO et y1=yA-yO On aboutit � l'�quation : D� + 2(x1.cos alpha + y1.sin alpha)D + x1�+y1�-Dist�
  '*******************************************************************************************************************

  Public Function PointSurDroiteADistancePointDonn�(ByVal pDonn� As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    Dim pCherch� As Point
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

    d1 = SolutionEquationDegr�2(pDonn�, Dist, pOrigine, Alpha)

    If Not Single.IsNaN(d1) Then

      pCherch� = PointPosition(pOrigine, d1, Alpha)
      If Sign(Beta) <> Sign(AngleForm�(pOrigine, pCherch�)) Then
        d1 = SolutionEquationDegr�2(pDonn�, Dist, pOrigine, Alpha, PremierAppel:=False)
        pCherch� = PointPosition(pOrigine, d1, Alpha)
      End If
      Return pCherch�
    End If

  End Function

  Public Function PointSurCercle(ByVal pDonn� As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single) As Point
    Dim pCherch� As Point
    Dim d1 As Single

    d1 = SolutionEquationDegr�2(pDonn�, Dist, pOrigine, Alpha)

    If Not Single.IsNaN(d1) Then
      pCherch� = PointPosition(pOrigine, d1, Alpha)
      If Sign(Alpha) <> Sign(AngleForm�(pOrigine, pCherch�)) Then
        d1 = SolutionEquationDegr�2(pDonn�, Dist, pOrigine, Alpha, PremierAppel:=False)
        pCherch� = PointPosition(pOrigine, d1, Alpha)
      End If
      If Distance(pCherch�, pOrigine) > 0 Then Return pCherch�
    End If

  End Function

  Public Function SolutionEquationDegr�2(ByVal pDonn� As Point, ByVal Dist As Single, ByVal pOrigine As Point, ByVal Alpha As Single, Optional ByVal PremierAppel As Boolean = True) As Single
    Static b As Single
    Static D�terminant As Single

    If PremierAppel Then
      Dim a As Single = 1.0
      'D�finir x1 et y1
      Dim x1 As Integer = pOrigine.X - pDonn�.X
      Dim y1 As Integer = pOrigine.Y - pDonn�.Y
      'coefficient b' de l'�quation
      b = x1 * Cos(Alpha) + y1 * Sin(Alpha)
      'coefficient c de l'�quation
      Dim c As Single = x1 ^ 2 + y1 ^ 2 - Dist ^ 2

      'D�terminant simplifi� car b est pair
      D�terminant = b ^ 2 - c

      If D�terminant < 0 Then
        Return Single.NaN
      Else
        Return -b + Sqrt(D�terminant)
      End If

    Else
      Return -b - Sqrt(D�terminant)
    End If

  End Function

  Public Function SolutionEquationDegr�2(ByVal a As Single, ByVal bPrime As Single, ByVal c As Single, Optional ByVal PremierAppel As Boolean = True) As Single
    Static D�terminant As Single

    If PremierAppel Then
      'D�terminant simplifi� car b est pair
      D�terminant = (bPrime ^ 2 - a * c)

      If D�terminant < 0 Then
        Return Single.NaN
      Else
        Return (-bPrime + Sqrt(D�terminant)) / a
      End If

    Else
      Return (-bPrime - Sqrt(D�terminant)) / a
    End If

  End Function

  '*********************************************************************************************
  ' Intersection d'un cercle et d'une droite
  '*********************************************************************************************
  Public Function IntersectionCercleDroite(ByVal pCentre As PointF, ByVal R As Single, ByVal uneLigne As Ligne, Optional ByVal PremierAppel As Boolean = True) As PointF
    Dim pProjet� As PointF = Projection(pCentre, uneLigne)
    Static pCherch� As PointF

    Select Case Sign(Distance(pProjet�, pCentre) - R)
      Case 0
        'droite tangente en P
        Return pProjet�
      Case -1
        ' droite s�cante
        If PremierAppel Then
          Dim AH As Single = Math.Sqrt(R ^ 2 - Carr�(Distance(pCentre, pProjet�)))
          pCherch� = PointPosition(pProjet�, AH, AngleForm�(uneLigne))
        Else
          pCherch� = Sym�trique(pCherch�, pProjet�)
        End If
        Return pCherch�
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

    Static pCherch� As PointF
    If PremierAppel Then
      pCherch� = Nothing
    ElseIf pCherch�.IsEmpty Then
      Return pCherch�
    End If

    x1 = p1.X
    y1 = p1.Y
    x0 = p0.X
    y0 = p0.Y

    ' R1� = x1� +x� - 2x1*x + y1� + y� - 2y1*y
    ' R0� = x0� +x� - 2x0*x + y0� + y� - 2y0*y
    ' La r�solution du syst�me aboutit par soustraction �
    ' R1� - R0� = x1� - x0� + 2x(x0-x1) + y1� - y0� +2y(y0-y1)

    If y0 = y1 Then
      ' on obtient : x =((R1� - R0�)- (x1� - x0�)) / 2(x0 - x1)
      If x0 <> x1 Then
        x = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2) / 2 / (x0 - x1)
        'Puis  y� - 2y1*y +  (x - x1)� + y1� - R1�  = 0
        a = 1
        bPrime = -y1
        c = (x - x1) ^ 2 + y1 ^ 2 - R1 ^ 2
        If PremierAppel Then
          y = SolutionEquationDegr�2(a, bPrime, c)
          If Not Single.IsNaN(y) Then
            pCherch� = New PointF(x, y)
            Return pCherch�
          End If

        Else
          pCherch�.Y = SolutionEquationDegr�2(a, bPrime, c, PremierAppel:=False)
        End If

      Else
        ' Cercles concentriques
      End If

    Else
      ' Cas g�n�ral
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
        x = SolutionEquationDegr�2(a, bPrime, c)
        If Not Single.IsNaN(x) Then
          y = (c1 - a1 * x) / b1
          pCherch� = New PointF(x, y)
        End If
      Else
        pCherch�.X = SolutionEquationDegr�2(a, bPrime, c, PremierAppel:=False)
        pCherch�.Y = (c1 - a1 * pCherch�.X) / b1
      End If

      Return Translation(pCherch�, p0)

    End If

  End Function

  Public Function IntersectionCercles(ByVal p1 As PointF, ByVal p0 As PointF, ByVal R1 As Single, ByVal R0 As Single, Optional ByVal PremierAppel As Boolean = True) As PointF
    Dim x1, x0, y1, y0 As Single
    Dim x As Single
    Dim y As Single
    Dim a, b, c As Single

    Static pCherch� As PointF
    If PremierAppel Then
      pCherch� = Nothing
    ElseIf pCherch�.IsEmpty Then
      Return pCherch�
    End If

    x1 = p1.X
    y1 = p1.Y
    x0 = p0.X
    y0 = p0.Y

    ' R1� = x1� +x� - 2x1*x + y1� + y� - 2y1*y
    ' R0� = x0� +x� - 2x0*x + y0� + y� - 2y0*y
    ' La r�solution du syst�me aboutit par soustraction �
    ' R1� - R0� = x1� - x0� + 2x(x0-x1) + y1� - y0� +2y(y0-y1)

    If y0 = y1 Then
      ' on obtient : x =((R1� - R0�)- (x1� - x0�)) / 2(x0 - x1)
      If x0 <> x1 Then
        x = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2) / 2 / (x0 - x1)
        'Puis  y� - 2y1*y +  (x - x1)� + y1� - R1�  = 0
        a = 1
        b = -y1
        c = (x - x1) ^ 2 + y1 ^ 2 - R1 ^ 2
        If PremierAppel Then
          y = SolutionEquationDegr�2(a, b, c)
          If Not Single.IsNaN(y) Then
            pCherch� = New PointF(x, y)
          End If

        Else
          pCherch�.Y = SolutionEquationDegr�2(a, b, c, PremierAppel:=False)
        End If

      Else
        ' Cercles concentriques
      End If

    Else
      ' Cas g�n�ral
      Dim N As Single = (R1 ^ 2 - R0 ^ 2 + x0 ^ 2 - x1 ^ 2 + y0 ^ 2 - y1 ^ 2) / 2 / (y0 - y1)
      Dim K As Single = (x0 - x1) / (y0 - y1)
      a = K ^ 2 + 1
      b = (y0 - N) * K - x0
      c = x0 ^ 2 + y0 ^ 2 + N ^ 2 - R0 ^ 2 - 2 * y0 * N

      If PremierAppel Then
        x = SolutionEquationDegr�2(a, b, c)
        If Not Single.IsNaN(x) Then
          y = N - K * x
          pCherch� = New PointF(x, y)
        End If

      Else
        pCherch�.X = SolutionEquationDegr�2(a, b, c, PremierAppel:=False)
        pCherch�.Y = N - K * pCherch�.X
      End If

      Return pCherch�

    End If

  End Function


#End Region

  '*******************************************************************************************************
  'Cr�er un arc raccordant 2 segments de ligne : Ligne1 et Ligne2
  'Ligne1 et Ligne2 sont ajust�s par rapport � l'arc de raccordement calcul�
  'R : Rayon de l'arc en unit�s r�elles,  diminu� r�cursivement si le raccord initial n'est pas posible
  '*******************************************************************************************************
  Public Function Cr�erRaccord(ByVal Ligne1 As Ligne, ByVal Ligne2 As Ligne, Optional ByVal R As Single = 3.0, Optional ByVal unePlume As Pen = Nothing) _
          As Arc

    Try

      'Sens =1 ou -1 selon sens trigo ou horaire
      Dim Sens As Short = Sign(AngleForm�(Ligne1.pBF, Ligne1.pAF, Ligne2.pBF))
      Dim Angle1 As Single = AngleForm�(Ligne1)
      Dim Angle2 As Single = AngleForm�(Ligne2)
      Dim CoordEntier As Boolean = False ' Not Ligne1.R�el

      'Point d'intersection des 2 segments
      Dim p As PointF = intersect(Ligne2, Ligne1)
      Dim Rayon As Single = Echelle * R ' 3 m, puis �ventuellement en d�croissant jusqu'� 0.5m

      'Rechercher le centre de l'arc 
      'D�terminer la droite P1P11 parall�le � Ligne1 � la distance 'Rayon' de celleci et de longueur 'infinie'
      Dim P1 As PointF = PointPosition(p, Rayon, Angle1 + PI / 2 * Sens)
      Dim p11 As PointF = PointPosition(P1, Angle1)
      'D�terminer la droite P2P12 parall�le � Ligne2 � la distance 'Rayon' de celleci et de longueur 'infinie'
      Dim P2 As PointF = PointPosition(p, Rayon, Angle2 - PI / 2 * Sens)
      Dim p22 As PointF = PointPosition(P2, Angle2)
      'Le centre de l'arc est l'intersection des 2 droites
      Dim pM As PointF = intersect(New Ligne(p11, P1, Nothing), New Ligne(p22, P2, Nothing), TypeInterSect:=Formules.TypeInterSection.Indiff�rent)

      If Not pM.IsEmpty Then

        'D�terminer les points de tangence avec l'arc
        Dim PT1 As PointF = Projection(pM, Ligne1)
        Dim PT2 As PointF = Projection(pM, Ligne2)
        If Not Ligne1.PtSurSegment(PT1) Then
          If R > 0.5 Then ' Sinon pas de raccord possible : les 2 segments initiaux restent inchang�s
            Return Cr�erRaccord(Ligne1, Ligne2, R - 0.5, unePlume)
          End If
        ElseIf Not Ligne2.PtSurSegment(PT2) Then
          If R > 0.5 Then
            Return Cr�erRaccord(Ligne1, Ligne2, R - 0.5, unePlume)
          End If
        Else

          'Construire l'arc de raccordement
          Angle1 = CvAngleDegr�s(AngleForm�(pM, PT1), InverserSens:=False)
          Angle2 = CvAngleDegr�s(AngleForm�(pM, PT2), InverserSens:=False)
          Dim AngleD�part As Single
          Dim AngleFinal As Single
          If Sens = -1 Then ' sens horaire
            AngleD�part = Angle1
            AngleFinal = Angle2
          Else
            AngleD�part = Angle2
            AngleFinal = Angle1
          End If
          Dim AngleBalayage As Single = AngleBalayageArc(AngleD�part, AngleFinal)
          Dim plumeFuschia = New Pen(Color.Fuchsia)
          If IsNothing(unePlume) Then unePlume = plumeFuschia
          If AngleBalayage <> 0 Then
            'Couper les 2 lignes � leur points de tangence avec l'arc
            PT1 = PointPosition(pM, Rayon, CvAngleRadians(Angle1))
            PT2 = PointPosition(pM, Rayon, CvAngleRadians(Angle2))
            Ligne1.pAF = PT1
            Ligne2.pAF = PT2
            Return New Arc(pM, Rayon, AngleD�part, AngleBalayage, unePlume)

          End If
        End If

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Formules.Cr�erRaccord")

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
  ' Retourne le point apr�s �ventuelt clipping en coordonn�es �cran
  '************************************************************************************************
  Public Function PtClipp�(ByVal p As Point, ByVal pOrigine As Point, Optional ByVal Coordonn�esEcran As Boolean = True) As Point

    If Not PointDansPicture(p) Then
      clip(p, pOrigine)
    End If

    If Coordonn�esEcran Then
      Return cndpicDessin.PointToScreen(p)
    Else
      Return p
    End If

  End Function

  Private Function codeClip(ByVal p As Point) As Integer

    'En VB (v4 � v6 tout au moins) True=-1 et False=0
    ' On remultiplie par -1 chaque valeur pour faire 'ressortir' positivement les bits int�ressants
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
          ' p1 est hors de la vue - red�finir p1 � l'intersection de p1p2  et d'un bord
          c1 = NewCodeClip(p1, c1, dX, dY)
          ' p2 est hors de la vue - red�finir p2 � l'intersection de p1p2  et d'un bord
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
      'Tronquage du bord inf�rieur
      p.X = p.X + dX * (-p.Y) / dY
      p.Y = 0.0#

    ElseIf c And 1 Then
      'Tronquage du bord sup�rieur
      p.X = p.X + dX * (yMaxPicture - p.Y) / dY
      p.Y = yMaxPicture
    End If

    NewCodeClip = codeClip(p)

  End Function
#End Region

  Public Sub DessinerPoign�e(ByVal p As Point, Optional ByVal ptCliqu� As Boolean = False)
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

      If ptCliqu� Then
        ControlPaint.DrawReversibleFrame(rc, Color.Gray, FrameStyle.Thick)
        '      ControlPaint.FillReversibleRectangle(rc, Color.Black)
      Else
        ControlPaint.FillReversibleRectangle(rc, Color.Cyan)
      End If

    End If

  End Sub

  '*****************************************************************************************
  'Retourne le point dans le rep�re de la droite d'origine pOrigine et d'orientation unAngle
  ' unAngle : angle de la branche en degr�s
  '*****************************************************************************************
  Public Function ChangementRep�re(ByVal pOrigine As PointF, ByVal unAngle As Single, ByVal p As PointF) As PointF
    Dim pTrans As PointF = New PointF(p.X - pOrigine.X, p.Y - pOrigine.Y)

    Return Rotation(pTrans, -CvAngleRadians(unAngle))

  End Function

  '**************************************************************************************
  'D�terminer l'angle de balayage de l'arc : les angles sont en degr�s
  '**************************************************************************************
  Public Function AngleBalayageArc(ByVal AngleD�part As Single, ByVal AngleFinal As Single) As Single
    If AngleFinal < AngleD�part Then
      AngleBalayageArc = AngleFinal + (360 - AngleD�part)
    Else
      AngleBalayageArc = AngleFinal - AngleD�part
    End If

  End Function

  '***********************************************************************************************************
  'D�termine un positionnement d'�criture selon l'angle de la ligne � l'extr�mit� de laquelle on veut �crire
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
  'D�termine un l'alignement du texte selon l'angle de la ligne � l'extr�mit� de laquelle on veut �crire
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

#Region "Conversions r�elles - dessin"
  '**************************************************************************************
  ' Les coordonn�es r�elles croissent en sens inverse des coordonn�es dessin (syst�me de coord �cran)
  ' Le 0,0 des coordonn�es dessin coincide avec OrigineR�elle (en coordonn�es r�elles)
  '**************************************************************************************
  Public Function PointR�el(ByVal p As Point) As PointF
    Return PointR�el(CvPointF(p))

  End Function

  Public Function PointR�el(ByVal p As PointF) As PointF
    PointR�el = New PointF(ToR�el(p.X), ToR�el(-p.Y))

    PointR�el.X += OrigineR�elle.X
    PointR�el.Y += OrigineR�elle.Y

  End Function

  Public Function PointDessin(ByVal p As PointF) As Point
    p.X -= OrigineR�elle.X
    p.Y -= OrigineR�elle.Y

    PointDessin = New Point(ToDessin(p.X), ToDessin(-p.Y))

  End Function

  Public Function PointDessinF(ByVal p As PointF) As PointF
    p.X -= OrigineR�elle.X
    p.Y -= OrigineR�elle.Y

    Return New PointF(ToDessin(p.X), ToDessin(-p.Y))

  End Function

  Public Function DistanceR�elle(ByVal p1 As Point, ByVal p2 As Point) As Double
    Return ToR�el(Distance(p1, p2))
  End Function

  Public Function DistanceR�elle(ByVal p1 As Point, ByVal uneDroite As Ligne) As Double
    Return ToR�el(Distance(p1, uneDroite))
  End Function
  Public Function DistanceDessin(ByVal p1 As PointF, ByVal p2 As PointF) As Double
    Return ToDessin(Distance(p1, p2))
  End Function
  Public Function ToR�el(ByVal Value As Integer) As Single
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

  Public ReadOnly Property OrigineR�elle() As PointF
    Get
      Return cndParamDessin.OrigineR�elle
    End Get
  End Property

  '**************************************************************************************
  'D�terminer la nouvelle origine r�elle suite au zoom
  'L'origine r�elle est le point correspondant au (0,0) du controle
  'pInvariant est le point cliqu� sur le controle (centre du zoom)
  '**************************************************************************************
  Public Function D�terminerNewOrigineR�elle(ByVal pInvariant As Point, ByVal ZoomPlus As Boolean) As ParamDessin
    Dim pInvariantR�el As PointF = PointR�el(pInvariant)
    Dim uneEchelle As Single = cndParamDessin.Echelle
    Dim uneOrigine As PointF

    If ZoomPlus Then
      uneEchelle *= 2
    Else
      uneEchelle /= 2
    End If

    Dim unRectangle As Rectangle = cndpicDessin.ClientRectangle

    'La formule qui suit d�finit que le point invariant reste en m�me position dans le picturebox
    'OrigineR�elle.X = pInvariantR�el.X - pInvariant.X / uneEchelle
    'OrigineR�elle.Y = pInvariantR�el.Y + pInvariant.Y / uneEchelle

    'La formule qui suit d�finit que le point invariant passe au centre du picturebox
    Dim pCentre As New Point(cndpicDessin.Width / 2, cndpicDessin.Height / 2)
    uneOrigine.X = pInvariantR�el.X - pCentre.X / uneEchelle
    uneOrigine.Y = pInvariantR�el.Y + pCentre.Y / uneEchelle

    Return New ParamDessin(uneEchelle, uneOrigine)

  End Function

  '**************************************************************************************
  'D�terminer la nouvelle origine r�elle suite au zoom
  'L'origine r�elle est le point correspondant au (0,0) du controle
  'pInvariant est le point cliqu� sur le controle (centre du zoom)
  '**************************************************************************************
  Public Function D�terminerNewOrigineR�ellePAN(ByVal pTrans As Point) As ParamDessin
    Dim pTransR�el As PointF
    Dim uneEchelle As Single = cndParamDessin.Echelle
    Dim uneOrigine As PointF = cndParamDessin.OrigineR�elle

    With pTrans
      pTransR�el.X = .X / uneEchelle
      pTransR�el.Y = -.Y / uneEchelle
    End With

    uneOrigine = Translation(uneOrigine, pTransR�el)

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
  'Cr�er un Arc en coordonn�es dessin � partir des propri�t�s d'un arc en coordonn�es r�elles
  'AngleD�part et AngleFinal sont en degr�s, mais dans le sens trigo
  '**************************************************************************************
  Public Function ArcDessin(ByVal pCentre As PointF, ByVal Rayon As Single, ByVal AngleD�part As Single, ByVal AngleFinal As Single) As Arc
    Dim AngleBalayage As Single

    'Conversion en sens horaire

    Dim Angle1 As Single = 360 - AngleFinal
    Dim Angle2 As Single = 360 - AngleD�part

    AngleBalayage = AngleBalayageArc(Angle1, Angle2)

    Return New Arc(PointDessin(pCentre), Rayon * Echelle, Angle1, AngleBalayage)

  End Function
#End Region


#Region "G�om�trie"
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
  ' Retourne le sym�trique de p1 par rapport � p2
  '********************************************************************************************************************
  Public Function Sym�trique(ByVal p1 As Point, ByVal p2 As Point) As Point
    Return New Point(2 * p2.X - p1.X, 2 * p2.Y - p1.Y)
  End Function
  Public Function Sym�trique(ByVal p1 As PointF, ByVal p2 As PointF) As PointF
    Return New PointF(2 * p2.X - p1.X, 2 * p2.Y - p1.Y)
  End Function
#Region "Rotation"
  '********************************************************************************************************************
  ' Retourne le point transform� de p  dans la rotation de centre (0,0) et d'angle Alpha
  '********************************************************************************************************************
  Public Function Rotation(ByVal p As Point, ByVal Alpha As Single) As Point
    Return New Point(p.X * Cos(Alpha) - p.Y * Sin(Alpha), p.X * Sin(Alpha) + p.Y * Cos(Alpha))
  End Function

  '********************************************************************************************************************
  ' Retourne le point transform� de p  dans la rotation de centre (0,0) et d'angle Alpha
  '********************************************************************************************************************
  Public Function Rotation(ByVal p As PointF, ByVal Alpha As Single) As PointF
    Return New PointF(p.X * Cos(Alpha) - p.Y * Sin(Alpha), p.X * Sin(Alpha) + p.Y * Cos(Alpha))
  End Function
#End Region
#Region "Translation"
  '********************************************************************************************************************
  ' Retourne le point transform� de p  dans la translation de vecteur V(pTran.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal p As Point, ByVal pTrans As Point) As Point
    Return TranslationBase(p, New Size(pTrans.X, pTrans.Y))
  End Function

  '********************************************************************************************************************
  ' Retourne le point transform� de p  dans la translation de vecteur unVecteur
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
  ' Retourne la ligne transform� de uneLigne  dans la translation de vecteur unVecteur
  '********************************************************************************************************************
  Public Function Translation(ByVal uneLigne As Ligne, ByVal unVecteur As Vecteur) As Ligne
    uneLigne.pA = Translation(uneLigne.pA, unVecteur)
    uneLigne.pB = Translation(uneLigne.pB, unVecteur)
    Return uneLigne
  End Function

  '********************************************************************************************************************
  ' Retourne le point transform� de p  dans la translation de vecteur V(pTrans.X,pTrans.Y)
  '********************************************************************************************************************
  Public Function Translation(ByVal p As PointF, ByVal pTrans As PointF) As PointF
    p.X += pTrans.X
    p.Y += pTrans.Y
    Return p
  End Function
#End Region
  '********************************************************************************************************************
  ' Transform� le point p dans la rotation de centre (0,0) et d'angle Alpha
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

    Dim d�terminant As Single
    d�terminant = a * b2 - a2 * b

    If d�terminant = 0.0F Then
      ' les 2 �l�ments sont parall�les
    Else
      ' chercher le centre de l'arc de cercle tangent aux 2 �l�ments
      Dim c As Single = a * pA.X + b * pA.Y
      Dim c2 As Single = a2 * pB.X + b2 * pB.Y

      Dim xO, yO As Single
      xO = (b2 * c - b * c2) / d�terminant
      yO = (a * c2 - a2 * c) / d�terminant
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
