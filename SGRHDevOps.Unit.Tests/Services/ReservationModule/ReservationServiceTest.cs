using FluentAssertions;
using Moq;
using SGRHDevOps.Core.Application.Dtos.ReservationModule;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.ReservationModule;
using SGRHDevOps.Core.Domain.Interfaces.ReservationModule;

namespace SGRHDevOps.Unit.Tests.Services.ReservationModule
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepository;
        private readonly Core.Application.Services.ReservationModule.ReservationService _service;

        public ReservationServiceTest()
        {
            _reservationRepository = new Mock<IReservationRepository>();
            _service = new Core.Application.Services.ReservationModule.ReservationService(_reservationRepository.Object);
        }


        private Reservation CreateReservation(
           int reservationId = 1,
           int clientId = 1,
           int createdBy = 1,
           DateTime? createdAt = null,
           DateTime? reservationDate = null,
           DateTime? startDate = null,
           DateTime? endDate = null,
           int guestCount = 3,
           decimal paymentAmount = 100,
           int roomId = 1
        )
        {
            return new Reservation
            {
                ReservationId = reservationId,
                ClientId = clientId,
                CreatedBy = createdBy,
                CreatedAt = createdAt ?? DateTime.Now,
                ReservationDate = reservationDate ?? DateTime.Now,
                StartDate = startDate ?? DateTime.Now.AddDays(1),
                EndDate = endDate ?? DateTime.Now.AddDays(14),
                GuestCount = guestCount,
                PaymentAmount = paymentAmount,
                RoomId = roomId
            };
        }

        private CreateReservationDto CreateReservationDto(
            int clientId = 1,
            int roomId = 1,
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? reservationDate = null,
            int guestCount = 3,
            decimal paymentAmount = 100
        )
        {
            return new CreateReservationDto
            {
                ClientId = clientId,
                RoomId = roomId,
                StartDate = startDate ?? DateTime.Now.AddDays(1),
                EndDate = endDate ?? DateTime.Now.AddDays(5),
                ReservationDate = reservationDate ?? DateTime.Now,
                GuestCount = guestCount,
                PaymentAmount = paymentAmount
            };
        }

        private UpdateReservationDto CreateUpdateReservationDto(
            int reservationId = 1,
            int clientId = 1,
            int roomId = 1,
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? reservationDate = null,
            int guestCount = 3,
            decimal paymentAmount = 100,
            string status = "Confirmed"
            )
        {
            return new UpdateReservationDto
            {
                ReservationId = reservationId,
                ClientId = clientId,
                RoomId = roomId,
                StartDate = startDate ?? DateTime.Now.AddDays(1),
                EndDate = endDate ?? DateTime.Now.AddDays(5),
                ReservationDate = reservationDate ?? DateTime.Now,
                GuestCount = guestCount,
                PaymentAmount = paymentAmount,
                Status = status
            };
        }

        public class GetAllReservationsAsync : ReservationServiceTest
        {
            [Fact]
            public async Task GetAllReservationAsync_Should_Return_Reservations_When_RepositorySucceeds()
            {
                // Arrange
                var reservations = new List<Reservation>
            {
                CreateReservation(reservationId:1),
                CreateReservation(reservationId:2)
            };

                _reservationRepository
                    .Setup(r => r.GetAllListAsync())
                    .ReturnsAsync(OperationResult<List<Reservation>>
                    .Success("Success", reservations));

                // Act
                var result = await _service.GetAllReservationAsync();

                // Assert
                result.Data.Should().NotBeNull();
                result.IsSuccess.Should().Be(true);
                result.Data.Count.Should().Be(2);

            }

            [Fact]
            public async Task GetAllReservationAsync_Should_Return_Failure_When_RepositoryFails()
            {
                // Arrange
                _reservationRepository
                    .Setup(r => r.GetAllListAsync())
                    .ReturnsAsync(OperationResult<List<Reservation>>
                    .Failure("Database error"));

                // Act
                var result = await _service.GetAllReservationAsync();

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Database error", result.Message);
            }

        }

        public class GetReservationByIdAsync : ReservationServiceTest
        {

            [Fact]
            public async Task GetReservationByIdAsync_ShouldFail_WhenIdIsInvalid()
            {
                var result = await _service.GetReservationByIdAsync(0);

                result.IsSuccess.Should().Be(false);
                result.Message.Should().Be("Invalid ID");
            }

            [Fact]
            public async Task GetReservationByIdAsync_ShouldFail_WhenReservationNotFound()
            {
                _reservationRepository
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Ok", null));

                var result = await _service.GetReservationByIdAsync(1);

                result.IsSuccess.Should().Be(false);
                result.Message.Should().Be("No Reservations found.");
            }

            [Fact]
            public async Task GetReservationByIdAsync_ShouldReturnReservation_WhenSuccessful()
            {
                var reservation = CreateReservation();

                _reservationRepository
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Ok", reservation));

                var result = await _service.GetReservationByIdAsync(1);

                result.Message.Should().Be("Ok");
                result.Data.Should().NotBeNull();
                result.Data.ReservationId.Should().Be(reservation.ReservationId);

            }
        }

        public class AddReservationAsync : ReservationServiceTest
        {


            [Fact]
            public async Task AddReservationAsync_ShouldFail_WhenDtoIsNull()
            {
                // Act
                var result = await _service.AddReservationAsync(null);

                // Assert
                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Reservation data cannot be null.");
            }

            [Fact]
            public async Task AddReservationAsync_ShouldFail_WhenClientIdIsInvalid()
            {
                // Arrange
                var dto = CreateReservationDto(clientId: 0);

                // Act
                var result = await _service.AddReservationAsync(dto);

                // Assert
                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("ClientId must be greater than 0.");
            }

            [Fact]
            public async Task AddReservationAsync_ShouldFail_WhenStartDateIsAfterEndDate()
            {
                // Arrange
                var dto = CreateReservationDto(
                    startDate: DateTime.Now.AddDays(10),
                    endDate: DateTime.Now.AddDays(1));

                // Act
                var result = await _service.AddReservationAsync(dto);

                // Assert
                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("StartDate must be earlier than EndDate.");
            }

            [Fact]
            public async Task AddReservationAsync_ShouldFail_WhenRepositoryFails()
            {
                // Arrange
                var dto = CreateReservationDto();

                _reservationRepository
                    .Setup(r => r.AddAsync(It.IsAny<Reservation>()))
                    .ReturnsAsync(OperationResult<Reservation>.Failure("Database error"));

                // Act
                var result = await _service.AddReservationAsync(dto);

                // Assert
                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Database error");
            }

            [Fact]
            public async Task AddReservationAsync_ShouldCreateReservation_WhenValid()
            {
                // Arrange
                var dto = CreateReservationDto();
                var entity = CreateReservation();

                _reservationRepository
                    .Setup(r => r.AddAsync(It.IsAny<Reservation>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Created", entity));

                // Act
                var result = await _service.AddReservationAsync(dto);

                // Assert
                result.IsSuccess.Should().BeTrue();
                result.Data.Should().NotBeNull();
                result.Data.ReservationId.Should().Be(entity.ReservationId);
            }
        }

        public class UpdateReservationAsync : ReservationServiceTest
        {
            [Fact]
            public async Task UpdateReservationAsync_ShouldFail_WhenDtoIsNull()
            {
                var result = await _service.UpdateReservationAsync(1, null);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Reservation data cannot be null.");
            }

            [Fact]
            public async Task UpdateReservationAsync_ShouldFail_WhenReservationIdIsInvalid()
            {
                var dto = CreateUpdateReservationDto(reservationId: 0);

                var result = await _service.UpdateReservationAsync(1, dto);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("ReservationId must be greater than 0.");
            }

            [Fact]
            public async Task UpdateReservationAsync_ShouldFail_WhenReservationDoesNotExist()
            {
                var dto = CreateUpdateReservationDto();

                _reservationRepository
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Ok", null));

                var result = await _service.UpdateReservationAsync(1, dto);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Reservation not found.");
            }

            [Fact]
            public async Task UpdateReservationAsync_ShouldFail_WhenRepositoryUpdateFails()
            {
                var dto = CreateUpdateReservationDto();
                var entity = CreateReservation();

                _reservationRepository
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Ok", entity));

                _reservationRepository
                    .Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Reservation>()))
                    .ReturnsAsync(OperationResult<Reservation>.Failure("Database error"));

                var result = await _service.UpdateReservationAsync(1, dto);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Database error");
            }

            [Fact]
            public async Task UpdateReservationAsync_ShouldUpdateReservation_WhenValid()
            {
                var dto = CreateUpdateReservationDto();
                var entity = CreateReservation();

                _reservationRepository
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Ok", entity));

                _reservationRepository
                    .Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Reservation>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Updated", entity));

                var result = await _service.UpdateReservationAsync(1, dto);

                result.IsSuccess.Should().BeTrue();
                result.Data.Should().NotBeNull();
                result.Data.ReservationId.Should().Be(entity.ReservationId);
            }

        }

        public class DeleteReservationAsync : ReservationServiceTest
        {
            [Fact]
            public async Task DeleteReservationAsync_ShouldFail_WhenIdIsInvalid()
            {
                var result = await _service.DeleteReservationAsync(0);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("ReservationId must be greater than 0.");
            }

            [Fact]
            public async Task DeleteReservationAsync_ShouldFail_WhenRepositoryFails()
            {
                _reservationRepository
                    .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Failure("Database error"));

                var result = await _service.DeleteReservationAsync(1);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().Be("Database error");
            }

            [Fact]
            public async Task DeleteReservationAsync_ShouldDeleteReservation_WhenValid()
            {
                _reservationRepository
                    .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                    .ReturnsAsync(OperationResult<Reservation>.Success("Deleted", CreateReservation()));

                var result = await _service.DeleteReservationAsync(1);

                result.IsSuccess.Should().BeTrue();
                result.Data.Should().BeTrue();
            }

        }




    }
}
