<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JoinGroup.aspx.cs" Inherits="JoinGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVGroups" runat="server">
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

            <div class="default-container" runat="server" id="Container" visible="false">
                

                <div class="category-title">
                    Join Group
                </div>

                <div class="default-separator"></div>

                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You have to select a group." ValidationGroup="DDValidator" ForeColor="Red" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="BtnJoin" runat="server" Text="Join Group" OnClick="BtnJoin_Click" ValidationGroup="DDValidator"/>

            </div>
            
        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>

