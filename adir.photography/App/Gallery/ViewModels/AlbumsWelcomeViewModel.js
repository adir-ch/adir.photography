(function() {
    angular.module('gallery').controller("AlbumsWelcomeViewModel", ['$scope', '$routeParams', '$window', '$location', '$timeout', 'ngProgressFactory', 'GalleryResources',

        function($scope, $routeParams, $window, $location, $timeout, ngProgressFactory, GalleryResources) {

            console.log("Albums welcome ViewModel");

            var timeout = 5000;

            $scope.serverError = false;
            $scope.serverErrorMessage = "";
            $scope.ShowWelcomeImage = true;
            $scope.ShowWelcomeElements = true;
            $scope.progressbar = ngProgressFactory.createInstance();

            Initialize = function() {

                $scope.progressbar.start();

                $timeout(function() {
                    $scope.ShowWelcomeElements = false;
                }, (timeout - 1000), true);

                $timeout(function() {
                    //console.log("Count finished - showing gallery");
                    $scope.progressbar.complete();
                    $scope.ShowWelcomeImage = false;
                    console.log("change location")
                    $window.location.href = "/gallery/albums";
                }, timeout, true);

            }

            Initialize();
        }
    ]);
}());