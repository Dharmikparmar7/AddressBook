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

    #region Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_UserMaster_SelectByUserNamePassword", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());

            objCmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            DataTable dtUser = new DataTable();

            dtUser.Load(objSDR);

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
                lblMessage.Text = "Username or Password is not valid";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
        }
        catch(SqlException ex)
        {
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