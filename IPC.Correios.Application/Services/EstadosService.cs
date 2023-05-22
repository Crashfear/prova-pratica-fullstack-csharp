using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Entities;
using IPC.Correios.Core.Services;
using Newtonsoft.Json;

namespace IPC.Correios.Application.Services
{
    public class EstadosService : IEstadosService
    {
        private HttpClient _httpClient;
        public EstadosService()
        {
            _httpClient = new HttpClient();
        }

        public void SetHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Estado> GetEstados()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var result = _httpClient.GetStringAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados").Result;
            var estados = JsonConvert.DeserializeObject<List<Estado>>(result);

            return estados;
        }

       
    }
}
