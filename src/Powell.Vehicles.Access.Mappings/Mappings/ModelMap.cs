namespace Powell.Vehicles
{
    using Domain;

    public class ModelMap : DomainObjectMap<Model>
    {
        public ModelMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Make, "MakeId").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
            HasMany(x => x.ModelYears);
        }
    }
}
