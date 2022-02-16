﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
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
            txtCCName.Focus();

            #region LoadControl
            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                btnAdd.Text = "Save";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

                conn.Open();

                SqlCommand cmd = new SqlCommand("PR_ContactCategory_SelectByPK", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContactCategoryID", (Page.RouteData.Values["ContactCategoryID"].ToString()));

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (!read["ContactCategoryName"].Equals(DBNull.Value))
                            txtCCName.Text = read["ContactCategoryName"].ToString();

                        break;
                    }
                }

                conn.Close();
            }
            #endregion
        }
    }
    #endregion

    #region Add and Edit
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Server Side Validation
        String strErrorMessage = "";

        if(txtCCName.Text.Trim() == "")
        {
            strErrorMessage = "Enter Contact Category Name<br/>";
        }
        if (strErrorMessage.Trim() != "")
        {
            txtMsg.Text = strErrorMessage;
            return;
        }

        #region Local Variables
        SqlString strContactCategoryName = SqlString.Null;
        SqlConnection objConnection = new SqlConnection();
        objConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        #endregion Local Variables

        #region Gather Information
        if (txtCCName.Text.Trim() != "")
        {
            strContactCategoryName = txtCCName.Text.Trim();
        }
        #endregion Gather Information

        #endregion Server Side Validation
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);

            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                cmd.Parameters.AddWithValue("@ContactCategoryId", (Page.RouteData.Values["ContactCategoryID"].ToString()));

                cmd.CommandText = "PR_ContactCategory_UpdateByPK";

                cmd.ExecuteNonQuery();

                conn.Close();

                txtMsg.Text = "Data Updated Successfully";

                Response.Redirect("~/AddressBook/AdminPanel/ContactCategory/Display");
            }
            else
            {
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("UserID", (Session["UserID"].ToString()));

                cmd.CommandText = "PR_ContactCategory_Insert";

                cmd.ExecuteNonQuery();

                conn.Close();

                txtMsg.Text = "Data Inserted Successfully";

                txtCCName.Text = "";
            }
        }
        catch (SqlException exec)
        {
            if (exec.Number == 2627)
            {
                txtMsg.Text = "Cannot insert duplicate value";
            }
        }

    }
    #endregion
}