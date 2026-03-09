using Moq;
using SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;

namespace SGRHDevops.Unit.Tests
{
    public class FloorServiceTests
    {
        private readonly Mock<IFloorRepository> _floorRepositoryMock;
        private readonly FloorService _floorService;

        public FloorServiceTests()
        {
            _floorRepositoryMock = new Mock<IFloorRepository>();
            _floorService = new FloorService(_floorRepositoryMock.Object);
        }
        [Fact]
        public async Task GetAllListAsync_ShouldReturnSuccess_WhenFloorsExist()
        {
            // Arrange
            var floors = new List<Floor>
            {
                new Floor { FloorId = 1, FloorNumber = 3, Description = "Floor 1" },
                new Floor { FloorId = 2, FloorNumber = 4, Description = "Floor 2" }
            };
            _floorRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<Floor>>.Success("Floors retrieved successfully.", floors));
            // Act
            var result = await _floorService.GetAllListAsync();
            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllListAsync_ShouldReturnFailure_WhenNoFloorsExist()
        {
            // Arrange
            _floorRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<Floor>>.Success("No floors found in the system.", new List<Floor>()));
            // Act
            var result = await _floorService.GetAllListAsync();
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No floors found in the system.", result.Message);
        }

        [Fact]
        public async Task GetAllListAsync_ShouldReturnFailure_WhenRepositoryFails()
        {
            // Arrange
            _floorRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<Floor>>.Failure("Database error"));
            // Act
            var result = await _floorService.GetAllListAsync();
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database error", result.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccess_WhenFloorExists()
        {
            // Arrange
            var floor = new Floor { FloorId = 1, FloorNumber = 3, Description = "Floor 1" };
            _floorRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(OperationResult<Floor>.Success("Floor retrieved successfully.", floor));
            // Act
            var result = await _floorService.GetByIdAsync(1);
            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFailure_WhenFloorDoesNotExist()
        {
            // Arrange
            _floorRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(OperationResult<Floor>.Failure("Floor not found"));
            // Act
            var result = await _floorService.GetByIdAsync(1);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Floor not found", result.Message);

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFailure_WhenRepositoryFails()
        {
            // Arrange
            _floorRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(OperationResult<Floor>.Failure("Database error"));
            // Act
            var result = await _floorService.GetByIdAsync(1);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database error", result.Message);
        }

        }
    }
