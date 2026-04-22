using SGRHDevOps.Core.Application.Dtos.ReservationModule;
using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Application.Interfaces.ReservationModule
{
    public interface IReservationService
    {
        Task<OperationResult<ReservationDto>> AddReservationAsync(CreateReservationDto createReservationDto);
        Task<OperationResult<bool>> DeleteReservationAsync(int id);
        Task<OperationResult<List<ReservationDto>>> GetAllReservationAsync();
        Task<OperationResult<ReservationDto>> GetReservationByIdAsync(int id);
        Task<OperationResult<ReservationDto>> UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto);
    }
}