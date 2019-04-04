<!-- default file list -->
*Files to look at*:

* [Model.DesignedDiffs.xafml](./CS/Solution73.Module.Web/Model.DesignedDiffs.xafml) (VB: [Model.DesignedDiffs.xafml](./VB/Solution73.Module.Web/Model.DesignedDiffs.xafml))
* [Updater.cs](./CS/Solution73.Module.Web/Updater.cs) (VB: [Updater.vb](./VB/Solution73.Module.Web/Updater.vb))
* [WebModule.cs](./CS/Solution73.Module.Web/WebModule.cs) (VB: [WebModule.vb](./VB/Solution73.Module.Web/WebModule.vb))
* [Model.DesignedDiffs.xafml](./CS/Solution73.Module.Win/Model.DesignedDiffs.xafml) (VB: [Model.DesignedDiffs.xafml](./VB/Solution73.Module.Win/Model.DesignedDiffs.xafml))
* [Updater.cs](./CS/Solution73.Module.Win/Updater.cs) (VB: [Updater.vb](./VB/Solution73.Module.Win/Updater.vb))
* [WinModule.cs](./CS/Solution73.Module.Win/WinModule.cs) (VB: [WinModule.vb](./VB/Solution73.Module.Win/WinModule.vb))
* [Authentication.cs](./CS/Solution73.Module/Authentication.cs) (VB: [Authentication.vb](./VB/Solution73.Module/Authentication.vb))
* [Model.DesignedDiffs.xafml](./CS/Solution73.Module/Model.DesignedDiffs.xafml) (VB: [Model.DesignedDiffs.xafml](./VB/Solution73.Module/Model.DesignedDiffs.xafml))
* [Module.cs](./CS/Solution73.Module/Module.cs) (VB: [Module.vb](./VB/Solution73.Module/Module.vb))
* [Updater.cs](./CS/Solution73.Module/Updater.cs) (VB: [Updater.vb](./VB/Solution73.Module/Updater.vb))
* [WebApplication.cs](./CS/Solution73.Web/ApplicationCode/WebApplication.cs) (VB: [WebApplication.vb](./VB/Solution73.Web/ApplicationCode/WebApplication.vb))
* [Default.aspx](./CS/Solution73.Web/Default.aspx) (VB: [Default.aspx](./VB/Solution73.Web/Default.aspx))
* [Default.aspx.cs](./CS/Solution73.Web/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Solution73.Web/Default.aspx.vb))
* [Error.aspx](./CS/Solution73.Web/Error.aspx) (VB: [Error.aspx](./VB/Solution73.Web/Error.aspx))
* [Error.aspx.cs](./CS/Solution73.Web/Error.aspx.cs) (VB: [Error.aspx.vb](./VB/Solution73.Web/Error.aspx.vb))
* [Global.asax](./CS/Solution73.Web/Global.asax) (VB: [Global.asax](./VB/Solution73.Web/Global.asax))
* [Global.asax.cs](./CS/Solution73.Web/Global.asax.cs) (VB: [Global.asax.vb](./VB/Solution73.Web/Global.asax.vb))
* [Login.aspx](./CS/Solution73.Web/Login.aspx) (VB: [Login.aspx](./VB/Solution73.Web/Login.aspx))
* [Login.aspx.cs](./CS/Solution73.Web/Login.aspx.cs) (VB: [Login.aspx.vb](./VB/Solution73.Web/Login.aspx.vb))
* [Model.xafml](./CS/Solution73.Web/Model.xafml) (VB: [Model.xafml](./VB/Solution73.Web/Model.xafml))
* [Model.xafml](./CS/Solution73.Win/Model.xafml) (VB: [Model.xafml](./VB/Solution73.Win/Model.xafml))
* [Program.cs](./CS/Solution73.Win/Program.cs) (VB: [Program.vb](./VB/Solution73.Win/Program.vb))
* [WinApplication.cs](./CS/Solution73.Win/WinApplication.cs) (VB: [WinApplication.vb](./VB/Solution73.Win/WinApplication.vb))
<!-- default file list end -->
# How to: Allow a user to select the authentication type at runtime


To accomplish this task, you should create a new authentication type, inherited from the AuthenticationBase, and combine the code of the AuthenticationStandard and AuthenticationActiveDirectory classes in it. Additionally, you should create a custom logon parameters class with a property, allowing users to select the authentication type.

>In the client-server security configuration, you are additionally required to:  
>- override the [AuthenticationBase.SetLogonParameters](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.AuthenticationBase.SetLogonParameters.method) method to initialize the [AuthenticationBase.LogonParameters](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.AuthenticationBase.LogonParameters.property) property;
>- register the custom logon parameters type using the static [WcfDataServerHelper.AddKnownType](https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.ClientServer.Wcf.WcfDataServerHelper.AddKnownType.method) method before the data server is initialized.

In the example, we set a random password for users that are automatically created with AD. Thus we prevent logging in with default authentication, an AD username and an empty password. 

<strong>See Also:</strong>  
[How to: Use Custom Logon Parameters and Authentication](https://documentation.devexpress.com/eXpressAppFramework/112982/Task-Based-Help/Security/How-to-Use-Custom-Logon-Parameters-and-Authentication)  
[How to enable the ResetPasswordController and ChangePasswordController Actions (Reset and Change Password) when ActiveDirectoryAuthentication is used](https://www.devexpress.com/Support/Center/p/Q478325)


