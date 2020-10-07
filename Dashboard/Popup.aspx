<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup.aspx.cs" Inherits="Dashboard_Popup" %>

<!doctype html>
<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/libs/css/style.css">
    <link rel="stylesheet" href="assets/vendor/fonts/fontawesome/css/fontawesome-all.css">
    <link rel="stylesheet" href="assets/vendor/charts/chartist-bundle/chartist.css">
    <link rel="stylesheet" href="assets/vendor/charts/morris-bundle/morris.css">
    <link rel="stylesheet" href="assets/vendor/fonts/material-design-iconic-font/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="assets/vendor/charts/c3charts/c3.css">
    <link rel="stylesheet" href="assets/vendor/fonts/flag-icon-css/flag-icon.min.css">
	 <link rel="stylesheet" href="assets/libs/css/style.css">
    <title>Detailed Description</title>
     <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:400,500,700,300,100);



        div.table-title {
           display: block;
          margin: auto;
          max-width: 600px;
          padding:5px;
          width: 100%;
        }

        .table-title h3 {
           color: #fafafa;
           font-size: 30px;
           font-weight: 400;
           font-style:normal;
           font-family: "Roboto", helvetica, arial, sans-serif;
           text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1);
           text-transform:uppercase;
        }


        /*** Table Styles **/

        .table-fill {
          background: white;
          border-radius:3px;
          border-collapse: collapse;
          height: 320px;
          margin: auto;
          max-width: 600px;
          padding:5px;
          width: 100%;
          box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
          animation: float 5s infinite;
        }
 
        th {
          color:#000000;;
          background:#b4a9e2;
          border-bottom:4px solid #9ea7af;
          border-right: 1px solid #343a45;
          font-size:23px;
          font-weight: 100;
          padding:24px;
          text-align:left;
          text-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);
          vertical-align:middle;
        }

        th:first-child {
          border-top-left-radius:3px;
        }
 
        th:last-child {
          border-top-right-radius:3px;
          border-right:none;
        }
  
        tr {
          border-top: 1px solid #C1C3D1;
          border-bottom-: 1px solid #C1C3D1;
          color: darkblue;
          font-size:17px;
          font-weight:normal;
          text-shadow: 0 1px 1px rgba(256, 256, 256, 0.1);
        }
 
        tr:hover td {
          background:#4E5066;
          color:#FFFFFF;
          border-top: 1px solid #22262e;
        }
 
        tr:first-child {
          border-top:none;
        }

        tr:last-child {
          border-bottom:none;
        }
 
        tr:nth-child(odd) td {
          background:#EBEBEB;
        }
 
        tr:nth-child(odd):hover td {
          background:#4E5066;
        }

        tr:last-child td:first-child {
          border-bottom-left-radius:3px;
        }
 
        tr:last-child td:last-child {
          border-bottom-right-radius:3px;
        }
 
        td {
          background:#FFFFFF;
          padding:20px;
          text-align:left;
          vertical-align:middle;
          font-weight:300;
          font-size:18px;
          text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1);
          border-right: 1px solid #C1C3D1;
        }

        td:last-child {
          border-right: 0px;
        }

        th.text-left {
          text-align: left;
        }

        th.text-center {
          text-align: center;
        }

        th.text-right {
          text-align: right;
        }

        td.text-left {
          text-align: left;
        }

        td.text-center {
          text-align: center;
        }

        td.text-right {
          text-align: right;
        }

</style>
</head>
<body>

    <form runat="server">
    <div class="ecommerce-widget">
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h4 class="text-muted">Cash Acceptor</h4>
                        <div class="metric-value d-inline-block">
                            <h3 class="mb-1">Disconnected/Total</h3>
                        </div>
                        <div class="metric-label d-inline-block float-right text-success font-weight-bold">
                            <span id="cashacceptor" runat="server" style="font-size:28px;">3/10</span>
                        </div>
                    </div>
                    <div id="sparkline-revenue"></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h4 class="text-muted">Reciept Printer</h4>
                        <div class="metric-value d-inline-block">
                            <h3 class="mb-1">Disconnected/Total</h3>
                        </div>
                        <div class="metric-label d-inline-block float-right text-success font-weight-bold">
                            <span id="recieptprinter" runat="server" style="font-size:28px;">8/10</span>
                        </div>
                    </div>
                    <div id="sparkline-revenue2"></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h4 class="text-muted">Total Machine</h4>
                        <div class="metric-value d-inline-block">
                            <h3 class="mb-1">Connected/Disconnected</h3>
                        </div>
                        <div class="metric-label d-inline-block float-right text-primary font-weight-bold">
                            <span id="total" runat="server" style="font-size:28px;">6/4</span>
                         
                        </div>
                    </div>
                    <div id="sparkline-revenue3"></div>
                </div>
            </div>
            <!--<div class="col-md-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="text-muted">Avg. Revenue Per User</h5>
                    <div class="metric-value d-inline-block">
                        <h1 class="mb-1">$28000</h1>
                    </div>
                    <div class="metric-label d-inline-block float-right text-secondary font-weight-bold">
                        <span>-2.00%</span>
                    </div>
                </div>
                <div id="sparkline-revenue4"></div>
            </div>
        </div>-->
        </div>
        <div class="row">

        </div>
        <div class="box-body" style="max-height: 500px; overflow-y: scroll">
            <asp:GridView ID="GV_Kiosk_Health" class="table table-bordered table-fixed table-striped table-hover " runat="server" ClientIDMode="AutoID">
            </asp:GridView>
        </div>
    </div>
      
       
<table align="center">
  <tr>
    <th>Key</th>
    <th>Value</th>
  </tr>
  <tr>
    <td>Kiosk IP</td>
    <td id="kiosk_ip" runat="server"></td>
  </tr>
  <tr>
    <td>Kiosk ID</td>
    <td id="kiosk_id" runat="server"></td>
  </tr>
  <tr>
    <td>Location</td>
    <td id="loc" runat="server">Udaipur</td>
  </tr> 
  <tr>
    <td>Cash Acceptor</td>
    <td id="cash" runat="server"></td>
  </tr>
  <tr>
    <td>Reciept Printer</td>
    <td id="reciept" runat="server"></td>
  </tr>
  <tr>
    <td>Barcode Scanner</td>
    <td id="barcode" runat="server"></td>
  </tr>
     <tr>
    <td>Doc Scanner</td>
    <td id="doc" runat="server"></td>
  </tr>
  <tr>
    <td>Camera Status</td>
    <td id="camera" runat="server"></td>
  </tr>
     <tr>
    <td>VCCamera Status</td>
    <td id="vccamera" runat="server"></td>
  </tr>
     <tr>
    <td>Card Reader</td>
    <td id="cardreader" runat="server"></td>
  </tr>
     <tr>
    <td>Laser Printer</td>
    <td id="laserprinter" runat="server"></td>
  </tr>
     <tr>
    <td>Finger Scanner</td>
    <td id="fingurescanner" runat="server"></td>
  </tr>
     <tr>
    <td>KeyPad/Mouse</td>
    <td id="keypadmouse" runat="server"></td>
  </tr>
     <tr>
    <td>SignageTV</td>
    <td id="signagetv" runat="server"></td>
  </tr>
     <tr>
    <td>TouchScreen</td>
    <td id="touchscreen" runat="server"></td>
  </tr>
     
</table>
<div class="col-md-2" style="left:450px;">       
    <asp:Button id="btn1" runat="server" OnClick="btnCallLog__Click" Text="Call Log" class="btn btn-success form-control" />
        <br /> 
    <asp:Button runat="server" id="btn2" OnClick="btnSendMessage_Click" Text="Send Message" class="btn btn-success form-control" />
</div>
       
        <%--<div>
            <asp:HiddenField id="hidden" runat="server"/> 
        <u>Details</u>
        <br />
        <br />
        <b>Id:</b> <span id="id" runat="server"></span>
        <br />
        <b>Name:</b> <span id="name"></span>
        <br />
        <b>Description:</b> <span id="description"></span>
            <b>Description:</b> <span id="description1"></span>
            <b>Description:</b> <span id="description2"></span>
            <b>Description:</b> <span id="description3"></span>
            <b>Description:</b> <span id="description4"></span>
    </div>--%>
         </form>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            if (window.opener != null && !window.opener.closed) {
                var rowIndex = window.location.href.split("?")[1].split("=")[1];
                var parent = $(window.opener.document).contents();
                var row = parent.find("[id*=GV_Kiosk_Health]").find("tr").eq(rowIndex);
                $("#id").html(row.find("td").eq(1).html());
               

               
            }
        });
    </script>--%>
    
    <script src="assets/vendor/jquery/jquery-3.3.1.min.js"></script>
 
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.js"></script>
   
    <script src="assets/vendor/slimscroll/jquery.slimscroll.js"></script>
    
    <script src="assets/libs/js/main-js.js"></script>
  
    <script src="assets/vendor/charts/chartist-bundle/chartist.min.js"></script>
    <!-- sparkline js -->
    <script src="assets/vendor/charts/sparkline/jquery.sparkline.js"></script>
    <!-- morris js -->
    <script src="assets/vendor/charts/morris-bundle/raphael.min.js"></script>
    <script src="assets/vendor/charts/morris-bundle/morris.js"></script>
    <!-- chart c3 js -->
    <script src="assets/vendor/charts/c3charts/c3.min.js"></script>
    <script src="assets/vendor/charts/c3charts/d3-5.4.0.min.js"></script>
    <script src="assets/vendor/charts/c3charts/C3chartjs.js"></script>
    <script src="assets/libs/js/dashboard-ecommerce.js"></script>
</body>

</html>

