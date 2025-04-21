using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Data;
using System.Web.UI.WebControls;

namespace ClubSphere.Admin
{
    public partial class AddEvent : System.Web.UI.Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateClubs();
            }
        }

        protected void AddEvent_Click(object sender, EventArgs e)
        {
            string eventName = txtEventName.Value.Trim();
            string clubName = ddlClubName.SelectedValue;
            string description = txtDescription.Value.Trim();
            DateTime eventDate;
            bool isValidDate = DateTime.TryParse(txtEventDate.Value, out eventDate);
            string location = txtLocation.Value.Trim();
            int? maxParticipants = string.IsNullOrEmpty(txtMaxParticipants.Value) ? (int?)null : int.Parse(txtMaxParticipants.Value);

            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(clubName) || !isValidDate || string.IsNullOrEmpty(location))
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.CssClass = "error";
                return;
            }

            string query = @"INSERT INTO Events 
                            (EventName, Description, EventDate, Location, MaxParticipants, Status, ClubName)
                            VALUES 
                            (@EventName, @Description, @EventDate, @Location, @MaxParticipants, @Status, @ClubName)";

            var parameters = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@EventName", eventName },
                { "@Description", description },
                { "@EventDate", eventDate },
                { "@Location", location },
                { "@MaxParticipants", maxParticipants.HasValue ? (object)maxParticipants.Value : DBNull.Value },
                { "@Status", "Upcoming" },
                { "@ClubName", clubName }
            };

            try
            {
                fn.Query(query, parameters);

                // ✅ Send email to all members
                string subject = "📢 New Event Announcement: " + eventName;
                string body = $@"
                    <h3>New Event: {eventName}</h3>
                    <p><strong>Date:</strong> {eventDate:dddd, dd MMM yyyy}</p>
                    <p><strong>Location:</strong> {location}</p>
                    <p><strong>Club:</strong> {clubName}</p>
                    <p><strong>Description:</strong> {description}</p>
                    <p>Please visit the portal to participate or get more details.</p>";

                DataTable members = fn.Fetch("SELECT Email FROM Members");

                foreach (DataRow row in members.Rows)
                {
                    string email = row["Email"].ToString();
                    new EmailService().SendEmail(email, subject, body, out string errorMessage);
                }

                lblMessage.Text = "✅ Event added and members notified successfully!";
                lblMessage.CssClass = "success";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        private void PopulateClubs()
        {
            string query = "SELECT ClubName FROM Clubs";

            try
            {
                var dt = fn.Fetch(query);

                ddlClubName.DataSource = dt;
                ddlClubName.DataTextField = "ClubName";
                ddlClubName.DataValueField = "ClubName";
                ddlClubName.DataBind();
                ddlClubName.Items.Insert(0, new ListItem("--Select Club--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching clubs: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void calEventDate_SelectionChanged(object sender, EventArgs e)
        {
            txtEventDate.Value = calEventDate.SelectedDate.ToString("yyyy-MM-dd");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideCalendar", "toggleCalendar();", true);
        }
    }
}
