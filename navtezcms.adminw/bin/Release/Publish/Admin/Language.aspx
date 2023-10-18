<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Language.aspx.cs" Inherits="navtezcms.adminw.Admin.Language" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Language
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Language</li>
            </ol>

            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddLanguage();"><i class="fa fa-plus"></i>&nbsp;Add New Language</a>
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
                                    <table id="tblLanguage" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Name</th>
                                                <th>IS Active</th>
                                                <th>IS Default</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptLanguage" runat="server" OnItemCommand="rptLanguage_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%#(Eval("Name")) %></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISActive")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISActive")) == false ?"InActive" : "Active" %></span></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISDefault")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISDefault")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editLanguage('<%#Eval("ID")%>');" data-original-title="Edit Language" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptLanguage" EventName="ItemCommand" />
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
    <div id="addLanguage" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Language"></asp:Label>
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
                                            <label class="col-sm-3 control-label">
                                                Title *</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">IS Active</label>
                                            <div class="col-sm-9">
                                                <button id="btnIsActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsActive" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">IS Default</label>
                                            <div class="col-sm-9">
                                                <button id="btnIsDefault" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsDefault" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddLanguage();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveLanguage" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSaveLanguage_Click" />
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


    <asp:HiddenField ID="hdnLanguageId" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

  
    <script type="text/javascript">
        var LanguageObj;
        var LanguageId = 0;
        $(document).ready(function () {
            
            $('#tblLanguage').DataTable({
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

            $('#btnIsDefault').on('click', function () {
                var IsDefault = $("#hdnIsDefault").val();
                if (IsDefault == 1) {
                    $("#hdnIsDefault").val(0);
                }
                else {
                    $("#hdnIsDefault").val(1);
                }
            });
        });

        
        function fnCanAddLanguage() {
            var request = { languageid: LanguageId, name: $("#txtName").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddlanguage";
            $.ajax({
                type: 'POST',
                data: request,
                dataType: "json",
                url: url,
                success: function (response) {
                    if (response == "0") {
                        $("#lblError").show();
                        $("#lblError").text("Language already exists");
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

        function openAddLanguage() {
            clearAll();
            LanguageId = 0;
            $('#addLanguage').modal('show');
        }
        function closeAddLanguage() {
            $('#addLanguage').modal('hide');
        }



        $('#addLanguage').on('show.bs.modal', function () {
            if (LanguageId != "") {
                getLanguageById(LanguageId);
                $("#hdnLanguageId").val(LanguageId);
                $('#lblHeader').html("Update Language");
                $("#<%=btnSaveLanguage.ClientID%>").val('Update');
            }
        });

        $('#addLanguage').on('hide.bs.modal', function () {
            LanguageId = "";
            LanguageObj = "";
        })

        function editLanguage(LanguageID) {
            clearAll();
            LanguageId = LanguageID;
            $('#addLanguage').modal('show');
        }


        function getLanguageDataByID() {
            var languageID = $("#ddlLanguage").val();
        }

        function getLanguageById(LanguageID) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getlanguagebyid&languageid=" + LanguageID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        LanguageObj = JSON.parse(response);
                        console.log(LanguageObj);
                        $("#txtName").val(LanguageObj.Name);
                       
                        $("#hdnIsActive").val(LanguageObj.ISActive);
                        $("#hdnIsDefault").val(LanguageObj.ISDefault);

                        if (LanguageObj.ISActive == 0)
                            $("#btnIsActive").removeClass("active");
                        else
                            $("#btnIsActive").addClass("active");


                        if (LanguageObj.ISDefault == 0)
                            $("#btnIsDefault").removeClass("active");

                        else
                            $("#btnIsDefault").addClass("active");
                        
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("#btnIsActive").removeClass("active").addClass("active");
            $("#btnIsDefault").removeClass("active").addClass("active");
            $('#lblError').hide();
            $("input:text").val("");
            $("#hdnLanguageId").val('');
            $("#imgLanguageImage").attr("src", "");
            $('#lblHeader').html("Add New Language");
            $("#<%=btnSaveLanguage.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblLanguage').DataTable({
                "scrollX": true
            });
        });
    </script>
   
</asp:Content>


