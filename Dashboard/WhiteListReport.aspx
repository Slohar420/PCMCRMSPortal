<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="WhiteListReport.aspx.cs" Inherits="Dashboard_WhiteListReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>WhiteList Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-th"></i>WhiteList Section</a></li>
            <li class="active">WhiteList Report</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <section class="col-lg-12" style="padding-top:5px;">
                <div class="box box-info">
                    <div class="box-header with-border ">
                        <div class="col-md-4">
                            <h3 class="box-title">WhiteList Reports</h3>
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
                                <asp:Button runat="server" ID="search" CssClass="btn btn-bitbucket" Text="Search" OnClick="search_Click"/>
                            </a>
                        </div>
                        
                    </div>
                    <div class="box-header with-border">
                        <div class="col-md-2" id="exdiv" runat="server" visible="false">
                            <a href="#" data-toggle="tooltip" title="Excel">
                                <asp:Button runat="server" ID="Excel" CssClass="btn btn-success" Text="Excel" OnClick="Excel_Click"/>
                            </a>
                        </div>
                        <div class="col-md-2" id="pdfdiv" runat="server" visible="false">
                            <a href="#" data-toggle="tooltip" title="PDF" >
                                <asp:Button runat="server" ID="PDF" CssClass="btn btn-danger" Text="PDF" OnClick="PDF_Click" />
                            </a>
                        </div>

                    </div>
                    <div class="box-body" runat="server" style="max-height: 500px; overflow-y: scroll">
                               
                                <asp:GridView ID="GV_Kiosk_Health" class="table table-bordered table-fixed table-striped table-hover " HeaderStyle-BackColor=" #eafafa" runat="server" ClientIDMode="AutoID" Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium">
                                    <Columns>
                                       <%-- <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkView" Text="View" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:BoundField DataField="Kiosk IP" HeaderText="Kiosk IP" ReadOnly="True" SortExpression="Kiosk IP" />
                                        <asp:BoundField DataField="Kiosk ID" HeaderText="Kiosk ID" SortExpression="Kiosk ID" />
                                        <asp:BoundField DataField="CashAcceptor" HeaderText="CashAcceptor" SortExpression="CashAcceptor" />
                                        <asp:BoundField DataField="RecieptPrinter" HeaderText="RecieptPrinter" SortExpression="RecieptPrinter" />
                                        <asp:BoundField DataField="BarcodeReader" HeaderText="BarcodeReader" SortExpression="BarcodeReader" />
                                        <asp:BoundField DataField="DocScanner" HeaderText="DocScanner" SortExpression="DocScanner" />
                                        <asp:BoundField DataField="CameraStatus" HeaderText="CameraStatus" SortExpression="CameraStatus" />
                                        <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" />--%>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="gridviewPager" />

                                </asp:GridView>
                                <img runat="server" ID="ErrorImg" src="~/Dashboard/images/download.png" style="padding-left:40%" visible="false" />
                                   <%--<img runat="server" ImageUrl="~/Dashboard/images/download.png" Height="100px" Width="100px" ImageAlign="Middle" style="display:flex;justify-content:center;position:absolute" Visible="false"  />--%>
                            </div>
                </div>
            </section>
        </div>
    </section>
</asp:Content>

