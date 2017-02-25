using System;

namespace Powell
{
    public abstract class DomainObject
    {
        public virtual Guid Id { get; set; }

        public virtual bool IsTransient => Id.Equals(Guid.Empty);

        protected DomainObject()
        {
            Initialize();
        }

        private void Initialize()
        {
            Id = Guid.Empty;
        }
    }
}
