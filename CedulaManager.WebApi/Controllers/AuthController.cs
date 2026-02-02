using CedulaManager.Application.Command;
using CedulaManager.Application.Query;
using CedulaManager.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CedulaManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INotificacionService _notificacionService;

        public AuthController(IMediator mediator, INotificacionService notificacionService)
        {
            _mediator = mediator;
            _notificacionService = notificacionService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("iniciarsesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionUsuarioCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("recuperarContrasena")]
        public async Task<IActionResult> RecuperarContrasena([FromBody] RecuperarContrasenaQuery command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("codigo")]
        public async Task<IActionResult> Codigo([FromBody] CodigoQuery command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("cambiarcontrasenawithCodigo")]
        public async Task<IActionResult> CambiarContrasenaWithCodigo([FromBody] CodigoQuery command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("cambiarcontrasenawithoutCodigo")]
        public async Task<IActionResult> CambiarContrasenaWithoutCodigo([FromBody] CodigoQuery command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPost("test-email")]
        public async Task<IActionResult> TestEmail([FromQuery] string correoDestino)
        {
            try
            {
                // Esto llamará directamente a tu implementación SMTP o SDK
                await _notificacionService.SendEmailAsync(
                    correoDestino,
                    "Prueba Directa desde Postman",
                    "<h1>¡Funciona!</h1><p>Esta es una prueba del servicio de mensajería.</p>"
                );

                return Ok(new { success = true, message = $"Petición enviada a {correoDestino}. Revisa la consola de Visual Studio." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
