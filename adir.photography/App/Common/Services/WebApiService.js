(function() {
    "use strict";

    var _result = "";

    angular.module("AppCommonServices").constant("WebApiServerSettings", {
        ServerPath: "http://localhost:61001"
    });

    angular.module("AppCommonServices").factory("WebApiService", ['$http', '$q', '$exceptionHandler', 'WebApiServerSettings', WebApiService]);

    function WebApiService($http, $q, $exceptionHandler) {

        var _onGetSuccess = function(response) {
            //console.log("Got new data from server");
        }

        var _onPostSuccess = function(response) {
            //console.log("Posted new data to server");
        }

        var _onFailure = function(response) {
            //console.log("Error while requesting data from server: ", response);
            if (response.data.ExceptionMessage) {
                //console.log("stack trace: ", response.data.StackTrace);
                _result = "Error on server request"; //  response.data.ExceptionMessage;
            } else if (response.data.Message) {
                _result = response.data.Message;
            } else {
                _result = response.statusText;
            }
        }

        var _apiGet = function(uri) {

            // TODO: Implement HTTP GET Cache
            return $http.get(uri, { cache: true })
                .then(function(response) {
                    _onGetSuccess(response);
                    return response.data; // resolve the promise of whever called me with the data
                }, function(response) {
                    _onFailure(response);
                    throw _result;
                });
        }

        var _apiPost = function(uri, data) {
            return $http.post(uri, data, { header: { 'Content-Type': 'application/json' } })
                .then(function(response) {
                    _onPostSuccess(response);
                    return response.data; // resolve the promise of whever called me with the data
                }, function(response) {
                    _onFailure(response);
                    throw _result;
                });
        }

        return {
            apiGet: _apiGet,
            apiPost: _apiPost
        };
    }
}());