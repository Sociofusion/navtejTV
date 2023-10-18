<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="advertisement.aspx.cs" Inherits="navtezcms.adminw.Admin.advertisement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <style>
        .img_ad {
            height: 75px;
            margin: 15px;
            max-width: 97%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Advertisement
            </h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Advertisement</li>
            </ol>
            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddPopup();"><i class="fa fa-plus"></i>&nbsp;Add New</a>
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
                                                <th>Asset</th>
                                                <th>Placement</th>
                                                <th>BannerType</th>
                                                <th>Ad Size</th>

                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptData" runat="server" OnItemCommand="rptData_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#(Eval("ID"))%></td>
                                                        <td>
                                                            <img style="height: 30px; max-width: 250px;" src='<%#Eval("AssetFullUrl")%>' /></td>
                                                        <td><%#(Eval("PlacementAreaName")) %></td>
                                                        <td><%#(Eval("AdType")) %></td>
                                                        <td><%#(Eval("AdSize")) %></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editContent('<%#Eval("ID")%>');" data-original-title="Edit Advertisement" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            
                                                            <asp:LinkButton CommandName="IsActive" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active" : "InActive" %>'
                                                                runat="server" ID="btnActive" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active" : "InActive" %>'
                                                                CssClass='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"btn btn-sm btn-detail btn-success" : "btn btn-sm btn-detail btn-warning" %>'
                                                                OnClientClick="return confirm('Confirm changes?');" />


                                                            <asp:LinkButton CommandName="delete" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete"
                                                                runat="server" ID="btnDelete" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this record?');" /></td>
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
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New"></asp:Label>
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
                                            <label class="col-sm-3 control-label">Select Placement*</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlPlacement" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Ad Size*</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlAdSize" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Size 728X90" Value="Size 728X90"></asp:ListItem>
                                                    <asp:ListItem Text="Size 468X60" Value="Size 468X60"></asp:ListItem>
                                                    <asp:ListItem Text="Size 234X60" Value="Size 234X60"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Ad Click Link</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtAdLink" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Ad Type*</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlAdType" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                    <asp:ListItem Text="Ad from Asset" Value="AdFromAsset"></asp:ListItem>
                                                    <asp:ListItem Text="Ad from Code" Value="AdFromCode"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="divFileUpload">
                                            <label class="col-sm-3 control-label">File</label>
                                            <div class="col-sm-9">
                                                <asp:FileUpload ID="fupFile" runat="server" />
                                            </div>
                                            <asp:Image ID="imgPreview" ClientIDMode="Static" runat="server" CssClass="img_ad" />
                                        </div>
                                        <div class="form-group" id="divAdCode">
                                            <label class="col-sm-3 control-label">Code</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtAdCode" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" runat="server" Rows="10"></asp:TextBox>
                                            </div>
                                        </div>

                                         
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Active *</label>
                                            <div class="col-sm-9">
                                                <button id="btnISActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true">
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
    <asp:HiddenField ID="hdnEditID" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">
        var objForEdit;
        var IdForEdit = 0;
        $(document).ready(function () {

            $("#divAdCode").hide();

            $('#tblList').DataTable({
                "scrollX": true
            });

            $('#btnISActive').on('click', function () {
                var isActive = $("#hdnActive").val();
                if (isActive == 1) {
                    $("#hdnActive").val(0);
                }
                else {
                    $("#hdnActive").val(1);
                }
            });

            $("#ddlAdType").change(function () {
                if ($('option:selected', this).val() == "AdFromAsset") {
                    $("#divFileUpload").show();
                    $("#divAdCode").hide();
                }
                else {
                    $("#divAdCode").show();
                    $("#divFileUpload").hide();
                }
            });
        });

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
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getadvertisement&id=" + id + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        objForEdit = JSON.parse(response);
                        console.log(objForEdit);
                        $("#ddlPlacement").val(objForEdit.PlacementAreaID);
                        $("#ddlAdSize").val(objForEdit.AdSize);
                        $("#ddlAdType").val(objForEdit.AdType);
                        $("#txtAdLink").val(objForEdit.AdLink);
                        $("#imgPreview").attr("src", objForEdit.AssetFullUrl)
                        $("#hdnActive").val(objForEdit.ISActive);

                        if (objForEdit.ISActive == 0)
                            $("#btnISActive").removeClass("active");
                        else
                            $("#btnISActive").addClass("active");


                        if (objForEdit.AdType == "AdFromCode") {
                            $("#divFileUpload").hide();
                            $("#txtAdCode").val(objForEdit.AdCode);
                            $("#divAdCode").show();
                        }
                        else {
                            $("#divFileUpload").show();
                            $("#divAdCode").hide();
                        }
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
