using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.Security;

public partial class Search : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        List<UserProfile> results = new List<UserProfile>();

        GridView1.DataSource = results;

        if (!Page.IsPostBack && Request.Params["user"] != null)
        {
            var queryString = Request.Params["user"].ToString().ToLower();
            var members = Membership.GetAllUsers();
            var profiles = ProfileManager.GetAllProfiles(ProfileAuthenticationOption.All);

            foreach (MembershipUser member in members)
            {
                var profile = profiles[member.UserName];

                if (profile != null)
                {
                    ProfileCommon profileCommon = Profile.GetProfile(profile.UserName);
                    String firstName = profileCommon.FirstName;
                    String lastName = profileCommon.LastName;

                    if ( firstName.ToLower().Contains( queryString ) || 
                         lastName.ToLower().Contains( queryString )
                        || queryString.Contains( firstName.ToLower() ) || 
                        queryString.Contains( lastName.ToLower() ) )
                    {
                        UserProfile profileInfo = new UserProfile
                        {
                            Id = member.ProviderUserKey.ToString(),
                            FirstName = firstName,
                            LastName = lastName,
                            ProfilePhoto = profileCommon.ProfilePhoto,
                            CoverPhoto = profileCommon.CoverPhoto

                        };
                        results.Add( profileInfo );
                    }

                }

            }

            nrRezultate.Text = results.Count().ToString()+" utilizatori";
            GridView1.DataBind();
        }
    }
}