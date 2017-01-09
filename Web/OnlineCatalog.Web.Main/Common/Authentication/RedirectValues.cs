using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class RedirectValues : IRedirectValues
    {
        public RedirectValues(string action, string controller)
        {
            if(action.IsNullOrEmpty()) throw new ArgumentException();
            if(controller.IsNullOrEmpty()) throw new ArgumentException();

            Action = action;
            Controller = controller;
        }

        public string Action { get; }
        public string Controller { get; }
        public object RouteValues { get; }
    }
}