(function() {
    angular.module('gallery').controller("GalleryViewModel", [ '$scope', '$routeParams', 'GalleryResources', function ($scope, $routeParams, GalleryResources) {

        console.log("Gallery ViewModel");

        $scope.galleryDataReady = false;
        $scope.galleryName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = "";

        $scope.galleryData = function() {
            console.log("read gallery data");
            return GalleryResources.galleryData();
        };

        if ($routeParams.galleryId) {
            console.log("setting gallery name from route to: " + $routeParams.galleryId);
            $scope.galleryName = $routeParams.galleryId;
        }

        Initialize = function () {

            // get all the images + opening image.
            console.log("calling Gallery API");
            console.log("Asking for gallery: " + $scope.galleryName);
            console.log("service: ", GalleryResources);
            //GalleryResources.getGalleryData($scope.galleryName, callback);
            GalleryResources.getGalleryData($scope.galleryName)
                .then(
                    function(status) { // success
                        console.log("Gallery data ready");
                        $scope.galleryDataReady = status;
                    },
                    function(reason) { // error
                        $scope.serverError = true;
                        console.log("Error while talking to server: ", reason);
                        $scope.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    console.log("Exception while asking for gallery data: ", exception);
                });
        }

        Initialize();
    }]);
}());