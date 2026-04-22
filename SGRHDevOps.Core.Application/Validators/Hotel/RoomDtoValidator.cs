using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel;

namespace SGRHDevOps.Core.Application.Validators.Hotel
{
    public class RoomDtoValidator : AbstractValidator<RoomDto>
    {
        public RoomDtoValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(10).WithMessage("Room number must not exceed 10 characters.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");

            RuleFor(x => x.FloorId)
                .GreaterThan(0).WithMessage("FloorId must be greater than 0.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => s == "available" || s == "occupied" || s == "maintenance")
                .WithMessage("Status must be available, occupied or maintenance.");
        }
    }
}
