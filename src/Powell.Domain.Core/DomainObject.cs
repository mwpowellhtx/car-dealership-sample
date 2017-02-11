using System;

namespace Powell
{
    public abstract class DomainObject
    {
        public Guid Id { get; set; }

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
