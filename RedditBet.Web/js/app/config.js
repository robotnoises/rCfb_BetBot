'use strict'

angular.module('redditBet.config', [])
  .config([
    '$locationProvider', function ($locationProvider) {
      $locationProvider.html5Mode(true);
    }
  ]);