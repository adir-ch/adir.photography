(function() {

    'use strict';

    angular
        .module('gallery')
        .controller('AlbumsWelcomeViewModel', GalleryViewModel);

    GalleryViewModel.$inject = [
        '$routeParams',
        '$window',
        '$location',
        '$timeout',
        'ngProgressFactory',
        'ImageLoader',
        'GalleryResources'
    ];

    function GalleryViewModel(
        $routeParams,
        $window,
        $location,
        $timeout,
        ngProgressFactory,
        ImageLoader,
        GalleryResources) {

        //console.log("Starting albums welcome controller");
        var vm = this;

        vm.albumsData = GetAlbumsData;
        vm.progressbar = ngProgressFactory.createInstance();
        vm.serverError = false;
        vm.serverErrorMessage = "";
        vm.ShowWelcomeElements = true;
        vm.ShowWelcomeImage = true;
        vm.StarAlbumsDisplayDirective = false;
        vm.progressbarStep = 0;
        var timeout = 2000;

        activate();

        function activate() {
            GalleryResources.getGalleriesData()
                .then(
                    function(status) { // success
                        vm.galleryDataReady = status;
                        HandleGalleriesDataReady();
                    },
                    function(reason) { // error
                        vm.serverError = true;
                        console.error("Error while talking to server: ", reason);
                        vm.serverErrorMessage = reason;
                    }
                ).catch(function(exception) {
                    console.error("Exception while asking for gallery data: ", exception);
                });
        }

        /////////////// Functions implementation 

        function GetAlbumsData() {
            return GalleryResources.galleriesData();
        }

        function HandleGalleriesDataReady() {
            //console.log("Gallery data ready");

            // +1 = added welcome image
            vm.progressbarStep = (100 / (vm.albumsData().length + 1));
            vm.progressbar.set(vm.progressbarStep);
            //console.log(vm.progressbar.status());
            PreLoadPhotos();
        }

        function HandlePhotoPreLoadingFinished() {
            //console.log("Finished pre-loading all photos");
            vm.progressbar.complete();

            $timeout(function() {
                vm.ShowWelcomeElements = false;
                //vm.ShowWelcomeImage = false;
            }, (timeout - 1000), true);

            $timeout(function() {
                vm.ShowWelcomeImage = false;
                vm.StarAlbumsDisplayDirective = true;
            }, timeout, true);
        }

        function PreLoadPhotos() {
            var preLoaderArray = BuildPhotosPreLoaderArray();

            angular.forEach(preLoaderArray, function(photo) {
                ImageLoader.loadImage(photo).then(function(loadedString) {
                    //console.log("loaded photo: " + photo + " percent=" + vm.progressbar.status());
                    if (vm.progressbar.status() >= 100) {
                        HandlePhotoPreLoadingFinished();
                    }

                    vm.progressbar.set(vm.progressbar.status() + vm.progressbarStep);
                });
            });
        }

        function BuildPhotosPreLoaderArray() {
            var photos = [];
            photos.push(vm.albumsData()[0].ImagesLocation + "/welcome-p.jpg");

            angular.forEach(vm.albumsData(), function(album) {
                this.push(album.ImagesLocation + "/" + album.OpeningPhoto);
            }, photos);

            return photos;
        }
    }
}());