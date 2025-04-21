using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using ClubSphere.Models;

namespace ClubSphere.Admin
{
    public partial class ModifyClub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the clubs into the dropdown list
                LoadClubs();
            }
        }

        private void LoadClubs()
        {
            // Fetch all clubs from the database
            string query = "SELECT ClubID, ClubName FROM Clubs";
            var commonFn = new CommonFn.Commonfnx();
            DataTable dt = commonFn.Fetch(query);

            // Bind the data to the dropdown list
            ddlClub.DataSource = dt;
            ddlClub.DataTextField = "ClubName";
            ddlClub.DataValueField = "ClubID";
            ddlClub.DataBind();

            // Add a default item
            ddlClub.Items.Insert(0, new ListItem("-- Select Club --", "0"));
        }

        protected void ddlClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClub.SelectedValue != "0")
            {
                // Fetch the selected club's details
                string query = "SELECT ClubName, Description, CreationDate FROM Clubs WHERE ClubID = @ClubID";
                var parameters = new Dictionary<string, object>
                {
                    { "@ClubID", ddlClub.SelectedValue }
                };

                var commonFn = new CommonFn.Commonfnx();
                DataTable dt = commonFn.Fetch(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    // Populate the form fields with the club's details
                    txtClubName.Text = dt.Rows[0]["ClubName"].ToString();
                    txtDescription.Text = dt.Rows[0]["Description"].ToString();
                    txtCreationDate.Text = Convert.ToDateTime(dt.Rows[0]["CreationDate"]).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                // Clear the form fields if no club is selected
                txtClubName.Text = "";
                txtDescription.Text = "";
                txtCreationDate.Text = "";
            }
        }

        protected void btnUpdateClub_Click(object sender, EventArgs e)
        {
            if (ddlClub.SelectedValue != "0")
            {
                // Get the input values from the form
                string clubID = ddlClub.SelectedValue;
                string clubName = txtClubName.Text.Trim();
                string description = txtDescription.Text.Trim();
                string creationDate = txtCreationDate.Text.Trim();

                // Validate inputs
                if (string.IsNullOrEmpty(clubName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(creationDate))
                {
                    lblMessage.Text = "Please fill in all fields.";
                    lblMessage.CssClass = "message error";
                    return;
                }

                // Create a dictionary for parameters
                var parameters = new Dictionary<string, object>
                {
                    { "@ClubName", clubName },
                    { "@Description", description },
                    { "@CreationDate", creationDate },
                    { "@ClubID", clubID }
                };

                // Define the SQL query
                string query = "UPDATE Clubs SET ClubName = @ClubName, Description = @Description, CreationDate = @CreationDate WHERE ClubID = @ClubID";

                // Execute the query
                try
                {
                    var commonFn = new CommonFn.Commonfnx();
                    commonFn.Query(query, parameters);

                    // Display success message
                    lblMessage.Text = "Club updated successfully!";
                    lblMessage.CssClass = "message success";

                    // Reload the clubs in the dropdown list
                    LoadClubs();
                }
                catch (Exception ex)
                {
                    // Display error message
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.CssClass = "message error";
                }
            }
            else
            {
                lblMessage.Text = "Please select a club to update.";
                lblMessage.CssClass = "message error";
            }
        }
    }
}