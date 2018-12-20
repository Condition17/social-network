using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class MasterPage2 : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if ( !Page.IsPostBack && Request.Params["id"] != null)
        {
            String userId = Request.Params["id"];

            try
            {
                
                if ( Utils.GetUsername(userId).Length == 0 )
                {
                    throw new Exception("This profile doesn't exist.");
                }

                String userName = Utils.GetUsername(userId);


                Boolean isAuthorizedUser = String.Equals(Utils.GetCurrentUserUid(), userId) || HttpContext.Current.User.IsInRole("Admin");

                ProfileCommon profile = Profile.GetProfile(userName);
                UserProfile user = null;

                if (profile == null)
                {
                    throw new Exception("This profile doesn't exist");
                }
                else
                {
              
                    user = new UserProfile(userId, profile);

                    PopulateProfile(user);

                    //if( Utils.BannedUser(userId))
                    //{
                    //    LBPrivateProfile.Visible = true;
                    //    LBPrivateProfile.Text = "This profile is currently blocked.";
                    //    return;
                    //}

                    if (String.Equals(user.Type, "private"))
                    {
                        if( !isAuthorizedUser ) LBPrivateProfile.Visible = true;
                        ChildContent2.Visible = false;
                    }

                    if ( isAuthorizedUser || String.Equals(user.Type,"public") )
                    {
                        ConfigureHyperLinks(userId);
                        ChildContent2.Visible = true;
                    }

                    ConfigureFriendshipData(userId);

                    if ( isAuthorizedUser )
                    {
                        BtnUpdateInfo.Visible = true;
                    }
                }

            } catch( Exception ex)
            {
                LBError.Visible = true;
                LBError.Text = ex.Message;
                Container.Visible = false;
                return;
            }

        }

        Container.Visible = true;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if ( Request.Params["id"] != null)
        {
            Response.Redirect("/EditProfile.aspx");
        }
    }

    private void PopulateProfile( UserProfile user)
    {
        LBDespre.Text = "About " + user.FirstName + ":";
        Cover.ImageUrl = "/Images/" + user.CoverPhoto;
        ProfilePhoto.ImageUrl = "/Images/" + user.ProfilePhoto;
        LBName.Text = user.FirstName + " " + user.LastName;

        if( user.Type != null && user.Type.Length > 0)
        {
            TagType.Visible = true;
            LBType.Visible = true;
            LBType.Text = char.ToUpper(user.Type[0]) + user.Type.Substring(1);
        }

        if( user.City != null && user.City.Length > 0)
        {
            TagCity.Visible = true;
            LBCity.Visible = true;
            LBCity.Text = user.City;
        }

        if (user.Country != null && user.Country.Length > 0)
        {
            TagCountry.Visible = true;
            LBCountry.Visible = true;
            LBCountry.Text = user.Country;
        }

        if (user.School != null && user.School.Length > 0)
        {
            TagSchool.Visible = true;
            LBSchool.Visible = true;
            LBSchool.Text = user.School;
        }

        if (user.Work != null && user.Work.Length > 0)
        {
            TagWork.Visible = true;
            LBWork.Visible = true;
            LBWork.Text = user.Work;
        }

        if (user.Birthday != null )
        {
            TagBirthday.Visible = true;
            LBBirthday.Visible = true;
            LBBirthday.Text = user.Birthday.ToString("dd MMMM yyyy");
        }

        TagGender.Visible = true;
        LBGender.Visible = true;
        LBGender.Text = user.Gender == 1 ? "Male" : "Female";
       
        if( user.RelationshipStatus != null && user.RelationshipStatus.Length > 0)
        {
            TagRelationshipStatus.Visible = true;
            LBRelationshipStatus.Visible = true;
            LBRelationshipStatus.Text = user.RelationshipStatus;
        }
    }

    private void ConfigureHyperLinks( String userId )
    {

        WallHypLink.NavigateUrl = "/Wall.aspx?id=" + userId;
        PhotosHypLink.NavigateUrl = "/Photos.aspx?id=" + userId;
        FriendsHypLink.NavigateUrl = "/Friends.aspx?id=" + userId;

        String currentUser = Utils.GetCurrentUserUid();

        if( String.Equals(userId, currentUser))
        {
            this.CreateMenuButton("New Post", "/NewPost.aspx");
            this.CreateMenuButton("New Album", "/NewAlbum.aspx");

            if( HttpContext.Current.User.IsInRole("Admin"))
            {
                this.CreateMenuButton("AdminPanel", "/Index.aspx");
            }

        }

    }

    private void CreateMenuButton( String text, String hyperlink)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("div");
        createDiv.Attributes["class"] = "menu-item";

        HyperLink hl = new HyperLink();
        hl.NavigateUrl = hyperlink;
        hl.Text = text;
        createDiv.Controls.Add(hl);
        menu_bar.Controls.Add(createDiv);

    }

    private void ConfigureFriendshipData(String userId)
    {
        String currentUserID = Utils.GetCurrentUserUid();

        if( String.Equals( currentUserID, userId ) || !Utils.userIsAuthenticated() )
        {
            return;
        }

        if( AreFriends( currentUserID, userId) )
        {

            ConfigureHyperLinks(userId);
            ChildContent2.Visible = true;
            LBPrivateProfile.Visible = false;

            Unfriend.Visible = true;
        }
        else if( FriendRequestSent( currentUserID, userId))
        {
            CancelRequest.Visible = true;
        }
        else
        {
            AddFriend.Visible = true;
        }


    }

    private Boolean AreFriends( String user1, String user2)
    {
        String query = "SELECT count(*) "
                        + " FROM Friends"
                        + " WHERE (UserID1 = @id1 and UserID2 = @id2) or (UserID1 = @id2 and UserID2 = @id1)";
        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean friends = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id1", user1);
            cmd.Parameters.AddWithValue("id2", user2);

            friends = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {

        }
        finally
        {
            con.Close();
        }

        return friends;
    }

    private Boolean FriendRequestSent(String sender, String receiver)
    {
        String query = "SELECT count(*) "
                        + " FROM FriendRequests"
                        + " WHERE SenderID = @id1 and ReceiverID = @id2";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        Boolean friendRequestSent = false;

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id1", sender);
            cmd.Parameters.AddWithValue("id2", receiver);

            friendRequestSent = (int)cmd.ExecuteScalar() != 0;

        }
        catch (Exception _)
        {

        }
        finally
        {
            con.Close();
        }
        return friendRequestSent;
    }

    protected void Unfriend_Click1(object sender, EventArgs e)
    {

        String idSender = Utils.GetCurrentUserUid();
        String profileUserID = Request.Params["id"];

        if (idSender == null || idSender.Length == 0)
        {
            ShowErrorMessage("Please make sure you are logged in.");
        }
        else
        {
            string query = "DELETE FROM Friends"
                    + " WHERE (UserID1 = @sender and UserID2 = @receiver) or (UserID1 = @receiver and UserID2 = @sender)";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("sender", idSender);
                cmd.Parameters.AddWithValue("receiver", profileUserID);

                int deleted = cmd.ExecuteNonQuery();

                if (deleted > 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occured while trying to finish the operation." + ex.Message);

            }
            finally
            {
                con.Close();
            }

        }
    }

    protected void CancelRequest_Click1(object sender, EventArgs e)
    {

        String idSender = Utils.GetCurrentUserUid();
        String profileUserID = Request.Params["id"];


        if (idSender == null || idSender.Length == 0)
        {
            ShowErrorMessage("Please make sure you are logged in.");
        }
        else
        {
            string query = "DELETE FROM FriendRequests"
                    + " WHERE SenderID = @sender and ReceiverID = @receiver";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("sender", idSender);
                cmd.Parameters.AddWithValue("receiver", profileUserID);

                int deleted = cmd.ExecuteNonQuery();

                if ( deleted > 0)
                {
                    ShowSuccessMessage("Friend request successfully canceled.");
                    CancelRequest.Visible = false;
                    AddFriend.Visible = true;

                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occured while trying to finish the operation." + ex.Message);

            }
            finally
            {
                con.Close();
            }

        }

    }

    protected void AddFriend_Click1(object sender, EventArgs e)
    {

        
        String idSender = Utils.GetCurrentUserUid();
        String profileUserID = Request.Params["id"];

        if (idSender == null || idSender.Length == 0)
        {
            ShowErrorMessage("Please make sure you are logged in.");
            return;
        }        
        else
        {
            string query = "INSERT INTO FriendRequests (SenderID,ReceiverID) OUTPUT INSERTED.SenderID"
                    + " VALUES (@sender, @receiver)";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("sender", idSender);
                cmd.Parameters.AddWithValue("receiver", profileUserID);

                Guid id = (Guid) cmd.ExecuteScalar();

                if (id != null )
                {
                    ShowSuccessMessage("Friend request successfully sent.");
                    CancelRequest.Visible = true;
                    AddFriend.Visible = false;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occured while trying to finish the operation."+ex.Message);

            }
            finally
            {
                con.Close();
            }

        }

        
    }

    private void ShowErrorMessage( String message )
    {
        LBError.Text = message;
        LBError.Visible = true;
    }

    private void ShowSuccessMessage( String message )
    {
        LBSuccess.Text = message;
        LBSuccess.Visible = true;
    }

    
}
