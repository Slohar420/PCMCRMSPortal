<%@ Page Title="Admin Panel | PCMC" Language="C#" MasterPageFile="~/Dashboard/DSMaster.master" AutoEventWireup="true" CodeFile="PatchUpdation.aspx.cs" Inherits="Dashboard_BroadcastTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function CheckAll(oCheckbox) {
            debugger;
          //  var GridView2 = document.getElementById("<%=GV_Kiosk_Details.ClientID %>");
            var rows1 = $('[id*=GV_Kiosk_Details] tr:not(:has(th))');
            for (i = 0; i < rows1.length; i++) {
                if ($(rows1[i]).css('display') != 'none') {
                    rows1[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
                }
            }
        }

    </script>


    <script type="text/javascript">
        $(function () {
            debugger;
            $('[id*=btnExport]').on('click', function () {
                ExportToExcel('GV_Kiosk_Details');
               // location.reload();
            });
        });
        function ExportToExcel(Id) {
           
            
            var datetime = FormatedTime(new Date());

            var tab_text = "";
            tab_text += "<table border='2px'><tr><td colspan='6' style='background-color:#367fa9;color:white' align='center'><h2> Kiosk List For Patch Updation (" + datetime+ ")</h2><br/></td></tr> <tr>";
            var textRange;
            var j = 0;
            tab = document.getElementById(Id);


            //var table = document.getElementById(Id),
            //rows = table.rows;

            //for (var i = 0; i < rows[0].cells.length; i++) {
            //    var str = rows[0].cells[i].innerHTML;
            //    if (str.search("<span></span>") != -1) {
            //        for (var j = 0; j < rows.length; j++) {
            //            rows[j].deleteCell(i);
            //        }
            //    }
            //}

            var headerRow = $('[id*=GV_Kiosk_Details] tr:first').children();

            for (var i = 1; i < headerRow.length; i++) {
                tab_text +="<td align='center' style='font-weight:bold;' >"+ headerRow[i].innerHTML; +"</td>";
            }

            //tab_text += headerRow.html() + '</tr>';
            tab_text +=  '</tr>';

            var rows = $('[id*=GV_Kiosk_Details] tr:not(:has(th))');

            for (j = 0; j < rows.length; j++) {
                
                if ($(rows[j]).css('display') != 'none') {
                    tab_text += "<tr>";
                    var row = rows[j];
                    for (var i = 1, col; col = row.cells[i]; i++) {
                        tab_text += "<td align='center' style='font-weight:400;' >" + col.innerHTML + "</td>";
                    }

                    tab_text += "</tr>";
                }

            }
            ////remove the 1st column
            //$('#table').find('td,th').first().remove();

            ////remove the 1st column
            //$('table tr').find('td:eq(1),th:eq(1)').remove();

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


    <script type="text/javascript">
        function Search_Gridview(strKey) {
            debugger;
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=GV_Kiosk_Details.ClientID %>");
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
        <h1>Software Update
                    <small style="color:black;"><b>(Patch Updation Panel)</b></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Adminpanel.aspx"><i class="fa fa-dashboard"></i>Admin Panel</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Patch Updation Panel</a></li>
           <%-- <li class="active">Boradcast Templates</li>--%>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <section class="col-lg-12 ">
                <!--data table box-->
                <div class="box box-info">
                    <div class="box-header with-border ">
                        <div class="col-md-3" style="padding-top:20px;">
                             <asp:FileUpload runat="server" ID="FileUpload1" accept=".zip,.rar" CssClass="form-control" Enabled="true" AllowMultiple="false" />
                               </div>

                        <div class="col-md-3">
                            <%--<span>Start Date and Time</span>
                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-clock-o"></i>
                                    </div>
                                     <a href="#" data-toggle="tooltip" title="Select start date/time and end date/time for broadcast  ">
                                    <input type="text" class="form-control pull-right" name="BroadcastdateTime" id="BroadcastdateTime" />
                                </a>

                                </div>
                            </div>--%>

                        </div>

                        <div class="col-md-3">
                            <span>Search</span>
                            <a href="#" data-toggle="tooltip" title="Search for filter Records">
                                <asp:textbox id="txtSearch" class="form-control" runat="server" onkeydown="return (event.keyCode!=13);" onkeyup="Search_Gridview(this)"></asp:textbox>
                            </a>
                        </div>

                        <div class="col-md-2">
                            <span>Download</span>
                           <%--// <asp:Button ID="btnExcelExport" runat="server" Text="Export Excel" class="btn btn-success form-control" OnClick="btnExcelExport_Click" />--%>
                            <input id="btnExport" type="button" value="Export Report" class="btn btn-success form-control" />

                        </div>

                    </div>

                    <!-- /.box-header -->
                    <div class="box-body" style="max-height: 500px; overflow-y: scroll">
                         <div class="col-md-6">
                            <h3 class="box-title">Kiosk List For Patch Updation</h3>

                        </div>

                        <asp:GridView ID="GV_Kiosk_Details" class="table table-bordered table-striped table-hover" runat="server" ClientIDMode="AutoID" HeaderStyle-BackColor=" #eafafa">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <a href="#" data-toggle="tooltip" title="Select All Checkbox"> <input id="Checkbox2" tooltip="Show" type="checkbox" onclick="CheckAll(this)" runat="server" /></a>
                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" CssClass="gridCB" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- /.box-body -->

                    <%-- box-footer--%>
                    <div class="box-footer">
                        <div class="col-md-4">
                           <%-- <asp:Button runat="server" ID="Broadcast" OnClick="Broadcast_Click" Text="Instant Broadcast" CssClass="btn btn-info form-control " />--%>
                        </div>
                        
                        <div class="col-md-2" style="align-items:center">
                            <asp:Button runat="server" ID="upload" CssClass="btn btn-success form-control" OnClick="Click_Function" Text="UPLOAD" />
                        </div>
                         <div class="col-md-2" style="align-items:center">
                            <asp:Button runat="server" ID="instantupload" CssClass="btn btn-success form-control" OnClick="Click_Function" Text="INSTANT UPLOAD" />
                        </div>
                        <div class="col-md-4">
                           <%-- <asp:Button runat="server" ID="Button1" OnClick="Broadcast_Click" Text="Instant Broadcast" CssClass="btn btn-info form-control " />--%>
                        </div>
                    </div>
                    <%--/box body--%>

                    <%--   <div class="row">
                       
                <asp:Button runat="server" ID="Preview" OnClick="Preview_Click" Text="Preview" CssClass="btn btn-danger" />
                <asp:Button runat="server" ID="Modify" OnClick="Modify_Click" Text="Modify This Template" CssClass="btn btn-info" />
                <asp:Button runat="server" ID="Delete" OnClick="Delete_Click" Text="Delete" CssClass="btn btn-warning" />
                    </div>--%>
                </div>

            </section>
        </div>
    </section>
</asp:Content>

