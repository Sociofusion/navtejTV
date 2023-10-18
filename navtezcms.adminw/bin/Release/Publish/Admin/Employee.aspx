<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/mainsite.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="navtezcms.adminw.Admin.Employee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />

    <link href="bower_components/bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Employee
            </h1>

            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx"><i class="fa fa-home"></i>Dashboard</a></li>
                <li class="active">Employee</li>
            </ol>

            <div class="text-right">
                <a class="btn btn-info btn-sm" onclick="openAddEmployee();"><i class="fa fa-plus"></i>&nbsp;Add New Employee</a>
            </div>
        </section>

        <!-- Main content -->
        <section class="content container-fluid">

            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table id="tblEmployee" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Role</th>
                                                <th>ISActive</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptEmployee" runat="server" OnItemCommand="rptEmployee_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%#(Eval("Name")) %></td>
                                                        <td><%#(Eval("Email")) %></td>
                                                        <td><%# (Eval("RoleName"))%></td>
                                                       
                                                        <td><span class='<%# Convert.ToBoolean(Eval("ISActive")) == false ?"btn btn-sm btn-detail btn-warning" : "btn btn-sm btn-detail btn-success" %>'
                                                            style="border-radius: 15px;"><%# Convert.ToBoolean(Eval("ISActive")) == false ?"InActive" : "Active" %></span></td>
                                                        <td>
                                                            <a data-toggle="tooltip" onclick="editEmployee('<%#Eval("ID")%>');" data-original-title="Edit Employee" class="btn btn-sm btn-info btn-detail"><i class="fa fa-pencil"></i>&nbsp;Edit</a>
                                                            <asp:LinkButton CommandName="deleteEmployee" CommandArgument='<%#Eval("ID")%>' data-toggle="tooltip" title="Delete Employee"
                                                                runat="server" ID="btnDeleteEmployee" Text="<i class='fa fa-remove'></i>&nbsp; Delete"
                                                                CssClass="btn btn-sm btn-danger btn-detail" OnClientClick="return confirm('Do you want to delete this Employee?');" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rptEmployee" EventName="ItemCommand" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>

            </div>

        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <div id="addEmployee" class="modal fade">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="bootbox-close-button close" data-dismiss="modal" aria-hidden="true">×</button><h4 class="modal-title">
                        <asp:Label ID="lblHeader" ClientIDMode="Static" runat="server" Text="Add New Employee"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="form-horizontal">
                                    <div class="box-body">

                                        <div id="lblError" clientidmode="Static" runat="server" style="display: none;" class="alert alert-danger" role="alert">
                                        </div>

                                         <div class="form-group">
                                            <label class="col-sm-3 control-label">Role *</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlRole" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Employee Name *</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Email ID</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtEmailID" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                         <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                               Password</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                     

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">IS Active</label>
                                            <div class="col-sm-9">
                                                <button id="btnISActive" type="button" class="btn btn-lg btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                                                    <div class="handle"></div>
                                                </button>
                                                <asp:HiddenField ID="hdnISActive" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <a onclick="closeAddEmployee();" class="btn btn-default">Cancel</a>
                                        <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" CssClass="btn btn-info pull-right" Text="Save" OnClick="btnSaveEmployee_Click" />
                                    </div>
                                    <!-- /.box-footer -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hdnEmployeeId" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpScripts" runat="Server">
    <!-- DataTables -->
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
   

    <script type="text/javascript">
        var EmployeeObj;
        var EmployeeId = 0;
        $(document).ready(function () {
            
            $('#tblEmployee').DataTable({
                "scrollX": true
            });

            $('#btnISActive').on('click', function () {
                var IsShowHome = $("#hdnISActive").val();
                if (IsShowHome == 1) {
                    $("#hdnISActive").val(0);
                }
                else {
                    $("#hdnISActive").val(1);
                }
            });
            
        });
        

        function openAddEmployee() {
            clearAll();
            EmployeeId = 0;
            $('#addEmployee').modal('show');
        }
        function closeAddEmployee() {
            $('#addEmployee').modal('hide');
        }



        $('#addEmployee').on('show.bs.modal', function () {
            if (EmployeeId != "") {
                getEmployeeById(EmployeeId);
                $("#hdnEmployeeId").val(EmployeeId);
                $('#lblHeader').html("Update Employee");
                $("#<%=btnSaveEmployee.ClientID%>").val('Update');
            }
        });

        $('#addEmployee').on('hide.bs.modal', function () {
            EmployeeId = "";
            EmployeeObj = "";
        })

        function editEmployee(EmployeeID) {
            clearAll();
            EmployeeId = EmployeeID;
            $('#addEmployee').modal('show');
        }
        
        function getEmployeeById(EmployeeID) {
            var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getemployeebyid&empid=" + EmployeeID + "";
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                success: function (response) {
                    if (response != "" && response != 'undefined') {
                        EmployeeObj = JSON.parse(response);
                        console.log(EmployeeObj);
                        $("#txtName").val(EmployeeObj.Name);
                        $("#txtEmailID").val(EmployeeObj.Email);
                        $("#txtPassword").val(EmployeeObj.Password);
                        $("#ddlRole").val(EmployeeObj.RoleID);

                        $("#hdnISActive").val(EmployeeObj.ISActive);
                       
                        if (EmployeeObj.ISActive == 0)
                            $("#btnISActive").removeClass("active");
                        else
                            $("#btnISActive").addClass("active");

                    
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        function clearAll() {
            $("#btnISActive").removeClass("active").addClass("active");
            $("#ddlRole").val(0);
            $('#lblError').hide();
            $("input:text").val("");
            $("#hdnEmployeeId").val('');
            $('#lblHeader').html("Add New Employee");
            $("#<%=btnSaveEmployee.ClientID%>").val('Save');
        }
    </script>

    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#tblEmployee').DataTable({
                "scrollX": true
            });
        });
    </script>
</asp:Content>


