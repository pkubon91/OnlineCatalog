﻿using System;
using Business.DataAccess.Product;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace OnlineCatalog.Services.ProductCategoryService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            if(_container == null) _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>()
                .Register(Component.For<IProductCategoryRepository>().ImplementedBy<ProductCategoryRepository>())
                .Register(Component.For<IProductCategoryService>().ImplementedBy<ProductCategoryService>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            _container?.Dispose();
        }
    }
}