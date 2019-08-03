using Microservice.User.Api.App_Start;
using Microservice.User.Windsor;
using Microservice.User.Windsor.Installers;
using System.Reflection;
using System.Web.Http;

namespace Microservice.User.Api
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register windsor installers
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            WindsorConfig.Configure(
                new ControllerInstaller()
                {
                    ControllerType = typeof(ApiController),
                    ExecutingAssembly = executingAssembly
                },
                new ServiceInstaller(),
                new RepositoryInstaller()
            );

            GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator),
                new HttpControllerActivator(WindsorConfig.Container));
        }
    }
}