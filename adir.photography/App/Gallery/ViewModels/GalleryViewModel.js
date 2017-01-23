(function() {

    'use strict';

    angular
        .module('gallery')
        .controller('GalleryViewModel', GalleryViewModel);

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

        //console.log("Starting gallery controller");
        var vm = this;

        vm.galleryDataReady = false;
        vm.progressbar = ngProgressFactory.createInstance();
        vm.galleryData = GetGalleryData;
        vm.galleryName = "Main";
        vm.userName = "default";
        vm.serverError = false;
        vm.serverErrorMessage = "";
        vm.progressbarStep = 0;
        vm.ShowWelcomeElements = true;
        vm.StarSlideShowtDirective = false;
        vm.ShowWelcomeImage = true;
        var timeout = 3000;

        if ($routeParams.galleryId) {
            //console.log("setting gallery name from route to: " + $routeParams.galleryId);
            vm.galleryName = $routeParams.galleryId;
        }

        activate();

        function activate() {
            GalleryResources.getGalleryData(vm.galleryName)
                .then(
                    function(status) { // success
                        vm.galleryDataReady = status;
                        HandleGalleryDataReady();
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

        function GetGalleryData() {
            return GalleryResources.galleryData();
        }

        function HandleGalleryDataReady() {
            //console.log("Gallery data ready");
            vm.progressbarStep = (100 / (vm.galleryData().GalleryPhotos.length + 1));
            vm.progressbar.set(vm.progressbarStep);
            //console.log(vm.progressbar.status());
            PreLoadPhotos();
        }

        function HandlePhotoPreLoadingFinished() {
            //console.log("Finished pre-loading all photos");
            vm.progressbar.complete();

            $timeout(function() {
                vm.ShowWelcomeElements = false;
                vm.ShowWelcomeImage = false;
            }, (timeout - 1000), true);

            $timeout(function() {
                vm.ShowWelcomeImage = false;
                vm.StarSlideShowtDirective = true;
            }, timeout, true);
        }

        function PreLoadPhotos() {
            var preLoaderArray = BuildPhotosPreLoaderArray();

            angular.forEach(preLoaderArray, function(photo) {
                ImageLoader.loadImage(photo).then(function(loadedString) {

                    if (vm.progressbar.status() >= 100) {
                        HandlePhotoPreLoadingFinished();
                    }

                    vm.progressbar.set(vm.progressbar.status() + vm.progressbarStep);
                });
            });
        }

        function BuildPhotosPreLoaderArray() {
            var photos = [];
            photos.push(vm.galleryData().ImagesLocation + "/" + vm.galleryData().OpeningPhoto);

            angular.forEach(vm.galleryData().GalleryPhotos, function(photo) {
                this.push(vm.galleryData().ImagesLocation + "/" + photo.FileName);
            }, photos);

            return photos;
        }
    }
}());