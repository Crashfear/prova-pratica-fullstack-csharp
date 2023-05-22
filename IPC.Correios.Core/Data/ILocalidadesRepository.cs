using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Core.Data
{
    public interface ILocalidadesRepository
    {
        List<Municipio> BuscaMunicipios(string uf);
        string BuscarMunicipioByCodMunicipio(string codMunicipioInformado);
        void SetAppDataPath(string path);
    }
}
