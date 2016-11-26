(function() {
    angular.module('gallery').controller("GalleryViewModel", ['$scope', '$routeParams', '$window', '$location', 'GalleryResources', function($scope, $routeParams, $window, $location, GalleryResources) {

        //console.log("Gallery ViewModel");

        $scope.galleryDataReady = false;
        $scope.galleryName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = "";
        //$scope.isMobile = false;
        $scope.isMobileView = false;
        $scope.isLandscapeView = false;

        $scope.galleryData = function() {
            //console.log("read gallery data");
            return GalleryResources.galleryData();
        };

        if ($routeParams.galleryId) {
            //console.log("setting gallery name from route to: " + $routeParams.galleryId);
            $scope.galleryName = $routeParams.galleryId;
        }

        // function subscribeToWindowResizeEvent() {
        //     angular.element($window).bind('resize', function() {
        //         console.log("windows size changed: " + $window.innerWidth);
        //     })
        // }

        Initialize = function() {

            //subscribeToWindowResizeEvent();

            // get all the images + opening image.
            //console.log("calling Gallery API");
            //console.log("Asking for gallery: " + $scope.galleryName);
            //console.log("service: ", GalleryResources);
            //GalleryResources.getGalleryData($scope.galleryName, callback);
            GalleryResources.getGalleryData($scope.galleryName)
                .then(
                    function(status) { // success
                        //console.log("Gallery data ready");
                        $scope.galleryDataReady = status;
                    },
                    function(reason) { // error
                        $scope.serverError = true;
                        console.error("Error while talking to server: ", reason);
                        $scope.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    console.error("Exception while asking for gallery data: ", exception);
                });
        }

        Initialize();
    }]);
}());