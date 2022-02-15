using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityList : System.Web.UI.Page
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
            fillcity();
        }
    }
    #endregion

    #region Delete
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_City_DeleteByPK", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CityID", DBNullOrStringValue(e.CommandArgument.ToString()));

            cmd.ExecuteNonQuery();

            conn.Close();

            fillcity();

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

    #region FillCity
    private void fillcity()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_City_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        gvCity.DataSource = read;

        gvCity.DataBind();

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