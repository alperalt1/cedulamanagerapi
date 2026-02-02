using CedulaManager.Application.Events;
using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class RecuperarContrasenaMailHandler: INotificationHandler<RecuperarContrasenaMailEvent>
    {

        private readonly INotificacionService _notificacionService;
        private readonly IRecuperarContrasenaRepository _recuperarContrasenaRepository;

        public RecuperarContrasenaMailHandler(INotificacionService notificacionService, IRecuperarContrasenaRepository recuperarContrasenaRepository)
        {
            _notificacionService = notificacionService;
            _recuperarContrasenaRepository = recuperarContrasenaRepository;
        }

        public async Task Handle(RecuperarContrasenaMailEvent notification, CancellationToken ct)
        {
            try
            {
                var resetInfo = new RecuperarContrasena 
                {
                    Id = Guid.NewGuid(),
                    Correo = notification.Correo,
                    Codigo = notification.Codigo,
                    FechaExpiracion = DateTime.UtcNow.AddMinutes(15), 
                    FueUtilizado = false
                };

                await _recuperarContrasenaRepository.AddRecuperarContrasenaAsync(resetInfo);
                await _recuperarContrasenaRepository.SaveChangesRecuperarContrasneaAsync();

                string asunto = "Recuperación de Contraseña";
                string cuerpo = $@"Código de Verificación

                Has solicitado restablecer tu contraseña.
                Tu código es: {notification.Codigo}

                Este código expirará en 15 minutos.";

                await _notificacionService.SendEmailAsync(notification.Correo, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de red con Unosend: {ex.Message}");
            }
        }
    }
}
