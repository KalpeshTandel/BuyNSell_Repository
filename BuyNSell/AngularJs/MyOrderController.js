var app = angular.module("IndexApp", []);
app.controller("IndexController", function ($scope, $http) {

    //Variables Declarations --Start

    $scope.MyOrderListFull = [];

    $scope.MyOrderListSpecific = [];

    $scope.ddlPageSizeList = [
     { Id: 5, Text: "5" },
     { Id: 10, Text: "10" },
     { Id: 15, Text: "15" }
    ];

    $scope.ddlPageSizeSelected = $scope.ddlPageSizeList[0];

    $scope.TotalRecords = 0;
    $scope.StartRecord = 0;
    $scope.EndRecord = 0;
    $scope.LastPageNumber = 0;
    $scope.CurrentPageNumber = 1;

    $scope.MyOrderSelectedItem = {};
    $scope.MyOrderDetails = {};

    //Variables Declarations --End


    //Methods Declarations --Start


    $scope.GetSpecificData = function () {
        debugger;
        $scope.StartRecord = ($scope.CurrentPageNumber * $scope.ddlPageSizeSelected.Id) - $scope.ddlPageSizeSelected.Id;
        $scope.EndRecord = ($scope.CurrentPageNumber * $scope.ddlPageSizeSelected.Id) - 1;
        $scope.MyOrderListSpecific = [];

        for (var i = $scope.StartRecord; i <= $scope.EndRecord && i < $scope.TotalRecords; i++) {
            $scope.MyOrderListSpecific.push($scope.MyOrderListFull[i]);
        }
    };

    $scope.GetOrderTemplate = function (item) {
        debugger;
        if ($scope.MyOrderSelectedItem == item) {
            return 'Edit';
        }
        else {
            return 'Display';
        }
    };

    $scope.ChangeOrderStatus = function (item) {
        $scope.MyOrderSelectedItem = item;
    };

    $scope.SaveMyOrderStatus = function (item) {
        debugger;
        $("#divPopupBackground").show();
        $("#divSaveStatusConfirm").show();
        $scope.MyOrderDetails = item;
    };

    $scope.CancelMyOrderStatus = function () {
        $scope.MyOrderSelectedItem = {};
    };

    $scope.btnSaveStatusConfirmYes = function (item) {
        $http({
            method: "Post",
            url: "../MyOrder/ChangeOrderStatus",
            datatype: "json",
            data: { OrderInfo: $scope.MyOrderDetails }
        }).then(function () {
            $scope.MyOrderDetails = {};
            $scope.MyOrderSelectedItem = {};
            $("#divPopupBackground").hide();
            $("#divSaveStatusConfirm").hide();
            $scope.MyOrder_PageLoad();
        }, function () {
            alert("Error");
        });
    };

    $scope.btnSaveStatusConfirmNo = function () {
        $scope.MyOrderSelectedItem = {};
        $("#divPopupBackground").hide();
        $("#divSaveStatusConfirm").hide();
    };



    $scope.MyOrder_PageLoad = function () {
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        $http({
            method: "Get",
            url: "/MyOrder/GetMyOrderList",
            datatype: "json"
        }).then(function (response) {
            debugger;
            $scope.MyOrderListFull = response.data;
            $scope.TotalRecords = $scope.MyOrderListFull.length;
            //$scope.CurrentPageNumber = 1;
            $scope.LastPageNumber = Math.ceil($scope.TotalRecords / $scope.ddlPageSizeSelected.Id);
            $scope.GetSpecificData();
            $("#divLoadingImage").hide();
            $("#divLoadingBackground").hide();
        }, function () {
            alert("Error");
        });
    };


    $scope.ddlPageSize_Change = function () {
        debugger;
        $scope.CurrentPageNumber = 1;
        $scope.LastPageNumber = Math.ceil($scope.TotalRecords / $scope.ddlPageSizeSelected.Id);
        $scope.GetSpecificData();
    };

    $scope.btnNextPageMyOrder_Click = function () {
        $scope.CurrentPageNumber = $scope.CurrentPageNumber + 1;
        $scope.GetSpecificData();
    };

    $scope.btnPreviousPageMyOrder_Click = function () {
        $scope.CurrentPageNumber = $scope.CurrentPageNumber - 1;
        $scope.GetSpecificData();
    };

    //Methods Declarations --End


    $scope.MyOrder_PageLoad();


});
