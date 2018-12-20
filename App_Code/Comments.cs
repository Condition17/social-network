using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comments
/// </summary>
public class Comments
{
    public Comments()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int Create(String authorId, String postId, String text)
    {
        if (authorId == null || authorId.Length == 0)
        {
            throw new Exception("Invalid authorID");
        }

        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid postID");
        }

        if (text == null || text.Length == 0)
        {
            throw new Exception("Invalid comment content.");
        }

        string query = "INSERT INTO Comments ( UserID, PostID, Text, Timestamp) OUTPUT INSERTED.Id"
                    + " VALUES (@authorId, @postId, @text, @timestamp)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("authorId", authorId);
            cmd.Parameters.AddWithValue("postId", postId);
            cmd.Parameters.AddWithValue("text", text);
            cmd.Parameters.AddWithValue("timestamp", DateTime.Now);

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

    public static void Delete( String commentId)
    {
        if( commentId == null || commentId.Length == 0)
        {
            throw new Exception("Invalid comment id");
        }

        String query = "DELETE FROM Comments WHERE Id =@id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", commentId);

            int deleted = cmd.ExecuteNonQuery();

            if (deleted == 0)
            {
                throw new Exception();
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

    public static List<String> GetCommentsIds(String postId)
    {

        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id");
        }

        List<String> ids = new List<String>();

        String query = "SELECT Id " +
                    "FROM Comments " +
                    "where PostID = @postId ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("postId", postId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String id = reader["Id"].ToString();
                ids.Add(id);
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

        return ids;
    }

}