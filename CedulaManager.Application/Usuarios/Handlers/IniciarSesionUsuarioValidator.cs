using CedulaManager.Application.Command;
using CedulaManager.Application.Query;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Usuarios.Handlers
{
    public class IniciarSesionUsuarioValidator: AbstractValidator<IniciarSesionUsuarioCommand>
    {
        public IniciarSesionUsuarioValidator()
        {
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
