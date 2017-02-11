using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace Powell.Identity.Domain
{
    using Collections.Generic;

    //TODO: RYO or not? should worry about logging in, timing out, expiration, etc
    //TODO: may seriously consider leveraging the Microsoft Identity meta system to manage such things
    /// <summary>
    /// Represents a User Credential.
    /// </summary>
    /// <remarks>We do not care about phone numbers or confirming them. Perhaps later should we be
    /// better established with financial backing. We also do not care about Claims, per se. It
    /// does not seem as though these are quite like features. Roles is closer to features. Then
    /// there is the matter of Group Credentials.</remarks>
    public class User : CredentialBase, IUser<Guid>
    {
        #region User Members

        /// <summary>
        /// Gets or sets the <see cref="IUser{Guid}.UserName"/>.
        /// </summary>
        public virtual string UserName { get; set; }

        #endregion

        #region Borrowed from Identity Framework notions

        ////TODO: "Claims" sounds more like phishing to me; phishing for details from things like external providers; don't care about that right now
        ////
        //// Summary:
        ////     Navigation property for user claims
        //public virtual ICollection<TClaim> Claims { get; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        /// <see cref="!:http://en.wikipedia.org/wiki/Email_address" >Email address (wikipedia)</see>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// DefaultEmailAddressConfirmed: false
        /// </summary>
        private const bool DefaultEmailAddressConfirmed = false;

        /// <summary>
        /// Gets or sets whether EmailAddressConfirmed.
        /// True if the email is confirmed, default is false.
        /// </summary>
        public virtual bool IsEmailAddressConfirmed { get; set; }

        //public static int MaxAccessControlCount { private get; set; }

        //public static TimeSpan MaxLockoutDuration { private get; set; }

        //static User()
        //{
        //    MaxAccessControlCount = 5;
        //    MaxLockoutDuration = TimeSpan.FromMinutes(15d);
        //}

        /// <summary>
        /// DefaultAccessFailedCount: 0
        /// </summary>
        private const int DefaultAccessFailedCount = 0;

        /// <summary>
        /// Gets or sets AccessFailedCount used to record failures for the purposes of lockout.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// DefaultLockedOut: false
        /// </summary>
        private const bool DefaultLockedOut = false;

        private bool _lockedOut;

        /// <summary>
        /// Gets or sets whether IsLockedOut. Borrowed from
        /// <see cref="!:http://msdn.microsoft.com/en-us/library/mt151767.aspx" >LockoutEnabled</see>,
        /// which suggests that lockout is a feature set that should be enabled or disabled.
        /// However, this is more aligned with the notion that the account is actually locked out.
        /// And which is time boxed concerning <see cref="LockoutExpiryUtc"/>.
        /// </summary>
        public virtual bool IsLockedOut
        {
            get { return _lockedOut; }
            set
            {
                _lockedOut = value;
                // Only clear the expiration when clearing the lock.
                if (!_lockedOut) _lockoutExpiryUtc = null;
            }
        }

        private DateTime? _lockoutExpiryUtc;

        /// <summary>
        /// Gets or sets when the <see cref="DateTime"/> stamp when lockout expires. Any moment
        /// in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutExpiryUtc
        {
            get { return _lockoutExpiryUtc; }
            set
            {
                _lockoutExpiryUtc = value;
                // Only set whether locked out when there is a DateTime stamp.
                if (_lockoutExpiryUtc.HasValue) _lockedOut = true;
            }
        }

        ////TODO: may be appropriate to enter/leave locked out state from the domain model perspective
        //private static void CouldEnterLockedOut(ref int count, ref bool state, ref DateTime? expiry)
        //{
        //}

        //private static void DoesLeaveLockOut(ref int count, ref bool state, ref DateTime? expiry)
        //{
        //}

        /// <summary>
        /// Gets or sets the salted or hashed form of the user password. The actual salting or
        /// hashing of the password is a concern of the controller or even view model layers.
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets the SecurityStamp. This is a random value that should change whenever
        /// User Credentials have changed, such as <see cref="PasswordHash"/>, etc.
        /// </summary>
        /// <remarks>In the format
        /// "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx".</remarks>
        /// <see cref="!:https://en.wikipedia.org/wiki/Universally_unique_identifier" >Universally
        /// unique identifier (wikipedia)</see>
        /// <a href="!:http://stackoverflow.com/questions/29350167/how-to-create-a-security-stamp-value-for-asp-net-identity-iusersecuritystampsto"
        /// >How to create a Security Stamp value for ASP.NET Identity (IUserSecurityStampStore)</a>
        public virtual Guid SecurityStamp {
            //TODO: may want to re-seed the stamp on every access: leaving some of the serialization for cookie cache
            get; protected internal set; }

        /// <summary>
        /// DefaultTwoFactoryEnabled: false
        /// </summary>
        private const bool DefaultTwoFactorEnabled = false;

        /// <summary>
        /// Gets or sets whether IsTwoFactorEnabled.
        /// </summary>
        public virtual bool IsTwoFactorEnabled { get; set; }

        #endregion

        private IList<LoginInfo> _logins;

        /// <summary>
        /// Gets or sets the Logins.
        /// </summary>
        public virtual IList<LoginInfo> Logins
        {
            get { return _logins; }
            set { _logins = value ?? new List<LoginInfo>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{LoginInfo}"/> for internal use.
        /// </summary>
        internal IList<LoginInfo> InternalLogins => Logins.ToBidirectionalList(
            a => a.User = this, r => r.User = null);

        private IList<Claim> _claims;

        /// <summary>
        /// Gets or sets the Claims.
        /// </summary>
        public virtual IList<Claim> Claims
        {
            get { return _claims; }
            set { _claims = value ?? new List<Claim>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{Claim}"/> for internal use.
        /// </summary>
        internal IList<Claim> InternalClaims => Claims.ToBidirectionalList(
            a => a.User = this, r => r.User = null);

        /// <summary>
        /// Gets an <see cref="IList{Membership}"/> for internal use.
        /// </summary>
        internal override IList<Membership> InternalMemberOf => MemberOf.ToBidirectionalList(
            a =>
            {
                a.Member = this;
                var group = a.Group;
                if (group == null || group.Members.Any(a.Equals)) return;
                group.InternalMembers.Add(a);
            },
            r =>
            {
                r.Member = null;
                var group = r.Group;
                if (group == null || !group.Members.Any(r.Equals)) return;
                group.InternalMembers.Remove(r);
            });

        ///// <summary>
        ///// Profile backing field.
        ///// </summary>
        //private Profile _profile;

        ///// <summary>
        ///// Gets or sets the Profile.
        ///// </summary>
        //public virtual Profile Profile
        //{
        //    get { return _profile; }
        //    set
        //    {
        //        SetProperty(ref _profile, value ?? new Profile(), () => Profile,
        //            (removing, adding) => !ReferenceEquals(adding, removing));
        //    }
        //}

        /// <summary>
        /// Default Constructor
        /// </summary>
        public User()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            EmailAddress = null;
            IsEmailAddressConfirmed = DefaultEmailAddressConfirmed;
            AccessFailedCount = DefaultAccessFailedCount;
            IsLockedOut = DefaultLockedOut;
            LockoutExpiryUtc = null;
            PasswordHash = null;
            IsTwoFactorEnabled = DefaultTwoFactorEnabled;
            Logins = null;
            Claims = null;
            this.ReseedSecurityStamp();
            //Profile = null;
        }

        /// <summary>
        /// Tries to Verify the Password <paramref name="hashedText"/>.
        /// </summary>
        /// <param name="hashedText"></param>
        /// <returns></returns>
        public virtual bool TryVerifyPassword(string hashedText)
        {
            return string.Equals(PasswordHash, hashedText);
        }

        /// <summary>
        /// Verifies whether <see cref="Feature"/> has what access according to
        /// <see cref="Privilege"/>.
        /// </summary>
        /// <param name="privilege"></param>
        /// <param name="features"></param>
        /// <returns></returns>
        public virtual bool? Verify(Privilege privilege, params Feature[] features)
        {
            //TODO: this whole part needs to be worked out...

            //Returning Null means check my parent credential
            if (privilege.Equals(Privilege.Inherited)) return null;
            if (privilege.Equals(Privilege.NotSet)) return null;

            //TODO: return true/false?
            if (privilege.Equals(Privilege.Allow)) return false;

            return !privilege.Equals(Privilege.Deny);
        }
    }

    public static class UserExtensionMethods
    {
        internal static T ReseedSecurityStamp<T>(this T user)
            where T : User
        {
            var _ = string.Empty;

            user.SecurityStamp = Guid.NewGuid();

            return user;
        }
    }
}
