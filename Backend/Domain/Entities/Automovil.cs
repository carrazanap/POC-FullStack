using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    public class Automovil : DomainEntity<int, AutomovilValidator>
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Color { get; private set; }
        public int Fabricacion { get; private set; }
        public string NumeroMotor { get; private set; }
        public string NumeroChasis { get; private set; }

        protected Automovil()
        {
        }

        public Automovil(string marca, string modelo, string color, int fabricacion, string numeroMotor, string numeroChasis)
        {
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Fabricacion = fabricacion;
            NumeroMotor = numeroMotor;
            NumeroChasis = numeroChasis;
            
            // Validar la entidad después de la construcción
            Validate();
        }

        public void ActualizarInformacion(string marca, string modelo, string color, int fabricacion)
        {
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Fabricacion = fabricacion;
            
            // Validar después de la actualización
            Validate();
        }

        public void ActualizarNumeroMotor(string numeroMotor)
        {
            NumeroMotor = numeroMotor;
            Validate();
        }

        public void ActualizarNumeroChasis(string numeroChasis)
        {
            NumeroChasis = numeroChasis;
            Validate();
        }
    }
}
