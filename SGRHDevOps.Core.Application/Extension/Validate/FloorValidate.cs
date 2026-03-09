using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Extension.Validate
{
    public class FloorDtoValidator : AbstractValidator<FloorDto>
    {
        public FloorDtoValidator()
        {
            RuleFor(x => x.FloorNumber)
                .GreaterThan(0)
                .WithMessage("El número de piso debe ser mayor que 0.")
                .NotEmpty()
                .WithMessage("El número de piso es requerido.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("La descripción no puede exceder 500 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El estado es requerido.")
                .Must(x => x == "active" || x == "inactive" || x == "maintenance")
                .WithMessage("El estado debe ser 'active', 'inactive' o 'maintenance'.");
        }
    }
}
