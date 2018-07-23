using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IAttendanceWebAPI
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!Debugger.IsAttached)
                if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent("<p>Please use https instead of http</p>",
                        Encoding.UTF8, "text/html");

                    var uriBuilder = new UriBuilder(actionContext.Request.RequestUri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = 443
                    };

                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    base.OnAuthorization(actionContext);
                }
            else
                base.OnAuthorization(actionContext);
        }
    }
}