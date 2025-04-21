using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace ClubSphere.Admin
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["clubCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e) { }

        protected void GenerateWeeklyReport(object sender, EventArgs e)
        {
            GenerateReportByTimeFrame("DAY", 7, "Weekly Report");
        }

        protected void GenerateMonthlyReport(object sender, EventArgs e)
        {
            GenerateReportByTimeFrame("MONTH", 1, "Monthly Report");
        }

        protected void GenerateAnnualReport(object sender, EventArgs e)
        {
            GenerateReportByTimeFrame("YEAR", 1, "Annual Report");
        }

        private void GenerateReportByTimeFrame(string intervalType, int intervalValue, string reportTitle)
        {
            try
            {
                StringBuilder report = new StringBuilder();
                report.AppendLine($"<h3>{reportTitle}</h3>");
                report.AppendLine(GetClubActivity(intervalType, intervalValue));
                report.AppendLine(GetMemberActivity(intervalType, intervalValue));
                report.AppendLine(GetEventActivity(intervalType, intervalValue));
                litReportOutput.Text = report.ToString();
            }
            catch (Exception ex)
            {
                litReportOutput.Text = $"<p style='color: red;'>Error generating report: {ex.Message}</p>";
            }
        }

        private string GetClubActivity(string intervalType, int intervalValue)
        {
            string query = $"SELECT COUNT(*) AS ClubCount FROM Clubs WHERE CreationDate >= DATEADD({intervalType}, -{intervalValue}, GETDATE())";
            return ExecuteQuery(query, "New Clubs Created");
        }

        private string GetMemberActivity(string intervalType, int intervalValue)
        {
            string query = $"SELECT COUNT(*) AS MemberCount FROM Members WHERE JoinDate >= DATEADD({intervalType}, -{intervalValue}, GETDATE())";
            return ExecuteQuery(query, "New Members Joined");
        }

        private string GetEventActivity(string intervalType, int intervalValue)
        {
            string query = $"SELECT COUNT(*) AS EventCount FROM Events WHERE CreatedAt >= DATEADD({intervalType}, -{intervalValue}, GETDATE())";
            return ExecuteQuery(query, "New Events Created");
        }

        private string ExecuteQuery(string query, string label)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return $"<p>{label}: {result}</p>";
                }
            }
        }
    }
}