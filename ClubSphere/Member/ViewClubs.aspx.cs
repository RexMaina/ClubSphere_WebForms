using System;
using System.Data;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class ViewClubs : Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClubs();
                LoadJoinedClubs();
            }
        }

        private void LoadClubs()
        {
            string query = "SELECT ClubName FROM Clubs";  // Removed Description
            gvAllClubs.DataSource = fn.Fetch(query);
            gvAllClubs.DataBind();
        }

        private void LoadJoinedClubs()
        {
            if (Session["MemberID"] == null) return;

            string query = @"SELECT C.ClubName FROM Clubs C
                             INNER JOIN Memberships M ON C.ClubID = M.ClubID
                             WHERE M.MemberID = @MemberID";

            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@MemberID", Session["MemberID"] }
            };

            gvJoinedClubs.DataSource = fn.Fetch(query, param);
            gvJoinedClubs.DataBind();
        }

        protected void btnJoinClub_Click(object sender, EventArgs e)
        {
            Response.Redirect("JoinClub.aspx");
        }
    }
}
