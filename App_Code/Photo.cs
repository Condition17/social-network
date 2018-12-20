using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Photo
/// </summary>
public class Photo
{
    public Photo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int Create(String url)
    {
        if (url == null || url.Length == 0) {
            throw new Exception("Invalid photo url.");
        }

        string query = "INSERT INTO Photos ( URL ) OUTPUT INSERTED.Id"
                    + " VALUES (@url)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("url", url);

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

    public static void DeleteFromAlbum( String photoId)
    {

        if (photoId == null || photoId.Length == 0)
        {
            throw new Exception("Invalid post id");
        }

        String query = "DELETE FROM Photos_Albums" +
            " WHERE PhotoID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", photoId);

            cmd.ExecuteNonQuery();

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

    public static void Delete(String id)
    {

        if (id == null || id.Length == 0)
        {
            throw new Exception("Invalid photo id");
        }

        DeleteFromAlbum(id);

        String query = "DELETE FROM Photos " +
                        "WHERE Id = @id ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();

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