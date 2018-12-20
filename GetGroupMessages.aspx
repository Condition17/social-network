<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetGroupMessages.aspx.cs" Inherits="GetGroupMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="error" >
        <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
    </div>

    <div class="default-container" runat="server" visible="false" id="Container">
            
        <asp:HyperLink ID="HLGrMembers" runat="server" >Group Members</asp:HyperLink> &nbsp;&nbsp;
        <asp:LinkButton ID="BtnLeave" runat="server" Text="Leave Group" OnClick="Btn_LeaveGroup" Visible="false"/>
        <br />
        <br />
            <asp:Label ID="LBNoMsg" runat="server" Text="There's no message in this group" Visible="true"></asp:Label>
        <br />
         
        <asp:GridView ID="GridView1" runat="server" GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" AutoGenerateColumns="False" Width="100%">
                <AlternatingRowStyle BorderStyle="None" />
                <Columns>
            
                    <asp:TemplateField>
                    
                        <ItemTemplate>
                            <div class="message">
                                <a href="GetPersonalMessages.aspx?Wall=<%#Eval("Id") %>">
                                <%# Eval("Sender.FirstName") %> <%# Eval("Sender.LastName") %>
                                </a>
                                <p>  <%# Eval("Text") %> </p>
                                <div class="post-date"> <%# Eval("Timestamp") %></div>
                            </div>
                            
                            <div class="default-separator"></div>
                        </ItemTemplate>

                    </asp:TemplateField>
                
                </Columns>
            </asp:GridView>

    </div>

    <div class="messages-input" runat="server" id="CommentArea" visible ="false" >

        <asp:TextBox ID="TBMessage" runat="server" TextMode="MultiLine" Width="100%" Height="125px"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RQValidator" ValidationGroup="ValMsg" runat="server" ErrorMessage="You can't send an empty message." ControlToValidate="TBMessage" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="MessageBtn" ValidationGroup="ValMsg" runat="server" Text="Send" OnClick="MessageBtn_Click"/>

   </div>
   
</asp:Content>

