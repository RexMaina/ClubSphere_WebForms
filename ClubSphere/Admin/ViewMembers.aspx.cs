using System;
using System.Data;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ViewMembers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMembers();
            }
        }

        private void LoadMembers()
        {
            string query = "SELECT MemberID, Name, Email, ChooseClub, JoinDate, ProfilePictureURL FROM Members";
            var commonFn = new CommonFn.Commonfnx();
            DataTable dt = commonFn.Fetch(query);

            if (dt.Rows.Count > 0)
            {
                gvMembers.DataSource = dt;
                gvMembers.DataBind();
            }
            else
            {
                lblMessage.Text = "No members found.";
                lblMessage.CssClass = "message error";
            }
        }
    }
}
