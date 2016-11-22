(function() {
    'use strict';

    angular.module('gallery', ['ngRoute', 'AppCommonServices'])

    .config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {
        $routeProvider.when('/', {
            templateUrl: '/App/Gallery/Views/GalleryView.html',
            controller: 'GalleryViewModel'
        });

        $routeProvider.when('/gallery/show/:galleryId', {
            templateUrl: '/App/Gallery/Views/GalleryView.html',
            controller: 'GalleryViewModel'
        });

        $routeProvider.when('/gallery/albums', {
            templateUrl: '/App/Gallery/Views/AlbumsView.html',
            controller: 'AlbumsViewModel'
        });

        $routeProvider.when('/gallery/album/:albumId', {
            templateUrl: '/App/Gallery/Views/AlbumView.html',
            controller: 'AlbumViewModel'
        });

        $routeProvider.otherwise({ redirectTo: '/' });

        $locationProvider.html5Mode(true);
    }])

    .run(['$log', '$route', '$rootScope', '$location', '$window', function($log, $route, $rootScope, $location, $window) {

        var portrait = false;
        var mobile = false;

        function adjustGalleryViewSettings() {
            if ($window.innerWidth < $window.innerHeight) { // portrait
                portrait = true;
                if ($window.innerWidth < 961) {
                    mobile = true;
                }
            } else { // landscape 
                portrait = false;
                if ($window.innerHeight < 961) {
                    mobile = true;
                }
            }
        }

        //$log.debug("Gallery App loaing...");

        // supported events: $routeChangeError, $routeChangeSuccess
        $rootScope.$on('$routeChangeStart', function(event, current, previous) {
            //$log.debug("route change start");
            //$log.debug(event);
            adjustGalleryViewSettings();
            if (portrait == true) { // portrait - show albums
                console.log("portrait");
                $location.path("/gallery/albums");
            }
        });
    }]);
}());