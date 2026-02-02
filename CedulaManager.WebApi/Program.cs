using CedulaManager.Application;
using CedulaManager.Domain.Interfaces;
using CedulaManager.Infrastructure;
using CedulaManager.Infrastructure.Services;
using CedulaManager.Persistence;
using CedulaManager.Persistence.Contexts;
using CedulaManager.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var smtpUser = builder.Configuration["MailerSend:SmtpUser"];
var smtpPass = builder.Configuration["MailerSend:SmtpPass"];
var fromEmail = builder.Configuration["MailerSend:FromEmail"];
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");
if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass) || string.IsNullOrEmpty(fromEmail))
{
    throw new Exception("ERROR: Las credenciales SMTP no se encontraron en appsettings.json. Revisa la sección 'MailerSend'.");
}

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);

builder.Services.AddScoped<INotificacionService>(sp =>
    new NotificacionService(smtpUser, smtpPass, fromEmail));


builder.Services.AddAuthentication(
    option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(
    option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
            ValidAudience = jwtSettings.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ClockSkew = TimeSpan.Zero
        };
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IRecuperarContrasenaRepository, RecuperarContrasenaRepository>();
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        Console.WriteLine("--> ÉXITO: Base de Datos conectada y Migraciones aplicadas.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> ERROR: No se pudo conectar a la base de datos: {ex.Message}");
    }
}

app.UseCors("AllowViteApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
