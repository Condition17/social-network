<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPost.aspx.cs" Inherits="EditPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h5>Edit post:</h5>
    <br />
        <div style="color:red">
            <asp:Label ID="LBError" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    <br />
    <asp:Label ID="LBHasImage" runat="server" Text="This post has an image so the text can be blank." Visible="false" ></asp:Label>
    <br />
    <asp:TextBox ID="TBDescription" runat="server" Width="100%" Height="300px" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Button ID="BtnUpdate" runat="server" Text="Update"  OnClick="BtnUpdate_Click" ValidationGroup="DescVal"/>
</asp:Content>

