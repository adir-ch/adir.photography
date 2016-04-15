(function () {
    "use strict";

    angular.module("gallery").factory("GalleryResources", ['WebApiService', GalleryResources])

    function GalleryResources(WebApiService) {

		var _galleryData = [];
	    var _galleryDataReady = false;
        var _errorMessage = "";

        var onSuccess = function(response) {
            angular.copy(response.data, _galleryData)
            _galleryDataReady = true;
        }

        var onFailure = function(response) {
            _errorMessage = response.data.ExceptionMessage;
        }

        var _getGalleryData = function(galleryName) {
            return WebApiService.apiGet('/api/galleryapi/', onSuccess, onFailure, null);
        }

        return {
            galleryData: _galleryData,
            errorMessage: _errorMessage,
            dataReadyStatus: _galleryDataReady,
            getGalleryData: _getGalleryData
        };

    }

}());