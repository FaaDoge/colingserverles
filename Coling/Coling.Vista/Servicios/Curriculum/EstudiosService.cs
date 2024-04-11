using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Vista.Modelos;
using Newtonsoft.Json;

namespace Coling.Vista.Servicios.Curriculum
{
    public class EstudiosService:IEstudiosServices
    {
        string url = "http://localhost:7127";
        string endPoint = "";
        private readonly HttpClient client;
        public EstudiosService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }

        public async Task<List<Estudios>> Listar(string token)
        {
            endPoint = "/api/ListarEstudios";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Estudios> result = new List<Estudios>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Estudios>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<Estudios> ListaID(string id, string token)
        {
            endPoint = url + "/api/listarEstudioss/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Estudios result = new Estudios();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Estudios>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Insertar(Estudios Estudios, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarEstudios";
            string jsonBody = JsonConvert.SerializeObject(Estudios);
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
            endPoint = url + "/api/eliminarEstudios/" + PartitionKey + "," + RowKey + "";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Editar(Estudios Estudios, string token)
        {
            bool sw = false;
            endPoint = url + "/api/modificarEstudios";
            string jsonBody = JsonConvert.SerializeObject(Estudios);
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
