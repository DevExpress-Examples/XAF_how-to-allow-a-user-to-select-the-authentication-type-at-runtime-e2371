Imports Microsoft.VisualBasic
Imports System
Namespace E2371.Win
	Partial Public Class E2371WindowsFormsApplication
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
			Me.module3 = New E2371.Module.E2371Module()
			Me.module4 = New E2371.Module.Win.E2371WindowsFormsModule()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
			Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
			Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
			Me.validationWindowsFormsModule = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
			Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' securityStrategyComplex1
			' 
			Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
			Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            Me.securityStrategyComplex1.UserType = GetType(E2371.Module.BusinessObjects.CustomPermissionPolicyUser)
            ' 
            ' securityModule1
            ' 
            Me.securityModule1.UserType = GetType(E2371.Module.BusinessObjects.CustomPermissionPolicyUser)
            ' 
            ' authenticationStandard1
            ' 
            Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
			' 
			' E2371WindowsFormsApplication
			' 
			Me.ApplicationName = "E2371"
			Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.module4)
			Me.Modules.Add(Me.securityModule1)
			Me.Security = Me.securityStrategyComplex1
			Me.Modules.Add(Me.validationModule)
			Me.Modules.Add(Me.validationWindowsFormsModule)
			Me.UseOldTemplates = False
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.E2371WindowsFormsApplication_DatabaseVersionMismatch);
'			Me.CustomizeLanguagesList += New System.EventHandler(Of DevExpress.ExpressApp.CustomizeLanguagesListEventArgs)(Me.E2371WindowsFormsApplication_CustomizeLanguagesList);

			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
		Private module3 As E2371.Module.E2371Module
		Private module4 As E2371.Module.Win.E2371WindowsFormsModule
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
		Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
		Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
		Private validationWindowsFormsModule As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
	End Class
End Namespace
