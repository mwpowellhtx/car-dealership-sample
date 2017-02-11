namespace Powell.Identity.Domain
{
    using static Privilege;

    /// <summary>
    /// Permission may be actively allowed, actively denied, or unspecified, which assumes denied.
    /// Lowest common denominator of <see cref="Feature"/> should be the detailed features
    /// themselves, with summarization being possible for parent features. Any level of
    /// <see cref="User"/> may be specified.
    /// </summary>
    public class Permission : ExpiringDomainObject
    {
        /// <summary>
        /// Gets or sets the Feature.
        /// </summary>
        public virtual Feature Feature { get; set; }

        /// <summary>
        /// Gets or sets the Credential.
        /// May be either a <see cref="User"/> or a <see cref="Group"/>.
        /// </summary>
        public virtual CredentialBase Credential { get; set; }

        ///// <summary>
        ///// Gets the DefaultPrivilege.
        ///// </summary>
        ///// <see cref="NotSet"/>
        //private static Privilege DefaultPrivilege => default(Privilege);

        /// <summary>
        /// Gets or sets the Privilege.
        /// </summary>
        public virtual Privilege? Privilege { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Permission()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Feature = null;
            Credential = null;
            Privilege = Inherited;
        }
    }
}
