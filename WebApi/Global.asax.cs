using System.Web.Http;
using DataAccess.DataBase;
using DataAccess.Repositories;
using FluentScheduler;
using WebApi.FluentScheduler;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
