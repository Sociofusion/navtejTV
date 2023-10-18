<%@ Page Title="Add / Update Post" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="AddPost.aspx.cs" Inherits="navtezcms.adminw.Admin.AddPost" %>

<%@ Register Src="~/Admin/UC/Asset.ascx" TagPrefix="uc1" TagName="Asset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-horizontal .control-label {
            text-align: left;
            margin-bottom: 10px;
            font-weight: 400;
        }

        .modal-lg {
            width: 95%;
        }

        .mt10 {
            margin-top: 10px;
        }

        .mt20 {
            margin-top: 20px;
        }

        .image-preview {
            width: 100%;
            background-size: auto !important;
            border: 1px solid #fff7f7;
            background-color: #f7f7f7 !important;
            background-size: auto;
            background-position: center 42px !important;
            /*text-align: center;*/
            padding: 10px;
        }

        .gal_box {
            display: inline-block;
        }

            .gal_box img {
                height: 50px;
                margin-left: 5px;
                border-radius: 8px;
            }

        #uploadGalleryPreview {
            margin-top: 10px;
            padding: 10px;
        }


        .embedSocial label {
            margin-right: 5px;
            margin-left: 2px;
        }

        .right {
            float: right;
        }
    </style>


    <!-- jQuery UI CSS -->
    <link href="tokenfield/css/jquery-ui.css" type="text/css" rel="stylesheet" />
    <!-- Bootstrap styling for Typeahead -->
    <link href="tokenfield/css/tokenfield-typeahead.css" type="text/css" rel="stylesheet" />
    <!-- Tokenfield CSS -->
    <link href="tokenfield/css/bootstrap-tokenfield.css" type="text/css" rel="stylesheet" />
    <!-- Docs CSS -->
    <link href="tokenfield/css/pygments-manni.css" type="text/css" rel="stylesheet" />

    <link href="tokenfield/css/docs.css" type="text/css" rel="stylesheet" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Add Post
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Add/Edit Post</li>
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


                                                <div id="lblError" clientidmode="Static" runat="server" style="display: none;" class="alert alert-danger" role="alert">
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">Please select a language  *</label>
                                                    <div class="col-sm-12">
                                                        <asp:DropDownList ID="ddlLanguage" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Title * (In Any Language)</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtTitle" ClientIDMode="Static" placeholder="Title" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                                                        <span id="reqTitle" style="color: red; display: none;"></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Slug * In English</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSlug" ClientIDMode="Static" placeholder="Slug" runat="server" class="form-control"></asp:TextBox>
                                                        <span id="reqSlug" style="color: red; display: none;"></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Keyword meta tag *</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtMetaKeywords" ClientIDMode="Static" placeholder="Keyword meta tag(comma separated)" runat="server" class="form-control"></asp:TextBox>
                                                        <span id="reqMetaKeywords" style="color: red; display: none;"></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Tags *</label>
                                                    <div class="col-sm-12">
                                                        <div class="tokenfield">
                                                            <asp:TextBox ID="txtTags" ClientIDMode="Static" placeholder="Enter Tags" runat="server" class="token-example-field form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnTags" ClientIDMode="Static" runat="server" />
                                                            <span id="reqTags" style="color: red; display: none;"></span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Description <a class="btn btn-default pull-right" onclick="openMedia();"><i class="fa fa-camera-retro"></i>&nbsp;Add Media</a></label>

                                                    <!-- RICH TEXT EDITOR -->
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtDescription" ClientIDMode="Static" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                        <span id="reqDesc" style="color: red; display: none;"></span>
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
                                            <label class="col-sm-12 control-label">Current Featured Image *</label>
                                            <div class="col-sm-12">

                                                <asp:DropDownList ID="ddlFeaturedImageType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Add from media" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Add from device" Value="1"></asp:ListItem>
                                                </asp:DropDownList>

                                                <div class="image-preview mt10">
                                                    <img id="imgFeaturedImage" src="#" alt="uploaded Image" style="border-width: 0px; min-width: 100%; width: 100%; visibility: hidden;" />
                                                    <asp:FileUpload ID="fupThumbnail_PreviewImage" ClientIDMode="Static" runat="server" onchange="showpreview(this);" CssClass="mt20" Style="display: none;" />
                                                    <a id="a_add_from_media" class="btn btn-default btn-sm mt20" onclick="openMedia('feature');"><i class="fa fa-camera-retro"></i>&nbsp;Open Media</a>
                                                    <a id="a_remove_Featured_Image" class="btn btn-default btn-sm mt20 right" onclick="removeFeaturedImage();"><i class="fa fa-trash"></i>&nbsp;Remove</a>
                                                    <label class="control-label mt20">* Prefered Size: (780x438) or rectangle Sized</label>
                                                    <span id="reqFeaturedImage" style="color: red; display: none;"></span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-12 control-label">Gallery Image</label>
                                            <div class="col-sm-12">
                                                <div class="image-preview">
                                                    <div id="galleryPreview"></div>
                                                    <asp:FileUpload ID="fupGalleryImages" runat="server" ClientIDMode="Static"
                                                        AllowMultiple="true" CssClass="mt20" />

                                                    <div id="uploadGalleryPreview"></div>
                                                </div>

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

                                        <div class="form-group" id="selectedCategory">
                                            <label class="col-sm-5 control-label">Selected Category</label>
                                            <div class="col-sm-7 control-label text-right">
                                                <asp:TextBox ID="txtSelectedCategory" ClientIDMode="Static" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">Category *</label>
                                            <div class="col-sm-7 control-label text-right">

                                                <asp:DropDownList ID="ddlCategory" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>

                                                <div id="parentCategory">
                                                </div>
                                                <span id="reqCategory" style="color: red; display: none;"></span>
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

                                            <label class="col-sm-6 control-label">Add to Feature *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISFeature" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISFeature" runat="server" ClientIDMode="Static" Value="1" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Add to Slider *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISSlider" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISSlider" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Slider Left Post *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISSliderLeft" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISSliderLeft" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Slider Right Post *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISSliderRight" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISSliderRight" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Add to Trending News *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISTrending" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISTrending" runat="server" ClientIDMode="Static" Value="1" />
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
                                            <label class="col-sm-6 control-label">Embed Social</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISEmbed" type="button" class="btn btn-lg btn-toggle" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISEmbed" runat="server" ClientIDMode="Static" Value="0" />
                                            </div>
                                        </div>

                                        <div class="form-group embedSocial">
                                            <label class="col-sm-12 control-label">
                                                <asp:RadioButton ID="rbInstagramEmbed" ClientIDMode="Static" runat="server" Text="Instagram" GroupName="Embed" />
                                                <asp:RadioButton ID="rbFacebookEmbed" ClientIDMode="Static" runat="server" Text="Facebook" GroupName="Embed" />
                                                <asp:RadioButton ID="rbTwitterEmbed" ClientIDMode="Static" runat="server" Text="Twitter" GroupName="Embed" />
                                                <asp:RadioButton ID="rbYoutubeEmbed" ClientIDMode="Static" runat="server" Text="Youtube" GroupName="Embed" />
                                            </label>
                                        </div>
                                        <div class="form-group">
                                            <label id="socialLabel" class="col-sm-12 control-label">
                                                Embed post link
                                            </label>

                                            <!-- RICH TEXT EDITOR -->
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtEmbedSocial" ClientIDMode="Static" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
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
                                            <div class="col-sm-6 control-label text-right">
                                                <asp:CheckBox ID="chkISSchedule" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" id="divSchedulePost" style="display: none;">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtScheduleStartDate" placeholder="Schedule Date" autocomplete="off" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-12 mt10">
                                                <asp:TextBox ID="txtScheduleEndDate" placeholder="Schedule End Date" autocomplete="off" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSaveAsDraft" ClientIDMode="Static" runat="server" OnClientClick="return getValidate();" ValidationGroup="vg" CssClass="btn btn-warning" Text="Save as Drafts" OnClick="btnSaveAsDraft_Click" />
                                                <asp:Button ID="btnSavePost" ClientIDMode="Static" runat="server" OnClientClick="return getValidate();" ValidationGroup="vg" CssClass="btn btn-success" Text="Add Post" OnClick="btnSavePost_Click" />
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

    <div id="addAsset" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add Media"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body no-padding">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="form-horizontal">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <uc1:Asset runat="server" ID="Asset" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <a id="btnMediaToPostFeature" class="btn btn-info pull-right" onclick="insertToFeature();"><i class="fa fa-arrow-right"></i>&nbsp;Set Featured Image</a>
                                        <a id="btnMediaToPost" class="btn btn-info pull-right" onclick="insertToPost();"><i class="fa fa-arrow-right"></i>&nbsp;Insert into post</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <asp:HiddenField ID="hdnPostId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCategoryID" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnGalleryCount" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnFeaturedImage" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnAssetID" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnISFeaturedUploaded" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">


    <script type="text/javascript">

        $('#lblError').hide();

        function gotoLocation(id) {
            $('html, body').animate({
                scrollTop: $("#" + id).offset().top - 40
            }, 2000);
        }

        function getValidate() {

            var result = false;
            // Title
            var v_title = $("#txtTitle").val();
            if (v_title == "" || v_title == null) {
                $("#reqTitle").html("*Enter Title");
                $("#reqTitle").show();
                gotoLocation('reqTitle');
                result = false;
                return result;
            }
            else {
                $("#reqTitle").html("");
                $("#reqTitle").hide();
                result = true;

            }
            // Slug
            var v_slug = $("#txtSlug").val();
            if (v_slug == "" || v_slug == null) {
                $("#reqSlug").html("*Enter Slug");
                $("#reqSlug").show();
                gotoLocation('reqSlug');
                result = false;
                return result;
            }
            else {
                $("#reqSlug").html("");
                $("#reqSlug").hide();
                result = true;

            }
            // Meta Tags
            var v_MetaKeywords = $("#txtMetaKeywords").val();
            if (v_MetaKeywords == "" || v_MetaKeywords == null) {
                $("#reqMetaKeywords").html("*Enter Meta Keywords");
                $("#reqMetaKeywords").show();
                gotoLocation('reqMetaKeywords');
                result = false;
                return result;
            }
            else {
                $("#reqMetaKeywords").html("");
                $("#reqMetaKeywords").hide();
                result = true;

            }
            // Tags
            var v_hdnTags = $("#hdnTags").val();
            if (v_hdnTags == "" || v_hdnTags == null) {
                $("#reqTags").html("*Enter Tags");
                $("#reqTags").show();
                gotoLocation('reqTags');
                result = false;
                return result;
            }
            else {
                $("#reqTags").html("");
                $("#reqTags").hide();
                result = true;

            }
            // Description
            var v_txtDescription = CKEDITOR.instances.txtDescription.getData();
            if (v_txtDescription == "" || v_txtDescription == null) {
                $("#reqDesc").html("*Enter Description");
                $("#reqDesc").show();
                gotoLocation('reqDesc');
                result = false;
                return result;
            }
            else {
                $("#reqDesc").html("");
                $("#reqDesc").hide();
                result = true;
            }
            // Featured Image
            var v_hdnISFeaturedUploaded = $("#hdnISFeaturedUploaded").val();
            if (v_hdnISFeaturedUploaded == "" || v_hdnISFeaturedUploaded == null) {
                $("#reqFeaturedImage").html("*Upload Featured Image");
                $("#reqFeaturedImage").show();
                gotoLocation('reqFeaturedImage');
                result = false;
                return result;
            }
            else {
                $("#reqFeaturedImage").html("");
                $("#reqFeaturedImage").hide();
                result = true;
            }

            // Category
            var v_hdnCategoryID = $("#hdnCategoryID").val();
            if (v_hdnCategoryID > 0) {
                $("#reqCategory").html("");
                $("#reqCategory").hide();
                result = true;

            }
            else {
                $("#reqCategory").html("Select Category");
                $("#reqCategory").show();
                gotoLocation('reqCategory');
                result = false;
                return result;
            }




            return result;
        }

        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgFeaturedImage').css('visibility', 'visible');
                    $('#imgFeaturedImage').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                $("#hdnISFeaturedUploaded").val(input.files[0].name)
            }

        }

        $(function () {
            // Multiple images preview in browser
            var imagesPreview = function (input, placeToInsertImagePreview) {

                if (input.files) {
                    var filesAmount = input.files.length;

                    for (i = 0; i < filesAmount; i++) {
                        var reader = new FileReader();

                        reader.onload = function (event) {

                            var $Gal_preview = '<div class="gal_box"><img src=' + event.target.result + ' /></div>';
                            $($Gal_preview).appendTo(placeToInsertImagePreview);

                        }

                        reader.readAsDataURL(input.files[i]);
                    }
                }

            };

            $('#fupGalleryImages').on('change', function () {
                $("#uploadGalleryPreview").html('');
                imagesPreview(this, '#uploadGalleryPreview');
            });
        });




    </script>

    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script src="ckeditor/ckeditor.js"></script>
    <script src="ckeditor/adapters/jquery.js"></script>

    <script type="text/javascript">
        var PostObj;
        var PostId = 0;


        $(document).ready(function () {

            CKEDITOR.replace('txtDescription');
            CKEDITOR.config.height = '50em';

            $('#chkISSchedule').change(function () {
                if (this.checked) {
                    $("#divSchedulePost").show();
                }
                else {
                    $("#divSchedulePost").hide();
                }

            });

            var mode = getUrlVars()["mode"];
            var id = getUrlVars()["id"];

            if (mode == "edit" && id > 0) {
                PostId = id;

                // Bind Post
                getPostById(PostId);
                $("#hdnPostId").val(PostId);
                $("#<%=btnSavePost.ClientID%>").val('Update');
            }
            else {
                PostId = 0;
                PostObj = "";
            }



            $('#txtSlug').focusout(function () {
                fnCanAddPost();
            });

            $('#ddlLanguage').change(function () {
                if (PostObj != undefined && PostObj != "")
                    getLanguageDataByID();
            });
        });


        function fnCanAddPost() {
            var request = { postid: PostId, slug: $("#txtSlug").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddpost";
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


        function getPostById(PostId) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getpostbyid&postid=" + PostId + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        PostObj = JSON.parse(response);
                        console.log(PostObj);
                        $("#txtTitle").val(PostObj.DefaultTitleToDisplay);
                        $("#txtMetaKeywords").val(PostObj.DefaultMetaTagToDisplay);

                        $("#txtSlug").val(PostObj.Slug);
                        $("#txtScheduleStartDate").val(PostObj.ScheduleDate_ddMMyyyy);
                        $("#txtScheduleEndDate").val(PostObj.ScheduleEndDate_ddMMyyyy);
                        $("#txtSelectedCategory").val(PostObj.CategoryName);


                        $("#imgFeaturedImage").attr("src", 'https://storage.googleapis.com/navtejcms/post/' + PostObj.ImageBig);
                        $('#imgFeaturedImage').css('visibility', 'visible');
                        $("#hdnISFeaturedUploaded").val(PostObj.ImageBig);

                        if (PostObj.ISSchedulePost) {
                            $('#chkISSchedule').prop('checked', true);
                            $("#divSchedulePost").show();
                        }
                        else {
                            $('#chkISSchedule').prop('checked', false);
                            $("#divSchedulePost").hide();
                        }


                        // ISFeature
                        if (PostObj.ISFeature == 0)
                            $("#btnISFeature").removeClass("active");
                        else
                            $("#btnISFeature").addClass("active");

                        // ISSlider

                        if (PostObj.ISSlider == 0)
                            $("#btnISSlider").removeClass("active");
                        else
                            $("#btnISSlider").addClass("active");

                        // ISSliderLeft

                        if (PostObj.ISSliderLeft == 0)
                            $("#btnISSliderLeft").removeClass("active");
                        else
                            $("#btnISSliderLeft").addClass("active");

                        // ISSliderRight

                        if (PostObj.ISSliderRight == 0)
                            $("#btnISSliderRight").removeClass("active");
                        else
                            $("#btnISSliderRight").addClass("active");

                        // ISTrending

                        if (PostObj.ISTrending == 0)
                            $("#btnISTrending").removeClass("active");
                        else
                            $("#btnISTrending").addClass("active");



                        $("#hdnISFeature").val(PostObj.ISFeature);
                        $("#hdnISSlider").val(PostObj.ISSlider);
                        $("#hdnISSliderLeft").val(PostObj.ISSliderLeft);
                        $("#hdnISSliderRight").val(PostObj.ISSliderRight);
                        $("#hdnISTrending").val(PostObj.ISTrending);
                        $("#hdnCategoryID").val(PostObj.CategoryID);


                        $("#hdnTags").val(PostObj.Tags);

                        var arr_tags = $("#hdnTags").val().split(",");
                        $.each(arr_tags, function (i) {
                            var $token = '<div class="token"><span class="token-label">' + arr_tags[i] + '</span><a href="javascript:void(0)" class="close" tabindex="-1">&times;</a></div>';
                            $($token).insertBefore(".token-input");
                        });


                        $.each(PostObj.pAssetGallery, function (i) {
                            var $preview = '<div class="gal_box"><img src=' + PostObj.pAssetGallery[i].AssetLiveUrl + ' /></div>';
                            $($preview).appendTo("#galleryPreview");
                        });


                        CKEDITOR.instances.txtDescription.setData(PostObj.DefaultDescriptionToDisplay);

                        $("#txtEmbedSocial").val(PostObj.EmbedSocial);



                        if (PostObj.ISFacebookEmbed) {
                            $('#rbFacebookEmbed').prop('checked', true);
                            $("#socialLabel").text("Embed facebook post link");
                        }
                        else {
                            $('#rbFacebookEmbed').prop('checked', false);
                        }

                        if (PostObj.ISInstagramEmbed) {
                            $('#rbInstagramEmbed').prop('checked', true);
                            $("#socialLabel").text("Embed instagram post link");
                        }
                        else {
                            $('#rbInstagramEmbed').prop('checked', false);
                        }

                        if (PostObj.ISTwitterEmbed) {
                            $('#rbTwitterEmbed').prop('checked', true);
                            $("#socialLabel").text("Embed twitter post link");
                        }
                        else {
                            $('#rbTwitterEmbed').prop('checked', false);
                        }

                        if (PostObj.ISYoutubeEmbed) {
                            $('#rbYoutubeEmbed').prop('checked', true);
                            $("#socialLabel").text("Embed youtube link");
                        }
                        else {
                            $('#rbYoutubeEmbed').prop('checked', false);
                        }


                        // ISEmbed

                        if (PostObj.ISTwitterEmbed == 0 && PostObj.ISInstagramEmbed == 0 && PostObj.ISFacebookEmbed == 0 && PostObj.ISYoutubeEmbed == 0) {
                            $("#btnISEmbed").removeClass("active");
                            $("#hdnISEmbed").val(0);
                        }
                        else {
                            $("#btnISEmbed").addClass("active");
                            $("#hdnISEmbed").val(1);
                        }

                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }


        $(document).on('click', 'a.close', function () {

            var removeItem = $(this).parents('.token').children(".token-label").text();
            var arr_tags = $("#hdnTags").val().split(",");

            arr_tags = $.grep(arr_tags, function (value) {
                return value != removeItem;
            });

            $("#hdnTags").val(arr_tags.join(","));

            $(this).parents('.token').remove();

        });


        function getLanguageDataByID() {

            var languageID = $("#ddlLanguage").val();
            // Title
            $.each(PostObj.TitleData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtTitle").val(v.Translation);
                    return;
                }
            });
            // Description
            $.each(PostObj.DescriptionData, function (i, v) {
                if (v.LanguageID == languageID) {
                    CKEDITOR.instances.txtDescription.setData(v.Translation);
                    return;
                }
            });

            // Meta Tags
            $.each(PostObj.MetaTagData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtMetaKeywords").val(v.Translation);
                    return;
                }
            });
        }

        function clearAll() {

            $('#lblError').hide();
            $("input:text").val("");
            CKEDITOR.instances.txtDescription.setData("");
            $("#hdnPostId").val('');
            $("#imgPostImage").attr("src", "");
            $('#lblHeader').html("Add New Post");
            $("#<%=btnSavePost.ClientID%>").val('Save');
        }

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }



        $("#ddlCategory").change(function () {
            var selectedCategoryID = this.value;
            $("#hdnCategoryID").val(selectedCategoryID);

            if (selectedCategoryID != 0) {
                getSubcategoryById(selectedCategoryID, 0);
            }
            else {
                $("#parentCategory").html('');
            }

        });

        $(document).on('change', ".subcategory", function () {
            var selectedCategoryID = this.value;
            $("#hdnCategoryID").val(selectedCategoryID);

            if (selectedCategoryID != 0) {
                getSubcategoryById(selectedCategoryID, 1);
            }
            else {
                $("#parentCategory").html('');
            }
        });


        function getSubcategoryById(selectedCategoryID, isagain) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getsubcategorybyid&catid=" + selectedCategoryID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        var CategoryObj = JSON.parse(response);
                        if (isagain == 0) {
                            $("#parentCategory").html('');
                        }

                        if (CategoryObj.length > 0) {
                            var ddlCustomers = $("<select class='form-control subcategory mt10' />");

                            var option = $("<option />");
                            option.html('-- Select Subcategory --');
                            option.val(0);
                            ddlCustomers.append(option);

                            for (var i = 0; i < CategoryObj.length; i++) {

                                var option = $("<option />");

                                option.html(CategoryObj[i].DefaultTitleToDisplay);
                                option.val(CategoryObj[i].ID);
                                ddlCustomers.append(option);
                            }


                            var dvContainer = $("#parentCategory");
                            var div = $("<div />");
                            div.append(ddlCustomers);

                            //Add the DIV to the container DIV.
                            dvContainer.append(div);
                        }
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }


    </script>



    <script>

        // Toggle buttons

        $('#btnISFeature').on('click', function () {
            var ISFeature = $("#hdnISFeature").val();
            if (ISFeature == 1) {
                $("#hdnISFeature").val(0);
            }
            else {
                $("#hdnISFeature").val(1);
            }
        });

        $('#btnISSlider').on('click', function () {
            var ISSlider = $("#hdnISSlider").val();
            if (ISSlider == 1) {
                $("#hdnISSlider").val(0);
            }
            else {
                $("#hdnISSlider").val(1);
            }
        });

        $('#btnISSliderLeft').on('click', function () {
            var ISSliderLeft = $("#hdnISSliderLeft").val();
            if (ISSliderLeft == 1) {
                $("#hdnISSliderLeft").val(0);
            }
            else {
                $("#hdnISSliderLeft").val(1);
            }
        });

        $('#btnISSliderRight').on('click', function () {
            var ISSliderRight = $("#hdnISSliderRight").val();
            if (ISSliderRight == 1) {
                $("#hdnISSliderRight").val(0);
            }
            else {
                $("#hdnISSliderRight").val(1);
            }
        });

        $('#btnISTrending').on('click', function () {
            var ISTrending = $("#hdnISTrending").val();
            if (ISTrending == 1) {
                $("#hdnISTrending").val(0);
            }
            else {
                $("#hdnISTrending").val(1);
            }
        });

        $('#btnISEmbed').on('click', function () {
            var ISEmbed = $("#hdnISEmbed").val();
            if (ISEmbed == 0) {
                $("#hdnISEmbed").val(1);
                $("#socialLabel").text("Embed instagram post link");
                $("#rbInstagramEmbed").prop("checked", true);
                $("#rbFacebookEmbed").prop("checked", false);
                $("#rbTwitterEmbed").prop("checked", false);
                $("#rbYoutubeEmbed").prop("checked", false);
            }
            else {
                $("#hdnISEmbed").val(0);
                $("#rbInstagramEmbed").prop("checked", false);
                $("#rbFacebookEmbed").prop("checked", false);
                $("#rbTwitterEmbed").prop("checked", false);
                $("#rbYoutubeEmbed").prop("checked", false);
            }
        });

    </script>

    <script type="text/javascript" src="tokenfield/js/bootstrap-tokenfield.js" charset="UTF-8"></script>
    <script type="text/javascript" src="tokenfield/js/scrollspy.js" charset="UTF-8"></script>
    <script type="text/javascript" src="tokenfield/js/affix.js" charset="UTF-8"></script>
    <script type="text/javascript" src="tokenfield/js/docs.min.js" charset="UTF-8"></script>

    <script>

        $('#txtScheduleStartDate').datetimepicker({
            format: 'd.m.Y H:i',
            lang: 'ru'
        });

        $('#txtScheduleEndDate').datetimepicker({
            format: 'd.m.Y H:i',
            lang: 'ru'
        });


        //$('#txtScheduleStartDate, #txtScheduleEndDate').datetimepicker();


        function convertToSlug(Text) {
            return Text.toLowerCase()
                .replace(/ /g, '-')
                .replace(/[^\w-]+/g, '');
        }

        $("#txtTitle").keyup(function () {
            var mode = getUrlVars()["mode"];
            if (mode != 'edit') {
                var Text = $(this).val();
                Text = Text.toLowerCase();
                Text = Text.replace(/[^a-zA-Z0-9]+/g, '-');
                $("#txtSlug").val(Text);
            }
        });

        $("#rbInstagramEmbed").change(function () {
            $("#socialLabel").text("Embed instagram post link");
        });

        $("#rbFacebookEmbed").change(function () {
            $("#socialLabel").text("Embed facebook post link");
        });

        $("#rbTwitterEmbed").change(function () {
            $("#socialLabel").text("Embed twitter post link");
        });

        $("#rbYoutubeEmbed").change(function () {
            $("#socialLabel").text("Embed youtube link");
        });


    </script>

    <script src="scripts/asset.js"></script>
    <script>
        function openMedia(type) {
            if (type == 'feature') {
                $("#btnMediaToPostFeature").show();
                $("#btnMediaToPost").hide();
            }
            else {
                $("#btnMediaToPostFeature").hide();
                $("#btnMediaToPost").show();
            }

            $("#rightSideBox").hide();
            $("#addAsset").modal("show");
        }
        function insertToPost() {
            var txtDescriptionData = CKEDITOR.instances.txtDescription.getData();
            var url = $("#hdnImageURL").val();
            var contentType = $("#hdnContentType").val();
            if (contentType == 'audio/video') {
                var videoStr = "<iframe class='IframeVideo' src=" + url + " frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share' allowfullscreen></iframe>";
                txtDescriptionData += videoStr;
            }
            else {
                var img = "<img src=" + url + " style='height:auto; width:100%' />";
                txtDescriptionData += img;
            }
            CKEDITOR.instances.txtDescription.setData(txtDescriptionData);
            $("#addAsset").modal("hide");
        }
        function insertToFeature() {
            var src = $("#hdnImageSRC").val();
            var url = $("#hdnImageURL").val();
            $('#imgFeaturedImage').css('visibility', 'visible');
            $('#imgFeaturedImage').attr('src', url);

            var AssetID = $("#hdnGallery").val();
            $("#hdnAssetID").val(AssetID);
            $("#hdnFeaturedImage").val(src);
            $("#hdnISFeaturedUploaded").val(src);
            $("#addAsset").modal("hide");

        }
        $("#ddlFeaturedImageType").change(function () {
            $('#imgFeaturedImage').css('visibility', 'hidden');
            $('#imgFeaturedImage').attr('src', '');
            var featuredImageType = this.value;
            if (featuredImageType == 0) {
                $("#a_add_from_media").show();
                $("#a_remove_Featured_Image").show();
                $("#fupThumbnail_PreviewImage").hide();
            }
            else {
                $("#hdnFeaturedImage").val('');
                $("#a_add_from_media").hide();
                $("#a_remove_Featured_Image").hide();
                $("#fupThumbnail_PreviewImage").show();
            }
        });

        function removeFeaturedImage() {
            $("#hdnISFeaturedUploaded").val('');
            $('#imgFeaturedImage').css('visibility', 'hidden');
            $('#imgFeaturedImage').attr('src', '');
        }
    </script>

</asp:Content>
