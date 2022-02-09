<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
    public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathInfo}");

        routes.MapPageRoute(
            "Route_Login",
            "AddressBook/AdminPanel/Login",
            "~/Default.aspx");

        routes.MapPageRoute(
            "Route_Register",
            "AddressBook/AdminPanel/Register",
            "~/Register.aspx");
        
        //Home, Contact List
        routes.MapPageRoute(
            "Route_Home",
            "AddressBook/AdminPanel/Contact/Display",
            "~/AdminPanel/Default.aspx");

        //Country
        routes.MapPageRoute(
            "Route_Country_Display",
            "AddressBook/AdminPanel/Country/Display",
            "~/AdminPanel/Country/CountryList.aspx");

        routes.MapPageRoute(
            "Route_Country_Add",
            "AddressBook/AdminPanel/Country/Add",
            "~/AdminPanel/Country/CountryAddEdit.aspx");

        routes.MapPageRoute(
            "Route_Country_Edit",
            "AddressBook/AdminPanel/Country/Edit/{CountryID}",
            "~/AdminPanel/Country/CountryAddEdit.aspx");

        //State
        routes.MapPageRoute(
            "Route_State_Add",
            "AddressBook/AdminPanel/State/Add",
            "~/AdminPanel/State/StateAddEdit.aspx");

        routes.MapPageRoute(
            "Route_State_Display",
            "AddressBook/AdminPanel/State/Display",
            "~/AdminPanel/State/StateList.aspx");

        routes.MapPageRoute(
            "Route_State_Edit",
            "AddressBook/AdminPanel/State/Edit/{StateID}",
            "~/AdminPanel/State/StateAddEdit.aspx");
        
        //City
        routes.MapPageRoute(
            "Route_City_Display",
            "AddressBook/AdminPanel/City/Display",
            "~/AdminPanel/City/CityList.aspx");

        routes.MapPageRoute(
            "Route_City_Add",
            "AddressBook/AdminPanel/City/Add",
            "~/AdminPanel/City/CityAddEdit.aspx");

        routes.MapPageRoute(
            "Route_City_Edit",
            "AddressBook/AdminPanel/City/Edit/{CityID}",
            "~/AdminPanel/City/CityAddEdit.aspx");
        
        //Contact Category
        routes.MapPageRoute(
            "Route_ContactCategory_Display",
            "AddressBook/AdminPanel/ContactCategory/Display",
            "~/AdminPanel/ContactCategory/ContactCategoryList.aspx");

        routes.MapPageRoute(
            "Route_ContactCategory_Add",
            "AddressBook/AdminPanel/ContactCategory/Add",
            "~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx");

        routes.MapPageRoute(
            "Route_ContactCategory_Edit",
            "AddressBook/AdminPanel/ContactCategory/Edit/{ContactCategoryID}",
            "~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx");
        
        //Contact
        routes.MapPageRoute(
            "Route_Contact_Add",
            "AddressBook/AdminPanel/Contact/Add",
            "~/AdminPanel/Contact/ContactAddEdit.aspx");

        routes.MapPageRoute(
            "Route_Contact_Edit",
            "AddressBook/AdminPanel/Contact/Edit/{ContactID}",
            "~/AdminPanel/Contact/ContactAddEdit.aspx");
    }
       
</script>
