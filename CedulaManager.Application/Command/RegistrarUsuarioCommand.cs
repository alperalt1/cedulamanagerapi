using CedulaManager.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Command
{
    public record RegistrarUsuarioCommand(
        string Nombre,
        string Apellido,
        string Correo,
        string Password
        ): IRequest<BaseResponse<Guid>>;
}
