using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPanel_CityAddEdit : System.Web.UI.Page
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
            txtCityName.Focus();

            loadControls();

            if (Page.RouteData.Values["CityID"] != null)
            {
                loadControlsByPK();
                btnAddCity.Text = "Save";
            }
            else
            {
                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

                ddlCountry.SelectedValue = "-1";

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

                ddlState.SelectedValue = "-1";
            }

        }
    }

    private void loadControlsByPK()
    {
        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        conn.Open();

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_City_SelectByPK";

        int CityID = 0;

        int.TryParse(Page.RouteData.Values["CityID"].ToString(), out CityID);

        cmd.Parameters.AddWithValue("@CityID", CityID);

        SqlDataReader read = cmd.ExecuteReader();

        if (read.HasRows)
        {
            while (read.Read())
            {
                txtCityName.Text = read["CityName"].ToString();
                txtPincode.Text = read["Pincode"].ToString();
                txtSTDCode.Text = read["STDCode"].ToString();
                ddlCountry.SelectedValue = read["CountryID"].ToString();
                ddlState.SelectedValue = read["StateID"].ToString();
            }
        }

        read.Close();
        cmd.Parameters.Clear();

        cmd.CommandText = "PR_State_DropdownByUserID";

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.ToString());

        read = cmd.ExecuteReader();

        ddlState.DataSource = read;

        ddlState.DataTextField = "StateName";

        ddlState.DataValueField = "StateID";

        ddlState.DataBind();

        conn.Close();
    }

    private void loadControls()
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

        read.Close();

        //cmd.CommandText = "PR_State_DropdownByUserID";

        //cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.ToString());

        //read = cmd.ExecuteReader();

        //ddlState.DataSource = read;

        //ddlState.DataTextField = "StateName";

        //ddlState.DataValueField = "StateID";

        //ddlState.DataBind();

        conn.Close();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry.Items.Remove(new ListItem("Select Country", "-1"));

        SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

        SqlCommand cmd = new SqlCommand();

        conn.Open();

        cmd.Connection = conn;

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "PR_State_DropdownByUserID";

        cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.ToString());

        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

        SqlDataReader read = cmd.ExecuteReader();

        ddlState.DataSource = read;

        ddlState.DataTextField = "StateName";

        ddlState.DataValueField = "StateID";

        ddlState.DataBind();

        ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

        ddlState.SelectedValue = "-1";

        conn.Close();

        txtMsg.Text = "";
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Remove(new ListItem("Select State", "-1"));
        txtMsg.Text = "";
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["CityID"] != null)
        {
            updateCity();
        }
        else
        {
            addCity();

        }
    }

    private void addCity()
    {
        try
        {

            SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "PR_City_Insert";

            cmd.Parameters.AddWithValue("@CityName", txtCityName.Text);

            cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text);

            cmd.Parameters.AddWithValue("@STDCode", txtSTDCode.Text);

            cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue.ToString());

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

            if (ddlCountry.SelectedItem.Value == "-1")
            {
                txtMsg.Text = "Please Select Country";
                ddlCountry.Focus();
                return;
            }
            if (ddlState.SelectedValue.ToString() == "-1")
            {
                txtMsg.Text = "Please Select State";
                ddlState.Focus();
                return;
            }

            cmd.ExecuteNonQuery();

            conn.Close();

            txtMsg.Text = "Data Inserted Successfully";
            txtCityName.Text = "";
            txtPincode.Text = "";
            txtSTDCode.Text = "";
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                txtMsg.Text = "Cannot insert duplicate value";
                txtCityName.Focus();
            }
        }
    }

    private void updateCity()
    {
        try
        {

            SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "PR_City_UpdateByPK";

            cmd.Parameters.AddWithValue("@CityID", Page.RouteData.Values["CityID"].ToString());

            cmd.Parameters.AddWithValue("@CityName", txtCityName.Text);

            cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text);

            cmd.Parameters.AddWithValue("@STDCode", txtSTDCode.Text);

            cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue.ToString());

            //cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (ddlState.SelectedValue.ToString() == "-1")
            {
                txtMsg.Text = "Please Select State";
                ddlState.Focus();
                return;
            }

            cmd.ExecuteNonQuery();

            conn.Close();

            txtMsg.Text = "Data Updated Successfully";

            Response.Redirect("~/AddressBook/AdminPanel/City/Display");
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                txtMsg.Text = "Cannot insert duplicate value";
                txtCityName.Focus();
            }
        }
    }
}