using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Conventions.Helpers;

namespace Powell.Data.Access.Policies
{
    using FluentNHibernate.Conventions;
    using static ConventionBuilder;

    public class AccessConventionPolicy : ConventionPolicyBase, IAccessConventionPolicy
    {
        public AccessConventionPolicy()
            : base(GetConventions().ToArray())
        {
        }

        private static IEnumerable<IConvention> GetConventions()
        {
            yield return Table.Is(x => x.EntityType.Name);

            // TODO: TBD: does not seem to be taking? having to specify Id column?
            yield return Id.Always(x => x.Column("Id"));

            yield return HasMany.Always(x => x.Cascade.AllDeleteOrphan());

            yield return HasMany.Always(x => x.LazyLoad());

            yield return HasMany.Always(x => x.Inverse());

            yield return HasMany.Always(x => x.AsBag());

            yield return Reference.Always(x => x.LazyLoad());

            yield return Reference.Always(x => x.Cascade.All());

            //// TODO: TBD: not strong enough:
            //yield return ForeignKey.EndsWith("Id");

            // TODO: TBD: does not seem to be taking? having to specify each reference column name?
            yield return ForeignKey.Format((p, t) => (p == null ? t.Name : p.Name) + "Id");

            // TODO: ditto Id Alwyas Id?
            yield return PrimaryKey.Name.Is(x => "Id");

            yield return DefaultCascade.All();

            yield return DefaultLazy.Always();
        }
    }
}
