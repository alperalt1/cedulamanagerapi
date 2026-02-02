using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Events
{
    public record RegistrarUsuarioMailEvent(string Correo, string Nombre) : INotification;
}
