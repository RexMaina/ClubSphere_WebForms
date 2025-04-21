<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ViewPosts.aspx.cs" Inherits="ClubSphere.Admin.ViewPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .posts-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .posts-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
            text-align: center;
        }

        .posts-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .posts-table th, .posts-table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .posts-table th {
            background-color: #5558c9;
            color: white;
        }

        .posts-table tr:hover {
            background-color: #f1f1f1;
        }

        .no-posts {
            text-align: center;
            color: #dc3545;
            font-size: 1rem;
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="posts-container">
        <h2>View All Posts</h2>
        <asp:GridView ID="gvPosts" runat="server" CssClass="posts-table" AutoGenerateColumns="false" EmptyDataText="No posts found.">
            <Columns>
                <asp:BoundField DataField="PostID" HeaderText="Post ID" />
                <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
                <asp:BoundField DataField="MemberID" HeaderText="Member ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Contents" HeaderText="Contents" />
                <asp:BoundField DataField="PostDate" HeaderText="Post Date" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>