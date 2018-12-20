<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVAlbum" runat="server">

        <AnonymousTemplate>
           You have to be logged in to access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>.
        </AnonymousTemplate>

        <LoggedInTemplate>
            
            <div class="error">
                <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <div class="phots-container" id="photo_container" runat="server" visible="false">

                <asp:Label ID="LBAlbumName" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                <br />
                <asp:Label ID="LBNrPhotos" runat="server" Text=""></asp:Label>
                <br />
            </div>

        </LoggedInTemplate>
    </asp:LoginView>
   
</asp:Content>

