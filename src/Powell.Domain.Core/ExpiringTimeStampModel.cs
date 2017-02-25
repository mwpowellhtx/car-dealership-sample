using System;

namespace Powell
{
    public class ExpiringTimeStampModel : TimeStampModel
    {
        /// <summary>
        /// Gets or sets when ExpiresOn.
        /// </summary>
        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets whether IsExpired from <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public virtual bool IsExpired => HasExpired(DateTime.UtcNow);

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExpiringTimeStampModel()
        {
            Initialize();
        }

        /// <summary>
        /// Initalizes this instance.
        /// </summary>
        private void Initialize()
        {
            ExpiresOn = null;
        }

        /// <summary>
        /// Returns whether HasExpired given <see cref="when"/>.
        /// </summary>
        /// <param name="when"></param>
        /// <returns></returns>
        public virtual bool HasExpired(DateTime? when)
        {
            return ExpiresOn.HasValue && when > ExpiresOn;
        }

        /// <summary>
        /// Returns whether HasAccess <see cref="until"/> the
        /// <see cref="DateTime"/>.
        /// </summary>
        /// <param name="until"></param>
        /// <returns></returns>
        public virtual bool HasAccess(DateTime? until)
        {
            return !ExpiresOn.HasValue || (until.HasValue && ExpiresOn <= until);
        }
    }
}
