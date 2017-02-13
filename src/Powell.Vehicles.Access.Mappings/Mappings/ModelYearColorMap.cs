namespace Powell.Vehicles
{
    using Domain;

    public class ModelYearColorMap : DomainObjectMap<ModelYearColor>
    {
        public ModelYearColorMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Color, "ColorId").Not.Nullable();
            References(x => x.ModelYear, "ModelYearId").Not.Nullable();
        }
    }
}
