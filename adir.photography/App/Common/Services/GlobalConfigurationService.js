
(function () {
    "use strict";

    angular.module("AppCommonServices").constant("GlobalSettings", {
		ServerPath: "http://localhost:61001"
    });

	angular.module("AppCommonServices").factory("GlobalConfigurationService", ['GlobalSettings', GlobalConfigurationService]);

	function GlobalConfigurationService(GlobalSettings) {

		var _WebApiUrls = {
			gallery: "/api/galleryapi/",
			galleries: "/api/galleriesapi/",
			member: "/api/member",
			idmLogin: "/api/idm/login",
			idmAuthenticate: "/api/idm/login"
		};

        return {
            url: function(which) {
            		return GlobalSettings.ServerPath + "/" + _WebApiUrls[which];
            	}
        };
	}
}());