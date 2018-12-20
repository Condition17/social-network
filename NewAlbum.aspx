<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewAlbum.aspx.cs" Inherits="NewAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVAlbum" runat="server">
        
        <AnonymousTemplate>
            
            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

        </AnonymousTemplate>

        <LoggedInTemplate>
            
            <div class="error">
                <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="success">
                <asp:Label ID="LBSuccess" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <div class="default-container">

                <div class="category-title">
                    New Album
                </div>

                <div class="default-separator"></div>

                <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You need to provide an album name" Display="Dynamic" ForeColor="Red" ControlToValidate="TBName"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Add Album" OnClick="Button1_Click" />

            </div>

        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>

