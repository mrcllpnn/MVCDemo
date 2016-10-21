var app = angular.module('searchApp', []);
//
//SearchController: this code calls methods in the service and 
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
