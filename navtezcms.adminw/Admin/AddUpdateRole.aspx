<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="AddUpdateRole.aspx.cs" Inherits="navtezcms.adminw.Admin.AddUpdateRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    //parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>
    <style>
        .ContentPlaceHolder1_TreeMenu_1,.ContentPlaceHolder1_TreeMenuCategory_1 {
            margin-left: 4px;
        }

        .control-label {
            margin-bottom: 20px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>
                <asp:Label ID="lblAddEdit" runat="server" Text=""></asp:Label>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="Roles.aspx"><i class="fa fa-list"></i>Role List</a></li>
                <li class="active">Add/Update Role</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content container-fluid">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-danger">
                                <div class="box-header">
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">* Role Name</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtRoleName" runat="server" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtRoleName" runat="server" ControlToValidate="txtRoleName"
                                                    Display="Dynamic" ErrorMessage="Please Enter Role Name" ForeColor="Red" SetFocusOnError="True"
                                                    ValidationGroup="v">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-1"></div>
                                            <div class="col-sm-2">
                                                <label class="control-label">Menu Rights</label><br />

                                                <asp:TreeView runat="server" ID="TreeMenu" OnTreeNodePopulate="TreeMenu_TreeNodePopulate"
                                                    ShowCheckBoxes="All" ImageSet="BulletedList" ShowLines="True" PopulateNodesFromClient="true"
                                                    onclick="OnTreeClick(event)">
                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                    <NodeStyle Font-Size="14px" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="0px" VerticalPadding="2px" />
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px"
                                                        BackColor="#B5B5B5" />
                                                </asp:TreeView>
                                            </div>

                                            <div class="col-sm-1"></div>
                                            <div class="col-sm-2">
                                                <label class="control-label">Category Rights</label><br />
                                                <asp:TreeView runat="server" ID="TreeMenuCategory" OnTreeNodePopulate="TreeMenuCategory_TreeNodePopulate"
                                                    ShowCheckBoxes="All" ImageSet="BulletedList" ShowLines="True" AfterClientCheck="CheckChildNodes();" PopulateNodesFromClient="true"
                                                    onclick="OnTreeClick(event)">
                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                    <NodeStyle Font-Size="14px" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="0px" VerticalPadding="2px" />
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px"
                                                        BackColor="#B5B5B5" />
                                                </asp:TreeView>
                                            </div>

                                            <div class="col-sm-1"></div>
                                            <div class="col-sm-5">
                                                <div class="col-sm-12 text-left">
                                                    <asp:CheckBox ID="chkISActive" runat="server" Text="&nbsp; ISActive" />
                                                    <asp:HiddenField ID="hdnISActive" runat="server" ClientIDMode="Static" Value="1" />

                                                </div>

                                                <div class="col-sm-12 text-left">
                                                    <asp:CheckBox ID="chkIsShowAll" runat="server" Text="&nbsp; IS Show All Data" />
                                                    <asp:HiddenField ID="hdnISShowAll" runat="server" ClientIDMode="Static" Value="1" />

                                                </div>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSaveRole" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right"
                                                    Text="Save" OnClick="btnSaveRole_Click" ValidationGroup="v" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="v" />
                                            </div>

                                        </div>
                                   

                                    </div>

                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveRole" />
                </Triggers>
            </asp:UpdatePanel>

        </section>
        <!-- /.content -->
    </div>
    <asp:HiddenField ID="hdnRoleId" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="server">
</asp:Content>
