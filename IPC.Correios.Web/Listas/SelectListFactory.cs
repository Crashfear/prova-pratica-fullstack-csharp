using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPC.Correios.Application.Services;
using IPC.Correios.Core.Data;
using IPC.Correios.Core.Services;

namespace IPC.Correios.Web.Listas
{
    public class SelectListFactory : ISelectListFactory
    {
        private readonly IBuscaEnderecoService _buscaEnderecoService;
        public SelectListFactory(IBuscaEnderecoService buscaEnderecoService)
        {
            _buscaEnderecoService = buscaEnderecoService;
        }
        public SelectList GetEstadosList()
        {
            var estados = _buscaEnderecoService.GetEstados().OrderBy(e => e.Sigla);
            return new SelectList(estados, "Sigla", "Nome");
        }

        public SelectList GetMunicipiosList(string uf)
        {
            var municipios = _buscaEnderecoService.BuscaMunicipiosByUf(uf);
            return new SelectList(municipios, "Id", "Nome");
        }

        public SelectList GetEnderecosList(string codMunicipio)
        {
            var municipios = _buscaEnderecoService.BuscarEnderecosByCodMunicipio(codMunicipio);
            return new SelectList(municipios, "CodLogradouro", "Logradouro");
        }
    }
}