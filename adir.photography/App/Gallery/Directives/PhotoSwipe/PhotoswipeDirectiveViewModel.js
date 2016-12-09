(function() {

    angular
        .module('gallery')
        .directive('apPhotoswipe', PhotoswipeFunction);

    function PhotoswipeFunction() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'App/Gallery/Directives/Photoswipe/PhotoswipeDirectiveView.html',
            //link: linkFunc,
            controller: PhotoswipeController,
            controllerAs: 'vm',
            bindToController: true,

            scope: {
                userName: '=',
                galleryName: '=',
                galleryData: '='
            }
        };

        return directive;
    }

    PhotoswipeController.$inject = ["$scope"];

    function PhotoswipeController($scope) {
        var vm = this;
        vm.photoSwipeArray = [];
        vm.setCellStyle = setCellStyleFunction;
        
        //console.log("--------- Starting photoswipe directive ----------");
        //console.log("Album: " + vm.galleryName);
        buildPhotoSwipeArray(vm.galleryData, vm.photoSwipeArray);

        vm.photoClicked = function(arrayIndex) {
            //console.log("Photo clicked: " + vm.galleryData.GalleryPhotos[arrayIndex].FileName);
            initPhotoswipe(vm.photoSwipeArray, arrayIndex);
        }

        
    }

    function linkFunc($scope, element, attrs) {

    }

    function setCellStyleFunction(photo) {
        if (photo.Metadata.Width > photo.Metadata.Height) {
            return "rig-img-landscape";
        } else {
            return "rig-img-portrait";
        }
    }

    function buildPhotoSwipeArray(dataArray, photoSwipeArray) {
        angular.forEach(dataArray.GalleryPhotos, function(value) {
            photoSwipeArray.push({
                src: (dataArray.ImagesLocation + "/" + value.FileName),
                w: value.Metadata.Width,
                h: value.Metadata.Height
            });
        });
    }

    function initPhotoswipe(photoArray, startFromIndex) {
        //console.log("Photoswipe start");
        var pswpElement = document.querySelectorAll('.pswp')[0];

        // define options (if needed)
        var options = {
            history: false, // avoid route problems
            index: startFromIndex, // start at first slide
            showAnimationDuration: 300
        };

        // Initializes and opens PhotoSwipe
        //console.log("lunching PS gallery")
        var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, photoArray, options);
        gallery.init();
    }

}());