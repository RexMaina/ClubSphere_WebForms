<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClubSphere.Login" MasterPageFile="~/Authentication.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="app-body">
    <form id="loginForm" method="post" runat="server">
      <!-- Email Field -->
      <div class="mb-3">
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" required="true"></asp:TextBox>
      </div>

      <!-- Password Field -->
      <div class="mb-3">
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" required="true"></asp:TextBox>
      </div>

      <!-- Login Button -->
      <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />

      <!-- Error Message -->
      <div class="mt-3">
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
      </div>
    </form>

    <!-- Social Login Options -->
    <div class="social-login mt-3">
      <p class="text-muted">Or login with</p>
      <button class="btn"><i class="fab fa-google"></i></button>
      <button class="btn"><i class="fab fa-facebook-f"></i></button>
      <button class="btn"><i class="fab fa-twitter"></i></button>
    </div>

    <!-- Toggle to Signup -->
    <div class="toggle-form mt-3">
      <p>Don't have an account? <a href="Signup.aspx">Sign Up</a></p>
    </div>
  </div>
</asp:Content>