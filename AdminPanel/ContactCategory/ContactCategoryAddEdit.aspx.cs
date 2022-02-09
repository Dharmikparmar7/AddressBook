using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
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
            txtCCName.Focus();

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_ContactCategory_SelectByPK";

                cmd.Parameters.AddWithValue("@ContactCategoryID", Page.RouteData.Values["ContactCategoryID"].ToString());

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        txtCCName.Text = read["ContactCategoryName"].ToString();
                    }
                }

                conn.Close();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["ContactCategoryID"] != null)
        {
            try
            {

                SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_ContactCategory_UpdateByPK";

                cmd.Parameters.AddWithValue("@ContactCategoryId", Page.RouteData.Values["ContactCategoryID"].ToString());

                cmd.Parameters.AddWithValue("@ContactCategoryName", txtCCName.Text);

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.ExecuteNonQuery();

                conn.Close();

                txtMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/ContactCategory/Display");
            }
            catch (SqlException exec)
            {
                if (exec.Number == 2627)
                {
                    txtMsg.Text = "Cannot insert duplicate value";
                }
            }
        }

        else
        {
            try
            {

                SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

                conn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "PR_ContactCategory_Insert";

                cmd.Parameters.AddWithValue("@ContactCategoryName", txtCCName.Text);

                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                cmd.ExecuteNonQuery();

                conn.Close();

                txtMsg.Text = "Data Inserted Successfully";

                txtCCName.Text = "";
            }
            catch (SqlException exec)
            {
                if (exec.Number == 2627)
                {
                    txtMsg.Text = "Cannot insert duplicate value";
                }
            }
        }
    }
}