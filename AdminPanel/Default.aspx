<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AdminPanel_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container m-5">

        <div class="row text-center m-5">
            <h1 class="">Contact List</h1>
        </div>

        <asp:GridView runat="server" ID="gvContact" OnRowCommand="gvContact_RowCommand" CssClass="table table-hover table-bordered border-2">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# "~/AddressBook/AdminPanel/Contact/Edit/" + Eval("ContactID").ToString() %>' CssClass="btn btn-primary"></asp:HyperLink>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="ContactID" CommandArgument='<%# Eval("ContactID").ToString() %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="row text-center m-5">
            <div>
                <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/Contact/Add" CssClass="btn btn-dark">Add Contact</asp:HyperLink>
            </div>
            <div>
                <asp:Label runat="server" ID="lbl"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

