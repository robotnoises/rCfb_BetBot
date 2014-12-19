'use strict';

angular.module('redditBet.routes', ['ngRoute'])
  .config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/bet', {
      controller: 'tempController',
      templateUrl: '/views/bet.html'
    })
    .otherwise({
      controller: 'errorController',
      templateUrl: '/views/404.html'
    });
  }]);