
angular.module('gallery').controller("GalleryViewModel", function ($scope, $http, $routeParams) {

    console.log("Gallery ViewModel");
    $scope.galleryData = [];
    $scope.galleryDataReady = false;
    $scope.galleryName = "main";
    $scope.userName = "default";

    if ($routeParams.galleryTag) {
        console.log("setting gallery name from route to: " + $scope.galleryName);
        $scope.galleryName = $routeParams.galleryTag;
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

    $scope.initialize();
});