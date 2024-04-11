using ColingShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class SolicitudService : ISolicitudService
    {
        private readonly HttpClient _httpClient;

        public SolicitudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> InsertarSolicitud(Solicitud solicitud, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("/api/insertarsolicitud", solicitud);
            return response.IsSuccessStatusCode;
        }

        public async Task<Solicitud> ObtenerSolicitudPorId(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"/api/listarsolicitudes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Solicitud>();
                return content!;
            }
            else
            {
                return new Solicitud();
            }
        }

        public async Task<bool> EditarSolicitud(string id, Solicitud solicitud, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"/api/modificarsolicitud/{id}", solicitud);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Solicitud>> ListaSolicitudes(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("/api/listarsolicitud");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<Solicitud>>();
                return content ?? new List<Solicitud>();
            }
            else
            {
                return new List<Solicitud>();
            }
        }

        public async Task<bool> BorrarSolicitud(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"/api/eliminarsolicitud/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
