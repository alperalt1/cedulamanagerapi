using CedulaManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Interfaces
{
    public interface IRecuperarContrasenaRepository
    {
        Task AddRecuperarContrasenaAsync(RecuperarContrasena recuperarContrasena);
        Task SaveChangesRecuperarContrasneaAsync();
        Task<RecuperarContrasena?> GetByCodigoAsync(string codigo);
        Task<RecuperarContrasena?> GetByEmailAndCodigoAsync(string correo, string codigo);
        Task UpdatePasswordAsync(string password, string correo);
        Task UpdateAsync(RecuperarContrasena entidad);
    }
}
