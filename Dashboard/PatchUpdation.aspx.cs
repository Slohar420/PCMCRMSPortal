using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

public partial class Dashboard_BroadcastTemplate : System.Web.UI.Page
{

    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    public static DataSet objds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        if (!IsPostBack)
        {
            // bindTemplateName();

            bindKioskDetails();
        }
    }


    public void bindKioskDetails()
    {
        try
        {
            if (objds == null)
                objds = new DataSet();

            Reply objRes = new Reply();
            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";

                string JsonString = JsonConvert.SerializeObject("KioskList");
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetKioskMasterList", "POST", dataEncrypted);

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
                    GV_Kiosk_Details.DataSource = objRes.DS;
                    GV_Kiosk_Details.DataBind();
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('" + objRes.strError + "')</script>");
                }
            }
        }
        catch (Exception excp)
        {

        }
    }

    protected void Click_Function(object sender, EventArgs e)
    {
        Button b1 = sender as Button;
        String s = b1.Text;
        
        string[] validFileTypes = { "zip", "rar"};
        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }

        if (!isValidFile)
        {
            Response.Write("<script type='text/javascript'>alert( 'Zip File Are Only Allowed! Please Upload A valid File!' )</script>");
        }
        else
        {
            if (FileUpload1.HasFile)
            {
                PatchUpdateINI objReq = new PatchUpdateINI();
                // string s = Server.MapPath(FileUpload1.FileName);
                byte[] b = FileUpload1.FileBytes;
                if (s.ToLower() == "upload")
                {
                    objReq.Instant = false;
                }
                else if (s.ToLower() == "instant upload")
                {
                    objReq.Instant = true;
                }

                objReq.KioskIP = new string[0];
                objReq.PatchName = FileUpload1.FileName;
                objReq.patch = Convert.ToBase64String(b);
                int newSize = 0;
                bool okey = false;
                for (int i = 0; i < GV_Kiosk_Details.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GV_Kiosk_Details.Rows[i].Cells[1].FindControl("cbSelect");//Gets the

                    if (cb.Checked == true)
                    {
                        Array.Resize(ref objReq.KioskIP, newSize + 1);
                        Array.Resize(ref objReq.MachineSrNo, newSize + 1);
                        // how to get those select values and how store those values in array
                        objReq.KioskIP[newSize] = GV_Kiosk_Details.Rows[i].Cells[2].Text;
                        objReq.MachineSrNo[newSize] = GV_Kiosk_Details.Rows[i].Cells[5].Text;
                        newSize++;
                        okey = true;

                    }
                    else
                    { //Do something here when CheckBox is UnChecked }
                    }
                }
                if (okey == false)
                {
                    Response.Write("<script type='text/javascript'>alert( 'Select Atleast one option' )</script>");
                    return;
                }

                try
                {
                    if (objds == null)
                        objds = new DataSet();

                    Reply objRes = new Reply();

                    // send request
                    using (WebClient client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "text/json";

                        string JsonString = JsonConvert.SerializeObject(objReq);
                        EncRequest objEncRequest = new EncRequest();
                        objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                        string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                        string result = client.UploadString(URL + "/PatchSave", "POST", dataEncrypted);

                        EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                        objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                        Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                        json.NullValueHandling = NullValueHandling.Ignore;
                        StringReader sr = new StringReader(objResponse.ResponseData);
                        Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                        result = json.Deserialize<string>(reader);
                        
                        if (result.ToLower().Contains("client"))
                        {
                            //Data Source
                            Response.Write("<script type='text/javascript'>alert('ClientPatch Updated SuccessFully')</script>");
                            bindKioskDetails();

                        }
                        else if (result.ToLower().Contains("monitor"))
                        {
                            Response.Write("<script type='text/javascript'>alert('MonitorPatch Updated SuccessFully')</script>");
                            bindKioskDetails();
                        }
                        else if (result.ToLower().Contains("lipirdservice"))
                        {
                            Response.Write("<script type='text/javascript'>alert('LipiRDService Updated SuccessFully')</script>");
                            bindKioskDetails();
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert('Error Occurs While Updation ')</script>");
                        }
                    }
                }
                catch (Exception excp)
                {
                    Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
                }
            }
            else
                Response.Write("<script type='text/javascript'>alert( 'Please Upload The File First' )</script>");
        }
    }

    protected void GV_Kiosk_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView grid = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "")
                    {
                        e.Row.Cells[i].Text = "N/A";
                    }
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}