<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetGroups.aspx.cs" Inherits="GetGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVGroups" runat="server">
        
        <AnonymousTemplate>
                
            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

        </AnonymousTemplate>

        <LoggedInTemplate>

            <div class="error">
                <asp:Label runat="server" Text="" ID="LBError" Visible="false"></asp:Label>
            </div>

            <div class="default-container" runat="server" id="Container">

                <div class="category-title">
                    My Groups:
                    <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>
                </div>

                <div class="separator"></div>

                <asp:Button ID="Button1" runat="server" Text="New Group" OnClick="Button1_Click"/>&nbsp;
                <asp:Button ID="BtnJoin" runat="server" Text="Join Group" OnClick="BtnJoin_Click"/>

                <asp:GridView ID="GridView1" runat="server" GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" AutoGenerateColumns="False" Width="100%">
                    <AlternatingRowStyle BorderStyle="None" />
                    <Columns>
            
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="GetGroupMessages.aspx?groupid=<%#Eval("Id") %>">
                                    <%# Eval("Name") %>
                                </a>
                                <div class="default-separator"></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                
                    </Columns>
            
                </asp:GridView>
            </div>

        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>

