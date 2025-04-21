<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="AddClub.aspx.cs" Inherits="ClubSphere.Admin.AddClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .form-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: 600;
            color: #333;
        }

        .form-group input[type="text"],
        .form-group textarea,
        .form-group input[type="date"] {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
        }

        .form-group textarea {
            resize: vertical;
            height: 100px;
        }

        .form-group .btn-submit {
            background-color: #5558c9;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 1rem;
        }

        .form-group .btn-submit:hover {
            background-color: #3d3d79;
        }

        .message {
            margin-top: 15px;
            font-size: 0.9rem;
        }

        .message.success {
            color: #28a745;
        }

        .message.error {
            color: #dc3545;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Add New Club</h2>
        <div class="form-group">
            <label for="txtClubName">Club Name:</label>
            <asp:TextBox ID="txtClubName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtDescription">Description:</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtCreationDate">Creation Date:</label>
            <asp:TextBox ID="txtCreationDate" runat="server" TextMode="Date" CssClass="form-control" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAddClub" runat="server" Text="Add Club" CssClass="btn-submit" OnClick="btnAddClub_Click" />
        </div>
        <div class="message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>