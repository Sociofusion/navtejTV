<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="AddContent.aspx.cs" Inherits="navtezcms.adminw.Admin.AddContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .justify-content-center {
            -ms-flex-pack: center !important;
            justify-content: center !important;
        }

        .add-post-area {
            background: #fff;
            border-radius: 4px;
            box-shadow: 0px 0px 15px rgb(0 0 0 / 10%);
            margin-bottom: 30px;
            padding: 30px;
            text-align: center;
            display: block;
        }

            .add-post-area .icon {
                margin-bottom: 20px;
                font-size: 45px;
                color: #000;
            }

            .add-post-area h6 {
                font-weight: 600;
                font-size: 24px;
                line-height: 30px;
                color: #000;
            }

            .add-post-area p {
                margin-bottom: 0px;
                font-size: 16px;
                color: #000;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Add Content
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Add Content</li>
                    </ol>
                </section>
            </div>
        </div>
        <!-- Main content -->
        <section class="content container-fluid">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12">

                    <div class="row justify-content-center">
                        <div class="col-lg-4 col-md-6">
                            <a href="AddPost.aspx" class="add-post-area">
                                <div class="icon">
                                    <i class="fa fa-list-alt"></i>
                                </div>
                                <h6>Post
                                </h6>
                                <p>An article with images</p>
                            </a>
                        </div>

                        <div class="col-lg-4 col-md-6">
                            <a href="addgallery.aspx" class="add-post-area">
                                <div class="icon">
                                    <i class="fa fa-image"></i>
                                </div>
                                <h6>Gallery
                                </h6>
                                <p>A collection of images</p>
                            </a>
                        </div>

                        <div class="col-lg-4 col-md-6">
                            <a href="AddPost.aspx" class="add-post-area">
                                <div class="icon">
                                    <i class="fa fa-video-camera"></i>
                                </div>
                                <h6>Video
                                </h6>
                                <p>Upload post with video</p>
                            </a>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
</asp:Content>
