using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetGroupMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( Utils.userIsAuthenticated() == false)
        {
            Response.Redirect("/Index.aspx");
        }

        if (Request.Params["groupid"] != null && Request.Params["groupid"].Length == 0)
        {
            Response.Redirect("/GetGroups.aspx");
        }

        String groupId = Request.Params["groupid"];
        try
        {

            if (!Group.ExistsById(groupId))
            {
                this.ShowErrorMessage("This group doesn't exist");
                return;
            }

            String currentUserId = Utils.GetCurrentUserUid();

            Boolean isGroupMember = Group.HasMember(groupId, currentUserId);

            if (!isGroupMember && !User.IsInRole("Admin"))
            {
                this.ShowErrorMessage("You don't have access to this group. Please join it first.");
                return;
            }

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage(ex.Message);
            return;

        }

        this.LoadGroupMembers(groupId);
        Container.Visible = true;

    }

    private void LoadGroupMembers( String groupId)
    {

        List<UserProfile> results = new List<UserProfile>();

        String currentUserId = Utils.GetCurrentUserUid();

        GridView1.DataSource = results;

        String query = "SELECT UserID"
                        + " FROM Profiles_Groups"
                        + " WHERE GroupID = @groupId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupId", groupId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String uid = reader["UserID"].ToString();
                String userName = Utils.GetUsername(uid);

                ProfileCommon profile = Profile.GetProfile(userName);
                UserProfile user = null;

                if (profile != null)
                {
                    user = new UserProfile(uid, profile);
                }
                results.Add(user);
            }

            nrRezultate.Text = results.Count.ToString() + " membri";
        }
        catch (Exception ex)
        {
            this.ShowErrorMessage( "Error encountered durring this operation: " + ex.Message );
        }
        finally
        {
            con.Close();
        }

        GridView1.DataBind();
    }

    private void ShowErrorMessage( String message)
    {
        LBError.Visible = true;
        LBError.Text = message;
    }

    protected void DelBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String userId = btn.CommandArgument;
        String groupId = Request.Params["groupid"];

        try
        {
            Group.RemoveUser(userId, groupId);

            if (!Group.HasUsers(groupId))
            {
                Group.Delete(groupId);
                Response.Redirect("/AdminGroups.aspx");

            }

        }
        catch (Exception ex)
        {
           this.ShowErrorMessage("An error occurred durring the operation: " + ex.Message );
        }

        Response.Redirect(Request.RawUrl);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow row = e.Row;
            LinkButton DelBtn = row.FindControl("DelBtn") as LinkButton;
            DelBtn.Visible = User.IsInRole("Admin");
            
        }
    }
}