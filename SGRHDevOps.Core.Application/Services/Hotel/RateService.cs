using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Interfaces.Hotel;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;

namespace SGRHDevOps.Core.Application.Services.Hotel
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;
        private readonly IValidator<RateDto> _validator;

        public RateService(IRateRepository rateRepository, IValidator<RateDto> validator)
        {
            _rateRepository = rateRepository;
            _validator = validator;
        }

        public async Task<OperationResult<RateDto>> AddAsync(RateDto dto)
        {
            if (dto == null)
                return OperationResult<RateDto>.Failure("RateDto is null.");

            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return OperationResult<RateDto>.Failure(validation.Errors.First().ErrorMessage);

            var rate = new Rate
            {
                CategoryId = dto.CategoryId,
                SeasonId = dto.SeasonId,
                NightPrice = dto.NightPrice
            };

            var result = await _rateRepository.AddAsync(rate);

            if (!result.IsSuccess)
                return OperationResult<RateDto>.Failure(result.Message);

            dto.RateId = result.Data!.RateId;
            return OperationResult<RateDto>.Success("Rate added successfully.", dto);
        }

        public async Task<OperationResult<RateDto>> UpdateAsync(int id, RateDto dto)
        {
            if (dto == null)
                return OperationResult<RateDto>.Failure("RateDto is null.");

            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return OperationResult<RateDto>.Failure(validation.Errors.First().ErrorMessage);

            var rate = new Rate
            {
                RateId = id,
                CategoryId = dto.CategoryId,
                SeasonId = dto.SeasonId,
                NightPrice = dto.NightPrice
            };

            var result = await _rateRepository.UpdateAsync(id, rate);

            if (!result.IsSuccess)
                return OperationResult<RateDto>.Failure(result.Message);

            dto.RateId = id;
            return OperationResult<RateDto>.Success("Rate updated successfully.", dto);
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var result = await _rateRepository.DeleteAsync(id);

            if (!result.IsSuccess)
                return OperationResult<bool>.Failure(result.Message);

            return OperationResult<bool>.Success("Rate deleted successfully.", true);
        }

        public async Task<OperationResult<List<RateDto>>> GetAllAsync()
        {
            var result = await _rateRepository.GetAllListAsync();

            if (!result.IsSuccess)
                return OperationResult<List<RateDto>>.Failure(result.Message);

            var dtos = result.Data!.Select(r => new RateDto
            {
                RateId = r.RateId,
                CategoryId = r.CategoryId,
                SeasonId = r.SeasonId,
                NightPrice = r.NightPrice
            }).ToList();

            return OperationResult<List<RateDto>>.Success("Rates retrieved successfully.", dtos);
        }

        public async Task<OperationResult<RateDto>> GetByIdAsync(int id)
        {
            var result = await _rateRepository.GetByIdAsync(id);

            if (!result.IsSuccess)
                return OperationResult<RateDto>.Failure(result.Message);

            var dto = new RateDto
            {
                RateId = result.Data!.RateId,
                CategoryId = result.Data.CategoryId,
                SeasonId = result.Data.SeasonId,
                NightPrice = result.Data.NightPrice
            };

            return OperationResult<RateDto>.Success("Rate retrieved successfully.", dto);
        }
    }
}
