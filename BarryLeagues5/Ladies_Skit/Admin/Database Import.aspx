<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Database Import.aspx.vb" Inherits="Admin_Database_Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
    <asp:Label ID="lblInfo" runat="server" Font-Size="Medium" ForeColor="White" 
        Text="Paste SQL commands into box below, then click 'Import Tables'">
    </asp:Label>
    <br/>
    <asp:TextBox ID="txtSQL" runat="server" Height="251px" TextMode="MultiLine" 
        Visible="True" Width="1008px" Font-Size="Xx-Small" BackColor="Black" 
        BorderStyle="Solid" ForeColor="White"></asp:TextBox>
    <br />

    <asp:Button ID="btnImport" runat="server" Text="Import Tables" Font-Size="Medium" />


</asp:Content>

