var app = angular.module("IndexApp", [])

app.controller("IndexController", function ($scope, $http)
{
    $http({
        method: "Get",
        url: "../CustomerOrder/GetOrderList"
    }).then(function (response) {
        $scope.OrderList = response.data
    }, function (error) {
        alert("Error")
    });
})