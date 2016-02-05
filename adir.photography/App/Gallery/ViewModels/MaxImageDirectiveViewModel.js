(function() {

    var MaxImageFunction = function() {
        return {
            restrict: 'AE',
            scope: false,
            scope: {
                userName : '=',
                galleryName: '=',
                galleryData: '='
            },

            templateUrl: 'App/Gallery/Views/MaxImageDirectiveView.html',

            // link: function($scope, $element, $attributes) {

            //     console.log("link function");

            //     $scope.$watch($element, function() {
            //         console.log("Elelment changed");
            //         console.log($element);
            //     });
            // },

            // compile: function($element, $attributes) {
            //     return {
            //         pre: function($scope, $element, $attributes, $controller, $transcludeFn){
            //          // console.log("pre compile");
            //         },

            //         post: function($scope, $element, $attributes, $controller, $transcludeFn){
            //          console.log("post compile");
            //         }
            //     }
            // },

            controller: function($scope, $timeout, $http) {
                console.log("--------- Starting maximage dispaly ----------");
                console.log("Gallery name: " + $scope.galleryName);
                console.log("User name: " + $scope.userName);
                console.log($scope.galleryData);
                //$scope.$emit('maxImageLoaded'); // sending event to the GalleryViewModel

                $scope.initMaxImage = function() {
                    console.log("maximage start");
                    jQuery('#maximage').maximage({
                        cycleOptions: {
                            fx: 'fade',
                            speed: 1000,
                            timeout: $scope.galleryData.Timeout, // time to wait between changes
                                prev: '#arrow_left',
                                next: '#arrow_right',
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

                 $timeout(function () { // You might need this timeout to be sure its run after DOM render.
                    $scope.initMaxImage();
                 }, 0, false);
            }
        };
    }

    angular.module('gallery').directive('apMaxImage', MaxImageFunction);
}());