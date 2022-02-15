using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Default : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
            Response.Redirect("~/AddressBook/AdminPanel/Login");

        if (!IsPostBack)
        {
            fillContact();
        }
    }
    #endregion

    #region Delete
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_Contact_DeleteByPK", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ContactID", DBNullOrStringValue(e.CommandArgument.ToString()));

            cmd.ExecuteNonQuery();

            conn.Close();

            fillContact();
        }
        catch(SqlException exec)
        {
            if (exec.Number == 547)
            {
                lbl.Text = "Could not delete the record as it is used as foregin key";
            }
        }
    }
    #endregion

    #region FillContactGridView
    private void fillContact()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_Contact_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        gvContact.DataSource = read;

        gvContact.DataBind();

        conn.Close();
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