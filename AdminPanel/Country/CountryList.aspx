<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="AdminPanel_Country_CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container m-5">
        <div class="row text-center m-5">
            <h1 class="">Country List</h1>
        </div>

        <asp:GridView runat="server" ID="gvCountry" OnRowCommand="gvCountry_RowCommand" CssClass="table table-hover table-bordered border-2">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# "~/AddressBook/AdminPanel/Country/Edit/" + Eval("CountryID").ToString() %>' CssClass="btn btn-primary"></asp:HyperLink>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="CountryId" CommandArgument='<%# Eval("CountryID") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="row text-center m-5">
            <div>
                <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/Country/Add" CssClass="btn btn-dark">Add Country</asp:HyperLink>
            </div>
            <div>
                <asp:Label runat="server" ID="lbl"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

