using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class JoinClub : Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();
        int memberId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!GetMemberIdFromSession()) return;

            if (!IsPostBack)
            {
                LoadClubs();
                LoadJoinedClub();
            }
        }

        private bool GetMemberIdFromSession()
        {
            if (Session["MemberID"] != null)
            {
                memberId = Convert.ToInt32(Session["MemberID"]);
                return true;
            }

            if (Session["Email"] != null)
            {
                string query = "SELECT MemberID FROM Members WHERE Email = @Email";
                var param = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@Email", Session["Email"] }
                };
                DataTable dt = fn.Fetch(query, param);

                if (dt.Rows.Count > 0)
                {
                    memberId = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                    Session["MemberID"] = memberId;
                    return true;
                }
            }

            Response.Redirect("~/Login.aspx");
            return false;
        }

        private void LoadClubs()
        {
            string query = "SELECT ClubID, ClubName FROM Clubs";
            ddlClubs.DataSource = fn.Fetch(query);
            ddlClubs.DataTextField = "ClubName";
            ddlClubs.DataValueField = "ClubID";
            ddlClubs.DataBind();
            ddlClubs.Items.Insert(0, new ListItem("-- Select Club --", "0"));
        }

        private void LoadJoinedClub()
        {
            string query = "SELECT ChooseClub FROM Members WHERE MemberID = @MemberID";
            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@MemberID", memberId }
            };
            DataTable dt = fn.Fetch(query, param);

            if (dt.Rows.Count > 0 && dt.Rows[0]["ChooseClub"] != DBNull.Value)
            {
                lblStatus.Text = "You have already joined: <b>" + dt.Rows[0]["ChooseClub"].ToString() + "</b>";
                btnJoin.Enabled = false;
                btnLeave.Visible = true;
            }
            else
            {
                lblStatus.Text = "You have not joined any club yet.";
                btnJoin.Enabled = true;
                btnLeave.Visible = false;
            }
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GetMemberIdFromSession()) return;

                if (ddlClubs.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select a club.";
                    lblMessage.CssClass = "text-danger";
                    return;
                }

                string clubName = ddlClubs.SelectedItem.Text;
                string clubId = ddlClubs.SelectedValue;

                string checkQuery = "SELECT ChooseClub FROM Members WHERE MemberID = @MemberID";
                var checkParam = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@MemberID", memberId }
                };
                DataTable dt = fn.Fetch(checkQuery, checkParam);

                if (dt.Rows.Count > 0 && dt.Rows[0]["ChooseClub"] != DBNull.Value)
                {
                    lblMessage.Text = "❌ You have already joined a club. Leave current club to join another.";
                    lblMessage.CssClass = "text-danger";
                    return;
                }

                string updateQuery = "UPDATE Members SET ChooseClub = @ChooseClub WHERE MemberID = @MemberID";
                var updateParam = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@ChooseClub", clubName },
                    { "@MemberID", memberId }
                };
                fn.Query(updateQuery, updateParam);

                string insertQuery = "INSERT INTO Memberships (MemberID, ClubID, Role) VALUES (@MemberID, @ClubID, 'Member')";
                var insertParam = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@MemberID", memberId },
                    { "@ClubID", clubId }
                };
                fn.Query(insertQuery, insertParam);


                lblMessage.Text = "✅ Joined " + clubName + " successfully!";
                lblMessage.CssClass = "text-success";
                btnJoin.Enabled = false;
                LoadJoinedClub();

                // ✅ Send email
                string userEmail = Session["Email"].ToString();
                string subject = "🎉 Club Joined Successfully!";
                string body = $"<h3>Welcome to {clubName}</h3><p>You’ve successfully joined <b>{clubName}</b>. Stay tuned for events and updates!</p>";
                string errorMessage;

                bool emailSent = new EmailService().SendEmail(userEmail, subject, body, out errorMessage);
                if (!emailSent)
                    lblMessage.Text += $"<br/>⚠️ Email not sent: {errorMessage}";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ ERROR: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }

        protected void btnLeave_Click(object sender, EventArgs e)
        {
            try
            {
                string updateQuery = "UPDATE Members SET ChooseClub = NULL WHERE MemberID = @MemberID";
                var param = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@MemberID", memberId }
                };
                fn.Query(updateQuery, param);

                lblMessage.Text = "✅ You have left your club.";
                lblMessage.CssClass = "text-success";
                LoadJoinedClub();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ ERROR: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }
    }
}
