using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Params["Id"] != null)
        {
            String userId = Request.Params["Id"];

            if (userId.Length == 0 || Utils.GetUsername(userId) == null)
            {
                LBError.Text = "Invalid user id";
            }

            String query = "SELECT * FROM ALBUMS " +
                           "WHERE UserID = @userId";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");
            
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                GridView1.DataSource = reader;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                LBError.Visible = true;
                LBError.Text = "An unexpected error occured while trying to get the albums for this user:" + ex.Message;
            }

            Boolean isAuthorizedUser = Utils.userIsAuthenticated() && (User.IsInRole("Admin") || String.Equals(Utils.GetCurrentUserUid(), userId));

            if (isAuthorizedUser)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton btn = row.FindControl("DelBtn") as LinkButton;
                        btn.Visible = true;
                    }
                }
            } 

        }
    }

    protected void DelBtn_Click1(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String albumId = btn.CommandArgument.ToString();
        try
        {
            AlbumModel.Delete(albumId);
        }
        catch (Exception ex)
        {
            LBError.Visible = true;
            LBError.Text = "Am error occured while trying to delete album: " + ex.Message;
        }

        Response.Redirect(Request.RawUrl);
    }
}