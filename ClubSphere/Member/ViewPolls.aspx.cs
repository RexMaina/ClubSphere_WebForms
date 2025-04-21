using System;
using System.Data;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class ViewPolls : System.Web.UI.Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                EnsureMemberIdInSession();
                LoadPolls();
            }
        }

        private void EnsureMemberIdInSession()
        {
            if (Session["MemberID"] == null)
            {
                string email = Session["Email"].ToString();
                string query = "SELECT MemberID FROM Members WHERE Email = @Email";
                var param = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@Email", email }
                };
                DataTable dt = fn.Fetch(query, param);
                if (dt.Rows.Count > 0)
                {
                    Session["MemberID"] = dt.Rows[0]["MemberID"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void LoadPolls()
        {
            string query = @"
                SELECT P.Poll_Id, P.Question, P.CreationDate, P.ExpirationDate,
                       ISNULL(R.Response, '') AS Response
                FROM Polls P
                LEFT JOIN PollResponses R ON P.Poll_Id = R.Poll_ID 
                    AND R.MemberID = @MemberID";

            var param = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@MemberID", Session["MemberID"].ToString() }
            };

            rptPolls.DataSource = fn.Fetch(query, param);
            rptPolls.DataBind();
        }

        protected void rptPolls_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            EnsureMemberIdInSession();

            string pollId = e.CommandArgument.ToString();
            string memberId = Session["MemberID"].ToString();
            string responseText = "";

            if (e.CommandName == "Like") responseText = "Like";
            else if (e.CommandName == "Dislike") responseText = "Dislike";
            else if (e.CommandName == "Respond")
            {
                TextBox txt = (TextBox)e.Item.FindControl("txtResponse");
                if (txt == null || string.IsNullOrWhiteSpace(txt.Text)) return;
                responseText = txt.Text.Trim();
            }

            string checkQuery = @"SELECT COUNT(*) FROM PollResponses 
                                  WHERE Poll_ID = @PollID AND MemberID = @MemberID";

            var checkParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@PollID", pollId },
                { "@MemberID", memberId }
            };

            DataTable dt = fn.Fetch(checkQuery, checkParam);

            string sql;
            var sqlParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@PollID", pollId },
                { "@MemberID", memberId },
                { "@Response", responseText }
            };

            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                sql = @"UPDATE PollResponses SET Response = @Response 
                        WHERE Poll_ID = @PollID AND MemberID = @MemberID";
            }
            else
            {
                sql = @"INSERT INTO PollResponses (Poll_ID, MemberID, Response)
                        VALUES (@PollID, @MemberID, @Response)";
            }

            fn.Query(sql, sqlParam);

            // ✅ Fetch poll question for email
            string pollQuery = "SELECT Question FROM Polls WHERE Poll_Id = @PollID";
            var pollParam = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@PollID", pollId }
            };
            DataTable pollDt = fn.Fetch(pollQuery, pollParam);

            if (pollDt.Rows.Count > 0)
            {
                string question = pollDt.Rows[0]["Question"].ToString();
                string email = Session["Email"].ToString();
                string subject = "🗳️ Poll Response Recorded";
                string body = $"<h3>Thanks for voting!</h3><p><b>Poll:</b> {question}</p><p><b>Your Response:</b> {responseText}</p>";
                string error;

                new EmailService().SendEmail(email, subject, body, out error);
            }

            LoadPolls(); 
        }
    }
}
