namespace Powell.Identity.Domain
{
    using Microsoft.AspNet.Identity;

    public class LoginInfo : DomainObject
    {
        /// <summary>
        /// Gets or sets the User who owns this login information.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the login provider for the login (i.e. facebook, google).
        /// </summary>
        public virtual string Provider { get; set; }

        /// <summary>
        /// Gets or sets the Key representing the login for the provider.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        public LoginInfo()
        {
            Initialize();
        }

        private void Initialize()
        {
            User = null;
            Provider = null;
            ProviderKey = null;
        }

        public static implicit operator UserLoginInfo(LoginInfo loginInfo)
        {
            return new UserLoginInfo(loginInfo.Provider, loginInfo.ProviderKey);
        }

        public static implicit operator LoginInfo(UserLoginInfo login)
        {
            return new LoginInfo {Provider = login.LoginProvider, ProviderKey = login.ProviderKey};
        }
    }
}
