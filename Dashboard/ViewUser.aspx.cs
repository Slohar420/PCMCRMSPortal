using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ViewUser : System.Web.UI.Page
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    public static DataSet ObjDS;

    public class Reply
    {
        public DataSet DS { get; set; }

        public bool res { get; set; }


        public string strError { get; set; }
    }

    public class UserDetailsInfo
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Role { get; set; }
    }

    public class UserDetailsCreation
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string UserCreationDate { get; set; }
        public string Pwd { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
          
            Response.Redirect("../Default.aspx");
        }

        if (!IsPostBack)
        {
            BindUserDetails();
        }
    }

    public void BindUserDetails()
    {
        try
        {
            if (ObjDS == null)
                ObjDS = new DataSet();

            Reply objRes = new Reply();
            
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";

                string JsonString = JsonConvert.SerializeObject(Session["Username"]);
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                string result = client.UploadString(URL + "/GetUserDetails", "POST", dataEncrypted);

                EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if (objRes.res == true)
                {
                    GV_Kiosk_Health.DataSource = objRes.DS;
                    GV_Kiosk_Health.DataBind();
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

    protected void BtnReset_Click(object sender, EventArgs e)
    {
       
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {

    }
    public static string CreateRandomPassword(int PasswordLength)
    {
        string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }


    protected void GV_Kiosk_Health_RowCommand(object sender, GridViewCommandEventArgs e)
    
    {
        try
        {
            if (e.CommandName == "Reset")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                //int rowIndex = Convert.ToInt32(e.CommandArgument);

                ////Reference the GridView Row.
                //GridViewRow row = GV_Kiosk_Health.Rows[rowIndex];

                //string username = (row.FindControl("Username") as Label).Text;
                ////string firstname = (row.FindControl("FirstName") as Label).Text;
                ////string lastname = (row.FindControl("LastName") as Label).Text;
                //string role = (row.FindControl("Role") as Label).Text;

                //UserDetailsCreation objUserReq = new UserDetailsCreation();

                ////objUserReq.FirstName = firstname;
                ////objUserReq.LastName = lastname;
                //string Role = string.Join("| ", role);
                //objUserReq.Role = Role;
                //objUserReq.UserCreationDate = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
                //objUserReq.UserName = username;

                //NameValueCollection collections = new NameValueCollection();
                ////collections.Add("FirstName", objUserReq.FirstName);
                ////collections.Add("LastName", objUserReq.LastName);
                //collections.Add("Role", objUserReq.Role);
                //collections.Add("Username", objUserReq.UserName);

                ////string remoteUrl = "http://localhost:5075/Post_Redirect_Website/Page2_CS.aspx";
                //string remoteUrl = "../ResetPassword.aspx";

                // //Response.Redirect("../ResetPassword.aspx");

                //string html = "<html><head>";
                //html += "</head><body onload='document.forms[0].submit()'>";
                //html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
                //foreach (string key in collections.Keys)
                //{
                //    html += string.Format("<input name='{0}' type='text' value='{1}'>", key, collections[key]);
                //}
                //html += "</form></body></html>";
                //Response.Clear();
                //Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
                //Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
                //Response.Charset = "ISO-8859-1";
                //Response.Write(html);
                //Response.End();

                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = GV_Kiosk_Health.Rows[rowIndex];

                //string username = row.Cells[1].Text;
                string username = (row.FindControl("UserName") as Label).Text;
                //Label lbl = (Label)GridView.Rows[e.RowIndex].FindControl("Label_Edit_Name");

                UserDetailsInfo objUserReq = new UserDetailsInfo();
                objUserReq.UserName = username;

                lblPassword.Text = CreateRandomPassword(8);

                string Pwd = lblPassword.Text;
                objUserReq.Pwd = "@" + Pwd;

                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");
                //var regexItem = new Regex(@"([<>\?\*\\\""/\|!@])+");
                var regexItem = new Regex(@"([<>?\*\"" /| !@#$%^&*()+={}':;.,/?[\]_-~`])+");


                if (ObjDS == null)
                    ObjDS = new DataSet();

                Reply objRes = new Reply();

                // send request
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "text/json";

                    string JsonString = JsonConvert.SerializeObject(objUserReq);
                    EncRequest objEncRequest = new EncRequest();
                    objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                    string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                    string result = client.UploadString(URL + "/UpdatePassword", "POST", dataEncrypted);

                    EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                    objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    StringReader sr = new StringReader(objResponse.ResponseData);
                    Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                    objRes = json.Deserialize<Reply>(reader);

                    if (objRes.res == true)
                    {
                        Response.Write("<script type='text/javascript'>alert( 'User Password Updated Successfully !!! Your Random Generated Password is :- " + objUserReq.Pwd + "' )</script>");


                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert( 'Invalid Password. / " + objRes.strError + "')</script>");
                    }
                }
            }
            if (e.CommandName == "ManageRole")
            {

                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = GV_Kiosk_Health.Rows[rowIndex];

                string username = (row.FindControl("Username") as Label).Text;
                //string firstname = (row.FindControl("FirstName") as Label).Text;
                //string lastname = (row.FindControl("LastName") as Label).Text;
                string role = (row.FindControl("Role") as Label).Text;

                UserDetailsCreation objUserReq = new UserDetailsCreation();

                //objUserReq.FirstName = firstname;
                //objUserReq.LastName = lastname;
                string Role = string.Join("| ", role);
                objUserReq.Role = Role;
                objUserReq.UserCreationDate = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
                objUserReq.UserName = username;

                NameValueCollection collections = new NameValueCollection();
                //collections.Add("FirstName", objUserReq.FirstName);
                //collections.Add("LastName", objUserReq.LastName);
                collections.Add("Role", objUserReq.Role);
                collections.Add("Username", objUserReq.UserName);

                //string remoteUrl = "http://localhost:5075/Post_Redirect_Website/Page2_CS.aspx";
                string remoteUrl = "CreateUser.aspx";

                string html = "<html><head>";
                html += "</head><body onload='document.forms[0].submit()'>";
                html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
                foreach (string key in collections.Keys)
                {
                    html += string.Format("<input name='{0}' type='text' value='{1}'>", key, collections[key]);
                }
                html += "</form></body></html>";
                Response.Clear();
                Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
                Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
                Response.Charset = "ISO-8859-1";
                Response.Write(html);
                Response.End();

            }

            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = GV_Kiosk_Health.Rows[rowIndex];

                string username = (row.FindControl("UserName") as Label).Text;

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "text/json";

                    string JsonString = JsonConvert.SerializeObject(username);
                    EncRequest objEncRequest = new EncRequest();
                    objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                    string dataEncrypted = JsonConvert.SerializeObject(objEncRequest); 

                    string result = client.UploadString(URL + "/DeleteUser", "POST", dataEncrypted);

                    EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                    objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    StringReader sr = new StringReader(objResponse.ResponseData);
                    Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                  string  res = json.Deserialize<string>(reader);

                    if (res == "success")
                    {
                        Response.Write("<script type='text/javascript'>alert( 'User Delete Successfully ' )</script>");
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert( 'Error in User Delete Process' )</script>");
                    }
                }
            }
        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
        } 
        }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {

        List<string> userrole = new List<string>();
        if (chkAdmin.Checked) { userrole.Add(chkAdmin.Text); }
        if (chkApproval.Checked) { userrole.Add(chkApproval.Text); }
        if (chkCreator.Checked) { userrole.Add(chkCreator.Text); }
        if (chkManager.Checked) { userrole.Add(chkManager.Text); }

        UserDetailsInfo objUserRole = new UserDetailsInfo();
        string Role = string.Join("| ", userrole);
        objUserRole.Role = Role;

        try
        {
            if (ObjDS == null)
                ObjDS = new DataSet();

            Reply objRes = new Reply();

            // send request
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "text/json";

                DataContractJsonSerializer objJsonSerSend = new DataContractJsonSerializer(typeof(UserDetailsInfo));

                MemoryStream memStrToSend = new MemoryStream();

                objJsonSerSend.WriteObject(memStrToSend, objUserRole);

                string data = Encoding.Default.GetString(memStrToSend.ToArray());

                string result = client.UploadString(URL + "/UpdateUserRole", "POST", data);

                MemoryStream memstrToReceive = new MemoryStream(Encoding.UTF8.GetBytes(result));

                DataContractJsonSerializer objJsonSerRecv = new DataContractJsonSerializer(typeof(Reply));

                objRes = (Reply)objJsonSerRecv.ReadObject(memstrToReceive);

                if (objRes.res == true)
                {
                    lblPassword.Text = "'User Role Updated Successfully !!!";
                    lblPassword.Visible = true;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert( 'Invalid User Role. / " + objRes.strError + "')</script>");
                }
            }

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'catch error : '" + excp.Message + "' )</script>");
        }
    }

    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as Button).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = GV_Kiosk_Health.Rows[rowIndex];

        string role = (row.FindControl("Role") as Label).Text;

        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal('" + role + "');", true);
    }

    protected void GV_Kiosk_Health_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindUserDetails();
    }
}
