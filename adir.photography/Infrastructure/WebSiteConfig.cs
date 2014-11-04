using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace adir.photography.Infrastructure
{
    public class WebSiteConfig
    {
        private static WebSiteConfig instance = null; 
        private IConfiguration _config; 

        public static WebSiteConfig Instance()
        {
            if (instance == null)
            {
                // TODO: replace this with IoC resolver and keep config file section for other things
                IConfiguration config = (WebConfigFileSection)ConfigurationManager.GetSection("WebSiteConfig"); 
                instance = new WebSiteConfig(config);
            }

            return instance; 
        }

        private WebSiteConfig(IConfiguration configuration)
        {
            _config = configuration; 
        }

        public IConfiguration Config {
            get
            {
                return _config;
            }

            private set {}
        }
    }
}