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
        //InitMaxImage(scope.galleryData.Timeout);
    }

    MaxImageDirectiveViewModel.$inject = ['$scope', '$timeout'];

    function MaxImageDirectiveViewModel($scope, $timeout) {
        //console.log("--------- Starting maximage directive ----------");
        //console.debug("Gallery data: ", $scope.galleryData);
        $scope.slideshowReady = false;
        $scope.nonOpeningPhotosArray = [];
        buildMaxImageGalleryPhotos($scope);

        var timer = $timeout(function() {
            //console.log("DOM finished loading, scope id: ", $scope.$id);
        }, 0, true);

        timer.then(
            function() {
                console.log("timer resolved calling MaxImageFunction");
                $scope.slideshowReady = true;
                InitMaxImage($scope.galleryData.Timeout);
            },
            function() {
                //console.log("timer rejected!", Date.now());
            }
        );

        $scope.$on("$destroy", function(event) {
            $timeout.cancel(timer);
        });
    }

    function InitMaxImage(slideChangeDelay) {
        console.log("maximage start");
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

        console.log("maximage here1");
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