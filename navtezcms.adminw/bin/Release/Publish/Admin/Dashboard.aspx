<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="navtezcms.adminw.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sales td {
            font-family: Impact;
            font-size: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->


        <!-- Main content -->
        <section class="content container-fluid">

            <!-- Small boxes (Stat box) -->

            <div class="row">
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litCategory" runat="server" Text="0"></asp:Literal></h3>

                            <p>Category</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-list-ul"></i>
                        </div>
                        <a href="Category.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-yellow">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litPosts" runat="server" Text="0"></asp:Literal></h3>

                            <p>Posts</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-file-o"></i>
                        </div>
                        <a href="Posts.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-red">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litCustomPage" runat="server" Text="0"></asp:Literal></h3>

                            <p>Custom Pages</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-file-code-o"></i>
                        </div>
                        <a href="CustomPages.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-green">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litLanguage" runat="server" Text="0"></asp:Literal></h3>
                            <p>Language</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-language"></i>
                        </div>
                        <a href="Language.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>


                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-yellow">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litAdvertisements" runat="server" Text="0"></asp:Literal></h3>

                            <p>Advertisements</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-bar-chart"></i>
                        </div>
                        <a href="advertisement.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-red">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litEmployee" runat="server" Text="0"></asp:Literal></h3>

                            <p>Employee</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-users"></i>
                        </div>
                        <a href="Employee.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-green">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="LitRole" runat="server" Text="0"></asp:Literal></h3>
                            <p>Roles</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-users"></i>
                        </div>
                        <a href="Roles.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <!-- ./col -->
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua">
                        <div class="inner">
                            <h3>
                                <asp:Literal ID="litAdminMenu" runat="server" Text="0"></asp:Literal></h3>
                            <p>Admin Menu</p>
                        </div>
                        <div class="icon">
                            <i class="fa fa-list-alt"></i>
                        </div>
                        <a href="AdminMenu.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>


            </div>

        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
</asp:Content>

