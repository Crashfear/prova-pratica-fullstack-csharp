using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC.Correios.Application.ViewModels
{
    public class EnderecoViewModel
    {
        public string CodLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Municipio { get; set; }
        public string CodMunicipio { get; set; }
        public string UF { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }

        public string DisplayMunicipioUf => $"{Municipio} - {UF}";
    }
}
