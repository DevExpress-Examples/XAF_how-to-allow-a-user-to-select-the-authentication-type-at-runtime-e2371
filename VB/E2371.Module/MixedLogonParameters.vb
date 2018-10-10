Imports Microsoft.VisualBasic
Imports DevExpress.ExpressApp.Security
Imports System
Imports System.ComponentModel
Imports System.Linq

Namespace E2371.Module
	Public Enum AuthenticationType
		Application
		Windows
	End Enum
	<Serializable> _
	Public Class MixedLogonParameters
		Inherits AuthenticationStandardLogonParameters
		Private privateAuthenticationType As AuthenticationType
		Public Property AuthenticationType() As AuthenticationType
			Get
				Return privateAuthenticationType
			End Get
			Set(ByVal value As AuthenticationType)
				privateAuthenticationType = value
			End Set
		End Property
	End Class
End Namespace
