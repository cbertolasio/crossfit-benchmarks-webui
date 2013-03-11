using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IdentityModel.Services;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.IdentityModel.Services.Configuration;

namespace CrossfitBenchmarks.WebUi
{

    
    

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutomapBootstrap.Initialize();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            DateTime validFrom = e.SessionToken.ValidFrom;
            DateTime validTo = e.SessionToken.ValidTo;
            if ((now < validTo) &&
            (now > validFrom.AddMinutes((validTo.Minute - validFrom.Minute) / 2))
            ) {
                SessionAuthenticationModule sam = sender as SessionAuthenticationModule;
                e.SessionToken = sam.CreateSessionSecurityToken(e.SessionToken.ClaimsPrincipal, e.SessionToken.Context,
                now, now.AddMinutes(10), e.SessionToken.IsPersistent);
                e.ReissueCookie = true;
            }
        }
    }
}