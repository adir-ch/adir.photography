(function() {
    angular
        .module('gallery')
        .directive('apMaxImage', MaxImageFunction);

    function MaxImageFunction() {
        var directive = {
            restrict: 'AE',
            scope: false,
            scope: {
                userName: '=',
                galleryName: '=',
                galleryData: '='
            },

            templateUrl: 'App/Gallery/Directives/MaxImage/MaxImageDirectiveView.html',
            controller: MaxImageDirectiveViewModel,
            link: MaxImageDirectiveLinkFunc
        }

        return directive;
    }

    function MaxImageDirectiveLinkFunc(scope, el, attr, ctrl) {
        //console.log("--------- Starting maximage directive link func----------");
        //InitMaxImage(scope.galleryData.Timeout);
        // el.ready(function() {
        //     InitMaxImage(scope.galleryData.Timeout);
        //     // $scope.$apply(function(){
        //     //     var func = $parse(attr.apMaxImage);
        //     //     func($scope);
        //     // })
        // });
    }

    MaxImageDirectiveViewModel.$inject = ['$rootScope', '$scope', '$timeout'];

    function MaxImageDirectiveViewModel($rootScope, $scope, $timeout) {
        //console.log("--------- Starting maximage directive ----------");
        // //console.debug("Gallery data: ", $scope.galleryData);
        // $scope.slideshowReady = false;
        $scope.nonOpeningPhotosArray = [];
        buildMaxImageGalleryPhotos($scope);

        var timer = $timeout(function() {
            //console.log("DOM finished loading, scope id: ", $scope.$id);
        }, 0, true);

        timer.then(
            function() {
                //console.log("timer resolved calling MaxImageFunction");
                InitMaxImage($scope.galleryData.Timeout);
            },
            function() {
                //console.log("timer rejected!", Date.now());
            }
        );

        $scope.$on("$destroy", function(event) {
            $timeout.cancel(timer);
        });

        // $scope.$on("MaxImageReadyEvent", function(event) {
        //     $timeout(function() {
        //         InitMaxImage($scope.galleryData.Timeout);
        //     }, 10000, true);
        // });
    }

    function InitMaxImage(slideChangeDelay) {
        //console.log("maximage start");
        jQuery('#maximage').maximage({
            cssBackgroundSize: false,
            backgroundSize: maximageBackground,
            cycleOptions: {
                fx: 'fade',
                speed: 1000,
                timeout: slideChangeDelay, // time to wait between changes
                prev: '#arrow_left',
                next: '#arrow_right',
                pause: 1, // pause when mouse hover on image

            },
            onFirstImageLoaded: function() {
                jQuery('#cycle-loader').hide();
                jQuery('#maximage').fadeIn('fast');
            }
        });

        // Helper function to Fill and Center the HTML5 Video
        jQuery('video,object').maximage('maxcover');

        // To show it is dynamic html text
        jQuery('.in-slide-content').delay(1200).fadeIn();
        //console.log("maximage end");
    }

    function maximageBackground($item) {
        //console.log("using self resize");
        // Contain portrait but cover landscape
        if ($item.data('h') > $item.data('w')) {
            if ($.Window.data('w') / $.Window.data('h') < $item.data('ar')) {
                $item
                    .height(($.Window.data('w') / $item.data('ar')).toFixed(0))
                    .width($.Window.data('w'));
            } else {
                $item
                    .height($.Window.data('h'))
                    .width(($.Window.data('h') * $item.data('ar')).toFixed(0));
            }
        } else {
            if ($.Window.data('w') / $.Window.data('h') < $item.data('ar')) {
                $item
                    .height($.Window.data('h'))
                    .width(($.Window.data('h') * $item.data('ar')).toFixed(0));
            } else {
                $item
                    .height(($.Window.data('w') / $item.data('ar')).toFixed(0))
                    .width($.Window.data('w'));
            }
        }
    }

    function buildMaxImageGalleryPhotos($scope) {
        angular.forEach($scope.galleryData.GalleryPhotos, function(photoObject) {
            if (photoObject.FileName != $scope.galleryData.OpeningPhoto) {
                $scope.nonOpeningPhotosArray.push(photoObject.FileName);
            }
        });
    }
}());