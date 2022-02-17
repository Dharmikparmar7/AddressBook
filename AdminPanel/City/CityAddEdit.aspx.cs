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


public partial class AdminPanel_CityAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCityName.Focus();

            if (Session["UserID"] != null)
                FillCountryDropdown();

            if (Page.RouteData.Values["CityID"] != null)
            {
                LoadControls();
                btnAddCity.Text = "Save";
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

            SqlCommand objCmd = new SqlCommand("PR_City_SelectByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CityID", Page.RouteData.Values["CityID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                        txtCityName.Text = objSDR["CityName"].ToString();

                    if (!objSDR["Pincode"].Equals(DBNull.Value))
                        txtPincode.Text = objSDR["Pincode"].ToString();

                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                        txtSTDCode.Text = objSDR["STDCode"].ToString();

                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString();

                    if (!objSDR["StateID"].Equals(DBNull.Value))
                        ddlState.SelectedValue = objSDR["StateID"].ToString();

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

            FillStateDropdown();
        }
    }
    #endregion LoadControls

    #region Fill Country Dropdown
    private void FillCountryDropdown()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_Country_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlCountry.DataSource = objSDR;

            ddlCountry.DataTextField = "CountryName";

            ddlCountry.DataValueField = "CountryID";

            ddlCountry.DataBind();

            objSDR.Close();
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
        }
    }
    #endregion

    #region Fill State Dropdown
    protected void FillStateDropdown()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryID", (ddlCountry.SelectedValue.ToString()));

            objCmd.Parameters.AddWithValue("@UserID", (Session["UserID"].ToString()));

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlState.DataSource = objSDR;

            ddlState.DataTextField = "StateName";

            ddlState.DataValueField = "StateID";

            ddlState.DataBind();
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
        }

    }
    #endregion

    #region Add and Edit
    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlInt32 strStateID = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strPincode = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        #endregion Local Variables

        #region Server Side Validation
        String strErrorMessage = "";

        if (ddlCountry.SelectedIndex == 0)
            strErrorMessage += "Select Country <br/>";

        if (ddlState.SelectedIndex == 0)
            strErrorMessage += "Select State<br/>";

        if (txtCityName.Text.Trim() == "")
            strErrorMessage += "Enter City Name<br/>";

        if (strErrorMessage.Trim() != "")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        if (ddlState.SelectedIndex > 0)
            strStateID = Convert.ToInt32(ddlState.SelectedValue);

        if (txtCityName.Text.Trim() != "")
            strCityName = txtCityName.Text.Trim();

        if (txtPincode.Text.Trim() != "")
            strPincode = txtPincode.Text.Trim();

        if (txtSTDCode.Text.Trim() != "")
            strSTDCode = txtSTDCode.Text.Trim();
        #endregion Gather Information

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = objConn;

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CityName", strCityName);

            objCmd.Parameters.AddWithValue("@Pincode", strPincode);

            objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);

            objCmd.Parameters.AddWithValue("@StateID", strStateID);

            if (Page.RouteData.Values["CityID"] != null)
            {
                objCmd.Parameters.AddWithValue("@CityID", Page.RouteData.Values["CityID"].ToString());

                objCmd.CommandText = "PR_City_UpdateByPK";

                objCmd.ExecuteNonQuery();

                Response.Redirect("~/AddressBook/AdminPanel/City/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("@UserID", (Session["UserID"].ToString()));

                objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                objCmd.CommandText = "PR_City_Insert";

                objCmd.ExecuteNonQuery();

                lblMessage.Text = "Data Inserted Successfully";
                txtCityName.Text = "";
                txtPincode.Text = "";
                txtSTDCode.Text = "";

                ddlCountry.SelectedIndex = 0;
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                lblMessage.Text = "Cannot insert duplicate value";
                txtCityName.Focus();
            }
            else
                lblMessage.Text = exec.Message.ToString();
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStateDropdown();

        lblMessage.Text = "";
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
    }
}