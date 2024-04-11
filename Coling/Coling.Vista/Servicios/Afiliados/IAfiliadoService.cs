using ColingShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IAfiliadoService
    {
        Task<List<Afiliado>> ListarAfiliados(string token);
        Task<Afiliado> ObtenerAfiliadoPorId(int id, string token);
        Task<bool> InsertarAfiliado(Afiliado afiliado, string token);
        Task<bool> ModificarAfiliado(int afiliado, string token);
        Task<bool> EliminarAfiliado(int id, string token);
    }
}
