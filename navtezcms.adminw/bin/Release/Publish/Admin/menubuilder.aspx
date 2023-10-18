<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="menubuilder.aspx.cs" Inherits="navtezcms.adminw.Admin.menubuilder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list-unstyled {
            padding-left: 0;
            list-style: none;
        }

        #page_list li {
            padding: 10px 10px;
            background-color: #f9f9f9;
            border: 1.5px dotted #c6d4e3;
            cursor: move;
            margin-top: 10px;
        }

        .mt20 {
            margin-top: 20px;
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
                    <h1>Menu Builder
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                        <li class="active">Menu Builder</li>
                    </ol>
                </section>
            </div>
        </div>
        <!-- Main content -->
        <section class="content container-fluid">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">

                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-horizontal">
                                        <ul class="list-unstyled ui-sortable" id="page_list">
                                            <asp:Repeater ID="rptCategory" runat="server">
                                                <ItemTemplate>
                                                    <li id='<%#Eval("ID")%>' order='<%#Eval("CategoryOrder")%>' class="ui-sortable-handle"><%#Eval("DefaultTitleToDisplay")%></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </div>


                            <div class="row mt20">
                                <div class="col-lg-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSaveBuilder" ClientIDMode="Static" runat="server" CssClass="btn btn-success" Text="Update Order" OnClick="btnSaveBuilder_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>

            </div>
        </section>
    </div>

    <asp:HiddenField ID="hdnCategoryIDStr" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnOrderStr" ClientIDMode="Static" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
    <script src="http://code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script type="text/javascript">

        (function ($) {
            "use strict";

            $(document).ready(function () {
                $("#page_list").sortable({
                    placeholder: "ui-state-highlight",
                    update: function (event, ui) {
                        var category_id_array = new Array();
                        var order_array = new Array();
                        $('#page_list li').each(function () {
                            category_id_array.push($(this).attr("id"));
                            order_array.push($(this).attr("order"));
                        });

                        $("#hdnCategoryIDStr").val(category_id_array);
                        $("#hdnOrderStr").val(order_array);
                        //alert(category_id_array);
                        //alert(order_array);

                    }
                });
            });


        })(jQuery);

    </script>
</asp:Content>
