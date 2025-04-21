<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ViewClubs.aspx.cs" Inherits="ClubSphere.Admin.ViewClubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-container {
            max-width: 800px;
            margin: 20px auto;
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .table-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
        }
        .club-table {
            width: 100%;
            border-collapse: collapse;
        }
        .club-table th, .club-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .club-table th {
            background-color: #5558c9;
            color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-container">
        <h2>View Clubs</h2>
        <asp:GridView ID="gvClubs" runat="server" CssClass="club-table" AutoGenerateColumns="true" AllowPaging="true" PageSize="10"></asp:GridView>
    </div>
</asp:Content>
