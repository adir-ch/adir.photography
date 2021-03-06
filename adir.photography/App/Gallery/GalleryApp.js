﻿(function() {
    'use strict';

    angular.module('gallery', ['ngRoute', 'ngAnimate', 'ngProgress', 'sap.imageloader', 'AppCommonServices'])

    .config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {
        $routeProvider.when('/', {
            templateUrl: 'App/Gallery/Views/GalleryViewV2.html',
            controller: 'GalleryViewModel',
            controllerAs: 'vm'
        });

        $routeProvider.when('/gallery/show/:galleryId', {
            templateUrl: '/App/Gallery/Views/GalleryViewV2.html',
            controller: 'GalleryViewModel',
            controllerAs: 'vm'
        });

        $routeProvider.when('/gallery/albums', {
            templateUrl: 'App/Gallery/Views/AlbumsView.html',
            controller: 'AlbumsViewModel'
        });

        $routeProvider.when('/gallery/albums/welcome', {
            templateUrl: 'App/Gallery/Views/AlbumsWelcomeView.html',
            controller: 'AlbumsWelcomeViewModel',
            controllerAs: 'vm'
        });

        $routeProvider.when('/gallery/album/:albumId', {
            templateUrl: 'App/Gallery/Views/AlbumView.html',
            controller: 'AlbumViewModel'
        });

        $routeProvider.otherwise({ redirectTo: '/' });

        $locationProvider.html5Mode(true);
    }])

    .run(['$log', '$route', '$rootScope', '$location', '$window', function($log, $route, $rootScope, $location, $window) {

        //$log.debug("Gallery App loaing...");

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

        function isMainPageRoute(route) {
            //return (route.indexOf("/gallery/album/:albumId") === -1);
            if (route === "/" || route === "/gallery/") {
                return true;
            }

            return false;
        }

        // supported events: $routeChangeError, $routeChangeSuccess
        $rootScope.$on('$routeChangeStart', function(event, current, previous) {
            //$log.debug("route change start: ", $location.path(), " current: ", current.originalPath);

            var route = $location.path();
            if (current.originalPath !== undefined) {
                route = current.originalPath;
            }

            if (isMainPageRoute(route) == true) { // not going to album page  
                //$log.debug("going to main page, route:", route);
                adjustGalleryViewSettings();
                if (portrait == true) { // portrait - show albums
                    //console.log("portrait");
                    //$location.path("/gallery/albums/welcome");
                }
            }
        });
    }]);
}());