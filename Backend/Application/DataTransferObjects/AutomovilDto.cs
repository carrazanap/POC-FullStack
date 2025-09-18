namespace Application.DataTransferObjects
{
    public class AutomovilDto
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Fabricacion { get; set; }
        public string NumeroMotor { get; set; } = string.Empty;
        public string NumeroChasis { get; set; } = string.Empty;
    }

    public class CrearAutomovilDto
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Fabricacion { get; set; }
        public string NumeroMotor { get; set; } = string.Empty;
        public string NumeroChasis { get; set; } = string.Empty;
    }

    public class ActualizarAutomovilDto
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Fabricacion { get; set; }
        public string NumeroMotor { get; set; } = string.Empty;
        public string NumeroChasis { get; set; } = string.Empty;
    }
}
