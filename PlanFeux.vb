'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours � feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : Andr� VIGNAUD																						'
'																																							'
'						Source : .PlanFeux.vb																							'
'						Classes																														'
'							PlanFeux																												'
'							PlanFeuxCollection																							'
'             PlanFeuxBase 			  																						'
'             PlanFeuxPhasage
'							PlanFeuxFonctionnement      																		'
'             FiltrePhasage
'******************************************************************************
Option Strict Off
Option Explicit On

'=====================================================================================================
'--------------------------- Classe PlanFeux --------------------------
'=====================================================================================================
Public MustInherit Class PlanFeux : Inherits M�tier

#Region "D�clarations"
  Public Const maxiDur�eCycleAbsolue As Short = 130
  Public Const maxiDur�eCycle As Short = 120
  Public Const Dur�eCycleD�faut As Short = 70

  'Nom du plan de feux 
  Protected mNom As String

  Protected mTrafic As Trafic

  Protected mDemande As Integer
  Protected mDemandeUVP As New Hashtable

  Protected mCapacit�Th�orique As Single
  'R�erve de capacit� calcul�e en fonction de la dur�e du cycle
  Private mR�serveCapacit� As Single
  Protected mStockage As Short
  Protected mCapacit�ACalculer As Boolean = True

  Public mPhases As New PhaseCollection(Me)
  Public mVariante As Variante

  Private mD�calages(1) As Hashtable
  Private dctPhasesLf As Hashtable
  Public Enum D�calage
    Ouverture
    Fermeture
  End Enum
  Public Enum Position
    Aucune = -1
    Unique
    Premi�re
    Derni�re
  End Enum

  Public Marges As Point
  Public IntervalX, IntervalY As Single

  Public MustOverride ReadOnly Property Dur�eMini() As Short
  Public MustOverride Property RougeIncompressible(ByVal uneLigneFeux As LigneFeux) As Short
  Public MustOverride ReadOnly Property mLignesFeux() As LigneFeuxCollection
  Public MustOverride Property Nom() As String

#End Region

#Region "Demande"
  'Protected Overridable Sub Dimensionner()
  '  mDemandeUVP = New Hashtable
  'End Sub

  Public Property DemandeUVP(ByVal uneLigneFeux As LigneFeux) As Integer
    Get
      Return mDemandeUVP(uneLigneFeux.ID)
    End Get
    Set(ByVal Value As Integer)
      mDemandeUVP(uneLigneFeux.ID) = Value
    End Set
  End Property

  Public ReadOnly Property Demande() As Integer
    Get
      If AvecTrafic() Then
        If DemandeACalculer Then
          CalculerDemande()
        End If
        Return mDemande
      End If
    End Get
  End Property

  Private ReadOnly Property DemandeACalculer() As Boolean
    Get
      Return (mDemande = -1)
    End Get
  End Property

  Public Sub CalculerDemande()
    mDemande = DemandeDuCarrefour()
  End Sub

  '***********************************************************************
  '	D�terminer la demande du carrefour
  ' R�f :  � d) et e) - p27 du guide carrefour � feux
  ' La demande est 
  '       - la somme des demandes pr�pond�rantes par phase
  '       - ind�pendante de la dur�e du cycle
  '       - d�pendante de la p�riode de trafic
  '***********************************************************************
  Private Function DemandeDuCarrefour() As Integer
    Dim unePhase, unePhaseSuivante As Phase
    Dim uneLigneFeux As LigneFeux
    Dim qPond�r� As Integer
    Dim CourantPr�pond�rant As New Hashtable
    'Dictionnaire pour stocker les lignes de feux sur 2 phases
    Dim LignesPhases As New Hashtable

    For Each unePhase In mPhases
      CourantPr�pond�rant(unePhase) = 0

      Try

        For Each uneLigneFeux In unePhase.mLignesFeux
          If uneLigneFeux.EstV�hicule Then

            qPond�r� = TraficPond�r�(CType(uneLigneFeux, LigneFeuV�hicules))
            DemandeUVP(uneLigneFeux) = qPond�r�

            Select Case PositionDansPhase(uneLigneFeux, unePhase)
              Case Position.Unique
                '	trafic pr�pond�rant de la phase
                CourantPr�pond�rant(unePhase) = Math.Max(CourantPr�pond�rant(unePhase), qPond�r�)
              Case Position.Premi�re
                LignesPhases(uneLigneFeux) = unePhase
            End Select
          End If

        Next

      Catch ex As System.Exception
        LancerDiagfeuxException(ex, "Calcul de la demande du carrefour")
      End Try

    Next unePhase

    Dim dMin, dMax, dLigneFeux As Integer

    For Each uneLigneFeux In mLignesFeux
      'Rechercher si la pr�sence de lignes de feux multiphases n'oblige pas 
      ' � red�finir les courants pr�pond�rants 
      If LignesPhases.ContainsKey(uneLigneFeux) Then
        unePhase = LignesPhases(uneLigneFeux)
        unePhaseSuivante = mPhases.PhaseSuivante(unePhase)
        dLigneFeux = DemandeUVP(uneLigneFeux)

        If dLigneFeux > CourantPr�pond�rant(unePhase) + CourantPr�pond�rant(unePhaseSuivante) Then
          'Sinon, la ligne de feux multiphase s'�coule de fa�on masqu�e : on ne fait rien
          dMin = Math.Min(CourantPr�pond�rant(unePhase), CourantPr�pond�rant(unePhaseSuivante))
          dMax = Math.Max(CourantPr�pond�rant(unePhase), CourantPr�pond�rant(unePhaseSuivante))
          If dLigneFeux > 2 * dMax Then
            'Partager la demande de la ligne sur 2 phases en 2 parts �gales sur les 2 phases
            CourantPr�pond�rant(unePhase) = dLigneFeux / 2
            CourantPr�pond�rant(unePhaseSuivante) = dLigneFeux / 2
          Else
            'Conserver la demande pour la phase la + importante
            'Attribuer le reste � l'autre phase
            If CourantPr�pond�rant(unePhase) = dMin Then
              CourantPr�pond�rant(unePhase) = dLigneFeux - dMax
            Else
              CourantPr�pond�rant(unePhase) = dLigneFeux - dMax
            End If
          End If


        End If
      End If
    Next

    For Each unePhase In mPhases
      'M�moriser dans la phase le trafic le + important qu'elle supporte
      unePhase.TraficSupport� = CourantPr�pond�rant(unePhase)
      'Cumuler les trafics support�s
      DemandeDuCarrefour += unePhase.TraficSupport�
    Next

  End Function

  Private Function TraficPond�r�(ByVal uneLigneFeux As LigneFeuV�hicules) As Integer
    If mVariante.ModeGraphique Then
      Return uneLigneFeux.TraficPond�r�Riche(mTrafic)
    Else
      Return uneLigneFeux.TraficPond�r�Riche(mTrafic)
    End If

  End Function

  '**********************************************************************************************
  ' Calculer la r�serve de capacit� du plan de feux en connaissant la dur�e de son cycle
  ' R�f�rence : Guide Carrefour � feux du CERTU - p 28 et 29
  '**********************************************************************************************
  Public Sub CalculerR�serveCapacit�()
    Dim uneDur�e As Short = Dur�eCycle()

    ' Formule p27
    Capacit�Th�orique = CType(mVariante.D�bitSaturation, Single) * (uneDur�e - TempsPerdu) / uneDur�e
    R�serveCapacit� = Capacit�Th�orique - Demande

    ' Formule p29
    Stockage = Trafic.QTotal(Trafic.TraficEnum.UVP) * Dur�eCycle() / 3600

    mCapacit�ACalculer = False

  End Sub

  Public Overridable Property Capacit�ACalculer() As Boolean
    Get
      If AvecTrafic() Then
        Return mCapacit�ACalculer
      End If
    End Get
    Set(ByVal Value As Boolean)
      mCapacit�ACalculer = Value
    End Set
  End Property

  Public ReadOnly Property TempsPerdu() As Short
    Get
      Return mPhases.TempsPerdu
    End Get
  End Property

  '****************************************************************************************************
  ' Calcul la dur�e du cycle du plan de feux en fonction de la demande du carrefour et des temps perdus
  '   CoefDemande = -1 ==> M�thode de Webster
  '   CoefDemande >=0  ==> M�thode classique, CoefDemande repr�sente le coefficient � apporter � la demande
  '*****************************************************************************************************
  Public Function CalculCycle(Optional ByRef Message As String = "", Optional ByVal CoefDemande As Single = -1) As Short
    Dim tp As Short = TempsPerdu
    Dim D�bitSaturation As Short = mVariante.D�bitSaturation
    Dim DemandePriseEncompte As Short = (1 + CoefDemande) * Demande
    Dim uneDur�e As Short

    If CoefDemande = -1 Then
      'Webster
      DemandePriseEncompte = Demande
    Else
      'M�thode classique
      DemandePriseEncompte = (1 + CoefDemande) * Demande
    End If

    If DemandePriseEncompte >= D�bitSaturation Then
      Message = "La m�thode n'est pas applicable pour un tel trafic" & vbCrLf & _
      "Demande du carrefour : " & DemandePriseEncompte & " uvpd/h"
    Else

      If CoefDemande = -1 Then
        'M�thode de Webster
        ' Formule fournie par le CERTU :
        ' Co =(1.5r + 5) / (1 -D/s)
        ' Co : Cycle optimum
        ' r : somme des temps perdus (rouge+jaune inutilis�+temps perdu au d�marrage)
        ' D : Demande en uvp/s
        ' s : d�bit de saturation = 0.5uvp/s soit 1800 uvp/h
        uneDur�e = (1.5 * tp + 5) / (1 - (Demande / D�bitSaturation))

      Else
        ' Formule d�duite de QTMax  =1800(C-T)/C du Guide des Carrefours � feux du  CERTU(p27) ==> C = (1800*T)/(1800-QTMax)
        uneDur�e = CType(CSng(D�bitSaturation) * tp / (D�bitSaturation - DemandePriseEncompte), Short)

      End If

      If uneDur�e > maxiDur�eCycleAbsolue Then
        Message = "Le calcul conduit � une dur�e trop importante : " & uneDur�e & "s" & vbCrLf & "Dur�e maximale admise : " & maxiDur�eCycleAbsolue & " s"
      Else
        If uneDur�e < Dur�eMini Then
          If TypeOf Me Is PlanFeuxFonctionnement Then
            MessageBox.Show("La valeur de " & uneDur�e & "s calcul�e est inf�rieure � la dur�e du plan de feux de base" & vbCrLf & _
            NomProduit & " retient la valeur de " & Dur�eMini & "s")
          Else
            'Calcul de dur�e du plan de base (ou plan pour phasage) faible car trafic de r�f�rence faible : rallonger la dur�e au mini
          End If
          uneDur�e = Dur�eMini
        End If

        Return uneDur�e
      End If

    End If

  End Function

  Public Function strR�serveCapacit�PourCent() As String
    Return Format(mR�serveCapacit� / Demande, "0%")
  End Function

  Public Function R�serveCapacit�PourCent() As Single
    Return mR�serveCapacit� / Demande * 100
  End Function

  Public Property R�serveCapacit�() As Single
    Get
      Return mR�serveCapacit�
    End Get
    Set(ByVal Value As Single)
      mR�serveCapacit� = Value
    End Set
  End Property

  Public Property Capacit�Th�orique() As Single
    Get
      Return mCapacit�Th�orique
    End Get
    Set(ByVal Value As Single)
      mCapacit�Th�orique = Value
    End Set
  End Property

  '********************************************************************************************
  'Dur�eCycle : Dur�e du cycle du plan de feux
  'Minimum : indique si l'on veut la dur�e minimum ou r�elle du cycle 
  '********************************************************************************************
  Public Function Dur�eCycle(Optional ByVal Minimum As Boolean = False) As Short
    Dim unePhase As Phase

    If Minimum Then
      Dur�eCycle = Dur�eMini
    Else
      'Dur�e du cycle en secondes : somme des dur�es des phases
      For Each unePhase In mPhases
        Dur�eCycle += unePhase.Dur�e
      Next

    End If
  End Function

#End Region

#Region "Trafic"

  Public Overridable Property Trafic() As Trafic
    Get
      Return mTrafic
    End Get
    Set(ByVal Value As Trafic)
      If Not Value Is mTrafic Then
        'La demande sera � recalculer
        mDemande = -1
        mCapacit�ACalculer = True
        mTrafic = Value
      End If
    End Set
  End Property

  Public Function AvecTrafic() As Boolean
    Return Not IsNothing(mTrafic)
  End Function

#End Region

  Public Sub AddPhases(ByVal unePhase As Phase)
    unePhase.mPlanFeux = Me
    mPhases.Add(unePhase)
  End Sub

  Public Property Stockage() As Short
    Get
      Return mStockage
    End Get
    Set(ByVal Value As Short)
      mStockage = Value
    End Set
  End Property

  '***************************************************************************
  ' R�partir la dur�e du cycle aux phases selon le trafic qu'elles supportent
  '***************************************************************************

  Public Sub R�partirDur�eCycle(ByVal Dur�e As Short)
    Dim unePhase As Phase
    Dim nbPhasesR�parties As Short = mPhases.Count
    Dim Dur�eAR�partir As Short = Dur�e

    'Si une phase est enti�rement pi�tonne, on lui affecte la dur�e incompressible (vert mini pi�ton + rouge d�gagemen)
    ' sauf si la dur�e a d�j� �t� augment�e
    For Each unePhase In mPhases
      With unePhase
        If .EstSeulementPi�ton Then
          'Retirer le temps perdu de la dur�e � r�partir : pour les pi�tons c'est toute la dur�e de la phase qui est perdue
          Dur�eAR�partir -= Math.Max(.Dur�e, .Dur�eIncompressible)
          nbPhasesR�parties -= 1
        Else
          'Retirer le temps perdu de la dur�e � r�partir
          Dur�eAR�partir -= unePhase.TempsPerdu(Me)
        End If
      End With
    Next

    ' R�partition proportionnelle de la dur�e restante par rapport aux trafics
    For Each unePhase In mPhases
      With unePhase
        If Not .EstSeulementPi�ton Then
          .Dur�e = Math.Max(Dur�eAR�partir * (.TraficSupport� / Demande) - mVariante.D�calageVertUtile + unePhase.TempsPerdu(Me), .Dur�eIncompressible)
        End If
      End With
    Next

    'Les boucles qui suivent pourraient �tre am�lior�es, en regardant l'�cart entre le pourcentage de trafic et le pourcentage de dur�e
    unePhase = mPhases(0)
    Do While Dur�eCycle() < Dur�e
      If Not unePhase.EstSeulementPi�ton Then
        unePhase.Dur�e += 1
      End If
      unePhase = PhaseSuivante(unePhase)
    Loop

    unePhase = mPhases(0)
    Do While Dur�eCycle() > Dur�e
      If unePhase.Dur�e > unePhase.Dur�eIncompressible Then
        unePhase.Dur�e -= 1
      End If
      unePhase = PhaseSuivante(unePhase)
    Loop

  End Sub

  Public Sub D�verrouillerPhases()
    Dim unePhase As Phase

    For Each unePhase In mPhases
      unePhase.Verrouill�e = False
    Next

  End Sub

  '********************************************************************************************************************
  ' D�terminer la 1�re phase non verrouill�e qui suit la phase
  '********************************************************************************************************************
  Protected Function PhaseSuivante(ByVal unePhase As Phase) As Phase

    Return mPhases.PhaseSuivante(unePhase)

  End Function

  Public Function PhaseAssoci�eLigneFeux(ByVal uneLigneFeux As LigneFeux) As Phase
    Dim unePhase As Phase

    'Rechercher la phase concern�e par la ligne de feux et m�moriser de combien le d�calage va varier
    For Each unePhase In mPhases
      If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
        Return unePhase
      End If
    Next

  End Function

  '***********************************************************************
  '	Dessiner le diagramme du plan de feux
  '***********************************************************************
  Public Sub DessinerDiagramme(ByVal g1 As Graphics, ByVal g2 As Graphics)
    Dim uneLigneFeux As LigneFeux
    Dim unePhase As Phase
    Dim uneFonte As Font
    Dim D�butVert As Short
    Dim LgVert As Short
    Dim D�butOrange As Short
    Dim LgOrange As Short
    Dim D�butPhase As Short
    Dim LgPhase As Short
    Dim FinPhase As Short
    'Dim FinVertPhaseMini As Short
    Dim X1, Y1, X2, Y2 As Single
    Dim LimiteSection(Dur�eCycle()) As Boolean
    Dim PositionLigne As Position

    Try

      Dim unePlumeVerte As New Pen(Color.Green, width:=3)
      Dim unePlumeRouge As New Pen(Color.Red, width:=3)
      Dim unePlumeOrange As New Pen(Color.Orange, width:=3)
      Dim unePlumeRose As New Pen(Color.Pink, width:=3)
      Dim unePlumeNoire As New Pen(Color.Black)

      Dim uneBrosseVerte As New SolidBrush(Color.Green)
      Dim uneBrosseRouge As New SolidBrush(Color.Red)
      Dim uneBrosseOrange As New SolidBrush(Color.Orange)
      Dim uneHachure As New Drawing2D.HatchBrush(Drawing2D.HatchStyle.BackwardDiagonal, Color.LightGray, Color.White)

      Dim uneBrosse As New SolidBrush(Color.Black)
      Dim i As Short = 1
      Dim MargeHaute As Short
      Dim YTraitVertical As Short
      Dim p(1) As Point

      Dim unTexte As Texte

      Dim EpaisseurVert, EpaisseurRouge As Single
      Dim PourImpression As Boolean = (cndFlagImpression = dlgImpressions.ImpressionEnum.PlanDeFeux)

      If PourImpression Then
        uneFonte = New Font("Arial", 9, unit:=GraphicsUnit.Point)
        unePlumeNoire.Width = 0.2
        MargeHaute = 8
        EpaisseurVert = 1.5
        EpaisseurRouge = 0.5

        X1 = 120
        X2 = 130
        Y1 = 191
        DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurVert, uneBrosseVerte, g1, g2)
        DessinerChaine("Vert", uneFonte, uneBrosse, X1 + 15, Y1 - 2, g1, g2)
        Y1 = 194
        DessinerPolygone(X1, X2, Y1, EpaisseurRouge, EpaisseurRouge, uneBrosseRouge, g1, g2)
        DessinerChaine("Rouge", uneFonte, uneBrosse, X1 + 15, Y1 - 2, g1, g2)
        Y1 = 197
        X2 = 123
        DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurRouge, uneBrosseOrange, g1, g2)
        DessinerChaine("Jaune", uneFonte, uneBrosse, X1 + 15, Y1 - 2, g1, g2)

        Y1 = 185
        p(0) = New Point(X1, Y1)
        X2 = 10
        Y2 = 2
        HachurerRectangle(p(0), New Size(X2, Y2), uneHachure, g1, g2, unePlumeNoire)
        DessinerChaine("Dur�es", uneFonte, uneBrosse, X1 + 15, Y1 - 2, g1, g2)
        DessinerChaine("compressibles", uneFonte, uneBrosse, X1 + 15, Y1, g1, g2)

      Else
        Dim D�butNomLf As Short
        uneFonte = New Font("Arial", 10, FontStyle.Bold, unit:=GraphicsUnit.Pixel)
        unePlumeNoire.DashStyle = Drawing2D.DashStyle.DashDot
        Marges.Y = 10
        'A l'�cran : r�duire l'�chelle en X si la r�solution est faible
        If Screen.PrimaryScreen.Bounds.Width > 1024 Then
          D�butNomLf = 6
          Marges.X = 26
          IntervalX = 6.5
        Else
          D�butNomLf = 1
          Marges.X = 17
          IntervalX = 6
        End If
        IntervalY = 20
        MargeHaute = 10

        EpaisseurVert = 10
        EpaisseurRouge = 3

        'Ecrire l'intitul� des lignes de feux au d�but de chaque ligne
        For Each uneLigneFeux In mLignesFeux
          DessinerChaine(uneLigneFeux.ID, uneFonte, uneBrosseRouge, D�butNomLf, 20 * i + MargeHaute, g1, g2)
          i += 1
        Next
      End If

      YTraitVertical = posY(0) - MargeHaute

      If PourImpression Then
        'Hachurer les dur�es compressibles
        For Each unePhase In mPhases
          X1 = posX(D�butPhase)
          Y1 = posY(0)
          X2 = (unePhase.Dur�e - unePhase.Dur�eIncompressible) * IntervalX
          'Y2 : Rajouter un intervalle pour l'espace au-dessus des lignes de feux
          Y2 = (mLignesFeux.Count + 1) * IntervalY
          p(0) = New Point(X1, Y1)
          HachurerRectangle(p(0), New Size(X2, Y2), uneHachure, g1, g2, unePlumeNoire)
          D�butPhase += unePhase.Dur�e
        Next
        D�butPhase = 0
      End If

      For Each unePhase In mPhases
        LgPhase = unePhase.Dur�e
        FinPhase = D�butPhase + LgPhase
        '        FinVertPhaseMini = 1000

        For Each uneLigneFeux In unePhase.mLignesFeux
          PositionLigne = PositionDansPhase(uneLigneFeux, unePhase)
          With uneLigneFeux
            Y1 = posY(mLignesFeux.IndexOf(uneLigneFeux)) + MargeHaute
            'Le d�but du vert est d�but de la phase �ventuellement d�cal�
            If PositionLigne <> Position.Derni�re Then
              D�butVert = D�butPhase + D�calageOuvreFerme(uneLigneFeux, D�calage.Ouverture)
            Else
              D�butVert = D�butPhase
            End If

            LgVert = Dur�eVertSurPhase(uneLigneFeux, unePhase)

            D�butOrange = D�butVert + LgVert
            LimiteSection(D�butOrange) = True
            LgOrange = .Dur�eJaune
            If PositionLigne = Position.Premi�re Then
              LgOrange = 0
            Else
              '              FinVertPhaseMini = Math.Min(FinVertPhaseMini, D�butOrange)
            End If

            'Tracer le segment de vert
            X1 = posX(D�butVert)
            X2 = posX(D�butOrange)
            DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurVert, uneBrosseVerte, g1, g2)
            If LgOrange > 0 Then
              'V�hicules qui ne se continue pas sur la phase suivante
              'Tracer le segment de jaune
              X1 = X2
              X2 = posX(D�butOrange + LgOrange)
              DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurRouge, uneBrosseOrange, g1, g2)
              LimiteSection(D�butOrange + LgOrange) = True
            End If

            'Tracer le segment de rouge jusqu'� la fin du cycle
            If PositionLigne <> Position.Premi�re Then
              X1 = X2
              X2 = posX(Dur�eCycle)
              If X2 > X1 Then
                DessinerPolygone(X1, X2, Y1, EpaisseurRouge, EpaisseurRouge, uneBrosseRouge, g1, g2)
              End If
            End If

            'Tracer le segment de rouge depuis le d�but du cycle jusqu'au vert
            If D�butVert <> 0 And PositionLigne <> Position.Derni�re Then
              X1 = posX(0)
              X2 = posX(D�butVert)
              DessinerPolygone(X1, X2, Y1, EpaisseurRouge, EpaisseurRouge, uneBrosseRouge, g1, g2)
            End If
            LimiteSection(D�butVert) = True

          End With
        Next uneLigneFeux

        'Trait vertical en d�but de phase
        X1 = posX(D�butPhase)
        Y1 = YTraitVertical
        Y2 = posY(mLignesFeux.Count - 1) + MargeHaute

        p(0) = New Point(X1, Y1)
        p(1) = New Point(X1, Y2)
        DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        'Indiquer l'abscisse de d�but de la phase
        If PourImpression Then
          'DessinerChaine("Phase " & mPhases.IndexOf(unePhase) + 1, uneFonte, uneBrosse, X1, Y1 + 4, g1, g2)

          ' v11 : Juin 06 - Centrer le texte 'Phase n' dans la largeur de la phase - doc ACONDIA v10 - Impressions �7
          p(1).X = posX(D�butPhase + unePhase.Dur�e)
          'Position du texte 4 unit�s au-dessus de la ligne
          p(0).Y += 4
          p(1).Y = p(0).Y
          unTexte = New Texte("Phase " & mPhases.IndexOf(unePhase) + 1, uneBrosse, uneFonte, Milieu(p(0), p(1)), unAlignement:=StringAlignment.Center)
          unTexte.Dessiner(g1, g2)
        End If

        If PourImpression Or D�butPhase > 0 Then
          DessinerChaine(CType(D�butPhase, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        End If

        '---------Fonctionnalit� supprim�e le 20/06/06 � la demande du CERTU : doc ACONDIA v10 - Impressions �6
        ''Trait vertical de Fin de vert le + t�t

        'X1 = posX(FinVertPhaseMini)
        ''ou mieux : 
        ''X1 = posX(D�butPhase + unePhase.Dur�eIncompressible)
        'p(0).X = X1
        'p(1).X = X1
        'DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        ''Indiquer l'abscisse de fin de vert
        'DessinerChaine(CType(FinVertPhaseMini, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        '--------------------------------------------------------------------------------------------------------------

        ''''---------Fonctionnalit� r�introduite le 25/01/07 � la demande du CERTU : doc ACONDIA v11 - Plans de feux �21
        ''''Trait vertical de Fin de vert le + t�t

        ''''X1 = posX(FinVertPhaseMini)
        ''''ou mieux : 
        ''''X1 = posX(D�butPhase + unePhase.Dur�eIncompressible)
        ''''p(0).X = X1
        ''''p(1).X = X1
        ''''DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        ''''Indiquer l'abscisse de fin de vert
        ''''DessinerChaine(CType(FinVertPhaseMini, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        ''''--------------------------------------------------------------------------------------------------------------

        D�butPhase += unePhase.Dur�e
        LimiteSection(D�butPhase) = True
      Next unePhase

      'Trait vertical en fin de cycle
      X1 = posX(FinPhase)
      Y1 = YTraitVertical
      Y2 = posY(mLignesFeux.Count - 1) + MargeHaute
      p(0) = New Point(X1, Y1)
      p(1) = New Point(X1, Y2)
      DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

      'Indiquer l'abscisse de fin du cycle (dur�e du cycle)
      DessinerChaine(CType(FinPhase, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)

      If PourImpression Then
        p(0).Y = posY(0)
      Else
        p(0).Y += IntervalY
      End If
      p(1).Y += IntervalY
      Y1 = p(1).Y - IntervalY

      Dim Bordure(-1), Espace As Short
      Dim texteEspace As Texte

      For i = 0 To LimiteSection.Length - 1
        If LimiteSection(i) Then
          p(0).X = posX(i)
          p(1).X = p(0).X
          DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)
          If Bordure.Length = 0 Then
            ReDim Bordure(0)
            Bordure(0) = i
          Else
            If Bordure.Length = 1 Then
              ReDim Preserve Bordure(1)
            Else
              Bordure(0) = Bordure(1)
            End If
            Bordure(1) = i
            Espace = Bordure(1) - Bordure(0)
            texteEspace = New Texte(CType(Espace, String), uneBrosseRouge, uneFonte, New Point(posX(Bordure(1) - Espace / 2), Y1))
            texteEspace.Dessiner(g1, g2)
            'DessinerChaine(CType(Espace, String), uneFonte, uneBrosseRouge, posX(Bordure(1) - Espace / 2), Y1, g1, g2)
          End If
        End If
      Next

      If PourImpression Then
        'Dessiner le trait de s�paration en dessous des noms des phases
        X1 = posX(0)
        X2 = posX(Me.Dur�eCycle)
        Y1 = posY(0)
        DessinerLigne(New Point(X1, Y1), New Point(X2, Y1), unePlumeNoire, g1, g2)
      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PlanFeux.DessinerDiagramme")
    End Try
  End Sub

  Private Function posX(ByVal X As Single) As Single
    posX = IntervalX * X + Marges.X
  End Function

  Private Function posY(ByVal Index As Single) As Single
    posY = IntervalY * (Index + 1) + Marges.Y
  End Function

  Private Sub DessinerLigne(ByVal p1 As Point, ByVal p2 As Point, ByVal unePlume As Pen, ByVal g1 As Graphics, ByVal g2 As Graphics)
    g1.DrawLine(unePlume, p1, p2)
    If Not IsNothing(g2) Then g2.DrawLine(unePlume, p1, p2)
  End Sub

  Private Sub DessinerChaine(ByVal Chaine As String, ByVal uneFonte As Font, ByVal uneBrosse As SolidBrush, ByVal X As Short, ByVal Y As Short, ByVal g1 As Graphics, ByVal g2 As Graphics)
    g1.DrawString(Chaine, uneFonte, uneBrosse, X, Y)
    If Not IsNothing(g2) Then g2.DrawString(Chaine, uneFonte, uneBrosse, X, Y)
  End Sub

  Private Sub DessinerPolygone(ByVal X1 As Single, ByVal X2 As Single, ByVal Y As Single, ByVal Hd As Single, ByVal Hf As Single, ByVal uneBrosse As SolidBrush, ByVal g1 As Graphics, ByVal g2 As Graphics)
    Dim tabPoint(3) As PointF

    tabPoint(0).X = X1
    tabPoint(0).Y = Y
    tabPoint(1).X = X1
    tabPoint(1).Y = Y - Hd
    tabPoint(2).X = X2
    tabPoint(2).Y = Y - Hf
    tabPoint(3).X = X2
    tabPoint(3).Y = Y

    g1.FillPolygon(uneBrosse, tabPoint)
    If Not IsNothing(g2) Then
      g2.FillPolygon(uneBrosse, tabPoint)
    End If

  End Sub

  Private Function TexteNomPhase(ByVal p1 As Point, ByVal p2 As Point, ByVal Chaine As String, ByVal uneFonte As Font, ByVal uneBrosse As SolidBrush) As Texte

    'Nom de la rue

    Dim PositionTexte As Point = Milieu(p1, p2)
    Dim unTexte As New Texte(Chaine, uneBrosse, uneFonte, PositionTexte, unAlignement:=StringAlignment.Center)

    Return unTexte

  End Function

  Private Sub HachurerRectangle(ByVal p As Point, ByVal uneTaille As Size, ByVal uneHachure As Drawing2D.HatchBrush, ByVal g1 As Graphics, ByVal g2 As Graphics, Optional ByVal Contour As Pen = Nothing)
    Dim unRectangle As New Rectangle(p, uneTaille)

    g1.FillRectangle(uneHachure, New Rectangle(p, uneTaille))
    If Not IsNothing(g2) Then g2.FillRectangle(uneHachure, unRectangle)
    If Not IsNothing(Contour) Then g1.DrawRectangle(Contour, unRectangle)
  End Sub

  '********************************************************************************************************************
  ' Enregistrer la Ligne de feux dans le fichier
  ' Etape 1 : Cr�er l' enregistrement n�cessaire dans le DataSet DIAGFEUX
  '********************************************************************************************************************
  Public Overridable Function Enregistrer(ByVal uneVariante As Variante, ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.PlanFeuxRow
    Dim uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow = ds.PlanFeux.NewPlanFeuxRow
    Dim unePhase As Phase

    uneRowPlanFeux.SetParentRow(uneRowVariante)
    ds.PlanFeux.AddPlanFeuxRow(uneRowPlanFeux)

    If AvecTrafic() Then
      uneRowPlanFeux.IDTrafic = mVariante.mTrafics.IndexOf(mTrafic)
    End If

    For Each unePhase In mPhases
      unePhase.Enregistrer(uneRowPlanFeux)
    Next

    Return uneRowPlanFeux

  End Function

  Public Sub New()
    mVariante = cndVariante
    mD�calages(0) = New Hashtable
    mD�calages(1) = New Hashtable
  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeux)

    mVariante = unPlanFeux.mVariante
    mD�calages(0) = New Hashtable
    mD�calages(1) = New Hashtable

    unPlanFeux.mPhases.Cloner(Me)

  End Sub

  Public Sub New(ByVal unTrafic As Trafic)
    mVariante = unTrafic.Variante
    mD�calages(0) = New Hashtable
    mD�calages(1) = New Hashtable

    Trafic = unTrafic
  End Sub

  Public Sub New(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim i As Short
    Dim unePhase As Phase

    mVariante = cndVariante
    mD�calages(0) = New Hashtable
    mD�calages(1) = New Hashtable

    With uneRowPlanFeux
      If Not .IsIDTraficNull Then
        Trafic = mVariante.mTrafics(uneRowPlanFeux.IDTrafic)
      End If

      For i = 0 To .GetPhaseRows.Length - 1
        unePhase = New Phase(Me, .GetPhaseRows(i))
        mPhases.Add(unePhase)
      Next
    End With

  End Sub

  ' D�calage � l'ouverture ou � la fermeture d'une ligne de feux
  ' uneLigneFeux : ligne de feux concern�e
  ' Index : 0 pour ouverture - 1 pour fermeture
  '*************************************************************************************************
  Public Property D�calageOuvreFerme(ByVal uneLigneFeux As LigneFeux, ByVal Index As D�calage) As Short
    Get
      If mD�calages(Index).Contains(uneLigneFeux) Then Return mD�calages(Index).Item(uneLigneFeux)
    End Get
    Set(ByVal Value As Short)
      mD�calages(Index).Item(uneLigneFeux) = Value
    End Set
  End Property

  '********************************************************************************************************************
  'Somme des d�calages � l'ouverture et � la fermeture
  '********************************************************************************************************************
  Private Function D�calageTotal(ByVal uneLigneFeux As LigneFeux) As Short
    Return D�calageOuvreFerme(uneLigneFeux, D�calage.Ouverture) + D�calageOuvreFerme(uneLigneFeux, D�calage.Fermeture)
  End Function

  '********************************************************************************************************************
  'Dur�e des phases concern�es par la ligne de feux 
  '********************************************************************************************************************
  Private Function Dur�ePhases(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase
    Dim uneDur�e As Short

    For Each unePhase In mPhases
      If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
        uneDur�e += unePhase.Dur�e
      End If
    Next

    Return uneDur�e

  End Function

  '***********************************************************************************
  'Retourne la dur�e de vert maxi de la ligne de feux (si elle n'a pas de d�calages)
  '***********************************************************************************
  Public Function Dur�eVertMaxi(ByVal uneLigneFeux As LigneFeux) As Short
    Return Dur�ePhases(uneLigneFeux) - RougeIncompressible(uneLigneFeux) - uneLigneFeux.Dur�eJaune
  End Function

  '********************************************************************************************
  'Rechercher les lignes de feux ayant le vert sur 2 phases
  ' et mettre dans une table le num�ro de la phase qui donne le d�but de vert � la ligne de feux
  '*********************************************************************************************
  Public Overridable Sub IndexerLignesFeux()
    Dim unePhase, unePhaseOriginale As Phase
    Dim uneLigneFeux As LigneFeux

    dctPhasesLf = New Hashtable

    For Each unePhase In mPhases
      For Each uneLigneFeux In unePhase.mLignesFeux
        If dctPhasesLf.Contains(uneLigneFeux) Then
          'La ligne de feux est sur 2 phases : rechercher la phase qui donne le vert
          unePhaseOriginale = dctPhasesLf(uneLigneFeux)
          If Not unePhase Is PhaseSuivante(unePhaseOriginale) Then
            dctPhasesLf(uneLigneFeux) = unePhase
          End If

        Else
          'M�moriser dans le  tableau la 1�re phase o� apparait la ligne de feux
          dctPhasesLf(uneLigneFeux) = unePhase
        End If

      Next
    Next

  End Sub

  Private Function IndexPremi�rePhase(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase

    For Each unePhase In mPhases
      Select Case PositionDansPhase(uneLigneFeux, unePhase)
        Case Position.Unique, Position.Premi�re
          Return mPhases.IndexOf(unePhase)
      End Select
    Next
  End Function

  Public Function Sup�rieur(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Boolean

    Dim Index1, Index2 As Short
    Index1 = IndexPremi�rePhase(L1)
    Index2 = IndexPremi�rePhase(L2)

    If Index1 > Index2 Then
      ' L1 commence apr�s L2 : il est dans une phase sup�rieure
      Return True

    ElseIf Index1 = Index2 Then
      'L1 et L2 commence   gnnt dans la m�me phase : on conserve l'ordre sauf si seul le 1er est multiphases
      If PositionDansPhase(L1, mPhases(Index1)) <> PlanFeux.Position.Unique _
      And PositionDansPhase(L2, mPhases(Index2)) = PlanFeux.Position.Unique Then
        Return True
      End If
    End If

  End Function

  Public Function PositionDansPhase(ByVal uneLigneFeux As LigneFeux, ByVal unePhase As Phase) As Position
    If dctPhasesLf.Item(uneLigneFeux) Is unePhase Then
      'Le vert d�marre dans cette phase pour cette ligne de feux
      If PhaseSuivante(unePhase).mLignesFeux.Contains(uneLigneFeux) Then
        'La ligne de feux continue sur la phase suivante : 
        ' elle circule donc jusqu'� la fin de la phase(pas de temps de jaune inutilis�)
        Return Position.Premi�re
      Else
        Return Position.Unique
      End If

    ElseIf unePhase.mLignesFeux.Contains(uneLigneFeux) Then
      'Les v�hicules passaient d�j� dans la phase pr�c�dente
      'Pas de temps perdu au d�marrage
      Return Position.Derni�re
    End If

    Return Position.Aucune

  End Function

  '********************************************************************************************************************
  'Dur�e de vert de la ligne de feux : cumul �ventuel sur 2 phases
  '********************************************************************************************************************
  Public Function Dur�eVert(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase
    Dim uneDur�e As Short

    For Each unePhase In mPhases
      If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
        uneDur�e += Dur�eVertSurPhase(uneLigneFeux, unePhase)
      End If
    Next

    Return uneDur�e

  End Function

  '******************************************************************************************************
  'Dur�e de vert de la ligne de feux pendant une phase donn�e : il s'agit du vert r�el
  '******************************************************************************************************
  Public Function Dur�eVertSurPhase(ByVal uneLigneFeux As LigneFeux, ByVal unePhase As Phase) As Short

    Dim uneDur�e As Short

    uneDur�e = unePhase.Dur�e

    Select Case PositionDansPhase(uneLigneFeux, unePhase)
      Case Position.Premi�re
        'La ligne de feux continue sur la phase suivante : 
        ' elle circule donc jusqu'� la fin de la phase(pas de temps de jaune inutilis�)
        uneDur�e -= D�calageOuvreFerme(uneLigneFeux, D�calage.Ouverture)

      Case Position.Derni�re
        'Les v�hicules passaient d�j� dans la phase pr�c�dente
        'Pas de temps perdu au d�marrage
        uneDur�e -= D�calageOuvreFerme(uneLigneFeux, D�calage.Fermeture)
        uneDur�e -= RougeIncompressible(uneLigneFeux)
        uneDur�e -= uneLigneFeux.Dur�eJaune

      Case Position.Unique
        uneDur�e -= D�calageTotal(uneLigneFeux)
        uneDur�e -= RougeIncompressible(uneLigneFeux) + uneLigneFeux.Dur�eJaune

    End Select

    Return uneDur�e

  End Function

  Public Function VertUtile(ByVal uneLigneFeux As LigneFeux) As Short
    Return Dur�eVert(uneLigneFeux) + mVariante.D�calageVertUtile
  End Function

  Public Overrides Function Cr�erGraphique(ByVal uneCollection As Graphiques) As PolyArc
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxCollection--------------------------
'=====================================================================================================
Public Class PlanFeuxCollection : Inherits CollectionBase

  ' Cr�er une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet � la collection.
  ' Retourne la position	� laquelle le plan est ins�r�
  Public Function Add(ByVal unPlan As PlanFeux) As Short
    If Not Me.Contains(unPlan) Then
      Return Me.List.Add(unPlan)
    End If
  End Function

  ' Ajouter une plage d'objets � la collection.
  Public Sub AddRange(ByVal valeurs() As PlanFeux)
    Me.InnerList.AddRange(valeurs)
  End Sub

  Public Sub Insert(ByVal unPlan As PlanFeux, ByVal Index As Short)
    Me.InnerList.Insert(Index, unPlan)
  End Sub

  ' Supprimer un objet sp�cifique de la collection.
  Public Sub Remove(ByVal unPlan As PlanFeux)
    If Me.List.Contains(unPlan) Then
      Me.List.Remove(unPlan)
    End If

  End Sub

  'Ins�rer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unPlan As PlanFeux)
    Me.List.Insert(Index, unPlan)
  End Sub

  ' Creer la propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As PlanFeux
    Get
      Return CType(Me.List.Item(Index), PlanFeux)
    End Get
  End Property

  ' Creer une autre propri�t� par d�faut Item pour cette collection.
  ' Permet la  recherche par nom. 
  Default Public ReadOnly Property Item(ByVal nomPlan As String) As PlanFeux
    Get
      Dim unPlan As PlanFeux
      For Each unPlan In Me.List
        If String.Compare(nomPlan, unPlan.Nom, ignoreCase:=True) = 0 Then
          Return unPlan
        End If
      Next
    End Get
  End Property

  Public Function IndexOf(ByVal unPlan As PlanFeux) As Short
    Return Me.List.IndexOf(unPlan)
  End Function

  ' Methode pour v�rifier si un Objet existe d�j� dans la collection.
  Public Overloads Function Contains(ByVal unPlan As PlanFeux) As Boolean
    Return Me.List.Contains(unPlan)
  End Function

  Public Overloads Function Contains(ByVal nomPlan As String) As Boolean
    Return Not IsNothing(Item(nomPlan))
  End Function

  '***************************************************************************
  ' Initialiser le dur�es minimum des plans de base possibles
  ' Au stade de l'organisation du phasage, celles-ci doivent �tre recalcul�es
  ' � chaque modification des temps de rouge de d�gagement
  '***************************************************************************
  Public Sub CalculerDur�esMini()
    Dim unPlanFeux As PlanFeuxBase

    For Each unPlanFeux In Me
      If Not unPlanFeux.PhasageIncorrect Then
        unPlanFeux.CalculerDur�esMini()
      End If
    Next
  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxBase --------------------------
'=====================================================================================================
Public Class PlanFeuxBase : Inherits PlanFeux
#Region "D�clarations"

  'plans de fonctionnement attach�s au plan de feux de base
  Public mPlansFonctionnement As New PlanFeuxCollection
  'Collection des trafics attach�s au plan de feux de base via ses plans de fonctionnement
  Private mTrafics As New TraficCollection
  Private mNbPfAvecTrafic As Short

  'Collection de lignes de feux identique � la variante, 
  '  mais ordonn�e sp�cifiquement pour le plan de feux de base et ses plans de fonctionnement associ�s
  Public desLignesFeux As New LigneFeuxCollection(Me)
  Private mAntagonismes As AntagonismeCollection
  Private mConflitsInitialis�s As Boolean
  Public mFiltrePhasage As FiltrePhasage
  Public PhasageInitialis� As Boolean

  Public PhasageIncorrect As Boolean
  Private mRougeIncompressible As New Hashtable
  'Indique si le plan de feux comporte au moins une ligne de feux sur 2 phases
  Public mLigneFeuxMultiPhases As Boolean
  'Indique si le plan de feux comporte une phase sp�ciale TAG ou TAD
  Public mAvecPhaseSp�ciale As Boolean

  Private mPlanPhasageAssoci� As PlanFeuxPhasage
  Private mVerrou As [Global].Verrouillage = [Global].Verrouillage.Aucun
  Private mPlansPourPhasage As New PlanFeuxCollection
  Private mPlanFonctionnementCourant As PlanFeuxFonctionnement

  Protected mPlanParent As PlanFeuxBase
  Public VertMiniV�hicules As Short = [Global].VertMiniV�hicules
  Public VertMiniPi�tons As Short = [Global].VertMiniPi�tons
#End Region

#Region "FiltrePhasage"
  Public Property mTroisPhasesSeulement() As Boolean
    Get
      Return Not mFiltrePhasage.TroisPhases
    End Get
    Set(ByVal Value As Boolean)
      mFiltrePhasage.TroisPhases = Not Value
    End Set
  End Property
  Private Sub InitFiltrePhasage()
    mFiltrePhasage = New FiltrePhasage
    With mFiltrePhasage
      .TroisPhases = (mPhases.Count = 3)
      .Crit�reCapacit� = FiltrePhasage.Capacit�Enum.Aucun
      .LigneFeuxMultiPhases = FiltrePhasage.LFMultiphasesEnum.Inclure
      .AvecPhaseSp�ciale = FiltrePhasage.PhaseSp�cialeEnum.Inclure
    End With
  End Sub
  Public Sub D�terminerPhaseSp�ciale()
    Dim dct As New Hashtable
    Dim uneLigneFeux As LigneFeux
    Dim unePhase As Phase

    For Each unePhase In mPhases
      For Each uneLigneFeux In unePhase.mLignesFeux
        'Ne faire la recherche que sur les lignes v�hicules
        If uneLigneFeux.EstV�hicule Then
          If dct.Contains(uneLigneFeux.mBranche) Then
            'Cette branche est d�j� concern�e par une phase
            If Not dct(uneLigneFeux.mBranche) Is unePhase Then
              'La branche est concern�e par 2 phases
              mAvecPhaseSp�ciale = True
              Exit For
            End If
          Else
            dct.Add(uneLigneFeux.mBranche, unePhase)
          End If
        End If
      Next
      If mAvecPhaseSp�ciale Then Exit For
    Next

  End Sub
#End Region

#Region "Propri�t�s"

  Public Overrides Property Nom() As String
    Get
      If AvecTrafic() Then
        Return mTrafic.Nom
      Else
        Return mNom
      End If
    End Get
    Set(ByVal Value As String)
      If AvecTrafic() Then
        mTrafic.Nom = Value
      Else
        mNom = Value
      End If
    End Set
  End Property

  Public Sub Renommer(ByVal nouveauNom As String)
    If AvecTrafic() Then
      mTrafic.Nom = nouveauNom
    Else
      mNom = nouveauNom
    End If
  End Sub

  Friend Property Verrou() As Verrouillage
    Get
      Verrou = mVerrou
    End Get
    Set(ByVal Value As Verrouillage)
      mVerrou = Value
    End Set
  End Property

  Public ReadOnly Property D�finitif() As Boolean
    Get
      Return mVariante.Sc�narioD�finitif Is Me
    End Get
  End Property

  Public ReadOnly Property Projet() As Boolean
    Get
      Return Not D�finitif
    End Get
  End Property

  Public Overrides ReadOnly Property mLignesFeux() As LigneFeuxCollection
    Get
      Return desLignesFeux
    End Get
  End Property

  Public ReadOnly Property Antagonismes() As AntagonismeCollection
    Get
      Return mAntagonismes
    End Get
  End Property

  Public Property ConflitsInitialis�s() As Boolean
    Get
      Return mConflitsInitialis�s
    End Get
    Set(ByVal Value As Boolean)
      mConflitsInitialis�s = Value
    End Set
  End Property

  Public Overrides Property Capacit�ACalculer() As Boolean
    Get
      Return MyBase.Capacit�ACalculer
    End Get

    Set(ByVal Value As Boolean)
      MyBase.Capacit�ACalculer = Value
      If Value Then
        'Il faut aussi recalculer les capacit�s des plans de fonctionnement
        Dim unPlanFonctionnement As PlanFeuxFonctionnement
        For Each unPlanFonctionnement In mPlansFonctionnement
          unPlanFonctionnement.Capacit�ACalculer = Value
        Next
      End If
    End Set
  End Property

  Public Property PlanFonctionnementCourant() As PlanFeuxFonctionnement
    Get
      Return mPlanFonctionnementCourant
    End Get
    Set(ByVal Value As PlanFeuxFonctionnement)
      mPlanFonctionnementCourant = Value
    End Set
  End Property

  Public ReadOnly Property Trafics() As TraficCollection
    Get
      Return mTrafics
    End Get
  End Property

  Public Sub ReCr�erTrafics()
    Dim unPlanFeux As PlanFeuxFonctionnement

    mTrafics.Clear()
    mNbPfAvecTrafic = 0

    For Each unPlanFeux In mPlansFonctionnement
      If unPlanFeux.AvecTrafic() Then
        If Not mTrafics.Contains(unPlanFeux.Trafic) Then
          mTrafics.Add(unPlanFeux.Trafic)
        End If
        mNbPfAvecTrafic += 1
      End If
    Next
  End Sub

  Public ReadOnly Property NbPfAvecTrafic() As Short
    Get
      Return mNbPfAvecTrafic
    End Get
  End Property

  Public Function TraficsImprimables() As TraficCollection
    'Pour les trafics, plusieurs solutions sont possibles
    '1 : Afficher tous les trafics
    Return mVariante.mTrafics
    '2 : N'afficher que les trafics concern�s par au moins 1 plan de fonctionnement
    'Return mTrafics
    '3 : Ne faire cette restriction que si le plan de feux de base est verrouill� et au moins un PFF
    'If mPlansFonctionnement.Count > 0 Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If
    '4 : Ne faire cette restriction que pour le sc�nario d�finitif
    'If mVariante.Sc�narioD�finitif Is Me Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If
    '5 : Combinaison des restrictions 3 et 4
    'If mVariante.Sc�narioD�finitif Is Me AndAlso mPlansFonctionnement.Count > 0 Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If

  End Function

  ''Indique s'il s'agit d'un carrefour compos�, auquel cas les lignes de feux seront d�compos�es en sous-ensembles disjoints
  'Private ReadOnly Property Compos�() As Boolean
  '  Get
  '    Return Not IsNothing(mLignesFeux)
  '  End Get
  'End Property

#End Region

#Region "Constructeurs"
  Public Sub New()
    MyBase.New()

    If Not TypeOf Me Is PlanFeuxPhasage Then
      Cr�erLignesFeux()
      InitFiltrePhasage()
    End If
  End Sub

  Public Sub New(ByVal unTrafic As Trafic)

    MyBase.New(unTrafic)

    InitFiltrePhasage()
    Cr�erLignesFeux()
    Cr�erAntagonismes()
  End Sub

  Public Sub New(ByVal unNom As String)
    MyBase.New()
    InitFiltrePhasage()
    mNom = unNom
    Cr�erLignesFeux()
    Cr�erAntagonismes()

  End Sub

  '*****************************************************************************************************
  'Duplication d'un plan de feux de base 
  ' Sert � la duplication de sc�nario
  ' Sert aussi � d�finir un plan de phasage � partir d'un autre pour mettre 1 ligne fe feux sur 2 phases
  '*****************************************************************************************************
  Public Sub New(ByVal unPlanFeux As PlanFeuxBase)

    MyBase.New(unPlanFeux)

    Dim unPlanPourPhasage As PlanFeuxPhasage
    Dim unPlanFonctionnement As PlanFeuxFonctionnement

    InitFiltrePhasage()

    mVerrou = unPlanFeux.Verrou

    If mVerrou >= [Global].Verrouillage.LignesFeux Then
      'duplication de sc�nario
      mPlanParent = unPlanFeux
      Cr�erAntagonismes()
      mPlanParent = Nothing

      With unPlanFeux
        If mVerrou >= [Global].Verrouillage.Matrices Then

          'Si la matrice des conflits est verrouill�e : dupliquer les plans de phasage possibles
          For Each unPlanPourPhasage In .PlansPourPhasage
            mPlansPourPhasage.Add(New PlanFeuxPhasage(unPlanPourPhasage))
          Next

          If .PhasageRetenu Then
            'Duplication de sc�nario : le plan de phasage associ� est en m�me position
            Me.PlanPhasageAssoci� = mPlansPourPhasage(.PlansPourPhasage.IndexOf(.PlanPhasageAssoci�))
            For Each unPlanFonctionnement In .mPlansFonctionnement
              'Dupliquer si n�cessaire les plans de feux de fonctionnement
              If String.Compare(unPlanFonctionnement.Nom, .Nom, ignorecase:=True) = 0 Then
                mPlansFonctionnement.Add(New PlanFeuxFonctionnement(unPlanFonctionnement, "", Me))
              Else
                mPlansFonctionnement.Add(New PlanFeuxFonctionnement(unPlanFonctionnement, unPlanFonctionnement.Nom, Me))
              End If
            Next
          End If
        End If
      End With
    End If

  End Sub

  Public Sub New(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    MyBase.New(uneRowPlanFeux)
    InitFiltrePhasage()

    Dim i As Short
    Dim uneRowIncompatible As DataSetDiagFeux.IncompatiblesRow
    Dim uneRowRougeD�gagement As DataSetDiagfeux.RougesD�gagementRow

    'Cr�er les lignes de feux en respectant l'ordre sp�cifique au plan de feux de base
    'Duplique �galement les Incompatibilit�s � partir de celles du niveau Variante (conflits syst�matiques)
    Cr�erLignesFeux(uneRowPlanFeux)
    'Cr�er les antagonismes et duplique les antagonismes syst�matiques niveau Variante
    Cr�erAntagonismes()

    With uneRowPlanFeux
      If Not .IsVerrouPlanNull Then
        mVerrou = .VerrouPlan
      End If

      mNom = .ID
      If mNom = "0" Then
        'Ancien projet ACONDIA
        mNom = ""
      End If

      If Not .IsD�finitifNull AndAlso .D�finitif Then
        mVariante.Sc�narioD�finitif = Me
      End If

      If Not .IsVertMiniV�hiculesPlanNull Then
        Me.VertMiniV�hicules = .VertMiniV�hiculesPlan
      End If
      If Not .IsVertMiniPi�tonsPlanNull Then
        Me.VertMiniPi�tons = .VertMiniPi�tonsPlan
      End If

      'Incompatibilit�s des lignes de feux : Matrice des conflits - Ajout de celles issues de la r�solution des antagonismes
      For i = 0 To .GetIncompatiblesRows.Length - 1
        uneRowIncompatible = .GetIncompatiblesRows(i)
        With uneRowIncompatible
          mLignesFeux.EstIncompatible(mLignesFeux(.IdLfInc1), mLignesFeux(.IdLfInc2)) = True
        End With
      Next

      'Matrice des rouges de d�gagement
      For i = 0 To .GetRougesD�gagementRows.Length - 1
        uneRowRougeD�gagement = .GetRougesD�gagementRows(i)
        With uneRowRougeD�gagement
          mLignesFeux.RougeD�gagement(mLignesFeux(.IdLfRouge1), mLignesFeux(.IdLfRouge2)) = .RougesD�gagement_text
        End With
      Next

      For i = 0 To .GetTypesConflitRows.Length - 1
        mAntagonismes(i).TypeConflit = .GetTypesConflitRows(i).TypesConflit_Column
      Next
    End With  ' uneRowPlanFeux


  End Sub

#End Region

  '********************Enregistrer**************
  Public Overrides Function Enregistrer(ByVal uneVariante As Variante, ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.PlanFeuxRow
    'Appeler la fonction de la classe de base
    Dim uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow = MyBase.Enregistrer(uneVariante, uneRowVariante)
    Dim uneLigneFeux As LigneFeux
    Dim uneLigneAdverse As LigneFeux
    Dim Rouge As Short
    Dim unAntagonisme As Antagonisme
    Dim unPlanFeux As PlanFeuxFonctionnement

    uneRowPlanFeux.ID = Nom
    uneRowPlanFeux.D�finitif = uneVariante.Sc�narioD�finitif Is Me

    uneRowPlanFeux.VerrouPlan = mVerrou

    If Me.VertMiniV�hicules <> mVariante.VertMiniV�hicules Then
      uneRowPlanFeux.VertMiniV�hiculesPlan = VertMiniV�hicules
    End If
    If Me.VertMiniPi�tons <> mVariante.VertMiniPi�tons Then
      uneRowPlanFeux.VertMiniPi�tonsPlan = VertMiniPi�tons
    End If

    For Each uneLigneFeux In mLignesFeux
      ds.OrdreLignes.AddOrdreLignesRow(uneLigneFeux.ID, uneRowPlanFeux)
      For Each uneLigneAdverse In mLignesFeux
        If mLignesFeux.IndexOf(uneLigneAdverse) > mLignesFeux.IndexOf(uneLigneFeux) Then
          'On n'�crit qu'une fois l'incompatibilit� : siF1 incompatible avec F2 inutile d'�crire que F2 l'est avec F1
          If Not mVariante.mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneAdverse) Then
            'On ne r��crit pas les conflits syst�matiques : contenus dans la colletion lignes de feux de la variante
            If mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneAdverse) Then
              ds.Incompatibles.AddIncompatiblesRow(uneLigneFeux.ID, uneLigneAdverse.ID, uneRowPlanFeux)
            End If
          End If

        End If
        Rouge = mLignesFeux.RougeD�gagement(uneLigneFeux, uneLigneAdverse)
        ds.RougesD�gagement.AddRougesD�gagementRow(uneLigneFeux.ID, uneLigneAdverse.ID, Rouge, uneRowPlanFeux)
      Next
    Next

    If Not IsNothing(mAntagonismes) Then
      'mAntagonismes peut �tre Nothing en mode tableur ou encore si les LF ne sont pas verrouill�es
      For Each unAntagonisme In mAntagonismes
        ds.TypesConflit.AddTypesConflitRow(unAntagonisme.TypeConflit, uneRowPlanFeux)
      Next
    End If

    For Each unPlanFeux In mPlansFonctionnement
      unPlanFeux.Enregistrer(uneVariante, uneRowVariante)
    Next

    'If Compos� Then
    '  Dim uneLigneFeux As LigneFeux
    '  uneRowPlanFeux.ID = CStr(uneVariante.mPlansFeuxBase.IndexOf(Me))
    '  For Each uneLigneFeux In mLignesFeux
    '    ds.IDLigneFeuxCompos�.AddIDLigneFeuxCompos�Row(uneLigneFeux.ID, uneRowPlanFeux)
    '  Next
    'End If

  End Function

#Region "LignesFeux-Antagonismes"

  Private Sub Cr�erLignesFeux(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim i As Short

    With uneRowPlanFeux
      'On cr�e les lignes de feux en s'appuyant sur l'ordre m�moris�(qui peut �tre diff�rent de l'ordre des lignes du projet)
      For i = 0 To .GetOrdreLignesRows.Length - 1
        mLignesFeux.Add(mVariante.mLignesFeux(.GetOrdreLignesRows(i).OrdreLignes_Column))
      Next
    End With

    Cr�erLignesFeux()

  End Sub

  Public Sub RenommerLigneFeux(ByVal uneLigneFeux As LigneFeux, ByVal exID As String)
    Dim unPlanFonctionnement As PlanFeuxFonctionnement

    mLignesFeux.Substituer(uneLigneFeux, exID)
    For Each unPlanFonctionnement In Me.mPlansFonctionnement
      unPlanFonctionnement.mLignesFeux.Substituer(uneLigneFeux, exID)
    Next

  End Sub

  Public Sub Cr�erAntagonismes()
    Dim unAntagonisme, NewAntagonisme As Antagonisme

    With mVariante
      If .Verrou >= [Global].Verrouillage.LignesFeux Then
        If mLignesFeux.Count = 0 Then
          'Le plan de feux de base a �t� cr�� avant le verrouillage des lignes de feux : elles n'ont donc pas �t� cr��es
          Cr�erLignesFeux()
        End If

        If .ModeGraphique Then
          mAntagonismes = New AntagonismeCollection(mLignesFeux)
          For Each unAntagonisme In AntagoADupliquer()
            NewAntagonisme = New Antagonisme(unAntagonisme, DuplicationIncompl�te:=IsNothing(mPlanParent))
            'Ajouter l'antagonisme � la collection
            'Cette instruction permet aussi de regrouper les antagonismes qui sont li�s car correspondant aux m�mes courants de circulation
            mAntagonismes.Add(NewAntagonisme)
          Next
        End If

        If Not IsNothing(mPlanParent) Then
          'Duplication de sc�nario
          Me.VertMiniV�hicules = mPlanParent.VertMiniV�hicules
          Me.VertMiniPi�tons = mPlanParent.VertMiniPi�tons
        End If

      Else
        'Remettre le plan de Feux de base � z�ro
        mLignesFeux.Dimensionner(RemiseAZ�ro:=True)
        mLignesFeux.Clear()
        If Not IsNothing(mAntagonismes) Then
          mAntagonismes.Clear()
        End If
      End If

    End With

  End Sub

  Private Function AntagoADupliquer() As AntagonismeCollection

    If IsNothing(mPlanParent) Then
      Return mVariante.Antagonismes
    Else
      Return mPlanParent.Antagonismes
    End If
  End Function

  Private Function LignesFeuxADupliquer() As LigneFeuxCollection
    If IsNothing(mPlanParent) Then
      Return mVariante.mLignesFeux
    Else
      Return mPlanParent.mLignesFeux
    End If

  End Function

  Private Sub Cr�erLignesFeux()
    Dim uneLigneFeux As LigneFeux

    Try

      If mVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
        If mLignesFeux.Count = 0 Then
          For Each uneLigneFeux In LignesFeuxADupliquer()
            mLignesFeux.Add(uneLigneFeux)
          Next
        End If

        'Clone les lignes de feux incompatibles, ainsi que les rouges de d�gagement 
        mLignesFeux.ClonerIncompatibilit�s(LignesFeuxADupliquer)
        'Dimensionner()

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PlanFeux.Cr�erLignesFeux")
    End Try

  End Sub
#End Region

#Region "Dur�esMini"

  '********************************************************************************************
  'Dur�eMini : Dur�e minimum du cycle du plan de feux
  '********************************************************************************************
  Public Overrides ReadOnly Property Dur�eMini() As Short
    Get
      Dim unePhase As Phase
      'Somme des dur�es incompressibles du plan de feux
      For Each unePhase In mPhases
        Dur�eMini += unePhase.Dur�eIncompressible
      Next
    End Get
  End Property

  '********************************************************************************************
  ' Calculer la dur�e minimum des phases du plan de feux de base
  '********************************************************************************************
  Public Sub CalculerDur�esMini()
    mPhases.CalculerDur�esMini()
    Capacit�ACalculer = True
  End Sub

  '********************************************************************************************
  ' Rouge incompressible de la ligne de feux : 
  ' le feu passera au rouge n secondes avant la fin de la phase
  'C'est le + grand rouge de d�gagement de la ligne /ensemble des lignes de la phase suivante
  '********************************************************************************************
  Public Overrides Property RougeIncompressible(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mRougeIncompressible.Item(uneLigneFeux)
    End Get
    Set(ByVal Value As Short)
      mRougeIncompressible.Item(uneLigneFeux) = Value
    End Set
  End Property
#End Region

#Region "PlansPhasage"
  Friend ReadOnly Property PlansPourPhasage() As PlanFeuxCollection
    Get
      Return mPlansPourPhasage
    End Get
  End Property
  Public Property PlanPhasageAssoci�() As PlanFeuxPhasage
    Get
      Return mPlanPhasageAssoci�
    End Get
    Set(ByVal Value As PlanFeuxPhasage)
      Dim exPlanPhasage As PlanFeuxPhasage = mPlanPhasageAssoci�

      If Not mPlanPhasageAssoci� Is Value Then
        mPlanPhasageAssoci� = Value
        If IsNothing(Value) Then
          'Le clear sert de drapeau pour indiquer que l'organisation du phasage n'est encore pas retenu
          mPhases.Clear()
          'La demande sera � recalculer si on choisit une organisation diff�rente du phasage
          mDemande = -1
          If Not IsNothing(exPlanPhasage) Then
            'D�solidariser aussi le plan de phasage associ�
            exPlanPhasage.PlanBaseAssoci� = Nothing
          End If

        Else
          mPlanPhasageAssoci�.PlanBaseAssoci� = Me
          IndexerLignesFeux()
          CalculerDur�esMini()
        End If
      End If
    End Set
  End Property

  Public Overrides Sub IndexerLignesFeux()
    Dim unPlanFeux As PlanFeuxFonctionnement

    If PhasageRetenu Then
      MyBase.IndexerLignesFeux()

      For Each unPlanFeux In Me.mPlansFonctionnement
        unPlanFeux.IndexerLignesFeux()
      Next
    End If

  End Sub

  Public ReadOnly Property PhasageRetenu() As Boolean
    Get
      Return mPhases.Count > 0
      'ou bien : en fait n'est pas vrai lors du chargement du projet
      'Return Not IsNothing(mPlanPhasageAssoci�)
    End Get
  End Property

  Public Sub R�initialiserPhasage()
    PlanPhasageAssoci� = Nothing
    mPlansFonctionnement.Clear()

  End Sub

  Public Function Equivalent(ByVal unPlanPourPhasage As PlanFeuxBase) As Boolean
    Dim unePhase, unePhase2 As Phase
    Dim nbPhasesEq As Short

    If mPhases.Count = unPlanPourPhasage.mPhases.Count Then
      For Each unePhase In mPhases
        For Each unePhase2 In unPlanPourPhasage.mPhases
          If unePhase.Equivalente(unePhase2) Then
            nbPhasesEq += 1
          End If
        Next
      Next

      Return nbPhasesEq = mPhases.Count
    End If

  End Function

#Region " InitPhasage"
  'Private Sub AfficherSc�nar(ByVal Sc�narios As TreeNodeCollection)
  '  Dim i As Integer
  '  Dim unSc�nario As TreeNode
  '  Dim NoeudPhase As TreeNode
  '  Dim NoeudFeu As TreeNode

  '  For Each unSc�nario In Sc�narios
  '    Debug.WriteLine("Sc�nario " & unSc�nario.Text)
  '    For Each NoeudPhase In unSc�nario.Nodes
  '      Debug.WriteLine("   " & NoeudPhase.Text)
  '      For Each NoeudFeu In NoeudPhase.Nodes
  '        Debug.WriteLine("   " & NoeudFeu.Text)
  '      Next
  '    Next
  '  Next

  'End Sub

  '***********************************************************************************************
  ' Construire les diff�rents sc�narios de phasage � partir de la matrice des conflits
  ' Fonction appel�e lors du verrouillage de la matrice des conflits afin de v�rifier qu'au moins
  ' une organisation est possible
  ' Appel�e �galement lors de la lecture d'un projet(InitPhasage)
  '     -soit les conflits sont verrouill�s : activation de l'onglet plans de feux
  '     -soit le phasage est retenu : cochage du phasage retenu
  '***********************************************************************************************
  Public Sub ConstruirePlansDePhasage()

    Dim trn As New TreeNode

    Dim Sc�narios As TreeNodeCollection = trn.Nodes  ' Me.TreeView1.Nodes		
    Sc�narios.Clear()
    PlansPourPhasage.Clear()

    Dim Sc�nario As TreeNode
    Dim NoeudPhase As TreeNode
    Dim NoeudFeu As TreeNode

    Dim uneLigneFeux As LigneFeux

    'Classer les lignes de feux en mettant d'abord les lignes de feux v�hicules

    Try

      'Traiter en 1er les lignes de feux v�hicules
      For Each uneLigneFeux In mLignesFeux
        If Not uneLigneFeux.EstPi�ton Then
          Ins�rerFeuDansPhases(uneLigneFeux, Sc�narios)
        End If
        If Sc�narios.Count = 0 Then Exit For
      Next

      If Sc�narios.Count > 0 Then
        'Traiter ensuite les lignes de feux pi�tons
        For Each uneLigneFeux In mLignesFeux
          If uneLigneFeux.EstPi�ton Then
            Ins�rerFeuDansPhases(uneLigneFeux, Sc�narios)
            If Sc�narios.Count = 0 Then Exit For
          End If
        Next

        'Construire les plans de feux de base  � partir des sc�narios
        Dim unPlanFeux As PlanFeuxPhasage
        Dim unePhase As Phase

        For Each Sc�nario In Sc�narios
          'Instancier un plan de feux
          unPlanFeux = New PlanFeuxPhasage(Me)
          unPlanFeux.Trafic = mTrafic
          For Each NoeudPhase In Sc�nario.Nodes
            'Ajouter la phase dans le plan
            unePhase = New Phase
            For Each NoeudFeu In NoeudPhase.Nodes
              'Ajouter la ligne de feux dans la phase
              uneLigneFeux = mLignesFeux(NoeudFeu.Text)
              unePhase.mLignesFeux.Add(uneLigneFeux)
            Next
            unPlanFeux.AddPhases(unePhase)
          Next NoeudPhase

          'Ajouter le plan � la collection
          PlansPourPhasage.Add(unPlanFeux)

        Next Sc�nario

        'Ajouter les sc�narios comportant des lignes de feux sur 2 phases
        RechercherCompatibilit�Sur2Phases()

        For Each unPlanFeux In PlansPourPhasage
          ' Rechercher si le plan pour phasage correspond au plan de base, afin de les associer
          RechercherPlanBaseEquivalent(unPlanFeux)

          unPlanFeux.D�terminerPhaseSp�ciale()
          unPlanFeux.IndexerLignesFeux()
        Next

      End If  ' Sc�narios.Count > 0

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "ConstruirePlansDePhasage")
    End Try

  End Sub

  Public Sub CalculerCapacit�sPlansPhasage()
    Dim unPlanFeux As PlanFeuxPhasage
    Dim uneDur�eCycle As Short

    mPlansPourPhasage.CalculerDur�esMini()

    If AvecTrafic() Then

      For Each unPlanFeux In PlansPourPhasage
        With unPlanFeux
          .CalculerDemande()
          If .Demande <= mVariante.D�bitSaturation Then
            '            uneDur�eCycle = .CalculCycle(CoefDemande:=0)
            'MODIF AV(26/03/07) : il faut calculer ces capacit�s avec lea m�thode de Webster : Point Plan de feux-13 du Document de suivi)
            uneDur�eCycle = .CalculCycle()
            If uneDur�eCycle <> 0 Then
              'Sinon, la demande aboutit � une dur�e sup�rieure � la dur�e admissible : ce plan devrait �tre �limin� + tard
              .R�partirDur�eCycle(uneDur�eCycle)
              .CalculerR�serveCapacit�()
            End If
          End If
        End With
      Next
    End If

  End Sub

  Private Sub RechercherCompatibilit�Sur2Phases()
    Dim PlansSuppl�mentaires As New PlanFeuxCollection
    Dim PlansTemporaires As New PlanFeuxCollection
    Dim unPlanFeux, unPlanFeux2, unPlanFeux3 As PlanFeuxPhasage
    Dim unePhase As Phase
    Dim uneLigneFeux As LigneFeux
    Dim IndexPhase As Short
    Dim EliminerDoublon As Boolean

    Try
      For Each unPlanFeux In PlansPourPhasage
        If unPlanFeux.mPhases.Count > 2 Then
          'Sur 2 phases �� n'a pas de sens qu'une ligne de feux soit sur les 2 phases(le feu serait toujours vert)
          For Each unePhase In unPlanFeux.mPhases
            IndexPhase = unPlanFeux.mPhases.IndexOf(unePhase)
            For Each uneLigneFeux In unePhase.mLignesFeux
              'Rechercher la compatibilit� de la LF avec la phase suivante
              IndexPhase = (IndexPhase + 1) Mod unPlanFeux.mPhases.Count
              If Not PlanCompatible2Phases(unPlanFeux, uneLigneFeux, IndexPhase, PlansTemporaires) Then
                'Rechercher la compatibilit� de la LF avec la phase pr�c�dente
                IndexPhase = (IndexPhase + 1) Mod unPlanFeux.mPhases.Count
                PlanCompatible2Phases(unPlanFeux, uneLigneFeux, IndexPhase, PlansTemporaires)
              End If
              'Se repositionner sur la phase en cours d'analyse
              IndexPhase = unPlanFeux.mPhases.IndexOf(unePhase)
            Next uneLigneFeux
          Next unePhase

          For Each unPlanFeux2 In PlansTemporaires
            EliminerDoublon = False
            For Each unPlanFeux3 In PlansSuppl�mentaires
              'Rechercher si dans les plans suppl�mentaires d�j� trouv�s, '
              'il n'y a pas un plan �quivalent � celui qu'on vient de trouver
              If unPlanFeux2.Equivalent(unPlanFeux3) Then
                EliminerDoublon = True
              End If
            Next
            If Not EliminerDoublon Then PlansSuppl�mentaires.Add(unPlanFeux2)
          Next
          PlansTemporaires.Clear()

        End If

      Next unPlanFeux

      For Each unPlanFeux2 In PlansSuppl�mentaires
        unPlanFeux2.Trafic = mTrafic
        PlansPourPhasage.Add(unPlanFeux2)
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "RechercherCompatibilit�Sur2Phases")
    End Try
  End Sub

  Private Function PlanCompatible2Phases(ByVal unPlanFeux As PlanFeuxPhasage, ByVal uneLigneFeux As LigneFeux, _
                  ByVal IndexPhase As Short, ByVal PlansSuppl�mentaires As PlanFeuxCollection) As Boolean
    Dim unPlanFeux2, unPlanFeux3 As PlanFeuxPhasage
    Dim desLignesFeux As LigneFeuxCollection = unPlanFeux.mPhases(IndexPhase).mLignesFeux
    Dim Ins�r� As Boolean

    If Compatibilit�Possible(uneLigneFeux, desLignesFeux) Then
      'Cloner le plan de feux et y ajouter la ligne de feux dans la phase suivante
      unPlanFeux2 = New PlanFeuxPhasage(unPlanFeux)
      unPlanFeux2.mLigneFeuxMultiPhases = True
      desLignesFeux = unPlanFeux2.mPhases(IndexPhase).mLignesFeux
      desLignesFeux.PositionInsertion(uneLigneFeux, mLignesFeux)

      If Not unPlanFeux2.mPhases.EquivalentDeuxPhases Then

        If PlansSuppl�mentaires.Count = 0 Then
          PlansSuppl�mentaires.Add(unPlanFeux2)
        Else
          For Each unPlanFeux3 In PlansSuppl�mentaires
            With unPlanFeux3
              desLignesFeux = unPlanFeux3.mPhases(IndexPhase).mLignesFeux
              If Compatibilit�Possible(uneLigneFeux, desLignesFeux) Then
                desLignesFeux.PositionInsertion(uneLigneFeux, mLignesFeux)
                'd�s qu'on a r�ussi � ins�rer la ligne dans au moins un plan suppl�mentaire :on s'arr�te l� (sinon on peut arriver � avoir une pl�thore de plans)
                If .mPhases.EquivalentDeuxPhases Then
                  desLignesFeux.Remove(uneLigneFeux)
                Else
                  '                  Ins�r� = True
                End If
              End If
            End With
          Next
          If Not Ins�r� Then
            PlansSuppl�mentaires.Add(unPlanFeux2)
          End If
        End If

        Return True

      End If
    End If

  End Function

  Private Function Compatibilit�Possible(ByVal uneLigneFeux As LigneFeux, ByVal desLignesFeux As LigneFeuxCollection) As Boolean
    Dim uneLigneFeux2 As LigneFeux

    For Each uneLigneFeux2 In desLignesFeux
      If mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneFeux2) Then
        Return False
      End If
    Next uneLigneFeux2

    Return True
  End Function

  Public Sub CalculerDur�esMiniPlansFeux()
    mPlansPourPhasage.CalculerDur�esMini()
    CalculerDur�esMini()
  End Sub

  '****************************************************************************************
  'Compl�mentOrganiserPhasage : Rehcerhcer les plans pour phasage trop longs
  'PhasagesAConstruire : Construire la collection mPlansPourPhasage
  'Ceci doit �tre fait lors de la r�ouverture d'un fichier 
  'Autrement les phasages sont construits lors du verrouillage de la matrice des conflits
  '*****************************************************************************************
  Public Sub Compl�mentOrganiserPhasage(ByVal PhasagesAConstruire As Boolean)
    Dim unPlanPourPhasage As PlanFeuxBase

    Try

      If PhasagesAConstruire Then
        'Lecture d'un projet existant suffisamment avanc�
        ConstruirePlansDePhasage()

      Else

        If AvecTrafic() Then
          Dim Garbage As New PlanFeuxCollection

          'Calculer la demande du carrefour (s'il y a au moins un trafic de d�fini : le trafic de r�f�rence)
          For Each unPlanPourPhasage In PlansPourPhasage

            With unPlanPourPhasage
              If .Dur�eCycle = 0 Then
                'la m�thode classique ne sait pas faire le calcul avec un tel trafic (>1800)
                Garbage.Add(unPlanPourPhasage)
              Else
                If .Dur�eCycle > PlanFeux.maxiDur�eCycleAbsolue Then
                  'Demande CERTU 07/07/06 : Point 4 du �Plan de feux
                  Garbage.Add(unPlanPourPhasage)
                End If
              End If
            End With
          Next

          For Each unPlanPourPhasage In Garbage
            PlansPourPhasage.Remove(unPlanPourPhasage)
          Next
        End If
      End If

      mTroisPhasesSeulement = True
      For Each unPlanPourPhasage In PlansPourPhasage
        If unPlanPourPhasage.mPhases.Count = 2 Then
          mTroisPhasesSeulement = False
        End If
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Compl�mentOrganiserPhasage")

    End Try

  End Sub

  'D�terminer si le plan pour phasage correspond � un plan de feux de base
  ' Associe le plan de feux de base s'il est trouv�
  '**********************************************************************************************************************
  Private Sub RechercherPlanBaseEquivalent(ByVal unPlanPourPhasage As PlanFeuxBase)
    Dim unePhase As Phase

    If Equivalent(unPlanPourPhasage) Then
      'Plan de feux de base correspondant trouv� : on va les associer
      For Each unePhase In mPhases
        With unPlanPourPhasage.mPhases
          .D�placer(.PhaseEquivalente(unePhase), mPhases.IndexOf(unePhase))
        End With
      Next
      PlanPhasageAssoci� = unPlanPourPhasage
    End If

  End Sub

  '**********************************************************************************************************************
  ' Ins�rer une ligne de feux dans chaque phase o� c'est possible
  '**********************************************************************************************************************
  Private Sub Ins�rerFeuDansPhases(ByVal uneLigneFeux As LigneFeux, ByVal Sc�narios As TreeNodeCollection)
    Dim unSc�nario As TreeNode
    Dim Sc�narioClon� As TreeNode
    Dim NoeudPhase As TreeNode
    Dim NoeudFeu As TreeNode
    Dim i As Integer

    Try
      If Sc�narios.Count = 0 Then
        'Cr�er le 1er sc�nario : le libell� d'1 noeud sc�nario est  "Sn"
        unSc�nario = Sc�narios.Add("S1")

        'Cr�er la 1�re phase du 1er sc�nario : Le libell� d'un noeud phase est "Phasen"
        unSc�nario.Nodes.Add("Phase1")
        'Mettre la ligne de feux dans la 1�re phase
        AjouterFeuDansPhase(uneLigneFeux, unSc�nario.FirstNode)

      Else
        Dim Sc�nariosImpossibles As New Hashtable
        For Each unSc�nario In Sc�narios
          'Par d�faut : aucune phase ne convient
          unSc�nario.Tag = 0
          For Each NoeudPhase In unSc�nario.Nodes
            'Par d�faut tous les feux de la phase sont compatibles
            NoeudPhase.Tag = 0
            For Each NoeudFeu In NoeudPhase.Nodes
              If mLignesFeux.EstIncompatible(uneLigneFeux, mLignesFeux(NoeudFeu.Text)) Then
                NoeudPhase.Tag = -1
                Exit For
              End If
            Next
            If NoeudPhase.Tag = 0 Then
              'Le feu peut �tre mis dans cette phase de ce sc�nario
              unSc�nario.Tag += 1      'Une phase de + convient pour ce sc�nario
              NoeudPhase.Tag = unSc�nario.Tag
            End If
          Next

          Select Case unSc�nario.Tag
            Case 0
              'Aucune phase ne convient pour ce feu dans ce sc�nario
              If unSc�nario.LastNode.Index = MAXPHASES - 1 Then
                'On ne peut plus ajouter de phase : ce sc�nario doit �tre abandonn�
                Sc�nariosImpossibles.Add(Sc�nariosImpossibles.Count, unSc�nario)
              Else
                'Ajouter une phase et y mettre la ligne de feux
                AjouterPhaseEtFeu(unSc�nario, uneLigneFeux)
              End If

            Case Else
              If unSc�nario.LastNode.Index <= MAXPHASES - 2 Then
                'Cloner le sc�nario
                Sc�narioClon� = Sc�narioAjout�(Sc�narios, unSc�nario)
                'Ajouter une phase et y mettre la ligne de feux
                AjouterPhaseEtFeu(Sc�narioClon�, uneLigneFeux)
              End If

              'Si plusieurs phases conviennent : cloner d'abord le sc�nario et mettre la ligne de feux dans les phases suivantes qui conviennent
              For i = 2 To unSc�nario.Tag
                Sc�narioClon� = Sc�narioAjout�(Sc�narios, unSc�nario)
                AjouterFeuDansPhase(uneLigneFeux, Sc�narioClon�.Nodes, i)
              Next

              'Mettre le feu dans la 1�re phase qui convient dans le sc�nario
              AjouterFeuDansPhase(uneLigneFeux, unSc�nario.Nodes, 1)

          End Select

        Next

        For i = 0 To Sc�nariosImpossibles.Count - 1
          unSc�nario = Sc�nariosImpossibles(i)
          unSc�nario.Remove()
        Next
        Sc�nariosImpossibles.Clear()
      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "Ins�rerFeuDansPhases")
    End Try

  End Sub
  '**********************************************************************************************************************
  ' Retourne le sc�nario de phasage ajout� � la liste des sc�narios
  ' Sc�narios : Liste des sc�narios
  ' Sc�nario : Sc�nario � cloner
  '**********************************************************************************************************************
  Private Function Sc�narioAjout�(ByVal Sc�narios As TreeNodeCollection, ByVal Sc�nario As TreeNode) As TreeNode

    Sc�narioAjout� = CType(Sc�nario.Clone, TreeNode)
    Sc�narios.Add(Sc�narioAjout�)
    Sc�narioAjout�.Text = "S" & CStr(Sc�narios.Count)

  End Function
  '**********************************************************************************************************************
  ' Ajouter une phase � un sc�nario et y Ins�rer le feu
  '**********************************************************************************************************************
  Private Sub AjouterPhaseEtFeu(ByVal Sc�nario As TreeNode, ByVal uneLigneFeux As LigneFeux)
    With Sc�nario
      AjouterFeuDansPhase(uneLigneFeux, .Nodes.Add("Phase" & CStr(.LastNode.Index + 2)))
    End With
  End Sub

  '**********************************************************************************************************************
  ' Rechercher la phase o� ins�rer le feu et l'ins�rer dans celle-ci
  ' NoeudsPhases : Liste des phases � explorer
  ' Index : Drapeau permettant de rep�rer la bonne phase
  '**********************************************************************************************************************
  Private Overloads Sub AjouterFeuDansPhase(ByVal uneLigneFeux As LigneFeux, ByVal Noeuds As TreeNodeCollection, ByVal Index As Integer)
    Dim NoeudPhase As TreeNode

    For Each NoeudPhase In Noeuds
      If NoeudPhase.Tag = Index Then
        AjouterFeuDansPhase(uneLigneFeux, NoeudPhase)
        Exit For
      End If
    Next

  End Sub

  '**********************************************************************************************************************
  ' Ajouter le feu dans  une phase  et m�moriser dans le treenode s'il est pi�ton ou v�hicule
  '**********************************************************************************************************************
  Private Overloads Sub AjouterFeuDansPhase(ByVal uneLigneFeux As LigneFeux, ByVal NoeudPhase As TreeNode)
    NoeudPhase.Nodes.Add(uneLigneFeux.ID).Tag = uneLigneFeux.EstPi�ton
  End Sub

#End Region

#End Region
  '**********************************************************************
  'D�terminer les autorisations de d�calage pour les lignes de feux du plan
  '**********************************************************************
  Public Sub D�terminerAutorisationsD�calage()
    Dim unePhase As Phase
    Dim uneLigneFeux As LigneFeux

    For Each unePhase In mPhases
      For Each uneLigneFeux In unePhase.mLignesFeux
        uneLigneFeux.D�terminerAutorisationD�calage(unePhase)
      Next
    Next
  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe FiltrePhasage --------------------------
'=====================================================================================================
Public Class FiltrePhasage
  Public TroisPhases As Boolean
  Public Crit�reCapacit� As Capacit�Enum
  Public LigneFeuxMultiPhases As LFMultiphasesEnum
  Public AvecPhaseSp�ciale As PhaseSp�cialeEnum

  Public Enum Capacit�Enum
    Aucun
    MoinsDix
    DixVingt
    PlusVingt
  End Enum

  Public Enum LFMultiphasesEnum
    Inclure
    Exclure
    Uniquement
  End Enum

  Public Enum PhaseSp�cialeEnum
    Inclure
    Exclure
    Uniquement
  End Enum
End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxPhasage --------------------------
'=====================================================================================================
Public Class PlanFeuxPhasage : Inherits PlanFeuxBase
  Private mPlanBaseAssoci� As PlanFeuxBase

  Public Sub New(ByVal unPlanFeux As PlanFeuxPhasage)
    'Cr�ation d'un plan de phasage identique en vue de rajouter des lignes de feux sur + d'une phase
    MyBase.New(unPlanFeux)

    mPlanParent = unPlanFeux
    '    mDemandeUVP = New Hashtable
  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeuxBase)
    'Cr�ation d'un plan de phasage associ� � un plan de feux de base
    mPlanParent = unPlanFeux
    '    mDemandeUVP = New Hashtable
  End Sub

  '*************************************************************************
  ' Plande feux de base �quivalent au plan de phasage
  '*************************************************************************
  Public Property PlanBaseAssoci�() As PlanFeuxBase
    Get
      Return mPlanBaseAssoci�
    End Get
    Set(ByVal Value As PlanFeuxBase)
      Dim exPlanBase As PlanFeuxBase = mPlanBaseAssoci�

      If Not mPlanBaseAssoci� Is Value Then
        mPlanBaseAssoci� = Value
        If IsNothing(Value) Then
          If Not IsNothing(exPlanBase) Then
            exPlanBase.PlanPhasageAssoci� = Nothing
          End If
        Else
          mPlanBaseAssoci�.PlanPhasageAssoci� = Me
        End If
      End If
    End Set
  End Property

  Public Overrides ReadOnly Property mLignesFeux() As LigneFeuxCollection
    Get
      Return mPlanParent.mLignesFeux
    End Get
  End Property
End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxFonctionnement --------------------------
'=====================================================================================================
Public Class PlanFeuxFonctionnement : Inherits PlanFeux

  Public mPlanBase As PlanFeuxBase
  Private mRetardMoyen As New Hashtable
  Private mNbV�hiculesEnAttente As New Hashtable

#Region "Dur�esMini"
  '********************************************************************************************
  'Dur�eMini : Dur�e minimum du cycle du plan de feux
  '********************************************************************************************
  Public Overrides ReadOnly Property Dur�eMini() As Short
    'Dur�e mini = dur�e du cycle du plan de feux de base(verrouill�e)
    Get
      Dur�eMini = mPlanBase.Dur�eCycle
    End Get
  End Property

  Public Overrides Property RougeIncompressible(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mPlanBase.RougeIncompressible(uneLigneFeux)
    End Get
    Set(ByVal Value As Short)
      'Valeur jamais affect�e car c'est celle du plan de feux de base correspondant
    End Set
  End Property

#End Region

#Region "TempsAttente"
  '**********************************************************************
  ' Temps moyen d'attente v�hicule sur l'ensemble des lignes du carrefour
  ' R�f�rence : 'Compl�ments de calcul' du CERTU (28/11/2005)
  '**********************************************************************
  Public Function TMAV�hicules() As Integer
    Dim uneLigneFeux As LigneFeux
    Dim SommeRetards, SommeDemandes, DemandeLF As Single

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstV�hicule Then
        With CType(uneLigneFeux, LigneFeuV�hicules)
          'SommeRetards += (DemandeUVP(uneLigneFeux) * .Voies.Count * RetardMoyen(uneLigneFeux))
          DemandeLF = DemandeUVP(uneLigneFeux) * .Voies.Count
          SommeDemandes += DemandeLF
          SommeRetards += DemandeLF * RetardMoyen(uneLigneFeux)
        End With
      End If
    Next

    Return CType(SommeRetards / SommeDemandes, Integer)
    'Return CType(SommeRetards / Me.Trafic.QTotal(Trafic.TraficEnum.UVP), Integer)

  End Function

  '**********************************************************************
  ' Temps moyen d'attente pi�tons sur l'ensemble des lignes du carrefour
  ' R�f�rence : aucune
  '**********************************************************************
  Public Function TMAPi�tons() As Integer
    Dim uneLigneFeux As LigneFeux
    Dim SommeRetards As Single
    Dim nbLignesPi�tons As Short

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstPi�ton Then
        nbLignesPi�tons += 1
        With CType(uneLigneFeux, LigneFeuPi�tons)
          SommeRetards += RetardMoyen(uneLigneFeux) * Me.Trafic.QPi�ton(uneLigneFeux.mBranche)
        End With
      End If
    Next

    If nbLignesPi�tons > 0 And Me.Trafic.QPi�tonTotal Then
      'Le trafic pi�ton peut n'�tre pas renseign�
      Return CType(SommeRetards / Me.Trafic.QPi�tonTotal, Integer)
    End If

  End Function

  Public Sub AffecterInfosAttente()
    Dim uneLigneFeux As LigneFeux
    Dim uneDur�eCycle As Short = Dur�eCycle()

    For Each uneLigneFeux In mLignesFeux
      AffecterInfosAttente(uneLigneFeux, uneDur�eCycle)
    Next

  End Sub

  Public Sub AffecterInfosAttente(ByVal uneLigneFeux As LigneFeux, ByVal uneDur�eCycle As Short)

    ' R�f�rence : 'Compl�ments de calcul' du CERTU (28/11/2005) : Longueur de file d'attente

    NbV�hiculesEnAttente(uneLigneFeux) = Math.Ceiling(DemandeUVP(uneLigneFeux) / 3600 * (uneDur�eCycle - VertUtile(uneLigneFeux)))

    AffecterRetardMoyen(uneLigneFeux, Dur�eCycle)
  End Sub

  '***********************************************************************
  ' Calculer le retard moyen subi par un pi�ton ou
  ' par un v�hicule sur une file de la ligne de feux
  ' R�f�rence : 'Compl�ments de calcul' du CERTU (28/11/2005)
  '***********************************************************************
  Protected Sub AffecterRetardMoyen(ByVal uneLigneFeux As LigneFeux, ByVal uneDur�eCycle As Short)
    'La demande est=0 pour les pi�tons

    RetardMoyen(uneLigneFeux) = Math.Ceiling(Carr�((uneDur�eCycle - VertUtile(uneLigneFeux))) / _
    (2 * uneDur�eCycle * (1 - (DemandeUVP(uneLigneFeux) / mVariante.D�bitSaturation))) _
    )

  End Sub

  Public Property NbV�hiculesEnAttente(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mNbV�hiculesEnAttente(uneLigneFeux.ID)
    End Get
    Set(ByVal Value As Short)
      mNbV�hiculesEnAttente(uneLigneFeux.ID) = Value
    End Set
  End Property

  Public Property RetardMoyen(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mRetardMoyen(uneLigneFeux.ID)
    End Get
    Set(ByVal Value As Short)
      mRetardMoyen(uneLigneFeux.ID) = Value
    End Set
  End Property

  Public ReadOnly Property LgFileAttente(ByVal uneLigneFeux As LigneFeux) As Integer
    Get
      'on compte 5 m par v�hicule en attente(uvp)
      Return NbV�hiculesEnAttente(uneLigneFeux) * LgMoyenneV�hicule
    End Get
  End Property


  'Public Function CalculClassique(ByVal R�serveCapacit� As Single) As Short
  '  'Demande major�e d'une marge de trafic suppl�mentaire acceptable(0 - 0.10 - 0.15 - 0.20)
  '  Dim DemandePriseEncompte As Short = (1 + R�serveCapacit�) * mDemande

  '  Dim COptimum, Capacit� As Short
  '  Dim unePhase As Phase
  '  Dim i, j As Short
  '  Dim Capacit�Inf�rieure, Capacit�Sup�rieure As Short
  '  Dim IndiceCycleMaxi As Short = TbCycleCapacit�.GetUpperBound(0)

  '  Dim TempsPerdu As Short = mPhases.TempsPerdu

  '  If TempsPerdu < TempsPerduMini Or TempsPerdu >= TempsPerduMini + TbCycleCapacit�.Length Then
  '    'Hors Abaque
  '  Else
  '    Capacit�Inf�rieure = TbCycleCapacit�(0, TempsPerdu - TempsPerduMini)
  '    If DemandePriseEncompte <= Capacit�Inf�rieure Then
  '      Return Dur�eCycleMini
  '    Else
  '      For i = 1 To IndiceCycleMaxi
  '        Capacit�Sup�rieure = TbCycleCapacit�(i, TempsPerdu - TempsPerduMini)
  '        If DemandePriseEncompte <= Capacit�Sup�rieure Then
  '          Return CType(Dur�eCycleMini + Incr�mentCycle * (i - 1 + (DemandePriseEncompte - Capacit�Inf�rieure) / (Capacit�Sup�rieure - Capacit�Inf�rieure)), Short)
  '        End If
  '        Capacit�Inf�rieure = Capacit�Sup�rieure
  '      Next
  '    End If

  '    Return Dur�eCycleMini + Incr�mentCycle * IndiceCycleMaxi

  '  End If

  'End Function
#End Region

#Region "Constructeurs"
  '**************************************************************
  ' Constructeur 
  '**************************************************************
  Public Sub New(ByVal unPlanFeux As PlanFeux, ByVal NomPlan As String, Optional ByVal pBase As PlanFeuxBase = Nothing)
    MyBase.New(unPlanFeux)

    If TypeOf unPlanFeux Is PlanFeuxBase Then
      'Plan de feux de fonctionnement bas� sur le plan de base unPlanFeux
      mPlanBase = unPlanFeux
    ElseIf IsNothing(pBase) Then
      'Plan de feux de fonctionnement dupliqu� � partir du plan de feux de fonctionnement unPlanFeux
      Dim unPlanFeuxFct As PlanFeuxFonctionnement = unPlanFeux
      mPlanBase = unPlanFeuxFct.mPlanBase
    Else
      'Plan de feux de fonctionnement issu de la duplication de sc�nario
      mPlanBase = pBase
    End If

    'Dimensionner()
    IndexerLignesFeux()

    'Ajouter le plan � la collection 
    mPlanBase.mPlansFonctionnement.Add(Me)

    mNom = NomPlan

  End Sub

  Public Sub New(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow, ByVal unPlanFeuxBase As PlanFeuxBase)
    MyBase.New(uneRowPlanFeux)
    mPlanBase = unPlanFeuxBase

    'Dimensionner les diff�rents tableaux selon le nombre de lignes de feux
    'Dimensionner()

    'Ajouter le plan � la collection 
    mPlanBase.mPlansFonctionnement.Add(Me)
    mPlanBase.ReCr�erTrafics()

    Nom = uneRowPlanFeux.NomFonctionnement

  End Sub


  ''********************************************************************************************
  ''Dimensionner les diff�rents tableaux selon le nombre de lignes de feux
  ''********************************************************************************************
  'Protected Overrides Sub Dimensionner()

  '  'Dimensionner mDemandeUVP
  '  'MyBase.Dimensionner()

  '  'Dimensionner les autres tableaux
  '  ReDim mNbV�hiculesEnAttente(mLignesFeux.Count - 1)
  '  ReDim mRetardMoyen(mLignesFeux.Count - 1)

  'End Sub

#End Region

  Public Overrides Function Enregistrer(ByVal uneVariante As Variante, ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.PlanFeuxRow
    'Appeler la fonction de la classe de base
    Dim uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow = MyBase.Enregistrer(uneVariante, uneRowVariante)

    With uneRowPlanFeux
      'R�f�rencer le plan de feux de base associ�
      .ID = mPlanBase.Nom
      .NomFonctionnement = mNom
    End With

  End Function

#Region "Propri�t�s"
  Public Overrides ReadOnly Property mLignesFeux() As LigneFeuxCollection
    Get
      Return mPlanBase.mLignesFeux
    End Get
  End Property

  Public Overrides Property Nom() As String
    Get
      Return mNom
    End Get
    Set(ByVal Value As String)
      mNom = Value
    End Set
  End Property

  Public Overrides Property Trafic() As Trafic
    Get
      Return MyBase.Trafic
    End Get
    Set(ByVal Value As Trafic)
      MyBase.Trafic = Value
      If Not IsNothing(mPlanBase) Then
        mPlanBase.ReCr�erTrafics()
      End If
    End Set
  End Property
#End Region

End Class