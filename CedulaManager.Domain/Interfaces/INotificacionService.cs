using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Interfaces
{
    public interface INotificacionService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
