﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryList : System.Web.UI.Page
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
            fillDropdown();
        }
    }

    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection("data source=DHARMIK-PARMAR;initial catalog=AddressBook;Integrated Security=true;");

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "PR_Country_DeleteByPK";

            cmd.Parameters.AddWithValue("@CountryID", e.CommandArgument.ToString());

            cmd.ExecuteScalar();

            conn.Close();
        }
        catch (SqlException exec)
        {
            if(exec.Number == 547)
            {
                lbl.Text = "Could not delete the record as it is used as foregin key";
            }
        }
        finally
        {
            fillDropdown();
        }
    }

    private void fillDropdown()
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

        gvCountry.DataSource = read;

        gvCountry.DataBind();

        conn.Close();
    }
}