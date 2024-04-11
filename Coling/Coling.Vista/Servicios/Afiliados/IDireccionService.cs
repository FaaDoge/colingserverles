using ColingShared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios
{
    public interface IDireccionService
    {
        Task<List<Direccion>> ListaDirecciones(string token);
        Task<Direccion> ObtenerDireccionPorId(int id, string token);
        Task<bool> InsertarDireccion(Direccion direccion, string token);
        Task<bool> ModificarDireccion(Direccion direccion, string token);
        Task<bool> EliminarDireccion(int id, string token);
    }
}
