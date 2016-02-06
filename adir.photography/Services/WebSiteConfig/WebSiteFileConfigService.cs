using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace adir.photography.Services.WebSiteConfig
{
    public class WebSiteFileConfigService : IWebSiteConfigService
    {
        private static WebSiteFileConfigService instance = null;
        private WebConfigFileSection _config; 

        public static WebSiteFileConfigService Instance()
        {
            if (instance == null)
            {
                // TODO: replace this with IoC resolver and keep config file section for other things
                WebConfigFileSection config = (WebConfigFileSection)ConfigurationManager.GetSection("WebSiteConfig"); 
                instance = new WebSiteFileConfigService(config);
            }

            return instance; 
        }

        private WebSiteFileConfigService(WebConfigFileSection configuration)
        {
            _config = configuration; 
        }

        public string PhotosLocation
        {
            get
            {
                return _config.Location.Path;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}