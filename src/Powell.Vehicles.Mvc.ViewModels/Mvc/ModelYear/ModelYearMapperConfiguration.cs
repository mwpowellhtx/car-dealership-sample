namespace Powell.Vehicles.Mvc.ModelYear
{
    using AutoMapper;

    public class ModelYearMapperConfiguration : MapperConfiguration, IModelYearMapperConfiguration
    {
        public ModelYearMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ModelYearColor, ModelYearViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ManufacturerId, o => o.MapFrom(s => s.ModelYear.Model.Make.Id))
                .ForMember(d => d.ManufacturerName, o => o.MapFrom(s => s.ModelYear.Model.Make.Name))
                .ForMember(d => d.ModelId, o => o.MapFrom(s => s.ModelYear.Model.Id))
                .ForMember(d => d.ModelName, o => o.MapFrom(s => s.ModelYear.Model.Name))
                .ForMember(d => d.Year, o => o.MapFrom(s => s.ModelYear.Year.Value.Year))
                .ForMember(d => d.ColorId, o => o.MapFrom(s => s.Color.Id))
                .ForMember(d => d.ColorName, o => o.MapFrom(s => s.Color.Name))
                .ForMember(d => d.ColorValue, o => o.MapFrom(s => s.Color.Value))
                ;

            // Even though it looks the same on its face, potentially it could be very different.
            cfg.CreateMap<ModelYearColor, ModelYearResponseModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ManufacturerId, o => o.MapFrom(s => s.ModelYear.Model.Make.Id))
                .ForMember(d => d.ManufacturerName, o => o.MapFrom(s => s.ModelYear.Model.Make.Name))
                .ForMember(d => d.ModelId, o => o.MapFrom(s => s.ModelYear.Model.Id))
                .ForMember(d => d.ModelName, o => o.MapFrom(s => s.ModelYear.Model.Name))
                .ForMember(d => d.Year, o => o.MapFrom(s => s.ModelYear.Year.Value.Year))
                .ForMember(d => d.ColorId, o => o.MapFrom(s => s.Color.Id))
                .ForMember(d => d.ColorName, o => o.MapFrom(s => s.Color.Name))
                .ForMember(d => d.ColorValue, o => o.MapFrom(s => s.Color.Value))
                // No need to map Summary; this is automatically calculated.
                ;

        }
    }
}
