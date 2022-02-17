<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-control {
            margin-bottom: 20px;
            margin-top: 10px;
        }

        .row
        {
            margin:10px;
        }

        .myContainer {
            padding: 50px;
            margin: 20px;
            border: 2px solid black;
            border-radius: 15px;
            width: 100%;
            height: 100%;
            margin-bottom:150px;
        }

        .scroll2 {
            width:100%;
            height: 600px;
            overflow: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container pb-1" >
            <div class="myContainer">
                <div class="form-group">
                    <div class="row text-center">
                        <h1>Contact Add / Edit</h1>
                    </div>

                    <div>
                        <div class="row">
                            Contact Name : 
                            <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContactName" ErrorMessage="Enter Contact Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            Select Contact Category :
                            <asp:DropDownList runat="server" ID="ddlContactCategory" CssClass="form-control" OnSelectedIndexChanged="ddlContactCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="row">
                            Address : 
                            <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" ErrorMessage="Enter Address" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            Pincode :
                            <asp:TextBox runat="server" ID="txtPincode" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPincode" ErrorMessage="Enter Pincode" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            Email Address :
                            <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter Email" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            Mobile Number :
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" ErrorMessage="Enter Mobile Number" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            Select Country :
                            <asp:DropDownList runat="server" ID="ddlCountry" OnTextChanged="ddlCountry_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="row">
                            Select State :
                            <asp:DropDownList runat="server" ID="ddlState" OnTextChanged="ddlState_TextChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem Value="-1">Select State</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="row">
                            Select City :
                            <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="-1">Select City</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="row">
                            <div>
                                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-success" />
                                <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/Contact/Display" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <asp:Label runat="server" ID="lblMessage"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        </div>
</asp:Content>

