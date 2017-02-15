namespace Powell.Vehicles.Mvc.Model
{
    using AutoMapper;

    public class ModelMapperConfiguration : MapperConfiguration, IModelMapperConfiguration
    {
        public ModelMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vehicles.Model, ModelResponseModel>()
                .ForMember(d => d.ManufacturerId, o => o.MapFrom(s => s.Make.Id))
                .ForMember(d => d.ManufacturerName, o => o.MapFrom(s => s.Make.Name))
                .ForMember(d => d.ModelId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ModelName, o => o.MapFrom(s => s.Name))
                ;
        }
    }
}
