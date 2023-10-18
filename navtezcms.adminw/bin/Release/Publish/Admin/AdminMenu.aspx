<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="AdminMenu.aspx.cs" Inherits="navtezcms.adminw.Admin.AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>AdminMenu
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">AdminMenu</li>
            </ol>

            <div class="text-right">
                <a href="AddUpdateAdminMenu.aspx" class="btn btn-info btn-sm"><i class="fa fa-plus"></i>&nbsp;Add New AdminMenu</a>
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
                                    <table id="tblAdminMenu" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>MenuName</th>
                                                <th>MenuLink</th>
                                                <th>MenuLevel</th>
                                                <th>ParentID</th>
                                                <th>Icon</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptAdminMenu" runat="server" OnItemCommand="rptAdminMenu_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# (Eval("MenuName"))%></td>
                                                        <td><%# (Eval("MenuLink"))%></td>
                                                        <td><%# (Eval("MenuLevel"))%></td>
                                                        <td><%# (Eval("ParentID"))%></td>
                                                        <td><i class='<%# (Eval("Icon"))%>'></i></td>
                                                        <td>
                                                            <a href="AddUpdateAdminMenu.aspx?id=<%#Eval("ID")%>" class="btn btn-xs btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="IsActive" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active" : "Deactive" %>'
                                                                runat="server" ID="btnActive" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active" : "Deactive" %>'
                                                                CssClass='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"btn btn-xs btn-detail btn-success" : "btn btn-xs btn-detail btn-warning" %>' OnClientClick="return confirm('Confirm changes?');" />
                                                            <asp:LinkButton CommandName="deleteAdminMenu" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Menu"
                                                                runat="server" ID="btnDeleteMenu" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-xs btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Menu?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptAdminMenu" EventName="ItemCommand" />
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


    <asp:HiddenField ID="hdnAdminMenuId" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>


    <script type="text/javascript">

        $('#tblAdminMenu').DataTable({
            "scrollX": true
        });

    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblAdminMenu').DataTable({
                "scrollX": true
            });
        });
    </script>

</asp:Content>


