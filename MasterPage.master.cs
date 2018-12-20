using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if( Utils.GetCurrentUserUid().Length > 0)
        {
            String userId = Utils.GetCurrentUserUid();
            String username = Utils.GetUsername( userId );
            HyperLink link = LV.FindControl("UserNameLink") as HyperLink;
            if( link != null )
            {
                link.NavigateUrl = "/Wall.aspx?id=" + userId;
                link.Text = username;
            }
            
        }
    }

    protected void BSearch_Click(object sender, EventArgs e)
    {
        

        if ( TBSearch.Text.Length > 0 )
        {
            Response.Redirect("UserSearch.aspx?user=" + Server.UrlEncode(TBSearch.Text));
        }

    }


    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }

}
