
(function () {

	var ButtonMenuFunction = function () {

	    console.log("starting button menu directive");
	    return {
	        restrict: 'E',
	        templateUrl: 'App/Common/Directives/ButtonMenuDirectiveView.html',
	        controller: 'mfbMenuViewModel',
	        controllerAs: 'ctrl'
	    };
	};

    angular.module('AppCommonDirectives').directive('apButtonMenu', ButtonMenuFunction);
}());