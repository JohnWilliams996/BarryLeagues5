<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Honours.aspx.vb" Inherits="Honours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #HonoursXL
        {
            width: 631px;
            height: 335px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   <table id="League" cellpadding="4">
    <tr valign="top">
        <td>
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center" Text="Select Season"  Width="120px"
                BackColor="#1B1B1B" BorderWidth="1px"></asp:Label>
            <br />        
            <br />
            <asp:RadioButtonList ID="rbSeasons" runat="server" AutoPostBack="true" oncheckedchanged="rbSeasons_SelectedIndexChanged" 
                ForeColor="#E4BB18" BackColor="Black" Width="120px">
            </asp:RadioButtonList>
        <br />        
        <br />
            <asp:Button ID="btnBack" runat="server" Text="&lt; Back" BackColor="Black" 
                ForeColor="#E4BB18" BorderWidth="1px" Height="32px" />
        </td>
        <td>    
            <asp:ImageButton ID="HonoursJPG" runat="server"
                Width="1400px" Height="700px" 
                BorderColor="#666699" BorderStyle="none" BorderWidth="0px" />
        </td>
    </tr>
    </table>
    
    
</asp:Content>
