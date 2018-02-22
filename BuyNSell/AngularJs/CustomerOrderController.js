
var app = angular.module("IndexApp", [])

app.controller("IndexController", function ($scope, $http) {
    
    //Variables Declarations --Start

    $scope.OrderListData = {}; //Object datatype 
    $scope.OrderItemSelected = {};
    $scope.TotalRecords = 0; //Number datatype 
    $scope.ddlSortByList = [
        { Id: "OrderAddedDateDesc", Text: "Newest First" },
        { Id: "ProductName", Text: "Product Name" },
        { Id: "OrderQuantityDesc", Text: "Order Quantity" },
        { Id: "PaymentAmountDesc", Text: "Payment Amount" }
    ]; //Array Object datatype

    $scope.ddlPageSizeList = [
    { Id: 5, Text: "5" },
    { Id: 10, Text: "10" },
    { Id: 15, Text: "15" }
    ]; //Array Object datatype

    $scope.ddlSortBySelected = "OrderAddedDateDesc";
    $scope.ddlPageSizeSelected = 5;

    //Variables Declarations --End


    //Methods Declarations --Start

    $scope.CustomerOrder_PageLoad = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Get",
            url: "../CustomerOrder/CustomerOrder_PageLoad"
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            alert("Error")
        });
    };

    $scope.GetOrderList = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Get",
            url: "../CustomerOrder/GetOrderList"
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
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
            //data: { OrderInfo: $scope.OrderDetails }
            data: { OrderId: $scope.OrderDetails.OrderId, OrderStatusId: $scope.OrderDetails.OrderStatusId, ProductId: $scope.OrderDetails.ProductId }
            //data: JSON.stringify(item)
        }).then(function () {
            $scope.OrderItemSelected = {};
            $scope.OrderDetails = {};
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
        }).then(function (response) {
            debugger;
            $("#divViewOrderDetails").html(response.data);
            $("#divPopupBackground").show();
            $("#divViewOrderDetails").show();
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            debugger;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
            alert("Error");
        });
    };

    $scope.ddlPageSize_Change = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Post",
            url: "../CustomerOrder/ddlPageSize_Change",
            datatype: "json",
            data: { ddlPageSizeSelected: $scope.ddlPageSizeSelected }
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            alert("Error");
        });
    };

    $scope.ddlSortBy_Change = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Post",
            url: "/CustomerOrder/ddlSortBy_Change",
            datatype: "json",
            data: { ddlSortBySelected: $scope.ddlSortBySelected }
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            alert("Error");
        });
    };

    $scope.btnNextPageOrder_Click = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Post",
            url: "/CustomerOrder/btnNextPageOrder_Click",
            datatype: "json",
            data: { ddlPageSizeSelected : $scope.ddlPageSizeSelected }
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
            alert("Error");
        });
    };

    $scope.btnPreviousPageOrder_Click = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Get",//Get Method don't need parameter and post Method must want parameter
            url: "/CustomerOrder/btnPreviousPageOrder_Click",
            datatype: "json",
        }).then(function (response) {
            debugger;
            $scope.OrderListData = response.data.OrderList;
            $scope.TotalRecords = response.data.TotalRecords;
            $scope.CurrentPageNumber = response.data.CurrentPageNumber;
            $scope.LastPageNumber = response.data.LastPageNumber;
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function (error) {
            alert("Error");
        });
        //-----.success and .error were removed in Angularjs 1.6. 
        //call.success(function (data, status) {
        //    debugger;
        //    $scope.OrderListData = data.OrderList;
        //    $scope.TotalRecords =data.TotalRecords;
        //    $scope.CurrentPageNumber = data.CurrentPageNumber;
        //    $scope.LastPageNumber = data.LastPageNumber;
        //});

        //call.error(function (data, status) {
        //    alert("Error");
        //});
    };


    //Methods Declarations --End





 
    //Method Call --Start

    $scope.CustomerOrder_PageLoad();
    
    //Method Call --End
});
