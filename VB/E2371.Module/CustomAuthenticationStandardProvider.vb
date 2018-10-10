Imports Microsoft.VisualBasic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports E2371.Module.BusinessObjects
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace E2371.Module
	Public Class CustomAuthenticationStandardProvider
		Inherits AuthenticationStandardProvider
		Public Sub New(ByVal userType As Type)
			MyBase.New(userType)
		End Sub
		Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
			Dim customPermissionPolicyUser As CustomPermissionPolicyUser = Nothing
			Try
				customPermissionPolicyUser = TryCast(MyBase.Authenticate(objectSpace), CustomPermissionPolicyUser)
			Catch e As AuthenticationException
				ThrowAuthenticationError(e.UserName)
			End Try
			If customPermissionPolicyUser IsNot Nothing AndAlso customPermissionPolicyUser.IsActiveDirectoryUser Then
				ThrowAuthenticationError(customPermissionPolicyUser.UserName)
			End If
			Return customPermissionPolicyUser
		End Function

		Private Shared Sub ThrowAuthenticationError(ByVal userName As String)
			Throw New Exception(String.Format("Login failed for '{0}'. Make sure your user name is correct and retype the password in the correct case, or use the Windows authentication instead.", userName))
		End Sub
	End Class
End Namespace
