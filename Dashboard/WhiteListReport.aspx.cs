
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_WhiteListReport : System.Web.UI.Page
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
            Session["Data"] = null;
        }
    }

    protected void filtertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        Session["Data"] = null;
        machineid.Visible = false;
        machineip.Visible = false;
        exdiv.Visible = false;
        pdfdiv.Visible = false;
        if (filtertype.SelectedIndex == 0)
        {
            return;
        }
        else if (filtertype.SelectedIndex == 1)
        {
            string s = "MachineIP";

            Reply objRes = new Reply();
            WebClient client1 = new WebClient();
            client1.Headers[HttpRequestHeader.ContentType] = "text/json";

            string JsonString = JsonConvert.SerializeObject(s);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result1 = client1.UploadString(URL + "/GetKioskReportWhiteList", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
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

            string result1 = client1.UploadString(URL + "/GetKioskReportWhiteList", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
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
                Response.Write("<script>alert('Please try again after sometime as :" + objRes.strError + "')</script>");
            }
        }
    }

    protected void machineiplist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        Session["Data"] = null;
        exdiv.Visible = false;
        pdfdiv.Visible = false;
    }

    protected void machineidlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        GV_Kiosk_Health.Visible = false;
        Session["Data"] = null;
        exdiv.Visible = false;
        pdfdiv.Visible = false;
    }

    protected void search_Click(object sender, EventArgs e)
    {
        if (filtertype.SelectedIndex == 0)
        {
            Response.Write("<script>alert('Please Select Filter Type')</script>");
            return;
        }
        else
        {
            if (filtertype.SelectedIndex == 1 && machineiplist.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please Select Kiosk IP')</script>");
                return;
            }
            if (filtertype.SelectedIndex == 2 && machineidlist.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please Select Kiosk ID')</script>");
                return;
            }
            string sendString;
            if (filtertype.SelectedIndex == 1)
            {
                sendString = "1#" + machineiplist.SelectedValue;
            }
            else
            {
                sendString = "2#" + machineidlist.SelectedValue;
            }
            Reply objRes = new Reply();
            WebClient client1 = new WebClient();
            client1.Headers[HttpRequestHeader.ContentType] = "text/json";

            string JsonString = JsonConvert.SerializeObject(sendString);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result1 = client1.UploadString(URL + "/GetKioskReportWhiteList", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
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

    protected void Excel_Click(object sender, EventArgs e)
    {
      
    }

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
            string iii = "";
            if (filtertype.SelectedIndex == 1)
            {
                iii = machineiplist.SelectedValue;
            }
            else
            {
                iii = machineidlist.SelectedValue;
            }
            Response.AddHeader("content-disposition", "attachment;filename=TSS_WhiteList_Report_"+iii + "_" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".pdf");
            table.AddCell(GetCell(header1, columnscount, 1));
            table.AddCell(GetCell(header2, columnscount, 2));

            for (int j = 1; j <= columnscount; j++)
            {
                table.AddCell(GetCell(dataSet.Tables[0].Columns[j - 1].ColumnName.ToString(), 3));  //
            }
            for(int k=0;k<dataSet.Tables[0].Rows.Count;k++)
            { DataRow row = dataSet.Tables[0].Rows[k];
            
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
            if (value == "allow")
                cell.BackgroundColor = BaseColor.GREEN;
            else if (value == "not allow")
                cell.BackgroundColor = BaseColor.RED;
            return cell;
        }
    }
}