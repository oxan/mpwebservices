using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MediaPortal.TvServer.WebServices;

public partial class Login : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["authenticated"] != null)
      Response.Redirect("Default.aspx");
    edUid.Focus();
  }
  protected void btnLogin_Click(object sender, EventArgs e)
  {
    string uid; string pwd;
    Utils.GetLogin(out uid, out pwd);
    if (edUid.Text == uid && edPwd.Text == pwd)
    {
      Session.Clear();
      Session.Timeout = 24 * 60; // =24 Hours
      Session["authenticated"] = true;
      Response.Redirect("Default.aspx");
    }
    else
      lError.Visible = true;
  }
}
