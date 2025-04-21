<%@ Page Title="Join Club" Language="C#" MasterPageFile="~/Member/Membermst.master"
    AutoEventWireup="true" CodeBehind="JoinClub.aspx.cs" Inherits="ClubSphere.Member.JoinClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-lg">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0"><i class="fas fa-user-plus"></i> Join a Club</h4>
            </div>
            <div class="card-body">

                <!-- Status (Already joined club name or not) -->
                <asp:Label ID="lblStatus" runat="server" CssClass="alert alert-info d-block"></asp:Label>

                <div class="form-group mb-3">
                    <label for="ddlClubs">Select Club</label>
                    <asp:DropDownList ID="ddlClubs" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <!-- Join button -->
                <asp:Button ID="btnJoin" runat="server" Text="Join This Club"
                    CssClass="btn btn-success me-2" OnClick="btnJoin_Click" />

                <!-- Leave button (hidden unless user has joined a club) -->
                <asp:Button ID="btnLeave" runat="server" Text="Leave Club"
                    CssClass="btn btn-danger" OnClick="btnLeave_Click" Visible="false" />

                <!-- Message output -->
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 d-block fw-bold"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
