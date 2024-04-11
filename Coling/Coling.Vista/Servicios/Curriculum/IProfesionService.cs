using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IProfesionService
    {
        Task<List<Profesion>> Listar(string token);
        Task<bool> Insertar(Profesion Profesion, string token);
        Task<bool> Editar(Profesion Profesion, string token);
        Task<Profesion> ListaID(string id, string token);
        Task<bool> Borrar(string PartitionKey, string RowKey, string token);
    }
}
