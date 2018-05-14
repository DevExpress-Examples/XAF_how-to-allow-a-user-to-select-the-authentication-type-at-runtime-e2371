# How to: Allow a user to select the authentication type at runtime


To accomplish this task, you should create a new authentication type, inherited from the AuthenticationBase, and combine the code of the AuthenticationStandard and AuthenticationActiveDirectory classes in it. Additionally, you should create a custom logon parameters class with a property, allowing users to select the authentication type.

>In the client-server security configuration, you are additionally required to:  
>- override the [AuthenticationBase.SetLogonParameters](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.AuthenticationBase.SetLogonParameters.method) method to initialize the [AuthenticationBase.LogonParameters](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.AuthenticationBase.LogonParameters.property) property;
>- register the custom logon parameters type using the static [WcfDataServerHelper.AddKnownType](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.ClientServer.Wcf.WcfDataServerHelper.AddKnownType.method) method before the data server is initialized.


<strong>See Also:</strong>  
[How to: Use Custom Logon Parameters and Authentication](https://documentation.devexpress.com/eXpressAppFramework/112982/Task-Based-Help/Security/How-to-Use-Custom-Logon-Parameters-and-Authentication)  
[How to enable the ResetPasswordController and ChangePasswordController Actions (Reset and Change Password) when ActiveDirectoryAuthentication is used](https://www.devexpress.com/Support/Center/p/Q478325)


