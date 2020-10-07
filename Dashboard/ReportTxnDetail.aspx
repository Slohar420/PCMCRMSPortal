<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="ReportTxnDetail.aspx.cs" Inherits="Dashboard_ReportTxnDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <script type="text/javascript">

        var filter = 0;

        function Search_Gridview(strKey) {
            filter = 0;
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=GV_Kiosk_Health.ClientID %>");
            var rowData;

            var totalRow = tblData.rows.length;
            debugger;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';

                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0) {
                        styleDisplay = '';
                        filter++;

                    }
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }

                tblData.rows[i].style.display = styleDisplay;

            }
         <%--   var rowsCount = <%=GV_Kiosk_Health.Rows.Count %>;--%>
            document.getElementById("Show").innerText = filter + ' Out of ';
        }
    </script>
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
            var tab_text = "<h2> Kiosk Transaction Report -(" + dateTime + ")</h2><br/>";
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
        <h1>Transaction Report Dashboard </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-th"></i>Report Section</a></li>
            <li class="active">Transaction Report</li>
        </ol>
    </section>



    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">

            <div id="div1" runat="server" class="col-lg-2" visible="false">
                <!-- small box -->
                <div class="small-box bg-green">
                    <div class="inner">
                        <h4>
                            <asp:Label Visible="false" ID="l1" runat="server" Text="Label">D1 (CashAcceptor)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l2" runat="server" Text="Label">D2 (ReceiptPrinter)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l3" runat="server" Text="Label">D3 (BarCodeReader)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l4" runat="server" Text="Label">D4 (DocScanner)</asp:Label>
                        </h4>

                    </div>

                </div>
            </div>

            <div id="div2" runat="server" class="col-lg-2" visible="false">
                <!-- small box -->
                <div class="small-box bg-yellow">
                    <div class="inner">
                        <h4>
                            <asp:Label Visible="false" ID="l5" runat="server" Text="Label">D5 (Camera)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l6" runat="server" Text="Label">D6 (VCCamera)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l7" runat="server" Text="Label">D7 (CardReader)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l8" runat="server" Text="Label">D8 (LaserPrinter)</asp:Label>
                        </h4>
                    </div>
                </div>
            </div>

            <div id="div3" runat="server" class="col-lg-2" visible="false">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <h4>
                            <asp:Label Visible="false" ID="l9" runat="server" Text="Label">D9 (FingerScanner)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l10" runat="server" Text="Label">D10 (KeyPadMouse)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l11" runat="server" Text="Label">D11 (SignageTV)</asp:Label>
                        </h4>
                        <h4>
                            <asp:Label Visible="false" ID="l12" runat="server" Text="Label">D12 (TouchScreen)</asp:Label>
                        </h4>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <section class="col-lg-12 ">

                <div class="box box-info">
                    <div class="box-header with-border ">
                        <div class="col-md-2">
                            <h3 class="box-title">Transaction Status</h3>
                        </div>
                        <div class="col-md-2">
                            <span>Search</span>
                            <a href="#" data-toggle="tooltip" title="Search for filter Records">
                                <asp:TextBox ID="txtSearch" onKeyDown="return (event.keyCode!=13);" class="form-control" runat="server" onkeyup="Search_Gridview(this)"></asp:TextBox>
                            </a>
                        </div>
                        <div class="col-md-2" id="filter" runat="server">
                            <span>Select Filter </span>
                            <a href="#" data-toggle="tooltip" title="Select Type For Filter">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="filterlist" OnSelectedIndexChanged="filterlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </a>
                        </div>
                        <div class="col-md-2" id="location" runat="server" visible="false">
                            <asp:label  Text="Select Location" id="lbllocation" runat="server"></asp:label>
                            <a href="#" data-toggle="tooltip" title="Select Location For Filter">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="locationlist" OnSelectedIndexChanged="locationlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </a>
                        </div>
                        <div class="col-md-2">
                            <span>Download PDF</span>
                            <asp:Button Style="background-color: #ec5e5e;" runat="server" ID="btn1" type="button" Text="Export PDF" OnClick="btn1_Click" class="btn btn-success form-control" />
                        </div>
                        <div class="col-md-2">
                            <span>Download Excel</span>
                            <input id="btnExport" type="button" value="Export Excel" class="btn btn-success form-control" />
                        </div>

                    </div>


                    <div class="box-body" runat="server" style="max-height: 500px; overflow-y: scroll">
                        <asp:GridView ID="GV_Kiosk_Health" class="table table-bordered table-fixed table-striped table-hover text-center " Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium" HeaderStyle-BackColor=" #eafafa" runat="server" ClientIDMode="AutoID">
                        </asp:GridView>
                        <img runat="server" ID="ErrorImg" src="~/Dashboard/images/download.png" style="padding-left:40%" visible="false" />
                    </div>
                </div>

            </section>
        </div>

    </section>
    <!--page script-->



</asp:Content>

