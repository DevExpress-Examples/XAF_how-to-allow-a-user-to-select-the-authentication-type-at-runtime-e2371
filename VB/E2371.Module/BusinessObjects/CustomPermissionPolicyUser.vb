Imports Microsoft.VisualBasic
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Xpo
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace E2371.Module.BusinessObjects
	Public Class CustomPermissionPolicyUser
		Inherits PermissionPolicyUser
		Private isActiveDirectoryUser_Renamed As Boolean
		Public Property IsActiveDirectoryUser() As Boolean
			Get
				Return isActiveDirectoryUser_Renamed
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue(nameof(IsActiveDirectoryUser), isActiveDirectoryUser_Renamed, value)
			End Set
		End Property
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
	End Class
End Namespace
