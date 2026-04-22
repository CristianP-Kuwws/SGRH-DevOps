using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using SGRHDevOps.Core.Application.Extension.Mappers;
using SGRHDevOps.Core.Application.Extension.Validate;
using SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly RoomCategoryDtoValidator _roomCategoryValidator;

        public RoomCategoryService(IRoomCategoryRepository roomCategoryRepository)
        {
            _roomCategoryRepository = roomCategoryRepository ?? throw new ArgumentNullException(nameof(roomCategoryRepository));
            _roomCategoryValidator = new RoomCategoryDtoValidator();
        }

        public async Task<OperationResult<List<RoomCategoryDto>>> GetAllListAsync()
        {
            try
            {
                var roomCategories = await _roomCategoryRepository.GetAllListAsync();

                if (!roomCategories.IsSuccess)
                    return OperationResult<List<RoomCategoryDto>>.Failure(roomCategories.Message);

                if (roomCategories.Data is null || !roomCategories.Data.Any())
                    return OperationResult<List<RoomCategoryDto>>.Failure("No room categories found in the system.");

                var roomCategoriesDtoList = RoomCategoryMappingProfile.MapToRoomCategoryDtoList(roomCategories.Data);

                return OperationResult<List<RoomCategoryDto>>.Success("Room categories retrieved successfully.", roomCategoriesDtoList);
            }
            catch (Exception ex)
            {
                return OperationResult<List<RoomCategoryDto>>.Failure($"An error occurred while retrieving room categories: {ex.Message}");
            }
        }

        public async Task<OperationResult<RoomCategoryDto>> GetByIdAsync(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return OperationResult<RoomCategoryDto>.Failure("The category ID must be greater than 0.");

                var roomCategory = await _roomCategoryRepository.GetByIdAsync(categoryId);

                if (!roomCategory.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(roomCategory.Message);

                if (roomCategory.Data is null)
                    return OperationResult<RoomCategoryDto>.Failure($"Room category with ID {categoryId} was not found.");

                var roomCategoryDto = RoomCategoryMappingProfile.MapToRoomCategoryDto(roomCategory.Data);

                return OperationResult<RoomCategoryDto>.Success("Room category retrieved successfully.", roomCategoryDto);
            }
            catch (Exception ex)
            {
                return OperationResult<RoomCategoryDto>.Failure($"An error occurred while retrieving the room category: {ex.Message}");
            }
        }

        public async Task<OperationResult<RoomCategoryDto>> AddAsync(RoomCategoryDto roomCategoryDto)
        {
            try
            {
                if (roomCategoryDto is null)
                    return OperationResult<RoomCategoryDto>.Failure("Room category data cannot be null.");

                var validationResult = await _roomCategoryValidator.ValidateAsync(roomCategoryDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return OperationResult<RoomCategoryDto>.Failure($"Validation failed: {errors}");
                }

                var roomCategory = RoomCategoryMappingProfile.MapToRoomCategoryEntity(roomCategoryDto);

                var result = await _roomCategoryRepository.AddAsync(roomCategory);

                if (!result.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(result.Message);

                roomCategoryDto.CategoryId = result.Data.CategoryId;

                return OperationResult<RoomCategoryDto>.Success("Room category created successfully.", roomCategoryDto);
            }
            catch (Exception ex)
            {
                return OperationResult<RoomCategoryDto>.Failure($"An error occurred while creating the room category: {ex.Message}");
            }
        }

        public async Task<OperationResult<RoomCategoryDto>> UpdateAsync(int categoryId, RoomCategoryDto roomCategoryDto)
        {
            try
            {
                if (categoryId <= 0)
                    return OperationResult<RoomCategoryDto>.Failure("The category ID must be greater than 0.");

                if (roomCategoryDto is null)
                    return OperationResult<RoomCategoryDto>.Failure("Room category data cannot be null.");

                var validationResult = await _roomCategoryValidator.ValidateAsync(roomCategoryDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return OperationResult<RoomCategoryDto>.Failure($"Validation failed: {errors}");
                }

                var roomCategory = await _roomCategoryRepository.GetByIdAsync(categoryId);

                if (!roomCategory.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(roomCategory.Message);

                if (roomCategory.Data is null)
                    return OperationResult<RoomCategoryDto>.Failure($"Room category with ID {categoryId} was not found.");

                RoomCategoryMappingProfile.UpdateRoomCategoryEntity(roomCategoryDto, roomCategory.Data);

                var result = await _roomCategoryRepository.UpdateAsync(roomCategory.Data.CategoryId, roomCategory.Data);

                if (!result.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(result.Message);

                return OperationResult<RoomCategoryDto>.Success("Room category updated successfully.", roomCategoryDto);
            }
            catch (Exception ex)
            {
                return OperationResult<RoomCategoryDto>.Failure($"An error occurred while updating the room category: {ex.Message}");
            }
        }

        public async Task<OperationResult<RoomCategoryDto>> DeleteAsync(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return OperationResult<RoomCategoryDto>.Failure("The category ID must be greater than 0.");

                var roomCategory = await _roomCategoryRepository.GetByIdAsync(categoryId);

                if (!roomCategory.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(roomCategory.Message);

                if (roomCategory.Data is null)
                    return OperationResult<RoomCategoryDto>.Failure($"Room category with ID {categoryId} was not found.");

                var result = await _roomCategoryRepository.DeleteAsync(roomCategory.Data.CategoryId);

                if (!result.IsSuccess)
                    return OperationResult<RoomCategoryDto>.Failure(result.Message);

                return OperationResult<RoomCategoryDto>.Success("Room category deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<RoomCategoryDto>.Failure($"An error occurred while deleting the room category: {ex.Message}");
            }
        }
    }
}
