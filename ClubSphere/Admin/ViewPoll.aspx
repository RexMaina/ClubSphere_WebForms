<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ViewPoll.aspx.cs" Inherits="ClubSphere.Admin.ViewPoll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .polls-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .polls-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
            text-align: center;
        }

        .polls-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .polls-table th, .polls-table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .polls-table th {
            background-color: #5558c9;
            color: white;
        }

        .polls-table tr:hover {
            background-color: #f1f1f1;
        }

        .no-polls {
            text-align: center;
            color: #dc3545;
            font-size: 1rem;
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="polls-container">
        <h2>View All Polls and Responses</h2>
        <asp:GridView ID="gvPolls" runat="server" CssClass="polls-table" AutoGenerateColumns="false" EmptyDataText="No polls found.">
            <Columns>
                <asp:BoundField DataField="Poll_Id" HeaderText="Poll ID" />
                <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
                <asp:BoundField DataField="Question" HeaderText="Question" />
                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="ExpirationDate" HeaderText="Expiration Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Response" HeaderText="Response" />
                <asp:BoundField DataField="MemberID" HeaderText="Member ID" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>