using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Dashboard_CreateUser : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(Request.Form["UserName"]))
            {
                btnsubmit.Text = "Update";
                if (Request.Form.Count > 0)
                {
                    txtusername.Text = Request.Form["UserName"];
                    txtusername.Enabled = false;
                    txtAnswer.Text = Request.Form["Answer"];
                    btnCancel.Visible = false;
                    string[] role = Request.Form["Role"].ToString().ToLower().Split('|');

                    for (int i = 0; i < role.Length; i++)
                    {
                        if (role[i].Trim() == "administrator")
                        {
                            chkUser.Checked = chkAdmin.Checked = chkLocation.Checked = true;
                            chkUser.Enabled = chkLocation.Enabled = false;
                        }
                        if (role[i].Trim() == "user")
                        {
                            chkUser.Checked = true;
                        }
                        if (role[i].Trim() == "location")
                        {
                            chkLocation.Checked = true;
                        }
                    }
                }
            }
            else
            {
                btnsubmit.Text = "Submit";
            }
            bindTemplateName();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtusername.Text == "" || txtusername.Text == null)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Enter The UserName');", true);
                txtusername.Focus();
                return;
            }

            if (txtPassword.Text == "" || txtPassword.Text == null )
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Enter The Password with atleast one special charcter and a Capital latter');", true);
                txtPassword.Focus();
                return;
            }
       
            if (txtAnswer.Text == "" || txtAnswer.Text == null)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Enter Answer For Selected Question');", true);
                txtAnswer.Focus();
                return;
            }
            bool ticked = false;
            List<string> userrole = new List<string>();
            if (chkAdmin.Checked) { userrole.Add(chkAdmin.Text); ticked = true; }
            if (chkUser.Checked) { userrole.Add(chkUser.Text); ticked = true; }
            if (chkLocation.Checked) { userrole.Add(chkLocation.Text); ticked = true; }
            if (!ticked)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Select Atleast One Role');", true);
                return;
            }
            UserDetails objUserReq = new UserDetails();
            if (btnsubmit.Text == "Submit")
            {
                objUserReq.Username = txtusername.Text;
                objUserReq.Password = txtPassword.Text;
                string Role = string.Join("| ", userrole);
                if (txtLocation.Text != "" && chkLocation.Checked == true)
                {
                    objUserReq.Location = txtLocation.Text;
                }
                objUserReq.Role = Role;
                objUserReq.Question = filterlist.SelectedValue.ToString();
                objUserReq.Answer = txtAnswer.Text.ToString();

                if (objds == null)
                    objds = new DataSet();

                Reply objRes = new Reply();
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "text/json";

                    string JsonString = JsonConvert.SerializeObject(objUserReq);
                    EncRequest objEncRequest = new EncRequest();
                    objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                    string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                    string result = client.UploadString(URL + "/Adduser", "POST", dataEncrypted);

                    EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                    objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    StringReader sr = new StringReader(objResponse.ResponseData);
                    Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                    objRes = json.Deserialize<Reply>(reader);

                    if (objRes.res == true)
                    {
                        var page1 = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page1, page1.GetType(), "alert", "alert('User Created Successfully' );window.location ='CreateUser.aspx';", true);
                        clearAll();
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert( 'User Already Exist. / " + objRes.strError + "')</script>");
                        lblPassword.Text = "";
                    }
                    chkAdmin.Checked = false;
                    chkUser.Checked = false;
                    chkLocation.Checked = false;
                    chkUser.Enabled = true;
                    chkAdmin.Enabled = true;
                }
            }
            else
            {

                objUserReq.Username = txtusername.Text;
                objUserReq.Password = txtPassword.Text;
                string Role = string.Join("| ", userrole);
                if (txtLocation.Text != "" && chkLocation.Checked == true)
                {
                    objUserReq.Location = txtLocation.Text;
                }
                objUserReq.Role = Role;
                objUserReq.Question = filterlist.SelectedValue.ToString();
                objUserReq.Answer = txtAnswer.Text.ToString();

                if (objds == null)
                    objds = new DataSet();

                Reply objRes = new Reply();
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "text/json";

                    string JsonString = JsonConvert.SerializeObject(objUserReq);
                    EncRequest objEncRequest = new EncRequest();
                    objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                    string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                    string result = client.UploadString(URL + "/UpdateUser", "POST", dataEncrypted);

                    EncResponse objResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                    objResponse.ResponseData = AesGcm256.Decrypt(objResponse.ResponseData);
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    StringReader sr = new StringReader(objResponse.ResponseData);
                    Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                    objRes = json.Deserialize<Reply>(reader);

                    if (objRes.res == true)
                    {
                        var page1 = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page1, page1.GetType(), "alert", "alert('User Update Successfully' );window.location ='ViewUser.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert( 'User Already Exist. / " + objRes.strError + "')</script>");
                        lblPassword.Text = "";
                    }
                 }
              }
            }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'Error catch " + excp.Message + "')</script>");
        }
    }
    protected void clearAll()
    {
        txtAnswer.Text = "";
        txtLocation.Text = "";
        txtPassword.Text = "";
        txtusername.Text = "";
    }
    protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        e.IsValid = chkAdmin.Checked;
        e.IsValid = chkUser.Checked;
    }
    protected void filterlist_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblPassword.Text = "";
        txtusername.Text = "";
        txtPassword.Text = "";
        txtAnswer.Text = "";
        chkAdmin.Checked = false;
        chkUser.Checked = false;
        chkUser.Checked = false;
        chkAdmin.Enabled = chkUser.Enabled = true;
    }

    protected void chkAdmin_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdmin.Checked)
        {
            chkUser.Checked = true;
            chkLocation.Checked = true;
            chkUser.Enabled = false;
            chkLocation.Enabled = false;
        }
        else
        {
            chkUser.Checked = false;
            chkUser.Enabled = true;
            chkLocation.Checked = false;
            chkLocation.Enabled = true;
        }
    }

    protected void chkCreator_CheckedChanged(object sender, EventArgs e)
    {
        if (chkUser.Checked)
        {
            chkAdmin.Checked = false;
            chkAdmin.Enabled = false;
        }
        else
        {
            chkAdmin.Enabled = true;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        lblPassword.Text = "";
        txtusername.Text = "";
        txtAnswer.Text = "";
        txtPassword.Text = "";
        txtAnswer.Text = "";

        chkAdmin.Checked = false;
        chkUser.Checked = false;
        chkAdmin.Enabled = chkUser.Enabled = true;
        if (btnsubmit.Text == "Submit")
            Response.Redirect("Dashboard.aspx");
        else
            Response.Redirect("ViewUser.aspx");
    }

    public void bindTemplateName()
    {
        try
        {
            filterlist.Items.Clear();
            filterlist.Items.Insert(0, "Who Is Your Favourite Cricketer?");
            filterlist.Items.Insert(1, "Which Is Your Favourite Place?");
            filterlist.Items.Insert(2, "What Is Your Pets Name?");
            filterlist.Items.Insert(3, "Who Is Your Favourite Actor/Actress?");
            filterlist.Items.Insert(4, "Which Is The Place Where You Born?");
        }
        catch (Exception excp)
        {
            Response.Write("<script type='text/javascript'>alert( 'Catch Error : '" + excp.Message + "' )</script>");
        }
    }

    protected void chkLocation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLocation.Checked)
        {
            chkAdmin.Checked = false;
            chkAdmin.Enabled = false;
            chkUser.Enabled = false;
            chkUser.Checked = true;
            divLocation.Visible = true;
        }
        else
        {
            chkAdmin.Enabled = true;
            chkUser.Enabled = true;
            chkUser.Checked = false;
            divLocation.Visible = false;
        }
    }
}
