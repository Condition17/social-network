<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetPost.aspx.cs" Inherits="GetPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="error">
        <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <div class="success">
        <asp:Label ID="LBSuccess" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <br />

    <div class="post-container" runat="server" id="postContainer" visible="false">
        <asp:Image ID="IMProfile" runat="server" Width="50px" Height="50px" />
        <asp:HyperLink ID="HypProfile" runat="server" Text="" Font-Size="14px"></asp:HyperLink>
    
        <br />
        <asp:Label ID="LBDate" runat="server" Text="" Font-Size="0.7em" ForeColor="Gray"></asp:Label>
    
        <div id="post-text">
            <asp:Label ID="LBText" runat="server" Text=""></asp:Label>
        </div>
    
        <div id="post-image">
            <asp:Image ID="IMPost" runat="server" Width="100%"/>
        </div>

        <div style="margin-top: 15px; display:inline-flex;">
           <asp:LinkButton ID="BtnDelPost" runat="server" OnClick="BtnDelPost_Click" Visible="false" >Delete Post</asp:LinkButton>
            
            <div style="margin-left: 15px">
               <asp:LinkButton ID="BtnEditPost" runat="server" OnClick="BtnEditPost_Click" Visible="false" >Edit Post</asp:LinkButton>
            </div>

        </div>

    </div>

    <div class="comments-container" runat="server" id="comments_container">

    </div>

    <asp:LoginView ID="LVComments" runat="server">
        <AnonymousTemplate>
            You have to be logged in to comment on posts. <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">Login/Signup</asp:HyperLink>.
        </AnonymousTemplate>
        <LoggedInTemplate>
            <div class="comment-field" runat="server" id="commentsInput">

                <asp:TextBox ID="TBComment" runat="server" TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox>
                <br />
                <asp:Button ID="CommentBtn" runat="server" Text="Comment" OnClick="CommentBtn_Click"/>

            </div>
        </LoggedInTemplate>
    </asp:LoginView>
    
    

</asp:Content>

