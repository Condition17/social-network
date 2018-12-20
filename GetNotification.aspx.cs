using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Params["id"] == null || Request.Params["id"].Length == 0)
        {
            this.ShowErrorMessage("Invalid notification id.");
            return;
        }

        String notificationId = Request.Params["id"];

        if (Notification.HasReceiver(notificationId, Utils.GetCurrentUserUid()) == false)
        {
            Response.Redirect("/PersonalMessages.aspx");
        }


        String query = "Select * FROM Notifications where Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", notificationId);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String text = reader["Text"].ToString();
                DateTime timestamp = new DateTime();
                timestamp = DateTime.Parse(reader["Timestamp"].ToString());

                Label t = LVNotifications.FindControl("AlertText") as Label;
                t.Text = text;

                t = LVNotifications.FindControl("LBTimestamp") as Label;
                t.Text = timestamp.ToString();
            }


            Control container = LVNotifications.FindControl("NotifContainer");
            container.Visible = true;

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to retrieve the notification: " + ex.Message);
            return;
        }

    }

    protected void DelBtn_Click(object sender, EventArgs e)
    {
        String notificationId = Request.Params["id"];

        try
        {
            Notification.Delete(notificationId);

        }catch( Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to delete this notification: " + ex.Message);
        }

        Response.Redirect("/PersonalMessages.aspx");
    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVNotifications.FindControl("LBError") as Label;

        if (LBError == null) return;

        LBError.Text = message;
        LBError.Visible = true;
    }

}