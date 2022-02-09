using System;
using System.Collections.Generic;
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
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            fuImage.SaveAs(Server.MapPath("~/Content/Image/" + fuImage.FileName));

            SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "PR_UserMaster_RegisterUser";

            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);

            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);

            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);

            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

            cmd.Parameters.AddWithValue("@PhotoPath", "~/Content/Image/" + fuImage.FileName);

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            SqlDataReader read = cmd.ExecuteReader();

            lblMsg.Text = "Registered";

            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }

        catch (SqlException ex)
        {
            if(ex.Number == 2601)
                lblMsg.Text = "This Username is already exist";
        }
    }
}