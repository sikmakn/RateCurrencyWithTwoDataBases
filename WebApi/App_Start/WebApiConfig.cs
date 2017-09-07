﻿using System.Web.Http;
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
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = UnityRegisterType.RegisterType();

            config.DependencyResolver = new UnityResolver(container);
            JobManager.JobFactory = new JobFactory(container);
            JobManager.Initialize(new FluentSchedulerRegistry());

        }
    }
}