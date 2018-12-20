<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FriendRequests.aspx.cs" Inherits="FriendRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
    <asp:LoginView ID="LVFriends" runat="server">

        <AnonymousTemplate>

            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

       </AnonymousTemplate>

        <LoggedInTemplate>
    
            <div class="error">
                <asp:Literal ID="LBError" runat="server" Visible="false"></asp:Literal>    
            </div>
        
            <div class="default-container" id="Container" runat="server" visible ="false" >
                <h4>Cereri de prietenie:
                    <br />
                    <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>
                </h4>

                <div class="default-separator"></div>

                <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" PageSize="20" AutoGenerateColumns="False">
                    <AlternatingRowStyle BorderStyle="None" />
                    <Columns>
                
                        <asp:ImageField DataImageUrlField="ProfilePhoto" DataImageUrlFormatString="images/{0}" ControlStyle-Height="80px" ControlStyle-Width="80px">
            <ControlStyle Height="80px" Width="80px"></ControlStyle>
                        </asp:ImageField>

                        <asp:TemplateField>

                            <ItemTemplate>
                                <a href="Wall.aspx?id=<%#Eval("Id") %>">
                                    <%#Eval("FirstName") %> <%#Eval("LastName") %> 
                                </a>
                                <br />
                                <asp:LinkButton ID="BtnAccept" runat="server" Text="Accept" OnClick="BtnAccept_Click" CommandArgument='<%#Eval("Id")%>' CssClass="default-btn btn-accept"/>
                                <asp:LinkButton ID="BtnDecline" runat="server" Text="Decline" OnClick="BtnDecline_Click" CommandArgument='<%#Eval("Id")%>' CssClass="default-btn btn-decline"/>

                            </ItemTemplate>
                        </asp:TemplateField>
                
                    </Columns>
       
                </asp:GridView>
            </div>

        </LoggedInTemplate>

    </asp:LoginView>

    
</asp:Content>

