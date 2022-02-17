<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AdminPanel_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <%--<link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
        <script src="../../Content/js/bootstrap.min.js"></script>
        <script src="../../Content/js/jquery-3.6.0.min.js"></script>--%>
    <style>
        .row
        {
            margin:8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="myContainer">
            <div class="form-group">
                <div class="row text-center">
                    <h1 class="">City Add / Edit</h1>
                </div>
                <div class="row">
                    City Name :
                    <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCityName" ErrorMessage="Enter City Name" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    Pincode :
                    <asp:TextBox runat="server" ID="txtPincode" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPincode" ErrorMessage="Enter PinCode" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    STD Code :
                    <asp:TextBox runat="server" ID="txtSTDCode" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSTDCode" ErrorMessage="Enter STD Code" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    Select Country :
                    <asp:DropDownList runat="server" ID="ddlCountry" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select mt-2 mb-4">
                    </asp:DropDownList>
                </div>
                <div class="row">
                    Select State :
                    <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" CssClass="form-select mt-2 mb-4" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Select State</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="row ">
                    <div>
                        <asp:Button runat="server" ID="btnAddCity" Text="Add City" OnClick="btnAddCity_Click" CssClass="btn btn-success" />
                        <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/City/Display" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                    </div>
                </div>
                <div class="row mt-3">
                    <asp:Label runat="server" ID="lblMessage" CssClass="float-right"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

