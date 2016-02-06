
var appMainModule = angular.module('AppMain', ['mfb-button-menu']); 
    

appMainModule.controller("AppMainModel", function ($scope) {
    $scope.test = "This is from the MAIN controller";
});