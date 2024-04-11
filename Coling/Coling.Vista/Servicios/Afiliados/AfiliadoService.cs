using ColingShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Coling.Vista.Servicios.Afiliados
{
    public class AfiliadoService : IAfiliadoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:7291";

        public AfiliadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }

        public async Task<List<Afiliado>> ListarAfiliados(string token)
        {
            string endPoint = "/api/listarafiliados";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
            List<Afiliado> result = new List<Afiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Afiliado>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<Afiliado> ObtenerAfiliadoPorId(int id, string token)
        {
            string endPoint = $"/api/listarafiliados/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
            Afiliado result = new Afiliado();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Afiliado>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado, string token)
        {
            string endPoint = "/api/insertarafiliado";
            string jsonBody = JsonConvert.SerializeObject(afiliado);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endPoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ModificarAfiliado(int afiliado, string token)
        {
            string endPoint = "/api/modificarafiliado";
            string jsonBody = JsonConvert.SerializeObject(afiliado);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(endPoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAfiliado(int id, string token)
        {
            string endPoint = $"/api/eliminarafiliado/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync(endPoint);
            return response.IsSuccessStatusCode;
        }
    }
}
