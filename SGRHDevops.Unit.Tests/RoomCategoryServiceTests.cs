using Moq;
using SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.Hotel;
using SGRHDevOps.Core.Domain.Interfaces.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevops.Unit.Tests
{
    public class RoomCategoryServiceTests
    {
        private readonly Mock<IRoomCategoryRepository> _roomCategoryRepositoryMock;
        private readonly RoomCategoryService _roomCategoryService;

        public RoomCategoryServiceTests()
        {
            _roomCategoryRepositoryMock = new Mock<IRoomCategoryRepository>();
            _roomCategoryService = new RoomCategoryService(_roomCategoryRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllListAsync_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            _roomCategoryRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<RoomCategory>>.Failure("Database error"));
            // Act
            var result = await _roomCategoryService.GetAllListAsync();
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database error", result.Message);
        }

        [Fact]
        public async Task GetAllListAsync_ShouldReturnFailure_WhenRepositoryReturnsEmptyList()
        {
            // Arrange
            _roomCategoryRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<RoomCategory>>.Success("No data", new List<RoomCategory>()));
            // Act
            var result = await _roomCategoryService.GetAllListAsync();
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No room categories found in the system.", result.Message);
        }

        [Fact]
        public async Task GetAllListAsync_ShouldReturnSuccess_WhenRepositoryReturnsData()
        {
            // Arrange
            var roomCategories = new List<RoomCategory>
            {
                new RoomCategory { CategoryId = 1, Name = "Standard" },
                new RoomCategory { CategoryId = 2, Name = "Deluxe" }
            };
            _roomCategoryRepositoryMock.Setup(repo => repo.GetAllListAsync())
                .ReturnsAsync(OperationResult<List<RoomCategory>>.Success("Data retrieved", roomCategories));
            // Act
            var result = await _roomCategoryService.GetAllListAsync();
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Room categories retrieved successfully.", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFailure_WhenIdIsInvalid()
        {
            // Act
            var result = await _roomCategoryService.GetByIdAsync(0);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The category ID must be greater than 0.", result.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
            // Arrange
            _roomCategoryRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(OperationResult<RoomCategory>.Failure("Database error"));
            // Act
            var result = await _roomCategoryService.GetByIdAsync(1);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database error", result.Message);
        }

    }
}
