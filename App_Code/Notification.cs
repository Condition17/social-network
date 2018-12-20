using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Notification
/// </summary>
public class Notification
{
    public Notification()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Boolean HasReceiver( String notificationId, String userID)
    {

        if (userID == null || userID.Length == 0)
        {
            throw new Exception("Invalid user id.");
        }

        string query = "SELECT count(*) FROM Notifications WHERE UserID = @id and Id = @notificationId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userID);
            cmd.Parameters.AddWithValue("notificationId", notificationId);

            return (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            con.Close();
        }

    }

    public static int Count(String userID)
    {

        if (userID == null || userID.Length == 0)
        {
            throw new Exception("Invalid user id.");
        }

        string query = "SELECT count(*) FROM Notifications WHERE UserID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        int nrNotifications = 0;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userID);
            nrNotifications = (int)cmd.ExecuteScalar();

            return nrNotifications;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

    public static void Create( String userID, String text)
    {
        if( userID == null || userID.Length == 0)
        {
            throw new Exception("Invalid user id.");
        }

        if (text == null || text.Length == 0)
        {
            throw new Exception("Notification text couldn't be blank.");
        }

        string query = "INSERT INTO Notifications ( UserID, Text, Timestamp ) OUTPUT INSERTED.Id"
                    + " VALUES (@userId, @text, @timestamp)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("userId", userID);
            cmd.Parameters.AddWithValue("text", text);
            cmd.Parameters.AddWithValue("timestamp", DateTime.Now );

            int id = (int)cmd.ExecuteScalar();

            if (id == 0)
            {
                throw new Exception("Notification couldn't be sent.");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }

    public static void Delete( String id)
    {
        if( id == null || id.Length == 0)
        {
            throw new Exception("Invalid notification id.");
        }

        String query = "DELETE FROM Notifications" +
           " WHERE Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", id);

            int deleted = (int)cmd.ExecuteNonQuery();

            if( deleted == 0)
            {
                throw new Exception("Notification couln't be deleted");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

    }
}