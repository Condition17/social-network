<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewNotification.aspx.cs" Inherits="SendNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView ID="LVNotification" runat="server">

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

                        <div style="color:red">
                            <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div style="color:green">
                            <asp:Label ID="LBSuccess" runat="server" Text="" Visible="false"></asp:Label>
                        </div>

                    <div class="newNoteContainer">
                        
                        <h5>New Notification</h5>
                        <br />
                        Send to: 
                        <br />
                        <asp:DropDownList ID="DDUsers" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>               
                        <br />
                        <br />
                        <asp:TextBox ID="TBNotification" runat="server" Width="100%" Height="300px" TextMode="MultiLine"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The notification message could not be blank." ForeColor="Red" ControlToValidate="TBNotification"></asp:RequiredFieldValidator>

                        <br />
                        <asp:Button ID="BtnSend" runat="server" Text="Send" OnClick="BtnSend_Click"/>
                    
                    </div>

                </ContentTemplate>
            </asp:RoleGroup>

        </RoleGroups>

    </asp:LoginView>

</asp:Content>

