<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GetGroupMembers.aspx.cs" Inherits="GetGroupMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="error">
        <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
    </div>

    <div class="default-container" runat="server" visible="false" id="Container">
            
        <div class="category-title">
            <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>
        </div>

        <div class="default-separator"></div>

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" PageSize="20" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
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

                        <asp:LinkButton ID="DelBtn" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="DelBtn_Click" CssClass="btn-decline" >Remove</asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        
    </div>

</asp:Content>