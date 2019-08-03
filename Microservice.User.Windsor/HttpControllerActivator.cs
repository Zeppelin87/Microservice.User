using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Microservice.User.Windsor
{
    public class HttpControllerActivator : System.Web.Http.Dispatcher.IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public HttpControllerActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController resolvedController = (IHttpController)this._container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => this._container.Release(resolvedController)));

            return resolvedController;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}
