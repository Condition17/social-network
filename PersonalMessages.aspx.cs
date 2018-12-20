using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PersonalMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Utils.userIsAuthenticated()) return;

        String currentUserId;
        int count = 0;

        List<MessageInfo> results = new List<MessageInfo>();
        GridView GridView1 = LVMessages.FindControl("GridView1") as GridView; 

        GridView1.DataSource = results;

        currentUserId = Utils.GetCurrentUserUid();

        String query = "SELECT MessageID, AuthorID, ReceiverID, Timestamp FROM " +
                        "Messages_Profiles join Messages on Messages_Profiles.MessageID = Messages.Id " +
                        "WHERE ReceiverID = @id or AuthorID = @id " +
                        "Order by Timestamp desc";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", currentUserId);

            SqlDataReader reader = cmd.ExecuteReader();

            while( reader.Read())
            {
                String messageID = reader["MessageID"].ToString();
                String authorID = String.Equals( reader["AuthorID"].ToString(), currentUserId ) ? reader["ReceiverID"].ToString() : reader["AuthorID"].ToString();

                DateTime timestamp = DateTime.Parse(reader["Timestamp"].ToString());

                String authorUsername = Utils.GetUsername(authorID);
                ProfileCommon profile = Profile.GetProfile(authorUsername);
                UserProfile authorProfile = new UserProfile(authorID, profile);

                MessageInfo message = new MessageInfo(authorProfile, authorID);

                message.Timestamp = timestamp;

                results.Add(message);
                count += 1;
            }

        }catch( Exception ex)
        {
            this.ShowErrorMessage("Some error encountered while getting personal messages: " + ex.Message );
        }

        Label nrRezultate = LVMessages.FindControl("nrRezultate") as Label;

        nrRezultate.Text = count.ToString()+ " messages";
        GridView1.DataBind();

        this.getAdminNotifications();

        Control Container = LVMessages.FindControl("Container");
        Container.Visible = true;
    }


    private void getAdminNotifications()
    {

        int nrNotifications = Notification.Count(Utils.GetCurrentUserUid());

        if (nrNotifications > 0)
        {
            Label l = LVMessages.FindControl("LBNotCount") as Label;
            l.Text = "Admin notifications: <br />" + nrNotifications.ToString() + " notifications";
            l.Visible = true;
        }

        String query = "SELECT Id, Timestamp FROM Notifications " +
            "WHERE UserID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        GridView GVNotifications = LVMessages.FindControl("GVNotifications") as GridView;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", Utils.GetCurrentUserUid());

            SqlDataReader reader = cmd.ExecuteReader();

            if( reader.HasRows)
            {
                GVNotifications.DataSource = reader;
                GVNotifications.DataBind();
            }
        }
        catch (Exception ex)
        {
           this.ShowErrorMessage( "An error occured while getting messages: " + ex.Message);
        }

    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVMessages.FindControl("LBError") as Label;

        LBError.Text = message;
        LBError.Visible = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewPersonalMessage.aspx");

    }

    protected void DelBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String notificationId = btn.CommandArgument;

        try
        {
            Notification.Delete(notificationId);
        }
        catch( Exception ex)
        {
            this.ShowErrorMessage(ex.Message);
        }

        Response.Redirect(Request.RawUrl);
    }
}