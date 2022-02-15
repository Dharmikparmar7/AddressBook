using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
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
            txtContactName.Focus();

            fillContactCategory();

            fillCountry();

            if (Page.RouteData.Values["ContactID"] != null)
            {
                loadControls();
                btnAdd.Text = "Save";
            }
            else
            {
                ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
                ddlContactCategory.SelectedValue = "-1";

                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
                ddlCountry.SelectedValue = "-1";

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "-1";

                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
                ddlState.SelectedValue = "-1";
            }

        }
    }
    #endregion


    #region FillContactCategory
    private void fillContactCategory()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_ContactCategory_DropdownByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        ddlContactCategory.DataSource = read;

        ddlContactCategory.DataTextField = "ContactCategoryName";

        ddlContactCategory.DataValueField = "ContactCategoryID";

        ddlContactCategory.DataBind();

        conn.Close();
    }
    #endregion


    #region FillCountry
    private void fillCountry()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_Country_SelectAllByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        ddlCountry.DataSource = read;

        ddlCountry.DataTextField = "CountryName";

        ddlCountry.DataValueField = "CountryID";

        ddlCountry.DataBind();

        conn.Close();
    }
    #endregion


    #region FillState
    private void fillState(string CountryID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_State_DropdownByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@CountryID", DBNullOrStringValue(CountryID));

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        ddlState.DataSource = read;

        ddlState.DataTextField = "StateName";

        ddlState.DataValueField = "StateID";

        ddlState.DataBind();

        conn.Close();
    }
    #endregion


    #region FillCity
    private void fillCity(String StateID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_City_DropdownByUserID", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@StateID", DBNullOrStringValue(StateID));

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        ddlCity.DataSource = read;

        ddlCity.DataTextField = "CityName";

        ddlCity.DataValueField = "CityID";

        ddlCity.DataBind();

        conn.Close();
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
            lbl.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ContactName", DBNullOrStringValue(txtContactName.Text.ToString().Trim()));

            cmd.Parameters.AddWithValue("@ContactCategoryID", DBNullOrStringValue(ddlContactCategory.SelectedValue.ToString()));

            cmd.Parameters.AddWithValue("@Address", DBNullOrStringValue(txtAddress.Text));

            cmd.Parameters.AddWithValue("@Pincode", DBNullOrStringValue(txtPincode.Text));

            cmd.Parameters.AddWithValue("@CityID", DBNullOrStringValue(ddlCity.SelectedValue));

            cmd.Parameters.AddWithValue("@StateID", DBNullOrStringValue(ddlState.SelectedValue));

            cmd.Parameters.AddWithValue("@CountryID", DBNullOrStringValue(ddlCountry.SelectedValue));

            cmd.Parameters.AddWithValue("@EmailAddress", DBNullOrStringValue(txtEmail.Text));

            cmd.Parameters.AddWithValue("@MobileNo", DBNullOrStringValue(txtMobile.Text));

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);


            if (ddlContactCategory.SelectedValue == "-1")
            {
                lbl.Text = "Please Select Contact Category";
                ddlContactCategory.Focus();
                return;
            }

            if (ddlCountry.SelectedValue == "-1")
            {
                lbl.Text = "Please Select Country";
                ddlCountry.Focus();
                return;
            }

            if (ddlState.SelectedValue == "-1")
            {
                lbl.Text = "Please Select State";
                ddlState.Focus();
                return;
            }

            if (ddlCity.SelectedValue == "-1")
            {
                lbl.Text = "Please Select City";
                ddlCity.Focus();
                return;
            }

            if (Page.RouteData.Values["ContactID"] != null)
            {
                cmd.Parameters.AddWithValue("@ContactID", DBNullOrStringValue(Page.RouteData.Values["ContactID"].ToString()));

                cmd.CommandText = "PR_Contact_UpdateByPK";

                cmd.ExecuteNonQuery();

                conn.Close();

                Response.Redirect("~/AddressBook/AdminPanel/Contact/Display");
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

                cmd.CommandText = "PR_Contact_Insert";

                cmd.ExecuteNonQuery();

                conn.Close();

                lbl.Text = "Data Inserted Successfully";

                txtContactName.Text = "";
                txtAddress.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtPincode.Text = "";

                ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
                ddlContactCategory.SelectedValue = "-1";

                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
                ddlCountry.SelectedValue = "-1";

                ddlState.Items.Clear();

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "-1";

                ddlCity.Items.Clear();

                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
                ddlCity.SelectedValue = "-1";
            }
        }
        catch(SqlException exec)
        {
            lbl.Text = exec.Message.ToString();
        }

    }
    #endregion


    #region LoadControl
    private void loadControls()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_Contact_SelectByPK", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@ContactID", DBNullOrStringValue(Page.RouteData.Values["ContactID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        if (read.HasRows)
        {
            while (read.Read())
            {
                if (!read["ContactName"].Equals(DBNull.Value))
                    txtContactName.Text = read["ContactName"].ToString();

                if (!read["ContactCategoryID"].Equals(DBNull.Value))
                    ddlContactCategory.SelectedValue = read["ContactCategoryID"].ToString();
                
                if (!read["Address"].Equals(DBNull.Value))
                    txtAddress.Text = read["Address"].ToString();

                if (!read["Pincode"].Equals(DBNull.Value))
                    txtPincode.Text = read["Pincode"].ToString();

                if (!read["EmailAddress"].Equals(DBNull.Value))
                    txtEmail.Text = read["EmailAddress"].ToString();

                if (!read["MobileNo"].Equals(DBNull.Value))
                    txtMobile.Text = read["MobileNo"].ToString();

                if (!read["CountryID"].Equals(DBNull.Value))
                    ddlCountry.SelectedValue = read["CountryID"].ToString();

                if (!read["StateID"].Equals(DBNull.Value))
                    ddlState.SelectedValue = read["StateID"].ToString();

                if (!read["CityID"].Equals(DBNull.Value))
                    ddlCity.SelectedValue = read["CityID"].ToString();

                break;
            }
        }

        fillState(ddlCountry.SelectedValue.ToString());
        fillCity(ddlState.SelectedValue.ToString());

        conn.Close();
    }
    #endregion

    protected void ddlCountry_TextChanged(object sender, EventArgs e)
    {
        ddlCountry.Items.Remove(new ListItem("Select Country", "-1"));

        fillState(ddlCountry.SelectedValue.ToString());
        ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
        ddlState.SelectedValue = "-1";

        ddlCity.Items.Clear();
        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        ddlCity.SelectedValue = "-1";

        lbl.Text = "";
    }
    
    protected void ddlState_TextChanged(object sender, EventArgs e)
    {
        ddlState.Items.Remove(new ListItem("Select State", "-1"));

        fillCity(ddlState.SelectedValue.ToString());
        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        ddlCity.SelectedValue = "-1";

        lbl.Text = "";
    }
    
    protected void ddlContactCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContactCategory.Items.Remove(new ListItem("Select Contact Category", "-1"));

        lbl.Text = "";
    }
    
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Remove(new ListItem("Select City", "-1"));

        lbl.Text = "";
    }

    private Object DBNullOrStringValue(String val)
    {
        if (String.IsNullOrEmpty(val))
        {
            return DBNull.Value;
        }
        return val;
    }
}