<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewGroup.aspx.cs" Inherits="NewGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:LoginView ID="LVGroup" runat="server">

        <AnonymousTemplate>
                
            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

        </AnonymousTemplate>
        
        <LoggedInTemplate>

            <div class="error">
                <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <div class="default-container" runat="server" id="Container" visible="false">
                
                <div class="category-title">
                    New Group                
                </div>

                <div class="default-separator"></div>
                <br />
                Group Name: <asp:TextBox ID="TBName" runat="server" ValidationGroup="GName"></asp:TextBox><br />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The group name cannot be blank." ControlToValidate="TBName" ValidationGroup="GName" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="BtnCreate" runat="server" Text="Create Group" OnClick="BtnCreate_Click"/>
             </div>

        </LoggedInTemplate>

    </asp:LoginView> 
    

</asp:Content>

