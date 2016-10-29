﻿using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OnlineCatalog.Web.Main.IoC
{
    public class CastleApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}