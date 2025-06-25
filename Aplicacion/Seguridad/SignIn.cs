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

public class SignIn
{
    public class SignInEjecuta : IRequest<UsuarioData>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
    
    public class EjecutaValidacion : AbstractValidator<SignInEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email es obligatorio y debe ser válido");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("La contraseña es obligatoria y debe tener al menos 6 caracteres");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
        }
    }
    
    public class Manejador : IRequestHandler<SignInEjecuta, UsuarioData>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly ICodigoUnicoGenerator _codigoUnicoGenerator;
        
        public Manejador(SistemaMonitoreaCdeContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, ICodigoUnicoGenerator codigoUnicoGenerator)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _codigoUnicoGenerator = codigoUnicoGenerator;
        }

        public async Task<UsuarioData> Handle(SignInEjecuta request, CancellationToken cancellationToken)
        {
            // Validar si el usuario ya existe
            var usuarioExistente = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
            if (usuarioExistente)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya exite un usuario registrado con este Email" });
            }
            
            // Validar si el nombre de usuario ya existe
            var userNameExistente = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
            if (userNameExistente)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya existe un usuario registrado con este nombre de usuario" });
            }
            
            // Crear el nuevo usuario
            var usuario = new Usuario
            {
                NombreCompleto = $"{request.Nombre} {request.Apellido}",
                Email = request.Email,
                UserName = request.UserName
            };
            
            var ultimoCodigo = await _context.Users
                .Where(u => u.CodigoUnico.StartsWith("CDE-US-"))
                .OrderByDescending(u => u.CodigoUnico)
                .Select(u => u.CodigoUnico)
                .FirstOrDefaultAsync();

            int nuevoNumero = 1;
            if (!string.IsNullOrEmpty(ultimoCodigo))
            {
                var partes = ultimoCodigo.Split('-');
                int.TryParse(partes.Last(), out nuevoNumero);
                nuevoNumero++;
            }
            usuario.CodigoUnico = $"CDE-US-{nuevoNumero:D4}";

            var resultado = await _userManager.CreateAsync(usuario, request.Password);
            if (resultado.Succeeded)
            {
                return new UsuarioData
                {
                    CodigoUnico = usuario.CodigoUnico,
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuario),
                    UserName = usuario.UserName,
                    Email = usuario.Email
                };
            }
            else
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Error al crear el usuario" });
            }
        }
    }
}