using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Extension.Mappers
{
    public class RoomCategoryMappingProfile
    { 
        public static RoomCategoryDto MapToRoomCategoryDto(RoomCategory roomCategory)
        {
            if (roomCategory is null)
                throw new ArgumentNullException(nameof(roomCategory), "RoomCategory entity cannot be null.");

            return new RoomCategoryDto
            {
                CategoryId = roomCategory.CategoryId,
                Name = roomCategory.Name,
                Description = roomCategory.Description,
                MaxCapacity = roomCategory.MaxCapacity,
                Amenities = roomCategory.Amenities
            };
        }
         
        public static List<RoomCategoryDto> MapToRoomCategoryDtoList(List<RoomCategory> roomCategories)
        {
            if (roomCategories is null)
                throw new ArgumentNullException(nameof(roomCategories), "RoomCategories list cannot be null.");

            return roomCategories.Select(MapToRoomCategoryDto).ToList();
        }
         
        public static RoomCategory MapToRoomCategoryEntity(RoomCategoryDto roomCategoryDto)
        {
            if (roomCategoryDto is null)
                throw new ArgumentNullException(nameof(roomCategoryDto), "RoomCategoryDto cannot be null.");

            return new RoomCategory
            {
                CategoryId = roomCategoryDto.CategoryId,
                Name = roomCategoryDto.Name,
                Description = roomCategoryDto.Description,
                MaxCapacity = roomCategoryDto.MaxCapacity,
                Amenities = roomCategoryDto.Amenities
            };
        }
         
        public static RoomCategory UpdateRoomCategoryEntity(RoomCategoryDto roomCategoryDto, RoomCategory roomCategory)
        {
            if (roomCategoryDto is null)
                throw new ArgumentNullException(nameof(roomCategoryDto), "RoomCategoryDto cannot be null.");

            if (roomCategory is null)
                throw new ArgumentNullException(nameof(roomCategory), "RoomCategory entity cannot be null.");

            roomCategory.Name = roomCategoryDto.Name;
            roomCategory.Description = roomCategoryDto.Description;
            roomCategory.MaxCapacity = roomCategoryDto.MaxCapacity;
            roomCategory.Amenities = roomCategoryDto.Amenities;

            return roomCategory;
        } 
        public static List<RoomCategoryDto> MapToRoomCategoryDtoListSafe(List<RoomCategory>? roomCategories)
        {
            if (roomCategories is null || roomCategories.Count == 0)
                return new List<RoomCategoryDto>();

            return MapToRoomCategoryDtoList(roomCategories);
        }
        public static RoomCategoryDto? MapToRoomCategoryDtoSafe(RoomCategory? roomCategory)
        {
            if (roomCategory is null)
                return null;

            return MapToRoomCategoryDto(roomCategory);
        }
    }
}
