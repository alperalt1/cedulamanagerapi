using CedulaManager.Application.Command;
using CedulaManager.Application.Common;
using CedulaManager.Application.Events;
using CedulaManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class RegistrarUsuarioHandler: IRequestHandler<RegistrarUsuarioCommand, BaseResponse<Guid>>
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public RegistrarUsuarioHandler(IAuthService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        public async Task<BaseResponse<Guid>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new RegistrarUsuarioValidator();
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    var errores = result.Errors.Select(e => e.ErrorMessage).ToList();
                    return BaseResponse<Guid>.Failure("Errores de validación", errores);
                }

                var userId = await _authService.RegistrarAsync(
                    request.Nombre,
                    request.Apellido,
                    request.Correo,
                    request.Password);
                await _mediator.Publish(new RegistrarUsuarioMailEvent(request.Correo, request.Nombre));
                return BaseResponse<Guid>.Ok(userId, "Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BaseResponse<Guid>.Failure($"Error al registrar el usuario: {ex.Message}");
            }
        }
    }
}
