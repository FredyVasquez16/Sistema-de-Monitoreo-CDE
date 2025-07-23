using System.Text;
using Aplicacion.Asesorias;
using Aplicacion.Capacitaciones;
using Aplicacion.ClientesEmpresas;
using Aplicacion.Contactos;
using Aplicacion.Contratos;
using Aplicacion.Seguridad;
using Aplicacion.Services;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Seguridad.Token;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SistemaMonitoreaCdeContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddMediatR(typeof(Consulta.Manejador).Assembly);
builder.Services.AddMediatR(typeof(ClienteEmpresaGet.Manejador).Assembly);
builder.Services.AddMediatR(typeof(CapacitacionGet.Manejador).Assembly);
builder.Services.AddMediatR(typeof(AsesoriaGet.Manejador).Assembly);

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ContactoCreate>());
/*builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ContactoCreate>());*/ // Va a agregar seguridad a todos los controladores, si se quiere agregar a un controlador en específico se debe agregar el atributo [Authorize] al controlador
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ClienteEmpresaCreate>());
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CapacitacionCreate>());
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AsesoriaCreate>());
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Login>());

var contructor = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(contructor.UserType, contructor.Services);
identityBuilder.AddEntityFrameworkStores<SistemaMonitoreaCdeContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("T7QdU8+wCBgbefv6iY5QRUo5FbRnbatA7NO5IK56zjDp5jWDZb9F36JqCWAL/IzD1XDhV+q3kDUKkZo59IdT4Q==\n"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false, // No se valida el público, se puede cambiar si se desea, se puede agregar la ip del cliente que va a estar autorizado para crear tockens
            ValidateIssuer = false
        };
    });

builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();
builder.Services.AddScoped<IUsuarioSesion, UsuarioSesion>();
builder.Services.AddScoped<ICodigoUnicoGenerator, CodigoUnicoGenerator>();
builder.Services.AddAutoMapper(typeof(AsesoriaGet.Manejador));
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var ambiente = app.Services.CreateScope())
{
    var services = ambiente.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var context = services.GetRequiredService<SistemaMonitoreaCdeContext>();
        context.Database.Migrate();
        DataPrueba.InsertarData(context, userManager).Wait();
    }
    catch (Exception e)
    {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Error al migrar la base de datos");
    }
    
}


// Configure the HTTP request pipeline.
app.UseMiddleware<ManejadorErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.MapPost("/controlador")

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
