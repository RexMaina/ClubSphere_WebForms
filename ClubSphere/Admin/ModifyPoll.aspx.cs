using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ClubSphere.Admin
{
    public partial class ModifyPoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePolls();
                PopulateClubs();
            }
        }

        private void PopulatePolls()
        {
            string query = "SELECT Poll_Id, Question FROM Polls";
            var parameters = new Dictionary<string, object>();

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                ddlPoll.DataSource = dt;
                ddlPoll.DataTextField = "Question";
                ddlPoll.DataValueField = "Poll_Id";
                ddlPoll.DataBind();
                ddlPoll.Items.Insert(0, new ListItem("--Select Poll--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching polls: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        private void PopulateClubs()
        {
            string query = "SELECT ClubID, ClubName FROM Clubs";
            var parameters = new Dictionary<string, object>();

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

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

        protected void ddlPoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPoll.SelectedIndex > 0)
            {
                int pollId = int.Parse(ddlPoll.SelectedValue);
                LoadPollDetails(pollId);
                pnlPollDetails.Visible = true;
            }
            else
            {
                pnlPollDetails.Visible = false;
            }
        }

        private void LoadPollDetails(int pollId)
        {
            string query = "SELECT ClubID, Question, ExpirationDate FROM Polls WHERE Poll_Id = @Poll_Id";
            var parameters = new Dictionary<string, object> { { "@Poll_Id", pollId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    ddlClub.SelectedValue = dt.Rows[0]["ClubID"].ToString();
                    txtQuestion.Value = dt.Rows[0]["Question"].ToString();
                    txtExpirationDate.Value = DateTime.Parse(dt.Rows[0]["ExpirationDate"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading poll details: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void UpdatePoll_Click(object sender, EventArgs e)
        {
            int pollId = int.Parse(ddlPoll.SelectedValue);

            string clubId = ddlClub.SelectedValue;
            string question = txtQuestion.Value.Trim();
            DateTime expirationDate;
            bool isValidDate = DateTime.TryParse(txtExpirationDate.Value, out expirationDate);

            if (string.IsNullOrEmpty(clubId) || string.IsNullOrEmpty(question) || !isValidDate)
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.CssClass = "error";
                return;
            }

            string query = "UPDATE Polls SET ClubID = @ClubID, Question = @Question, ExpirationDate = @ExpirationDate " +
                           "WHERE Poll_Id = @Poll_Id";

            var parameters = new Dictionary<string, object>
            {
                { "@ClubID", clubId },
                { "@Question", question },
                { "@ExpirationDate", expirationDate },
                { "@Poll_Id", pollId }
            };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Poll updated successfully!";
                lblMessage.CssClass = "success";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating poll: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void DeletePoll_Click(object sender, EventArgs e)
        {
            int pollId = int.Parse(ddlPoll.SelectedValue);

            string query = "DELETE FROM Polls WHERE Poll_Id = @Poll_Id";
            var parameters = new Dictionary<string, object> { { "@Poll_Id", pollId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Poll deleted successfully!";
                lblMessage.CssClass = "success";

                // Reset the form
                ddlPoll.SelectedIndex = 0;
                pnlPollDetails.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting poll: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }
    }
}