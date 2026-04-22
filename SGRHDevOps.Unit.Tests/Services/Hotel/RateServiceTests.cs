using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Services.Hotel;
using SGRHDevOps.Core.Application.Validators.Hotel;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Hotel;

namespace SGRHDevOps.Unit.Tests.Services.Hotel
{
    public class RateServiceTests
    {
        private readonly DbContextOptions<SGRHContext> _dbContextOptions;
        private readonly IValidator<RateDto> _validator;

        public RateServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<SGRHContext>()
                .UseInMemoryDatabase(databaseName: $"SGRHTestDb_{Guid.NewGuid()}")
                .Options;

            _validator = new RateDtoValidator(); 
        }

        private RateService CreateService()
        {
            var context = new SGRHContext(_dbContextOptions);
            var rateRepository = new RateRepository(context);
            var service = new RateService(rateRepository, _validator);
            return service;
        }

        // AddAsync
        [Fact]
        public async Task AddAsync_Should_Return_Success_With_Added_Rate()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(dto.CategoryId, result.Data!.CategoryId);
            Assert.Equal(dto.SeasonId, result.Data.SeasonId);
            Assert.Equal(dto.NightPrice, result.Data.NightPrice);
        }

        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_Dto_Is_Null()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.AddAsync(null!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("RateDto is null.", result.Message);
        }

        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_NightPrice_Is_Zero()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 0m };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("NightPrice must be greater than zero.", result.Message);
        }

        // UpdateAsync
        [Fact]
        public async Task UpdateAsync_Should_Return_Success_With_Updated_Rate()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m };
            var added = await service.AddAsync(dto);

            var updatedDto = new RateDto { CategoryId = 2, SeasonId = 2, NightPrice = 200.00m };

            // Act
            var result = await service.UpdateAsync(added.Data!.RateId, updatedDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200.00m, result.Data!.NightPrice);
            Assert.Equal(2, result.Data.CategoryId);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Failure_When_Dto_Is_Null()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.UpdateAsync(1, null!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("RateDto is null.", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Failure_When_Rate_Not_Found()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m };

            // Act
            var result = await service.UpdateAsync(999, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Rate with id: 999 not found.", result.Message);
        }

        // DeleteAsync
        [Fact]
        public async Task DeleteAsync_Should_Return_Success_When_Rate_Exists()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m };
            var added = await service.AddAsync(dto);

            // Act
            var result = await service.DeleteAsync(added.Data!.RateId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_Failure_When_Rate_Not_Found()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.DeleteAsync(999);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Rate with id: 999 not found.", result.Message);
        }

        // GetAllAsync
        [Fact]
        public async Task GetAllAsync_Should_Return_All_Rates()
        {
            // Arrange
            var service = CreateService();
            await service.AddAsync(new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m });
            await service.AddAsync(new RateDto { CategoryId = 2, SeasonId = 2, NightPrice = 200.00m });

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);
        }

        // GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_Should_Return_Rate_When_Exists()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = 100.00m };
            var added = await service.AddAsync(dto);

            // Act
            var result = await service.GetByIdAsync(added.Data!.RateId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(added.Data.RateId, result.Data!.RateId);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Failure_When_Not_Found()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Rate with id: 999 not found.", result.Message);
        }

        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_NightPrice_Is_Negative()
        {
            // Arrange
            var service = CreateService();
            var dto = new RateDto { CategoryId = 1, SeasonId = 1, NightPrice = -50.00m };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("NightPrice must be greater than zero.", result.Message);
        }
    }
}
