Imports Microsoft.VisualBasic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports E2371.Module.BusinessObjects
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace E2371.Module
	Public NotInheritable Class SetupAuthenticationHelper
		Private Sub New()
		End Sub
		Public Shared Sub Setup(ByVal application As XafApplication)
			Dim mixedAuthentication As New AuthenticationMixed()
			mixedAuthentication.LogonParametersType = GetType(MixedLogonParameters)
			mixedAuthentication.IsSupportChangePassword = True
			Dim authenticationStandardProvider As New CustomAuthenticationStandardProvider(application.Security.UserType)
			Dim authenticationActiveDirectoryProvider As New AuthenticationActiveDirectoryProvider(application.Security.UserType, CType(application.Security, ICanInitializeNewUser), True)
			AddHandler authenticationActiveDirectoryProvider.CustomCreateUser, AddressOf AuthenticationActiveDirectoryProvider_CustomCreateUser
			mixedAuthentication.AuthenticationProviders.Add(GetType(AuthenticationStandardProvider).Name, authenticationStandardProvider)
			mixedAuthentication.AuthenticationProviders.Add(GetType(AuthenticationActiveDirectoryProvider).Name, authenticationActiveDirectoryProvider)
			CType(application.Security, SecurityStrategyComplex).Authentication = mixedAuthentication
			AddHandler application.LoggingOn, AddressOf SecurityMixedAuthenticationWindowsFormsApplication_LoggingOn
		End Sub
		Private Shared Sub SecurityMixedAuthenticationWindowsFormsApplication_LoggingOn(ByVal sender As Object, ByVal e As LogonEventArgs)
			Dim application As XafApplication = CType(sender, XafApplication)
			Dim authenticationMixed As AuthenticationMixed = CType((CType(application.Security, SecurityStrategyComplex)).Authentication, AuthenticationMixed)
			Select Case (CType(e.LogonParameters, MixedLogonParameters)).AuthenticationType
				Case AuthenticationType.Application
					authenticationMixed.SetupAuthenticationProvider(GetType(AuthenticationStandardProvider).Name, e.LogonParameters)
				Case AuthenticationType.Windows
					authenticationMixed.SetupAuthenticationProvider(GetType(AuthenticationActiveDirectoryProvider).Name)
			End Select
		End Sub
		Private Shared Sub AuthenticationActiveDirectoryProvider_CustomCreateUser(ByVal sender As Object, ByVal e As CustomCreateUserEventArgs)
			Dim provider As AuthenticationActiveDirectoryProvider = CType(sender, AuthenticationActiveDirectoryProvider)
			Dim user As CustomPermissionPolicyUser = e.ObjectSpace.CreateObject(Of CustomPermissionPolicyUser)()
			user.UserName = e.UserName
			user.IsActiveDirectoryUser = True
			provider.CanInitializeNewUser.InitializeNewUser(e.ObjectSpace, user)
			e.User = user
			e.Handled = True
		End Sub
	End Class
End Namespace
