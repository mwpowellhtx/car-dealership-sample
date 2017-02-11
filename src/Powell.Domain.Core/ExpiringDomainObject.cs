namespace Powell
{
    public abstract class ExpiringDomainObject : DomainObject
    {
        /// <summary>
        /// Expiry backing field.
        /// </summary>
        private ExpiringTimeStampModel _expiry;

        /// <summary>
        /// Gets or sets the Expiry.
        /// </summary>
        public virtual ExpiringTimeStampModel Expiry
        {
            get { return _expiry; }
            set { _expiry = value ?? new ExpiringTimeStampModel(); }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        protected ExpiringDomainObject()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Expiry = null;
        }
    }
}
