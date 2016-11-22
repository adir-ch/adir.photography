/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-route.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-mocks.js" />
/// <reference path="../../../adir.photography/app/common/services/appcommonservices.js" />
/// <reference path="../../../adir.photography/app/common/services/webapiservice.js" />
/// <reference path="../../../adir.photography/app/gallery/galleryapp.js" />
/// <reference path="../../../adir.photography/app/gallery/services/galleryservices.js" />

describe("WeApiServiceShould->", function() {

    beforeEach(function() {
        module("AppCommonServices");
    });

    var $exceptionHandler;
    var $httpBackend;
    var $rootScope;
    var $q;
    var deferred;

    beforeEach(module(function($exceptionHandlerProvider) {
        $exceptionHandlerProvider.mode('log');
    }));

    beforeEach(inject(function(_$q_, _$rootScope_, _$exceptionHandler_) {
        $rootScope = _$rootScope_;
        $q = _$q_;
        deferred = _$q_.defer();
        $exceptionHandler = _$exceptionHandler_;
    }));

    beforeEach(inject(function($injector) {

        $httpBackend = $injector.get("$httpBackend");

        $httpBackend.whenGET("/App.*/").respond(200, '');
        $httpBackend.when("GET", "/api/galleryapi")
            .respond([{
                HomeGalleryModel: {
                    AutoCycle: "true",
                    GalleryPhotos: ["br1.jpg", "fe1.jpg", "fe2.jpg"],
                    ImagesLocation: "/Content/Photos",
                    OpeningPhoto: "girona.jpg",
                    Timeout: "50"
                }
            }]);
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it("Call server api and resolve a promise", inject(function(WebApiService) {

        var data;
        var promise = deferred.promise;
        promise.then(function(response) { // subscribe to the promise
            data = response;
        });

        $httpBackend.expectGET("/api/galleryapi");

        // WebApiService should return a promise to whoever calls it. Calling then on
        // the service is similar to call then on returned response - i.e. testing the service
        WebApiService.apiGet("/api/galleryapi").then(function(response) {
            // WebApi will call HTTP service, which will invoke the mocked httpBackend, and the response
            // created before will get resolved. It is similar to call WebApiService from a different location
            // and subscribe on the promise, since WebApiService does not hold the data received internally.
            // The promised reolved here is the only way to get access to the data that was fetched by WebApiService,
            // otherwise the data should be kept in WebApiService (which is not needed for production).
            deferred.resolve(response);
        });

        // will actually invoke the HTTP backend.
        $httpBackend.flush();

        // Now the WebApiService promise will be reolved and the promise created here will get resolved
        // as well, and the var data will contain the data received from HTTP backend.
        expect(data).not.toBe(undefined);
        expect(data.length).toEqual(1);
        expect(data[0].HomeGalleryModel.OpeningPhoto).toEqual("girona.jpg");

    }));

    it("Return error message on fail", inject(function(WebApiService) {

        var data;

        // responde with server error
        $httpBackend.expectGET("/api/galleryapi").respond(500, '');

        // WebApiService should return a promise to whoever calls it. Calling then on
        // the service is similar to call then on returned response - i.e. testing the service
        WebApiService.apiGet("/api/galleryapi").then(
            function(response) {
                data = response;
            },

            function(response) { // handle errors - service should extruct the error message
                data = response;
            }
        ).catch(function(exception) {
            data = exception;
        });

        // will actually invoke the HTTP backend.
        $httpBackend.flush();

        // Now the WebApiService promise will be reolved and the promise created here will get resolved
        // as well, and the var data will contain the data received from HTTP backend.
        expect(data).not.toBe(undefined);
        //expect(WebApiService._onFailure).toHaveBeenCalled();
        expect(data).toEqual('');
    }));
});