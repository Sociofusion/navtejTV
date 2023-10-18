<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="adplacement.aspx.cs" Inherits="navtezcms.adminw.Admin.adplacement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
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
                <li class="active">Ads Placement</li>
            </ol>
            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddPopup();"><i class="fa fa-plus"></i>&nbsp;Add New Placement</a>
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
                                    <table id="tblList" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Name</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptData" runat="server" OnItemCommand="rptData_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#(Eval("ID"))%></td>
                                                        <td><%#(Eval("PlacementAreaName")) %></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISActive")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISACtive")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editContent('<%#Eval("ID")%>');" data-original-title="Edit Placement" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deletePlacement" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Placement"
                                                                runat="server" ID="btnDeletePlacement" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Placement?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptData" EventName="ItemCommand" />
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
    <div id="addContent" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Placement"></asp:Label>
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
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Area Name  (can not have space and special characters) *</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtAreaName" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Active *</label>
                                            <div class="col-sm-9">
                                                <button id="btnActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnActive" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closePopup();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSave" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSave_Click" />
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
    <asp:HiddenField ID="hdnEditID" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="bower_components/fastclick/lib/fastclick.js"></script>

    <script type="text/javascript">
        var objForEdit;
        var IdForEdit = 0;
        $(document).ready(function () {

            $('#txtAreaName').focusout(function () {
                if ($.trim($('#txtAreaName').val()).length > 0)
                    fnCheckForDuplicate();
            });


            $('#tblList').DataTable({
                "scrollX": true
            });

            $('#btnActive').on('click', function () {
                var isActive = $("#hdnActive").val();
                if (isActive == 1) {
                    $("#hdnActive").val(0);
                }
                else {
                    $("#hdnActive").val(1);
                }
            });
        });
        function fnCheckForDuplicate() {
            var request = { id: IdForEdit, areaname: $("#txtAreaName").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddadverplacement";
            $.ajax({
                type: 'POST',
                data: request,
                dataType: "json",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        if (response == "0") {
                            $("#lblError").html("Placement Area already exists");
                        }
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
        function openAddPopup() {
            clearAll();
            $('#addContent').modal('show');
        }
        function closePopup() {
            $('#addContent').modal('hide');
        }
        $('#addContent').on('show.bs.modal', function () {
            if (IdForEdit != "") {
                getForEdit(IdForEdit);
                $("#hdnEditID").val(IdForEdit);
                $('#lblHeader').html("Update");
                $("#<%=btnSave.ClientID%>").val('Update');
            }
        });

        $('#addContent').on('hide.bs.modal', function () {
            clearAll();
        })

        function editContent(editId) {
            clearAll();
            IdForEdit = editId;
            $('#addContent').modal('show');
        }

        function getForEdit(id) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getplacementbyid&id=" + id + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        objForEdit = JSON.parse(response);
                        console.log(objForEdit);
                        $("#txtAreaName").val(objForEdit.PlacementAreaName);

                        if (objForEdit.ISActive == 0)
                            $("#btnActive").removeClass("active");
                        else
                            $("#btnActive").addClass("active");


                        $("#hdnActive").val(objForEdit.ISActive);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("input:text").val("");
            $("#hdnEditID").val('');
            $('#lblHeader').html("Add New");
            IdForEdit = "";
            objForEdit = "";
            $("#<%=btnSave.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblList').DataTable({
                "scrollX": true
            });
        });
    </script>
</asp:Content>


