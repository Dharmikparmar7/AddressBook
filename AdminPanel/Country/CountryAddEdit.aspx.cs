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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCountryName.Focus();

            if (Page.RouteData.Values["CountryID"] != null)
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

            SqlCommand objCmd = new SqlCommand("PR_Country_SelectByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"]);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                        txtCountryCode.Text = objSDR["CountryCode"].ToString();

                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                        txtCountryName.Text = objSDR["CountryName"].ToString();

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

        if (txtCountryName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Country Name <br/>";
        }

        if (strErrorMessage != "")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Local Variables
        SqlString strCountryCode = SqlString.Null;
        SqlString strCountryName = SqlString.Null;
        #endregion Local Variables

        #region Gather Information
        if (txtCountryName.Text.Trim() != "")
        {
            strCountryName = txtCountryName.Text.Trim();
        }
        if (txtCountryCode.Text.Trim() != "")
        {
            strCountryCode = txtCountryCode.Text.Trim();
        }
        #endregion Gather Information


        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = objConn;

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);

            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);

            objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["CountryID"] != null)
            {
                objCmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"].ToString());

                objCmd.CommandText = "PR_Country_UpdateByPK";

                objCmd.ExecuteNonQuery();

                objConn.Close();

                Response.Redirect("~/AddressBook/AdminPanel/Country/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                objCmd.CommandText = "PR_Country_Insert";

                objCmd.ExecuteNonQuery();

                lblMessage.Text = "Data Inserted Successfully";

                txtCountryCode.Text = "";
                txtCountryName.Text = "";
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                lblMessage.Text = "Cannot insert duplicate value";
                txtCountryName.Focus();
            }
            else
            {
                lblMessage.Text = exec.Message.ToString();
            }
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion
}