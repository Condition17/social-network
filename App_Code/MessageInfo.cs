using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MessageInfo
/// </summary>
public class MessageInfo
{
    public MessageInfo( UserProfile sender, string id)
    {
        this.Id = id;
        this.Sender = sender;

    }

    public string Id { get; set; }
    public UserProfile Sender { get; set; }
    public DateTime Timestamp { get; set; }
    public String Text { get; set; }

}