using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Post
/// </summary>
public class Post
{
    public Post()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int Create( String authorId, String photoId, String text)
    {
        if (authorId == null || authorId.Length == 0)
        {
            throw new Exception("Invalid authorID");
        }

        

        string query = "INSERT INTO Posts ( AuthorID, PhotoID, Text, Timestamp) OUTPUT INSERTED.Id"
                    + " VALUES (@authorId, @photoId, @text, @timestamp)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("authorId", authorId);

            if (photoId == null || photoId.Length == 0)
            {
                cmd.Parameters.AddWithValue("photoId", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("photoId", photoId);
            }

            if (text == null || text.Length == 0)
            {
                cmd.Parameters.AddWithValue("text", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("text", text);
            }

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

    public static void DeletePhoto( String photoId)
    {

        if ( photoId == null || photoId.Length == 0)
        {
            throw new Exception("Invalid photo id.");
        }

        String query = "UPDATE Posts " +
            "SET PhotoID = @val, Timestamp = @timestamp " +
            "WHERE PhotoID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", photoId);
            cmd.Parameters.AddWithValue("val", DBNull.Value);
            cmd.Parameters.AddWithValue("timestamp", DateTime.Now);

            int updated = (int)cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

        Photo.Delete(photoId);

    }

    public static Boolean HasAuthor( String postId, String authorId )
    {
        if (authorId == null || authorId.Length == 0)
        {
            throw new Exception("Invalid user id.");
        }

        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id.");
        }

        String query = "SELECT count(*) "
                        + " FROM Posts"
                        + " WHERE Id = @id and AuthorID = @authorId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", postId);
            cmd.Parameters.AddWithValue("authorId", authorId);

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

        return exists;
    }

    public static Boolean HasPhoto( String postId)
    {
        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id.");
        }

        String query = "SELECT count(*) "
                        + " FROM Posts"
                        + " WHERE Id = @id and PhotoID is not null";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", postId);

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

        return exists;

    }

    public static void Update( String postId, String text)
    {

        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id.");
        }

        String query = "UPDATE Posts " +
                       "SET Text = @text, Timestamp = @timestamp " +
                       "WHERE Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", postId);

            if (text == null || text.Length == 0)
            {
                cmd.Parameters.AddWithValue("text", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("text", text);
            }

            cmd.Parameters.AddWithValue("timestamp", DateTime.Now );

            int updated = (int)cmd.ExecuteNonQuery();

            if( updated == 0)
            {
                throw new Exception("Post not updated. Try again!");
            }

        }catch( Exception ex)
        {

            throw ex;

        } finally
        {
            con.Close();
        }
        
    }

    public static String GetPhotoId( String postId)
    {

        if (postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id");
        }

        String photoId = "";

        String query = "SELECT PhotoID FROM Posts WHERE Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", postId);

            SqlDataReader reader = cmd.ExecuteReader();

            while( reader.Read())
            {
                photoId = reader["PhotoID"].ToString();
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

        return photoId;
    }

    public static void Delete( String postId)
    {
        if( postId == null || postId.Length == 0)
        {
            throw new Exception("Invalid post id");
        }

        // delete the post comments
        List<String> ids = Comments.GetCommentsIds(postId);

        foreach (String id in ids)
        {
            Comments.Delete(id);
        }

        String photoId = GetPhotoId(postId);
        
        String query = "DELETE FROM Posts WHERE Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", postId);

            int deleted = (int)cmd.ExecuteNonQuery();

            if ( deleted == 0)
            {
                throw new Exception("Post couldn't be deleted");
            }

        }catch( Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }

        if( photoId.Length > 0 ) Photo.Delete(photoId);
    }
    

}