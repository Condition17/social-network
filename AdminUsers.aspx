<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminUsers.aspx.cs" Inherits="AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:LoginView ID="LVUsers" runat="server">
         <AnonymousTemplate>
           You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>.
        </AnonymousTemplate>
        <LoggedInTemplate>
            
        </LoggedInTemplate>
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

                    <div class="default-container" >
                        
                        <div class="category-title">
                            Administrate users
                        </div>

                        <div class="default-separator"></div>

                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" PageSize="20" AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound" >
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


                                    <div class="btn-admin-user">
                                        <asp:LinkButton ID="Ban" runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="BanBtn_Click" Visible="false">Ban</asp:LinkButton>
                                    </div>
                                    <div class="btn-admin-user">
                                        <asp:LinkButton ID="Unban" runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="UnbanBtn_Click" Visible="false">Unban</asp:LinkButton>
                                    </div>
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

