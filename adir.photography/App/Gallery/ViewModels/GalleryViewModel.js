(function() {
    angular.module('gallery').controller("GalleryViewModel", ['$scope', '$routeParams', '$window', '$location', '$timeout', 'ngProgressFactory', 'GalleryResources',

        function($scope, $routeParams, $window, $location, $timeout, ngProgressFactory, GalleryResources) {

            //console.log("Gallery ViewModel");

            var timeout = 10000;

            $scope.galleryDataReady = false;
            $scope.StarSlideShowtDirective = false;
            $scope.galleryName = "Main";
            $scope.userName = "default";
            $scope.serverError = false;
            $scope.serverErrorMessage = "";
            $scope.isMobileView = false;
            $scope.isLandscapeView = false;
            $scope.ShowWelcomeImage = true;
            $scope.ShowWelcomeElements = true;
            $scope.progressbar = ngProgressFactory.createInstance();

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
                $scope.progressbar.start();
                GalleryResources.getGalleryData($scope.galleryName)
                    .then(
                        function(status) { // success
                            //console.log("Gallery data ready");
                            $scope.galleryDataReady = status;

                            // fade welcome text 1 sec erlier 
                            $timeout(function() {
                                $scope.ShowWelcomeElements = false;
                            }, (timeout - 1000), true);

                            $timeout(function() {
                                //console.log("Count finished - showing gallery");
                                $scope.progressbar.complete();
                                $scope.ShowWelcomeImage = false;

                                // to prevent directive CSS from being loaded. 
                                $scope.StarSlideShowtDirective = $scope.galleryDataReady;
                            }, timeout, true);
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
        }
    ]);
}());