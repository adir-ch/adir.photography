(function () {
    "use strict";

    angular.module("member").factory("MemberResources", ['$q', 'WebApiService', MemberResources]).run(function(){
        console.log("starting member resources service");
    });

    function MemberResources($q, WebApiService) {
        var _errorMessage = "";
        var _memberData = {};
        var _memberDataReady = false;

        var _onSuccess = function(response) {
            angular.copy(response, _memberData);
            _memberDataReady = true;
        }

        var _onFailure = function (response) {
            console.log("Failed to find member");
            _errorMessage = response.data.ExceptionMessage;
        }

        var _isMemberLoggedIn = function(username) {
            // Go to server and check that memeber is authenticated
            return false;
        }

        // var _getMemberData = function($q, galleryName) {
        //     return WebApiService.apiGet('/api/memberapi')
        //         .then(function(response) {
        //             _onSuccess(response);
        //             console.log("WebApi call success");
        //             return _memberDataReady;
        //         },
        //         function(response) {
        //             console.log("WebApi call failed");
        //             _onFailure(response);
        //             return _errorMessage;
        //         }
        //     ).catch(function(response) {
        //         console.log("Exception while calling WebApi service");
        //     });

        // }

        var _getMemberData = function(username) {  // just until web api are ready
            var deferred = $q.defer();
            deferred.resolve({MemberModel: {
                                    username: "adirc",
                                    firstName: "adir",
                                    lastName: "c"
                                }});
            return deferred.promise;
        }

        return {
            isMemberLoggedIn: function() { return _isMemberLoggedIn; },
            errorMessage: function() { return _errorMessage; },
            getMemberData: _getMemberData
        };
    }
}());