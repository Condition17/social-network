using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Group
/// </summary>
public class Group
{
    public Group()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Boolean ExistsByName( String groupName)
    {
        if (groupName == null || groupName.Length == 0)
        {
            throw new Exception("Invalid group name");
        }

        String query = "SELECT count(*) "
                        + " FROM Groups"
                        + " WHERE Name = @name";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", groupName);

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {
            return false;
        }
        finally
        {
            con.Close();
        }

        return exists;

    }

    public static Boolean HasMember( String groupId, String userId)
    {
        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        if (userId == null || userId.Length == 0)
        {
            throw new Exception("Invalid user id");
        }

        String query = "SELECT count(*) "
                        + " FROM Profiles_Groups"
                        + " WHERE UserID = @userId and GroupID = @groupId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("groupId", groupId);

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {
            return false;
        }
        finally
        {
            con.Close();
        }

        return exists;

    }
    public static Boolean ExistsById(String groupId)
    {
        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        String query = "SELECT count(*) "
                        + " FROM Groups"
                        + " WHERE Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean exists = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", groupId );

            exists = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {
            return false;
        }
        finally
        {
            con.Close();
        }

        return exists;

    }


    public static int Create( String groupName )
    {

        if( groupName == null || groupName.Length == 0)
        {
            throw new Exception("Invalid group name");
        }

        if( ExistsByName( groupName) )
        {
            throw new Exception("Group already exists");
        }



        string query = "INSERT INTO Groups ( Name ) OUTPUT INSERTED.Id"
                    + " VALUES ( @name )";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("name", groupName);

            int id = (int)cmd.ExecuteScalar();

            if (id == 0)
            {
                throw new Exception("Couldn't create group. Try again!");
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

    public static void RemoveUser( String userId, String groupId)
    {

        if (userId == null || userId.Length == 0)
        {
            throw new Exception("Invalid user id");
        }

        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        String query = "DELETE FROM Profiles_Groups " +
                       "WHERE UserID = @userId and GroupID = @groupId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("groupId", groupId);

            int deletedRows = cmd.ExecuteNonQuery();

            if (deletedRows == 0)
            {
                new Exception("User couldn't be removed from group. Please make sure this user is in the group.");
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

    public static List<String> GetUserIds( String groupId)
    {
        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        List<String> userIds = new List<String>();

        String query = "SELECT AuthorID FROM Messages_Groups WHERE GroupID = @groupId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupId", groupId);
            SqlDataReader reader = cmd.ExecuteReader();

            while( reader.Read())
            {
                userIds.Add( reader["AuthorID"].ToString() );
            }

        }
        catch( Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            con.Close();
        }

        return userIds;
    }

    public static void Delete( String groupId)
    {

        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        if (!ExistsById(groupId))
        {
            throw new Exception("Group doesn't exist");
        }

        List<String> removableMessageIds = Messages.GetIds(groupId);
        List<String> removableUsersIds = GetUserIds(groupId);

        String query = "DELETE FROM Groups " +
                       "WHERE Id = @groupId";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupId", groupId);

            int deletedRows = cmd.ExecuteNonQuery();

            if (deletedRows == 0)
            {
                new Exception("Group couldn't be removed");
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

        foreach( String id in removableMessageIds)
        {
            Messages.Delete(id);
        }

        foreach (String id in removableUsersIds)
        {
            RemoveUser(id, groupId);
        }

    }


    public static Boolean HasUsers( String groupId)
    {

        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        String query = "SELECT count(*) "
                        + " FROM Profiles_Groups"
                        + " WHERE GroupID = @groupID";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean hasUsers = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("groupID", groupId);

            hasUsers = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {
           
        }
        finally
        {
            con.Close();
        }

        return hasUsers;

    }

    public static void AddUser(String userId, String groupId)
    {
        if (userId == null || userId.Length == 0)
        {
            throw new Exception("Invalid user id");
        }

        if (groupId == null || groupId.Length == 0)
        {
            throw new Exception("Invalid group id");
        }

        if (!ExistsById(groupId))
        {
            throw new Exception("Group doesn't exist");
        }

        String query = "INSERT INTO Profiles_Groups ( UserID, GroupID ) " +
                       "VALUES ( @userId, @groupId ) ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("groupId", groupId);

            int insertedRows = cmd.ExecuteNonQuery();

            if (insertedRows == 0)
            {
                new Exception("User not added to group");
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