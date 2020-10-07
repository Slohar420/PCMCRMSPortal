<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Login | Digital Signage </title>

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

  <%--  custom js--%>
<script src="Dashboard/js/Custom.js"></script>
  
  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">

</head>
<body class="hold-transition login-page">
    
        <div class="login-box">
            <div class="login-logo"  >
    <a><b>Digital</b>Signage</a>
  </div>
            <div class="login-box-body">
                <p class="login-box-msg">Reset your Password start your session</p>
                <form id="form1" runat="server">
                <div class="form-group has-feedback">
      
          <label>User Name </label>
                                                <%--<asp:TextBox ID="Txtusername" placeholder="Enter your username" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Visible="true"></asp:Label>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
                    <div class="form-group has-feedback">
          <label>New Password </label>
                                                <asp:TextBox ID="txt_Pwd" autocomplete="off" placeholder="Enter your password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
                    <div class="form-group has-feedback">
           <label>Re-enter New Password </label>

                                                <asp:TextBox ID="txt_ConfirmPwd" autocomplete="off" placeholder="Enter your confirm password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div><div class="row">
        <div class="col-xs-4">
          <asp:Button ID="btnsubmit" class="btn btn-primary" runat="server" Text="Submit" OnClick="btnsubmit_Click"></asp:Button>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
             <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="Clear" OnClick="btnCancel_Click"></asp:Button>
        </div>
        <!-- /.col -->
          <div class="col-xs-4">
             <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Go Back" OnClick="btnBack_Click"></asp:Button>
        </div>
      </div>
                </form>
            </div>
        </div>
    
</body>
</html>
