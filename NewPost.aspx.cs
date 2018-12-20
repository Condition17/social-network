using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Utils.userIsAuthenticated()) return;

        String currentUserId = Utils.GetCurrentUserUid();
       
        this.GetAlbums(currentUserId);
        this.ShowContainer();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        FileUpload FileUpload1 = LVPost.FindControl("FileUpload1") as FileUpload;
        RadioButtonList RBList = LVPost.FindControl("RBList") as RadioButtonList;
        TextBox TBDescription = LVPost.FindControl("TBDescription") as TextBox;

        String currentUserId = Utils.GetCurrentUserUid();
        string photoName = null;

        if ( FileUpload1.HasFile )
        {
            photoName = Path.GetFileName( FileUpload1.PostedFile.FileName );
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/") +  photoName );
        }

        string postText = TBDescription.Text.Trim();
        string albumType = null;

        if ( RBList.SelectedItem != null)
        {
            albumType= RBList.SelectedItem.Value;
        }
        
        if(  photoName == null && postText.Length == 0 )
        {
            this.ShowErrorMessage("The post is not complete. Add at least an image or some text to post something.");
            return;
        }
        
        if( photoName == null)
        {
            // just text post

            Post.Create(currentUserId, null, postText);
            this.ShowSuccessMessage("Successfully posted");
            return;
        }
        else
        {
            if( albumType == null)
            {
                ShowErrorMessage("Chose an album type and name for your photo.");
                return;
            }
            
        }



        // photo post with( or not) text

        DropDownList DropDownList1 = LVPost.FindControl("DropDownList1") as DropDownList;
        TextBox TBAlbumName = LVPost.FindControl("TBAlbumName") as TextBox;

        try
        {
            String albumId = null;

            if (albumType.Equals("0"))
            {
                // new album
                if (TBAlbumName.Text.Length == 0)
                {
                    this.ShowErrorMessage( "Please enter an album name." );
                    return;
                }

                String albumName = TBAlbumName.Text.Trim();
                if (AlbumModel.Exists(currentUserId, albumName))
                {
                    this.ShowErrorMessage("Album already exists in your albums collection. Please chose another name");
                    return;
                }
   
                // create new album
                albumId = AlbumModel.Create(currentUserId, albumName).ToString();
                
            }
            else
            {

                String albumName = DropDownList1.SelectedItem.Text;
                albumId = DropDownList1.SelectedItem.Value;

                if ( !AlbumModel.Exists(currentUserId, albumName) )
                {
                    this.ShowErrorMessage("Selected album name doesn't exist in your albums list.");
                    return;
                }

            }

            String photoId = Photo.Create(photoName).ToString();

            AlbumModel.AddPhoto(albumId, photoId);
            Post.Create(currentUserId, photoId, postText);

            this.ShowSuccessMessage("Successfully posted");

        }catch( Exception ex)
        {
            this.ShowErrorMessage( "An error has occured while trying to add this post: " + ex.Message );
        }

    }

    private void GetAlbums( String userId)
    {
        DropDownList DropDownList1 = LVPost.FindControl("DropDownList1") as DropDownList;
        DropDownList1.Items.Clear();

        SqlDataReader reader = null;

        String query = "SELECT Name, Id FROM"
                        + " Albums"
                        + " WHERE UserID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", userId);
            reader = cmd.ExecuteReader();

            if( !reader.HasRows )
            {
                DropDownList1.Visible = false;
            }
            else
            {
                DropDownList1.Visible = true;
                while (reader.Read())
                {
                    ListItem listItem = new ListItem();

                    listItem.Text = reader["Name"].ToString();
                    listItem.Value = reader["Id"].ToString();

                    DropDownList1.Items.Add(listItem);
                }
            }

        }
        catch (Exception _)
        {
        }
        finally
        {
            con.Close();
        }


    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVPost.FindControl("LBError") as Label;
        Label LBSuccess = LVPost.FindControl("LBSuccess") as Label;

        LBError.Text = message;
        LBSuccess.Visible = false;
        LBError.Visible = true;

    }

    private void ShowSuccessMessage( String message)
    {
        Label LBError = LVPost.FindControl("LBError") as Label;
        Label LBSuccess = LVPost.FindControl("LBSuccess") as Label;

        LBSuccess.Text = message;
        LBError.Visible = false;
        LBSuccess.Visible = true;
    }

    private void ShowContainer()
    {
        Control Container = LVPost.FindControl("Container");
        Container.Visible = true;

    }
}