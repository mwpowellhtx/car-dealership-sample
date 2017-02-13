namespace Powell.Vehicles
{
    using Domain;

    public class VehicleMap : DomainObjectMap<Vehicle>
    {
        public VehicleMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.ModelYear, "ModelYearId").Not.Nullable();
            References(x => x.Color, "ColorId").Not.Nullable();
            Map(x => x.Mileage).Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
        }
    }
}
