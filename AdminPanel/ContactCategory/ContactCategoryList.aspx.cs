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
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AddressBook/AdminPanel/Login");
        }
        if (!IsPostBack)
        {
            fillContactCategory();
        }
    }
    #endregion

    #region Delete
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_ContactCategory_DeleteByPK", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ContactCategoryID", DBNullOrStringValue(e.CommandArgument.ToString()));

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
            fillContactCategory();
        }

    }
    #endregion

    #region FillContactCategory
    private void fillContactCategory()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.ConnectionString = "data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;";

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_ContactCategory_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        gvContactCategory.DataSource = read;

        gvContactCategory.DataBind();

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