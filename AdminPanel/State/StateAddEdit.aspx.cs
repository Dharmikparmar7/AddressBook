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

        if (Session["UserID"] != null)
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

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
        if (ddlCountry.SelectedIndex == 0)
        {
            strErrorMessage += "Select Country";
        }
        if (strErrorMessage.Trim() != "")
        {
            lblMsg.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Local Variables
        SqlString strStateName = SqlString.Null;
        SqlInt32 strCountryID = SqlInt32.Null;
        #endregion Local Variables

        #region Gather Information
        if (ddlCountry.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        if (txtStateName.Text.Trim() != "")
        {
            strStateName = txtStateName.Text.Trim();
        }
        #endregion Gather Information


        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StateName", strStateName);

            cmd.Parameters.AddWithValue("@CountryID", strCountryID);

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["StateID"] != null)
            {
                cmd.Parameters.AddWithValue("@StateID", Page.RouteData.Values["StateID"].ToString());

                cmd.CommandText = "PR_State_UpdateByPK";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/State/Display");
            }
            else
            {
                if(Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("UserID", Session["UserID"].ToString());

                cmd.CommandText = "PR_State_Insert";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Inserted Successfully";

                txtStateName.Text = "";

                ddlCountry.SelectedIndex = 0;
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
        lblMsg.Text = "";
    }
}