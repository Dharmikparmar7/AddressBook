using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }

        if (!Page.IsPostBack)
        {
            if (Session["ImgProfile"] != null)
                imgProfile.ImageUrl = Session["ImgProfile"].ToString();

            lblMessage.Text = Session["FullName"].ToString();
        }
    }

    #region LogOut
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();

        Response.Redirect("~/AddressBook/AdminPanel/Login");
    }
    #endregion
}
