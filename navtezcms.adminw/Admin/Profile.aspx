<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="navtezcms.adminw.Admin.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Profile
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Profile</li>
                    </ol>
                </section>
            </div>
        </div>

        <!-- Main content -->
        <section class="content container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h4>Update Profile</h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="form-group">
                                <label><b>Name</b></label>
                                <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Enter Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtName" ValidationGroup="validateProfile" runat="server" ErrorMessage="Enter Name" Display="Dynamic" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label><b>Email</b></label>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Email" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="box-footer text-right">
                            <div class="form-group">
                                <div class="send-block">
                                    <asp:Label ID="lblTopAlert" runat="server" Visible="false" CssClass="text-danger"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="send-block">
                                    <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" ValidationGroup="validateProfile" class="btn btn-info" OnClick="btnUpdateProfile_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h4>Update Password</h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                <label><b>Current password</b></label>
                                <asp:TextBox ID="txtOldPassword" runat="server" class="form-control" placeholder="Enter Current Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtOldPassword" ValidationGroup="validate" runat="server" ErrorMessage="Enter Old Password" Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label><b>New password</b></label>
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Enter New Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" ValidationGroup="validate" ErrorMessage="Enter Password" Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label><b>Confirm password</b></label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" placeholder="Enter Confirm Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtConfirmPassword" runat="server" ValidationGroup="validate" ErrorMessage="Enter Confirm Password" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ValidationGroup="validate" ID="cmpConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" CssClass="ValidationError" ControlToCompare="txtPassword"
                                    ForeColor="Red" ErrorMessage="Password and Confirm Password must be the same" />
                            </div>


                        </div>
                        <div class="box-footer text-right">
                            <div class="form-group">
                                <div class="send-block">
                                    <asp:Label ID="litMessage" runat="server" Visible="false" CssClass="text-danger"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="send-block">
                                    <asp:Button ID="btnSave" runat="server" Text="Update Password" ValidationGroup="validate" class="btn btn-info" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
</asp:Content>
