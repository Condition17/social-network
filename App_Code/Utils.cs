using System; 
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{

    //public static getUserBy

    public static String GetCurrentUserUid() {

        MembershipUser user = Membership.GetUser();
        if ( user != null )
        {
            return user.ProviderUserKey.ToString();
        }
        else
        {
            return "";
        }
                
    }

    public static Boolean BannedUser( String uid)
    {
        String username = GetUsername(uid);
        MembershipUser user = Membership.GetUser(username);

        return !user.IsApproved;

    }

    public static String GetUsername( String uid)
    {
        try
        {
            Guid uuid = new Guid(uid);
            MembershipUser member = Membership.GetUser(uuid);

            if (member == null)
            {
                return null;
            }

            return member.UserName;
        } catch( Exception ex)
        {
            return "";
        }
        
    }

    public static Boolean userIsAuthenticated()
    {
        return (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
    }

    public static void AddPhotoToAlbum( int idPhoto, int idAlbum) {

        if ( idPhoto == 0)
        {
            throw new Exception("Invalid photo id");
        }

        if ( idAlbum == 0 )
        {
            throw new Exception("Invalid album id.");
        }

        string query = "INSERT INTO Albums ( AlbumID, PhotoID) OUTPUT INSERTED.AlbumID"
                    + " VALUES (@albumId, @photoId)";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("albumId", idAlbum);
            cmd.Parameters.AddWithValue("photoId", idPhoto);

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