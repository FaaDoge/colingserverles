using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IExperienciaLaboralService
    {
        Task<List<ExperienciaLaboral>> Listar(string token);
        Task<bool> Insertar(ExperienciaLaboral ExperienciaLaboral, string token);
        Task<bool> Editar(ExperienciaLaboral ExperienciaLaboral, string token);
        Task<ExperienciaLaboral> ListaID(string id, string token);
        Task<bool> Borrar(string PartitionKey, string RowKey, string token);
    }
}
