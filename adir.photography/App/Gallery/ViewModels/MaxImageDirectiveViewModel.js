(function() {

    var MaxImageFunction = function($timeout) {
        return {
            restrict: 'AE',
            scope: false,
            scope: {
                userName : '=',
                galleryName: '=',
                galleryData: '='
            },

            templateUrl: 'App/Gallery/Views/MaxImageDirectiveView.html',

            controller: function($scope, $timeout, $http) {
                console.log("--------- Starting maximage directive ----------");
                console.debug("Gallery name: " + $scope.galleryName);
                console.debug("User name: " + $scope.userName);
                console.debug($scope.galleryData);
                console.log("Waiting for DOM to load, scope id: ", $scope.$id);
                //$scope.$emit('maxImageLoaded'); // sending event to the GalleryViewModel

                $scope.initMaxImage = function() {
                    console.log("maximage start");
                    jQuery('#maximage').maximage({
                        cycleOptions: {
                            fx: 'fade',
                            speed: 1000,
                            timeout: $scope.galleryData.Timeout, // time to wait between changes
                                prev: '#arrow_left',
                                next: '#maximage',
                                pause: 1,  // pause when mouse hover on image

                        },
                        onFirstImageLoaded: function () {
                            jQuery('#cycle-loader').hide();
                            jQuery('#maximage').fadeIn('fast');
                        }
                    });

                    // Helper function to Fill and Center the HTML5 Video
                    jQuery('video,object').maximage('maxcover');

                    // To show it is dynamic html text
                    jQuery('.in-slide-content').delay(1200).fadeIn();
                    console.log("maximage end");
                }

                var timer = $timeout(function() {
                    console.log("DOM finished loading, scope id: ", $scope.$id);
                }, 0, false);

                timer.then(
                    function() {
                        console.log("timer resolved calling MaxImageFunction");
                        $scope.initMaxImage();
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

    angular.module('gallery').directive('apMaxImage', MaxImageFunction);
}());