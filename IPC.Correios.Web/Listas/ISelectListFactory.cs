using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IPC.Correios.Application.Services
{
    public interface ISelectListFactory
    {
        SelectList GetEstadosList();
        SelectList GetMunicipiosList(string uf);
        SelectList GetEnderecosList(string codMunicipio);
    }
}
