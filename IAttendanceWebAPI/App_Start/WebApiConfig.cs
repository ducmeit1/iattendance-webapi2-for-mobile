using Microsoft.Owin.Security.OAuth;
using MultipartDataMediaFormatter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IAttendanceWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());
            config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());

            var setting = config.Formatters.JsonFormatter.SerializerSettings;
            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            setting.Formatting = Formatting.Indented;

            config.Formatters.JsonFormatter.SerializerSettings = setting;

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            if (!Debugger.IsAttached)
                config.Filters.Add(new RequireHttpsAttribute());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //config.Filters.Add(new ValidationModelStateAttribute());
            //config.Filters.Add(new ValidationMimeMultipartAttribute());
            //config.Filters.Add(new Persistence.Filters.AuthorizeAttribute());
        }
    }
}