using FluentValidation;
using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRHDevOps.Core.Application.Extension.Validate
{
    public class RoomCategoryDtoValidator : AbstractValidator<RoomCategoryDto>
    {
        public RoomCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre de la categoría es requerido.")
                .MinimumLength(2)
                .WithMessage("El nombre debe tener al menos 2 caracteres.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede exceder 100 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s\-áéíóúÁÉÍÓÚñÑ]+$")
                .WithMessage("El nombre solo puede contener letras, números, espacios, guiones y acentos.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("La descripción es requerida.")
                .MinimumLength(5)
                .WithMessage("La descripción debe tener al menos 5 caracteres.")
                .MaximumLength(500)
                .WithMessage("La descripción no puede exceder 500 caracteres.");

            RuleFor(x => x.MaxCapacity)
                .GreaterThan(0)
                .WithMessage("La capacidad máxima debe ser mayor que 0.")
                .LessThanOrEqualTo(50)
                .WithMessage("La capacidad máxima no puede exceder 50 personas.");

            RuleFor(x => x.Amenities)
                .MaximumLength(1000)
                .WithMessage("Las amenidades no pueden exceder 1000 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Amenities));
        }
    }
}
