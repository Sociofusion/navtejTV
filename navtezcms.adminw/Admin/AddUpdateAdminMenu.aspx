<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="AddUpdateAdminMenu.aspx.cs" Inherits="navtezcms.adminw.Admin.AddUpdateAdminMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="AdminMenu.aspx"><i class="fa fa-list"></i>Menu List</a></li>
                <li class="active">Add/Update Menu</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content container-fluid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="form-horizontal">

                                        <div id="lblError" clientidmode="Static" runat="server" style="display: none;" class="alert alert-danger" role="alert">
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><span class="red">*</span>MenuName</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMenuName" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMenuName" runat="server" ControlToValidate="txtMenuName"
                                                    Display="Dynamic" ErrorMessage="Please enter Menu name" SetFocusOnError="True"
                                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="txtid" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><span class="red">*</span>MenuLevel</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddllevel_SelectedIndexChanged"
                                                    Width="100px">
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="rfvMenuLevel" runat="server" ControlToValidate="ddllevel"
                                                    Display="Dynamic" ErrorMessage="Please enter MenuLevel" SetFocusOnError="True"
                                                    ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><span class="red">*</span>ParentID</label>
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblparent" runat="server" Text="Self" Style="background: #e9e9e9; border: solid 1px #000; padding: 5px;" CssClass="form-control"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlp1" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlp1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlp2" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><span class="red">*</span>Menu Link</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtlink" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><span class="red">*</span>Position</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPosition" runat="server" ClientIDMode="Static" Text="0" TextMode="Number" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPosition" runat="server" ControlToValidate="txtPosition"
                                                    Display="Dynamic" ErrorMessage="Please enter Position" SetFocusOnError="True"
                                                    ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <label class="col-sm-3 control-label"><span class="red">*</span>Icon</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right" ValidationGroup="v" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="v" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

    <asp:HiddenField ID="hdnAdminMenuId" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
</asp:Content>
