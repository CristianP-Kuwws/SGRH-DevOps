using SGRHDevOps.Core.Application.Dtos.ReservationModule;
using SGRHDevOps.Core.Application.Interfaces.ReservationModule;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.ReservationModule;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;

namespace SGRHDevOps.Core.Application.Services.ReservationModule
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<OperationResult<List<ReservationDto>>> GetAllReservationAsync()
        {
            var result = await _reservationRepository.GetAllListAsync();

            if (!result.IsSuccess)
                return OperationResult<List<ReservationDto>>.Failure(result.Message);

            if (result.Data is null)
                return OperationResult<List<ReservationDto>>.Failure("No Reservations found.");

            var reservationsDtoList = result.Data.Select(r => new ReservationDto
            {
                ReservationId = r.ReservationId,
                ClientId = r.ClientId,
                EndDate = r.EndDate,
                ReservationDate = r.ReservationDate,
                StartDate = r.StartDate,
                GuestCount = r.GuestCount,
                PaymentAmount = r.PaymentAmount,
                RoomId = r.RoomId,
                Status = r.Status,
            }).ToList();

            return OperationResult<List<ReservationDto>>.Success(result.Message, reservationsDtoList);
        }

        public async Task<OperationResult<ReservationDto>> GetReservationByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult<ReservationDto>.Failure("Invalid ID");

            var result = await _reservationRepository.GetByIdAsync(id);

            if (!result.IsSuccess)
                return OperationResult<ReservationDto>.Failure(result.Message);

            if (result.Data is null)
                return OperationResult<ReservationDto>.Failure("No Reservations found.");

            ReservationDto reservationDto = new()
            {
                ReservationId = result.Data.ReservationId,
                ClientId = result.Data.ClientId,
                EndDate = result.Data.EndDate,
                ReservationDate = result.Data.ReservationDate,
                StartDate = result.Data.StartDate,
                GuestCount = result.Data.GuestCount,
                PaymentAmount = result.Data.PaymentAmount,
                RoomId = result.Data.RoomId,
                Status = result.Data.Status
            };


            return OperationResult<ReservationDto>.Success(result.Message, reservationDto);
        }

        public async Task<OperationResult<ReservationDto>> AddReservationAsync(CreateReservationDto createReservationDto)
        {
            if (createReservationDto == null)
                return OperationResult<ReservationDto>.Failure("Reservation data cannot be null.");

            if (createReservationDto.ClientId <= 0)
                return OperationResult<ReservationDto>.Failure("ClientId must be greater than 0.");

            if (createReservationDto.RoomId <= 0)
                return OperationResult<ReservationDto>.Failure("RoomId must be greater than 0.");

            if (createReservationDto.GuestCount <= 0)
                return OperationResult<ReservationDto>.Failure("GuestCount must be greater than 0.");

            if (createReservationDto.PaymentAmount < 0)
                return OperationResult<ReservationDto>.Failure("PaymentAmount cannot be negative.");

            if (createReservationDto.StartDate == default)
                return OperationResult<ReservationDto>.Failure("StartDate is required.");

            if (createReservationDto.EndDate == default)
                return OperationResult<ReservationDto>.Failure("EndDate is required.");

            if (createReservationDto.ReservationDate == default)
                return OperationResult<ReservationDto>.Failure("ReservationDate is required.");

            if (createReservationDto.StartDate >= createReservationDto.EndDate)
                return OperationResult<ReservationDto>.Failure("StartDate must be earlier than EndDate.");

            if (createReservationDto.ReservationDate > createReservationDto.StartDate)
                return OperationResult<ReservationDto>.Failure("ReservationDate cannot be after StartDate.");

            Reservation entity = new()
            {
                ClientId = createReservationDto.ClientId,
                EndDate = createReservationDto.EndDate,
                ReservationDate = createReservationDto.ReservationDate,
                StartDate = createReservationDto.StartDate,
                GuestCount = createReservationDto.GuestCount,
                PaymentAmount = createReservationDto.PaymentAmount,
                RoomId = createReservationDto.RoomId,
            };

            var result = await _reservationRepository.AddAsync(entity);

            if (!result.IsSuccess)
                return OperationResult<ReservationDto>.Failure(result.Message);

            if (result.Data is null)
                return OperationResult<ReservationDto>.Failure("Error creating reservation.");

            ReservationDto reservationDto = new()
            {
                ReservationId = result.Data.ReservationId,
                ClientId = result.Data.ClientId,
                EndDate = result.Data.EndDate,
                ReservationDate = result.Data.ReservationDate,
                StartDate = result.Data.StartDate,
                GuestCount = result.Data.GuestCount,
                PaymentAmount = result.Data.PaymentAmount,
                RoomId = result.Data.RoomId,
                Status = result.Data.Status
            };

            return OperationResult<ReservationDto>.Success(result.Message, reservationDto);
        }

        public async Task<OperationResult<ReservationDto>> UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto)
        {
            if (updateReservationDto == null)
                return OperationResult<ReservationDto>.Failure("Reservation data cannot be null.");

            if (updateReservationDto.ReservationId <= 0)
                return OperationResult<ReservationDto>.Failure("ReservationId must be greater than 0.");

            if (updateReservationDto.ClientId <= 0)
                return OperationResult<ReservationDto>.Failure("ClientId must be greater than 0.");

            if (updateReservationDto.RoomId <= 0)
                return OperationResult<ReservationDto>.Failure("RoomId must be greater than 0.");

            if (updateReservationDto.GuestCount <= 0)
                return OperationResult<ReservationDto>.Failure("GuestCount must be greater than 0.");

            if (updateReservationDto.PaymentAmount < 0)
                return OperationResult<ReservationDto>.Failure("PaymentAmount cannot be negative.");

            if (updateReservationDto.StartDate == default)
                return OperationResult<ReservationDto>.Failure("StartDate is required.");

            if (updateReservationDto.EndDate == default)
                return OperationResult<ReservationDto>.Failure("EndDate is required.");

            if (updateReservationDto.ReservationDate == default)
                return OperationResult<ReservationDto>.Failure("ReservationDate is required.");

            if (updateReservationDto.StartDate >= updateReservationDto.EndDate)
                return OperationResult<ReservationDto>.Failure("StartDate must be earlier than EndDate.");

            if (updateReservationDto.ReservationDate > updateReservationDto.StartDate)
                return OperationResult<ReservationDto>.Failure("ReservationDate cannot be after StartDate.");

            var existingReservation = await _reservationRepository.GetByIdAsync(id);

            if (!existingReservation.IsSuccess)
                return OperationResult<ReservationDto>.Failure(existingReservation.Message);

            if (existingReservation.Data is null)
                return OperationResult<ReservationDto>.Failure("Reservation not found.");

            var entity = existingReservation.Data;

            entity.ClientId = updateReservationDto.ClientId;
            entity.RoomId = updateReservationDto.RoomId;
            entity.StartDate = updateReservationDto.StartDate;
            entity.EndDate = updateReservationDto.EndDate;
            entity.ReservationDate = updateReservationDto.ReservationDate;
            entity.GuestCount = updateReservationDto.GuestCount;
            entity.PaymentAmount = updateReservationDto.PaymentAmount;

            if (!string.IsNullOrWhiteSpace(updateReservationDto.Status))
                entity.Status = updateReservationDto.Status;

            var result = await _reservationRepository.UpdateAsync(id, entity);

            if (!result.IsSuccess)
                return OperationResult<ReservationDto>.Failure(result.Message);

            if (result.Data is null)
                return OperationResult<ReservationDto>.Failure("Error updating reservation.");

            ReservationDto reservationDto = new()
            {
                ReservationId = result.Data.ReservationId,
                ClientId = result.Data.ClientId,
                RoomId = result.Data.RoomId,
                StartDate = result.Data.StartDate,
                EndDate = result.Data.EndDate,
                ReservationDate = result.Data.ReservationDate,
                GuestCount = result.Data.GuestCount,
                PaymentAmount = result.Data.PaymentAmount,
                Status = result.Data.Status
            };

            return OperationResult<ReservationDto>.Success(result.Message, reservationDto);
        }

        public async Task<OperationResult<bool>> DeleteReservationAsync(int id)
        {
            if (id <= 0)
                return OperationResult<bool>.Failure("ReservationId must be greater than 0.");

            var result = await _reservationRepository.DeleteAsync(id);

            if (!result.IsSuccess)
                return OperationResult<bool>.Failure(result.Message);

            return OperationResult<bool>.Success("Reservation deleted successfully.", true);
        }
    }
}
