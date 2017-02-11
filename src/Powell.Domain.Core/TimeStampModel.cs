using System;

namespace Powell
{
    public class TimeStampModel : DomainObject, ICreatableModel
    {
        /// <summary>
        /// Gets or sets when CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets when ModifiedOn. This is a get-only property.
        /// </summary>
        public DateTime ModifiedOn
        {
            get { return DateTime.UtcNow; }
            protected internal set { }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TimeStampModel()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        private void Initialize()
        {
            this.Created();
        }
    }
}
