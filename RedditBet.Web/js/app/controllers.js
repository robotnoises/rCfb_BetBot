'use strict';

angular.module('redditBet.controllers', [])
  .controller('tempController', ['$scope', function ($scope) {
    $scope.test = "it worked.";
  }])
  .controller('errorController', ['$scope', function ($scope) {
    $scope.errorMsg = "404";
  }]);