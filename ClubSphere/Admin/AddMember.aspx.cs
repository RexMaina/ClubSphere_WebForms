using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class AddMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the club dropdown when the page loads for the first time
                PopulateClubDropdown();
            }
        }

        private void PopulateClubDropdown()
        {
            try
            {
                // SQL query to fetch clubs from the database
                string query = "SELECT ClubID, ClubName FROM Clubs"; // Replace with your actual table and column names

                // Execute the query and get the data
                var commonFn = new CommonFn.Commonfnx();
                DataTable dt = commonFn.Fetch(query);

                // Bind the data to the dropdown
                ddlChooseClub.DataSource = dt;
                ddlChooseClub.DataTextField = "ClubName"; // Display text
                ddlChooseClub.DataValueField = "ClubID"; // Value field
                ddlChooseClub.DataBind();

                // Add a default "Select Club" option
                ddlChooseClub.Items.Insert(0, new ListItem("Select Club", ""));
            }
            catch (Exception ex)
            {
                // Handle any errors
                lblMessage.Text = "An error occurred while loading clubs: " + ex.Message;
                lblMessage.CssClass = "message error";
            }
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string clubID = ddlChooseClub.SelectedValue; // Get selected club ID from dropdown
            string role = ddlRole.SelectedValue;

            // Validate inputs
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(clubID) || string.IsNullOrEmpty(role))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.CssClass = "message error";
                return;
            }

            // Hash the password securely using SHA-256
            string hashedPassword = HashPassword(password);

            // Get the current timestamp for JoinDate
            string joinDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Handle profile picture upload (optional)
            string profilePictureURL = string.Empty;

            if (fileProfilePicture.HasFile)
            {
                string fileExtension = Path.GetExtension(fileProfilePicture.FileName).ToLower();

                // Check if the uploaded file is an image (jpg, png, jpeg)
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    // Generate a unique filename for the uploaded file
                    string fileName = Guid.NewGuid().ToString() + fileExtension; // Unique filename
                    string filePath = Server.MapPath("~/ProfileImages/") + fileName;

                    // Save the file to the server
                    fileProfilePicture.SaveAs(filePath);

                    // Store the path of the image
                    profilePictureURL = "~/ProfileImages/" + fileName;
                }
                else
                {
                    lblMessage.Text = "Invalid image format. Please upload a jpg, jpeg, or png file.";
                    lblMessage.CssClass = "message error";
                    return;
                }
            }

            // Create a dictionary for the parameters
            var parameters = new Dictionary<string, object>
            {
                { "@Name", name },
                { "@Email", email },
                { "@Password", hashedPassword },
                { "@ClubID", clubID }, // Use ClubID from dropdown
                { "@JoinDate", joinDate },
                { "@ProfilePictureURL", string.IsNullOrEmpty(profilePictureURL) ? DBNull.Value : (object)profilePictureURL },
                { "@Role", role }
            };

            // SQL query to insert a new member
            string query = "INSERT INTO Members (Name, Email, Password, ClubID, JoinDate, ProfilePictureURL, Role) " +
                           "VALUES (@Name, @Email, @Password, @ClubID, @JoinDate, @ProfilePictureURL, @Role)";

            try
            {
                // Execute the query to add the member
                var commonFn = new CommonFn.Commonfnx();
                commonFn.Query(query, parameters);

                // Display success message
                lblMessage.Text = "Member added successfully!";
                lblMessage.CssClass = "message success";

                // Clear the form fields after successful submission
                ClearForm();
            }
            catch (Exception ex)
            {
                // Display error message
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "message error";
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash of the password
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hex string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void ClearForm()
        {
            // Clear all input fields
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            ddlChooseClub.SelectedIndex = 0; // Reset dropdown to default
            ddlRole.SelectedIndex = 0; // Reset role dropdown to default
            fileProfilePicture.Attributes.Clear(); // Clear file upload
        }
    }
}