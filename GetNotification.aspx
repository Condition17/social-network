<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetNotification.aspx.cs" Inherits="GetNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:LoginView ID="LVNotifications" runat="server">
        
        <AnonymousTemplate>
            You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>.
       </AnonymousTemplate>

        <LoggedInTemplate>

            <div style="color: red">
                <asp:Label runat="server" Text="" ID="LBError"></asp:Label>
             </div>
             <br />

            <div class="alert-container" runat="server" id="NotifContainer" visible="false">

                <asp:Label ID="AlertText" runat="server" Text=""></asp:Label>

                <div class="post-date">
                    <asp:Label ID="LBTimestamp" runat="server" Text=""></asp:Label>
                </div>
                <asp:LinkButton ID="DelBtn" runat="server" OnClick="DelBtn_Click">Delete</asp:LinkButton>
            </div>
            
        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>

