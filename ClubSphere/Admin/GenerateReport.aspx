<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateReport.aspx.cs" Inherits="ClubSphere.Admin.GenerateReport" MasterPageFile="~/Admin/Adminmst.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .report-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .report-options {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-bottom: 20px;
        }

        .report-options button {
            padding: 10px 20px;
            background-color: #5558c9;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .report-options button:hover {
            background-color: #4e51b7;
        }

        .report-output {
            margin-top: 20px;
            padding: 20px;
            background: #f8f9fa;
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="report-container">
        <h2>Generate Report</h2>
        <div class="report-options">
            <asp:Button ID="btnWeekly" runat="server" Text="Weekly Report" OnClick="GenerateWeeklyReport" />
            <asp:Button ID="btnMonthly" runat="server" Text="Monthly Report" OnClick="GenerateMonthlyReport" />
            <asp:Button ID="btnAnnual" runat="server" Text="Annual Report" OnClick="GenerateAnnualReport" />
        </div>
        <div class="report-output">
            <asp:Literal ID="litReportOutput" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>