using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AddressBook/AdminPanel/Login");
            return;
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

    private void fillContactCategory()
    {
        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_ContactCategory_DropdownByUserID";

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        ddlContactCategory.DataSource = read;

        ddlContactCategory.DataTextField = "ContactCategoryName";

        ddlContactCategory.DataValueField = "ContactCategoryID";

        ddlContactCategory.DataBind();

        conn.Close();
    }

    protected void ddlContactCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContactCategory.Items.Remove(new ListItem("Select Contact Category", "-1"));

        lbl.Text = "";
    }

    private void fillCountry()
    {
        SqlConnection conn = new SqlConnection();

        conn.ConnectionString = "data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;";

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_Country_SelectAllByUserID";

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        ddlCountry.DataSource = read;

        ddlCountry.DataTextField = "CountryName";

        ddlCountry.DataValueField = "CountryID";

        ddlCountry.DataBind();

        conn.Close();

        //fillState(ddlCountry.SelectedValue.ToString());
    }

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

    private void fillState(string CountryID)
    {
        SqlConnection conn = new SqlConnection();

        conn.ConnectionString = "data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;";

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_State_DropdownByUserID";

        cmd.Parameters.AddWithValue("@CountryID", CountryID);

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        ddlState.DataSource = read;

        ddlState.DataTextField = "StateName";

        ddlState.DataValueField = "StateID";

        ddlState.DataBind();

        conn.Close();

        //if (Page.RouteData.Values["ContactID"] == null)
        //{
        //    ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
        //    ddlState.SelectedValue = "-1";
        //}

    }

    protected void ddlState_TextChanged(object sender, EventArgs e)
    {
        ddlState.Items.Remove(new ListItem("Select State", "-1"));


        fillCity(ddlState.SelectedValue.ToString());
        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        ddlCity.SelectedValue = "-1";

        lbl.Text = "";
    }

    private void fillCity(String StateID)
    {
        SqlConnection conn = new SqlConnection();

        conn.ConnectionString = "data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;";

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_City_DropdownByUserID";

        cmd.Parameters.AddWithValue("@StateID", StateID);

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        ddlCity.DataSource = read;

        ddlCity.DataTextField = "CityName";

        ddlCity.DataValueField = "CityID";

        ddlCity.DataBind();

        conn.Close();
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Remove(new ListItem("Select City", "-1"));

        lbl.Text = "";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["ContactID"] != null)
        {
            updateContact();
        }
        else
        {
            addContact();
        }
    }

    private void addContact()
    {
        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_Contact_Insert";

        cmd.Parameters.AddWithValue("@ContactName", txtContactName.Text.ToString().Trim());

        cmd.Parameters.AddWithValue("@ContactCategoryID", ddlContactCategory.SelectedValue.ToString());

        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

        cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text);

        cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);

        cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue);

        cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue);

        cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);

        cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text);

        cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        if(ddlContactCategory.SelectedValue == "-1")
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

        cmd.ExecuteScalar();

        conn.Close();

        lbl.Text = "Data Inserted Successfully";
        txtContactName.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtPincode.Text = "";
    }

    private void updateContact()
    {
        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_Contact_UpdateByPK";

        cmd.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"].ToString());

        cmd.Parameters.AddWithValue("@ContactName", txtContactName.Text.ToString().Trim());

        cmd.Parameters.AddWithValue("@ContactCategoryID", ddlContactCategory.SelectedValue.ToString());

        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

        cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text);

        cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);

        cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue);

        cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue);

        cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);

        cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text);

        cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

        if(ddlContactCategory.SelectedValue == "-1")
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

        cmd.ExecuteScalar();

        conn.Close();

        lbl.Text = "Data Updated Successfully";

        Response.Redirect("~/AddressBook/AdminPanel/Contact/Display");
    }

    private void loadControls()
    {
        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_Contact_SelectByPK";

        cmd.Parameters.AddWithValue("@ContactID", Convert.ToInt32(Page.RouteData.Values["ContactID"]));

        SqlDataReader read = cmd.ExecuteReader();

        if (read.HasRows)
        {
            while (read.Read())
            {
                txtContactName.Text = read["ContactName"].ToString();
                ddlContactCategory.SelectedValue = read["ContactCategoryID"].ToString();
                txtAddress.Text = read["Address"].ToString();
                txtPincode.Text = read["Pincode"].ToString();
                txtEmail.Text = read["EmailAddress"].ToString();
                txtMobile.Text = read["MobileNo"].ToString();
                ddlCountry.SelectedValue = read["CountryID"].ToString();
                ddlState.SelectedValue = read["StateID"].ToString();
                ddlCity.SelectedValue = read["CityID"].ToString();
            }
        }

        fillState(ddlCountry.SelectedValue.ToString());
        fillCity(ddlState.SelectedValue.ToString());

        conn.Close();
    }
}