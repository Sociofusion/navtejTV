<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Asset.ascx.cs" Inherits="navtezcms.adminw.Admin.UC.Asset" %>

<style type="text/css">
    .form-horizontal .control-label {
        text-align: left;
        margin-bottom: 10px;
        font-weight: 400;
    }

    .borderLeft {
        border-left: 1px #ddd solid;
        margin-top: 40px;
    }

    .pad-l-r-0 {
        padding-left: 0px !important;
        padding-right: 0px !important;
    }

    .imgPost {
        height: 100%;
        max-width: 100%;
        object-fit: cover;
    }

    .mt10 {
        margin-top: 10px;
    }

    .mt20 {
        margin-top: 20px;
    }

    .margin65 {
        margin: 70px 50px 50px 145px;
    }

    .margin25 {
        margin: 100px 0px;
    }



    .gal_box {
        display: inline-block;
    }

        .gal_box img {
            height: 50px;
            margin-left: 5px;
            border-radius: 8px;
        }

    .borderBoxUpload {
        border: 1px #ddd solid;
        padding: 5px;
        border-radius: 5px;
        min-height: 145px;
    }

    .borderBox {
        border: 1px #d9d9d9 solid;
        width: 125px;
        height: 125px;
        display: inline-flex;
        margin: 0px 10px 12px 0px;
    }

    .borderBoxVideo {
        border: 1px #d9d9d9 solid;
        width: 260px;
        height: 150px;
        display: inline-flex;
        margin: 0px 10px 12px 0px;
    }

    .attachment {
        position: relative;
        float: left;
        margin: 0;
        color: #3c434a;
        cursor: pointer;
        list-style: none;
        text-align: center;
        -webkit-user-select: none;
        user-select: none;
        box-sizing: border-box;
    }

    .bigGalleryImage {
        max-width: 100%;
        max-height: 500px;
    }

    .galleryBox {
        margin-top: 10px;
    }

    .nav-tabs > li > a {
        color: #000;
    }

    .dropzone {
        border: 0px !important;
    }

    .p-l-10 {
        padding-left: 10px !important;
    }

    .min350 {
        min-height: 350px;
    }

    .VideoClass {
        width: 260px;
        height: 150px;
    }

    .VideoClassSide {
        width: 100%;
        height: auto;
    }
</style>

<link rel="stylesheet" href="https://unpkg.com/dropzone@5/dist/min/dropzone.min.css" type="text/css" />


<section class="content container-fluid">
    <!-- Small boxes (Stat box) -->
    <div class="row">
        <div class="col-md-9 min350 pad-l-r-0">

            <div class="card card-primary card-outline card-tabs">
                <div class="card-header p-0 pt-1 border-bottom-0">
                    <ul class="nav nav-tabs" id="custom-tabs-three-tab" role="tablist">
                        <li class="nav-item active">
                            <a class="nav-link" id="tab-upload-files-tab" data-toggle="pill" href="#tab-upload-files" role="tab" aria-controls="tab-upload-files" aria-selected="false">Upload Files</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="tab-media-library-tab" data-toggle="pill" href="#tab-media-library" role="tab" aria-controls="tab-media-library" aria-selected="false">Media Library</a>
                        </li>
                    </ul>
                </div>
                <div class="card-body">
                    <div class="tab-content" id="custom-tabs-three-tabContent">
                        <div class="tab-pane active fade in" id="tab-upload-files" role="tabpanel" aria-labelledby="tab-upload-files-tab">
                            <div id="dZUpload" class="dropzone">
                                <div class="dz-default dz-message">
                                    <img src="images/upload.png" style="width: 80px;" alt="Upload files" />
                                    <p>
                                        Drag and drop files here (Max file size 20 MB)
                                    </p>
                                    <p>or</p>
                                    <p>Click to upload.</p>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab-media-library" role="tabpanel" aria-labelledby="tab-media-library-tab">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="galleryBox">
                                        <div id="galleryItems">
                                        </div>

                                        <div id="galleryVideoItems">
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
        <div class="col-md-3 min350 p-l-10 borderLeft pad-l-r-0">
            <div id="rightSideBox" style="display: none;">
                <img id="imgGallery" src="/" class="bigGalleryImage" />
                <video id="videoGallery" class="VideoClassSide" src="" controls></video>
                <input type="text" value="" id="url" class="form-control mt10" />
                <span id="copyAlert" class="text-success"></span>

                <div class="mt10">
                    <a onclick="copy();" class="btn btn-info"><i class="fa fa-copy"></i>&nbsp;Copy URL to clipboard</a>
                    <a onclick="confirmDelete();" class="btn btn-danger pull-right"><i class="fa fa-trash-o"></i>&nbsp;Delete</a>
                </div>
            </div>
        </div>
    </div>
</section>



<asp:HiddenField ID="hdnGallery" ClientIDMode="Static" runat="server" Value="0" />
<asp:HiddenField ID="hdnImageURL" ClientIDMode="Static" runat="server" Value="" />
<asp:HiddenField ID="hdnEmployeeID" ClientIDMode="Static" runat="server" Value="0" />
<asp:HiddenField ID="hdnImageSRC" ClientIDMode="Static" runat="server" Value="" />
<asp:HiddenField ID="hdnContentType" ClientIDMode="Static" runat="server" Value="" />
