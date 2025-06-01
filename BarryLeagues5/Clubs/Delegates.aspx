<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Delegates.aspx.vb" Inherits="Delegates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divDelegates" dir="ltr">
        <asp:Label ID="Label6" runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                style="text-align:center" Text="Club Delegates & Phone Numbers"  
            Width="280px" BackColor="#1B1B1B"></asp:Label>
        <br />        <br />        
        <asp:GridView ID="gridDelegates" runat="server" BorderColor="Black" 
                BorderStyle="Solid" GridLines="None" 
                style="margin-top: 0px" Height="136px" Width="700px" 
            BackColor="#1B1B1B" Font-Size="16px">
        </asp:GridView>
        <br />
        <br />
    </div>
     
     <div class="clearboth">
    </div>
</asp:Content>
