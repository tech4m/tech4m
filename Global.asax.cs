using Microsoft.AspNet.Facebook;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using tech4mEntity;
using tech4mUI.Controllers;
using tech4mUI.Extensions;
using tech4mUI.Models;
using tech4mUI.Providers;
using tech4mUI.Tech4mService;
namespace tech4mUI
{
    //public class MvcApplication : NinjectHttpApplication//System.Web.HttpApplication
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FacebookConfig.Register(GlobalFacebookConfiguration.Configuration);
        }

        //protected override IKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();

        //    kernel.Load(new RepositoryModule());
        //    kernel.Bind<ITech4mService>().To<Tech4mServiceClient>();
        //    kernel.Bind<IAuthProvider>().To<AuthProvider>();

        //    return kernel;
        //}

        //protected override void OnApplicationStarted()
        //{
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));

        //    HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

        //    base.OnApplicationStarted();
        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var ex = Server.GetLastError();
            var status = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;

            // Is Ajax request? return json
            if (httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;
                httpContext.Response.TrySkipIisCustomErrors = true;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Write("{ success: false, message: \"Error occured in server.\" }");
                httpContext.Response.End();
            }
            else
            {
                var currentController = " ";
                var currentAction = " ";
                var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

                if (currentRouteData != null)
                {
                    if (currentRouteData.Values["controller"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                    {
                        currentController = currentRouteData.Values["controller"].ToString();
                    }

                    if (currentRouteData.Values["action"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                    {
                        currentAction = currentRouteData.Values["action"].ToString();
                    }
                }

                var controller = new ErrorController();
                var routeData = new RouteData();

                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;
                httpContext.Response.TrySkipIisCustomErrors = true;

                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "Index";

                controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }

    }
}
