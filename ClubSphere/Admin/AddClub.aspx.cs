using System;
using System.Collections.Generic;
using System.Data;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class AddClub : System.Web.UI.Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCreationDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnAddClub_Click(object sender, EventArgs e)
        {
            string clubName = txtClubName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string creationDate = txtCreationDate.Text.Trim();

            if (string.IsNullOrEmpty(clubName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(creationDate))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.CssClass = "message error";
                return;
            }

            var parameters = new Dictionary<string, object>
            {
                { "@ClubName", clubName },
                { "@Description", description },
                { "@CreationDate", creationDate }
            };

            string query = "INSERT INTO Clubs (ClubName, Description, CreationDate) VALUES (@ClubName, @Description, @CreationDate)";

            try
            {
                fn.Query(query, parameters);

                // ✅ Notify all members
                string subject = "📢 New Club Created: " + clubName;
                string body = $@"
                    <h3>🎉 Club Created: {clubName}</h3>
                    <p><strong>Description:</strong> {description}</p>
                    <p><strong>Date:</strong> {creationDate}</p>
                    <p>You can now visit the ClubSphere portal to view and join this club!</p>";

                DataTable members = fn.Fetch("SELECT Email FROM Members");

                foreach (DataRow row in members.Rows)
                {
                    string email = row["Email"].ToString();
                    new EmailService().SendEmail(email, subject, body, out string _);
                }

                lblMessage.Text = "✅ Club added and members notified successfully!";
                lblMessage.CssClass = "message success";

                txtClubName.Text = "";
                txtDescription.Text = "";
                txtCreationDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "message error";
            }
        }
    }
}
