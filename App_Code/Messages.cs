using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Messages
/// </summary>
public class Messages
{
    public Messages()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int Create( String text)
    {
        if (text == null || text.Length == 0)
        {
            throw new Exception("The message couldn't be blank");
        }

        string query = "INSERT INTO Messages ( Text, Timestamp ) OUTPUT INSERTED.Id"
                    + " VALUES (@text, @timestamp)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("text", text);
            cmd.Parameters.AddWithValue("timestamp", DateTime.Now );

            int id = (int)cmd.ExecuteScalar();

            if (id == 0)
            {
                throw new Exception();
            }

            return id;
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

    public static void Delete(String messageID)
    {
        if (messageID == null || messageID.Length == 0)
        {
            throw new Exception("Invalid message Id");
        }

        String query = "DELETE FROM Messages WHERE Id = @messageID";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("messageID", messageID);

            int deletedRows = (int)cmd.ExecuteNonQuery();

            if( deletedRows == 0)
            {
                throw new Exception("Couldn't delete message");
            }

        }catch( Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

    }

    public static void CreatePersonalMessage(String senderID, String receiverID, String text)
    {

        if (senderID == null || senderID.Length == 0)
        {
            throw new Exception("Invalid sender Id");
        }

        if (receiverID == null || receiverID.Length == 0)
        {
            throw new Exception("Invalid receiver Id");
        }

        if (text == null || text.Length == 0)
        {
            throw new Exception("The message couldn't be blank");
        }

        int messageId = Create(text);

        String query = " INSERT INTO Messages_Profiles ( MessageID, AuthorID, ReceiverID ) " +
                        "VALUES( @messageId, @senderId, @receiverId )";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("messageId", messageId);
            cmd.Parameters.AddWithValue("senderId", senderID);
            cmd.Parameters.AddWithValue("receiverId", receiverID);

            cmd.ExecuteScalar();

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

    public static List<String> GetIds(String groupID)
    {
        if (groupID == null || groupID.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        List<String> ids = new List<String>();

        String query = "SELECT MessageID FROM Messages_Groups WHERE GroupID = @groupID";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupID", groupID);

            SqlDataReader reader = cmd.ExecuteReader();

            while( reader.Read())
            {
                String id = reader["MessageID"].ToString();
                ids.Add(id);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }

        return ids;
    }


    public static void CreateGroupMessage(String authorID, String groupID, String text)
    {

        if (authorID == null || authorID.Length == 0)
        {
            throw new Exception("Invalid author Id");
        }

        if ( groupID == null || groupID.Length == 0)
        {
            throw new Exception("Invalid group Id");
        }

        if (text == null || text.Length == 0)
        {
            throw new Exception("The message couldn't be blank");
        }

        int messageId = Create(text);

        String query = " INSERT INTO Messages_Groups ( MessageID, AuthorID, GroupID ) " +
                        "VALUES( @messageId, @authorId, @groupId )";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("messageId", messageId);
            cmd.Parameters.AddWithValue("authorId", authorID);
            cmd.Parameters.AddWithValue("groupId", groupID);

            int inserted = (int)cmd.ExecuteNonQuery();

            if( inserted == 0) {
                throw new Exception("Couldn't assign message to user");
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