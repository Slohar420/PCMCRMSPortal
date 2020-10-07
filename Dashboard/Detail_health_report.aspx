<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="Detail_health_report.aspx.cs" Inherits="Dashboard_Detail_health_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Detail Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-th"></i>Report Section</a></li>
            <li class="active">Device Report</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <section class="col-lg-12" style="padding-top:5px;">
                <div class="box box-info">

                    <div class="box-header with-border ">

                            <div class="col-md-2">
                                <h3 class="box-title">Detailed Reports</h3>
                            </div>

                            <div class="col-md-2">
                                <span>Select</span>
                                <a href="#" data-toggle="tooltip" title="Select Type For Filter">
                                    <asp:DropDownList runat="server" ID="filtertype" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="filtertype_SelectedIndexChanged"></asp:DropDownList>
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
                         
                            <div class="col-md-2" id="mode" runat="server" visible="true">
                                <span>Select Mode</span>
                                <a href="#" data-toggle="tooltip" title="Select Filter Mode">
                                    <asp:DropDownList runat="server" ID="Modefilter" CssClass="form-control" OnSelectedIndexChanged="Modefilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </a>
                            </div>

                           <div class="col-md-4" id="Dttime" runat="server"  visible="false" >
                                <span>Select Date Range</span>
                                <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-clock-o"></i>
                                        </div>
                                         <a href="#" data-toggle="tooltip" title="Select start date and end date for filteration  ">
                                        <input type="text" class="form-control pull-right" name="ReportTime" id="ReportTime" />
                                         </a>
                                </div>
                            </div>
                    </div>

                    <div class="box-header with-border ">
                        
                            <div class="col-md-2">

                            </div>
                            <div class="col-md-2" style="margin-top:1.5%;">
                            
                                <a href="#" data-toggle="tooltip" title="Click to initiate search">
                                    <asp:Button width="100%" runat="server" ID="search" Text="Search" CssClass="btn btn-bitbucket" OnClick="search_Click" />
                                </a>
                            </div>
                        
                            <div class="col-md-2" id="pdfdiv" runat="server" visible="false" style="margin-top:0%;">
                                <span>Download PDF</span>
                                <a href="#" data-toggle="tooltip">
                                    <asp:Button  Style=" width:100%;  background-color: #ec5e5e; " runat="server" ID="PDF" Text="Export PDF" Class="btn btn-success from-control" OnClick="PDF_Click" />
                                </a>
                            </div>

                            <div class="col-md-2" id="exdiv" runat="server" visible="false" style="margin-top:0%;">
                                <span>Download Excel</span>
                                <a href="#" data-toggle="tooltip">
                                    <asp:Button Style=" width:100%; " runat="server" ID="Excel" Text="Export Excel" Class="btn btn-success from-control" OnClick="Excel_Click" />
                                </a>
                            </div>
                    </div>

                        <div class="panel-body" runat="server" style="max-height: 500px; overflow-y: auto">
                            
                             <asp:GridView ID="GV_Kiosk_Health" class="table table-bordered table-fixed table-striped table-hover text-center " HeaderStyle-BackColor=" #eafafa" runat="server" ClientIDMode="AutoID" Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium">
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
 

                                </asp:GridView>
                                <img runat="server" ID="ErrorImg" src="~/Dashboard/images/download.png" style="padding-left:40%" visible="false" />
                                   <%--<img runat="server" ImageUrl="~/Dashboard/images/download.png" Height="100px" Width="100px" ImageAlign="Middle" style="display:flex;justify-content:center;position:absolute" Visible="false"  />--%>
                    </div>
                </div>
            </section>
        </div>
    </section>
</asp:Content>

