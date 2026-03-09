namespace SGRHDevOps.Core.Application.Dtos.ReservationModule
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required DateTime ReservationDate { get; set; }
        public string? Status { get; set; }
        public int GuestCount { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
