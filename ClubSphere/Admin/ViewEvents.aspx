<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ViewEvents.aspx.cs" Inherits="ClubSphere.Admin.ViewEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .events-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .events-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
            text-align: center;
        }

        .events-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .events-table th, .events-table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .events-table th {
            background-color: #5558c9;
            color: white;
        }

        .events-table tr:hover {
            background-color: #f1f1f1;
        }

        .participants-container {
            max-width: 1000px;
            margin: 40px auto 0;
            background-color: #f9f9f9;
            padding: 20px;
            border-left: 5px solid #5558c9;
            border-radius: 8px;
        }

        .participants-container h4 {
            color: #5558c9;
            margin-bottom: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="events-container">
        <h2>📋 View All Events & Participation</h2>

        <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="false" CssClass="events-table"
            OnRowCommand="gvEvents_RowCommand" EmptyDataText="No events found." DataKeyNames="EventID">
            <Columns>
                <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="EventDate" HeaderText="Event Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                <asp:BoundField DataField="MaxParticipants" HeaderText="Max Participants" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="JoinedCount" HeaderText="Total Joined" />
                <asp:ButtonField ButtonType="Button" CommandName="ViewMembers" HeaderText="Action" Text="View Members"
                    ControlStyle-CssClass="btn btn-sm btn-info" />
            </Columns>
        </asp:GridView>
    </div>

    <asp:Panel ID="pnlParticipants" runat="server" Visible="false" CssClass="participants-container">
        <h4>👥 Members Participated</h4>
        <asp:GridView ID="gvParticipants" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="true" />
    </asp:Panel>
</asp:Content>
