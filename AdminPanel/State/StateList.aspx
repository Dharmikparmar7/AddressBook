<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container m-5">
        <div class="row text-center m-5">
            <h1 class="">State List</h1>
        </div>

        <asp:GridView runat="server" ID="gvState" OnRowCommand="gvState_RowCommand" CssClass="table table-hover table-bordered border-2">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# "~/AddressBook/AdminPanel/State/Edit/" + Eval("StateID").ToString() %>' CssClass="btn btn-primary"></asp:HyperLink>
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="StateId" CommandArgument='<%# Eval("StateID") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="row text-center m-5">
            <div>
                <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/State/Add" CssClass="btn btn-dark">Add State</asp:HyperLink>
            </div>
            <div>
                <asp:Label runat="server" ID="lbl"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

