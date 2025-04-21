using System;
using System.Data;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ViewPosts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind the posts data to the GridView
                BindPosts();
            }
        }

        private void BindPosts()
        {
            try
            {
                // Query to fetch posts along with club names
                string query = @"
                    SELECT 
                        P.PostID, 
                        C.ClubName, 
                        P.MemberID, 
                        P.Title, 
                        P.Contents, 
                        P.PostDate 
                    FROM 
                        Posts P 
                    INNER JOIN 
                        Clubs C ON P.ClubID = C.ClubID";

                // Fetch data from the database
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, null);

                // Bind the data to the GridView
                gvPosts.DataSource = dt;
                gvPosts.DataBind();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}