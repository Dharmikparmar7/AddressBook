using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_StateAddEdit : System.Web.UI.Page
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
            txtStateName.Focus();

            fillDropDown();

            if (Page.RouteData.Values["StateID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

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
                        txtStateName.Text = read["StateName"].ToString();
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


        }

    }
    private void fillDropDown()
    {
        SqlConnection conn = new SqlConnection("data source = DHARMIK-PARMAR; initial catalog = AddressBook; integrated security = true");

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
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["StateID"] != null)
        {
            try
            {

                SqlConnection conn = new SqlConnection("data source = DHARMIK-PARMAR; initial catalog = AddressBook; integrated security = true");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_State_UpdateByPK";

                cmd.Parameters.AddWithValue("@StateID", Page.RouteData.Values["StateID"].ToString());

                cmd.Parameters.AddWithValue("@StateName", txtStateName.Text.ToString().Trim());

                cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.ToString().Trim());

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                if (ddlCountry.SelectedValue == "-1")
                {
                    lblMsg.Text = "Please Select Country";
                    ddlCountry.Focus();
                    return;
                }

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/State/Display");
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
        else
        {
            try
            {

                SqlConnection conn = new SqlConnection("data source = DHARMIK-PARMAR; initial catalog = AddressBook; integrated security = true");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_State_Insert";

                cmd.Parameters.AddWithValue("@StateName", txtStateName.Text.ToString().Trim());

                cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.ToString().Trim());

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                if (ddlCountry.SelectedValue == "-1")
                {
                    lblMsg.Text = "Please Select Country";
                    ddlCountry.Focus();
                    return;
                }

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Inserted Successfully";

                txtStateName.Text = "";
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
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry.Items.Remove(new ListItem("Select Country", "-1"));
        lblMsg.Text = "";
    }
}