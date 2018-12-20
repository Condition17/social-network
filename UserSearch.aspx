<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserSearch.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="default-container">

        <asp:Literal ID="MessagePlaceholder" runat="server"></asp:Literal>
        <div>

            <div class="category-title">

                Rezultate cautare:
                <br />
                <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>

            </div>

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

    </div>

</asp:Content>

