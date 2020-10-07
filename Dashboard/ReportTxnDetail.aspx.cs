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

public partial class Dashboard_ReportTxnDetail : System.Web.UI.Page
{
    private DataSet objds;
    public static Reply objGlobal = new Reply();
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
            bindTemplateName();
            string strRole = Session["Role"].ToString();
            if (Session["Location"] != null && !strRole.ToLower().Contains("administrator"))
            {
                String loc = Session["Location"].ToString();
                bindKioskTxnWithLocation(loc);
                filter.Visible = false;  //For User With Desired Location
            }
            else
                bindKioskHealth();
           
        }
    }

    private void bindKioskTxnWithLocation(string loc)
    {
        try
        {
            int connected = 0;
            int Disconnected = 0;


            if (objds == null)
                objds = new DataSet();

            Reply objRes = new Reply();
            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                
                string JsonString = JsonConvert.SerializeObject("Location#" + loc);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);
               

                string result = client.UploadString(URL + "/GetTxnDetails", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res == true)
                {
                    //Data Source

                    objGlobal.DS = objRes.DS;
                    //for(int i=0; i< objRes.DS.Tables[0].Rows.Count;++i)
                    //{
                    //   if("connected"== objRes.DS.Tables[0].Rows[i]["D1"].ToString().ToLower())
                    //    {
                    //        ++CashConnected;
                    //    }
                    //   else
                    //    {
                    //        ++CashDisconnected;
                    //    }
                    //    if ("connected" == objRes.DS.Tables[0].Rows[i]["D2"].ToString().ToLower())
                    //    {
                    //        ++RecConnected;
                    //    }
                    //    else
                    //    {
                    //        ++RecDisconnected;
                    //    }

                    //}
                    //cashacceptor.InnerText = CashConnected + "/" + CashDisconnected;
                    //recieptprinter.InnerText = RecConnected + "/" + RecDisconnected;

                    //CashDisconnected = CashConnected = RecDisconnected = RecConnected = 0;
                    ErrorImg.Visible = false;
                    GV_Kiosk_Health.DataSource = objRes.DS;
                    GV_Kiosk_Health.DataBind();

                    for (int i = 1; i <= objRes.DeviceCount; ++i)
                    {
                        switch (i)
                        {
                            case 1:
                                div1.Visible = true;
                                l1.Visible = true;

                                break;
                            case 2:
                                l2.Visible = true;
                                break;
                            case 3:
                                l3.Visible = true;
                                break;
                            case 4:
                                l4.Visible = true;
                                break;
                            case 5:
                                div2.Visible = true;
                                l5.Visible = true;
                                break;
                            case 6:
                                l6.Visible = true;
                                break;
                            case 7:
                                l7.Visible = true;
                                break;
                            case 8:
                                l8.Visible = true;
                                break;
                            case 9:
                                div3.Visible = true;
                                l9.Visible = true;
                                break;
                            case 10:
                                l10.Visible = true;
                                break;
                            case 11:
                                l11.Visible = true;
                                break;
                            case 12:
                                l12.Visible = true;
                                break;
                            default:

                                break;
                        }
                    }


                    //for (int i = 0; i < GV_Kiosk_Health.Rows.Count; i++)
                    //{
                    //    if (Convert.ToDateTime(GV_Kiosk_Health.Rows[i].Cells[15].Text) < DateTime.Now.AddMinutes(-120))
                    //    {
                    //        GV_Kiosk_Health.Rows[i].Cells[3].Controls.Clear();
                    //        System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                    //        img1.ImageUrl = "images/cross.png";
                    //        GV_Kiosk_Health.Rows[i].Cells[3].Controls.Add(img1);
                    //        img1.ToolTip = "Disconnected";
                    //        Disconnected++;
                    //    }
                    //    else
                    //    {
                    //        GV_Kiosk_Health.Rows[i].Cells[13].Controls.Clear();
                    //        System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                    //        img1.ImageUrl = "images/right.png";
                    //        GV_Kiosk_Health.Rows[i].Cells[3].Controls.Add(img1);
                    //        img1.ToolTip = "Connected";
                    //        connected++;
                    //    }


                    //}

                    //lblTotalConnected.Text = connected.ToString();
                    //lblTotalDisConnected.Text = Disconnected.ToString();

                }
                else
                {
                    // lblTotalRows.Text = "0";
                    ErrorImg.Visible = true;
                }
            }

        }
        catch (Exception excp)
        {
            // Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");

            Response.Write("<script type='text/javascript'>alert( 'Service Error Occured Machine Not Connected:-" + excp.Message + "' )</script>");
            //lblTotalConnected.Text = "0";
            //lblTotalDisConnected.Text = "0";
        }
    }

    public void bindKioskHealth()
    {
        try
        {
            int connected = 0;
            int Disconnected = 0;


            if (objds == null)
                objds = new DataSet();

            Reply objRes = new Reply();
            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                //URL = "http://localhost:50462/Service1.svc";
                string JsonString = JsonConvert.SerializeObject("KioskList");
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetTxnDetails", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res == true)
                {
                    ErrorImg.Visible = false;
                    GV_Kiosk_Health.Visible = true;
                    objGlobal.DS = objRes.DS;
                    GV_Kiosk_Health.DataSource = objRes.DS;
                    GV_Kiosk_Health.DataBind();
                }
                else
                {
                    ErrorImg.Visible = true;
                }
            }

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'Service Error Occured Machine Not Connected:-" + excp.Message + "' )</script>");
        }


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
            if (value == "disconnected" || value.ToLower().Contains("jam") || value.ToLower().Contains("low") || value.ToLower().Contains("out") || value.ToLower().Contains("printererror") || value == "faulty")
                cell.BackgroundColor = BaseColor.RED;
            return cell;
        }
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        try
        {
            //UpdateGrid();
            GV_Kiosk_Health.DataSource = objGlobal.DS;
            GV_Kiosk_Health.DataBind();
            ////
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Cash_Recent_Health_Report_" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".xlsx");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //creating pdf document      
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 5f, 5f, 10f, 5f);
            // getting writer for pdf
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            Paragraph para = new Paragraph();
            Paragraph para1 = new Paragraph();
            Paragraph para2 = new Paragraph();
            para.Alignment = 1;
            para.Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 30f, BaseColor.BLACK);

            para.SpacingBefore = 50;
            para.SpacingAfter = 50;
            para.Add("PCMC");

            PdfPTable table = new PdfPTable(objGlobal.DS.Tables[0].Columns.Count);  // -2 new
            //spacing before and after table
            table.TotalWidth = 823f;
            table.LockedWidth = true;
            table.SpacingBefore = 5f;
            table.SpacingAfter = 5f;
            table.HorizontalAlignment = 0;


            PdfPCell cell = new PdfPCell(new Phrase());
            int columnscount = objGlobal.DS.Tables[0].Columns.Count;
            string login_ini_path = HttpRuntime.AppDomainAppPath + "Configuration\\machines_type.ini";

            string bank = null;
            string header1 = null;
            string header2 = null;

            header1 = bank + " PCMC -TRANSACTION REPORT GENERATED ON " + DateTime.Now.ToString("dd MMM yyyy, HH:mm tt");
            header2 = "REPORT";
            Response.AddHeader("content-disposition", "attachment;filename=PCMC_Transaction_Report_" + DateTime.Now.ToString("dd-MM-yy_HH:mm") + ".pdf");

            table.AddCell(GetCell(header1, columnscount, 1));
            table.AddCell(GetCell(header2, columnscount, 2));


            for (int j = 1; j <= columnscount; j++)
            {
                table.AddCell(GetCell(objGlobal.DS.Tables[0].Columns[j - 1].ColumnName.ToString(), 3));  //
            }

            foreach (DataRow row in objGlobal.DS.Tables[0].Rows)
            {
                for (int i = 0; i < objGlobal.DS.Tables[0].Columns.Count; i++)
                {
                    table.AddCell(GetCell(row[i].ToString(), 4));
                }
            }
            string imageURL = Server.MapPath(".") + "\\images\\logo.png";
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
            Response.Write(pdfDoc);
            Response.End();
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    public void bindTemplateName()
    {
        try
        {
            filterlist.Items.Insert(0, "Select Filteration");
            filterlist.Items.Insert(1, "All");
            filterlist.Items.Insert(2, "Location");
            filterlist.Items.Insert(2, "Kiosk ID");

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
        }
    }
    protected void filterlist_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (filterlist.SelectedValue.ToLower() == "location")
        {
            location.Visible = true;
            lbllocation.Text = "Location";
            Reply objRes = new Reply();
            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                string JsonString = JsonConvert.SerializeObject("Location");
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetLocation", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);
                if (objRes.res == true)
                {

                    locationlist.Items.Clear();
                    locationlist.Items.Add("Select Location");

                    //Data Source
                    for (int i = 0; i < objRes.DS.Tables[0].Rows.Count; i++)
                    {
                        locationlist.Items.Add(objRes.DS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else
                {
                    Response.Write("<script>alert('" + objRes.strError + "')</script>");

                }
            }

            bindKioskHealth();
        }

        else if (filterlist.SelectedValue.ToLower() == "select filteration")
        {
            location.Visible = false;
            bindKioskHealth();
        }
        else if (filterlist.SelectedValue.ToLower() == "all")
        {
            location.Visible = false;
            bindKioskHealth();
        }
        else if (filterlist.SelectedValue.ToLower() == "kiosk id")
        {
            location.Visible = true;
            lbllocation.Text = "Kiosk Id";
            Reply objRes = new Reply();
            //send Request

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                string JsonString = JsonConvert.SerializeObject("kiosk id");
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetLocation", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result); 
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res == true)
                {
                    locationlist.Items.Clear();
                    locationlist.Items.Add("Select Kiosk Id");
                    //Data Source
                    for (int i = 0; i < objRes.DS.Tables[0].Rows.Count; i++)
                    {
                        locationlist.Items.Add(objRes.DS.Tables[0].Rows[i][0].ToString());
                    }

                }
                else
                {
                    Response.Write("<script>alert('" + objRes.strError + "')</script>");
                }


            }


        }
        else
        {
            bindKioskHealth();
        }

    }
    protected void locationlist_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (locationlist.SelectedValue.ToLower() == "select location")
        {
            bindKioskHealth();
        }

        else if (filterlist.SelectedValue.ToLower() == "location")
        {
            String s = "Location#" + locationlist.SelectedValue + "#";
            Reply objRes = new Reply();
            WebClient client1 = new WebClient();
            client1.Headers[HttpRequestHeader.ContentType] = "text/json";

            string JsonString = JsonConvert.SerializeObject(s);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result1 = client1.UploadString(URL + "/GetTxnDetails", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            objRes = json.Deserialize<Reply>(reader);
            if (objRes.res == true)
            {
                ErrorImg.Visible = false;
                objGlobal.DS = objRes.DS;
                GV_Kiosk_Health.DataSource = objRes.DS;
                GV_Kiosk_Health.DataBind();
                GV_Kiosk_Health.Visible = true;
            }
            else
            {
                ErrorImg.Visible = true;
                GV_Kiosk_Health.Visible = false;
                locationlist.SelectedIndex = 0;

            }
        }
        else
        {
            string s = "kioskid#" + locationlist.SelectedValue + "#";

            Reply objRes = new Reply();
            WebClient client2 = new WebClient();

            client2.Headers[HttpRequestHeader.ContentType] = "text/json";
            string JsonString = JsonConvert.SerializeObject(s);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result = client2.UploadString(URL + "/GetTxnDetails", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            objRes = json.Deserialize<Reply>(reader);
            if (objRes.res == true)
            {
                ErrorImg.Visible = false;
                objGlobal.DS = objRes.DS;
                GV_Kiosk_Health.DataSource = objRes.DS;
                GV_Kiosk_Health.DataBind();
                GV_Kiosk_Health.Visible = true;

            }
            else
            {
                ErrorImg.Visible = true;
                GV_Kiosk_Health.Visible = false;
                locationlist.SelectedIndex = 0;

            }


        }
    }
}