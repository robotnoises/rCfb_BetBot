'use strict'

angular.module('redditBet.directives', [])
  // Credits to: http://stackoverflow.com/questions/17772260/textarea-auto-height#answer-24090733
  .directive('ngElastic', [
    '$timeout',
    function ($timeout) {
      return {
        restrict: 'A',
        link: function ($scope, element) {

          var resize = function () {
            return element[0].style.height = "" + element[0].scrollHeight + "px";
          };

          element.on("blur keyup change", resize);

          $timeout(resize, 0);
          }
        };
      }
  ]);