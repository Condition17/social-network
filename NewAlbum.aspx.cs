using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label LBError = LVAlbum.FindControl("LBError") as Label;
        Label LBSuccess = LVAlbum.FindControl("LBSuccess") as Label;

        TextBox TBName = LVAlbum.FindControl("TBName") as TextBox;

        String currentUserId = Utils.GetCurrentUserUid();

        if( currentUserId == null || currentUserId.Length == 0)
        {
            LBError.Visible = true;
            LBError.Text = "You are not authorized to perform this operation";

        }
        else
        {
            String albumName = TBName.Text.Trim();

            if (AlbumModel.Exists(currentUserId, albumName) )
            {
                LBError.Visible = true;
                LBError.Text = "Album already exists in your albums collection. Please chose another name";
                return;
            }

            try
            {
                AlbumModel.Create(currentUserId, albumName).ToString();
            }
            catch ( Exception ex)
            {
                LBError.Visible = true;
                LBError.Text = "An error occured while trying to create the album: " + ex.Message;
            }

            LBSuccess.Visible = true;
            LBSuccess.Text = "Album successfully created.";
        }

    }
}