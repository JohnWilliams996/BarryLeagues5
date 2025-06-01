<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="Fixture Result.aspx.vb" Inherits="Admin_Fixture_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 435px;
            background-color: #333333;
            height: 116px;
        }
        .style14
        {
            width: 74px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    


       <div id="divFixtureTable">
        <asp:Label ID="lblTableBefore" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Width="308px" Font-Size="18px" 
            BackColor="#1B1B1B" Height="17px">LEAGUE TABLE BEFORE RESULT      
        </asp:Label>
        <asp:GridView ID="gridTable" runat="server" GridLines="None" 
            style="margin-top: 0px" Height="136px"
            AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Pos" HeaderText="Pos">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="#FFC000" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Team"  ShowHeader="True"   HeaderText="Team" 
                    HtmlEncode="False">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" /> 
                </asp:BoundField>
                <asp:BoundField DataField="Pld" HeaderText="Pld">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="W" HeaderText="W">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt" />
                </asp:BoundField>               
                <asp:BoundField DataField="D"  HeaderText="D">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>               
                <asp:BoundField DataField="L"  headerText="L">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="Deducted" SortExpression="Pts Ded" 
                    HeaderText="Ded">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" />
                    <ItemStyle ForeColor="Orange" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Red" HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField />

            </Columns>

       </asp:GridView>
       <br />
       <asp:Button ID="btnOK" runat="server" Text="OK" BackColor="Black" 
               ForeColor="White"  Width="124px" Height="37px" />           
       <br />
       <br />    
        <asp:Label ID="lblTableAfter" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Width="299px" Font-Size="18px" 
            BackColor="#1B1B1B" Height="17px">LEAGUE TABLE AFTER RESULT      
        </asp:Label>
        <asp:GridView ID="gridTable2" runat="server" GridLines="None" 
            style="margin-top: 0px" Height="136px"
            AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="14px" CellPadding="3" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Pos" HeaderText="Pos">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="#FFC000" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Team"  ShowHeader="True"   HeaderText="Team" 
                    HtmlEncode="False">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" /> 
                </asp:BoundField>
                <asp:BoundField DataField="Pld" HeaderText="Pld">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="W" HeaderText="W">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt" />
                </asp:BoundField>               
                <asp:BoundField DataField="D"  HeaderText="D">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>               
                <asp:BoundField DataField="L"  headerText="L">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="Deducted" SortExpression="Pts Ded" 
                    HeaderText="Ded">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" />
                    <ItemStyle ForeColor="Orange" HorizontalAlign="Right" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Red" HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Move">
                <HeaderStyle ForeColor="DarkKhaki" />
                <ItemStyle ForeColor="DarkKhaki" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                </asp:BoundField>

            </Columns>


       </asp:GridView>

    </div>

               

       <asp:GridView ID="gridSkittlesResult" runat="server" GridLines="Both" 
            style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
            AutoGenerateColumns="False" BackColor="#99CCFF" 
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
<%--                <asp:BoundField DataField="Number Nines" ShowHeader="False" ItemStyle-BorderColor="Black">
                    <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="True"  Width = "40px"/>
                </asp:BoundField>
--%>            </Columns>
       </asp:GridView>
 
     <asp:GridView ID="gridCribResult" runat="server" GridLines="Both" 
            style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
            AutoGenerateColumns="False" BackColor="#FFFF99"
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
            AutoGenerateColumns="False" BackColor="#CCFFCC"
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
 
        <br />

        <asp:Label ID="lblPlayerStats" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Font-Size="18px" BorderWidth = "2PX"
            BackColor="#1B1B1B" Height="25px">Click on a Player to see his Stats for the Season
        </asp:Label>       
       <br />
       <br />

       <asp:Label ID="lblVenueLit" runat="server" ForeColor="#E4BB18" Text="Venue: " BorderWidth="0px" Font-Names="Arial"></asp:Label>
       <asp:Label ID="lblVenue" runat="server" ForeColor="#E4BB18"  BorderWidth="0px" Font-Names="Arial"></asp:Label>

       <br />
       <br />

        <asp:Label ID="lblNoCard" runat="server" Font-Names="Arial" ForeColor="Yellow" 
                style="text-align:center" Width="521px" Font-Size="18px" BorderWidth = "2PX"
            BackColor="#1B1B1B" Height="25px">RESULT CARD NOT YET GIVEN TO RESULTS SECRETARY
        </asp:Label>       
       <br />
       <asp:Button ID="btnBacktoStats" runat="server" Text="Return to Stats" BackColor="Black" 
               ForeColor="White" Height="37px" />           
       <br />
       <asp:Button ID="btnBack" runat="server" Text="&lt;Back" BackColor="Black" 
               ForeColor="White" Height="37px" />           
       


 <div id = "Admin">
    <table  class="style8" >
        <tr>
            <td align="center">
                <asp:Label ID="lblHomePointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                    width="200px" Font-Names="Arial"></asp:Label>
                <asp:DropDownList ID="ddHomePointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
            </td>
            <td class="style14" align="right">
                <asp:RadioButtonList ID="rbResults" runat="server" Font-Size="14px" 
                    ForeColor="Red" Width="90px" BackColor="#333333" Height="22px" 
                    AutoPostBack="True">
                </asp:RadioButtonList>
            </td>
            <td align="center">
              <asp:Label ID="lblAwayPointsDeducted" runat="server" ForeColor="#E4BB18" 
                    Text="Points Deducted" BorderWidth="0px" 
                    width="200px" Font-Names="Arial"></asp:Label>
                <asp:DropDownList ID="ddAwayPointsDeducted" runat="server" BackColor="#333333" 
                    ForeColor="Red">
                </asp:DropDownList>
            </td>
        </tr>
        </table>



    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="34px" 
    Width="87px" Font-Names="Arial" BackColor="Black" 
    ForeColor="White"/>

    <asp:Button ID="btnAddEditResult" runat="server" Text="Add/Edit Players" Height="34px" 
        Width="111px" Font-Names="Arial" BackColor="Red" 
        ForeColor="White"/>
    <table>
        <tr>
           <td align="left"valign="bottom">
                <br />
                <br />
                <asp:Label ID="lblNewDate"  runat="server" ForeColor="#E4BB18" Text="New Date" Font-Names="Arial" Font-Size="16px" Height="22px"></asp:Label><br />
                <asp:Calendar ID="Calendar1" runat="server" ForeColor="#E4BB18" TitleStyle-BackColor="Black" ></asp:Calendar><br />
            </td>
            <td align="left"valign="top">
                <br />
                <br />
                <asp:Label ID="lblNewStatus"  runat="server" ForeColor="#E4BB18" Text="New Status" Font-Names="Arial" Font-Size="16px" Height="22px"></asp:Label>
                <asp:TextBox ID="txtNewStatus" runat="server" Width="24px" Font-Size="16px"></asp:TextBox>  
                <br />
                <br />
                <asp:Label ID="lblError"  runat="server" backcolor="#E4BB18" 
                    ForeColor = "Black" Wrap="True"  Font-Names="Arial" Font-Size="16px" 
                    Height="22px" BorderStyle="Solid">Date OK</asp:Label>

                <br />
                <br />
                <br />
                <asp:Button ID="btnUpdateDateStatus" runat="server" 
                Text="Update Date & Status" Height="65px" 
                Font-Names="Arial" BackColor="Red"  wrap="True"
                visible="true" ForeColor="White" Width="147px" />
                
            </td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>



</div>

    
</asp:Content>
