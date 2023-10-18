<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="CustomPages.aspx.cs" Inherits="navtezcms.adminw.Admin.CustomPages" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Custom Pages
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Custom Pages</li>
            </ol>

            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddCustomPage();"><i class="fa fa-plus"></i>&nbsp;Add New CustomPage</a>
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
                                    <table id="tblCustomPage" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Page Name</th>
                                                <th>Slug</th>
                                                <th>Languge</th>
                                                <th>At Footer</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptCustomPage" runat="server" OnItemCommand="rptCustomPage_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%#(Eval("DefaultTitleToDisplay")) %></td>
                                                        <td><%#(Eval("Slug")) %></td>
                                                        <td><%# (Eval("Language"))%></td>

                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISFooter")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISFooter")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editCustomPage('<%#Eval("ID")%>');" data-original-title="Edit CustomPage" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deleteCustomPage" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Custom Page"
                                                                runat="server" ID="btnDeleteCustomPage" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Custom Page?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptCustomPage" EventName="ItemCommand" />
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
    <div id="addCustomPage" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Custom Page"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="form-horizontal">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <div id="lblError" clientidmode="Static" runat="server" class="alert alert-danger" role="alert">
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Language *</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlLanguage" ClientIDMode="Static" runat="server" CssClass="form-control ddlLanguage">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Page Name * (In English)</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtPageName" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
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
                                            <label class="col-sm-3 control-label">
                                                Description *
                                                <br />
                                                (In Any Language)</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDescription" ClientIDMode="Static" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Is Footer *</label>
                                            <div class="col-sm-9 control-label text-left">
                                                <button id="btnIsFooter" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnIsFooter" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddCustomPage();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveCustomPage" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSaveCustomPage_Click" />
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


    <asp:HiddenField ID="hdnCustomPageId" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>


    <script type="text/javascript">
        var CustomPageObj;
        var CustomPageId = 0;
        $(document).ready(function () {

            $('#txtSlug').focusout(function () {
                fnCanAddCustomPage();
            });

            $('#ddlLanguage').change(function () {
                if (CustomPageObj != undefined && CustomPageObj != "")
                    getLanguageDataByID();
            });

            $('#tblCustomPage').DataTable({
                "scrollX": true
            });

        });

        $('#btnIsFooter').on('click', function () {
            var IsShowHome = $("#hdnIsFooter").val();
            if (IsShowHome == 1) {
                $("#hdnIsFooter").val(0);
            }
            else {
                $("#hdnIsFooter").val(1);
            }
        });

        function fnCanAddCustomPage() {
            var request = { custompageid: CustomPageId, slug: $("#txtSlug").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddcustompage";
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

        function openAddCustomPage() {
            clearAll();
            CustomPageId = 0;
            $('#addCustomPage').modal('show');
        }
        function closeAddCustomPage() {
            $('#addCustomPage').modal('hide');
        }



        $('#addCustomPage').on('show.bs.modal', function () {
            if (CustomPageId != "") {
                getCustomPageById(CustomPageId);
                $("#hdnCustomPageId").val(CustomPageId);
                $('#lblHeader').html("Update Custom Page");
                $("#<%=btnSaveCustomPage.ClientID%>").val('Update');
            }
        });

        $('#addCustomPage').on('hide.bs.modal', function () {
            CustomPageId = "";
            CustomPageObj = "";
        })

        function editCustomPage(CustomPageID) {
            clearAll();
            CustomPageId = CustomPageID;
            $('#addCustomPage').modal('show');
        }


        function getLanguageDataByID() {
            var languageID = $("#ddlLanguage").val();
            // Title
            $.each(CustomPageObj.TitleData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtTitle").val(v.Translation);
                    return;
                }
            });
            // Description
            $.each(CustomPageObj.DescriptionData, function (i, v) {
                if (v.LanguageID == languageID) {
                    CKEDITOR.instances.txtDescription.setData(v.Translation);
                    return;
                }
            });
        }

        function getCustomPageById(CustomPageID) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getcustompagebyid&custompageid=" + CustomPageID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        CustomPageObj = JSON.parse(response);
                        console.log(CustomPageObj);
                        $("#txtPageName").val(CustomPageObj.PageName);
                        $("#txtTitle").val(CustomPageObj.DefaultTitleToDisplay);
                        $("#txtSlug").val(CustomPageObj.Slug);

                        if (CustomPageObj.ISFooter == 0)
                            $("#btnIsFooter").removeClass("active");
                        else
                            $("#btnIsFooter").addClass("active");


                        CKEDITOR.instances.txtDescription.setData(CustomPageObj.DefaultDescriptionToDisplay);

                        $("#hdnIsFooter").val(CustomPageObj.ISFooter);
                        $("#ddlLanguage").val(CustomPageObj.DefaultLanguageID);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("#btnIsFooter").removeClass("active").addClass("active");
            $('#lblError').hide();
            $("input:text").val("");
            CKEDITOR.instances.txtDescription.setData("");
            $("#hdnCustomPageId").val('');
            $("#imgCustomPageImage").attr("src", "");
            $('#lblHeader').html("Add New CustomPage");
            $("#<%=btnSaveCustomPage.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblCustomPage').DataTable({
                "scrollX": true
            });
        });
    </script>


    <script src="ckeditor/ckeditor.js"></script>
    <script src="ckeditor/adapters/jquery.js"></script>


    <script>
        $(function () {
            CKEDITOR.replace('txtDescription', {
                // Define the toolbar groups as it is a more accessible solution.
                toolbarGroups: [
                    {
                        "name": "basicstyles",
                        "groups": ["basicstyles"]
                    },
                    {
                        "name": "paragraph",
                        "groups": ["list", "blocks"]
                    },
                    {
                        "name": "document",
                        "groups": ["mode"]
                    },
                    {
                        "name": "insert",
                        "groups": ["insert"]
                    },
                    {
                        "name": "styles",
                        "groups": ["styles"]
                    }
                ],
                // Remove the redundant buttons from toolbar groups defined above.
                removeButtons: 'Underline,Strike,Subscript,Superscript,Anchor,Styles,Specialchar,PasteFromWord'
            });
        });


    </script>

</asp:Content>


