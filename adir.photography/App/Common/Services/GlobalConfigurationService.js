(function() {
    "use strict";

    var GlobalSettings = {
        /* local debug */
        //ServerPath: "http://localhost:61001"

        /* release */
        //ServerPath: "http://adir.photography"

        /* Azure */
        ServerPath: ""
    };

    angular
        .module("AppCommonServices")
        .constant("GlobalSettings", GlobalSettings);


    angular
        .module("AppCommonServices")
        .factory("GlobalConfigurationService", GlobalConfigurationService);

    GlobalConfigurationService.$inject = ['GlobalSettings'];

    function GlobalConfigurationService(GlobalSettings) {

        var _WebApiUrls = {
            gallery: "api/galleryapi/",
            galleries: "api/galleryapi/all",
            member: "api/member",
            idmLogin: "/api/idm/login",
            idmAuthenticate: "api/idm/login",
            addNewInfoSubscriber: "api/registerapi/updates",
            clientLogging: "api/clientlogging"
        };

        return {
            url: function(which) {
                return GlobalSettings.ServerPath + "/" + _WebApiUrls[which];
            }
        };
    }
}());