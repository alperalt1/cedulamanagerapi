using CedulaManager.Application.Command;
using CedulaManager.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CedulaManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INotificacionService _notificacionService;

        public UserController(IMediator mediator, INotificacionService notificacionService)
        {
            _mediator = mediator;
            _notificacionService = notificacionService;
        }
        [HttpGet("perfil")]
        public async Task<IActionResult> Pefil([FromBody] IniciarSesionUsuarioCommand command)
        {
            return Ok("E");
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
