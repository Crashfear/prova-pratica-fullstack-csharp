using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Core.Data
{
    public interface IEnderecoRepository
    {
        Endereco BuscarEnderecoByCep(string cep);
        void SetAppDataPath(string path);
        List<Endereco> BuscarEnderecosByCodMunicipio(string codMunicipioInformado);
        Endereco BuscarEnderecoByCodLogradouro(string codLogradouro);
    }
}
