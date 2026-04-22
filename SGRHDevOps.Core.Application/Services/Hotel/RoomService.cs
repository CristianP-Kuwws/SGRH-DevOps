using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Interfaces.Hotel;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;

namespace SGRHDevOps.Core.Application.Services.Hotel
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomDto> _validator;

        public RoomService(IRoomRepository roomRepository, IValidator<RoomDto> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task<OperationResult<RoomDto>> AddAsync(RoomDto dto)
        {
            if (dto == null)
                return OperationResult<RoomDto>.Failure("RoomDto is null.");

            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return OperationResult<RoomDto>.Failure(validation.Errors.First().ErrorMessage);

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                CategoryId = dto.CategoryId,
                FloorId = dto.FloorId,
                Description = dto.Description,
                RoomImgUrl = dto.RoomImgUrl,
                Status = dto.Status
            };

            var result = await _roomRepository.AddAsync(room);

            if (!result.IsSuccess)
                return OperationResult<RoomDto>.Failure(result.Message);

            dto.RoomId = result.Data!.RoomId;
            return OperationResult<RoomDto>.Success("Room added successfully.", dto);
        }

        public async Task<OperationResult<RoomDto>> UpdateAsync(int id, RoomDto dto)
        {
            if (dto == null)
                return OperationResult<RoomDto>.Failure("RoomDto is null.");

            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return OperationResult<RoomDto>.Failure(validation.Errors.First().ErrorMessage);

            var room = new Room
            {
                RoomId = id,
                RoomNumber = dto.RoomNumber,
                CategoryId = dto.CategoryId,
                FloorId = dto.FloorId,
                Description = dto.Description,
                RoomImgUrl = dto.RoomImgUrl,
                Status = dto.Status
            };

            var result = await _roomRepository.UpdateAsync(id, room);

            if (!result.IsSuccess)
                return OperationResult<RoomDto>.Failure(result.Message);

            dto.RoomId = id;
            return OperationResult<RoomDto>.Success("Room updated successfully.", dto);
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var result = await _roomRepository.DeleteAsync(id);

            if (!result.IsSuccess)
                return OperationResult<bool>.Failure(result.Message);

            return OperationResult<bool>.Success("Room deleted successfully.", true);
        }

        public async Task<OperationResult<List<RoomDto>>> GetAllAsync()
        {
            var result = await _roomRepository.GetAllListAsync();

            if (!result.IsSuccess)
                return OperationResult<List<RoomDto>>.Failure(result.Message);

            var dtos = result.Data!.Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                RoomNumber = r.RoomNumber,
                CategoryId = r.CategoryId,
                FloorId = r.FloorId,
                Description = r.Description,
                RoomImgUrl = r.RoomImgUrl,
                Status = r.Status
            }).ToList();

            return OperationResult<List<RoomDto>>.Success("Rooms retrieved successfully.", dtos);
        }

        public async Task<OperationResult<RoomDto>> GetByIdAsync(int id)
        {
            var result = await _roomRepository.GetByIdAsync(id);

            if (!result.IsSuccess)
                return OperationResult<RoomDto>.Failure(result.Message);

            var dto = new RoomDto
            {
                RoomId = result.Data!.RoomId,
                RoomNumber = result.Data.RoomNumber,
                CategoryId = result.Data.CategoryId,
                FloorId = result.Data.FloorId,
                Description = result.Data.Description,
                RoomImgUrl = result.Data.RoomImgUrl,
                Status = result.Data.Status
            };

            return OperationResult<RoomDto>.Success("Room retrieved successfully.", dto);
        }
    }
}
