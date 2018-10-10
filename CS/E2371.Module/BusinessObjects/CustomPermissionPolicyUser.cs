using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2371.Module.BusinessObjects {
    public class CustomPermissionPolicyUser : PermissionPolicyUser {
        private bool isActiveDirectoryUser;
        public bool IsActiveDirectoryUser {
            get { return isActiveDirectoryUser; }
            set { SetPropertyValue(nameof(IsActiveDirectoryUser), ref isActiveDirectoryUser, value); }
        }
        public CustomPermissionPolicyUser(Session session) : base(session) {
        }
    }
}
