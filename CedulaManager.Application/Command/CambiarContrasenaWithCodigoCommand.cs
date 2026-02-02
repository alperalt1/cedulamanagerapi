using CedulaManager.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Command
{
    public record CambiarContrasenaWithCodigoCommand(
        string Correo,
        string Codigo,
        string PasswordSinHash,
        string RepeatPasswordSinHash
        ) : IRequest<BaseResponse<bool>>;
}
