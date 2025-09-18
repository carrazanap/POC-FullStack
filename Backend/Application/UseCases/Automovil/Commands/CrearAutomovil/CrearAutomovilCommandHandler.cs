using Application.ApplicationServices;
using Application.DataTransferObjects;
using Core.Application.ComandQueryBus.Commands;

namespace Application.UseCases.Automovil.Commands.CrearAutomovil
{
    public class CrearAutomovilCommandHandler : IRequestCommandHandler<CrearAutomovilCommand, int>
    {
        private readonly IAutomovilApplicationService _automovilApplicationService;

        public CrearAutomovilCommandHandler(IAutomovilApplicationService automovilApplicationService)
        {
            _automovilApplicationService = automovilApplicationService ?? throw new ArgumentNullException(nameof(automovilApplicationService));
        }

        public async Task<int> Handle(CrearAutomovilCommand request, CancellationToken cancellationToken)
        {
            var dto = new CrearAutomovilDto
            {
                Marca = request.Marca,
                Modelo = request.Modelo,
                Color = request.Color,
                Fabricacion = request.Fabricacion,
                NumeroMotor = request.NumeroMotor,
                NumeroChasis = request.NumeroChasis
            };

            return await _automovilApplicationService.CrearAsync(dto);
        }
    }
}
