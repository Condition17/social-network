using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.GetUsers();

        }

    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {

        DropDownList DDUsers = LVNotification.FindControl("DDUsers") as DropDownList;
        if (DDUsers == null) this.ShowErrorMessage("Invalid user.");

        TextBox t = LVNotification.FindControl("TBNotification") as TextBox;

        try
        {
            Notification.Create(DDUsers.SelectedValue, t.Text);

        }catch( Exception ex)
        {
            this.ShowErrorMessage("An error occured while trying to send notification: " + ex.Message);
        }

        this.ShowSuccessMessage("Notification successfully sent");
    }

    private void GetUsers()
    {
        // populate dropdown list
        DropDownList DDUsers = LVNotification.FindControl("DDUsers") as DropDownList;
        if (DDUsers == null) return;

        DDUsers.Items.Clear();

        var users = Membership.GetAllUsers();
        var userList = new List<UserProfile>();

        foreach (MembershipUser user in users)
        {

            String userId = user.ProviderUserKey.ToString();
            String username = Utils.GetUsername(userId);

            if (Roles.IsUserInRole(username, "Admin"))
            {
                continue;
            }

            UserProfile profile = new UserProfile(userId, Profile.GetProfile(username) );
            ListItem listItem = new ListItem();

            listItem.Text = profile.FirstName + " " + profile.LastName;
            listItem.Value = userId;

            DDUsers.Items.Add(listItem);

        }

    }

    private void ShowErrorMessage(String message)
    {
        Label LBError = LVNotification.FindControl("LBError") as Label;
        Label LBSuccess = LVNotification.FindControl("LBSuccess") as Label;

        LBError.Text = message;
        LBSuccess.Visible = false;
        LBError.Visible = true;
    }

    private void ShowSuccessMessage(String message)
    {
        Label LBError = LVNotification.FindControl("LBError") as Label;
        Label LBSuccess = LVNotification.FindControl("LBSuccess") as Label;

        LBSuccess.Text = message;
        LBError.Visible = false;
        LBSuccess.Visible = true;
    }

}