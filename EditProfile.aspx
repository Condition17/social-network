<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <asp:LoginView ID="LVProfile" runat="server">
        
        <AnonymousTemplate>
           You have to be logged in access this page. You can create an account <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Index.aspx">here</asp:HyperLink>.
        </AnonymousTemplate>
        
        <LoggedInTemplate>

        <div id="edit-container">

            <div style="color: red;">
                <asp:Literal ID="LBError" runat="server" ></asp:Literal>
            </div>

            <div class="edit-img-container">
                
                <asp:Label ID="Label10" runat="server" Text="Profile photo:" Font-Bold="true" ></asp:Label>
                 <br />
                 <br />
                <asp:Image ID="IMProfile" runat="server" Width="400px"/>
                <br />
                <br />
                <asp:FileUpload ID="ProfileUpload" runat="server" />
                <br />
                <br />

                <asp:Label ID="Label11" runat="server" Text="Conver photo:" Font-Bold="true" ></asp:Label>
                <br />
                <br />
                <asp:Image ID="IMCover" runat="server" Width="400px"/>
                <br />
                <br />
                <asp:FileUpload ID="CoverUpload" runat="server" />


            </div>
            
            <div class="user-edit-continer">

                <h3>Update profile info</h3>
                <div style="padding: 10px">
                <asp:Label ID="Label2" runat="server" Text="First Name:"></asp:Label>
                <br />
                <asp:TextBox ID="TBFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TBFirstName" runat="server" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label6" runat="server" Text="Last Name:"></asp:Label>
                <br />
                <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TBLastName" runat="server" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label7" runat="server" Text="Birthday:"></asp:Label>
                <br />
                <asp:TextBox ID="TBBirthday" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TBBirthday" runat="server" ErrorMessage="Birthday is required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TBBirthday" ErrorMessage="Date must be in MM/DD/YYYY format" ForeColor="Red" Type="Date" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label3" runat="server" Text="Gender:"></asp:Label>
                <br />
                <div style="float: left; width: 120px">
                <asp:RadioButtonList ID="RBLGender" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Female" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
                </div>
                <div style="float: right; width: 315px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="RBLGender" runat="server" ErrorMessage="Gender is required" ForeColor="Red"></asp:RequiredFieldValidator>    
                </div>
            </div>
            <br />
            <div style="padding: 10px">
                <asp:Label ID="Label1" runat="server" Text="School:"></asp:Label>
                <br />
                <asp:TextBox ID="TBSchool" runat="server"></asp:TextBox>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label12" runat="server" Text="Work:"></asp:Label>
                <br />
                <asp:TextBox ID="TBWork" runat="server"></asp:TextBox>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label4" runat="server" Text="City:"></asp:Label>
                <br />
                <asp:TextBox ID="TBCity" runat="server"></asp:TextBox>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label5" runat="server" Text="Country:"></asp:Label>
                <br />
                <asp:TextBox ID="TBCountry" runat="server"></asp:TextBox>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label8" runat="server" Text="Relationship status:"></asp:Label>
                <br />
                <asp:DropDownList ID="DDRelationship" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem >Single</asp:ListItem>
                    <asp:ListItem >Married</asp:ListItem>
                    <asp:ListItem>Engaged</asp:ListItem>
                    <asp:ListItem>I's complicated</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div style="padding: 10px">
                <asp:Label ID="Label9" runat="server" Text="Profile type:"></asp:Label>
                <br />
                <asp:DropDownList ID="DDProfileType" runat="server">
                    <asp:ListItem>public</asp:ListItem>
                    <asp:ListItem>private</asp:ListItem>
                </asp:DropDownList>
            </div>

            <br />
            
            <div style="padding: 50px">
                <asp:Button ID="SaveButton" runat="server" Text="Save Profile" OnClick="SaveButton_Click" />
            </div>
 
            </div>

            </div>
            
        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>

