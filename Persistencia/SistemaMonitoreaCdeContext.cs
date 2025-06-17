using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class SistemaMonitoreaCdeContext : IdentityDbContext<Usuario>
{
    public SistemaMonitoreaCdeContext(DbContextOptions options) : base(options) {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Convención global para nombres en minúsculas con guiones bajos
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().ToLower());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToLower());
            }

            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(fk.GetConstraintName().ToLower());
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToLower());
            }
        }
        

        // Relación ClienteEmpresa.ContactoPrimario
        modelBuilder.Entity<ClientesEmpresas>()
            .HasOne(c => c.ContactoPrimario)
            .WithMany()
            .HasForeignKey(c => c.ContactoPrimarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación ClienteEmpresa.NombrePropietario
        modelBuilder.Entity<ClientesEmpresas>()
            .HasOne(c => c.NombrePropietario)
            .WithMany()
            .HasForeignKey(c => c.NombrePropietarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Contactos.ClienteEmpresa
        modelBuilder.Entity<Contactos>()
            .HasOne(c => c.ClienteEmpresa)
            .WithMany(c => c.Contactos)
            .HasForeignKey(c => c.EmpresaClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientesEmpresas>()
            .HasOne(c => c.FuenteFinanciamiento)
            .WithMany(f => f.ClientesEmpresasFuente)
            .HasForeignKey(c => c.FuenteFinanciamientoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ClientesEmpresas>()
            .HasOne(c => c.SubFuenteFinanciamiento)
            .WithMany(f => f.ClientesEmpresasSubfuente)
            .HasForeignKey(c => c.SubFuenteFinanciamientoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ClientesEmpresas>()
            .HasOne(c => c.NivelFormalizacion)
            .WithMany(n => n.ClientesEmpresas)  // Agrega esta línea
            .HasForeignKey(c => c.NivelFormalizacionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Asesorias>()
            .HasOne(a => a.TiposContacto)
            .WithMany(t => t.Asesoria)
            .HasForeignKey(a => a.TipoContactoId)
            .HasConstraintName("fk_asesorias_tipo_contacto")
            .OnDelete(DeleteBehavior.Restrict);
        
        /*modelBuilder.Entity<AsesoriasAsesores>()
            .HasOne(aa => aa.Asesoria)
            .WithMany(a => a.Asesores)
            .HasForeignKey(aa => aa.AsesoriaId);

        modelBuilder.Entity<AsesoriasAsesores>()
            .HasOne(aa => aa.Asesor)
            .WithMany() // Opcional: podrías poner `WithMany(u => u.AsesoriasAsesores)` si quieres la colección inversa en Usuario.
            .HasForeignKey(aa => aa.AsesorId);*/
    }
    
    public DbSet<AreasAsesoria> AreasAsesorias { get; set; }
    public DbSet<AsesoresClientesEmpresas> AsesoresClientesEmpresas { get; set; }
    public DbSet<Asesorias> Asesorias { get; set; }
    public DbSet<AsesoriasArchivos> AsesoriasArchivos { get; set; }
    public DbSet<AsesoriasAsesores> AsesoriasAsesores { get; set; }
    public DbSet<AsesoriasContactos> AsesoriasContactos { get; set; }
    public DbSet<Capacitaciones> Capacitaciones { get; set; }
    public DbSet<CapacitacionesArchivos> CapacitacionesArchivos { get; set; }
    public DbSet<CapacitacionesTemas> CapacitacionesTemas { get; set; }
    public DbSet<CategoriasLaborales> CategoriasLaborales { get; set; }
    public DbSet<ClientesEmpresas> ClientesEmpresas { get; set; }
    public DbSet<Contactos> Contactos { get; set; }
    public DbSet<Cuotas> Cuotas { get; set; }
    public DbSet<EstadosCiviles> EstadosCiviles { get; set; }
    public DbSet<FormatosPrograma> FormatosProgramas { get; set; }
    public DbSet<FuenteFinanciamiento> FuenteFinanciamientos { get; set; }
    public DbSet<NivelesEstudio> NivelesEstudios { get; set; }
    public DbSet<NivelesFormalizacion> NivelesFormalizaciones { get; set; }
    public DbSet<ServiciosSolicitados> ServiciosSolicitados { get; set; }
    public DbSet<Sesiones> Sesiones { get; set; }
    public DbSet<SesionesCapacitacion> SesionesCapacitaciones { get; set; }
    public DbSet<SesionesParticipantes> SesionesParticipantes { get; set; }
    public DbSet<TamanoEmpresas> TamanoEmpresas { get; set; }
    public DbSet<Temas> Temas { get; set; }
    public DbSet<Tipos> Tipos { get; set; }
    public DbSet<TiposClienteNivel> TiposClienteNiveles { get; set; }
    public DbSet<TiposClientesEstado> TiposClientesEstados { get; set; }
    public DbSet<TiposComerciosInternacional> TiposComerciosInternacionales { get; set; }
    public DbSet<TiposContabilidad> TiposContabilidades { get; set; }
    public DbSet<TiposContacto> TiposContactos { get; set; }
    public DbSet<TiposEmpresa> TiposEmpresas { get; set; }
    public DbSet<TiposOrganizacion> TiposOrganizaciones { get; set; }
   // public DbSet<Usuario> Usuarios { get; set; }
    
}