namespace Powell.Vehicles
{
    using Domain;

    public class ModelYearMap : DomainObjectMap<ModelYear>
    {
        public ModelYearMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Year, "YearId").Not.Nullable();
            References(x => x.Model, "ModelId").Not.Nullable();
            Map(x => x.ProducedOn).Not.Nullable();
            HasMany(x => x.Colors);
            HasMany(x => x.Vehicles);
        }
    }
}
