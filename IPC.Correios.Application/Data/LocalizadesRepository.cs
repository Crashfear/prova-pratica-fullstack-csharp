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
    public class LocalizadesRepository : ILocalidadesRepository
    {
        public LocalizadesRepository()
        {
            AppDataPath = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ?? string.Empty;
        }
        private string AppDataPath { get; set; }

        public void SetAppDataPath(string appDataPath)
        {
            AppDataPath = appDataPath;
        }

        public List<Municipio> BuscaMunicipios(string uf)
        {
            var municipios = new List<Municipio>();
            using (StreamReader reader = new StreamReader(Path.Combine(AppDataPath, @"Correios/LOG_LOCALIDADE.TXT"), Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('@');
                    var codMunicipio = values[0];
                    var siglaEstado = values[1];
                    var nome = values[2];
                    if (siglaEstado == uf)
                    {
                        var municipio = new Municipio
                        {
                            Id = codMunicipio,
                            Nome = nome
                        };

                        municipios.Add(municipio);
                    }

                }
            }

            return municipios.OrderBy(m => m.Nome).ToList();
        }

        public string BuscarMunicipioByCodMunicipio(string codMunicipioInformado)
        {
            if (string.IsNullOrEmpty(codMunicipioInformado)) return null;
            
            using (StreamReader reader = new StreamReader(Path.Combine(AppDataPath, @"Correios/LOG_LOCALIDADE.TXT"), Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split('@');
                    var codMunicipio = values[0];
                    if (codMunicipio == codMunicipioInformado)
                    {
                        return values[2];
                    }
                }
            }

            return null;
        }
    }
}
