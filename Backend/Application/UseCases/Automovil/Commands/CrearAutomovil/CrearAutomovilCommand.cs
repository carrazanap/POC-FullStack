using Core.Application;

namespace Application.UseCases.Automovil.Commands.CrearAutomovil
{
    public class CrearAutomovilCommand : IRequestCommand<int>
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Fabricacion { get; set; }
        public string NumeroMotor { get; set; } = string.Empty;
        public string NumeroChasis { get; set; } = string.Empty;

        public CrearAutomovilCommand(string marca, string modelo, string color, int fabricacion, string numeroMotor, string numeroChasis)
        {
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Fabricacion = fabricacion;
            NumeroMotor = numeroMotor;
            NumeroChasis = numeroChasis;
        }
    }
}
