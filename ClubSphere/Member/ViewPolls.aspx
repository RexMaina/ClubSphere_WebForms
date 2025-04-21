<%@ Page Title="View Polls" Language="C#" MasterPageFile="~/Member/Membermst.master"
    AutoEventWireup="true" CodeBehind="ViewPolls.aspx.cs" Inherits="ClubSphere.Member.ViewPolls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3>Available Polls</h3>
        <asp:Repeater ID="rptPolls" runat="server" OnItemCommand="rptPolls_ItemCommand">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <h5><%# Eval("Question") %></h5>
                        <p><small>Created: <%# Eval("CreationDate", "{0:dd MMM yyyy}") %> | Expires: <%# Eval("ExpirationDate", "{0:dd MMM yyyy}") %></small></p>
                        <div class="mb-2">
                            <asp:Button ID="btnLike" runat="server" Text="👍 Like" CssClass="btn btn-outline-success me-2"
                                        CommandName="Like" CommandArgument='<%# Eval("Poll_Id") %>' />
                            <asp:Button ID="btnDislike" runat="server" Text="👎 Dislike" CssClass="btn btn-outline-danger me-2"
                                        CommandName="Dislike" CommandArgument='<%# Eval("Poll_Id") %>' />
                        </div>
                        <div class="input-group mt-2">
                            <asp:TextBox ID="txtResponse" runat="server" CssClass="form-control" placeholder="Type your feedback..." />
                            <asp:Button ID="btnRespond" runat="server" Text="Respond" CssClass="btn btn-primary"
                                        CommandName="Respond" CommandArgument='<%# Eval("Poll_Id") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
