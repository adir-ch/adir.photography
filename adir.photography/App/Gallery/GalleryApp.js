
angular.module('gallery', ['ngRoute'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.
                        when("/gallery", { templateUrl: "/App/Gallery/Views/GalleryView.html", controller: "GalleryViewModel", controllerAs: "galleryViewModel" }).
                        when("/gallery/:galleryTag", { templateUrl: "/App/Gallery/Views/GalleryView.html", controller: "GalleryViewModel", controllerAs: "galleryViewModel" }).
                        when("/albums", { templateUrl: "/App/Gallery/Views/AlbumsView.html", controller: "AlbumsViewModel", controllerAs: "albumsViewModel" }).
                        otherwise({ redirectTo: "/gallery/main" });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }).run(function () {
        console.log("Gallery App loading")
    });

