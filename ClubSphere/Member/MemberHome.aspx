<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="ClubSphere.Member.MemberHome" MasterPageFile="~/Member/Membermst.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-4 mb-4">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-primary text-white" style="color: white !important;">
                        <i class="fas fa-chess" style="color: white !important;"></i> Your Clubs
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Club Participation</h5>
                        <p class="card-text">View the clubs you are a member of.</p>
                        <p>Joined Club: <asp:Label ID="lblJoinedClub" runat="server" Text=""></asp:Label></p>
                        <a href="ViewClubs.aspx" class="btn btn-primary btn-block">View Clubs</a>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-success text-white" style="color: white !important;">
                        <i class="fas fa-calendar-alt" style="color: white !important;"></i> Upcoming Events
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Stay Updated</h5>
                        <p class="card-text">Check out the upcoming events for your clubs.</p>
                        <p>Events Participated: <asp:Label ID="lblEventsParticipated" runat="server" Text=""></asp:Label></p>
                        <a href="ViewEvents.aspx" class="btn btn-success btn-block">View Events</a>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-warning text-white" style="color: white !important;">
                        <i class="fas fa-poll" style="color: white !important;"></i> Polls & Feedback
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Participate in Polls</h5>
                        <p class="card-text">Take part in club polls and provide your feedback.</p>
                        <p>Total Polls: <asp:Label ID="lblTotalPolls" runat="server" Text=""></asp:Label></p>
                        <a href="ViewPolls.aspx" class="btn btn-warning btn-block">View Polls</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6 mb-4">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-info text-white" style="color: white !important;">
                        <i class="fas fa-chart-line" style="color: white !important;"></i> Club Activity Overview
                    </div>
                    <div class="card-body">
                        <canvas id="clubActivityChart" width="400" height="200"></canvas>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-secondary text-white" style="color: white !important;">
                        <i class="fas fa-chart-pie" style="color: white !important;"></i> Event Participation
                    </div>
                    <div class="card-body">
                        <canvas id="eventParticipationChart" width="400" height="200"></canvas>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card shadow-lg">
                    <div class="card-header bg-gradient-dark text-white" style="color: white !important;">
                        <i class="fas fa-clock" style="color: white !important;"></i> Recent Activity
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Recent Club Activity</h5>
                        <p class="card-text">Stay up-to-date with the latest activities and announcements in your clubs.</p>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">Posts: <asp:Label ID="lblTotalPosts" runat="server" Text=""></asp:Label></li>
                            <li class="list-group-item">Joined the <asp:Label ID="lblRecentClub" runat="server" Text=""></asp:Label> club.</li>
                            <li class="list-group-item">RSVP'd to the <asp:Label ID="lblRecentEvent" runat="server" Text=""></asp:Label> event.</li>
                            <li class="list-group-item">Voted in the <asp:Label ID="lblRecentPoll" runat="server" Text=""></asp:Label> poll.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="MemberHomeChart.js"></script>
</asp:Content>