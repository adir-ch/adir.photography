
(function () {

	angular.module('AppCommonDirectives').controller('LoginDialogViewModel', LoginDialogViewModel);

	function LoginDialogViewModel() {
		console.log("Login dialog directive - start");
		var vm = this;
		vm.user = "ADIR";

	}

	var LoginDialogDirectiveFunction = function () {
	    return {
	        restrict: 'E',
	        templateUrl: '/App/Common/Directives/LoginDialog/LoginDialogDirectiveView.html',
	        controller: 'LoginDialogViewModel',
	        controllerAs: 'vm'
	        // bindToController: true // if the scope is isolated
	    };
	};

    angular.module('AppCommonDirectives').directive('apLoginDialog', LoginDialogDirectiveFunction);
}());