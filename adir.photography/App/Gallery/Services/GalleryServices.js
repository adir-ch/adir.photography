(function () {
    "use strict";

    angular.module("gallery").factory("GalleryResources", ['WebApiService', GalleryResources]).run(function(){
        console.log("starting gallery service");
    });

    function GalleryResources(WebApiService) {

		var _galleryData = [];
	    var _galleryDataReady = false;
        var _errorMessage = "";

        var _onSuccess = function(response) {
            angular.copy(response, _galleryData);
            _galleryDataReady = true;
        }

        var _onFailure = function (response) {
            console.log("Failed to find gallery");
            _errorMessage = response.data.ExceptionMessage;
        }

        // Gallery service gets a promise obj from the WebApiService and set a handle
        // (callback anon function) that will be called when the promise is resolved.
        // The WebApiService when resolved will call to the anon func with the resolved
        // data, and the Gallery service will resolve the promise of whoever called it.
        var _getGalleryData = function(galleryName) {
            return WebApiService.apiGet('/api/galleryapi')
                .then(function(response) {
                    _onSuccess(response);
                    console.log("WebApi call success");
                    return _galleryDataReady;
                },
                function(response) {
                    console.log("WebApi call failed");
                    _onFailure(response);
                    return _errorMessage;
                }
            ).catch(function(response) {
                console.log("Exception while calling WebApi service");
            });

        }

        return {
            galleryData: function() { return _galleryData; },

            errorMessage: function() { return _errorMessage; },

            dataReadyStatus: function() { return _galleryDataReady; },

            getGalleryData: _getGalleryData,
        };

    }

}());