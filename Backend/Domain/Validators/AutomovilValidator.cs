using Core.Domain.Validators;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AutomovilValidator : EntityValidator<Automovil>
    {
        public AutomovilValidator()
        {
            // Validar Marca
            RuleFor(a => a.Marca)
                .NotEmpty().WithMessage("La marca es obligatoria")
                .MinimumLength(2).WithMessage("La marca debe tener al menos 2 caracteres")
                .MaximumLength(50).WithMessage("La marca no puede exceder 50 caracteres");

            // Validar Modelo
            RuleFor(a => a.Modelo)
                .NotEmpty().WithMessage("El modelo es obligatorio")
                .MinimumLength(2).WithMessage("El modelo debe tener al menos 2 caracteres")
                .MaximumLength(50).WithMessage("El modelo no puede exceder 50 caracteres");

            // Validar Color
            RuleFor(a => a.Color)
                .NotEmpty().WithMessage("El color es obligatorio")
                .MinimumLength(3).WithMessage("El color debe tener al menos 3 caracteres")
                .MaximumLength(30).WithMessage("El color no puede exceder 30 caracteres");

            // Validar Año de Fabricación
            RuleFor(a => a.Fabricacion)
                .GreaterThanOrEqualTo(1900).WithMessage("El año de fabricación no puede ser anterior a 1900")
                .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage($"El año de fabricación no puede ser posterior a {DateTime.Now.Year + 1}");

            // Validar Número de Motor
            RuleFor(a => a.NumeroMotor)
                .NotEmpty().WithMessage("El número de motor es obligatorio")
                .MinimumLength(5).WithMessage("El número de motor debe tener al menos 5 caracteres")
                .MaximumLength(30).WithMessage("El número de motor no puede exceder 30 caracteres");

            // Validar Número de Chasis
            RuleFor(a => a.NumeroChasis)
                .NotEmpty().WithMessage("El número de chasis es obligatorio")
                .MinimumLength(10).WithMessage("El número de chasis debe tener al menos 10 caracteres")
                .MaximumLength(50).WithMessage("El número de chasis no puede exceder 50 caracteres");
        }
    }
}
