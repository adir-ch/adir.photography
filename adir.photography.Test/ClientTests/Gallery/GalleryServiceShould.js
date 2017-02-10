/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-route.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-mocks.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-animate.js" />
/// <reference path="../../../adir.photography/scripts/ngprogress.js" />
/// <reference path="../../../adir.photography/scripts/image-loader.js" />
/// <reference path="../../../adir.photography/app/common/services/appcommonservices.js" />
/// <reference path="../../../adir.photography/app/common/services/webapiservice.js" />
/// <reference path="../../../adir.photography/app/common/services/GlobalConfigurationService.js" />
/// <reference path="../../../adir.photography/app/gallery/galleryapp.js" />
/// <reference path="../../../adir.photography/app/gallery/services/galleryservices.js" />

describe("GalleryResourcesServiceShould->", function () {

    beforeEach(function () {
        module("gallery");
        module("AppCommonServices");
        module('ngRoute');
        module('ngAnimate');
        module('ngProgress');
        module('sap.imageloader');
    });

    var $httpBackend;
    var $rootScope;
    var $q;
    var deferred;

    beforeEach(module('ng', function ($exceptionHandlerProvider) {
        $exceptionHandlerProvider.mode('log');
    }));

    beforeEach(inject(function (_$q_, _$rootScope_, $injector, WebApiService) {

        $httpBackend = $injector.get("$httpBackend");
        $httpBackend.whenGET(/App.*/).respond(200, ''); // needed to intercept html templates requests.

        $rootScope = _$rootScope_;
        $q = _$q_;
        deferred = _$q_.defer();

        spyOn(WebApiService, 'apiGet').and.callFake(function () {
            return deferred.promise;
        });

    }));

    it("Load data", inject(function (GalleryResources) {

        expect(GalleryResources.galleryData()).toEqual([]);

        // do the async API call
        GalleryResources.getGalleryData("Main"); // will get the promise set in the beforEach

        // Mock server reply, with the following data
        deferred.resolve([{ // resolve the promise with mocked server data
            // will invoke WebApi service then, and Gallery service then
            // and will set the data in the Gallery service

            HomeGalleryModel: {
                AutoCycle: "true",
                GalleryPhotos: ["br1.jpg", "fe1.jpg", "fe2.jpg"],
                ImagesLocation: "/Content/Photos",
                OpeningPhoto: "girona.jpg",
                Timeout: "50"
            }
        }]);

        $rootScope.$apply(); // add the resolved promise to the queue, and eventually
        // will call to the function that the GalleryResources set
        // as a callback when the promise then was called.

        expect(GalleryResources.galleryData().length).toBeGreaterThan(0);
        expect(GalleryResources.galleryData().length).toEqual(1);

        var data = GalleryResources.galleryData();
        expect(data[0].HomeGalleryModel.OpeningPhoto).toEqual("girona.jpg");
        expect(GalleryResources.dataReadyStatus()).toEqual(true);
    }));

    it("Return error if gallry not found", inject(function (GalleryResources) {

        var state = "OK";
        var promise = GalleryResources.getGalleryData("Main");

        promise.catch(function (response) {
            state = response;
        });

        deferred.reject("Gallery not found");

        $rootScope.$apply();
        expect(state).toEqual("Gallery not found");
    }));
});