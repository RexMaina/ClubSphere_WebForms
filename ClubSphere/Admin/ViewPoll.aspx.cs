using System;
using System.Data;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ViewPoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind the polls and responses data to the GridView
                BindPolls();
            }
        }

        private void BindPolls()
        {
            try
            {
                // Query to fetch polls along with club names and responses
                string query = @"
                    SELECT 
                        P.Poll_Id, 
                        C.ClubName, 
                        P.Question, 
                        P.CreationDate, 
                        P.ExpirationDate, 
                        PR.Response, 
                        PR.MemberID 
                    FROM 
                        Polls P 
                    INNER JOIN 
                        Clubs C ON P.ClubID = C.ClubID 
                    LEFT JOIN 
                        PollResponses PR ON P.Poll_Id = PR.Poll_ID";

                // Fetch data from the database
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, null);

                // Bind the data to the GridView
                gvPolls.DataSource = dt;
                gvPolls.DataBind();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}