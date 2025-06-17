using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Persistencia
{
    public class SistemaMonitoreaCdeContextFactory : IDesignTimeDbContextFactory<SistemaMonitoreaCdeContext>
    {
        public SistemaMonitoreaCdeContext CreateDbContext(string[] args)
        {
            // Ruta raíz del proyecto WebAPI para leer appsettings.json
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SistemaMonitoreaCdeContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new SistemaMonitoreaCdeContext(optionsBuilder.Options);
        }
    }
}