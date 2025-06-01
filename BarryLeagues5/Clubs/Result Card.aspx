<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Result Card.aspx.vb" Inherits="Clubs_ResultCard" %>

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
                        visible="true"
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
                  <asp:GridView ID="gridSkittlesResult" runat="server" GridLines="Both" 
                        style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                        AutoGenerateColumns="False" BackColor="#99CCFF" visible="false"
                        Font-Names="Arial" Font-Size="14px" CellPadding="2">
                        <Columns>
                            <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" Width = "160px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                        </Columns>
                   </asp:GridView>
                   <asp:GridView ID="gridCribResult" runat="server" GridLines="Both" 
                        style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                        AutoGenerateColumns="False" BackColor="#FFFF99" visible="false"
                        Font-Names="Arial" Font-Size="14px" CellPadding="2">
                        <Columns>
                            <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left" Wrap="false" Width = "160px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                        </Columns>
                   </asp:GridView>
                   <asp:GridView ID="gridSnookerResult" runat="server" GridLines="Both" 
                        style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                        AutoGenerateColumns="False" BackColor="#CCFFCC" visible="false"
                        Font-Names="Arial" Font-Size="14px" CellPadding="2">
                        <Columns>
                            <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false" Width = "15px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black" >
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" Width = "160px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false"  Width = "150px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                                <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" Width = "40px"/> 
                            </asp:BoundField>
                        </Columns>
                   </asp:GridView>
                    <asp:Label ID="lblComments" runat="server" Font-Names="Arial" ForeColor="Yellow" style="text-align:center" Font-Size="18px" BorderWidth = "1px"
                        BackColor="#1B1B1B" Height="72px" Width="450px">Click on a Player in the grid above to see his or her Stats for the Season. If any of the names are wrong, leave a message on the results WhatsApp group.
                    </asp:Label>       
                   <br />
                   <br />
                    <asp:Label ID="lblNoCard" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                        style="text-align:center" Width="450px" Font-Size="18px" BorderWidth = "2PX"
                        BackColor="#1B1B1B" Height="25px">RESULT CARD NOT YET GIVEN TO RESULTS SECRETARY 
                    </asp:Label>
 
                    <br />

                </td>

                <td valign="top">
                    <asp:button ID="btnClose" runat="server" Text="Close" BackColor="Red" Font-Size="25px"/>
                </td>

            </tr>
        </table>
   </div>
     
   <div class="clearboth">
   </div>
 

</asp:Content>

