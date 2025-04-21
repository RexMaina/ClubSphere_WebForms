<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="AddPoll.aspx.cs" Inherits="ClubSphere.Admin.AddPoll" %>

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
        <h2>Add New Poll</h2>

        <div>
            <label for="ddlClub">Club:</label>
            <asp:DropDownList ID="ddlClub" runat="server">
                <asp:ListItem Text="--Select Club--" Value="" />
            </asp:DropDownList>
        </div>

        <div>
            <label for="txtQuestion">Question:</label>
            <textarea id="txtQuestion" runat="server"></textarea>
        </div>

        <div>
            <label for="txtExpirationDate">Expiration Date:</label>
            <input type="date" id="txtExpirationDate" runat="server" />
        </div>

        <div>
            <button type="submit" runat="server" onserverclick="AddPoll_Click">Add Poll</button>
        </div>

        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>