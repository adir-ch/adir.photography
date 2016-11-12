(function() {
    angular.module('gallery').controller("AlbumsViewModel", [ '$scope', '$routeParams', 'GalleryResources', function ($scope, $routeParams, GalleryResources) {

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

        if ($routeParams.galleryId) {
            //console.log("setting gallery name from route to: " + $routeParams.galleryId);
            $scope.galleryName = $routeParams.galleryId;
        }

        // $scope.SetAlbumPhoto = function(album) { // not used!!!
        // 	var css = {
        // 				'background-image': 'url(\'' + GenerateCssBackgroundImage(album) + '\')',
        // 			};
        // 	//console.log(css);
        // 	return css;
        // }

        $scope.SetAlbumPhotoStyle = function(album) {
            var css = {
                        //'background-image': 'url(\'' + GenerateCssBackgroundImage(album) + '\')',
                        "background-image": "cover"
                    };
            return css;
        }

        $scope.GenerateAlbumLink = function(albumName) {
            //console.log("setting album link to: /gallery/album/" + albumName);
            return "/gallery/album/" + albumName;
        }

        // var GenerateCssBackgroundImage = function (album) { // not used
        // 	return album.ImagesLocation + "/" + album.OpeningPhoto;
        // }

        $scope.GenerateAlbumBackgroundImage = function (album) {
            //console.log(album.ImagesLocation + "/" + album.OpeningPhoto);
            return album.ImagesLocation + "/" + album.OpeningPhoto;
        }

        var Initialize = function () {

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