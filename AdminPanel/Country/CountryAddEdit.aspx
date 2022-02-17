<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-control {
            margin-top:20px;
            margin-bottom: 20px;
        }

        .row{
            margin:10px;
        }

        .myContainer {
            padding: 50px;
            margin: 20px;
            border: 2px solid black;
            border-radius: 15px;
            width: 100%;
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
            <div class="myContainer">
                <div class="form-group">
                    <div class="row text-center">
                        <h1 class="">Country Add / Edit</h1>
                    </div>

                    <div class="row">
                        Country Name : 
                        <asp:TextBox runat="server" ID="txtCountryName" CssClass="form-control "></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCountryName" ErrorMessage="Enter Country Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        Country Code :
                        <asp:TextBox runat="server" ID="txtCountryCode" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCountryCode" ErrorMessage="Enter Country Code" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        <div>
                            <asp:Button runat="server" ID="btnAdd" Text="Add Country" OnClick="btnAdd_Click" CssClass="btn btn-success" />
                            <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/Country/Display" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

