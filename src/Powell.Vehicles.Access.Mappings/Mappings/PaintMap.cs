namespace Powell.Vehicles
{
    using Domain;

    public class PaintMap : DomainObjectMap<Paint>
    {
        public PaintMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.Name).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
            Map(x => x.Value).CustomSqlType("NVARCHAR").Length(7).Not.Nullable();
            HasMany(x => x.ModelYearColors);
        }
    }
}
