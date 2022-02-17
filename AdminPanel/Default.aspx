<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AdminPanel_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .scroll {
            width:100%;
            max-height: 400px;
            overflow-y: scroll;
        }
        .scrolling {  
                position: absolute;  
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container align-items-center">

        <div class="row text-center m-5">
            <h1 class="">Contact List</h1>
        </div>

        <div class="row">
            <div class="table-responsive scroll">
                <asp:GridView runat="server" ID="gvContact" OnRowCommand="gvContact_RowCommand" CssClass="table table-hover table-bordered border-2">
                    <Columns>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# "~/AddressBook/AdminPanel/Contact/Edit/" + Eval("ContactID").ToString() %>' CssClass="btn btn-primary m-1"></asp:HyperLink>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="ContactID" CommandArgument='<%# Eval("ContactID").ToString() %>' CssClass="btn btn-danger m-1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row text-center m-4">
            <div>
                <asp:HyperLink runat="server" NavigateUrl="~/AddressBook/AdminPanel/Contact/Add" CssClass="btn btn-dark">Add Contact</asp:HyperLink>
            </div>
            <div>
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

