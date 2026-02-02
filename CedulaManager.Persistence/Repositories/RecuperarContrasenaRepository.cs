using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using CedulaManager.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Persistence.Repositories
{
    public class RecuperarContrasenaRepository: IRecuperarContrasenaRepository
    {
        private readonly ApplicationDbContext _context;

        public RecuperarContrasenaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRecuperarContrasenaAsync(RecuperarContrasena recuperarContrasena)
        {
            await _context.RecuperarContrasenas.AddAsync(recuperarContrasena);
        }

        public async Task SaveChangesRecuperarContrasneaAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RecuperarContrasena?> GetByCodigoAsync(string Codigo)
        {
            return await _context.RecuperarContrasenas.FirstAsync(x => x.Codigo == Codigo);
        }

        public async Task<RecuperarContrasena?> GetByEmailAndCodigoAsync(string correo, string codigo)
        {
            return await _context.RecuperarContrasenas
                .Where(x => x.Correo == correo && x.Codigo == codigo)
                .OrderByDescending(x => x.FechaExpiracion)
                .FirstOrDefaultAsync();
        }

        public async Task UpdatePasswordAsync(string password, string correo)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == correo && x.Activo == true);

            if (usuario != null)
            {
                usuario.PasswordHash = password;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Usuario no encontrado o inactivo");
            }

        }

        public async Task UpdateAsync(RecuperarContrasena entidad)
        {
            _context.RecuperarContrasenas.Update(entidad);
            await _context.SaveChangesAsync();
        }



    }
}
