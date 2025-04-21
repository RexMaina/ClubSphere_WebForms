using System;
using System.Web.UI;
using ClubSphere.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ClubSphere.Admin
{
    public partial class ModifyPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePosts();
                PopulateClubs();
            }
        }

        private void PopulatePosts()
        {
            string query = "SELECT PostID, Title FROM Posts";
            var parameters = new Dictionary<string, object>();

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                ddlPost.DataSource = dt;
                ddlPost.DataTextField = "Title";
                ddlPost.DataValueField = "PostID";
                ddlPost.DataBind();
                ddlPost.Items.Insert(0, new ListItem("--Select Post--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching posts: " + ex.Message;
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

        protected void ddlPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPost.SelectedIndex > 0)
            {
                int postId = int.Parse(ddlPost.SelectedValue);
                LoadPostDetails(postId);
                pnlPostDetails.Visible = true;
            }
            else
            {
                pnlPostDetails.Visible = false;
            }
        }

        private void LoadPostDetails(int postId)
        {
            string query = "SELECT ClubID, MemberID, Title, Contents FROM Posts WHERE PostID = @PostID";
            var parameters = new Dictionary<string, object> { { "@PostID", postId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                var dt = common.Fetch(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    ddlClub.SelectedValue = dt.Rows[0]["ClubID"].ToString();
                    txtMemberID.Value = dt.Rows[0]["MemberID"].ToString();
                    txtTitle.Value = dt.Rows[0]["Title"].ToString();
                    txtContents.Value = dt.Rows[0]["Contents"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading post details: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void UpdatePost_Click(object sender, EventArgs e)
        {
            int postId = int.Parse(ddlPost.SelectedValue);

            string clubId = ddlClub.SelectedValue;
            int memberId;
            bool isValidMemberId = int.TryParse(txtMemberID.Value, out memberId);
            string title = txtTitle.Value.Trim();
            string contents = txtContents.Value.Trim();

            if (string.IsNullOrEmpty(clubId) || !isValidMemberId || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(contents))
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.CssClass = "error";
                return;
            }

            string query = "UPDATE Posts SET ClubID = @ClubID, MemberID = @MemberID, Title = @Title, Contents = @Contents " +
                           "WHERE PostID = @PostID";

            var parameters = new Dictionary<string, object>
            {
                { "@ClubID", clubId },
                { "@MemberID", memberId },
                { "@Title", title },
                { "@Contents", contents },
                { "@PostID", postId }
            };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Post updated successfully!";
                lblMessage.CssClass = "success";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating post: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }

        protected void DeletePost_Click(object sender, EventArgs e)
        {
            int postId = int.Parse(ddlPost.SelectedValue);

            string query = "DELETE FROM Posts WHERE PostID = @PostID";
            var parameters = new Dictionary<string, object> { { "@PostID", postId } };

            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();
                common.Query(query, parameters);

                lblMessage.Text = "Post deleted successfully!";
                lblMessage.CssClass = "success";

                // Reset the form
                ddlPost.SelectedIndex = 0;
                pnlPostDetails.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting post: " + ex.Message;
                lblMessage.CssClass = "error";
            }
        }
    }
}