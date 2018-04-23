Namespace Solution73.Win
    Partial Public Class Solution73WindowsFormsApplication
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
            Me.module3 = New Solution73.Module.Solution73Module()
            Me.module4 = New Solution73.Module.Win.Solution73WindowsFormsModule()
            Me.module5 = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            Me.module7 = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
            Me.securityComplex1 = New DevExpress.ExpressApp.Security.SecurityComplex()
            Me.authenticationCombined1 = New DevExpress.ExpressApp.Security.AuthenticationCombined()
            DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' module5
            ' 
            Me.module5.AllowValidationDetailsAccess = True
            ' 
            ' sqlConnection1
            ' 
            Me.sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=Solution73;Integrated Security=SSPI;Pooling=f" & "alse"
            Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
            ' 
            ' securityComplex1
            ' 
            Me.securityComplex1.Authentication = Me.authenticationCombined1
            Me.securityComplex1.IsGrantedForNonExistentPermission = False
            Me.securityComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.Role)
            Me.securityComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.User)
            ' 
            ' authenticationCombined1
            ' 
            Me.authenticationCombined1.CreateUserAutomatically = True
            Me.authenticationCombined1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationCombinedLogonParameters)
            ' 
            ' Solution73WindowsFormsApplication
            ' 
            Me.ApplicationName = "Solution73"
            Me.Connection = Me.sqlConnection1
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module6)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.module4)
            Me.Modules.Add(Me.module5)
            Me.Modules.Add(Me.module7)
            Me.Security = Me.securityComplex1
            DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        #End Region

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private module3 As Solution73.Module.Solution73Module
        Private module4 As Solution73.Module.Win.Solution73WindowsFormsModule
        Private module5 As DevExpress.ExpressApp.Validation.ValidationModule
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private module7 As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private sqlConnection1 As System.Data.SqlClient.SqlConnection
        Private securityComplex1 As DevExpress.ExpressApp.Security.SecurityComplex
        Private authenticationCombined1 As DevExpress.ExpressApp.Security.AuthenticationCombined
    End Class
End Namespace
