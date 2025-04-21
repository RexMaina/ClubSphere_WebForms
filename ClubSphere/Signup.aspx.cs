using System;
using System.Collections.Generic;
using System.Web.UI;
using ClubSphere.Models;
using System.Security.Cryptography;
using System.Text;

namespace ClubSphere
{
    public partial class Signup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No additional logic is needed on page load.
        }

        // This method will be called on form submission.
        // Ensure your form in Signup.aspx posts back to this page.
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Check if the form was posted (i.e., if Request.Form is not empty)
            if (IsPostBack)
            {
                // Retrieve form values using the "name" attribute keys
                string fullName = Request.Form["FullName"];
                string email = Request.Form["Email"];
                string password = Request.Form["Password"];
                string confirmPassword = Request.Form["ConfirmPassword"];

                // Validate that passwords match
                if (password != confirmPassword)
                {
                    Response.Write("<script>alert('Passwords do not match');</script>");
                    return;
                }

                // Hash the password using SHA-256
                string hashedPassword = HashPassword(password);

                // Get the current timestamp as a string (or use DateTime.Now directly)
                string joinDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Create a dictionary to hold parameters for the query
                var parameters = new Dictionary<string, object>
                {
                    { "@Name", fullName },
                    { "@Email", email },
                    { "@Password", hashedPassword },
                    { "@JoinDate", joinDate },
                    { "@Role", "Member" }
                    // Note: We are not including ChooseClub or ProfilePictureURL since they are not collected.
                };

                // Define the INSERT query
                string query = "INSERT INTO Members (Name, Email, Password, JoinDate, Role) " +
                               "VALUES (@Name, @Email, @Password, @JoinDate, @Role)";

                // Execute the query using your CommonFn utility
                var commonFn = new CommonFn.Commonfnx();
                try
                {
                    commonFn.Query(query, parameters);

                    // Show success message and provide a link to Login.aspx.
                    Response.Write("<script>alert('User account successfully created!'); window.location='Login.aspx';</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                }
            }
        }

        // Method to hash the password securely using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash as a byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Convert the byte array to a hexadecimal string
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
