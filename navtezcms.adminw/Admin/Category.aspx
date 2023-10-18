<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="navtezcms.adminw.Admin.Category" %>


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
                <li class="active">Category</li>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table id="tblCategory" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Name</th>
                                                <th>Slug</th>
                                                <th>Languge</th>
                                                <th>Menu Order</th>
                                                <th>Color</th>
                                                <th>At Homepage</th>
                                                <th>At Menu</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%#(Eval("DefaultTitleToDisplay")) %></td>
                                                        <td><%#(Eval("Slug")) %></td>
                                                        <td><%# (Eval("Language"))%></td>
                                                        <td><%# (Eval("CategoryOrder"))%></td>
                                                        <td>
                                                            <div style='width: 60px; height: 30px; background-color: <%# (Eval("Color"))%>'></div>
                                                        </td>

                                                        <td><span class='<%# Convert.ToBoolean(Eval("CanShowAtHome")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("CanShowAtHome")) == false ?"InActive" : "Active" %></span></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("CanShowOnMenu")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("CanShowOnMenu")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editCategory('<%#Eval("ID")%>');" data-original-title="Edit Category" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deleteCategory" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Category"
                                                                runat="server" ID="btnDeleteCategory" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Category?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptCategory" EventName="ItemCommand" />
                                </Triggers>
                            </asp:UpdatePanel>

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

                                        <div id="lblError" clientidmode="Static" runat="server" style="display: none;" class="alert alert-danger" role="alert">
                                        </div>

                                         <div class="form-group">
                                            <label class="col-sm-3 control-label">Parent Category</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlParentCategory" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Language *</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlLanguage" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Title * (In Any Language)</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtTitle" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Slug * (In English)</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtSlug" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Menu Order *</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtCategoryOrder" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblExistingMenuCategoryOrder" ClientIDMode="Static" ForeColor="Red" runat="server" class="col-sm-12 control-label"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Color *</label>
                                            <div class="col-sm-9">
                                                <div class="input-group" id="cp">
                                                    <input type="text" class="form-control" name="txtColor" id="txtColor" />
                                                    <span class="input-group-addon colorpicker-component"><i id="cp-icon"></i></span>
                                                    <asp:HiddenField ClientIDMode="Static" ID="hdnColor" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Show At Home Page *</label>
                                            <div class="col-sm-9">
                                                <button id="btnShowHome" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsShowHome" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Show At Menu *</label>
                                            <div class="col-sm-9">
                                                <button id="btnShowMenu" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsShowMenu" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddCategory();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveCategory" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSaveCategory_Click" />
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
   

    <script type="text/javascript">
        var categoryObj;
        var categoryId = 0;
        $(document).ready(function () {

            $('#txtSlug').focusout(function () {
                fnCanAddCategory();
            });

            $('#ddlLanguage').change(function () {
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
                    if (response == "0") {
                        $("#lblError").show();
                        $("#lblError").text("Slug already exists");
                    }
                    else {
                        $('#lblError').hide();
                        $("#lblError").text("");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function openAddCategory() {
            clearAll();
            categoryId = 0;
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
                        $("#ddlParentCategory").val(categoryObj.ParentCategoryID);

                        $("#hdnIsShowHome").val(categoryObj.CanShowAtHome);
                        $("#hdnIsShowMenu").val(categoryObj.CanShowOnMenu);

                        if (categoryObj.CanShowAtHome == 0)
                            $("#btnShowHome").removeClass("active");
                        else
                            $("#btnShowHome").addClass("active");


                        if (categoryObj.CanShowOnMenu == 0)
                            $("#btnShowMenu").removeClass("active");

                        else
                            $("#btnShowMenu").addClass("active");


                        $("#hdnColor").val(categoryObj.Color);
                        $("#txtColor").val(categoryObj.Color);
                        
                        $('#cp-icon').css('background-color', categoryObj.Color);
                        $("#txtTitleUrlEnglish").val(categoryObj.TitleUrlEnglish);
                        $("#ddlLanguage").val(categoryObj.DefaultLanguageID);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("#btnShowHome").removeClass("active").addClass("active");
            $("#btnShowMenu").removeClass("active").addClass("active");
            $("#ddlParentCategory").val(0);
            $('#lblError').hide();
            $("input:text").val("");
            $("#hdnCategoryId").val('');
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
                $("#txtColor").text(txtColor);
                $("#hdnColor").val(txtColor);

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


