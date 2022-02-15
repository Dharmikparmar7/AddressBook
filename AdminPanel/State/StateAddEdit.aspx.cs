using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_StateAddEdit : System.Web.UI.Page
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
            txtStateName.Focus();

            fillDropDown();

            #region LoadControls
            if (Page.RouteData.Values["StateID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_State_SelectByPK";

                cmd.Parameters.AddWithValue("@StateID", Page.RouteData.Values["StateID"]);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (!read["StateName"].Equals(DBNull.Value))
                            txtStateName.Text = read["StateName"].ToString();

                        if (!read["CountryID"].Equals(DBNull.Value))
                            ddlCountry.SelectedValue = read["CountryID"].ToString();
                        
                    }
                }

                conn.Close();
            }
            else
            {
                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

                ddlCountry.SelectedValue = "-1";
            }
            #endregion
        }
    }
    #endregion


    #region FillCountryDropdown
    private void fillDropDown()
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


    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if (txtStateName.Text.Trim() == "")
        {
            strErrorMessage += "Enter State Name";
        }
        if (ddlCountry.SelectedItem.Text == "Select Country")
        {
            strErrorMessage += "Select Country";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblMsg.Text = strErrorMessage;
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

            cmd.Parameters.AddWithValue("@StateName", DBNullOrStringValue(txtStateName.Text.ToString().Trim()));

            cmd.Parameters.AddWithValue("@CountryID", DBNullOrStringValue(ddlCountry.SelectedValue.ToString().Trim()));

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (ddlCountry.SelectedValue == "-1")
            {
                lblMsg.Text = "Please Select Country";
                ddlCountry.Focus();
                return;
            }

            if (Page.RouteData.Values["StateID"] != null)
            {
                cmd.Parameters.AddWithValue("@StateID", DBNullOrStringValue(Page.RouteData.Values["StateID"].ToString()));

                cmd.CommandText = "PR_State_UpdateByPK";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/State/Display");
            }
            else
            {
                cmd.Parameters.AddWithValue("UserID", DBNullOrStringValue(Session["UserID"].ToString()));

                cmd.CommandText = "PR_State_Insert";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Inserted Successfully";

                txtStateName.Text = "";

                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

                ddlCountry.SelectedValue = "-1";
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                lblMsg.Text = "Cannot insert duplicate value";
                txtStateName.Focus();
            }
        }
    }
    #endregion


    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry.Items.Remove(new ListItem("Select Country", "-1"));
        lblMsg.Text = "";
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