using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using DataAccess.DataBase;
using DataAccess.Models;

namespace WebApi
{
    public class ODataConfig
    {
        public static void ODataRegister(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CurrencyRateByTime>("CurrencyRateByTime");
            builder.EntitySet<BankDepartment>("BankDepartment");
            builder.EntitySet<Currency>("Currency");
            builder.EntitySet<City>("City");

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
            );
        }
    }
}