using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;
using Newtonsoft.Json;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ProfesionService : IProfesionService
    {
        string url = "http://localhost:7127";
        string endPoint = "";
        private readonly HttpClient client;
        public ProfesionService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }

        public async Task<List<Profesion>> Listar(string token)
        {
            endPoint = "/api/ListarProfesion";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Profesion> result = new List<Profesion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<Profesion> ListaID(string id, string token)
        {
            endPoint = url + "/api/listarProfesions/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Profesion result = new Profesion();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Profesion>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Insertar(Profesion Profesion, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarProfesion";
            string jsonBody = JsonConvert.SerializeObject(Profesion);
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
            endPoint = url + "/api/eliminarProfesion/" + PartitionKey + "," + RowKey + "";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Editar(Profesion Profesion, string token)
        {
            bool sw = false;
            endPoint = url + "/api/modificarProfesion";
            string jsonBody = JsonConvert.SerializeObject(Profesion);
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
