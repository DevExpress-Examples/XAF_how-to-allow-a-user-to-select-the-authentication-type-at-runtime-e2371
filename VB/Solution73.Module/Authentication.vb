Imports DevExpress.ExpressApp.Model
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.Persistent.Base.Security
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Design
Imports DevExpress.Persistent.Base
Imports System.Security.Principal
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.DC

Namespace DevExpress.ExpressApp.Security
    <DomainComponent, Serializable>
<System.ComponentModel.DisplayName("Log On")>
    Public Class AuthenticationCombinedLogonParameters
        Implements INotifyPropertyChanged


        Private useActiveDirectory_Renamed As Boolean

        Private userName_Renamed As String

        Private password_Renamed As String
        Public Sub New()
        End Sub
        Public Property UseActiveDirectory() As Boolean
            Get
                Return useActiveDirectory_Renamed
            End Get
            Set(ByVal value As Boolean)
                useActiveDirectory_Renamed = value
                RaisePropertyChanged("UseActiveDirectory")
            End Set
        End Property
        Public Property UserName() As String
            Get
                Return userName_Renamed
            End Get
            Set(ByVal value As String)
                userName_Renamed = value
                RaisePropertyChanged("UserName")
            End Set
        End Property
        <ModelDefault("IsPassword", "True")> _
        Public Property Password() As String
            Get
                Return password_Renamed
            End Get
            Set(ByVal value As String)
                password_Renamed = value
                RaisePropertyChanged("Password")
            End Set
        End Property
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
    <ToolboxItem(True), DevExpress.Utils.ToolboxTabName(DevExpress.ExpressApp.XafAssemblyInfo.DXTabXafSecurity)> _
    Public Class AuthenticationCombined
        Inherits AuthenticationBase
        Implements IAuthenticationStandard


        Private logonParameters_Renamed As AuthenticationCombinedLogonParameters

        Protected userType_Renamed As Type

        Protected logonParametersType_Renamed As Type

        Private createUserAutomatically_Renamed As Boolean = False
        Public Sub New()
            Me.LogonParametersType = GetType(AuthenticationCombinedLogonParameters)
        End Sub
        Public Sub New(ByVal userType As Type, ByVal logonParametersType As Type)
            Me.UserType = userType
            Me.LogonParametersType = logonParametersType
        End Sub
        Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
            If logonParameters_Renamed.UseActiveDirectory Then
                Return AuthenticateActiveDirectory(objectSpace)
            Else
                Return AuthenticateStandard(objectSpace)
            End If
        End Function
        Private Function AuthenticateStandard(ByVal objectSpace As IObjectSpace) As Object
            If String.IsNullOrEmpty(logonParameters_Renamed.UserName) Then
                Throw New ArgumentException(SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.UserNameIsEmpty))
            End If
            Dim user As IAuthenticationStandardUser = DirectCast(objectSpace.FindObject(UserType, New BinaryOperator("UserName", logonParameters_Renamed.UserName)), IAuthenticationStandardUser)
            If user Is Nothing OrElse (Not user.ComparePassword(logonParameters_Renamed.Password)) Then
                Throw New AuthenticationException(logonParameters_Renamed.UserName, SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation))
            End If
            Return user
        End Function
        Private Function AuthenticateActiveDirectory(ByVal objectSpace As IObjectSpace) As Object
            Dim userName As String = WindowsIdentity.GetCurrent().Name
            Dim user As IAuthenticationActiveDirectoryUser = DirectCast(objectSpace.FindObject(UserType, New BinaryOperator("UserName", userName)), IAuthenticationActiveDirectoryUser)
            If user Is Nothing Then
                If createUserAutomatically_Renamed Then
                    Dim args As New CustomCreateUserEventArgs(objectSpace, userName)
                    If Not args.Handled Then
                        user = DirectCast(objectSpace.CreateObject(UserType), IAuthenticationActiveDirectoryUser)
                        user.UserName = userName
                        'if (base.Security is ICanInitializeNewUser) {
                        '    ((ICanInitializeNewUser)base.Security).InitializeNewUser(objectSpace, user);
                        '}
                        If TypeOf Security Is SecurityStrategyBase Then
                            Dim mi As System.Reflection.MethodInfo = GetType(SecurityStrategyBase).GetMethod("InitializeNewUserCore", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                            mi.Invoke(Security, New Object() { objectSpace, user })
                        End If
                    End If
                    objectSpace.CommitChanges()
                End If
            End If
            If user Is Nothing Then
                Throw New AuthenticationException(userName)
            End If
            Return user
        End Function
        Public Overrides Sub ClearSecuredLogonParameters()
            logonParameters_Renamed.Password = String.Empty
            MyBase.ClearSecuredLogonParameters()
        End Sub
        Public Overrides Function IsSecurityMember(ByVal type As Type, ByVal memberName As String) As Boolean
            If GetType(IAuthenticationStandardUser).IsAssignableFrom(type) Then
                If GetType(IAuthenticationStandardUser).GetMember(memberName).Length > 0 Then
                    Return True
                End If
            End If
            If GetType(IAuthenticationActiveDirectoryUser).IsAssignableFrom(type) Then
                If GetType(IAuthenticationActiveDirectoryUser).GetMember(memberName).Length > 0 Then
                    Return True
                End If
            End If
            Return False
        End Function
        <Browsable(False)> _
        Public Overrides ReadOnly Property LogonParameters() As Object
            Get
                Return logonParameters_Renamed
            End Get
        End Property
        Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides Function GetBusinessClasses() As IList(Of Type)
            Dim result As New List(Of Type)()
            If UserType IsNot Nothing Then
                result.Add(UserType)
            End If
            If LogonParametersType IsNot Nothing Then
                result.Add(LogonParametersType)
            End If
            Return result
        End Function
        <Category("Behavior")> _
        Public Overrides Property UserType() As Type
            Get
                Return userType_Renamed
            End Get
            Set(ByVal value As Type)
                userType_Renamed = value
                If userType_Renamed IsNot Nothing AndAlso (Not GetType(IAuthenticationStandardUser).IsAssignableFrom(userType_Renamed)) AndAlso (Not GetType(IAuthenticationActiveDirectoryUser).IsAssignableFrom(userType_Renamed)) Then
                    Throw New ArgumentException(String.Format("AuthenticationCombined does not support the {0} user type." & ControlChars.Lf & "A class that implements the IAuthenticationStandardUser interface should be set for the UserType property.", userType_Renamed))
                End If
            End Set
        End Property
        <TypeConverter(GetType(BusinessClassTypeConverter(Of AuthenticationCombinedLogonParameters))), RefreshProperties(RefreshProperties.All), Category("Behavior")> _
        Public Property LogonParametersType() As Type
            Get
                Return logonParametersType_Renamed
            End Get
            Set(ByVal value As Type)
                logonParametersType_Renamed = value
                If value IsNot Nothing Then
                    If Not GetType(AuthenticationCombinedLogonParameters).IsAssignableFrom(logonParametersType_Renamed) Then
                        Throw New ArgumentException("LogonParametersType")
                    End If
                    logonParameters_Renamed = CType(ReflectionHelper.CreateObject(logonParametersType_Renamed, New Object(){}), AuthenticationCombinedLogonParameters)
                End If
            End Set
        End Property
        <Category("Behavior")> _
        Public Property CreateUserAutomatically() As Boolean
            Get
                Return createUserAutomatically_Renamed
            End Get
            Set(ByVal value As Boolean)
                createUserAutomatically_Renamed = value
            End Set
        End Property
    End Class
End Namespace

