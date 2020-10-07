using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Utilities.Encoders;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Session.Clear();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Reply objRes = new Reply();
        try
        {
            if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                txtUserName.Text = "";
                txtUserName.Text = "";
                Response.Write("<script>alert('Kindly enter username')</script>");
                return;
            }
            if (txtPassword.Text == "" || txtPassword.Text == null)
            {

                txtUserName.Text = "";
                txtPassword.Text = "";
                Response.Write("<script>alert('Kindly enter password')</script>");
                return;
            }
            UserLogin userLogin = new UserLogin();
            var decodedString1 = Base64.Decode(txtUserName.Text);
            userLogin.UserName = Convert.ToString(TrimString(System.Text.Encoding.UTF8.GetString(decodedString1), '&'));
            var decodedString2 = Base64.Decode(txtPassword.Text);
            userLogin.Password =  Convert.ToString(TrimString(System.Text.Encoding.UTF8.GetString(decodedString2), '*'));
            WebClient objWC = new WebClient();
            objWC.Headers[HttpRequestHeader.ContentType] = "text/json";
          
            string JsonString = JsonConvert.SerializeObject(userLogin);
            EncRequest objEncRequest = new EncRequest();
            objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
            string EncData = JsonConvert.SerializeObject(objEncRequest);
            string result = objWC.UploadString(URL + "/Login", "POST", EncData);

            EncResponse objEncResponse = JsonConvert.DeserializeObject<EncResponse>(result);
            objEncResponse.ResponseData = AesGcm256.Decrypt(objEncResponse.ResponseData);

            JsonSerializer json = new JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            StringReader sr = new StringReader(objEncResponse.ResponseData);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            objRes = json.Deserialize<Reply>(reader);
           
            if (objRes.res == true)
            {

                Session["Username"] = userLogin.UserName;
                Session["Role"] = objRes.DS.Tables[0].Rows[0]["credential_type"].ToString();
                Response.Redirect("Dashboard//PieChart.aspx", false);

                if (objRes.DS.Tables[0].Rows[0]["Credential_type"].ToString().ToLower().Trim() == "user")
                {
                    HtmlGenericControl hideUserManagment = (HtmlGenericControl)Page.Master.FindControl("UserManagement");
                    hideUserManagment.Style.Add("display", "none");
                    
                }

            }
            else if (result == "redirect")
            {
                Session["Username"] = userLogin.UserName;
                Response.Redirect("ResetPassword.aspx?Username=" + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserName)), false);
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                Response.Write("<script>alert('Invalid Username Password')</script>");
            }
        }
        catch (Exception ex)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }

    private string TrimString(string text, char char1)
    {
        string str = null;
        string[] strArr = null;

        str = text;
        char[] splitchar = { char1 };
        strArr = str.Split(splitchar);

        string output = strArr[0];
        return output;
    }
 }