using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ViewEvents : System.Web.UI.Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEvents();
            }
        }

        private void BindEvents()
        {
            string query = @"
                SELECT 
                    E.EventID,
                    E.EventName, 
                    E.ClubName, 
                    E.Description, 
                    E.EventDate, 
                    E.Location, 
                    E.MaxParticipants, 
                    E.Status,
                    COUNT(EP.MemberID) AS JoinedCount
                FROM Events E
                LEFT JOIN EventParticipants EP ON E.EventID = EP.EventID
                GROUP BY 
                    E.EventID, E.EventName, E.ClubName, E.Description, 
                    E.EventDate, E.Location, 
                    E.MaxParticipants, E.Status
                ORDER BY E.EventDate DESC";

            DataTable dt = fn.Fetch(query, null);
            gvEvents.DataSource = dt;
            gvEvents.DataBind();
        }

        protected void gvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewMembers")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEvents.Rows[index];
                string eventId = gvEvents.DataKeys[index].Value.ToString();
                LoadEventMembers(eventId);
            }
        }

        private void LoadEventMembers(string eventId)
        {
            string query = @"
                SELECT 
                    M.Name AS [Member Name], 
                    M.Email AS [Email], 
                    CONVERT(varchar, EP.JoinDate, 106) AS [Join Date]
                FROM EventParticipants EP
                INNER JOIN Members M ON EP.MemberID = M.MemberID
                WHERE EP.EventID = @EventID";

            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@EventID", eventId }
            };

            DataTable dt = fn.Fetch(query, param);
            gvParticipants.DataSource = dt;
            gvParticipants.DataBind();
            pnlParticipants.Visible = true;
        }
    }
}
