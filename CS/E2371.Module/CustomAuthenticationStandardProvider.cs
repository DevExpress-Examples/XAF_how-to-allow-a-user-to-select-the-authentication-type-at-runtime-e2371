using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using E2371.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2371.Module {
    public class CustomAuthenticationStandardProvider : AuthenticationStandardProvider {
        public CustomAuthenticationStandardProvider(Type userType) : base(userType) {
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            CustomPermissionPolicyUser customPermissionPolicyUser = null;
            try {
                customPermissionPolicyUser = base.Authenticate(objectSpace) as CustomPermissionPolicyUser;
            }
            catch(AuthenticationException e) {
                ThrowAuthenticationError(e.UserName);
            }
            if(customPermissionPolicyUser != null && customPermissionPolicyUser.IsActiveDirectoryUser) {
                ThrowAuthenticationError(customPermissionPolicyUser.UserName);
            }
            return customPermissionPolicyUser;
        }

        private static void ThrowAuthenticationError(string userName) {
            throw new Exception(string.Format("Login failed for '{0}'. Make sure your user name is correct and retype the password in the correct case, or use the Windows authentication instead.", userName));
        }
    }
}
