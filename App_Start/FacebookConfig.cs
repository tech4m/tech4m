using Microsoft.AspNet.Facebook;
using Microsoft.AspNet.Facebook.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tech4mUI
{
    public static class FacebookConfig
    {
        public static void Register(FacebookConfiguration configuration)
        {
            // Loads the settings from web.config using the following app setting keys:
            GlobalFacebookConfiguration.Configuration.AppId =
                ConfigurationManager.AppSettings["Facebook_AppId"];
            GlobalFacebookConfiguration.Configuration.AppSecret =
                ConfigurationManager.AppSettings["Facebook_AppSecret"];

            // Adding the authorization filter to check for Facebook signed requests 
            // and permissions
            GlobalFilters.Filters.Add(new FacebookAuthorizeFilter(configuration));
        }
    }

}