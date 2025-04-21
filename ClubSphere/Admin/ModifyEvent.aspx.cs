using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

namespace ClubSphere.Admin
{
    public partial class ModifyEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateEvents();
                PopulateClubs();
            }
        }

        private void PopulateEvents()
        {
            string query = "SELECT EventID, EventName FROM Events";
            var parameters = new Dictionary<string, object>();

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                ddlEvent.DataSource = dt;
                ddlEvent.DataTextField = "EventName";
                ddlEvent.DataValueField = "EventID";
                ddlEvent.DataBind();
                ddlEvent.Items.Insert(0, new ListItem("--Select Event--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching events: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        private void PopulateClubs()
        {
            string query = "SELECT ClubName FROM Clubs";
            var parameters = new Dictionary<string, object>();

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

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

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvent.SelectedIndex > 0)
            {
                int eventId = int.Parse(ddlEvent.SelectedValue);
                LoadEventDetails(eventId);
                pnlEventDetails.Visible = true;
            }
            else
            {
                pnlEventDetails.Visible = false;
                ClearEventDetails();
            }
        }

        private void ClearEventDetails()
        {
            txtEventName.Value = "";
            ddlClubName.SelectedIndex = 0;
            txtDescription.Value = "";
            txtEventDate.Value = "";
            txtLocation.Value = "";
            txtMaxParticipants.Value = "";
        }

        private void LoadEventDetails(int eventId)
        {
            string query = "SELECT EventName, ClubName, Description, EventDate, Location, MaxParticipants " +
                           "FROM Events WHERE EventID = @EventID";

            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    txtEventName.Value = dt.Rows[0]["EventName"].ToString();
                    ddlClubName.SelectedValue = dt.Rows[0]["ClubName"].ToString();
                    txtDescription.Value = dt.Rows[0]["Description"].ToString();
                    txtEventDate.Value = DateTime.Parse(dt.Rows[0]["EventDate"].ToString()).ToString("yyyy-MM-dd");
                    txtLocation.Value = dt.Rows[0]["Location"].ToString();

                    if (dt.Rows[0]["MaxParticipants"] != DBNull.Value)
                    {
                        txtMaxParticipants.Value = dt.Rows[0]["MaxParticipants"].ToString();
                    }
                    else
                    {
                        txtMaxParticipants.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading event details: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void UpdateEvent_Click(object sender, EventArgs e)
        {
            int eventId = int.Parse(ddlEvent.SelectedValue);

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

            string query = "UPDATE Events SET EventName = @EventName, ClubName = @ClubName, Description = @Description, " +
                           "EventDate = @EventDate, Location = @Location, MaxParticipants = @MaxParticipants " +
                           "WHERE EventID = @EventID";

            var parameters = new Dictionary<string, object>
            {
                { "@EventName", eventName },
                { "@ClubName", clubName },
                { "@Description", description },
                { "@EventDate", eventDate },
                { "@Location", location },
                { "@MaxParticipants", maxParticipants.HasValue ? (object)maxParticipants.Value : DBNull.Value },
                { "@EventID", eventId }
            };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Event updated successfully!";
                lblMessage.CssClass = "success";
                LoadEventDetails(eventId); // Reload the event details to reflect changes
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating event: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void DeleteEvent_Click(object sender, EventArgs e)
        {
            int eventId = int.Parse(ddlEvent.SelectedValue);

            string query = "DELETE FROM Events WHERE EventID = @EventID";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Event deleted successfully!";
                lblMessage.CssClass = "success";

                // Reset the form after successful deletion:
                ddlEvent.SelectedIndex = 0;
                pnlEventDetails.Visible = false;
                ClearEventDetails();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting event: " + ex.Message;
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