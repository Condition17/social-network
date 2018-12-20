<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewPersonalMessage.aspx.cs" Inherits="NewPersonalMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVMessage" runat="server">

        <AnonymousTemplate>
                
            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

        </AnonymousTemplate>

        <LoggedInTemplate>

            <div class="error">
                <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>    
            </div>
            
            <div class="success">
                <asp:Label ID="LBSuccess" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <div class="default-container" runat="server" id="Container" visible="false">
                    
                <div class="category-title">
                    New Message
                </div>

                <div class="default-separator"></div>
                
                <br />
                Send to: 
                <br />

                <asp:DropDownList ID="DDFriends" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
    
                <br />
                <br />
                <asp:TextBox ID="TBMessage" runat="server" Width="100%" Height="300px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The message could not be blank." ForeColor="Red" ControlToValidate="TBMessage"></asp:RequiredFieldValidator>

                <br />
                <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click" />

            </div>
        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>