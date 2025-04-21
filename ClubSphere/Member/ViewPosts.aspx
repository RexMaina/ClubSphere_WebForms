<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPosts.aspx.cs" Inherits="ClubSphere.Member.ViewPosts" MasterPageFile="~/Member/Membermst.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-3">Timeline</h3>

        <asp:Panel ID="PanelNoPosts" runat="server" Visible="false">
            <div class="alert alert-info">
                No posts yet. What’s on your mind? Create a post below:
            </div>
        </asp:Panel>

        <div class="card mb-4">
            <div class="card-body">
                <h5>Create a New Post</h5>
                <asp:TextBox ID="TxtPostTitle" runat="server" CssClass="form-control mb-2" placeholder="Post Title" />
                <asp:TextBox ID="TxtPostContent" runat="server" CssClass="form-control mb-2" TextMode="MultiLine" Rows="3" placeholder="Write something..." />
                <asp:Button ID="BtnSubmitPost" runat="server" Text="Post" CssClass="btn btn-primary" OnClick="BtnSubmitPost_Click" />
            </div>
        </div>

        <asp:Repeater ID="RepeaterPosts" runat="server" OnItemCommand="RepeaterPosts_ItemCommand">
            <ItemTemplate>
                <div class="card shadow-sm mb-3">
                    <div class="card-body">
                        <h5><%# Eval("Title") %></h5>
                        <p><%# Eval("Contents") %></p>
                        <small class="text-muted">Posted on <%# Eval("PostDate", "{0:dd MMM yyyy hh:mm tt}") %></small>
                        <div class="mt-2">
                            <asp:Button runat="server" CommandName="Like" CommandArgument='<%# Eval("PostID") %>' CssClass="btn btn-outline-success btn-sm" Text="👍 Like" />
                            <asp:Button runat="server" CommandName="Dislike" CommandArgument='<%# Eval("PostID") %>' CssClass="btn btn-outline-danger btn-sm" Text="👎 Dislike" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
