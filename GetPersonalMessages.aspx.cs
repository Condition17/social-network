using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetPersonalMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Utils.userIsAuthenticated() )
        {
            Response.Redirect("/Index.aspx");
        }

        if( Request.Params["correspondent"] == null || Request.Params["correspondent"].Length == 0 )
        {
            Response.Redirect("/PersonalMessages.aspx");
        }

        String senderId = Request.Params["correspondent"];

        this.getMessagesWith(senderId);
        Container.Visible = true;
        MessagesInput.Visible = true;
    }

    private void getMessagesWith(String userId)
    {
        List<MessageInfo> results = new List<MessageInfo>();
        GridView1.DataSource = results;

        String currentUserId = Utils.GetCurrentUserUid();

        String query = "SELECT Messages_Profiles.AuthorID, Messages.Id, Messages.Text, Messages.Timestamp " +
                        "FROM Messages_Profiles join Messages on Messages_Profiles.MessageID = Messages.Id " +
                        "WHERE ( Messages_Profiles.AuthorID = @authorId and Messages_Profiles.ReceiverID = @receiverId) " +
                        "OR ( Messages_Profiles.AuthorID = @receiverId and Messages_Profiles.ReceiverID = @authorId )" +
                        "ORDER BY Messages.Timestamp ASC";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("authorId", currentUserId);
            cmd.Parameters.AddWithValue("receiverId", userId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String authorId = reader["AuthorID"].ToString();
                String messageId = reader["Id"].ToString();
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
        String senderId = Utils.GetCurrentUserUid();
        String receiverId = Request.Params["correspondent"];
        String message = TBMessage.Text;

        try
        {
            Messages.CreatePersonalMessage(senderId, receiverId, message);
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "An error occured while trying to send the message: " + ex.Message;

        }
        
    }
}