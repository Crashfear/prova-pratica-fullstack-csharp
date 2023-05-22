using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Core.Services
{
    public interface IBuscaEnderecoService
    {
        Endereco BuscarEnderecoByCep(string cep);
        List<Municipio> BuscaMunicipiosByUf(string uf);
        List<Endereco> BuscarEnderecosByCodMunicipio(string codMunicipioInformado);
        List<Estado> GetEstados();
        Endereco GetEnderecoByCodLogradouro(string codLogradouro);

    }
}
