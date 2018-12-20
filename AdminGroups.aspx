<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminGroups.aspx.cs" Inherits="Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:LoginView ID="LVGroups" runat="server">
         <AnonymousTemplate>
           You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>.
        </AnonymousTemplate>

         <RoleGroups>
             <asp:RoleGroup Roles="User">
                 <ContentTemplate>
                     You don&#39;t have proper access rights to use this page.
                 </ContentTemplate>
             </asp:RoleGroup>
             <asp:RoleGroup Roles="Admin">
                <ContentTemplate>

                    <div class="error">
                        <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
                    </div>

                    <div class="default-container">

                        <div class="category-title">
                            Administrate groups
                        </div>

                        <div class="default-separator"></div>

                        <asp:Label ID="LBNoGroup" runat="server" Text="There are no groups." Visible="false"></asp:Label>

                        <asp:GridView ID="GridView1" runat="server"  GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" PageSize="20" AutoGenerateColumns="False" Width="30%">
                        <AlternatingRowStyle BorderStyle="None" />
                        <Columns>
            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="group-name">
                                            <a href="GetGroupMessages.aspx?groupid=<%#Eval("Id") %>">
                                            <%#Eval("Name") %>
                                            </a>
                                        </div>
                                    
                                        <div class="btn-decline del-group default-btn" >
                                            <asp:LinkButton ID="DelBtn" runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="DelBtn_Click">Delete</asp:LinkButton>
                                        </div>
                                    <div class="default-separator"></div>
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                
                            </Columns>
                        
                        </asp:GridView>


                    </div>

                </ContentTemplate>
             </asp:RoleGroup>
         </RoleGroups>
    </asp:LoginView>
</asp:Content>

