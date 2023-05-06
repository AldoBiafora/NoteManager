using Autofac;
using Autofac.Integration.WebApi;
using NoteManager.Api.Config;
using NoteManager.Config;
using NoteManager.Api.Controllers;
using System;
using System.Reflection;
using System.Web.Http;

namespace NoteManager.Api
{
    public static class ApiCore
    {
        public static void Start()
        {
            TraceConfig.Register(GlobalConfiguration.Configuration);

            RegisterDependencyInjections((containerBuilder) =>
            {


            });

            // Configura le Web Api
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Configura l'automapper per i DTOs
            //Plugin.Test.Core.Projections.ProjectorConfig.Register();

        }

        static void RegisterDependencyInjections(Action<ContainerBuilder> config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .OnActivated((e) =>
                {
                    if (e.Instance is BaseApiController)
                    {
                        (e.Instance as BaseApiController).IocContext = e.Context.Resolve<IComponentContext>();
                    }
                });

            config(builder);

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}