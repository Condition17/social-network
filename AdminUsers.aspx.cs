using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var users = Membership.GetAllUsers();
            var userList = new List<UserProfile>();

            foreach (MembershipUser user in users)
            {
                
                String userId = user.ProviderUserKey.ToString();
                String username = Utils.GetUsername(userId);

                if (Roles.IsUserInRole(username,"Admin"))
                {
                    continue;
                }

                ProfileCommon profile = Profile.GetProfile(username);
                userList.Add( new UserProfile(userId, profile) );

            }

            GridView grid = LVUsers.FindControl("GridView1") as GridView;

            if (grid != null)
            {
                grid.DataSource = userList;
                grid.DataBind();
            }

        }
        catch (Exception ex)
        {
            Label LBError = LVUsers.FindControl("LBError") as Label;

            if (LBError != null)
            {
                LBError.Visible = true;
                LBError.Text = "An error occured while retrieving the users: " + ex.Message;
            }

        }

    }

    protected void BanBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String userId = btn.CommandArgument;

        String username = Utils.GetUsername(userId);
        MembershipUser user = Membership.GetUser(username);
        user.IsApproved = false;
        Membership.UpdateUser(user);
        Response.Redirect(Request.RawUrl);

    }

    protected void UnbanBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        String userId = btn.CommandArgument;

        String username = Utils.GetUsername(userId);
        MembershipUser user = Membership.GetUser(username);
        user.IsApproved = true;
        Membership.UpdateUser(user);
        Response.Redirect(Request.RawUrl);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow row = e.Row;
            LinkButton BanBtn = row.FindControl("Ban") as LinkButton;
            LinkButton BtnUnban = row.FindControl("Unban") as LinkButton;

            if( !Utils.BannedUser(BanBtn.CommandArgument) )
            {
                BanBtn.Visible = true;
            }
            else
            {
                BtnUnban.Visible = true;
            }

        }


    }
}