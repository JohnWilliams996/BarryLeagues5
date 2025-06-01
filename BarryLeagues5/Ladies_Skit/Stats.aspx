<%@ Page Title="" Language="VB" MasterPageFile="~/Ladies_Skit/MasterPage.master" AutoEventWireup="false" CodeFile="Stats.aspx.vb" Inherits="Stats" MaintainScrollPositionOnPostback="true"  %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btnBack1" runat="server" Text="&lt; Back" BackColor="Black" 
            ForeColor="White" Height="32px" />
    <br />
    <br />

    <table cellpadding="3" cellspacing="3" style="margin-left: 0px">
    <tr>
        <td valign = "top" colspan="2">
            <asp:Label ID="Label3" runat="server" Text="1. Select League "  
                ForeColor="#E4BB18" BackColor="#003366" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="Larger" ></asp:Label>
             <asp:GridView ID="gridLeagues" runat="server" CssClass="gv"
                    style="margin-top: 0px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="18px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="both" >
                <Columns>
                    <asp:BoundField DataField="League1" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="League2" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="League3" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="League4" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="League5" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="League6" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                </Columns>

                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:GridView>   
            <br />
        </td>
    </tr>
    <tr>
        <td valign = "top">
                <asp:Label ID="lblTop10" Text="TOP 10 LEAGUE PLAYERS (MIN 50% OF RESULT CARDS RETURNED TO COUNT)"
                     Height="20px" runat="server" ForeColor="#E4BB18" BorderWidth="0px"  Font-Names="Arial" BackColor="#1B1B1B" Font-Size="16px"
                    Visible="False">
                </asp:Label>
                <br />
                <asp:Hyperlink ID="hlAll" Text = "Show All Players" Height="27px" 
                    runat="server" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Black" BackColor="Red" Font-Size="14px" Width="114px" 
                    Visible="False" />
               <asp:Hyperlink ID="hlTop10" Text = "Show Top 10 Players" Height="27px" 
                    runat="server" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Black" BackColor="Red" Font-Size="14px" Width="137px" 
                    Visible="False" />
    
             <asp:GridView ID="gridLeagueStats" runat="server" CssClass="gv"
                    style="margin-top: 0px"  width="200px" 
                AutoGenerateColumns="False" Font-Size="14px"
                Font-Names="Arial" BackColor="#1B1B1B" 
                CellPadding="3" ShowHeader="False" ForeColor="#333333" 
                GridLines="None">
                <Columns>
                    <asp:BoundField  DataField="Last 6"  HeaderText="">
                    <ItemStyle BackColor="#1B1B1B" ForeColor="DarkGray" HorizontalAlign="Right"  Wrap="False"/>
                    </asp:BoundField>

                    <asp:BoundField  DataField="League Pos"  HeaderText="">
                    <ItemStyle BackColor="DarkGreen" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Player" HeaderText="">
                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Team" HeaderText="">
                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left"  Wrap="False" />
                    </asp:BoundField>
 
                    <asp:BoundField DataField="Total Played" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="LightGreen" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Total Pins" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Average" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="DarkSlateGray"  ForeColor="White" HorizontalAlign="Right"  Wrap="False" Font-Bold="true"/>
                    </asp:BoundField>

                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:GridView>   

        </td>
        <td valign="top">
             <asp:Chart ID="chtLeague" runat="server" ViewStateContent="Appearance" 
                BackColor="#1B1B1B" 
                BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                BorderlineColor="Transparent" Palette="SeaGreen" Width="350px" 
                Visible="False" >
                 <Series>
                     <asp:Series ChartType="Bar" Name="League">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true"  >
                     </asp:ChartArea>
                 </ChartAreas>          
            </asp:Chart>
         </td>
    </tr>
    <tr valign="top">
        <td colspan="2">    
            <br />   
            <asp:Label ID="Label2" runat="server" Text="2. Select Team"  
                ForeColor="#E4BB18" BackColor="#003366" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="Larger" >
            </asp:Label>
             <asp:GridView ID="gridTeams" runat="server" CssClass="gv"
                    style="margin-top: 0px"  width="180px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="16px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="Both">
                <Columns>
                    <asp:BoundField DataField="Team1" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team2" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team3" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team4" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team5" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team6" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team7" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Team8" HeaderText="">
                        <ItemStyle ForeColor="Cyan" BackColor="#1B1B1B"  HorizontalAlign="left" Wrap="False"/>
                    </asp:BoundField>   
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:GridView>   
            <br />

        </td>
    </tr>
    <tr >
        <td valign="top">    
                <asp:Label ID="lblTeamRule" Text="TEAM STATS" Height="40px"  
                        runat="server" ForeColor="#E4BB18" BorderWidth="0px" 
                    Font-Names="Arial" BackColor="#1B1B1B" Font-Size="16px" 
                    Visible="False"></asp:Label>
  
  
             <asp:GridView ID="gridTeamStats" runat="server" CssClass="gv" style="margin-top: 0px"  width="200px" 
                AutoGenerateColumns="False"  Font-Names="Arial" BackColor="#1B1B1B" CellPadding="2" ShowHeader="False" 
                ForeColor="#333333" Font-Size="14px" GridLines="None">
                <Columns>
                    <asp:BoundField  DataField="Last 6"  HeaderText="">
                        <ItemStyle BackColor="#1B1B1B" ForeColor="DarkGray" HorizontalAlign="Right"  Wrap="False"/>
                    </asp:BoundField>

                    <asp:BoundField  DataField="League Pos"  HeaderText="">
                        <ItemStyle BackColor="DarkGreen" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField  DataField="Team Pos"  HeaderText="">
                       <ItemStyle BackColor="DarkBlue" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Player" HeaderText="">
                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Left"  Wrap="False" />
                    </asp:BoundField>
 
                     <asp:BoundField DataField="Total Played" HeaderText="">
                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="LightGreen" HorizontalAlign="center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Total Pins" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Total Average" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="DarkSlateGray"  ForeColor="White" HorizontalAlign="Right"  Wrap="False" Font-Bold="true"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Total High Score" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Home Played" HeaderText="">
                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                    <ItemStyle BackColor="#333333" ForeColor="LightGreen" HorizontalAlign="center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Home Pins" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#333333" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Home Average" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#333333"  ForeColor="White" HorizontalAlign="Right"  Wrap="False" Font-Bold="true"/>
                    </asp:BoundField>
  
                    <asp:BoundField DataField="Home High Score" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#333333" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Away Played" HeaderText="">
                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="LightGreen" HorizontalAlign="center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Away Pins" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Away Average" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B"  ForeColor="White" HorizontalAlign="Right"  Wrap="False" Font-Bold="true"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Away High Score" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Away Thirties" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>
   
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:GridView>   

         </td>
        <td valign="top">
            <br />

              <asp:Hyperlink ID="hlShowResults" Text = "<< Show Team Results" Height="20px" 
                    runat="server" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Black" BackColor="Red" Font-Size="14px" 
                    Visible="False" />

             <asp:Hyperlink ID="hlHideResults" Text = "Hide Team Results >>" Height="20px" 
                    runat="server" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Black" BackColor="Red" Font-Size="14px" 
                    Visible="False" />

             <asp:Label ID="lblClickResult" Text="Click on the Result to View the Card" Height="20px" 
                    runat="server" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Red" BackColor="Black" Font-Size="12px" bordercolor="Black"
                    Visible="False" />

            <br />
             <asp:GridView ID="gridResults" runat="server" GridLines="None" CssClass="gv" Width="230px" Visible="false"
             AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" CellPadding="1" BackColor="#1B1B1B">
            <Columns>
                <asp:BoundField DataField="Week Number" HeaderText="Wk" >
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle ForeColor="White" BackColor="DarkGreen" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Fixture Calendar" HeaderText="Date">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle ForeColor="White" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="Home Team Name" HeaderText="Opponents">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Left" Wrap="False"/>
                    <ItemStyle ForeColor="Cyan" HorizontalAlign="Left" Wrap="False"/>
                </asp:BoundField>                            
                           
                <asp:BoundField DataField="Away Team Name" HeaderText="H/A">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="cENTER" Wrap="False"/>
                    <ItemStyle ForeColor="LightGreen" HorizontalAlign="cENTER" Wrap="False"/>
                </asp:BoundField>     
                                        
                <asp:BoundField DataField="Home Result"  headerText="Result">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                    <ItemStyle ForeColor="DarkGray" BackColor="DarkGreen" HorizontalAlign="Center" Wrap="False"/>
                </asp:BoundField>

                <asp:BoundField DataField="Rolls Result" HeaderText="Rolls">
                    <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Wrap="False"/>
                    <ItemStyle ForeColor="red" HorizontalAlign="Center" Wrap="False"/>
                </asp:BoundField>

                <asp:BoundField DataField="Fixture ID" SortExpression=""  visible="True"
                    HeaderText="">
                    <ItemStyle ForeColor="red" />
                </asp:BoundField>

                </Columns>
              </asp:GridView>

              <asp:Chart ID="chtTeam1" runat="server" ViewStateContent="Appearance" 
                BackColor="#1B1B1B" 
                BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                BorderlineColor="Transparent" Palette="SeaGreen" Width="350px" 
                 Visible="False" >
                 <Series>
                     <asp:Series ChartType="Bar" Name="Team">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true" >
                     </asp:ChartArea>
                 </ChartAreas>          
            </asp:Chart>
          </td>

         <td valign="top">
            <br />

             <asp:Chart ID="chtTeam2" runat="server" ViewStateContent="Appearance" 
                BackColor="#1B1B1B" 
                BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                BorderlineColor="Transparent" Palette="SeaGreen" Width="350px" 
                 Visible="False" >
                 <Series>
                     <asp:Series ChartType="Bar" Name="Team">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true" >
                     </asp:ChartArea>
                 </ChartAreas>          
            </asp:Chart>
         </td>
    </tr>

  
    <tr valign="top">
        <td colspan="2"> 
            <br />   
            <asp:Label ID="lblSelectPlayer" runat="server" Text="3. Select Player"  
                ForeColor="#E4BB18" BackColor="#003366" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="Larger" ></asp:Label>
             <asp:GridView ID="gridPlayers" runat="server" CssClass="gv"
                    style="margin-top: 0px"  width="180px" 
                AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="13px" BackColor="#1B1B1B" 
                CellPadding="4" ShowHeader="False" ForeColor="#333333" 
                GridLines="Both">
                <Columns>
                    <asp:BoundField DataField="Player1" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player2" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player3" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player4" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player5" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player6" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player7" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player8" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player9" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                    <asp:BoundField DataField="Player10" HeaderText="">
                        <ItemStyle ForeColor="white" BackColor="#1B1B1B"  HorizontalAlign="Left" Wrap="False"/>
                    </asp:BoundField>   
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:GridView>   
            <br />
        </td>
    </tr>
    <tr>
        <td valign="top">
           <br />
                <asp:Label ID="lblPlayerStats" Text="PLAYER STATS ()" Height="33px"  
                        runat="server" ForeColor="#E4BB18" BackColor="#1B1B1B" BorderWidth="0px" 
                    Font-Names="Arial" Font-Size="16px"
                    Visible="False"></asp:Label>

                
                <asp:GridView ID="gridPlayerStats" runat="server" CssClass="gv"
                    style="margin-top: 0px" 
                    AutoGenerateColumns="False"
                    Font-Names="Arial" BackColor="#1B1B1B" cellspacing="2"
                    CellPadding="2" ShowHeader="False" ForeColor="#333333" Font-Size="12px"
                    GridLines="None">
                <Columns>

                    <asp:BoundField  DataField="Week"  HeaderText="">
                    <ItemStyle BackColor="DarkGreen" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Date" HeaderText="">
                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="LightGreen" HorizontalAlign="Left"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Opponents" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="Cyan" HorizontalAlign="Left"  Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="H/A" HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B" ForeColor="White" HorizontalAlign="Center"  Wrap="False" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Score" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="DarkSlateGray"  ForeColor="White" HorizontalAlign="Center"  Wrap="False" Font-Bold="true"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Number Thirties" HeaderText="" HeaderStyle-Font-Bold="true" Visible = "true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B"  ForeColor="LightGreen" HorizontalAlign="Center"  Wrap="true"/>
                    </asp:BoundField>

                   <asp:BoundField DataField="Result" HeaderText="" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="DarkGreen"  ForeColor="White" HorizontalAlign="Left"  Wrap="False"/>
                    </asp:BoundField>

                   <asp:BoundField DataField="Rolls Result" HeaderText="Rolls">
                        <HeaderStyle ForeColor="DarkKhaki" HorizontalAlign="Center" Width="10px" />
                        <ItemStyle BackColor="#1B1B1B" ForeColor="red" HorizontalAlign="Center"/>
                    </asp:BoundField>

                   <asp:BoundField DataField="Fixture ID" HeaderText="" HeaderStyle-Font-Bold="true"  Visible="False">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle BackColor="#1B1B1B"  ForeColor="White" HorizontalAlign="Center"  Wrap="False"/>
                    </asp:BoundField>

 
                </Columns>
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

            </asp:GridView>   


           <asp:button ID="btnClose" runat="server" Text="Hide Card" BackColor="Red"  Width="163px"  Font-Size="20px" Visible="false"/>

           <asp:Image ID="imgCard" runat="server" ImageAlign="Left" Height="450px" Width="450px" />
           <asp:GridView ID="gridResult" runat="server" GridLines="Both" 
                style="margin-top: 0px" Height="136px" HeaderStyle-BorderColor="Black"
                AutoGenerateColumns="False" BackColor="#99CCFF" 
                Font-Names="Arial" Font-Size="14px" CellPadding="2">
                <Columns>
                    <asp:BoundField DataField="Match" ShowHeader="False" ItemStyle-BorderColor="Black" >
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="false"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Home Player" ShowHeader="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="lEFT" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Home Points"  ShowHeader="False" HtmlEncode="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                    <asp:BoundField DataField="Away Player" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Left"  Wrap="false" />
                    </asp:BoundField>                   
                    <asp:BoundField DataField="Away Points"  ShowHeader="False"  HtmlEncode="False" ItemStyle-BorderColor="Black">
                        <ItemStyle ForeColor="BLACK" HorizontalAlign="Center" Wrap="False" /> 
                    </asp:BoundField>
                </Columns>
           </asp:GridView>
 
            <br />

            <asp:Label ID="lblClickPlayerStats" runat="server" Font-Names="Arial" ForeColor="Yellow" Visible="false"
                    style="text-align:center" Font-Size="18px" BorderWidth = "2PX"
                BackColor="#1B1B1B" Height="75px" Width="400px">Click on a Player to see her Stats for the Season. If any of the names are wrong, leave a message on the league WhatsApp group.
            </asp:Label>       
                
            <br />        

            <br />        

        </td>

        <td valign="top">
             <asp:Chart ID="chtPlayer" runat="server" ViewStateContent="Appearance" 
                BackColor="#1B1B1B" 
                BackImageTransparentColor="Transparent" BackSecondaryColor="64, 64, 64" 
                BorderlineColor="Transparent" Palette="SeaGreen" 
                 Visible="False" >
                 <Series>
                     <asp:Series ChartType="RangeColumn" Name="Player"
                     IsValueShownAsLabel="True" CustomProperties="LabelStyle=Bottom" >
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="MainChartArea" Area3DStyle-Enable3D="true" BackColor="LightGray" >
                         <AxisY>
                            <StripLines>
                               <asp:StripLine TextAlignment="Near" BorderDashStyle="Solid" BorderColor="Orange" BorderWidth="4" BackColor="Orange" />
                            </StripLines>
                         </AxisY>                     
                     </asp:ChartArea>
                 </ChartAreas>          
            </asp:Chart>
        </td>
 
    </tr>
  </table>   
    <asp:Button ID="btnBack2" runat="server" Text="&lt; Back" BackColor="Black" 
        ForeColor="White" Height="32px" />
</asp:Content>

