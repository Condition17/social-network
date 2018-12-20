using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewPersonalMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Utils.userIsAuthenticated () ) return;
    
        if ( !Page.IsPostBack)
        {
            // populate dropdown list
            this.GetFriends( Utils.GetCurrentUserUid() );
        }

        this.ShowContainer();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String senderId = Utils.GetCurrentUserUid();
        DropDownList DDFriends = LVMessage.FindControl("DDFriends") as DropDownList;
        String receiverId = DDFriends.SelectedItem.Value;

        TextBox TBMessage = LVMessage.FindControl("TBMessage") as TextBox;
        String message = TBMessage.Text;

        try
        {
            Messages.CreatePersonalMessage(senderId, receiverId, message);

        }catch( Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to send the message: " + ex.Message);

        }

        this.ShowSuccessMessage( "Message successfully sent" );

    }

    private void GetFriends( String userId)
    {
        DropDownList DDFriends = LVMessage.FindControl("DDFriends") as DropDownList;
        DDFriends.Items.Clear();

        String query = "SELECT UserID1, UserID2"
                            + " FROM Friends"
                            + " WHERE UserID1 = @id or UserID2 = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String uid = reader["UserID1"].ToString().Equals(userId) ? reader["UserID2"].ToString() : reader["UserID1"].ToString();
                String userName = Utils.GetUsername(uid);
                ProfileCommon profile = Profile.GetProfile(userName);
                UserProfile user = new UserProfile(uid, profile);

                ListItem listItem = new ListItem();

                listItem.Text = user.FirstName + " " + user.LastName;
                listItem.Value = uid;

                DDFriends.Items.Add(listItem);

            }

        }
        catch (Exception ex)
        { 
            this.ShowErrorMessage( "Error while trying to obtain friends list: " + ex.Message );
        }
        finally
        {
            con.Close();
        }
    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVMessage.FindControl("LBError") as Label;

        LBError.Text = message;
        LBError.Visible = true;
    }

    private void ShowSuccessMessage(String message)
    {
        Label LBSuccess = LVMessage.FindControl("LBSuccess") as Label;

        LBSuccess.Text = message;
        LBSuccess.Visible = true;
    }

    private void ShowContainer()
    {
        Control Container = LVMessage.FindControl("Container");
        Container.Visible = true;

    }
}
