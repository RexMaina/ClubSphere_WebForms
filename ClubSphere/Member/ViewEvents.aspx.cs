using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class ViewEvents : Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();
        string memberId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            memberId = GetMemberIdFromEmail(Session["Email"].ToString());

            if (string.IsNullOrEmpty(memberId))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadEvents();
            }
        }

        private string GetMemberIdFromEmail(string email)
        {
            string query = "SELECT MemberID FROM Members WHERE Email = @Email";
            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@Email", email }
            };
            DataTable dt = fn.Fetch(query, param);
            return dt.Rows.Count > 0 ? dt.Rows[0]["MemberID"].ToString() : null;
        }

        private void LoadEvents()
        {
            string query = @"
                SELECT 
                    E.EventID, E.EventName, E.Description, E.EventDate, E.Location, E.ClubName,
                    (SELECT COUNT(*) FROM EventParticipants WHERE EventID = E.EventID) AS ParticipantCount,
                    CASE 
                        WHEN EXISTS (
                            SELECT 1 FROM EventParticipants 
                            WHERE EventID = E.EventID AND MemberID = @MemberID
                        ) THEN 'Joined'
                        ELSE 'NotJoined'
                    END AS ParticipationStatus
                FROM Events E
                WHERE E.EventDate >= GETDATE()
                ORDER BY E.EventDate ASC";

            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@MemberID", memberId }
            };

            rptEvents.DataSource = fn.Fetch(query, param);
            rptEvents.DataBind();
        }

        protected void rptEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(memberId)) return;

            string eventId = e.CommandArgument.ToString();

            string eventQuery = "SELECT EventName, EventDate FROM Events WHERE EventID = @EventID";
            var eventParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@EventID", eventId }
            };
            DataTable eventDt = fn.Fetch(eventQuery, eventParam);

            if (eventDt.Rows.Count == 0)
            {
                LoadEvents();
                return;
            }

            string eventName = eventDt.Rows[0]["EventName"].ToString();
            string eventDate = Convert.ToDateTime(eventDt.Rows[0]["EventDate"]).ToString("dddd, dd MMMM yyyy");
            string userEmail = Session["Email"].ToString();
            string errorMessage;

            if (e.CommandName == "Join")
            {
                string insert = @"
                    INSERT INTO EventParticipants (EventID, MemberID, JoinDate)
                    VALUES (@EventID, @MemberID, GETDATE())";

                var param = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@EventID", eventId },
                    { "@MemberID", memberId }
                };
                fn.Query(insert, param);

                string subject = "📅 Event Joined Successfully!";
                string body = $"<h3>You have joined <b>{eventName}</b></h3><p>📍 Date: {eventDate}</p><p>Thanks for participating!</p>";

                new EmailService().SendEmail(userEmail, subject, body, out errorMessage);
            }
            else if (e.CommandName == "Cancel")
            {
                string delete = @"
                    DELETE FROM EventParticipants
                    WHERE EventID = @EventID AND MemberID = @MemberID";

                var param = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@EventID", eventId },
                    { "@MemberID", memberId }
                };
                fn.Query(delete, param);

                string subject = "❌ Event Participation Cancelled";
                string body = $"<h3>You have cancelled your registration for <b>{eventName}</b></h3><p>📅 Scheduled on: {eventDate}</p><p>If this was a mistake, feel free to rejoin anytime!</p>";

                new EmailService().SendEmail(userEmail, subject, body, out errorMessage);
            }

            LoadEvents();
        }
    }
}
