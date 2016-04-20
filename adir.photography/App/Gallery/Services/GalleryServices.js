(function () {
    "use strict";

    angular.module("gallery").factory("GalleryResources", ['WebApiService', GalleryResources]).run(function(){
        console.log("starting gallery service");
    });

    function GalleryResources(WebApiService) {

		var _galleryData = [];
	    var _galleryDataReady = false;
        var _errorMessage = "";
        var _callback;

        var onSuccess = function(response) {
            angular.copy(response.data, _galleryData)
            _galleryDataReady = true;
            //_callback();
        }

        var onFailure = function(response) {
            _errorMessage += response.data.ExceptionMessage;
        }

        var _getGalleryData = function(galleryName, callback) {
            _callback = callback;
            return WebApiService.apiGet('/api/galleryapi/', onSuccess, onFailure, null);
        }

        return {
            galleryData: _galleryData,

            errorMessage: function() {
                return _errorMessage;
            },

            //errorMessage: _errorMessage,

            dataReadyStatus: function() {
                return _galleryDataReady
            },

            getGalleryData: _getGalleryData,
        };

    }

}());