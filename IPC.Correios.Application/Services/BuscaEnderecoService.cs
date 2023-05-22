using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Data;
using IPC.Correios.Core.Entities;
using IPC.Correios.Core.Services;

namespace IPC.Correios.Application.Services
{
    public class BuscaEnderecoService : IBuscaEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IEstadosService _estadosService;
        private readonly ILocalidadesRepository _localidadesRepository;
        public BuscaEnderecoService(IEnderecoRepository enderecoRepository,
                                    IEstadosService estadosService,
                                    ILocalidadesRepository localidadesRepository)
        {
            _enderecoRepository = enderecoRepository;
            _estadosService = estadosService;
            _localidadesRepository = localidadesRepository;
        }
        public Endereco BuscarEnderecoByCep(string cep)
        {
            return _enderecoRepository.BuscarEnderecoByCep(cep);
        }

        public List<Municipio> BuscaMunicipiosByUf(string uf)
        {
            return _localidadesRepository.BuscaMunicipios(uf);
        }

        public List<Endereco> BuscarEnderecosByCodMunicipio(string codMunicipioInformado)
        {
            return _enderecoRepository.BuscarEnderecosByCodMunicipio(codMunicipioInformado);
        }

        public List<Estado> GetEstados()
        {
           return _estadosService.GetEstados();
        }

        public Endereco GetEnderecoByCodLogradouro(string codLogradouro)
        {
            return _enderecoRepository.BuscarEnderecoByCodLogradouro(codLogradouro);
        }
    }
}
