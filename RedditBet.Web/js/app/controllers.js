'use strict';

angular.module('redditBet.controllers', [])
  .controller('tempController', ['$scope', function ($scope) {
    $scope.test = "it worked.";
  }])
  //.controller('titleController', ['$scope', function ($scope) {
  //  $scope.title = "Page title";
  //}])
  .controller('errorController', ['$scope', function ($scope) {
    $scope.errorMsg = "404";
  }]);