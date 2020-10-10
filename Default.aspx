<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Login | PCMC </title>
    <!--Jquery 3.0-->
    <script src="Dashboard/bower_components/jquery/dist/jquery.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="Dashboard/bower_components/bootstrap/dist/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="Dashboard/bower_components/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="Dashboard/bower_components/Ionicons/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="Dashboard/dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="Dashboard/plugins/iCheck/square/blue.css">


    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">


    <script type="text/javascript">
        var isSubmitted = false;
        function GetName() {
            var newDate = new Date();
            if (!isSubmitted) {
                var username = document.getElementById('<%= txtUserName.ClientID%>').value;
                document.getElementById('<%= txtUserName.ClientID %>').value = data(username + "&" + newDate.getTime());
                var password = document.getElementById('<%= txtPassword.ClientID%>').value;
                document.getElementById('<%= txtPassword.ClientID %>').value = data(password + "*" + newDate.getTime());

                isSubmitted = true;

                return true;
            }
            else { return false; }
        }
    </script>

    <script type="text/javascript">
        function data(s) {
            var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9+/=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/rn/g, "n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } }
            var encodedString = Base64.encode(s);
            return encodedString;
        }

        $(document).ready(function () {
            //fa fa-unlock

            $('#' + '<%= pwddiv.ClientID %>').click(function () {
                if ($(this).siblings('#txtPassword').attr('type') == 'text') {
                    $(this).siblings('#txtPassword').attr('type', 'password');
                    $(this).find('span').removeClass("fa fa-unlock").addClass("glyphicon glyphicon-lock").delay(1000);
                }
                else {
                    $(this).siblings('#txtPassword').attr('type', 'text');
                    $(this).find('span').removeClass("glyphicon glyphicon-lock").addClass("fa fa-unlock").delay(1000);

                }
            });

           <%-- $('#' + '<%= pwddiv.ClientID %>').click(function () {
                
               
            });--%>
        });
    </script>




</head>
<body class="hold-transition login-page" style="background-color: white;">

    <%--<div class="row">
        <div class="col-md-12">
            <div></div>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="col-md-4 col-sm-1 col-xs-1">
            </div>
            <div class="col-md-4 col-sm-10 col-xs-10" style="">
                <div class="login-box" style="padding-top: 5rem; margin: auto; text-align: center">
                    <img src="Dashboard/images/logo.png" style="width: 180px; height: 180px" />
                    <div class="login-logo">
                        <a><b style="margin-left: -10px;">PCMC</b></a>
                    </div>
                    <!-- /.login-logo -->
                    <div class="login-box-body" style="background-color: #ffffab;">
                        <p class="login-box-msg">Sign in to start your session</p>
                        <form runat="server">
                            <div class="form-group has-feedback">
                                <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="User Name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                <span class="glyphicon glyphicon-user form-control-feedback"></span>
                            </div>
                            <div class="form-group has-feedback">
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control txtPassword" oncopy="return false" onpaste="return false" oncut="return false" placeholder="Password" TextMode="Password" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                                <div runat="server" id="pwddiv" class="pwddiv" style="" title="click to show and hide password">
                                    <span class="glyphicon glyphicon-lock form-control-feedback" id="pwdspan"></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-6">
                                </div>
                                <!-- /.col -->
                                <div class="col-xs-6">
                                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClientClick="return GetName();" class="btn btn-primary btn-block btn-flat" OnClick="btnLogin_Click" />

                                </div>
                                <!-- /.col -->
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-1 col-xs-1">
            </div>
        </div>

        <!-- /.login-box-body -->
    </div>
    <!-- /.content-wrapper -->
    <footer class="main-footer" style="margin-left: 0px; color: #03a9f4; position: absolute; bottom: 0; width: 100%;">
        <div class="pull-right  hidden-xs image">
            <img style="height: 38px; width: 77px;" src="Dashboard/dist/img/Lipi%20Blue.png" />
        </div>
        <div style="text-align: center; padding-left: 7%">
            <strong>Copyright &copy; 2020 <u>LIPI DATA SYSTEM </u>|</strong> All rights
            reserved.
        </div>
    </footer>


    <!-- jQuery 3 -->
    <script src="Dashboard/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="Dashboard/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="Dashboard/plugins/iCheck/icheck.min.js"></script>

    <style>
        .pwddiv {
            height: 40px;
            width: 15%;
            position: absolute;
            top: 0;
            text-align: right;
            right: 0;
        }

            .pwddiv:hover {
                cursor: pointer;
            }
    </style>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '0%' /* optional */
            });
        });
    </script>
</body>
</html>

