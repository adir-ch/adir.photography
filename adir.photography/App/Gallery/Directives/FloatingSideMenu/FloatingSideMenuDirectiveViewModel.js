(function () {
    angular
        .module('gallery')
        .directive('apFloatingSideMenu', FloatingSideMenuFunction);

    function FloatingSideMenuFunction($window) {
        var directive = {
            restrict: 'EA',
            templateUrl: 'App/Gallery/Directives/FloatingSIdeMenu/FloatingSideMenuDirectiveView.html',
            scope: true,
            scope: {
                albumsData: '=data'
            },

            link: linkFunc,
            controller: FloatingSideMenuDisplayController,
            controllerAs: 'vm',
            bindToController: true // because the scope is isolated
        };

        return directive;

        function linkFunc(scope, element, attrs) {
            angular.element($window).bind("scroll", function() {
                //console.log("scroll");
                
                scope.$apply(function() {
                    scope.vm.UpdateMenuItemsStyle();
                });
            });
        }
    }

    FloatingSideMenuDisplayController.$inject = ["$attrs", "$location", "$anchorScroll", "$timeout"];

    function FloatingSideMenuDisplayController($attrs, $location, $anchorScroll, $timeout) {
        var vm = this;
        vm.albums = [];
        vm.MenuLinkClicked = MenuLinkClicked; 
        vm.UpdateMenuItemsStyle = UpdateMenuItemsStyle;
        vm.SetMenuLinkStyle = SetMenuLinkStyle;

        activate(); 

        function activate() {
            //console.log("floating menu init", vm.albums);
            ExtractAlbumsName();

            $timeout(function() {
                UpdateMenuItemsStyle(); 
            }, 100, true);
            
        }

        function ExtractAlbumsName() {
            angular.forEach(vm.albumsData, function (album) {
                if (album.Name != "Main-Portrait") {
                    this.push(album.Name);
                }
            }, vm.albums);
        }

        function MenuLinkClicked(linkName) {
            //console.log("menu click: ", linkName);
            var menuItem = angular.element(document).find("#" + linkName);
            var albumName = linkName.split("-", 2)[1];
            ScrollToElement(albumName); 
        }

        function ScrollToElement(albumName) {
            //console.log("Scrolling to: " + albumName); 
            var oldHash = $location.hash();
            $location.hash(albumName); 
            $anchorScroll(); 

            // have to restore old hash, otherwise the route mechanisem will 
            // trigger a full page render again!
            $location.hash(oldHash);
        }

        function isElementOutViewport(albumName) {
            var albumElement = angular.element(document).find("#" + albumName);
            if(albumElement.length == 0) { // item does not exist on page
                return false; 
            }

            var rect = albumElement[0].getBoundingClientRect();
            return rect.bottom < 0 || rect.right < 0 || rect.left > window.innerWidth || rect.top > window.innerHeight;
        }

        function UpdateMenuItemsStyle() {
            //console.log("updating style...");
            angular.forEach(vm.albums, function(album) {
                SetMenuLinkStyle(album); 
            });
        }

        function SetMenuLinkStyle(albumName) {
            var textColor = "white"; 
            var zoom = "100%";
            if (isElementOutViewport(albumName) == false) {
                //console.log("Album " + albumName + " is visable");
                textColor = "#213765";
                var zoom = "110%";
            } 

            return {
                "text-decoration": "none", 
                "color" : textColor,
                //"zoom" : zoom,
            };
        }
    }
}());