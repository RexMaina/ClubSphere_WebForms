<%@ Page Title="View Clubs" Language="C#" MasterPageFile="~/Member/Membermst.master"
    AutoEventWireup="true" CodeBehind="ViewClubs.aspx.cs" Inherits="ClubSphere.Member.ViewClubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h3>Available Clubs</h3>
        <asp:GridView ID="gvAllClubs" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
            </Columns>
        </asp:GridView>

        <h3 class="mt-5">Joined Clubs</h3>
        <asp:GridView ID="gvJoinedClubs" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ClubName" HeaderText="Joined Club(s)" />
            </Columns>
        </asp:GridView>

        <div class="mt-4">
            <asp:Button ID="btnJoinClub" runat="server" Text="Join Club"
                        CssClass="btn btn-primary" OnClick="btnJoinClub_Click" />
        </div>

    </div>
</asp:Content>
