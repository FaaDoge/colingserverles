using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Coling.Vista.Modelos;
using Coling.Vista.Servicios.Afiliados;
using ColingShared;

namespace Coling.Vista.Servicios.Curriculum
{
    public class PersonaService : IPersonaService
    {
        private readonly HttpClient client;
        private readonly string url = "http://localhost:7291";
        private string endPoint = "";

        public PersonaService(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri(url);
        }

        public async Task<List<Persona>> ListaPersonas(string token)
        {
            endPoint = "/api/listarpersonas";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Persona> result = new List<Persona>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Persona>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<Persona> ObtenerPersonaPorId(int id, string token)
        {
            endPoint = $"/api/listarpersonas/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Persona result = new Persona();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Persona>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> InsertarPersona(Persona persona, string token)
        {
            endPoint = "/api/insertarpersona";
            string jsonBody = JsonConvert.SerializeObject(persona);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EditarPersona(int id, string token)
        {
            endPoint = $"/api/modificarpersona/{id}";
            string jsonBody = JsonConvert.SerializeObject(id);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> BorrarPersona(int id, string token)
        {
            endPoint = $"/api/eliminarpersona/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
