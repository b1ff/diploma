﻿using System.Web;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Gamification.Core.ProjectSettings;
using Gamification.Data.EF.Contexts;
using Gamification.Data.EF.Repositories;
using Gamification.Web.Utils.SimpleMembership;

namespace Gamification.IOC
{
    public class ComponentRegistrator 
    {
        public static IWindsorContainer BuildContainerWithCommonComponents()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IApplicationSettings>().ImplementedBy<FromConfigFileConfiguration>().LifeStyle.Singleton);
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
            
            return container;
        }

        public static IWindsorContainer BuildWebContainer()
        {
            var container = BuildContainerWithCommonComponents();

            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.PerWebRequest);

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            container.Register(AllTypes.FromAssembly(typeof(ISimpleMembership).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            container.Register(Component.For<HttpContextBase>().UsingFactoryMethod(x => new HttpContextWrapper(HttpContext.Current)).LifeStyle.PerWebRequest);

            return container;
        }

        public static IWindsorContainer BuildWcfContainer()
        {
            var container = BuildContainerWithCommonComponents();
            container.AddFacility<WcfFacility>()
                     .Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.PerWcfOperation());

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            return container;
        }
    }
}