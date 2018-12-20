using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Page.IsPostBack && Request.Params["id"] != null)
        {
            List<UserProfile> results = new List<UserProfile>();
            String userId = Request.Params["id"];

            GridView1.DataSource = results;
           
            String query = "SELECT UserID1, UserID2"
                            + " FROM Friends"
                            + " WHERE UserID1 = @id or UserID2 = @id";

            SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String uid = reader["UserID1"].ToString().Equals(userId) ? reader["UserID2"].ToString() : reader["UserID1"].ToString();
                    String userName = Utils.GetUsername(uid);

                    ProfileCommon profile = Profile.GetProfile(userName);
                    UserProfile user = null;

                    if (profile != null)
                    {
                        user = new UserProfile(uid, profile);
                    }
                    results.Add(user);
                }

                nrRezultate.Text = results.Count.ToString() + " prieteni";
            }
            catch (Exception ex)
            {
                LBError.Text = "Error encountered durring this operation: " + ex.Message;
            }
            finally
            {
                con.Close();
            }

            GridView1.DataBind();
        }
        else
        {
            LBError.Text = "Invalid request";
        }
        
       
    }
}