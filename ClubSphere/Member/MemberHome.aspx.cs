using System;
using System.Data;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere.Member
{
    public partial class MemberHome : Page
    {
        CommonFn.Commonfnx fn = new CommonFn.Commonfnx();
        private int memberId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberID"] != null)
            {
                memberId = Convert.ToInt32(Session["MemberID"]);
            }
            else
            {
                memberId = 1; // Default for testing, REMOVE IN PRODUCTION
            }

            if (!IsPostBack)
            {
                LoadMemberData();
                InitializeCharts();
            }
        }

        private void LoadMemberData()
        {
            LoadJoinedClub();
            LoadEventsParticipated();
            LoadTotalPolls();
            LoadTotalPosts();
            LoadRecentEvent();
            LoadRecentPoll();
        }

        private void LoadJoinedClub()
        {
            string clubQuery = $"SELECT ChooseClub FROM Members WHERE MemberID = {memberId}";
            DataTable clubDt = fn.Fetch(clubQuery);
            if (clubDt.Rows.Count > 0 && clubDt.Rows[0]["ChooseClub"] != DBNull.Value)
            {
                lblJoinedClub.Text = clubDt.Rows[0]["ChooseClub"].ToString();
                lblRecentClub.Text = clubDt.Rows[0]["ChooseClub"].ToString();
            }
        }

        private void LoadEventsParticipated()
        {
            string eventQuery = $"SELECT COUNT(*) FROM EventParticipants WHERE MemberID = {memberId}";
            DataTable eventDt = fn.Fetch(eventQuery);
            if (eventDt.Rows.Count > 0)
            {
                lblEventsParticipated.Text = eventDt.Rows[0][0].ToString();
            }
        }

        private void LoadTotalPolls()
        {
            string pollQuery = "SELECT COUNT(*) FROM Polls";
            DataTable pollDt = fn.Fetch(pollQuery);
            if (pollDt.Rows.Count > 0)
            {
                lblTotalPolls.Text = pollDt.Rows[0][0].ToString();
            }
        }

        private void LoadTotalPosts()
        {
            string postQuery = "SELECT COUNT(*) FROM Posts";
            DataTable postDt = fn.Fetch(postQuery);
            if (postDt.Rows.Count > 0)
            {
                lblTotalPosts.Text = postDt.Rows[0][0].ToString();
            }
        }

        private void LoadRecentEvent()
        {
            string recentEventQuery = $"SELECT TOP 1 e.EventName FROM EventParticipants ep JOIN Events e ON ep.EventID = e.EventID WHERE ep.MemberID = {memberId} ORDER BY ep.JoinDate DESC";
            DataTable recentEventDt = fn.Fetch(recentEventQuery);
            if (recentEventDt.Rows.Count > 0)
            {
                lblRecentEvent.Text = recentEventDt.Rows[0][0].ToString();
            }
        }

        private void LoadRecentPoll()
        {
            string recentPollQuery = $"SELECT TOP 1 p.Question FROM PollResponses pr JOIN Polls p ON pr.Poll_ID = p.Poll_Id WHERE pr.MemberID = {memberId} ORDER BY pr.ResponseID DESC";
            DataTable recentPollDt = fn.Fetch(recentPollQuery);
            if (recentPollDt.Rows.Count > 0)
            {
                lblRecentPoll.Text = recentPollDt.Rows[0][0].ToString();
            }
        }

        private void InitializeCharts()
        {
            // Example for Club Activity Chart (Replace with actual data)
            int[] clubActivityData = { 10, 15, 8, 20, 12, 18, 25 }; // Example data
            string clubActivityLabels = "['January', 'February', 'March', 'April', 'May', 'June', 'July']";

            // Example for Event Participation Chart (Replace with actual data)
            int attendedEvents = 75; // Example data
            int notAttendedEvents = 25; // Example data

            string clubChartScript = $@"
                var ctx1 = document.getElementById('clubActivityChart').getContext('2d');
                var clubActivityChart = new Chart(ctx1, {{
                    type: 'line',
                    data: {{
                        labels: {clubActivityLabels},
                        datasets: [{{
                            label: 'Club Activity',
                            data: [{string.Join(",", clubActivityData)}],
                            backgroundColor: 'rgba(54, 162, 235, 0.2)',
                            borderColor: 'rgba(54, 162, 235, 1)',
                            borderWidth: 2
                        }}]
                    }},
                    options: {{
                        scales: {{
                            y: {{
                                beginAtZero: true
                            }}
                        }}
                    }}
                }});
            ";

            string eventChartScript = $@"
                var ctx2 = document.getElementById('eventParticipationChart').getContext('2d');
                var eventParticipationChart = new Chart(ctx2, {{
                    type: 'pie',
                    data: {{
                        labels: ['Attended', 'Not Attended'],
                        datasets: [{{
                            label: 'Event Participation',
                            data: [{attendedEvents}, {notAttendedEvents}],
                            backgroundColor: [
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(255, 99, 132, 0.2)'
                            ],
                            borderColor: [
                                'rgba(75, 192, 192, 1)',
                                'rgba(255, 99, 132, 1)'
                            ],
                            borderWidth: 1
                        }}]
                    }}
                }});
            ";

            ClientScript.RegisterStartupScript(this.GetType(), "clubChartScript", clubChartScript, true);
            ClientScript.RegisterStartupScript(this.GetType(), "eventChartScript", eventChartScript, true);
        }
    }
}