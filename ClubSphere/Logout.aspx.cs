using System;
using System.Web;
using System.Web.UI;
using System.Web.Security; // If you are using Forms Authentication

namespace ClubSphere
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Clear the current session
                Session.Clear();
                Session.Abandon();

                // Optionally clear the session cookie
                if (Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    HttpCookie cookie = new HttpCookie("ASP.NET_SessionId");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }

                // If you are using Forms Authentication, sign out the user
                FormsAuthentication.SignOut();

                // Prevent caching of the page
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();

                // Redirect the user to the Login page
                Response.Redirect("Login.aspx");
                Response.End(); // Prevent further code execution
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the logout process
                // Log the error or show a message to the user
                Response.Write("Error during logout: " + ex.Message);
            }
        }
    }
}
