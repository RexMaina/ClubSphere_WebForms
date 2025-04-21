using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null) // Check if the session is null
            {
                Response.Redirect("../Login.aspx"); // Redirect to login page
            }

            if (!IsPostBack)
            {
                /* Fetch and display dashboard data */
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();

                // Fetch total members
                string queryMembers = "SELECT COUNT(*) AS TotalMembers FROM Members";
                DataTable dtMembers = common.Fetch(queryMembers, null);

                if (dtMembers != null && dtMembers.Rows.Count > 0)
                {
                    lblTotalMembers.Text = dtMembers.Rows[0]["TotalMembers"].ToString();
                }
                else
                {
                    lblTotalMembers.Text = "0";
                }

                // Fetch upcoming events
                string queryEvents = "SELECT COUNT(*) AS UpcomingEvents FROM Events WHERE EventDate >= GETDATE()";
                DataTable dtEvents = common.Fetch(queryEvents, null);

                if (dtEvents != null && dtEvents.Rows.Count > 0)
                {
                    lblUpcomingEvents.Text = dtEvents.Rows[0]["UpcomingEvents"].ToString();
                }
                else
                {
                    lblUpcomingEvents.Text = "0";
                }

                // Fetch active polls
                string queryPolls = "SELECT COUNT(*) AS ActivePolls FROM Polls WHERE ExpirationDate >= GETDATE()";
                DataTable dtPolls = common.Fetch(queryPolls, null);

                if (dtPolls != null && dtPolls.Rows.Count > 0)
                {
                    lblActivePolls.Text = dtPolls.Rows[0]["ActivePolls"].ToString();
                }
                else
                {
                    lblActivePolls.Text = "0";
                }

                // Fetch recent posts (last 7 days)
                string queryPosts = "SELECT COUNT(*) AS RecentPosts FROM Posts WHERE PostDate >= DATEADD(DAY, -7, GETDATE())";
                DataTable dtPosts = common.Fetch(queryPosts, null);

                if (dtPosts != null && dtPosts.Rows.Count > 0)
                {
                    lblRecentPosts.Text = dtPosts.Rows[0]["RecentPosts"].ToString();
                }
                else
                {
                    lblRecentPosts.Text = "0";
                }
            }
            catch (Exception ex)
            {
                // Handle errors properly
                Response.Write("<script>alert('Error loading dashboard data: " + ex.Message + "');</script>");
            }
        }

        // Method to get members growth labels (e.g., months)
        public string GetMembersGrowthLabels()
        {
            try
            {
                List<string> labels = new List<string>();
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();

                string query = "SELECT DISTINCT FORMAT(JoinDate, 'MMM yyyy') AS MonthYear FROM Members ORDER BY MIN(JoinDate)";
                DataTable dt = common.Fetch(query, null);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        labels.Add($"'{row["MonthYear"].ToString()}'");
                    }
                }

                return $"[{string.Join(", ", labels)}]";
            }
            catch (Exception ex)
            {
                return "[]"; // Return empty array on error
            }
        }

        // Method to get members growth data
        public string GetMembersGrowthData()
        {
            try
            {
                List<int> data = new List<int>();
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();

                string query = "SELECT FORMAT(JoinDate, 'MMM yyyy') AS MonthYear, COUNT(*) AS MemberCount FROM Members GROUP BY FORMAT(JoinDate, 'MMM yyyy') ORDER BY MIN(JoinDate)";
                DataTable dt = common.Fetch(query, null);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data.Add(Convert.ToInt32(row["MemberCount"]));
                    }
                }

                return $"[{string.Join(", ", data)}]";
            }
            catch (Exception ex)
            {
                return "[]"; // Return empty array on error
            }
        }

        // Method to get events overview labels (e.g., event types)
        public string GetEventsOverviewLabels()
        {
            try
            {
                List<string> labels = new List<string>();
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();

                string query = "SELECT DISTINCT ClubName FROM Events";
                DataTable dt = common.Fetch(query, null);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        labels.Add($"'{row["ClubName"].ToString()}'");
                    }
                }

                return $"[{string.Join(", ", labels)}]";
            }
            catch (Exception ex)
            {
                return "[]"; // Return empty array on error
            }
        }

        // Method to get events overview data
        public string GetEventsOverviewData()
        {
            try
            {
                List<int> data = new List<int>();
                CommonFn.Commonfnx common = new CommonFn.Commonfnx();

                string query = "SELECT ClubName, COUNT(*) AS EventCount FROM Events GROUP BY ClubName";
                DataTable dt = common.Fetch(query, null);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data.Add(Convert.ToInt32(row["EventCount"]));
                    }
                }

                return $"[{string.Join(", ", data)}]";
            }
            catch (Exception ex)
            {
                return "[]"; // Return empty array on error
            }
        }
    }
}
