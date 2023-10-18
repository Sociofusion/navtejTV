<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="schedulepost.aspx.cs" Inherits="navtezcms.adminw.Admin.schedulepost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Content Wrapper. Contains page content -->
            <deiv class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Category
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Schedule Posts</li>
                    </ol>
                    <div class="text-right">
                        <a class="btn btn-info btn-sm" onclick="editpost.aspx?mode=add"><i class="fa fa-plus"></i>&nbsp;Add New</a>
                    </div>
                </section>



                <!-- Main content -->
                <section class="content container-fluid">
                    <div class="row">
                         <div class="form-group">
                           <label class="col-sm-3 control-label">Language</label>
                             <div class="col-sm-9">
                               <asp:DropDownList ID="ddlLanguage" AutoPostBack="true"  OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                              </div> 
                           </div>

                        <div class="form-group">
                           <label class="col-sm-3 control-label">Category</label>
                             <div class="col-sm-9">
                               <asp:DropDownList ID="ddlCategory" AutoPostBack="true"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                              </div> 
                           </div>
                     </div>

                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <table id="tblList" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Image</th>
                                                <th>Title</th>
                                                <th>Category</th>
                                                <th>Language</th>
                                                <th>PostType</th>
                                                <th>Author</th>
                                                <th>ScheduleDate</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptData" runat="server" OnItemCommand="rptData_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#(Eval("ID"))%></td>
                                                        <td><%#(Eval("FeatureImageUrl")) %></td>
                                                        <td><%#(Eval("DefaultTitleToDisplay")) %></td>
                                                        <td><%#(Eval("CategoryName")) %></td>
                                                        <td><%#(Eval("Language")) %></td>
                                                        <td><%#(Eval("PostType")) %></td>
                                                        <td><%#(Eval("Author")) %></td>
                                                        <td><%#(Eval("ScheduleDateStr")) %></td>
                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISACtive")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISACtive")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editContent('<%#Eval("ID")%>');" data-original-title="Edit" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            </deiv>
            <!-- /.content-wrapper -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        var objForEdit;
        var IdForEdit = 0;
        $(document).ready(function () {
            $('#tblList').DataTable({
                "scrollX": true
            });
        });
    </script>

    <script type="text/javascript">
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblList').DataTable({
                "scrollX": true
            });
        });
    </script>
</asp:Content>


