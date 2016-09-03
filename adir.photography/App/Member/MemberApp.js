(function () {
    'use strict';

    angular.module('member', ['ngRoute', 'AppCommonServices', 'AppCommonDirectives'])

        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $routeProvider.when('/', {
                templateUrl: '/App/Member/Views/ProfileView.html',
                controller: 'ProfileViewModel'
            });

            $routeProvider.when('/member/profile', {
                templateUrl: '/App/Member/Views/ProfileView.html',
                controller: 'ProfileViewModel'
            });

            $routeProvider.otherwise({ redirectTo: '/' });

            $locationProvider.html5Mode(true);
        }])

        .run(['$log', '$route', '$rootScope', function($log, $route, $rootScope) {
            $log.log("Member App loaing...");


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

    }]);


}());