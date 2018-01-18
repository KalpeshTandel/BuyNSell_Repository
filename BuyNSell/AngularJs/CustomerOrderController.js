

var app = angular.module("IndexApp", [])

app.controller("IndexController", function ($scope, $http) {
    
    $scope.CustomerOrder_PageLoad = function () {
        $http({
            method: "Get",
            url: "../CustomerOrder/CustomerOrder_PageLoad"
        }).then(function (response) {
            $scope.OrderList = response.data.OrderList;
        }, function (error) {
            alert("Error")
        });
    }
     
    $scope.CustomerOrder_PageLoad();
    
    $scope.GetOrderList = function () {
        
        $http({
            method: "Get",
            url: "../CustomerOrder/GetOrderList"
        }).then(function (response) {
            debugger;
            $scope.OrderList = response.data.Data.OrderList;

        }, function (error) {
            alert("Error")
        });
    }

    $scope.ddlSortByList = [
        { Id: 1, Text: "Newest First" },
        { Id: 2, Text: "Product Name" },
        { Id: 3, Text: "Order Quantity" },
        { Id: 4, Text: "Payment Amount" }
    ];

    $scope.ddlSortBySelected = 1;

    $scope.ddlPageSizeList = [
        { Id: 1, Text: "5" },
        { Id: 2, Text: "10" },
        { Id: 3, Text: "15" }
    ];

    $scope.ddlPageSizeSelected = 1;

    $scope.selected = {};

    $scope.GetTemplate = function (item) {
        //debugger;
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

    $scope.btnViewOrderDetails_Click = function (OrderId) {
        //debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Post",
            url: "../CustomerOrder/ViewCustomerOrderDetails",
            datatype: "json",
            data:{OrderId : OrderId}
        }).then(function (result) {
            debugger;
            $("#divViewOrderDetails").html(result.data);
            $("#divPopupBackground").show();
            $("#divViewOrderDetails").show();
            $("#divLoadingBackground").hide();
            $("#divLoadingImage").hide();
        }, function (error) {
            debugger;
            $("#divLoadingBackground").hide();
            $("#divLoadingImage").hide();
            alert("Error");
        });
    }


});
