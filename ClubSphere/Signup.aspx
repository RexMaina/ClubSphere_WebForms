<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="ClubSphere.Signup" MasterPageFile="~/Authentication.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="app-body">
    <form id="signupForm" method="post" runat="server">
      <!-- Sign Up Form Fields -->
      <div class="mb-3">
        <input type="text" class="form-control" placeholder="Full Name" name="FullName" required />
      </div>
      <div class="mb-3">
        <input type="email" class="form-control" placeholder="Email" name="Email" required />
      </div>
      <div class="mb-3">
        <input type="password" class="form-control" placeholder="Password" name="Password" required />
      </div>
      <div class="mb-3">
        <input type="password" class="form-control" placeholder="Confirm Password" name="ConfirmPassword" required />
      </div>
      <button type="submit" class="btn btn-primary">Sign Up</button>
    </form>

    <!-- Social Sign Up -->
    <div class="social-signup mt-3">
      <p class="text-muted">Or sign up with</p>
      <button class="btn"><i class="fab fa-google"></i></button>
      <button class="btn"><i class="fab fa-facebook-f"></i></button>
      <button class="btn"><i class="fab fa-twitter"></i></button>
    </div>

    <!-- Toggle to Login -->
    <div class="toggle-form mt-3">
      <p>Already have an account? <a href="Login.aspx">Login</a></p>
    </div>
  </div>
</asp:Content>
