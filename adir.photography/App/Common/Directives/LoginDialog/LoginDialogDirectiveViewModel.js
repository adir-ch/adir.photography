
(function () {

	angular.module('AppCommonDirectives').controller('LoginDialogDirectiveViewModel',
			["$scope", "$location", "ngDialog", "WebApiService", "GlobalConfigurationService", LoginDialogDirectiveViewModel]);

	function LoginDialogDirectiveViewModel($scope, $location, ngDialog, WebApiService, GlobalConfigurationService) {
		console.log("Login dialog directive - start");

		$scope.userName = ""; // take data from login form
		$scope.loginFormInput = { userEmail: "", password: ""};
		$scope.authenticated = false;

		var CheckIfUserIsAuthenticated = function(userName) {
			console.log("Calling API to check authentication for userName: ", userName);
			var url = GlobalConfigurationService.url("idmAuthenticate");
			console.log("Calling API url: ", url);
			//var authenticated = WebApiService.apiGet("/idm/authenticateUser/userName");
			return false;
		}

		var DoUserLogin = function(username, password) {
			console.log("Login: user=", username, " pass=", password);
			var url = GlobalConfigurationService.url("idmLogin");
			console.log("Calling API url: ", url);
			//var authenticated = WebApiService.apiGet("/idm/authenticateUser/userName");
			$scope.userName = "Adir";
			$scope.authenticated = true;
		}

		function OpenLoginDialog() {
			ngDialog.openConfirm({
					className: 'ngdialog-theme-default',
					template: 'loginDialogPopupId',
					scope: $scope,
			}).then(
				function (inputData) {
					console.log("Form input data status: ", inputData, " data: ", $scope.loginFormInput);
					//DoUserLogin($scope.loginFormInput.userEmail, $scope.loginFormInput.password);
					showThankYouDialog($scope.loginFormInput.userEmail);
				},
				function(status) {
					console.log("Login form:", status);
				}
			).finally(function() {
				$scope.$emit('loginPopupDialogFinished', "");
			});
		}

		function showThankYouDialog(email) {
			var template = '<div class="login-dialog-thankyou-text">';
			if (email == "" || email === undefined) {
				template = template + "Try again, this time with an email address :)";
			} else {
				template = template + "Thank You"
			}

			template = template + "</div>";

			ngDialog.open({
                template: template,
                plain: true,
                closeByEscape: false,
            });
		}

		function Init() {
			if ($scope.userName != "" && CheckIfUserIsAuthenticated($scope.userName) == true) {
				console.log("User ", $scope.userName, " is authenticated, redirecting to user profile page");
				console.log("sending login event");
				$scope.$emit('loginPopupDialogFinished', "");
				// $location.path("/home/member/", $scope.userName); --------------> wait with that for now
				// can also redirect to the user gallery
			} else {
				console.log("Show login dialog - user is not authenticated");
				OpenLoginDialog();
			}
		}

		Init();


	}

	var LoginDialogDirectiveFunction = function () {
	    return {
	        restrict: 'E',
	        templateUrl: '/App/Common/Directives/LoginDialog/LoginDialogDirectiveView.html',
	        controller: 'LoginDialogDirectiveViewModel',
	        // controllerAs: 'vm'
	        // bindToController: true // if the scope is isolated

	  		// link: function ($scope, element, attrs) {

			// 	      $scope.$on('loginEvent', function(event, data) {
			// 	         console.log("got login event in link");
			// 	      });
			// },
		}
	}

    angular.module('AppCommonDirectives').directive('apLoginDialog', LoginDialogDirectiveFunction);
}());