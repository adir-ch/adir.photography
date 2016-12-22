
(function () {

	angular.module('AppCommonDirectives').controller('LoginDialogDirectiveViewModel',
			["$scope", "$location", "ngDialog", "WebApiService", "GlobalConfigurationService", LoginDialogDirectiveViewModel]);

	function LoginDialogDirectiveViewModel($scope, $location, ngDialog, WebApiService, GlobalConfigurationService) {
		//console.log("Login dialog directive - start");

		$scope.userName = ""; // take data from login form
		$scope.loginFormInput = { userEmail: "", password: ""};
		$scope.authenticated = false;

		var CheckIfUserIsAuthenticated = function(userName) {
			//console.log("Calling API to check authentication for userName: ", userName);
			var url = GlobalConfigurationService.url("idmAuthenticate");
			//console.log("Calling API url: ", url);
			//var authenticated = WebApiService.apiGet("/idm/authenticateUser/userName");
			return false;
		}

		var DoUserLogin = function(username, password) {
			//console.log("Login: user=", username, " pass=", password);
			var url = GlobalConfigurationService.url("idmLogin");
			//console.log("Calling API url: ", url);
			//var authenticated = WebApiService.apiGet("/idm/authenticateUser/userName");
			$scope.userName = "Adir";
			$scope.authenticated = true;
		}

		function PostDataToServer(apiUrl, data) {
            //console.log("calling api with: ", apiUrl);
            WebApiService.apiPost(apiUrl, data)
                .then(function(response) {
                    //console.log("WebApi call success");
                    showThankYouDialog("Thank You, " + $scope.loginFormInput.userEmail);
                },
                function(response) {
                    console.log("WebApi call failed");
                  	showThankYouDialog("Failed, try again later...");
                }
            ).catch(function(response) {
                console.log("Exception while calling WebApi service");
                throw response;
            });
        }

		function DoSubscriberAdd(emailAddress) {
			if (emailAddress === undefined || emailAddress === "") {
				showThankYouDialog("Please supply a valid email address");
				return;
			}

			var data = {
				Name: '',
				Email: emailAddress,
				Location: ''
			};

			var url = GlobalConfigurationService.url("addNewInfoSubscriber");
			PostDataToServer(url, data);
		}

		function OpenLoginDialog() {
			ngDialog.openConfirm({
					className: 'ngdialog-theme-default',
					template: 'loginDialogPopupId',
					scope: $scope,
			}).then(
				function (inputData) {
					//console.log("Form input data status: ", inputData, " data: ", $scope.loginFormInput);
					//DoUserLogin($scope.loginFormInput.userEmail, $scope.loginFormInput.password);
					DoSubscriberAdd($scope.loginFormInput.userEmail);
				},
				function(status) {
					//console.log("Login form:", status);
				}
			).finally(function() {
				$scope.$emit('loginPopupDialogFinished', "");
			});
		}

		function showThankYouDialog(message) {
			var template = '<div class="login-dialog-thankyou-text">' + message + '</div>';

			ngDialog.open({
                template: template,
                plain: true,
                closeByEscape: false,
            });
		}

		function Init() {
			if ($scope.userName != "" && CheckIfUserIsAuthenticated($scope.userName) == true) {
				//console.log("User ", $scope.userName, " is authenticated, redirecting to user profile page");
				//console.log("sending login event");
				$scope.$emit('loginPopupDialogFinished', "");
				// $location.path("/home/member/", $scope.userName); --------------> wait with that for now
				// can also redirect to the user gallery
			} else {
				//console.log("Show login dialog - user is not authenticated");
				OpenLoginDialog();
			}
		}

		Init();
	}

	var LoginDialogDirectiveFunction = function () {
	    return {
	        restrict: 'E',
	        templateUrl: 'App/Common/Directives/LoginDialog/LoginDialogDirectiveView.html',
	        controller: 'LoginDialogDirectiveViewModel',
	        // controllerAs: 'vm'
	        // bindToController: true // if the scope is isolated

	  		// link: function ($scope, element, attrs) {

			// 	      $scope.$on('loginEvent', function(event, data) {
			// 	         //console.log("got login event in link");
			// 	      });
			// },
		}
	}

    angular.module('AppCommonDirectives').directive('apLoginDialog', LoginDialogDirectiveFunction);
}());