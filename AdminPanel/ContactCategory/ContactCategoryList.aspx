<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="AdminPanel_Contact_Category_ContactCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container align-items-center">
        <div class="row text-center m-5">
            <h1 class="">Contact Category List</h1>
        </div>

        <div class="row">
            <div class="table-responsive scroll">
                <asp:GridView runat="server" ID="gvContactCategory" OnRowCommand="gvContactCategory_RowCommand" CssClass="table table-hover table-bordered border-2">
                    <Columns>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# "~/AddressBook/AdminPanel/ContactCategory/Edit/" + Eval("ContactCategoryID").ToString()
 %>'
                                    CssClass="btn btn-primary"></asp:HyperLink>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandArgument='<%# Eval("ContactCategoryID") %>' CssClass="btn btn-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>

                <div class="row text-center m-4">
                    <div>
                        <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/ContactCategory/Add" CssClass="btn btn-dark">Add Contact Category</asp:HyperLink>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="lbl"></asp:Label>
                    </div>
                </div>
            </div>
</asp:Content>

