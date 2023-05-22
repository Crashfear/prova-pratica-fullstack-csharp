using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using IPC.Correios.Application.Mappers;
using IPC.Correios.Web.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;


namespace IPC.Correios.Middleware.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new IPCContainer();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var rootPath = HttpContext.Current.Server.MapPath("~/");
            Path.Combine(rootPath,"App_data");

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            AutoMapperConfig.RegisterMappings();
        }
    }
}
