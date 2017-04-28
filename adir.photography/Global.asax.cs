using log4net.Config;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace adir.photography
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StartLogger();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            log4net.ThreadContext.Properties["SessionId"] = HttpContext.Current.Session.SessionID;
        }

        private void StartLogger()
        {
            BasicConfigurator.Configure();
            FileInfo file = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("~") + "/LoggerConfig.xml");
            XmlConfigurator.Configure(file);
        }
    }
}