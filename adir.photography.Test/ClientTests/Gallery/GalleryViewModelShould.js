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
/// <reference path="../../../adir.photography/app/gallery/viewmodels/galleryviewmodel.js" />

describe("GalleryViewModelShould->", function () {

    var $controller;
    var $httpBackend;
    var $rootScope;
    var $scope;
    var $httpBackend;
    var deferred;
    var GalleryResources;
    var ImageLoader; 

    var mockedGalleryData = {
        "Name": "Main",
        "OpeningPhoto": "girona.jpg",
        "GalleryPhotos": [
            {
                "FileName": "girona.jpg",
                "Title": "girona",
                "Caption": "girona",
                "Metadata": {
                    "Width": 1920,
                    "Height": 795
                },
                "Tags": [
                    "",
                    "ap-main",
                    "ap-travel",
                    "ap-website"
                ]
            },
            {
                "FileName": "gray.jpg",
                "Title": "gray",
                "Caption": "A.C(C)",
                "Metadata": {
                    "Width": 1920,
                    "Height": 717
                },
                "Tags": [
                    "",
                    "ap-landscapes",
                    "ap-main",
                    "ap-website"
                ]
            },
            {
                "FileName": "greek-serenity.jpg",
                "Title": "greek-serenity",
                "Caption": "A.C(C)",
                "Metadata": {
                    "Width": 1626,
                    "Height": 1080
                },
                "Tags": [
                    "",
                    "ap-landscapes",
                    "ap-main",
                    "ap-website"
                ]
            }
        ],
        "Timeout": 50,
        "AutoCycle": true,
        "ImagesLocation": "/Content/Photos"
    };

    // create a mock for image loader 
    var ImageLoaderMock = {}; 

    // create a mock for the GalleryResources service instead
    // of creating a Spy for each function.
    var GalleryResourcesMock = {};

    // load the modules and build fake services
    beforeEach(function () {
        module("AppCommonServices");
        module("AppCommonServices");
        module('ngRoute');
        module('ngAnimate');
        module('ngProgress');
        module('sap.imageloader');

        // load the module and set the provider with a service mock
        // The provider holds the dependencies dictionary
        module("gallery", function ($provide) {
            // register the service mock in the gallery module
            $provide.value('GalleryResources', GalleryResourcesMock);
            $provide.value('ImageLoader', ImageLoaderMock);
        });

        // create GalleryResources service mock
        inject(function ($q) {
            GalleryResourcesMock.galleryData = function () {
                    //console.log("Fake service called with galleryData()!")
                    return mockedGalleryData;
                },

                GalleryResourcesMock.getGalleryData = function (galleryName) {
                    //console.log("Fake service called with getGalleryData()!");
                    var deffered = $q.defer();
                    deffered.resolve(true);
                    return deffered.promise;
                }
        });

        // create ImageLoader service mock 
        inject(function($q) {
            ImageLoaderMock.loadImage = function(image) {
                //console.log("Fake service called with loadImage()!");
                var deffered = $q.defer();
                deffered.resolve(image);
                return deffered.promise;
            }
        });
    });

    beforeEach(inject(function (_$rootScope_, _$controller_, _$httpBackend_) {
        $rootScope = _$rootScope_;
        $controller = _$controller_;
        $scope = $rootScope.$new();
        $httpBackend = _$httpBackend_;
        $httpBackend.whenGET(/App.*/).respond(200, '');
    }));


    it("Load gallery data", inject(function (
        _$routeParams_,
        _$window_,
        _$timeout_,
        _ngProgressFactory_,
        _ImageLoader_,
        _GalleryResources_) {

        $routeParams = _$routeParams_;
        $window = _$window_;
        $timeout = _$timeout_;
        ngProgressFactory = _ngProgressFactory_;
        ImageLoader = _ImageLoader_;
        GalleryResources = _GalleryResources_;

        // build the controller with the mocked service
        GalleryResources = _GalleryResources_;
        var ctrl = $controller('GalleryViewModel', {
            $scope: $scope,
            $routeParams: $routeParams,
            $window: $window,
            $timeout: $timeout,
            ngProgressFactory: ngProgressFactory,
            ImageLoader: ImageLoader,
            GalleryResources: GalleryResources
        });

        $rootScope.$apply();
        var data = ctrl.galleryData();

        expect(ctrl.galleryDataReady).toEqual(true);
        expect(data.OpeningPhoto).toEqual("girona.jpg");
        expect(data.GalleryPhotos.length).toEqual(3);
        expect(ctrl.progressbar.status()).toBeGreaterThan(99.5);
    }));

    it("Set error message on fail", inject(function ($q, _GalleryResources_) {

        // build the controller with the mocked service
        GalleryResources = _GalleryResources_;

        spyOn(GalleryResources, 'getGalleryData').and.callFake(function () {
            var deferred = $q.defer();
            deferred.reject("Server Error");
            return deferred.promise;
        });


        var ctrl = $controller('GalleryViewModel', {
            $scope: $scope,
            GalleryResources: GalleryResources
        });

        $rootScope.$apply();
        expect(ctrl.galleryDataReady).toEqual(false);
        expect(ctrl.serverError).toEqual(true);
        expect(ctrl.serverErrorMessage).toEqual("Server Error");
    }));
});