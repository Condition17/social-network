using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Groups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String query = "SELECT * from Groups";

        SqlConnection con = new SqlConnection(@"Data Source=GHGRLH2\SQLEXPRESS;Initial Catalog=aspnetdb;Integrated Security=True");

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if( !reader.HasRows)
            {
                Label l = LVGroups.FindControl("LBNoGroup") as Label;
                l.Visible = true;

            }

            GridView grid = LVGroups.FindControl("GridView1") as GridView;

            if (grid != null)
            {
                grid.DataSource = reader;
                grid.DataBind();
            }

        }catch( Exception ex)
        {
            Label LBError = LVGroups.FindControl("LBError") as Label;

            if( LBError != null)
            {
                LBError.Visible = true;
                LBError.Text = "An error occured while retrieving the groups: " + ex.Message;
            }

        }

    }

    protected void DelBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String groupId = btn.CommandArgument;

        try
        {
            Group.Delete(groupId);

        }catch( Exception ex)
        {

            Label LBError = LVGroups.FindControl("LBError") as Label;

            if (LBError != null)
            {
                LBError.Visible = true;
                LBError.Text = "An error occured while retrieving the groups: " + ex.Message;
            }

        }

        Response.Redirect(Request.RawUrl);

    }

}
