using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Application.Interfaces.Hotel
{
    public interface IRateService
    {
        Task<OperationResult<RateDto>> AddAsync(RateDto dto);
        Task<OperationResult<RateDto>> UpdateAsync(int id, RateDto dto);
        Task<OperationResult<bool>> DeleteAsync(int id);
        Task<OperationResult<List<RateDto>>> GetAllAsync();
        Task<OperationResult<RateDto>> GetByIdAsync(int id);
    }
}
