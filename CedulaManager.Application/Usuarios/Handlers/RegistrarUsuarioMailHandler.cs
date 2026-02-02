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
    public class RegistrarUsuarioMailHandler : INotificationHandler<RegistrarUsuarioMailEvent>
    {
        private readonly INotificacionService _notificacionService;
        public RegistrarUsuarioMailHandler(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        public async Task Handle(RegistrarUsuarioMailEvent notification, CancellationToken ct)
        {
            try
            {
                string asunto = $"Bienvenido {notification.Nombre}";
                string cuerpo = $"Hola {notification.Nombre}, gracias por registrarte con el correo {notification.Correo}.";
                await _notificacionService.SendEmailAsync(notification.Correo, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de red con Unosend: {ex.Message}");
            }
        }
    }
}
