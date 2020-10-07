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

public partial class Dashboard_Popup : System.Web.UI.Page
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    string[] strMsgDevice = new string[0];
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = Request.QueryString["ip"].ToString();
        bindKioskHealth(s);
    }
    public void bindKioskHealth(string s)
    {
        try
        {
            DataSet objds = new DataSet();

            Reply objRes = new Reply();
            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                //URL = "http://localhost:50462/Service1.svc";
                string JsonString = JsonConvert.SerializeObject(s);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);
                string result = client.UploadString(URL + "/GetKioskHealth", "POST", dataEncrypted);

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
                    kiosk_ip.InnerText = objRes.DS.Tables[0].Rows[0]["Kiosk IP"].ToString();
                    kiosk_id.InnerText = objRes.DS.Tables[0].Rows[0]["Kiosk ID"].ToString();

                    cash.InnerText = objRes.DS.Tables[0].Rows[0]["D1"].ToString();
                    reciept.InnerText = objRes.DS.Tables[0].Rows[0]["D2"].ToString();
                    barcode.InnerText = objRes.DS.Tables[0].Rows[0]["D3"].ToString();
                    doc.InnerText = objRes.DS.Tables[0].Rows[0]["D4"].ToString();
                    camera.InnerText = objRes.DS.Tables[0].Rows[0]["D5"].ToString();
                    vccamera.InnerText = objRes.DS.Tables[0].Rows[0]["D6"].ToString();
                    cardreader.InnerText = objRes.DS.Tables[0].Rows[0]["D7"].ToString();
                    laserprinter.InnerText = objRes.DS.Tables[0].Rows[0]["D8"].ToString();
                    fingurescanner.InnerText = objRes.DS.Tables[0].Rows[0]["D9"].ToString();
                    keypadmouse.InnerText = objRes.DS.Tables[0].Rows[0]["D10"].ToString();
                    signagetv.InnerText = objRes.DS.Tables[0].Rows[0]["D11"].ToString();
                    touchscreen.InnerText = objRes.DS.Tables[0].Rows[0]["D12"].ToString();
                }
                else
                {   
                    Response.Write("<script type='text/javascript'>alert('" + objRes.strError + "')</script>");
                }
            }

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'Service Error Occured Machine Not Connected:-" + excp.Message + "' )</script>");
        }


    }

    protected void btnCallLog__Click(object sender, EventArgs e)
    {

    }

    protected void btnSendMessage_Click(object sender, EventArgs e)
    {
        Array.Clear(strMsgDevice, 0, strMsgDevice.Length);
        string strDeviceDiconnected = "";

        if (cash.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Cash Depositor";
        }

        if (reciept.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Receipt Printer";
        }

        if (barcode.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Barcode";
        }

        if (doc.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "DocScanner";
        }

        if (camera.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Txn Camera";
        }

        if (vccamera.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "VC Camera";
        }

        if (cardreader.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Card Reader";
        }

        if (laserprinter.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Laser Printer";
        }

        if (fingurescanner.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Fingure Scanner";
        }

        if (keypadmouse.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "KeyPad/Mouse";
        }

        if (signagetv.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Signage TV";
        }

        if (touchscreen.InnerText == "Disconnected")
        {
            Array.Resize(ref strMsgDevice, strMsgDevice.Length + 1);
            strMsgDevice[strMsgDevice.Length - 1] = "Touch Screen";
        }

        foreach(string strDevice in strMsgDevice)
           strDeviceDiconnected += strDevice + ",";


        if (strDeviceDiconnected != "")
        {
            SmsManager.SmsManager.SendMessage("9587805002", strDeviceDiconnected.Substring(0, strDeviceDiconnected.Length - 1) + " is not working at delhi gate udaipur for Kiosk ID- " + kiosk_id.InnerText);
            Response.Write("<script type='text/javascript'>alert('SMS send successfully')</script>");
        }
    }
}