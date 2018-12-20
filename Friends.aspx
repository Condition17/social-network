<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Friends.aspx.cs" Inherits="Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildContent2" Runat="Server">
        
        <div style="color: red">
           <asp:Literal ID="LBError" runat="server"></asp:Literal>
        </div>

        <div>
            
            <h5>
                <br />
                <asp:Label ID="nrRezultate" runat="server" Text=""></asp:Label>
            </h5>

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
</asp:Content>

