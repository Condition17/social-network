using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Wall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if( !Page.IsPostBack && Request.Params["Id"] != null)
        {
            String userId = Request.Params["Id"];

            if( userId.Length == 0 || Utils.GetUsername(userId) == null)
            {
                LBError.Text = "Invalid user id";
            }

            String query = "SELECT Posts.Id, Posts.Text, Posts.Timestamp,  Photos.URL " +
                    "FROM Posts left join Photos on Posts.PhotoID = Photos.Id " +
                    "where Posts.AuthorID = @userId " +
                    "order by Posts.Timestamp desc";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String id = reader["Id"].ToString();
                    String text = reader["Text"].ToString();
                    DateTime timestamp = DateTime.Parse(reader["Timestamp"].ToString());
                    String photoURL = reader["URL"].ToString();

                    HtmlGenericControl wallPost = new HtmlGenericControl("DIV");
                    wallPost.Attributes["class"] = "wall-post";

                    if (!String.Equals(text, ""))
                    {
                        HtmlGenericControl textContent = new HtmlGenericControl("DIV");
                        textContent.InnerHtml = text;
                        textContent.Attributes["class"] = "post-text";
                        wallPost.Controls.Add(textContent);
                    }

                    if (!String.Equals(photoURL, ""))
                    {
                        HtmlImage img = new HtmlImage();
                        img.Src = "/Images/" + photoURL;
                        img.Attributes["class"] = "wall-img";
                        wallPost.Controls.Add(img);

                    }

                    HtmlGenericControl date = new HtmlGenericControl("div");
                    date.InnerHtml = timestamp.ToString("dd MMMM yyyy HH:mm");
                    date.Attributes["class"] = "post-date";
                    wallPost.Controls.Add(date);

                    HtmlGenericControl seeMore = new HtmlGenericControl("div");
                    seeMore.Attributes["class"] = "seeMore-btn";

                    HtmlAnchor seeMoreBtn = new HtmlAnchor();
                    seeMoreBtn.HRef = "/GetPost.aspx?id=" + id;
                    seeMoreBtn.InnerText = "See More";

                    seeMore.Controls.Add(seeMoreBtn);

                    wallPost.Controls.Add(seeMore);

                    posts_container.Controls.Add(wallPost);

                }


            }
            catch (Exception ex)
            {
                LBError.Visible = true;
                LBError.Text = "An error occured while retrieving profile posts: " + ex.Message;
            }
            finally
            {

                con.Close();
            }

        }
    }
        
}