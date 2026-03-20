using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad;

public class UsuarioUpdate
{
    public class EjecutarUsuarioUpdate : IRequest<UsuarioData>
    {
        public string Nombrecompleto { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; } 
    }
    
    public class EjecutaValidador : AbstractValidator<EjecutarUsuarioUpdate>
    {
        public EjecutaValidador()
        {
            RuleFor(x => x.Nombrecompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("El nombre completo solo puede contener letras y espacios");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email es obligatorio y debe ser válido");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Password)); // La clave: la regla se activa solo bajo esta condición.
        }
    }
    
    public class Manejador : IRequestHandler<EjecutarUsuarioUpdate, UsuarioData>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IUsuarioSesion _usuarioSesion;

        public Manejador(
            UserManager<Usuario> userManager, 
            IJwtGenerador jwtGenerador, 
            SistemaMonitoreaCdeContext context, 
            IPasswordHasher<Usuario> passwordHasher,
            IUsuarioSesion usuarioSesion
        )
        {
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _context = context;
            _passwordHasher = passwordHasher;
            _usuarioSesion = usuarioSesion;
        }

        public async Task<UsuarioData> Handle(EjecutarUsuarioUpdate request, CancellationToken cancellationToken)
        {
            var userNameActual = _usuarioSesion.ObtenerUsuarioSesion();
            if (userNameActual == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "No se pudo obtener la sesión del usuario." });
            }

            var usuario = await _userManager.FindByNameAsync(userNameActual);
            
            if (usuario == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El usuario de la sesión actual no fue encontrado en la base de datos." });
            }
            
            if (usuario.Email != request.Email && await _context.Users.AnyAsync(x => x.Email == request.Email))
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El email ya está en uso por otro usuario." });
            }
            
            if (usuario.UserName != request.UserName && await _context.Users.AnyAsync(x => x.UserName == request.UserName))
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El UserName ya está en uso por otro usuario." });
            }

            usuario.NombreCompleto = request.Nombrecompleto;
            usuario.Email = request.Email;
            usuario.UserName = request.UserName;
            
            if (!string.IsNullOrEmpty(request.Password))
            {
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, request.Password);
            }
            
            var resultado = await _userManager.UpdateAsync(usuario);
            var resultadoRoles = await _userManager.GetRolesAsync(usuario);
            var listaRoles = new List<string>(resultadoRoles);
            
            if (resultado.Succeeded)
            {
                return new UsuarioData
                {
                    CodigoUnico = usuario.CodigoUnico,
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                    Email = usuario.Email,
                    UserName = usuario.UserName
                };
            }

            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo actualizar el usuario" });
        }
    }
}