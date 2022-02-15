using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
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
            txtCountryName.Focus();

            if (Page.RouteData.Values["CountryID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

                conn.Open();

                SqlCommand cmd = new SqlCommand("PR_Country_SelectByPK", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"]);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if(!read["CountryCode"].Equals(DBNull.Value))
                            txtCountryCode.Text = read["CountryCode"].ToString();

                        if (!read["CountryName"].Equals(DBNull.Value))
                            txtCountryName.Text = read["CountryName"].ToString();

                        
                    }
                }

                conn.Close();
            }
        }
    }
    #endregion

    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if (txtCountryName.Text.Trim() == "")
        {
            strErrorMessage += "Enter Country Name <br/>";
        }

        if (strErrorMessage != "")
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

            cmd.Parameters.AddWithValue("@CountryName", DBNullOrStringValue(txtCountryName.Text));

            cmd.Parameters.AddWithValue("@CountryCode", DBNullOrStringValue(txtCountryCode.Text));

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);


            if (Page.RouteData.Values["CountryID"] != null)
            {
                cmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"].ToString());

                cmd.CommandText = "PR_Country_UpdateByPK";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/Country/Display");
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                cmd.CommandText = "PR_Country_Insert";

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMsg.Text = "Data Inserted Successfully";

                txtCountryCode.Text = "";
                txtCountryName.Text = "";
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                lblMsg.Text = "Cannot insert duplicate value";
                txtCountryName.Focus();
            }
            else
            {
                lblMsg.Text = exec.Message.ToString();
            }
        }
    }
    #endregion

    private Object DBNullOrStringValue(String val)
    {
        if(String.IsNullOrEmpty(val))
        {
            return DBNull.Value;
        }
        return val;
    }
}