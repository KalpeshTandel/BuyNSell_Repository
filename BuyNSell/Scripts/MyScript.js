﻿


$(document).ready(function () {
   // debugger;
    $("#divCategory").mouseover(function () {
        $("#divCategoryItems").show();
    });

    
    $("#divCategory").mouseout(function () {
        $("#divCategoryItems").hide();
    });

    $("#divCategoryItems").mouseover(function () {
        $("#divCategoryItems").show();
    });


    $("#divCategoryItems").mouseout(function () {
        $("#divCategoryItems").hide();
    });


    $("#divMyProducts").mouseover(function () {
        $("#divAddProducts").show();
    });


    $("#divMyProducts").mouseout(function () {
        $("#divAddProducts").hide();
    });

    $("#divAddProducts").mouseover(function () {
        $("#divAddProducts").show();
    });


    $("#divAddProducts").mouseout(function () {
        $("#divAddProducts").hide();
    });

    $("#divMyProfile").mouseover(function () {
        
        $("#divMyProfileItems").show();
    });

    $("#divMyProfile").mouseout(function () {
        
        $("#divMyProfileItems").hide();
    });

    $("#divMyProfileItems").mouseover(function () {
       
        $("#divMyProfileItems").show();
    });

    $("#divMyProfileItems").mouseout(function () {
       
        $("#divMyProfileItems").hide();
    });
//    debugger;

        var SearchText = "";

        //var txtProductName = $("#txtProductName").val();

        //if (txtProductName != "")
        //{
        //    SearchText = " And ProductName Like '%" + txtProductName + "%'";
        //}

        //var ddlProductCategory = $("#ddlProductCategory").val();

        //if (ddlProductCategory != "")
        //{
        //    SearchText = SearchText + " And P.ProductCategoryId =" + ddlProductCategory;
        //}



    var PageSize = $("#ddlPageSize").val();

    var SortBy = $("#ddlSortBy").val();



    //$("#txtProductName").keyup(function () {
    //    debugger;
    //    var txtProductName = $("#txtProductName").val();
    //   // if (txtProductName != "") {
    //        SearchText = "And ProductName Like '%" + txtProductName + "%'";
    //    //}
    //});


    $("#btnSearch").click(function () {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();

        var SearchText = "";

        var txtProductName = $("#txtProductName").val();
        if (txtProductName != "")
        {
            SearchText = " And ProductName Like '%" + txtProductName + "%'";
        }

        var ddlProductCategory = $("#ddlProductCategory").val();
        if (ddlProductCategory != "")
        {
            SearchText = SearchText + " And P.ProductCategoryId =" + ddlProductCategory;
        }

        var txtFromDate = $("#txtFromDate").val();
        var txtToDate = $("#txtToDate").val();
        if (txtFromDate != "" && txtToDate != "")
        {
            SearchText = SearchText + " And Cast(AddedDate as Date) between '" + txtFromDate + "' and '" + txtToDate + "'";
        }

        $.ajax({
            type: "Post",
            url: "../Home/SearchProduct",
            datatype: "json",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();
            }          
        });
        e.stopImmediatePropagation();
    });



    //$('#btnNextPage').on('click', function () {
    $("#btnNextPage").click(function () {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();

        $.ajax({
            type: "Post",
            url: "../Home/NextPage_Click",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: "", OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();                
            }
        });
        // e.preventDefault();
        // e.stopPropagation();
         e.stopImmediatePropagation();
    });


    $("#btnPreviousPage").click(function () {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();

        $.ajax({
            type: "Post",
            url: "../Home/PreviousPage_Click",
            datatype: "json",
            data: { SearchText: "", OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();
             }
        });
        // e.preventDefault();
        // e.stopPropagation();
        e.stopImmediatePropagation();
    });



    $("#ddlPageSize").change(function ()  {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        PageSize = $("#ddlPageSize").val();

        $.ajax({
            type: "Post",
            url: "../Home/ddlPageSize_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();
            }
        });
        e.stopImmediatePropagation();
    });



    $("#ddlSortBy").change(function () {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        SortBy = $("#ddlSortBy").val();

        $.ajax({
            type: "Post",
            url: "../Home/ddlSortBy_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();
            }
        });
        e.stopImmediatePropagation();
    });


    //$("#ddlProductCategory").change(function () {
    //    debugger;
    //    var ddlProductCategory = $("#ddlProductCategory").val();
    //    SearchText = SearchText + "And P.ProductCategoryId =" + ddlProductCategory;
    // })


    //$(".SubmitButton").click(function () {
    //    debugger;
    //    $("#divAddOrderBackground").show();
    //    $("#divAddOrder").show();
    //    $.ajax({
    //        type: "Post",
    //        url: "../Order/AddOrder",
    //        datatype: "json",
    //        data: { ProductId: },
    //        success: function (result) {
    //            $("#divAddOrder").html(result);
    //            //$('#divAddOrder').dialog('open');
    //        }

    //    });
    //    e.stopImmediatePropagation();
    //});

    //$("#divAddOrder").dialog({
    //    autoOpen: false,
    //    modal: true,
    //    title: "View Details"
    //});

    //function btnAddOrder_Click(ProductId)
    //{
    //    debugger;
    //    $("#divAddOrderBackground").show();
    //    $("#divAddOrder").show();
    //    $.ajax({
    //        type: "Post",
    //        url: "../Order/AddOrder",
    //        datatype: "json",
    //        data: { ProductId:ProductId},
    //        success: function (result) {
    //            $("#divAddOrder").html(result);
    //            //$('#divAddOrder').dialog('open');
    //        }

    //    });
    //    e.stopImmediatePropagation();
    //}

    $("#OrderQuantity").change(function () {
        debugger;
        $("#divLoadingBackground").show();
        $("#divLoadingImage").show();
        var OrderQuantity = $("#OrderQuantity").val();

        $.ajax({
            type: "Post",
            url: "../Order/OrderQuantity_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { OrderQuantity: OrderQuantity},
            success: function (result) {
                $("#lblPaymentAmount").html(result);
                $("#divLoadingBackground").hide();
                $("#divLoadingImage").hide();
            }
        });
        e.stopImmediatePropagation();
    });



    $("#btnOrderNow").click(function () {
        debugger;
        var DeliveryAddress = $("#DeliveryAddress").val();
        var ContactNum = $("#ContactNum").val();
        $("#lblErrorDelivery").hide();
        $("#lblErrorContact").hide();

        if (DeliveryAddress == "" && ContactNum == "") {
            $("#lblErrorDelivery").show();
            $("#lblErrorContact").show();
            return false;
        }

        else if (DeliveryAddress == "") {
            $("#lblErrorDelivery").show();
            return false;
        }
        else if (ContactNum == "") {
            $("#lblErrorContact").show();
            return false;
        }
        else
        {
            $("#divAddOrder").empty();
            $("#divPopupBackground").empty();
            $("#divAddOrder").hide();
            $("#divPopupBackground").hide();
            $("#divSuccess").show();
            $("#lblSuccess").val("Order placed Sccessfully!Thank You")
        }
    });


    $("#btnOrderNowCancel").click(function () {
        debugger;
        $("#divAddOrder").empty();
        $("#divPopupBackground").empty();
        $("#divAddOrder").hide();
        $("#divPopupBackground").hide();

    });

    function Callback() {
        debugger;
        $("#divAddOrder").empty();
        $("#divPopupBackground").empty();
        $("#divAddOrder").hide();
        $("#divPopupBackground").hide();

    }

    $("#btnLogout").click(function () {
        $("#divPopupBackground").show();
        $("#divLogoutConfirm").show();
    });


    $("#btnLogoutConfirmNo").click(function () {
        $("#divPopupBackground").hide();
        $("#divLogoutConfirm").hide();
    });


    //$("#btnLogoutConfirmYes").click(function () {
    //    debugger;
    //    $.ajax({
    //        type: "Post",
    //        //url:'@Url.Action("Home","Home")'
    //        url: "../Login/Logout"
    //    });

    //});

    //function btnViewProductDetails_Click(ProductId) {
    //    debugger;
    //    alert(ProductId);

    //}
   

    $("#btnCloseViewProductDetails").click(function () {
        $("#divViewProductDetails").empty();
        $("#divPopupBackground").empty();
        $("#divViewProductDetails").hide();
        $("#divPopupBackground").hide();

    });


//    $("#btnnext").click(function () {
//        $.ajax({
//            type: "Post",
//            url: "../Product/btnnext_Click",
//            datatype: "json",
//            //data: { ProductId: ProductId },
//            success: function (result) {

////                $("#Pic").html(result);

//                $("#Pic").attr("src", result);
//                //$("#divPopupBackground").show();
//                //$("#divViewProductDetails").show();
//            }
//        });

//    });



});