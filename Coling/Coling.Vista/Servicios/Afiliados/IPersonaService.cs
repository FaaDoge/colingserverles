using ColingShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IPersonaService
    {
        Task<List<Persona>> ListaPersonas(string token);
        Task<Persona> ObtenerPersonaPorId(int id, string token);
        Task<bool> InsertarPersona(Persona persona, string token);
        Task<bool> EditarPersona(int id, string token);
        Task<bool> BorrarPersona(int id, string token);
    }
}
