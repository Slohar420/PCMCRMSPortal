using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;

public partial class Dashboard_Detail_health_report : System.Web.UI.Page
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        if (!IsPostBack)
        {
            ErrorImg.Visible = false;
            GV_Kiosk_Health.Visible = false;
            filtertype.Items.Clear();
            filtertype.Items.Add("Select Filteration Type");
            filtertype.Items.Add("All Machines");
            filtertype.Items.Add("Machine ID");
            filtertype.Items.Add("Machine IP");
            Modefilter.Items.Add("Select ");
            Modefilter.Items.Add("Time Base Access");
            Modefilter.Items.Add("HDD Encryption");
            Modefilter.Items.Add("White Listing");
            Modefilter.Items.Add("Process List");
            Session["Data"] = null;
        }
    }

    protected void machineiplist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        pdfdiv.Visible = false;
        exdiv.Visible = false;
        Session["Data"] = null;
    }

    protected void machineidlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        pdfdiv.Visible = false;
        exdiv.Visible = false;
        Session["Data"] = null;
    }

    protected void filtertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        Session["Data"] = null;
        Dttime.Visible = false;
        try
        {
            pdfdiv.Visible = false;
            exdiv.Visible = false;
            machineip.Visible = false;
            machineid.Visible = false;
            if (filtertype.SelectedIndex == 0)
            { return; }
            else if (filtertype.SelectedIndex == 2)
            {
                string s = "MachineID";

                Reply objRes = new Reply();
                WebClient client1 = new WebClient();
                client1.Headers[HttpRequestHeader.ContentType] = "text/json";

                string JsonString = JsonConvert.SerializeObject(s);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result1 = client1.UploadString(URL + "/GetKioskReport", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res)  
                {

                    machineidlist.Items.Clear();
                    machineidlist.DataSource = objRes.DS;
                    machineidlist.DataTextField = "kiosk_id";
                    machineidlist.DataBind();
                    machineidlist.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "NA"));
                    machineid.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('Please try again after sometime as :"+objRes.strError+"')</script>");
                }
            }
            else if (filtertype.SelectedIndex == 3)
            {
                string s = "MachineIP";

                Reply objRes = new Reply();
                WebClient client1 = new WebClient();
                client1.Headers[HttpRequestHeader.ContentType] = "text/json";

                string JsonString = JsonConvert.SerializeObject(s);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result1 = client1.UploadString(URL + "/GetKioskReport", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res)
                {
                    machineiplist.Items.Clear();
                    machineiplist.DataSource = objRes.DS;
                    machineiplist.DataTextField = "kiosk_ip";
                    machineiplist.DataBind();
                    machineiplist.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "NA"));
                    machineip.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('Please try again after sometime as :" + objRes.strError + "')</script>");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Please try again after sometime as :" + ex.Message + "')</script>");
        }
    }

    protected void Modefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        pdfdiv.Visible = false;
        exdiv.Visible = false;
        Session["Data"] = null;
        Dttime.Visible = false;
        if (filtertype.SelectedIndex == 0 || filtertype.SelectedIndex == 1)
        {
            machineip.Visible = false;
            machineid.Visible = false;
        }
        if (Modefilter.SelectedIndex == 1 || Modefilter.SelectedIndex == 3 || Modefilter.SelectedIndex == 4)
            Dttime.Visible = true;
    }

    protected void search_Click(object sender, EventArgs e)
    {
        
        try
        {
            
            Session["Data"] = null;
            if (filtertype.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please select type of Search to be made')</script>");
                return;
            }
            else
            {
                string sendstring = "";
                if (Modefilter.SelectedIndex == 2)
                {
                    sendstring = "1#";
                    if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine ID')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex != 0)
                    {
                        sendstring += "ID#" + machineidlist.SelectedItem.Text;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine IP')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex != 0)
                    {
                        sendstring += "IP#" + machineiplist.SelectedItem.Text;
                    }
                    sendstring += "##";
                }
                else if (Modefilter.SelectedIndex == 1)
                {
                    sendstring = "2#";
                    string Datevalue = Request.Form["ReportTime"].ToString().Replace("/", "-");
                    string startDT = Datevalue.Substring(0, 10);
                    string endDT = Datevalue.Substring(13);
                    DateTime StartDateTime = DateTime.ParseExact(startDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime EndDateTime = DateTime.ParseExact(endDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (StartDateTime > EndDateTime)
                    {
                        Response.Write("<script>alert('Start Date cannot be greater than end Date.')</script>");
                        return;
                    }
                    if (EndDateTime > DateTime.Today)
                    {
                        Response.Write("<script>alert('End Date cannot be greater than Today.')</script>");
                        return;
                    }
                    sendstring +=  startDT + "#" + endDT;
                    if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine ID')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex != 0)
                    {
                        sendstring += "#ID#" + machineidlist.SelectedItem.Text;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine IP')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex != 0)
                    {
                        sendstring += "#IP#" + machineiplist.SelectedItem.Text;
                    }
                    sendstring += "##";
                }
                else if (Modefilter.SelectedIndex == 3)
                {
                    sendstring = "3#";
                    string Datevalue = Request.Form["ReportTime"].ToString().Replace("/", "-");
                    string startDT = Datevalue.Substring(0, 10);
                    string endDT = Datevalue.Substring(13);
                    DateTime StartDateTime = DateTime.ParseExact(startDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime EndDateTime = DateTime.ParseExact(endDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (StartDateTime > EndDateTime)
                    {
                        Response.Write("<script>alert('Start Date cannot be greater than end Date.')</script>");
                        return;
                    }
                    if (EndDateTime > DateTime.Today)
                    {
                        Response.Write("<script>alert('End Date cannot be greater than Today.')</script>");
                        return;
                    }
                    sendstring += startDT + "#" + endDT;
                    if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine ID')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex != 0)
                    {
                        sendstring += "#ID#" + machineidlist.SelectedItem.Text;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine IP')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex != 0)
                    {
                        sendstring += "#IP#" + machineiplist.SelectedItem.Text;
                    }
                    sendstring += "##";
                }
                else if (Modefilter.SelectedIndex == 4)
                {
                    sendstring = "4#";
                    string Datevalue = Request.Form["ReportTime"].ToString().Replace("/", "-");
                    string startDT = Datevalue.Substring(0, 10);
                    string endDT = Datevalue.Substring(13);
                    DateTime StartDateTime = DateTime.ParseExact(startDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime EndDateTime = DateTime.ParseExact(endDT, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (StartDateTime > EndDateTime)
                    {
                        Response.Write("<script>alert('Start Date cannot be greater than end Date.')</script>");
                        return;
                    }
                    if (EndDateTime > DateTime.Today)
                    {
                        Response.Write("<script>alert('End Date cannot be greater than Today.')</script>");
                        return;
                    }
                    sendstring += startDT + "#" + endDT;
                    if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine ID')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex != 0)
                    {
                        sendstring += "#ID#" + machineidlist.SelectedItem.Text;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex == 0)
                    {
                        Response.Write("<script>alert('Please select Machine IP')</script>");
                        return;
                    }
                    else if (filtertype.SelectedIndex == 3 && machineiplist.SelectedIndex != 0)
                    {
                        sendstring += "#IP#" + machineiplist.SelectedItem.Text;
                    }
                    sendstring += "##";
                }
                else
                {
                    Response.Write("<script>alert('Please select Valid Mode for filter')</script>");
                    return;
                }

                Reply objRes = new Reply();
                WebClient client1 = new WebClient();
                client1.Headers[HttpRequestHeader.ContentType] = "text/json";

                string JsonString = JsonConvert.SerializeObject(sendstring);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result1 = client1.UploadString(URL + "/GetKioskReport", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res)
                {
                    GV_Kiosk_Health.DataSource = objRes.DS;
                    GV_Kiosk_Health.Visible = true;
                    GV_Kiosk_Health.DataBind();
                    ErrorImg.Visible = false;
                    pdfdiv.Visible = true;
                    exdiv.Visible = true;
                    Session["Data"] = objRes.DS;
                }
                else
                {
                    GV_Kiosk_Health.Visible = false;
                    ErrorImg.Visible = true;
                }
                
            }
        }
        catch
        { }
    }

    protected void Excel_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Session["Data"] == null)
        //    {
        //        Response.Write("No data to generate Excel");
        //        return;
        //    }
        //    DataSet dataSet = (DataSet)Session["Data"];
        //    using (DataTable dt = dataSet.Tables[0])
        //    {
        //        //using (XLWorkbook wb = new XLWorkbook())
                //{
                    //wb.Worksheets.Add(dt);

                    //  var ws = wb.Worksheets.Add("TSS Report ");
                    //  var str = string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(dt.Columns.Count));
                    //  var wsReportNameHeaderRange = ws.Range("A1:G1");
                    //  wsReportNameHeaderRange.Style.Font.Bold = true;
                    //  wsReportNameHeaderRange.Style.Font.Italic = true ;
                    //  wsReportNameHeaderRange.Style.Font.FontSize = 20;
                    ////  wsReportNameHeaderRange.Style.Font.FontColor = Color.Blue;
                    //  wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //  wsReportNameHeaderRange.Merge();
                    //  wsReportNameHeaderRange.Value = "                       Terminal Secure Server";
                    //  var wsreportsecond = ws.Range("A1:G1");
                    //  wsReportNameHeaderRange.Style.Font.Bold = false;
                    //  wsreportsecond.Style.Font.Italic = true;
                    //  wsReportNameHeaderRange.Style.Font.FontSize = 10;
                    //  wsreportsecond.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //  wsreportsecond.Merge();
                    //  wsreportsecond.Value = "     Report Generated on "+DateTime.Now+" ";


                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TSS_Kiosk_Report__" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".xlsx");
                    // MergeCells mergeCells;   // Merge Cell


                    //switch (Modefilter.SelectedIndex)
                    //{
                    //    case 1:
                    //        {
                    //            Response.AddHeader("content-disposition", "attachment;filename=TSS_DeviceHealthReport" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".xlsx");
                    //            string s1 = "A1:" + A + "1";  //H1
                    //            MergeCell mergeCell = new MergeCell() { Reference = s1 };
                    //            mergeCells.Append(mergeCell);

                    //            string s2 = "A2:" + A + "2";
                    //            MergeCell mergeCell1 = new MergeCell() { Reference = s2 };
                    //            mergeCells.Append(mergeCell1);
                    //            var header0 = new Row();
                    //            {
                    //                var cell0 = new Cell { StyleIndex = 2, DataType = CellValues.String, CellValue = new CellValue(bank_name + " Terminal Secure Server - DETAILED REPORT For " + Modefilter.SelectedValue + " GENERATED ON " + DateTime.Now.ToString("dd MMM yyyy, HH:mm")) };
                    //                header0.Append(cell0);
                    //            }
                    //            wb.AppendChild(header0);
                    //        }
                    //        break;
                    //    case 2:
                    //        {
                    //            Response.AddHeader("content-disposition", "attachment;filename=TSS_DeviceHealthReport" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".xlsx");
                    //            string s1 = "A1:" + A + "1";  //H1
                    //            MergeCell mergeCell = new MergeCell() { Reference = s1 };
                    //            mergeCells.Append(mergeCell);

                    //            string s2 = "A2:" + A + "2";
                    //            MergeCell mergeCell1 = new MergeCell() { Reference = s2 };
                    //            mergeCells.Append(mergeCell1);
                    //            var header0 = new Row();
                    //            {
                    //                var cell0 = new Cell { StyleIndex = 2, DataType = CellValues.String, CellValue = new CellValue(bank_name + " Terminal Secure Server - DETAILED REPORT For " + Modefilter.SelectedValue + " GENERATED ON " + DateTime.Now.ToString("dd MMM yyyy, HH:mm")) };
                    //                header0.Append(cell0);
                    //            }
                    //            sheetData.AppendChild(header0);
                    //        }
                    //        break;
                    //}
                    //if (Modefilter.SelectedIndex == 2)
                    //{
                    //    var header1 = new Row();
                    //    {
                    //        var cell1 = new Cell { StyleIndex = 1, DataType = CellValues.String, CellValue = new CellValue("Time Base Access REPORT FROM " + Request.Form["ReportTime"].ToString().Substring(0, 10) + " TO " + Request.Form["ReportTime"].ToString().Substring(13)) };
                    //        header1.Append(cell1);
                    //    }
                    //    sheetData.AppendChild(header1);
                    //}
        //            wb.Worksheets.Add(dt);
        //            using (MemoryStream MyMemoryStream = new MemoryStream())
        //            {
        //                wb.SaveAs(MyMemoryStream);
        //                MyMemoryStream.WriteTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{ }
    }
    public static bool IsDouble(string s)
    {
        double dOutput = 0;
        if (Double.TryParse(s, out dOutput))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //private static Column CreateColumnData(UInt32 StartColumnIndex, UInt32 EndColumnIndex, double ColumnWidth)   // getting width of column
    //{
    //    Column column;
    //    column = new Column();
    //    column.Min = StartColumnIndex;
    //    column.Max = EndColumnIndex;
    //    column.Width = ColumnWidth;
    //    column.CustomWidth = true;
    //    return column;
    //}
        protected void PDF_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Data"] == null)
            {
                Response.Write("No data to generate Excel");
                return;
            }
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Cash_Recent_Health_Report_" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".xlsx");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            MemoryStream memoryStream = new MemoryStream();

            //creating pdf document      
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 5f, 5f, 10f, 5f);
            // getting writer for pdf
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            PdfWriter.GetInstance(pdfDoc, memoryStream);

            Paragraph para = new Paragraph();
            Paragraph para1 = new Paragraph();
            Paragraph para2 = new Paragraph();
            para.Alignment = 1;
            para.Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 30f, BaseColor.BLACK);

            para.SpacingBefore = 50;
            para.SpacingAfter = 50;
            para.Add("Terminal Secure Server");
            DataSet dataSet = (DataSet)Session["Data"];

            PdfPTable table = new PdfPTable(dataSet.Tables[0].Columns.Count);  // -2 new
            //spacing before and after table
            table.TotalWidth = 823f;
            table.LockedWidth = true;
            table.SpacingBefore = 5f;
            table.SpacingAfter = 5f;
            table.HorizontalAlignment = 0;

            PdfPCell cell = new PdfPCell(new Phrase());
            int columnscount = dataSet.Tables[0].Columns.Count;

            string header1 = null;
            string header2 = null;

            header1 = " Terminal Secure Server -Device REPORT GENERATED ON " + DateTime.Now.ToString("dd MMM yyyy, HH:mm tt");
            header2 = "REPORT";
            Response.AddHeader("content-disposition", "attachment;filename=TSS_Device_Report_"+Modefilter.SelectedValue+"_" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".pdf");
            table.AddCell(GetCell(header1, columnscount, 1));
            table.AddCell(GetCell(header2, columnscount, 2));

            for (int j = 1; j <= columnscount; j++)
            {
                table.AddCell(GetCell(dataSet.Tables[0].Columns[j - 1].ColumnName.ToString(), 3));  //
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                //char b = 'A';
                //string str = b + "" + k;
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    table.AddCell(GetCell(row[i].ToString(), 4));
                }
            }
            string imageURL = Server.MapPath(".") + "\\images\\LipiBlue.png";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

            jpg.ScaleToFit(140f, 120f);
            jpg.SpacingBefore = 10f;
            jpg.SpacingAfter = 1f;
            jpg.Alignment = Element.ALIGN_MIDDLE;



            pdfDoc.Open();
            pdfDoc.Add(para);
            pdfDoc.Add(jpg);
            pdfDoc.Add(table);


            pdfDoc.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    string password = "pass@123";
                    PdfReader reader = new PdfReader(input);
                    PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                    bytes = output.ToArray();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    //Response.Write(pdfDoc);
                    //Response.End();
                }
            }
        }
        catch (Exception ex)
        { }
    }
    private PdfPCell GetCell(string text, int i)
    {
        return GetCell(text, 1, i);
    }
    private PdfPCell GetCell(string text, int colSpan, int i)
    {
        var whitefont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 14, BaseColor.BLACK);//"Times New Roman"
        var blackfont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 14, BaseColor.BLACK);//"Times New Roman"

        if (i < 3)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, whitefont));
            cell.HorizontalAlignment = 1;
            //cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            //Header colour
            if (i == 1 || i == 2)
            {
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            }
            //column name colour
            if (i == 3)
                cell.BackgroundColor = BaseColor.CYAN;
            return cell;
        }
        else if (i == 3)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, blackfont));
            cell.HorizontalAlignment = 1;
            //cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            //Header colour
            if (i == 1 || i == 2)
            {
                cell.BackgroundColor = BaseColor.BLUE;
            }
            //column name colour
            if (i == 3)
                cell.BackgroundColor = BaseColor.CYAN;
            return cell;
        }
        else
        {
            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.HorizontalAlignment = 1;
            //cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            //Header colour
            if (i == 1 || i == 2)
            {
                cell.BackgroundColor = BaseColor.BLUE;
            }
            //column name colour
            if (i == 3)
                cell.BackgroundColor = BaseColor.CYAN;
            string value = text.ToLower();
           
            return cell;
        }
    }
    //private static double GetWidth(string font, int fontSize, string text)  // getting width of column
    //{
    //    //System.Drawing.Font stringFont = new System.Drawing.Font(font, fontSize);
    //    //return GetWidth(stringFont, text);
    //}

    //private static double GetWidth(System.Drawing.Font stringFont, string text)   // getting width of column
    //{
    //    // This formula is based on this article plus a nudge ( + 0.2M )
    //    // http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column.width.aspx
    //    // Truncate(((256 * Solve_For_This + Truncate(128 / 7)) / 256) * 7) = DeterminePixelsOfString

    //    Size textSize = System.Windows.Forms.TextRenderer.MeasureText(text, stringFont);  // using winfroms
    //    double width = (double)(((textSize.Width / (double)7) * 256) - (128 / 7)) / 256;
    //    width = (double)decimal.Round((decimal)width + 0.2M, 2);
    //    return width;
    //}

}