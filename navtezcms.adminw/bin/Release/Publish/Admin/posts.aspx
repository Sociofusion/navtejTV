<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="Posts.aspx.cs" Inherits="navtezcms.adminw.Admin.Posts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Posts
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Posts</li>
            </ol>

            <div class="text-right">
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <asp:TextBox ID="txtStartDate" autocomplete="off" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtEndDate" autocomplete="off" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">

                    <asp:LinkButton ID="btnFilter" runat="server" CssClass="btn-sm btn btn-warning" OnClick="btnFilter_Click"><i class="fa fa-refresh"></i> Filter </asp:LinkButton>
                </div>

            </div>

            <div class="text-right">
                <a class="btn btn-info btn-sm" href="AddPost.aspx?mode=add"><i class="fa fa-plus"></i>&nbsp;Add New Post</a>

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
                                    <table id="tblPost" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Image</th>
                                                <th>Title</th>
                                                <th>Category</th>
                                                <th>Language</th>
                                                <th>PostType</th>
                                                <th>Author</th>
                                                <th>CreatedOn</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptPost" runat="server" OnItemCommand="rptPost_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#(Eval("ID"))%></td>
                                                        <td>
                                                            <img src='https://storage.googleapis.com/navtejcms/post/<%# Eval("ImageBig")%>' class="imgPost" /></td>
                                                        <td><%# navtezcms.BO.Common.Truncate(Convert.ToString(Eval("DefaultTitleToDisplay")),18) %></td>
                                                        <td><%#(Eval("CategoryName")) %></td>
                                                        <td><%#(Eval("Language")) %></td>
                                                        <td><%#(Eval("PostType")) %></td>
                                                        <td><%#(Eval("Author")) %></td>
                                                        <td><%#(Eval("CreatedOnStr")) %></td>
                                                        <td><span class='<%# Convert.ToInt32(Eval("StatusID")) == Convert.ToInt32(navtezcms.DAL.DBHelper.EStatus.Draft) ? "btn btn-xs btn-detail btn-warning" : Convert.ToInt32(Eval("StatusID")) == Convert.ToInt32(navtezcms.DAL.DBHelper.EStatus.Pending) ? "btn btn-xs btn-detail btn-info" : "btn btn-xs btn-detail btn-success" %>'>
                                                            <%# Convert.ToInt32(Eval("StatusID")) == Convert.ToInt32(navtezcms.DAL.DBHelper.EStatus.Draft) ?"Draft" : Convert.ToInt32(Eval("StatusID")) == Convert.ToInt32(navtezcms.DAL.DBHelper.EStatus.Pending) ?"Pending" : "Approved" %> </span></td>
                                                        <td>
                                                            <asp:LinkButton CommandName="ISActive" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title='<%# Convert.ToBoolean(Eval("ISActive")) == true ?"Active" : "InActive" %>'
                                                                runat="server" ID="btnActive" Text='<%# Convert.ToBoolean(Eval("ISActive")) == true ?"Active" : "InActive" %>'
                                                                CssClass='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"btn btn-xs btn-detail btn-success" : "btn btn-xs btn-detail btn-warning" %>'
                                                                OnClientClick="return confirm('Confirm changes?');" />

                                                            <a data-toggle="tooltip" href="Addpost.aspx?mode=edit&id=<%#Eval("ID")%>" data-original-title="Edit"
                                                                class="btn btn-xs btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>

                                                            <asp:LinkButton CommandName="Delete" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title='<%# Convert.ToBoolean(Eval("ISActive")) == true ?"Active" : "InActive" %>'
                                                                runat="server" ID="btnDelete" Text="<i class='fa fa-trash'></i>&nbsp; Delete"
                                                                CssClass="btn btn-xs btn-danger" OnClientClick="return confirm('Do you want to delete this post?');" />

                                                            <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("StatusID")) == Convert.ToInt32(navtezcms.DAL.DBHelper.EStatus.Draft) ? true : false %>'
                                                                CommandName="sendApproval" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Send Approval"
                                                                runat="server" ID="btnSendApproval" Text="<i class='fa fa-paper-plane-o'></i>&nbsp; Send Approval"
                                                                CssClass="btn btn-xs btn-primary btn-detail" OnClientClick="return confirm('Do you want to send this for approval?');" />

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptPost" EventName="ItemCommand" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>


    <script type="text/javascript">
        var objForEdit;
        var IdForEdit = 0;
        $(document).ready(function () {
            $('#tblPost').DataTable({
                "scrollX": true
            });



            $('#txtStartDate').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true
            });

            $('#txtEndDate').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true
            });


        });
    </script>

    <script type="text/javascript">
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblPost').DataTable({
                "scrollX": true
            });
        });
    </script>

</asp:Content>


