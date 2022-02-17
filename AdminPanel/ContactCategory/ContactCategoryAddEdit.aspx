<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-control {
            margin-bottom: 20px;
            margin-top:10px;
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
                        <h1 class="">Contact Category Add / Edit</h1>
                    </div>

                    <div class="row">
                        <label>Contact Category Name : </label>
                        <asp:TextBox runat="server" ID="txtCCName" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCCName" ErrorMessage="Enter Contact Category Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        <div>
                            <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-success"/>
                            <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/ContactCategory/Display" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                        </div>

                    </div>

                    <div class="row">
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

