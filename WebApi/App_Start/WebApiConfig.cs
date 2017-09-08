using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using DataAccess.DataBase;
using FluentScheduler;
using WebApi.App_Start;
using WebApi.FluentScheduler;
using WebApi.Unity;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = UnityRegisterType.RegisterType();

            config.DependencyResolver = new UnityResolver(container);

            //bacfground FluentScheduler
            JobManager.UseUtcTime();
            JobManager.JobFactory = new JobFactory(container);
            JobManager.Initialize(new FluentSchedulerRegistry());

            //OData
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CurrencyRateByTime>("CurrencyRateByTimes");
            builder.EntitySet<BankDepartment>("BankDepartment");
            builder.EntitySet<Currency>("Currency");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
            );
            
        }
    }
}
