using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        CreateUserWizard wizard;
        wizard = LVProfile.FindControl("CreateUserWizard1") as CreateUserWizard;

        Roles.AddUserToRole( wizard.UserName , "User");

    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {   
        const String MaleProfilePicture = "defaultProfilePictureMale.jpg";
        const String FemaleProfilePicture = "defaultProfilePictureFemale.jpg";

        const String CoverPhoto = "defaultCoverPhoto.jpg";

        TextBox t;
        RadioButtonList rbList;

        CreateUserWizard createUserWizard;
        createUserWizard = LVProfile.FindControl("CreateUserWizard1") as CreateUserWizard;

        WizardStep wizard;
        wizard = createUserWizard.FindControl("ProfileDataStepWizard") as WizardStep;

        t = wizard.FindControl("TBFirstName") as TextBox;
        Profile.FirstName = t.Text;

        t = wizard.FindControl("TBLastName") as TextBox;
        Profile.LastName = t.Text;

        t = wizard.FindControl("TBBirthday") as TextBox;
        Profile.Birthday = DateTime.Parse(t.Text);

        rbList = wizard.FindControl("RBLGender") as RadioButtonList;
        Profile.Gender = int.Parse(rbList.SelectedValue);

        if( Profile.Gender == 1)
        {
            Profile.ProfilePhoto = MaleProfilePicture;

        }
        else
        {
            Profile.ProfilePhoto = FemaleProfilePicture;
        }

        Profile.CoverPhoto = CoverPhoto;

        Profile.Type = "public";

    }

    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {

        Response.Redirect("Index.aspx");

    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, EventArgs e)
    {
        
    }


    protected void BtnGroups_Click(object sender, EventArgs e)
    {
        Response.Redirect("/AdminGroups.aspx");
    }

    protected void BtnUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("/AdminUsers.aspx");

    }
}