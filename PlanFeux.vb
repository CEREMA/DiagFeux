'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux								'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
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
Public MustInherit Class PlanFeux : Inherits Métier

#Region "Déclarations"
  Public Const maxiDuréeCycleAbsolue As Short = 130
  Public Const maxiDuréeCycle As Short = 120
  Public Const DuréeCycleDéfaut As Short = 70

  'Nom du plan de feux 
  Protected mNom As String

  Protected mTrafic As Trafic

  Protected mDemande As Integer
  Protected mDemandeUVP As New Hashtable

  Protected mCapacitéThéorique As Single
  'Réerve de capacité calculée en fonction de la durée du cycle
  Private mRéserveCapacité As Single
  Protected mStockage As Short
  Protected mCapacitéACalculer As Boolean = True

  Public mPhases As New PhaseCollection(Me)
  Public mVariante As Variante

  Private mDécalages(1) As Hashtable
  Private dctPhasesLf As Hashtable
  Public Enum Décalage
    Ouverture
    Fermeture
  End Enum
  Public Enum Position
    Aucune = -1
    Unique
    Première
    Dernière
  End Enum

  Public Marges As Point
  Public IntervalX, IntervalY As Single

  Public MustOverride ReadOnly Property DuréeMini() As Short
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
  '	Déterminer la demande du carrefour
  ' Réf :  § d) et e) - p27 du guide carrefour à feux
  ' La demande est 
  '       - la somme des demandes prépondérantes par phase
  '       - indépendante de la durée du cycle
  '       - dépendante de la période de trafic
  '***********************************************************************
  Private Function DemandeDuCarrefour() As Integer
    Dim unePhase, unePhaseSuivante As Phase
    Dim uneLigneFeux As LigneFeux
    Dim qPondéré As Integer
    Dim CourantPrépondérant As New Hashtable
    'Dictionnaire pour stocker les lignes de feux sur 2 phases
    Dim LignesPhases As New Hashtable

    For Each unePhase In mPhases
      CourantPrépondérant(unePhase) = 0

      Try

        For Each uneLigneFeux In unePhase.mLignesFeux
          If uneLigneFeux.EstVéhicule Then

            qPondéré = TraficPondéré(CType(uneLigneFeux, LigneFeuVéhicules))
            DemandeUVP(uneLigneFeux) = qPondéré

            Select Case PositionDansPhase(uneLigneFeux, unePhase)
              Case Position.Unique
                '	trafic prépondérant de la phase
                CourantPrépondérant(unePhase) = Math.Max(CourantPrépondérant(unePhase), qPondéré)
              Case Position.Première
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
      'Rechercher si la présence de lignes de feux multiphases n'oblige pas 
      ' à redéfinir les courants prépondérants 
      If LignesPhases.ContainsKey(uneLigneFeux) Then
        unePhase = LignesPhases(uneLigneFeux)
        unePhaseSuivante = mPhases.PhaseSuivante(unePhase)
        dLigneFeux = DemandeUVP(uneLigneFeux)

        If dLigneFeux > CourantPrépondérant(unePhase) + CourantPrépondérant(unePhaseSuivante) Then
          'Sinon, la ligne de feux multiphase s'écoule de façon masquée : on ne fait rien
          dMin = Math.Min(CourantPrépondérant(unePhase), CourantPrépondérant(unePhaseSuivante))
          dMax = Math.Max(CourantPrépondérant(unePhase), CourantPrépondérant(unePhaseSuivante))
          If dLigneFeux > 2 * dMax Then
            'Partager la demande de la ligne sur 2 phases en 2 parts égales sur les 2 phases
            CourantPrépondérant(unePhase) = dLigneFeux / 2
            CourantPrépondérant(unePhaseSuivante) = dLigneFeux / 2
          Else
            'Conserver la demande pour la phase la + importante
            'Attribuer le reste à l'autre phase
            If CourantPrépondérant(unePhase) = dMin Then
              CourantPrépondérant(unePhase) = dLigneFeux - dMax
            Else
              CourantPrépondérant(unePhase) = dLigneFeux - dMax
            End If
          End If


        End If
      End If
    Next

    For Each unePhase In mPhases
      'Mémoriser dans la phase le trafic le + important qu'elle supporte
      unePhase.TraficSupporté = CourantPrépondérant(unePhase)
      'Cumuler les trafics supportés
      DemandeDuCarrefour += unePhase.TraficSupporté
    Next

  End Function

  Private Function TraficPondéré(ByVal uneLigneFeux As LigneFeuVéhicules) As Integer
    If mVariante.ModeGraphique Then
      Return uneLigneFeux.TraficPondéréRiche(mTrafic)
    Else
      Return uneLigneFeux.TraficPondéréRiche(mTrafic)
    End If

  End Function

  '**********************************************************************************************
  ' Calculer la réserve de capacité du plan de feux en connaissant la durée de son cycle
  ' Référence : Guide Carrefour à feux du CERTU - p 28 et 29
  '**********************************************************************************************
  Public Sub CalculerRéserveCapacité()
    Dim uneDurée As Short = DuréeCycle()

    ' Formule p27
    CapacitéThéorique = CType(mVariante.DébitSaturation, Single) * (uneDurée - TempsPerdu) / uneDurée
    RéserveCapacité = CapacitéThéorique - Demande

    ' Formule p29
    Stockage = Trafic.QTotal(Trafic.TraficEnum.UVP) * DuréeCycle() / 3600

    mCapacitéACalculer = False

  End Sub

  Public Overridable Property CapacitéACalculer() As Boolean
    Get
      If AvecTrafic() Then
        Return mCapacitéACalculer
      End If
    End Get
    Set(ByVal Value As Boolean)
      mCapacitéACalculer = Value
    End Set
  End Property

  Public ReadOnly Property TempsPerdu() As Short
    Get
      Return mPhases.TempsPerdu
    End Get
  End Property

  '****************************************************************************************************
  ' Calcul la durée du cycle du plan de feux en fonction de la demande du carrefour et des temps perdus
  '   CoefDemande = -1 ==> Méthode de Webster
  '   CoefDemande >=0  ==> Méthode classique, CoefDemande représente le coefficient à apporter à la demande
  '*****************************************************************************************************
  Public Function CalculCycle(Optional ByRef Message As String = "", Optional ByVal CoefDemande As Single = -1) As Short
    Dim tp As Short = TempsPerdu
    Dim DébitSaturation As Short = mVariante.DébitSaturation
    Dim DemandePriseEncompte As Short = (1 + CoefDemande) * Demande
    Dim uneDurée As Short

    If CoefDemande = -1 Then
      'Webster
      DemandePriseEncompte = Demande
    Else
      'Méthode classique
      DemandePriseEncompte = (1 + CoefDemande) * Demande
    End If

    If DemandePriseEncompte >= DébitSaturation Then
      Message = "La méthode n'est pas applicable pour un tel trafic" & vbCrLf & _
      "Demande du carrefour : " & DemandePriseEncompte & " uvpd/h"
    Else

      If CoefDemande = -1 Then
        'Méthode de Webster
        ' Formule fournie par le CERTU :
        ' Co =(1.5r + 5) / (1 -D/s)
        ' Co : Cycle optimum
        ' r : somme des temps perdus (rouge+jaune inutilisé+temps perdu au démarrage)
        ' D : Demande en uvp/s
        ' s : débit de saturation = 0.5uvp/s soit 1800 uvp/h
        uneDurée = (1.5 * tp + 5) / (1 - (Demande / DébitSaturation))

      Else
        ' Formule déduite de QTMax  =1800(C-T)/C du Guide des Carrefours à feux du  CERTU(p27) ==> C = (1800*T)/(1800-QTMax)
        uneDurée = CType(CSng(DébitSaturation) * tp / (DébitSaturation - DemandePriseEncompte), Short)

      End If

      If uneDurée > maxiDuréeCycleAbsolue Then
        Message = "Le calcul conduit à une durée trop importante : " & uneDurée & "s" & vbCrLf & "Durée maximale admise : " & maxiDuréeCycleAbsolue & " s"
      Else
        If uneDurée < DuréeMini Then
          If TypeOf Me Is PlanFeuxFonctionnement Then
            MessageBox.Show("La valeur de " & uneDurée & "s calculée est inférieure à la durée du plan de feux de base" & vbCrLf & _
            NomProduit & " retient la valeur de " & DuréeMini & "s")
          Else
            'Calcul de durée du plan de base (ou plan pour phasage) faible car trafic de référence faible : rallonger la durée au mini
          End If
          uneDurée = DuréeMini
        End If

        Return uneDurée
      End If

    End If

  End Function

  Public Function strRéserveCapacitéPourCent() As String
    Return Format(mRéserveCapacité / Demande, "0%")
  End Function

  Public Function RéserveCapacitéPourCent() As Single
    Return mRéserveCapacité / Demande * 100
  End Function

  Public Property RéserveCapacité() As Single
    Get
      Return mRéserveCapacité
    End Get
    Set(ByVal Value As Single)
      mRéserveCapacité = Value
    End Set
  End Property

  Public Property CapacitéThéorique() As Single
    Get
      Return mCapacitéThéorique
    End Get
    Set(ByVal Value As Single)
      mCapacitéThéorique = Value
    End Set
  End Property

  '********************************************************************************************
  'DuréeCycle : Durée du cycle du plan de feux
  'Minimum : indique si l'on veut la durée minimum ou réelle du cycle 
  '********************************************************************************************
  Public Function DuréeCycle(Optional ByVal Minimum As Boolean = False) As Short
    Dim unePhase As Phase

    If Minimum Then
      DuréeCycle = DuréeMini
    Else
      'Durée du cycle en secondes : somme des durées des phases
      For Each unePhase In mPhases
        DuréeCycle += unePhase.Durée
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
        'La demande sera à recalculer
        mDemande = -1
        mCapacitéACalculer = True
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
  ' Répartir la durée du cycle aux phases selon le trafic qu'elles supportent
  '***************************************************************************

  Public Sub RépartirDuréeCycle(ByVal Durée As Short)
    Dim unePhase As Phase
    Dim nbPhasesRéparties As Short = mPhases.Count
    Dim DuréeARépartir As Short = Durée

    'Si une phase est entièrement piétonne, on lui affecte la durée incompressible (vert mini piéton + rouge dégagemen)
    ' sauf si la durée a déjà été augmentée
    For Each unePhase In mPhases
      With unePhase
        If .EstSeulementPiéton Then
          'Retirer le temps perdu de la durée à répartir : pour les piétons c'est toute la durée de la phase qui est perdue
          DuréeARépartir -= Math.Max(.Durée, .DuréeIncompressible)
          nbPhasesRéparties -= 1
        Else
          'Retirer le temps perdu de la durée à répartir
          DuréeARépartir -= unePhase.TempsPerdu(Me)
        End If
      End With
    Next

    ' Répartition proportionnelle de la durée restante par rapport aux trafics
    For Each unePhase In mPhases
      With unePhase
        If Not .EstSeulementPiéton Then
          .Durée = Math.Max(DuréeARépartir * (.TraficSupporté / Demande) - mVariante.DécalageVertUtile + unePhase.TempsPerdu(Me), .DuréeIncompressible)
        End If
      End With
    Next

    'Les boucles qui suivent pourraient être améliorées, en regardant l'écart entre le pourcentage de trafic et le pourcentage de durée
    unePhase = mPhases(0)
    Do While DuréeCycle() < Durée
      If Not unePhase.EstSeulementPiéton Then
        unePhase.Durée += 1
      End If
      unePhase = PhaseSuivante(unePhase)
    Loop

    unePhase = mPhases(0)
    Do While DuréeCycle() > Durée
      If unePhase.Durée > unePhase.DuréeIncompressible Then
        unePhase.Durée -= 1
      End If
      unePhase = PhaseSuivante(unePhase)
    Loop

  End Sub

  Public Sub DéverrouillerPhases()
    Dim unePhase As Phase

    For Each unePhase In mPhases
      unePhase.Verrouillée = False
    Next

  End Sub

  '********************************************************************************************************************
  ' Déterminer la 1ère phase non verrouillée qui suit la phase
  '********************************************************************************************************************
  Protected Function PhaseSuivante(ByVal unePhase As Phase) As Phase

    Return mPhases.PhaseSuivante(unePhase)

  End Function

  Public Function PhaseAssociéeLigneFeux(ByVal uneLigneFeux As LigneFeux) As Phase
    Dim unePhase As Phase

    'Rechercher la phase concernée par la ligne de feux et mémoriser de combien le décalage va varier
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
    Dim DébutVert As Short
    Dim LgVert As Short
    Dim DébutOrange As Short
    Dim LgOrange As Short
    Dim DébutPhase As Short
    Dim LgPhase As Short
    Dim FinPhase As Short
    'Dim FinVertPhaseMini As Short
    Dim X1, Y1, X2, Y2 As Single
    Dim LimiteSection(DuréeCycle()) As Boolean
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
        DessinerChaine("Durées", uneFonte, uneBrosse, X1 + 15, Y1 - 2, g1, g2)
        DessinerChaine("compressibles", uneFonte, uneBrosse, X1 + 15, Y1, g1, g2)

      Else
        Dim DébutNomLf As Short
        uneFonte = New Font("Arial", 10, FontStyle.Bold, unit:=GraphicsUnit.Pixel)
        unePlumeNoire.DashStyle = Drawing2D.DashStyle.DashDot
        Marges.Y = 10
        'A l'écran : réduire l'échelle en X si la résolution est faible
        If Screen.PrimaryScreen.Bounds.Width > 1024 Then
          DébutNomLf = 6
          Marges.X = 26
          IntervalX = 6.5
        Else
          DébutNomLf = 1
          Marges.X = 17
          IntervalX = 6
        End If
        IntervalY = 20
        MargeHaute = 10

        EpaisseurVert = 10
        EpaisseurRouge = 3

        'Ecrire l'intitulé des lignes de feux au début de chaque ligne
        For Each uneLigneFeux In mLignesFeux
          DessinerChaine(uneLigneFeux.ID, uneFonte, uneBrosseRouge, DébutNomLf, 20 * i + MargeHaute, g1, g2)
          i += 1
        Next
      End If

      YTraitVertical = posY(0) - MargeHaute

      If PourImpression Then
        'Hachurer les durées compressibles
        For Each unePhase In mPhases
          X1 = posX(DébutPhase)
          Y1 = posY(0)
          X2 = (unePhase.Durée - unePhase.DuréeIncompressible) * IntervalX
          'Y2 : Rajouter un intervalle pour l'espace au-dessus des lignes de feux
          Y2 = (mLignesFeux.Count + 1) * IntervalY
          p(0) = New Point(X1, Y1)
          HachurerRectangle(p(0), New Size(X2, Y2), uneHachure, g1, g2, unePlumeNoire)
          DébutPhase += unePhase.Durée
        Next
        DébutPhase = 0
      End If

      For Each unePhase In mPhases
        LgPhase = unePhase.Durée
        FinPhase = DébutPhase + LgPhase
        '        FinVertPhaseMini = 1000

        For Each uneLigneFeux In unePhase.mLignesFeux
          PositionLigne = PositionDansPhase(uneLigneFeux, unePhase)
          With uneLigneFeux
            Y1 = posY(mLignesFeux.IndexOf(uneLigneFeux)) + MargeHaute
            'Le début du vert est début de la phase éventuellement décalé
            If PositionLigne <> Position.Dernière Then
              DébutVert = DébutPhase + DécalageOuvreFerme(uneLigneFeux, Décalage.Ouverture)
            Else
              DébutVert = DébutPhase
            End If

            LgVert = DuréeVertSurPhase(uneLigneFeux, unePhase)

            DébutOrange = DébutVert + LgVert
            LimiteSection(DébutOrange) = True
            LgOrange = .DuréeJaune
            If PositionLigne = Position.Première Then
              LgOrange = 0
            Else
              '              FinVertPhaseMini = Math.Min(FinVertPhaseMini, DébutOrange)
            End If

            'Tracer le segment de vert
            X1 = posX(DébutVert)
            X2 = posX(DébutOrange)
            DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurVert, uneBrosseVerte, g1, g2)
            If LgOrange > 0 Then
              'Véhicules qui ne se continue pas sur la phase suivante
              'Tracer le segment de jaune
              X1 = X2
              X2 = posX(DébutOrange + LgOrange)
              DessinerPolygone(X1, X2, Y1, EpaisseurVert, EpaisseurRouge, uneBrosseOrange, g1, g2)
              LimiteSection(DébutOrange + LgOrange) = True
            End If

            'Tracer le segment de rouge jusqu'à la fin du cycle
            If PositionLigne <> Position.Première Then
              X1 = X2
              X2 = posX(DuréeCycle)
              If X2 > X1 Then
                DessinerPolygone(X1, X2, Y1, EpaisseurRouge, EpaisseurRouge, uneBrosseRouge, g1, g2)
              End If
            End If

            'Tracer le segment de rouge depuis le début du cycle jusqu'au vert
            If DébutVert <> 0 And PositionLigne <> Position.Dernière Then
              X1 = posX(0)
              X2 = posX(DébutVert)
              DessinerPolygone(X1, X2, Y1, EpaisseurRouge, EpaisseurRouge, uneBrosseRouge, g1, g2)
            End If
            LimiteSection(DébutVert) = True

          End With
        Next uneLigneFeux

        'Trait vertical en début de phase
        X1 = posX(DébutPhase)
        Y1 = YTraitVertical
        Y2 = posY(mLignesFeux.Count - 1) + MargeHaute

        p(0) = New Point(X1, Y1)
        p(1) = New Point(X1, Y2)
        DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        'Indiquer l'abscisse de début de la phase
        If PourImpression Then
          'DessinerChaine("Phase " & mPhases.IndexOf(unePhase) + 1, uneFonte, uneBrosse, X1, Y1 + 4, g1, g2)

          ' v11 : Juin 06 - Centrer le texte 'Phase n' dans la largeur de la phase - doc ACONDIA v10 - Impressions §7
          p(1).X = posX(DébutPhase + unePhase.Durée)
          'Position du texte 4 unités au-dessus de la ligne
          p(0).Y += 4
          p(1).Y = p(0).Y
          unTexte = New Texte("Phase " & mPhases.IndexOf(unePhase) + 1, uneBrosse, uneFonte, Milieu(p(0), p(1)), unAlignement:=StringAlignment.Center)
          unTexte.Dessiner(g1, g2)
        End If

        If PourImpression Or DébutPhase > 0 Then
          DessinerChaine(CType(DébutPhase, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        End If

        '---------Fonctionnalité supprimée le 20/06/06 à la demande du CERTU : doc ACONDIA v10 - Impressions §6
        ''Trait vertical de Fin de vert le + tôt

        'X1 = posX(FinVertPhaseMini)
        ''ou mieux : 
        ''X1 = posX(DébutPhase + unePhase.DuréeIncompressible)
        'p(0).X = X1
        'p(1).X = X1
        'DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        ''Indiquer l'abscisse de fin de vert
        'DessinerChaine(CType(FinVertPhaseMini, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        '--------------------------------------------------------------------------------------------------------------

        ''''---------Fonctionnalité réintroduite le 25/01/07 à la demande du CERTU : doc ACONDIA v11 - Plans de feux §21
        ''''Trait vertical de Fin de vert le + tôt

        ''''X1 = posX(FinVertPhaseMini)
        ''''ou mieux : 
        ''''X1 = posX(DébutPhase + unePhase.DuréeIncompressible)
        ''''p(0).X = X1
        ''''p(1).X = X1
        ''''DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

        ''''Indiquer l'abscisse de fin de vert
        ''''DessinerChaine(CType(FinVertPhaseMini, String), uneFonte, uneBrosseRouge, X1, Y1, g1, g2)
        ''''--------------------------------------------------------------------------------------------------------------

        DébutPhase += unePhase.Durée
        LimiteSection(DébutPhase) = True
      Next unePhase

      'Trait vertical en fin de cycle
      X1 = posX(FinPhase)
      Y1 = YTraitVertical
      Y2 = posY(mLignesFeux.Count - 1) + MargeHaute
      p(0) = New Point(X1, Y1)
      p(1) = New Point(X1, Y2)
      DessinerLigne(p(0), p(1), unePlumeNoire, g1, g2)

      'Indiquer l'abscisse de fin du cycle (durée du cycle)
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
        'Dessiner le trait de séparation en dessous des noms des phases
        X1 = posX(0)
        X2 = posX(Me.DuréeCycle)
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
  ' Etape 1 : Créer l' enregistrement nécessaire dans le DataSet DIAGFEUX
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
    mDécalages(0) = New Hashtable
    mDécalages(1) = New Hashtable
  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeux)

    mVariante = unPlanFeux.mVariante
    mDécalages(0) = New Hashtable
    mDécalages(1) = New Hashtable

    unPlanFeux.mPhases.Cloner(Me)

  End Sub

  Public Sub New(ByVal unTrafic As Trafic)
    mVariante = unTrafic.Variante
    mDécalages(0) = New Hashtable
    mDécalages(1) = New Hashtable

    Trafic = unTrafic
  End Sub

  Public Sub New(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim i As Short
    Dim unePhase As Phase

    mVariante = cndVariante
    mDécalages(0) = New Hashtable
    mDécalages(1) = New Hashtable

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

  ' Décalage à l'ouverture ou à la fermeture d'une ligne de feux
  ' uneLigneFeux : ligne de feux concernée
  ' Index : 0 pour ouverture - 1 pour fermeture
  '*************************************************************************************************
  Public Property DécalageOuvreFerme(ByVal uneLigneFeux As LigneFeux, ByVal Index As Décalage) As Short
    Get
      If mDécalages(Index).Contains(uneLigneFeux) Then Return mDécalages(Index).Item(uneLigneFeux)
    End Get
    Set(ByVal Value As Short)
      mDécalages(Index).Item(uneLigneFeux) = Value
    End Set
  End Property

  '********************************************************************************************************************
  'Somme des décalages à l'ouverture et à la fermeture
  '********************************************************************************************************************
  Private Function DécalageTotal(ByVal uneLigneFeux As LigneFeux) As Short
    Return DécalageOuvreFerme(uneLigneFeux, Décalage.Ouverture) + DécalageOuvreFerme(uneLigneFeux, Décalage.Fermeture)
  End Function

  '********************************************************************************************************************
  'Durée des phases concernées par la ligne de feux 
  '********************************************************************************************************************
  Private Function DuréePhases(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase
    Dim uneDurée As Short

    For Each unePhase In mPhases
      If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
        uneDurée += unePhase.Durée
      End If
    Next

    Return uneDurée

  End Function

  '***********************************************************************************
  'Retourne la durée de vert maxi de la ligne de feux (si elle n'a pas de décalages)
  '***********************************************************************************
  Public Function DuréeVertMaxi(ByVal uneLigneFeux As LigneFeux) As Short
    Return DuréePhases(uneLigneFeux) - RougeIncompressible(uneLigneFeux) - uneLigneFeux.DuréeJaune
  End Function

  '********************************************************************************************
  'Rechercher les lignes de feux ayant le vert sur 2 phases
  ' et mettre dans une table le numéro de la phase qui donne le début de vert à la ligne de feux
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
          'Mémoriser dans le  tableau la 1ère phase où apparait la ligne de feux
          dctPhasesLf(uneLigneFeux) = unePhase
        End If

      Next
    Next

  End Sub

  Private Function IndexPremièrePhase(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase

    For Each unePhase In mPhases
      Select Case PositionDansPhase(uneLigneFeux, unePhase)
        Case Position.Unique, Position.Première
          Return mPhases.IndexOf(unePhase)
      End Select
    Next
  End Function

  Public Function Supérieur(ByVal L1 As LigneFeux, ByVal L2 As LigneFeux) As Boolean

    Dim Index1, Index2 As Short
    Index1 = IndexPremièrePhase(L1)
    Index2 = IndexPremièrePhase(L2)

    If Index1 > Index2 Then
      ' L1 commence après L2 : il est dans une phase supérieure
      Return True

    ElseIf Index1 = Index2 Then
      'L1 et L2 commence   gnnt dans la même phase : on conserve l'ordre sauf si seul le 1er est multiphases
      If PositionDansPhase(L1, mPhases(Index1)) <> PlanFeux.Position.Unique _
      And PositionDansPhase(L2, mPhases(Index2)) = PlanFeux.Position.Unique Then
        Return True
      End If
    End If

  End Function

  Public Function PositionDansPhase(ByVal uneLigneFeux As LigneFeux, ByVal unePhase As Phase) As Position
    If dctPhasesLf.Item(uneLigneFeux) Is unePhase Then
      'Le vert démarre dans cette phase pour cette ligne de feux
      If PhaseSuivante(unePhase).mLignesFeux.Contains(uneLigneFeux) Then
        'La ligne de feux continue sur la phase suivante : 
        ' elle circule donc jusqu'à la fin de la phase(pas de temps de jaune inutilisé)
        Return Position.Première
      Else
        Return Position.Unique
      End If

    ElseIf unePhase.mLignesFeux.Contains(uneLigneFeux) Then
      'Les véhicules passaient déjà dans la phase précédente
      'Pas de temps perdu au démarrage
      Return Position.Dernière
    End If

    Return Position.Aucune

  End Function

  '********************************************************************************************************************
  'Durée de vert de la ligne de feux : cumul éventuel sur 2 phases
  '********************************************************************************************************************
  Public Function DuréeVert(ByVal uneLigneFeux As LigneFeux) As Short
    Dim unePhase As Phase
    Dim uneDurée As Short

    For Each unePhase In mPhases
      If unePhase.mLignesFeux.Contains(uneLigneFeux) Then
        uneDurée += DuréeVertSurPhase(uneLigneFeux, unePhase)
      End If
    Next

    Return uneDurée

  End Function

  '******************************************************************************************************
  'Durée de vert de la ligne de feux pendant une phase donnée : il s'agit du vert réel
  '******************************************************************************************************
  Public Function DuréeVertSurPhase(ByVal uneLigneFeux As LigneFeux, ByVal unePhase As Phase) As Short

    Dim uneDurée As Short

    uneDurée = unePhase.Durée

    Select Case PositionDansPhase(uneLigneFeux, unePhase)
      Case Position.Première
        'La ligne de feux continue sur la phase suivante : 
        ' elle circule donc jusqu'à la fin de la phase(pas de temps de jaune inutilisé)
        uneDurée -= DécalageOuvreFerme(uneLigneFeux, Décalage.Ouverture)

      Case Position.Dernière
        'Les véhicules passaient déjà dans la phase précédente
        'Pas de temps perdu au démarrage
        uneDurée -= DécalageOuvreFerme(uneLigneFeux, Décalage.Fermeture)
        uneDurée -= RougeIncompressible(uneLigneFeux)
        uneDurée -= uneLigneFeux.DuréeJaune

      Case Position.Unique
        uneDurée -= DécalageTotal(uneLigneFeux)
        uneDurée -= RougeIncompressible(uneLigneFeux) + uneLigneFeux.DuréeJaune

    End Select

    Return uneDurée

  End Function

  Public Function VertUtile(ByVal uneLigneFeux As LigneFeux) As Short
    Return DuréeVert(uneLigneFeux) + mVariante.DécalageVertUtile
  End Function

  Public Overrides Function CréerGraphique(ByVal uneCollection As Graphiques) As PolyArc
  End Function

End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxCollection--------------------------
'=====================================================================================================
Public Class PlanFeuxCollection : Inherits CollectionBase

  ' Créer une instance la collection
  Public Sub New()
    MyBase.New()
  End Sub

  ' Ajouter un objet à la collection.
  ' Retourne la position	à laquelle le plan est inséré
  Public Function Add(ByVal unPlan As PlanFeux) As Short
    If Not Me.Contains(unPlan) Then
      Return Me.List.Add(unPlan)
    End If
  End Function

  ' Ajouter une plage d'objets à la collection.
  Public Sub AddRange(ByVal valeurs() As PlanFeux)
    Me.InnerList.AddRange(valeurs)
  End Sub

  Public Sub Insert(ByVal unPlan As PlanFeux, ByVal Index As Short)
    Me.InnerList.Insert(Index, unPlan)
  End Sub

  ' Supprimer un objet spécifique de la collection.
  Public Sub Remove(ByVal unPlan As PlanFeux)
    If Me.List.Contains(unPlan) Then
      Me.List.Remove(unPlan)
    End If

  End Sub

  'Insérer un objet dans la collection
  Public Sub Insert(ByVal Index As Short, ByVal unPlan As PlanFeux)
    Me.List.Insert(Index, unPlan)
  End Sub

  ' Creer la propriété par défaut Item pour cette collection.
  ' Permet la  recherche par index.
  Default Public ReadOnly Property Item(ByVal Index As Short) As PlanFeux
    Get
      Return CType(Me.List.Item(Index), PlanFeux)
    End Get
  End Property

  ' Creer une autre propriété par défaut Item pour cette collection.
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

  ' Methode pour vérifier si un Objet existe déjà dans la collection.
  Public Overloads Function Contains(ByVal unPlan As PlanFeux) As Boolean
    Return Me.List.Contains(unPlan)
  End Function

  Public Overloads Function Contains(ByVal nomPlan As String) As Boolean
    Return Not IsNothing(Item(nomPlan))
  End Function

  '***************************************************************************
  ' Initialiser le durées minimum des plans de base possibles
  ' Au stade de l'organisation du phasage, celles-ci doivent être recalculées
  ' à chaque modification des temps de rouge de dégagement
  '***************************************************************************
  Public Sub CalculerDuréesMini()
    Dim unPlanFeux As PlanFeuxBase

    For Each unPlanFeux In Me
      If Not unPlanFeux.PhasageIncorrect Then
        unPlanFeux.CalculerDuréesMini()
      End If
    Next
  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxBase --------------------------
'=====================================================================================================
Public Class PlanFeuxBase : Inherits PlanFeux
#Region "Déclarations"

  'plans de fonctionnement attachés au plan de feux de base
  Public mPlansFonctionnement As New PlanFeuxCollection
  'Collection des trafics attachés au plan de feux de base via ses plans de fonctionnement
  Private mTrafics As New TraficCollection
  Private mNbPfAvecTrafic As Short

  'Collection de lignes de feux identique à la variante, 
  '  mais ordonnée spécifiquement pour le plan de feux de base et ses plans de fonctionnement associés
  Public desLignesFeux As New LigneFeuxCollection(Me)
  Private mAntagonismes As AntagonismeCollection
  Private mConflitsInitialisés As Boolean
  Public mFiltrePhasage As FiltrePhasage
  Public PhasageInitialisé As Boolean

  Public PhasageIncorrect As Boolean
  Private mRougeIncompressible As New Hashtable
  'Indique si le plan de feux comporte au moins une ligne de feux sur 2 phases
  Public mLigneFeuxMultiPhases As Boolean
  'Indique si le plan de feux comporte une phase spéciale TAG ou TAD
  Public mAvecPhaseSpéciale As Boolean

  Private mPlanPhasageAssocié As PlanFeuxPhasage
  Private mVerrou As [Global].Verrouillage = [Global].Verrouillage.Aucun
  Private mPlansPourPhasage As New PlanFeuxCollection
  Private mPlanFonctionnementCourant As PlanFeuxFonctionnement

  Protected mPlanParent As PlanFeuxBase
  Public VertMiniVéhicules As Short = [Global].VertMiniVéhicules
  Public VertMiniPiétons As Short = [Global].VertMiniPiétons
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
      .CritèreCapacité = FiltrePhasage.CapacitéEnum.Aucun
      .LigneFeuxMultiPhases = FiltrePhasage.LFMultiphasesEnum.Inclure
      .AvecPhaseSpéciale = FiltrePhasage.PhaseSpécialeEnum.Inclure
    End With
  End Sub
  Public Sub DéterminerPhaseSpéciale()
    Dim dct As New Hashtable
    Dim uneLigneFeux As LigneFeux
    Dim unePhase As Phase

    For Each unePhase In mPhases
      For Each uneLigneFeux In unePhase.mLignesFeux
        'Ne faire la recherche que sur les lignes véhicules
        If uneLigneFeux.EstVéhicule Then
          If dct.Contains(uneLigneFeux.mBranche) Then
            'Cette branche est déjà concernée par une phase
            If Not dct(uneLigneFeux.mBranche) Is unePhase Then
              'La branche est concernée par 2 phases
              mAvecPhaseSpéciale = True
              Exit For
            End If
          Else
            dct.Add(uneLigneFeux.mBranche, unePhase)
          End If
        End If
      Next
      If mAvecPhaseSpéciale Then Exit For
    Next

  End Sub
#End Region

#Region "Propriétés"

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

  Public ReadOnly Property Définitif() As Boolean
    Get
      Return mVariante.ScénarioDéfinitif Is Me
    End Get
  End Property

  Public ReadOnly Property Projet() As Boolean
    Get
      Return Not Définitif
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

  Public Property ConflitsInitialisés() As Boolean
    Get
      Return mConflitsInitialisés
    End Get
    Set(ByVal Value As Boolean)
      mConflitsInitialisés = Value
    End Set
  End Property

  Public Overrides Property CapacitéACalculer() As Boolean
    Get
      Return MyBase.CapacitéACalculer
    End Get

    Set(ByVal Value As Boolean)
      MyBase.CapacitéACalculer = Value
      If Value Then
        'Il faut aussi recalculer les capacités des plans de fonctionnement
        Dim unPlanFonctionnement As PlanFeuxFonctionnement
        For Each unPlanFonctionnement In mPlansFonctionnement
          unPlanFonctionnement.CapacitéACalculer = Value
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

  Public Sub ReCréerTrafics()
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
    '2 : N'afficher que les trafics concernés par au moins 1 plan de fonctionnement
    'Return mTrafics
    '3 : Ne faire cette restriction que si le plan de feux de base est verrouillé et au moins un PFF
    'If mPlansFonctionnement.Count > 0 Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If
    '4 : Ne faire cette restriction que pour le scénario définitif
    'If mVariante.ScénarioDéfinitif Is Me Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If
    '5 : Combinaison des restrictions 3 et 4
    'If mVariante.ScénarioDéfinitif Is Me AndAlso mPlansFonctionnement.Count > 0 Then
    '  Return mTrafics
    'Else
    '  Return mVariante.mTrafics
    'End If

  End Function

  ''Indique s'il s'agit d'un carrefour composé, auquel cas les lignes de feux seront décomposées en sous-ensembles disjoints
  'Private ReadOnly Property Composé() As Boolean
  '  Get
  '    Return Not IsNothing(mLignesFeux)
  '  End Get
  'End Property

#End Region

#Region "Constructeurs"
  Public Sub New()
    MyBase.New()

    If Not TypeOf Me Is PlanFeuxPhasage Then
      CréerLignesFeux()
      InitFiltrePhasage()
    End If
  End Sub

  Public Sub New(ByVal unTrafic As Trafic)

    MyBase.New(unTrafic)

    InitFiltrePhasage()
    CréerLignesFeux()
    CréerAntagonismes()
  End Sub

  Public Sub New(ByVal unNom As String)
    MyBase.New()
    InitFiltrePhasage()
    mNom = unNom
    CréerLignesFeux()
    CréerAntagonismes()

  End Sub

  '*****************************************************************************************************
  'Duplication d'un plan de feux de base 
  ' Sert à la duplication de scénario
  ' Sert aussi à définir un plan de phasage à partir d'un autre pour mettre 1 ligne fe feux sur 2 phases
  '*****************************************************************************************************
  Public Sub New(ByVal unPlanFeux As PlanFeuxBase)

    MyBase.New(unPlanFeux)

    Dim unPlanPourPhasage As PlanFeuxPhasage
    Dim unPlanFonctionnement As PlanFeuxFonctionnement

    InitFiltrePhasage()

    mVerrou = unPlanFeux.Verrou

    If mVerrou >= [Global].Verrouillage.LignesFeux Then
      'duplication de scénario
      mPlanParent = unPlanFeux
      CréerAntagonismes()
      mPlanParent = Nothing

      With unPlanFeux
        If mVerrou >= [Global].Verrouillage.Matrices Then

          'Si la matrice des conflits est verrouillée : dupliquer les plans de phasage possibles
          For Each unPlanPourPhasage In .PlansPourPhasage
            mPlansPourPhasage.Add(New PlanFeuxPhasage(unPlanPourPhasage))
          Next

          If .PhasageRetenu Then
            'Duplication de scénario : le plan de phasage associé est en même position
            Me.PlanPhasageAssocié = mPlansPourPhasage(.PlansPourPhasage.IndexOf(.PlanPhasageAssocié))
            For Each unPlanFonctionnement In .mPlansFonctionnement
              'Dupliquer si nécessaire les plans de feux de fonctionnement
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
    Dim uneRowRougeDégagement As DataSetDiagfeux.RougesDégagementRow

    'Créer les lignes de feux en respectant l'ordre spécifique au plan de feux de base
    'Duplique également les Incompatibilités à partir de celles du niveau Variante (conflits systématiques)
    CréerLignesFeux(uneRowPlanFeux)
    'Créer les antagonismes et duplique les antagonismes systématiques niveau Variante
    CréerAntagonismes()

    With uneRowPlanFeux
      If Not .IsVerrouPlanNull Then
        mVerrou = .VerrouPlan
      End If

      mNom = .ID
      If mNom = "0" Then
        'Ancien projet ACONDIA
        mNom = ""
      End If

      If Not .IsDéfinitifNull AndAlso .Définitif Then
        mVariante.ScénarioDéfinitif = Me
      End If

      If Not .IsVertMiniVéhiculesPlanNull Then
        Me.VertMiniVéhicules = .VertMiniVéhiculesPlan
      End If
      If Not .IsVertMiniPiétonsPlanNull Then
        Me.VertMiniPiétons = .VertMiniPiétonsPlan
      End If

      'Incompatibilités des lignes de feux : Matrice des conflits - Ajout de celles issues de la résolution des antagonismes
      For i = 0 To .GetIncompatiblesRows.Length - 1
        uneRowIncompatible = .GetIncompatiblesRows(i)
        With uneRowIncompatible
          mLignesFeux.EstIncompatible(mLignesFeux(.IdLfInc1), mLignesFeux(.IdLfInc2)) = True
        End With
      Next

      'Matrice des rouges de dégagement
      For i = 0 To .GetRougesDégagementRows.Length - 1
        uneRowRougeDégagement = .GetRougesDégagementRows(i)
        With uneRowRougeDégagement
          mLignesFeux.RougeDégagement(mLignesFeux(.IdLfRouge1), mLignesFeux(.IdLfRouge2)) = .RougesDégagement_text
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
    uneRowPlanFeux.Définitif = uneVariante.ScénarioDéfinitif Is Me

    uneRowPlanFeux.VerrouPlan = mVerrou

    If Me.VertMiniVéhicules <> mVariante.VertMiniVéhicules Then
      uneRowPlanFeux.VertMiniVéhiculesPlan = VertMiniVéhicules
    End If
    If Me.VertMiniPiétons <> mVariante.VertMiniPiétons Then
      uneRowPlanFeux.VertMiniPiétonsPlan = VertMiniPiétons
    End If

    For Each uneLigneFeux In mLignesFeux
      ds.OrdreLignes.AddOrdreLignesRow(uneLigneFeux.ID, uneRowPlanFeux)
      For Each uneLigneAdverse In mLignesFeux
        If mLignesFeux.IndexOf(uneLigneAdverse) > mLignesFeux.IndexOf(uneLigneFeux) Then
          'On n'écrit qu'une fois l'incompatibilité : siF1 incompatible avec F2 inutile d'écrire que F2 l'est avec F1
          If Not mVariante.mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneAdverse) Then
            'On ne réécrit pas les conflits systématiques : contenus dans la colletion lignes de feux de la variante
            If mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneAdverse) Then
              ds.Incompatibles.AddIncompatiblesRow(uneLigneFeux.ID, uneLigneAdverse.ID, uneRowPlanFeux)
            End If
          End If

        End If
        Rouge = mLignesFeux.RougeDégagement(uneLigneFeux, uneLigneAdverse)
        ds.RougesDégagement.AddRougesDégagementRow(uneLigneFeux.ID, uneLigneAdverse.ID, Rouge, uneRowPlanFeux)
      Next
    Next

    If Not IsNothing(mAntagonismes) Then
      'mAntagonismes peut être Nothing en mode tableur ou encore si les LF ne sont pas verrouillées
      For Each unAntagonisme In mAntagonismes
        ds.TypesConflit.AddTypesConflitRow(unAntagonisme.TypeConflit, uneRowPlanFeux)
      Next
    End If

    For Each unPlanFeux In mPlansFonctionnement
      unPlanFeux.Enregistrer(uneVariante, uneRowVariante)
    Next

    'If Composé Then
    '  Dim uneLigneFeux As LigneFeux
    '  uneRowPlanFeux.ID = CStr(uneVariante.mPlansFeuxBase.IndexOf(Me))
    '  For Each uneLigneFeux In mLignesFeux
    '    ds.IDLigneFeuxComposé.AddIDLigneFeuxComposéRow(uneLigneFeux.ID, uneRowPlanFeux)
    '  Next
    'End If

  End Function

#Region "LignesFeux-Antagonismes"

  Private Sub CréerLignesFeux(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow)
    Dim i As Short

    With uneRowPlanFeux
      'On crée les lignes de feux en s'appuyant sur l'ordre mémorisé(qui peut être différent de l'ordre des lignes du projet)
      For i = 0 To .GetOrdreLignesRows.Length - 1
        mLignesFeux.Add(mVariante.mLignesFeux(.GetOrdreLignesRows(i).OrdreLignes_Column))
      Next
    End With

    CréerLignesFeux()

  End Sub

  Public Sub RenommerLigneFeux(ByVal uneLigneFeux As LigneFeux, ByVal exID As String)
    Dim unPlanFonctionnement As PlanFeuxFonctionnement

    mLignesFeux.Substituer(uneLigneFeux, exID)
    For Each unPlanFonctionnement In Me.mPlansFonctionnement
      unPlanFonctionnement.mLignesFeux.Substituer(uneLigneFeux, exID)
    Next

  End Sub

  Public Sub CréerAntagonismes()
    Dim unAntagonisme, NewAntagonisme As Antagonisme

    With mVariante
      If .Verrou >= [Global].Verrouillage.LignesFeux Then
        If mLignesFeux.Count = 0 Then
          'Le plan de feux de base a été créé avant le verrouillage des lignes de feux : elles n'ont donc pas été créées
          CréerLignesFeux()
        End If

        If .ModeGraphique Then
          mAntagonismes = New AntagonismeCollection(mLignesFeux)
          For Each unAntagonisme In AntagoADupliquer()
            NewAntagonisme = New Antagonisme(unAntagonisme, DuplicationIncomplète:=IsNothing(mPlanParent))
            'Ajouter l'antagonisme à la collection
            'Cette instruction permet aussi de regrouper les antagonismes qui sont liés car correspondant aux mêmes courants de circulation
            mAntagonismes.Add(NewAntagonisme)
          Next
        End If

        If Not IsNothing(mPlanParent) Then
          'Duplication de scénario
          Me.VertMiniVéhicules = mPlanParent.VertMiniVéhicules
          Me.VertMiniPiétons = mPlanParent.VertMiniPiétons
        End If

      Else
        'Remettre le plan de Feux de base à zéro
        mLignesFeux.Dimensionner(RemiseAZéro:=True)
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

  Private Sub CréerLignesFeux()
    Dim uneLigneFeux As LigneFeux

    Try

      If mVariante.Verrou >= [Global].Verrouillage.LignesFeux Then
        If mLignesFeux.Count = 0 Then
          For Each uneLigneFeux In LignesFeuxADupliquer()
            mLignesFeux.Add(uneLigneFeux)
          Next
        End If

        'Clone les lignes de feux incompatibles, ainsi que les rouges de dégagement 
        mLignesFeux.ClonerIncompatibilités(LignesFeuxADupliquer)
        'Dimensionner()

      End If

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "PlanFeux.CréerLignesFeux")
    End Try

  End Sub
#End Region

#Region "DuréesMini"

  '********************************************************************************************
  'DuréeMini : Durée minimum du cycle du plan de feux
  '********************************************************************************************
  Public Overrides ReadOnly Property DuréeMini() As Short
    Get
      Dim unePhase As Phase
      'Somme des durées incompressibles du plan de feux
      For Each unePhase In mPhases
        DuréeMini += unePhase.DuréeIncompressible
      Next
    End Get
  End Property

  '********************************************************************************************
  ' Calculer la durée minimum des phases du plan de feux de base
  '********************************************************************************************
  Public Sub CalculerDuréesMini()
    mPhases.CalculerDuréesMini()
    CapacitéACalculer = True
  End Sub

  '********************************************************************************************
  ' Rouge incompressible de la ligne de feux : 
  ' le feu passera au rouge n secondes avant la fin de la phase
  'C'est le + grand rouge de dégagement de la ligne /ensemble des lignes de la phase suivante
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
  Public Property PlanPhasageAssocié() As PlanFeuxPhasage
    Get
      Return mPlanPhasageAssocié
    End Get
    Set(ByVal Value As PlanFeuxPhasage)
      Dim exPlanPhasage As PlanFeuxPhasage = mPlanPhasageAssocié

      If Not mPlanPhasageAssocié Is Value Then
        mPlanPhasageAssocié = Value
        If IsNothing(Value) Then
          'Le clear sert de drapeau pour indiquer que l'organisation du phasage n'est encore pas retenu
          mPhases.Clear()
          'La demande sera à recalculer si on choisit une organisation différente du phasage
          mDemande = -1
          If Not IsNothing(exPlanPhasage) Then
            'Désolidariser aussi le plan de phasage associé
            exPlanPhasage.PlanBaseAssocié = Nothing
          End If

        Else
          mPlanPhasageAssocié.PlanBaseAssocié = Me
          IndexerLignesFeux()
          CalculerDuréesMini()
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
      'Return Not IsNothing(mPlanPhasageAssocié)
    End Get
  End Property

  Public Sub RéinitialiserPhasage()
    PlanPhasageAssocié = Nothing
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
  'Private Sub AfficherScénar(ByVal Scénarios As TreeNodeCollection)
  '  Dim i As Integer
  '  Dim unScénario As TreeNode
  '  Dim NoeudPhase As TreeNode
  '  Dim NoeudFeu As TreeNode

  '  For Each unScénario In Scénarios
  '    Debug.WriteLine("Scénario " & unScénario.Text)
  '    For Each NoeudPhase In unScénario.Nodes
  '      Debug.WriteLine("   " & NoeudPhase.Text)
  '      For Each NoeudFeu In NoeudPhase.Nodes
  '        Debug.WriteLine("   " & NoeudFeu.Text)
  '      Next
  '    Next
  '  Next

  'End Sub

  '***********************************************************************************************
  ' Construire les différents scénarios de phasage à partir de la matrice des conflits
  ' Fonction appelée lors du verrouillage de la matrice des conflits afin de vérifier qu'au moins
  ' une organisation est possible
  ' Appelée également lors de la lecture d'un projet(InitPhasage)
  '     -soit les conflits sont verrouillés : activation de l'onglet plans de feux
  '     -soit le phasage est retenu : cochage du phasage retenu
  '***********************************************************************************************
  Public Sub ConstruirePlansDePhasage()

    Dim trn As New TreeNode

    Dim Scénarios As TreeNodeCollection = trn.Nodes  ' Me.TreeView1.Nodes		
    Scénarios.Clear()
    PlansPourPhasage.Clear()

    Dim Scénario As TreeNode
    Dim NoeudPhase As TreeNode
    Dim NoeudFeu As TreeNode

    Dim uneLigneFeux As LigneFeux

    'Classer les lignes de feux en mettant d'abord les lignes de feux véhicules

    Try

      'Traiter en 1er les lignes de feux véhicules
      For Each uneLigneFeux In mLignesFeux
        If Not uneLigneFeux.EstPiéton Then
          InsérerFeuDansPhases(uneLigneFeux, Scénarios)
        End If
        If Scénarios.Count = 0 Then Exit For
      Next

      If Scénarios.Count > 0 Then
        'Traiter ensuite les lignes de feux piétons
        For Each uneLigneFeux In mLignesFeux
          If uneLigneFeux.EstPiéton Then
            InsérerFeuDansPhases(uneLigneFeux, Scénarios)
            If Scénarios.Count = 0 Then Exit For
          End If
        Next

        'Construire les plans de feux de base  à partir des scénarios
        Dim unPlanFeux As PlanFeuxPhasage
        Dim unePhase As Phase

        For Each Scénario In Scénarios
          'Instancier un plan de feux
          unPlanFeux = New PlanFeuxPhasage(Me)
          unPlanFeux.Trafic = mTrafic
          For Each NoeudPhase In Scénario.Nodes
            'Ajouter la phase dans le plan
            unePhase = New Phase
            For Each NoeudFeu In NoeudPhase.Nodes
              'Ajouter la ligne de feux dans la phase
              uneLigneFeux = mLignesFeux(NoeudFeu.Text)
              unePhase.mLignesFeux.Add(uneLigneFeux)
            Next
            unPlanFeux.AddPhases(unePhase)
          Next NoeudPhase

          'Ajouter le plan à la collection
          PlansPourPhasage.Add(unPlanFeux)

        Next Scénario

        'Ajouter les scénarios comportant des lignes de feux sur 2 phases
        RechercherCompatibilitéSur2Phases()

        For Each unPlanFeux In PlansPourPhasage
          ' Rechercher si le plan pour phasage correspond au plan de base, afin de les associer
          RechercherPlanBaseEquivalent(unPlanFeux)

          unPlanFeux.DéterminerPhaseSpéciale()
          unPlanFeux.IndexerLignesFeux()
        Next

      End If  ' Scénarios.Count > 0

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "ConstruirePlansDePhasage")
    End Try

  End Sub

  Public Sub CalculerCapacitésPlansPhasage()
    Dim unPlanFeux As PlanFeuxPhasage
    Dim uneDuréeCycle As Short

    mPlansPourPhasage.CalculerDuréesMini()

    If AvecTrafic() Then

      For Each unPlanFeux In PlansPourPhasage
        With unPlanFeux
          .CalculerDemande()
          If .Demande <= mVariante.DébitSaturation Then
            '            uneDuréeCycle = .CalculCycle(CoefDemande:=0)
            'MODIF AV(26/03/07) : il faut calculer ces capacités avec lea méthode de Webster : Point Plan de feux-13 du Document de suivi)
            uneDuréeCycle = .CalculCycle()
            If uneDuréeCycle <> 0 Then
              'Sinon, la demande aboutit à une durée supérieure à la durée admissible : ce plan devrait être éliminé + tard
              .RépartirDuréeCycle(uneDuréeCycle)
              .CalculerRéserveCapacité()
            End If
          End If
        End With
      Next
    End If

  End Sub

  Private Sub RechercherCompatibilitéSur2Phases()
    Dim PlansSupplémentaires As New PlanFeuxCollection
    Dim PlansTemporaires As New PlanFeuxCollection
    Dim unPlanFeux, unPlanFeux2, unPlanFeux3 As PlanFeuxPhasage
    Dim unePhase As Phase
    Dim uneLigneFeux As LigneFeux
    Dim IndexPhase As Short
    Dim EliminerDoublon As Boolean

    Try
      For Each unPlanFeux In PlansPourPhasage
        If unPlanFeux.mPhases.Count > 2 Then
          'Sur 2 phases çà n'a pas de sens qu'une ligne de feux soit sur les 2 phases(le feu serait toujours vert)
          For Each unePhase In unPlanFeux.mPhases
            IndexPhase = unPlanFeux.mPhases.IndexOf(unePhase)
            For Each uneLigneFeux In unePhase.mLignesFeux
              'Rechercher la compatibilité de la LF avec la phase suivante
              IndexPhase = (IndexPhase + 1) Mod unPlanFeux.mPhases.Count
              If Not PlanCompatible2Phases(unPlanFeux, uneLigneFeux, IndexPhase, PlansTemporaires) Then
                'Rechercher la compatibilité de la LF avec la phase précédente
                IndexPhase = (IndexPhase + 1) Mod unPlanFeux.mPhases.Count
                PlanCompatible2Phases(unPlanFeux, uneLigneFeux, IndexPhase, PlansTemporaires)
              End If
              'Se repositionner sur la phase en cours d'analyse
              IndexPhase = unPlanFeux.mPhases.IndexOf(unePhase)
            Next uneLigneFeux
          Next unePhase

          For Each unPlanFeux2 In PlansTemporaires
            EliminerDoublon = False
            For Each unPlanFeux3 In PlansSupplémentaires
              'Rechercher si dans les plans supplémentaires déjà trouvés, '
              'il n'y a pas un plan équivalent à celui qu'on vient de trouver
              If unPlanFeux2.Equivalent(unPlanFeux3) Then
                EliminerDoublon = True
              End If
            Next
            If Not EliminerDoublon Then PlansSupplémentaires.Add(unPlanFeux2)
          Next
          PlansTemporaires.Clear()

        End If

      Next unPlanFeux

      For Each unPlanFeux2 In PlansSupplémentaires
        unPlanFeux2.Trafic = mTrafic
        PlansPourPhasage.Add(unPlanFeux2)
      Next

    Catch ex As DiagFeux.Exception
      Throw New DiagFeux.Exception(ex.Message)
    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "RechercherCompatibilitéSur2Phases")
    End Try
  End Sub

  Private Function PlanCompatible2Phases(ByVal unPlanFeux As PlanFeuxPhasage, ByVal uneLigneFeux As LigneFeux, _
                  ByVal IndexPhase As Short, ByVal PlansSupplémentaires As PlanFeuxCollection) As Boolean
    Dim unPlanFeux2, unPlanFeux3 As PlanFeuxPhasage
    Dim desLignesFeux As LigneFeuxCollection = unPlanFeux.mPhases(IndexPhase).mLignesFeux
    Dim Inséré As Boolean

    If CompatibilitéPossible(uneLigneFeux, desLignesFeux) Then
      'Cloner le plan de feux et y ajouter la ligne de feux dans la phase suivante
      unPlanFeux2 = New PlanFeuxPhasage(unPlanFeux)
      unPlanFeux2.mLigneFeuxMultiPhases = True
      desLignesFeux = unPlanFeux2.mPhases(IndexPhase).mLignesFeux
      desLignesFeux.PositionInsertion(uneLigneFeux, mLignesFeux)

      If Not unPlanFeux2.mPhases.EquivalentDeuxPhases Then

        If PlansSupplémentaires.Count = 0 Then
          PlansSupplémentaires.Add(unPlanFeux2)
        Else
          For Each unPlanFeux3 In PlansSupplémentaires
            With unPlanFeux3
              desLignesFeux = unPlanFeux3.mPhases(IndexPhase).mLignesFeux
              If CompatibilitéPossible(uneLigneFeux, desLignesFeux) Then
                desLignesFeux.PositionInsertion(uneLigneFeux, mLignesFeux)
                'dès qu'on a réussi à insérer la ligne dans au moins un plan supplémentaire :on s'arrête là (sinon on peut arriver à avoir une pléthore de plans)
                If .mPhases.EquivalentDeuxPhases Then
                  desLignesFeux.Remove(uneLigneFeux)
                Else
                  '                  Inséré = True
                End If
              End If
            End With
          Next
          If Not Inséré Then
            PlansSupplémentaires.Add(unPlanFeux2)
          End If
        End If

        Return True

      End If
    End If

  End Function

  Private Function CompatibilitéPossible(ByVal uneLigneFeux As LigneFeux, ByVal desLignesFeux As LigneFeuxCollection) As Boolean
    Dim uneLigneFeux2 As LigneFeux

    For Each uneLigneFeux2 In desLignesFeux
      If mLignesFeux.EstIncompatible(uneLigneFeux, uneLigneFeux2) Then
        Return False
      End If
    Next uneLigneFeux2

    Return True
  End Function

  Public Sub CalculerDuréesMiniPlansFeux()
    mPlansPourPhasage.CalculerDuréesMini()
    CalculerDuréesMini()
  End Sub

  '****************************************************************************************
  'ComplémentOrganiserPhasage : Rehcerhcer les plans pour phasage trop longs
  'PhasagesAConstruire : Construire la collection mPlansPourPhasage
  'Ceci doit être fait lors de la réouverture d'un fichier 
  'Autrement les phasages sont construits lors du verrouillage de la matrice des conflits
  '*****************************************************************************************
  Public Sub ComplémentOrganiserPhasage(ByVal PhasagesAConstruire As Boolean)
    Dim unPlanPourPhasage As PlanFeuxBase

    Try

      If PhasagesAConstruire Then
        'Lecture d'un projet existant suffisamment avancé
        ConstruirePlansDePhasage()

      Else

        If AvecTrafic() Then
          Dim Garbage As New PlanFeuxCollection

          'Calculer la demande du carrefour (s'il y a au moins un trafic de défini : le trafic de référence)
          For Each unPlanPourPhasage In PlansPourPhasage

            With unPlanPourPhasage
              If .DuréeCycle = 0 Then
                'la méthode classique ne sait pas faire le calcul avec un tel trafic (>1800)
                Garbage.Add(unPlanPourPhasage)
              Else
                If .DuréeCycle > PlanFeux.maxiDuréeCycleAbsolue Then
                  'Demande CERTU 07/07/06 : Point 4 du §Plan de feux
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
      LancerDiagfeuxException(ex, "ComplémentOrganiserPhasage")

    End Try

  End Sub

  'Déterminer si le plan pour phasage correspond à un plan de feux de base
  ' Associe le plan de feux de base s'il est trouvé
  '**********************************************************************************************************************
  Private Sub RechercherPlanBaseEquivalent(ByVal unPlanPourPhasage As PlanFeuxBase)
    Dim unePhase As Phase

    If Equivalent(unPlanPourPhasage) Then
      'Plan de feux de base correspondant trouvé : on va les associer
      For Each unePhase In mPhases
        With unPlanPourPhasage.mPhases
          .Déplacer(.PhaseEquivalente(unePhase), mPhases.IndexOf(unePhase))
        End With
      Next
      PlanPhasageAssocié = unPlanPourPhasage
    End If

  End Sub

  '**********************************************************************************************************************
  ' Insérer une ligne de feux dans chaque phase où c'est possible
  '**********************************************************************************************************************
  Private Sub InsérerFeuDansPhases(ByVal uneLigneFeux As LigneFeux, ByVal Scénarios As TreeNodeCollection)
    Dim unScénario As TreeNode
    Dim ScénarioCloné As TreeNode
    Dim NoeudPhase As TreeNode
    Dim NoeudFeu As TreeNode
    Dim i As Integer

    Try
      If Scénarios.Count = 0 Then
        'Créer le 1er scénario : le libellé d'1 noeud scénario est  "Sn"
        unScénario = Scénarios.Add("S1")

        'Créer la 1ère phase du 1er scénario : Le libellé d'un noeud phase est "Phasen"
        unScénario.Nodes.Add("Phase1")
        'Mettre la ligne de feux dans la 1ère phase
        AjouterFeuDansPhase(uneLigneFeux, unScénario.FirstNode)

      Else
        Dim ScénariosImpossibles As New Hashtable
        For Each unScénario In Scénarios
          'Par défaut : aucune phase ne convient
          unScénario.Tag = 0
          For Each NoeudPhase In unScénario.Nodes
            'Par défaut tous les feux de la phase sont compatibles
            NoeudPhase.Tag = 0
            For Each NoeudFeu In NoeudPhase.Nodes
              If mLignesFeux.EstIncompatible(uneLigneFeux, mLignesFeux(NoeudFeu.Text)) Then
                NoeudPhase.Tag = -1
                Exit For
              End If
            Next
            If NoeudPhase.Tag = 0 Then
              'Le feu peut être mis dans cette phase de ce scénario
              unScénario.Tag += 1      'Une phase de + convient pour ce scénario
              NoeudPhase.Tag = unScénario.Tag
            End If
          Next

          Select Case unScénario.Tag
            Case 0
              'Aucune phase ne convient pour ce feu dans ce scénario
              If unScénario.LastNode.Index = MAXPHASES - 1 Then
                'On ne peut plus ajouter de phase : ce scénario doit être abandonné
                ScénariosImpossibles.Add(ScénariosImpossibles.Count, unScénario)
              Else
                'Ajouter une phase et y mettre la ligne de feux
                AjouterPhaseEtFeu(unScénario, uneLigneFeux)
              End If

            Case Else
              If unScénario.LastNode.Index <= MAXPHASES - 2 Then
                'Cloner le scénario
                ScénarioCloné = ScénarioAjouté(Scénarios, unScénario)
                'Ajouter une phase et y mettre la ligne de feux
                AjouterPhaseEtFeu(ScénarioCloné, uneLigneFeux)
              End If

              'Si plusieurs phases conviennent : cloner d'abord le scénario et mettre la ligne de feux dans les phases suivantes qui conviennent
              For i = 2 To unScénario.Tag
                ScénarioCloné = ScénarioAjouté(Scénarios, unScénario)
                AjouterFeuDansPhase(uneLigneFeux, ScénarioCloné.Nodes, i)
              Next

              'Mettre le feu dans la 1ère phase qui convient dans le scénario
              AjouterFeuDansPhase(uneLigneFeux, unScénario.Nodes, 1)

          End Select

        Next

        For i = 0 To ScénariosImpossibles.Count - 1
          unScénario = ScénariosImpossibles(i)
          unScénario.Remove()
        Next
        ScénariosImpossibles.Clear()
      End If

    Catch ex As System.Exception
      LancerDiagfeuxException(ex, "InsérerFeuDansPhases")
    End Try

  End Sub
  '**********************************************************************************************************************
  ' Retourne le scénario de phasage ajouté à la liste des scénarios
  ' Scénarios : Liste des scénarios
  ' Scénario : Scénario à cloner
  '**********************************************************************************************************************
  Private Function ScénarioAjouté(ByVal Scénarios As TreeNodeCollection, ByVal Scénario As TreeNode) As TreeNode

    ScénarioAjouté = CType(Scénario.Clone, TreeNode)
    Scénarios.Add(ScénarioAjouté)
    ScénarioAjouté.Text = "S" & CStr(Scénarios.Count)

  End Function
  '**********************************************************************************************************************
  ' Ajouter une phase à un scénario et y Insérer le feu
  '**********************************************************************************************************************
  Private Sub AjouterPhaseEtFeu(ByVal Scénario As TreeNode, ByVal uneLigneFeux As LigneFeux)
    With Scénario
      AjouterFeuDansPhase(uneLigneFeux, .Nodes.Add("Phase" & CStr(.LastNode.Index + 2)))
    End With
  End Sub

  '**********************************************************************************************************************
  ' Rechercher la phase où insérer le feu et l'insérer dans celle-ci
  ' NoeudsPhases : Liste des phases à explorer
  ' Index : Drapeau permettant de repérer la bonne phase
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
  ' Ajouter le feu dans  une phase  et mémoriser dans le treenode s'il est piéton ou véhicule
  '**********************************************************************************************************************
  Private Overloads Sub AjouterFeuDansPhase(ByVal uneLigneFeux As LigneFeux, ByVal NoeudPhase As TreeNode)
    NoeudPhase.Nodes.Add(uneLigneFeux.ID).Tag = uneLigneFeux.EstPiéton
  End Sub

#End Region

#End Region
  '**********************************************************************
  'Déterminer les autorisations de décalage pour les lignes de feux du plan
  '**********************************************************************
  Public Sub DéterminerAutorisationsDécalage()
    Dim unePhase As Phase
    Dim uneLigneFeux As LigneFeux

    For Each unePhase In mPhases
      For Each uneLigneFeux In unePhase.mLignesFeux
        uneLigneFeux.DéterminerAutorisationDécalage(unePhase)
      Next
    Next
  End Sub

End Class

'=====================================================================================================
'--------------------------- Classe FiltrePhasage --------------------------
'=====================================================================================================
Public Class FiltrePhasage
  Public TroisPhases As Boolean
  Public CritèreCapacité As CapacitéEnum
  Public LigneFeuxMultiPhases As LFMultiphasesEnum
  Public AvecPhaseSpéciale As PhaseSpécialeEnum

  Public Enum CapacitéEnum
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

  Public Enum PhaseSpécialeEnum
    Inclure
    Exclure
    Uniquement
  End Enum
End Class

'=====================================================================================================
'--------------------------- Classe PlanFeuxPhasage --------------------------
'=====================================================================================================
Public Class PlanFeuxPhasage : Inherits PlanFeuxBase
  Private mPlanBaseAssocié As PlanFeuxBase

  Public Sub New(ByVal unPlanFeux As PlanFeuxPhasage)
    'Création d'un plan de phasage identique en vue de rajouter des lignes de feux sur + d'une phase
    MyBase.New(unPlanFeux)

    mPlanParent = unPlanFeux
    '    mDemandeUVP = New Hashtable
  End Sub

  Public Sub New(ByVal unPlanFeux As PlanFeuxBase)
    'Création d'un plan de phasage associé à un plan de feux de base
    mPlanParent = unPlanFeux
    '    mDemandeUVP = New Hashtable
  End Sub

  '*************************************************************************
  ' Plande feux de base équivalent au plan de phasage
  '*************************************************************************
  Public Property PlanBaseAssocié() As PlanFeuxBase
    Get
      Return mPlanBaseAssocié
    End Get
    Set(ByVal Value As PlanFeuxBase)
      Dim exPlanBase As PlanFeuxBase = mPlanBaseAssocié

      If Not mPlanBaseAssocié Is Value Then
        mPlanBaseAssocié = Value
        If IsNothing(Value) Then
          If Not IsNothing(exPlanBase) Then
            exPlanBase.PlanPhasageAssocié = Nothing
          End If
        Else
          mPlanBaseAssocié.PlanPhasageAssocié = Me
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
  Private mNbVéhiculesEnAttente As New Hashtable

#Region "DuréesMini"
  '********************************************************************************************
  'DuréeMini : Durée minimum du cycle du plan de feux
  '********************************************************************************************
  Public Overrides ReadOnly Property DuréeMini() As Short
    'Durée mini = durée du cycle du plan de feux de base(verrouillée)
    Get
      DuréeMini = mPlanBase.DuréeCycle
    End Get
  End Property

  Public Overrides Property RougeIncompressible(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mPlanBase.RougeIncompressible(uneLigneFeux)
    End Get
    Set(ByVal Value As Short)
      'Valeur jamais affectée car c'est celle du plan de feux de base correspondant
    End Set
  End Property

#End Region

#Region "TempsAttente"
  '**********************************************************************
  ' Temps moyen d'attente véhicule sur l'ensemble des lignes du carrefour
  ' Référence : 'Compléments de calcul' du CERTU (28/11/2005)
  '**********************************************************************
  Public Function TMAVéhicules() As Integer
    Dim uneLigneFeux As LigneFeux
    Dim SommeRetards, SommeDemandes, DemandeLF As Single

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstVéhicule Then
        With CType(uneLigneFeux, LigneFeuVéhicules)
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
  ' Temps moyen d'attente piétons sur l'ensemble des lignes du carrefour
  ' Référence : aucune
  '**********************************************************************
  Public Function TMAPiétons() As Integer
    Dim uneLigneFeux As LigneFeux
    Dim SommeRetards As Single
    Dim nbLignesPiétons As Short

    For Each uneLigneFeux In mLignesFeux
      If uneLigneFeux.EstPiéton Then
        nbLignesPiétons += 1
        With CType(uneLigneFeux, LigneFeuPiétons)
          SommeRetards += RetardMoyen(uneLigneFeux) * Me.Trafic.QPiéton(uneLigneFeux.mBranche)
        End With
      End If
    Next

    If nbLignesPiétons > 0 And Me.Trafic.QPiétonTotal Then
      'Le trafic piéton peut n'être pas renseigné
      Return CType(SommeRetards / Me.Trafic.QPiétonTotal, Integer)
    End If

  End Function

  Public Sub AffecterInfosAttente()
    Dim uneLigneFeux As LigneFeux
    Dim uneDuréeCycle As Short = DuréeCycle()

    For Each uneLigneFeux In mLignesFeux
      AffecterInfosAttente(uneLigneFeux, uneDuréeCycle)
    Next

  End Sub

  Public Sub AffecterInfosAttente(ByVal uneLigneFeux As LigneFeux, ByVal uneDuréeCycle As Short)

    ' Référence : 'Compléments de calcul' du CERTU (28/11/2005) : Longueur de file d'attente

    NbVéhiculesEnAttente(uneLigneFeux) = Math.Ceiling(DemandeUVP(uneLigneFeux) / 3600 * (uneDuréeCycle - VertUtile(uneLigneFeux)))

    AffecterRetardMoyen(uneLigneFeux, DuréeCycle)
  End Sub

  '***********************************************************************
  ' Calculer le retard moyen subi par un piéton ou
  ' par un véhicule sur une file de la ligne de feux
  ' Référence : 'Compléments de calcul' du CERTU (28/11/2005)
  '***********************************************************************
  Protected Sub AffecterRetardMoyen(ByVal uneLigneFeux As LigneFeux, ByVal uneDuréeCycle As Short)
    'La demande est=0 pour les piétons

    RetardMoyen(uneLigneFeux) = Math.Ceiling(Carré((uneDuréeCycle - VertUtile(uneLigneFeux))) / _
    (2 * uneDuréeCycle * (1 - (DemandeUVP(uneLigneFeux) / mVariante.DébitSaturation))) _
    )

  End Sub

  Public Property NbVéhiculesEnAttente(ByVal uneLigneFeux As LigneFeux) As Short
    Get
      Return mNbVéhiculesEnAttente(uneLigneFeux.ID)
    End Get
    Set(ByVal Value As Short)
      mNbVéhiculesEnAttente(uneLigneFeux.ID) = Value
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
      'on compte 5 m par véhicule en attente(uvp)
      Return NbVéhiculesEnAttente(uneLigneFeux) * LgMoyenneVéhicule
    End Get
  End Property


  'Public Function CalculClassique(ByVal RéserveCapacité As Single) As Short
  '  'Demande majorée d'une marge de trafic supplémentaire acceptable(0 - 0.10 - 0.15 - 0.20)
  '  Dim DemandePriseEncompte As Short = (1 + RéserveCapacité) * mDemande

  '  Dim COptimum, Capacité As Short
  '  Dim unePhase As Phase
  '  Dim i, j As Short
  '  Dim CapacitéInférieure, CapacitéSupérieure As Short
  '  Dim IndiceCycleMaxi As Short = TbCycleCapacité.GetUpperBound(0)

  '  Dim TempsPerdu As Short = mPhases.TempsPerdu

  '  If TempsPerdu < TempsPerduMini Or TempsPerdu >= TempsPerduMini + TbCycleCapacité.Length Then
  '    'Hors Abaque
  '  Else
  '    CapacitéInférieure = TbCycleCapacité(0, TempsPerdu - TempsPerduMini)
  '    If DemandePriseEncompte <= CapacitéInférieure Then
  '      Return DuréeCycleMini
  '    Else
  '      For i = 1 To IndiceCycleMaxi
  '        CapacitéSupérieure = TbCycleCapacité(i, TempsPerdu - TempsPerduMini)
  '        If DemandePriseEncompte <= CapacitéSupérieure Then
  '          Return CType(DuréeCycleMini + IncrémentCycle * (i - 1 + (DemandePriseEncompte - CapacitéInférieure) / (CapacitéSupérieure - CapacitéInférieure)), Short)
  '        End If
  '        CapacitéInférieure = CapacitéSupérieure
  '      Next
  '    End If

  '    Return DuréeCycleMini + IncrémentCycle * IndiceCycleMaxi

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
      'Plan de feux de fonctionnement basé sur le plan de base unPlanFeux
      mPlanBase = unPlanFeux
    ElseIf IsNothing(pBase) Then
      'Plan de feux de fonctionnement dupliqué à partir du plan de feux de fonctionnement unPlanFeux
      Dim unPlanFeuxFct As PlanFeuxFonctionnement = unPlanFeux
      mPlanBase = unPlanFeuxFct.mPlanBase
    Else
      'Plan de feux de fonctionnement issu de la duplication de scénario
      mPlanBase = pBase
    End If

    'Dimensionner()
    IndexerLignesFeux()

    'Ajouter le plan à la collection 
    mPlanBase.mPlansFonctionnement.Add(Me)

    mNom = NomPlan

  End Sub

  Public Sub New(ByVal uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow, ByVal unPlanFeuxBase As PlanFeuxBase)
    MyBase.New(uneRowPlanFeux)
    mPlanBase = unPlanFeuxBase

    'Dimensionner les différents tableaux selon le nombre de lignes de feux
    'Dimensionner()

    'Ajouter le plan à la collection 
    mPlanBase.mPlansFonctionnement.Add(Me)
    mPlanBase.ReCréerTrafics()

    Nom = uneRowPlanFeux.NomFonctionnement

  End Sub


  ''********************************************************************************************
  ''Dimensionner les différents tableaux selon le nombre de lignes de feux
  ''********************************************************************************************
  'Protected Overrides Sub Dimensionner()

  '  'Dimensionner mDemandeUVP
  '  'MyBase.Dimensionner()

  '  'Dimensionner les autres tableaux
  '  ReDim mNbVéhiculesEnAttente(mLignesFeux.Count - 1)
  '  ReDim mRetardMoyen(mLignesFeux.Count - 1)

  'End Sub

#End Region

  Public Overrides Function Enregistrer(ByVal uneVariante As Variante, ByVal uneRowVariante As DataSetDiagfeux.VarianteRow) As DataSetDiagfeux.PlanFeuxRow
    'Appeler la fonction de la classe de base
    Dim uneRowPlanFeux As DataSetDiagfeux.PlanFeuxRow = MyBase.Enregistrer(uneVariante, uneRowVariante)

    With uneRowPlanFeux
      'Référencer le plan de feux de base associé
      .ID = mPlanBase.Nom
      .NomFonctionnement = mNom
    End With

  End Function

#Region "Propriétés"
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
        mPlanBase.ReCréerTrafics()
      End If
    End Set
  End Property
#End Region

End Class