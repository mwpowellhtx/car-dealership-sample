using FluentNHibernate.Mapping;

namespace Powell.Domain
{
    public abstract class TimeStampModelComponentMapBase<T> : ComponentMap<T>
        where T : TimeStampModel
    {
        protected TimeStampModelComponentMapBase()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.CreatedOn);
            Map(x => x.ModifiedOn);
        }
    }
}
