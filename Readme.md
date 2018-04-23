# How to: Allow a user to select the authentication type at runtime


<p>To accomplish this task, you should create a new authentication type, inherited from the AuthenticationBase, and combine the code of the AuthenticationStandard and AuthenticationActiveDirectory classes in it. Additionally, you should create a custom logon parameters class with a property, allowing users to select the authentication type.<br><br><strong>See Also:</strong><br><a href="https://www.devexpress.com/Support/Center/p/Q478325">How to enable the ResetPasswordController and ChangePasswordController Actions (Reset and Change Password) when ActiveDirectoryAuthentication is used</a></p>

<br/>


