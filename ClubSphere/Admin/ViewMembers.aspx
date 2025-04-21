<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ViewMembers.aspx.cs" Inherits="ClubSphere.Admin.ViewMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-container {
            max-width: 1000px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .table-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
        }

        .members-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .members-table th, .members-table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .members-table th {
            background-color: #5558c9;
            color: white;
            font-weight: bold;
        }

        .members-table tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .members-table img {
            width: 50px;
            height: 50px;
            border-radius: 50%;
        }

        .message {
            margin-top: 15px;
            font-size: 0.9rem;
        }

        .message.error {
            color: #dc3545;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-container">
        <h2>All Registered Members</h2>

        <asp:GridView ID="gvMembers" runat="server" CssClass="members-table" AutoGenerateColumns="False" GridLines="None"
            EmptyDataText="No members found." CellPadding="5">
            <Columns>
                <asp:BoundField DataField="MemberID" HeaderText="Member ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="ChooseClub" HeaderText="Club" />
                <asp:BoundField DataField="JoinDate" HeaderText="Join Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="Profile Picture">
                    <ItemTemplate>
                        <asp:Image ID="imgProfile" runat="server" ImageUrl='<%# Eval("ProfilePictureURL") %>' Width="50px" Height="50px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
