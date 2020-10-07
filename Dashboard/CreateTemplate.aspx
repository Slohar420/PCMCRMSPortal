<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="CreateTemplate.aspx.cs" Inherits="Dashboard_CreateTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <section class="content">
        <div class="row">
            <div class="col-md-5">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Design Canvas</h3>
                    </div>
                    <div class="box-body" style="height: 350px">

                        <div class="col-md-12">
                            <span>Select Template Resolution</span>
                            <asp:DropDownList runat="server" ID="Resolution" CssClass="form-control ddl" Enabled="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="4:3" Value="1" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="5:4" Value="2" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="5:3" Value="3" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="16:10" Value="4" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="16:9" Value="5" Selected="False"></asp:ListItem>
                                <asp:ListItem Text="1:1" Value="6" Selected="False"></asp:ListItem>
                            </asp:DropDownList>


                        </div>


                        <div class="col-md-12" id="previewPanel" style="padding: 5px; height: 100%; width: 100%; background-color: #ededed; border: 1px solid #808080">
                            <%--<asp:Panel ID="Template" Class="Preview" runat="server"></asp:Panel>--%>
                        </div>


                    </div>
                    <div class="box-footer"></div>
                </div>
            </div>

            <div class="col-md-7">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Start Designing</h3>
                    </div>
                    <div class="box-body" style="height: 400px">
                        <div id="Row1" visible="true" runat="server" class="col-md-6">
                            <asp:Button ID="Button1" runat="server" Text="Add New Div" CssClass="btn btn-info" />

                        </div>
                        <div class="col-md-6" runat="server">
                            <asp:DropDownList runat="server" ID="DivList" Visible="false" CssClass="form-control ddl" AutoPostBack="true"></asp:DropDownList>

                        </div>


                    </div>
                    <div class="box-footer"></div>
                </div>

            </div>

            <%--  <div class="col-md-5">
                 <div class="box box-default">
                      <div class="box-header with-border">
                        <h3 class="box-title">Design Layout</h3>
                    </div>
                       <div class="box-body" style="height:100px">
                          
                    </div>
                       <div class="box-footer"></div>
                 </div>
            </div>--%>
        </div>


    </section>

</asp:Content>

