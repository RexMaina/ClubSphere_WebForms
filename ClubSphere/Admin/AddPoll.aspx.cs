using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace ClubSphere.Admin
{
    public partial class AddPoll : System.Web.UI.Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateClubs();
            }
        }

        private void PopulateClubs()
        {
            string query = "SELECT ClubID, ClubName FROM Clubs";
            try
            {
                var dt = fn.Fetch(query, null);
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubID";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("--Select Club--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching clubs: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void AddPoll_Click(object sender, EventArgs e)
        {
            string clubId = ddlClub.SelectedValue;
            string question = txtQuestion.Value.Trim();
            bool isValidDate = DateTime.TryParse(txtExpirationDate.Value, out DateTime expirationDate);

            if (string.IsNullOrEmpty(clubId) || string.IsNullOrEmpty(question) || !isValidDate)
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.CssClass = "error";
                return;
            }

            string insertQuery = @"
                INSERT INTO Polls (ClubID, Question, CreationDate, ExpirationDate)
                VALUES (@ClubID, @Question, @CreationDate, @ExpirationDate)";

            var parameters = new Dictionary<string, object>
            {
                { "@ClubID", clubId },
                { "@Question", question },
                { "@CreationDate", DateTime.Now },
                { "@ExpirationDate", expirationDate }
            };

            try
            {
                fn.Query(insertQuery, parameters);

                // ✅ Email Notification to all members
                string subject = "📊 New Poll Available!";
                string body = $@"
                    <h3>New Poll Created</h3>
                    <p><strong>Question:</strong> {question}</p>
                    <p><strong>Expires on:</strong> {expirationDate:dddd, dd MMM yyyy}</p>
                    <p>🗳️ Visit the portal to vote now!</p>";

                DataTable members = fn.Fetch("SELECT Email FROM Members");

                foreach (DataRow row in members.Rows)
                {
                    string email = row["Email"].ToString();
                    new EmailService().SendEmail(email, subject, body, out _);
                }

                lblMessage.Text = "✅ Poll added and notifications sent!";
                lblMessage.CssClass = "success";

                ddlClub.SelectedIndex = 0;
                txtQuestion.Value = "";
                txtExpirationDate.Value = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }
    }
}
