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

public partial class AdminPanel_StateAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStateName.Focus();

            FillCountryDropDown();

            if (Page.RouteData.Values["StateID"] != null)
            {
                btnAdd.Text = "Save";

                LoadControls();
            }
        }
    }

    #region LoadControls
    private void LoadControls()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = objConn;

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.CommandText = "PR_State_SelectByPK";

            objCmd.Parameters.AddWithValue("@StateID", Page.RouteData.Values["StateID"]);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                        txtStateName.Text = objSDR["StateName"].ToString();

                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString();

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

    #region FillCountryDropdown
    private void FillCountryDropDown()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_Country_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlCountryID.DataSource = objSDR;

            ddlCountryID.DataTextField = "CountryName";

            ddlCountryID.DataValueField = "CountryID";

            ddlCountryID.DataBind();
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
        }
    }
    #endregion


    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if (txtStateName.Text.Trim() == "")
        {
            strErrorMessage += "Enter State Name";
        }
        if (ddlCountryID.SelectedIndex == 0)
        {
            strErrorMessage += "Select Country";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Local Variables
        SqlString strStateName = SqlString.Null;
        SqlInt32 strCountryID = SqlInt32.Null;
        #endregion Local Variables

        #region Gather Information
        if (ddlCountryID.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
        }
        if (txtStateName.Text.Trim() != "")
        {
            strStateName = txtStateName.Text.Trim();
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

            objCmd.Parameters.AddWithValue("@StateName", strStateName);

            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);

            objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["StateID"] != null)
            {
                objCmd.Parameters.AddWithValue("@StateID", Page.RouteData.Values["StateID"].ToString());

                objCmd.CommandText = "PR_State_UpdateByPK";

                objCmd.ExecuteNonQuery();

                Response.Redirect("~/AddressBook/AdminPanel/State/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("UserID", Session["UserID"].ToString());

                objCmd.CommandText = "PR_State_Insert";

                objCmd.ExecuteNonQuery();

                lblMessage.Text = "Data Inserted Successfully";

                txtStateName.Text = "";

                ddlCountryID.SelectedIndex = 0;
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                lblMessage.Text = "Cannot insert duplicate value";
                txtStateName.Focus();
            }
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion


    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
    }
}