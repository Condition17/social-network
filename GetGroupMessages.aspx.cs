using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetGroupMessages : System.Web.UI.Page
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
                LBError.Visible = true;
                LBError.Text = "This group doesn't exist";
                return;
            }

            String currentUserId = Utils.GetCurrentUserUid();

            Boolean isGroupMember = Group.HasMember(groupId, currentUserId);

            if ( !isGroupMember && !User.IsInRole("Admin") )
            {
                LBError.Visible = true;
                LBError.Text = "You don't have access to this group. Please join it first.";
            }

            if( isGroupMember)
            {
                BtnLeave.Visible = true;
                CommentArea.Visible = true;
            }

            if( User.IsInRole("Admin") || isGroupMember)
            {
                Container.Visible = true;
            }

        }catch( Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = ex.Message;
            return;
        }

        HLGrMembers.NavigateUrl = "/GetGroupMembers.aspx?groupid=" + groupId;

        this.GetMessages(groupId);

    }

    private void GetMessages( String groupId)
    {

        List<MessageInfo> results = new List<MessageInfo>();

        GridView1.DataSource = results;

        String query = "SELECT Messages.Text, Messages.Timestamp, AuthorID " +
                       "FROM Messages_Groups join Messages on Messages_Groups.MessageID = Messages.Id " +
                       "WHERE GroupID = @groupId " +
                       "ORDER BY Messages.Timestamp ASC";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupId", groupId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if( LBNoMsg.Visible)
                {
                    LBNoMsg.Visible = false;
                }

                String authorId = reader["AuthorID"].ToString();
                String text = reader["Text"].ToString();
                DateTime timestamp = new DateTime();
                timestamp = DateTime.Parse(reader["Timestamp"].ToString());

                String authorUsername = Utils.GetUsername(authorId);
                ProfileCommon profile = Profile.GetProfile(authorUsername);
                UserProfile authorProfile = new UserProfile(authorId, profile);

                MessageInfo message = new MessageInfo(authorProfile, authorId);
                message.Timestamp = timestamp;
                message.Text = text;

                results.Add(message);
            }

        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "Error occured while getting messages";
        }
        finally
        {
            con.Close();
        }

        GridView1.DataBind();

    }

    protected void MessageBtn_Click(object sender, EventArgs e)
    {
        String authorId = Utils.GetCurrentUserUid();
        String groupId = Request.Params["groupid"];
        String message = TBMessage.Text;

        try
        {
            Messages.CreateGroupMessage(authorId, groupId, message);
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "An error occured while trying to send the message: " + ex.Message;
        }

    }

    protected void Btn_LeaveGroup(object sender, EventArgs e)
    {
        String groupId = Request.Params["groupid"];
        String currentUserId = Utils.GetCurrentUserUid();

        try
        {
            Group.RemoveUser(currentUserId, groupId);

            if ( !Group.HasUsers(groupId) )
            {
                Group.Delete(groupId);
            }

            Response.Redirect("/GetGroups.aspx");

        }catch( Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "An error occurred durring the operation: " + ex.Message;
        }

    }


}