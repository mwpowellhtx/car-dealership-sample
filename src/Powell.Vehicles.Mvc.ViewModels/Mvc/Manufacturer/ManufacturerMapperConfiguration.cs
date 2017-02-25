namespace Powell.Vehicles.Mvc.Manufacturer
{
    using AutoMapper;

    public class ManufacturerMapperConfiguration : MapperConfiguration, IManufacturerMapperConfiguration
    {
        public ManufacturerMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vehicles.Manufacturer, ManufacturerViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                ;
        }
    }
}
