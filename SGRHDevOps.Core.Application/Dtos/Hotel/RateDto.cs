namespace SGRHDevOps.Core.Application.Dtos.Hotel
{
    public class RateDto
    {
        public int RateId { get; set; }
        public int CategoryId { get; set; }
        public int SeasonId { get; set; }
        public decimal NightPrice { get; set; }
    }
}
