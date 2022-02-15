using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_State_StateList : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }
        if (!IsPostBack)
        {
            fillState();
        }
    }
    #endregion

    #region Delete
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_State_DeleteByPK", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StateID", DBNullOrStringValue(e.CommandArgument.ToString()));

            cmd.ExecuteScalar();

            conn.Close();
        }
        catch (SqlException exec)
        {
            if (exec.Number == 547)
            {
                lbl.Text = "Could not delete the record as it is used as foregin key";
            }
        }
        finally
        {
            fillState();
        }
    }
    #endregion

    #region FillStateGridView
    private void fillState()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_State_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        gvState.DataSource = read;

        gvState.DataBind();

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