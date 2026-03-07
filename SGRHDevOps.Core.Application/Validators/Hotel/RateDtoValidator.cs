using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel;

namespace SGRHDevOps.Core.Application.Validators.Hotel
{
    public class RateDtoValidator : AbstractValidator<RateDto>
    {
        public RateDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");

            RuleFor(x => x.SeasonId)
                .GreaterThan(0).WithMessage("SeasonId must be greater than zero.");

            RuleFor(x => x.NightPrice)
                .GreaterThan(0).WithMessage("NightPrice must be greater than zero.");
        }
    }
}
