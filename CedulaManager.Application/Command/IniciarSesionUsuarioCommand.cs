using CedulaManager.Application.Common;
using CedulaManager.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Command
{
    public record IniciarSesionUsuarioCommand(
        string Correo,
        string Password
        ) : IRequest<BaseResponse<LoginResponse>>;
}
