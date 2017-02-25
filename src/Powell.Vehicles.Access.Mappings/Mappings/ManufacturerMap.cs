namespace Powell.Vehicles
{
    using Domain;

    public class ManufacturerMap : DomainObjectMap<Manufacturer>
    {
        public ManufacturerMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.Name).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
            HasMany(x => x.Models);
        }
    }
}
