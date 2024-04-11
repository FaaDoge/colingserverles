using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IEstudiosServices
    {
        Task<List<Estudios>> Listar(string token);
        Task<bool> Insertar(Estudios Estudios, string token);
        Task<bool> Editar(Estudios Estudios, string token);
        Task<Estudios> ListaID(string id, string token);
        Task<bool> Borrar(string PartitionKey, string RowKey, string token);
    }
}
