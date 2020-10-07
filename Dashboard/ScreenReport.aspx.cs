using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ScreenReport : System.Web.UI.Page
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
        }
    }

    protected void filtertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
        machineid.Visible = false;
        machineip.Visible = false;
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

            string result1 = client1.UploadString(URL + "/GetKioskReport", "POST", dataEncrypted);

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

            string result1 = client1.UploadString(URL + "/GetKioskReport", "POST", dataEncrypted);

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
    }

    protected void machineidlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ErrorImg.Visible = false;
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
            
            WebClient client1 = new WebClient();
            client1.Headers[HttpRequestHeader.ContentType] = "text/json";

            string JsonString = JsonConvert.SerializeObject(sendString);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);


            string result1 = client1.UploadString(URL + "/GetScreenData", "POST", dataEncrypted);
            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result1);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            string objRes = json.Deserialize<string>(reader);
            if (objRes != "fail")
            {
                byte[] vs = Convert.FromBase64String(objRes);
                Response.Clear();
                // for the browser's download dialog
                Response.AddHeader("Content-Disposition",
                                   "attachment; filename=Screencapture"+DateTime.Now+".zip");
                // Add a HTTP header to the output stream that contains the 
                Response.AddHeader("Content-Length",
                                   vs.Length.ToString());
                // Set the HTTP MIME type of the output stream
                Response.ContentType = "application/zip";
                // Write the data out to the client.
                Response.BinaryWrite(vs);
            }
            else
            { ErrorImg.Visible = true; }
        }
    }
}