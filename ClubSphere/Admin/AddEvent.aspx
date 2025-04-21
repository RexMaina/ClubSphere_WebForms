<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="AddEvent.aspx.cs" Inherits="ClubSphere.Admin.AddEvent" %>

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

        #calendar {
            display: none;
            position: absolute;
            background-color: white;
            border: 1px solid #ddd;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            z-index: 999;
        }

        #calendar td {
            padding: 5px;
            cursor: pointer;
        }

        #calendar td:hover {
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Add New Event</h2>

        <div>
            <label for="txtEventName">Event Name:</label>
            <input type="text" id="txtEventName" runat="server" />
        </div>

        <div>
            <label for="ddlClubName">Club Name:</label>
            <asp:DropDownList ID="ddlClubName" runat="server">
                <asp:ListItem Text="--Select Club--" Value="" />
            </asp:DropDownList>
        </div>

        <div>
            <label for="txtDescription">Description:</label>
            <textarea id="txtDescription" runat="server"></textarea>
        </div>

        <div>
            <label for="txtEventDate">Event Date:</label>
            <input type="text" id="txtEventDate" runat="server" onclick="toggleCalendar()" />
            <div id="calendar">
                <asp:Calendar ID="calEventDate" runat="server" OnSelectionChanged="calEventDate_SelectionChanged" />
            </div>
        </div>

        <div>
            <label for="txtLocation">Location:</label>
            <input type="text" id="txtLocation" runat="server" />
        </div>

        <div>
            <label for="txtMaxParticipants">Max Participants:</label>
            <input type="number" id="txtMaxParticipants" runat="server" />
        </div>

        <div>
            <button type="submit" runat="server" onserverclick="AddEvent_Click">Add Event</button>
        </div>

        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>

    <script type="text/javascript">
        function toggleCalendar() {
            var calendar = document.getElementById('calendar');
            if (calendar.style.display === 'none' || calendar.style.display === '') {
                calendar.style.display = 'block';
            } else {
                calendar.style.display = 'none';
            }
        }

        function selectDate(date) {
            document.getElementById('<%= txtEventDate.ClientID %>').value = date;
            toggleCalendar();
        }
    </script>
</asp:Content>