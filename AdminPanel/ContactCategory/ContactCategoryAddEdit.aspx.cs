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

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCCName.Focus();

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                btnAdd.Text = "Save";
                LoadControls();
            }
        }
    }

    #region Load Controls
    protected void LoadControls()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactCategoryID", (Page.RouteData.Values["ContactCategoryID"].ToString()));

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                        txtCCName.Text = objSDR["ContactCategoryName"].ToString();

                    break;
                }
            }
        }
        catch (SqlException ex)
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

    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if (txtCCName.Text.Trim() == "")
        {
            strErrorMessage = "Enter Contact Category Name<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }

        #region Local Variables
        SqlString strContactCategoryName = SqlString.Null;
        SqlConnection objConnection = new SqlConnection();
        objConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        #endregion Local Variables

        #region Gather Information
        if (txtCCName.Text.Trim() != "")
        {
            strContactCategoryName = txtCCName.Text.Trim();
        }
        #endregion Gather Information

        #endregion Server Side Validation

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = objConn;

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);

            objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                objCmd.Parameters.AddWithValue("@ContactCategoryId", Page.RouteData.Values["ContactCategoryID"].ToString());

                objCmd.CommandText = "PR_ContactCategory_UpdateByPK";

                objCmd.ExecuteNonQuery();

                Response.Redirect("~/AddressBook/AdminPanel/ContactCategory/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("UserID", (Session["UserID"].ToString()));

                objCmd.CommandText = "PR_ContactCategory_Insert";

                objCmd.ExecuteNonQuery();

                objConn.Close();

                lblMessage.Text = "Data Inserted Successfully";

                txtCCName.Text = "";
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
                lblMessage.Text = "Cannot insert duplicate value";
            else
                lblMessage.Text = exec.Message;

        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion
}