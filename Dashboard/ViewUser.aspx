<%@ Page Title="Users | PCMC" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master"   AutoEventWireup="true" CodeFile="ViewUser.aspx.cs" Inherits="Dashboard_ViewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .btn {
               width:100px;
           }
    </style>
    <script type="text/javascript">
        function Search_Gridview(strKey) {
            debugger;
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=GV_Kiosk_Health.ClientID %>");
            var rowData;
            for (var i = 1; i <= tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    
       <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Manage User Page
                  
                        </h1>
                        <ol class="breadcrumb">
                            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                            <li><a href="#">User Management</a></li>
                            <li class="active">Manage User</li>
                        </ol>
                    </section>
                    <!-- Main content -->
                    <section class="content">
                        <div class="row">
                            <!-- Default box -->
                            <div class="col-md-12">
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <div class="col-md-6"> 
                                        <h3 class="box-title">View User Detail</h3>
                                  
                       </div>  
    <div class="col-md-3">
                           
                            <a href="#" data-toggle="tooltip" title="Search for filter Records">
                                <asp:TextBox ID="txtSearch" placeholder="Search" onKeyDown="return (event.keyCode!=13);" class="form-control"  runat="server" onkeyup="Search_Gridview(this)"></asp:TextBox>
                        </a>
                                </div>


                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body">
                                        <div class="col-md-12 table-responsive" style="max-height: 500px; overflow-y: auto">

                                            <asp:GridView ID="GV_Kiosk_Health" class="table table-bordered table-striped table-hover text-center" runat="server" ClientIDMode="AutoID" OnRowCommand="GV_Kiosk_Health_RowCommand" OnRowDeleting="GV_Kiosk_Health_RowDeleting" AutoGenerateColumns="false" HeaderStyle-BackColor=" #eafafa">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="UserName" runat="server" Text='<%#Eval("UserName") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="180px" />
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="First Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="FirstName" runat="server" Text='<%#Eval("FirstName") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LastName" runat="server" Text='<%#Eval("LastName") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Role">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Role" runat="server" Text='<%#Eval("credential_type") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="User Creation Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="UserCreationDate" runat="server" Text='<%#Eval("UserCreationDate","{0:dd MMM yyyy}")  %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Reset">
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnReset" CommandName="Reset" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="btn btn-warning btn" OnClick="BtnReset_Click" Text="Reset" OnClientClick="return confirm('Do you want to reset it ?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Manage Role">
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnManageRole" CommandName="ManageRole" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="btn btn-info btn" Text="Manage Role" OnClick="Display"  OnClientClick="return confirm('Do you want to modify it ?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete User">
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnDeleteUser" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="btn btn-danger btn" Text="Delete"   OnClientClick="return confirm('Do you want to delete this user ?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Label ID="lblPassword" runat="server" Font-Bold="True" Font-Italic="True"
                                                        Font-Names="Arial" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="modal fade" id="myModal" role="dialog">
                                                    <div class="modal-dialog">
                                                        <!-- Modal content-->
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">
                                                                    &times;</button>
                                                                <h4 class="modal-title">Assign Role</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="col-lg-12 col-sm-12 col-md-12 col-xs-12">
                                                                    <div class="form-group">
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <asp:CheckBox ID="chkAdmin" runat="server" class="flat-red" Text="Administrator" />
                                                                            </label>
                                                                        </div>
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <asp:CheckBox ID="chkCreator" class="flat-red" runat="server" Text="Creator" />
                                                                            </label>
                                                                        </div>
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <asp:CheckBox ID="chkApproval" class="flat-red" Text="Approval" runat="server" />
                                                                            </label>
                                                                        </div>
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <asp:CheckBox ID="chkManager" class="flat-red" Text="Manager" runat="server" />
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnSave" runat="server" Text="OK" OnClick="btnSave_Click" CssClass="btn btn-info" />
                                                                <button type="button" class="btn btn-info" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <script type='text/javascript'>
                                                        //function openModal() {
                                                        //    $('[id*=myModal]').modal('show');
                                                        //}
                                                    </script>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box -->

                                    <div class="box-footer"></div>

                                </div>
                            </div>

                        </div>
                    </section>
                    <!-- /.content -->
   

</asp:Content>

