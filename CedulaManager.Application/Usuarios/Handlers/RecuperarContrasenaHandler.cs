using CedulaManager.Application.Common;
using CedulaManager.Application.Dtos;
using CedulaManager.Application.Events;
using CedulaManager.Application.Query;
using CedulaManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class RecuperarContrasenaHandler : IRequestHandler<RecuperarContrasenaQuery, BaseResponse<string>>
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        public RecuperarContrasenaHandler(IAuthService authService, IMediator mediator, IUsuarioRepository usuarioRepository)
        {
            _authService = authService;
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<BaseResponse<string>> Handle(RecuperarContrasenaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Correo))
                {
                    return BaseResponse<string>.Failure("Completar el campo");
                }

                var usuario = await _usuarioRepository.GetByEmailAsync(request.Correo);
                if (usuario != null)
                {
                    string codigoGenerado = new Random().Next(100000, 999999).ToString();
                    await _mediator.Publish(new RecuperarContrasenaMailEvent(request.Correo, codigoGenerado));
                }
                return BaseResponse<string>.Ok("Si el correo coincide con una cuenta, recibirá un código.", "Operación Exitosa");
            }
            catch (Exception ex)
            {
                return BaseResponse<string>.Failure($"Error: {ex.Message}");
            }
        }

    }
}
