<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="navtezcms.adminw.Admin.Menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

    <link href="bower_components/bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Menu
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Menu</li>
            </ol>

            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddMenu();"><i class="fa fa-plus"></i>&nbsp;Add New Menu</a>
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
                                    <table id="tblMenu" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Name</th>
                                                <th>Menu Order</th>
                                                <th>Parent Type</th>
                                                <th>IsActive</th>
                                                <th>IsFooter</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptMenu" runat="server" OnItemCommand="rptMenu_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%#(Eval("DefaultTitleToDisplay")) %></td>
                                                        <td><%#(Eval("MenuOrder")) %></td>
                                                       <td><%#(Eval("ParentType")) %></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("IsActive")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("IsActive")) == false ?"InActive" : "Active" %></span></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("IsFooter")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("IsFooter")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editMenu('<%#Eval("ID")%>');" data-original-title="Edit Menu" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deleteMenu" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Menu"
                                                                runat="server" ID="btnDeleteMenu" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Menu?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptMenu" EventName="ItemCommand" />
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
    <div id="addMenu" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Menu"></asp:Label>
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
                                            <label class="col-sm-3 control-label">Parent Type</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlParentType" ClientIDMode="Static" runat="server" CssClass="form-control">
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
                                            <label class="col-sm-3 control-label">Menu Order *</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtMenuOrder" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblExistingMenuMenuOrder" ClientIDMode="Static" ForeColor="Red" runat="server" class="col-sm-12 control-label"></asp:Label>
                                        </div>
                                      
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Is Active *</label>
                                            <div class="col-sm-9">
                                                <button id="btnIsActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsActive" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Is Footer *</label>
                                            <div class="col-sm-9">
                                                <button id="btnIsFooter" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsFooter" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddMenu();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveMenu" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSaveMenu_Click" />
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


    <asp:HiddenField ID="hdnMenuId" Value="0" ClientIDMode="Static" runat="server" />
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
        var MenuObj;
        var MenuId = 0;
        $(document).ready(function () {
            
            $('#ddlLanguage').change(function () {
                if (MenuObj != undefined && MenuObj != "")
                    getLanguageDataByID();
            });

            $('#tblMenu').DataTable({
                "scrollX": true
            });

            $('#btnIsActive').on('click', function () {
                var IsActive = $("#hdnIsActive").val();
                if (IsActive == 1) {
                    $("#hdnIsActive").val(0);
                }
                else {
                    $("#hdnIsActive").val(1);
                }
            });

            $('#btnIsFooter').on('click', function () {
                var IsFooter = $("#hdnIsFooter").val();
                if (IsFooter == 1) {
                    $("#hdnIsFooter").val(0);
                }
                else {
                    $("#hdnIsFooter").val(1);
                }
            });
        });

        function fnGetExistingMenuOrders() {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getexistingmenuorders";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        $("#lblExistingMenuMenuOrder").html(response);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        
        function openAddMenu() {
            clearAll();
            MenuId = 0;
            $('#addMenu').modal('show');
        }
        function closeAddMenu() {
            $('#addMenu').modal('hide');
        }



        $('#addMenu').on('show.bs.modal', function () {
            fnGetExistingMenuOrders();
            if (MenuId != "") {
                getMenuById(MenuId);
                $("#hdnMenuId").val(MenuId);
                $('#lblHeader').html("Update Menu");
                $("#<%=btnSaveMenu.ClientID%>").val('Update');
            }
        });

        $('#addMenu').on('hide.bs.modal', function () {
            MenuId = "";
            MenuObj = "";
        })

        function editMenu(MenuID) {
            clearAll();
            MenuId = MenuID;
            $('#addMenu').modal('show');
        }


        function getLanguageDataByID() {
            var languageID = $("#ddlLanguage").val();
            $.each(MenuObj.TitleData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtTitle").val(v.Translation);
                    return;
                }
            });

            $.each(MenuObj.SlugData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtSlug").val(v.Translation);
                    return;
                }
            });
        }

        function getMenuById(MenuID) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getmenubyid&menuid=" + MenuID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        MenuObj = JSON.parse(response);
                        console.log(MenuObj);
                        $("#txtTitle").val(MenuObj.DefaultTitleToDisplay);
                        $("#ddlLanguage").val(MenuObj.DefaultLanguageID);
                        $("#txtMenuOrder").val(MenuObj.MenuOrder);
                        $("#ddlParentType").val(MenuObj.ParentTypeID);

                        $("#hdnIsActive").val(MenuObj.ISActive);
                        $("#hdnIsFooter").val(MenuObj.ISFooter);

                        if (MenuObj.ISActive == 0)
                            $("#btnIsActive").removeClass("active");
                        else
                            $("#btnIsActive").addClass("active");


                        if (MenuObj.ISFooter == 0)
                            $("#btnIsFooter").removeClass("active");

                        else
                            $("#btnIsFooter").addClass("active");

                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("#btnIsActive").removeClass("active").addClass("active");
            $("#btnIsFooter").removeClass("active").addClass("active");
            $("#ddlParentType").val(0);
            $('#lblError').hide();
            $("input:text").val("");
            $("#hdnMenuId").val('');
            $('#lblHeader').html("Add New Menu");
            $("#<%=btnSaveMenu.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblMenu').DataTable({
                "scrollX": true
            });
        });
    </script>
    
</asp:Content>


