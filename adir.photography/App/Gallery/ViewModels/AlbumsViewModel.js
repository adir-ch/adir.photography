(function() {
    angular.module('gallery').controller("AlbumsViewModel", ['$scope', 'GalleryResources', function($scope, GalleryResources) {

        //console.log("Albums ViewModel");

        $scope.galleryDataReady = false;
        $scope.galleryName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = "";

        $scope.albumsData = function() {
            //console.log("read album data");
            return GalleryResources.galleriesData();
        };

        var Initialize = function() {

            // get all the images + opening image.
            //console.log("calling Galleries API");

            GalleryResources.getGalleriesData()
                .then(
                    function(status) { // success
                        //console.log("Galleries data ready");
                        $scope.galleryDataReady = status;
                    },
                    function(reason) { // error
                        $scope.serverError = true;
                        console.error("Error while talking to server: ", reason);
                        $scope.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    //console.log("Exception while asking for galleries data: ", exception);
                });
        }

        Initialize();
    }]);
}());