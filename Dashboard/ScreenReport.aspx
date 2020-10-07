<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="ScreenReport.aspx.cs" Inherits="Dashboard_ScreenReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Detail Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-th"></i>Report Section</a></li>
            <li class="active">Screen Report</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <section class="col-lg-12" style="padding-top:5px;">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <div class="col-md-4">
                            <h3 class="box-title">Screen Captured</h3>
                        </div>
                    
                    <div class="col-md-2">
                            <span>Select</span>
                            <a href="#" data-toggle="tooltip" title="Select Type For Filter">
                                <asp:DropDownList runat="server" ID="filtertype" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="filtertype_SelectedIndexChanged"><asp:ListItem Text="-Select-" Value="0"></asp:ListItem><asp:ListItem Text="Machine IP" Value="1"></asp:ListItem><asp:ListItem Text="Machine ID" Value="2"></asp:ListItem></asp:DropDownList>
                            </a>
                        </div>
                        <div class="col-md-2" id="machineip" runat="server" visible="false">
                            <span>Select IP </span>
                            <a href="#" data-toggle="tooltip" title="Select Machine IP For Filter">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="machineiplist" OnSelectedIndexChanged="machineiplist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </a>
                        </div>
                       <div class="col-md-2" id="machineid" runat="server" visible="false">
                            <span>Select ID </span>
                            <a href="#" data-toggle="tooltip" title="Select Machine ID For Filter">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="machineidlist" OnSelectedIndexChanged="machineidlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </a>
                        </div>    
                        <div class="col-md-2" style="margin-top:20px;">
                            
                            <a href="#" data-toggle="tooltip" title="Click to Made the Search">
                                <asp:Button runat="server" ID="search" CssClass="btn btn-bitbucket" Text="Search" OnClick="search_Click" HeaderStyle-BackColor=" #eafafa"/>
                            </a>
                        </div>
                </div>
                    <div class="box-body" runat="server" style="max-height: 500px; overflow-y: scroll">
                        <img runat="server" ID="ErrorImg" src="~/Dashboard/images/download.png" style="padding-left:40%" visible="false" />
                    </div>
                </div>
            </section>
        </div>
    </section>
</asp:Content>

