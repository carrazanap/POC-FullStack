using Application.ApplicationServices;
using Application.DataTransferObjects;
using Application.UseCases.Automovil.Commands.CrearAutomovil;
using Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    public class AutomovilController : BaseController
    {
        private readonly ICommandQueryBus _commandQueryBus;
        private readonly IAutomovilApplicationService _automovilApplicationService;
        
        public AutomovilController(
            ICommandQueryBus commandQueryBus, 
            IAutomovilApplicationService automovilApplicationService)
        {
            _commandQueryBus = commandQueryBus ?? throw new ArgumentNullException(nameof(commandQueryBus));
            _automovilApplicationService = automovilApplicationService ?? throw new ArgumentNullException(nameof(automovilApplicationService));
        }

        /// <summary>
        /// Obtiene la lista completa de automóviles
        /// </summary>
        [HttpGet("api/v1/[controller]")]
        public async Task<IActionResult> GetAutomoviles()
        {
            try
            {
                var automoviles = await _automovilApplicationService.ObtenerTodosAsync();
                return Ok(new { success = true, data = automoviles, message = "Automóviles obtenidos exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Obtiene un automóvil específico por su ID
        /// </summary>
        [HttpGet("api/v1/[controller]/{id}")]
        public async Task<IActionResult> GetAutomovil(int id)
        {
            try
            {
                var automovil = await _automovilApplicationService.ObtenerPorIdAsync(id);
                if (automovil == null)
                {
                    return NotFound(new { success = false, data = (object?)null, message = "Automóvil no encontrado", errors = new[] { $"No existe un automóvil con el ID {id}" } });
                }

                return Ok(new { success = true, data = automovil, message = "Automóvil encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Crea un nuevo automóvil
        /// </summary>
        [HttpPost("api/v1/[controller]")]
        public async Task<IActionResult> Create([FromBody] CrearAutomovilDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { success = false, data = (object?)null, message = "Datos inválidos", errors = new[] { "El cuerpo de la petición no puede estar vacío" } });

                var command = new CrearAutomovilCommand(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis);
                var id = await _commandQueryBus.Send(command);

                return Created($"api/v1/[Controller]/{id}", new { success = true, data = new { id }, message = "Automóvil creado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { success = false, data = (object?)null, message = ex.Message, errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Actualiza un automóvil existente
        /// </summary>
        [HttpPut("api/v1/[controller]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarAutomovilDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { success = false, data = (object?)null, message = "Datos inválidos", errors = new[] { "El cuerpo de la petición no puede estar vacío" } });

                var resultado = await _automovilApplicationService.ActualizarAsync(id, dto);
                if (!resultado)
                {
                    return NotFound(new { success = false, data = (object?)null, message = "Automóvil no encontrado", errors = new[] { $"No existe un automóvil con el ID {id}" } });
                }

                var automovilActualizado = await _automovilApplicationService.ObtenerPorIdAsync(id);
                return Ok(new { success = true, data = automovilActualizado, message = "Automóvil actualizado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { success = false, data = (object?)null, message = ex.Message, errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Elimina un automóvil
        /// </summary>
        [HttpDelete("api/v1/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _automovilApplicationService.EliminarAsync(id);
                if (!resultado)
                {
                    return NotFound(new { success = false, data = (object?)null, message = "Automóvil no encontrado", errors = new[] { $"No existe un automóvil con el ID {id}" } });
                }

                return Ok(new { success = true, data = (object?)null, message = "Automóvil eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Buscar automóviles por marca
        /// </summary>
        [HttpGet("api/v1/[controller]/buscar/marca/{marca}")]
        public async Task<IActionResult> BuscarPorMarca(string marca)
        {
            try
            {
                var automoviles = await _automovilApplicationService.BuscarPorMarcaAsync(marca);
                return Ok(new { success = true, data = automoviles, message = $"Búsqueda por marca '{marca}' completada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Buscar automóviles por rango de años de fabricación
        /// </summary>
        [HttpGet("api/v1/[controller]/buscar/anio")]
        public async Task<IActionResult> BuscarPorAnioFabricacion([FromQuery] int anioInicio, [FromQuery] int anioFin)
        {
            try
            {
                var automoviles = await _automovilApplicationService.BuscarPorAnioFabricacionAsync(anioInicio, anioFin);
                return Ok(new { success = true, data = automoviles, message = $"Búsqueda por años {anioInicio}-{anioFin} completada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Buscar automóviles por color
        /// </summary>
        [HttpGet("api/v1/[controller]/buscar/color/{color}")]
        public async Task<IActionResult> BuscarPorColor(string color)
        {
            try
            {
                var automoviles = await _automovilApplicationService.BuscarPorColorAsync(color);
                return Ok(new { success = true, data = automoviles, message = $"Búsqueda por color '{color}' completada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Validar si un número de motor es único
        /// </summary>
        [HttpGet("api/v1/[controller]/validar/motor/{numeroMotor}")]
        public async Task<IActionResult> ValidarNumeroMotor(string numeroMotor, [FromQuery] int? excluirId = null)
        {
            try
            {
                var esUnico = await _automovilApplicationService.ValidarNumeroMotorUnicoAsync(numeroMotor, excluirId);
                return Ok(new { success = true, data = new { esUnico, numeroMotor }, message = "Validación completada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }

        /// <summary>
        /// Validar si un número de chasis es único
        /// </summary>
        [HttpGet("api/v1/[controller]/validar/chasis/{numeroChasis}")]
        public async Task<IActionResult> ValidarNumeroChasis(string numeroChasis, [FromQuery] int? excluirId = null)
        {
            try
            {
                var esUnico = await _automovilApplicationService.ValidarNumeroChasisUnicoAsync(numeroChasis, excluirId);
                return Ok(new { success = true, data = new { esUnico, numeroChasis }, message = "Validación completada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, data = (object?)null, message = "Error interno del servidor", errors = new[] { ex.Message } });
            }
        }
    }
}
