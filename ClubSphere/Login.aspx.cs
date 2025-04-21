using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using ClubSphere.Models;

namespace ClubSphere
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Clear message on page load
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            // Debug: Print email to check if input is received
            System.Diagnostics.Debug.WriteLine("Login Attempt: " + email);

            // Query to fetch stored hashed password and role
            string query = "SELECT Password, Role FROM Members WHERE Email = @Email";

            var parameters = new Dictionary<string, object> { { "@Email", email } };

            try
            {
                var commonFn = new CommonFn.Commonfnx();
                DataTable dt = commonFn.Fetch(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    string storedHashedPassword = dt.Rows[0]["Password"].ToString();
                    string role = dt.Rows[0]["Role"].ToString();

                    // Debug: Print retrieved password hash
                    System.Diagnostics.Debug.WriteLine("Stored Hashed Password: " + storedHashedPassword);

                    // Compare hashed passwords
                    if (VerifyPassword(password, storedHashedPassword))
                    {
                        // ✅ Store session values
                        Session["Email"] = email;
                        Session["Role"] = role;

                        // Debug: Print role
                        System.Diagnostics.Debug.WriteLine("User Role: " + role);

                        // Redirect based on role
                        if (role == "Administrator")
                        {
                            Response.Redirect("~/Admin/AdminHome.aspx");
                        }
                        else if (role == "Member")
                        {
                            Response.Redirect("~/Member/MemberHome.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid role assigned. Please contact support.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password. Please try again.";
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid email or password. Please try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
