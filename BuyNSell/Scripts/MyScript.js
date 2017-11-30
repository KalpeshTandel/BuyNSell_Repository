


$(document).ready(function () {
   // debugger;
    $("#divCategory").mouseover(function () {
        $("#divCategoryItems").show();
    });

    
    $("#divCategory").mouseleave(function () {
        $("#divCategoryItems").hide();
    });

    $("#divCategoryItems").mouseover(function () {
        $("#divCategoryItems").show();
    });


    $("#divCategoryItems").mouseleave(function () {
        $("#divCategoryItems").hide();
    });


    $("#divMyProducts").mouseover(function () {
        $("#divAddProducts").show();
    });


    $("#divMyProducts").mouseleave(function () {
        $("#divAddProducts").hide();
    });

    $("#divAddProducts").mouseover(function () {
        $("#divAddProducts").show();
    });


    $("#divAddProducts").mouseleave(function () {
        $("#divAddProducts").hide();
    });

    var SearchText = ""

    $("#btnSearch").click(function () {
        debugger;
        var txtProductName = $("#txtProductName").val();
        SearchText = "And ProductName Like '%" + txtProductName + "%'";

        $.ajax({
            type: "Post",
            url: "../Home/SearchProduct",
            datatype: "json",
            data: { "SearchText": SearchText, "Start": 1, "End": 5, "OrderBy": "ProductName" },
            success: function (result) {

                $("#divSearchResult").html(result);
            }
            
        });

    });

    //$('#btnNextPage').on('click', function () {
    $("#btnNextPage").click(function () {
        debugger;
        var txtProductName = $("#txtProductName").val();
        SearchText = "And ProductName Like '%" + txtProductName + "%'";
        var ddlPageSize = $("#ddlPageSize").val();

        $.ajax({
            type: "Post",
            url: "../Home/Paging",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { "SearchText": SearchText, "Start": 1, "End": 5, "OrderBy": "ProductName" , "PageSize":ddlPageSize},
            success: function (result) {

                $("#divSearchResult").html(result);
                
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
      

    });



    $("#ddlPageSize").change(function ()  {
        debugger;
        var txtProductName = $("#txtProductName").val();
        SearchText = "And ProductName Like '%" + txtProductName + "%'";
        var ddlPageSize = $("#ddlPageSize").val();
        $.ajax({
            type: "Post",
            url: "../Home/ddlPageSize_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { "SearchText": SearchText, "OrderBy": "ProductName", "PageSize": ddlPageSize },
            success: function (result) {

                $("#divSearchResult").html(result);

            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();

    });




});