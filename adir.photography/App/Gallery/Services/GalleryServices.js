(function () {
    "use strict";

    angular.module("gallery").factory("GalleryResources", ['GlobalConfigurationService', 'WebApiService', GalleryResources]).run(function(){
        //console.log("starting gallery resources service");
    });

    function GalleryResources(GlobalConfigurationService, WebApiService) {

		var _galleryData = [];
        var _galleriesData = [];
	    var _serverDataReady = false;
        var _errorMessage = "";

        var _onGallerySuccess = function(response) {
            angular.copy(response, _galleryData);
        }

        var _onGalleriesSuccess = function(response) {
            angular.copy(response, _galleriesData);
        }

        var _onFailure = function (response) {
            _serverDataReady = false;
            _errorMessage = response;
            console.error("Failed to find galleries: ", _errorMessage);
        }

        function GetServerData(apiUrl, success) {
            //console.log("calling api with: ", apiUrl);
            return WebApiService.apiGet(apiUrl)
                .then(function(response) {
                    //console.log("WebApi call success");
                    success(response);
                    _serverDataReady = true;
                    return _serverDataReady;
                },
                function(response) {
                    console.error("WebApi call failed");
                    _onFailure(response);
                    throw _errorMessage;
                }
            ).catch(function(response) {
                console.error("Exception while calling WebApi service");
                throw response;
            });
        }

        var _getGalleryData = function(galleryName) {
            var url = GlobalConfigurationService.url("gallery") + "/" + galleryName;
            return GetServerData(url, _onGallerySuccess);
        }

        var _getGalleriesData = function() {
            var url = GlobalConfigurationService.url("galleries");
            return GetServerData(url, _onGalleriesSuccess);
        }

        return {
            galleryData: function() { return _galleryData; },
            galleriesData: function() { return _galleriesData; },
            errorMessage: function() { return _errorMessage; },
            dataReadyStatus: function() { return _galleryDataReady; },
            getGalleryData: _getGalleryData,
            getGalleriesData: _getGalleriesData,
        };
    }
}());