var app = angular.module("IndexApp", []);
app.controller("IndexController", function ($scope, $http) {

    //Variables Declarations --Start

    $scope.MyOrderListFull = [];

    //Variables Declarations --End
    $scope.ddlPageSizeList = [
         { Id: 5, Text: "5" },
         { Id: 10, Text: "10" },
         { Id: 15, Text: "15" }
    ];

    $scope.ddlPageSizeSelected = $scope.ddlPageSizeList[0];




    //Methods Declarations --Start


    //Methods Declarations --End


    $scope.MyOrder_PageLoad = function () {
        $http({
            method: "Get",
            url: "/MyOrder/GetMyOrderList",
            datatype: "json"
        }).then(function (response) {
            debugger;
            $scope.MyOrderListFull = response.data;
        }, function () {
            alert("Error");
        });
    };

    $scope.MyOrder_PageLoad();

});
