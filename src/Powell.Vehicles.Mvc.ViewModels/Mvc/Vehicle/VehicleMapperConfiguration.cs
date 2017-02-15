namespace Powell.Vehicles.Mvc.Vehicle
{
    using AutoMapper;

    public class VehicleMapperConfiguration : MapperConfiguration, IVehicleMapperConfiguration
    {
        public VehicleMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vehicles.Vehicle, VehicleViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ManufacturerId, o => o.MapFrom(s => s.ModelYear.Model.Make.Id))
                .ForMember(d => d.ManufacturerName, o => o.MapFrom(s => s.ModelYear.Model.Make.Name))
                .ForMember(d => d.ModelId, o => o.MapFrom(s => s.ModelYear.Model.Id))
                .ForMember(d => d.ModelName, o => o.MapFrom(s => s.ModelYear.Model.Name))
                .ForMember(d => d.Year, o => o.MapFrom(s => s.ModelYear.Year.Value.Year))
                .ForMember(d => d.ColorId, o => o.MapFrom(s => s.Color.Id))
                .ForMember(d => d.ColorName, o => o.MapFrom(s => s.Color.Name))
                .ForMember(d => d.ColorValue, o => o.MapFrom(s => s.Color.Value))
                .ForMember(d => d.Mileage, o => o.MapFrom(s => s.Mileage))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                ;

            cfg.CreateMap<AddVehicleRequestModel, Vehicles.Vehicle>()
                .ForMember(d => d.Mileage, o => o.MapFrom(s => s.Mileage))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                ;
        }
    }
}
