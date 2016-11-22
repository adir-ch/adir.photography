/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-route.js" />
/// <reference path="../../../adir.photography/scripts/angular/angular-mocks.js" />
/// <reference path="../../../adir.photography/app/common/services/appcommonservices.js" />
/// <reference path="../../../adir.photography/app/common/services/webapiservice.js" />
/// <reference path="../../../adir.photography/app/gallery/galleryapp.js" />
/// <reference path="../../../adir.photography/app/gallery/viewmodels/galleryviewmodel.js" />
/// <reference path="../../../adir.photography/app/gallery/services/galleryservices.js" />

describe("GalleryViewModelShould->", function () {

	var $controller;
    var $httpBackend;
    var $rootScope;
    var $scope;
    var $httpBackend;
    var deferred;
    var GalleryResources;

	// create a mock for the GalleryResources service instead
	// of creating a Spy for each function.
	var GalleryResourcesMock = {};

    // load the modules and build fake services
    beforeEach(function () {
    	module("AppCommonServices");

    	// load the module and set the provider with a service mock
    	// The provider holds the dependencies dictionary
        module("gallery", function($provide) {
        	// register the service mock in the gallery module
        	$provide.value('GalleryResources', GalleryResourcesMock);
        });

        // create GalleryResources service mock
	    inject(function($q) {
	        GalleryResourcesMock.galleryData = function() {
	            return [{ HomeGalleryModel: {
	                        AutoCycle: "true",
	                        GalleryPhotos: ["br1.jpg", "fe1.jpg", "fe2.jpg"],
	                        ImagesLocation: "/Content/Photos",
	                        OpeningPhoto: "girona.jpg",
	                        Timeout: "50"
	                    }
	            }];
	        },

	        GalleryResourcesMock.getGalleryData = function(galleryName) {
	        	//console.log("Fake service called (getGalleryData) !!!!!!!!!!!!!!!!!!!!!!!")
	            var deffered = $q.defer();
	            deffered.resolve(true);
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


    it("Load gallery data", inject(function(_GalleryResources_) {

    	// build the controller with the mocked service
        GalleryResources = _GalleryResources_;
        $controller('GalleryViewModel', { $scope: $scope, GalleryResources: GalleryResources});

        $rootScope.$apply();
        expect($scope.galleryDataReady).toEqual(true);
        var data = $scope.galleryData();
        expect(data[0].HomeGalleryModel.OpeningPhoto).toEqual("girona.jpg");
    }));

    it("Set error message on fail", inject(function($q, _GalleryResources_) {

    	// build the controller with the mocked service
        GalleryResources = _GalleryResources_;

    	spyOn(GalleryResources, 'getGalleryData').and.callFake(function () {
            var deferred = $q.defer();
            deferred.reject("Server Error");
            return deferred.promise;
        });


        $controller('GalleryViewModel', { $scope: $scope, GalleryResources: GalleryResources});

        $rootScope.$apply();
        expect($scope.galleryDataReady).toEqual(false);
        expect($scope.serverErrorMessage).toEqual("Server Error");
    }));
});