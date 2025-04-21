using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ModifyMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                lblMessage.Text = "Please enter a name to search.";
                lblMessage.CssClass = "message error";
                return;
            }

            string query = "SELECT Name, Email, ChooseClub, ProfilePictureURL FROM Members WHERE Name = @Name";
            var parameters = new Dictionary<string, object> { { "@Name", name } };

            var commonFn = new CommonFn.Commonfnx();
            DataTable dt = commonFn.Fetch(query, parameters);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtChooseClub.Text = dt.Rows[0]["ChooseClub"].ToString();
            }
            else
            {
                lblMessage.Text = "Member not found.";
                lblMessage.CssClass = "message error";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string chooseClub = txtChooseClub.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(chooseClub))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.CssClass = "message error";
                return;
            }

            string profilePictureURL = null;

            if (fileProfilePicture.HasFile)
            {
                string fileExtension = Path.GetExtension(fileProfilePicture.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    string fileName = Path.GetFileName(fileProfilePicture.PostedFile.FileName);
                    string filePath = Server.MapPath("~/ProfileImages/") + fileName;
                    fileProfilePicture.SaveAs(filePath);
                    profilePictureURL = "~/ProfileImages/" + fileName;
                }
                else
                {
                    lblMessage.Text = "Invalid image format. Please upload a jpg, jpeg, or png file.";
                    lblMessage.CssClass = "message error";
                    return;
                }
            }

            var parameters = new Dictionary<string, object>
            {
                { "@Name", name },
                { "@Email", email },
                { "@ChooseClub", chooseClub },
                { "@ProfilePictureURL", profilePictureURL ?? (object)DBNull.Value }
            };

            string updateQuery = "UPDATE Members SET Email = @Email, ChooseClub = @ChooseClub, " +
                                 "ProfilePictureURL = @ProfilePictureURL WHERE Name = @Name";

            try
            {
                var commonFn = new CommonFn.Commonfnx();
                commonFn.Query(updateQuery, parameters);

                lblMessage.Text = "Member details updated successfully!";
                lblMessage.CssClass = "message success";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "message error";
            }
        }
    }
}
