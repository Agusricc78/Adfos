using System.Web.Http;
using System.Web.Http.Cors;
using IntegracionApi.Filters;
using IntegracionApi.Formatters;

namespace IntegracionApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationRequiredAttribute());
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.Formatters.Add(new RegistroDiscapacidadExcelFormatter());
            config.Formatters.Add(new RegistroDiscapacidadFormatter());
            config.Formatters.Add(new RendicionExcelFormatter());
            config.Formatters.Add(new RegistroDevolucionExcelFormatter());
            config.Formatters.Add(new RegistroDevolucionFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
           
        }
    }
}
