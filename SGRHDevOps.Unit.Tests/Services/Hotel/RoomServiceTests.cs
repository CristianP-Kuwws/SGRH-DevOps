using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Services.Hotel;
using SGRHDevOps.Core.Application.Validators.Hotel;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Hotel;

namespace SGRHDevOps.Unit.Tests.Services.Hotel
{
    public class RoomServiceTests
    {
        private readonly DbContextOptions<SGRHContext> _dbContextOptions;
        private readonly IValidator<RoomDto> _validator;

        public RoomServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<SGRHContext>()
                .UseInMemoryDatabase(databaseName: $"SGRHTestDb_{Guid.NewGuid()}")
                .Options;

            _validator = new RoomDtoValidator();
        }

        private RoomService CreateService()
        {
            var context = new SGRHContext(_dbContextOptions);
            var roomRepository = new RoomRepository(context);
            return new RoomService(roomRepository, _validator);
        }

        // AddAsync
        [Fact]
        public async Task AddAsync_Should_Return_Success_With_Added_Room()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(dto.RoomNumber, result.Data!.RoomNumber);
            Assert.Equal(dto.CategoryId, result.Data.CategoryId);
            Assert.Equal(dto.FloorId, result.Data.FloorId);
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
            Assert.Equal("RoomDto is null.", result.Message);
        }

        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_RoomNumber_Is_Empty()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "", CategoryId = 1, FloorId = 1, Status = "available" };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Room number is required.", result.Message);
        }

        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_Status_Is_Invalid()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "invalid_status" };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Status must be available, occupied or maintenance.", result.Message);
        }

        // UpdateAsync
        [Fact]
        public async Task UpdateAsync_Should_Return_Success_With_Updated_Room()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" };
            var added = await service.AddAsync(dto);

            var updatedDto = new RoomDto { RoomNumber = "202", CategoryId = 2, FloorId = 2, Status = "occupied" };

            // Act
            var result = await service.UpdateAsync(added.Data!.RoomId, updatedDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("202", result.Data!.RoomNumber);
            Assert.Equal("occupied", result.Data.Status);
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
            Assert.Equal("RoomDto is null.", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Failure_When_Room_Not_Found()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" };

            // Act
            var result = await service.UpdateAsync(999, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Room with id: 999 not found.", result.Message);
        }

        // DeleteAsync
        [Fact]
        public async Task DeleteAsync_Should_Return_Success_When_Room_Exists()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" };
            var added = await service.AddAsync(dto);

            // Act
            var result = await service.DeleteAsync(added.Data!.RoomId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_Failure_When_Room_Not_Found()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.DeleteAsync(999);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Room with id: 999 not found.", result.Message);
        }

        // GetAllAsync
        [Fact]
        public async Task GetAllAsync_Should_Return_All_Rooms()
        {
            // Arrange
            var service = CreateService();
            await service.AddAsync(new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" });
            await service.AddAsync(new RoomDto { RoomNumber = "102", CategoryId = 1, FloorId = 1, Status = "occupied" });

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);
        }

        // GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_Should_Return_Room_When_Exists()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "101", CategoryId = 1, FloorId = 1, Status = "available" };
            var added = await service.AddAsync(dto);

            // Act
            var result = await service.GetByIdAsync(added.Data!.RoomId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(added.Data.RoomId, result.Data!.RoomId);
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
            Assert.Equal("Room with id: 999 not found.", result.Message);
        }

        // Failing test
        [Fact]
        public async Task AddAsync_Should_Return_Failure_When_RoomNumber_Exceeds_MaxLength()
        {
            // Arrange
            var service = CreateService();
            var dto = new RoomDto { RoomNumber = "12345678901", CategoryId = 1, FloorId = 1, Status = "available" };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Room number must not exceed 10 characters.", result.Message);
        }
    }
}
