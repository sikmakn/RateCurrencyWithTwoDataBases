using System.Web.Http;
using FluentScheduler;
using WebApi.FluentScheduler;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            JobManager.Initialize(new FluentSchedulerRegistry());
        }
    }
}
