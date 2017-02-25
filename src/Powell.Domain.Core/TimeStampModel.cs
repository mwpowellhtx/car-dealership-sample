using System;

namespace Powell
{
    public class TimeStampModel : ICreatableModel
    {
        /// <summary>
        /// Gets or sets when CreatedOn.
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets when ModifiedOn. This is a get-only property.
        /// </summary>
        public virtual DateTime ModifiedOn
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
