using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ColingShared;
using Newtonsoft.Json;

namespace Coling.Vista.Servicios
{
    public class DireccionService : IDireccionService
    {
        private readonly HttpClient _httpClient;

        public DireccionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Direccion>> ListaDirecciones(string token)
        {
            var endPoint = "http://localhost:7291/api/listadirecciones";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
            List<Direccion> result = new List<Direccion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Direccion>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<Direccion> ObtenerDireccionPorId(int id, string token)
        {
            var endPoint = $"http://localhost:7291/api/listadirecciones/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
            Direccion result = new Direccion();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Direccion>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> InsertarDireccion(Direccion direccion, string token)
        {
            var endPoint = "http://localhost:7291/api/insertardireccion";
            var jsonBody = JsonConvert.SerializeObject(direccion);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await _httpClient.PostAsync(endPoint, content);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, string token)
        {
            var endPoint = "http://localhost:7291/api/modificardireccion";
            var jsonBody = JsonConvert.SerializeObject(direccion);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await _httpClient.PutAsync(endPoint, content);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarDireccion(int id, string token)
        {
            var endPoint = $"http://localhost:7291/api/eliminardireccion/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await _httpClient.DeleteAsync(endPoint);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
