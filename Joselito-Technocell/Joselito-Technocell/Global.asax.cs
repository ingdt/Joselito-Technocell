using Joselito_Technocell.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Joselito_Technocell
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Joselito_TechnocellDbContext, Migrations.Configuration>());
            CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UserHelper.CheckRole("Admin");
            UserHelper.CheckRole("Vendedor");
            UserHelper.CheckSuperUser();
        }
    }
}
