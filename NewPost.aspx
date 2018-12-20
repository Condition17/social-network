<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewPost.aspx.cs" Inherits="NewPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVPost" runat="server">
        
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

            <div class="default-container" runat="server" id="Container">

                <div class="category-title">
                    Create a new post
                </div>

            
                <div class="default-separator"></div>
            
                <br />
            
                <asp:TextBox ID="TBDescription" runat="server" Width="100%" Height="300px" TextMode="MultiLine"></asp:TextBox>
                <br />

                Upload an image:
                <br />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br />
                <br />

    
                <asp:Label ID="LBChose" runat="server" Text="Chose Album:"></asp:Label>
                <br />
                <asp:RadioButtonList ID="RBList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Existent Album" Value="1"></asp:ListItem>  
                    <asp:ListItem Text="New Album" Value="0"></asp:ListItem>
                </asp:RadioButtonList>

                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>

                <br />
                <br />
                New Album Name: <asp:TextBox ID="TBAlbumName" runat="server"></asp:TextBox>
                <br />
                <br />

                <asp:Button ID="Button1" runat="server" Text="Post" OnClick="Button1_Click" />
            </div>
         </LoggedInTemplate>

    </asp:LoginView>
   
</asp:Content>

