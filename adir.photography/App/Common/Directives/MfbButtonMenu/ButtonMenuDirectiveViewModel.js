﻿
(function () {

	var ButtonMenuFunction = function () {

	    console.log("starting button menu directive");
	    return {
	        restrict: 'E',
	        templateUrl: 'App/Common/Directives/MfbButtonMenu/ButtonMenuDirectiveView.html',
	        controller: 'MfbButtonMenuViewModel',
	        controllerAs: 'ctrl'
	    };
	};

    angular.module('AppCommonDirectives').directive('apButtonMenu', ButtonMenuFunction);
}());