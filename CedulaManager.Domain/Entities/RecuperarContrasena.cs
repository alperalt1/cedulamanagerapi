using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Entities
{
    public class RecuperarContrasena
    {
        public Guid Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public DateTime FechaExpiracion { get; set; }
        public bool FueUtilizado { get; set; } = false;
    }
}
