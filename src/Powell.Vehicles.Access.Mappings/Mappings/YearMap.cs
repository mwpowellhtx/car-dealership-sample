namespace Powell.Vehicles
{
    using Domain;

    public class YearMap : DomainObjectMap<Year>
    {
        public YearMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.Value).Not.Nullable();
            HasMany(x => x.ModelYears);
        }
    }
}
