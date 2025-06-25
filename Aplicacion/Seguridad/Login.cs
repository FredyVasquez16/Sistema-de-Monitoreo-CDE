using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class Login
{
    public class LoginEjecuta : IRequest<UsuarioData>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class EjecutaValidacion : AbstractValidator<LoginEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria");
        }
    }
    
    public class Manejador : IRequestHandler<LoginEjecuta, UsuarioData>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGenerador _jwtGenerador;
        
        public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerador = jwtGenerador;
        }
        public async Task<UsuarioData> Handle(LoginEjecuta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized,
                    new { mensaje = "Email o contraseña incorrectos" });
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
            if (resultado.Succeeded)
            {
                return new UsuarioData
                {
                    CodigoUnico = usuario.CodigoUnico,
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuario),
                    Email = usuario.Email,
                    UserName = usuario.UserName
                };
            }
            else
            {
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized,
                    new { mensaje = "Email o contraseña incorrectos" });
            }
        }
    }
}