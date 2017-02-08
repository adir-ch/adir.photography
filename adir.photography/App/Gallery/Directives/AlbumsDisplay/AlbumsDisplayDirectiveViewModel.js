(function() {
    angular
        .module('gallery')
        .directive('apAlbumsDisplay', AlbumsViewFunction);
    //console.log("Albums ViewModel");

    function AlbumsViewFunction() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'App/Gallery/Directives/AlbumsDisplay/AlbumsDisplayDirectiveView.html',
            scope: true,
            scope: {
                albumsData: '=data'
            },

            link: linkFunc,
            controller: AlbumsDisplayController,
            controllerAs: 'vm',
            bindToController: true // because the scope is isolated
        };

        return directive;
    }

    function linkFunc($scope, element, attrs) {
        // not implemented.
    }

    AlbumsDisplayController.$inject = ["$attrs"];

    function AlbumsDisplayController($attrs) {
        var vm = this;
        vm.albums = []; 
        
        RemoveMainPortraitAlbum(); 

        vm.SetAlbumPhotoStyle = function(album) {
            var css = {
                //'background-image': 'url(\'' + GenerateCssBackgroundImage(album) + '\')',
                "background-image": "cover"
            };
            return css;
        }

        vm.GenerateAlbumLink = function(albumName) {
            //console.log("setting album link to: /gallery/album/" + albumName);
            return "/gallery/album/" + albumName;
        }

        // var GenerateCssBackgroundImage = function (album) { // not used
        // 	return album.ImagesLocation + "/" + album.OpeningPhoto;
        // }

        vm.GenerateAlbumBackgroundImage = function(album) {
            //console.log(album.ImagesLocation + "/" + album.OpeningPhoto);
            return album.ImagesLocation + "/" + album.OpeningPhoto;
        }

        function  RemoveMainPortraitAlbum() {
            angular.forEach(vm.albumsData, function(album) {
                if (album.Name != "Main-Portrait") {
                    this.push(album);
                }
            }, vm.albums);
        }
    }
}());