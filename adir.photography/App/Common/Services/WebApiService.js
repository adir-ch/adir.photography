
(function () {
    "use strict";

    angular.module("AppCommonServices").constant("WebApiServerSettings", {
		ServerPath: "http://localhost:61001"
    });

	angular.module("AppCommonServices").factory("WebApiService", ['$http', '$q', 'WebApiServerSettings', WebApiService]);

	function WebApiService($http, $q) {

		var _deffered;

		var _onFailure = function(reponse, success, always) {
			success(response);

			if (always != null) {
				always(response);
			}

            _deffered.resolve(response);
		}

		var _onFailure = function(response, failure, always) {
			if (failure != null) {
				failure(response);
			}

			if (always != null) {
				always(response);
			}

			_deffered.reject(response.data.ExceptionMessage);
		}

		var _apiGet = function(uri, success, failure, always) {

            _deffered = $q.defer();

			$http.get(uri)
                .then(function(response) {
                    _onSuccess(response, success, always);
                }, function(response) {
					_onFailure(response, failure, always);
                })
                .catch(function(response) {
                    _onFailure(response, failure, always);
                });

            return _deffered.promise;
        }

        return {
            apiGet: _apiGet
        };

	}


}());