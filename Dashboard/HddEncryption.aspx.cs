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
            bindTemplateName();
            bindKioskDetails();
        }
    }

    public void bindTemplateName()
    {
        try
        {
            tempList.Items.Insert(0, "Select Type");
            tempList.Items.Insert(1, "HDDEncryption");
           
        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
        }
    }

    protected void Broadcast_Click(object sender, EventArgs e)
    {
        CommandIniUpdate objReq = new CommandIniUpdate();

        if (tempList.SelectedValue.ToLower() == "hddencryption")
        {
            objReq.CommandCount = "3";
            objReq.Command = "securityupdate#hddencryption#" + list1.SelectedValue.ToLower();
        }
       


        objReq.KioskIP = new string[0];
        objReq.MachineSrNo = new string[0];
        int newSize = 0;
        bool okey = false;
        for (int i = 0; i < GV_Kiosk_Details.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)GV_Kiosk_Details.Rows[i].Cells[0].FindControl("cbSelect");//Gets the

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
                
                string result = client.UploadString(URL + "/CommandIniUpdate", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                result = json.Deserialize<string>(reader);

                if (result.ToLower().Contains("true"))
                {
                    //Data Source
                    Response.Write("<script type='text/javascript'>alert('Ini Updated Succesfully')</script>");
                    bindKioskDetails();
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('" + objRes.strError + "')</script>");
                }
            }

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
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

    protected void tempList_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (tempList.SelectedValue.ToLower() == "hddencryption")
        {
            div1.Visible = true;
            div3.Visible = false;
            div4.Visible = false;
            list1.Items.Clear();
            list1.Items.Insert(0, "Select Type");
            list1.Items.Insert(1, "Enable");
            list1.Items.Insert(2, "Disable");
        }
        else if (tempList.SelectedValue.ToLower() == "select type")
        {

            list1.Items.Clear();
        }

    }

    protected void list1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (list1.SelectedIndex != 0 && tempList.SelectedValue.ToLower() == "logs")
        //    div3.Visible = true;
        //if (list1.SelectedIndex != 0 && tempList.SelectedValue.ToLower() == "custom command")
        //    div4.Visible = true;
    }
}