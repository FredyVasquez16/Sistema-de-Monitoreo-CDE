namespace Aplicacion.Contratos;

public interface ICodigoUnicoGenerator
{
    string GenerarCodigo(string prefijo, int id);
}