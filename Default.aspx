<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="~/Content/js/bootstrap.min.js"></script>
    <script src="~/Content/js/jquery-3.6.0.min.js"></script>
    <style>
        .myContainer {
            padding: 50px;
            margin: 20px;
            border: 2px solid black;
            border-radius: 15px;
            width: 100%;
            height: 100%;
        }

        .gradient-custom-3 {
            background: #84fab0;
            background: -webkit-linear-gradient(to right, rgba(132, 250, 176, 0.5), rgba(143, 211, 244, 0.5));
            background: linear-gradient(to right, rgba(132, 250, 176, 0.5), rgba(143, 211, 244, 0.5));
        }

        .gradient-custom-4 {
            background: #84fab0;
            background: -webkit-linear-gradient(to right, rgba(132, 250, 176, 1), rgba(143, 211, 244, 1));
            background: linear-gradient(to right, rgba(132, 250, 176, 1), rgba(143, 211, 244, 1));
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="vh-100 bg-image img-fluid" style="background-image: url('https://mdbcdn.b-cdn.net/img/Photos/new-templates/search-box/img4.webp');">
            <div class="mask d-flex align-items-center h-100 gradient-custom-3">
                <div class="container h-100">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                        <div class="col-12 col-md-9 col-lg-7 col-xl-6">
                            <div class="card" style="border-radius: 15px;">
                                <div class="card-body p-5">
                                    <h2 class="text-uppercase text-center mb-5">Login</h2>

                                    <div class="form-outline mb-4">
                                        <label class="form-label" for="form3Example1cg">Username</label>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfUsername" runat="server" CssClass="" ErrorMessage="Enter Username" ForeColor="red" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-4">
                                        <label class="form-label" for="form3Example4cg">Password</label>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Password" ForeColor="red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="d-flex justify-content-center">
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success btn-block btn-lg gradient-custom-4 text-body" OnClick="btnLogin_Click" />
                                    </div>
                                    <div class="d-flex justify-content-center mt-4">
                                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                                    </div>
                                    <p class="text-center text-muted mt-5 mb-0">Didn't Register ? <a href="#!" class="fw-bold text-body"><u><asp:HyperLink ID="hlRegister" NavigateUrl="~/AddressBook/AdminPanel/Register" CssClass="fw-bold text-body" runat="server">Register Here</asp:HyperLink></u></a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
