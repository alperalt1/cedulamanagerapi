using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Infrastructure.Services
{
    public class TokenService: ITokenService
    {
        public readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        { 
            _config = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            try
            {
                var secretKey = _config["JwtSettings:SecretKey"];
                var issuer = _config["JwtSettings:Issuer"];
                var audience = _config["JwtSettings:Audience"];
                var minutes = double.Parse(_config["JwtSettings:DurationInMinutes"] ?? "60");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, usuario.Correo!),
                    new Claim("nombre", usuario.Nombre!)
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(minutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico en TokenService: {ex.Message}");
                throw;
            }
        }

        public string GenerarRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
