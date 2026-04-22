using SGRHDevOps.Core.Application.Dtos.ReservationModule;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;

namespace SGRHDevOps.Core.Application.Services.ReservationModule
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<OperationResult<List<ReservationDto>>> GetAllReservation()
        {
            var reservations = await _reservationRepository.GetAllListAsync();

            if (!reservations.IsSuccess)
                return OperationResult<List<ReservationDto>>.Failure(reservations.Message);

            if (reservations.Data is null)
                return OperationResult<List<ReservationDto>>.Failure("No Reservations found.");

            var reservationsDtoList = reservations.Data.Select(r => new ReservationDto
            {
                ReservationId = r.ReservationId,
                ClientId = r.ClientId,
                EndDate = r.EndDate,
                ReservationDate = r.ReservationDate,
                StartDate = r.StartDate,
                GuestCount = r.GuestCount,
                PaymentAmount = r.PaymentAmount,
                RoomId = r.RoomId,
                Status = r.Status
            }).ToList();

            return OperationResult<List<ReservationDto>>.Success(reservations.Message, reservationsDtoList);
        }
    }
}
