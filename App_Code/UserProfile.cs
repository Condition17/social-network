using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserProfile
/// </summary>
public class UserProfile
{
    public UserProfile()
    {
    }

    public UserProfile(String Id,ProfileCommon profile)
    {
        this.Id = Id;
        this.FirstName = profile.FirstName;
        this.LastName = profile.LastName;
        this.ProfilePhoto = profile.ProfilePhoto;
        this.CoverPhoto = profile.CoverPhoto;
        this.Gender = profile.Gender;
        this.RelationshipStatus = profile.RelationshipStatus;
        this.School = profile.School;
        this.Type = profile.Type;
        this.Work = profile.Work;
        this.City = profile.City;
   
    }

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public int Gender { get; set; }
    public string Work { get; set; }
    public string School { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string RelationshipStatus { get; set; }
    public string ProfilePhoto { get; set; }
    public string CoverPhoto { get; set; }
    public string Type { get; set; }
    public string Email { get; set; }

}