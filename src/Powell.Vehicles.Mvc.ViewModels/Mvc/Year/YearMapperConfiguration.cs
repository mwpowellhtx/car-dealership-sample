namespace Powell.Vehicles.Mvc.Year
{
    using AutoMapper;

    public class YearMapperConfiguration : MapperConfiguration, IYearMapperConfiguration
    {
        public YearMapperConfiguration()
            : base(Configure)
        {
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vehicles.Year, YearViewModel>()
                .ForMember(d => d.Year, o => o.MapFrom(s => s.Value.Year));
        }
    }
}
