(function() {
    angular
        .module('gallery')
        .directive('apMaxImageInner', MaxImageInnerFunction);

    function MaxImageInnerFunction() {
        var directive = {
            restrict: 'AE',
            scope: false,
            scope: {
                galleryData: '='
            },

            templateUrl: 'App/Gallery/Directives/MaxImage/MaxImageInnerDirectiveView.html',
            controller: MaxImageInnerDirectiveViewModel,
            link: MaxImageInnerDirectiveLinkFunc
        }

        return directive;
    }

    function MaxImageInnerDirectiveLinkFunc(scope, el, attr, ctrl) {
        console.log("--------- Starting maximage inner directive link func----------");

        el.ready(function() {
            scope.$emit("MaxImageReadyEvent");
            // $scope.$apply(function(){
            //     var func = $parse(attr.apMaxImage);
            //     func($scope);
            // })
        });
    }

    MaxImageInnerDirectiveViewModel.$inject = ['$rootScope', '$scope', '$timeout'];

    function MaxImageInnerDirectiveViewModel($rootScope, $scope, $timeout) {
        console.log("--------- Starting maximage inner directive ----------");
        $scope.nonOpeningPhotosArray = [];
        buildMaxImageGalleryPhotos($scope);
    }

    function buildMaxImageGalleryPhotos($scope) {
        angular.forEach($scope.galleryData.GalleryPhotos, function(photoObject) {
            if (photoObject.FileName != $scope.galleryData.OpeningPhoto) {
                $scope.nonOpeningPhotosArray.push(photoObject.FileName);
            }
        });
    }
}());