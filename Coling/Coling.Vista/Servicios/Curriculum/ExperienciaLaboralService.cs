using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;
using Newtonsoft.Json;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ExperienciaLaboralService : IExperienciaLaboralService
    {
        string url = "http://localhost:7127";
        string endPoint = "";
        private readonly HttpClient client;
        public ExperienciaLaboralService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }

        public async Task<List<ExperienciaLaboral>> Listar(string token)
        {
            endPoint = "/api/ListarExperienciaLaboral";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<ExperienciaLaboral> result = new List<ExperienciaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExperienciaLaboral>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<ExperienciaLaboral> ListaID(string id, string token)
        {
            endPoint = url + "/api/listarExperienciaLaborals/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            ExperienciaLaboral result = new ExperienciaLaboral();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ExperienciaLaboral>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Insertar(ExperienciaLaboral ExperienciaLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarExperienciaLaboral";
            string jsonBody = JsonConvert.SerializeObject(ExperienciaLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Borrar(string PartitionKey, string RowKey, string token)
        {
            bool sw = false;
            endPoint = url + "/api/eliminarExperienciaLaboral/" + PartitionKey + "," + RowKey + "";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Editar(ExperienciaLaboral ExperienciaLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "/api/modificarExperienciaLaboral";
            string jsonBody = JsonConvert.SerializeObject(ExperienciaLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
    }
}
