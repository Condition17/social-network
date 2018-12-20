using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utils.userIsAuthenticated()) return;

        String currentUserID = null;

        currentUserID = Utils.GetCurrentUserUid();
        this.getFriendRequests(currentUserID);

        Control Container = LVFriends.FindControl("Container");
        Container.Visible = true;
    }



    protected void BtnAccept_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;

        String senderId = btn.CommandArgument;
        String currentUserID = Utils.GetCurrentUserUid();

        try
        {
            this.DeleteFriendRequest(senderId, currentUserID);
            this.AddFriend(senderId, currentUserID);
            Response.Redirect(Request.RawUrl);

        }catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to finish the operation:" + ex.Message);
        }

    }

    protected void BtnDecline_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;

        String senderId = btn.CommandArgument;
        String currentUserID = Utils.GetCurrentUserUid();

        try
        {
            this.DeleteFriendRequest(senderId, currentUserID);
            Response.Redirect(Request.RawUrl);

        }
        catch ( Exception ex)
        {
            this.ShowErrorMessage( "An error occured while trying to finish the operation:" + ex.Message );
        }

    }

    private void AddFriend( String id1, String id2)
    {

        string query = "INSERT INTO Friends (UserID1,UserID2) OUTPUT INSERTED.UserID1"
                   + " VALUES (@id1, @id2)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id1", id1);
            cmd.Parameters.AddWithValue("id2", id2);

            Guid id = (Guid)cmd.ExecuteScalar();

            if (id == null) { 
                throw new Exception();
            }

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to finish the operation: " + ex.Message);
        }
        finally
        {
            con.Close();
        }

    }

    private void DeleteFriendRequest( String senderId, String receiverId)
    {
        string query = "DELETE FROM FriendRequests"
                + " WHERE (SenderID = @sender and ReceiverID = @receiver) or (SenderID = @receiver and ReceiverID = @sender)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("sender", senderId);
            cmd.Parameters.AddWithValue("receiver", receiverId);

            int deleted = cmd.ExecuteNonQuery();

            if (deleted == 0)
            {
                throw new Exception("Friend request couldn't be declined");
            }
            

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to finish the operation: " + ex.Message);

        }
        finally
        {
            con.Close();
        }

    }

    private void getFriendRequests( String currentUserID)
    {
        List<UserProfile> results = new List<UserProfile>();
        GridView GridView1 = LVFriends.FindControl("GridView1") as GridView;
        Label nrRezultate = LVFriends.FindControl("nrRezultate") as Label;

        GridView1.DataSource = results;

        String query = "SELECT SenderID"
                        + " FROM FriendRequests"
                        + " WHERE ReceiverID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", currentUserID);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String uid = reader["SenderID"].ToString();
                String userName = Utils.GetUsername(uid);

                ProfileCommon profile = Profile.GetProfile(userName);
                UserProfile user = null;

                if (profile != null)
                {
                    user = new UserProfile(uid, profile);
                }
                results.Add(user);
            }

            nrRezultate.Text = results.Count.ToString() + " cereri";
        }
        catch (Exception _)
        {

        }
        finally
        {
            con.Close();
        }

        GridView1.DataBind();
    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVFriends.FindControl("LBError") as Label;

        if (LBError == null) return;

        LBError.Text = message;
        LBError.Visible = true;
    }

}