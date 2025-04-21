using System;
using System.Data;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ViewClubs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClubs();
            }
        }

        private void LoadClubs()
        {
            string query = "SELECT ClubID, ClubName, Description, CreationDate FROM Clubs";
            var commonFn = new CommonFn.Commonfnx();
            DataTable dt = commonFn.Fetch(query);

            gvClubs.DataSource = dt;
            gvClubs.DataBind();
        }
    }
}
