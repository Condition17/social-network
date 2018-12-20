using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !Utils.userIsAuthenticated()) return;

        Label LBAlbumName = LVAlbum.FindControl("LBAlbumName") as Label;
        Label LBNrPhotos = LVAlbum.FindControl("LBNrPhotos") as Label;
        Control photo_container = LVAlbum.FindControl("photo_container");

        if (Request.Params["id"] == null || Request.Params["id"].Length == 0)
        {
            this.ShowErrorMessage("Invalid album id");
            return;
        }
        else    
        {

            String albumId = Request.Params["id"];
            String currentUserId = Utils.GetCurrentUserUid();
            Boolean isAuthorizedUser = false;
            Boolean authorizationChecked = false;
            String albumName = null;

            if (!AlbumModel.Exists(albumId))
            {
                this.ShowErrorMessage("This album doesn't exist");
                return;
            }
                
            String query = " Select Albums.Name, Photos.Id, Photos.URL, Posts.Id'PostId' FROM " +
                           " Photos join Photos_Albums on Photos.Id = Photos_Albums.PhotoID " +
                           " join Albums on Photos_Albums.AlbumID = Albums.ID " +
                           " join Posts on Photos.Id = Posts.PhotoID " +
                           "where Photos_Albums.AlbumID = @albumId";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("albumId", albumId);

                SqlDataReader reader = cmd.ExecuteReader();

                int nrPhotos = 0;

                while (reader.Read())
                {
                    String url = reader["URL"].ToString();
                    String postId = reader["PostId"].ToString();
                    String photoId = reader["Id"].ToString();

                    if( albumName == null )
                    {
                        albumName = reader["Name"].ToString();
                        LBAlbumName.Text = albumName;
                    }

                    if( albumName != null && authorizationChecked == false )
                    {
                        isAuthorizedUser = User.IsInRole("Admin") || AlbumModel.Exists(currentUserId, albumName);
                        authorizationChecked = true;
                    }


                    HtmlGenericControl albumItem = new HtmlGenericControl("DIV");
                    albumItem.Attributes["class"] = "album-item";

                    HtmlImage img = new HtmlImage();
                    img.Src = "/Images/" + url;
                    img.Attributes["class"] = "album-img";

                    albumItem.Controls.Add(img);

                    if( authorizationChecked == true && isAuthorizedUser)
                    {
                        HtmlGenericControl buttonsContainer = new HtmlGenericControl("DIV");
                        buttonsContainer.Attributes["class"] = "album-btn-container";

                        LinkButton showPostBtn = new LinkButton();
                        showPostBtn.Command += new CommandEventHandler(ShowPostBtn_Click);
                        showPostBtn.CommandArgument = postId;
                        showPostBtn.Text = "Show post";
                        buttonsContainer.Controls.Add(showPostBtn);

                        LinkButton deletePhotoBtn = new LinkButton();
                        deletePhotoBtn.Command += new CommandEventHandler(DeletePhotoBtn_Click);
                        deletePhotoBtn.CommandArgument = photoId;
                        deletePhotoBtn.Text = "Delete photo";
                        buttonsContainer.Controls.Add(deletePhotoBtn);

                        LinkButton deletePostBtn = new LinkButton();
                        deletePostBtn.Command += new CommandEventHandler(DeleteBtn_Click);
                        deletePostBtn.CommandArgument = postId;
                        deletePostBtn.Text = "Delete entire post";
                        buttonsContainer.Controls.Add(deletePostBtn);

                        albumItem.Controls.Add(buttonsContainer);

                    }

                    photo_container.Controls.Add(albumItem);

                    nrPhotos += 1;
                        
                }

                LBNrPhotos.Text = "Photos: " + nrPhotos.ToString();
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("An error occured while trying to retrieve the photos from this album:" + ex.Message);
                return;
            }

        }

        this.ShowContainer();
    }

    protected void DeleteBtn_Click(object sender, CommandEventArgs e)
    {
        String postId = e.CommandArgument.ToString();

        try
        {
            Post.Delete(postId);

        }
        catch (Exception ex)
        {
            this.ShowErrorMessage("An exception occured while trying to delete the post: " + ex.Message);
            return;
        }

        Response.Redirect(Request.RawUrl);

    }

    protected void ShowPostBtn_Click(object sender, CommandEventArgs e)
    {
        String postId = e.CommandArgument.ToString();
        Response.Redirect("/GetPost.aspx?id=" + postId);

    }

    protected void DeletePhotoBtn_Click(object sender, CommandEventArgs e)
    {
        String photoId = e.CommandArgument.ToString();
        try
        {
            Post.DeletePhoto(photoId);
        }
        catch (Exception ex)
        {
            ShowErrorMessage("An error occured when trying to delete comment: " + ex.Message);
        }
        Response.Redirect(Request.RawUrl);

    }

    private void ShowErrorMessage( String message)
    {
       
        Label LBError = LVAlbum.FindControl("LBError") as Label;

        if (LBError == null) return;

        LBError.Visible = true;
        LBError.Text = message;

        Control container = LVAlbum.FindControl("photo_container");
        container.Visible = false;
    }

    private void ShowContainer()
    {

        Control container = LVAlbum.FindControl("photo_container");
        container.Visible = true; 
    }

}