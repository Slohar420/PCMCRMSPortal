using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetPassword : System.Web.UI.Page
{
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"].ToString();
    public static DataSet objds;
    UserDetailsInfo objUserReq = new UserDetailsInfo();

    public class Reply
    {
        public DataSet DS { get; set; }

        public bool res { get; set; }


        public string strError { get; set; }
    }

    public class UserDetailsInfo
    {
        public string UserName { get; set; }
        public string ConfPwd { get; set; }
        public string Pwd { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //= Request.QueryString["Username"].ToString();
            //Response.Write(Name);
            if (!string.IsNullOrEmpty(Request.Form["UserName"]))
            {
                btnsubmit.Text = "Update";
                if (Request.Form.Count > 0)
                {
                    lblUser.Text = Request.Form["UserName"];
                     btnCancel.Visible = false;
                }
            }
        
            else
            {
                Response.Redirect("Default.aspx");
            }
         }
    }
  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string pwd = txt_Pwd.Text;
        string Cpwd = txt_ConfirmPwd.Text;

        if (pwd == "" || pwd == null)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert(' Please Enter Password');", true);
            return;
        }

        if (Cpwd == "" || Cpwd == null)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert(' Please Enter Confirm Password');", true);
            return;
        }

        if (Cpwd != pwd)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert(' Password and Confirm Password Should Same');", true);
            return;
        }


        UserDetailsInfo objUserReq = new UserDetailsInfo();
        objUserReq.UserName = lblUser.Text;
        objUserReq.Pwd = txt_Pwd.Text;
        objUserReq.ConfPwd = txt_ConfirmPwd.Text;

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        var regexItem = new Regex(@"([<>?\*\"" /| !@#$%^&*()+={}':;.,/?[\]_-~`])+");



        var isValidated = hasNumber.IsMatch(pwd) && hasUpperChar.IsMatch(pwd) && hasMinimum8Chars.IsMatch(pwd) && regexItem.IsMatch(pwd);

        if (!isValidated)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Password must contain one uppercase, one number and Special Character with minimum length is 8 character');window.location='ResetPassword.aspx';", true);
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

                DataContractJsonSerializer objJsonSerSend = new DataContractJsonSerializer(typeof(UserDetailsInfo));

                MemoryStream memStrToSend = new MemoryStream();

                objJsonSerSend.WriteObject(memStrToSend, objUserReq);

                string data = Encoding.Default.GetString(memStrToSend.ToArray());

                string result = client.UploadString(URL + "/UpdatePassword", "POST", data);

                MemoryStream memstrToReceive = new MemoryStream(Encoding.UTF8.GetBytes(result));

                DataContractJsonSerializer objJsonSerRecv = new DataContractJsonSerializer(typeof(Reply));

                objRes = (Reply)objJsonSerRecv.ReadObject(memstrToReceive);

                if (objRes.res == true)
                {
                    Response.Write("<script>alert( 'Password Reset successfully')</script>");
                
                    //Txtusername.Text = "";
                    txt_Pwd.Text = "";
                    txt_ConfirmPwd.Text = "";

                 //   Response.Redirect("Default.aspx");
                }
                else
                {
                    txt_Pwd.Text = "";
                    txt_ConfirmPwd.Text = "";
                    Response.Write("<script type='text/javascript'>alert( 'Invalid Password. / " + objRes.strError + "')</script>");
                }
            }

        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'error catch : '"+ excp.Message + " )</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_Pwd.Text = "";
        txt_ConfirmPwd.Text = "";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        txt_Pwd.Text = "";
        txt_ConfirmPwd.Text = "";
        Response.Redirect("Default.aspx");
    }
}