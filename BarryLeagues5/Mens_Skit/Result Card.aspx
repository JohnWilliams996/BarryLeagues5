<%@ Page Title="" Language="VB" MasterPageFile="~/Mens_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Result Card.aspx.vb" Inherits="Mens_Skit_ResultCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 453px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div id="divResult" dir="ltr">
         <table style="margin-right: 5px;" cellpadding="1" cellspacing="1">
            <tr>
                <td valign="top">
                    <asp:RadioButtonList
                        ID="rbView" 
                        runat="server" 
                        AutoPostBack="true"  
                        oncheckedchanged="rbView_SelectedIndexChanged"
                        foreColor="#E4BB18" 
                        BackColor="Black" 
                        RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                    </asp:RadioButtonList>
                </td>
             </tr>
             <tr>
                <td valign="top" class="auto-style1">
                    <asp:Image ID="imgCard" runat="server" Height="450px" Width="700px" BorderStyle="Solid" BorderWidth="1px" ImageAlign="Middle" />
                </td>
                <td valign="top">
                    <asp:GridView ID="gridResult" runat="server" GridLines="Both"  
                        style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                        AutoGenerateColumns="False" BackColor="White"  
                        Font-Names="Arial" Font-Size="14px" CellPadding="3" CellSpacing="2" BorderColor="Silver">
                        <Columns>
                            <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-Width="30%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Score"  ShowHeader="False" HtmlEncode="False" ItemStyle-Width="8%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Roll"  ShowHeader="False" HtmlEncode="False" ItemStyle-Width="10%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Roll No"  ShowHeader="False" HtmlEncode="False" ItemStyle-Width="2%" >
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Roll"  ShowHeader="False" HtmlEncode="False" ItemStyle-Width="10%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Player" showheader="False" ItemStyle-Width="30%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Score"  ShowHeader="False"  HtmlEncode="False" ItemStyle-Width="8%">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblComments" runat="server" Font-Names="Arial" ForeColor="Yellow" style="text-align:center" Font-Size="18px" BorderWidth = "1px" 
                        BackColor="#1B1B1B" Height="50px" Width="633px">Click on a Player in the grid above to see his Stats for the Season. If any of the names are wrong, leave a message on the league WhatsApp group.
                    </asp:Label>       
                </td>
                <td valign="top">
                    <asp:button ID="btnClose" runat="server" Text="Close" BackColor="Red" Font-Size="25px" CausesValidation="False"/>
                </td>
            </tr>
        </table>
   </div>
     
   <div class="clearboth">
   </div>
 

</asp:Content>

