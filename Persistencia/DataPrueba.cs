using Dominio;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        // El método principal ahora también necesita RoleManager
        public static async Task InsertarData(SistemaMonitoreaCdeContext context, UserManager<Usuario> usuarioManager, RoleManager<IdentityRole> roleManager)
        {
            // Verificamos si ya hay datos para evitar duplicados en ejecuciones futuras
            if (!context.Roles.Any())
            {
                await CrearRoles(roleManager);
            }
            if (!usuarioManager.Users.Any())
            {
                await CrearUsuarioInicial(usuarioManager);
            }
            if (!context.NivelesEstudios.Any())
            {
                await InsertarNivelesEstudio(context);
            }
            if (!context.CategoriasLaborales.Any())
            {
                await InsertarCategoriasLaborales(context);
            }
            if (!context.TiposOrganizaciones.Any())
            {
                await InsertarTiposOrganizacion(context);
            }
            if (!context.TiposEmpresas.Any())
            {
                await InsertarTiposEmpresa(context);
            }
            if (!context.TamanoEmpresas.Any())
            {
                await InsertarTamanosEmpresa(context);
            }
            if (!context.TiposContabilidades.Any())
            {
                await InsertarTiposContabilidad(context);
            }
            if (!context.NivelesFormalizaciones.Any())
            {
                await InsertarNivelesFormalizacion(context);
            }
            if (!context.TiposComerciosInternacionales.Any())
            {
                await InsertarTiposComercioInternacional(context);
            }
            if (!context.FuenteFinanciamientos.Any())
            {
                await InsertarFuentesFinanciamiento(context);
            }
            if (!context.AreasAsesorias.Any())
            {
                await InsertarAreasAsesoria(context);
            }
            if (!context.EstadosCiviles.Any())
            {
                await InsertarEstadosCiviles(context);
            }
            if (!context.FormatosProgramas.Any())
            {
                await InsertarFormatosPrograma(context);
            }
            if (!context.ServiciosSolicitados.Any())
            {
                await InsertarServiciosSolicitados(context);
            }
            if (!context.Temas.Any())
            {
                await InsertarTemas(context);
            }
            if (!context.Tipos.Any())
            {
                await InsertarTiposCapacitacionEvento(context);
            }
            if (!context.TiposClienteNiveles.Any())
            {
                await InsertarTiposClienteNivel(context);
            }
            if (!context.TiposClientesEstados.Any())
            {
                await InsertarTiposClienteEstado(context);
            }
            if (!context.TiposContactos.Any())
            {
                await InsertarTiposContacto(context);
            }
        }

        private static async Task CrearRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Asesor"));
            await roleManager.CreateAsync(new IdentityRole("Administrador"));
        }

        private static async Task CrearUsuarioInicial(UserManager<Usuario> usuarioManager)
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Fredy Vasquez (Asesor)",
                UserName = "fredy16v",
                Email = "fredy16v@gmail.com",
                CodigoUnico = "CDE-US-0001"
            };
            var resultado = await usuarioManager.CreateAsync(usuario, "Test123@");
            if (resultado.Succeeded)
            {
                await usuarioManager.AddToRoleAsync(usuario, "Administrador");
            }
        }

        private static async Task InsertarNivelesEstudio(SistemaMonitoreaCdeContext context)
        {
            var niveles = new List<NivelesEstudio>
            {
                new NivelesEstudio { Descripcion = "Primaria" },
                new NivelesEstudio { Descripcion = "Secundaria" },
                new NivelesEstudio { Descripcion = "Técnico Superior" },
                new NivelesEstudio { Descripcion = "Universitario (Licenciatura / Ingeniería)" },
                new NivelesEstudio { Descripcion = "Postgrado" },
                new NivelesEstudio { Descripcion = "Sin Estudios" }
            };
            await context.NivelesEstudios.AddRangeAsync(niveles);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarCategoriasLaborales(SistemaMonitoreaCdeContext context)
        {
            var categorias = new List<CategoriasLaborales>
            {
                new CategoriasLaborales { Descripcion = "Empresario / Dueño de Negocio" },
                new CategoriasLaborales { Descripcion = "Empleado (Público o Privado)" },
                new CategoriasLaborales { Descripcion = "Trabajador Independiente" },
                new CategoriasLaborales { Descripcion = "Emprendedor (con idea de negocio)" },
                new CategoriasLaborales { Descripcion = "Estudiante" },
                new CategoriasLaborales { Descripcion = "Desempleado/a" }
            };
            await context.CategoriasLaborales.AddRangeAsync(categorias);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarTiposOrganizacion(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<TiposOrganizacion>
            {
                new TiposOrganizacion { Descripcion = "Persona Natural" },
                new TiposOrganizacion { Descripcion = "Empresa Individual de Responsabilidad Limitada" },
                new TiposOrganizacion { Descripcion = "Sociedad de Responsabilidad Limitada" },
                new TiposOrganizacion { Descripcion = "Sociedad por Acciones" },
                new TiposOrganizacion { Descripcion = "Sociedad Anónima" },
                new TiposOrganizacion { Descripcion = "Cooperativa" },
                new TiposOrganizacion { Descripcion = "Organización sin fines de lucro" },
                new TiposOrganizacion { Descripcion = "Negocio Informal" }
            };
            await context.TiposOrganizaciones.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarTiposEmpresa(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<TiposEmpresa>
            {
                new TiposEmpresa { Descripcion = "Comercio" },
                new TiposEmpresa { Descripcion = "Servicios" },
                new TiposEmpresa { Descripcion = "Turismo" },
                new TiposEmpresa { Descripcion = "Manufactura / Industria" },
                new TiposEmpresa { Descripcion = "Agricultura / Agropecuario" },
                new TiposEmpresa { Descripcion = "Construcción" },
                new TiposEmpresa { Descripcion = "Tecnología" }
            };
            await context.TiposEmpresas.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarTamanosEmpresa(SistemaMonitoreaCdeContext context)
        {
            var tamanos = new List<TamanoEmpresas>
            {
                new TamanoEmpresas { Descripcion = "Emprendedor (Negocio Informal)" },
                new TamanoEmpresas { Descripcion = "Micro Empresa" },
                new TamanoEmpresas { Descripcion = "Pequeña Empresa" },
                new TamanoEmpresas { Descripcion = "Mediana Empresa" },
                new TamanoEmpresas { Descripcion = "Grande Empresa" },
                new TamanoEmpresas { Descripcion = "Sin Ventas (No Aplica)" }
            };
            await context.TamanoEmpresas.AddRangeAsync(tamanos);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarTiposContabilidad(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<TiposContabilidad>
            {
                new TiposContabilidad { Descripcion = "Contabilidad Completa" },
                new TiposContabilidad { Descripcion = "Contabilidad Simplificada" },
                new TiposContabilidad { Descripcion = "No Lleva Contabilidad" },
                new TiposContabilidad { Descripcion = "No Aplica" }
            };
            await context.TiposContabilidades.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarNivelesFormalizacion(SistemaMonitoreaCdeContext context)
        {
            var niveles = new List<NivelesFormalizacion>
            {
                new NivelesFormalizacion { Descripcion = "Idea de Negocio" },
                new NivelesFormalizacion { Descripcion = "Negocio Informal" },
                new NivelesFormalizacion { Descripcion = "En Proceso de Formalización" },
                new NivelesFormalizacion { Descripcion = "Formalizado" }
            };
            await context.NivelesFormalizaciones.AddRangeAsync(niveles);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTiposComercioInternacional(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<TiposComerciosInternacional>
            {
                new TiposComerciosInternacional { Descripcion = "Exportación" },
                new TiposComerciosInternacional { Descripcion = "Importación" },
                new TiposComerciosInternacional { Descripcion = "Ambos (Exportación e Importación)" },
                new TiposComerciosInternacional { Descripcion = "Ninguno" }
            };
            await context.TiposComerciosInternacionales.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }

        private static async Task InsertarFuentesFinanciamiento(SistemaMonitoreaCdeContext context)
        {
            var fuentes = new List<FuenteFinanciamiento>
            {
                new FuenteFinanciamiento { Descripcion = "Recursos Propios" },
                new FuenteFinanciamiento { Descripcion = "Crédito Bancario" },
                new FuenteFinanciamiento { Descripcion = "Crédito de Microfinanciera" },
                new FuenteFinanciamiento { Descripcion = "Crédito de Cooperativa / Caja Rural" },
                new FuenteFinanciamiento { Descripcion = "Fondos Públicos / Subsidios" },
                new FuenteFinanciamiento { Descripcion = "Inversión Privada" },
                new FuenteFinanciamiento { Descripcion = "No Aplica" }
            };
            await context.FuenteFinanciamientos.AddRangeAsync(fuentes);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarAreasAsesoria(SistemaMonitoreaCdeContext context)
        {
            var areas = new List<AreasAsesoria>
            {
                new AreasAsesoria { Descripcion = "Gestión Empresarial y Estrategia" },
                new AreasAsesoria { Descripcion = "Finanzas y Contabilidad" },
                new AreasAsesoria { Descripcion = "Marketing y Ventas" },
                new AreasAsesoria { Descripcion = "Operaciones y Producción" },
                new AreasAsesoria { Descripcion = "Legal y Formalización" },
                new AreasAsesoria { Descripcion = "Recursos Humanos" },
                new AreasAsesoria { Descripcion = "Tecnología e Informática (TICs)" },
                new AreasAsesoria { Descripcion = "Empresarialidad Femenina" },
                new AreasAsesoria { Descripcion = "Comercio Internacional" }
            };
            await context.AreasAsesorias.AddRangeAsync(areas);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarEstadosCiviles(SistemaMonitoreaCdeContext context)
        {
            var estados = new List<EstadosCiviles>
            {
                new EstadosCiviles { Descripcion = "Soltero/a" },
                new EstadosCiviles { Descripcion = "Casado/a" },
                new EstadosCiviles { Descripcion = "Unión Libre / Conviviente" },
                new EstadosCiviles { Descripcion = "Divorciado/a" },
                new EstadosCiviles { Descripcion = "Viudo/a" },
                new EstadosCiviles { Descripcion = "No especifica" }
            };
            await context.EstadosCiviles.AddRangeAsync(estados);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarFormatosPrograma(SistemaMonitoreaCdeContext context)
        {
            var formatos = new List<FormatosPrograma>
            {
                new FormatosPrograma { Descripcion = "Curso (Charla o Taller)" },
                new FormatosPrograma { Descripcion = "Seminario" },
                new FormatosPrograma { Descripcion = "Tele-conferencia (Vídeo Conferencia)" },
                new FormatosPrograma { Descripcion = "Curso en Línea (Capacitación Virtual)" }
            };
            await context.FormatosProgramas.AddRangeAsync(formatos);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarServiciosSolicitados(SistemaMonitoreaCdeContext context)
        {
            var servicios = new List<ServiciosSolicitados>
            {
                new ServiciosSolicitados { Descripcion = "Asistencia Técnica" },
                new ServiciosSolicitados { Descripcion = "Capacitación y Formación Empresarial" },
                new ServiciosSolicitados { Descripcion = "Asesoría Legal y Formalización" },
                new ServiciosSolicitados { Descripcion = "Asesoría en Tecnologías (TICs)" },
                new ServiciosSolicitados { Descripcion = "Vinculación a Mercados y Comercialización" },
                new ServiciosSolicitados { Descripcion = "Asesoría y Gestión Financiera" },
                new ServiciosSolicitados { Descripcion = "Desarrollo y Diversificación de Producto" },
                new ServiciosSolicitados { Descripcion = "Empresarialidad Femenina" }
            };
            await context.ServiciosSolicitados.AddRangeAsync(servicios);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTemas(SistemaMonitoreaCdeContext context)
        {
            var temas = new List<Temas>
            {
                // Gestión y Estrategia
                new Temas { Descripcion = "Administración de Empresas" },
                new Temas { Descripcion = "Iniciar un negocio" },
                new Temas { Descripcion = "Plan de Negocios" },
                new Temas { Descripcion = "Gestión de Personal" },
                new Temas { Descripcion = "Compra-Venta de Negocios" },
                new Temas { Descripcion = "Franquicias" },

                // Finanzas y Contabilidad
                new Temas { Descripcion = "Contabilidad y Presupuestos" },
                new Temas { Descripcion = "Financiación de Empresas" },
                new Temas { Descripcion = "Flujo de Fondos" },
                new Temas { Descripcion = "Asistencia para el Pago de Impuestos" },

                // Marketing y Comercialización
                new Temas { Descripcion = "Mercadotecnia y Ventas" },
                new Temas { Descripcion = "Relaciones Públicas" },
                new Temas { Descripcion = "Proveeduría y Compras" },
                new Temas { Descripcion = "Comercio Internacional" },

                // Digital y Tecnología
                new Temas { Descripcion = "Comercio Electrónico" },
                new Temas { Descripcion = "Social Media" },
                new Temas { Descripcion = "Tecnología" },
        
                // Otros
                new Temas { Descripcion = "Temas Legales" },
                new Temas { Descripcion = "Empresa liderada por una mujer" }
            };
            await context.Temas.AddRangeAsync(temas);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTiposCapacitacionEvento(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<Tipos>
            {
                new Tipos { Descripcion = "Capacitación" },
                new Tipos { Descripcion = "Evento" }
            };
            await context.Tipos.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTiposClienteNivel(SistemaMonitoreaCdeContext context)
        {
            var niveles = new List<TiposClienteNivel>
            {
                new TiposClienteNivel { Descripcion = "Nivel 1" },
                new TiposClienteNivel { Descripcion = "Nivel 2" },
                new TiposClienteNivel { Descripcion = "Nivel 3" }
            };
            await context.TiposClienteNiveles.AddRangeAsync(niveles);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTiposClienteEstado(SistemaMonitoreaCdeContext context)
        {
            var estados = new List<TiposClientesEstado>
            {
                new TiposClientesEstado { Descripcion = "Activo" },
                new TiposClientesEstado { Descripcion = "Inactivo" },
                new TiposClientesEstado { Descripcion = "Retirado" }
            };
            await context.TiposClientesEstados.AddRangeAsync(estados);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarTiposContacto(SistemaMonitoreaCdeContext context)
        {
            var tipos = new List<TiposContacto>
            {
                // Presencial
                new TiposContacto { Descripcion = "En Centro" },
                new TiposContacto { Descripcion = "En Empresa" },

                // No Presencial
                new TiposContacto { Descripcion = "Correo Electrónico" },
                new TiposContacto { Descripcion = "En Línea / Virtual" },
                new TiposContacto { Descripcion = "Telefónico" }
            };
            await context.TiposContactos.AddRangeAsync(tipos);
            await context.SaveChangesAsync();
        }
        
        private static async Task InsertarUnidades(SistemaMonitoreaCdeContext context)
        {
            var unidades = new List<Unidad>
            {
                new Unidad { Descripcion = "Unidad de Asistencia Técnica" },
                new Unidad { Descripcion = "Unidad de Formación Empresarial" },
                new Unidad { Descripcion = "Unidad de Legalización Empresarial" },
                new Unidad { Descripcion = "Unidad de Desarrollo Económico Local" },
                new Unidad { Descripcion = "Unidad de Empresarialidad Femenina" },
                new Unidad { Descripcion = "Unidad de Asesoría y Gestión Financiera" },
                new Unidad { Descripcion = "Unidad de Inteligencia de Mercado" },
                new Unidad { Descripcion = "Unidad de Especialización y Diversificación Productiva" },
                new Unidad { Descripcion = "Unidad de Promoción y Visibilidad" },
                new Unidad { Descripcion = "Unidad de Tecnologías de la Información (TIC)" }
            };
            await context.Unidades.AddRangeAsync(unidades);
            await context.SaveChangesAsync();
        }
    }
}