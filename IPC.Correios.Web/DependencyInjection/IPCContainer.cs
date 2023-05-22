using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPC.Correios.Application.Data;
using IPC.Correios.Application.Services;
using IPC.Correios.Core.Data;
using IPC.Correios.Core.Services;
using IPC.Correios.Web.Listas;
using Microsoft.Ajax.Utilities;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace IPC.Correios.Web.DependencyInjection
{
    public class IPCContainer : Container
    {
        public IPCContainer()
        {
            this.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            this.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            this.RegisterMvcIntegratedFilterProvider();

            this.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            this.Register<IBuscaEnderecoService, BuscaEnderecoService>(Lifestyle.Scoped);
            this.Register<IEstadosService, EstadosService>(Lifestyle.Scoped);
            this.Register<ILocalidadesRepository, LocalizadesRepository>(Lifestyle.Scoped);
            this.Register<ISelectListFactory, SelectListFactory>(Lifestyle.Scoped);
        }
    }
}