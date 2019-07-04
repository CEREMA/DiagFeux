'******************************************************************************
'																																							'
'						Projet DIAGFEUX : programmation des carrefours à feux							'
'						Maitrise d'ouvrage : CERTU																				'	
'						Maitrise d'oeuvre : CETE de l'OUEST - ITS													'		
'						Auteur : André VIGNAUD																						'
'																																							'
'						Source : Exception.vb	   									  											'
'						Classes																														'
'							Exception																												'
'             ErreurSansMessage                                               '
'             MétierException                                                 '
'******************************************************************************

Imports System.IO

'=====================================================================================================
'----------- Class Exception : Gestion des exceptions de l'application ---------------------
'=====================================================================================================
Public Class Exception : Inherits ApplicationException

  Public Sub New(ByVal Message As String)
    MyBase.New(Message)
  End Sub

End Class

'=====================================================================================================
'----------- Class ErreurSansMessage  ---------------------
'=====================================================================================================
Public Class ErreurSansMessage : Inherits DiagFeux.Exception
  Public Sub New()
    MyBase.New("")
  End Sub
End Class

'=====================================================================================================
'----------- Class MétierException  ---------------------
'=====================================================================================================
Public Class MétierException : Inherits DiagFeux.Exception

  Public Sub New(ByVal Message As String)
    MyBase.New(Message)
  End Sub
End Class