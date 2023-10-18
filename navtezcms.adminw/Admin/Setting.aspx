<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Setting.aspx.cs" Inherits="navtezcms.adminw.Admin.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Setting
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Setting</li>
                    </ol>
                </section>
            </div>
        </div>

        <!-- Main content -->
        <section class="content container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="form-horizontal">
                            <!-- /.box-header -->
                            <div class="box-body">

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Logo</label>
                                    <div class="col-sm-4">
                                        <div class="image-preview">
                                            <asp:Image ID="imgLogo" ClientIDMode="Static" runat="server" Height="60" alt="uploaded Image" Style="height: 60px; background: #000; border: 1px #e3e3e3 solid; margin-bottom: 10px;"
                                                Visible="false" />

                                            <asp:FileUpload ID="fupLogo_PreviewImage" runat="server" onchange="showLogo(this);" CssClass="mt20" />
                                            <label class="control-label">* Prefered Size: (135x65) or Square Sized Image</label>
                                        </div>
                                    </div>

                                    <label class="col-sm-2 control-label">Footer Logo</label>
                                    <div class="col-sm-4">
                                        <div class="image-preview">
                                            <asp:Image ID="imgLogoFooter" ClientIDMode="Static" runat="server" Height="60" alt="uploaded Image" Style="height: 60px; border: 1px #e3e3e3 solid; margin-bottom: 10px;"
                                                Visible="false" />
                                            <asp:FileUpload ID="fupFooterLogo_PreviewImage" runat="server" onchange="showFooterLogo(this);" CssClass="mt20" />
                                            <label class="control-label">* Prefered Size: (135x65) or Square Sized Image</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Facebook Link</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFacebookLink" runat="server" class="form-control" placeholder="Enter Facebook Link"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtFacebookLink" ValidationGroup="validateProfile" runat="server" ErrorMessage="Enter Facebook Link" Display="Dynamic" ControlToValidate="txtFacebookLink"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">Twitter Link</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTwitterLink" runat="server" class="form-control" placeholder="Enter Twitter Link"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtTwitterLink" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Twitter Link" Display="Dynamic" ControlToValidate="txtTwitterLink"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Instagram Link</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtInstagramLink" runat="server" class="form-control" placeholder="Enter Instagram Link"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtInstagramLink" ValidationGroup="validateProfile" runat="server" ErrorMessage="Enter Instagram Link" Display="Dynamic" ControlToValidate="txtInstagramLink"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">Youtube Link</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtYoutubeLink" runat="server" class="form-control" placeholder="Enter Twitter Link"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvYoutubeLink" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Youtube Link" Display="Dynamic" ControlToValidate="txtTwitterLink"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Youtube Video URL</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtYoutubeVideoURL" runat="server" class="form-control" placeholder="Enter Youtube Video URL"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtYoutubeVideoURL" ValidationGroup="validateProfile" runat="server" ErrorMessage="Enter Youtube Video URL" Display="Dynamic" ControlToValidate="txtYoutubeVideoURL"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">Copyright</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCopyright" runat="server" class="form-control" placeholder="Enter Copyright"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCopyright" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Copyright"
                                            Display="Dynamic" ControlToValidate="txtCopyright"></asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Address</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" placeholder="Enter Address"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Address"
                                            Display="Dynamic" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">MailID</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMailID" runat="server" class="form-control" placeholder="Enter MailID"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMailID" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter MailID"
                                            Display="Dynamic" ControlToValidate="txtMailID"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Mobile 1</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMobile1" runat="server" class="form-control" placeholder="Enter Mobile1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobile1" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Mobile1"
                                            Display="Dynamic" ControlToValidate="txtMobile1"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">Mobile 2</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMobile2" runat="server" class="form-control" placeholder="Enter Mobile2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobile2" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Mobile2"
                                            Display="Dynamic" ControlToValidate="txtMobile2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <label class="col-sm-2 control-label">Meta Tags</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMetaTags" runat="server" class="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Meta Tags"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtMetaTags" runat="server" ValidationGroup="validateProfile" ErrorMessage="Enter Meta Tags"
                                            Display="Dynamic" ControlToValidate="txtMetaTags"></asp:RequiredFieldValidator>
                                    </div>

                                    <label class="col-sm-2 control-label">Google Analytics</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtGoogleAnalytics" runat="server" class="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Google Analytics"></asp:TextBox>

                                    </div>
                                </div>

                            </div>
                            <div class="box-footer">
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <div class="send-block text-left">
                                            <asp:Label ID="lblTopAlert" runat="server" Visible="false" CssClass="text-danger"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 text-right">
                                        <div class="send-block">
                                            <asp:Button ID="btnUpdateSetting" runat="server" Text="Update Setting" ValidationGroup="validateProfile" class="btn btn-info" OnClick="btnUpdateSetting_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:HiddenField ID="hdnID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
    <script>
        function showLogo(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgLogo').css('visibility', 'visible');
                    $('#imgLogo').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }

        }

        function showFooterLogo(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgLogoFooter').css('visibility', 'visible');
                    $('#imgLogoFooter').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }

        }
    </script>
</asp:Content>
