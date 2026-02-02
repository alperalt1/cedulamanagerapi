using CedulaManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegistrarAsync(string nombre, string apellido, string correo, string password);
        Task<Usuario> IniciarSesionAsync(string correo, string password);
    }
}
