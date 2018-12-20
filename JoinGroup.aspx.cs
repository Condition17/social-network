using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JoinGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utils.userIsAuthenticated()) return;

        String currentUserId = Utils.GetCurrentUserUid();

        if (!Page.IsPostBack)
        {
            // populate dropdown list
            this.GetGroups(currentUserId);
        }

        this.ShowContainer();

    }

    protected void BtnJoin_Click(object sender, EventArgs e)
    {
        DropDownList DropDownList1 = LVGroups.FindControl("DropDownList1") as DropDownList;
        String groupId = DropDownList1.SelectedItem.Value;
        String currentUserId = Utils.GetCurrentUserUid();

        try
        {
            Group.AddUser(currentUserId, groupId);

        } catch( Exception ex)
        {
            this.ShowErrorMessage("An unexprected error occured while trying to join to selected group:" + ex.Message);
            return;
        }

        this.ShowSuccessMessage("Successfully joined to group.");
        return;

    }

    private void GetGroups(String userId)
    {
        DropDownList DropDownList1 = LVGroups.FindControl("DropDownList1") as DropDownList;
        DropDownList1.Items.Clear();
        
        String query = "SELECT Groups.Name, Groups.Id FROM " +
                       "Profiles_Groups join Groups on Profiles_Groups.GroupID = Groups.Id " +
                       "WHERE NOT Profiles_Groups.UserID = @id ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String groupId = reader["Id"].ToString();
                String groupName = reader["Name"].ToString();

                ListItem listItem = new ListItem();

                listItem.Text = groupName;
                listItem.Value = groupId;

                DropDownList1.Items.Add(listItem);

            }

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to get all groups you can join: " + ex.Message);
            return;
        }

    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVGroups.FindControl("LBError") as Label;
        Label LBSuccess = LVGroups.FindControl("LBSuccess") as Label;

        LBError.Text = message;
        LBSuccess.Visible = false;
        LBError.Visible = true;
    }

    private void ShowSuccessMessage(String message)
    {
        Label LBError = LVGroups.FindControl("LBError") as Label;
        Label LBSuccess = LVGroups.FindControl("LBSuccess") as Label;

        LBSuccess.Text = message;
        LBError.Visible = false;
        LBSuccess.Visible = true;
    }

    private void ShowContainer()
    {
        Control Container = LVGroups.FindControl("Container");
        Container.Visible = true;

    }
}

