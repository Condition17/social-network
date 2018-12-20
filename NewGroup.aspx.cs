using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utils.userIsAuthenticated()) return;
        this.ShowContainer();
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {

        String currentUserId = Utils.GetCurrentUserUid();
        TextBox TBName = LVGroup.FindControl("TBName") as TextBox;

        try
        {

            String groupName = TBName.Text.Trim();
            String groupId = Group.Create(groupName).ToString();

            Group.AddUser(currentUserId, groupId);

            Response.Redirect("/GetGroups.aspx");
            
        }catch( Exception ex)
        {
            Label LBError = LVGroup.FindControl("LBError") as Label;
            LBError.Visible = true;
            LBError.Text = "An error occured while creating group: " + ex.Message;
        }
    }

    private void ShowContainer()
    {
        Control Container = LVGroup.FindControl("Container");
        Container.Visible = true;

    }
}