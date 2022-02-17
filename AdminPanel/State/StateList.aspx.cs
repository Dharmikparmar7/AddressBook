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
        if (!IsPostBack)
        {
            FillStateGridView();
        }
    }
    #endregion

    #region Delete State
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_DeleteByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@StateID", e.CommandArgument.ToString());

            objCmd.ExecuteNonQuery();

        }
        catch (SqlException exec)
        {
            if (exec.Number == 547)
                lblMessage.Text = "Could not delete the record as it is used as foregin key";
            else
                lblMessage.Text = exec.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FillStateGridView();
        }
    }
    #endregion

    #region Fill State GridView
    private void FillStateGridView()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_SelectAllByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            if(Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            gvState.DataSource = objSDR;

            gvState.DataBind();

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