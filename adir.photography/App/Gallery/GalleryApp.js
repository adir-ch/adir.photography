﻿(function () {
    'use strict';

    angular.module('gallery', ['ngRoute'])

        .config(function ($routeProvider, $locationProvider) {
            $routeProvider.when('/gallery', {
                templateUrl: '/App/Gallery/Views/GalleryView.html',
                controller: 'GalleryViewModel'
            });

            $routeProvider.when('/gallery/albums', {
                templateUrl: '/App/Gallery/Views/AlbumsView.html',
                controller: 'AlbumsViewModel'
            });

            $routeProvider.when('/gallery/show/:galleryId', {
                templateUrl: '/App/Gallery/Views/GalleryView.html',
                controller: 'GalleryViewModel'
            });

            $routeProvider.otherwise({ redirectTo: '/gallery' });

            $locationProvider.html5Mode(true);
        })

        .run(function($log, $route, $rootScope) {
            $log.log("Gallery App loaing...");


            $rootScope.$on('$routeChangeError', function(event, current, previous, rejection) {
                $log.debug("route change error");
                $log.debug(event);
                $log.debug($route.routes);
                $log.debug($route.current);
                $log.debug(rejection);
            });

            $rootScope.$on('$routeChangeSuccess', function(event, current, previous) {
                $log.debug("route change success");
                $log.debug($route.current);
            });

            $rootScope.$on('$routeChangeStart', function(event, current, previous) {
                $log.debug("route change start");
                $log.debug(event);

            });

    });


}());