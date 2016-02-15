
//(function() {
	var app = angular.module('AppMain', ['mfb-button-menu']);

	console.log("Starting Main App");
    var ButtonMenuFunction = function() {

    	console.log("starting button menu directive");
        return {
            restrict: 'E',
            templateUrl: 'App/Common/Views/ButtonMenuDirectiveView.html',
            controller: 'mfbMenuViewModel',
            controllerAs: 'ctrl'
        };
    }

    angular.module('AppMain').directive('apButtonMenu', ButtonMenuFunction);
//}());