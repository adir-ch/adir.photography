
(function () {

	angular
		.module('AppCommonDirectives')
		.directive('apButtonMenu', ButtonMenuFunction);

	function ButtonMenuFunction() {

	    //console.log("starting button menu directive");
	    return {
	        restrict: 'E',
	        templateUrl: 'App/Common/Directives/MfbButtonMenu/ButtonMenuDirectiveView.html',
	        controller: 'MfbButtonMenuViewModel',
	        controllerAs: 'ctrl'
	    };
	};
}());