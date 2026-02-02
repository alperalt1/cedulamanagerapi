using CedulaManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Entities
{
    public class HistorialConsultas: AuditableEntity
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public Guid CedulaId {  get; set; }
        public Cedula? Cedula { get; set; }

    }
}
