<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="BusinessBase.aspx.cs" Inherits="navtezcms.adminw.Admin.BusinessBase" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

    <link href="bower_components/bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Category
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">BusinessBase</li>
            </ol>

            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddCategory();"><i class="fa fa-plus"></i>&nbsp;Add New Category</a>
            </div>
        </section>

        <!-- Main content -->
        <section class="content container-fluid">

            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                           
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>

            </div>

        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <div id="addCategory" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Category"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="form-horizontal">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblError" ClientIDMode="Static" ForeColor="Red" runat="server" class="col-sm-12 control-label"></asp:Label>
                                        </div>
                                       
                                       
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddCategory();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveCategory" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Add" OnClick="btnSaveCategory_Click" />
                                    </div>
                                    <!-- /.box-footer -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hdnCategoryId" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="bower_components/fastclick/lib/fastclick.js"></script>

    <script src="ckeditor/ckeditor.js"></script>

    <script type="text/javascript">
        var categoryObj;
        var categoryId = 0;
        $(document).ready(function () {

            $('#txtSlug').focusout(function () {
                fnCanAddCategory();
            });

            $('.ddlLanguage').change(function () {
                if (categoryObj != undefined && categoryObj != "")
                    getLanguageDataByID();
            });

            $('#tblCategory').DataTable({
                "scrollX": true
            });

            $('#btnShowHome').on('click', function () {
                var IsShowHome = $("#hdnIsShowHome").val();
                if (IsShowHome == 1) {
                    $("#hdnIsShowHome").val(0);
                }
                else {
                    $("#hdnIsShowHome").val(1);
                }
            });

            $('#btnShowMenu').on('click', function () {
                var IsShowMenu = $("#hdnIsShowMenu").val();
                if (IsShowMenu == 1) {
                    $("#hdnIsShowMenu").val(0);
                }
                else {
                    $("#hdnIsShowMenu").val(1);
                }
            });
        });

        function fnGetExistingCategoryOrders() {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getexistingcategoryorders";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        $("#lblExistingMenuCategoryOrder").html(response);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }


        function fnCanAddCategory() {
            var request = { catid: categoryId, titleurl: $("#txtSlug").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddcategory";
            $.ajax({    
                type: 'POST',
                data: request,
                dataType: "json",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        if (response == "0") {
                            $("#lblError").html("Slug already exists");
                        }
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function openAddCategory() {
            clearAll();
            $('#addCategory').modal('show');
        }
        function closeAddCategory() {
            $('#addCategory').modal('hide');
        }



        $('#addCategory').on('show.bs.modal', function () {
            fnGetExistingCategoryOrders();
            if (categoryId != "") {
                getCategoryById(categoryId);
                $("#hdnCategoryId").val(categoryId);
                $('#lblHeader').html("Update Category");
                $("#<%=btnSaveCategory.ClientID%>").val('Update');
            }
        });

        $('#addCategory').on('hide.bs.modal', function () {
            categoryId = "";
            categoryObj = "";
        })

        function editCategory(CategoryID) {
            clearAll();
            categoryId = CategoryID;
            $('#addCategory').modal('show');
        }


        function getLanguageDataByID() {
            var languageID = $("#ddlLanguage").val();
            $.each(categoryObj.TitleData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtTitle").val(v.Translation);
                    return;
                }
            });

            $.each(categoryObj.SlugData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtSlug").val(v.Translation);
                    return;
                }
            });
        }

        function getCategoryById(CategoryID) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getCategorybyid&catid=" + CategoryID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        categoryObj = JSON.parse(response);
                        console.log(categoryObj);
                        $("#txtTitle").val(categoryObj.DefaultTitleToDisplay);
                        $("#txtSlug").val(categoryObj.Slug);
                        $("#txtCategoryOrder").val(categoryObj.CategoryOrder);
                        $("#hdnIsShowHome").val(categoryObj.CanShowAtHome);
                        $("#hdnIsShowMenu").val(categoryObj.CanShowOnMenu);
                        $("#hdnColor").val(categoryObj.Color);
                        $("#txtColor").val(categoryObj.Color);
                        $("#txtTitleUrlEnglish").val(categoryObj.TitleUrlEnglish);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("input:text").val("");
            $("#hdnCategoryId").val('');
            $("#imgCategoryImage").attr("src", "");
            $('#lblHeader').html("Add New Category");
            $("#<%=btnSaveCategory.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblCategory').DataTable({
                "scrollX": true
            });
        });
    </script>
    <%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-colorpicker/2.5.1/js/bootstrap-colorpicker.min.js"></script>--%>
    <script src="bower_components/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"></script>

    <script>
        $(function () {

            // Initiate the colorpicker
            $("#cp").colorpicker();

            // Unbind the keyup event from the input (which normally changes the color.)
            $("#txtColor").off("keyup");

            // Define a variable for the user's text at global scope.
            var txtColor;

            // Get the text value inputed by the user as soon as the colorPicker is accessed.
            $('#cp-icon').on("mousedown", function () {
                txtColor = $("#cp>input").val();
                //console.log(titleText);
                $("#txtColor").text(titleText);
                $("#hdnColor").val(titleText);
            });

            // On color selection, affect the input's text with it.
            $("#cp").on("changeColor", function () {
                //console.log("change event");
                var colorPicked = $('#cp').data('colorpicker').input[0].value;

                $("#txtColor").text(colorPicked);
                $("#hdnColor").val(colorPicked);
            });
        });
    </script>
</asp:Content>


