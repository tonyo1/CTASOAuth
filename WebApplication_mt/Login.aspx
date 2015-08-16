<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebApplication_mt.Login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <a href="Login.aspx">Login</a> | <a href="Default.aspx">Default</a>
    <hr />

    <form id="form1" runat="server">
        <div>
            login
        <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Forms Login" OnClick="ButtonFrmClick" />
            <br />
            <br />


            <asp:Button ID="Button2" runat="server" Text="Google Oauth" OnClick="ButtonGoogleClick" />
            <br />
            <br />
            <br />
            <br />
            <asp:TextBox ID="TextBox3" runat="server" Width="300"></asp:TextBox>
            <br />
             <asp:Button ID="Button3" runat="server" Text="Log Out" OnClick="btnLogOut" />
        </div>
    </form>

</body>
</html>
