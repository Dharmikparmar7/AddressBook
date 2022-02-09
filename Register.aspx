<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="~/Content/js/bootstrap.min.js"></script>
    <script src="~/Content/js/jquery-3.6.0.min.js"></script>
    <style>
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
        <section style="background-image: url('https://mdbcdn.b-cdn.net/img/Photos/new-templates/search-box/img4.webp'); background-repeat: space">
            <div class="mask d-flex align-items-center h-100 gradient-custom-3 p-5">
                <div class="container h-100">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                        <div class="col-12 col-md-9 col-lg-7 col-xl-6">
                            <div class="card" style="border-radius: 15px;">
                                <div class="card-body p-5">
                                    <h2 class="text-uppercase text-center mb-5">Create an account</h2>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Full Name</label>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Fullname" ForeColor="red" ControlToValidate="txtFullname"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">User Name</label>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Username" ForeColor="red" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Address</label>
                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Address" ForeColor="red" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Password</label>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Password" ForeColor="red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ErrorMessage="</br>Minimum 8 characters </br> Should have at least one number </br> Should have at least one upper case </br>Should have at least one lower case </br> Should have at least one special character </br> </br>"
                                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$" ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Confirm Password</label>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Password" ForeColor="red" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ErrorMessage="Passwords do not match" ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Mobile Number</label>
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Mobile Number" ForeColor="red" ControlToValidate="txtMobileNo"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="" class="form-label">Email</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Enter Email Address" ForeColor="red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-outline mb-1">
                                        <label for="txtUserName" class="form-label">Upload Photo</label>
                                        <asp:FileUpload runat="server" ID="fuImage" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="" ErrorMessage="Select Image" ForeColor="red" ControlToValidate="fuImage"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="d-flex justify-content-center">
                                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success btn-block btn-lg gradient-custom-4 text-body" OnClick="btnRegister_Click" />

                                    </div>

                                    <div class="d-flex justify-content-center m-3">
                                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
                                    </div>

                                    <p class="text-center text-muted mt-5 mb-0">
                                        Have already an account? <a href="#!" class=""><u>
                                            <asp:HyperLink ID="hlRegister" NavigateUrl="~/AddressBook/AdminPanel/Login" CssClass="fw-bold text-body" runat="server">Login Here</asp:HyperLink></u></a>
                                    </p>
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
