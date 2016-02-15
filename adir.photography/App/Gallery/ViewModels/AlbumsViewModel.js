
angular.module('gallery').controller("AlbumsViewModel", function ($scope, $http, $route, $log) {
    console.log("Albums ViewModel");
    var initialize = function () {
    	$scope.where = "This is from the Albums controller";
    }

    initialize();
});
