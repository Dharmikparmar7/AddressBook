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
            return;
        }

        if(!IsPostBack)
        {
            if(Session["UserID"] == null)
            {
                Response.Redirect("~/AddressBook/AdminPanel/Login");
            }
            else
            {
                if(Session["ImgProfile"] != null)
                {
                    imgProfile.ImageUrl = Session["ImgProfile"].ToString();
                }
                lbl.Text = Session["FullName"].ToString();
            }
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/AddressBook/AdminPanel/Login");
    }
}
