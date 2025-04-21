<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="ClubSphere.Admin.AddMember" %>

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
        .form-group input[type="password"],
        .form-group textarea,
        .form-group select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
        }

        .form-group input[type="file"] {
            padding: 8px;
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
        <h2>Add New Member</h2>
        <div class="form-group">
            <label for="txtName">Name:</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlChooseClub">Choose Club:</label>
            <asp:DropDownList ID="ddlChooseClub" runat="server" CssClass="form-control" required="true">
                <asp:ListItem Value="">Select Club</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ddlRole">Role:</label>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" required="true">
                <asp:ListItem Value="">Select Role</asp:ListItem>
                <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                <asp:ListItem Value="Member">Member</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="fileProfilePicture">Profile Picture (jpg, png, jpeg):</label>
            <asp:FileUpload ID="fileProfilePicture" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" CssClass="btn-submit" OnClick="btnAddMember_Click" />
        </div>
        <div class="message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>