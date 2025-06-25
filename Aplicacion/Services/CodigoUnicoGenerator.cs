using Aplicacion.Contratos;

namespace Aplicacion.Services;

public class CodigoUnicoGenerator : ICodigoUnicoGenerator
{
    public string GenerarCodigo(string prefijo, int id)
    {
        return $"CDE-{prefijo}-{id:D4}";
    }
}