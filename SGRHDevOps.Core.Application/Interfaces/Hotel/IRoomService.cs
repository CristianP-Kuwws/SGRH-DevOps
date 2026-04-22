using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Application.Interfaces.Hotel
{
    public interface IRoomService
    {
        Task<OperationResult<RoomDto>> AddAsync(RoomDto dto);
        Task<OperationResult<RoomDto>> UpdateAsync(int id, RoomDto dto);
        Task<OperationResult<bool>> DeleteAsync(int id);
        Task<OperationResult<List<RoomDto>>> GetAllAsync();
        Task<OperationResult<RoomDto>> GetByIdAsync(int id);
    }
}
