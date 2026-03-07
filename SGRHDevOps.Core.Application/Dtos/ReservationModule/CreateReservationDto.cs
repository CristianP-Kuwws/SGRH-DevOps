namespace SGRHDevOps.Core.Application.Dtos.ReservationModule
{
    public class CreateReservationDto
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required DateTime ReservationDate { get; set; }
        public int GuestCount { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
