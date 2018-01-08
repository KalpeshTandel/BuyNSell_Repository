

var app = angular.module("IndexApp", [])

app.controller("IndexController", function ($scope, $http) {

    $http({
        method: "Get",
        url: "../CustomerOrder/GetOrderList"
    }).then(function (response) {
        $scope.OrderList = response.data;

    }, function (error) {
        alert("Error")
    });


    $scope.GetOrderList = function(){
        $http({
            method: "Get",
            url: "../CustomerOrder/GetOrderList"
        }).then(function (response) {
            $scope.OrderList = response.data;

        }, function (error) {
            alert("Error")
        });
    }


    $scope.selected = {};

    $scope.GetTemplate = function (item) {
        debugger;
        if (item.OrderId === $scope.selected.OrderId) {
            //$(this).css("background-color", "#71C6ED");
            return 'Edit';
           
        }
        else return 'Display';
    };

    $scope.ChangeStatus = function (item) {
        $scope.selected = angular.copy(item);

    }


    $scope.SaveStatus = function (item) {
        debugger;
        //console.log("Saving contact");
        //$scope.model.contacts[idx] = angular.copy($scope.model.selected);
        //$scope.reset();
        $("#divPopupBackground").show();
        $("#divSaveStatusConfirm").show();
        $scope.OrderDetails = item;
    }


    $scope.CancelStatus = function () {
        $scope.selected = {};
    }

    $scope.btnSaveStatusConfirmYes = function () {
        $http({
            method: "Post",
            url: "../CustomerOrder/ChangeOrderStatus",
            datatype: "json",
            data: { OrderInfo: $scope.OrderDetails }
            //data: JSON.stringify(item)
        }).then(function () {
            $scope.selected = {};
            //$route.reload();
            $scope.GetOrderList();
            $("#divPopupBackground").hide();
            $("#divSaveStatusConfirm").hide();
        });
    }


    $scope.btnSaveStatusConfirmNo = function () {
        $scope.selected = {};
        $scope.OrderDetails = {};
        $("#divPopupBackground").hide();
        $("#divSaveStatusConfirm").hide();
    }




});
