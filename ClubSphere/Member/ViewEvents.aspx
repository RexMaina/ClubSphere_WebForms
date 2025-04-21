<%@ Page Title="View Events" Language="C#" MasterPageFile="~/Member/Membermst.master"
    AutoEventWireup="true" CodeBehind="ViewEvents.aspx.cs" Inherits="ClubSphere.Member.ViewEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-4">📅 Upcoming Events</h3>

        <asp:Repeater ID="rptEvents" runat="server" OnItemCommand="rptEvents_ItemCommand">
            <ItemTemplate>
                <div class="card shadow-sm mb-3">
                    <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                        <div>
                            <i class="fas fa-calendar-alt me-1"></i>
                            <%# Eval("EventName") %> — <span class="fw-light"><%# Eval("ClubName") %></span>
                        </div>
                        <span class="badge bg-secondary"><%# Eval("EventDate", "{0:dd MMM yyyy}") %></span>
                    </div>
                    <div class="card-body">
                        <p><%# Eval("Description") %></p>
                        <p><strong>📍 Location:</strong> <%# Eval("Location") %></p>
                        <p><strong>👥 Registered Participants:</strong> <%# Eval("ParticipantCount") %></p>

                        <asp:Button ID="btnAction" runat="server"
                            Text='<%# Eval("ParticipationStatus").ToString() == "Joined" ? "Cancel Event Plan" : "Join Event" %>'
                            CssClass='<%# Eval("ParticipationStatus").ToString() == "Joined" ? "btn btn-outline-danger" : "btn btn-success" %>'
                            CommandName='<%# Eval("ParticipationStatus").ToString() == "Joined" ? "Cancel" : "Join" %>'
                            CommandArgument='<%# Eval("EventID") %>'
                            Visible='<%# Convert.ToDateTime(Eval("EventDate")) >= DateTime.Now %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
