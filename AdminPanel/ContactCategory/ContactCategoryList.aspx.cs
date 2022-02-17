using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_Category_ContactCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillContactCategoryGridview();
        }
    }

    #region Delete Contact Category
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_DeleteByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactCategoryID", e.CommandArgument.ToString());

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

            FillContactCategoryGridview();
        }

    }
    #endregion

    #region Fill Contact Category Gridview
    private void FillContactCategoryGridview()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectAllByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            if(Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            gvContactCategory.DataSource = objSDR;

            gvContactCategory.DataBind();
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