namespace Powell.Vehicles.Mvc.Paint
{
    using AutoMapper;

    public class PaintMapperConfiguration : MapperConfiguration, IPaintMapperConfiguration
    {
        public PaintMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vehicles.Paint, PaintResponseModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
                ;
        }
    }
}
