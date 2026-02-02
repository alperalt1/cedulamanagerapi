using CedulaManager.Application.Common;
using CedulaManager.Application.Dtos;
using CedulaManager.Application.Query;
using CedulaManager.Domain.Interfaces;
using MediatR;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class CodigoHandler : IRequestHandler<CodigoQuery, BaseResponse<bool>>
    {
        private readonly IRecuperarContrasenaRepository _recuperarContrasenaRepository;
        public CodigoHandler(IRecuperarContrasenaRepository recuperarContrasenaRepository )
        {
            _recuperarContrasenaRepository = recuperarContrasenaRepository;
        }

        public async Task<BaseResponse<bool>> Handle(CodigoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var codigo = await _recuperarContrasenaRepository.GetByCodigoAsync( request.Codigo );
                if (codigo == null)
                {
                    return BaseResponse<bool>.Failure("El código es inválido.");
                }
                if (codigo.FueUtilizado)
                {
                    return BaseResponse<bool>.Failure("Este código ya ha sido utilizado.");
                }
                if (codigo?.FechaExpiracion < DateTime.UtcNow) 
                {
                    return BaseResponse<bool>.Failure("El código ha expirado. Solicita uno nuevo.");
                }

                return BaseResponse<bool>.Ok(true, "Código válido, puede proceder al cambio.");

            }
            catch ( Exception ex)
            {
                return BaseResponse<bool>.Failure($"Error: {ex.Message}");
            }
        }
    }
}
