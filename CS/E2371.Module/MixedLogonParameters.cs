using DevExpress.ExpressApp.Security;
using System;
using System.ComponentModel;
using System.Linq;

namespace E2371.Module {
    public enum AuthenticationType { Application, Windows }
    [Serializable]
    public class MixedLogonParameters : AuthenticationStandardLogonParameters {
        public AuthenticationType AuthenticationType { get; set; }
    }
}
