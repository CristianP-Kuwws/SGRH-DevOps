using AutoMapper;
using SGRHDevOps.Core.Application.Dtos.ReservationModule;
using SGRHDevOps.Core.Domain.Entities.ReservationModule;

namespace SGRHDevOps.Core.Application.Mappings
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();

            CreateMap<Reservation, CreateReservationDto>().ReverseMap()
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore());

            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
        }
    }
}
