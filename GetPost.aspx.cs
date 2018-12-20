using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class GetPost : System.Web.UI.Page
{
    String POST_AUTHOR = "";

    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (Request.Params["Id"] == null || Request.Params["Id"].Length == 0)
        {
            this.ShowErrorMessage("Invalid post id");
            return;
        }
        else {

            String postId = Request.Params["Id"];

            this.GetPostInfo(postId);
            this.GetComments(postId);

            postContainer.Visible = true;

        }
    }

    private void ShowErrorMessage(String message)
    {
        LBError.Text = message;
        LBSuccess.Visible = false;
        LBError.Visible = true;
        postContainer.Visible = false;
        comments_container.Visible = false;


        Control commentsInput = LVComments.FindControl("commentsInput") as Control;

        if (commentsInput != null ) commentsInput.Visible = false;

    }

    private void ShowSuccessMessage(String message)
    {
        LBSuccess.Text = message;
        LBError.Visible = false;
        LBSuccess.Visible = true;
    }

    private void GetPostInfo( String postId)
    {
        String query = "SELECT Posts.Id, Posts.AuthorID, Posts.Text, Posts.Timestamp,  Photos.URL " +
                    "FROM Posts left join Photos on Posts.PhotoID = Photos.Id " +
                    "where Posts.Id = @postId ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");
        
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("postId", postId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                this.ShowErrorMessage("There's no post with this id");
                return;
            }
            else
            {
                UserProfile profileData = null;
                String text = "", photoURL = "", authorID = null;
                DateTime timestamp = new DateTime();

                while (reader.Read())
                {
                    
                    if( profileData == null)
                    {
                        authorID = reader["AuthorID"].ToString();

                        if( POST_AUTHOR.Length == 0)
                        {
                            POST_AUTHOR = authorID;
                        }

                        String username = Utils.GetUsername(authorID);

                        ProfileCommon profile = Profile.GetProfile(username);
                        profileData = new UserProfile(authorID, profile);
                    }

                    text = reader["Text"].ToString();
                    timestamp = DateTime.Parse(reader["Timestamp"].ToString());
                    photoURL = reader["URL"].ToString();
                }

                LBDate.Text = timestamp.ToString("dd MMMM yyyy HH:mm");
                LBText.Text = text;
                if (photoURL.Length > 0)
                {
                    IMPost.ImageUrl = "/Images/" + photoURL;

                }
                else
                {
                    IMPost.Visible = false;
                }

                this.WriteProfileInfo( authorID, profileData);

            }

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to get post infos: "+ex.Message);
            postContainer.Visible = false;
            comments_container.Visible = false;
            return;
        }
        finally
        {
            con.Close();
        }

        //Show delete post button for admins and user who posted 
        if( String.Equals(POST_AUTHOR, Utils.GetCurrentUserUid() ) || User.IsInRole("Admin"))
        {
            BtnDelPost.Visible = true;
        }

        if(String.Equals(POST_AUTHOR, Utils.GetCurrentUserUid()))
        {
            BtnEditPost.Visible = true;
        }

    }

    private void WriteProfileInfo( String uid, UserProfile profile)
    {
        HypProfile.Text = profile.FirstName + " " + profile.LastName;
        HypProfile.NavigateUrl = "/Wall.aspx?id=" + uid;
        IMProfile.ImageUrl = "/Images/" + profile.ProfilePhoto;
    }

    private void GetComments(String postId)
    {

        String query = "SELECT Id, UserID, Text,  Timestamp " +
                    "FROM Comments " +
                    "where PostID = @postId ";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        String currentUserId = Utils.GetCurrentUserUid();
        Boolean userIsAdmin = User.IsInRole("Admin");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("postId", postId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String text = reader["Text"].ToString();
                String authorId = reader["UserID"].ToString();
                String commentId = reader["Id"].ToString();
                DateTime timestamp = DateTime.Parse(reader["Timestamp"].ToString());
                String username = Utils.GetUsername(authorId);

                ProfileCommon profile = Profile.GetProfile(username);

                UserProfile userProfile = new UserProfile( authorId, profile);

                HtmlGenericControl comment = new HtmlGenericControl("DIV");
                comment.Attributes["class"] = "comment";
                
                HtmlImage img = new HtmlImage();
                img.Width = 50;
                img.Height = 50;
                img.Src = "/Images/" + userProfile.ProfilePhoto;
                img.Attributes["class"] = "comment-img";

                comment.Controls.Add(img);

                HtmlAnchor authorUrl = new HtmlAnchor();
                authorUrl.Attributes["class"] = "comment-author";
                authorUrl.HRef = "/Wall.aspx?id=" + authorId;
                authorUrl.InnerText = userProfile.FirstName + " " + userProfile.LastName;

                comment.Controls.Add(authorUrl);

                HtmlGenericControl textContent = new HtmlGenericControl("DIV");

                textContent.InnerHtml = text;
                textContent.Attributes["class"] = "comment-text";
                comment.Controls.Add(textContent);

                HtmlGenericControl date = new HtmlGenericControl("div");
                date.InnerHtml = timestamp.ToString("dd MMMM yyyy HH:mm");
                date.Attributes["class"] = "comment-date";
                comment.Controls.Add(date);

                if( userIsAdmin || String.Equals(currentUserId, authorId) || String.Equals(currentUserId, POST_AUTHOR ) )
                {
                    LinkButton deleteBtn = new LinkButton();
                    deleteBtn.Command += new CommandEventHandler(DeleteBtn_Click);
                    deleteBtn.CommandArgument = commentId;
                    deleteBtn.Text = "Delete";
                    comment.Controls.Add(deleteBtn);
                }
                

                comments_container.Controls.Add(comment);

            }

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to get the comments: " + ex.Message);
            return;
        }

    }


    protected void DeleteBtn_Click(object sender, CommandEventArgs e)
    {
        String commentId = e.CommandArgument.ToString();

        try
        {
            Comments.Delete(commentId);
        }
        catch (Exception ex)
        {
            ShowErrorMessage("An error occured when trying to delete comment: " + ex.Message);
        }
        Response.Redirect(Request.RawUrl);

    }

    protected void CommentBtn_Click(object sender, EventArgs e)
    {
        String postId = Request.Params["Id"];
        String authorId = Utils.GetCurrentUserUid();

        TextBox TBComment = LVComments.FindControl("TBComment") as TextBox;

        String comment = TBComment.Text.Trim();

        if( comment.Length == 0)
        {
            this.ShowErrorMessage("The comment couldn't be blank. Please insert some text");
            return;
        }

        try
        {

            Comments.Create(authorId, postId, comment);
            Response.Redirect(Request.RawUrl);

        }catch( Exception ex)
        {
            this.ShowErrorMessage("An exception occured while trying to comment: " + ex.Message);
            return;
        }

    }

    protected void BtnDelPost_Click(object sender, EventArgs e)
    {
        String postId = Request.Params["id"];

        if( postId == null || postId.Length == 0)
        {
            this.ShowErrorMessage("Invalid post id");
        }

        try
        {
            Post.Delete( postId );

        }catch(Exception ex)
        {
            this.ShowErrorMessage("An exception occured while trying to delete the post: " + ex.Message);
            return;
        }

        Response.Redirect("/Wall.aspx?id=" + POST_AUTHOR);

    }

    protected void BtnEditPost_Click(object sender, EventArgs e)
    {
        Response.Redirect("/EditPost.aspx?id="+Request.Params["Id"]);
    }
}