<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ModifyPost.aspx.cs" Inherits="ClubSphere.Admin.ModifyPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .form-container h2 {
            margin-bottom: 20px;
            font-size: 1.5rem;
            color: #5558c9;
        }

        .form-container label {
            font-size: 1rem;
            margin-bottom: 5px;
            display: block;
        }

        .form-container input, .form-container textarea, .form-container select {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
            border: 1px solid #ddd;
        }

        .form-container button {
            background-color: #5558c9;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .form-container button:hover {
            background-color: #4e51b7;
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
        <h2>Modify Post</h2>

        <div>
            <label for="ddlPost">Select Post:</label>
            <asp:DropDownList ID="ddlPost" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPost_SelectedIndexChanged">
                <asp:ListItem Text="--Select Post--" Value="" />
            </asp:DropDownList>
        </div>

        <asp:Panel ID="pnlPostDetails" runat="server" Visible="false">
            <div>
                <label for="ddlClub">Club:</label>
                <asp:DropDownList ID="ddlClub" runat="server">
                    <asp:ListItem Text="--Select Club--" Value="" />
                </asp:DropDownList>
            </div>

            <div>
                <label for="txtMemberID">Member ID:</label>
                <input type="number" id="txtMemberID" runat="server" />
            </div>

            <div>
                <label for="txtTitle">Title:</label>
                <input type="text" id="txtTitle" runat="server" />
            </div>

            <div>
                <label for="txtContents">Contents:</label>
                <textarea id="txtContents" runat="server"></textarea>
            </div>

            <div>
                <button type="submit" runat="server" onserverclick="UpdatePost_Click">Update Post</button>
                <asp:Button ID="btnDelete" runat="server" Text="Delete Post" OnClick="DeletePost_Click"
                    OnClientClick="return confirm('Are you sure you want to delete this post?');" />
            </div>
        </asp:Panel>

        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>