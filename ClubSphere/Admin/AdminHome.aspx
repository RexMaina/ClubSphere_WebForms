<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="ClubSphere.Admin.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styles */
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .dashboard-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
        }

        .dashboard-header {
            text-align: center;
            margin-bottom: 40px;
        }

        .dashboard-header h1 {
            font-size: 2.5rem;
            color: #5558c9;
            margin-bottom: 10px;
        }

        .dashboard-header p {
            font-size: 1rem;
            color: #666;
        }

        /* Cards Layout */
        .cards-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-bottom: 40px;
        }

        .card {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        .card-icon {
            font-size: 2.5rem;
            color: #5558c9;
            margin-bottom: 15px;
        }

        .card h3 {
            font-size: 1.5rem;
            color: #333;
            margin-bottom: 10px;
        }

        .card p {
            font-size: 1rem;
            color: #666;
        }

        .card a {
            display: inline-block;
            margin-top: 15px;
            padding: 10px 20px;
            background-color: #5558c9;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s ease;
        }

        .card a:hover {
            background-color: #4e51b7;
        }

        /* Quick Links Section */
        .quick-links {
            margin-top: 40px;
        }

        .quick-links h2 {
            font-size: 2rem;
            color: #5558c9;
            margin-bottom: 20px;
            text-align: center;
        }

        .quick-links-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
        }

        .quick-link {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .quick-link:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        .quick-link-icon {
            font-size: 2rem;
            color: #5558c9;
            margin-bottom: 15px;
        }

        .quick-link h4 {
            font-size: 1.25rem;
            color: #333;
            margin-bottom: 10px;
        }

        .quick-link a {
            display: inline-block;
            margin-top: 15px;
            padding: 10px 20px;
            background-color: #5558c9;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s ease;
        }

        .quick-link a:hover {
            background-color: #4e51b7;
        }

        /* Charts Section */
        .charts-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
            gap: 20px;
            margin-top: 40px;
        }

        .chart-card {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .chart-card h3 {
            font-size: 1.5rem;
            color: #333;
            margin-bottom: 20px;
            text-align: center;
        }

        .chart-container {
            width: 100%;
            height: 300px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-container">
        <!-- Dashboard Header -->
        <div class="dashboard-header">
            <h1>Welcome to ClubSphere Admin Dashboard</h1>
            <p>Manage your clubs, events, polls, posts, and members efficiently.</p>
        </div>

        <!-- Cards Section -->
        <div class="cards-container">
            <div class="card">
                <div class="card-icon">
                    <i class="fas fa-users"></i>
                </div>
                <h3>Total Members</h3>
                <p><asp:Label ID="lblTotalMembers" runat="server" Text="0"></asp:Label></p>
                <a href="../Admin/ViewMembers.aspx">View Members</a>
            </div>

            <div class="card">
                <div class="card-icon">
                    <i class="fas fa-calendar-alt"></i>
                </div>
                <h3>Upcoming Events</h3>
                <p><asp:Label ID="lblUpcomingEvents" runat="server" Text="0"></asp:Label></p>
                <a href="../Admin/ViewEvents.aspx">View Events</a>
            </div>

            <div class="card">
                <div class="card-icon">
                    <i class="fas fa-poll"></i>
                </div>
                <h3>Active Polls</h3>
                <p><asp:Label ID="lblActivePolls" runat="server" Text="0"></asp:Label></p>
                <a href="../Admin/ViewPoll.aspx">View Polls</a>
            </div>

            <div class="card">
                <div class="card-icon">
                    <i class="fas fa-comments"></i>
                </div>
                <h3>Recent Posts</h3>
                <p><asp:Label ID="lblRecentPosts" runat="server" Text="0"></asp:Label></p>
                <a href="../Admin/ViewPosts.aspx">View Posts</a>
            </div>
        </div>

        <!-- Charts Section -->
        <div class="charts-container">
            <div class="chart-card">
                <h3>Members Growth</h3>
                <div class="chart-container">
                    <canvas id="membersChart"></canvas>
                </div>
            </div>

            <div class="chart-card">
                <h3>Events Overview</h3>
                <div class="chart-container">
                    <canvas id="eventsChart"></canvas>
                </div>
            </div>
        </div>

        <!-- Quick Links Section -->
        <div class="quick-links">
            <h2>Quick Links</h2>
            <div class="quick-links-container">
                <div class="quick-link">
                    <div class="quick-link-icon">
                        <i class="fas fa-plus"></i>
                    </div>
                    <h4>Add Club</h4>
                    <a href="../Admin/AddClub.aspx">Go to Page</a>
                </div>

                <div class="quick-link">
                    <div class="quick-link-icon">
                        <i class="fas fa-edit"></i>
                    </div>
                    <h4>Modify Club</h4>
                    <a href="../Admin/ModifyClub.aspx">Go to Page</a>
                </div>

                <div class="quick-link">
                    <div class="quick-link-icon">
                        <i class="fas fa-eye"></i>
                    </div>
                    <h4>View Clubs</h4>
                    <a href="../Admin/ViewClubs.aspx">Go to Page</a>
                </div>

                <div class="quick-link">
                    <div class="quick-link-icon">
                        <i class="fas fa-sign-out-alt"></i>
                    </div>
                    <h4>Logout</h4>
                    <a href="../Logout.aspx">Logout</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Chart.js Library -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <!-- Script to Render Charts -->
    <script>
        // Members Growth Chart
        const membersCtx = document.getElementById('membersChart').getContext('2d');
        const membersChart = new Chart(membersCtx, {
            type: 'line',
            data: {
                labels: <%= GetMembersGrowthLabels() %>,
                datasets: [{
                    label: 'Total Members',
                    data: <%= GetMembersGrowthData() %>,
                    backgroundColor: 'rgba(85, 88, 201, 0.2)',
                    borderColor: 'rgba(85, 88, 201, 1)',
                    borderWidth: 2,
                    fill: true
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        // Events Overview Chart
        const eventsCtx = document.getElementById('eventsChart').getContext('2d');
        const eventsChart = new Chart(eventsCtx, {
            type: 'bar',
            data: {
                labels: <%= GetEventsOverviewLabels() %>,
                datasets: [{
                    label: 'Number of Events',
                    data: <%= GetEventsOverviewData() %>,
                    backgroundColor: 'rgba(85, 88, 201, 0.5)',
                    borderColor: 'rgba(85, 88, 201, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    </script>
</asp:Content>