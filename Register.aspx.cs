using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        SqlString strFullName = SqlString.Null;
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strPhotoPath = SqlString.Null;

        if (txtFullName.Text.Trim() != "")
            strFullName = txtFullName.Text;

        if (txtUserName.Text.Trim() != "")
            strUserName = txtUserName.Text;

        if (txtPassword.Text.Trim() != "")
            strPassword = txtPassword.Text;

        if (txtAddress.Text.Trim() != "")
            strAddress = txtAddress.Text;

        if (txtMobileNo.Text.Trim() != "")
            strMobileNo = txtMobileNo.Text;

        if (txtEmail.Text.Trim() != "")
            strEmail = txtEmail.Text;

        if(fuImage.HasFile)
            strPhotoPath = "~/Content/Image/" + fuImage.FileName;


        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_UserMaster_RegisterUser", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@FullName", strFullName);

            objCmd.Parameters.AddWithValue("@UserName", strUserName);

            objCmd.Parameters.AddWithValue("@Password", strPassword);

            objCmd.Parameters.AddWithValue("@Address", strAddress);

            objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);

            objCmd.Parameters.AddWithValue("@Email", strMobileNo);

            objCmd.Parameters.AddWithValue("@PhotoPath", strPhotoPath);

            objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            objCmd.ExecuteNonQuery();

            fuImage.SaveAs(Server.MapPath(strPhotoPath.ToString()));

            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }

        catch (SqlException ex)
        {
            if (ex.Number == 2601)
                lblMessage.Text = "This Username is already exist";

            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion
}