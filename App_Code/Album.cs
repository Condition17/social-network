using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Album
/// </summary>
public class Album
{
    public Album()
    {
    }

    public static int Create( String userID, String name )
    {
        if( name == null || name.Length == 0)
        {
            throw new Exception("Invalid album name");
        }

        if( userID == null || userID.Length == 0)
        {
            throw new Exception("Invalid userID");
        }

        string query = "INSERT INTO Albums ( UserId, Name) OUTPUT INSERTED.Id"
                    + " VALUES (@userId, @name)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("userId", userID);
            cmd.Parameters.AddWithValue("name", name);

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

    public static Boolean Exists( String userID, String albumName)
    {
        String query = "SELECT count(*) "
                        + " FROM Albums"
                        + " WHERE (UserID = @id and Name = @name)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userID);
            cmd.Parameters.AddWithValue("name", albumName);

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {

        }
        finally
        {
            con.Close();
        }

        return exists;
    }

    public static void AddPhoto( String albumId, String photoId)
    {
        if (albumId == null || albumId.Length == 0)
        {
            throw new Exception("Invalid album name");
        }

        if (photoId == null || albumId.Length == 0)
        {
            throw new Exception("Invalid userID");
        }

        string query = "INSERT INTO Photos_Albums ( AlbumID, PhotoID) OUTPUT INSERTED.AlbumID"
                    + " VALUES (@albumId, @photoId)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("albumId", albumId);
            cmd.Parameters.AddWithValue("photoId", photoId);

            int id = (int)cmd.ExecuteScalar();

            if (id == 0)
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

}