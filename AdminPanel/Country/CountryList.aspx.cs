using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryList : System.Web.UI.Page
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
            fillCountry();
        }
    }
    #endregion


    #region Delete
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_Country_DeleteByPK", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CountryID", e.CommandArgument.ToString());

            cmd.ExecuteScalar();

            conn.Close();
        }
        catch (SqlException exec)
        {
            if(exec.Number == 547)
            {
                lbl.Text = "Could not delete the record as it is used as foregin key";
            }
        }
        finally
        {
            fillCountry();
        }
    }
    #endregion


    #region FillCountry
    private void fillCountry()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_Country_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        gvCountry.DataSource = read;

        gvCountry.DataBind();

        conn.Close();
    }
    #endregion
}