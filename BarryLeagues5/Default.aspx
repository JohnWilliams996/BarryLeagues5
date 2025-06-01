<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="League_Selection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    
    <style type="text/css">
        .header
        {
            width: 1000px;
        }
        .style1
        {
            text-align: center;
            font-family: Arial;
            font-weight: normal;
        }
        </style>
</head>

<body id="PageBody" bgcolor="black" runat="server">
    <form id="form1" runat="server">
    <div class="Title">
                <h1 style="background-color: #FFCC00; left:100px; width:1000px;" 
                    class="style1">
                    BARRY & DISTRICT LEAGUES
                </h1>
     </div>
    <div>
        <br />
        <br />
         <asp:Label ID="Label4"  runat="server" width="140px" style="height: 19px"></asp:Label>
        <asp:Label ID="Label3" runat="server" 
            Text="SELECT THE WEBSITE REQUIRED." 
            Font-Names="ARIAL" Font-Size="X-Large" ForeColor="White"></asp:Label>
        <br />
        <br />
         <asp:Label ID="Label1"  runat="server" width="140px"></asp:Label>
        <asp:Button ID="btnClubs" runat="server" Font-Size="XX-Large" Height="160px" 
            Text="Enter Barry &amp; District Clubs Website" Width="740px" />
        <br/>
        <br/>
        <br/>
        <asp:Label ID="Label2"  runat="server" width="140px"></asp:Label>
        <asp:Button ID="btnMens_Skittles" runat="server" Font-Size="XX-Large" Height="160px" 
            Text="Enter Barry &amp; District Mens Skittles Website" 
            Width="740px" />
        <br/>
        <br/>
        <br/>
        <asp:Label ID="Label6"  runat="server" width="140px"></asp:Label>
        <asp:Button ID="Button1" runat="server" Font-Size="XX-Large" Height="160px" 
            Text="Enter Barry &amp; District Pool Website" 
            Width="740px" Visible="False" />
    
        <br />
        <br />
         <asp:Label ID="Label5"  runat="server" width="140px" style="height: 19px"></asp:Label>
    
    </div>
    </form>
</body>

</html>
