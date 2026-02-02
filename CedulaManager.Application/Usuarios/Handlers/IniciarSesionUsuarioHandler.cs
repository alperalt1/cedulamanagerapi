using CedulaManager.Application.Command;
using CedulaManager.Application.Common;
using CedulaManager.Application.Dtos;
using CedulaManager.Application.Query;
using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CedulaManager.Application.Usuarios.Handlers
{
    public class IniciarSesionUsuarioHandler: IRequestHandler<IniciarSesionUsuarioCommand, BaseResponse<LoginResponse>>
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;

        public IniciarSesionUsuarioHandler(IAuthService authService, ITokenService tokenService, ITokenRepository tokenRepository)
        {
            _authService = authService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public async Task<BaseResponse<LoginResponse>> Handle(IniciarSesionUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new IniciarSesionUsuarioValidator();
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    var errores = result.Errors.Select(e => e.ErrorMessage).ToList();
                    return BaseResponse<LoginResponse>.Failure("Errores de validación", errores);
                }

                var response = await _authService.IniciarSesionAsync(
                    request.Correo,
                    request.Password);


                string token = _tokenService.GenerarToken(response);
                string refreshtoken = _tokenService.GenerarRefreshToken();
                var tokenEntidad = new RefreshToken
                {
                    Token = refreshtoken,
                    UsuarioId = response.Id,
                    FechaExpiracion = DateTime.UtcNow.AddDays(7),
                    EstaRevocado = false
                };


                await _tokenRepository.AddTokenAsync(tokenEntidad);
                await _tokenRepository.SaveChangesTokenAsync();

                var data = new LoginResponse
                {
                    Id = response.Id,
                    Nombre = response.Nombre,
                    Apellido = response.Apellido,
                    Correo = response.Correo,
                    Token = token,
                    RefreshToken = refreshtoken
                };

                return BaseResponse<LoginResponse>.Ok(data, "Login exitoso");
            }
            catch (Exception ex)
            {
                return BaseResponse<LoginResponse>.Failure($"Error al Iniciar Sesion: {ex.Message}");
            }
        }
    }
}
