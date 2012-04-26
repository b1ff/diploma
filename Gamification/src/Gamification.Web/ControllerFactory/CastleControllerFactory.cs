using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Gamification.Web.ControllerFactory
{
    /// <summary>
    /// Controller factory with windsor
    /// </summary>
    public class CastleControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// windsor container instance
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CastleControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public CastleControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>The controller instance.</returns>
        /// <exception cref="T:System.Web.HttpException">
        /// 	<paramref name="controllerType"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="controllerType"/> cannot be assigned.</exception>
        /// <exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
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
            catch(Exception e)
            {
                throw new HttpException(404, "Not found");
            }
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}