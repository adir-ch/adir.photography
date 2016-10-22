(function() {

    var PhotoswipeFunction = function($timeout) {
        return {
            restrict: 'AE',
            scope: false,
            scope: {
                userName : '=',
                galleryName: '=',
                galleryData: '='
            },

            templateUrl: 'App/Gallery/Directives/Photoswipe/PhotoswipeDirectiveView.html',

            controller: function($scope, $timeout, $http) {
                console.log("--------- Starting photoswipe directive ----------");
                console.debug("Gallery name: " + $scope.galleryName);
                console.debug("User name: " + $scope.userName);
                console.debug($scope.galleryData);
                console.log("Waiting for DOM to load, scope id: ", $scope.$id);
                //$scope.$emit('maxImageLoaded'); // sending event to the GalleryViewModel

                $scope.initPhotoswipe = function() {
                    console.log("Photoswipe start");
                    var pswpElement = document.querySelectorAll('.pswp')[0];

                    // build items array
                    var items = [{
                        src: 'http://localhost:61001/Content/Photos/sa7.jpg',
                        w: 600,
                        h: 400
                    }, {
                        src: 'http://localhost:61001/Content/Photos/us1.jpg',
                        w: 1200,
                        h: 900
                    }];

                    // define options (if needed)
                    var options = {
                        // optionName: 'option value'
                        // for example:
                        index: 0 // start at first slide
                    };

                    // Initializes and opens PhotoSwipe
                    console.log("lunching PS gallery")
                    var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
                    gallery.init();
                }

                var timer = $timeout(function() {
                    console.log("DOM finished loading, scope id: ", $scope.$id);
                }, 0, false);

                timer.then(
                    function() {
                        console.log("timer resolved calling PhotoswipeFunction");
                        $scope.initPhotoswipe();
                    },
                    function() {
                        console.log("timer rejected!", Date.now());
                    }
                );

                $scope.$on("$destroy", function( event ) {
                    $timeout.cancel(timer);
                    console.log("timer removed");
                });
            }
        };
    }

    angular.module('gallery').directive('apPhotoswipe', PhotoswipeFunction);
}());