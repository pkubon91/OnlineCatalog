using System;
using System.Web.Mvc;
using Castle.Windsor;
using RequestContext = System.Web.Routing.RequestContext;

namespace OnlineCatalog.Web.Main.IoC
{
    public class CastleControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public CastleControllerFactory(IWindsorContainer container)
        {
            if(container == null) throw new ArgumentNullException(nameof(container));
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            return _container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            disposableController?.Dispose();

            _container.Release(controller);
        }
    }
}