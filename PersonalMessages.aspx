<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PersonalMessages.aspx.cs" Inherits="PersonalMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       
    <asp:LoginView ID="LVMessages" runat="server">
            
        <AnonymousTemplate>
                
            <div class="info">
                You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>
            </div>

        </AnonymousTemplate>

        <LoggedInTemplate>

            <div class="error">
                <asp:Literal ID="LBError" runat="server" Visible="false"></asp:Literal>    
            </div>

            <div class="default-container" runat="server" id="Container" visible="false">
                    
                <div class="category-title">
                    <asp:Label ID="LBNotCount" runat="server" Text=""></asp:Label>
                </div>

                <asp:GridView ID="GVNotifications" runat="server" GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" AutoGenerateColumns="False" CssClass="custom-grid">
                    <AlternatingRowStyle BorderStyle="None" />
                    <Columns>
            
                        <asp:TemplateField>
                    
                            <ItemTemplate>
                                <div class="notification">
                                        
                                    <div class="message-header">

                                        <a href="GetNotification.aspx?id=<%#Eval("Id") %>">
                                            Admin notification
                                        </a>

                                        <div class="post-date"> <%# Eval("Timestamp") %></div>
                                    </div>
                                        
                                    <asp:LinkButton ID="DelBtn" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="DelBtn_Click" CssClass="btn-decline message-delete-btn">Delete</asp:LinkButton>
                                    <div class="default-separator"></div>
                                </div>
                        
                            </ItemTemplate>

                        </asp:TemplateField>
                
                    </Columns>
                </asp:GridView>

                <br />
                <br />

                <div class="category-title">
                    Personal Messages:
                    <br />
                    <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>
                </div>
                <br />
                <asp:Button ID="Button1" runat="server" Text="New Message" OnClick="Button1_Click"/>
    
                <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="20" GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" AutoGenerateColumns="False" CssClass="custom-grid">
                    <AlternatingRowStyle BorderStyle="None" />
                    <Columns>
            
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="GetPersonalMessages.aspx?correspondent=<%#Eval("Id") %>">
                                    <%# Eval("Sender.FirstName") %> <%# Eval("Sender.LastName") %>
                                </a>
                                <div class="post-date"> <%# Eval("Timestamp") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                
                    </Columns>
 
                </asp:GridView>
            </div>

            </div>

        </LoggedInTemplate>
    </asp:LoginView>

</asp:Content>

