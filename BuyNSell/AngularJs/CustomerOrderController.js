
var app = angular.module("IndexApp", [])

app.controller("IndexController", function ($scope, $http) {
    
    //Variables,Methods Declarations --Start

    $scope.OrderListData = {}; //Object datatype 
    $scope.OrderItemSelected = {};
    $scope.TotalRecords = 0; //Number datatype 
    $scope.ddlSortByList = [
        { Id: 1, Text: "Newest First" },
        { Id: 2, Text: "Product Name" },
        { Id: 3, Text: "Order Quantity" },
        { Id: 4, Text: "Payment Amount" }
    ]; //Array Object datatype

    $scope.ddlPageSizeList = [
    { Id: 5, Text: "5" },
    { Id: 2, Text: "10" },
    { Id: 15, Text: "15" }
    ]; //Array Object datatype

    $scope.ddlSortBySelected = 1;
    $scope.ddlPageSizeSelected = 5;

    $scope.CustomerOrder_PageLoad = function () {
        $http({
            method: "Get",
            url: "../CustomerOrder/CustomerOrder_PageLoad"
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
        }, function (error) {
            alert("Error")
        });
    };

    $scope.GetOrderList = function () {
        $http({
            method: "Get",
            url: "../CustomerOrder/GetOrderList"
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
        }, function (error) {
            alert("Error")
        });
    };

    $scope.GetOrderTemplate = function (item) {
        if (item.OrderId === $scope.OrderItemSelected.OrderId) {
            return 'Edit';
        }
        else return 'Display';
    };

    $scope.ChangeOrderStatus = function (item) {
        $scope.OrderItemSelected = angular.copy(item);

    };

    $scope.SaveOrderStatus = function (item) {
        debugger;
        //console.log("Saving contact");
        //$scope.model.contacts[idx] = angular.copy($scope.model.selected);
        //$scope.reset();
        $("#divPopupBackground").show();
        $("#divSaveStatusConfirm").show();
        $scope.OrderDetails = item;
    };


    $scope.CancelOrderStatus = function () {
        $scope.OrderItemSelected = {};
    };

    $scope.btnSaveStatusConfirmYes = function () {
        $http({
            method: "Post",
            url: "../CustomerOrder/ChangeOrderStatus",
            datatype: "json",
            data: { OrderInfo: $scope.OrderDetails }
            //data: JSON.stringify(item)
        }).then(function () {
            $scope.OrderItemSelected = {};
            debugger;
            $scope.CustomerOrder_PageLoad();
            $("#divPopupBackground").hide();
            $("#divSaveStatusConfirm").hide();
        });
    };


    $scope.btnSaveStatusConfirmNo = function () {
        $scope.OrderItemSelected = {};
        $scope.OrderDetails = {};
        $("#divPopupBackground").hide();
        $("#divSaveStatusConfirm").hide();
    };
    
    $scope.btnViewOrderDetails_Click = function (OrderId) {
        //debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Post",
            url: "../CustomerOrder/ViewCustomerOrderDetails",
            datatype: "json",
            data: { OrderId: OrderId }
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
    };

    $scope.ddlPageSize_Change = function () {
        $http({
            method: "Post",
            url: "../CustomerOrder/ddlPageSize_Change",
            datatype: "json",
            data: { ddlPageSizeSelected: $scope.ddlPageSizeSelected }
        }).then(function (result) {
            debugger;
            $scope.OrderListData = result.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
        }, function (error) {
            alert("Error");
        });
    };

    //Variables,Methods Declarations --End
 
    $scope.CustomerOrder_PageLoad();
    
});
