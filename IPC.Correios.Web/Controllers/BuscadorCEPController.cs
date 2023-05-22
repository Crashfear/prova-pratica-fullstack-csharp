using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IPC.Correios.Application.Services;
using IPC.Correios.Application.ViewModels;
using IPC.Correios.Core.Entities;
using IPC.Correios.Core.Services;

namespace IPC.Correios.Web.Controllers
{
    public class BuscadorCEPController : Controller
    {
        private readonly IBuscaEnderecoService _buscaEnderecoService;
        private readonly ISelectListFactory _selectListFactory;
        public BuscadorCEPController(IBuscaEnderecoService buscaEnderecoService,
                                     ISelectListFactory selectListFactory)
        {
            _buscaEnderecoService = buscaEnderecoService;
            _selectListFactory = selectListFactory;
        }

        public ActionResult BuscarEndereco(BuscaCepViewModel buscaCep)
        {
            if (ModelState.IsValid)
            {
                var endereco = _buscaEnderecoService.BuscarEnderecoByCep(buscaCep.Cep);
                if (endereco != null)
                {
                    var enderecoViewModel = Mapper.Map<Endereco, EnderecoViewModel>(endereco);
                    return View("ResultadoBusca", enderecoViewModel);
                }
            }
            return View(buscaCep);
        }

        public ActionResult BuscarCEP()
        {
            var estadosList = _selectListFactory.GetEstadosList();
            ViewBag.Estados = estadosList;
            return View(new BuscaEnderecoViewModel());
        }

        [HttpPost]
        public JsonResult GetMunicipios(string uf)
        {
            var municipios = _selectListFactory.GetMunicipiosList(uf);
            return Json(municipios);
        }

        [HttpPost]
        public JsonResult GetLogradouros(string codMunicipio)
        {
            var enderecos = _selectListFactory.GetEnderecosList(codMunicipio);
            return Json(enderecos);
        }


        [HttpPost]
        public JsonResult GetInfosLogradouro(string codLogradouro)
        {
            var enderecos = _buscaEnderecoService.GetEnderecoByCodLogradouro(codLogradouro);
            return Json(enderecos);
        }
    }
}