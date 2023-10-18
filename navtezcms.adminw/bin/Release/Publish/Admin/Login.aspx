<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="navtezcms.adminw.Admin.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Navtej TV | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="plugins/iCheck/square/blue.css" />
    <link rel="short icon" type="img/png" href="images/favicon.png" />
    <style>
        a {
            color: #3c8dbc;
        }
    </style>

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">

        <div class="container">
            <div class="row mt-10">
                <div class="col-lg-4 col-lg-offset-4">
                    <div class="login-box justify-content-center">
                        <div class="login-logo">
                        </div>
                        <!-- /.login-logo -->
                        <div class="login-box-body">

                            <div class="header-area">
                                <h4 class="title">Login Now</h4>
                                <p class="text">Welcome back, please sign in below</p>
                            </div>


                            <div class="adminFormInput form-group has-feedback">
                                <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Type Email Address"></asp:TextBox>
                                <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                            </div>
                            <div class="adminFormInput form-group has-feedback">
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Type Password" TextMode="Password"></asp:TextBox>
                                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div id="alert" runat="server" class="alert alert-danger" role="alert" visible="false">
                                        <asp:Label ID="lblAlert" runat="server" CssClass=""></asp:Label>
                                    </div>
                                </div>

                                <div class="col-xs-12">
                                    <div class="checkbox icheck text-left">
                                        <label>
                                            <asp:CheckBox ID="chkRememberMe" runat="server" />
                                            Remember Password
                                        </label>
                                    </div>
                                </div>
                                <!-- /.col -->
                                <div class="col-xs-12">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-danger btn-block btn-flat" OnClick="btnLogin_Click" />
                                </div>
                                <!-- /.col -->
                            </div>


                        </div>
                        <!-- /.login-box-body -->
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- jQuery 3 -->
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' /* optional */
            });
        });
    </script>
</body>
</html>
