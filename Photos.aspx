<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildContent2" Runat="Server">
    
    <h4>Albums:</h4>
    
    <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
    <div id="albums-container">

        <asp:GridView ID="GridView1" runat="server"  GridLines="None" BorderStyle="None" CellPadding="5" CellSpacing="5" PageSize="20" AutoGenerateColumns="False">
            <AlternatingRowStyle BorderStyle="None" />
            <Columns>
            
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="album-name">
                            <a href="Album.aspx?id=<%#Eval("Id") %>">
                            <%#Eval("Name") %>
                            </a>
                            
                            <div class="del-album">
                                <asp:LinkButton ID="DelBtn" runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="DelBtn_Click1" Visible="false">Delete</asp:LinkButton>
                            </div>

                        </div>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <EditRowStyle BorderStyle="None" />
            <EmptyDataRowStyle BorderStyle="None" />
            <FooterStyle BorderStyle="None" />
            <HeaderStyle BorderStyle="None" />
            <PagerStyle BorderStyle="None" />
            <RowStyle BorderStyle="None" />
            <SelectedRowStyle BorderStyle="None" />
            <SortedAscendingHeaderStyle BorderStyle="None" />
            <SortedDescendingCellStyle BorderStyle="None" />
            <SortedDescendingHeaderStyle BorderStyle="None" />
        </asp:GridView>

    </div>

</asp:Content>

