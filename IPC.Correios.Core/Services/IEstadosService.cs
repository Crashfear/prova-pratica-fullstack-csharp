using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Core.Services
{
    public interface IEstadosService
    {
        List<Estado> GetEstados();
        void SetHttpClient(HttpClient httpClient);
    }
}
