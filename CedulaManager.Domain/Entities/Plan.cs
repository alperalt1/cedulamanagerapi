using CedulaManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Entities
{
    public class Plan: AuditableEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public long ConsultasContratadas { get; set; }
        public long ConsultasRestantes { get; set; }
        public decimal Precio { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
