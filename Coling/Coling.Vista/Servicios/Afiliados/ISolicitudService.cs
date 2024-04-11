using ColingShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface ISolicitudService
    {
        Task<bool> InsertarSolicitud(Solicitud solicitud, string token);
        Task<Solicitud> ObtenerSolicitudPorId(string id, string token);
        Task<bool> EditarSolicitud(string id, Solicitud solicitud, string token);
        Task<List<Solicitud>> ListaSolicitudes(string token);
        Task<bool> BorrarSolicitud(string id, string token);
    }
}
