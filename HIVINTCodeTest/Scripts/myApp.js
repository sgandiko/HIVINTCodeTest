var app = angular.module('myApp', []);

app.controller('myCtrl', ['$scope', '$http', myCtrl])

function myCtrl($scope, $http) {

    $scope.errormsg = '';

    $scope.subdomains = [];
   

    $scope.findAllIPAddresses = function () {

        $scope.errormsg = '';

        if ($scope.subdomains == undefined) {
            $scope.errormsg = 'please search the sub domains first';
            return;
        }

        if ($scope.subdomains.length == 0)
        {
            $scope.errormsg = 'no data is passed to the api call';
            return;
        }


        function findIPADDRSuccess(res) {
            
            $scope.subdomains = res.data;

            
            return;
        }

        function findIPADDRError(res) {

            $scope.errormsg = 'exception while fetching ip addresses';
            
            return;
        }


        $http.put('subdomain/findIPAddresses/', $scope.subdomains)
            .then(function (success) {
                return findIPADDRSuccess(success);
            }, function (error) {
                return findIPADDRError(error);
            });

    };


    $scope.getsubdomains = function () {

        $scope.errormsg = '';

        if ($scope.inputdomain == undefined) {
            $scope.errormsg = "Please enter the domain name";
            return;
        }


        var re = new RegExp(/^((?:(?:(?:\w[\.\-\+]?)*)\w)+)((?:(?:(?:\w[\.\-\+]?){0,62})\w)+)\.(\w{2,6})$/);
        if (!$scope.inputdomain.match(re)) {
            $scope.errormsg = "Please enter the correct domain name e.g yahoo.com";
            return;
        }

        
        function getdomainSuccess(res) {
            $scope.subdomains = res.data;
            if (res.data.length == 0)
                $scope.errormsg = "Zero records found"

            return;
        }

        function getdomainError(res) {
            $scope.errormsg = 'exception while fetching sub domains';
            
            return;
        }

        $http({
            method: 'GET',
            url: '/subdomain/enumerate/somedomain/?domain=' + $scope.inputdomain
        }).then(function (success) {
            return getdomainSuccess(success);
        }, function (error) {
            return getdomainError(error);
        });

    };
};