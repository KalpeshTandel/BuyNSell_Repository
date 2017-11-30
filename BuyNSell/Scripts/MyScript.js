


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





    var SearchText = "";

    var PageSize = $("#ddlPageSize").val();

    var SortBy = $("#ddlSortBy").val();



    $("#txtProductName").keyup(function () {
        debugger;
        var txtProductName = $("#txtProductName").val();
        if (txtProductName != "") {
            SearchText = "And ProductName Like '%" + txtProductName + "%'";
        }
    });


    $("#btnSearch").click(function () {
        debugger;

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
        //$("#btnPreviousPage").css("display", "none");
        $.ajax({
            type: "Post",
            url: "../Home/NextPage_Click",
            datatype: "json",
            //contentType: "application/json; charset=utf-8",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
            success: function (result) {

                $("#divSearchResult").html(result);
                
            }
        });
         e.preventDefault();
        // e.stopPropagation();
        // e.stopImmediatePropagation();
       
      

    });


    $("#btnPreviousPage").click(function () {
        debugger;
        $.ajax({
            type: "Post",
            url: "../Home/PreviousPage_Click",
            datatype: "json",
            data: { SearchText: SearchText, OrderBy: SortBy, PageSize: PageSize },
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




});