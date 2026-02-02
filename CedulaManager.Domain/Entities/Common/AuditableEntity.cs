using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Entities.Common
{
    public abstract class AuditableEntity
    {
        public bool Activo { get; set; }
        public string UsuarioRegistro { get; set; } = "SYSTEM";
        public DateTime FechaRegistro { get; set; }
        public string? IpRegistro { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? IpModificacion { get; set; }

    }
}
