using SGRHDevOps.Core.Application.Dtos.Hotel;
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
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;
        private readonly FloorDtoValidator _floorValidator;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository ?? throw new ArgumentNullException(nameof(floorRepository));
            _floorValidator = new FloorDtoValidator();
        }

        public async Task<OperationResult<List<FloorDto>>> GetAllListAsync()
        {
            try
            {
                var floors = await _floorRepository.GetAllListAsync();

                if (!floors.IsSuccess)
                    return OperationResult<List<FloorDto>>.Failure(floors.Message);

                if (floors.Data is null || !floors.Data.Any())
                    return OperationResult<List<FloorDto>>.Failure("No floors found in the system.");

                var floorsDtoList = FloorMappingProfile.MapToFloorDtoList(floors.Data);

                return OperationResult<List<FloorDto>>.Success("Floors retrieved successfully.", floorsDtoList);
            }
            catch (Exception ex)
            {
                return OperationResult<List<FloorDto>>.Failure($"An error occurred while retrieving floors: {ex.Message}");
            }
        }

        public async Task<OperationResult<FloorDto>> GetByIdAsync(int floorId)
        {
            try
            {
                if (floorId <= 0)
                    return OperationResult<FloorDto>.Failure("The floor ID must be greater than 0.");

                var floor = await _floorRepository.GetByIdAsync(floorId);

                if (!floor.IsSuccess)
                    return OperationResult<FloorDto>.Failure(floor.Message);

                if (floor.Data is null)
                    return OperationResult<FloorDto>.Failure($"Floor with ID {floorId} was not found.");

                var floorDto = FloorMappingProfile.MapToFloorDto(floor.Data);

                return OperationResult<FloorDto>.Success("Floor retrieved successfully.", floorDto);
            }
            catch (Exception ex)
            {
                return OperationResult<FloorDto>.Failure($"An error occurred while retrieving the floor: {ex.Message}");
            }
        }

        public async Task<OperationResult<FloorDto>> AddAsync(FloorDto floorDto)
        {
            try
            {
                if (floorDto is null)
                    return OperationResult<FloorDto>.Failure("Floor data cannot be null.");

                var validationResult = await _floorValidator.ValidateAsync(floorDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return OperationResult<FloorDto>.Failure($"Validation failed: {errors}");
                }

                var floor = FloorMappingProfile.MapToFloorEntity(floorDto);

                var result = await _floorRepository.AddAsync(floor);

                if (!result.IsSuccess)
                    return OperationResult<FloorDto>.Failure(result.Message);

                floorDto.FloorId = result.Data.FloorId;

                return OperationResult<FloorDto>.Success("Floor created successfully.", floorDto);
            }
            catch (Exception ex)
            {
                return OperationResult<FloorDto>.Failure($"An error occurred while creating the floor: {ex.Message}");
            }
        }

        public async Task<OperationResult<FloorDto>> UpdateAsync(int floorId, FloorDto floorDto)
        {
            try
            {
                if (floorId <= 0)
                    return OperationResult<FloorDto>.Failure("The floor ID must be greater than 0.");

                if (floorDto is null)
                    return OperationResult<FloorDto>.Failure("Floor data cannot be null.");

                var validationResult = await _floorValidator.ValidateAsync(floorDto);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return OperationResult<FloorDto>.Failure($"Validation failed: {errors}");
                }

                var floor = await _floorRepository.GetByIdAsync(floorId);

                if (!floor.IsSuccess)
                    return OperationResult<FloorDto>.Failure(floor.Message);

                if (floor.Data is null)
                    return OperationResult<FloorDto>.Failure($"Floor with ID {floorId} was not found.");

                FloorMappingProfile.UpdateFloorEntity(floorDto, floor.Data);

                var result = await _floorRepository.UpdateAsync(floor.Data.FloorId, floor.Data);

                if (!result.IsSuccess)
                    return OperationResult<FloorDto>.Failure(result.Message);

                return OperationResult<FloorDto>.Success("Floor updated successfully.", floorDto);
            }
            catch (Exception ex)
            {
                return OperationResult<FloorDto>.Failure($"An error occurred while updating the floor: {ex.Message}");
            }
        }

        public async Task<OperationResult<FloorDto>> DeleteAsync(int floorId)
        {
            try
            {
                if (floorId <= 0)
                    return OperationResult<FloorDto>.Failure("The floor ID must be greater than 0.");

                var floor = await _floorRepository.GetByIdAsync(floorId);

                if (!floor.IsSuccess)
                    return OperationResult<FloorDto>.Failure(floor.Message);

                if (floor.Data is null)
                    return OperationResult<FloorDto>.Failure($"Floor with ID {floorId} was not found.");

                var result = await _floorRepository.DeleteAsync(floor.Data.FloorId);

                if (!result.IsSuccess)
                    return OperationResult<FloorDto>.Failure(result.Message);

                return OperationResult<FloorDto>.Success("Floor deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<FloorDto>.Failure($"An error occurred while deleting the floor: {ex.Message}");
            }
        }
    }
}
