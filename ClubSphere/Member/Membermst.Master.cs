using System;
using System.Web;
using System.Web.UI;

namespace ClubSphere.Member
{
    public partial class Membermst : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ✅ Check if the user is logged in using Email
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page if the member is not logged in
            }
            else
            {
                // Optionally, you can load general member data for the entire page (such as profile information)
                // If needed, you can call LoadMemberData() here as well, or let individual content pages handle their own data loading.
            }
        }
    }
}
