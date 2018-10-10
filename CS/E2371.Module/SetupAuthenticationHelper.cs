using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using E2371.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2371.Module {
    public static class SetupAuthenticationHelper {
        public static void Setup(XafApplication application) {
            AuthenticationMixed mixedAuthentication = new AuthenticationMixed();
            mixedAuthentication.LogonParametersType = typeof(MixedLogonParameters);
            mixedAuthentication.IsSupportChangePassword = true;
            CustomAuthenticationStandardProvider authenticationStandardProvider = new CustomAuthenticationStandardProvider(application.Security.UserType);
            AuthenticationActiveDirectoryProvider authenticationActiveDirectoryProvider = new AuthenticationActiveDirectoryProvider(application.Security.UserType, (ICanInitializeNewUser)application.Security, true);
            authenticationActiveDirectoryProvider.CustomCreateUser += AuthenticationActiveDirectoryProvider_CustomCreateUser;
            mixedAuthentication.AuthenticationProviders.Add(typeof(AuthenticationStandardProvider).Name, authenticationStandardProvider);
            mixedAuthentication.AuthenticationProviders.Add(typeof(AuthenticationActiveDirectoryProvider).Name, authenticationActiveDirectoryProvider);
            ((SecurityStrategyComplex)application.Security).Authentication = mixedAuthentication;
            application.LoggingOn += SecurityMixedAuthenticationWindowsFormsApplication_LoggingOn;
        }
        private static void SecurityMixedAuthenticationWindowsFormsApplication_LoggingOn(object sender, LogonEventArgs e) {
            XafApplication application = (XafApplication)sender;
            AuthenticationMixed authenticationMixed = (AuthenticationMixed)((SecurityStrategyComplex)application.Security).Authentication;
            switch(((MixedLogonParameters)e.LogonParameters).AuthenticationType) {
                case AuthenticationType.Application:
                    authenticationMixed.SetupAuthenticationProvider(typeof(AuthenticationStandardProvider).Name, e.LogonParameters);
                    break;
                case AuthenticationType.Windows:
                    authenticationMixed.SetupAuthenticationProvider(typeof(AuthenticationActiveDirectoryProvider).Name);
                    break;
            }
        }
        private static void AuthenticationActiveDirectoryProvider_CustomCreateUser(object sender, CustomCreateUserEventArgs e) {
            AuthenticationActiveDirectoryProvider provider = (AuthenticationActiveDirectoryProvider)sender;
            CustomPermissionPolicyUser user = e.ObjectSpace.CreateObject<CustomPermissionPolicyUser>();
            user.UserName = e.UserName;
            user.IsActiveDirectoryUser = true;
            provider.CanInitializeNewUser.InitializeNewUser(e.ObjectSpace, user);
            e.User = user;
            e.Handled = true;
        }
    }
}
