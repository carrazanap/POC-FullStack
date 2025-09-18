using Core.Domain.Validators;
using Domain.Entities;

namespace Domain.Validators
{
    public class AutomovilValidator : EntityValidator<Automovil>
    {
        public override void Validate(Automovil entity)
        {
            base.Validate(entity);

            // Validar Marca
            if (string.IsNullOrWhiteSpace(entity.Marca))
                AddError("La marca es obligatoria");
            else if (entity.Marca.Length < 2)
                AddError("La marca debe tener al menos 2 caracteres");
            else if (entity.Marca.Length > 50)
                AddError("La marca no puede exceder 50 caracteres");

            // Validar Modelo
            if (string.IsNullOrWhiteSpace(entity.Modelo))
                AddError("El modelo es obligatorio");
            else if (entity.Modelo.Length < 2)
                AddError("El modelo debe tener al menos 2 caracteres");
            else if (entity.Modelo.Length > 50)
                AddError("El modelo no puede exceder 50 caracteres");

            // Validar Color
            if (string.IsNullOrWhiteSpace(entity.Color))
                AddError("El color es obligatorio");
            else if (entity.Color.Length < 3)
                AddError("El color debe tener al menos 3 caracteres");
            else if (entity.Color.Length > 30)
                AddError("El color no puede exceder 30 caracteres");

            // Validar Año de Fabricación
            var currentYear = DateTime.Now.Year;
            if (entity.Fabricacion < 1900)
                AddError("El año de fabricación no puede ser anterior a 1900");
            else if (entity.Fabricacion > currentYear + 1)
                AddError($"El año de fabricación no puede ser posterior a {currentYear + 1}");

            // Validar Número de Motor
            if (string.IsNullOrWhiteSpace(entity.NumeroMotor))
                AddError("El número de motor es obligatorio");
            else if (entity.NumeroMotor.Length < 5)
                AddError("El número de motor debe tener al menos 5 caracteres");
            else if (entity.NumeroMotor.Length > 30)
                AddError("El número de motor no puede exceder 30 caracteres");

            // Validar Número de Chasis
            if (string.IsNullOrWhiteSpace(entity.NumeroChasis))
                AddError("El número de chasis es obligatorio");
            else if (entity.NumeroChasis.Length < 10)
                AddError("El número de chasis debe tener al menos 10 caracteres");
            else if (entity.NumeroChasis.Length > 50)
                AddError("El número de chasis no puede exceder 50 caracteres");
        }
    }
}
