using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Extension.Mappers
{
    public class FloorMappingProfile
    { 
        public static FloorDto MapToFloorDto(Floor floor)
        {
            if (floor is null)
                throw new ArgumentNullException(nameof(floor), "Floor entity cannot be null.");

            return new FloorDto
            {
                FloorId = floor.FloorId,
                FloorNumber = floor.FloorNumber,
                Description = floor.Description,
                Status = floor.Status
            };
        } 
        public static List<FloorDto> MapToFloorDtoList(List<Floor> floors)
        {
            if (floors is null)
                throw new ArgumentNullException(nameof(floors), "Floors list cannot be null.");

            return floors.Select(MapToFloorDto).ToList();
        } 
        public static Floor MapToFloorEntity(FloorDto floorDto)
        {
            if (floorDto is null)
                throw new ArgumentNullException(nameof(floorDto), "FloorDto cannot be null.");

            return new Floor
            {
                FloorId = floorDto.FloorId,
                FloorNumber = floorDto.FloorNumber,
                Description = floorDto.Description,
                Status = floorDto.Status ?? "active"
            };
        } 
        public static Floor UpdateFloorEntity(FloorDto floorDto, Floor floor)
        {
            if (floorDto is null)
                throw new ArgumentNullException(nameof(floorDto), "FloorDto cannot be null.");

            if (floor is null)
                throw new ArgumentNullException(nameof(floor), "Floor entity cannot be null.");

            floor.FloorNumber = floorDto.FloorNumber;
            floor.Description = floorDto.Description;
            floor.Status = floorDto.Status ?? floor.Status;

            return floor;
        }
         
        public static List<FloorDto> MapToFloorDtoListSafe(List<Floor>? floors)
        {
            if (floors is null || floors.Count == 0)
                return new List<FloorDto>();

            return MapToFloorDtoList(floors);
        } 
        public static FloorDto? MapToFloorDtoSafe(Floor? floor)
        {
            if (floor is null)
                return null;

            return MapToFloorDto(floor);
        }
    }
}
 