<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="addgallery.aspx.cs" Inherits="navtezcms.adminw.Admin.addgallery" %>

<%@ Register Src="~/Admin/UC/Asset.ascx" TagPrefix="uc1" TagName="Asset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="https://unpkg.com/dropzone@5/dist/min/dropzone.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Add Image Gallery
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Image Gallery</li>
                    </ol>
                </section>
            </div>
        </div>
        <!-- Main content -->
        <uc1:Asset runat="server" ID="Asset" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
    <script src="scripts/asset.js"></script>
    <script>
        function openMedia() {
            $("#rightSideBox").hide();
            $("#addAsset").modal("show");
        }
        function insertToPost() {
            var txtDescriptionData = CKEDITOR.instances.txtDescription.getData();
            
            var url = $("#hdnImageURL").val();
            var img = "<img src=" + url + " style='height:auto; width:100%' />";
            txtDescriptionData += img;
            CKEDITOR.instances.txtDescription.setData(txtDescriptionData);
            $("#addAsset").modal("hide");
        }
    </script>
    
</asp:Content>
