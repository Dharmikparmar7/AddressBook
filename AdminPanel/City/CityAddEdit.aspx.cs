using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPanel_CityAddEdit : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/AddressBook/AdminPanel/Login");
            }

            txtCityName.Focus();

            fillCountry();

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
    #endregion

    #region LoadControls
    private void loadControlsByPK()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand("PR_City_SelectByPK", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@CityID", DBNullOrStringValue(Page.RouteData.Values["CityID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        if (read.HasRows)
        {
            while (read.Read())
            {
                if (!read["CityName"].Equals(DBNull.Value))
                    txtCityName.Text = read["CityName"].ToString();

                if (!read["Pincode"].Equals(DBNull.Value))
                    txtPincode.Text = read["Pincode"].ToString();

                if (!read["STDCode"].Equals(DBNull.Value))
                    txtSTDCode.Text = read["STDCode"].ToString();

                if (!read["CountryID"].Equals(DBNull.Value))
                    ddlCountry.SelectedValue = read["CountryID"].ToString();

                if (!read["StateID"].Equals(DBNull.Value))
                    ddlState.SelectedValue = read["StateID"].ToString();
            }
        }

        read.Close();

        fillState();

        conn.Close();
    }
    #endregion LoadControls

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

        read.Close();

        conn.Close();
    }
    #endregion

    #region FillState
    protected void fillState()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        SqlCommand cmd = new SqlCommand("PR_State_DropdownByUserID", conn);

        conn.Open();

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@CountryID", DBNullOrStringValue(ddlCountry.SelectedValue.ToString()));

        cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

        SqlDataReader read = cmd.ExecuteReader();

        ddlState.DataSource = read;

        ddlState.DataTextField = "StateName";

        ddlState.DataValueField = "StateID";

        ddlState.DataBind();

        conn.Close();
    }
    #endregion

    #region Add and Edit
    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if(ddlCountry.SelectedItem.Text == "Select Country")
        {
            strErrorMessage += "Select Country <br/>";
        }
        if (ddlState.SelectedItem.Text == "Select State")
        {
            strErrorMessage += "Select State<br/>";
        }
        if (txtCityName.Text.Trim() == "")
        {
            strErrorMessage += "Enter City Name<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            txtMsg.Text = strErrorMessage;
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

            cmd.Parameters.AddWithValue("@CityName", DBNullOrStringValue(txtCityName.Text));

            cmd.Parameters.AddWithValue("@Pincode", DBNullOrStringValue(txtPincode.Text));

            cmd.Parameters.AddWithValue("@STDCode", DBNullOrStringValue(txtSTDCode.Text));

            cmd.Parameters.AddWithValue("@StateID", DBNullOrStringValue(ddlState.SelectedValue.ToString()));

            if (Page.RouteData.Values["CityID"] != null)
            {
                cmd.Parameters.AddWithValue("@CityID", DBNullOrStringValue(Page.RouteData.Values["CityID"].ToString()));

                cmd.CommandText = "PR_City_UpdateByPK";

                cmd.ExecuteNonQuery();

                txtMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/City/Display");
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserID", DBNullOrStringValue(Session["UserID"].ToString()));

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.CommandText = "PR_City_Insert";

                cmd.ExecuteNonQuery();

                txtMsg.Text = "Data Inserted Successfully";
                txtCityName.Text = "";
                txtPincode.Text = "";
                txtSTDCode.Text = "";

                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

                ddlCountry.SelectedValue = "-1";

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

                ddlState.SelectedValue = "-1";
            }

            conn.Close();
        }

        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                txtMsg.Text = "Cannot insert duplicate value";
                txtCityName.Focus();
            }
            else
            {
                txtMsg.Text = exec.Message.ToString();
            }
        }
    }
    #endregion
    
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry.Items.Remove(new ListItem("Select Country", "-1"));

        fillState();

        ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

        ddlState.SelectedValue = "-1";

        txtMsg.Text = "";
    }
    
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Remove(new ListItem("Select State", "-1"));
        txtMsg.Text = "";
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