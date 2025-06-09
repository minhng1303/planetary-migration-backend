using AutoMapper;
using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Mappers
{
    public class FactorProfile : Profile
    {
        public FactorProfile()
        {
            CreateMap<Factor, PlanetFactor>().ReverseMap();
        }
    }
}
