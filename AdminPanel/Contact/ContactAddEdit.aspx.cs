﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtContactName.Focus();

            if (Session["UserID"] != null)
            {
                FillContactCategoryDropdown();

                FillCountryDropdown();
            }

            if (Page.RouteData.Values["ContactID"] != null)
            {
                LoadControls();
                btnAdd.Text = "Save";
            }
        }
    }

    #region Fill Contact Category Dropdown
    private void FillContactCategoryDropdown()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlContactCategory.DataSource = objSDR;

            ddlContactCategory.DataTextField = "ContactCategoryName";

            ddlContactCategory.DataValueField = "ContactCategoryID";

            ddlContactCategory.DataBind();
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
        }
    }
    #endregion


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

            objCmd.Parameters.AddWithValue("@UserID", (Session["UserID"].ToString()));

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlCountry.DataSource = objSDR;

            ddlCountry.DataTextField = "CountryName";

            ddlCountry.DataValueField = "CountryID";

            ddlCountry.DataBind();
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
    private void FillStateDropdown(string CountryID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@CountryID", CountryID);

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

    #region Fill City Dropdown
    private void FillCityDropdown(String StateID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_City_SelectDropdownListByUserID", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@StateID", StateID);

            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            ddlCity.DataSource = objSDR;

            ddlCity.DataTextField = "CityName";

            ddlCity.DataValueField = "CityID";

            ddlCity.DataBind();
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        }
    }
    #endregion


    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if (ddlContactCategory.SelectedItem.Text == "Select Contact Category")
        {
            strErrorMessage += "Select Contact category<br/>";
        }
        if (ddlCity.SelectedItem.Text == "Select City")
        {
            strErrorMessage += "Select City<br/>";
        }
        if (ddlState.SelectedItem.Text == "Select State")
        {
            strErrorMessage += "Select State<br/>";
        }
        if (ddlCountry.SelectedItem.Text == "Select Country")
        {
            strErrorMessage += "Select Country<br/>";
        }
        if (txtContactName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Contact Name<br/>";
        }
        if (txtMobile.Text.Trim() == "")
        {
            strErrorMessage += "Enter Mobile Number<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Local Variables
        SqlString strContactName = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlInt32 strContactCategoryID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlString strMobileNumber = SqlString.Null;
        SqlString strEmailAddress = SqlString.Null;
        SqlString strPincode = SqlString.Null;
        SqlString strFaceBookID = SqlString.Null;
        SqlString strLinkedinID = SqlString.Null;
        #endregion Local Variables

        #region Gather Information
        if (ddlContactCategory.SelectedIndex > 0)
        {
            strContactCategoryID = Convert.ToInt32(ddlContactCategory.SelectedValue);
        }
        if (ddlCity.SelectedIndex > 0)
        {
            strCityID = Convert.ToInt32(ddlCity.SelectedValue);
        }
        if (ddlState.SelectedIndex > 0)
        {
            strStateID = Convert.ToInt32(ddlState.SelectedValue);
        }
        if (ddlCountry.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        if (txtContactName.Text.Trim() != "")
        {
            strContactName = txtContactName.Text.Trim();
        }
        if (txtMobile.Text.Trim() != "")
        {
            strMobileNumber = txtMobile.Text.Trim();
        }
        if (txtAddress.Text.Trim() != "")
        {
            strAddress = txtAddress.Text.Trim();
        }
        if (txtEmail.Text.Trim() != "")
        {
            strEmailAddress = txtEmail.Text.Trim();
        }
        if (txtPincode.Text.Trim() != "")
        {
            strPincode = txtPincode.Text.Trim();
        }
        #endregion

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = objConn;

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactName", strContactName);

            objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);

            objCmd.Parameters.AddWithValue("@Address", strAddress);

            objCmd.Parameters.AddWithValue("@Pincode", strPincode);

            objCmd.Parameters.AddWithValue("@CityID", strCityID);

            objCmd.Parameters.AddWithValue("@StateID", strStateID);

            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);

            objCmd.Parameters.AddWithValue("@EmailAddress", strEmailAddress);

            objCmd.Parameters.AddWithValue("@MobileNo", strMobileNumber);

            objCmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["ContactID"] != null)
            {
                objCmd.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"].ToString());

                objCmd.CommandText = "PR_Contact_UpdateByPK";

                objCmd.ExecuteNonQuery();

                Response.Redirect("~/AddressBook/AdminPanel/Contact/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                objCmd.CommandText = "PR_Contact_Insert";

                objCmd.ExecuteNonQuery();

                lblMessage.Text = "Data Inserted Successfully";

                txtContactName.Text = "";
                txtAddress.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtPincode.Text = "";

                ddlContactCategory.SelectedIndex = 0;

                ddlCountry.SelectedIndex = 0;

                ddlState.Items.Clear();

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "-1";

                ddlCity.Items.Clear();

                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
                ddlCity.SelectedValue = "-1";
            }
        }
        catch (SqlException exec)
        {
            lblMessage.Text = exec.Message.ToString();
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

    }
    #endregion


    #region LoadControl
    private void LoadControls()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_Contact_SelectByPK", objConn);

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"].ToString());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                        txtContactName.Text = objSDR["ContactName"].ToString();

                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        ddlContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString();

                    if (!objSDR["Address"].Equals(DBNull.Value))
                        txtAddress.Text = objSDR["Address"].ToString();

                    if (!objSDR["Pincode"].Equals(DBNull.Value))
                        txtPincode.Text = objSDR["Pincode"].ToString();

                    if (!objSDR["EmailAddress"].Equals(DBNull.Value))
                        txtEmail.Text = objSDR["EmailAddress"].ToString();

                    if (!objSDR["MobileNo"].Equals(DBNull.Value))
                        txtMobile.Text = objSDR["MobileNo"].ToString();

                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString();

                    if (!objSDR["StateID"].Equals(DBNull.Value))
                        ddlState.SelectedValue = objSDR["StateID"].ToString();

                    if (!objSDR["CityID"].Equals(DBNull.Value))
                        ddlCity.SelectedValue = objSDR["CityID"].ToString();

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
            
            FillStateDropdown(ddlCountry.SelectedValue.ToString());
            FillCityDropdown(ddlState.SelectedValue.ToString());
        }
    }
    #endregion

    protected void ddlCountry_TextChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex == 0)
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();

            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
            ddlState.SelectedValue = "-1";

            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
            ddlCity.SelectedValue = "-1";
            return;
        }

        FillStateDropdown(ddlCountry.SelectedValue.ToString());
        
        ddlCity.Items.Clear();
        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        ddlCity.SelectedValue = "-1";

        lblMessage.Text = "";
    }

    protected void ddlState_TextChanged(object sender, EventArgs e)
    {
        FillCityDropdown(ddlState.SelectedValue.ToString());

        lblMessage.Text = "";
    }

    protected void ddlContactCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
    }
}