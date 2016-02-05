
var appMainModule = angular.module('AppMain', []); 
    

appMainModule.controller("AppMainModel", function ($scope) {
    $scope.test = "This is from the MAIN controller";
});