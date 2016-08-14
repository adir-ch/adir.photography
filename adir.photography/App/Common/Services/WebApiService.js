
(function () {
    "use strict";

    var _result;

    angular.module("AppCommonServices").constant("WebApiServerSettings", {
		ServerPath: "http://localhost:61001"
    });

	angular.module("AppCommonServices").factory("WebApiService", ['$http', '$q', '$exceptionHandler', 'WebApiServerSettings', WebApiService]);

	function WebApiService($http, $q, $exceptionHandler) {

		var _onSuccess = function(response) {
			console.log("Got new data from server");
		}

		var _onFailure = function(response) {
			console.log("Could not get new data from server: ", response);
			//throw response.data.ExceptionMessage;
		}

		var _apiGet = function(uri) {

			return $http.get(uri)
                .then(function(response) {
					_onSuccess(response);
					return response.data; // resolve the promise of whever called me with the data
                }, function(response) {
					console.info("Server error, calling failur");
					_onFailure(response);
					throw response;
                })
                .catch(function(response) {
                    throw "Got exception while connecting server";
                });
        }

        return {
            apiGet: _apiGet,
        };
	}
}());