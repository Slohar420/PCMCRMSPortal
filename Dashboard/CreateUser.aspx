<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master"  AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Dashboard_CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function fn_validateNumeric() {
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;
            else return true;
            if (((keycode >= 65) && (keycode <= 90)) || ((keycode >= 97) && (keycode <= 122))) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function RestrictSpace() {
            if (event.keyCode == 32) {
                return false;
            }
        }


    </script>





    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>User Management Page
                  
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dasshboard"></i>Home</a></li>
            <li><a href="#">User Management</a></li>
            <li class="active">Create User</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content" style="margin-top: 40px;">
        <div class="row">
            <!-- Default box -->
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">User Creation</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->

                    <div class="box-body ">

                        <div class="input-group">
                            <span class="input-group-addon">@</span>
                            <asp:TextBox ID="txtusername" class="form-control " placeholder="Enter Username" runat="server" MaxLength="30" onkeypress="return RestrictSpace()" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <asp:TextBox ID="txtPassword" class="form-control " placeholder="Enter Password" runat="server" MaxLength="30" onkeypress="return RestrictSpace()" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </div>
                        <div>
                            <asp:RegularExpressionValidator ID="RegExp1" runat="server"
                                ErrorMessage="Password length must be between 7 to 10 characters atleast 1 UpperCase & 1 LowerCase Alphabet, 1 Number & 1 Special Character" ForeColor="Red"
                                ControlToValidate="txtPassword"
                                ValidationExpression="^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"  />      
                        </div>

                        <%--<div class="input-group">      ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}" 
                            <span>Select Question </span>
                            <a href="#" data-toggle="tooltip" title="Select Question of Your Choice">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="filterlist" OnSelectedIndexChanged="filterlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </a>
                        </div>--%>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <span>Select Question </span>
                                    <a href="#" data-toggle="tooltip" title="Select Question of Your Choice">
                                        <asp:DropDownList CssClass="form-control" runat="server" ID="filterlist" OnSelectedIndexChanged="filterlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </a>
                                </div>

                                <div class="col-xs-6">
                                    <span for="exampleInputusername1">Answer</span>
                                    <asp:TextBox ID="txtAnswer" class="form-control" placeholder="Enter the Answer For Desired Question" runat="server" onkeypress="return fn_validateNumeric();" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <br />
                        <div class="input-group" runat="server" id="divLocation" visible="false">
                            <span class="input-group-addon">$</span>
                            <asp:TextBox ID="txtLocation" class="form-control " placeholder="Enter Location For User" runat="server" MaxLength="30" onkeypress="return RestrictSpace()" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </div>


                        <span for="exampleInputAssignRole">Assign User Role</span>
                        <div class="form-group">
                            <div class="checkbox ">
                                <label>
                                    <asp:CheckBox ID="chkAdmin" runat="server" class="flat-red" Text="Administrator" OnCheckedChanged="chkAdmin_CheckedChanged" AutoPostBack="true" />

                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkUser" class="flat-red" runat="server" Text="User" OnCheckedChanged="chkCreator_CheckedChanged" AutoPostBack="true" />

                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkLocation" class="flat-red" Text="Location" runat="server" OnCheckedChanged="chkLocation_CheckedChanged" AutoPostBack="true" />

                                </label>
                            </div>
                            <%--  <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkManager" class="flat-red" Text="Manager" runat="server" />
                                    
                                </label>
                            </div>--%>
                        </div>


                        <!-- /.box-body -->
                        <div class="form-group">

                            <div class="col-md-12">
                                <div class="" style="text-align: center">
                                    <asp:Button ID="btnsubmit" class="btn btn-primary" runat="server" Text="Submit" OnClick="btnsubmit_Click"></asp:Button>
                                    <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="Clear" OnClick="btnCancel_Click"></asp:Button>
                                    <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Go back" OnClick="btnBack_Click"></asp:Button>
                                </div>
                            </div>

                        </div>

                    </div>
                    <!-- /.box -->

                    <div class="box-footer">
                        <asp:Label ID="lblPassword" runat="server" Font-Bold="True" Visible="true"></asp:Label>



                    </div>
                </div>
            </div>

        </div>
    </section>
    <!-- /.content -->



</asp:Content>

