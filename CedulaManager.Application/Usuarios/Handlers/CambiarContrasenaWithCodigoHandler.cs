using CedulaManager.Application.Command;
using CedulaManager.Application.Common;
using CedulaManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class CambiarContrasenaWithCodigoHandler: IRequestHandler<CambiarContrasenaWithCodigoCommand, BaseResponse<bool>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRecuperarContrasenaRepository _recuperarContrasenaRepository;
        public CambiarContrasenaWithCodigoHandler(IUsuarioRepository usuarioRepository, IRecuperarContrasenaRepository recuperarContrasenaRepository) 
        { 
            _usuarioRepository = usuarioRepository;
            _recuperarContrasenaRepository = recuperarContrasenaRepository;
        }

        public async Task<BaseResponse<bool>> Handle(CambiarContrasenaWithCodigoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existe = await _usuarioRepository.GetByEmailAsync(request.Correo);
                if (existe == null)
                {
                    return BaseResponse<bool>.Failure("Correo Invalido");
                }
                if ( request.PasswordSinHash != request.RepeatPasswordSinHash)
                {
                    return BaseResponse<bool>.Failure("Contrasena no son iguales");
                }

                var registroCodigo = await _recuperarContrasenaRepository.GetByEmailAndCodigoAsync(request.Correo, request.Codigo);

                if (registroCodigo == null || registroCodigo.FueUtilizado || registroCodigo.FechaExpiracion < DateTime.UtcNow)
                {
                    return BaseResponse<bool>.Failure("El código es inválido o ya ha expirado.");
                }

                string passwordHasheada = BCrypt.Net.BCrypt.HashPassword(request.PasswordSinHash);
                await _recuperarContrasenaRepository.UpdatePasswordAsync(passwordHasheada, request.Correo);

                registroCodigo.FueUtilizado = true;
                await _recuperarContrasenaRepository.UpdateAsync(registroCodigo);
                return BaseResponse<bool>.Ok(true, 
                    
                    "Código válido, puede proceder al cambio.");

            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure($"Error: {ex.Message}");
            }
        }
    }
}
