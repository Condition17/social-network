using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TextBox t;
            RadioButtonList r;
            DropDownList d;
            Image im;

            t = LVProfile.FindControl("TBFirstName") as TextBox;
            if (t != null) t.Text = Profile.FirstName;

            t = LVProfile.FindControl("TBLastName") as TextBox;
            if (t != null) t.Text = Profile.LastName;

            t = LVProfile.FindControl("TBBirthday") as TextBox;
            if (t != null) t.Text = Profile.Birthday.ToShortDateString();

            r = LVProfile.FindControl("RBLGender") as RadioButtonList;
            if (r != null) r.SelectedValue = Profile.Gender.ToString();

            t = LVProfile.FindControl("TBWork") as TextBox;
            if (t != null) t.Text = Profile.Work;

            t = LVProfile.FindControl("TBSchool") as TextBox;
            if (t != null) t.Text = Profile.School;

            t = LVProfile.FindControl("TBCity") as TextBox;
            if (t != null) t.Text = Profile.City;

            t = LVProfile.FindControl("TBCountry") as TextBox;
            if (t != null) t.Text = Profile.Country;

            d = LVProfile.FindControl("DDRelationship") as DropDownList;
            if (d != null) d.Items.FindByText(Profile.RelationshipStatus).Selected = true;

            d = LVProfile.FindControl("DDProfileType") as DropDownList;
            if (d != null) d.Items.FindByText(Profile.Type).Selected = true;

            im = LVProfile.FindControl("IMProfile") as Image;
            if (im != null) im.ImageUrl = "Images/" + Profile.ProfilePhoto;

            im = LVProfile.FindControl("IMCover") as Image;
            if (im != null) im.ImageUrl = "Images/" + Profile.CoverPhoto;

        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        TextBox t;
        RadioButtonList r;
        DropDownList d;
        FileUpload f;
        Literal err;

        err = LVProfile.FindControl("LBError") as Literal;

        try
        {
            t = LVProfile.FindControl("TBFirstName") as TextBox;
            if (t != null) Profile.FirstName = t.Text;

            t = LVProfile.FindControl("TBLastName") as TextBox;
            if (t != null) Profile.LastName = t.Text;

            t = LVProfile.FindControl("TBBirthday") as TextBox;
            if (t != null) Profile.Birthday = DateTime.Parse(t.Text);

            r = LVProfile.FindControl("RBLGender") as RadioButtonList;
            if (r != null) Profile.Gender = int.Parse(r.SelectedValue);

            t = LVProfile.FindControl("TBSchool") as TextBox;
            if (t != null) Profile.School = t.Text;

            t = LVProfile.FindControl("TBWork") as TextBox;
            if (t != null) Profile.Work = t.Text;

            t = LVProfile.FindControl("TBCity") as TextBox;
            if (t != null) Profile.City = t.Text;

            t = LVProfile.FindControl("TBCountry") as TextBox;
            if (t != null) Profile.Country = t.Text;

            d = LVProfile.FindControl("DDRelationship") as DropDownList;
            if (d != null) Profile.RelationshipStatus = d.SelectedItem.Text;

            d = LVProfile.FindControl("DDProfileType") as DropDownList;
            if (d != null) Profile.Type = d.SelectedItem.Text;

            f = LVProfile.FindControl("ProfileUpload") as FileUpload;

            if (f != null && f.HasFile)
            {
                String photoName = Path.GetFileName(f.PostedFile.FileName);
                f.PostedFile.SaveAs(Server.MapPath("~/Images/") + photoName);
                Profile.ProfilePhoto = photoName;
            }

            f = LVProfile.FindControl("CoverUpload") as FileUpload;

            if (f != null && f.HasFile)
            {
                String photoName = Path.GetFileName(f.PostedFile.FileName);
                f.PostedFile.SaveAs(Server.MapPath("~/Images/") + photoName);
                Profile.CoverPhoto = photoName;
            }

        }
        catch (Exception ex)
        {
            if ( err != null) err.Text = "Error saving profile: " + ex.Message;
        }

        Response.Redirect(Request.RawUrl);
    }
}