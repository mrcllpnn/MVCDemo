var app = angular.module('searchApp', []);

//
//HomeController: This section opens a new window to my github account.
//  This is linked to the 'view source' button on the home page.    
//
app.controller("HomeController", function($scope,$window) {

    $scope.openLink = function() {
        $window.open('https://github.com/mrcllpnn/MVCDemo', '_blank');
    };

});

//
//SearchController: This section calls methods in the service and 
//     updates the scope with the results. 
//
app.controller('SearchController', function SearchController($scope, SearchService) {

    $scope.searchArtist = function () {

        $scope.loading = true;

        SearchService.Search($scope.artist)
            .success(function (Artists) {
                $scope.Artists = Artists;
                $scope.loading = false;
            })
        .error(function (error) {
            $scope.loading = false;
            alert('ERROR: I could not post to the remote server!');
        })
    };

});

//
// SearchService: Makes a http post to the MVC
// controller and returns artist data. 
//
app.service('SearchService', function ($http) {

    this.Search = function (artist) {
        return $http.post('/Home/Search', { artistName: artist });
    };

});