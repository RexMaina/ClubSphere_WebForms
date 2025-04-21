using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace ClubSphere.Admin
{
    public partial class AddPost : System.Web.UI.Page
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

        protected void AddPost_Click(object sender, EventArgs e)
        {
            string clubId = ddlClub.SelectedValue;
            bool isValidMemberId = int.TryParse(txtMemberID.Value, out int memberId);
            string title = txtTitle.Value.Trim();
            string contents = txtContents.Value.Trim();

            if (string.IsNullOrEmpty(clubId) || !isValidMemberId || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(contents))
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.CssClass = "error";
                return;
            }

            string query = @"
                INSERT INTO Posts (ClubID, MemberID, Title, Contents, PostDate)
                VALUES (@ClubID, @MemberID, @Title, @Contents, @PostDate)";

            var parameters = new Dictionary<string, object>
            {
                { "@ClubID", clubId },
                { "@MemberID", memberId },
                { "@Title", title },
                { "@Contents", contents },
                { "@PostDate", DateTime.Now }
            };

            try
            {
                fn.Query(query, parameters);

                // ✅ Email Notification
                string subject = "📝 New ClubSphere Post: " + title;
                string body = $@"
                    <h3>{title}</h3>
                    <p>{contents}</p>
                    <p><strong>Posted on:</strong> {DateTime.Now:dddd, dd MMM yyyy}</p>
                    <p>Visit the platform to view and engage with this post.</p>";

                DataTable members = fn.Fetch("SELECT Email FROM Members");

                foreach (DataRow row in members.Rows)
                {
                    string email = row["Email"].ToString();
                    new EmailService().SendEmail(email, subject, body, out _);
                }

                lblMessage.Text = "✅ Post added and notifications sent!";
                lblMessage.CssClass = "success";

                ddlClub.SelectedIndex = 0;
                txtMemberID.Value = "";
                txtTitle.Value = "";
                txtContents.Value = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }
    }
}
