using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
public partial class PieChart : System.Web.UI.Page
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
           // bindLocationddl();
            chart1();            
            chart4();
           
            chart6();
            chart7();
        }
        

    }


    private void chart1()
    {
        using (WebClient client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "text/json";
            //URL = "http://localhost:50463/Service1.svc";

            string nowdatetime = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss tt");
            string strQuery = "SELECT COUNT(case when client_status_with_rms = 'Connected' then client_status_with_rms end) as Connected, COUNT(case when client_status_with_rms = 'Disconnected' then client_status_with_rms end) as Disconnected FROM pcmc.health_recent where cast(date_time as datetime)>'"+nowdatetime+"' ";

            string JsonString = JsonConvert.SerializeObject(strQuery);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            Reply objReply = json.Deserialize<Reply>(reader);
           

            //Chart1 Start
            string[] seriesArray = {"Connected","Disconnected"};
            int[] pointsArray = new int[2];

            if (objReply.res && objReply.DS != null)
            {
                pointsArray[0] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][0]);
                pointsArray[1] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][1]);
            }

            Title title = Chart1.Titles.Add("Title1");
            title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            Chart1.Titles["Title1"].Text = "Machine With RMS";
            Chart1.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);
            Chart1.Series["Series1"].Points[0].Color = Color.Green;
            Chart1.Series["Series1"].Points[1].Color = Color.Red;

            CalloutAnnotation objAnnotation = new CalloutAnnotation();
            objAnnotation.AnchorDataPoint = Chart1.Series["Series1"].Points[0];
            objAnnotation.Text = pointsArray[0].ToString();
            objAnnotation.Visible = true;
            Chart1.Annotations.Add(objAnnotation);

            CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
            objAnnotation1.AnchorDataPoint = Chart1.Series["Series1"].Points[1];
            objAnnotation1.Text = pointsArray[1].ToString();
            objAnnotation1.Visible = true;
            Chart1.Annotations.Add(objAnnotation1);

            seriesArray = null;
            pointsArray = null;
            objAnnotation = null;
            objAnnotation1 = null;
        }
    }

    private void chart2()
    {
        //Chart2 Start
        string[] seriesArray = { "Card", "Cash" };
        int[] pointsArray = new int[2];

        string strQuery = "SELECT COUNT(case when [Status] = '000' then [Status] end) as Successful,COUNT(case when [Status] != '000' then [Status] end)as Unsuccessful FROM [kmsdbv9.1].[dbo].[cashtxndetails] where [TxnDate] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and [TxnDate] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";

        pointsArray[0] = Convert.ToInt32("65");
        pointsArray[1] = Convert.ToInt32("20");

        Title title = Chart2.Titles.Add("Title1");
        title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
        Chart2.Titles["Title1"].Text = "Payment Mode With Machine";
        Chart2.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);
        Chart2.Series["Series1"].Points[0].Color = Color.Green;
        Chart2.Series["Series1"].Points[1].Color = Color.Red;

        CalloutAnnotation objAnnotation = new CalloutAnnotation();

        objAnnotation.AnchorDataPoint = Chart2.Series["Series1"].Points[0];
        objAnnotation.Text = pointsArray[0].ToString();
        objAnnotation.Visible = true;
        Chart2.Annotations.Add(objAnnotation);

        CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
        objAnnotation1.AnchorDataPoint = Chart2.Series["Series1"].Points[1];
        objAnnotation1.Text = pointsArray[1].ToString();
        objAnnotation1.Visible = true;
        Chart2.Annotations.Add(objAnnotation1);

        objAnnotation = null;
        objAnnotation1 = null;
    }

    private void chart3()
    {
        using (WebClient client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "text/json";
            // URL = "http://localhost:50463/Service1.svc";

            DateTime objTxnDtTime = DateTime.Today;

            string strQuery = "SELECT SUM(bill_payemnt_txn) as total_bill_payemnt_txn, SUM(birth_certificate_txn) as total_birth_certificate_txn, SUM(death_certificate_txn) as total_death_certificate_txn FROM seva_sindhu_" + objTxnDtTime.ToString("yy") + ".txn_detail_" + objTxnDtTime.ToString("MMM") + " where txn_date_time >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and txn_date_time <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";

            string JsonString = JsonConvert.SerializeObject(strQuery);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            Reply objReply = json.Deserialize<Reply>(reader);

            //Chart3 Start
            string[] seriesArray = { "Bill Payment", "Birth Certificate", "Death Certificate" };
            int[] pointsArray = new int[3];

            if (objReply.res && objReply.DS != null)
            {
                pointsArray[0] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][0].ToString() != "" ? objReply.DS.Tables[0].Rows[0][0].ToString() : "0");
                pointsArray[1] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][1].ToString() != "" ? objReply.DS.Tables[0].Rows[0][1].ToString() : "0");
                pointsArray[2] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][2].ToString() != "" ? objReply.DS.Tables[0].Rows[0][2].ToString() : "0");
            }

            Title title = Chart3.Titles.Add("Title1");
            title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            Chart3.Titles["Title1"].Text = "Transaction Hitting On Service Type";
            Chart3.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);

            Chart3.Series["Series1"].Points[0].Color = Color.LightBlue;
            Chart3.Series["Series1"].Points[1].Color = Color.LightGreen;
            Chart3.Series["Series1"].Points[2].Color = Color.LightYellow;
            Chart3.Series["Series1"].ChartType = SeriesChartType.Pie;

            CalloutAnnotation objAnnotation = new CalloutAnnotation();
            objAnnotation.AnchorDataPoint = Chart3.Series["Series1"].Points[0];
            objAnnotation.Text = pointsArray[0].ToString();
            objAnnotation.Visible = true;
            Chart3.Annotations.Add(objAnnotation);

            CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
            objAnnotation1.AnchorDataPoint = Chart3.Series["Series1"].Points[1];
            objAnnotation1.Text = pointsArray[1].ToString();
            objAnnotation1.Visible = true;
            Chart3.Annotations.Add(objAnnotation1);


            CalloutAnnotation objAnnotation2 = new CalloutAnnotation();
            objAnnotation2.AnchorDataPoint = Chart3.Series["Series1"].Points[2];
            objAnnotation2.Text = pointsArray[2].ToString();
            objAnnotation1.Visible = true;
            Chart3.Annotations.Add(objAnnotation2);

            objAnnotation =  null;
            objAnnotation1 = null;
            objAnnotation2 = null;
        }
    }

    private void chart4()
    {
        //Chart4 Start
        DateTime objFromDate;
        DateTime objToDate;
        objToDate = DateTime.Now.AddDays(-1);
        objFromDate = DateTime.Now.AddDays(-8);
        string[] seriesArray = new string[7];
        int i;
        DateTime objWeek = DateTime.Today;
        for (i = 0; i < seriesArray.Length; i++)
        {
            objWeek = objWeek.AddDays(-1);
            seriesArray[6 - i] = objWeek.ToString("dd-MMM-yy");
        }

        int[] pointsArray = new int[7];
        Title title = Chart4.Titles.Add("Title1");
        title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
        Chart4.Titles["Title1"].Text = "Last 7 Days Transaction Hitting";

        CalloutAnnotation objAnnotation = null;

        for (int iDays = 1; iDays <= 7; iDays++)  //Last 7 Days Txn
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";
                //URL = "http://localhost:50463/Service1.svc";

                DateTime objTxnDtTime = DateTime.Today;
                objTxnDtTime = objTxnDtTime.AddDays(-iDays);

                string strQuery = "SELECT bill_payemnt_txn as total_bill_payemnt_txn, birth_certificate_txn as total_birth_certificate_txn, death_certificate_txn as total_death_certificate_txn FROM seva_sindhu_" + objTxnDtTime.ToString("yy") + ".txn_detail_" + objTxnDtTime.ToString("MMM") + " where txn_date_time >= '" + objTxnDtTime.ToString("yyyy-MM-dd") + "' and txn_date_time <= '" + objTxnDtTime.ToString("yyyy-MM-dd") + " 23:59:59'";

                string JsonString = JsonConvert.SerializeObject(strQuery);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

                Reply objReply = json.Deserialize<Reply>(reader);

                if (objReply.res && objReply.DS != null)
                {
                    pointsArray[7 - iDays] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][0].ToString() != "" ? objReply.DS.Tables[0].Rows[0][0].ToString() : "0") +
                                             Convert.ToInt32(objReply.DS.Tables[0].Rows[0][1].ToString() != "" ? objReply.DS.Tables[0].Rows[0][0].ToString() : "0") +
                                             Convert.ToInt32(objReply.DS.Tables[0].Rows[0][2].ToString() != "" ? objReply.DS.Tables[0].Rows[0][2].ToString() : "0");
                }
                else
                {
                    pointsArray[7 - iDays] = 0;
                }

            }
        }

        Chart4.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[0];
        objAnnotation.Text = pointsArray[0].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[1];
        objAnnotation.Text = pointsArray[1].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[2];
        objAnnotation.Text = pointsArray[2].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[3];
        objAnnotation.Text = pointsArray[3].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[4];
        objAnnotation.Text = pointsArray[4].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[5];
        objAnnotation.Text = pointsArray[5].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

        objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart4.Series["Series1"].Points[6];
        objAnnotation.Text = pointsArray[6].ToString();
        objAnnotation.Visible = true;
        Chart4.Annotations.Add(objAnnotation);
        objAnnotation = null;

    }

    private void chart5()
    {
        using (WebClient client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "text/json";
            //URL = "http://localhost:50463/Service1.svc";

            string strQuery = "SELECT COUNT(case when client_status_with_rms = 'Connected' then client_status_with_rms end) as Connected, COUNT(case when client_status_with_rms = 'Disconnected' then client_status_with_rms end) as Disconnected FROM pcmc.health_recent";

            string JsonString = JsonConvert.SerializeObject(strQuery);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            Reply objReply = json.Deserialize<Reply>(reader);
            //Chart5 Start
            string[] seriesArray = { "Activated", "Pending" };
            int[] pointsArray = new int[2];

            if (objReply.res && objReply.DS != null)
            {
                pointsArray[0] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][0]);
                pointsArray[1] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][1]);
            }

            Title title = Chart5.Titles.Add("Title1");
            title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            Chart5.Titles["Title1"].Text = "Machine Activated";
            Chart5.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);
            Chart5.Series["Series1"].Points[0].Color = Color.Green;
            Chart5.Series["Series1"].Points[1].Color = Color.Red;
            

            CalloutAnnotation objAnnotation = new CalloutAnnotation();
            objAnnotation.AnchorDataPoint = Chart5.Series["Series1"].Points[0];
            objAnnotation.Text = pointsArray[0].ToString();
            objAnnotation.Visible = true;
            Chart5.Annotations.Add(objAnnotation);

            CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
            objAnnotation1.AnchorDataPoint = Chart5.Series["Series1"].Points[1];
            objAnnotation1.Text = pointsArray[1].ToString();
            objAnnotation1.Visible = true;
            Chart5.Annotations.Add(objAnnotation1);

            seriesArray = null;
            pointsArray = null;
            objAnnotation = null;
            objAnnotation1 = null;
        }
    }

    private void bindLocationddl() {
        try
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "text/json";
            string strQuery =  "SELECT distinct location FROM pcmc.kiosk_master where location <> '' or location<> null";

            string JsonString = JsonConvert.SerializeObject(strQuery);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

            string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

            EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

            Reply objReply = json.Deserialize<Reply>(reader);

            if(objReply.res&&objReply.DS!=null&& objReply.DS.Tables[0]!=null)
            foreach (DataRow item in objReply.DS.Tables[0].Rows)
            {
                ddlLocations.Items.Add(item[0].ToString());
            }
            
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void chart6()
    {
        //Chart6 Start

        WebClient client = new WebClient();
        client.Headers[HttpRequestHeader.ContentType] = "text/json";

        string[] seriesArray = { "Online Machine", "Offline Machine" };
        int[] pointsArray = new int[2];

        //  string selectedLocation = ddlLocations.SelectedItem.Text;

        // string strQuery = "SELECT COUNT(case when client_status_with_rms = 'Connected' then 'connected' end)  as Online,COUNT(case when client_status_with_rms = 'Disconnected' then 'Disconnected' end) as Offline from health_recent where f_kiosk_id in (select kiosk_id from kiosk_master where location = '"+selectedLocation+"')";

        //string JsonString = JsonConvert.SerializeObject(strQuery);
        //EncRequest objEncRequest = new EncRequest();
        //objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
        //string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

        //string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

        //EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
        //objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
        //JsonSerializer json = new JsonSerializer();
        //json.NullValueHandling = NullValueHandling.Ignore;
        //StringReader sr = new StringReader(objResponse.ResponseData);
        //Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

        //Reply objReply = json.Deserialize<Reply>(reader);
        //if (objReply.res&&objReply.DS!=null)
        //{
        //    pointsArray[0] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][0].ToString());
        //    pointsArray[1] = Convert.ToInt32(objReply.DS.Tables[0].Rows[0][1].ToString());
        //}

        pointsArray[0] = 1;
        pointsArray[1] = 0;

        Title title = Chart6.Titles.Add("Title1");
        title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
        Chart6.Titles["Title1"].Text = "";
        Chart6.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);
        Chart6.Series["Series1"].Points[0].Color = Color.LightGreen;
        Chart6.Series["Series1"].Points[1].Color = Color.MediumPurple;
        Chart6.Series["Series1"].ChartType = SeriesChartType.Pie;


        CalloutAnnotation objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart6.Series["Series1"].Points[0];
        objAnnotation.Text = pointsArray[0].ToString();
        objAnnotation.Visible = true;
        Chart6.Annotations.Add(objAnnotation);

        CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
        objAnnotation1.AnchorDataPoint = Chart6.Series["Series1"].Points[1];
        objAnnotation1.Text = pointsArray[1].ToString();
        objAnnotation1.Visible = true;
        Chart6.Annotations.Add(objAnnotation1);

        objAnnotation = null;
        objAnnotation1 = null;
    }

    private void chart7()
    {
        WebClient client = new WebClient();
        client.Headers[HttpRequestHeader.ContentType] = "text/json";

        //Chart7 Start
        string[] seriesArray = { "Offline Activate Machine", "Online Activate Machine" };
        int[] pointsArray = new int[2];

        // string strQuery = "SELECT COUNT(case when [Status] = '000' then [Status] end) as Successful,COUNT(case when [Status] != '000' then [Status] end)as Unsuccessful FROM pcmc.o.[cashtxndetails] where [TxnDate] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and [TxnDate] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";
        DateTime healthTime = DateTime.Now.AddHours(-2);
        string strQuery = "SELECT * FROM pcmc.health_recent ";


        string JsonString = JsonConvert.SerializeObject(strQuery);
        EncRequest objEncRequest = new EncRequest();
        objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
        string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

        string result = client.UploadString(URL + "/GetPieChart", "POST", dataEncrypted);

        EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
        objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
        JsonSerializer json = new JsonSerializer();
        json.NullValueHandling = NullValueHandling.Ignore;
        StringReader sr = new StringReader(objResponse.ResponseData);
        Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

        Reply objReply = json.Deserialize<Reply>(reader);

        int connected = 1;
        int disConnected = 0;


        //foreach (DataRow item in objReply.DS.Tables[0].Rows)
        //{
        //    var timestr = item["date_time"].ToString();
        //    DateTime dt = DateTime.ParseExact(timestr, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

        //    if (dt > healthTime)
        //    {
        //        connected++;
        //    }
        //    else {
        //        disConnected++;
        //    }
        //}



      
            pointsArray[0] = disConnected;
            pointsArray[1] = connected;
        

        Title title = Chart7.Titles.Add("Title1");
        title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
        Chart7.Titles["Title1"].Text = "Machine Connected with server";
        Chart7.Series["Series1"].Points.DataBindXY(seriesArray, pointsArray);
        Chart7.Series["Series1"].Points[0].Color = Color.Red;
        Chart7.Series["Series1"].Points[1].Color = Color.Green;        


        CalloutAnnotation objAnnotation = new CalloutAnnotation();
        objAnnotation.AnchorDataPoint = Chart7.Series["Series1"].Points[0];
        objAnnotation.Text = pointsArray[0].ToString();
        objAnnotation.Visible = true;
        Chart7.Annotations.Add(objAnnotation);

        CalloutAnnotation objAnnotation1 = new CalloutAnnotation();
        objAnnotation1.AnchorDataPoint = Chart7.Series["Series1"].Points[1];
        objAnnotation1.Text = pointsArray[1].ToString();
        objAnnotation1.Visible = true;
        Chart7.Annotations.Add(objAnnotation1);

        objAnnotation = null;
        objAnnotation1 = null;
    }




    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            chart6();
            
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
