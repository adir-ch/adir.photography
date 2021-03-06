(function() {
    angular.module('gallery').controller("AlbumViewModel", [ '$scope', '$routeParams', 'GalleryResources', function ($scope, $routeParams, GalleryResources) {

        //console.log("Album ViewModel");

        $scope.albumDataReady = false;
        $scope.albumName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = "";

        $scope.albumData = function() {
            //console.log("read album data");
            return GalleryResources.galleryData();
        };

        if ($routeParams.albumId) {
            //console.log("setting album name from route to: " + $routeParams.albumId);
            $scope.albumName = $routeParams.albumId;
        } else {
            //console.log("no album ID found on route, using default: " + $routeParams.albumId);
        }

        Initialize = function () {

            // get all the images + opening image.
            //console.log("calling Gallery API");
            //console.log("Asking for album: " + $scope.albumName);
            //console.log("service: ", GalleryResources);
            //GalleryResources.getGalleryData($scope.galleryName, callback);
            GalleryResources.getGalleryData($scope.albumName)
                .then(
                    function(status) { // success
                        //console.log("Album data ready");
                        $scope.albumDataReady = status;
                    },
                    function(reason) { // error
                        $scope.serverError = true;
                        console.error("Error while talking to server: ", reason);
                        $scope.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    console.error("Exception while asking for album data: ", exception);
                });
        }

        Initialize();
    }]);
}());