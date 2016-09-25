

// NOT USED ANYMORE !!!!!!!!!!!!!!!!!!

(function () {

	angular.module('AppCommonDirectives').controller('LoginDialogFormViewModel',
			["$scope", "$location", "WebApiService", "GlobalConfigurationService", LoginDialogFormViewModel]);

	function LoginDialogFormViewModel($scope, $location, WebApiService, GlobalConfigurationService) {
		console.log("Login dialog form - start");

		function Init() {

		}

		$scope.submit = function() {
			console.log("Form input data: ", $scope.user);
			console.log("Will call API to attempt login - future build");

		}


		Init();

	}

}());