using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Data;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Application.Data
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ILocalidadesRepository _localidadesRepository;
        public EnderecoRepository(ILocalidadesRepository localidadesRepository)
        {
            _localidadesRepository = localidadesRepository;
            AppDataPath = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ?? string.Empty;
        }

        private string AppDataPath { get; set; }

        public void SetAppDataPath(string appDataPath)
        {
            AppDataPath = appDataPath;
        }

        public Endereco BuscarEnderecoByCep(string cep)
        {
            var endereco = buscarEnderecoByCep(cep);

            if (endereco != null)
            {
                var nomeMunicipio = _localidadesRepository.BuscarMunicipioByCodMunicipio(endereco?.CodMunicipio);

                if (!string.IsNullOrEmpty(nomeMunicipio))
                {
                    endereco.Municipio = nomeMunicipio;
                }

            }

            return endereco;
        }

        public Endereco BuscarEnderecoByCodLogradouro(string codLogradouro)
        {
            var endereco = buscarEnderecoByCodLogradouro(codLogradouro);

            if (endereco != null)
            {
                var nomeMunicipio = _localidadesRepository.BuscarMunicipioByCodMunicipio(endereco?.CodMunicipio);

                if (!string.IsNullOrEmpty(nomeMunicipio))
                {
                    endereco.Municipio = nomeMunicipio;
                }

            }

            return endereco;
        }

        private Endereco buscarEnderecoByCep(string cepInformado)
        {
            using (StreamReader reader = new StreamReader(Path.Combine(AppDataPath, @"Correios/LOG_LOGRADOURO_SC.TXT"), Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('@');
                    var cep = values[7];
                    if (cep == cepInformado)
                    {
                        return new Endereco
                        {
                            Logradouro = values[10],
                            Cep = values[7],
                            Bairro = "",
                            CodLogradouro = values[0],
                            CodMunicipio = values[2],
                            UF = values[1]
                        };
                    }
                }
            }

            return null;
        }

        private Endereco buscarEnderecoByCodLogradouro(string codLogradouroInformado)
        {
            using (StreamReader reader = new StreamReader(Path.Combine(AppDataPath, @"Correios/LOG_LOGRADOURO_SC.TXT"), Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('@');
                    var codLogradouro = values[0];
                    if (codLogradouro == codLogradouroInformado)
                    {
                        return new Endereco
                        {
                            Logradouro = values[10],
                            Cep = values[7],
                            Bairro = "",
                            CodLogradouro = values[0],
                            CodMunicipio = values[2],
                            UF = values[1]
                        };
                    }
                }
            }

            return null;
        }

        public List<Endereco> BuscarEnderecosByCodMunicipio(string codMunicipioInformado)
        {
            var enderecos = new List<Endereco>();
            using (StreamReader reader = new StreamReader(Path.Combine(AppDataPath, @"Correios/LOG_LOGRADOURO_SC.TXT"), Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('@');
                    var codMunicipio = values[2];
                    if (codMunicipio == codMunicipioInformado)
                    {
                        enderecos.Add(new Endereco
                        {
                            Logradouro = values[10],
                            Cep = values[7],
                            Bairro = "",
                            CodLogradouro = values[0],
                            CodMunicipio = values[2],
                            UF = values[1]
                        });
                    }
                }
            }

            return enderecos;
        }

        
    }
}
