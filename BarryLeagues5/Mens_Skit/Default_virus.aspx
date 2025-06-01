<%@ Page Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Default_virus.aspx.vb" Inherits="Clubs_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <asp:Label ID="Label6" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="League Update - 16th January"  
            Width="1044px" BackColor="#1B1B1B" Font-Size="XX-Large" Height="75px" 
            Font-Bold="True"></asp:Label>
        
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="1. The Welsh government have announced that all indoor restrictions will be lifted on Friday 28th January, assuming that Covid cases continue to decline in the next 2 weeks. With this is mind, the league committee have decided to restart the league on Monday 31st January with the week 19 fixtures. The webstie has been changed to reflect the new dates."
            Width="1045px" BackColor="#1B1B1B" Font-Size="X-Large" Height="123px"></asp:Label>

        <asp:Label ID="Label2" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="2. There will be a league meeting on Monday 31st January at the Liberals club at 7pm."
            Width="1045px" BackColor="#1B1B1B" Font-Size="X-Large" Height="44px"></asp:Label>
<%--        <asp:Label ID="Label2" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:left" Text="Johnwillgee@gmail.com"  
            Width="1045px" BackColor="#1B1B1B" Font-Size="X-Large" Height="71px"></asp:Label>                   
   --%>                 
   
           <asp:Button ID="btnClose" runat="server" BackColor="Black" BorderColor="#E4BB18" 
            Font-Names="Arial" Font-Size="25px" ForeColor="#E4BB18" 
            Height="32px" Text="Close" BorderStyle="Solid" Width="134px"  />
</asp:Content>
