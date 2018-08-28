using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.Persistent.Base.Security;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Design;
using DevExpress.Persistent.Base;
using System.Security.Principal;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace DevExpress.ExpressApp.Security {
    [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("Log On")]
    public class AuthenticationCombinedLogonParameters : INotifyPropertyChanged {
        private bool useActiveDirectory;
        private string userName;
        private string password;
        public AuthenticationCombinedLogonParameters() { }
        public bool UseActiveDirectory {
            get { return useActiveDirectory; }
            set { useActiveDirectory = value; RaisePropertyChanged("UseActiveDirectory"); }
        }
        public string UserName {
            get { return userName; }
            set { userName = value; RaisePropertyChanged("UserName"); }
        }
        [ModelDefault("IsPassword", "True")]
        public string Password {
            get { return password; }
            set { password = value; RaisePropertyChanged("Password"); }
        }
        protected void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public AuthenticationCombinedLogonParameters(SerializationInfo info, StreamingContext context) {
            if(info.MemberCount > 0) {
                useActiveDirectory = info.GetBoolean("UseActiveDirectory");
                userName = info.GetString("UserName");
                password = info.GetString("Password");
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("UseActiveDirectory", UseActiveDirectory);
            info.AddValue("UesrName", UserName);
            info.AddValue("Password", Password);
        }
    }
    [ToolboxItem(true)]
    [DevExpress.Utils.ToolboxTabName(DevExpress.ExpressApp.XafAssemblyInfo.DXTabXafSecurity)]
    public class AuthenticationCombined : AuthenticationBase, IAuthenticationStandard {
        private AuthenticationCombinedLogonParameters logonParameters;
        protected Type userType;
        protected Type logonParametersType;
        private bool createUserAutomatically = false;
        public AuthenticationCombined() {
            this.LogonParametersType = typeof(AuthenticationCombinedLogonParameters);
        }
        public AuthenticationCombined(Type userType, Type logonParametersType) {
            this.UserType = userType;
            this.LogonParametersType = logonParametersType;
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            if (logonParameters.UseActiveDirectory)
                return AuthenticateActiveDirectory(objectSpace);
            else
                return AuthenticateStandard(objectSpace);
        }
        private object AuthenticateStandard(IObjectSpace objectSpace) {
            if (string.IsNullOrEmpty(logonParameters.UserName))
                throw new ArgumentException(SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.UserNameIsEmpty));
            IAuthenticationStandardUser user = (IAuthenticationStandardUser)objectSpace.FindObject(UserType, new BinaryOperator("UserName", logonParameters.UserName));
            if (user == null || !user.ComparePassword(logonParameters.Password)) {
                throw new AuthenticationException(logonParameters.UserName, SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation));
            }
            return user;
        }
        private object AuthenticateActiveDirectory(IObjectSpace objectSpace) {
            string userName = WindowsIdentity.GetCurrent().Name;
            IAuthenticationActiveDirectoryUser user = (IAuthenticationActiveDirectoryUser)objectSpace.FindObject(UserType, new BinaryOperator("UserName", userName));
            if (user == null) {
                if (createUserAutomatically) {
                    CustomCreateUserEventArgs args = new CustomCreateUserEventArgs(objectSpace, userName);
                    if (!args.Handled) {
                        user = (IAuthenticationActiveDirectoryUser)objectSpace.CreateObject(UserType);
                        user.UserName = userName;
                        var userWithPass = user as IAuthenticationStandardUser;
                        if(userWithPass != null) {
                            byte[] array = new byte[6];
                            new RNGCryptoServiceProvider().GetBytes(array);
                            var password = Convert.ToBase64String(array);
                            userWithPass.SetPassword(password);
                        }
                        //if (base.Security is ICanInitializeNewUser) {
                        //    ((ICanInitializeNewUser)base.Security).InitializeNewUser(objectSpace, user);
                        //}
                        if (Security is SecurityStrategyBase) {
                            System.Reflection.MethodInfo mi = typeof(SecurityStrategyBase).GetMethod("InitializeNewUserCore", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                            mi.Invoke(Security, new object[] { objectSpace, user });
                        }
                    }
                    objectSpace.CommitChanges();
                }
            }
            if (user == null) {
                throw new AuthenticationException(userName);
            }
            return user;
        }
        public override void ClearSecuredLogonParameters() {
            logonParameters.Password = string.Empty;
            base.ClearSecuredLogonParameters();
        }
        public override bool IsSecurityMember(Type type, string memberName) {
            if (typeof(IAuthenticationStandardUser).IsAssignableFrom(type)) {
                if (typeof(IAuthenticationStandardUser).GetMember(memberName).Length > 0) {
                    return true;
                }
            }
            if (typeof(IAuthenticationActiveDirectoryUser).IsAssignableFrom(type)) {
                if (typeof(IAuthenticationActiveDirectoryUser).GetMember(memberName).Length > 0) {
                    return true;
                }
            }
            return false;
        }
        [Browsable(false)]
        public override object LogonParameters {
            get {
                return logonParameters;
            }
        }
        public override bool AskLogonParametersViaUI {
            get {
                return true;
            }
        }
        public override bool IsLogoffEnabled {
            get { return true; }
        }
        public override IList<Type> GetBusinessClasses() {
            List<Type> result = new List<Type>();
            if (UserType != null) {
                result.Add(UserType);
            }
            if (LogonParametersType != null) {
                result.Add(LogonParametersType);
            }
            return result;
        }
        [Category("Behavior")]
        public override Type UserType {
            get { return userType; }
            set {
                userType = value;
                if (userType != null && !typeof(IAuthenticationStandardUser).IsAssignableFrom(userType)
                    && !typeof(IAuthenticationActiveDirectoryUser).IsAssignableFrom(userType)) {
                    throw new ArgumentException(string.Format("AuthenticationCombined does not support the {0} user type.\nA class that implements the IAuthenticationStandardUser interface should be set for the UserType property.", userType));
                }
            }
        }
        [TypeConverter(typeof(BusinessClassTypeConverter<AuthenticationCombinedLogonParameters>))]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Behavior")]
        public Type LogonParametersType {
            get { return logonParametersType; }
            set {
                logonParametersType = value;
                if (value != null) {
                    if (!typeof(AuthenticationCombinedLogonParameters).IsAssignableFrom(logonParametersType)) {
                        throw new ArgumentException("LogonParametersType");
                    }
                    logonParameters = (AuthenticationCombinedLogonParameters)ReflectionHelper.CreateObject(logonParametersType, new object[0]);
                }
            }
        }
        [Category("Behavior")]
        public bool CreateUserAutomatically {
            get { return createUserAutomatically; }
            set { createUserAutomatically = value; }
        }
        public override void SetLogonParameters(object logonParameters) {
            this.logonParameters = (AuthenticationCombinedLogonParameters)logonParameters;
        }
    }
}

