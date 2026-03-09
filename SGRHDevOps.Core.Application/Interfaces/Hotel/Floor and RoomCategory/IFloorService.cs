using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory
{
    public interface IFloorService
    {
        Task<OperationResult<FloorDto>> AddAsync(FloorDto floorDto);
        Task<OperationResult<FloorDto>> UpdateAsync(int id, FloorDto floorDto);
        Task<OperationResult<FloorDto>> DeleteAsync(int id);
        Task<OperationResult<List<FloorDto>>> GetAllListAsync();
        Task<OperationResult<FloorDto>> GetByIdAsync(int id);

    }
}
