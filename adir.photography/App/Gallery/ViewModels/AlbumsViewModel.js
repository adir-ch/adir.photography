(function() {
    angular.module('gallery').controller("AlbumsViewModel", [ '$scope', '$routeParams', 'GalleryResources', function ($scope, $routeParams, GalleryResources) {

        console.log("Albums ViewModel");

        $scope.galleryDataReady = false;
        $scope.galleryName = "Main";
        $scope.userName = "default";
        $scope.serverError = false;
        $scope.serverErrorMessage = "";

        $scope.galleriesData = function() {
            console.log("read galleries data");
            return GalleryResources.galleriesData();
        };

        if ($routeParams.galleryId) {
            console.log("setting gallery name from route to: " + $routeParams.galleryId);
            $scope.galleryName = $routeParams.galleryId;
        }

        $scope.SetAlbumPhoto = function(gallery) {
        	var css = {
        				'background-image': 'url(\'' + GenerateCssBackgroundImage(gallery) + '\')',
        			};
        	console.log(css);
        	return css;
        }

        $scope.GenerateAlbumLink = function(albumName) {
            console.log("setting album link to: /gallery/show/" + albumName);
            return "/gallery/show/" + albumName;
        }

        var GenerateCssBackgroundImage = function (gallery) {
        	return gallery.ImagesLocation + "/" + gallery.OpeningPhoto;
        }

        var Initialize = function () {

            // get all the images + opening image.
            console.log("calling Galleries API");

            GalleryResources.getGalleriesData()
                .then(
                    function(status) { // success
                        console.log("Galleries data ready");
                        $scope.galleryDataReady = status;
                    },
                    function(reason) { // error
                        $scope.serverError = true;
                        console.log("Error while talking to server: ", reason);
                        $scope.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    console.log("Exception while asking for galleries data: ", exception);
                });
        }

        Initialize();
    }]);
}());