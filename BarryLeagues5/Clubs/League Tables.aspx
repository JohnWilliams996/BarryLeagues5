<%@ Page Title="" Language="VB" MasterPageFile="~/Clubs/MasterPage.master" AutoEventWireup="false" CodeFile="League Tables.aspx.vb" Inherits="League_Tables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    a:link {text-decoration: none }
    a:active {text-decoration: none }
    a:visited {text-decoration: none }
    a:hover {text-decoration: underline }

    /** GRIDVIEW STYLES **/

    .gv tr.row:hover 
    {
    	background-color:Black;
    	text-decoration:underline
    }
    .gv td cell:hover 
    {
    	background-color:Black;
    	text-decoration:underline
    }
    </style>

</asp:Content>

<asp:Content ID="lblInfo" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">     
         <asp:TextBox ID="txtInfo" runat="server" Height="45px" Width="1042px" 
             BackColor="Red" Font-Names="Arial" Font-Size="Large" ForeColor="White" 
             Rows="3" TextMode="MultiLine"></asp:TextBox>
    <br />
    <br />
   
   <table id="League" cellpadding="4">
    <tr valign="top">
        <td rowspan="2">
            <asp:Label ID="Label4" Text="Click on a Team/Competition to view the Fixtures" 
                runat="server" Font-Names="Arial" ForeColor="#E4BB18" 
                    style="text-align:center" Width="211px" Height="58px" 
                BackColor="#1B1B1B"> </asp:Label>
         
         
            <asp:GridView ID="gridOptions" runat="server" CssClass="gv"
                    style="margin-top: 0px" Height="136px"   
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Long Name" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Home Night" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Venue" HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left" wrap="false"/>
                    </asp:BoundField>   
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

            </asp:GridView>           

        </td>
        <td>
            <asp:Label ID="lblLeague" runat="server" Font-Names="Arial" ForeColor="Yellow"  style="text-align:left" Font-Size="20px" 
                BackColor="#1B1B1B">LEAGUE TABLE </asp:Label>
            <br />
            <asp:HyperLink ID="hlLeagueStats" Text="League Stats" runat="server" height="16px"
            BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" ></asp:HyperLink>
            <asp:GridView ID="gridTable" runat="server" GridLines="None" CssClass="gv"
                style="margin-top: 0px"
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="4" BackColor="#1B1B1B" CellSpacing="2">
                <Columns>
                    <asp:BoundField DataField="Stats" HeaderText="">
                        <ItemStyle ForeColor="Black" Font-Size="10px" BackColor="#99CCFF" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>               
                    <asp:BoundField  DataField="Last 6"  HeaderText="Last 6">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle BackColor="#1B1B1B" ForeColor="LightGray" HorizontalAlign="Right"  Wrap="False" Font-Names="Consolas" Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pos" HeaderText="Pos">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle ForeColor="White"  BackColor="DarkGreen" HorizontalAlign="Center" Font-Size="14px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Team" HeaderText="Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" Font-Size="14px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Pld" HeaderText="Pld">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="W" HeaderText="W">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right"  />
                    </asp:BoundField>               
                    <asp:BoundField DataField="D"  HeaderText="D">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right"/>
                    </asp:BoundField>               
                    <asp:BoundField DataField="L"  headerText="L">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Deducted" SortExpression="Pts Ded" 
                        HeaderText="Pts Ded">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Width="20px" 
                        Font-Size="10px" />
                        <ItemStyle ForeColor="red" HorizontalAlign="Right" />
                    </asp:BoundField>                
                    <asp:BoundField DataField="Pts" SortExpression="Pts" HeaderText="Pts" >
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Right" Font-Size="14px" />
                    </asp:BoundField>
<%--                <asp:BoundField DataField="Number Nines" SortExpression="Number_Nines" 
                        ItemStyle-ForeColor="#0099FF" HeaderText="9+">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Right" />
                    </asp:BoundField>
--%>            </Columns>
           </asp:GridView>
           <br />
           <br />

        </td>
        
        <td>
            <asp:Label ID="lblRecentResults" Text="RECENT RESULTS (* = Points Deducted)" runat="server" 
                Font-Names="Arial" ForeColor="Yellow"  style="text-align:left"   Font-Size="20px" 
                BackColor="#1B1B1B" ></asp:Label>
           <br />
           <br />

            <asp:GridView ID="gridRecentResults" runat="server" BorderColor="Black" GridLines="None" CssClass="gv"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="3" BackColor="#1B1B1B" CellSpacing="3" >
                <Columns>
                    
                    <asp:BoundField DataField="Fixture Calendar" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>                
       
                   <asp:BoundField DataField="Home Team Name" HeaderText="Home Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Right" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Home Result" HeaderText="Result">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                        <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>

 <%--                  <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="More"  NavigateUrl="" ShowHeader="True"  HeaderText="">
                        <ControlStyle Font-Underline="True" ForeColor="White"></ControlStyle>
                        <ItemStyle Wrap="False" />
                    </asp:HyperLinkField>
--%>

                    <asp:BoundField DataField="More" HeaderText="">
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Left" Wrap="False" Font-Size="12px"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Fixture ID" HeaderText="">
                        <ItemStyle ForeColor="Red" HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>

               </Columns>
            </asp:GridView>
<%--          <asp:Label ID="lblFiller"  runat="server" width="263px"></asp:Label>--%>

            <br />
           <br />
           <br />

 
        </td>
    </tr>

    <tr valign="top">
        <td>
            <asp:Label ID="lblHighScores" Text="HIGH SCORES" runat="server" 
                BorderColor="Black" Font-Names="Arial" ForeColor="Yellow" 
                    style="text-align:left"  Font-Size="20px"
                BackColor="#1B1B1B"></asp:Label>
            <asp:GridView ID="gridHS" runat="server" GridLines="None" CssClass="gv"
                style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="14px" CellPadding="5" BackColor="#1B1B1B" CellSpacing="3">
                <Columns>
                    <asp:BoundField DataField="HomeAway" HeaderText="H/A">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" />
                        <ItemStyle ForeColor="#FFC000" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>           

<%--                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="White"  
                        DataTextField="Player"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Player">
                        <ControlStyle Font-Underline="False" ForeColor="White"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Left" />
                    </asp:HyperLinkField>
--%>       
                   <asp:BoundField DataField="Player" HeaderText="Player">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>

                   <asp:BoundField DataField="Team" HeaderText="Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>
                                        
<%--                    <asp:HyperLinkField  ControlStyle-Font-Underline="False" ControlStyle-ForeColor="Cyan"  
                        DataTextField="Team"  NavigateUrl="" ShowHeader="True" 
                            HeaderText="Team">
                        <ControlStyle Font-Underline="False" ForeColor="Cyan"></ControlStyle>
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False"  HorizontalAlign="Left" />
                    </asp:HyperLinkField>

--%>                    <asp:BoundField DataField="Score" HeaderText="Score">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Right" />
                    </asp:BoundField>               
               </Columns>
            </asp:GridView>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>

        <td>
          <asp:Label ID="lblLateResults" Text="LATE RESULTS" runat="server" CssClass="gv"
                Font-Names="Arial" ForeColor="Yellow" 
                    style="text-align:left" Width="148px"  Font-Size="20px" 
                BackColor="#1B1B1B"></asp:Label>
            <asp:GridView ID="gridLateResults" runat="server" CssClass="gv"
                BorderStyle="None" BorderWidth="1px" GridLines="None" 
                style="margin-top: 0px;"
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="12px" CellPadding="5" BackColor="#1B1B1B">
                <Columns>
                    <asp:BoundField DataField="Fixture Calendar" HeaderText="Fixture Date">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                        <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False"  />
                    </asp:BoundField>   

                    <asp:BoundField DataField="Home Team Name" HeaderText="Home Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Right" Wrap="False"/>
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Right" Wrap="False"/>
                    </asp:BoundField>   

                    <asp:BoundField DataField="Home Result" HeaderText="">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="center" Wrap="False"   />
                        <ItemStyle ForeColor="LightGreen" HorizontalAlign="Right" Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Away Team Name" HeaderText="Away Team">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
             
               </Columns>

            </asp:GridView>
        </td>
    </tr>
    </table>
    <br />
    <br />

        
</asp:Content>
