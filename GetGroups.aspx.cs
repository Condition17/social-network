using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utils.userIsAuthenticated()) return;

        String currentUserId = Utils.GetCurrentUserUid();

        String query = "SELECT Groups.Id, Groups.Name " +
                       "FROM Profiles_Groups join Groups on Groups.Id = Profiles_Groups.GroupID " +
                       "WHERE Profiles_Groups.UserID = @id";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("id", currentUserId);
            SqlDataReader reader = cmd.ExecuteReader();

            GridView GridView1 = LVGroups.FindControl("GridView1") as GridView;

            GridView1.DataSource = reader;
            GridView1.DataBind();

        }
        catch( Exception ex)
        {
            Label LBError = LVGroups.FindControl("LBError") as Label;
            LBError.Visible = true;
            LBError.Text = "An error occured while trying to get the groups: " + ex.Message;
        }
        finally
        {
            con.Close();
        }

        this.ShowContainer();
    }

    private void ShowContainer()
    {
        Control Container = LVGroups.FindControl("Container");
        Container.Visible = true;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewGroup.aspx");
    }

    protected void BtnJoin_Click(object sender, EventArgs e)
    {
        Response.Redirect("/JoinGroup.aspx");
    }
}