using System;
using System.Data;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class ViewPosts : System.Web.UI.Page
    {
        private readonly CommonFn.Commonfnx Fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadPosts();
            }
        }

        private string GetMemberID()
        {
            string email = Session["Email"]?.ToString();
            if (string.IsNullOrEmpty(email)) return null;

            string query = "SELECT MemberID FROM Members WHERE Email = @Email";
            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@Email", email }
            };

            DataTable dt = Fn.Fetch(query, param);
            return dt.Rows.Count > 0 ? dt.Rows[0]["MemberID"].ToString() : null;
        }

        private void LoadPosts()
        {
            string query = "SELECT PostID, Title, Contents, PostDate FROM Posts ORDER BY PostDate DESC";
            DataTable dt = Fn.Fetch(query);

            PanelNoPosts.Visible = dt.Rows.Count == 0;
            RepeaterPosts.Visible = dt.Rows.Count > 0;
            RepeaterPosts.DataSource = dt;
            RepeaterPosts.DataBind();
        }

        protected void BtnSubmitPost_Click(object sender, EventArgs e)
        {
            string memberId = GetMemberID();
            if (memberId == null || string.IsNullOrWhiteSpace(TxtPostTitle.Text) || string.IsNullOrWhiteSpace(TxtPostContent.Text))
                return;

            string title = TxtPostTitle.Text.Trim();
            string content = TxtPostContent.Text.Trim();

            string query = @"INSERT INTO Posts (ClubID, MemberID, Title, Contents, PostDate)
                             VALUES (NULL, @MemberID, @Title, @Contents, GETDATE())";

            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@MemberID", memberId },
                { "@Title", title },
                { "@Contents", content }
            };

            Fn.Query(query, param);

            // ✅ Send email
            string email = Session["Email"].ToString();
            string subject = "📝 New Post Created";
            string body = $"<h3>Post Created Successfully</h3><p><b>Title:</b> {title}</p><p>{content}</p>";
            string err;
            new EmailService().SendEmail(email, subject, body, out err);

            TxtPostTitle.Text = TxtPostContent.Text = "";
            LoadPosts();
        }

        protected void RepeaterPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string postId = e.CommandArgument.ToString();
            string memberId = GetMemberID();
            if (memberId == null) return;

            string reaction = e.CommandName == "Like" ? "Like" :
                              e.CommandName == "Dislike" ? "Dislike" : null;
            if (reaction == null) return;

            string postTitle = "";

            // Get post title for email
            string postQuery = "SELECT Title FROM Posts WHERE PostID = @PostID";
            var postParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@PostID", postId }
            };
            DataTable postDt = Fn.Fetch(postQuery, postParam);
            if (postDt.Rows.Count > 0)
            {
                postTitle = postDt.Rows[0]["Title"].ToString();
            }

            string checkQuery = "SELECT COUNT(*) FROM PostReactions WHERE PostID = @PostID AND MemberID = @MemberID";
            var checkParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@PostID", postId },
                { "@MemberID", memberId }
            };
            DataTable dt = Fn.Fetch(checkQuery, checkParam);

            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                string insert = @"INSERT INTO PostReactions (PostID, MemberID, Reaction)
                                  VALUES (@PostID, @MemberID, @Reaction)";
                var insertParam = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@PostID", postId },
                    { "@MemberID", memberId },
                    { "@Reaction", reaction }
                };
                Fn.Query(insert, insertParam);
            }

            // ✅ Send reaction email
            string email = Session["Email"].ToString();
            string subject = $"📣 You {reaction}d a Post!";
            string body = $"<h3>Your {reaction} was registered</h3><p><b>Post:</b> {postTitle}</p>";
            string error;
            new EmailService().SendEmail(email, subject, body, out error);

            LoadPosts();
        }
    }
}
