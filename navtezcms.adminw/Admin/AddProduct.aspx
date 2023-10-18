<%@ Page Title="Add / Update Product" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="AddProduct.aspx.cs" Inherits="navtezcms.adminw.Admin.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-horizontal .control-label {
            text-align: left;
            margin-bottom: 10px;
            font-weight: 400;
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
            text-align: center;
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="row">
            <div class="col-md-12">
                <section class="content-header">
                    <h1>Add Product
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Add/Edit Product</li>
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
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Slug * In English</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSlug" ClientIDMode="Static" placeholder="Slug" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label class="col-sm-12 control-label">
                                                                Price</label>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtPrice" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="10" TextMode="Number" AutoPostBack="true" OnTextChanged="txtOff_TextChanged"
                                                                    EnableTheming="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtPrice" runat="server" ControlToValidate="txtPrice"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Price " SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <label class="col-sm-12 control-label">
                                                                Offer(%)</label>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtOff" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="2" Text="0" TextMode="Number" AutoPostBack="true" OnTextChanged="txtOff_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">

                                                            <label class="col-sm-12 control-label">
                                                                Effective Price</label>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtEffectivePrice" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="10"
                                                                    Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-12 control-label">
                                                                Shipping</label>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtShipping" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="5" Text="0" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Return Policy</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtReturnPolicy" ClientIDMode="Static" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-12 control-label">
                                                        Description</label>
                                                    <!-- RICH TEXT EDITOR -->
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtDescription" ClientIDMode="Static" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <div class="image-preview">
                                                    <img id="imgFeaturedImage" src="#" height="100" alt="uploaded Image" style="border-width: 0px; visibility: hidden;" />
                                                    <asp:FileUpload ID="fupThumbnail_PreviewImage" runat="server" onchange="showpreview(this);" CssClass="mt20" />
                                                    <label class="control-label">* Prefered Size: (600x600) or Square Sized Image</label>
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
                                            <label class="col-sm-5 control-label">Category</label>
                                            <div class="col-sm-7 control-label text-right">


                                                <asp:DropDownList ID="ddlCategory" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>

                                                <div id="parentCategory">
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
                                        <div class="form-group">

                                            <label class="col-sm-6 control-label">COD Available *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISFeature" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISCOD" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">IS Active *</label>
                                            <div class="col-sm-6 control-label text-right">
                                                <button id="btnISActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISActive" runat="server" ClientIDMode="Static" Value="1" />
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
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSaveProduct" ClientIDMode="Static" runat="server" CssClass="btn btn-success" Text="Add Product" OnClick="btnSaveProduct_Click" />
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

    <asp:HiddenField ID="hdnProductId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCategoryID" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnGalleryCount" ClientIDMode="Static" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
    <script type="text/javascript">

        $(function () {
            
        });


        //////////////////////////////////


        $('#lblError').hide();

        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgFeaturedImage').css('visibility', 'visible');
                    $('#imgFeaturedImage').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
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
        var ProductObj;
        var ProductId = 0;

        $(document).ready(function () {


            CKEDITOR.replace('txtDescription', {
                // Define the toolbar groups as it is a more accessible solution.
                toolbarGroups: [
                    {
                        "name": "basicstyles",
                        "groups": ["basicstyles"]
                    },
                    {
                        "name": "paragraph",
                        "groups": ["list", "blocks"]
                    },
                    {
                        "name": "document",
                        "groups": ["mode"]
                    },
                    {
                        "name": "insert",
                        "groups": ["insert"]
                    },
                    {
                        "name": "styles",
                        "groups": ["styles"]
                    }
                ],
                // Remove the redundant buttons from toolbar groups defined above.
                removeButtons: 'Underline,Strike,Subscript,Superscript,Anchor,Styles,Specialchar,PasteFromWord'
            });
            CKEDITOR.config.height = '25em';


            //////////////////////////////


            CKEDITOR.replace('txtReturnPolicy', {
                // Define the toolbar groups as it is a more accessible solution.
                toolbarGroups: [
                    {
                        "name": "basicstyles",
                        "groups": ["basicstyles"]
                    },
                    {
                        "name": "paragraph",
                        "groups": ["list", "blocks"]
                    },
                    {
                        "name": "document",
                        "groups": ["mode"]
                    },
                    {
                        "name": "insert",
                        "groups": ["insert"]
                    },
                    {
                        "name": "styles",
                        "groups": ["styles"]
                    }
                ],
                // Remove the redundant buttons from toolbar groups defined above.
                removeButtons: 'Underline,Strike,Subscript,Superscript,Anchor,Styles,Specialchar,PasteFromWord'
            });
            CKEDITOR.config.height = '25em';

            var mode = getUrlVars()["mode"];
            var id = getUrlVars()["id"];

            if (mode == "edit" && id > 0) {
                ProductId = id;

                // Bind Product
                getProductById(ProductId);
                $("#hdnProductId").val(ProductId);
                $("#<%=btnSaveProduct.ClientID%>").val('Update');
            }
            else {
                ProductId = 0;
                ProductObj = "";
            }

            $('#txtSlug').focusout(function () {
                fnCanAddProduct();
            });

            $('#ddlLanguage').change(function () {
                if (ProductObj != undefined && ProductObj != "")
                    getLanguageDataByID();
            });
        });

        function fnCanAddProduct() {
            var request = { productid: ProductId, slug: $("#txtSlug").val() };
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=canaddproduct";
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

        function getProductById(ProductId) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getproductbyid&productid=" + ProductId + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        ProductObj = JSON.parse(response);
                        console.log(ProductObj);
                        $("#txtTitle").val(ProductObj.DefaultTitleToDisplay);
                        $("#txtMetaKeywords").val(ProductObj.DefaultMetaTagToDisplay);
                        $("#txtSlug").val(ProductObj.Slug);

                        $("#txtPrice").val(ProductObj.Price);
                        $("#txtOff").val(ProductObj.Off);
                        $("#txtEffectivePrice").val(ProductObj.EffectivePrice);
                        $("#txtShipping").val(ProductObj.ShippingCharge);

                        $("#txtSelectedCategory").val(ProductObj.CategoryName);

                        $("#imgFeaturedImage").attr("src", ProductObj.AssetFullUrl);
                        $('#imgFeaturedImage').css('visibility', 'visible');

                        // ISCOD
                        if (ProductObj.ISCOD == 0)
                            $("#btnISCOD").removeClass("active");
                        else
                            $("#btnISCOD").addClass("active");

                        // ISActive

                        if (ProductObj.ISActive == 0)
                            $("#btnISActive").removeClass("active");
                        else
                            $("#btnISActive").addClass("active");


                        $("#hdnISCOD").val(ProductObj.ISCOD);
                        $("#hdnISActive").val(ProductObj.ISActive);
                        $("#hdnCategoryID").val(ProductObj.CategoryID);


                        $.each(ProductObj.pAssetGallery, function (i) {
                            var $preview = '<div class="gal_box"><img src=' + ProductObj.pAssetGallery[i].AssetLiveUrl + ' /></div>';
                            $($preview).appendTo("#galleryPreview");
                        });
                        
                        CKEDITOR.instances.txtDescription.setData(ProductObj.DefaultDescriptionToDisplay);
                        CKEDITOR.instances.txtReturnPolicy.setData(ProductObj.DefaultReturnPolicyToDisplay);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function getLanguageDataByID() {

            var languageID = $("#ddlLanguage").val();
            // Title
            $.each(ProductObj.TitleData, function (i, v) {
                if (v.LanguageID == languageID) {
                    $("#txtTitle").val(v.Translation);
                    return;
                }
            });
            // Description
            $.each(ProductObj.DescriptionData, function (i, v) {
                if (v.LanguageID == languageID) {
                    CKEDITOR.instances.txtDescription.setData(v.Translation);
                    return;
                }
            });

            // ReturnPolicy
            $.each(ProductObj.ReturnPolicyData, function (i, v) {
                if (v.LanguageID == languageID) {
                    CKEDITOR.instances.txtReturnPolicy.setData(v.Translation);
                    return;
                }
            });
        }

        function clearAll() {

            $('#lblError').hide();
            $("input:text").val("");
            CKEDITOR.instances.txtDescription.setData("");
            CKEDITOR.instances.txtReturnPolicy.setData("");
            $("#hdnProductId").val('');
            $("#imgProductImage").attr("src", "");
            $('#lblHeader').html("Add New Product");
            $("#<%=btnSaveProduct.ClientID%>").val('Save');
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

        // Toggle buttons

        $('#btnISCOD').on('click', function () {
            var ISCOD = $("#hdnISCOD").val();
            if (ISCOD == 1) {
                $("#hdnISCOD").val(0);
            }
            else {
                $("#hdnISCOD").val(1);
            }
        });

        $('#btnISActive').on('click', function () {
            var ISActive = $("#hdnISActive").val();
            if (ISActive == 1) {
                $("#hdnISActive").val(0);
            }
            else {
                $("#hdnISActive").val(1);
            }
        });

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

    </script>

</asp:Content>
