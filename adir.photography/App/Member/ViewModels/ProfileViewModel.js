(function() {
    angular.module('member').controller("ProfileViewModel", [ '$scope', '$routeParams', 'MemberResources', function ($scope, $routeParams, MemberResources) {

        console.log("Member Profile ViewModel");

        $scope.currentMemeberAuthenticated = false;
        $scope.currentLoggedInMemeber = "adirc";
        $scope.memberData = {};

        $scope.initialize = function () {

            // get all the images + opening image.
            console.log("calling Member API");
            console.log("Asking for member: " + $scope.currentLoggedInMemeber);

            // first check if a member is logged in - and if so, get the memers details,
            // if not - lunch the login dialog!
            if ($scope.currentMemeberAuthenticated == true) {
                MemberResources.getMemberData($scope.currentLoggedInMemeber)
                    .then(
                        function(response) { // success
                            console.log("Member data ready");
                            $scope.memberData = response;
                            $scope.memberDataReady = status;
                        },
                        function(reason) { // error
                            $scope.serverError = true;
                            console.log("Error while talking to server: ", reason);
                            $scope.serverErrorMessage = reason;
                        }
                    ).catch(function(exception) {
                        console.log("Exception while asking for member (", $scope.currentLoggedInMemeber, ") data: ", exception);
                    });
            } else {
                console.log("user must login - activating Login dialog directive");
            }
        }

        $scope.initialize();
    }]);
}());