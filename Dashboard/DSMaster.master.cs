using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using Newtonsoft.Json;
using System.IO;

public partial class Dashboard_DSMaster : System.Web.UI.MasterPage
{
    public const string AntiXsrfTokenKey = "__AntiXsrfToken";
    public const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    public string _antiXsrfTokenValue;
    public static string URL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL1"].ToString();
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //First, check for the existence of the Anti-XSS cookie
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;

            //If the CSRF cookie is found, parse the token from the cookie.
            //Then, set the global page variable and view state user
            //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad
            //method.
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be
                //validated against the value in the view state form field in
                //the Page.PreLoad method.
                _antiXsrfTokenValue = requestCookie.Value;

                //Set the view state user key, which will be validated by the
                //framework during each request
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            //If the CSRF cookie is not found, then this is a new session.
            else
            {
                //Generate a new Anti-XSRF token
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");

                //Set the view state user key, which will be validated by the
                //framework during each request
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                //Create the non-persistent CSRF cookie
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    //Set the HttpOnly property to prevent the cookie from
                    //being accessed by client side script
                    HttpOnly = true,

                    //Add the Anti-XSRF token to the cookie value
                    Value = _antiXsrfTokenValue
                };

                //If we are using SSL, the cookie should be set to secure to
                //prevent it from being sent over HTTP connections
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                    responseCookie.Secure = true;

                //Add the CSRF cookie to the response
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error catch : '" + ex.Message + "')</script>");
            //throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Session Null');window.location ='../Default.aspx';", true);

          //  Response.Redirect("../Default.aspx");
            return;
        }
         
          


        if (!IsPostBack)
        {
            
            //string[] strRole = Session["Role"].ToString().ToLower().Split('|');

            //for (int i = 0; i < strRole.Length; i++)
            //{
            //    if (strRole[i].Trim() == "administrator")
            //    {
            //        Li1.Visible = true;
            //        usermanagement.Visible = true;
            //    }
            //    else 
            //    {
            //        if(strRole[i].Trim() == "user")
            //            usermanagement.Visible = false;
            //            Li1.Visible = false;
                    
            //    }

            //}

            lblUserName.Text = lblUSName.Text = lblUname.Text = Session["Username"].ToString();
            Reply objRes = new Reply();
            using (WebClient objClient = new WebClient())
            {
               
                string JsonString = JsonConvert.SerializeObject(Session["Username"].ToString().ToLower());
                EncRequest objEncRequest = new EncRequest();
                objEncRequest.RequestData = AesGcm256.Encrypt(JsonString);
                string dataEncrypted = JsonConvert.SerializeObject(objEncRequest);

                objClient.Headers[HttpRequestHeader.ContentType] = "text/json";
                string result = objClient.UploadString(URL + "/GetUserType", "POST", dataEncrypted);
               
                 EncResponse objEncResponse = JsonConvert.DeserializeObject<EncResponse>(result);
                objEncResponse.ResponseData = AesGcm256.Decrypt(objEncResponse.ResponseData);

                JsonSerializer json = new JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                StringReader sr = new StringReader(objEncResponse.ResponseData);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
                objRes = json.Deserialize<Reply>(reader);

                if(objRes.res == true)
                {
                    if (objRes.DS.Tables[0].Rows[0]["credential_type"].ToString().ToLower() == "user")
                    {
                        usermanagement.Visible = false;
                        Li1.Visible = false;
                    }
                }
                lblUserName.Text = lblUSName.Text = lblUname.Text =  Session["Username"].ToString();
            }

        }
    }
    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        try
        {

            //During the initial page load, add the Anti-XSRF token and user
            //name to the ViewState
            if (!IsPostBack)
            {
                // LNKLogOut.Visible = true;
                //Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;

                //If a user name is assigned, set the user name
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            //During all subsequent post backs to the page, the token value from
            //the cookie should be validated against the token in the view state
            //form field. Additionally user name should be compared to the
            //authenticated users name
            else
            {
                //Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] !=
                (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error catch : '"+ex.Message+"')</script>");
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["Username"] = null;
               Response.Redirect("../Default.aspx");
        return;

    }

}
