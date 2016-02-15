
angular.module('gallery').controller("GalleryViewModel", function ($scope, $http, $routeParams, $route, $log) {

    console.log("Gallery ViewModel");
    $scope.galleryData = [];
    $scope.galleryDataReady = false;
    $scope.galleryName = "Main";
    $scope.userName = "default";

    if ($routeParams.galleryId) {
        console.log("setting gallery name from route to: " + $routeParams.galleryId);
        $scope.galleryName = $routeParams.galleryId;
    }

    $scope.initialize = function () {

        // get all the images + opening image.
        console.log("calling Gallery API");
        console.log("Asking for gallery: " + $scope.galleryName);

        $http.get('/api/galleryapi/')
            .then(
                function (result) {
                    $scope.galleryData = result.data;
                    $scope.galleryDataReady = true;
                    console.log("Gallery data ready");
                },
                function () { //error

                }
            );
    }

    console.log("Gallery scope: id:", $scope.$id);
    $scope.initialize();
});