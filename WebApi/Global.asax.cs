using System.Web.Http;
using DataAccess.AutoMapper;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
