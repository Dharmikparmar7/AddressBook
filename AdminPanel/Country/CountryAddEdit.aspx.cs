using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
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
            txtCountryName.Focus();

            if (Page.RouteData.Values["CountryID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_Country_SelectByPK";

                cmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"]);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        txtCountryCode.Text = read["CountryCode"].ToString();
                        txtCountryName.Text = read["CountryName"].ToString();
                    }
                }

                conn.Close();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["CountryID"] != null)
        {
            try
            {
                SqlConnection conn = new SqlConnection("data source = DHARMIK-PARMAR; initial catalog = AddressBook; integrated security = true;");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_Country_UpdateByPK";

                cmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"].ToString());

                cmd.Parameters.AddWithValue("@CountryName", txtCountryName.Text);

                cmd.Parameters.AddWithValue("@CountryCode", txtCountryCode.Text);

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/Country/Display");
            }
            catch (SqlException exec)
            {
                if (exec.Number == 2627)
                {
                    lblMsg.Text = "Cannot insert duplicate value";
                    txtCountryName.Focus();
                }
            }
        }
        else
        {
            try
            {
                SqlConnection conn = new SqlConnection("data source = DHARMIK-PARMAR; initial catalog = AddressBook; integrated security = true;");
                    
                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_Country_Insert";

                cmd.Parameters.AddWithValue("@CountryName", txtCountryName.Text);

                cmd.Parameters.AddWithValue("@CountryCode", txtCountryCode.Text);

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Inserted Successfully";

                txtCountryCode.Text = "";
                txtCountryName.Text = "";
            }
            catch (SqlException exec)
            {
                if (exec.Number == 2627)
                {
                    lblMsg.Text = "Cannot insert duplicate value";
                    txtCountryName.Focus();
                }
            }
        }
    }
}