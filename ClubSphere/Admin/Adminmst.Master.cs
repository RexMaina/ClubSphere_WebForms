using System;
using System.Web;
using System.Web.UI;

namespace ClubSphere.Admin
{
    public partial class Adminmst : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is not logged in and is trying to access any page
            //if (Session["AdminUser"] == null && !Request.Url.AbsolutePath.Contains("Login.aspx"))
            {
                 //If not logged in, redirect to login page
                //Response.Redirect("~/Login.aspx");
            }
        }
    }
}
