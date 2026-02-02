using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Events
{
    public record RecuperarContrasenaMailEvent(string Correo, string Codigo) : INotification;
}
