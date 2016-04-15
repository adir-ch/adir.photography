(function() {
    angular.module('gallery').controller("GalleryViewModel", [ '$scope', '$routeParams', 'GalleryResources', function ($scope, $routeParams, GalleryResources) {

        console.log("Gallery ViewModel");
        $scope.galleryData = GalleryResources.galleryData;
        $scope.galleryDataReady = false;
        $scope.galleryName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = false;

        if ($routeParams.galleryId) {
            console.log("setting gallery name from route to: " + $routeParams.galleryId);
            $scope.galleryName = $routeParams.galleryId;
        }

        $scope.initialize = function () {

            // get all the images + opening image.
            console.log("calling Gallery API");
            console.log("Asking for gallery: " + $scope.galleryName);
            GalleryResources.getGalleryData($scope.galleryName)
                .then(function(status) { // success
                    $scope.galleryDataReady = GalleryResources.dataReadyStatus;
                },
                function(reason) { // error
                    $scope.serverError = true;
                    console.log("Error while talking to server: ", reason);
                    $scope.serverErrorMessage = reason; // TODO: check why GalleryResources.errorMessage - is not working !!!
                });

        }

        $scope.initialize();
    }]);
}());