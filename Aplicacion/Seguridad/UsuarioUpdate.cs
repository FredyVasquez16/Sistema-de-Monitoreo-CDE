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
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    
    public class EjecutaValidador : AbstractValidator<EjecutarUsuarioUpdate>
    {
        public EjecutaValidador()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Apellidos).NotEmpty().WithMessage("Los apellidos son obligatorios");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email es obligatorio y debe ser válido");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("La contraseña es obligatoria y debe tener al menos 6 caracteres");
        }
    }
    
    public class Manejador : IRequestHandler<EjecutarUsuarioUpdate, UsuarioData>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, SistemaMonitoreaCdeContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioData> Handle(EjecutarUsuarioUpdate request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(request.UserName);
            if (usuario == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, "Usuario no encontrado");
            }
            
            // Verificar si el email ya está en uso por otro usuario
            var emailExistente = await _userManager.Users.Where(x => x.Email == request.Email && x.UserName != request.UserName)
                .AnyAsync(cancellationToken);
            if (emailExistente)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest, "Ya existe un usuario registrado con este Email");
            }

            usuario.NombreCompleto = $"{request.Nombre} {request.Apellidos}";
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, request.Password);
            usuario.UserName = request.UserName;
            usuario.Email = request.Email;

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

            throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError, "No se pudo actualizar el usuario");
        }
    }
}