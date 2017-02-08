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
        var timeout = 3000;
        var isLandscapeView = true; 

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
        vm.galleryWelcomeId = "ap-gallery-welcome-landscape";
        
        activate();

        function activate() {
            AdjustGalleryViewSettings();
            SetGalleryName(); 
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

        function SetGalleryName() {
            if ($routeParams.galleryId) {
                //console.log("setting gallery name from route to: " + $routeParams.galleryId);
                vm.galleryName = $routeParams.galleryId;
            } else {
                if (isLandscapeView == true) {
                    vm.galleryName = "Main"; 
                } else {
                    vm.galleryName = "Main-Portrait";
                }
            }
        }

        function GetGalleryData() {
            return GalleryResources.galleryData();
        }

        function HandleGalleryDataReady() {
            //console.log("Gallery data ready");

            // +1 = added welcome image
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
            PreLoadWelcomePhoto(); // make sure welcome photo is loaded before all the rest!
        }

        function PreLoadWelcomePhoto() {
            var welcomePhoto = vm.galleryData().ImagesLocation + "/welcome.jpg";

            if (isLandscapeView == false) {
                welcomePhoto = vm.galleryData().ImagesLocation + "/welcome-p.jpg";
            }

            ImageLoader.loadImage(welcomePhoto).then(function(loadedString) {
                vm.progressbar.set(vm.progressbar.status() + vm.progressbarStep);
                PreLoadGalleryPhotos();
            });
        }

        function PreLoadGalleryPhotos() {
            var preLoaderArray = BuildPhotosPreLoaderArray();

            angular.forEach(preLoaderArray, function(photo) {
                ImageLoader.loadImage(photo).then(function(loadedString) {
                    //console.log("loaded photo: " + photo + " percent=" + vm.progressbar.status());
                    if (vm.progressbar.status() >= 99.5) {
                        HandlePhotoPreLoadingFinished();
                    }

                    vm.progressbar.set(vm.progressbar.status() + vm.progressbarStep);
                });
            });
        }

        function BuildPhotosPreLoaderArray() {
            var photos = [];

            angular.forEach(vm.galleryData().GalleryPhotos, function(photo) {
                this.push(vm.galleryData().ImagesLocation + "/" + photo.FileName);
            }, photos);

            return photos;
        }

        function AdjustGalleryViewSettings() {
            if ($window.innerWidth < $window.innerHeight) { // portrait
                isLandscapeView = false;
                vm.galleryWelcomeId = "ap-gallery-welcome-portrait";
                if ($window.innerWidth < 961) {
                    //mobile = true;
                }
            } else { // landscape 
                isLandscapeView = true;
                vm.galleryWelcomeId = "ap-gallery-welcome-landscape";
                if ($window.innerHeight < 961) {
                    //mobile = true;
                }
            }
        }
    }
}());