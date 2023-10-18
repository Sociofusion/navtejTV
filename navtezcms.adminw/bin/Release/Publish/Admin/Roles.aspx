<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="navtezcms.adminw.Admin.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Roles
            </h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Roles</li>
            </ol>
            <div class="text-right">
                <a class="btn btn-info" href="AddUpdateRole.aspx"><i class="fa fa-plus"></i>&nbsp;Add New Role</a>
            </div>
        </section>

        <!-- Main content -->
        <section class="content container-fluid">

            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-danger">
                        <div class="box-header">
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table id="tblRoles" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Role Name</th>
                                                <th>Menu Ids</th>    
                                                <th>Category Ids</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptRoles" runat="server" OnItemCommand="rptRoles_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# (Eval("RoleName"))%></td>
                                                        <td><%# (navtezcms.BO.Common.Truncate(Convert.ToString(Eval("MenuIds")),50))%></td>
                                                        <td><%# (navtezcms.BO.Common.Truncate(Convert.ToString(Eval("CategoryIds")),50))%></td>
                                                        <td>
                                                            <a href='AddUpdateRole.aspx?id=<%#Eval("ID")%>' class="btn btn-xs btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deleteRoles" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Roles"
                                                                runat="server" ID="btnDeleteRoles" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-xs btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Roles?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptRoles" EventName="ItemCommand" />
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
    <asp:HiddenField ID="hdnRolesId" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">

    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblRoles').DataTable({
                "scrollX": true,
                "pageLength": 20
            });
        });

    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblRoles').DataTable({
                "scrollX": true,
                "pageLength": 20
            });
        });
    </script>
</asp:Content>
