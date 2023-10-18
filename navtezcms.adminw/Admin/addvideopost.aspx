<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="addvideopost.aspx.cs" Inherits="navtezcms.adminw.Admin.addvideopost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-horizontal .control-label {
            text-align: left;
            margin-bottom: 10px;
            font-weight: 400;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Add Video
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Add/Edit Video Post</li>
                    </ol>
                </section>
            </div>
        </div>
        <!-- Main content -->
        <section class="content container-fluid">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-8">
                    <div class="box box-primary">

                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">Please select a language  *</label>
                                                    <div class="col-sm-12">
                                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Title * (In Any Language)</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtTitle" ClientIDMode="Static" placeholder="Title" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Slug * In English</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSlug" ClientIDMode="Static" placeholder="Slug" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Keyword meta tag *</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtMetaKeywords" ClientIDMode="Static" placeholder="Keyword meta tag" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Tags *</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtTags" ClientIDMode="Static" placeholder="Enter Tags" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Description</label>

                                                    <!-- RICH TEXT EDITOR -->
                                                    <div class="col-sm-12">
                                                        <textarea rows="10" class="form-control">

                                                        </textarea>
                                                        </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>

                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>



                <div class="col-md-4">
                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:DropDownList ID="ddlVideoType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Local Video" Value="LocalVideo"></asp:ListItem>
                                                    <asp:ListItem Text="Embed Video" Value="EmbedVideo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group" id="divFileUploadVideo">
                                            <label class="col-sm-12 control-label">Video</label>
                                            <div class="col-sm-12">
                                                <asp:FileUpload ID="fupVideo" runat="server" />
                                            </div>
                                        </div>

                                        <div class="form-group" id="divEmbedCode">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtEmbedCode" Columns="10" Rows="8" TextMode="MultiLine" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-12 control-label">Thumbnail / Preview Image *</label>
                                            <div class="col-sm-12">
                                                <img id="imgThumbnailPreview" src="#" height="200" width="200" alt="uploaded Image" style="border-width: 0px; visibility: hidden;" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:FileUpload ID="fupThumbnail_PreviewImage" runat="server" onchange="showpreview(this);" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-12 control-label">Prefered Size: (600x600) or Square Sized Image</label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Add to Feature *</label>
                                            <div class="col-sm-6 control-label">
                                                <asp:RadioButton ID="rbFeatureYes" runat="server" Text="Yes" GroupName="feature" />
                                                <asp:RadioButton ID="rbFeatureNo" runat="server" Text="No" GroupName="feature" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Add to Slider *</label>
                                            <div class="col-sm-6 control-label">
                                                <asp:RadioButton ID="rbSliderYes" runat="server" Text="Yes" GroupName="slider" />
                                                <asp:RadioButton ID="rbSliderNo" runat="server" Text="No" GroupName="slider" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Slider Right Post *</label>
                                            <div class="col-sm-6 control-label">
                                                <asp:RadioButton ID="rbSliderRightYes" runat="server" Text="Yes" GroupName="sliderright" />
                                                <asp:RadioButton ID="rbSliderRightNo" runat="server" Text="No" GroupName="sliderright" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Add to Trending News *</label>
                                            <div class="col-sm-6 control-label">
                                                <asp:RadioButton ID="rbTrendingYes" runat="server" Text="Yes" GroupName="trending" />
                                                <asp:RadioButton ID="rbTrendingNo" runat="server" Text="No" GroupName="trending" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Schedule Post *</label>
                                            <div class="col-sm-6 control-label">
                                                <asp:CheckBox ID="chkISSchedule" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" id="divSchedulePost" style="display: none;">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtScheduleDate" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSaveAsDraft" ClientIDMode="Static" runat="server" CssClass="btn btn-warning" Text="Save as Drafts" OnClick="btnSaveAsDraft_Click" />
                                                <asp:Button ID="btnAddPost" ClientIDMode="Static" runat="server" CssClass="btn btn-success" Text="Add Post" OnClick="btnAddPost_Click" />
                                            </div>
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

    <asp:HiddenField ID="hdnEditID" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //set initial state.
            $('#chkISSchedule').change(function () {
                if (this.checked) {
                    $("#divSchedulePost").show();
                }
                else {
                    $("#divSchedulePost").hide();
                }

            });

            $("#ddlVideoType").change(function () {
                var end = this.value;
                if (end == "LocalVideo") {
                    $("#divFileUploadVideo").show();
                    $("#divEmbedCode").hide();
                }
                else {
                    $("#divFileUploadVideo").hide();
                    $("#divEmbedCode").show();
                }
            });
        });



        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgThumbnailPreview').css('visibility', 'visible');
                    $('#imgThumbnailPreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }

        }


    </script>
</asp:Content>
