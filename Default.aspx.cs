using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserName.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand();
        
        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_UserMaster_SelectByUserNamePassword";

        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());

        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

        SqlDataReader read = cmd.ExecuteReader();

        DataTable dtUser = new DataTable();

        dtUser.Load(read);

        conn.Close();

        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            foreach (DataRow drUser in dtUser.Rows)
            {
                if (!drUser["UserID"].Equals(DBNull.Value))
                {
                    Session["UserID"] = drUser["UserID"].ToString();
                }
                if (!drUser["FullName"].Equals(DBNull.Value))
                {
                    Session["FullName"] = drUser["FullName"].ToString();
                }
                if (!drUser["PhotoPath"].Equals(DBNull.Value))
                {
                    Session["ImgProfile"] = drUser["PhotoPath"].ToString();
                }

                break;
            }
            Response.Redirect("~/AddressBook/AdminPanel/Contact/Display");
        }
        else
        {
            lblMsg.Text = "Username or Password is not valid";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }
    }
}