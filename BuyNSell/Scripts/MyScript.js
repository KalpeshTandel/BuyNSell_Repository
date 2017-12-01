﻿


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




    debugger;

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
            }
            
        });
        e.stopImmediatePropagation();
    });

    //$('#btnNextPage').on('click', function () {
    $("#btnNextPage").click(function () {
        debugger;

        $.ajax({
            type: "Post",
            url: "../Home/NextPage_Click",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: "", OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {

                $("#divSearchResult").html(result);
                
            }
        });
        // e.preventDefault();
        // e.stopPropagation();
         e.stopImmediatePropagation();
       
      

    });


    $("#btnPreviousPage").click(function () {
        debugger;

        $.ajax({
            type: "Post",
            url: "../Home/PreviousPage_Click",
            datatype: "json",
            data: { SearchText: "", OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {
                $("#divSearchResult").html(result);

             }

        });
        // e.preventDefault();
        // e.stopPropagation();
        e.stopImmediatePropagation();
    });



    $("#ddlPageSize").change(function ()  {
        debugger;

        PageSize = $("#ddlPageSize").val();

        $.ajax({
            type: "Post",
            url: "../Home/ddlPageSize_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {

                $("#divSearchResult").html(result);

            }
        });
        e.stopImmediatePropagation();
    });



    $("#ddlSortBy").change(function () {
        debugger;
        SortBy = $("#ddlSortBy").val();
        $.ajax({
            type: "Post",
            url: "../Home/ddlSortBy_Change",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {

                $("#divSearchResult").html(result);

            }
        });
        e.stopImmediatePropagation();
    });


    //$("#ddlProductCategory").change(function () {
    //    debugger;
    //    var ddlProductCategory = $("#ddlProductCategory").val();
    //    SearchText = SearchText + "And P.ProductCategoryId =" + ddlProductCategory;
    // })




});