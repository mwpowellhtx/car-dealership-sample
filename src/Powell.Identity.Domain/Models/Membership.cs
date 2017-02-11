namespace Powell.Identity.Domain
{
    public class Membership : ExpiringDomainObject
    {
        /// <summary>
        /// Gets or sets the Group.
        /// </summary>
        public virtual Group Group { get; set; }

        /// <summary>
        /// Gets or sets the Member.
        /// </summary>
        public virtual CredentialBase Member { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Membership()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Group = null;
            Member = null;
        }
    }
}
