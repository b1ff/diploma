using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Gamification.Web.ControllerFactory
{
    public class CastleControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public CastleControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "Not found");
            }

            try
            {
                return (IController)this.container.Resolve(controllerType);
            }
            catch
            {
                throw new HttpException(404, "Not found");
            }
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}