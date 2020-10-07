<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="MachineReports.aspx.cs" Inherits="Dashboard_MachineReports" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(function () {

            $('[id*=btnExport]').on('click', function () {
                ExportToExcel('GV_Kiosk_Health');
                // location.reload();
            });
        });
        function ExportToExcel(Id) {

            var today = new Date();
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            var dateTime = date + ' ' + time;
            var tab_text = "<h2> Kiosk Health Report -(" + dateTime + ")</h2><br/>";
            tab_text += "<table border='2px'><tr>";
            var textRange;
            var j = 0;
            tab = document.getElementById(Id);
            var headerRow = $('[id*=GV_Kiosk_Health] tr:first');

            tab_text += headerRow.html() + '</tr><tr>';
            var rows = $('[id*=GV_Kiosk_Health] tr:not(:has(th))');
            for (j = 0; j < rows.length; j++) {
                if ($(rows[j]).css('display') != 'none') {
                    tab_text = tab_text + rows[j].innerHTML + "</tr>";
                }
            }
            tab_text = tab_text + "</table>";
            // tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, ""); //remove if u want links in your table
            // tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            // tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, Id + ".xlsx");
            }
            else {                 //other browser not tested on IE 11
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
            }
            return (sa);
        }
    </script>
    <section class="content-header">
        <h1>Machine Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-th"></i>Report Section</a></li>
            <li class="active">Machine Report</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <section class="col-lg-12" style="padding-top:5px">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <div class="col-md-2">
                            <h3 class="box-title">Machine Reports</h3>
                        </div>
                        <div class="col-md-2">
                            <span>Select</span>
                            <a href="#" data-toggle="tooltip" title="Select Type For Filter">
                                <asp:DropDownList runat="server" ID="filtertype" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="filtertype_SelectedIndexChanged"><asp:ListItem Text="All" Value="All"></asp:ListItem><asp:ListItem Text="Online" Value="Online"></asp:ListItem><asp:ListItem Text="Offline" Value="Offline"></asp:ListItem></asp:DropDownList>
                            </a>
                        </div>
                        <div class="col-md-2" id="mode" runat="server" visible="true">
                            <span>Select Mode</span>
                            <a href="#" data-toggle="tooltip" title="Select Filter Mode">
                                <asp:DropDownList runat="server" ID="Modefilter" CssClass="form-control" OnSelectedIndexChanged="Modefilter_SelectedIndexChanged" AutoPostBack="true"><asp:ListItem Text="Both" Value="Both"></asp:ListItem><asp:ListItem Text="Activated" Value="Activated"></asp:ListItem><asp:ListItem Value="Pending" Text="Pending"></asp:ListItem></asp:DropDownList>
                            </a>
                        </div>
                        <div class="col-md-2" id="pdfdiv" runat="server" visible="false" >
                            <span>Download PDF</span>
                            <a href="#" data-toggle="tooltip">
                                <asp:Button Style="background-color: #ec5e5e;" runat="server" ID="PDF" Text="Export PDF" CssClass="btn btn-success form-control" OnClick="PDF_Click" />
                            </a>
                        </div>

                        <div class="col-md-2" id="exdiv" runat="server" visible="false"  >
                            <span>Download Excel</span>
                            <a href="#" data-toggle="tooltip">
                               
                                <input  id="btnExport" type="button" value="Export Report" class="btn btn-success form-control" />
                            </a>
                        </div>
                        
                    </div>
                     <div class="box-body" runat="server" style="max-height: 500px; overflow-y: scroll">
                               
                                <asp:GridView ID="GV_Kiosk_Health"  class="table table-bordered table-fixed table-striped table-hover text-center " HeaderStyle-BackColor=" #eafafa" runat="server" ClientIDMode="AutoID" Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium">
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

