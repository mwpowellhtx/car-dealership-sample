using System.Collections;
using System.Collections.Generic;

namespace Powell.Data
{
    using FluentNHibernate.Conventions;

    public abstract class ConventionPolicyBase : IConventionPolicy
    {
        private IEnumerable<IConvention> Conventions { get; }

        protected ConventionPolicyBase(params IConvention[] conventions)
        {
            Conventions = conventions;
        }

        public IEnumerator<IConvention> GetEnumerator()
        {
            return Conventions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
