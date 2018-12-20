using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditPost : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if( Request.Params["id"] == null || Request.Params["id"].Length == 0)
        {
            Response.Redirect("/Index.aspx");
        }

        if( !Utils.userIsAuthenticated() )
        {
            Response.Redirect("/Index.aspx");
        }

        String postId = Request.Params["id"];

        String currentUserId = Utils.GetCurrentUserUid();

        if ( !Post.HasAuthor( postId, currentUserId ) )
        {
            Response.Redirect("/Index.aspx");
        }

        if( !Page.IsPostBack)
        {
            this.GetPost(postId);
        }

    }

    private void GetPost(String postId)
    {
        String query = "Select Text, PhotoID FROM Posts where Id = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");
        String postContent = "";

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", postId);

            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                postContent = reader["Text"].ToString();
                String photoID = reader["PhotoID"].ToString();

                Boolean hasPhoto = !String.Equals(photoID, "");

                if( hasPhoto )
                {
                    LBHasImage.Visible = true;
                }

            }

        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "An error occured while trying to retrieve post data: " + ex.Message;
            return;
        }
        finally
        {
            con.Close();
        }

        TBDescription.Text = postContent;

    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        String postId = Request.Params["id"];
        String description = TBDescription.Text.Trim();

        Debug.Write(description);

        if ( !Post.HasPhoto(postId) && String.Equals( description, "" ) )
        {
            LBError.Visible = true;
            LBError.Text = "This post doesn't have an image, so the description couldn't be blank.";
            return;
        }

        try
        {
            Post.Update(postId, description);

        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "An error occured while trying to update post: " + ex.Message;
            return;
        }

        String currentUserId = Utils.GetCurrentUserUid();
        Response.Redirect("/Wall.aspx?id=" + currentUserId);

    }
}