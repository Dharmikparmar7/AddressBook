using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Register
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_UserMaster_RegisterUser", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FullName", DBNullOrStringValue(txtFullName.Text));

            cmd.Parameters.AddWithValue("@UserName", DBNullOrStringValue(txtUserName.Text));

            cmd.Parameters.AddWithValue("@Password", DBNullOrStringValue(txtPassword.Text));

            cmd.Parameters.AddWithValue("@Address", DBNullOrStringValue(txtAddress.Text));

            cmd.Parameters.AddWithValue("@MobileNo", DBNullOrStringValue(txtMobileNo.Text));

            cmd.Parameters.AddWithValue("@Email", DBNullOrStringValue(txtEmail.Text));

            cmd.Parameters.AddWithValue("@PhotoPath", "~/Content/Image/" + fuImage.FileName);

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            SqlDataReader read = cmd.ExecuteReader();

            fuImage.SaveAs(Server.MapPath("~/Content/Image/" + fuImage.FileName));

            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }

        catch (SqlException ex)
        {
            if(ex.Number == 2601)
                lblMsg.Text = "This Username is already exist";
        }
    }
    #endregion

    private Object DBNullOrStringValue(String val)
    {
        if (String.IsNullOrEmpty(val))
        {
            return DBNull.Value;
        }
        return val;
    }
}