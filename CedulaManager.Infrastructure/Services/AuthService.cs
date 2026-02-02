using CedulaManager.Application.Common;
using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRecuperarContrasenaRepository _recuperarContrasenaRepository;
        public AuthService(IUsuarioRepository usuarioRepository, IRecuperarContrasenaRepository recuperarContrasenaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _recuperarContrasenaRepository = recuperarContrasenaRepository;
        }

        public async Task<Guid> RegistrarAsync(string nombre, string apellido, string correo, string password)
        {
            var existe = await _usuarioRepository.GetByEmailAsync(correo);
            if (existe != null)
                throw new Exception("El correo ya está registrado");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                PasswordHash = passwordHash,
                Activo = true
            };

            await _usuarioRepository.AddAsync(nuevoUsuario);
            await _usuarioRepository.SaveChangesAsync();

            return nuevoUsuario.Id;
        }

        public async Task<Usuario> IniciarSesionAsync(string correo, string password)
        {
            var existe = await _usuarioRepository.GetByEmailAsync(correo);
            if (existe == null || !BCrypt.Net.BCrypt.Verify(password, existe.PasswordHash))
            {
                throw new Exception("Credenciales Incorrectas");
            }

            return existe;
        }

    }
}
