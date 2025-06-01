<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Meeting Minutes.aspx.vb" Inherits="Mens_Skit_Meeting_Minutes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table id="League" cellpadding="4">
    <tr valign="top">
        <td rowspan="2">
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center" Text="Select Meeting or League Rules"  
            Width="233px" BackColor="#1B1B1B" BorderWidth="1px"></asp:Label>
        <br />        
        <br />        
        <asp:RadioButtonList ID="rbMeetings" runat="server" 
            ForeColor="#E4BB18" BackColor="Black" Width="340px">
        </asp:RadioButtonList>
        <br />

        <asp:Button ID="btnOpenWebsite" runat="server" BackColor="Black" BorderColor="#E4BB18" 
            Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" 
            Height="32px" Text="Open in Website" BorderStyle="Solid"  visible="true" Width="251px" />
        <br />        
        <br />        
  
        <asp:Button ID="btnOpenPDF" runat="server" BackColor="Black" BorderColor="#E4BB18" 
            Font-Names="Arial" Font-Size="15px" ForeColor="#E4BB18" Target="_blank" 
            Height="32px" Text="Open/Print Minutes (PDF)" BorderStyle="Solid"  visible="true" Width="251px" />
              <br />   
        <td class="style8">
            <asp:TextBox ID="txtMinutes" runat="server" Font-Names="Arial" 
                TextMode="MultiLine" Height="700px" 
                Width="877px" BackColor="Black" ForeColor="White" Font-Size="Medium" 
                style="margin-top: 0px"></asp:TextBox>
            <br />
            <br />


        </td>
        
        
    </tr>


 </table>

</asp:Content>

