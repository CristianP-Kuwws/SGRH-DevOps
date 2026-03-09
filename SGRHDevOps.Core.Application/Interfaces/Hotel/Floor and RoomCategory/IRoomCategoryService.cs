using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using SGRHDevOps.Core.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory
{
    public interface IRoomCategoryService
    {
        Task<OperationResult<RoomCategoryDto>> AddAsync(RoomCategoryDto roomCategoryDto);
        Task<OperationResult<RoomCategoryDto>> UpdateAsync(int id, RoomCategoryDto roomCategoryDto);
        Task<OperationResult<RoomCategoryDto>> DeleteAsync(int id);
        Task<OperationResult<List<RoomCategoryDto>>> GetAllListAsync();
        Task<OperationResult<RoomCategoryDto>> GetByIdAsync(int id);

    }
}
