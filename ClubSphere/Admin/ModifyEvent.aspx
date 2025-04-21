<%@ Page Language="C#" MasterPageFile="~/Admin/Adminmst.Master" AutoEventWireup="true" CodeBehind="ModifyEvent.aspx.cs" Inherits="ClubSphere.Admin.ModifyEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Form Styling */
        .form-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 30px; /* Increased padding */
            background: #fff;
            border-radius: 10px; /* More rounded corners */
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.15); /* Softer shadow */
        }

        .form-container h2 {
            margin-bottom: 30px; /* Increased margin */
            font-size: 1.8rem; /* Larger heading */
            color: #5558c9;
            text-align: center; /* Center the heading */
        }

        .form-group { /* New class for form groups */
            margin-bottom: 20px;
        }

        .form-container label {
            font-size: 1rem;
            margin-bottom: 8px;
            display: block;
            color: #777; /* Slightly darker label color */
        }

        .form-container input,
        .form-container textarea,
        .form-container select {
            width: 100%;
            padding: 12px; /* Increased padding */
            margin-bottom: 0;
            border-radius: 6px; /* More rounded corners */
            border: 1px solid #ddd;
            box-sizing: border-box; /* Include padding in width */
            font-size: 1rem;
            transition: border-color 0.3s ease; /* Smooth border transition */
        }

        .form-container input:focus,
        .form-container textarea:focus,
        .form-container select:focus {
            border-color: #5558c9; /* Highlight border on focus */
            outline: none;
            box-shadow: 0 0 5px rgba(85, 88, 201, 0.2); /* Subtle box shadow on focus */
        }

        .form-container textarea {
            height: 120px; /* Increased height for textarea */
            resize: vertical; /* Allow vertical resizing */
        }

        .form-container button, .form-container .asp-button{
            background-color: #5558c9;
            color: white;
            padding: 12px 25px; /* Increased padding */
            border: none;
            border-radius: 6px; /* More rounded corners */
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease; /* Smooth background transition */
        }

        .form-container button:hover, .form-container .asp-button:hover {
            background-color: #4e51b7;
        }

        .message {
            margin-top: 20px;
            font-size: 0.9rem;
        }

        .message.success {
            color: #28a745;
        }

        .message.error {
            color: #dc3545;
        }

        /* Calendar Styling */
        #calendar {
            position: absolute;
            background-color: white;
            border: 1px solid #ddd;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
            z-index: 999;
            border-radius: 6px; /* Rounded corners for calendar */
            padding: 10px;
        }

        #calendar td {
            padding: 8px; /* Increased padding for calendar cells */
            cursor: pointer;
        }

        #calendar td:hover {
            background-color: #f1f1f1;
        }

        /* Panel Styling */
        #pnlEventDetails {
            padding: 20px;
            border: 1px solid #eee;
            border-radius: 8px;
            margin-top: 20px;
            background-color: #f9f9f9; /* Slightly off-white background */
        }

        /* Hide the panel initially and show it only when an event is selected */
        #pnlEventDetails.hidden {
            display: none;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Modify Event</h2>

        <div class="form-group">
            <label for="ddlEvent">Select Event:</label>
            <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" CssClass="form-control">
                <asp:ListItem Text="--Select Event--" Value="" />
            </asp:DropDownList>
        </div>

        <asp:Panel ID="pnlEventDetails" runat="server" Visible="false" CssClass="form-group">
            <div class="form-group">
                <label for="txtEventName">Event Name:</label>
                <input type="text" id="txtEventName" runat="server" CssClass="form-control"/>
            </div>

            <div class="form-group">
                <label for="ddlClubName">Club Name:</label>
                <asp:DropDownList ID="ddlClubName" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--Select Club--" Value="" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <textarea id="txtDescription" runat="server" CssClass="form-control"></textarea>
            </div>

            <div class="form-group">
                <label for="txtEventDate">Event Date:</label>
                <input type="date" id="txtEventDate" runat="server" onclick="toggleCalendar()" class="form-control"/>
    
                <div id="calendar" style="display: none; position: absolute; z-index: 1000; background: white; border: 1px solid #ccc; padding: 5px;">
                    <asp:Calendar ID="calEventDate" runat="server" OnSelectionChanged="calEventDate_SelectionChanged" />
                </div>
            </div>

            <script>
                function toggleCalendar() {
                    var calendar = document.getElementById("calendar");
                    calendar.style.display = (calendar.style.display === "none") ? "block" : "none";
                }

                document.addEventListener("click", function (event) {
                    var input = document.getElementById("txtEventDate");
                    var calendar = document.getElementById("calendar");

                    if (event.target !== input && !calendar.contains(event.target)) {
                        calendar.style.display = "none";
                    }
                });
            </script>


            <div class="form-group">
                <label for="txtLocation">Location:</label>
                <input type="text" id="txtLocation" runat="server" CssClass="form-control"/>
            </div>

            <div class="form-group">
                <label for="txtMaxParticipants">Max Participants:</label>
                <input type="number" id="txtMaxParticipants" runat="server" CssClass="form-control"/>
            </div>

            <div class="form-group">
                <button type="submit" runat="server" onserverclick="UpdateEvent_Click" CssClass="btn btn-primary">Update Event</button>
                <asp:Button ID="btnDelete" runat="server" Text="Delete Event" OnClick="DeleteEvent_Click"
                    OnClientClick="return confirm('Are you sure you want to delete this event?');" CssClass="asp-button btn btn-danger" />
            </div>
        </asp:Panel>

        <div class="form-group">
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
    </script>
</asp:Content>